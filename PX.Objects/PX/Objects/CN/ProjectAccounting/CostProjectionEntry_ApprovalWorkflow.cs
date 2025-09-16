// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.CostProjectionEntry_ApprovalWorkflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting;

public class CostProjectionEntry_ApprovalWorkflow : 
  PXGraphExtension<CostProjectionEntry_Workflow, CostProjectionEntry>
{
  public PXAction<PMCostProjection> approve;
  public PXAction<PMCostProjection> reject;
  public PXAction<PMCostProjection> reassignApproval;

  protected static bool ApprovalIsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() && CostProjectionEntry_ApprovalWorkflow.CostProjectionSetupApproval.IsActive;
  }

  [PXWorkflowDependsOnType(new Type[] {typeof (PMSetup)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    CostProjectionEntry_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<CostProjectionEntry, PMCostProjection>());
  }

  protected static void Configure(
    WorkflowContext<CostProjectionEntry, PMCostProjection> context)
  {
    CostProjectionEntry_ApprovalWorkflow.Conditions conditions = context.Conditions.GetPack<CostProjectionEntry_ApprovalWorkflow.Conditions>();
    BoundedTo<CostProjectionEntry, PMCostProjection>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("Approval", (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionCategory.IConfigured>) (category => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IConfigured approve = context.ActionDefinitions.CreateExisting<CostProjectionEntry_ApprovalWorkflow>((Expression<Func<CostProjectionEntry_ApprovalWorkflow, PXAction<PMCostProjection>>>) (g => g.approve), (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IConfigured>) (a => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, (Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.removeHold)).PlaceAfter((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.removeHold)).IsHiddenWhen((BoundedTo<CostProjectionEntry, PMCostProjection>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMCostProjection.approved>((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.INeedRightOperand, BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.IConfigured>) (e => (BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IConfigured reject = context.ActionDefinitions.CreateExisting<CostProjectionEntry_ApprovalWorkflow>((Expression<Func<CostProjectionEntry_ApprovalWorkflow, PXAction<PMCostProjection>>>) (g => g.reject), (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IConfigured>) (a => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, approve).PlaceAfter(approve).IsHiddenWhen((BoundedTo<CostProjectionEntry, PMCostProjection>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMCostProjection.rejected>((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.INeedRightOperand, BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.IConfigured>) (e => (BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IConfigured reassign = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IConfigured>) (a => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter(reject).IsHiddenWhen((BoundedTo<CostProjectionEntry, PMCostProjection>.ISharedCondition) conditions.IsApprovalDisabled)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<CostProjectionEntry, PMCostProjection>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Workflow.ConfiguratorFlow, BoundedTo<CostProjectionEntry, PMCostProjection>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.BaseFlowStep.ContainerAdjusterStates>) (fss =>
    {
      fss.Add<CostProjectionStatus.pendingApproval>((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CostProjectionEntry, PMCostProjection>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CostProjectionEntry, PMCostProjection>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(approve, (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IAllowOptionalConfig, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured>) (c => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add(reject, (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IAllowOptionalConfig, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured>) (c => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add(reassign, (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IAllowOptionalConfig, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.hold), (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IAllowOptionalConfig, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.createRevision), (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IAllowOptionalConfig, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured>) (c => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
      }))));
      fss.Add<CostProjectionStatus.rejected>((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CostProjectionEntry, PMCostProjection>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CostProjectionEntry, PMCostProjection>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.hold), (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IAllowOptionalConfig, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured>) (c => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.createRevision), (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IAllowOptionalConfig, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured>) (c => (BoundedTo<CostProjectionEntry, PMCostProjection>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
      }))));
    })).WithTransitions((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.ContainerAdjusterTransitions>) (transitions =>
    {
      transitions.UpdateGroupFrom<CostProjectionStatus.onHold>((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.SourceContainerFillerTransitions>) (ts =>
      {
        ts.Update((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.INeedTarget, BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.IConfigured>) (t => (BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.IConfigured) t.To<CostProjectionStatus.open>().IsTriggeredOn((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.removeHold))), (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.ConfiguratorTransition, BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.ConfiguratorTransition>) (t => t.When((BoundedTo<CostProjectionEntry, PMCostProjection>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.INeedTarget, BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.IConfigured>) (t => (BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.IConfigured) t.To<CostProjectionStatus.rejected>().IsTriggeredOn((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.removeHold)).When((BoundedTo<CostProjectionEntry, PMCostProjection>.ISharedCondition) conditions.IsRejected)));
        ts.Add((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.INeedTarget, BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.IConfigured>) (t => (BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.IConfigured) t.To<CostProjectionStatus.pendingApproval>().IsTriggeredOn((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.removeHold)).When((BoundedTo<CostProjectionEntry, PMCostProjection>.ISharedCondition) conditions.IsNotRejected).When((BoundedTo<CostProjectionEntry, PMCostProjection>.ISharedCondition) conditions.IsNotApproved)));
      }));
      transitions.AddGroupFrom<CostProjectionStatus.pendingApproval>((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.INeedTarget, BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.IConfigured>) (t => (BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.IConfigured) t.To<CostProjectionStatus.open>().IsTriggeredOn(approve).When((BoundedTo<CostProjectionEntry, PMCostProjection>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.INeedTarget, BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.IConfigured>) (t => (BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.IConfigured) t.To<CostProjectionStatus.rejected>().IsTriggeredOn(reject).When((BoundedTo<CostProjectionEntry, PMCostProjection>.ISharedCondition) conditions.IsRejected)));
        ts.Add((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.INeedTarget, BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.IConfigured>) (t => (BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.IConfigured) t.To<CostProjectionStatus.onHold>().IsTriggeredOn((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.hold))));
      }));
      transitions.AddGroupFrom<CostProjectionStatus.rejected>((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.INeedTarget, BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.IConfigured>) (t => (BoundedTo<CostProjectionEntry, PMCostProjection>.Transition.IConfigured) t.To<CostProjectionStatus.onHold>().IsTriggeredOn((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.hold))))));
    })))).WithActions((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approve);
      actions.Add(reject);
      actions.Add(reassign);
      actions.Update((Expression<Func<CostProjectionEntry, PXAction<PMCostProjection>>>) (g => g.hold), (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.ConfiguratorAction, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.FieldContainerAdjusterAssignment>) (fa =>
      {
        fa.Add<PMCostProjection.approved>((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.INeedRightOperand, BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.IConfigured>) (f => (BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fa.Add<PMCostProjection.rejected>((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.INeedRightOperand, BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.IConfigured>) (f => (BoundedTo<CostProjectionEntry, PMCostProjection>.Assignment.IConfigured) f.SetFromValue((object) false)));
      }))));
    })).WithCategories((Action<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionCategory.ContainerAdjusterCategories>) (categories =>
    {
      categories.Add(approvalCategory);
      categories.Update("Approval", (Func<BoundedTo<CostProjectionEntry, PMCostProjection>.ActionCategory.ConfiguratorCategory, BoundedTo<CostProjectionEntry, PMCostProjection>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(context.Categories.Get("Processing"))));
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
    return ((PXGraphExtension<CostProjectionEntry>) this).Base.Approval.ReassignApproval(adapter);
  }

  private class CostProjectionSetupApproval : IPrefetchable, IPXCompanyDependent
  {
    private bool RequestApproval;

    public static bool IsActive
    {
      get
      {
        return PXDatabase.GetSlot<CostProjectionEntry_ApprovalWorkflow.CostProjectionSetupApproval>(nameof (CostProjectionSetupApproval), new Type[1]
        {
          typeof (PMSetup)
        }).RequestApproval;
      }
    }

    void IPrefetchable.Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PMSetup>(new PXDataField[1]
      {
        (PXDataField) new PXDataField<PMSetup.costProjectionApprovalMapID>()
      }))
      {
        if (pxDataRecord == null)
          return;
        this.RequestApproval = pxDataRecord.GetInt32(0).HasValue;
      }
    }
  }

  public class Conditions : BoundedTo<CostProjectionEntry, PMCostProjection>.Condition.Pack
  {
    public BoundedTo<CostProjectionEntry, PMCostProjection>.Condition IsApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Condition.ConditionBuilder, BoundedTo<CostProjectionEntry, PMCostProjection>.Condition>) (b => b.FromBql<BqlOperand<PMCostProjection.approved, IBqlBool>.IsEqual<True>>()), nameof (IsApproved));
      }
    }

    public BoundedTo<CostProjectionEntry, PMCostProjection>.Condition IsRejected
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Condition.ConditionBuilder, BoundedTo<CostProjectionEntry, PMCostProjection>.Condition>) (b => b.FromBql<BqlOperand<PMCostProjection.rejected, IBqlBool>.IsEqual<True>>()), nameof (IsRejected));
      }
    }

    public BoundedTo<CostProjectionEntry, PMCostProjection>.Condition IsNotApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Condition.ConditionBuilder, BoundedTo<CostProjectionEntry, PMCostProjection>.Condition>) (b => b.FromBql<BqlOperand<PMCostProjection.approved, IBqlBool>.IsEqual<False>>()), nameof (IsNotApproved));
      }
    }

    public BoundedTo<CostProjectionEntry, PMCostProjection>.Condition IsNotRejected
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Condition.ConditionBuilder, BoundedTo<CostProjectionEntry, PMCostProjection>.Condition>) (b => b.FromBql<BqlOperand<PMCostProjection.rejected, IBqlBool>.IsNotEqual<True>>()), nameof (IsNotRejected));
      }
    }

    public BoundedTo<CostProjectionEntry, PMCostProjection>.Condition IsApprovalDisabled
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CostProjectionEntry, PMCostProjection>.Condition.ConditionBuilder, BoundedTo<CostProjectionEntry, PMCostProjection>.Condition>) (b => !CostProjectionEntry_ApprovalWorkflow.ApprovalIsActive() ? b.FromBql<BqlOperand<PMCostProjection.status, IBqlString>.IsNotIn<CostProjectionStatus.pendingApproval, CostProjectionStatus.rejected>>() : b.FromBql<BqlOperand<True, IBqlBool>.IsEqual<False>>()), nameof (IsApprovalDisabled));
      }
    }
  }
}
