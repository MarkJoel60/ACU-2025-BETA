// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARChargeInvoices
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR.MigrationMode;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
[Serializable]
public class ARChargeInvoices : PXGraph<
#nullable disable
ARChargeInvoices>
{
  public PXFilter<ARChargeInvoices.PayBillsFilter> Filter;
  public PXCancel<ARChargeInvoices.PayBillsFilter> Cancel;
  public PXAction<ARChargeInvoices.PayBillsFilter> viewDocument;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessingJoin<ARInvoice, ARChargeInvoices.PayBillsFilter, InnerJoin<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>>, InnerJoinSingleTable<CustomerPaymentMethod, On<CustomerPaymentMethod.bAccountID, Equal<ARInvoice.customerID>, And<CustomerPaymentMethod.pMInstanceID, Equal<ARInvoice.pMInstanceID>, And<CustomerPaymentMethod.isActive, Equal<True>>>>, LeftJoin<PaymentMethodAccount, On<ARInvoice.cashAccountID, IsNull, And<Where2<Where<PaymentMethodAccount.cashAccountID, Equal<CustomerPaymentMethod.cashAccountID>, And<PaymentMethodAccount.paymentMethodID, Equal<CustomerPaymentMethod.paymentMethodID>, And<PaymentMethodAccount.useForAR, Equal<True>, And<PaymentMethodAccount.aRIsDefault, Equal<True>, And<ARInvoice.docType, NotEqual<ARDocType.creditMemo>>>>>>, Or<Where<CustomerPaymentMethod.cashAccountID, IsNull, And<PaymentMethodAccount.paymentMethodID, Equal<ARInvoice.paymentMethodID>, And<Where2<Where<ARInvoice.docType, NotEqual<ARDocType.creditMemo>, And<PaymentMethodAccount.aRIsDefault, Equal<True>>>, Or<Where<ARInvoice.docType, Equal<ARDocType.creditMemo>, And<PaymentMethodAccount.aRIsDefaultForRefund, Equal<True>>>>>>>>>>>>, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<CustomerPaymentMethod.paymentMethodID>, And<PX.Objects.CA.PaymentMethod.paymentType, In3<PaymentMethodType.creditCard, PaymentMethodType.eft>, And<PX.Objects.CA.PaymentMethod.aRIsProcessingRequired, Equal<True>>>>, LeftJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<ARInvoice.cashAccountID>, Or<Where<ARInvoice.cashAccountID, IsNull, And<PX.Objects.CA.CashAccount.cashAccountID, Equal<PaymentMethodAccount.cashAccountID>>>>>, LeftJoin<ARAdjust, On<ARAdjust.adjdDocType, Equal<ARInvoice.docType>, And<ARAdjust.adjdRefNbr, Equal<ARInvoice.refNbr>, And<ARAdjust.released, Equal<False>, And<ARAdjust.voided, Equal<False>>>>>, LeftJoin<ARPayment, On<ARPayment.docType, Equal<ARAdjust.adjgDocType>, And<ARPayment.refNbr, Equal<ARAdjust.adjgRefNbr>>>>>>>>>>, Where<ARInvoice.released, Equal<True>, And<ARInvoice.openDoc, Equal<True>, And<ARInvoice.pendingPPD, NotEqual<True>>>>, OrderBy<Asc<ARInvoice.customerID>>> ARDocumentList;
  public ToggleCurrency<ARChargeInvoices.PayBillsFilter> CurrencyView;
  public PXSelect<ARPayment, Where<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>>> arPayment;
  public PXSelect<PX.Objects.CM.CurrencyInfo> currencyinfo;
  public PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>> CurrencyInfo_CuryInfoID;
  public ARSetupNoMigrationMode ARSetup;

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Branch", Visible = false)]
  [PXUIVisible(typeof (BqlChainableConditionLite<FeatureInstalled<FeaturesSet.branch>>.Or<FeatureInstalled<FeaturesSet.multiCompany>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<ARInvoice.branchID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Document Currency")]
  protected virtual void _(PX.Data.Events.CacheAttached<ARInvoice.curyID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Cash Account Currency")]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CA.CashAccount.curyID> e)
  {
  }

  public ARChargeInvoices()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    ((PXProcessingBase<ARInvoice>) this.ARDocumentList).SetSelected<ARInvoice.selected>();
    ((PXProcessing<ARInvoice>) this.ARDocumentList).SetProcessCaption("Process");
    ((PXProcessing<ARInvoice>) this.ARDocumentList).SetProcessAllCaption("Process All");
    ((PXSelectBase) this.ARDocumentList).Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled<ARInvoice.docType>(((PXSelectBase) this.ARDocumentList).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<ARInvoice.refNbr>(((PXSelectBase) this.ARDocumentList).Cache, (object) null, true);
    OpenPeriodAttribute.SetValidatePeriod<ARChargeInvoices.PayBillsFilter.payFinPeriodID>(((PXSelectBase) this.Filter).Cache, (object) null, ((PXGraph) this).IsContractBasedAPI || ((PXGraph) this).IsImport || ((PXGraph) this).IsExport || ((PXGraph) this).UnattendedMode ? PeriodValidation.DefaultUpdate : PeriodValidation.DefaultSelectUpdate);
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    ARInvoice current = ((PXSelectBase<ARInvoice>) this.ARDocumentList).Current;
    if (current != null)
    {
      ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
      ((PXSelectBase<ARInvoice>) instance.Document).Current = PXResultset<ARInvoice>.op_Implicit(((PXSelectBase<ARInvoice>) instance.Document).Search<ARInvoice.refNbr>((object) current.RefNbr, new object[1]
      {
        (object) current.DocType
      }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Invoice");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  protected virtual IEnumerable ardocumentlist()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultSorted = true
    };
    ARChargeInvoices.PayBillsFilter current = ((PXSelectBase<ARChargeInvoices.PayBillsFilter>) this.Filter).Current;
    if (current == null || !current.PayDate.HasValue)
      return (IEnumerable) pxDelegateResult;
    DateTime dateTime1 = current.PayDate.Value.AddDays((double) (-1 * (int) current.OverDueFor.Value));
    DateTime dateTime2 = current.PayDate.Value.AddDays((double) current.DueInLessThan.Value);
    DateTime? payDate = current.PayDate;
    DateTime dateTime3 = payDate.Value;
    DateTime dateTime4 = dateTime3.AddDays((double) (-1 * (int) current.DiscountExparedWithinLast.Value));
    payDate = current.PayDate;
    dateTime3 = payDate.Value;
    DateTime dateTime5 = dateTime3.AddDays((double) current.DiscountExpiresInLessThan.Value);
    string[] strArray = new string[5]
    {
      "INV",
      "DRM",
      "FCH",
      "PPI",
      "CRM"
    };
    PXView view = ((PXSelectBase) this.ARDocumentList).View.BqlSelect.WhereAnd<Where2<Where2<Where2<Where<Current<ARChargeInvoices.PayBillsFilter.showOverDueFor>, Equal<True>, And<ARInvoice.dueDate, LessEqual<Required<ARInvoice.dueDate>>>>, Or2<Where<Current<ARChargeInvoices.PayBillsFilter.showDueInLessThan>, Equal<True>, And<ARInvoice.dueDate, GreaterEqual<Current<ARChargeInvoices.PayBillsFilter.payDate>>, And<ARInvoice.dueDate, LessEqual<Required<ARInvoice.dueDate>>>>>, Or2<Where<Current<ARChargeInvoices.PayBillsFilter.showDiscountExparedWithinLast>, Equal<True>, And<ARInvoice.discDate, GreaterEqual<Required<ARInvoice.discDate>>, And<ARInvoice.discDate, LessEqual<Current<ARChargeInvoices.PayBillsFilter.payDate>>>>>, Or<Where<Current<ARChargeInvoices.PayBillsFilter.showDiscountExpiresInLessThan>, Equal<True>, And<ARInvoice.discDate, GreaterEqual<Current<ARChargeInvoices.PayBillsFilter.payDate>>, And<ARInvoice.discDate, LessEqual<Required<ARInvoice.discDate>>>>>>>>>, Or<Where<Current<ARChargeInvoices.PayBillsFilter.showOverDueFor>, Equal<False>, And<Current<ARChargeInvoices.PayBillsFilter.showDueInLessThan>, Equal<False>, And<Current<ARChargeInvoices.PayBillsFilter.showDiscountExparedWithinLast>, Equal<False>, And<Current<ARChargeInvoices.PayBillsFilter.showDiscountExpiresInLessThan>, Equal<False>>>>>>>, And<ARInvoice.docType, In<Required<ARInvoice.docType>>, And<Where2<Where<ARInvoice.docType, NotEqual<ARDocType.prepaymentInvoice>, Or<ARInvoice.pendingPayment, Equal<True>>>, And<Where2<Where<ARAdjust.adjgRefNbr, IsNull, Or<ARPayment.voided, Equal<True>>>, And<Match<Customer, Current<AccessInfo.userName>>>>>>>>>>().CreateView((PXGraph) this);
    object[] objArray = new object[5]
    {
      (object) dateTime1,
      (object) dateTime2,
      (object) dateTime4,
      (object) dateTime5,
      (object) strArray
    };
    foreach (PXResult<ARInvoice, Customer, CustomerPaymentMethod, PaymentMethodAccount, PX.Objects.CA.PaymentMethod, PX.Objects.CA.CashAccount, ARAdjust, ARPayment> pxResult in view.SelectWithViewContext(objArray))
    {
      ARInvoice aDoc = PXResult<ARInvoice, Customer, CustomerPaymentMethod, PaymentMethodAccount, PX.Objects.CA.PaymentMethod, PX.Objects.CA.CashAccount, ARAdjust, ARPayment>.op_Implicit(pxResult);
      PX.Objects.CA.CashAccount cashAccount = PXResult<ARInvoice, Customer, CustomerPaymentMethod, PaymentMethodAccount, PX.Objects.CA.PaymentMethod, PX.Objects.CA.CashAccount, ARAdjust, ARPayment>.op_Implicit(pxResult);
      if (cashAccount == null || !cashAccount.AccountID.HasValue)
        cashAccount = this.findDefaultCashAccount(aDoc);
      if (cashAccount != null && (string.IsNullOrEmpty(current.CuryID) || !(current.CuryID != cashAccount.CuryID)))
        ((List<object>) pxDelegateResult).Add((object) new PXResult<ARInvoice, Customer, CustomerPaymentMethod, PX.Objects.CA.CashAccount>(PXResult<ARInvoice, Customer, CustomerPaymentMethod, PaymentMethodAccount, PX.Objects.CA.PaymentMethod, PX.Objects.CA.CashAccount, ARAdjust, ARPayment>.op_Implicit(pxResult), PXResult<ARInvoice, Customer, CustomerPaymentMethod, PaymentMethodAccount, PX.Objects.CA.PaymentMethod, PX.Objects.CA.CashAccount, ARAdjust, ARPayment>.op_Implicit(pxResult), PXResult<ARInvoice, Customer, CustomerPaymentMethod, PaymentMethodAccount, PX.Objects.CA.PaymentMethod, PX.Objects.CA.CashAccount, ARAdjust, ARPayment>.op_Implicit(pxResult), cashAccount));
    }
    PXView.StartRow = 0;
    ((PXSelectBase) this.ARDocumentList).Cache.IsDirty = false;
    return (IEnumerable) pxDelegateResult;
  }

  public static void CreatePayments(
    List<ARInvoice> list,
    ARChargeInvoices.PayBillsFilter filter,
    PX.Objects.CM.CurrencyInfo info)
  {
    if (RunningFlagScope<ARChargeInvoices>.IsRunning)
      throw new PXSetPropertyException("Another 'Generate Payments' process is already running. Please wait until it is finished.", (PXErrorLevel) 2);
    using (new RunningFlagScope<ARChargeInvoices>())
      ARChargeInvoices._createPayments(list, filter, info);
  }

  private static void _createPayments(
    List<ARInvoice> list,
    ARChargeInvoices.PayBillsFilter filter,
    PX.Objects.CM.CurrencyInfo info)
  {
    bool flag = false;
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    foreach (IEnumerable<\u003C\u003Ef__AnonymousType9<ARInvoice, int>> source in list.Zip(Enumerable.Range(0, list.Count), (doc, idx) => new
    {
      Document = doc,
      Index = idx
    }).GroupBy(e => new
    {
      CustomerID = e.Document.CustomerID,
      PaymentMethodID = e.Document.PaymentMethodID
    }))
    {
      List<\u003C\u003Ef__AnonymousType9<ARInvoice, int>> list1 = source.ToList();
      Decimal num1 = list1.Sum(d =>
      {
        Decimal? signAmount = d.Document.SignAmount;
        Decimal? docBal = d.Document.DocBal;
        return (signAmount.HasValue & docBal.HasValue ? new Decimal?(signAmount.GetValueOrDefault() * docBal.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
      });
      foreach (var data in list1.Where(d =>
      {
        Decimal? signAmount = d.Document.SignAmount;
        Decimal num2 = (Decimal) 1;
        return signAmount.GetValueOrDefault() == num2 & signAmount.HasValue;
      }))
        flag &= ARChargeInvoices.ProcessDocument(data.Document, data.Index, instance, filter, info);
      if (num1 < 0M)
        ((PXGraph) instance).Clear();
      foreach (var data in list1.Where(d =>
      {
        Decimal? signAmount = d.Document.SignAmount;
        Decimal num3 = (Decimal) -1;
        return signAmount.GetValueOrDefault() == num3 & signAmount.HasValue;
      }))
        flag &= ARChargeInvoices.ProcessDocument(data.Document, data.Index, instance, filter, info);
      if (flag)
        throw new PXException("Creation of the Payment document has failed for one or more selected documents. Please, check specific error in each row");
    }
  }

  public static bool ProcessDocument(
    ARInvoice doc,
    int index,
    ARPaymentEntry pe,
    ARChargeInvoices.PayBillsFilter filter,
    PX.Objects.CM.CurrencyInfo info)
  {
    ARPayment row = (ARPayment) null;
    bool flag = false;
    try
    {
      Decimal? signAmount = doc.SignAmount;
      Decimal num = (Decimal) -1;
      string paymentType = signAmount.GetValueOrDefault() == num & signAmount.HasValue ? "REF" : "PMT";
      pe.CreatePayment(doc, PX.Objects.CM.Extensions.CurrencyInfo.GetEX(info), filter.PayDate, filter.PayFinPeriodID, false, paymentType);
      row = ((PXSelectBase<ARPayment>) pe.Document).Current;
      if (row != null)
      {
        row.Hold = new bool?(false);
        ((PXSelectBase<ARPayment>) pe.Document).Update(row);
        FinPeriodIDAttribute.SetPeriodsByMaster<ARPayment.finPeriodID>(((PXSelectBase) pe.Document).Cache, (object) row, filter.PayFinPeriodID);
      }
      ((PXAction) pe.Save).Press();
    }
    catch (Exception ex)
    {
      PXProcessing<ARInvoice>.SetError(index, ex.Message);
      flag = true;
    }
    if (!flag)
      PXProcessing<ARInvoice>.SetInfo(index, PXMessages.LocalizeFormatNoPrefixNLA("Payment {0} has been created", new object[1]
      {
        (object) row.RefNbr
      }));
    return flag;
  }

  protected virtual void PayBillsFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARChargeInvoices.\u003C\u003Ec__DisplayClass19_0 cDisplayClass190 = new ARChargeInvoices.\u003C\u003Ec__DisplayClass19_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass190.filter = e.Row as ARChargeInvoices.PayBillsFilter;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass190.filter == null)
      return;
    // ISSUE: reference to a compiler-generated field
    PXUIFieldAttribute.SetVisible<ARChargeInvoices.PayBillsFilter.curyID>(sender, (object) cDisplayClass190.filter, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
    PXCache pxCache1 = sender;
    // ISSUE: reference to a compiler-generated field
    ARChargeInvoices.PayBillsFilter filter1 = cDisplayClass190.filter;
    // ISSUE: reference to a compiler-generated field
    ARChargeInvoices.PayBillsFilter filter2 = cDisplayClass190.filter;
    bool? nullable;
    int num1;
    if (filter2 == null)
    {
      num1 = 0;
    }
    else
    {
      nullable = filter2.ShowOverDueFor;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<ARChargeInvoices.PayBillsFilter.overDueFor>(pxCache1, (object) filter1, num1 != 0);
    PXCache pxCache2 = sender;
    // ISSUE: reference to a compiler-generated field
    ARChargeInvoices.PayBillsFilter filter3 = cDisplayClass190.filter;
    // ISSUE: reference to a compiler-generated field
    ARChargeInvoices.PayBillsFilter filter4 = cDisplayClass190.filter;
    int num2;
    if (filter4 == null)
    {
      num2 = 0;
    }
    else
    {
      nullable = filter4.ShowDueInLessThan;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<ARChargeInvoices.PayBillsFilter.dueInLessThan>(pxCache2, (object) filter3, num2 != 0);
    PXCache pxCache3 = sender;
    // ISSUE: reference to a compiler-generated field
    ARChargeInvoices.PayBillsFilter filter5 = cDisplayClass190.filter;
    // ISSUE: reference to a compiler-generated field
    ARChargeInvoices.PayBillsFilter filter6 = cDisplayClass190.filter;
    int num3;
    if (filter6 == null)
    {
      num3 = 0;
    }
    else
    {
      nullable = filter6.ShowDiscountExparedWithinLast;
      num3 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<ARChargeInvoices.PayBillsFilter.discountExparedWithinLast>(pxCache3, (object) filter5, num3 != 0);
    PXCache pxCache4 = sender;
    // ISSUE: reference to a compiler-generated field
    ARChargeInvoices.PayBillsFilter filter7 = cDisplayClass190.filter;
    // ISSUE: reference to a compiler-generated field
    ARChargeInvoices.PayBillsFilter filter8 = cDisplayClass190.filter;
    int num4;
    if (filter8 == null)
    {
      num4 = 0;
    }
    else
    {
      nullable = filter8.ShowDiscountExpiresInLessThan;
      num4 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<ARChargeInvoices.PayBillsFilter.discountExpiresInLessThan>(pxCache4, (object) filter7, num4 != 0);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass190.info = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.CurrencyInfo_CuryInfoID).Select(new object[1]
    {
      (object) cDisplayClass190.filter.CuryInfoID
    }));
    // ISSUE: method pointer
    ((PXProcessingBase<ARInvoice>) this.ARDocumentList).SetProcessDelegate(new PXProcessingBase<ARInvoice>.ProcessListDelegate((object) cDisplayClass190, __methodptr(\u003CPayBillsFilter_RowSelected\u003Eb__0)));
  }

  protected virtual void PayBillsFilter_CustomerID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((PXSelectBase) this.ARDocumentList).Cache.Clear();
  }

  protected virtual void PayBillsFilter_PayDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    foreach (PXResult<PX.Objects.CM.CurrencyInfo> pxResult in PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<ARChargeInvoices.PayBillsFilter.curyInfoID>>>>.Config>.Select((PXGraph) this, (object[]) null))
      ((PXSelectBase) this.currencyinfo).Cache.SetDefaultExt<PX.Objects.CM.CurrencyInfo.curyEffDate>((object) PXResult<PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult));
    ((PXSelectBase) this.ARDocumentList).Cache.Clear();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ARChargeInvoices.PayBillsFilter.showOverDueFor> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARChargeInvoices.PayBillsFilter.showOverDueFor>>) e).Cache.SetDefaultExt<ARChargeInvoices.PayBillsFilter.overDueFor>(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ARChargeInvoices.PayBillsFilter.showDueInLessThan> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARChargeInvoices.PayBillsFilter.showDueInLessThan>>) e).Cache.SetDefaultExt<ARChargeInvoices.PayBillsFilter.dueInLessThan>(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ARChargeInvoices.PayBillsFilter.showDiscountExparedWithinLast> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARChargeInvoices.PayBillsFilter.showDiscountExparedWithinLast>>) e).Cache.SetDefaultExt<ARChargeInvoices.PayBillsFilter.discountExparedWithinLast>(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<ARChargeInvoices.PayBillsFilter.showDiscountExpiresInLessThan> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<ARChargeInvoices.PayBillsFilter.showDiscountExpiresInLessThan>>) e).Cache.SetDefaultExt<ARChargeInvoices.PayBillsFilter.discountExpiresInLessThan>(e.Row);
  }

  protected virtual void CurrencyInfo_CuryEffDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    foreach (ARChargeInvoices.PayBillsFilter payBillsFilter in ((PXSelectBase) this.Filter).Cache.Inserted)
      e.NewValue = (object) payBillsFilter.PayDate;
  }

  protected virtual void ARInvoice_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXUIFieldAttribute.SetEnabled<ARInvoice.docType>(sender, e.Row, false);
    PXUIFieldAttribute.SetEnabled<ARInvoice.refNbr>(sender, e.Row, false);
  }

  protected PX.Objects.CA.CashAccount findDefaultCashAccount(ARInvoice aDoc)
  {
    PX.Objects.CA.CashAccount defaultCashAccount = (PX.Objects.CA.CashAccount) null;
    PXCache cache = ((PXSelectBase) this.arPayment).Cache;
    ARPayment arPayment1 = new ARPayment();
    arPayment1.DocType = "PMT";
    arPayment1.CustomerID = aDoc.CustomerID;
    arPayment1.CustomerLocationID = aDoc.CustomerLocationID;
    arPayment1.BranchID = aDoc.BranchID;
    arPayment1.PaymentMethodID = aDoc.PaymentMethodID;
    arPayment1.PMInstanceID = aDoc.PMInstanceID;
    ARPayment arPayment2 = arPayment1;
    object obj;
    ref object local = ref obj;
    cache.RaiseFieldDefaulting<ARPayment.cashAccountID>((object) arPayment2, ref local);
    int? nullable = obj as int?;
    if (nullable.HasValue)
      defaultCashAccount = PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Required<PX.Objects.CA.CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) nullable
      }));
    return defaultCashAccount;
  }

  [Serializable]
  public class PayBillsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _PayDate;
    protected string _PayFinPeriodID;
    protected short? _OverDueFor;
    protected bool? _ShowOverDueFor;
    protected short? _DueInLessThan;
    protected bool? _ShowDueInLessThan;
    protected short? _DiscountExparedWithinLast;
    protected bool? _ShowDiscountExparedWithinLast;
    protected short? _DiscountExpiresInLessThan;
    protected bool? _ShowDiscountExpiresInLessThan;
    protected string _CuryID;
    protected long? _CuryInfoID;

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? PayDate
    {
      get => this._PayDate;
      set => this._PayDate = value;
    }

    [AROpenPeriod(typeof (ARChargeInvoices.PayBillsFilter.payDate), null, null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null)]
    [PXUIField]
    public virtual string PayFinPeriodID
    {
      get => this._PayFinPeriodID;
      set => this._PayFinPeriodID = value;
    }

    [PXDBShort]
    [PXUIField]
    [PXDefault(0)]
    public virtual short? OverDueFor
    {
      get => this._OverDueFor;
      set => this._OverDueFor = value;
    }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField]
    public virtual bool? ShowOverDueFor
    {
      get => this._ShowOverDueFor;
      set => this._ShowOverDueFor = value;
    }

    [PXDBShort]
    [PXUIField]
    [PXDefault(typeof (IIf<Where<ARChargeInvoices.PayBillsFilter.showDueInLessThan, Equal<True>>, short7, short0>))]
    public virtual short? DueInLessThan
    {
      get => this._DueInLessThan;
      set => this._DueInLessThan = value;
    }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField]
    public virtual bool? ShowDueInLessThan
    {
      get => this._ShowDueInLessThan;
      set => this._ShowDueInLessThan = value;
    }

    [PXDBShort]
    [PXUIField]
    [PXDefault(0)]
    public virtual short? DiscountExparedWithinLast
    {
      get => this._DiscountExparedWithinLast;
      set => this._DiscountExparedWithinLast = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField]
    public virtual bool? ShowDiscountExparedWithinLast
    {
      get => this._ShowDiscountExparedWithinLast;
      set => this._ShowDiscountExparedWithinLast = value;
    }

    [PXDBShort]
    [PXUIField]
    [PXDefault(typeof (IIf<Where<ARChargeInvoices.PayBillsFilter.showDiscountExpiresInLessThan, Equal<True>>, short7, short0>))]
    public virtual short? DiscountExpiresInLessThan
    {
      get => this._DiscountExpiresInLessThan;
      set => this._DiscountExpiresInLessThan = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField]
    public virtual bool? ShowDiscountExpiresInLessThan
    {
      get => this._ShowDiscountExpiresInLessThan;
      set => this._ShowDiscountExpiresInLessThan = value;
    }

    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
    [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
    [PXUIField]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
    public virtual string CuryID
    {
      get => this._CuryID;
      set => this._CuryID = value;
    }

    [PXDBLong]
    [CurrencyInfo(ModuleCode = "AR")]
    public virtual long? CuryInfoID
    {
      get => this._CuryInfoID;
      set => this._CuryInfoID = value;
    }

    public abstract class payDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARChargeInvoices.PayBillsFilter.payDate>
    {
    }

    public abstract class payFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARChargeInvoices.PayBillsFilter.payFinPeriodID>
    {
    }

    public abstract class overDueFor : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      ARChargeInvoices.PayBillsFilter.overDueFor>
    {
    }

    public abstract class showOverDueFor : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARChargeInvoices.PayBillsFilter.showOverDueFor>
    {
    }

    public abstract class dueInLessThan : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      ARChargeInvoices.PayBillsFilter.dueInLessThan>
    {
    }

    public abstract class showDueInLessThan : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARChargeInvoices.PayBillsFilter.showDueInLessThan>
    {
    }

    public abstract class discountExparedWithinLast : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      ARChargeInvoices.PayBillsFilter.discountExparedWithinLast>
    {
    }

    public abstract class showDiscountExparedWithinLast : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARChargeInvoices.PayBillsFilter.showDiscountExparedWithinLast>
    {
    }

    public abstract class discountExpiresInLessThan : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      ARChargeInvoices.PayBillsFilter.discountExpiresInLessThan>
    {
    }

    public abstract class showDiscountExpiresInLessThan : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARChargeInvoices.PayBillsFilter.showDiscountExpiresInLessThan>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARChargeInvoices.PayBillsFilter.curyID>
    {
    }

    public abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      ARChargeInvoices.PayBillsFilter.curyInfoID>
    {
    }
  }
}
