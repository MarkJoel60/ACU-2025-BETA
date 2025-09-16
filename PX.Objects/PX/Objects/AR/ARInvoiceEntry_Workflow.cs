// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceEntry_Workflow
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
using PX.Objects.GL;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.AR;

public class ARInvoiceEntry_Workflow : PXGraphExtension<ARInvoiceEntry>
{
  public const string MarkAsDontEmail = "Mark as Do not Email";

  [PXWorkflowDependsOnType(new Type[] {typeof (ARSetup)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    ARInvoiceEntry_Workflow.Configure(config.GetScreenConfigurationContext<ARInvoiceEntry, ARInvoice>());
  }

  protected static void Configure(WorkflowContext<ARInvoiceEntry, ARInvoice> context)
  {
    ARInvoiceEntry_Workflow.Conditions conditions = context.Conditions.GetPack<ARInvoiceEntry_Workflow.Conditions>();
    BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("ProcessingID", (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured>) (category => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("ApprovalID", (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured>) (category => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured printingAndEmailingCategory = context.Categories.CreateNew("PrintingAndEmailingID", (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured>) (category => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured) category.DisplayName("Printing and Emailing")));
    BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured correctionsCategory = context.Categories.CreateNew("CorrectionsID", (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured>) (category => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured) category.DisplayName("Corrections")));
    BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured intercompanyCategory = context.Categories.CreateNew("IntercompanyID", (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured>) (category => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured) category.DisplayName("Intercompany")));
    BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured relatedDocumentsCategory = context.Categories.CreateNew("Related DocumentsID", (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured>) (category => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured) category.DisplayName("Related Documents")));
    BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured customOtherCategory = context.Categories.CreateNew("OtherID", (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured>) (category => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IConfigured) category.DisplayName("Other")));
    BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IExtendedConfigured markDontEmail = context.ActionDefinitions.CreateNew("Mark as Do not Email", (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IExtendedConfigured>) (a => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IExtendedConfigured) a.DisplayName("Mark as Do not Email").WithCategory(printingAndEmailingCategory, (Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.emailInvoice)).PlaceAfter((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.createSchedule)).IsDisabledWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsEmailed).WithFieldAssignments((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IContainerFillerFields>) (fa => fa.Add<ARInvoice.dontEmail>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (e => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    context.AddScreenConfigurationFor((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ScreenConfiguration.IStartConfigScreen, BoundedTo<ARInvoiceEntry, ARInvoice>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<ARInvoiceEntry, ARInvoice>.ScreenConfiguration.IConfigured) ((BoundedTo<ARInvoiceEntry, ARInvoice>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<ARInvoice.status>().AddDefaultFlow((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Workflow.INeedStatesFlow, BoundedTo<ARInvoiceEntry, ARInvoice>.Workflow.IConfigured>) (flow => (BoundedTo<ARInvoiceEntry, ARInvoice>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<ARInvoiceEntry, PXAutoAction<ARInvoice>>>) (g => g.initializeState))));
      fss.AddSequence<ARDocStatus.HoldToBalance>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Sequence.INeedStatesThenConditionsConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured>) (seq => (BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.INeedAnyConfigState) ((BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.INeedAnyConfigState) seq.WithStates((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IContainerFillerStates>) (sss =>
      {
        sss.Add<ARDocStatus.hold>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig) flowState.IsSkippedWhen(conditions.IsNotOnHold)).WithActions((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.releaseFromHold), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) (a => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)))))));
        sss.Add<ARDocStatus.creditHold>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig) flowState.IsSkippedWhen(conditions.IsCreditHoldChecked)).WithActions((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.releaseFromCreditHold), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) (act => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured) act.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.putOnHold), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        }))));
        sss.Add<ARDocStatus.pendingPrint>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig) flowState.IsSkippedWhen(conditions.IsPrinted)).WithActions((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.putOnHold), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.printInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) (act => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured) act.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
          actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.createSchedule), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.putOnCreditHold), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.emailInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        }))));
        sss.Add<ARDocStatus.pendingEmail>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured>) (flowState =>
        {
          BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig anyFlowStateConfig = flowState;
          BoundedTo<ARInvoiceEntry, ARInvoice>.Condition isEmailed = conditions.IsEmailed;
          BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_True(isEmailed) ? isEmailed : BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_BitwiseOr(isEmailed, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_LogicalNot(conditions.IsEmailedRequired));
          return (BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig) anyFlowStateConfig.IsSkippedWhen(condition)).WithActions((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IContainerFillerActions>) (actions =>
          {
            actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.putOnHold), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.createSchedule), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.emailInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) (act => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured) act.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
            actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.putOnCreditHold), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
          }));
        }));
        sss.Add<ARDocStatus.cCHold>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig) flowState.IsSkippedWhen(conditions.IsCCProcessed)).WithActions((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (a => a.putOnHold), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.release), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (a => a.voidCheck), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        }))));
        sss.Add<ARDocStatus.balanced>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.release), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) (a => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
          actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.putOnHold), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.putOnCreditHold), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.createSchedule), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.emailInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        }))));
      }))).WithActions((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.validateAddresses), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.recalculateDiscountsAction), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.sendEmail), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.printAREdit), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.printInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.customerDocuments), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add(markDontEmail, (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandler<ARInvoice>>>) (g => g.OnUpdateStatus));
        handlers.Add<ARRegister>((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandler<ARInvoice, ARRegister>>>) (g => g.OnConfirmSchedule));
        handlers.Add((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandler<ARInvoice>>>) (g => g.OnReleaseDocument));
      }))));
      fss.Add<ARDocStatus.scheduled>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.createSchedule), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) (a => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.printAREdit), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.printInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.validateAddresses), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.customerDocuments), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add<ARRegister>((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandler<ARInvoice, ARRegister>>>) (g => g.OnConfirmSchedule));
        handlers.Add<ARRegister>((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandler<ARInvoice, ARRegister>>>) (g => g.OnVoidSchedule));
      }))));
      fss.Add<ARDocStatus.pendingPayment>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.payInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) (a => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.emailInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.reverseInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.reverseInvoiceAndApplyToMemo), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.validateAddresses), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.writeOff), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.reclassifyBatch), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.customerRefund), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.sendEmail), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add(markDontEmail, (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.printInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.printARRegister), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.customerDocuments), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.sOInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandler<ARInvoice>>>) (g => g.OnCloseDocument));
        handlers.Add((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandler<ARInvoice>>>) (g => g.OnVoidDocument));
        handlers.Add((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandler<ARInvoice>>>) (g => g.OnCancelDocument));
      }))));
      fss.Add<ARDocStatus.open>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.payInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) (a => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.emailInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.reverseInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.reverseInvoiceAndApplyToMemo), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.validateAddresses), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.writeOff), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.reclassifyBatch), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.customerRefund), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.sendEmail), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add(markDontEmail, (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.printInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.printARRegister), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.customerDocuments), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.sOInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandler<ARInvoice>>>) (g => g.OnCloseDocument));
        handlers.Add((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandler<ARInvoice>>>) (g => g.OnCancelDocument));
        handlers.Add((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandler<ARInvoice>>>) (g => g.OnOpenDocument));
      }))));
      fss.Add<ARDocStatus.unapplied>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.payInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) (a => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.emailInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.reverseInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.reverseInvoiceAndApplyToMemo), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.validateAddresses), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.writeOff), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.reclassifyBatch), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.customerRefund), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.sendEmail), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add(markDontEmail, (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.printInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.printARRegister), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.customerDocuments), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.sOInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandler<ARInvoice>>>) (g => g.OnCloseDocument));
        handlers.Add((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandler<ARInvoice>>>) (g => g.OnCancelDocument));
        handlers.Add((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandler<ARInvoice>>>) (g => g.OnOpenDocument));
      }))));
      fss.Add<ARDocStatus.closed>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.emailInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.reverseInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.validateAddresses), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.reclassifyBatch), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.printInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.printARRegister), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.customerDocuments), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.sOInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.sendEmail), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add(markDontEmail, (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandler<ARInvoice>>>) (g => g.OnOpenDocument));
        handlers.Add((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandler<ARInvoice>>>) (g => g.OnVoidDocument));
        handlers.Add((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandler<ARInvoice>>>) (g => g.OnCancelDocument));
      }))));
      fss.Add<ARDocStatus.canceled>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.printInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null)))).WithFieldStates((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.FieldState.IContainerFillerFields>) (states => states.AddTable<ARInvoice>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.FieldState.INeedAnyConfigField, BoundedTo<ARInvoiceEntry, ARInvoice>.FieldState.IConfigured>) (state => (BoundedTo<ARInvoiceEntry, ARInvoice>.FieldState.IConfigured) state.IsDisabled()))))));
      fss.Add<ARDocStatus.reserved>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.customerDocuments), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null)))));
      fss.Add<ARDocStatus.voided>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<ARInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.sendEmail), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null)))).WithActions((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.customerDocuments), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null)))).WithActions((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.printARRegister), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null)))));
    })).WithTransitions((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.HoldToBalance>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.initializeState))))));
      transitions.AddGroupFrom<ARDocStatus.HoldToBalance>((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.HoldToBalance>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnUpdateStatus)).When((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsARInvoice)));
        ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t =>
        {
          BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<ARDocStatus.pendingPayment>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnReleaseDocument));
          BoundedTo<ARInvoiceEntry, ARInvoice>.Condition prepaymentInvoice = conditions.IsPrepaymentInvoice;
          BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_False(prepaymentInvoice) ? prepaymentInvoice : BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_BitwiseAnd(prepaymentInvoice, conditions.IsOpen);
          return (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) condition);
        }));
        ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t =>
        {
          BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<ARDocStatus.open>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnReleaseDocument));
          BoundedTo<ARInvoiceEntry, ARInvoice>.Condition isOpen = conditions.IsOpen;
          BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_False(isOpen) ? isOpen : BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_BitwiseAnd(isOpen, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_LogicalNot(conditions.IsPrepaymentInvoice));
          return (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) condition);
        }));
        ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t =>
        {
          BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<ARDocStatus.unapplied>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnReleaseDocument));
          BoundedTo<ARInvoiceEntry, ARInvoice>.Condition isOpen = conditions.IsOpen;
          BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_False(isOpen) ? isOpen : BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_BitwiseAnd(isOpen, conditions.IsPrepaymentInvoice);
          return (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) condition);
        }));
        ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.closed>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnReleaseDocument)).When((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsClosed)));
        ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.scheduled>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnConfirmSchedule)).WithFieldAssignments((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<ARInvoice.scheduled>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (e => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) e.SetFromValue((object) true)));
          fas.Add<ARInvoice.scheduleID>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (e => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) e.SetFromExpression("@ScheduleID")));
        }))));
      }));
      transitions.AddGroupFrom<ARDocStatus.scheduled>((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.voided>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnVoidSchedule)).WithFieldAssignments((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<ARInvoice.voided>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (e => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) e.SetFromValue((object) true)));
          fas.Add<ARInvoice.scheduled>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (e => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) e.SetFromValue((object) false)));
          fas.Add<ARInvoice.scheduleID>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (e => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) e.SetFromValue((object) null)));
        }))));
        ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.scheduled>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnConfirmSchedule)).WithFieldAssignments((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<ARInvoice.scheduled>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (e => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) e.SetFromValue((object) true)));
          fas.Add<ARInvoice.scheduleID>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (e => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) e.SetFromExpression("@ScheduleID")));
        }))));
      }));
      transitions.AddGroupFrom<ARDocStatus.pendingPayment>((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.unapplied>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnReleaseDocument)).When((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsClosed).WithFieldAssignments((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IContainerFillerFields>) (fas => fas.Add<ARInvoice.pendingPayment>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (e => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) e.SetFromValue((object) false)))))));
        ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.unapplied>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnCloseDocument)).WithFieldAssignments((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IContainerFillerFields>) (fas => fas.Add<ARInvoice.pendingPayment>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (e => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) e.SetFromValue((object) false)))))));
        ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.voided>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnVoidDocument)).WithFieldAssignments((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<ARInvoice.openDoc>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (e => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) e.SetFromValue((object) false)));
          fas.Add<ARInvoice.pendingPayment>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (e => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) e.SetFromValue((object) false)));
          fas.Add<ARInvoice.voided>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (e => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) e.SetFromValue((object) true)));
        }))));
      }));
      transitions.AddGroupFrom<ARDocStatus.open>((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.closed>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnReleaseDocument)).When((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsClosed)));
        ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.closed>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnCloseDocument))));
        ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.canceled>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnCancelDocument))));
      }));
      transitions.AddGroupFrom<ARDocStatus.unapplied>((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.closed>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnReleaseDocument)).When((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsClosed)));
        ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.closed>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnCloseDocument))));
        ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.canceled>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnCancelDocument))));
        ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.pendingPayment>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnOpenDocument)).When((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsPendingPayment)));
      }));
      transitions.AddGroupFrom<ARDocStatus.closed>((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.open>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnOpenDocument)).When((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_LogicalNot(conditions.IsPrepaymentInvoice))));
        ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.unapplied>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnOpenDocument)).When((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsPrepaymentInvoice)));
        ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.voided>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnVoidDocument))));
        ts.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<ARInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.canceled>().IsTriggeredOn((Expression<Func<ARInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnCancelDocument))));
      }));
    })))).WithActions((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.initializeState), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (a => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.releaseFromHold), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IContainerFillerFields>) (fas => fas.Add<ARInvoice.hold>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (f => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.putOnHold), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IContainerFillerFields>) (fas => fas.Add<ARInvoice.hold>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (f => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.releaseFromCreditHold), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c.WithCategory(approvalCategory);
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition isCreditMemo = conditions.IsCreditMemo;
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_True(isCreditMemo) ? isCreditMemo : BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_BitwiseOr(isCreditMemo, conditions.IsPrepaymentInvoice);
        return (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) condition).WithFieldAssignments((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IContainerFillerFields>) (fass => fass.Add<ARInvoice.creditHold>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (v => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) v.SetFromValue((object) false)))));
      }));
      actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.putOnCreditHold), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c.WithCategory(approvalCategory, (Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.releaseFromCreditHold));
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition isCreditMemo = conditions.IsCreditMemo;
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_True(isCreditMemo) ? isCreditMemo : BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_BitwiseOr(isCreditMemo, conditions.IsPrepaymentInvoice);
        return (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) condition).WithFieldAssignments((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IContainerFillerFields>) (fass =>
        {
          fass.Add<ARInvoice.creditHold>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (v => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) v.SetFromValue((object) true)));
          fass.Add<ARInvoice.approvedCredit>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (v => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) v.SetFromValue((object) false)));
          fass.Add<ARInvoice.approvedCreditAmt>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (v => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) v.SetFromValue((object) 0)));
        }));
      }));
      actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.printInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(printingAndEmailingCategory).MassProcessingScreen<PrintARDocuments>().InBatchMode().IsDisabledWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsPrepaymentInvoiceReversing).WithFieldAssignments((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IContainerFillerFields>) (fa => fa.Add<ARRegister.printed>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (e => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.emailInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(printingAndEmailingCategory, (Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.printInvoice)).MassProcessingScreen<PrintARDocuments>().InBatchMode().WithFieldAssignments((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IContainerFillerFields>) (fa => fa.Add<ARRegister.emailed>((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.INeedRightOperand, BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured>) (e => (BoundedTo<ARInvoiceEntry, ARInvoice>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.release), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(processingCategory)));
      actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.payInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).IsDisabledWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsPrepaymentInvoiceReversing).IsHiddenWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsMigrationMode)));
      actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.reverseInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction5 = c.WithCategory(correctionsCategory);
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition prepaymentInvoice = conditions.IsPrepaymentInvoice;
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition13 = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_False(prepaymentInvoice) ? prepaymentInvoice : BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_BitwiseAnd(prepaymentInvoice, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_LogicalNot(conditions.IsPendingPayment));
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition14 = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_True(condition13) ? condition13 : BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_BitwiseOr(condition13, conditions.IsPrepaymentInvoiceReversing);
        BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction6 = optionalConfigAction5.IsDisabledWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) condition14);
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition isFinCharge = conditions.IsFinCharge;
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition15 = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_True(isFinCharge) ? isFinCharge : BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_BitwiseOr(isFinCharge, conditions.IsSmallCreditWO);
        return (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) optionalConfigAction6.IsHiddenWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) condition15);
      }));
      actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.reverseInvoiceAndApplyToMemo), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c.WithCategory(correctionsCategory).IsDisabledWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsPrepaymentInvoiceReversing);
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition isFinCharge = conditions.IsFinCharge;
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_True(isFinCharge) ? isFinCharge : BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_BitwiseOr(isFinCharge, conditions.IsPrepaymentInvoice);
        return (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) condition);
      }));
      actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.customerRefund), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c.WithCategory(processingCategory);
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition isCreditMemo = conditions.IsCreditMemo;
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition16;
        if (!BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_True(isCreditMemo))
        {
          BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition17 = isCreditMemo;
          BoundedTo<ARInvoiceEntry, ARInvoice>.Condition prepaymentInvoice = conditions.IsPrepaymentInvoice;
          BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition18 = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_False(prepaymentInvoice) ? prepaymentInvoice : BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_BitwiseAnd(prepaymentInvoice, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_LogicalNot(conditions.IsPendingPayment));
          condition16 = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_BitwiseOr(condition17, condition18);
        }
        else
          condition16 = isCreditMemo;
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition19 = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_LogicalNot(condition16);
        return (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) condition19);
      }));
      actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.writeOff), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c.WithCategory(correctionsCategory).IsDisabledWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsPrepaymentInvoiceReversing);
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition isMigrationMode = conditions.IsMigrationMode;
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_True(isMigrationMode) ? isMigrationMode : BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_BitwiseOr(isMigrationMode, conditions.IsPrepaymentInvoice);
        return (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) condition);
      }));
      actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.createSchedule), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c.WithCategory(customOtherCategory).IsDisabledWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsPrepaymentInvoiceReversing);
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition isNotSchedulable = conditions.IsNotSchedulable;
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_True(isNotSchedulable) ? isNotSchedulable : BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_BitwiseOr(isNotSchedulable, conditions.IsMigrationMode);
        return (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) condition);
      }));
      actions.Add((BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) markDontEmail);
      actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.recalculateDiscountsAction), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction7 = c.WithCategory(customOtherCategory);
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition allowRecalcPrice = conditions.IsNotAllowRecalcPrice;
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition20 = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_True(allowRecalcPrice) ? allowRecalcPrice : BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_BitwiseOr(allowRecalcPrice, conditions.IsPrepaymentInvoiceReversing);
        BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction8 = optionalConfigAction7.IsDisabledWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) condition20);
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition isMigrationMode = conditions.IsMigrationMode;
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition21 = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_True(isMigrationMode) ? isMigrationMode : BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_BitwiseOr(isMigrationMode, conditions.IsPrepaymentInvoice);
        return (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) optionalConfigAction8.IsHiddenWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) condition21);
      }));
      actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.reclassifyBatch), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c.WithCategory(correctionsCategory).IsDisabledWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsPrepaymentInvoiceReversing);
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition isMigrationMode = conditions.IsMigrationMode;
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_True(isMigrationMode) ? isMigrationMode : BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_BitwiseOr(isMigrationMode, conditions.IsPrepaymentInvoice);
        return (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) condition);
      }));
      actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.validateAddresses), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(customOtherCategory)));
      actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.sendEmail), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c;
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition invoiceReversing = conditions.IsPrepaymentInvoiceReversing;
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_False(invoiceReversing) ? invoiceReversing : BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_BitwiseAnd(invoiceReversing, conditions.IsClosed);
        return (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) optionalConfigAction.IsDisabledWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) condition).WithCategory(customOtherCategory);
      }));
      actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.customerDocuments), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) c.WithCategory((PredefinedCategory) 1)));
      actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.sOInvoice), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c.WithCategory(relatedDocumentsCategory);
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition isFinCharge = conditions.IsFinCharge;
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition22 = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_True(isFinCharge) ? isFinCharge : BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_BitwiseOr(isFinCharge, conditions.IsSmallCreditWO);
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition23 = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_True(condition22) ? condition22 : BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_BitwiseOr(condition22, conditions.IsARInvoice);
        BoundedTo<ARInvoiceEntry, ARInvoice>.Condition condition24 = BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_True(condition23) ? condition23 : BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.op_BitwiseOr(condition23, conditions.IsPrepaymentInvoice);
        return (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<ARInvoiceEntry, ARInvoice>.ISharedCondition) condition24);
      }));
      actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.printAREdit), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) c.WithCategory((PredefinedCategory) 2)));
      actions.Add((Expression<Func<ARInvoiceEntry, PXAction<ARInvoice>>>) (g => g.printARRegister), (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) c.WithCategory((PredefinedCategory) 2)));
    })).WithHandlers((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers =>
    {
      handlers.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<ARRegister>) ((BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedSubscriber<ARRegister>) ((BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedEventContainer<ARRegister, PX.Objects.GL.Schedule>) handler.WithTargetOf<ARRegister>().WithParametersOf<PX.Objects.GL.Schedule>()).OfEntityEvent<ARRegister.Events>((Expression<Func<ARRegister.Events, PXEntityEvent<ARRegister, PX.Objects.GL.Schedule>>>) (e => e.ConfirmSchedule))).Is((Expression<Func<ARRegister, PXWorkflowEventHandler<ARInvoice, ARRegister>>>) (g => g.OnConfirmSchedule))).UsesPrimaryEntityGetter<SelectFromBase<ARInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARInvoice.docType, Equal<BqlField<ARRegister.docType, IBqlString>.FromCurrent>>>>>.And<BqlOperand<ARInvoice.refNbr, IBqlString>.IsEqual<BqlField<ARRegister.refNbr, IBqlString>.FromCurrent>>>>(false)));
      handlers.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<ARRegister>) ((BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedSubscriber<ARRegister>) ((BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedEventContainer<ARRegister, PX.Objects.GL.Schedule>) handler.WithTargetOf<ARRegister>().WithParametersOf<PX.Objects.GL.Schedule>()).OfEntityEvent<ARRegister.Events>((Expression<Func<ARRegister.Events, PXEntityEvent<ARRegister, PX.Objects.GL.Schedule>>>) (e => e.VoidSchedule))).Is((Expression<Func<ARRegister, PXWorkflowEventHandler<ARInvoice, ARRegister>>>) (g => g.OnVoidSchedule))).UsesPrimaryEntityGetter<SelectFromBase<ARInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARInvoice.docType, Equal<BqlField<ARRegister.docType, IBqlString>.FromCurrent>>>>>.And<BqlOperand<ARInvoice.refNbr, IBqlString>.IsEqual<BqlField<ARRegister.refNbr, IBqlString>.FromCurrent>>>>(false)));
      handlers.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<ARInvoice>) ((BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<ARInvoice>) ((BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedSubscriber<ARInvoice>) handler.WithTargetOf<ARInvoice>().OfEntityEvent<ARInvoice.Events>((Expression<Func<ARInvoice.Events, PXEntityEvent<ARInvoice>>>) (e => e.ReleaseDocument))).Is((Expression<Func<ARInvoice, PXWorkflowEventHandler<ARInvoice, ARInvoice>>>) (g => g.OnReleaseDocument))).UsesTargetAsPrimaryEntity()).WithUpcastTo<ARRegister>()));
      handlers.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<ARInvoice>) ((BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<ARInvoice>) ((BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedSubscriber<ARInvoice>) handler.WithTargetOf<ARInvoice>().OfEntityEvent<ARInvoice.Events>((Expression<Func<ARInvoice.Events, PXEntityEvent<ARInvoice>>>) (e => e.OpenDocument))).Is((Expression<Func<ARInvoice, PXWorkflowEventHandler<ARInvoice, ARInvoice>>>) (g => g.OnOpenDocument))).UsesTargetAsPrimaryEntity()).WithUpcastTo<ARRegister>()));
      handlers.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<ARInvoice>) ((BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<ARInvoice>) ((BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedSubscriber<ARInvoice>) handler.WithTargetOf<ARInvoice>().OfEntityEvent<ARInvoice.Events>((Expression<Func<ARInvoice.Events, PXEntityEvent<ARInvoice>>>) (e => e.CloseDocument))).Is((Expression<Func<ARInvoice, PXWorkflowEventHandler<ARInvoice, ARInvoice>>>) (g => g.OnCloseDocument))).UsesTargetAsPrimaryEntity()).WithUpcastTo<ARRegister>()));
      handlers.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<ARInvoice>) ((BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedSubscriber<ARInvoice>) handler.WithTargetOf<ARInvoice>().OfEntityEvent<ARInvoice.Events>((Expression<Func<ARInvoice.Events, PXEntityEvent<ARInvoice>>>) (e => e.CancelDocument))).Is((Expression<Func<ARInvoice, PXWorkflowEventHandler<ARInvoice, ARInvoice>>>) (g => g.OnCancelDocument))).UsesTargetAsPrimaryEntity()));
      handlers.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<ARInvoice>) ((BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<ARInvoice>) ((BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedSubscriber<ARInvoice>) handler.WithTargetOf<ARInvoice>().OfEntityEvent<ARInvoice.Events>((Expression<Func<ARInvoice.Events, PXEntityEvent<ARInvoice>>>) (e => e.VoidDocument))).Is((Expression<Func<ARInvoice, PXWorkflowEventHandler<ARInvoice, ARInvoice>>>) (g => g.OnVoidDocument))).UsesTargetAsPrimaryEntity()).WithUpcastTo<ARRegister>()));
      handlers.Add((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<ARInvoice>) ((BoundedTo<ARInvoiceEntry, ARInvoice>.WorkflowEventHandlerDefinition.INeedSubscriber<ARInvoice>) handler.WithTargetOf<ARInvoice>().OfFieldsUpdated<ARInvoiceEntry_Workflow.OnUpdateStatusFields>()).Is((Expression<Func<ARInvoice, PXWorkflowEventHandler<ARInvoice, ARInvoice>>>) (g => g.OnUpdateStatus))).UsesTargetAsPrimaryEntity()));
    })).WithCategories((Action<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(correctionsCategory);
      categories.Add(intercompanyCategory);
      categories.Add(approvalCategory);
      categories.Add(printingAndEmailingCategory);
      categories.Add(customOtherCategory);
      categories.Add(relatedDocumentsCategory);
      categories.Update((FolderType) 1, (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.ConfiguratorCategory, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(relatedDocumentsCategory)));
      categories.Update((FolderType) 2, (Func<BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.ConfiguratorCategory, BoundedTo<ARInvoiceEntry, ARInvoice>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter((FolderType) 1)));
    }))));
  }

  public class Conditions : BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.Pack
  {
    private readonly ARSetupDefinition _Definition = ARSetupDefinition.GetSlot();

    public BoundedTo<ARInvoiceEntry, ARInvoice>.Condition IsNotOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARInvoice.hold, Equal<False>>>>>.And<BqlOperand<ARInvoice.released, IBqlBool>.IsEqual<False>>>()), nameof (IsNotOnHold));
      }
    }

    public BoundedTo<ARInvoiceEntry, ARInvoice>.Condition IsCreditHoldChecked
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition>) (c => c.FromBql<BqlOperand<ARInvoice.creditHold, IBqlBool>.IsEqual<False>>()), nameof (IsCreditHoldChecked));
      }
    }

    public BoundedTo<ARInvoiceEntry, ARInvoice>.Condition IsPrinted
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition>) (c => !this._Definition.PrintBeforeRelease.GetValueOrDefault() ? c.FromBql<BqlOperand<True, IBqlBool>.IsEqual<True>>() : c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARInvoice.printInvoice, Equal<False>>>>>.Or<BqlOperand<ARInvoice.printed, IBqlBool>.IsEqual<True>>>()), nameof (IsPrinted));
      }
    }

    public BoundedTo<ARInvoiceEntry, ARInvoice>.Condition IsEmailedRequired
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition>) (c => !this._Definition.EmailBeforeRelease.GetValueOrDefault() ? c.FromBql<BqlOperand<True, IBqlBool>.IsEqual<False>>() : c.FromBql<BqlOperand<True, IBqlBool>.IsEqual<True>>()), nameof (IsEmailedRequired));
      }
    }

    public BoundedTo<ARInvoiceEntry, ARInvoice>.Condition IsEmailed
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARInvoice.dontEmail, Equal<True>>>>>.Or<BqlOperand<ARInvoice.emailed, IBqlBool>.IsEqual<True>>>()), nameof (IsEmailed));
      }
    }

    public BoundedTo<ARInvoiceEntry, ARInvoice>.Condition IsCCProcessed
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition>) (c => c.FromBql<BqlOperand<ARInvoice.pendingProcessing, IBqlBool>.IsEqual<False>>()), nameof (IsCCProcessed));
      }
    }

    public BoundedTo<ARInvoiceEntry, ARInvoice>.Condition IsOpen
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARInvoice.openDoc, Equal<True>>>>>.And<BqlOperand<ARInvoice.released, IBqlBool>.IsEqual<True>>>()), nameof (IsOpen));
      }
    }

    public BoundedTo<ARInvoiceEntry, ARInvoice>.Condition IsClosed
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARInvoice.openDoc, Equal<False>>>>>.And<BqlOperand<ARInvoice.released, IBqlBool>.IsEqual<True>>>()), nameof (IsClosed));
      }
    }

    public BoundedTo<ARInvoiceEntry, ARInvoice>.Condition IsZeroBalance
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition>) (c => c.FromBql<BqlOperand<ARInvoice.docBal, IBqlDecimal>.IsEqual<decimal0>>()), nameof (IsZeroBalance));
      }
    }

    public BoundedTo<ARInvoiceEntry, ARInvoice>.Condition IsCreditMemo
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition>) (c => c.FromBql<BqlOperand<ARInvoice.docType, IBqlString>.IsEqual<ARDocType.creditMemo>>()), nameof (IsCreditMemo));
      }
    }

    public BoundedTo<ARInvoiceEntry, ARInvoice>.Condition IsNotSchedulable
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition>) (c => c.FromBql<BqlOperand<ARInvoice.docType, IBqlString>.IsNotIn<ARDocType.invoice, ARDocType.creditMemo, ARDocType.debitMemo>>()), nameof (IsNotSchedulable));
      }
    }

    public BoundedTo<ARInvoiceEntry, ARInvoice>.Condition IsNotCreditMemo
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition>) (c => c.FromBql<BqlOperand<ARInvoice.docType, IBqlString>.IsNotEqual<ARDocType.creditMemo>>()), nameof (IsNotCreditMemo));
      }
    }

    public BoundedTo<ARInvoiceEntry, ARInvoice>.Condition IsFinCharge
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition>) (c => c.FromBql<BqlOperand<ARInvoice.docType, IBqlString>.IsEqual<ARDocType.finCharge>>()), nameof (IsFinCharge));
      }
    }

    public BoundedTo<ARInvoiceEntry, ARInvoice>.Condition IsSmallCreditWO
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition>) (c => c.FromBql<BqlOperand<ARInvoice.docType, IBqlString>.IsEqual<ARDocType.smallCreditWO>>()), nameof (IsSmallCreditWO));
      }
    }

    public BoundedTo<ARInvoiceEntry, ARInvoice>.Condition IsNotAllowRecalcPrice
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARInvoice.pendingPPD, Equal<True>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.curyRetainageTotal, Greater<decimal0>>>>>.Or<BqlOperand<ARInvoice.isRetainageDocument, IBqlBool>.IsEqual<True>>>>()), nameof (IsNotAllowRecalcPrice));
      }
    }

    public BoundedTo<ARInvoiceEntry, ARInvoice>.Condition IsARInvoice
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition>) (c => c.FromBql<BqlOperand<ARInvoice.origModule, IBqlString>.IsNotEqual<BatchModule.moduleSO>>()), nameof (IsARInvoice));
      }
    }

    public BoundedTo<ARInvoiceEntry, ARInvoice>.Condition IsMigrationMode
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition>) (c => !this._Definition.MigrationMode.GetValueOrDefault() ? c.FromBql<BqlOperand<True, IBqlBool>.IsEqual<False>>() : c.FromBql<BqlOperand<True, IBqlBool>.IsEqual<True>>()), nameof (IsMigrationMode));
      }
    }

    public BoundedTo<ARInvoiceEntry, ARInvoice>.Condition IsPendingPayment
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition>) (c => c.FromBql<BqlOperand<ARInvoice.pendingPayment, IBqlBool>.IsEqual<True>>()), nameof (IsPendingPayment));
      }
    }

    public BoundedTo<ARInvoiceEntry, ARInvoice>.Condition IsPrepaymentInvoice
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition>) (c => c.FromBql<BqlOperand<ARInvoice.docType, IBqlString>.IsEqual<ARDocType.prepaymentInvoice>>()), nameof (IsPrepaymentInvoice));
      }
    }

    public BoundedTo<ARInvoiceEntry, ARInvoice>.Condition IsPrepaymentInvoiceReversing
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<ARInvoiceEntry, ARInvoice>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARInvoice.docType, Equal<ARDocType.creditMemo>>>>>.And<BqlOperand<ARInvoice.origDocType, IBqlString>.IsEqual<ARDocType.prepaymentInvoice>>>()), nameof (IsPrepaymentInvoiceReversing));
      }
    }
  }

  public class OnUpdateStatusFields : 
    TypeArrayOf<IBqlField>.FilledWith<ARInvoice.hold, ARInvoice.creditHold, ARInvoice.printed, ARRegister.dontPrint, ARInvoice.emailed, ARInvoice.dontEmail, ARInvoice.pendingProcessing>
  {
  }

  public static class CategoryNames
  {
    public const string Processing = "Processing";
    public const string Approval = "Approval";
    public const string PrintingAndEmailing = "Printing and Emailing";
    public const string Corrections = "Corrections";
    public const string Intercompany = "Intercompany";
    public const string Other = "Other";
    public const string RelatedDocuments = "Related Documents";
    public const string Inquiries = "Inquiries";
    public const string Reports = "Reports";
  }

  public static class CategoryID
  {
    public const string Processing = "ProcessingID";
    public const string Approval = "ApprovalID";
    public const string PrintingAndEmailing = "PrintingAndEmailingID";
    public const string Corrections = "CorrectionsID";
    public const string Intercompany = "IntercompanyID";
    public const string Other = "OtherID";
    public const string RelatedDocuments = "Related DocumentsID";
    public const string Inquiries = "InquiriesID";
    public const string Reports = "ReportsID";
  }
}
