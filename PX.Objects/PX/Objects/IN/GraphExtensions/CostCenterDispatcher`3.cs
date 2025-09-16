// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.CostCenterDispatcher`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class CostCenterDispatcher<TGraph, TLine, TCostCenterField> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TLine : class, IItemPlanMaster, IBqlTable, new()
  where TCostCenterField : class, IBqlField
{
  [PXCopyPasteHiddenView]
  public PXSelect<INCostCenter> CostCenters;

  [PXMergeAttributes]
  [CostCenterDBDefault]
  protected virtual void _(Events.CacheAttached<INItemPlan.costCenterID> e)
  {
  }

  [PXMergeAttributes]
  [CostCenterDBDefault]
  protected virtual void _(
    Events.CacheAttached<SiteStatusByCostCenter.costCenterID> e)
  {
  }

  [PXMergeAttributes]
  [CostCenterDBDefault]
  protected virtual void _(
    Events.CacheAttached<LocationStatusByCostCenter.costCenterID> e)
  {
  }

  [PXMergeAttributes]
  [CostCenterDBDefault]
  protected virtual void _(
    Events.CacheAttached<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter.costCenterID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Cost Layer Type", Enabled = false, Visible = false)]
  protected virtual void _(Events.CacheAttached<INTran.costLayerType> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "To Cost Layer Type", Enabled = false, Visible = false)]
  protected virtual void _(Events.CacheAttached<INTran.toCostLayerType> e)
  {
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.MoveCostCenterViewCacheToTop();
    this.SubscribeToFieldsDependOn();
    this.SubscribeToRowInserting();
  }

  protected virtual void MoveCostCenterViewCacheToTop()
  {
    int index = this.Base.Views.Caches.IndexOf(typeof (INCostCenter));
    if (index <= 0)
      return;
    this.Base.Views.Caches.RemoveAt(index);
    this.Base.Views.Caches.Insert(0, typeof (INCostCenter));
  }

  protected virtual void SubscribeToFieldsDependOn()
  {
    foreach (Type c in this.Base.FindAllImplementations<ICostCenterSupport<TLine>>().SelectMany<ICostCenterSupport<TLine>, Type>((Func<ICostCenterSupport<TLine>, IEnumerable<Type>>) (ext => ext.GetFieldsDependOn())))
    {
      if (!typeof (IBqlField).IsAssignableFrom(c) || BqlCommand.GetItemType(c) != typeof (TLine))
        throw new PXArgumentException();
      PXGraph.FieldUpdatedEvents fieldUpdated = this.Base.FieldUpdated;
      Type type = typeof (TLine);
      string name = c.Name;
      CostCenterDispatcher<TGraph, TLine, TCostCenterField> centerDispatcher = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) centerDispatcher, __vmethodptr(centerDispatcher, FieldDependOnUpdated));
      fieldUpdated.AddHandler(type, name, pxFieldUpdated);
    }
  }

  protected virtual void SubscribeToRowInserting()
  {
    PXGraph.RowInsertingEvents rowInserting = this.Base.RowInserting;
    Type type = typeof (TLine);
    CostCenterDispatcher<TGraph, TLine, TCostCenterField> centerDispatcher = this;
    // ISSUE: virtual method pointer
    PXRowInserting pxRowInserting = new PXRowInserting((object) centerDispatcher, __vmethodptr(centerDispatcher, RowOnInserting));
    rowInserting.AddHandler(type, pxRowInserting);
  }

  protected virtual void FieldDependOnUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    TLine row = (TLine) e.Row;
    this.ResetCostCenter(cache, row);
  }

  protected virtual void RowOnInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    TLine row = (TLine) e.Row;
    int? nullable1 = (int?) cache.GetValue<TCostCenterField>((object) row);
    if (nullable1.HasValue)
    {
      int? nullable2 = nullable1;
      int num = 0;
      if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
        return;
    }
    this.ResetCostCenter(cache, row);
  }

  protected virtual void ResetCostCenter(PXCache cache, TLine row)
  {
    int costCenterIdByLine = this.GetCostCenterIDByLine(row);
    int? nullable = (int?) cache.GetValue<TCostCenterField>((object) row);
    int num = costCenterIdByLine;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return;
    cache.SetValueExt<TCostCenterField>((object) row, (object) costCenterIdByLine);
  }

  protected ICostCenterSupport<TLine> GetCostCenterSupportByLine(TLine line)
  {
    return this.Base.FindAllImplementations<ICostCenterSupport<TLine>>().OrderBy<ICostCenterSupport<TLine>, int>((Func<ICostCenterSupport<TLine>, int>) (e => e.SortOrder)).FirstOrDefault<ICostCenterSupport<TLine>>((Func<ICostCenterSupport<TLine>, bool>) (e => e.IsSpecificCostCenter(line)));
  }

  public int GetCostCenterIDByLine(TLine line)
  {
    ICostCenterSupport<TLine> centerSupportByLine = this.GetCostCenterSupportByLine(line);
    return centerSupportByLine == null ? 0 : centerSupportByLine.GetCostCenterID(line);
  }
}
