// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.SOShipLineSplitPlanBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

public abstract class SOShipLineSplitPlanBase<TItemPlanSource> : 
  ItemPlan<SOShipmentEntry, PX.Objects.SO.SOShipment, TItemPlanSource>
  where TItemPlanSource : class, IItemPlanSOShipSource, IBqlTable, new()
{
  public override void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOShipment> e)
  {
    base._(e);
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOShipment>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOShipment.shipDate, PX.Objects.SO.SOShipment.hold>((object) e.Row, (object) e.OldRow))
      return;
    HashSet<long?> nullableSet = this.CollectShipmentPlans();
    foreach (INItemPlan inItemPlan in ((PXCache) this.PlanCache).Inserted)
    {
      if (nullableSet.Contains(inItemPlan.PlanID))
      {
        inItemPlan.Hold = e.Row.Hold;
        inItemPlan.PlanDate = e.Row.ShipDate;
      }
    }
    this.UpdatePlansOnParentUpdated(e.Row);
  }

  protected abstract void UpdatePlansOnParentUpdated(PX.Objects.SO.SOShipment parent);

  protected abstract HashSet<long?> CollectShipmentPlans(string shipmentNbr = null);

  public override INItemPlan DefaultValues(INItemPlan planRow, TItemPlanSource splitRow)
  {
    if (!splitRow.Released.GetValueOrDefault())
    {
      bool? nullable1 = splitRow.IsStockItem;
      bool flag = false;
      if (!(nullable1.GetValueOrDefault() == flag & nullable1.HasValue))
      {
        nullable1 = planRow.IsTemporary;
        if (!nullable1.GetValueOrDefault())
        {
          nullable1 = splitRow.Confirmed;
          if (nullable1.GetValueOrDefault())
            return planRow;
        }
        nullable1 = planRow.IsTemporary;
        SOShipLine parent = nullable1.GetValueOrDefault() ? (SOShipLine) null : PXParentAttribute.SelectParent<SOShipLine>((PXCache) this.ItemPlanSourceCache, (object) splitRow);
        nullable1 = planRow.IsTemporary;
        if (!nullable1.GetValueOrDefault() && parent == null)
          return (INItemPlan) null;
        planRow.BAccountID = (int?) parent?.CustomerID;
        planRow.PlanType = splitRow.PlanType;
        planRow.OrigPlanType = splitRow.OrigPlanType;
        planRow.IgnoreOrigPlan = splitRow.IsComponentItem;
        planRow.InventoryID = splitRow.InventoryID;
        INItemPlan inItemPlan1 = planRow;
        int? inventoryId = splitRow.InventoryID;
        int? nullable2 = (int?) parent?.InventoryID;
        int? nullable3;
        if (inventoryId.GetValueOrDefault() == nullable2.GetValueOrDefault() & inventoryId.HasValue == nullable2.HasValue)
        {
          nullable2 = new int?();
          nullable3 = nullable2;
        }
        else if (parent == null)
        {
          nullable2 = new int?();
          nullable3 = nullable2;
        }
        else
          nullable3 = parent.InventoryID;
        inItemPlan1.KitInventoryID = nullable3;
        planRow.Reverse = new bool?(splitRow.Operation == "R");
        planRow.SubItemID = splitRow.SubItemID;
        planRow.SiteID = splitRow.SiteID;
        planRow.LocationID = splitRow.LocationID;
        planRow.LotSerialNbr = splitRow.LotSerialNbr;
        INItemPlan inItemPlan2 = planRow;
        int? nullable4;
        if (parent == null)
        {
          nullable2 = new int?();
          nullable4 = nullable2;
        }
        else
          nullable4 = parent.ProjectID;
        inItemPlan2.ProjectID = nullable4;
        INItemPlan inItemPlan3 = planRow;
        int? nullable5;
        if (parent == null)
        {
          nullable2 = new int?();
          nullable5 = nullable2;
        }
        else
          nullable5 = parent.TaskID;
        inItemPlan3.TaskID = nullable5;
        INItemPlan inItemPlan4 = planRow;
        int? nullable6;
        if (parent == null)
        {
          nullable2 = new int?();
          nullable6 = nullable2;
        }
        else
          nullable6 = parent.CostCenterID;
        inItemPlan4.CostCenterID = nullable6;
        planRow.IsTempLotSerial = new bool?(!string.IsNullOrEmpty(splitRow.AssignedNbr) && INLotSerialNbrAttribute.StringsEqual(splitRow.AssignedNbr, planRow.LotSerialNbr));
        nullable1 = planRow.IsTempLotSerial;
        if (nullable1.GetValueOrDefault())
          planRow.LotSerialNbr = (string) null;
        planRow.PlanDate = splitRow.ShipDate;
        planRow.OrigUOM = parent?.OrderUOM;
        planRow.UOM = parent?.UOM;
        planRow.PlanQty = splitRow.BaseQty;
        PX.Objects.SO.SOShipment current = ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current;
        planRow.RefNoteID = current.NoteID;
        planRow.Hold = current.Hold;
        if (string.IsNullOrEmpty(planRow.PlanType))
          return (INItemPlan) null;
        if (parent != null)
        {
          SOShipLineSplitPlanDefaultValuesExtension implementation = ((PXGraph) this.Base).FindImplementation<SOShipLineSplitPlanDefaultValuesExtension>();
          if (!planRow.OrigNoteID.HasValue)
            planRow.OrigNoteID = implementation.GetOrigDocumentNoteID(parent);
          nullable2 = parent.OrigSplitLineNbr;
          if (nullable2.HasValue)
            implementation.UpdateINItemPlanFromLineSourceSplit(planRow, parent);
        }
        return planRow;
      }
    }
    return (INItemPlan) null;
  }

  public override TNode UpdateAllocatedQuantitiesBase<TNode>(
    INItemPlan plan,
    INPlanType plantype,
    bool InclQtyAvail)
  {
    TNode node = base.UpdateAllocatedQuantitiesBase<TNode>(plan, plantype, InclQtyAvail);
    if (typeof (TNode) == typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter) && node is PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter && !statusByCostCenter.SkipQtyValidation.GetValueOrDefault() && plan.LocationID.HasValue)
    {
      INLocation inLocation = INLocation.PK.Find((PXGraph) this.Base, plan.LocationID);
      if ((inLocation != null ? (inLocation.IsSorting.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        statusByCostCenter.SkipQtyValidation = new bool?(true);
    }
    return node;
  }
}
