// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.GraphExtensions.SOOrderEntry_PayLinkWorkflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.CS;
using PX.Objects.SO;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CC.GraphExtensions;

public class SOOrderEntry_PayLinkWorkflow : PXGraphExtension<SOOrderEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.acumaticaPayments>();

  public virtual void Configure(PXScreenConfiguration config)
  {
    config.GetScreenConfigurationContext<SOOrderEntry, PX.Objects.SO.SOOrder>().UpdateScreenConfigurationFor((Func<BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ScreenConfiguration.ConfiguratorScreen>) (i => i.WithActions((Action<BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ActionDefinition.ContainerAdjusterActions>) (actions => actions.Add<SOOrderEntryPayLink>((Expression<Func<SOOrderEntryPayLink, PXAction<PX.Objects.SO.SOOrder>>>) (e => e.createLink), (Func<BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ActionDefinition.IConfigured>) (c => (BoundedTo<SOOrderEntry, PX.Objects.SO.SOOrder>.ActionDefinition.IConfigured) c.MassProcessingScreen<SOCreateShipment>()))))));
  }
}
