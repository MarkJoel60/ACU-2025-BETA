// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectWipAdjustmentEntry_ApprovalWorkflow
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

public class ProjectWipAdjustmentEntry_ApprovalWorkflow : 
  PXGraphExtension<ProjectWipAdjustmentEntry_Workflow, ProjectWipAdjustmentEntry>
{
  public PXAction<PMWipAdjustment> approve;
  public PXAction<PMWipAdjustment> reject;
  public PXAction<PMWipAdjustment> reassignApproval;

  protected static bool ApprovalIsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() && ProjectWipAdjustmentEntry_ApprovalWorkflow.ProjectWipAdjustmentSetupApproval.IsActive;
  }

  [PXWorkflowDependsOnType(new Type[] {typeof (PMSetup)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    ProjectWipAdjustmentEntry_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<ProjectWipAdjustmentEntry, PMWipAdjustment>());
  }

  protected static void Configure(
    WorkflowContext<ProjectWipAdjustmentEntry, PMWipAdjustment> context)
  {
    ProjectWipAdjustmentEntry_ApprovalWorkflow.Conditions conditions = context.Conditions.GetPack<ProjectWipAdjustmentEntry_ApprovalWorkflow.Conditions>();
    BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("Approval", (Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionCategory.IConfigured>) (category => (BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionDefinition.IConfigured approve = context.ActionDefinitions.CreateExisting<ProjectWipAdjustmentEntry_ApprovalWorkflow>((Expression<Func<ProjectWipAdjustmentEntry_ApprovalWorkflow, PXAction<PMWipAdjustment>>>) (g => g.approve), (Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionDefinition.IConfigured>) (a => (BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory, (Expression<Func<ProjectWipAdjustmentEntry, PXAction<PMWipAdjustment>>>) (g => g.removeHold)).PlaceAfter((Expression<Func<ProjectWipAdjustmentEntry, PXAction<PMWipAdjustment>>>) (g => g.removeHold)).IsHiddenWhen((BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMWipAdjustment.approved>((Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Assignment.INeedRightOperand, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Assignment.IConfigured>) (e => (BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionDefinition.IConfigured reject = context.ActionDefinitions.CreateExisting<ProjectWipAdjustmentEntry_ApprovalWorkflow>((Expression<Func<ProjectWipAdjustmentEntry_ApprovalWorkflow, PXAction<PMWipAdjustment>>>) (g => g.reject), (Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionDefinition.IConfigured>) (a => (BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory, approve).PlaceAfter(approve).IsHiddenWhen((BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMWipAdjustment.rejected>((Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Assignment.INeedRightOperand, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Assignment.IConfigured>) (e => (BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionDefinition.IConfigured reassign = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionDefinition.IConfigured>) (a => (BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter(reject).IsHiddenWhen((BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ISharedCondition) conditions.IsApprovalDisabled)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow((Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Workflow.ConfiguratorFlow, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.BaseFlowStep.ContainerAdjusterStates>) (fss =>
    {
      fss.Add<ProjectWipAdjustmentStatus.pendingApproval>((Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(approve, (Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionState.IConfigured>) (c => (BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add(reject, (Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionState.IConfigured>) (c => (BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add(reassign, (Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ProjectWipAdjustmentEntry, PXAction<PMWipAdjustment>>>) (g => g.hold), (Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionState.IConfigured>) null);
      }))));
      fss.Add<ProjectWipAdjustmentStatus.rejected>((Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<ProjectWipAdjustmentEntry, PXAction<PMWipAdjustment>>>) (g => g.hold), (Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionState.IConfigured>) (c => (BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionState.IConfigured) c.IsDuplicatedInToolbar()))))));
    })).WithTransitions((Action<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.ContainerAdjusterTransitions>) (transitions =>
    {
      transitions.UpdateGroupFrom<ProjectWipAdjustmentStatus.onHold>((Action<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.SourceContainerFillerTransitions>) (ts =>
      {
        ts.Update((Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.INeedTarget, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.IConfigured>) (t => (BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.IConfigured) t.To<ProjectWipAdjustmentStatus.open>().IsTriggeredOn((Expression<Func<ProjectWipAdjustmentEntry, PXAction<PMWipAdjustment>>>) (g => g.removeHold))), (Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.ConfiguratorTransition, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.ConfiguratorTransition>) (t => t.When((BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.INeedTarget, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.IConfigured>) (t => (BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.IConfigured) t.To<ProjectWipAdjustmentStatus.rejected>().IsTriggeredOn((Expression<Func<ProjectWipAdjustmentEntry, PXAction<PMWipAdjustment>>>) (g => g.removeHold)).When((BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ISharedCondition) conditions.IsRejected)));
        ts.Add((Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.INeedTarget, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.IConfigured>) (t => (BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.IConfigured) t.To<ProjectWipAdjustmentStatus.pendingApproval>().IsTriggeredOn((Expression<Func<ProjectWipAdjustmentEntry, PXAction<PMWipAdjustment>>>) (g => g.removeHold)).When((BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ISharedCondition) conditions.IsNotRejected).When((BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ISharedCondition) conditions.IsNotApproved)));
      }));
      transitions.AddGroupFrom<ProjectWipAdjustmentStatus.pendingApproval>((Action<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.INeedTarget, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.IConfigured>) (t => (BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.IConfigured) t.To<ProjectWipAdjustmentStatus.open>().IsTriggeredOn(approve).When((BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.INeedTarget, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.IConfigured>) (t => (BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.IConfigured) t.To<ProjectWipAdjustmentStatus.rejected>().IsTriggeredOn(reject).When((BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ISharedCondition) conditions.IsRejected)));
        ts.Add((Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.INeedTarget, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.IConfigured>) (t => (BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.IConfigured) t.To<ProjectWipAdjustmentStatus.onHold>().IsTriggeredOn((Expression<Func<ProjectWipAdjustmentEntry, PXAction<PMWipAdjustment>>>) (g => g.hold))));
      }));
      transitions.AddGroupFrom<ProjectWipAdjustmentStatus.rejected>((Action<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.INeedTarget, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.IConfigured>) (t => (BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Transition.IConfigured) t.To<ProjectWipAdjustmentStatus.onHold>().IsTriggeredOn((Expression<Func<ProjectWipAdjustmentEntry, PXAction<PMWipAdjustment>>>) (g => g.hold))))));
    })))).WithActions((Action<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approve);
      actions.Add(reject);
      actions.Add(reassign);
      actions.Update((Expression<Func<ProjectWipAdjustmentEntry, PXAction<PMWipAdjustment>>>) (g => g.hold), (Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionDefinition.ConfiguratorAction, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((Action<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Assignment.FieldContainerAdjusterAssignment>) (fa =>
      {
        fa.Add<PMWipAdjustment.approved>((Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Assignment.INeedRightOperand, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Assignment.IConfigured>) (f => (BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fa.Add<PMWipAdjustment.rejected>((Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Assignment.INeedRightOperand, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Assignment.IConfigured>) (f => (BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Assignment.IConfigured) f.SetFromValue((object) false)));
      }))));
    })).WithCategories((Action<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionCategory.ContainerAdjusterCategories>) (categories =>
    {
      categories.Add(approvalCategory);
      categories.Update("Approval", (Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionCategory.ConfiguratorCategory, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(context.Categories.Get("Processing"))));
    }))));
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Approve")]
  protected virtual IEnumerable Approve(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Reject")]
  protected virtual IEnumerable Reject(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReassignApproval(PXAdapter adapter)
  {
    return ((PXGraphExtension<ProjectWipAdjustmentEntry>) this).Base.Approval.ReassignApproval(adapter);
  }

  private class ProjectWipAdjustmentSetupApproval : IPrefetchable, IPXCompanyDependent
  {
    private bool RequestApproval;

    public static bool IsActive
    {
      get
      {
        return PXDatabase.GetSlot<ProjectWipAdjustmentEntry_ApprovalWorkflow.ProjectWipAdjustmentSetupApproval>(nameof (ProjectWipAdjustmentSetupApproval), new Type[1]
        {
          typeof (PMSetup)
        }).RequestApproval;
      }
    }

    void IPrefetchable.Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PMSetup>(new PXDataField[1]
      {
        (PXDataField) new PXDataField<PMSetup.wipAdjustmentApprovalMapID>()
      }))
      {
        if (pxDataRecord == null)
          return;
        this.RequestApproval = pxDataRecord.GetInt32(0).HasValue;
      }
    }
  }

  public class Conditions : BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Condition.Pack
  {
    public BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Condition IsApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Condition.ConditionBuilder, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Condition>) (b => b.FromBql<BqlOperand<PMWipAdjustment.approved, IBqlBool>.IsEqual<True>>()), nameof (IsApproved));
      }
    }

    public BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Condition IsRejected
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Condition.ConditionBuilder, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Condition>) (b => b.FromBql<BqlOperand<PMWipAdjustment.rejected, IBqlBool>.IsEqual<True>>()), nameof (IsRejected));
      }
    }

    public BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Condition IsNotApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Condition.ConditionBuilder, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Condition>) (b => b.FromBql<BqlOperand<PMWipAdjustment.approved, IBqlBool>.IsEqual<False>>()), nameof (IsNotApproved));
      }
    }

    public BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Condition IsNotRejected
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Condition.ConditionBuilder, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Condition>) (b => b.FromBql<BqlOperand<PMWipAdjustment.rejected, IBqlBool>.IsNotEqual<True>>()), nameof (IsNotRejected));
      }
    }

    public BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Condition IsApprovalDisabled
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Condition.ConditionBuilder, BoundedTo<ProjectWipAdjustmentEntry, PMWipAdjustment>.Condition>) (b => !ProjectWipAdjustmentEntry_ApprovalWorkflow.ApprovalIsActive() ? b.FromBql<BqlOperand<PMWipAdjustment.status, IBqlString>.IsNotIn<ProjectWipAdjustmentStatus.pendingApproval, ProjectWipAdjustmentStatus.rejected>>() : b.FromBql<BqlOperand<True, IBqlBool>.IsEqual<False>>()), nameof (IsApprovalDisabled));
      }
    }
  }
}
