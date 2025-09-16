// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.CCCustomerInformationManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Factories;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Interfaces;
using PX.Objects.AR.CCPaymentProcessing.Repositories;
using PX.Objects.AR.CCPaymentProcessing.Wrappers;
using PX.Objects.AR.Repositories;
using PX.Objects.CA;
using PX.Objects.Common.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing;

public class CCCustomerInformationManager
{
  private ICardProcessingReadersProvider _readersProvider;
  private object _pluginObject;

  private IBaseProfileProcessingWrapper _profileProcessingWrapper
  {
    get
    {
      return BaseProfileProcessingWrapper.GetBaseProfileProcessingWrapper(this._pluginObject, this._readersProvider);
    }
  }

  private IHostedFromProcessingWrapper _hfProcessor
  {
    get
    {
      return HostedFromProcessingWrapper.GetHostedFormProcessingWrapper(this._pluginObject, this._readersProvider);
    }
  }

  private IExtendedProfileProcessingWrapper _extendedProfileProcessor
  {
    get
    {
      return ExtendedProfileProcessingWrapper.GetExtendedProfileProcessingWrapper(this._pluginObject, this._readersProvider);
    }
  }

  protected CCCustomerInformationManager(ProcessingCardsPluginFactory pluginFactory)
  {
    this._pluginObject = pluginFactory.GetPlugin();
  }

  protected virtual string CreateCustomerProfile()
  {
    return this._profileProcessingWrapper.CreateCustomerProfile();
  }

  protected virtual string CreatePaymentProfile()
  {
    return this._profileProcessingWrapper.CreatePaymentProfile();
  }

  protected virtual CreditCardData GetPaymentProfile()
  {
    return this._profileProcessingWrapper.GetPaymentProfile();
  }

  protected virtual CustomerData GetCustomerProfile(string ccProcessingCenterID)
  {
    return this._profileProcessingWrapper.GetCustomerProfile(ccProcessingCenterID);
  }

  protected virtual void DeletePaymentProfile()
  {
    this._profileProcessingWrapper.DeletePaymentProfile();
  }

  protected virtual IEnumerable<CreditCardData> GetAllPaymentProfiles()
  {
    return this._extendedProfileProcessor.GetAllPaymentProfiles();
  }

  protected virtual void GetCreatePaymentProfileForm() => this._hfProcessor.GetCreateForm();

  protected virtual void PrepareProfileForm() => this._hfProcessor.PrepareProfileForm();

  protected virtual IEnumerable<CreditCardData> GetMissingPaymentProfiles()
  {
    return this._hfProcessor.GetMissingPaymentProfiles();
  }

  protected virtual void GetManagePaymentProfileForm() => this._hfProcessor.GetManageForm();

  protected virtual ProfileFormResponseProcessResult ProcessProfileFormResponse(string response)
  {
    return this._hfProcessor.ProcessProfileFormResponse(response);
  }

  protected virtual TranProfile GetOrCreateCustomerProfileFromTransaction(
    string tranId,
    CreateTranPaymentProfileParams cParams)
  {
    return this._extendedProfileProcessor.GetOrCreatePaymentProfileFromTransaction(tranId, cParams);
  }

  protected void SetReadersProvider(ICardProcessingReadersProvider readerProvider)
  {
    this._readersProvider = readerProvider;
  }

