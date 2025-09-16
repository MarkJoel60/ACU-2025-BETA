// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipLineUnitAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.SO;

/// <summary>
/// Specific <see cref="T:PX.Objects.IN.INUnitAttribute" /> for additional verifying of selected UOM in <see cref="T:PX.Objects.SO.SOShipLine" /> entity
/// </summary>
public class SOShipLineUnitAttribute : INUnitAttribute
{
  public SOShipLineUnitAttribute()
    : base(INUnitAttribute.VerifyingMode.InventoryUnitConversion)
  {
    this.InventoryType = typeof (SOShipLine.inventoryID);
    Type type = typeof (Search<INUnit.fromUnit, Where<INUnit.unitType, Equal<INUnitType.inventoryItem>, And<INUnit.inventoryID, Equal<Current<SOShipLine.inventoryID>>, And<Where<INUnit.fromUnit, Equal<INUnit.toUnit>, Or<INUnit.fromUnit, Equal<Current<SOShipLine.orderUOM>>>>>>>>);
    this.Init(type, type);
  }

  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null || !this.VerifyOnCopyPaste && sender.Graph.IsCopyPasteContext)
      return;
    SOShipLine row = (SOShipLine) e.Row;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, row.InventoryID);
    if (inventoryItem == null)
    {
      this.UnitVerifying(sender, e, (INUnit) null);
    }
    else
    {
      INUnit unit = INUnit.UK.ByInventory.Find(sender.Graph, inventoryItem.InventoryID, (string) e.NewValue);
      this.UnitVerifying(sender, e, unit);
      if (!EnumerableExtensions.IsIn<string>(unit.FromUnit, row.OrderUOM, unit.ToUnit))
        throw new PXSetPropertyException("The UOM can be changed only to a base one or to UOM originally used in the sales order.");
    }
  }
}
