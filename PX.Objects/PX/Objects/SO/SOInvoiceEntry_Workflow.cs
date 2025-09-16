// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOInvoiceEntry_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.GL;
using PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.SO;

public class SOInvoiceEntry_Workflow : PXGraphExtension<SOInvoiceEntry>
{
  [PXWorkflowDependsOnType(new Type[] {typeof (ARSetup)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    SOInvoiceEntry_Workflow.Configure(config.GetScreenConfigurationContext<SOInvoiceEntry, PX.Objects.AR.ARInvoice>());
  }

  protected static void Configure(WorkflowContext<SOInvoiceEntry, PX.Objects.AR.ARInvoice> context)
  {
    SOInvoiceEntry_Workflow.Conditions conditions = context.Conditions.GetPack<SOInvoiceEntry_Workflow.Conditions>();
    CommonActionCategories.Categories<SOInvoiceEntry, PX.Objects.AR.ARInvoice> categories1 = CommonActionCategories.Get<SOInvoiceEntry, PX.Objects.AR.ARInvoice>(context);
    BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionCategory.IConfigured processingCategory = categories1.Processing;
    BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionCategory.IConfigured correctionsCategory = context.Categories.CreateNew("Corrections Category", (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionCategory.IConfigured>) (category => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionCategory.IConfigured) category.DisplayName("Corrections")));
    BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionCategory.IConfigured approvalCategory = categories1.Approval;
    BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionCategory.IConfigured printingEmailingCategory = categories1.PrintingAndEmailing;
    BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionCategory.IConfigured otherCategory = categories1.Other;
    context.AddScreenConfigurationFor((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ScreenConfiguration.IStartConfigScreen, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ScreenConfiguration.IConfigured) ((BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<PX.Objects.AR.ARInvoice.status>().AddDefaultFlow((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Workflow.INeedStatesFlow, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Workflow.IConfigured>) (flow => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IContainerFillerStates>) (flowStates =>
    {
      flowStates.Add("_", (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<SOInvoiceEntry, PXAutoAction<PX.Objects.AR.ARInvoice>>>) (g => g.initializeState))));
      flowStates.Add<ARDocStatus.incomplete>((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.completeProcessing), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) (a => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar()))))).WithEventHandlers((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<SOInvoiceEntry, PXWorkflowEventHandler<PX.Objects.AR.ARInvoice>>>) (g => g.OnCompleteProcessing))))));
      flowStates.AddSequence<ARDocStatus.HoldToBalance>((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Sequence.INeedStatesThenConditionsConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured>) (seq => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.INeedAnyConfigState) seq.WithStates((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IContainerFillerStates>) (sss =>
      {
        sss.Add<ARDocStatus.hold>((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FlowState.INeedAnyFlowStateConfig) flowState.IsSkippedWhen(conditions.IsNotOnHold)).WithActions((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.releaseFromHold), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) (act => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured) act.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.validateAddresses), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.recalculateDiscountsAction), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        }))));
        sss.Add<ARDocStatus.cCHold>((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FlowState.INeedAnyFlowStateConfig) flowState.IsSkippedWhen(conditions.IsCCProcessed)).WithActions((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.putOnHold), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.putOnCreditHold), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.printInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.emailInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.validateAddresses), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.recalculateDiscountsAction), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add<CreatePaymentExt>((Expression<Func<CreatePaymentExt, PXAction<PX.Objects.AR.ARInvoice>>>) (e => e.createAndCapturePayment), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        }))));
        sss.Add<ARDocStatus.creditHold>((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FlowState.INeedAnyFlowStateConfig) flowState.IsSkippedWhen(conditions.IsCreditHoldChecked)).WithActions((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.releaseFromCreditHold), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) (act => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured) act.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.putOnHold), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) (act => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured) act.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.validateAddresses), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.emailInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add<CreatePaymentExt>((Expression<Func<CreatePaymentExt, PXAction<PX.Objects.AR.ARInvoice>>>) (e => e.createAndCapturePayment), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        }))));
        sss.Add<ARDocStatus.pendingPrint>((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FlowState.INeedAnyFlowStateConfig) flowState.IsSkippedWhen(conditions.IsPrinted)).WithActions((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.printInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) (act => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured) act.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.emailInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.putOnHold), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.putOnCreditHold), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.validateAddresses), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.recalculateDiscountsAction), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        }))));
        sss.Add<ARDocStatus.pendingEmail>((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FlowState.INeedAnyFlowStateConfig) flowState.IsSkippedWhen(conditions.IsEmailed)).WithActions((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.emailInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) (act => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured) act.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.printInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.putOnHold), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.putOnCreditHold), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.validateAddresses), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.recalculateDiscountsAction), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        }))));
        sss.Add<ARDocStatus.balanced>((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.release), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) (act => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured) act.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.putOnHold), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) (act => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured) act.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.putOnCreditHold), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.printInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.emailInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.arEdit), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.validateAddresses), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.recalculateDiscountsAction), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
          actions.Add<CreatePaymentExt>((Expression<Func<CreatePaymentExt, PXAction<PX.Objects.AR.ARInvoice>>>) (e => e.createAndCapturePayment), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        }))));
      }))).WithEventHandlers((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<SOInvoiceEntry, PXWorkflowEventHandler<PX.Objects.AR.ARInvoice>>>) (g => g.OnUpdateStatus))))));
      flowStates.Add<ARDocStatus.open>((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.post), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.writeOff), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.payInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.printInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.emailInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.validateAddresses), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.reclassifyBatch), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        actions.Add<Correction>((Expression<Func<Correction, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.cancelInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        actions.Add<Correction>((Expression<Func<Correction, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.correctInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        actions.Add<Correction>((Expression<Func<Correction, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.reverseDirectInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
      }))));
      flowStates.Add<ARDocStatus.closed>((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.post), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.printInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.emailInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.validateAddresses), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.reclassifyBatch), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        actions.Add<Correction>((Expression<Func<Correction, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.cancelInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        actions.Add<Correction>((Expression<Func<Correction, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.correctInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        actions.Add<Correction>((Expression<Func<Correction, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.reverseDirectInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
      }))).WithFieldStates((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FieldState.IContainerFillerFields>) (states => states.AddTable<PX.Objects.AR.ARInvoice>((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FieldState.INeedAnyConfigField, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FieldState.IConfigured>) (state => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FieldState.IConfigured) state.IsDisabled()))))));
      flowStates.Add<ARDocStatus.canceled>((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.printInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.emailInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionState.IConfigured>) null);
      }))).WithFieldStates((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FieldState.IContainerFillerFields>) (states => states.AddTable<PX.Objects.AR.ARInvoice>((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FieldState.INeedAnyConfigField, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FieldState.IConfigured>) (state => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.FieldState.IConfigured) state.IsDisabled()))))));
    })).WithTransitions((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.INeedTarget, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.incomplete>().IsTriggeredOn((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.initializeState)).When((BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ISharedCondition) conditions.IsPaymentsTransferPostponed)));
        ts.Add((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.INeedTarget, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.HoldToBalance>().IsTriggeredOn((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.initializeState))));
      }));
      transitions.AddGroupFrom<ARDocStatus.incomplete>((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.INeedTarget, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.HoldToBalance>().IsTriggeredOn((Expression<Func<SOInvoiceEntry, PXWorkflowEventHandlerBase<PX.Objects.AR.ARInvoice>>>) (g => g.OnCompleteProcessing)).WithFieldAssignments((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Assignment.IContainerFillerFields>) (fass => fass.Add<PX.Objects.AR.ARInvoice.isPaymentsTransferred>(new bool?(true)))).DoesNotPersist()))));
      transitions.AddGroupFrom<ARDocStatus.HoldToBalance>((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.INeedTarget, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.HoldToBalance>().IsTriggeredOn((Expression<Func<SOInvoiceEntry, PXWorkflowEventHandlerBase<PX.Objects.AR.ARInvoice>>>) (g => g.OnUpdateStatus)).When((BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ISharedCondition) conditions.IsSOInvoice)))));
      transitions.AddGroupFrom<ARDocStatus.balanced>((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.INeedTarget, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.open>().IsTriggeredOn((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.release)).When((BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ISharedCondition) conditions.IsReleased)));
        ts.Add((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.INeedTarget, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.closed>().IsTriggeredOn((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.release)).When((BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ISharedCondition) conditions.IsClosed)));
      }));
      transitions.AddGroupFrom<ARDocStatus.open>((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.ISourceContainerFillerTransitions>) (ts => { }));
      transitions.AddGroupFrom<ARDocStatus.closed>((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Transition.ISourceContainerFillerTransitions>) (ts => { }));
    })))).WithActions((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.initializeState), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.completeProcessing), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).MassProcessingScreen<SOReleaseInvoice>()));
      actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.releaseFromHold), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).WithFieldAssignments((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Assignment.IContainerFillerFields>) (fass => fass.Add<PX.Objects.AR.ARInvoice.hold>(new bool?(false))))));
      actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.putOnHold), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(processingCategory, (Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.releaseFromHold)).PlaceAfter((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.release)).WithFieldAssignments((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Assignment.IContainerFillerFields>) (fass => fass.Add<PX.Objects.AR.ARInvoice.hold>(new bool?(true))))));
      actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.release), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).MassProcessingScreen<SOReleaseInvoice>().InBatchMode()));
      actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.payInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(processingCategory)));
      actions.Add<Correction>((Expression<Func<Correction, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.correctInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(correctionsCategory)));
      actions.Add<Correction>((Expression<Func<Correction, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.cancelInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(correctionsCategory)));
      actions.Add<Correction>((Expression<Func<Correction, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.reverseDirectInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(correctionsCategory)));
      actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.writeOff), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(correctionsCategory)));
      actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.reclassifyBatch), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(correctionsCategory)));
      actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.releaseFromCreditHold), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) ((BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction) c.WithCategory(approvalCategory).IsHiddenWhen((BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ISharedCondition) conditions.IsCreditType).MassProcessingScreen<SOReleaseInvoice>()).WithFieldAssignments((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Assignment.IContainerFillerFields>) (fass => fass.Add<PX.Objects.AR.ARInvoice.creditHold>(new bool?(false))))));
      actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.putOnCreditHold), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(approvalCategory).IsHiddenWhen((BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ISharedCondition) conditions.IsCreditType).WithFieldAssignments((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Assignment.IContainerFillerFields>) (fass =>
      {
        fass.Add<PX.Objects.AR.ARInvoice.hold>(new bool?(false));
        fass.Add<PX.Objects.AR.ARInvoice.creditHold>(new bool?(true));
        fass.Add<PX.Objects.AR.ARInvoice.approvedCredit>(new bool?(false));
        fass.Add<PX.Objects.AR.ARInvoice.approvedCreditAmt>(new int?(0));
        fass.Add<PX.Objects.AR.ARInvoice.approvedCaptureFailed>(new bool?(false));
        fass.Add<PX.Objects.AR.ARInvoice.approvedPrepaymentRequired>(new bool?(false));
      }))));
      actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.printInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(printingEmailingCategory).WithPersistOptions((ActionPersistOptions) 2).MassProcessingScreen<SOReleaseInvoice>().InBatchMode().WithFieldAssignments((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Assignment.IContainerFillerFields>) (fass => fass.Add<PX.Objects.AR.ARInvoice.printed>(new bool?(true))))));
      actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.emailInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(printingEmailingCategory).MassProcessingScreen<SOReleaseInvoice>().InBatchMode().WithPersistOptions((ActionPersistOptions) 2).WithFieldAssignments((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Assignment.IContainerFillerFields>) (fass => fass.Add<PX.Objects.AR.ARInvoice.emailed>(new bool?(true))))));
      actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.recalculateDiscountsAction), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(otherCategory)));
      actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.post), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(otherCategory).MassProcessingScreen<SOReleaseInvoice>().InBatchMode()));
      actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.validateAddresses), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(otherCategory)));
      actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.arEdit), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory((PredefinedCategory) 2)));
      actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.printAREdit), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory((PredefinedCategory) 2).IsHiddenAlways()));
      actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.printARRegister), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory((PredefinedCategory) 2).IsHiddenAlways()));
      actions.Add<CreatePaymentExt>((Expression<Func<CreatePaymentExt, PXAction<PX.Objects.AR.ARInvoice>>>) (e => e.createAndCapturePayment), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory((PredefinedCategory) 0).IsHiddenAlways().MassProcessingScreen<SOReleaseInvoice>()));
      actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.reverseInvoiceAndApplyToMemo), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory((PredefinedCategory) 0).IsHiddenAlways()));
      actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.reverseInvoice), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory((PredefinedCategory) 0).IsHiddenAlways()));
      actions.Add((Expression<Func<SOInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.sendEmail), (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory((PredefinedCategory) 0).IsHiddenAlways()));
    })).WithCategories((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(correctionsCategory);
      categories.Add(approvalCategory);
      categories.Add(printingEmailingCategory);
      categories.Add(otherCategory);
      categories.Update((FolderType) 2, (Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionCategory.ConfiguratorCategory, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(otherCategory)));
    })).WithHandlers((Action<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers =>
    {
      handlers.Add((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<PX.Objects.AR.ARInvoice>) ((BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.WorkflowEventHandlerDefinition.INeedSubscriber<PX.Objects.AR.ARInvoice>) handler.WithTargetOf<PX.Objects.AR.ARInvoice>().OfEntityEvent<PX.Objects.AR.ARInvoice.Events>((Expression<Func<PX.Objects.AR.ARInvoice.Events, PXEntityEvent<PX.Objects.AR.ARInvoice>>>) (e => e.ProcessingCompleted))).Is((Expression<Func<PX.Objects.AR.ARInvoice, PXWorkflowEventHandler<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARInvoice>>>) (g => g.OnCompleteProcessing))).UsesTargetAsPrimaryEntity()));
      handlers.Add((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<PX.Objects.AR.ARInvoice>) ((BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<PX.Objects.AR.ARInvoice>) ((BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.WorkflowEventHandlerDefinition.INeedSubscriber<PX.Objects.AR.ARInvoice>) handler.WithTargetOf<PX.Objects.AR.ARInvoice>().OfFieldsUpdated<ARInvoiceEntry_Workflow.OnUpdateStatusFields>()).Is((Expression<Func<PX.Objects.AR.ARInvoice, PXWorkflowEventHandler<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARInvoice>>>) (g => g.OnUpdateStatus))).UsesTargetAsPrimaryEntity()).DisplayName("Invoice Updated")));
    }))));
  }

  public class Conditions : BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition.Pack
  {
    public BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition IsOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition.ConditionBuilder, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition>) (b => b.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.released, Equal<False>>>>>.And<BqlOperand<PX.Objects.AR.ARInvoice.hold, IBqlBool>.IsEqual<True>>>()), nameof (IsOnHold));
      }
    }

    public BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition IsNotOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition.ConditionBuilder, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition>) (b => b.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.hold, Equal<False>>>>>.And<BqlOperand<PX.Objects.AR.ARInvoice.released, IBqlBool>.IsEqual<False>>>()), nameof (IsNotOnHold));
      }
    }

    public BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition IsCreditHoldChecked
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition.ConditionBuilder, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition>) (b => b.FromBql<BqlOperand<PX.Objects.AR.ARInvoice.creditHold, IBqlBool>.IsEqual<False>>()), nameof (IsCreditHoldChecked));
      }
    }

    public BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition IsPrinted
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition.ConditionBuilder, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition>) (b => b.FromBql<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Setup<ARSetup.printBeforeRelease>, Equal<False>>>>, Or<BqlOperand<PX.Objects.AR.ARInvoice.printInvoice, IBqlBool>.IsEqual<False>>>>.Or<BqlOperand<PX.Objects.AR.ARInvoice.printed, IBqlBool>.IsEqual<True>>>()), nameof (IsPrinted));
      }
    }

    public BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition IsEmailed
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition.ConditionBuilder, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition>) (b => b.FromBql<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Setup<ARSetup.emailBeforeRelease>, Equal<False>>>>, Or<BqlOperand<PX.Objects.AR.ARInvoice.dontEmail, IBqlBool>.IsEqual<True>>>>.Or<BqlOperand<PX.Objects.AR.ARInvoice.emailed, IBqlBool>.IsEqual<True>>>()), nameof (IsEmailed));
      }
    }

    public BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition IsCCProcessed
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition.ConditionBuilder, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition>) (b => b.FromBql<BqlOperand<PX.Objects.AR.ARInvoice.pendingProcessing, IBqlBool>.IsEqual<False>>()), nameof (IsCCProcessed));
      }
    }

    public BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition IsOnCreditHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition.ConditionBuilder, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition>) (b => b.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.released, Equal<False>>>>>.And<BqlOperand<PX.Objects.AR.ARInvoice.creditHold, IBqlBool>.IsEqual<True>>>()), nameof (IsOnCreditHold));
      }
    }

    public BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition IsReleased
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition.ConditionBuilder, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition>) (b => b.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.released, Equal<True>>>>>.And<BqlOperand<PX.Objects.AR.ARInvoice.openDoc, IBqlBool>.IsEqual<True>>>()), nameof (IsReleased));
      }
    }

    public BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition IsClosed
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition.ConditionBuilder, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition>) (b => b.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.released, Equal<True>>>>>.And<BqlOperand<PX.Objects.AR.ARInvoice.openDoc, IBqlBool>.IsEqual<False>>>()), nameof (IsClosed));
      }
    }

    public BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition IsPendingProcessingPure
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition.ConditionBuilder, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition>) (b => b.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.pendingProcessing, Equal<True>>>>>.And<BqlOperand<PX.Objects.AR.ARInvoice.origModule, IBqlString>.IsEqual<BatchModule.moduleSO>>>()), nameof (IsPendingProcessingPure));
      }
    }

    public BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition IsCreditType
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition.ConditionBuilder, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition>) (b => b.FromBql<BqlOperand<PX.Objects.AR.ARInvoice.docType, IBqlString>.IsIn<ARDocType.creditMemo, ARDocType.cashReturn, ARDocType.cashSale>>()), nameof (IsCreditType));
      }
    }

    public BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition IsSOInvoice
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition.ConditionBuilder, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition>) (b => b.FromBql<BqlOperand<PX.Objects.AR.ARInvoice.origModule, IBqlString>.IsEqual<BatchModule.moduleSO>>()), nameof (IsSOInvoice));
      }
    }

    public BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition IsPaymentsTransferPostponed
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition.ConditionBuilder, BoundedTo<SOInvoiceEntry, PX.Objects.AR.ARInvoice>.Condition>) (b => b.FromBql<BqlOperand<PX.Objects.AR.ARInvoice.isPaymentsTransferred, IBqlBool>.IsEqual<False>>()), nameof (IsPaymentsTransferPostponed));
      }
    }
  }

  public static class ActionCategories
  {
    public const string CorrectionsCategoryID = "Corrections Category";

    [PXLocalizable]
    public static class DisplayNames
    {
      public const string Corrections = "Corrections";
    }
  }
}
