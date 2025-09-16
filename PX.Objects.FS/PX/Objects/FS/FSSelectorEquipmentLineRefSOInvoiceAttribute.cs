// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorEquipmentLineRefSOInvoiceAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.FS.DAC;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorEquipmentLineRefSOInvoiceAttribute : PXSelectorAttribute
{
  public FSSelectorEquipmentLineRefSOInvoiceAttribute()
    : this(typeof (False))
  {
  }

  public FSSelectorEquipmentLineRefSOInvoiceAttribute(Type withNonActiveRecords)
    : base(((IBqlTemplate) BqlTemplate.OfCommand<Search2<FSEquipmentComponent.lineNbr, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<FSEquipmentComponent.inventoryID>>>, Where2<Where2<Where<FSEquipmentComponent.status, Equal<ListField_Equipment_Status.Active>, Or<BqlPlaceholder.A, Equal<True>>>, And<FSEquipmentComponent.SMequipmentID, Equal<Current<FSxARTran.sMEquipmentID>>>>, And<Where2<Where<FSEquipmentComponent.itemClassID, Equal<PX.Objects.IN.InventoryItem.itemClassID>, Or<Current<FSxARTran.equipmentAction>, Equal<ListField_EquipmentActionBase.None>>>, And<Where<Current<FSxARTran.componentID>, IsNull, Or<FSEquipmentComponent.componentID, Equal<Current<FSxARTran.componentID>>>>>>>>>>.Replace<BqlPlaceholder.A>(withNonActiveRecords)).ToType(), new Type[5]
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
