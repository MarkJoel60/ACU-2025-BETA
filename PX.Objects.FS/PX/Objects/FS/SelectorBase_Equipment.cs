// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SelectorBase_Equipment
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System;

#nullable disable
namespace PX.Objects.FS;

public static class SelectorBase_Equipment
{
  public static Type[] selectorColumns = new Type[14]
  {
    typeof (FSEquipment.refNbr),
    typeof (FSEquipment.descr),
    typeof (FSEquipment.serialNumber),
    typeof (FSEquipment.ownerType),
    typeof (FSEquipment.ownerID),
    typeof (FSEquipment.locationType),
    typeof (FSEquipment.customerID),
    typeof (FSEquipment.customerLocationID),
    typeof (FSEquipment.branchID),
    typeof (FSEquipment.branchLocationID),
    typeof (FSEquipment.inventoryID),
    typeof (FSEquipment.iNSerialNumber),
    typeof (FSEquipment.color),
    typeof (FSEquipment.status)
  };
}
