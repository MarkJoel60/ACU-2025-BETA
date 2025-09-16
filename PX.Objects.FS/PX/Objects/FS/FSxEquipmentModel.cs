// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxEquipmentModel
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.FS;

public class FSxEquipmentModel : PXCacheExtension<
#nullable disable
PX.Objects.IN.InventoryItem>
{
  protected string _EquipmentItemClass;

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.equipmentManagementModule>();
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Model Equipment")]
  public virtual bool? EQEnabled { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Manufacturer")]
  [PXSelector(typeof (FSManufacturer.manufacturerID), SubstituteKey = typeof (FSManufacturer.manufacturerCD), DescriptionField = typeof (FSManufacturer.descr))]
  public virtual int? ManufacturerID { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<FSManufacturerModel.manufacturerModelID, Where<FSManufacturerModel.manufacturerID, Equal<Current<FSxEquipmentModel.manufacturerID>>>>), SubstituteKey = typeof (FSManufacturerModel.manufacturerModelCD), DescriptionField = typeof (FSManufacturerModel.descr))]
  [PXFormula(typeof (Default<FSxEquipmentModel.manufacturerID>))]
  public virtual int? ManufacturerModelID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [ListField_ModelType.ListAtrribute]
  [PXDefault("EQ")]
  [PXUIField(DisplayName = "Model Type")]
  public virtual string ModelType { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("OI")]
  [PXUIField(DisplayName = "Equipment Class")]
  [ListField_EquipmentItemClass.ListAtrribute]
  public virtual string EquipmentItemClass
  {
    get => this._EquipmentItemClass;
    set
    {
      this._EquipmentItemClass = value;
      if (this._EquipmentItemClass == "ME" || this._EquipmentItemClass == "CT")
        this.EQEnabled = new bool?(true);
      else
        this.EQEnabled = new bool?(false);
    }
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Equipment Type")]
  [FSSelectorEquipmentType]
  [PXDefault]
  public virtual int? EquipmentTypeID { get; set; }

  [PXDBInt(MinValue = 0)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Company Warranty")]
  public virtual int? CpnyWarrantyValue { get; set; }

  [PXDBString(1, IsFixed = true)]
  [ListField_WarrantyDurationType.ListAtrribute]
  [PXDefault("M")]
  [PXUIField(DisplayName = "Company Warranty Type")]
  public virtual string CpnyWarrantyType { get; set; }

  [PXDBInt(MinValue = 0)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Vendor Warranty")]
  public virtual int? VendorWarrantyValue { get; set; }

  [PXDBString(1, IsFixed = true)]
  [ListField_WarrantyDurationType.ListAtrribute]
  [PXDefault("M")]
  [PXUIField(DisplayName = "Vendor Warranty Type")]
  public virtual string VendorWarrantyType { get; set; }

  [PXBool]
  [PXUIField(Visible = false)]
  [PXDefault]
  public virtual bool? chkEquipmentManagement => new bool?(true);

  [PXBool]
  [PXDefault(false)]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual bool Mem_ShowComponent { get; set; }

  public abstract class eQEnabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSxEquipmentModel.eQEnabled>
  {
  }

  public abstract class manufacturerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxEquipmentModel.manufacturerID>
  {
  }

  public abstract class manufacturerModelID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxEquipmentModel.manufacturerModelID>
  {
  }

  public abstract class modelType : ListField_ModelType
  {
  }

  public abstract class equipmentItemClass : ListField_EquipmentItemClass
  {
  }

  public abstract class equipmentTypeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxEquipmentModel.equipmentTypeID>
  {
  }

  public abstract class cpnyWarrantyValue : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxEquipmentModel.cpnyWarrantyValue>
  {
  }

  public abstract class cpnyWarrantyType : ListField_WarrantyDurationType
  {
  }

  public abstract class vendorWarrantyValue : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxEquipmentModel.vendorWarrantyValue>
  {
  }

  public abstract class vendorWarrantyType : ListField_WarrantyDurationType
  {
  }

  public abstract class ChkEquipmentManagement : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxEquipmentModel.ChkEquipmentManagement>
  {
  }

  public abstract class mem_ShowComponent : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxEquipmentModel.mem_ShowComponent>
  {
  }
}
