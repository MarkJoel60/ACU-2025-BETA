// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteAppointmentInfo
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXPrimaryGraph(typeof (AppointmentEntry))]
[PXProjection(typeof (Select5<FSAppointment, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, LeftJoin<FSSODet, On<FSSODet.sOID, Equal<FSServiceOrder.sOID>>, LeftJoin<FSAppointmentDet, On<FSAppointmentDet.appointmentID, Equal<FSAppointment.appointmentID>, And<FSAppointmentDet.sODetID, Equal<FSSODet.sODetID>>>, LeftJoin<FSAppointmentEmployee, On<FSAppointmentEmployee.appointmentID, Equal<FSAppointment.appointmentID>>, LeftJoin<BAccountStaffMember, On<FSAppointmentEmployee.employeeID, Equal<BAccountStaffMember.bAccountID>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>, InnerJoin<FSAddress, On<FSAddress.addressID, Equal<FSServiceOrder.serviceOrderAddressID>>, LeftJoin<FSGeoZonePostalCode, On<FSGeoZonePostalCode.postalCode, Equal<FSAddress.postalCode>>, LeftJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdType, Equal<FSServiceOrder.srvOrdType>>, LeftJoin<FSRouteDocument, On<FSRouteDocument.routeDocumentID, Equal<FSAppointment.routeDocumentID>>, CrossJoin<FSSetup, LeftJoin<FSCustomerBillingSetup, On<FSCustomerBillingSetup.customerID, Equal<FSServiceOrder.billCustomerID>, And<Where2<Where<FSSetup.customerMultipleBillingOptions, Equal<True>, And<FSCustomerBillingSetup.srvOrdType, Equal<FSServiceOrder.srvOrdType>>>, Or<Where<FSSetup.customerMultipleBillingOptions, Equal<False>, And<FSCustomerBillingSetup.srvOrdType, IsNull>>>>>>>>>>>>>>>>>>, Aggregate<GroupBy<FSAppointment.appointmentID, GroupBy<FSAppointment.noteID, GroupBy<FSAppointment.confirmed>>>>>))]
[Serializable]
public class RouteAppointmentInfo : FSAppointment
{
  [PXDBString(4, IsFixed = true, IsKey = true, InputMask = ">AAAA", BqlField = typeof (FSAppointment.srvOrdType))]
  [PXUIField(DisplayName = "Service Order Type")]
  [FSSelectorSrvOrdTypeNOTQuote]
  [PXFieldDescription]
  public override 
  #nullable disable
  string SrvOrdType { get; set; }