  public static void GetCreatePaymentProfileForm(
    PXGraph graph,
    ICCPaymentProfileAdapter paymentProfileAdapter)
  {
    if (graph == null || paymentProfileAdapter == null)
      return;
    ICCPaymentProfile current = paymentProfileAdapter.Current;
    PXCache cache = paymentProfileAdapter.Cache;
    ProcessingCardsPluginFactory cardsPluginFactory = CCCustomerInformationManager.GetProcessingCardsPluginFactory(current.CCProcessingCenterID);
    CCCustomerInformationManager informationManager = CCCustomerInformationManager.GetCustomerInformationManager(cardsPluginFactory);
    CCProcessingContext context = new CCProcessingContext()
    {
      processingCenter = cardsPluginFactory.GetProcessingCenter(),
      aCustomerID = current.BAccountID,
      aPMInstanceID = current.PMInstanceID,
      PaymentMethodID = current.PaymentMethodID,
      callerGraph = graph
    };
    CardProcessingReadersProvider readerProvider = new CardProcessingReadersProvider(context);
    informationManager.SetReadersProvider((ICardProcessingReadersProvider) readerProvider);
    string customerCcpid = current.CustomerCCPID;
    if (customerCcpid == null)
    {
      string customerProfile = informationManager.CreateCustomerProfile();
      ICCPaymentProfile copy = cache.CreateCopy((object) current) as ICCPaymentProfile;
      copy.CustomerCCPID = customerProfile;
      cache.Update((object) copy);
    }
    else
    {
      CustomerData customerProfile = informationManager.GetCustomerProfile(customerCcpid);
      if (!string.IsNullOrEmpty(customerProfile?.CustomerCD))
      {
        PX.Objects.AR.Customer customer1 = CCProcessingHelper.GetCustomer(graph, current.BAccountID);
        string customerID = CCProcessingHelper.DeleteCustomerPrefix(customerProfile.CustomerCD.Trim());
        if (!customer1.AcctCD.Trim().Equals(customerID))
        {
          string acctCD = CCProcessingHelper.DeleteCustomerPrefix(customerID);
          PX.Objects.AR.Customer customer2 = CCProcessingHelper.GetCustomer(graph, acctCD);
          if (customer2 != null)
            throw new PXException("The {0} customer profile belongs to the {1} customer.", new object[2]
            {
              (object) customerCcpid,
              (object) customer2.AcctCD
            });
        }
      }
    }
    CCProcessingCenter processingCenter = cardsPluginFactory.GetProcessingCenter();
    if (processingCenter.CreateAdditionalCustomerProfiles.GetValueOrDefault())
    {
      int customerProfileCount = CCProcessingHelper.CustomerProfileCountPerCustomer(graph, current.BAccountID, current.CCProcessingCenterID);
      int? creditCardLimit = processingCenter.CreditCardLimit;
      if (creditCardLimit.HasValue)
      {
        int? nullable = creditCardLimit;
        int num = 0;
        if (nullable.GetValueOrDefault() > num & nullable.HasValue && CCProcessingHelper.IsCreditCardCountEnough(informationManager.GetAllPaymentProfiles().Count<CreditCardData>(), creditCardLimit.Value))
        {
          context.PrefixForCustomerCD = CCProcessingHelper.BuildPrefixForCustomerCD(customerProfileCount, processingCenter);
          string customerProfile = informationManager.CreateCustomerProfile();
          ICCPaymentProfile copy = cache.CreateCopy((object) current) as ICCPaymentProfile;
          copy.CustomerCCPID = customerProfile;
          cache.Update((object) copy);
        }
      }
    }
    if (CCProcessingFeatureHelper.IsFeatureSupported(processingCenter, CCProcessingFeature.ProfileForm))
      informationManager.PrepareProfileForm();
    else
      informationManager.GetCreatePaymentProfileForm();
  }

  public static string CreateCustomerProfile(PXGraph graph, int? baccountID, string procCenterID)
  {
    ProcessingCardsPluginFactory cardsPluginFactory = CCCustomerInformationManager.GetProcessingCardsPluginFactory(procCenterID);
    CCCustomerInformationManager informationManager = CCCustomerInformationManager.GetCustomerInformationManager(cardsPluginFactory);
    informationManager.SetReadersProvider((ICardProcessingReadersProvider) new CardProcessingReadersProvider(new CCProcessingContext()
    {
      processingCenter = cardsPluginFactory.GetProcessingCenter(),
      aCustomerID = baccountID,
      callerGraph = graph
    }));
    return informationManager.CreateCustomerProfile();
  }

  public static PXResultset<CustomerPaymentMethodDetail> GetAllCustomersCardsInProcCenter(
    PXGraph graph,
    int? BAccountID,
    string CCProcessingCenterID)
  {
    return PXSelectBase<CustomerPaymentMethodDetail, PXSelectJoin<CustomerPaymentMethodDetail, InnerJoin<PaymentMethodDetail, On<CustomerPaymentMethodDetail.paymentMethodID, Equal<PaymentMethodDetail.paymentMethodID>, And<CustomerPaymentMethodDetail.detailID, Equal<PaymentMethodDetail.detailID>>>, InnerJoin<PX.Objects.AR.CustomerPaymentMethod, On<CustomerPaymentMethodDetail.pMInstanceID, Equal<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.bAccountID>>, And<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID>>, And<PaymentMethodDetail.isCCProcessingID, Equal<True>>>>>.Config>.Select(graph, new object[2]
    {
      (object) BAccountID,
      (object) CCProcessingCenterID
    });
  }

