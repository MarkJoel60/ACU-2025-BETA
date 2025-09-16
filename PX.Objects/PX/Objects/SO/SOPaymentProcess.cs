// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPaymentProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.AR.CCPaymentProcessing.Common;
using PX.Objects.AR.GraphExtensions;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.SO.DAC.Unbound;
using PX.Objects.SO.GraphExtensions;
using PX.Objects.SO.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO;

[TableAndChartDashboardType]
public class SOPaymentProcess : PXGraph<SOPaymentProcess>
{
  public PXCancel<SOPaymentProcessFilter> Cancel;
  public PXAction<SOPaymentProcessFilter> viewDocument;
  public PXAction<SOPaymentProcessFilter> viewRelatedDocument;
  public PXFilter<SOPaymentProcessFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<SOPaymentProcessResult, SOPaymentProcessFilter> Payments;

  public SOPaymentProcess()
  {
    ((PXGraph) this).Views.Caches.Remove(typeof (SOPaymentProcessFilter));
    ((PXGraph) this).Views.Caches.Remove(typeof (SOPaymentProcessResult));
  }

  public IEnumerable payments()
  {
    if (PXView.MaximumRows == 1)
    {
      object[] searches = PXView.Searches;
      if ((searches != null ? (searches.Length == 2 ? 1 : 0) : 0) != 0 && ((IEnumerable<object>) PXView.Searches).All<object>((Func<object, bool>) (f => f is string str && !string.IsNullOrEmpty(str))))
      {
        PXCache cache = ((PXSelectBase) this.Payments).Cache;
        SOPaymentProcessResult paymentProcessResult = new SOPaymentProcessResult();
        paymentProcessResult.DocType = (string) PXView.Searches[0];
        paymentProcessResult.RefNbr = (string) PXView.Searches[1];
        object obj = cache.Locate((object) paymentProcessResult);
        if (obj != null)
          return (IEnumerable) new object[1]{ obj };
      }
    }
    PXSelectBase<PX.Objects.AR.ARPayment> view = this.GetView();
    this.AddFilters(view, ((PXSelectBase<SOPaymentProcessFilter>) this.Filter).Current);
    bool filtersContainNonDbField;
    bool sortsContainNonDbField;
    this.VerifyNonDBFields(out filtersContainNonDbField, out sortsContainNonDbField);
    List<SOPaymentProcessResult> resultList = new List<SOPaymentProcessResult>();
    int startRow = filtersContainNonDbField | sortsContainNonDbField ? 0 : PXView.StartRow;
    int num = filtersContainNonDbField | sortsContainNonDbField ? -1 : PXView.MaximumRows;
    int totalRows = 0;
    using (new PXFieldScope(((PXSelectBase) view).View, this.GetViewFieldList(), true))
    {
      foreach (PXResult<PX.Objects.AR.ARPayment> row in ((PXSelectBase) view).View.Select(PXView.Currents, this.GetParameters(((PXSelectBase<SOPaymentProcessFilter>) this.Filter).Current), PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, num, ref totalRows))
      {
        SOPaymentProcessResult paymentProcessResult = this.CreateSOPaymentProcessResult(row);
        resultList.Add(paymentProcessResult);
      }
    }
    PXView.StartRow = 0;
    return (IEnumerable) this.CreateDelegateResult(resultList, totalRows, filtersContainNonDbField, sortsContainNonDbField);
  }

