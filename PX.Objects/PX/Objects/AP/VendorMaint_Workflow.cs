// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorMaint_Workflow
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
namespace PX.Objects.AP;

public class VendorMaint_Workflow : PXGraphExtension<VendorMaint>
{
  public static bool IsActive() => false;

  public sealed override void Configure(PXScreenConfiguration config)
  {
    VendorMaint_Workflow.Configure(config.GetScreenConfigurationContext<VendorMaint, VendorR>());
  }

  protected static void Configure(WorkflowContext<VendorMaint, VendorR> context)
  {
    BoundedTo<VendorMaint, VendorR>.ActionCategory.IConfigured processingCategory = context.Categories.CreateNew("DocumentProcessing", (Func<BoundedTo<VendorMaint, VendorR>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<VendorMaint, VendorR>.ActionCategory.IConfigured>) (category => (BoundedTo<VendorMaint, VendorR>.ActionCategory.IConfigured) category.DisplayName("Document Processing")));
    BoundedTo<VendorMaint, VendorR>.ActionCategory.IConfigured managementCategory = context.Categories.CreateNew("VendorManagement", (Func<BoundedTo<VendorMaint, VendorR>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<VendorMaint, VendorR>.ActionCategory.IConfigured>) (category => (BoundedTo<VendorMaint, VendorR>.ActionCategory.IConfigured) category.DisplayName("Vendor Management")));
    BoundedTo<VendorMaint, VendorR>.ActionCategory.IConfigured customOtherCategory = context.Categories.CreateNew("CustomOther", (Func<BoundedTo<VendorMaint, VendorR>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<VendorMaint, VendorR>.ActionCategory.IConfigured>) (category => (BoundedTo<VendorMaint, VendorR>.ActionCategory.IConfigured) category.DisplayName("Other")));
    var conditions = new
    {
      IsNewBillAdjustmentDisabled = Bql<BqlOperand<Vendor.vStatus, IBqlString>.IsNotIn<VendorStatus.active, VendorStatus.holdPayments, VendorStatus.oneTime>>(),
      IsNewManualCheckDisabled = Bql<BqlOperand<Vendor.vStatus, IBqlString>.IsNotIn<VendorStatus.active, VendorStatus.oneTime>>(),
      IsPayBillDisabled = Bql<BqlOperand<Vendor.vStatus, IBqlString>.IsIn<VendorStatus.holdPayments, VendorStatus.inactive, VendorStatus.hold>>()
    }.AutoNameConditions();
    context.AddScreenConfigurationFor((Func<BoundedTo<VendorMaint, VendorR>.ScreenConfiguration.IStartConfigScreen, BoundedTo<VendorMaint, VendorR>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<VendorMaint, VendorR>.ScreenConfiguration.IConfigured) screen.WithActions((System.Action<BoundedTo<VendorMaint, VendorR>.ActionDefinition.IContainerFillerActions>) (actions =>
    {
      actions.Add<VendorMaint.CreateContactFromVendorGraphExt>((Expression<Func<VendorMaint.CreateContactFromVendorGraphExt, PXAction<VendorR>>>) (g => g.CreateContact), (Func<BoundedTo<VendorMaint, VendorR>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured>) (a => (BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured) a.WithCategory(managementCategory)));
      actions.Add<VendorMaint.CreateContactFromVendorGraphExt>((Expression<Func<VendorMaint.CreateContactFromVendorGraphExt, PXAction<VendorR>>>) (g => g.CreateContactToolBar));
      actions.Add<VendorMaint.ExtendToCustomer>((Expression<Func<VendorMaint.ExtendToCustomer, PXAction<VendorR>>>) (e => e.extendToCustomer), (Func<BoundedTo<VendorMaint, VendorR>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured>) (a => (BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured) a.WithCategory(managementCategory)));
      actions.Add((Expression<Func<VendorMaint, PXAction<VendorR>>>) (g => g.newBillAdjustment), (Func<BoundedTo<VendorMaint, VendorR>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured>) (a => (BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured) a.WithCategory(processingCategory).IsDisabledWhen((BoundedTo<VendorMaint, VendorR>.ISharedCondition) conditions.IsNewBillAdjustmentDisabled)));
      actions.Add((Expression<Func<VendorMaint, PXAction<VendorR>>>) (g => g.newManualCheck), (Func<BoundedTo<VendorMaint, VendorR>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured>) (a => (BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured) a.WithCategory(processingCategory).IsDisabledWhen((BoundedTo<VendorMaint, VendorR>.ISharedCondition) conditions.IsNewManualCheckDisabled)));
      actions.Add((Expression<Func<VendorMaint, PXAction<VendorR>>>) (g => g.approveBillsForPayments), (Func<BoundedTo<VendorMaint, VendorR>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured>) (a => (BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured) a.WithCategory(processingCategory)));
      actions.Add((Expression<Func<VendorMaint, PXAction<VendorR>>>) (g => g.payBills), (Func<BoundedTo<VendorMaint, VendorR>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured>) (a => (BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured) a.WithCategory(processingCategory).IsDisabledWhen((BoundedTo<VendorMaint, VendorR>.ISharedCondition) conditions.IsPayBillDisabled)));
      actions.Add((Expression<Func<VendorMaint, PXAction<VendorR>>>) (g => g.ChangeID), (Func<BoundedTo<VendorMaint, VendorR>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured>) (a => (BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured) a.WithCategory(customOtherCategory)));
      actions.Add<VendorMaint.DefContactAddressExt>((Expression<Func<VendorMaint.DefContactAddressExt, PXAction<VendorR>>>) (e => e.ValidateAddresses), (Func<BoundedTo<VendorMaint, VendorR>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured>) (a => (BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured) a.WithCategory(customOtherCategory)));
      actions.Add((Expression<Func<VendorMaint, PXAction<VendorR>>>) (g => g.viewBusnessAccount), (Func<BoundedTo<VendorMaint, VendorR>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured>) (a => (BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured) a.WithCategory(customOtherCategory)));
      actions.Add<VendorMaint.ExtendToCustomer>((Expression<Func<VendorMaint.ExtendToCustomer, PXAction<VendorR>>>) (e => e.viewCustomer), (Func<BoundedTo<VendorMaint, VendorR>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured>) (a => (BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured) a.WithCategory(customOtherCategory)));
      actions.Add((Expression<Func<VendorMaint, PXAction<VendorR>>>) (g => g.viewRestrictionGroups), (Func<BoundedTo<VendorMaint, VendorR>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured>) (a => (BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured) a.WithCategory(customOtherCategory)));
      actions.Add((Expression<Func<VendorMaint, PXAction<VendorR>>>) (g => g.vendorDetails), (Func<BoundedTo<VendorMaint, VendorR>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured>) (a => (BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured) a.WithCategory(PredefinedCategory.Inquiries)));
      actions.Add((Expression<Func<VendorMaint, PXAction<VendorR>>>) (g => g.vendorPrice), (Func<BoundedTo<VendorMaint, VendorR>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured>) (a => (BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured) a.WithCategory(PredefinedCategory.Inquiries)));
      actions.Add((Expression<Func<VendorMaint, PXAction<VendorR>>>) (g => g.balanceByVendor), (Func<BoundedTo<VendorMaint, VendorR>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured>) (a => (BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured) a.WithCategory(PredefinedCategory.Reports)));
      actions.Add((Expression<Func<VendorMaint, PXAction<VendorR>>>) (g => g.aPDocumentRegister), (Func<BoundedTo<VendorMaint, VendorR>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured>) (a => (BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured) a.WithCategory(PredefinedCategory.Reports)));
      actions.Add((Expression<Func<VendorMaint, PXAction<VendorR>>>) (g => g.vendorHistory), (Func<BoundedTo<VendorMaint, VendorR>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured>) (a => (BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured) a.WithCategory(PredefinedCategory.Reports)));
      actions.Add((Expression<Func<VendorMaint, PXAction<VendorR>>>) (g => g.aPAgedPastDue), (Func<BoundedTo<VendorMaint, VendorR>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured>) (a => (BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured) a.WithCategory(PredefinedCategory.Reports)));
      actions.Add((Expression<Func<VendorMaint, PXAction<VendorR>>>) (g => g.aPAgedOutstanding), (Func<BoundedTo<VendorMaint, VendorR>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured>) (a => (BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured) a.WithCategory(PredefinedCategory.Reports)));
      actions.Add((Expression<Func<VendorMaint, PXAction<VendorR>>>) (g => g.repVendorDetails), (Func<BoundedTo<VendorMaint, VendorR>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured>) (a => (BoundedTo<VendorMaint, VendorR>.ActionDefinition.IConfigured) a.WithCategory(PredefinedCategory.Reports)));
    })).WithCategories((System.Action<BoundedTo<VendorMaint, VendorR>.ActionCategory.IContainerFillerCategories>) (categories =>
    {
      categories.Add(managementCategory);
      categories.Add(processingCategory);
      categories.Add(customOtherCategory);
      categories.Update(FolderType.InquiriesFolder, (Func<BoundedTo<VendorMaint, VendorR>.ActionCategory.ConfiguratorCategory, BoundedTo<VendorMaint, VendorR>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(customOtherCategory)));
      categories.Update(FolderType.ReportsFolder, (Func<BoundedTo<VendorMaint, VendorR>.ActionCategory.ConfiguratorCategory, BoundedTo<VendorMaint, VendorR>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(context.Categories.Get(FolderType.InquiriesFolder))));
    }))));

    BoundedTo<VendorMaint, VendorR>.Condition Bql<T>() where T : IBqlUnary, new()
    {
      return context.Conditions.FromBql<T>();
    }
  }

  public static class ActionCategoryNames
  {
    public const string Management = "VendorManagement";
    public const string Processing = "DocumentProcessing";
    public const string CustomOther = "CustomOther";
  }

  public static class ActionCategory
  {
    public const string Management = "Vendor Management";
    public const string Processing = "Document Processing";
    public const string Other = "Other";
  }
}
