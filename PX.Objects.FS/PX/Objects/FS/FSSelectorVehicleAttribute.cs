// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorVehicleAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorVehicleAttribute : PXSelectorAttribute
{
  public FSSelectorVehicleAttribute()
    : base(typeof (Search<FSVehicle.SMequipmentID, Where<FSEquipment.isVehicle, Equal<True>>, OrderBy<Asc<FSEquipment.refNbr>>>), new Type[9]
    {
      typeof (FSEquipment.refNbr),
      typeof (FSEquipment.status),
      typeof (FSEquipment.vehicleTypeID),
      typeof (FSEquipment.descr),
      typeof (FSEquipment.registrationNbr),
      typeof (FSVehicle.manufacturerModelID),
      typeof (FSVehicle.manufacturerID),
      typeof (FSEquipment.manufacturingYear),
      typeof (FSEquipment.color)
    })
  {
    this.SubstituteKey = typeof (FSEquipment.refNbr);
    this.DescriptionField = typeof (FSEquipment.descr);
  }
}
