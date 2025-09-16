// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.GraphExtensions.APPaymentEntry_WorkflowEPPExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.AP.PaymentProcessor.GraphExtensions;

public class APPaymentEntry_WorkflowEPPExt : 
  PXGraphExtension<APPaymentEntry_Workflow, APPaymentEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.paymentProcessor>();

  public sealed override void Configure(PXScreenConfiguration config)
  {
    APPaymentEntry_WorkflowEPPExt.Configure(config.GetScreenConfigurationContext<APPaymentEntry, APPayment>());
  }

  protected static void Configure(WorkflowContext<APPaymentEntry, APPayment> context)
  {
    APPaymentEntry_Workflow.Conditions conditions = context.Conditions.GetPack<APPaymentEntry_Workflow.Conditions>();
    context.UpdateScreenConfigurationFor((Func<BoundedTo<APPaymentEntry, APPayment>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<APPaymentEntry, APPayment>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow((Func<BoundedTo<APPaymentEntry, APPayment>.Workflow.ConfiguratorFlow, BoundedTo<APPaymentEntry, APPayment>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((System.Action<BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.ContainerAdjusterStates>) (states => states.UpdateSequence<APDocStatus.HoldToBalance>((Func<BoundedTo<APPaymentEntry, APPayment>.Sequence.ConfiguratorSequence, BoundedTo<APPaymentEntry, APPayment>.Sequence.ConfiguratorSequence>) (seq => seq.WithStates((System.Action<BoundedTo<APPaymentEntry, APPayment>.Sequence.ContainerAdjusterSequenceStates>) (sss => sss.Add<APDocStatus.pendingProcessing>((Func<BoundedTo<APPaymentEntry, APPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured) flowState.IsSkippedWhen(conditions.IsNotPendingExternalProcessing).WithActions((System.Action<BoundedTo<APPaymentEntry, APPayment>.ActionState.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.putOnHold), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionState.IAllowOptionalConfig, BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured>) (a => (BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
      actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.printCheck), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionState.IAllowOptionalConfig, BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured>) (a => (BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success)));
      actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.voidCheck));
    })).WithEventHandlers((System.Action<BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
    {
      handlers.Add((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment>>>) (g => g.OnVoidDocument));
      handlers.Add((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment>>>) (g => g.OnProcessDocument));
    })).PlaceAfter<APDocStatus.pendingPrint>())))))))).WithTransitions((System.Action<BoundedTo<APPaymentEntry, APPayment>.Transition.ContainerAdjusterTransitions>) (transitions => transitions.AddGroupFrom<APDocStatus.pendingProcessing>((System.Action<BoundedTo<APPaymentEntry, APPayment>.Transition.ISourceContainerFillerTransitions>) (ts =>
    {
      ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.voided>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnVoidDocument))));
      ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.balanced>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnProcessDocument))));
    })))))).WithHandlers((System.Action<BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.ContainerAdjusterHandlers>) (handlers => handlers.Add((Func<BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) handler.WithTargetOf<APPayment>().OfEntityEvent<APPayment.Events>((Expression<Func<APPayment.Events, PXEntityEvent<APPayment>>>) (e => e.ProcessDocument)).Is((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment, APPayment>>>) (g => g.OnProcessDocument)).UsesTargetAsPrimaryEntity().WithUpcastTo<APRegister>()))))));
  }
}
