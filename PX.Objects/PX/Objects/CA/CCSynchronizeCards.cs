// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CCSynchronizeCards
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing.CardsSynchronization;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.CCPaymentProcessing.Wrappers;
using PX.Objects.AR.Repositories;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Objects.CA;

public class CCSynchronizeCards : PXGraph<CCSynchronizeCards>
{
  public PXCancel<CCSynchronizeCards.CreditCardsFilter> Cancel;
  public PXSave<CCSynchronizeCards.CreditCardsFilter> Save;
  private CustomerRepository customerRepo;
  public PXAction<CCSynchronizeCards.CreditCardsFilter> LoadCards;
  public PXAction<CCSynchronizeCards.CreditCardsFilter> SetDefaultPaymentMethod;
  public PXAction<CCSynchronizeCards.CreditCardsFilter> ViewCustomer;
  public PXMenuAction<CCSynchronizeCards.CreditCardsFilter> GroupAction;
  public PXFilter<CCSynchronizeCards.CreditCardsFilter> Filter;
  public PXFilter<CCSynchronizeCards.CreditCardsFilter> PMFilter;
  public PXSelect<CustomerPaymentMethodDetail, Where<True, Equal<False>>> DummyCPMD;
  public PXSelectJoin<CCSynchronizeCard, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<CCSynchronizeCard.bAccountID>>>, Where<CCSynchronizeCard.cCProcessingCenterID, Equal<Required<CCSynchronizeCard.cCProcessingCenterID>>, And<CCSynchronizeCard.imported, Equal<False>>>, OrderBy<Asc<CCSynchronizeCard.customerCCPID>>> SynchronizeCardPaymentData;
  [PXFilterable(new Type[] {})]
  public CCSyncFilteredProcessing<CCSynchronizeCard, CCSynchronizeCards.CreditCardsFilter, Where<CCSynchronizeCard.cCProcessingCenterID, Equal<Current<CCSynchronizeCards.CreditCardsFilter.processingCenterId>>>, OrderBy<Asc<CCSynchronizeCard.customerCCPID>>> CustomerCardPaymentData;
  public PXSelect<CCSynchronizeCards.CustomerPaymentProfile> CustPaymentProfileForDialog;
  private List<CCSynchronizeCard> cacheRecordsSameCustomerCCPID;

  [InjectDependency]
  public ICCDisplayMaskService CCDisplayMaskService { get; set; }