  [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = "CCCCCCCCCCCCCCCCCCCC", BqlField = typeof (FSAppointment.refNbr))]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search2<FSAppointment.refNbr, LeftJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<FSServiceOrder.customerID>, And<PX.Objects.CR.Location.locationID, Equal<FSServiceOrder.locationID>>>>>>, Where2<Where<FSAppointment.srvOrdType, Equal<Optional<FSAppointment.srvOrdType>>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>, OrderBy<Desc<FSAppointment.refNbr>>>), new System.Type[] {typeof (FSAppointment.refNbr), typeof (PX.Objects.AR.Customer.acctCD), typeof (PX.Objects.AR.Customer.acctName), typeof (PX.Objects.CR.Location.locationCD), typeof (FSAppointment.docDesc), typeof (FSAppointment.status), typeof (FSAppointment.scheduledDateTimeBegin)})]
  public override string RefNbr { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointment.appointmentID))]
  public override int? AppointmentID { get; set; }

  [PXDefault]
  [PXDBString(15, IsUnicode = true, BqlField = typeof (FSAppointment.soRefNbr))]
  [PXUIField(DisplayName = "Service Order Nbr.")]
  [FSSelectorSORefNbr_Appointment]
  public override string SORefNbr { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.customerID))]
  [PXUIField(DisplayName = "Customer")]
  [FSSelectorCustomer]
  public override int? CustomerID { get; set; }

  [PX.Objects.CS.LocationID(BqlField = typeof (FSServiceOrder.locationID), DisplayName = "Location", DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  public virtual int? LocationID { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (FSAddress.state))]
  [PXUIField(DisplayName = "State")]
  [PX.Objects.CR.State(typeof (FSAddress.countryID), DescriptionField = typeof (PX.Objects.CS.State.name))]
  public virtual string State { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (FSServiceOrder.priority))]
  [PXUIField]
  [ListField_Priority_ServiceOrder.ListAtrribute]
  public virtual string Priority { get; set; }

  [PXDBInt(BqlField = typeof (FSSODet.inventoryID))]
  [PXDefault]
  [PXSelector(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID>), SubstituteKey = typeof (PX.Objects.IN.InventoryItem.inventoryCD), DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr))]
  public virtual int? ServiceID { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointmentDet.sODetID))]
  [PXUIField(DisplayName = "Service Order Detail Ref. Nbr.")]
  [FSSelectorSODetIDService]
  public virtual int? SODetID { get; set; }

  [PXDBInt(BqlField = typeof (FSGeoZonePostalCode.geoZoneID))]
  [PXUIField(DisplayName = "Geographical Zone ID")]
  [PXSelector(typeof (Search<FSGeoZone.geoZoneID>), SubstituteKey = typeof (FSGeoZone.geoZoneCD), DescriptionField = typeof (FSGeoZone.descr))]
  public virtual int? GeoZoneID { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.branchLocationID))]
  [PXUIField(DisplayName = "Branch Location ID")]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  public virtual int? BranchLocationID { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (FSServiceOrder.roomID))]
  [PXUIField(DisplayName = "Room")]
  [PXSelector(typeof (Search<FSRoom.roomID>), SubstituteKey = typeof (FSRoom.roomID), DescriptionField = typeof (FSRoom.descr))]
  public virtual string RoomID { get; set; }

  [PXDBInt(BqlField = typeof (FSAppointmentEmployee.employeeID))]
  [PXUIField(DisplayName = "Staff Member ID", TabOrder = 0)]
  [PXSelector(typeof (Search<BAccountStaffMember.bAccountID>), SubstituteKey = typeof (BAccountSelectorBase.acctCD), DescriptionField = typeof (BAccountSelectorBase.acctName))]
  public virtual int? EmployeeID { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.billCustomerID))]
  [PXUIField(DisplayName = "Billing Customer ID")]
  [FSSelectorCustomer]
  public override int? BillCustomerID { get; set; }

  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSServiceOrder.billCustomerID>>>), BqlField = typeof (FSServiceOrder.billLocationID), DescriptionField = typeof (PX.Objects.CR.Location.descr), DisplayName = "Billing Location", DirtyRead = true)]
  public virtual int? BillLocationID { get; set; }

  [PXDBInt(BqlField = typeof (FSCustomerBillingSetup.billingCycleID))]
  [PXDefault]
  [PXSelector(typeof (Search<FSBillingCycle.billingCycleID>), SubstituteKey = typeof (FSBillingCycle.billingCycleCD), DescriptionField = typeof (FSBillingCycle.descr))]
  [PXUIField(DisplayName = "Billing Cycle ID", Enabled = false)]
  public virtual int? CustomerBillingCycleID { get; set; }

  [PXDBString(BqlField = typeof (FSCustomerBillingSetup.srvOrdType))]
  [PXDefault]
  [FSSelectorSrvOrdTypeNOTQuote]
  [PXUIField(DisplayName = "Service Order Type")]
  public virtual string CustomerBillingSrvOrdType { get; set; }

  [PXDBInt(BqlField = typeof (FSxCustomer.billingCycleID))]
  [PXSelector(typeof (FSBillingCycle.billingCycleID), SubstituteKey = typeof (FSBillingCycle.billingCycleCD), DescriptionField = typeof (FSBillingCycle.descr))]
  [PXUIField(DisplayName = "Billing Cycle ID", Enabled = false)]
  public virtual int? FSxCustomerBillingCycleID { get; set; }

  [PXDBDateAndTime(BqlField = typeof (FSServiceOrder.sLAETA))]
  [PXUIField]
  public virtual DateTime? SLAETA { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (FSAddress.addressLine1))]
  [PXUIField(DisplayName = "Address Line 1")]
  public virtual string AddressLine1 { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (FSAddress.addressLine2))]
  [PXUIField(DisplayName = "Address Line 2")]
  public virtual string AddressLine2 { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (FSAddress.addressLine3))]
  [PXUIField(DisplayName = "Address Line 3", Visible = false, Enabled = false)]
  public virtual string AddressLine3 { get; set; }

  [PXDBString(20, BqlField = typeof (FSAddress.postalCode))]
  [PXUIField(DisplayName = "Postal code")]
  [PXZipValidation(typeof (PX.Objects.CS.Country.zipCodeRegexp), typeof (PX.Objects.CS.Country.zipCodeMask), typeof (FSAddress.countryID))]
  [PXDynamicMask(typeof (Search<PX.Objects.CS.Country.zipCodeMask, Where<PX.Objects.CS.Country.countryID, Equal<Current<FSAddress.countryID>>>>))]
  [PXFormula(typeof (Default<FSAddress.countryID>))]
  public virtual string PostalCode { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (FSAddress.city))]
  [PXUIField(DisplayName = "City")]
  public virtual string City { get; set; }

  public new class PK : 
    PrimaryKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.srvOrdType, RouteAppointmentInfo.refNbr>
  {
    public static RouteAppointmentInfo Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (RouteAppointmentInfo) PrimaryKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.srvOrdType, RouteAppointmentInfo.refNbr>.FindBy(graph, (object) srvOrdType, (object) refNbr, options);
    }
  }

  public new static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.customerID>
    {
    }

    public class CustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.customerID, RouteAppointmentInfo.locationID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.branchID>
    {
    }

    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.srvOrdType>
    {
    }

    public class ServiceOrder : 
      PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.srvOrdType, FSServiceOrder.refNbr>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.srvOrdType, RouteAppointmentInfo.soRefNbr>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.projectID, RouteAppointmentInfo.dfltProjectTaskID>
    {
    }

    public class WorkFlowStage : 
      PrimaryKeyOf<FSWFStage>.By<FSWFStage.wFStageID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.wFStageID>
    {
    }

    public class ServiceContract : 
      PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.serviceContractID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.serviceContractID>
    {
    }

    public class Schedule : 
      PrimaryKeyOf<FSSchedule>.By<FSSchedule.scheduleID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.scheduleID>
    {
    }

    public class BillServiceContract : 
      PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.serviceContractID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.billServiceContractID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.taxZoneID>
    {
    }

    public class Route : 
      PrimaryKeyOf<FSRoute>.By<FSRoute.routeID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.routeID>
    {
    }

    public class RouteDocument : 
      PrimaryKeyOf<FSRouteDocument>.By<FSRouteDocument.routeDocumentID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.routeDocumentID>
    {
    }

    public class Vehicle : 
      PrimaryKeyOf<FSVehicle>.By<FSVehicle.SMequipmentID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.vehicleID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.curyID>
    {
    }

    public class SalesPerson : 
      PrimaryKeyOf<PX.Objects.AR.SalesPerson>.By<PX.Objects.AR.SalesPerson.salesPersonID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.salesPersonID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.serviceID>
    {
    }

    public class ServiceOrderLine : 
      PrimaryKeyOf<FSSODet>.By<FSSODet.sODetID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.sODetID>
    {
    }

    public class GeoZone : 
      PrimaryKeyOf<FSGeoZone>.By<FSGeoZone.geoZoneID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.geoZoneID>
    {
    }

    public class BranchLocation : 
      PrimaryKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.branchLocationID>
    {
    }

    public class BillCustomer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.billCustomerID>
    {
    }

    public class BillCustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.billCustomerID, RouteAppointmentInfo.billLocationID>
    {
    }

    public class Staff : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.employeeID>
    {
    }

    public class Room : 
      PrimaryKeyOf<FSRoom>.By<FSRoom.branchLocationID, FSRoom.roomID>.ForeignKeyOf<RouteAppointmentInfo>.By<RouteAppointmentInfo.branchLocationID, RouteAppointmentInfo.roomID>
    {
    }
  }

  public new abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RouteAppointmentInfo.srvOrdType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RouteAppointmentInfo.refNbr>
  {
  }

  public new abstract class appointmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentInfo.appointmentID>
  {
  }

  public new abstract class soRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RouteAppointmentInfo.soRefNbr>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RouteAppointmentInfo.customerID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RouteAppointmentInfo.locationID>
  {
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RouteAppointmentInfo.state>
  {
  }

  public abstract class priority : ListField_Priority_ServiceOrder
  {
  }

  public abstract class serviceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RouteAppointmentInfo.serviceID>
  {
  }

  public abstract class sODetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RouteAppointmentInfo.sODetID>
  {
  }

  public abstract class geoZoneID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RouteAppointmentInfo.geoZoneID>
  {
  }

  public abstract class branchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentInfo.branchLocationID>
  {
  }

  public abstract class roomID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RouteAppointmentInfo.roomID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RouteAppointmentInfo.employeeID>
  {
  }

  public new abstract class billCustomerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentInfo.billCustomerID>
  {
  }

  public abstract class billLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentInfo.billLocationID>
  {
  }

  public abstract class customerBillingCycleID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentInfo.customerBillingCycleID>
  {
  }

  public abstract class customerBillingSrvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RouteAppointmentInfo.customerBillingSrvOrdType>
  {
  }

  public abstract class fsxCustomerBillingCycleID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentInfo.fsxCustomerBillingCycleID>
  {
  }

  public abstract class sLAETA : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  RouteAppointmentInfo.sLAETA>
  {
  }

  public abstract class addressLine1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RouteAppointmentInfo.addressLine1>
  {
  }

  public abstract class addressLine2 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RouteAppointmentInfo.addressLine2>
  {
  }

  public abstract class addressLine3 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RouteAppointmentInfo.addressLine3>
  {
  }

  public abstract class postalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RouteAppointmentInfo.postalCode>
  {
  }

  public abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RouteAppointmentInfo.city>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RouteAppointmentInfo.branchID>
  {
  }

  public new abstract class dfltProjectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentInfo.dfltProjectTaskID>
  {
  }

  public new abstract class wFStageID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RouteAppointmentInfo.wFStageID>
  {
  }

  public new abstract class serviceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentInfo.serviceContractID>
  {
  }

  public new abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RouteAppointmentInfo.scheduleID>
  {
  }

  public new abstract class billServiceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentInfo.billServiceContractID>
  {
  }

  public new abstract class routeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RouteAppointmentInfo.routeID>
  {
  }

  public new abstract class routeDocumentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentInfo.routeDocumentID>
  {
  }

  public new abstract class vehicleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RouteAppointmentInfo.vehicleID>
  {
  }

  public new abstract class curyInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    RouteAppointmentInfo.curyInfoID>
  {
  }

  public new abstract class salesPersonID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentInfo.salesPersonID>
  {
  }

  public new abstract class taxZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RouteAppointmentInfo.taxZoneID>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RouteAppointmentInfo.projectID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RouteAppointmentInfo.curyID>
  {
  }
}
