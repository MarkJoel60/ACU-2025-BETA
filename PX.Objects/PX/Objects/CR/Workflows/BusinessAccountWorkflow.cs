// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Workflows.BusinessAccountWorkflow
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
namespace PX.Objects.CR.Workflows;

public class BusinessAccountWorkflow : PXGraphExtension<BusinessAccountMaint>
{
  public static bool IsActive() => false;

  public virtual void Configure(PXScreenConfiguration configuration)
  {
    BusinessAccountWorkflow.Configure(configuration.GetScreenConfigurationContext<BusinessAccountMaint, BAccount>());
  }

  protected static void Configure(
    WorkflowContext<BusinessAccountMaint, BAccount> context)
  {
    BoundedTo<BusinessAccountMaint, BAccount>.ActionCategory.IConfigured categoryRecordCreation = context.Categories.CreateNew("RecordCreation", (Func<BoundedTo<BusinessAccountMaint, BAccount>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<BusinessAccountMaint, BAccount>.ActionCategory.IConfigured>) (category => (BoundedTo<BusinessAccountMaint, BAccount>.ActionCategory.IConfigured) category.DisplayName("Record Creation")));
    BoundedTo<BusinessAccountMaint, BAccount>.ActionCategory.IConfigured categoryActivities = context.Categories.CreateNew("Activities", (Func<BoundedTo<BusinessAccountMaint, BAccount>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<BusinessAccountMaint, BAccount>.ActionCategory.IConfigured>) (category => (BoundedTo<BusinessAccountMaint, BAccount>.ActionCategory.IConfigured) category.DisplayName("Activities").PlaceAfter(categoryRecordCreation)));
    BoundedTo<BusinessAccountMaint, BAccount>.ActionCategory.IConfigured categoryValidation = context.Categories.CreateNew("Validation", (Func<BoundedTo<BusinessAccountMaint, BAccount>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<BusinessAccountMaint, BAccount>.ActionCategory.IConfigured>) (category => (BoundedTo<BusinessAccountMaint, BAccount>.ActionCategory.IConfigured) category.DisplayName("Validation").PlaceAfter(categoryActivities)));
    BoundedTo<BusinessAccountMaint, BAccount>.ActionCategory.IConfigured categoryOther = context.Categories.CreateNew("Other", (Func<BoundedTo<BusinessAccountMaint, BAccount>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<BusinessAccountMaint, BAccount>.ActionCategory.IConfigured>) (category => (BoundedTo<BusinessAccountMaint, BAccount>.ActionCategory.IConfigured) category.DisplayName("Other").PlaceAfter(categoryValidation)));
    var conditions = new
    {
      IsBusinessAccount = context.Conditions.FromBql<BqlOperand<BAccount.type, IBqlString>.IsEqual<BAccountType.prospectType>>(),
      IsCustomer = context.Conditions.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.type, Equal<BAccountType.customerType>>>>>.Or<BqlOperand<BAccount.type, IBqlString>.IsEqual<BAccountType.combinedType>>>(),
      IsNotCustomer = context.Conditions.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.type, NotEqual<BAccountType.customerType>>>>>.And<BqlOperand<BAccount.type, IBqlString>.IsNotEqual<BAccountType.combinedType>>>(),
      IsVendor = context.Conditions.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.type, Equal<BAccountType.vendorType>>>>>.Or<BqlOperand<BAccount.type, IBqlString>.IsEqual<BAccountType.combinedType>>>(),
      IsNotVendor = context.Conditions.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.type, NotEqual<BAccountType.vendorType>>>>>.And<BqlOperand<BAccount.type, IBqlString>.IsNotEqual<BAccountType.combinedType>>>(),
      IsExtendToCustomerHidden = context.Conditions.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.type, Equal<BAccountType.customerType>>>>>.Or<BqlOperand<BAccount.type, IBqlString>.IsEqual<BAccountType.combinedType>>>(),
      IsExtendToVendorHidded = context.Conditions.FromBql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.type, Equal<BAccountType.vendorType>>>>>.Or<BqlOperand<BAccount.type, IBqlString>.IsEqual<BAccountType.combinedType>>>()
    }.AutoNameConditions();
    context.AddScreenConfigurationFor((Func<BoundedTo<BusinessAccountMaint, BAccount>.ScreenConfiguration.IStartConfigScreen, BoundedTo<BusinessAccountMaint, BAccount>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<BusinessAccountMaint, BAccount>.ScreenConfiguration.IConfigured) ((BoundedTo<BusinessAccountMaint, BAccount>.ScreenConfiguration.IAllowOptionalConfig) screen).WithActions((Action<BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add((Expression<Func<BusinessAccountMaint, PXAction<BAccount>>>) (g => g.addOpportunity), (Func<BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IConfigured>) (a => (BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IConfigured) a.WithCategory(categoryRecordCreation).WithPersistOptions((ActionPersistOptions) 2)));
      actions.Add<BusinessAccountMaint.CreateContactFromAccountGraphExt>((Expression<Func<BusinessAccountMaint.CreateContactFromAccountGraphExt, PXAction<BAccount>>>) (e => e.CreateContact), (Func<BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IConfigured>) (a => (BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IConfigured) a.WithCategory(categoryRecordCreation).WithPersistOptions((ActionPersistOptions) 2)));
      actions.Add<BusinessAccountMaint.ExtendToCustomer>((Expression<Func<BusinessAccountMaint.ExtendToCustomer, PXAction<BAccount>>>) (e => e.extendToCustomer), (Func<BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IConfigured>) (a => (BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IConfigured) a.WithPersistOptions((ActionPersistOptions) 2).IsHiddenWhen((BoundedTo<BusinessAccountMaint, BAccount>.ISharedCondition) conditions.IsExtendToCustomerHidden)));
      actions.Add<BusinessAccountMaint.ExtendToVendor>((Expression<Func<BusinessAccountMaint.ExtendToVendor, PXAction<BAccount>>>) (e => e.extendToVendor), (Func<BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IConfigured>) (a => (BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IConfigured) a.WithCategory(categoryRecordCreation).WithPersistOptions((ActionPersistOptions) 2).IsHiddenWhen((BoundedTo<BusinessAccountMaint, BAccount>.ISharedCondition) conditions.IsExtendToVendorHidded)));
      actions.Add<BusinessAccountMaint.CreateLeadFromAccountGraphExt>((Expression<Func<BusinessAccountMaint.CreateLeadFromAccountGraphExt, PXAction<BAccount>>>) (e => e.CreateLead), (Func<BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IConfigured>) (a => (BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IConfigured) a.WithCategory(categoryRecordCreation).WithPersistOptions((ActionPersistOptions) 2)));
      actions.Add("NewTask", (Func<BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IConfigured>) (a => (BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IConfigured) a.WithCategory(categoryActivities)));
      actions.Add("NewActivityN_Workflow", (Func<BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IConfigured>) (a => (BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IConfigured) a.WithCategory(categoryActivities)));
      actions.Add<BusinessAccountMaint.DefContactAddressExt>((Expression<Func<BusinessAccountMaint.DefContactAddressExt, PXAction<BAccount>>>) (e => e.ValidateAddresses), (Func<BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IConfigured>) (a => (BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IConfigured) a.WithCategory(categoryValidation)));
      actions.Add<BusinessAccountMaint.ExtendToCustomer>((Expression<Func<BusinessAccountMaint.ExtendToCustomer, PXAction<BAccount>>>) (e => e.viewCustomer), (Func<BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IConfigured>) (a => (BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IConfigured) a.WithPersistOptions((ActionPersistOptions) 2).IsHiddenWhen((BoundedTo<BusinessAccountMaint, BAccount>.ISharedCondition) conditions.IsNotCustomer)));
      actions.Add<BusinessAccountMaint.ExtendToVendor>((Expression<Func<BusinessAccountMaint.ExtendToVendor, PXAction<BAccount>>>) (e => e.viewVendor), (Func<BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IConfigured>) (a => (BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IConfigured) a.WithCategory(categoryOther).WithPersistOptions((ActionPersistOptions) 2).IsHiddenWhen((BoundedTo<BusinessAccountMaint, BAccount>.ISharedCondition) conditions.IsNotVendor)));
      actions.Add((Expression<Func<BusinessAccountMaint, PXAction<BAccount>>>) (g => g.ChangeID), (Func<BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IConfigured>) (a => (BoundedTo<BusinessAccountMaint, BAccount>.ActionDefinition.IConfigured) a.WithCategory(categoryOther, "viewVendor").WithPersistOptions((ActionPersistOptions) 2)));
    })).WithCategories((Action<BoundedTo<BusinessAccountMaint, BAccount>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(categoryRecordCreation);
      categories.Add(categoryValidation);
      categories.Add(categoryActivities);
      categories.Add(categoryOther);
    }))));
  }

  public static class CategoryNames
  {
    public const string RecordCreation = "RecordCreation";
    public const string Activities = "Activities";
    public const string Validation = "Validation";
    public const string Other = "Other";
  }
}