  public static void GetNewPaymentProfiles(
    PXGraph graph,
    ICCPaymentProfileAdapter payment,
    ICCPaymentProfileDetailAdapter paymentDetail)
  {
    if (graph == null || payment == null || paymentDetail == null)
      return;
    ICCPaymentProfile current = payment.Current;
    ProcessingCardsPluginFactory cardsPluginFactory = CCCustomerInformationManager.GetProcessingCardsPluginFactory(current.CCProcessingCenterID);
    CCCustomerInformationManager informationManager = CCCustomerInformationManager.GetCustomerInformationManager(cardsPluginFactory);
    CardProcessingReadersProvider readerProvider = new CardProcessingReadersProvider(new CCProcessingContext()
    {
      processingCenter = cardsPluginFactory.GetProcessingCenter(),
      aCustomerID = current.BAccountID,
      aPMInstanceID = current.PMInstanceID,
      callerGraph = graph
    });
    informationManager.SetReadersProvider((ICardProcessingReadersProvider) readerProvider);
    int num1 = 1;
    CreditCardData newCard = (CreditCardData) null;
    CCProcessingCenter processingCenter = cardsPluginFactory.GetProcessingCenter();
    while (true)
    {
      int num2 = num1;
      int? nullable = processingCenter.SyncRetryAttemptsNo;
      int num3 = nullable.GetValueOrDefault() + 1;
      if (num2 <= num3 && newCard == null)
      {
        nullable = processingCenter.SyncRetryDelayMs;
        Thread.Sleep(nullable.GetValueOrDefault());
        List<CreditCardData> list;
        try
        {
          list = informationManager.GetMissingPaymentProfiles().ToList<CreditCardData>();
        }
        catch (Exception ex)
        {
          throw new PXException(ex.Message + ". Credit card data cannot be synchronized. Please process the synchronization manually.");
        }
        if (list != null && list.Count > 1)
        {
          list.Sort((IComparer<CreditCardData>) new InterfaceExtensions.CreditCardDataComparer());
          newCard = list[0];
        }
        else if (list != null && list.Count == 1)
          newCard = list[0];
        if (newCard != null)
          CCCustomerInformationManager.SetPaymentDetailsFromCreditCardData(graph.GetService<ICCDisplayMaskService>(), newCard, payment, paymentDetail, informationManager);
        ++num1;
      }
      else
        break;
    }
    if (newCard == null)
      throw new PXException("Credit card data cannot be synchronized. Please process the synchronization manually.");
  }

  private static void SetPaymentDetailsFromCreditCardData(
    ICCDisplayMaskService service,
    CreditCardData newCard,
    ICCPaymentProfileAdapter payment,
    ICCPaymentProfileDetailAdapter paymentDetail,
    CCCustomerInformationManager cim)
  {
    ICCPaymentProfile current = payment.Current;
    CCCustomerInformationManager.SetCardTypeValue(newCard, payment, current);
    foreach (Tuple<ICCPaymentProfileDetail, ICCPaymentMethodDetail> tuple in paymentDetail.Select())
    {
      ICCPaymentProfileDetail paymentProfileDetail = tuple.Item1;
      ICCPaymentMethodDetail paymentMethodDetail = tuple.Item2;
      bool? nullable = paymentMethodDetail.IsCCProcessingID;
      if (nullable.GetValueOrDefault())
      {
        paymentProfileDetail.Value = newCard.PaymentProfileID;
        paymentDetail.Cache.Update((object) paymentProfileDetail);
      }
      else
      {
        nullable = paymentMethodDetail.IsIdentifier;
        if (nullable.GetValueOrDefault())
        {
          paymentProfileDetail.Value = newCard.CardNumber;
          paymentDetail.Cache.Update((object) paymentProfileDetail);
        }
      }
    }
    if (string.IsNullOrEmpty(current.Descr))
    {
      string displayCardTypeName = CCCustomerInformationManager.GetDisplayCardTypeName(newCard);
      string str = service.UseDefaultMaskForCardNumber(newCard.CardNumber, displayCardTypeName, newCard.PaymentMethodType);
      payment.Cache.SetValueExt<PX.Objects.AR.CustomerPaymentMethod.descr>((object) current, (object) str);
    }
    newCard = cim.GetPaymentProfile();
    if (!newCard.CardExpirationDate.HasValue)
      return;
    payment.Cache.SetValueExt<PX.Objects.AR.CustomerPaymentMethod.expirationDate>((object) current, (object) newCard.CardExpirationDate);
    payment.Cache.Update((object) current);
  }

