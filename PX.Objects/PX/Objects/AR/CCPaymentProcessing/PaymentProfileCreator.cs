// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.PaymentProfileCreator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.CA;
using PX.Objects.Extensions.PaymentTransaction;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing;

public class PaymentProfileCreator
{
  private CCPaymentHelperGraph graph;
  private string paymentMethod;
  private string processingCenter;
  private int? bAccountId;

  public PaymentProfileCreator(
    CCPaymentHelperGraph graph,
    string paymentMethod,
    string procCenter,
    int? bAccountId)
  {
    this.graph = graph;
    this.paymentMethod = paymentMethod;
    this.processingCenter = procCenter;
    this.bAccountId = bAccountId;
    // ISSUE: method pointer
    ((PXSelectBase) graph.CustomerPaymentMethodDetails).View = new PXView((PXGraph) graph, false, (BqlCommand) new Select<CustomerPaymentMethodDetail>(), (Delegate) new PXSelectDelegate((object) this, __methodptr(GetCpmd)));
    // ISSUE: method pointer
    ((PXSelectBase) graph.PaymentMethodDet).View = new PXView((PXGraph) graph, false, (BqlCommand) new Select<PaymentMethodDetail>(), (Delegate) new PXSelectDelegate((object) this, __methodptr(GetPaymentMethodDet)));
    // ISSUE: method pointer
    ((PXSelectBase) graph.CustomerPaymentMethods).View = new PXView((PXGraph) graph, false, (BqlCommand) new Select<PX.Objects.AR.CustomerPaymentMethod>(), (Delegate) new PXSelectDelegate((object) this, __methodptr(GetCpm)));
  }

