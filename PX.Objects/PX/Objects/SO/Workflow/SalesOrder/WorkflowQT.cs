// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Workflow.SalesOrder.WorkflowQT
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.Common;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.SO.Workflow.SalesOrder;

public class WorkflowQT : WorkflowBase
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    WorkflowQT.Configure(config.GetScreenConfigurationContext<SOOrderEntry, SOOrder>());
  }

  protected static void Configure(WorkflowContext<SOOrderEntry, SOOrder> context)
  {
    WorkflowBase.Conditions conditions = context.Conditions.GetPack<WorkflowBase.Conditions>();
    BoundedTo<SOOrderEntry, SOOrder>.ActionCategory.IConfigured processingCategory = CommonActionCategories.Get<SOOrderEntry, SOOrder>(context).Processing;
    context.UpdateScreenConfigurationFor((Func<BoundedTo<SOOrderEntry, SOOrder>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<SOOrderEntry, SOOrder>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.ContainerAdjusterActions>) (actions => actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.copyOrderQT), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IConfigured) c.WithCategory(processingCategory, (Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold)))))).WithFlows((Action<BoundedTo<SOOrderEntry, SOOrder>.Workflow.ContainerAdjusterFlows>) (flows => flows.Add<SOBehavior.qT>((Func<BoundedTo<SOOrderEntry, SOOrder>.Workflow.INeedStatesFlow, BoundedTo<SOOrderEntry, SOOrder>.Workflow.IConfigured>) (flow => (BoundedTo<SOOrderEntry, SOOrder>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IContainerFillerStates>) (flowStates =>
    {
      flowStates.Add("_", (Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (fs => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) fs.IsInitial((Expression<Func<SOOrderEntry, PXAutoAction<SOOrder>>>) (g => g.initializeState))));
      flowStates.Add<SOOrderStatus.hold>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.releaseFromHold), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printQuote), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.recalculateDiscountsAction), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.emailQuote), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))));
      flowStates.Add<SOOrderStatus.open>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.copyOrderQT), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printQuote), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.recalculateDiscountsAction), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.emailQuote), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))));
      flowStates.Add<SOOrderStatus.completed>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.copyOrderQT), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printQuote), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<SOOrderEntry, SOOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnOrderDeleted_ReopenQuote))))).WithFieldStates(new Action<BoundedTo<SOOrderEntry, SOOrder>.FieldState.IContainerFillerFields>(WorkflowBase.DisableWholeScreen))));
      flowStates.Add<SOOrderStatus.cancelled>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.copyOrderQT), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printQuote), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))).WithFieldStates(new Action<BoundedTo<SOOrderEntry, SOOrder>.FieldState.IContainerFillerFields>(WorkflowBase.DisableWholeScreen))));
    })).WithTransitions((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.hold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.initializeState)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.initializeState))));
      }));
      transitions.AddGroupFrom<SOOrderStatus.hold>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.releaseFromHold)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.hold>(new bool?(false))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.cancelled>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder))));
      }));
      transitions.AddGroupFrom<SOOrderStatus.open>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.hold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.hold>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.cancelled>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder))));
      }));
      transitions.AddGroupFrom<SOOrderStatus.cancelled>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.hold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsHoldEntryOrLSEntryEnabled).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.hold>((Func<BoundedTo<SOOrderEntry, SOOrder>.Assignment.INeedRightOperand, BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured>) (v => (BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured) v.SetFromValue((object) true)))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<SOOrder.inclCustOpenOrders>(new bool?(true));
          fas.Add<SOOrder.approved>((Func<BoundedTo<SOOrderEntry, SOOrder>.Assignment.INeedRightOperand, BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured>) (v => (BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured) v.SetFromValue((object) true)));
        }))));
      }));
      transitions.AddGroupFrom<SOOrderStatus.completed>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.completed>(new bool?(false))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnOrderDeleted_ReopenQuote)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.completed>(new bool?(false))))));
      }));
    }))))))));
  }
}
