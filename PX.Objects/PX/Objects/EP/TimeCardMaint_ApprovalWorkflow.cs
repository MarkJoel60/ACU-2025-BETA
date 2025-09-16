// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.TimeCardMaint_ApprovalWorkflow
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
namespace PX.Objects.EP;

public class TimeCardMaint_ApprovalWorkflow : PXGraphExtension<TimeCardMaint_Workflow, TimeCardMaint>
{
  public PXAction<EPTimeCard> approve;
  public PXAction<EPTimeCard> reject;
  public PXAction<EPTimeCard> reassignApproval;

  protected static bool ApprovalIsActive()
  {
    return PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.approvalWorkflow>() && TimeCardMaint_ApprovalWorkflow.TimeCardSetupApproval.IsActive;
  }

  [PXWorkflowDependsOnType(new System.Type[] {typeof (EPSetup)})]
  public sealed override void Configure(PXScreenConfiguration config)
  {
    TimeCardMaint_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<TimeCardMaint, EPTimeCard>());
  }

  protected static void Configure(WorkflowContext<TimeCardMaint, EPTimeCard> context)
  {
    BoundedTo<TimeCardMaint, EPTimeCard>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("Approval", (Func<BoundedTo<TimeCardMaint, EPTimeCard>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<TimeCardMaint, EPTimeCard>.ActionCategory.IConfigured>) (category => (BoundedTo<TimeCardMaint, EPTimeCard>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    var conditions = new
    {
      IsRejected = Bql<BqlOperand<EPTimeCard.isRejected, IBqlBool>.IsEqual<True>>(),
      IsApproved = Bql<BqlOperand<EPTimeCard.isApproved, IBqlBool>.IsEqual<True>>(),
      IsNotApproved = Bql<BqlOperand<EPTimeCard.isApproved, IBqlBool>.IsEqual<False>>(),
      IsApprovalDisabled = (TimeCardMaint_ApprovalWorkflow.ApprovalIsActive() ? Bql<BqlOperand<True, IBqlBool>.IsEqual<False>>() : Bql<BqlOperand<EPTimeCard.status, IBqlString>.IsNotIn<EPTimeCardStatusAttribute.openStatus, EPTimeCardStatusAttribute.rejectedStatus>>())
    }.AutoNameConditions();
    BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IConfigured approve = context.ActionDefinitions.CreateExisting<TimeCardMaint_ApprovalWorkflow>((Expression<Func<TimeCardMaint_ApprovalWorkflow, PXAction<EPTimeCard>>>) (g => g.approve), (Func<BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IConfigured>) (a => (BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, (Expression<Func<TimeCardMaint, PXAction<EPTimeCard>>>) (g => g.release)).PlaceAfter((Expression<Func<TimeCardMaint, PXAction<EPTimeCard>>>) (g => g.release)).IsHiddenWhen((BoundedTo<TimeCardMaint, EPTimeCard>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.Assignment.IContainerFillerFields>) (fa => fa.Add<EPTimeCard.isApproved>((Func<BoundedTo<TimeCardMaint, EPTimeCard>.Assignment.INeedRightOperand, BoundedTo<TimeCardMaint, EPTimeCard>.Assignment.IConfigured>) (e => (BoundedTo<TimeCardMaint, EPTimeCard>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IConfigured reject = context.ActionDefinitions.CreateExisting<TimeCardMaint_ApprovalWorkflow>((Expression<Func<TimeCardMaint_ApprovalWorkflow, PXAction<EPTimeCard>>>) (g => g.reject), (Func<BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IConfigured>) (a => (BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, approve).PlaceAfter(approve).IsHiddenWhen((BoundedTo<TimeCardMaint, EPTimeCard>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.Assignment.IContainerFillerFields>) (fa => fa.Add<EPTimeCard.isRejected>((Func<BoundedTo<TimeCardMaint, EPTimeCard>.Assignment.INeedRightOperand, BoundedTo<TimeCardMaint, EPTimeCard>.Assignment.IConfigured>) (e => (BoundedTo<TimeCardMaint, EPTimeCard>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IConfigured reassign = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IConfigured>) (a => (BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter(reject).IsHiddenWhen((BoundedTo<TimeCardMaint, EPTimeCard>.ISharedCondition) conditions.IsApprovalDisabled)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<TimeCardMaint, EPTimeCard>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<TimeCardMaint, EPTimeCard>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow((Func<BoundedTo<TimeCardMaint, EPTimeCard>.Workflow.ConfiguratorFlow, BoundedTo<TimeCardMaint, EPTimeCard>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.BaseFlowStep.ContainerAdjusterStates>) (fss =>
    {
      fss.Add<EPTimeCardStatusAttribute.openStatus>((Func<BoundedTo<TimeCardMaint, EPTimeCard>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TimeCardMaint, EPTimeCard>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<TimeCardMaint, EPTimeCard>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(approve, (Func<BoundedTo<TimeCardMaint, EPTimeCard>.ActionState.IAllowOptionalConfig, BoundedTo<TimeCardMaint, EPTimeCard>.ActionState.IConfigured>) (c => (BoundedTo<TimeCardMaint, EPTimeCard>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add(reject, (Func<BoundedTo<TimeCardMaint, EPTimeCard>.ActionState.IAllowOptionalConfig, BoundedTo<TimeCardMaint, EPTimeCard>.ActionState.IConfigured>) (c => (BoundedTo<TimeCardMaint, EPTimeCard>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add(reassign);
        actions.Add((Expression<Func<TimeCardMaint, PXAction<EPTimeCard>>>) (g => g.edit));
      }))));
      fss.Add<EPTimeCardStatusAttribute.rejectedStatus>((Func<BoundedTo<TimeCardMaint, EPTimeCard>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TimeCardMaint, EPTimeCard>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<TimeCardMaint, EPTimeCard>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<TimeCardMaint, PXAction<EPTimeCard>>>) (g => g.edit), (Func<BoundedTo<TimeCardMaint, EPTimeCard>.ActionState.IAllowOptionalConfig, BoundedTo<TimeCardMaint, EPTimeCard>.ActionState.IConfigured>) (c => (BoundedTo<TimeCardMaint, EPTimeCard>.ActionState.IConfigured) c.IsDuplicatedInToolbar()))))));
    })).WithTransitions((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.Transition.ContainerAdjusterTransitions>) (transitions =>
    {
      transitions.UpdateGroupFrom<EPTimeCardStatusAttribute.holdStatus>((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.Transition.SourceContainerFillerTransitions>) (ts =>
      {
        ts.Update((Func<BoundedTo<TimeCardMaint, EPTimeCard>.Transition.INeedTarget, BoundedTo<TimeCardMaint, EPTimeCard>.Transition.IConfigured>) (t => (BoundedTo<TimeCardMaint, EPTimeCard>.Transition.IConfigured) t.To<EPTimeCardStatusAttribute.approvedStatus>().IsTriggeredOn((Expression<Func<TimeCardMaint, PXAction<EPTimeCard>>>) (g => g.submit))), (Func<BoundedTo<TimeCardMaint, EPTimeCard>.Transition.ConfiguratorTransition, BoundedTo<TimeCardMaint, EPTimeCard>.Transition.ConfiguratorTransition>) (t => t.When((BoundedTo<TimeCardMaint, EPTimeCard>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<TimeCardMaint, EPTimeCard>.Transition.INeedTarget, BoundedTo<TimeCardMaint, EPTimeCard>.Transition.IConfigured>) (t => (BoundedTo<TimeCardMaint, EPTimeCard>.Transition.IConfigured) t.To<EPTimeCardStatusAttribute.openStatus>().IsTriggeredOn((Expression<Func<TimeCardMaint, PXAction<EPTimeCard>>>) (g => g.submit)).When((BoundedTo<TimeCardMaint, EPTimeCard>.ISharedCondition) conditions.IsNotApproved)));
        ts.Add((Func<BoundedTo<TimeCardMaint, EPTimeCard>.Transition.INeedTarget, BoundedTo<TimeCardMaint, EPTimeCard>.Transition.IConfigured>) (t => (BoundedTo<TimeCardMaint, EPTimeCard>.Transition.IConfigured) t.To<EPTimeCardStatusAttribute.rejectedStatus>().IsTriggeredOn((Expression<Func<TimeCardMaint, PXWorkflowEventHandlerBase<EPTimeCard>>>) (g => g.OnUpdateStatus)).When((BoundedTo<TimeCardMaint, EPTimeCard>.ISharedCondition) conditions.IsRejected)));
      }));
      transitions.AddGroupFrom<EPTimeCardStatusAttribute.openStatus>((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<TimeCardMaint, EPTimeCard>.Transition.INeedTarget, BoundedTo<TimeCardMaint, EPTimeCard>.Transition.IConfigured>) (t => (BoundedTo<TimeCardMaint, EPTimeCard>.Transition.IConfigured) t.To<EPTimeCardStatusAttribute.approvedStatus>().IsTriggeredOn(approve).When((BoundedTo<TimeCardMaint, EPTimeCard>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<TimeCardMaint, EPTimeCard>.Transition.INeedTarget, BoundedTo<TimeCardMaint, EPTimeCard>.Transition.IConfigured>) (t => (BoundedTo<TimeCardMaint, EPTimeCard>.Transition.IConfigured) t.To<EPTimeCardStatusAttribute.rejectedStatus>().IsTriggeredOn(reject).When((BoundedTo<TimeCardMaint, EPTimeCard>.ISharedCondition) conditions.IsRejected)));
        ts.Add((Func<BoundedTo<TimeCardMaint, EPTimeCard>.Transition.INeedTarget, BoundedTo<TimeCardMaint, EPTimeCard>.Transition.IConfigured>) (t => (BoundedTo<TimeCardMaint, EPTimeCard>.Transition.IConfigured) t.To<EPTimeCardStatusAttribute.holdStatus>().IsTriggeredOn((Expression<Func<TimeCardMaint, PXAction<EPTimeCard>>>) (g => g.edit))));
      }));
      transitions.AddGroupFrom<EPTimeCardStatusAttribute.rejectedStatus>((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<TimeCardMaint, EPTimeCard>.Transition.INeedTarget, BoundedTo<TimeCardMaint, EPTimeCard>.Transition.IConfigured>) (t => (BoundedTo<TimeCardMaint, EPTimeCard>.Transition.IConfigured) t.To<EPTimeCardStatusAttribute.holdStatus>().IsTriggeredOn((Expression<Func<TimeCardMaint, PXAction<EPTimeCard>>>) (g => g.edit))))));
    })))).WithActions((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approve);
      actions.Add(reject);
      actions.Add(reassign);
      actions.Update((Expression<Func<TimeCardMaint, PXAction<EPTimeCard>>>) (g => g.edit), (Func<BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.ConfiguratorAction, BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.Assignment.FieldContainerAdjusterAssignment>) (fa =>
      {
        fa.Add<EPTimeCard.isApproved>((Func<BoundedTo<TimeCardMaint, EPTimeCard>.Assignment.INeedRightOperand, BoundedTo<TimeCardMaint, EPTimeCard>.Assignment.IConfigured>) (f => (BoundedTo<TimeCardMaint, EPTimeCard>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fa.Add<EPTimeCard.isRejected>((Func<BoundedTo<TimeCardMaint, EPTimeCard>.Assignment.INeedRightOperand, BoundedTo<TimeCardMaint, EPTimeCard>.Assignment.IConfigured>) (f => (BoundedTo<TimeCardMaint, EPTimeCard>.Assignment.IConfigured) f.SetFromValue((object) false)));
      }))));
    })).WithCategories((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.ActionCategory.ContainerAdjusterCategories>) (categories =>
    {
      categories.Add(approvalCategory);
      categories.Update("Approval", (Func<BoundedTo<TimeCardMaint, EPTimeCard>.ActionCategory.ConfiguratorCategory, BoundedTo<TimeCardMaint, EPTimeCard>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(context.Categories.Get("Corrections"))));
    }))));

    BoundedTo<TimeCardMaint, EPTimeCard>.Condition Bql<T>() where T : IBqlUnary, new()
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

  private class TimeCardSetupApproval : IPrefetchable, IPXCompanyDependent
  {
    private bool RequestApproval;

    public static bool IsActive
    {
      get
      {
        return PXDatabase.GetSlot<TimeCardMaint_ApprovalWorkflow.TimeCardSetupApproval>(nameof (TimeCardSetupApproval), typeof (EPSetup)).RequestApproval;
      }
    }

    void IPrefetchable.Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<EPSetup>((PXDataField) new PXDataField<EPSetup.timeCardAssignmentMapID>()))
      {
        if (pxDataRecord == null)
          return;
        this.RequestApproval = pxDataRecord.GetInt32(0).HasValue;
      }
    }
  }
}
