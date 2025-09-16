// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.EquipmentModelItemAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

[PXDBInt]
[PXUIField]
public class EquipmentModelItemAttribute(Type whereType, Type[] headers = null) : 
  FSInventoryAttribute(BqlCommand.Compose(new Type[3]
  {
    typeof (Search<,>),
    typeof (PX.Objects.IN.InventoryItem.inventoryID),
    typeof (Where<FSxEquipmentModel.eQEnabled, Equal<True>>)
  }), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr), headers ?? EquipmentModelItemAttribute.defaultHeaders)
{
  private static Type[] defaultHeaders = new Type[6]
  {
    typeof (PX.Objects.IN.InventoryItem.inventoryCD),
    typeof (PX.Objects.IN.InventoryItem.descr),
    typeof (PX.Objects.IN.InventoryItem.itemClassID),
    typeof (PX.Objects.IN.InventoryItem.itemType),
    typeof (PX.Objects.IN.InventoryItem.baseUnit),
    typeof (PX.Objects.IN.InventoryItem.salesUnit)
  };

  public EquipmentModelItemAttribute(Type[] headers = null)
    : this(typeof (Where<True, Equal<True>>), headers)
  {
  }
}
