// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CATranEntry_ApprovalWorkflow
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
namespace PX.Objects.CA;

public class CATranEntry_ApprovalWorkflow : PXGraphExtension<CATranEntry_Workflow, CATranEntry>
{
  public PXAction<CAAdj> approve;
  public PXAction<CAAdj> reject;
  public PXAction<CAAdj> reassignApproval;

  private static bool ApprovalIsActive => PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>();

  [PXWorkflowDependsOnType(new Type[] {typeof (CASetupApproval)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    CATranEntry_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<CATranEntry, CAAdj>());
  }

  protected static void Configure(WorkflowContext<CATranEntry, CAAdj> context)
  {
    BoundedTo<CATranEntry, CAAdj>.ActionCategory.IConfigured approvalCategory = context.Categories.Get("ApprovalID");
    CATranEntry_ApprovalWorkflow.Conditions conditions = context.Conditions.GetPack<CATranEntry_ApprovalWorkflow.Conditions>();
    BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IConfigured approve = context.ActionDefinitions.CreateExisting<CATranEntry_ApprovalWorkflow>((Expression<Func<CATranEntry_ApprovalWorkflow, PXAction<CAAdj>>>) (g => g.approve), (Func<BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IConfigured>) (a => (BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, (Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.releaseFromHold)).PlaceAfter((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.releaseFromHold)).IsHiddenWhen((BoundedTo<CATranEntry, CAAdj>.ISharedCondition) conditions.IsApprovalDisabled).IsDisabledWhen(context.Conditions.Get("IsNotAdjustment")).WithFieldAssignments((Action<BoundedTo<CATranEntry, CAAdj>.Assignment.IContainerFillerFields>) (fa => fa.Add<CAAdj.approved>((Func<BoundedTo<CATranEntry, CAAdj>.Assignment.INeedRightOperand, BoundedTo<CATranEntry, CAAdj>.Assignment.IConfigured>) (e => (BoundedTo<CATranEntry, CAAdj>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IConfigured reject = context.ActionDefinitions.CreateExisting<CATranEntry_ApprovalWorkflow>((Expression<Func<CATranEntry_ApprovalWorkflow, PXAction<CAAdj>>>) (g => g.reject), (Func<BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IConfigured>) (a => (BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, approve).PlaceAfter(approve).IsHiddenWhen((BoundedTo<CATranEntry, CAAdj>.ISharedCondition) conditions.IsApprovalDisabled).IsDisabledWhen(context.Conditions.Get("IsNotAdjustment")).WithFieldAssignments((Action<BoundedTo<CATranEntry, CAAdj>.Assignment.IContainerFillerFields>) (fa => fa.Add<CAAdj.rejected>((Func<BoundedTo<CATranEntry, CAAdj>.Assignment.INeedRightOperand, BoundedTo<CATranEntry, CAAdj>.Assignment.IConfigured>) (e => (BoundedTo<CATranEntry, CAAdj>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IConfigured reassign = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IConfigured>) (a => (BoundedTo<CATranEntry, CAAdj>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter(reject).IsHiddenWhen((BoundedTo<CATranEntry, CAAdj>.ISharedCondition) conditions.IsApprovalDisabled)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<CATranEntry, CAAdj>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<CATranEntry, CAAdj>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow(new Func<BoundedTo<CATranEntry, CAAdj>.Workflow.ConfiguratorFlow, BoundedTo<CATranEntry, CAAdj>.Workflow.ConfiguratorFlow>(InjectApprovalWorkflow)).WithActions((Action<BoundedTo<CATranEntry, CAAdj>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approve);
      actions.Add(reject);
      actions.Add(reassign);
      actions.Update((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.putOnHold), (Func<BoundedTo<CATranEntry, CAAdj>.ActionDefinition.ConfiguratorAction, BoundedTo<CATranEntry, CAAdj>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((Action<BoundedTo<CATranEntry, CAAdj>.Assignment.FieldContainerAdjusterAssignment>) (fas =>
      {
        fas.Add<CAAdj.approved>((Func<BoundedTo<CATranEntry, CAAdj>.Assignment.INeedRightOperand, BoundedTo<CATranEntry, CAAdj>.Assignment.IConfigured>) (f => (BoundedTo<CATranEntry, CAAdj>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fas.Add<CAAdj.rejected>((Func<BoundedTo<CATranEntry, CAAdj>.Assignment.INeedRightOperand, BoundedTo<CATranEntry, CAAdj>.Assignment.IConfigured>) (f => (BoundedTo<CATranEntry, CAAdj>.Assignment.IConfigured) f.SetFromValue((object) false)));
      }))));
    }))));

    BoundedTo<CATranEntry, CAAdj>.Workflow.ConfiguratorFlow InjectApprovalWorkflow(
      BoundedTo<CATranEntry, CAAdj>.Workflow.ConfiguratorFlow flow)
    {
      return flow.WithFlowStates((Action<BoundedTo<CATranEntry, CAAdj>.BaseFlowStep.ContainerAdjusterStates>) (states =>
      {
        states.Add<CATransferStatus.pending>((Func<BoundedTo<CATranEntry, CAAdj>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CATranEntry, CAAdj>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CATranEntry, CAAdj>.BaseFlowStep.IConfigured) ((BoundedTo<CATranEntry, CAAdj>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<CATranEntry, CAAdj>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add(approve, (Func<BoundedTo<CATranEntry, CAAdj>.ActionState.IAllowOptionalConfig, BoundedTo<CATranEntry, CAAdj>.ActionState.IConfigured>) (a => (BoundedTo<CATranEntry, CAAdj>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add(reject, (Func<BoundedTo<CATranEntry, CAAdj>.ActionState.IAllowOptionalConfig, BoundedTo<CATranEntry, CAAdj>.ActionState.IConfigured>) (a => (BoundedTo<CATranEntry, CAAdj>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add(reassign, (Func<BoundedTo<CATranEntry, CAAdj>.ActionState.IAllowOptionalConfig, BoundedTo<CATranEntry, CAAdj>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.putOnHold), (Func<BoundedTo<CATranEntry, CAAdj>.ActionState.IAllowOptionalConfig, BoundedTo<CATranEntry, CAAdj>.ActionState.IConfigured>) null);
        }))).WithFieldStates((Action<BoundedTo<CATranEntry, CAAdj>.FieldState.IContainerFillerFields>) (fields =>
        {
          fields.AddAllFields<CAAdj>((Func<BoundedTo<CATranEntry, CAAdj>.FieldState.INeedAnyConfigField, BoundedTo<CATranEntry, CAAdj>.FieldState.IConfigured>) (table => (BoundedTo<CATranEntry, CAAdj>.FieldState.IConfigured) table.IsDisabled()));
          fields.AddField<CAAdj.adjRefNbr>((Func<BoundedTo<CATranEntry, CAAdj>.FieldState.INeedAnyConfigField, BoundedTo<CATranEntry, CAAdj>.FieldState.IConfigured>) null);
          fields.AddTable<CASplit>((Func<BoundedTo<CATranEntry, CAAdj>.FieldState.INeedAnyConfigField, BoundedTo<CATranEntry, CAAdj>.FieldState.IConfigured>) (table => (BoundedTo<CATranEntry, CAAdj>.FieldState.IConfigured) table.IsDisabled()));
          fields.AddTable<CATaxTran>((Func<BoundedTo<CATranEntry, CAAdj>.FieldState.INeedAnyConfigField, BoundedTo<CATranEntry, CAAdj>.FieldState.IConfigured>) (table => (BoundedTo<CATranEntry, CAAdj>.FieldState.IConfigured) table.IsDisabled()));
        }))));
        states.Add<CATransferStatus.rejected>((Func<BoundedTo<CATranEntry, CAAdj>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CATranEntry, CAAdj>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CATranEntry, CAAdj>.BaseFlowStep.IConfigured) ((BoundedTo<CATranEntry, CAAdj>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<CATranEntry, CAAdj>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.putOnHold), (Func<BoundedTo<CATranEntry, CAAdj>.ActionState.IAllowOptionalConfig, BoundedTo<CATranEntry, CAAdj>.ActionState.IConfigured>) (a => (BoundedTo<CATranEntry, CAAdj>.ActionState.IConfigured) a.IsDuplicatedInToolbar()))))).WithFieldStates((Action<BoundedTo<CATranEntry, CAAdj>.FieldState.IContainerFillerFields>) (fields =>
        {
          fields.AddAllFields<CAAdj>((Func<BoundedTo<CATranEntry, CAAdj>.FieldState.INeedAnyConfigField, BoundedTo<CATranEntry, CAAdj>.FieldState.IConfigured>) (table => (BoundedTo<CATranEntry, CAAdj>.FieldState.IConfigured) table.IsDisabled()));
          fields.AddField<CAAdj.adjRefNbr>((Func<BoundedTo<CATranEntry, CAAdj>.FieldState.INeedAnyConfigField, BoundedTo<CATranEntry, CAAdj>.FieldState.IConfigured>) null);
          fields.AddTable<CASplit>((Func<BoundedTo<CATranEntry, CAAdj>.FieldState.INeedAnyConfigField, BoundedTo<CATranEntry, CAAdj>.FieldState.IConfigured>) (table => (BoundedTo<CATranEntry, CAAdj>.FieldState.IConfigured) table.IsDisabled()));
          fields.AddTable<CATaxTran>((Func<BoundedTo<CATranEntry, CAAdj>.FieldState.INeedAnyConfigField, BoundedTo<CATranEntry, CAAdj>.FieldState.IConfigured>) (table => (BoundedTo<CATranEntry, CAAdj>.FieldState.IConfigured) table.IsDisabled()));
        }))));
      })).WithTransitions((Action<BoundedTo<CATranEntry, CAAdj>.Transition.ContainerAdjusterTransitions>) (transitions =>
      {
        transitions.UpdateGroupFrom("_", (Action<BoundedTo<CATranEntry, CAAdj>.Transition.SourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<CATranEntry, CAAdj>.Transition.INeedTarget, BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured>) (t => (BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured) t.To<CATransferStatus.pending>().IsTriggeredOn((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.initializeState)).When((BoundedTo<CATranEntry, CAAdj>.ISharedCondition) conditions.IsNotOnHoldAndIsNotApproved)));
          ts.Update((Func<BoundedTo<CATranEntry, CAAdj>.Transition.INeedTarget, BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured>) (t => (BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured) t.To<CATransferStatus.balanced>().IsTriggeredOn((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.initializeState))), (Func<BoundedTo<CATranEntry, CAAdj>.Transition.ConfiguratorTransition, BoundedTo<CATranEntry, CAAdj>.Transition.ConfiguratorTransition>) (t => t.When((BoundedTo<CATranEntry, CAAdj>.ISharedCondition) conditions.IsNotOnHoldAndIsApproved)));
        }));
        transitions.UpdateGroupFrom<CATransferStatus.hold>((Action<BoundedTo<CATranEntry, CAAdj>.Transition.SourceContainerFillerTransitions>) (ts =>
        {
          ts.Update((Func<BoundedTo<CATranEntry, CAAdj>.Transition.INeedTarget, BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured>) (t => (BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured) t.To<CATransferStatus.balanced>().IsTriggeredOn((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.releaseFromHold))), (Func<BoundedTo<CATranEntry, CAAdj>.Transition.ConfiguratorTransition, BoundedTo<CATranEntry, CAAdj>.Transition.ConfiguratorTransition>) (t => t.When((BoundedTo<CATranEntry, CAAdj>.ISharedCondition) conditions.IsApproved)));
          ts.Add((Func<BoundedTo<CATranEntry, CAAdj>.Transition.INeedTarget, BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured>) (t => (BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured) t.To<CATransferStatus.pending>().IsTriggeredOn((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.releaseFromHold)).DoesNotPersist().When((BoundedTo<CATranEntry, CAAdj>.ISharedCondition) conditions.IsNotApproved)));
          ts.Add((Func<BoundedTo<CATranEntry, CAAdj>.Transition.INeedTarget, BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured>) (t => (BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured) t.To<CATransferStatus.rejected>().IsTriggeredOn((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.releaseFromHold)).DoesNotPersist().When((BoundedTo<CATranEntry, CAAdj>.ISharedCondition) conditions.IsRejected)));
          ts.Add((Func<BoundedTo<CATranEntry, CAAdj>.Transition.INeedTarget, BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured>) (t => (BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured) t.To<CATransferStatus.pending>().IsTriggeredOn((Expression<Func<CATranEntry, PXWorkflowEventHandlerBase<CAAdj>>>) (g => g.OnUpdateStatus)).DoesNotPersist().When((BoundedTo<CATranEntry, CAAdj>.ISharedCondition) conditions.IsNotApproved)));
          ts.Add((Func<BoundedTo<CATranEntry, CAAdj>.Transition.INeedTarget, BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured>) (t => (BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured) t.To<CATransferStatus.rejected>().IsTriggeredOn((Expression<Func<CATranEntry, PXWorkflowEventHandlerBase<CAAdj>>>) (g => g.OnUpdateStatus)).DoesNotPersist().When((BoundedTo<CATranEntry, CAAdj>.ISharedCondition) conditions.IsRejected)));
        }));
        transitions.AddGroupFrom<CATransferStatus.pending>((Action<BoundedTo<CATranEntry, CAAdj>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<CATranEntry, CAAdj>.Transition.INeedTarget, BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured>) (t => (BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured) t.To<CATransferStatus.balanced>().IsTriggeredOn(approve).When((BoundedTo<CATranEntry, CAAdj>.ISharedCondition) conditions.IsApproved)));
          ts.Add((Func<BoundedTo<CATranEntry, CAAdj>.Transition.INeedTarget, BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured>) (t => (BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured) t.To<CATransferStatus.rejected>().IsTriggeredOn(reject).When((BoundedTo<CATranEntry, CAAdj>.ISharedCondition) conditions.IsRejected)));
          ts.Add((Func<BoundedTo<CATranEntry, CAAdj>.Transition.INeedTarget, BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured>) (t => (BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured) t.To<CATransferStatus.hold>().IsTriggeredOn((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.putOnHold)).DoesNotPersist()));
        }));
        transitions.AddGroupFrom<CATransferStatus.rejected>((Action<BoundedTo<CATranEntry, CAAdj>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<CATranEntry, CAAdj>.Transition.INeedTarget, BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured>) (t => (BoundedTo<CATranEntry, CAAdj>.Transition.IConfigured) t.To<CATransferStatus.hold>().IsTriggeredOn((Expression<Func<CATranEntry, PXAction<CAAdj>>>) (g => g.putOnHold)).DoesNotPersist()))));
      }));
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
    return ((PXGraphExtension<CATranEntry>) this).Base.Approval.ReassignApproval(adapter);
  }

  private class SetupApproval : IPrefetchable, IPXCompanyDependent
  {
    private bool RequestApproval;

    void IPrefetchable.Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<CASetupApproval>(new PXDataField[1]
      {
        (PXDataField) new PXDataField<CASetupApproval.isActive>()
      }))
      {
        if (pxDataRecord == null)
          return;
        this.RequestApproval = pxDataRecord.GetBoolean(0).Value;
      }
    }

    private static CATranEntry_ApprovalWorkflow.SetupApproval Slot
    {
      get
      {
        return PXDatabase.GetSlot<CATranEntry_ApprovalWorkflow.SetupApproval>(typeof (CATranEntry_ApprovalWorkflow.SetupApproval).FullName, new Type[1]
        {
          typeof (CASetupApproval)
        });
      }
    }

    public static bool IsRequestApproval
    {
      get
      {
        return PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() && CATranEntry_ApprovalWorkflow.SetupApproval.Slot.RequestApproval;
      }
    }
  }

  public class Conditions : BoundedTo<CATranEntry, CAAdj>.Condition.Pack
  {
    public BoundedTo<CATranEntry, CAAdj>.Condition IsNotOnHoldAndIsApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CATranEntry, CAAdj>.Condition.ConditionBuilder, BoundedTo<CATranEntry, CAAdj>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CAAdj.hold, Equal<False>>>>>.And<BqlOperand<CAAdj.approved, IBqlBool>.IsEqual<True>>>()), nameof (IsNotOnHoldAndIsApproved));
      }
    }

    public BoundedTo<CATranEntry, CAAdj>.Condition IsNotOnHoldAndIsNotApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CATranEntry, CAAdj>.Condition.ConditionBuilder, BoundedTo<CATranEntry, CAAdj>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CAAdj.hold, Equal<False>>>>>.And<BqlOperand<CAAdj.approved, IBqlBool>.IsEqual<False>>>()), nameof (IsNotOnHoldAndIsNotApproved));
      }
    }

    public BoundedTo<CATranEntry, CAAdj>.Condition IsRejected
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CATranEntry, CAAdj>.Condition.ConditionBuilder, BoundedTo<CATranEntry, CAAdj>.Condition>) (c => c.FromBql<BqlOperand<CAAdj.rejected, IBqlBool>.IsEqual<True>>()), nameof (IsRejected));
      }
    }

    public BoundedTo<CATranEntry, CAAdj>.Condition IsApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CATranEntry, CAAdj>.Condition.ConditionBuilder, BoundedTo<CATranEntry, CAAdj>.Condition>) (c => c.FromBql<BqlOperand<CAAdj.approved, IBqlBool>.IsEqual<True>>()), nameof (IsApproved));
      }
    }

    public BoundedTo<CATranEntry, CAAdj>.Condition IsNotApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CATranEntry, CAAdj>.Condition.ConditionBuilder, BoundedTo<CATranEntry, CAAdj>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CAAdj.approved, Equal<False>>>>>.And<BqlOperand<CAAdj.rejected, IBqlBool>.IsEqual<False>>>()), nameof (IsNotApproved));
      }
    }

    public BoundedTo<CATranEntry, CAAdj>.Condition IsApprovalDisabled
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CATranEntry, CAAdj>.Condition.ConditionBuilder, BoundedTo<CATranEntry, CAAdj>.Condition>) (c => !CATranEntry_ApprovalWorkflow.SetupApproval.IsRequestApproval ? c.FromBql<BqlOperand<CAAdj.status, IBqlString>.IsNotIn<CATransferStatus.pending, CATransferStatus.rejected>>() : c.FromBql<BqlOperand<True, IBqlBool>.IsEqual<False>>()), nameof (IsApprovalDisabled));
      }
    }
  }
}
