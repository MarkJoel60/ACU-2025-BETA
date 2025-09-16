// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectCostProjectionByDateEntry_ApprovalWorkflow
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

public class ProjectCostProjectionByDateEntry_ApprovalWorkflow : 
  PXGraphExtension<ProjectCostProjectionByDateEntry_Workflow, ProjectCostProjectionByDateEntry>
{
  public PXAction<PMCostProjectionByDate> approve;
  public PXAction<PMCostProjectionByDate> reject;
  public PXAction<PMCostProjectionByDate> reassignApproval;

  protected static bool ApprovalIsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() && ProjectCostProjectionByDateEntry_ApprovalWorkflow.ProjectCostProjectionByDateSetupApproval.IsActive;
  }

  [PXWorkflowDependsOnType(new Type[] {typeof (PMSetup)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    ProjectCostProjectionByDateEntry_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>());
  }

  protected static void Configure(
    WorkflowContext<ProjectCostProjectionByDateEntry, PMCostProjectionByDate> context)
  {
    ProjectCostProjectionByDateEntry_ApprovalWorkflow.Conditions conditions = context.Conditions.GetPack<ProjectCostProjectionByDateEntry_ApprovalWorkflow.Conditions>();
    BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("Approval", (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionCategory.IConfigured>) (category => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IConfigured approve = context.ActionDefinitions.CreateExisting<ProjectCostProjectionByDateEntry_ApprovalWorkflow>((Expression<Func<ProjectCostProjectionByDateEntry_ApprovalWorkflow, PXAction<PMCostProjectionByDate>>>) (g => g.approve), (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IConfigured>) (a => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory, (Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.removeHold)).PlaceAfter((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.removeHold)).IsHiddenWhen((BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMCostProjectionByDate.approved>((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.INeedRightOperand, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured>) (e => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IConfigured reject = context.ActionDefinitions.CreateExisting<ProjectCostProjectionByDateEntry_ApprovalWorkflow>((Expression<Func<ProjectCostProjectionByDateEntry_ApprovalWorkflow, PXAction<PMCostProjectionByDate>>>) (g => g.reject), (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IConfigured>) (a => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory, approve).PlaceAfter(approve).IsHiddenWhen((BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMCostProjectionByDate.rejected>((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.INeedRightOperand, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured>) (e => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IConfigured reassign = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IConfigured>) (a => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter(reject).IsHiddenWhen((BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ISharedCondition) conditions.IsApprovalDisabled)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Workflow.ConfiguratorFlow, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.BaseFlowStep.ContainerAdjusterStates>) (fss =>
    {
      fss.Add<ProjectCostProjectionByDateStatus.pendingApproval>((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(approve, (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IConfigured>) (c => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add(reject, (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IConfigured>) (c => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add(reassign, (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.hold), (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IConfigured>) null);
      }))));
      fss.Add<ProjectCostProjectionByDateStatus.rejected>((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.hold), (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IAllowOptionalConfig, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IConfigured>) (c => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionState.IConfigured) c.IsDuplicatedInToolbar()))))));
    })).WithTransitions((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.ContainerAdjusterTransitions>) (transitions =>
    {
      transitions.UpdateGroupFrom<ProjectCostProjectionByDateStatus.onHold>((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.SourceContainerFillerTransitions>) (ts =>
      {
        ts.Update((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.INeedTarget, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IConfigured>) (t => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IConfigured) t.To<ProjectCostProjectionByDateStatus.open>().IsTriggeredOn((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.removeHold))), (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.ConfiguratorTransition, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.ConfiguratorTransition>) (t => t.When((BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.INeedTarget, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IConfigured>) (t => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IConfigured) t.To<ProjectCostProjectionByDateStatus.rejected>().IsTriggeredOn((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.removeHold)).When((BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ISharedCondition) conditions.IsRejected)));
        ts.Add((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.INeedTarget, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IConfigured>) (t => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IConfigured) t.To<ProjectCostProjectionByDateStatus.pendingApproval>().IsTriggeredOn((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.removeHold)).When((BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ISharedCondition) conditions.IsNotRejected).When((BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ISharedCondition) conditions.IsNotApproved)));
      }));
      transitions.AddGroupFrom<ProjectCostProjectionByDateStatus.pendingApproval>((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.INeedTarget, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IConfigured>) (t => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IConfigured) t.To<ProjectCostProjectionByDateStatus.open>().IsTriggeredOn(approve).When((BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.INeedTarget, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IConfigured>) (t => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IConfigured) t.To<ProjectCostProjectionByDateStatus.rejected>().IsTriggeredOn(reject).When((BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ISharedCondition) conditions.IsRejected)));
        ts.Add((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.INeedTarget, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IConfigured>) (t => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IConfigured) t.To<ProjectCostProjectionByDateStatus.onHold>().IsTriggeredOn((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.hold)).WithFieldAssignments((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IContainerFillerFields>) (fa =>
        {
          fa.Add<PMCostProjectionByDate.approved>((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.INeedRightOperand, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured>) (f => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured) f.SetFromValue((object) false)));
          fa.Add<PMCostProjectionByDate.rejected>((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.INeedRightOperand, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured>) (f => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured) f.SetFromValue((object) false)));
          fa.Add<PMCostProjectionByDate.hold>((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.INeedRightOperand, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured>) (f => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured) f.SetFromValue((object) true)));
        }))));
      }));
      transitions.AddGroupFrom<ProjectCostProjectionByDateStatus.rejected>((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.INeedTarget, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IConfigured>) (t => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Transition.IConfigured) t.To<ProjectCostProjectionByDateStatus.onHold>().IsTriggeredOn((Expression<Func<ProjectCostProjectionByDateEntry, PXAction<PMCostProjectionByDate>>>) (g => g.hold)).WithFieldAssignments((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IContainerFillerFields>) (fa =>
      {
        fa.Add<PMCostProjectionByDate.approved>((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.INeedRightOperand, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured>) (f => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fa.Add<PMCostProjectionByDate.rejected>((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.INeedRightOperand, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured>) (f => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fa.Add<PMCostProjectionByDate.hold>((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.INeedRightOperand, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured>) (f => (BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Assignment.IConfigured) f.SetFromValue((object) true)));
      }))))));
    })))).WithActions((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approve);
      actions.Add(reject);
      actions.Add(reassign);
    })).WithCategories((Action<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionCategory.ContainerAdjusterCategories>) (categories =>
    {
      categories.Add(approvalCategory);
      categories.Update("Approval", (Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionCategory.ConfiguratorCategory, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(context.Categories.Get("Processing"))));
    }))));
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
    return ((PXGraphExtension<ProjectCostProjectionByDateEntry>) this).Base.Approval.ReassignApproval(adapter);
  }

  private class ProjectCostProjectionByDateSetupApproval : IPrefetchable, IPXCompanyDependent
  {
    private bool RequestApproval;

    public static bool IsActive
    {
      get
      {
        return PXDatabase.GetSlot<ProjectCostProjectionByDateEntry_ApprovalWorkflow.ProjectCostProjectionByDateSetupApproval>(nameof (ProjectCostProjectionByDateSetupApproval), new Type[1]
        {
          typeof (PMSetup)
        }).RequestApproval;
      }
    }

    void IPrefetchable.Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PMSetup>(new PXDataField[1]
      {
        (PXDataField) new PXDataField<PMSetup.costProjectionByDateApprovalMapID>()
      }))
      {
        if (pxDataRecord == null)
          return;
        this.RequestApproval = pxDataRecord.GetInt32(0).HasValue;
      }
    }
  }

  public class Conditions : 
    BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Condition.Pack
  {
    public BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Condition IsApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Condition.ConditionBuilder, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Condition>) (b => b.FromBql<BqlOperand<PMCostProjectionByDate.approved, IBqlBool>.IsEqual<True>>()), nameof (IsApproved));
      }
    }

    public BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Condition IsRejected
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Condition.ConditionBuilder, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Condition>) (b => b.FromBql<BqlOperand<PMCostProjectionByDate.rejected, IBqlBool>.IsEqual<True>>()), nameof (IsRejected));
      }
    }

    public BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Condition IsNotApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Condition.ConditionBuilder, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Condition>) (b => b.FromBql<BqlOperand<PMCostProjectionByDate.approved, IBqlBool>.IsEqual<False>>()), nameof (IsNotApproved));
      }
    }

    public BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Condition IsNotRejected
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Condition.ConditionBuilder, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Condition>) (b => b.FromBql<BqlOperand<PMCostProjectionByDate.rejected, IBqlBool>.IsNotEqual<True>>()), nameof (IsNotRejected));
      }
    }

    public BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Condition IsApprovalDisabled
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Condition.ConditionBuilder, BoundedTo<ProjectCostProjectionByDateEntry, PMCostProjectionByDate>.Condition>) (b => !ProjectCostProjectionByDateEntry_ApprovalWorkflow.ApprovalIsActive() ? b.FromBql<BqlOperand<PMCostProjectionByDate.status, IBqlString>.IsNotIn<ProjectCostProjectionByDateStatus.pendingApproval, ProjectCostProjectionByDateStatus.rejected>>() : b.FromBql<BqlOperand<True, IBqlBool>.IsEqual<False>>()), nameof (IsApprovalDisabled));
      }
    }
  }
}
