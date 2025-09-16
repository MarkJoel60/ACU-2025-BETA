// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOOrderItemAvailabilityAllocatedExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

[PXProtectedAccess(typeof (SOOrderItemAvailabilityExtension))]
public abstract class SOOrderItemAvailabilityAllocatedExtension : 
  ItemAvailabilityAllocatedExtension<SOOrderEntry, SOOrderItemAvailabilityExtension, PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit>
{
  protected override string GetStatusWithAllocated(PX.Objects.SO.SOLine line)
  {
    string statusWithAllocated = string.Empty;
    IStatus availability = this.Base1.FetchWithLineUOM(line, line == null || !line.Completed.GetValueOrDefault(), line.CostCenterID);
    if (availability != null)
    {
      Decimal? allocated = new Decimal?(this.GetAllocatedQty(line));
      statusWithAllocated = this.FormatStatusAllocated(availability, allocated, line.UOM);
      this.Check((ILSMaster) line, availability);
    }
    return statusWithAllocated;
  }

  protected virtual Decimal GetAllocatedQty(PX.Objects.SO.SOLine line)
  {
    return PXDBQuantityAttribute.Round(new Decimal?(line.LineQtyHardAvail.GetValueOrDefault() * this.GetUnitRate(line)));
  }

  private string FormatStatusAllocated(IStatus availability, Decimal? allocated, string uom)
  {
    return PXMessages.LocalizeFormatNoPrefixNLA("On Hand {1} {0}, Available {2} {0}, Available for Shipping {3} {0}, Allocated {4} {0}", new object[5]
    {
      (object) uom,
      (object) this.FormatQty(availability.QtyOnHand),
      (object) this.FormatQty(availability.QtyAvail),
      (object) this.FormatQty(availability.QtyHardAvail),
      (object) this.FormatQty(allocated)
    });
  }

  protected override Type LineQtyAvail => typeof (PX.Objects.SO.SOLine.lineQtyAvail);

  protected override Type LineQtyHardAvail => typeof (PX.Objects.SO.SOLine.lineQtyHardAvail);

  protected override PX.Objects.SO.SOLineSplit[] GetSplits(PX.Objects.SO.SOLine line)
  {
    return ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).FindImplementation<SOOrderLineSplittingExtension>().GetSplits(line);
  }

  protected override PX.Objects.SO.SOLineSplit EnsurePlanType(PX.Objects.SO.SOLineSplit split)
  {
    if (split.PlanID.HasValue)
    {
      INItemPlan itemPlan = this.GetItemPlan(split.PlanID);
      if (itemPlan != null)
      {
        split = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(split);
        split.PlanType = itemPlan.PlanType;
      }
    }
    return split;
  }

  protected override Guid? DocumentNoteID
  {
    get
    {
      return ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current?.NoteID;
    }
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3.Fetch(PX.Objects.IN.ILSDetail,System.Boolean,System.Nullable{System.Int32})" />
  /// </summary>
  [PXOverride]
  public virtual IStatus Fetch(
    ILSDetail split,
    bool excludeCurrent,
    int? costCenterID,
    Func<ILSDetail, bool, int?, IStatus> base_Fetch)
  {
    return this.IsAllocationEntryEnabled && this.LineToExcludeAllocated != null ? this.ExcludeAllocated(this.LineToExcludeAllocated, base_Fetch(split, false, costCenterID)) : base_Fetch(split, excludeCurrent, costCenterID);
  }

  /// <summary>
  /// Intension of overriding ExcludeCurrent in here is to skip the condition check of LineToExcludeAllocated at
  /// <see cref="M:PX.Objects.SO.GraphExtensions.ItemAvailabilityAllocatedExtension`4.ExcludeCurrent(PX.Objects.IN.ILSDetail,PX.Objects.IN.IStatus,PX.Objects.IN.AvailabilitySigns,System.Action{PX.Objects.IN.ILSDetail,PX.Objects.IN.IStatus,PX.Objects.IN.AvailabilitySigns})" />
  /// and invoke the supper base method of <see cref="M:PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3.ExcludeCurrent(PX.Objects.IN.ILSDetail,PX.Objects.IN.IStatus,PX.Objects.IN.AvailabilitySigns)" />
  /// </summary>
  [PXOverride]
  public override void ExcludeCurrent(
    ILSDetail currentSplit,
    IStatus allocated,
    AvailabilitySigns signs,
    Action<ILSDetail, IStatus, AvailabilitySigns> base_ExcludeCurrent)
  {
    base_ExcludeCurrent(currentSplit, allocated, signs);
  }

  [PXProtectedAccess(null)]
  protected abstract string FormatQty(Decimal? value);

  [PXProtectedAccess(null)]
  protected abstract Decimal GetUnitRate(PX.Objects.SO.SOLine line);

  [PXProtectedAccess(null)]
  protected abstract void Check(ILSMaster row, IStatus availability);
}
