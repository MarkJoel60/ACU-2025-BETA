// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ExpenseClaimDetailEntry_Workflow
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
namespace PX.Objects.EP;

public class ExpenseClaimDetailEntry_Workflow : PXGraphExtension<ExpenseClaimDetailEntry>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    ExpenseClaimDetailEntry_Workflow.Configure(config.GetScreenConfigurationContext<ExpenseClaimDetailEntry, EPExpenseClaimDetails>());
  }

  protected static void Configure(
    WorkflowContext<ExpenseClaimDetailEntry, EPExpenseClaimDetails> context)
  {
    BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionCategory.IConfigured>) (category => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    var conditions = new
    {
      IsOnHold = Bql<BqlOperand<EPExpenseClaimDetails.hold, IBqlBool>.IsEqual<True>>(),
      IsApproved = Bql<BqlOperand<EPExpenseClaimDetails.approved, IBqlBool>.IsEqual<True>>(),
      IsHoldDisabled = Bql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPExpenseClaimDetails.holdClaim, Equal<False>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPExpenseClaimDetails.rejected, Equal<False>>>>>.And<BqlOperand<EPExpenseClaimDetails.bankTranDate, IBqlDateTime>.IsNotNull>>>()
    }.AutoNameConditions();
    context.AddScreenConfigurationFor((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ScreenConfiguration.IStartConfigScreen, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ScreenConfiguration.IConfigured) ((BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<EPExpenseClaimDetails.status>().AddDefaultFlow((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Workflow.INeedStatesFlow, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Workflow.IConfigured>) (flow => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add("_", (Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.BaseFlowStep.IConfigured) flowState.IsInitial((Expression<Func<ExpenseClaimDetailEntry, PXAutoAction<EPExpenseClaimDetails>>>) (g => g.initializeState))));
      fss.Add<EPExpenseClaimDetailsStatus.holdStatus>((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<ExpenseClaimDetailEntry, PXAction<EPExpenseClaimDetails>>>) (g => g.Submit), (Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IAllowOptionalConfig, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IConfigured>) (c => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)))))));
      fss.Add<EPExpenseClaimDetailsStatus.approvedStatus>((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<ExpenseClaimDetailEntry, PXAction<EPExpenseClaimDetails>>>) (g => g.hold), (Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IAllowOptionalConfig, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<ExpenseClaimDetailEntry, PXAction<EPExpenseClaimDetails>>>) (g => g.Claim), (Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IAllowOptionalConfig, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IConfigured>) (c => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
      }))));
      fss.Add<EPExpenseClaimDetailsStatus.releasedStatus>((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionState.IContainerFillerActions>) (actions => { }))));
    })).WithTransitions((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom("_", (Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.INeedTarget, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IConfigured) t.To<EPExpenseClaimDetailsStatus.holdStatus>().IsTriggeredOn((Expression<Func<ExpenseClaimDetailEntry, PXAction<EPExpenseClaimDetails>>>) (g => g.initializeState)).When((BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ISharedCondition) conditions.IsOnHold)));
        ts.Add((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.INeedTarget, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IConfigured) t.To<EPExpenseClaimDetailsStatus.approvedStatus>().IsTriggeredOn((Expression<Func<ExpenseClaimDetailEntry, PXAction<EPExpenseClaimDetails>>>) (g => g.initializeState)).When((BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ISharedCondition) conditions.IsApproved)));
      }));
      transitions.AddGroupFrom<EPExpenseClaimDetailsStatus.holdStatus>((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.INeedTarget, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IConfigured) t.To<EPExpenseClaimDetailsStatus.approvedStatus>().IsTriggeredOn((Expression<Func<ExpenseClaimDetailEntry, PXAction<EPExpenseClaimDetails>>>) (g => g.Submit))))));
      transitions.AddGroupFrom<EPExpenseClaimDetailsStatus.approvedStatus>((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.INeedTarget, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IConfigured>) (t => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.IConfigured) t.To<EPTimeCardStatusAttribute.holdStatus>().IsTriggeredOn((Expression<Func<ExpenseClaimDetailEntry, PXAction<EPExpenseClaimDetails>>>) (g => g.hold))))));
      transitions.AddGroupFrom<EPExpenseClaimDetailsStatus.releasedStatus>((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Transition.ISourceContainerFillerTransitions>) (ts => { }));
    })))).WithActions((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<ExpenseClaimDetailEntry, PXAction<EPExpenseClaimDetails>>>) (g => g.Submit), (Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.IConfigured>) (c => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithFieldAssignments((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.IContainerFillerFields>) (fa => fa.Add<EPExpenseClaimDetails.hold>((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.INeedRightOperand, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.IConfigured>) (e => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.IConfigured) e.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<ExpenseClaimDetailEntry, PXAction<EPExpenseClaimDetails>>>) (g => g.hold), (Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.IConfigured>) (c => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.IConfigured) c.InFolder(processingCategory).IsDisabledWhen((BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ISharedCondition) conditions.IsHoldDisabled).WithFieldAssignments((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.IContainerFillerFields>) (fa => fa.Add<EPExpenseClaimDetails.hold>((Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.INeedRightOperand, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.IConfigured>) (e => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Assignment.IConfigured) e.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<ExpenseClaimDetailEntry, PXAction<EPExpenseClaimDetails>>>) (g => g.Claim), (Func<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.IConfigured>) (c => (BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
    })).WithCategories((Action<BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.ActionCategory.IContainerFillerCategories>) (categories => categories.Add(processingCategory)))));

    BoundedTo<ExpenseClaimDetailEntry, EPExpenseClaimDetails>.Condition Bql<T>() where T : IBqlUnary, new()
    {
      return context.Conditions.FromBql<T>();
    }
  }
}
