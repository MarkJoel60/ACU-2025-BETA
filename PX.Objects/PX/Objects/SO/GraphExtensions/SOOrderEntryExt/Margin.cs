// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.Margin
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.TX;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class Margin : PXGraphExtension<SOOrderEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  [PXMergeAttributes]
  [PXUnboundFormula(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.behavior, Equal<SOBehavior.tR>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.pOCreate, Equal<True>>>>>.And<BqlOperand<PX.Objects.SO.SOLine.pOSource, IBqlString>.IsIn<INReplenishmentSource.dropShipToOrder, INReplenishmentSource.blanketDropShipToOrder>>>>, decimal0>, BqlFunction<Mult<PX.Objects.SO.SOLine.curyExtCost, BqlOperand<int_1, IBqlInt>.When<BqlOperand<PX.Objects.SO.SOLine.invtMult, IBqlShort>.IsEqual<short0>>.Else<PX.Objects.SO.SOLine.invtMult>>, IBqlDecimal>.Multiply<int_1>>), typeof (SumCalc<PX.Objects.SO.SOOrder.curySalesCostTotal>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOLine.curyExtCost> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Switch<Case<Where<BqlOperand<PX.Objects.SO.SOLine.completed, IBqlBool>.IsEqual<True>>, PX.Objects.SO.SOLine.curyNetSales, Case<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.curyExtCost, Equal<decimal0>>>>, Or<BqlOperand<PX.Objects.SO.SOLine.behavior, IBqlString>.IsEqual<SOBehavior.tR>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.pOCreate, Equal<True>>>>>.And<BqlOperand<PX.Objects.SO.SOLine.pOSource, IBqlString>.IsIn<INReplenishmentSource.dropShipToOrder, INReplenishmentSource.blanketDropShipToOrder>>>>, decimal0>>, BqlFunctionMirror<Mult<ArrayedSwitch<TypeArrayOf<IBqlCase>.Append<TypeArrayOf<IBqlCase>.Empty, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.curyTaxableAmt, Equal<decimal0>>>>>.Or<BqlOperand<Parent<PX.Objects.SO.SOOrder.taxCalcMode>, IBqlString>.IsEqual<TaxCalculationMode.net>>>, Mult<Mult<PX.Objects.SO.SOLine.curyLineAmt, PX.Objects.SO.SOLine.groupDiscountRate>, PX.Objects.SO.SOLine.documentDiscountRate>>>, PX.Objects.SO.SOLine.curyTaxableAmt>, BqlOperand<int_1, IBqlInt>.When<BqlOperand<PX.Objects.SO.SOLine.invtMult, IBqlShort>.IsEqual<short0>>.Else<PX.Objects.SO.SOLine.invtMult>>, IBqlDecimal>.Multiply<int_1>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOLine.curyNetSales> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Switch<Case<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.curyExtCost, Equal<decimal0>>>>, Or<BqlOperand<PX.Objects.SO.SOLine.behavior, IBqlString>.IsEqual<SOBehavior.tR>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.pOCreate, Equal<True>>>>>.And<BqlOperand<PX.Objects.SO.SOLine.pOSource, IBqlString>.IsIn<INReplenishmentSource.dropShipToOrder, INReplenishmentSource.blanketDropShipToOrder>>>>, Null, Case<Where<BqlOperand<PX.Objects.SO.SOLine.curyExtPrice, IBqlDecimal>.IsEqual<decimal0>>, decimal0>>, BqlOperand<PX.Objects.SO.SOLine.curyNetSales, IBqlDecimal>.Subtract<BqlFunction<Mult<PX.Objects.SO.SOLine.curyExtCost, BqlOperand<int_1, IBqlInt>.When<BqlOperand<PX.Objects.SO.SOLine.invtMult, IBqlShort>.IsEqual<short0>>.Else<PX.Objects.SO.SOLine.invtMult>>, IBqlDecimal>.Multiply<int_1>>>))]
  [PXUnboundFormula(typeof (BqlOperand<int1, IBqlInt>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.behavior, NotEqual<SOBehavior.tR>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.curyUnitCost, Equal<decimal0>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.pOCreate, Equal<True>>>>>.And<BqlOperand<PX.Objects.SO.SOLine.pOSource, IBqlString>.IsIn<INReplenishmentSource.dropShipToOrder, INReplenishmentSource.blanketDropShipToOrder>>>>>.Else<int0>), typeof (SumCalc<PX.Objects.SO.SOOrder.noMarginLineCntr>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOLine.curyMarginAmt> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Switch<Case<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.curyExtCost, Equal<decimal0>>>>, Or<BqlOperand<PX.Objects.SO.SOLine.behavior, IBqlString>.IsEqual<SOBehavior.tR>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.pOCreate, Equal<True>>>>>.And<BqlOperand<PX.Objects.SO.SOLine.pOSource, IBqlString>.IsIn<INReplenishmentSource.dropShipToOrder, INReplenishmentSource.blanketDropShipToOrder>>>>, Null, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.curyExtPrice, Equal<decimal0>>>>>.Or<BqlOperand<PX.Objects.SO.SOLine.curyNetSales, IBqlDecimal>.IsEqual<decimal0>>>, decimal0>>, BqlFunction<Div<PX.Objects.SO.SOLine.curyMarginAmt, Abs<PX.Objects.SO.SOLine.curyNetSales>>, IBqlDecimal>.Multiply<decimal100>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOLine.marginPct> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (BqlOperand<PX.Objects.SO.SOOrder.curyNetSalesTotal, IBqlDecimal>.Add<BqlFunction<ArrayedSwitch<TypeArrayOf<IBqlCase>.Append<TypeArrayOf<IBqlCase>.Empty, Case<Where<BqlOperand<PX.Objects.SO.SOOrder.behavior, IBqlString>.IsEqual<SOBehavior.tR>>, decimal0>>, BqlOperand<PX.Objects.SO.SOOrder.curyFreightTot, IBqlDecimal>.When<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.curyTaxableFreightAmt, Equal<decimal0>>>>, Or<BqlOperand<PX.Objects.SO.SOOrder.curyTaxTotal, IBqlDecimal>.IsEqual<decimal0>>>>.Or<BqlOperand<PX.Objects.SO.SOOrder.taxCalcMode, IBqlString>.IsEqual<TaxCalculationMode.net>>>.Else<PX.Objects.SO.SOOrder.curyTaxableFreightAmt>>, IBqlDecimal>.Multiply<BqlOperand<decimal_1, IBqlDecimal>.When<BqlOperand<PX.Objects.SO.SOOrder.defaultOperation, IBqlString>.IsEqual<SOOperation.receipt>>.Else<decimal1>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOOrder.curyOrderNetSales> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (BqlOperand<PX.Objects.SO.SOOrder.curySalesCostTotal, IBqlDecimal>.Add<BqlFunction<ArrayedSwitch<TypeArrayOf<IBqlCase>.Append<TypeArrayOf<IBqlCase>.Empty, Case<Where<BqlOperand<PX.Objects.SO.SOOrder.behavior, IBqlString>.IsEqual<SOBehavior.tR>>, decimal0>>, PX.Objects.SO.SOOrder.curyFreightCost>, IBqlDecimal>.Multiply<BqlOperand<decimal_1, IBqlDecimal>.When<BqlOperand<PX.Objects.SO.SOOrder.defaultOperation, IBqlString>.IsEqual<SOOperation.receipt>>.Else<decimal1>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOOrder.curyOrderCosts> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (BqlOperand<PX.Objects.SO.SOOrder.curyOrderNetSales, IBqlDecimal>.Subtract<PX.Objects.SO.SOOrder.curyOrderCosts>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOOrder.curyMarginAmt> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Switch<Case<Where<BqlOperand<PX.Objects.SO.SOOrder.curyOrderNetSales, IBqlDecimal>.IsEqual<decimal0>>, decimal0>, BqlFunction<Div<PX.Objects.SO.SOOrder.curyMarginAmt, Abs<PX.Objects.SO.SOOrder.curyOrderNetSales>>, IBqlDecimal>.Multiply<decimal100>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.SO.SOOrder.marginPct> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder> e)
  {
    bool visible = e.Row?.Behavior != "TR";
    PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) e).Cache, (object) e.Row).For<PX.Objects.SO.SOOrder.marginPct>((Action<PXUIFieldAttribute>) (a => a.Visible = visible)).SameFor<PX.Objects.SO.SOOrder.curyMarginAmt>();
    PXCacheEx.Adjust<PXUIFieldAttribute>(((PXSelectBase) this.Base.Transactions).Cache, (object) null).For<PX.Objects.SO.SOLine.marginPct>((Action<PXUIFieldAttribute>) (a => a.Visible = visible)).SameFor<PX.Objects.SO.SOLine.curyMarginAmt>();
    this.RaiseMarginWarning(e.Row);
  }

  protected virtual void RaiseMarginWarning(PX.Objects.SO.SOOrder order)
  {
    PXSetPropertyException propertyException;
    if (order != null && !(order.Behavior == "TR"))
    {
      int? noMarginLineCntr = order.NoMarginLineCntr;
      int num = 0;
      if (!(noMarginLineCntr.GetValueOrDefault() == num & noMarginLineCntr.HasValue))
      {
        bool? nullable = order.Cancelled;
        if (!nullable.GetValueOrDefault())
        {
          nullable = order.Completed;
          if (!nullable.GetValueOrDefault() && !(order.Status == "V"))
          {
            propertyException = (PXSetPropertyException) new PXSetPropertyException<PX.Objects.SO.SOOrder.curyMarginAmt>("Lines with zero unit cost and lines that are marked for drop-ship are not included in the calculation of margin for sales orders.", (PXErrorLevel) 2);
            goto label_6;
          }
        }
      }
    }
    propertyException = (PXSetPropertyException) null;
label_6:
    ((PXSelectBase) this.Base.Document).Cache.RaiseExceptionHandling<PX.Objects.SO.SOOrder.curyMarginAmt>((object) order, (object) (Decimal?) order?.CuryMarginAmt, (Exception) propertyException);
  }

  /// Overrides <see cref="M:PX.Objects.SO.SOOrderEntry.OrderCreated(PX.Objects.SO.SOOrder,PX.Objects.SO.SOOrder)" />
  [PXOverride]
  public void OrderCreated(PX.Objects.SO.SOOrder document, PX.Objects.SO.SOOrder source, Action<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder> baseImpl)
  {
    baseImpl(document, source);
    this.EvaluateFormula<PX.Objects.SO.SOOrder.curyOrderNetSales>(((PXSelectBase) this.Base.Document).Cache, (object) document, (object) null);
    this.EvaluateFormula<PX.Objects.SO.SOOrder.curyOrderCosts>(((PXSelectBase) this.Base.Document).Cache, (object) document, (object) null);
  }

  protected virtual void EvaluateFormula<TField>(PXCache cache, object row, object oldRow) where TField : IBqlField
  {
    PXFormulaAttribute formulaAttribute = cache.GetAttributesReadonly<TField>(row).OfType<PXFormulaAttribute>().FirstOrDefault<PXFormulaAttribute>();
    if (formulaAttribute == null)
      throw new PXArgumentException("formulaAttribute");
    PXFieldDefaultingEventArgs defaultingEventArgs = new PXFieldDefaultingEventArgs(row);
    formulaAttribute.FormulaDefaulting(cache, defaultingEventArgs);
    cache.SetValueExt<TField>(row, defaultingEventArgs.NewValue);
    if (!(formulaAttribute.Aggregate != (Type) null))
      return;
    formulaAttribute.RowUpdated(cache, new PXRowUpdatedEventArgs(row, oldRow, false));
  }

  public virtual void RequestRefreshLines()
  {
    ((PXSelectBase) this.Base.Transactions).View.RequestRefresh();
  }
}
