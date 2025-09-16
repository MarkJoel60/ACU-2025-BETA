// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSRouteDocument
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Route Document")]
[PXPrimaryGraph(typeof (RouteDocumentMaint))]
[Serializable]
public class FSRouteDocument : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected DateTime? _TimeBegin;
  protected DateTime? _TimeEnd;
  protected DateTime? _ActualStartTime;
  protected DateTime? _ActualEndTime;

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [AutoNumber(typeof (Search<FSRouteSetup.routeNumberingID>), typeof (AccessInfo.businessDate))]
  [PXSelector(typeof (Search3<FSRouteDocument.refNbr, OrderBy<Desc<FSRouteDocument.refNbr>>>))]
  [PXFieldDescription]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  [PXDBInt]
  [PXDefault(typeof (AccessInfo.branchID))]
  [PXUIField(DisplayName = "Branch")]
  [PXSelector(typeof (Search<PX.Objects.GL.Branch.branchID>), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  public virtual int? BranchID { get; set; }

  [PXDBIdentity]
  [PXUIField(Enabled = false)]
  public virtual int? RouteDocumentID { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? Date { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField]
  [FSSelectorRouteID]
  [PXFieldDescription]
  public virtual int? RouteID { get; set; }

  [PXDBInt]
  [PXUIField]
  [PXRestrictor(typeof (Where<EPEmployeeFSRouteEmployee.vStatus, IsNull, Or<EPEmployeeFSRouteEmployee.vStatus, NotEqual<VendorStatus.inactive>>>), "Employee is {0}.", new System.Type[] {typeof (PX.Objects.CR.BAccount.status)})]
  [FSSelector_Driver_RouteDocumentRouteID]
  public virtual int? DriverID { get; set; }

  [PXDBInt]
  [PXUIField]
  [PXRestrictor(typeof (Where<EPEmployeeFSRouteEmployee.vStatus, IsNull, Or<EPEmployeeFSRouteEmployee.vStatus, NotEqual<VendorStatus.inactive>>>), "Employee is {0}.", new System.Type[] {typeof (PX.Objects.CR.BAccount.status)})]
  [FSSelector_Driver_RouteDocumentRouteID]
  public virtual int? AdditionalDriverID { get; set; }

  [PXDBBool]
  [PXDefault(typeof (Search<FSRouteSetup.autoCalculateRouteStats>))]
  [PXUIField(DisplayName = "Route Stats Updated")]
  public virtual bool? RouteStatsUpdated { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("O")]
  [ListField_Status_Route.ListAtrribute]
  [PXUIField]
  public virtual string Status { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "Start Time")]
  [PXDefault]
  [PXUIField(DisplayName = "Start Time")]
  public virtual DateTime? TimeBegin
  {
    get => this._TimeBegin;
    set
    {
      this.TimeBeginUTC = value;
      this._TimeBegin = value;
    }
  }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "End Time")]
  [PXUIField(DisplayName = "End Time", Enabled = false, Visible = false)]
  public virtual DateTime? TimeEnd
  {
    get => this._TimeEnd;
    set
    {
      this.TimeEndUTC = value;
      this._TimeEnd = value;
    }
  }

  [PXDefault(1)]
  [PXFormula(typeof (Default<FSRouteDocument.date>))]
  [PXFormula(typeof (Default<FSRouteDocument.routeID>))]
  [PXDBInt(MinValue = 0, MaxValue = 2147483647 /*0x7FFFFFFF*/)]
  [PXUIField(DisplayName = "Trip Nbr.")]
  public virtual int? TripNbr { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Actual Start Date")]
  [PXDefault]
  [PXUIField(DisplayName = "Actual Start Date")]
  public virtual DateTime? ActualStartTime
  {
    get => this._ActualStartTime;
    set
    {
      this.ActualStartTimeUTC = value;
      this._ActualStartTime = value;
    }
  }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Actual End Date")]
  [PXDefault]
  [PXUIField(DisplayName = "Actual End Date")]
  public virtual DateTime? ActualEndTime
  {
    get => this._ActualEndTime;
    set
    {
      this.ActualEndTimeUTC = value;
      this._ActualEndTime = value;
    }
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Number of Appointments", Enabled = false)]
  public virtual int? TotalNumAppointments { get; set; }

  [PXUIField(DisplayName = "Total Driving Duration", Enabled = false)]
  [PXDBTimeSpanLong]
  public virtual int? TotalDuration { get; set; }

  [PXDBDecimal]
  public virtual Decimal? TotalDistance { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Total Distance", Enabled = false)]
  public virtual string TotalDistanceFriendly { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Total Services", Enabled = false)]
  public virtual int? TotalServices { get; set; }

  [PXDBTimeSpanLong]
  [PXUIField(DisplayName = "Total Services Duration", Enabled = false)]
  public virtual int? TotalServicesDuration { get; set; }

  [PXUIField(DisplayName = "Total Route Duration", Enabled = false)]
  [PXDBTimeSpanLong]
  public virtual int? TotalTravelTime { get; set; }

  [PXDBInt]
  [PXUIField]
  [FSSelectorVehicle]
  [PXRestrictor(typeof (Where<FSEquipment.status, Equal<ID.Equipment_Status.Equipment_StatusActive>>), "Vehicle is {0}.", new System.Type[] {typeof (FSEquipment.status)})]
  public virtual int? VehicleID { get; set; }

  [PXDBInt]
  [PXUIField]
  [FSSelectorVehicle]
  [PXRestrictor(typeof (Where<FSEquipment.status, Equal<ID.Equipment_Status.Equipment_StatusActive>>), "Vehicle is {0}.", new System.Type[] {typeof (FSEquipment.status)})]
  public virtual int? AdditionalVehicleID1 { get; set; }

  [PXDBInt]
  [PXUIField]
  [FSSelectorVehicle]
  [PXRestrictor(typeof (Where<FSEquipment.status, Equal<ID.Equipment_Status.Equipment_StatusActive>>), "Vehicle is {0}.", new System.Type[] {typeof (FSEquipment.status)})]
  public virtual int? AdditionalVehicleID2 { get; set; }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Latitude", Enabled = false)]
  public virtual Decimal? GPSLatitudeStart { get; set; }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Longitude", Enabled = false)]
  public virtual Decimal? GPSLongitudeStart { get; set; }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Latitude", Enabled = false)]
  public virtual Decimal? GPSLatitudeComplete { get; set; }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Longitude", Enabled = false)]
  public virtual Decimal? GPSLongitudeComplete { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote(ShowInReferenceSelector = true, Selector = typeof (FSRouteDocument.refNbr))]
  public virtual Guid? NoteID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Generated by System", Enabled = false)]
  public virtual bool? GeneratedBySystem { get; set; }

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

  [PXDBInt(MinValue = 0, MaxValue = 999)]
  [PXUIField(DisplayName = "Miles")]
  public virtual int? Miles { get; set; }

  [PXDBInt(MinValue = 0, MaxValue = 99999)]
  [PXUIField(DisplayName = "Weight")]
  public virtual int? Weight { get; set; }

  [PXDBInt(MinValue = 0, MaxValue = 999)]
  [PXUIField(DisplayName = "Fuel Qty.")]
  public virtual int? FuelQty { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("R")]
  [PXUIField]
  [ListField_FuelType_Equipment.ListAtrribute]
  public virtual string FuelType { get; set; }

  [PXDBInt(MinValue = 0, MaxValue = 99)]
  [PXUIField(DisplayName = "Oil")]
  public virtual int? Oil { get; set; }

  [PXDBInt(MinValue = 0, MaxValue = 99)]
  [PXUIField(DisplayName = "Anti-freeze")]
  public virtual int? AntiFreeze { get; set; }

  [PXDBInt(MinValue = 0, MaxValue = 99)]
  [PXUIField(DisplayName = "DEF")]
  public virtual int? DEF { get; set; }

  [PXDBInt(MinValue = 0, MaxValue = 99)]
  [PXUIField(DisplayName = "Propane")]
  public virtual int? Propane { get; set; }

  /// <summary>
  /// A service field, which is necessary for the <see cref="T:PX.Objects.CS.CSAnswers">dynamically
  /// added attributes</see> defined at the <see cref="T:PX.Objects.FS.FSRoute">Route
  /// screen</see> level to function correctly.
  /// </summary>
  [CRAttributesField(typeof (FSRouteDocument.routeCD))]
  public virtual string[] Attributes { get; set; }

  [PXString]
  [PXFormula(typeof (Selector<FSRouteDocument.routeID, FSRoute.descr>))]
  public string FormCaptionDescription { get; set; }

  [PXString(10)]
  [PXDefault(typeof (FSRouteSetup.routeNumberingID))]
  [PXUIField(DisplayName = "Route Numbering ID")]
  public virtual string RouteNumberingID { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXUIField(DisplayName = "Actual Duration", Enabled = false)]
  [PXTimeSpanLong]
  [PXFormula(typeof (DateDiff<FSRouteDocument.actualStartTime, FSRouteDocument.actualEndTime, DateDiff.minute>))]
  public virtual int? MemActualDuration { get; set; }

  [PXDateAndTime]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXFormula(typeof (TimeZoneNow))]
  public virtual DateTime? MemBusinessDateTime { get; set; }

  [PXString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "GPS Latitude Longitude", Enabled = false)]
  public virtual string GPSLatitudeLongitude { get; set; }

  [PXBool]
  public virtual bool? MustRecalculateStats { get; set; }

  [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
  [PXUIField(Visible = false)]
  [PXDBScalar(typeof (Search<FSRoute.routeCD, Where<FSRoute.routeID, Equal<FSRouteDocument.routeID>>>))]
  [PXDefault(typeof (Search<FSRoute.routeCD, Where<FSRoute.routeID, Equal<Current<FSRouteDocument.routeID>>>>))]
  public virtual string RouteCD { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Additional Driver Name", Enabled = false)]
  public virtual string MemAdditionalDriverName { get; set; }

  [PXString]
  public virtual string ApproximateValuesLabel
  {
    get => PXMessages.LocalizeNoPrefix("[*] Approximate values. Use for reference only.");
  }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "Start Time")]
  [PXUIField(DisplayName = "Start Time")]
  public virtual DateTime? TimeBeginUTC { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "End Time")]
  [PXUIField(DisplayName = "End Time", Enabled = false, Visible = false)]
  public virtual DateTime? TimeEndUTC { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "Actual Start Time")]
  [PXUIField(DisplayName = "Actual Start Time")]
  public virtual DateTime? ActualStartTimeUTC { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Date", DisplayNameTime = "Actual End Time")]
  [PXUIField(DisplayName = "Actual End Time")]
  public virtual DateTime? ActualEndTimeUTC { get; set; }

  public class PK : PrimaryKeyOf<FSRouteDocument>.By<FSRouteDocument.routeDocumentID>
  {
    public static FSRouteDocument Find(PXGraph graph, int? routeDocumentID, PKFindOptions options = 0)
    {
      return (FSRouteDocument) PrimaryKeyOf<FSRouteDocument>.By<FSRouteDocument.routeDocumentID>.FindBy(graph, (object) routeDocumentID, options);
    }
  }

  public class UK : PrimaryKeyOf<FSRouteDocument>.By<FSRouteDocument.refNbr>
  {
    public static FSRouteDocument Find(PXGraph graph, string refNbr, PKFindOptions options = 0)
    {
      return (FSRouteDocument) PrimaryKeyOf<FSRouteDocument>.By<FSRouteDocument.refNbr>.FindBy(graph, (object) refNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FSRouteDocument>.By<FSRouteDocument.branchID>
    {
    }

    public class Route : 
      PrimaryKeyOf<FSRoute>.By<FSRoute.routeID>.ForeignKeyOf<FSRouteDocument>.By<FSRouteDocument.routeID>
    {
    }

    public class Driver : 
      PrimaryKeyOf<EPEmployee>.By<EPEmployee.bAccountID>.ForeignKeyOf<FSRouteDocument>.By<FSRouteDocument.driverID>
    {
    }

    public class AdditionalDriver : 
      PrimaryKeyOf<EPEmployee>.By<EPEmployee.bAccountID>.ForeignKeyOf<FSRouteDocument>.By<FSRouteDocument.additionalDriverID>
    {
    }

    public class Vehicle : 
      PrimaryKeyOf<FSVehicle>.By<FSVehicle.SMequipmentID>.ForeignKeyOf<FSRouteDocument>.By<FSRouteDocument.vehicleID>
    {
    }

    public class AdditionalVehicle1 : 
      PrimaryKeyOf<FSVehicle>.By<FSVehicle.SMequipmentID>.ForeignKeyOf<FSRouteDocument>.By<FSRouteDocument.additionalVehicleID1>
    {
    }

    public class AdditionalVehicle2 : 
      PrimaryKeyOf<FSVehicle>.By<FSVehicle.SMequipmentID>.ForeignKeyOf<FSRouteDocument>.By<FSRouteDocument.additionalVehicleID2>
    {
    }
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSRouteDocument.refNbr>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRouteDocument.branchID>
  {
  }

  public abstract class routeDocumentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSRouteDocument.routeDocumentID>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSRouteDocument.date>
  {
  }

  public abstract class routeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRouteDocument.routeID>
  {
  }

  public abstract class driverID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRouteDocument.driverID>
  {
  }

  public abstract class additionalDriverID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSRouteDocument.additionalDriverID>
  {
  }

  public abstract class routeStatsUpdated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSRouteDocument.routeStatsUpdated>
  {
  }

  public abstract class status : ListField_Status_Route
  {
  }

  public abstract class timeBegin : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSRouteDocument.timeBegin>
  {
  }

  public abstract class timeEnd : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSRouteDocument.timeEnd>
  {
  }

  public abstract class tripNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRouteDocument.tripNbr>
  {
  }

  public abstract class actualStartTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRouteDocument.actualStartTime>
  {
  }

  public abstract class actualEndTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRouteDocument.actualEndTime>
  {
  }

  public abstract class totalNumAppointments : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSRouteDocument.totalNumAppointments>
  {
  }

  public abstract class totalDuration : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRouteDocument.totalDuration>
  {
  }

  public abstract class totalDistance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSRouteDocument.totalDistance>
  {
  }

  public abstract class totalDistanceFriendly : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRouteDocument.totalDistanceFriendly>
  {
  }

  public abstract class totalServices : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRouteDocument.totalServices>
  {
  }

  public abstract class totalServicesDuration : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSRouteDocument.totalServicesDuration>
  {
  }

  public abstract class totalTravelTime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSRouteDocument.totalTravelTime>
  {
  }

  public abstract class vehicleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRouteDocument.vehicleID>
  {
  }

  public abstract class additionalVehicleID1 : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSRouteDocument.additionalVehicleID1>
  {
  }

  public abstract class additionalVehicleID2 : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSRouteDocument.additionalVehicleID2>
  {
  }

  public abstract class gPSLatitudeStart : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSRouteDocument.gPSLatitudeStart>
  {
  }

  public abstract class gPSLongitudeStart : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSRouteDocument.gPSLongitudeStart>
  {
  }

  public abstract class gPSLatitudeComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSRouteDocument.gPSLatitudeComplete>
  {
  }

  public abstract class gPSLongitudeComplete : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSRouteDocument.gPSLongitudeComplete>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSRouteDocument.noteID>
  {
  }

  public abstract class generatedBySystem : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSRouteDocument.generatedBySystem>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSRouteDocument.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRouteDocument.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRouteDocument.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSRouteDocument.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRouteDocument.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRouteDocument.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSRouteDocument.Tstamp>
  {
  }

  public abstract class miles : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRouteDocument.miles>
  {
  }

  public abstract class weight : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRouteDocument.weight>
  {
  }

  public abstract class fuelQty : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRouteDocument.fuelQty>
  {
  }

  public abstract class fuelType : ListField_FuelType_Equipment
  {
  }

  public abstract class oil : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRouteDocument.oil>
  {
  }

  public abstract class antiFreeze : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRouteDocument.antiFreeze>
  {
  }

  public abstract class dEF : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRouteDocument.dEF>
  {
  }

  public abstract class propane : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRouteDocument.propane>
  {
  }

  public abstract class routeNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRouteDocument.routeNumberingID>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSRouteDocument.selected>
  {
  }

  public abstract class memActualDuration : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSRouteDocument.memActualDuration>
  {
  }

  public abstract class memBusinessDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRouteDocument.memBusinessDateTime>
  {
  }

  public abstract class gpsLatitudeLongitude : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRouteDocument.gpsLatitudeLongitude>
  {
  }

  public abstract class routeCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSRouteDocument.routeCD>
  {
  }

  public abstract class memAdditionalDriverName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRouteDocument.memAdditionalDriverName>
  {
  }

  public abstract class approximateValuesLabel : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRouteDocument.approximateValuesLabel>
  {
  }

  public abstract class timeBeginUTC : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRouteDocument.timeBeginUTC>
  {
  }

  public abstract class timeEndUTC : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRouteDocument.timeEndUTC>
  {
  }

  public abstract class actualStartTimeUTC : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRouteDocument.actualStartTimeUTC>
  {
  }

  public abstract class actualEndTimeUTC : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRouteDocument.actualEndTimeUTC>
  {
  }
}
