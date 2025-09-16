// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.JournalWithSubEntry_Workflow
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
namespace PX.Objects.GL;

public class JournalWithSubEntry_Workflow : PXGraphExtension<JournalWithSubEntry>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    JournalWithSubEntry_Workflow.Configure(config.GetScreenConfigurationContext<JournalWithSubEntry, GLDocBatch>());
  }

  protected static void Configure(
    WorkflowContext<JournalWithSubEntry, GLDocBatch> context)
  {
    JournalWithSubEntry_Workflow.Conditions conditions = context.Conditions.GetPack<JournalWithSubEntry_Workflow.Conditions>();
    BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionCategory.IConfigured>) (category => (BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    context.AddScreenConfigurationFor((Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.ScreenConfiguration.IStartConfigScreen, BoundedTo<JournalWithSubEntry, GLDocBatch>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<JournalWithSubEntry, GLDocBatch>.ScreenConfiguration.IConfigured) ((BoundedTo<JournalWithSubEntry, GLDocBatch>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<GLDocBatch.status>().AddDefaultFlow((Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.Workflow.INeedStatesFlow, BoundedTo<JournalWithSubEntry, GLDocBatch>.Workflow.IConfigured>) (flow => (BoundedTo<JournalWithSubEntry, GLDocBatch>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<JournalWithSubEntry, GLDocBatch>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.FlowState.INeedAnyFlowStateConfig, BoundedTo<JournalWithSubEntry, GLDocBatch>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<JournalWithSubEntry, GLDocBatch>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<JournalWithSubEntry, PXAutoAction<GLDocBatch>>>) (g => g.initializeState))));
      fss.Add<GLDocBatchStatus.hold>((Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.FlowState.INeedAnyFlowStateConfig, BoundedTo<JournalWithSubEntry, GLDocBatch>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<JournalWithSubEntry, GLDocBatch>.BaseFlowStep.IConfigured) ((BoundedTo<JournalWithSubEntry, GLDocBatch>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<JournalWithSubEntry, PXAction<GLDocBatch>>>) (g => g.releaseFromHold), (Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionState.IConfigured>) (a => (BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)))))).WithEventHandlers((Action<BoundedTo<JournalWithSubEntry, GLDocBatch>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<JournalWithSubEntry, PXWorkflowEventHandler<GLDocBatch>>>) (g => g.OnUpdateStatus))))));
      fss.Add<GLDocBatchStatus.balanced>((Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.FlowState.INeedAnyFlowStateConfig, BoundedTo<JournalWithSubEntry, GLDocBatch>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<JournalWithSubEntry, GLDocBatch>.BaseFlowStep.IConfigured) ((BoundedTo<JournalWithSubEntry, GLDocBatch>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<JournalWithSubEntry, PXAction<GLDocBatch>>>) (g => g.release), (Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionState.IConfigured>) (a => (BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<JournalWithSubEntry, PXAction<GLDocBatch>>>) (g => g.putOnHold), (Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionState.IConfigured>) (a => (BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
      }))).WithEventHandlers((Action<BoundedTo<JournalWithSubEntry, GLDocBatch>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<JournalWithSubEntry, PXWorkflowEventHandler<GLDocBatch>>>) (g => g.OnUpdateStatus));
        handlers.Add((Expression<Func<JournalWithSubEntry, PXWorkflowEventHandler<GLDocBatch>>>) (g => g.OnReleaseVoucher));
      }))));
      fss.Add<GLDocBatchStatus.completed>((Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.FlowState.INeedAnyFlowStateConfig, BoundedTo<JournalWithSubEntry, GLDocBatch>.BaseFlowStep.IConfigured>) null);
      fss.Add<GLDocBatchStatus.released>((Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.FlowState.INeedAnyFlowStateConfig, BoundedTo<JournalWithSubEntry, GLDocBatch>.BaseFlowStep.IConfigured>) null);
    })).WithTransitions((Action<BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.INeedTarget, BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.IConfigured>) (t => (BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.IConfigured) t.To<GLDocBatchStatus.hold>().IsTriggeredOn((Expression<Func<JournalWithSubEntry, PXAction<GLDocBatch>>>) (g => g.initializeState)).When((BoundedTo<JournalWithSubEntry, GLDocBatch>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.INeedTarget, BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.IConfigured>) (t => (BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.IConfigured) t.To<GLDocBatchStatus.balanced>().IsTriggeredOn((Expression<Func<JournalWithSubEntry, PXAction<GLDocBatch>>>) (g => g.initializeState)).When((BoundedTo<JournalWithSubEntry, GLDocBatch>.ISharedCondition) conditions.IsNotOnHold)));
      }));
      transitions.AddGroupFrom<GLDocBatchStatus.hold>((Action<BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.INeedTarget, BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.IConfigured>) (t => (BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.IConfigured) t.To<GLDocBatchStatus.balanced>().IsTriggeredOn((Expression<Func<JournalWithSubEntry, PXAction<GLDocBatch>>>) (g => g.releaseFromHold)).DoesNotPersist().WithFieldAssignments((Action<BoundedTo<JournalWithSubEntry, GLDocBatch>.Assignment.IContainerFillerFields>) (fas => fas.Add<GLDocBatch.hold>((Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.Assignment.INeedRightOperand, BoundedTo<JournalWithSubEntry, GLDocBatch>.Assignment.IConfigured>) (f => (BoundedTo<JournalWithSubEntry, GLDocBatch>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
        ts.Add((Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.INeedTarget, BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.IConfigured>) (t => (BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.IConfigured) t.To<GLDocBatchStatus.balanced>().IsTriggeredOn((Expression<Func<JournalWithSubEntry, PXWorkflowEventHandlerBase<GLDocBatch>>>) (g => g.OnUpdateStatus)).When((BoundedTo<JournalWithSubEntry, GLDocBatch>.ISharedCondition) conditions.IsNotOnHold)));
      }));
      transitions.AddGroupFrom<GLDocBatchStatus.balanced>((Action<BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.INeedTarget, BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.IConfigured>) (t => (BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.IConfigured) t.To<GLDocBatchStatus.released>().IsTriggeredOn((Expression<Func<JournalWithSubEntry, PXWorkflowEventHandlerBase<GLDocBatch>>>) (g => g.OnReleaseVoucher))));
        ts.Add((Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.INeedTarget, BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.IConfigured>) (t => (BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.IConfigured) t.To<GLDocBatchStatus.hold>().IsTriggeredOn((Expression<Func<JournalWithSubEntry, PXAction<GLDocBatch>>>) (g => g.putOnHold)).DoesNotPersist().WithFieldAssignments((Action<BoundedTo<JournalWithSubEntry, GLDocBatch>.Assignment.IContainerFillerFields>) (fas => fas.Add<GLDocBatch.hold>((Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.Assignment.INeedRightOperand, BoundedTo<JournalWithSubEntry, GLDocBatch>.Assignment.IConfigured>) (f => (BoundedTo<JournalWithSubEntry, GLDocBatch>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        ts.Add((Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.INeedTarget, BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.IConfigured>) (t => (BoundedTo<JournalWithSubEntry, GLDocBatch>.Transition.IConfigured) t.To<GLDocBatchStatus.hold>().IsTriggeredOn((Expression<Func<JournalWithSubEntry, PXWorkflowEventHandlerBase<GLDocBatch>>>) (g => g.OnUpdateStatus)).When((BoundedTo<JournalWithSubEntry, GLDocBatch>.ISharedCondition) conditions.IsOnHold)));
      }));
    })))).WithActions((Action<BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<JournalWithSubEntry, PXAction<GLDocBatch>>>) (g => g.initializeState), (Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionDefinition.IConfigured>) (a => (BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<JournalWithSubEntry, PXAction<GLDocBatch>>>) (g => g.releaseFromHold), (Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionDefinition.IConfigured>) (c => (BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionDefinition.IConfigured) c.InFolder(processingCategory).PlaceAfter("Last").WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<JournalWithSubEntry, GLDocBatch>.Assignment.IContainerFillerFields>) (fas => fas.Add<GLDocBatch.hold>((Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.Assignment.INeedRightOperand, BoundedTo<JournalWithSubEntry, GLDocBatch>.Assignment.IConfigured>) (f => (BoundedTo<JournalWithSubEntry, GLDocBatch>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<JournalWithSubEntry, PXAction<GLDocBatch>>>) (g => g.putOnHold), (Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionDefinition.IConfigured>) (c => (BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<JournalWithSubEntry, GLDocBatch>.Assignment.IContainerFillerFields>) (fas => fas.Add<GLDocBatch.hold>((Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.Assignment.INeedRightOperand, BoundedTo<JournalWithSubEntry, GLDocBatch>.Assignment.IConfigured>) (f => (BoundedTo<JournalWithSubEntry, GLDocBatch>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<JournalWithSubEntry, PXAction<GLDocBatch>>>) (g => g.release), (Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionDefinition.IConfigured>) (c => (BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
    })).WithHandlers((Action<BoundedTo<JournalWithSubEntry, GLDocBatch>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers =>
    {
      handlers.Add((Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<JournalWithSubEntry, GLDocBatch>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<JournalWithSubEntry, GLDocBatch>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<JournalWithSubEntry, GLDocBatch>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<GLDocBatch>) ((BoundedTo<JournalWithSubEntry, GLDocBatch>.WorkflowEventHandlerDefinition.INeedSubscriber<GLDocBatch>) handler.WithTargetOf<GLDocBatch>().OfFieldUpdated<GLDocBatch.hold>()).Is((Expression<Func<GLDocBatch, PXWorkflowEventHandler<GLDocBatch, GLDocBatch>>>) (g => g.OnUpdateStatus))).UsesTargetAsPrimaryEntity()));
      handlers.Add((Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<JournalWithSubEntry, GLDocBatch>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<JournalWithSubEntry, GLDocBatch>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<JournalWithSubEntry, GLDocBatch>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<GLDocBatch>) ((BoundedTo<JournalWithSubEntry, GLDocBatch>.WorkflowEventHandlerDefinition.INeedSubscriber<GLDocBatch>) handler.WithTargetOf<GLDocBatch>().OfEntityEvent<GLDocBatch.Events>((Expression<Func<GLDocBatch.Events, PXEntityEvent<GLDocBatch>>>) (e => e.ReleaseVoucher))).Is((Expression<Func<GLDocBatch, PXWorkflowEventHandler<GLDocBatch, GLDocBatch>>>) (g => g.OnReleaseVoucher))).UsesTargetAsPrimaryEntity()));
    })).WithCategories((Action<BoundedTo<JournalWithSubEntry, GLDocBatch>.ActionCategory.IContainerFillerCategories>) (categories => categories.Add(processingCategory)))));
  }

  public class Conditions : BoundedTo<JournalWithSubEntry, GLDocBatch>.Condition.Pack
  {
    public BoundedTo<JournalWithSubEntry, GLDocBatch>.Condition IsOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.Condition.ConditionBuilder, BoundedTo<JournalWithSubEntry, GLDocBatch>.Condition>) (c => c.FromBql<BqlOperand<GLDocBatch.hold, IBqlBool>.IsEqual<True>>()), nameof (IsOnHold));
      }
    }

    public BoundedTo<JournalWithSubEntry, GLDocBatch>.Condition IsNotOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<JournalWithSubEntry, GLDocBatch>.Condition.ConditionBuilder, BoundedTo<JournalWithSubEntry, GLDocBatch>.Condition>) (c => c.FromBql<BqlOperand<GLDocBatch.hold, IBqlBool>.IsEqual<False>>()), nameof (IsNotOnHold));
      }
    }
  }

  public static class ActionCategoryNames
  {
    public const string Processing = "Processing";
  }

  public static class ActionCategory
  {
    public const string Processing = "Processing";
  }
}
