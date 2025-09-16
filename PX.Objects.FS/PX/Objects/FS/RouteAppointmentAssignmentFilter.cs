// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteAppointmentAssignmentFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class RouteAppointmentAssignmentFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDateAndTime(UseTimeZone = true)]
  [PXDefault(typeof (FSRouteDocument.date))]
  [PXUIField(DisplayName = "Route Date", Visible = true)]
  public virtual DateTime? RouteDate { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Route")]
  [PXSelector(typeof (FSRoute.routeID), SubstituteKey = typeof (FSRoute.routeCD))]
  public virtual int? RouteID { get; set; }

  [PXString(10)]
  [PXUIField(DisplayName = "Route Short")]
  [PXSelector(typeof (FSRoute.routeShort))]
  public virtual 
  #nullable disable
  string Mem_RouteShort { get; set; }

  [PXString]
  public virtual string RefNbr { get; set; }

  [PXInt]
  public virtual string SrvOrdType { get; set; }

  [PXInt]
  public virtual int? AppointmentID { get; set; }

  [PXInt]
  public virtual int? RouteDocumentID { get; set; }

  [PXString]
  [PXDefault("U")]
  public virtual string UnassignAppointment { get; set; }

  [PXDBString(4, IsFixed = true, IsKey = true)]
  [PXDefault(typeof (Search<FSAppointment.srvOrdType, Where<FSAppointment.appointmentID, Equal<Current<FSAppointment.appointmentID>>>>))]
  [PXUIField(DisplayName = "Service Order Type", Enabled = false, IsReadOnly = true)]
  public virtual string AppSrvOrdType { get; set; }

  [PXDefault(typeof (Search<FSAppointment.refNbr, Where<FSAppointment.appointmentID, Equal<Current<FSAppointment.appointmentID>>>>))]
  [PXFormula(typeof (Default<FSAppointment.appointmentID>))]
  [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = "CCCCCCCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Appointment Nbr.", Visible = true, Enabled = false, IsReadOnly = true)]
  public virtual string AppRefNbr { get; set; }

  [PXDBTimeSpanLong]
  [PXUIField(DisplayName = "Estimated Duration Total (by employee performance)", Enabled = false, IsReadOnly = true)]
  [PXDefault(typeof (FSAppointment.estimatedDurationTotal))]
  public virtual int? EstimatedDurationTotal { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "Start Time")]
  [PXDefault(typeof (FSAppointment.scheduledDateTimeBegin))]
  [PXUIField(DisplayName = "Scheduled Date", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? ScheduledDateTimeBegin { get; set; }

  [PXDefault(typeof (Search2<FSAddress.addressLine1, InnerJoin<FSServiceOrder, On<FSServiceOrder.serviceOrderAddressID, Equal<FSAddress.addressID>>>, Where<FSServiceOrder.sOID, Equal<Current<FSAppointment.sOID>>>>))]
  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "Address Line 1", Enabled = false, IsReadOnly = true)]
  public virtual string AddressLine1 { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXDefault(typeof (Search2<FSAddress.addressLine2, InnerJoin<FSServiceOrder, On<FSServiceOrder.serviceOrderAddressID, Equal<FSAddress.addressID>>>, Where<FSServiceOrder.sOID, Equal<Current<FSAppointment.sOID>>>>))]
  [PXUIField(DisplayName = "Address Line 2", Enabled = false, IsReadOnly = true)]
  public virtual string AddressLine2 { get; set; }

  [PXDBInt]
  [PXDefault(typeof (Search<FSServiceOrder.customerID, Where<FSServiceOrder.sOID, Equal<Current<FSAppointment.sOID>>>>))]
  [PXUIField(DisplayName = "Customer", Enabled = false, IsReadOnly = true)]
  [FSSelectorCustomer]
  public virtual int? CustomerID { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXDefault(typeof (Search2<FSAddress.state, InnerJoin<FSServiceOrder, On<FSServiceOrder.serviceOrderAddressID, Equal<FSAddress.addressID>>>, Where<FSServiceOrder.sOID, Equal<Current<FSAppointment.sOID>>>>))]
  [PXUIField(DisplayName = "State", Enabled = false, IsReadOnly = true)]
  public virtual string State { get; set; }

  [PXDefault(typeof (Search<FSServiceOrder.locationID, Where<FSServiceOrder.sOID, Equal<Current<FSAppointment.sOID>>>>))]
  [PXUIField(DisplayName = "Location", Enabled = false, IsReadOnly = true)]
  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSServiceOrder.customerID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), DirtyRead = true)]
  [FSSelectorLocation]
  public virtual int? LocationID { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXDefault(typeof (Search2<FSAddress.city, InnerJoin<FSServiceOrder, On<FSServiceOrder.serviceOrderAddressID, Equal<FSAddress.addressID>>>, Where<FSServiceOrder.sOID, Equal<Current<FSAppointment.sOID>>>>))]
  [PXUIField(DisplayName = "City", Enabled = false, IsReadOnly = true)]
  public virtual string City { get; set; }

  public abstract class routeDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RouteAppointmentAssignmentFilter.routeDate>
  {
  }

  public abstract class routeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentAssignmentFilter.routeID>
  {
  }

  public abstract class mem_RouteShort : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RouteAppointmentAssignmentFilter.mem_RouteShort>
  {
  }

  public abstract class refNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RouteAppointmentAssignmentFilter.refNbr>
  {
  }

  public abstract class srvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RouteAppointmentAssignmentFilter.srvOrdType>
  {
  }

  public abstract class appointmentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentAssignmentFilter.appointmentID>
  {
  }

  public abstract class routeDocumentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentAssignmentFilter.routeDocumentID>
  {
  }

  public abstract class unassignAppointment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RouteAppointmentAssignmentFilter.unassignAppointment>
  {
  }

  public abstract class appSrvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RouteAppointmentAssignmentFilter.appSrvOrdType>
  {
  }

  public abstract class appRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RouteAppointmentAssignmentFilter.appRefNbr>
  {
  }

  public abstract class estimatedDurationTotal : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentAssignmentFilter.estimatedDurationTotal>
  {
  }

  public abstract class scheduledDateTimeBegin : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RouteAppointmentAssignmentFilter.scheduledDateTimeBegin>
  {
  }

  public abstract class addressLine1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RouteAppointmentAssignmentFilter.addressLine1>
  {
  }

  public abstract class addressLine2 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RouteAppointmentAssignmentFilter.addressLine2>
  {
  }

  public abstract class customerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentAssignmentFilter.customerID>
  {
  }

  public abstract class state : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RouteAppointmentAssignmentFilter.state>
  {
  }

  public abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RouteAppointmentAssignmentFilter.locationID>
  {
  }

  public abstract class city : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RouteAppointmentAssignmentFilter.city>
  {
  }
}
