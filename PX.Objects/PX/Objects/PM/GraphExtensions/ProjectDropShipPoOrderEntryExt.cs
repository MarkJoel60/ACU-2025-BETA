// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.ProjectDropShipPoOrderEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.PO;

#nullable disable
namespace PX.Objects.PM.GraphExtensions;

public class ProjectDropShipPoOrderEntryExt : PXGraphExtension<POOrderEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [PXRemoveBaseAttribute(typeof (POOrderType.ListAttribute))]
  [PXMergeAttributes]
  [POOrderTypeListProjectDropShip]
  protected virtual void _(PX.Data.Events.CacheAttached<POOrder.orderType> e)
  {
  }

  protected void _(
    PX.Data.Events.FieldVerifying<POOrder, POOrder.projectID> e)
  {
    int? newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POOrder, POOrder.projectID>, POOrder, object>) e).NewValue as int?;
    if (!newValue.HasValue || !(e.Row?.OrderType == "PD") || !((PXGraph) this.Base).IsCopyPasteContext || !ProjectDefaultAttribute.IsNonProject(newValue))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POOrder, POOrder.projectID>, POOrder, object>) e).NewValue = (object) null;
  }

  protected void _(PX.Data.Events.FieldVerifying<POLine, POLine.projectID> e)
  {
    int? newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLine, POLine.projectID>, POLine, object>) e).NewValue as int?;
    if (!newValue.HasValue || !(e.Row?.OrderType == "PD") || !((PXGraph) this.Base).IsCopyPasteContext || !ProjectDefaultAttribute.IsNonProject(newValue))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLine, POLine.projectID>, POLine, object>) e).NewValue = (object) null;
  }
}