  public PX.Objects.AR.CustomerPaymentMethod PrepeareCpmRecord()
  {
    PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.graph.CustomerPaymentMethods).Current;
    if (customerPaymentMethod == null)
      customerPaymentMethod = ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) this.graph.CustomerPaymentMethods).Insert(new PX.Objects.AR.CustomerPaymentMethod()
      {
        PaymentMethodID = this.paymentMethod,
        CCProcessingCenterID = this.processingCenter,
        BAccountID = this.bAccountId
      });
    return customerPaymentMethod;
  }

  public int? CreatePaymentProfile(TranProfile input)
  {
    PXSelect<PX.Objects.AR.CustomerPaymentMethod> customerPaymentMethods = this.graph.CustomerPaymentMethods;
    PXSelect<CustomerPaymentMethodDetail> paymentMethodDetails = this.graph.CustomerPaymentMethodDetails;
    PXSelect<PaymentMethodDetail> paymentMethodDet = this.graph.PaymentMethodDet;
    PX.Objects.AR.CustomerPaymentMethod current = ((PXSelectBase) customerPaymentMethods).Cache.Current as PX.Objects.AR.CustomerPaymentMethod;
    current.CustomerCCPID = input.CustomerProfileId;
    foreach (PaymentMethodDetail paymentMethodDetail1 in this.GetPaymentMethodDetails())
    {
      CustomerPaymentMethodDetail paymentMethodDetail2 = new CustomerPaymentMethodDetail();
      paymentMethodDetail2.PaymentMethodID = this.paymentMethod;
      paymentMethodDetail2.DetailID = paymentMethodDetail1.DetailID;
      if (paymentMethodDetail1.IsCCProcessingID.GetValueOrDefault())
        paymentMethodDetail2.Value = input.PaymentProfileId;
      ((PXSelectBase<CustomerPaymentMethodDetail>) paymentMethodDetails).Insert(paymentMethodDetail2);
    }
    CCCustomerInformationManagerGraph instance = PXGraph.CreateInstance<CCCustomerInformationManagerGraph>();
    GenericCCPaymentProfileAdapter<PX.Objects.AR.CustomerPaymentMethod> payment = new GenericCCPaymentProfileAdapter<PX.Objects.AR.CustomerPaymentMethod>((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) customerPaymentMethods);
    GenericCCPaymentProfileDetailAdapter<CustomerPaymentMethodDetail, PaymentMethodDetail> paymentDetail = new GenericCCPaymentProfileDetailAdapter<CustomerPaymentMethodDetail, PaymentMethodDetail>((PXSelectBase<CustomerPaymentMethodDetail>) paymentMethodDetails, (PXSelectBase<PaymentMethodDetail>) paymentMethodDet);
    try
    {
      instance.GetPaymentProfile((PXGraph) this.graph, (ICCPaymentProfileAdapter) payment, (ICCPaymentProfileDetailAdapter) paymentDetail);
    }
    catch (PXException ex) when (((Exception) ex).InnerException is CCProcessingException innerException && innerException.Reason == 3)
    {
      return PaymentTranExtConstants.NewPaymentProfile;
    }
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      ((PXSelectBase) customerPaymentMethods).Cache.Persist((PXDBOperation) 2);
      ((PXSelectBase) paymentMethodDetails).Cache.Persist((PXDBOperation) 2);
      transactionScope.Complete();
    }
    return current.PMInstanceID;
  }

  public void CreateCustomerProcessingCenterRecord(TranProfile input)
  {
    PXCache cach = ((PXGraph) this.graph).Caches[typeof (CustomerProcessingCenterID)];
    cach.ClearQueryCacheObsolete();
    if (((PXSelectBase<CustomerProcessingCenterID>) new PXSelectReadonly<CustomerProcessingCenterID, Where<CustomerProcessingCenterID.cCProcessingCenterID, Equal<Required<CustomerProcessingCenterID.cCProcessingCenterID>>, And<CustomerProcessingCenterID.bAccountID, Equal<Required<CustomerProcessingCenterID.bAccountID>>, And<CustomerProcessingCenterID.customerCCPID, Equal<Required<CustomerProcessingCenterID.customerCCPID>>>>>>((PXGraph) this.graph)).SelectSingle(new object[3]
    {
      (object) this.processingCenter,
      (object) this.bAccountId,
      (object) input.CustomerProfileId
    }) != null)
      return;
    CustomerProcessingCenterID instance = cach.CreateInstance() as CustomerProcessingCenterID;
    instance.BAccountID = this.bAccountId;
    instance.CCProcessingCenterID = this.processingCenter;
    instance.CustomerCCPID = input.CustomerProfileId;
    cach.Insert((object) instance);
    cach.Persist((PXDBOperation) 2);
  }

  public IEnumerable<PaymentMethodDetail> GetPaymentMethodDetails()
  {
    return GraphHelper.RowCast<PaymentMethodDetail>((IEnumerable) ((PXSelectBase<PaymentMethodDetail>) new PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Required<PaymentMethodDetail.paymentMethodID>>>>((PXGraph) this.graph)).Select(new object[1]
    {
      (object) this.paymentMethod
    }));
  }

  public void ClearCaches()
  {
    ((PXSelectBase) this.graph.CustomerPaymentMethods).Cache.Clear();
    ((PXSelectBase) this.graph.CustomerPaymentMethodDetails).Cache.Clear();
  }

  private IEnumerable GetCpmd()
  {
    PXCache cache = ((PXGraph) this.graph).Caches[typeof (CustomerPaymentMethodDetail)];
    foreach (object obj in cache.Cached)
    {
      if (cache.GetStatus(obj) == 2)
        yield return obj;
    }
  }

  private IEnumerable GetPaymentMethodDet()
  {
    return (IEnumerable) ((PXSelectBase<PaymentMethodDetail>) new PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Required<PaymentMethodDetail.paymentMethodID>>>>((PXGraph) this.graph)).Select(new object[1]
    {
      (object) this.paymentMethod
    });
  }

  private IEnumerable GetCpm()
  {
    PXCache cache = ((PXGraph) this.graph).Caches[typeof (PX.Objects.AR.CustomerPaymentMethod)];
    foreach (object obj in cache.Cached)
    {
      PX.Objects.AR.CustomerPaymentMethod customerPaymentMethod = obj as PX.Objects.AR.CustomerPaymentMethod;
      if (cache.GetStatus(obj) == 2)
      {
        int? pmInstanceId = customerPaymentMethod.PMInstanceID;
        int num = 0;
        if (pmInstanceId.GetValueOrDefault() < num & pmInstanceId.HasValue)
          yield return obj;
      }
    }
  }
}
