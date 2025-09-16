// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.PM.GraphExtensions.ProjectOverviewExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Graphs;
using PX.Objects.CS;
using PX.Objects.PM;
using System.Collections;

#nullable disable
namespace PX.Objects.CN.Compliance.PM.GraphExtensions;

public class ProjectOverviewExt : ProjectEntryBaseExt<ProjectOverview>
{
  public PXAction<PMProject> openCompliance;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable OpenCompliance(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToView((PXGraph) PXGraph.CreateInstance<ComplianceDocumentEntry>(), "Documents", "", DataViewHelper.DataViewFilter.Create("projectID", (PXCondition) 0, (object) ((PXSelectBase<PMProject>) ((PXGraphExtension<ProjectOverview>) this).Base.Project).Current.ContractCD));
    return adapter.Get();
  }

  protected override void _(Events.RowSelected<ComplianceDocument> e)
  {
    base._(e);
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<ComplianceDocument>>) e).Cache.AllowInsert = false;
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<ComplianceDocument>>) e).Cache.AllowUpdate = false;
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<ComplianceDocument>>) e).Cache.AllowDelete = false;
  }
}
