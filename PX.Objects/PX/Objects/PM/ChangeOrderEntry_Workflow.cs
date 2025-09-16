// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ChangeOrderEntry_Workflow
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

public class ChangeOrderEntry_Workflow : PXGraphExtension<ChangeOrderEntry>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    ChangeOrderEntry_Workflow.Configure(config.GetScreenConfigurationContext<ChangeOrderEntry, PMChangeOrder>());
  }

  protected static void Configure(
    WorkflowContext<ChangeOrderEntry, PMChangeOrder> context)
  {
    BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionCategory.IConfigured>) (category => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionCategory.IConfigured correctionsCategory = context.Categories.CreateNew("Corrections", (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionCategory.IConfigured>) (category => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionCategory.IConfigured) category.DisplayName("Corrections")));
    BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionCategory.IConfigured printingAndEmailingCategory = context.Categories.CreateNew("Printing and Emailing", (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionCategory.IConfigured>) (category => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionCategory.IConfigured) category.DisplayName("Printing and Emailing")));
    context.AddScreenConfigurationFor((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ScreenConfiguration.IStartConfigScreen, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.ScreenConfiguration.IConfigured) ((BoundedTo<ChangeOrderEntry, PMChangeOrder>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<PMCostProjection.status>().AddDefaultFlow((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Workflow.INeedStatesFlow, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Workflow.IConfigured>) (flow => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add<ChangeOrderStatus.onHold>((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.BaseFlowStep.IConfigured) ((BoundedTo<ChangeOrderEntry, PMChangeOrder>.FlowState.INeedAnyFlowStateConfig) flowState.IsInitial()).WithActions((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.removeHold), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured>) (c => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.send), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.coCancel), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured>) null);
      }))));
      fss.Add<ChangeOrderStatus.open>((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.release), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured>) (c => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.hold), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.send), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.coCancel), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured>) null);
      }))));
      fss.Add<ChangeOrderStatus.closed>((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.send), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.reverse), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured>) null);
      }))));
      fss.Add<ChangeOrderStatus.canceled>((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.hold), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.send), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured>) null);
      }))));
    })).WithTransitions((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom<ChangeOrderStatus.onHold>((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.INeedTarget, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured>) (t => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured) t.To<ChangeOrderStatus.open>().IsTriggeredOn((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.removeHold))));
        ts.Add((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.INeedTarget, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured>) (t => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured) t.To<ChangeOrderStatus.canceled>().IsTriggeredOn((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.coCancel))));
      }));
      transitions.AddGroupFrom<ChangeOrderStatus.open>((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.INeedTarget, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured>) (t => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured) t.To<ChangeOrderStatus.onHold>().IsTriggeredOn((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.hold))));
        ts.Add((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.INeedTarget, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured>) (t => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured) t.To<ChangeOrderStatus.closed>().IsTriggeredOn((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.release)).WithFieldAssignments((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMChangeOrder.released>((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.INeedRightOperand, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.IConfigured>) (f => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        ts.Add((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.INeedTarget, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured>) (t => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured) t.To<ChangeOrderStatus.canceled>().IsTriggeredOn((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.coCancel))));
      }));
      transitions.AddGroupFrom<ChangeOrderStatus.canceled>((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.INeedTarget, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured>) (t => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured) t.To<ChangeOrderStatus.onHold>().IsTriggeredOn((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.hold))))));
    })))).WithActions((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.removeHold), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithFieldAssignments((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMChangeOrder.hold>((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.INeedRightOperand, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.IConfigured>) (f => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.hold), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithFieldAssignments((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMChangeOrder.hold>((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.INeedRightOperand, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.IConfigured>) (f => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.release), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
      actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.coCancel), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
      actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.send), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured) c.InFolder(printingAndEmailingCategory)));
      actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.reverse), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured) c.InFolder(correctionsCategory)));
      actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.coReport), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured) c.InFolder(printingAndEmailingCategory)));
      if (!PXAccess.FeatureInstalled<FeaturesSet.changeRequest>())
        return;
      actions.AddNew("ChangeRequests", (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured>) (config => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured) config.IsSidePanelScreen((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<ChangeOrderEntry, PMChangeOrder>.NavigationDefinition.IConfiguredSidePanel>) (sidePanelAction => sidePanelAction.NavigateToScreen("PM3085PL").WithIcon("event").WithAssignments((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.NavigationParameter.IContainerFillerNavigationActionParameters>) (containerFiller => containerFiller.Add("PMChangeRequest_changeOrderNbr", (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.NavigationParameter.INeedRightOperand, BoundedTo<ChangeOrderEntry, PMChangeOrder>.NavigationParameter.IConfigured>) (c => c.SetFromField<PMChangeOrder.refNbr>())))))).DisplayName("Change Requests")));
    })).WithCategories((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(correctionsCategory);
      categories.Add(printingAndEmailingCategory);
    }))));
  }
}
