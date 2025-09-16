// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CATranEntry_Workflow
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
namespace PX.Objects.CA;

public class CATranEntry_Workflow : PXGraphExtension<CATranEntry>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    CATranEntry_Workflow.Configure(config.GetScreenConfigurationContext<CATranEntry, CAAdj>());
  }

  protected static void Configure(WorkflowContext<CATranEntry, CAAdj> context)
  {
    CATranEntry_Workflow.Conditions conditions = context.Conditions.GetPack<CATranEntry_Workflow.Conditions>();
    BoundedTo<CATranEntry, CAAdj>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("ProcessingID", (Func<BoundedTo<CATranEntry, CAAdj>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CATranEntry, CAAdj>.ActionCategory.IConfigured>) (category => (BoundedTo<CATranEntry, CAAdj>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<CATranEntry, CAAdj>.ActionCategory.IConfigured correctionsCategory = context.Categories.CreateNew("CorrectionsID", (Func<BoundedTo<CATranEntry, CAAdj>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CATranEntry, CAAdj>.ActionCategory.IConfigured>) (category => (BoundedTo<CATranEntry, CAAdj>.ActionCategory.IConfigured) category.DisplayName("Corrections")));
    BoundedTo<CATranEntry, CAAdj>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("ApprovalID", (Func<BoundedTo<CATranEntry, CAAdj>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CATranEntry, CAAdj>.ActionCategory.IConfigured>) (category => (BoundedTo<CATranEntry, CAAdj>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    context.AddScreenConfigurationFor((Func<BoundedTo<CATranEntry, CAAdj>.ScreenConfiguration.IStartConfigScreen, BoundedTo<CATranEntry, CAAdj>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<CATranEntry, CAAdj>.ScreenConfiguration.IConfigured) ((BoundedTo<CATranEntry, CAAdj>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<CAAdj.status>().AddDefaultFlow((Func<BoundedTo<CATranEntry, CAAdj>.Workflow.INeedStatesFlow, BoundedTo<CATranEntry, CAAdj>.Workflow.IConfigured>) (flow => (BoundedTo<CATranEntry, CAAdj>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<CATranEntry, CAAdj>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<CATranEntry, CAAdj>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CATranEntry, CAAdj>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CATranEntry, CAAdj>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<CATranEntry, PXAutoAction<CAAdj>>>) (g => g.initializeState))));
      fss.Add<CATransferStatus.hold>((Func<BoundedTo<CATranEntry, CAAdj>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CATranEntry, CAAdj>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CATranEntry, CAAdj>.BaseFlowStep.IConfigured) ((BoundedTo<CATranEntry, CAAdj>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<CATranEntry, CAAdj>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.releaseFromHold), (Func<BoundedTo<CATranEntry, CAAdj>.ActionState.IAllowOptionalConfig, BoundedTo<CATranEntry, CAAdj>.ActionState.IConfigured>) (a => (BoundedTo<CATranEntry, CAAdj>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)))))).WithEventHandlers((Action<BoundedTo<CATranEntry, CAAdj>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<CATranEntry, PXWorkflowEventHandler<CAAdj>>>) (g => g.OnUpdateStatus))))));
      fss.Add<CATransferStatus.balanced>((Func<BoundedTo<CATranEntry, CAAdj>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CATranEntry, CAAdj>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CATranEntry, CAAdj>.BaseFlowStep.IConfigured) ((BoundedTo<CATranEntry, CAAdj>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<CATranEntry, CAAdj>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.Release), (Func<BoundedTo<CATranEntry, CAAdj>.ActionState.IAllowOptionalConfig, BoundedTo<CATranEntry, CAAdj>.ActionState.IConfigured>) (a => (BoundedTo<CATranEntry, CAAdj>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.putOnHold), (Func<BoundedTo<CATranEntry, CAAdj>.ActionState.IAllowOptionalConfig, BoundedTo<CATranEntry, CAAdj>.ActionState.IConfigured>) (a => (BoundedTo<CATranEntry, CAAdj>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
      }))).WithEventHandlers((Action<BoundedTo<CATranEntry, CAAdj>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<CATranEntry, PXWorkflowEventHandler<CAAdj>>>) (g => g.OnReleaseDocument));
        handlers.Add((Expression<Func<CATranEntry, PXWorkflowEventHandler<CAAdj>>>) (g => g.OnUpdateStatus));
      }))));
      fss.Add<CATransferStatus.released>((Func<BoundedTo<CATranEntry, CAAdj>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CATranEntry, CAAdj>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CATranEntry, CAAdj>.BaseFlowStep.IConfigured) ((BoundedTo<CATranEntry, CAAdj>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<CATranEntry, CAAdj>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.Reverse), (Func<BoundedTo<CATranEntry, CAAdj>.ActionState.IAllowOptionalConfig, BoundedTo<CATranEntry, CAAdj>.ActionState.IConfigured>) (a => (BoundedTo<CATranEntry, CAAdj>.ActionState.IConfigured) a.IsDuplicatedInToolbar()))))).WithFieldStates((Action<BoundedTo<CATranEntry, CAAdj>.FieldState.IContainerFillerFields>) (fields =>
      {
        fields.AddAllFields<CAAdj>((Func<BoundedTo<CATranEntry, CAAdj>.FieldState.INeedAnyConfigField, BoundedTo<CATranEntry, CAAdj>.FieldState.IConfigured>) (table => (BoundedTo<CATranEntry, CAAdj>.FieldState.IConfigured) table.IsDisabled()));
        fields.AddField<CAAdj.depositAsBatch>((Func<BoundedTo<CATranEntry, CAAdj>.FieldState.INeedAnyConfigField, BoundedTo<CATranEntry, CAAdj>.FieldState.IConfigured>) null);
        fields.AddField<CAAdj.adjRefNbr>((Func<BoundedTo<CATranEntry, CAAdj>.FieldState.INeedAnyConfigField, BoundedTo<CATranEntry, CAAdj>.FieldState.IConfigured>) null);
        fields.AddTable<CASplit>((Func<BoundedTo<CATranEntry, CAAdj>.FieldState.INeedAnyConfigField, BoundedTo<CATranEntry, CAAdj>.FieldState.IConfigured>) (table => (BoundedTo<CATranEntry, CAAdj>.FieldState.IConfigured) table.IsDisabled()));
        fields.AddTable<CATaxTran>((Func<BoundedTo<CATranEntry, CAAdj>.FieldState.INeedAnyConfigField, BoundedTo<CATranEntry, CAAdj>.FieldState.IConfigured>) (table => (BoundedTo<CATranEntry, CAAdj>.FieldState.IConfigured) table.IsDisabled()));
      }))));
    })).WithTransitions((Action<BoundedTo<CATranEntry, CAAdj>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<CATranEntry, CAAdj>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<CATranEntry, CAAdj>.Transition.INeedTarget, BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured>) (t => (BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured) t.To<CATransferStatus.hold>().IsTriggeredOn((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.initializeState)).When((BoundedTo<CATranEntry, CAAdj>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<CATranEntry, CAAdj>.Transition.INeedTarget, BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured>) (t => (BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured) t.To<CATransferStatus.balanced>().IsTriggeredOn((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.initializeState)).When((BoundedTo<CATranEntry, CAAdj>.ISharedCondition) conditions.IsBalanced)));
      }));
      transitions.AddGroupFrom<CATransferStatus.hold>((Action<BoundedTo<CATranEntry, CAAdj>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<CATranEntry, CAAdj>.Transition.INeedTarget, BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured>) (t => (BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured) t.To<CATransferStatus.balanced>().IsTriggeredOn((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.releaseFromHold)).WithFieldAssignments((Action<BoundedTo<CATranEntry, CAAdj>.Assignment.IContainerFillerFields>) (fas => fas.Add<CAAdj.hold>((Func<BoundedTo<CATranEntry, CAAdj>.Assignment.INeedRightOperand, BoundedTo<CATranEntry, CAAdj>.Assignment.IConfigured>) (f => (BoundedTo<CATranEntry, CAAdj>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
        ts.Add((Func<BoundedTo<CATranEntry, CAAdj>.Transition.INeedTarget, BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured>) (t => (BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured) t.To<CATransferStatus.balanced>().IsTriggeredOn((Expression<Func<CATranEntry, PXWorkflowEventHandlerBase<CAAdj>>>) (g => g.OnUpdateStatus)).When((BoundedTo<CATranEntry, CAAdj>.ISharedCondition) conditions.IsBalanced)));
      }));
      transitions.AddGroupFrom<CATransferStatus.balanced>((Action<BoundedTo<CATranEntry, CAAdj>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<CATranEntry, CAAdj>.Transition.INeedTarget, BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured>) (t => (BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured) t.To<CATransferStatus.hold>().IsTriggeredOn((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.putOnHold)).WithFieldAssignments((Action<BoundedTo<CATranEntry, CAAdj>.Assignment.IContainerFillerFields>) (fas => fas.Add<CAAdj.hold>((Func<BoundedTo<CATranEntry, CAAdj>.Assignment.INeedRightOperand, BoundedTo<CATranEntry, CAAdj>.Assignment.IConfigured>) (f => (BoundedTo<CATranEntry, CAAdj>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        ts.Add((Func<BoundedTo<CATranEntry, CAAdj>.Transition.INeedTarget, BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured>) (t => (BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured) t.To<CATransferStatus.released>().IsTriggeredOn((Expression<Func<CATranEntry, PXWorkflowEventHandlerBase<CAAdj>>>) (g => g.OnReleaseDocument))));
        ts.Add((Func<BoundedTo<CATranEntry, CAAdj>.Transition.INeedTarget, BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured>) (t => (BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured) t.To<CATransferStatus.hold>().IsTriggeredOn((Expression<Func<CATranEntry, PXWorkflowEventHandlerBase<CAAdj>>>) (g => g.OnUpdateStatus)).When((BoundedTo<CATranEntry, CAAdj>.ISharedCondition) conditions.IsOnHold)));
      }));
    })))).WithActions((Action<BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.initializeState), (Func<BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IConfigured>) (a => (BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.putOnHold), (Func<BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IConfigured>) (c => (BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<CATranEntry, CAAdj>.Assignment.IContainerFillerFields>) (fas => fas.Add<CAAdj.hold>((Func<BoundedTo<CATranEntry, CAAdj>.Assignment.INeedRightOperand, BoundedTo<CATranEntry, CAAdj>.Assignment.IConfigured>) (f => (BoundedTo<CATranEntry, CAAdj>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.releaseFromHold), (Func<BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IConfigured>) (c => (BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<CATranEntry, CAAdj>.Assignment.IContainerFillerFields>) (fas => fas.Add<CAAdj.hold>((Func<BoundedTo<CATranEntry, CAAdj>.Assignment.INeedRightOperand, BoundedTo<CATranEntry, CAAdj>.Assignment.IConfigured>) (f => (BoundedTo<CATranEntry, CAAdj>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.Release), (Func<BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IConfigured>) (c => (BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IConfigured) c.InFolder(processingCategory).PlaceAfter("Last").IsDisabledWhen((BoundedTo<CATranEntry, CAAdj>.ISharedCondition) conditions.IsNotAdjustment)));
      actions.Add((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.Reverse), (Func<BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IConfigured>) (c => (BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IConfigured) c.InFolder(correctionsCategory).IsDisabledWhen((BoundedTo<CATranEntry, CAAdj>.ISharedCondition) conditions.IsNotAdjustment)));
    })).WithHandlers((Action<BoundedTo<CATranEntry, CAAdj>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers =>
    {
      handlers.Add((Func<BoundedTo<CATranEntry, CAAdj>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<CATranEntry, CAAdj>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<CATranEntry, CAAdj>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<CATranEntry, CAAdj>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<CAAdj>) ((BoundedTo<CATranEntry, CAAdj>.WorkflowEventHandlerDefinition.INeedSubscriber<CAAdj>) handler.WithTargetOf<CAAdj>().OfEntityEvent<CAAdj.Events>((Expression<Func<CAAdj.Events, PXEntityEvent<CAAdj>>>) (e => e.ReleaseDocument))).Is((Expression<Func<CAAdj, PXWorkflowEventHandler<CAAdj, CAAdj>>>) (g => g.OnReleaseDocument))).UsesTargetAsPrimaryEntity()));
      handlers.Add((Func<BoundedTo<CATranEntry, CAAdj>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<CATranEntry, CAAdj>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<CATranEntry, CAAdj>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<CATranEntry, CAAdj>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<CAAdj>) ((BoundedTo<CATranEntry, CAAdj>.WorkflowEventHandlerDefinition.INeedSubscriber<CAAdj>) handler.WithTargetOf<CAAdj>().OfFieldUpdated<CAAdj.hold>()).Is((Expression<Func<CAAdj, PXWorkflowEventHandler<CAAdj, CAAdj>>>) (g => g.OnUpdateStatus))).UsesTargetAsPrimaryEntity()));
    })).WithCategories((Action<BoundedTo<CATranEntry, CAAdj>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(correctionsCategory);
      categories.Add(approvalCategory);
    }))));
  }

  public class Conditions : BoundedTo<CATranEntry, CAAdj>.Condition.Pack
  {
    public BoundedTo<CATranEntry, CAAdj>.Condition IsOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CATranEntry, CAAdj>.Condition.ConditionBuilder, BoundedTo<CATranEntry, CAAdj>.Condition>) (c => c.FromBql<BqlOperand<CAAdj.hold, IBqlBool>.IsEqual<True>>()), nameof (IsOnHold));
      }
    }

    public BoundedTo<CATranEntry, CAAdj>.Condition IsBalanced
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CATranEntry, CAAdj>.Condition.ConditionBuilder, BoundedTo<CATranEntry, CAAdj>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CAAdj.approved, Equal<True>>>>>.And<BqlOperand<CAAdj.hold, IBqlBool>.IsEqual<False>>>()), nameof (IsBalanced));
      }
    }

    public BoundedTo<CATranEntry, CAAdj>.Condition IsNotAdjustment
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CATranEntry, CAAdj>.Condition.ConditionBuilder, BoundedTo<CATranEntry, CAAdj>.Condition>) (c => c.FromBql<BqlOperand<CAAdj.adjTranType, IBqlString>.IsNotEqual<CATranType.cAAdjustment>>()), nameof (IsNotAdjustment));
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