  private static void SetCardTypeValue(
    CreditCardData newCard,
    ICCPaymentProfileAdapter payment,
    ICCPaymentProfile cpm)
  {
    string cardTypeCode = CCCustomerInformationManager.GetCardTypeCode(newCard);
    payment.Cache.SetValueExt<PX.Objects.AR.CustomerPaymentMethod.cardType>((object) cpm, (object) cardTypeCode);
    payment.Cache.SetValueExt<PX.Objects.AR.CustomerPaymentMethod.procCenterCardTypeCode>((object) cpm, cardTypeCode == "OTH" ? (object) newCard.CardType : (object) (string) null);
    payment.Cache.Update((object) cpm);
  }

  private static string GetDisplayCardTypeName(CreditCardData newCard)
  {
    return CardType.GetDisplayName(V2Converter.ConvertCardType(newCard.CardTypeCode));
  }

  private static string GetCardTypeCode(CreditCardData newCard)
  {
    return CardType.GetCardTypeCode(V2Converter.ConvertCardType(newCard.CardTypeCode));
  }

  public static void GetManagePaymentProfileForm(PXGraph graph, ICCPaymentProfile paymentProfile)
  {
    if (graph == null || paymentProfile == null)
      return;
    CustomerPaymentMethodDetail paymentMethodDetail = PXResultset<CustomerPaymentMethodDetail>.op_Implicit(PXSelectBase<CustomerPaymentMethodDetail, PXSelectJoin<CustomerPaymentMethodDetail, InnerJoin<PaymentMethodDetail, On<CustomerPaymentMethodDetail.paymentMethodID, Equal<PaymentMethodDetail.paymentMethodID>, And<CustomerPaymentMethodDetail.detailID, Equal<PaymentMethodDetail.detailID>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>>>, Where<PaymentMethodDetail.isCCProcessingID, Equal<True>, And<CustomerPaymentMethodDetail.pMInstanceID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[1]
    {
      (object) paymentProfile.PMInstanceID
    }));
    if (paymentMethodDetail == null || string.IsNullOrEmpty(paymentMethodDetail.Value))
      return;
    ProcessingCardsPluginFactory cardsPluginFactory = CCCustomerInformationManager.GetProcessingCardsPluginFactory(paymentProfile.CCProcessingCenterID);
    CCCustomerInformationManager informationManager = CCCustomerInformationManager.GetCustomerInformationManager(cardsPluginFactory);
    cardsPluginFactory.GetProcessingCenter();
    informationManager.SetReadersProvider((ICardProcessingReadersProvider) new CardProcessingReadersProvider(new CCProcessingContext()
    {
      processingCenter = cardsPluginFactory.GetProcessingCenter(),
      aCustomerID = paymentProfile.BAccountID,
      aPMInstanceID = paymentProfile.PMInstanceID,
      callerGraph = graph
    }));
    informationManager.GetManagePaymentProfileForm();
  }

  public static void ProcessProfileFormResponse(
    PXGraph graph,
    ICCPaymentProfileAdapter payment,
    ICCPaymentProfileDetailAdapter paymentDetail,
    string response)
  {
    ICCPaymentProfile current = payment.Current;
    ProcessingCardsPluginFactory cardsPluginFactory = CCCustomerInformationManager.GetProcessingCardsPluginFactory(current.CCProcessingCenterID);
    CCCustomerInformationManager informationManager = CCCustomerInformationManager.GetCustomerInformationManager(cardsPluginFactory);
    CardProcessingReadersProvider readerProvider = new CardProcessingReadersProvider(new CCProcessingContext()
    {
      processingCenter = cardsPluginFactory.GetProcessingCenter(),
      aCustomerID = current.BAccountID,
      aPMInstanceID = current.PMInstanceID,
      callerGraph = graph
    });
    informationManager.SetReadersProvider((ICardProcessingReadersProvider) readerProvider);
    CreditCardData cardData = informationManager.ProcessProfileFormResponse(response)?.CardData;
    if (cardData == null)
      return;
    CCCustomerInformationManager.SetPaymentDetailsFromCreditCardData(graph.GetService<ICCDisplayMaskService>(), cardData, payment, paymentDetail, informationManager);
  }

  public static void GetOrCreatePaymentProfile(
    PXGraph graph,
    ICCPaymentProfileAdapter payment,
    ICCPaymentProfileDetailAdapter paymentDetail)
  {
    ICCPaymentProfile current = payment.Current;
    bool flag1 = CCProcessingHelper.IsHFPaymentMethod(graph, payment.Current.PMInstanceID, false);
    bool flag2 = false;
    bool? nullable1;
    if (current is PX.Objects.AR.CustomerPaymentMethod)
    {
      nullable1 = ((PX.Objects.AR.CustomerPaymentMethod) current).Selected;
      flag2 = nullable1.GetValueOrDefault();
    }
    bool flag3 = flag1 && !flag2;
    ICCPaymentProfileDetail paymentProfileDetail1 = (ICCPaymentProfileDetail) null;
    bool flag4 = false;
    bool flag5 = false;
    foreach (Tuple<ICCPaymentProfileDetail, ICCPaymentMethodDetail> tuple in paymentDetail.Select())
    {
      ICCPaymentProfileDetail paymentProfileDetail2 = tuple.Item1;
      nullable1 = tuple.Item2.IsCCProcessingID;
      if (nullable1.GetValueOrDefault())
      {
        flag4 = paymentProfileDetail2.Value != null;
        paymentProfileDetail1 = paymentProfileDetail2;
      }
      else
        flag5 = paymentProfileDetail2.Value != null | flag5;
    }
    if (paymentProfileDetail1 == null)
      throw new PXException("No Payment Profile ID in detials for payment method {0}!", new object[1]
      {
        (object) payment.Current.Descr
      });
    if (flag4 & flag5)
      return;
    bool flag6 = flag4 && !flag5;
    if (((!(flag4 | flag5) ? 0 : (!flag3 ? 1 : 0)) | (flag6 ? 1 : 0)) == 0)
      return;
    ICCPaymentProfile currCpm = payment.Current;
    ProcessingCardsPluginFactory cardsPluginFactory = CCCustomerInformationManager.GetProcessingCardsPluginFactory(currCpm.CCProcessingCenterID);
    CCCustomerInformationManager informationManager = CCCustomerInformationManager.GetCustomerInformationManager(cardsPluginFactory);
    CCProcessingCenter processingCenter = cardsPluginFactory.GetProcessingCenter();
    CCProcessingContext context = new CCProcessingContext()
    {
      processingCenter = cardsPluginFactory.GetProcessingCenter(),
      aCustomerID = currCpm.BAccountID,
      aPMInstanceID = currCpm.PMInstanceID,
      callerGraph = graph,
      expirationDateConverter = (String2DateConverterFunc) (s => CustomerPaymentMethodMaint.ParseExpiryDate(graph, currCpm, s))
    };
    CardProcessingReadersProvider readerProvider = new CardProcessingReadersProvider(context);
    informationManager.SetReadersProvider((ICardProcessingReadersProvider) readerProvider);
    string ccProcessingCenterID = currCpm.CustomerCCPID;
    if (currCpm.CustomerCCPID == null)
    {
      ccProcessingCenterID = informationManager.CreateCustomerProfile();
      current.CustomerCCPID = ccProcessingCenterID;
      payment.Cache.Update((object) current);
    }
    if (processingCenter.CreateAdditionalCustomerProfiles.GetValueOrDefault() && !flag6)
    {
      int customerProfileCount = CCProcessingHelper.CustomerProfileCountPerCustomer(graph, currCpm.BAccountID, currCpm.CCProcessingCenterID);
      int? creditCardLimit = processingCenter.CreditCardLimit;
      if (creditCardLimit.HasValue)
      {
        int? nullable2 = creditCardLimit;
        int num = 0;
        if (nullable2.GetValueOrDefault() > num & nullable2.HasValue && CCProcessingHelper.IsCreditCardCountEnough(informationManager.GetAllPaymentProfiles().Count<CreditCardData>(), creditCardLimit.Value))
        {
          context.PrefixForCustomerCD = CCProcessingHelper.BuildPrefixForCustomerCD(customerProfileCount, processingCenter);
          ccProcessingCenterID = informationManager.CreateCustomerProfile();
          current.CustomerCCPID = ccProcessingCenterID;
          payment.Cache.Update((object) current);
        }
      }
    }
    if (flag5)
    {
      string paymentProfile = informationManager.CreatePaymentProfile();
      paymentProfileDetail1.Value = paymentProfile;
      paymentProfileDetail1 = paymentDetail.Cache.Update((object) paymentProfileDetail1) as ICCPaymentProfileDetail;
    }
    CreditCardData paymentProfile1 = informationManager.GetPaymentProfile();
    if (paymentProfile1 != null && !string.IsNullOrEmpty(paymentProfile1.PaymentProfileID))
    {
      CustomerData customerProfile = informationManager.GetCustomerProfile(ccProcessingCenterID);
      if (!string.IsNullOrEmpty(customerProfile?.CustomerCD))
      {
        PX.Objects.AR.Customer customer1 = CCProcessingHelper.GetCustomer(graph, currCpm.BAccountID);
        string customerID = CCProcessingHelper.DeleteCustomerPrefix(customerProfile.CustomerCD.Trim());
        if (customer1 == null || !customer1.AcctCD.Trim().Equals(customerID))
        {
          string acctCD = CCProcessingHelper.DeleteCustomerPrefix(customerID);
          PX.Objects.AR.Customer customer2 = CCProcessingHelper.GetCustomer(graph, acctCD);
          if (customer2 != null)
            throw new PXException("The {0} customer profile belongs to the {1} customer.", new object[2]
            {
              (object) ccProcessingCenterID,
              (object) customer2.AcctCD
            });
        }
      }
      CCCustomerInformationManager.SetCardTypeValue(paymentProfile1, payment, current);
      foreach (Tuple<ICCPaymentProfileDetail, ICCPaymentMethodDetail> tuple in paymentDetail.Select())
      {
        ICCPaymentProfileDetail paymentProfileDetail3 = tuple.Item1;
        ICCPaymentMethodDetail paymentMethodDetail = tuple.Item2;
        if (!(paymentProfileDetail3.DetailID == paymentProfileDetail1.DetailID))
        {
          string str = (string) null;
          if (!paymentMethodDetail.IsCCProcessingID.GetValueOrDefault() && paymentMethodDetail.IsIdentifier.GetValueOrDefault() && !string.IsNullOrEmpty(paymentProfile1.CardNumber))
            str = paymentProfile1.CardNumber;
          paymentProfileDetail3.Value = str;
          paymentDetail.Cache.Update((object) paymentProfileDetail3);
        }
      }
      if (string.IsNullOrEmpty(current.Descr) || current.Descr == current?.PaymentMethodID)
      {
        string displayCardTypeName = CCCustomerInformationManager.GetDisplayCardTypeName(paymentProfile1);
        string str = graph.GetService<ICCDisplayMaskService>().UseDefaultMaskForCardNumber(paymentProfile1.CardNumber, displayCardTypeName, paymentProfile1.PaymentMethodType);
        payment.Cache.SetValueExt<PX.Objects.AR.CustomerPaymentMethod.descr>((object) current, (object) str);
      }
      if (!paymentProfile1.CardExpirationDate.HasValue)
        return;
      payment.Cache.SetValueExt((object) current, "ExpirationDate", (object) paymentProfile1.CardExpirationDate);
      payment.Cache.Update((object) current);
    }
    else
      throw new PXException("Couldn't get details from processing center for payment method instance {0}", new object[1]
      {
        (object) payment.Current.Descr
      });
  }

  public static void GetPaymentProfile(
    PXGraph graph,
    ICCPaymentProfileAdapter payment,
    ICCPaymentProfileDetailAdapter paymentDetail)
  {
    string str1 = (string) null;
    foreach (Tuple<ICCPaymentProfileDetail, ICCPaymentMethodDetail> tuple in paymentDetail.Select())
    {
      ICCPaymentProfileDetail paymentProfileDetail = tuple.Item1;
      if (tuple.Item2.IsCCProcessingID.GetValueOrDefault())
      {
        str1 = paymentProfileDetail.Value;
        break;
      }
    }
    if (string.IsNullOrEmpty(str1))
      throw new PXException("Card token ID cannot be found.");
    ICCPaymentProfile current = payment.Current;
    string processingCenterId = current.CCProcessingCenterID;
    int? baccountId = current.BAccountID;
    int? pmInstanceId = current.PMInstanceID;
    ProcessingCardsPluginFactory cardsPluginFactory = CCCustomerInformationManager.GetProcessingCardsPluginFactory(processingCenterId);
    CCCustomerInformationManager informationManager = CCCustomerInformationManager.GetCustomerInformationManager(cardsPluginFactory);
    informationManager.SetReadersProvider((ICardProcessingReadersProvider) new CardProcessingReadersProvider(new CCProcessingContext()
    {
      processingCenter = cardsPluginFactory.GetProcessingCenter(),
      aCustomerID = baccountId,
      aPMInstanceID = pmInstanceId,
      callerGraph = graph
    }));
    CreditCardData paymentProfile = informationManager.GetPaymentProfile();
    if (paymentProfile == null)
      throw new PXException("No card with token ID {0} is found in the payment processing center {1}.", new object[2]
      {
        (object) str1,
        (object) current.CCProcessingCenterID
      });
    CCCustomerInformationManager.SetCardTypeValue(paymentProfile, payment, current);
    foreach (Tuple<ICCPaymentProfileDetail, ICCPaymentMethodDetail> tuple in paymentDetail.Select())
    {
      ICCPaymentProfileDetail paymentProfileDetail = tuple.Item1;
      ICCPaymentMethodDetail paymentMethodDetail = tuple.Item2;
      if (!paymentMethodDetail.IsCCProcessingID.GetValueOrDefault() && paymentMethodDetail.IsIdentifier.GetValueOrDefault() && !string.IsNullOrEmpty(paymentProfile.CardNumber))
      {
        paymentProfileDetail.Value = paymentProfile.CardNumber;
        paymentDetail.Cache.Update((object) paymentProfileDetail);
      }
    }
    if (string.IsNullOrEmpty(current.Descr))
    {
      string displayCardTypeName = CCCustomerInformationManager.GetDisplayCardTypeName(paymentProfile);
      string str2 = graph.GetService<ICCDisplayMaskService>().UseDefaultMaskForCardNumber(paymentProfile.CardNumber, displayCardTypeName, paymentProfile.PaymentMethodType);
      payment.Cache.SetValueExt<PX.Objects.AR.CustomerPaymentMethod.descr>((object) current, (object) str2);
    }
    if (!paymentProfile.CardExpirationDate.HasValue)
      return;
    payment.Cache.SetValueExt((object) current, "ExpirationDate", (object) paymentProfile.CardExpirationDate);
    payment.Cache.Update((object) current);
  }

  public static void DeletePaymentProfile(
    PXGraph graph,
    ICCPaymentProfileAdapter payment,
    ICCPaymentProfileDetailAdapter paymentDetail)
  {
    IEnumerator enumerator = payment.Cache.Deleted.GetEnumerator();
    if (!enumerator.MoveNext())
      return;
    ICCPaymentProfile current = (ICCPaymentProfile) enumerator.Current;
    ProcessingCardsPluginFactory cardsPluginFactory = CCCustomerInformationManager.GetProcessingCardsPluginFactory(current.CCProcessingCenterID);
    CCProcessingCenter processingCenter = cardsPluginFactory.GetProcessingCenter();
    if (string.IsNullOrEmpty(current.CCProcessingCenterID) || !processingCenter.SyncronizeDeletion.GetValueOrDefault())
      return;
    CCCustomerInformationManager informationManager = CCCustomerInformationManager.GetCustomerInformationManager(cardsPluginFactory);
    CardProcessingReadersProvider readerProvider = new CardProcessingReadersProvider(new CCProcessingContext()
    {
      processingCenter = cardsPluginFactory.GetProcessingCenter(),
      aCustomerID = current.BAccountID,
      aPMInstanceID = current.PMInstanceID,
      callerGraph = cardsPluginFactory.GetPaymentProcessingRepository().Graph
    });
    informationManager.SetReadersProvider((ICardProcessingReadersProvider) readerProvider);
    ICCPaymentProfileDetail paymentProfileDetail1 = (ICCPaymentProfileDetail) null;
    PaymentMethodDetail paymentMethodDetail = PXResultset<PaymentMethodDetail>.op_Implicit(PXSelectBase<PaymentMethodDetail, PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Optional<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>, And<PaymentMethodDetail.isCCProcessingID, Equal<True>>>>>.Config>.Select(graph, new object[1]
    {
      (object) current.PaymentMethodID
    }));
    foreach (ICCPaymentProfileDetail paymentProfileDetail2 in paymentDetail.Cache.Deleted)
    {
      if (paymentProfileDetail2.DetailID == paymentMethodDetail.DetailID)
      {
        paymentProfileDetail1 = paymentProfileDetail2;
        break;
      }
    }
    if (paymentProfileDetail1 == null || string.IsNullOrEmpty(paymentProfileDetail1.Value))
      return;
    informationManager.DeletePaymentProfile();
  }

  public static CustomerData GetCustomerProfile(
    PXGraph graph,
    string ccProcessingCenterID,
    string customerId)
  {
    ProcessingCardsPluginFactory cardsPluginFactory = CCCustomerInformationManager.GetProcessingCardsPluginFactory(ccProcessingCenterID);
    CCProcessingCenter processingCenter = cardsPluginFactory.GetProcessingCenter();
    CCCustomerInformationManager informationManager = CCCustomerInformationManager.GetCustomerInformationManager(cardsPluginFactory);
    informationManager.SetReadersProvider((ICardProcessingReadersProvider) new CardProcessingReadersProvider(new CCProcessingContext()
    {
      processingCenter = processingCenter,
      callerGraph = graph
    }));
    return informationManager.GetCustomerProfile(customerId);
  }

  public static TranProfile GetOrCreatePaymentProfileByTran(
    PXGraph graph,
    ICCPaymentProfileAdapter adapter,
    string tranId)
  {
    ICCPaymentProfile current = adapter.Current;
    ProcessingCardsPluginFactory cardsPluginFactory = CCCustomerInformationManager.GetProcessingCardsPluginFactory(current.CCProcessingCenterID);
    CCProcessingCenter processingCenter = cardsPluginFactory.GetProcessingCenter();
    CCCustomerInformationManager informationManager = CCCustomerInformationManager.GetCustomerInformationManager(cardsPluginFactory);
    CardProcessingReadersProvider readerProvider = new CardProcessingReadersProvider(new CCProcessingContext()
    {
      processingCenter = processingCenter,
      aCustomerID = current.BAccountID,
      aPMInstanceID = current.PMInstanceID,
      callerGraph = graph
    });
    informationManager.SetReadersProvider((ICardProcessingReadersProvider) readerProvider);
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) new PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>(graph)).Select(new object[1]
    {
      (object) current.BAccountID
    }));
    if (current.CustomerCCPID == null)
    {
      CreateTranPaymentProfileParams cParams = new CreateTranPaymentProfileParams()
      {
        LocalCustomerId = customer.AcctCD.Trim(),
        Description = customer.AcctName
      };
      return informationManager.GetOrCreateCustomerProfileFromTransaction(tranId, cParams);
    }
    int? creditCardLimit = processingCenter.CreditCardLimit;
    int customerProfileCount = CCProcessingHelper.CustomerProfileCountPerCustomer(graph, current.BAccountID, current.CCProcessingCenterID);
    string str = customerProfileCount == 0 ? string.Empty : CCProcessingHelper.BuildPrefixForCustomerCD(customerProfileCount, processingCenter);
    CreateTranPaymentProfileParams cParams1;
    if (processingCenter.CreateAdditionalCustomerProfiles.GetValueOrDefault() && creditCardLimit.HasValue)
    {
      int? nullable = creditCardLimit;
      int num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
      {
        bool flag;
        try
        {
          flag = CCProcessingHelper.IsCreditCardCountEnough(informationManager.GetAllPaymentProfiles().Count<CreditCardData>(), creditCardLimit.Value);
        }
        catch (PXException ex) when (((Exception) ex).InnerException?.GetType() == typeof (CCProcessingException))
        {
          flag = true;
        }
        if (flag)
        {
          cParams1 = new CreateTranPaymentProfileParams()
          {
            LocalCustomerId = str + customer.AcctCD.Trim(),
            Description = customer.AcctName
          };
          goto label_11;
        }
        cParams1 = new CreateTranPaymentProfileParams()
        {
          PCCustomerId = current.CustomerCCPID,
          LocalCustomerId = str + customer.AcctCD.Trim(),
          Description = customer.AcctName
        };
        goto label_11;
      }
    }
    cParams1 = new CreateTranPaymentProfileParams()
    {
      PCCustomerId = current.CustomerCCPID,
      LocalCustomerId = str + customer.AcctCD.Trim(),
      Description = customer.AcctName
    };
label_11:
    return informationManager.GetOrCreateCustomerProfileFromTransaction(tranId, cParams1);
  }

  private static ProcessingCardsPluginFactory GetProcessingCardsPluginFactory(
    string processingCenterId)
  {
    return new ProcessingCardsPluginFactory(processingCenterId);
  }

  private static CCCustomerInformationManager GetCustomerInformationManager(
    ProcessingCardsPluginFactory pluginFactory)
  {
    return new CCCustomerInformationManager(pluginFactory);
  }
}
