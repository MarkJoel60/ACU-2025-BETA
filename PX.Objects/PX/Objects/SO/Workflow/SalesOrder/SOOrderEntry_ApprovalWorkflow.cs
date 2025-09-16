// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Workflow.SalesOrder.SOOrderEntry_ApprovalWorkflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.SO.GraphExtensions.SOOrderEntryExt;
using System;
using System.Collections;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.SO.Workflow.SalesOrder;

public class SOOrderEntry_ApprovalWorkflow : PXGraphExtension<SOOrderEntry_Workflow, SOOrderEntry>
{
  public PXAction<SOOrder> approve;
  public PXAction<SOOrder> reject;
  public PXAction<SOOrder> reassignApproval;

  public static bool ApprovalIsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() && SOOrderEntry_ApprovalWorkflow.SOSetupApproval.IsActive;
  }

  [PXWorkflowDependsOnType(new Type[] {typeof (SOSetup)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    if (SOOrderEntry_ApprovalWorkflow.ApprovalIsActive())
      SOOrderEntry_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<SOOrderEntry, SOOrder>());
    else
      SOOrderEntry_ApprovalWorkflow.HideApproveAndRejectActions(config.GetScreenConfigurationContext<SOOrderEntry, SOOrder>());
  }

  protected static void Configure(WorkflowContext<SOOrderEntry, SOOrder> context)
  {
    SOOrderEntry_ApprovalWorkflow.Conditions conditions = context.Conditions.GetPack<SOOrderEntry_ApprovalWorkflow.Conditions>();
    WorkflowSO.Conditions soConditions = context.Conditions.GetPack<WorkflowSO.Conditions>();
    BoundedTo<SOOrderEntry, SOOrder>.ActionCategory.IConfigured approvalCategory = CommonActionCategories.Get<SOOrderEntry, SOOrder>(context).Approval;
    BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IConfigured approve = context.ActionDefinitions.CreateExisting<SOOrderEntry_ApprovalWorkflow>((Expression<Func<SOOrderEntry_ApprovalWorkflow, PXAction<SOOrder>>>) (g => g.approve), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceInCategory((Placement) 2, (string) null).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fa => fa.Add<SOOrder.approved>(new bool?(true))))));
    BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IConfigured reject = context.ActionDefinitions.CreateExisting<SOOrderEntry_ApprovalWorkflow>((Expression<Func<SOOrderEntry_ApprovalWorkflow, PXAction<SOOrder>>>) (g => g.reject), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceInCategory((Placement) 2, (string) null).PlaceAfter(approve).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fa => fa.Add<SOOrder.rejected>(new bool?(true))))));
    BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IConfigured reassign = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceInCategory((Placement) 2, (string) null).PlaceAfter(reject)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<SOOrderEntry, SOOrder>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<SOOrderEntry, SOOrder>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithFlows((Action<BoundedTo<SOOrderEntry, SOOrder>.Workflow.ContainerAdjusterFlows>) (flows =>
    {
      flows.Update<SOBehavior.sO>((Func<BoundedTo<SOOrderEntry, SOOrder>.Workflow.ConfiguratorFlow, BoundedTo<SOOrderEntry, SOOrder>.Workflow.ConfiguratorFlow>) (f => InjectApprovalWorkflow(f, "SO")));
      flows.Update<SOBehavior.tR>((Func<BoundedTo<SOOrderEntry, SOOrder>.Workflow.ConfiguratorFlow, BoundedTo<SOOrderEntry, SOOrder>.Workflow.ConfiguratorFlow>) (f => InjectApprovalWorkflow(f, "TR")));
      flows.Update<SOBehavior.qT>((Func<BoundedTo<SOOrderEntry, SOOrder>.Workflow.ConfiguratorFlow, BoundedTo<SOOrderEntry, SOOrder>.Workflow.ConfiguratorFlow>) (f => InjectApprovalWorkflow(f, "QT")));
      flows.Update<SOBehavior.rM>((Func<BoundedTo<SOOrderEntry, SOOrder>.Workflow.ConfiguratorFlow, BoundedTo<SOOrderEntry, SOOrder>.Workflow.ConfiguratorFlow>) (f => InjectApprovalWorkflow(f, "RM")));
      flows.Update<SOBehavior.iN>((Func<BoundedTo<SOOrderEntry, SOOrder>.Workflow.ConfiguratorFlow, BoundedTo<SOOrderEntry, SOOrder>.Workflow.ConfiguratorFlow>) (f => InjectApprovalWorkflow(f, "IN")));
      flows.Update<SOBehavior.cM>((Func<BoundedTo<SOOrderEntry, SOOrder>.Workflow.ConfiguratorFlow, BoundedTo<SOOrderEntry, SOOrder>.Workflow.ConfiguratorFlow>) (f => InjectApprovalWorkflow(f, "CM")));
      flows.Update<SOBehavior.bL>((Func<BoundedTo<SOOrderEntry, SOOrder>.Workflow.ConfiguratorFlow, BoundedTo<SOOrderEntry, SOOrder>.Workflow.ConfiguratorFlow>) (f => InjectApprovalWorkflow(f, "BL")));
      flows.Update<SOBehavior.mO>((Func<BoundedTo<SOOrderEntry, SOOrder>.Workflow.ConfiguratorFlow, BoundedTo<SOOrderEntry, SOOrder>.Workflow.ConfiguratorFlow>) (f => InjectApprovalWorkflow(f, "MO")));
    })).WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approve);
      actions.Add(reject);
      actions.Add(reassign);
      actions.Update((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.ConfiguratorAction, BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.FieldContainerAdjusterAssignment>) (fas =>
      {
        fas.Add<SOOrder.approved>(new bool?(false));
        fas.Add<SOOrder.rejected>(new bool?(false));
      }))));
    }))));

    BoundedTo<SOOrderEntry, SOOrder>.Workflow.ConfiguratorFlow InjectApprovalWorkflow(
      BoundedTo<SOOrderEntry, SOOrder>.Workflow.ConfiguratorFlow flow,
      string behavior)
    {
      bool includeCreditHold = EnumerableExtensions.IsIn<string>(behavior, "SO", "IN", "RM", "MO");
      bool inclCustOpenOrders = EnumerableExtensions.IsIn<string>(behavior, "SO", "IN", "RM", "MO", "CM", Array.Empty<string>());
      return flow.WithFlowStates((Action<BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.ContainerAdjusterStates>) (states =>
      {
        states.Add<SOOrderStatus.pendingApproval>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add(approve, (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
          actions.Add(reject, (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add(reassign, (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
          switch (behavior)
          {
            case "QT":
              actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printQuote), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
              break;
            case "BL":
              actions.Add<Blanket>((Expression<Func<Blanket, PXAction<SOOrder>>>) (g => g.printBlanket), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
              break;
            default:
              actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
              break;
          }
        }))));
        states.Add<SOOrderStatus.voided>((Func<BoundedTo<SOOrderEntry, SOOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<SOOrderEntry, SOOrder>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          if (behavior == "QT")
          {
            actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.copyOrderQT), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printQuote), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
          }
          else
          {
            actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.copyOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
            if (behavior == "BL")
              actions.Add<Blanket>((Expression<Func<Blanket, PXAction<SOOrder>>>) (g => g.printBlanket), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
            else
              actions.Add((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.printSalesOrder), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SOOrderEntry, SOOrder>.ActionState.IConfigured>) null);
          }
        }))));
      })).WithTransitions((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ContainerAdjusterTransitions>) (transitions =>
      {
        transitions.UpdateGroupFrom("_", (Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.SourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.pendingApproval>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.initializeState)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) BoundedTo<SOOrderEntry, SOOrder>.Condition.op_LogicalNot(conditions.IsApproved)).PlaceAfter((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.ITransitionSelector, BoundedTo<SOOrderEntry, SOOrder>.Transition.ITransitionSelector>) (tr => tr.To<SOOrderStatus.hold>())).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          if (!inclCustOpenOrders)
            return;
          fas.Add<SOOrder.inclCustOpenOrders>(new bool?(false));
        }))))));
        transitions.UpdateGroupFrom<SOOrderStatus.hold>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.SourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.pendingApproval>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.releaseFromHold)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) BoundedTo<SOOrderEntry, SOOrder>.Condition.op_LogicalNot(conditions.IsApproved)).PlaceBefore((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.ITransitionSelector, BoundedTo<SOOrderEntry, SOOrder>.Transition.ITransitionSelector>) (tr => !EnumerableExtensions.IsIn<string>(behavior, "SO", "RM") ? tr.To<SOOrderStatus.open>() : tr.To<SOOrderStatus.pendingProcessing>()))))));
        transitions.UpdateGroupFrom<SOOrderStatus.cancelled>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.SourceContainerFillerTransitions>) (ts =>
        {
          if (EnumerableExtensions.IsIn<string>(behavior, "SO", "RM"))
          {
            ts.Update((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.pendingProcessing>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder))), (Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.ConfiguratorTransition, BoundedTo<SOOrderEntry, SOOrder>.Transition.ConfiguratorTransition>) (t =>
            {
              BoundedTo<SOOrderEntry, SOOrder>.Transition.ConfiguratorTransition configuratorTransition = t;
              BoundedTo<SOOrderEntry, SOOrder>.Condition isApproved = conditions.IsApproved;
              BoundedTo<SOOrderEntry, SOOrder>.Condition condition = BoundedTo<SOOrderEntry, SOOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<SOOrderEntry, SOOrder>.Condition.op_BitwiseAnd(isApproved, soConditions.HasPaymentsInPendingProcessing);
              return configuratorTransition.When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) condition);
            }));
            ts.Update((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.awaitingPayment>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder))), (Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.ConfiguratorTransition, BoundedTo<SOOrderEntry, SOOrder>.Transition.ConfiguratorTransition>) (t =>
            {
              BoundedTo<SOOrderEntry, SOOrder>.Transition.ConfiguratorTransition configuratorTransition = t;
              BoundedTo<SOOrderEntry, SOOrder>.Condition isApproved = conditions.IsApproved;
              BoundedTo<SOOrderEntry, SOOrder>.Condition condition = BoundedTo<SOOrderEntry, SOOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<SOOrderEntry, SOOrder>.Condition.op_BitwiseAnd(isApproved, soConditions.IsPaymentRequirementsViolated);
              return configuratorTransition.When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) condition);
            }));
          }
          ts.Update((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder))), (Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.ConfiguratorTransition, BoundedTo<SOOrderEntry, SOOrder>.Transition.ConfiguratorTransition>) (t =>
          {
            BoundedTo<SOOrderEntry, SOOrder>.Transition.ConfiguratorTransition configuratorTransition = t;
            BoundedTo<SOOrderEntry, SOOrder>.Condition isApproved = conditions.IsApproved;
            BoundedTo<SOOrderEntry, SOOrder>.Condition condition = BoundedTo<SOOrderEntry, SOOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<SOOrderEntry, SOOrder>.Condition.op_BitwiseAnd(isApproved, conditions.IsNotHoldEntryAndNotLSEntryEnabled);
            return configuratorTransition.When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) condition);
          }));
          ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t =>
          {
            BoundedTo<SOOrderEntry, SOOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<SOOrderStatus.pendingApproval>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.reopenOrder));
            BoundedTo<SOOrderEntry, SOOrder>.Condition condition3 = BoundedTo<SOOrderEntry, SOOrder>.Condition.op_LogicalNot(conditions.IsApproved);
            BoundedTo<SOOrderEntry, SOOrder>.Condition condition4 = BoundedTo<SOOrderEntry, SOOrder>.Condition.op_False(condition3) ? condition3 : BoundedTo<SOOrderEntry, SOOrder>.Condition.op_BitwiseAnd(condition3, conditions.IsNotHoldEntryAndNotLSEntryEnabled);
            return (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) condition4);
          }));
        }));
        if (includeCreditHold)
          transitions.UpdateGroupFrom<SOOrderStatus.creditHold>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.SourceContainerFillerTransitions>) (ts =>
          {
            ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.pendingApproval>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.releaseFromCreditHold)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) BoundedTo<SOOrderEntry, SOOrder>.Condition.op_LogicalNot(conditions.IsApproved)).PlaceBefore((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.ITransitionSelector, BoundedTo<SOOrderEntry, SOOrder>.Transition.ITransitionSelector>) (tr => tr.To<SOOrderStatus.open>())).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.creditHold>(new bool?(false))))));
            ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.pendingApproval>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXWorkflowEventHandlerBase<SOOrder>>>) (g => g.OnCreditLimitSatisfied)).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) BoundedTo<SOOrderEntry, SOOrder>.Condition.op_LogicalNot(conditions.IsApproved)).PlaceBefore((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.ITransitionSelector, BoundedTo<SOOrderEntry, SOOrder>.Transition.ITransitionSelector>) (tr => tr.To<SOOrderStatus.open>())).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<SOOrder.creditHold>(new bool?(false))))));
          }));
        transitions.AddGroupFrom<SOOrderStatus.pendingApproval>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          if (EnumerableExtensions.IsIn<string>(behavior, "SO", "RM"))
          {
            ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t =>
            {
              BoundedTo<SOOrderEntry, SOOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<SOOrderStatus.pendingProcessing>().IsTriggeredOn(approve);
              BoundedTo<SOOrderEntry, SOOrder>.Condition isApproved = conditions.IsApproved;
              BoundedTo<SOOrderEntry, SOOrder>.Condition condition = BoundedTo<SOOrderEntry, SOOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<SOOrderEntry, SOOrder>.Condition.op_BitwiseAnd(isApproved, soConditions.HasPaymentsInPendingProcessing);
              return (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) condition);
            }));
            ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t =>
            {
              BoundedTo<SOOrderEntry, SOOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<SOOrderStatus.awaitingPayment>().IsTriggeredOn(approve);
              BoundedTo<SOOrderEntry, SOOrder>.Condition isApproved = conditions.IsApproved;
              BoundedTo<SOOrderEntry, SOOrder>.Condition condition = BoundedTo<SOOrderEntry, SOOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<SOOrderEntry, SOOrder>.Condition.op_BitwiseAnd(isApproved, soConditions.IsPaymentRequirementsViolated);
              return (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) condition);
            }));
          }
          ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.open>().IsTriggeredOn(approve).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsApproved).WithFieldAssignments((Action<BoundedTo<SOOrderEntry, SOOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            if (!inclCustOpenOrders)
              return;
            fas.Add<SOOrder.inclCustOpenOrders>(new bool?(true));
          }))));
          ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.voided>().IsTriggeredOn(reject).When((BoundedTo<SOOrderEntry, SOOrder>.ISharedCondition) conditions.IsRejected)));
          ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.hold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold))));
        }));
        transitions.AddGroupFrom<SOOrderStatus.voided>((Action<BoundedTo<SOOrderEntry, SOOrder>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<SOOrderEntry, SOOrder>.Transition.INeedTarget, BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured>) (t => (BoundedTo<SOOrderEntry, SOOrder>.Transition.IConfigured) t.To<SOOrderStatus.hold>().IsTriggeredOn((Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.putOnHold))))));
      }));
    }
  }

  protected static void HideApproveAndRejectActions(WorkflowContext<SOOrderEntry, SOOrder> context)
  {
    BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IConfigured approveHidden = context.ActionDefinitions.CreateExisting<SOOrderEntry_ApprovalWorkflow>((Expression<Func<SOOrderEntry_ApprovalWorkflow, PXAction<SOOrder>>>) (g => g.approve), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 0).IsHiddenAlways()));
    BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IConfigured rejectHidden = context.ActionDefinitions.CreateExisting<SOOrderEntry_ApprovalWorkflow>((Expression<Func<SOOrderEntry_ApprovalWorkflow, PXAction<SOOrder>>>) (g => g.reject), (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 0).IsHiddenAlways()));
    BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IConfigured reassignHidden = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 0).IsHiddenAlways()));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<SOOrderEntry, SOOrder>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<SOOrderEntry, SOOrder>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((Action<BoundedTo<SOOrderEntry, SOOrder>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add(approveHidden);
      actions.Add(rejectHidden);
      actions.Add(reassignHidden);
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
    return ((PXGraphExtension<SOOrderEntry>) this).Base.Approval.ReassignApproval(adapter);
  }

  private class SOSetupApproval : IPrefetchable, IPXCompanyDependent
  {
    private bool OrderRequestApproval;

    public static bool IsActive
    {
      get
      {
        return PXDatabase.GetSlot<SOOrderEntry_ApprovalWorkflow.SOSetupApproval>(nameof (SOSetupApproval), new Type[1]
        {
          typeof (SOSetup)
        }).OrderRequestApproval;
      }
    }

    void IPrefetchable.Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<SOSetup>(new PXDataField[1]
      {
        (PXDataField) new PXDataField<SOSetup.orderRequestApproval>()
      }))
      {
        if (pxDataRecord == null)
          return;
        this.OrderRequestApproval = pxDataRecord.GetBoolean(0).Value;
      }
    }
  }

  public class Conditions : BoundedTo<SOOrderEntry, SOOrder>.Condition.Pack
  {
    public BoundedTo<SOOrderEntry, SOOrder>.Condition IsApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOOrderEntry, SOOrder>.Condition.ConditionBuilder, BoundedTo<SOOrderEntry, SOOrder>.Condition>) (b => b.FromBql<BqlOperand<SOOrder.approved, IBqlBool>.IsEqual<True>>()), nameof (IsApproved));
      }
    }

    public BoundedTo<SOOrderEntry, SOOrder>.Condition IsRejected
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOOrderEntry, SOOrder>.Condition.ConditionBuilder, BoundedTo<SOOrderEntry, SOOrder>.Condition>) (b => b.FromBql<BqlOperand<SOOrder.rejected, IBqlBool>.IsEqual<True>>()), nameof (IsRejected));
      }
    }

    public BoundedTo<SOOrderEntry, SOOrder>.Condition IsNotHoldEntryAndNotLSEntryEnabled
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SOOrderEntry, SOOrder>.Condition.ConditionBuilder, BoundedTo<SOOrderEntry, SOOrder>.Condition>) (b => b.FromBql<Where<BqlField<SOOrderType.holdEntry, IBqlBool>.FromCurrent, Equal<False>, And<BqlField<SOOrderType.requireLocation, IBqlBool>.FromCurrent, Equal<False>, And<BqlField<SOOrderType.requireLotSerial, IBqlBool>.FromCurrent, Equal<False>>>>>()), nameof (IsNotHoldEntryAndNotLSEntryEnabled));
      }
    }
  }
}
