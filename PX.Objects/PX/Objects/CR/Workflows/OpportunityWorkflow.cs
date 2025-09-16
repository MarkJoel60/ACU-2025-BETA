// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Workflows.OpportunityWorkflow
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
namespace PX.Objects.CR.Workflows;

/// <summary>
/// Extensions that used to configure Workflow for <see cref="T:PX.Objects.CR.OpportunityMaint" /> and <see cref="T:PX.Objects.CR.CROpportunity" />.
/// Use Extensions Chaining for this extension if you want customize workflow with code for this graph of DAC.
/// </summary>
public class OpportunityWorkflow : PXGraphExtension<OpportunityMaint>
{
  private static readonly string[] NewReasons = new string[3]
  {
    "CR",
    "FL",
    "QL"
  };
  private static readonly string[] OpenReasons = new string[2]
  {
    "IP",
    "QL"
  };
  private static readonly string[] WonReasons = new string[5]
  {
    "OP",
    "PR",
    "RL",
    "TH",
    "OT"
  };
  private static readonly string[] LostReasons = new string[5]
  {
    "CM",
    "PR",
    "RL",
    "TH",
    "OT"
  };
  private const string ReasonFormField = "Reason";
  private const string StageFormField = "Stage";

  public static bool IsActive() => false;

  public virtual void Configure(PXScreenConfiguration config)
  {
    OpportunityWorkflow.Configure(config.GetScreenConfigurationContext<OpportunityMaint, CROpportunity>());
  }

