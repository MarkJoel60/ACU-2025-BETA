// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.CostProjectionEntry_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.PM;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting;

public class CostProjectionEntry_Workflow : PXGraphExtension<CostProjectionEntry>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    CostProjectionEntry_Workflow.Configure(config.GetScreenConfigurationContext<CostProjectionEntry, PMCostProjection>());
  }

  protected static void Configure(
    WorkflowContext<CostProjectionEntry, PMCostProjection> context)
  {
    BoundedTo<CostProjectionEntry, PMCostProjection>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionCategory.IConfigured>) (category => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<CostProjectionEntry, PMCostProjection>.ActionCategory.IConfigured otherCategory = context.Categories.CreateNew("OtherCategory", (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionCategory.IConfigured>) (category => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionCategory.IConfigured) category.DisplayName("Other")));
    context.AddScreenConfigurationFor((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ScreenConfiguration.IStartConfigScreen, BoundedTo<CostProjectionEntry, PMCostProjection>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<CostProjectionEntry, PMCostProjection>.ScreenConfiguration.IConfigured) ((BoundedTo<CostProjectionEntry, PMCostProjection>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<PMCostProjection.status>().AddDefaultFlow((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Workflow.INeedStatesFlow, BoundedTo<CostProjectionEntry, PMCostProjection>.Workflow.IConfigured>) (flow => (BoundedTo<CostProjectionEntry, PMCostProjection>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add<CostProjectionStatus.onHold>((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CostProjectionEntry, PMCostProjection>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CostProjectionEntry, PMCostProjection>.BaseFlowStep.IConfigured) ((BoundedTo<CostProjectionEntry, PMCostProjection>.FlowState.INeedAnyFlowStateConfig) flowState.IsInitial()).WithActions((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.refreshBudget), (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IAllowOptionalConfig, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured>) (c => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.createRevision), (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IAllowOptionalConfig, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured>) (c => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.removeHold), (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IAllowOptionalConfig, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured>) (c => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
      }))));
      fss.Add<CostProjectionStatus.open>((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CostProjectionEntry, PMCostProjection>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CostProjectionEntry, PMCostProjection>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.createRevision), (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IAllowOptionalConfig, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured>) (c => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.hold), (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IAllowOptionalConfig, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.release), (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IAllowOptionalConfig, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured>) (c => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
      }))));
      fss.Add<CostProjectionStatus.released>((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CostProjectionEntry, PMCostProjection>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CostProjectionEntry, PMCostProjection>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.createRevision), (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IAllowOptionalConfig, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured>) (c => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured) c.IsDuplicatedInToolbar()))))));
    })).WithTransitions((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom<CostProjectionStatus.onHold>((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.INeedTarget, BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.IConfigured>) (t => (BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.IConfigured) t.To<CostProjectionStatus.open>().IsTriggeredOn((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.removeHold))))));
      transitions.AddGroupFrom<CostProjectionStatus.open>((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.INeedTarget, BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.IConfigured>) (t => (BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.IConfigured) t.To<CostProjectionStatus.onHold>().IsTriggeredOn((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.hold))));
        ts.Add((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.INeedTarget, BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.IConfigured>) (t => (BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.IConfigured) t.To<CostProjectionStatus.released>().IsTriggeredOn((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.release))));
      }));
      transitions.AddGroupFrom<CostProjectionStatus.released>((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.ISourceContainerFillerTransitions>) (ts => { }));
    })))).WithActions((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.removeHold), (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IConfigured>) (c => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithFieldAssignments((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMCostProjection.hold>((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.INeedRightOperand, BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.IConfigured>) (f => (BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.hold), (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IConfigured>) (c => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithFieldAssignments((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMCostProjection.hold>((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.INeedRightOperand, BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.IConfigured>) (f => (BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.release), (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IConfigured>) (c => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
      actions.Add((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.refreshBudget), (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IConfigured>) (c => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IConfigured) c.InFolder(otherCategory)));
      actions.Add((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.createRevision), (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IConfigured>) (c => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IConfigured) c.InFolder(otherCategory)));
      actions.Add((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.costProjectionReport), (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IConfigured>) (c => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IConfigured) c.InFolder((FolderType) 2)));
    })).WithCategories((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(otherCategory);
      categories.Update((FolderType) 2, (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionCategory.ConfiguratorCategory, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(otherCategory)));
    }))));
  }
}
