// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorEquipmentLineRefServiceOrderAppointmentAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorEquipmentLineRefServiceOrderAppointmentAttribute : PXSelectorAttribute
{
  public FSSelectorEquipmentLineRefServiceOrderAppointmentAttribute(
    Type inventoryID,
    Type smEquipmentID,
    Type componentID,
    Type equipmentAction)
    : base(BqlCommand.Compose(new Type[39]
    {
      typeof (Search2<,,>),
      typeof (FSEquipmentComponent.lineNbr),
      typeof (LeftJoin<,>),
      typeof (PX.Objects.IN.InventoryItem),
      typeof (On<,>),
      typeof (PX.Objects.IN.InventoryItem.inventoryID),
      typeof (Equal<>),
      typeof (Current<>),
      inventoryID,
      typeof (Where2<,>),
      typeof (Where<,,>),
      typeof (FSEquipmentComponent.SMequipmentID),
      typeof (Equal<>),
      typeof (Current<>),
      smEquipmentID,
      typeof (And<FSEquipmentComponent.status, Equal<ListField_Equipment_Status.Active>>),
      typeof (And<>),
      typeof (Where2<,>),
      typeof (Where<,,>),
      typeof (FSEquipmentComponent.itemClassID),
      typeof (Equal<PX.Objects.IN.InventoryItem.itemClassID>),
      typeof (Or<,,>),
      typeof (Current<>),
      inventoryID,
      typeof (IsNull),
      typeof (Or<,>),
      typeof (Current<>),
      equipmentAction,
      typeof (Equal<ListField_EquipmentActionBase.None>),
      typeof (And<>),
      typeof (Where<,,>),
      typeof (FSEquipmentComponent.componentID),
      typeof (Equal<>),
      typeof (Current<>),
      componentID,
      typeof (Or<,>),
      typeof (Current<>),
      componentID,
      typeof (IsNull)
    }), new Type[5]
    {
      typeof (FSEquipmentComponent.lineRef),
      typeof (FSEquipmentComponent.componentID),
      typeof (FSEquipmentComponent.longDescr),
      typeof (FSEquipmentComponent.serialNumber),
      typeof (FSEquipmentComponent.comment)
    })
  {
    this.SubstituteKey = typeof (FSEquipmentComponent.lineRef);
  }
}
