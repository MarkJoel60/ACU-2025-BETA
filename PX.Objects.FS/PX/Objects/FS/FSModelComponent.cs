// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSModelComponent
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Model Warranty")]
[Serializable]
public class FSModelComponent : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (PX.Objects.IN.InventoryItem.inventoryID))]
  [PXParent(typeof (Select<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<FSModelComponent.modelID>>>>))]
  public virtual int? ModelID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Component ID")]
  [FSSelectorComponentID]
  public virtual int? ComponentID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active { get; set; }

  [PXDBLocalizableString(250, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual 
  #nullable disable
  string Descr { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Requires Serial")]
  public virtual bool? RequireSerial { get; set; }

  [PXDBInt(MinValue = 0)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Vendor Warranty")]
  public virtual int? VendorWarrantyValue { get; set; }

  [PXDBString(1, IsFixed = true)]
  [ListField_WarrantyDurationType.ListAtrribute]
  [PXDefault("M")]
  [PXUIField(DisplayName = "Vendor Warranty Type")]
  public virtual string VendorWarrantyType { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Vendor ID")]
  [FSSelectorBusinessAccount_VE]
  public virtual int? VendorID { get; set; }

  [PXDBInt(MinValue = 0)]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Company Warranty")]
  public virtual int? CpnyWarrantyValue { get; set; }

  [PXDBString(1, IsFixed = true)]
  [ListField_WarrantyDurationType.ListAtrribute]
  [PXDefault("M")]
  [PXUIField(DisplayName = "Company Warranty Type")]
  public virtual string CpnyWarrantyType { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Item Class ID")]
  [PXDefault]
  [PXSelector(typeof (Search2<INItemClass.itemClassID, InnerJoin<FSModelTemplateComponent, On<FSModelTemplateComponent.classID, Equal<INItemClass.itemClassID>>>, Where<FSModelTemplateComponent.componentID, Equal<Current<FSModelComponent.componentID>>, And<FSModelTemplateComponent.modelTemplateID, Equal<Current<PX.Objects.IN.InventoryItem.itemClassID>>, And<FSxEquipmentModelTemplate.equipmentItemClass, Equal<ListField_EquipmentItemClass.Component>>>>>), SubstituteKey = typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr))]
  public virtual int? ClassID { get; set; }

  [PXDBInt(MinValue = 1)]
  [PXUIField(DisplayName = "Quantity")]
  public virtual int? Qty { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Optional { get; set; }

  [PXDefault]
  [StockItem(Enabled = true)]
  [PXRestrictor(typeof (Where<FSxEquipmentModel.equipmentItemClass, Equal<ListField_EquipmentItemClass.Component>, And<PX.Objects.IN.InventoryItem.itemClassID, Equal<Current<FSModelComponent.classID>>>>), "The selected inventory ID should be the same as the class ID for this component. Select another one.", new Type[] {})]
  public virtual int? InventoryID { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<FSModelComponent>.By<FSModelComponent.modelID, FSModelComponent.componentID>
  {
    public static FSModelComponent Find(
      PXGraph graph,
      int? modelID,
      int? componentID,
      PKFindOptions options = 0)
    {
      return (FSModelComponent) PrimaryKeyOf<FSModelComponent>.By<FSModelComponent.modelID, FSModelComponent.componentID>.FindBy(graph, (object) modelID, (object) componentID, options);
    }
  }

  public static class FK
  {
    public class Model : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSModelComponent>.By<FSModelComponent.modelID>
    {
    }

    public class Component : 
      PrimaryKeyOf<FSModelTemplateComponent>.By<FSModelTemplateComponent.componentID>.ForeignKeyOf<FSModelComponent>.By<FSModelComponent.componentID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<FSModelComponent>.By<FSModelComponent.vendorID>
    {
    }

    public class ItemClass : 
      PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>.ForeignKeyOf<FSModelComponent>.By<FSModelComponent.classID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSModelComponent>.By<FSModelComponent.inventoryID>
    {
    }
  }

  public abstract class modelID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSModelComponent.modelID>
  {
  }

  public abstract class componentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSModelComponent.componentID>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSModelComponent.active>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSModelComponent.descr>
  {
  }

  public abstract class requireSerial : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSModelComponent.requireSerial>
  {
  }

  public abstract class vendorWarrantyValue : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSModelComponent.vendorWarrantyValue>
  {
  }

  public abstract class vendorWarrantyType : ListField_WarrantyDurationType
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSModelComponent.vendorID>
  {
  }

  public abstract class cpnyWarrantyValue : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSModelComponent.cpnyWarrantyValue>
  {
  }

  public abstract class cpnyWarrantyType : ListField_WarrantyDurationType
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSModelComponent.classID>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSModelComponent.qty>
  {
  }

  public abstract class optional : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSModelComponent.optional>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSModelComponent.inventoryID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSModelComponent.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSModelComponent.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSModelComponent.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSModelComponent.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSModelComponent.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSModelComponent.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSModelComponent.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSModelComponent.Tstamp>
  {
  }
}