  protected virtual PXSelectBase<PX.Objects.AR.ARPayment> GetView()
  {
    return (PXSelectBase<PX.Objects.AR.ARPayment>) new PXSelectJoin<PX.Objects.AR.ARPayment, InnerJoin<PX.Objects.CA.PaymentMethod, On<PX.Objects.AR.ARPayment.paymentMethodID, Equal<PX.Objects.CA.PaymentMethod.paymentMethodID>>, InnerJoin<ARPaymentTotals, On<PX.Objects.AR.ARPayment.docType, Equal<ARPaymentTotals.docType>, And<PX.Objects.AR.ARPayment.refNbr, Equal<ARPaymentTotals.refNbr>>>, LeftJoin<SOOrder, On<ARPaymentTotals.adjdOrderType, Equal<SOOrder.orderType>, And<ARPaymentTotals.adjdOrderNbr, Equal<SOOrder.orderNbr>>>, LeftJoin<SOInvoice, On<ARPaymentTotals.adjdDocType, Equal<SOInvoice.docType>, And<ARPaymentTotals.adjdRefNbr, Equal<SOInvoice.refNbr>>>, LeftJoin<PX.Objects.AR.ARInvoice, On<ARPaymentTotals.adjdDocType, Equal<PX.Objects.AR.ARInvoice.docType>, And<ARPaymentTotals.adjdRefNbr, Equal<PX.Objects.AR.ARInvoice.refNbr>>>, LeftJoin<PX.Objects.AR.ExternalTransaction, On<PX.Objects.AR.ExternalTransaction.transactionID, Equal<PX.Objects.AR.ARPayment.cCActualExternalTransactionID>>, LeftJoin<SOAdjust, On<PX.Objects.AR.ARPayment.docType, Equal<SOAdjust.adjgDocType>, And<PX.Objects.AR.ARPayment.refNbr, Equal<SOAdjust.adjgRefNbr>, And<SOAdjust.adjdOrderType, Equal<ARPaymentTotals.adjdOrderType>, And<SOAdjust.adjdOrderNbr, Equal<ARPaymentTotals.adjdOrderNbr>>>>>, LeftJoin<CCProcessingCenter, On<PX.Objects.AR.ARPayment.processingCenterID, Equal<CCProcessingCenter.processingCenterID>>, LeftJoin<ARAdjust, On<PX.Objects.AR.ARPayment.docType, Equal<ARAdjust.adjgDocType>, And<PX.Objects.AR.ARPayment.refNbr, Equal<ARAdjust.adjgRefNbr>, And<ARAdjust.adjdDocType, Equal<ARPaymentTotals.adjdDocType>, And<ARAdjust.adjdRefNbr, Equal<ARPaymentTotals.adjdRefNbr>>>>>>>>>>>>>>, Where<PX.Objects.AR.ARPayment.isCCPayment, Equal<True>, And<PX.Objects.AR.ARPayment.pendingProcessing, Equal<True>, And<PX.Objects.AR.ARPayment.released, NotEqual<True>, And<PX.Objects.AR.ARPayment.voided, NotEqual<True>, And2<Where2<Where<ARPaymentTotals.invoiceCntr, Equal<Zero>, And<ARPaymentTotals.adjdOrderNbr, IsNotNull>>, Or2<Where<ARPaymentTotals.orderCntr, Equal<Zero>, And<ARPaymentTotals.adjdRefNbr, IsNotNull>>, Or<Where<ARPaymentTotals.adjdOrderType, Equal<SOInvoice.sOOrderType>, And<ARPaymentTotals.adjdOrderNbr, Equal<SOInvoice.sOOrderNbr>>>>>>, And<PX.Objects.AR.ARPayment.docType, In3<ARDocType.prepayment, ARDocType.payment>, And<PX.Objects.AR.ARPayment.hold, Equal<False>, And<PX.Objects.CA.PaymentMethod.isActive, Equal<True>, And<PX.Objects.CA.PaymentMethod.useForAR, Equal<True>, And2<Where2<Where<ARPaymentTotals.adjdRefNbr, IsNotNull, And<SOInvoice.released, NotEqual<True>>>, Or<Where<ARPaymentTotals.adjdRefNbr, IsNull, And<SOOrder.completed, NotEqual<True>>>>>, And<Where<ARPaymentTotals.adjdRefNbr, IsNull, Or<SOInvoice.refNbr, IsNotNull>>>>>>>>>>>>>>((PXGraph) this);
  }

  protected virtual void AddFilters(PXSelectBase<PX.Objects.AR.ARPayment> view, SOPaymentProcessFilter filter)
  {
    if (filter.CustomerID.HasValue)
      view.WhereAnd<Where<Current<SOPaymentProcessFilter.customerID>, Equal<PX.Objects.AR.ARPayment.customerID>>>();
    if (filter.StartDate.HasValue)
      view.WhereAnd<Where<Current<SOPaymentProcessFilter.startDate>, LessEqual<PX.Objects.AR.ARPayment.docDate>>>();
    if (filter.EndDate.HasValue)
      view.WhereAnd<Where<Current<SOPaymentProcessFilter.endDate>, GreaterEqual<PX.Objects.AR.ARPayment.docDate>>>();
    switch (filter.Action)
    {
      case "CaptureCCPayment":
        view.WhereAnd<Where<PX.Objects.AR.ARPayment.isCCAuthorized, Equal<True>, And<PX.Objects.AR.ARPayment.isCCCaptureFailed, NotEqual<True>>>>();
        break;
      case "ValidateCCPayment":
        view.WhereAnd<Where<PX.Objects.AR.ARPayment.syncLock, Equal<True>, Or<PX.Objects.AR.ExternalTransaction.transactionID, IsNull, Or<PX.Objects.AR.ExternalTransaction.procStatus, In3<ExtTransactionProcStatusCode.authorizeHeldForReview, ExtTransactionProcStatusCode.captureHeldForReview, ExtTransactionProcStatusCode.creditHeldForReview, ExtTransactionProcStatusCode.voidHeldForReview>, Or<Where<PX.Objects.AR.ARPayment.isCCAuthorized, Equal<True>, And<PX.Objects.AR.ExternalTransaction.fundHoldExpDate, Less<Required<PX.Objects.AR.ExternalTransaction.fundHoldExpDate>>>>>>>>>();
        break;
      case "VoidExpiredCCPayment":
        view.WhereAnd<Where<PX.Objects.AR.ARPayment.isCCAuthorized, Equal<True>, And<PX.Objects.AR.ExternalTransaction.fundHoldExpDate, Less<Required<PX.Objects.AR.ExternalTransaction.fundHoldExpDate>>, And<PX.Objects.AR.ARPayment.pMInstanceID, IsNotNull, And<CCProcessingCenter.isExternalAuthorizationOnly, NotEqual<True>, And<CCProcessingCenter.reauthRetryNbr, Greater<Zero>>>>>>>();
        break;
      case "ReAuthorizeCCPayment":
        view.WhereAnd<Where<PX.Objects.AR.ARPayment.isCCAuthorized, NotEqual<True>, And<PX.Objects.AR.ARPayment.isCCCaptured, NotEqual<True>, And<PX.Objects.AR.ARPayment.pMInstanceID, IsNotNull, And<PX.Objects.AR.ARPayment.cCReauthDate, Less<Required<PX.Objects.AR.ARPayment.cCReauthDate>>, And<PX.Objects.AR.ARPayment.cCReauthTriesLeft, Greater<Zero>>>>>>>();
        break;
      case "IncreaseCCPayment":
        view.WhereAnd<Where<PX.Objects.AR.ARPayment.isCCAuthorized, Equal<True>, And<PX.Objects.AR.ARPayment.isCCCaptured, NotEqual<True>, And<CCProcessingCenter.allowAuthorizedIncrement, Equal<True>, And2<Where<Add<IsNull<SOOrder.authorizedPaymentCntr, Zero>, IsNull<PX.Objects.AR.ARInvoice.authorizedPaymentCntr, Zero>>, Equal<One>>, And<Where<SOOrder.curyUnpaidBalance, Greater<decimal0>, And<ARPaymentTotals.invoiceCntr, Equal<Zero>, Or<PX.Objects.AR.ARInvoice.curyUnpaidBalance, Greater<decimal0>>>>>>>>>>();
        break;
      default:
        view.WhereAnd<Where<True, Equal<False>>>();
        break;
    }
  }

