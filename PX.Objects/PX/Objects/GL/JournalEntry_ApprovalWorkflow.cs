// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.JournalEntry_ApprovalWorkflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.EP;
using System;
using System.Collections;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.GL;

public class JournalEntry_ApprovalWorkflow : PXGraphExtension<JournalEntry_Workflow, JournalEntry>
{
  public PXAction<Batch> approve;
  public PXAction<Batch> reject;
  public PXAction<Batch> reassignApproval;

  [PXWorkflowDependsOnType(new Type[] {typeof (GLSetupApproval)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    JournalEntry_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<JournalEntry, Batch>());
  }

  protected static void Configure(WorkflowContext<JournalEntry, Batch> context)
  {
    JournalEntry_ApprovalWorkflow.Conditions conditions = context.Conditions.GetPack<JournalEntry_ApprovalWorkflow.Conditions>();
    BoundedTo<JournalEntry, Batch>.ActionCategory.IConfigured approvalCategory = context.Categories.Get("Approval");
    BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured approveAction = context.ActionDefinitions.CreateExisting<JournalEntry_ApprovalWorkflow>((Expression<Func<JournalEntry_ApprovalWorkflow, PXAction<Batch>>>) (g => g.approve), (Func<BoundedTo<JournalEntry, Batch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured>) (a => (BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.post)).IsHiddenWhen((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<JournalEntry, Batch>.Assignment.IContainerFillerFields>) (fa => fa.Add<Batch.approved>((Func<BoundedTo<JournalEntry, Batch>.Assignment.INeedRightOperand, BoundedTo<JournalEntry, Batch>.Assignment.IConfigured>) (e => (BoundedTo<JournalEntry, Batch>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured rejectAction = context.ActionDefinitions.CreateExisting<JournalEntry_ApprovalWorkflow>((Expression<Func<JournalEntry_ApprovalWorkflow, PXAction<Batch>>>) (g => g.reject), (Func<BoundedTo<JournalEntry, Batch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured>) (a => (BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory, approveAction).PlaceAfter(approveAction).IsHiddenWhen((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<JournalEntry, Batch>.Assignment.IContainerFillerFields>) (fa => fa.Add<Batch.rejected>((Func<BoundedTo<JournalEntry, Batch>.Assignment.INeedRightOperand, BoundedTo<JournalEntry, Batch>.Assignment.IConfigured>) (e => (BoundedTo<JournalEntry, Batch>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured reassignAction = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<JournalEntry, Batch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured>) (a => (BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter(rejectAction).IsHiddenWhen((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsApprovalDisabled)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<JournalEntry, Batch>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<JournalEntry, Batch>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow(new Func<BoundedTo<JournalEntry, Batch>.Workflow.ConfiguratorFlow, BoundedTo<JournalEntry, Batch>.Workflow.ConfiguratorFlow>(InjectApprovalWorkflow)).WithActions((Action<BoundedTo<JournalEntry, Batch>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approveAction);
      actions.Add(rejectAction);
      actions.Add(reassignAction);
      actions.Update((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.putOnHold), (Func<BoundedTo<JournalEntry, Batch>.ActionDefinition.ConfiguratorAction, BoundedTo<JournalEntry, Batch>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((Action<BoundedTo<JournalEntry, Batch>.Assignment.FieldContainerAdjusterAssignment>) (fas =>
      {
        fas.Add<Batch.approved>((Func<BoundedTo<JournalEntry, Batch>.Assignment.INeedRightOperand, BoundedTo<JournalEntry, Batch>.Assignment.IConfigured>) (f => (BoundedTo<JournalEntry, Batch>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fas.Add<Batch.rejected>((Func<BoundedTo<JournalEntry, Batch>.Assignment.INeedRightOperand, BoundedTo<JournalEntry, Batch>.Assignment.IConfigured>) (f => (BoundedTo<JournalEntry, Batch>.Assignment.IConfigured) f.SetFromValue((object) false)));
      }))));
    }))));

    BoundedTo<JournalEntry, Batch>.Workflow.ConfiguratorFlow InjectApprovalWorkflow(
      BoundedTo<JournalEntry, Batch>.Workflow.ConfiguratorFlow flow)
    {
      return flow.WithFlowStates((Action<BoundedTo<JournalEntry, Batch>.BaseFlowStep.ContainerAdjusterStates>) (states =>
      {
        states.Add<BatchStatus.pendingApproval>((Func<BoundedTo<JournalEntry, Batch>.FlowState.INeedAnyFlowStateConfig, BoundedTo<JournalEntry, Batch>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<JournalEntry, Batch>.BaseFlowStep.IConfigured) ((BoundedTo<JournalEntry, Batch>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<JournalEntry, Batch>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<JournalEntry, Batch>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add(approveAction, (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) (a => (BoundedTo<JournalEntry, Batch>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add(rejectAction, (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) (a => (BoundedTo<JournalEntry, Batch>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add(reassignAction, (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.putOnHold), (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.createSchedule), (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) null);
        }))).WithFieldStates((Action<BoundedTo<JournalEntry, Batch>.FieldState.IContainerFillerFields>) (fields =>
        {
          fields.AddAllFields<Batch>((Func<BoundedTo<JournalEntry, Batch>.FieldState.INeedAnyConfigField, BoundedTo<JournalEntry, Batch>.FieldState.IConfigured>) (table => (BoundedTo<JournalEntry, Batch>.FieldState.IConfigured) table.IsDisabled()));
          fields.AddField<Batch.batchNbr>((Func<BoundedTo<JournalEntry, Batch>.FieldState.INeedAnyConfigField, BoundedTo<JournalEntry, Batch>.FieldState.IConfigured>) null);
          fields.AddField<Batch.module>((Func<BoundedTo<JournalEntry, Batch>.FieldState.INeedAnyConfigField, BoundedTo<JournalEntry, Batch>.FieldState.IConfigured>) null);
          fields.AddTable<GLTran>((Func<BoundedTo<JournalEntry, Batch>.FieldState.INeedAnyConfigField, BoundedTo<JournalEntry, Batch>.FieldState.IConfigured>) (table => (BoundedTo<JournalEntry, Batch>.FieldState.IConfigured) table.IsDisabled()));
        }))).WithEventHandlers((Action<BoundedTo<JournalEntry, Batch>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<JournalEntry, PXWorkflowEventHandler<Batch>>>) (g => g.OnConfirmSchedule))))));
        states.Add<BatchStatus.rejected>((Func<BoundedTo<JournalEntry, Batch>.FlowState.INeedAnyFlowStateConfig, BoundedTo<JournalEntry, Batch>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<JournalEntry, Batch>.BaseFlowStep.IConfigured) ((BoundedTo<JournalEntry, Batch>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<JournalEntry, Batch>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.putOnHold), (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) (a => (BoundedTo<JournalEntry, Batch>.ActionState.IConfigured) a.IsDuplicatedInToolbar()))))).WithFieldStates((Action<BoundedTo<JournalEntry, Batch>.FieldState.IContainerFillerFields>) (fields =>
        {
          fields.AddAllFields<Batch>((Func<BoundedTo<JournalEntry, Batch>.FieldState.INeedAnyConfigField, BoundedTo<JournalEntry, Batch>.FieldState.IConfigured>) (table => (BoundedTo<JournalEntry, Batch>.FieldState.IConfigured) table.IsDisabled()));
          fields.AddField<Batch.batchNbr>((Func<BoundedTo<JournalEntry, Batch>.FieldState.INeedAnyConfigField, BoundedTo<JournalEntry, Batch>.FieldState.IConfigured>) null);
          fields.AddField<Batch.module>((Func<BoundedTo<JournalEntry, Batch>.FieldState.INeedAnyConfigField, BoundedTo<JournalEntry, Batch>.FieldState.IConfigured>) null);
          fields.AddTable<GLTran>((Func<BoundedTo<JournalEntry, Batch>.FieldState.INeedAnyConfigField, BoundedTo<JournalEntry, Batch>.FieldState.IConfigured>) (table => (BoundedTo<JournalEntry, Batch>.FieldState.IConfigured) table.IsDisabled()));
        }))));
      })).WithTransitions((Action<BoundedTo<JournalEntry, Batch>.Transition.ContainerAdjusterTransitions>) (transitions =>
      {
        transitions.UpdateGroupFrom("_", (Action<BoundedTo<JournalEntry, Batch>.Transition.SourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.pendingApproval>().IsTriggeredOn((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.initializeState)).When((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsNotOnHoldAndIsNotApproved)));
          ts.Update((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.balanced>().IsTriggeredOn((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.initializeState))), (Func<BoundedTo<JournalEntry, Batch>.Transition.ConfiguratorTransition, BoundedTo<JournalEntry, Batch>.Transition.ConfiguratorTransition>) (t => t.When((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsNotOnHoldAndIsApproved)));
        }));
        transitions.UpdateGroupFrom<BatchStatus.hold>((Action<BoundedTo<JournalEntry, Batch>.Transition.SourceContainerFillerTransitions>) (ts =>
        {
          ts.Update((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.balanced>().IsTriggeredOn((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.releaseFromHold))), (Func<BoundedTo<JournalEntry, Batch>.Transition.ConfiguratorTransition, BoundedTo<JournalEntry, Batch>.Transition.ConfiguratorTransition>) (t => t.When((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsApproved)));
          ts.Update((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.balanced>().IsTriggeredOn((Expression<Func<JournalEntry, PXWorkflowEventHandlerBase<Batch>>>) (g => g.OnUpdateStatus))), (Func<BoundedTo<JournalEntry, Batch>.Transition.ConfiguratorTransition, BoundedTo<JournalEntry, Batch>.Transition.ConfiguratorTransition>) (t => t.When((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsApproved)));
          ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.pendingApproval>().IsTriggeredOn((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.releaseFromHold)).DoesNotPersist().When((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsNotApproved)));
          ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.rejected>().IsTriggeredOn((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.releaseFromHold)).DoesNotPersist().When((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsRejected)));
          ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.pendingApproval>().IsTriggeredOn((Expression<Func<JournalEntry, PXWorkflowEventHandlerBase<Batch>>>) (g => g.OnUpdateStatus)).DoesNotPersist().When((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsNotApproved)));
          ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.rejected>().IsTriggeredOn((Expression<Func<JournalEntry, PXWorkflowEventHandlerBase<Batch>>>) (g => g.OnUpdateStatus)).DoesNotPersist().When((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsRejected)));
        }));
        transitions.AddGroupFrom<BatchStatus.pendingApproval>((Action<BoundedTo<JournalEntry, Batch>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.balanced>().IsTriggeredOn(approveAction).When((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsApproved)));
          ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.rejected>().IsTriggeredOn(rejectAction).When((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsRejected)));
          ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.hold>().IsTriggeredOn((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.putOnHold)).DoesNotPersist()));
          ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.scheduled>().IsTriggeredOn((Expression<Func<JournalEntry, PXWorkflowEventHandlerBase<Batch>>>) (g => g.OnConfirmSchedule)).WithFieldAssignments((Action<BoundedTo<JournalEntry, Batch>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<Batch.scheduled>((Func<BoundedTo<JournalEntry, Batch>.Assignment.INeedRightOperand, BoundedTo<JournalEntry, Batch>.Assignment.IConfigured>) (e => (BoundedTo<JournalEntry, Batch>.Assignment.IConfigured) e.SetFromValue((object) true)));
            fas.Add<Batch.scheduleID>((Func<BoundedTo<JournalEntry, Batch>.Assignment.INeedRightOperand, BoundedTo<JournalEntry, Batch>.Assignment.IConfigured>) (e => (BoundedTo<JournalEntry, Batch>.Assignment.IConfigured) e.SetFromExpression("@ScheduleID")));
          }))));
        }));
        transitions.AddGroupFrom<BatchStatus.rejected>((Action<BoundedTo<JournalEntry, Batch>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.hold>().IsTriggeredOn((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.putOnHold)).DoesNotPersist()))));
      }));
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable Approve(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable Reject(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReassignApproval(PXAdapter adapter)
  {
    return ((PXGraphExtension<JournalEntry>) this).Base.Approval.ReassignApproval(adapter);
  }

  public class Conditions : BoundedTo<JournalEntry, Batch>.Condition.Pack
  {
    public BoundedTo<JournalEntry, Batch>.Condition IsApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<JournalEntry, Batch>.Condition.ConditionBuilder, BoundedTo<JournalEntry, Batch>.Condition>) (b => b.FromBql<BqlOperand<Batch.approved, IBqlBool>.IsEqual<True>>()), nameof (IsApproved));
      }
    }

    public BoundedTo<JournalEntry, Batch>.Condition IsRejected
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<JournalEntry, Batch>.Condition.ConditionBuilder, BoundedTo<JournalEntry, Batch>.Condition>) (b => b.FromBql<BqlOperand<Batch.rejected, IBqlBool>.IsEqual<True>>()), nameof (IsRejected));
      }
    }

    public BoundedTo<JournalEntry, Batch>.Condition IsNotApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<JournalEntry, Batch>.Condition.ConditionBuilder, BoundedTo<JournalEntry, Batch>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Batch.approved, Equal<False>>>>>.And<BqlOperand<Batch.rejected, IBqlBool>.IsEqual<False>>>()), nameof (IsNotApproved));
      }
    }

    public BoundedTo<JournalEntry, Batch>.Condition IsNotOnHoldAndIsApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<JournalEntry, Batch>.Condition.ConditionBuilder, BoundedTo<JournalEntry, Batch>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Batch.hold, Equal<False>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Batch.approved, Equal<True>>>>>.And<BqlOperand<Batch.released, IBqlBool>.IsEqual<False>>>>()), nameof (IsNotOnHoldAndIsApproved));
      }
    }

    public BoundedTo<JournalEntry, Batch>.Condition IsNotOnHoldAndIsNotApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<JournalEntry, Batch>.Condition.ConditionBuilder, BoundedTo<JournalEntry, Batch>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Batch.hold, Equal<False>>>>>.And<BqlOperand<Batch.approved, IBqlBool>.IsEqual<False>>>()), nameof (IsNotOnHoldAndIsNotApproved));
      }
    }

    public BoundedTo<JournalEntry, Batch>.Condition IsApprovalDisabled
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<JournalEntry, Batch>.Condition.ConditionBuilder, BoundedTo<JournalEntry, Batch>.Condition>) (b => b.FromBql<Not<EPApprovalSettings<GLSetupApproval, GLSetupApproval.batchType, BatchTypeCode, BatchStatus.hold, BatchStatus.pendingApproval, BatchStatus.rejected>.IsDocumentApprovable<Batch.batchType, Batch.status>>>()), nameof (IsApprovalDisabled));
      }
    }

    public BoundedTo<JournalEntry, Batch>.Condition NonEditable
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<JournalEntry, Batch>.Condition.ConditionBuilder, BoundedTo<JournalEntry, Batch>.Condition>) (b => b.FromBql<EPApprovalSettings<GLSetupApproval, GLSetupApproval.batchType, BatchTypeCode, BatchStatus.hold, BatchStatus.pendingApproval, BatchStatus.rejected>.IsDocumentLockedByApproval<Batch.batchType, Batch.status>>()), nameof (NonEditable));
      }
    }
  }
}
