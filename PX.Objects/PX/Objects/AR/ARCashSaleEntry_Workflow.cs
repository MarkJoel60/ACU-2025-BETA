// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARCashSaleEntry_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.AR.GraphExtensions;
using PX.Objects.AR.Standalone;
using PX.Objects.CS;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.AR;

public class ARCashSaleEntry_Workflow : PXGraphExtension<ARCashSaleEntry>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    ARCashSaleEntry_Workflow.Configure(config.GetScreenConfigurationContext<ARCashSaleEntry, ARCashSale>());
  }

  [PXWorkflowDependsOnType(new Type[] {typeof (ARSetup)})]
  protected static void Configure(
    WorkflowContext<ARCashSaleEntry, ARCashSale> context)
  {
    ARSetupDefinition.GetSlot();
    ARCashSaleEntry_Workflow.Conditions conditions = context.Conditions.GetPack<ARCashSaleEntry_Workflow.Conditions>();
    BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("ProcessingID", (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IConfigured>) (category => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IConfigured cardProcessingCategory = context.Categories.CreateNew("CardProcessingID", (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IConfigured>) (category => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IConfigured) category.DisplayName("Card Processing")));
    BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IConfigured correctionsCategory = context.Categories.CreateNew("CorrectionsID", (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IConfigured>) (category => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IConfigured) category.DisplayName("Corrections")));
    BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IConfigured printingAndEmailingCategory = context.Categories.CreateNew("PrintingAndEmailingID", (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IConfigured>) (category => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IConfigured) category.DisplayName("Printing and Emailing")));
    BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("ApprovalID", (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IConfigured>) (category => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IConfigured otherCategory = context.Categories.CreateNew("OtherID", (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IConfigured>) (category => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IConfigured) category.DisplayName("Other")));
    context.AddScreenConfigurationFor((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ScreenConfiguration.IStartConfigScreen, BoundedTo<ARCashSaleEntry, ARCashSale>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<ARCashSaleEntry, ARCashSale>.ScreenConfiguration.IConfigured) ((BoundedTo<ARCashSaleEntry, ARCashSale>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<ARCashSale.status>().AddDefaultFlow((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Workflow.INeedStatesFlow, BoundedTo<ARCashSaleEntry, ARCashSale>.Workflow.IConfigured>) (flow => (BoundedTo<ARCashSaleEntry, ARCashSale>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<ARCashSaleEntry, PXAutoAction<ARCashSale>>>) (g => g.initializeState))));
      fss.AddSequence<ARDocStatus.HoldToBalance>((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Sequence.INeedStatesThenConditionsConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.IConfigured>) (seq => (BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.IConfigured) ((BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.INeedAnyConfigState) ((BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.INeedAnyConfigState) seq.WithStates((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.IContainerFillerStates>) (sss =>
      {
        sss.Add<ARDocStatus.hold>((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.IConfigured) ((BoundedTo<ARCashSaleEntry, ARCashSale>.FlowState.INeedAnyFlowStateConfig) flowState.IsSkippedWhen(conditions.IsNotOnHold)).WithActions((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.releaseFromHold), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) (a => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
          actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.sendEmail), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.printAREdit), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.customerDocuments), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.printInvoice), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
        }))));
        sss.Add<ARDocStatus.cCHold>((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.IConfigured) ((BoundedTo<ARCashSaleEntry, ARCashSale>.FlowState.INeedAnyFlowStateConfig) flowState.IsSkippedWhen(conditions.IsCCProcessed)).WithActions((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (a => a.putOnHold), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
          actions.Add<ARCashSaleEntryPaymentTransaction>((Expression<Func<ARCashSaleEntryPaymentTransaction, PXAction<ARCashSale>>>) (a => a.captureCCPayment), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) (a => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured) a.WithConnotation((ActionConnotation) 3)));
          actions.Add<ARCashSaleEntryPaymentTransaction>((Expression<Func<ARCashSaleEntryPaymentTransaction, PXAction<ARCashSale>>>) (a => a.authorizeCCPayment), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
          actions.Add<ARCashSaleEntryPaymentTransaction>((Expression<Func<ARCashSaleEntryPaymentTransaction, PXAction<ARCashSale>>>) (a => a.voidCCPayment), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
          actions.Add<ARCashSaleEntryPaymentTransaction>((Expression<Func<ARCashSaleEntryPaymentTransaction, PXAction<ARCashSale>>>) (a => a.creditCCPayment), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
          actions.Add<ARCashSaleEntryPaymentTransaction>((Expression<Func<ARCashSaleEntryPaymentTransaction, PXAction<ARCashSale>>>) (a => a.recordCCPayment), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
          actions.Add<ARCashSaleEntryPaymentTransaction>((Expression<Func<ARCashSaleEntryPaymentTransaction, PXAction<ARCashSale>>>) (a => a.captureOnlyCCPayment), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
          actions.Add<ARCashSaleEntryPaymentTransaction>((Expression<Func<ARCashSaleEntryPaymentTransaction, PXAction<ARCashSale>>>) (a => a.validateCCPayment), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.release), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (a => a.voidCheck), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.printInvoice), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.emailInvoice), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
        }))));
        sss.Add<ARDocStatus.balanced>((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.release), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) (a => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
          actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.sendEmail), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.emailInvoice), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.putOnHold), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.printAREdit), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.printInvoice), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.customerDocuments), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
        }))));
      }))).WithActions((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.validateAddresses), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null)))).WithEventHandlers((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<ARCashSaleEntry, PXWorkflowEventHandler<ARCashSale>>>) (g => g.OnUpdateStatus))))));
      fss.Add<ARDocStatus.closed>((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.sendEmail), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.emailInvoice), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.printInvoice), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.printARRegister), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.customerDocuments), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.voidCheck), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) (a => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.reclassifyBatch), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.validateAddresses), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
        actions.Add<ARCashSaleEntryPaymentTransaction>((Expression<Func<ARCashSaleEntryPaymentTransaction, PXAction<ARCashSale>>>) (a => a.captureCCPayment), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
        actions.Add<ARCashSaleEntryPaymentTransaction>((Expression<Func<ARCashSaleEntryPaymentTransaction, PXAction<ARCashSale>>>) (a => a.authorizeCCPayment), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
        actions.Add<ARCashSaleEntryPaymentTransaction>((Expression<Func<ARCashSaleEntryPaymentTransaction, PXAction<ARCashSale>>>) (a => a.voidCCPayment), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
        actions.Add<ARCashSaleEntryPaymentTransaction>((Expression<Func<ARCashSaleEntryPaymentTransaction, PXAction<ARCashSale>>>) (a => a.creditCCPayment), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
        actions.Add<ARCashSaleEntryPaymentTransaction>((Expression<Func<ARCashSaleEntryPaymentTransaction, PXAction<ARCashSale>>>) (a => a.recordCCPayment), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
        actions.Add<ARCashSaleEntryPaymentTransaction>((Expression<Func<ARCashSaleEntryPaymentTransaction, PXAction<ARCashSale>>>) (a => a.captureOnlyCCPayment), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
        actions.Add<ARCashSaleEntryPaymentTransaction>((Expression<Func<ARCashSaleEntryPaymentTransaction, PXAction<ARCashSale>>>) (a => a.validateCCPayment), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
      }))));
      fss.Add<ARDocStatus.open>((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.validateAddresses), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null)))));
      fss.Add<ARDocStatus.voided>((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.IConfigured>) null);
    })).WithTransitions((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.INeedTarget, BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.IConfigured>) (t => (BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.IConfigured) t.To<ARDocStatus.HoldToBalance>().IsTriggeredOn((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.initializeState))))));
      transitions.AddGroupFrom<ARDocStatus.HoldToBalance>((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.INeedTarget, BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.IConfigured>) (t => (BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.IConfigured) t.To<ARDocStatus.HoldToBalance>().IsTriggeredOn((Expression<Func<ARCashSaleEntry, PXWorkflowEventHandlerBase<ARCashSale>>>) (g => g.OnUpdateStatus))))));
      transitions.AddGroupFrom<ARDocStatus.cCHold>((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.INeedTarget, BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.IConfigured>) (t => (BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.IConfigured) t.To<ARDocStatus.voided>().IsTriggeredOn((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.voidCheck))))));
    })))).WithActions((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.initializeState), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (a => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.releaseFromHold), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.IContainerFillerFields>) (fas => fas.Add<ARCashSale.hold>((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.INeedRightOperand, BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.IConfigured>) (f => (BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.putOnHold), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.IContainerFillerFields>) (fas => fas.Add<ARCashSale.hold>((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.INeedRightOperand, BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.IConfigured>) (f => (BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add<ARCashSaleEntryPaymentTransaction>((Expression<Func<ARCashSaleEntryPaymentTransaction, PXAction<ARCashSale>>>) (a => a.captureCCPayment), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (a => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) a.WithCategory(cardProcessingCategory).IsHiddenWhen((BoundedTo<ARCashSaleEntry, ARCashSale>.ISharedCondition) conditions.IsNotCapturable)));
      actions.Add<ARCashSaleEntryPaymentTransaction>((Expression<Func<ARCashSaleEntryPaymentTransaction, PXAction<ARCashSale>>>) (a => a.authorizeCCPayment), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (a => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) a.WithCategory(cardProcessingCategory)));
      actions.Add<ARCashSaleEntryPaymentTransaction>((Expression<Func<ARCashSaleEntryPaymentTransaction, PXAction<ARCashSale>>>) (a => a.voidCCPayment), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (a => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) a.WithCategory(cardProcessingCategory)));
      actions.Add<ARCashSaleEntryPaymentTransaction>((Expression<Func<ARCashSaleEntryPaymentTransaction, PXAction<ARCashSale>>>) (a => a.creditCCPayment), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (a => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) a.WithCategory(cardProcessingCategory)));
      actions.Add<ARCashSaleEntryPaymentTransaction>((Expression<Func<ARCashSaleEntryPaymentTransaction, PXAction<ARCashSale>>>) (a => a.recordCCPayment), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (a => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) a.WithCategory(cardProcessingCategory)));
      actions.Add<ARCashSaleEntryPaymentTransaction>((Expression<Func<ARCashSaleEntryPaymentTransaction, PXAction<ARCashSale>>>) (a => a.captureOnlyCCPayment), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (a => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) a.WithCategory(cardProcessingCategory)));
      actions.Add<ARCashSaleEntryPaymentTransaction>((Expression<Func<ARCashSaleEntryPaymentTransaction, PXAction<ARCashSale>>>) (a => a.validateCCPayment), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (a => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) a.WithCategory(cardProcessingCategory)));
      actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.printInvoice), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) c.WithCategory(printingAndEmailingCategory).WithFieldAssignments((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.IContainerFillerFields>) (fa => fa.Add<ARRegister.printed>((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.INeedRightOperand, BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.IConfigured>) (e => (BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.emailInvoice), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) c.WithCategory(printingAndEmailingCategory).WithFieldAssignments((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.IContainerFillerFields>) (fass => fass.Add<ARRegister.emailed>((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.INeedRightOperand, BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.IConfigured>) (v => (BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.IConfigured) v.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.release), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).IsDisabledWhen((BoundedTo<ARCashSaleEntry, ARCashSale>.ISharedCondition) conditions.IsCCIntegrated)));
      actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.voidCheck), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) c.WithCategory(correctionsCategory).IsHiddenWhen((BoundedTo<ARCashSaleEntry, ARCashSale>.ISharedCondition) conditions.IsVoid)));
      actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.reclassifyBatch), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) c.IsHiddenWhen((BoundedTo<ARCashSaleEntry, ARCashSale>.ISharedCondition) conditions.IsMigrationMode).WithCategory(correctionsCategory)));
      actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.sendEmail), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) c.WithCategory(otherCategory)));
      actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.validateAddresses), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) c.WithCategory(otherCategory)));
      actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.customerDocuments), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) c.WithCategory((PredefinedCategory) 1)));
      actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.printAREdit), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) c.WithCategory((PredefinedCategory) 2)));
      actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.printARRegister), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) c.WithCategory((PredefinedCategory) 2)));
    })).WithHandlers((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers => handlers.Add((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ARCashSaleEntry, ARCashSale>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<ARCashSaleEntry, ARCashSale>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ARCashSaleEntry, ARCashSale>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<ARCashSale>) ((BoundedTo<ARCashSaleEntry, ARCashSale>.WorkflowEventHandlerDefinition.INeedSubscriber<ARCashSale>) handler.WithTargetOf<ARCashSale>().OfFieldsUpdated<TypeArrayOf<IBqlField>.FilledWith<ARCashSale.hold, ARRegister.pendingProcessing>>()).Is((Expression<Func<ARCashSale, PXWorkflowEventHandler<ARCashSale, ARCashSale>>>) (g => g.OnUpdateStatus))).UsesTargetAsPrimaryEntity())))).WithCategories((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(cardProcessingCategory);
      categories.Add(correctionsCategory);
      categories.Add(approvalCategory);
      categories.Add(printingAndEmailingCategory);
      categories.Add(otherCategory);
      categories.Update((FolderType) 1, (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.ConfiguratorCategory, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(otherCategory)));
      categories.Update((FolderType) 2, (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.ConfiguratorCategory, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter((FolderType) 1)));
    }))));
  }

  public class Conditions : BoundedTo<ARCashSaleEntry, ARCashSale>.Condition.Pack
  {
    private readonly ARSetupDefinition _Definition = ARSetupDefinition.GetSlot();

    public BoundedTo<ARCashSaleEntry, ARCashSale>.Condition IsNotOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Condition.ConditionBuilder, BoundedTo<ARCashSaleEntry, ARCashSale>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARCashSale.hold, Equal<False>>>>>.And<BqlOperand<ARCashSale.released, IBqlBool>.IsEqual<False>>>()), nameof (IsNotOnHold));
      }
    }

    public BoundedTo<ARCashSaleEntry, ARCashSale>.Condition IsVoid
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Condition.ConditionBuilder, BoundedTo<ARCashSaleEntry, ARCashSale>.Condition>) (c => c.FromBql<BqlOperand<ARCashSale.docType, IBqlString>.IsIn<ARDocType.voidPayment, ARDocType.voidRefund>>()), nameof (IsVoid));
      }
    }

    public BoundedTo<ARCashSaleEntry, ARCashSale>.Condition IsCCProcessed
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Condition.ConditionBuilder, BoundedTo<ARCashSaleEntry, ARCashSale>.Condition>) (c => c.FromBql<BqlOperand<ARRegister.pendingProcessing, IBqlBool>.IsEqual<False>>()), nameof (IsCCProcessed));
      }
    }

    public BoundedTo<ARCashSaleEntry, ARCashSale>.Condition IsCCIntegrated
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Condition.ConditionBuilder, BoundedTo<ARCashSaleEntry, ARCashSale>.Condition>) (c => !PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>() || !this._Definition.IntegratedCCProcessing.GetValueOrDefault() || this._Definition.MigrationMode.GetValueOrDefault() ? c.FromBql<BqlOperand<True, IBqlBool>.IsEqual<False>>() : c.FromBql<BqlOperand<ARCashSale.status, IBqlString>.IsEqual<ARDocStatus.cCHold>>()), nameof (IsCCIntegrated));
      }
    }

    public BoundedTo<ARCashSaleEntry, ARCashSale>.Condition IsNotCapturable
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Condition.ConditionBuilder, BoundedTo<ARCashSaleEntry, ARCashSale>.Condition>) (c => c.FromBql<BqlOperand<ARCashSale.docType, IBqlString>.IsEqual<ARDocType.cashReturn>>()), nameof (IsNotCapturable));
      }
    }

    public BoundedTo<ARCashSaleEntry, ARCashSale>.Condition IsMigrationMode
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Condition.ConditionBuilder, BoundedTo<ARCashSaleEntry, ARCashSale>.Condition>) (c => !this._Definition.MigrationMode.GetValueOrDefault() ? c.FromBql<BqlOperand<True, IBqlBool>.IsEqual<False>>() : c.FromBql<BqlOperand<True, IBqlBool>.IsEqual<True>>()), nameof (IsMigrationMode));
      }
    }
  }

  public static class CategoryNames
  {
    public const string Processing = "Processing";
    public const string CardProcessing = "Card Processing";
    public const string Corrections = "Corrections";
    public const string PrintingAndEmailing = "Printing and Emailing";
    public const string Approval = "Approval";
    public const string Other = "Other";
  }

  public static class CategoryID
  {
    public const string Processing = "ProcessingID";
    public const string CardProcessing = "CardProcessingID";
    public const string Corrections = "CorrectionsID";
    public const string PrintingAndEmailing = "PrintingAndEmailingID";
    public const string Approval = "ApprovalID";
    public const string Other = "OtherID";
  }
}
