// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorNewTargetEquipmentAppointmentAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorNewTargetEquipmentAppointmentAttribute : PXSelectorAttribute
{
  public FSSelectorNewTargetEquipmentAppointmentAttribute()
    : base(typeof (Search2<FSAppointmentDet.lineRef, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<FSAppointmentDet.inventoryID>>>, Where<FSxEquipmentModel.equipmentItemClass, Equal<ListField_EquipmentItemClass.ModelEquipment>, And<FSAppointmentDet.lineType, Equal<FSLineType.Inventory_Item>, And<FSAppointmentDet.equipmentAction, Equal<ListField_EquipmentActionBase.SellingTargetEquipment>, And<FSAppointmentDet.appointmentID, Equal<Current<FSAppointment.appointmentID>>, And<FSAppointmentDet.lineRef, IsNotNull>>>>>>), new Type[2]
    {
      typeof (FSAppointmentDet.lineRef),
      typeof (FSAppointmentDet.inventoryID)
    })
  {
    this.DirtyRead = true;
  }
}
