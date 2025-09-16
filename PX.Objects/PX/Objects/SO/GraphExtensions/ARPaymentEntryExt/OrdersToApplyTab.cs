// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.ARPaymentEntryExt.OrdersToApplyTab
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.AR.MigrationMode;
using PX.Objects.Common;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.ARPaymentEntryExt;

public class OrdersToApplyTab : PXGraphExtension<ARPaymentEntry.MultiCurrency, ARPaymentEntry>
{
  [PXViewName("Orders to Apply")]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<SOAdjust, LeftJoin<PX.Objects.SO.SOOrder, On<PX.Objects.SO.SOOrder.orderType, Equal<SOAdjust.adjdOrderType>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<SOAdjust.adjdOrderNbr>>>>, Where<SOAdjust.adjgDocType, Equal<Current<ARPayment.docType>>, And<SOAdjust.adjgRefNbr, Equal<Current<ARPayment.refNbr>>>>> SOAdjustments;
  public PXSelectJoin<SOAdjust, InnerJoin<PX.Objects.SO.SOOrder, On<PX.Objects.SO.SOOrder.orderType, Equal<SOAdjust.adjdOrderType>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<SOAdjust.adjdOrderNbr>>>, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<PX.Objects.SO.SOOrder.curyInfoID>>>>, Where<SOAdjust.adjgDocType, Equal<Current<ARPayment.docType>>, And<SOAdjust.adjgRefNbr, Equal<Current<ARPayment.refNbr>>>>> SOAdjustments_Orders;
  public PXSelect<SOAdjust, Where<SOAdjust.adjgDocType, Equal<Current<ARPayment.docType>>, And<SOAdjust.adjgRefNbr, Equal<Current<ARPayment.refNbr>>>>> SOAdjustments_Raw;
  public PXSelect<PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.customerID, Equal<Required<PX.Objects.SO.SOOrder.customerID>>, And<PX.Objects.SO.SOOrder.orderType, Equal<Required<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<Required<PX.Objects.SO.SOOrder.orderNbr>>>>>> SOOrder_CustomerID_OrderType_RefNbr;
  public PXAction<ARPayment> loadOrders;
  public PXAction<ARPayment> viewSODocumentToApply;

