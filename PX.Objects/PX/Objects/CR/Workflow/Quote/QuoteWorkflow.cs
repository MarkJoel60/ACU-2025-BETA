// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Workflow.Quote.QuoteWorkflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.CR.QuoteMaint_Extensions;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CR.Workflow.Quote;

public class QuoteWorkflow : PXGraphExtension<QuoteMaint>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    QuoteWorkflow.Configure(config.GetScreenConfigurationContext<QuoteMaint, CRQuote>());
  }

  protected static void Configure(WorkflowContext<QuoteMaint, CRQuote> context)
  {
    QuoteWorkflow.Conditions conditions = context.Conditions.GetPack<QuoteWorkflow.Conditions>();
    BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IConfigured categoryProcessing = context.Categories.CreateNew("Processing", (Func<BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IConfigured>) (category => (BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IConfigured categoryApproval = context.Categories.CreateNew("Approval", (Func<BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IConfigured>) (category => (BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IConfigured) category.DisplayName("Approval")));
    BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IConfigured categoryRecordCreation = context.Categories.CreateNew("RecordCreation", (Func<BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IConfigured>) (category => (BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IConfigured) category.DisplayName("Record Creation")));
    BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IConfigured categoryActivities = context.Categories.CreateNew("Activities", (Func<BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IConfigured>) (category => (BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IConfigured) category.DisplayName("Activities")));
    BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IConfigured categoryValidation = context.Categories.CreateNew("Validation", (Func<BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IConfigured>) (category => (BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IConfigured) category.DisplayName("Validation")));
    BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IConfigured categoryOther = context.Categories.CreateNew("Ohter", (Func<BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IConfigured>) (category => (BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IConfigured) category.DisplayName("Other")));
    BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured actionSend = context.ActionDefinitions.CreateExisting((Expression<Func<QuoteMaint, PXAction<CRQuote>>>) (g => g.sendQuote), (Func<BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured>) (a =>
    {
      BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = a.WithCategory(categoryProcessing).MassProcessingScreen<CRQuoteProcess>().InBatchMode();
      BoundedTo<QuoteMaint, CRQuote>.Condition condition1 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_LogicalNot(conditions.IsApprovalMapEnabled);
      BoundedTo<QuoteMaint, CRQuote>.Condition condition2 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_False(condition1) ? condition1 : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseAnd(condition1, conditions.IsDraftState);
      BoundedTo<QuoteMaint, CRQuote>.Condition condition3 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_LogicalNot(BoundedTo<QuoteMaint, CRQuote>.Condition.op_True(condition2) ? condition2 : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseOr(condition2, conditions.IsApprovedState));
      BoundedTo<QuoteMaint, CRQuote>.Condition condition4 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_False(condition3) ? condition3 : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseAnd(condition3, BoundedTo<QuoteMaint, CRQuote>.Condition.op_LogicalNot(conditions.IsSentState));
      return (BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<QuoteMaint, CRQuote>.ISharedCondition) condition4).WithPersistOptions((ActionPersistOptions) 2).IsExposedToMobile(true);
    }));
    BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured actionMarkAsAccepted = context.ActionDefinitions.CreateExisting((Expression<Func<QuoteMaint, PXAction<CRQuote>>>) (g => g.accept), (Func<BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured>) (a =>
    {
      BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = a.WithCategory(categoryProcessing);
      BoundedTo<QuoteMaint, CRQuote>.Condition approvalMapEnabled = conditions.IsApprovalMapEnabled;
      BoundedTo<QuoteMaint, CRQuote>.Condition condition5 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_False(approvalMapEnabled) ? approvalMapEnabled : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseAnd(approvalMapEnabled, conditions.IsDraftState);
      BoundedTo<QuoteMaint, CRQuote>.Condition condition6 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_True(condition5) ? condition5 : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseOr(condition5, conditions.IsPendingApprovalState);
      BoundedTo<QuoteMaint, CRQuote>.Condition condition7 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_True(condition6) ? condition6 : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseOr(condition6, conditions.IsAcceptedState);
      BoundedTo<QuoteMaint, CRQuote>.Condition condition8 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_True(condition7) ? condition7 : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseOr(condition7, conditions.IsRejectedState);
      return (BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<QuoteMaint, CRQuote>.ISharedCondition) condition8).WithPersistOptions((ActionPersistOptions) 2).IsExposedToMobile(true);
    }));
    BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured actionEditQuote = context.ActionDefinitions.CreateExisting((Expression<Func<QuoteMaint, PXAction<CRQuote>>>) (g => g.editQuote), (Func<BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured>) (a =>
    {
      BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = a.WithCategory(categoryProcessing);
      BoundedTo<QuoteMaint, CRQuote>.Condition isPreparedState = conditions.IsPreparedState;
      BoundedTo<QuoteMaint, CRQuote>.Condition condition9 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_True(isPreparedState) ? isPreparedState : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseOr(isPreparedState, conditions.IsRejectedState);
      BoundedTo<QuoteMaint, CRQuote>.Condition condition10 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_True(condition9) ? condition9 : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseOr(condition9, conditions.IsPendingApprovalState);
      BoundedTo<QuoteMaint, CRQuote>.Condition condition11 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_LogicalNot(BoundedTo<QuoteMaint, CRQuote>.Condition.op_True(condition10) ? condition10 : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseOr(condition10, conditions.IsDeclinedState));
      return (BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<QuoteMaint, CRQuote>.ISharedCondition) condition11).WithPersistOptions((ActionPersistOptions) 2).IsExposedToMobile(true);
    }));
    BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured actionSetAsPrimary = context.ActionDefinitions.CreateExisting((Expression<Func<QuoteMaint, PXAction<CRQuote>>>) (g => g.primaryQuote), (Func<BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured>) (a => (BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured) a.WithCategory(categoryOther).IsDisabledWhen((BoundedTo<QuoteMaint, CRQuote>.ISharedCondition) conditions.IsPrimaryQuote).IsExposedToMobile(true)));
    BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured actionRequestApproval = context.ActionDefinitions.CreateExisting((Expression<Func<QuoteMaint, PXAction<CRQuote>>>) (g => g.requestApproval), (Func<BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured>) (a =>
    {
      BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = a.WithCategory(categoryApproval).PlaceAfter(actionSetAsPrimary);
      BoundedTo<QuoteMaint, CRQuote>.Condition approvalMapEnabled = conditions.IsApprovalMapEnabled;
      BoundedTo<QuoteMaint, CRQuote>.Condition condition = BoundedTo<QuoteMaint, CRQuote>.Condition.op_LogicalNot(BoundedTo<QuoteMaint, CRQuote>.Condition.op_False(approvalMapEnabled) ? approvalMapEnabled : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseAnd(approvalMapEnabled, conditions.IsDraftState));
      return (BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<QuoteMaint, CRQuote>.ISharedCondition) condition).WithPersistOptions((ActionPersistOptions) 2).IsExposedToMobile(true);
    }));
    BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured actionApprove = context.ActionDefinitions.CreateExisting((Expression<Func<QuoteMaint, PXAction<CRQuote>>>) (g => g.approve), (Func<BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured>) (a =>
    {
      BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = a.WithCategory(categoryApproval);
      BoundedTo<QuoteMaint, CRQuote>.Condition approvalMapEnabled = conditions.IsApprovalMapEnabled;
      BoundedTo<QuoteMaint, CRQuote>.Condition condition = BoundedTo<QuoteMaint, CRQuote>.Condition.op_LogicalNot(BoundedTo<QuoteMaint, CRQuote>.Condition.op_False(approvalMapEnabled) ? approvalMapEnabled : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseAnd(approvalMapEnabled, conditions.IsPendingApprovalState));
      return (BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<QuoteMaint, CRQuote>.ISharedCondition) condition).WithFieldAssignments((Action<BoundedTo<QuoteMaint, CRQuote>.Assignment.IContainerFillerFields>) (fa => fa.Add<CRQuote.approved>(new bool?(true)))).WithPersistOptions((ActionPersistOptions) 2);
    }));
    BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured actionReject = context.ActionDefinitions.CreateExisting((Expression<Func<QuoteMaint, PXAction<CRQuote>>>) (g => g.reject), (Func<BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured>) (a =>
    {
      BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = a.WithCategory(categoryApproval);
      BoundedTo<QuoteMaint, CRQuote>.Condition approvalMapEnabled = conditions.IsApprovalMapEnabled;
      BoundedTo<QuoteMaint, CRQuote>.Condition condition = BoundedTo<QuoteMaint, CRQuote>.Condition.op_LogicalNot(BoundedTo<QuoteMaint, CRQuote>.Condition.op_False(approvalMapEnabled) ? approvalMapEnabled : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseAnd(approvalMapEnabled, conditions.IsPendingApprovalState));
      return (BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<QuoteMaint, CRQuote>.ISharedCondition) condition).WithFieldAssignments((Action<BoundedTo<QuoteMaint, CRQuote>.Assignment.IContainerFillerFields>) (fa => fa.Add<CRQuote.rejected>(new bool?(true)))).WithPersistOptions((ActionPersistOptions) 2);
    }));
    BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured actionReassign = context.ActionDefinitions.CreateExisting("ReassignApproval", (Func<BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured>) (a =>
    {
      BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = a.WithCategory(categoryApproval);
      BoundedTo<QuoteMaint, CRQuote>.Condition approvalMapEnabled = conditions.IsApprovalMapEnabled;
      BoundedTo<QuoteMaint, CRQuote>.Condition condition = BoundedTo<QuoteMaint, CRQuote>.Condition.op_LogicalNot(BoundedTo<QuoteMaint, CRQuote>.Condition.op_False(approvalMapEnabled) ? approvalMapEnabled : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseAnd(approvalMapEnabled, conditions.IsPendingApprovalState));
      return (BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<QuoteMaint, CRQuote>.ISharedCondition) condition).WithPersistOptions((ActionPersistOptions) 2);
    }));
    BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured actionMarkAsConverted = context.ActionDefinitions.CreateExisting((Expression<Func<QuoteMaint, PXAction<CRQuote>>>) (g => g.markAsConverted), (Func<BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured>) (a =>
    {
      BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = a.WithCategory(categoryProcessing);
      BoundedTo<QuoteMaint, CRQuote>.Condition approvalMapEnabled = conditions.IsApprovalMapEnabled;
      BoundedTo<QuoteMaint, CRQuote>.Condition condition12 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_False(approvalMapEnabled) ? approvalMapEnabled : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseAnd(approvalMapEnabled, conditions.IsDraftState);
      BoundedTo<QuoteMaint, CRQuote>.Condition condition13 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_True(condition12) ? condition12 : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseOr(condition12, conditions.IsPendingApprovalState);
      BoundedTo<QuoteMaint, CRQuote>.Condition condition14 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_True(condition13) ? condition13 : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseOr(condition13, conditions.IsConvertedState);
      BoundedTo<QuoteMaint, CRQuote>.Condition condition15 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_True(condition14) ? condition14 : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseOr(condition14, conditions.IsRejectedState);
      return (BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<QuoteMaint, CRQuote>.ISharedCondition) condition15).WithPersistOptions((ActionPersistOptions) 2).IsExposedToMobile(true);
    }));
    BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured actionMarkAsDeclined = context.ActionDefinitions.CreateExisting((Expression<Func<QuoteMaint, PXAction<CRQuote>>>) (g => g.decline), (Func<BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured>) (a =>
    {
      BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = a.WithCategory(categoryProcessing);
      BoundedTo<QuoteMaint, CRQuote>.Condition isDraftState = conditions.IsDraftState;
      BoundedTo<QuoteMaint, CRQuote>.Condition condition16 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_True(isDraftState) ? isDraftState : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseOr(isDraftState, conditions.IsDeclinedState);
      BoundedTo<QuoteMaint, CRQuote>.Condition condition17 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_True(condition16) ? condition16 : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseOr(condition16, conditions.IsPendingApprovalState);
      BoundedTo<QuoteMaint, CRQuote>.Condition condition18 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_True(condition17) ? condition17 : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseOr(condition17, conditions.IsRejectedState);
      return (BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured) optionalConfigAction.IsHiddenWhen((BoundedTo<QuoteMaint, CRQuote>.ISharedCondition) condition18).WithPersistOptions((ActionPersistOptions) 2).IsExposedToMobile(true);
    }));
    BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured actionPrintQuote = context.ActionDefinitions.CreateExisting((Expression<Func<QuoteMaint, PXAction<CRQuote>>>) (g => g.printQuote), (Func<BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured>) (a =>
    {
      BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = a.PlaceAfter(actionSetAsPrimary).WithCategory(categoryOther).MassProcessingScreen<CRQuoteProcess>().InBatchMode();
      BoundedTo<QuoteMaint, CRQuote>.Condition approvalMapEnabled = conditions.IsApprovalMapEnabled;
      BoundedTo<QuoteMaint, CRQuote>.Condition condition19;
      if (!BoundedTo<QuoteMaint, CRQuote>.Condition.op_False(approvalMapEnabled))
      {
        BoundedTo<QuoteMaint, CRQuote>.Condition condition20 = approvalMapEnabled;
        BoundedTo<QuoteMaint, CRQuote>.Condition isDraftState = conditions.IsDraftState;
        BoundedTo<QuoteMaint, CRQuote>.Condition condition21 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_True(isDraftState) ? isDraftState : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseOr(isDraftState, conditions.IsPendingApprovalState);
        BoundedTo<QuoteMaint, CRQuote>.Condition condition22 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_True(condition21) ? condition21 : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseOr(condition21, conditions.IsRejectedState);
        condition19 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseAnd(condition20, condition22);
      }
      else
        condition19 = approvalMapEnabled;
      return (BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured) optionalConfigAction.IsDisabledWhen((BoundedTo<QuoteMaint, CRQuote>.ISharedCondition) condition19).IsExposedToMobile(true);
    }));
    BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured actionCopyQuote = context.ActionDefinitions.CreateExisting((Expression<Func<QuoteMaint, PXAction<CRQuote>>>) (g => g.copyQuote), (Func<BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured>) (a => (BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured) a.WithCategory(categoryOther).IsExposedToMobile(true)));
    BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured actionRecalculatePrices = context.ActionDefinitions.CreateExisting<QuoteMaint.Discount>((Expression<Func<QuoteMaint.Discount, PXAction<CRQuote>>>) (g => g.graphRecalculateDiscountsAction), (Func<BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured>) (a =>
    {
      BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = a.PlaceAfter(actionCopyQuote).WithCategory(categoryOther);
      BoundedTo<QuoteMaint, CRQuote>.Condition pendingApprovalState = conditions.IsPendingApprovalState;
      BoundedTo<QuoteMaint, CRQuote>.Condition condition23 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_True(pendingApprovalState) ? pendingApprovalState : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseOr(pendingApprovalState, conditions.IsRejectedState);
      BoundedTo<QuoteMaint, CRQuote>.Condition condition24 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_True(condition23) ? condition23 : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseOr(condition23, conditions.IsPreparedState);
      BoundedTo<QuoteMaint, CRQuote>.Condition condition25 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_True(condition24) ? condition24 : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseOr(condition24, conditions.IsConvertedState);
      BoundedTo<QuoteMaint, CRQuote>.Condition condition26 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_True(condition25) ? condition25 : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseOr(condition25, conditions.IsDeclinedState);
      return (BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured) optionalConfigAction.IsDisabledWhen((BoundedTo<QuoteMaint, CRQuote>.ISharedCondition) condition26).IsExposedToMobile(true);
    }));
    BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured actionValidateAddresses = context.ActionDefinitions.CreateExisting((Expression<Func<QuoteMaint, PXAction<CRQuote>>>) (g => g.validateAddresses), (Func<BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured>) (a => (BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured) a.WithCategory(categoryOther).PlaceAfter(actionRecalculatePrices).IsExposedToMobile(true)));
    BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured actionCreateSalesOrder = context.ActionDefinitions.CreateExisting<QuoteMaint.CRCreateSalesOrderExt>((Expression<Func<QuoteMaint.CRCreateSalesOrderExt, PXAction<CRQuote>>>) (g => g.CreateSalesOrder), (Func<BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured>) (a =>
    {
      BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = a.DisplayName("Convert to Order").WithCategory(categoryRecordCreation).PlaceAfter(actionValidateAddresses);
      BoundedTo<QuoteMaint, CRQuote>.Condition approvalMapEnabled = conditions.IsApprovalMapEnabled;
      BoundedTo<QuoteMaint, CRQuote>.Condition condition = BoundedTo<QuoteMaint, CRQuote>.Condition.op_False(approvalMapEnabled) ? approvalMapEnabled : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseAnd(approvalMapEnabled, BoundedTo<QuoteMaint, CRQuote>.Condition.op_LogicalNot(conditions.IsApproved));
      return (BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured) optionalConfigAction.IsDisabledWhen((BoundedTo<QuoteMaint, CRQuote>.ISharedCondition) condition).IsExposedToMobile(true);
    }));
    BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured actionCreateInvoices = context.ActionDefinitions.CreateExisting<QuoteMaint.CRCreateInvoiceExt>((Expression<Func<QuoteMaint.CRCreateInvoiceExt, PXAction<CRQuote>>>) (g => g.CreateInvoice), (Func<BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured>) (a =>
    {
      BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = a.DisplayName("Convert to Invoice").WithCategory(categoryRecordCreation).PlaceAfter(actionCreateSalesOrder);
      BoundedTo<QuoteMaint, CRQuote>.Condition approvalMapEnabled = conditions.IsApprovalMapEnabled;
      BoundedTo<QuoteMaint, CRQuote>.Condition condition = BoundedTo<QuoteMaint, CRQuote>.Condition.op_False(approvalMapEnabled) ? approvalMapEnabled : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseAnd(approvalMapEnabled, BoundedTo<QuoteMaint, CRQuote>.Condition.op_LogicalNot(conditions.IsApproved));
      return (BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured) optionalConfigAction.IsDisabledWhen((BoundedTo<QuoteMaint, CRQuote>.ISharedCondition) condition);
    }));
    BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured actionCreateTask = context.ActionDefinitions.CreateExisting("NewTask", (Func<BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured>) (a => (BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured) a.WithCategory(categoryActivities)));
    BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured actionCreateEmail = context.ActionDefinitions.CreateExisting("newMailActivity", (Func<BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured>) (a => (BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured) a.WithCategory(categoryActivities)));
    BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured actionCreatePhoneCall = context.ActionDefinitions.CreateExisting("NewActivityP_Workflow", (Func<BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured>) (a => (BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured) a.WithCategory(categoryActivities)));
    context.AddScreenConfigurationFor((Func<BoundedTo<QuoteMaint, CRQuote>.ScreenConfiguration.IStartConfigScreen, BoundedTo<QuoteMaint, CRQuote>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<QuoteMaint, CRQuote>.ScreenConfiguration.IConfigured) ((BoundedTo<QuoteMaint, CRQuote>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<CRQuote.status>().AddDefaultFlow(new Func<BoundedTo<QuoteMaint, CRQuote>.Workflow.INeedStatesFlow, BoundedTo<QuoteMaint, CRQuote>.Workflow.IConfigured>(DefaultSalesQuoteNoApprovalMapFlow)).WithActions((Action<BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add(actionSend);
      actions.Add(actionEditQuote);
      actions.Add(actionMarkAsAccepted);
      actions.Add(actionMarkAsConverted);
      actions.Add(actionMarkAsDeclined);
      actions.Add(actionRequestApproval);
      actions.Add(actionApprove);
      actions.Add(actionReject);
      actions.Add(actionReassign);
      actions.Add<QuoteMaint_CRCreateContactAction>((Expression<Func<QuoteMaint_CRCreateContactAction, PXAction<CRQuote>>>) (e => e.CreateContact), (Func<BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured>) (c => (BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured) c.WithCategory(categoryRecordCreation).WithPersistOptions((ActionPersistOptions) 2)));
      actions.Add<QuoteMaint_CRCreateBothContactAndAccountAction>((Expression<Func<QuoteMaint_CRCreateBothContactAndAccountAction, PXAction<CRQuote>>>) (e => e.CreateBothContactAndAccount), (Func<BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured>) (c => (BoundedTo<QuoteMaint, CRQuote>.ActionDefinition.IConfigured) c.WithCategory(categoryRecordCreation).WithPersistOptions((ActionPersistOptions) 2)));
      actions.Add(actionCreateSalesOrder);
      actions.Add(actionCreateInvoices);
      actions.Add(actionCreateTask);
      actions.Add(actionCreateEmail);
      actions.Add(actionCreatePhoneCall);
      actions.Add(actionValidateAddresses);
      actions.Add(actionPrintQuote);
      actions.Add(actionCopyQuote);
      actions.Add(actionSetAsPrimary);
      actions.Add(actionRecalculatePrices);
    })).WithHandlers((Action<BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers =>
    {
      handlers.Add((Func<BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => !(((BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.INeedSubscriber<PX.Objects.SO.SOOrder>) ((BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.INeedEventContainer<PX.Objects.SO.SOOrder, CRQuote>) handler.WithTargetOf<PX.Objects.SO.SOOrder>().WithParametersOf<CRQuote>()).OfEntityEvent<PX.Objects.SO.SOOrder.Events>((Expression<Func<PX.Objects.SO.SOOrder.Events, PXEntityEvent<PX.Objects.SO.SOOrder, CRQuote>>>) (e => e.OrderCreatedFromQuote))).Is((Expression<Func<PX.Objects.SO.SOOrder, PXWorkflowEventHandler<CRQuote, PX.Objects.SO.SOOrder>>>) (g => g.OnSalesOrderCreatedFromQuote)) is BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<PX.Objects.SO.SOOrder, CRQuote> primaryEntityGetter2) ? (BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) null : (BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) primaryEntityGetter2.UsesParameterAsPrimaryEntity()));
      handlers.Add((Func<BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<PX.Objects.SO.SOOrder>) ((BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.INeedSubscriber<PX.Objects.SO.SOOrder>) handler.WithTargetOf<PX.Objects.SO.SOOrder>().OfEntityEvent<PX.Objects.SO.SOOrder.Events>((Expression<Func<PX.Objects.SO.SOOrder.Events, PXEntityEvent<PX.Objects.SO.SOOrder>>>) (e => e.OrderDeleted))).Is((Expression<Func<PX.Objects.SO.SOOrder, PXWorkflowEventHandler<CRQuote, PX.Objects.SO.SOOrder>>>) (g => g.OnSalesOrderDeleted))).UsesPrimaryEntityGetter<SelectFromBase<CRQuote, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CRRelation>.On<BqlOperand<CRRelation.targetNoteID, IBqlGuid>.IsEqual<CRQuote.noteID>>>>.Where<BqlOperand<CRRelation.refNoteID, IBqlGuid>.IsEqual<BqlField<PX.Objects.SO.SOOrder.noteID, IBqlGuid>.FromCurrent>>>(false)));
      handlers.Add((Func<BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<PX.Objects.AR.ARInvoice, CRQuote>) ((BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.INeedSubscriber<PX.Objects.AR.ARInvoice, CRQuote>) ((BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.INeedEventContainer<PX.Objects.AR.ARInvoice, CRQuote>) handler.WithTargetOf<PX.Objects.AR.ARInvoice>().WithParametersOf<CRQuote>()).OfEntityEvent<PX.Objects.AR.ARInvoice.Events>((Expression<Func<PX.Objects.AR.ARInvoice.Events, PXEntityEvent<PX.Objects.AR.ARInvoice, CRQuote>>>) (e => e.ARInvoiceCreatedFromQuote))).Is((Expression<Func<PX.Objects.AR.ARInvoice, PXWorkflowEventHandler<CRQuote, PX.Objects.AR.ARInvoice, CRQuote>>>) (g => g.OnARInvoiceCreatedFromQuote))).UsesParameterAsPrimaryEntity()));
      handlers.Add((Func<BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<PX.Objects.AR.ARInvoice>) ((BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandlerDefinition.INeedSubscriber<PX.Objects.AR.ARInvoice>) handler.WithTargetOf<PX.Objects.AR.ARInvoice>().OfEntityEvent<PX.Objects.AR.ARInvoice.Events>((Expression<Func<PX.Objects.AR.ARInvoice.Events, PXEntityEvent<PX.Objects.AR.ARInvoice>>>) (e => e.ARInvoiceDeleted))).Is((Expression<Func<PX.Objects.AR.ARInvoice, PXWorkflowEventHandler<CRQuote, PX.Objects.AR.ARInvoice>>>) (g => g.OnARInvoiceDeleted))).UsesPrimaryEntityGetter<SelectFromBase<CRQuote, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CRRelation>.On<BqlOperand<CRRelation.targetNoteID, IBqlGuid>.IsEqual<CRQuote.noteID>>>>.Where<BqlOperand<CRRelation.refNoteID, IBqlGuid>.IsEqual<BqlField<PX.Objects.AR.ARInvoice.noteID, IBqlGuid>.FromCurrent>>>(false)));
    })).WithCategories((Action<BoundedTo<QuoteMaint, CRQuote>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(categoryProcessing);
      categories.Add(categoryApproval);
      categories.Add(categoryActivities);
      categories.Add(categoryRecordCreation);
      categories.Add(categoryValidation);
      categories.Add(categoryOther);
    }))));

    static void DisableQuoteMain(
      BoundedTo<QuoteMaint, CRQuote>.FieldState.IContainerFillerFields fields)
    {
      fields.AddField<CRQuote.documentDate>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.expirationDate>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.locationID>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.curyID>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.manualTotalEntry>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.curyAmount>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.curyDiscTot>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.ownerID>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.bAccountID>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.branchID>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.projectID>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.externalRef>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.workgroupID>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.termsID>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.taxZoneID>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.taxCalcMode>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.taxRegistrationID>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.externalTaxExemptionNumber>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.avalaraCustomerUsageType>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.siteID>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.carrierID>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.shipTermsID>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.shipZoneID>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.fOBPointID>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.resedential>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.saturdayDelivery>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.insurance>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.shipComplete>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddTable<CRTaxTran>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddTable<CROpportunityDiscountDetail>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddTable<CROpportunityProducts>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
    }

    static void DisableContact(
      BoundedTo<QuoteMaint, CRQuote>.FieldState.IContainerFillerFields fields)
    {
      fields.AddField<CRQuote.contactID>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddField<CRQuote.allowOverrideContactAddress>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddTable<CRContact>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
    }

    static void DisableBilling(
      BoundedTo<QuoteMaint, CRQuote>.FieldState.IContainerFillerFields fields)
    {
      fields.AddField<CRQuote.allowOverrideBillingContactAddress>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddTable<CRBillingContact>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddTable<CRBillingAddress>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
    }

    static void DisableShipping(
      BoundedTo<QuoteMaint, CRQuote>.FieldState.IContainerFillerFields fields)
    {
      fields.AddField<CRQuote.allowOverrideShippingContactAddress>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddTable<CRShippingContact>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
      fields.AddTable<CRShippingAddress>((Func<BoundedTo<QuoteMaint, CRQuote>.FieldState.INeedAnyConfigField, BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured>) (f => (BoundedTo<QuoteMaint, CRQuote>.FieldState.IConfigured) f.IsDisabled()));
    }

    static void ResetQuoteAprroveRejectStatus(
      BoundedTo<QuoteMaint, CRQuote>.Assignment.IContainerFillerFields fields)
    {
      fields.Add<CRQuote.approved>((Func<BoundedTo<QuoteMaint, CRQuote>.Assignment.INeedRightOperand, BoundedTo<QuoteMaint, CRQuote>.Assignment.IConfigured>) (e => (BoundedTo<QuoteMaint, CRQuote>.Assignment.IConfigured) e.SetFromValue((object) false)));
      fields.Add<CRQuote.rejected>((Func<BoundedTo<QuoteMaint, CRQuote>.Assignment.INeedRightOperand, BoundedTo<QuoteMaint, CRQuote>.Assignment.IConfigured>) (e => (BoundedTo<QuoteMaint, CRQuote>.Assignment.IConfigured) e.SetFromValue((object) false)));
    }

    BoundedTo<QuoteMaint, CRQuote>.Workflow.IConfigured DefaultSalesQuoteNoApprovalMapFlow(
      BoundedTo<QuoteMaint, CRQuote>.Workflow.INeedStatesFlow flow)
    {
      BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured stateDraft = context.FlowStates.Create<CRQuoteStatusAttribute.draft>((Func<BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig, BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured>) (state => (BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured) ((BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig) state.IsInitial()).WithActions((Action<BoundedTo<QuoteMaint, CRQuote>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(actionSend, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionRequestApproval, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) (a => (BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add(actionMarkAsAccepted, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionMarkAsConverted, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add<QuoteMaint_CRCreateContactAction>((Expression<Func<QuoteMaint_CRCreateContactAction, PXAction<CRQuote>>>) (e => e.CreateContact), (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add<QuoteMaint_CRCreateBothContactAndAccountAction>((Expression<Func<QuoteMaint_CRCreateBothContactAndAccountAction, PXAction<CRQuote>>>) (e => e.CreateBothContactAndAccount), (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionCreateSalesOrder, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionCreateInvoices, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        AddCommonActionState(actions);
      }))).WithEventHandlers((Action<BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add<PX.Objects.SO.SOOrder>((Expression<Func<QuoteMaint, PXWorkflowEventHandler<CRQuote, PX.Objects.SO.SOOrder>>>) (g => g.OnSalesOrderCreatedFromQuote));
        handlers.Add<PX.Objects.AR.ARInvoice, CRQuote>((Expression<Func<QuoteMaint, PXWorkflowEventHandler<CRQuote, PX.Objects.AR.ARInvoice, CRQuote>>>) (g => g.OnARInvoiceCreatedFromQuote));
      }))));
      BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured stateSent = context.FlowStates.Create<CRQuoteStatusAttribute.sent>((Func<BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig, BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured>) (state => (BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured) ((BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<QuoteMaint, CRQuote>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(actionEditQuote, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionSend, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionMarkAsAccepted, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionMarkAsConverted, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionMarkAsDeclined, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionCreateSalesOrder, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionCreateInvoices, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        AddCommonActionState(actions);
      }))).WithFieldStates((Action<BoundedTo<QuoteMaint, CRQuote>.FieldState.IContainerFillerFields>) (fields =>
      {
        DisableQuoteMain(fields);
        DisableShipping(fields);
      }))));
      BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured stateAccepted = context.FlowStates.Create<CRQuoteStatusAttribute.accepted>((Func<BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig, BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured>) (state => (BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured) ((BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<QuoteMaint, CRQuote>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(actionEditQuote, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionMarkAsConverted, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionMarkAsDeclined, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionCreateSalesOrder, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionCreateInvoices, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        AddCommonActionState(actions);
      }))).WithEventHandlers((Action<BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add<PX.Objects.SO.SOOrder>((Expression<Func<QuoteMaint, PXWorkflowEventHandler<CRQuote, PX.Objects.SO.SOOrder>>>) (g => g.OnSalesOrderCreatedFromQuote));
        handlers.Add<PX.Objects.AR.ARInvoice, CRQuote>((Expression<Func<QuoteMaint, PXWorkflowEventHandler<CRQuote, PX.Objects.AR.ARInvoice, CRQuote>>>) (g => g.OnARInvoiceCreatedFromQuote));
      }))).WithFieldStates((Action<BoundedTo<QuoteMaint, CRQuote>.FieldState.IContainerFillerFields>) (fields =>
      {
        DisableQuoteMain(fields);
        DisableShipping(fields);
      }))));
      BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured stateConverted = context.FlowStates.Create<CRQuoteStatusAttribute.converted>((Func<BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig, BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured>) (state => (BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured) ((BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<QuoteMaint, CRQuote>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(actionMarkAsAccepted, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        AddCommonActionState(actions);
      }))).WithEventHandlers((Action<BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add<PX.Objects.SO.SOOrder>((Expression<Func<QuoteMaint, PXWorkflowEventHandler<CRQuote, PX.Objects.SO.SOOrder>>>) (g => g.OnSalesOrderDeleted));
        handlers.Add<PX.Objects.AR.ARInvoice>((Expression<Func<QuoteMaint, PXWorkflowEventHandler<CRQuote, PX.Objects.AR.ARInvoice>>>) (g => g.OnARInvoiceDeleted));
      }))).WithFieldStates((Action<BoundedTo<QuoteMaint, CRQuote>.FieldState.IContainerFillerFields>) (fields =>
      {
        DisableQuoteMain(fields);
        DisableContact(fields);
        DisableBilling(fields);
        DisableShipping(fields);
      }))));
      BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured stateDeclined = context.FlowStates.Create<CRQuoteStatusAttribute.declined>((Func<BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig, BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured>) (state => (BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured) ((BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<QuoteMaint, CRQuote>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(actionEditQuote, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionMarkAsAccepted, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        AddCommonActionState(actions);
      }))).WithFieldStates((Action<BoundedTo<QuoteMaint, CRQuote>.FieldState.IContainerFillerFields>) (fields =>
      {
        DisableQuoteMain(fields);
        DisableContact(fields);
        DisableBilling(fields);
        DisableShipping(fields);
      }))));
      BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured statePendingApproval = context.FlowStates.Create<CRQuoteStatusAttribute.pendingApproval>((Func<BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig, BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured>) (state => (BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured) ((BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<QuoteMaint, CRQuote>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(actionEditQuote, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionApprove, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) (a => (BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add(actionReject, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) (a => (BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add(actionReassign, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        AddCommonActionState(actions);
      }))).WithFieldStates((Action<BoundedTo<QuoteMaint, CRQuote>.FieldState.IContainerFillerFields>) (fields =>
      {
        DisableQuoteMain(fields);
        DisableShipping(fields);
      }))));
      BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured stateRejected = context.FlowStates.Create<CRQuoteStatusAttribute.rejected>((Func<BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig, BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured>) (state => (BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured) ((BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<QuoteMaint, CRQuote>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(actionEditQuote, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) (a => (BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        AddCommonActionState(actions);
      }))).WithFieldStates((Action<BoundedTo<QuoteMaint, CRQuote>.FieldState.IContainerFillerFields>) (fields =>
      {
        DisableQuoteMain(fields);
        DisableShipping(fields);
      }))));
      BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured stateApproved = context.FlowStates.Create<CRQuoteStatusAttribute.quoteApproved>((Func<BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig, BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured>) (state => (BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured) ((BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<QuoteMaint, CRQuote>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(actionEditQuote, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionSend, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionMarkAsAccepted, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionMarkAsConverted, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionMarkAsDeclined, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionCreateSalesOrder, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionCreateInvoices, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        AddCommonActionState(actions);
      }))).WithEventHandlers((Action<BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
      {
        handlers.Add<PX.Objects.SO.SOOrder>((Expression<Func<QuoteMaint, PXWorkflowEventHandler<CRQuote, PX.Objects.SO.SOOrder>>>) (g => g.OnSalesOrderCreatedFromQuote));
        handlers.Add<PX.Objects.AR.ARInvoice, CRQuote>((Expression<Func<QuoteMaint, PXWorkflowEventHandler<CRQuote, PX.Objects.AR.ARInvoice, CRQuote>>>) (g => g.OnARInvoiceCreatedFromQuote));
      }))).WithFieldStates((Action<BoundedTo<QuoteMaint, CRQuote>.FieldState.IContainerFillerFields>) (fields =>
      {
        DisableQuoteMain(fields);
        DisableShipping(fields);
      }))));
      BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured statePrepared = context.FlowStates.Create<CRQuoteStatusAttribute.approved>((Func<BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig, BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured>) (state => (BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IConfigured) ((BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<QuoteMaint, CRQuote>.FlowState.INeedAnyFlowStateConfig) state.WithActions((Action<BoundedTo<QuoteMaint, CRQuote>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add(actionEditQuote, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionSend, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionMarkAsAccepted, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionMarkAsConverted, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionMarkAsDeclined, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionCreateSalesOrder, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        actions.Add(actionCreateInvoices, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
        AddCommonActionState(actions);
      }))).WithEventHandlers((Action<BoundedTo<QuoteMaint, CRQuote>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add<PX.Objects.SO.SOOrder>((Expression<Func<QuoteMaint, PXWorkflowEventHandler<CRQuote, PX.Objects.SO.SOOrder>>>) (g => g.OnSalesOrderCreatedFromQuote))))).WithFieldStates((Action<BoundedTo<QuoteMaint, CRQuote>.FieldState.IContainerFillerFields>) (fields =>
      {
        DisableQuoteMain(fields);
        DisableShipping(fields);
      }))));
      return (BoundedTo<QuoteMaint, CRQuote>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<QuoteMaint, CRQuote>.BaseFlowStep.IContainerFillerStates>) (states =>
      {
        states.Add(stateDraft);
        states.Add(stateSent);
        states.Add(stateAccepted);
        states.Add(stateConverted);
        states.Add(stateDeclined);
        states.Add(stateApproved);
        states.Add(stateRejected);
        states.Add(statePendingApproval);
        states.Add(statePrepared);
      })).WithTransitions((Action<BoundedTo<QuoteMaint, CRQuote>.Transition.IContainerFillerTransitions>) (transitions =>
      {
        transitions.AddGroupFrom(stateDraft, (Action<BoundedTo<QuoteMaint, CRQuote>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateSent).IsTriggeredOn(actionSend)));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateAccepted).IsTriggeredOn(actionMarkAsAccepted)));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateConverted).IsTriggeredOn(actionMarkAsConverted)));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateConverted).IsTriggeredOn((Expression<Func<QuoteMaint, PXWorkflowEventHandlerBase<CRQuote>>>) (g => g.OnSalesOrderCreatedFromQuote))));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateApproved).IsTriggeredOn(actionRequestApproval).When((BoundedTo<QuoteMaint, CRQuote>.ISharedCondition) conditions.IsApproved)));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateRejected).IsTriggeredOn(actionRequestApproval).When((BoundedTo<QuoteMaint, CRQuote>.ISharedCondition) conditions.IsRejected)));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t =>
          {
            BoundedTo<QuoteMaint, CRQuote>.Transition.IAllowOptionalConfig iallowOptionalConfig = t.To(statePendingApproval).IsTriggeredOn(actionRequestApproval);
            BoundedTo<QuoteMaint, CRQuote>.Condition condition29 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_LogicalNot(conditions.IsRejected);
            BoundedTo<QuoteMaint, CRQuote>.Condition condition30 = BoundedTo<QuoteMaint, CRQuote>.Condition.op_False(condition29) ? condition29 : BoundedTo<QuoteMaint, CRQuote>.Condition.op_BitwiseAnd(condition29, BoundedTo<QuoteMaint, CRQuote>.Condition.op_LogicalNot(conditions.IsApproved));
            return (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) iallowOptionalConfig.When((BoundedTo<QuoteMaint, CRQuote>.ISharedCondition) condition30);
          }));
        }));
        transitions.AddGroupFrom(statePendingApproval, (Action<BoundedTo<QuoteMaint, CRQuote>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateDraft).IsTriggeredOn(actionEditQuote).WithFieldAssignments((Action<BoundedTo<QuoteMaint, CRQuote>.Assignment.IContainerFillerFields>) (fas => ResetQuoteAprroveRejectStatus(fas)))));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateApproved).IsTriggeredOn(actionApprove).When((BoundedTo<QuoteMaint, CRQuote>.ISharedCondition) conditions.IsApproved)));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateRejected).IsTriggeredOn(actionReject).When((BoundedTo<QuoteMaint, CRQuote>.ISharedCondition) conditions.IsRejected)));
        }));
        transitions.AddGroupFrom(stateApproved, (Action<BoundedTo<QuoteMaint, CRQuote>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateDraft).IsTriggeredOn(actionEditQuote).WithFieldAssignments((Action<BoundedTo<QuoteMaint, CRQuote>.Assignment.IContainerFillerFields>) (fas => ResetQuoteAprroveRejectStatus(fas)))));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateSent).IsTriggeredOn(actionSend)));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateAccepted).IsTriggeredOn(actionMarkAsAccepted)));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateConverted).IsTriggeredOn(actionMarkAsConverted)));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateDeclined).IsTriggeredOn(actionMarkAsDeclined)));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateConverted).IsTriggeredOn((Expression<Func<QuoteMaint, PXWorkflowEventHandlerBase<CRQuote>>>) (g => g.OnSalesOrderCreatedFromQuote))));
        }));
        transitions.AddGroupFrom(statePrepared, (Action<BoundedTo<QuoteMaint, CRQuote>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateDraft).IsTriggeredOn(actionEditQuote).WithFieldAssignments((Action<BoundedTo<QuoteMaint, CRQuote>.Assignment.IContainerFillerFields>) (fas => ResetQuoteAprroveRejectStatus(fas)))));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateSent).IsTriggeredOn(actionSend)));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateAccepted).IsTriggeredOn(actionMarkAsAccepted)));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateConverted).IsTriggeredOn(actionMarkAsConverted)));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateDeclined).IsTriggeredOn(actionMarkAsDeclined)));
        }));
        transitions.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedSource, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (ts => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) ts.From(stateRejected).To(stateDraft).IsTriggeredOn(actionEditQuote).WithFieldAssignments((Action<BoundedTo<QuoteMaint, CRQuote>.Assignment.IContainerFillerFields>) (fas => ResetQuoteAprroveRejectStatus(fas)))));
        transitions.AddGroupFrom(stateSent, (Action<BoundedTo<QuoteMaint, CRQuote>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateAccepted).IsTriggeredOn(actionMarkAsAccepted)));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateDraft).IsTriggeredOn(actionEditQuote).WithFieldAssignments((Action<BoundedTo<QuoteMaint, CRQuote>.Assignment.IContainerFillerFields>) (fas => ResetQuoteAprroveRejectStatus(fas)))));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateConverted).IsTriggeredOn(actionMarkAsConverted)));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateDeclined).IsTriggeredOn(actionMarkAsDeclined)));
        }));
        transitions.AddGroupFrom(stateAccepted, (Action<BoundedTo<QuoteMaint, CRQuote>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateDraft).IsTriggeredOn(actionEditQuote).WithFieldAssignments((Action<BoundedTo<QuoteMaint, CRQuote>.Assignment.IContainerFillerFields>) (fas => ResetQuoteAprroveRejectStatus(fas)))));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateConverted).IsTriggeredOn(actionMarkAsConverted)));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateDeclined).IsTriggeredOn(actionMarkAsDeclined)));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateConverted).IsTriggeredOn((Expression<Func<QuoteMaint, PXWorkflowEventHandlerBase<CRQuote>>>) (g => g.OnARInvoiceCreatedFromQuote))));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateConverted).IsTriggeredOn((Expression<Func<QuoteMaint, PXWorkflowEventHandlerBase<CRQuote>>>) (g => g.OnSalesOrderCreatedFromQuote))));
        }));
        transitions.AddGroupFrom(stateConverted, (Action<BoundedTo<QuoteMaint, CRQuote>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateAccepted).IsTriggeredOn(actionMarkAsAccepted)));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateAccepted).IsTriggeredOn((Expression<Func<QuoteMaint, PXWorkflowEventHandlerBase<CRQuote>>>) (g => g.OnSalesOrderDeleted))));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateAccepted).IsTriggeredOn((Expression<Func<QuoteMaint, PXWorkflowEventHandlerBase<CRQuote>>>) (g => g.OnARInvoiceDeleted))));
        }));
        transitions.AddGroupFrom(stateDeclined, (Action<BoundedTo<QuoteMaint, CRQuote>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateDraft).IsTriggeredOn(actionEditQuote).WithFieldAssignments((Action<BoundedTo<QuoteMaint, CRQuote>.Assignment.IContainerFillerFields>) (fas => ResetQuoteAprroveRejectStatus(fas)))));
          ts.Add((Func<BoundedTo<QuoteMaint, CRQuote>.Transition.INeedTarget, BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured>) (t => (BoundedTo<QuoteMaint, CRQuote>.Transition.IConfigured) t.To(stateAccepted).IsTriggeredOn(actionMarkAsAccepted)));
        }));
      }));
    }

    void AddCommonActionState(
      BoundedTo<QuoteMaint, CRQuote>.ActionState.IContainerFillerActions actions)
    {
      actions.Add(actionSetAsPrimary, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
      actions.Add(actionPrintQuote, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
      actions.Add(actionCopyQuote, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
      actions.Add(actionRecalculatePrices, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
      actions.Add(actionValidateAddresses, (Func<BoundedTo<QuoteMaint, CRQuote>.ActionState.IAllowOptionalConfig, BoundedTo<QuoteMaint, CRQuote>.ActionState.IConfigured>) null);
    }
  }

  public class Conditions : BoundedTo<QuoteMaint, CRQuote>.Condition.Pack
  {
    public BoundedTo<QuoteMaint, CRQuote>.Condition IsApprovalMapEnabled
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<QuoteMaint, CRQuote>.Condition.ConditionBuilder, BoundedTo<QuoteMaint, CRQuote>.Condition>) (b => b.FromExpr((Func<CRQuote, bool>) (g => g.IsSetupApprovalRequired.GetValueOrDefault()))), nameof (IsApprovalMapEnabled));
      }
    }

    public BoundedTo<QuoteMaint, CRQuote>.Condition IsPendingApprovalState
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<QuoteMaint, CRQuote>.Condition.ConditionBuilder, BoundedTo<QuoteMaint, CRQuote>.Condition>) (b => b.FromExpr((Func<CRQuote, bool>) (g => g.Status == "P"))), nameof (IsPendingApprovalState));
      }
    }

    public BoundedTo<QuoteMaint, CRQuote>.Condition IsRejectedState
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<QuoteMaint, CRQuote>.Condition.ConditionBuilder, BoundedTo<QuoteMaint, CRQuote>.Condition>) (b => b.FromExpr((Func<CRQuote, bool>) (g => g.Status == "R"))), nameof (IsRejectedState));
      }
    }

    public BoundedTo<QuoteMaint, CRQuote>.Condition IsDraftState
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<QuoteMaint, CRQuote>.Condition.ConditionBuilder, BoundedTo<QuoteMaint, CRQuote>.Condition>) (b => b.FromExpr((Func<CRQuote, bool>) (g => g.Status == "D"))), nameof (IsDraftState));
      }
    }

    public BoundedTo<QuoteMaint, CRQuote>.Condition IsPreparedState
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<QuoteMaint, CRQuote>.Condition.ConditionBuilder, BoundedTo<QuoteMaint, CRQuote>.Condition>) (b => b.FromExpr((Func<CRQuote, bool>) (g => g.Status == "A" || g.Status == "V" || g.Status == "S" || g.Status == "T"))), nameof (IsPreparedState));
      }
    }

    public BoundedTo<QuoteMaint, CRQuote>.Condition IsDeclinedState
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<QuoteMaint, CRQuote>.Condition.ConditionBuilder, BoundedTo<QuoteMaint, CRQuote>.Condition>) (b => b.FromExpr((Func<CRQuote, bool>) (g => g.Status == "L"))), nameof (IsDeclinedState));
      }
    }

    public BoundedTo<QuoteMaint, CRQuote>.Condition IsSentState
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<QuoteMaint, CRQuote>.Condition.ConditionBuilder, BoundedTo<QuoteMaint, CRQuote>.Condition>) (b => b.FromExpr((Func<CRQuote, bool>) (g => g.Status == "S"))), nameof (IsSentState));
      }
    }

    public BoundedTo<QuoteMaint, CRQuote>.Condition IsApprovedState
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<QuoteMaint, CRQuote>.Condition.ConditionBuilder, BoundedTo<QuoteMaint, CRQuote>.Condition>) (b => b.FromExpr((Func<CRQuote, bool>) (g => g.Status == "A" || g.Status == "V"))), nameof (IsApprovedState));
      }
    }

    public BoundedTo<QuoteMaint, CRQuote>.Condition IsConvertedState
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<QuoteMaint, CRQuote>.Condition.ConditionBuilder, BoundedTo<QuoteMaint, CRQuote>.Condition>) (b => b.FromExpr((Func<CRQuote, bool>) (g => g.Status == "O"))), nameof (IsConvertedState));
      }
    }

    public BoundedTo<QuoteMaint, CRQuote>.Condition IsAcceptedState
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<QuoteMaint, CRQuote>.Condition.ConditionBuilder, BoundedTo<QuoteMaint, CRQuote>.Condition>) (b => b.FromExpr((Func<CRQuote, bool>) (g => g.Status == "T"))), nameof (IsAcceptedState));
      }
    }

    public BoundedTo<QuoteMaint, CRQuote>.Condition IsPrimaryQuote
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<QuoteMaint, CRQuote>.Condition.ConditionBuilder, BoundedTo<QuoteMaint, CRQuote>.Condition>) (b => b.FromExpr((Func<CRQuote, bool>) (g => g.IsPrimary.GetValueOrDefault()))), nameof (IsPrimaryQuote));
      }
    }

    public BoundedTo<QuoteMaint, CRQuote>.Condition IsApproved
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<QuoteMaint, CRQuote>.Condition.ConditionBuilder, BoundedTo<QuoteMaint, CRQuote>.Condition>) (b => b.FromBql<BqlOperand<CRQuote.approved, IBqlBool>.IsEqual<True>>()), nameof (IsApproved));
      }
    }

    public BoundedTo<QuoteMaint, CRQuote>.Condition IsRejected
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<QuoteMaint, CRQuote>.Condition.ConditionBuilder, BoundedTo<QuoteMaint, CRQuote>.Condition>) (b => b.FromBql<BqlOperand<CRQuote.rejected, IBqlBool>.IsEqual<True>>()), nameof (IsRejected));
      }
    }
  }

  public static class CategoryNames
  {
    public const string Processing = "Processing";
    public const string Approval = "Approval";
    public const string RecordCreation = "RecordCreation";
    public const string Activities = "Activities";
    public const string Validation = "Validation";
    public const string Other = "Ohter";
  }
}
