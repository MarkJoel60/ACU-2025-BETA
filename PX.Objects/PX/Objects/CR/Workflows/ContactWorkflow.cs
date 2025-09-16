// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Workflows.ContactWorkflow
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

public class ContactWorkflow : PXGraphExtension<ContactMaint>
{
  public static bool IsActive() => false;

  public virtual void Configure(PXScreenConfiguration configuration)
  {
    ContactWorkflow.Configure(configuration.GetScreenConfigurationContext<ContactMaint, Contact>());
  }

  protected static void Configure(WorkflowContext<ContactMaint, Contact> config)
  {
    var conditions = new
    {
      IsContactActive = config.Conditions.FromBql<BqlOperand<Contact.status, IBqlString>.IsNotEqual<ContactStatus.active>>()
    }.AutoNameConditions();
    BoundedTo<ContactMaint, Contact>.ActionCategory.IConfigured categoryRecordCreation = config.Categories.CreateNew("RecordCreation", (Func<BoundedTo<ContactMaint, Contact>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ContactMaint, Contact>.ActionCategory.IConfigured>) (category => (BoundedTo<ContactMaint, Contact>.ActionCategory.IConfigured) category.DisplayName("Record Creation")));
    BoundedTo<ContactMaint, Contact>.ActionCategory.IConfigured categoryActivities = config.Categories.CreateNew("Activities", (Func<BoundedTo<ContactMaint, Contact>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ContactMaint, Contact>.ActionCategory.IConfigured>) (category => (BoundedTo<ContactMaint, Contact>.ActionCategory.IConfigured) category.DisplayName("Activities")));
    BoundedTo<ContactMaint, Contact>.ActionCategory.IConfigured categoryValidation = config.Categories.CreateNew("Validation", (Func<BoundedTo<ContactMaint, Contact>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<ContactMaint, Contact>.ActionCategory.IConfigured>) (category => (BoundedTo<ContactMaint, Contact>.ActionCategory.IConfigured) category.DisplayName("Validation")));
    BoundedTo<ContactMaint, Contact>.ActionDefinition.IConfigured actionCreatePhoneCall = config.ActionDefinitions.CreateExisting("NewActivityP_Workflow", (Func<BoundedTo<ContactMaint, Contact>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContactMaint, Contact>.ActionDefinition.IConfigured>) (a => (BoundedTo<ContactMaint, Contact>.ActionDefinition.IConfigured) a.WithCategory(categoryActivities)));
    BoundedTo<ContactMaint, Contact>.ActionDefinition.IConfigured actionCreateNote = config.ActionDefinitions.CreateExisting("NewActivityN_Workflow", (Func<BoundedTo<ContactMaint, Contact>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContactMaint, Contact>.ActionDefinition.IConfigured>) (a => (BoundedTo<ContactMaint, Contact>.ActionDefinition.IConfigured) a.WithCategory(categoryActivities)));
    BoundedTo<ContactMaint, Contact>.ActionDefinition.IConfigured actionCreateMail = config.ActionDefinitions.CreateExisting("newMailActivity", (Func<BoundedTo<ContactMaint, Contact>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContactMaint, Contact>.ActionDefinition.IConfigured>) (a => (BoundedTo<ContactMaint, Contact>.ActionDefinition.IConfigured) a.WithCategory(categoryActivities)));
    BoundedTo<ContactMaint, Contact>.ActionDefinition.IConfigured actionCreateTask = config.ActionDefinitions.CreateExisting("NewTask", (Func<BoundedTo<ContactMaint, Contact>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContactMaint, Contact>.ActionDefinition.IConfigured>) (a => (BoundedTo<ContactMaint, Contact>.ActionDefinition.IConfigured) a.WithCategory(categoryActivities)));
    config.AddScreenConfigurationFor((Func<BoundedTo<ContactMaint, Contact>.ScreenConfiguration.IStartConfigScreen, BoundedTo<ContactMaint, Contact>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<ContactMaint, Contact>.ScreenConfiguration.IConfigured) ((BoundedTo<ContactMaint, Contact>.ScreenConfiguration.IAllowOptionalConfig) screen).WithActions((Action<BoundedTo<ContactMaint, Contact>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<ContactMaint, PXAction<Contact>>>) (g => g.addOpportunity), (Func<BoundedTo<ContactMaint, Contact>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContactMaint, Contact>.ActionDefinition.IConfigured>) (c => (BoundedTo<ContactMaint, Contact>.ActionDefinition.IConfigured) c.WithCategory(categoryRecordCreation)));
      actions.Add<ContactMaint.CreateAccountFromContactGraphExt>((Expression<Func<ContactMaint.CreateAccountFromContactGraphExt, PXAction<Contact>>>) (g => g.CreateBAccount), (Func<BoundedTo<ContactMaint, Contact>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContactMaint, Contact>.ActionDefinition.IConfigured>) (c => (BoundedTo<ContactMaint, Contact>.ActionDefinition.IConfigured) c.WithCategory(categoryRecordCreation)));
      actions.Add((Expression<Func<ContactMaint, PXAction<Contact>>>) (g => g.addCase), (Func<BoundedTo<ContactMaint, Contact>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContactMaint, Contact>.ActionDefinition.IConfigured>) (c => (BoundedTo<ContactMaint, Contact>.ActionDefinition.IConfigured) c.WithCategory(categoryRecordCreation)));
      actions.Add<ContactMaint.CreateLeadFromContactGraphExt>((Expression<Func<ContactMaint.CreateLeadFromContactGraphExt, PXAction<Contact>>>) (g => g.CreateLead), (Func<BoundedTo<ContactMaint, Contact>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContactMaint, Contact>.ActionDefinition.IConfigured>) (c => (BoundedTo<ContactMaint, Contact>.ActionDefinition.IConfigured) c.WithCategory(categoryRecordCreation).IsDisabledWhen((BoundedTo<ContactMaint, Contact>.ISharedCondition) conditions.IsContactActive)));
      actions.Add(actionCreatePhoneCall);
      actions.Add(actionCreateNote);
      actions.Add(actionCreateMail);
      actions.Add(actionCreateTask);
      actions.AddNew("ShowProjects", (Func<BoundedTo<ContactMaint, Contact>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ContactMaint, Contact>.ActionDefinition.IConfigured>) (a => (BoundedTo<ContactMaint, Contact>.ActionDefinition.IConfigured) a.DisplayName("Projects").IsSidePanelScreen((Func<BoundedTo<ContactMaint, Contact>.NavigationDefinition.ISidePanelNeedScreen, BoundedTo<ContactMaint, Contact>.NavigationDefinition.IConfiguredSidePanel>) (sp => sp.NavigateToScreen("PM3010SP").WithIcon("project").WithAssignments((Action<BoundedTo<ContactMaint, Contact>.NavigationParameter.IContainerFillerNavigationActionParameters>) (ass => ass.Add("PMProjectContact_contactID", (Func<BoundedTo<ContactMaint, Contact>.NavigationParameter.INeedRightOperand, BoundedTo<ContactMaint, Contact>.NavigationParameter.IConfigured>) (e => e.SetFromField<Contact.contactID>()))))))));
    })).WithCategories((Action<BoundedTo<ContactMaint, Contact>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(categoryRecordCreation);
      categories.Add(categoryValidation);
      categories.Add(categoryActivities);
    })).WithFieldStates((Action<BoundedTo<ContactMaint, Contact>.DynamicFieldState.IContainerFillerFields>) (fields => fields.Add<Contact.resolution>((Func<BoundedTo<ContactMaint, Contact>.DynamicFieldState.INeedAnyConfigField, BoundedTo<ContactMaint, Contact>.DynamicFieldState.IConfigured>) (field => (BoundedTo<ContactMaint, Contact>.DynamicFieldState.IConfigured) field.SetComboValues(new (string, string)[9]
    {
      ("AS", "Assign"),
      ("CA", "Abandoned"),
      ("CD", "Duplicate"),
      ("CL", "Unable to contact"),
      ("CU", "Unknown"),
      ("EX", "External"),
      ("IN", "In Process"),
      ("OU", "Nurture"),
      ("RJ", "Disqualified")
    })))))));
  }

  public static class CategoryNames
  {
    public const string RecordCreation = "RecordCreation";
    public const string Activities = "Activities";
    public const string Validation = "Validation";
  }
}
