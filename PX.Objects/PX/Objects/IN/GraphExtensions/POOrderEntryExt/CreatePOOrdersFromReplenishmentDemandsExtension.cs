// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.POOrderEntryExt.CreatePOOrdersFromReplenishmentDemandsExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.PO;
using PX.Objects.PO.GraphExtensions.POOrderEntryExt;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.POOrderEntryExt;

public class CreatePOOrdersFromReplenishmentDemandsExtension : 
  PXGraphExtension<CreatePOOrdersFromDemandsExtension, POOrderEntry>
{
  [PXMergeAttributes]
  [PXDefault]
  protected void _(PX.Data.Events.CacheAttached<INReplenishmentLine.pOType> e)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (PX.Objects.PO.POOrder.orderNbr))]
  protected void _(PX.Data.Events.CacheAttached<INReplenishmentLine.pONbr> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault]
  protected void _(
    PX.Data.Events.CacheAttached<INReplenishmentLine.pOLineNbr> e)
  {
  }

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POOrderEntryExt.CreatePOOrdersFromDemandsExtension.FillNewPOLineFromDemand(PX.Objects.PO.POLine,PX.Objects.PO.POFixedDemand,PX.Objects.PO.GraphExtensions.POOrderEntryExt.PODemandSourceInfo)" />
  [PXOverride]
  public PX.Objects.PO.POLine FillNewPOLineFromDemand(
    PX.Objects.PO.POLine poLine,
    POFixedDemand demand,
    PODemandSourceInfo demandSource,
    Func<PX.Objects.PO.POLine, POFixedDemand, PODemandSourceInfo, PX.Objects.PO.POLine> base_FillNewPOLineFromDemand)
  {
    poLine = base_FillNewPOLineFromDemand(poLine, demand, demandSource);
    if (demand.PlanType == "90")
      poLine.LineType = "GR";
    return poLine;
  }

  /// Overrides <see cref="M:PX.Objects.PO.GraphExtensions.POOrderEntryExt.CreatePOOrdersFromDemandsExtension.LinkPOLineToSource(PX.Objects.PO.POLine,PX.Objects.PO.POFixedDemand,PX.Objects.PO.GraphExtensions.POOrderEntryExt.PODemandSourceInfo)" />
  [PXOverride]
  public PX.Objects.PO.POLine LinkPOLineToSource(
    PX.Objects.PO.POLine poLine,
    POFixedDemand demand,
    PODemandSourceInfo demandSource,
    Func<PX.Objects.PO.POLine, POFixedDemand, PODemandSourceInfo, PX.Objects.PO.POLine> base_LinkPOLineToSource)
  {
    poLine = base_LinkPOLineToSource(poLine, demand, demandSource);
    if (demand.PlanType == "90")
      poLine = this.LinkPOLineToReplenishmentLine(poLine, demand, demandSource);
    return poLine;
  }

  protected virtual PX.Objects.PO.POLine LinkPOLineToReplenishmentLine(
    PX.Objects.PO.POLine poLine,
    POFixedDemand demand,
    PODemandSourceInfo demandSource)
  {
    Decimal? orderQty = demand.OrderQty;
    Decimal? planUnitQty1 = demand.PlanUnitQty;
    if (!(orderQty.GetValueOrDefault() == planUnitQty1.GetValueOrDefault() & orderQty.HasValue == planUnitQty1.HasValue))
    {
      INItemPlan inItemPlan1 = (INItemPlan) PrimaryKeyOf<INItemPlan>.By<INItemPlan.planID>.Find((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base, (INItemPlan.planID) demand, (PKFindOptions) 0);
      INItemPlan copy = PXCache<INItemPlan>.CreateCopy(inItemPlan1);
      copy.PlanID = new long?();
      INItemPlan inItemPlan2 = copy;
      Decimal? planUnitQty2 = demand.PlanUnitQty;
      Decimal? nullable1 = demand.OrderQty;
      Decimal? nullable2 = planUnitQty2.HasValue & nullable1.HasValue ? new Decimal?(planUnitQty2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
      inItemPlan2.PlanQty = nullable2;
      Decimal? nullable3;
      if (demand.UnitMultDiv == "M")
      {
        INItemPlan inItemPlan3 = copy;
        nullable1 = inItemPlan3.PlanQty;
        nullable3 = demand.UnitRate;
        inItemPlan3.PlanQty = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable3.GetValueOrDefault()) : new Decimal?();
      }
      else
      {
        INItemPlan inItemPlan4 = copy;
        nullable3 = inItemPlan4.PlanQty;
        nullable1 = demand.UnitRate;
        inItemPlan4.PlanQty = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() / nullable1.GetValueOrDefault()) : new Decimal?();
      }
      PXCache<INItemPlan> pxCache = GraphHelper.Caches<INItemPlan>((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base);
      pxCache.Insert(copy);
      pxCache.RaiseRowDeleted((INItemPlan) demand);
      POFixedDemand poFixedDemand = demand;
      nullable1 = inItemPlan1.PlanQty;
      nullable3 = copy.PlanQty;
      Decimal? nullable4 = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
      poFixedDemand.PlanQty = nullable4;
      pxCache.RaiseRowInserted((INItemPlan) demand);
    }
    ((PXSelectBase<INReplenishmentOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Replenihment).Current = PXResultset<INReplenishmentOrder>.op_Implicit(((PXSelectBase<INReplenishmentOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Replenihment).Search<INReplenishmentOrder.noteID>((object) demand.RefNoteID, Array.Empty<object>()));
    if (((PXSelectBase<INReplenishmentOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Replenihment).Current != null)
    {
      INReplenishmentLine copy = PXCache<INReplenishmentLine>.CreateCopy(((PXSelectBase<INReplenishmentLine>) ((PXGraphExtension<POOrderEntry>) this).Base.ReplenishmentLines).Insert(new INReplenishmentLine()));
      copy.POType = poLine.OrderType;
      copy.PONbr = ((PXSelectBase<PX.Objects.PO.POOrder>) ((PXGraphExtension<POOrderEntry>) this).Base.Document).Current.OrderNbr;
      copy.POLineNbr = poLine.LineNbr;
      copy.InventoryID = poLine.InventoryID;
      copy.SubItemID = poLine.SubItemID;
      copy.UOM = poLine.UOM;
      copy.VendorID = poLine.VendorID;
      copy.VendorLocationID = poLine.VendorLocationID;
      copy.Qty = poLine.OrderQty;
      copy.SiteID = demand.POSiteID;
      copy.PlanID = demand.PlanID;
      ((PXSelectBase<INReplenishmentLine>) ((PXGraphExtension<POOrderEntry>) this).Base.ReplenishmentLines).Update(copy);
      GraphHelper.Caches<INItemPlan>((PXGraph) ((PXGraphExtension<POOrderEntry>) this).Base).Delete((INItemPlan) demand);
    }
    return poLine;
  }
}