  protected virtual object[] GetParameters(SOPaymentProcessFilter filter)
  {
    List<object> objectList = new List<object>();
    if (EnumerableExtensions.IsIn<string>(filter.Action, "ValidateCCPayment", "VoidExpiredCCPayment", "ReAuthorizeCCPayment"))
      objectList.Add((object) PXTimeZoneInfo.Now);
    return objectList.ToArray();
  }

  protected virtual SOPaymentProcessResult CreateSOPaymentProcessResult(PXResult<PX.Objects.AR.ARPayment> row)
  {
    SOPaymentProcessResult paymentProcessResult1 = PropertyTransfer.Transfer<PX.Objects.AR.ARPayment, SOPaymentProcessResult>(((PXResult) row).GetItem<PX.Objects.AR.ARPayment>(), new SOPaymentProcessResult());
    PX.Objects.AR.ExternalTransaction externalTransaction = ((PXResult) row).GetItem<PX.Objects.AR.ExternalTransaction>();
    SOOrder soOrder = ((PXResult) row).GetItem<SOOrder>();
    PX.Objects.AR.ARInvoice arInvoice = ((PXResult) row).GetItem<PX.Objects.AR.ARInvoice>();
    ARAdjust arAdjust = ((PXResult) row).GetItem<ARAdjust>();
    SOAdjust soAdjust = ((PXResult) row).GetItem<SOAdjust>();
    CCProcessingCenter processingCenter = ((PXResult) row).GetItem<CCProcessingCenter>();
    bool flag = arInvoice?.RefNbr != null;
    paymentProcessResult1.RelatedDocument = flag ? "Invoice" : "SalesOrder";
    paymentProcessResult1.RelatedDocumentNumber = flag ? arInvoice.RefNbr : soOrder.OrderNbr;
    paymentProcessResult1.RelatedDocumentType = flag ? arInvoice.DocType : soOrder.OrderType;
    MultiDocumentStatusListAttribute statusListAttribute = ((PXSelectBase) this.Payments).Cache.GetAttributesReadonly<SOPaymentProcessResult.relatedDocumentStatus>().OfType<MultiDocumentStatusListAttribute>().FirstOrDefault<MultiDocumentStatusListAttribute>();
    paymentProcessResult1.RelatedDocumentStatus = flag ? statusListAttribute?.GetDocumentStatusValue(((PXGraph) this).Caches[typeof (SOInvoice)], arInvoice.Status) : statusListAttribute?.GetDocumentStatusValue(((PXGraph) this).Caches[typeof (SOOrder)], soOrder.Status);
    paymentProcessResult1.RelatedDocumentCreditTerms = flag ? arInvoice.TermsID : soOrder.TermsID;
    paymentProcessResult1.RelatedDocumentCuryInfoID = flag ? arAdjust.AdjgCuryInfoID : soAdjust.AdjgCuryInfoID;
    paymentProcessResult1.RelatedDocumentCuryID = flag ? arInvoice.CuryID : soOrder.CuryID;
    paymentProcessResult1.RelatedTranProcessingStatus = externalTransaction?.ProcStatus;
    paymentProcessResult1.FundHoldExpDate = (DateTime?) externalTransaction?.FundHoldExpDate;
    SOPaymentProcessResult paymentProcessResult2 = paymentProcessResult1;
    int? nullable1 = (int?) arInvoice?.AuthorizedPaymentCntr;
    int valueOrDefault1 = nullable1.GetValueOrDefault();
    int? nullable2;
    if (soOrder == null)
    {
      nullable1 = new int?();
      nullable2 = nullable1;
    }
    else
      nullable2 = soOrder.AuthorizedPaymentCntr;
    nullable1 = nullable2;
    int valueOrDefault2 = nullable1.GetValueOrDefault();
    int? nullable3 = new int?(valueOrDefault1 + valueOrDefault2);
    paymentProcessResult2.RelatedDocumentPaymentCntr = nullable3;
    paymentProcessResult1.CuryRelatedDocumentAppliedAmount = flag ? arAdjust.CuryAdjgAmt : soAdjust.CuryAdjgAmt;
    SOPaymentProcessResult paymentProcessResult3 = (SOPaymentProcessResult) ((PXSelectBase) this.Payments).Cache.Locate((object) paymentProcessResult1);
    if (paymentProcessResult3 == null)
    {
      GraphHelper.Hold(((PXSelectBase) this.Payments).Cache, (object) paymentProcessResult1);
    }
    else
    {
      paymentProcessResult1.Selected = paymentProcessResult3.Selected;
      paymentProcessResult1.ErrorDescription = paymentProcessResult3.ErrorDescription;
    }
    paymentProcessResult1.CuryRelatedDocumentUnpaidAmount = flag ? arInvoice.CuryUnpaidBalance : soOrder.CuryUnpaidBalance;
    Decimal? documentUnpaidAmount1 = paymentProcessResult1.CuryRelatedDocumentUnpaidAmount;
    Decimal num1 = 0M;
    if (documentUnpaidAmount1.GetValueOrDefault() > num1 & documentUnpaidAmount1.HasValue && processingCenter != null && processingCenter.AllowAuthorizedIncrement.GetValueOrDefault())
    {
      nullable1 = paymentProcessResult1.RelatedDocumentPaymentCntr;
      if (nullable1.GetValueOrDefault() == 1)
      {
        paymentProcessResult1.RelatedDocumentUnpaidAmount = flag ? arInvoice.UnpaidBalance : soOrder.UnpaidBalance;
        Decimal? nullable4;
        if (paymentProcessResult1.CuryID != paymentProcessResult1.RelatedDocumentCuryID)
        {
          SOPaymentProcessResult paymentProcessResult4 = paymentProcessResult1;
          Decimal? docBal = paymentProcessResult1.DocBal;
          Decimal? documentUnpaidAmount2 = paymentProcessResult1.RelatedDocumentUnpaidAmount;
          Decimal? nullable5 = docBal.HasValue & documentUnpaidAmount2.HasValue ? new Decimal?(docBal.GetValueOrDefault() + documentUnpaidAmount2.GetValueOrDefault()) : new Decimal?();
          paymentProcessResult4.IncreasedAuthorizedAmount = nullable5;
          PXCache cache = ((PXSelectBase) this.Filter).Cache;
          SOPaymentProcessResult row1 = paymentProcessResult1;
          nullable4 = paymentProcessResult1.IncreasedAuthorizedAmount;
          Decimal valueOrDefault3 = nullable4.GetValueOrDefault();
          Decimal num2;
          ref Decimal local = ref num2;
          PXCurrencyAttribute.CuryConvCury<PX.Objects.AR.ARPayment.curyInfoID>(cache, (object) row1, valueOrDefault3, out local);
          paymentProcessResult1.CuryIncreasedAuthorizedAmount = new Decimal?(num2);
        }
        else
        {
          SOPaymentProcessResult paymentProcessResult5 = paymentProcessResult1;
          Decimal? curyDocBal = paymentProcessResult1.CuryDocBal;
          Decimal? documentUnpaidAmount3 = paymentProcessResult1.CuryRelatedDocumentUnpaidAmount;
          Decimal? nullable6 = curyDocBal.HasValue & documentUnpaidAmount3.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() + documentUnpaidAmount3.GetValueOrDefault()) : new Decimal?();
          paymentProcessResult5.CuryIncreasedAuthorizedAmount = nullable6;
        }
        SOPaymentProcessResult paymentProcessResult6 = paymentProcessResult1;
        Decimal? documentAppliedAmount = paymentProcessResult1.CuryRelatedDocumentAppliedAmount;
        Decimal? authorizedAmount = paymentProcessResult1.CuryIncreasedAuthorizedAmount;
        Decimal? nullable7 = documentAppliedAmount.HasValue & authorizedAmount.HasValue ? new Decimal?(documentAppliedAmount.GetValueOrDefault() + authorizedAmount.GetValueOrDefault()) : new Decimal?();
        nullable4 = paymentProcessResult1.CuryDocBal;
        Decimal? nullable8 = nullable7.HasValue & nullable4.HasValue ? new Decimal?(nullable7.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
        paymentProcessResult6.CuryIncreasedAppliedAmount = nullable8;
      }
    }
    return paymentProcessResult1;
  }

