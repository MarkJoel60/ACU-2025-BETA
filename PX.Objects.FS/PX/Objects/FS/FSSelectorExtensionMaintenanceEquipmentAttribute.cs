// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorExtensionMaintenanceEquipmentAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorExtensionMaintenanceEquipmentAttribute : PXSelectorAttribute
{
  public FSSelectorExtensionMaintenanceEquipmentAttribute(Type customerID)
    : this(customerID, typeof (False))
  {
  }

  public FSSelectorExtensionMaintenanceEquipmentAttribute(
    Type customerID,
    Type withNonActiveRecords)
    : base(((IBqlTemplate) BqlTemplate.OfCommand<Search2<FSEquipment.SMequipmentID, CrossJoinSingleTable<FSSetup>, Where2<Where<FSSetup.enableAllTargetEquipment, Equal<True>, And<FSEquipment.requireMaintenance, Equal<True>>>, Or<Where2<Where<FSEquipment.status, Equal<ListField_Equipment_Status.Active>, Or<BqlPlaceholder.A, Equal<True>>>, And<Where2<Where<FSEquipment.ownerType, Equal<ListField_OwnerType_Equipment.Customer>, And<FSEquipment.ownerID, Equal<Current<BqlPlaceholder.B>>>>, Or<Where<FSEquipment.ownerType, Equal<ListField_OwnerType_Equipment.OwnCompany>, And<FSEquipment.customerID, Equal<Current<BqlPlaceholder.B>>>>>>>>>>>>.Replace<BqlPlaceholder.A>(withNonActiveRecords).Replace<BqlPlaceholder.B>(customerID)).ToType(), SelectorBase_Equipment.selectorColumns)
  {
    this.SubstituteKey = typeof (FSEquipment.refNbr);
    this.DescriptionField = typeof (FSEquipment.descr);
  }
}
