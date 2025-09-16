// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Workflow.SalesOrder.WorkflowSO
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.SO.GraphExtensions.SOOrderEntryExt;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.SO.Workflow.SalesOrder;

public class WorkflowSO : WorkflowBase
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    WorkflowSO.Configure(config.GetScreenConfigurationContext<SOOrderEntry, SOOrder>());
  }

  protected static void Configure(WorkflowContext<SOOrderEntry, SOOrder> context)
  {
    WorkflowSO.Conditions conditions = context.Conditions.GetPack<WorkflowSO.Conditions>();
    context.UpdateScreenConfigurationFor((Func<BoundedTo<SOOrderEntry, SOOrder>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<SOOrderEntry, SOOrder>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithFlows((Action<BoundedTo<SOOrderEntry, SOOrder>.Workflow.ContainerAdjusterFlows>) (flows => flows.Add<SOBehavior.sO>((Func<BoundedTo<SOOrderEntry, SOOrder>.Workflow.INeedStatesFlow, BoundedTo<SOOrderEntry, SOOrder>.Workflow.IConfigured>) (flow => (BoundedTo<SOOrderEntry, SOOrder>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IContainerFillerStates>) (flowStates =>
    {
      flowStates.Add("_", (Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (fs => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) fs.IsInitial((Expression<Func<SOOrderEntry, PXAutoAction<SOOrder>>>) (g => g.initializeState))));
      flowStates.Add<SOOrderStatus.hold>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.releaseFromHold), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.copyOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.recalculateDiscountsAction), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<SOOrderEntryExternalTax>((Expression<Func<SOOrderEntryExternalTax, PXAction<SOOrder>>>) (e => e.recalcExternalTax), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<SOOrchestration>((Expression<Func<SOOrchestration, PXAction<SOOrder>>>) (g => g.OrchestrateOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<SOOrderEntry, SOOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnShipmentCorrected))))));
      flowStates.Add<SOOrderStatus.creditHold>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.releaseFromCreditHold), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.emailSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.copyOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<CreatePaymentExt>((Expression<Func<CreatePaymentExt, PXAction<SOOrder>>>) (e => e.createAndCapturePayment), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<CreatePaymentExt>((Expression<Func<CreatePaymentExt, PXAction<SOOrder>>>) (e => e.createAndAuthorizePayment), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<SOOrderEntry, SOOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnPaymentRequirementsViolated));
        handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnObtainedPaymentInPendingProcessing));
        handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnCreditLimitSatisfied));
      }))));
      flowStates.Add<SOOrderStatus.pendingProcessing>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.emailSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.copyOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<CreatePaymentExt>((Expression<Func<CreatePaymentExt, PXAction<SOOrder>>>) (e => e.createAndCapturePayment), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<CreatePaymentExt>((Expression<Func<CreatePaymentExt, PXAction<SOOrder>>>) (e => e.createAndAuthorizePayment), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<SOOrderEntry, SOOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnLostLastPaymentInPendingProcessing));
        handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnCreditLimitViolated));
      }))));
      flowStates.Add<SOOrderStatus.awaitingPayment>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.emailSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.copyOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<CreatePaymentExt>((Expression<Func<CreatePaymentExt, PXAction<SOOrder>>>) (e => e.createAndCapturePayment), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<CreatePaymentExt>((Expression<Func<CreatePaymentExt, PXAction<SOOrder>>>) (e => e.createAndAuthorizePayment), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<SOOrderEntry, SOOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnPaymentRequirementsSatisfied));
        handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnObtainedPaymentInPendingProcessing));
      }))));
      flowStates.Add<SOOrderStatus.open>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add<SOOrderEntry.SOQuickProcess>((Expression<Func<SOOrderEntry.SOQuickProcess, PXAction<SOOrder>>>) (g => g.quickProcess), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.WithConnotation((ActionConnotation) 3).IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.createShipmentIssue), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.prepareInvoice), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.completeOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.placeOnBackOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.createPurchaseOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.createTransferOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.emailSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.copyOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.recalculateDiscountsAction), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<SOOrderEntryExternalTax>((Expression<Func<SOOrderEntryExternalTax, PXAction<SOOrder>>>) (e => e.recalcExternalTax), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<CreatePaymentExt>((Expression<Func<CreatePaymentExt, PXAction<SOOrder>>>) (e => e.createAndCapturePayment), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<CreatePaymentExt>((Expression<Func<CreatePaymentExt, PXAction<SOOrder>>>) (e => e.createAndAuthorizePayment), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<SOOrchestration>((Expression<Func<SOOrchestration, PXAction<SOOrder>>>) (g => g.OrchestrateOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<SOOrderEntry, SOOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add<SOOrderShipment, SOShipment>((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder, SOOrderShipment, SOShipment>>>) (g => g.OnShipmentLinked));
        handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnShipmentCorrected));
        handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnPaymentRequirementsViolated));
        handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnObtainedPaymentInPendingProcessing));
        handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnCreditLimitViolated));
        handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnShipmentCreationFailed));
      }))));
      flowStates.Add<SOOrderStatus.shipping>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.createShipmentIssue), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.emailSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.createPurchaseOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<CreatePaymentExt>((Expression<Func<CreatePaymentExt, PXAction<SOOrder>>>) (e => e.createAndCapturePayment), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<CreatePaymentExt>((Expression<Func<CreatePaymentExt, PXAction<SOOrder>>>) (e => e.createAndAuthorizePayment), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<SOOrderEntry, SOOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add<SOOrderShipment, SOShipment>((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder, SOOrderShipment, SOShipment>>>) (g => g.OnShipmentUnlinked));
        handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnShipmentConfirmed));
      }))).WithFieldStates(new Action<BoundedTo<SOOrderEntry, SOOrder>.FieldState.IContainerFillerFields>(WorkflowBase.DisableWholeScreen))));
      flowStates.Add<SOOrderStatus.backOrder>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.openOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.createShipmentIssue), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add<SOOrderEntry.SOQuickProcess>((Expression<Func<SOOrderEntry.SOQuickProcess, PXAction<SOOrder>>>) (g => g.quickProcess), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.completeOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.prepareInvoice), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.emailSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.copyOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.createPurchaseOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.createTransferOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<CreatePaymentExt>((Expression<Func<CreatePaymentExt, PXAction<SOOrder>>>) (e => e.createAndCapturePayment), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<CreatePaymentExt>((Expression<Func<CreatePaymentExt, PXAction<SOOrder>>>) (e => e.createAndAuthorizePayment), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add<SOOrchestration>((Expression<Func<SOOrchestration, PXAction<SOOrder>>>) (g => g.OrchestrateOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<SOOrderEntry, SOOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add<SOOrderShipment, SOShipment>((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder, SOOrderShipment, SOShipment>>>) (g => g.OnShipmentLinked));
        handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnShipmentCorrected));
        handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnPaymentRequirementsViolated));
        handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnObtainedPaymentInPendingProcessing));
        handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnCreditLimitViolated));
      }))));
      flowStates.Add<SOOrderStatus.completed>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.prepareInvoice), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.copyOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<SOOrderEntry, SOOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<SOOrderEntry, PXWorkflowEventHandler<SOOrder>>>) (g => g.OnShipmentCorrected))))).WithFieldStates(new Action<BoundedTo<SOOrderEntry, SOOrder>.FieldState.IContainerFillerFields>(WorkflowBase.DisableWholeScreen))));
      flowStates.Add<SOOrderStatus.cancelled>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.copyOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
      }))).WithFieldStates(new Action<BoundedTo<SOOrderEntry, SOOrder>.FieldState.IContainerFillerFields>(WorkflowBase.DisableWholeScreen))));
    })).WithTransitions((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.hold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.initializeState)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsOnHold).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.pendingProcessing>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.initializeState)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.HasPaymentsInPendingProcessing).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.awaitingPayment>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.initializeState)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsPaymentRequirementsViolated).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.creditHold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.initializeState)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsOnCreditHold).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<SOOrder.hold>(new bool?(false));
          fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false));
        }))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.completed>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.initializeState)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsCompleted).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.completed>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.initializeState)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.inclCustOpenOrders>(new bool?(true))))));
      }));
      transitions.AddGroupFrom<SOOrderStatus.hold>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.pendingProcessing>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.releaseFromHold)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.HasPaymentsInPendingProcessing)));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.awaitingPayment>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.releaseFromHold)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsPaymentRequirementsViolated)));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.shipping>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnShipmentCorrected)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.HasOpenShipments)));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.releaseFromHold)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.inclCustOpenOrders>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.cancelled>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsCancelled)));
      }));
      transitions.AddGroupFrom<SOOrderStatus.creditHold>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.hold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.creditHold>(new bool?(false))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.pendingProcessing>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnObtainedPaymentInPendingProcessing)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.creditHold>(new bool?(false))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.awaitingPayment>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnPaymentRequirementsViolated)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.creditHold>(new bool?(false))))));
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
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.cancelled>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsCancelled)));
      }));
      transitions.AddGroupFrom<SOOrderStatus.pendingProcessing>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.hold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.awaitingPayment>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnLostLastPaymentInPendingProcessing)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsPaymentRequirementsViolated)));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnLostLastPaymentInPendingProcessing)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.inclCustOpenOrders>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.cancelled>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsCancelled)));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.creditHold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnCreditLimitViolated)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<SOOrder.creditHold>((Func<BoundedTo<SOOrderEntry, SOOrder>.Assignment.INeedRightOperand, BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured>) (e => (BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured) e.SetFromValue((object) true)));
          fas.Add<SOOrder.hold>((Func<BoundedTo<SOOrderEntry, SOOrder>.Assignment.INeedRightOperand, BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured>) (e => (BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured) e.SetFromValue((object) false)));
          fas.Add<SOOrder.inclCustOpenOrders>((Func<BoundedTo<SOOrderEntry, SOOrder>.Assignment.INeedRightOperand, BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured>) (e => (BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured) e.SetFromValue((object) false)));
        }))));
      }));
      transitions.AddGroupFrom<SOOrderStatus.awaitingPayment>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.hold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.pendingProcessing>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnObtainedPaymentInPendingProcessing))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnPaymentRequirementsSatisfied)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.inclCustOpenOrders>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.cancelled>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsCancelled)));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.creditHold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnCreditLimitViolated)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<SOOrder.creditHold>((Func<BoundedTo<SOOrderEntry, SOOrder>.Assignment.INeedRightOperand, BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured>) (e => (BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured) e.SetFromValue((object) true)));
          fas.Add<SOOrder.hold>((Func<BoundedTo<SOOrderEntry, SOOrder>.Assignment.INeedRightOperand, BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured>) (e => (BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured) e.SetFromValue((object) false)));
          fas.Add<SOOrder.inclCustOpenOrders>((Func<BoundedTo<SOOrderEntry, SOOrder>.Assignment.INeedRightOperand, BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured>) (e => (BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured) e.SetFromValue((object) false)));
        }))));
      }));
      transitions.AddGroupFrom<SOOrderStatus.open>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.hold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.cancelled>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsCancelled)));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.shipping>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnShipmentLinked)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.HasOpenShipments)));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.shipping>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnShipmentCorrected)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.HasOpenShipments)));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.backOrder>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.placeOnBackOrder)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.backOrdered>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.backOrder>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnShipmentCreationFailed)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.backOrdered>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.pendingProcessing>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnObtainedPaymentInPendingProcessing)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.awaitingPayment>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnPaymentRequirementsViolated)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.creditHold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnCreditLimitViolated)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<SOOrder.creditHold>(new bool?(true));
          fas.Add<SOOrder.hold>(new bool?(false));
          fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false));
        }))));
      }));
      transitions.AddGroupFrom<SOOrderStatus.shipping>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.pendingProcessing>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnShipmentUnlinked)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.HasPaymentsInPendingProcessing).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.awaitingPayment>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnShipmentUnlinked)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsPaymentRequirementsViolated).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnShipmentUnlinked)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsShippable).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.inclCustOpenOrders>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.completed>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnShipmentConfirmed)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsShippingCompleted).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.completed>(new bool?(true))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.pendingProcessing>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnShipmentConfirmed)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.HasPaymentsInPendingProcessing).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.awaitingPayment>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnShipmentConfirmed)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsPaymentRequirementsViolated).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.backOrder>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnShipmentConfirmed)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsShippable).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.backOrdered>(new bool?(true))))));
      }));
      transitions.AddGroupFrom<SOOrderStatus.backOrder>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.hold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<SOOrder.backOrdered>(new bool?(false));
          fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false));
        }))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.pendingProcessing>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnObtainedPaymentInPendingProcessing)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<SOOrder.backOrdered>(new bool?(false));
          fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false));
        }))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.awaitingPayment>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnPaymentRequirementsViolated)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<SOOrder.backOrdered>(new bool?(false));
          fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false));
        }))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.creditHold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnCreditLimitViolated)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<SOOrder.creditHold>(new bool?(true));
          fas.Add<SOOrder.backOrdered>(new bool?(false));
          fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false));
        }))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.openOrder)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<SOOrder.backOrdered>(new bool?(false));
          fas.Add<SOOrder.inclCustOpenOrders>(new bool?(true));
        }))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.shipping>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnShipmentLinked)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.HasOpenShipments)));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.shipping>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnShipmentCorrected)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.HasOpenShipments)));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.cancelled>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.cancelOrder)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsCancelled)));
      }));
      transitions.AddGroupFrom<SOOrderStatus.completed>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.shipping>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnShipmentCorrected)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.HasOpenShipments)));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.pendingProcessing>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.HasPaymentsInPendingProcessing).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.awaitingPayment>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsPaymentRequirementsViolated).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.backOrder>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.backOrdered>(new bool?(false))))));
      }));
      transitions.AddGroupFrom<SOOrderStatus.cancelled>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.pendingProcessing>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.HasPaymentsInPendingProcessing).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false));
          fas.Add<SOOrder.approved>((Func<BoundedTo<SOOrderEntry, SOOrder>.Assignment.INeedRightOperand, BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured>) (v => (BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured) v.SetFromValue((object) true)));
        }))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.awaitingPayment>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsPaymentRequirementsViolated).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false));
          fas.Add<SOOrder.approved>((Func<BoundedTo<SOOrderEntry, SOOrder>.Assignment.INeedRightOperand, BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured>) (v => (BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured) v.SetFromValue((object) true)));
        }))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.hold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsHoldEntryOrLSEntryEnabled).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.hold>((Func<BoundedTo<SOOrderEntry, SOOrder>.Assignment.INeedRightOperand, BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured>) (v => (BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured) v.SetFromValue((object) true)))))));
        ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder)).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<SOOrder.inclCustOpenOrders>(new bool?(true));
          fas.Add<SOOrder.approved>((Func<BoundedTo<SOOrderEntry, SOOrder>.Assignment.INeedRightOperand, BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured>) (v => (BoundedTo<SOOrderEntry, SOOrder>.Assignment.IConfigured) v.SetFromValue((object) true)));
        }))));
      }));
    }))))))));
  }

  public new class Conditions : WorkflowBase.Conditions
  {
    public BoundedTo<SOOrderEntry, SOOrder>.Condition HasPaymentsInPendingProcessing
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOOrderEntry, SOOrder>.Condition.ConditionBuilder, BoundedTo<SOOrderEntry, SOOrder>.Condition>) (b => b.FromBql<BqlOperand<SOOrder.paymentsNeedValidationCntr, IBqlInt>.IsGreater<Zero>>()), nameof (HasPaymentsInPendingProcessing));
      }
    }

    public BoundedTo<SOOrderEntry, SOOrder>.Condition IsPaymentRequirementsViolated
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOOrderEntry, SOOrder>.Condition.ConditionBuilder, BoundedTo<SOOrderEntry, SOOrder>.Condition>) (b => b.FromBql<BqlOperand<SOOrder.prepaymentReqSatisfied, IBqlBool>.IsEqual<False>>()), nameof (IsPaymentRequirementsViolated));
      }
    }

    public BoundedTo<SOOrderEntry, SOOrder>.Condition IsShippable
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOOrderEntry, SOOrder>.Condition.ConditionBuilder, BoundedTo<SOOrderEntry, SOOrder>.Condition>) (b => b.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrder.openShipmentCntr, Equal<Zero>>>>>.And<BqlOperand<SOOrder.openLineCntr, IBqlInt>.IsGreater<Zero>>>()), nameof (IsShippable));
      }
    }

    public BoundedTo<SOOrderEntry, SOOrder>.Condition IsShippingCompleted
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOOrderEntry, SOOrder>.Condition.ConditionBuilder, BoundedTo<SOOrderEntry, SOOrder>.Condition>) (b => b.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrder.completed, Equal<True>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrder.openShipmentCntr, Equal<Zero>>>>>.And<BqlOperand<SOOrder.openLineCntr, IBqlInt>.IsEqual<Zero>>>>()), nameof (IsShippingCompleted));
      }
    }

    public BoundedTo<SOOrderEntry, SOOrder>.Condition HasOpenShipments
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOOrderEntry, SOOrder>.Condition.ConditionBuilder, BoundedTo<SOOrderEntry, SOOrder>.Condition>) (b => b.FromBql<BqlOperand<SOOrder.openShipmentCntr, IBqlInt>.IsGreater<Zero>>()), nameof (HasOpenShipments));
      }
    }
  }
}
