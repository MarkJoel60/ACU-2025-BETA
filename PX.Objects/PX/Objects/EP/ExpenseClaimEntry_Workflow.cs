// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ExpenseClaimEntry_Workflow
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

public class ExpenseClaimEntry_Workflow : PXGraphExtension<ExpenseClaimEntry>
{
  public ExpenseClaimEntry_Workflow.PXUpdateStatus updateStatus;

  public sealed override void Configure(PXScreenConfiguration config)
  {
    ExpenseClaimEntry_Workflow.Configure(config.GetScreenConfigurationContext<ExpenseClaimEntry, EPExpenseClaim>());
  }

  protected static void Configure(
    WorkflowContext<ExpenseClaimEntry, EPExpenseClaim> context)
  {
    BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionCategory.IConfigured>) (category => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionCategory.IConfigured printingAndEmailingCategory = context.Categories.CreateNew("Printing and Emailing", (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionCategory.IConfigured>) (category => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionCategory.IConfigured) category.DisplayName("Printing and Emailing")));
    context.AddScreenConfigurationFor((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ScreenConfiguration.IStartConfigScreen, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ScreenConfiguration.IConfigured) screen.StateIdentifierIs<EPExpenseClaim.status>().AddDefaultFlow((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Workflow.INeedStatesFlow, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Workflow.IConfigured>) (flow => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add<EPExpenseClaimStatus.holdStatus>((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.BaseFlowStep.IConfigured) flowState.IsInitial().WithActions((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.submit), (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IAllowOptionalConfig, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IConfigured>) (c => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success)));
        actions.Add((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.expenseClaimPrint), (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IAllowOptionalConfig, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IConfigured>) (c => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
      })).WithEventHandlers((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<ExpenseClaimEntry, PXWorkflowEventHandler<EPExpenseClaim>>>) (g => g.OnSubmit));
        handlers.Add((Expression<Func<ExpenseClaimEntry, PXWorkflowEventHandler<EPExpenseClaim>>>) (g => g.OnUpdateStatus));
      }))));
      fss.Add<EPExpenseClaimStatus.approvedStatus>((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.release), (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IAllowOptionalConfig, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IConfigured>) (c => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation(ActionConnotation.Success)));
        actions.Add((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.edit));
        actions.Add((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.expenseClaimPrint), (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IAllowOptionalConfig, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IConfigured>) (c => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
      }))));
      fss.Add<EPExpenseClaimStatus.releasedStatus>((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.expenseClaimPrint), (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IAllowOptionalConfig, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IConfigured>) (c => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionState.IConfigured) c.IsDuplicatedInToolbar()))))));
    })).WithTransitions((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom<EPExpenseClaimStatus.holdStatus>((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.INeedTarget, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured) t.To<EPExpenseClaimStatus.approvedStatus>().IsTriggeredOn((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.submit))));
        ts.Add((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.INeedTarget, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured) t.To<EPExpenseClaimStatus.approvedStatus>().IsTriggeredOn((Expression<Func<ExpenseClaimEntry, PXWorkflowEventHandlerBase<EPExpenseClaim>>>) (g => g.OnSubmit))));
      }));
      transitions.AddGroupFrom<EPExpenseClaimStatus.approvedStatus>((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.INeedTarget, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured) t.To<EPExpenseClaimStatus.releasedStatus>().IsTriggeredOn((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.release))));
        ts.Add((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.INeedTarget, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.IConfigured) t.To<EPExpenseClaimStatus.holdStatus>().IsTriggeredOn((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.edit))));
      }));
      transitions.AddGroupFrom<EPExpenseClaimStatus.releasedStatus>((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Transition.ISourceContainerFillerTransitions>) (ts => { }));
    })))).WithActions((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.submit), (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IConfigured>) (c => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
      actions.Add((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.edit), (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IConfigured>) (c => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithFieldAssignments((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.IContainerFillerFields>) (fa => fa.Add<EPExpenseClaim.hold>((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.INeedRightOperand, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.IConfigured>) (f => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.release), (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IConfigured>) (c => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
      actions.Add((Expression<Func<ExpenseClaimEntry, PXAction<EPExpenseClaim>>>) (g => g.expenseClaimPrint), (Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IConfigured>) (c => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionDefinition.IConfigured) c.InFolder(printingAndEmailingCategory)));
    })).WithHandlers((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers =>
    {
      handlers.Add((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) handler.WithTargetOf<EPExpenseClaim>().OfEntityEvent<EPExpenseClaim.Events>((Expression<Func<EPExpenseClaim.Events, PXEntityEvent<EPExpenseClaim>>>) (e => e.Submit)).Is((Expression<Func<ExpenseClaimEntry, PXWorkflowEventHandler<EPExpenseClaim, EPExpenseClaim>>>) (g => g.OnSubmit)).UsesTargetAsPrimaryEntity().WithFieldAssignments((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.IContainerFillerFields>) (fa => fa.Add<EPExpenseClaim.hold>((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.INeedRightOperand, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.IConfigured>) (f => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      handlers.Add((Func<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) handler.WithTargetOf<EPExpenseClaim>().OfEntityEvent<EPExpenseClaim.Events>((Expression<Func<EPExpenseClaim.Events, PXEntityEvent<EPExpenseClaim>>>) (e => e.UpdateStatus)).Is((Expression<Func<ExpenseClaimEntry, PXWorkflowEventHandler<EPExpenseClaim, EPExpenseClaim>>>) (g => g.OnUpdateStatus)).UsesTargetAsPrimaryEntity()));
    })).WithCategories((Action<BoundedTo<ExpenseClaimEntry, EPExpenseClaim>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(printingAndEmailingCategory);
    }))));
  }

  public class PXUpdateStatus : PXSelect<EPExpenseClaim>
  {
    public PXUpdateStatus(PXGraph graph)
      : base(graph)
    {
      graph.Initialized += (PXGraphInitializedDelegate) (g => g.RowUpdated.AddHandler<EPExpenseClaim>((PXRowUpdated) ((sender, e) =>
      {
        if (sender.ObjectsEqual<EPExpenseClaim.rejected>(e.Row, e.OldRow))
          return;
        PXEntityEventBase<EPExpenseClaim>.Container<EPExpenseClaim.Events>.Select((Expression<Func<EPExpenseClaim.Events, PXEntityEvent<EPExpenseClaim>>>) (ev => ev.UpdateStatus)).FireOn(g, (EPExpenseClaim) e.Row);
      })));
    }
  }
}
