// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorEquipmentLineRefSalesOrderAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorEquipmentLineRefSalesOrderAttribute : PXSelectorAttribute
{
  public FSSelectorEquipmentLineRefSalesOrderAttribute()
    : base(typeof (Search2<FSEquipmentComponent.lineNbr, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PX.Objects.SO.SOLine.inventoryID>>>>, Where2<Where<FSEquipmentComponent.status, Equal<ListField_Equipment_Status.Active>, And<FSEquipmentComponent.SMequipmentID, Equal<Current<FSxSOLine.sMEquipmentID>>>>, And<Where2<Where<FSEquipmentComponent.itemClassID, Equal<PX.Objects.IN.InventoryItem.itemClassID>, Or<Current<FSxSOLine.equipmentAction>, Equal<ListField_EquipmentActionBase.None>>>, And<Where<Current<FSxSOLine.componentID>, IsNull, Or<FSEquipmentComponent.componentID, Equal<Current<FSxSOLine.componentID>>>>>>>>>), new Type[5]
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
