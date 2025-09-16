// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INProductionEntry_Workflow
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.IN;

public class INProductionEntry_Workflow : 
  INRegisterEntryBase_Workflow<INProductionEntry, INDocType.production>
{
  public virtual void Configure(PXScreenConfiguration config)
  {
    WorkflowContext<INProductionEntry, INRegister> configurationContext = config.GetScreenConfigurationContext<INProductionEntry, INRegister>();
    INRegisterEntryBase_Workflow<INProductionEntry, INDocType.production>.ConfigureCommon(configurationContext);
    configurationContext.UpdateScreenConfigurationFor((Func<BoundedTo<INProductionEntry, INRegister>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<INProductionEntry, INRegister>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((Action<BoundedTo<INProductionEntry, INRegister>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Update((Expression<Func<INProductionEntry, PXAction<INRegister>>>) (g => g.releaseFromHold), (Func<BoundedTo<INProductionEntry, INRegister>.ActionDefinition.ConfiguratorAction, BoundedTo<INProductionEntry, INRegister>.ActionDefinition.ConfiguratorAction>) (g => g.IsHiddenAlways()));
      actions.Update((Expression<Func<INProductionEntry, PXAction<INRegister>>>) (g => g.putOnHold), (Func<BoundedTo<INProductionEntry, INRegister>.ActionDefinition.ConfiguratorAction, BoundedTo<INProductionEntry, INRegister>.ActionDefinition.ConfiguratorAction>) (g => g.IsHiddenAlways()));
      actions.Update((Expression<Func<INProductionEntry, PXAction<INRegister>>>) (g => g.release), (Func<BoundedTo<INProductionEntry, INRegister>.ActionDefinition.ConfiguratorAction, BoundedTo<INProductionEntry, INRegister>.ActionDefinition.ConfiguratorAction>) (g => g.IsHiddenAlways()));
      actions.Update((Expression<Func<INProductionEntry, PXAction<INRegister>>>) (g => g.iNEdit), (Func<BoundedTo<INProductionEntry, INRegister>.ActionDefinition.ConfiguratorAction, BoundedTo<INProductionEntry, INRegister>.ActionDefinition.ConfiguratorAction>) (g => g.IsHiddenAlways()));
      actions.Update((Expression<Func<INProductionEntry, PXAction<INRegister>>>) (g => g.iNRegisterDetails), (Func<BoundedTo<INProductionEntry, INRegister>.ActionDefinition.ConfiguratorAction, BoundedTo<INProductionEntry, INRegister>.ActionDefinition.ConfiguratorAction>) (g => g.IsHiddenAlways()));
    }))));
  }
}
