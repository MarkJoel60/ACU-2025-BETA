// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Workflows.LeadWorkflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.CR.Workflows;

/// <summary>
/// Extensions that used to configure Workflow for <see cref="T:PX.Objects.CR.LeadMaint" /> and <see cref="T:PX.Objects.CR.CRLead" />.
/// Use Extensions Chaining for this extension if you want customize workflow with code for this graph of DAC.
/// </summary>
public class LeadWorkflow : PXGraphExtension<
#nullable disable
LeadMaint>
{
  private const string _fieldReason = "Reason";
  private const string _formOpen = "FormOpen";
  private const string _formQualify = "FormQualify";
  private const string _formAccept = "FormAccept";
  private const string _formDisqualify = "FormDisqualify";
  private const string _formConvert = "FormConvert";
  private const string _actionOpen = "Open";
  private const string _actionQualify = "Qualify";
  private const string _actionAccept = "Accept";
  private const string _actionDisqualify = "Disqualify";
  private const string _actionMarkAsConverted = "MarkAsConverted";
  private const string _reasonCreated = "CR";
  private const string _reasonPotentialInterest = "PI";
  private const string _reasonSubscribed = "SB";
  private const string _reasonInquiry = "IQ";
  private const string _reasonQualifiedByMarketing = "QM";
  private const string _reasonAcceptedBySales = "AS";
  private const string _reasonQualifiedBySales = "QS";
  private const string _reasonNoInterest = "NI";
  private const string _reasonDuplicate = "DL";
  private const string _reasonUnableToContact = "CL";
  private const string _reasonOther = "OT";

  public static bool IsActive() => false;

  public virtual void Configure(PXScreenConfiguration configuration)
  {
    LeadWorkflow.Configure(configuration.GetScreenConfigurationContext<LeadMaint, CRLead>());
  }

  protected static void Configure(WorkflowContext<LeadMaint, CRLead> context)
  {
    Dictionary<string, (string, string[])> reasons = new Dictionary<string, (string, string[])>(6)
    {
      ["H"] = ("CR", new string[1]{ "CR" }),
      ["O"] = ("PI", new string[3]{ "PI", "SB", "IQ" }),
      ["Q"] = ("QM", new string[1]{ "QM" }),
      ["A"] = ("AS", new string[2]{ "AS", "QM" }),
      ["C"] = ("QS", new string[3]{ "QS", "AS", "QM" }),
      ["L"] = ((string) null, new string[4]
      {
        "NI",
        "CL",
        "DL",
        "OT"
      })
    };
    BoundedTo<LeadMaint, CRLead>.Form.IConfigured formOpen = context.Forms.Create("FormOpen", (Func<BoundedTo<LeadMaint, CRLead>.Form.INeedAnyConfigForm, BoundedTo<LeadMaint, CRLead>.Form.IConfigured>) (form => (BoundedTo<LeadMaint, CRLead>.Form.IConfigured) ((BoundedTo<LeadMaint, CRLead>.Form.INeedAnyConfigForm) form.Prompt("Details")).WithFields((Action<BoundedTo<LeadMaint, CRLead>.FormField.IContainerFillerFields>) (fields => AddResolutionFormField(fields, reasons["O"])))));
    BoundedTo<LeadMaint, CRLead>.Form.IConfigured formQualify = context.Forms.Create("FormQualify", (Func<BoundedTo<LeadMaint, CRLead>.Form.INeedAnyConfigForm, BoundedTo<LeadMaint, CRLead>.Form.IConfigured>) (form => (BoundedTo<LeadMaint, CRLead>.Form.IConfigured) ((BoundedTo<LeadMaint, CRLead>.Form.INeedAnyConfigForm) form.Prompt("Details")).WithFields((Action<BoundedTo<LeadMaint, CRLead>.FormField.IContainerFillerFields>) (fields => AddResolutionFormField(fields, reasons["Q"])))));
    BoundedTo<LeadMaint, CRLead>.Form.IConfigured formAccept = context.Forms.Create("FormAccept", (Func<BoundedTo<LeadMaint, CRLead>.Form.INeedAnyConfigForm, BoundedTo<LeadMaint, CRLead>.Form.IConfigured>) (form => (BoundedTo<LeadMaint, CRLead>.Form.IConfigured) ((BoundedTo<LeadMaint, CRLead>.Form.INeedAnyConfigForm) form.Prompt("Details")).WithFields((Action<BoundedTo<LeadMaint, CRLead>.FormField.IContainerFillerFields>) (fields => AddResolutionFormField(fields, reasons["A"])))));
    BoundedTo<LeadMaint, CRLead>.Form.IConfigured formDisqualify = context.Forms.Create("FormDisqualify", (Func<BoundedTo<LeadMaint, CRLead>.Form.INeedAnyConfigForm, BoundedTo<LeadMaint, CRLead>.Form.IConfigured>) (form => (BoundedTo<LeadMaint, CRLead>.Form.IConfigured) ((BoundedTo<LeadMaint, CRLead>.Form.INeedAnyConfigForm) form.Prompt("Details")).WithFields((Action<BoundedTo<LeadMaint, CRLead>.FormField.IContainerFillerFields>) (fields => AddResolutionFormField(fields, reasons["L"])))));
    BoundedTo<LeadMaint, CRLead>.Form.IConfigured formConvert = context.Forms.Create("FormConvert", (Func<BoundedTo<LeadMaint, CRLead>.Form.INeedAnyConfigForm, BoundedTo<LeadMaint, CRLead>.Form.IConfigured>) (form => (BoundedTo<LeadMaint, CRLead>.Form.IConfigured) ((BoundedTo<LeadMaint, CRLead>.Form.INeedAnyConfigForm) form.Prompt("Details")).WithFields((Action<BoundedTo<LeadMaint, CRLead>.FormField.IContainerFillerFields>) (fields => AddResolutionFormField(fields, reasons["C"])))));
    BoundedTo<LeadMaint, CRLead>.ActionCategory.IConfigured categoryProcessing = context.Categories.CreateNew("Processing", (Func<BoundedTo<LeadMaint, CRLead>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<LeadMaint, CRLead>.ActionCategory.IConfigured>) (category => (BoundedTo<LeadMaint, CRLead>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<LeadMaint, CRLead>.ActionCategory.IConfigured categoryActivities = context.Categories.CreateNew("Activities", (Func<BoundedTo<LeadMaint, CRLead>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<LeadMaint, CRLead>.ActionCategory.IConfigured>) (category => (BoundedTo<LeadMaint, CRLead>.ActionCategory.IConfigured) category.DisplayName("Activities")));
    BoundedTo<LeadMaint, CRLead>.ActionCategory.IConfigured categoryRecordCreation = context.Categories.CreateNew("RecordCreation", (Func<BoundedTo<LeadMaint, CRLead>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<LeadMaint, CRLead>.ActionCategory.IConfigured>) (category => (BoundedTo<LeadMaint, CRLead>.ActionCategory.IConfigured) category.DisplayName("Record Creation")));
    BoundedTo<LeadMaint, CRLead>.ActionCategory.IConfigured categoryValidation = context.Categories.CreateNew("Validation", (Func<BoundedTo<LeadMaint, CRLead>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<LeadMaint, CRLead>.ActionCategory.IConfigured>) (category => (BoundedTo<LeadMaint, CRLead>.ActionCategory.IConfigured) category.DisplayName("Validation")));
    BoundedTo<LeadMaint, CRLead>.ActionDefinition.IExtendedConfigured actionOpen = context.ActionDefinitions.CreateNew("Open", (Func<BoundedTo<LeadMaint, CRLead>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<LeadMaint, CRLead>.ActionDefinition.IExtendedConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionDefinition.IExtendedConfigured) a.WithFieldAssignments((Action<BoundedTo<LeadMaint, CRLead>.Assignment.IContainerFillerFields>) (fields =>
    {
      fields.Add<CRLead.isActive>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) true)));
      fields.Add<CRLead.resolution>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromFormField(formOpen, "Reason")));
    })).DisplayName("Open").WithCategory(categoryProcessing).MapEnableToUpdate().WithForm(formOpen).WithPersistOptions((ActionPersistOptions) 2).IsExposedToMobile(true).MassProcessingScreen<UpdateLeadMassProcess>()));
    BoundedTo<LeadMaint, CRLead>.ActionDefinition.IExtendedConfigured actionQualify = context.ActionDefinitions.CreateNew("Qualify", (Func<BoundedTo<LeadMaint, CRLead>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<LeadMaint, CRLead>.ActionDefinition.IExtendedConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionDefinition.IExtendedConfigured) a.WithFieldAssignments((Action<BoundedTo<LeadMaint, CRLead>.Assignment.IContainerFillerFields>) (fields =>
    {
      fields.Add<CRLead.isActive>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) true)));
      fields.Add<CRLead.resolution>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromFormField(formQualify, "Reason")));
    })).DisplayName("Qualify").WithCategory(categoryProcessing).MapEnableToUpdate().WithForm(formQualify).WithPersistOptions((ActionPersistOptions) 2).IsExposedToMobile(true).MassProcessingScreen<UpdateLeadMassProcess>()));
    BoundedTo<LeadMaint, CRLead>.ActionDefinition.IExtendedConfigured actionAccept = context.ActionDefinitions.CreateNew("Accept", (Func<BoundedTo<LeadMaint, CRLead>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<LeadMaint, CRLead>.ActionDefinition.IExtendedConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionDefinition.IExtendedConfigured) a.WithFieldAssignments((Action<BoundedTo<LeadMaint, CRLead>.Assignment.IContainerFillerFields>) (fields =>
    {
      fields.Add<CRLead.isActive>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) true)));
      fields.Add<CRLead.resolution>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromFormField(formAccept, "Reason")));
    })).DisplayName("Accept").WithCategory(categoryProcessing).MapEnableToUpdate().WithForm(formAccept).WithPersistOptions((ActionPersistOptions) 2).IsExposedToMobile(true).MassProcessingScreen<UpdateLeadMassProcess>()));
    BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured actionConvertToOpportunityAll = context.ActionDefinitions.CreateExisting<LeadMaint.CreateOpportunityAllFromLeadGraphExt>((Expression<Func<LeadMaint.CreateOpportunityAllFromLeadGraphExt, PXAction<CRLead>>>) (e => e.ConvertToOpportunityAll), (Func<BoundedTo<LeadMaint, CRLead>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) a.WithCategory(categoryProcessing).WithPersistOptions((ActionPersistOptions) 2)));
    BoundedTo<LeadMaint, CRLead>.ActionDefinition.IExtendedConfigured actionDisqualify = context.ActionDefinitions.CreateNew("Disqualify", (Func<BoundedTo<LeadMaint, CRLead>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<LeadMaint, CRLead>.ActionDefinition.IExtendedConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionDefinition.IExtendedConfigured) a.WithFieldAssignments((Action<BoundedTo<LeadMaint, CRLead>.Assignment.IContainerFillerFields>) (fields =>
    {
      fields.Add<CRLead.isActive>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) false)));
      fields.Add<CRLead.resolution>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromFormField(formDisqualify, "Reason")));
      fields.Add<CRLead.overrideRefContact>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) true)));
    })).DisplayName("Disqualify").WithCategory(categoryProcessing).MapEnableToUpdate().WithForm(formDisqualify).WithPersistOptions((ActionPersistOptions) 2).IsExposedToMobile(true).MassProcessingScreen<UpdateLeadMassProcess>()));
    BoundedTo<LeadMaint, CRLead>.ActionDefinition.IExtendedConfigured actionMarkAsConverted = context.ActionDefinitions.CreateNew("MarkAsConverted", (Func<BoundedTo<LeadMaint, CRLead>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<LeadMaint, CRLead>.ActionDefinition.IExtendedConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionDefinition.IExtendedConfigured) a.WithFieldAssignments((Action<BoundedTo<LeadMaint, CRLead>.Assignment.IContainerFillerFields>) (fields =>
    {
      fields.Add<CRLead.resolution>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromFormField(formConvert, "Reason")));
      fields.Add<CRLead.isActive>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) false)));
    })).DisplayName("Mark as Converted").WithCategory(categoryProcessing).MapEnableToUpdate().WithForm(formConvert).WithPersistOptions((ActionPersistOptions) 2).IsExposedToMobile(true).MassProcessingScreen<UpdateLeadMassProcess>()));
    BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured actionCloseAsDuplicate = context.ActionDefinitions.CreateExisting<LeadMaint.CRDuplicateEntitiesForLeadGraphExt>((Expression<Func<LeadMaint.CRDuplicateEntitiesForLeadGraphExt, PXAction<CRLead>>>) (e => e.CloseAsDuplicate), (Func<BoundedTo<LeadMaint, CRLead>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) a.WithFieldAssignments((Action<BoundedTo<LeadMaint, CRLead>.Assignment.IContainerFillerFields>) (fields =>
    {
      fields.Add<CRLead.isActive>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) false)));
      fields.Add<CRLead.resolution>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) "DL")));
      fields.Add<CRLead.overrideRefContact>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) true)));
    })).WithCategory(categoryValidation).WithPersistOptions((ActionPersistOptions) 2)));
    BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured actionCreateEmail = context.ActionDefinitions.CreateExisting("newMailActivity", (Func<BoundedTo<LeadMaint, CRLead>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) a.WithCategory(categoryActivities)));
    BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured actionCreateTask = context.ActionDefinitions.CreateExisting("NewTask", (Func<BoundedTo<LeadMaint, CRLead>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) a.WithCategory(categoryActivities)));
    BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured actionCreatePhoneCall = context.ActionDefinitions.CreateExisting("NewActivityP_Workflow", (Func<BoundedTo<LeadMaint, CRLead>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) a.WithCategory(categoryActivities)));
    BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured actionCreateNote = context.ActionDefinitions.CreateExisting("NewActivityN_Workflow", (Func<BoundedTo<LeadMaint, CRLead>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) a.WithCategory(categoryActivities)));
    context.AddScreenConfigurationFor((Func<BoundedTo<LeadMaint, CRLead>.ScreenConfiguration.IStartConfigScreen, BoundedTo<LeadMaint, CRLead>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<LeadMaint, CRLead>.ScreenConfiguration.IConfigured) ((BoundedTo<LeadMaint, CRLead>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<CRLead.status>().AddDefaultFlow(new Func<BoundedTo<LeadMaint, CRLead>.Workflow.INeedStatesFlow, BoundedTo<LeadMaint, CRLead>.Workflow.IConfigured>(DefaultLeadFlow)).WithActions((Action<BoundedTo<LeadMaint, CRLead>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionOpen);
      actions.Add((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionQualify);
      actions.Add((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionAccept);
      actions.Add(actionConvertToOpportunityAll);
      actions.Add((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionDisqualify);
      actions.Add((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionMarkAsConverted);
      actions.Add(actionCreateEmail);
      actions.Add(actionCreateTask);
      actions.Add(actionCreatePhoneCall);
      actions.Add(actionCreateNote);
      actions.Add<LeadMaint.CreateBothAccountAndContactFromLeadGraphExt>((Expression<Func<LeadMaint.CreateBothAccountAndContactFromLeadGraphExt, PXAction<CRLead>>>) (e => e.CreateBothContactAndAccount), (Func<BoundedTo<LeadMaint, CRLead>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) a.WithCategory(categoryRecordCreation)));
      actions.Add<LeadMaint.CreateContactFromLeadGraphExt>((Expression<Func<LeadMaint.CreateContactFromLeadGraphExt, PXAction<CRLead>>>) (e => e.CreateContact), (Func<BoundedTo<LeadMaint, CRLead>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) a.WithCategory(categoryRecordCreation)));
      actions.Add<LeadMaint.CRDuplicateEntitiesForLeadGraphExt>((Expression<Func<LeadMaint.CRDuplicateEntitiesForLeadGraphExt, PXAction<CRLead>>>) (e => e.CheckForDuplicates), (Func<BoundedTo<LeadMaint, CRLead>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) a.WithCategory(categoryValidation)));
      actions.Add<LeadMaint.CRDuplicateEntitiesForLeadGraphExt>((Expression<Func<LeadMaint.CRDuplicateEntitiesForLeadGraphExt, PXAction<CRLead>>>) (e => e.MarkAsValidated), (Func<BoundedTo<LeadMaint, CRLead>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) a.WithCategory(categoryValidation).WithPersistOptions((ActionPersistOptions) 2)));
      actions.Add(actionCloseAsDuplicate);
      actions.Add<LeadMaint.LeadAddressActions>((Expression<Func<LeadMaint.LeadAddressActions, PXAction<CRLead>>>) (e => e.ValidateAddress), (Func<BoundedTo<LeadMaint, CRLead>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) a.WithCategory(categoryValidation).WithPersistOptions((ActionPersistOptions) 2)));
    })).WithForms((Action<BoundedTo<LeadMaint, CRLead>.Form.IContainerForms>) (forms =>
    {
      forms.Add(formOpen);
      forms.Add(formQualify);
      forms.Add(formAccept);
      forms.Add(formDisqualify);
      forms.Add(formConvert);
    })).WithFieldStates((Action<BoundedTo<LeadMaint, CRLead>.DynamicFieldState.IContainerFillerFields>) (fields =>
    {
      fields.Add<CRLead.status>((Func<BoundedTo<LeadMaint, CRLead>.DynamicFieldState.INeedAnyConfigField, BoundedTo<LeadMaint, CRLead>.DynamicFieldState.IConfigured>) (field => (BoundedTo<LeadMaint, CRLead>.DynamicFieldState.IConfigured) field.SetComboValues(new (string, string)[1]
      {
        ("S", "Suspended")
      })));
      fields.Add<CRLead.resolution>((Func<BoundedTo<LeadMaint, CRLead>.DynamicFieldState.INeedAnyConfigField, BoundedTo<LeadMaint, CRLead>.DynamicFieldState.IConfigured>) (field => (BoundedTo<LeadMaint, CRLead>.DynamicFieldState.IConfigured) field.SetComboValues(new (string, string)[11]
      {
        ("CR", "Created"),
        ("PI", "Potential Interest"),
        ("SB", "Subscribed"),
        ("IQ", "Inquiry"),
        ("QM", "Qualified by Marketing"),
        ("AS", "Accepted by Sales"),
        ("QS", "Qualified by Sales"),
        ("NI", "No Interest"),
        ("DL", "Duplicate"),
        ("CL", "Unable To Contact"),
        ("OT", "Other")
      })));
    })).WithCategories((Action<BoundedTo<LeadMaint, CRLead>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(categoryProcessing);
      categories.Add(categoryRecordCreation);
      categories.Add(categoryActivities);
      categories.Add(categoryValidation);
    }))));

    static void AddResolutionFormField(
      BoundedTo<LeadMaint, CRLead>.FormField.IContainerFillerFields filler,
      (string defaultValue, string[] values) comboBox)
    {
      filler.Add("Reason", (Func<BoundedTo<LeadMaint, CRLead>.FormField.INeedTypeConfigField, BoundedTo<LeadMaint, CRLead>.FormField.IConfigured>) (field => (BoundedTo<LeadMaint, CRLead>.FormField.IConfigured) field.WithSchemaOf<CRLead.resolution>().IsRequired().Prompt("Reason").DefaultValue((object) comboBox.defaultValue).OnlyComboBoxValues(comboBox.values)));
    }

    static void AddSourceFieldState(
      BoundedTo<LeadMaint, CRLead>.FieldState.IContainerFillerFields filler,
      bool disabled = false)
    {
      filler.AddField<CRLead.source>((Func<BoundedTo<LeadMaint, CRLead>.FieldState.INeedAnyConfigField, BoundedTo<LeadMaint, CRLead>.FieldState.IConfigured>) (field => (BoundedTo<LeadMaint, CRLead>.FieldState.IConfigured) field.ComboBoxValues(CRMSourcesAttribute.Values).IsDisabled<LeadMaint, CRLead>(disabled)));
    }

    static void DisableFieldsForFinalStates(
      BoundedTo<LeadMaint, CRLead>.FieldState.IContainerFillerFields filler)
    {
      filler.AddTable<CRLead>((Func<BoundedTo<LeadMaint, CRLead>.FieldState.INeedAnyConfigField, BoundedTo<LeadMaint, CRLead>.FieldState.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.FieldState.IConfigured) f.IsDisabled()));
      filler.AddTable<Address>((Func<BoundedTo<LeadMaint, CRLead>.FieldState.INeedAnyConfigField, BoundedTo<LeadMaint, CRLead>.FieldState.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.FieldState.IConfigured) f.IsDisabled()));
      filler.AddTable<CRCampaignMembers>((Func<BoundedTo<LeadMaint, CRLead>.FieldState.INeedAnyConfigField, BoundedTo<LeadMaint, CRLead>.FieldState.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.FieldState.IConfigured) f.IsDisabled()));
      filler.AddTable<CROpportunity>((Func<BoundedTo<LeadMaint, CRLead>.FieldState.INeedAnyConfigField, BoundedTo<LeadMaint, CRLead>.FieldState.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.FieldState.IConfigured) f.IsDisabled()));
      filler.AddField<CRLead.contactID>((Func<BoundedTo<LeadMaint, CRLead>.FieldState.INeedAnyConfigField, BoundedTo<LeadMaint, CRLead>.FieldState.IConfigured>) null);
      filler.AddField<CRLead.description>((Func<BoundedTo<LeadMaint, CRLead>.FieldState.INeedAnyConfigField, BoundedTo<LeadMaint, CRLead>.FieldState.IConfigured>) null);
    }

    static void DisableFieldsAndAttributesForFinalStates(
      BoundedTo<LeadMaint, CRLead>.FieldState.IContainerFillerFields fields)
    {
      DisableFieldsForFinalStates(fields);
      fields.AddTable<CSAnswers>((Func<BoundedTo<LeadMaint, CRLead>.FieldState.INeedAnyConfigField, BoundedTo<LeadMaint, CRLead>.FieldState.IConfigured>) (field => (BoundedTo<LeadMaint, CRLead>.FieldState.IConfigured) field.IsDisabled()));
    }

    BoundedTo<LeadMaint, CRLead>.Workflow.IConfigured DefaultLeadFlow(
      BoundedTo<LeadMaint, CRLead>.Workflow.INeedStatesFlow flow)
    {
      BoundedTo<LeadMaint, CRLead>.BaseFlowStep.IConfigured newState = context.FlowStates.Create("H", (Func<BoundedTo<LeadMaint, CRLead>.FlowState.INeedAnyFlowStateConfig, BoundedTo<LeadMaint, CRLead>.BaseFlowStep.IConfigured>) (state => (BoundedTo<LeadMaint, CRLead>.BaseFlowStep.IConfigured) ((BoundedTo<LeadMaint, CRLead>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<LeadMaint, CRLead>.FlowState.INeedAnyFlowStateConfig) state.IsInitial()).WithFieldStates((Action<BoundedTo<LeadMaint, CRLead>.FieldState.IContainerFillerFields>) (fields =>
      {
        AddReasonFieldState(fields, "H");
        AddSourceFieldState(fields);
      }))).WithActions((Action<BoundedTo<LeadMaint, CRLead>.ActionState.IContainerFillerActions>) (actions =>
      {
        AddOpenAction(actions, true, true);
        AddQualifyAction(actions);
        AddAcceptAction(actions);
        AddDisqualifyAction(actions);
        AddConvertToOpportunityAction(actions);
        AddMarkAsConvertedAction(actions);
        actions.Add<LeadMaint.CreateContactFromLeadGraphExt>((Expression<Func<LeadMaint.CreateContactFromLeadGraphExt, PXAction<CRLead>>>) (e => e.CreateContact), (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) null);
        actions.Add<LeadMaint.CreateBothAccountAndContactFromLeadGraphExt>((Expression<Func<LeadMaint.CreateBothAccountAndContactFromLeadGraphExt, PXAction<CRLead>>>) (e => e.CreateBothContactAndAccount), (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) null);
        AddCloseAsDuplicateAction(actions);
        actions.Add<LeadMaint.CRDuplicateEntitiesForLeadGraphExt>((Expression<Func<LeadMaint.CRDuplicateEntitiesForLeadGraphExt, PXAction<CRLead>>>) (e => e.CheckForDuplicates), (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) null);
        actions.Add<LeadMaint.CRDuplicateEntitiesForLeadGraphExt>((Expression<Func<LeadMaint.CRDuplicateEntitiesForLeadGraphExt, PXAction<CRLead>>>) (e => e.MarkAsValidated), (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) null);
        actions.Add<LeadMaint.LeadAddressActions>((Expression<Func<LeadMaint.LeadAddressActions, PXAction<CRLead>>>) (e => e.ValidateAddress), (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) null);
      }))));
      BoundedTo<LeadMaint, CRLead>.BaseFlowStep.IConfigured openState = context.FlowStates.Create("O", (Func<BoundedTo<LeadMaint, CRLead>.FlowState.INeedAnyFlowStateConfig, BoundedTo<LeadMaint, CRLead>.BaseFlowStep.IConfigured>) (state => (BoundedTo<LeadMaint, CRLead>.BaseFlowStep.IConfigured) ((BoundedTo<LeadMaint, CRLead>.FlowState.INeedAnyFlowStateConfig) state.WithFieldStates((Action<BoundedTo<LeadMaint, CRLead>.FieldState.IContainerFillerFields>) (fields =>
      {
        AddReasonFieldState(fields, "O");
        AddSourceFieldState(fields);
      }))).WithActions((Action<BoundedTo<LeadMaint, CRLead>.ActionState.IContainerFillerActions>) (actions =>
      {
        AddQualifyAction(actions, true, true);
        AddAcceptAction(actions);
        AddDisqualifyAction(actions);
        AddConvertToOpportunityAction(actions);
        AddMarkAsConvertedAction(actions);
        actions.Add<LeadMaint.CreateContactFromLeadGraphExt>((Expression<Func<LeadMaint.CreateContactFromLeadGraphExt, PXAction<CRLead>>>) (e => e.CreateContact), (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) null);
        actions.Add<LeadMaint.CreateBothAccountAndContactFromLeadGraphExt>((Expression<Func<LeadMaint.CreateBothAccountAndContactFromLeadGraphExt, PXAction<CRLead>>>) (e => e.CreateBothContactAndAccount), (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) null);
        AddCloseAsDuplicateAction(actions);
        actions.Add<LeadMaint.CRDuplicateEntitiesForLeadGraphExt>((Expression<Func<LeadMaint.CRDuplicateEntitiesForLeadGraphExt, PXAction<CRLead>>>) (e => e.CheckForDuplicates), (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) null);
        actions.Add<LeadMaint.CRDuplicateEntitiesForLeadGraphExt>((Expression<Func<LeadMaint.CRDuplicateEntitiesForLeadGraphExt, PXAction<CRLead>>>) (e => e.MarkAsValidated), (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) null);
        actions.Add<LeadMaint.LeadAddressActions>((Expression<Func<LeadMaint.LeadAddressActions, PXAction<CRLead>>>) (e => e.ValidateAddress), (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) null);
      }))));
      BoundedTo<LeadMaint, CRLead>.BaseFlowStep.IConfigured salesReadyState = context.FlowStates.Create("Q", (Func<BoundedTo<LeadMaint, CRLead>.FlowState.INeedAnyFlowStateConfig, BoundedTo<LeadMaint, CRLead>.BaseFlowStep.IConfigured>) (state => (BoundedTo<LeadMaint, CRLead>.BaseFlowStep.IConfigured) ((BoundedTo<LeadMaint, CRLead>.FlowState.INeedAnyFlowStateConfig) state.WithFieldStates((Action<BoundedTo<LeadMaint, CRLead>.FieldState.IContainerFillerFields>) (fields =>
      {
        AddReasonFieldState(fields, "Q", true);
        AddSourceFieldState(fields, true);
        fields.AddField<CRLead.campaignID>((Func<BoundedTo<LeadMaint, CRLead>.FieldState.INeedAnyConfigField, BoundedTo<LeadMaint, CRLead>.FieldState.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.FieldState.IConfigured) f.IsDisabled()));
      }))).WithActions((Action<BoundedTo<LeadMaint, CRLead>.ActionState.IContainerFillerActions>) (actions =>
      {
        AddOpenAction(actions);
        AddAcceptAction(actions, true, true);
        AddDisqualifyAction(actions);
        AddConvertToOpportunityAction(actions);
        AddMarkAsConvertedAction(actions);
        actions.Add<LeadMaint.CreateContactFromLeadGraphExt>((Expression<Func<LeadMaint.CreateContactFromLeadGraphExt, PXAction<CRLead>>>) (e => e.CreateContact), (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) null);
        actions.Add<LeadMaint.CreateBothAccountAndContactFromLeadGraphExt>((Expression<Func<LeadMaint.CreateBothAccountAndContactFromLeadGraphExt, PXAction<CRLead>>>) (e => e.CreateBothContactAndAccount), (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) null);
        AddCloseAsDuplicateAction(actions);
        actions.Add<LeadMaint.CRDuplicateEntitiesForLeadGraphExt>((Expression<Func<LeadMaint.CRDuplicateEntitiesForLeadGraphExt, PXAction<CRLead>>>) (e => e.CheckForDuplicates), (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) null);
        actions.Add<LeadMaint.CRDuplicateEntitiesForLeadGraphExt>((Expression<Func<LeadMaint.CRDuplicateEntitiesForLeadGraphExt, PXAction<CRLead>>>) (e => e.MarkAsValidated), (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) null);
        actions.Add<LeadMaint.LeadAddressActions>((Expression<Func<LeadMaint.LeadAddressActions, PXAction<CRLead>>>) (e => e.ValidateAddress), (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) null);
      }))));
      BoundedTo<LeadMaint, CRLead>.BaseFlowStep.IConfigured salesAcceptedState = context.FlowStates.Create("A", (Func<BoundedTo<LeadMaint, CRLead>.FlowState.INeedAnyFlowStateConfig, BoundedTo<LeadMaint, CRLead>.BaseFlowStep.IConfigured>) (state => (BoundedTo<LeadMaint, CRLead>.BaseFlowStep.IConfigured) ((BoundedTo<LeadMaint, CRLead>.FlowState.INeedAnyFlowStateConfig) state.WithFieldStates((Action<BoundedTo<LeadMaint, CRLead>.FieldState.IContainerFillerFields>) (fields =>
      {
        AddReasonFieldState(fields, "A", true);
        AddSourceFieldState(fields, true);
        fields.AddField<CRLead.campaignID>((Func<BoundedTo<LeadMaint, CRLead>.FieldState.INeedAnyConfigField, BoundedTo<LeadMaint, CRLead>.FieldState.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.FieldState.IConfigured) f.IsDisabled()));
      }))).WithActions((Action<BoundedTo<LeadMaint, CRLead>.ActionState.IContainerFillerActions>) (actions =>
      {
        AddOpenAction(actions);
        AddDisqualifyAction(actions);
        AddConvertToOpportunityAction(actions, true, true);
        AddMarkAsConvertedAction(actions);
        actions.Add<LeadMaint.CreateContactFromLeadGraphExt>((Expression<Func<LeadMaint.CreateContactFromLeadGraphExt, PXAction<CRLead>>>) (e => e.CreateContact), (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) null);
        actions.Add<LeadMaint.CreateBothAccountAndContactFromLeadGraphExt>((Expression<Func<LeadMaint.CreateBothAccountAndContactFromLeadGraphExt, PXAction<CRLead>>>) (e => e.CreateBothContactAndAccount), (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) null);
        AddCloseAsDuplicateAction(actions);
        actions.Add<LeadMaint.CRDuplicateEntitiesForLeadGraphExt>((Expression<Func<LeadMaint.CRDuplicateEntitiesForLeadGraphExt, PXAction<CRLead>>>) (e => e.CheckForDuplicates), (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) null);
        actions.Add<LeadMaint.CRDuplicateEntitiesForLeadGraphExt>((Expression<Func<LeadMaint.CRDuplicateEntitiesForLeadGraphExt, PXAction<CRLead>>>) (e => e.MarkAsValidated), (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) null);
        actions.Add<LeadMaint.LeadAddressActions>((Expression<Func<LeadMaint.LeadAddressActions, PXAction<CRLead>>>) (e => e.ValidateAddress), (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) null);
      }))));
      BoundedTo<LeadMaint, CRLead>.BaseFlowStep.IConfigured convertedState = context.FlowStates.Create("C", (Func<BoundedTo<LeadMaint, CRLead>.FlowState.INeedAnyFlowStateConfig, BoundedTo<LeadMaint, CRLead>.BaseFlowStep.IConfigured>) (state => (BoundedTo<LeadMaint, CRLead>.BaseFlowStep.IConfigured) ((BoundedTo<LeadMaint, CRLead>.FlowState.INeedAnyFlowStateConfig) state.WithFieldStates((Action<BoundedTo<LeadMaint, CRLead>.FieldState.IContainerFillerFields>) (fields =>
      {
        AddReasonFieldState(fields, "C", true);
        DisableFieldsForFinalStates(fields);
      }))).WithActions((Action<BoundedTo<LeadMaint, CRLead>.ActionState.IContainerFillerActions>) (actions => AddOpenAction(actions, true)))));
      BoundedTo<LeadMaint, CRLead>.BaseFlowStep.IConfigured disqualifiedState = context.FlowStates.Create("L", (Func<BoundedTo<LeadMaint, CRLead>.FlowState.INeedAnyFlowStateConfig, BoundedTo<LeadMaint, CRLead>.BaseFlowStep.IConfigured>) (state => (BoundedTo<LeadMaint, CRLead>.BaseFlowStep.IConfigured) ((BoundedTo<LeadMaint, CRLead>.FlowState.INeedAnyFlowStateConfig) state.WithFieldStates((Action<BoundedTo<LeadMaint, CRLead>.FieldState.IContainerFillerFields>) (fields =>
      {
        AddReasonFieldState(fields, "L", true);
        DisableFieldsForFinalStates(fields);
      }))).WithActions((Action<BoundedTo<LeadMaint, CRLead>.ActionState.IContainerFillerActions>) (actions => AddOpenAction(actions, true, true)))));
      BoundedTo<LeadMaint, CRLead>.BaseFlowStep.IConfigured suspendState = context.FlowStates.Create("S", (Func<BoundedTo<LeadMaint, CRLead>.FlowState.INeedAnyFlowStateConfig, BoundedTo<LeadMaint, CRLead>.BaseFlowStep.IConfigured>) (state => (BoundedTo<LeadMaint, CRLead>.BaseFlowStep.IConfigured) ((BoundedTo<LeadMaint, CRLead>.FlowState.INeedAnyFlowStateConfig) state.WithFieldStates((Action<BoundedTo<LeadMaint, CRLead>.FieldState.IContainerFillerFields>) (fields => DisableFieldsAndAttributesForFinalStates(fields)))).WithActions((Action<BoundedTo<LeadMaint, CRLead>.ActionState.IContainerFillerActions>) (actions => AddOpenAction(actions)))));
      return (BoundedTo<LeadMaint, CRLead>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<LeadMaint, CRLead>.BaseFlowStep.IContainerFillerStates>) (states =>
      {
        states.Add(newState);
        states.Add(openState);
        states.Add(salesReadyState);
        states.Add(salesAcceptedState);
        states.Add(disqualifiedState);
        states.Add(convertedState);
        states.Add(suspendState);
      })).WithTransitions((Action<BoundedTo<LeadMaint, CRLead>.Transition.IContainerFillerTransitions>) (transitions =>
      {
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(newState).To(openState).IsTriggeredOn((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionOpen)));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(newState).To(salesReadyState).IsTriggeredOn((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionQualify)));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(newState).To(salesAcceptedState).IsTriggeredOn((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionAccept)));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(newState).To(disqualifiedState).IsTriggeredOn((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionDisqualify)));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(newState).To(disqualifiedState).IsTriggeredOn<LeadMaint.CRDuplicateEntitiesForLeadGraphExt>((Expression<Func<LeadMaint.CRDuplicateEntitiesForLeadGraphExt, PXAction<CRLead>>>) (e => e.CloseAsDuplicate)).WithFieldAssignments((Action<BoundedTo<LeadMaint, CRLead>.Assignment.IContainerFillerFields>) (fields =>
        {
          fields.Add<CRLead.isActive>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) false)));
          fields.Add<CRLead.resolution>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) "DL")));
          fields.Add<CRLead.overrideRefContact>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) true)));
        }))));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(newState).To(convertedState).IsTriggeredOn((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionMarkAsConverted).WithFieldAssignments((Action<BoundedTo<LeadMaint, CRLead>.Assignment.IContainerFillerFields>) (fields => fields.Add<CRLead.overrideRefContact>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(newState).To(convertedState).IsTriggeredOn<LeadMaint.CreateOpportunityAllFromLeadGraphExt>((Expression<Func<LeadMaint.CreateOpportunityAllFromLeadGraphExt, PXAction<CRLead>>>) (e => e.ConvertToOpportunityAll)).WithFieldAssignments((Action<BoundedTo<LeadMaint, CRLead>.Assignment.IContainerFillerFields>) (fields =>
        {
          fields.Add<CRLead.overrideRefContact>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) true)));
          fields.Add<CRLead.isActive>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) false)));
          fields.Add<CRLead.resolution>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) reasons["C"].Item1)));
        }))));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(openState).To(salesReadyState).IsTriggeredOn((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionQualify)));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(openState).To(salesAcceptedState).IsTriggeredOn((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionAccept)));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(openState).To(disqualifiedState).IsTriggeredOn((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionDisqualify)));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(openState).To(disqualifiedState).IsTriggeredOn<LeadMaint.CRDuplicateEntitiesForLeadGraphExt>((Expression<Func<LeadMaint.CRDuplicateEntitiesForLeadGraphExt, PXAction<CRLead>>>) (e => e.CloseAsDuplicate))));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(openState).To(convertedState).IsTriggeredOn((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionMarkAsConverted).WithFieldAssignments((Action<BoundedTo<LeadMaint, CRLead>.Assignment.IContainerFillerFields>) (fields => fields.Add<CRLead.overrideRefContact>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(openState).To(convertedState).IsTriggeredOn<LeadMaint.CreateOpportunityAllFromLeadGraphExt>((Expression<Func<LeadMaint.CreateOpportunityAllFromLeadGraphExt, PXAction<CRLead>>>) (e => e.ConvertToOpportunityAll)).WithFieldAssignments((Action<BoundedTo<LeadMaint, CRLead>.Assignment.IContainerFillerFields>) (fields =>
        {
          fields.Add<CRLead.overrideRefContact>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) true)));
          fields.Add<CRLead.isActive>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) false)));
          fields.Add<CRLead.resolution>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) reasons["C"].Item1)));
        }))));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(salesReadyState).To(openState).IsTriggeredOn((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionOpen)));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(salesReadyState).To(salesAcceptedState).IsTriggeredOn((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionAccept)));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(salesReadyState).To(disqualifiedState).IsTriggeredOn((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionDisqualify)));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(salesReadyState).To(disqualifiedState).IsTriggeredOn<LeadMaint.CRDuplicateEntitiesForLeadGraphExt>((Expression<Func<LeadMaint.CRDuplicateEntitiesForLeadGraphExt, PXAction<CRLead>>>) (e => e.CloseAsDuplicate))));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(salesReadyState).To(convertedState).IsTriggeredOn((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionMarkAsConverted).WithFieldAssignments((Action<BoundedTo<LeadMaint, CRLead>.Assignment.IContainerFillerFields>) (fields => fields.Add<CRLead.overrideRefContact>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(salesReadyState).To(convertedState).IsTriggeredOn<LeadMaint.CreateOpportunityAllFromLeadGraphExt>((Expression<Func<LeadMaint.CreateOpportunityAllFromLeadGraphExt, PXAction<CRLead>>>) (e => e.ConvertToOpportunityAll)).WithFieldAssignments((Action<BoundedTo<LeadMaint, CRLead>.Assignment.IContainerFillerFields>) (fields =>
        {
          fields.Add<CRLead.overrideRefContact>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) true)));
          fields.Add<CRLead.isActive>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) false)));
          fields.Add<CRLead.resolution>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) reasons["C"].Item1)));
        }))));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(salesAcceptedState).To(openState).IsTriggeredOn((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionOpen)));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(salesAcceptedState).To(disqualifiedState).IsTriggeredOn((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionDisqualify)));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(salesAcceptedState).To(disqualifiedState).IsTriggeredOn<LeadMaint.CRDuplicateEntitiesForLeadGraphExt>((Expression<Func<LeadMaint.CRDuplicateEntitiesForLeadGraphExt, PXAction<CRLead>>>) (e => e.CloseAsDuplicate))));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(salesAcceptedState).To(convertedState).IsTriggeredOn((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionMarkAsConverted).WithFieldAssignments((Action<BoundedTo<LeadMaint, CRLead>.Assignment.IContainerFillerFields>) (fields => fields.Add<CRLead.overrideRefContact>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) true)))))));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(salesAcceptedState).To(convertedState).IsTriggeredOn<LeadMaint.CreateOpportunityAllFromLeadGraphExt>((Expression<Func<LeadMaint.CreateOpportunityAllFromLeadGraphExt, PXAction<CRLead>>>) (e => e.ConvertToOpportunityAll)).WithFieldAssignments((Action<BoundedTo<LeadMaint, CRLead>.Assignment.IContainerFillerFields>) (fields =>
        {
          fields.Add<CRLead.overrideRefContact>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) true)));
          fields.Add<CRLead.isActive>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) false)));
          fields.Add<CRLead.resolution>((Func<BoundedTo<LeadMaint, CRLead>.Assignment.INeedRightOperand, BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured>) (f => (BoundedTo<LeadMaint, CRLead>.Assignment.IConfigured) f.SetFromValue((object) reasons["C"].Item1)));
        }))));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(disqualifiedState).To(openState).IsTriggeredOn((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionOpen)));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(convertedState).To(openState).IsTriggeredOn((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionOpen)));
        transitions.Add((Func<BoundedTo<LeadMaint, CRLead>.Transition.INeedSource, BoundedTo<LeadMaint, CRLead>.Transition.IConfigured>) (transition => (BoundedTo<LeadMaint, CRLead>.Transition.IConfigured) transition.From(suspendState).To(openState).IsTriggeredOn((BoundedTo<LeadMaint, CRLead>.ActionDefinition.IConfigured) actionOpen)));
      }));
    }

    void AddReasonFieldState(
      BoundedTo<LeadMaint, CRLead>.FieldState.IContainerFillerFields filler,
      string state,
      bool disabled = false)
    {
      filler.AddField<CRLead.resolution>((Func<BoundedTo<LeadMaint, CRLead>.FieldState.INeedAnyConfigField, BoundedTo<LeadMaint, CRLead>.FieldState.IConfigured>) (field => (BoundedTo<LeadMaint, CRLead>.FieldState.IConfigured) ((BoundedTo<LeadMaint, CRLead>.FieldState.INeedAnyConfigField) ((BoundedTo<LeadMaint, CRLead>.FieldState.INeedAnyConfigField) field.DefaultValue((object) reasons[state].Item1)).ComboBoxValues(reasons[state].Item2)).IsRequired().IsDisabled<LeadMaint, CRLead>(disabled)));
    }

    void AddCloseAsDuplicateAction(
      BoundedTo<LeadMaint, CRLead>.ActionState.IContainerFillerActions filler)
    {
      filler.Add(actionCloseAsDuplicate, (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured) a));
    }

    void AddOpenAction(
      BoundedTo<LeadMaint, CRLead>.ActionState.IContainerFillerActions filler,
      bool addToToolbar = false,
      bool withConnotation = false)
    {
      filler.Add(actionOpen, (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured) ((BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig) a).IsDuplicatedInToolbar<LeadMaint, CRLead>(addToToolbar).WithSuccessConnotation(withConnotation)));
    }

    void AddQualifyAction(
      BoundedTo<LeadMaint, CRLead>.ActionState.IContainerFillerActions filler,
      bool addToToolbar = false,
      bool withConnotation = false)
    {
      filler.Add(actionQualify, (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured) ((BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig) a).IsDuplicatedInToolbar<LeadMaint, CRLead>(addToToolbar).WithSuccessConnotation(withConnotation)));
    }

    void AddAcceptAction(
      BoundedTo<LeadMaint, CRLead>.ActionState.IContainerFillerActions filler,
      bool addToToolbar = false,
      bool withConnotation = false)
    {
      filler.Add(actionAccept, (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured) ((BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig) a).IsDuplicatedInToolbar<LeadMaint, CRLead>(addToToolbar).WithSuccessConnotation(withConnotation)));
    }

    void AddDisqualifyAction(
      BoundedTo<LeadMaint, CRLead>.ActionState.IContainerFillerActions filler)
    {
      filler.Add(actionDisqualify, (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured) a));
    }

    void AddConvertToOpportunityAction(
      BoundedTo<LeadMaint, CRLead>.ActionState.IContainerFillerActions filler,
      bool addToToolbar = false,
      bool withConnotation = false)
    {
      filler.Add(actionConvertToOpportunityAll, (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured) a.IsDuplicatedInToolbar<LeadMaint, CRLead>(addToToolbar).WithSuccessConnotation(withConnotation)));
    }

    void AddMarkAsConvertedAction(
      BoundedTo<LeadMaint, CRLead>.ActionState.IContainerFillerActions filler)
    {
      filler.Add(actionMarkAsConverted, (Func<BoundedTo<LeadMaint, CRLead>.ActionState.IAllowExtendedOptionalConfig, BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured>) (a => (BoundedTo<LeadMaint, CRLead>.ActionState.IConfigured) a));
    }
  }

  /// <summary>
  /// Statuses for <see cref="T:PX.Objects.CR.CRLead.status" /> used by default in system workflow.
  /// Values could be changed and extended by workflow.
  /// Note, that <see cref="F:PX.Objects.CR.Workflows.LeadWorkflow.States.Converted" /> status used in Campaigns screen to count converted leads: <see cref="T:PX.Objects.CR.DAC.Standalone.CRCampaign.leadsConverted" />.
  /// </summary>
  public static class States
  {
    public const string New = "H";
    public const string Open = "O";
    public const string SalesReady = "Q";
    public const string SalesAccepted = "A";
    public const string Converted = "C";
    public const string Disqualified = "L";
    [Obsolete("This status used only for backward (data) compatibility.")]
    public const string Suspend = "S";

    internal class converted : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    LeadWorkflow.States.converted>
    {
      public converted()
        : base("C")
      {
      }
    }

    internal class List : PXStringListAttribute
    {
      public List()
        : base(new string[6]{ "H", "O", "Q", "A", "C", "L" }, new string[6]
        {
          "New",
          "Open",
          "Sales-Ready",
          "Sales-Accepted",
          "Converted",
          "Disqualified"
        })
      {
      }
    }
  }

  public static class CategoryNames
  {
    public const string Processing = "Processing";
    public const string Activities = "Activities";
    public const string RecordCreation = "RecordCreation";
    public const string Validation = "Validation";
  }
}
