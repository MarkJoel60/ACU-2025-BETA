// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.JournalEntry_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.GL;

public class JournalEntry_Workflow : PXGraphExtension<JournalEntry>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    JournalEntry_Workflow.Configure(config.GetScreenConfigurationContext<JournalEntry, Batch>());
  }

  protected static void Configure(WorkflowContext<JournalEntry, Batch> context)
  {
    JournalEntry_Workflow.Conditions conditions = context.Conditions.GetPack<JournalEntry_Workflow.Conditions>();
    BoundedTo<JournalEntry, Batch>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<JournalEntry, Batch>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<JournalEntry, Batch>.ActionCategory.IConfigured>) (category => (BoundedTo<JournalEntry, Batch>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<JournalEntry, Batch>.ActionCategory.IConfigured correctionsCategory = context.Categories.CreateNew("Corrections", (Func<BoundedTo<JournalEntry, Batch>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<JournalEntry, Batch>.ActionCategory.IConfigured>) (category => (BoundedTo<JournalEntry, Batch>.ActionCategory.IConfigured) category.DisplayName("Corrections")));
    BoundedTo<JournalEntry, Batch>.ActionCategory.IConfigured approvalCategory = context.Categories.CreateNew("Approval", (Func<BoundedTo<JournalEntry, Batch>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<JournalEntry, Batch>.ActionCategory.IConfigured>) (category => (BoundedTo<JournalEntry, Batch>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    BoundedTo<JournalEntry, Batch>.ActionCategory.IConfigured customOtherCategory = context.Categories.CreateNew("CustomOther", (Func<BoundedTo<JournalEntry, Batch>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<JournalEntry, Batch>.ActionCategory.IConfigured>) (category => (BoundedTo<JournalEntry, Batch>.ActionCategory.IConfigured) category.DisplayName("Other")));
    context.AddScreenConfigurationFor((Func<BoundedTo<JournalEntry, Batch>.ScreenConfiguration.IStartConfigScreen, BoundedTo<JournalEntry, Batch>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<JournalEntry, Batch>.ScreenConfiguration.IConfigured) ((BoundedTo<JournalEntry, Batch>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<Batch.status>().AddDefaultFlow((Func<BoundedTo<JournalEntry, Batch>.Workflow.INeedStatesFlow, BoundedTo<JournalEntry, Batch>.Workflow.IConfigured>) (flow => (BoundedTo<JournalEntry, Batch>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<JournalEntry, Batch>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<JournalEntry, Batch>.FlowState.INeedAnyFlowStateConfig, BoundedTo<JournalEntry, Batch>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<JournalEntry, Batch>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<JournalEntry, PXAutoAction<Batch>>>) (g => g.initializeState))));
      fss.Add<BatchStatus.hold>((Func<BoundedTo<JournalEntry, Batch>.FlowState.INeedAnyFlowStateConfig, BoundedTo<JournalEntry, Batch>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<JournalEntry, Batch>.BaseFlowStep.IConfigured) ((BoundedTo<JournalEntry, Batch>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<JournalEntry, Batch>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.releaseFromHold), (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) (a => (BoundedTo<JournalEntry, Batch>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.editReclassBatch), (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) (a => (BoundedTo<JournalEntry, Batch>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
      }))).WithEventHandlers((Action<BoundedTo<JournalEntry, Batch>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<JournalEntry, PXWorkflowEventHandler<Batch>>>) (g => g.OnUpdateStatus))))));
      fss.Add<BatchStatus.balanced>((Func<BoundedTo<JournalEntry, Batch>.FlowState.INeedAnyFlowStateConfig, BoundedTo<JournalEntry, Batch>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<JournalEntry, Batch>.BaseFlowStep.IConfigured) ((BoundedTo<JournalEntry, Batch>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<JournalEntry, Batch>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.release), (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) (a => (BoundedTo<JournalEntry, Batch>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.putOnHold), (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.createSchedule), (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.editReclassBatch), (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) (a => (BoundedTo<JournalEntry, Batch>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.glEditDetails), (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<JournalEntry, Batch>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<JournalEntry, PXWorkflowEventHandler<Batch>>>) (g => g.OnConfirmSchedule));
        handlers.Add((Expression<Func<JournalEntry, PXWorkflowEventHandler<Batch>>>) (g => g.OnReleaseBatch));
        handlers.Add((Expression<Func<JournalEntry, PXWorkflowEventHandler<Batch>>>) (g => g.OnUpdateStatus));
      }))));
      fss.Add<BatchStatus.scheduled>((Func<BoundedTo<JournalEntry, Batch>.FlowState.INeedAnyFlowStateConfig, BoundedTo<JournalEntry, Batch>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<JournalEntry, Batch>.BaseFlowStep.IConfigured) ((BoundedTo<JournalEntry, Batch>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<JournalEntry, Batch>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.createSchedule), (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) (a => (BoundedTo<JournalEntry, Batch>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.glEditDetails), (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<JournalEntry, Batch>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<JournalEntry, PXWorkflowEventHandler<Batch>>>) (g => g.OnConfirmSchedule));
        handlers.Add((Expression<Func<JournalEntry, PXWorkflowEventHandler<Batch>>>) (g => g.OnVoidSchedule));
      }))));
      fss.Add<BatchStatus.voided>((Func<BoundedTo<JournalEntry, Batch>.FlowState.INeedAnyFlowStateConfig, BoundedTo<JournalEntry, Batch>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<JournalEntry, Batch>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<JournalEntry, Batch>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.glEditDetails), (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) null)))));
      fss.Add<BatchStatus.unposted>((Func<BoundedTo<JournalEntry, Batch>.FlowState.INeedAnyFlowStateConfig, BoundedTo<JournalEntry, Batch>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<JournalEntry, Batch>.BaseFlowStep.IConfigured) ((BoundedTo<JournalEntry, Batch>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<JournalEntry, Batch>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.reverseBatch), (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.reclassify), (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.batchRegisterDetails), (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.glReversingBatches), (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.post), (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) (a => (BoundedTo<JournalEntry, Batch>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
      }))).WithEventHandlers((Action<BoundedTo<JournalEntry, Batch>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<JournalEntry, PXWorkflowEventHandler<Batch>>>) (g => g.OnPostBatch))))));
      fss.Add<BatchStatus.posted>((Func<BoundedTo<JournalEntry, Batch>.FlowState.INeedAnyFlowStateConfig, BoundedTo<JournalEntry, Batch>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<JournalEntry, Batch>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<JournalEntry, Batch>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.reverseBatch), (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.reclassify), (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.batchRegisterDetails), (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.glReversingBatches), (Func<BoundedTo<JournalEntry, Batch>.ActionState.IAllowOptionalConfig, BoundedTo<JournalEntry, Batch>.ActionState.IConfigured>) null);
      }))));
    })).WithTransitions((Action<BoundedTo<JournalEntry, Batch>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<JournalEntry, Batch>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.hold>().IsTriggeredOn((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.initializeState)).When((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.balanced>().IsTriggeredOn((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.initializeState)).When((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsBalanced)));
        ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.unposted>().IsTriggeredOn((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.initializeState)).When((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsUnposted)));
        ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.scheduled>().IsTriggeredOn((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.initializeState)).When((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsScheduled)));
      }));
      transitions.AddGroupFrom<BatchStatus.hold>((Action<BoundedTo<JournalEntry, Batch>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.balanced>().IsTriggeredOn((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.releaseFromHold)).DoesNotPersist().WithFieldAssignments((Action<BoundedTo<JournalEntry, Batch>.Assignment.IContainerFillerFields>) (fas => fas.Add<Batch.hold>((Func<BoundedTo<JournalEntry, Batch>.Assignment.INeedRightOperand, BoundedTo<JournalEntry, Batch>.Assignment.IConfigured>) (f => (BoundedTo<JournalEntry, Batch>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
        ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.balanced>().IsTriggeredOn((Expression<Func<JournalEntry, PXWorkflowEventHandlerBase<Batch>>>) (g => g.OnUpdateStatus)).When((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsBalanced)));
      }));
      transitions.AddGroupFrom<BatchStatus.balanced>((Action<BoundedTo<JournalEntry, Batch>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.hold>().IsTriggeredOn((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.putOnHold)).DoesNotPersist().WithFieldAssignments((Action<BoundedTo<JournalEntry, Batch>.Assignment.IContainerFillerFields>) (fas => fas.Add<Batch.hold>((Func<BoundedTo<JournalEntry, Batch>.Assignment.INeedRightOperand, BoundedTo<JournalEntry, Batch>.Assignment.IConfigured>) (f => (BoundedTo<JournalEntry, Batch>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.scheduled>().IsTriggeredOn((Expression<Func<JournalEntry, PXWorkflowEventHandlerBase<Batch>>>) (g => g.OnConfirmSchedule)).WithFieldAssignments((Action<BoundedTo<JournalEntry, Batch>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<Batch.scheduled>((Func<BoundedTo<JournalEntry, Batch>.Assignment.INeedRightOperand, BoundedTo<JournalEntry, Batch>.Assignment.IConfigured>) (e => (BoundedTo<JournalEntry, Batch>.Assignment.IConfigured) e.SetFromValue((object) true)));
          fas.Add<Batch.scheduleID>((Func<BoundedTo<JournalEntry, Batch>.Assignment.INeedRightOperand, BoundedTo<JournalEntry, Batch>.Assignment.IConfigured>) (e => (BoundedTo<JournalEntry, Batch>.Assignment.IConfigured) e.SetFromExpression("@ScheduleID")));
        }))));
        ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.unposted>().IsTriggeredOn((Expression<Func<JournalEntry, PXWorkflowEventHandlerBase<Batch>>>) (g => g.OnReleaseBatch))));
        ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.hold>().IsTriggeredOn((Expression<Func<JournalEntry, PXWorkflowEventHandlerBase<Batch>>>) (g => g.OnUpdateStatus)).When((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.unposted>().IsTriggeredOn((Expression<Func<JournalEntry, PXWorkflowEventHandlerBase<Batch>>>) (g => g.OnUpdateStatus)).When((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsUnposted)));
      }));
      transitions.AddGroupFrom<BatchStatus.unposted>((Action<BoundedTo<JournalEntry, Batch>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.posted>().IsTriggeredOn((Expression<Func<JournalEntry, PXWorkflowEventHandlerBase<Batch>>>) (g => g.OnPostBatch))))));
      transitions.AddGroupFrom<BatchStatus.scheduled>((Action<BoundedTo<JournalEntry, Batch>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.scheduled>().IsTriggeredOn((Expression<Func<JournalEntry, PXWorkflowEventHandlerBase<Batch>>>) (g => g.OnConfirmSchedule)).WithFieldAssignments((Action<BoundedTo<JournalEntry, Batch>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<Batch.scheduled>((Func<BoundedTo<JournalEntry, Batch>.Assignment.INeedRightOperand, BoundedTo<JournalEntry, Batch>.Assignment.IConfigured>) (e => (BoundedTo<JournalEntry, Batch>.Assignment.IConfigured) e.SetFromValue((object) true)));
          fas.Add<Batch.scheduleID>((Func<BoundedTo<JournalEntry, Batch>.Assignment.INeedRightOperand, BoundedTo<JournalEntry, Batch>.Assignment.IConfigured>) (e => (BoundedTo<JournalEntry, Batch>.Assignment.IConfigured) e.SetFromExpression("@ScheduleID")));
        }))));
        ts.Add((Func<BoundedTo<JournalEntry, Batch>.Transition.INeedTarget, BoundedTo<JournalEntry, Batch>.Transition.IConfigured>) (t => (BoundedTo<JournalEntry, Batch>.Transition.IConfigured) t.To<BatchStatus.voided>().IsTriggeredOn((Expression<Func<JournalEntry, PXWorkflowEventHandlerBase<Batch>>>) (g => g.OnVoidSchedule)).WithFieldAssignments((Action<BoundedTo<JournalEntry, Batch>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<Batch.voided>((Func<BoundedTo<JournalEntry, Batch>.Assignment.INeedRightOperand, BoundedTo<JournalEntry, Batch>.Assignment.IConfigured>) (e => (BoundedTo<JournalEntry, Batch>.Assignment.IConfigured) e.SetFromValue((object) true)));
          fas.Add<Batch.scheduled>((Func<BoundedTo<JournalEntry, Batch>.Assignment.INeedRightOperand, BoundedTo<JournalEntry, Batch>.Assignment.IConfigured>) (e => (BoundedTo<JournalEntry, Batch>.Assignment.IConfigured) e.SetFromValue((object) false)));
          fas.Add<Batch.scheduleID>((Func<BoundedTo<JournalEntry, Batch>.Assignment.INeedRightOperand, BoundedTo<JournalEntry, Batch>.Assignment.IConfigured>) (e => (BoundedTo<JournalEntry, Batch>.Assignment.IConfigured) e.SetFromValue((object) null)));
        }))));
      }));
    })))).WithActions((Action<BoundedTo<JournalEntry, Batch>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.initializeState), (Func<BoundedTo<JournalEntry, Batch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured>) (a => (BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.releaseFromHold), (Func<BoundedTo<JournalEntry, Batch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured>) (c => (BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).IsHiddenWhen((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsNotGLModule).WithFieldAssignments((Action<BoundedTo<JournalEntry, Batch>.Assignment.IContainerFillerFields>) (fas => fas.Add<Batch.hold>((Func<BoundedTo<JournalEntry, Batch>.Assignment.INeedRightOperand, BoundedTo<JournalEntry, Batch>.Assignment.IConfigured>) (f => (BoundedTo<JournalEntry, Batch>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.putOnHold), (Func<BoundedTo<JournalEntry, Batch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured>) (c => (BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).IsHiddenWhen((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsNotGLModule).WithFieldAssignments((Action<BoundedTo<JournalEntry, Batch>.Assignment.IContainerFillerFields>) (fas => fas.Add<Batch.hold>((Func<BoundedTo<JournalEntry, Batch>.Assignment.INeedRightOperand, BoundedTo<JournalEntry, Batch>.Assignment.IConfigured>) (f => (BoundedTo<JournalEntry, Batch>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.editReclassBatch), (Func<BoundedTo<JournalEntry, Batch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured>) (c => (BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured) c.InFolder(correctionsCategory).IsHiddenWhen((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsNotReclassification)));
      actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.release), (Func<BoundedTo<JournalEntry, Batch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured>) (c => (BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured) c.InFolder(processingCategory).PlaceAfter("Last").IsDisabledWhen((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsNotGLModule)));
      actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.post), (Func<BoundedTo<JournalEntry, Batch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured>) (c => (BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured) c.InFolder(processingCategory).PlaceAfter("Release")));
      actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.reverseBatch), (Func<BoundedTo<JournalEntry, Batch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured>) (c => (BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured) c.InFolder(correctionsCategory)));
      actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.createSchedule), (Func<BoundedTo<JournalEntry, Batch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured>) (c => (BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured) c.InFolder(customOtherCategory).IsDisabledWhen((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsDisabledSchedule)));
      actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.reclassify), (Func<BoundedTo<JournalEntry, Batch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured>) (c => (BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured) c.InFolder(correctionsCategory)));
      actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.batchRegisterDetails), (Func<BoundedTo<JournalEntry, Batch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured>) (c => (BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured) c.InFolder((FolderType) 2)));
      actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.glEditDetails), (Func<BoundedTo<JournalEntry, Batch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured>) (c => (BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured) c.InFolder((FolderType) 2)));
      actions.Add((Expression<Func<JournalEntry, PXAction<Batch>>>) (g => g.glReversingBatches), (Func<BoundedTo<JournalEntry, Batch>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured>) (c => (BoundedTo<JournalEntry, Batch>.ActionDefinition.IConfigured) c.InFolder((FolderType) 2).IsDisabledWhen((BoundedTo<JournalEntry, Batch>.ISharedCondition) conditions.IsNotReversed)));
    })).WithHandlers((Action<BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers =>
    {
      handlers.Add((Func<BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<Batch>) ((BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.INeedSubscriber<Batch>) ((BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.INeedEventContainer<Batch, Schedule>) handler.WithTargetOf<Batch>().WithParametersOf<Schedule>()).OfEntityEvent<Batch.Events>((Expression<Func<Batch.Events, PXEntityEvent<Batch, Schedule>>>) (e => e.ConfirmSchedule))).Is((Expression<Func<Batch, PXWorkflowEventHandler<Batch, Batch>>>) (g => g.OnConfirmSchedule))).UsesTargetAsPrimaryEntity()));
      handlers.Add((Func<BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<Batch>) ((BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.INeedSubscriber<Batch>) ((BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.INeedEventContainer<Batch, Schedule>) handler.WithTargetOf<Batch>().WithParametersOf<Schedule>()).OfEntityEvent<Batch.Events>((Expression<Func<Batch.Events, PXEntityEvent<Batch, Schedule>>>) (e => e.VoidSchedule))).Is((Expression<Func<Batch, PXWorkflowEventHandler<Batch, Batch>>>) (g => g.OnVoidSchedule))).UsesTargetAsPrimaryEntity()));
      handlers.Add((Func<BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<Batch>) ((BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.INeedSubscriber<Batch>) handler.WithTargetOf<Batch>().OfEntityEvent<Batch.Events>((Expression<Func<Batch.Events, PXEntityEvent<Batch>>>) (e => e.ReleaseBatch))).Is((Expression<Func<Batch, PXWorkflowEventHandler<Batch, Batch>>>) (g => g.OnReleaseBatch))).UsesTargetAsPrimaryEntity()));
      handlers.Add((Func<BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<Batch>) ((BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.INeedSubscriber<Batch>) handler.WithTargetOf<Batch>().OfEntityEvent<Batch.Events>((Expression<Func<Batch.Events, PXEntityEvent<Batch>>>) (e => e.PostBatch))).Is((Expression<Func<Batch, PXWorkflowEventHandler<Batch, Batch>>>) (g => g.OnPostBatch))).UsesTargetAsPrimaryEntity()));
      handlers.Add((Func<BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<Batch>) ((BoundedTo<JournalEntry, Batch>.WorkflowEventHandlerDefinition.INeedSubscriber<Batch>) handler.WithTargetOf<Batch>().OfFieldsUpdated<TypeArrayOf<IBqlField>.FilledWith<Batch.hold, Batch.released>>()).Is((Expression<Func<Batch, PXWorkflowEventHandler<Batch, Batch>>>) (g => g.OnUpdateStatus))).UsesTargetAsPrimaryEntity()));
    })).WithCategories((Action<BoundedTo<JournalEntry, Batch>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(correctionsCategory);
      categories.Add(approvalCategory);
      categories.Add(customOtherCategory);
      categories.Update((FolderType) 2, (Func<BoundedTo<JournalEntry, Batch>.ActionCategory.ConfiguratorCategory, BoundedTo<JournalEntry, Batch>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(customOtherCategory)));
    }))));
  }

  public class Conditions : BoundedTo<JournalEntry, Batch>.Condition.Pack
  {
    public BoundedTo<JournalEntry, Batch>.Condition IsOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<JournalEntry, Batch>.Condition.ConditionBuilder, BoundedTo<JournalEntry, Batch>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Batch.hold, Equal<True>>>>>.And<BqlOperand<Batch.released, IBqlBool>.IsEqual<False>>>()), nameof (IsOnHold));
      }
    }

    public BoundedTo<JournalEntry, Batch>.Condition IsBalanced
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<JournalEntry, Batch>.Condition.ConditionBuilder, BoundedTo<JournalEntry, Batch>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Batch.hold, Equal<False>>>>>.And<BqlOperand<Batch.released, IBqlBool>.IsEqual<False>>>()), nameof (IsBalanced));
      }
    }

    public BoundedTo<JournalEntry, Batch>.Condition IsScheduled
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<JournalEntry, Batch>.Condition.ConditionBuilder, BoundedTo<JournalEntry, Batch>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Batch.scheduled, Equal<True>>>>>.And<BqlOperand<Batch.released, IBqlBool>.IsEqual<False>>>()), nameof (IsScheduled));
      }
    }

    public BoundedTo<JournalEntry, Batch>.Condition IsPosted
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<JournalEntry, Batch>.Condition.ConditionBuilder, BoundedTo<JournalEntry, Batch>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Batch.posted, Equal<True>>>>>.And<BqlOperand<Batch.released, IBqlBool>.IsEqual<True>>>()), nameof (IsPosted));
      }
    }

    public BoundedTo<JournalEntry, Batch>.Condition IsUnposted
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<JournalEntry, Batch>.Condition.ConditionBuilder, BoundedTo<JournalEntry, Batch>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Batch.posted, Equal<False>>>>>.And<BqlOperand<Batch.released, IBqlBool>.IsEqual<True>>>()), nameof (IsUnposted));
      }
    }

    public BoundedTo<JournalEntry, Batch>.Condition IsNotGLModule
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<JournalEntry, Batch>.Condition.ConditionBuilder, BoundedTo<JournalEntry, Batch>.Condition>) (c => c.FromBql<BqlOperand<Batch.module, IBqlString>.IsNotEqual<BatchModule.moduleGL>>()), nameof (IsNotGLModule));
      }
    }

    public BoundedTo<JournalEntry, Batch>.Condition IsNotReversed
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<JournalEntry, Batch>.Condition.ConditionBuilder, BoundedTo<JournalEntry, Batch>.Condition>) (c => c.FromBql<BqlOperand<Batch.reverseCount, IBqlInt>.IsEqual<Zero>>()), nameof (IsNotReversed));
      }
    }

    public BoundedTo<JournalEntry, Batch>.Condition IsDisabledSchedule
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<JournalEntry, Batch>.Condition.ConditionBuilder, BoundedTo<JournalEntry, Batch>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Batch.module, NotEqual<BatchModule.moduleGL>>>>, Or<BqlOperand<Batch.batchType, IBqlString>.IsEqual<BatchTypeCode.reclassification>>>, Or<BqlOperand<Batch.batchType, IBqlString>.IsEqual<BatchTypeCode.trialBalance>>>>.Or<BqlOperand<Batch.batchType, IBqlString>.IsEqual<BatchTypeCode.allocation>>>()), nameof (IsDisabledSchedule));
      }
    }

    public BoundedTo<JournalEntry, Batch>.Condition IsNotReclassification
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<JournalEntry, Batch>.Condition.ConditionBuilder, BoundedTo<JournalEntry, Batch>.Condition>) (c => c.FromBql<BqlOperand<Batch.batchType, IBqlString>.IsNotEqual<BatchTypeCode.reclassification>>()), nameof (IsNotReclassification));
      }
    }
  }

  public static class ActionCategoryNames
  {
    public const string Processing = "Processing";
    public const string Corrections = "Corrections";
    public const string Approval = "Approval";
    public const string CustomOther = "CustomOther";
  }

  public static class ActionCategory
  {
    public const string Processing = "Processing";
    public const string Corrections = "Corrections";
    public const string Approval = "Approval";
    public const string Other = "Other";
  }
}
