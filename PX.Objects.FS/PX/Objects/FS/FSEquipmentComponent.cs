// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSEquipmentComponent
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("FSEquipmentComponent")]
[Serializable]
public class FSEquipmentComponent : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXParent(typeof (Select<FSEquipment, Where<FSEquipment.SMequipmentID, Equal<Current<FSEquipmentComponent.SMequipmentID>>>>))]
  [PXDBDefault(typeof (FSEquipment.SMequipmentID))]
  [PXUIField(DisplayName = "Equipment ID")]
  public virtual int? SMEquipmentID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (FSEquipment))]
  public virtual int? LineNbr { get; set; }

  [PXDBString(5, IsFixed = true)]
  [PXUIField]
  public virtual 
  #nullable disable
  string LineRef { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Component ID")]
  [FSSelectorComponentIDEquipment]
  public virtual int? ComponentID { get; set; }

  [PXDBInt(MinValue = 0)]
  [PXUIField(DisplayName = "Company Warranty")]
  public virtual int? CpnyWarrantyDuration { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Company Warranty End Date", Enabled = false)]
  public virtual DateTime? CpnyWarrantyEndDate { get; set; }

  [PXDBDate]
  [PXDefault(typeof (Search<FSEquipment.dateInstalled, Where<FSEquipment.SMequipmentID, Equal<Current<FSEquipmentComponent.SMequipmentID>>>>))]
  [PXUIField(DisplayName = "Installation Date", Visible = false)]
  public virtual DateTime? InstallationDate { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Last Replacement Date", Enabled = false)]
  public virtual DateTime? LastReplacementDate { get; set; }

  [PXDBLocalizableString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string LongDescr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Vendor ID")]
  [FSSelectorBusinessAccount_VE]
  public virtual int? VendorID { get; set; }

  [PXDBInt(MinValue = 0)]
  [PXUIField(DisplayName = "Vendor Warranty")]
  public virtual int? VendorWarrantyDuration { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Vendor Warranty End Date", Enabled = false)]
  public virtual DateTime? VendorWarrantyEndDate { get; set; }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Serial Nbr.")]
  [PXDefault]
  public virtual string SerialNumber { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Requires Serial Nbr.")]
  public virtual bool? RequireSerial { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Item Class ID")]
  [PXDefault]
  [PXSelector(typeof (Search2<INItemClass.itemClassID, InnerJoin<FSModelComponent, On<FSModelComponent.classID, Equal<INItemClass.itemClassID>>>, Where<FSModelComponent.componentID, Equal<Current<FSEquipmentComponent.componentID>>, And<FSModelComponent.modelID, Equal<Current<FSEquipment.inventoryID>>, And<FSxEquipmentModelTemplate.equipmentItemClass, Equal<ListField_EquipmentItemClass.Component>>>>>), SubstituteKey = typeof (INItemClass.itemClassCD))]
  public virtual int? ItemClassID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Inventory ID")]
  [PXDimensionSelector("INVENTORY", typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where2<Where<Current<FSEquipmentComponent.itemClassID>, IsNotNull, And<PX.Objects.IN.InventoryItem.itemClassID, Equal<Current<FSEquipmentComponent.itemClassID>>, Or<Where<Current<FSEquipmentComponent.itemClassID>, IsNull>>>>, And<FSxEquipmentModel.equipmentItemClass, Equal<ListField_EquipmentItemClass.Component>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.markedForDeletion>>>), "The inventory item is {0}.", new Type[] {typeof (PX.Objects.IN.InventoryItem.itemStatus)}, ShowWarning = true)]
  public virtual int? InventoryID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [ListField_WarrantyDurationType.ListAtrribute]
  [PXDefault("M")]
  [PXUIField(DisplayName = "Company Warranty Type")]
  public virtual string CpnyWarrantyType { get; set; }

  [PXDBString(1, IsFixed = true)]
  [ListField_WarrantyDurationType.ListAtrribute]
  [PXDefault("M")]
  [PXUIField(DisplayName = "Vendor Warranty Type")]
  public virtual string VendorWarrantyType { get; set; }

  [PXDBDate]
  [PXDefault(typeof (Search<FSEquipment.salesDate, Where<FSEquipment.SMequipmentID, Equal<Current<FSEquipmentComponent.SMequipmentID>>>>))]
  [PXUIField(DisplayName = "Sales Date", Visible = false)]
  public virtual DateTime? SalesDate { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type")]
  public virtual string InstSrvOrdType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (Search<FSServiceOrder.refNbr, Where<FSServiceOrder.srvOrdType, Equal<Current<FSEquipmentComponent.instSrvOrdType>>>>), SubstituteKey = typeof (FSServiceOrder.refNbr), DescriptionField = typeof (FSServiceOrder.docDesc))]
  [PXUIField(DisplayName = "Installation Service Order Nbr.", Enabled = false)]
  public virtual string InstServiceOrderRefNbr { get; set; }

  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<FSAppointment.refNbr, Where<FSAppointment.srvOrdType, Equal<Current<FSEquipmentComponent.instSrvOrdType>>>>), SubstituteKey = typeof (FSAppointment.refNbr), DescriptionField = typeof (FSAppointment.docDesc))]
  [PXUIField(DisplayName = "Installation Appointment Nbr.", Enabled = false)]
  public virtual string InstAppointmentRefNbr { get; set; }

  [PXDBString(15, IsUnicode = false)]
  [PXUIField(DisplayName = "Invoice Reference Nbr.", Enabled = false, Visible = false)]
  [PXSelector(typeof (Search<PX.Objects.AR.ARInvoice.refNbr, Where<PX.Objects.AR.ARInvoice.docType, Equal<ARDocType.invoice>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Current<FSEquipmentComponent.invoiceRefNbr>>>>>), SubstituteKey = typeof (PX.Objects.AR.ARInvoice.refNbr), ValidateValue = false)]
  public virtual string InvoiceRefNbr { get; set; }

  [PXDBString(3, IsFixed = true)]
  [PXUIField(DisplayName = "Sales Order Type")]
  public virtual string SalesOrderType { get; set; }

  [PXDBString(15, IsUnicode = false)]
  [PXUIField(DisplayName = "Sales Order Nbr.", Enabled = false, Visible = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<FSEquipmentComponent.salesOrderType>>>>))]
  public virtual string SalesOrderNbr { get; set; }

  [PXDBString(1, IsFixed = true)]
  [ListField_Equipment_Status.ListAtrribute]
  [PXDefault("A")]
  [PXUIField(DisplayName = "Status")]
  public virtual string Status { get; set; }

  [PXDBString(2147483647 /*0x7FFFFFFF*/, IsUnicode = false)]
  [PXUIField(DisplayName = "Equipment Action Comment")]
  public virtual string Comment { get; set; }

  [PXDBInt]
  [PXUIField]
  [PXSelector(typeof (Search<FSEquipmentComponent.lineNbr>), SubstituteKey = typeof (FSEquipmentComponent.lineRef), ValidateValue = false)]
  public virtual int? ComponentReplaced { get; set; }

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

  [PXString(2147483647 /*0x7FFFFFFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Component Description")]
  public virtual string Mem_Description { get; set; }

  public class PK : 
    PrimaryKeyOf<FSEquipmentComponent>.By<FSEquipmentComponent.SMequipmentID, FSEquipmentComponent.lineNbr>
  {
    public static FSEquipmentComponent Find(
      PXGraph graph,
      int? SMequipmentID,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (FSEquipmentComponent) PrimaryKeyOf<FSEquipmentComponent>.By<FSEquipmentComponent.SMequipmentID, FSEquipmentComponent.lineNbr>.FindBy(graph, (object) SMequipmentID, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Equipment : 
      PrimaryKeyOf<FSEquipment>.By<FSEquipment.SMequipmentID>.ForeignKeyOf<FSEquipmentComponent>.By<FSEquipmentComponent.SMequipmentID>
    {
    }

    public class Component : 
      PrimaryKeyOf<FSModelTemplateComponent>.By<FSModelTemplateComponent.componentID>.ForeignKeyOf<FSEquipmentComponent>.By<FSEquipmentComponent.componentID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<FSEquipmentComponent>.By<FSEquipmentComponent.vendorID>
    {
    }

    public class ItemClass : 
      PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>.ForeignKeyOf<FSEquipmentComponent>.By<FSEquipmentComponent.itemClassID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSEquipmentComponent>.By<FSEquipmentComponent.inventoryID>
    {
    }

    public class InstallationServiceOrder : 
      PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.srvOrdType, FSServiceOrder.refNbr>.ForeignKeyOf<FSEquipmentComponent>.By<FSEquipmentComponent.instSrvOrdType, FSEquipmentComponent.instServiceOrderRefNbr>
    {
    }

    public class InstallationAppointment : 
      PrimaryKeyOf<FSAppointment>.By<FSAppointment.srvOrdType, FSAppointment.refNbr>.ForeignKeyOf<FSEquipmentComponent>.By<FSEquipmentComponent.instSrvOrdType, FSEquipmentComponent.instAppointmentRefNbr>
    {
    }

    public class ReplacedComponent : 
      PrimaryKeyOf<FSEquipmentComponent>.By<FSEquipmentComponent.SMequipmentID, FSEquipmentComponent.lineNbr>.ForeignKeyOf<FSEquipmentComponent>.By<FSEquipmentComponent.SMequipmentID, FSEquipmentComponent.componentReplaced>
    {
    }
  }

  public abstract class SMequipmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSEquipmentComponent.SMequipmentID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEquipmentComponent.lineNbr>
  {
  }

  public abstract class lineRef : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSEquipmentComponent.lineRef>
  {
  }

  public abstract class componentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEquipmentComponent.componentID>
  {
  }

  public abstract class cpnyWarrantyDuration : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSEquipmentComponent.cpnyWarrantyDuration>
  {
  }

  public abstract class cpnyWarrantyEndDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSEquipmentComponent.cpnyWarrantyEndDate>
  {
  }

  public abstract class installationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSEquipmentComponent.installationDate>
  {
  }

  public abstract class lastReplacementDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSEquipmentComponent.lastReplacementDate>
  {
  }

  public abstract class longDescr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSEquipmentComponent.longDescr>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEquipmentComponent.vendorID>
  {
  }

  public abstract class vendorWarrantyDuration : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSEquipmentComponent.vendorWarrantyDuration>
  {
  }

  public abstract class vendorWarrantyEndDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSEquipmentComponent.vendorWarrantyEndDate>
  {
  }

  public abstract class serialNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipmentComponent.serialNumber>
  {
  }

  public abstract class requireSerial : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSEquipmentComponent.requireSerial>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEquipmentComponent.itemClassID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEquipmentComponent.inventoryID>
  {
  }

  public abstract class cpnyWarrantyType : ListField_WarrantyDurationType
  {
  }

  public abstract class vendorWarrantyType : ListField_WarrantyDurationType
  {
  }

  public abstract class salesDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSEquipmentComponent.salesDate>
  {
  }

  public abstract class instSrvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipmentComponent.instSrvOrdType>
  {
  }

  public abstract class instServiceOrderRefNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSEquipmentComponent.instServiceOrderRefNbr>
  {
  }

  public abstract class instAppointmentRefNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSEquipmentComponent.instAppointmentRefNbr>
  {
  }

  public abstract class invoiceRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipmentComponent.invoiceRefNbr>
  {
  }

  public abstract class salesOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipmentComponent.salesOrderType>
  {
  }

  public abstract class salesOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipmentComponent.salesOrderNbr>
  {
  }

  public abstract class status : ListField_Equipment_Status
  {
  }

  public abstract class comment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSEquipmentComponent.comment>
  {
  }

  public abstract class componentReplaced : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSEquipmentComponent.componentReplaced>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSEquipmentComponent.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSEquipmentComponent.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipmentComponent.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSEquipmentComponent.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSEquipmentComponent.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipmentComponent.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSEquipmentComponent.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSEquipmentComponent.Tstamp>
  {
  }

  public abstract class mem_Description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipmentComponent.mem_Description>
  {
  }
}
