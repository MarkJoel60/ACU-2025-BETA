// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.ShippingZoneMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.CS;

public class ShippingZoneMaint : PXGraph<ShippingZoneMaint>
{
  public PXSavePerRow<ShippingZone> Save;
  public PXCancel<ShippingZone> Cancel;
  [PXImport(typeof (ShippingZone))]
  public PXSelect<ShippingZone> ShippingZones;

  protected virtual void ShippingZone_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    if (PXResultset<ShippingZone>.op_Implicit(PXSelectBase<ShippingZone, PXSelect<ShippingZone, Where<ShippingZone.zoneID, Equal<Required<ShippingZone.zoneID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) ((ShippingZone) e.Row).ZoneID
    })) == null)
      return;
    cache.RaiseExceptionHandling<ShippingZone.zoneID>(e.Row, (object) ((ShippingZone) e.Row).ZoneID, (Exception) new PXException("The record already exists."));
    ((CancelEventArgs) e).Cancel = true;
  }
}
