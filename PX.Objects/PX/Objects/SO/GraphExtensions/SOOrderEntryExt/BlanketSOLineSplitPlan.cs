// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.BlanketSOLineSplitPlan
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.PO;
using PX.Objects.SO.DAC.Projections;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class BlanketSOLineSplitPlan : ItemPlan<SOOrderEntry, BlanketSOOrder, BlanketSOLineSplit>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  public override INItemPlan DefaultValues(INItemPlan planRow, BlanketSOLineSplit splitRow)
  {
    bool? nullable1;
    if (string.IsNullOrEmpty(planRow.PlanType))
    {
      nullable1 = splitRow.Completed;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = splitRow.POCompleted;
        if (!nullable1.GetValueOrDefault() && !(splitRow.LineType == "MI"))
        {
          BlanketSOLine blanketSoLine = PXParentAttribute.SelectParent<BlanketSOLine>((PXCache) this.ItemPlanSourceCache, (object) splitRow);
          BlanketSOOrder order = PXParentAttribute.SelectParent<BlanketSOOrder>((PXCache) GraphHelper.Caches<BlanketSOLine>((PXGraph) this.Base), (object) blanketSoLine);
          planRow.PlanType = order != null ? this.CalcPlanType(order, splitRow) : throw new RowNotFoundException((PXCache) GraphHelper.Caches<BlanketSOOrder>((PXGraph) this.Base), new object[2]
          {
            (object) splitRow.OrderType,
            (object) splitRow.OrderNbr
          });
          if (string.IsNullOrEmpty(planRow.PlanType))
            return (INItemPlan) null;
          nullable1 = splitRow.POCreate;
          if (nullable1.GetValueOrDefault())
          {
            planRow.FixedSource = "P";
            planRow.SourceSiteID = !(splitRow.POType != "BL") || !(splitRow.POType != "DP") || !(splitRow.POSource == "O") ? splitRow.SiteID : splitRow.SiteID;
            planRow.VendorID = splitRow.VendorID;
            if (planRow.VendorID.HasValue)
              planRow.VendorLocationID = POItemCostManager.FetchLocation((PXGraph) this.Base, splitRow.VendorID, splitRow.InventoryID, splitRow.SubItemID, splitRow.SiteID);
          }
          else
          {
            INItemPlan inItemPlan = planRow;
            int? siteId = splitRow.SiteID;
            int? toSiteId = splitRow.ToSiteID;
            string str = !(siteId.GetValueOrDefault() == toSiteId.GetValueOrDefault() & siteId.HasValue == toSiteId.HasValue) ? "T" : "N";
            inItemPlan.FixedSource = str;
            planRow.SourceSiteID = splitRow.SiteID;
          }
          planRow.BAccountID = blanketSoLine.CustomerID;
          planRow.InventoryID = splitRow.InventoryID;
          planRow.SubItemID = splitRow.SubItemID;
          planRow.SiteID = splitRow.SiteID;
          planRow.LocationID = splitRow.LocationID;
          planRow.ProjectID = blanketSoLine.ProjectID;
          planRow.TaskID = blanketSoLine.TaskID;
          planRow.PlanDate = splitRow.ShipDate;
          planRow.UOM = blanketSoLine.UOM;
          planRow.LotSerialNbr = splitRow.LotSerialNbr;
          INItemPlan inItemPlan1 = planRow;
          nullable1 = order.Hold;
          int num;
          if (!nullable1.GetValueOrDefault())
          {
            nullable1 = order.Approved;
            num = (nullable1.HasValue ? new bool?(!nullable1.GetValueOrDefault()) : new bool?()).GetValueOrDefault() ? 1 : 0;
          }
          else
            num = 1;
          bool? nullable2 = new bool?(num != 0);
          inItemPlan1.Hold = nullable2;
          planRow.RefNoteID = order.NoteID;
          goto label_16;
        }
      }
      return (INItemPlan) null;
    }
label_16:
    INItemPlan inItemPlan2 = planRow;
    nullable1 = splitRow.POCreate;
    Decimal? nullable3 = nullable1.GetValueOrDefault() ? splitRow.BaseUnreceivedQty : splitRow.BaseQty;
    Decimal? baseQtyOnOrders = splitRow.BaseQtyOnOrders;
    Decimal? nullable4 = nullable3.HasValue & baseQtyOnOrders.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - baseQtyOnOrders.GetValueOrDefault()) : new Decimal?();
    inItemPlan2.PlanQty = nullable4;
    return planRow;
  }

  protected virtual string CalcPlanType(BlanketSOOrder order, BlanketSOLineSplit split)
  {
    return split.POCreate.GetValueOrDefault() && split.POSource == "B" ? (!order.IsExpired.GetValueOrDefault() || !string.IsNullOrEmpty(split.PONbr) ? "6B" : (string) null) : (split.POCreate.GetValueOrDefault() && split.POSource == "O" ? (!order.IsExpired.GetValueOrDefault() || !string.IsNullOrEmpty(split.PONbr) ? "66" : (string) null) : (!split.IsAllocated.GetValueOrDefault() ? (string) null : "61"));
  }

  protected override string GetRefEntityType() => typeof (PX.Objects.SO.SOOrder).FullName;
}
