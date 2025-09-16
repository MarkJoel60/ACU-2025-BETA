// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequestEntry_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.PO;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.RQ;

public class RQRequestEntry_Workflow : PXGraphExtension<RQRequestEntry>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    RQRequestEntry_Workflow.Configure(config.GetScreenConfigurationContext<RQRequestEntry, RQRequest>());
  }

  protected static void Configure(WorkflowContext<RQRequestEntry, RQRequest> context)
  {
    RQRequestEntry_Workflow.Conditions conditions = context.Conditions.GetPack<RQRequestEntry_Workflow.Conditions>();
    CommonActionCategories.Categories<RQRequestEntry, RQRequest> categories1 = CommonActionCategories.Get<RQRequestEntry, RQRequest>(context);
    BoundedTo<RQRequestEntry, RQRequest>.ActionCategory.IConfigured processingCategory = categories1.Processing;
    BoundedTo<RQRequestEntry, RQRequest>.ActionCategory.IConfigured printingEmailingCategory = categories1.PrintingAndEmailing;
    BoundedTo<RQRequestEntry, RQRequest>.ActionCategory.IConfigured otherCategory = categories1.Other;
    context.AddScreenConfigurationFor((Func<BoundedTo<RQRequestEntry, RQRequest>.ScreenConfiguration.IStartConfigScreen, BoundedTo<RQRequestEntry, RQRequest>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<RQRequestEntry, RQRequest>.ScreenConfiguration.IConfigured) ((BoundedTo<RQRequestEntry, RQRequest>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<RQRequest.status>().AddDefaultFlow((Func<BoundedTo<RQRequestEntry, RQRequest>.Workflow.INeedStatesFlow, BoundedTo<RQRequestEntry, RQRequest>.Workflow.IConfigured>) (flow => (BoundedTo<RQRequestEntry, RQRequest>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<RQRequestEntry, RQRequest>.BaseFlowStep.IContainerFillerStates>) (flowStates =>
    {
      flowStates.Add("_", (Func<BoundedTo<RQRequestEntry, RQRequest>.FlowState.INeedAnyFlowStateConfig, BoundedTo<RQRequestEntry, RQRequest>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<RQRequestEntry, RQRequest>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<RQRequestEntry, PXAutoAction<RQRequest>>>) (g => g.initializeState))));
      flowStates.Add<RQRequestStatus.hold>((Func<BoundedTo<RQRequestEntry, RQRequest>.FlowState.INeedAnyFlowStateConfig, BoundedTo<RQRequestEntry, RQRequest>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<RQRequestEntry, RQRequest>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<RQRequestEntry, RQRequest>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.releaseFromHold), (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionState.IAllowOptionalConfig, BoundedTo<RQRequestEntry, RQRequest>.ActionState.IConfigured>) (a => (BoundedTo<RQRequestEntry, RQRequest>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.requestForm), (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionState.IAllowOptionalConfig, BoundedTo<RQRequestEntry, RQRequest>.ActionState.IConfigured>) null);
      }))));
      flowStates.Add<RQRequestStatus.open>((Func<BoundedTo<RQRequestEntry, RQRequest>.FlowState.INeedAnyFlowStateConfig, BoundedTo<RQRequestEntry, RQRequest>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<RQRequestEntry, RQRequest>.BaseFlowStep.IConfigured) ((BoundedTo<RQRequestEntry, RQRequest>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<RQRequestEntry, RQRequest>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<RQRequestEntry, RQRequest>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.putOnHold), (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionState.IAllowOptionalConfig, BoundedTo<RQRequestEntry, RQRequest>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.cancelRequest), (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionState.IAllowOptionalConfig, BoundedTo<RQRequestEntry, RQRequest>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.requestForm), (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionState.IAllowOptionalConfig, BoundedTo<RQRequestEntry, RQRequest>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<RQRequestEntry, RQRequest>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<RQRequestEntry, PXWorkflowEventHandler<RQRequest>>>) (g => g.OnOpenOrderQtyExhausted))))).WithFieldStates((Action<BoundedTo<RQRequestEntry, RQRequest>.FieldState.IContainerFillerFields>) (fieldStates =>
      {
        RQRequestEntry_Workflow.DisableWholeScreen(fieldStates);
        fieldStates.AddField<RQRequestLine.requestedDate>((Func<BoundedTo<RQRequestEntry, RQRequest>.FieldState.INeedAnyConfigField, BoundedTo<RQRequestEntry, RQRequest>.FieldState.IConfigured>) null);
        fieldStates.AddField<RQRequestLine.promisedDate>((Func<BoundedTo<RQRequestEntry, RQRequest>.FieldState.INeedAnyConfigField, BoundedTo<RQRequestEntry, RQRequest>.FieldState.IConfigured>) null);
        fieldStates.AddField<RQRequestLine.cancelled>((Func<BoundedTo<RQRequestEntry, RQRequest>.FieldState.INeedAnyConfigField, BoundedTo<RQRequestEntry, RQRequest>.FieldState.IConfigured>) null);
        fieldStates.AddField<RQRequestLine.inventoryID>((Func<BoundedTo<RQRequestEntry, RQRequest>.FieldState.INeedAnyConfigField, BoundedTo<RQRequestEntry, RQRequest>.FieldState.IConfigured>) null);
        fieldStates.AddField<RQRequestLine.subItemID>((Func<BoundedTo<RQRequestEntry, RQRequest>.FieldState.INeedAnyConfigField, BoundedTo<RQRequestEntry, RQRequest>.FieldState.IConfigured>) null);
        fieldStates.AddField<RQRequestLine.description>((Func<BoundedTo<RQRequestEntry, RQRequest>.FieldState.INeedAnyConfigField, BoundedTo<RQRequestEntry, RQRequest>.FieldState.IConfigured>) null);
        fieldStates.AddField<RQRequestLine.orderQty>((Func<BoundedTo<RQRequestEntry, RQRequest>.FieldState.INeedAnyConfigField, BoundedTo<RQRequestEntry, RQRequest>.FieldState.IConfigured>) null);
        fieldStates.AddField<RQRequestLine.uOM>((Func<BoundedTo<RQRequestEntry, RQRequest>.FieldState.INeedAnyConfigField, BoundedTo<RQRequestEntry, RQRequest>.FieldState.IConfigured>) null);
        fieldStates.AddField<RQRequestLine.estUnitCost>((Func<BoundedTo<RQRequestEntry, RQRequest>.FieldState.INeedAnyConfigField, BoundedTo<RQRequestEntry, RQRequest>.FieldState.IConfigured>) null);
      }))));
      flowStates.Add<RQRequestStatus.closed>((Func<BoundedTo<RQRequestEntry, RQRequest>.FlowState.INeedAnyFlowStateConfig, BoundedTo<RQRequestEntry, RQRequest>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<RQRequestEntry, RQRequest>.BaseFlowStep.IConfigured) ((BoundedTo<RQRequestEntry, RQRequest>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<RQRequestEntry, RQRequest>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<RQRequestEntry, RQRequest>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.requestForm), (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionState.IAllowOptionalConfig, BoundedTo<RQRequestEntry, RQRequest>.ActionState.IConfigured>) null)))).WithEventHandlers((Action<BoundedTo<RQRequestEntry, RQRequest>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<RQRequestEntry, PXWorkflowEventHandler<RQRequest>>>) (g => g.OnOpenOrderQtyIncreased))))).WithFieldStates(new Action<BoundedTo<RQRequestEntry, RQRequest>.FieldState.IContainerFillerFields>(RQRequestEntry_Workflow.DisableWholeScreen))));
      flowStates.Add<RQRequestStatus.canceled>((Func<BoundedTo<RQRequestEntry, RQRequest>.FlowState.INeedAnyFlowStateConfig, BoundedTo<RQRequestEntry, RQRequest>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<RQRequestEntry, RQRequest>.BaseFlowStep.IConfigured) ((BoundedTo<RQRequestEntry, RQRequest>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<RQRequestEntry, RQRequest>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.putOnHold), (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionState.IAllowOptionalConfig, BoundedTo<RQRequestEntry, RQRequest>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.requestForm), (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionState.IAllowOptionalConfig, BoundedTo<RQRequestEntry, RQRequest>.ActionState.IConfigured>) null);
      }))).WithFieldStates(new Action<BoundedTo<RQRequestEntry, RQRequest>.FieldState.IContainerFillerFields>(RQRequestEntry_Workflow.DisableWholeScreen))));
    })).WithTransitions((Action<BoundedTo<RQRequestEntry, RQRequest>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<RQRequestEntry, RQRequest>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<RQRequestEntry, RQRequest>.Transition.INeedTarget, BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured>) (t => (BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured) t.To<RQRequestStatus.canceled>().IsTriggeredOn((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.initializeState)).When((BoundedTo<RQRequestEntry, RQRequest>.ISharedCondition) conditions.IsCancelled)));
        ts.Add((Func<BoundedTo<RQRequestEntry, RQRequest>.Transition.INeedTarget, BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured>) (t => (BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured) t.To<RQRequestStatus.hold>().IsTriggeredOn((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.initializeState)).When((BoundedTo<RQRequestEntry, RQRequest>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<RQRequestEntry, RQRequest>.Transition.INeedTarget, BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured>) (t => (BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured) t.To<RQRequestStatus.closed>().IsTriggeredOn((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.initializeState)).When((BoundedTo<RQRequestEntry, RQRequest>.ISharedCondition) conditions.HasZeroOpenOrderQty)));
        ts.Add((Func<BoundedTo<RQRequestEntry, RQRequest>.Transition.INeedTarget, BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured>) (t => (BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured) t.To<RQRequestStatus.open>().IsTriggeredOn((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.initializeState))));
      }));
      transitions.AddGroupFrom<RQRequestStatus.hold>((Action<BoundedTo<RQRequestEntry, RQRequest>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<RQRequestEntry, RQRequest>.Transition.INeedTarget, BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured>) (t => (BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured) t.To<RQRequestStatus.closed>().IsTriggeredOn((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.releaseFromHold)).When((BoundedTo<RQRequestEntry, RQRequest>.ISharedCondition) conditions.HasZeroOpenOrderQty)));
        ts.Add((Func<BoundedTo<RQRequestEntry, RQRequest>.Transition.INeedTarget, BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured>) (t => (BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured) t.To<RQRequestStatus.open>().IsTriggeredOn((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.releaseFromHold))));
      }));
      transitions.AddGroupFrom<RQRequestStatus.open>((Action<BoundedTo<RQRequestEntry, RQRequest>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<RQRequestEntry, RQRequest>.Transition.INeedTarget, BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured>) (t => (BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured) t.To<RQRequestStatus.hold>().IsTriggeredOn((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.putOnHold)).When((BoundedTo<RQRequestEntry, RQRequest>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<RQRequestEntry, RQRequest>.Transition.INeedTarget, BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured>) (t => (BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured) t.To<RQRequestStatus.canceled>().IsTriggeredOn((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.cancelRequest)).When((BoundedTo<RQRequestEntry, RQRequest>.ISharedCondition) conditions.IsCancelled)));
        ts.Add((Func<BoundedTo<RQRequestEntry, RQRequest>.Transition.INeedTarget, BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured>) (t => (BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured) t.To<RQRequestStatus.closed>().IsTriggeredOn((Expression<Func<RQRequestEntry, PXWorkflowEventHandlerBase<RQRequest>>>) (g => g.OnOpenOrderQtyExhausted))));
      }));
      transitions.AddGroupFrom<RQRequestStatus.closed>((Action<BoundedTo<RQRequestEntry, RQRequest>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<RQRequestEntry, RQRequest>.Transition.INeedTarget, BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured>) (t => (BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured) t.To<RQRequestStatus.open>().IsTriggeredOn((Expression<Func<RQRequestEntry, PXWorkflowEventHandlerBase<RQRequest>>>) (g => g.OnOpenOrderQtyIncreased))))));
      transitions.AddGroupFrom<RQRequestStatus.canceled>((Action<BoundedTo<RQRequestEntry, RQRequest>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<RQRequestEntry, RQRequest>.Transition.INeedTarget, BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured>) (t => (BoundedTo<RQRequestEntry, RQRequest>.Transition.IConfigured) t.To<RQRequestStatus.hold>().IsTriggeredOn((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.putOnHold)).When((BoundedTo<RQRequestEntry, RQRequest>.ISharedCondition) conditions.IsOnHold)))));
    })))).WithActions((Action<BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.initializeState), (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured>) (a => (BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.releaseFromHold), (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured>) (a => (BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured) a.WithCategory(processingCategory).WithFieldAssignments((Action<BoundedTo<RQRequestEntry, RQRequest>.Assignment.IContainerFillerFields>) (fass => fass.Add<RQRequest.hold>(new bool?(false))))));
      actions.Add((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.putOnHold), (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured>) (a => (BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured) a.WithCategory(processingCategory).WithFieldAssignments((Action<BoundedTo<RQRequestEntry, RQRequest>.Assignment.IContainerFillerFields>) (fass => fass.Add<RQRequest.hold>(new bool?(true))))));
      actions.Add((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.cancelRequest), (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured>) (a => (BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured) a.WithCategory(processingCategory).WithFieldAssignments((Action<BoundedTo<RQRequestEntry, RQRequest>.Assignment.IContainerFillerFields>) (fass => fass.Add<RQRequest.cancelled>(new bool?(true))))));
      actions.Add((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.requestForm), (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured>) (a => (BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured) a.WithCategory(printingEmailingCategory)));
      actions.Add((Expression<Func<RQRequestEntry, PXAction<RQRequest>>>) (g => g.validateAddresses), (Func<BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured>) (a => (BoundedTo<RQRequestEntry, RQRequest>.ActionDefinition.IConfigured) a.WithCategory(otherCategory)));
    })).WithCategories((Action<BoundedTo<RQRequestEntry, RQRequest>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(printingEmailingCategory);
      categories.Add(otherCategory);
    })).WithHandlers((Action<BoundedTo<RQRequestEntry, RQRequest>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers =>
    {
      handlers.Add((Func<BoundedTo<RQRequestEntry, RQRequest>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<RQRequestEntry, RQRequest>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<RQRequestEntry, RQRequest>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<RQRequestEntry, RQRequest>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<RQRequest>) ((BoundedTo<RQRequestEntry, RQRequest>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<RQRequest>) ((BoundedTo<RQRequestEntry, RQRequest>.WorkflowEventHandlerDefinition.INeedSubscriber<RQRequest>) handler.WithTargetOf<RQRequest>().OfEntityEvent<RQRequest.Events>((Expression<Func<RQRequest.Events, PXEntityEvent<RQRequest>>>) (e => e.OpenOrderQtyChanged))).Is((Expression<Func<RQRequest, PXWorkflowEventHandler<RQRequest, RQRequest>>>) (g => g.OnOpenOrderQtyExhausted))).UsesTargetAsPrimaryEntity()).AppliesWhen((BoundedTo<RQRequest, RQRequest>.ISharedCondition) conditions.HasZeroOpenOrderQty)));
      handlers.Add((Func<BoundedTo<RQRequestEntry, RQRequest>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<RQRequestEntry, RQRequest>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<RQRequestEntry, RQRequest>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<RQRequestEntry, RQRequest>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<RQRequest>) ((BoundedTo<RQRequestEntry, RQRequest>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<RQRequest>) ((BoundedTo<RQRequestEntry, RQRequest>.WorkflowEventHandlerDefinition.INeedSubscriber<RQRequest>) handler.WithTargetOf<RQRequest>().OfEntityEvent<RQRequest.Events>((Expression<Func<RQRequest.Events, PXEntityEvent<RQRequest>>>) (e => e.OpenOrderQtyChanged))).Is((Expression<Func<RQRequest, PXWorkflowEventHandler<RQRequest, RQRequest>>>) (g => g.OnOpenOrderQtyIncreased))).UsesTargetAsPrimaryEntity()).AppliesWhen((BoundedTo<RQRequest, RQRequest>.ISharedCondition) conditions.HasOpenOrderQty)));
    }))));
  }

  public static void DisableWholeScreen(
    BoundedTo<RQRequestEntry, RQRequest>.FieldState.IContainerFillerFields fieldStates)
  {
    fieldStates.AddAllFields<RQRequest>((Func<BoundedTo<RQRequestEntry, RQRequest>.FieldState.INeedAnyConfigField, BoundedTo<RQRequestEntry, RQRequest>.FieldState.IConfigured>) (fs => (BoundedTo<RQRequestEntry, RQRequest>.FieldState.IConfigured) fs.IsDisabled()));
    fieldStates.AddField<RQRequest.orderNbr>((Func<BoundedTo<RQRequestEntry, RQRequest>.FieldState.INeedAnyConfigField, BoundedTo<RQRequestEntry, RQRequest>.FieldState.IConfigured>) null);
    fieldStates.AddTable<RQRequestLine>((Func<BoundedTo<RQRequestEntry, RQRequest>.FieldState.INeedAnyConfigField, BoundedTo<RQRequestEntry, RQRequest>.FieldState.IConfigured>) (fs => (BoundedTo<RQRequestEntry, RQRequest>.FieldState.IConfigured) fs.IsDisabled()));
    fieldStates.AddTable<POShipAddress>((Func<BoundedTo<RQRequestEntry, RQRequest>.FieldState.INeedAnyConfigField, BoundedTo<RQRequestEntry, RQRequest>.FieldState.IConfigured>) (fs => (BoundedTo<RQRequestEntry, RQRequest>.FieldState.IConfigured) fs.IsDisabled()));
    fieldStates.AddTable<POShipContact>((Func<BoundedTo<RQRequestEntry, RQRequest>.FieldState.INeedAnyConfigField, BoundedTo<RQRequestEntry, RQRequest>.FieldState.IConfigured>) (fs => (BoundedTo<RQRequestEntry, RQRequest>.FieldState.IConfigured) fs.IsDisabled()));
    fieldStates.AddTable<PX.Objects.CM.CurrencyInfo>((Func<BoundedTo<RQRequestEntry, RQRequest>.FieldState.INeedAnyConfigField, BoundedTo<RQRequestEntry, RQRequest>.FieldState.IConfigured>) (fs => (BoundedTo<RQRequestEntry, RQRequest>.FieldState.IConfigured) fs.IsDisabled()));
  }

  public class Conditions : BoundedTo<RQRequestEntry, RQRequest>.Condition.Pack
  {
    public BoundedTo<RQRequestEntry, RQRequest>.Condition IsCancelled
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<RQRequestEntry, RQRequest>.Condition.ConditionBuilder, BoundedTo<RQRequestEntry, RQRequest>.Condition>) (b => b.FromBql<BqlOperand<RQRequest.cancelled, IBqlBool>.IsEqual<True>>()), nameof (IsCancelled));
      }
    }

    public BoundedTo<RQRequestEntry, RQRequest>.Condition IsOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<RQRequestEntry, RQRequest>.Condition.ConditionBuilder, BoundedTo<RQRequestEntry, RQRequest>.Condition>) (b => b.FromBql<BqlOperand<RQRequest.hold, IBqlBool>.IsEqual<True>>()), nameof (IsOnHold));
      }
    }

    public BoundedTo<RQRequestEntry, RQRequest>.Condition HasOpenOrderQty
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<RQRequestEntry, RQRequest>.Condition.ConditionBuilder, BoundedTo<RQRequestEntry, RQRequest>.Condition>) (b => b.FromBql<BqlOperand<RQRequest.openOrderQty, IBqlDecimal>.IsGreater<decimal0>>()), nameof (HasOpenOrderQty));
      }
    }

    public BoundedTo<RQRequestEntry, RQRequest>.Condition HasZeroOpenOrderQty
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<RQRequestEntry, RQRequest>.Condition.ConditionBuilder, BoundedTo<RQRequestEntry, RQRequest>.Condition>) (b => b.FromBql<BqlOperand<RQRequest.openOrderQty, IBqlDecimal>.IsEqual<decimal0>>()), nameof (HasZeroOpenOrderQty));
      }
    }
  }
}
