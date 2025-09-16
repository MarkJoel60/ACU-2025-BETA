// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.SOOrderEntryVATRecognitionOnPrepayments
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Collection;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.PM;
using PX.Objects.SO;
using PX.Objects.SO.GraphExtensions.SOOrderEntryExt;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable enable
namespace PX.Objects.AR;

public class SOOrderEntryVATRecognitionOnPrepayments : 
  PXGraphExtension<
  #nullable disable
  CreatePaymentExt, SOOrderEntry>
{
  [PXCopyPasteHiddenView]
  public PXFilter<SOQuickPrepaymentInvoice> QuickPrepaymentInvoice;
  [PXHidden]
  public FbqlSelect<SelectFromBase<SOAdjust, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<ARRegister>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SOAdjust.adjgDocType, 
  #nullable disable
  Equal<ARRegister.docType>>>>>.And<BqlOperand<
  #nullable enable
  SOAdjust.adjgRefNbr, IBqlString>.IsEqual<
  #nullable disable
  ARRegister.refNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SOAdjust.adjdOrderType, 
  #nullable disable
  Equal<P.AsString.ASCII>>>>, And<BqlOperand<
  #nullable enable
  SOAdjust.adjdOrderNbr, IBqlString>.IsEqual<
  #nullable disable
  P.AsString>>>, And<BqlOperand<
  #nullable enable
  ARRegister.docType, IBqlString>.IsEqual<
  #nullable disable
  ARDocType.prepaymentInvoice>>>>.And<BqlOperand<
  #nullable enable
  ARRegister.openDoc, IBqlBool>.IsEqual<
  #nullable disable
  True>>>, SOAdjust>.View OpenAdjustingPrepaymentInvoices;
  public PXAction<PX.Objects.SO.SOOrder> createPrepaymentInvoice;
  public PXAction<PX.Objects.SO.SOOrder> refreshGraph;
  public PXAction<PX.Objects.SO.SOOrder> createPrepaymentInvoiceOK;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>();
  }

  protected DiscountEngine<ARTran, ARInvoiceDiscountDetail> ARDiscountEngine
  {
    get => DiscountEngineProvider.GetEngineFor<ARTran, ARInvoiceDiscountDetail>();
  }

  [PXUIField]
  [PXButton(ImageKey = "AddNew", Tooltip = "Create Prepayment Invoice", DisplayOnMainToolbar = false, PopupCommand = "refreshGraph")]
  public virtual IEnumerable CreatePrepaymentInvoice(PXAdapter adapter)
  {
    ((PXAction) ((PXGraphExtension<SOOrderEntry>) this).Base.Save).Press();
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current;
    if (current.Behavior == "BL")
      throw new PXException("Prepayment invoices are not supported for blanket sales orders.");
    if (!((PXSelectBase<SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Any<SOLine>())
      throw new PXException("The sales order contains no lines for the creation of a prepayment invoice.");
    this.Base1.CheckTermsInstallmentType();
    string str1 = PXMessages.LocalizeNoPrefix("Create Prepayment Invoice");
    PXView view = ((PXSelectBase) this.QuickPrepaymentInvoice).View;
    string str2 = str1;
    SOOrderEntryVATRecognitionOnPrepayments recognitionOnPrepayments = this;
    // ISSUE: virtual method pointer
    PXView.InitializePanel initializePanel = new PXView.InitializePanel((object) recognitionOnPrepayments, __vmethodptr(recognitionOnPrepayments, InitializeQuickPrepaymentInvoicePanel));
    if (view.AskExtWithHeader(str2, initializePanel, (List<string>) null, false) == 1)
    {
      Decimal? curyPrepaymentAmt1 = ((PXSelectBase<SOQuickPrepaymentInvoice>) this.QuickPrepaymentInvoice).Current.CuryPrepaymentAmt;
      Decimal? curyUnpaidBalance = current.CuryUnpaidBalance;
      if (curyPrepaymentAmt1.GetValueOrDefault() > curyUnpaidBalance.GetValueOrDefault() & curyPrepaymentAmt1.HasValue & curyUnpaidBalance.HasValue)
        throw new PXException("The prepayment amount must be less than or equal to the order unpaid balance ({0}).", new object[1]
        {
          (object) current.CuryUnpaidBalance
        });
      Decimal? curyPrepaymentAmt2 = ((PXSelectBase<SOQuickPrepaymentInvoice>) this.QuickPrepaymentInvoice).Current.CuryPrepaymentAmt;
      Decimal num = 0M;
      if (curyPrepaymentAmt2.GetValueOrDefault() <= num & curyPrepaymentAmt2.HasValue)
        throw new PXException("The prepayment amount must be greater than zero.");
      ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
      bool valueOrDefault = ((PXSelectBase<ARSetup>) instance.ARSetup).Current.RequireControlTotal.GetValueOrDefault();
      ((PXSelectBase<ARSetup>) instance.ARSetup).Current.RequireControlTotal = new bool?(false);
      PXSelectJoin<ARInvoice, LeftJoinSingleTable<Customer, On<Customer.bAccountID, Equal<ARInvoice.customerID>>>, Where<ARInvoice.docType, Equal<Optional<ARInvoice.docType>>, And2<Where<ARInvoice.origModule, Equal<BatchModule.moduleAR>, Or<ARInvoice.origModule, Equal<BatchModule.moduleEP>, Or<ARInvoice.released, Equal<True>>>>, And<Where<Customer.bAccountID, IsNull, Or<Match<Customer, Current<AccessInfo.userName>>>>>>>> document = instance.Document;
      ARInvoice arInvoice1 = new ARInvoice();
      arInvoice1.DocType = "PPI";
      ARInvoice newdoc = ((PXSelectBase<ARInvoice>) document).Insert(arInvoice1);
      ARInvoice arInvoice2 = this.FillPrepaymentInvoice(instance, newdoc, current, ((PXSelectBase<SOQuickPrepaymentInvoice>) this.QuickPrepaymentInvoice).Current);
      ((PXSelectBase<ARInvoice>) instance.Document).Current = arInvoice2;
      SOAdjust soApplication = this.CreateSOApplication(instance, current);
      ((PXSelectBase<SOAdjust>) ((PXGraph) instance).GetExtension<ARInvoiceEntryVATRecognitionOnPrepayments>().SOAdjustments).Current = soApplication;
      ((PXSelectBase<ARSetup>) instance.ARSetup).Current.RequireControlTotal = new bool?(valueOrDefault);
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable RefreshGraph(PXAdapter adapter)
  {
    return ((PXAction) ((PXGraphExtension<SOOrderEntry>) this).Base.Cancel).Press(adapter);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable CreatePrepaymentInvoiceOK(PXAdapter adapter) => adapter.Get();

  [PXMergeAttributes]
  [ARPaymentType.AdjgRefNbr(typeof (Search<ARPayment.refNbr, Where<ARPayment.customerID, In3<Current<PX.Objects.SO.SOOrder.customerID>, Current<Customer.consolidatingBAccountID>>, And<ARPayment.docType, Equal<Optional<SOAdjust.adjgDocType>>, And<ARPayment.openDoc, Equal<True>, And<Where<ARPayment.docType, NotEqual<ARDocType.prepaymentInvoice>, Or<ARPayment.released, Equal<True>, Or<Exists<Select<SOAdjust, Where<SOAdjust.adjgRefNbr, Equal<ARPayment.refNbr>, And<SOAdjust.adjgDocType, Equal<ARPayment.docType>>>>>>>>>>>>>), Filterable = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOAdjust.adjgRefNbr> eventArgs)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder> eventArgs)
  {
    if (eventArgs.Row == null)
      return;
    SOOrderStateForPayments documentState = this.Base1.GetDocumentState(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) eventArgs).Cache, eventArgs.Row);
    ((PXAction) this.createPrepaymentInvoice).SetVisible(documentState.PaymentsAllowed);
    ((PXAction) this.createPrepaymentInvoice).SetEnabled(documentState.CreatePaymentEnabled);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.SO.SOOrder> eventArgs)
  {
    PX.Objects.SO.SOOrder row = eventArgs.Row;
    if (row == null)
      return;
    if (((IQueryable<PXResult<SOAdjust>>) ((PXSelectBase<SOAdjust>) this.OpenAdjustingPrepaymentInvoices).Select(new object[2]
    {
      (object) row.OrderType,
      (object) row.RefNbr
    })).Any<PXResult<SOAdjust>>())
      throw new PXException("The {0} sales order cannot be deleted because at least one payment or prepayment invoice is linked to this sales order. To delete the sales order, first remove the payment or prepayment invoice application.", new object[1]
      {
        (object) row.RefNbr
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.cancelled> eventArgs)
  {
    PX.Objects.SO.SOOrder row = eventArgs.Row;
    if (row == null || !((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.cancelled>, PX.Objects.SO.SOOrder, object>) eventArgs).NewValue).GetValueOrDefault())
      return;
    if (((IQueryable<PXResult<SOAdjust>>) ((PXSelectBase<SOAdjust>) this.OpenAdjustingPrepaymentInvoices).Select(new object[2]
    {
      (object) row.OrderType,
      (object) row.RefNbr
    })).Any<PXResult<SOAdjust>>())
      throw new PXException("The {0} sales order cannot be canceled because at least one payment or prepayment invoice has been applied to it. Remove the application before canceling the order.", new object[1]
      {
        (object) row.RefNbr
      });
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2025 R1.")]
  public void CalculateApplicationBalance(PX.Objects.CM.CurrencyInfo inv_info, ARPayment payment, SOAdjust adj)
  {
  }

  [PXOverride]
  public Decimal GetPaymentBalance(
    PX.Objects.CM.CurrencyInfo inv_info,
    ARPayment payment,
    SOAdjust adj,
    Func<PX.Objects.CM.CurrencyInfo, ARPayment, SOAdjust, Decimal> baseMethod)
  {
    if (payment.IsPrepaymentInvoiceDocument() && payment.PendingPayment.GetValueOrDefault())
    {
      ARTranPostGL arTranPostGl = PXResultset<ARTranPostGL>.op_Implicit(PXSelectBase<ARTranPostGL, PXViewOf<ARTranPostGL>.BasedOn<SelectFromBase<ARTranPostGL, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPostGL.docType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<ARTranPostGL.refNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<ARTranPostGL.accountID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<ARTranPostGL.sourceDocType, IBqlString>.IsIn<ARDocType.prepaymentInvoice, ARDocType.creditMemo>>>.Aggregate<To<GroupBy<ARTranPostGL.docType>, GroupBy<ARTranPostGL.refNbr>, Sum<ARTranPostGL.curyBalanceAmt>, Sum<ARTranPostGL.balanceAmt>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[3]
      {
        (object) payment.DocType,
        (object) payment.RefNbr,
        (object) payment.ARAccountID
      }));
      payment.CuryDocBal = (Decimal?) arTranPostGl?.CuryBalanceAmt ?? payment.CuryOrigDocAmt;
      payment.DocBal = (Decimal?) arTranPostGl?.BalanceAmt ?? payment.OrigDocAmt;
    }
    return baseMethod(inv_info, payment, adj);
  }

  [PXOverride]
  public Decimal NewBalanceCalculation(
    SOAdjust adj,
    Decimal newValue,
    SOOrderEntryVATRecognitionOnPrepayments.NewBalanceCalculationDelegate baseMethod)
  {
    ARRegister doc = ARRegister.PK.Find((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, adj.AdjgDocType, adj.AdjgRefNbr);
    return doc == null || !doc.IsPrepaymentInvoiceDocument() || !doc.PendingPayment.GetValueOrDefault() ? baseMethod(adj, newValue) : doc.CuryOrigDocAmt.Value - newValue;
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2025 R1.")]
  public void UpdateBalanceOnCuryAdjdAmtUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e,
    Action<PXCache, PXFieldUpdatedEventArgs> baseMethod)
  {
    baseMethod(sender, e);
  }

  protected virtual void _(PX.Data.Events.RowSelected<SOAdjust> e)
  {
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current;
    SOAdjust row = e.Row;
    if (!(row?.AdjgDocType == "PPI") || !(current?.Behavior == "BL"))
      return;
    ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Adjustments).Cache.RaiseExceptionHandling<SOAdjust.adjgDocType>((object) e.Row, (object) row.AdjgDocType, (Exception) new PXSetPropertyException("Prepayment invoices are not supported for blanket sales orders."));
  }

  protected virtual void _(PX.Data.Events.RowPersisting<SOAdjust> e)
  {
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current;
    SOAdjust row = e.Row;
    if (row?.AdjgDocType == "PPI" && current?.Behavior == "BL" && EnumerableExtensions.IsNotIn<PXEntryStatus>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<SOAdjust>>) e).Cache.GetStatus((object) e.Row), (PXEntryStatus) 3, (PXEntryStatus) 4))
      throw new PXRowPersistingException(typeof (SOAdjust.adjgDocType).Name, (object) row.AdjgDocType, "Prepayment invoices are not supported for blanket sales orders.");
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<SOQuickPrepaymentInvoice> eventArgs)
  {
    SOQuickPrepaymentInvoice row = eventArgs.Row;
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current;
    if (current != null)
    {
      Decimal? nullable1;
      int num1;
      if (row == null)
      {
        num1 = 1;
      }
      else
      {
        nullable1 = row.PrepaymentPct;
        num1 = !nullable1.HasValue ? 1 : 0;
      }
      if (num1 == 0)
      {
        int num2;
        if (row == null)
        {
          num2 = 1;
        }
        else
        {
          nullable1 = row.CuryPrepaymentAmt;
          num2 = !nullable1.HasValue ? 1 : 0;
        }
        if (num2 == 0)
        {
          nullable1 = row.PrepaymentPct;
          Decimal num3 = 0M;
          Decimal? nullable2;
          int num4;
          if (nullable1.GetValueOrDefault() >= num3 & nullable1.HasValue)
          {
            nullable1 = row.PrepaymentPct;
            num3 = 100M;
            if (nullable1.GetValueOrDefault() <= num3 & nullable1.HasValue)
            {
              nullable1 = row.CuryPrepaymentAmt;
              Decimal num5 = 0M;
              if (nullable1.GetValueOrDefault() > num5 & nullable1.HasValue)
              {
                nullable1 = row.CuryPrepaymentAmt;
                nullable2 = current.CuryUnpaidBalance;
                num4 = nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue ? 1 : 0;
                goto label_15;
              }
            }
          }
          num4 = 0;
label_15:
          ((PXAction) this.createPrepaymentInvoiceOK).SetEnabled(num4 != 0);
          nullable2 = row.CuryPrepaymentAmt;
          Decimal num6 = 0M;
          if (nullable2.GetValueOrDefault() <= num6 & nullable2.HasValue)
          {
            ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPrepaymentInvoice>>) eventArgs).Cache.RaiseExceptionHandling<SOQuickPrepaymentInvoice.curyPrepaymentAmt>((object) row, (object) row.CuryPrepaymentAmt, (Exception) new PXSetPropertyException("The prepayment amount must be greater than zero.", (PXErrorLevel) 4));
            return;
          }
          nullable2 = row.CuryPrepaymentAmt;
          nullable1 = current.CuryUnpaidBalance;
          if (nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
          {
            ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPrepaymentInvoice>>) eventArgs).Cache.RaiseExceptionHandling<SOQuickPrepaymentInvoice.curyPrepaymentAmt>((object) row, (object) row.CuryPrepaymentAmt, (Exception) new PXSetPropertyException("The prepayment amount must be less than or equal to the order unpaid balance ({0}).", (PXErrorLevel) 4, new object[1]
            {
              (object) current.CuryUnpaidBalance
            }));
            return;
          }
          ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOQuickPrepaymentInvoice>>) eventArgs).Cache.RaiseExceptionHandling<SOQuickPrepaymentInvoice.curyPrepaymentAmt>((object) row, (object) row.CuryPrepaymentAmt, (Exception) null);
          return;
        }
      }
    }
    ((PXAction) this.createPrepaymentInvoiceOK).SetEnabled(false);
  }

  protected virtual void _(
    PX.Data.Events.RowUpdating<SOQuickPrepaymentInvoice> eventArgs)
  {
    PXCache cache = ((PX.Data.Events.Event<PXRowUpdatingEventArgs, PX.Data.Events.RowUpdating<SOQuickPrepaymentInvoice>>) eventArgs).Cache;
    SOQuickPrepaymentInvoice row = eventArgs.Row;
    SOQuickPrepaymentInvoice newRow = eventArgs.NewRow;
    if ((row != null ? (!row.CuryPrepaymentAmt.HasValue ? 1 : 0) : 1) != 0 || (newRow != null ? (!newRow.CuryPrepaymentAmt.HasValue ? 1 : 0) : 1) != 0 || (row != null ? (!row.PrepaymentPct.HasValue ? 1 : 0) : 1) != 0 || (newRow != null ? (!newRow.PrepaymentPct.HasValue ? 1 : 0) : 1) != 0)
      return;
    Decimal? nullable1 = newRow.CuryOrigDocAmt;
    Decimal num = 0M;
    if (nullable1.GetValueOrDefault() == num & nullable1.HasValue)
      return;
    nullable1 = row.CuryPrepaymentAmt;
    Decimal? nullable2 = newRow.CuryPrepaymentAmt;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
    {
      PXCache pxCache = cache;
      SOQuickPrepaymentInvoice prepaymentInvoice = newRow;
      num = 100M;
      nullable1 = newRow.CuryPrepaymentAmt;
      Decimal? nullable3 = newRow.CuryOrigDocAmt;
      nullable2 = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() / nullable3.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable4;
      if (!nullable2.HasValue)
      {
        nullable3 = new Decimal?();
        nullable4 = nullable3;
      }
      else
        nullable4 = new Decimal?(num * nullable2.GetValueOrDefault());
      // ISSUE: variable of a boxed type
      __Boxed<Decimal?> local = (ValueType) nullable4;
      pxCache.SetValueExt<SOQuickPrepaymentInvoice.prepaymentPct>((object) prepaymentInvoice, (object) local);
    }
    else
    {
      nullable2 = row.PrepaymentPct;
      Decimal? nullable5 = newRow.PrepaymentPct;
      if (nullable2.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable2.HasValue == nullable5.HasValue)
        return;
      PXCache pxCache = cache;
      SOQuickPrepaymentInvoice prepaymentInvoice = newRow;
      nullable5 = newRow.CuryOrigDocAmt;
      nullable1 = newRow.PrepaymentPct;
      num = 100M;
      nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() / num) : new Decimal?();
      Decimal? nullable6;
      if (!(nullable5.HasValue & nullable2.HasValue))
      {
        nullable1 = new Decimal?();
        nullable6 = nullable1;
      }
      else
        nullable6 = new Decimal?(nullable5.GetValueOrDefault() * nullable2.GetValueOrDefault());
      // ISSUE: variable of a boxed type
      __Boxed<Decimal?> local = (ValueType) nullable6;
      pxCache.SetValueExt<SOQuickPrepaymentInvoice.curyPrepaymentAmt>((object) prepaymentInvoice, (object) local);
    }
  }

  protected virtual void InitializeQuickPrepaymentInvoicePanel(PXGraph graph, string viewName)
  {
    this.SetDefaultValues(((PXSelectBase<SOQuickPrepaymentInvoice>) this.QuickPrepaymentInvoice).Current, ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current);
    ((PXSelectBase) this.QuickPrepaymentInvoice).Cache.RaiseRowSelected((object) ((PXSelectBase<SOQuickPrepaymentInvoice>) this.QuickPrepaymentInvoice).Current);
  }

  protected virtual void SetDefaultValues(SOQuickPrepaymentInvoice prepaymentInvoice, PX.Objects.SO.SOOrder order)
  {
    Decimal? unbilledOrderTotal1 = order.CuryUnbilledOrderTotal;
    Decimal num1 = 0M;
    Decimal? nullable1;
    Decimal? nullable2;
    Decimal? unbilledOrderTotal2;
    Decimal? nullable3;
    if (!(unbilledOrderTotal1.GetValueOrDefault() == num1 & unbilledOrderTotal1.HasValue))
    {
      Decimal? curyUnpaidBalance1 = order.CuryUnpaidBalance;
      Decimal num2 = 0M;
      if (!(curyUnpaidBalance1.GetValueOrDefault() == num2 & curyUnpaidBalance1.HasValue))
      {
        Decimal? curyOrderTotal1 = order.CuryOrderTotal;
        Decimal num3 = 0M;
        if (!(curyOrderTotal1.GetValueOrDefault() == num3 & curyOrderTotal1.HasValue))
        {
          Decimal? curyUnpaidBalance2 = order.CuryUnpaidBalance;
          Decimal? curyOrderTotal2 = order.CuryOrderTotal;
          if (!(curyUnpaidBalance2.GetValueOrDefault() == curyOrderTotal2.GetValueOrDefault() & curyUnpaidBalance2.HasValue == curyOrderTotal2.HasValue))
          {
            nullable1 = order.CuryUnpaidBalance;
            num3 = 100M;
            nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num3) : new Decimal?();
            unbilledOrderTotal2 = order.CuryUnbilledOrderTotal;
            if (!(nullable2.HasValue & unbilledOrderTotal2.HasValue))
            {
              nullable1 = new Decimal?();
              nullable3 = nullable1;
              goto label_9;
            }
            nullable3 = new Decimal?(nullable2.GetValueOrDefault() / unbilledOrderTotal2.GetValueOrDefault());
            goto label_9;
          }
          Decimal? prepaymentReqPct = order.PrepaymentReqPct;
          Decimal num4 = 0M;
          nullable3 = !(prepaymentReqPct.GetValueOrDefault() == num4 & prepaymentReqPct.HasValue) ? order.PrepaymentReqPct : new Decimal?(100M);
          goto label_9;
        }
      }
    }
    nullable3 = new Decimal?(0M);
