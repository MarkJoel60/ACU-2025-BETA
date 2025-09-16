// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Workflow.SalesOrder.WorkflowMO
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.SO.Workflow.SalesOrder;

public class WorkflowMO : WorkflowBase
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    WorkflowMO.Configure(config.GetScreenConfigurationContext<SOOrderEntry, SOOrder>());
  }

  protected static void Configure(WorkflowContext<SOOrderEntry, SOOrder> context)
  {
    WorkflowIN.Conditions conditions = context.Conditions.GetPack<WorkflowIN.Conditions>();
    context.UpdateScreenConfigurationFor((Func<BoundedTo<SOOrderEntry, SOOrder>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<SOOrderEntry, SOOrder>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithFlows((Action<BoundedTo<SOOrderEntry, SOOrder>.Workflow.ContainerAdjusterFlows>) (flows => flows.Add<SOBehavior.mO>((Func<BoundedTo<SOOrderEntry, SOOrder>.Workflow.INeedStatesFlow, BoundedTo<SOOrderEntry, SOOrder>.Workflow.IConfigured>) (flow => (BoundedTo<SOOrderEntry, SOOrder>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IContainerFillerStates>) (flowStates =>
    {
      flowStates.Add("_", (Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (fs => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) fs.IsInitial((Expression<Func<SOOrderEntry, PXAutoAction<SOOrder>>>) (g => g.initializeState))));
      flowStates.Add<SOOrderStatus.hold>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.releaseFromHold), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.copyOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.recalculateDiscountsAction), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))));
      flowStates.Add<SOOrderStatus.creditHold>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.releaseFromCreditHold), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.copyOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<SOOrderEntry, SOOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnCreditLimitSatisfied))))));
      flowStates.Add<SOOrderStatus.open>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add<SOOrderEntry.SOQuickProcess>((Expression<Func<SOOrderEntry.SOQuickProcess, PXAction<SOOrder>>>) (g => g.quickProcess), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.prepareInvoice), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.copyOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.recalculateDiscountsAction), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.emailSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<SOOrderEntry, SOOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add<SOOrderShipment, SOInvoice>((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder, SOOrderShipment, SOInvoice>>>) (g => g.OnInvoiceLinked));
        handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnCreditLimitViolated));
      }))));
      flowStates.Add<SOOrderStatus.invoiced>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.copyOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<SOOrderEntry, SOOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add<SOInvoice>((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder, SOInvoice>>>) (g => g.OnInvoiceReleased));
        handlers.Add<SOOrderShipment, SOInvoice>((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder, SOOrderShipment, SOInvoice>>>) (g => g.OnInvoiceUnlinked));
      }))).WithFieldStates(new Action<BoundedTo<SOOrderEntry, SOOrder>.FieldState.IContainerFillerFields>(WorkflowBase.DisableWholeScreen))));
      flowStates.Add<SOOrderStatus.completed>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.copyOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))).WithFieldStates(new Action<BoundedTo<SOOrderEntry, SOOrder>.FieldState.IContainerFillerFields>(WorkflowBase.DisableWholeScreen))));
      flowStates.Add<SOOrderStatus.cancelled>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.copyOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))).WithFieldStates(new Action<BoundedTo<SOOrderEntry, SOOrder>.FieldState.IContainerFillerFields>(WorkflowBase.DisableWholeScreen))));
    })).WithTransitions((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.hold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.initializeState)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsOnHold).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.creditHold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.initializeState)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsOnCreditHold)));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.initializeState)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.inclCustOpenOrders>(new bool?(true))))));
      }));
      transitions.AddGroupFrom<SOOrderStatus.hold>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.releaseFromHold)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.inclCustOpenOrders>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.cancelled>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder))));
      }));
      transitions.AddGroupFrom<SOOrderStatus.creditHold>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.releaseFromCreditHold)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<SOOrder.creditHold>(new bool?(false));
          fas.Add<SOOrder.inclCustOpenOrders>(new bool?(true));
        }))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnCreditLimitSatisfied)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<SOOrder.creditHold>(new bool?(false));
          fas.Add<SOOrder.inclCustOpenOrders>(new bool?(true));
        }))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.hold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<SOOrder.creditHold>(new bool?(false));
          fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false));
        }))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.cancelled>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder))));
      }));
      transitions.AddGroupFrom<SOOrderStatus.open>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.hold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.invoiced>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnInvoiceLinked))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.cancelled>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.creditHold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnCreditLimitViolated)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<SOOrder.creditHold>(new bool?(true));
          fas.Add<SOOrder.hold>(new bool?(false));
          fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false));
        }))));
      }));
      transitions.AddGroupFrom<SOOrderStatus.invoiced>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.completed>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnInvoiceReleased)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.HasAllBillsReleased).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.completed>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnInvoiceUnlinked)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsUnbilled)));
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
    }))))))));
  }
}
