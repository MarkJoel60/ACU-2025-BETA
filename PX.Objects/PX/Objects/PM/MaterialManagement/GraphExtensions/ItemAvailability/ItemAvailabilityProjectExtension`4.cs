// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.MaterialManagement.GraphExtensions.ItemAvailability.ItemAvailabilityProjectExtension`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.Abstraction;
using System;

#nullable disable
namespace PX.Objects.PM.MaterialManagement.GraphExtensions.ItemAvailability;

public abstract class ItemAvailabilityProjectExtension<TGraph, TItemAvailExt, TLine, TSplit> : 
  PXGraphExtension<TItemAvailExt, TGraph>
  where TGraph : PXGraph
  where TItemAvailExt : ItemAvailabilityExtension<TGraph, TLine, TSplit>
  where TLine : class, IBqlTable, ILSPrimary, new()
  where TSplit : class, IBqlTable, ILSDetail, new()
{
  private bool _projectAvailability;

  protected TItemAvailExt ItemAvailBase => this.Base1;

  protected virtual bool IsLinkedProject(int? projectID)
  {
    if (EnumerableExtensions.IsNotIn<int?>(projectID, new int?(), ProjectDefaultAttribute.NonProject()))
    {
      PMProject pmProject = PMProject.PK.Find((PXGraph) ((PXGraphExtension<TGraph>) this).Base, projectID);
      if (pmProject != null)
        return pmProject.AccountingMode == "L";
    }
    return false;
  }

  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3.GetStatus(`1)" />
  [PXOverride]
  public virtual string GetStatus(TLine line, Func<TLine, string> base_GetStatus)
  {
    return PXAccess.FeatureInstalled<FeaturesSet.materialManagement>() ? this.GetStatusProject(line) ?? base_GetStatus(line) : base_GetStatus(line);
  }

  protected abstract string GetStatusProject(TLine line);

  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3.Check(PX.Objects.IN.ILSMaster,System.Nullable{System.Int32})" />
  [PXOverride]
  public virtual void Check(ILSMaster row, int? costCenterID, Action<ILSMaster, int?> base_Check)
  {
    using (PXAccess.FeatureInstalled<FeaturesSet.materialManagement>() ? this.ProjectAvailabilityScope() : (IDisposable) null)
      base_Check(row, costCenterID);
  }

  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3.GetStatusLevel(PX.Objects.IN.IStatus)" />
  [PXOverride]
  public virtual ItemAvailabilityExtension<TGraph, TLine, TSplit>.StatusLevel GetStatusLevel(
    IStatus availability,
    Func<IStatus, ItemAvailabilityExtension<TGraph, TLine, TSplit>.StatusLevel> base_GetWarningLevel)
  {
    switch (availability)
    {
      case PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter _:
        return ItemAvailabilityExtension<TGraph, TLine, TSplit>.StatusLevel.LotSerial;
      case LocationStatusByCostCenter _:
        return ItemAvailabilityExtension<TGraph, TLine, TSplit>.StatusLevel.Location;
      case SiteStatusByCostCenter _:
        return ItemAvailabilityExtension<TGraph, TLine, TSplit>.StatusLevel.Site;
      default:
        return base_GetWarningLevel(availability);
    }
  }

  public IStatus FetchWithLineUOMProject(TLine line, bool excludeCurrent, int? costCenterID)
  {
    using (this.ProjectAvailabilityScope())
      return this.FetchWithLineUOM(line, excludeCurrent, costCenterID);
  }

  public virtual IStatus FetchWithBaseUOMProject(
    ILSMaster row,
    bool excludeCurrent,
    int? costCenterID)
  {
    using (this.ProjectAvailabilityScope())
      return this.FetchWithBaseUOM(row, excludeCurrent, costCenterID);
  }

  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3.FetchSite(PX.Objects.IN.ILSDetail,System.Boolean,System.Nullable{System.Int32})" />
  [PXOverride]
  public virtual IStatus FetchSite(
    ILSDetail split,
    bool excludeCurrent,
    int? costCenterID,
    Func<ILSDetail, bool, int?, IStatus> base_FetchSite)
  {
    return this.FetchProjectStatus(split, excludeCurrent, costCenterID, base_FetchSite);
  }

  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3.FetchLocation(PX.Objects.IN.ILSDetail,System.Boolean,System.Nullable{System.Int32})" />
  [PXOverride]
  public virtual IStatus FetchLocation(
    ILSDetail split,
    bool excludeCurrent,
    int? costCenterID,
    Func<ILSDetail, bool, int?, IStatus> base_FetchLocation)
  {
    return this.FetchProjectStatus(split, excludeCurrent, costCenterID, base_FetchLocation);
  }

  public virtual IStatus FetchProjectStatus(
    ILSDetail split,
    bool excludeCurrent,
    int? costCenterID,
    Func<ILSDetail, bool, int?, IStatus> base_FetchMethod)
  {
    if (!this._projectAvailability)
      return base_FetchMethod(split, excludeCurrent, costCenterID);
    int? nullable1 = split.ProjectID;
    if (!nullable1.HasValue)
    {
      TLine line = PXParentAttribute.SelectParent<TLine>((PXCache) this.SplitCache, (object) split);
      if ((object) line != null)
      {
        split.ProjectID = line.ProjectID;
        split.TaskID = line.TaskID;
      }
    }
    nullable1 = split.TaskID;
    if (nullable1.HasValue)
      return base_FetchMethod(split, excludeCurrent, costCenterID);
    nullable1 = split.ProjectID;
    int? nullable2 = ProjectDefaultAttribute.NonProject();
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      return base_FetchMethod(split, excludeCurrent, new int?(0));
    PMProject pmProject = PMProject.PK.Find((PXGraph) ((PXGraphExtension<TGraph>) this).Base, split.ProjectID);
    if (pmProject == null || !(pmProject.BaseType == "P") || !(pmProject.AccountingMode != "L"))
      return base_FetchMethod(split, excludeCurrent, costCenterID);
    PXSelect<INCostCenter, Where<INCostCenter.siteID, Equal<Required<INCostCenter.siteID>>, And<INCostCenter.projectID, Equal<Required<INCostCenter.projectID>>>>> pxSelect = new PXSelect<INCostCenter, Where<INCostCenter.siteID, Equal<Required<INCostCenter.siteID>>, And<INCostCenter.projectID, Equal<Required<INCostCenter.projectID>>>>>((PXGraph) ((PXGraphExtension<TGraph>) this).Base);
    IStatus it = (IStatus) null;
    object[] objArray = new object[2]
    {
      (object) ((ILSMaster) split).SiteID,
      (object) split.ProjectID
    };
    foreach (PXResult<INCostCenter> pxResult in ((PXSelectBase<INCostCenter>) pxSelect).Select(objArray))
    {
      INCostCenter inCostCenter = PXResult<INCostCenter>.op_Implicit(pxResult);
      if (it == null)
        it = base_FetchMethod(split, excludeCurrent, inCostCenter.CostCenterID);
      else
        it.Add<IStatus>(base_FetchMethod(split, excludeCurrent, inCostCenter.CostCenterID));
    }
    return it;
  }

  protected IDisposable ProjectAvailabilityScope()
  {
    return (IDisposable) new SimpleScope((Action) (() => this._projectAvailability = true), (Action) (() => this._projectAvailability = false));
  }

  /// Uses <see cref="P:PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3.LineCache" />
  [PXProtectedAccess(null)]
  protected abstract PXCache<TLine> LineCache { get; }

  /// Uses <see cref="P:PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3.SplitCache" />
  [PXProtectedAccess(null)]
  protected abstract PXCache<TSplit> SplitCache { get; }

  /// Uses <see cref="M:PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3.Fetch``1(PX.Objects.IN.ILSDetail,PX.Objects.IN.IStatus,PX.Objects.IN.IStatus,System.Boolean)" />
  [PXProtectedAccess(null)]
  protected abstract IStatus Fetch<TQtyAllocated>(
    ILSDetail split,
    IStatus allocated,
    IStatus existing,
    bool excludeCurrent)
    where TQtyAllocated : class, IQtyAllocated, IBqlTable, new();

  /// Uses <see cref="M:PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3.InitializeRecord``1(``0)" />
  [PXProtectedAccess(null)]
  protected abstract T InitializeRecord<T>(T row) where T : class, IBqlTable, new();

  /// Uses <see cref="M:PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3.FetchWithLineUOM(`1,System.Boolean,System.Nullable{System.Int32})" />
  [PXProtectedAccess(null)]
  protected abstract IStatus FetchWithLineUOM(TLine line, bool excludeCurrent, int? costCenterID);

  /// Uses <see cref="M:PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3.FetchWithBaseUOM(PX.Objects.IN.ILSMaster,System.Boolean,System.Nullable{System.Int32})" />
  [PXProtectedAccess(null)]
  protected abstract IStatus FetchWithBaseUOM(
    ILSMaster row,
    bool excludeCurrent,
    int? costCenterID);

  /// Uses <see cref="M:PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3.Check(PX.Objects.IN.ILSMaster,System.Nullable{System.Int32})" />
  [PXProtectedAccess(null)]
  protected abstract void Check(ILSMaster row, int? costCenterID);

  /// Uses <see cref="M:PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3.FormatQty(System.Nullable{System.Decimal})" />
  [PXProtectedAccess(null)]
  protected abstract string FormatQty(Decimal? value);
}
