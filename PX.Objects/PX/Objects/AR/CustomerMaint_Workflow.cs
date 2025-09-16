// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerMaint_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.CS;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.AR;

public class CustomerMaint_Workflow : PXGraphExtension<CustomerMaint>
{
  public static bool IsActive() => false;

  public virtual void Configure(PXScreenConfiguration configuration)
  {
    CustomerMaint_Workflow.Configure(configuration.GetScreenConfigurationContext<CustomerMaint, Customer>());
  }

  protected static void Configure(WorkflowContext<CustomerMaint, Customer> context)
  {
    BoundedTo<CustomerMaint, Customer>.ActionCategory.IConfigured customerManagementCategory = context.Categories.CreateNew("CustomerManagementID", (Func<BoundedTo<CustomerMaint, Customer>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CustomerMaint, Customer>.ActionCategory.IConfigured>) (category => (BoundedTo<CustomerMaint, Customer>.ActionCategory.IConfigured) category.DisplayName("Customer Management")));
    BoundedTo<CustomerMaint, Customer>.ActionCategory.IConfigured documentProcessingCategory = context.Categories.CreateNew("DocumentProcessingID", (Func<BoundedTo<CustomerMaint, Customer>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CustomerMaint, Customer>.ActionCategory.IConfigured>) (category => (BoundedTo<CustomerMaint, Customer>.ActionCategory.IConfigured) category.DisplayName("Document Processing")));
    BoundedTo<CustomerMaint, Customer>.ActionCategory.IConfigured statementsCategory = context.Categories.CreateNew("StatementsID", (Func<BoundedTo<CustomerMaint, Customer>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CustomerMaint, Customer>.ActionCategory.IConfigured>) (category => (BoundedTo<CustomerMaint, Customer>.ActionCategory.IConfigured) category.DisplayName("Statements")));
    BoundedTo<CustomerMaint, Customer>.ActionCategory.IConfigured servicesCategory = context.Categories.CreateNew("ServicesID", (Func<BoundedTo<CustomerMaint, Customer>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CustomerMaint, Customer>.ActionCategory.IConfigured>) (category => (BoundedTo<CustomerMaint, Customer>.ActionCategory.IConfigured) category.DisplayName("Services")));
    BoundedTo<CustomerMaint, Customer>.ActionCategory.IConfigured otherCategory = context.Categories.CreateNew("OtherID", (Func<BoundedTo<CustomerMaint, Customer>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CustomerMaint, Customer>.ActionCategory.IConfigured>) (category => (BoundedTo<CustomerMaint, Customer>.ActionCategory.IConfigured) category.DisplayName("Other")));
    var conditions = new
    {
      IsNewInvoiceMemoDisabled = Bql<BqlOperand<Customer.status, IBqlString>.IsNotIn<CustomerStatus.active, CustomerStatus.oneTime>>(),
      IsNewSalesOrderDisabled = Bql<BqlOperand<Customer.status, IBqlString>.IsNotIn<CustomerStatus.active, CustomerStatus.oneTime>>(),
      IsNewPaymentDisabled = Bql<BqlOperand<Customer.status, IBqlString>.IsNotIn<CustomerStatus.active, CustomerStatus.oneTime, CustomerStatus.creditHold, CustomerStatus.hold>>(),
      IsWriteOffBalanceDisabled = Bql<BqlOperand<Customer.status, IBqlString>.IsNotIn<CustomerStatus.active, CustomerStatus.oneTime, CustomerStatus.creditHold>>(),
      IsGenerateOnDemandStatementDisabled = Bql<BqlOperand<Customer.status, IBqlString>.IsNotIn<CustomerStatus.active, CustomerStatus.oneTime, CustomerStatus.creditHold, CustomerStatus.inactive>>(),
      IsRegenerateLastStatementDisabled = Bql<BqlOperand<Customer.status, IBqlString>.IsNotIn<CustomerStatus.active, CustomerStatus.oneTime, CustomerStatus.creditHold, CustomerStatus.inactive>>(),
      IsViewBusnessAccountDisabled = Bql<BqlOperand<Customer.status, IBqlString>.IsNotIn<CustomerStatus.active, CustomerStatus.oneTime, CustomerStatus.creditHold, CustomerStatus.hold, CustomerStatus.inactive>>(),
      IsInventoryAndOrderManagementOff = (PXAccess.FeatureInstalled<FeaturesSet.distributionModule>() ? Bql<BqlOperand<True, IBqlBool>.IsEqual<False>>() : Bql<BqlOperand<True, IBqlBool>.IsEqual<True>>())
    }.AutoNameConditions();
    context.AddScreenConfigurationFor((Func<BoundedTo<CustomerMaint, Customer>.ScreenConfiguration.IStartConfigScreen, BoundedTo<CustomerMaint, Customer>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<CustomerMaint, Customer>.ScreenConfiguration.IConfigured) ((BoundedTo<CustomerMaint, Customer>.ScreenConfiguration.IAllowOptionalConfig) screen).WithActions((Action<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add<CustomerMaint.CreateContactFromCustomerGraphExt>((Expression<Func<CustomerMaint.CreateContactFromCustomerGraphExt, PXAction<Customer>>>) (g => g.CreateContact), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory(customerManagementCategory)));
      actions.Add<CustomerMaint.CreateContactFromCustomerGraphExt>((Expression<Func<CustomerMaint.CreateContactFromCustomerGraphExt, PXAction<Customer>>>) (g => g.CreateContactToolBar), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) null);
      actions.Add((Expression<Func<CustomerMaint, PXAction<Customer>>>) (g => g.newInvoiceMemo), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory(documentProcessingCategory).IsDisabledWhen((BoundedTo<CustomerMaint, Customer>.ISharedCondition) conditions.IsNewInvoiceMemoDisabled)));
      actions.Add((Expression<Func<CustomerMaint, PXAction<Customer>>>) (g => g.newSalesOrder), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory(documentProcessingCategory).IsDisabledWhen((BoundedTo<CustomerMaint, Customer>.ISharedCondition) conditions.IsNewSalesOrderDisabled).IsHiddenWhen((BoundedTo<CustomerMaint, Customer>.ISharedCondition) conditions.IsInventoryAndOrderManagementOff)));
      actions.Add((Expression<Func<CustomerMaint, PXAction<Customer>>>) (g => g.newPayment), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory(documentProcessingCategory).IsDisabledWhen((BoundedTo<CustomerMaint, Customer>.ISharedCondition) conditions.IsNewPaymentDisabled)));
      actions.Add((Expression<Func<CustomerMaint, PXAction<Customer>>>) (g => g.writeOffBalance), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory(documentProcessingCategory).IsDisabledWhen((BoundedTo<CustomerMaint, Customer>.ISharedCondition) conditions.IsWriteOffBalanceDisabled)));
      actions.Add((Expression<Func<CustomerMaint, PXAction<Customer>>>) (g => g.generateOnDemandStatement), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory(statementsCategory).IsDisabledWhen((BoundedTo<CustomerMaint, Customer>.ISharedCondition) conditions.IsGenerateOnDemandStatementDisabled)));
      actions.Add((Expression<Func<CustomerMaint, PXAction<Customer>>>) (g => g.regenerateLastStatement), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory(statementsCategory).IsDisabledWhen((BoundedTo<CustomerMaint, Customer>.ISharedCondition) conditions.IsRegenerateLastStatementDisabled)));
      actions.Add((Expression<Func<CustomerMaint, PXAction<Customer>>>) (g => g.viewBusnessAccount), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.IsDisabledWhen((BoundedTo<CustomerMaint, Customer>.ISharedCondition) conditions.IsViewBusnessAccountDisabled)));
      actions.Add<CustomerMaint.ExtendToVendor>((Expression<Func<CustomerMaint.ExtendToVendor, PXAction<Customer>>>) (g => g.viewVendor), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory(otherCategory)));
      actions.Add<CustomerMaint.DefContactAddressExt>((Expression<Func<CustomerMaint.DefContactAddressExt, PXAction<Customer>>>) (g => g.ValidateAddresses), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory(otherCategory)));
      actions.Add((Expression<Func<CustomerMaint, PXAction<Customer>>>) (g => g.ChangeID), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory(otherCategory, "CreateContact")));
      actions.Add((Expression<Func<CustomerMaint, PXAction<Customer>>>) (g => g.viewRestrictionGroups), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory(otherCategory)));
      actions.Add<CustomerMaint.ExtendToVendor>((Expression<Func<CustomerMaint.ExtendToVendor, PXAction<Customer>>>) (g => g.extendToVendor), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory(customerManagementCategory)));
      actions.Add((Expression<Func<CustomerMaint, PXAction<Customer>>>) (g => g.customerDocuments), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 1)));
      actions.Add((Expression<Func<CustomerMaint, PXAction<Customer>>>) (g => g.statementForCustomer), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory(statementsCategory)));
      actions.Add((Expression<Func<CustomerMaint, PXAction<Customer>>>) (g => g.salesPrice), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 1)));
      actions.Add((Expression<Func<CustomerMaint, PXAction<Customer>>>) (g => g.aRBalanceByCustomer), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 2)));
      actions.Add((Expression<Func<CustomerMaint, PXAction<Customer>>>) (g => g.aRRegister), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 2)));
      actions.Add((Expression<Func<CustomerMaint, PXAction<Customer>>>) (g => g.customerHistory), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 2)));
      actions.Add((Expression<Func<CustomerMaint, PXAction<Customer>>>) (g => g.aRAgedPastDue), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 2)));
      actions.Add((Expression<Func<CustomerMaint, PXAction<Customer>>>) (g => g.aRAgedOutstanding), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 2)));
      actions.Add((Expression<Func<CustomerMaint, PXAction<Customer>>>) (g => g.customerDetails), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory((PredefinedCategory) 2)));
      actions.Add((Expression<Func<CustomerMaint, PXAction<Customer>>>) (g => g.customerStatement), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory(statementsCategory)));
    })).WithCategories((Action<BoundedTo<CustomerMaint, Customer>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(customerManagementCategory);
      categories.Add(documentProcessingCategory);
      categories.Add(statementsCategory);
      categories.Add(servicesCategory);
      categories.Add(otherCategory);
      categories.Update((FolderType) 1, (Func<BoundedTo<CustomerMaint, Customer>.ActionCategory.ConfiguratorCategory, BoundedTo<CustomerMaint, Customer>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(otherCategory)));
      categories.Update((FolderType) 2, (Func<BoundedTo<CustomerMaint, Customer>.ActionCategory.ConfiguratorCategory, BoundedTo<CustomerMaint, Customer>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter((FolderType) 1)));
    }))));

    BoundedTo<CustomerMaint, Customer>.Condition Bql<T>() where T : IBqlUnary, new()
    {
      return context.Conditions.FromBql<T>();
    }
  }

  public static class CategoryNames
  {
    public const string CustomerManagement = "Customer Management";
    public const string DocumentProcessing = "Document Processing";
    public const string Statements = "Statements";
    public const string Services = "Services";
    public const string Other = "Other";
  }

  public static class CategoryID
  {
    public const string CustomerManagement = "CustomerManagementID";
    public const string DocumentProcessing = "DocumentProcessingID";
    public const string Statements = "StatementsID";
    public const string Services = "ServicesID";
    public const string Other = "OtherID";
  }
}
