// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Workflow.SalesOrder.WorkflowBL
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.SO.GraphExtensions.SOOrderEntryExt;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.SO.Workflow.SalesOrder;

public class WorkflowBL : WorkflowBase
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    WorkflowBL.Configure(config.GetScreenConfigurationContext<SOOrderEntry, SOOrder>());
  }

  protected static void Configure(WorkflowContext<SOOrderEntry, SOOrder> context)
  {
    WorkflowBL.Conditions conditions = context.Conditions.GetPack<WorkflowBL.Conditions>();
    context.UpdateScreenConfigurationFor((Func<BoundedTo<SOOrderEntry, SOOrder>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<SOOrderEntry, SOOrder>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithFlows((Action<BoundedTo<SOOrderEntry, SOOrder>.Workflow.ContainerAdjusterFlows>) (flows => flows.Add<SOBehavior.bL>((Func<BoundedTo<SOOrderEntry, SOOrder>.Workflow.INeedStatesFlow, BoundedTo<SOOrderEntry, SOOrder>.Workflow.IConfigured>) (flow => (BoundedTo<SOOrderEntry, SOOrder>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IContainerFillerStates>) (flowStates =>
    {
      flowStates.Add("_", (Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (fs => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) fs.IsInitial((Expression<Func<SOOrderEntry, PXAutoAction<SOOrder>>>) (g => g.initializeState))));
      flowStates.Add<SOOrderStatus.hold>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.releaseFromHold), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<Blanket>((Expression<Func<Blanket, PXAction<SOOrder>>>) (g => g.printBlanket), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.recalculateDiscountsAction), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<SOOrderEntryExternalTax>((Expression<Func<SOOrderEntryExternalTax, PXAction<SOOrder>>>) (e => e.recalcExternalTax), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))));
      flowStates.Add<SOOrderStatus.open>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add<Blanket>((Expression<Func<Blanket, PXAction<SOOrder>>>) (g => g.printBlanket), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<Blanket>((Expression<Func<Blanket, PXAction<SOOrder>>>) (e => e.createChildOrders), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.prepareInvoice), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.completeOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<Blanket>((Expression<Func<Blanket, PXAction<SOOrder>>>) (e => e.processExpiredOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.createPurchaseOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<Blanket>((Expression<Func<Blanket, PXAction<SOOrder>>>) (g => g.emailBlanket), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.recalculateDiscountsAction), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<SOOrderEntryExternalTax>((Expression<Func<SOOrderEntryExternalTax, PXAction<SOOrder>>>) (e => e.recalcExternalTax), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<CreatePaymentExt>((Expression<Func<CreatePaymentExt, PXAction<SOOrder>>>) (e => e.createAndCapturePayment), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<CreatePaymentExt>((Expression<Func<CreatePaymentExt, PXAction<SOOrder>>>) (e => e.createAndAuthorizePayment), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<SOOrderEntry, SOOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnBlanketCompleted))))));
      flowStates.Add<SOOrderStatus.expired>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.completeOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<Blanket>((Expression<Func<Blanket, PXAction<SOOrder>>>) (g => g.printBlanket), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))).WithFieldStates((Action<BoundedTo<SOOrderEntry, SOOrder>.FieldState.IContainerFillerFields>) (states =>
      {
        states.AddAllFields<SOOrder>((Func<BoundedTo<SOOrderEntry, SOOrder>.FieldState.INeedAnyConfigField, BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured>) (state => (BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured) state.IsDisabled()));
        states.AddField<SOOrder.orderType>((Func<BoundedTo<SOOrderEntry, SOOrder>.FieldState.INeedAnyConfigField, BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured>) null);
        states.AddField<SOOrder.orderNbr>((Func<BoundedTo<SOOrderEntry, SOOrder>.FieldState.INeedAnyConfigField, BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured>) null);
        states.AddAllFields<SOLine>((Func<BoundedTo<SOOrderEntry, SOOrder>.FieldState.INeedAnyConfigField, BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured>) (state => (BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured) state.IsDisabled()));
        states.AddTable<SOLineSplit>((Func<BoundedTo<SOOrderEntry, SOOrder>.FieldState.INeedAnyConfigField, BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured>) (state => (BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured) state.IsDisabled()));
        states.AddField<SOLineSplit.isAllocated>((Func<BoundedTo<SOOrderEntry, SOOrder>.FieldState.INeedAnyConfigField, BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured>) null);
        states.AddTable<SOTaxTran>((Func<BoundedTo<SOOrderEntry, SOOrder>.FieldState.INeedAnyConfigField, BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured>) (state => (BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured) state.IsDisabled()));
        states.AddTable<SOBillingAddress>((Func<BoundedTo<SOOrderEntry, SOOrder>.FieldState.INeedAnyConfigField, BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured>) (state => (BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured) state.IsDisabled()));
        states.AddTable<SOBillingContact>((Func<BoundedTo<SOOrderEntry, SOOrder>.FieldState.INeedAnyConfigField, BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured>) (state => (BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured) state.IsDisabled()));
        states.AddTable<SOShippingAddress>((Func<BoundedTo<SOOrderEntry, SOOrder>.FieldState.INeedAnyConfigField, BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured>) (state => (BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured) state.IsDisabled()));
        states.AddTable<SOShippingContact>((Func<BoundedTo<SOOrderEntry, SOOrder>.FieldState.INeedAnyConfigField, BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured>) (state => (BoundedTo<SOOrderEntry, SOOrder>.FieldState.IConfigured) state.IsDisabled()));
      }))).WithEventHandlers((Action<BoundedTo<SOOrderEntry, SOOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnBlanketCompleted))))));
      flowStates.Add<SOOrderStatus.completed>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.prepareInvoice), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add<Blanket>((Expression<Func<Blanket, PXAction<SOOrder>>>) (g => g.printBlanket), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<Blanket>((Expression<Func<Blanket, PXAction<SOOrder>>>) (g => g.emailBlanket), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<SOOrderEntry, SOOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnBlanketReopened))))).WithFieldStates(new Action<BoundedTo<SOOrderEntry, SOOrder>.FieldState.IContainerFillerFields>(WorkflowBase.DisableWholeScreen))));
      flowStates.Add<SOOrderStatus.cancelled>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add<Blanket>((Expression<Func<Blanket, PXAction<SOOrder>>>) (g => g.printBlanket), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))).WithFieldStates(new Action<BoundedTo<SOOrderEntry, SOOrder>.FieldState.IContainerFillerFields>(WorkflowBase.DisableWholeScreen))));
    })).WithTransitions((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.hold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.initializeState)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.completed>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.initializeState)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsCompleted).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.completed>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.initializeState))));
      }));
      transitions.AddGroupFrom<SOOrderStatus.hold>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.expired>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.releaseFromHold)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsExpiredByDate).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.isExpired>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.releaseFromHold))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.cancelled>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder))));
      }));
      transitions.AddGroupFrom<SOOrderStatus.open>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.hold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.expired>().IsTriggeredOn<Blanket>((Expression<Func<Blanket, PXAction<SOOrder>>>) (e => e.processExpiredOrder)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsExpiredByDate).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.isExpired>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.completed>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnBlanketCompleted)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.completed>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.cancelled>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder))));
      }));
      transitions.AddGroupFrom<SOOrderStatus.expired>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.hold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.isExpired>(new bool?(false))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.completed>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.completeOrder)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.isExpired>(new bool?(false))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.completed>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnBlanketCompleted)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<SOOrder.isExpired>(new bool?(false));
          fas.Add<SOOrder.completed>(new bool?(true));
        }))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.cancelled>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.isExpired>(new bool?(false))))));
      }));
      transitions.AddGroupFrom<SOOrderStatus.completed>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.expired>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsExpiredByDate).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.isExpired>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.expired>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnBlanketReopened)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsExpiredByDate).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<SOOrder.isExpired>(new bool?(true));
          fas.Add<SOOrder.completed>(new bool?(false));
        }))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnBlanketReopened)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.completed>(new bool?(false))))));
      }));
      transitions.AddGroupFrom<SOOrderStatus.cancelled>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.expired>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsExpiredByDate).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<SOOrder.approved>(new bool?(true));
          fas.Add<SOOrder.isExpired>(new bool?(true));
        }))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.hold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsHoldEntryOrLSEntryEnabled).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.hold>((Func<BoundedTo<SOOrderEntry, SOOrder>.Assignment.INeedRightOperand, BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured>) (v => (BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured) v.SetFromValue((object) true)))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.approved>(new bool?(true))))));
      }));
    }))))))));
  }

  public new class Conditions : WorkflowBase.Conditions
  {
    public BoundedTo<SOOrderEntry, SOOrder>.Condition IsExpiredByDate
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOOrderEntry, SOOrder>.Condition.ConditionBuilder, BoundedTo<SOOrderEntry, SOOrder>.Condition>) (b => b.FromBql<BqlOperand<SOOrder.expireDate, IBqlDateTime>.IsLess<BqlField<AccessInfo.businessDate, IBqlDateTime>.FromCurrent>>()), nameof (IsExpiredByDate));
      }
    }
  }
}
