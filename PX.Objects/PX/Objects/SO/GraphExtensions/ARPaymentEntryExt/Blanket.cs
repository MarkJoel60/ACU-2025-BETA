// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.ARPaymentEntryExt.Blanket
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.ARPaymentEntryExt;

public class Blanket : 
  PXGraphExtension<OrdersToApplyTab, ARPaymentEntry.MultiCurrency, ARPaymentEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUnboundFormula(typeof (IIf<Where<SOAdjust.voided, Equal<False>>, SOAdjust.curyAdjdTransferredToChildrenAmt, decimal0>), typeof (SumCalc<PX.Objects.SO.SOOrder.curyTransferredToChildrenPaymentTotal>), ForceAggregateRecalculation = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOAdjust.curyAdjdTransferredToChildrenAmt> eventArgs)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<SOAdjust> e)
  {
    if (e.Row == null || !this.Base2.IsApplicationToBlanketOrderWithChild(e.Row))
      return;
    PXUIFieldAttribute.SetEnabled<SOAdjust.curyAdjgAmt>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOAdjust>>) e).Cache, (object) e.Row, false);
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOAdjust>>) e).Cache.RaiseExceptionHandling<SOAdjust.curyDocBal>((object) e.Row, (object) 0M, (Exception) new PXSetPropertyException("The balance of a blanket sales order is always shown as zero on this tab if the blanket sales order has one or more child orders.", (PXErrorLevel) 2));
  }

  protected virtual void _(PX.Data.Events.RowUpdated<SOAdjust> e)
  {
    Decimal? curyAdjgAmt1 = e.Row.CuryAdjgAmt;
    Decimal? curyAdjgAmt2 = e.OldRow.CuryAdjgAmt;
    if (curyAdjgAmt1.GetValueOrDefault() == curyAdjgAmt2.GetValueOrDefault() & curyAdjgAmt1.HasValue == curyAdjgAmt2.HasValue || e.Row.BlanketNbr == null)
      return;
    Decimal? curyAdjgBilledAmt1 = e.Row.CuryAdjgBilledAmt;
    Decimal? curyAdjgBilledAmt2 = e.OldRow.CuryAdjgBilledAmt;
    if (!(curyAdjgBilledAmt1.GetValueOrDefault() == curyAdjgBilledAmt2.GetValueOrDefault() & curyAdjgBilledAmt1.HasValue == curyAdjgBilledAmt2.HasValue))
      return;
    this.UpdateBlanketSOAdjust(e.Row, e.OldRow);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<SOAdjust> e)
  {
    if (!this.Base2.IsApplicationToBlanketOrderWithChild(e.Row))
      return;
    this.ClearLinksToBlanketSOAdjust(e.Row);
  }

  protected virtual void UpdateBlanketSOAdjust(SOAdjust adjustment, SOAdjust oldAdjustment)
  {
    (Decimal CuryAdjg, Decimal Adj, Decimal CuryAdjd) adjustDifference = this.GetBlanketSOAdjustDifference(adjustment, oldAdjustment);
    if (adjustDifference.CuryAdjg == 0M)
      return;
    SOAdjust blanketSoAdjust = this.GetBlanketSOAdjust(adjustment);
    this.CalculateBlanketTransferredAmount(adjustDifference, blanketSoAdjust);
    ((PXSelectBase<SOAdjust>) this.Base2.SOAdjustments).Update(blanketSoAdjust);
    ((PXSelectBase) this.Base2.SOAdjustments).View.RequestRefresh();
  }

  protected virtual (Decimal CuryAdjg, Decimal Adj, Decimal CuryAdjd) GetBlanketSOAdjustDifference(
    SOAdjust adjustment,
    SOAdjust oldAdjustment)
  {
    Decimal? nullable = adjustment.CuryAdjgAmt;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = oldAdjustment.CuryAdjgAmt;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    Decimal num1 = valueOrDefault1 - valueOrDefault2;
    nullable = adjustment.AdjAmt;
    Decimal valueOrDefault3 = nullable.GetValueOrDefault();
    nullable = oldAdjustment.AdjAmt;
    Decimal valueOrDefault4 = nullable.GetValueOrDefault();
    Decimal num2 = valueOrDefault3 - valueOrDefault4;
    nullable = adjustment.CuryAdjdAmt;
    Decimal valueOrDefault5 = nullable.GetValueOrDefault();
    nullable = oldAdjustment.CuryAdjdAmt;
    Decimal valueOrDefault6 = nullable.GetValueOrDefault();
    Decimal num3 = valueOrDefault5 - valueOrDefault6;
    return (num1, num2, num3);
  }

  protected virtual SOAdjust GetBlanketSOAdjust(SOAdjust adjustment)
  {
    return PXResultset<SOAdjust>.op_Implicit(PXSelectBase<SOAdjust, PXViewOf<SOAdjust>.BasedOn<SelectFromBase<SOAdjust, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOAdjust.recordID, Equal<BqlField<SOAdjust.blanketRecordID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<SOAdjust.adjdOrderType, IBqlString>.IsEqual<BqlField<SOAdjust.blanketType, IBqlString>.FromCurrent>>>, And<BqlOperand<SOAdjust.adjdOrderNbr, IBqlString>.IsEqual<BqlField<SOAdjust.blanketNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<SOAdjust.adjgDocType, IBqlString>.IsEqual<BqlField<SOAdjust.adjgDocType, IBqlString>.FromCurrent>>>>.And<BqlOperand<SOAdjust.adjgRefNbr, IBqlString>.IsEqual<BqlField<SOAdjust.adjgRefNbr, IBqlString>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base, new object[1]
    {
      (object) adjustment
    }, Array.Empty<object>())) ?? throw new RowNotFoundException(((PXSelectBase) this.Base2.SOAdjustments).Cache, new object[5]
    {
      (object) adjustment.RecordID,
      (object) adjustment.BlanketType,
      (object) adjustment.BlanketNbr,
      (object) adjustment.AdjgDocType,
      (object) adjustment.AdjgRefNbr
    });
  }

  protected virtual void CalculateBlanketTransferredAmount(
    (Decimal CuryAdjg, Decimal Adj, Decimal CuryAdjd) difference,
    SOAdjust blanketAdjustment)
  {
    SOAdjust soAdjust1 = blanketAdjustment;
    Decimal? nullable1 = blanketAdjustment.CuryAdjgAmt;
    Decimal curyAdjg1 = difference.CuryAdjg;
    Decimal? nullable2 = new Decimal?(Math.Max((nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - curyAdjg1) : new Decimal?()).GetValueOrDefault(), 0M));
    soAdjust1.CuryAdjgAmt = nullable2;
    SOAdjust soAdjust2 = blanketAdjustment;
    nullable1 = blanketAdjustment.CuryAdjgTransferredToChildrenAmt;
    Decimal curyAdjg2 = difference.CuryAdjg;
    Decimal? nullable3 = new Decimal?(Math.Max((nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + curyAdjg2) : new Decimal?()).GetValueOrDefault(), 0M));
    soAdjust2.CuryAdjgTransferredToChildrenAmt = nullable3;
    SOAdjust soAdjust3 = blanketAdjustment;
    nullable1 = blanketAdjustment.AdjTransferredToChildrenAmt;
    Decimal adj = difference.Adj;
    Decimal? nullable4 = new Decimal?(Math.Max((nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + adj) : new Decimal?()).GetValueOrDefault(), 0M));
    soAdjust3.AdjTransferredToChildrenAmt = nullable4;
    SOAdjust soAdjust4 = blanketAdjustment;
    nullable1 = blanketAdjustment.CuryAdjdTransferredToChildrenAmt;
    Decimal curyAdjd = difference.CuryAdjd;
    Decimal? nullable5 = new Decimal?(Math.Max((nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + curyAdjd) : new Decimal?()).GetValueOrDefault(), 0M));
    soAdjust4.CuryAdjdTransferredToChildrenAmt = nullable5;
  }

  protected virtual void ClearLinksToBlanketSOAdjust(SOAdjust blanketSOAdjust)
  {
    foreach (PXResult<SOAdjust> pxResult in PXSelectBase<SOAdjust, PXViewOf<SOAdjust>.BasedOn<SelectFromBase<SOAdjust, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOAdjust.blanketRecordID, Equal<BqlField<SOAdjust.recordID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<SOAdjust.blanketType, IBqlString>.IsEqual<BqlField<SOAdjust.adjdOrderType, IBqlString>.FromCurrent>>>, And<BqlOperand<SOAdjust.blanketNbr, IBqlString>.IsEqual<BqlField<SOAdjust.adjdOrderNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<SOAdjust.adjgDocType, IBqlString>.IsEqual<BqlField<SOAdjust.adjgDocType, IBqlString>.FromCurrent>>>>.And<BqlOperand<SOAdjust.adjgRefNbr, IBqlString>.IsEqual<BqlField<SOAdjust.adjgRefNbr, IBqlString>.FromCurrent>>>>.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<ARPaymentEntry>) this).Base, new object[1]
    {
      (object) blanketSOAdjust
    }, Array.Empty<object>()))
    {
      SOAdjust soAdjust = PXResult<SOAdjust>.op_Implicit(pxResult);
      soAdjust.BlanketRecordID = new int?();
      soAdjust.BlanketType = (string) null;
      soAdjust.BlanketNbr = (string) null;
      ((PXSelectBase<SOAdjust>) this.Base2.SOAdjustments).Update(soAdjust);
    }
  }
}
