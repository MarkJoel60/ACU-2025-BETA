// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerMaintECMWorkflowExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.CS;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.AR;

public class CustomerMaintECMWorkflowExt : PXGraphExtension<CustomerMaint_Workflow, CustomerMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.eCM>();

  public virtual void Configure(PXScreenConfiguration config)
  {
    CustomerMaintECMWorkflowExt.Configure(config.GetScreenConfigurationContext<CustomerMaint, Customer>());
  }

  protected static void Configure(WorkflowContext<CustomerMaint, Customer> context)
  {
    BoundedTo<CustomerMaint, Customer>.ActionCategory.IConfigured ecmCategory = context.Categories.CreateNew("ECMID", (Func<BoundedTo<CustomerMaint, Customer>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<CustomerMaint, Customer>.ActionCategory.IConfigured>) (category => (BoundedTo<CustomerMaint, Customer>.ActionCategory.IConfigured) category.DisplayName("Exemption Certificates")));
    var conditions = new
    {
      HasCreatedInECM = Bql<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Customer.eCMCompanyCode, IsNotNull>>>>.And<BqlOperand<Customer.eCMCompanyCode, IBqlString>.IsNotEqual<Empty>>>(),
      IsECMValid = Bql<BqlOperand<Customer.isECMValid, IBqlBool>.IsEqual<True>>(),
      IsECMOff = (PXAccess.FeatureInstalled<FeaturesSet.eCM>() ? Bql<BqlOperand<True, IBqlBool>.IsEqual<False>>() : Bql<BqlOperand<True, IBqlBool>.IsEqual<True>>()),
      IsNewDocument = Bql<BqlOperand<Customer.acctCD, IBqlString>.IsNull>()
    }.AutoNameConditions();
    context.UpdateScreenConfigurationFor((Func<BoundedTo<CustomerMaint, Customer>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<CustomerMaint, Customer>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((Action<BoundedTo<CustomerMaint, Customer>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add<CustomerMaintExternalECMExt>((Expression<Func<CustomerMaintExternalECMExt, PXAction<Customer>>>) (g => g.retrieveCertificate), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory(ecmCategory).IsDisabledWhen((BoundedTo<CustomerMaint, Customer>.ISharedCondition) BoundedTo<CustomerMaint, Customer>.Condition.op_LogicalNot(conditions.HasCreatedInECM)).IsHiddenWhen((BoundedTo<CustomerMaint, Customer>.ISharedCondition) conditions.IsECMOff)));
      actions.Add<CustomerMaintExternalECMExt>((Expression<Func<CustomerMaintExternalECMExt, PXAction<Customer>>>) (g => g.requestCertificate), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory(ecmCategory).IsDisabledWhen((BoundedTo<CustomerMaint, Customer>.ISharedCondition) BoundedTo<CustomerMaint, Customer>.Condition.op_LogicalNot(conditions.HasCreatedInECM)).IsHiddenWhen((BoundedTo<CustomerMaint, Customer>.ISharedCondition) conditions.IsECMOff)));
      actions.Add<CustomerMaintExternalECMExt>((Expression<Func<CustomerMaintExternalECMExt, PXAction<Customer>>>) (g => g.createCustomerInECM), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a => (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) a.WithCategory(ecmCategory).IsDisabledWhen((BoundedTo<CustomerMaint, Customer>.ISharedCondition) conditions.IsNewDocument).IsHiddenWhen((BoundedTo<CustomerMaint, Customer>.ISharedCondition) conditions.IsECMOff)));
      actions.Add<CustomerMaintExternalECMExt>((Expression<Func<CustomerMaintExternalECMExt, PXAction<Customer>>>) (g => g.updateCustomerInECM), (Func<BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured>) (a =>
      {
        BoundedTo<CustomerMaint, Customer>.ActionDefinition.IAllowOptionalConfigAction optionalConfigAction = a.WithCategory(ecmCategory);
        BoundedTo<CustomerMaint, Customer>.Condition condition3 = BoundedTo<CustomerMaint, Customer>.Condition.op_LogicalNot(conditions.HasCreatedInECM);
        BoundedTo<CustomerMaint, Customer>.Condition condition4 = BoundedTo<CustomerMaint, Customer>.Condition.op_True(condition3) ? condition3 : BoundedTo<CustomerMaint, Customer>.Condition.op_BitwiseOr(condition3, conditions.IsECMValid);
        return (BoundedTo<CustomerMaint, Customer>.ActionDefinition.IConfigured) optionalConfigAction.IsDisabledWhen((BoundedTo<CustomerMaint, Customer>.ISharedCondition) condition4).IsHiddenWhen((BoundedTo<CustomerMaint, Customer>.ISharedCondition) conditions.IsECMOff);
      }));
    })).WithCategories((Action<BoundedTo<CustomerMaint, Customer>.ActionCategory.ContainerAdjusterCategories>) (categories =>
    {
      categories.Add(ecmCategory);
      categories.Update("OtherID", (Func<BoundedTo<CustomerMaint, Customer>.ActionCategory.ConfiguratorCategory, BoundedTo<CustomerMaint, Customer>.ActionCategory.ConfiguratorCategory>) (category => category.PlaceAfter(ecmCategory)));
    }))));

    BoundedTo<CustomerMaint, Customer>.Condition Bql<T>() where T : IBqlUnary, new()
    {
      return context.Conditions.FromBql<T>();
    }
  }

  public static class ECMCategoryNames
  {
    public const string ECM = "Exemption Certificates";
  }

  public static class ECMCategoryID
  {
    public const string ECM = "ECMID";
  }
}
