// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorServiceOrderResourceEquipmentAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorServiceOrderResourceEquipmentAttribute : PXSelectorAttribute
{
  public FSSelectorServiceOrderResourceEquipmentAttribute()
    : base(typeof (Search<FSEquipment.SMequipmentID, Where<FSEquipment.resourceEquipment, Equal<True>, And<Where2<Where2<Where<FSEquipment.locationType, Equal<ListField_LocationType.Company>, And<FSEquipment.branchID, Equal<Current<FSServiceOrder.branchID>>, And<FSEquipment.branchLocationID, Equal<Current<FSServiceOrder.branchLocationID>>>>>, Or<Where<FSEquipment.locationType, Equal<ListField_LocationType.Company>, And<FSEquipment.branchID, Equal<Current<FSServiceOrder.branchID>>, And<FSEquipment.branchLocationID, IsNull>>>>>, Or2<Where<FSEquipment.locationType, Equal<ListField_LocationType.Customer>, And<FSEquipment.customerID, Equal<Current<FSServiceOrder.customerID>>, And<FSEquipment.customerLocationID, Equal<Current<FSServiceOrder.locationID>>>>>, Or<Where<FSEquipment.locationType, Equal<ListField_LocationType.Customer>, And<FSEquipment.customerID, Equal<Current<FSServiceOrder.customerID>>, And<FSEquipment.customerLocationID, IsNull>>>>>>>>>), SelectorBase_Equipment.selectorColumns)
  {
    this.SubstituteKey = typeof (FSEquipment.refNbr);
    this.DescriptionField = typeof (FSEquipment.descr);
  }
}
