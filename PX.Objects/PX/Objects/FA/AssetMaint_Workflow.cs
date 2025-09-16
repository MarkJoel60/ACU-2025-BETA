// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.AssetMaint_Workflow
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

public class AssetMaint_Workflow : PXGraphExtension<AssetMaint>
{
  [PXWorkflowDependsOnType(new Type[] {typeof (FASetup)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    AssetMaint_Workflow.Configure(config.GetScreenConfigurationContext<AssetMaint, FixedAsset>());
  }

  protected static void Configure(WorkflowContext<AssetMaint, FixedAsset> context)
  {
    AssetMaint_Workflow.Conditions conditions = context.Conditions.GetPack<AssetMaint_Workflow.Conditions>();
    BoundedTo<AssetMaint, FixedAsset>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<AssetMaint, FixedAsset>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<AssetMaint, FixedAsset>.ActionCategory.IConfigured>) (category => (BoundedTo<AssetMaint, FixedAsset>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<AssetMaint, FixedAsset>.ActionCategory.IConfigured correctionsCategory = context.Categories.CreateNew("Corrections", (Func<BoundedTo<AssetMaint, FixedAsset>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<AssetMaint, FixedAsset>.ActionCategory.IConfigured>) (category => (BoundedTo<AssetMaint, FixedAsset>.ActionCategory.IConfigured) category.DisplayName("Corrections")));
    context.AddScreenConfigurationFor((Func<BoundedTo<AssetMaint, FixedAsset>.ScreenConfiguration.IStartConfigScreen, BoundedTo<AssetMaint, FixedAsset>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<AssetMaint, FixedAsset>.ScreenConfiguration.IConfigured) ((BoundedTo<AssetMaint, FixedAsset>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<FixedAsset.status>().AddDefaultFlow((Func<BoundedTo<AssetMaint, FixedAsset>.Workflow.INeedStatesFlow, BoundedTo<AssetMaint, FixedAsset>.Workflow.IConfigured>) (flow => (BoundedTo<AssetMaint, FixedAsset>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<AssetMaint, FixedAsset>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<AssetMaint, FixedAsset>.FlowState.INeedAnyFlowStateConfig, BoundedTo<AssetMaint, FixedAsset>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<AssetMaint, FixedAsset>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<AssetMaint, PXAutoAction<FixedAsset>>>) (g => g.initializeState))));
      fss.Add<FixedAssetStatus.hold>((Func<BoundedTo<AssetMaint, FixedAsset>.FlowState.INeedAnyFlowStateConfig, BoundedTo<AssetMaint, FixedAsset>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<AssetMaint, FixedAsset>.BaseFlowStep.IConfigured) ((BoundedTo<AssetMaint, FixedAsset>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<AssetMaint, FixedAsset>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.releaseFromHold), (Func<BoundedTo<AssetMaint, FixedAsset>.ActionState.IAllowOptionalConfig, BoundedTo<AssetMaint, FixedAsset>.ActionState.IConfigured>) (a => (BoundedTo<AssetMaint, FixedAsset>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)))))).WithEventHandlers((Action<BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<AssetMaint, PXWorkflowEventHandler<FixedAsset>>>) (g => g.OnUpdateStatus));
        handlers.Add((Expression<Func<AssetMaint, PXWorkflowEventHandler<FixedAsset>>>) (g => g.OnActivateAsset));
        handlers.Add((Expression<Func<AssetMaint, PXWorkflowEventHandler<FixedAsset>>>) (g => g.OnSuspendAsset));
      }))));
      fss.Add<FixedAssetStatus.active>((Func<BoundedTo<AssetMaint, FixedAsset>.FlowState.INeedAnyFlowStateConfig, BoundedTo<AssetMaint, FixedAsset>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<AssetMaint, FixedAsset>.BaseFlowStep.IConfigured) ((BoundedTo<AssetMaint, FixedAsset>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<AssetMaint, FixedAsset>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.putOnHold), (Func<BoundedTo<AssetMaint, FixedAsset>.ActionState.IAllowOptionalConfig, BoundedTo<AssetMaint, FixedAsset>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.runDisposal), (Func<BoundedTo<AssetMaint, FixedAsset>.ActionState.IAllowOptionalConfig, BoundedTo<AssetMaint, FixedAsset>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.Suspend), (Func<BoundedTo<AssetMaint, FixedAsset>.ActionState.IAllowOptionalConfig, BoundedTo<AssetMaint, FixedAsset>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.runReversal), (Func<BoundedTo<AssetMaint, FixedAsset>.ActionState.IAllowOptionalConfig, BoundedTo<AssetMaint, FixedAsset>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.CalculateDepreciation), (Func<BoundedTo<AssetMaint, FixedAsset>.ActionState.IAllowOptionalConfig, BoundedTo<AssetMaint, FixedAsset>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.runSplit), (Func<BoundedTo<AssetMaint, FixedAsset>.ActionState.IAllowOptionalConfig, BoundedTo<AssetMaint, FixedAsset>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<AssetMaint, PXWorkflowEventHandler<FixedAsset>>>) (g => g.OnUpdateStatus));
        handlers.Add((Expression<Func<AssetMaint, PXWorkflowEventHandler<FixedAsset>>>) (g => g.OnDisposeAsset));
        handlers.Add((Expression<Func<AssetMaint, PXWorkflowEventHandler<FixedAsset>>>) (g => g.OnSuspendAsset));
        handlers.Add((Expression<Func<AssetMaint, PXWorkflowEventHandler<FixedAsset>>>) (g => g.OnReverseAsset));
        handlers.Add((Expression<Func<AssetMaint, PXWorkflowEventHandler<FixedAsset>>>) (g => g.OnFullyDepreciateAsset));
      }))));
      fss.Add<FixedAssetStatus.suspended>((Func<BoundedTo<AssetMaint, FixedAsset>.FlowState.INeedAnyFlowStateConfig, BoundedTo<AssetMaint, FixedAsset>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<AssetMaint, FixedAsset>.BaseFlowStep.IConfigured) ((BoundedTo<AssetMaint, FixedAsset>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<AssetMaint, FixedAsset>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.Suspend), (Func<BoundedTo<AssetMaint, FixedAsset>.ActionState.IAllowOptionalConfig, BoundedTo<AssetMaint, FixedAsset>.ActionState.IConfigured>) (a => (BoundedTo<AssetMaint, FixedAsset>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)))))).WithEventHandlers((Action<BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<AssetMaint, PXWorkflowEventHandler<FixedAsset>>>) (g => g.OnActivateAsset))))));
      fss.Add<FixedAssetStatus.fullyDepreciated>((Func<BoundedTo<AssetMaint, FixedAsset>.FlowState.INeedAnyFlowStateConfig, BoundedTo<AssetMaint, FixedAsset>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<AssetMaint, FixedAsset>.BaseFlowStep.IConfigured) ((BoundedTo<AssetMaint, FixedAsset>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<AssetMaint, FixedAsset>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.runDisposal), (Func<BoundedTo<AssetMaint, FixedAsset>.ActionState.IAllowOptionalConfig, BoundedTo<AssetMaint, FixedAsset>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.runReversal), (Func<BoundedTo<AssetMaint, FixedAsset>.ActionState.IAllowOptionalConfig, BoundedTo<AssetMaint, FixedAsset>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.runSplit), (Func<BoundedTo<AssetMaint, FixedAsset>.ActionState.IAllowOptionalConfig, BoundedTo<AssetMaint, FixedAsset>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<AssetMaint, PXWorkflowEventHandler<FixedAsset>>>) (g => g.OnDisposeAsset));
        handlers.Add((Expression<Func<AssetMaint, PXWorkflowEventHandler<FixedAsset>>>) (g => g.OnReverseAsset));
        handlers.Add((Expression<Func<AssetMaint, PXWorkflowEventHandler<FixedAsset>>>) (g => g.OnActivateAsset));
        handlers.Add((Expression<Func<AssetMaint, PXWorkflowEventHandler<FixedAsset>>>) (g => g.OnNotFullyDepreciateAsset));
      }))));
      fss.Add<FixedAssetStatus.disposed>((Func<BoundedTo<AssetMaint, FixedAsset>.FlowState.INeedAnyFlowStateConfig, BoundedTo<AssetMaint, FixedAsset>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<AssetMaint, FixedAsset>.BaseFlowStep.IConfigured) ((BoundedTo<AssetMaint, FixedAsset>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<AssetMaint, FixedAsset>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.runDispReversal), (Func<BoundedTo<AssetMaint, FixedAsset>.ActionState.IAllowOptionalConfig, BoundedTo<AssetMaint, FixedAsset>.ActionState.IConfigured>) null)))).WithEventHandlers((Action<BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<AssetMaint, PXWorkflowEventHandler<FixedAsset>>>) (g => g.OnDisposeAsset));
        handlers.Add((Expression<Func<AssetMaint, PXWorkflowEventHandler<FixedAsset>>>) (g => g.OnFullyDepreciateAsset));
        handlers.Add((Expression<Func<AssetMaint, PXWorkflowEventHandler<FixedAsset>>>) (g => g.OnActivateAsset));
      }))));
      fss.Add<FixedAssetStatus.reversed>((Func<BoundedTo<AssetMaint, FixedAsset>.FlowState.INeedAnyFlowStateConfig, BoundedTo<AssetMaint, FixedAsset>.BaseFlowStep.IConfigured>) null);
    })).WithTransitions((Action<BoundedTo<AssetMaint, FixedAsset>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<AssetMaint, FixedAsset>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<AssetMaint, FixedAsset>.Transition.INeedTarget, BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured>) (t => (BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured) t.To<FixedAssetStatus.active>().IsTriggeredOn((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.initializeState))))));
      transitions.AddGroupFrom<FixedAssetStatus.hold>((Action<BoundedTo<AssetMaint, FixedAsset>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<AssetMaint, FixedAsset>.Transition.INeedTarget, BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured>) (t => (BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured) t.To<FixedAssetStatus.active>().IsTriggeredOn((Expression<Func<AssetMaint, PXWorkflowEventHandlerBase<FixedAsset>>>) (g => g.OnUpdateStatus)).When((BoundedTo<AssetMaint, FixedAsset>.ISharedCondition) conditions.NotHoldEntry)));
        ts.Add((Func<BoundedTo<AssetMaint, FixedAsset>.Transition.INeedTarget, BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured>) (t => (BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured) t.To<FixedAssetStatus.active>().IsTriggeredOn((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.releaseFromHold)).DoesNotPersist()));
        ts.Add((Func<BoundedTo<AssetMaint, FixedAsset>.Transition.INeedTarget, BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured>) (t => (BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured) t.To<FixedAssetStatus.active>().IsTriggeredOn((Expression<Func<AssetMaint, PXWorkflowEventHandlerBase<FixedAsset>>>) (g => g.OnActivateAsset))));
        ts.Add((Func<BoundedTo<AssetMaint, FixedAsset>.Transition.INeedTarget, BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured>) (t => (BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured) t.To<FixedAssetStatus.suspended>().IsTriggeredOn((Expression<Func<AssetMaint, PXWorkflowEventHandlerBase<FixedAsset>>>) (g => g.OnSuspendAsset))));
      }));
      transitions.AddGroupFrom<FixedAssetStatus.active>((Action<BoundedTo<AssetMaint, FixedAsset>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<AssetMaint, FixedAsset>.Transition.INeedTarget, BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured>) (t => (BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured) t.To<FixedAssetStatus.hold>().IsTriggeredOn((Expression<Func<AssetMaint, PXWorkflowEventHandlerBase<FixedAsset>>>) (g => g.OnUpdateStatus)).When((BoundedTo<AssetMaint, FixedAsset>.ISharedCondition) conditions.HoldEntry)));
        ts.Add((Func<BoundedTo<AssetMaint, FixedAsset>.Transition.INeedTarget, BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured>) (t => (BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured) t.To<FixedAssetStatus.hold>().IsTriggeredOn((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.putOnHold)).DoesNotPersist()));
        ts.Add((Func<BoundedTo<AssetMaint, FixedAsset>.Transition.INeedTarget, BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured>) (t => (BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured) t.To<FixedAssetStatus.disposed>().IsTriggeredOn((Expression<Func<AssetMaint, PXWorkflowEventHandlerBase<FixedAsset>>>) (g => g.OnDisposeAsset))));
        ts.Add((Func<BoundedTo<AssetMaint, FixedAsset>.Transition.INeedTarget, BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured>) (t => (BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured) t.To<FixedAssetStatus.suspended>().IsTriggeredOn((Expression<Func<AssetMaint, PXWorkflowEventHandlerBase<FixedAsset>>>) (g => g.OnSuspendAsset)).WithFieldAssignments((Action<BoundedTo<AssetMaint, FixedAsset>.Assignment.IContainerFillerFields>) (fas => fas.Add<FixedAsset.suspended>((Func<BoundedTo<AssetMaint, FixedAsset>.Assignment.INeedRightOperand, BoundedTo<AssetMaint, FixedAsset>.Assignment.IConfigured>) (f => (BoundedTo<AssetMaint, FixedAsset>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        ts.Add((Func<BoundedTo<AssetMaint, FixedAsset>.Transition.INeedTarget, BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured>) (t => (BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured) t.To<FixedAssetStatus.reversed>().IsTriggeredOn((Expression<Func<AssetMaint, PXWorkflowEventHandlerBase<FixedAsset>>>) (g => g.OnReverseAsset))));
        ts.Add((Func<BoundedTo<AssetMaint, FixedAsset>.Transition.INeedTarget, BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured>) (t => (BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured) t.To<FixedAssetStatus.fullyDepreciated>().IsTriggeredOn((Expression<Func<AssetMaint, PXWorkflowEventHandlerBase<FixedAsset>>>) (g => g.OnFullyDepreciateAsset))));
      }));
      transitions.AddGroupFrom<FixedAssetStatus.suspended>((Action<BoundedTo<AssetMaint, FixedAsset>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<AssetMaint, FixedAsset>.Transition.INeedTarget, BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured>) (t => (BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured) t.To<FixedAssetStatus.active>().IsTriggeredOn((Expression<Func<AssetMaint, PXWorkflowEventHandlerBase<FixedAsset>>>) (g => g.OnActivateAsset)).WithFieldAssignments((Action<BoundedTo<AssetMaint, FixedAsset>.Assignment.IContainerFillerFields>) (fas => fas.Add<FixedAsset.suspended>((Func<BoundedTo<AssetMaint, FixedAsset>.Assignment.INeedRightOperand, BoundedTo<AssetMaint, FixedAsset>.Assignment.IConfigured>) (f => (BoundedTo<AssetMaint, FixedAsset>.Assignment.IConfigured) f.SetFromValue((object) false)))))))));
      transitions.AddGroupFrom<FixedAssetStatus.fullyDepreciated>((Action<BoundedTo<AssetMaint, FixedAsset>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<AssetMaint, FixedAsset>.Transition.INeedTarget, BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured>) (t => (BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured) t.To<FixedAssetStatus.disposed>().IsTriggeredOn((Expression<Func<AssetMaint, PXWorkflowEventHandlerBase<FixedAsset>>>) (g => g.OnDisposeAsset))));
        ts.Add((Func<BoundedTo<AssetMaint, FixedAsset>.Transition.INeedTarget, BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured>) (t => (BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured) t.To<FixedAssetStatus.reversed>().IsTriggeredOn((Expression<Func<AssetMaint, PXWorkflowEventHandlerBase<FixedAsset>>>) (g => g.OnReverseAsset))));
        ts.Add((Func<BoundedTo<AssetMaint, FixedAsset>.Transition.INeedTarget, BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured>) (t => (BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured) t.To<FixedAssetStatus.active>().IsTriggeredOn((Expression<Func<AssetMaint, PXWorkflowEventHandlerBase<FixedAsset>>>) (g => g.OnActivateAsset))));
        ts.Add((Func<BoundedTo<AssetMaint, FixedAsset>.Transition.INeedTarget, BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured>) (t => (BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured) t.To<FixedAssetStatus.active>().IsTriggeredOn((Expression<Func<AssetMaint, PXWorkflowEventHandlerBase<FixedAsset>>>) (g => g.OnNotFullyDepreciateAsset))));
      }));
      transitions.AddGroupFrom<FixedAssetStatus.reversed>((Action<BoundedTo<AssetMaint, FixedAsset>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<AssetMaint, FixedAsset>.Transition.INeedTarget, BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured>) (t => (BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured) t.To<FixedAssetStatus.active>().IsTriggeredOn((Expression<Func<AssetMaint, PXWorkflowEventHandlerBase<FixedAsset>>>) (g => g.OnActivateAsset))))));
      transitions.AddGroupFrom<FixedAssetStatus.disposed>((Action<BoundedTo<AssetMaint, FixedAsset>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<AssetMaint, FixedAsset>.Transition.INeedTarget, BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured>) (t => (BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured) t.To<FixedAssetStatus.active>().IsTriggeredOn((Expression<Func<AssetMaint, PXWorkflowEventHandlerBase<FixedAsset>>>) (g => g.OnActivateAsset))));
        ts.Add((Func<BoundedTo<AssetMaint, FixedAsset>.Transition.INeedTarget, BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured>) (t => (BoundedTo<AssetMaint, FixedAsset>.Transition.IConfigured) t.To<FixedAssetStatus.fullyDepreciated>().IsTriggeredOn((Expression<Func<AssetMaint, PXWorkflowEventHandlerBase<FixedAsset>>>) (g => g.OnFullyDepreciateAsset))));
      }));
    })))).WithActions((Action<BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.initializeState), (Func<BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IConfigured>) (a => (BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.releaseFromHold), (Func<BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IConfigured>) (c => (BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1)));
      actions.Add((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.putOnHold), (Func<BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IConfigured>) (c => (BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithPersistOptions((ActionPersistOptions) 1)));
      actions.Add((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.runDisposal), (Func<BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IConfigured>) (c => (BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IConfigured) c.InFolder(processingCategory).IsDisabledWhen((BoundedTo<AssetMaint, FixedAsset>.ISharedCondition) conditions.IsNotUpdateGL).MassProcessingScreen<DisposalProcess>().InBatchMode()));
      actions.Add((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.runDispReversal), (Func<BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IConfigured>) (c => (BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IConfigured) c.InFolder(correctionsCategory).IsDisabledWhen((BoundedTo<AssetMaint, FixedAsset>.ISharedCondition) conditions.IsNotUpdateGL)));
      actions.Add((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.Suspend), (Func<BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c.InFolder(processingCategory);
        BoundedTo<AssetMaint, FixedAsset>.Condition isNotUpdateGl = conditions.IsNotUpdateGL;
        BoundedTo<AssetMaint, FixedAsset>.Condition condition = BoundedTo<AssetMaint, FixedAsset>.Condition.op_True(isNotUpdateGl) ? isNotUpdateGl : BoundedTo<AssetMaint, FixedAsset>.Condition.op_BitwiseOr(isNotUpdateGl, conditions.IsUnderConstruction);
        return (BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IConfigured) optionalConfigAction.IsDisabledWhen((BoundedTo<AssetMaint, FixedAsset>.ISharedCondition) condition);
      }));
      actions.Add((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.runReversal), (Func<BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IConfigured>) (c => (BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IConfigured) c.InFolder(correctionsCategory).IsDisabledWhen((BoundedTo<AssetMaint, FixedAsset>.ISharedCondition) conditions.IsNotUpdateGL)));
      actions.Add((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.CalculateDepreciation), (Func<BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IConfigured>) (c => (BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IConfigured) c.InFolder(processingCategory).IsDisabledWhen((BoundedTo<AssetMaint, FixedAsset>.ISharedCondition) conditions.IsNotDepreciable)));
      actions.Add((Expression<Func<AssetMaint, PXAction<FixedAsset>>>) (g => g.runSplit), (Func<BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IConfigured>) (c => (BoundedTo<AssetMaint, FixedAsset>.ActionDefinition.IConfigured) c.InFolder(processingCategory).IsDisabledWhen((BoundedTo<AssetMaint, FixedAsset>.ISharedCondition) conditions.IsNotUpdateGL).MassProcessingScreen<SplitProcess>().InBatchMode()));
    })).WithHandlers((Action<BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers =>
    {
      handlers.Add((Func<BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<FixedAsset>) ((BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.INeedSubscriber<FixedAsset>) handler.WithTargetOf<FixedAsset>().OfFieldUpdated<FixedAsset.classID>()).Is((Expression<Func<FixedAsset, PXWorkflowEventHandler<FixedAsset, FixedAsset>>>) (g => g.OnUpdateStatus))).UsesTargetAsPrimaryEntity()));
      handlers.Add((Func<BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<FixedAsset>) ((BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<FixedAsset>) ((BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.INeedSubscriber<FixedAsset>) handler.WithTargetOf<FixedAsset>().OfEntityEvent<FixedAsset.Events>((Expression<Func<FixedAsset.Events, PXEntityEvent<FixedAsset>>>) (e => e.ActivateAsset))).Is((Expression<Func<FixedAsset, PXWorkflowEventHandler<FixedAsset, FixedAsset>>>) (g => g.OnActivateAsset))).UsesTargetAsPrimaryEntity()).WithFieldAssignments((Action<BoundedTo<FixedAsset, FixedAsset>.Assignment.IContainerFillerFields>) (fas =>
      {
        fas.Add<FixedAsset.active>((Func<BoundedTo<AssetMaint, FixedAsset>.Assignment.INeedRightOperand, BoundedTo<AssetMaint, FixedAsset>.Assignment.IConfigured>) (f => (BoundedTo<AssetMaint, FixedAsset>.Assignment.IConfigured) f.SetFromValue((object) true)));
        fas.Add<FixedAsset.suspended>((Func<BoundedTo<AssetMaint, FixedAsset>.Assignment.INeedRightOperand, BoundedTo<AssetMaint, FixedAsset>.Assignment.IConfigured>) (f => (BoundedTo<AssetMaint, FixedAsset>.Assignment.IConfigured) f.SetFromValue((object) false)));
      }))));
      handlers.Add((Func<BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<FixedAsset>) ((BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<FixedAsset>) ((BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.INeedSubscriber<FixedAsset>) handler.WithTargetOf<FixedAsset>().OfEntityEvent<FixedAsset.Events>((Expression<Func<FixedAsset.Events, PXEntityEvent<FixedAsset>>>) (e => e.DisposeAsset))).Is((Expression<Func<FixedAsset, PXWorkflowEventHandler<FixedAsset, FixedAsset>>>) (g => g.OnDisposeAsset))).UsesTargetAsPrimaryEntity()).WithFieldAssignments((Action<BoundedTo<FixedAsset, FixedAsset>.Assignment.IContainerFillerFields>) (fas => fas.Add<FixedAsset.suspended>((Func<BoundedTo<AssetMaint, FixedAsset>.Assignment.INeedRightOperand, BoundedTo<AssetMaint, FixedAsset>.Assignment.IConfigured>) (f => (BoundedTo<AssetMaint, FixedAsset>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      handlers.Add((Func<BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<FixedAsset>) ((BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.INeedSubscriber<FixedAsset>) handler.WithTargetOf<FixedAsset>().OfEntityEvent<FixedAsset.Events>((Expression<Func<FixedAsset.Events, PXEntityEvent<FixedAsset>>>) (e => e.SuspendAsset))).Is((Expression<Func<FixedAsset, PXWorkflowEventHandler<FixedAsset, FixedAsset>>>) (g => g.OnSuspendAsset))).UsesTargetAsPrimaryEntity()));
      handlers.Add((Func<BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<FixedAsset>) ((BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.INeedSubscriber<FixedAsset>) handler.WithTargetOf<FixedAsset>().OfEntityEvent<FixedAsset.Events>((Expression<Func<FixedAsset.Events, PXEntityEvent<FixedAsset>>>) (e => e.FullyDepreciateAsset))).Is((Expression<Func<FixedAsset, PXWorkflowEventHandler<FixedAsset, FixedAsset>>>) (g => g.OnFullyDepreciateAsset))).UsesTargetAsPrimaryEntity()));
      handlers.Add((Func<BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<FixedAsset>) ((BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.INeedSubscriber<FixedAsset>) handler.WithTargetOf<FixedAsset>().OfEntityEvent<FixedAsset.Events>((Expression<Func<FixedAsset.Events, PXEntityEvent<FixedAsset>>>) (e => e.ReverseAsset))).Is((Expression<Func<FixedAsset, PXWorkflowEventHandler<FixedAsset, FixedAsset>>>) (g => g.OnReverseAsset))).UsesTargetAsPrimaryEntity()));
      handlers.Add((Func<BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<FixedAsset>) ((BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<FixedAsset>) ((BoundedTo<AssetMaint, FixedAsset>.WorkflowEventHandlerDefinition.INeedSubscriber<FixedAsset>) handler.WithTargetOf<FixedAsset>().OfEntityEvent<FixedAsset.Events>((Expression<Func<FixedAsset.Events, PXEntityEvent<FixedAsset>>>) (e => e.NotFullyDepreciateAsset))).Is((Expression<Func<FixedAsset, PXWorkflowEventHandler<FixedAsset, FixedAsset>>>) (g => g.OnNotFullyDepreciateAsset))).UsesTargetAsPrimaryEntity()).WithFieldAssignments((Action<BoundedTo<FixedAsset, FixedAsset>.Assignment.IContainerFillerFields>) (fas => fas.Add<FixedAsset.active>((Func<BoundedTo<AssetMaint, FixedAsset>.Assignment.INeedRightOperand, BoundedTo<AssetMaint, FixedAsset>.Assignment.IConfigured>) (f => (BoundedTo<AssetMaint, FixedAsset>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
    })).WithCategories((Action<BoundedTo<AssetMaint, FixedAsset>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(correctionsCategory);
    }))));
  }

  public class Conditions : BoundedTo<AssetMaint, FixedAsset>.Condition.Pack
  {
    private readonly FASetupDefinition _Definition = FASetupDefinition.GetSlot();

    public BoundedTo<AssetMaint, FixedAsset>.Condition HoldEntry
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<AssetMaint, FixedAsset>.Condition.ConditionBuilder, BoundedTo<AssetMaint, FixedAsset>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FixedAsset.holdEntry, Equal<True>>>>>.And<BqlOperand<FixedAsset.isAcquired, IBqlBool>.IsNull>>()), nameof (HoldEntry));
      }
    }

    public BoundedTo<AssetMaint, FixedAsset>.Condition NotHoldEntry
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<AssetMaint, FixedAsset>.Condition.ConditionBuilder, BoundedTo<AssetMaint, FixedAsset>.Condition>) (c => c.FromBql<BqlOperand<FixedAsset.holdEntry, IBqlBool>.IsEqual<False>>()), nameof (NotHoldEntry));
      }
    }

    public BoundedTo<AssetMaint, FixedAsset>.Condition IsUnderConstruction
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<AssetMaint, FixedAsset>.Condition.ConditionBuilder, BoundedTo<AssetMaint, FixedAsset>.Condition>) (c => c.FromBql<BqlOperand<FixedAsset.underConstruction, IBqlBool>.IsEqual<True>>()), nameof (IsUnderConstruction));
      }
    }

    public BoundedTo<AssetMaint, FixedAsset>.Condition IsNotDepreciable
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<AssetMaint, FixedAsset>.Condition.ConditionBuilder, BoundedTo<AssetMaint, FixedAsset>.Condition>) (c => c.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FixedAsset.depreciable, NotEqual<True>>>>>.Or<BqlOperand<FixedAsset.underConstruction, IBqlBool>.IsEqual<True>>>()), nameof (IsNotDepreciable));
      }
    }

    public BoundedTo<AssetMaint, FixedAsset>.Condition IsNotUpdateGL
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<AssetMaint, FixedAsset>.Condition.ConditionBuilder, BoundedTo<AssetMaint, FixedAsset>.Condition>) (c =>
        {
          bool? updateGl = this._Definition.UpdateGL;
          bool flag = false;
          return !(updateGl.GetValueOrDefault() == flag & updateGl.HasValue) ? c.FromBql<BqlOperand<True, IBqlBool>.IsEqual<False>>() : c.FromBql<BqlOperand<True, IBqlBool>.IsEqual<True>>();
        }), nameof (IsNotUpdateGL));
      }
    }
  }

  public static class ActionCategoryNames
  {
    public const string Processing = "Processing";
    public const string Corrections = "Corrections";
  }

  public static class ActionCategory
  {
    public const string Processing = "Processing";
    public const string Corrections = "Corrections";
  }
}
