// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.VehicleTypeMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.FS;

public class VehicleTypeMaint : PXGraph<VehicleTypeMaint, FSVehicleType>
{
  public PXSelect<FSVehicleType> VehicleTypeRecords;
  public PXSelect<FSVehicleType, Where<FSVehicleType.vehicleTypeID, Equal<Current<FSVehicleType.vehicleTypeID>>>> VehicleTypeSelected;
  [PXViewName("Attributes")]
  public CSAttributeGroupList<FSVehicleType, FSVehicle> Mapping;

  [PXDefault]
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
  [NormalizeWhiteSpace]
  [PXUIField]
  [PXSelector(typeof (Search<FSVehicleType.vehicleTypeCD>))]
  protected virtual void FSVehicleType_VehicleTypeCD_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, IsFixed = true)]
  [PXDefault]
  [PXUIField]
  protected virtual void CSAttributeGroup_EntityClassID_CacheAttached(PXCache sender)
  {
  }
}
