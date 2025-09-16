// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CashTransferEntry_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CA;

public class CashTransferEntry_Workflow : PXGraphExtension<CashTransferEntry>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    CashTransferEntry_Workflow.Configure(config.GetScreenConfigurationContext<CashTransferEntry, CATransfer>());
  }

  protected static void Configure(
    WorkflowContext<CashTransferEntry, CATransfer> context)
  {
    CashTransferEntry_Workflow.Conditions conditions = context.Conditions.GetPack<CashTransferEntry_Workflow.Conditions>();
    BoundedTo<CashTransferEntry, CATransfer>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("ProcessingID", (Func<BoundedTo<CashTransferEntry, CATransfer>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CashTransferEntry, CATransfer>.ActionCategory.IConfigured>) (category => (BoundedTo<CashTransferEntry, CATransfer>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<CashTransferEntry, CATransfer>.ActionCategory.IConfigured correctionsCategory = context.Categories.CreateNew("CorrectionsID", (Func<BoundedTo<CashTransferEntry, CATransfer>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CashTransferEntry, CATransfer>.ActionCategory.IConfigured>) (category => (BoundedTo<CashTransferEntry, CATransfer>.ActionCategory.IConfigured) category.DisplayName("Corrections")));
    context.AddScreenConfigurationFor((Func<BoundedTo<CashTransferEntry, CATransfer>.ScreenConfiguration.IStartConfigScreen, BoundedTo<CashTransferEntry, CATransfer>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<CashTransferEntry, CATransfer>.ScreenConfiguration.IConfigured) ((BoundedTo<CashTransferEntry, CATransfer>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<CATransfer.status>().AddDefaultFlow((Func<BoundedTo<CashTransferEntry, CATransfer>.Workflow.INeedStatesFlow, BoundedTo<CashTransferEntry, CATransfer>.Workflow.IConfigured>) (flow => (BoundedTo<CashTransferEntry, CATransfer>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<CashTransferEntry, CATransfer>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<CashTransferEntry, CATransfer>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CashTransferEntry, CATransfer>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CashTransferEntry, CATransfer>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<CashTransferEntry, PXAutoAction<CATransfer>>>) (g => g.initializeState))));
      fss.Add<CATransferStatus.hold>((Func<BoundedTo<CashTransferEntry, CATransfer>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CashTransferEntry, CATransfer>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CashTransferEntry, CATransfer>.BaseFlowStep.IConfigured) ((BoundedTo<CashTransferEntry, CATransfer>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<CashTransferEntry, CATransfer>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<CashTransferEntry, PXAction<CATransfer>>>) (g => g.releaseFromHold), (Func<BoundedTo<CashTransferEntry, CATransfer>.ActionState.IAllowOptionalConfig, BoundedTo<CashTransferEntry, CATransfer>.ActionState.IConfigured>) (a => (BoundedTo<CashTransferEntry, CATransfer>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)))))).WithEventHandlers((Action<BoundedTo<CashTransferEntry, CATransfer>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<CashTransferEntry, PXWorkflowEventHandler<CATransfer>>>) (g => g.OnUpdateStatus))))));
      fss.Add<CATransferStatus.balanced>((Func<BoundedTo<CashTransferEntry, CATransfer>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CashTransferEntry, CATransfer>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CashTransferEntry, CATransfer>.BaseFlowStep.IConfigured) ((BoundedTo<CashTransferEntry, CATransfer>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<CashTransferEntry, CATransfer>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<CashTransferEntry, PXAction<CATransfer>>>) (g => g.Release), (Func<BoundedTo<CashTransferEntry, CATransfer>.ActionState.IAllowOptionalConfig, BoundedTo<CashTransferEntry, CATransfer>.ActionState.IConfigured>) (a => (BoundedTo<CashTransferEntry, CATransfer>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<CashTransferEntry, PXAction<CATransfer>>>) (g => g.putOnHold), (Func<BoundedTo<CashTransferEntry, CATransfer>.ActionState.IAllowOptionalConfig, BoundedTo<CashTransferEntry, CATransfer>.ActionState.IConfigured>) (a => (BoundedTo<CashTransferEntry, CATransfer>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
      }))).WithEventHandlers((Action<BoundedTo<CashTransferEntry, CATransfer>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<CashTransferEntry, PXWorkflowEventHandler<CATransfer>>>) (g => g.OnReleaseDocument));
        handlers.Add((Expression<Func<CashTransferEntry, PXWorkflowEventHandler<CATransfer>>>) (g => g.OnUpdateStatus));
      }))));
      fss.Add<CATransferStatus.released>((Func<BoundedTo<CashTransferEntry, CATransfer>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CashTransferEntry, CATransfer>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CashTransferEntry, CATransfer>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<CashTransferEntry, CATransfer>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<CashTransferEntry, PXAction<CATransfer>>>) (g => g.Reverse), (Func<BoundedTo<CashTransferEntry, CATransfer>.ActionState.IAllowOptionalConfig, BoundedTo<CashTransferEntry, CATransfer>.ActionState.IConfigured>) (a => (BoundedTo<CashTransferEntry, CATransfer>.ActionState.IConfigured) a.IsDuplicatedInToolbar()))))));
    })).WithTransitions((Action<BoundedTo<CashTransferEntry, CATransfer>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<CashTransferEntry, CATransfer>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<CashTransferEntry, CATransfer>.Transition.INeedTarget, BoundedTo<CashTransferEntry, CATransfer>.Transition.IConfigured>) (t => (BoundedTo<CashTransferEntry, CATransfer>.Transition.IConfigured) t.To<CATransferStatus.hold>().IsTriggeredOn((Expression<Func<CashTransferEntry, PXAction<CATransfer>>>) (g => g.initializeState)).When((BoundedTo<CashTransferEntry, CATransfer>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<CashTransferEntry, CATransfer>.Transition.INeedTarget, BoundedTo<CashTransferEntry, CATransfer>.Transition.IConfigured>) (t => (BoundedTo<CashTransferEntry, CATransfer>.Transition.IConfigured) t.To<CATransferStatus.balanced>().IsTriggeredOn((Expression<Func<CashTransferEntry, PXAction<CATransfer>>>) (g => g.initializeState)).When((BoundedTo<CashTransferEntry, CATransfer>.ISharedCondition) conditions.IsNotOnHold)));
      }));
      transitions.AddGroupFrom<CATransferStatus.hold>((Action<BoundedTo<CashTransferEntry, CATransfer>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<CashTransferEntry, CATransfer>.Transition.INeedTarget, BoundedTo<CashTransferEntry, CATransfer>.Transition.IConfigured>) (t => (BoundedTo<CashTransferEntry, CATransfer>.Transition.IConfigured) t.To<CATransferStatus.balanced>().IsTriggeredOn((Expression<Func<CashTransferEntry, PXAction<CATransfer>>>) (g => g.releaseFromHold)).WithFieldAssignments((Action<BoundedTo<CashTransferEntry, CATransfer>.Assignment.IContainerFillerFields>) (fas => fas.Add<CATransfer.hold>((Func<BoundedTo<CashTransferEntry, CATransfer>.Assignment.INeedRightOperand, BoundedTo<CashTransferEntry, CATransfer>.Assignment.IConfigured>) (f => (BoundedTo<CashTransferEntry, CATransfer>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
        ts.Add((Func<BoundedTo<CashTransferEntry, CATransfer>.Transition.INeedTarget, BoundedTo<CashTransferEntry, CATransfer>.Transition.IConfigured>) (t => (BoundedTo<CashTransferEntry, CATransfer>.Transition.IConfigured) t.To<CATransferStatus.balanced>().IsTriggeredOn((Expression<Func<CashTransferEntry, PXWorkflowEventHandlerBase<CATransfer>>>) (g => g.OnUpdateStatus)).When((BoundedTo<CashTransferEntry, CATransfer>.ISharedCondition) conditions.IsNotOnHold)));
      }));
      transitions.AddGroupFrom<CATransferStatus.balanced>((Action<BoundedTo<CashTransferEntry, CATransfer>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<CashTransferEntry, CATransfer>.Transition.INeedTarget, BoundedTo<CashTransferEntry, CATransfer>.Transition.IConfigured>) (t => (BoundedTo<CashTransferEntry, CATransfer>.Transition.IConfigured) t.To<CATransferStatus.hold>().IsTriggeredOn((Expression<Func<CashTransferEntry, PXAction<CATransfer>>>) (g => g.putOnHold)).WithFieldAssignments((Action<BoundedTo<CashTransferEntry, CATransfer>.Assignment.IContainerFillerFields>) (fas => fas.Add<CATransfer.hold>((Func<BoundedTo<CashTransferEntry, CATransfer>.Assignment.INeedRightOperand, BoundedTo<CashTransferEntry, CATransfer>.Assignment.IConfigured>) (f => (BoundedTo<CashTransferEntry, CATransfer>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        ts.Add((Func<BoundedTo<CashTransferEntry, CATransfer>.Transition.INeedTarget, BoundedTo<CashTransferEntry, CATransfer>.Transition.IConfigured>) (t => (BoundedTo<CashTransferEntry, CATransfer>.Transition.IConfigured) t.To<CATransferStatus.released>().IsTriggeredOn((Expression<Func<CashTransferEntry, PXWorkflowEventHandlerBase<CATransfer>>>) (g => g.OnReleaseDocument))));
        ts.Add((Func<BoundedTo<CashTransferEntry, CATransfer>.Transition.INeedTarget, BoundedTo<CashTransferEntry, CATransfer>.Transition.IConfigured>) (t => (BoundedTo<CashTransferEntry, CATransfer>.Transition.IConfigured) t.To<CATransferStatus.hold>().IsTriggeredOn((Expression<Func<CashTransferEntry, PXWorkflowEventHandlerBase<CATransfer>>>) (g => g.OnUpdateStatus)).When((BoundedTo<CashTransferEntry, CATransfer>.ISharedCondition) conditions.IsOnHold)));
      }));
    })))).WithActions((Action<BoundedTo<CashTransferEntry, CATransfer>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<CashTransferEntry, PXAction<CATransfer>>>) (g => g.initializeState), (Func<BoundedTo<CashTransferEntry, CATransfer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CashTransferEntry, CATransfer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CashTransferEntry, CATransfer>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<CashTransferEntry, PXAction<CATransfer>>>) (g => g.putOnHold), (Func<BoundedTo<CashTransferEntry, CATransfer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CashTransferEntry, CATransfer>.ActionDefinition.IConfigured>) (c => (BoundedTo<CashTransferEntry, CATransfer>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<CashTransferEntry, CATransfer>.Assignment.IContainerFillerFields>) (fas => fas.Add<CATransfer.hold>((Func<BoundedTo<CashTransferEntry, CATransfer>.Assignment.INeedRightOperand, BoundedTo<CashTransferEntry, CATransfer>.Assignment.IConfigured>) (f => (BoundedTo<CashTransferEntry, CATransfer>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<CashTransferEntry, PXAction<CATransfer>>>) (g => g.releaseFromHold), (Func<BoundedTo<CashTransferEntry, CATransfer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CashTransferEntry, CATransfer>.ActionDefinition.IConfigured>) (c => (BoundedTo<CashTransferEntry, CATransfer>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<CashTransferEntry, CATransfer>.Assignment.IContainerFillerFields>) (fas => fas.Add<CATransfer.hold>((Func<BoundedTo<CashTransferEntry, CATransfer>.Assignment.INeedRightOperand, BoundedTo<CashTransferEntry, CATransfer>.Assignment.IConfigured>) (f => (BoundedTo<CashTransferEntry, CATransfer>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<CashTransferEntry, PXAction<CATransfer>>>) (g => g.Release), (Func<BoundedTo<CashTransferEntry, CATransfer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CashTransferEntry, CATransfer>.ActionDefinition.IConfigured>) (c => (BoundedTo<CashTransferEntry, CATransfer>.ActionDefinition.IConfigured) c.InFolder(processingCategory).PlaceAfter("Last")));
      actions.Add((Expression<Func<CashTransferEntry, PXAction<CATransfer>>>) (g => g.Reverse), (Func<BoundedTo<CashTransferEntry, CATransfer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CashTransferEntry, CATransfer>.ActionDefinition.IConfigured>) (c => (BoundedTo<CashTransferEntry, CATransfer>.ActionDefinition.IConfigured) c.InFolder(correctionsCategory)));
    })).WithHandlers((Action<BoundedTo<CashTransferEntry, CATransfer>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers =>
    {
      handlers.Add((Func<BoundedTo<CashTransferEntry, CATransfer>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<CashTransferEntry, CATransfer>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<CashTransferEntry, CATransfer>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<CashTransferEntry, CATransfer>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<CATransfer>) ((BoundedTo<CashTransferEntry, CATransfer>.WorkflowEventHandlerDefinition.INeedSubscriber<CATransfer>) handler.WithTargetOf<CATransfer>().OfEntityEvent<CATransfer.Events>((Expression<Func<CATransfer.Events, PXEntityEvent<CATransfer>>>) (e => e.ReleaseDocument))).Is((Expression<Func<CATransfer, PXWorkflowEventHandler<CATransfer, CATransfer>>>) (g => g.OnReleaseDocument))).UsesTargetAsPrimaryEntity()));
      handlers.Add((Func<BoundedTo<CashTransferEntry, CATransfer>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<CashTransferEntry, CATransfer>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<CashTransferEntry, CATransfer>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<CashTransferEntry, CATransfer>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<CATransfer>) ((BoundedTo<CashTransferEntry, CATransfer>.WorkflowEventHandlerDefinition.INeedSubscriber<CATransfer>) handler.WithTargetOf<CATransfer>().OfFieldUpdated<CATransfer.hold>()).Is((Expression<Func<CATransfer, PXWorkflowEventHandler<CATransfer, CATransfer>>>) (g => g.OnUpdateStatus))).UsesTargetAsPrimaryEntity()));
    })).WithCategories((Action<BoundedTo<CashTransferEntry, CATransfer>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(correctionsCategory);
    }))));
  }

  public class Conditions : BoundedTo<CashTransferEntry, CATransfer>.Condition.Pack
  {
    public BoundedTo<CashTransferEntry, CATransfer>.Condition IsOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CashTransferEntry, CATransfer>.Condition.ConditionBuilder, BoundedTo<CashTransferEntry, CATransfer>.Condition>) (c => c.FromBql<BqlOperand<CATransfer.hold, IBqlBool>.IsEqual<True>>()), nameof (IsOnHold));
      }
    }

    public BoundedTo<CashTransferEntry, CATransfer>.Condition IsNotOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CashTransferEntry, CATransfer>.Condition.ConditionBuilder, BoundedTo<CashTransferEntry, CATransfer>.Condition>) (c => c.FromBql<BqlOperand<CATransfer.hold, IBqlBool>.IsEqual<False>>()), nameof (IsNotOnHold));
      }
    }
  }

  public static class CategoryNames
  {
    public const string Processing = "Processing";
    public const string Corrections = "Corrections";
  }

  public static class CategoryID
  {
    public const string Processing = "ProcessingID";
    public const string Corrections = "CorrectionsID";
  }
}
