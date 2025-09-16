// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.KitAssemblyEntry_Workflow
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

public class KitAssemblyEntry_Workflow : PXGraphExtension<KitAssemblyEntry>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    KitAssemblyEntry_Workflow.Configure(config.GetScreenConfigurationContext<KitAssemblyEntry, INKitRegister>());
  }

  protected static void Configure(
    WorkflowContext<KitAssemblyEntry, INKitRegister> context)
  {
    var conditions = new
    {
      IsReleased = Bql<BqlOperand<INKitRegister.released, IBqlBool>.IsEqual<True>>(),
      IsOnHold = Bql<BqlOperand<INKitRegister.hold, IBqlBool>.IsEqual<True>>(),
      IsDisassembly = Bql<BqlOperand<INKitRegister.docType, IBqlString>.IsEqual<INDocType.disassembly>>(),
      HasBatchNbr = Bql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INKitRegister.batchNbr, IsNotNull>>>>.And<BqlOperand<INKitRegister.batchNbr, IBqlString>.IsNotEqual<Empty>>>()
    }.AutoNameConditions();
    BoundedTo<KitAssemblyEntry, INKitRegister>.ActionCategory.IConfigured processingCategory = CommonActionCategories.Get<KitAssemblyEntry, INKitRegister>(context).Processing;
    context.AddScreenConfigurationFor((Func<BoundedTo<KitAssemblyEntry, INKitRegister>.ScreenConfiguration.IStartConfigScreen, BoundedTo<KitAssemblyEntry, INKitRegister>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<KitAssemblyEntry, INKitRegister>.ScreenConfiguration.IConfigured) ((BoundedTo<KitAssemblyEntry, INKitRegister>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<INKitRegister.status>().AddDefaultFlow((Func<BoundedTo<KitAssemblyEntry, INKitRegister>.Workflow.INeedStatesFlow, BoundedTo<KitAssemblyEntry, INKitRegister>.Workflow.IConfigured>) (flow => (BoundedTo<KitAssemblyEntry, INKitRegister>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<KitAssemblyEntry, INKitRegister>.BaseFlowStep.IContainerFillerStates>) (flowStates =>
    {
      flowStates.Add("_", (Func<BoundedTo<KitAssemblyEntry, INKitRegister>.FlowState.INeedAnyFlowStateConfig, BoundedTo<KitAssemblyEntry, INKitRegister>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<KitAssemblyEntry, INKitRegister>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<KitAssemblyEntry, PXAutoAction<INKitRegister>>>) (g => g.initializeState))));
      flowStates.Add<INDocStatus.hold>((Func<BoundedTo<KitAssemblyEntry, INKitRegister>.FlowState.INeedAnyFlowStateConfig, BoundedTo<KitAssemblyEntry, INKitRegister>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<KitAssemblyEntry, INKitRegister>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<KitAssemblyEntry, INKitRegister>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<KitAssemblyEntry, PXAction<INKitRegister>>>) (g => g.releaseFromHold), (Func<BoundedTo<KitAssemblyEntry, INKitRegister>.ActionState.IAllowOptionalConfig, BoundedTo<KitAssemblyEntry, INKitRegister>.ActionState.IConfigured>) (a => (BoundedTo<KitAssemblyEntry, INKitRegister>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)))))));
      flowStates.Add<INDocStatus.balanced>((Func<BoundedTo<KitAssemblyEntry, INKitRegister>.FlowState.INeedAnyFlowStateConfig, BoundedTo<KitAssemblyEntry, INKitRegister>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<KitAssemblyEntry, INKitRegister>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<KitAssemblyEntry, INKitRegister>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<KitAssemblyEntry, PXAction<INKitRegister>>>) (g => g.release), (Func<BoundedTo<KitAssemblyEntry, INKitRegister>.ActionState.IAllowOptionalConfig, BoundedTo<KitAssemblyEntry, INKitRegister>.ActionState.IConfigured>) (a => (BoundedTo<KitAssemblyEntry, INKitRegister>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<KitAssemblyEntry, PXAction<INKitRegister>>>) (g => g.putOnHold), (Func<BoundedTo<KitAssemblyEntry, INKitRegister>.ActionState.IAllowOptionalConfig, BoundedTo<KitAssemblyEntry, INKitRegister>.ActionState.IConfigured>) null);
      }))));
      flowStates.Add<INDocStatus.released>((Func<BoundedTo<KitAssemblyEntry, INKitRegister>.FlowState.INeedAnyFlowStateConfig, BoundedTo<KitAssemblyEntry, INKitRegister>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<KitAssemblyEntry, INKitRegister>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<KitAssemblyEntry, INKitRegister>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<KitAssemblyEntry, PXAction<INKitRegister>>>) (g => g.viewBatch), (Func<BoundedTo<KitAssemblyEntry, INKitRegister>.ActionState.IAllowOptionalConfig, BoundedTo<KitAssemblyEntry, INKitRegister>.ActionState.IConfigured>) (a => (BoundedTo<KitAssemblyEntry, INKitRegister>.ActionState.IConfigured) a.IsDuplicatedInToolbar()))))));
    })).WithTransitions((Action<BoundedTo<KitAssemblyEntry, INKitRegister>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<KitAssemblyEntry, INKitRegister>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<KitAssemblyEntry, INKitRegister>.Transition.INeedTarget, BoundedTo<KitAssemblyEntry, INKitRegister>.Transition.IConfigured>) (t => (BoundedTo<KitAssemblyEntry, INKitRegister>.Transition.IConfigured) t.To<INDocStatus.released>().IsTriggeredOn((Expression<Func<KitAssemblyEntry, PXAction<INKitRegister>>>) (g => g.initializeState)).When((BoundedTo<KitAssemblyEntry, INKitRegister>.ISharedCondition) conditions.IsReleased)));
        ts.Add((Func<BoundedTo<KitAssemblyEntry, INKitRegister>.Transition.INeedTarget, BoundedTo<KitAssemblyEntry, INKitRegister>.Transition.IConfigured>) (t => (BoundedTo<KitAssemblyEntry, INKitRegister>.Transition.IConfigured) t.To<INDocStatus.hold>().IsTriggeredOn((Expression<Func<KitAssemblyEntry, PXAction<INKitRegister>>>) (g => g.initializeState)).When((BoundedTo<KitAssemblyEntry, INKitRegister>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<KitAssemblyEntry, INKitRegister>.Transition.INeedTarget, BoundedTo<KitAssemblyEntry, INKitRegister>.Transition.IConfigured>) (t => (BoundedTo<KitAssemblyEntry, INKitRegister>.Transition.IConfigured) t.To<INDocStatus.balanced>().IsTriggeredOn((Expression<Func<KitAssemblyEntry, PXAction<INKitRegister>>>) (g => g.initializeState))));
      }));
      transitions.Add((Func<BoundedTo<KitAssemblyEntry, INKitRegister>.Transition.INeedSource, BoundedTo<KitAssemblyEntry, INKitRegister>.Transition.IConfigured>) (t => (BoundedTo<KitAssemblyEntry, INKitRegister>.Transition.IConfigured) t.From<INDocStatus.hold>().To<INDocStatus.balanced>().IsTriggeredOn((Expression<Func<KitAssemblyEntry, PXAction<INKitRegister>>>) (g => g.releaseFromHold)).When((BoundedTo<KitAssemblyEntry, INKitRegister>.ISharedCondition) BoundedTo<KitAssemblyEntry, INKitRegister>.Condition.op_LogicalNot(conditions.IsOnHold))));
      transitions.Add((Func<BoundedTo<KitAssemblyEntry, INKitRegister>.Transition.INeedSource, BoundedTo<KitAssemblyEntry, INKitRegister>.Transition.IConfigured>) (t => (BoundedTo<KitAssemblyEntry, INKitRegister>.Transition.IConfigured) t.From<INDocStatus.balanced>().To<INDocStatus.hold>().IsTriggeredOn((Expression<Func<KitAssemblyEntry, PXAction<INKitRegister>>>) (g => g.putOnHold)).When((BoundedTo<KitAssemblyEntry, INKitRegister>.ISharedCondition) conditions.IsOnHold)));
    })))).WithActions((Action<BoundedTo<KitAssemblyEntry, INKitRegister>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<KitAssemblyEntry, PXAction<INKitRegister>>>) (g => g.initializeState), (Func<BoundedTo<KitAssemblyEntry, INKitRegister>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<KitAssemblyEntry, INKitRegister>.ActionDefinition.IConfigured>) (a => (BoundedTo<KitAssemblyEntry, INKitRegister>.ActionDefinition.IConfigured) a.IsHiddenAlways()));
      actions.Add((Expression<Func<KitAssemblyEntry, PXAction<INKitRegister>>>) (g => g.releaseFromHold), (Func<BoundedTo<KitAssemblyEntry, INKitRegister>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<KitAssemblyEntry, INKitRegister>.ActionDefinition.IConfigured>) (a => (BoundedTo<KitAssemblyEntry, INKitRegister>.ActionDefinition.IConfigured) a.WithCategory(processingCategory).WithFieldAssignments((Action<BoundedTo<KitAssemblyEntry, INKitRegister>.Assignment.IContainerFillerFields>) (fass => fass.Add<INKitRegister.hold>(new bool?(false))))));
      actions.Add((Expression<Func<KitAssemblyEntry, PXAction<INKitRegister>>>) (g => g.putOnHold), (Func<BoundedTo<KitAssemblyEntry, INKitRegister>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<KitAssemblyEntry, INKitRegister>.ActionDefinition.IConfigured>) (a => (BoundedTo<KitAssemblyEntry, INKitRegister>.ActionDefinition.IConfigured) a.WithCategory(processingCategory).WithFieldAssignments((Action<BoundedTo<KitAssemblyEntry, INKitRegister>.Assignment.IContainerFillerFields>) (fass => fass.Add<INKitRegister.hold>(new bool?(true))))));
      actions.Add((Expression<Func<KitAssemblyEntry, PXAction<INKitRegister>>>) (g => g.release), (Func<BoundedTo<KitAssemblyEntry, INKitRegister>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<KitAssemblyEntry, INKitRegister>.ActionDefinition.IConfigured>) (a => (BoundedTo<KitAssemblyEntry, INKitRegister>.ActionDefinition.IConfigured) a.WithCategory(processingCategory)));
      actions.Add((Expression<Func<KitAssemblyEntry, PXAction<INKitRegister>>>) (g => g.viewBatch), (Func<BoundedTo<KitAssemblyEntry, INKitRegister>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<KitAssemblyEntry, INKitRegister>.ActionDefinition.IConfigured>) (a => (BoundedTo<KitAssemblyEntry, INKitRegister>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 1).IsDisabledWhen((BoundedTo<KitAssemblyEntry, INKitRegister>.ISharedCondition) BoundedTo<KitAssemblyEntry, INKitRegister>.Condition.op_LogicalNot(conditions.HasBatchNbr))));
    })).WithCategories((Action<BoundedTo<KitAssemblyEntry, INKitRegister>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Update((FolderType) 1, (Func<BoundedTo<KitAssemblyEntry, INKitRegister>.ActionCategory.ConfiguratorCategory, BoundedTo<KitAssemblyEntry, INKitRegister>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(processingCategory)));
    })).WithFieldStates((Action<BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.IContainerFillerFields>) (fieldStates =>
    {
      fieldStates.Add<INKitSpecStkDet.allowQtyVariation>((Func<BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.INeedAnyConfigField, BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.IConfigured>) (s => (BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.IConfigured) ((BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.INeedAnyConfigField) s.IsDisabledAlways()).IsHiddenWhen((BoundedTo<KitAssemblyEntry, INKitRegister>.ISharedCondition) conditions.IsDisassembly)));
      fieldStates.Add<INKitSpecStkDet.maxCompQty>((Func<BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.INeedAnyConfigField, BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.IConfigured>) (s => (BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.IConfigured) ((BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.INeedAnyConfigField) s.IsDisabledAlways()).IsHiddenWhen((BoundedTo<KitAssemblyEntry, INKitRegister>.ISharedCondition) conditions.IsDisassembly)));
      fieldStates.Add<INKitSpecStkDet.minCompQty>((Func<BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.INeedAnyConfigField, BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.IConfigured>) (s => (BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.IConfigured) ((BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.INeedAnyConfigField) s.IsDisabledAlways()).IsHiddenWhen((BoundedTo<KitAssemblyEntry, INKitRegister>.ISharedCondition) conditions.IsDisassembly)));
      fieldStates.Add<INKitSpecStkDet.disassemblyCoeff>((Func<BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.INeedAnyConfigField, BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.IConfigured>) (s => (BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.IConfigured) ((BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.INeedAnyConfigField) s.IsDisabledAlways()).IsHiddenWhen((BoundedTo<KitAssemblyEntry, INKitRegister>.ISharedCondition) BoundedTo<KitAssemblyEntry, INKitRegister>.Condition.op_LogicalNot(conditions.IsDisassembly))));
      fieldStates.Add<INKitSpecNonStkDet.allowQtyVariation>((Func<BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.INeedAnyConfigField, BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.IConfigured>) (s => (BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.IConfigured) ((BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.INeedAnyConfigField) s.IsDisabledAlways()).IsHiddenWhen((BoundedTo<KitAssemblyEntry, INKitRegister>.ISharedCondition) conditions.IsDisassembly)));
      fieldStates.Add<INKitSpecNonStkDet.maxCompQty>((Func<BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.INeedAnyConfigField, BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.IConfigured>) (s => (BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.IConfigured) ((BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.INeedAnyConfigField) s.IsDisabledAlways()).IsHiddenWhen((BoundedTo<KitAssemblyEntry, INKitRegister>.ISharedCondition) conditions.IsDisassembly)));
      fieldStates.Add<INKitSpecNonStkDet.minCompQty>((Func<BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.INeedAnyConfigField, BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.IConfigured>) (s => (BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.IConfigured) ((BoundedTo<KitAssemblyEntry, INKitRegister>.DynamicFieldState.INeedAnyConfigField) s.IsDisabledAlways()).IsHiddenWhen((BoundedTo<KitAssemblyEntry, INKitRegister>.ISharedCondition) conditions.IsDisassembly)));
    }))));

    BoundedTo<KitAssemblyEntry, INKitRegister>.Condition Bql<T>() where T : IBqlUnary, new()
    {
      return context.Conditions.FromBql<T>();
    }
  }
}
