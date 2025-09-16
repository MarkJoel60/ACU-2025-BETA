// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.SOSiteAvailAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.SO;

#nullable disable
namespace PX.Objects.IN;

[PXDBInt]
[PXUIField]
public class SOSiteAvailAttribute : SiteAvailAttribute
{
  public SOSiteAvailAttribute()
    : base(typeof (PX.Objects.SO.SOLine.inventoryID), typeof (PX.Objects.SO.SOLine.subItemID), typeof (PX.Objects.SO.SOLine.costCenterID))
  {
  }

  public override void InventoryID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    using (new SOOrderPriceCalculationScope().AppendContext<PX.Objects.SO.SOLine.inventoryID>())
    {
      sender.SetDefaultExt<PX.Objects.SO.SOLine.uOM>(e.Row);
      base.InventoryID_FieldUpdated(sender, e);
    }
  }
}
