// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequestEntry_ApprovalWorkflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using System;
using System.Collections;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.RQ;

public class RQRequestEntry_ApprovalWorkflow : 
  PXGraphExtension<RQRequestEntry_Workflow, RQRequestEntry>
{
  public PXAction<RQRequest> approve;
  public PXAction<RQRequest> reject;
  public PXAction<RQRequest> reassignApproval;

  [PXWorkflowDependsOnType(new Type[] {typeof (RQSetup)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    if (RQRequestEntry_ApprovalWorkflow.RQRequestApproval.IsActive)
      RQRequestEntry_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<RQRequestEntry, RQRequest>());
    else
      RQRequestEntry_ApprovalWorkflow.HideApprovalActions(config.GetScreenConfigurationContext<RQRequestEntry, RQRequest>());
  }

  protected static void Configure(WorkflowContext<RQRequestEntry, RQRequest> context)
  {
    RQRequestEntry_ApprovalWorkflow.Conditions conditions = context.Conditions.GetPack<RQRequestEntry_ApprovalWorkflow.Conditions>();
    (BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured approve, BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured reject, BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured reassign, BoundedTo<RQRequestEntry, RQRequest>.ActionCategory.IConfigured approvalCategory) = RQRequestEntry_ApprovalWorkflow.GetApprovalActions(context, false);
    context.UpdateScreenConfigurationFor((Func<BoundedTo<RQRequestEntry, RQRequest>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<RQRequestEntry, RQRequest>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow((Func<BoundedTo<RQRequestEntry, RQRequest>.Workflow.ConfiguratorFlow, BoundedTo<RQRequestEntry, RQRequest>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<RQRequestEntry, RQRequest>.BaseFlowStep.ContainerAdjusterStates>) (states =>
    {
      states.Add<RQRequestStatus.pendingApproval>((Func<BoundedTo<RQRequestEntry, RQRequest>.FlowState.INeedAnyFlowStateConfig, BoundedTo<RQRequestEntry, RQRequest>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<RQRequestEntry, RQRequest>.BaseFlowStep.IConfigured) ((BoundedTo<RQRequestEntry, RQRequest>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<RQRequestEntry, RQRequest>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.putOnHold), (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionState.IAllowOptionalConfig, BoundedTo<RQRequestEntry, RQRequest>.ActionState.IConfigured>) null);
        actions.Add(approve, (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionState.IAllowOptionalConfig, BoundedTo<RQRequestEntry, RQRequest>.ActionState.IConfigured>) (a => (BoundedTo<RQRequestEntry, RQRequest>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add(reject, (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionState.IAllowOptionalConfig, BoundedTo<RQRequestEntry, RQRequest>.ActionState.IConfigured>) (a => (BoundedTo<RQRequestEntry, RQRequest>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add(reassign, (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionState.IAllowOptionalConfig, BoundedTo<RQRequestEntry, RQRequest>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.requestForm), (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionState.IAllowOptionalConfig, BoundedTo<RQRequestEntry, RQRequest>.ActionState.IConfigured>) null);
      }))).WithFieldStates(new Action<BoundedTo<RQRequestEntry, RQRequest>.FieldState.IContainerFillerFields>(RQRequestEntry_Workflow.DisableWholeScreen))));
      states.Add<RQRequestStatus.rejected>((Func<BoundedTo<RQRequestEntry, RQRequest>.FlowState.INeedAnyFlowStateConfig, BoundedTo<RQRequestEntry, RQRequest>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<RQRequestEntry, RQRequest>.BaseFlowStep.IConfigured) ((BoundedTo<RQRequestEntry, RQRequest>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<RQRequestEntry, RQRequest>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.putOnHold), (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionState.IAllowOptionalConfig, BoundedTo<RQRequestEntry, RQRequest>.ActionState.IConfigured>) (a => (BoundedTo<RQRequestEntry, RQRequest>.ActionState.IConfigured) a.IsDuplicatedInToolbar()))))).WithFieldStates(new Action<BoundedTo<RQRequestEntry, RQRequest>.FieldState.IContainerFillerFields>(RQRequestEntry_Workflow.DisableWholeScreen))));
    })).WithTransitions((Action<BoundedTo<RQRequestEntry, RQRequest>.Transition.ContainerAdjusterTransitions>) (transitions =>
    {
      transitions.UpdateGroupFrom("_", (Action<BoundedTo<RQRequestEntry, RQRequest>.Transition.SourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<RQRequestEntry, RQRequest>.Transition.INeedTarget, BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured>) (t => (BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured) t.To<RQRequestStatus.pendingApproval>().IsTriggeredOn((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.initializeState)).When((BoundedTo<RQRequestEntry, RQRequest>.ISharedCondition) BoundedTo<RQRequestEntry, RQRequest>.Condition.op_LogicalNot(conditions.IsApproved)).PlaceAfter((Func<BoundedTo<RQRequestEntry, RQRequest>.Transition.ITransitionSelector, BoundedTo<RQRequestEntry, RQRequest>.Transition.ITransitionSelector>) (rt => rt.To<RQRequestStatus.hold>().IsTriggeredOn((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.initializeState))))))));
      transitions.UpdateGroupFrom<RQRequestStatus.hold>((Action<BoundedTo<RQRequestEntry, RQRequest>.Transition.SourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<RQRequestEntry, RQRequest>.Transition.INeedTarget, BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured>) (t => (BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured) t.To<RQRequestStatus.pendingApproval>().IsTriggeredOn((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.releaseFromHold)).When((BoundedTo<RQRequestEntry, RQRequest>.ISharedCondition) BoundedTo<RQRequestEntry, RQRequest>.Condition.op_LogicalNot(conditions.IsApproved)).PlaceBefore((Func<BoundedTo<RQRequestEntry, RQRequest>.Transition.ITransitionSelector, BoundedTo<RQRequestEntry, RQRequest>.Transition.ITransitionSelector>) (rt => rt.To<RQRequestStatus.closed>().IsTriggeredOn((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.releaseFromHold))))))));
      transitions.AddGroupFrom<RQRequestStatus.pendingApproval>((Action<BoundedTo<RQRequestEntry, RQRequest>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<RQRequestEntry, RQRequest>.Transition.INeedTarget, BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured>) (t =>
        {
          BoundedTo<RQRequestEntry, RQRequest>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<RQRequestStatus.closed>().IsTriggeredOn(approve);
          BoundedTo<RQRequestEntry, RQRequest>.Condition isApproved = conditions.IsApproved;
          BoundedTo<RQRequestEntry, RQRequest>.Condition condition = BoundedTo<RQRequestEntry, RQRequest>.Condition.op_False(isApproved) ? isApproved : BoundedTo<RQRequestEntry, RQRequest>.Condition.op_BitwiseAnd(isApproved, conditions.HasZeroOpenOrderQty);
          return (BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<RQRequestEntry, RQRequest>.ISharedCondition) condition);
        }));
        ts.Add((Func<BoundedTo<RQRequestEntry, RQRequest>.Transition.INeedTarget, BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured>) (t => (BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured) t.To<RQRequestStatus.open>().IsTriggeredOn(approve).When((BoundedTo<RQRequestEntry, RQRequest>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<RQRequestEntry, RQRequest>.Transition.INeedTarget, BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured>) (t => (BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured) t.To<RQRequestStatus.rejected>().IsTriggeredOn(reject).When((BoundedTo<RQRequestEntry, RQRequest>.ISharedCondition) conditions.IsRejected)));
        ts.Add((Func<BoundedTo<RQRequestEntry, RQRequest>.Transition.INeedTarget, BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured>) (t => (BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured) t.To<RQRequestStatus.hold>().IsTriggeredOn((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.putOnHold))));
      }));
      transitions.AddGroupFrom<RQRequestStatus.rejected>((Action<BoundedTo<RQRequestEntry, RQRequest>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<RQRequestEntry, RQRequest>.Transition.INeedTarget, BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured>) (t => (BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured) t.To<RQRequestStatus.hold>().IsTriggeredOn((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.putOnHold))))));
    })))).WithActions((Action<BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approve);
      actions.Add(reject);
      actions.Add(reassign);
      actions.Update((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.putOnHold), (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.ConfiguratorAction, BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((Action<BoundedTo<RQRequestEntry, RQRequest>.Assignment.FieldContainerAdjusterAssignment>) (fas =>
      {
        fas.Add<RQRequest.approved>(new bool?(false));
        fas.Add<RQRequest.rejected>(new bool?(false));
      }))));
    })).WithCategories((Action<BoundedTo<RQRequestEntry, RQRequest>.ActionCategory.ContainerAdjusterCategories>) (categories => categories.Add(approvalCategory)))));
  }

  protected static void HideApprovalActions(WorkflowContext<RQRequestEntry, RQRequest> context)
  {
    (BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured approve, BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured reject, BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured reassign, BoundedTo<RQRequestEntry, RQRequest>.ActionCategory.IConfigured _) = RQRequestEntry_ApprovalWorkflow.GetApprovalActions(context, true);
    context.UpdateScreenConfigurationFor((Func<BoundedTo<RQRequestEntry, RQRequest>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<RQRequestEntry, RQRequest>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((Action<BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approve);
      actions.Add(reject);
      actions.Add(reassign);
    }))));
  }

  protected static (BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured approve, BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured reject, BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured reassign, BoundedTo<RQRequestEntry, RQRequest>.ActionCategory.IConfigured approvalCategory) GetApprovalActions(
    WorkflowContext<RQRequestEntry, RQRequest> context,
    bool hidden)
  {
    BoundedTo<RQRequestEntry, RQRequest>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("Approval Category", (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<RQRequestEntry, RQRequest>.ActionCategory.IConfigured>) (category => (BoundedTo<RQRequestEntry, RQRequest>.ActionCategory.IConfigured) category.DisplayName("Approval").PlaceAfter("Processing Category")));
    BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured approve = context.ActionDefinitions.CreateExisting<RQRequestEntry_ApprovalWorkflow>((Expression<Func<RQRequestEntry_ApprovalWorkflow, PXAction<RQRequest>>>) (g => g.approve), (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured>) (a => (BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.putOnHold)).With<BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IAllowOptionalConfigAction>((Func<BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IAllowOptionalConfigAction>) (it => !hidden ? it : it.IsHiddenAlways())).WithFieldAssignments((Action<BoundedTo<RQRequestEntry, RQRequest>.Assignment.IContainerFillerFields>) (fa => fa.Add<RQRequest.approved>(new bool?(true))))));
    BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured reject = context.ActionDefinitions.CreateExisting<RQRequestEntry_ApprovalWorkflow>((Expression<Func<RQRequestEntry_ApprovalWorkflow, PXAction<RQRequest>>>) (g => g.reject), (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured>) (a => (BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter(approve).With<BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IAllowOptionalConfigAction>((Func<BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IAllowOptionalConfigAction>) (it => !hidden ? it : it.IsHiddenAlways())).WithFieldAssignments((Action<BoundedTo<RQRequestEntry, RQRequest>.Assignment.IContainerFillerFields>) (fa => fa.Add<RQRequest.rejected>(new bool?(true))))));
    BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured existing = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured>) (a => (BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter(reject).With<BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IAllowOptionalConfigAction>((Func<BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IAllowOptionalConfigAction>) (it => !hidden ? it : it.IsHiddenAlways()))));
    return (approve, reject, existing, approvalCategory);
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
    return ((PXGraphExtension<RQRequestEntry>) this).Base.Approval.ReassignApproval(adapter);
  }

  private class RQRequestApproval : IPrefetchable, IPXCompanyDependent
  {
    private bool RequireApproval;

    public static bool IsActive
    {
      get
      {
        return PXDatabase.GetSlot<RQRequestEntry_ApprovalWorkflow.RQRequestApproval>(nameof (RQRequestApproval), new Type[1]
        {
          typeof (RQSetup)
        }).RequireApproval;
      }
    }

    void IPrefetchable.Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<RQSetup>(new PXDataField[1]
      {
        (PXDataField) new PXDataField<RQSetup.requestApproval>()
      }))
      {
        if (pxDataRecord == null)
          return;
        this.RequireApproval = pxDataRecord.GetBoolean(0).GetValueOrDefault();
      }
    }
  }

  public class Conditions : BoundedTo<RQRequestEntry, RQRequest>.Condition.Pack
  {
    public BoundedTo<RQRequestEntry, RQRequest>.Condition IsApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<RQRequestEntry, RQRequest>.Condition.ConditionBuilder, BoundedTo<RQRequestEntry, RQRequest>.Condition>) (b => b.FromBql<BqlOperand<RQRequest.approved, IBqlBool>.IsEqual<True>>()), nameof (IsApproved));
      }
    }

    public BoundedTo<RQRequestEntry, RQRequest>.Condition IsRejected
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<RQRequestEntry, RQRequest>.Condition.ConditionBuilder, BoundedTo<RQRequestEntry, RQRequest>.Condition>) (b => b.FromBql<BqlOperand<RQRequest.rejected, IBqlBool>.IsEqual<True>>()), nameof (IsRejected));
      }
    }

    public BoundedTo<RQRequestEntry, RQRequest>.Condition HasZeroOpenOrderQty
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<RQRequestEntry, RQRequest>.Condition.ConditionBuilder, BoundedTo<RQRequestEntry, RQRequest>.Condition>) (b => b.FromBql<BqlOperand<RQRequest.openOrderQty, IBqlDecimal>.IsEqual<Zero>>()), nameof (HasZeroOpenOrderQty));
      }
    }
  }
}
