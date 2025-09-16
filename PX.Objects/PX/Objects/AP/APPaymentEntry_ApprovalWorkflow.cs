// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APPaymentEntry_ApprovalWorkflow
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
namespace PX.Objects.AP;

public class APPaymentEntry_ApprovalWorkflow : 
  PXGraphExtension<APPaymentEntry_Workflow, APPaymentEntry>
{
  public PXAction<APPayment> approve;
  public PXAction<APPayment> reject;
  public PXAction<APPayment> reassignApproval;

  [PXWorkflowDependsOnType(new System.Type[] {typeof (APSetupApproval)})]
  public sealed override void Configure(PXScreenConfiguration config)
  {
    APPaymentEntry_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<APPaymentEntry, APPayment>());
  }

  protected static void Configure(WorkflowContext<APPaymentEntry, APPayment> context)
  {
    BoundedTo<APPaymentEntry, APPayment>.ActionCategory.IConfigured approvalCategory = context.Categories.Get("Approval");
    APPaymentEntry_ApprovalWorkflow.Conditions conditions = context.Conditions.GetPack<APPaymentEntry_ApprovalWorkflow.Conditions>();
    BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured approveAction = context.ActionDefinitions.CreateExisting<APPaymentEntry_ApprovalWorkflow>((Expression<Func<APPaymentEntry_ApprovalWorkflow, PXAction<APPayment>>>) (g => g.approve), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured>) (a => (BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.releaseFromHold)).WithPersistOptions(ActionPersistOptions.PersistBeforeAction).IsHiddenWhen((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((System.Action<BoundedTo<APPaymentEntry, APPayment>.Assignment.IContainerFillerFields>) (fa => fa.Add<APRegister.approved>((Func<BoundedTo<APPaymentEntry, APPayment>.Assignment.INeedRightOperand, BoundedTo<APPaymentEntry, APPayment>.Assignment.IConfigured>) (e => (BoundedTo<APPaymentEntry, APPayment>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured rejectAction = context.ActionDefinitions.CreateExisting<APPaymentEntry_ApprovalWorkflow>((Expression<Func<APPaymentEntry_ApprovalWorkflow, PXAction<APPayment>>>) (g => g.reject), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured>) (a => (BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory, approveAction).PlaceAfter(approveAction).IsHiddenWhen((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((System.Action<BoundedTo<APPaymentEntry, APPayment>.Assignment.IContainerFillerFields>) (fa => fa.Add<APRegister.rejected>((Func<BoundedTo<APPaymentEntry, APPayment>.Assignment.INeedRightOperand, BoundedTo<APPaymentEntry, APPayment>.Assignment.IConfigured>) (e => (BoundedTo<APPaymentEntry, APPayment>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured reassignAction = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured>) (a => (BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter(rejectAction).IsHiddenWhen((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) conditions.IsApprovalDisabled)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<APPaymentEntry, APPayment>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<APPaymentEntry, APPayment>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow(new Func<BoundedTo<APPaymentEntry, APPayment>.Workflow.ConfiguratorFlow, BoundedTo<APPaymentEntry, APPayment>.Workflow.ConfiguratorFlow>(InjectApprovalWorkflow)).WithActions((System.Action<BoundedTo<APPaymentEntry, APPayment>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approveAction);
      actions.Add(rejectAction);
      actions.Add(reassignAction);
    }))));

    BoundedTo<APPaymentEntry, APPayment>.Workflow.ConfiguratorFlow InjectApprovalWorkflow(
      BoundedTo<APPaymentEntry, APPayment>.Workflow.ConfiguratorFlow flow)
    {
      return flow.WithFlowStates((System.Action<BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.ContainerAdjusterStates>) (states => states.UpdateSequence<APDocStatus.HoldToBalance>((Func<BoundedTo<APPaymentEntry, APPayment>.Sequence.ConfiguratorSequence, BoundedTo<APPaymentEntry, APPayment>.Sequence.ConfiguratorSequence>) (seq => seq.WithStates((System.Action<BoundedTo<APPaymentEntry, APPayment>.Sequence.ContainerAdjusterSequenceStates>) (sss =>
      {
        sss.Add<APDocStatus.pendingApproval>((Func<BoundedTo<APPaymentEntry, APPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured) flowState.IsSkippedWhen(conditions.IsApproved || conditions.IsRejected).WithActions((System.Action<BoundedTo<APPaymentEntry, APPayment>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add(approveAction, (Func<BoundedTo<APPaymentEntry, APPayment>.ActionState.IAllowOptionalConfig, BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured>) (a => (BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add(rejectAction, (Func<BoundedTo<APPaymentEntry, APPayment>.ActionState.IAllowOptionalConfig, BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured>) (a => (BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add(reassignAction);
          actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.putOnHold));
        })).PlaceAfter<APDocStatus.hold>()));
        sss.Add<APDocStatus.rejected>((Func<BoundedTo<APPaymentEntry, APPayment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APPaymentEntry, APPayment>.BaseFlowStep.IConfigured) flowState.IsSkippedWhen(!conditions.IsRejected).WithActions((System.Action<BoundedTo<APPaymentEntry, APPayment>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.putOnHold), (Func<BoundedTo<APPaymentEntry, APPayment>.ActionState.IAllowOptionalConfig, BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured>) (a => (BoundedTo<APPaymentEntry, APPayment>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.printAPEdit));
          actions.Add((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.vendorDocuments));
        })).PlaceAfter<APDocStatus.pendingApproval>()));
      })))))).WithTransitions((System.Action<BoundedTo<APPaymentEntry, APPayment>.Transition.ContainerAdjusterTransitions>) (transitions =>
      {
        transitions.AddGroupFrom<APDocStatus.pendingApproval>((System.Action<BoundedTo<APPaymentEntry, APPayment>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.HoldToBalance>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXWorkflowEventHandlerBase<APPayment>>>) (g => g.OnUpdateStatus))));
          ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.ToNext().IsTriggeredOn(approveAction).When((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) conditions.IsApproved)));
          ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.rejected>().IsTriggeredOn(rejectAction).When((BoundedTo<APPaymentEntry, APPayment>.ISharedCondition) conditions.IsRejected)));
        }));
        transitions.AddGroupFrom<APDocStatus.rejected>((System.Action<BoundedTo<APPaymentEntry, APPayment>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<APPaymentEntry, APPayment>.Transition.INeedTarget, BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured>) (t => (BoundedTo<APPaymentEntry, APPayment>.Transition.IConfigured) t.To<APDocStatus.hold>().IsTriggeredOn((Expression<Func<APPaymentEntry, PXAction<APPayment>>>) (g => g.putOnHold)).DoesNotPersist()))));
      }));
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Approve", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected virtual IEnumerable Approve(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Reject", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected virtual IEnumerable Reject(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Reassign", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected virtual IEnumerable ReassignApproval(PXAdapter adapter)
  {
    return this.Base.Approval.ReassignApproval(adapter);
  }

  public class Conditions : BoundedTo<APPaymentEntry, APPayment>.Condition.Pack
  {
    public BoundedTo<APPaymentEntry, APPayment>.Condition IsApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APPaymentEntry, APPayment>.Condition.ConditionBuilder, BoundedTo<APPaymentEntry, APPayment>.Condition>) (b => b.FromBql<BqlOperand<APRegister.approved, IBqlBool>.IsEqual<True>>()), nameof (IsApproved));
      }
    }

    public BoundedTo<APPaymentEntry, APPayment>.Condition IsRejected
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APPaymentEntry, APPayment>.Condition.ConditionBuilder, BoundedTo<APPaymentEntry, APPayment>.Condition>) (b => b.FromBql<BqlOperand<APRegister.rejected, IBqlBool>.IsEqual<True>>()), nameof (IsRejected));
      }
    }

    public BoundedTo<APPaymentEntry, APPayment>.Condition IsApprovalDisabled
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APPaymentEntry, APPayment>.Condition.ConditionBuilder, BoundedTo<APPaymentEntry, APPayment>.Condition>) (b => b.FromBql<Not<EPApprovalSettings<APSetupApproval, APSetupApproval.docType, APDocType, APDocStatus.hold, APDocStatus.pendingApproval, APDocStatus.rejected>.IsDocumentApprovable<APPayment.docType, APPayment.status>>>()), nameof (IsApprovalDisabled));
      }
    }
  }
}
