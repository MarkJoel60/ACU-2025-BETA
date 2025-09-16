// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectEntry_ChangeOrderExt_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.CS;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// Extensions that used to configure Workflow for <see cref="T:PX.Objects.PM.ProjectEntry" /> and <see cref="T:PX.Objects.PM.PMProject" />.
/// </summary>
public class ProjectEntry_ChangeOrderExt_Workflow : 
  PXGraphExtension<ProjectEntry_Workflow, ProjectEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.changeOrder>();

  public virtual void Configure(PXScreenConfiguration config)
  {
    ProjectEntry_ChangeOrderExt_Workflow.Configure(config.GetScreenConfigurationContext<ProjectEntry, PMProject>());
  }

  protected static void Configure(WorkflowContext<ProjectEntry, PMProject> context)
  {
    context.UpdateScreenConfigurationFor((Func<BoundedTo<ProjectEntry, PMProject>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<ProjectEntry, PMProject>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow((Func<BoundedTo<ProjectEntry, PMProject>.Workflow.ConfiguratorFlow, BoundedTo<ProjectEntry, PMProject>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<ProjectEntry, PMProject>.BaseFlowStep.ContainerAdjusterStates>) (fss =>
    {
      fss.Update<ProjectStatus.planned>((Func<BoundedTo<ProjectEntry, PMProject>.FlowState.ConfiguratorState, BoundedTo<ProjectEntry, PMProject>.FlowState.ConfiguratorState>) (flowState => flowState.WithActions((Action<BoundedTo<ProjectEntry, PMProject>.ActionState.ContainerAdjusterActions>) (actions => actions.Add<ProjectEntry_ChangeOrderExt>((Expression<Func<ProjectEntry_ChangeOrderExt, PXAction<PMProject>>>) (g => g.createChangeOrder), (Func<BoundedTo<ProjectEntry, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured>) null)))));
      fss.Update<ProjectStatus.active>((Func<BoundedTo<ProjectEntry, PMProject>.FlowState.ConfiguratorState, BoundedTo<ProjectEntry, PMProject>.FlowState.ConfiguratorState>) (flowState => flowState.WithActions((Action<BoundedTo<ProjectEntry, PMProject>.ActionState.ContainerAdjusterActions>) (actions => actions.Add<ProjectEntry_ChangeOrderExt>((Expression<Func<ProjectEntry_ChangeOrderExt, PXAction<PMProject>>>) (g => g.createChangeOrder), (Func<BoundedTo<ProjectEntry, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured>) null)))));
      fss.Update<ProjectStatus.pendingApproval>((Func<BoundedTo<ProjectEntry, PMProject>.FlowState.ConfiguratorState, BoundedTo<ProjectEntry, PMProject>.FlowState.ConfiguratorState>) (flowState => flowState.WithActions((Action<BoundedTo<ProjectEntry, PMProject>.ActionState.ContainerAdjusterActions>) (actions => actions.Add<ProjectEntry_ChangeOrderExt>((Expression<Func<ProjectEntry_ChangeOrderExt, PXAction<PMProject>>>) (g => g.createChangeOrder), (Func<BoundedTo<ProjectEntry, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured>) null)))));
    })))).WithActions((Action<BoundedTo<ProjectEntry, PMProject>.ActionDefinition.ContainerAdjusterActions>) (actions => actions.Add<ProjectEntry_ChangeOrderExt>((Expression<Func<ProjectEntry_ChangeOrderExt, PXAction<PMProject>>>) (g => g.createChangeOrder), (Func<BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IConfigured>) (c => (BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IConfigured) c.InFolder(context.Categories.Get("Change Management"))))))));
  }
}
