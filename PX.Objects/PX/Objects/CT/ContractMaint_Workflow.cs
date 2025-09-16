// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractMaint_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CT;

public class ContractMaint_Workflow : PXGraphExtension<ContractMaint>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    ContractMaint_Workflow.Configure(config.GetScreenConfigurationContext<ContractMaint, Contract>());
  }

  protected static void Configure(WorkflowContext<ContractMaint, Contract> context)
  {
    BoundedTo<ContractMaint, Contract>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<ContractMaint, Contract>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ContractMaint, Contract>.ActionCategory.IConfigured>) (category => (BoundedTo<ContractMaint, Contract>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<ContractMaint, Contract>.ActionCategory.IConfigured customOtherCategory = context.Categories.CreateNew("CustomOther", (Func<BoundedTo<ContractMaint, Contract>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ContractMaint, Contract>.ActionCategory.IConfigured>) (category => (BoundedTo<ContractMaint, Contract>.ActionCategory.IConfigured) category.DisplayName("Other")));
    context.AddScreenConfigurationFor((Func<BoundedTo<ContractMaint, Contract>.ScreenConfiguration.IStartConfigScreen, BoundedTo<ContractMaint, Contract>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<ContractMaint, Contract>.ScreenConfiguration.IConfigured) ((BoundedTo<ContractMaint, Contract>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<Contract.status>().AddDefaultFlow((Func<BoundedTo<ContractMaint, Contract>.Workflow.INeedStatesFlow, BoundedTo<ContractMaint, Contract>.Workflow.IConfigured>) (flow => (BoundedTo<ContractMaint, Contract>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<ContractMaint, Contract>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<ContractMaint, Contract>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ContractMaint, Contract>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ContractMaint, Contract>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<ContractMaint, PXAutoAction<Contract>>>) (g => g.initializeState))));
      fss.Add<Contract.status.draft>((Func<BoundedTo<ContractMaint, Contract>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ContractMaint, Contract>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ContractMaint, Contract>.BaseFlowStep.IConfigured) ((BoundedTo<ContractMaint, Contract>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ContractMaint, Contract>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.setup), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) (a => (BoundedTo<ContractMaint, Contract>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.setupAndActivate), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.ChangeID), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<ContractMaint, Contract>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<ContractMaint, PXWorkflowEventHandler<Contract>>>) (g => g.OnSetupContract));
        handlers.Add((Expression<Func<ContractMaint, PXWorkflowEventHandler<Contract>>>) (g => g.OnActivateContract));
      }))));
      fss.Add<Contract.status.pendingActivation>((Func<BoundedTo<ContractMaint, Contract>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ContractMaint, Contract>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ContractMaint, Contract>.BaseFlowStep.IConfigured) ((BoundedTo<ContractMaint, Contract>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<ContractMaint, Contract>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ContractMaint, Contract>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.activate), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) (a => (BoundedTo<ContractMaint, Contract>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.undoBilling), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.ChangeID), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) null);
      }))).WithFieldStates((Action<BoundedTo<ContractMaint, Contract>.FieldState.IContainerFillerFields>) (fields =>
      {
        fields.AddField<Contract.startDate>((Func<BoundedTo<ContractMaint, Contract>.FieldState.INeedAnyConfigField, BoundedTo<ContractMaint, Contract>.FieldState.IConfigured>) (field => (BoundedTo<ContractMaint, Contract>.FieldState.IConfigured) field.IsDisabled()));
        fields.AddField<Contract.terminationDate>((Func<BoundedTo<ContractMaint, Contract>.FieldState.INeedAnyConfigField, BoundedTo<ContractMaint, Contract>.FieldState.IConfigured>) (field => (BoundedTo<ContractMaint, Contract>.FieldState.IConfigured) field.IsDisabled()));
      }))).WithEventHandlers((Action<BoundedTo<ContractMaint, Contract>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<ContractMaint, PXWorkflowEventHandler<Contract>>>) (g => g.OnActivateContract))))));
      fss.Add<Contract.status.active>((Func<BoundedTo<ContractMaint, Contract>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ContractMaint, Contract>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ContractMaint, Contract>.BaseFlowStep.IConfigured) ((BoundedTo<ContractMaint, Contract>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<ContractMaint, Contract>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ContractMaint, Contract>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.bill), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) (a => (BoundedTo<ContractMaint, Contract>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.renew), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.terminate), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.upgrade), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.undoBilling), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.ChangeID), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.viewUsage), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) null);
      }))).WithFieldStates((Action<BoundedTo<ContractMaint, Contract>.FieldState.IContainerFillerFields>) (fields =>
      {
        fields.AddField<Contract.startDate>((Func<BoundedTo<ContractMaint, Contract>.FieldState.INeedAnyConfigField, BoundedTo<ContractMaint, Contract>.FieldState.IConfigured>) (field => (BoundedTo<ContractMaint, Contract>.FieldState.IConfigured) field.IsDisabled()));
        fields.AddField<Contract.activationDate>((Func<BoundedTo<ContractMaint, Contract>.FieldState.INeedAnyConfigField, BoundedTo<ContractMaint, Contract>.FieldState.IConfigured>) (field => (BoundedTo<ContractMaint, Contract>.FieldState.IConfigured) field.IsDisabled()));
        fields.AddField<Contract.terminationDate>((Func<BoundedTo<ContractMaint, Contract>.FieldState.INeedAnyConfigField, BoundedTo<ContractMaint, Contract>.FieldState.IConfigured>) (field => (BoundedTo<ContractMaint, Contract>.FieldState.IConfigured) field.IsDisabled()));
        fields.AddField<Contract.pendingSetup>((Func<BoundedTo<ContractMaint, Contract>.FieldState.INeedAnyConfigField, BoundedTo<ContractMaint, Contract>.FieldState.IConfigured>) (field => (BoundedTo<ContractMaint, Contract>.FieldState.IConfigured) field.IsHidden()));
        fields.AddField<Contract.pendingRecurring>((Func<BoundedTo<ContractMaint, Contract>.FieldState.INeedAnyConfigField, BoundedTo<ContractMaint, Contract>.FieldState.IConfigured>) (field => (BoundedTo<ContractMaint, Contract>.FieldState.IConfigured) field.IsHidden()));
        fields.AddField<Contract.pendingRenewal>((Func<BoundedTo<ContractMaint, Contract>.FieldState.INeedAnyConfigField, BoundedTo<ContractMaint, Contract>.FieldState.IConfigured>) (field => (BoundedTo<ContractMaint, Contract>.FieldState.IConfigured) field.IsHidden()));
        fields.AddField<Contract.totalPending>((Func<BoundedTo<ContractMaint, Contract>.FieldState.INeedAnyConfigField, BoundedTo<ContractMaint, Contract>.FieldState.IConfigured>) (field => (BoundedTo<ContractMaint, Contract>.FieldState.IConfigured) field.IsHidden()));
        fields.AddField<Contract.discountID>((Func<BoundedTo<ContractMaint, Contract>.FieldState.INeedAnyConfigField, BoundedTo<ContractMaint, Contract>.FieldState.IConfigured>) (field => (BoundedTo<ContractMaint, Contract>.FieldState.IConfigured) field.IsDisabled()));
      }))).WithEventHandlers((Action<BoundedTo<ContractMaint, Contract>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<ContractMaint, PXWorkflowEventHandler<Contract>>>) (g => g.OnExpireContract));
        handlers.Add((Expression<Func<ContractMaint, PXWorkflowEventHandler<Contract>>>) (g => g.OnUpgradeContract));
        handlers.Add((Expression<Func<ContractMaint, PXWorkflowEventHandler<Contract>>>) (g => g.OnCancelContract));
      }))));
      fss.Add<Contract.status.expired>((Func<BoundedTo<ContractMaint, Contract>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ContractMaint, Contract>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ContractMaint, Contract>.BaseFlowStep.IConfigured) ((BoundedTo<ContractMaint, Contract>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<ContractMaint, Contract>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ContractMaint, Contract>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.renew), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) (a => (BoundedTo<ContractMaint, Contract>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.terminate), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.undoBilling), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.ChangeID), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) null);
      }))).WithFieldStates((Action<BoundedTo<ContractMaint, Contract>.FieldState.IContainerFillerFields>) (fields =>
      {
        fields.AddField<Contract.startDate>((Func<BoundedTo<ContractMaint, Contract>.FieldState.INeedAnyConfigField, BoundedTo<ContractMaint, Contract>.FieldState.IConfigured>) (field => (BoundedTo<ContractMaint, Contract>.FieldState.IConfigured) field.IsDisabled()));
        fields.AddField<Contract.activationDate>((Func<BoundedTo<ContractMaint, Contract>.FieldState.INeedAnyConfigField, BoundedTo<ContractMaint, Contract>.FieldState.IConfigured>) (field => (BoundedTo<ContractMaint, Contract>.FieldState.IConfigured) field.IsDisabled()));
        fields.AddField<Contract.discountID>((Func<BoundedTo<ContractMaint, Contract>.FieldState.INeedAnyConfigField, BoundedTo<ContractMaint, Contract>.FieldState.IConfigured>) (field => (BoundedTo<ContractMaint, Contract>.FieldState.IConfigured) field.IsDisabled()));
      }))).WithEventHandlers((Action<BoundedTo<ContractMaint, Contract>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add((Expression<Func<ContractMaint, PXWorkflowEventHandler<Contract>>>) (g => g.OnActivateContract));
        handlers.Add((Expression<Func<ContractMaint, PXWorkflowEventHandler<Contract>>>) (g => g.OnCancelContract));
      }))));
      fss.Add<Contract.status.inUpgrade>((Func<BoundedTo<ContractMaint, Contract>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ContractMaint, Contract>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ContractMaint, Contract>.BaseFlowStep.IConfigured) ((BoundedTo<ContractMaint, Contract>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<ContractMaint, Contract>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ContractMaint, Contract>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.activateUpgrade), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) (a => (BoundedTo<ContractMaint, Contract>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.bill), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.undoBilling), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.ChangeID), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.viewUsage), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) null);
      }))).WithFieldStates((Action<BoundedTo<ContractMaint, Contract>.FieldState.IContainerFillerFields>) (fields =>
      {
        fields.AddField<Contract.startDate>((Func<BoundedTo<ContractMaint, Contract>.FieldState.INeedAnyConfigField, BoundedTo<ContractMaint, Contract>.FieldState.IConfigured>) (field => (BoundedTo<ContractMaint, Contract>.FieldState.IConfigured) field.IsDisabled()));
        fields.AddField<Contract.terminationDate>((Func<BoundedTo<ContractMaint, Contract>.FieldState.INeedAnyConfigField, BoundedTo<ContractMaint, Contract>.FieldState.IConfigured>) (field => (BoundedTo<ContractMaint, Contract>.FieldState.IConfigured) field.IsDisabled()));
      }))).WithEventHandlers((Action<BoundedTo<ContractMaint, Contract>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<ContractMaint, PXWorkflowEventHandler<Contract>>>) (g => g.OnExpireContract))))));
      fss.Add<Contract.status.canceled>((Func<BoundedTo<ContractMaint, Contract>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ContractMaint, Contract>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ContractMaint, Contract>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ContractMaint, Contract>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.undoBilling), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.ChangeID), (Func<BoundedTo<ContractMaint, Contract>.ActionState.IAllowOptionalConfig, BoundedTo<ContractMaint, Contract>.ActionState.IConfigured>) null);
      }))));
    })).WithTransitions((Action<BoundedTo<ContractMaint, Contract>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<ContractMaint, Contract>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ContractMaint, Contract>.Transition.INeedTarget, BoundedTo<ContractMaint, Contract>.Transition.IConfigured>) (t => (BoundedTo<ContractMaint, Contract>.Transition.IConfigured) t.To<Contract.status.draft>().IsTriggeredOn((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.initializeState))))));
      transitions.AddGroupFrom<Contract.status.draft>((Action<BoundedTo<ContractMaint, Contract>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ContractMaint, Contract>.Transition.INeedTarget, BoundedTo<ContractMaint, Contract>.Transition.IConfigured>) (t => (BoundedTo<ContractMaint, Contract>.Transition.IConfigured) t.To<Contract.status.pendingActivation>().IsTriggeredOn((Expression<Func<ContractMaint, PXWorkflowEventHandlerBase<Contract>>>) (g => g.OnSetupContract))));
        ts.Add((Func<BoundedTo<ContractMaint, Contract>.Transition.INeedTarget, BoundedTo<ContractMaint, Contract>.Transition.IConfigured>) (t => (BoundedTo<ContractMaint, Contract>.Transition.IConfigured) t.To<Contract.status.active>().IsTriggeredOn((Expression<Func<ContractMaint, PXWorkflowEventHandlerBase<Contract>>>) (g => g.OnActivateContract))));
      }));
      transitions.AddGroupFrom<Contract.status.pendingActivation>((Action<BoundedTo<ContractMaint, Contract>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ContractMaint, Contract>.Transition.INeedTarget, BoundedTo<ContractMaint, Contract>.Transition.IConfigured>) (t => (BoundedTo<ContractMaint, Contract>.Transition.IConfigured) t.To<Contract.status.active>().IsTriggeredOn((Expression<Func<ContractMaint, PXWorkflowEventHandlerBase<Contract>>>) (g => g.OnActivateContract))))));
      transitions.AddGroupFrom<Contract.status.active>((Action<BoundedTo<ContractMaint, Contract>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ContractMaint, Contract>.Transition.INeedTarget, BoundedTo<ContractMaint, Contract>.Transition.IConfigured>) (t => (BoundedTo<ContractMaint, Contract>.Transition.IConfigured) t.To<Contract.status.expired>().IsTriggeredOn((Expression<Func<ContractMaint, PXWorkflowEventHandlerBase<Contract>>>) (g => g.OnExpireContract))));
        ts.Add((Func<BoundedTo<ContractMaint, Contract>.Transition.INeedTarget, BoundedTo<ContractMaint, Contract>.Transition.IConfigured>) (t => (BoundedTo<ContractMaint, Contract>.Transition.IConfigured) t.To<Contract.status.canceled>().IsTriggeredOn((Expression<Func<ContractMaint, PXWorkflowEventHandlerBase<Contract>>>) (g => g.OnCancelContract))));
        ts.Add((Func<BoundedTo<ContractMaint, Contract>.Transition.INeedTarget, BoundedTo<ContractMaint, Contract>.Transition.IConfigured>) (t => (BoundedTo<ContractMaint, Contract>.Transition.IConfigured) t.To<Contract.status.inUpgrade>().IsTriggeredOn((Expression<Func<ContractMaint, PXWorkflowEventHandlerBase<Contract>>>) (g => g.OnUpgradeContract))));
      }));
      transitions.AddGroupFrom<Contract.status.expired>((Action<BoundedTo<ContractMaint, Contract>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ContractMaint, Contract>.Transition.INeedTarget, BoundedTo<ContractMaint, Contract>.Transition.IConfigured>) (t => (BoundedTo<ContractMaint, Contract>.Transition.IConfigured) t.To<Contract.status.active>().IsTriggeredOn((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.renew))));
        ts.Add((Func<BoundedTo<ContractMaint, Contract>.Transition.INeedTarget, BoundedTo<ContractMaint, Contract>.Transition.IConfigured>) (t => (BoundedTo<ContractMaint, Contract>.Transition.IConfigured) t.To<Contract.status.canceled>().IsTriggeredOn((Expression<Func<ContractMaint, PXWorkflowEventHandlerBase<Contract>>>) (g => g.OnCancelContract))));
        ts.Add((Func<BoundedTo<ContractMaint, Contract>.Transition.INeedTarget, BoundedTo<ContractMaint, Contract>.Transition.IConfigured>) (t => (BoundedTo<ContractMaint, Contract>.Transition.IConfigured) t.To<Contract.status.active>().IsTriggeredOn((Expression<Func<ContractMaint, PXWorkflowEventHandlerBase<Contract>>>) (g => g.OnActivateContract))));
      }));
      transitions.AddGroupFrom<Contract.status.inUpgrade>((Action<BoundedTo<ContractMaint, Contract>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ContractMaint, Contract>.Transition.INeedTarget, BoundedTo<ContractMaint, Contract>.Transition.IConfigured>) (t => (BoundedTo<ContractMaint, Contract>.Transition.IConfigured) t.To<Contract.status.active>().IsTriggeredOn((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.activateUpgrade)).WithFieldAssignments((Action<BoundedTo<ContractMaint, Contract>.Assignment.IContainerFillerFields>) (fass => fass.Add<Contract.isPendingUpdate>((Func<BoundedTo<ContractMaint, Contract>.Assignment.INeedRightOperand, BoundedTo<ContractMaint, Contract>.Assignment.IConfigured>) (v => (BoundedTo<ContractMaint, Contract>.Assignment.IConfigured) v.SetFromValue((object) false)))))));
        ts.Add((Func<BoundedTo<ContractMaint, Contract>.Transition.INeedTarget, BoundedTo<ContractMaint, Contract>.Transition.IConfigured>) (t => (BoundedTo<ContractMaint, Contract>.Transition.IConfigured) t.To<Contract.status.expired>().IsTriggeredOn((Expression<Func<ContractMaint, PXWorkflowEventHandlerBase<Contract>>>) (g => g.OnExpireContract))));
      }));
      transitions.AddGroupFrom<Contract.status.canceled>((Action<BoundedTo<ContractMaint, Contract>.Transition.ISourceContainerFillerTransitions>) (ts => { }));
    })))).WithActions((Action<BoundedTo<ContractMaint, Contract>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.initializeState), (Func<BoundedTo<ContractMaint, Contract>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured>) (a => (BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.setup), (Func<BoundedTo<ContractMaint, Contract>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured>) (c => (BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
      actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.activate), (Func<BoundedTo<ContractMaint, Contract>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured>) (c => (BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
      actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.setupAndActivate), (Func<BoundedTo<ContractMaint, Contract>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured>) (c => (BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
      actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.bill), (Func<BoundedTo<ContractMaint, Contract>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured>) (c => (BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
      actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.renew), (Func<BoundedTo<ContractMaint, Contract>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured>) (c => (BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
      actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.terminate), (Func<BoundedTo<ContractMaint, Contract>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured>) (c => (BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
      actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.upgrade), (Func<BoundedTo<ContractMaint, Contract>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured>) (c => (BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
      actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.activateUpgrade), (Func<BoundedTo<ContractMaint, Contract>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured>) (c => (BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
      actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.undoBilling), (Func<BoundedTo<ContractMaint, Contract>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured>) (c => (BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
      actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.viewUsage), (Func<BoundedTo<ContractMaint, Contract>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured>) (c => (BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured) c.InFolder(customOtherCategory)));
      actions.Add((Expression<Func<ContractMaint, PXAction<Contract>>>) (g => g.ChangeID), (Func<BoundedTo<ContractMaint, Contract>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured>) (c => (BoundedTo<ContractMaint, Contract>.ActionDefinition.IConfigured) c.InFolder(customOtherCategory)));
    })).WithHandlers((Action<BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers =>
    {
      handlers.Add((Func<BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<Contract>) ((BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.INeedSubscriber<Contract>) handler.WithTargetOf<Contract>().OfEntityEvent<Contract.Events>((Expression<Func<Contract.Events, PXEntityEvent<Contract>>>) (e => e.SetupContract))).Is((Expression<Func<Contract, PXWorkflowEventHandler<Contract, Contract>>>) (g => g.OnSetupContract))).UsesTargetAsPrimaryEntity()));
      handlers.Add((Func<BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<Contract>) ((BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.INeedSubscriber<Contract>) handler.WithTargetOf<Contract>().OfEntityEvent<Contract.Events>((Expression<Func<Contract.Events, PXEntityEvent<Contract>>>) (e => e.ExpireContract))).Is((Expression<Func<Contract, PXWorkflowEventHandler<Contract, Contract>>>) (g => g.OnExpireContract))).UsesTargetAsPrimaryEntity()));
      handlers.Add((Func<BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<Contract>) ((BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.INeedSubscriber<Contract>) handler.WithTargetOf<Contract>().OfEntityEvent<Contract.Events>((Expression<Func<Contract.Events, PXEntityEvent<Contract>>>) (e => e.ActivateContract))).Is((Expression<Func<Contract, PXWorkflowEventHandler<Contract, Contract>>>) (g => g.OnActivateContract))).UsesTargetAsPrimaryEntity()));
      handlers.Add((Func<BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<Contract>) ((BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.INeedSubscriber<Contract>) handler.WithTargetOf<Contract>().OfEntityEvent<Contract.Events>((Expression<Func<Contract.Events, PXEntityEvent<Contract>>>) (e => e.CancelContract))).Is((Expression<Func<Contract, PXWorkflowEventHandler<Contract, Contract>>>) (g => g.OnCancelContract))).UsesTargetAsPrimaryEntity()));
      handlers.Add((Func<BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<Contract>) ((BoundedTo<ContractMaint, Contract>.WorkflowEventHandlerDefinition.INeedSubscriber<Contract>) handler.WithTargetOf<Contract>().OfEntityEvent<Contract.Events>((Expression<Func<Contract.Events, PXEntityEvent<Contract>>>) (e => e.UpgradeContract))).Is((Expression<Func<Contract, PXWorkflowEventHandler<Contract, Contract>>>) (g => g.OnUpgradeContract))).UsesTargetAsPrimaryEntity()));
    })).WithCategories((Action<BoundedTo<ContractMaint, Contract>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(customOtherCategory);
    }))));
  }

  public static class ActionCategoryNames
  {
    public const string Processing = "Processing";
    public const string CustomOther = "CustomOther";
  }

  public static class ActionCategory
  {
    public const string Processing = "Processing";
    public const string Other = "Other";
  }
}