  protected virtual void VerifyNonDBFields(
    out bool filtersContainNonDbField,
    out bool sortsContainNonDbField)
  {
    filtersContainNonDbField = PXView.Filters != null && ((IEnumerable<PXFilterRow>) PXView.PXFilterRowCollection.op_Implicit(PXView.Filters)).Any<PXFilterRow>((Func<PXFilterRow, bool>) (f =>
    {
      bool? nullable1;
      if (f != null)
      {
        string dataField = f.DataField;
        nullable1 = dataField != null ? new bool?(Str.Contains(dataField, "RelatedDocument", StringComparison.OrdinalIgnoreCase)) : new bool?();
        if (nullable1.GetValueOrDefault())
          goto label_18;
      }
      if (f != null)
      {
        string dataField = f.DataField;
        bool? nullable2;
        if (dataField == null)
        {
          nullable1 = new bool?();
          nullable2 = nullable1;
        }
        else
          nullable2 = new bool?(dataField.Contains("Selected"));
        nullable1 = nullable2;
        if (nullable1.GetValueOrDefault())
          goto label_18;
      }
      if (f != null)
      {
        string dataField = f.DataField;
        bool? nullable3;
        if (dataField == null)
        {
          nullable1 = new bool?();
          nullable3 = nullable1;
        }
        else
          nullable3 = new bool?(dataField.Contains("ErrorDescription"));
        nullable1 = nullable3;
        if (nullable1.GetValueOrDefault())
          goto label_18;
      }
      if (f == null)
        return false;
      string dataField1 = f.DataField;
      bool? nullable4;
      if (dataField1 == null)
      {
        nullable1 = new bool?();
        nullable4 = nullable1;
      }
      else
        nullable4 = new bool?(dataField1.Contains("RelatedTranProcessingStatus"));
      nullable1 = nullable4;
      return nullable1.GetValueOrDefault();
label_18:
      return true;
    }));
    ref bool local = ref sortsContainNonDbField;
    string[] sortColumns = PXView.SortColumns;
    int num = sortColumns != null ? (((IEnumerable<string>) sortColumns).Any<string>((Func<string, bool>) (c =>
    {
      if (c != null && Str.Contains(c, "RelatedDocument", StringComparison.OrdinalIgnoreCase) || c != null && c.Contains("Selected") || c != null && c.Contains("ErrorDescription"))
        return true;
      return c != null && c.Contains("RelatedTranProcessingStatus");
    })) ? 1 : 0) : 0;
    local = num != 0;
  }

