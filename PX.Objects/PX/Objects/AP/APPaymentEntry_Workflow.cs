// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPaymentEntry_Workflow
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
namespace PX.Objects.AP;

public class APPaymentEntry_Workflow : PXGraphExtension<APPaymentEntry>
{
  public sealed override void Configure(PXScreenConfiguration config)
  {
    APPaymentEntry_Workflow.Configure(config.GetScreenConfigurationContext<APPaymentEntry, APPayment>());
  }

  protected static void Configure(WorkflowContext<APPaymentEntry, APPayment> context)
  {
    APPaymentEntry_Workflow.Conditions conditions = context.Conditions.GetPack<APPaymentEntry_Workflow.Conditions>();
    BoundedTo<APPaymentEntry, APPayment>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<APPaymentEntry, APPayment>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<APPaymentEntry, APPayment>.ActionCategory.IConfigured>) (category => (BoundedTo<APPaymentEntry, APPayment>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<APPaymentEntry, APPayment>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("Approval", (Func<BoundedTo<APPaymentEntry, APPayment>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<APPaymentEntry, APPayment>.ActionCategory.IConfigured>) (category => (BoundedTo<APPaymentEntry, APPayment>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    BoundedTo<APPaymentEntry, APPayment>.ActionCategory.IConfigured correctionsCategory = context.Categories.CreateNew("Corrections", (Func<BoundedTo<APPaymentEntry, APPayment>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<APPaymentEntry, APPayment>.ActionCategory.IConfigured>) (category => (BoundedTo<APPaymentEntry, APPayment>.ActionCategory.IConfigured) category.DisplayName("Corrections")));
    BoundedTo<APPaymentEntry, APPayment>.ActionCategory.IConfigured customOtherCategory = context.Categories.CreateNew("CustomOther", (Func<BoundedTo<APPaymentEntry, APPayment>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<APPaymentEntry, APPayment>.ActionCategory.IConfigured>) (category => (BoundedTo<APPaymentEntry, APPayment>.ActionCategory.IConfigured) category.DisplayName("Other")));
    context.AddScreenConfigurationFor((Func<BoundedTo<APPaymentEntry, APPayment>.ScreenConfiguration.IStartConfigScreen, BoundedTo<APPaymentEntry, APPayment>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<APPaymentEntry, APPayment>.ScreenConfiguration.IConfigured) screen.StateIdentifierIs<APPayment.status>().AddDefaultFlow((Func<BoundedTo<APPaymentEntry, APPayment>.Workflow.INeedStatesFlow, BoundedTo<APPaymentEntry, APPayment>.Workflow.IConfigured>) (flow => (BoundedTo<APPaymentEntry, APPayment>.Workflow.IConfigured) flow.WithFlowStates((System.Action<BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<APPaymentEntry, APPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<APPaymentEntry, PXAutoAction<APPayment>>>) (g => g.initializeState))));
      fss.AddSequence<APDocStatus.HoldToBalance>((Func<BoundedTo<APPaymentEntry, APPayment>.Sequence.INeedStatesThenConditionsConfig, BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured>) (seq => (BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured) seq.WithStates((System.Action<BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IContainerFillerStates>) (sss =>
      {
        sss.Add<APDocStatus.hold>((Func<BoundedTo<APPaymentEntry, APPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured) flowState.IsSkippedWhen(conditions.IsNotOnHold).WithActions((System.Action<BoundedTo<APPaymentEntry, APPayment>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.releaseFromHold), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionState.IAllowOptionalConfig, BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured>) (a => (BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success)))))));
        sss.Add<APDocStatus.pendingPrint>((Func<BoundedTo<APPaymentEntry, APPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured) flowState.IsSkippedWhen(conditions.IsNotPrintable).WithActions((System.Action<BoundedTo<APPaymentEntry, APPayment>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.putOnHold), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionState.IAllowOptionalConfig, BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured>) (a => (BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.printCheck), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionState.IAllowOptionalConfig, BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured>) (a => (BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success)));
        })).WithEventHandlers((System.Action<BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment>>>) (g => g.OnPrintCheck))))));
        sss.Add<APDocStatus.printed>((Func<BoundedTo<APPaymentEntry, APPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured) flowState.IsSkippedWhen(conditions.IsSkipPrinted).WithActions((System.Action<BoundedTo<APPaymentEntry, APPayment>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.release), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionState.IAllowOptionalConfig, BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured>) (a => (BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success))))).WithEventHandlers((System.Action<BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment>>>) (g => g.OnCancelPrintCheck))))));
        sss.Add<APDocStatus.balanced>((Func<BoundedTo<APPaymentEntry, APPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<APPaymentEntry, APPayment>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.release), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionState.IAllowOptionalConfig, BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured>) (a => (BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success)));
          actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.putOnHold));
          actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.voidCheck));
        }))));
      })).WithActions((System.Action<BoundedTo<APPaymentEntry, APPayment>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.printAPEdit));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.vendorDocuments));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.validateAddresses));
      })).WithEventHandlers((System.Action<BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment>>>) (g => g.OnUpdateStatus));
        handlers.Add((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment>>>) (g => g.OnReleaseDocument));
      }))));
      fss.Add<APDocStatus.prebooked>((Func<BoundedTo<APPaymentEntry, APPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<APPaymentEntry, APPayment>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.release), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionState.IAllowOptionalConfig, BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured>) (a => (BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.printAPEdit));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.printAPRegister));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.printAPPayment));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.vendorDocuments));
      })).WithEventHandlers((System.Action<BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment>>>) (g => g.OnReleaseDocument));
        handlers.Add((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment>>>) (g => g.OnVoidDocument));
        handlers.Add((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment>>>) (g => g.OnCloseDocument));
      }))));
      fss.Add<APDocStatus.open>((Func<BoundedTo<APPaymentEntry, APPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<APPaymentEntry, APPayment>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.release), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionState.IAllowOptionalConfig, BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured>) (a => (BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success)));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.putOnHold));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.printAPRegister));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.printAPPayment));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.vendorDocuments));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.voidCheck));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.reverseApplication));
        actions.Add((Expression<Func<APPaymentEntry, PXAutoAction<APPayment>>>) (g => g.initializeState), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured>) (act => (BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured) act.IsAutoAction()));
      })).WithEventHandlers((System.Action<BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment>>>) (g => g.OnReleaseDocument));
        handlers.Add((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment>>>) (g => g.OnVoidDocument));
        handlers.Add((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment>>>) (g => g.OnCloseDocument));
      }))));
      fss.Add<APDocStatus.unapplied>((Func<BoundedTo<APPaymentEntry, APPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<APPaymentEntry, APPayment>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.release), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionState.IAllowOptionalConfig, BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured>) (a => (BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success)));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.putOnHold));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.printAPRegister));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.printAPPayment));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.vendorDocuments));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.voidCheck));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.reverseApplication));
        actions.Add((Expression<Func<APPaymentEntry, PXAutoAction<APPayment>>>) (g => g.initializeState), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured>) (act => (BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured) act.IsAutoAction()));
      })).WithEventHandlers((System.Action<BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment>>>) (g => g.OnReleaseDocument));
        handlers.Add((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment>>>) (g => g.OnVoidDocument));
        handlers.Add((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment>>>) (g => g.OnCloseDocument));
      }))));
      fss.Add<APDocStatus.reserved>((Func<BoundedTo<APPaymentEntry, APPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<APPaymentEntry, APPayment>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.releaseFromHold), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionState.IAllowOptionalConfig, BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured>) (a => (BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success)));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.printAPRegister));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.printAPPayment));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.vendorDocuments));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.voidCheck));
      })).WithEventHandlers((System.Action<BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment>>>) (g => g.OnVoidDocument))))));
      fss.Add<APDocStatus.closed>((Func<BoundedTo<APPaymentEntry, APPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<APPaymentEntry, APPayment>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.printAPRegister));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.printAPPayment));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.vendorDocuments));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.reverseApplication));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.voidCheck), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionState.IAllowOptionalConfig, BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured>) (a => (BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
      })).WithEventHandlers((System.Action<BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment>>>) (g => g.OnVoidDocument));
        handlers.Add((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment>>>) (g => g.OnOpenDocument));
      }))));
      fss.Add<APDocStatus.voided>((Func<BoundedTo<APPaymentEntry, APPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<APPaymentEntry, APPayment>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.printAPPayment));
        actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.printAPRegister));
      }))));
    })).WithTransitions((System.Action<BoundedTo<APPaymentEntry, APPayment>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (System.Action<BoundedTo<APPaymentEntry, APPayment>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.HoldToBalance>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.initializeState))))));
      transitions.AddGroupFrom<APDocStatus.HoldToBalance>((System.Action<BoundedTo<APPaymentEntry, APPayment>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.HoldToBalance>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnUpdateStatus))))));
      transitions.AddGroupFrom<APDocStatus.pendingPrint>((System.Action<BoundedTo<APPaymentEntry, APPayment>.Transition.ISourceContainerFillerTransitions>) (ts => { }));
      transitions.AddGroupFrom<APDocStatus.printed>((System.Action<BoundedTo<APPaymentEntry, APPayment>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.open>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnReleaseDocument)).When((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) (conditions.IsOpen && !conditions.IsPrepaymentInvoice))));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.unapplied>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnReleaseDocument)).When((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) (conditions.IsOpen && conditions.IsPrepaymentInvoice))));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.closed>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnReleaseDocument)).When((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) conditions.IsClosed)));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.pendingPrint>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnCancelPrintCheck))));
      }));
      transitions.AddGroupFrom<APDocStatus.balanced>((System.Action<BoundedTo<APPaymentEntry, APPayment>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.open>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnReleaseDocument)).When((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) (conditions.IsOpen && !conditions.IsPrepaymentInvoice))));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.unapplied>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnReleaseDocument)).When((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) (conditions.IsOpen && conditions.IsPrepaymentInvoice))));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.closed>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnReleaseDocument)).When((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) conditions.IsClosed)));
      }));
      transitions.AddGroupFrom<APDocStatus.prebooked>((System.Action<BoundedTo<APPaymentEntry, APPayment>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.voided>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnVoidDocument))));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.closed>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnReleaseDocument)).When((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) conditions.IsClosed)));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.open>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnReleaseDocument)).When((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) (conditions.IsOpen && !conditions.IsPrepaymentInvoice))));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.unapplied>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnReleaseDocument)).When((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) (conditions.IsOpen && conditions.IsPrepaymentInvoice))));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.closed>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnCloseDocument))));
      }));
      transitions.AddGroupFrom<APDocStatus.open>((System.Action<BoundedTo<APPaymentEntry, APPayment>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.reserved>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.putOnHold)).WithFieldAssignments((System.Action<BoundedTo<APPaymentEntry, APPayment>.Assignment.IContainerFillerFields>) (fas => fas.Add<APRegister.rejected>((Func<BoundedTo<APPaymentEntry, APPayment>.Assignment.INeedRightOperand, BoundedTo<APPaymentEntry, APPayment>.Assignment.IConfigured>) (f => (BoundedTo<APPaymentEntry, APPayment>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.voided>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnVoidDocument))));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.closed>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnReleaseDocument)).When((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) conditions.IsClosed)));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.closed>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnCloseDocument))));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.closed>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.initializeState)).When((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) conditions.IsClosed)));
      }));
      transitions.AddGroupFrom<APDocStatus.unapplied>((System.Action<BoundedTo<APPaymentEntry, APPayment>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.reserved>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.putOnHold)).WithFieldAssignments((System.Action<BoundedTo<APPaymentEntry, APPayment>.Assignment.IContainerFillerFields>) (fas => fas.Add<APRegister.rejected>((Func<BoundedTo<APPaymentEntry, APPayment>.Assignment.INeedRightOperand, BoundedTo<APPaymentEntry, APPayment>.Assignment.IConfigured>) (f => (BoundedTo<APPaymentEntry, APPayment>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.voided>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnVoidDocument))));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.closed>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnReleaseDocument)).When((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) conditions.IsClosed)));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.closed>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnCloseDocument))));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.closed>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.initializeState)).When((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) conditions.IsClosed)));
      }));
      transitions.AddGroupFrom<APDocStatus.reserved>((System.Action<BoundedTo<APPaymentEntry, APPayment>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.open>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.releaseFromHold)).When((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) !conditions.IsPrepaymentInvoice)));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.unapplied>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.releaseFromHold)).When((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) conditions.IsPrepaymentInvoice)));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.voided>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnVoidDocument))));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.closed>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnReleaseDocument)).When((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) conditions.IsClosed)));
      }));
      transitions.AddGroupFrom<APDocStatus.closed>((System.Action<BoundedTo<APPaymentEntry, APPayment>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.open>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnOpenDocument)).When((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) !conditions.IsPrepaymentInvoice)));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.unapplied>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnOpenDocument)).When((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) conditions.IsPrepaymentInvoice)));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.voided>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnVoidDocument))));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.open>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.reverseApplication)).When((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) !conditions.IsPrepaymentInvoice).DoesNotPersist()));
        ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.unapplied>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.reverseApplication)).When((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) conditions.IsPrepaymentInvoice).DoesNotPersist()));
      }));
    })))).WithActions((System.Action<BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.initializeState), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured>) (a => (BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.releaseFromHold), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured>) (c => (BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).WithPersistOptions(ActionPersistOptions.NoPersist).WithFieldAssignments((System.Action<BoundedTo<APPaymentEntry, APPayment>.Assignment.IContainerFillerFields>) (fas => fas.Add<APPayment.hold>((Func<BoundedTo<APPaymentEntry, APPayment>.Assignment.INeedRightOperand, BoundedTo<APPaymentEntry, APPayment>.Assignment.IConfigured>) (f => (BoundedTo<APPaymentEntry, APPayment>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.putOnHold), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured>) (c => (BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).WithPersistOptions(ActionPersistOptions.NoPersist).WithFieldAssignments((System.Action<BoundedTo<APPaymentEntry, APPayment>.Assignment.IContainerFillerFields>) (fas => fas.Add<APPayment.hold>((Func<BoundedTo<APPaymentEntry, APPayment>.Assignment.INeedRightOperand, BoundedTo<APPaymentEntry, APPayment>.Assignment.IConfigured>) (f => (BoundedTo<APPaymentEntry, APPayment>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.printCheck), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured>) (c => (BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).IsHiddenWhen((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) (conditions.IsNotPrintable && conditions.IsNotPendingExternalProcessing))));
      actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.release), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured>) (c => (BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured) c.WithCategory(processingCategory)));
      actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.voidCheck), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured>) (c => (BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured) c.WithCategory(correctionsCategory).IsHiddenWhen((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) conditions.IsVoidHidden)));
      actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.validateAddresses), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured>) (g => (BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured) g.WithCategory(customOtherCategory)));
      actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.reverseApplication), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured>) (g => (BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured) g.WithPersistOptions(ActionPersistOptions.NoPersist)));
      actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.vendorDocuments), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured>) (c => (BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured) c.WithCategory(PredefinedCategory.Inquiries)));
      actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.printAPEdit), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured>) (c => (BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured) c.WithCategory(PredefinedCategory.Reports).IsHiddenWhen((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) conditions.IsDebitAdj)));
      actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.printAPRegister), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured>) (c => (BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured) c.WithCategory(PredefinedCategory.Reports)));
      actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.printAPPayment), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured>) (c => (BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured) c.WithCategory(PredefinedCategory.Reports).IsHiddenWhen((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) (conditions.IsDebitAdj || conditions.IsPrepaymentInvoice))));
    })).WithHandlers((System.Action<BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers =>
    {
      handlers.Add((Func<BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) handler.WithTargetOf<APPayment>().OfEntityEvent<APPayment.Events>((Expression<Func<APPayment.Events, PXEntityEvent<APPayment>>>) (e => e.PrintCheck)).Is((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment, APPayment>>>) (g => g.OnPrintCheck)).UsesTargetAsPrimaryEntity().WithFieldAssignments((System.Action<BoundedTo<APPaymentEntry, APPayment>.Assignment.IContainerFillerFields>) (fas => fas.Add<APPayment.printed>((Func<BoundedTo<APPaymentEntry, APPayment>.Assignment.INeedRightOperand, BoundedTo<APPaymentEntry, APPayment>.Assignment.IConfigured>) (f => (BoundedTo<APPaymentEntry, APPayment>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      handlers.Add((Func<BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) handler.WithTargetOf<APPayment>().OfEntityEvent<APPayment.Events>((Expression<Func<APPayment.Events, PXEntityEvent<APPayment>>>) (e => e.CancelPrintCheck)).Is((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment, APPayment>>>) (g => g.OnCancelPrintCheck)).UsesTargetAsPrimaryEntity().WithFieldAssignments((System.Action<BoundedTo<APPaymentEntry, APPayment>.Assignment.IContainerFillerFields>) (fas =>
      {
        fas.Add<APPayment.hold>((Func<BoundedTo<APPaymentEntry, APPayment>.Assignment.INeedRightOperand, BoundedTo<APPaymentEntry, APPayment>.Assignment.IConfigured>) (f => (BoundedTo<APPaymentEntry, APPayment>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fas.Add<APPayment.printed>((Func<BoundedTo<APPaymentEntry, APPayment>.Assignment.INeedRightOperand, BoundedTo<APPaymentEntry, APPayment>.Assignment.IConfigured>) (f => (BoundedTo<APPaymentEntry, APPayment>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fas.Add<APPayment.extRefNbr>((Func<BoundedTo<APPaymentEntry, APPayment>.Assignment.INeedRightOperand, BoundedTo<APPaymentEntry, APPayment>.Assignment.IConfigured>) (f => (BoundedTo<APPaymentEntry, APPayment>.Assignment.IConfigured) f.SetFromValue((object) null)));
      }))));
      handlers.Add((Func<BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) handler.WithTargetOf<APPayment>().OfEntityEvent<APPayment.Events>((Expression<Func<APPayment.Events, PXEntityEvent<APPayment>>>) (e => e.ReleaseDocument)).Is((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment, APPayment>>>) (g => g.OnReleaseDocument)).UsesTargetAsPrimaryEntity().WithUpcastTo<APRegister>()));
      handlers.Add((Func<BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) handler.WithTargetOf<APPayment>().OfEntityEvent<APPayment.Events>((Expression<Func<APPayment.Events, PXEntityEvent<APPayment>>>) (e => e.VoidDocument)).Is((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment, APPayment>>>) (g => g.OnVoidDocument)).UsesTargetAsPrimaryEntity().WithUpcastTo<APRegister>()));
      handlers.Add((Func<BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) handler.WithTargetOf<APPayment>().OfEntityEvent<APPayment.Events>((Expression<Func<APPayment.Events, PXEntityEvent<APPayment>>>) (e => e.OpenDocument)).Is((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment, APPayment>>>) (g => g.OnOpenDocument)).UsesTargetAsPrimaryEntity().WithUpcastTo<APRegister>()));
      handlers.Add((Func<BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) handler.WithTargetOf<APPayment>().OfEntityEvent<APPayment.Events>((Expression<Func<APPayment.Events, PXEntityEvent<APPayment>>>) (e => e.CloseDocument)).Is((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment, APPayment>>>) (g => g.OnCloseDocument)).UsesTargetAsPrimaryEntity().WithUpcastTo<APRegister>()));
      handlers.Add((Func<BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<APPaymentEntry, APPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) handler.WithTargetOf<APPayment>().OfFieldsUpdated<TypeArrayOf<IBqlField>.FilledWith<APPayment.hold, APPayment.printCheck, APPayment.printed>>().Is((Expression<Func<APPaymentEntry, PXWorkflowEventHandler<APPayment, APPayment>>>) (g => g.OnUpdateStatus)).UsesTargetAsPrimaryEntity()));
    })).WithCategories((System.Action<BoundedTo<APPaymentEntry, APPayment>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(correctionsCategory);
      categories.Add(approvalCategory);
      categories.Add(customOtherCategory);
      categories.Update(FolderType.InquiriesFolder, (Func<BoundedTo<APPaymentEntry, APPayment>.ActionCategory.ConfiguratorCategory, BoundedTo<APPaymentEntry, APPayment>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(customOtherCategory)));
      categories.Update(FolderType.ReportsFolder, (Func<BoundedTo<APPaymentEntry, APPayment>.ActionCategory.ConfiguratorCategory, BoundedTo<APPaymentEntry, APPayment>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(context.Categories.Get(FolderType.InquiriesFolder))));
    }))));
  }

  public class Conditions : BoundedTo<APPaymentEntry, APPayment>.Condition.Pack
  {
    public BoundedTo<APPaymentEntry, APPayment>.Condition IsNotOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APPaymentEntry, APPayment>.Condition.ConditionBuilder, BoundedTo<APPaymentEntry, APPayment>.Condition>) (c => c.FromBql<BqlOperand<APPayment.hold, IBqlBool>.IsEqual<False>>()), nameof (IsNotOnHold));
      }
    }

    public BoundedTo<APPaymentEntry, APPayment>.Condition IsReserved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APPaymentEntry, APPayment>.Condition.ConditionBuilder, BoundedTo<APPaymentEntry, APPayment>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPayment.hold, Equal<True>>>>>.And<BqlOperand<APPayment.released, IBqlBool>.IsEqual<True>>>()), nameof (IsReserved));
      }
    }

    public BoundedTo<APPaymentEntry, APPayment>.Condition IsNotPrintable
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APPaymentEntry, APPayment>.Condition.ConditionBuilder, BoundedTo<APPaymentEntry, APPayment>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPayment.printCheck, Equal<False>>>>>.Or<BqlOperand<APPayment.printed, IBqlBool>.IsEqual<True>>>()), nameof (IsNotPrintable));
      }
    }

    public BoundedTo<APPaymentEntry, APPayment>.Condition IsSkipPrinted
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APPaymentEntry, APPayment>.Condition.ConditionBuilder, BoundedTo<APPaymentEntry, APPayment>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPayment.printCheck, Equal<False>>>>>.Or<BqlOperand<APPayment.printed, IBqlBool>.IsEqual<False>>>()), nameof (IsSkipPrinted));
      }
    }

    public BoundedTo<APPaymentEntry, APPayment>.Condition IsOpen
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APPaymentEntry, APPayment>.Condition.ConditionBuilder, BoundedTo<APPaymentEntry, APPayment>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPayment.openDoc, Equal<True>>>>>.And<BqlOperand<APPayment.released, IBqlBool>.IsEqual<True>>>()), nameof (IsOpen));
      }
    }

    public BoundedTo<APPaymentEntry, APPayment>.Condition IsClosed
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APPaymentEntry, APPayment>.Condition.ConditionBuilder, BoundedTo<APPaymentEntry, APPayment>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APPayment.openDoc, Equal<False>>>>>.And<BqlOperand<APPayment.released, IBqlBool>.IsEqual<True>>>()), nameof (IsClosed));
      }
    }

    public BoundedTo<APPaymentEntry, APPayment>.Condition IsVoidHidden
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APPaymentEntry, APPayment>.Condition.ConditionBuilder, BoundedTo<APPaymentEntry, APPayment>.Condition>) (c => c.FromBql<BqlOperand<APPayment.docType, IBqlString>.IsIn<APDocType.voidCheck, APDocType.voidRefund, APDocType.debitAdj>>()), nameof (IsVoidHidden));
      }
    }

    public BoundedTo<APPaymentEntry, APPayment>.Condition IsDebitAdj
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APPaymentEntry, APPayment>.Condition.ConditionBuilder, BoundedTo<APPaymentEntry, APPayment>.Condition>) (c => c.FromBql<BqlOperand<APPayment.docType, IBqlString>.IsEqual<APDocType.debitAdj>>()), nameof (IsDebitAdj));
      }
    }

    public BoundedTo<APPaymentEntry, APPayment>.Condition IsNotPendingExternalProcessing
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APPaymentEntry, APPayment>.Condition.ConditionBuilder, BoundedTo<APPaymentEntry, APPayment>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APRegister.pendingProcessing, Equal<False>>>>>.Or<BqlOperand<APPayment.externalPaymentStatus, IBqlString>.IsNotEqual<Null>>>()), nameof (IsNotPendingExternalProcessing));
      }
    }

    public BoundedTo<APPaymentEntry, APPayment>.Condition IsPrepaymentInvoice
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APPaymentEntry, APPayment>.Condition.ConditionBuilder, BoundedTo<APPaymentEntry, APPayment>.Condition>) (c => c.FromBql<BqlOperand<APPayment.docType, IBqlString>.IsEqual<APDocType.prepaymentInvoice>>()), nameof (IsPrepaymentInvoice));
      }
    }
  }

  public static class ActionCategoryNames
  {
    public const string Processing = "Processing";
    public const string Approval = "Approval";
    public const string Corrections = "Corrections";
    public const string CustomOther = "CustomOther";
    public const string CustomInquiries = "Inquiries";
  }

  public static class ActionCategory
  {
    public const string Processing = "Processing";
    public const string Approval = "Approval";
    public const string Corrections = "Corrections";
    public const string Other = "Other";
    public const string Inquiries = "Inquiries";
  }
}
