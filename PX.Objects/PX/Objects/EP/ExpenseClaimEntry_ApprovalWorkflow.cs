// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ExpenseClaimEntry_ApprovalWorkflow
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

public class ExpenseClaimEntry_ApprovalWorkflow : 
  PXGraphExtension<ExpenseClaimEntry_Workflow, ExpenseClaimEntry>
{
  public PXAction<EPExpenseClaim> approve;
  public PXAction<EPExpenseClaim> reject;
  public PXAction<EPExpenseClaim> reassignApproval;

  protected static bool ApprovalIsActive()
  {
    return !PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() || ExpenseClaimEntry_ApprovalWorkflow.ExpenseClaimSetupApproval.IsActive;
  }

  [PXWorkflowDependsOnType(new Type[] {typeof (EPSetup)})]
  public sealed override void Configure(PXScreenConfiguration config)
  {
    ExpenseClaimEntry_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<ExpenseClaimEntry, EPExpenseClaim>());
  }

  protected static void Configure(
    WorkflowContext<ExpenseClaimEntry, EPExpenseClaim> context)
  {
    var conditions = new
    {
      IsRejected = Bql<BqlOperand<EPExpenseClaim.rejected, IBqlBool>.IsEqual<True>>(),
      IsApproved = Bql<BqlOperand<EPExpenseClaim.approved, IBqlBool>.IsEqual<True>>(),
      IsNotApproved = Bql<BqlOperand<EPExpenseClaim.approved, IBqlBool>.IsEqual<False>>(),
      IsApprovalDisabled = (ExpenseClaimEntry_ApprovalWorkflow.ApprovalIsActive() ? Bql<BqlOperand<True, IBqlBool>.IsEqual<False>>() : Bql<BqlOperand<EPExpenseClaim.status, IBqlString>.IsNotIn<EPExpenseClaimStatus.openStatus, EPExpenseClaimStatus.rejectedStatus>>())
    }.AutoNameConditions();
    BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("Approval", (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionCategory.IConfigured>) (category => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IConfigured approve = context.ActionDefinitions.CreateExisting<ExpenseClaimEntry_ApprovalWorkflow>((Expression<Func<ExpenseClaimEntry_ApprovalWorkflow, PXAction<EPExpenseClaim>>>) (g => g.approve), (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IConfigured>) (a => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, (Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.release)).PlaceAfter((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.release)).IsHiddenWhen((BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.IContainerFillerFields>) (fa => fa.Add<EPExpenseClaim.approved>((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.INeedRightOperand, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.IConfigured>) (e => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IConfigured reject = context.ActionDefinitions.CreateExisting<ExpenseClaimEntry_ApprovalWorkflow>((Expression<Func<ExpenseClaimEntry_ApprovalWorkflow, PXAction<EPExpenseClaim>>>) (g => g.reject), (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IConfigured>) (a => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, approve).PlaceAfter(approve).IsHiddenWhen((BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.IContainerFillerFields>) (fa => fa.Add<EPExpenseClaim.rejected>((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.INeedRightOperand, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.IConfigured>) (e => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IConfigured reassign = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IConfigured>) (a => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter(reject).IsHiddenWhen((BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ISharedCondition) conditions.IsApprovalDisabled)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Workflow.ConfiguratorFlow, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.BaseFlowStep.ContainerAdjusterStates>) (fss =>
    {
      fss.Add<EPExpenseClaimStatus.openStatus>((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(approve, (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IAllowOptionalConfig, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IConfigured>) (c => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add(reject, (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IAllowOptionalConfig, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IConfigured>) (c => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add(reassign);
        actions.Add((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.edit));
        actions.Add((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.expenseClaimPrint), (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IAllowOptionalConfig, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IConfigured>) (c => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
      }))));
      fss.Add<EPExpenseClaimStatus.rejectedStatus>((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.edit), (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IAllowOptionalConfig, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IConfigured>) (c => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.expenseClaimPrint), (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IAllowOptionalConfig, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IConfigured>) (c => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
      }))));
    })).WithTransitions((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.ContainerAdjusterTransitions>) (transitions =>
    {
      transitions.UpdateGroupFrom<EPExpenseClaimStatus.holdStatus>((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.SourceContainerFillerTransitions>) (ts =>
      {
        ts.Update((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.INeedTarget, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured) t.To<EPExpenseClaimStatus.approvedStatus>().IsTriggeredOn((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.submit))), (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.ConfiguratorTransition, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.ConfiguratorTransition>) (t => t.When((BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ISharedCondition) conditions.IsApproved)));
        ts.Update((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.INeedTarget, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured) t.To<EPExpenseClaimStatus.approvedStatus>().IsTriggeredOn((Expression<Func<ExpenseClaimEntry, PXWorkflowEventHandlerBase<EPExpenseClaim>>>) (g => g.OnSubmit))), (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.ConfiguratorTransition, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.ConfiguratorTransition>) (t => t.When((BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.INeedTarget, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured) t.To<EPExpenseClaimStatus.openStatus>().IsTriggeredOn((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.submit)).When((BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ISharedCondition) conditions.IsNotApproved)));
        ts.Add((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.INeedTarget, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured) t.To<EPExpenseClaimStatus.openStatus>().IsTriggeredOn((Expression<Func<ExpenseClaimEntry, PXWorkflowEventHandlerBase<EPExpenseClaim>>>) (g => g.OnSubmit)).When((BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ISharedCondition) conditions.IsNotApproved)));
        ts.Add((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.INeedTarget, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured) t.To<EPExpenseClaimStatus.rejectedStatus>().IsTriggeredOn((Expression<Func<ExpenseClaimEntry, PXWorkflowEventHandlerBase<EPExpenseClaim>>>) (g => g.OnUpdateStatus)).When((BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ISharedCondition) conditions.IsRejected)));
      }));
      transitions.AddGroupFrom<EPExpenseClaimStatus.openStatus>((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.INeedTarget, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured) t.To<EPExpenseClaimStatus.approvedStatus>().IsTriggeredOn(approve).When((BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.INeedTarget, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured) t.To<EPExpenseClaimStatus.rejectedStatus>().IsTriggeredOn(reject).When((BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ISharedCondition) conditions.IsRejected)));
        ts.Add((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.INeedTarget, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured) t.To<EPExpenseClaimStatus.holdStatus>().IsTriggeredOn((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.edit))));
      }));
      transitions.AddGroupFrom<EPExpenseClaimStatus.rejectedStatus>((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.INeedTarget, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured) t.To<EPExpenseClaimStatus.holdStatus>().IsTriggeredOn((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.edit))))));
    })))).WithActions((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approve);
      actions.Add(reject);
      actions.Add(reassign);
      actions.Update((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.edit), (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.ConfiguratorAction, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.FieldContainerAdjusterAssignment>) (fa =>
      {
        fa.Add<EPExpenseClaim.approved>((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.INeedRightOperand, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.IConfigured>) (f => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fa.Add<EPExpenseClaim.rejected>((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.INeedRightOperand, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.IConfigured>) (f => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.IConfigured) f.SetFromValue((object) false)));
      }))));
    })).WithCategories((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionCategory.ContainerAdjusterCategories>) (categories =>
    {
      categories.Add(approvalCategory);
      categories.Update("Approval", (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionCategory.ConfiguratorCategory, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(context.Categories.Get("Processing"))));
    }))));

    BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Condition Bql<T>() where T : IBqlUnary, new()
    {
      return context.Conditions.FromBql<T>();
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

  private class ExpenseClaimSetupApproval : IPrefetchable, IPXCompanyDependent
  {
    private bool RequestApproval;

    public static bool IsActive
    {
      get
      {
        return PXDatabase.GetSlot<ExpenseClaimEntry_ApprovalWorkflow.ExpenseClaimSetupApproval>(nameof (ExpenseClaimSetupApproval), typeof (EPSetup)).RequestApproval;
      }
    }

    void IPrefetchable.Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<EPSetup>((PXDataField) new PXDataField<EPSetup.claimAssignmentMapID>()))
      {
        if (pxDataRecord == null)
          return;
        this.RequestApproval = pxDataRecord.GetInt32(0).HasValue;
      }
    }
  }
}