  protected virtual IEnumerable<Type> GetViewFieldList()
  {
    return (IEnumerable<Type>) new Type[26]
    {
      typeof (PX.Objects.AR.ARPayment),
      typeof (PX.Objects.AR.ExternalTransaction.procStatus),
      typeof (PX.Objects.AR.ExternalTransaction.fundHoldExpDate),
      typeof (SOOrder.orderType),
      typeof (SOOrder.orderNbr),
      typeof (SOOrder.status),
      typeof (SOOrder.termsID),
      typeof (SOOrder.curyUnpaidBalance),
      typeof (SOOrder.unpaidBalance),
      typeof (SOOrder.curyID),
      typeof (SOOrder.authorizedPaymentCntr),
      typeof (PX.Objects.AR.ARInvoice.docType),
      typeof (PX.Objects.AR.ARInvoice.refNbr),
      typeof (PX.Objects.AR.ARInvoice.status),
      typeof (PX.Objects.AR.ARInvoice.termsID),
      typeof (PX.Objects.AR.ARInvoice.curyUnpaidBalance),
      typeof (PX.Objects.AR.ARInvoice.unpaidBalance),
      typeof (PX.Objects.AR.ARInvoice.curyID),
      typeof (PX.Objects.AR.ARInvoice.authorizedPaymentCntr),
      typeof (ARAdjust.adjgCuryInfoID),
      typeof (ARAdjust.curyAdjgAmt),
      typeof (ARAdjust.curyAdjgAmt),
      typeof (SOAdjust.adjgCuryInfoID),
      typeof (SOAdjust.curyAdjgAmt),
      typeof (SOAdjust.curyAdjgAmt),
      typeof (CCProcessingCenter.allowAuthorizedIncrement)
    };
  }

