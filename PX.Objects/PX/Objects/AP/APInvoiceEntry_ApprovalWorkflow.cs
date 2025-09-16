// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APInvoiceEntry_ApprovalWorkflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.EP;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.AP;

public class APInvoiceEntry_ApprovalWorkflow : 
  PXGraphExtension<APInvoiceEntry_Workflow, APInvoiceEntry>
{
  public PXAction<APInvoice> approve;
  public PXAction<APInvoice> reject;
  public PXAction<APInvoice> reassignApproval;

  [PXWorkflowDependsOnType(new System.Type[] {typeof (APSetupApproval)})]
  public sealed override void Configure(PXScreenConfiguration config)
  {
    APInvoiceEntry_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<APInvoiceEntry, APInvoice>());
  }

  protected static void Configure(WorkflowContext<APInvoiceEntry, APInvoice> context)
  {
    BoundedTo<APInvoiceEntry, APInvoice>.ActionCategory.IConfigured approvalCategory = context.Categories.Get("Approval");
    APInvoiceEntry_ApprovalWorkflow.Conditions conditions = context.Conditions.GetPack<APInvoiceEntry_ApprovalWorkflow.Conditions>();
    BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured approveAction = context.ActionDefinitions.CreateExisting<APInvoiceEntry_ApprovalWorkflow>((Expression<Func<APInvoiceEntry_ApprovalWorkflow, PXAction<APInvoice>>>) (g => g.approve), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (a => (BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.releaseFromHold)).WithPersistOptions(ActionPersistOptions.PersistBeforeAction).IsHiddenWhen((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IContainerFillerFields>) (fa => fa.Add<APRegister.approved>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (e => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured rejectAction = context.ActionDefinitions.CreateExisting<APInvoiceEntry_ApprovalWorkflow>((Expression<Func<APInvoiceEntry_ApprovalWorkflow, PXAction<APInvoice>>>) (g => g.reject), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (a => (BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory, approveAction).PlaceAfter(approveAction).IsHiddenWhen((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IContainerFillerFields>) (fa => fa.Add<APRegister.rejected>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (e => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured reassignAction = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured>) (a => (BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter(rejectAction).IsHiddenWhen((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) conditions.IsApprovalDisabled)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<APInvoiceEntry, APInvoice>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<APInvoiceEntry, APInvoice>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow(new Func<BoundedTo<APInvoiceEntry, APInvoice>.Workflow.ConfiguratorFlow, BoundedTo<APInvoiceEntry, APInvoice>.Workflow.ConfiguratorFlow>(InjectApprovalWorkflow)).WithActions((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approveAction);
      actions.Add(rejectAction);
      actions.Add(reassignAction);
      actions.Update((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.putOnHold), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.ConfiguratorAction, BoundedTo<APInvoiceEntry, APInvoice>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.FieldContainerAdjusterAssignment>) (fas =>
      {
        fas.Add<APRegister.approved>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (f => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fas.Add<APRegister.rejected>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (f => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) f.SetFromValue((object) false)));
      }))));
    }))));

    BoundedTo<APInvoiceEntry, APInvoice>.Workflow.ConfiguratorFlow InjectApprovalWorkflow(
      BoundedTo<APInvoiceEntry, APInvoice>.Workflow.ConfiguratorFlow flow)
    {
      return flow.WithFlowStates((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.ContainerAdjusterStates>) (states => states.UpdateSequence<APDocStatus.HoldToBalance>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Sequence.ConfiguratorSequence, BoundedTo<APInvoiceEntry, APInvoice>.Sequence.ConfiguratorSequence>) (seq => seq.WithStates((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Sequence.ContainerAdjusterSequenceStates>) (sss =>
      {
        sss.Add<APDocStatus.pendingApproval>((Func<BoundedTo<APInvoiceEntry, APInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured) flowState.IsSkippedWhen(conditions.IsApproved || conditions.IsRejected || conditions.IsTaxBill).WithActions((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add(approveAction, (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured>) (a => (BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add(rejectAction, (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured>) (a => (BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add(reassignAction);
          actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.putOnHold));
        })).PlaceAfter<APDocStatus.hold>()));
        sss.Add<APDocStatus.rejected>((Func<BoundedTo<APInvoiceEntry, APInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APInvoiceEntry, APInvoice>.BaseFlowStep.IConfigured) flowState.IsSkippedWhen(!conditions.IsRejected).WithActions((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.putOnHold), (Func<BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured>) (a => (BoundedTo<APInvoiceEntry, APInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.printAPEdit));
          actions.Add((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.vendorDocuments));
        })).PlaceAfter<APDocStatus.pendingApproval>()));
      })))))).WithTransitions((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Transition.ContainerAdjusterTransitions>) (transitions =>
      {
        transitions.AddGroupFrom<APDocStatus.pendingApproval>((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.HoldToBalance>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnUpdateStatus))));
          ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.ToNext().IsTriggeredOn(approveAction).When((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) conditions.IsApproved)));
          ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.rejected>().IsTriggeredOn(rejectAction).When((BoundedTo<APInvoiceEntry, APInvoice>.ISharedCondition) conditions.IsRejected)));
          ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.scheduled>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXWorkflowEventHandlerBase<APInvoice>>>) (g => g.OnConfirmSchedule)).WithFieldAssignments((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<APInvoice.scheduled>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (e => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) e.SetFromValue((object) true)));
            fas.Add<APInvoice.scheduleID>((Func<BoundedTo<APInvoiceEntry, APInvoice>.Assignment.INeedRightOperand, BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured>) (e => (BoundedTo<APInvoiceEntry, APInvoice>.Assignment.IConfigured) e.SetFromExpression("@ScheduleID")));
          }))));
        }));
        transitions.AddGroupFrom<APDocStatus.rejected>((System.Action<BoundedTo<APInvoiceEntry, APInvoice>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<APInvoiceEntry, APInvoice>.Transition.INeedTarget, BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured>) (t => (BoundedTo<APInvoiceEntry, APInvoice>.Transition.IConfigured) t.To<APDocStatus.hold>().IsTriggeredOn((Expression<Func<APInvoiceEntry, PXAction<APInvoice>>>) (g => g.putOnHold)).DoesNotPersist()))));
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

  public class Conditions : BoundedTo<APInvoiceEntry, APInvoice>.Condition.Pack
  {
    public BoundedTo<APInvoiceEntry, APInvoice>.Condition IsApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<APInvoiceEntry, APInvoice>.Condition>) (b => b.FromBql<BqlOperand<APRegister.approved, IBqlBool>.IsEqual<True>>()), nameof (IsApproved));
      }
    }

    public BoundedTo<APInvoiceEntry, APInvoice>.Condition IsRejected
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<APInvoiceEntry, APInvoice>.Condition>) (b => b.FromBql<BqlOperand<APRegister.rejected, IBqlBool>.IsEqual<True>>()), nameof (IsRejected));
      }
    }

    public BoundedTo<APInvoiceEntry, APInvoice>.Condition IsApprovalDisabled
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<APInvoiceEntry, APInvoice>.Condition>) (b => b.FromBql<Not<EPApprovalSettings<APSetupApproval, APSetupApproval.docType, APDocType, APDocStatus.hold, APDocStatus.pendingApproval, APDocStatus.rejected>.IsDocumentApprovable<APInvoice.docType, APInvoice.status>>>()), nameof (IsApprovalDisabled));
      }
    }

    public BoundedTo<APInvoiceEntry, APInvoice>.Condition IsTaxBill
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APInvoiceEntry, APInvoice>.Condition.ConditionBuilder, BoundedTo<APInvoiceEntry, APInvoice>.Condition>) (b => b.FromBql<BqlOperand<APRegister.origModule, IBqlString>.IsEqual<BatchModule.moduleTX>>()), nameof (IsTaxBill));
      }
    }
  }
}
