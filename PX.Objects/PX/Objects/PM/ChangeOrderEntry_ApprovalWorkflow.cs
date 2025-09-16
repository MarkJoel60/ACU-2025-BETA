// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ChangeOrderEntry_ApprovalWorkflow
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

public class ChangeOrderEntry_ApprovalWorkflow : 
  PXGraphExtension<ChangeOrderEntry_Workflow, ChangeOrderEntry>
{
  public PXAction<PMChangeOrder> approve;
  public PXAction<PMChangeOrder> reassignApproval;

  protected static bool ApprovalIsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() && ChangeOrderEntry_ApprovalWorkflow.PMChangeOrderSetupApproval.IsActive;
  }

  [PXWorkflowDependsOnType(new Type[] {typeof (PMSetup)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    ChangeOrderEntry_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<ChangeOrderEntry, PMChangeOrder>());
  }

  protected static void Configure(
    WorkflowContext<ChangeOrderEntry, PMChangeOrder> context)
  {
    var conditions = new
    {
      IsRejected = Bql<BqlOperand<PMChangeOrder.rejected, IBqlBool>.IsEqual<True>>(),
      IsNotRejected = Bql<BqlOperand<PMChangeOrder.rejected, IBqlBool>.IsNotEqual<True>>(),
      IsApproved = Bql<BqlOperand<PMChangeOrder.approved, IBqlBool>.IsEqual<True>>(),
      IsNotApproved = Bql<BqlOperand<PMChangeOrder.approved, IBqlBool>.IsEqual<False>>(),
      IsApprovalDisabled = (ChangeOrderEntry_ApprovalWorkflow.ApprovalIsActive() ? Bql<BqlOperand<True, IBqlBool>.IsEqual<False>>() : Bql<BqlOperand<PMChangeOrder.status, IBqlString>.IsNotIn<ChangeOrderStatus.pendingApproval, ChangeOrderStatus.rejected>>())
    }.AutoNameConditions();
    BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("Approval", (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionCategory.IConfigured>) (category => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured approve = context.ActionDefinitions.CreateExisting<ChangeOrderEntry_ApprovalWorkflow>((Expression<Func<ChangeOrderEntry_ApprovalWorkflow, PXAction<PMChangeOrder>>>) (g => g.approve), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured>) (a => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, (Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.removeHold)).PlaceAfter((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.removeHold)).IsHiddenWhen((BoundedTo<ChangeOrderEntry, PMChangeOrder>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMChangeOrder.approved>((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.INeedRightOperand, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.IConfigured>) (e => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured reassign = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured>) (a => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.reject)).IsHiddenWhen((BoundedTo<ChangeOrderEntry, PMChangeOrder>.ISharedCondition) conditions.IsApprovalDisabled)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Workflow.ConfiguratorFlow, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.BaseFlowStep.ContainerAdjusterStates>) (fss =>
    {
      fss.Add<ChangeOrderStatus.pendingApproval>((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(approve, (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured>) (c => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.reject), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured>) (c => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add(reassign, (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.hold), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.send), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.coCancel), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured>) null);
      }))));
      fss.Add<ChangeOrderStatus.rejected>((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.hold), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured>) (c => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.send), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.coCancel), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionState.IConfigured>) null);
      }))));
    })).WithTransitions((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.ContainerAdjusterTransitions>) (transitions =>
    {
      transitions.UpdateGroupFrom<ChangeOrderStatus.onHold>((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.SourceContainerFillerTransitions>) (ts =>
      {
        ts.Update((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.INeedTarget, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured>) (t => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured) t.To<ChangeOrderStatus.open>().IsTriggeredOn((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.removeHold))), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.ConfiguratorTransition, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.ConfiguratorTransition>) (t => t.When((BoundedTo<ChangeOrderEntry, PMChangeOrder>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.INeedTarget, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured>) (t => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured) t.To<ChangeOrderStatus.rejected>().IsTriggeredOn((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.removeHold)).When((BoundedTo<ChangeOrderEntry, PMChangeOrder>.ISharedCondition) conditions.IsRejected)));
        ts.Add((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.INeedTarget, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured>) (t => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured) t.To<ChangeOrderStatus.pendingApproval>().IsTriggeredOn((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.removeHold)).When((BoundedTo<ChangeOrderEntry, PMChangeOrder>.ISharedCondition) conditions.IsNotRejected).When((BoundedTo<ChangeOrderEntry, PMChangeOrder>.ISharedCondition) conditions.IsNotApproved)));
      }));
      transitions.AddGroupFrom<ChangeOrderStatus.pendingApproval>((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.INeedTarget, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured>) (t => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured) t.To<ChangeOrderStatus.open>().IsTriggeredOn(approve).When((BoundedTo<ChangeOrderEntry, PMChangeOrder>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.INeedTarget, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured>) (t => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured) t.To<ChangeOrderStatus.rejected>().IsTriggeredOn((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.reject)).When((BoundedTo<ChangeOrderEntry, PMChangeOrder>.ISharedCondition) conditions.IsRejected)));
        ts.Add((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.INeedTarget, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured>) (t => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured) t.To<ChangeOrderStatus.onHold>().IsTriggeredOn((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.hold))));
        ts.Add((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.INeedTarget, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured>) (t => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured) t.To<ChangeOrderStatus.canceled>().IsTriggeredOn((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.coCancel))));
      }));
      transitions.AddGroupFrom<ChangeOrderStatus.rejected>((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.INeedTarget, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured>) (t => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured) t.To<ChangeOrderStatus.onHold>().IsTriggeredOn((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.hold))));
        ts.Add((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.INeedTarget, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured>) (t => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Transition.IConfigured) t.To<ChangeOrderStatus.canceled>().IsTriggeredOn((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.coCancel))));
      }));
    })))).WithActions((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approve);
      actions.Add((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.reject), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.IConfigured) c.InFolder(approvalCategory, approve).PlaceAfter(approve).IsHiddenWhen((BoundedTo<ChangeOrderEntry, PMChangeOrder>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMChangeOrder.rejected>((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.INeedRightOperand, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.IConfigured>) (e => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
      actions.Add(reassign);
      actions.Update((Expression<Func<ChangeOrderEntry, PXAction<PMChangeOrder>>>) (g => g.hold), (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.ConfiguratorAction, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.FieldContainerAdjusterAssignment>) (fa =>
      {
        fa.Add<PMChangeOrder.approved>((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.INeedRightOperand, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.IConfigured>) (f => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fa.Add<PMChangeOrder.rejected>((Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.INeedRightOperand, BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.IConfigured>) (f => (BoundedTo<ChangeOrderEntry, PMChangeOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
      }))));
    })).WithCategories((Action<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionCategory.ContainerAdjusterCategories>) (categories =>
    {
      categories.Add(approvalCategory);
      categories.Update("Approval", (Func<BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionCategory.ConfiguratorCategory, BoundedTo<ChangeOrderEntry, PMChangeOrder>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(context.Categories.Get("Corrections"))));
    }))));

    BoundedTo<ChangeOrderEntry, PMChangeOrder>.Condition Bql<T>() where T : IBqlUnary, new()
    {
      return context.Conditions.FromBql<T>();
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable Approve(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReassignApproval(PXAdapter adapter)
  {
    return ((PXGraphExtension<ChangeOrderEntry>) this).Base.Approval.ReassignApproval(adapter);
  }

  private class PMChangeOrderSetupApproval : IPrefetchable, IPXCompanyDependent
  {
    private bool RequestApproval;

    public static bool IsActive
    {
      get
      {
        return PXDatabase.GetSlot<ChangeOrderEntry_ApprovalWorkflow.PMChangeOrderSetupApproval>(nameof (PMChangeOrderSetupApproval), new Type[1]
        {
          typeof (PMSetup)
        }).RequestApproval;
      }
    }

    void IPrefetchable.Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PMSetup>(new PXDataField[1]
      {
        (PXDataField) new PXDataField<PMSetup.changeOrderApprovalMapID>()
      }))
      {
        if (pxDataRecord == null)
          return;
        this.RequestApproval = pxDataRecord.GetInt32(0).HasValue;
      }
    }
  }
}
