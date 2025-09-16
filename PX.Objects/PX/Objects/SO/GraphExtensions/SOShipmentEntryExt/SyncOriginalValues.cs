// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.SyncOriginalValues
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

public class SyncOriginalValues : PXGraphExtension<SOShipmentEntry>
{
  protected virtual void _(
    Events.FieldSelecting<SOShipLine, SOShipLine.fullOrderQty> e)
  {
    if (e.Row?.UOM == e.Row?.OrderUOM)
      return;
    ((Events.FieldSelectingBase<Events.FieldSelecting<SOShipLine, SOShipLine.fullOrderQty>>) e).ReturnValue = (object) e.Row.BaseFullOrderQty;
  }

  protected virtual void _(
    Events.FieldSelecting<SOShipLine, SOShipLine.fullOpenQty> e)
  {
    if (e.Row?.UOM == e.Row?.OrderUOM)
      return;
    ((Events.FieldSelectingBase<Events.FieldSelecting<SOShipLine, SOShipLine.fullOpenQty>>) e).ReturnValue = (object) e.Row.BaseFullOpenQty;
  }
}
