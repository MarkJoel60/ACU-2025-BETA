// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Models.SOLineShipLineSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using PX.Objects.SO.Interfaces;
using System;

#nullable disable
namespace PX.Objects.SO.Models;

public class SOLineShipLineSource : IShipLineSource, ICloneable
{
  private readonly SOShipmentPlan _plan;
  private readonly PX.Objects.SO.SOLineSplit _linesplit;
  private readonly PX.Objects.SO.SOLine _line;

  public SOLineShipLineSource(
    PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite> details)
    : this(PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite>.op_Implicit(details), PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite>.op_Implicit(details), PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite>.op_Implicit(details), PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite>.op_Implicit(details), PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite>.op_Implicit(details), PXResult<SOShipmentPlan, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine, PX.Objects.IN.InventoryItem, INLotSerClass, PX.Objects.IN.INSite>.op_Implicit(details))
  {
  }

  public SOLineShipLineSource(
    SOShipmentPlan plan,
    PX.Objects.SO.SOLineSplit lineSplit,
    PX.Objects.SO.SOLine line,
    PX.Objects.IN.InventoryItem inventoryItem,
    INLotSerClass lotSerClass,
    PX.Objects.IN.INSite site)
  {
    this._plan = plan;
    this._linesplit = lineSplit;
    this._line = line;
    this.INLotSerClass = lotSerClass;
    this.InventoryItem = inventoryItem;
    this.INSite = site;
  }

  public Decimal? PlanQty
  {
    get => this._plan.PlanQty;
    set => this._plan.PlanQty = value;
  }

  public long? PlanID => this._plan.PlanID;

  public string PlanType => this._plan.PlanType;

  public string LotSerialNbr => this._plan.LotSerialNbr;

  public bool Selected => this._plan.Selected.GetValueOrDefault();

  public string NewPlanType => this._linesplit.PlanType;

  public bool RequireAllocationUnallocated
  {
    get
    {
      if (this._plan.RequireAllocation.GetValueOrDefault() && this._linesplit.LineType != "GN" && this._linesplit.Operation != "R")
      {
        short? inclQtySoShipping = this._plan.InclQtySOShipping;
        if ((inclQtySoShipping.HasValue ? new int?((int) inclQtySoShipping.GetValueOrDefault()) : new int?()).GetValueOrDefault() != 1)
        {
          short? inclQtySoShipped = this._plan.InclQtySOShipped;
          return (inclQtySoShipped.HasValue ? new int?((int) inclQtySoShipped.GetValueOrDefault()) : new int?()).GetValueOrDefault() != 1;
        }
      }
      return false;
    }
  }

  public bool RequireINItemPlanUpdate
  {
    get
    {
      return this._plan.PlanType != this._linesplit.PlanType && !this._linesplit.POCreate.GetValueOrDefault() && !this._linesplit.IsAllocated.GetValueOrDefault();
    }
  }

  public INLotSerClass INLotSerClass { get; }

  public PX.Objects.IN.InventoryItem InventoryItem { get; }

  public PX.Objects.IN.INSite INSite { get; }

  public DateTime? ExpireDate => this._linesplit.ExpireDate;

  public object FilesAndNotesSource => (object) this._line;

  public Decimal? MinRequiredBaseShippedQty
  {
    get
    {
      Decimal? planQty = this._plan.PlanQty;
      Decimal? completeQtyMin = this._line.CompleteQtyMin;
      return !(planQty.HasValue & completeQtyMin.HasValue) ? new Decimal?() : new Decimal?(planQty.GetValueOrDefault() * completeQtyMin.GetValueOrDefault() / 100M);
    }
  }

  public string ShippingRule => this._line.ShipComplete;

  public bool? IsStockItem => this._line.IsStockItem;

  public int? InventoryID => this._line.InventoryID;

  public int? SubItemID => this._line.SubItemID;

  public int? SiteID => this._line.SiteID;

  public int? CostCenterID => this._line.CostCenterID;

  public string TranDesc => this._line.TranDesc;

  public int? ProjectID => this._line.ProjectID;

  public int? TaskID => this._line.TaskID;

  public int? CostCodeID => this._line.CostCodeID;

  public string UOM => this._linesplit.UOM;

  public object Clone()
  {
    return (object) new SOLineShipLineSource(PXCache<SOShipmentPlan>.CreateCopy(this._plan), this._linesplit, this._line, this.InventoryItem, this.INLotSerClass, this.INSite);
  }

  public PX.Objects.SO.SOLine SOLine => this._line;

  public PX.Objects.SO.SOLineSplit SOLineSplit => this._linesplit;

  public int? LineNbr => this._line.LineNbr;
}
