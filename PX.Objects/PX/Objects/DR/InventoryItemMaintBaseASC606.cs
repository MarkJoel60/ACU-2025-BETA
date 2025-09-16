// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.InventoryItemMaintBaseASC606
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;

#nullable disable
namespace PX.Objects.DR;

public class InventoryItemMaintBaseASC606 : PXGraphExtension<InventoryItemMaintBase>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.aSC606>();

  protected virtual void _(
    Events.FieldUpdated<INComponent, INComponent.deferredCode> e)
  {
    if (e.Row == null)
      return;
    DRDeferredCode code = (DRDeferredCode) PXSelectorAttribute.Select(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INComponent, INComponent.deferredCode>>) e).Cache, (object) e.Row, typeof (INComponent.deferredCode).Name);
    if (code != null)
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INComponent, INComponent.deferredCode>>) e).Cache.SetValueExt<INComponent.overrideDefaultTerm>((object) e.Row, (object) DeferredMethodType.RequiresTerms(code));
    else
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INComponent, INComponent.deferredCode>>) e).Cache.SetValueExt<INComponent.overrideDefaultTerm>((object) e.Row, (object) false);
  }

  protected virtual void _(
    Events.FieldUpdated<INComponent, INComponent.componentID> e)
  {
    if (e.Row == null)
      return;
    DRDeferredCode code = (DRDeferredCode) PXSelectorAttribute.Select(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INComponent, INComponent.componentID>>) e).Cache, (object) e.Row, typeof (INComponent.deferredCode).Name);
    if (code != null)
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INComponent, INComponent.componentID>>) e).Cache.SetValueExt<INComponent.overrideDefaultTerm>((object) e.Row, (object) DeferredMethodType.RequiresTerms(code));
    else
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<INComponent, INComponent.componentID>>) e).Cache.SetValueExt<INComponent.overrideDefaultTerm>((object) e.Row, (object) false);
  }
}
