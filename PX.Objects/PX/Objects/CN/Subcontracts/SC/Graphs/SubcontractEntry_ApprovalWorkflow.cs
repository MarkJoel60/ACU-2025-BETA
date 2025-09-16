// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.SC.Graphs.SubcontractEntry_ApprovalWorkflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.CS;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CN.Subcontracts.SC.Graphs;

public class SubcontractEntry_ApprovalWorkflow : 
  PXGraphExtension<SubcontractEntry_Workflow, SubcontractEntry>
{
  public const string ApproveActionName = "Approve";
  public const string RejectActionName = "Reject";
  public PXAction<POOrder> reassignApproval;

  private static bool ApprovalIsActive
  {
    get
    {
      return PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() && SubcontractSetupApproval.IsActive;
    }
  }

  [PXWorkflowDependsOnType(new Type[] {typeof (POSetup), typeof (POSetupApproval)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    if (SubcontractEntry_ApprovalWorkflow.ApprovalIsActive)
      SubcontractEntry_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<SubcontractEntry, POOrder>());
    else
      SubcontractEntry_ApprovalWorkflow.HideApproveAndRejectActions(config.GetScreenConfigurationContext<SubcontractEntry, POOrder>());
  }

  protected static void HideApproveAndRejectActions(
    WorkflowContext<SubcontractEntry, POOrder> context)
  {
    BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IExtendedConfigured approve = context.ActionDefinitions.CreateNew("Approve", (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IExtendedConfigured>) (a => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IExtendedConfigured) a.InFolder(context.Categories.Get("Approval"), (Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.putOnHold)).PlaceAfter((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.putOnHold)).IsHiddenAlways()));
    BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IExtendedConfigured reject = context.ActionDefinitions.CreateNew("Reject", (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IExtendedConfigured>) (a => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IExtendedConfigured) a.InFolder(context.Categories.Get("Approval"), (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) approve).PlaceAfter((BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) approve).IsHiddenAlways()));
    BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured reassign = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (a => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) a.WithCategory(context.Categories.Get("Approval")).PlaceAfter((BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) reject).IsHiddenAlways()));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<SubcontractEntry, POOrder>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<SubcontractEntry, POOrder>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((Action<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add((BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) approve);
      actions.Add((BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) reject);
      actions.Add(reassign);
    }))));
  }

  protected static void Configure(WorkflowContext<SubcontractEntry, POOrder> context)
  {
    SubcontractEntry_ApprovalWorkflow.Conditions conditions = context.Conditions.GetPack<SubcontractEntry_ApprovalWorkflow.Conditions>();
    SubcontractEntry_Workflow.Conditions scConditions = context.Conditions.GetPack<SubcontractEntry_Workflow.Conditions>();
    BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IExtendedConfigured approve = context.ActionDefinitions.CreateNew("Approve", (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IExtendedConfigured>) (a => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IExtendedConfigured) a.InFolder(context.Categories.Get("Approval"), (Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.putOnHold)).PlaceAfter((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.putOnHold)).WithFieldAssignments((Action<BoundedTo<SubcontractEntry, POOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<POOrder.approved>(new bool?(true))))));
    BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IExtendedConfigured reject = context.ActionDefinitions.CreateNew("Reject", (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IExtendedConfigured>) (a => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IExtendedConfigured) a.InFolder(context.Categories.Get("Approval"), (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) approve).PlaceAfter((BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) approve).WithFieldAssignments((Action<BoundedTo<SubcontractEntry, POOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<POOrder.rejected>(new bool?(true))))));
    BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured reassign = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured>) (a => (BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) a.WithCategory(context.Categories.Get("Approval")).PlaceAfter((BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) reject)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<SubcontractEntry, POOrder>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<SubcontractEntry, POOrder>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithFlows((Action<BoundedTo<SubcontractEntry, POOrder>.Workflow.ContainerAdjusterFlows>) (flows => flows.Update<POOrderType.regularSubcontract>((Func<BoundedTo<SubcontractEntry, POOrder>.Workflow.ConfiguratorFlow, BoundedTo<SubcontractEntry, POOrder>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<SubcontractEntry, POOrder>.BaseFlowStep.ContainerAdjusterStates>) (states =>
    {
      states.Add<POOrderStatus.pendingApproval>((Func<BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SubcontractEntry, POOrder>.BaseFlowStep.IConfigured>) (state => (BoundedTo<SubcontractEntry, POOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<SubcontractEntry, POOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.putOnHold), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add(approve, (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured) ((BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig) c).IsDuplicatedInToolbar()));
        actions.Add(reject, (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured) ((BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig) c).IsDuplicatedInToolbar()));
        actions.Add(reassign, (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.vendorDetails), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.printSubcontract), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
      }))).WithFieldStates((Action<BoundedTo<SubcontractEntry, POOrder>.FieldState.IContainerFillerFields>) (fields =>
      {
        fields.AddAllFields<POOrder>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddField<POOrder.orderType>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POOrder.orderNbr>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POOrder.controlTotal>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POOrder.workgroupID>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POOrder.ownerID>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POOrder.printed>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POOrder.emailed>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POOrder.dontPrint>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POOrder.dontEmail>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddTable<POLine>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddField<POLine.cancelled>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.completed>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.promisedDate>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.closed>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddTable<POTax>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddAllFields<POShipAddress>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddAllFields<POShipContact>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddTable<PX.Objects.CM.Extensions.CurrencyInfo>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
      }))));
      states.Add<POOrderStatus.rejected>((Func<BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<SubcontractEntry, POOrder>.BaseFlowStep.IConfigured>) (state => (BoundedTo<SubcontractEntry, POOrder>.BaseFlowStep.IConfigured) ((BoundedTo<SubcontractEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<SubcontractEntry, POOrder>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.putOnHold), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.vendorDetails), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.printSubcontract), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<SubcontractEntry, POOrder>.ActionState.IConfigured>) null);
      }))).WithFieldStates((Action<BoundedTo<SubcontractEntry, POOrder>.FieldState.IContainerFillerFields>) (fields =>
      {
        fields.AddTable<POOrder>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddField<POOrder.orderType>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POOrder.orderNbr>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddTable<POLine>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddField<POLine.cancelled>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.completed>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.promisedDate>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddField<POLine.closed>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) null);
        fields.AddAllFields<PORemitAddress>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddAllFields<PORemitContact>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddAllFields<POShipAddress>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddAllFields<POShipContact>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddTable<POTax>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        fields.AddTable<PX.Objects.CM.Extensions.CurrencyInfo>((Func<BoundedTo<SubcontractEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<SubcontractEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
      }))));
    })).WithTransitions((Action<BoundedTo<SubcontractEntry, POOrder>.Transition.ContainerAdjusterTransitions>) (transitions =>
    {
      transitions.UpdateGroupFrom("_", (Action<BoundedTo<SubcontractEntry, POOrder>.Transition.SourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.rejected>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.initializeState)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.IsRejected).PlaceBefore((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.ITransitionSelector, BoundedTo<SubcontractEntry, POOrder>.Transition.ITransitionSelector>) (tr => tr.To<POOrderStatus.pendingPrint>()))));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.pendingApproval>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.initializeState)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(conditions.IsApproved)).PlaceAfter((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.ITransitionSelector, BoundedTo<SubcontractEntry, POOrder>.Transition.ITransitionSelector>) (tr => tr.To<POOrderStatus.rejected>()))));
      }));
      transitions.UpdateGroupFrom<POOrderStatus.hold>((Action<BoundedTo<SubcontractEntry, POOrder>.Transition.SourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t =>
        {
          BoundedTo<SubcontractEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.pendingApproval>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.releaseFromHold));
          BoundedTo<SubcontractEntry, POOrder>.Condition condition4 = BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(scConditions.IsOnHold);
          BoundedTo<SubcontractEntry, POOrder>.Condition condition5 = BoundedTo<SubcontractEntry, POOrder>.Condition.op_False(condition4) ? condition4 : BoundedTo<SubcontractEntry, POOrder>.Condition.op_BitwiseAnd(condition4, BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(conditions.IsApproved));
          BoundedTo<SubcontractEntry, POOrder>.Condition condition6 = BoundedTo<SubcontractEntry, POOrder>.Condition.op_False(condition5) ? condition5 : BoundedTo<SubcontractEntry, POOrder>.Condition.op_BitwiseAnd(condition5, BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(conditions.IsRejected));
          return (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) condition6).PlaceBefore((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.ITransitionSelector, BoundedTo<SubcontractEntry, POOrder>.Transition.ITransitionSelector>) (tr => tr.To<POOrderStatus.pendingPrint>()));
        }));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.rejected>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.releaseFromHold)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.IsRejected)));
        ts.Update((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.pendingPrint>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.releaseFromHold))), (Func<BoundedTo<SubcontractEntry, POOrder>.Transition.ConfiguratorTransition, BoundedTo<SubcontractEntry, POOrder>.Transition.ConfiguratorTransition>) (t =>
        {
          BoundedTo<SubcontractEntry, POOrder>.Transition.ConfiguratorTransition configuratorTransition = t;
          BoundedTo<SubcontractEntry, POOrder>.Condition isApproved = conditions.IsApproved;
          BoundedTo<SubcontractEntry, POOrder>.Condition condition = BoundedTo<SubcontractEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<SubcontractEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(scConditions.IsPrinted));
          return configuratorTransition.When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) condition);
        }));
        ts.Update((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.pendingEmail>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.releaseFromHold))), (Func<BoundedTo<SubcontractEntry, POOrder>.Transition.ConfiguratorTransition, BoundedTo<SubcontractEntry, POOrder>.Transition.ConfiguratorTransition>) (t =>
        {
          BoundedTo<SubcontractEntry, POOrder>.Transition.ConfiguratorTransition configuratorTransition = t;
          BoundedTo<SubcontractEntry, POOrder>.Condition isApproved = conditions.IsApproved;
          BoundedTo<SubcontractEntry, POOrder>.Condition condition = BoundedTo<SubcontractEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<SubcontractEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(scConditions.IsEmailed));
          return configuratorTransition.When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) condition);
        }));
        ts.Update((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.closed>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.releaseFromHold))), (Func<BoundedTo<SubcontractEntry, POOrder>.Transition.ConfiguratorTransition, BoundedTo<SubcontractEntry, POOrder>.Transition.ConfiguratorTransition>) (t =>
        {
          BoundedTo<SubcontractEntry, POOrder>.Transition.ConfiguratorTransition configuratorTransition = t;
          BoundedTo<SubcontractEntry, POOrder>.Condition isApproved = conditions.IsApproved;
          BoundedTo<SubcontractEntry, POOrder>.Condition condition = BoundedTo<SubcontractEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<SubcontractEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, scConditions.HasAllLinesClosed);
          return configuratorTransition.When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) condition);
        }));
        ts.Update((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.completed>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.releaseFromHold))), (Func<BoundedTo<SubcontractEntry, POOrder>.Transition.ConfiguratorTransition, BoundedTo<SubcontractEntry, POOrder>.Transition.ConfiguratorTransition>) (t =>
        {
          BoundedTo<SubcontractEntry, POOrder>.Transition.ConfiguratorTransition configuratorTransition = t;
          BoundedTo<SubcontractEntry, POOrder>.Condition isApproved = conditions.IsApproved;
          BoundedTo<SubcontractEntry, POOrder>.Condition condition = BoundedTo<SubcontractEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<SubcontractEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, scConditions.HasAllLinesCompleted);
          return configuratorTransition.When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) condition);
        }));
        ts.Update((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.open>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.releaseFromHold))), (Func<BoundedTo<SubcontractEntry, POOrder>.Transition.ConfiguratorTransition, BoundedTo<SubcontractEntry, POOrder>.Transition.ConfiguratorTransition>) (t => t.When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.IsApproved)));
      }));
      transitions.AddGroupFrom<POOrderStatus.pendingApproval>((Action<BoundedTo<SubcontractEntry, POOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t =>
        {
          BoundedTo<SubcontractEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.pendingPrint>().IsTriggeredOn((BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) approve);
          BoundedTo<SubcontractEntry, POOrder>.Condition isApproved = conditions.IsApproved;
          BoundedTo<SubcontractEntry, POOrder>.Condition condition = BoundedTo<SubcontractEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<SubcontractEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(scConditions.IsPrinted));
          return (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) condition);
        }));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t =>
        {
          BoundedTo<SubcontractEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.pendingEmail>().IsTriggeredOn((BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) approve);
          BoundedTo<SubcontractEntry, POOrder>.Condition isApproved = conditions.IsApproved;
          BoundedTo<SubcontractEntry, POOrder>.Condition condition = BoundedTo<SubcontractEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<SubcontractEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, BoundedTo<SubcontractEntry, POOrder>.Condition.op_LogicalNot(scConditions.IsEmailed));
          return (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) condition);
        }));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t =>
        {
          BoundedTo<SubcontractEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.closed>().IsTriggeredOn((BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) approve);
          BoundedTo<SubcontractEntry, POOrder>.Condition isApproved = conditions.IsApproved;
          BoundedTo<SubcontractEntry, POOrder>.Condition condition = BoundedTo<SubcontractEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<SubcontractEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, scConditions.HasAllLinesClosed);
          return (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) condition);
        }));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t =>
        {
          BoundedTo<SubcontractEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.completed>().IsTriggeredOn((BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) approve);
          BoundedTo<SubcontractEntry, POOrder>.Condition isApproved = conditions.IsApproved;
          BoundedTo<SubcontractEntry, POOrder>.Condition condition = BoundedTo<SubcontractEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<SubcontractEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, scConditions.HasAllLinesCompleted);
          return (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) condition);
        }));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.open>().IsTriggeredOn((BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) approve).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.IsApproved)));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.rejected>().IsTriggeredOn((BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) reject).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) conditions.IsRejected)));
        ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.hold>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.putOnHold)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) scConditions.IsOnHold)));
      }));
      transitions.AddGroupFrom<POOrderStatus.rejected>((Action<BoundedTo<SubcontractEntry, POOrder>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<SubcontractEntry, POOrder>.Transition.INeedTarget, BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<SubcontractEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.hold>().IsTriggeredOn((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.putOnHold)).When((BoundedTo<SubcontractEntry, POOrder>.ISharedCondition) scConditions.IsOnHold)))));
    })))))).WithActions((Action<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add((BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) approve);
      actions.Add((BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.IConfigured) reject);
      actions.Add(reassign);
      actions.Update((Expression<Func<SubcontractEntry, PXAction<POOrder>>>) (g => g.putOnHold), (Func<BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.ConfiguratorAction, BoundedTo<SubcontractEntry, POOrder>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((Action<BoundedTo<SubcontractEntry, POOrder>.Assignment.FieldContainerAdjusterAssignment>) (fas =>
      {
        fas.Add<POOrder.approved>(new bool?(false));
        fas.Add<POOrder.rejected>(new bool?(false));
      }))));
    }))));
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReassignApproval(PXAdapter adapter)
  {
    return ((PXGraphExtension<SubcontractEntry>) this).Base.Approval.ReassignApproval(adapter);
  }

  public class Conditions : BoundedTo<SubcontractEntry, POOrder>.Condition.Pack
  {
    public BoundedTo<SubcontractEntry, POOrder>.Condition IsApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SubcontractEntry, POOrder>.Condition.ConditionBuilder, BoundedTo<SubcontractEntry, POOrder>.Condition>) (b => b.FromBql<BqlOperand<POOrder.approved, IBqlBool>.IsEqual<True>>()), nameof (IsApproved));
      }
    }

    public BoundedTo<SubcontractEntry, POOrder>.Condition IsRejected
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<SubcontractEntry, POOrder>.Condition.ConditionBuilder, BoundedTo<SubcontractEntry, POOrder>.Condition>) (b => b.FromBql<BqlOperand<POOrder.rejected, IBqlBool>.IsEqual<True>>()), nameof (IsRejected));
      }
    }
  }
}
