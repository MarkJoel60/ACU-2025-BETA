// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APQuickCheckEntry_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.AP.Standalone;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.AP;

public class APQuickCheckEntry_Workflow : PXGraphExtension<APQuickCheckEntry>
{
  public sealed override void Configure(PXScreenConfiguration config)
  {
    APQuickCheckEntry_Workflow.Configure(config.GetScreenConfigurationContext<APQuickCheckEntry, APQuickCheck>());
  }

  protected static void Configure(
    WorkflowContext<APQuickCheckEntry, APQuickCheck> context)
  {
    APQuickCheckEntry_Workflow.Conditions conditions = context.Conditions.GetPack<APQuickCheckEntry_Workflow.Conditions>();
    BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionCategory.IConfigured>) (category => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("Approval", (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionCategory.IConfigured>) (category => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionCategory.IConfigured correctionsCategory = context.Categories.CreateNew("Corrections", (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionCategory.IConfigured>) (category => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionCategory.IConfigured) category.DisplayName("Corrections")));
    BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionCategory.IConfigured customOtherCategory = context.Categories.CreateNew("CustomOther", (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionCategory.IConfigured>) (category => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionCategory.IConfigured) category.DisplayName("Other")));
    context.AddScreenConfigurationFor((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ScreenConfiguration.IStartConfigScreen, BoundedTo<APQuickCheckEntry, APQuickCheck>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ScreenConfiguration.IConfigured) screen.StateIdentifierIs<APQuickCheck.status>().AddDefaultFlow((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Workflow.INeedStatesFlow, BoundedTo<APQuickCheckEntry, APQuickCheck>.Workflow.IConfigured>) (flow => (BoundedTo<APQuickCheckEntry, APQuickCheck>.Workflow.IConfigured) flow.WithFlowStates((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<APQuickCheckEntry, PXAutoAction<APQuickCheck>>>) (g => g.initializeState))));
      fss.AddSequence<APDocStatus.HoldToBalance>((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Sequence.INeedStatesThenConditionsConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IConfigured>) (seq => (BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IConfigured) seq.WithStates((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IContainerFillerStates>) (sss =>
      {
        sss.Add<APDocStatus.hold>((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IConfigured) flowState.IsSkippedWhen(conditions.IsNotOnHold).WithActions((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.releaseFromHold), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IAllowOptionalConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured>) (a => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success)));
          actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.validateAddresses));
        }))));
        sss.Add<APDocStatus.pendingPrint>((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IConfigured) flowState.IsSkippedWhen(conditions.IsNotPrintable).WithActions((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.putOnHold), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IAllowOptionalConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured>) (a => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.printCheck), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IAllowOptionalConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured>) (a => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success)));
        }))));
        sss.Add<APDocStatus.printed>((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IConfigured) flowState.IsSkippedWhen(conditions.IsSkipPrinted).WithActions((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.release), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IAllowOptionalConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured>) (a => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success)));
          actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.prebook), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IAllowOptionalConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured>) (a => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        }))));
        sss.Add<APDocStatus.balanced>((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.release), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IAllowOptionalConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured>) (a => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success)));
          actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.prebook), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IAllowOptionalConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured>) (a => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.putOnHold));
          actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.validateAddresses));
        }))));
      })).WithActions((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.printAPEdit));
        actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.printAPPayment));
        actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.vendorDocuments));
      })).WithEventHandlers((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<APQuickCheckEntry, PXWorkflowEventHandler<APQuickCheck>>>) (g => g.OnUpdateStatus))))));
      fss.Add<APDocStatus.prebooked>((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.release), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IAllowOptionalConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured>) (a => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success)));
        actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.voidCheck), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IAllowOptionalConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured>) (a => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.printAPEdit));
        actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.printAPRegister));
        actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.printAPPayment));
        actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.vendorDocuments));
      }))));
      fss.Add<APDocStatus.open>((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.voidCheck), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IAllowOptionalConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured>) (a => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.reclassifyBatch));
        actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.printAPEdit));
        actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.printAPRegister));
        actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.printAPPayment));
        actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.vendorDocuments));
      }))));
      fss.Add<APDocStatus.closed>((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.voidCheck), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IAllowOptionalConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured>) (a => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.reclassifyBatch));
        actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.printAPRegister));
        actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.printAPPayment));
        actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.vendorDocuments));
        actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.cashReturn));
      }))));
      fss.Add<APDocStatus.voided>();
    })).WithTransitions((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.INeedTarget, BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.IConfigured>) (t => (BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.IConfigured) t.To<APDocStatus.HoldToBalance>().IsTriggeredOn((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.initializeState))))));
      transitions.AddGroupFrom<APDocStatus.HoldToBalance>((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.INeedTarget, BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.IConfigured>) (t => (BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.IConfigured) t.To<APDocStatus.HoldToBalance>().IsTriggeredOn((Expression<Func<APQuickCheckEntry, PXWorkflowEventHandlerBase<APQuickCheck>>>) (g => g.OnUpdateStatus))))));
      transitions.AddGroupFrom<APDocStatus.printed>((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.INeedTarget, BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.IConfigured>) (t => (BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.IConfigured) t.To<APDocStatus.prebooked>().IsTriggeredOn((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.prebook))))));
      transitions.AddGroupFrom<APDocStatus.balanced>((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.INeedTarget, BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.IConfigured>) (t => (BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.IConfigured) t.To<APDocStatus.prebooked>().IsTriggeredOn((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.prebook))))));
      transitions.AddGroupFrom<APDocStatus.open>((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.INeedTarget, BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.IConfigured>) (t => (BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.IConfigured) t.To<APDocStatus.voided>().IsTriggeredOn((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.voidCheck))))));
    })))).WithActions((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.initializeState), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured>) (a => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.releaseFromHold), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured>) (c => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).WithPersistOptions(ActionPersistOptions.NoPersist).WithFieldAssignments((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.IContainerFillerFields>) (fas => fas.Add<APQuickCheck.hold>((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.INeedRightOperand, BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.IConfigured>) (f => (BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.putOnHold), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured>) (c => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).WithPersistOptions(ActionPersistOptions.NoPersist).WithFieldAssignments((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.IContainerFillerFields>) (fas => fas.Add<APQuickCheck.hold>((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.INeedRightOperand, BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.IConfigured>) (f => (BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.printCheck), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured>) (c => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).IsHiddenWhen((BoundedTo<APQuickCheckEntry, APQuickCheck>.ISharedCondition) conditions.IsNotQuickCheck)));
      actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.prebook), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured>) (c => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).IsHiddenWhen((BoundedTo<APQuickCheckEntry, APQuickCheck>.ISharedCondition) (conditions.IsNotQuickCheck || conditions.IsMigrationMode))));
      actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.release), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured>) (c => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured) c.WithCategory(processingCategory)));
      actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.cashReturn), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured>) (c => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured) c.WithCategory(correctionsCategory).IsHiddenWhen((BoundedTo<APQuickCheckEntry, APQuickCheck>.ISharedCondition) conditions.IsNotQuickCheck)));
      actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.voidCheck), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured>) (c => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured) c.WithCategory(correctionsCategory).IsHiddenWhen((BoundedTo<APQuickCheckEntry, APQuickCheck>.ISharedCondition) (conditions.IsVoided || conditions.IsCashReturn))));
      actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.validateAddresses), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured>) (c => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured) c.WithCategory(customOtherCategory)));
      actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.reclassifyBatch), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured>) (c => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured) c.WithCategory(correctionsCategory).IsHiddenWhen((BoundedTo<APQuickCheckEntry, APQuickCheck>.ISharedCondition) conditions.IsMigrationMode)));
      actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.vendorDocuments), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured>) (c => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured) c.WithCategory(PredefinedCategory.Inquiries)));
      actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.printAPEdit), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured>) (c => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured) c.WithCategory(PredefinedCategory.Reports)));
      actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.printAPRegister), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured>) (c => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured) c.WithCategory(PredefinedCategory.Reports)));
      actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.printAPPayment), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured>) (c => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured) c.WithCategory(PredefinedCategory.Reports)));
    })).WithHandlers((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers => handlers.Add((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<APQuickCheckEntry, APQuickCheck>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<APQuickCheckEntry, APQuickCheck>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) handler.WithTargetOf<APQuickCheck>().OfFieldsUpdated<TypeArrayOf<IBqlField>.FilledWith<APQuickCheck.hold, APQuickCheck.printCheck>>().Is((Expression<Func<APQuickCheckEntry, PXWorkflowEventHandler<APQuickCheck, APQuickCheck>>>) (g => g.OnUpdateStatus)).UsesTargetAsPrimaryEntity())))).WithCategories((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(correctionsCategory);
      categories.Add(approvalCategory);
      categories.Add(customOtherCategory);
      categories.Update(FolderType.InquiriesFolder, (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionCategory.ConfiguratorCategory, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(customOtherCategory)));
      categories.Update(FolderType.ReportsFolder, (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionCategory.ConfiguratorCategory, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(context.Categories.Get(FolderType.InquiriesFolder))));
    }))));
  }

  public class Conditions : BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition.Pack
  {
    private readonly APSetupDefinition _Definition = APSetupDefinition.GetSlot();

    public BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition IsNotOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition.ConditionBuilder, BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition>) (b => b.FromBql<BqlOperand<APQuickCheck.hold, IBqlBool>.IsEqual<False>>()), nameof (IsNotOnHold));
      }
    }

    public BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition IsNotQuickCheck
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition.ConditionBuilder, BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition>) (b => b.FromBql<BqlOperand<APQuickCheck.docType, IBqlString>.IsNotEqual<APDocType.quickCheck>>()), nameof (IsNotQuickCheck));
      }
    }

    public BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition IsNotPrintable
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition.ConditionBuilder, BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition>) (b => b.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APQuickCheck.printCheck, Equal<False>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APQuickCheck.printed, Equal<True>>>>>.Or<BqlOperand<APQuickCheck.docType, IBqlString>.IsEqual<APDocType.cashReturn>>>>()), nameof (IsNotPrintable));
      }
    }

    public BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition IsSkipPrinted
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition.ConditionBuilder, BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition>) (b => b.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APQuickCheck.printCheck, Equal<False>>>>>.Or<BqlOperand<APQuickCheck.printed, IBqlBool>.IsEqual<False>>>()), nameof (IsSkipPrinted));
      }
    }

    public BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition IsVoided
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition.ConditionBuilder, BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition>) (b => b.FromBql<BqlOperand<APQuickCheck.docType, IBqlString>.IsEqual<APDocType.voidQuickCheck>>()), nameof (IsVoided));
      }
    }

    public BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition IsCashReturn
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition.ConditionBuilder, BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition>) (b => b.FromBql<BqlOperand<APQuickCheck.docType, IBqlString>.IsEqual<APDocType.cashReturn>>()), nameof (IsCashReturn));
      }
    }

    public BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition IsMigrationMode
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition.ConditionBuilder, BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition>) (c => !this._Definition.MigrationMode.GetValueOrDefault() ? c.FromBql<BqlOperand<True, IBqlBool>.IsEqual<False>>() : c.FromBql<BqlOperand<True, IBqlBool>.IsEqual<True>>()), nameof (IsMigrationMode));
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
