// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TemplateMaint_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using System;
using System.Collections;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.PM;

public class TemplateMaint_Workflow : PXGraphExtension<TemplateMaint>
{
  public PXAction<PMProject> activate;
  public PXAction<PMProject> hold;

  public virtual void Configure(PXScreenConfiguration config)
  {
    TemplateMaint_Workflow.Configure(config.GetScreenConfigurationContext<TemplateMaint, PMProject>());
  }

  protected static void Configure(WorkflowContext<TemplateMaint, PMProject> context)
  {
    BoundedTo<TemplateMaint, PMProject>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<TemplateMaint, PMProject>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<TemplateMaint, PMProject>.ActionCategory.IConfigured>) (category => (BoundedTo<TemplateMaint, PMProject>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<TemplateMaint, PMProject>.ActionDefinition.IConfigured activate = context.ActionDefinitions.CreateExisting<TemplateMaint_Workflow>((Expression<Func<TemplateMaint_Workflow, PXAction<PMProject>>>) (g => g.activate), (Func<BoundedTo<TemplateMaint, PMProject>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TemplateMaint, PMProject>.ActionDefinition.IConfigured>) (a => (BoundedTo<TemplateMaint, PMProject>.ActionDefinition.IConfigured) a.InFolder(processingCategory).WithFieldAssignments((Action<BoundedTo<TemplateMaint, PMProject>.Assignment.IContainerFillerFields>) (fa =>
    {
      fa.Add<PMProject.isActive>((Func<BoundedTo<TemplateMaint, PMProject>.Assignment.INeedRightOperand, BoundedTo<TemplateMaint, PMProject>.Assignment.IConfigured>) (e => (BoundedTo<TemplateMaint, PMProject>.Assignment.IConfigured) e.SetFromValue((object) true)));
      fa.Add<PMProject.hold>((Func<BoundedTo<TemplateMaint, PMProject>.Assignment.INeedRightOperand, BoundedTo<TemplateMaint, PMProject>.Assignment.IConfigured>) (e => (BoundedTo<TemplateMaint, PMProject>.Assignment.IConfigured) e.SetFromValue((object) false)));
    }))));
    BoundedTo<TemplateMaint, PMProject>.ActionDefinition.IConfigured hold = context.ActionDefinitions.CreateExisting<TemplateMaint_Workflow>((Expression<Func<TemplateMaint_Workflow, PXAction<PMProject>>>) (g => g.hold), (Func<BoundedTo<TemplateMaint, PMProject>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TemplateMaint, PMProject>.ActionDefinition.IConfigured>) (a => (BoundedTo<TemplateMaint, PMProject>.ActionDefinition.IConfigured) a.InFolder(processingCategory).WithFieldAssignments((Action<BoundedTo<TemplateMaint, PMProject>.Assignment.IContainerFillerFields>) (fa =>
    {
      fa.Add<PMProject.isActive>((Func<BoundedTo<TemplateMaint, PMProject>.Assignment.INeedRightOperand, BoundedTo<TemplateMaint, PMProject>.Assignment.IConfigured>) (e => (BoundedTo<TemplateMaint, PMProject>.Assignment.IConfigured) e.SetFromValue((object) false)));
      fa.Add<PMProject.hold>((Func<BoundedTo<TemplateMaint, PMProject>.Assignment.INeedRightOperand, BoundedTo<TemplateMaint, PMProject>.Assignment.IConfigured>) (e => (BoundedTo<TemplateMaint, PMProject>.Assignment.IConfigured) e.SetFromValue((object) true)));
    }))));
    context.AddScreenConfigurationFor((Func<BoundedTo<TemplateMaint, PMProject>.ScreenConfiguration.IStartConfigScreen, BoundedTo<TemplateMaint, PMProject>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<TemplateMaint, PMProject>.ScreenConfiguration.IConfigured) ((BoundedTo<TemplateMaint, PMProject>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<PMProject.status>().AddDefaultFlow((Func<BoundedTo<TemplateMaint, PMProject>.Workflow.INeedStatesFlow, BoundedTo<TemplateMaint, PMProject>.Workflow.IConfigured>) (flow => (BoundedTo<TemplateMaint, PMProject>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<TemplateMaint, PMProject>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add<ProjectStatus.onHold>((Func<BoundedTo<TemplateMaint, PMProject>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TemplateMaint, PMProject>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<TemplateMaint, PMProject>.BaseFlowStep.IConfigured) ((BoundedTo<TemplateMaint, PMProject>.FlowState.INeedAnyFlowStateConfig) flowState.IsInitial()).WithActions((Action<BoundedTo<TemplateMaint, PMProject>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(activate, (Func<BoundedTo<TemplateMaint, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<TemplateMaint, PMProject>.ActionState.IConfigured>) (c => (BoundedTo<TemplateMaint, PMProject>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<TemplateMaint, PXAction<PMProject>>>) (g => g.copyTemplate), (Func<BoundedTo<TemplateMaint, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<TemplateMaint, PMProject>.ActionState.IConfigured>) (c => (BoundedTo<TemplateMaint, PMProject>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
      }))));
      fss.Add<ProjectStatus.active>((Func<BoundedTo<TemplateMaint, PMProject>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TemplateMaint, PMProject>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<TemplateMaint, PMProject>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<TemplateMaint, PMProject>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(hold, (Func<BoundedTo<TemplateMaint, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<TemplateMaint, PMProject>.ActionState.IConfigured>) (c => (BoundedTo<TemplateMaint, PMProject>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<TemplateMaint, PXAction<PMProject>>>) (g => g.copyTemplate), (Func<BoundedTo<TemplateMaint, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<TemplateMaint, PMProject>.ActionState.IConfigured>) (c => (BoundedTo<TemplateMaint, PMProject>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
      }))));
    })).WithTransitions((Action<BoundedTo<TemplateMaint, PMProject>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom<ProjectStatus.onHold>((Action<BoundedTo<TemplateMaint, PMProject>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<TemplateMaint, PMProject>.Transition.INeedTarget, BoundedTo<TemplateMaint, PMProject>.Transition.IConfigured>) (t => (BoundedTo<TemplateMaint, PMProject>.Transition.IConfigured) t.To<ProjectStatus.active>().IsTriggeredOn(activate)))));
      transitions.AddGroupFrom<ProjectStatus.active>((Action<BoundedTo<TemplateMaint, PMProject>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<TemplateMaint, PMProject>.Transition.INeedTarget, BoundedTo<TemplateMaint, PMProject>.Transition.IConfigured>) (t => (BoundedTo<TemplateMaint, PMProject>.Transition.IConfigured) t.To<ProjectStatus.onHold>().IsTriggeredOn(hold)))));
    })))).WithActions((Action<BoundedTo<TemplateMaint, PMProject>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<TemplateMaint, PXAction<PMProject>>>) (g => g.copyTemplate), (Func<BoundedTo<TemplateMaint, PMProject>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TemplateMaint, PMProject>.ActionDefinition.IConfigured>) null);
      actions.Add(activate);
      actions.Add(hold);
      actions.Add((Expression<Func<TemplateMaint, PXAction<PMProject>>>) (g => g.ChangeID), (Func<BoundedTo<TemplateMaint, PMProject>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TemplateMaint, PMProject>.ActionDefinition.IConfigured>) null);
    }))));
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Activate")]
  protected virtual IEnumerable Activate(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Hold")]
  protected virtual IEnumerable Hold(PXAdapter adapter) => adapter.Get();
}
