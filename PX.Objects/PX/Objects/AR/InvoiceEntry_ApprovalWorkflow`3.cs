// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.InvoiceEntry_ApprovalWorkflow`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using System;
using System.Collections;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.AR;

public abstract class InvoiceEntry_ApprovalWorkflow<TInvoiceEntry, TWorkflowExtension, TConditionPack> : 
  PXGraphExtension<TWorkflowExtension, TInvoiceEntry>
  where TInvoiceEntry : ARInvoiceEntry
  where TWorkflowExtension : PXGraphExtension<TInvoiceEntry>
  where TConditionPack : InvoiceEntry_ApprovalWorkflow<TInvoiceEntry, TWorkflowExtension, TConditionPack>.Conditions, new()
{
  public PXAction<ARInvoice> reassignApproval;

  protected static void ConfigureBase(
    PXScreenConfiguration config,
    Func<WorkflowContext<TInvoiceEntry, ARInvoice>, BoundedTo<TInvoiceEntry, ARInvoice>.ActionCategory.IConfigured> approvalCategorySelector)
  {
    WorkflowContext<TInvoiceEntry, ARInvoice> configurationContext = config.GetScreenConfigurationContext<TInvoiceEntry, ARInvoice>();
    InvoiceEntry_ApprovalWorkflow<TInvoiceEntry, TWorkflowExtension, TConditionPack>.ConfigureBase(configurationContext, configurationContext.Conditions.GetPack<TConditionPack>(), approvalCategorySelector(configurationContext));
  }

  private static void ConfigureBase(
    WorkflowContext<TInvoiceEntry, ARInvoice> context,
    TConditionPack conditions,
    BoundedTo<TInvoiceEntry, ARInvoice>.ActionCategory.IConfigured approvalCategory)
  {
    context.UpdateScreenConfigurationFor((Func<BoundedTo<TInvoiceEntry, ARInvoice>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<TInvoiceEntry, ARInvoice>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.UpdateDefaultFlow((Func<BoundedTo<TInvoiceEntry, ARInvoice>.Workflow.ConfiguratorFlow, BoundedTo<TInvoiceEntry, ARInvoice>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<TInvoiceEntry, ARInvoice>.BaseFlowStep.ContainerAdjusterStates>) (states => states.UpdateSequence<ARDocStatus.HoldToBalance>((Func<BoundedTo<TInvoiceEntry, ARInvoice>.Sequence.ConfiguratorSequence, BoundedTo<TInvoiceEntry, ARInvoice>.Sequence.ConfiguratorSequence>) (seq => seq.WithStates((Action<BoundedTo<TInvoiceEntry, ARInvoice>.Sequence.ContainerAdjusterSequenceStates>) (sss =>
    {
      ((BoundedTo<TInvoiceEntry, ARInvoice>.BaseFlowStep.ContainerAdjusterStates) sss).Add<ARDocStatus.pendingApproval>((Func<BoundedTo<TInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured>) (flowState =>
      {
        BoundedTo<TInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig anyFlowStateConfig = flowState;
        BoundedTo<TInvoiceEntry, ARInvoice>.Condition isApproved = conditions.IsApproved;
        BoundedTo<TInvoiceEntry, ARInvoice>.Condition condition3 = BoundedTo<TInvoiceEntry, ARInvoice>.Condition.op_True(isApproved) ? isApproved : BoundedTo<TInvoiceEntry, ARInvoice>.Condition.op_BitwiseOr(isApproved, conditions.IsRejected);
        BoundedTo<TInvoiceEntry, ARInvoice>.Condition condition4 = BoundedTo<TInvoiceEntry, ARInvoice>.Condition.op_True(condition3) ? condition3 : BoundedTo<TInvoiceEntry, ARInvoice>.Condition.op_BitwiseOr(condition3, conditions.SkipApproval);
        return (BoundedTo<TInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<TInvoiceEntry, ARInvoice>.BaseFlowStep.INeedAnyConfigState) ((BoundedTo<TInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig) anyFlowStateConfig.IsSkippedWhen(condition4)).WithActions((Action<BoundedTo<TInvoiceEntry, ARInvoice>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<TInvoiceEntry, PXAction<ARInvoice>>>) (g => g.approve), (Func<BoundedTo<TInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<TInvoiceEntry, ARInvoice>.ActionState.IConfigured>) (a => (BoundedTo<TInvoiceEntry, ARInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<TInvoiceEntry, PXAction<ARInvoice>>>) (g => g.reject), (Func<BoundedTo<TInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<TInvoiceEntry, ARInvoice>.ActionState.IConfigured>) (a => (BoundedTo<TInvoiceEntry, ARInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add("ReassignApproval", (Func<BoundedTo<TInvoiceEntry, ARInvoice>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<TInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<TInvoiceEntry, PXAction<ARInvoice>>>) (g => g.putOnHold), (Func<BoundedTo<TInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<TInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        }))).PlaceAfter<ARDocStatus.hold>();
      }));
      ((BoundedTo<TInvoiceEntry, ARInvoice>.BaseFlowStep.ContainerAdjusterStates) sss).Add<ARDocStatus.rejected>((Func<BoundedTo<TInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<TInvoiceEntry, ARInvoice>.BaseFlowStep.IConfigured) ((BoundedTo<TInvoiceEntry, ARInvoice>.BaseFlowStep.INeedAnyConfigState) ((BoundedTo<TInvoiceEntry, ARInvoice>.FlowState.INeedAnyFlowStateConfig) flowState.IsSkippedWhen(BoundedTo<TInvoiceEntry, ARInvoice>.Condition.op_LogicalNot(conditions.IsRejected))).WithActions((Action<BoundedTo<TInvoiceEntry, ARInvoice>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<TInvoiceEntry, PXAction<ARInvoice>>>) (g => g.putOnHold), (Func<BoundedTo<TInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<TInvoiceEntry, ARInvoice>.ActionState.IConfigured>) (a => (BoundedTo<TInvoiceEntry, ARInvoice>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<TInvoiceEntry, PXAction<ARInvoice>>>) (g => g.printAREdit), (Func<BoundedTo<TInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<TInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<TInvoiceEntry, PXAction<ARInvoice>>>) (g => g.customerDocuments), (Func<BoundedTo<TInvoiceEntry, ARInvoice>.ActionState.IAllowOptionalConfig, BoundedTo<TInvoiceEntry, ARInvoice>.ActionState.IConfigured>) null);
      }))).PlaceAfter<ARDocStatus.pendingApproval>()));
    })))))).WithTransitions((Action<BoundedTo<TInvoiceEntry, ARInvoice>.Transition.ContainerAdjusterTransitions>) (transitions =>
    {
      transitions.AddGroupFrom<ARDocStatus.pendingApproval>((Action<BoundedTo<TInvoiceEntry, ARInvoice>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<TInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<TInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<TInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.HoldToBalance>().IsTriggeredOn((Expression<Func<TInvoiceEntry, PXWorkflowEventHandlerBase<ARInvoice>>>) (g => g.OnUpdateStatus))));
        ts.Add((Func<BoundedTo<TInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<TInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<TInvoiceEntry, ARInvoice>.Transition.IConfigured) t.ToNext().IsTriggeredOn((Expression<Func<TInvoiceEntry, PXAction<ARInvoice>>>) (g => g.approve)).When((BoundedTo<TInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<TInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<TInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<TInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.rejected>().IsTriggeredOn((Expression<Func<TInvoiceEntry, PXAction<ARInvoice>>>) (g => g.reject)).When((BoundedTo<TInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsRejected)));
      }));
      transitions.AddGroupFrom<ARDocStatus.rejected>((Action<BoundedTo<TInvoiceEntry, ARInvoice>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<TInvoiceEntry, ARInvoice>.Transition.INeedTarget, BoundedTo<TInvoiceEntry, ARInvoice>.Transition.IConfigured>) (t => (BoundedTo<TInvoiceEntry, ARInvoice>.Transition.IConfigured) t.To<ARDocStatus.hold>().IsTriggeredOn((Expression<Func<TInvoiceEntry, PXAction<ARInvoice>>>) (g => g.putOnHold)).DoesNotPersist()))));
    })))).WithActions((Action<BoundedTo<TInvoiceEntry, ARInvoice>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add((Expression<Func<TInvoiceEntry, PXAction<ARInvoice>>>) (g => g.approve), (Func<BoundedTo<TInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (a => (BoundedTo<TInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter((Expression<Func<TInvoiceEntry, PXAction<ARInvoice>>>) (g => g.releaseFromHold)).WithPersistOptions((ActionPersistOptions) 2).IsHiddenWhen((BoundedTo<TInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<TInvoiceEntry, ARInvoice>.Assignment.IContainerFillerFields>) (fa => fa.Add<ARRegister.approved>(new bool?(true))))));
      actions.Add((Expression<Func<TInvoiceEntry, PXAction<ARInvoice>>>) (g => g.reject), (Func<BoundedTo<TInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (a => (BoundedTo<TInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory, (Expression<Func<TInvoiceEntry, PXAction<ARInvoice>>>) (g => g.approve)).PlaceAfter((Expression<Func<TInvoiceEntry, PXAction<ARInvoice>>>) (g => g.approve)).IsHiddenWhen((BoundedTo<TInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsApprovalDisabled).WithFieldAssignments((Action<BoundedTo<TInvoiceEntry, ARInvoice>.Assignment.IContainerFillerFields>) (fa => fa.Add<ARRegister.rejected>(new bool?(true))))));
      actions.Add("ReassignApproval", (Func<BoundedTo<TInvoiceEntry, ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured>) (a => (BoundedTo<TInvoiceEntry, ARInvoice>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter((Expression<Func<TInvoiceEntry, PXAction<ARInvoice>>>) (g => g.reject)).IsHiddenWhen((BoundedTo<TInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsApprovalDisabled)));
      actions.Update((Expression<Func<TInvoiceEntry, PXAction<ARInvoice>>>) (g => g.putOnHold), (Func<BoundedTo<TInvoiceEntry, ARInvoice>.ActionDefinition.ConfiguratorAction, BoundedTo<TInvoiceEntry, ARInvoice>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((Action<BoundedTo<TInvoiceEntry, ARInvoice>.Assignment.FieldContainerAdjusterAssignment>) (fas =>
      {
        fas.Add<ARRegister.approved>(new bool?(false));
        fas.Add<ARRegister.rejected>(new bool?(false));
      }))));
      actions.Update((Expression<Func<TInvoiceEntry, PXAction<ARInvoice>>>) (g => g.releaseFromCreditHold), (Func<BoundedTo<TInvoiceEntry, ARInvoice>.ActionDefinition.ConfiguratorAction, BoundedTo<TInvoiceEntry, ARInvoice>.ActionDefinition.ConfiguratorAction>) (a => a.PlaceAfterInCategory((Expression<Func<TInvoiceEntry, PXAction<ARInvoice>>>) (g => g.reject))));
      actions.Update((Expression<Func<TInvoiceEntry, PXAction<ARInvoice>>>) (g => g.recalculateDiscountsAction), (Func<BoundedTo<TInvoiceEntry, ARInvoice>.ActionDefinition.ConfiguratorAction, BoundedTo<TInvoiceEntry, ARInvoice>.ActionDefinition.ConfiguratorAction>) (a => a.IsDisabledWhenElse((BoundedTo<TInvoiceEntry, ARInvoice>.ISharedCondition) conditions.NonEditable)));
      actions.Update((Expression<Func<TInvoiceEntry, PXAction<ARInvoice>>>) (g => g.printInvoice), (Func<BoundedTo<TInvoiceEntry, ARInvoice>.ActionDefinition.ConfiguratorAction, BoundedTo<TInvoiceEntry, ARInvoice>.ActionDefinition.ConfiguratorAction>) (a => a.IsDisabledWhen((BoundedTo<TInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsRejected)));
      actions.Update((Expression<Func<TInvoiceEntry, PXAction<ARInvoice>>>) (g => g.sendEmail), (Func<BoundedTo<TInvoiceEntry, ARInvoice>.ActionDefinition.ConfiguratorAction, BoundedTo<TInvoiceEntry, ARInvoice>.ActionDefinition.ConfiguratorAction>) (a => a.IsDisabledWhen((BoundedTo<TInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsRejected)));
      actions.Update((Expression<Func<TInvoiceEntry, PXAction<ARInvoice>>>) (g => g.validateAddresses), (Func<BoundedTo<TInvoiceEntry, ARInvoice>.ActionDefinition.ConfiguratorAction, BoundedTo<TInvoiceEntry, ARInvoice>.ActionDefinition.ConfiguratorAction>) (a => a.IsDisabledWhen((BoundedTo<TInvoiceEntry, ARInvoice>.ISharedCondition) conditions.IsRejected)));
    }))));
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReassignApproval(PXAdapter adapter)
  {
    return ((PXGraphExtension<TInvoiceEntry>) this).Base.Approval.ReassignApproval(adapter);
  }

  public abstract class Conditions : BoundedTo<TInvoiceEntry, ARInvoice>.Condition.Pack
  {
    public virtual BoundedTo<TInvoiceEntry, ARInvoice>.Condition IsApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<TInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<TInvoiceEntry, ARInvoice>.Condition>) (b => b.FromBql<BqlOperand<ARRegister.approved, IBqlBool>.IsEqual<True>>()), nameof (IsApproved));
      }
    }

    public virtual BoundedTo<TInvoiceEntry, ARInvoice>.Condition IsRejected
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<TInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<TInvoiceEntry, ARInvoice>.Condition>) (b => b.FromBql<BqlOperand<ARRegister.rejected, IBqlBool>.IsEqual<True>>()), nameof (IsRejected));
      }
    }

    public virtual BoundedTo<TInvoiceEntry, ARInvoice>.Condition SkipApproval
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<TInvoiceEntry, ARInvoice>.Condition.ConditionBuilder, BoundedTo<TInvoiceEntry, ARInvoice>.Condition>) (b => b.FromBql<BqlOperand<ARRegister.dontApprove, IBqlBool>.IsEqual<True>>()), nameof (SkipApproval));
      }
    }

    public abstract BoundedTo<TInvoiceEntry, ARInvoice>.Condition IsApprovalDisabled { get; }

    public abstract BoundedTo<TInvoiceEntry, ARInvoice>.Condition NonEditable { get; }
  }
}
