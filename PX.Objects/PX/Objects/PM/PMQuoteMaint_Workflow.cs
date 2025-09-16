// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMQuoteMaint_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.CR;
using System;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.PM;

public class PMQuoteMaint_Workflow : PXGraphExtension<PMQuoteMaint>
{
  public static bool IsActive() => true;

  public virtual void Configure(PXScreenConfiguration config)
  {
    PMQuoteMaint_Workflow.Configure(config.GetScreenConfigurationContext<PMQuoteMaint, PMQuote>());
  }

  protected static void Configure(WorkflowContext<PMQuoteMaint, PMQuote> context)
  {
    var conditions = new
    {
      IsPrimaryQuoteEnabled = Bql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMQuote.opportunityID, IsNotNull>>>, And<BqlOperand<PMQuote.opportunityID, IBqlString>.IsNotEqual<StringEmpty>>>, And<BqlOperand<PMQuote.isPrimary, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<Selector<PMQuote.opportunityID, CROpportunity.isActive>, IBqlBool>.IsEqual<True>>>(),
      IsCancelDisabled = Bql<BqlOperand<PMChangeRequest.costChangeOrderNbr, IBqlString>.IsNotNull>(),
      IsCloseDisabled = Bql<BqlOperand<PMChangeRequest.costChangeOrderReleased, IBqlBool>.IsNotEqual<True>>(),
      IsConvertToProjectEnabled = Bql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMQuote.opportunityID, IsNull>>>>.Or<BqlOperand<PMQuote.isPrimary, IBqlBool>.IsEqual<True>>>>>.And<BqlOperand<PMQuote.quoteProjectID, IBqlInt>.IsNull>>()
    }.AutoNameConditions();
    BoundedTo<PMQuoteMaint, PMQuote>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("Processing", (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<PMQuoteMaint, PMQuote>.ActionCategory.IConfigured>) (category => (BoundedTo<PMQuoteMaint, PMQuote>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<PMQuoteMaint, PMQuote>.ActionCategory.IConfigured printingAndEmailingCategory = context.Categories.CreateNew("Printing and Emailing", (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<PMQuoteMaint, PMQuote>.ActionCategory.IConfigured>) (category => (BoundedTo<PMQuoteMaint, PMQuote>.ActionCategory.IConfigured) category.DisplayName("Printing and Emailing")));
    BoundedTo<PMQuoteMaint, PMQuote>.ActionCategory.IConfigured otherCategory = context.Categories.CreateNew("OtherCategory", (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<PMQuoteMaint, PMQuote>.ActionCategory.IConfigured>) (category => (BoundedTo<PMQuoteMaint, PMQuote>.ActionCategory.IConfigured) category.DisplayName("Other")));
    context.AddScreenConfigurationFor((Func<BoundedTo<PMQuoteMaint, PMQuote>.ScreenConfiguration.IStartConfigScreen, BoundedTo<PMQuoteMaint, PMQuote>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<PMQuoteMaint, PMQuote>.ScreenConfiguration.IConfigured) ((BoundedTo<PMQuoteMaint, PMQuote>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<PMQuote.status>().AddDefaultFlow((Func<BoundedTo<PMQuoteMaint, PMQuote>.Workflow.INeedStatesFlow, BoundedTo<PMQuoteMaint, PMQuote>.Workflow.IConfigured>) (flow => (BoundedTo<PMQuoteMaint, PMQuote>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<PMQuoteMaint, PMQuote>.BaseFlowStep.IContainerFillerStates>) (fss =>
    {
      fss.Add<CRQuoteStatusAttribute.draft>((Func<BoundedTo<PMQuoteMaint, PMQuote>.FlowState.INeedAnyFlowStateConfig, BoundedTo<PMQuoteMaint, PMQuote>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<PMQuoteMaint, PMQuote>.BaseFlowStep.IConfigured) ((BoundedTo<PMQuoteMaint, PMQuote>.FlowState.INeedAnyFlowStateConfig) flowState.IsInitial()).WithActions((Action<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.submit), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) (c => (BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.copyQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.primaryQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.accept), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: field reference
        actions.Add(Expression.Lambda<Func<PMQuoteMaint, PXAction<PMQuote>>>((Expression) Expression.Field((Expression) Expression.Call(g, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXGraph.GetExtension)), Array.Empty<Expression>()), FieldInfo.GetFieldFromHandle(__fieldref (PMQuoteMaint.PMDiscount.graphRecalculateDiscountsAction))), parameterExpression), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
      }))));
      fss.Add<CRQuoteStatusAttribute.approved>((Func<BoundedTo<PMQuoteMaint, PMQuote>.FlowState.INeedAnyFlowStateConfig, BoundedTo<PMQuoteMaint, PMQuote>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<PMQuoteMaint, PMQuote>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.convertToProject), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) (c => (BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.editQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.decline), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.accept), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.sendQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) (c => (BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.printQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) (c => (BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.copyQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.primaryQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
      }))));
      fss.Add<CRQuoteStatusAttribute.sent>((Func<BoundedTo<PMQuoteMaint, PMQuote>.FlowState.INeedAnyFlowStateConfig, BoundedTo<PMQuoteMaint, PMQuote>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<PMQuoteMaint, PMQuote>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.convertToProject), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) (c => (BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.editQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.decline), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.accept), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.sendQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.printQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) (c => (BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.copyQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.primaryQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
      }))));
      fss.Add<PMQuoteStatusAttribute.closed>((Func<BoundedTo<PMQuoteMaint, PMQuote>.FlowState.INeedAnyFlowStateConfig, BoundedTo<PMQuoteMaint, PMQuote>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<PMQuoteMaint, PMQuote>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.sendQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.printQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) (c => (BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured) c.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.copyQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.primaryQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
      }))));
      fss.Add<CRQuoteStatusAttribute.accepted>((Func<BoundedTo<PMQuoteMaint, PMQuote>.FlowState.INeedAnyFlowStateConfig, BoundedTo<PMQuoteMaint, PMQuote>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<PMQuoteMaint, PMQuote>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.decline), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.editQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.copyQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.convertToProject), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) (c => (BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured) c.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.sendQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.printQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.primaryQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
      }))));
      fss.Add<CRQuoteStatusAttribute.declined>((Func<BoundedTo<PMQuoteMaint, PMQuote>.FlowState.INeedAnyFlowStateConfig, BoundedTo<PMQuoteMaint, PMQuote>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<PMQuoteMaint, PMQuote>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.accept), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.editQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.copyQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.sendQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
        actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.printQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IAllowOptionalConfig, BoundedTo<PMQuoteMaint, PMQuote>.ActionState.IConfigured>) null);
      }))));
    })).WithTransitions((Action<BoundedTo<PMQuoteMaint, PMQuote>.Transition.IContainerFillerTransitions>) (transitions =>
    {
      transitions.AddGroupFrom<CRQuoteStatusAttribute.draft>((Action<BoundedTo<PMQuoteMaint, PMQuote>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<CRQuoteStatusAttribute.approved>().IsTriggeredOn((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.submit))));
        ts.Add((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<CRQuoteStatusAttribute.accepted>().IsTriggeredOn((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.accept))));
      }));
      transitions.AddGroupFrom<CRQuoteStatusAttribute.approved>((Action<BoundedTo<PMQuoteMaint, PMQuote>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<CRQuoteStatusAttribute.draft>().IsTriggeredOn((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.editQuote))));
        ts.Add((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<CRQuoteStatusAttribute.sent>().IsTriggeredOn((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.sendQuote))));
        ts.Add((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<PMQuoteStatusAttribute.closed>().IsTriggeredOn((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.convertToProject))));
        ts.Add((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<CRQuoteStatusAttribute.accepted>().IsTriggeredOn((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.accept))));
        ts.Add((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<CRQuoteStatusAttribute.declined>().IsTriggeredOn((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.decline))));
      }));
      transitions.AddGroupFrom<CRQuoteStatusAttribute.sent>((Action<BoundedTo<PMQuoteMaint, PMQuote>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<CRQuoteStatusAttribute.draft>().IsTriggeredOn((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.editQuote))));
        ts.Add((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<PMQuoteStatusAttribute.closed>().IsTriggeredOn((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.convertToProject))));
        ts.Add((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<CRQuoteStatusAttribute.accepted>().IsTriggeredOn((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.accept))));
        ts.Add((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<CRQuoteStatusAttribute.declined>().IsTriggeredOn((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.decline))));
      }));
      transitions.AddGroupFrom<CRQuoteStatusAttribute.accepted>((Action<BoundedTo<PMQuoteMaint, PMQuote>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<CRQuoteStatusAttribute.declined>().IsTriggeredOn((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.decline))));
        ts.Add((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<CRQuoteStatusAttribute.draft>().IsTriggeredOn((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.editQuote))));
        ts.Add((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<PMQuoteStatusAttribute.closed>().IsTriggeredOn((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.convertToProject))));
      }));
      transitions.AddGroupFrom<CRQuoteStatusAttribute.declined>((Action<BoundedTo<PMQuoteMaint, PMQuote>.Transition.ISourceContainerFillerTransitions>) (ts =>
      {
        ts.Add((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<CRQuoteStatusAttribute.accepted>().IsTriggeredOn((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.accept))));
        ts.Add((Func<BoundedTo<PMQuoteMaint, PMQuote>.Transition.INeedTarget, BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured>) (t => (BoundedTo<PMQuoteMaint, PMQuote>.Transition.IConfigured) t.To<CRQuoteStatusAttribute.draft>().IsTriggeredOn((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.editQuote))));
      }));
    })))).WithActions((Action<BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.submit), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured>) (c => (BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithFieldAssignments((Action<BoundedTo<PMQuoteMaint, PMQuote>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMQuote.hold>((Func<BoundedTo<PMQuoteMaint, PMQuote>.Assignment.INeedRightOperand, BoundedTo<PMQuoteMaint, PMQuote>.Assignment.IConfigured>) (f => (BoundedTo<PMQuoteMaint, PMQuote>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
      actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.editQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured>) (c => (BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured) c.InFolder(processingCategory).WithFieldAssignments((Action<BoundedTo<PMQuoteMaint, PMQuote>.Assignment.IContainerFillerFields>) (fa => fa.Add<PMQuote.hold>((Func<BoundedTo<PMQuoteMaint, PMQuote>.Assignment.INeedRightOperand, BoundedTo<PMQuoteMaint, PMQuote>.Assignment.IConfigured>) (f => (BoundedTo<PMQuoteMaint, PMQuote>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.accept), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured>) (c => (BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
      actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.decline), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured>) (c => (BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured) c.InFolder(processingCategory)));
      actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.convertToProject), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured>) (c => (BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured) c.InFolder(processingCategory).IsDisabledWhen((BoundedTo<PMQuoteMaint, PMQuote>.ISharedCondition) BoundedTo<PMQuoteMaint, PMQuote>.Condition.op_LogicalNot(conditions.IsConvertToProjectEnabled))));
      actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.printQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured>) (c => (BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured) c.InFolder(printingAndEmailingCategory).MassProcessingScreen<PMQuoteProcess>().InBatchMode()));
      actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.sendQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured>) (c => (BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured) c.InFolder(printingAndEmailingCategory).MassProcessingScreen<PMQuoteProcess>().InBatchMode()));
      actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.copyQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured>) (c => (BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured) c.InFolder(otherCategory)));
      actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.primaryQuote), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured>) (c => (BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured) c.InFolder(otherCategory).IsDisabledWhen((BoundedTo<PMQuoteMaint, PMQuote>.ISharedCondition) BoundedTo<PMQuoteMaint, PMQuote>.Condition.op_LogicalNot(conditions.IsPrimaryQuoteEnabled))));
      actions.Add((Expression<Func<PMQuoteMaint, PXAction<PMQuote>>>) (g => g.validateAddresses), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured>) (c => (BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured) c.InFolder(otherCategory)));
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: field reference
      actions.Add(Expression.Lambda<Func<PMQuoteMaint, PXAction<PMQuote>>>((Expression) Expression.Field((Expression) Expression.Call(g, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXGraph.GetExtension)), Array.Empty<Expression>()), FieldInfo.GetFieldFromHandle(__fieldref (PMQuoteMaint.PMDiscount.graphRecalculateDiscountsAction))), parameterExpression), (Func<BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured>) (c => (BoundedTo<PMQuoteMaint, PMQuote>.ActionDefinition.IConfigured) c.InFolder(otherCategory)));
    })).WithCategories((Action<BoundedTo<PMQuoteMaint, PMQuote>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(printingAndEmailingCategory);
      categories.Add(otherCategory);
    }))));

    BoundedTo<PMQuoteMaint, PMQuote>.Condition Bql<T>() where T : IBqlUnary, new()
    {
      return context.Conditions.FromBql<T>();
    }
  }
}
