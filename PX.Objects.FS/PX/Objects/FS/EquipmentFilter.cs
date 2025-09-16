// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.EquipmentFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class EquipmentFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  [PXUIField(DisplayName = "Customer ID")]
  [FSSelectorCustomer]
  public virtual int? CustomerID { get; set; }

  [PXInt]
  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<EquipmentFilter.customerID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), DisplayName = "Customer Location ID", DirtyRead = true)]
  [PXDefault(typeof (Search<BAccount.defLocationID, Where<BAccount.bAccountID, Equal<Current<EquipmentFilter.customerID>>>>))]
  [PXFormula(typeof (Default<EquipmentFilter.customerID>))]
  public virtual int? CustomerLocationID { get; set; }

  [EquipmentModelItem(null, Filterable = true)]
  public virtual int? InventoryID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Owner ID")]
  [FSSelectorCustomer]
  public virtual int? OwnerID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Equipment Nbr.")]
  [FSSelectorSMEquipmentRefNbr]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Show only maintenance equipment")]
  public virtual bool? RequireMaintenance { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Show only resource equipment")]
  public virtual bool? ResourceEquipment { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Show only equipment without warranties and components")]
  public virtual bool? WarrantyLess { get; set; }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EquipmentFilter.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    EquipmentFilter.customerLocationID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EquipmentFilter.inventoryID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EquipmentFilter.ownerID>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EquipmentFilter.refNbr>
  {
  }

  public abstract class requireMaintenance : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EquipmentFilter.requireMaintenance>
  {
  }

  public abstract class resourceEquipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    EquipmentFilter.resourceEquipment>
  {
  }

  public abstract class warrantyLess : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EquipmentFilter.warrantyLess>
  {
  }
}
