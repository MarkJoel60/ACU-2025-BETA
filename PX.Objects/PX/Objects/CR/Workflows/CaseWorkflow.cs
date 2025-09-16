// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Workflows.CaseWorkflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.CR.CRCaseMaint_Extensions;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CR.Workflows;

/// <summary>
/// Extensions that used to configure Workflow for <see cref="T:PX.Objects.CR.CRCaseMaint" /> and <see cref="T:PX.Objects.CR.CRCase" />.
/// Use Extensions Chaining for this extension if you want customize workflow with code for this graph of DAC.
/// </summary>
public class CaseWorkflow : PXGraphExtension<CRCaseMaint>
{
  public PXAction<CRCase> openCaseFromProcessing;
  public PXAction<CRCase> openCaseFromPortal;
  public PXAction<CRCase> closeCaseFromPortal;

  [PXUIField]
  [PXButton]
  protected virtual void OpenCaseFromProcessing()
  {
  }

  [PXUIField]
  [PXButton]
  protected virtual void OpenCaseFromPortal()
  {
  }

  [PXUIField]
  [PXButton]
  protected virtual void CloseCaseFromPortal()
  {
  }

  public virtual void Configure(PXScreenConfiguration config)
  {
    CaseWorkflow.Configure(config.GetScreenConfigurationContext<CRCaseMaint, CRCase>());
  }

