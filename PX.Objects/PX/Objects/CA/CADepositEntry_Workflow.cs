// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CADepositEntry_Workflow
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

public class CADepositEntry_Workflow : PXGraphExtension<CADepositEntry>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    CADepositEntry_Workflow.Configure(config.GetScreenConfigurationContext<CADepositEntry, CADeposit>());
  }

  protected static void Configure(WorkflowContext<CADepositEntry, CADeposit> context)
  {
    CADepositEntry_Workflow.Conditions conditions = context.Conditions.GetPack<CADepositEntry_Workflow.Conditions>();
    BoundedTo<CADepositEntry, CADeposit>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("ProcessingID", (Func<BoundedTo<CADepositEntry, CADeposit>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CADepositEntry, CADeposit>.ActionCategory.IConfigured>) (category => (BoundedTo<CADepositEntry, CADeposit>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<CADepositEntry, CADeposit>.ActionCategory.IConfigured correctionsCategory = context.Categories.CreateNew("CorrectionsID", (Func<BoundedTo<CADepositEntry, CADeposit>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CADepositEntry, CADeposit>.ActionCategory.IConfigured>) (category => (BoundedTo<CADepositEntry, CADeposit>.ActionCategory.IConfigured) category.DisplayName("Corrections")));
    context.AddScreenConfigurationFor((Func<BoundedTo<CADepositEntry, CADeposit>.ScreenConfiguration.IStartConfigScreen, BoundedTo<CADepositEntry, CADeposit>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<CADepositEntry, CADeposit>.ScreenConfiguration.IConfigured) ((BoundedTo<CADepositEntry, CADeposit>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<CADeposit.status>().AddDefaultFlow((Func<BoundedTo<CADepositEntry, CADeposit>.Workflow.INeedStatesFlow, BoundedTo<CADepositEntry, CADeposit>.Workflow.IConfigured>) (flow => (BoundedTo<CADepositEntry, CADeposit>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<CADepositEntry, CADeposit>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<CADepositEntry, CADeposit>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CADepositEntry, CADeposit>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CADepositEntry, CADeposit>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<CADepositEntry, PXAutoAction<CADeposit>>>) (g => g.initializeState))));
      fss.Add<CADepositStatus.hold>((Func<BoundedTo<CADepositEntry, CADeposit>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CADepositEntry, CADeposit>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CADepositEntry, CADeposit>.BaseFlowStep.IConfigured) ((BoundedTo<CADepositEntry, CADeposit>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<CADepositEntry, CADeposit>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<CADepositEntry, PXAction<CADeposit>>>) (g => g.releaseFromHold), (Func<BoundedTo<CADepositEntry, CADeposit>.ActionState.IAllowOptionalConfig, BoundedTo<CADepositEntry, CADeposit>.ActionState.IConfigured>) (a => (BoundedTo<CADepositEntry, CADeposit>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<CADepositEntry, PXAction<CADeposit>>>) (g => g.addPayment), (Func<BoundedTo<CADepositEntry, CADeposit>.ActionState.IAllowOptionalConfig, BoundedTo<CADepositEntry, CADeposit>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<CADepositEntry, PXAction<CADeposit>>>) (g => g.printDepositSlip), (Func<BoundedTo<CADepositEntry, CADeposit>.ActionState.IAllowOptionalConfig, BoundedTo<CADepositEntry, CADeposit>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<CADepositEntry, CADeposit>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<CADepositEntry, PXWorkflowEventHandler<CADeposit>>>) (g => g.OnUpdateStatus))))));
      fss.Add<CADepositStatus.balanced>((Func<BoundedTo<CADepositEntry, CADeposit>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CADepositEntry, CADeposit>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CADepositEntry, CADeposit>.BaseFlowStep.IConfigured) ((BoundedTo<CADepositEntry, CADeposit>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<CADepositEntry, CADeposit>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<CADepositEntry, PXAction<CADeposit>>>) (g => g.Release), (Func<BoundedTo<CADepositEntry, CADeposit>.ActionState.IAllowOptionalConfig, BoundedTo<CADepositEntry, CADeposit>.ActionState.IConfigured>) (a => (BoundedTo<CADepositEntry, CADeposit>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<CADepositEntry, PXAction<CADeposit>>>) (g => g.putOnHold), (Func<BoundedTo<CADepositEntry, CADeposit>.ActionState.IAllowOptionalConfig, BoundedTo<CADepositEntry, CADeposit>.ActionState.IConfigured>) (a => (BoundedTo<CADepositEntry, CADeposit>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<CADepositEntry, PXAction<CADeposit>>>) (g => g.addPayment), (Func<BoundedTo<CADepositEntry, CADeposit>.ActionState.IAllowOptionalConfig, BoundedTo<CADepositEntry, CADeposit>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<CADepositEntry, PXAction<CADeposit>>>) (g => g.printDepositSlip), (Func<BoundedTo<CADepositEntry, CADeposit>.ActionState.IAllowOptionalConfig, BoundedTo<CADepositEntry, CADeposit>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<CADepositEntry, CADeposit>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<CADepositEntry, PXWorkflowEventHandler<CADeposit>>>) (g => g.OnReleaseDocument));
        handlers.Add((Expression<Func<CADepositEntry, PXWorkflowEventHandler<CADeposit>>>) (g => g.OnUpdateStatus));
      }))));
      fss.Add<CADepositStatus.released>((Func<BoundedTo<CADepositEntry, CADeposit>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CADepositEntry, CADeposit>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<CADepositEntry, CADeposit>.BaseFlowStep.IConfigured) ((BoundedTo<CADepositEntry, CADeposit>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<CADepositEntry, CADeposit>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<CADepositEntry, PXAction<CADeposit>>>) (g => g.VoidDocument), (Func<BoundedTo<CADepositEntry, CADeposit>.ActionState.IAllowOptionalConfig, BoundedTo<CADepositEntry, CADeposit>.ActionState.IConfigured>) (a => (BoundedTo<CADepositEntry, CADeposit>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<CADepositEntry, PXAction<CADeposit>>>) (g => g.printDepositSlip), (Func<BoundedTo<CADepositEntry, CADeposit>.ActionState.IAllowOptionalConfig, BoundedTo<CADepositEntry, CADeposit>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<CADepositEntry, CADeposit>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<CADepositEntry, PXWorkflowEventHandler<CADeposit>>>) (g => g.OnVoidDocument))))));
      fss.Add<CADepositStatus.voided>((Func<BoundedTo<CADepositEntry, CADeposit>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CADepositEntry, CADeposit>.BaseFlowStep.IConfigured>) null);
    })).WithTransitions((Action<BoundedTo<CADepositEntry, CADeposit>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<CADepositEntry, CADeposit>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<CADepositEntry, CADeposit>.Transition.INeedTarget, BoundedTo<CADepositEntry, CADeposit>.Transition.IConfigured>) (t => (BoundedTo<CADepositEntry, CADeposit>.Transition.IConfigured) t.To<CADepositStatus.hold>().IsTriggeredOn((Expression<Func<CADepositEntry, PXAction<CADeposit>>>) (g => g.initializeState)).When((BoundedTo<CADepositEntry, CADeposit>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<CADepositEntry, CADeposit>.Transition.INeedTarget, BoundedTo<CADepositEntry, CADeposit>.Transition.IConfigured>) (t => (BoundedTo<CADepositEntry, CADeposit>.Transition.IConfigured) t.To<CADepositStatus.balanced>().IsTriggeredOn((Expression<Func<CADepositEntry, PXAction<CADeposit>>>) (g => g.initializeState)).When((BoundedTo<CADepositEntry, CADeposit>.ISharedCondition) conditions.IsNotOnHold)));
      }));
      transitions.AddGroupFrom<CADepositStatus.hold>((Action<BoundedTo<CADepositEntry, CADeposit>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<CADepositEntry, CADeposit>.Transition.INeedTarget, BoundedTo<CADepositEntry, CADeposit>.Transition.IConfigured>) (t => (BoundedTo<CADepositEntry, CADeposit>.Transition.IConfigured) t.To<CADepositStatus.balanced>().IsTriggeredOn((Expression<Func<CADepositEntry, PXAction<CADeposit>>>) (g => g.releaseFromHold)).WithFieldAssignments((Action<BoundedTo<CADepositEntry, CADeposit>.Assignment.IContainerFillerFields>) (fas => fas.Add<CADeposit.hold>((Func<BoundedTo<CADepositEntry, CADeposit>.Assignment.INeedRightOperand, BoundedTo<CADepositEntry, CADeposit>.Assignment.IConfigured>) (f => (BoundedTo<CADepositEntry, CADeposit>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
        ts.Add((Func<BoundedTo<CADepositEntry, CADeposit>.Transition.INeedTarget, BoundedTo<CADepositEntry, CADeposit>.Transition.IConfigured>) (t => (BoundedTo<CADepositEntry, CADeposit>.Transition.IConfigured) t.To<CADepositStatus.balanced>().IsTriggeredOn((Expression<Func<CADepositEntry, PXWorkflowEventHandlerBase<CADeposit>>>) (g => g.OnUpdateStatus)).When((BoundedTo<CADepositEntry, CADeposit>.ISharedCondition) conditions.IsNotOnHold)));
      }));
      transitions.AddGroupFrom<CADepositStatus.balanced>((Action<BoundedTo<CADepositEntry, CADeposit>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<CADepositEntry, CADeposit>.Transition.INeedTarget, BoundedTo<CADepositEntry, CADeposit>.Transition.IConfigured>) (t => (BoundedTo<CADepositEntry, CADeposit>.Transition.IConfigured) t.To<CADepositStatus.hold>().IsTriggeredOn((Expression<Func<CADepositEntry, PXAction<CADeposit>>>) (g => g.putOnHold)).WithFieldAssignments((Action<BoundedTo<CADepositEntry, CADeposit>.Assignment.IContainerFillerFields>) (fas => fas.Add<CADeposit.hold>((Func<BoundedTo<CADepositEntry, CADeposit>.Assignment.INeedRightOperand, BoundedTo<CADepositEntry, CADeposit>.Assignment.IConfigured>) (f => (BoundedTo<CADepositEntry, CADeposit>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        ts.Add((Func<BoundedTo<CADepositEntry, CADeposit>.Transition.INeedTarget, BoundedTo<CADepositEntry, CADeposit>.Transition.IConfigured>) (t => (BoundedTo<CADepositEntry, CADeposit>.Transition.IConfigured) t.To<CADepositStatus.released>().IsTriggeredOn((Expression<Func<CADepositEntry, PXWorkflowEventHandlerBase<CADeposit>>>) (g => g.OnReleaseDocument))));
        ts.Add((Func<BoundedTo<CADepositEntry, CADeposit>.Transition.INeedTarget, BoundedTo<CADepositEntry, CADeposit>.Transition.IConfigured>) (t => (BoundedTo<CADepositEntry, CADeposit>.Transition.IConfigured) t.To<CADepositStatus.hold>().IsTriggeredOn((Expression<Func<CADepositEntry, PXWorkflowEventHandlerBase<CADeposit>>>) (g => g.OnUpdateStatus)).When((BoundedTo<CADepositEntry, CADeposit>.ISharedCondition) conditions.IsOnHold)));
      }));
      transitions.AddGroupFrom<CADepositStatus.released>((Action<BoundedTo<CADepositEntry, CADeposit>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<CADepositEntry, CADeposit>.Transition.INeedTarget, BoundedTo<CADepositEntry, CADeposit>.Transition.IConfigured>) (t => (BoundedTo<CADepositEntry, CADeposit>.Transition.IConfigured) t.To<CADepositStatus.voided>().IsTriggeredOn((Expression<Func<CADepositEntry, PXWorkflowEventHandlerBase<CADeposit>>>) (g => g.OnVoidDocument))))));
    })))).WithActions((Action<BoundedTo<CADepositEntry, CADeposit>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<CADepositEntry, PXAction<CADeposit>>>) (g => g.initializeState), (Func<BoundedTo<CADepositEntry, CADeposit>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CADepositEntry, CADeposit>.ActionDefinition.IConfigured>) (a => (BoundedTo<CADepositEntry, CADeposit>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<CADepositEntry, PXAction<CADeposit>>>) (g => g.putOnHold), (Func<BoundedTo<CADepositEntry, CADeposit>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CADepositEntry, CADeposit>.ActionDefinition.IConfigured>) (c => (BoundedTo<CADepositEntry, CADeposit>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<CADepositEntry, CADeposit>.Assignment.IContainerFillerFields>) (fas => fas.Add<CADeposit.hold>((Func<BoundedTo<CADepositEntry, CADeposit>.Assignment.INeedRightOperand, BoundedTo<CADepositEntry, CADeposit>.Assignment.IConfigured>) (f => (BoundedTo<CADepositEntry, CADeposit>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<CADepositEntry, PXAction<CADeposit>>>) (g => g.releaseFromHold), (Func<BoundedTo<CADepositEntry, CADeposit>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CADepositEntry, CADeposit>.ActionDefinition.IConfigured>) (c => (BoundedTo<CADepositEntry, CADeposit>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<CADepositEntry, CADeposit>.Assignment.IContainerFillerFields>) (fas => fas.Add<CADeposit.hold>((Func<BoundedTo<CADepositEntry, CADeposit>.Assignment.INeedRightOperand, BoundedTo<CADepositEntry, CADeposit>.Assignment.IConfigured>) (f => (BoundedTo<CADepositEntry, CADeposit>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<CADepositEntry, PXAction<CADeposit>>>) (g => g.Release), (Func<BoundedTo<CADepositEntry, CADeposit>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CADepositEntry, CADeposit>.ActionDefinition.IConfigured>) (c => (BoundedTo<CADepositEntry, CADeposit>.ActionDefinition.IConfigured) c.InFolder(processingCategory).PlaceAfter("Last")));
      actions.Add((Expression<Func<CADepositEntry, PXAction<CADeposit>>>) (g => g.addPayment), (Func<BoundedTo<CADepositEntry, CADeposit>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CADepositEntry, CADeposit>.ActionDefinition.IConfigured>) (c => (BoundedTo<CADepositEntry, CADeposit>.ActionDefinition.IConfigured) c.IsDisabledWhen((BoundedTo<CADepositEntry, CADeposit>.ISharedCondition) conditions.IsVoidingEntry)));
      actions.Add((Expression<Func<CADepositEntry, PXAction<CADeposit>>>) (g => g.VoidDocument), (Func<BoundedTo<CADepositEntry, CADeposit>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CADepositEntry, CADeposit>.ActionDefinition.IConfigured>) (c => (BoundedTo<CADepositEntry, CADeposit>.ActionDefinition.IConfigured) c.InFolder(correctionsCategory).IsHiddenWhen((BoundedTo<CADepositEntry, CADeposit>.ISharedCondition) conditions.IsVoidingEntry)));
      actions.Add((Expression<Func<CADepositEntry, PXAction<CADeposit>>>) (g => g.printDepositSlip), (Func<BoundedTo<CADepositEntry, CADeposit>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CADepositEntry, CADeposit>.ActionDefinition.IConfigured>) (c => (BoundedTo<CADepositEntry, CADeposit>.ActionDefinition.IConfigured) c.InFolder((FolderType) 2)));
    })).WithHandlers((Action<BoundedTo<CADepositEntry, CADeposit>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers =>
    {
      handlers.Add((Func<BoundedTo<CADepositEntry, CADeposit>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<CADepositEntry, CADeposit>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<CADepositEntry, CADeposit>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<CADepositEntry, CADeposit>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<CADeposit>) ((BoundedTo<CADepositEntry, CADeposit>.WorkflowEventHandlerDefinition.INeedSubscriber<CADeposit>) handler.WithTargetOf<CADeposit>().OfEntityEvent<CADeposit.Events>((Expression<Func<CADeposit.Events, PXEntityEvent<CADeposit>>>) (e => e.ReleaseDocument))).Is((Expression<Func<CADeposit, PXWorkflowEventHandler<CADeposit, CADeposit>>>) (g => g.OnReleaseDocument))).UsesTargetAsPrimaryEntity()));
      handlers.Add((Func<BoundedTo<CADepositEntry, CADeposit>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<CADepositEntry, CADeposit>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<CADepositEntry, CADeposit>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<CADepositEntry, CADeposit>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<CADeposit>) ((BoundedTo<CADepositEntry, CADeposit>.WorkflowEventHandlerDefinition.INeedSubscriber<CADeposit>) handler.WithTargetOf<CADeposit>().OfEntityEvent<CADeposit.Events>((Expression<Func<CADeposit.Events, PXEntityEvent<CADeposit>>>) (e => e.VoidDocument))).Is((Expression<Func<CADeposit, PXWorkflowEventHandler<CADeposit, CADeposit>>>) (g => g.OnVoidDocument))).UsesTargetAsPrimaryEntity()));
      handlers.Add((Func<BoundedTo<CADepositEntry, CADeposit>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<CADepositEntry, CADeposit>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<CADepositEntry, CADeposit>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<CADepositEntry, CADeposit>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<CADeposit>) ((BoundedTo<CADepositEntry, CADeposit>.WorkflowEventHandlerDefinition.INeedSubscriber<CADeposit>) handler.WithTargetOf<CADeposit>().OfFieldUpdated<CADeposit.hold>()).Is((Expression<Func<CADeposit, PXWorkflowEventHandler<CADeposit, CADeposit>>>) (g => g.OnUpdateStatus))).UsesTargetAsPrimaryEntity()));
    })).WithCategories((Action<BoundedTo<CADepositEntry, CADeposit>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(correctionsCategory);
      categories.Update((FolderType) 2, (Func<BoundedTo<CADepositEntry, CADeposit>.ActionCategory.ConfiguratorCategory, BoundedTo<CADepositEntry, CADeposit>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(correctionsCategory)));
    }))));
  }

  public class Conditions : BoundedTo<CADepositEntry, CADeposit>.Condition.Pack
  {
    public BoundedTo<CADepositEntry, CADeposit>.Condition IsOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CADepositEntry, CADeposit>.Condition.ConditionBuilder, BoundedTo<CADepositEntry, CADeposit>.Condition>) (c => c.FromBql<BqlOperand<CADeposit.hold, IBqlBool>.IsEqual<True>>()), nameof (IsOnHold));
      }
    }

    public BoundedTo<CADepositEntry, CADeposit>.Condition IsNotOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CADepositEntry, CADeposit>.Condition.ConditionBuilder, BoundedTo<CADepositEntry, CADeposit>.Condition>) (c => c.FromBql<BqlOperand<CADeposit.hold, IBqlBool>.IsEqual<False>>()), nameof (IsNotOnHold));
      }
    }

    public BoundedTo<CADepositEntry, CADeposit>.Condition IsVoidingEntry
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CADepositEntry, CADeposit>.Condition.ConditionBuilder, BoundedTo<CADepositEntry, CADeposit>.Condition>) (c => c.FromBql<BqlOperand<CADeposit.tranType, IBqlString>.IsEqual<CATranType.cAVoidDeposit>>()), nameof (IsVoidingEntry));
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
