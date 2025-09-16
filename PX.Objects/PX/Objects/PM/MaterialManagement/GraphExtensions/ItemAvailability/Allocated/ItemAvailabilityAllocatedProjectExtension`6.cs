// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.MaterialManagement.GraphExtensions.ItemAvailability.Allocated.ItemAvailabilityAllocatedProjectExtension`6
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.SO.GraphExtensions;
using System;

#nullable disable
namespace PX.Objects.PM.MaterialManagement.GraphExtensions.ItemAvailability.Allocated;

public abstract class ItemAvailabilityAllocatedProjectExtension<TGraph, TItemAvailExt, TItemAvailAllocExt, TItemAvailProjExt, TLine, TSplit> : 
  PXGraphExtension<TItemAvailProjExt, TItemAvailAllocExt, TItemAvailExt, TGraph>
  where TGraph : PXGraph
  where TItemAvailExt : ItemAvailabilityExtension<TGraph, TLine, TSplit>
  where TItemAvailAllocExt : ItemAvailabilityAllocatedExtension<TGraph, TItemAvailExt, TLine, TSplit>
  where TItemAvailProjExt : ItemAvailabilityProjectExtension<TGraph, TItemAvailExt, TLine, TSplit>
  where TLine : class, IBqlTable, ILSPrimary, new()
  where TSplit : class, IBqlTable, ILSDetail, new()
{
  protected static bool UseProjectAvailability
  {
    get => PXAccess.FeatureInstalled<FeaturesSet.materialManagement>();
  }

  protected TItemAvailExt ItemAvailBase => ((PXGraphExtension<TItemAvailExt, TGraph>) this).Base1;

  protected TItemAvailAllocExt ItemAvailAllocBase
  {
    get => ((PXGraphExtension<TItemAvailAllocExt, TItemAvailExt, TGraph>) this).Base2;
  }

  protected TItemAvailProjExt ItemAvailProjExt => this.Base3;

  /// Overrides <see cref="M:PX.Objects.PM.MaterialManagement.GraphExtensions.ItemAvailability.ItemAvailabilityProjectExtension`4.GetStatusProject(`2)" />
  [PXOverride]
  public virtual string GetStatusProject(TLine line, Func<TLine, string> base_GetStatusProject)
  {
    return ((PXGraphExtension<TItemAvailAllocExt, TItemAvailExt, TGraph>) this).Base2.IsAllocationEntryEnabled ? this.GetStatusWithAllocatedProject(line) ?? base_GetStatusProject(line) : base_GetStatusProject(line);
  }

  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.ItemAvailabilityAllocatedExtension`4.GetStatusWithAllocated(`2)" />
  [PXOverride]
  public virtual string GetStatusWithAllocated(
    TLine line,
    Func<TLine, string> base_GetStatusWithAllocated)
  {
    return ItemAvailabilityAllocatedProjectExtension<TGraph, TItemAvailExt, TItemAvailAllocExt, TItemAvailProjExt, TLine, TSplit>.UseProjectAvailability ? this.GetStatusWithAllocatedProject(line) ?? base_GetStatusWithAllocated(line) : base_GetStatusWithAllocated(line);
  }

  protected abstract string GetStatusWithAllocatedProject(TLine line);
}
