// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.TimeCardMaint_Workflow
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

public class TimeCardMaint_Workflow : PXGraphExtension<TimeCardMaint>
{
  public TimeCardMaint_Workflow.PXUpdateStatus updateStatus;

  public sealed override void Configure(PXScreenConfiguration config)
  {
    TimeCardMaint_Workflow.Configure(config.GetScreenConfigurationContext<TimeCardMaint, EPTimeCard>());
  }

  protected static void Configure(WorkflowContext<TimeCardMaint, EPTimeCard> context)
  {
    BoundedTo<TimeCardMaint, EPTimeCard>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<TimeCardMaint, EPTimeCard>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<TimeCardMaint, EPTimeCard>.ActionCategory.IConfigured>) (category => (BoundedTo<TimeCardMaint, EPTimeCard>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<TimeCardMaint, EPTimeCard>.ActionCategory.IConfigured correctionsCategory = context.Categories.CreateNew("Corrections", (Func<BoundedTo<TimeCardMaint, EPTimeCard>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<TimeCardMaint, EPTimeCard>.ActionCategory.IConfigured>) (category => (BoundedTo<TimeCardMaint, EPTimeCard>.ActionCategory.IConfigured) category.DisplayName("Corrections")));
    context.AddScreenConfigurationFor((Func<BoundedTo<TimeCardMaint, EPTimeCard>.ScreenConfiguration.IStartConfigScreen, BoundedTo<TimeCardMaint, EPTimeCard>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<TimeCardMaint, EPTimeCard>.ScreenConfiguration.IConfigured) screen.StateIdentifierIs<EPTimeCard.status>().AddDefaultFlow((Func<BoundedTo<TimeCardMaint, EPTimeCard>.Workflow.INeedStatesFlow, BoundedTo<TimeCardMaint, EPTimeCard>.Workflow.IConfigured>) (flow => (BoundedTo<TimeCardMaint, EPTimeCard>.Workflow.IConfigured) flow.WithFlowStates((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add<EPTimeCardStatusAttribute.holdStatus>((Func<BoundedTo<TimeCardMaint, EPTimeCard>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TimeCardMaint, EPTimeCard>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<TimeCardMaint, EPTimeCard>.BaseFlowStep.IConfigured) flowState.IsInitial().WithActions((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<TimeCardMaint, PXAction<EPTimeCard>>>) (g => g.submit), (Func<BoundedTo<TimeCardMaint, EPTimeCard>.ActionState.IAllowOptionalConfig, BoundedTo<TimeCardMaint, EPTimeCard>.ActionState.IConfigured>) (c => (BoundedTo<TimeCardMaint, EPTimeCard>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success))))).WithEventHandlers((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<TimeCardMaint, PXWorkflowEventHandler<EPTimeCard>>>) (g => g.OnUpdateStatus))))));
      fss.Add<EPTimeCardStatusAttribute.approvedStatus>((Func<BoundedTo<TimeCardMaint, EPTimeCard>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TimeCardMaint, EPTimeCard>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<TimeCardMaint, EPTimeCard>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<TimeCardMaint, PXAction<EPTimeCard>>>) (g => g.release), (Func<BoundedTo<TimeCardMaint, EPTimeCard>.ActionState.IAllowOptionalConfig, BoundedTo<TimeCardMaint, EPTimeCard>.ActionState.IConfigured>) (c => (BoundedTo<TimeCardMaint, EPTimeCard>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success)));
        actions.Add((Expression<Func<TimeCardMaint, PXAction<EPTimeCard>>>) (g => g.edit));
      }))));
      fss.Add<EPTimeCardStatusAttribute.releasedStatus>((Func<BoundedTo<TimeCardMaint, EPTimeCard>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TimeCardMaint, EPTimeCard>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<TimeCardMaint, EPTimeCard>.BaseFlowStep.IConfigured) flowState.WithActions((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<TimeCardMaint, PXAction<EPTimeCard>>>) (g => g.correct))))));
    })).WithTransitions((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom<EPTimeCardStatusAttribute.holdStatus>((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<TimeCardMaint, EPTimeCard>.Transition.INeedTarget, BoundedTo<TimeCardMaint, EPTimeCard>.Transition.IConfigured>) (t => (BoundedTo<TimeCardMaint, EPTimeCard>.Transition.IConfigured) t.To<EPTimeCardStatusAttribute.approvedStatus>().IsTriggeredOn((Expression<Func<TimeCardMaint, PXAction<EPTimeCard>>>) (g => g.submit))))));
      transitions.AddGroupFrom<EPTimeCardStatusAttribute.approvedStatus>((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<TimeCardMaint, EPTimeCard>.Transition.INeedTarget, BoundedTo<TimeCardMaint, EPTimeCard>.Transition.IConfigured>) (t => (BoundedTo<TimeCardMaint, EPTimeCard>.Transition.IConfigured) t.To<EPTimeCardStatusAttribute.releasedStatus>().IsTriggeredOn((Expression<Func<TimeCardMaint, PXAction<EPTimeCard>>>) (g => g.release))));
        ts.Add((Func<BoundedTo<TimeCardMaint, EPTimeCard>.Transition.INeedTarget, BoundedTo<TimeCardMaint, EPTimeCard>.Transition.IConfigured>) (t => (BoundedTo<TimeCardMaint, EPTimeCard>.Transition.IConfigured) t.To<EPTimeCardStatusAttribute.holdStatus>().IsTriggeredOn((Expression<Func<TimeCardMaint, PXAction<EPTimeCard>>>) (g => g.edit))));
      }));
      transitions.AddGroupFrom<EPTimeCardStatusAttribute.releasedStatus>((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.Transition.ISourceContainerFillerTransitions>) (ts => { }));
    })))).WithActions((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<TimeCardMaint, PXAction<EPTimeCard>>>) (g => g.correct), (Func<BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IConfigured>) (c => (BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IConfigured) c.InFolder(correctionsCategory)));
      actions.Add((Expression<Func<TimeCardMaint, PXAction<EPTimeCard>>>) (g => g.submit), (Func<BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IConfigured>) (c => (BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
      actions.Add((Expression<Func<TimeCardMaint, PXAction<EPTimeCard>>>) (g => g.edit), (Func<BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IConfigured>) (c => (BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithFieldAssignments((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.Assignment.IContainerFillerFields>) (fa => fa.Add<EPTimeCard.isHold>((Func<BoundedTo<TimeCardMaint, EPTimeCard>.Assignment.INeedRightOperand, BoundedTo<TimeCardMaint, EPTimeCard>.Assignment.IConfigured>) (f => (BoundedTo<TimeCardMaint, EPTimeCard>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<TimeCardMaint, PXAction<EPTimeCard>>>) (g => g.release), (Func<BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IConfigured>) (c => (BoundedTo<TimeCardMaint, EPTimeCard>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
    })).WithHandlers((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers => handlers.Add((Func<BoundedTo<TimeCardMaint, EPTimeCard>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<TimeCardMaint, EPTimeCard>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<TimeCardMaint, EPTimeCard>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) handler.WithTargetOf<EPTimeCard>().OfEntityEvent<EPTimeCard.Events>((Expression<Func<EPTimeCard.Events, PXEntityEvent<EPTimeCard>>>) (e => e.UpdateStatus)).Is((Expression<Func<TimeCardMaint, PXWorkflowEventHandler<EPTimeCard, EPTimeCard>>>) (g => g.OnUpdateStatus)).UsesTargetAsPrimaryEntity())))).WithCategories((System.Action<BoundedTo<TimeCardMaint, EPTimeCard>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(correctionsCategory);
    }))));
  }

  public class PXUpdateStatus : PXSelect<EPTimeCard>
  {
    public PXUpdateStatus(PXGraph graph)
      : base(graph)
    {
      graph.Initialized += (PXGraphInitializedDelegate) (g => g.RowUpdated.AddHandler<EPTimeCard>((PXRowUpdated) ((sender, e) =>
      {
        if (sender.ObjectsEqual<EPTimeCard.isRejected>(e.Row, e.OldRow))
          return;
        PXEntityEventBase<EPTimeCard>.Container<EPTimeCard.Events>.Select((Expression<Func<EPTimeCard.Events, PXEntityEvent<EPTimeCard>>>) (ev => ev.UpdateStatus)).FireOn(g, (EPTimeCard) e.Row);
      })));
    }
  }
}
