// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSRoute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Route")]
[PXPrimaryGraph(typeof (RouteMaint))]
[Serializable]
public class FSRoute : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  [PXUIField(Enabled = false)]
  public virtual int? RouteID { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
  [PXSelector(typeof (FSRoute.routeCD), DescriptionField = typeof (FSRoute.descr))]
  [PXDefault]
  [NormalizeWhiteSpace]
  [PXUIField]
  public virtual 
  #nullable disable
  string RouteCD { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Monday")]
  public virtual bool? ActiveOnMonday { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Tuesday")]
  public virtual bool? ActiveOnTuesday { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Wednesday")]
  public virtual bool? ActiveOnWednesday { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Thursday ")]
  public virtual bool? ActiveOnThursday { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Friday")]
  public virtual bool? ActiveOnFriday { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Saturday")]
  public virtual bool? ActiveOnSaturday { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Sunday")]
  public virtual bool? ActiveOnSunday { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameTime = "Start Time")]
  [PXUIField(DisplayName = "Start time")]
  [PXDefault]
  public virtual DateTime? BeginTimeOnMonday { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameTime = "Start Time")]
  [PXUIField(DisplayName = "Start time")]
  [PXDefault]
  public virtual DateTime? BeginTimeOnTuesday { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameTime = "Start Time")]
  [PXUIField(DisplayName = "Start time")]
  [PXDefault]
  public virtual DateTime? BeginTimeOnWednesday { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameTime = "Start Time")]
  [PXUIField(DisplayName = "Start time")]
  [PXDefault]
  public virtual DateTime? BeginTimeOnThursday { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameTime = "Start Time")]
  [PXUIField(DisplayName = "Start time")]
  [PXDefault]
  public virtual DateTime? BeginTimeOnFriday { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameTime = "Start Time")]
  [PXUIField(DisplayName = "Start time")]
  [PXDefault]
  public virtual DateTime? BeginTimeOnSaturday { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameTime = "Start Time")]
  [PXUIField(DisplayName = "Start time")]
  [PXDefault]
  public virtual DateTime? BeginTimeOnSunday { get; set; }

  [PXDBInt(MinValue = 0, MaxValue = 2147483647 /*0x7FFFFFFF*/)]
  [PXUIField(DisplayName = "Nbr. Trip(s) per Day")]
  public virtual int? NbrTripOnMonday { get; set; }

  [PXDBInt(MinValue = 0, MaxValue = 2147483647 /*0x7FFFFFFF*/)]
  [PXUIField(DisplayName = "Nbr. Trip(s) per Day")]
  public virtual int? NbrTripOnTuesday { get; set; }

  [PXDBInt(MinValue = 0, MaxValue = 2147483647 /*0x7FFFFFFF*/)]
  [PXUIField(DisplayName = "Nbr. Trip(s) per Day")]
  public virtual int? NbrTripOnWednesday { get; set; }

  [PXDBInt(MinValue = 0, MaxValue = 2147483647 /*0x7FFFFFFF*/)]
  [PXUIField(DisplayName = "Nbr. Trip(s) per Day")]
  public virtual int? NbrTripOnThursday { get; set; }

  [PXDBInt(MinValue = 0, MaxValue = 2147483647 /*0x7FFFFFFF*/)]
  [PXUIField(DisplayName = "Nbr. Trip(s) per Day")]
  public virtual int? NbrTripOnFriday { get; set; }

  [PXDBInt(MinValue = 0, MaxValue = 2147483647 /*0x7FFFFFFF*/)]
  [PXUIField(DisplayName = "Nbr. Trip(s) per Day")]
  public virtual int? NbrTripOnSaturday { get; set; }

  [PXDBInt(MinValue = 0, MaxValue = 2147483647 /*0x7FFFFFFF*/)]
  [PXUIField(DisplayName = "Nbr. Trip(s) per Day")]
  public virtual int? NbrTripOnSunday { get; set; }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Descr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Vehicle Type")]
  [PXSelector(typeof (FSVehicleType.vehicleTypeID), SubstituteKey = typeof (FSVehicleType.vehicleTypeCD), DescriptionField = typeof (FSVehicleType.descr))]
  public virtual int? VehicleTypeID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Origin Route")]
  [PXSelector(typeof (FSRoute.routeID), SubstituteKey = typeof (FSRoute.routeCD), DescriptionField = typeof (FSRoute.descr))]
  public virtual int? OriginRouteID { get; set; }

  [PXDBInt(MinValue = 0, MaxValue = 2147483647 /*0x7FFFFFFF*/)]
  [PXDefault(1)]
  [PXUIField(DisplayName = "Max. Appointment Qty.")]
  public virtual int? MaxAppointmentQty { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "No Limit")]
  public virtual bool? NoAppointmentLimit { get; set; }

  [PXDBString(10, IsUnicode = true, IsFixed = true, InputMask = ">CCCCCCCCCC")]
  [PXUIField]
  public virtual string RouteShort { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Week Code(s) e.g.: 1, 2B, 1ACS")]
  public virtual string WeekCode { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
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

  [PXDBString(4)]
  [PXDefault("BRLC")]
  [PXUIField(DisplayName = "Start Location Type")]
  public virtual string RouteBeginLocationType { get; set; }

  [PXDBString(4)]
  [PXDefault("BRLC")]
  [PXUIField(DisplayName = "End Location Type")]
  public virtual string RouteEndLocationType { get; set; }

  [PXDBInt]
  [PXDefault(typeof (AccessInfo.branchID))]
  [PXUIField(DisplayName = "Branch")]
  [PXSelector(typeof (PX.Objects.GL.Branch.branchID), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  public virtual int? BeginBranchID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (Search<FSxUserPreferences.dfltBranchLocationID, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>, And<UserPreferences.defBranchID, Equal<Current<FSRoute.beginBranchID>>>>>))]
  [PXUIField(DisplayName = "Branch Location")]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<FSRoute.beginBranchID>>>>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  [PXFormula(typeof (Default<FSRoute.beginBranchID>))]
  public virtual int? BeginBranchLocationID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (AccessInfo.branchID))]
  [PXUIField(DisplayName = "Branch")]
  [PXSelector(typeof (PX.Objects.GL.Branch.branchID), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  public virtual int? EndBranchID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (Search<FSxUserPreferences.dfltBranchLocationID, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>, And<UserPreferences.defBranchID, Equal<Current<FSRoute.endBranchID>>>>>))]
  [PXUIField(DisplayName = "Branch Location")]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<FSRoute.endBranchID>>>>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  [PXFormula(typeof (Default<FSRoute.endBranchID>))]
  public virtual int? EndBranchLocationID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Start Branch")]
  public virtual string BeginBranchCD { get; set; }

  [PXString]
  [PXUIField(DisplayName = "End Branch")]
  public virtual string EndBranchCD { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Start Branch Location")]
  public virtual string BeginBranchLocationCD { get; set; }

  [PXString]
  [PXUIField(DisplayName = "End Branch Location")]
  public virtual string EndBranchLocationCD { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Route Name", Enabled = false)]
  public virtual string MemRouteName { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Route", Enabled = false)]
  public virtual string MemRoute { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Route Description", Enabled = false)]
  public virtual string MemRouteDescription { get; set; }

  public class PK : PrimaryKeyOf<FSRoute>.By<FSRoute.routeID>
  {
    public static FSRoute Find(PXGraph graph, int? routeID, PKFindOptions options = 0)
    {
      return (FSRoute) PrimaryKeyOf<FSRoute>.By<FSRoute.routeID>.FindBy(graph, (object) routeID, options);
    }
  }

  public class UK : PrimaryKeyOf<FSRoute>.By<FSRoute.routeCD>
  {
    public static FSRoute Find(PXGraph graph, string routeCD, PKFindOptions options = 0)
    {
      return (FSRoute) PrimaryKeyOf<FSRoute>.By<FSRoute.routeCD>.FindBy(graph, (object) routeCD, options);
    }
  }

  public static class FK
  {
    public class VehicleType : 
      PrimaryKeyOf<FSVehicleType>.By<FSVehicleType.vehicleTypeID>.ForeignKeyOf<FSRoute>.By<FSRoute.vehicleTypeID>
    {
    }

    public class OriginRoute : 
      PrimaryKeyOf<FSRoute>.By<FSRoute.routeID>.ForeignKeyOf<FSRoute>.By<FSRoute.originRouteID>
    {
    }

    public class BeginBranch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FSRoute>.By<FSRoute.beginBranchID>
    {
    }

    public class EndBranch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FSRoute>.By<FSRoute.endBranchID>
    {
    }

    public class BeginBranchLocation : 
      PrimaryKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationID>.ForeignKeyOf<FSRoute>.By<FSRoute.beginBranchLocationID>
    {
    }

    public class EndBranchLocation : 
      PrimaryKeyOf<FSBranchLocation>.By<FSBranchLocation.branchLocationID>.ForeignKeyOf<FSRoute>.By<FSRoute.endBranchLocationID>
    {
    }
  }

  public abstract class routeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRoute.routeID>
  {
  }

  public abstract class routeCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSRoute.routeCD>
  {
  }

  public abstract class activeOnMonday : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSRoute.activeOnMonday>
  {
  }

  public abstract class activeOnTuesday : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSRoute.activeOnTuesday>
  {
  }

  public abstract class activeOnWednesday : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSRoute.activeOnWednesday>
  {
  }

  public abstract class activeOnThursday : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSRoute.activeOnThursday>
  {
  }

  public abstract class activeOnFriday : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSRoute.activeOnFriday>
  {
  }

  public abstract class activeOnSaturday : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSRoute.activeOnSaturday>
  {
  }

  public abstract class activeOnSunday : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSRoute.activeOnSunday>
  {
  }

  public abstract class beginTimeOnMonday : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRoute.beginTimeOnMonday>
  {
  }

  public abstract class beginTimeOnTuesday : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRoute.beginTimeOnTuesday>
  {
  }

  public abstract class beginTimeOnWednesday : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRoute.beginTimeOnWednesday>
  {
  }

  public abstract class beginTimeOnThursday : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRoute.beginTimeOnThursday>
  {
  }

  public abstract class beginTimeOnFriday : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRoute.beginTimeOnFriday>
  {
  }

  public abstract class beginTimeOnSaturday : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRoute.beginTimeOnSaturday>
  {
  }

  public abstract class beginTimeOnSunday : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRoute.beginTimeOnSunday>
  {
  }

  public abstract class nbrTripOnMonday : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRoute.nbrTripOnMonday>
  {
  }

  public abstract class nbrTripOnTuesday : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRoute.nbrTripOnTuesday>
  {
  }

  public abstract class nbrTripOnWednesday : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRoute.nbrTripOnWednesday>
  {
  }

  public abstract class nbrTripOnThursday : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRoute.nbrTripOnThursday>
  {
  }

  public abstract class nbrTripOnFriday : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRoute.nbrTripOnFriday>
  {
  }

  public abstract class nbrTripOnSaturday : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRoute.nbrTripOnSaturday>
  {
  }

  public abstract class nbrTripOnSunday : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRoute.nbrTripOnSunday>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSRoute.descr>
  {
  }

  public abstract class vehicleTypeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRoute.vehicleTypeID>
  {
  }

  public abstract class originRouteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRoute.originRouteID>
  {
  }

  public abstract class maxAppointmentQty : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRoute.maxAppointmentQty>
  {
  }

  public abstract class noAppointmentLimit : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSRoute.noAppointmentLimit>
  {
  }

  public abstract class routeShort : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSRoute.routeShort>
  {
  }

  public abstract class weekCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSRoute.weekCode>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSRoute.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSRoute.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRoute.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRoute.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSRoute.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRoute.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSRoute.lastModifiedDateTime>
  {
  }

  public abstract class routeBeginLocationType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRoute.routeBeginLocationType>
  {
  }

  public abstract class routeEndLocationType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRoute.routeEndLocationType>
  {
  }

  public abstract class beginBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRoute.beginBranchID>
  {
  }

  public abstract class beginBranchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSRoute.beginBranchLocationID>
  {
  }

  public abstract class endBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSRoute.endBranchID>
  {
  }

  public abstract class endBranchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSRoute.endBranchLocationID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSRoute.Tstamp>
  {
  }

  public abstract class beginBranchCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSRoute.beginBranchCD>
  {
  }

  public abstract class endBranchCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSRoute.endBranchCD>
  {
  }

  public abstract class beginBranchLocationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRoute.beginBranchLocationCD>
  {
  }

  public abstract class endBranchLocationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRoute.endBranchLocationCD>
  {
  }

  public abstract class memRouteName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSRoute.memRouteName>
  {
  }

  public abstract class memRoute : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSRoute.memRoute>
  {
  }

  public abstract class memRouteDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSRoute.memRouteDescription>
  {
  }
}
