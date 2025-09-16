// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SO2PO.SO2POSyncBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SO2PO;

public abstract class SO2POSyncBase<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  private PXCache<PX.Objects.SO.SOOrder> _soOrderCache;
  private PXCache<SOLine4> _soLineCache;
  private PXCache<PX.Objects.SO.SOLineSplit> _soSplitCache;
  private PXCache<INItemPlan> _planCache;

  public PXCache<PX.Objects.SO.SOOrder> SOOrderCache
  {
    get
    {
      return this._soOrderCache ?? (this._soOrderCache = GraphHelper.Caches<PX.Objects.SO.SOOrder>((PXGraph) this.Base));
    }
  }

  public PXCache<SOLine4> SOLineCache
  {
    get
    {
      return this._soLineCache ?? (this._soLineCache = GraphHelper.Caches<SOLine4>((PXGraph) this.Base));
    }
  }

  public PXCache<PX.Objects.SO.SOLineSplit> SOSplitCache
  {
    get
    {
      return this._soSplitCache ?? (this._soSplitCache = GraphHelper.Caches<PX.Objects.SO.SOLineSplit>((PXGraph) this.Base));
    }
  }

  public PXCache<INItemPlan> PlanCache
  {
    get
    {
      return this._planCache ?? (this._planCache = GraphHelper.Caches<INItemPlan>((PXGraph) this.Base, true));
    }
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    GraphHelper.EnsureCachePersistence<PX.Objects.SO.SOOrder>((PXGraph) this.Base);
    GraphHelper.EnsureCachePersistence<SOLine4>((PXGraph) this.Base);
    GraphHelper.EnsureCachePersistence<PX.Objects.SO.SOLineSplit>((PXGraph) this.Base);
  }

  protected void UpdateSchedulesFromCompletedPO(
    PX.Objects.SO.SOLineSplit parentSchedule,
    INItemPlan demand,
    bool cancelDropShip = false)
  {
    using (new SimpleScope((Action) (() => ((FieldDefaultingEvents) this.Base.FieldDefaulting).AddAbstractHandler<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.locationID, int>(new Action<AbstractEvents.IFieldDefaulting<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.locationID, int?>>(CancelLocationDefaulting))), (Action) (() => ((FieldDefaultingEvents) this.Base.FieldDefaulting).RemoveAbstractHandler<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.locationID, int>(new Action<AbstractEvents.IFieldDefaulting<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.locationID, int?>>(CancelLocationDefaulting)))))
    {
      PX.Objects.SO.SOLineSplit copy1 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(parentSchedule);
      this.ClearScheduleReferences(ref copy1);
      copy1.LotSerialNbr = demand.LotSerialNbr;
      copy1.SiteID = demand.SiteID;
      Decimal? nullable1 = parentSchedule.POSource == "D" ? parentSchedule.BaseShippedQty : parentSchedule.BaseReceivedQty;
      PX.Objects.SO.SOLineSplit soLineSplit1 = copy1;
      Decimal? baseQty = parentSchedule.BaseQty;
      Decimal? nullable2 = parentSchedule.BaseQtyOnOrders;
      Decimal? nullable3 = baseQty.HasValue & nullable2.HasValue ? new Decimal?(baseQty.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable4 = nullable1;
      Decimal? nullable5;
      if (!(nullable3.HasValue & nullable4.HasValue))
      {
        nullable2 = new Decimal?();
        nullable5 = nullable2;
      }
      else
        nullable5 = new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault());
      soLineSplit1.BaseQty = nullable5;
      PX.Objects.SO.SOLineSplit soLineSplit2 = copy1;
      PXCache<PX.Objects.SO.SOLineSplit> soSplitCache = this.SOSplitCache;
      int? inventoryId = copy1.InventoryID;
      string uom = copy1.UOM;
      nullable4 = copy1.BaseQty;
      Decimal num = nullable4.Value;
      Decimal? nullable6 = new Decimal?(INUnitAttribute.ConvertFromBase((PXCache) soSplitCache, inventoryId, uom, num, INPrecision.QUANTITY));
      soLineSplit2.Qty = nullable6;
      copy1.BaseReceivedQty = new Decimal?(0M);
      copy1.ReceivedQty = new Decimal?(0M);
      copy1.BaseShippedQty = new Decimal?(0M);
      copy1.ShippedQty = new Decimal?(0M);
      copy1.QtyOnOrders = new Decimal?(0M);
      bool valueOrDefault = PXParentAttribute.SelectParent<PX.Objects.SO.SOLine>((PXCache) this.SOSplitCache, (object) parentSchedule).IsSpecialOrder.GetValueOrDefault();
      if (cancelDropShip)
      {
        copy1.POCreate = new bool?(true);
        copy1.POSource = "D";
      }
      else if (valueOrDefault)
      {
        copy1.POCreate = new bool?(true);
        copy1.POSource = "O";
      }
      INItemPlan plan = (INItemPlan) null;
      if (this.SOOrderCache.Rows.Current?.Behavior == "BL")
      {
        if (!parentSchedule.Completed.GetValueOrDefault())
        {
          PX.Objects.SO.SOLineSplit soLineSplit3 = parentSchedule;
          nullable4 = soLineSplit3.Qty;
          nullable3 = copy1.Qty;
          Decimal? nullable7;
          if (!(nullable4.HasValue & nullable3.HasValue))
          {
            nullable2 = new Decimal?();
            nullable7 = nullable2;
          }
          else
            nullable7 = new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault());
          soLineSplit3.Qty = nullable7;
          this.SOSplitCache.Update(parentSchedule);
        }
      }
      else
      {
        INItemPlan copy2 = PXCache<INItemPlan>.CreateCopy(demand);
        INItemPlan inItemPlan = copy2;
        string str;
        if (!cancelDropShip)
        {
          if (!valueOrDefault)
          {
            PX.Objects.SO.SOOrder current = this.SOOrderCache.Rows.Current;
            str = (current != null ? (current.Hold.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? "69" : "60";
          }
          else
            str = "66";
        }
        else
          str = demand.PlanType;
        inItemPlan.PlanType = str;
        copy2.PlanID = new long?();
        copy2.SupplyPlanID = new long?();
        copy2.DemandPlanID = new long?();
        copy2.PlanQty = copy1.BaseQty;
        copy2.VendorID = cancelDropShip | valueOrDefault ? demand.VendorID : new int?();
        copy2.VendorLocationID = cancelDropShip | valueOrDefault ? demand.VendorLocationID : new int?();
        copy2.FixedSource = cancelDropShip | valueOrDefault ? "P" : "N";
        plan = this.PlanCache.Insert(copy2);
      }
      copy1.PlanID = (long?) plan?.PlanID;
      this.BreakupSplitByUom(this.SOSplitCache.Insert(copy1), plan);
    }

    static void CancelLocationDefaulting(
      AbstractEvents.IFieldDefaulting<PX.Objects.SO.SOLineSplit, IBqlField, int?> e)
    {
      if (e.Row == null || e.Row.RequireLocation.GetValueOrDefault())
        return;
      e.NewValue = new int?();
      ((ICancelEventArgs) e).Cancel = true;
    }
  }

  protected void ClearScheduleReferences(ref PX.Objects.SO.SOLineSplit schedule)
  {
    schedule.ParentSplitLineNbr = schedule.SplitLineNbr;
    schedule.SplitLineNbr = new int?();
    schedule.Completed = new bool?(false);
    schedule.PlanID = new long?();
    schedule.ClearPOFlags();
    schedule.ClearPOReferences();
    schedule.POSource = "N";
    schedule.ClearSOReferences();
    schedule.RefNoteID = new Guid?();
  }

  protected void BreakupSplitByUom(PX.Objects.SO.SOLineSplit split, INItemPlan plan = null)
  {
    Decimal? qty = split.Qty;
    Decimal num1 = 1M;
    Decimal? nullable = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() % num1) : new Decimal?();
    Decimal num2 = 0M;
    if (nullable.GetValueOrDefault() == num2 & nullable.HasValue || split.Behavior == "BL")
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, split.InventoryID);
    int num3;
    if (inventoryItem == null)
    {
      num3 = 1;
    }
    else
    {
      bool? decimalSalesUnit = inventoryItem.DecimalSalesUnit;
      bool flag = false;
      num3 = !(decimalSalesUnit.GetValueOrDefault() == flag & decimalSalesUnit.HasValue) ? 1 : 0;
    }
    if (num3 != 0 || string.Equals(inventoryItem.BaseUnit, split.UOM, StringComparison.OrdinalIgnoreCase))
      return;
    INUnit inUnit = INUnit.UK.ByInventory.Find((PXGraph) this.Base, split.InventoryID, split.UOM);
    if (inUnit?.UnitMultDiv != "M")
      return;
    nullable = inUnit.UnitRate;
    Decimal num4 = 1M;
    if (nullable.GetValueOrDefault() <= num4 & nullable.HasValue)
      return;
    PX.Objects.SO.SOLineSplit copy1 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(split);
    nullable = split.Qty;
    Decimal num5 = Math.Floor(nullable.GetValueOrDefault());
    if (num5 == 0M)
    {
      copy1.Qty = copy1.BaseQty;
      copy1.UOM = inventoryItem.BaseUnit;
      split = this.SOSplitCache.Update(copy1);
    }
    else
    {
      PX.Objects.SO.SOLineSplit copy2 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(split);
      if (plan == null)
        plan = INItemPlan.PK.Find((PXGraph) this.Base, split.PlanID, (PKFindOptions) 1);
      copy1.Qty = new Decimal?(num5);
      split = this.SOSplitCache.Update(copy1);
      plan.PlanQty = split.BaseQty;
      plan = this.PlanCache.Update(plan);
      copy2.SplitLineNbr = new int?();
      PX.Objects.SO.SOLineSplit soLineSplit = copy2;
      nullable = soLineSplit.BaseQty;
      Decimal? baseQty = split.BaseQty;
      soLineSplit.BaseQty = nullable.HasValue & baseQty.HasValue ? new Decimal?(nullable.GetValueOrDefault() - baseQty.GetValueOrDefault()) : new Decimal?();
      copy2.Qty = copy2.BaseQty;
      copy2.UOM = inventoryItem.BaseUnit;
      INItemPlan copy3 = PXCache<INItemPlan>.CreateCopy(plan);
      copy3.PlanID = new long?();
      copy3.PlanQty = copy2.BaseQty;
      INItemPlan inItemPlan = this.PlanCache.Insert(copy3);
      copy2.PlanID = inItemPlan.PlanID;
      this.SOSplitCache.Update(copy2);
    }
  }
}