  [PXProcessButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Load Card Data")]
  protected virtual IEnumerable loadCards(PXAdapter adapter)
  {
    ((PXSelectBase<CCSynchronizeCards.CreditCardsFilter>) this.Filter).Current.EnableCustomerPaymentDialog = new bool?(false);
    CCProcessingHelper.CheckHttpsConnection();
    if (!this.ValidateLoadCardsAction() && (((PXSelectBase<CCSynchronizeCard>) this.CustomerCardPaymentData).Select(Array.Empty<object>()).Count == 0 || !adapter.ExternalCall || ((PXSelectBase<CCSynchronizeCards.CreditCardsFilter>) this.Filter).Ask("Data will be reloaded. Continue loading?", (MessageButtons) 4) == 6))
    {
      PXResultset<CCSynchronizeCard> source = ((PXSelectBase<CCSynchronizeCard>) this.CustomerCardPaymentData).Select(Array.Empty<object>());
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      Expression<Func<PXResult<CCSynchronizeCard>, CCSynchronizeCard>> selector = Expression.Lambda<Func<PXResult<CCSynchronizeCard>, CCSynchronizeCard>>((Expression) Expression.Call(cc, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), parameterExpression);
      foreach (CCSynchronizeCard ccSynchronizeCard in (IEnumerable<CCSynchronizeCard>) ((IQueryable<PXResult<CCSynchronizeCard>>) source).Select<PXResult<CCSynchronizeCard>, CCSynchronizeCard>(selector))
        ((PXSelectBase<CCSynchronizeCard>) this.CustomerCardPaymentData).Delete(ccSynchronizeCard);
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CloadCards\u003Eb__8_0)));
    }
    return adapter.Get();
  }

  [PXButton(CommitChanges = true, Category = "Actions")]
  [PXUIField(DisplayName = "Set Payment Method")]
  protected virtual IEnumerable setDefaultPaymentMethod(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CCSynchronizeCards.\u003C\u003Ec__DisplayClass10_0 cDisplayClass100 = new CCSynchronizeCards.\u003C\u003Ec__DisplayClass10_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass100.filter = ((PXSelectBase<CCSynchronizeCards.CreditCardsFilter>) this.Filter).Current;
    // ISSUE: method pointer
    if (!adapter.ExternalCall || ((PXSelectBase<CCSynchronizeCards.CreditCardsFilter>) this.PMFilter).AskExt(new PXView.InitializePanel((object) cDisplayClass100, __methodptr(\u003CsetDefaultPaymentMethod\u003Eb__0))) == 1)
    {
      // ISSUE: reference to a compiler-generated field
      string eftPaymentMethodId = cDisplayClass100.filter.EftPaymentMethodId;
      // ISSUE: reference to a compiler-generated field
      string ccPaymentMethodId = cDisplayClass100.filter.CCPaymentMethodId;
      PXFilterRow[] externalFilters = ((PXSelectBase) this.CustomerCardPaymentData).View.GetExternalFilters();
      int num1 = 0;
      int num2 = 0;
      foreach (CCSynchronizeCard ccSynchronizeCard in GraphHelper.RowCast<CCSynchronizeCard>((IEnumerable) ((PXSelectBase) this.CustomerCardPaymentData).View.Select((object[]) null, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, externalFilters, ref num1, 0, ref num2)))
      {
        // ISSUE: reference to a compiler-generated field
        if (cDisplayClass100.filter.OverwriteEftPaymentMethod.GetValueOrDefault() || ccSynchronizeCard.PaymentMethodID == null)
        {
          if (ccSynchronizeCard.PaymentType == "EFT")
            ccSynchronizeCard.PaymentMethodID = eftPaymentMethodId;
          ((PXSelectBase<CCSynchronizeCard>) this.CustomerCardPaymentData).Update(ccSynchronizeCard);
        }
        // ISSUE: reference to a compiler-generated field
        if (cDisplayClass100.filter.OverwriteCCPaymentMethod.GetValueOrDefault() || ccSynchronizeCard.PaymentMethodID == null)
        {
          if (ccSynchronizeCard.PaymentType == "CCD")
            ccSynchronizeCard.PaymentMethodID = ccPaymentMethodId;
          ((PXSelectBase<CCSynchronizeCard>) this.CustomerCardPaymentData).Update(ccSynchronizeCard);
        }
      }
    }
    return adapter.Get();
  }

  [PXButton]
  protected virtual void viewCustomer()
  {
    CCSynchronizeCard current = ((PXSelectBase<CCSynchronizeCard>) this.CustomerCardPaymentData).Current;
    CustomerMaint instance = PXGraph.CreateInstance<CustomerMaint>();
    PXSelectBase<PX.Objects.AR.Customer> pxSelectBase = (PXSelectBase<PX.Objects.AR.Customer>) new PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>((PXGraph) this);
    ((PXSelectBase<PX.Objects.AR.Customer>) instance.CurrentCustomer).Current = pxSelectBase.SelectSingle(new object[1]
    {
      (object) current.BAccountID
    });
    if (((PXSelectBase<PX.Objects.AR.Customer>) instance.CurrentCustomer).Current != null)
      throw new PXRedirectRequiredException((PXGraph) instance, true, string.Empty);
  }

  public IEnumerable customerCardPaymentData()
  {
    CCSynchronizeCards.CreditCardsFilter current = ((PXSelectBase<CCSynchronizeCards.CreditCardsFilter>) this.Filter).Current;
    PXDelegateResult pxDelegateResult = new PXDelegateResult();
    if (current.ProcessingCenterId != null)
    {
      CCProcessingHelper.CheckHttpsConnection();
      PXResultset<CCSynchronizeCard> collection = ((PXSelectBase<CCSynchronizeCard>) this.SynchronizeCardPaymentData).Select(new object[1]
      {
        (object) current.ProcessingCenterId
      });
      ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) collection);
    }
    return (IEnumerable) pxDelegateResult;
  }

  public IEnumerable custPaymentProfileForDialog()
  {
    foreach (CCSynchronizeCards.CustomerPaymentProfile customerPaymentProfile in ((PXSelectBase) this.CustPaymentProfileForDialog).Cache.Cached)
      yield return (object) customerPaymentProfile;
  }

  public CCSynchronizeCards()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CCSynchronizeCards.\u003C\u003Ec__DisplayClass25_0 cDisplayClass250 = new CCSynchronizeCards.\u003C\u003Ec__DisplayClass25_0();
    this.customerRepo = new CustomerRepository((PXGraph) this);
    this.CustomerCardPaymentData.SetBeforeScheduleAddAction((Action) (() => ((PXSelectBase<CCSynchronizeCards.CreditCardsFilter>) this.Filter).Current.ScheduledServiceSync = new bool?(true)));
    this.CustomerCardPaymentData.SetAfterScheduleAddAction((Action) (() => ((PXSelectBase<CCSynchronizeCards.CreditCardsFilter>) this.Filter).Current.ScheduledServiceSync = new bool?(false)));
    this.CustomerCardPaymentData.SetBeforeScheduleProcessAllAction((Action) (() => ((PXSelectBase<CCSynchronizeCards.CreditCardsFilter>) this.Filter).Current.IsScheduleProcess = new bool?(true)));
    // ISSUE: reference to a compiler-generated field
    cDisplayClass250.filter = ((PXSelectBase<CCSynchronizeCards.CreditCardsFilter>) this.Filter).Current;
    // ISSUE: method pointer
    ((PXProcessingBase<CCSynchronizeCard>) this.CustomerCardPaymentData).SetProcessDelegate(new PXProcessingBase<CCSynchronizeCard>.ProcessListDelegate((object) cDisplayClass250, __methodptr(\u003C\u002Ector\u003Eb__3)));
    ((PXAction) this.GroupAction).AddMenuAction((PXAction) this.SetDefaultPaymentMethod);
  }

  private static void DoLoadCards(CCSynchronizeCards.CreditCardsFilter filter)
  {
    CCSynchronizeCards instance = PXGraph.CreateInstance<CCSynchronizeCards>();
    filter.EnableCustomerPaymentDialog = new bool?(false);
    ((PXSelectBase<CCSynchronizeCards.CreditCardsFilter>) instance.Filter).Current = filter;
    if (instance.ValidateLoadCardsAction() || instance.GetCardsAllProfiles() <= 0)
      return;
    foreach (CCSynchronizeCard ccSynchronizeCard in ((PXSelectBase) instance.CustomerCardPaymentData).Cache.Inserted)
    {
      if (ccSynchronizeCard.NoteID.HasValue)
        ProcessingInfo.AppendProcessingInfo(ccSynchronizeCard.NoteID.Value, "Loading of the credit card has been completed.");
    }
    try
    {
      ((PXGraph) instance).Persist();
    }
    catch
    {
      ProcessingInfo.ClearProcessingRows();
      throw;
    }
  }

  private static void DoImportCards(List<CCSynchronizeCard> items)
  {
    int cardIndex = 0;
    CCSynchronizeCard ccSynchronizeCard1 = items.First<CCSynchronizeCard>();
    if (ccSynchronizeCard1 != null)
    {
      string processingCenterId = ccSynchronizeCard1.CCProcessingCenterID;
    }
    CCSynchronizeCards instance = PXGraph.CreateInstance<CCSynchronizeCards>();
    foreach (CCSynchronizeCard ccSynchronizeCard2 in items)
    {
      if (instance.ValidateImportCard(ccSynchronizeCard2, cardIndex) && !instance.CheckCustomerPaymentProfileExists(ccSynchronizeCard2, cardIndex))
      {
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          instance.CreateCustomerPaymentMethodRecord(ccSynchronizeCard2);
          instance.UpdateCCProcessingSyncronizeCardRecord(ccSynchronizeCard2);
          transactionScope.Complete();
          PXProcessing<CCSynchronizeCard>.SetInfo(cardIndex, "Completed");
        }
      }
      ++cardIndex;
    }
  }

  public void CreditCardsFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs args)
  {
    if (!(args.Row is CCSynchronizeCards.CreditCardsFilter row))
      return;
    ((PXSelectBase) this.CustomerCardPaymentData).AllowInsert = false;
    ((PXSelectBase) this.CustomerCardPaymentData).AllowDelete = false;
    ((PXGraph) this).Actions["LoadCards"].SetIsLockedOnToolbar(true);
    PXButtonState state = (PXButtonState) ((PXGraph) this).Actions["Process"].GetState((object) ((PXSelectBase<CCSynchronizeCards.CreditCardsFilter>) this.Filter).Current);
    bool flag1 = ((PXFieldState) state).Visible && ((PXFieldState) state).Enabled;
    ((PXAction) this.SetDefaultPaymentMethod).SetEnabled(flag1);
    ((PXAction) this.LoadCards).SetEnabled(flag1);
    ((PXAction) this.Save).SetEnabled(!string.IsNullOrEmpty(row.ProcessingCenterId));
    PXUIFieldAttribute.SetVisible<PX.Objects.AR.CustomerPaymentMethod.customerCCPID>(((PXGraph) this).Caches[typeof (PX.Objects.AR.CustomerPaymentMethod)], (object) null, true);
    PXUIFieldAttribute.SetVisible<CCSynchronizeCards.CreditCardsFilter.scheduledServiceSync>(sender, (object) null, false);
    bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.acumaticaPayments>();
    PXUIFieldAttribute.SetVisible<CCSynchronizeCards.CreditCardsFilter.eftPaymentMethodId>(sender, (object) null, flag2);
    PXUIFieldAttribute.SetVisible<CCSynchronizeCards.CreditCardsFilter.overwriteEftPaymentMethod>(sender, (object) null, flag2);
    ((PXAction) this.LoadCards).SetCaption(flag2 ? "Load Card/Account Data" : "Load Card Data");
  }

  public virtual void CCSynchronizeCard_RowSelected(PXCache sender, PXRowSelectedEventArgs args)
  {
    PXUIFieldAttribute.SetEnabled<CCSynchronizeCard.bAccountID>(sender, args.Row);
    PXUIFieldAttribute.SetEnabled<CCSynchronizeCard.paymentMethodID>(sender, args.Row);
    PXUIFieldAttribute.SetEnabled<CCSynchronizeCard.cashAccountID>(sender, args.Row);
  }

  public virtual void CCSynchronizeCard_BAccountID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is CCSynchronizeCard row))
      return;
    CustomerProcessingCenterID processingCenterId = ((PXSelectBase<CustomerProcessingCenterID>) new PXSelect<CustomerProcessingCenterID, Where<CustomerProcessingCenterID.customerCCPID, Equal<Required<CustomerProcessingCenterID.customerCCPID>>>, OrderBy<Desc<CustomerProcessingCenterID.createdDateTime>>>((PXGraph) this)).SelectSingle(new object[1]
    {
      (object) row.CustomerCCPID
    });
    if (processingCenterId != null)
    {
      e.NewValue = (object) processingCenterId.BAccountID;
    }
    else
    {
      string pcCustomerId = row.PCCustomerID;
      if (pcCustomerId == null)
        return;
      PX.Objects.AR.Customer customer = ((PXSelectBase<PX.Objects.AR.Customer>) new PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.acctCD, Equal<Required<CCSynchronizeCard.pCCustomerID>>>>((PXGraph) this)).SelectSingle(new object[1]
      {
        (object) CCProcessingHelper.DeleteCustomerPrefix(pcCustomerId)
      });
      if (customer == null)
        return;
      e.NewValue = (object) customer.BAccountID;
    }
  }

  public virtual void CCSynchronizeCard_PaymentMethodId_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CCSynchronizeCard row) || !row.CashAccountID.HasValue || this.CheckCashAccountAvailability(row))
      return;
    cache.SetDefaultExt<CCSynchronizeCard.cashAccountID>((object) row);
  }

  public virtual void CCSynchronizeCard_BAccountID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CCSynchronizeCard row) || !e.ExternalCall || !row.BAccountID.HasValue)
      return;
    bool? customerPaymentDialog = ((PXSelectBase<CCSynchronizeCards.CreditCardsFilter>) this.Filter).Current.EnableCustomerPaymentDialog;
    if (!customerPaymentDialog.GetValueOrDefault())
    {
      ((PXSelectBase<CCSynchronizeCards.CreditCardsFilter>) this.Filter).Current.EnableCustomerPaymentDialog = new bool?(true);
      ((PXSelectBase<CCSynchronizeCards.CreditCardsFilter>) this.Filter).Current.CustomerName = this.GetCustomerNameByID(row.BAccountID);
      ((PXSelectBase) this.CustPaymentProfileForDialog).Cache.Clear();
      if (this.PopulatePaymentProfileForDialog(row.CustomerCCPID, row.PCCustomerID, row.BAccountID) > 0)
        ((PXSelectBase<CCSynchronizeCards.CustomerPaymentProfile>) this.CustPaymentProfileForDialog).AskExt();
    }
    customerPaymentDialog = ((PXSelectBase<CCSynchronizeCards.CreditCardsFilter>) this.Filter).Current.EnableCustomerPaymentDialog;
    if (!customerPaymentDialog.GetValueOrDefault())
      return;
    ((PXSelectBase<CCSynchronizeCards.CreditCardsFilter>) this.Filter).Current.EnableCustomerPaymentDialog = new bool?(false);
  }

  public virtual void CustomerPaymentProfile_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is CCSynchronizeCards.CustomerPaymentProfile row) || !row.Selected.GetValueOrDefault())
      return;
    foreach (CCSynchronizeCard ccSynchronizeCard in this.GetRecordsWithSameCustomerCCPID(row.PCCustomerID))
    {
      int? recordId = ccSynchronizeCard.RecordID;
      int? nullable = row.RecordID;
      if (recordId.GetValueOrDefault() == nullable.GetValueOrDefault() & recordId.HasValue == nullable.HasValue)
      {
        nullable = ccSynchronizeCard.BAccountID;
        if (!nullable.HasValue)
        {
          ccSynchronizeCard.BAccountID = row.BAccountID;
          ((PXSelectBase<CCSynchronizeCard>) this.CustomerCardPaymentData).Update(ccSynchronizeCard);
          ((PXSelectBase) this.CustomerCardPaymentData).View.RequestRefresh();
        }
      }
    }
  }

  private int PopulatePaymentProfileForDialog(
    string customerCCPID,
    string customerID,
    int? bAccountID)
  {
    int num = 0;
    customerID = CCProcessingHelper.DeleteCustomerPrefix(customerID);
    foreach (PXResult<CCSynchronizeCard> pxResult in ((PXSelectBase<CCSynchronizeCard>) this.CustomerCardPaymentData).Select(Array.Empty<object>()))
    {
      CCSynchronizeCard syncCard = PXResult<CCSynchronizeCard>.op_Implicit(pxResult);
      string str = CCProcessingHelper.DeleteCustomerPrefix(syncCard.PCCustomerID);
      if (!syncCard.BAccountID.HasValue && (customerCCPID == syncCard.CustomerCCPID || !string.IsNullOrEmpty(customerID) && str == customerID))
      {
        CCSynchronizeCards.CustomerPaymentProfile fromSyncCard = CCSynchronizeCards.CustomerPaymentProfile.CreateFromSyncCard(syncCard);
        fromSyncCard.BAccountID = bAccountID;
        ((PXSelectBase<CCSynchronizeCards.CustomerPaymentProfile>) this.CustPaymentProfileForDialog).Insert(fromSyncCard);
        ++num;
      }
    }
    return num;
  }

  [PXMergeAttributes]
  [SyncCardCustomerSelector(new Type[] {typeof (PX.Objects.AR.Customer.acctCD), typeof (PX.Objects.AR.Customer.acctName)}, ValidateValue = false)]
  protected virtual void CCSynchronizeCard_BAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDefault]
  [PXRSACryptStringWithMask(1028, typeof (Search<PaymentMethodDetail.entryMask, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<CustomerPaymentMethodDetail.paymentMethodID>>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>, And<PaymentMethodDetail.detailID, Equal<Current<CustomerPaymentMethodDetail.detailID>>>>>>), IsUnicode = true)]
  protected virtual void CustomerPaymentMethodDetail_Value_CacheAttached(PXCache sender)
  {
  }

  private string GetCustomerNameByID(int? bAccountId)
  {
    string customerNameById = string.Empty;
    PX.Objects.AR.Customer customer = ((PXSelectBase<PX.Objects.AR.Customer>) new PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>((PXGraph) this)).SelectSingle(new object[1]
    {
      (object) bAccountId
    });
    if (customer != null)
      customerNameById = customer.AcctName;
    return customerNameById;
  }

  private List<CCSynchronizeCard> GetRecordsWithSameCustomerCCPID(string customerID)
  {
    customerID = CCProcessingHelper.DeleteCustomerPrefix(customerID);
    if (this.cacheRecordsSameCustomerCCPID == null)
    {
      this.cacheRecordsSameCustomerCCPID = new List<CCSynchronizeCard>();
      foreach (PXResult<CCSynchronizeCard> pxResult in ((PXSelectBase<CCSynchronizeCard>) this.CustomerCardPaymentData).Select(Array.Empty<object>()))
      {
        CCSynchronizeCard ccSynchronizeCard = PXResult<CCSynchronizeCard>.op_Implicit(pxResult);
        if (CCProcessingHelper.DeleteCustomerPrefix(ccSynchronizeCard.PCCustomerID) == customerID)
          this.cacheRecordsSameCustomerCCPID.Add(ccSynchronizeCard);
      }
    }
    return this.cacheRecordsSameCustomerCCPID;
  }

  private int GetCardsAllProfiles()
  {
    CCSynchronizeCards.CreditCardsFilter current = ((PXSelectBase<CCSynchronizeCards.CreditCardsFilter>) this.Filter).Current;
    string processingCenterId = current.ProcessingCenterId;
    CreditCardReceiverFactory receiverFactory = new CreditCardReceiverFactory(current);
    CCSynchronizeCardManager synchronizeCardManager = new CCSynchronizeCardManager((PXGraph) this, processingCenterId, receiverFactory);
    Dictionary<string, CustomerData> profilesFromService = synchronizeCardManager.GetCustomerProfilesFromService();
    synchronizeCardManager.SetCustomerProfileIds(profilesFromService.Select<KeyValuePair<string, CustomerData>, string>((Func<KeyValuePair<string, CustomerData>, string>) (i => i.Key)));
    Dictionary<string, CustomerCreditCard> unsynchronizedPaymentProfiles = synchronizeCardManager.GetUnsynchronizedPaymentProfiles();
    int cardsAllProfiles = 0;
    string paymentMethodId1 = this.GetPaymentMethodId("EFT", processingCenterId);
    string paymentMethodId2 = this.GetPaymentMethodId("CCD", processingCenterId);
    foreach (KeyValuePair<string, CustomerCreditCard> keyValuePair in unsynchronizedPaymentProfiles)
    {
      List<CCSynchronizeCard> entriesByCustomerCcpid = this.GetExistedSyncCardEntriesByCustomerCCPID(keyValuePair.Key, processingCenterId);
      CustomerCreditCard customerCreditCard = keyValuePair.Value;
      foreach (CreditCardData creditCard in customerCreditCard.CreditCards)
      {
        if (!this.CheckNotImportedRecordExists(customerCreditCard.CustomerProfileId, creditCard.PaymentProfileID, entriesByCustomerCcpid))
        {
          CCSynchronizeCard syncCard = new CCSynchronizeCard();
          CustomerData customerData = profilesFromService[customerCreditCard.CustomerProfileId];
          string cardTypeCode = this.GetCardTypeCode(creditCard.CardTypeCode);
          string paymentMethodId3;
          syncCard.PaymentType = this.GetPaymentMethodInfo(creditCard.PaymentMethodType, paymentMethodId1, paymentMethodId2, out paymentMethodId3);
          syncCard.PaymentMethodID = paymentMethodId3;
          syncCard.CardType = cardTypeCode;
          syncCard.ProcCenterCardTypeCode = cardTypeCode == "OTH" ? creditCard.CardType : (string) null;
          string cardNumber = creditCard.CardNumber.Trim('X');
          this.FormatMaskedCardNum(syncCard, cardNumber, creditCard.PaymentMethodType);
          syncCard.CCProcessingCenterID = processingCenterId;
          syncCard.CustomerCCPID = customerCreditCard.CustomerProfileId;
          syncCard.CustomerCCPIDHash = CCSynchronizeCard.GetSha1HashString(syncCard.CustomerCCPID);
          syncCard.PaymentCCPID = creditCard.PaymentProfileID;
          syncCard.PCCustomerID = customerData.CustomerCD;
          syncCard.PCCustomerDescription = customerData.CustomerName;
          syncCard.PCCustomerEmail = customerData.Email;
          DateTime? cardExpirationDate = creditCard.CardExpirationDate;
          if (cardExpirationDate.HasValue)
          {
            CCSynchronizeCard ccSynchronizeCard = syncCard;
            cardExpirationDate = creditCard.CardExpirationDate;
            DateTime? nullable = new DateTime?(cardExpirationDate.Value);
            ccSynchronizeCard.ExpirationDate = nullable;
          }
          if (creditCard.AddressData != null)
          {
            AddressData addressData = creditCard.AddressData;
            syncCard.FirstName = addressData.FirstName;
            syncCard.LastName = addressData.LastName;
          }
          ((PXSelectBase<CCSynchronizeCard>) this.CustomerCardPaymentData).Insert(syncCard);
          ++cardsAllProfiles;
        }
      }
    }
    return cardsAllProfiles;
  }

  protected virtual string GetPaymentMethodId(string paymentType, string processingCenterId)
  {
    PXResultset<CCProcessingCenterPmntMethod> pxResultset = PXSelectBase<CCProcessingCenterPmntMethod, PXSelectReadonly2<CCProcessingCenterPmntMethod, InnerJoin<PaymentMethod, On<CCProcessingCenterPmntMethod.paymentMethodID, Equal<PaymentMethod.paymentMethodID>, And<PaymentMethod.paymentType, Equal<Required<PaymentMethod.paymentType>>>>>, Where<CCProcessingCenterPmntMethod.processingCenterID, Equal<Required<CCSynchronizeCard.cCProcessingCenterID>>, And<PaymentMethod.isActive, Equal<True>, And<CCProcessingCenterPmntMethod.isActive, Equal<True>, And<PaymentMethod.useForAR, Equal<True>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) paymentType,
      (object) processingCenterId
    });
    return pxResultset.Count == 1 ? PXResultset<CCProcessingCenterPmntMethod>.op_Implicit(pxResultset).PaymentMethodID : (string) null;
  }

  protected virtual string GetPaymentMethodInfo(
    MeansOfPayment? paymentMethodType,
    string eftPaymentMethodId,
    string ccPaymentMethodId,
    out string paymentMethodId)
  {
    if (paymentMethodType.HasValue)
    {
      MeansOfPayment valueOrDefault = paymentMethodType.GetValueOrDefault();
      if (valueOrDefault != null)
      {
        if (valueOrDefault == 1)
        {
          paymentMethodId = eftPaymentMethodId;
          return "EFT";
        }
      }
      else
      {
        paymentMethodId = ccPaymentMethodId;
        return "CCD";
      }
    }
    paymentMethodId = (string) null;
    return (string) null;
  }

  protected virtual string GetCardTypeCode(CCCardType cCCardType)
  {
    return CardType.GetCardTypeCode(V2Converter.ConvertCardType(cCCardType));
  }

  private bool ValidateLoadCardsAction()
  {
    bool flag = false;
    CCSynchronizeCards.CreditCardsFilter current = ((PXSelectBase<CCSynchronizeCards.CreditCardsFilter>) this.Filter).Current;
    string processingCenterId = current.ProcessingCenterId;
    if (processingCenterId == null && current.ScheduledServiceSync.GetValueOrDefault())
      throw new PXException("Processing Center is not selected.");
    if (processingCenterId == null)
    {
      ((PXSelectBase) this.Filter).Cache.RaiseExceptionHandling<CCSynchronizeCards.CreditCardsFilter.processingCenterId>((object) current, (object) current.ProcessingCenterId, (Exception) new PXSetPropertyException("Processing Center is not selected."));
      flag = true;
    }
    return flag;
  }

  public bool ValidateImportCard(CCSynchronizeCard card, int cardIndex)
  {
    bool flag = true;
    if (!card.BAccountID.HasValue)
    {
      PXProcessing<CCSynchronizeCard>.SetError(cardIndex, "Customer not defined!");
      flag = false;
    }
    Tuple<CustomerClass, PX.Objects.AR.Customer> customerAndClassById = this.customerRepo.GetCustomerAndClassById(card.BAccountID);
    if (customerAndClassById != null)
    {
      CustomerClass customerClass = customerAndClassById.Item1;
      if (customerClass.SavePaymentProfiles == "P")
      {
        PX.Objects.AR.Customer customer = customerAndClassById.Item2;
        PXProcessing<CCSynchronizeCard>.SetError(cardIndex, PXMessages.LocalizeFormatNoPrefix("Saving payment profiles is not allowed for the {0} customer class that is selected for the {1} customer.", new object[2]
        {
          (object) customerClass.CustomerClassID,
          (object) customer.AcctCD
        }));
        flag = false;
      }
    }
    if (card.PaymentMethodID == null)
    {
      PXProcessing<CCSynchronizeCard>.SetError(cardIndex, "Payment Method not defined!");
      flag = false;
    }
    if (card.CashAccountID.HasValue && !this.CheckCashAccountAvailability(card))
    {
      PXProcessing<CCSynchronizeCard>.SetError(cardIndex, PXMessages.LocalizeFormatNoPrefixNLA("The specified cash account is not configured for use in AR for the {0} payment method.", new object[1]
      {
        (object) card.PaymentMethodID
      }));
      flag = false;
    }
    return flag;
  }

  public void CreateCustomerPaymentMethodRecord(CCSynchronizeCard item)
  {
    PXCache cach = ((PXGraph) this).Caches[typeof (PX.Objects.AR.CustomerPaymentMethod)];
    PX.Objects.AR.CustomerPaymentMethod instance = cach.CreateInstance() as PX.Objects.AR.CustomerPaymentMethod;
    instance.BAccountID = item.BAccountID;
    instance.CustomerCCPID = item.CustomerCCPID;
    instance.PaymentMethodID = item.PaymentMethodID;
    instance.CashAccountID = item.CashAccountID;
    instance.CCProcessingCenterID = item.CCProcessingCenterID;
    instance.CardType = item.CardType;
    instance.ProcCenterCardTypeCode = item.ProcCenterCardTypeCode;
    if (item.ExpirationDate.HasValue)
      cach.SetValueExt<PX.Objects.AR.CustomerPaymentMethod.expirationDate>((object) instance, (object) item.ExpirationDate);
    string str = item.CardNumber;
    string displayMask = this.GetDisplayMask(item);
    if (displayMask != null)
      str = $"{str.Split(':')[0]}:{this.CCDisplayMaskService.UseAdjustedDisplayMaskForCardNumber("XXXX" + str.Substring(str.Length - 4), displayMask)}";
    cach.SetValueExt<PX.Objects.AR.CustomerPaymentMethod.descr>((object) instance, (object) str);
    cach.Insert((object) instance);
    cach.Persist((PXDBOperation) 2);
    PX.Objects.AR.CustomerPaymentMethod current = cach.Current as PX.Objects.AR.CustomerPaymentMethod;
    this.CreateCustomerPaymentMethodDetailRecord(current, item);
    this.CreateCustomerProcessingCenterRecord(current, item);
  }

  public void UpdateCCProcessingSyncronizeCardRecord(CCSynchronizeCard item)
  {
    PXCache cach = ((PXGraph) this).Caches[typeof (CCSynchronizeCard)];
    item.Imported = new bool?(true);
    cach.Update((object) item);
    cach.Persist((PXDBOperation) 1);
  }

  private void CreateCustomerProcessingCenterRecord(
    PX.Objects.AR.CustomerPaymentMethod customerPM,
    CCSynchronizeCard syncCard)
  {
    PXCache cach = ((PXGraph) this).Caches[typeof (CustomerProcessingCenterID)];
    cach.ClearQueryCacheObsolete();
    if (((PXSelectBase<CustomerProcessingCenterID>) new PXSelectReadonly<CustomerProcessingCenterID, Where<CustomerProcessingCenterID.cCProcessingCenterID, Equal<Required<CCSynchronizeCards.CreditCardsFilter.processingCenterId>>, And<CustomerProcessingCenterID.bAccountID, Equal<Required<CustomerProcessingCenterID.bAccountID>>, And<CustomerProcessingCenterID.customerCCPID, Equal<Required<CustomerProcessingCenterID.customerCCPID>>>>>>((PXGraph) this)).SelectSingle(new object[3]
    {
      (object) syncCard.CCProcessingCenterID,
      (object) syncCard.BAccountID,
      (object) syncCard.CustomerCCPID
    }) != null)
      return;
    CustomerProcessingCenterID instance = cach.CreateInstance() as CustomerProcessingCenterID;
    instance.BAccountID = syncCard.BAccountID;
    instance.CCProcessingCenterID = syncCard.CCProcessingCenterID;
    instance.CustomerCCPID = syncCard.CustomerCCPID;
    cach.Insert((object) instance);
    cach.Persist((PXDBOperation) 2);
  }

  private void CreateCustomerPaymentMethodDetailRecord(
    PX.Objects.AR.CustomerPaymentMethod customerPM,
    CCSynchronizeCard syncCard)
  {
    PXResultset<PaymentMethodDetail> methodDetailParams = this.GetPaymentMethodDetailParams(customerPM.PaymentMethodID);
    PXCache cach = ((PXGraph) this).Caches[typeof (CustomerPaymentMethodDetail)];
    foreach (PXResult<PaymentMethodDetail> pxResult in methodDetailParams)
    {
      PaymentMethodDetail paymentMethodDetail = PXResult<PaymentMethodDetail>.op_Implicit(pxResult);
      CustomerPaymentMethodDetail instance = cach.CreateInstance() as CustomerPaymentMethodDetail;
      instance.DetailID = paymentMethodDetail.DetailID;
      instance.PMInstanceID = customerPM.PMInstanceID;
      instance.PaymentMethodID = customerPM.PaymentMethodID;
      if (instance.DetailID == "CCDNUM")
      {
        Match match = new Regex("[\\d]+").Match(syncCard.CardNumber);
        if (match.Success)
        {
          string str = match.Value.PadLeft(8, 'X');
          instance.Value = str;
        }
      }
      if (instance.DetailID == "CCPID")
        instance.Value = syncCard.PaymentCCPID;
      cach.Insert((object) instance);
      cach.Persist((PXDBOperation) 2);
    }
  }

  private string GetDisplayMask(CCSynchronizeCard item)
  {
    foreach (PXResult<PaymentMethodDetail> methodDetailParam in this.GetPaymentMethodDetailParams(item.PaymentMethodID))
    {
      PaymentMethodDetail paymentMethodDetail = PXResult<PaymentMethodDetail>.op_Implicit(methodDetailParam);
      if (paymentMethodDetail.DetailID == "CCDNUM")
        return paymentMethodDetail.DisplayMask;
    }
    return (string) null;
  }

  private PXResultset<PaymentMethodDetail> GetPaymentMethodDetailParams(string paymentMethodId)
  {
    return ((PXSelectBase<PaymentMethodDetail>) new PXSelectReadonly<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Required<PaymentMethodDetail.paymentMethodID>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) paymentMethodId
    });
  }

  protected virtual void FormatMaskedCardNum(
    CCSynchronizeCard syncCard,
    string cardNumber,
    MeansOfPayment? meansOfPayment)
  {
    string displayName = CardType.GetDisplayName(syncCard.CardType);
    syncCard.CardNumber = this.CCDisplayMaskService.UseDefaultMaskForCardNumber(cardNumber, displayName, meansOfPayment);
  }

  private bool CheckCashAccountAvailability(CCSynchronizeCard row)
  {
    return GraphHelper.RowCast<CashAccount>((IEnumerable) PXSelectorAttribute.SelectAll<CCSynchronizeCard.cashAccountID>(((PXSelectBase) this.CustomerCardPaymentData).Cache, (object) row)).Any<CashAccount>((Func<CashAccount, bool>) (i =>
    {
      int? cashAccountId1 = i.CashAccountID;
      int? cashAccountId2 = row.CashAccountID;
      return cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue;
    }));
  }

  private IEnumerable<PXResult<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail>> GetPaymentsProfilesByCustomer(
    string processingCenterID,
    string customerCCPID)
  {
    return (IEnumerable<PXResult<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail>>) ((IQueryable<PXResult<PX.Objects.AR.CustomerPaymentMethod>>) ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) new PXSelectReadonly2<PX.Objects.AR.CustomerPaymentMethod, InnerJoin<CustomerPaymentMethodDetail, On<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<CustomerPaymentMethodDetail.pMInstanceID>>, InnerJoin<PaymentMethodDetail, On<CustomerPaymentMethodDetail.detailID, Equal<PaymentMethodDetail.detailID>, And<CustomerPaymentMethodDetail.paymentMethodID, Equal<PaymentMethodDetail.paymentMethodID>>>>>, Where<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID>>, And<PX.Objects.AR.CustomerPaymentMethod.customerCCPID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.customerCCPID>>, And<PaymentMethodDetail.isCCProcessingID, Equal<True>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) processingCenterID,
      (object) customerCCPID
    })).Select<PXResult<PX.Objects.AR.CustomerPaymentMethod>, PXResult<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail>>((Expression<Func<PXResult<PX.Objects.AR.CustomerPaymentMethod>, PXResult<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail>>>) (i => (PXResult<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail>) i));
  }

  private List<CCSynchronizeCard> GetExistedSyncCardEntriesByCustomerCCPID(
    string customerCCPID,
    string processingCenterId)
  {
    return GraphHelper.RowCast<CCSynchronizeCard>((IEnumerable) ((PXSelectBase<CCSynchronizeCard>) new PXSelect<CCSynchronizeCard, Where<CCSynchronizeCard.customerCCPIDHash, Equal<Required<CCSynchronizeCard.customerCCPIDHash>>, And<CCSynchronizeCard.cCProcessingCenterID, Equal<Required<CCSynchronizeCard.cCProcessingCenterID>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) CCSynchronizeCard.GetSha1HashString(customerCCPID),
      (object) processingCenterId
    })).ToList<CCSynchronizeCard>();
  }

  private bool CheckNotImportedRecordExists(
    string custCCPID,
    string paymentCCPID,
    List<CCSynchronizeCard> checkList)
  {
    bool flag = false;
    if (checkList.Where<CCSynchronizeCard>((Func<CCSynchronizeCard, bool>) (i => i.CustomerCCPID == custCCPID && i.PaymentCCPID == paymentCCPID && !i.Imported.GetValueOrDefault())).FirstOrDefault<CCSynchronizeCard>() != null)
      flag = true;
    return flag;
  }

  private bool CheckCustomerPaymentProfileExists(CCSynchronizeCard syncCard, int cardIndex)
  {
    IEnumerable<PXResult<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail>> profilesByCustomer = this.GetPaymentsProfilesByCustomer(syncCard.CCProcessingCenterID, syncCard.CustomerCCPID);
    string paymentCcpid = syncCard.PaymentCCPID;
    foreach (CustomerPaymentMethodDetail paymentMethodDetail in GraphHelper.RowCast<CustomerPaymentMethodDetail>((IEnumerable) profilesByCustomer))
    {
      if (paymentMethodDetail.Value == paymentCcpid)
      {
        PXProcessing<CCSynchronizeCard>.SetError(cardIndex, "The customer payment method cannot be created because a record with the specified payment profile ID already exists.");
        return true;
      }
    }
    return false;
  }

  public virtual void Persist()
  {
    ((PXSelectBase) this.CustPaymentProfileForDialog).Cache.Clear();
    ((PXGraph) this).Persist();
  }

  [Serializable]
  public class CustomerPaymentProfile : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXInt(IsKey = true)]
    public virtual int? RecordID { get; set; }

    [PXInt]
    [PXUIField(Visible = false)]
    public virtual int? BAccountID { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Proc. Center Cust. Profile ID", Enabled = false)]
    public virtual string CustomerCCPID { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Proc. Center Cust. ID", Enabled = false)]
    public virtual string PCCustomerID { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Proc. Center Cust. Descr.", Enabled = false)]
    public virtual string PCCustomerDescription { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Proc. Center Cust. Email", Enabled = false)]
    public virtual string PCCustomerEmail { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Proc. Center Payment Profile ID", Enabled = false)]
    public virtual string PaymentCCPID { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Proc. Center Payment Profile First Name", Enabled = false)]
    public virtual string PaymentProfileFirstName { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Proc. Center Payment Profile Last Name", Enabled = false)]
    public virtual string PaymentProfileLastName { get; set; }

    [PXBool]
    [PXUIField(DisplayName = "Selected")]
    public virtual bool? Selected { get; set; }

    public static CCSynchronizeCards.CustomerPaymentProfile CreateFromSyncCard(
      CCSynchronizeCard syncCard)
    {
      return new CCSynchronizeCards.CustomerPaymentProfile()
      {
        RecordID = syncCard.RecordID,
        CustomerCCPID = syncCard.CustomerCCPID,
        PCCustomerDescription = syncCard.PCCustomerDescription,
        PCCustomerEmail = syncCard.PCCustomerEmail,
        PCCustomerID = syncCard.PCCustomerID,
        BAccountID = syncCard.BAccountID,
        PaymentProfileFirstName = syncCard.FirstName,
        PaymentProfileLastName = syncCard.LastName,
        PaymentCCPID = syncCard.PaymentCCPID
      };
    }

    public abstract class recordID : IBqlField, IBqlOperand
    {
    }

    public abstract class bAccountID : IBqlField, IBqlOperand
    {
    }

    public abstract class customerCCPID : IBqlField, IBqlOperand
    {
    }

    public abstract class pCCustomerID : IBqlField, IBqlOperand
    {
    }

    public abstract class pCCustomerDescription : IBqlField, IBqlOperand
    {
    }

    public abstract class pCCustomerEmail : IBqlField, IBqlOperand
    {
    }

    public abstract class paymentCCPID : IBqlField, IBqlOperand
    {
    }

    public abstract class paymentProfileFirstName : IBqlField, IBqlOperand
    {
    }

    public abstract class paymentProfileLastName : IBqlField, IBqlOperand
    {
    }

    public abstract class setPaymentProfile : IBqlField, IBqlOperand
    {
    }
  }

  [Serializable]
  public class CreditCardsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString]
    [CCProcessingCenterSelector(CCProcessingFeature.ExtendedProfileManagement)]
    [PXUIField(DisplayName = "Processing Center")]
    public virtual string ProcessingCenterId { get; set; }

    [PXBool]
    [PXUnboundDefault(false)]
    [PXUIField(DisplayName = "Scheduled Sync")]
    public virtual bool? ScheduledServiceSync { get; set; }

    [PXBool]
    [PXUnboundDefault(false)]
    [PXUIField(DisplayName = "Load Expired Card Data")]
    public virtual bool? LoadExpiredCards { get; set; }

    [PXString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
    [PXSelector(typeof (Search5<CCProcessingCenterPmntMethod.paymentMethodID, InnerJoin<PaymentMethod, On<CCProcessingCenterPmntMethod.paymentMethodID, Equal<PaymentMethod.paymentMethodID>>>, Where<CCProcessingCenterPmntMethod.processingCenterID, Equal<Current<CCSynchronizeCards.CreditCardsFilter.processingCenterId>>, And<PaymentMethod.paymentType, Equal<PaymentMethodType.eft>, And<PaymentMethod.isActive, Equal<True>, And<CCProcessingCenterPmntMethod.isActive, Equal<True>, And<PaymentMethod.useForAR, Equal<True>>>>>>, Aggregate<GroupBy<CCProcessingCenterPmntMethod.paymentMethodID>>>), new Type[] {typeof (CCProcessingCenterPmntMethod.paymentMethodID), typeof (PaymentMethod.descr)})]
    [PXUIField(DisplayName = "Payment Method for EFT")]
    public virtual string EftPaymentMethodId { get; set; }

    [PXString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
    [PXSelector(typeof (Search5<CCProcessingCenterPmntMethod.paymentMethodID, InnerJoin<PaymentMethod, On<CCProcessingCenterPmntMethod.paymentMethodID, Equal<PaymentMethod.paymentMethodID>>>, Where<CCProcessingCenterPmntMethod.processingCenterID, Equal<Current<CCSynchronizeCards.CreditCardsFilter.processingCenterId>>, And<PaymentMethod.paymentType, Equal<PaymentMethodType.creditCard>, And<PaymentMethod.isActive, Equal<True>, And<CCProcessingCenterPmntMethod.isActive, Equal<True>, And<PaymentMethod.useForAR, Equal<True>>>>>>, Aggregate<GroupBy<CCProcessingCenterPmntMethod.paymentMethodID>>>), new Type[] {typeof (CCProcessingCenterPmntMethod.paymentMethodID), typeof (PaymentMethod.descr)})]
    [PXUIField(DisplayName = "Payment Method for Credit Card")]
    public virtual string CCPaymentMethodId { get; set; }

    [PXBool]
    [PXUIField(DisplayName = "Overwrite EFT Values")]
    public virtual bool? OverwriteEftPaymentMethod { get; set; }

    [PXBool]
    [PXUIField(DisplayName = "Overwrite Credit Card Values")]
    public virtual bool? OverwriteCCPaymentMethod { get; set; }

    [PXBool]
    public virtual bool? EnableCustomerPaymentDialog { get; set; }

    [PXBool]
    [PXUnboundDefault(false)]
    public virtual bool? IsScheduleProcess { get; set; }

    [PXUIField(DisplayName = "Select the payment profiles to be assigned to the customer", Enabled = false)]
    public virtual string CustomerName { get; set; }

    public abstract class processingCenterId : IBqlField, IBqlOperand
    {
    }

    public abstract class scheduledServiceSync : IBqlField, IBqlOperand
    {
    }

    public abstract class loadExpiredCard : IBqlField, IBqlOperand
    {
    }

    public abstract class eftPaymentMethodId : IBqlField, IBqlOperand
    {
    }

    public abstract class ccPaymentMethodId : IBqlField, IBqlOperand
    {
    }

    public abstract class overwriteEftPaymentMethod : IBqlField, IBqlOperand
    {
    }

    public abstract class overwriteCCPaymentMethod : IBqlField, IBqlOperand
    {
    }

    public abstract class enableMultipleSettingCustomer : IBqlField, IBqlOperand
    {
    }

    public abstract class customerName : IBqlField, IBqlOperand
    {
    }
  }
}
