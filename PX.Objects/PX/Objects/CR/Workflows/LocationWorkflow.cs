// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Workflows.LocationWorkflow
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

public static class LocationWorkflow
{
  public static void Configure(PXScreenConfiguration configuration)
  {
    LocationWorkflow.Configure(configuration.GetScreenConfigurationContext<LocationMaint, Location>());
  }

  public static void Configure(WorkflowContext<LocationMaint, Location> context)
  {
    BoundedTo<LocationMaint, Location>.ActionCategory.IConfigured customOtherCategory = context.Categories.CreateNew("CustomOther", (Func<BoundedTo<LocationMaint, Location>.ActionCategory.IAllowOptionalConfigCategory, BoundedTo<LocationMaint, Location>.ActionCategory.IConfigured>) (category => (BoundedTo<LocationMaint, Location>.ActionCategory.IConfigured) category.DisplayName("Other")));
    context.Conditions.FromBql<BqlOperand<Location.isDefault, IBqlBool>.IsEqual<True>>().WithSharedName("IsDefault");
    context.AddScreenConfigurationFor((Func<BoundedTo<LocationMaint, Location>.ScreenConfiguration.IStartConfigScreen, BoundedTo<LocationMaint, Location>.ScreenConfiguration.IConfigured>) (screen => (BoundedTo<LocationMaint, Location>.ScreenConfiguration.IConfigured) ((BoundedTo<LocationMaint, Location>.ScreenConfiguration.IAllowOptionalConfig) screen).WithActions((Action<BoundedTo<LocationMaint, Location>.ActionDefinition.IContainerFillerActions>) (actions => actions.Add((Expression<Func<LocationMaint, PXAction<Location>>>) (g => g.validateAddresses), (Func<BoundedTo<LocationMaint, Location>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<LocationMaint, Location>.ActionDefinition.IConfigured>) (a => (BoundedTo<LocationMaint, Location>.ActionDefinition.IConfigured) a.InFolder(customOtherCategory))))).WithCategories((Action<BoundedTo<LocationMaint, Location>.ActionCategory.IContainerFillerCategories>) (categories => categories.Add(customOtherCategory)))));
  }

  public static class ActionCategoryNames
  {
    public const string CustomOther = "CustomOther";
  }

  public static class ActionCategory
  {
    public const string Other = "Other";
  }
}
