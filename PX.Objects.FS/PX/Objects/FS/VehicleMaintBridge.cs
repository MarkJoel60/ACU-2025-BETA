// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.VehicleMaintBridge
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.EP;

#nullable disable
namespace PX.Objects.FS;

public class VehicleMaintBridge : PXGraph<VehicleMaintBridge, FSVehicle>
{
  public PXSelect<FSVehicle, Where<FSEquipment.isVehicle, Equal<True>>> VehicleRecords;

  protected virtual void _(PX.Data.Events.RowSelected<FSVehicle> e)
  {
    if (e.Row != null)
    {
      FSVehicle row = e.Row;
      VehicleMaint instance = PXGraph.CreateInstance<VehicleMaint>();
      if (row.SMEquipmentID.HasValue)
        ((PXSelectBase<EPEquipment>) instance.EPEquipmentRecords).Current = PXResultset<EPEquipment>.op_Implicit(PXSelectBase<EPEquipment, PXSelectJoin<EPEquipment, InnerJoin<FSEquipment, On<FSEquipment.sourceID, Equal<EPEquipment.equipmentID>, And<FSEquipment.sourceType, Equal<ListField_SourceType_Equipment.Vehicle>>>>, Where<FSEquipment.SMequipmentID, Equal<Required<FSEquipment.SMequipmentID>>>>.Config>.Select((PXGraph) instance, new object[1]
        {
          (object) row.SMEquipmentID
        }));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, (string) null);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }
}
