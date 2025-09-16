// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceOrderEntryWorkflow
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.FS.SiteStatusLookup;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.FS;

public class ServiceOrderEntryWorkflow : PXGraphExtension<ServiceOrderEntry>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    ServiceOrderEntryWorkflow.Configure(config.GetScreenConfigurationContext<ServiceOrderEntry, FSServiceOrder>());
  }

  protected static void Configure(
    WorkflowContext<ServiceOrderEntry, FSServiceOrder> context)
  {
    var conditions = new
    {
      IsOnHold = Bql<BqlOperand<FSServiceOrder.hold, IBqlBool>.IsEqual<True>>(),
      IsNotOnHold = Bql<BqlOperand<FSServiceOrder.hold, IBqlBool>.IsEqual<False>>(),
      IsQuote = Bql<BqlOperand<FSServiceOrder.quote, IBqlBool>.IsEqual<True>>(),
      IsOpen = Bql<BqlOperand<FSServiceOrder.openDoc, IBqlBool>.IsEqual<True>>(),
      IsCompleted = Bql<BqlOperand<FSServiceOrder.completed, IBqlBool>.IsEqual<True>>(),
      IsCanceled = Bql<BqlOperand<FSServiceOrder.canceled, IBqlBool>.IsEqual<True>>(),
      IsAwaiting = Bql<BqlOperand<FSServiceOrder.awaiting, IBqlBool>.IsEqual<True>>(),
      IsClosed = Bql<BqlOperand<FSServiceOrder.closed, IBqlBool>.IsEqual<True>>(),
      IsCopied = Bql<BqlOperand<FSServiceOrder.copied, IBqlBool>.IsEqual<True>>(),
      IsBilled = Bql<BqlOperand<FSServiceOrder.billed, IBqlBool>.IsEqual<True>>(),
      IsAllowedForInvoice = Bql<BqlOperand<FSServiceOrder.allowInvoice, IBqlBool>.IsEqual<True>>(),
      IsNotAllowedForInvoice = Bql<BqlOperand<FSServiceOrder.allowInvoice, IBqlBool>.IsEqual<False>>(),
      UserConfirmedClosing = Bql<BqlOperand<FSServiceOrder.userConfirmedClosing, IBqlBool>.IsEqual<True>>(),
      UserConfirmedUnclosing = Bql<BqlOperand<FSServiceOrder.userConfirmedUnclosing, IBqlBool>.IsEqual<True>>(),
      IsInternalBehavior = Bql<BqlOperand<Current<FSSrvOrdType.behavior>, IBqlString>.IsEqual<ListField.ServiceOrderTypeBehavior.internalAppointment>>(),
      PostToSOSIPM = Bql<BqlOperand<Current<FSSrvOrdType.postToSOSIPM>, IBqlBool>.IsEqual<True>>(),
      PostToProjects = Bql<BqlOperand<Current<FSSrvOrdType.postTo>, IBqlString>.IsEqual<FSPostTo.Projects>>(),
      CustomerIsSpecified = Bql<BqlOperand<FSServiceOrder.customerID, IBqlInt>.IsNotNull>(),
      CustomerIsProspect = Bql<BqlOperand<Current<BAccount.type>, IBqlString>.IsEqual<BAccountType.prospectType>>(),
      IsFinalState = Bql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSServiceOrder.canceled, Equal<True>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSServiceOrder.copied, Equal<True>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSServiceOrder.closed, Equal<True>>>>>.Or<BqlOperand<FSServiceOrder.hold, IBqlBool>.IsEqual<True>>>>>(),
      IsQuickProcessEnabled = Bql<BqlOperand<Current<FSSrvOrdType.allowQuickProcess>, IBqlBool>.IsEqual<True>>(),
      IsBilledByAppointment = Bql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSServiceOrder.billingBy, IsNotNull>>>>.And<BqlOperand<Current<FSServiceOrder.billingBy>, IBqlString>.IsEqual<ListField_Billing_By.Appointment>>>>>.Or<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSServiceOrder.billingBy, IsNull>>>>.And<BqlOperand<Current<FSBillingCycle.billingBy>, IBqlString>.IsEqual<ListField_Billing_By.Appointment>>>>>(),
      IsBilledByContract = Bql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSServiceOrder.billServiceContractID, IsNotNull>>>>.And<BqlOperand<Current<FSServiceContract.billingType>, IBqlString>.IsEqual<ListField.ServiceContractBillingType.standardizedBillings>>>(),
      HasNoLinesToCreatePurchaseOrder = Bql<BqlOperand<FSServiceOrder.pendingPOLineCntr, IBqlInt>.IsEqual<Zero>>(),
      IsExpenseFeatureEnabled = Bql<FeatureInstalled<FeaturesSet.expenseManagement>>(),
      IsInserted = Bql<BqlOperand<FSServiceOrder.sOID, IBqlInt>.IsLess<Zero>>(),
      RoomFeatureEnabled = Bql<BqlOperand<Current<FSSetup.manageRooms>, IBqlBool>.IsEqual<True>>()
    }.AutoNameConditions();
    CommonActionCategories.Categories<ServiceOrderEntry, FSServiceOrder> categories1 = CommonActionCategories.Get<ServiceOrderEntry, FSServiceOrder>(context);
    BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionCategory.IConfigured processingCategory = categories1.Processing;
    BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionCategory.IConfigured correctionCategory = context.Categories.CreateNew("Corrections Category", (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionCategory.IConfigured>) (category => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionCategory.IConfigured) category.DisplayName("Corrections")));
    BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionCategory.IConfigured schedulingCategory = context.Categories.CreateNew("Scheduling Category", (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionCategory.IConfigured>) (category => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionCategory.IConfigured) category.DisplayName("Scheduling")));
    BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionCategory.IConfigured printingEmailingCategory = categories1.PrintingAndEmailing;
    BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionCategory.IConfigured replenishmentCategory = context.Categories.CreateNew("Replenishment Category", (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionCategory.IConfigured>) (category => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionCategory.IConfigured) category.DisplayName("Replenishment")));
    BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionCategory.IConfigured otherCategory = categories1.Other;
    context.AddScreenConfigurationFor((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ScreenConfiguration.IStartConfigScreen, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ScreenConfiguration.IConfigured) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<FSServiceOrder.status>().FlowTypeIdentifierIs<FSServiceOrder.workflowTypeID>(false).WithFlows((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Workflow.IContainerFillerFlows>) (flows =>
    {
      flows.AddDefault(new Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Workflow.INeedStatesFlow, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Workflow.IConfigured>(SimpleServiceOrderFlow));
      flows.Add<ListField.ServiceOrderWorkflowTypes.quote>(new Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Workflow.INeedStatesFlow, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Workflow.IConfigured>(QuoteServiceOrderFlow));
      flows.Add<ListField.ServiceOrderWorkflowTypes.simple>(new Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Workflow.INeedStatesFlow, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Workflow.IConfigured>(SimpleServiceOrderFlow));
    })).WithActions((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.initializeState), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) c.IsHiddenAlways()));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.releaseFromHold), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
      {
        fas.Add<FSServiceOrder.hold>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fas.Add<FSServiceOrder.openDoc>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
      }))));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.putOnHold), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).WithPersistOptions((ActionPersistOptions) 1).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
      {
        fas.Add<FSServiceOrder.hold>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
        fas.Add<FSServiceOrder.openDoc>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
      }))));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.completeOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).IsDisabledWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) conditions.IsQuote).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
      {
        fas.Add<FSServiceOrder.skipExternalTaxCalculation>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
        fas.Add<FSServiceOrder.completeActionRunning>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
      }))));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.confirmQuote), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<FSServiceOrder.skipExternalTaxCalculation>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.copyToServiceOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) c.WithCategory(processingCategory)));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.closeOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
      {
        fas.Add<FSServiceOrder.skipExternalTaxCalculation>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
        fas.Add<FSServiceOrder.closeActionRunning>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
      })).IsDisabledWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) conditions.IsQuote)));
      actions.Add<ServiceOrderEntry.ServiceOrderQuickProcess>((Expression<Func<ServiceOrderEntry.ServiceOrderQuickProcess, PXAction<FSServiceOrder>>>) (g => g.quickProcess), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction11 = c.WithCategory(processingCategory);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition billedByAppointment3 = conditions.IsBilledByAppointment;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition77 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(billedByAppointment3) ? billedByAppointment3 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(billedByAppointment3, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_LogicalNot(conditions.IsQuickProcessEnabled));
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition78 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition77) ? condition77 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition77, conditions.IsOnHold);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition79 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition78) ? condition78 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition78, conditions.IsCanceled);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition80 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition79) ? condition79 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition79, conditions.IsBilledByContract);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition81 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition80) ? condition80 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition80, conditions.IsBilled);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction12 = optionalConfigAction11.IsDisabledWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) condition81);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition billedByAppointment4 = conditions.IsBilledByAppointment;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition82 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(billedByAppointment4) ? billedByAppointment4 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(billedByAppointment4, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_LogicalNot(conditions.IsQuickProcessEnabled));
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition83 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition82) ? condition82 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition82, conditions.IsBilledByContract);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition84 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition83) ? condition83 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition83, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_LogicalNot(conditions.HasNoLinesToCreatePurchaseOrder));
        return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) optionalConfigAction12.IsHiddenWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) condition84);
      }));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.allowBilling), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction13 = c.WithCategory(processingCategory);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition isOnHold = conditions.IsOnHold;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition85 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(isOnHold) ? isOnHold : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(isOnHold, conditions.IsCanceled);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition86 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition85) ? condition85 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition85, conditions.IsAllowedForInvoice);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition87 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition86) ? condition86 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition86, conditions.IsBilled);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition88 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition87) ? condition87 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition87, conditions.IsBilledByAppointment);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition89 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition88) ? condition88 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition88, conditions.IsInternalBehavior);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction14 = optionalConfigAction13.IsDisabledWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) condition89);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition billedByAppointment = conditions.IsBilledByAppointment;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition90 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(billedByAppointment) ? billedByAppointment : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(billedByAppointment, conditions.IsAllowedForInvoice);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition91 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition90) ? condition90 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition90, conditions.IsQuote);
        return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) optionalConfigAction14.IsHiddenWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) condition91);
      }));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.invoiceOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction15 = c.WithCategory(processingCategory);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition isOnHold = conditions.IsOnHold;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition92 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(isOnHold) ? isOnHold : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(isOnHold, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_LogicalNot(conditions.IsAllowedForInvoice));
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition93 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition92) ? condition92 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition92, conditions.IsBilled);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition94 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition93) ? condition93 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition93, conditions.IsBilledByContract);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition95 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition94) ? condition94 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition94, conditions.IsInternalBehavior);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction16 = optionalConfigAction15.IsDisabledWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) condition95);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition billedByAppointment = conditions.IsBilledByAppointment;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition96 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(billedByAppointment) ? billedByAppointment : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(billedByAppointment, conditions.IsInternalBehavior);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition97 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition96) ? condition96 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition96, conditions.IsQuote);
        return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) optionalConfigAction16.IsHiddenWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) condition97);
      }));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) c.WithCategory(processingCategory).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
      {
        fas.Add<FSServiceOrder.skipExternalTaxCalculation>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
        fas.Add<FSServiceOrder.cancelActionRunning>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
      }))));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.scheduleAppointment), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c.WithCategory(schedulingCategory);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition isFinalState = conditions.IsFinalState;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition98 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(isFinalState) ? isFinalState : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(isFinalState, conditions.IsCompleted);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition99 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition98) ? condition98 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition98, conditions.CustomerIsProspect);
        return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) optionalConfigAction.IsDisabledWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) condition99).IsHiddenWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) conditions.IsQuote);
      }));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.openEmployeeBoard), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c.WithCategory(schedulingCategory);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition isFinalState = conditions.IsFinalState;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition100 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(isFinalState) ? isFinalState : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(isFinalState, conditions.IsCompleted);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition101 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition100) ? condition100 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition100, conditions.CustomerIsProspect);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition102 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition101) ? condition101 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition101, conditions.IsInserted);
        return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) optionalConfigAction.IsDisabledWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) condition102).IsHiddenWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) conditions.IsQuote);
      }));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.openUserCalendar), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c.WithCategory(schedulingCategory);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition isFinalState = conditions.IsFinalState;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition103 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(isFinalState) ? isFinalState : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(isFinalState, conditions.IsCompleted);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition104 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition103) ? condition103 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition103, conditions.CustomerIsProspect);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition105 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition104) ? condition104 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition104, conditions.IsInserted);
        return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) optionalConfigAction.IsDisabledWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) condition105).IsHiddenWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) conditions.IsQuote);
      }));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.OpenRoomBoard), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction17 = c.WithCategory(schedulingCategory);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition isFinalState = conditions.IsFinalState;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition106 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(isFinalState) ? isFinalState : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(isFinalState, conditions.IsCompleted);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition107 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition106) ? condition106 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition106, conditions.CustomerIsProspect);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition108 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition107) ? condition107 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition107, conditions.IsInserted);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition109 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition108) ? condition108 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition108, conditions.IsAwaiting);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction18 = optionalConfigAction17.IsDisabledWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) condition109);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition110 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_LogicalNot(conditions.RoomFeatureEnabled);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition111 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition110) ? condition110 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition110, conditions.IsQuote);
        return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) optionalConfigAction18.IsHiddenWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) condition111);
      }));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.reopenOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c.WithCategory(correctionCategory).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<FSServiceOrder.skipExternalTaxCalculation>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
          fas.Add<FSServiceOrder.reopenActionRunning>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
        }));
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition isClosed = conditions.IsClosed;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(isClosed) ? isClosed : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(isClosed, conditions.IsBilled);
        return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) optionalConfigAction.IsDisabledWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) condition);
      }));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.uncloseOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) c.WithCategory(correctionCategory).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
      {
        fas.Add<FSServiceOrder.skipExternalTaxCalculation>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
        fas.Add<FSServiceOrder.unCloseActionRunning>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
      })).IsDisabledWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) conditions.IsQuote)));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.billReversal), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction19 = c.WithCategory(correctionCategory);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition isInserted = conditions.IsInserted;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition112 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(isInserted) ? isInserted : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(isInserted, conditions.IsBilledByAppointment);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition113 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition112) ? condition112 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition112, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_LogicalNot(conditions.PostToProjects));
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction20 = optionalConfigAction19.IsDisabledWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) condition113);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition billedByAppointment = conditions.IsBilledByAppointment;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition114 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(billedByAppointment) ? billedByAppointment : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(billedByAppointment, conditions.IsInternalBehavior);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition115 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition114) ? condition114 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition114, conditions.IsQuote);
        return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) optionalConfigAction20.IsHiddenWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) condition115);
      }));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.printServiceOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) c.WithCategory(printingEmailingCategory)));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.printServiceTimeActivityReport), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) c.WithCategory(printingEmailingCategory).IsHiddenWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) conditions.IsQuote)));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.serviceOrderAppointmentsReport), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) c.WithCategory(printingEmailingCategory).IsHiddenWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) conditions.IsQuote)));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.createPurchaseOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c.WithCategory(replenishmentCategory);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition createPurchaseOrder = conditions.HasNoLinesToCreatePurchaseOrder;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition116 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(createPurchaseOrder) ? createPurchaseOrder : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(createPurchaseOrder, conditions.CustomerIsProspect);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition117 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition116) ? condition116 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition116, conditions.IsFinalState);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition118 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition117) ? condition117 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition117, conditions.IsCompleted);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition119 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition118) ? condition118 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition118, conditions.IsBilled);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition120 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition119) ? condition119 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition119, conditions.IsAwaiting);
        return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) optionalConfigAction.IsDisabledWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) condition120).IsHiddenWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) conditions.IsQuote);
      }));
      actions.Add<ServiceOrderEntryExternalTax>((Expression<Func<ServiceOrderEntryExternalTax, PXAction<FSServiceOrder>>>) (g => g.recalcExternalTax), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c.WithCategory(otherCategory);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition isCanceled = conditions.IsCanceled;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition121 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(isCanceled) ? isCanceled : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(isCanceled, conditions.IsCopied);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition122 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition121) ? condition121 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition121, conditions.IsClosed);
        return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) optionalConfigAction.IsDisabledWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) condition122);
      }));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.createPrepayment), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition isQuote = conditions.IsQuote;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition123 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(isQuote) ? isQuote : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(isQuote, conditions.IsFinalState);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition124 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition123) ? condition123 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition123, conditions.IsInternalBehavior);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition125 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition124) ? condition124 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition124, conditions.IsBilledByContract);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition126 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition125) ? condition125 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition125, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_LogicalNot(conditions.PostToSOSIPM));
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition127 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition126) ? condition126 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition126, conditions.IsAllowedForInvoice);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition128 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition127) ? condition127 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition127, conditions.IsCompleted);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition129 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition128) ? condition128 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition128, conditions.IsInserted);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition130 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition129) ? condition129 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition129, conditions.IsAwaiting);
        return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) optionalConfigAction.IsDisabledWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) condition130);
      }));
      actions.Add<ServiceOrderSiteStatusLookupExt>((Expression<Func<ServiceOrderSiteStatusLookupExt, PXAction<FSServiceOrder>>>) (g => g.showItems), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition isCanceled = conditions.IsCanceled;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition131 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(isCanceled) ? isCanceled : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(isCanceled, conditions.IsBilled);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition132 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition131) ? condition131 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition131, conditions.IsClosed);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition133 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition132) ? condition132 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition132, conditions.IsInternalBehavior);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition134 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition133) ? condition133 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition133, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_LogicalNot(conditions.CustomerIsSpecified));
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition135;
        if (!BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition134))
        {
          BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition136 = condition134;
          BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition isQuote = conditions.IsQuote;
          BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition137 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_False(isQuote) ? isQuote : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseAnd(isQuote, conditions.IsCopied);
          condition135 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition136, condition137);
        }
        else
          condition135 = condition134;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition138 = condition135;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition139 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition138) ? condition138 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition138, conditions.IsAllowedForInvoice);
        return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) optionalConfigAction.IsDisabledWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) condition139);
      }));
      actions.Add<ServiceOrderSiteStatusLookupExt>((Expression<Func<ServiceOrderSiteStatusLookupExt, PXAction<FSServiceOrder>>>) (g => g.addSelectedItems), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) c.IsDisabledWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) conditions.IsFinalState)));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.addReceipt), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition isCanceled = conditions.IsCanceled;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition140 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(isCanceled) ? isCanceled : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(isCanceled, conditions.IsInternalBehavior);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition141 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition140) ? condition140 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition140, conditions.IsQuote);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition142 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition141) ? condition141 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition141, conditions.IsInserted);
        return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) optionalConfigAction.IsDisabledWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) condition142).IsHiddenWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_LogicalNot(conditions.IsExpenseFeatureEnabled));
      }));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.addBill), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition isCanceled = conditions.IsCanceled;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition143 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(isCanceled) ? isCanceled : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(isCanceled, conditions.IsInternalBehavior);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition144 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition143) ? condition143 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition143, conditions.IsQuote);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition145 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition144) ? condition144 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition144, conditions.IsInserted);
        return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) optionalConfigAction.IsDisabledWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) condition145);
      }));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.openStaffSelectorFromServiceTab), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition isCanceled = conditions.IsCanceled;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition146 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(isCanceled) ? isCanceled : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(isCanceled, conditions.IsBilled);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition147 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition146) ? condition146 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition146, conditions.IsClosed);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition148;
        if (!BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition147))
        {
          BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition149 = condition147;
          BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition isQuote = conditions.IsQuote;
          BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition150 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_False(isQuote) ? isQuote : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseAnd(isQuote, conditions.IsCopied);
          condition148 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition149, condition150);
        }
        else
          condition148 = condition147;
        return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) optionalConfigAction.IsDisabledWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) condition148);
      }));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.openStaffSelectorFromStaffTab), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) (c =>
      {
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = c;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition isCanceled = conditions.IsCanceled;
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition151 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(isCanceled) ? isCanceled : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(isCanceled, conditions.IsBilled);
        BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition condition152 = BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_True(condition151) ? condition151 : BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition.op_BitwiseOr(condition151, conditions.IsClosed);
        return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured) optionalConfigAction.IsDisabledWhen((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) condition152);
      }));
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.OpenScheduleScreen), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) null);
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.openSource), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) null);
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.openServiceOrderScreen), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) null);
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.viewDirectionOnMap), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) null);
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.createNewCustomer), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) null);
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.OpenPostingDocument), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) null);
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.viewPayment), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) null);
      actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.validateAddress), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionDefinition.IConfigured>) null);
    })).WithCategories((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(processingCategory);
      categories.Add(schedulingCategory);
      categories.Add(correctionCategory);
      categories.Add(printingEmailingCategory);
      categories.Add(replenishmentCategory);
      categories.Add(otherCategory);
    })).WithHandlers((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers =>
    {
      handlers.Add(new Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>(OnServiceOrderDeleted));
      handlers.Add(new Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>(OnServiceContractCleared));
      handlers.Add(new Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>(OnServiceContractPeriodAssigned));
      handlers.Add(new Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>(OnRequiredServiceContractPeriodCleared));
      handlers.Add(new Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>(OnLastAppointmentCompleted));
      handlers.Add(new Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>(OnLastAppointmentCanceled));
      handlers.Add(new Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>(OnLastAppointmentClosed));
      handlers.Add(new Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>(OnAppointmentReOpened));
      handlers.Add(new Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>(OnAppointmentUnclosed));
      handlers.Add(new Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>(OnAppointmentEdit));
    }))));

    static void DisableScreenByQuote(
      BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IContainerFillerFields states)
    {
      states.AddTable<FSSOEmployee>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.INeedAnyConfigField, BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IConfigured>) (state => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IConfigured) state.IsDisabled()));
      states.AddTable<FSSOResource>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.INeedAnyConfigField, BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IConfigured>) (state => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IConfigured) state.IsDisabled()));
    }

    static void DisableWholeScreen(
      BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IContainerFillerFields states)
    {
      states.AddTable<FSServiceOrder>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.INeedAnyConfigField, BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IConfigured>) (state => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IConfigured) state.IsDisabled()));
      states.AddTable<FSSODet>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.INeedAnyConfigField, BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IConfigured>) (state => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IConfigured) state.IsDisabled()));
      states.AddTable<FSSODetSplit>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.INeedAnyConfigField, BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IConfigured>) (state => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IConfigured) state.IsDisabled()));
      states.AddTable<FSAddress>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.INeedAnyConfigField, BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IConfigured>) (state => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IConfigured) state.IsDisabled()));
      states.AddTable<FSContact>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.INeedAnyConfigField, BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IConfigured>) (state => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IConfigured) state.IsDisabled()));
      states.AddTable<FSSOEmployee>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.INeedAnyConfigField, BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IConfigured>) (state => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IConfigured) state.IsDisabled()));
      states.AddTable<FSSOResource>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.INeedAnyConfigField, BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IConfigured>) (state => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IConfigured) state.IsDisabled()));
      states.AddTable<FSServiceOrderTax>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.INeedAnyConfigField, BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IConfigured>) (state => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IConfigured) state.IsDisabled()));
      states.AddTable<FSServiceOrderTaxTran>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.INeedAnyConfigField, BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IConfigured>) (state => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IConfigured) state.IsDisabled()));
    }

    static BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase OnServiceOrderDeleted(
      BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventTarget handler)
    {
      return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<FSServiceOrder>) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<FSServiceOrder>) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedSubscriber<FSServiceOrder>) handler.WithTargetOf<FSServiceOrder>().OfEntityEvent<FSServiceOrder.Events>((Expression<Func<FSServiceOrder.Events, PXEntityEvent<FSServiceOrder>>>) (e => e.ServiceOrderDeleted))).Is((Expression<Func<FSServiceOrder, PXWorkflowEventHandler<FSServiceOrder, FSServiceOrder>>>) (g => g.OnServiceOrderDeleted))).UsesTargetAsPrimaryEntity()).DisplayName("Service Order Deleted");
    }

    static BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase OnServiceContractCleared(
      BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventTarget handler)
    {
      return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<FSServiceOrder>) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<FSServiceOrder>) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedSubscriber<FSServiceOrder>) handler.WithTargetOf<FSServiceOrder>().OfEntityEvent<FSServiceOrder.Events>((Expression<Func<FSServiceOrder.Events, PXEntityEvent<FSServiceOrder>>>) (e => e.ServiceContractCleared))).Is((Expression<Func<FSServiceOrder, PXWorkflowEventHandler<FSServiceOrder, FSServiceOrder>>>) (g => g.OnServiceContractCleared))).UsesTargetAsPrimaryEntity()).DisplayName("Service Contract Cleared");
    }

    static BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase OnServiceContractPeriodAssigned(
      BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventTarget handler)
    {
      return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<FSServiceOrder>) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<FSServiceOrder>) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedSubscriber<FSServiceOrder>) handler.WithTargetOf<FSServiceOrder>().OfEntityEvent<FSServiceOrder.Events>((Expression<Func<FSServiceOrder.Events, PXEntityEvent<FSServiceOrder>>>) (e => e.ServiceContractPeriodAssigned))).Is((Expression<Func<FSServiceOrder, PXWorkflowEventHandler<FSServiceOrder, FSServiceOrder>>>) (g => g.OnServiceContractPeriodAssigned))).UsesTargetAsPrimaryEntity()).DisplayName("Service Contract Period Assigned");
    }

    static BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase OnRequiredServiceContractPeriodCleared(
      BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventTarget handler)
    {
      return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<FSServiceOrder>) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<FSServiceOrder>) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedSubscriber<FSServiceOrder>) handler.WithTargetOf<FSServiceOrder>().OfEntityEvent<FSServiceOrder.Events>((Expression<Func<FSServiceOrder.Events, PXEntityEvent<FSServiceOrder>>>) (e => e.RequiredServiceContractPeriodCleared))).Is((Expression<Func<FSServiceOrder, PXWorkflowEventHandler<FSServiceOrder, FSServiceOrder>>>) (g => g.OnRequiredServiceContractPeriodCleared))).UsesTargetAsPrimaryEntity()).DisplayName("Required Service Contract Period Cleared");
    }

    static BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase OnLastAppointmentCompleted(
      BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventTarget handler)
    {
      return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<FSServiceOrder>) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<FSServiceOrder>) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedSubscriber<FSServiceOrder>) handler.WithTargetOf<FSServiceOrder>().OfEntityEvent<FSServiceOrder.Events>((Expression<Func<FSServiceOrder.Events, PXEntityEvent<FSServiceOrder>>>) (e => e.LastAppointmentCompleted))).Is((Expression<Func<FSServiceOrder, PXWorkflowEventHandler<FSServiceOrder, FSServiceOrder>>>) (g => g.OnLastAppointmentCompleted))).UsesTargetAsPrimaryEntity()).DisplayName("Last Appointment Completed");
    }

    static BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase OnLastAppointmentCanceled(
      BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventTarget handler)
    {
      return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<FSServiceOrder>) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<FSServiceOrder>) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedSubscriber<FSServiceOrder>) handler.WithTargetOf<FSServiceOrder>().OfEntityEvent<FSServiceOrder.Events>((Expression<Func<FSServiceOrder.Events, PXEntityEvent<FSServiceOrder>>>) (e => e.LastAppointmentCanceled))).Is((Expression<Func<FSServiceOrder, PXWorkflowEventHandler<FSServiceOrder, FSServiceOrder>>>) (g => g.OnLastAppointmentCanceled))).UsesTargetAsPrimaryEntity()).DisplayName("Last Appointment Canceled");
    }

    static BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase OnLastAppointmentClosed(
      BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventTarget handler)
    {
      return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<FSServiceOrder>) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<FSServiceOrder>) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedSubscriber<FSServiceOrder>) handler.WithTargetOf<FSServiceOrder>().OfEntityEvent<FSServiceOrder.Events>((Expression<Func<FSServiceOrder.Events, PXEntityEvent<FSServiceOrder>>>) (e => e.LastAppointmentClosed))).Is((Expression<Func<FSServiceOrder, PXWorkflowEventHandler<FSServiceOrder, FSServiceOrder>>>) (g => g.OnLastAppointmentClosed))).UsesTargetAsPrimaryEntity()).DisplayName("Last Appointment Closed");
    }

    static BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase OnAppointmentReOpened(
      BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventTarget handler)
    {
      return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<FSServiceOrder>) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<FSServiceOrder>) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedSubscriber<FSServiceOrder>) handler.WithTargetOf<FSServiceOrder>().OfEntityEvent<FSServiceOrder.Events>((Expression<Func<FSServiceOrder.Events, PXEntityEvent<FSServiceOrder>>>) (e => e.AppointmentReOpened))).Is((Expression<Func<FSServiceOrder, PXWorkflowEventHandler<FSServiceOrder, FSServiceOrder>>>) (g => g.OnAppointmentReOpened))).UsesTargetAsPrimaryEntity()).DisplayName("Appointment Reopened");
    }

    static BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase OnAppointmentUnclosed(
      BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventTarget handler)
    {
      return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<FSServiceOrder>) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<FSServiceOrder>) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedSubscriber<FSServiceOrder>) handler.WithTargetOf<FSServiceOrder>().OfEntityEvent<FSServiceOrder.Events>((Expression<Func<FSServiceOrder.Events, PXEntityEvent<FSServiceOrder>>>) (e => e.AppointmentUnclosed))).Is((Expression<Func<FSServiceOrder, PXWorkflowEventHandler<FSServiceOrder, FSServiceOrder>>>) (g => g.OnAppointmentUnclosed))).UsesTargetAsPrimaryEntity()).DisplayName("Appointment Unclosed");
    }

    static BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase OnAppointmentEdit(
      BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventTarget handler)
    {
      return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<FSServiceOrder>) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<FSServiceOrder>) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandlerDefinition.INeedSubscriber<FSServiceOrder>) handler.WithTargetOf<FSServiceOrder>().OfEntityEvent<FSServiceOrder.Events>((Expression<Func<FSServiceOrder.Events, PXEntityEvent<FSServiceOrder>>>) (e => e.AppointmentEdit))).Is((Expression<Func<FSServiceOrder, PXWorkflowEventHandler<FSServiceOrder, FSServiceOrder>>>) (g => g.OnAppointmentEdit))).UsesTargetAsPrimaryEntity()).DisplayName("Appointment Edit");
    }

    BoundedTo<ServiceOrderEntry, FSServiceOrder>.Condition Bql<T>() where T : IBqlUnary, new()
    {
      return context.Conditions.FromBql<T>();
    }

    BoundedTo<ServiceOrderEntry, FSServiceOrder>.Workflow.IConfigured QuoteServiceOrderFlow(
      BoundedTo<ServiceOrderEntry, FSServiceOrder>.Workflow.INeedStatesFlow flow)
    {
      return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IContainerFillerStates>) (flowStates =>
      {
        flowStates.Add("_", (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured>) (fs => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured) fs.IsInitial((Expression<Func<ServiceOrderEntry, PXAutoAction<FSServiceOrder>>>) (g => g.initializeState))));
        flowStates.Add<ListField.ServiceOrderStatus.open>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.putOnHold), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.confirmQuote), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) (a => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) null);
        }))).WithFieldStates(new Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IContainerFillerFields>(DisableScreenByQuote))));
        flowStates.Add<ListField.ServiceOrderStatus.hold>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.releaseFromHold), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) (a => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) null);
        }))));
        flowStates.Add<ListField.ServiceOrderStatus.canceled>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.reopenOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) (a => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()))))).WithFieldStates(new Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IContainerFillerFields>(DisableWholeScreen))));
        flowStates.Add<ListField.ServiceOrderStatus.confirmed>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.copyToServiceOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) (a => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.reopenOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) null);
        }))).WithFieldStates(new Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IContainerFillerFields>(DisableScreenByQuote))));
        flowStates.Add<ListField.ServiceOrderStatus.copied>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.copyToServiceOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) (a => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()))))).WithEventHandlers((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<ServiceOrderEntry, PXWorkflowEventHandler<FSServiceOrder>>>) (g => g.OnServiceOrderDeleted))))).WithFieldStates(new Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IContainerFillerFields>(DisableWholeScreen))));
      })).WithTransitions((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IContainerFillerTransitions>) (transitions =>
      {
        transitions.AddGroupFrom("_", (Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.hold>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.initializeState)).When((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) conditions.IsOnHold).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<FSServiceOrder.openDoc>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.quote>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
          }))));
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.open>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.initializeState)).When((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) conditions.IsNotOnHold).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<FSServiceOrder.openDoc>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
            fas.Add<FSServiceOrder.quote>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
          }))));
        }));
        transitions.AddGroupFrom<ListField.ServiceOrderStatus.open>((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.hold>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.putOnHold))));
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.confirmed>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.confirmQuote)).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<FSServiceOrder.skipExternalTaxCalculation>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.confirmed>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (e => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) e.SetFromValue((object) true)));
            fas.Add<FSServiceOrder.openDoc>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
          }))));
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.canceled>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.cancelOrder)).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<FSServiceOrder.skipExternalTaxCalculation>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.cancelActionRunning>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.openDoc>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.canceled>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
          }))));
        }));
        transitions.AddGroupFrom<ListField.ServiceOrderStatus.hold>((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.open>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.releaseFromHold))));
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.canceled>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.cancelOrder)).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<FSServiceOrder.skipExternalTaxCalculation>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.hold>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.canceled>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
          }))));
        }));
        transitions.AddGroupFrom<ListField.ServiceOrderStatus.canceled>((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.open>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.reopenOrder)).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
        {
          fas.Add<FSServiceOrder.skipExternalTaxCalculation>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
          fas.Add<FSServiceOrder.reopenActionRunning>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
          fas.Add<FSServiceOrder.openDoc>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
          fas.Add<FSServiceOrder.canceled>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (e => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) e.SetFromValue((object) false)));
        }))))));
        transitions.AddGroupFrom<ListField.ServiceOrderStatus.confirmed>((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.open>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.reopenOrder)).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<FSServiceOrder.skipExternalTaxCalculation>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.reopenActionRunning>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.openDoc>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
            fas.Add<FSServiceOrder.confirmed>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (e => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) e.SetFromValue((object) false)));
          }))));
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.copied>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.copyToServiceOrder))));
        }));
        transitions.AddGroupFrom<ListField.ServiceOrderStatus.copied>((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.ISourceContainerFillerTransitions>) (ts => ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.confirmed>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXWorkflowEventHandlerBase<FSServiceOrder>>>) (g => g.OnServiceOrderDeleted)).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<FSServiceOrder.copied>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (e => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) e.SetFromValue((object) false)))))))));
      }));
    }

    BoundedTo<ServiceOrderEntry, FSServiceOrder>.Workflow.IConfigured SimpleServiceOrderFlow(
      BoundedTo<ServiceOrderEntry, FSServiceOrder>.Workflow.INeedStatesFlow flow)
    {
      return (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IContainerFillerStates>) (flowStates =>
      {
        flowStates.Add("_", (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured>) (fs => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured) fs.IsInitial((Expression<Func<ServiceOrderEntry, PXAutoAction<FSServiceOrder>>>) (g => g.initializeState))));
        flowStates.Add<ListField.ServiceOrderStatus.open>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.putOnHold), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.completeOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) (a => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add<ServiceOrderEntry.ServiceOrderQuickProcess>((Expression<Func<ServiceOrderEntry.ServiceOrderQuickProcess, PXAction<FSServiceOrder>>>) (g => g.quickProcess), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) (a => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.allowBilling), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.scheduleAppointment), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) (a => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.invoiceOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) (a => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) null);
        }))).WithEventHandlers((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
        {
          handlers.Add((Expression<Func<ServiceOrderEntry, PXWorkflowEventHandler<FSServiceOrder>>>) (g => g.OnRequiredServiceContractPeriodCleared));
          handlers.Add((Expression<Func<ServiceOrderEntry, PXWorkflowEventHandler<FSServiceOrder>>>) (g => g.OnLastAppointmentCompleted));
          handlers.Add((Expression<Func<ServiceOrderEntry, PXWorkflowEventHandler<FSServiceOrder>>>) (g => g.OnLastAppointmentCanceled));
        }))));
        flowStates.Add<ListField.ServiceOrderStatus.hold>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured) flowState.WithActions((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.releaseFromHold), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) (a => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) null);
        }))));
        flowStates.Add<ListField.ServiceOrderStatus.awaiting>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.cancelOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.scheduleAppointment), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) null);
        }))).WithEventHandlers((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
        {
          handlers.Add((Expression<Func<ServiceOrderEntry, PXWorkflowEventHandler<FSServiceOrder>>>) (g => g.OnServiceContractCleared));
          handlers.Add((Expression<Func<ServiceOrderEntry, PXWorkflowEventHandler<FSServiceOrder>>>) (g => g.OnServiceContractPeriodAssigned));
        }))));
        flowStates.Add<ListField.ServiceOrderStatus.completed>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.closeOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) (a => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
          actions.Add<ServiceOrderEntry.ServiceOrderQuickProcess>((Expression<Func<ServiceOrderEntry.ServiceOrderQuickProcess, PXAction<FSServiceOrder>>>) (g => g.quickProcess), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) (a => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.allowBilling), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.invoiceOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.reopenOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) null);
        }))).WithEventHandlers((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers =>
        {
          handlers.Add((Expression<Func<ServiceOrderEntry, PXWorkflowEventHandler<FSServiceOrder>>>) (g => g.OnLastAppointmentClosed));
          handlers.Add((Expression<Func<ServiceOrderEntry, PXWorkflowEventHandler<FSServiceOrder>>>) (g => g.OnAppointmentReOpened));
          handlers.Add((Expression<Func<ServiceOrderEntry, PXWorkflowEventHandler<FSServiceOrder>>>) (g => g.OnAppointmentEdit));
        }))));
        flowStates.Add<ListField.ServiceOrderStatus.closed>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.uncloseOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) null);
          actions.Add<ServiceOrderEntry.ServiceOrderQuickProcess>((Expression<Func<ServiceOrderEntry.ServiceOrderQuickProcess, PXAction<FSServiceOrder>>>) (g => g.quickProcess), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) (a => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.invoiceOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) (a => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        }))).WithEventHandlers((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<ServiceOrderEntry, PXWorkflowEventHandler<FSServiceOrder>>>) (g => g.OnAppointmentUnclosed))))).WithFieldStates(new Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IContainerFillerFields>(DisableWholeScreen))));
        flowStates.Add<ListField.ServiceOrderStatus.canceled>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured>) (flowState => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.BaseFlowStep.IConfigured) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<ServiceOrderEntry, FSServiceOrder>.FlowState.INeedAnyFlowStateConfig) flowState.WithActions((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.reopenOrder), (Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IAllowOptionalConfig, BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured>) (a => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.ActionState.IConfigured) a.IsDuplicatedInToolbar()))))).WithEventHandlers((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<ServiceOrderEntry, PXWorkflowEventHandler<FSServiceOrder>>>) (g => g.OnAppointmentReOpened))))).WithFieldStates(new Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.FieldState.IContainerFillerFields>(DisableWholeScreen))));
      })).WithTransitions((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IContainerFillerTransitions>) (transitions =>
      {
        transitions.AddGroupFrom("_", (Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.hold>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.initializeState)).When((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) conditions.IsOnHold).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<FSServiceOrder.openDoc>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.open>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.initializeState)).When((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) conditions.IsNotOnHold).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<FSServiceOrder.openDoc>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        }));
        transitions.AddGroupFrom<ListField.ServiceOrderStatus.open>((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.hold>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.putOnHold))));
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.completed>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.completeOrder)).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<FSServiceOrder.skipExternalTaxCalculation>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.completeActionRunning>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.processCompleteAction>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.openDoc>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.completed>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
          }))));
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.completed>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXWorkflowEventHandlerBase<FSServiceOrder>>>) (g => g.OnLastAppointmentCompleted)).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<FSServiceOrder.skipExternalTaxCalculation>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.processCompleteAction>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
            fas.Add<FSServiceOrder.openDoc>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.completed>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
          }))));
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.canceled>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.cancelOrder)).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<FSServiceOrder.skipExternalTaxCalculation>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.cancelActionRunning>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.openDoc>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.canceled>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
          }))));
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.canceled>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXWorkflowEventHandlerBase<FSServiceOrder>>>) (g => g.OnLastAppointmentCanceled)).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<FSServiceOrder.processCancelAction>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
            fas.Add<FSServiceOrder.openDoc>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.canceled>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
          }))));
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.awaiting>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXWorkflowEventHandlerBase<FSServiceOrder>>>) (g => g.OnRequiredServiceContractPeriodCleared)).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<FSServiceOrder.awaiting>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        }));
        transitions.AddGroupFrom<ListField.ServiceOrderStatus.hold>((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.open>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.releaseFromHold))));
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.canceled>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.cancelOrder)).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<FSServiceOrder.skipExternalTaxCalculation>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.hold>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.canceled>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
          }))));
        }));
        transitions.AddGroupFrom<ListField.ServiceOrderStatus.awaiting>((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.canceled>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.cancelOrder)).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<FSServiceOrder.skipExternalTaxCalculation>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.awaiting>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.canceled>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
          }))));
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.open>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXWorkflowEventHandlerBase<FSServiceOrder>>>) (g => g.OnServiceContractCleared)).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<FSServiceOrder.openDoc>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
            fas.Add<FSServiceOrder.awaiting>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
          }))));
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.open>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXWorkflowEventHandlerBase<FSServiceOrder>>>) (g => g.OnServiceContractPeriodAssigned)).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<FSServiceOrder.openDoc>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
            fas.Add<FSServiceOrder.awaiting>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
          }))));
        }));
        transitions.AddGroupFrom<ListField.ServiceOrderStatus.completed>((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.open>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.reopenOrder)).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<FSServiceOrder.openDoc>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
            fas.Add<FSServiceOrder.completed>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (e => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) e.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.skipExternalTaxCalculation>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.reopenActionRunning>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
          }))));
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.open>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXWorkflowEventHandlerBase<FSServiceOrder>>>) (g => g.OnAppointmentEdit)).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<FSServiceOrder.openDoc>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
            fas.Add<FSServiceOrder.completed>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (e => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) e.SetFromValue((object) false)));
          }))));
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.open>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXWorkflowEventHandlerBase<FSServiceOrder>>>) (g => g.OnAppointmentReOpened)).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<FSServiceOrder.openDoc>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
            fas.Add<FSServiceOrder.completed>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (e => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) e.SetFromValue((object) false)));
          }))));
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.closed>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.closeOrder)).When((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) conditions.UserConfirmedClosing).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<FSServiceOrder.skipExternalTaxCalculation>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.closeActionRunning>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.closed>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
          }))));
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.closed>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXWorkflowEventHandlerBase<FSServiceOrder>>>) (g => g.OnLastAppointmentClosed)).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<FSServiceOrder.processCloseAction>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
            fas.Add<FSServiceOrder.closed>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
          }))));
        }));
        transitions.AddGroupFrom<ListField.ServiceOrderStatus.closed>((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.completed>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.uncloseOrder)).When((BoundedTo<ServiceOrderEntry, FSServiceOrder>.ISharedCondition) conditions.UserConfirmedUnclosing).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<FSServiceOrder.skipExternalTaxCalculation>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.unCloseActionRunning>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.closed>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
          }))));
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.completed>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXWorkflowEventHandlerBase<FSServiceOrder>>>) (g => g.OnAppointmentUnclosed)).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas => fas.Add<FSServiceOrder.closed>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
        }));
        transitions.AddGroupFrom<ListField.ServiceOrderStatus.canceled>((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.open>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXAction<FSServiceOrder>>>) (g => g.reopenOrder)).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<FSServiceOrder.openDoc>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
            fas.Add<FSServiceOrder.canceled>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (e => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) e.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.skipExternalTaxCalculation>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
            fas.Add<FSServiceOrder.reopenActionRunning>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) false)));
          }))));
          ts.Add((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.INeedTarget, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured>) (t => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Transition.IConfigured) t.To<ListField.ServiceOrderStatus.open>().IsTriggeredOn((Expression<Func<ServiceOrderEntry, PXWorkflowEventHandlerBase<FSServiceOrder>>>) (g => g.OnAppointmentReOpened)).WithFieldAssignments((Action<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IContainerFillerFields>) (fas =>
          {
            fas.Add<FSServiceOrder.openDoc>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (f => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) f.SetFromValue((object) true)));
            fas.Add<FSServiceOrder.canceled>((Func<BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.INeedRightOperand, BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured>) (e => (BoundedTo<ServiceOrderEntry, FSServiceOrder>.Assignment.IConfigured) e.SetFromValue((object) false)));
          }))));
        }));
      }));
    }
  }
}
