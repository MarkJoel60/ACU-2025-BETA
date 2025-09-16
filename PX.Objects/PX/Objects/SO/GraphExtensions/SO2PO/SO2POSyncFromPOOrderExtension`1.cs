// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SO2PO.SO2POSyncFromPOOrderExtension`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SO2PO;

public class SO2POSyncFromPOOrderExtension<TGraph> : SO2POSyncBase<TGraph> where TGraph : PXGraph
{
  public virtual void Process((string Type, string Nbr) poOrderKey)
  {
    if (poOrderKey.Nbr == null)
      return;
    foreach (PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder, PX.Objects.SO.SOLineSplit> pxResult in PXSelectBase<PX.Objects.PO.POLine, PXSelectJoin<PX.Objects.PO.POLine, InnerJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POLine.FK.Order>, InnerJoin<PX.Objects.SO.SOLineSplit, On<PX.Objects.SO.SOLineSplit.pOType, Equal<PX.Objects.PO.POLine.orderType>, And<PX.Objects.SO.SOLineSplit.pONbr, Equal<PX.Objects.PO.POLine.orderNbr>, And<PX.Objects.SO.SOLineSplit.pOLineNbr, Equal<PX.Objects.PO.POLine.lineNbr>>>>>>, Where<PX.Objects.PO.POLine.orderType, Equal<Required<PX.Objects.PO.POLine.orderType>>, And<PX.Objects.PO.POLine.orderNbr, Equal<Required<PX.Objects.PO.POLine.orderNbr>>, And2<Where<PX.Objects.PO.POLine.cancelled, Equal<boolTrue>, Or<PX.Objects.PO.POLine.completed, Equal<boolTrue>>>, And<PX.Objects.PO.POOrder.orderType, Equal<POOrderType.dropShip>, And<PX.Objects.PO.POOrder.isLegacyDropShip, Equal<True>, And<PX.Objects.SO.SOLineSplit.receivedQty, LessEqual<PX.Objects.SO.SOLineSplit.qty>, And<PX.Objects.SO.SOLineSplit.pOCancelled, NotEqual<boolTrue>, And<PX.Objects.SO.SOLineSplit.completed, NotEqual<boolTrue>>>>>>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) poOrderKey.Type,
      (object) poOrderKey.Nbr
    }))
    {
      PX.Objects.PO.POLine poLine = PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder, PX.Objects.SO.SOLineSplit>.op_Implicit(pxResult);
      PX.Objects.SO.SOLineSplit copy1 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(PXResult<PX.Objects.PO.POLine, PX.Objects.PO.POOrder, PX.Objects.SO.SOLineSplit>.op_Implicit(pxResult));
      INItemPlan inItemPlan = INItemPlan.PK.Find((PXGraph) this.Base, copy1.PlanID);
      if (inItemPlan != null)
      {
        long? supplyPlanId = inItemPlan.SupplyPlanID;
        long? nullable1 = poLine.PlanID;
        if (supplyPlanId.GetValueOrDefault() == nullable1.GetValueOrDefault() & supplyPlanId.HasValue == nullable1.HasValue)
        {
          INItemPlan copy2 = PXCache<INItemPlan>.CreateCopy(inItemPlan);
          this.SOOrderCache.Rows.Current = PXParentAttribute.SelectParent<PX.Objects.SO.SOOrder>((PXCache) this.SOSplitCache, (object) copy1);
          bool? nullable2 = copy1.Completed;
          if (!nullable2.GetValueOrDefault())
          {
            nullable2 = copy1.POCancelled;
            if (!nullable2.GetValueOrDefault())
            {
              Decimal? baseQty = copy1.BaseQty;
              Decimal? baseReceivedQty = copy1.BaseReceivedQty;
              if (baseQty.GetValueOrDefault() >= baseReceivedQty.GetValueOrDefault() & baseQty.HasValue & baseReceivedQty.HasValue)
              {
                nullable2 = poLine.Cancelled;
                bool cancelDropShip = nullable2.GetValueOrDefault() && POLineType.IsDropShip(poLine.LineType);
                this.UpdateSchedulesFromCompletedPO(copy1, copy2, cancelDropShip);
                if (this.PlanCache.GetStatus(copy2) != 2)
                  this.PlanCache.Delete(copy2);
                ((PXCache) this.SOSplitCache).SetStatus((object) copy1, (PXEntryStatus) 0);
                PX.Objects.SO.SOLineSplit copy3 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(copy1);
                PX.Objects.SO.SOLineSplit soLineSplit = copy3;
                nullable1 = new long?();
                long? nullable3 = nullable1;
                soLineSplit.PlanID = nullable3;
                copy3.Completed = new bool?(true);
                copy3.POCompleted = poLine.Completed;
                copy3.POCancelled = poLine.Cancelled;
                this.SOSplitCache.Update(copy3);
              }
            }
          }
        }
      }
    }
  }
}
