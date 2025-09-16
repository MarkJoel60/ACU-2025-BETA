// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAReconEntry_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CA;

public class CAReconEntry_Workflow : PXGraphExtension<CAReconEntry>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    CAReconEntry_Workflow.Configure(config.GetScreenConfigurationContext<CAReconEntry, CARecon>());
  }

  protected static void Configure(WorkflowContext<CAReconEntry, CARecon> context)
  {
    CAReconEntry_Workflow.Conditions conditions = context.Conditions.GetPack<CAReconEntry_Workflow.Conditions>();
    BoundedTo<CAReconEntry, CARecon>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("ProcessingID", (Func<BoundedTo<CAReconEntry, CARecon>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CAReconEntry, CARecon>.ActionCategory.IConfigured>) (category => (BoundedTo<CAReconEntry, CARecon>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<CAReconEntry, CARecon>.ActionCategory.IConfigured correctionsCategory = context.Categories.CreateNew("CorrectionsID", (Func<BoundedTo<CAReconEntry, CARecon>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CAReconEntry, CARecon>.ActionCategory.IConfigured>) (category => (BoundedTo<CAReconEntry, CARecon>.ActionCategory.IConfigured) category.DisplayName("Corrections")));
    BoundedTo<CAReconEntry, CARecon>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("ApprovalID", (Func<BoundedTo<CAReconEntry, CARecon>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CAReconEntry, CARecon>.ActionCategory.IConfigured>) (category => (BoundedTo<CAReconEntry, CARecon>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    context.AddScreenConfigurationFor((Func<BoundedTo<CAReconEntry, CARecon>.ScreenConfiguration.IStartConfigScreen, BoundedTo<CAReconEntry, CARecon>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<CAReconEntry, CARecon>.ScreenConfiguration.IConfigured) ((BoundedTo<CAReconEntry, CARecon>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<CARecon.status>().AddDefaultFlow((Func<BoundedTo<CAReconEntry, CARecon>.Workflow.INeedStatesFlow, BoundedTo<CAReconEntry, CARecon>.Workflow.IConfigured>) (flow => (BoundedTo<CAReconEntry, CARecon>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<CAReconEntry, CARecon>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<CAReconEntry, CARecon>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CAReconEntry, CARecon>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CAReconEntry, CARecon>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<CAReconEntry, PXAutoAction<CARecon>>>) (g => g.initializeState))));
      fss.Add<CADocStatus.hold>((Func<BoundedTo<CAReconEntry, CARecon>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CAReconEntry, CARecon>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CAReconEntry, CARecon>.BaseFlowStep.IConfigured) ((BoundedTo<CAReconEntry, CARecon>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<CAReconEntry, CARecon>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.releaseFromHold), (Func<BoundedTo<CAReconEntry, CARecon>.ActionState.IAllowOptionalConfig, BoundedTo<CAReconEntry, CARecon>.ActionState.IConfigured>) (a => (BoundedTo<CAReconEntry, CARecon>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.printReconciliationReport), (Func<BoundedTo<CAReconEntry, CARecon>.ActionState.IAllowOptionalConfig, BoundedTo<CAReconEntry, CARecon>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<CAReconEntry, CARecon>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<CAReconEntry, PXWorkflowEventHandler<CARecon>>>) (g => g.OnUpdateStatus))))));
      fss.Add<CADocStatus.balanced>((Func<BoundedTo<CAReconEntry, CARecon>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CAReconEntry, CARecon>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CAReconEntry, CARecon>.BaseFlowStep.IConfigured) ((BoundedTo<CAReconEntry, CARecon>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<CAReconEntry, CARecon>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.Release), (Func<BoundedTo<CAReconEntry, CARecon>.ActionState.IAllowOptionalConfig, BoundedTo<CAReconEntry, CARecon>.ActionState.IConfigured>) (a => (BoundedTo<CAReconEntry, CARecon>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.putOnHold), (Func<BoundedTo<CAReconEntry, CARecon>.ActionState.IAllowOptionalConfig, BoundedTo<CAReconEntry, CARecon>.ActionState.IConfigured>) (a => (BoundedTo<CAReconEntry, CARecon>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.printReconciliationReport), (Func<BoundedTo<CAReconEntry, CARecon>.ActionState.IAllowOptionalConfig, BoundedTo<CAReconEntry, CARecon>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<CAReconEntry, CARecon>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<CAReconEntry, PXWorkflowEventHandler<CARecon>>>) (g => g.OnUpdateStatus))))));
      fss.Add<CADocStatus.closed>((Func<BoundedTo<CAReconEntry, CARecon>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CAReconEntry, CARecon>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CAReconEntry, CARecon>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<CAReconEntry, CARecon>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.Voided), (Func<BoundedTo<CAReconEntry, CARecon>.ActionState.IAllowOptionalConfig, BoundedTo<CAReconEntry, CARecon>.ActionState.IConfigured>) (a => (BoundedTo<CAReconEntry, CARecon>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.printReconciliationReport), (Func<BoundedTo<CAReconEntry, CARecon>.ActionState.IAllowOptionalConfig, BoundedTo<CAReconEntry, CARecon>.ActionState.IConfigured>) null);
      }))));
      fss.Add<CADocStatus.voided>((Func<BoundedTo<CAReconEntry, CARecon>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CAReconEntry, CARecon>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CAReconEntry, CARecon>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<CAReconEntry, CARecon>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.printReconciliationReport), (Func<BoundedTo<CAReconEntry, CARecon>.ActionState.IAllowOptionalConfig, BoundedTo<CAReconEntry, CARecon>.ActionState.IConfigured>) null)))));
    })).WithTransitions((Action<BoundedTo<CAReconEntry, CARecon>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<CAReconEntry, CARecon>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<CAReconEntry, CARecon>.Transition.INeedTarget, BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured>) (t => (BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured) t.To<CADocStatus.hold>().IsTriggeredOn((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.initializeState)).When((BoundedTo<CAReconEntry, CARecon>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<CAReconEntry, CARecon>.Transition.INeedTarget, BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured>) (t => (BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured) t.To<CADocStatus.balanced>().IsTriggeredOn((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.initializeState)).When((BoundedTo<CAReconEntry, CARecon>.ISharedCondition) conditions.IsNotOnHold)));
      }));
      transitions.AddGroupFrom<CADocStatus.hold>((Action<BoundedTo<CAReconEntry, CARecon>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<CAReconEntry, CARecon>.Transition.INeedTarget, BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured>) (t => (BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured) t.To<CADocStatus.balanced>().IsTriggeredOn((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.releaseFromHold)).WithFieldAssignments((Action<BoundedTo<CAReconEntry, CARecon>.Assignment.IContainerFillerFields>) (fas => fas.Add<CARecon.hold>((Func<BoundedTo<CAReconEntry, CARecon>.Assignment.INeedRightOperand, BoundedTo<CAReconEntry, CARecon>.Assignment.IConfigured>) (f => (BoundedTo<CAReconEntry, CARecon>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
        ts.Add((Func<BoundedTo<CAReconEntry, CARecon>.Transition.INeedTarget, BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured>) (t => (BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured) t.To<CADocStatus.balanced>().IsTriggeredOn((Expression<Func<CAReconEntry, PXWorkflowEventHandlerBase<CARecon>>>) (g => g.OnUpdateStatus)).When((BoundedTo<CAReconEntry, CARecon>.ISharedCondition) conditions.IsNotOnHold)));
      }));
      transitions.AddGroupFrom<CADocStatus.balanced>((Action<BoundedTo<CAReconEntry, CARecon>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<CAReconEntry, CARecon>.Transition.INeedTarget, BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured>) (t => (BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured) t.To<CADocStatus.hold>().IsTriggeredOn((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.putOnHold)).WithFieldAssignments((Action<BoundedTo<CAReconEntry, CARecon>.Assignment.IContainerFillerFields>) (fas => fas.Add<CARecon.hold>((Func<BoundedTo<CAReconEntry, CARecon>.Assignment.INeedRightOperand, BoundedTo<CAReconEntry, CARecon>.Assignment.IConfigured>) (f => (BoundedTo<CAReconEntry, CARecon>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        ts.Add((Func<BoundedTo<CAReconEntry, CARecon>.Transition.INeedTarget, BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured>) (t => (BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured) t.To<CADocStatus.closed>().IsTriggeredOn((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.Release))));
        ts.Add((Func<BoundedTo<CAReconEntry, CARecon>.Transition.INeedTarget, BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured>) (t => (BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured) t.To<CADocStatus.hold>().IsTriggeredOn((Expression<Func<CAReconEntry, PXWorkflowEventHandlerBase<CARecon>>>) (g => g.OnUpdateStatus)).When((BoundedTo<CAReconEntry, CARecon>.ISharedCondition) conditions.IsOnHold)));
      }));
      transitions.AddGroupFrom<CADocStatus.closed>((Action<BoundedTo<CAReconEntry, CARecon>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<CAReconEntry, CARecon>.Transition.INeedTarget, BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured>) (t => (BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured) t.To<CADocStatus.voided>().IsTriggeredOn((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.Voided)).When((BoundedTo<CAReconEntry, CARecon>.ISharedCondition) conditions.IsVoided)))));
    })))).WithActions((Action<BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.initializeState), (Func<BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IConfigured>) (a => (BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.putOnHold), (Func<BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IConfigured>) (c => (BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<CAReconEntry, CARecon>.Assignment.IContainerFillerFields>) (fas => fas.Add<CARecon.hold>((Func<BoundedTo<CAReconEntry, CARecon>.Assignment.INeedRightOperand, BoundedTo<CAReconEntry, CARecon>.Assignment.IConfigured>) (f => (BoundedTo<CAReconEntry, CARecon>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.releaseFromHold), (Func<BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IConfigured>) (c => (BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 2).WithFieldAssignments((Action<BoundedTo<CAReconEntry, CARecon>.Assignment.IContainerFillerFields>) (fas => fas.Add<CARecon.hold>((Func<BoundedTo<CAReconEntry, CARecon>.Assignment.INeedRightOperand, BoundedTo<CAReconEntry, CARecon>.Assignment.IConfigured>) (f => (BoundedTo<CAReconEntry, CARecon>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.Release), (Func<BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IConfigured>) (c => (BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IConfigured) c.InFolder(processingCategory).PlaceAfter("Last")));
      actions.Add((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.Voided), (Func<BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IConfigured>) (c => (BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IConfigured) c.InFolder(correctionsCategory)));
      actions.Add((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.printReconciliationReport), (Func<BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IConfigured>) (c => (BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IConfigured) c.InFolder((FolderType) 2)));
    })).WithHandlers((Action<BoundedTo<CAReconEntry, CARecon>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers => handlers.Add((Func<BoundedTo<CAReconEntry, CARecon>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<CAReconEntry, CARecon>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<CAReconEntry, CARecon>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<CAReconEntry, CARecon>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<CARecon>) ((BoundedTo<CAReconEntry, CARecon>.WorkflowEventHandlerDefinition.INeedSubscriber<CARecon>) handler.WithTargetOf<CARecon>().OfFieldUpdated<CARecon.hold>()).Is((Expression<Func<CARecon, PXWorkflowEventHandler<CARecon, CARecon>>>) (g => g.OnUpdateStatus))).UsesTargetAsPrimaryEntity())))).WithCategories((Action<BoundedTo<CAReconEntry, CARecon>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(correctionsCategory);
      categories.Add(approvalCategory);
      categories.Update((FolderType) 2, (Func<BoundedTo<CAReconEntry, CARecon>.ActionCategory.ConfiguratorCategory, BoundedTo<CAReconEntry, CARecon>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(approvalCategory)));
    }))));
  }

  public class Conditions : BoundedTo<CAReconEntry, CARecon>.Condition.Pack
  {
    public BoundedTo<CAReconEntry, CARecon>.Condition IsOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CAReconEntry, CARecon>.Condition.ConditionBuilder, BoundedTo<CAReconEntry, CARecon>.Condition>) (c => c.FromBql<BqlOperand<CARecon.hold, IBqlBool>.IsEqual<True>>()), nameof (IsOnHold));
      }
    }

    public BoundedTo<CAReconEntry, CARecon>.Condition IsNotOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CAReconEntry, CARecon>.Condition.ConditionBuilder, BoundedTo<CAReconEntry, CARecon>.Condition>) (c => c.FromBql<BqlOperand<CARecon.hold, IBqlBool>.IsEqual<False>>()), nameof (IsNotOnHold));
      }
    }

    public BoundedTo<CAReconEntry, CARecon>.Condition IsVoided
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CAReconEntry, CARecon>.Condition.ConditionBuilder, BoundedTo<CAReconEntry, CARecon>.Condition>) (c => c.FromBql<BqlOperand<CARecon.voided, IBqlBool>.IsEqual<True>>()), nameof (IsVoided));
      }
    }
  }

  public static class CategoryNames
  {
    public const string Processing = "Processing";
    public const string Corrections = "Corrections";
    public const string Approval = "Approval";
  }

  public static class CategoryID
  {
    public const string Processing = "ProcessingID";
    public const string Corrections = "CorrectionsID";
    public const string Approval = "ApprovalID";
  }
}
