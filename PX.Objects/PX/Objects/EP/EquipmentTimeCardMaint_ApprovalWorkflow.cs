// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EquipmentTimeCardMaint_ApprovalWorkflow
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

public class EquipmentTimeCardMaint_ApprovalWorkflow : 
  PXGraphExtension<EquipmentTimeCardMaint_Workflow, EquipmentTimeCardMaint>
{
  public PXAction<EPEquipmentTimeCard> approve;
  public PXAction<EPEquipmentTimeCard> reject;
  public PXAction<EPEquipmentTimeCard> reassignApproval;

  protected static bool ApprovalIsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() && EquipmentTimeCardMaint_ApprovalWorkflow.EquipmentTimeCardSetupApproval.IsActive;
  }

  [PXWorkflowDependsOnType(new Type[] {typeof (EPSetup)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    EquipmentTimeCardMaint_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<EquipmentTimeCardMaint, EPEquipmentTimeCard>());
  }

  protected static void Configure(
    WorkflowContext<EquipmentTimeCardMaint, EPEquipmentTimeCard> context)
  {
    var conditions = new
    {
      IsRejected = Bql<BqlOperand<EPEquipmentTimeCard.isRejected, IBqlBool>.IsEqual<True>>(),
      IsApproved = Bql<BqlOperand<EPEquipmentTimeCard.isApproved, IBqlBool>.IsEqual<True>>(),
      IsNotApproved = Bql<BqlOperand<EPEquipmentTimeCard.isApproved, IBqlBool>.IsEqual<False>>(),
      IsApprovalDisabled = (EquipmentTimeCardMaint_ApprovalWorkflow.ApprovalIsActive() ? Bql<BqlOperand<True, IBqlBool>.IsEqual<False>>() : Bql<BqlOperand<EPEquipmentTimeCard.status, IBqlString>.IsNotIn<EPEquipmentTimeCardStatusAttribute.pendingApproval, EPEquipmentTimeCardStatusAttribute.rejected>>())
    }.AutoNameConditions();
    BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("Approval", (Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionCategory.IConfigured>) (category => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IConfigured approve = context.ActionDefinitions.CreateExisting<EquipmentTimeCardMaint_ApprovalWorkflow>((Expression<Func<EquipmentTimeCardMaint_ApprovalWorkflow, PXAction<EPEquipmentTimeCard>>>) (g => g.approve), (Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IConfigured>) (a => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, (Expression<Func<EquipmentTimeCardMaint, PXAction<EPEquipmentTimeCard>>>) (g => g.release)).PlaceAfter((Expression<Func<EquipmentTimeCardMaint, PXAction<EPEquipmentTimeCard>>>) (g => g.release)).IsHiddenWhen((BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.IContainerFillerFields>) (fa => fa.Add<EPEquipmentTimeCard.isApproved>((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.INeedRightOperand, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.IConfigured>) (e => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IConfigured reject = context.ActionDefinitions.CreateExisting<EquipmentTimeCardMaint_ApprovalWorkflow>((Expression<Func<EquipmentTimeCardMaint_ApprovalWorkflow, PXAction<EPEquipmentTimeCard>>>) (g => g.reject), (Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IConfigured>) (a => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, approve).PlaceAfter(approve).IsHiddenWhen((BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.IContainerFillerFields>) (fa => fa.Add<EPEquipmentTimeCard.isRejected>((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.INeedRightOperand, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.IConfigured>) (e => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IConfigured reassign = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IConfigured>) (a => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter(reject).IsHiddenWhen((BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ISharedCondition) conditions.IsApprovalDisabled)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Workflow.ConfiguratorFlow, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.BaseFlowStep.ContainerAdjusterStates>) (fss =>
    {
      fss.Add<EPEquipmentTimeCardStatusAttribute.pendingApproval>((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.FlowState.INeedAnyFlowStateConfig, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(approve, (Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IAllowOptionalConfig, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IConfigured>) (c => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add(reject, (Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IAllowOptionalConfig, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IConfigured>) (c => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add(reassign, (Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IAllowOptionalConfig, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<EquipmentTimeCardMaint, PXAction<EPEquipmentTimeCard>>>) (g => g.edit), (Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IAllowOptionalConfig, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IConfigured>) null);
      }))));
      fss.Add<EPEquipmentTimeCardStatusAttribute.rejected>((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.FlowState.INeedAnyFlowStateConfig, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<EquipmentTimeCardMaint, PXAction<EPEquipmentTimeCard>>>) (g => g.edit), (Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IAllowOptionalConfig, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IConfigured>) (c => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IConfigured) c.IsDuplicatedInToolbar()))))));
    })).WithTransitions((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.ContainerAdjusterTransitions>) (transitions =>
    {
      transitions.UpdateGroupFrom<EPEquipmentTimeCardStatusAttribute.onHold>((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.SourceContainerFillerTransitions>) (ts =>
      {
        ts.Update((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.INeedTarget, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.IConfigured>) (t => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.IConfigured) t.To<EPEquipmentTimeCardStatusAttribute.approved>().IsTriggeredOn((Expression<Func<EquipmentTimeCardMaint, PXAction<EPEquipmentTimeCard>>>) (g => g.submit))), (Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.ConfiguratorTransition, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.ConfiguratorTransition>) (t => t.When((BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.INeedTarget, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.IConfigured>) (t => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.IConfigured) t.To<EPEquipmentTimeCardStatusAttribute.pendingApproval>().IsTriggeredOn((Expression<Func<EquipmentTimeCardMaint, PXAction<EPEquipmentTimeCard>>>) (g => g.submit)).When((BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ISharedCondition) conditions.IsNotApproved)));
        ts.Add((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.INeedTarget, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.IConfigured>) (t => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.IConfigured) t.To<EPEquipmentTimeCardStatusAttribute.rejected>().IsTriggeredOn((Expression<Func<EquipmentTimeCardMaint, PXWorkflowEventHandlerBase<EPEquipmentTimeCard>>>) (g => g.OnUpdateStatus)).When((BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ISharedCondition) conditions.IsRejected)));
      }));
      transitions.AddGroupFrom<EPEquipmentTimeCardStatusAttribute.pendingApproval>((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.INeedTarget, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.IConfigured>) (t => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.IConfigured) t.To<EPEquipmentTimeCardStatusAttribute.approved>().IsTriggeredOn(approve).When((BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.INeedTarget, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.IConfigured>) (t => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.IConfigured) t.To<EPEquipmentTimeCardStatusAttribute.rejected>().IsTriggeredOn(reject).When((BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ISharedCondition) conditions.IsRejected)));
        ts.Add((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.INeedTarget, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.IConfigured>) (t => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.IConfigured) t.To<EPEquipmentTimeCardStatusAttribute.onHold>().IsTriggeredOn((Expression<Func<EquipmentTimeCardMaint, PXAction<EPEquipmentTimeCard>>>) (g => g.edit))));
      }));
      transitions.AddGroupFrom<EPEquipmentTimeCardStatusAttribute.rejected>((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.INeedTarget, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.IConfigured>) (t => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.IConfigured) t.To<EPEquipmentTimeCardStatusAttribute.onHold>().IsTriggeredOn((Expression<Func<EquipmentTimeCardMaint, PXAction<EPEquipmentTimeCard>>>) (g => g.edit))))));
    })))).WithActions((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approve);
      actions.Add(reject);
      actions.Add(reassign);
      actions.Update((Expression<Func<EquipmentTimeCardMaint, PXAction<EPEquipmentTimeCard>>>) (g => g.edit), (Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.ConfiguratorAction, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.FieldContainerAdjusterAssignment>) (fa =>
      {
        fa.Add<EPEquipmentTimeCard.isApproved>((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.INeedRightOperand, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.IConfigured>) (f => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fa.Add<EPEquipmentTimeCard.isRejected>((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.INeedRightOperand, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.IConfigured>) (f => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.IConfigured) f.SetFromValue((object) false)));
      }))));
    })).WithCategories((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionCategory.ContainerAdjusterCategories>) (categories =>
    {
      categories.Add(approvalCategory);
      categories.Update("Approval", (Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionCategory.ConfiguratorCategory, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(context.Categories.Get("Processing"))));
    }))));

    BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Condition Bql<T>() where T : IBqlUnary, new()
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
    return ((PXGraphExtension<EquipmentTimeCardMaint>) this).Base.Approval.ReassignApproval(adapter);
  }

  private class EquipmentTimeCardSetupApproval : IPrefetchable, IPXCompanyDependent
  {
    private bool RequestApproval;

    public static bool IsActive
    {
      get
      {
        return PXDatabase.GetSlot<EquipmentTimeCardMaint_ApprovalWorkflow.EquipmentTimeCardSetupApproval>(nameof (EquipmentTimeCardSetupApproval), new Type[1]
        {
          typeof (EPSetup)
        }).RequestApproval;
      }
    }

    void IPrefetchable.Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<EPSetup>(new PXDataField[1]
      {
        (PXDataField) new PXDataField<EPSetup.equipmentTimeCardAssignmentMapID>()
      }))
      {
        if (pxDataRecord == null)
          return;
        this.RequestApproval = pxDataRecord.GetInt32(0).HasValue;
      }
    }
  }
}
