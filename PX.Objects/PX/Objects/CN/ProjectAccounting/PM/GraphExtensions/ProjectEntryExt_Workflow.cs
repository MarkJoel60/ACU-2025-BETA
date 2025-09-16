// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.PM.GraphExtensions.ProjectEntryExt_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.PM.GraphExtensions;

public class ProjectEntryExt_Workflow : PXGraphExtension<ProjectEntry_Workflow, ProjectEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public virtual void Configure(PXScreenConfiguration config)
  {
    ProjectEntryExt_Workflow.Configure(config.GetScreenConfigurationContext<ProjectEntry, PMProject>());
  }

  protected static void Configure(WorkflowContext<ProjectEntry, PMProject> context)
  {
    context.UpdateScreenConfigurationFor((Func<BoundedTo<ProjectEntry, PMProject>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<ProjectEntry, PMProject>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((Action<BoundedTo<ProjectEntry, PMProject>.ActionDefinition.ContainerAdjusterActions>) (actions => actions.Add<ProjectEntryExt>((Expression<Func<ProjectEntryExt, PXAction<PMProject>>>) (g => g.costProjection), (Func<BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IConfigured>) (c => (BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IConfigured) c.WithCategory(context.Categories.Get("Budget Operations"))))))));
  }
}