  protected virtual PXDelegateResult CreateDelegateResult(
    List<SOPaymentProcessResult> resultList,
    int totalRows,
    bool filtersContainNonDBField,
    bool sortsContainNonDBField)
  {
    PXDelegateResult delegateResult = new PXDelegateResult();
    delegateResult.IsResultFiltered = !filtersContainNonDBField;
    delegateResult.IsResultTruncated = totalRows > resultList.Count;
    if (!sortsContainNonDBField)
    {
      delegateResult.IsResultSorted = true;
      if (!PXView.ReverseOrder)
      {
        ((List<object>) delegateResult).AddRange((IEnumerable<object>) resultList);
      }
      else
      {
        IEnumerable source = PXView.Sort((IEnumerable) resultList);
        ((List<object>) delegateResult).AddRange((IEnumerable<object>) source.Cast<SOPaymentProcessResult>());
      }
    }
    else
      ((List<object>) delegateResult).AddRange((IEnumerable<object>) resultList);
    return delegateResult;
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    SOPaymentProcessResult current = ((PXSelectBase<SOPaymentProcessResult>) this.Payments).Current;
    if (current == null)
      return adapter.Get();
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current = PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Search<PX.Objects.AR.ARPayment.refNbr>((object) current.RefNbr, new object[1]
    {
      (object) current.DocType
    }));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Document");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewRelatedDocument(PXAdapter adapter)
  {
    SOPaymentProcessResult current = ((PXSelectBase<SOPaymentProcessResult>) this.Payments).Current;
    if (current == null)
      return adapter.Get();
    PXGraph pxGraph;
    if (current.RelatedDocument == "Invoice")
    {
      SOInvoiceEntry instance = PXGraph.CreateInstance<SOInvoiceEntry>();
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Search<SOInvoice.refNbr>((object) current.RelatedDocumentNumber, new object[1]
      {
        (object) current.RelatedDocumentType
      }));
      pxGraph = (PXGraph) instance;
    }
    else
    {
      SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
      ((PXSelectBase<SOOrder>) instance.Document).Current = PXResultset<SOOrder>.op_Implicit(((PXSelectBase<SOOrder>) instance.Document).Search<SOOrder.orderNbr>((object) current.RelatedDocumentNumber, new object[1]
      {
        (object) current.RelatedDocumentType
      }));
      pxGraph = (PXGraph) instance;
    }
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException(pxGraph, true, "View Document");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<SOPaymentProcessFilter.action> eventArgs)
  {
    ((PXSelectBase) this.Payments).Cache.Clear();
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<SOPaymentProcessFilter> eventArgs)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SOPaymentProcess.\u003C\u003Ec__DisplayClass17_0 cDisplayClass170 = new SOPaymentProcess.\u003C\u003Ec__DisplayClass17_0();
    PXUIFieldAttribute.SetVisible<SOPaymentProcessFilter.increaseBeforeCapture>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOPaymentProcessFilter>>) eventArgs).Cache, (object) null, ((PXSelectBase<SOPaymentProcessFilter>) this.Filter).Current?.Action == "CaptureCCPayment");
    SOPaymentProcessFilter current = ((PXSelectBase<SOPaymentProcessFilter>) this.Filter).Current;
    PXCache cache = ((PXSelectBase) this.Filter).Cache;
    if (current == null)
      return;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass170.action = current.Action;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass170.action == "CaptureCCPayment" && current.IncreaseBeforeCapture.GetValueOrDefault())
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass170.action = "IncreaseAndCaptureCCPayment";
    }
    // ISSUE: method pointer
    ((PXProcessingBase<SOPaymentProcessResult>) this.Payments).SetProcessDelegate(new PXProcessingBase<SOPaymentProcessResult>.ProcessListDelegate((object) cDisplayClass170, __methodptr(\u003C_\u003Eb__0)));
    this.ShowTranWillBeReauthorizedWarning(cache, current);
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<SOPaymentProcessResult> eventArgs)
  {
    if (((PXSelectBase<SOPaymentProcessFilter>) this.Filter).Current == null)
      return;
    PXUIFieldAttribute.SetVisible<SOPaymentProcessResult.fundHoldExpDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOPaymentProcessResult>>) eventArgs).Cache, (object) null, ((PXSelectBase<SOPaymentProcessFilter>) this.Filter).Current.Action == "VoidExpiredCCPayment");
    bool flag = ((PXSelectBase<SOPaymentProcessFilter>) this.Filter).Current.Action == "IncreaseCCPayment" || ((PXSelectBase<SOPaymentProcessFilter>) this.Filter).Current.Action == "CaptureCCPayment" && ((PXSelectBase<SOPaymentProcessFilter>) this.Filter).Current.IncreaseBeforeCapture.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<SOPaymentProcessResult.curyIncreasedAuthorizedAmount>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOPaymentProcessResult>>) eventArgs).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<SOPaymentProcessResult.curyIncreasedAppliedAmount>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOPaymentProcessResult>>) eventArgs).Cache, (object) null, flag);
  }

  protected virtual void ProcessPayments(List<SOPaymentProcessResult> payments, string action)
  {
    SOOrderEntry entry1 = (SOOrderEntry) null;
    SOInvoiceEntry entry2 = (SOInvoiceEntry) null;
    ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
    CreatePaymentExtBase<SOOrderEntry, SOOrder, SOAdjust> paymentExt1 = (CreatePaymentExtBase<SOOrderEntry, SOOrder, SOAdjust>) null;
    CreatePaymentExtBase<SOInvoiceEntry, ARInvoiceEntry, PX.Objects.AR.ARInvoice, ARAdjust2, ARAdjust> paymentExt2 = (CreatePaymentExtBase<SOInvoiceEntry, ARInvoiceEntry, PX.Objects.AR.ARInvoice, ARAdjust2, ARAdjust>) null;
    ARPaymentEntryPaymentTransaction extension = ((PXGraph) instance).GetExtension<ARPaymentEntryPaymentTransaction>();
    int num = 0;
    foreach (SOPaymentProcessResult payment in payments)
    {
      try
      {
        string action1 = action;
        if (action == "IncreaseAndCaptureCCPayment" && !payment.CuryIncreasedAuthorizedAmount.HasValue)
          action1 = "CaptureCCPayment";
        if (payment.RelatedDocument == "Invoice")
        {
          if (entry2 == null)
          {
            entry2 = PXGraph.CreateInstance<SOInvoiceEntry>();
            paymentExt2 = ((PXGraph) entry2).FindImplementation<CreatePaymentExtBase<SOInvoiceEntry, ARInvoiceEntry, PX.Objects.AR.ARInvoice, ARAdjust2, ARAdjust>>();
          }
          this.ProcessInvoice(action1, entry2, paymentExt2, instance, extension, payment);
        }
        else
        {
          if (entry1 == null)
          {
            entry1 = PXGraph.CreateInstance<SOOrderEntry>();
            paymentExt1 = ((PXGraph) entry1).FindImplementation<CreatePaymentExtBase<SOOrderEntry, SOOrder, SOAdjust>>();
          }
          this.ProcessOrder(action1, entry1, paymentExt1, instance, extension, payment);
        }
      }
      catch (Exception ex)
      {
        PXProcessing<SOPaymentProcessResult>.SetError(num, ex);
      }
      finally
      {
        payment.ErrorDescription = this.GetErrorDescription(instance, extension, payment);
        ++num;
      }
    }
  }

  private void InitPaymentEntry(ARPaymentEntry paymentEntry, SOPaymentProcessResult paymentResult)
  {
    ((PXGraph) paymentEntry).Clear();
    ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current = PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Search<PX.Objects.AR.ARPayment.refNbr>((object) paymentResult.RefNbr, new object[1]
    {
      (object) paymentResult.DocType
    }));
    PX.Objects.Extensions.PaymentTransaction.Payment extension = PXCacheEx.GetExtension<PX.Objects.Extensions.PaymentTransaction.Payment>((IBqlTable) ((PXSelectBase<PX.Objects.AR.ARPayment>) paymentEntry.Document).Current);
    if (extension == null || !paymentResult.CuryIncreasedAuthorizedAmount.HasValue)
      return;
    extension.CuryDocBalIncrease = paymentResult.CuryIncreasedAuthorizedAmount;
    extension.TransactionOrigDocType = paymentResult.RelatedDocumentType;
    extension.TransactionOrigDocRefNbr = paymentResult.RelatedDocumentNumber;
    extension.OrigDocAppliedAmount = paymentResult.CuryIncreasedAppliedAmount;
  }

  private void InitPaymentTransaction(
    ARPaymentEntryPaymentTransaction paymentTransaction,
    SOPaymentProcessResult paymentResult)
  {
    if (!paymentResult.CuryIncreasedAuthorizedAmount.HasValue)
      return;
    PX.Objects.Extensions.PaymentTransaction.Payment current = ((PXSelectBase<PX.Objects.Extensions.PaymentTransaction.Payment>) paymentTransaction.PaymentDoc).Current;
    current.CuryDocBalIncrease = paymentResult.CuryIncreasedAuthorizedAmount;
    current.TransactionOrigDocType = paymentResult.RelatedDocumentType;
    current.TransactionOrigDocRefNbr = paymentResult.RelatedDocumentNumber;
    current.OrigDocAppliedAmount = paymentResult.CuryIncreasedAppliedAmount;
  }

  protected virtual void ProcessInvoice(
    string action,
    SOInvoiceEntry entry,
    CreatePaymentExtBase<SOInvoiceEntry, ARInvoiceEntry, PX.Objects.AR.ARInvoice, ARAdjust2, ARAdjust> paymentExt,
    ARPaymentEntry paymentEntry,
    ARPaymentEntryPaymentTransaction paymentTransactionExt,
    SOPaymentProcessResult payment)
  {
    ((PXGraph) entry).Clear();
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) entry.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) entry.Document).Search<SOInvoice.refNbr>((object) payment.RelatedDocumentNumber, new object[1]
    {
      (object) payment.RelatedDocumentType
    }));
    this.InitPaymentEntry(paymentEntry, payment);
    this.InitPaymentTransaction(paymentTransactionExt, payment);
    IEnumerable<ARAdjust> source1 = ((IEnumerable<ARAdjust>) ((PXSelectBase<ARAdjust>) paymentEntry.Adjustments).SelectMain(Array.Empty<object>())).Where<ARAdjust>((Func<ARAdjust, bool>) (a =>
    {
      Decimal? adjAmt = a.AdjAmt;
      Decimal num = 0M;
      return adjAmt.GetValueOrDefault() > num & adjAmt.HasValue;
    }));
    ARAdjust source2 = source1.FirstOrDefault<ARAdjust>();
    if (source1.Count<ARAdjust>() != 1 || source2.AdjdRefNbr != payment.RelatedDocumentNumber || source2.AdjdDocType != payment.RelatedDocumentType)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<PX.Objects.AR.ARPayment>((PXGraph) entry), new object[2]
      {
        (object) payment.DocType,
        (object) payment.RefNbr
      });
    ARAdjust2 adjustment = PropertyTransfer.Transfer<ARAdjust, ARAdjust2>(source2, new ARAdjust2());
    this.RunAction<SOInvoiceEntry, ARInvoiceEntry, PX.Objects.AR.ARInvoice, ARAdjust2, ARAdjust>(action, paymentEntry, paymentTransactionExt, adjustment, paymentExt);
  }

  protected virtual void ProcessOrder(
    string action,
    SOOrderEntry entry,
    CreatePaymentExtBase<SOOrderEntry, SOOrder, SOAdjust> paymentExt,
    ARPaymentEntry paymentEntry,
    ARPaymentEntryPaymentTransaction paymentTransactionExt,
    SOPaymentProcessResult payment)
  {
    ((PXGraph) entry).Clear();
    ((PXSelectBase<SOOrder>) entry.Document).Current = PXResultset<SOOrder>.op_Implicit(((PXSelectBase<SOOrder>) entry.Document).Search<SOOrder.orderNbr>((object) payment.RelatedDocumentNumber, new object[1]
    {
      (object) payment.RelatedDocumentType
    }));
    this.InitPaymentEntry(paymentEntry, payment);
    this.InitPaymentTransaction(paymentTransactionExt, payment);
    IEnumerable<SOAdjust> source = ((IEnumerable<SOAdjust>) ((PXSelectBase<SOAdjust>) paymentEntry.GetOrdersToApplyTabExtension(true).SOAdjustments).SelectMain(Array.Empty<object>())).Where<SOAdjust>((Func<SOAdjust, bool>) (a =>
    {
      Decimal? adjAmt = a.AdjAmt;
      Decimal num = 0M;
      return adjAmt.GetValueOrDefault() > num & adjAmt.HasValue;
    }));
    SOAdjust adjustment = source.FirstOrDefault<SOAdjust>();
    if (source.Count<SOAdjust>() != 1 || adjustment.AdjdOrderNbr != payment.RelatedDocumentNumber || adjustment.AdjdOrderType != payment.RelatedDocumentType)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<PX.Objects.AR.ARPayment>((PXGraph) entry), new object[2]
      {
        (object) payment.DocType,
        (object) payment.RefNbr
      });
    this.RunAction<SOOrderEntry, SOOrderEntry, SOOrder, SOAdjust, SOAdjust>(action, paymentEntry, paymentTransactionExt, adjustment, (CreatePaymentExtBase<SOOrderEntry, SOOrderEntry, SOOrder, SOAdjust, SOAdjust>) paymentExt);
  }

  protected virtual void RunAction<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust>(
    string action,
    ARPaymentEntry paymentEntry,
    ARPaymentEntryPaymentTransaction paymentTransactionExt,
    TDocumentAdjust adjustment,
    CreatePaymentExtBase<TGraph, TFirstGraph, TDocument, TDocumentAdjust, TPaymentAdjust> paymentExt)
    where TGraph : PXGraph<TFirstGraph, TDocument>, new()
    where TFirstGraph : PXGraph
    where TDocument : class, IBqlTable, ICreatePaymentDocument, new()
    where TDocumentAdjust : class, IBqlTable, ICreatePaymentAdjust, IAdjustment, new()
    where TPaymentAdjust : class, IBqlTable, new()
  {
    switch (action)
    {
      case "CaptureCCPayment":
        paymentExt.CapturePayment(adjustment, paymentEntry, paymentTransactionExt);
        break;
      case "VoidExpiredCCPayment":
        paymentExt.VoidCCTransactionForReAuthorization(adjustment, paymentEntry, paymentTransactionExt);
        break;
      case "ValidateCCPayment":
        paymentExt.ValidatePayment(adjustment, paymentEntry, paymentTransactionExt);
        break;
      case "ReAuthorizeCCPayment":
        paymentExt.AuthorizePayment(adjustment, paymentEntry, paymentTransactionExt);
        break;
      case "IncreaseCCPayment":
        paymentExt.IncreasePayment(adjustment, paymentEntry, paymentTransactionExt, false);
        break;
      case "IncreaseAndCaptureCCPayment":
        paymentExt.IncreasePayment(adjustment, paymentEntry, paymentTransactionExt, true);
        break;
      default:
        throw new NotImplementedException();
    }
  }

  protected virtual string GetErrorDescription(
    ARPaymentEntry paymentEntry,
    ARPaymentEntryPaymentTransaction paymentTransactionExt,
    SOPaymentProcessResult payment)
  {
    try
    {
      ExternalTransactionState transactionState = paymentTransactionExt.GetActiveTransactionState();
      int? transactionId = (int?) transactionState?.ExternalTransaction?.TransactionID;
      if ((transactionState != null ? (transactionState.HasErrors ? 1 : 0) : 0) == 0 || !transactionId.HasValue)
        return (string) null;
      return ((PXSelectBase<CCProcTran>) new PXSelect<CCProcTran, Where<CCProcTran.transactionID, Equal<Required<CCProcTran.transactionID>>>, OrderBy<Desc<CCProcTran.tranNbr>>>((PXGraph) paymentEntry)).SelectSingle(new object[1]
      {
        (object) transactionId
      })?.ErrorText;
    }
    catch (Exception ex)
    {
      PXTrace.WriteError(ex);
      return ex.Message;
    }
  }

  private void ShowTranWillBeReauthorizedWarning(PXCache cache, SOPaymentProcessFilter filter)
  {
    string action = filter.Action;
    if (action == "VoidExpiredCCPayment")
      cache.RaiseExceptionHandling<SOPaymentProcessFilter.action>((object) filter, (object) action, (Exception) new PXSetPropertyException("The selected transaction will be reauthorized automatically after voiding if Reauthorization Retry Delay (Hours) is set to 0 for the processing center on the Processing Centers tab of the Payment Methods (CA204000) form.", (PXErrorLevel) 2));
    else
      cache.RaiseExceptionHandling<SOPaymentProcessFilter.action>((object) filter, (object) action, (Exception) null);
  }
}
