// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppointmentInRoute
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
[PXProjection(typeof (Select2<FSAppointment, InnerJoin<FSServiceOrder, On<FSServiceOrder.sOID, Equal<FSAppointment.sOID>>, InnerJoin<FSAddress, On<FSAddress.addressID, Equal<FSServiceOrder.serviceOrderAddressID>>, LeftJoin<FSServiceContract, On<FSAppointment.serviceContractID, Equal<FSServiceContract.serviceContractID>>, CrossJoinSingleTable<FSSetup>>>>>))]
[Serializable]
public class FSAppointmentInRoute : FSAppointment
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

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (FSServiceContract.customerContractNbr))]
  [PXUIField(DisplayName = "Customer Contract Nbr.")]
  public virtual string CustomerContractNbr { get; set; }

  [PX.Objects.CS.LocationID(BqlField = typeof (FSServiceOrder.locationID), DisplayName = "Location", DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  public virtual int? LocationID { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (FSAddress.state))]
  [PXUIField(DisplayName = "State")]
  [PX.Objects.CR.State(typeof (FSAddress.countryID), DescriptionField = typeof (PX.Objects.CS.State.name))]
  public virtual string State { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (FSAddress.addressLine1))]
  [PXUIField(DisplayName = "Address Line 1")]
  public virtual string AddressLine1 { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (FSAddress.addressLine2))]
  [PXUIField(DisplayName = "Address Line 2")]
  public virtual string AddressLine2 { get; set; }

  [PXDBString(20, BqlField = typeof (FSAddress.postalCode))]
  [PXUIField(DisplayName = "Postal code")]
  [PXZipValidation(typeof (PX.Objects.CS.Country.zipCodeRegexp), typeof (PX.Objects.CS.Country.zipCodeMask), typeof (FSAddress.countryID))]
  [PXDynamicMask(typeof (Search<PX.Objects.CS.Country.zipCodeMask, Where<PX.Objects.CS.Country.countryID, Equal<Current<FSAddress.countryID>>>>))]
  [PXFormula(typeof (Default<FSAddress.countryID>))]
  public virtual string PostalCode { get; set; }

  [PXDBString(50, IsUnicode = true, BqlField = typeof (FSAddress.city))]
  [PXUIField(DisplayName = "City")]
  public virtual string City { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (FSSetup.mapApiKey))]
  [PXUIField(DisplayName = "Map API Key")]
  public virtual string MapApiKey { get; set; }

  [PXDBInt(BqlField = typeof (FSServiceOrder.serviceContractID))]
  [PXSelector(typeof (Search<FSServiceContract.serviceContractID, Where<FSServiceContract.customerID, Equal<Current<FSAppointmentInRoute.customerID>>>>), SubstituteKey = typeof (FSServiceContract.refNbr))]
  [PXUIField(DisplayName = "Source Service Contract ID", Enabled = false, FieldClass = "FSCONTRACT")]
  public override int? ServiceContractID { get; set; }

  public new class PK : 
    PrimaryKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.srvOrdType, FSAppointmentInRoute.refNbr>
  {
    public static FSAppointmentInRoute Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (FSAppointmentInRoute) PrimaryKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.srvOrdType, FSAppointmentInRoute.refNbr>.FindBy(graph, (object) srvOrdType, (object) refNbr, options);
    }
  }

  public new class UK : PrimaryKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.appointmentID>
  {
    public static FSAppointmentInRoute Find(
      PXGraph graph,
      int? appointmentID,
      PKFindOptions options = 0)
    {
      return (FSAppointmentInRoute) PrimaryKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.appointmentID>.FindBy(graph, (object) appointmentID, options);
    }
  }

  public new static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.customerID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.branchID>
    {
    }

    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.srvOrdType>
    {
    }

    public class ServiceOrder : 
      PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.srvOrdType, FSServiceOrder.refNbr>.ForeignKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.srvOrdType, FSAppointmentInRoute.soRefNbr>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.projectID, FSAppointmentInRoute.dfltProjectTaskID>
    {
    }

    public class WorkFlowStage : 
      PrimaryKeyOf<FSWFStage>.By<FSWFStage.wFStageID>.ForeignKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.wFStageID>
    {
    }

    public class ServiceContract : 
      PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.serviceContractID>.ForeignKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.serviceContractID>
    {
    }

    public class Schedule : 
      PrimaryKeyOf<FSSchedule>.By<FSSchedule.scheduleID>.ForeignKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.scheduleID>
    {
    }

    public class BillServiceContract : 
      PrimaryKeyOf<FSServiceContract>.By<FSServiceContract.serviceContractID>.ForeignKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.billServiceContractID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.taxZoneID>
    {
    }

    public class Route : 
      PrimaryKeyOf<FSRoute>.By<FSRoute.routeID>.ForeignKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.routeID>
    {
    }

    public class RouteDocument : 
      PrimaryKeyOf<FSRouteDocument>.By<FSRouteDocument.routeDocumentID>.ForeignKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.routeDocumentID>
    {
    }

    public class Vehicle : 
      PrimaryKeyOf<FSVehicle>.By<FSVehicle.SMequipmentID>.ForeignKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.vehicleID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.curyID>
    {
    }

    public class SalesPerson : 
      PrimaryKeyOf<PX.Objects.AR.SalesPerson>.By<PX.Objects.AR.SalesPerson.salesPersonID>.ForeignKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.salesPersonID>
    {
    }

    public class BillCustomer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.billCustomerID>
    {
    }

    public class CustomerLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<FSAppointmentInRoute>.By<FSAppointmentInRoute.customerID, FSAppointmentInRoute.locationID>
    {
    }
  }

  public new abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentInRoute.srvOrdType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentInRoute.refNbr>
  {
  }

  public new abstract class appointmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentInRoute.appointmentID>
  {
  }

  public new abstract class soRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentInRoute.soRefNbr>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentInRoute.customerID>
  {
  }

  public abstract class customerContractNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentInRoute.locationID>
  {
  }

  public abstract class state : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentInRoute.state>
  {
  }

  public abstract class addressLine1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentInRoute.addressLine1>
  {
  }

  public abstract class addressLine2 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentInRoute.addressLine2>
  {
  }

  public abstract class postalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentInRoute.postalCode>
  {
  }

  public abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentInRoute.city>
  {
  }

  public abstract class mapApiKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentInRoute.mapApiKey>
  {
  }

  public new abstract class serviceContractID : IBqlField, IBqlOperand
  {
  }

  public new abstract class sOID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentInRoute.sOID>
  {
  }

  public new abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentInRoute.scheduleID>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentInRoute.branchID>
  {
  }

  public new abstract class taxZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentInRoute.taxZoneID>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentInRoute.projectID>
  {
  }

  public new abstract class dfltProjectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentInRoute.dfltProjectTaskID>
  {
  }

  public new abstract class wFStageID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentInRoute.wFStageID>
  {
  }

  public new abstract class routeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentInRoute.routeID>
  {
  }

  public new abstract class routeDocumentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentInRoute.routeDocumentID>
  {
  }

  public new abstract class vehicleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentInRoute.vehicleID>
  {
  }

  public new abstract class curyInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    FSAppointmentInRoute.curyInfoID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentInRoute.curyID>
  {
  }

  public new abstract class salesPersonID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentInRoute.salesPersonID>
  {
  }

  public new abstract class billCustomerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentInRoute.billCustomerID>
  {
  }

  public new abstract class billServiceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentInRoute.billServiceContractID>
  {
  }

  public new abstract class notStarted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentInRoute.notStarted>
  {
  }

  public new abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentInRoute.hold>
  {
  }

  public new abstract class awaiting : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentInRoute.awaiting>
  {
  }

  public new abstract class inProcess : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentInRoute.inProcess>
  {
  }

  public new abstract class paused : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentInRoute.paused>
  {
  }

  public new abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentInRoute.completed>
  {
  }

  public new abstract class closed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentInRoute.closed>
  {
  }

  public new abstract class canceled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentInRoute.canceled>
  {
  }

  public new abstract class billed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentInRoute.billed>
  {
  }

  public new abstract class generatedByContract : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentInRoute.generatedByContract>
  {
  }
}
