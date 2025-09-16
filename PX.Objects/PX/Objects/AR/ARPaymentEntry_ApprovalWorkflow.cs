// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPaymentEntry_ApprovalWorkflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.EP;
using System;
using System.Collections;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.AR;

public class ARPaymentEntry_ApprovalWorkflow : 
  PXGraphExtension<ARPaymentEntry_Workflow, ARPaymentEntry>
{
  public PXAction<ARPayment> approve;
  public PXAction<ARPayment> reject;
  public PXAction<ARPayment> reassignApproval;

  [PXWorkflowDependsOnType(new Type[] {typeof (ARSetupApproval)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    ARPaymentEntry_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<ARPaymentEntry, ARPayment>());
  }

  protected static void Configure(WorkflowContext<ARPaymentEntry, ARPayment> context)
  {
    BoundedTo<ARPaymentEntry, ARPayment>.ActionCategory.IConfigured approvalCategory = context.Categories.Get("ApprovalID");
    ARPaymentEntry_ApprovalWorkflow.Conditions conditions = context.Conditions.GetPack<ARPaymentEntry_ApprovalWorkflow.Conditions>();
    BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured aproveAction = context.ActionDefinitions.CreateExisting<ARPaymentEntry_ApprovalWorkflow>((Expression<Func<ARPaymentEntry_ApprovalWorkflow, PXAction<ARPayment>>>) (g => g.approve), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured>) (a => (BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory, (Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.releaseFromHold)).PlaceAfter((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.releaseFromHold)).WithPersistOptions((ActionPersistOptions) 2).IsHiddenWhen((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IContainerFillerFields>) (fa => fa.Add<ARRegister.approved>((Func<BoundedTo<ARPaymentEntry, ARPayment>.Assignment.INeedRightOperand, BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IConfigured>) (e => (BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured rejectAction = context.ActionDefinitions.CreateExisting<ARPaymentEntry_ApprovalWorkflow>((Expression<Func<ARPaymentEntry_ApprovalWorkflow, PXAction<ARPayment>>>) (g => g.reject), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured>) (a => (BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory, aproveAction).PlaceAfter(aproveAction).IsHiddenWhen((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IContainerFillerFields>) (fa => fa.Add<ARRegister.rejected>((Func<BoundedTo<ARPaymentEntry, ARPayment>.Assignment.INeedRightOperand, BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IConfigured>) (e => (BoundedTo<ARPaymentEntry, ARPayment>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured reassignAction = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured>) (a => (BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory, (Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.releaseFromHold)).PlaceAfter(rejectAction).IsHiddenWhen((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsApprovalDisabled)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<ARPaymentEntry, ARPayment>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<ARPaymentEntry, ARPayment>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow(new Func<BoundedTo<ARPaymentEntry, ARPayment>.Workflow.ConfiguratorFlow, BoundedTo<ARPaymentEntry, ARPayment>.Workflow.ConfiguratorFlow>(InjectApprovalWorkflow)).WithActions((Action<BoundedTo<ARPaymentEntry, ARPayment>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(aproveAction);
      actions.Add(rejectAction);
      actions.Add(reassignAction);
    }))));

    BoundedTo<ARPaymentEntry, ARPayment>.Workflow.ConfiguratorFlow InjectApprovalWorkflow(
      BoundedTo<ARPaymentEntry, ARPayment>.Workflow.ConfiguratorFlow flow)
    {
      return flow.WithFlowStates((Action<BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.ContainerAdjusterStates>) (states => states.UpdateSequence<ARDocStatus.HoldToBalance>((Func<BoundedTo<ARPaymentEntry, ARPayment>.Sequence.ConfiguratorSequence, BoundedTo<ARPaymentEntry, ARPayment>.Sequence.ConfiguratorSequence>) (seq => seq.WithStates((Action<BoundedTo<ARPaymentEntry, ARPayment>.Sequence.ContainerAdjusterSequenceStates>) (sss =>
      {
        ((BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.ContainerAdjusterStates) sss).Add<ARDocStatus.pendingApproval>((Func<BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured>) (flowState =>
        {
          BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig anyFlowStateConfig = flowState;
          BoundedTo<ARPaymentEntry, ARPayment>.Condition isApproved = conditions.IsApproved;
          BoundedTo<ARPaymentEntry, ARPayment>.Condition condition = BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_True(isApproved) ? isApproved : BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_BitwiseOr(isApproved, conditions.IsRejected);
          return (BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured) ((BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.INeedAnyConfigState) ((BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig) anyFlowStateConfig.IsSkippedWhen(condition)).WithActions((Action<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IContainerFillerActions>) (actions =>
          {
            actions.Add(aproveAction, (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) (a => (BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
            actions.Add(rejectAction, (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) (a => (BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
            actions.Add(reassignAction, (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.putOnHold), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.printAREdit), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.customerDocuments), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
          }))).PlaceAfter<ARDocStatus.hold>();
        }));
        ((BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.ContainerAdjusterStates) sss).Add<ARDocStatus.rejected>((Func<BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.IConfigured) ((BoundedTo<ARPaymentEntry, ARPayment>.BaseFlowStep.INeedAnyConfigState) ((BoundedTo<ARPaymentEntry, ARPayment>.FlowState.INeedAnyFlowStateConfig) flowState.IsSkippedWhen(BoundedTo<ARPaymentEntry, ARPayment>.Condition.op_LogicalNot(conditions.IsRejected))).WithActions((Action<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.putOnHold), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) (a => (BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.printAREdit), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.customerDocuments), (Func<BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IAllowOptionalConfig, BoundedTo<ARPaymentEntry, ARPayment>.ActionState.IConfigured>) null);
        }))).PlaceAfter<ARDocStatus.pendingApproval>()));
      })))))).WithTransitions((Action<BoundedTo<ARPaymentEntry, ARPayment>.Transition.ContainerAdjusterTransitions>) (transitions =>
      {
        transitions.AddGroupFrom<ARDocStatus.pendingApproval>((Action<BoundedTo<ARPaymentEntry, ARPayment>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.HoldToBalance>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXWorkflowEventHandlerBase<ARPayment>>>) (g => g.OnUpdateStatus))));
          ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.ToNext().IsTriggeredOn(aproveAction).When((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsApproved)));
          ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.rejected>().IsTriggeredOn(rejectAction).When((BoundedTo<ARPaymentEntry, ARPayment>.ISharedCondition) conditions.IsRejected)));
        }));
        transitions.AddGroupFrom<ARDocStatus.rejected>((Action<BoundedTo<ARPaymentEntry, ARPayment>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ARPaymentEntry, ARPayment>.Transition.INeedTarget, BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured>) (t => (BoundedTo<ARPaymentEntry, ARPayment>.Transition.IConfigured) t.To<ARDocStatus.hold>().IsTriggeredOn((Expression<Func<ARPaymentEntry, PXAction<ARPayment>>>) (g => g.putOnHold)).DoesNotPersist()))));
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
    return ((PXGraphExtension<ARPaymentEntry>) this).Base.Approval.ReassignApproval(adapter);
  }

  public class Conditions : BoundedTo<ARPaymentEntry, ARPayment>.Condition.Pack
  {
    public BoundedTo<ARPaymentEntry, ARPayment>.Condition IsApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARPaymentEntry, ARPayment>.Condition.ConditionBuilder, BoundedTo<ARPaymentEntry, ARPayment>.Condition>) (b => b.FromBql<BqlOperand<ARRegister.approved, IBqlBool>.IsEqual<True>>()), nameof (IsApproved));
      }
    }

    public BoundedTo<ARPaymentEntry, ARPayment>.Condition IsRejected
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARPaymentEntry, ARPayment>.Condition.ConditionBuilder, BoundedTo<ARPaymentEntry, ARPayment>.Condition>) (b => b.FromBql<BqlOperand<ARRegister.rejected, IBqlBool>.IsEqual<True>>()), nameof (IsRejected));
      }
    }

    public BoundedTo<ARPaymentEntry, ARPayment>.Condition IsApprovalDisabled
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARPaymentEntry, ARPayment>.Condition.ConditionBuilder, BoundedTo<ARPaymentEntry, ARPayment>.Condition>) (b => b.FromBql<Not<EPApprovalSettings<ARSetupApproval, ARSetupApproval.docType, ARDocType, ARDocStatus.hold, ARDocStatus.pendingApproval, ARDocStatus.rejected>.IsDocumentApprovable<ARPayment.docType, ARPayment.status>>>()), nameof (IsApprovalDisabled));
      }
    }
  }
}
