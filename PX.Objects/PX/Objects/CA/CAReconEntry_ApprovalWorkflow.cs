// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAReconEntry_ApprovalWorkflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.CM.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CA;

public class CAReconEntry_ApprovalWorkflow : PXGraphExtension<CAReconEntry_Workflow, CAReconEntry>
{
  public PXAction<CARecon> approve;
  public PXAction<CARecon> reject;
  public PXAction<CARecon> reassignApproval;

  private static bool ApprovalIsActive => PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>();

  [PXWorkflowDependsOnType(new System.Type[] {typeof (CASetupApproval)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    CAReconEntry_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<CAReconEntry, CARecon>());
  }

  protected static void Configure(WorkflowContext<CAReconEntry, CARecon> context)
  {
    BoundedTo<CAReconEntry, CARecon>.ActionCategory.IConfigured approvalCategory = context.Categories.Get("ApprovalID");
    CAReconEntry_ApprovalWorkflow.Conditions conditions = context.Conditions.GetPack<CAReconEntry_ApprovalWorkflow.Conditions>();
    BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IConfigured approve = context.ActionDefinitions.CreateExisting<CAReconEntry_ApprovalWorkflow>((Expression<Func<CAReconEntry_ApprovalWorkflow, PXAction<CARecon>>>) (g => g.approve), (Func<BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IConfigured>) (a => (BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, (Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.releaseFromHold)).IsHiddenWhen((BoundedTo<CAReconEntry, CARecon>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<CAReconEntry, CARecon>.Assignment.IContainerFillerFields>) (fa => fa.Add<CARecon.approved>((Func<BoundedTo<CAReconEntry, CARecon>.Assignment.INeedRightOperand, BoundedTo<CAReconEntry, CARecon>.Assignment.IConfigured>) (e => (BoundedTo<CAReconEntry, CARecon>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IConfigured reject = context.ActionDefinitions.CreateExisting<CAReconEntry_ApprovalWorkflow>((Expression<Func<CAReconEntry_ApprovalWorkflow, PXAction<CARecon>>>) (g => g.reject), (Func<BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IConfigured>) (a => (BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IConfigured) a.InFolder(approvalCategory, approve).IsHiddenWhen((BoundedTo<CAReconEntry, CARecon>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<CAReconEntry, CARecon>.Assignment.IContainerFillerFields>) (fa => fa.Add<CARecon.rejected>((Func<BoundedTo<CAReconEntry, CARecon>.Assignment.INeedRightOperand, BoundedTo<CAReconEntry, CARecon>.Assignment.IConfigured>) (e => (BoundedTo<CAReconEntry, CARecon>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
    BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IConfigured reassign = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IConfigured>) (a => (BoundedTo<CAReconEntry, CARecon>.ActionDefinition.IConfigured) a.PlaceAfter(reject).IsHiddenWhen((BoundedTo<CAReconEntry, CARecon>.ISharedCondition) conditions.IsApprovalDisabled)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<CAReconEntry, CARecon>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<CAReconEntry, CARecon>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow(new Func<BoundedTo<CAReconEntry, CARecon>.Workflow.ConfiguratorFlow, BoundedTo<CAReconEntry, CARecon>.Workflow.ConfiguratorFlow>(InjectApprovalWorkflow)).WithActions((Action<BoundedTo<CAReconEntry, CARecon>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approve);
      actions.Add(reject);
      actions.Add(reassign);
      actions.Update((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.putOnHold), (Func<BoundedTo<CAReconEntry, CARecon>.ActionDefinition.ConfiguratorAction, BoundedTo<CAReconEntry, CARecon>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((Action<BoundedTo<CAReconEntry, CARecon>.Assignment.FieldContainerAdjusterAssignment>) (fas =>
      {
        fas.Add<CARecon.approved>((Func<BoundedTo<CAReconEntry, CARecon>.Assignment.INeedRightOperand, BoundedTo<CAReconEntry, CARecon>.Assignment.IConfigured>) (f => (BoundedTo<CAReconEntry, CARecon>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fas.Add<CARecon.rejected>((Func<BoundedTo<CAReconEntry, CARecon>.Assignment.INeedRightOperand, BoundedTo<CAReconEntry, CARecon>.Assignment.IConfigured>) (f => (BoundedTo<CAReconEntry, CARecon>.Assignment.IConfigured) f.SetFromValue((object) false)));
      }))));
    }))));

    BoundedTo<CAReconEntry, CARecon>.Workflow.ConfiguratorFlow InjectApprovalWorkflow(
      BoundedTo<CAReconEntry, CARecon>.Workflow.ConfiguratorFlow flow)
    {
      return flow.WithFlowStates((Action<BoundedTo<CAReconEntry, CARecon>.BaseFlowStep.ContainerAdjusterStates>) (states =>
      {
        states.Add<CADocStatus.pending>((Func<BoundedTo<CAReconEntry, CARecon>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CAReconEntry, CARecon>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CAReconEntry, CARecon>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<CAReconEntry, CARecon>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add(approve, (Func<BoundedTo<CAReconEntry, CARecon>.ActionState.IAllowOptionalConfig, BoundedTo<CAReconEntry, CARecon>.ActionState.IConfigured>) (a => (BoundedTo<CAReconEntry, CARecon>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add(reject, (Func<BoundedTo<CAReconEntry, CARecon>.ActionState.IAllowOptionalConfig, BoundedTo<CAReconEntry, CARecon>.ActionState.IConfigured>) (a => (BoundedTo<CAReconEntry, CARecon>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add(reassign, (Func<BoundedTo<CAReconEntry, CARecon>.ActionState.IAllowOptionalConfig, BoundedTo<CAReconEntry, CARecon>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.putOnHold), (Func<BoundedTo<CAReconEntry, CARecon>.ActionState.IAllowOptionalConfig, BoundedTo<CAReconEntry, CARecon>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.printReconciliationReport), (Func<BoundedTo<CAReconEntry, CARecon>.ActionState.IAllowOptionalConfig, BoundedTo<CAReconEntry, CARecon>.ActionState.IConfigured>) null);
        }))));
        states.Add<CADocStatus.rejected>((Func<BoundedTo<CAReconEntry, CARecon>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CAReconEntry, CARecon>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CAReconEntry, CARecon>.BaseFlowStep.IConfigured) ((BoundedTo<CAReconEntry, CARecon>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<CAReconEntry, CARecon>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.putOnHold), (Func<BoundedTo<CAReconEntry, CARecon>.ActionState.IAllowOptionalConfig, BoundedTo<CAReconEntry, CARecon>.ActionState.IConfigured>) (a => (BoundedTo<CAReconEntry, CARecon>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.printReconciliationReport), (Func<BoundedTo<CAReconEntry, CARecon>.ActionState.IAllowOptionalConfig, BoundedTo<CAReconEntry, CARecon>.ActionState.IConfigured>) null);
        }))).WithFieldStates((Action<BoundedTo<CAReconEntry, CARecon>.FieldState.IContainerFillerFields>) (fields =>
        {
          fields.AddAllFields<CARecon>((Func<BoundedTo<CAReconEntry, CARecon>.FieldState.INeedAnyConfigField, BoundedTo<CAReconEntry, CARecon>.FieldState.IConfigured>) (table => (BoundedTo<CAReconEntry, CARecon>.FieldState.IConfigured) table.IsDisabled()));
          fields.AddTable<CAReconEntry.CATranExt>((Func<BoundedTo<CAReconEntry, CARecon>.FieldState.INeedAnyConfigField, BoundedTo<CAReconEntry, CARecon>.FieldState.IConfigured>) (table => (BoundedTo<CAReconEntry, CARecon>.FieldState.IConfigured) table.IsDisabled()));
        }))));
      })).WithTransitions((Action<BoundedTo<CAReconEntry, CARecon>.Transition.ContainerAdjusterTransitions>) (transitions =>
      {
        transitions.UpdateGroupFrom("_", (Action<BoundedTo<CAReconEntry, CARecon>.Transition.SourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<CAReconEntry, CARecon>.Transition.INeedTarget, BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured>) (t => (BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured) t.To<CADocStatus.pending>().IsTriggeredOn((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.initializeState)).When((BoundedTo<CAReconEntry, CARecon>.ISharedCondition) conditions.IsNotOnHoldAndIsNotApproved)));
          ts.Update((Func<BoundedTo<CAReconEntry, CARecon>.Transition.INeedTarget, BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured>) (t => (BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured) t.To<CADocStatus.balanced>().IsTriggeredOn((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.initializeState))), (Func<BoundedTo<CAReconEntry, CARecon>.Transition.ConfiguratorTransition, BoundedTo<CAReconEntry, CARecon>.Transition.ConfiguratorTransition>) (t => t.When((BoundedTo<CAReconEntry, CARecon>.ISharedCondition) conditions.IsNotOnHoldAndIsApproved)));
        }));
        transitions.UpdateGroupFrom<CADocStatus.hold>((Action<BoundedTo<CAReconEntry, CARecon>.Transition.SourceContainerFillerTransitions>) (ts =>
        {
          ts.Update((Func<BoundedTo<CAReconEntry, CARecon>.Transition.INeedTarget, BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured>) (t => (BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured) t.To<CADocStatus.balanced>().IsTriggeredOn((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.releaseFromHold))), (Func<BoundedTo<CAReconEntry, CARecon>.Transition.ConfiguratorTransition, BoundedTo<CAReconEntry, CARecon>.Transition.ConfiguratorTransition>) (t => t.When((BoundedTo<CAReconEntry, CARecon>.ISharedCondition) conditions.IsApproved)));
          ts.Add((Func<BoundedTo<CAReconEntry, CARecon>.Transition.INeedTarget, BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured>) (t =>
          {
            BoundedTo<CAReconEntry, CARecon>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<CADocStatus.pending>().IsTriggeredOn((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.releaseFromHold));
            BoundedTo<CAReconEntry, CARecon>.Condition isNotApproved = conditions.IsNotApproved;
            BoundedTo<CAReconEntry, CARecon>.Condition condition = BoundedTo<CAReconEntry, CARecon>.Condition.op_False(isNotApproved) ? isNotApproved : BoundedTo<CAReconEntry, CARecon>.Condition.op_BitwiseAnd(isNotApproved, conditions.ReconDiffIsZero);
            return (BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<CAReconEntry, CARecon>.ISharedCondition) condition);
          }));
          ts.Add((Func<BoundedTo<CAReconEntry, CARecon>.Transition.INeedTarget, BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured>) (t => (BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured) t.To<CADocStatus.rejected>().IsTriggeredOn((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.releaseFromHold)).When((BoundedTo<CAReconEntry, CARecon>.ISharedCondition) conditions.IsRejected)));
          ts.Update((Func<BoundedTo<CAReconEntry, CARecon>.Transition.INeedTarget, BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured>) (t => (BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured) t.To<CADocStatus.balanced>().IsTriggeredOn((Expression<Func<CAReconEntry, PXWorkflowEventHandlerBase<CARecon>>>) (g => g.OnUpdateStatus))), (Func<BoundedTo<CAReconEntry, CARecon>.Transition.ConfiguratorTransition, BoundedTo<CAReconEntry, CARecon>.Transition.ConfiguratorTransition>) (t => t.When((BoundedTo<CAReconEntry, CARecon>.ISharedCondition) conditions.IsApproved)));
          ts.Add((Func<BoundedTo<CAReconEntry, CARecon>.Transition.INeedTarget, BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured>) (t =>
          {
            BoundedTo<CAReconEntry, CARecon>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<CADocStatus.pending>().IsTriggeredOn((Expression<Func<CAReconEntry, PXWorkflowEventHandlerBase<CARecon>>>) (g => g.OnUpdateStatus));
            BoundedTo<CAReconEntry, CARecon>.Condition isNotApproved = conditions.IsNotApproved;
            BoundedTo<CAReconEntry, CARecon>.Condition condition = BoundedTo<CAReconEntry, CARecon>.Condition.op_False(isNotApproved) ? isNotApproved : BoundedTo<CAReconEntry, CARecon>.Condition.op_BitwiseAnd(isNotApproved, conditions.ReconDiffIsZero);
            return (BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<CAReconEntry, CARecon>.ISharedCondition) condition);
          }));
          ts.Add((Func<BoundedTo<CAReconEntry, CARecon>.Transition.INeedTarget, BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured>) (t => (BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured) t.To<CADocStatus.rejected>().IsTriggeredOn((Expression<Func<CAReconEntry, PXWorkflowEventHandlerBase<CARecon>>>) (g => g.OnUpdateStatus)).DoesNotPersist().When((BoundedTo<CAReconEntry, CARecon>.ISharedCondition) conditions.IsRejected)));
        }));
        transitions.AddGroupFrom<CADocStatus.pending>((Action<BoundedTo<CAReconEntry, CARecon>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<CAReconEntry, CARecon>.Transition.INeedTarget, BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured>) (t => (BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured) t.To<CADocStatus.balanced>().IsTriggeredOn(approve).When((BoundedTo<CAReconEntry, CARecon>.ISharedCondition) conditions.IsApproved)));
          ts.Add((Func<BoundedTo<CAReconEntry, CARecon>.Transition.INeedTarget, BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured>) (t => (BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured) t.To<CADocStatus.rejected>().IsTriggeredOn(reject).When((BoundedTo<CAReconEntry, CARecon>.ISharedCondition) conditions.IsRejected)));
          ts.Add((Func<BoundedTo<CAReconEntry, CARecon>.Transition.INeedTarget, BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured>) (t => (BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured) t.To<CADocStatus.hold>().IsTriggeredOn((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.putOnHold)).DoesNotPersist()));
        }));
        transitions.AddGroupFrom<CADocStatus.rejected>((Action<BoundedTo<CAReconEntry, CARecon>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<CAReconEntry, CARecon>.Transition.INeedTarget, BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured>) (t => (BoundedTo<CAReconEntry, CARecon>.Transition.IConfigured) t.To<CADocStatus.hold>().IsTriggeredOn((Expression<Func<CAReconEntry, PXAction<CARecon>>>) (g => g.putOnHold)).DoesNotPersist()))));
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
    return ((PXGraphExtension<CAReconEntry>) this).Base.Approval.ReassignApproval(adapter);
  }

  [PXDefault(typeof (CARecon.reconDate))]
  [PXMergeAttributes]
  protected virtual void EPApproval_DocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (Search<CREmployee.defContactID, Where<BqlOperand<CREmployee.bAccountID, IBqlInt>.IsEqual<BqlField<CARecon.employeeID, IBqlInt>.FromCurrent>>>))]
  [PXMergeAttributes]
  protected virtual void EPApproval_DocumentOwnerID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (CARecon.reconNbr))]
  [PXMergeAttributes]
  protected virtual void EPApproval_Descr_CacheAttached(PXCache sender)
  {
  }

  [CurrencyInfo(typeof (CARecon.curyInfoID))]
  [PXMergeAttributes]
  protected virtual void EPApproval_CuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (CARecon.curyBalance))]
  [PXMergeAttributes]
  protected virtual void EPApproval_CuryTotalAmount_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (CARecon.curyBalance))]
  [PXMergeAttributes]
  protected virtual void EPApproval_TotalAmount_CacheAttached(PXCache sender)
  {
  }

  protected virtual void EPApproval_Details_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<CARecon>) ((PXGraphExtension<CAReconEntry>) this).Base.CAReconRecords).Current == null)
      return;
    CARecon current = ((PXSelectBase<CARecon>) ((PXGraphExtension<CAReconEntry>) this).Base.CAReconRecords).Current;
    Decimal reconciledDebits = PX.Objects.CM.PXCurrencyAttribute.RoundCury(((PXSelectBase) ((PXGraphExtension<CAReconEntry>) this).Base.CAReconRecords).Cache, (object) current, current.CuryReconciledDebits.GetValueOrDefault());
    Decimal reconciledCredits = PX.Objects.CM.PXCurrencyAttribute.RoundCury(((PXSelectBase) ((PXGraphExtension<CAReconEntry>) this).Base.CAReconRecords).Cache, (object) current, current.CuryReconciledCredits.GetValueOrDefault());
    e.NewValue = (object) CAReconEntry_ApprovalWorkflow.BuildEPApprovalDetailsString(((PXSelectBase<CashAccount>) ((PXGraphExtension<CAReconEntry>) this).Base.cashaccount).Current, reconciledDebits, reconciledCredits);
  }

  private static string BuildEPApprovalDetailsString(
    CashAccount cashAccount,
    Decimal reconciledDebits,
    Decimal reconciledCredits)
  {
    return $"{cashAccount.CashAccountCD} - {cashAccount.Descr} (Reconciled Receipts: {reconciledDebits} , Reconciled Disb.: {reconciledCredits})";
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

    private static CAReconEntry_ApprovalWorkflow.SetupApproval Slot
    {
      get
      {
        return PXDatabase.GetSlot<CAReconEntry_ApprovalWorkflow.SetupApproval>(typeof (CAReconEntry_ApprovalWorkflow.SetupApproval).FullName, new System.Type[1]
        {
          typeof (CASetupApproval)
        });
      }
    }

    public static bool IsRequestApproval
    {
      get
      {
        return PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() && CAReconEntry_ApprovalWorkflow.SetupApproval.Slot.RequestApproval;
      }
    }
  }

  public class Conditions : BoundedTo<CAReconEntry, CARecon>.Condition.Pack
  {
    public BoundedTo<CAReconEntry, CARecon>.Condition IsNotOnHoldAndIsApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CAReconEntry, CARecon>.Condition.ConditionBuilder, BoundedTo<CAReconEntry, CARecon>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CARecon.hold, Equal<False>>>>>.And<BqlOperand<CARecon.approved, IBqlBool>.IsEqual<True>>>()), nameof (IsNotOnHoldAndIsApproved));
      }
    }

    public BoundedTo<CAReconEntry, CARecon>.Condition IsNotOnHoldAndIsNotApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CAReconEntry, CARecon>.Condition.ConditionBuilder, BoundedTo<CAReconEntry, CARecon>.Condition>) (c => c.FromBql<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CARecon.hold, Equal<False>>>>, And<BqlOperand<CARecon.approved, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<CARecon.excludeFromApproval, IBqlBool>.IsEqual<False>>>()), nameof (IsNotOnHoldAndIsNotApproved));
      }
    }

    public BoundedTo<CAReconEntry, CARecon>.Condition IsRejected
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CAReconEntry, CARecon>.Condition.ConditionBuilder, BoundedTo<CAReconEntry, CARecon>.Condition>) (c => c.FromBql<BqlOperand<CARecon.rejected, IBqlBool>.IsEqual<True>>()), nameof (IsRejected));
      }
    }

    public BoundedTo<CAReconEntry, CARecon>.Condition IsApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CAReconEntry, CARecon>.Condition.ConditionBuilder, BoundedTo<CAReconEntry, CARecon>.Condition>) (c => c.FromBql<BqlOperand<CARecon.approved, IBqlBool>.IsEqual<True>>()), nameof (IsApproved));
      }
    }

    public BoundedTo<CAReconEntry, CARecon>.Condition IsNotApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CAReconEntry, CARecon>.Condition.ConditionBuilder, BoundedTo<CAReconEntry, CARecon>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CARecon.approved, Equal<False>>>>>.And<BqlOperand<CARecon.rejected, IBqlBool>.IsEqual<False>>>()), nameof (IsNotApproved));
      }
    }

    public BoundedTo<CAReconEntry, CARecon>.Condition ReconDiffIsZero
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CAReconEntry, CARecon>.Condition.ConditionBuilder, BoundedTo<CAReconEntry, CARecon>.Condition>) (c => c.FromBql<BqlOperand<CARecon.curyDiffBalance, IBqlDecimal>.IsEqual<decimal0>>()), nameof (ReconDiffIsZero));
      }
    }

    public BoundedTo<CAReconEntry, CARecon>.Condition IsApprovalDisabled
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CAReconEntry, CARecon>.Condition.ConditionBuilder, BoundedTo<CAReconEntry, CARecon>.Condition>) (c => !CAReconEntry_ApprovalWorkflow.SetupApproval.IsRequestApproval ? c.FromBql<BqlOperand<CARecon.status, IBqlString>.IsNotIn<CADocStatus.pending, CADocStatus.rejected>>() : c.FromBql<BqlOperand<True, IBqlBool>.IsEqual<False>>()), nameof (IsApprovalDisabled));
      }
    }
  }
}
