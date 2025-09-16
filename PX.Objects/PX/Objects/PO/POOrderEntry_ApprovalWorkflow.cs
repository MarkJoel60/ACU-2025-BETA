// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POOrderEntry_ApprovalWorkflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.CS;
using PX.Objects.PO.GraphExtensions.POOrderEntryExt;
using System;
using System.Collections;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.PO;

public class POOrderEntry_ApprovalWorkflow : PXGraphExtension<POOrderEntry_Workflow, POOrderEntry>
{
  public const string ApproveActionName = "Approve";
  public const string RejectActionName = "Reject";
  public PXAction<POOrder> reassignApproval;

  private static bool ApprovalIsActive
  {
    get
    {
      return PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() && POOrderEntry_ApprovalWorkflow.POApprovalSettings.IsActive;
    }
  }

  protected static bool IsAnyApprovalActive()
  {
    return POOrderEntry_ApprovalWorkflow.IsBlanketApprovalActive() || POOrderEntry_ApprovalWorkflow.IsDropShipApprovalActive() || POOrderEntry_ApprovalWorkflow.IsNormalApprovalActive() || POOrderEntry_ApprovalWorkflow.IsStandartApprovalActive() || POOrderEntry_ApprovalWorkflow.IsProjectDropShipApprovalActive();
  }

  protected static bool IsBlanketApprovalActive()
  {
    return POOrderEntry_ApprovalWorkflow.ApprovalIsActive && POOrderEntry_ApprovalWorkflow.POApprovalSettings.IsActiveForBlanket;
  }

  protected static bool IsDropShipApprovalActive()
  {
    return POOrderEntry_ApprovalWorkflow.ApprovalIsActive && POOrderEntry_ApprovalWorkflow.POApprovalSettings.IsActiveForDropShip;
  }

  protected static bool IsNormalApprovalActive()
  {
    return POOrderEntry_ApprovalWorkflow.ApprovalIsActive && POOrderEntry_ApprovalWorkflow.POApprovalSettings.IsActiveForNormal;
  }

  protected static bool IsStandartApprovalActive()
  {
    return POOrderEntry_ApprovalWorkflow.ApprovalIsActive && POOrderEntry_ApprovalWorkflow.POApprovalSettings.IsActiveForStandart;
  }

  protected static bool IsProjectDropShipApprovalActive()
  {
    return POOrderEntry_ApprovalWorkflow.ApprovalIsActive && POOrderEntry_ApprovalWorkflow.POApprovalSettings.IsActiveForProjectDropShip;
  }

