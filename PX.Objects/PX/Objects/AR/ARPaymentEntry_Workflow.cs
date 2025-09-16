// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPaymentEntry_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.AR.GraphExtensions;
using PX.Objects.CS;
using System;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.AR;

public class ARPaymentEntry_Workflow : PXGraphExtension<
#nullable disable
ARPaymentEntry>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    ARPaymentEntry_Workflow.Configure(config.GetScreenConfigurationContext<ARPaymentEntry, ARPayment>());
  }

  [PXWorkflowDependsOnType(new Type[] {typeof (ARSetup)})]
  protected static void Configure(WorkflowContext<ARPaymentEntry, ARPayment> context)
  {
    ARSetupDefinition.GetSlot();
    ARPaymentEntry_Workflow.Conditions conditions = context.Conditions.GetPack<ARPaymentEntry_Workflow.Conditions>();
    BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("ProcessingID", (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IConfigured>) (category => (BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IConfigured cardProcessingCategory = context.Categories.CreateNew("CardProcessingID", (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IConfigured>) (category => (BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IConfigured) category.DisplayName("Card Processing")));
    BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IConfigured correctionsCategory = context.Categories.CreateNew("CorrectionsID", (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IConfigured>) (category => (BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IConfigured) category.DisplayName("Corrections")));
    BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("ApprovalID", (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IConfigured>) (category => (BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IConfigured otherCategory = context.Categories.CreateNew("OtherID", (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IConfigured>) (category => (BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IConfigured) category.DisplayName("Other")));
    context.AddScreenConfigurationFor((Func<BoundedTo<ARPaymentEntry, ARPayment>.ScreenConfiguration.IStartConfigScreen, BoundedTo<ARPaymentEntry, ARPayment>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<ARPaymentEntry, ARPayment>.ScreenConfiguration.IConfigured) ((BoundedTo<ARPaymentEntry, ARPayment>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<ARPayment.status>().AddDefaultFlow((Func<BoundedTo<ARPaymentEntry, ARPayment>.Workflow.INeedStatesFlow, BoundedTo<ARPaymentEntry, ARPayment>.Workflow.IConfigured>) (flow => (BoundedTo<ARPaymentEntry, ARPayment>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<ARPaymentEntry, PXAutoAction<ARPayment>>>) (g => g.initializeState))));
      fss.AddSequence<ARDocStatus.HoldToBalance>((Func<BoundedTo<ARPaymentEntry, ARPayment>.Sequence.INeedStatesThenConditionsConfig, BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured>) (seq => (BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured) ((BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.INeedAnyConfigState) seq.WithStates((Action<BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IContainerFillerStates>) (sss =>
      {
        sss.Add<ARDocStatus.hold>((Func<BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured) ((BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig) flowState.IsSkippedWhen(conditions.IsNotOnHold)).WithActions((Action<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.releaseFromHold), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) (a => (BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
          actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.printAREdit), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.customerDocuments), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
          actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.voidCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        }))).WithEventHandlers((Action<BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<ARPaymentEntry, PXWorkflowEventHandler<ARPayment>>>) (g => g.OnVoidDocument))))));
        sss.Add<ARDocStatus.cCHold>((Func<BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured) ((BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig) flowState.IsSkippedWhen(conditions.IsCCProcessed)).WithActions((Action<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (a => a.putOnHold), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
          actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.captureCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) (a => (BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured) a.WithConnotation((ActionConnotation) 3)));
          actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.authorizeCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
          actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.voidCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
          actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.creditCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
          actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.recordCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
          actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.captureOnlyCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
          actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.validateCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.release), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (a => a.voidCheck), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        }))).WithEventHandlers((Action<BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
        {
          handlers.Add((Expression<Func<ARPaymentEntry, PXWorkflowEventHandler<ARPayment>>>) (g => g.OnUpdateStatus));
          handlers.Add((Expression<Func<ARPaymentEntry, PXWorkflowEventHandler<ARPayment>>>) (g => g.OnReleaseDocument));
          handlers.Add((Expression<Func<ARPaymentEntry, PXWorkflowEventHandler<ARPayment>>>) (g => g.OnVoidDocument));
        }))));
        sss.Add<ARDocStatus.balanced>((Func<BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.release), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) (a => (BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
          actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.captureCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
          actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.authorizeCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
          actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.voidCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
          actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.creditCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
          actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.recordCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
          actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.captureOnlyCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
          actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.validateCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.putOnHold), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.printAREdit), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.customerDocuments), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        }))));
      }))).WithEventHandlers((Action<BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<ARPaymentEntry, PXWorkflowEventHandler<ARPayment>>>) (g => g.OnUpdateStatus));
        handlers.Add((Expression<Func<ARPaymentEntry, PXWorkflowEventHandler<ARPayment>>>) (g => g.OnReleaseDocument));
        handlers.Add((Expression<Func<ARPaymentEntry, PXWorkflowEventHandler<ARPayment>>>) (g => g.OnVoidDocument));
      }))));
      fss.Add<ARDocStatus.pendingPayment>((Func<BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.printARRegister), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.customerDocuments), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
      }))));
      fss.Add<ARDocStatus.open>((Func<BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured) ((BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.release), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) (a => (BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.voidCheck), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) (a => (BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.putOnHold), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.captureCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.authorizeCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.voidCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.creditCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.recordCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.captureOnlyCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.validateCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.reverseApplication), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.printARRegister), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.printAREdit), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.customerDocuments), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARPaymentEntry, PXAutoAction<ARPayment>>>) (g => g.initializeState), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) (act => (BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured) act.IsAutoAction((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) null)));
      }))).WithEventHandlers((Action<BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<ARPaymentEntry, PXWorkflowEventHandler<ARPayment>>>) (g => g.OnCloseDocument));
        handlers.Add((Expression<Func<ARPaymentEntry, PXWorkflowEventHandler<ARPayment>>>) (g => g.OnVoidDocument));
      }))));
      fss.Add<ARDocStatus.unapplied>((Func<BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured) ((BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.release), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) (a => (BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.voidCheck), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) (a => (BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.putOnHold), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.captureCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.authorizeCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.voidCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.creditCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.recordCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.captureOnlyCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.validateCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.reverseApplication), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.printARRegister), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.printAREdit), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.customerDocuments), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARPaymentEntry, PXAutoAction<ARPayment>>>) (g => g.initializeState), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) (act => (BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured) act.IsAutoAction((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) null)));
      }))).WithEventHandlers((Action<BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<ARPaymentEntry, PXWorkflowEventHandler<ARPayment>>>) (g => g.OnCloseDocument));
        handlers.Add((Expression<Func<ARPaymentEntry, PXWorkflowEventHandler<ARPayment>>>) (g => g.OnVoidDocument));
      }))));
      fss.Add<ARDocStatus.reserved>((Func<BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured) ((BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.releaseFromHold), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) (a => (BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.printARRegister), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.customerDocuments), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.voidCheck), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<ARPaymentEntry, PXWorkflowEventHandler<ARPayment>>>) (g => g.OnVoidDocument))))));
      fss.Add<ARDocStatus.closed>((Func<BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured) ((BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.voidCheck), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) (a => (BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.captureCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.authorizeCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.voidCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.creditCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.recordCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.captureOnlyCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.validateCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.reverseApplication), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.printARRegister), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.customerDocuments), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<ARPaymentEntry, PXWorkflowEventHandler<ARPayment>>>) (g => g.OnOpenDocument));
        handlers.Add((Expression<Func<ARPaymentEntry, PXWorkflowEventHandler<ARPayment>>>) (g => g.OnVoidDocument));
      }))));
      fss.Add<ARDocStatus.voided>((Func<BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.printARRegister), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.customerDocuments), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
      }))));
    })).WithTransitions((Action<BoundedTo<ARPaymentEntry, ARPayment>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<ARPaymentEntry, ARPayment>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.HoldToBalance>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.initializeState))))));
      transitions.AddGroupFrom<ARDocStatus.HoldToBalance>((Action<BoundedTo<ARPaymentEntry, ARPayment>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.HoldToBalance>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXWorkflowEventHandlerBase<ARPayment>>>) (g => g.OnUpdateStatus))));
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t =>
        {
          BoundedTo<ARPaymentEntry, ARPayment>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<ARDocStatus.open>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXWorkflowEventHandlerBase<ARPayment>>>) (g => g.OnReleaseDocument));
          BoundedTo<ARPaymentEntry, ARPayment>.Condition isOpen = conditions.IsOpen;
          BoundedTo<ARPaymentEntry, ARPayment>.Condition condition = BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_False(isOpen) ? isOpen : BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_BitwiseAnd(isOpen, BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_LogicalNot(conditions.IsPrepaymentInvoice));
          return (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) condition);
        }));
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t =>
        {
          BoundedTo<ARPaymentEntry, ARPayment>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<ARDocStatus.unapplied>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXWorkflowEventHandlerBase<ARPayment>>>) (g => g.OnReleaseDocument));
          BoundedTo<ARPaymentEntry, ARPayment>.Condition isOpen = conditions.IsOpen;
          BoundedTo<ARPaymentEntry, ARPayment>.Condition condition = BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_False(isOpen) ? isOpen : BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_BitwiseAnd(isOpen, conditions.IsPrepaymentInvoice);
          return (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) condition);
        }));
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.closed>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXWorkflowEventHandlerBase<ARPayment>>>) (g => g.OnReleaseDocument)).When((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsClosed)));
      }));
      transitions.AddGroupFrom<ARDocStatus.hold>((Action<BoundedTo<ARPaymentEntry, ARPayment>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.voided>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXWorkflowEventHandlerBase<ARPayment>>>) (g => g.OnVoidDocument))))));
      transitions.AddGroupFrom<ARDocStatus.cCHold>((Action<BoundedTo<ARPaymentEntry, ARPayment>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.voided>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXWorkflowEventHandlerBase<ARPayment>>>) (g => g.OnVoidDocument))))));
      transitions.AddGroupFrom<ARDocStatus.balanced>((Action<BoundedTo<ARPaymentEntry, ARPayment>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.voided>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXWorkflowEventHandlerBase<ARPayment>>>) (g => g.OnVoidDocument))))));
      transitions.AddGroupFrom<ARDocStatus.open>((Action<BoundedTo<ARPaymentEntry, ARPayment>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.reserved>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.putOnHold)).WithFieldAssignments((Action<BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IContainerFillerFields>) (fas => fas.Add<ARRegister.rejected>((Func<BoundedTo<ARPaymentEntry, ARPayment>.Assignment.INeedRightOperand, BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IConfigured>) (f => (BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.voided>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXWorkflowEventHandlerBase<ARPayment>>>) (g => g.OnVoidDocument))));
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.closed>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXWorkflowEventHandlerBase<ARPayment>>>) (g => g.OnCloseDocument))));
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.closed>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.initializeState)).When((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsClosed)));
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.closed>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXWorkflowEventHandlerBase<ARPayment>>>) (g => g.OnReleaseDocument)).When((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsClosed)));
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.voided>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXWorkflowEventHandlerBase<ARPayment>>>) (g => g.OnReleaseDocument)).When((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsVoided)));
      }));
      transitions.AddGroupFrom<ARDocStatus.unapplied>((Action<BoundedTo<ARPaymentEntry, ARPayment>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.reserved>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.putOnHold)).WithFieldAssignments((Action<BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IContainerFillerFields>) (fas => fas.Add<ARRegister.rejected>((Func<BoundedTo<ARPaymentEntry, ARPayment>.Assignment.INeedRightOperand, BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IConfigured>) (f => (BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.voided>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXWorkflowEventHandlerBase<ARPayment>>>) (g => g.OnVoidDocument))));
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.closed>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXWorkflowEventHandlerBase<ARPayment>>>) (g => g.OnCloseDocument))));
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.closed>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.initializeState)).When((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsClosed)));
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.closed>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXWorkflowEventHandlerBase<ARPayment>>>) (g => g.OnReleaseDocument)).When((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsClosed)));
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.voided>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXWorkflowEventHandlerBase<ARPayment>>>) (g => g.OnReleaseDocument)).When((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsVoided)));
      }));
      transitions.AddGroupFrom<ARDocStatus.reserved>((Action<BoundedTo<ARPaymentEntry, ARPayment>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.open>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.releaseFromHold)).When((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_LogicalNot(conditions.IsPrepaymentInvoice))));
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.unapplied>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.releaseFromHold)).When((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsPrepaymentInvoice)));
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.voided>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXWorkflowEventHandlerBase<ARPayment>>>) (g => g.OnVoidDocument))));
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.closed>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXWorkflowEventHandlerBase<ARPayment>>>) (g => g.OnReleaseDocument)).When((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsClosed)));
      }));
      transitions.AddGroupFrom<ARDocStatus.closed>((Action<BoundedTo<ARPaymentEntry, ARPayment>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.voided>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXWorkflowEventHandlerBase<ARPayment>>>) (g => g.OnVoidDocument))));
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.open>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXWorkflowEventHandlerBase<ARPayment>>>) (g => g.OnOpenDocument)).When((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_LogicalNot(conditions.IsPrepaymentInvoice))));
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.unapplied>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXWorkflowEventHandlerBase<ARPayment>>>) (g => g.OnOpenDocument)).When((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsPrepaymentInvoice)));
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.open>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.reverseApplication)).When((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_LogicalNot(conditions.IsPrepaymentInvoice)).DoesNotPersist()));
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.unapplied>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.reverseApplication)).When((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsPrepaymentInvoice).DoesNotPersist()));
        ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.voided>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXWorkflowEventHandlerBase<ARPayment>>>) (g => g.OnReleaseDocument)).When((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsVoided)));
      }));
    })))).WithActions((Action<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.initializeState), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured>) (a => (BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.releaseFromHold), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IContainerFillerFields>) (fas => fas.Add<ARPayment.hold>((Func<BoundedTo<ARPaymentEntry, ARPayment>.Assignment.INeedRightOperand, BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IConfigured>) (f => (BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.putOnHold), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IContainerFillerFields>) (fas => fas.Add<ARPayment.hold>((Func<BoundedTo<ARPaymentEntry, ARPayment>.Assignment.INeedRightOperand, BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IConfigured>) (f => (BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IConfigured) f.SetFromValue((object) true))))).IsDisabledWhen((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsNoDoc)));
      actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.authorizeCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured>) (a =>
      {
        BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = a.WithCategory(cardProcessingCategory);
        BoundedTo<ARPaymentEntry, ARPayment>.Condition isCreditMemo = conditions.IsCreditMemo;
        BoundedTo<ARPaymentEntry, ARPayment>.Condition condition16 = BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_True(isCreditMemo) ? isCreditMemo : BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_BitwiseOr(isCreditMemo, conditions.IsBalanceWO);
        BoundedTo<ARPaymentEntry, ARPayment>.Condition condition17 = BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_True(condition16) ? condition16 : BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_BitwiseOr(condition16, conditions.IsPrepaymentInvoice);
        return (BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) condition17);
      }));
      actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.captureCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured>) (a =>
      {
        BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = a.WithCategory(cardProcessingCategory);
        BoundedTo<ARPaymentEntry, ARPayment>.Condition isNotCapturable = conditions.IsNotCapturable;
        BoundedTo<ARPaymentEntry, ARPayment>.Condition condition18 = BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_True(isNotCapturable) ? isNotCapturable : BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_BitwiseOr(isNotCapturable, conditions.IsCreditMemo);
        BoundedTo<ARPaymentEntry, ARPayment>.Condition condition19 = BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_True(condition18) ? condition18 : BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_BitwiseOr(condition18, conditions.IsBalanceWO);
        BoundedTo<ARPaymentEntry, ARPayment>.Condition condition20 = BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_True(condition19) ? condition19 : BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_BitwiseOr(condition19, conditions.IsPrepaymentInvoice);
        return (BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) condition20);
      }));
      actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.voidCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured>) (a =>
      {
        BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = a.WithCategory(cardProcessingCategory);
        BoundedTo<ARPaymentEntry, ARPayment>.Condition isCreditMemo = conditions.IsCreditMemo;
        BoundedTo<ARPaymentEntry, ARPayment>.Condition condition21 = BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_True(isCreditMemo) ? isCreditMemo : BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_BitwiseOr(isCreditMemo, conditions.IsBalanceWO);
        BoundedTo<ARPaymentEntry, ARPayment>.Condition condition22 = BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_True(condition21) ? condition21 : BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_BitwiseOr(condition21, conditions.IsPrepaymentInvoice);
        return (BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) condition22);
      }));
      actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.creditCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured>) (a =>
      {
        BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = a.WithCategory(cardProcessingCategory);
        BoundedTo<ARPaymentEntry, ARPayment>.Condition isCreditMemo = conditions.IsCreditMemo;
        BoundedTo<ARPaymentEntry, ARPayment>.Condition condition23 = BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_True(isCreditMemo) ? isCreditMemo : BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_BitwiseOr(isCreditMemo, conditions.IsBalanceWO);
        BoundedTo<ARPaymentEntry, ARPayment>.Condition condition24 = BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_True(condition23) ? condition23 : BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_BitwiseOr(condition23, conditions.IsPrepaymentInvoice);
        return (BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) condition24);
      }));
      actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.recordCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured>) (a =>
      {
        BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = a.WithCategory(cardProcessingCategory);
        BoundedTo<ARPaymentEntry, ARPayment>.Condition isCreditMemo = conditions.IsCreditMemo;
        BoundedTo<ARPaymentEntry, ARPayment>.Condition condition25 = BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_True(isCreditMemo) ? isCreditMemo : BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_BitwiseOr(isCreditMemo, conditions.IsBalanceWO);
        BoundedTo<ARPaymentEntry, ARPayment>.Condition condition26 = BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_True(condition25) ? condition25 : BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_BitwiseOr(condition25, conditions.IsPrepaymentInvoice);
        return (BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) condition26);
      }));
      actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.captureOnlyCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured>) (a =>
      {
        BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = a.WithCategory(cardProcessingCategory);
        BoundedTo<ARPaymentEntry, ARPayment>.Condition isCreditMemo = conditions.IsCreditMemo;
        BoundedTo<ARPaymentEntry, ARPayment>.Condition condition27 = BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_True(isCreditMemo) ? isCreditMemo : BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_BitwiseOr(isCreditMemo, conditions.IsBalanceWO);
        BoundedTo<ARPaymentEntry, ARPayment>.Condition condition28 = BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_True(condition27) ? condition27 : BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_BitwiseOr(condition27, conditions.IsPrepaymentInvoice);
        return (BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) condition28);
      }));
      actions.Add<ARPaymentEntryPaymentTransaction>((Expression<Func<ARPaymentEntryPaymentTransaction, PXAction<ARPayment>>>) (a => a.validateCCPayment), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured>) (a =>
      {
        BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = a.WithCategory(cardProcessingCategory);
        BoundedTo<ARPaymentEntry, ARPayment>.Condition isCreditMemo = conditions.IsCreditMemo;
        BoundedTo<ARPaymentEntry, ARPayment>.Condition condition29 = BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_True(isCreditMemo) ? isCreditMemo : BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_BitwiseOr(isCreditMemo, conditions.IsBalanceWO);
        BoundedTo<ARPaymentEntry, ARPayment>.Condition condition30 = BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_True(condition29) ? condition29 : BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_BitwiseOr(condition29, conditions.IsPrepaymentInvoice);
        return (BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) condition30);
      }));
      actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.release), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).IsDisabledWhen((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsCCIntegrated)));
      actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.refund), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).IsDisabledWhen((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsNotRefundable)));
      actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.voidCheck), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured) c.WithCategory(correctionsCategory).IsHiddenWhen((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsNotVoidable)));
      actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.reverseApplication), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured>) (g => (BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured) g.WithPersistOptions((ActionPersistOptions) 1)));
      actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.customerDocuments), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured) c.WithCategory((PredefinedCategory) 1).IsDisabledWhen((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsNoDoc)));
      actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.printAREdit), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured) c.WithCategory((PredefinedCategory) 2).IsDisabledWhen((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsNoDoc).IsHiddenWhen((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsPrepaymentInvoice)));
      actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.printARRegister), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured) c.WithCategory((PredefinedCategory) 2)));
    })).WithHandlers((Action<BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers =>
    {
      handlers.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<ARPayment>) ((BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<ARPayment>) ((BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.INeedSubscriber<ARPayment>) handler.WithTargetOf<ARPayment>().OfEntityEvent<ARPayment.Events>((Expression<Func<ARPayment.Events, PXEntityEvent<ARPayment>>>) (e => e.ReleaseDocument))).Is((Expression<Func<ARPayment, PXWorkflowEventHandler<ARPayment, ARPayment>>>) (g => g.OnReleaseDocument))).UsesTargetAsPrimaryEntity()).WithUpcastTo<ARRegister>()));
      handlers.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<ARPayment>) ((BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<ARPayment>) ((BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.INeedSubscriber<ARPayment>) handler.WithTargetOf<ARPayment>().OfEntityEvent<ARPayment.Events>((Expression<Func<ARPayment.Events, PXEntityEvent<ARPayment>>>) (e => e.OpenDocument))).Is((Expression<Func<ARPayment, PXWorkflowEventHandler<ARPayment, ARPayment>>>) (g => g.OnOpenDocument))).UsesTargetAsPrimaryEntity()).WithUpcastTo<ARRegister>()));
      handlers.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<ARPayment>) ((BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<ARPayment>) ((BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.INeedSubscriber<ARPayment>) handler.WithTargetOf<ARPayment>().OfEntityEvent<ARPayment.Events>((Expression<Func<ARPayment.Events, PXEntityEvent<ARPayment>>>) (e => e.CloseDocument))).Is((Expression<Func<ARPayment, PXWorkflowEventHandler<ARPayment, ARPayment>>>) (g => g.OnCloseDocument))).UsesTargetAsPrimaryEntity()).WithUpcastTo<ARRegister>()));
      handlers.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<ARPayment>) ((BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<ARPayment>) ((BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.INeedSubscriber<ARPayment>) handler.WithTargetOf<ARPayment>().OfEntityEvent<ARPayment.Events>((Expression<Func<ARPayment.Events, PXEntityEvent<ARPayment>>>) (e => e.VoidDocument))).Is((Expression<Func<ARPayment, PXWorkflowEventHandler<ARPayment, ARPayment>>>) (g => g.OnVoidDocument))).UsesTargetAsPrimaryEntity()).WithUpcastTo<ARRegister>()));
      handlers.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<ARPayment>) ((BoundedTo<ARPaymentEntry, ARPayment>.WorkflowEventHandlerDefinition.INeedSubscriber<ARPayment>) handler.WithTargetOf<ARPayment>().OfFieldsUpdated<TypeArrayOf<IBqlField>.FilledWith<ARPayment.hold, ARPayment.pendingProcessing>>()).Is((Expression<Func<ARPayment, PXWorkflowEventHandler<ARPayment, ARPayment>>>) (g => g.OnUpdateStatus))).UsesTargetAsPrimaryEntity()));
    })).WithCategories((Action<BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(cardProcessingCategory);
      categories.Add(correctionsCategory);
      categories.Add(approvalCategory);
      categories.Add(otherCategory);
      categories.Update((FolderType) 1, (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.ConfiguratorCategory, BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(otherCategory)));
      categories.Update((FolderType) 2, (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.ConfiguratorCategory, BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter((FolderType) 1)));
    }))));
  }

  public static class Constants
  {
    public const string NoDocNbr = " <SELECT>";

    public class noDocNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ARPaymentEntry_Workflow.Constants.noDocNbr>
    {
      public noDocNbr()
        : base(" <SELECT>")
      {
      }
    }
  }

  public class Conditions : BoundedTo<ARPaymentEntry, ARPayment>.Condition.Pack
  {
    private readonly ARSetupDefinition _Definition = ARSetupDefinition.GetSlot();

    public BoundedTo<ARPaymentEntry, ARPayment>.Condition IsNotOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARPaymentEntry, ARPayment>.Condition.ConditionBuilder, BoundedTo<ARPaymentEntry, ARPayment>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARPayment.hold, Equal<False>>>>>.And<BqlOperand<ARPayment.released, IBqlBool>.IsEqual<False>>>()), nameof (IsNotOnHold));
      }
    }

    public BoundedTo<ARPaymentEntry, ARPayment>.Condition IsOpen
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARPaymentEntry, ARPayment>.Condition.ConditionBuilder, BoundedTo<ARPaymentEntry, ARPayment>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARPayment.openDoc, Equal<True>>>>>.And<BqlOperand<ARPayment.released, IBqlBool>.IsEqual<True>>>()), nameof (IsOpen));
      }
    }

    public BoundedTo<ARPaymentEntry, ARPayment>.Condition IsClosed
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARPaymentEntry, ARPayment>.Condition.ConditionBuilder, BoundedTo<ARPaymentEntry, ARPayment>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARPayment.openDoc, Equal<False>>>>>.And<BqlOperand<ARPayment.released, IBqlBool>.IsEqual<True>>>()), nameof (IsClosed));
      }
    }

    public BoundedTo<ARPaymentEntry, ARPayment>.Condition IsNotVoidable
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARPaymentEntry, ARPayment>.Condition.ConditionBuilder, BoundedTo<ARPaymentEntry, ARPayment>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARPayment.docType, In3<ARDocType.voidPayment, ARDocType.voidRefund, ARDocType.creditMemo, ARDocType.prepaymentInvoice>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARPayment.docType, Equal<ARDocType.smallBalanceWO>>>>>.And<BqlOperand<ARPayment.status, IBqlString>.IsEqual<ARDocStatus.reserved>>>>()), nameof (IsNotVoidable));
      }
    }

    public BoundedTo<ARPaymentEntry, ARPayment>.Condition IsNotRefundable
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARPaymentEntry, ARPayment>.Condition.ConditionBuilder, BoundedTo<ARPaymentEntry, ARPayment>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARPayment.docType, NotIn3<ARDocType.payment, ARDocType.prepayment, ARDocType.creditMemo, ARDocType.prepaymentInvoice>>>>, Or<BqlOperand<ARPayment.curyUnappliedBal, IBqlDecimal>.IsLessEqual<decimal0>>>, Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARPayment.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARPayment.pendingPayment, IBqlBool>.IsEqual<True>>>>>.Or<BqlOperand<ARPayment.released, IBqlBool>.IsEqual<False>>>()), nameof (IsNotRefundable));
      }
    }

    public BoundedTo<ARPaymentEntry, ARPayment>.Condition IsCCIntegrated
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARPaymentEntry, ARPayment>.Condition.ConditionBuilder, BoundedTo<ARPaymentEntry, ARPayment>.Condition>) (c => !PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>() || !this._Definition.IntegratedCCProcessing.GetValueOrDefault() || this._Definition.MigrationMode.GetValueOrDefault() ? c.FromBql<BqlOperand<True, IBqlBool>.IsEqual<False>>() : c.FromBql<BqlOperand<ARPayment.status, IBqlString>.IsEqual<ARDocStatus.cCHold>>()), nameof (IsCCIntegrated));
      }
    }

    public BoundedTo<ARPaymentEntry, ARPayment>.Condition IsCCProcessed
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARPaymentEntry, ARPayment>.Condition.ConditionBuilder, BoundedTo<ARPaymentEntry, ARPayment>.Condition>) (c => c.FromBql<BqlOperand<ARRegister.pendingProcessing, IBqlBool>.IsEqual<False>>()), nameof (IsCCProcessed));
      }
    }

    public BoundedTo<ARPaymentEntry, ARPayment>.Condition IsVoided
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARPaymentEntry, ARPayment>.Condition.ConditionBuilder, BoundedTo<ARPaymentEntry, ARPayment>.Condition>) (c => c.FromBql<BqlOperand<ARPayment.voided, IBqlBool>.IsEqual<True>>()), nameof (IsVoided));
      }
    }

    public BoundedTo<ARPaymentEntry, ARPayment>.Condition IsNotCapturable
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARPaymentEntry, ARPayment>.Condition.ConditionBuilder, BoundedTo<ARPaymentEntry, ARPayment>.Condition>) (c => c.FromBql<BqlOperand<ARPayment.docType, IBqlString>.IsIn<ARDocType.voidPayment, ARDocType.refund, ARDocType.voidRefund, ARDocType.cashReturn>>()), nameof (IsNotCapturable));
      }
    }

    public BoundedTo<ARPaymentEntry, ARPayment>.Condition IsCreditMemo
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARPaymentEntry, ARPayment>.Condition.ConditionBuilder, BoundedTo<ARPaymentEntry, ARPayment>.Condition>) (c => c.FromBql<BqlOperand<ARPayment.docType, IBqlString>.IsEqual<ARDocType.creditMemo>>()), nameof (IsCreditMemo));
      }
    }

    public BoundedTo<ARPaymentEntry, ARPayment>.Condition IsBalanceWO
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARPaymentEntry, ARPayment>.Condition.ConditionBuilder, BoundedTo<ARPaymentEntry, ARPayment>.Condition>) (c => c.FromBql<BqlOperand<ARPayment.docType, IBqlString>.IsEqual<ARDocType.smallBalanceWO>>()), nameof (IsBalanceWO));
      }
    }

    public BoundedTo<ARPaymentEntry, ARPayment>.Condition IsPrepaymentInvoice
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARPaymentEntry, ARPayment>.Condition.ConditionBuilder, BoundedTo<ARPaymentEntry, ARPayment>.Condition>) (c => c.FromBql<BqlOperand<ARPayment.docType, IBqlString>.IsEqual<ARDocType.prepaymentInvoice>>()), nameof (IsPrepaymentInvoice));
      }
    }

    public BoundedTo<ARPaymentEntry, ARPayment>.Condition IsNoDoc
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARPaymentEntry, ARPayment>.Condition.ConditionBuilder, BoundedTo<ARPaymentEntry, ARPayment>.Condition>) (c => c.FromBql<BqlOperand<ARPayment.refNbr, IBqlString>.IsEqual<ARPaymentEntry_Workflow.Constants.noDocNbr>>()), nameof (IsNoDoc));
      }
    }
  }

  public static class CategoryNames
  {
    public const string Processing = "Processing";
    public const string CardProcessing = "Card Processing";
    public const string Corrections = "Corrections";
    public const string Approval = "Approval";
    public const string Other = "Other";
  }

  public static class CategoryID
  {
    public const string Processing = "ProcessingID";
    public const string CardProcessing = "CardProcessingID";
    public const string Corrections = "CorrectionsID";
    public const string Approval = "ApprovalID";
    public const string Other = "OtherID";
  }
}
