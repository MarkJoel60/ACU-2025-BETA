// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSEquipment
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.FA;
using PX.Objects.IN;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Equipment")]
[PXPrimaryGraph(typeof (SMEquipmentMaint))]
[Serializable]
public class FSEquipment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ImageUrl;

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCC")]
  [PXUIField]
  [FSSelectorSMEquipmentRefNbr]
  [PXFieldDescription]
  public virtual string RefNbr { get; set; }

  [PXReferentialIntegrityCheck]
  [PXDBIdentity]
  public virtual int? SMEquipmentID { get; set; }

  [PXDBString(20, IsUnicode = true)]
  [PXUIField(DisplayName = "Barcode")]
  public virtual string Barcode { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Vehicle", Enabled = false)]
  public virtual bool? IsVehicle { get; set; }

  [PXDBInt]
  [PXUIField]
  [PXSelector(typeof (FSManufacturer.manufacturerID), SubstituteKey = typeof (FSManufacturer.manufacturerCD), DescriptionField = typeof (FSManufacturer.descr))]
  public virtual int? ManufacturerID { get; set; }

  [PXDBInt]
  [PXUIField]
  [PXSelector(typeof (Search<FSManufacturerModel.manufacturerModelID, Where<FSManufacturerModel.manufacturerID, Equal<Current<FSEquipment.manufacturerID>>>>), SubstituteKey = typeof (FSManufacturerModel.manufacturerModelCD), DescriptionField = typeof (FSManufacturerModel.descr))]
  [PXFormula(typeof (Default<FSEquipment.manufacturerID>))]
  public virtual int? ManufacturerModelID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [FADetails.propertyType.List]
  [PXDefault]
  [PXUIField(DisplayName = "Property Type")]
  public virtual string PropertyType { get; set; }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Serial Nbr.")]
  public virtual string SerialNumber { get; set; }

  [PXDBString(1, IsFixed = true)]
  [ListField_Equipment_Status.ListAtrribute]
  [PXDefault("A")]
  [PXUIField(DisplayName = "Status")]
  public virtual string Status { get; set; }

  [PXDBString(20, IsUnicode = true)]
  [PXUIField(DisplayName = "Tag Nbr.")]
  public virtual string TagNbr { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXSearchable(8192 /*0x2000*/, "SM {0}: {1} {2}", new System.Type[] {typeof (FSEquipment.refNbr), typeof (FSEquipment.descr), typeof (FSEquipment.serialNumber)}, new System.Type[] {typeof (FSEquipment.refNbr), typeof (FSEquipment.descr), typeof (FSEquipment.serialNumber)}, NumberFields = new System.Type[] {typeof (FSEquipment.refNbr)}, Line1Format = "{0:d} {1} {2}", Line1Fields = new System.Type[] {typeof (FSEquipment.registeredDate), typeof (FSEquipment.status), typeof (FSEquipment.equipmentTypeID)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (FSEquipment.descr)})]
  [PXNote(ShowInReferenceSelector = true)]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Equipment Type")]
  [FSSelectorEquipmentType]
  public virtual int? EquipmentTypeID { get; set; }

  [PXDBInt]
  public virtual int? SourceID { get; set; }

  [PXDBString(3, IsFixed = true)]
  [PXUIField(DisplayName = "Document Type", Enabled = false)]
  public virtual string SourceDocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  public virtual string SourceRefNbr { get; set; }

  [PXDBString(3, IsFixed = true)]
  [PXDefault("SME")]
  [PXUIField(DisplayName = "Document Type", Enabled = false)]
  [ListField_SourceType_Equipment.ListAtrribute]
  public virtual string SourceType { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Manufacturing Year")]
  public virtual string ManufacturingYear { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Installation Date")]
  public virtual DateTime? DateInstalled { get; set; }

  [PXDBInt]
  [FSSelectorBusinessAccount_VE]
  [PXUIField(DisplayName = "Vendor")]
  public virtual int? VendorID { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Purchase Date")]
  public virtual DateTime? PurchDate { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Purchase Order Nbr.")]
  public virtual string PurchPONumber { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Purchase Amount")]
  public virtual Decimal? PurchAmount { get; set; }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Registration Nbr.")]
  public virtual string RegistrationNbr { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Registered Date")]
  public virtual DateTime? RegisteredDate { get; set; }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Axles")]
  public virtual short? Axles { get; set; }

  [PXDBString(1, IsFixed = true)]
  [ListField_FuelType_Equipment.ListAtrribute]
  [PXUIField(DisplayName = "Fuel Type")]
  public virtual string FuelType { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tank 1 - Gallons")]
  public virtual Decimal? FuelTank1 { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tank 2 - Gallons")]
  public virtual Decimal? FuelTank2 { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Gross Vehicle Weight")]
  public virtual Decimal? GrossVehicleWeight { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Max Miles")]
  public virtual Decimal? MaxMiles { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tare Weight")]
  public virtual Decimal? TareWeight { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Weight Capacity")]
  public virtual Decimal? WeightCapacity { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Customer")]
  [FSCustomer]
  [PXForeignReference(typeof (PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSEquipment>.By<FSEquipment.customerID>))]
  public virtual int? CustomerID { get; set; }

  [FSLocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSEquipment.customerID>>, And<MatchWithBranch<PX.Objects.CR.Location.cBranchID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), DisplayName = "Location", DirtyRead = true)]
  [PXDefault(typeof (Coalesce<Search2<BAccountR.defLocationID, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>>, Where<BAccountR.bAccountID, Equal<Current<FSEquipment.customerID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.cBranchID>>>>>, Search<PX.Objects.CR.Standalone.Location.locationID, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<FSEquipment.customerID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.cBranchID>>>>>>))]
  [PXFormula(typeof (Default<FSEquipment.customerID>))]
  public virtual int? CustomerLocationID { get; set; }

  [PXDBLocalizableString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  [PXFieldDescription]
  public virtual string Descr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Vehicle Type ID")]
  [PXSelector(typeof (FSVehicleType.vehicleTypeID), SubstituteKey = typeof (FSVehicleType.vehicleTypeCD))]
  public virtual int? VehicleTypeID { get; set; }

  [Site(DisplayName = "Warehouse", DescriptionField = typeof (PX.Objects.IN.INSite.descr), Enabled = false)]
  public virtual int? SiteID { get; set; }

  [Location(typeof (FSEquipment.siteID), DisplayName = "Warehouse Location", KeepEntry = false, DescriptionField = typeof (INLocation.descr), Enabled = false)]
  public virtual int? LocationID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Color")]
  [PXDBIntList(typeof (SystemColor), typeof (SystemColor.colorID), typeof (SystemColor.colorName))]
  public virtual int? Color { get; set; }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Engine Nbr.")]
  public virtual string EngineNo { get; set; }

  [EquipmentModelItem(null, Filterable = true, DirtyRead = false)]
  [PXDefault]
  public virtual int? InventoryID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("OW")]
  [PXUIField(DisplayName = "Owner Type")]
  [ListField_OwnerType_Equipment.ListAtrribute]
  public virtual string OwnerType { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Customer")]
  [FSCustomer]
  [PXForeignReference(typeof (PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSEquipment>.By<FSEquipment.ownerID>))]
  [PXDefault]
  [PXFormula(typeof (Default<FSEquipment.ownerType>))]
  public virtual int? OwnerID { get; set; }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Model Serial Nbr.", Enabled = false)]
  public virtual string INSerialNumber { get; set; }

  [SubItem(DisplayName = "Subitem")]
  public virtual int? SubItemID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Target Equipment")]
  public virtual bool? RequireMaintenance { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXFormula(typeof (Default<FSEquipment.ownerType, FSEquipment.ownerID>))]
  [PXUIField(DisplayName = "Resource Equipment")]
  public virtual bool? ResourceEquipment { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Sales Date")]
  public virtual DateTime? SalesDate { get; set; }

  [PXDBInt]
  public virtual int? ARTranLineNbr { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("CU")]
  [PXUIField(DisplayName = "Location Type")]
  [ListField_LocationType.ListAtrribute]
  public virtual string LocationType { get; set; }

  [PXDBInt]
  [PXDefault(typeof (AccessInfo.branchID))]
  [PXUIField(DisplayName = "Branch")]
  [PXSelector(typeof (Search<PX.Objects.GL.Branch.branchID>), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  public virtual int? BranchID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (Search<FSxUserPreferences.dfltBranchLocationID, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>, And<UserPreferences.defBranchID, Equal<Current<FSEquipment.branchID>>, And<Current<FSEquipment.locationType>, Equal<ListField_LocationType.Company>>>>>))]
  [PXUIField(DisplayName = "Branch Location")]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<FSEquipment.branchID>>>>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  [PXFormula(typeof (Default<FSEquipment.branchID>))]
  public virtual int? BranchLocationID { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type")]
  public virtual string InstSrvOrdType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (Search<FSServiceOrder.refNbr, Where<FSServiceOrder.srvOrdType, Equal<Current<FSEquipment.instSrvOrdType>>>>), SubstituteKey = typeof (FSServiceOrder.refNbr), DescriptionField = typeof (FSServiceOrder.docDesc))]
  [PXUIField(DisplayName = "Service Order Nbr.", Enabled = false)]
  public virtual string InstServiceOrderRefNbr { get; set; }

  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<FSAppointment.refNbr, Where<FSAppointment.srvOrdType, Equal<Current<FSEquipment.instSrvOrdType>>>>), SubstituteKey = typeof (FSAppointment.refNbr), DescriptionField = typeof (FSAppointment.docDesc))]
  [PXUIField(DisplayName = "Appointment Nbr.", Enabled = false)]
  public virtual string InstAppointmentRefNbr { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Disposal Date")]
  public virtual DateTime? DisposalDate { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<FSEquipment.SMequipmentID, Where2<Where<Current<FSEquipment.ownerType>, Equal<ListField_OwnerType_Equipment.OwnCompany>>, And<FSEquipment.ownerType, Equal<ListField_OwnerType_Equipment.OwnCompany>, Or<Where<Current<FSEquipment.ownerType>, Equal<ListField_OwnerType_Equipment.Customer>, And<FSEquipment.ownerType, Equal<ListField_OwnerType_Equipment.Customer>, And<Current<FSEquipment.customerID>, Equal<FSEquipment.customerID>>>>>>>>), SubstituteKey = typeof (FSEquipment.refNbr), DescriptionField = typeof (FSEquipment.descr))]
  [PXUIField(DisplayName = "Replacement Equipment Nbr.")]
  public virtual int? ReplaceEquipmentID { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type")]
  public virtual string DispSrvOrdType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (Search<FSServiceOrder.refNbr, Where<FSServiceOrder.srvOrdType, Equal<Current<FSEquipment.dispSrvOrdType>>>>), SubstituteKey = typeof (FSServiceOrder.refNbr), DescriptionField = typeof (FSServiceOrder.docDesc))]
  [PXUIField(DisplayName = "Service Order Nbr.", Enabled = false)]
  public virtual string DispServiceOrderRefNbr { get; set; }

  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<FSAppointment.refNbr, Where<FSAppointment.srvOrdType, Equal<Current<FSEquipment.dispSrvOrdType>>>>), SubstituteKey = typeof (FSAppointment.refNbr), DescriptionField = typeof (FSAppointment.docDesc))]
  [PXUIField(DisplayName = "Appointment Nbr.", Enabled = false)]
  public virtual string DispAppointmentRefNbr { get; set; }

  [PXDBInt(MinValue = 0)]
  [PXUIField(DisplayName = "Company Warranty")]
  public virtual int? CpnyWarrantyValue { get; set; }

  [PXDBString(1, IsFixed = true)]
  [ListField_WarrantyDurationType.ListAtrribute]
  [PXDefault("M")]
  [PXUIField]
  public virtual string CpnyWarrantyType { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Company Warranty End Date", Enabled = false)]
  public virtual DateTime? CpnyWarrantyEndDate { get; set; }

  [PXDBInt(MinValue = 0)]
  [PXUIField(DisplayName = "Vendor Warranty")]
  public virtual int? VendorWarrantyValue { get; set; }

  [PXDBString(1, IsFixed = true)]
  [ListField_WarrantyDurationType.ListAtrribute]
  [PXDefault("M")]
  [PXUIField]
  public virtual string VendorWarrantyType { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Vendor Warranty End Date", Enabled = false)]
  public virtual DateTime? VendorWarrantyEndDate { get; set; }

  [PXDBString(3, IsFixed = true)]
  [PXUIField(DisplayName = "Sales Order Type")]
  public virtual string SalesOrderType { get; set; }

  [PXDBString(15, IsUnicode = false)]
  [PXUIField(DisplayName = "Sales Order Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<FSEquipment.salesOrderType>>>>))]
  public virtual string SalesOrderNbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Equipment Replaced", Enabled = false)]
  [PXSelector(typeof (Search<FSEquipment.SMequipmentID, Where<FSEquipment.SMequipmentID, NotEqual<Current<FSEquipment.SMequipmentID>>>>), SubstituteKey = typeof (FSEquipment.refNbr))]
  public virtual int? EquipmentReplacedID { get; set; }

  /// <summary>The URL of the image associated with the item.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Image")]
  public virtual string ImageUrl
  {
    get => this._ImageUrl;
    set => this._ImageUrl = value;
  }

  /// <summary>Is Vehicle Already Assigned</summary>
  [PXBool]
  [PXUIField(DisplayName = "Already Assigned", Enabled = false)]
  public virtual bool? Mem_UnassignedVehicle { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
  [PXUIField(Visible = false)]
  [PXFormula(typeof (Default<FSEquipmentType.equipmentTypeID>))]
  [PXDefault(typeof (Search<FSEquipmentType.equipmentTypeCD, Where<FSEquipmentType.equipmentTypeID, Equal<Current<FSEquipment.equipmentTypeID>>>>))]
  public virtual string EquipmentTypeCD { get; set; }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Equipment Description")]
  public virtual string MemDescription { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Suspended Target Equipment ID")]
  public virtual string MemReplacedEquipment { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Vehicle Description", Enabled = false)]
  public virtual string MemDescrVehicle { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Additional Vehicle 1 Description", Enabled = false)]
  public virtual string MemDescrAdditionalVehicle1 { get; set; }

  /// <summary>
  /// A service field, which is necessary for the <see cref="T:PX.Objects.CS.CSAnswers">dynamically
  /// added attributes</see> defined at the <see cref="T:PX.Objects.FS.FSVehicleType">Vehicle
  /// screen</see> level to function correctly.
  /// </summary>
  [CRAttributesField(typeof (FSEquipment.equipmentTypeCD), typeof (FSEquipment.noteID))]
  public virtual string[] Attributes { get; set; }

  [PXInt]
  [PXSelector(typeof (Search<FSEquipment.refNbr, Where<FSEquipment.customerID, Equal<Optional<FSEquipment.customerID>>>>), new System.Type[] {typeof (FSEquipment.refNbr), typeof (FSEquipment.customerID), typeof (FSEquipment.status), typeof (FSEquipment.customerLocationID)})]
  public virtual int? ReportSMEquipmentID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Fixed Asset")]
  public virtual string FixedAssetCD { get; set; }

  public class PK : PrimaryKeyOf<FSEquipment>.By<FSEquipment.SMequipmentID>
  {
    public static FSEquipment Find(PXGraph graph, int? SMequipmentID, PKFindOptions options = 0)
    {
      return (FSEquipment) PrimaryKeyOf<FSEquipment>.By<FSEquipment.SMequipmentID>.FindBy(graph, (object) SMequipmentID, options);
    }
  }

  public class UK : PrimaryKeyOf<FSEquipment>.By<FSEquipment.refNbr>
  {
    public static FSEquipment Find(PXGraph graph, string refNbr, PKFindOptions options = 0)
    {
      return (FSEquipment) PrimaryKeyOf<FSEquipment>.By<FSEquipment.refNbr>.FindBy(graph, (object) refNbr, options);
    }
  }

  public static class FK
  {
    public class Manufacturer : 
      PrimaryKeyOf<FSManufacturer>.By<FSManufacturer.manufacturerID>.ForeignKeyOf<FSEquipment>.By<FSEquipment.manufacturerID>
    {
    }

    public class ManufacturerModel : 
      PrimaryKeyOf<FSManufacturerModel>.By<FSManufacturerModel.manufacturerID, FSManufacturerModel.manufacturerModelCD>.ForeignKeyOf<FSEquipment>.By<FSEquipment.manufacturerID, FSEquipment.manufacturerModelID>
    {
    }

    public class EquipmentType : 
      PrimaryKeyOf<FSEquipmentType>.By<FSEquipmentType.equipmentTypeCD>.ForeignKeyOf<FSEquipment>.By<FSEquipment.equipmentTypeID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<FSEquipment>.By<FSEquipment.vendorID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSEquipment>.By<FSEquipment.customerID>
    {
    }

    public class CustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<FSEquipment>.By<FSEquipment.customerID, FSEquipment.customerLocationID>
    {
    }

    public class VehicleType : 
      PrimaryKeyOf<FSVehicleType>.By<FSVehicleType.vehicleTypeID>.ForeignKeyOf<FSEquipment>.By<FSEquipment.vehicleTypeID>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<FSEquipment>.By<FSEquipment.siteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<FSEquipment>.By<FSEquipment.locationID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSEquipment>.By<FSEquipment.inventoryID>
    {
    }

    public class Owner : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSEquipment>.By<FSEquipment.ownerID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<FSEquipment>.By<FSEquipment.subItemID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FSEquipment>.By<FSEquipment.branchID>
    {
    }

    public class BranchLocation : 
      PrimaryKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationID>.ForeignKeyOf<FSEquipment>.By<FSEquipment.branchLocationID>
    {
    }

    public class InstallationServiceOrder : 
      PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.srvOrdType, FSServiceOrder.refNbr>.ForeignKeyOf<FSEquipment>.By<FSEquipment.instSrvOrdType, FSEquipment.instServiceOrderRefNbr>
    {
    }

    public class InstallationAppointment : 
      PrimaryKeyOf<FSAppointment>.By<FSAppointment.srvOrdType, FSAppointment.refNbr>.ForeignKeyOf<FSEquipment>.By<FSEquipment.instSrvOrdType, FSEquipment.instAppointmentRefNbr>
    {
    }

    public class ReplacedEquipment : 
      PrimaryKeyOf<FSEquipment>.By<FSEquipment.SMequipmentID>.ForeignKeyOf<FSEquipment>.By<FSEquipment.replaceEquipmentID>
    {
    }

    public class DisposalServiceOrder : 
      PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.srvOrdType, FSServiceOrder.refNbr>.ForeignKeyOf<FSEquipment>.By<FSEquipment.dispSrvOrdType, FSEquipment.dispServiceOrderRefNbr>
    {
    }

    public class DisposalAppointment : 
      PrimaryKeyOf<FSAppointment>.By<FSAppointment.srvOrdType, FSAppointment.refNbr>.ForeignKeyOf<FSEquipment>.By<FSEquipment.dispSrvOrdType, FSEquipment.dispAppointmentRefNbr>
    {
    }
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSEquipment.refNbr>
  {
  }

  public abstract class SMequipmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEquipment.SMequipmentID>
  {
  }

  public abstract class barcode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSEquipment.barcode>
  {
  }

  public abstract class isVehicle : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSEquipment.isVehicle>
  {
  }

  public abstract class manufacturerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEquipment.manufacturerID>
  {
  }

  public abstract class manufacturerModelID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSEquipment.manufacturerModelID>
  {
  }

  public abstract class propertyType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSEquipment.propertyType>
  {
  }

  public abstract class serialNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSEquipment.serialNumber>
  {
  }

  public abstract class status : ListField_Equipment_Status
  {
  }

  public abstract class tagNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSEquipment.tagNbr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSEquipment.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSEquipment.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipment.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSEquipment.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSEquipment.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipment.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSEquipment.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSEquipment.Tstamp>
  {
  }

  public abstract class equipmentTypeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEquipment.equipmentTypeID>
  {
  }

  public abstract class sourceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEquipment.sourceID>
  {
  }

  public abstract class sourceDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSEquipment.sourceDocType>
  {
  }

  public abstract class sourceRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSEquipment.sourceRefNbr>
  {
  }

  public abstract class sourceType : ListField_SourceType_Equipment
  {
  }

  public abstract class manufacturingYear : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipment.manufacturingYear>
  {
  }

  public abstract class dateInstalled : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSEquipment.dateInstalled>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEquipment.vendorID>
  {
  }

  public abstract class purchDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSEquipment.purchDate>
  {
  }

  public abstract class purchPONumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSEquipment.purchPONumber>
  {
  }

  public abstract class purchAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSEquipment.purchAmount>
  {
  }

  public abstract class registrationNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipment.registrationNbr>
  {
  }

  public abstract class registeredDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSEquipment.registeredDate>
  {
  }

  public abstract class axles : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSEquipment.axles>
  {
  }

  public abstract class fuelType : ListField_FuelType_Equipment
  {
  }

  public abstract class fuelTank1 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSEquipment.fuelTank1>
  {
  }

  public abstract class fuelTank2 : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSEquipment.fuelTank2>
  {
  }

  public abstract class grossVehicleWeight : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSEquipment.grossVehicleWeight>
  {
  }

  public abstract class maxMiles : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSEquipment.maxMiles>
  {
  }

  public abstract class tareWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSEquipment.tareWeight>
  {
  }

  public abstract class weightCapacity : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSEquipment.weightCapacity>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEquipment.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSEquipment.customerLocationID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSEquipment.descr>
  {
  }

  public abstract class vehicleTypeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEquipment.vehicleTypeID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEquipment.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEquipment.locationID>
  {
  }

  public abstract class color : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEquipment.color>
  {
  }

  public abstract class engineNo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSEquipment.engineNo>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEquipment.inventoryID>
  {
  }

  public abstract class ownerType : ListField_OwnerType_Equipment
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEquipment.ownerID>
  {
  }

  public abstract class iNSerialNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipment.iNSerialNumber>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEquipment.subItemID>
  {
  }

  public abstract class requireMaintenance : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSEquipment.requireMaintenance>
  {
  }

  public abstract class resourceEquipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSEquipment.resourceEquipment>
  {
  }

  public abstract class salesDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSEquipment.salesDate>
  {
  }

  public abstract class arTranLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEquipment.arTranLineNbr>
  {
  }

  public abstract class locationType : ListField_LocationType
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEquipment.branchID>
  {
  }

  public abstract class branchLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEquipment.branchLocationID>
  {
  }

  public abstract class instSrvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipment.instSrvOrdType>
  {
  }

  public abstract class instServiceOrderRefNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSEquipment.instServiceOrderRefNbr>
  {
  }

  public abstract class instAppointmentRefNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSEquipment.instAppointmentRefNbr>
  {
  }

  public abstract class disposalDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSEquipment.disposalDate>
  {
  }

  public abstract class replaceEquipmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSEquipment.replaceEquipmentID>
  {
  }

  public abstract class dispSrvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipment.dispSrvOrdType>
  {
  }

  public abstract class dispServiceOrderRefNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSEquipment.dispServiceOrderRefNbr>
  {
  }

  public abstract class dispAppointmentRefNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSEquipment.dispAppointmentRefNbr>
  {
  }

  public abstract class cpnyWarrantyValue : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSEquipment.cpnyWarrantyValue>
  {
  }

  public abstract class cpnyWarrantyType : ListField_WarrantyDurationType
  {
  }

  public abstract class cpnyWarrantyEndDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSEquipment.cpnyWarrantyEndDate>
  {
  }

  public abstract class vendorWarrantyValue : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSEquipment.vendorWarrantyValue>
  {
  }

  public abstract class vendorWarrantyType : ListField_WarrantyDurationType
  {
  }

  public abstract class vendorWarrantyEndDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSEquipment.vendorWarrantyEndDate>
  {
  }

  public abstract class salesOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipment.salesOrderType>
  {
  }

  public abstract class salesOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSEquipment.salesOrderNbr>
  {
  }

  public abstract class equipmentReplacedID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSEquipment.equipmentReplacedID>
  {
  }

  public abstract class imageUrl : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSEquipment.imageUrl>
  {
  }

  public abstract class memUnassignedVehicle : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSEquipment.memUnassignedVehicle>
  {
  }

  public abstract class equipmentTypeCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipment.equipmentTypeCD>
  {
  }

  public abstract class memDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipment.memDescription>
  {
  }

  public abstract class memReplacedEquipment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipment.memReplacedEquipment>
  {
  }

  public abstract class memDescrVehicle : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipment.memDescrVehicle>
  {
  }

  public abstract class memDescrAdditionalVehicle1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipment.memDescrAdditionalVehicle1>
  {
  }

  public abstract class reportSMEquipmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSEquipment.reportSMEquipmentID>
  {
  }

  public abstract class fixedAssetCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSEquipment.fixedAssetCD>
  {
  }
}