  public ARPaymentEntry.ARPaymentSOBalanceCalculator SOBalanceCalculator
  {
    get
    {
      return ((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base).FindImplementation<ARPaymentEntry.ARPaymentSOBalanceCalculator>();
    }
  }

  [PXUIField]
  [PXButton(ImageKey = "Refresh")]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable LoadOrders(PXAdapter adapter)
  {
    if (((PXGraphExtension<ARPaymentEntry>) this).Base.loadOpts != null && ((PXSelectBase<ARPaymentEntry.LoadOptions>) ((PXGraphExtension<ARPaymentEntry>) this).Base.loadOpts).Current != null)
      ((PXSelectBase<ARPaymentEntry.LoadOptions>) ((PXGraphExtension<ARPaymentEntry>) this).Base.loadOpts).Current.IsInvoice = new bool?(false);
    WebDialogResult webDialogResult = ((PXSelectBase<ARPaymentEntry.LoadOptions>) ((PXGraphExtension<ARPaymentEntry>) this).Base.loadOpts).AskExt();
    if (webDialogResult == 1 || webDialogResult == 6)
      this.LoadOrdersProc(false, ((PXSelectBase<ARPaymentEntry.LoadOptions>) ((PXGraphExtension<ARPaymentEntry>) this).Base.loadOpts).Current);
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewSODocumentToApply(PXAdapter adapter)
  {
    SOAdjust current = ((PXSelectBase<SOAdjust>) this.SOAdjustments).Current;
    if (current != null && !string.IsNullOrEmpty(current.AdjdOrderType) && !string.IsNullOrEmpty(current.AdjdOrderNbr))
    {
      SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
      ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) current.AdjdOrderNbr, new object[1]
      {
        (object) current.AdjdOrderType
      }));
      if (((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current != null)
      {
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Document");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  [PXDBLong]
  protected virtual void SOOrder_CuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXUnboundDefaultAttribute))]
  protected virtual void _(PX.Data.Events.CacheAttached<ARPayment.curySOApplAmt> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<ARPayment> eventArgs)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ARPayment>>) eventArgs).Cache.ObjectsEqual<ARPayment.refTranExtNbr>((object) eventArgs.Row, (object) eventArgs.OldRow))
      return;
    foreach (PXResult<SOAdjust> pxResult in ((PXSelectBase<SOAdjust>) this.SOAdjustments).Select(Array.Empty<object>()))
      GraphHelper.MarkUpdated(((PXSelectBase) this.SOAdjustments).Cache, (object) PXResult<SOAdjust>.op_Implicit(pxResult), true);
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Maximum<Minimum<Sub<SOAdjust.curyOrigAdjgAmt, SOAdjust.curyAdjgBilledAmt>, Current<SOAdjust.curyDocBal>>, decimal0>), typeof (SumCalc<ARPayment.curySOApplAmt>))]
  protected virtual void _(PX.Data.Events.CacheAttached<SOAdjust.curyAdjgAmt> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<SOAdjust> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<SOAdjust.adjdOrderNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOAdjust>>) e).Cache, (object) e.Row, this.IsSOOrderReferenceEditable(e.Row));
    PXUIFieldAttribute.SetEnabled<SOAdjust.adjdOrderType>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOAdjust>>) e).Cache, (object) e.Row, this.IsSOOrderReferenceEditable(e.Row));
  }

  protected virtual void _(PX.Data.Events.RowPersisting<SOAdjust> eventArgs)
  {
    if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(eventArgs.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    if (!string.IsNullOrEmpty(((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current?.RefTranExtNbr))
    {
      SOAdjust row = eventArgs.Row;
      int num;
      if (row == null)
      {
        num = 1;
      }
      else
      {
        bool? refundOrigTransaction = row.ValidateCCRefundOrigTransaction;
        bool flag = false;
        num = !(refundOrigTransaction.GetValueOrDefault() == flag & refundOrigTransaction.HasValue) ? 1 : 0;
      }
      if (num != 0)
      {
        if (PXResultset<SOLine>.op_Implicit(PXSelectBase<SOLine, PXViewOf<SOLine>.BasedOn<SelectFromBase<SOLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<ARAdjust>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjdDocType, Equal<SOLine.invoiceType>>>>>.And<BqlOperand<ARAdjust.adjdRefNbr, IBqlString>.IsEqual<SOLine.invoiceNbr>>>>, FbqlJoins.Inner<ExternalTransaction>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ExternalTransaction.docType, Equal<ARAdjust.adjgDocType>>>>>.And<BqlOperand<ExternalTransaction.refNbr, IBqlString>.IsEqual<ARAdjust.adjgRefNbr>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLine.orderType, Equal<BqlField<SOAdjust.adjdOrderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<SOLine.orderNbr, IBqlString>.IsEqual<BqlField<SOAdjust.adjdOrderNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<ExternalTransaction.processingCenterID, IBqlString>.IsEqual<BqlField<ARPayment.processingCenterID, IBqlString>.FromCurrent>>>, And<BqlOperand<ExternalTransaction.tranNumber, IBqlString>.IsEqual<BqlField<ARPayment.refTranExtNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<ARAdjust.voided, IBqlBool>.IsNotEqual<True>>>, And<BqlOperand<ARAdjust.curyAdjdAmt, IBqlDecimal>.IsNotEqual<decimal0>>>>.And<BqlOperand<SOLine.curyLineAmt, IBqlDecimal>.IsNotEqual<decimal0>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base, (object[]) new SOAdjust[1]
        {
          eventArgs.Row
        }, Array.Empty<object>())) == null)
          ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<SOAdjust>>) eventArgs).Cache.RaiseExceptionHandling<SOAdjust.adjdOrderNbr>((object) eventArgs.Row, (object) eventArgs.Row.AdjdOrderNbr, (Exception) new PXSetPropertyException("The {0} sales order has no items that were paid with the {1} transaction and can be returned.", new object[2]
          {
            (object) eventArgs.Row.AdjdOrderNbr,
            (object) ((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current.RefTranExtNbr
          }));
      }
    }
    Decimal? curyDocBal = eventArgs.Row.CuryDocBal;
    Decimal num1 = 0M;
    if (!(curyDocBal.GetValueOrDefault() < num1 & curyDocBal.HasValue) || this.IsApplicationToBlanketOrderWithChild(eventArgs.Row) || ((PXGraphExtension<ARPaymentEntry>) this).Base.IgnoreNegativeOrderBal)
      return;
    int num2 = (eventArgs.Operation & 3) == 1 ? 1 : 0;
    bool flag1 = false;
    if (num2 != 0)
    {
      object original = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<SOAdjust>>) eventArgs).Cache.GetOriginal((object) eventArgs.Row);
      flag1 = original == null || !((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<SOAdjust>>) eventArgs).Cache.ObjectsEqual<SOAdjust.curyAdjgAmt, SOAdjust.adjAmt, SOAdjust.curyAdjdAmt>((object) eventArgs.Row, original);
    }
    if (!(num2 == 0 | flag1))
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<SOAdjust>>) eventArgs).Cache.RaiseExceptionHandling<SOAdjust.curyAdjgAmt>((object) eventArgs.Row, (object) eventArgs.Row.CuryAdjgAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
  }

  protected virtual void _(PX.Data.Events.RowInserting<SOAdjust> e)
  {
    string error = PXUIFieldAttribute.GetError<SOAdjust.adjdOrderNbr>(((PX.Data.Events.Event<PXRowInsertingEventArgs, PX.Data.Events.RowInserting<SOAdjust>>) e).Cache, (object) e.Row);
    e.Cancel = e.Row.AdjdOrderNbr == null || !string.IsNullOrEmpty(error);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<SOAdjust> e)
  {
    Decimal? curyAdjdBilledAmt = e.Row.CuryAdjdBilledAmt;
    Decimal num = 0M;
    if (curyAdjdBilledAmt.GetValueOrDefault() > num & curyAdjdBilledAmt.HasValue)
      throw new PXSetPropertyException("The record cannot be deleted.");
  }

  protected virtual void _(PX.Data.Events.RowDeleted<SOAdjust> e)
  {
    long? adjdCuryInfoId1 = e.Row.AdjdCuryInfoID;
    long? adjgCuryInfoId = e.Row.AdjgCuryInfoID;
    if (adjdCuryInfoId1.GetValueOrDefault() == adjgCuryInfoId.GetValueOrDefault() & adjdCuryInfoId1.HasValue == adjgCuryInfoId.HasValue)
      return;
    long? adjdCuryInfoId2 = e.Row.AdjdCuryInfoID;
    long? adjdOrigCuryInfoId = e.Row.AdjdOrigCuryInfoID;
    if (adjdCuryInfoId2.GetValueOrDefault() == adjdOrigCuryInfoId.GetValueOrDefault() & adjdCuryInfoId2.HasValue == adjdOrigCuryInfoId.HasValue)
      return;
    foreach (PXResult<PX.Objects.CM.Extensions.CurrencyInfo> pxResult in ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) ((PXGraphExtension<ARPaymentEntry>) this).Base.CurrencyInfo_CuryInfoID).Select(new object[1]
    {
      (object) e.Row.AdjdCuryInfoID
    }))
      ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) ((PXGraphExtension<ARPaymentEntry>) this).Base.currencyinfo).Delete(PXResult<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.adjdOrderNbr> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.adjdOrderNbr>>) e).Cancel)
      return;
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.adjdOrderNbr>, SOAdjust, object>) e).NewValue != null && this.IsSOOrderReferenceEditable(e.Row))
    {
      ARSetupNoMigrationMode.EnsureMigrationModeDisabled((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base);
      if (PXSelectorAttribute.Select<SOAdjust.adjdOrderNbr>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.adjdOrderNbr>>) e).Cache, (object) e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.adjdOrderNbr>, SOAdjust, object>) e).NewValue) == null)
        throw new PXSetPropertyException<SOAdjust.adjdOrderNbr>("The order cannot be applied, the specified combination of the order type and order number cannot be found in the system.", (PXErrorLevel) 4);
    }
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.adjdOrderNbr>>) e).ExternalCall)
    {
      if (((IQueryable<PXResult<PX.Objects.SO.SOOrder>>) PXSelectBase<PX.Objects.SO.SOOrder, PXSelectJoin<PX.Objects.SO.SOOrder, InnerJoin<PX.Objects.CS.Terms, On<PX.Objects.CS.Terms.termsID, Equal<PX.Objects.SO.SOOrder.termsID>>>, Where<PX.Objects.SO.SOOrder.orderType, Equal<Required<SOAdjust.adjdOrderType>>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<Required<SOAdjust.adjdOrderNbr>>, And<PX.Objects.CS.Terms.installmentType, NotEqual<TermsInstallmentType.single>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base, new object[2]
      {
        (object) e.Row.AdjdOrderType,
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.adjdOrderNbr>, SOAdjust, object>) e).NewValue
      })).Count<PXResult<PX.Objects.SO.SOOrder>>() > 0)
        throw new PXSetPropertyException("No applications can be created for documents with multiple installment credit terms specified.");
    }
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOAdjust, SOAdjust.adjdOrderNbr>>) e).Cancel = ((PXGraphExtension<ARPaymentEntry>) this).Base.IsAdjdRefNbrFieldVerifyingDisabled((IAdjustment) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<SOAdjust, SOAdjust.adjdOrderNbr> e)
  {
    try
    {
      if (e.Row.AdjdCuryInfoID.HasValue)
        return;
      using (IEnumerator<PXResult<PX.Objects.SO.SOOrder>> enumerator = PXSelectBase<PX.Objects.SO.SOOrder, PXSelectJoin<PX.Objects.SO.SOOrder, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<PX.Objects.SO.SOOrder.curyInfoID>>>, Where<PX.Objects.SO.SOOrder.orderType, Equal<Required<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<Required<PX.Objects.SO.SOOrder.orderNbr>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base, new object[2]
      {
        (object) e.Row.AdjdOrderType,
        (object) e.Row.AdjdOrderNbr
      }).GetEnumerator())
      {
        if (!enumerator.MoveNext())
          return;
        PXResult<PX.Objects.SO.SOOrder, PX.Objects.CM.Extensions.CurrencyInfo> current = (PXResult<PX.Objects.SO.SOOrder, PX.Objects.CM.Extensions.CurrencyInfo>) enumerator.Current;
        this.UpdateAppliedToOrderAmount(PXResult<PX.Objects.SO.SOOrder, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(current), PXResult<PX.Objects.SO.SOOrder, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(current), e.Row);
      }
    }
    catch (PXSetPropertyException ex)
    {
      throw new PXException(((Exception) ex).Message);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<SOAdjust, SOAdjust.adjdOrderType> e)
  {
    if (e.Row == null || !((string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<SOAdjust, SOAdjust.adjdOrderType>, SOAdjust, object>) e).OldValue != e.Row.AdjdOrderType))
      return;
    object obj = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOAdjust, SOAdjust.adjdOrderType>>) e).Cache.GetValue<SOAdjust.adjdOrderNbr>((object) e.Row);
    try
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOAdjust, SOAdjust.adjdOrderType>>) e).Cache.RaiseFieldVerifying<SOAdjust.adjdOrderNbr>((object) e.Row, ref obj);
    }
    catch (PXSetPropertyException<SOAdjust.adjdOrderNbr> ex)
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOAdjust, SOAdjust.adjdOrderType>>) e).Cache.SetValue<SOAdjust.adjdOrderNbr>((object) e.Row, (object) null);
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<ARPayment> e, PXRowSelected baseMethod)
  {
    if (e.Row == null || ((PXGraphExtension<ARPaymentEntry>) this).Base.InternalCall)
    {
      baseMethod?.Invoke(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARPayment>>) e).Cache, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARPayment>>) e).Args);
    }
    else
    {
      bool flag = e.Row.Status != "C" && !e.Row.VoidAppl.GetValueOrDefault() && !e.Row.Voided.GetValueOrDefault() && !e.Row.PaymentsByLinesAllowed.GetValueOrDefault();
      ((PXSelectBase) this.SOAdjustments).Cache.AllowUpdate = flag;
      ((PXSelectBase) this.SOAdjustments).Cache.AllowDelete = flag;
      ((PXSelectBase) this.SOAdjustments).Cache.AllowInsert = flag;
      ((PXSelectBase) this.SOAdjustments).Cache.AllowSelect = !e.Row.IsMigratedRecord.GetValueOrDefault() || e.Row.Released.GetValueOrDefault();
      baseMethod?.Invoke(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARPayment>>) e).Cache, ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ARPayment>>) e).Args);
      ((PXAction) this.loadOrders).SetEnabled(((PXAction) ((PXGraphExtension<ARPaymentEntry>) this).Base.loadInvoices).GetEnabled());
    }
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.AR.ARPaymentEntry.SetUnreleasedIncomingApplicationWarning(PX.Data.PXCache,PX.Objects.AR.ARPayment,System.String)" />
  /// </summary>
  [PXOverride]
  public virtual void SetUnreleasedIncomingApplicationWarning(
    PXCache sender,
    ARPayment document,
    string warningMessage,
    Action<PXCache, ARPayment, string> baseMethod)
  {
    baseMethod(sender, document, warningMessage);
    ((PXSelectBase) this.SOAdjustments).Cache.AllowInsert = false;
    ((PXSelectBase) this.SOAdjustments).Cache.AllowUpdate = false;
    ((PXSelectBase) this.SOAdjustments).Cache.AllowDelete = false;
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.AR.ARPaymentEntry.DisableViewsOnUnapprovedRefund" />
  /// </summary>
  [PXOverride]
  public virtual void DisableViewsOnUnapprovedRefund(Action baseMethod)
  {
    baseMethod();
    ((PXSelectBase) this.SOAdjustments).Cache.AllowInsert = false;
    ((PXSelectBase) this.SOAdjustments).Cache.AllowUpdate = false;
    ((PXSelectBase) this.SOAdjustments).Cache.AllowDelete = false;
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.CM.Extensions.CurrencyInfo> e, PXRowUpdated baseMethod)
  {
    baseMethod?.Invoke(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.CM.Extensions.CurrencyInfo>>) e).Cache, ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.CM.Extensions.CurrencyInfo>>) e).Args);
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.CM.Extensions.CurrencyInfo>>) e).Cache.ObjectsEqual<PX.Objects.CM.Extensions.CurrencyInfo.curyID, PX.Objects.CM.Extensions.CurrencyInfo.curyRate, PX.Objects.CM.Extensions.CurrencyInfo.curyMultDiv>((object) e.Row, (object) e.OldRow))
      return;
    foreach (PXResult<SOAdjust> pxResult in PXSelectBase<SOAdjust, PXSelect<SOAdjust, Where<SOAdjust.adjgCuryInfoID, Equal<Required<SOAdjust.adjgCuryInfoID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base, new object[1]
    {
      (object) e.Row.CuryInfoID
    }))
    {
      SOAdjust soAdjust = PXResult<SOAdjust>.op_Implicit(pxResult);
      GraphHelper.MarkUpdated(((PXSelectBase) this.SOAdjustments).Cache, (object) soAdjust, true);
      this.SOBalanceCalculator.CalcBalances(soAdjust, true, true);
      Decimal? curyDocBal = soAdjust.CuryDocBal;
      Decimal num = 0M;
      if (curyDocBal.GetValueOrDefault() < num & curyDocBal.HasValue && !this.IsApplicationToBlanketOrderWithChild(soAdjust))
        ((PXSelectBase) this.SOAdjustments).Cache.RaiseExceptionHandling<SOAdjust.curyAdjgAmt>((object) soAdjust, (object) soAdjust.CuryAdjgAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
    }
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.AR.ARPaymentEntry.MultiCurrency.GetChildren" />
  /// </summary>
  [PXOverride]
  public virtual PXSelectBase[] GetChildren(Func<PXSelectBase[]> baseMethod)
  {
    return EnumerableExtensions.Append<PXSelectBase>(baseMethod(), new PXSelectBase[3]
    {
      (PXSelectBase) this.SOAdjustments,
      (PXSelectBase) this.SOAdjustments_Orders,
      (PXSelectBase) this.SOAdjustments_Raw
    });
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.AR.ARPaymentEntry.CalcApplAmounts(PX.Data.PXCache,PX.Objects.AR.ARPayment)" />
  /// </summary>
  [PXOverride]
  public virtual void CalcApplAmounts(
    PXCache sender,
    ARPayment row,
    Action<PXCache, ARPayment> baseMethod)
  {
    baseMethod(sender, row);
    if (row.CurySOApplAmt.HasValue)
      return;
    this.RecalcSOApplAmounts(sender, row);
  }

  public virtual void LoadOrdersProc(bool LoadExistingOnly, ARPaymentEntry.LoadOptions opts)
  {
    ((PXGraphExtension<ARPaymentEntry>) this).Base.InternalCall = true;
    try
    {
      ARPayment current = ((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current;
      if ((current != null ? (!current.CustomerID.HasValue ? 1 : 0) : 1) == 0)
      {
        bool? openDoc = ((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current.OpenDoc;
        bool flag = false;
        if (!(openDoc.GetValueOrDefault() == flag & openDoc.HasValue) && !EnumerableExtensions.IsNotIn<string>(((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current.DocType, "PMT", "PPM"))
        {
          Dictionary<string, SOAdjust> existing = new Dictionary<string, SOAdjust>();
          foreach (PXResult<SOAdjust> pxResult in ((PXSelectBase<SOAdjust>) this.SOAdjustments_Raw).Select(Array.Empty<object>()))
          {
            SOAdjust soAdjust = PXResult<SOAdjust>.op_Implicit(pxResult);
            if (!LoadExistingOnly)
            {
              soAdjust = PXCache<SOAdjust>.CreateCopy(soAdjust);
              soAdjust.CuryAdjgAmt = new Decimal?();
              soAdjust.CuryAdjgDiscAmt = new Decimal?();
            }
            existing.Add($"{soAdjust.AdjdOrderType}_{soAdjust.AdjdOrderNbr}", soAdjust);
            ((PXSelectBase<SOAdjust>) this.SOAdjustments).Delete(PXResult<SOAdjust>.op_Implicit(pxResult));
          }
          GraphHelper.MarkUpdated(((PXSelectBase) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Cache, (object) ((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current, true);
          ((PXSelectBase) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Cache.IsDirty = true;
          foreach (KeyValuePair<string, SOAdjust> keyValuePair in existing)
            this.AddSOAdjustmentExistingBefore(keyValuePair.Value);
          if (LoadExistingOnly)
            return;
          PXResultset<PX.Objects.SO.SOOrder> load = this.CollectOrdersToLoad(opts);
          Expression<Func<PXResult<PX.Objects.SO.SOOrder>, bool>> predicate = (Expression<Func<PXResult<PX.Objects.SO.SOOrder>, bool>>) (res => !existing.ContainsKey($"{((PX.Objects.SO.SOOrder) res).OrderType}_{((PX.Objects.SO.SOOrder) res).OrderNbr}"));
          using (IEnumerator<PXResult<PX.Objects.SO.SOOrder>> enumerator = ((IQueryable<PXResult<PX.Objects.SO.SOOrder>>) load).Where<PXResult<PX.Objects.SO.SOOrder>>(predicate).GetEnumerator())
          {
            while (enumerator.MoveNext())
              this.AddSOAdjustmentNotExistingBefore(enumerator.Current);
            return;
          }
        }
      }
      throw new ARPaymentEntry.PXLoadInvoiceException();
    }
    catch (ARPaymentEntry.PXLoadInvoiceException ex)
    {
    }
    finally
    {
      ((PXGraphExtension<ARPaymentEntry>) this).Base.InternalCall = false;
    }
  }

  protected virtual SOAdjust AddSOAdjustmentNotExistingBefore(PXResult<PX.Objects.SO.SOOrder> res)
  {
    return this.AddSOAdjustment(new SOAdjust()
    {
      AdjdOrderType = PXResult<PX.Objects.SO.SOOrder>.op_Implicit(res).OrderType,
      AdjdOrderNbr = PXResult<PX.Objects.SO.SOOrder>.op_Implicit(res).OrderNbr
    });
  }

  protected virtual PXResultset<PX.Objects.SO.SOOrder> CollectOrdersToLoad(
    ARPaymentEntry.LoadOptions opts)
  {
    PXSelectBase<PX.Objects.SO.SOOrder> loadQuery = this.BuidOrdersToLoadQuery(opts);
    ARPaymentEntry.LoadOptions loadOptions = opts;
    PXResultset<PX.Objects.SO.SOOrder> load1 = (loadOptions != null ? (!loadOptions.MaxDocs.HasValue ? 1 : 0) : 1) != 0 ? loadQuery.Select(Array.Empty<object>()) : loadQuery.SelectWindowed(0, opts.MaxDocs.Value, Array.Empty<object>());
    load1.Sort((Comparison<PXResult<PX.Objects.SO.SOOrder>>) ((a, b) =>
    {
      if (((PXSelectBase<ARSetup>) ((PXGraphExtension<ARPaymentEntry>) this).Base.arsetup).Current.FinChargeFirst.GetValueOrDefault())
      {
        int load2 = ((IComparable) (!(PXResult<PX.Objects.SO.SOOrder>.op_Implicit(a).DocType == "FCH") ? 1 : 0)).CompareTo((object) (!(PXResult<PX.Objects.SO.SOOrder>.op_Implicit(b).DocType == "FCH") ? 1 : 0));
        if (load2 != 0)
          return load2;
      }
      if (opts == null)
      {
        DateTime? orderDate = PXResult<PX.Objects.SO.SOOrder>.op_Implicit(a).OrderDate;
        // ISSUE: variable of a boxed type
        __Boxed<DateTime> local = (ValueType) (orderDate ?? DateTime.MinValue);
        orderDate = PXResult<PX.Objects.SO.SOOrder>.op_Implicit(b).OrderDate;
        object obj = (object) (orderDate ?? DateTime.MinValue);
        return ((IComparable) local).CompareTo(obj);
      }
      switch (opts.OrderBy)
      {
        case "ORD":
          return PXResult<PX.Objects.SO.SOOrder>.op_Implicit(a).OrderNbr.CompareTo((object) PXResult<PX.Objects.SO.SOOrder>.op_Implicit(b).OrderNbr);
        default:
          DateTime? orderDate1 = PXResult<PX.Objects.SO.SOOrder>.op_Implicit(a).OrderDate;
          // ISSUE: variable of a boxed type
          __Boxed<DateTime> local1 = (ValueType) (orderDate1 ?? DateTime.MinValue);
          orderDate1 = PXResult<PX.Objects.SO.SOOrder>.op_Implicit(b).OrderDate;
          object obj1 = (object) (orderDate1 ?? DateTime.MinValue);
          int num = ((IComparable) local1).CompareTo(obj1);
          return num != 0 ? num : PXResult<PX.Objects.SO.SOOrder>.op_Implicit(a).OrderNbr.CompareTo((object) PXResult<PX.Objects.SO.SOOrder>.op_Implicit(b).OrderNbr);
      }
    }));
    return load1;
  }

  protected virtual PXSelectBase<PX.Objects.SO.SOOrder> BuidOrdersToLoadQuery(
    ARPaymentEntry.LoadOptions opts)
  {
    PXSelectBase<PX.Objects.SO.SOOrder> loadQuery = (PXSelectBase<PX.Objects.SO.SOOrder>) new PXSelectJoin<PX.Objects.SO.SOOrder, InnerJoin<SOOrderType, On<SOOrderType.orderType, Equal<PX.Objects.SO.SOOrder.orderType>>, InnerJoin<PX.Objects.CS.Terms, On<PX.Objects.CS.Terms.termsID, Equal<PX.Objects.SO.SOOrder.termsID>>>>, Where<PX.Objects.SO.SOOrder.customerID, Equal<Optional<ARPayment.customerID>>, And<PX.Objects.SO.SOOrder.openDoc, Equal<True>, And<PX.Objects.SO.SOOrder.orderDate, LessEqual<Current<ARPayment.adjDate>>, And<SOOrderType.aRDocType, In3<ARDocType.invoice, ARDocType.debitMemo>, And<PX.Objects.SO.SOOrder.status, NotIn3<SOOrderStatus.cancelled, SOOrderStatus.pendingApproval, SOOrderStatus.voided>, And<PX.Objects.CS.Terms.installmentType, NotEqual<TermsInstallmentType.multiple>>>>>>>, OrderBy<Asc<PX.Objects.SO.SOOrder.orderDate, Asc<PX.Objects.SO.SOOrder.orderNbr>>>>((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base);
    if (opts != null)
    {
      DateTime? nullable = opts.FromDate;
      if (nullable.HasValue)
        loadQuery.WhereAnd<Where<PX.Objects.SO.SOOrder.orderDate, GreaterEqual<Current<ARPaymentEntry.LoadOptions.fromDate>>>>();
      nullable = opts.TillDate;
      if (nullable.HasValue)
        loadQuery.WhereAnd<Where<PX.Objects.SO.SOOrder.orderDate, LessEqual<Current<ARPaymentEntry.LoadOptions.tillDate>>>>();
      if (!string.IsNullOrEmpty(opts.StartOrderNbr))
        loadQuery.WhereAnd<Where<PX.Objects.SO.SOOrder.orderNbr, GreaterEqual<Current<ARPaymentEntry.LoadOptions.startOrderNbr>>>>();
      if (!string.IsNullOrEmpty(opts.EndOrderNbr))
        loadQuery.WhereAnd<Where<PX.Objects.SO.SOOrder.orderNbr, LessEqual<Current<ARPaymentEntry.LoadOptions.endOrderNbr>>>>();
    }
    return loadQuery;
  }

  protected virtual SOAdjust AddSOAdjustmentExistingBefore(SOAdjust existingAdj)
  {
    SOAdjust adj = new SOAdjust()
    {
      RecordID = existingAdj.RecordID,
      AdjdOrderType = existingAdj.AdjdOrderType,
      AdjdOrderNbr = existingAdj.AdjdOrderNbr
    };
    try
    {
      adj = PXCache<SOAdjust>.CreateCopy(this.AddSOAdjustment(adj) ?? adj);
      Decimal? nullable1 = existingAdj.CuryAdjgDiscAmt;
      Decimal? nullable2;
      if (nullable1.HasValue)
      {
        nullable1 = existingAdj.CuryAdjgDiscAmt;
        nullable2 = adj.CuryAdjgDiscAmt;
        if (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
        {
          adj.CuryAdjgDiscAmt = existingAdj.CuryAdjgDiscAmt;
          adj = PXCache<SOAdjust>.CreateCopy((SOAdjust) ((PXSelectBase) ((PXGraphExtension<ARPaymentEntry>) this).Base.Adjustments).Cache.Update((object) adj));
        }
      }
      nullable2 = existingAdj.CuryAdjgAmt;
      if (nullable2.HasValue)
      {
        nullable2 = existingAdj.CuryAdjgAmt;
        nullable1 = adj.CuryAdjgAmt;
        if (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
        {
          adj.CuryAdjgAmt = existingAdj.CuryAdjgAmt;
          ((PXSelectBase) ((PXGraphExtension<ARPaymentEntry>) this).Base.Adjustments).Cache.Update((object) adj);
        }
      }
    }
    catch (PXSetPropertyException ex)
    {
    }
    return adj;
  }

  protected virtual SOAdjust AddSOAdjustment(SOAdjust adj)
  {
    Decimal? curyUnappliedBal = ((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current.CuryUnappliedBal;
    Decimal num1 = 0M;
    if (curyUnappliedBal.GetValueOrDefault() == num1 & curyUnappliedBal.HasValue)
    {
      Decimal? curyOrigDocAmt = ((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current.CuryOrigDocAmt;
      Decimal num2 = 0M;
      if (curyOrigDocAmt.GetValueOrDefault() > num2 & curyOrigDocAmt.HasValue)
        throw new ARPaymentEntry.PXLoadInvoiceException();
    }
    return ((PXSelectBase<SOAdjust>) this.SOAdjustments).Insert(adj);
  }

  public virtual void RecalcSOApplAmounts(PXCache sender, ARPayment row)
  {
    bool flag = sender.GetStatus((object) row) == 0;
    PXFormulaAttribute.CalcAggregate<SOAdjust.curyAdjgAmt>(((PXSelectBase) this.SOAdjustments).Cache, (object) row, flag);
    if (!row.CurySOApplAmt.HasValue)
      row.CurySOApplAmt = new Decimal?(0M);
    sender.RaiseFieldUpdated<ARPayment.curySOApplAmt>((object) row, (object) null);
  }

  protected virtual void UpdateAppliedToOrderAmount(
    PX.Objects.SO.SOOrder order,
    PX.Objects.CM.Extensions.CurrencyInfo curyInfo,
    SOAdjust adj)
  {
    PX.Objects.SO.SOOrder copy = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(order);
    adj.CustomerID = ((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current.CustomerID;
    adj.AdjgDocDate = ((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current.AdjDate;
    adj.AdjgCuryInfoID = ((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current.CuryInfoID;
    adj.AdjdCuryInfoID = curyInfo.CuryInfoID;
    adj.AdjdOrigCuryInfoID = copy.CuryInfoID;
    SOAdjust soAdjust1 = adj;
    DateTime? orderDate = copy.OrderDate;
    DateTime? adjDate = ((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current.AdjDate;
    DateTime? nullable1 = (orderDate.HasValue & adjDate.HasValue ? (orderDate.GetValueOrDefault() > adjDate.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? ((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current.AdjDate : copy.OrderDate;
    soAdjust1.AdjdOrderDate = nullable1;
    adj.Released = new bool?(false);
    SOAdjust soAdjust2 = PXResultset<SOAdjust>.op_Implicit(PXSelectBase<SOAdjust, PXSelectGroupBy<SOAdjust, Where<SOAdjust.voided, Equal<False>, And<SOAdjust.adjdOrderType, Equal<Required<SOAdjust.adjdOrderType>>, And<SOAdjust.adjdOrderNbr, Equal<Required<SOAdjust.adjdOrderNbr>>, And<Where<SOAdjust.adjgDocType, NotEqual<Required<SOAdjust.adjgDocType>>, Or<SOAdjust.adjgRefNbr, NotEqual<Required<SOAdjust.adjgRefNbr>>>>>>>>, Aggregate<GroupBy<SOAdjust.adjdOrderType, GroupBy<SOAdjust.adjdOrderNbr, Sum<SOAdjust.curyAdjdAmt, Sum<SOAdjust.adjAmt>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base, new object[4]
    {
      (object) adj.AdjdOrderType,
      (object) adj.AdjdOrderNbr,
      (object) adj.AdjgDocType,
      (object) adj.AdjgRefNbr
    }));
    Decimal? nullable2;
    Decimal? nullable3;
    if (soAdjust2 != null && soAdjust2.AdjdOrderNbr != null)
    {
      PX.Objects.SO.SOOrder soOrder1 = copy;
      Decimal? curyDocBal = soOrder1.CuryDocBal;
      nullable2 = soAdjust2.CuryAdjdAmt;
      soOrder1.CuryDocBal = curyDocBal.HasValue & nullable2.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      PX.Objects.SO.SOOrder soOrder2 = copy;
      nullable2 = soOrder2.DocBal;
      nullable3 = soAdjust2.AdjAmt;
      soOrder2.DocBal = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
    }
    this.SOBalanceCalculator.CalcBalances(adj, copy, false, true);
    nullable3 = adj.CuryDocBal;
    nullable2 = adj.CuryDiscBal;
    Decimal? nullable4 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable5 = adj.CuryDiscBal;
    Decimal? curyUnappliedBal = ((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current.CuryUnappliedBal;
    nullable2 = adj.CuryDiscBal;
    Decimal num1 = 0M;
    Decimal? nullable6;
    if (nullable2.GetValueOrDefault() >= num1 & nullable2.HasValue)
    {
      nullable3 = adj.CuryDocBal;
      nullable6 = adj.CuryDiscBal;
      nullable2 = nullable3.HasValue & nullable6.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
      Decimal num2 = 0M;
      if (nullable2.GetValueOrDefault() <= num2 & nullable2.HasValue)
        return;
    }
    if (((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current != null && string.IsNullOrEmpty(((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current.DocDesc))
      ((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current.DocDesc = copy.OrderDesc;
    if (((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current != null)
    {
      nullable2 = curyUnappliedBal;
      Decimal num3 = 0M;
      if (nullable2.GetValueOrDefault() > num3 & nullable2.HasValue)
      {
        nullable4 = new Decimal?(Math.Min(nullable4.Value, curyUnappliedBal.Value));
        nullable3 = nullable4;
        Decimal? nullable7 = nullable5;
        nullable2 = nullable3.HasValue & nullable7.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable7.GetValueOrDefault()) : new Decimal?();
        nullable6 = adj.CuryDocBal;
        if (nullable2.GetValueOrDefault() < nullable6.GetValueOrDefault() & nullable2.HasValue & nullable6.HasValue)
        {
          nullable5 = new Decimal?(0M);
          goto label_14;
        }
        goto label_14;
      }
    }
    if (((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current != null)
    {
      nullable6 = curyUnappliedBal;
      Decimal num4 = 0M;
      if (nullable6.GetValueOrDefault() <= num4 & nullable6.HasValue)
      {
        nullable6 = ((PXSelectBase<ARPayment>) ((PXGraphExtension<ARPaymentEntry>) this).Base.Document).Current.CuryOrigDocAmt;
        Decimal num5 = 0M;
        if (nullable6.GetValueOrDefault() > num5 & nullable6.HasValue)
        {
          nullable4 = new Decimal?(0M);
          nullable5 = new Decimal?(0M);
        }
      }
    }
label_14:
    SOAdjust soAdjust3 = (SOAdjust) ((PXSelectBase) this.SOAdjustments).Cache.Locate((object) adj);
    if (soAdjust3 == null)
      soAdjust3 = (SOAdjust) PrimaryKeyOf<SOAdjust>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<SOAdjust.recordID, SOAdjust.adjdOrderType, SOAdjust.adjdOrderNbr, SOAdjust.adjgDocType, SOAdjust.adjgRefNbr>>.Find((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base, (TypeArrayOf<IBqlField>.IFilledWith<SOAdjust.recordID, SOAdjust.adjdOrderType, SOAdjust.adjdOrderNbr, SOAdjust.adjgDocType, SOAdjust.adjgRefNbr>) adj, (PKFindOptions) 0);
    else if (EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.SOAdjustments).Cache.GetStatus((object) soAdjust3), (PXEntryStatus) 3, (PXEntryStatus) 4))
      soAdjust3 = (SOAdjust) null;
    PXCache cache1 = ((PXSelectBase) this.SOAdjustments).Cache;
    SOAdjust soAdjust4 = adj;
    Decimal? nullable8;
    if (soAdjust3 == null)
    {
      nullable6 = new Decimal?();
      nullable8 = nullable6;
    }
    else
      nullable8 = soAdjust3.CuryAdjgAmt;
    nullable6 = nullable8;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal> valueOrDefault1 = (ValueType) nullable6.GetValueOrDefault();
    cache1.SetValue<SOAdjust.curyAdjgAmt>((object) soAdjust4, (object) valueOrDefault1);
    PXCache cache2 = ((PXSelectBase) this.SOAdjustments).Cache;
    SOAdjust soAdjust5 = adj;
    Decimal? nullable9;
    if (soAdjust3 == null)
    {
      nullable6 = new Decimal?();
      nullable9 = nullable6;
    }
    else
    {
      // ISSUE: explicit non-virtual call
      nullable9 = __nonvirtual (soAdjust3.CuryAdjgDiscAmt);
    }
    nullable6 = nullable9;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal> valueOrDefault2 = (ValueType) nullable6.GetValueOrDefault();
    cache2.SetValue<SOAdjust.curyAdjgDiscAmt>((object) soAdjust5, (object) valueOrDefault2);
    ((PXSelectBase) this.SOAdjustments).Cache.SetValueExt<SOAdjust.curyAdjgAmt>((object) adj, (object) nullable4);
    ((PXSelectBase) this.SOAdjustments).Cache.SetValueExt<SOAdjust.curyAdjgDiscAmt>((object) adj, (object) nullable5);
    this.SOBalanceCalculator.CalcBalances(adj, copy, true, true);
  }

  public virtual bool IsApplicationToBlanketOrderWithChild(SOAdjust adjustment)
  {
    if (adjustment == null || !(SOOrderType.PK.Find((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base, adjustment.AdjdOrderType)?.Behavior == "BL"))
      return false;
    Decimal? transferredToChildrenAmt = adjustment.AdjTransferredToChildrenAmt;
    Decimal num1 = 0M;
    if (transferredToChildrenAmt.GetValueOrDefault() > num1 & transferredToChildrenAmt.HasValue)
      return true;
    PX.Objects.SO.SOOrder soOrder = PXParentAttribute.SelectParent<PX.Objects.SO.SOOrder>(((PXSelectBase) this.SOAdjustments).Cache, (object) adjustment);
    if (soOrder == null)
      return true;
    int? childLineCntr = soOrder.ChildLineCntr;
    int num2 = 0;
    return !(childLineCntr.GetValueOrDefault() == num2 & childLineCntr.HasValue);
  }

  protected virtual bool IsSOOrderReferenceEditable(SOAdjust adj)
  {
    return adj != null && GraphHelper.Caches<SOAdjust>((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base).GetStatus(adj) == 2;
  }
}