  protected static void Configure(
    WorkflowContext<OpportunityMaint, CROpportunity> context)
  {
    var conditions = new
    {
      IsInNewState = context.Conditions.FromBql<BqlOperand<CROpportunity.status, IBqlString>.IsEqual<OpportunityStatus.@new>>(),
      IsNotInNewState = context.Conditions.FromBql<BqlOperand<CROpportunity.status, IBqlString>.IsNotEqual<OpportunityStatus.@new>>(),
      BAccountIDIsNull = context.Conditions.FromBql<BqlOperand<CROpportunity.bAccountID, IBqlInt>.IsNull>()
    }.AutoNameConditions();
    BoundedTo<OpportunityMaint, CROpportunity>.Form.IConfigured formOpen = CreateForm("FormOpen", OpportunityWorkflow.OpenReasons, "QL");
    BoundedTo<OpportunityMaint, CROpportunity>.Form.IConfigured formWon = CreateForm("FormWon", OpportunityWorkflow.WonReasons);
    BoundedTo<OpportunityMaint, CROpportunity>.Form.IConfigured formLost = CreateForm("FormLost", OpportunityWorkflow.LostReasons);
    BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured categoryProcessing = context.Categories.CreateNew("Processing", (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured>) (category => (BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured categoryRecordCreation = context.Categories.CreateNew("RecordCreation", (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured>) (category => (BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured) category.DisplayName("Record Creation")));
    BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured categoryServices = context.Categories.CreateNew("Services", (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured>) (category => (BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured) category.DisplayName("Services")));
    BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured categoryActivities = context.Categories.CreateNew("Activities", (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured>) (category => (BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured) category.DisplayName("Activities")));
    BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured categoryValidation = context.Categories.CreateNew("Validation", (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured>) (category => (BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured) category.DisplayName("Validation")));
    BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured categoryOther = context.Categories.CreateNew("Other", (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured>) (category => (BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IConfigured) category.DisplayName("Other")));
    BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured actionCreateTask = context.ActionDefinitions.CreateExisting("NewTask", (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured>) (a => (BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured) a.WithCategory(categoryActivities)));
    BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured actionCreateNote = context.ActionDefinitions.CreateExisting("NewActivityN_Workflow", (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured>) (a => (BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured) a.WithCategory(categoryActivities)));
    context.AddScreenConfigurationFor((Func<BoundedTo<OpportunityMaint, CROpportunity>.ScreenConfiguration.IStartConfigScreen, BoundedTo<OpportunityMaint, CROpportunity>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<OpportunityMaint, CROpportunity>.ScreenConfiguration.IConfigured) ((BoundedTo<OpportunityMaint, CROpportunity>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<CROpportunity.status>().AddDefaultFlow(new Func<BoundedTo<OpportunityMaint, CROpportunity>.Workflow.INeedStatesFlow, BoundedTo<OpportunityMaint, CROpportunity>.Workflow.IConfigured>(DefaultOpportunityFlow)).WithActions((Action<BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.Open), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured>) (c => (BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured) c.WithFieldAssignments((Action<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IContainerFillerFields>) (fields =>
      {
        fields.Add<CROpportunity.resolution>((Func<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.INeedRightOperand, BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured>) (f => (BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured) f.SetFromFormField(formOpen, "Reason")));
        fields.Add<CROpportunity.stageID>((Func<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.INeedRightOperand, BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured>) (f => (BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured) f.SetFromFormField(formOpen, "Stage")));
        fields.Add<CROpportunity.isActive>((Func<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.INeedRightOperand, BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured>) (f => (BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured) f.SetFromValue((object) true)));
        fields.Add<CROpportunity.closingDate>((Func<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.INeedRightOperand, BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured>) (f => (BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured) f.SetFromValue((object) null)));
      })).WithForm(formOpen).IsHiddenWhen((BoundedTo<OpportunityMaint, CROpportunity>.ISharedCondition) conditions.IsInNewState).WithPersistOptions((ActionPersistOptions) 2).WithCategory(categoryProcessing).IsExposedToMobile(true).MassProcessingScreen<UpdateOpportunityMassProcess>()));
      actions.Add((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.OpenFromNew), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured>) (c => (BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured) c.WithFieldAssignments((Action<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IContainerFillerFields>) (fields =>
      {
        fields.Add<CROpportunity.resolution>((Func<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.INeedRightOperand, BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured>) (f => (BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured) f.SetFromFormField(formOpen, "Reason")));
        fields.Add<CROpportunity.stageID>((Func<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.INeedRightOperand, BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured>) (f => (BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured) f.SetFromFormField(formOpen, "Stage")));
        fields.Add<CROpportunity.isActive>((Func<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.INeedRightOperand, BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured>) (f => (BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured) f.SetFromValue((object) true)));
        fields.Add<CROpportunity.closingDate>((Func<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.INeedRightOperand, BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured>) (f => (BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured) f.SetFromValue((object) null)));
      })).WithForm(formOpen).IsHiddenWhen((BoundedTo<OpportunityMaint, CROpportunity>.ISharedCondition) conditions.IsNotInNewState).WithPersistOptions((ActionPersistOptions) 2).WithCategory(categoryProcessing).IsExposedToMobile(true).MassProcessingScreen<UpdateOpportunityMassProcess>()));
      actions.Add((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.CloseAsWon), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured>) (c => (BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured) c.WithFieldAssignments((Action<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IContainerFillerFields>) (fields =>
      {
        fields.Add<CROpportunity.resolution>((Func<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.INeedRightOperand, BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured>) (f => (BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured) f.SetFromFormField(formWon, "Reason")));
        fields.Add<CROpportunity.stageID>((Func<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.INeedRightOperand, BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured>) (f => (BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured) f.SetFromFormField(formWon, "Stage")));
        fields.Add<CROpportunity.isActive>((Func<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.INeedRightOperand, BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured>) (f => (BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fields.Add<CROpportunity.closingDate>((Func<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.INeedRightOperand, BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured>) (f => (BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured) f.SetFromToday()));
        fields.Add<CROpportunity.allowOverrideContactAddress>((Func<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.INeedRightOperand, BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured>) (f => (BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured) f.SetFromValue((object) true)));
        fields.Add<CROpportunity.allowOverrideShippingContactAddress>((Func<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.INeedRightOperand, BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured>) (f => (BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured) f.SetFromValue((object) true)));
        fields.Add<CROpportunity.allowOverrideBillingContactAddress>((Func<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.INeedRightOperand, BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured>) (f => (BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured) f.SetFromValue((object) true)));
      })).WithForm(formWon).WithPersistOptions((ActionPersistOptions) 2).WithCategory(categoryProcessing).IsDisabledWhen((BoundedTo<OpportunityMaint, CROpportunity>.ISharedCondition) conditions.BAccountIDIsNull).IsExposedToMobile(true).MassProcessingScreen<UpdateOpportunityMassProcess>()));
      actions.Add((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.CloseAsLost), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured>) (c => (BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured) c.WithFieldAssignments((Action<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IContainerFillerFields>) (fields =>
      {
        fields.Add<CROpportunity.resolution>((Func<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.INeedRightOperand, BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured>) (f => (BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured) f.SetFromFormField(formLost, "Reason")));
        fields.Add<CROpportunity.stageID>((Func<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.INeedRightOperand, BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured>) (f => (BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured) f.SetFromFormField(formLost, "Stage")));
        fields.Add<CROpportunity.isActive>((Func<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.INeedRightOperand, BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured>) (f => (BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured) f.SetFromValue((object) false)));
        fields.Add<CROpportunity.closingDate>((Func<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.INeedRightOperand, BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured>) (f => (BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured) f.SetFromToday()));
        fields.Add<CROpportunity.allowOverrideContactAddress>((Func<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.INeedRightOperand, BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured>) (f => (BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured) f.SetFromValue((object) true)));
        fields.Add<CROpportunity.allowOverrideShippingContactAddress>((Func<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.INeedRightOperand, BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured>) (f => (BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured) f.SetFromValue((object) true)));
        fields.Add<CROpportunity.allowOverrideBillingContactAddress>((Func<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.INeedRightOperand, BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured>) (f => (BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IConfigured) f.SetFromValue((object) true)));
      })).WithForm(formLost).WithPersistOptions((ActionPersistOptions) 2).WithCategory(categoryProcessing).IsExposedToMobile(true).MassProcessingScreen<UpdateOpportunityMassProcess>()));
      actions.Add((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.createQuote), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured>) (c => (BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured) c.WithCategory(categoryRecordCreation)));
      actions.Add<OpportunityMaint.CRCreateSalesOrderExt>((Expression<Func<OpportunityMaint.CRCreateSalesOrderExt, PXAction<CROpportunity>>>) (g => g.CreateSalesOrder), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured>) (c => (BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured) c.WithCategory(categoryRecordCreation).WithPersistOptions((ActionPersistOptions) 1)));
      actions.Add<OpportunityMaint.CreateBothAccountAndContactFromOpportunityGraphExt>((Expression<Func<OpportunityMaint.CreateBothAccountAndContactFromOpportunityGraphExt, PXAction<CROpportunity>>>) (e => e.CreateBothContactAndAccount), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured>) (c => (BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured) c.WithCategory(categoryRecordCreation).WithPersistOptions((ActionPersistOptions) 2)));
      actions.Add<OpportunityMaint.CreateContactFromOpportunityGraphExt>((Expression<Func<OpportunityMaint.CreateContactFromOpportunityGraphExt, PXAction<CROpportunity>>>) (e => e.CreateContact), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured>) (c => (BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured) c.WithCategory(categoryRecordCreation).WithPersistOptions((ActionPersistOptions) 2)));
      actions.Add<OpportunityMaint.CRCreateInvoiceExt>((Expression<Func<OpportunityMaint.CRCreateInvoiceExt, PXAction<CROpportunity>>>) (g => g.CreateInvoice), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured>) (c => (BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured) c.WithCategory(categoryRecordCreation).WithPersistOptions((ActionPersistOptions) 2)));
      actions.Add(actionCreateTask);
      actions.Add(actionCreateNote);
      actions.Add<OpportunityMaint.Discount>((Expression<Func<OpportunityMaint.Discount, PXAction<CROpportunity>>>) (e => e.recalculatePrices), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured>) (c => (BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured) c.WithCategory(categoryOther).WithPersistOptions((ActionPersistOptions) 2)));
      actions.Add((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.validateAddresses), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured>) (c => (BoundedTo<OpportunityMaint, CROpportunity>.ActionDefinition.IConfigured) c.WithCategory(categoryOther).WithPersistOptions((ActionPersistOptions) 2)));
    })).WithForms((Action<BoundedTo<OpportunityMaint, CROpportunity>.Form.IContainerForms>) (forms =>
    {
      forms.Add(formOpen);
      forms.Add(formWon);
      forms.Add(formLost);
    })).WithHandlers((Action<BoundedTo<OpportunityMaint, CROpportunity>.WorkflowEventHandlerDefinition.IContainerFillerHandlers>) (handlers => handlers.Add((Func<BoundedTo<OpportunityMaint, CROpportunity>.WorkflowEventHandlerDefinition.INeedEventTarget, BoundedTo<OpportunityMaint, CROpportunity>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase>) (handler => (BoundedTo<OpportunityMaint, CROpportunity>.WorkflowEventHandlerDefinition.IHandlerConfiguredBase) ((BoundedTo<OpportunityMaint, CROpportunity>.WorkflowEventHandlerDefinition.IAllowOptionalConfigAction<CROpportunity>) ((BoundedTo<OpportunityMaint, CROpportunity>.WorkflowEventHandlerDefinition.INeedEventPrimaryEntityGetter<CROpportunity>) ((BoundedTo<OpportunityMaint, CROpportunity>.WorkflowEventHandlerDefinition.INeedSubscriber<CROpportunity>) handler.WithTargetOf<CROpportunity>().OfEntityEvent<CROpportunity.Events>((Expression<Func<CROpportunity.Events, PXEntityEvent<CROpportunity>>>) (e => e.OpportunityCreatedFromLead))).Is((Expression<Func<CROpportunity, PXWorkflowEventHandler<CROpportunity, CROpportunity>>>) (g => g.OnOpportunityCreatedFromLead))).UsesTargetAsPrimaryEntity()).DisplayName("Opportunity Created from Lead"))))).WithCategories((Action<BoundedTo<OpportunityMaint, CROpportunity>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(categoryProcessing);
      categories.Add(categoryRecordCreation);
      categories.Add(categoryActivities);
      categories.Add(categoryServices);
      categories.Add(categoryValidation);
      categories.Add(categoryOther);
    }))));

    static void DisableFieldsForFinalStates(
      BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IContainerFillerFields fields)
    {
      fields.AddTable<CROpportunity>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured>) (field => (BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured) field.IsDisabled()));
      fields.AddTable<CROpportunityProducts>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured>) (field => (BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured) field.IsDisabled()));
      fields.AddTable<CRTaxTran>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured>) (field => (BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured) field.IsDisabled()));
      fields.AddTable<CROpportunityDiscountDetail>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured>) (field => (BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured) field.IsDisabled()));
      fields.AddTable<CRContact>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured>) (field => (BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured) field.IsDisabled()));
      fields.AddTable<CRAddress>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured>) (field => (BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured) field.IsDisabled()));
      fields.AddTable<CROpportunityTax>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured>) (field => (BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured) field.IsDisabled()));
      fields.AddTable<CRShippingContact>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured>) (field => (BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured) field.IsDisabled()));
      fields.AddTable<CRShippingAddress>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured>) (field => (BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured) field.IsDisabled()));
      fields.AddTable<CRBillingContact>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured>) (field => (BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured) field.IsDisabled()));
      fields.AddTable<CRBillingAddress>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured>) (field => (BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured) field.IsDisabled()));
      fields.AddField<CROpportunity.opportunityID>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured>) null);
      fields.AddField<CROpportunity.subject>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured>) null);
      fields.AddField<CROpportunity.details>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured>) null);
    }

    BoundedTo<OpportunityMaint, CROpportunity>.Workflow.IConfigured DefaultOpportunityFlow(
      BoundedTo<OpportunityMaint, CROpportunity>.Workflow.INeedStatesFlow flow)
    {
      return (BoundedTo<OpportunityMaint, CROpportunity>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<OpportunityMaint, CROpportunity>.BaseFlowStep.IContainerFillerStates>) (states =>
      {
        states.Add(context.FlowStates.Create<OpportunityStatus.@new>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FlowState.INeedAnyFlowStateConfig, BoundedTo<OpportunityMaint, CROpportunity>.BaseFlowStep.IConfigured>) (state => (BoundedTo<OpportunityMaint, CROpportunity>.BaseFlowStep.IConfigured) ((BoundedTo<OpportunityMaint, CROpportunity>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<OpportunityMaint, CROpportunity>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<OpportunityMaint, CROpportunity>.FlowState.INeedAnyFlowStateConfig) state.IsInitial()).WithFieldStates((Action<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IContainerFillerFields>) (fields =>
        {
          fields.AddField<CROpportunity.resolution>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured>) (field => (BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured) ((BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField) field.DefaultValue((object) "CR")).ComboBoxValues(OpportunityWorkflow.NewReasons)));
          fields.AddField<CROpportunity.isActive>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured>) (field => (BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured) field.IsDisabled()));
          fields.AddField<CROpportunity.source>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured>) (field => (BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured) field.ComboBoxValues(CRMSourcesAttribute.Values)));
        }))).WithActions((Action<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.createQuote), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) (a => (BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add<OpportunityMaint.CRCreateSalesOrderExt>((Expression<Func<OpportunityMaint.CRCreateSalesOrderExt, PXAction<CROpportunity>>>) (g => g.CreateSalesOrder), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
          actions.Add<OpportunityMaint.CRCreateInvoiceExt>((Expression<Func<OpportunityMaint.CRCreateInvoiceExt, PXAction<CROpportunity>>>) (g => g.CreateInvoice), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
          actions.Add<OpportunityMaint.CreateContactFromOpportunityGraphExt>((Expression<Func<OpportunityMaint.CreateContactFromOpportunityGraphExt, PXAction<CROpportunity>>>) (e => e.CreateContact), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
          actions.Add<OpportunityMaint.CreateBothAccountAndContactFromOpportunityGraphExt>((Expression<Func<OpportunityMaint.CreateBothAccountAndContactFromOpportunityGraphExt, PXAction<CROpportunity>>>) (e => e.CreateBothContactAndAccount), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.validateAddresses), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
          actions.Add<OpportunityMaint.Discount>((Expression<Func<OpportunityMaint.Discount, PXAction<CROpportunity>>>) (e => e.recalculatePrices), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.OpenFromNew), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) (a => (BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured) a.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
          actions.Add((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.CloseAsWon), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.CloseAsLost), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
        }))).WithEventHandlers((Action<BoundedTo<OpportunityMaint, CROpportunity>.WorkflowEventHandler.IContainerFillerHandlers>) (handlers => handlers.Add((Expression<Func<OpportunityMaint, PXWorkflowEventHandler<CROpportunity>>>) (g => g.OnOpportunityCreatedFromLead)))))));
        states.Add(context.FlowStates.Create<OpportunityStatus.open>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FlowState.INeedAnyFlowStateConfig, BoundedTo<OpportunityMaint, CROpportunity>.BaseFlowStep.IConfigured>) (state => (BoundedTo<OpportunityMaint, CROpportunity>.BaseFlowStep.IConfigured) ((BoundedTo<OpportunityMaint, CROpportunity>.FlowState.INeedAnyFlowStateConfig) state.WithFieldStates((Action<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IContainerFillerFields>) (fields =>
        {
          fields.AddField<CROpportunity.resolution>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured>) (field => (BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured) ((BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField) field.DefaultValue((object) "QL")).ComboBoxValues(OpportunityWorkflow.OpenReasons)));
          fields.AddField<CROpportunity.isActive>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured>) (field => (BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured) field.IsDisabled()));
          fields.AddField<CROpportunity.source>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured>) (field => (BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured) field.ComboBoxValues(CRMSourcesAttribute.Values)));
        }))).WithActions((Action<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.createQuote), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) (a => (BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
          actions.Add<OpportunityMaint.CRCreateSalesOrderExt>((Expression<Func<OpportunityMaint.CRCreateSalesOrderExt, PXAction<CROpportunity>>>) (g => g.CreateSalesOrder), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
          actions.Add<OpportunityMaint.CRCreateInvoiceExt>((Expression<Func<OpportunityMaint.CRCreateInvoiceExt, PXAction<CROpportunity>>>) (g => g.CreateInvoice), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
          actions.Add<OpportunityMaint.CreateContactFromOpportunityGraphExt>((Expression<Func<OpportunityMaint.CreateContactFromOpportunityGraphExt, PXAction<CROpportunity>>>) (e => e.CreateContact), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
          actions.Add<OpportunityMaint.CreateBothAccountAndContactFromOpportunityGraphExt>((Expression<Func<OpportunityMaint.CreateBothAccountAndContactFromOpportunityGraphExt, PXAction<CROpportunity>>>) (e => e.CreateBothContactAndAccount), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.validateAddresses), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
          actions.Add<OpportunityMaint.Discount>((Expression<Func<OpportunityMaint.Discount, PXAction<CROpportunity>>>) (e => e.recalculatePrices), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.CloseAsWon), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) (g => (BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured) g.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
          actions.Add((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.CloseAsLost), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
        })))));
        states.Add(context.FlowStates.Create<OpportunityStatus.won>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FlowState.INeedAnyFlowStateConfig, BoundedTo<OpportunityMaint, CROpportunity>.BaseFlowStep.IConfigured>) (state => (BoundedTo<OpportunityMaint, CROpportunity>.BaseFlowStep.IConfigured) ((BoundedTo<OpportunityMaint, CROpportunity>.FlowState.INeedAnyFlowStateConfig) state.WithFieldStates((Action<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IContainerFillerFields>) (fields =>
        {
          fields.AddField<CROpportunity.resolution>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured>) (field => (BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured) ((BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField) field.ComboBoxValues(OpportunityWorkflow.WonReasons)).IsDisabled()));
          DisableFieldsForFinalStates(fields);
        }))).WithActions((Action<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.createQuote), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
          actions.Add<OpportunityMaint.CRCreateSalesOrderExt>((Expression<Func<OpportunityMaint.CRCreateSalesOrderExt, PXAction<CROpportunity>>>) (g => g.CreateSalesOrder), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
          actions.Add<OpportunityMaint.CRCreateInvoiceExt>((Expression<Func<OpportunityMaint.CRCreateInvoiceExt, PXAction<CROpportunity>>>) (g => g.CreateInvoice), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.Open), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) (g => (BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured) g.IsDuplicatedInToolbar()));
        })))));
        states.Add(context.FlowStates.Create<OpportunityStatus.lost>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FlowState.INeedAnyFlowStateConfig, BoundedTo<OpportunityMaint, CROpportunity>.BaseFlowStep.IConfigured>) (state => (BoundedTo<OpportunityMaint, CROpportunity>.BaseFlowStep.IConfigured) ((BoundedTo<OpportunityMaint, CROpportunity>.FlowState.INeedAnyFlowStateConfig) state.WithFieldStates((Action<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IContainerFillerFields>) (fields =>
        {
          fields.AddField<CROpportunity.resolution>((Func<BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured>) (field => (BoundedTo<OpportunityMaint, CROpportunity>.FieldState.IConfigured) ((BoundedTo<OpportunityMaint, CROpportunity>.FieldState.INeedAnyConfigField) field.ComboBoxValues(OpportunityWorkflow.LostReasons)).IsDisabled()));
          DisableFieldsForFinalStates(fields);
        }))).WithActions((Action<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IContainerFillerActions>) (actions =>
        {
          actions.Add((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.createQuote), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) null);
          actions.Add((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.Open), (Func<BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IAllowOptionalConfig, BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured>) (g => (BoundedTo<OpportunityMaint, CROpportunity>.ActionState.IConfigured) g.IsDuplicatedInToolbar()));
        })))));
      })).WithTransitions((Action<BoundedTo<OpportunityMaint, CROpportunity>.Transition.IContainerFillerTransitions>) (transitions =>
      {
        transitions.AddGroupFrom<OpportunityStatus.@new>((Action<BoundedTo<OpportunityMaint, CROpportunity>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<OpportunityMaint, CROpportunity>.Transition.INeedTarget, BoundedTo<OpportunityMaint, CROpportunity>.Transition.IConfigured>) (t => (BoundedTo<OpportunityMaint, CROpportunity>.Transition.IConfigured) t.To<OpportunityStatus.open>().IsTriggeredOn((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.OpenFromNew))));
          ts.Add((Func<BoundedTo<OpportunityMaint, CROpportunity>.Transition.INeedTarget, BoundedTo<OpportunityMaint, CROpportunity>.Transition.IConfigured>) (t => (BoundedTo<OpportunityMaint, CROpportunity>.Transition.IConfigured) t.To<OpportunityStatus.won>().IsTriggeredOn((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.CloseAsWon))));
          ts.Add((Func<BoundedTo<OpportunityMaint, CROpportunity>.Transition.INeedTarget, BoundedTo<OpportunityMaint, CROpportunity>.Transition.IConfigured>) (t => (BoundedTo<OpportunityMaint, CROpportunity>.Transition.IConfigured) t.To<OpportunityStatus.lost>().IsTriggeredOn((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.CloseAsLost))));
          ts.Add((Func<BoundedTo<OpportunityMaint, CROpportunity>.Transition.INeedTarget, BoundedTo<OpportunityMaint, CROpportunity>.Transition.IConfigured>) (t => (BoundedTo<OpportunityMaint, CROpportunity>.Transition.IConfigured) t.To<OpportunityStatus.@new>().IsTriggeredOn((Expression<Func<OpportunityMaint, PXWorkflowEventHandlerBase<CROpportunity>>>) (g => g.OnOpportunityCreatedFromLead)).WithFieldAssignments((Action<BoundedTo<OpportunityMaint, CROpportunity>.Assignment.IContainerFillerFields>) (f => f.Add<CROpportunity.resolution>("FL")))));
        }));
        transitions.AddGroupFrom<OpportunityStatus.open>((Action<BoundedTo<OpportunityMaint, CROpportunity>.Transition.ISourceContainerFillerTransitions>) (ts =>
        {
          ts.Add((Func<BoundedTo<OpportunityMaint, CROpportunity>.Transition.INeedTarget, BoundedTo<OpportunityMaint, CROpportunity>.Transition.IConfigured>) (t => (BoundedTo<OpportunityMaint, CROpportunity>.Transition.IConfigured) t.To<OpportunityStatus.won>().IsTriggeredOn((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.CloseAsWon))));
          ts.Add((Func<BoundedTo<OpportunityMaint, CROpportunity>.Transition.INeedTarget, BoundedTo<OpportunityMaint, CROpportunity>.Transition.IConfigured>) (t => (BoundedTo<OpportunityMaint, CROpportunity>.Transition.IConfigured) t.To<OpportunityStatus.lost>().IsTriggeredOn((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.CloseAsLost))));
        }));
        transitions.Add((Func<BoundedTo<OpportunityMaint, CROpportunity>.Transition.INeedSource, BoundedTo<OpportunityMaint, CROpportunity>.Transition.IConfigured>) (ts => (BoundedTo<OpportunityMaint, CROpportunity>.Transition.IConfigured) ts.From<OpportunityStatus.won>().To<OpportunityStatus.open>().IsTriggeredOn((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.Open))));
        transitions.Add((Func<BoundedTo<OpportunityMaint, CROpportunity>.Transition.INeedSource, BoundedTo<OpportunityMaint, CROpportunity>.Transition.IConfigured>) (ts => (BoundedTo<OpportunityMaint, CROpportunity>.Transition.IConfigured) ts.From<OpportunityStatus.lost>().To<OpportunityStatus.open>().IsTriggeredOn((Expression<Func<OpportunityMaint, PXAction<CROpportunity>>>) (g => g.Open))));
      }));
    }

    BoundedTo<OpportunityMaint, CROpportunity>.Form.IConfigured CreateForm(
      string formID,
      string[] valueCollection,
      string defaultValue = null,
      Action<BoundedTo<OpportunityMaint, CROpportunity>.FormField.IContainerFillerFields> fieldsExt = null)
    {
      return context.Forms.Create(formID, (Func<BoundedTo<OpportunityMaint, CROpportunity>.Form.INeedAnyConfigForm, BoundedTo<OpportunityMaint, CROpportunity>.Form.IConfigured>) (form => (BoundedTo<OpportunityMaint, CROpportunity>.Form.IConfigured) ((BoundedTo<OpportunityMaint, CROpportunity>.Form.INeedAnyConfigForm) form.Prompt("Details")).WithFields((Action<BoundedTo<OpportunityMaint, CROpportunity>.FormField.IContainerFillerFields>) (fields =>
      {
        fields.Add("Reason", (Func<BoundedTo<OpportunityMaint, CROpportunity>.FormField.INeedTypeConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FormField.IConfigured>) (field =>
        {
          BoundedTo<OpportunityMaint, CROpportunity>.FormField.IAllowOptionalConfig iallowOptionalConfig = field.WithSchemaOf<CROpportunity.resolution>().IsRequired().Prompt("Reason").OnlyComboBoxValues(valueCollection);
          return defaultValue != null ? (BoundedTo<OpportunityMaint, CROpportunity>.FormField.IConfigured) iallowOptionalConfig.DefaultValue((object) defaultValue) : (BoundedTo<OpportunityMaint, CROpportunity>.FormField.IConfigured) iallowOptionalConfig;
        }));
        fields.Add("Stage", (Func<BoundedTo<OpportunityMaint, CROpportunity>.FormField.INeedTypeConfigField, BoundedTo<OpportunityMaint, CROpportunity>.FormField.IConfigured>) (field => (BoundedTo<OpportunityMaint, CROpportunity>.FormField.IConfigured) field.WithSchemaOf<CROpportunity.stageID>().DefaultValueFromSchemaField().IsRequired().Prompt("Stage")));
        Action<BoundedTo<OpportunityMaint, CROpportunity>.FormField.IContainerFillerFields> action = fieldsExt;
        if (action == null)
          return;
        action(fields);
      }))));
    }
  }

  /// <summary>
  /// Statuses for <see cref="T:PX.Objects.CR.CROpportunity.status" /> used by default in system workflow.
  /// Values could be changed and extended by workflow.
  /// Note, that <see cref="F:PX.Objects.CR.OpportunityStatus.Won" /> status used in Campaigns screen to count won opportunities: <see cref="T:PX.Objects.CR.DAC.Standalone.CRCampaign.closedOpportunities" />.
  /// </summary>
  [Obsolete("Use OpportunityStatus")]
  public class States : OpportunityStatus
  {
  }

  public static class CategoryNames
  {
    public const string Processing = "Processing";
    public const string RecordCreation = "RecordCreation";
    public const string Services = "Services";
    public const string Activities = "Activities";
    public const string Validation = "Validation";
    public const string Other = "Other";
  }

  public static class CategoryDisplayNames
  {
    public const string Processing = "Processing";
    public const string RecordCreation = "Record Creation";
    public const string Services = "Services";
    public const string Activities = "Activities";
    public const string Validation = "Validation";
    public const string Other = "Other";
  }
}
