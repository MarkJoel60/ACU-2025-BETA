// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorContractPeriodEquipmentAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorContractPeriodEquipmentAttribute : PXSelectorAttribute
{
  public FSSelectorContractPeriodEquipmentAttribute()
    : base(typeof (Search2<FSEquipment.SMequipmentID, CrossJoinSingleTable<FSSetup>, Where<FSEquipment.requireMaintenance, Equal<True>, And<FSSetup.enableAllTargetEquipment, Equal<True>, Or<Where2<Where<FSEquipment.ownerType, Equal<ListField_OwnerType_Equipment.Customer>, And<Where2<Where<FSEquipment.ownerID, Equal<Current<FSServiceContract.customerID>>>, Or<Where<FSEquipment.locationType, Equal<ListField_LocationType.Customer>, And<FSEquipment.customerID, Equal<Current<FSServiceContract.customerID>>, And<Where2<Where<FSEquipment.customerLocationID, Equal<Current<FSServiceContract.customerLocationID>>>, Or<Where<FSEquipment.customerLocationID, IsNull>>>>>>>>>>, Or<Where<FSEquipment.ownerType, Equal<ListField_OwnerType_Equipment.OwnCompany>, And<FSEquipment.locationType, Equal<ListField_LocationType.Customer>, And<FSEquipment.customerID, Equal<Current<FSServiceContract.customerID>>, And<Where2<Where<FSEquipment.customerLocationID, Equal<Current<FSServiceContract.customerLocationID>>>, Or<Where<FSEquipment.customerLocationID, IsNull>>>>>>>>>>>>>), SelectorBase_Equipment.selectorColumns)
  {
    this.SubstituteKey = typeof (FSEquipment.refNbr);
    this.DescriptionField = typeof (FSEquipment.descr);
  }
}
