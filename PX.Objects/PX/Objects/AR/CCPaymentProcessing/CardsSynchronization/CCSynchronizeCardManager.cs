// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.CardsSynchronization.CCSynchronizeCardManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.AR.CCPaymentProcessing.Wrappers;
using PX.Objects.CA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.CardsSynchronization;

public class CCSynchronizeCardManager
{
  private readonly string processingCenterId;
  private readonly List<string> customerProfileIds;
  private readonly PXGraph graph;
  private readonly CreditCardReceiverFactory receiverFactory;
  private ICCProcessingPlugin processingPlugin;
  private V2SettingsGenerator settingsGenerator;
  private CCProcessingContext context;

  public CCSynchronizeCardManager(
    PXGraph graph,
    string processingCenterId,
    CreditCardReceiverFactory receiverFactory)
  {
    this.graph = graph;
    this.processingCenterId = processingCenterId;
    this.receiverFactory = receiverFactory;
    this.InitializeContext();
    this.InitializeProcessingPlugin();
    this.InitializeSettingsGenerator();
    this.customerProfileIds = new List<string>();
  }

  private void InitializeContext()
  {
    this.context = new CCProcessingContext();
    this.context.processingCenter = CCProcessingCenter.PK.Find(this.graph, this.processingCenterId);
    this.context.callerGraph = this.graph;
  }

  private void InitializeSettingsGenerator()
  {
    this.settingsGenerator = new V2SettingsGenerator((ICardProcessingReadersProvider) new CardProcessingReadersProvider(this.context));
  }

  private void InitializeProcessingPlugin()
  {
    this.processingPlugin = this.GetProcessorPlugin(this.context.processingCenter) as ICCProcessingPlugin;
    if (this.processingPlugin == null)
      throw new PXException("Could not get the processing plug-in by the {0} processing center.", new object[1]
      {
        (object) this.processingCenterId
      });
  }

  private object GetProcessorPlugin(CCProcessingCenter processingCenter)
  {
    return CCPluginTypeHelper.CreatePluginInstance(processingCenter);
  }

  public void SetCustomerProfileIds(IEnumerable<string> customerProfileIds)
  {
    this.customerProfileIds.Clear();
    this.customerProfileIds.AddRange(customerProfileIds);
  }

  public Dictionary<string, CustomerCreditCard> GetPaymentProfilesFromService()
  {
    IEnumerable<SettingsValue> settings = this.settingsGenerator.GetSettings();
    this.processingPlugin.CreateProcessor<ICCProfileProcessor>(settings);
    List<CreditCardReceiver> list = this.customerProfileIds.Select<string, CreditCardReceiver>((Func<string, CreditCardReceiver>) (i => this.receiverFactory.GetCreditCardReceiver(this.processingPlugin.CreateProcessor<ICCProfileProcessor>(settings), i))).ToList<CreditCardReceiver>();
    try
    {
      Parallel.ForEach<CreditCardReceiver>((IEnumerable<CreditCardReceiver>) list, (Action<CreditCardReceiver>) (task => task.DoAction()));
    }
    catch (AggregateException ex)
    {
      throw new PXException(ex.InnerExceptions[0].Message);
    }
    return list.Select<CreditCardReceiver, CustomerCreditCard>((Func<CreditCardReceiver, CustomerCreditCard>) (i => new CustomerCreditCard()
    {
      CreditCards = i.Result,
      CustomerProfileId = i.CustomerProfileId
    })).ToDictionary<CustomerCreditCard, string>((Func<CustomerCreditCard, string>) (i => i.CustomerProfileId));
  }

  public Dictionary<string, CustomerData> GetCustomerProfilesFromService()
  {
    return this.processingPlugin.CreateProcessor<ICCProfileProcessor>(this.settingsGenerator.GetSettings()).GetAllCustomerProfiles().ToDictionary<CustomerData, string>((Func<CustomerData, string>) (i => i.CustomerProfileID));
  }

  public Dictionary<string, CustomerData> GetUnsynchronizedCustomerProfiles()
  {
    Dictionary<string, CustomerData> profilesFromService = this.GetCustomerProfilesFromService();
    foreach (PXResult<PX.Objects.AR.CustomerPaymentMethod> pxResult in ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) new PXSelectJoinGroupBy<PX.Objects.AR.CustomerPaymentMethod, InnerJoin<CCProcessingCenterPmntMethod, On<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<CCProcessingCenterPmntMethod.paymentMethodID>>>, Where<CCProcessingCenterPmntMethod.processingCenterID, Equal<Required<CCProcessingCenterPmntMethod.processingCenterID>>, And<PX.Objects.AR.CustomerPaymentMethod.customerCCPID, IsNotNull>>, Aggregate<GroupBy<PX.Objects.AR.CustomerPaymentMethod.customerCCPID>>>(this.graph)).Select(new object[1]
    {
      (object) this.processingCenterId
    }))
    {
      string customerCcpid = PXResult<PX.Objects.AR.CustomerPaymentMethod>.op_Implicit(pxResult).CustomerCCPID;
      if (profilesFromService.ContainsKey(customerCcpid))
        profilesFromService.Remove(customerCcpid);
    }
    return profilesFromService;
  }

  public Dictionary<string, CustomerCreditCard> GetUnsynchronizedPaymentProfiles()
  {
    Dictionary<string, CustomerCreditCard> profilesFromService = this.GetPaymentProfilesFromService();
    foreach (PXResult<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail> line in ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) new PXSelectReadonly2<PX.Objects.AR.CustomerPaymentMethod, InnerJoin<CustomerPaymentMethodDetail, On<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<CustomerPaymentMethodDetail.pMInstanceID>>, InnerJoin<PaymentMethodDetail, On<CustomerPaymentMethodDetail.detailID, Equal<PaymentMethodDetail.detailID>, And<CustomerPaymentMethodDetail.paymentMethodID, Equal<PaymentMethodDetail.paymentMethodID>>>>>, Where<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID>>, And<PaymentMethodDetail.isCCProcessingID, Equal<True>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>>>(this.graph)).Select(new object[1]
    {
      (object) this.processingCenterId
    }))
      this.FilterPaymentsProfilesFromService(profilesFromService, line);
    return profilesFromService;
  }

  private void FilterPaymentsProfilesFromService(
    Dictionary<string, CustomerCreditCard> paymentProfiles,
    PXResult<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail> line)
  {
    PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = PXResult<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail>.op_Implicit(line);
    CustomerPaymentMethodDetail paymentMethodDetail = PXResult<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail>.op_Implicit(line);
    string customerCcpid = customerPaymentMethod.CustomerCCPID;
    string checkPaymentProfileId = paymentMethodDetail.Value;
    if (customerCcpid == null || !paymentProfiles.ContainsKey(customerCcpid))
      return;
    CustomerCreditCard paymentProfile = paymentProfiles[customerCcpid];
    int index = paymentProfile.CreditCards.FindIndex((Predicate<CreditCardData>) (i => i.PaymentProfileID == checkPaymentProfileId));
    if (index == -1)
      return;
    paymentProfile.CreditCards.RemoveAt(index);
  }
}
