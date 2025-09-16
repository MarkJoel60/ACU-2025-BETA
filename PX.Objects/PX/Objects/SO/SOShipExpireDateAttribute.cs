// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipExpireDateAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.SO;

public class SOShipExpireDateAttribute(Type InventoryType) : INExpireDateAttribute(InventoryType)
{
  public override void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(sender, ((ILSMaster) e.Row).InventoryID);
    if (pxResult == null || !(PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerTrack != "N"))
      return;
    bool? nullable = (bool?) sender.GetValue<SOShipLineSplit.confirmed>(e.Row);
    if (!(PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerAssign != "U") && !nullable.GetValueOrDefault())
      return;
    base.RowPersisting(sender, e);
  }
}
