// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APQuickCheckEntry_ApprovalWorkflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.AP.Standalone;
using PX.Objects.EP;
using System;
using System.Collections;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.AP;

public class APQuickCheckEntry_ApprovalWorkflow : 
  PXGraphExtension<APQuickCheckEntry_Workflow, APQuickCheckEntry>
{
  public PXAction<APQuickCheck> approve;
  public PXAction<APQuickCheck> reject;
  public PXAction<APQuickCheck> reassignApproval;

  [PXWorkflowDependsOnType(new System.Type[] {typeof (APSetupApproval)})]
  public sealed override void Configure(PXScreenConfiguration config)
  {
    APQuickCheckEntry_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<APQuickCheckEntry, APQuickCheck>());
  }

  protected static void Configure(
    WorkflowContext<APQuickCheckEntry, APQuickCheck> context)
  {
    BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionCategory.IConfigured approvalCategory = context.Categories.Get("Approval");
    APQuickCheckEntry_ApprovalWorkflow.Conditions conditions = context.Conditions.GetPack<APQuickCheckEntry_ApprovalWorkflow.Conditions>();
    BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured approveAction = context.ActionDefinitions.CreateExisting<APQuickCheckEntry_ApprovalWorkflow>((Expression<Func<APQuickCheckEntry_ApprovalWorkflow, PXAction<APQuickCheck>>>) (g => g.approve), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured>) (a => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.releaseFromHold)).IsHiddenWhen((BoundedTo<APQuickCheckEntry, APQuickCheck>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.IContainerFillerFields>) (fa => fa.Add<APRegister.approved>((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.INeedRightOperand, BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.IConfigured>) (e => (BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured rejectAction = context.ActionDefinitions.CreateExisting<APQuickCheckEntry_ApprovalWorkflow>((Expression<Func<APQuickCheckEntry_ApprovalWorkflow, PXAction<APQuickCheck>>>) (g => g.reject), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured>) (a => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory, approveAction).PlaceAfter(approveAction).IsHiddenWhen((BoundedTo<APQuickCheckEntry, APQuickCheck>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.IContainerFillerFields>) (fa => fa.Add<APRegister.rejected>((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.INeedRightOperand, BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.IConfigured>) (e => (BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured reassignAction = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured>) (a => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter(rejectAction).IsHiddenWhen((BoundedTo<APQuickCheckEntry, APQuickCheck>.ISharedCondition) conditions.IsApprovalDisabled)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<APQuickCheckEntry, APQuickCheck>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow(new Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Workflow.ConfiguratorFlow, BoundedTo<APQuickCheckEntry, APQuickCheck>.Workflow.ConfiguratorFlow>(InjectApprovalWorkflow)).WithActions((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approveAction);
      actions.Add(rejectAction);
      actions.Add(reassignAction);
      actions.Update((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.putOnHold), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.ConfiguratorAction, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.FieldContainerAdjusterAssignment>) (fas =>
      {
        fas.Add<APRegister.approved>((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.INeedRightOperand, BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.IConfigured>) (f => (BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fas.Add<APRegister.rejected>((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.INeedRightOperand, BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.IConfigured>) (f => (BoundedTo<APQuickCheckEntry, APQuickCheck>.Assignment.IConfigured) f.SetFromValue((object) false)));
      }))));
    }))));

    BoundedTo<APQuickCheckEntry, APQuickCheck>.Workflow.ConfiguratorFlow InjectApprovalWorkflow(
      BoundedTo<APQuickCheckEntry, APQuickCheck>.Workflow.ConfiguratorFlow flow)
    {
      return flow.WithFlowStates((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.ContainerAdjusterStates>) (states => states.UpdateSequence<APDocStatus.HoldToBalance>((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Sequence.ConfiguratorSequence, BoundedTo<APQuickCheckEntry, APQuickCheck>.Sequence.ConfiguratorSequence>) (seq => seq.WithStates((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.Sequence.ContainerAdjusterSequenceStates>) (sss =>
      {
        sss.Add<APDocStatus.pendingApproval>((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IConfigured) flowState.IsSkippedWhen(conditions.IsApproved || conditions.IsRejected).WithActions((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add(approveAction, (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IAllowOptionalConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured>) (a => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add(rejectAction, (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IAllowOptionalConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured>) (a => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add(reassignAction);
          actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.putOnHold));
        })).PlaceAfter<APDocStatus.hold>()));
        sss.Add<APDocStatus.rejected>((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.FlowState.INeedAnyFlowStateConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<APQuickCheckEntry, APQuickCheck>.BaseFlowStep.IConfigured) flowState.IsSkippedWhen(!conditions.IsRejected).WithActions((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.putOnHold), (Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IAllowOptionalConfig, BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured>) (a => (BoundedTo<APQuickCheckEntry, APQuickCheck>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.printAPEdit));
          actions.Add((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.vendorDocuments));
        })).PlaceAfter<APDocStatus.pendingApproval>()));
      })))))).WithTransitions((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.ContainerAdjusterTransitions>) (transitions =>
      {
        transitions.AddGroupFrom<APDocStatus.pendingApproval>((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.INeedTarget, BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.IConfigured>) (t => (BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.IConfigured) t.To<APDocStatus.HoldToBalance>().IsTriggeredOn((Expression<Func<APQuickCheckEntry, PXWorkflowEventHandlerBase<APQuickCheck>>>) (g => g.OnUpdateStatus))));
          ts.Add((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.INeedTarget, BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.IConfigured>) (t => (BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.IConfigured) t.ToNext().IsTriggeredOn(approveAction).When((BoundedTo<APQuickCheckEntry, APQuickCheck>.ISharedCondition) conditions.IsApproved)));
          ts.Add((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.INeedTarget, BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.IConfigured>) (t => (BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.IConfigured) t.To<APDocStatus.rejected>().IsTriggeredOn(rejectAction).When((BoundedTo<APQuickCheckEntry, APQuickCheck>.ISharedCondition) conditions.IsRejected)));
        }));
        transitions.AddGroupFrom<APDocStatus.rejected>((System.Action<BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.INeedTarget, BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.IConfigured>) (t => (BoundedTo<APQuickCheckEntry, APQuickCheck>.Transition.IConfigured) t.To<APDocStatus.hold>().IsTriggeredOn((Expression<Func<APQuickCheckEntry, PXAction<APQuickCheck>>>) (g => g.putOnHold)).DoesNotPersist()))));
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

  public class Conditions : BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition.Pack
  {
    public BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition IsApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition.ConditionBuilder, BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition>) (b => b.FromBql<BqlOperand<APRegister.approved, IBqlBool>.IsEqual<True>>()), nameof (IsApproved));
      }
    }

    public BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition IsRejected
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition.ConditionBuilder, BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition>) (b => b.FromBql<BqlOperand<APRegister.rejected, IBqlBool>.IsEqual<True>>()), nameof (IsRejected));
      }
    }

    public BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition IsApprovalDisabled
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition.ConditionBuilder, BoundedTo<APQuickCheckEntry, APQuickCheck>.Condition>) (b => b.FromBql<Not<EPApprovalSettings<APSetupApproval, APSetupApproval.docType, APDocType, APDocStatus.hold, APDocStatus.pendingApproval, APDocStatus.rejected>.IsDocumentApprovable<APQuickCheck.docType, APQuickCheck.status>>>()), nameof (IsApprovalDisabled));
      }
    }
  }
}
