// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Workflows.CustomerLocationMaint_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.CR;
using PX.Objects.CR.Workflows;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.AR.Workflows;

public class CustomerLocationMaint_Workflow : PXGraphExtension<CustomerLocationMaint>
{
  public static bool IsActive() => false;

  public virtual void Configure(PXScreenConfiguration configuration)
  {
    CustomerLocationMaint_Workflow.Configure(configuration.GetScreenConfigurationContext<LocationMaint, PX.Objects.CR.Location>());
  }

  protected static void Configure(WorkflowContext<LocationMaint, PX.Objects.CR.Location> context)
  {
    LocationWorkflow.Configure(context);
    BoundedTo<LocationMaint, PX.Objects.CR.Location>.ActionCategory.IConfigured otherCategory = context.Categories.Get("CustomOther");
    BoundedTo<LocationMaint, PX.Objects.CR.Location>.ActionDefinition.IConfigured viewAccountLocationAction = context.ActionDefinitions.CreateExisting((Expression<Func<LocationMaint, PXAction<PX.Objects.CR.Location>>>) (g => ((CustomerLocationMaint) g).ViewAccountLocation), (Func<BoundedTo<LocationMaint, PX.Objects.CR.Location>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<LocationMaint, PX.Objects.CR.Location>.ActionDefinition.IConfigured>) (a => (BoundedTo<LocationMaint, PX.Objects.CR.Location>.ActionDefinition.IConfigured) a.InFolder(otherCategory)));
    context.UpdateScreenConfigurationFor((Func<BoundedTo<LocationMaint, PX.Objects.CR.Location>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<LocationMaint, PX.Objects.CR.Location>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((Action<BoundedTo<LocationMaint, PX.Objects.CR.Location>.ActionDefinition.ContainerAdjusterActions>) (a => a.Add(viewAccountLocationAction)))));
  }
}
