// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt.UpdateSOOrder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt;

public class UpdateSOOrder : PXGraphExtension<SOInvoiceEntry>
{
  public PXSelect<SOLine2> soline;
  public PXSelect<SOMiscLine2> somiscline;
  public PXSelect<SOTax> sotax;
  public PXSelect<SOTaxTran> sotaxtran;
  public PXSelect<PX.Objects.SO.SOOrder> soorder;

  [PXMergeAttributes]
  [PXParent(typeof (Select<SOLine2, Where<SOLine2.orderType, Equal<Current<ARTran.sOOrderType>>, And<SOLine2.orderNbr, Equal<Current<ARTran.sOOrderNbr>>, And<SOLine2.lineNbr, Equal<Current<ARTran.sOOrderLineNbr>>>>>>), LeaveChildren = true)]
  [PXParent(typeof (Select<SOMiscLine2, Where<SOMiscLine2.orderType, Equal<Current<ARTran.sOOrderType>>, And<SOMiscLine2.orderNbr, Equal<Current<ARTran.sOOrderNbr>>, And<SOMiscLine2.lineNbr, Equal<Current<ARTran.sOOrderLineNbr>>>>>>), LeaveChildren = true)]
  protected virtual void ARTran_SOOrderLineNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUnboundFormula(typeof (BaseBilledQtyFormula), typeof (AddCalc<SOLine2.baseBilledQty>), EnableAggregation = false)]
  [PXUnboundFormula(typeof (BaseBilledQtyFormula), typeof (SubCalc<SOLine2.baseUnbilledQty>), EnableAggregation = false)]
  [PXUnboundFormula(typeof (BaseBilledQtyFormula), typeof (AddCalc<SOMiscLine2.baseBilledQty>), EnableAggregation = false)]
  [PXUnboundFormula(typeof (BaseBilledQtyFormula), typeof (SubCalc<SOMiscLine2.baseUnbilledQty>), EnableAggregation = false)]
  protected virtual void ARTran_BaseQty_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUnboundFormula(typeof (CuryBilledAmtFormula), typeof (AddCalc<SOMiscLine2.curyBilledAmt>), EnableAggregation = false)]
  [PXUnboundFormula(typeof (CuryBilledAmtFormula), typeof (SubCalc<SOMiscLine2.curyUnbilledAmt>), EnableAggregation = false)]
  protected virtual void ARTran_CuryTranAmt_CacheAttached(PXCache sender)
  {
  }

  [PXOverride]
  public virtual void ARTran_RowDeleted(
    PXCache sender,
    PXRowDeletedEventArgs e,
    PXRowDeleted baseMethod)
  {
    baseMethod.Invoke(sender, e);
    PXFormulaAttribute.CalcAggregate<ARTran.baseQty>(sender, (object) null, e.Row, typeof (SOLine2.baseBilledQty));
    PXFormulaAttribute.CalcAggregate<ARTran.baseQty>(sender, (object) null, e.Row, typeof (SOLine2.baseUnbilledQty));
    PXFormulaAttribute.CalcAggregate<ARTran.baseQty>(sender, (object) null, e.Row, typeof (SOMiscLine2.baseBilledQty));
    PXFormulaAttribute.CalcAggregate<ARTran.baseQty>(sender, (object) null, e.Row, typeof (SOMiscLine2.baseUnbilledQty));
    PXFormulaAttribute.CalcAggregate<ARTran.curyTranAmt>(sender, (object) null, e.Row, typeof (SOMiscLine2.curyBilledAmt));
    PXFormulaAttribute.CalcAggregate<ARTran.curyTranAmt>(sender, (object) null, e.Row, typeof (SOMiscLine2.curyUnbilledAmt));
    SOLine2 soLine2_1 = PXParentAttribute.SelectParent<SOLine2>(sender, e.Row);
    if (soLine2_1 == null)
      return;
    Decimal? curyExtPrice1 = soLine2_1.CuryExtPrice;
    Decimal num1 = 0M;
    if (curyExtPrice1.GetValueOrDefault() == num1 & curyExtPrice1.HasValue)
      return;
    Decimal? orderQty = soLine2_1.OrderQty;
    Decimal num2 = 0M;
    if (!(orderQty.GetValueOrDefault() == num2 & orderQty.HasValue) || !EnumerableExtensions.IsIn<string>(soLine2_1.Behavior, "IN", "MO", "CM"))
      return;
    SOLine2 soLine2_2 = soLine2_1;
    Decimal? curyExtPrice2 = soLine2_1.CuryExtPrice;
    Decimal? curyDiscAmt = soLine2_1.CuryDiscAmt;
    Decimal? nullable = curyExtPrice2.HasValue & curyDiscAmt.HasValue ? new Decimal?(curyExtPrice2.GetValueOrDefault() - curyDiscAmt.GetValueOrDefault()) : new Decimal?();
    soLine2_2.CuryUnbilledAmt = nullable;
    ((PXSelectBase<SOLine2>) this.soline).Update(soLine2_1);
  }

  [PXOverride]
  public virtual void ARTran_RowInserted(
    PXCache sender,
    PXRowInsertedEventArgs e,
    PXRowInserted baseMethod)
  {
    baseMethod.Invoke(sender, e);
    PXFormulaAttribute.CalcAggregate<ARTran.baseQty>(sender, e.Row, (object) null, typeof (SOLine2.baseBilledQty));
    PXFormulaAttribute.CalcAggregate<ARTran.baseQty>(sender, e.Row, (object) null, typeof (SOLine2.baseUnbilledQty));
    PXFormulaAttribute.CalcAggregate<ARTran.baseQty>(sender, e.Row, (object) null, typeof (SOMiscLine2.baseBilledQty));
    PXFormulaAttribute.CalcAggregate<ARTran.baseQty>(sender, e.Row, (object) null, typeof (SOMiscLine2.baseUnbilledQty));
    PXFormulaAttribute.CalcAggregate<ARTran.curyTranAmt>(sender, e.Row, (object) null, typeof (SOMiscLine2.curyBilledAmt));
    PXFormulaAttribute.CalcAggregate<ARTran.curyTranAmt>(sender, e.Row, (object) null, typeof (SOMiscLine2.curyUnbilledAmt));
    SOLine2 soLine2 = PXParentAttribute.SelectParent<SOLine2>(sender, e.Row);
    if (soLine2 == null)
      return;
    Decimal? orderQty = soLine2.OrderQty;
    Decimal num1 = 0M;
    if (!(orderQty.GetValueOrDefault() == num1 & orderQty.HasValue))
      return;
    Decimal? curyExtPrice = soLine2.CuryExtPrice;
    Decimal num2 = 0M;
    if (curyExtPrice.GetValueOrDefault() == num2 & curyExtPrice.HasValue || !EnumerableExtensions.IsIn<string>(soLine2.Behavior, "IN", "MO", "CM"))
      return;
    soLine2.CuryUnbilledAmt = (Decimal?) PXFormulaAttribute.Evaluate<SOLine.curyUnbilledAmt>(((PXSelectBase) this.soline).Cache, (object) soLine2);
    ((PXSelectBase<SOLine2>) this.soline).Update(soLine2);
  }

  [PXOverride]
  public virtual void ARTran_RowUpdated(
    PXCache sender,
    PXRowUpdatedEventArgs e,
    PXRowUpdated baseMethod)
  {
    baseMethod.Invoke(sender, e);
    PXFormulaAttribute.CalcAggregate<ARTran.baseQty>(sender, e.Row, e.OldRow, typeof (SOLine2.baseBilledQty));
    PXFormulaAttribute.CalcAggregate<ARTran.baseQty>(sender, e.Row, e.OldRow, typeof (SOLine2.baseUnbilledQty));
    PXFormulaAttribute.CalcAggregate<ARTran.baseQty>(sender, e.Row, e.OldRow, typeof (SOMiscLine2.baseBilledQty));
    PXFormulaAttribute.CalcAggregate<ARTran.baseQty>(sender, e.Row, e.OldRow, typeof (SOMiscLine2.baseUnbilledQty));
    PXFormulaAttribute.CalcAggregate<ARTran.curyTranAmt>(sender, e.Row, e.OldRow, typeof (SOMiscLine2.curyBilledAmt));
    PXFormulaAttribute.CalcAggregate<ARTran.curyTranAmt>(sender, e.Row, e.OldRow, typeof (SOMiscLine2.curyUnbilledAmt));
  }

  protected virtual void SOLine2_BaseShippedQty_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 3) != 1)
      return;
    e.ExcludeFromInsertUpdate();
  }

  protected virtual void SOLine2_ShippedQty_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 3) != 1)
      return;
    e.ExcludeFromInsertUpdate();
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.SO.SOOrderShipment> e)
  {
    if (!e.Row.OrderTaxAllocated.GetValueOrDefault())
      return;
    PX.Objects.SO.SOOrder soOrder = PXParentAttribute.SelectParent<PX.Objects.SO.SOOrder>(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<PX.Objects.SO.SOOrderShipment>>) e).Cache, (object) e.Row);
    if (soOrder == null)
      return;
    soOrder.OrderTaxAllocated = new bool?(true);
    GraphHelper.Caches<PX.Objects.SO.SOOrder>(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<PX.Objects.SO.SOOrderShipment>>) e).Cache.Graph).Update(soOrder);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrderShipment> e)
  {
    if (!e.Row.OrderTaxAllocated.GetValueOrDefault() && !e.OldRow.OrderTaxAllocated.GetValueOrDefault())
      return;
    bool? orderTaxAllocated1 = e.Row.OrderTaxAllocated;
    bool? orderTaxAllocated2 = e.OldRow.OrderTaxAllocated;
    if (orderTaxAllocated1.GetValueOrDefault() == orderTaxAllocated2.GetValueOrDefault() & orderTaxAllocated1.HasValue == orderTaxAllocated2.HasValue)
      return;
    PX.Objects.SO.SOOrder soOrder1 = PXParentAttribute.SelectParent<PX.Objects.SO.SOOrder>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrderShipment>>) e).Cache, (object) e.Row);
    if (soOrder1 == null)
      return;
    PX.Objects.SO.SOOrder soOrder2 = soOrder1;
    orderTaxAllocated2 = e.Row.OrderTaxAllocated;
    bool? nullable = new bool?(orderTaxAllocated2.GetValueOrDefault());
    soOrder2.OrderTaxAllocated = nullable;
    GraphHelper.Caches<PX.Objects.SO.SOOrder>(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrderShipment>>) e).Cache.Graph).Update(soOrder1);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.SO.SOOrderShipment> e)
  {
    if (!e.Row.OrderTaxAllocated.GetValueOrDefault())
      return;
    PX.Objects.SO.SOOrder soOrder = PXParentAttribute.SelectParent<PX.Objects.SO.SOOrder>(((PX.Data.Events.Event<PXRowDeletedEventArgs, PX.Data.Events.RowDeleted<PX.Objects.SO.SOOrderShipment>>) e).Cache, (object) e.Row);
    if (soOrder == null)
      return;
    soOrder.OrderTaxAllocated = new bool?(false);
    GraphHelper.Caches<PX.Objects.SO.SOOrder>(((PX.Data.Events.Event<PXRowDeletedEventArgs, PX.Data.Events.RowDeleted<PX.Objects.SO.SOOrderShipment>>) e).Cache.Graph).Update(soOrder);
  }
}
