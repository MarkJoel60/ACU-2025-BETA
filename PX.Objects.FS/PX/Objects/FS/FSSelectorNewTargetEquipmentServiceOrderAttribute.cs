// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorNewTargetEquipmentServiceOrderAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorNewTargetEquipmentServiceOrderAttribute : PXSelectorAttribute
{
  public FSSelectorNewTargetEquipmentServiceOrderAttribute()
    : base(typeof (Search2<FSSODet.lineRef, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<FSSODet.inventoryID>>>, Where<FSxEquipmentModel.equipmentItemClass, Equal<ListField_EquipmentItemClass.ModelEquipment>, And<FSSODet.equipmentAction, Equal<ListField_EquipmentActionBase.SellingTargetEquipment>, And<FSSODet.sOID, Equal<Current<FSServiceOrder.sOID>>>>>>), new Type[2]
    {
      typeof (FSSODet.lineRef),
      typeof (FSSODet.inventoryID)
    })
  {
  }
}
