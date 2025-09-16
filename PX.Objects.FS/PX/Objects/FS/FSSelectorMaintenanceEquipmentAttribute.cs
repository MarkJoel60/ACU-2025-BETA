// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelectorMaintenanceEquipmentAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelectorMaintenanceEquipmentAttribute : PXSelectorAttribute
{
  public FSSelectorMaintenanceEquipmentAttribute(
    Type srvOrdType,
    Type billCustomerID,
    Type customerID,
    Type customerLocationID,
    Type branchLocationID)
    : this(srvOrdType, billCustomerID, customerID, customerLocationID, typeof (AccessInfo.branchID), branchLocationID)
  {
  }

  public FSSelectorMaintenanceEquipmentAttribute(
    Type srvOrdType,
    Type billCustomerID,
    Type customerID,
    Type customerLocationID,
    Type branchID,
    Type branchLocationID)
    : base(BqlCommand.Compose(new Type[79]
    {
      typeof (Search2<,,>),
      typeof (FSEquipment.SMequipmentID),
      typeof (InnerJoin<,,>),
      typeof (FSSrvOrdType),
      typeof (On<,>),
      typeof (FSSrvOrdType.srvOrdType),
      typeof (Equal<>),
      typeof (Current<>),
      srvOrdType,
      typeof (CrossJoinSingleTable<>),
      typeof (FSSetup),
      typeof (Where<,,>),
      typeof (FSEquipment.requireMaintenance),
      typeof (Equal<True>),
      typeof (And<>),
      typeof (Where2<,>),
      typeof (Where<FSSetup.enableAllTargetEquipment, Equal<True>>),
      typeof (Or<>),
      typeof (Where2<,>),
      typeof (Where<,,>),
      typeof (FSEquipment.locationType),
      typeof (Equal<ListField_LocationType.Customer>),
      typeof (And2<,>),
      typeof (Where<,,>),
      typeof (FSEquipment.customerID),
      typeof (Equal<>),
      typeof (Current<>),
      customerID,
      typeof (And<>),
      typeof (Where<,,>),
      typeof (FSEquipment.customerLocationID),
      typeof (Equal<>),
      typeof (Current<>),
      customerLocationID,
      typeof (Or<FSEquipment.customerLocationID, IsNull>),
      typeof (And<>),
      typeof (Where2<,>),
      typeof (Where<,,>),
      typeof (FSEquipment.ownerType),
      typeof (Equal<ListField_OwnerType_Equipment.Customer>),
      typeof (And<,>),
      typeof (FSEquipment.ownerID),
      typeof (Equal<>),
      typeof (Current<>),
      billCustomerID,
      typeof (Or<Where<FSEquipment.ownerType, Equal<ListField_OwnerType_Equipment.OwnCompany>>>),
      typeof (Or<>),
      typeof (Where<,,>),
      typeof (FSEquipment.locationType),
      typeof (Equal<ListField_LocationType.Company>),
      typeof (And<,,>),
      typeof (FSEquipment.branchID),
      typeof (Equal<>),
      typeof (Current<>),
      branchID,
      typeof (And2<,>),
      typeof (Where<,,>),
      typeof (FSEquipment.branchLocationID),
      typeof (Equal<>),
      typeof (Current<>),
      branchLocationID,
      typeof (Or<FSEquipment.branchLocationID, IsNull>),
      typeof (And<>),
      typeof (Where2<,>),
      typeof (Where<,,>),
      typeof (FSEquipment.ownerType),
      typeof (Equal<ListField_OwnerType_Equipment.Customer>),
      typeof (And<,>),
      typeof (FSEquipment.ownerID),
      typeof (Equal<>),
      typeof (Current<>),
      billCustomerID,
      typeof (Or<>),
      typeof (Where<,,>),
      typeof (FSEquipment.ownerType),
      typeof (Equal<ListField_OwnerType_Equipment.OwnCompany>),
      typeof (And<,>),
      typeof (FSSrvOrdType.behavior),
      typeof (Equal<ListField.ServiceOrderTypeBehavior.internalAppointment>)
    }), SelectorBase_Equipment.selectorColumns)
  {
    this.SubstituteKey = typeof (FSEquipment.refNbr);
    this.DescriptionField = typeof (FSEquipment.descr);
  }
}
