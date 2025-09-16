// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARCashSaleEntry_ApprovalWorkflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.AR.Standalone;
using PX.Objects.EP;
using System;
using System.Collections;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.AR;

public class ARCashSaleEntry_ApprovalWorkflow : 
  PXGraphExtension<ARCashSaleEntry_Workflow, ARCashSaleEntry>
{
  public PXAction<ARCashSale> approve;
  public PXAction<ARCashSale> reject;

  [PXWorkflowDependsOnType(new Type[] {typeof (ARSetupApproval)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    ARCashSaleEntry_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<ARCashSaleEntry, ARCashSale>());
  }

  protected static void Configure(
    WorkflowContext<ARCashSaleEntry, ARCashSale> context)
  {
    BoundedTo<ARCashSaleEntry, ARCashSale>.ActionCategory.IConfigured approvalCategory = context.Categories.Get("ApprovalID");
    ARCashSaleEntry_ApprovalWorkflow.Conditions conditions = context.Conditions.GetPack<ARCashSaleEntry_ApprovalWorkflow.Conditions>();
    BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured aproveAction = context.ActionDefinitions.CreateExisting<ARCashSaleEntry_ApprovalWorkflow>((Expression<Func<ARCashSaleEntry_ApprovalWorkflow, PXAction<ARCashSale>>>) (g => g.approve), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (a => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory, (Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.releaseFromHold)).PlaceAfter((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.releaseFromHold)).IsHiddenWhen((BoundedTo<ARCashSaleEntry, ARCashSale>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.IContainerFillerFields>) (fa => fa.Add<ARRegister.approved>((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.INeedRightOperand, BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.IConfigured>) (e => (BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured rejectAction = context.ActionDefinitions.CreateExisting<ARCashSaleEntry_ApprovalWorkflow>((Expression<Func<ARCashSaleEntry_ApprovalWorkflow, PXAction<ARCashSale>>>) (g => g.reject), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (a => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory, aproveAction).PlaceAfter(aproveAction).IsHiddenWhen((BoundedTo<ARCashSaleEntry, ARCashSale>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.IContainerFillerFields>) (fa => fa.Add<ARRegister.rejected>((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.INeedRightOperand, BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.IConfigured>) (e => (BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured reassignAction = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured>) (a => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter(rejectAction).IsHiddenWhen((BoundedTo<ARCashSaleEntry, ARCashSale>.ISharedCondition) conditions.IsApprovalDisabled)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<ARCashSaleEntry, ARCashSale>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow(new Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Workflow.ConfiguratorFlow, BoundedTo<ARCashSaleEntry, ARCashSale>.Workflow.ConfiguratorFlow>(InjectApprovalWorkflow)).WithActions((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(aproveAction);
      actions.Add(rejectAction);
      actions.Add(reassignAction);
      actions.Update((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.putOnHold), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.ConfiguratorAction, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.FieldContainerAdjusterAssignment>) (fas =>
      {
        fas.Add<ARRegister.approved>((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.INeedRightOperand, BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.IConfigured>) (f => (BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fas.Add<ARRegister.rejected>((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.INeedRightOperand, BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.IConfigured>) (f => (BoundedTo<ARCashSaleEntry, ARCashSale>.Assignment.IConfigured) f.SetFromValue((object) false)));
      }))));
    }))));

    BoundedTo<ARCashSaleEntry, ARCashSale>.Workflow.ConfiguratorFlow InjectApprovalWorkflow(
      BoundedTo<ARCashSaleEntry, ARCashSale>.Workflow.ConfiguratorFlow flow)
    {
      return flow.WithFlowStates((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.ContainerAdjusterStates>) (states => states.UpdateSequence<ARDocStatus.HoldToBalance>((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Sequence.ConfiguratorSequence, BoundedTo<ARCashSaleEntry, ARCashSale>.Sequence.ConfiguratorSequence>) (seq => seq.WithStates((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.Sequence.ContainerAdjusterSequenceStates>) (sss =>
      {
        ((BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.ContainerAdjusterStates) sss).Add<ARDocStatus.pendingApproval>((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.IConfigured>) (flowState =>
        {
          BoundedTo<ARCashSaleEntry, ARCashSale>.FlowState.INeedAnyFlowStateConfig anyFlowStateConfig = flowState;
          BoundedTo<ARCashSaleEntry, ARCashSale>.Condition isApproved = conditions.IsApproved;
          BoundedTo<ARCashSaleEntry, ARCashSale>.Condition condition = BoundedTo<ARCashSaleEntry, ARCashSale>.Condition.op_True(isApproved) ? isApproved : BoundedTo<ARCashSaleEntry, ARCashSale>.Condition.op_BitwiseOr(isApproved, conditions.IsRejected);
          return (BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.IConfigured) ((BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.INeedAnyConfigState) ((BoundedTo<ARCashSaleEntry, ARCashSale>.FlowState.INeedAnyFlowStateConfig) anyFlowStateConfig.IsSkippedWhen(condition)).WithActions((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IContainerFillerActions>) (actions =>
          {
            actions.Add(aproveAction, (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) (a => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
            actions.Add(rejectAction, (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) (a => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
            actions.Add(reassignAction, (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.putOnHold), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.printAREdit), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.customerDocuments), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
          }))).PlaceAfter<ARDocStatus.hold>();
        }));
        ((BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.ContainerAdjusterStates) sss).Add<ARDocStatus.rejected>((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.IConfigured) ((BoundedTo<ARCashSaleEntry, ARCashSale>.BaseFlowStep.INeedAnyConfigState) ((BoundedTo<ARCashSaleEntry, ARCashSale>.FlowState.INeedAnyFlowStateConfig) flowState.IsSkippedWhen(BoundedTo<ARCashSaleEntry, ARCashSale>.Condition.op_LogicalNot(conditions.IsRejected))).WithActions((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.putOnHold), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) (a => (BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.printAREdit), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.customerDocuments), (Func<BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IAllowOptionalConfig, BoundedTo<ARCashSaleEntry, ARCashSale>.ActionState.IConfigured>) null);
        }))).PlaceAfter<ARDocStatus.pendingApproval>()));
      })))))).WithTransitions((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.ContainerAdjusterTransitions>) (transitions =>
      {
        transitions.AddGroupFrom<ARDocStatus.pendingApproval>((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.INeedTarget, BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.IConfigured>) (t => (BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.IConfigured) t.To<ARDocStatus.HoldToBalance>().IsTriggeredOn((Expression<Func<ARCashSaleEntry, PXWorkflowEventHandlerBase<ARCashSale>>>) (g => g.OnUpdateStatus))));
          ts.Add((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.INeedTarget, BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.IConfigured>) (t => (BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.IConfigured) t.ToNext().IsTriggeredOn(aproveAction).When((BoundedTo<ARCashSaleEntry, ARCashSale>.ISharedCondition) conditions.IsApproved)));
          ts.Add((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.INeedTarget, BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.IConfigured>) (t => (BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.IConfigured) t.To<ARDocStatus.rejected>().IsTriggeredOn(rejectAction).When((BoundedTo<ARCashSaleEntry, ARCashSale>.ISharedCondition) conditions.IsRejected)));
        }));
        transitions.AddGroupFrom<ARDocStatus.rejected>((Action<BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.INeedTarget, BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.IConfigured>) (t => (BoundedTo<ARCashSaleEntry, ARCashSale>.Transition.IConfigured) t.To<ARDocStatus.hold>().IsTriggeredOn((Expression<Func<ARCashSaleEntry, PXAction<ARCashSale>>>) (g => g.putOnHold)).DoesNotPersist()))));
      }));
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable Approve(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable Reject(PXAdapter adapter) => adapter.Get();

  public class Conditions : BoundedTo<ARCashSaleEntry, ARCashSale>.Condition.Pack
  {
    public BoundedTo<ARCashSaleEntry, ARCashSale>.Condition IsApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Condition.ConditionBuilder, BoundedTo<ARCashSaleEntry, ARCashSale>.Condition>) (b => b.FromBql<BqlOperand<ARRegister.approved, IBqlBool>.IsEqual<True>>()), nameof (IsApproved));
      }
    }

    public BoundedTo<ARCashSaleEntry, ARCashSale>.Condition IsRejected
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Condition.ConditionBuilder, BoundedTo<ARCashSaleEntry, ARCashSale>.Condition>) (b => b.FromBql<BqlOperand<ARRegister.rejected, IBqlBool>.IsEqual<True>>()), nameof (IsRejected));
      }
    }

    public BoundedTo<ARCashSaleEntry, ARCashSale>.Condition IsApprovalDisabled
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ARCashSaleEntry, ARCashSale>.Condition.ConditionBuilder, BoundedTo<ARCashSaleEntry, ARCashSale>.Condition>) (b => b.FromBql<Not<EPApprovalSettings<ARSetupApproval, ARSetupApproval.docType, ARDocType, ARDocStatus.hold, ARDocStatus.pendingApproval, ARDocStatus.rejected>.IsDocumentApprovable<ARCashSale.docType, ARCashSale.status>>>()), nameof (IsApprovalDisabled));
      }
    }
  }
}
