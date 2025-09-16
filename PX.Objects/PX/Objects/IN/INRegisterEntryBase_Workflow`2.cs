// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INRegisterEntryBase_Workflow`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.Common;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.IN;

public abstract class INRegisterEntryBase_Workflow<TGraph, TDocType> : PXGraphExtension<TGraph>
  where TGraph : INRegisterEntryBase, new()
  where TDocType : IConstant, IBqlOperand, IImplement<IBqlString>
{
  protected static void ConfigureCommon(WorkflowContext<TGraph, INRegister> context)
  {
    var conditions = new
    {
      IsReleased = Bql<BqlOperand<INRegister.released, IBqlBool>.IsEqual<True>>(),
      IsOnHold = Bql<BqlOperand<INRegister.hold, IBqlBool>.IsEqual<True>>(),
      MatchDocumentType = Bql<BqlOperand<INRegister.docType, IBqlString>.IsEqual<TDocType>>(),
      HasBatchNbr = Bql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INRegister.batchNbr, IsNotNull>>>>.And<BqlOperand<INRegister.batchNbr, IBqlString>.IsNotEqual<Empty>>>()
    }.AutoNameConditions();
    BoundedTo<TGraph, INRegister>.ActionCategory.IConfigured processingCategory = CommonActionCategories.Get<TGraph, INRegister>(context).Processing;
    context.AddScreenConfigurationFor((Func<BoundedTo<TGraph, INRegister>.ScreenConfiguration.IStartConfigScreen, BoundedTo<TGraph, INRegister>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<TGraph, INRegister>.ScreenConfiguration.IConfigured) ((BoundedTo<TGraph, INRegister>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<INRegister.status>().AddDefaultFlow((Func<BoundedTo<TGraph, INRegister>.Workflow.INeedStatesFlow, BoundedTo<TGraph, INRegister>.Workflow.IConfigured>) (flow => (BoundedTo<TGraph, INRegister>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<TGraph, INRegister>.BaseFlowStep.IContainerFillerStates>) (flowStates =>
    {
      flowStates.Add("_", (Func<BoundedTo<TGraph, INRegister>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TGraph, INRegister>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<TGraph, INRegister>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<TGraph, PXAutoAction<INRegister>>>) (g => g.initializeState))));
      flowStates.Add<INDocStatus.hold>((Func<BoundedTo<TGraph, INRegister>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TGraph, INRegister>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<TGraph, INRegister>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<TGraph, INRegister>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<TGraph, PXAction<INRegister>>>) (g => g.releaseFromHold), (Func<BoundedTo<TGraph, INRegister>.ActionState.IAllowOptionalConfig, BoundedTo<TGraph, INRegister>.ActionState.IConfigured>) (a => (BoundedTo<TGraph, INRegister>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)))))));
      flowStates.Add<INDocStatus.balanced>((Func<BoundedTo<TGraph, INRegister>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TGraph, INRegister>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<TGraph, INRegister>.BaseFlowStep.IConfigured) ((BoundedTo<TGraph, INRegister>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<TGraph, INRegister>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<TGraph, PXAction<INRegister>>>) (g => g.release), (Func<BoundedTo<TGraph, INRegister>.ActionState.IAllowOptionalConfig, BoundedTo<TGraph, INRegister>.ActionState.IConfigured>) (a => (BoundedTo<TGraph, INRegister>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<TGraph, PXAction<INRegister>>>) (g => g.putOnHold), (Func<BoundedTo<TGraph, INRegister>.ActionState.IAllowOptionalConfig, BoundedTo<TGraph, INRegister>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<TGraph, PXAction<INRegister>>>) (g => g.iNEdit), (Func<BoundedTo<TGraph, INRegister>.ActionState.IAllowOptionalConfig, BoundedTo<TGraph, INRegister>.ActionState.IConfigured>) null);
      }))).WithEventHandlers((Action<BoundedTo<TGraph, INRegister>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<TGraph, PXWorkflowEventHandler<INRegister>>>) (g => g.OnDocumentReleased))))));
      flowStates.Add<INDocStatus.released>((Func<BoundedTo<TGraph, INRegister>.FlowState.INeedAnyFlowStateConfig, BoundedTo<TGraph, INRegister>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<TGraph, INRegister>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<TGraph, INRegister>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<TGraph, PXAction<INRegister>>>) (g => g.iNRegisterDetails), (Func<BoundedTo<TGraph, INRegister>.ActionState.IAllowOptionalConfig, BoundedTo<TGraph, INRegister>.ActionState.IConfigured>) null)))));
    })).WithTransitions((Action<BoundedTo<TGraph, INRegister>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<TGraph, INRegister>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<TGraph, INRegister>.Transition.INeedTarget, BoundedTo<TGraph, INRegister>.Transition.IConfigured>) (t => (BoundedTo<TGraph, INRegister>.Transition.IConfigured) t.To<INDocStatus.released>().IsTriggeredOn((Expression<Func<TGraph, PXAction<INRegister>>>) (g => g.initializeState)).When((BoundedTo<TGraph, INRegister>.ISharedCondition) conditions.IsReleased)));
        ts.Add((Func<BoundedTo<TGraph, INRegister>.Transition.INeedTarget, BoundedTo<TGraph, INRegister>.Transition.IConfigured>) (t => (BoundedTo<TGraph, INRegister>.Transition.IConfigured) t.To<INDocStatus.hold>().IsTriggeredOn((Expression<Func<TGraph, PXAction<INRegister>>>) (g => g.initializeState)).When((BoundedTo<TGraph, INRegister>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<TGraph, INRegister>.Transition.INeedTarget, BoundedTo<TGraph, INRegister>.Transition.IConfigured>) (t => (BoundedTo<TGraph, INRegister>.Transition.IConfigured) t.To<INDocStatus.balanced>().IsTriggeredOn((Expression<Func<TGraph, PXAction<INRegister>>>) (g => g.initializeState))));
      }));
      transitions.Add((Func<BoundedTo<TGraph, INRegister>.Transition.INeedSource, BoundedTo<TGraph, INRegister>.Transition.IConfigured>) (t => (BoundedTo<TGraph, INRegister>.Transition.IConfigured) t.From<INDocStatus.hold>().To<INDocStatus.balanced>().IsTriggeredOn((Expression<Func<TGraph, PXAction<INRegister>>>) (g => g.releaseFromHold)).When((BoundedTo<TGraph, INRegister>.ISharedCondition) BoundedTo<TGraph, INRegister>.Condition.op_LogicalNot(conditions.IsOnHold))));
      transitions.Add((Func<BoundedTo<TGraph, INRegister>.Transition.INeedSource, BoundedTo<TGraph, INRegister>.Transition.IConfigured>) (t => (BoundedTo<TGraph, INRegister>.Transition.IConfigured) t.From<INDocStatus.balanced>().To<INDocStatus.hold>().IsTriggeredOn((Expression<Func<TGraph, PXAction<INRegister>>>) (g => g.putOnHold)).When((BoundedTo<TGraph, INRegister>.ISharedCondition) conditions.IsOnHold)));
      transitions.Add((Func<BoundedTo<TGraph, INRegister>.Transition.INeedSource, BoundedTo<TGraph, INRegister>.Transition.IConfigured>) (t => (BoundedTo<TGraph, INRegister>.Transition.IConfigured) t.From<INDocStatus.balanced>().To<INDocStatus.released>().IsTriggeredOn((Expression<Func<TGraph, PXWorkflowEventHandlerBase<INRegister>>>) (g => g.OnDocumentReleased)).When((BoundedTo<TGraph, INRegister>.ISharedCondition) conditions.IsReleased)));
    })))).WithActions((Action<BoundedTo<TGraph, INRegister>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<TGraph, PXAction<INRegister>>>) (g => g.initializeState), (Func<BoundedTo<TGraph, INRegister>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, INRegister>.ActionDefinition.IConfigured>) (a => (BoundedTo<TGraph, INRegister>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<TGraph, PXAction<INRegister>>>) (g => g.releaseFromHold), (Func<BoundedTo<TGraph, INRegister>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, INRegister>.ActionDefinition.IConfigured>) (a => (BoundedTo<TGraph, INRegister>.ActionDefinition.IConfigured) a.WithCategory(processingCategory).WithFieldAssignments((Action<BoundedTo<TGraph, INRegister>.Assignment.IContainerFillerFields>) (fass => fass.Add<INRegister.hold>(new bool?(false))))));
      actions.Add((Expression<Func<TGraph, PXAction<INRegister>>>) (g => g.putOnHold), (Func<BoundedTo<TGraph, INRegister>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, INRegister>.ActionDefinition.IConfigured>) (a => (BoundedTo<TGraph, INRegister>.ActionDefinition.IConfigured) a.WithCategory(processingCategory).WithFieldAssignments((Action<BoundedTo<TGraph, INRegister>.Assignment.IContainerFillerFields>) (fass => fass.Add<INRegister.hold>(new bool?(true))))));
      actions.Add((Expression<Func<TGraph, PXAction<INRegister>>>) (g => g.release), (Func<BoundedTo<TGraph, INRegister>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, INRegister>.ActionDefinition.IConfigured>) (a => (BoundedTo<TGraph, INRegister>.ActionDefinition.IConfigured) a.WithCategory(processingCategory)));
      actions.Add((Expression<Func<TGraph, PXAction<INRegister>>>) (g => g.iNEdit), (Func<BoundedTo<TGraph, INRegister>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, INRegister>.ActionDefinition.IConfigured>) (a => (BoundedTo<TGraph, INRegister>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 2)));
      actions.Add((Expression<Func<TGraph, PXAction<INRegister>>>) (g => g.iNRegisterDetails), (Func<BoundedTo<TGraph, INRegister>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<TGraph, INRegister>.ActionDefinition.IConfigured>) (a => (BoundedTo<TGraph, INRegister>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 2)));
    })).WithCategories((Action<BoundedTo<TGraph, INRegister>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Update((FolderType) 2, (Func<BoundedTo<TGraph, INRegister>.ActionCategory.ConfiguratorCategory, BoundedTo<TGraph, INRegister>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(processingCategory)));
    })).WithHandlers((Action<BoundedTo<TGraph, INRegister>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers => handlers.Add((Func<BoundedTo<TGraph, INRegister>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<TGraph, INRegister>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<TGraph, INRegister>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<TGraph, INRegister>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<INRegister>) ((BoundedTo<TGraph, INRegister>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<INRegister>) ((BoundedTo<TGraph, INRegister>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<INRegister>) ((BoundedTo<TGraph, INRegister>.WorkflowEventHandlerDefinition.INeedSubscriber<INRegister>) handler.WithTargetOf<INRegister>().OfEntityEvent<INRegister.Events>((Expression<Func<INRegister.Events, PXEntityEvent<INRegister>>>) (e => e.DocumentReleased))).Is((Expression<Func<INRegister, PXWorkflowEventHandler<INRegister, INRegister>>>) (g => g.OnDocumentReleased))).UsesTargetAsPrimaryEntity()).AppliesWhen((BoundedTo<INRegister, INRegister>.ISharedCondition) conditions.MatchDocumentType)).WithFieldAssignments((Action<BoundedTo<INRegister, INRegister>.Assignment.IContainerFillerFields>) (fass =>
    {
      fass.Add<INRegister.released>(new bool?(true));
      fass.Add<INRegister.releasedToVerify>(new bool?(false));
    }))))))));

    BoundedTo<TGraph, INRegister>.Condition Bql<T>() where T : IBqlUnary, new()
    {
      return context.Conditions.FromBql<T>();
    }
  }
}
