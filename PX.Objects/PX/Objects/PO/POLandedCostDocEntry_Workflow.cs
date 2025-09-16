// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLandedCostDocEntry_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.Common;
using PX.Objects.PO.LandedCosts.Attributes;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.PO;

public class POLandedCostDocEntry_Workflow : PXGraphExtension<POLandedCostDocEntry>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    POLandedCostDocEntry_Workflow.Configure(config.GetScreenConfigurationContext<POLandedCostDocEntry, POLandedCostDoc>());
  }

  protected static void Configure(
    WorkflowContext<POLandedCostDocEntry, POLandedCostDoc> context)
  {
    var conditions = new
    {
      IsOnHold = Bql<BqlOperand<POLandedCostDoc.hold, IBqlBool>.IsEqual<True>>(),
      IsReleased = Bql<BqlOperand<POLandedCostDoc.released, IBqlBool>.IsEqual<True>>()
    }.AutoNameConditions();
    BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionCategory.IConfigured processingCategory = CommonActionCategories.Get<POLandedCostDocEntry, POLandedCostDoc>(context).Processing;
    context.AddScreenConfigurationFor((Func<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ScreenConfiguration.IStartConfigScreen, BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ScreenConfiguration.IConfigured) ((BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<POLandedCostDoc.status>().FlowTypeIdentifierIs<POLandedCostDoc.docType>(false).WithFlows((Action<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Workflow.IContainerFillerFlows>) (flows => flows.Add<POLandedCostDocType.landedCost>((Func<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Workflow.INeedStatesFlow, BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Workflow.IConfigured>) (flow => (BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.BaseFlowStep.IContainerFillerStates>) (states =>
    {
      states.Add("_", (Func<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.FlowState.INeedAnyFlowStateConfig, BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.BaseFlowStep.IConfigured>) (state => (BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.BaseFlowStep.IConfigured) state.IsInitial((Expression<Func<POLandedCostDocEntry, PXAutoAction<POLandedCostDoc>>>) (g => g.initializeState))));
      states.Add<POLandedCostDocStatus.hold>((Func<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.FlowState.INeedAnyFlowStateConfig, BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.BaseFlowStep.IConfigured>) (state => (BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.BaseFlowStep.IConfigured) state.WithActions((Action<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<POLandedCostDocEntry, PXAction<POLandedCostDoc>>>) (g => g.releaseFromHold), (Func<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionState.IAllowOptionalConfig, BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionState.IConfigured>) (c => (BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)))))));
      states.Add<POLandedCostDocStatus.balanced>((Func<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.FlowState.INeedAnyFlowStateConfig, BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.BaseFlowStep.IConfigured>) (state => (BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.BaseFlowStep.IConfigured) ((BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<POLandedCostDocEntry, PXAction<POLandedCostDoc>>>) (g => g.release), (Func<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionState.IAllowOptionalConfig, BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionState.IConfigured>) (c => (BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<POLandedCostDocEntry, PXAction<POLandedCostDoc>>>) (g => g.putOnHold), (Func<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionState.IAllowOptionalConfig, BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<POLandedCostDocEntry, PXWorkflowEventHandler<POLandedCostDoc>>>) (g => g.OnInventoryAdjustmentCreated))))));
      states.Add<POLandedCostDocStatus.released>((Func<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.FlowState.INeedAnyFlowStateConfig, BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.BaseFlowStep.IConfigured>) (state => (BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.BaseFlowStep.IConfigured) state.WithActions((Action<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<POLandedCostDocEntry, PXAction<POLandedCostDoc>>>) (g => g.createAPInvoice), (Func<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionState.IAllowOptionalConfig, BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionState.IConfigured>) (c => (BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)))))));
    })).WithTransitions((Action<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Transition.INeedTarget, BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Transition.IConfigured>) (t => (BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Transition.IConfigured) t.To<POLandedCostDocStatus.hold>().IsTriggeredOn((Expression<Func<POLandedCostDocEntry, PXAction<POLandedCostDoc>>>) (g => g.initializeState)).When((BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Transition.INeedTarget, BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Transition.IConfigured>) (t => (BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Transition.IConfigured) t.To<POLandedCostDocStatus.released>().IsTriggeredOn((Expression<Func<POLandedCostDocEntry, PXAction<POLandedCostDoc>>>) (g => g.initializeState)).When((BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ISharedCondition) conditions.IsReleased)));
        ts.Add((Func<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Transition.INeedTarget, BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Transition.IConfigured>) (t => (BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Transition.IConfigured) t.To<POLandedCostDocStatus.balanced>().IsTriggeredOn((Expression<Func<POLandedCostDocEntry, PXAction<POLandedCostDoc>>>) (g => g.initializeState))));
      }));
      transitions.AddGroupFrom<POLandedCostDocStatus.hold>((Action<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Transition.INeedTarget, BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Transition.IConfigured>) (t => (BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Transition.IConfigured) t.To<POLandedCostDocStatus.balanced>().IsTriggeredOn((Expression<Func<POLandedCostDocEntry, PXAction<POLandedCostDoc>>>) (g => g.releaseFromHold))))));
      transitions.AddGroupFrom<POLandedCostDocStatus.balanced>((Action<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Transition.INeedTarget, BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Transition.IConfigured>) (t => (BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Transition.IConfigured) t.To<POLandedCostDocStatus.hold>().IsTriggeredOn((Expression<Func<POLandedCostDocEntry, PXAction<POLandedCostDoc>>>) (g => g.putOnHold)).When((BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Transition.INeedTarget, BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Transition.IConfigured>) (t => (BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Transition.IConfigured) t.To<POLandedCostDocStatus.released>().IsTriggeredOn((Expression<Func<POLandedCostDocEntry, PXWorkflowEventHandlerBase<POLandedCostDoc>>>) (g => g.OnInventoryAdjustmentCreated)).WithFieldAssignments((Action<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Assignment.IContainerFillerFields>) (fields => fields.Add<POLandedCostDoc.released>(new bool?(true))))));
      }));
    })))))).WithActions((Action<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<POLandedCostDocEntry, PXAction<POLandedCostDoc>>>) (g => g.initializeState), (Func<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionDefinition.IConfigured>) null);
      actions.Add((Expression<Func<POLandedCostDocEntry, PXAction<POLandedCostDoc>>>) (g => g.releaseFromHold), (Func<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionDefinition.IConfigured>) (c => (BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Assignment.IContainerFillerFields>) (fas => fas.Add<POLandedCostDoc.hold>(new bool?(false))))));
      actions.Add((Expression<Func<POLandedCostDocEntry, PXAction<POLandedCostDoc>>>) (g => g.putOnHold), (Func<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionDefinition.IConfigured>) (c => (BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Assignment.IContainerFillerFields>) (fas => fas.Add<POLandedCostDoc.hold>(new bool?(true))))));
      actions.Add((Expression<Func<POLandedCostDocEntry, PXAction<POLandedCostDoc>>>) (g => g.release), (Func<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionDefinition.IConfigured>) (c => (BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionDefinition.IConfigured) c.WithCategory(processingCategory)));
      actions.Add((Expression<Func<POLandedCostDocEntry, PXAction<POLandedCostDoc>>>) (g => g.createAPInvoice), (Func<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionDefinition.IConfigured>) (c => (BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionDefinition.IConfigured) c.WithCategory(processingCategory)));
    })).WithCategories((Action<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.ActionCategory.IContainerFillerCategories>) (categories => categories.Add(processingCategory))).WithHandlers((Action<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers => handlers.Add((Func<BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<POLandedCostDoc>) ((BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<POLandedCostDoc>) ((BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.WorkflowEventHandlerDefinition.INeedSubscriber<POLandedCostDoc>) handler.WithTargetOf<POLandedCostDoc>().OfEntityEvent<POLandedCostDoc.Events>((Expression<Func<POLandedCostDoc.Events, PXEntityEvent<POLandedCostDoc>>>) (e => e.InventoryAdjustmentCreated))).Is((Expression<Func<POLandedCostDoc, PXWorkflowEventHandler<POLandedCostDoc, POLandedCostDoc>>>) (g => g.OnInventoryAdjustmentCreated))).UsesTargetAsPrimaryEntity()).DisplayName("IN Adjustment Created")))))));

    BoundedTo<POLandedCostDocEntry, POLandedCostDoc>.Condition Bql<T>() where T : IBqlUnary, new()
    {
      return context.Conditions.FromBql<T>();
    }
  }
}
