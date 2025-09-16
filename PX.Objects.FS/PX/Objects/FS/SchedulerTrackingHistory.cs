// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SchedulerTrackingHistory
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.FS;
using PX.Objects.EP;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.FS;

/// <exclude />
[PXProjection(typeof (Select2<EPEmployee, InnerJoin<Users, On<BqlOperand<Users.pKID, IBqlGuid>.IsEqual<EPEmployee.userID>>, InnerJoin<FSGPSTrackingRequest, On<BqlOperand<FSGPSTrackingRequest.userName, IBqlString>.IsEqual<Users.username>>, InnerJoin<FSGPSTrackingHistory, On<BqlOperand<FSGPSTrackingHistory.trackingID, IBqlGuid>.IsEqual<FSGPSTrackingRequest.trackingID>>>>>>))]
[PXCacheName("Scheduler Tracking History")]
[Serializable]
public class SchedulerTrackingHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(BqlField = typeof (EPEmployee.bAccountID))]
  public virtual int? BAccountID { get; set; }

  [PXDBString(BqlField = typeof (Users.fullName))]
  public virtual 
  #nullable disable
  string FullName { get; set; }

  [PXDBDate(BqlField = typeof (FSGPSTrackingHistory.executionDate))]
  public virtual DateTime? ExecutionDate { get; set; }

  [PXDBDecimal(BqlField = typeof (FSGPSTrackingHistory.latitude))]
  public virtual Decimal? Latitude { get; set; }

  [PXDBDecimal(BqlField = typeof (FSGPSTrackingHistory.longitude))]
  public virtual Decimal? Longitude { get; set; }

  [PXDBDecimal(BqlField = typeof (FSGPSTrackingHistory.altitude))]
  public virtual Decimal? Altitude { get; set; }

  [PXBool]
  public virtual bool? TrackLocation { get; set; }

  [PXInt]
  public virtual int? Interval { get; set; }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SchedulerTrackingHistory.bAccountID>
  {
  }

  public abstract class fullName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerTrackingHistory.fullName>
  {
  }

  public abstract class executionDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SchedulerTrackingHistory.executionDate>
  {
  }

  public abstract class latitude : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SchedulerTrackingHistory.latitude>
  {
  }

  public abstract class longitude : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SchedulerTrackingHistory.longitude>
  {
  }

  public abstract class altitude : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SchedulerTrackingHistory.altitude>
  {
  }

  public abstract class trackLocation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SchedulerTrackingHistory.trackLocation>
  {
  }

  public abstract class interval : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SchedulerTrackingHistory.interval>
  {
  }
}
