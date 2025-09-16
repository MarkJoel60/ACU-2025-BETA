// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSVehicle
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXPrimaryGraph(typeof (VehicleMaintBridge))]
[Serializable]
public class FSVehicle : FSEquipment
{
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search<FSEquipment.refNbr, Where<FSEquipment.isVehicle, Equal<True>>>), new System.Type[] {typeof (FSEquipment.refNbr), typeof (FSEquipment.status), typeof (FSEquipment.descr), typeof (FSEquipment.registrationNbr), typeof (FSEquipment.manufacturerModelID), typeof (FSEquipment.manufacturerID), typeof (FSEquipment.manufacturingYear), typeof (FSEquipment.color)}, DescriptionField = typeof (FSEquipment.descr))]
  [AutoNumber(typeof (Search<FSSetup.equipmentNumberingID>), typeof (AccessInfo.businessDate))]
  public override 
  #nullable disable
  string RefNbr { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "VIN")]
  public override string SerialNumber { get; set; }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "License Nbr.")]
  public override string RegistrationNbr { get; set; }

  /// <summary>
  /// A service field, which is necessary for the <see cref="T:PX.Objects.CS.CSAnswers">dynamically
  /// added attributes</see> defined at the <see cref="T:PX.Objects.FS.FSVehicleType">Vehicle
  /// screen</see> level to function correctly.
  /// </summary>
  [CRAttributesField(typeof (FSVehicle.vehicleTypeCD), typeof (FSEquipment.noteID))]
  public override string[] Attributes { get; set; }

  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
  [PXUIField(Visible = false)]
  [PXDBScalar(typeof (Search<FSVehicleType.vehicleTypeCD, Where<FSVehicleType.vehicleTypeID, Equal<FSEquipment.vehicleTypeID>>>))]
  [PXDefault(typeof (Search<FSVehicleType.vehicleTypeCD, Where<FSVehicleType.vehicleTypeID, Equal<Current<FSEquipment.vehicleTypeID>>>>))]
  public virtual string VehicleTypeCD { get; set; }

  public new class PK : PrimaryKeyOf<FSVehicle>.By<FSVehicle.SMequipmentID>
  {
    public static FSVehicle Find(PXGraph graph, int? SMequipmentID, PKFindOptions options = 0)
    {
      return (FSVehicle) PrimaryKeyOf<FSVehicle>.By<FSVehicle.SMequipmentID>.FindBy(graph, (object) SMequipmentID, options);
    }
  }

  public new static class FK
  {
    public class Manufacturer : 
      PrimaryKeyOf<FSManufacturer>.By<FSManufacturer.manufacturerID>.ForeignKeyOf<FSVehicle>.By<FSVehicle.manufacturerID>
    {
    }

    public class ManufacturerModel : 
      PrimaryKeyOf<FSManufacturerModel>.By<FSManufacturerModel.manufacturerID, FSManufacturerModel.manufacturerModelCD>.ForeignKeyOf<FSVehicle>.By<FSVehicle.manufacturerID, FSVehicle.manufacturerModelID>
    {
    }

    public class EquipmentType : 
      PrimaryKeyOf<FSEquipmentType>.By<FSEquipmentType.equipmentTypeCD>.ForeignKeyOf<FSVehicle>.By<FSVehicle.equipmentTypeID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<FSVehicle>.By<FSVehicle.vendorID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSVehicle>.By<FSVehicle.customerID>
    {
    }

    public class CustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<FSVehicle>.By<FSVehicle.customerID, FSVehicle.customerLocationID>
    {
    }

    public class VehicleType : 
      PrimaryKeyOf<FSVehicleType>.By<FSVehicleType.vehicleTypeID>.ForeignKeyOf<FSVehicle>.By<FSEquipment.vehicleTypeID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<FSVehicle>.By<FSVehicle.siteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<FSVehicle>.By<FSVehicle.locationID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSVehicle>.By<FSVehicle.inventoryID>
    {
    }

    public class Owner : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSVehicle>.By<FSVehicle.ownerID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<FSVehicle>.By<FSVehicle.subItemID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FSVehicle>.By<FSVehicle.branchID>
    {
    }

    public class BranchLocation : 
      PrimaryKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationID>.ForeignKeyOf<FSVehicle>.By<FSVehicle.branchLocationID>
    {
    }

    public class InstallationServiceOrder : 
      PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.sOID>.ForeignKeyOf<FSVehicle>.By<FSVehicle.instServiceOrderID>
    {
    }

    public class InstallationAppointment : 
      PrimaryKeyOf<FSAppointment>.By<FSAppointment.appointmentID>.ForeignKeyOf<FSVehicle>.By<FSVehicle.instAppointmentID>
    {
    }

    public class ReplacedEquipment : 
      PrimaryKeyOf<FSEquipment>.By<FSEquipment.SMequipmentID>.ForeignKeyOf<FSVehicle>.By<FSVehicle.replaceEquipmentID>
    {
    }

    public class DisposalServiceOrder : 
      PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.sOID>.ForeignKeyOf<FSVehicle>.By<FSVehicle.dispServiceOrderID>
    {
    }

    public class DisposalAppointment : 
      PrimaryKeyOf<FSAppointment>.By<FSAppointment.appointmentID>.ForeignKeyOf<FSVehicle>.By<FSVehicle.dispAppointmentID>
    {
    }
  }

  public new class UK : PrimaryKeyOf<FSVehicle>.By<FSEquipment.refNbr>
  {
    public static FSVehicle Find(PXGraph graph, string refNbr, PKFindOptions options = 0)
    {
      return (FSVehicle) PrimaryKeyOf<FSVehicle>.By<FSEquipment.refNbr>.FindBy(graph, (object) refNbr, options);
    }
  }

  public new abstract class SMequipmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSVehicle.SMequipmentID>
  {
  }

  public new abstract class sourceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSVehicle.sourceID>
  {
  }

  public new abstract class sourceType : ListField_SourceType_Equipment
  {
  }

  public abstract class vehicleTypeCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSVehicle.vehicleTypeCD>
  {
  }

  public new abstract class manufacturerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSVehicle.manufacturerID>
  {
  }

  public new abstract class manufacturerModelID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSVehicle.manufacturerModelID>
  {
  }

  public new abstract class equipmentTypeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSVehicle.equipmentTypeID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSVehicle.vendorID>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSVehicle.customerID>
  {
  }

  public new abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSVehicle.customerLocationID>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSVehicle.siteID>
  {
  }

  public new abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSVehicle.locationID>
  {
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSVehicle.inventoryID>
  {
  }

  public new abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSVehicle.ownerID>
  {
  }

  public new abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSVehicle.subItemID>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSVehicle.branchID>
  {
  }

  public new abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSVehicle.branchLocationID>
  {
  }

  public abstract class instServiceOrderID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSVehicle.instServiceOrderID>
  {
  }

  public abstract class instAppointmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSVehicle.instAppointmentID>
  {
  }

  public new abstract class replaceEquipmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSVehicle.replaceEquipmentID>
  {
  }

  public abstract class dispServiceOrderID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSVehicle.dispServiceOrderID>
  {
  }

  public abstract class dispAppointmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSVehicle.dispAppointmentID>
  {
  }
}
