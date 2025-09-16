// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectCostProjectionByDateEntry_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.PM;

public class ProjectCostProjectionByDateEntry_Workflow : 
  PXGraphExtension<ProjectCostProjectionByDateEntry>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    ProjectCostProjectionByDateEntry_Workflow.Configure(config.GetScreenConfigurationContext<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>());
  }

  protected static void Configure(
    WorkflowContext<ProjectCostProjectionByDateEntry, PMCostProjectionByDate> context)
  {
    ProjectCostProjectionByDateEntry_Workflow.Conditions conditions = context.Conditions.GetPack<ProjectCostProjectionByDateEntry_Workflow.Conditions>();
    BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionCategory.IConfigured>) (category => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionCategory.IConfigured otherCategory = context.Categories.CreateNew("OtherCategory", (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionCategory.IConfigured>) (category => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionCategory.IConfigured) category.DisplayName("Other")));
    context.AddScreenConfigurationFor((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ScreenConfiguration.IStartConfigScreen, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ScreenConfiguration.IConfigured) ((BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<PMCostProjectionByDate.status>().AddDefaultFlow((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Workflow.INeedStatesFlow, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Workflow.IConfigured>) (flow => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add<ProjectCostProjectionByDateStatus.onHold>((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.BaseFlowStep.IConfigured) ((BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.FlowState.INeedAnyFlowStateConfig) flowState.IsInitial()).WithActions((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.removeHold), (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IConfigured>) (c => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.rebuildProjection), (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IConfigured>) (c => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.uploadProjection), (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IConfigured>) (c => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
      }))));
      fss.Add<ProjectCostProjectionByDateStatus.open>((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.hold), (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.release), (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IConfigured>) (c => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
      }))));
      fss.Add<ProjectCostProjectionByDateStatus.released>((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.hold), (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IConfigured>) null)))));
    })).WithTransitions((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom<ProjectCostProjectionByDateStatus.onHold>((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.INeedTarget, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IConfigured>) (t => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IConfigured) t.To<ProjectCostProjectionByDateStatus.open>().IsTriggeredOn((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.removeHold))))));
      transitions.AddGroupFrom<ProjectCostProjectionByDateStatus.open>((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.INeedTarget, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IConfigured>) (t => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IConfigured) t.To<ProjectCostProjectionByDateStatus.onHold>().IsTriggeredOn((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.hold)).WithFieldAssignments((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMCostProjectionByDate.hold>((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.INeedRightOperand, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured>) (f => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        ts.Add((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.INeedTarget, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IConfigured>) (t => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IConfigured) t.To<ProjectCostProjectionByDateStatus.released>().IsTriggeredOn((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.release)).WithFieldAssignments((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMCostProjectionByDate.released>((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.INeedRightOperand, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured>) (f => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      }));
      transitions.AddGroupFrom<ProjectCostProjectionByDateStatus.released>((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.INeedTarget, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IConfigured>) (t => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IConfigured) t.To<ProjectCostProjectionByDateStatus.onHold>().IsTriggeredOn((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.hold)).WithFieldAssignments((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IContainerFillerFields>) (fa =>
      {
        fa.Add<PMCostProjectionByDate.released>((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.INeedRightOperand, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured>) (f => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fa.Add<PMCostProjectionByDate.hold>((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.INeedRightOperand, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured>) (f => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured) f.SetFromValue((object) true)));
      }))))));
    })))).WithActions((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.rebuildProjection), (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IConfigured>) (c => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IConfigured) c.WithCategory(otherCategory)));
      actions.Add((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.uploadProjection), (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IConfigured>) (c => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IConfigured) c.WithCategory(otherCategory)));
      actions.Add((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.removeHold), (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IConfigured>) (c => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).WithFieldAssignments((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMCostProjectionByDate.hold>((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.INeedRightOperand, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured>) (f => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.hold), (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IConfigured>) (c => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).IsHiddenWhen((BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ISharedCondition) conditions.CantBeHold).IsDisabledWhen((BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ISharedCondition) conditions.CantBeHold)));
      actions.Add((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.release), (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IConfigured>) (c => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IConfigured) c.WithCategory(processingCategory)));
    })).WithCategories((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(otherCategory);
    }))));
  }

  public class Conditions : 
    BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Condition.Pack
  {
    public BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Condition CantBeHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Condition.ConditionBuilder, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostProjectionByDate.released, Equal<True>>>>>.And<BqlOperand<PMCostProjectionByDate.updateProjectBudget, IBqlBool>.IsEqual<True>>>()), nameof (CantBeHold));
      }
    }
  }
}
