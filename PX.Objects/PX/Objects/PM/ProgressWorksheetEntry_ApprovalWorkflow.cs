// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProgressWorksheetEntry_ApprovalWorkflow
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

public class ProgressWorksheetEntry_ApprovalWorkflow : 
  PXGraphExtension<ProgressWorksheetEntry_Workflow, ProgressWorksheetEntry>
{
  public PXAction<PMProgressWorksheet> approve;
  public PXAction<PMProgressWorksheet> reject;
  public PXAction<PMProgressWorksheet> reassignApproval;

  public static bool IsActive() => true;

  protected static bool ApprovalIsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() && ProgressWorksheetEntry_ApprovalWorkflow.ProgressWorksheetSetupApproval.IsActive;
  }

  [PXWorkflowDependsOnType(new Type[] {typeof (PMSetup)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    ProgressWorksheetEntry_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<ProgressWorksheetEntry, PMProgressWorksheet>());
  }

  protected static void Configure(
    WorkflowContext<ProgressWorksheetEntry, PMProgressWorksheet> context)
  {
    var conditions = new
    {
      IsRejected = Bql<BqlOperand<PMProgressWorksheet.rejected, IBqlBool>.IsEqual<True>>(),
      IsApproved = Bql<BqlOperand<PMProgressWorksheet.approved, IBqlBool>.IsEqual<True>>(),
      IsNotApproved = Bql<BqlOperand<PMProgressWorksheet.approved, IBqlBool>.IsEqual<False>>(),
      IsApprovalDisabled = (ProgressWorksheetEntry_ApprovalWorkflow.ApprovalIsActive() ? Bql<BqlOperand<True, IBqlBool>.IsEqual<False>>() : Bql<BqlOperand<PMProgressWorksheet.status, IBqlString>.IsNotIn<ProgressWorksheetStatus.pendingApproval, ProgressWorksheetStatus.rejected>>())
    }.AutoNameConditions();
    BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("Approval", (Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionCategory.IConfigured>) (category => (BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionDefinition.IConfigured approve = context.ActionDefinitions.CreateExisting<ProgressWorksheetEntry_ApprovalWorkflow>((Expression<Func<ProgressWorksheetEntry_ApprovalWorkflow, PXAction<PMProgressWorksheet>>>) (g => g.approve), (Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionDefinition.IConfigured>) (a => (BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, (Expression<Func<ProgressWorksheetEntry, PXAction<PMProgressWorksheet>>>) (g => g.removeHold)).PlaceAfter((Expression<Func<ProgressWorksheetEntry, PXAction<PMProgressWorksheet>>>) (g => g.removeHold)).IsHiddenWhen((BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMProgressWorksheet.approved>((Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Assignment.INeedRightOperand, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Assignment.IConfigured>) (e => (BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionDefinition.IConfigured reject = context.ActionDefinitions.CreateExisting<ProgressWorksheetEntry_ApprovalWorkflow>((Expression<Func<ProgressWorksheetEntry_ApprovalWorkflow, PXAction<PMProgressWorksheet>>>) (g => g.reject), (Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionDefinition.IConfigured>) (a => (BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, approve).PlaceAfter(approve).IsHiddenWhen((BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMProgressWorksheet.rejected>((Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Assignment.INeedRightOperand, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Assignment.IConfigured>) (e => (BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionDefinition.IConfigured reassign = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionDefinition.IConfigured>) (a => (BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter(reject).IsHiddenWhen((BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ISharedCondition) conditions.IsApprovalDisabled)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow((Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Workflow.ConfiguratorFlow, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.BaseFlowStep.ContainerAdjusterStates>) (fss =>
    {
      fss.Add<ProgressWorksheetStatus.pendingApproval>((Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(approve, (Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionState.IConfigured>) (c => (BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add(reject, (Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionState.IConfigured>) (c => (BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add(reassign, (Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ProgressWorksheetEntry, PXAction<PMProgressWorksheet>>>) (g => g.hold), (Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionState.IConfigured>) null);
      }))));
      fss.Add<ProgressWorksheetStatus.rejected>((Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<ProgressWorksheetEntry, PXAction<PMProgressWorksheet>>>) (g => g.hold), (Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionState.IAllowOptionalConfig, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionState.IConfigured>) (c => (BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionState.IConfigured) c.IsDuplicatedInToolbar()))))));
    })).WithTransitions((Action<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.ContainerAdjusterTransitions>) (transitions =>
    {
      transitions.UpdateGroupFrom<ProgressWorksheetStatus.onHold>((Action<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.SourceContainerFillerTransitions>) (ts =>
      {
        ts.Update((Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.INeedTarget, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.IConfigured>) (t => (BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.IConfigured) t.To<ProgressWorksheetStatus.open>().IsTriggeredOn((Expression<Func<ProgressWorksheetEntry, PXAction<PMProgressWorksheet>>>) (g => g.removeHold))), (Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.ConfiguratorTransition, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.ConfiguratorTransition>) (t => t.When((BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.INeedTarget, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.IConfigured>) (t => (BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.IConfigured) t.To<ProgressWorksheetStatus.pendingApproval>().IsTriggeredOn((Expression<Func<ProgressWorksheetEntry, PXAction<PMProgressWorksheet>>>) (g => g.removeHold)).When((BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ISharedCondition) conditions.IsNotApproved)));
      }));
      transitions.AddGroupFrom<ProgressWorksheetStatus.pendingApproval>((Action<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.INeedTarget, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.IConfigured>) (t => (BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.IConfigured) t.To<ProgressWorksheetStatus.open>().IsTriggeredOn(approve).When((BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.INeedTarget, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.IConfigured>) (t => (BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.IConfigured) t.To<ProgressWorksheetStatus.rejected>().IsTriggeredOn(reject).When((BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ISharedCondition) conditions.IsRejected)));
        ts.Add((Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.INeedTarget, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.IConfigured>) (t => (BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.IConfigured) t.To<ProgressWorksheetStatus.onHold>().IsTriggeredOn((Expression<Func<ProgressWorksheetEntry, PXAction<PMProgressWorksheet>>>) (g => g.hold))));
      }));
      transitions.AddGroupFrom<ProgressWorksheetStatus.rejected>((Action<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.INeedTarget, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.IConfigured>) (t => (BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Transition.IConfigured) t.To<ProgressWorksheetStatus.onHold>().IsTriggeredOn((Expression<Func<ProgressWorksheetEntry, PXAction<PMProgressWorksheet>>>) (g => g.hold))))));
    })))).WithActions((Action<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approve);
      actions.Add(reject);
      actions.Add(reassign);
      actions.Update((Expression<Func<ProgressWorksheetEntry, PXAction<PMProgressWorksheet>>>) (g => g.hold), (Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionDefinition.ConfiguratorAction, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((Action<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Assignment.FieldContainerAdjusterAssignment>) (fa =>
      {
        fa.Add<PMProgressWorksheet.approved>((Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Assignment.INeedRightOperand, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Assignment.IConfigured>) (f => (BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fa.Add<PMProgressWorksheet.rejected>((Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Assignment.INeedRightOperand, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Assignment.IConfigured>) (f => (BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Assignment.IConfigured) f.SetFromValue((object) false)));
      }))));
    })).WithCategories((Action<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionCategory.ContainerAdjusterCategories>) (categories =>
    {
      categories.Add(approvalCategory);
      categories.Update("Approval", (Func<BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionCategory.ConfiguratorCategory, BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(context.Categories.Get("Corrections"))));
    }))));

    BoundedTo<ProgressWorksheetEntry, PMProgressWorksheet>.Condition Bql<T>() where T : IBqlUnary, new()
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
    return ((PXGraphExtension<ProgressWorksheetEntry>) this).Base.Approval.ReassignApproval(adapter);
  }

  private class ProgressWorksheetSetupApproval : IPrefetchable, IPXCompanyDependent
  {
    private bool RequestApproval;

    public static bool IsActive
    {
      get
      {
        return PXDatabase.GetSlot<ProgressWorksheetEntry_ApprovalWorkflow.ProgressWorksheetSetupApproval>(nameof (ProgressWorksheetSetupApproval), new Type[1]
        {
          typeof (PMSetup)
        }).RequestApproval;
      }
    }

    void IPrefetchable.Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PMSetup>(new PXDataField[1]
      {
        (PXDataField) new PXDataField<PMSetup.progressWorksheetApprovalMapID>()
      }))
      {
        if (pxDataRecord == null)
          return;
        this.RequestApproval = pxDataRecord.GetInt32(0).HasValue;
      }
    }
  }
}
