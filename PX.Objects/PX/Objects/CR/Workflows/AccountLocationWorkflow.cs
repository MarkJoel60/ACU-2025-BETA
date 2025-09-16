// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Workflows.AccountLocationWorkflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CR.Workflows;

public class AccountLocationWorkflow : PXGraphExtension<AccountLocationMaint>
{
  public static bool IsActive() => false;

  public virtual void Configure(PXScreenConfiguration configuration)
  {
    AccountLocationWorkflow.Configure(configuration.GetScreenConfigurationContext<LocationMaint, Location>());
  }

  protected static void Configure(WorkflowContext<LocationMaint, Location> context)
  {
    LocationWorkflow.Configure(context);
    BoundedTo<LocationMaint, Location>.ActionCategory.IConfigured otherCategory = context.Categories.Get("CustomOther");
    context.UpdateScreenConfigurationFor((Func<BoundedTo<LocationMaint, Location>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<LocationMaint, Location>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((Action<BoundedTo<LocationMaint, Location>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add((Expression<Func<LocationMaint, PXAction<Location>>>) (g => (g as AccountLocationMaint).ViewCustomerLocation), (Func<BoundedTo<LocationMaint, Location>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<LocationMaint, Location>.ActionDefinition.IConfigured>) (a => (BoundedTo<LocationMaint, Location>.ActionDefinition.IConfigured) a.InFolder(otherCategory)));
      actions.Add((Expression<Func<LocationMaint, PXAction<Location>>>) (g => (g as AccountLocationMaint).ViewVendorLocation), (Func<BoundedTo<LocationMaint, Location>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<LocationMaint, Location>.ActionDefinition.IConfigured>) (a => (BoundedTo<LocationMaint, Location>.ActionDefinition.IConfigured) a.InFolder(otherCategory)));
    }))));
  }
}