label_9:
    Decimal? nullable4 = nullable3;
    unbilledOrderTotal2 = order.CuryUnbilledOrderTotal;
    nullable2 = nullable4;
    Decimal? nullable5;
    if (!(unbilledOrderTotal2.HasValue & nullable2.HasValue))
    {
      nullable1 = new Decimal?();
      nullable5 = nullable1;
    }
    else
      nullable5 = new Decimal?(unbilledOrderTotal2.GetValueOrDefault() * nullable2.GetValueOrDefault() / 100M);
    Decimal? nullable6 = nullable5;
    PXCache cache = ((PXSelectBase) this.QuickPrepaymentInvoice).Cache;
    cache.SetValueExt<SOQuickPrepaymentInvoice.curyID>((object) prepaymentInvoice, (object) order.CuryID);
    cache.SetValueExt<SOQuickPrepaymentInvoice.curyInfoID>((object) prepaymentInvoice, (object) order.CuryInfoID);
    cache.SetValueExt<SOQuickPrepaymentInvoice.curyOrigDocAmt>((object) prepaymentInvoice, (object) order.CuryUnbilledOrderTotal);
    cache.SetValueExt<SOQuickPrepaymentInvoice.curyPrepaymentAmt>((object) prepaymentInvoice, (object) nullable6);
    cache.SetValueExt<SOQuickPrepaymentInvoice.prepaymentPct>((object) prepaymentInvoice, (object) nullable4);
  }

  protected virtual ARInvoice FillPrepaymentInvoice(
    ARInvoiceEntry invoiceEntry,
    ARInvoice newdoc,
    PX.Objects.SO.SOOrder soOrder,
    SOQuickPrepaymentInvoice prepaymentParams)
  {
    TaxCalc taxCalc = TaxBaseAttribute.GetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) invoiceEntry.Transactions).Cache, (object) null);
    newdoc.BranchID = soOrder.BranchID;
    if (!string.IsNullOrEmpty(soOrder.FinPeriodID))
      newdoc.FinPeriodID = soOrder.FinPeriodID;
    newdoc.CustomerID = soOrder.CustomerID;
    newdoc.CuryID = soOrder.CuryID;
    newdoc.ProjectID = soOrder.ProjectID;
    newdoc.CustomerLocationID = soOrder.CustomerLocationID;
    newdoc.TaxZoneID = soOrder.TaxZoneID;
    newdoc.TaxCalcMode = soOrder.TaxCalcMode;
    newdoc.ExternalTaxExemptionNumber = soOrder.ExternalTaxExemptionNumber;
    newdoc.AvalaraCustomerUsageType = soOrder.AvalaraCustomerUsageType;
    newdoc.DocDesc = soOrder.OrderDesc;
    newdoc.InvoiceNbr = soOrder.CustomerOrderNbr;
    newdoc.DisableAutomaticTaxCalculation = soOrder.DisableAutomaticTaxCalculation;
    newdoc.DisableAutomaticDiscountCalculation = new bool?(true);
    ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (ARInvoice)].GetExtension<ARInvoiceVATRecognitionOnPrepayments>((object) newdoc).CuryPrepaymentAmt = prepaymentParams.CuryPrepaymentAmt;
    Decimal? nullable;
    if (((PXSelectBase<Customer>) ((PXGraphExtension<SOOrderEntry>) this).Base.customer).Current?.TermsID != null)
    {
      PX.Objects.CS.Terms terms = PX.Objects.CS.Terms.PK.Find((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, ((PXSelectBase<Customer>) ((PXGraphExtension<SOOrderEntry>) this).Base.customer).Current.TermsID);
      if (terms != null)
      {
        nullable = terms.DiscPercent;
        Decimal num = 0M;
        if (!(nullable.GetValueOrDefault() == num & nullable.HasValue) || terms.InstallmentType == "M")
          newdoc.TermsID = (string) null;
      }
    }
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) invoiceEntry.currencyinfo).Select(Array.Empty<object>()));
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<PX.Objects.SO.SOOrder.curyInfoID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, Array.Empty<object>()));
    SOOrderType soOrderType = SOOrderType.PK.Find((PXGraph) invoiceEntry, soOrder.OrderType);
    nullable = currencyInfo1.CuryRate;
    if (nullable.GetValueOrDefault() == 0M || soOrderType.UseCuryRateFromSO.GetValueOrDefault())
    {
      PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.RestoreCopy(currencyInfo1, currencyInfo2);
      currencyInfo1.CuryInfoID = newdoc.CuryInfoID;
    }
    else
    {
      currencyInfo1.CuryRateTypeID = currencyInfo2.CuryRateTypeID;
      ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) invoiceEntry.currencyinfo).Update(currencyInfo1);
    }
    newdoc = ((PXSelectBase<ARInvoice>) invoiceEntry.Document).Update(newdoc);
    TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) invoiceEntry.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
    ARTran details;
    using (new ARInvoiceEntry.SuppressRecalculateDiscountScope())
      details = this.CreateDetails(invoiceEntry, soOrder, prepaymentParams);
    if (!((PXSelectBase<ARTran>) invoiceEntry.Transactions).Any<ARTran>())
      throw new PXException("The sales order contains no lines for the creation of a prepayment invoice.");
    this.CreateFreightDetail(invoiceEntry, newdoc, soOrder, prepaymentParams);
    this.CreateGroupAndDocumentDiscountDetails(invoiceEntry, newdoc, soOrder);
    this.CreateTaxes(invoiceEntry, newdoc);
    if (details != null)
      this.ProcessRoundingDiff(invoiceEntry, newdoc, prepaymentParams, details, taxCalc);
    TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) invoiceEntry.Transactions).Cache, (object) null, taxCalc);
    newdoc.CuryOrigDocAmt = newdoc.CuryDocBal;
    newdoc.OrigDocAmt = newdoc.DocBal;
    return newdoc;
  }

  protected virtual ARTran CreateDetails(
    ARInvoiceEntry invoiceEntry,
    PX.Objects.SO.SOOrder soOrder,
    SOQuickPrepaymentInvoice prepaymentParams)
  {
    ARTran details = (ARTran) null;
    foreach (PXResult<SOLine> pxResult in ((PXSelectBase<SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Select(Array.Empty<object>()))
    {
      SOLine orderline = PXResult<SOLine>.op_Implicit(pxResult);
      Decimal? nullable1 = orderline.UnbilledQty;
      Decimal num1 = 0M;
      if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
      {
        nullable1 = orderline.CuryUnbilledAmt;
        Decimal num2 = 0M;
        if (nullable1.GetValueOrDefault() == num2 & nullable1.HasValue)
          continue;
      }
      ARTran arTran1 = new ARTran();
      arTran1.LineType = orderline.LineType;
      arTran1.BranchID = orderline.BranchID;
      arTran1.AccountID = orderline.SalesAcctID;
      arTran1.SubID = orderline.SalesSubID;
      arTran1.InventoryID = orderline.InventoryID;
      arTran1.SiteID = orderline.SiteID;
      arTran1.ProjectID = orderline.ProjectID;
      arTran1.TaskID = orderline.TaskID;
      arTran1.UOM = orderline.UOM;
      arTran1.Qty = orderline.UnbilledQty;
      arTran1.BaseQty = orderline.BaseUnbilledQty;
      arTran1.CuryUnitPrice = orderline.CuryUnitPrice;
      arTran1.CuryTranAmt = orderline.CuryUnbilledAmt;
      Decimal? nullable2 = new Decimal?(0M);
      nullable1 = orderline.UnbilledQty;
      Decimal num3 = 0M;
      Decimal? nullable3;
      if (!(nullable1.GetValueOrDefault() == num3 & nullable1.HasValue))
      {
        nullable1 = orderline.UnbilledQty;
        nullable3 = orderline.CuryUnitPrice;
        nullable2 = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable3.GetValueOrDefault()) : new Decimal?();
      }
      else
      {
        nullable3 = orderline.CuryUnbilledAmt;
        Decimal num4 = 0M;
        if (!(nullable3.GetValueOrDefault() == num4 & nullable3.HasValue))
          nullable2 = orderline.CuryExtPrice;
      }
      arTran1.CuryExtPrice = nullable2;
      arTran1.TranDesc = orderline.TranDesc;
      arTran1.TaxCategoryID = orderline.TaxCategoryID;
      arTran1.AvalaraCustomerUsageType = orderline.AvalaraCustomerUsageType;
      arTran1.DiscPct = orderline.DiscPct;
      ARTran arTran2 = arTran1;
      nullable3 = arTran1.CuryExtPrice;
      nullable1 = arTran1.DiscPct;
      Decimal? nullable4 = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * nullable1.GetValueOrDefault() / 100M) : new Decimal?();
      arTran2.CuryDiscAmt = nullable4;
      arTran1.IsFree = orderline.IsFree;
      arTran1.ManualPrice = new bool?(true);
      arTran1.ManualDisc = new bool?(true);
      arTran1.FreezeManualDisc = new bool?(true);
      arTran1.AutomaticDiscountsDisabled = new bool?(true);
      arTran1.DiscountID = orderline.DiscountID;
      arTran1.DiscountSequenceID = orderline.DiscountSequenceID;
      arTran1.DRTermStartDate = orderline.DRTermStartDate;
      arTran1.DRTermEndDate = orderline.DRTermEndDate;
      arTran1.CuryUnitPriceDR = orderline.CuryUnitPriceDR;
      arTran1.DiscPctDR = orderline.DiscPctDR;
      arTran1.DefScheduleID = orderline.DefScheduleID;
      arTran1.SortOrder = orderline.SortOrder;
      arTran1.OrigInvoiceType = orderline.InvoiceType;
      arTran1.OrigInvoiceNbr = orderline.InvoiceNbr;
      arTran1.OrigInvoiceLineNbr = orderline.InvoiceLineNbr;
      arTran1.OrigInvoiceDate = orderline.InvoiceDate;
      arTran1.CostCodeID = orderline.CostCodeID;
      arTran1.BlanketType = orderline.BlanketType;
      arTran1.BlanketNbr = orderline.BlanketNbr;
      arTran1.BlanketLineNbr = orderline.BlanketLineNbr;
      arTran1.BlanketSplitLineNbr = orderline.BlanketSplitLineNbr;
      arTran1.SOOrderType = orderline.OrderType;
      arTran1.SOOrderNbr = orderline.OrderNbr;
      ARTran tranMax = ((PXSelectBase<ARTran>) invoiceEntry.Transactions).Insert(arTran1);
      if (PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>())
      {
        tranMax.OrigGroupDiscountRate = orderline.GroupDiscountRate;
        tranMax.OrigDocumentDiscountRate = orderline.DocumentDiscountRate;
      }
      else
      {
        tranMax.GroupDiscountRate = orderline.GroupDiscountRate;
        tranMax.DocumentDiscountRate = orderline.DocumentDiscountRate;
      }
      ARTranVATRecognitionOnPrepayments extension = ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (ARTran)].GetExtension<ARTranVATRecognitionOnPrepayments>((object) tranMax);
      extension.PrepaymentPct = prepaymentParams.PrepaymentPct;
      ARTranVATRecognitionOnPrepayments recognitionOnPrepayments = extension;
      IPXCurrencyHelper implementation1 = ((PXGraph) invoiceEntry).FindImplementation<IPXCurrencyHelper>();
      nullable1 = orderline.CuryUnbilledAmt;
      Decimal num5 = nullable1.Value;
      nullable1 = prepaymentParams.PrepaymentPct;
      Decimal num6 = nullable1.Value;
      Decimal val1 = num5 * num6 / 100M;
      Decimal? nullable5 = new Decimal?(implementation1.RoundCury(val1));
      recognitionOnPrepayments.CuryPrepaymentAmt = nullable5;
      ARTran arTran3 = tranMax;
      IPXCurrencyHelper implementation2 = ((PXGraph) invoiceEntry).FindImplementation<IPXCurrencyHelper>();
      nullable1 = orderline.CuryUnbilledAmt;
      Decimal num7 = nullable1.Value + this.GetPerUnitTaxAmt(orderline);
      nullable1 = prepaymentParams.PrepaymentPct;
      Decimal num8 = nullable1.Value;
      Decimal val2 = num7 * num8 / 100M;
      Decimal? nullable6 = new Decimal?(implementation2.RoundCury(val2));
      arTran3.CuryTranAmt = nullable6;
      nullable1 = extension.CuryPrepaymentAmt;
      Decimal num9 = 0M;
      if (!(nullable1.GetValueOrDefault() == num9 & nullable1.HasValue))
      {
        nullable1 = extension.CuryPrepaymentAmt;
        Decimal num10 = Math.Abs(nullable1.Value);
        nullable1 = tranMax.CuryExtPrice;
        Decimal num11 = nullable1.Value;
        nullable1 = tranMax.CuryDiscAmt;
        Decimal num12 = nullable1.Value;
        Decimal num13 = Math.Abs(num11 - num12);
        Decimal num14 = num10 - num13;
        nullable1 = extension.CuryPrepaymentAmt;
        Decimal num15 = nullable1.Value;
        nullable1 = extension.CuryPrepaymentAmt;
        Decimal num16 = Math.Abs(nullable1.Value);
        Decimal diffTaxable = num14 * (num15 / num16);
        if (num14 > 0M)
          this.DistributeDiffBetweenExtPriceAndDiscount(tranMax, diffTaxable);
      }
      using (new DisableFormulaCalculationScope(((PXSelectBase) invoiceEntry.Transactions).Cache, new Type[3]
      {
        typeof (ARTranVATRecognitionOnPrepayments.prepaymentPct),
        typeof (ARTranVATRecognitionOnPrepayments.curyPrepaymentAmt),
        typeof (ARTran.curyTranAmt)
      }))
        tranMax = ((PXSelectBase<ARTran>) invoiceEntry.Transactions).Update(tranMax);
      if (details != null)
      {
        nullable1 = tranMax.CuryTranAmt;
        Decimal num17 = Math.Abs(nullable1.Value);
        nullable1 = details.CuryTranAmt;
        Decimal num18 = Math.Abs(nullable1.Value);
        if (!(num17 > num18))
          continue;
      }
      details = tranMax;
    }
    return details;
  }

  protected virtual Decimal GetPerUnitTaxAmt(SOLine orderline)
  {
    return orderline.OrderQty.Value == 0M ? 0M : this.GetPerUnitSOTaxDetails(orderline).Sum<SOTax>((Func<SOTax, Decimal>) (_ => _.CuryTaxAmt.Value)) * orderline.UnbilledQty.Value / orderline.OrderQty.Value;
  }

  protected virtual IEnumerable<SOTax> GetPerUnitSOTaxDetails(SOLine orderLine)
  {
    SOOrderEntryVATRecognitionOnPrepayments recognitionOnPrepayments = this;
    ParameterExpression instance;
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    IQueryable<PXResult<SOTaxTran>> source = ((IQueryable<PXResult<SOTaxTran>>) ((PXSelectBase<SOTaxTran>) ((PXGraphExtension<SOOrderEntry>) recognitionOnPrepayments).Base.Taxes).Select(Array.Empty<object>())).Where<PXResult<SOTaxTran>>(Expression.Lambda<Func<PXResult<SOTaxTran>, bool>>((Expression) Expression.AndAlso((Expression) Expression.Equal((Expression) Expression.Property((Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PX.Objects.TX.Tax.get_TaxType))), (Expression) Expression.Constant((object) "Q", typeof (string))), (Expression) Expression.NotEqual((Expression) Expression.Property((Expression) Expression.Call((Expression) instance, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PX.Objects.TX.Tax.get_TaxCalcLevel))), (Expression) Expression.Constant((object) "0", typeof (string)))), instance));
    ParameterExpression parameterExpression1;
    // ISSUE: method reference
    // ISSUE: method reference
    Expression<Func<PXResult<SOTaxTran>, string>> selector = Expression.Lambda<Func<PXResult<SOTaxTran>, string>>((Expression) Expression.Property((Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (TaxDetail.get_TaxID))), parameterExpression1);
    foreach (string str in (IEnumerable<string>) source.Select<PXResult<SOTaxTran>, string>(selector))
    {
      string taxID = str;
      ParameterExpression parameterExpression2;
      // ISSUE: method reference
      SOTax perUnitSoTaxDetail = ((IQueryable<PXResult<SOTax>>) ((PXSelectBase<SOTax>) ((PXGraphExtension<SOOrderEntry>) recognitionOnPrepayments).Base.Tax_Rows).Select(Array.Empty<object>())).Select<PXResult<SOTax>, SOTax>(Expression.Lambda<Func<PXResult<SOTax>, SOTax>>((Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), parameterExpression2)).SingleOrDefault<SOTax>((Expression<Func<SOTax, bool>>) (_ => _.TaxID == taxID && _.LineNbr == orderLine.LineNbr));
      if (perUnitSoTaxDetail != null && perUnitSoTaxDetail.CuryTaxAmt.HasValue)
        yield return perUnitSoTaxDetail;
    }
  }

  protected virtual void CreateFreightDetail(
    ARInvoiceEntry invoiceEntry,
    ARInvoice newdoc,
    PX.Objects.SO.SOOrder soOrder,
    SOQuickPrepaymentInvoice prepaymentParams)
  {
    Decimal? unbilledFreightTot = soOrder.CuryUnbilledFreightTot;
    Decimal num = 0M;
    if (!(unbilledFreightTot.GetValueOrDefault() > num & unbilledFreightTot.HasValue))
      return;
    ARTran arTran1 = new ARTran();
    arTran1.LineType = "FR";
    arTran1.SOOrderType = soOrder.OrderType;
    arTran1.SOOrderNbr = soOrder.OrderNbr;
    arTran1.BranchID = soOrder.BranchID;
    arTran1.CuryInfoID = soOrder.CuryInfoID;
    ARTran arTran2 = PXResult<ARTran>.op_Implicit(((IQueryable<PXResult<ARTran>>) ((PXSelectBase<ARTran>) invoiceEntry.Transactions).Select(Array.Empty<object>())).First<PXResult<ARTran>>());
    arTran1.AccountID = (int?) arTran2?.AccountID;
    arTran1.SubID = (int?) arTran2?.SubID;
    arTran1.Qty = new Decimal?((Decimal) 1);
    arTran1.CuryUnitPrice = soOrder.CuryUnbilledFreightTot;
    arTran1.CuryExtPrice = soOrder.CuryUnbilledFreightTot;
    arTran1.ProjectID = soOrder.ProjectID;
    arTran1.Commissionable = new bool?(false);
    if (CostCodeAttribute.UseCostCode())
      arTran1.CostCodeID = CostCodeAttribute.DefaultCostCode;
    using (new PXLocaleScope(((PXSelectBase<Customer>) ((PXGraphExtension<SOOrderEntry>) this).Base.customer).Current.LocaleName))
    {
      if (string.IsNullOrEmpty(soOrder.ShipVia))
        arTran1.TranDesc = PXMessages.LocalizeNoPrefix("Freight");
      else
        arTran1.TranDesc = PXMessages.LocalizeFormatNoPrefix("Freight ShipVia {0}", new object[1]
        {
          (object) soOrder.ShipVia
        });
    }
    ARTran arTran3 = ((PXSelectBase<ARTran>) invoiceEntry.Transactions).Insert(arTran1);
    arTran3.TaxCategoryID = soOrder.FreightTaxCategoryID;
    ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (ARTran)].GetExtension<ARTranVATRecognitionOnPrepayments>((object) arTran3).PrepaymentPct = prepaymentParams.PrepaymentPct;
    ((PXSelectBase<ARTran>) invoiceEntry.Transactions).Update(arTran3);
    newdoc.CuryFreightAmt = soOrder.CuryUnbilledFreightTot;
    newdoc.CuryFreightTot = soOrder.CuryUnbilledFreightTot;
    newdoc = ((PXSelectBase<ARInvoice>) invoiceEntry.Document).Update(newdoc);
  }

  /// <summary>
  /// This method prepares group and document discounts for Prepayment Invoice. Discounts are either prorated from the originating SO to the AR document, or recalculated on the AR level.
  /// </summary>
  /// <param name="args">InvoiceOrderArgs contains original SOOrder, SOLines, SOShipLines, SOOrderShipment, etc. that were passed to the main InvoiceOrder method</param>
  public virtual void CreateGroupAndDocumentDiscountDetails(
    ARInvoiceEntry invoiceEntry,
    ARInvoice newdoc,
    PX.Objects.SO.SOOrder soOrder)
  {
    bool discountsFeatureEnabled = PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>();
    ARInvoiceDiscountDetail draftDiscountDetail1 = this.CreateDraftDiscountDetail((ARRegister) newdoc, soOrder, "G", discountsFeatureEnabled);
    ARInvoiceDiscountDetail draftDiscountDetail2 = this.CreateDraftDiscountDetail((ARRegister) newdoc, soOrder, "D", discountsFeatureEnabled);
    ARInvoiceDiscountDetail.ARInvoiceDiscountDetailComparer discountDetailComparer = new ARInvoiceDiscountDetail.ARInvoiceDiscountDetailComparer();
    ARInvoiceDiscountDetail.ARInvoiceDiscountDetailComparerNoID detailComparerNoId = new ARInvoiceDiscountDetail.ARInvoiceDiscountDetailComparerNoID();
    TwoWayLookup<ARInvoiceDiscountDetail, ARTran> twoWayLookup = new TwoWayLookup<ARInvoiceDiscountDetail, ARTran>((IEqualityComparer<ARInvoiceDiscountDetail>) discountDetailComparer, (IEqualityComparer<ARTran>) null, (Func<ARInvoiceDiscountDetail, ARTran, bool>) null);
    ParameterExpression parameterExpression;
    // ISSUE: method reference
    // ISSUE: method reference
    bool hasPerUnitTax = ((IQueryable<PXResult<SOTaxTran>>) ((PXSelectBase<SOTaxTran>) ((PXGraphExtension<SOOrderEntry>) this).Base.Taxes).Select(Array.Empty<object>())).Select<PXResult<SOTaxTran>, bool>(Expression.Lambda<Func<PXResult<SOTaxTran>, bool>>((Expression) Expression.Equal((Expression) Expression.Property((Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PX.Objects.TX.Tax.get_TaxType))), (Expression) Expression.Constant((object) "Q", typeof (string))), parameterExpression)).Any<bool>();
    foreach (PXResult<ARTran> pxResult in ((PXSelectBase<ARTran>) invoiceEntry.Transactions).Select(Array.Empty<object>()))
    {
      ARTran tran = PXResult<ARTran>.op_Implicit(pxResult);
      if ((!discountsFeatureEnabled ? 1 : (!(soOrder.OrderType == tran.SOOrderType) ? 0 : (soOrder.OrderNbr == tran.SOOrderNbr ? 1 : 0))) != 0)
        SOOrderEntryVATRecognitionOnPrepayments.SetDiscountAmounts(((PXSelectBase) invoiceEntry.ARDiscountDetails).Cache, draftDiscountDetail1, draftDiscountDetail2, twoWayLookup, tran, discountsFeatureEnabled, hasPerUnitTax);
    }
    foreach (ARInvoiceDiscountDetail leftValue1 in twoWayLookup.LeftValues)
    {
      ARInvoiceDiscountDetail invoiceDiscountDetail1 = (ARInvoiceDiscountDetail) null;
      foreach (PXResult<ARInvoiceDiscountDetail> pxResult in ((PXSelectBase<ARInvoiceDiscountDetail>) invoiceEntry.ARDiscountDetails).Select(Array.Empty<object>()))
      {
        ARInvoiceDiscountDetail invoiceDiscountDetail2 = PXResult<ARInvoiceDiscountDetail>.op_Implicit(pxResult);
        if (invoiceDiscountDetail2.DiscountID == leftValue1.DiscountID && invoiceDiscountDetail2.DiscountSequenceID == leftValue1.DiscountSequenceID && (!discountsFeatureEnabled || invoiceDiscountDetail2.OrderNbr == leftValue1.OrderNbr && invoiceDiscountDetail2.OrderType == leftValue1.OrderType) && invoiceDiscountDetail2.RefNbr == leftValue1.RefNbr && invoiceDiscountDetail2.Type == leftValue1.Type)
        {
          invoiceDiscountDetail1 = invoiceDiscountDetail2;
          break;
        }
      }
      leftValue1.DiscountPct = leftValue1.CuryDiscountAmt.IsNullOrZero() || leftValue1.CuryDiscountableAmt.IsNullOrZero() ? new Decimal?(0M) : new Decimal?(leftValue1.CuryDiscountAmt.GetValueOrDefault() / (leftValue1.CuryDiscountableAmt ?? 1M) * 100M);
      ARInvoiceDiscountDetail discountDetail2;
      if (invoiceDiscountDetail1 != null)
      {
        ((PXSelectBase) invoiceEntry.ARDiscountDetails).Cache.SetValueExt<ARInvoiceDiscountDetail.discountAmt>((object) invoiceDiscountDetail1, (object) leftValue1.DiscountAmt);
        ((PXSelectBase) invoiceEntry.ARDiscountDetails).Cache.SetValueExt<ARInvoiceDiscountDetail.curyDiscountAmt>((object) invoiceDiscountDetail1, (object) leftValue1.CuryDiscountAmt);
        ((PXSelectBase) invoiceEntry.ARDiscountDetails).Cache.SetValueExt<ARInvoiceDiscountDetail.discountableAmt>((object) invoiceDiscountDetail1, (object) leftValue1.DiscountableAmt);
        ((PXSelectBase) invoiceEntry.ARDiscountDetails).Cache.SetValueExt<ARInvoiceDiscountDetail.curyDiscountableAmt>((object) invoiceDiscountDetail1, (object) leftValue1.CuryDiscountableAmt);
        ((PXSelectBase) invoiceEntry.ARDiscountDetails).Cache.SetValueExt<ARInvoiceDiscountDetail.discountableQty>((object) invoiceDiscountDetail1, (object) leftValue1.DiscountableQty);
        ((PXSelectBase) invoiceEntry.ARDiscountDetails).Cache.SetValueExt<ARInvoiceDiscountDetail.discountPct>((object) invoiceDiscountDetail1, (object) leftValue1.DiscountPct);
        discountDetail2 = ((PXSelectBase) invoiceEntry.ARDiscountDetails).Cache.GetStatus((object) invoiceDiscountDetail1) != 3 ? this.ARDiscountEngine.UpdateDiscountDetail(((PXSelectBase) invoiceEntry.ARDiscountDetails).Cache, (PXSelectBase<ARInvoiceDiscountDetail>) invoiceEntry.ARDiscountDetails, invoiceDiscountDetail1) : this.ARDiscountEngine.InsertDiscountDetail(((PXSelectBase) invoiceEntry.ARDiscountDetails).Cache, (PXSelectBase<ARInvoiceDiscountDetail>) invoiceEntry.ARDiscountDetails, invoiceDiscountDetail1);
      }
      else
        discountDetail2 = this.ARDiscountEngine.InsertDiscountDetail(((PXSelectBase) invoiceEntry.ARDiscountDetails).Cache, (PXSelectBase<ARInvoiceDiscountDetail>) invoiceEntry.ARDiscountDetails, leftValue1);
      if (discountDetail2 != null)
      {
        foreach (ARInvoiceDiscountDetail leftValue2 in twoWayLookup.LeftValues)
        {
          if (detailComparerNoId.Equals(leftValue2, discountDetail2))
            leftValue2.LineNbr = discountDetail2.LineNbr;
        }
      }
    }
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions = this.GetDefaultARDiscountCalculationOptions(((PXSelectBase<ARInvoice>) invoiceEntry.Document).Current) | DiscountEngine.DiscountCalculationOptions.DisableOrigAutomaticDiscounts;
    this.ARDiscountEngine.UpdateListOfDiscountsAppliedToLines(((PXSelectBase) invoiceEntry.Transactions).Cache, twoWayLookup, discountCalculationOptions);
    this.RecalculateTotalDiscount(invoiceEntry);
  }

  public void RecalculateTotalDiscount(ARInvoiceEntry invoiceEntry)
  {
    if (((PXSelectBase<ARInvoice>) invoiceEntry.Document).Current == null)
      return;
    ARInvoice copy = PXCache<ARInvoice>.CreateCopy(((PXSelectBase<ARInvoice>) invoiceEntry.Document).Current);
    ((PXSelectBase) invoiceEntry.Document).Cache.SetValueExt<ARInvoice.curyDiscTot>((object) ((PXSelectBase<ARInvoice>) invoiceEntry.Document).Current, (object) this.ARDiscountEngine.GetTotalGroupAndDocumentDiscount<ARInvoiceDiscountDetail>((PXSelectBase<ARInvoiceDiscountDetail>) invoiceEntry.ARDiscountDetails));
    ((PXSelectBase) invoiceEntry.Document).Cache.RaiseRowUpdated((object) ((PXSelectBase<ARInvoice>) invoiceEntry.Document).Current, (object) copy);
  }

  public virtual DiscountEngine.DiscountCalculationOptions GetDefaultARDiscountCalculationOptions(
    ARInvoice doc)
  {
    return this.GetDefaultARDiscountCalculationOptions(doc, false);
  }

  public virtual DiscountEngine.DiscountCalculationOptions GetDefaultARDiscountCalculationOptions(
    ARInvoice doc,
    bool doNotDeferDiscountCalculation)
  {
    DiscountEngine.DiscountCalculationOptions calculationOptions = (DiscountEngine.DiscountCalculationOptions) (16 /*0x10*/ | (doc == null || !doc.DisableAutomaticDiscountCalculation.GetValueOrDefault() ? 0 : 4));
    if (doc == null || !doc.DeferPriceDiscountRecalculation.GetValueOrDefault() || doNotDeferDiscountCalculation)
      return calculationOptions;
    doc.IsPriceAndDiscountsValid = new bool?(false);
    return calculationOptions | DiscountEngine.DiscountCalculationOptions.DisablePriceCalculation | DiscountEngine.DiscountCalculationOptions.DisableGroupAndDocumentDiscounts | DiscountEngine.DiscountCalculationOptions.DisableARDiscountsCalculation | DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation;
  }

  private static void SetDiscountAmounts(
    PXCache cache,
    ARInvoiceDiscountDetail draftGroupDiscountDetail,
    ARInvoiceDiscountDetail draftDocumentDiscountDetail,
    TwoWayLookup<ARInvoiceDiscountDetail, ARTran> discountCodesWithApplicableARLines,
    ARTran tran,
    bool discountsFeatureEnabled,
    bool hasPerUnitTax)
  {
    Decimal? nullable1 = discountsFeatureEnabled ? tran.OrigGroupDiscountRate : tran.GroupDiscountRate;
    Decimal? nullable2 = discountsFeatureEnabled ? tran.OrigDocumentDiscountRate : tran.DocumentDiscountRate;
    Decimal? nullable3 = tran.Qty;
    Decimal num1 = 0M;
    Decimal valueOrDefault1;
    Decimal valueOrDefault2;
    if (!(nullable3.GetValueOrDefault() == num1 & nullable3.HasValue) & hasPerUnitTax)
    {
      ARTranVATRecognitionOnPrepayments extension = cache.Graph.Caches["ARTran"].GetExtension<ARTranVATRecognitionOnPrepayments>((object) tran);
      nullable3 = extension.CuryPrepaymentAmt;
      valueOrDefault1 = nullable3.GetValueOrDefault();
      nullable3 = extension.PrepaymentAmt;
      valueOrDefault2 = nullable3.GetValueOrDefault();
    }
    else
    {
      nullable3 = tran.CuryTranAmt;
      valueOrDefault1 = nullable3.GetValueOrDefault();
      nullable3 = tran.TranAmt;
      valueOrDefault2 = nullable3.GetValueOrDefault();
    }
    nullable3 = nullable1;
    Decimal num2 = (Decimal) 1;
    Decimal? nullable4;
    if (!(nullable3.GetValueOrDefault() == num2 & nullable3.HasValue))
    {
      Decimal num3 = valueOrDefault1 * (1M - nullable1.GetValueOrDefault());
      ARInvoiceDiscountDetail invoiceDiscountDetail1 = draftGroupDiscountDetail;
      nullable3 = invoiceDiscountDetail1.CuryDiscountAmt;
      Decimal num4 = num3;
      invoiceDiscountDetail1.CuryDiscountAmt = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + num4) : new Decimal?();
      ARInvoiceDiscountDetail invoiceDiscountDetail2 = draftGroupDiscountDetail;
      nullable3 = invoiceDiscountDetail2.CuryDiscountableAmt;
      Decimal num5 = PX.Objects.CM.PXCurrencyAttribute.RoundCury(cache, (object) draftGroupDiscountDetail, valueOrDefault1);
      invoiceDiscountDetail2.CuryDiscountableAmt = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + num5) : new Decimal?();
      Decimal num6 = valueOrDefault2 * (1M - nullable1.GetValueOrDefault());
      ARInvoiceDiscountDetail invoiceDiscountDetail3 = draftGroupDiscountDetail;
      nullable3 = invoiceDiscountDetail3.DiscountAmt;
      Decimal num7 = num6;
      invoiceDiscountDetail3.DiscountAmt = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + num7) : new Decimal?();
      ARInvoiceDiscountDetail invoiceDiscountDetail4 = draftGroupDiscountDetail;
      nullable3 = invoiceDiscountDetail4.DiscountableAmt;
      Decimal num8 = PX.Objects.CM.PXCurrencyAttribute.BaseRound(cache.Graph, valueOrDefault2);
      invoiceDiscountDetail4.DiscountableAmt = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + num8) : new Decimal?();
      ARInvoiceDiscountDetail invoiceDiscountDetail5 = draftGroupDiscountDetail;
      nullable3 = invoiceDiscountDetail5.DiscountableQty;
      nullable4 = tran.Qty;
      invoiceDiscountDetail5.DiscountableQty = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
      discountCodesWithApplicableARLines.Link(draftGroupDiscountDetail, tran);
    }
    nullable4 = nullable2;
    Decimal num9 = (Decimal) 1;
    if (nullable4.GetValueOrDefault() == num9 & nullable4.HasValue)
      return;
    Decimal val = valueOrDefault1 - valueOrDefault1 * (1M - (nullable1 ?? 1M));
    Decimal num10 = valueOrDefault2 - valueOrDefault2 * (1M - (nullable1 ?? 1M));
    Decimal num11 = val * (1M - nullable2.GetValueOrDefault());
    ARInvoiceDiscountDetail invoiceDiscountDetail6 = draftDocumentDiscountDetail;
    nullable4 = invoiceDiscountDetail6.CuryDiscountAmt;
    Decimal num12 = num11;
    Decimal? nullable5;
    if (!nullable4.HasValue)
    {
      nullable3 = new Decimal?();
      nullable5 = nullable3;
    }
    else
      nullable5 = new Decimal?(nullable4.GetValueOrDefault() + num12);
    invoiceDiscountDetail6.CuryDiscountAmt = nullable5;
    ARInvoiceDiscountDetail invoiceDiscountDetail7 = draftDocumentDiscountDetail;
    nullable4 = invoiceDiscountDetail7.CuryDiscountableAmt;
    Decimal num13 = PX.Objects.CM.PXCurrencyAttribute.RoundCury(cache, (object) draftGroupDiscountDetail, val);
    Decimal? nullable6;
    if (!nullable4.HasValue)
    {
      nullable3 = new Decimal?();
      nullable6 = nullable3;
    }
    else
      nullable6 = new Decimal?(nullable4.GetValueOrDefault() + num13);
    invoiceDiscountDetail7.CuryDiscountableAmt = nullable6;
    Decimal num14 = num10 * (1M - nullable2.GetValueOrDefault());
    ARInvoiceDiscountDetail invoiceDiscountDetail8 = draftDocumentDiscountDetail;
    nullable4 = invoiceDiscountDetail8.DiscountAmt;
    Decimal num15 = num14;
    Decimal? nullable7;
    if (!nullable4.HasValue)
    {
      nullable3 = new Decimal?();
      nullable7 = nullable3;
    }
    else
      nullable7 = new Decimal?(nullable4.GetValueOrDefault() + num15);
    invoiceDiscountDetail8.DiscountAmt = nullable7;
    ARInvoiceDiscountDetail invoiceDiscountDetail9 = draftDocumentDiscountDetail;
    nullable4 = invoiceDiscountDetail9.DiscountableAmt;
    Decimal num16 = PX.Objects.CM.PXCurrencyAttribute.BaseRound(cache.Graph, num10);
    Decimal? nullable8;
    if (!nullable4.HasValue)
    {
      nullable3 = new Decimal?();
      nullable8 = nullable3;
    }
    else
      nullable8 = new Decimal?(nullable4.GetValueOrDefault() + num16);
    invoiceDiscountDetail9.DiscountableAmt = nullable8;
    ARInvoiceDiscountDetail invoiceDiscountDetail10 = draftDocumentDiscountDetail;
    nullable4 = invoiceDiscountDetail10.DiscountableQty;
    nullable3 = tran.Qty;
    invoiceDiscountDetail10.DiscountableQty = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
    discountCodesWithApplicableARLines.Link(draftDocumentDiscountDetail, tran);
  }

  private ARInvoiceDiscountDetail CreateDraftDiscountDetail(
    ARRegister invoice,
    PX.Objects.SO.SOOrder soOrder,
    string discountType,
    bool discountsFeatureEnabled)
  {
    return new ARInvoiceDiscountDetail()
    {
      Type = discountsFeatureEnabled ? discountType : "B",
      DocType = invoice.DocType,
      RefNbr = invoice.RefNbr,
      OrderType = discountsFeatureEnabled ? soOrder?.OrderType : (string) null,
      OrderNbr = discountsFeatureEnabled ? soOrder?.OrderNbr : (string) null,
      CuryInfoID = invoice.CuryInfoID,
      CuryDiscountAmt = new Decimal?(0M),
      DiscountAmt = new Decimal?(0M),
      CuryDiscountableAmt = new Decimal?(0M),
      DiscountableAmt = new Decimal?(0M),
      DiscountableQty = new Decimal?(0M),
      IsOrigDocDiscount = new bool?(discountsFeatureEnabled),
      Description = discountsFeatureEnabled ? (discountType == "G" ? "Group discounts from the related sales order" : "Document discounts from the related sales order") : "Discount Total Adjustment"
    };
  }

  protected virtual void CreateTaxes(ARInvoiceEntry invoiceEntry, ARInvoice newdoc)
  {
    PXResultset<SOTaxTran> source = ((PXSelectBase<SOTaxTran>) ((PXGraphExtension<SOOrderEntry>) this).Base.Taxes).Select(Array.Empty<object>());
    ParameterExpression parameterExpression;
    // ISSUE: method reference
    Expression<Func<PXResult<SOTaxTran>, PX.Objects.TX.Tax>> selector = Expression.Lambda<Func<PXResult<SOTaxTran>, PX.Objects.TX.Tax>>((Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), parameterExpression);
    foreach (PX.Objects.TX.Tax tax in (IEnumerable<PX.Objects.TX.Tax>) ((IQueryable<PXResult<SOTaxTran>>) source).Select<PXResult<SOTaxTran>, PX.Objects.TX.Tax>(selector))
    {
      ARTaxTran arTaxTran1 = new ARTaxTran();
      arTaxTran1.Module = "AR";
      ARTaxTran arTaxTran2 = arTaxTran1;
      ((PXSelectBase) invoiceEntry.Taxes).Cache.SetDefaultExt<TaxTran.origTranType>((object) arTaxTran2);
      ((PXSelectBase) invoiceEntry.Taxes).Cache.SetDefaultExt<TaxTran.origRefNbr>((object) arTaxTran2);
      ((PXSelectBase) invoiceEntry.Taxes).Cache.SetDefaultExt<TaxTran.lineRefNbr>((object) arTaxTran2);
      arTaxTran2.TranType = newdoc.DocType;
      arTaxTran2.RefNbr = newdoc.RefNbr;
      arTaxTran2.TaxID = tax.TaxID;
      arTaxTran2.TaxRate = new Decimal?(0M);
      arTaxTran2.CuryID = newdoc.CuryID;
      if (tax.TaxType == "Q")
      {
        arTaxTran2.TaxRate = new Decimal?((Decimal) -1);
        ((PXSelectBase) invoiceEntry.Taxes).Cache.RaiseRowInserted((object) arTaxTran2);
      }
      else
        ((PXSelectBase<ARTaxTran>) invoiceEntry.Taxes).Insert(arTaxTran2);
    }
  }

  protected void ProcessRoundingDiff(
    ARInvoiceEntry invoiceEntry,
    ARInvoice newdoc,
    SOQuickPrepaymentInvoice prepaymentParams,
    ARTran tranMax,
    TaxCalc oldTaxCalc)
  {
    Decimal? nullable1 = prepaymentParams.CuryPrepaymentAmt;
    Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
    nullable1 = newdoc.CuryDocBal;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    Decimal diff1 = valueOrDefault1 - valueOrDefault2;
    Decimal diffTaxable = 0M;
    PXResult<ARTax, PX.Objects.TX.Tax> pxResult = (PXResult<ARTax, PX.Objects.TX.Tax>) null;
    Decimal? nullable2;
    if (diff1 != 0M)
    {
      PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = ((PXGraph) invoiceEntry).FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo();
      Decimal num1 = 1M;
      int num2 = 0;
      while (true)
      {
        int num3 = num2;
        short? curyPrecision = defaultCurrencyInfo.CuryPrecision;
        int? nullable3 = curyPrecision.HasValue ? new int?((int) curyPrecision.GetValueOrDefault()) : new int?();
        int valueOrDefault3 = nullable3.GetValueOrDefault();
        if (num3 < valueOrDefault3 & nullable3.HasValue)
        {
          num1 /= 10M;
          ++num2;
        }
        else
          break;
      }
      int num4 = ((IEnumerable<PXResult<ARTax>>) PXSelectBase<ARTax, PXViewOf<ARTax>.BasedOn<SelectFromBase<ARTax, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.TX.Tax>.On<BqlOperand<ARTax.taxID, IBqlString>.IsEqual<PX.Objects.TX.Tax.taxID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTax.tranType, Equal<BqlField<ARInvoice.docType, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTax.refNbr, Equal<BqlField<ARInvoice.refNbr, IBqlString>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.TX.Tax.taxCalcLevel, IBqlString>.IsNotEqual<CSTaxCalcLevel.inclusive>>>>>.Config>.Select((PXGraph) invoiceEntry, Array.Empty<object>())).AsEnumerable<PXResult<ARTax>>().Cast<PXResult<ARTax, PX.Objects.TX.Tax>>().Count<PXResult<ARTax, PX.Objects.TX.Tax>>();
      Decimal? nullable4 = new Decimal?(num1 * (Decimal) (((PXSelectBase) invoiceEntry.Transactions).Cache.Inserted.Count() + 2L + (long) num4));
      if (PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>() && newdoc.TaxCalcMode == "G")
      {
        Decimal num5 = Math.Abs(diff1);
        nullable2 = nullable4;
        Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
        if (num5 > valueOrDefault4 & nullable2.HasValue)
          return;
        diffTaxable = diff1;
      }
      else
      {
        IEnumerable<PXResult<ARTax, PX.Objects.TX.Tax>> pxResults = ((IEnumerable<PXResult<ARTax>>) PXSelectBase<ARTax, PXViewOf<ARTax>.BasedOn<SelectFromBase<ARTax, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.TX.Tax>.On<BqlOperand<ARTax.taxID, IBqlString>.IsEqual<PX.Objects.TX.Tax.taxID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTax.tranType, Equal<BqlField<ARInvoice.docType, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTax.refNbr, Equal<BqlField<ARInvoice.refNbr, IBqlString>.FromCurrent>>>>>.And<BqlOperand<ARTax.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>>.Config>.Select((PXGraph) invoiceEntry, new object[1]
        {
          (object) tranMax.LineNbr
        })).AsEnumerable<PXResult<ARTax>>().Cast<PXResult<ARTax, PX.Objects.TX.Tax>>();
        IEnumerable<PXResult<ARTax, PX.Objects.TX.Tax>> source = pxResults.Where<PXResult<ARTax, PX.Objects.TX.Tax>>((Func<PXResult<ARTax, PX.Objects.TX.Tax>, bool>) (row => PXResult<ARTax, PX.Objects.TX.Tax>.op_Implicit(row).TaxCalcLevel != "0"));
        Decimal num6 = Math.Abs(diff1);
        Decimal? nullable5 = nullable4;
        Decimal valueOrDefault5 = nullable5.GetValueOrDefault();
        if (num6 > valueOrDefault5 & nullable5.HasValue)
          return;
        if (source.Any<PXResult<ARTax, PX.Objects.TX.Tax>>())
        {
          pxResult = source.OrderByDescending<PXResult<ARTax, PX.Objects.TX.Tax>, Decimal>((Func<PXResult<ARTax, PX.Objects.TX.Tax>, Decimal>) (row => Math.Abs(PXResult<ARTax, PX.Objects.TX.Tax>.op_Implicit(row).CuryTaxAmt.GetValueOrDefault()))).First<PXResult<ARTax, PX.Objects.TX.Tax>>();
          diffTaxable = this.ProcessRoundingDiffToTaxable(invoiceEntry, pxResults, diff1, PXResult<ARTax, PX.Objects.TX.Tax>.op_Implicit(pxResult));
        }
        else
          diffTaxable = diff1;
      }
    }
    TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) invoiceEntry.Transactions).Cache, (object) null, oldTaxCalc);
    ARTran arTran1 = tranMax;
    nullable2 = arTran1.CuryTranAmt;
    Decimal num7 = diffTaxable;
    arTran1.CuryTranAmt = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num7) : new Decimal?();
    Decimal num8 = this.DistributeDiffBetweenExtPriceAndDiscount(tranMax, diffTaxable);
    ARTranVATRecognitionOnPrepayments extension = ((PXGraph) invoiceEntry).Caches[typeof (ARTran)].GetExtension<ARTranVATRecognitionOnPrepayments>((object) tranMax);
    nullable2 = extension.CuryPrepaymentAmt;
    Decimal num9 = num8;
    extension.CuryPrepaymentAmt = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num9) : new Decimal?();
    using (new DisableFormulaCalculationScope(((PXSelectBase) invoiceEntry.Transactions).Cache, new Type[3]
    {
      typeof (ARTranVATRecognitionOnPrepayments.curyPrepaymentAmt),
      typeof (ARTranVATRecognitionOnPrepayments.prepaymentPct),
      typeof (ARTran.curyTranAmt)
    }))
      ((PXSelectBase<ARTran>) invoiceEntry.Transactions).Update(tranMax);
    Decimal? nullable6 = prepaymentParams.CuryPrepaymentAmt;
    Decimal num10 = Math.Abs(nullable6.GetValueOrDefault());
    nullable6 = newdoc.CuryDocBal;
    Decimal valueOrDefault6 = nullable6.GetValueOrDefault();
    Decimal diff2 = num10 - valueOrDefault6;
    if (!(diff2 != 0M))
      return;
    TaxBaseAttribute.SetTaxCalc<ARTran.taxCategoryID>(((PXSelectBase) invoiceEntry.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
    if (pxResult == null)
    {
      ARInvoiceDiscountDetail invoiceDiscountDetail1 = PXResult<ARInvoiceDiscountDetail>.op_Implicit(((IQueryable<PXResult<ARInvoiceDiscountDetail>>) ((PXSelectBase<ARInvoiceDiscountDetail>) invoiceEntry.ARDiscountDetails).Select(Array.Empty<object>())).OrderByDescending<PXResult<ARInvoiceDiscountDetail>, Decimal>((Expression<Func<PXResult<ARInvoiceDiscountDetail>, Decimal>>) (row => Math.Abs(((ARInvoiceDiscountDetail) row).CuryDiscountAmt ?? 0M))).First<PXResult<ARInvoiceDiscountDetail>>());
      ARInvoiceDiscountDetail invoiceDiscountDetail2 = invoiceDiscountDetail1;
      Decimal? curyDiscountAmt = invoiceDiscountDetail2.CuryDiscountAmt;
      Decimal num11 = diff2;
      invoiceDiscountDetail2.CuryDiscountAmt = curyDiscountAmt.HasValue ? new Decimal?(curyDiscountAmt.GetValueOrDefault() - num11) : new Decimal?();
      ((PXSelectBase) invoiceEntry.ARDiscountDetails).Cache.Update((object) invoiceDiscountDetail1);
      invoiceEntry.RecalculateTotalDiscount();
    }
    else if (((PXResult) pxResult)?.GetItem<PX.Objects.TX.Tax>().TaxType == "Q")
    {
      ARTran arTran2 = tranMax;
      Decimal? curyTranAmt = arTran2.CuryTranAmt;
      Decimal num12 = diff2;
      arTran2.CuryTranAmt = curyTranAmt.HasValue ? new Decimal?(curyTranAmt.GetValueOrDefault() + num12) : new Decimal?();
      ((PXSelectBase) invoiceEntry.Transactions).Cache.Update((object) tranMax);
    }
    else
      this.ProcessRoundingDiffToTax(invoiceEntry, diff2, PXResult<ARTax, PX.Objects.TX.Tax>.op_Implicit(pxResult));
  }

  protected virtual Decimal DistributeDiffBetweenExtPriceAndDiscount(
    ARTran tranMax,
    Decimal diffTaxable)
  {
    Decimal? curyDiscAmt = tranMax.CuryDiscAmt;
    Decimal num1 = 0M;
    Decimal? nullable;
    if (!(curyDiscAmt.GetValueOrDefault() == num1 & curyDiscAmt.HasValue))
    {
      Decimal num2 = diffTaxable;
      nullable = tranMax.CuryDiscAmt;
      Decimal num3 = nullable.Value;
      if (num2 < num3)
      {
        ARTran arTran = tranMax;
        nullable = arTran.CuryDiscAmt;
        Decimal num4 = diffTaxable;
        arTran.CuryDiscAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() - num4) : new Decimal?();
        goto label_6;
      }
    }
    nullable = tranMax.CuryDiscAmt;
    Decimal num5 = 0M;
    if (!(nullable.GetValueOrDefault() == num5 & nullable.HasValue))
    {
      Decimal num6 = diffTaxable;
      nullable = tranMax.CuryDiscAmt;
      Decimal num7 = nullable.Value;
      if (num6 >= num7)
      {
        Decimal num8 = diffTaxable;
        nullable = tranMax.CuryDiscAmt;
        Decimal num9 = nullable.Value;
        diffTaxable = num8 - num9;
        tranMax.CuryDiscAmt = new Decimal?(0M);
      }
    }
label_6:
    nullable = tranMax.CuryDiscAmt;
    Decimal num10 = 0M;
    if (nullable.GetValueOrDefault() == num10 & nullable.HasValue)
    {
      ARTran arTran = tranMax;
      nullable = arTran.CuryExtPrice;
      Decimal num11 = diffTaxable;
      arTran.CuryExtPrice = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num11) : new Decimal?();
    }
    return diffTaxable;
  }

  protected virtual Decimal ProcessRoundingDiffToTaxable(
    ARInvoiceEntry invoiceEntry,
    IEnumerable<PXResult<ARTax, PX.Objects.TX.Tax>> taxLines,
    Decimal diff,
    ARTax maxTaxAmt)
  {
    Decimal num1 = 0M;
    Decimal taxable;
    if (maxTaxAmt != null)
    {
      taxable = ((PXGraph) invoiceEntry).FindImplementation<IPXCurrencyHelper>().RoundCury(diff / (1M + taxLines.Where<PXResult<ARTax, PX.Objects.TX.Tax>>((Func<PXResult<ARTax, PX.Objects.TX.Tax>, bool>) (row => PXResult<ARTax, PX.Objects.TX.Tax>.op_Implicit(row).TaxCalcLevel != "0" && ((PXResult) row).GetItem<PX.Objects.TX.Tax>().TaxType != "Q")).Sum<PXResult<ARTax, PX.Objects.TX.Tax>>((Func<PXResult<ARTax, PX.Objects.TX.Tax>, Decimal>) (row => PXResult<ARTax, PX.Objects.TX.Tax>.op_Implicit(row).TaxRate.Value / 100M))));
      num1 = diff - taxable;
    }
    else
      taxable = diff;
    foreach (IGrouping<\u003C\u003Ef__AnonymousType6<string, string, string>, PXResult<ARTax, PX.Objects.TX.Tax>> grouping in taxLines.GroupBy(row => new
    {
      TranType = PXResult<ARTax, PX.Objects.TX.Tax>.op_Implicit(row).TranType,
      RefNbr = PXResult<ARTax, PX.Objects.TX.Tax>.op_Implicit(row).RefNbr,
      TaxID = PXResult<ARTax, PX.Objects.TX.Tax>.op_Implicit(row).TaxID
    }))
    {
      Decimal? nullable;
      foreach (PXResult<ARTax, PX.Objects.TX.Tax> pxResult in (IEnumerable<PXResult<ARTax, PX.Objects.TX.Tax>>) grouping)
      {
        PXResult<ARTax, PX.Objects.TX.Tax>.op_Implicit(pxResult);
        ARTax arTax1 = PXResult<ARTax, PX.Objects.TX.Tax>.op_Implicit(pxResult);
        ARTax copy = PXCache<ARTax>.CreateCopy(arTax1);
        IPXCurrencyHelper implementation = ((PXGraph) invoiceEntry).FindImplementation<IPXCurrencyHelper>();
        Decimal num2 = diff;
        nullable = arTax1.TaxRate;
        Decimal num3 = nullable.Value;
        Decimal val = num2 * num3 / 100M;
        num1 = implementation.RoundCury(val);
        if (maxTaxAmt != null && copy.TaxID == maxTaxAmt.TaxID)
        {
          ARTax arTax2 = copy;
          nullable = arTax2.CuryTaxableAmt;
          Decimal num4 = taxable;
          arTax2.CuryTaxableAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num4) : new Decimal?();
          ARTax arTax3 = copy;
          nullable = arTax3.CuryTaxAmt;
          Decimal num5 = num1;
          arTax3.CuryTaxAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num5) : new Decimal?();
        }
        else
        {
          ARTax arTax4 = copy;
          nullable = arTax4.CuryTaxableAmt;
          Decimal num6 = taxable;
          arTax4.CuryTaxableAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num6) : new Decimal?();
        }
        ((PXSelectBase<ARTax>) invoiceEntry.Tax_Rows).Update(copy);
      }
      ARTaxTran arTaxTran1 = PXResultset<ARTaxTran>.op_Implicit(PXSelectBase<ARTaxTran, PXViewOf<ARTaxTran>.BasedOn<SelectFromBase<ARTaxTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTaxTran.tranType, Equal<P.AsString.ASCII>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTaxTran.refNbr, Equal<P.AsString>>>>>.And<BqlOperand<ARTaxTran.taxID, IBqlString>.IsEqual<P.AsString>>>>>.Config>.Select((PXGraph) invoiceEntry, new object[3]
      {
        (object) grouping.Key.TranType,
        (object) grouping.Key.RefNbr,
        (object) grouping.Key.TaxID
      }));
      if (arTaxTran1 != null)
      {
        ARTaxTran copy = PXCache<ARTaxTran>.CreateCopy(arTaxTran1);
        if (maxTaxAmt != null && copy.TaxID == maxTaxAmt.TaxID)
        {
          ARTaxTran arTaxTran2 = copy;
          nullable = arTaxTran2.CuryTaxableAmt;
          Decimal num7 = taxable;
          arTaxTran2.CuryTaxableAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num7) : new Decimal?();
          ARTaxTran arTaxTran3 = copy;
          nullable = arTaxTran3.CuryTaxAmt;
          Decimal num8 = num1;
          arTaxTran3.CuryTaxAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num8) : new Decimal?();
        }
        else
        {
          ARTaxTran arTaxTran4 = copy;
          nullable = arTaxTran4.CuryTaxableAmt;
          Decimal num9 = taxable;
          arTaxTran4.CuryTaxableAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num9) : new Decimal?();
        }
        ((PXSelectBase<ARTaxTran>) invoiceEntry.Taxes).Update(copy);
      }
    }
    return taxable;
  }

  protected virtual void ProcessRoundingDiffToTax(
    ARInvoiceEntry invoiceEntry,
    Decimal diff,
    ARTax maxTaxAmt)
  {
    if (maxTaxAmt == null)
      return;
    ARTax copy1 = PXCache<ARTax>.CreateCopy(maxTaxAmt);
    ARTax arTax = copy1;
    Decimal? curyTaxAmt = arTax.CuryTaxAmt;
    Decimal num1 = diff;
    arTax.CuryTaxAmt = curyTaxAmt.HasValue ? new Decimal?(curyTaxAmt.GetValueOrDefault() + num1) : new Decimal?();
    ((PXSelectBase<ARTax>) invoiceEntry.Tax_Rows).Update(copy1);
    ARTaxTran arTaxTran1 = PXResultset<ARTaxTran>.op_Implicit(PXSelectBase<ARTaxTran, PXViewOf<ARTaxTran>.BasedOn<SelectFromBase<ARTaxTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTaxTran.tranType, Equal<P.AsString.ASCII>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTaxTran.refNbr, Equal<P.AsString>>>>>.And<BqlOperand<ARTaxTran.taxID, IBqlString>.IsEqual<P.AsString>>>>>.Config>.Select((PXGraph) invoiceEntry, new object[3]
    {
      (object) maxTaxAmt.TranType,
      (object) maxTaxAmt.RefNbr,
      (object) maxTaxAmt.TaxID
    }));
    if (arTaxTran1 == null || !(arTaxTran1.TaxID == maxTaxAmt.TaxID))
      return;
    ARTaxTran copy2 = PXCache<ARTaxTran>.CreateCopy(arTaxTran1);
    ARTaxTran arTaxTran2 = copy2;
    curyTaxAmt = arTaxTran2.CuryTaxAmt;
    Decimal num2 = diff;
    arTaxTran2.CuryTaxAmt = curyTaxAmt.HasValue ? new Decimal?(curyTaxAmt.GetValueOrDefault() + num2) : new Decimal?();
    ((PXSelectBase<ARTaxTran>) invoiceEntry.Taxes).Update(copy2);
  }

  protected virtual SOAdjust CreateSOApplication(ARInvoiceEntry invoiceEntry, PX.Objects.SO.SOOrder order)
  {
    ARInvoice current = ((PXSelectBase<ARInvoice>) invoiceEntry.Document).Current;
    SOAdjust soAdjust1 = new SOAdjust();
    soAdjust1.AdjdOrderType = order.OrderType;
    soAdjust1.AdjdOrderNbr = order.OrderNbr;
    soAdjust1.AdjgRefNbr = current.RefNbr;
    soAdjust1.AdjgDocType = current.DocType;
    soAdjust1.CustomerID = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current.CustomerID;
    soAdjust1.AdjgCuryInfoID = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current.CuryInfoID;
    soAdjust1.AdjdCuryInfoID = order.CuryInfoID;
    PX.Objects.SO.SOOrder copy = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(order);
    soAdjust1.AdjdOrigCuryInfoID = copy.CuryInfoID;
    soAdjust1.AdjgDocDate = current.DocDate;
    soAdjust1.AdjgCuryInfoID = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current.CuryInfoID;
    soAdjust1.AdjdCuryInfoID = order.CuryInfoID;
    soAdjust1.AdjdOrigCuryInfoID = copy.CuryInfoID;
    soAdjust1.AdjdOrderDate = copy.OrderDate;
    SOAdjust soAdjust2 = soAdjust1;
    DateTime? orderDate = copy.OrderDate;
    DateTime? docDate = current.DocDate;
    DateTime? nullable = (orderDate.HasValue & docDate.HasValue ? (orderDate.GetValueOrDefault() > docDate.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? current.DocDate : copy.OrderDate;
    soAdjust2.AdjdOrderDate = nullable;
    soAdjust1.Released = new bool?(false);
    soAdjust1.IsCCPayment = new bool?(false);
    soAdjust1.PaymentReleased = new bool?(false);
    soAdjust1.PendingPayment = new bool?(false);
    soAdjust1.IsCCAuthorized = new bool?(false);
    soAdjust1.IsCCCaptured = new bool?(false);
    soAdjust1.Voided = new bool?(false);
    soAdjust1.Hold = new bool?(true);
    soAdjust1.PaymentMethodID = copy.PaymentMethodID;
    soAdjust1.CashAccountID = copy.CashAccountID;
    soAdjust1.PMInstanceID = copy.PMInstanceID;
    return ((PXSelectBase<SOAdjust>) ((PXGraph) invoiceEntry).GetExtension<ARInvoiceEntryVATRecognitionOnPrepayments>().SOAdjustments).Insert(soAdjust1);
  }

  public delegate Decimal NewBalanceCalculationDelegate(SOAdjust adj, Decimal newValue);
}
