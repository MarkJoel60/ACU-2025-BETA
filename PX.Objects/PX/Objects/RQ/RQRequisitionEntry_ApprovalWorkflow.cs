// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequisitionEntry_ApprovalWorkflow
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

public class RQRequisitionEntry_ApprovalWorkflow : 
  PXGraphExtension<RQRequisitionEntry_Workflow, RQRequisitionEntry>
{
  public PXAction<RQRequisition> approve;
  public PXAction<RQRequisition> reject;
  public PXAction<RQRequisition> reassignApproval;

  [PXWorkflowDependsOnType(new Type[] {typeof (RQSetup)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    if (RQRequisitionEntry_ApprovalWorkflow.RQRequisitionApproval.IsActive)
      RQRequisitionEntry_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<RQRequisitionEntry, RQRequisition>());
    else
      RQRequisitionEntry_ApprovalWorkflow.HideApprovalActions(config.GetScreenConfigurationContext<RQRequisitionEntry, RQRequisition>());
  }

  protected static void Configure(
    WorkflowContext<RQRequisitionEntry, RQRequisition> context)
  {
    RQRequisitionEntry_ApprovalWorkflow.Conditions conditions = context.Conditions.GetPack<RQRequisitionEntry_ApprovalWorkflow.Conditions>();
    RQRequisitionEntry_Workflow.Conditions baseConditions = context.Conditions.GetPack<RQRequisitionEntry_Workflow.Conditions>();
    (BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IConfigured approve, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IConfigured reject, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IConfigured reassign, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionCategory.IConfigured approvalCategory) = RQRequisitionEntry_ApprovalWorkflow.GetApprovalActions(context, false);
    context.UpdateScreenConfigurationFor((Func<BoundedTo<RQRequisitionEntry, RQRequisition>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<RQRequisitionEntry, RQRequisition>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow((Func<BoundedTo<RQRequisitionEntry, RQRequisition>.Workflow.ConfiguratorFlow, BoundedTo<RQRequisitionEntry, RQRequisition>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<RQRequisitionEntry, RQRequisition>.BaseFlowStep.ContainerAdjusterStates>) (states =>
    {
      states.Add<RQRequisitionStatus.pendingApproval>((Func<BoundedTo<RQRequisitionEntry, RQRequisition>.FlowState.INeedAnyFlowStateConfig, BoundedTo<RQRequisitionEntry, RQRequisition>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<RQRequisitionEntry, RQRequisition>.BaseFlowStep.IConfigured) ((BoundedTo<RQRequisitionEntry, RQRequisition>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<RQRequisitionEntry, RQRequisition>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<RQRequisitionEntry, PXAction<RQRequisition>>>) (g => g.putOnHold), (Func<BoundedTo<RQRequisitionEntry, RQRequisition>.ActionState.IAllowOptionalConfig, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionState.IConfigured>) null);
        actions.Add(approve, (Func<BoundedTo<RQRequisitionEntry, RQRequisition>.ActionState.IAllowOptionalConfig, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionState.IConfigured>) (a => (BoundedTo<RQRequisitionEntry, RQRequisition>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add(reject, (Func<BoundedTo<RQRequisitionEntry, RQRequisition>.ActionState.IAllowOptionalConfig, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionState.IConfigured>) (a => (BoundedTo<RQRequisitionEntry, RQRequisition>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add(reassign, (Func<BoundedTo<RQRequisitionEntry, RQRequisition>.ActionState.IAllowOptionalConfig, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionState.IConfigured>) null);
      }))).WithFieldStates(new Action<BoundedTo<RQRequisitionEntry, RQRequisition>.FieldState.IContainerFillerFields>(RQRequisitionEntry_Workflow.DisableWholeScreen))));
      states.Add<RQRequisitionStatus.rejected>((Func<BoundedTo<RQRequisitionEntry, RQRequisition>.FlowState.INeedAnyFlowStateConfig, BoundedTo<RQRequisitionEntry, RQRequisition>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<RQRequisitionEntry, RQRequisition>.BaseFlowStep.IConfigured) ((BoundedTo<RQRequisitionEntry, RQRequisition>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<RQRequisitionEntry, RQRequisition>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<RQRequisitionEntry, PXAction<RQRequisition>>>) (g => g.putOnHold), (Func<BoundedTo<RQRequisitionEntry, RQRequisition>.ActionState.IAllowOptionalConfig, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionState.IConfigured>) (a => (BoundedTo<RQRequisitionEntry, RQRequisition>.ActionState.IConfigured) a.IsDuplicatedInToolbar()))))).WithFieldStates(new Action<BoundedTo<RQRequisitionEntry, RQRequisition>.FieldState.IContainerFillerFields>(RQRequisitionEntry_Workflow.DisableWholeScreen))));
    })).WithTransitions((Action<BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.ContainerAdjusterTransitions>) (transitions =>
    {
      transitions.UpdateGroupFrom("_", (Action<BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.SourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.INeedTarget, BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.IConfigured>) (t => (BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.IConfigured) t.To<RQRequisitionStatus.pendingApproval>().IsTriggeredOn((Expression<Func<RQRequisitionEntry, PXAction<RQRequisition>>>) (g => g.initializeState)).When((BoundedTo<RQRequisitionEntry, RQRequisition>.ISharedCondition) BoundedTo<RQRequisitionEntry, RQRequisition>.Condition.op_LogicalNot(conditions.IsApproved)).PlaceAfter((Func<BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.ITransitionSelector, BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.ITransitionSelector>) (rt => rt.To<RQRequisitionStatus.hold>().IsTriggeredOn((Expression<Func<RQRequisitionEntry, PXAction<RQRequisition>>>) (g => g.initializeState))))))));
      transitions.UpdateGroupFrom<RQRequisitionStatus.hold>((Action<BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.SourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.INeedTarget, BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.IConfigured>) (t => (BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.IConfigured) t.To<RQRequisitionStatus.pendingApproval>().IsTriggeredOn((Expression<Func<RQRequisitionEntry, PXAction<RQRequisition>>>) (g => g.releaseFromHold)).When((BoundedTo<RQRequisitionEntry, RQRequisition>.ISharedCondition) BoundedTo<RQRequisitionEntry, RQRequisition>.Condition.op_LogicalNot(conditions.IsApproved)).PlaceBefore((Func<BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.ITransitionSelector, BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.ITransitionSelector>) (rt => rt.To<RQRequisitionStatus.open>().IsTriggeredOn((Expression<Func<RQRequisitionEntry, PXAction<RQRequisition>>>) (g => g.releaseFromHold))))))));
      transitions.AddGroupFrom<RQRequisitionStatus.pendingApproval>((Action<BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.INeedTarget, BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.IConfigured>) (t =>
        {
          BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<RQRequisitionStatus.open>().IsTriggeredOn(approve);
          BoundedTo<RQRequisitionEntry, RQRequisition>.Condition isApproved = conditions.IsApproved;
          BoundedTo<RQRequisitionEntry, RQRequisition>.Condition condition = BoundedTo<RQRequisitionEntry, RQRequisition>.Condition.op_False(isApproved) ? isApproved : BoundedTo<RQRequisitionEntry, RQRequisition>.Condition.op_BitwiseAnd(isApproved, baseConditions.IsQuoted);
          return (BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<RQRequisitionEntry, RQRequisition>.ISharedCondition) condition);
        }));
        ts.Add((Func<BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.INeedTarget, BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.IConfigured>) (t =>
        {
          BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<RQRequisitionStatus.pendingQuotation>().IsTriggeredOn(approve);
          BoundedTo<RQRequisitionEntry, RQRequisition>.Condition isApproved = conditions.IsApproved;
          BoundedTo<RQRequisitionEntry, RQRequisition>.Condition condition = BoundedTo<RQRequisitionEntry, RQRequisition>.Condition.op_False(isApproved) ? isApproved : BoundedTo<RQRequisitionEntry, RQRequisition>.Condition.op_BitwiseAnd(isApproved, baseConditions.IsBiddingCompleted);
          return (BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<RQRequisitionEntry, RQRequisition>.ISharedCondition) condition);
        }));
        ts.Add((Func<BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.INeedTarget, BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.IConfigured>) (t => (BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.IConfigured) t.To<RQRequisitionStatus.bidding>().IsTriggeredOn(approve).When((BoundedTo<RQRequisitionEntry, RQRequisition>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.INeedTarget, BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.IConfigured>) (t => (BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.IConfigured) t.To<RQRequisitionStatus.rejected>().IsTriggeredOn(reject).When((BoundedTo<RQRequisitionEntry, RQRequisition>.ISharedCondition) conditions.IsRejected)));
        ts.Add((Func<BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.INeedTarget, BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.IConfigured>) (t => (BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.IConfigured) t.To<RQRequisitionStatus.hold>().IsTriggeredOn((Expression<Func<RQRequisitionEntry, PXAction<RQRequisition>>>) (g => g.putOnHold))));
      }));
      transitions.AddGroupFrom<RQRequisitionStatus.rejected>((Action<BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.INeedTarget, BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.IConfigured>) (t => (BoundedTo<RQRequisitionEntry, RQRequisition>.Transition.IConfigured) t.To<RQRequisitionStatus.hold>().IsTriggeredOn((Expression<Func<RQRequisitionEntry, PXAction<RQRequisition>>>) (g => g.putOnHold))))));
    })))).WithActions((Action<BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approve);
      actions.Add(reject);
      actions.Add(reassign);
      actions.Update((Expression<Func<RQRequisitionEntry, PXAction<RQRequisition>>>) (g => g.putOnHold), (Func<BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.ConfiguratorAction, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((Action<BoundedTo<RQRequisitionEntry, RQRequisition>.Assignment.FieldContainerAdjusterAssignment>) (fas =>
      {
        fas.Add<RQRequisition.approved>(new bool?(false));
        fas.Add<RQRequisition.rejected>(new bool?(false));
      }))));
    })).WithCategories((Action<BoundedTo<RQRequisitionEntry, RQRequisition>.ActionCategory.ContainerAdjusterCategories>) (categories => categories.Add(approvalCategory)))));
  }

  protected static void HideApprovalActions(
    WorkflowContext<RQRequisitionEntry, RQRequisition> context)
  {
    (BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IConfigured approve, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IConfigured reject, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IConfigured reassign, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionCategory.IConfigured _) = RQRequisitionEntry_ApprovalWorkflow.GetApprovalActions(context, true);
    context.UpdateScreenConfigurationFor((Func<BoundedTo<RQRequisitionEntry, RQRequisition>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<RQRequisitionEntry, RQRequisition>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((Action<BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approve);
      actions.Add(reject);
      actions.Add(reassign);
    }))));
  }

  protected static (BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IConfigured approve, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IConfigured reject, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IConfigured reassign, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionCategory.IConfigured approvalCategory) GetApprovalActions(
    WorkflowContext<RQRequisitionEntry, RQRequisition> context,
    bool hidden)
  {
    BoundedTo<RQRequisitionEntry, RQRequisition>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("Approval Category", (Func<BoundedTo<RQRequisitionEntry, RQRequisition>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionCategory.IConfigured>) (category => (BoundedTo<RQRequisitionEntry, RQRequisition>.ActionCategory.IConfigured) category.DisplayName("Approval").PlaceAfter("Processing Category")));
    BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IConfigured approve = context.ActionDefinitions.CreateExisting<RQRequisitionEntry_ApprovalWorkflow>((Expression<Func<RQRequisitionEntry_ApprovalWorkflow, PXAction<RQRequisition>>>) (g => g.approve), (Func<BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IConfigured>) (a => (BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter((Expression<Func<RQRequisitionEntry, PXAction<RQRequisition>>>) (g => g.putOnHold)).With<BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IAllowOptionalConfigAction>((Func<BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IAllowOptionalConfigAction>) (it => !hidden ? it : it.IsHiddenAlways())).WithFieldAssignments((Action<BoundedTo<RQRequisitionEntry, RQRequisition>.Assignment.IContainerFillerFields>) (fa => fa.Add<RQRequisition.approved>(new bool?(true))))));
    BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IConfigured reject = context.ActionDefinitions.CreateExisting<RQRequisitionEntry_ApprovalWorkflow>((Expression<Func<RQRequisitionEntry_ApprovalWorkflow, PXAction<RQRequisition>>>) (g => g.reject), (Func<BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IConfigured>) (a => (BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter(approve).With<BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IAllowOptionalConfigAction>((Func<BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IAllowOptionalConfigAction>) (it => !hidden ? it : it.IsHiddenAlways())).WithFieldAssignments((Action<BoundedTo<RQRequisitionEntry, RQRequisition>.Assignment.IContainerFillerFields>) (fa => fa.Add<RQRequisition.rejected>(new bool?(true))))));
    BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IConfigured existing = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IConfigured>) (a => (BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter(reject).With<BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IAllowOptionalConfigAction>((Func<BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequisitionEntry, RQRequisition>.ActionDefinition.IAllowOptionalConfigAction>) (it => !hidden ? it : it.IsHiddenAlways()))));
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
    return ((PXGraphExtension<RQRequisitionEntry>) this).Base.Approval.ReassignApproval(adapter);
  }

  private class RQRequisitionApproval : IPrefetchable, IPXCompanyDependent
  {
    private bool RequireApproval;

    public static bool IsActive
    {
      get
      {
        return PXDatabase.GetSlot<RQRequisitionEntry_ApprovalWorkflow.RQRequisitionApproval>(nameof (RQRequisitionApproval), new Type[1]
        {
          typeof (RQSetup)
        }).RequireApproval;
      }
    }

    void IPrefetchable.Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<RQSetup>(new PXDataField[1]
      {
        (PXDataField) new PXDataField<RQSetup.requisitionApproval>()
      }))
      {
        if (pxDataRecord == null)
          return;
        this.RequireApproval = pxDataRecord.GetBoolean(0).GetValueOrDefault();
      }
    }
  }

  public class Conditions : BoundedTo<RQRequisitionEntry, RQRequisition>.Condition.Pack
  {
    public BoundedTo<RQRequisitionEntry, RQRequisition>.Condition IsApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<RQRequisitionEntry, RQRequisition>.Condition.ConditionBuilder, BoundedTo<RQRequisitionEntry, RQRequisition>.Condition>) (b => b.FromBql<BqlOperand<RQRequisition.approved, IBqlBool>.IsEqual<True>>()), nameof (IsApproved));
      }
    }

    public BoundedTo<RQRequisitionEntry, RQRequisition>.Condition IsRejected
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<RQRequisitionEntry, RQRequisition>.Condition.ConditionBuilder, BoundedTo<RQRequisitionEntry, RQRequisition>.Condition>) (b => b.FromBql<BqlOperand<RQRequisition.rejected, IBqlBool>.IsEqual<True>>()), nameof (IsRejected));
      }
    }
  }
}
