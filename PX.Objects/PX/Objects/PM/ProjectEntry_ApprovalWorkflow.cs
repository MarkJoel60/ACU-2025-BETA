// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectEntry_ApprovalWorkflow
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
namespace PX.Objects.PM;

public class ProjectEntry_ApprovalWorkflow : PXGraphExtension<ProjectEntry_Workflow, ProjectEntry>
{
  public PXAction<PMProject> approve;
  public PXAction<PMProject> reject;
  public PXAction<PMProject> reassignApproval;

  protected static bool ApprovalIsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() && ProjectEntry_ApprovalWorkflow.ProjectSetupApproval.IsActive;
  }

  [PXWorkflowDependsOnType(new Type[] {typeof (PMSetup)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    ProjectEntry_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<ProjectEntry, PMProject>());
  }

  protected static void Configure(WorkflowContext<ProjectEntry, PMProject> context)
  {
    var conditions = new
    {
      IsApproved = Bql<BqlOperand<PMProject.approved, IBqlBool>.IsEqual<True>>(),
      IsNotApproved = Bql<BqlOperand<PMProject.approved, IBqlBool>.IsEqual<False>>(),
      IsRejected = Bql<BqlOperand<PMProject.rejected, IBqlBool>.IsEqual<True>>(),
      IsNotRejected = Bql<BqlOperand<PMProject.rejected, IBqlBool>.IsNotEqual<True>>(),
      IsApprovalDisabled = (ProjectEntry_ApprovalWorkflow.ApprovalIsActive() ? Bql<BqlOperand<True, IBqlBool>.IsEqual<False>>() : Bql<BqlOperand<PMProject.status, IBqlString>.IsNotEqual<ProjectStatus.pendingApproval>>())
    }.AutoNameConditions();
    BoundedTo<ProjectEntry, PMProject>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("Approval", (Func<BoundedTo<ProjectEntry, PMProject>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ProjectEntry, PMProject>.ActionCategory.IConfigured>) (category => (BoundedTo<ProjectEntry, PMProject>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IConfigured approve = context.ActionDefinitions.CreateExisting<ProjectEntry_ApprovalWorkflow>((Expression<Func<ProjectEntry_ApprovalWorkflow, PXAction<PMProject>>>) (g => g.approve), (Func<BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IConfigured>) (a => (BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, (Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.activate)).PlaceAfter((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.activate)).IsHiddenWhen((BoundedTo<ProjectEntry, PMProject>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<ProjectEntry, PMProject>.Assignment.IContainerFillerFields>) (fa =>
    {
      fa.Add<PMProject.approved>((Func<BoundedTo<ProjectEntry, PMProject>.Assignment.INeedRightOperand, BoundedTo<ProjectEntry, PMProject>.Assignment.IConfigured>) (e => (BoundedTo<ProjectEntry, PMProject>.Assignment.IConfigured) e.SetFromValue((object) true)));
      fa.Add<PMProject.isActive>((Func<BoundedTo<ProjectEntry, PMProject>.Assignment.INeedRightOperand, BoundedTo<ProjectEntry, PMProject>.Assignment.IConfigured>) (e => (BoundedTo<ProjectEntry, PMProject>.Assignment.IConfigured) e.SetFromField<PMProject.approved>()));
    }))));
    BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IConfigured reject = context.ActionDefinitions.CreateExisting<ProjectEntry_ApprovalWorkflow>((Expression<Func<ProjectEntry_ApprovalWorkflow, PXAction<PMProject>>>) (g => g.reject), (Func<BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IConfigured>) (a => (BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, approve).PlaceAfter(approve).IsHiddenWhen((BoundedTo<ProjectEntry, PMProject>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<ProjectEntry, PMProject>.Assignment.IContainerFillerFields>) (fa =>
    {
      fa.Add<PMProject.rejected>((Func<BoundedTo<ProjectEntry, PMProject>.Assignment.INeedRightOperand, BoundedTo<ProjectEntry, PMProject>.Assignment.IConfigured>) (e => (BoundedTo<ProjectEntry, PMProject>.Assignment.IConfigured) e.SetFromValue((object) true)));
      fa.Add<PMProject.hold>((Func<BoundedTo<ProjectEntry, PMProject>.Assignment.INeedRightOperand, BoundedTo<ProjectEntry, PMProject>.Assignment.IConfigured>) (e => (BoundedTo<ProjectEntry, PMProject>.Assignment.IConfigured) e.SetFromValue((object) false)));
      fa.Add<PMProject.isActive>((Func<BoundedTo<ProjectEntry, PMProject>.Assignment.INeedRightOperand, BoundedTo<ProjectEntry, PMProject>.Assignment.IConfigured>) (e => (BoundedTo<ProjectEntry, PMProject>.Assignment.IConfigured) e.SetFromValue((object) false)));
    }))));
    BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IConfigured reassign = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IConfigured>) (a => (BoundedTo<ProjectEntry, PMProject>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter(reject).IsHiddenWhen((BoundedTo<ProjectEntry, PMProject>.ISharedCondition) conditions.IsApprovalDisabled)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<ProjectEntry, PMProject>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<ProjectEntry, PMProject>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow((Func<BoundedTo<ProjectEntry, PMProject>.Workflow.ConfiguratorFlow, BoundedTo<ProjectEntry, PMProject>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<ProjectEntry, PMProject>.BaseFlowStep.ContainerAdjusterStates>) (fss =>
    {
      fss.Add<ProjectStatus.pendingApproval>((Func<BoundedTo<ProjectEntry, PMProject>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ProjectEntry, PMProject>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ProjectEntry, PMProject>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ProjectEntry, PMProject>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(approve, (Func<BoundedTo<ProjectEntry, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured>) (c => (BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add(reject, (Func<BoundedTo<ProjectEntry, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured>) (c => (BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add(reassign, (Func<BoundedTo<ProjectEntry, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.hold), (Func<BoundedTo<ProjectEntry, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.lockBudget), (Func<BoundedTo<ProjectEntry, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.unlockBudget), (Func<BoundedTo<ProjectEntry, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.lockCommitments), (Func<BoundedTo<ProjectEntry, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.unlockCommitments), (Func<BoundedTo<ProjectEntry, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.validateAddresses), (Func<BoundedTo<ProjectEntry, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.validateBalance), (Func<BoundedTo<ProjectEntry, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.createTemplate), (Func<BoundedTo<ProjectEntry, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.runAllocation), (Func<BoundedTo<ProjectEntry, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.autoBudget), (Func<BoundedTo<ProjectEntry, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured>) null);
      }))));
      fss.Add<ProjectStatus.rejected>((Func<BoundedTo<ProjectEntry, PMProject>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ProjectEntry, PMProject>.BaseFlowStep.IConfigured>) (flowsState => (BoundedTo<ProjectEntry, PMProject>.BaseFlowStep.IConfigured) ((BoundedTo<ProjectEntry, PMProject>.FlowState.INeedAnyFlowStateConfig) flowsState.WithActions((Action<BoundedTo<ProjectEntry, PMProject>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.hold), (Func<BoundedTo<ProjectEntry, PMProject>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured>) (c => (BoundedTo<ProjectEntry, PMProject>.ActionState.IConfigured) c.IsDuplicatedInToolbar()))))).WithFieldStates((Action<BoundedTo<ProjectEntry, PMProject>.FieldState.IContainerFillerFields>) (fields => fields.AddAllFields<PMProject>((Func<BoundedTo<ProjectEntry, PMProject>.FieldState.INeedAnyConfigField, BoundedTo<ProjectEntry, PMProject>.FieldState.IConfigured>) (c => (BoundedTo<ProjectEntry, PMProject>.FieldState.IConfigured) c.IsDisabled()))))));
    })).WithTransitions((Action<BoundedTo<ProjectEntry, PMProject>.Transition.ContainerAdjusterTransitions>) (transitions =>
    {
      transitions.UpdateGroupFrom<ProjectStatus.planned>((Action<BoundedTo<ProjectEntry, PMProject>.Transition.SourceContainerFillerTransitions>) (ts =>
      {
        ts.Update((Func<BoundedTo<ProjectEntry, PMProject>.Transition.INeedTarget, BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured>) (t => (BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured) t.To<ProjectStatus.active>().IsTriggeredOn((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.activate))), (Func<BoundedTo<ProjectEntry, PMProject>.Transition.ConfiguratorTransition, BoundedTo<ProjectEntry, PMProject>.Transition.ConfiguratorTransition>) (t =>
        {
          BoundedTo<ProjectEntry, PMProject>.Transition.ConfiguratorTransition configuratorTransition = t;
          BoundedTo<ProjectEntry, PMProject>.Condition isApproved = conditions.IsApproved;
          BoundedTo<ProjectEntry, PMProject>.Condition condition = BoundedTo<ProjectEntry, PMProject>.Condition.op_False(isApproved) ? isApproved : BoundedTo<ProjectEntry, PMProject>.Condition.op_BitwiseAnd(isApproved, conditions.IsNotRejected);
          return configuratorTransition.When((BoundedTo<ProjectEntry, PMProject>.ISharedCondition) condition);
        }));
        ts.Add((Func<BoundedTo<ProjectEntry, PMProject>.Transition.INeedTarget, BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured>) (t =>
        {
          BoundedTo<ProjectEntry, PMProject>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<ProjectStatus.rejected>().IsTriggeredOn((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.activate));
          BoundedTo<ProjectEntry, PMProject>.Condition isNotApproved = conditions.IsNotApproved;
          BoundedTo<ProjectEntry, PMProject>.Condition condition = BoundedTo<ProjectEntry, PMProject>.Condition.op_False(isNotApproved) ? isNotApproved : BoundedTo<ProjectEntry, PMProject>.Condition.op_BitwiseAnd(isNotApproved, conditions.IsRejected);
          return (BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<ProjectEntry, PMProject>.ISharedCondition) condition);
        }));
        ts.Add((Func<BoundedTo<ProjectEntry, PMProject>.Transition.INeedTarget, BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured>) (t => (BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured) t.To<ProjectStatus.pendingApproval>().IsTriggeredOn((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.activate)).When((BoundedTo<ProjectEntry, PMProject>.ISharedCondition) conditions.IsNotApproved).When((BoundedTo<ProjectEntry, PMProject>.ISharedCondition) conditions.IsNotRejected).WithFieldAssignments((Action<BoundedTo<ProjectEntry, PMProject>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMProject.isActive>((Func<BoundedTo<ProjectEntry, PMProject>.Assignment.INeedRightOperand, BoundedTo<ProjectEntry, PMProject>.Assignment.IConfigured>) (e => (BoundedTo<ProjectEntry, PMProject>.Assignment.IConfigured) e.SetFromValue((object) false)))))));
      }));
      transitions.AddGroupFrom<ProjectStatus.pendingApproval>((Action<BoundedTo<ProjectEntry, PMProject>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ProjectEntry, PMProject>.Transition.INeedTarget, BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured>) (t => (BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured) t.To<ProjectStatus.active>().IsTriggeredOn(approve).When((BoundedTo<ProjectEntry, PMProject>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<ProjectEntry, PMProject>.Transition.INeedTarget, BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured>) (t => (BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured) t.To<ProjectStatus.rejected>().IsTriggeredOn(reject).When((BoundedTo<ProjectEntry, PMProject>.ISharedCondition) conditions.IsRejected)));
        ts.Add((Func<BoundedTo<ProjectEntry, PMProject>.Transition.INeedTarget, BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured>) (t => (BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured) t.To<ProjectStatus.planned>().IsTriggeredOn((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.hold))));
      }));
      transitions.AddGroupFrom<ProjectStatus.rejected>((Action<BoundedTo<ProjectEntry, PMProject>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ProjectEntry, PMProject>.Transition.INeedTarget, BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured>) (t => (BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured) t.To<ProjectStatus.planned>().IsTriggeredOn((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.hold))))));
      transitions.UpdateGroupFrom<ProjectStatus.completed>((Action<BoundedTo<ProjectEntry, PMProject>.Transition.SourceContainerFillerTransitions>) (ts =>
      {
        ts.Update((Func<BoundedTo<ProjectEntry, PMProject>.Transition.INeedTarget, BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured>) (t => (BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured) t.To<ProjectStatus.active>().IsTriggeredOn((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.activate))), (Func<BoundedTo<ProjectEntry, PMProject>.Transition.ConfiguratorTransition, BoundedTo<ProjectEntry, PMProject>.Transition.ConfiguratorTransition>) (t =>
        {
          BoundedTo<ProjectEntry, PMProject>.Transition.ConfiguratorTransition configuratorTransition = t;
          BoundedTo<ProjectEntry, PMProject>.Condition isApproved = conditions.IsApproved;
          BoundedTo<ProjectEntry, PMProject>.Condition condition = BoundedTo<ProjectEntry, PMProject>.Condition.op_False(isApproved) ? isApproved : BoundedTo<ProjectEntry, PMProject>.Condition.op_BitwiseAnd(isApproved, conditions.IsNotRejected);
          return configuratorTransition.When((BoundedTo<ProjectEntry, PMProject>.ISharedCondition) condition);
        }));
        ts.Add((Func<BoundedTo<ProjectEntry, PMProject>.Transition.INeedTarget, BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured>) (t =>
        {
          BoundedTo<ProjectEntry, PMProject>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<ProjectStatus.rejected>().IsTriggeredOn((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.activate));
          BoundedTo<ProjectEntry, PMProject>.Condition isNotApproved = conditions.IsNotApproved;
          BoundedTo<ProjectEntry, PMProject>.Condition condition = BoundedTo<ProjectEntry, PMProject>.Condition.op_False(isNotApproved) ? isNotApproved : BoundedTo<ProjectEntry, PMProject>.Condition.op_BitwiseAnd(isNotApproved, conditions.IsRejected);
          return (BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<ProjectEntry, PMProject>.ISharedCondition) condition);
        }));
      }));
      transitions.UpdateGroupFrom<ProjectStatus.cancelled>((Action<BoundedTo<ProjectEntry, PMProject>.Transition.SourceContainerFillerTransitions>) (ts =>
      {
        ts.Update((Func<BoundedTo<ProjectEntry, PMProject>.Transition.INeedTarget, BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured>) (t => (BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured) t.To<ProjectStatus.active>().IsTriggeredOn((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.activate))), (Func<BoundedTo<ProjectEntry, PMProject>.Transition.ConfiguratorTransition, BoundedTo<ProjectEntry, PMProject>.Transition.ConfiguratorTransition>) (t =>
        {
          BoundedTo<ProjectEntry, PMProject>.Transition.ConfiguratorTransition configuratorTransition = t;
          BoundedTo<ProjectEntry, PMProject>.Condition isApproved = conditions.IsApproved;
          BoundedTo<ProjectEntry, PMProject>.Condition condition = BoundedTo<ProjectEntry, PMProject>.Condition.op_False(isApproved) ? isApproved : BoundedTo<ProjectEntry, PMProject>.Condition.op_BitwiseAnd(isApproved, conditions.IsNotRejected);
          return configuratorTransition.When((BoundedTo<ProjectEntry, PMProject>.ISharedCondition) condition);
        }));
        ts.Add((Func<BoundedTo<ProjectEntry, PMProject>.Transition.INeedTarget, BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured>) (t =>
        {
          BoundedTo<ProjectEntry, PMProject>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<ProjectStatus.rejected>().IsTriggeredOn((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.activate));
          BoundedTo<ProjectEntry, PMProject>.Condition isNotApproved = conditions.IsNotApproved;
          BoundedTo<ProjectEntry, PMProject>.Condition condition = BoundedTo<ProjectEntry, PMProject>.Condition.op_False(isNotApproved) ? isNotApproved : BoundedTo<ProjectEntry, PMProject>.Condition.op_BitwiseAnd(isNotApproved, conditions.IsRejected);
          return (BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<ProjectEntry, PMProject>.ISharedCondition) condition);
        }));
      }));
      transitions.UpdateGroupFrom<ProjectStatus.suspended>((Action<BoundedTo<ProjectEntry, PMProject>.Transition.SourceContainerFillerTransitions>) (ts =>
      {
        ts.Update((Func<BoundedTo<ProjectEntry, PMProject>.Transition.INeedTarget, BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured>) (t => (BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured) t.To<ProjectStatus.active>().IsTriggeredOn((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.activate))), (Func<BoundedTo<ProjectEntry, PMProject>.Transition.ConfiguratorTransition, BoundedTo<ProjectEntry, PMProject>.Transition.ConfiguratorTransition>) (t =>
        {
          BoundedTo<ProjectEntry, PMProject>.Transition.ConfiguratorTransition configuratorTransition = t;
          BoundedTo<ProjectEntry, PMProject>.Condition isApproved = conditions.IsApproved;
          BoundedTo<ProjectEntry, PMProject>.Condition condition = BoundedTo<ProjectEntry, PMProject>.Condition.op_False(isApproved) ? isApproved : BoundedTo<ProjectEntry, PMProject>.Condition.op_BitwiseAnd(isApproved, conditions.IsNotRejected);
          return configuratorTransition.When((BoundedTo<ProjectEntry, PMProject>.ISharedCondition) condition);
        }));
        ts.Add((Func<BoundedTo<ProjectEntry, PMProject>.Transition.INeedTarget, BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured>) (t =>
        {
          BoundedTo<ProjectEntry, PMProject>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<ProjectStatus.rejected>().IsTriggeredOn((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.activate));
          BoundedTo<ProjectEntry, PMProject>.Condition isNotApproved = conditions.IsNotApproved;
          BoundedTo<ProjectEntry, PMProject>.Condition condition = BoundedTo<ProjectEntry, PMProject>.Condition.op_False(isNotApproved) ? isNotApproved : BoundedTo<ProjectEntry, PMProject>.Condition.op_BitwiseAnd(isNotApproved, conditions.IsRejected);
          return (BoundedTo<ProjectEntry, PMProject>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<ProjectEntry, PMProject>.ISharedCondition) condition);
        }));
      }));
    })))).WithActions((Action<BoundedTo<ProjectEntry, PMProject>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approve);
      actions.Add(reject);
      actions.Add(reassign);
      actions.Update((Expression<Func<ProjectEntry, PXAction<PMProject>>>) (g => g.hold), (Func<BoundedTo<ProjectEntry, PMProject>.ActionDefinition.ConfiguratorAction, BoundedTo<ProjectEntry, PMProject>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((Action<BoundedTo<ProjectEntry, PMProject>.Assignment.FieldContainerAdjusterAssignment>) (fa =>
      {
        fa.Add<PMProject.approved>((Func<BoundedTo<ProjectEntry, PMProject>.Assignment.INeedRightOperand, BoundedTo<ProjectEntry, PMProject>.Assignment.IConfigured>) (f => (BoundedTo<ProjectEntry, PMProject>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fa.Add<PMProject.rejected>((Func<BoundedTo<ProjectEntry, PMProject>.Assignment.INeedRightOperand, BoundedTo<ProjectEntry, PMProject>.Assignment.IConfigured>) (f => (BoundedTo<ProjectEntry, PMProject>.Assignment.IConfigured) f.SetFromValue((object) false)));
      }))));
    })).WithCategories((Action<BoundedTo<ProjectEntry, PMProject>.ActionCategory.ContainerAdjusterCategories>) (categories =>
    {
      categories.Add(approvalCategory);
      categories.Update("Approval", (Func<BoundedTo<ProjectEntry, PMProject>.ActionCategory.ConfiguratorCategory, BoundedTo<ProjectEntry, PMProject>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(context.Categories.Get("Processing"))));
    }))));

    BoundedTo<ProjectEntry, PMProject>.Condition Bql<T>() where T : IBqlUnary, new()
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
    return ((PXGraphExtension<ProjectEntry>) this).Base.Approval.ReassignApproval(adapter);
  }

  private class ProjectSetupApproval : IPrefetchable, IPXCompanyDependent
  {
    private bool RequestApproval;

    public static bool IsActive
    {
      get
      {
        return PXDatabase.GetSlot<ProjectEntry_ApprovalWorkflow.ProjectSetupApproval>(nameof (ProjectSetupApproval), new Type[1]
        {
          typeof (PMSetup)
        }).RequestApproval;
      }
    }

    void IPrefetchable.Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PMSetup>(new PXDataField[1]
      {
        (PXDataField) new PXDataField<PMSetup.assignmentMapID>()
      }))
      {
        if (pxDataRecord == null)
          return;
        this.RequestApproval = pxDataRecord.GetInt32(0).HasValue;
      }
    }
  }
}
