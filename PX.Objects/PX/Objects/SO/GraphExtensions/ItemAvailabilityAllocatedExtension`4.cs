// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.ItemAvailabilityAllocatedExtension`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions;

public abstract class ItemAvailabilityAllocatedExtension<TGraph, TItemAvailExt, TLine, TSplit> : 
  PXGraphExtension<TItemAvailExt, TGraph>
  where TGraph : PXGraph
  where TItemAvailExt : ItemAvailabilityExtension<TGraph, TLine, TSplit>
  where TLine : class, IBqlTable, ILSPrimary, new()
  where TSplit : class, IBqlTable, ILSDetail, new()
{
  protected TItemAvailExt ItemAvailBase => this.Base1;

  protected abstract Type LineQtyAvail { get; }

  protected abstract Type LineQtyHardAvail { get; }

  public virtual bool IsAllocationEntryEnabled
  {
    get
    {
      PX.Objects.SO.SOOrderType soOrderType = PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(PXSetup<PX.Objects.SO.SOOrderType>.Select((PXGraph) ((PXGraphExtension<TGraph>) this).Base, Array.Empty<object>()));
      return soOrderType == null || soOrderType.RequireShipping.GetValueOrDefault() || soOrderType.Behavior == "BL";
    }
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3.GetStatus(`1)" />
  /// </summary>
  [PXOverride]
  public virtual string GetStatus(TLine line, Func<TLine, string> base_GetStatus)
  {
    return this.IsAllocationEntryEnabled ? this.GetStatusWithAllocated(line) : base_GetStatus(line);
  }

  protected abstract string GetStatusWithAllocated(TLine line);

  protected TLine LineToExcludeAllocated { get; private set; }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3.FetchWithLineUOM(`1,System.Boolean,System.Nullable{System.Int32})" />
  /// </summary>
  [PXOverride]
  public virtual IStatus FetchWithBaseUOM(
    ILSMaster row,
    bool excludeCurrent,
    int? costCenterID,
    Func<ILSMaster, bool, int?, IStatus> base_FetchWithBaseUOM)
  {
    try
    {
      if (row is TLine line)
        this.LineToExcludeAllocated = line;
      return base_FetchWithBaseUOM(row, excludeCurrent, costCenterID);
    }
    finally
    {
      this.LineToExcludeAllocated = default (TLine);
    }
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3.ExcludeCurrent(PX.Objects.IN.ILSDetail,PX.Objects.IN.IStatus,PX.Objects.IN.AvailabilitySigns)" />
  /// </summary>
  [PXOverride]
  public virtual void ExcludeCurrent(
    ILSDetail currentSplit,
    IStatus allocated,
    AvailabilitySigns signs,
    Action<ILSDetail, IStatus, AvailabilitySigns> base_ExcludeCurrent)
  {
    if ((object) this.LineToExcludeAllocated != null)
      this.ExcludeAllocated(this.LineToExcludeAllocated, allocated);
    else
      base_ExcludeCurrent(currentSplit, allocated, signs);
  }

  protected virtual IStatus ExcludeAllocated(TLine line, IStatus availability)
  {
    if (availability == null)
      return (IStatus) null;
    PXCache<TLine> pxCache1 = GraphHelper.Caches<TLine>((PXGraph) ((PXGraphExtension<TGraph>) this).Base);
    Decimal? lineQtyAvail = (Decimal?) ((PXCache) pxCache1).GetValue((object) line, this.LineQtyAvail.Name);
    Decimal? lineQtyHardAvail = (Decimal?) ((PXCache) pxCache1).GetValue((object) line, this.LineQtyHardAvail.Name);
    if (!lineQtyAvail.HasValue || !lineQtyHardAvail.HasValue)
    {
      PXCache<TSplit> pxCache2 = GraphHelper.Caches<TSplit>((PXGraph) ((PXGraphExtension<TGraph>) this).Base);
      lineQtyAvail = new Decimal?(0M);
      lineQtyHardAvail = new Decimal?(0M);
      foreach (TSplit split1 in this.GetSplits(line))
      {
        TSplit split2 = this.EnsurePlanType(split1);
        PXParentAttribute.SetParent((PXCache) pxCache2, (object) split2, typeof (TLine), (object) line);
        AvailabilitySigns availabilitySigns = this.ItemAvailBase.GetAvailabilitySigns<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>(split1);
        Decimal? nullable1;
        Decimal? nullable2;
        if (Sign.op_Inequality(availabilitySigns.SignQtyAvail, Sign.Zero))
        {
          nullable1 = lineQtyAvail;
          Sign signQtyAvail = availabilitySigns.SignQtyAvail;
          nullable2 = split2.BaseQty;
          Decimal valueOrDefault = nullable2.GetValueOrDefault();
          Decimal num = Sign.op_Multiply(signQtyAvail, valueOrDefault);
          Decimal? nullable3;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable3 = nullable2;
          }
          else
            nullable3 = new Decimal?(nullable1.GetValueOrDefault() - num);
          lineQtyAvail = nullable3;
        }
        if (Sign.op_Inequality(availabilitySigns.SignQtyHardAvail, Sign.Zero))
        {
          nullable1 = lineQtyHardAvail;
          Sign signQtyHardAvail = availabilitySigns.SignQtyHardAvail;
          nullable2 = split2.BaseQty;
          Decimal valueOrDefault = nullable2.GetValueOrDefault();
          Decimal num = Sign.op_Multiply(signQtyHardAvail, valueOrDefault);
          Decimal? nullable4;
          if (!nullable1.HasValue)
          {
            nullable2 = new Decimal?();
            nullable4 = nullable2;
          }
          else
            nullable4 = new Decimal?(nullable1.GetValueOrDefault() - num);
          lineQtyHardAvail = nullable4;
        }
      }
      ((PXCache) pxCache1).SetValue((object) line, this.LineQtyAvail.Name, (object) lineQtyAvail);
      ((PXCache) pxCache1).SetValue((object) line, this.LineQtyHardAvail.Name, (object) lineQtyHardAvail);
    }
    return this.CalculateAllocatedQuantity(availability, lineQtyAvail, lineQtyHardAvail);
  }

  protected virtual IStatus CalculateAllocatedQuantity(
    IStatus availability,
    Decimal? lineQtyAvail,
    Decimal? lineQtyHardAvail)
  {
    IStatus status1 = availability;
    Decimal? nullable1 = status1.QtyAvail;
    Decimal? nullable2 = lineQtyAvail;
    status1.QtyAvail = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    IStatus status2 = availability;
    Decimal? nullable3 = status2.QtyHardAvail;
    nullable1 = lineQtyHardAvail;
    status2.QtyHardAvail = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    IStatus status3 = availability;
    nullable1 = lineQtyAvail;
    Decimal? nullable4;
    if (!nullable1.HasValue)
    {
      nullable3 = new Decimal?();
      nullable4 = nullable3;
    }
    else
      nullable4 = new Decimal?(-nullable1.GetValueOrDefault());
    status3.QtyNotAvail = nullable4;
    return availability;
  }

  protected abstract TSplit EnsurePlanType(TSplit split);

  protected virtual INItemPlan GetItemPlan(long? planID)
  {
    return PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.planID, IBqlLong>.IsEqual<P.AsLong>>>.Config>.Select((PXGraph) ((PXGraphExtension<TGraph>) this).Base, new object[1]
    {
      (object) planID
    }));
  }

  protected abstract TSplit[] GetSplits(TLine line);

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3.Optimize" />
  /// </summary>
  [PXOverride]
  public virtual void Optimize(Action base_Optimize)
  {
    base_Optimize();
    if (!this.DocumentNoteID.HasValue)
      return;
    foreach (PXResult<INItemPlan> pxResult in PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.refNoteID, IBqlGuid>.IsEqual<P.AsGuid>>>.Config>.Select((PXGraph) ((PXGraphExtension<TGraph>) this).Base, new object[1]
    {
      (object) this.DocumentNoteID
    }))
      PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.planID, IBqlLong>.IsEqual<P.AsLong>>>.Config>.StoreResult((PXGraph) ((PXGraphExtension<TGraph>) this).Base, (IBqlTable) PXResult<INItemPlan>.op_Implicit(pxResult));
  }

  protected abstract Guid? DocumentNoteID { get; }
}
