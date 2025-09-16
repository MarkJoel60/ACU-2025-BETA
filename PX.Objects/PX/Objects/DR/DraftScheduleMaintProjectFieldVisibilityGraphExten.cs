// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DraftScheduleMaintProjectFieldVisibilityGraphExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.DR;

public class DraftScheduleMaintProjectFieldVisibilityGraphExtension : 
  PXGraphExtension<DraftScheduleMaint>
{
  protected virtual void _(Events.RowSelected<DRSchedule> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetVisible<DRSchedule.projectID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<DRSchedule>>) e).Cache, (object) e.Row, PXAccess.FeatureInstalled<FeaturesSet.contractManagement>() || PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>());
    PXUIFieldAttribute.SetDisplayName<DRSchedule.projectID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<DRSchedule>>) e).Cache, "Project/Contract");
  }
}
