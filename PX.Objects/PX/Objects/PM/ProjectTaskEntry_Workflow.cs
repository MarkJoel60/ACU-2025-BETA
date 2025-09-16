// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectTaskEntry_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.PM;

public class ProjectTaskEntry_Workflow : PXGraphExtension<ProjectTaskEntry>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    ProjectTaskEntry_Workflow.Configure(config.GetScreenConfigurationContext<ProjectTaskEntry, PMTask>());
  }

  protected static void Configure(WorkflowContext<ProjectTaskEntry, PMTask> context)
  {
    BoundedTo<ProjectTaskEntry, PMTask>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<ProjectTaskEntry, PMTask>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ProjectTaskEntry, PMTask>.ActionCategory.IConfigured>) (category => (BoundedTo<ProjectTaskEntry, PMTask>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    context.AddScreenConfigurationFor((Func<BoundedTo<ProjectTaskEntry, PMTask>.ScreenConfiguration.IStartConfigScreen, BoundedTo<ProjectTaskEntry, PMTask>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<ProjectTaskEntry, PMTask>.ScreenConfiguration.IConfigured) ((BoundedTo<ProjectTaskEntry, PMTask>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<PMTask.status>().AddDefaultFlow((Func<BoundedTo<ProjectTaskEntry, PMTask>.Workflow.INeedStatesFlow, BoundedTo<ProjectTaskEntry, PMTask>.Workflow.IConfigured>) (flow => (BoundedTo<ProjectTaskEntry, PMTask>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<ProjectTaskEntry, PMTask>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add<ProjectTaskStatus.planned>((Func<BoundedTo<ProjectTaskEntry, PMTask>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ProjectTaskEntry, PMTask>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ProjectTaskEntry, PMTask>.BaseFlowStep.IConfigured) ((BoundedTo<ProjectTaskEntry, PMTask>.FlowState.INeedAnyFlowStateConfig) flowState.IsInitial()).WithActions((Action<BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ProjectTaskEntry, PXAction<PMTask>>>) (g => g.activate), (Func<BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IConfigured>) (c => (BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<ProjectTaskEntry, PXAction<PMTask>>>) (g => g.cancelTask), (Func<BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IConfigured>) (c => (BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
      }))));
      fss.Add<ProjectTaskStatus.active>((Func<BoundedTo<ProjectTaskEntry, PMTask>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ProjectTaskEntry, PMTask>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ProjectTaskEntry, PMTask>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ProjectTaskEntry, PXAction<PMTask>>>) (g => g.complete), (Func<BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IConfigured>) (c => (BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<ProjectTaskEntry, PXAction<PMTask>>>) (g => g.cancelTask), (Func<BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IConfigured>) (c => (BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<ProjectTaskEntry, PXAction<PMTask>>>) (g => g.hold), (Func<BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IConfigured>) null);
      }))));
      fss.Add<ProjectTaskStatus.completed>((Func<BoundedTo<ProjectTaskEntry, PMTask>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ProjectTaskEntry, PMTask>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ProjectTaskEntry, PMTask>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<ProjectTaskEntry, PXAction<PMTask>>>) (g => g.activate), (Func<BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IConfigured>) (c => (BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IConfigured) c.IsDuplicatedInToolbar()))))));
      fss.Add<ProjectTaskStatus.canceled>((Func<BoundedTo<ProjectTaskEntry, PMTask>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ProjectTaskEntry, PMTask>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ProjectTaskEntry, PMTask>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<ProjectTaskEntry, PXAction<PMTask>>>) (g => g.activate), (Func<BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IConfigured>) (c => (BoundedTo<ProjectTaskEntry, PMTask>.ActionState.IConfigured) c.IsDuplicatedInToolbar()))))));
    })).WithTransitions((Action<BoundedTo<ProjectTaskEntry, PMTask>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom<ProjectTaskStatus.planned>((Action<BoundedTo<ProjectTaskEntry, PMTask>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ProjectTaskEntry, PMTask>.Transition.INeedTarget, BoundedTo<ProjectTaskEntry, PMTask>.Transition.IConfigured>) (t => (BoundedTo<ProjectTaskEntry, PMTask>.Transition.IConfigured) t.To<ProjectTaskStatus.active>().IsTriggeredOn((Expression<Func<ProjectTaskEntry, PXAction<PMTask>>>) (g => g.activate))));
        ts.Add((Func<BoundedTo<ProjectTaskEntry, PMTask>.Transition.INeedTarget, BoundedTo<ProjectTaskEntry, PMTask>.Transition.IConfigured>) (t => (BoundedTo<ProjectTaskEntry, PMTask>.Transition.IConfigured) t.To<ProjectTaskStatus.canceled>().IsTriggeredOn((Expression<Func<ProjectTaskEntry, PXAction<PMTask>>>) (g => g.cancelTask))));
      }));
      transitions.AddGroupFrom<ProjectTaskStatus.active>((Action<BoundedTo<ProjectTaskEntry, PMTask>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ProjectTaskEntry, PMTask>.Transition.INeedTarget, BoundedTo<ProjectTaskEntry, PMTask>.Transition.IConfigured>) (t => (BoundedTo<ProjectTaskEntry, PMTask>.Transition.IConfigured) t.To<ProjectTaskStatus.completed>().IsTriggeredOn((Expression<Func<ProjectTaskEntry, PXAction<PMTask>>>) (g => g.complete))));
        ts.Add((Func<BoundedTo<ProjectTaskEntry, PMTask>.Transition.INeedTarget, BoundedTo<ProjectTaskEntry, PMTask>.Transition.IConfigured>) (t => (BoundedTo<ProjectTaskEntry, PMTask>.Transition.IConfigured) t.To<ProjectTaskStatus.canceled>().IsTriggeredOn((Expression<Func<ProjectTaskEntry, PXAction<PMTask>>>) (g => g.cancelTask))));
        ts.Add((Func<BoundedTo<ProjectTaskEntry, PMTask>.Transition.INeedTarget, BoundedTo<ProjectTaskEntry, PMTask>.Transition.IConfigured>) (t => (BoundedTo<ProjectTaskEntry, PMTask>.Transition.IConfigured) t.To<ProjectTaskStatus.planned>().IsTriggeredOn((Expression<Func<ProjectTaskEntry, PXAction<PMTask>>>) (g => g.hold))));
      }));
      transitions.AddGroupFrom<ProjectTaskStatus.completed>((Action<BoundedTo<ProjectTaskEntry, PMTask>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ProjectTaskEntry, PMTask>.Transition.INeedTarget, BoundedTo<ProjectTaskEntry, PMTask>.Transition.IConfigured>) (t => (BoundedTo<ProjectTaskEntry, PMTask>.Transition.IConfigured) t.To<ProjectTaskStatus.active>().IsTriggeredOn((Expression<Func<ProjectTaskEntry, PXAction<PMTask>>>) (g => g.activate))))));
      transitions.AddGroupFrom<ProjectTaskStatus.canceled>((Action<BoundedTo<ProjectTaskEntry, PMTask>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ProjectTaskEntry, PMTask>.Transition.INeedTarget, BoundedTo<ProjectTaskEntry, PMTask>.Transition.IConfigured>) (t => (BoundedTo<ProjectTaskEntry, PMTask>.Transition.IConfigured) t.To<ProjectTaskStatus.active>().IsTriggeredOn((Expression<Func<ProjectTaskEntry, PXAction<PMTask>>>) (g => g.activate))))));
    })))).WithActions((Action<BoundedTo<ProjectTaskEntry, PMTask>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<ProjectTaskEntry, PXAction<PMTask>>>) (g => g.activate), (Func<BoundedTo<ProjectTaskEntry, PMTask>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectTaskEntry, PMTask>.ActionDefinition.IConfigured>) (c => (BoundedTo<ProjectTaskEntry, PMTask>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).WithFieldAssignments((Action<BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IContainerFillerFields>) (fa =>
      {
        fa.Add<PMTask.isActive>((Func<BoundedTo<ProjectTaskEntry, PMTask>.Assignment.INeedRightOperand, BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured>) (e => (BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured) e.SetFromValue((object) true)));
        fa.Add<PMTask.isCompleted>((Func<BoundedTo<ProjectTaskEntry, PMTask>.Assignment.INeedRightOperand, BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured>) (e => (BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured) e.SetFromValue((object) false)));
        fa.Add<PMTask.isCancelled>((Func<BoundedTo<ProjectTaskEntry, PMTask>.Assignment.INeedRightOperand, BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured>) (e => (BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured) e.SetFromValue((object) false)));
      }))));
      actions.Add((Expression<Func<ProjectTaskEntry, PXAction<PMTask>>>) (g => g.hold), (Func<BoundedTo<ProjectTaskEntry, PMTask>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectTaskEntry, PMTask>.ActionDefinition.IConfigured>) (a => (BoundedTo<ProjectTaskEntry, PMTask>.ActionDefinition.IConfigured) a.WithCategory(processingCategory).WithFieldAssignments((Action<BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IContainerFillerFields>) (fa =>
      {
        fa.Add<PMTask.isActive>((Func<BoundedTo<ProjectTaskEntry, PMTask>.Assignment.INeedRightOperand, BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured>) (e => (BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured) e.SetFromValue((object) false)));
        fa.Add<PMTask.isCompleted>((Func<BoundedTo<ProjectTaskEntry, PMTask>.Assignment.INeedRightOperand, BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured>) (e => (BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured) e.SetFromValue((object) false)));
        fa.Add<PMTask.isCancelled>((Func<BoundedTo<ProjectTaskEntry, PMTask>.Assignment.INeedRightOperand, BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured>) (e => (BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured) e.SetFromValue((object) false)));
      }))));
      actions.Add((Expression<Func<ProjectTaskEntry, PXAction<PMTask>>>) (g => g.complete), (Func<BoundedTo<ProjectTaskEntry, PMTask>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectTaskEntry, PMTask>.ActionDefinition.IConfigured>) (a => (BoundedTo<ProjectTaskEntry, PMTask>.ActionDefinition.IConfigured) a.WithCategory(processingCategory).WithFieldAssignments((Action<BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IContainerFillerFields>) (fa =>
      {
        fa.Add<PMTask.isActive>((Func<BoundedTo<ProjectTaskEntry, PMTask>.Assignment.INeedRightOperand, BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured>) (e => (BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured) e.SetFromValue((object) true)));
        fa.Add<PMTask.isCompleted>((Func<BoundedTo<ProjectTaskEntry, PMTask>.Assignment.INeedRightOperand, BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured>) (e => (BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured) e.SetFromValue((object) true)));
        fa.Add<PMTask.isCancelled>((Func<BoundedTo<ProjectTaskEntry, PMTask>.Assignment.INeedRightOperand, BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured>) (e => (BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured) e.SetFromValue((object) false)));
      }))));
      actions.Add((Expression<Func<ProjectTaskEntry, PXAction<PMTask>>>) (g => g.cancelTask), (Func<BoundedTo<ProjectTaskEntry, PMTask>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectTaskEntry, PMTask>.ActionDefinition.IConfigured>) (a => (BoundedTo<ProjectTaskEntry, PMTask>.ActionDefinition.IConfigured) a.WithCategory(processingCategory).PlaceAfter((Expression<Func<ProjectTaskEntry, PXAction<PMTask>>>) (g => g.complete)).WithFieldAssignments((Action<BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IContainerFillerFields>) (fa =>
      {
        fa.Add<PMTask.isActive>((Func<BoundedTo<ProjectTaskEntry, PMTask>.Assignment.INeedRightOperand, BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured>) (e => (BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured) e.SetFromValue((object) false)));
        fa.Add<PMTask.isCompleted>((Func<BoundedTo<ProjectTaskEntry, PMTask>.Assignment.INeedRightOperand, BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured>) (e => (BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured) e.SetFromValue((object) false)));
        fa.Add<PMTask.isCancelled>((Func<BoundedTo<ProjectTaskEntry, PMTask>.Assignment.INeedRightOperand, BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured>) (e => (BoundedTo<ProjectTaskEntry, PMTask>.Assignment.IConfigured) e.SetFromValue((object) true)));
      }))));
    })).WithCategories((Action<BoundedTo<ProjectTaskEntry, PMTask>.ActionCategory.IContainerFillerCategories>) (categories => categories.Add(processingCategory)))));
  }
}