  protected static void Configure(WorkflowContext<CRCaseMaint, CRCase> context)
  {
    CaseWorkflow.Conditions conditions = context.Conditions.GetPack<CaseWorkflow.Conditions>();
    Dictionary<string, string[]> reasons = new Dictionary<string, string[]>(5)
    {
      ["N"] = new string[2]{ "AS", "AA" },
      ["O"] = new string[5]{ "IP", "AD", "ES", "CC", "AA" },
      ["P"] = new string[3]{ "MI", "CR", "CC" },
      ["C"] = new string[5]{ "RD", "RJ", "CL", "CA", "DP" },
      ["R"] = new string[5]{ "RD", "RJ", "CL", "CA", "DP" }
    };
    BoundedTo<CRCaseMaint, CRCase>.Form.IConfigured formOpen = context.Forms.Create("FormOpen", (Func<BoundedTo<CRCaseMaint, CRCase>.Form.INeedAnyConfigForm, BoundedTo<CRCaseMaint, CRCase>.Form.IConfigured>) (form => (BoundedTo<CRCaseMaint, CRCase>.Form.IConfigured) ((BoundedTo<CRCaseMaint, CRCase>.Form.INeedAnyConfigForm) form.Prompt("Open")).WithFields((Action<BoundedTo<CRCaseMaint, CRCase>.FormField.IContainerFillerFields>) (fields =>
    {
      AddResolutionFormField(fields, "IP");
      fields.Add("Owner", (Func<BoundedTo<CRCaseMaint, CRCase>.FormField.INeedTypeConfigField, BoundedTo<CRCaseMaint, CRCase>.FormField.IConfigured>) (field => (BoundedTo<CRCaseMaint, CRCase>.FormField.IConfigured) field.WithSchemaOf<CRCase.ownerID>().Prompt("Owner").DefaultValueFromSchemaField()));
    }))));
    BoundedTo<CRCaseMaint, CRCase>.Form.IConfigured formPendingCustomer = context.Forms.Create("FormPendingCustomer", (Func<BoundedTo<CRCaseMaint, CRCase>.Form.INeedAnyConfigForm, BoundedTo<CRCaseMaint, CRCase>.Form.IConfigured>) (form => (BoundedTo<CRCaseMaint, CRCase>.Form.IConfigured) ((BoundedTo<CRCaseMaint, CRCase>.Form.INeedAnyConfigForm) form.Prompt("Pending Customer")).WithFields((Action<BoundedTo<CRCaseMaint, CRCase>.FormField.IContainerFillerFields>) (fields =>
    {
      AddResolutionFormField(fields, "CR");
      AddSolutionActivityNoteIDFormField(fields);
    }))));
    BoundedTo<CRCaseMaint, CRCase>.Form.IConfigured formClose = context.Forms.Create("FormClose", (Func<BoundedTo<CRCaseMaint, CRCase>.Form.INeedAnyConfigForm, BoundedTo<CRCaseMaint, CRCase>.Form.IConfigured>) (form => (BoundedTo<CRCaseMaint, CRCase>.Form.IConfigured) ((BoundedTo<CRCaseMaint, CRCase>.Form.INeedAnyConfigForm) form.Prompt("Close")).WithFields((Action<BoundedTo<CRCaseMaint, CRCase>.FormField.IContainerFillerFields>) (fields =>
    {
      AddResolutionFormField(fields, "RD");
      AddSolutionActivityNoteIDFormField(fields);
      fields.Add("ClosureNotes", (Func<BoundedTo<CRCaseMaint, CRCase>.FormField.INeedTypeConfigField, BoundedTo<CRCaseMaint, CRCase>.FormField.IConfigured>) (field => (BoundedTo<CRCaseMaint, CRCase>.FormField.IConfigured) field.WithRichTextEditorField().IsRequiredWhen((BoundedTo<CRCaseMaint, CRCase>.ISharedCondition) conditions.IsClosureNotesRequired).Prompt("Closure Notes").DefaultExpression("[closureNotes]")));
    }))));
    BoundedTo<CRCaseMaint, CRCase>.ActionCategory.IConfigured categoryProcessing = context.Categories.CreateNew("Processing", (Func<BoundedTo<CRCaseMaint, CRCase>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CRCaseMaint, CRCase>.ActionCategory.IConfigured>) (category => (BoundedTo<CRCaseMaint, CRCase>.ActionCategory.IConfigured) category.DisplayName("Processing")));
    BoundedTo<CRCaseMaint, CRCase>.ActionCategory.IConfigured categoryServices = context.Categories.CreateNew("CustomerServices", (Func<BoundedTo<CRCaseMaint, CRCase>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CRCaseMaint, CRCase>.ActionCategory.IConfigured>) (category => (BoundedTo<CRCaseMaint, CRCase>.ActionCategory.IConfigured) category.DisplayName("Customer Services")));
    BoundedTo<CRCaseMaint, CRCase>.ActionCategory.IConfigured categoryActivities = context.Categories.CreateNew("Activities", (Func<BoundedTo<CRCaseMaint, CRCase>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CRCaseMaint, CRCase>.ActionCategory.IConfigured>) (category => (BoundedTo<CRCaseMaint, CRCase>.ActionCategory.IConfigured) category.DisplayName("Activities")));
    BoundedTo<CRCaseMaint, CRCase>.ActionCategory.IConfigured categoryOther = context.Categories.CreateNew("Other", (Func<BoundedTo<CRCaseMaint, CRCase>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CRCaseMaint, CRCase>.ActionCategory.IConfigured>) (category => (BoundedTo<CRCaseMaint, CRCase>.ActionCategory.IConfigured) category.DisplayName("Other")));
    BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured actionOpen = context.ActionDefinitions.CreateExisting((Expression<Func<CRCaseMaint, PXAction<CRCase>>>) (g => g.Open), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured) a.WithFieldAssignments((Action<BoundedTo<CRCaseMaint, CRCase>.Assignment.IContainerFillerFields>) (fields =>
    {
      fields.Add<CRCase.isActive>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) true)));
      fields.Add<CRCase.resolution>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromFormField(formOpen, "Reason")));
      fields.Add<CRCase.ownerID>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromFormField(formOpen, "Owner")));
      fields.Add<CRCase.resolutionDate>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) null)));
    })).DisplayName("Open").WithCategory(categoryProcessing).MapEnableToUpdate().WithForm(formOpen).WithPersistOptions((ActionPersistOptions) 2).IsExposedToMobile(true).MassProcessingScreen<UpdateCaseMassProcess>()));
    BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured gactionTakeCase = context.ActionDefinitions.CreateExisting((Expression<Func<CRCaseMaint, PXAction<CRCase>>>) (g => g.takeCase), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured) a.DoesNotPersist().WithCategory(categoryProcessing).PlaceAfter(actionOpen)));
    BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured actionClose = context.ActionDefinitions.CreateExisting((Expression<Func<CRCaseMaint, PXAction<CRCase>>>) (g => g.Close), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured) a.WithFieldAssignments((Action<BoundedTo<CRCaseMaint, CRCase>.Assignment.IContainerFillerFields>) (fields =>
    {
      fields.Add<CRCase.isActive>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) false)));
      fields.Add<CRCase.resolutionDate>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromNow()));
      fields.Add<CRCase.resolution>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromFormField(formClose, "Reason")));
      fields.Add<CRCase.solutionActivityNoteID>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromFormField(formClose, "SolutionActivityNoteID")));
      fields.Add<CRCase.closureNotes>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromFormField(formClose, "ClosureNotes")));
    })).DisplayName("Close").WithCategory(categoryProcessing).PlaceAfter(gactionTakeCase).WithForm(formClose).MapEnableToUpdate().WithPersistOptions((ActionPersistOptions) 2).IsExposedToMobile(true).MassProcessingScreen<UpdateCaseMassProcess>()));
    BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured actionPending = context.ActionDefinitions.CreateExisting((Expression<Func<CRCaseMaint, PXAction<CRCase>>>) (g => g.PendingCustomer), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured) a.WithFieldAssignments((Action<BoundedTo<CRCaseMaint, CRCase>.Assignment.IContainerFillerFields>) (fields =>
    {
      fields.Add<CRCase.isActive>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) true)));
      fields.Add<CRCase.resolutionDate>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) null)));
      fields.Add<CRCase.resolution>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromFormField(formPendingCustomer, "Reason")));
      fields.Add<CRCase.solutionActivityNoteID>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromFormField(formPendingCustomer, "SolutionActivityNoteID")));
    })).DisplayName("Pending Customer").WithCategory(categoryProcessing).PlaceAfter(actionClose).WithForm(formPendingCustomer).MapEnableToUpdate().WithPersistOptions((ActionPersistOptions) 2).IsExposedToMobile(true).MassProcessingScreen<UpdateCaseMassProcess>()));
    BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured gactionRelease = context.ActionDefinitions.CreateExisting((Expression<Func<CRCaseMaint, PXAction<CRCase>>>) (g => g.release), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured) a.WithCategory(categoryProcessing).PlaceAfter(actionPending).WithPersistOptions((ActionPersistOptions) 2).MassProcessingScreen<CRCaseReleaseProcess>()));
    BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured gactionAssign = context.ActionDefinitions.CreateExisting((Expression<Func<CRCaseMaint, PXAction<CRCase>>>) (g => g.assign), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured) a.WithCategory(categoryOther)));
    BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured gactionViewInvoice = context.ActionDefinitions.CreateExisting((Expression<Func<CRCaseMaint, PXAction<CRCase>>>) (g => g.viewInvoice), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured) a.WithCategory(categoryOther).WithPersistOptions((ActionPersistOptions) 2)));
    BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured gactionOpenFromPortal = context.ActionDefinitions.CreateExisting<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (g => g.openCaseFromPortal), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured) a.WithCategory(categoryOther).DisplayName("Open Case in Portal").WithPersistOptions((ActionPersistOptions) 2)));
    BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured gactionOpenFromProcessing = context.ActionDefinitions.CreateExisting<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (g => g.openCaseFromProcessing), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured) a.WithCategory(categoryOther).DisplayName("Reopen Case from Email").WithPersistOptions((ActionPersistOptions) 2)));
    BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured gactionCloseFromPortal = context.ActionDefinitions.CreateExisting<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (g => g.closeCaseFromPortal), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured) a.WithCategory(categoryOther).DisplayName("Close Case from Portal").WithPersistOptions((ActionPersistOptions) 2)));
    BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured actionCreateEmail = context.ActionDefinitions.CreateExisting("newMailActivity", (Func<BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured) a.WithCategory(categoryActivities)));
    BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured actionCreateWorkItem = context.ActionDefinitions.CreateExisting("NewActivityW_Workflow", (Func<BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured) a.WithCategory(categoryActivities)));
    BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured actionCreateNote = context.ActionDefinitions.CreateExisting("NewActivityN_Workflow", (Func<BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured) a.WithCategory(categoryActivities)));
    BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured actionCreateTask = context.ActionDefinitions.CreateExisting("NewTask", (Func<BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured) a.WithCategory(categoryActivities)));
    BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured actionCreatePhoneCall = context.ActionDefinitions.CreateExisting("NewActivityP_Workflow", (Func<BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured) a.WithCategory(categoryActivities)));
    BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured actionCreateReturnOrder = context.ActionDefinitions.CreateExisting<CRCaseMaint_CRCreateReturnOrder>((Expression<Func<CRCaseMaint_CRCreateReturnOrder, PXAction<CRCase>>>) (g => g.CreateReturnOrder), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IConfigured) a.WithCategory(categoryServices)));
    context.AddScreenConfigurationFor((Func<BoundedTo<CRCaseMaint, CRCase>.ScreenConfiguration.IStartConfigScreen, BoundedTo<CRCaseMaint, CRCase>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<CRCaseMaint, CRCase>.ScreenConfiguration.IConfigured) ((BoundedTo<CRCaseMaint, CRCase>.ScreenConfiguration.INeedStateIDScreen) screen).StateIdentifierIs<CRCase.status>().AddDefaultFlow(new Func<BoundedTo<CRCaseMaint, CRCase>.Workflow.INeedStatesFlow, BoundedTo<CRCaseMaint, CRCase>.Workflow.IConfigured>(DefaultCaseFlow)).WithActions((Action<BoundedTo<CRCaseMaint, CRCase>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add(gactionTakeCase);
      actions.Add(actionOpen);
      actions.Add(actionPending);
      actions.Add(actionClose);
      actions.Add(gactionRelease);
      actions.Add(actionCreateEmail);
      actions.Add(actionCreateWorkItem);
      actions.Add(actionCreateNote);
      actions.Add(actionCreateTask);
      actions.Add(actionCreatePhoneCall);
      actions.Add(gactionAssign);
      actions.Add(gactionViewInvoice);
      actions.Add(gactionOpenFromPortal);
      actions.Add(gactionOpenFromProcessing);
      actions.Add(gactionCloseFromPortal);
      actions.Add(actionCreateReturnOrder);
    })).WithForms((Action<BoundedTo<CRCaseMaint, CRCase>.Form.IContainerForms>) (forms =>
    {
      forms.Add(formOpen);
      forms.Add(formPendingCustomer);
      forms.Add(formClose);
    })).WithFieldStates((Action<BoundedTo<CRCaseMaint, CRCase>.DynamicFieldState.IContainerFillerFields>) (fields =>
    {
      fields.Add<CRCase.resolution>((Func<BoundedTo<CRCaseMaint, CRCase>.DynamicFieldState.INeedAnyConfigField, BoundedTo<CRCaseMaint, CRCase>.DynamicFieldState.IConfigured>) (field => (BoundedTo<CRCaseMaint, CRCase>.DynamicFieldState.IConfigured) field.SetComboValues(new (string, string)[17]
      {
        ("RJ", "Rejected"),
        ("RD", "Resolved"),
        ("MI", "More Info Requested"),
        ("IP", "In Process"),
        ("IN", "Internal"),
        ("ES", "In Escalation"),
        ("DP", "Duplicate"),
        ("CR", "Solution Provided"),
        ("CP", "Customer Postpone"),
        ("CL", "Canceled"),
        ("CC", "Pending Closure"),
        ("CA", "Abandoned"),
        ("AS", "Unassigned"),
        ("AA", "Assigned"),
        ("AD", "Updated"),
        ("PC", "Closed on Portal"),
        ("PO", "Opened on Portal")
      })));
      fields.Add<CRCase.resolutionDate>((Func<BoundedTo<CRCaseMaint, CRCase>.DynamicFieldState.INeedAnyConfigField, BoundedTo<CRCaseMaint, CRCase>.DynamicFieldState.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.DynamicFieldState.IConfigured) f.IsHiddenWhen((BoundedTo<CRCaseMaint, CRCase>.ISharedCondition) conditions.IsActive)));
    })).WithCategories((Action<BoundedTo<CRCaseMaint, CRCase>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(categoryProcessing);
      categories.Add(categoryServices);
      categories.Add(categoryActivities);
      categories.Add(categoryOther);
    }))));

    static void AddResolutionFormField(
      BoundedTo<CRCaseMaint, CRCase>.FormField.IContainerFillerFields fields,
      string defaultValue)
    {
      fields.Add("Reason", (Func<BoundedTo<CRCaseMaint, CRCase>.FormField.INeedTypeConfigField, BoundedTo<CRCaseMaint, CRCase>.FormField.IConfigured>) (field => (BoundedTo<CRCaseMaint, CRCase>.FormField.IConfigured) field.WithSchemaOf<CRCase.resolution>().IsRequired().Prompt("Reason").ComboBoxValuesSource((ComboBoxValuesSource) 1).DefaultValue((object) defaultValue)));
    }

    static void DisableFieldsForFinalStates(
      BoundedTo<CRCaseMaint, CRCase>.FieldState.IContainerFillerFields fields)
    {
      fields.AddTable<CRCase>((Func<BoundedTo<CRCaseMaint, CRCase>.FieldState.INeedAnyConfigField, BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured>) (field => (BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured) field.IsDisabled()));
      fields.AddTable<CRPMTimeActivity>((Func<BoundedTo<CRCaseMaint, CRCase>.FieldState.INeedAnyConfigField, BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured>) (field => (BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured) field.IsDisabled()));
      fields.AddField<CRCase.caseCD>((Func<BoundedTo<CRCaseMaint, CRCase>.FieldState.INeedAnyConfigField, BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured>) null);
      fields.AddField<CRCase.closureNotes>((Func<BoundedTo<CRCaseMaint, CRCase>.FieldState.INeedAnyConfigField, BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured>) null);
    }

    static void DisableFieldsAndAttributesForFinalStates(
      BoundedTo<CRCaseMaint, CRCase>.FieldState.IContainerFillerFields fields)
    {
      DisableFieldsForFinalStates(fields);
      fields.AddTable<CSAnswers>((Func<BoundedTo<CRCaseMaint, CRCase>.FieldState.INeedAnyConfigField, BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured>) (field => (BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured) field.IsDisabled()));
    }

    void AddSolutionActivityNoteIDFormField(
      BoundedTo<CRCaseMaint, CRCase>.FormField.IContainerFillerFields fields)
    {
      fields.Add("SolutionActivityNoteID", (Func<BoundedTo<CRCaseMaint, CRCase>.FormField.INeedTypeConfigField, BoundedTo<CRCaseMaint, CRCase>.FormField.IConfigured>) (field => (BoundedTo<CRCaseMaint, CRCase>.FormField.IConfigured) field.WithSchemaOf<CRCase.solutionActivityNoteID>().DefaultValueFromSchemaField().IsHiddenWhen((BoundedTo<CRCaseMaint, CRCase>.ISharedCondition) BoundedTo<CRCaseMaint, CRCase>.Condition.op_LogicalNot(conditions.TrackSolutionsInActivities)).Prompt("Solution Provided In")));
    }

    BoundedTo<CRCaseMaint, CRCase>.Workflow.IConfigured DefaultCaseFlow(
      BoundedTo<CRCaseMaint, CRCase>.Workflow.INeedStatesFlow flow)
    {
      BoundedTo<CRCaseMaint, CRCase>.BaseFlowStep.IConfigured newState = context.FlowStates.Create("N", (Func<BoundedTo<CRCaseMaint, CRCase>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CRCaseMaint, CRCase>.BaseFlowStep.IConfigured>) (state => (BoundedTo<CRCaseMaint, CRCase>.BaseFlowStep.IConfigured) ((BoundedTo<CRCaseMaint, CRCase>.FlowState.INeedAnyFlowStateConfig) ((BoundedTo<CRCaseMaint, CRCase>.FlowState.INeedAnyFlowStateConfig) state.IsInitial()).WithFieldStates((Action<BoundedTo<CRCaseMaint, CRCase>.FieldState.IContainerFillerFields>) (fields =>
      {
        fields.AddField<CRCase.resolution>((Func<BoundedTo<CRCaseMaint, CRCase>.FieldState.INeedAnyConfigField, BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured>) (field => (BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured) ((BoundedTo<CRCaseMaint, CRCase>.FieldState.INeedAnyConfigField) field.DefaultValue((object) "AS")).ComboBoxValues(((IEnumerable<string>) reasons["N"]).Union<string>((IEnumerable<string>) new string[1]
        {
          "PO"
        }).ToArray<string>())));
        fields.AddField<CRCase.isActive>((Func<BoundedTo<CRCaseMaint, CRCase>.FieldState.INeedAnyConfigField, BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured>) (field => (BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured) field.IsDisabled()));
      }))).WithActions((Action<BoundedTo<CRCaseMaint, CRCase>.ActionState.IContainerFillerActions>) (actions =>
      {
        AddOpenAction(actions, true, true);
        actions.Add((Expression<Func<CRCaseMaint, PXAction<CRCase>>>) (g => g.takeCase), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionState.IAllowOptionalConfig, BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<CRCaseMaint, PXAction<CRCase>>>) (g => g.assign), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionState.IAllowOptionalConfig, BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured>) null);
        AddPendingCustomerAction(actions);
        AddCloseAction(actions);
        actions.Add<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (g => g.openCaseFromPortal), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionState.IAllowOptionalConfig, BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured>) null);
        actions.Add<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (g => g.closeCaseFromPortal), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionState.IAllowOptionalConfig, BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured>) null);
      }))));
      BoundedTo<CRCaseMaint, CRCase>.BaseFlowStep.IConfigured openState = context.FlowStates.Create("O", (Func<BoundedTo<CRCaseMaint, CRCase>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CRCaseMaint, CRCase>.BaseFlowStep.IConfigured>) (state => (BoundedTo<CRCaseMaint, CRCase>.BaseFlowStep.IConfigured) ((BoundedTo<CRCaseMaint, CRCase>.FlowState.INeedAnyFlowStateConfig) state.WithFieldStates((Action<BoundedTo<CRCaseMaint, CRCase>.FieldState.IContainerFillerFields>) (fields =>
      {
        fields.AddField<CRCase.resolution>((Func<BoundedTo<CRCaseMaint, CRCase>.FieldState.INeedAnyConfigField, BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured>) (field => (BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured) field.ComboBoxValues(((IEnumerable<string>) reasons["O"]).Union<string>((IEnumerable<string>) new string[1]
        {
          "PO"
        }).ToArray<string>())));
        fields.AddField<CRCase.isActive>((Func<BoundedTo<CRCaseMaint, CRCase>.FieldState.INeedAnyConfigField, BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured>) (field => (BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured) field.IsDisabled()));
      }))).WithActions((Action<BoundedTo<CRCaseMaint, CRCase>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<CRCaseMaint, PXAction<CRCase>>>) (g => g.takeCase), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionState.IAllowOptionalConfig, BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        actions.Add((Expression<Func<CRCaseMaint, PXAction<CRCase>>>) (g => g.assign), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionState.IAllowOptionalConfig, BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured>) null);
        AddPendingCustomerAction(actions);
        AddCloseAction(actions, true, true);
        actions.Add<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (g => g.openCaseFromProcessing), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionState.IAllowOptionalConfig, BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured>) null);
        actions.Add<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (g => g.openCaseFromPortal), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionState.IAllowOptionalConfig, BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured>) null);
        actions.Add<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (g => g.closeCaseFromPortal), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionState.IAllowOptionalConfig, BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured>) null);
      }))));
      BoundedTo<CRCaseMaint, CRCase>.BaseFlowStep.IConfigured pendingState = context.FlowStates.Create("P", (Func<BoundedTo<CRCaseMaint, CRCase>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CRCaseMaint, CRCase>.BaseFlowStep.IConfigured>) (state => (BoundedTo<CRCaseMaint, CRCase>.BaseFlowStep.IConfigured) ((BoundedTo<CRCaseMaint, CRCase>.FlowState.INeedAnyFlowStateConfig) state.WithFieldStates((Action<BoundedTo<CRCaseMaint, CRCase>.FieldState.IContainerFillerFields>) (fields =>
      {
        fields.AddField<CRCase.resolution>((Func<BoundedTo<CRCaseMaint, CRCase>.FieldState.INeedAnyConfigField, BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured>) (field => (BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured) ((BoundedTo<CRCaseMaint, CRCase>.FieldState.INeedAnyConfigField) field.ComboBoxValues(reasons["P"])).IsDisabled()));
        fields.AddField<CRCase.isActive>((Func<BoundedTo<CRCaseMaint, CRCase>.FieldState.INeedAnyConfigField, BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured>) (field => (BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured) field.IsDisabled()));
      }))).WithActions((Action<BoundedTo<CRCaseMaint, CRCase>.ActionState.IContainerFillerActions>) (actions =>
      {
        actions.Add((Expression<Func<CRCaseMaint, PXAction<CRCase>>>) (g => g.takeCase), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionState.IAllowOptionalConfig, BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured) a.IsDuplicatedInToolbar()));
        AddOpenAction(actions);
        AddCloseAction(actions, true);
        actions.Add<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (g => g.openCaseFromProcessing), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionState.IAllowOptionalConfig, BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured>) null);
        actions.Add<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (g => g.openCaseFromPortal), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionState.IAllowOptionalConfig, BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured>) null);
        actions.Add<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (g => g.closeCaseFromPortal), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionState.IAllowOptionalConfig, BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured>) null);
      }))));
      BoundedTo<CRCaseMaint, CRCase>.BaseFlowStep.IConfigured closedState = context.FlowStates.Create("C", (Func<BoundedTo<CRCaseMaint, CRCase>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CRCaseMaint, CRCase>.BaseFlowStep.IConfigured>) (state => (BoundedTo<CRCaseMaint, CRCase>.BaseFlowStep.IConfigured) ((BoundedTo<CRCaseMaint, CRCase>.FlowState.INeedAnyFlowStateConfig) state.WithFieldStates((Action<BoundedTo<CRCaseMaint, CRCase>.FieldState.IContainerFillerFields>) (fields =>
      {
        fields.AddField<CRCase.resolution>((Func<BoundedTo<CRCaseMaint, CRCase>.FieldState.INeedAnyConfigField, BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured>) (field => (BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured) ((BoundedTo<CRCaseMaint, CRCase>.FieldState.INeedAnyConfigField) field.ComboBoxValues(((IEnumerable<string>) reasons["C"]).Union<string>((IEnumerable<string>) new string[1]
        {
          "PC"
        }).ToArray<string>())).IsDisabled()));
        DisableFieldsAndAttributesForFinalStates(fields);
        fields.AddField<CRCase.isBillable>((Func<BoundedTo<CRCaseMaint, CRCase>.FieldState.INeedAnyConfigField, BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured>) null);
        fields.AddField<CRCase.manualBillableTimes>((Func<BoundedTo<CRCaseMaint, CRCase>.FieldState.INeedAnyConfigField, BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured>) null);
        fields.AddField<CRCase.timeBillable>((Func<BoundedTo<CRCaseMaint, CRCase>.FieldState.INeedAnyConfigField, BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured>) null);
        fields.AddField<CRCase.overtimeBillable>((Func<BoundedTo<CRCaseMaint, CRCase>.FieldState.INeedAnyConfigField, BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured>) null);
      }))).WithActions((Action<BoundedTo<CRCaseMaint, CRCase>.ActionState.IContainerFillerActions>) (actions =>
      {
        AddOpenAction(actions);
        actions.Add((Expression<Func<CRCaseMaint, PXAction<CRCase>>>) (g => g.release), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionState.IAllowOptionalConfig, BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured>) (action => (BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured) action.IsDuplicatedInToolbar().WithConnotation((ActionConnotation) 3)));
        actions.Add<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (g => g.openCaseFromProcessing), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionState.IAllowOptionalConfig, BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured>) null);
        actions.Add<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (g => g.openCaseFromPortal), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionState.IAllowOptionalConfig, BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured>) null);
      }))));
      BoundedTo<CRCaseMaint, CRCase>.BaseFlowStep.IConfigured releasedState = context.FlowStates.Create("R", (Func<BoundedTo<CRCaseMaint, CRCase>.FlowState.INeedAnyFlowStateConfig, BoundedTo<CRCaseMaint, CRCase>.BaseFlowStep.IConfigured>) (state => (BoundedTo<CRCaseMaint, CRCase>.BaseFlowStep.IConfigured) ((BoundedTo<CRCaseMaint, CRCase>.FlowState.INeedAnyFlowStateConfig) state.WithFieldStates((Action<BoundedTo<CRCaseMaint, CRCase>.FieldState.IContainerFillerFields>) (fields =>
      {
        fields.AddField<CRCase.resolution>((Func<BoundedTo<CRCaseMaint, CRCase>.FieldState.INeedAnyConfigField, BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured>) (field => (BoundedTo<CRCaseMaint, CRCase>.FieldState.IConfigured) ((BoundedTo<CRCaseMaint, CRCase>.FieldState.INeedAnyConfigField) field.ComboBoxValues(reasons["R"])).IsDisabled()));
        DisableFieldsAndAttributesForFinalStates(fields);
      }))).WithActions((Action<BoundedTo<CRCaseMaint, CRCase>.ActionState.IContainerFillerActions>) (actions => actions.Add((Expression<Func<CRCaseMaint, PXAction<CRCase>>>) (g => g.viewInvoice), (Func<BoundedTo<CRCaseMaint, CRCase>.ActionState.IAllowOptionalConfig, BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured>) null)))));
      return (BoundedTo<CRCaseMaint, CRCase>.Workflow.IConfigured) flow.WithFlowStates((Action<BoundedTo<CRCaseMaint, CRCase>.BaseFlowStep.IContainerFillerStates>) (states =>
      {
        states.Add(newState);
        states.Add(openState);
        states.Add(pendingState);
        states.Add(closedState);
        states.Add(releasedState);
      })).WithTransitions((Action<BoundedTo<CRCaseMaint, CRCase>.Transition.IContainerFillerTransitions>) (transitions =>
      {
        transitions.Add((Func<BoundedTo<CRCaseMaint, CRCase>.Transition.INeedSource, BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured>) (transition => (BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured) transition.From(newState).To(newState).IsTriggeredOn(gactionTakeCase).WithFieldAssignments((Action<BoundedTo<CRCaseMaint, CRCase>.Assignment.IContainerFillerFields>) (fields => fields.Add<CRCase.resolution>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) "AA"))))).DoesNotPersist()));
        transitions.Add((Func<BoundedTo<CRCaseMaint, CRCase>.Transition.INeedSource, BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured>) (transition => (BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured) transition.From(newState).To(openState).IsTriggeredOn(actionOpen)));
        transitions.Add((Func<BoundedTo<CRCaseMaint, CRCase>.Transition.INeedSource, BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured>) (transition => (BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured) transition.From(newState).To(pendingState).IsTriggeredOn(actionPending)));
        transitions.Add((Func<BoundedTo<CRCaseMaint, CRCase>.Transition.INeedSource, BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured>) (transition => (BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured) transition.From(newState).To(closedState).IsTriggeredOn(actionClose)));
        transitions.Add((Func<BoundedTo<CRCaseMaint, CRCase>.Transition.INeedSource, BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured>) (transition => (BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured) transition.From(newState).To(openState).IsTriggeredOn<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (e => e.openCaseFromPortal)).WithFieldAssignments((Action<BoundedTo<CRCaseMaint, CRCase>.Assignment.IContainerFillerFields>) (fields =>
        {
          fields.Add<CRCase.isActive>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) true)));
          fields.Add<CRCase.resolution>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) "PO")));
        }))));
        transitions.Add((Func<BoundedTo<CRCaseMaint, CRCase>.Transition.INeedSource, BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured>) (transition => (BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured) transition.From(newState).To(closedState).IsTriggeredOn<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (e => e.closeCaseFromPortal)).WithFieldAssignments((Action<BoundedTo<CRCaseMaint, CRCase>.Assignment.IContainerFillerFields>) (fields =>
        {
          fields.Add<CRCase.isActive>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) false)));
          fields.Add<CRCase.resolution>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) "PC")));
        }))));
        transitions.Add((Func<BoundedTo<CRCaseMaint, CRCase>.Transition.INeedSource, BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured>) (transition => (BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured) transition.From(openState).To(openState).IsTriggeredOn(gactionTakeCase).WithFieldAssignments((Action<BoundedTo<CRCaseMaint, CRCase>.Assignment.IContainerFillerFields>) (fields => fields.Add<CRCase.resolution>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) "AA"))))).DoesNotPersist()));
        transitions.Add((Func<BoundedTo<CRCaseMaint, CRCase>.Transition.INeedSource, BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured>) (transition => (BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured) transition.From(openState).To(pendingState).IsTriggeredOn(actionPending)));
        transitions.Add((Func<BoundedTo<CRCaseMaint, CRCase>.Transition.INeedSource, BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured>) (transition => (BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured) transition.From(openState).To(closedState).IsTriggeredOn(actionClose)));
        transitions.Add((Func<BoundedTo<CRCaseMaint, CRCase>.Transition.INeedSource, BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured>) (transition => (BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured) transition.From(openState).To(openState).IsTriggeredOn<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (e => e.openCaseFromProcessing)).WithFieldAssignments((Action<BoundedTo<CRCaseMaint, CRCase>.Assignment.IContainerFillerFields>) (fields =>
        {
          fields.Add<CRCase.isActive>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) true)));
          fields.Add<CRCase.resolution>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) "AD")));
        }))));
        transitions.Add((Func<BoundedTo<CRCaseMaint, CRCase>.Transition.INeedSource, BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured>) (transition => (BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured) transition.From(openState).To(openState).IsTriggeredOn<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (e => e.openCaseFromPortal)).WithFieldAssignments((Action<BoundedTo<CRCaseMaint, CRCase>.Assignment.IContainerFillerFields>) (fields =>
        {
          fields.Add<CRCase.isActive>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) true)));
          fields.Add<CRCase.resolution>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) "PO")));
        }))));
        transitions.Add((Func<BoundedTo<CRCaseMaint, CRCase>.Transition.INeedSource, BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured>) (transition => (BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured) transition.From(openState).To(closedState).IsTriggeredOn<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (e => e.closeCaseFromPortal)).WithFieldAssignments((Action<BoundedTo<CRCaseMaint, CRCase>.Assignment.IContainerFillerFields>) (fields =>
        {
          fields.Add<CRCase.isActive>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) false)));
          fields.Add<CRCase.resolution>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) "PC")));
        }))));
        transitions.Add((Func<BoundedTo<CRCaseMaint, CRCase>.Transition.INeedSource, BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured>) (transition => (BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured) transition.From(pendingState).To(openState).IsTriggeredOn(actionOpen).WithFieldAssignments((Action<BoundedTo<CRCaseMaint, CRCase>.Assignment.IContainerFillerFields>) (fields => fields.Add<CRCase.solutionActivityNoteID>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromExpression("=null")))))));
        transitions.Add((Func<BoundedTo<CRCaseMaint, CRCase>.Transition.INeedSource, BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured>) (transition => (BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured) transition.From(pendingState).To(closedState).IsTriggeredOn(actionClose)));
        transitions.Add((Func<BoundedTo<CRCaseMaint, CRCase>.Transition.INeedSource, BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured>) (transition => (BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured) transition.From(pendingState).To(openState).IsTriggeredOn<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (e => e.openCaseFromProcessing)).WithFieldAssignments((Action<BoundedTo<CRCaseMaint, CRCase>.Assignment.IContainerFillerFields>) (fields =>
        {
          fields.Add<CRCase.isActive>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) true)));
          fields.Add<CRCase.resolution>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) "AD")));
          fields.Add<CRCase.solutionActivityNoteID>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromExpression("=null")));
        }))));
        transitions.Add((Func<BoundedTo<CRCaseMaint, CRCase>.Transition.INeedSource, BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured>) (transition => (BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured) transition.From(pendingState).To(openState).IsTriggeredOn<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (e => e.openCaseFromPortal)).WithFieldAssignments((Action<BoundedTo<CRCaseMaint, CRCase>.Assignment.IContainerFillerFields>) (fields =>
        {
          fields.Add<CRCase.isActive>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) true)));
          fields.Add<CRCase.resolution>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) "PO")));
          fields.Add<CRCase.solutionActivityNoteID>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromExpression("=null")));
        }))));
        transitions.Add((Func<BoundedTo<CRCaseMaint, CRCase>.Transition.INeedSource, BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured>) (transition => (BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured) transition.From(pendingState).To(closedState).IsTriggeredOn<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (e => e.closeCaseFromPortal)).WithFieldAssignments((Action<BoundedTo<CRCaseMaint, CRCase>.Assignment.IContainerFillerFields>) (fields =>
        {
          fields.Add<CRCase.isActive>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) false)));
          fields.Add<CRCase.resolution>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) "PC")));
        }))));
        transitions.Add((Func<BoundedTo<CRCaseMaint, CRCase>.Transition.INeedSource, BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured>) (transition => (BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured) transition.From(closedState).To(openState).IsTriggeredOn(actionOpen).WithFieldAssignments((Action<BoundedTo<CRCaseMaint, CRCase>.Assignment.IContainerFillerFields>) (fields => fields.Add<CRCase.solutionActivityNoteID>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromExpression("=null")))))));
        transitions.Add((Func<BoundedTo<CRCaseMaint, CRCase>.Transition.INeedSource, BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured>) (transition => (BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured) transition.From(closedState).To(releasedState).IsTriggeredOn((Expression<Func<CRCaseMaint, PXAction<CRCase>>>) (g => g.release)).WithFieldAssignments((Action<BoundedTo<CRCaseMaint, CRCase>.Assignment.IContainerFillerFields>) (fields => fields.Add<CRCase.isActive>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) false)))))));
        transitions.Add((Func<BoundedTo<CRCaseMaint, CRCase>.Transition.INeedSource, BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured>) (transition => (BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured) transition.From(closedState).To(openState).IsTriggeredOn<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (e => e.openCaseFromProcessing)).WithFieldAssignments((Action<BoundedTo<CRCaseMaint, CRCase>.Assignment.IContainerFillerFields>) (fields =>
        {
          fields.Add<CRCase.isActive>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) true)));
          fields.Add<CRCase.resolution>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) "AD")));
          fields.Add<CRCase.solutionActivityNoteID>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromExpression("=null")));
        }))));
        transitions.Add((Func<BoundedTo<CRCaseMaint, CRCase>.Transition.INeedSource, BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured>) (transition => (BoundedTo<CRCaseMaint, CRCase>.Transition.IConfigured) transition.From(closedState).To(openState).IsTriggeredOn<CaseWorkflow>((Expression<Func<CaseWorkflow, PXAction<CRCase>>>) (e => e.openCaseFromPortal)).WithFieldAssignments((Action<BoundedTo<CRCaseMaint, CRCase>.Assignment.IContainerFillerFields>) (fields =>
        {
          fields.Add<CRCase.isActive>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) true)));
          fields.Add<CRCase.resolution>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromValue((object) "PO")));
          fields.Add<CRCase.solutionActivityNoteID>((Func<BoundedTo<CRCaseMaint, CRCase>.Assignment.INeedRightOperand, BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured>) (f => (BoundedTo<CRCaseMaint, CRCase>.Assignment.IConfigured) f.SetFromExpression("=null")));
        }))));
      }));
    }

    void AddOpenAction(
      BoundedTo<CRCaseMaint, CRCase>.ActionState.IContainerFillerActions filler,
      bool isDuplicationInToolbar = false,
      bool withConnotation = false)
    {
      filler.Add(actionOpen, (Func<BoundedTo<CRCaseMaint, CRCase>.ActionState.IAllowOptionalConfig, BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured) a.IsDuplicatedInToolbar<CRCaseMaint, CRCase>(isDuplicationInToolbar).WithSuccessConnotation(withConnotation)));
    }

    void AddPendingCustomerAction(
      BoundedTo<CRCaseMaint, CRCase>.ActionState.IContainerFillerActions filler,
      bool isDuplicationInToolbar = false)
    {
      filler.Add(actionPending, (Func<BoundedTo<CRCaseMaint, CRCase>.ActionState.IAllowOptionalConfig, BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured) a.IsDuplicatedInToolbar<CRCaseMaint, CRCase>(isDuplicationInToolbar)));
    }

    void AddCloseAction(
      BoundedTo<CRCaseMaint, CRCase>.ActionState.IContainerFillerActions filler,
      bool isDuplicationInToolbar = false,
      bool withConnotation = false)
    {
      filler.Add(actionClose, (Func<BoundedTo<CRCaseMaint, CRCase>.ActionState.IAllowOptionalConfig, BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured>) (a => (BoundedTo<CRCaseMaint, CRCase>.ActionState.IConfigured) a.IsDuplicatedInToolbar<CRCaseMaint, CRCase>(isDuplicationInToolbar).WithSuccessConnotation(withConnotation)));
    }
  }

  public class Conditions : BoundedTo<CRCaseMaint, CRCase>.Condition.Pack
  {
    public BoundedTo<CRCaseMaint, CRCase>.Condition IsActive
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CRCaseMaint, CRCase>.Condition.ConditionBuilder, BoundedTo<CRCaseMaint, CRCase>.Condition>) (b => b.FromBql<BqlOperand<CRCase.isActive, IBqlBool>.IsEqual<True>>()), nameof (IsActive));
      }
    }

    public BoundedTo<CRCaseMaint, CRCase>.Condition IsClosureNotesRequired
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CRCaseMaint, CRCase>.Condition.ConditionBuilder, BoundedTo<CRCaseMaint, CRCase>.Condition>) (b => b.FromBql<BqlOperand<Current<CRCaseClass.requireClosureNotes>, IBqlBool>.IsEqual<True>>()), nameof (IsClosureNotesRequired));
      }
    }

    public BoundedTo<CRCaseMaint, CRCase>.Condition TrackSolutionsInActivities
    {
      get
      {
        return this.GetOrCreate((Func<BoundedTo<CRCaseMaint, CRCase>.Condition.ConditionBuilder, BoundedTo<CRCaseMaint, CRCase>.Condition>) (b => b.FromBql<BqlOperand<Current<CRCaseClass.trackSolutionsInActivities>, IBqlBool>.IsEqual<True>>()), nameof (TrackSolutionsInActivities));
      }
    }
  }

  /// <summary>
  /// Statuses for <see cref="T:PX.Objects.CR.CROpportunity.status" /> used by default in system workflow.
  /// Values could be changed and extended by workflow.
  /// </summary>
  public static class States
  {
    public const string New = "N";
    public const string Open = "O";
    public const string Closed = "C";
    public const string Released = "R";
    public const string PendingCustomer = "P";

    internal class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[5]{ "N", "O", "C", "R", "P" }, new string[5]
        {
          "New",
          "Open",
          "Closed",
          "Released",
          "Pending Customer"
        })
      {
      }
    }
  }

  public static class Reasons
  {
    public const string Rejected = "RJ";
    public const string Resolved = "RD";
    public const string MoreInfoRequested = "MI";
    public const string InProcess = "IP";
    public const string Internal = "IN";
    public const string InEscalation = "ES";
    public const string Duplicate = "DP";
    public const string SolutionProvided = "CR";
    public const string CustomerPostpone = "CP";
    public const string Canceled = "CL";
    public const string PendingClosure = "CC";
    public const string Abandoned = "CA";
    public const string Unassigned = "AS";
    public const string Assigned = "AA";
    public const string Updated = "AD";
    public const string ClosedOnPortal = "PC";
    public const string OpenedOnPortal = "PO";

    public static class Messages
    {
      public const string Rejected = "Rejected";
      public const string Resolved = "Resolved";
      public const string MoreInfoRequested = "More Info Requested";
      public const string InProcess = "In Process";
      public const string Internal = "Internal";
      public const string InEscalation = "In Escalation";
      public const string Duplicate = "Duplicate";
      public const string SolutionProvided = "Solution Provided";
      public const string CustomerPostpone = "Customer Postpone";
      public const string Canceled = "Canceled";
      public const string PendingClosure = "Pending Closure";
      public const string Abandoned = "Abandoned";
      public const string Unassigned = "Unassigned";
      public const string Assigned = "Assigned";
      public const string Updated = "Updated";
      public const string ClosedOnPortal = "Closed on Portal";
      public const string OpenedOnPortal = "Opened on Portal";
    }
  }

  private static class FieldNames
  {
    public const string Reason = "Reason";
    public const string Owner = "Owner";
    public const string SolutionActivityNoteID = "SolutionActivityNoteID";
    public const string ClosureNotes = "ClosureNotes";
  }

  public static class CategoryNames
  {
    public const string Processing = "Processing";
    public const string Services = "CustomerServices";
    public const string Activities = "Activities";
    public const string Other = "Other";
  }

  public static class CategoryDisplayNames
  {
    public const string Processing = "Processing";
    public const string Services = "Customer Services";
    public const string Activities = "Activities";
    public const string Other = "Other";
  }
}
