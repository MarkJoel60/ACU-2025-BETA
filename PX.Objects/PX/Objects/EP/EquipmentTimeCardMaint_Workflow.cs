// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EquipmentTimeCardMaint_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.EP;

public class EquipmentTimeCardMaint_Workflow : PXGraphExtension<EquipmentTimeCardMaint>
{
  public EquipmentTimeCardMaint_Workflow.PXUpdateStatus updateStatus;

  public virtual void Configure(PXScreenConfiguration config)
  {
    EquipmentTimeCardMaint_Workflow.Configure(config.GetScreenConfigurationContext<EquipmentTimeCardMaint, EPEquipmentTimeCard>());
  }

  protected static void Configure(
    WorkflowContext<EquipmentTimeCardMaint, EPEquipmentTimeCard> context)
  {
    BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionCategory.IConfigured>) (category => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    context.AddScreenConfigurationFor((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ScreenConfiguration.IStartConfigScreen, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ScreenConfiguration.IConfigured) ((BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<EPEquipmentTimeCard.status>().AddDefaultFlow((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Workflow.INeedStatesFlow, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Workflow.IConfigured>) (flow => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add<EPEquipmentTimeCardStatusAttribute.onHold>((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.FlowState.INeedAnyFlowStateConfig, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.BaseFlowStep.IConfigured) ((BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.FlowState.INeedAnyFlowStateConfig) flowState.IsInitial()).WithActions((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<EquipmentTimeCardMaint, PXAction<EPEquipmentTimeCard>>>) (g => g.submit), (Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IAllowOptionalConfig, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IConfigured>) (c => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)))))).WithEventHandlers((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<EquipmentTimeCardMaint, PXWorkflowEventHandler<EPEquipmentTimeCard>>>) (g => g.OnUpdateStatus))))));
      fss.Add<EPEquipmentTimeCardStatusAttribute.approved>((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.FlowState.INeedAnyFlowStateConfig, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<EquipmentTimeCardMaint, PXAction<EPEquipmentTimeCard>>>) (g => g.release), (Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IAllowOptionalConfig, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IConfigured>) (c => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<EquipmentTimeCardMaint, PXAction<EPEquipmentTimeCard>>>) (g => g.edit), (Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IAllowOptionalConfig, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IConfigured>) null);
      }))));
      fss.Add<EPEquipmentTimeCardStatusAttribute.released>((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.FlowState.INeedAnyFlowStateConfig, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<EquipmentTimeCardMaint, PXAction<EPEquipmentTimeCard>>>) (g => g.correct), (Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IAllowOptionalConfig, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionState.IConfigured>) null)))));
    })).WithTransitions((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom<EPEquipmentTimeCardStatusAttribute.onHold>((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.INeedTarget, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.IConfigured>) (t => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.IConfigured) t.To<EPEquipmentTimeCardStatusAttribute.approved>().IsTriggeredOn((Expression<Func<EquipmentTimeCardMaint, PXAction<EPEquipmentTimeCard>>>) (g => g.submit))))));
      transitions.AddGroupFrom<EPEquipmentTimeCardStatusAttribute.approved>((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.INeedTarget, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.IConfigured>) (t => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.IConfigured) t.To<EPEquipmentTimeCardStatusAttribute.released>().IsTriggeredOn((Expression<Func<EquipmentTimeCardMaint, PXAction<EPEquipmentTimeCard>>>) (g => g.release))));
        ts.Add((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.INeedTarget, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.IConfigured>) (t => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.IConfigured) t.To<EPEquipmentTimeCardStatusAttribute.onHold>().IsTriggeredOn((Expression<Func<EquipmentTimeCardMaint, PXAction<EPEquipmentTimeCard>>>) (g => g.edit))));
      }));
      transitions.AddGroupFrom<EPEquipmentTimeCardStatusAttribute.released>((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Transition.ISourceContainerFillerTransitions>) (ts => { }));
    })))).WithActions((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<EquipmentTimeCardMaint, PXAction<EPEquipmentTimeCard>>>) (g => g.submit), (Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IConfigured>) (c => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithFieldAssignments((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.IContainerFillerFields>) (fa => fa.Add<EPEquipmentTimeCard.isHold>((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.INeedRightOperand, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.IConfigured>) (f => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<EquipmentTimeCardMaint, PXAction<EPEquipmentTimeCard>>>) (g => g.edit), (Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IConfigured>) (c => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithFieldAssignments((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.IContainerFillerFields>) (fa => fa.Add<EPEquipmentTimeCard.isHold>((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.INeedRightOperand, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.IConfigured>) (f => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<EquipmentTimeCardMaint, PXAction<EPEquipmentTimeCard>>>) (g => g.release), (Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IConfigured>) (c => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
      actions.Add((Expression<Func<EquipmentTimeCardMaint, PXAction<EPEquipmentTimeCard>>>) (g => g.correct), (Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IConfigured>) (c => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionDefinition.IConfigured) c.InFolder((FolderType) 0)));
    })).WithHandlers((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers => handlers.Add((Func<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<EPEquipmentTimeCard>) ((BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.WorkflowEventHandlerDefinition.INeedSubscriber<EPEquipmentTimeCard>) handler.WithTargetOf<EPEquipmentTimeCard>().OfEntityEvent<EPEquipmentTimeCard.Events>((Expression<Func<EPEquipmentTimeCard.Events, PXEntityEvent<EPEquipmentTimeCard>>>) (e => e.UpdateStatus))).Is((Expression<Func<EPEquipmentTimeCard, PXWorkflowEventHandler<EPEquipmentTimeCard, EPEquipmentTimeCard>>>) (g => g.OnUpdateStatus))).UsesTargetAsPrimaryEntity())))).WithCategories((Action<BoundedTo<EquipmentTimeCardMaint, EPEquipmentTimeCard>.ActionCategory.IContainerFillerCategories>) (categories => categories.Add(processingCategory)))));
  }

  public class PXUpdateStatus : PXSelect<EPEquipmentTimeCard>
  {
    public PXUpdateStatus(PXGraph graph)
      : base(graph)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      graph.Initialized += EquipmentTimeCardMaint_Workflow.PXUpdateStatus.\u003C\u003Ec.\u003C\u003E9__0_0 ?? (EquipmentTimeCardMaint_Workflow.PXUpdateStatus.\u003C\u003Ec.\u003C\u003E9__0_0 = new PXGraphInitializedDelegate((object) EquipmentTimeCardMaint_Workflow.PXUpdateStatus.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__0_0)));
    }
  }
}