  [PXWorkflowDependsOnType(new Type[] {typeof (POSetup), typeof (POSetupApproval)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    if (POOrderEntry_ApprovalWorkflow.IsAnyApprovalActive())
      POOrderEntry_ApprovalWorkflow.Configure(config.GetScreenConfigurationContext<POOrderEntry, POOrder>());
    else
      POOrderEntry_ApprovalWorkflow.HideApproveAndRejectActions(config.GetScreenConfigurationContext<POOrderEntry, POOrder>());
  }

  protected static void HideApproveAndRejectActions(WorkflowContext<POOrderEntry, POOrder> context)
  {
    BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IExtendedConfigured approve = context.ActionDefinitions.CreateNew("Approve", (Func<BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IExtendedConfigured>) (a => (BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IExtendedConfigured) a.WithCategory((PredefinedCategory) 0).PlaceAfter((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold)).IsHiddenAlways()));
    BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IExtendedConfigured reject = context.ActionDefinitions.CreateNew("Reject", (Func<BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IExtendedConfigured>) (a => (BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IExtendedConfigured) a.WithCategory((PredefinedCategory) 0).PlaceAfter((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve).IsHiddenAlways()));
    BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured reassign = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured>) (a => (BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 0).PlaceAfter((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) reject).IsHiddenAlways()));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<POOrderEntry, POOrder>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<POOrderEntry, POOrder>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((Action<BoundedTo<POOrderEntry, POOrder>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve);
      actions.Add((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) reject);
      actions.Add(reassign);
    }))));
  }

  protected static void Configure(WorkflowContext<POOrderEntry, POOrder> context)
  {
    var conditions = new
    {
      IsApproved = Bql<BqlOperand<POOrder.approved, IBqlBool>.IsEqual<True>>(),
      IsRejected = Bql<BqlOperand<POOrder.rejected, IBqlBool>.IsEqual<True>>(),
      IsOnHold = Existing("IsOnHold"),
      IsPrinted = Existing("IsPrinted"),
      IsEmailed = Existing("IsEmailed"),
      HasAllLinesClosed = Existing("HasAllLinesClosed"),
      HasAllLinesCompleted = Existing("HasAllLinesCompleted"),
      HasAllDropShipLinesLinked = Existing("HasAllDropShipLinesLinked")
    }.AutoNameConditions();
    BoundedTo<POOrderEntry, POOrder>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("Approval Category", (Func<BoundedTo<POOrderEntry, POOrder>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<POOrderEntry, POOrder>.ActionCategory.IConfigured>) (category => (BoundedTo<POOrderEntry, POOrder>.ActionCategory.IConfigured) category.DisplayName("Approval").PlaceAfter("Intercompany Category")));
    BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IExtendedConfigured approve = context.ActionDefinitions.CreateNew("Approve", (Func<BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IExtendedConfigured>) (a => (BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IExtendedConfigured) a.WithCategory(approvalCategory).PlaceAfter((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold)).MapEnableToSelect().WithFieldAssignments((Action<BoundedTo<POOrderEntry, POOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<POOrder.approved>(new bool?(true))))));
    BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IExtendedConfigured reject = context.ActionDefinitions.CreateNew("Reject", (Func<BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IExtendedConfigured>) (a => (BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IExtendedConfigured) a.WithCategory(approvalCategory).PlaceAfter((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve).MapEnableToSelect().WithFieldAssignments((Action<BoundedTo<POOrderEntry, POOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<POOrder.rejected>(new bool?(true))))));
    BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured reassign = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured>) (a => (BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) a.WithCategory(approvalCategory).PlaceAfter((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) reject).MapEnableToSelect()));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<POOrderEntry, POOrder>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<POOrderEntry, POOrder>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithFlows((Action<BoundedTo<POOrderEntry, POOrder>.Workflow.ContainerAdjusterFlows>) (flows =>
    {
      if (POOrderEntry_ApprovalWorkflow.IsBlanketApprovalActive())
        flows.Update<POOrderType.blanket>((Func<BoundedTo<POOrderEntry, POOrder>.Workflow.ConfiguratorFlow, BoundedTo<POOrderEntry, POOrder>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.ContainerAdjusterStates>) (states =>
        {
          states.Add<POOrderStatus.pendingApproval>((Func<BoundedTo<POOrderEntry, POOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.IConfigured>) (state => (BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<POOrderEntry, POOrder>.ActionState.IContainerFillerActions>) (actions =>
          {
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add(approve, (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig) c).IsDuplicatedInToolbar()));
            actions.Add(reject, (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig) c).IsDuplicatedInToolbar()));
            actions.Add(reassign, (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.vendorDetails), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.printPurchaseOrder), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
          }))).WithFieldStates((Action<BoundedTo<POOrderEntry, POOrder>.FieldState.IContainerFillerFields>) (fields =>
          {
            fields.AddAllFields<POOrder>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddField<POOrder.orderType>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.orderNbr>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.controlTotal>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.workgroupID>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.ownerID>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.printed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddField<POOrder.emailed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddField<POOrder.dontPrint>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddField<POOrder.dontEmail>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddTable<POLine>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddField<POLine.cancelled>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POLine.completed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POLine.promisedDate>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POLine.closed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddFields<POOrderEntry_Workflow.DropShipOrderLineFields>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddTable<POTax>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddAllFields<POShipAddress>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddAllFields<POShipContact>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddTable<PX.Objects.CM.Extensions.CurrencyInfo>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
          }))));
          states.Add<POOrderStatus.rejected>((Func<BoundedTo<POOrderEntry, POOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.IConfigured>) (state => (BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<POOrderEntry, POOrder>.ActionState.IContainerFillerActions>) (actions =>
          {
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.vendorDetails), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.printPurchaseOrder), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
          }))).WithFieldStates((Action<BoundedTo<POOrderEntry, POOrder>.FieldState.IContainerFillerFields>) (fields =>
          {
            fields.AddTable<POOrder>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddField<POOrder.orderType>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.orderNbr>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.printed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddField<POOrder.emailed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddField<POOrder.dontPrint>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddField<POOrder.dontEmail>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddField<POLine.cancelled>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POLine.completed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POLine.promisedDate>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POLine.closed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddFields<POOrderEntry_Workflow.DropShipOrderLineFields>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddAllFields<PORemitAddress>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddAllFields<PORemitContact>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddAllFields<POShipAddress>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddAllFields<POShipContact>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddTable<POTax>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddTable<PX.Objects.CM.Extensions.CurrencyInfo>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
          }))));
        })).WithTransitions((Action<BoundedTo<POOrderEntry, POOrder>.Transition.ContainerAdjusterTransitions>) (transitions =>
        {
          transitions.UpdateGroupFrom("_", (Action<BoundedTo<POOrderEntry, POOrder>.Transition.SourceContainerFillerTransitions>) (ts =>
          {
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.rejected>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.initializeState)).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsRejected).PlaceBefore((Func<BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector, BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector>) (tr => tr.To<POOrderStatus.closed>()))));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.pendingApproval>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.initializeState)).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) BoundedTo<POOrderEntry, POOrder>.Condition.op_LogicalNot(conditions.IsApproved)).PlaceAfter((Func<BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector, BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector>) (tr => tr.To<POOrderStatus.rejected>()))));
          }));
          transitions.UpdateGroupFrom<POOrderStatus.hold>((Action<BoundedTo<POOrderEntry, POOrder>.Transition.SourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t =>
          {
            BoundedTo<POOrderEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.pendingApproval>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.releaseFromHold));
            BoundedTo<POOrderEntry, POOrder>.Condition condition3 = BoundedTo<POOrderEntry, POOrder>.Condition.op_LogicalNot(conditions.IsOnHold);
            BoundedTo<POOrderEntry, POOrder>.Condition condition4 = BoundedTo<POOrderEntry, POOrder>.Condition.op_False(condition3) ? condition3 : BoundedTo<POOrderEntry, POOrder>.Condition.op_BitwiseAnd(condition3, BoundedTo<POOrderEntry, POOrder>.Condition.op_LogicalNot(conditions.IsApproved));
            return (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) condition4).PlaceBefore((Func<BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector, BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector>) (tr => tr.To<POOrderStatus.closed>()));
          }))));
          transitions.AddGroupFrom<POOrderStatus.pendingApproval>((Action<BoundedTo<POOrderEntry, POOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
          {
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t =>
            {
              BoundedTo<POOrderEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.closed>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve);
              BoundedTo<POOrderEntry, POOrder>.Condition isApproved = conditions.IsApproved;
              BoundedTo<POOrderEntry, POOrder>.Condition condition = BoundedTo<POOrderEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<POOrderEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, conditions.HasAllLinesClosed);
              return (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) condition);
            }));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t =>
            {
              BoundedTo<POOrderEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.completed>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve);
              BoundedTo<POOrderEntry, POOrder>.Condition isApproved = conditions.IsApproved;
              BoundedTo<POOrderEntry, POOrder>.Condition condition = BoundedTo<POOrderEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<POOrderEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, conditions.HasAllLinesCompleted);
              return (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) condition);
            }));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.open>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsApproved)));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.rejected>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) reject).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsRejected)));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.hold>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold)).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsOnHold)));
          }));
          transitions.AddGroupFrom<POOrderStatus.rejected>((Action<BoundedTo<POOrderEntry, POOrder>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.hold>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold)).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsOnHold)))));
        }))));
      if (POOrderEntry_ApprovalWorkflow.IsStandartApprovalActive())
        flows.Update<POOrderType.standardBlanket>((Func<BoundedTo<POOrderEntry, POOrder>.Workflow.ConfiguratorFlow, BoundedTo<POOrderEntry, POOrder>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.ContainerAdjusterStates>) (states =>
        {
          states.Add<POOrderStatus.pendingApproval>((Func<BoundedTo<POOrderEntry, POOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.IConfigured>) (state => (BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<POOrderEntry, POOrder>.ActionState.IContainerFillerActions>) (actions =>
          {
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add(approve, (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig) c).IsDuplicatedInToolbar()));
            actions.Add(reject, (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig) c).IsDuplicatedInToolbar()));
            actions.Add(reassign, (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.vendorDetails), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.printPurchaseOrder), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.viewDemand), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
          }))).WithFieldStates((Action<BoundedTo<POOrderEntry, POOrder>.FieldState.IContainerFillerFields>) (fields =>
          {
            fields.AddAllFields<POOrder>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddField<POOrder.orderType>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.orderNbr>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.controlTotal>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.workgroupID>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.ownerID>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.printed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddField<POOrder.emailed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddField<POOrder.dontPrint>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddField<POOrder.dontEmail>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddTable<POLine>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddField<POLine.completed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (x => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField) x.IsDisabled()).IsHidden()));
            fields.AddField<POLine.closed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (x => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField) x.IsDisabled()).IsHidden()));
            fields.AddField<POLine.promisedDate>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddFields<POOrderEntry_Workflow.DropShipOrderLineFields>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddFields<POOrderEntry_Workflow.BlanketOrderLineFields>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddTable<POTax>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddAllFields<POShipAddress>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddAllFields<POShipContact>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddTable<PX.Objects.CM.Extensions.CurrencyInfo>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddTable<POOrderPrepayment>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
          }))));
          states.Add<POOrderStatus.rejected>((Func<BoundedTo<POOrderEntry, POOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.IConfigured>) (state => (BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<POOrderEntry, POOrder>.ActionState.IContainerFillerActions>) (actions =>
          {
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.vendorDetails), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.printPurchaseOrder), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.viewDemand), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
          }))).WithFieldStates((Action<BoundedTo<POOrderEntry, POOrder>.FieldState.IContainerFillerFields>) (fields =>
          {
            fields.AddTable<POOrder>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddField<POOrder.orderType>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.orderNbr>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.printed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddField<POOrder.emailed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddField<POOrder.dontPrint>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddField<POOrder.dontEmail>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddTable<POLine>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddField<POLine.completed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (x => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField) x.IsDisabled()).IsHidden()));
            fields.AddField<POLine.closed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (x => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField) x.IsDisabled()).IsHidden()));
            fields.AddField<POLine.promisedDate>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddFields<POOrderEntry_Workflow.DropShipOrderLineFields>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddFields<POOrderEntry_Workflow.BlanketOrderLineFields>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddAllFields<PORemitAddress>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddAllFields<PORemitContact>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddAllFields<POShipAddress>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddAllFields<POShipContact>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddTable<POTax>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddTable<PX.Objects.CM.Extensions.CurrencyInfo>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddTable<POOrderPrepayment>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
          }))));
        })).WithTransitions((Action<BoundedTo<POOrderEntry, POOrder>.Transition.ContainerAdjusterTransitions>) (transitions =>
        {
          transitions.UpdateGroupFrom("_", (Action<BoundedTo<POOrderEntry, POOrder>.Transition.SourceContainerFillerTransitions>) (ts =>
          {
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.rejected>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.initializeState)).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsRejected).PlaceBefore((Func<BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector, BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector>) (tr => tr.To<POOrderStatus.open>()))));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.pendingApproval>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.initializeState)).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) BoundedTo<POOrderEntry, POOrder>.Condition.op_LogicalNot(conditions.IsApproved)).PlaceAfter((Func<BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector, BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector>) (tr => tr.To<POOrderStatus.rejected>()))));
          }));
          transitions.UpdateGroupFrom<POOrderStatus.hold>((Action<BoundedTo<POOrderEntry, POOrder>.Transition.SourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t =>
          {
            BoundedTo<POOrderEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.pendingApproval>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.releaseFromHold));
            BoundedTo<POOrderEntry, POOrder>.Condition condition7 = BoundedTo<POOrderEntry, POOrder>.Condition.op_LogicalNot(conditions.IsOnHold);
            BoundedTo<POOrderEntry, POOrder>.Condition condition8 = BoundedTo<POOrderEntry, POOrder>.Condition.op_False(condition7) ? condition7 : BoundedTo<POOrderEntry, POOrder>.Condition.op_BitwiseAnd(condition7, BoundedTo<POOrderEntry, POOrder>.Condition.op_LogicalNot(conditions.IsApproved));
            return (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) condition8).PlaceBefore((Func<BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector, BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector>) (tr => tr.To<POOrderStatus.open>()));
          }))));
          transitions.AddGroupFrom<POOrderStatus.pendingApproval>((Action<BoundedTo<POOrderEntry, POOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
          {
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.open>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsApproved)));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.rejected>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) reject).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsRejected)));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.hold>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold)).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsOnHold)));
          }));
          transitions.AddGroupFrom<POOrderStatus.rejected>((Action<BoundedTo<POOrderEntry, POOrder>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.hold>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold)).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsOnHold)))));
        }))));
      if (POOrderEntry_ApprovalWorkflow.IsDropShipApprovalActive())
        flows.Update<POOrderType.dropShip>((Func<BoundedTo<POOrderEntry, POOrder>.Workflow.ConfiguratorFlow, BoundedTo<POOrderEntry, POOrder>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.ContainerAdjusterStates>) (states =>
        {
          states.Add<POOrderStatus.pendingApproval>((Func<BoundedTo<POOrderEntry, POOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.IConfigured>) (state => (BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<POOrderEntry, POOrder>.ActionState.IContainerFillerActions>) (actions =>
          {
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add(approve, (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig) c).IsDuplicatedInToolbar()));
            actions.Add(reject, (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig) c).IsDuplicatedInToolbar()));
            actions.Add(reassign, (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.vendorDetails), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.printPurchaseOrder), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add<DropShipLinksExt>((Expression<Func<DropShipLinksExt, PXAction<POOrder>>>) (g => g.unlinkFromSO), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.viewDemand), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
          }))).WithFieldStates((Action<BoundedTo<POOrderEntry, POOrder>.FieldState.IContainerFillerFields>) (fields =>
          {
            fields.AddAllFields<POOrder>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddField<POOrder.orderType>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.orderNbr>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.controlTotal>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.workgroupID>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.ownerID>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.printed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.emailed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.dontPrint>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.dontEmail>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddTable<POLine>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddField<POLine.cancelled>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POLine.completed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POLine.promisedDate>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POLine.closed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddFields<POOrderEntry_Workflow.BlanketOrderLineFields>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddTable<POTax>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddAllFields<POShipAddress>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddAllFields<POShipContact>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddTable<PX.Objects.CM.Extensions.CurrencyInfo>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
          }))));
          states.Add<POOrderStatus.rejected>((Func<BoundedTo<POOrderEntry, POOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.IConfigured>) (state => (BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<POOrderEntry, POOrder>.ActionState.IContainerFillerActions>) (actions =>
          {
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.vendorDetails), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.printPurchaseOrder), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.viewPurchaseOrderReceipt), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add<DropShipLinksExt>((Expression<Func<DropShipLinksExt, PXAction<POOrder>>>) (g => g.unlinkFromSO), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.viewDemand), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
          }))).WithFieldStates((Action<BoundedTo<POOrderEntry, POOrder>.FieldState.IContainerFillerFields>) (fields =>
          {
            fields.AddTable<POOrder>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddField<POOrder.orderType>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.orderNbr>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddTable<POLine>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddField<POLine.cancelled>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POLine.completed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POLine.promisedDate>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POLine.closed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddFields<POOrderEntry_Workflow.BlanketOrderLineFields>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddAllFields<PORemitAddress>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddAllFields<PORemitContact>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddAllFields<POShipAddress>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddAllFields<POShipContact>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddTable<POTax>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddTable<PX.Objects.CM.Extensions.CurrencyInfo>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
          }))));
        })).WithTransitions((Action<BoundedTo<POOrderEntry, POOrder>.Transition.ContainerAdjusterTransitions>) (transitions =>
        {
          transitions.UpdateGroupFrom("_", (Action<BoundedTo<POOrderEntry, POOrder>.Transition.SourceContainerFillerTransitions>) (ts =>
          {
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.rejected>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.initializeState)).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsRejected).PlaceBefore((Func<BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector, BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector>) (tr => tr.To<POOrderStatus.pendingPrint>()))));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.pendingApproval>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.initializeState)).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) BoundedTo<POOrderEntry, POOrder>.Condition.op_LogicalNot(conditions.IsApproved)).PlaceAfter((Func<BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector, BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector>) (tr => tr.To<POOrderStatus.rejected>()))));
          }));
          transitions.UpdateGroupFrom<POOrderStatus.hold>((Action<BoundedTo<POOrderEntry, POOrder>.Transition.SourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t =>
          {
            BoundedTo<POOrderEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.pendingApproval>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.releaseFromHold));
            BoundedTo<POOrderEntry, POOrder>.Condition condition11 = BoundedTo<POOrderEntry, POOrder>.Condition.op_LogicalNot(conditions.IsOnHold);
            BoundedTo<POOrderEntry, POOrder>.Condition condition12 = BoundedTo<POOrderEntry, POOrder>.Condition.op_False(condition11) ? condition11 : BoundedTo<POOrderEntry, POOrder>.Condition.op_BitwiseAnd(condition11, BoundedTo<POOrderEntry, POOrder>.Condition.op_LogicalNot(conditions.IsApproved));
            return (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) condition12).PlaceBefore((Func<BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector, BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector>) (tr => tr.To<POOrderStatus.pendingPrint>()));
          }))));
          transitions.AddGroupFrom<POOrderStatus.pendingApproval>((Action<BoundedTo<POOrderEntry, POOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
          {
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t =>
            {
              BoundedTo<POOrderEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.pendingPrint>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve);
              BoundedTo<POOrderEntry, POOrder>.Condition isApproved = conditions.IsApproved;
              BoundedTo<POOrderEntry, POOrder>.Condition condition = BoundedTo<POOrderEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<POOrderEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, BoundedTo<POOrderEntry, POOrder>.Condition.op_LogicalNot(conditions.IsPrinted));
              return (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) condition);
            }));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t =>
            {
              BoundedTo<POOrderEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.pendingEmail>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve);
              BoundedTo<POOrderEntry, POOrder>.Condition isApproved = conditions.IsApproved;
              BoundedTo<POOrderEntry, POOrder>.Condition condition = BoundedTo<POOrderEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<POOrderEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, BoundedTo<POOrderEntry, POOrder>.Condition.op_LogicalNot(conditions.IsEmailed));
              return (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) condition);
            }));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t =>
            {
              BoundedTo<POOrderEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.closed>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve);
              BoundedTo<POOrderEntry, POOrder>.Condition isApproved = conditions.IsApproved;
              BoundedTo<POOrderEntry, POOrder>.Condition condition = BoundedTo<POOrderEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<POOrderEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, conditions.HasAllLinesClosed);
              return (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) condition);
            }));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t =>
            {
              BoundedTo<POOrderEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.completed>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve);
              BoundedTo<POOrderEntry, POOrder>.Condition isApproved = conditions.IsApproved;
              BoundedTo<POOrderEntry, POOrder>.Condition condition = BoundedTo<POOrderEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<POOrderEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, conditions.HasAllLinesCompleted);
              return (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) condition);
            }));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t =>
            {
              BoundedTo<POOrderEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.awaitingLink>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve);
              BoundedTo<POOrderEntry, POOrder>.Condition isApproved = conditions.IsApproved;
              BoundedTo<POOrderEntry, POOrder>.Condition condition = BoundedTo<POOrderEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<POOrderEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, BoundedTo<POOrderEntry, POOrder>.Condition.op_LogicalNot(conditions.HasAllDropShipLinesLinked));
              return (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) condition);
            }));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.open>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsApproved)));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.rejected>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) reject).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsRejected)));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.hold>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold)).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsOnHold)));
          }));
          transitions.AddGroupFrom<POOrderStatus.rejected>((Action<BoundedTo<POOrderEntry, POOrder>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.hold>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold)).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsOnHold)))));
        }))));
      if (POOrderEntry_ApprovalWorkflow.IsNormalApprovalActive())
        flows.Update<POOrderType.regularOrder>((Func<BoundedTo<POOrderEntry, POOrder>.Workflow.ConfiguratorFlow, BoundedTo<POOrderEntry, POOrder>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.ContainerAdjusterStates>) (states =>
        {
          states.Add<POOrderStatus.pendingApproval>((Func<BoundedTo<POOrderEntry, POOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.IConfigured>) (state => (BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<POOrderEntry, POOrder>.ActionState.IContainerFillerActions>) (actions =>
          {
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add(approve, (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig) c).IsDuplicatedInToolbar()));
            actions.Add(reject, (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig) c).IsDuplicatedInToolbar()));
            actions.Add(reassign, (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.vendorDetails), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.printPurchaseOrder), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.viewDemand), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
          }))).WithFieldStates((Action<BoundedTo<POOrderEntry, POOrder>.FieldState.IContainerFillerFields>) (fields =>
          {
            fields.AddAllFields<POOrder>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddField<POOrder.orderType>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.orderNbr>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.controlTotal>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.workgroupID>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.ownerID>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.printed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.emailed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.dontPrint>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.dontEmail>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.excludeFromIntercompanyProc>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddTable<POLine>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddField<POLine.cancelled>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POLine.completed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POLine.promisedDate>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POLine.closed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddFields<POOrderEntry_Workflow.DropShipOrderLineFields>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddFields<POOrderEntry_Workflow.BlanketOrderLineFields>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddTable<POTax>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddAllFields<POShipAddress>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddAllFields<POShipContact>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddTable<PX.Objects.CM.Extensions.CurrencyInfo>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
          }))));
          states.Add<POOrderStatus.rejected>((Func<BoundedTo<POOrderEntry, POOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.IConfigured>) (state => (BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<POOrderEntry, POOrder>.ActionState.IContainerFillerActions>) (actions =>
          {
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.vendorDetails), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.printPurchaseOrder), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.viewPurchaseOrderReceipt), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
            actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.viewDemand), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
          }))).WithFieldStates((Action<BoundedTo<POOrderEntry, POOrder>.FieldState.IContainerFillerFields>) (fields =>
          {
            fields.AddTable<POOrder>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddField<POOrder.orderType>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POOrder.orderNbr>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddTable<POLine>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddField<POLine.cancelled>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POLine.completed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POLine.promisedDate>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddField<POLine.closed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
            fields.AddFields<POOrderEntry_Workflow.DropShipOrderLineFields>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddFields<POOrderEntry_Workflow.BlanketOrderLineFields>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
            fields.AddAllFields<PORemitAddress>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddAllFields<PORemitContact>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddAllFields<POShipAddress>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddAllFields<POShipContact>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddTable<POTax>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
            fields.AddTable<PX.Objects.CM.Extensions.CurrencyInfo>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
          }))));
        })).WithTransitions((Action<BoundedTo<POOrderEntry, POOrder>.Transition.ContainerAdjusterTransitions>) (transitions =>
        {
          transitions.UpdateGroupFrom("_", (Action<BoundedTo<POOrderEntry, POOrder>.Transition.SourceContainerFillerTransitions>) (ts =>
          {
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.rejected>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.initializeState)).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsRejected).PlaceBefore((Func<BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector, BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector>) (tr => tr.To<POOrderStatus.pendingPrint>()))));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.pendingApproval>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.initializeState)).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) BoundedTo<POOrderEntry, POOrder>.Condition.op_LogicalNot(conditions.IsApproved)).PlaceAfter((Func<BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector, BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector>) (tr => tr.To<POOrderStatus.rejected>()))));
          }));
          transitions.UpdateGroupFrom<POOrderStatus.hold>((Action<BoundedTo<POOrderEntry, POOrder>.Transition.SourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t =>
          {
            BoundedTo<POOrderEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.pendingApproval>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.releaseFromHold));
            BoundedTo<POOrderEntry, POOrder>.Condition condition15 = BoundedTo<POOrderEntry, POOrder>.Condition.op_LogicalNot(conditions.IsOnHold);
            BoundedTo<POOrderEntry, POOrder>.Condition condition16 = BoundedTo<POOrderEntry, POOrder>.Condition.op_False(condition15) ? condition15 : BoundedTo<POOrderEntry, POOrder>.Condition.op_BitwiseAnd(condition15, BoundedTo<POOrderEntry, POOrder>.Condition.op_LogicalNot(conditions.IsApproved));
            return (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) condition16).PlaceBefore((Func<BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector, BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector>) (tr => tr.To<POOrderStatus.pendingPrint>()));
          }))));
          transitions.AddGroupFrom<POOrderStatus.pendingApproval>((Action<BoundedTo<POOrderEntry, POOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
          {
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t =>
            {
              BoundedTo<POOrderEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.pendingPrint>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve);
              BoundedTo<POOrderEntry, POOrder>.Condition isApproved = conditions.IsApproved;
              BoundedTo<POOrderEntry, POOrder>.Condition condition = BoundedTo<POOrderEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<POOrderEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, BoundedTo<POOrderEntry, POOrder>.Condition.op_LogicalNot(conditions.IsPrinted));
              return (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) condition);
            }));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t =>
            {
              BoundedTo<POOrderEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.pendingEmail>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve);
              BoundedTo<POOrderEntry, POOrder>.Condition isApproved = conditions.IsApproved;
              BoundedTo<POOrderEntry, POOrder>.Condition condition = BoundedTo<POOrderEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<POOrderEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, BoundedTo<POOrderEntry, POOrder>.Condition.op_LogicalNot(conditions.IsEmailed));
              return (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) condition);
            }));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t =>
            {
              BoundedTo<POOrderEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.closed>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve);
              BoundedTo<POOrderEntry, POOrder>.Condition isApproved = conditions.IsApproved;
              BoundedTo<POOrderEntry, POOrder>.Condition condition = BoundedTo<POOrderEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<POOrderEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, conditions.HasAllLinesClosed);
              return (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) condition);
            }));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t =>
            {
              BoundedTo<POOrderEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.completed>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve);
              BoundedTo<POOrderEntry, POOrder>.Condition isApproved = conditions.IsApproved;
              BoundedTo<POOrderEntry, POOrder>.Condition condition = BoundedTo<POOrderEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<POOrderEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, conditions.HasAllLinesCompleted);
              return (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) condition);
            }));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.open>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsApproved)));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.rejected>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) reject).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsRejected)));
            ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.hold>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold)).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsOnHold)));
          }));
          transitions.AddGroupFrom<POOrderStatus.rejected>((Action<BoundedTo<POOrderEntry, POOrder>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.hold>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold)).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsOnHold)))));
        }))));
      if (!POOrderEntry_ApprovalWorkflow.IsProjectDropShipApprovalActive())
        return;
      flows.Update<POOrderType.projectDropShip>((Func<BoundedTo<POOrderEntry, POOrder>.Workflow.ConfiguratorFlow, BoundedTo<POOrderEntry, POOrder>.Workflow.ConfiguratorFlow>) (flow => flow.WithFlowStates((Action<BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.ContainerAdjusterStates>) (states =>
      {
        states.Add<POOrderStatus.pendingApproval>((Func<BoundedTo<POOrderEntry, POOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.IConfigured>) (state => (BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<POOrderEntry, POOrder>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
          actions.Add(approve, (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig) c).IsDuplicatedInToolbar()));
          actions.Add(reject, (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig) c).IsDuplicatedInToolbar()));
          actions.Add(reassign, (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.vendorDetails), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.printPurchaseOrder), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
        }))).WithFieldStates((Action<BoundedTo<POOrderEntry, POOrder>.FieldState.IContainerFillerFields>) (fields =>
        {
          fields.AddAllFields<POOrder>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
          fields.AddField<POOrder.orderType>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
          fields.AddField<POOrder.orderNbr>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
          fields.AddField<POOrder.controlTotal>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
          fields.AddField<POOrder.workgroupID>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
          fields.AddField<POOrder.ownerID>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
          fields.AddField<POOrder.printed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
          fields.AddField<POOrder.emailed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
          fields.AddField<POOrder.dontPrint>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
          fields.AddField<POOrder.dontEmail>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
          fields.AddField<POOrder.excludeFromIntercompanyProc>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
          fields.AddTable<POLine>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
          fields.AddField<POLine.cancelled>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
          fields.AddField<POLine.completed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
          fields.AddField<POLine.promisedDate>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
          fields.AddField<POLine.closed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
          fields.AddFields<POOrderEntry_Workflow.DropShipOrderLineFields>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
          fields.AddFields<POOrderEntry_Workflow.BlanketOrderLineFields>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
          fields.AddTable<POTax>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
          fields.AddAllFields<POShipAddress>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
          fields.AddAllFields<POShipContact>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
          fields.AddTable<PX.Objects.CM.Extensions.CurrencyInfo>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        }))));
        states.Add<POOrderStatus.rejected>((Func<BoundedTo<POOrderEntry, POOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.IConfigured>) (state => (BoundedTo<POOrderEntry, POOrder>.BaseFlowStep.IConfigured) ((BoundedTo<POOrderEntry, POOrder>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<POOrderEntry, POOrder>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.validateAddresses), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.vendorDetails), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.printPurchaseOrder), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.viewPurchaseOrderReceipt), (Func<BoundedTo<POOrderEntry, POOrder>.ActionState.IAllowOptionalConfig, BoundedTo<POOrderEntry, POOrder>.ActionState.IConfigured>) null);
        }))).WithFieldStates((Action<BoundedTo<POOrderEntry, POOrder>.FieldState.IContainerFillerFields>) (fields =>
        {
          fields.AddTable<POOrder>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
          fields.AddField<POOrder.orderType>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
          fields.AddField<POOrder.orderNbr>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
          fields.AddTable<POLine>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
          fields.AddField<POLine.cancelled>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
          fields.AddField<POLine.completed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
          fields.AddField<POLine.promisedDate>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
          fields.AddField<POLine.closed>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) null);
          fields.AddFields<POOrderEntry_Workflow.DropShipOrderLineFields>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
          fields.AddFields<POOrderEntry_Workflow.BlanketOrderLineFields>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsHidden()));
          fields.AddAllFields<PORemitAddress>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
          fields.AddAllFields<PORemitContact>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
          fields.AddAllFields<POShipAddress>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
          fields.AddAllFields<POShipContact>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
          fields.AddTable<POTax>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
          fields.AddTable<PX.Objects.CM.Extensions.CurrencyInfo>((Func<BoundedTo<POOrderEntry, POOrder>.FieldState.INeedAnyConfigField, BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured>) (c => (BoundedTo<POOrderEntry, POOrder>.FieldState.IConfigured) c.IsDisabled()));
        }))));
      })).WithTransitions((Action<BoundedTo<POOrderEntry, POOrder>.Transition.ContainerAdjusterTransitions>) (transitions =>
      {
        transitions.UpdateGroupFrom("_", (Action<BoundedTo<POOrderEntry, POOrder>.Transition.SourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.rejected>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.initializeState)).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsRejected).PlaceBefore((Func<BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector, BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector>) (tr => tr.To<POOrderStatus.pendingPrint>()))));
          ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.pendingApproval>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.initializeState)).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) BoundedTo<POOrderEntry, POOrder>.Condition.op_LogicalNot(conditions.IsApproved)).PlaceAfter((Func<BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector, BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector>) (tr => tr.To<POOrderStatus.rejected>()))));
        }));
        transitions.UpdateGroupFrom<POOrderStatus.hold>((Action<BoundedTo<POOrderEntry, POOrder>.Transition.SourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t =>
        {
          BoundedTo<POOrderEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.pendingApproval>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.releaseFromHold));
          BoundedTo<POOrderEntry, POOrder>.Condition condition19 = BoundedTo<POOrderEntry, POOrder>.Condition.op_LogicalNot(conditions.IsOnHold);
          BoundedTo<POOrderEntry, POOrder>.Condition condition20 = BoundedTo<POOrderEntry, POOrder>.Condition.op_False(condition19) ? condition19 : BoundedTo<POOrderEntry, POOrder>.Condition.op_BitwiseAnd(condition19, BoundedTo<POOrderEntry, POOrder>.Condition.op_LogicalNot(conditions.IsApproved));
          return (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) condition20).PlaceBefore((Func<BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector, BoundedTo<POOrderEntry, POOrder>.Transition.ITransitionSelector>) (tr => tr.To<POOrderStatus.pendingPrint>()));
        }))));
        transitions.AddGroupFrom<POOrderStatus.pendingApproval>((Action<BoundedTo<POOrderEntry, POOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t =>
          {
            BoundedTo<POOrderEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.pendingPrint>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve);
            BoundedTo<POOrderEntry, POOrder>.Condition isApproved = conditions.IsApproved;
            BoundedTo<POOrderEntry, POOrder>.Condition condition = BoundedTo<POOrderEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<POOrderEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, BoundedTo<POOrderEntry, POOrder>.Condition.op_LogicalNot(conditions.IsPrinted));
            return (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) condition);
          }));
          ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t =>
          {
            BoundedTo<POOrderEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.pendingEmail>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve);
            BoundedTo<POOrderEntry, POOrder>.Condition isApproved = conditions.IsApproved;
            BoundedTo<POOrderEntry, POOrder>.Condition condition = BoundedTo<POOrderEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<POOrderEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, BoundedTo<POOrderEntry, POOrder>.Condition.op_LogicalNot(conditions.IsEmailed));
            return (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) condition);
          }));
          ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t =>
          {
            BoundedTo<POOrderEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.closed>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve);
            BoundedTo<POOrderEntry, POOrder>.Condition isApproved = conditions.IsApproved;
            BoundedTo<POOrderEntry, POOrder>.Condition condition = BoundedTo<POOrderEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<POOrderEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, conditions.HasAllLinesClosed);
            return (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) condition);
          }));
          ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t =>
          {
            BoundedTo<POOrderEntry, POOrder>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To<POOrderStatus.completed>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve);
            BoundedTo<POOrderEntry, POOrder>.Condition isApproved = conditions.IsApproved;
            BoundedTo<POOrderEntry, POOrder>.Condition condition = BoundedTo<POOrderEntry, POOrder>.Condition.op_False(isApproved) ? isApproved : BoundedTo<POOrderEntry, POOrder>.Condition.op_BitwiseAnd(isApproved, conditions.HasAllLinesCompleted);
            return (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) condition);
          }));
          ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.open>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsApproved)));
          ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.rejected>().IsTriggeredOn((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) reject).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsRejected)));
          ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.hold>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold)).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsOnHold)));
        }));
        transitions.AddGroupFrom<POOrderStatus.rejected>((Action<BoundedTo<POOrderEntry, POOrder>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<POOrderEntry, POOrder>.Transition.INeedTarget, BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured>) (t => (BoundedTo<POOrderEntry, POOrder>.Transition.IConfigured) t.To<POOrderStatus.hold>().IsTriggeredOn((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold)).When((BoundedTo<POOrderEntry, POOrder>.ISharedCondition) conditions.IsOnHold)))));
      }))));
    })).WithActions((Action<BoundedTo<POOrderEntry, POOrder>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) approve);
      actions.Add((BoundedTo<POOrderEntry, POOrder>.ActionDefinition.IConfigured) reject);
      actions.Add(reassign);
      actions.Update((Expression<Func<POOrderEntry, PXAction<POOrder>>>) (g => g.putOnHold), (Func<BoundedTo<POOrderEntry, POOrder>.ActionDefinition.ConfiguratorAction, BoundedTo<POOrderEntry, POOrder>.ActionDefinition.ConfiguratorAction>) (a => a.WithFieldAssignments((Action<BoundedTo<POOrderEntry, POOrder>.Assignment.FieldContainerAdjusterAssignment>) (fas =>
      {
        fas.Add<POOrder.approved>(new bool?(false));
        fas.Add<POOrder.rejected>(new bool?(false));
      }))));
    })).WithCategories((Action<BoundedTo<POOrderEntry, POOrder>.ActionCategory.ContainerAdjusterCategories>) (categories => categories.Add(approvalCategory)))));

    BoundedTo<POOrderEntry, POOrder>.Condition Bql<T>() where T : IBqlUnary, new()
    {
      return context.Conditions.FromBql<T>();
    }

    BoundedTo<POOrderEntry, POOrder>.Condition Existing(string name)
    {
      return (BoundedTo<POOrderEntry, POOrder>.Condition) context.Conditions.Get(name);
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReassignApproval(PXAdapter adapter)
  {
    return ((PXGraphExtension<POOrderEntry>) this).Base.Approval.ReassignApproval(adapter);
  }

  private class POApprovalSettings : IPrefetchable, IPXCompanyDependent
  {
    private bool OrderRequestApproval;
    private bool BlanketOrderRequestApproval;
    private bool DropShipOrderRequestApproval;
    private bool NormalOrderRequestApproval;
    private bool StandartOrderRequestApproval;
    private bool ProjectDropShipOrderRequestApproval;

    private static POOrderEntry_ApprovalWorkflow.POApprovalSettings Current
    {
      get
      {
        return PXDatabase.GetSlot<POOrderEntry_ApprovalWorkflow.POApprovalSettings>(nameof (POApprovalSettings), new Type[2]
        {
          typeof (POSetup),
          typeof (POSetupApproval)
        });
      }
    }

    public static bool IsActive
    {
      get => POOrderEntry_ApprovalWorkflow.POApprovalSettings.Current.OrderRequestApproval;
    }

    public static bool IsActiveForBlanket
    {
      get => POOrderEntry_ApprovalWorkflow.POApprovalSettings.Current.BlanketOrderRequestApproval;
    }

    public static bool IsActiveForDropShip
    {
      get => POOrderEntry_ApprovalWorkflow.POApprovalSettings.Current.DropShipOrderRequestApproval;
    }

    public static bool IsActiveForNormal
    {
      get => POOrderEntry_ApprovalWorkflow.POApprovalSettings.Current.NormalOrderRequestApproval;
    }

    public static bool IsActiveForStandart
    {
      get => POOrderEntry_ApprovalWorkflow.POApprovalSettings.Current.StandartOrderRequestApproval;
    }

    public static bool IsActiveForProjectDropShip
    {
      get
      {
        return POOrderEntry_ApprovalWorkflow.POApprovalSettings.Current.ProjectDropShipOrderRequestApproval;
      }
    }

    void IPrefetchable.Prefetch()
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<POSetup>(new PXDataField[1]
      {
        (PXDataField) new PXDataField<POSetup.orderRequestApproval>()
      }))
      {
        if (pxDataRecord != null)
          this.OrderRequestApproval = pxDataRecord.GetBoolean(0).Value;
      }
      if (!this.OrderRequestApproval)
        return;
      this.BlanketOrderRequestApproval = POOrderEntry_ApprovalWorkflow.POApprovalSettings.RequestApproval("BL");
      this.DropShipOrderRequestApproval = POOrderEntry_ApprovalWorkflow.POApprovalSettings.RequestApproval("DP");
      this.NormalOrderRequestApproval = POOrderEntry_ApprovalWorkflow.POApprovalSettings.RequestApproval("RO");
      this.StandartOrderRequestApproval = POOrderEntry_ApprovalWorkflow.POApprovalSettings.RequestApproval("SB");
      this.ProjectDropShipOrderRequestApproval = POOrderEntry_ApprovalWorkflow.POApprovalSettings.RequestApproval("PD");
    }

    private static bool RequestApproval(string orderType)
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<POSetupApproval>(new PXDataField[2]
      {
        (PXDataField) new PXDataField<POSetupApproval.approvalID>(),
        (PXDataField) new PXDataFieldValue<POSetupApproval.orderType>((object) orderType)
      }))
      {
        if (pxDataRecord != null)
          return pxDataRecord.GetInt32(0).HasValue;
      }
      return false;
    }
  }
}
