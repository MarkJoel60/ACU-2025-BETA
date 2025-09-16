// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ExpenseClaimDetailEntry_ApprovalWorkflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.EP;

public class ExpenseClaimDetailEntry_ApprovalWorkflow : 
  PXGraphExtension<ExpenseClaimDetailEntry_Workflow, ExpenseClaimDetailEntry>
{
  public PXAction<EPExpenseClaimDetails> approve;
  public PXAction<EPExpenseClaimDetails> reject;
  public PXAction<EPExpenseClaimDetails> reassignApproval;

  protected static bool ApprovalIsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() && ExpenseClaimDetailEntry_ApprovalWorkflow.ExpenseClaimDetailetupApproval.IsActive;
  }

  [PXWorkflowDependsOnType(new Type[] {typeof (EPSetup)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    ExpenseClaimDetailEntry_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<ExpenseClaimDetailEntry, EPExpenseClaimDetails>());
  }

  protected static void Configure(
    WorkflowContext<ExpenseClaimDetailEntry, EPExpenseClaimDetails> context)
  {
    BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("Approval", (Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionCategory.IConfigured>) (category => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    var conditions = new
    {
      IsRejected = Bql<BqlOperand<EPExpenseClaimDetails.rejected, IBqlBool>.IsEqual<True>>(),
      IsNotRejected = Bql<BqlOperand<EPExpenseClaimDetails.rejected, IBqlBool>.IsEqual<False>>(),
      IsNotApproved = Bql<BqlOperand<EPExpenseClaimDetails.approved, IBqlBool>.IsEqual<False>>(),
      IsApprovalDisabled = (ExpenseClaimDetailEntry_ApprovalWorkflow.ApprovalIsActive() ? Bql<BqlOperand<True, IBqlBool>.IsEqual<False>>() : Bql<BqlOperand<EPExpenseClaimDetails.status, IBqlString>.IsNotIn<EPExpenseClaimDetailsStatus.openStatus, EPExpenseClaimDetailsStatus.rejectedStatus>>()),
      IsRejectDisabled = Bql<BqlOperand<EPExpenseClaimDetails.bankTranDate, IBqlDateTime>.IsNotNull>(),
      IsApproveDisabled = Bql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPExpenseClaimDetails.holdClaim, Equal<False>>>>>.And<BqlOperand<EPExpenseClaimDetails.bankTranDate, IBqlDateTime>.IsNotNull>>()
    }.AutoNameConditions();
    BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.IConfigured approve = context.ActionDefinitions.CreateExisting<ExpenseClaimDetailEntry_ApprovalWorkflow>((Expression<Func<ExpenseClaimDetailEntry_ApprovalWorkflow, PXAction<EPExpenseClaimDetails>>>) (g => g.approve), (Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.IConfigured>) (a => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, (Expression<Func<ExpenseClaimDetailEntry, PXAction<EPExpenseClaimDetails>>>) (g => g.Submit)).PlaceAfter((Expression<Func<ExpenseClaimDetailEntry, PXAction<EPExpenseClaimDetails>>>) (g => g.Submit)).IsHiddenWhen((BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ISharedCondition) conditions.IsApprovalDisabled).IsDisabledWhen((BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ISharedCondition) conditions.IsApproveDisabled).WithFieldAssignments((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.IContainerFillerFields>) (fa => fa.Add<EPExpenseClaimDetails.approved>((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.INeedRightOperand, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.IConfigured>) (e => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.IConfigured reject = context.ActionDefinitions.CreateExisting<ExpenseClaimDetailEntry_ApprovalWorkflow>((Expression<Func<ExpenseClaimDetailEntry_ApprovalWorkflow, PXAction<EPExpenseClaimDetails>>>) (g => g.reject), (Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.IConfigured>) (a => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, approve).PlaceAfter(approve).IsHiddenWhen((BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ISharedCondition) conditions.IsApprovalDisabled).IsDisabledWhen((BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ISharedCondition) conditions.IsRejectDisabled).WithFieldAssignments((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.IContainerFillerFields>) (fa => fa.Add<EPExpenseClaimDetails.rejected>((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.INeedRightOperand, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.IConfigured>) (e => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.IConfigured reassign = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.IConfigured>) (a => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter(reject).IsHiddenWhen((BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ISharedCondition) conditions.IsApprovalDisabled).IsDisabledWhen((BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ISharedCondition) conditions.IsApproveDisabled)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Workflow.ConfiguratorFlow, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.BaseFlowStep.ContainerAdjusterStates>) (fss =>
    {
      fss.Add<EPExpenseClaimDetailsStatus.openStatus>((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(approve, (Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IAllowOptionalConfig, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IConfigured>) (c => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add(reject, (Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IAllowOptionalConfig, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IConfigured>) (c => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add(reassign, (Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IAllowOptionalConfig, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ExpenseClaimDetailEntry, PXAction<EPExpenseClaimDetails>>>) (g => g.hold), (Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IAllowOptionalConfig, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IConfigured>) null);
      }))));
      fss.Add<EPExpenseClaimDetailsStatus.rejectedStatus>((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<ExpenseClaimDetailEntry, PXAction<EPExpenseClaimDetails>>>) (g => g.hold), (Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IAllowOptionalConfig, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IConfigured>) (c => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IConfigured) c.IsDuplicatedInToolbar()))))));
    })).WithTransitions((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.ContainerAdjusterTransitions>) (transitions =>
    {
      transitions.UpdateGroupFrom<EPExpenseClaimDetailsStatus.holdStatus>((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.SourceContainerFillerTransitions>) (ts =>
      {
        ts.Update((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.INeedTarget, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IConfigured) t.To<EPExpenseClaimDetailsStatus.approvedStatus>().IsTriggeredOn((Expression<Func<ExpenseClaimDetailEntry, PXAction<EPExpenseClaimDetails>>>) (g => g.Submit))), (Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.ConfiguratorTransition, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.ConfiguratorTransition>) (t => t.When(context.Conditions.Get("IsApproved"))));
        ts.Add((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.INeedTarget, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IConfigured) t.To<EPExpenseClaimDetailsStatus.rejectedStatus>().IsTriggeredOn((Expression<Func<ExpenseClaimDetailEntry, PXAction<EPExpenseClaimDetails>>>) (g => g.Submit)).When((BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ISharedCondition) conditions.IsRejected)));
        ts.Add((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.INeedTarget, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IConfigured>) (t =>
        {
          BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<EPExpenseClaimDetailsStatus.openStatus>().IsTriggeredOn((Expression<Func<ExpenseClaimDetailEntry, PXAction<EPExpenseClaimDetails>>>) (g => g.Submit));
          BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Condition isNotApproved = conditions.IsNotApproved;
          BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Condition condition = BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Condition.op_False(isNotApproved) ? isNotApproved : BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Condition.op_BitwiseAnd(isNotApproved, conditions.IsNotRejected);
          return (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ISharedCondition) condition);
        }));
      }));
      transitions.AddGroupFrom<EPExpenseClaimDetailsStatus.openStatus>((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.INeedTarget, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IConfigured) t.To<EPExpenseClaimDetailsStatus.approvedStatus>().IsTriggeredOn(approve).When(context.Conditions.Get("IsApproved"))));
        ts.Add((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.INeedTarget, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IConfigured) t.To<EPExpenseClaimDetailsStatus.rejectedStatus>().IsTriggeredOn(reject).When((BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ISharedCondition) conditions.IsRejected)));
        ts.Add((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.INeedTarget, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IConfigured) t.To<EPExpenseClaimDetailsStatus.holdStatus>().IsTriggeredOn((Expression<Func<ExpenseClaimDetailEntry, PXAction<EPExpenseClaimDetails>>>) (g => g.hold))));
      }));
      transitions.AddGroupFrom<EPExpenseClaimDetailsStatus.rejectedStatus>((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.INeedTarget, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IConfigured) t.To<EPExpenseClaimDetailsStatus.holdStatus>().IsTriggeredOn((Expression<Func<ExpenseClaimDetailEntry, PXAction<EPExpenseClaimDetails>>>) (g => g.hold))))));
    })))).WithActions((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approve);
      actions.Add(reject);
      actions.Add(reassign);
      actions.Update((Expression<Func<ExpenseClaimDetailEntry, PXAction<EPExpenseClaimDetails>>>) (g => g.hold), (Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.ConfiguratorAction, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.FieldContainerAdjusterAssignment>) (fa =>
      {
        fa.Add<EPExpenseClaimDetails.approved>((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.INeedRightOperand, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.IConfigured>) (f => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fa.Add<EPExpenseClaimDetails.rejected>((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.INeedRightOperand, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.IConfigured>) (f => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.IConfigured) f.SetFromValue((object) false)));
      }))));
    })).WithCategories((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionCategory.ContainerAdjusterCategories>) (categories =>
    {
      categories.Add(approvalCategory);
      categories.Update("Approval", (Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionCategory.ConfiguratorCategory, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(context.Categories.Get("Processing"))));
    }))));

    BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Condition Bql<T>() where T : IBqlUnary, new()
    {
      return context.Conditions.FromBql<T>();
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
    return ((PXGraphExtension<ExpenseClaimDetailEntry>) this).Base.Approval.ReassignApproval(adapter);
  }

  private class ExpenseClaimDetailetupApproval : IPrefetchable, IPXCompanyDependent
  {
    private bool RequestApproval;

    public static bool IsActive
    {
      get
      {
        return PXDatabase.GetSlot<ExpenseClaimDetailEntry_ApprovalWorkflow.ExpenseClaimDetailetupApproval>(nameof (ExpenseClaimDetailetupApproval), new Type[1]
        {
          typeof (EPSetup)
        }).RequestApproval;
      }
    }

    void IPrefetchable.Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<EPSetup>(new PXDataField[1]
      {
        (PXDataField) new PXDataField<EPSetup.claimDetailsAssignmentMapID>()
      }))
      {
        if (pxDataRecord == null)
          return;
        this.RequestApproval = pxDataRecord.GetInt32(0).HasValue;
      }
    }
  }
}
