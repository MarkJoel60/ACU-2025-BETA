// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.TransactionEntry_Workflow
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
namespace PX.Objects.FA;

public class TransactionEntry_Workflow : PXGraphExtension<TransactionEntry>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    TransactionEntry_Workflow.Configure(config.GetScreenConfigurationContext<TransactionEntry, FARegister>());
  }

  protected static void Configure(
    WorkflowContext<TransactionEntry, FARegister> context)
  {
    TransactionEntry_Workflow.Conditions conditions = context.Conditions.GetPack<TransactionEntry_Workflow.Conditions>();
    BoundedTo<TransactionEntry, FARegister>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<TransactionEntry, FARegister>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<TransactionEntry, FARegister>.ActionCategory.IConfigured>) (category => (BoundedTo<TransactionEntry, FARegister>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    context.AddScreenConfigurationFor((Func<BoundedTo<TransactionEntry, FARegister>.ScreenConfiguration.IStartConfigScreen, BoundedTo<TransactionEntry, FARegister>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<TransactionEntry, FARegister>.ScreenConfiguration.IConfigured) ((BoundedTo<TransactionEntry, FARegister>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<FARegister.status>().AddDefaultFlow((Func<BoundedTo<TransactionEntry, FARegister>.Workflow.INeedStatesFlow, BoundedTo<TransactionEntry, FARegister>.Workflow.IConfigured>) (flow => (BoundedTo<TransactionEntry, FARegister>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<TransactionEntry, FARegister>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<TransactionEntry, FARegister>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TransactionEntry, FARegister>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<TransactionEntry, FARegister>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<TransactionEntry, PXAutoAction<FARegister>>>) (g => g.initializeState))));
      fss.Add<FARegister.status.hold>((Func<BoundedTo<TransactionEntry, FARegister>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TransactionEntry, FARegister>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<TransactionEntry, FARegister>.BaseFlowStep.IConfigured) ((BoundedTo<TransactionEntry, FARegister>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<TransactionEntry, FARegister>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<TransactionEntry, PXAction<FARegister>>>) (g => g.releaseFromHold), (Func<BoundedTo<TransactionEntry, FARegister>.ActionState.IAllowOptionalConfig, BoundedTo<TransactionEntry, FARegister>.ActionState.IConfigured>) (a => (BoundedTo<TransactionEntry, FARegister>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)))))).WithEventHandlers((Action<BoundedTo<TransactionEntry, FARegister>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<TransactionEntry, PXWorkflowEventHandler<FARegister>>>) (g => g.OnUpdateStatus))))));
      fss.Add<FARegister.status.balanced>((Func<BoundedTo<TransactionEntry, FARegister>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TransactionEntry, FARegister>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<TransactionEntry, FARegister>.BaseFlowStep.IConfigured) ((BoundedTo<TransactionEntry, FARegister>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<TransactionEntry, FARegister>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<TransactionEntry, PXAction<FARegister>>>) (g => g.release), (Func<BoundedTo<TransactionEntry, FARegister>.ActionState.IAllowOptionalConfig, BoundedTo<TransactionEntry, FARegister>.ActionState.IConfigured>) (a => (BoundedTo<TransactionEntry, FARegister>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<TransactionEntry, PXAction<FARegister>>>) (g => g.putOnHold), (Func<BoundedTo<TransactionEntry, FARegister>.ActionState.IAllowOptionalConfig, BoundedTo<TransactionEntry, FARegister>.ActionState.IConfigured>) (a => (BoundedTo<TransactionEntry, FARegister>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
      }))).WithEventHandlers((Action<BoundedTo<TransactionEntry, FARegister>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<TransactionEntry, PXWorkflowEventHandler<FARegister>>>) (g => g.OnUpdateStatus));
        handlers.Add((Expression<Func<TransactionEntry, PXWorkflowEventHandler<FARegister>>>) (g => g.OnReleaseDocument));
      }))));
      fss.Add<FARegister.status.posted>((Func<BoundedTo<TransactionEntry, FARegister>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TransactionEntry, FARegister>.BaseFlowStep.IConfigured>) null);
      fss.Add<FARegister.status.unposted>((Func<BoundedTo<TransactionEntry, FARegister>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TransactionEntry, FARegister>.BaseFlowStep.IConfigured>) null);
    })).WithTransitions((Action<BoundedTo<TransactionEntry, FARegister>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<TransactionEntry, FARegister>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<TransactionEntry, FARegister>.Transition.INeedTarget, BoundedTo<TransactionEntry, FARegister>.Transition.IConfigured>) (t => (BoundedTo<TransactionEntry, FARegister>.Transition.IConfigured) t.To<FARegister.status.hold>().IsTriggeredOn((Expression<Func<TransactionEntry, PXAction<FARegister>>>) (g => g.initializeState)).When((BoundedTo<TransactionEntry, FARegister>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<TransactionEntry, FARegister>.Transition.INeedTarget, BoundedTo<TransactionEntry, FARegister>.Transition.IConfigured>) (t => (BoundedTo<TransactionEntry, FARegister>.Transition.IConfigured) t.To<FARegister.status.balanced>().IsTriggeredOn((Expression<Func<TransactionEntry, PXAction<FARegister>>>) (g => g.initializeState)).When((BoundedTo<TransactionEntry, FARegister>.ISharedCondition) conditions.IsNotOnHold)));
      }));
      transitions.AddGroupFrom<FARegister.status.hold>((Action<BoundedTo<TransactionEntry, FARegister>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<TransactionEntry, FARegister>.Transition.INeedTarget, BoundedTo<TransactionEntry, FARegister>.Transition.IConfigured>) (t => (BoundedTo<TransactionEntry, FARegister>.Transition.IConfigured) t.To<FARegister.status.balanced>().IsTriggeredOn((Expression<Func<TransactionEntry, PXAction<FARegister>>>) (g => g.releaseFromHold)).WithFieldAssignments((Action<BoundedTo<TransactionEntry, FARegister>.Assignment.IContainerFillerFields>) (fas => fas.Add<FARegister.hold>((Func<BoundedTo<TransactionEntry, FARegister>.Assignment.INeedRightOperand, BoundedTo<TransactionEntry, FARegister>.Assignment.IConfigured>) (f => (BoundedTo<TransactionEntry, FARegister>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
        ts.Add((Func<BoundedTo<TransactionEntry, FARegister>.Transition.INeedTarget, BoundedTo<TransactionEntry, FARegister>.Transition.IConfigured>) (t => (BoundedTo<TransactionEntry, FARegister>.Transition.IConfigured) t.To<FARegister.status.balanced>().IsTriggeredOn((Expression<Func<TransactionEntry, PXWorkflowEventHandlerBase<FARegister>>>) (g => g.OnUpdateStatus)).When((BoundedTo<TransactionEntry, FARegister>.ISharedCondition) conditions.IsNotOnHold)));
      }));
      transitions.AddGroupFrom<FARegister.status.balanced>((Action<BoundedTo<TransactionEntry, FARegister>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<TransactionEntry, FARegister>.Transition.INeedTarget, BoundedTo<TransactionEntry, FARegister>.Transition.IConfigured>) (t => (BoundedTo<TransactionEntry, FARegister>.Transition.IConfigured) t.To<FARegister.status.hold>().IsTriggeredOn((Expression<Func<TransactionEntry, PXAction<FARegister>>>) (g => g.putOnHold)).WithFieldAssignments((Action<BoundedTo<TransactionEntry, FARegister>.Assignment.IContainerFillerFields>) (fas => fas.Add<FARegister.hold>((Func<BoundedTo<TransactionEntry, FARegister>.Assignment.INeedRightOperand, BoundedTo<TransactionEntry, FARegister>.Assignment.IConfigured>) (f => (BoundedTo<TransactionEntry, FARegister>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        ts.Add((Func<BoundedTo<TransactionEntry, FARegister>.Transition.INeedTarget, BoundedTo<TransactionEntry, FARegister>.Transition.IConfigured>) (t => (BoundedTo<TransactionEntry, FARegister>.Transition.IConfigured) t.To<FARegister.status.hold>().IsTriggeredOn((Expression<Func<TransactionEntry, PXWorkflowEventHandlerBase<FARegister>>>) (g => g.OnUpdateStatus)).When((BoundedTo<TransactionEntry, FARegister>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<TransactionEntry, FARegister>.Transition.INeedTarget, BoundedTo<TransactionEntry, FARegister>.Transition.IConfigured>) (t => (BoundedTo<TransactionEntry, FARegister>.Transition.IConfigured) t.To<FARegister.status.unposted>().IsTriggeredOn((Expression<Func<TransactionEntry, PXWorkflowEventHandlerBase<FARegister>>>) (g => g.OnReleaseDocument)).When((BoundedTo<TransactionEntry, FARegister>.ISharedCondition) conditions.IsReleased)));
        ts.Add((Func<BoundedTo<TransactionEntry, FARegister>.Transition.INeedTarget, BoundedTo<TransactionEntry, FARegister>.Transition.IConfigured>) (t => (BoundedTo<TransactionEntry, FARegister>.Transition.IConfigured) t.To<FARegister.status.posted>().IsTriggeredOn((Expression<Func<TransactionEntry, PXWorkflowEventHandlerBase<FARegister>>>) (g => g.OnReleaseDocument)).When((BoundedTo<TransactionEntry, FARegister>.ISharedCondition) conditions.IsPosted)));
      }));
      transitions.AddGroupFrom<FARegister.status.unposted>((Action<BoundedTo<TransactionEntry, FARegister>.Transition.ISourceContainerFillerTransitions>) (ts => { }));
      transitions.AddGroupFrom<FARegister.status.posted>((Action<BoundedTo<TransactionEntry, FARegister>.Transition.ISourceContainerFillerTransitions>) (ts => { }));
    })))).WithActions((Action<BoundedTo<TransactionEntry, FARegister>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<TransactionEntry, PXAction<FARegister>>>) (g => g.initializeState), (Func<BoundedTo<TransactionEntry, FARegister>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TransactionEntry, FARegister>.ActionDefinition.IConfigured>) (a => (BoundedTo<TransactionEntry, FARegister>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<TransactionEntry, PXAction<FARegister>>>) (g => g.releaseFromHold), (Func<BoundedTo<TransactionEntry, FARegister>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TransactionEntry, FARegister>.ActionDefinition.IConfigured>) (c => (BoundedTo<TransactionEntry, FARegister>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<TransactionEntry, FARegister>.Assignment.IContainerFillerFields>) (fas => fas.Add<FARegister.hold>((Func<BoundedTo<TransactionEntry, FARegister>.Assignment.INeedRightOperand, BoundedTo<TransactionEntry, FARegister>.Assignment.IConfigured>) (f => (BoundedTo<TransactionEntry, FARegister>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<TransactionEntry, PXAction<FARegister>>>) (g => g.putOnHold), (Func<BoundedTo<TransactionEntry, FARegister>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TransactionEntry, FARegister>.ActionDefinition.IConfigured>) (c => (BoundedTo<TransactionEntry, FARegister>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<TransactionEntry, FARegister>.Assignment.IContainerFillerFields>) (fas => fas.Add<FARegister.hold>((Func<BoundedTo<TransactionEntry, FARegister>.Assignment.INeedRightOperand, BoundedTo<TransactionEntry, FARegister>.Assignment.IConfigured>) (f => (BoundedTo<TransactionEntry, FARegister>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<TransactionEntry, PXAction<FARegister>>>) (g => g.release), (Func<BoundedTo<TransactionEntry, FARegister>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TransactionEntry, FARegister>.ActionDefinition.IConfigured>) (c => (BoundedTo<TransactionEntry, FARegister>.ActionDefinition.IConfigured) c.InFolder(processingCategory).PlaceAfter("Last")));
    })).WithHandlers((Action<BoundedTo<TransactionEntry, FARegister>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers =>
    {
      handlers.Add((Func<BoundedTo<TransactionEntry, FARegister>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<TransactionEntry, FARegister>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<TransactionEntry, FARegister>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<TransactionEntry, FARegister>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<FARegister>) ((BoundedTo<TransactionEntry, FARegister>.WorkflowEventHandlerDefinition.INeedSubscriber<FARegister>) handler.WithTargetOf<FARegister>().OfFieldsUpdated<TypeArrayOf<IBqlField>.FilledWith<FARegister.hold, FARegister.released, FARegister.posted>>()).Is((Expression<Func<FARegister, PXWorkflowEventHandler<FARegister, FARegister>>>) (g => g.OnUpdateStatus))).UsesTargetAsPrimaryEntity()));
      handlers.Add((Func<BoundedTo<TransactionEntry, FARegister>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<TransactionEntry, FARegister>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<TransactionEntry, FARegister>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<TransactionEntry, FARegister>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<FARegister>) ((BoundedTo<TransactionEntry, FARegister>.WorkflowEventHandlerDefinition.INeedSubscriber<FARegister>) handler.WithTargetOf<FARegister>().OfEntityEvent<FARegister.Events>((Expression<Func<FARegister.Events, PXEntityEvent<FARegister>>>) (e => e.ReleaseDocument))).Is((Expression<Func<FARegister, PXWorkflowEventHandler<FARegister, FARegister>>>) (g => g.OnReleaseDocument))).UsesTargetAsPrimaryEntity()));
    })).WithCategories((Action<BoundedTo<TransactionEntry, FARegister>.ActionCategory.IContainerFillerCategories>) (categories => categories.Add(processingCategory)))));
  }

  public class Conditions : BoundedTo<TransactionEntry, FARegister>.Condition.Pack
  {
    public BoundedTo<TransactionEntry, FARegister>.Condition IsOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<TransactionEntry, FARegister>.Condition.ConditionBuilder, BoundedTo<TransactionEntry, FARegister>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FARegister.hold, Equal<True>>>>>.And<BqlOperand<FARegister.released, IBqlBool>.IsEqual<False>>>()), nameof (IsOnHold));
      }
    }

    public BoundedTo<TransactionEntry, FARegister>.Condition IsNotOnHold
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<TransactionEntry, FARegister>.Condition.ConditionBuilder, BoundedTo<TransactionEntry, FARegister>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FARegister.hold, Equal<False>>>>>.And<BqlOperand<FARegister.released, IBqlBool>.IsEqual<False>>>()), nameof (IsNotOnHold));
      }
    }

    public BoundedTo<TransactionEntry, FARegister>.Condition IsReleased
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<TransactionEntry, FARegister>.Condition.ConditionBuilder, BoundedTo<TransactionEntry, FARegister>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FARegister.posted, Equal<False>>>>>.And<BqlOperand<FARegister.released, IBqlBool>.IsEqual<True>>>()), nameof (IsReleased));
      }
    }

    public BoundedTo<TransactionEntry, FARegister>.Condition IsPosted
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<TransactionEntry, FARegister>.Condition.ConditionBuilder, BoundedTo<TransactionEntry, FARegister>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FARegister.posted, Equal<True>>>>>.And<BqlOperand<FARegister.released, IBqlBool>.IsEqual<True>>>()), nameof (IsPosted));
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
