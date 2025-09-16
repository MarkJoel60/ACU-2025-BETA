// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.POReceiptEntryExt.AffectedBlanketOrderByPOReceiptRelease
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.PO;
using PX.Objects.SO.DAC.Projections;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.POReceiptEntryExt;

public class AffectedBlanketOrderByPOReceiptRelease : 
  AffectedBlanketOrderByChildOrders<AffectedBlanketOrderByPOReceiptRelease, POReceiptEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  protected virtual void _(PX.Data.Events.RowUpdated<SOLine4> e)
  {
    if (string.IsNullOrEmpty(e.Row.BlanketNbr) || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOLine4>>) e).Cache.ObjectsEqual<SOLine4.shippedQty, SOLine4.completed>((object) e.Row, (object) e.OldRow))
      return;
    Decimal? shippedQty1 = e.Row.ShippedQty;
    Decimal? shippedQty2 = e.OldRow.ShippedQty;
    Decimal? nullable1 = shippedQty1.HasValue & shippedQty2.HasValue ? new Decimal?(shippedQty1.GetValueOrDefault() - shippedQty2.GetValueOrDefault()) : new Decimal?();
    Decimal? orderQty1 = e.Row.OrderQty;
    Decimal? openQty1 = e.Row.OpenQty;
    Decimal? nullable2 = orderQty1.HasValue & openQty1.HasValue ? new Decimal?(orderQty1.GetValueOrDefault() - openQty1.GetValueOrDefault()) : new Decimal?();
    Decimal? orderQty2 = e.OldRow.OrderQty;
    Decimal? openQty2 = e.OldRow.OpenQty;
    Decimal? nullable3 = orderQty2.HasValue & openQty2.HasValue ? new Decimal?(orderQty2.GetValueOrDefault() - openQty2.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable4 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
    BlanketSOLineSplit blanketSoLineSplit1 = this.SelectParentSplit(e.Row);
    BlanketSOLineSplit blanketSoLineSplit2 = blanketSoLineSplit1;
    Decimal? nullable5 = blanketSoLineSplit2.ShippedQty;
    Decimal? nullable6 = nullable1;
    blanketSoLineSplit2.ShippedQty = nullable5.HasValue & nullable6.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
    BlanketSOLineSplit blanketSoLineSplit3 = blanketSoLineSplit1;
    Decimal? closedQty1 = blanketSoLineSplit3.ClosedQty;
    nullable5 = nullable4;
    blanketSoLineSplit3.ClosedQty = closedQty1.HasValue & nullable5.HasValue ? new Decimal?(closedQty1.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
    bool? completed = blanketSoLineSplit1.Completed;
    if (!completed.GetValueOrDefault())
    {
      BlanketSOLineSplit blanketSoLineSplit4 = blanketSoLineSplit1;
      Decimal? closedQty2 = blanketSoLineSplit1.ClosedQty;
      Decimal? receivedQty = blanketSoLineSplit1.ReceivedQty;
      nullable5 = closedQty2.HasValue & receivedQty.HasValue ? new Decimal?(closedQty2.GetValueOrDefault() + receivedQty.GetValueOrDefault()) : new Decimal?();
      Decimal? qty = blanketSoLineSplit1.Qty;
      bool? nullable7 = new bool?(nullable5.GetValueOrDefault() >= qty.GetValueOrDefault() & nullable5.HasValue & qty.HasValue);
      blanketSoLineSplit4.Completed = nullable7;
    }
    BlanketSOLineSplit blanketSoLineSplit5 = GraphHelper.Caches<BlanketSOLineSplit>((PXGraph) this.Base).Update(blanketSoLineSplit1);
    BlanketSOLine blanketSoLine1 = PXParentAttribute.SelectParent<BlanketSOLine>((PXCache) GraphHelper.Caches<BlanketSOLineSplit>((PXGraph) this.Base), (object) blanketSoLineSplit5);
    BlanketSOLine blanketSoLine2 = blanketSoLine1 != null ? blanketSoLine1 : throw new RowNotFoundException((PXCache) GraphHelper.Caches<BlanketSOLine>((PXGraph) this.Base), new object[3]
    {
      (object) blanketSoLineSplit5.OrderType,
      (object) blanketSoLineSplit5.OrderNbr,
      (object) blanketSoLineSplit5.LineNbr
    });
    Decimal? nullable8 = blanketSoLine2.ShippedQty;
    nullable5 = nullable1;
    blanketSoLine2.ShippedQty = nullable8.HasValue & nullable5.HasValue ? new Decimal?(nullable8.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
    BlanketSOLine blanketSoLine3 = blanketSoLine1;
    nullable5 = blanketSoLine3.ClosedQty;
    nullable8 = nullable4;
    blanketSoLine3.ClosedQty = nullable5.HasValue & nullable8.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable8.GetValueOrDefault()) : new Decimal?();
    nullable8 = blanketSoLine1.ClosedQty;
    nullable5 = blanketSoLine1.OrderQty;
    if (nullable8.GetValueOrDefault() > nullable5.GetValueOrDefault() & nullable8.HasValue & nullable5.HasValue)
      blanketSoLine1.ClosedQty = blanketSoLine1.OrderQty;
    completed = blanketSoLine1.Completed;
    if (!completed.GetValueOrDefault())
    {
      BlanketSOLine blanketSoLine4 = blanketSoLine1;
      nullable5 = blanketSoLine1.ClosedQty;
      nullable8 = blanketSoLine1.OrderQty;
      bool? nullable9 = new bool?(nullable5.GetValueOrDefault() >= nullable8.GetValueOrDefault() & nullable5.HasValue & nullable8.HasValue);
      blanketSoLine4.Completed = nullable9;
    }
    this.UpdateBlanketOrderShipmentCntr(GraphHelper.Caches<BlanketSOLine>((PXGraph) this.Base).Update(blanketSoLine1));
  }

  private BlanketSOLineSplit SelectParentSplit(SOLine4 row)
  {
    return PXResultset<BlanketSOLineSplit>.op_Implicit(PXSelectBase<BlanketSOLineSplit, PXViewOf<BlanketSOLineSplit>.BasedOn<SelectFromBase<BlanketSOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BlanketSOLineSplit.orderType, Equal<BqlField<SOLine4.blanketType, IBqlString>.FromCurrent>>>>, And<BqlOperand<BlanketSOLineSplit.orderNbr, IBqlString>.IsEqual<BqlField<SOLine4.blanketNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<BlanketSOLineSplit.lineNbr, IBqlInt>.IsEqual<BqlField<SOLine4.blanketLineNbr, IBqlInt>.FromCurrent>>>>.And<BqlOperand<BlanketSOLineSplit.splitLineNbr, IBqlInt>.IsEqual<BqlField<SOLine4.blanketSplitLineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new SOLine4[1]
    {
      row
    }, Array.Empty<object>())) ?? throw new RowNotFoundException((PXCache) GraphHelper.Caches<BlanketSOLineSplit>((PXGraph) this.Base), new object[4]
    {
      (object) row.BlanketType,
      (object) row.BlanketNbr,
      (object) row.BlanketLineNbr,
      (object) row.BlanketSplitLineNbr
    });
  }

  private void UpdateBlanketOrderShipmentCntr(BlanketSOLine blanketline)
  {
    BlanketSOOrder blanketSoOrder1 = PXParentAttribute.SelectParent<BlanketSOOrder>((PXCache) GraphHelper.Caches<BlanketSOLine>((PXGraph) this.Base), (object) blanketline);
    if (blanketSoOrder1 == null)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<BlanketSOOrder>((PXGraph) this.Base), new object[2]
      {
        (object) blanketline.OrderType,
        (object) blanketline.OrderNbr
      });
    if (blanketSoOrder1.ShipmentCntrUpdated.GetValueOrDefault())
      return;
    BlanketSOOrder blanketSoOrder2 = blanketSoOrder1;
    int? shipmentCntr = blanketSoOrder2.ShipmentCntr;
    blanketSoOrder2.ShipmentCntr = shipmentCntr.HasValue ? new int?(shipmentCntr.GetValueOrDefault() + 1) : new int?();
    blanketSoOrder1.ShipmentCntrUpdated = new bool?(true);
    GraphHelper.Caches<BlanketSOOrder>((PXGraph) this.Base).Update(blanketSoOrder1);
  }
}
