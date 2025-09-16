// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APInvoiceEntry_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.CS;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.AP;

public class APInvoiceEntry_Workflow : PXGraphExtension<APInvoiceEntry>
{
  [PXWorkflowDependsOnType(new System.Type[] {typeof (APSetup)})]
  public sealed override void Configure(PXScreenConfiguration config)
  {
    APInvoiceEntry_Workflow.Configure(config.GetScreenConfigurationContext<APInvoiceEntry, APInvoice>());
  }

  protected static void Configure(WorkflowContext<APInvoiceEntry, APInvoice> context)
  {
    APInvoiceEntry_Workflow.Conditions conditions = context.Conditions.GetPack<APInvoiceEntry_Workflow.Conditions>();
    BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.IConfigured>) (category => (BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("Approval", (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.IConfigured>) (category => (BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.IConfigured correctionsCategory = context.Categories.CreateNew("Corrections", (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.IConfigured>) (category => (BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.IConfigured) category.DisplayName("Corrections")));
    BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.IConfigured customOtherCategory = context.Categories.CreateNew("CustomOther", (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.IConfigured>) (category => (BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.IConfigured) category.DisplayName("Other")));
    context.AddScreenConfigurationFor((Func<BoundedTo<APInvoiceEntry, APInvoice>.ScreenConfiguration.IStartConfigScreen, BoundedTo<APInvoiceEntry, APInvoice>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<APInvoiceEntry, APInvoice>.ScreenConfiguration.IConfigured) screen.StateIdentifierIs<APInvoice.status>().AddDefaultFlow((Func<BoundedTo<APInvoiceEntry, APInvoice>.Workflow.INeedStatesFlow, BoundedTo<APInvoiceEntry, APInvoice>.Workflow.IConfigured>) (flow => (BoundedTo<APInvoiceEntry, APInvoice>.Workflow.IConfigured) flow.WithFlowStates((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<APInvoiceEntry, APInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<APInvoiceEntry, PXAutoAction<APInvoice>>>) (g => g.initializeState))));
      fss.AddSequence<APDocStatus.HoldToBalance>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Sequence.INeedStatesThenConditionsConfig, BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured>) (seq => (BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured) seq.WithStates((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IContainerFillerStates>) (sss =>
      {
        sss.Add<APDocStatus.hold>((Func<BoundedTo<APInvoiceEntry, APInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured) flowState.IsSkippedWhen(conditions.IsNotOnHold).WithActions((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.releaseFromHold), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured>) (a => (BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success)))))));
        sss.Add<APDocStatus.balanced>((Func<BoundedTo<APInvoiceEntry, APInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.release), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured>) (a => (BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success)));
          actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.prebook), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured>) (a => (BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.putOnHold));
          actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.createSchedule));
        }))));
      })).WithActions((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.printAPEdit));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.vendorDocuments));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.recalculateDiscountsAction));
      })).WithEventHandlers((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice>>>) (g => g.OnUpdateStatus));
        handlers.Add<APRegister>((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice, APRegister>>>) (g => g.OnConfirmSchedule));
        handlers.Add((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice>>>) (g => g.OnReleaseDocument));
      }))));
      fss.Add<APDocStatus.scheduled>((Func<BoundedTo<APInvoiceEntry, APInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.createSchedule), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured>) (a => (BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.printAPEdit));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.vendorDocuments));
      })).WithEventHandlers((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add<APRegister>((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice, APRegister>>>) (g => g.OnConfirmSchedule));
        handlers.Add<APRegister>((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice, APRegister>>>) (g => g.OnVoidSchedule));
      }))));
      fss.Add<APDocStatus.voided>((Func<BoundedTo<APInvoiceEntry, APInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.printAPEdit));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.vendorDocuments));
      }))));
      fss.Add<APDocStatus.pendingPayment>((Func<BoundedTo<APInvoiceEntry, APInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.payInvoice), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured>) (a => (BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success)));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.printAPRegister));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.vendorDocuments));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.voidDocument));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.voidInvoice));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.vendorRefund));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.reverseInvoice));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.reclassifyBatch));
      })).WithEventHandlers((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice>>>) (g => g.OnCloseDocument));
        handlers.Add((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice>>>) (g => g.OnReleaseDocument));
        handlers.Add((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice>>>) (g => g.OnVoidDocument));
      }))));
      fss.Add<APDocStatus.open>((Func<BoundedTo<APInvoiceEntry, APInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.payInvoice), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured>) (a => (BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success)));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.printAPRegister));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.vendorDocuments));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.voidDocument));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.voidInvoice));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.vendorRefund));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.reverseInvoice));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.reclassifyBatch));
      })).WithEventHandlers((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice>>>) (g => g.OnCloseDocument));
        handlers.Add((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice>>>) (g => g.OnReleaseDocument));
        handlers.Add((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice>>>) (g => g.OnVoidDocument));
      }))));
      fss.Add<APDocStatus.unapplied>((Func<BoundedTo<APInvoiceEntry, APInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.payInvoice), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured>) (a => (BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success)));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.printAPRegister));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.vendorDocuments));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.voidDocument));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.voidInvoice));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.vendorRefund));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.reverseInvoice));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.reclassifyBatch));
      })).WithEventHandlers((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice>>>) (g => g.OnCloseDocument));
        handlers.Add((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice>>>) (g => g.OnReleaseDocument));
        handlers.Add((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice>>>) (g => g.OnVoidDocument));
        handlers.Add((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice>>>) (g => g.OnOpenDocument));
      }))));
      fss.Add<APDocStatus.prebooked>((Func<BoundedTo<APInvoiceEntry, APInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.release), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured>) (a => (BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.payInvoice), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured>) (a => (BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.printAPRegister));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.vendorDocuments));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.reverseInvoice));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.voidInvoice));
      })).WithEventHandlers((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice>>>) (g => g.OnReleaseDocument))))));
      fss.Add<APDocStatus.printed>((Func<BoundedTo<APInvoiceEntry, APInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.release), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured>) (a => (BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.payInvoice), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured>) (a => (BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.printAPRegister));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.vendorDocuments));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.voidInvoice));
      })).WithEventHandlers((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice>>>) (g => g.OnReleaseDocument))))));
      fss.Add<APDocStatus.closed>((Func<BoundedTo<APInvoiceEntry, APInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.printAPRegister));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.vendorDocuments));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.reverseInvoice));
        actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.reclassifyBatch));
      })).WithEventHandlers((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice>>>) (g => g.OnOpenDocument))))));
      fss.Add<APDocStatus.reserved>();
    })).WithTransitions((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.HoldToBalance>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.initializeState))))));
      transitions.AddGroupFrom<APDocStatus.HoldToBalance>((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.HoldToBalance>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnUpdateStatus))));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.pendingPayment>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnReleaseDocument)).When((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) (conditions.IsOpen && conditions.IsPrepaymentInvoice)).WithFieldAssignments((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IContainerFillerFields>) (fas => fas.Add<APInvoice.pendingPayment>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (e => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.open>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnReleaseDocument)).When((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) (conditions.IsOpen && !conditions.IsPrepaymentInvoice))));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.closed>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnReleaseDocument)).When((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) conditions.IsClosed)));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.scheduled>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnConfirmSchedule)).WithFieldAssignments((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<APInvoice.scheduled>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (e => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) e.SetFromValue((object) true)));
          fas.Add<APInvoice.scheduleID>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (e => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) e.SetFromExpression("@ScheduleID")));
        }))));
      }));
      transitions.AddGroupFrom<APDocStatus.balanced>((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.prebooked>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.prebook))))));
      transitions.AddGroupFrom<APDocStatus.prebooked>((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.voided>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.voidInvoice))));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.open>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnReleaseDocument)).When((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) (conditions.IsOpen && !conditions.IsPrepaymentInvoice))));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.unapplied>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnReleaseDocument)).When((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) (conditions.IsOpen && conditions.IsPrepaymentInvoice))));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.closed>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnReleaseDocument)).When((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) conditions.IsClosed)));
      }));
      transitions.AddGroupFrom<APDocStatus.scheduled>((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.scheduled>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnConfirmSchedule)).WithFieldAssignments((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<APInvoice.scheduled>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (e => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) e.SetFromValue((object) true)));
          fas.Add<APInvoice.scheduleID>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (e => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) e.SetFromExpression("@ScheduleID")));
        }))));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.voided>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnVoidSchedule)).WithFieldAssignments((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<APInvoice.voided>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (e => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) e.SetFromValue((object) true)));
          fas.Add<APInvoice.scheduled>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (e => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) e.SetFromValue((object) false)));
          fas.Add<APInvoice.scheduleID>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (e => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) e.SetFromValue((object) null)));
        }))));
      }));
      transitions.AddGroupFrom<APDocStatus.pendingPayment>((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.unapplied>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnReleaseDocument)).WithFieldAssignments((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IContainerFillerFields>) (fas => fas.Add<APInvoice.pendingPayment>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (e => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) e.SetFromValue((object) false)))))));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.unapplied>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnCloseDocument)).WithFieldAssignments((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IContainerFillerFields>) (fas => fas.Add<APInvoice.pendingPayment>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (e => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) e.SetFromValue((object) false)))))));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.voided>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnVoidDocument)).WithFieldAssignments((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<APInvoice.openDoc>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (e => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) e.SetFromValue((object) false)));
          fas.Add<APInvoice.pendingPayment>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (e => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) e.SetFromValue((object) false)));
          fas.Add<APInvoice.voided>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (e => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) e.SetFromValue((object) true)));
        }))));
      }));
      transitions.AddGroupFrom<APDocStatus.open>((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.voided>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.voidInvoice))));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.voided>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.voidDocument))));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.closed>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnCloseDocument))));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.closed>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnReleaseDocument)).When((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) conditions.IsClosed)));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.voided>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnVoidDocument))));
      }));
      transitions.AddGroupFrom<APDocStatus.unapplied>((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.voided>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.voidInvoice))));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.voided>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.voidDocument))));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.closed>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnCloseDocument))));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.voided>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnVoidDocument))));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.pendingPayment>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnOpenDocument)).WithFieldAssignments((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IContainerFillerFields>) (fas => fas.Add<APInvoice.pendingPayment>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (e => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
      }));
      transitions.AddGroupFrom<APDocStatus.printed>((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.open>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnReleaseDocument)).When((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) (conditions.IsOpen && !conditions.IsPrepaymentInvoice))));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.unapplied>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnReleaseDocument)).When((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) (conditions.IsOpen && conditions.IsPrepaymentInvoice))));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.closed>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnReleaseDocument)).When((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) conditions.IsClosed)));
      }));
      transitions.AddGroupFrom<APDocStatus.closed>((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.open>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnOpenDocument)).When((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) !conditions.IsPrepaymentInvoice)));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.unapplied>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnOpenDocument)).When((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) (conditions.IsPrepaymentInvoice && conditions.IsZeroBalance))));
        ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.pendingPayment>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnOpenDocument)).When((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) (conditions.IsPrepaymentInvoice && !conditions.IsZeroBalance)).WithFieldAssignments((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IContainerFillerFields>) (fas => fas.Add<APInvoice.pendingPayment>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (e => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
      }));
    })))).WithActions((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.initializeState), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (a => (BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.releaseFromHold), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).WithPersistOptions(ActionPersistOptions.NoPersist).WithFieldAssignments((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IContainerFillerFields>) (fas => fas.Add<APInvoice.hold>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (f => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.putOnHold), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).WithPersistOptions(ActionPersistOptions.NoPersist).WithFieldAssignments((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IContainerFillerFields>) (fas => fas.Add<APInvoice.hold>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (f => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.prebook), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).IsHiddenWhen((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) (conditions.IsPrepayment || conditions.IsPrepaymentInvoice || conditions.IsPrepaymentInvoiceReversing || conditions.IsMigrationMode)).IsDisabledWhen((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) conditions.IsRetainage)));
      actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.release), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.WithCategory(processingCategory)));
      actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.payInvoice), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).IsHiddenWhen((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) conditions.IsMigrationMode).IsDisabledWhen((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) conditions.IsClosed)));
      actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.vendorRefund), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).IsDisabledWhen((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) conditions.IsNotAllowRefund).IsHiddenWhen((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) (conditions.IsNotDebitAdjustment || conditions.IsMigrationMode))));
      actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.reverseInvoice), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.WithCategory(correctionsCategory)));
      actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.voidInvoice), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.WithCategory(correctionsCategory).PlaceAfterInCategory((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.reverseInvoice)).IsHiddenWhen((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) (conditions.IsNotAllowVoidInvoice || conditions.IsMigrationMode))));
      actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.reclassifyBatch), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.WithCategory(correctionsCategory).PlaceAfterInCategory((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.voidInvoice)).IsDisabledWhen((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) conditions.IsNotAllowReclasify).IsHiddenWhen((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) (conditions.IsMigrationMode || conditions.IsPrepayment || conditions.IsPrepaymentInvoice || conditions.IsPrepaymentInvoiceReversing))));
      actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.voidDocument), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.WithCategory(correctionsCategory).IsDisabledWhen((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) conditions.IsNotAllowVoidPrepayment).IsHiddenWhen((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) (conditions.IsNotAllowVoidPrepayment || conditions.IsMigrationMode))));
      actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.createSchedule), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.WithCategory(customOtherCategory).IsHiddenWhen((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) (conditions.IsMigrationMode || conditions.IsPrepaymentInvoice || conditions.IsPrepaymentInvoiceReversing))));
      actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.recalculateDiscountsAction), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.WithCategory(customOtherCategory).IsHiddenWhen((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) (conditions.IsMigrationMode || conditions.IsPrepaymentInvoice || conditions.IsPrepaymentInvoiceReversing)).IsDisabledWhen((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) conditions.IsNotAllowRecalcPrice)));
      actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.vendorDocuments), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.WithCategory(PredefinedCategory.Inquiries)));
      actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.printAPEdit), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.WithCategory(PredefinedCategory.Reports)));
      actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.printAPRegister), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) c.WithCategory(PredefinedCategory.Reports).IsHiddenWhen((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) conditions.IsPrepayment)));
    })).WithHandlers((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers =>
    {
      handlers.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) handler.WithTargetOf<APRegister>().WithParametersOf<PX.Objects.GL.Schedule>().OfEntityEvent<APRegister.Events>((Expression<Func<APRegister.Events, PXEntityEvent<APRegister, PX.Objects.GL.Schedule>>>) (e => e.ConfirmSchedule)).Is((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice, APRegister>>>) (g => g.OnConfirmSchedule)).UsesPrimaryEntityGetter<SelectFromBase<APInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.docType, Equal<BqlField<APRegister.docType, IBqlString>.FromCurrent>>>>>.And<BqlOperand<APInvoice.refNbr, IBqlString>.IsEqual<BqlField<APRegister.refNbr, IBqlString>.FromCurrent>>>>()));
      handlers.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) handler.WithTargetOf<APRegister>().WithParametersOf<PX.Objects.GL.Schedule>().OfEntityEvent<APRegister.Events>((Expression<Func<APRegister.Events, PXEntityEvent<APRegister, PX.Objects.GL.Schedule>>>) (e => e.VoidSchedule)).Is((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice, APRegister>>>) (g => g.OnVoidSchedule)).UsesPrimaryEntityGetter<SelectFromBase<APInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.docType, Equal<BqlField<APRegister.docType, IBqlString>.FromCurrent>>>>>.And<BqlOperand<APInvoice.refNbr, IBqlString>.IsEqual<BqlField<APRegister.refNbr, IBqlString>.FromCurrent>>>>()));
      handlers.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) handler.WithTargetOf<APInvoice>().OfEntityEvent<APInvoice.Events>((Expression<Func<APInvoice.Events, PXEntityEvent<APInvoice>>>) (e => e.OpenDocument)).Is((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice, APInvoice>>>) (g => g.OnOpenDocument)).UsesTargetAsPrimaryEntity().WithUpcastTo<APRegister>()));
      handlers.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) handler.WithTargetOf<APInvoice>().OfEntityEvent<APInvoice.Events>((Expression<Func<APInvoice.Events, PXEntityEvent<APInvoice>>>) (e => e.CloseDocument)).Is((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice, APInvoice>>>) (g => g.OnCloseDocument)).UsesTargetAsPrimaryEntity().WithUpcastTo<APRegister>()));
      handlers.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) handler.WithTargetOf<APInvoice>().OfEntityEvent<APInvoice.Events>((Expression<Func<APInvoice.Events, PXEntityEvent<APInvoice>>>) (e => e.ReleaseDocument)).Is((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice, APInvoice>>>) (g => g.OnReleaseDocument)).UsesTargetAsPrimaryEntity().WithUpcastTo<APRegister>()));
      handlers.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) handler.WithTargetOf<APInvoice>().OfEntityEvent<APInvoice.Events>((Expression<Func<APInvoice.Events, PXEntityEvent<APInvoice>>>) (e => e.VoidDocument)).Is((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice, APInvoice>>>) (g => g.OnVoidDocument)).UsesTargetAsPrimaryEntity().WithUpcastTo<APRegister>()));
      handlers.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<APInvoiceEntry, APInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) handler.WithTargetOf<APInvoice>().OfFieldUpdated<APInvoice.hold>().Is((Expression<Func<APInvoiceEntry, PXWorkflowEventHandler<APInvoice, APInvoice>>>) (g => g.OnUpdateStatus)).UsesTargetAsPrimaryEntity()));
    })).WithCategories((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(correctionsCategory);
      categories.Add(approvalCategory);
      categories.Add(customOtherCategory);
      categories.Update(FolderType.InquiriesFolder, (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.ConfiguratorCategory, BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(customOtherCategory)));
      categories.Update(FolderType.ReportsFolder, (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.ConfiguratorCategory, BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(context.Categories.Get(FolderType.InquiriesFolder))));
    }))));
  }

  public class Conditions : BoundedTo<APInvoiceEntry, APInvoice>.Condition.Pack
  {
    private readonly APSetupDefinition _Definition = APSetupDefinition.GetSlot();

    public BoundedTo<APInvoiceEntry, APInvoice>.Condition IsNotOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<APInvoiceEntry, APInvoice>.Condition>) (c => c.FromBql<BqlOperand<APInvoice.hold, IBqlBool>.IsEqual<False>>()), nameof (IsNotOnHold));
      }
    }

    public BoundedTo<APInvoiceEntry, APInvoice>.Condition IsReserved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<APInvoiceEntry, APInvoice>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.hold, Equal<True>>>>>.And<BqlOperand<APInvoice.released, IBqlBool>.IsEqual<True>>>()), nameof (IsReserved));
      }
    }

    public BoundedTo<APInvoiceEntry, APInvoice>.Condition IsOpen
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<APInvoiceEntry, APInvoice>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.openDoc, Equal<True>>>>>.And<BqlOperand<APInvoice.released, IBqlBool>.IsEqual<True>>>()), nameof (IsOpen));
      }
    }

    public BoundedTo<APInvoiceEntry, APInvoice>.Condition IsClosed
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<APInvoiceEntry, APInvoice>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.openDoc, Equal<False>>>>>.And<BqlOperand<APInvoice.released, IBqlBool>.IsEqual<True>>>()), nameof (IsClosed));
      }
    }

    public BoundedTo<APInvoiceEntry, APInvoice>.Condition IsZeroBalance
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<APInvoiceEntry, APInvoice>.Condition>) (c => c.FromBql<BqlOperand<APInvoice.docBal, IBqlDecimal>.IsEqual<decimal0>>()), nameof (IsZeroBalance));
      }
    }

    public BoundedTo<APInvoiceEntry, APInvoice>.Condition IsPrepayment
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<APInvoiceEntry, APInvoice>.Condition>) (c => c.FromBql<BqlOperand<APInvoice.docType, IBqlString>.IsEqual<APDocType.prepayment>>()), nameof (IsPrepayment));
      }
    }

    public BoundedTo<APInvoiceEntry, APInvoice>.Condition IsNotDebitAdjustment
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<APInvoiceEntry, APInvoice>.Condition>) (c => c.FromBql<BqlOperand<APInvoice.docType, IBqlString>.IsNotEqual<APDocType.debitAdj>>()), nameof (IsNotDebitAdjustment));
      }
    }

    public BoundedTo<APInvoiceEntry, APInvoice>.Condition IsNotAllowRefund
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<APInvoiceEntry, APInvoice>.Condition>) (c => c.FromBql<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.docType, NotEqual<APDocType.debitAdj>>>>, PX.Data.Or<BqlOperand<APRegister.curyRetainageTotal, IBqlDecimal>.IsGreater<decimal0>>>>.Or<BqlOperand<APInvoice.isRetainageDocument, IBqlBool>.IsEqual<True>>>()), nameof (IsNotAllowRefund));
      }
    }

    public BoundedTo<APInvoiceEntry, APInvoice>.Condition IsNotAllowReclasify
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<APInvoiceEntry, APInvoice>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.docType, Equal<APDocType.prepayment>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.docType, Equal<APDocType.debitAdj>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APRegister.curyRetainageTotal, Greater<decimal0>>>>>.Or<BqlOperand<APInvoice.isRetainageDocument, IBqlBool>.IsEqual<True>>>>>()), nameof (IsNotAllowReclasify));
      }
    }

    public BoundedTo<APInvoiceEntry, APInvoice>.Condition IsNotAllowVoidPrepayment
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<APInvoiceEntry, APInvoice>.Condition>) (c => c.FromBql<BqlOperand<APInvoice.docType, IBqlString>.IsNotEqual<APDocType.prepayment>>()), nameof (IsNotAllowVoidPrepayment));
      }
    }

    public BoundedTo<APInvoiceEntry, APInvoice>.Condition IsNotAllowVoidInvoice
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<APInvoiceEntry, APInvoice>.Condition>) (c => c.FromBql<BqlOperand<APInvoice.docType, IBqlString>.IsNotIn<APDocType.invoice, APDocType.creditAdj, APDocType.debitAdj>>()), nameof (IsNotAllowVoidInvoice));
      }
    }

    public BoundedTo<APInvoiceEntry, APInvoice>.Condition IsRetainage
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<APInvoiceEntry, APInvoice>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.isRetainageDocument, Equal<True>>>>>.Or<BqlOperand<APInvoice.retainageApply, IBqlBool>.IsEqual<True>>>()), nameof (IsRetainage));
      }
    }

    public BoundedTo<APInvoiceEntry, APInvoice>.Condition IsNotAllowRecalcPrice
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<APInvoiceEntry, APInvoice>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.pendingPPD, Equal<True>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.docType, Equal<APDocType.debitAdj>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APRegister.curyRetainageTotal, Greater<decimal0>>>>>.Or<BqlOperand<APInvoice.isRetainageDocument, IBqlBool>.IsEqual<True>>>>>()), nameof (IsNotAllowRecalcPrice));
      }
    }

    public BoundedTo<APInvoiceEntry, APInvoice>.Condition IsMigrationMode
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<APInvoiceEntry, APInvoice>.Condition>) (c => !this._Definition.MigrationMode.GetValueOrDefault() ? c.FromBql<BqlOperand<True, IBqlBool>.IsEqual<False>>() : c.FromBql<BqlOperand<True, IBqlBool>.IsEqual<True>>()), nameof (IsMigrationMode));
      }
    }

    public BoundedTo<APInvoiceEntry, APInvoice>.Condition IsPrepaymentInvoice
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<APInvoiceEntry, APInvoice>.Condition>) (c => c.FromBql<BqlOperand<APInvoice.docType, IBqlString>.IsEqual<APDocType.prepaymentInvoice>>()), nameof (IsPrepaymentInvoice));
      }
    }

    public BoundedTo<APInvoiceEntry, APInvoice>.Condition IsPrepaymentInvoiceReversing
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<APInvoiceEntry, APInvoice>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APInvoice.docType, Equal<APDocType.debitAdj>>>>>.And<BqlOperand<APInvoice.origDocType, IBqlString>.IsEqual<APDocType.prepaymentInvoice>>>()), nameof (IsPrepaymentInvoiceReversing));
      }
    }
  }

  public static class ActionCategoryNames
  {
    public const string Processing = "Processing";
    public const string Approval = "Approval";
    public const string Corrections = "Corrections";
    public const string CustomOther = "CustomOther";
  }

  public static class ActionCategory
  {
    public const string Processing = "Processing";
    public const string Approval = "Approval";
    public const string Corrections = "Corrections";
    public const string Other = "Other";
  }
}
