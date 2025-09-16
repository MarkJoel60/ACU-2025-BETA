// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.LocationTrackingInq
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.FS;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.FS;

public class LocationTrackingInq : PXGraph<
#nullable disable
LocationTrackingInq>
{
  public PXCancel<FSGPSTrackingHistoryFilter> Cancel;
  public PXFilter<FSGPSTrackingHistoryFilter> Filter;
  [PXFilterable(new Type[] {})]
  [PXVirtualDAC]
  public PXSelect<LocationTrackingInq.GPSTrackingHistory> LocationTrackingRecords;
  public PXSelect<LocationTrackingInq.FSGPSTrackingHistoryRaw, Where<LocationTrackingInq.FSGPSTrackingHistoryRaw.executionDate, GreaterEqual<Current<FSGPSTrackingHistoryFilter.startDate>>, And<LocationTrackingInq.FSGPSTrackingHistoryRaw.executionDate, LessEqual<Current<FSGPSTrackingHistoryFilter.endDate>>>>> LocationTrackingRawRecords;
  public PXSelect<LocationTrackingInq.FSGPSTrackingHistoryRaw, Where<LocationTrackingInq.FSGPSTrackingHistoryRaw.executionDate, GreaterEqual<Current<FSGPSTrackingHistoryFilter.startDate>>, And<LocationTrackingInq.FSGPSTrackingHistoryRaw.executionDate, LessEqual<Current<FSGPSTrackingHistoryFilter.endDate>>, And<LocationTrackingInq.FSGPSTrackingHistoryRaw.pKID, Equal<Current<AccessInfo.userID>>>>>> CurrentUserLocationTrackingRecords;
  public PXAction<FSGPSTrackingHistoryFilter> ViewGPSOnMap;

  public LocationTrackingInq()
  {
    ((PXSelectBase) this.LocationTrackingRecords).AllowInsert = false;
    ((PXSelectBase) this.LocationTrackingRecords).AllowUpdate = false;
    ((PXSelectBase) this.LocationTrackingRecords).AllowDelete = false;
  }

  public IEnumerable locationTrackingRecords()
  {
    LocationTrackingInq locationTrackingInq = this;
    ((PXCache) GraphHelper.Caches<LocationTrackingInq.FSGPSTrackingHistoryRaw>((PXGraph) locationTrackingInq)).Clear();
    ((PXCache) GraphHelper.Caches<LocationTrackingInq.FSGPSTrackingHistoryRaw>((PXGraph) locationTrackingInq)).ClearQueryCache();
    PXView pxView = new PXView((PXGraph) locationTrackingInq, true, ((PXSelectBase) locationTrackingInq.LocationTrackingRawRecords).View.BqlSelect);
    int num = 0;
    int startRow = PXView.StartRow;
    object[] currents = PXView.Currents;
    object[] parameters = PXView.Parameters;
    object[] searches = PXView.Searches;
    string[] sortColumns = PXView.SortColumns;
    bool[] descendings = PXView.Descendings;
    PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
    ref int local1 = ref startRow;
    int maximumRows = PXView.MaximumRows;
    ref int local2 = ref num;
    List<object> objectList = pxView.Select(currents, parameters, searches, sortColumns, descendings, pxFilterRowArray, ref local1, maximumRows, ref local2);
    PXView.StartRow = 0;
    LocationTrackingInq.FSGPSTrackingHistoryRaw trackingHistoryRaw1 = PXResultset<LocationTrackingInq.FSGPSTrackingHistoryRaw>.op_Implicit(((PXSelectBase<LocationTrackingInq.FSGPSTrackingHistoryRaw>) locationTrackingInq.CurrentUserLocationTrackingRecords).Select(Array.Empty<object>()));
    LatLng from = (LatLng) null;
    if (trackingHistoryRaw1 != null && trackingHistoryRaw1.Latitude.HasValue && trackingHistoryRaw1.Longitude.HasValue)
      from = new LatLng(trackingHistoryRaw1.Latitude, trackingHistoryRaw1.Longitude);
    foreach (LocationTrackingInq.FSGPSTrackingHistoryRaw trackingHistoryRaw2 in objectList)
    {
      LatLng to = new LatLng(trackingHistoryRaw2.Latitude, trackingHistoryRaw2.Longitude);
      LocationTrackingInq.GPSTrackingHistory gpsTrackingHistory1 = new LocationTrackingInq.GPSTrackingHistory()
      {
        ExecutionDate = trackingHistoryRaw2.ExecutionDate,
        TrackingID = trackingHistoryRaw2.TrackingID,
        Latitude = trackingHistoryRaw2.Latitude,
        Longitude = trackingHistoryRaw2.Longitude,
        Altitude = trackingHistoryRaw2.Altitude,
        CreatedDateTime = trackingHistoryRaw2.CreatedDateTime,
        PKID = trackingHistoryRaw2.PKID,
        Username = trackingHistoryRaw2.Username,
        FullName = trackingHistoryRaw2.FullName,
        Distance = new Decimal?(from != null ? Convert.ToDecimal(Haversine.calculate(from, to, Haversine.DistanceUnit.Miles)) : 0M)
      };
      if (!(((PXSelectBase) locationTrackingInq.LocationTrackingRecords).Cache.Locate((object) gpsTrackingHistory1) is LocationTrackingInq.GPSTrackingHistory gpsTrackingHistory2))
        gpsTrackingHistory2 = (LocationTrackingInq.GPSTrackingHistory) ((PXSelectBase) locationTrackingInq.LocationTrackingRecords).Cache.Insert((object) gpsTrackingHistory1);
      yield return (object) gpsTrackingHistory2;
      ((PXSelectBase) locationTrackingInq.LocationTrackingRecords).Cache.IsDirty = false;
    }
  }

  [PXButton]
  [PXUIField]
  public virtual void viewGPSOnMap()
  {
    if (((PXSelectBase<LocationTrackingInq.GPSTrackingHistory>) this.LocationTrackingRecords).Current == null)
      return;
    new GoogleMapLatLongRedirector().ShowAddressByLocation(((PXSelectBase<LocationTrackingInq.GPSTrackingHistory>) this.LocationTrackingRecords).Current.Latitude, ((PXSelectBase<LocationTrackingInq.GPSTrackingHistory>) this.LocationTrackingRecords).Current.Longitude);
  }

  [PXProjection(typeof (Select4<FSGPSTrackingHistory, Where<FSGPSTrackingHistory.executionDate, GreaterEqual<Current<FSGPSTrackingHistoryFilter.startDate>>, And<FSGPSTrackingHistory.executionDate, LessEqual<Current<FSGPSTrackingHistoryFilter.endDate>>>>, Aggregate<GroupBy<FSGPSTrackingHistory.trackingID>>>))]
  [Serializable]
  public class LatestFSGPSTrackingHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBDate(IsKey = true, PreserveTime = true, UseTimeZone = true, UseSmallDateTime = false, InputMask = "g", BqlField = typeof (FSGPSTrackingHistory.executionDate))]
    [PXDefault]
    [PXUIField(DisplayName = "Execution Date")]
    public virtual DateTime? ExecutionDate { get; set; }

    [PXDBGuid(false, IsKey = true, BqlField = typeof (FSGPSTrackingHistory.trackingID))]
    public virtual Guid? TrackingID { get; set; }

    [PXDBCreatedDateTime(BqlField = typeof (FSGPSTrackingHistory.createdDateTime))]
    public virtual DateTime? CreatedDateTime { get; set; }

    public abstract class executionDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      LocationTrackingInq.LatestFSGPSTrackingHistory.executionDate>
    {
    }

    public abstract class trackingID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      LocationTrackingInq.LatestFSGPSTrackingHistory.trackingID>
    {
    }

    public abstract class createdDateTime : IBqlField, IBqlOperand
    {
    }
  }

  [PXProjection(typeof (Select2<FSGPSTrackingHistory, InnerJoin<LocationTrackingInq.LatestFSGPSTrackingHistory, On<LocationTrackingInq.LatestFSGPSTrackingHistory.trackingID, Equal<FSGPSTrackingHistory.trackingID>, And<LocationTrackingInq.LatestFSGPSTrackingHistory.executionDate, Equal<FSGPSTrackingHistory.executionDate>>>, LeftJoin<FSGPSTrackingRequest, On<FSGPSTrackingRequest.trackingID, Equal<FSGPSTrackingHistory.trackingID>>, LeftJoin<Users, On<Users.username, Equal<FSGPSTrackingRequest.userName>>>>>>))]
  [Serializable]
  public class FSGPSTrackingHistoryRaw : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBDate(IsKey = true, PreserveTime = true, UseTimeZone = true, UseSmallDateTime = false, InputMask = "g", BqlField = typeof (FSGPSTrackingHistory.executionDate))]
    [PXDefault]
    [PXUIField(DisplayName = "Execution Date")]
    public virtual DateTime? ExecutionDate { get; set; }

    [PXDBGuid(false, IsKey = true, BqlField = typeof (FSGPSTrackingHistory.trackingID))]
    [PXDefault]
    [PXUIField(DisplayName = "Tracking ID")]
    public virtual Guid? TrackingID { get; set; }

    [PXDBDecimal(6, BqlField = typeof (FSGPSTrackingHistory.latitude))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Latitude", Enabled = false)]
    public virtual Decimal? Latitude { get; set; }

    [PXDBDecimal(6, BqlField = typeof (FSGPSTrackingHistory.longitude))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Longitude", Enabled = false)]
    public virtual Decimal? Longitude { get; set; }

    [PXDBDecimal(6, BqlField = typeof (FSGPSTrackingHistory.altitude))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Altitude", Enabled = false)]
    public virtual Decimal? Altitude { get; set; }

    [PXDBCreatedDateTime(BqlField = typeof (FSGPSTrackingHistory.createdDateTime))]
    public virtual DateTime? CreatedDateTime { get; set; }

    [PXDBGuidMaintainDeleted(BqlField = typeof (Users.pKID))]
    public virtual Guid? PKID { get; set; }

    [PXDBString(BqlField = typeof (Users.username))]
    [PXUIField]
    public virtual string Username { get; set; }

    [PXDBString(BqlField = typeof (Users.fullName))]
    [PXUIField(DisplayName = "Full Name", Enabled = false)]
    public virtual string FullName { get; set; }

    public abstract class executionDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      LocationTrackingInq.FSGPSTrackingHistoryRaw.executionDate>
    {
    }

    public abstract class trackingID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      LocationTrackingInq.FSGPSTrackingHistoryRaw.trackingID>
    {
    }

    public abstract class latitude : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      LocationTrackingInq.FSGPSTrackingHistoryRaw.latitude>
    {
    }

    public abstract class longitude : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      LocationTrackingInq.FSGPSTrackingHistoryRaw.longitude>
    {
    }

    public abstract class altitude : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      LocationTrackingInq.FSGPSTrackingHistoryRaw.altitude>
    {
    }

    public abstract class createdDateTime : IBqlField, IBqlOperand
    {
    }

    public abstract class pKID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      LocationTrackingInq.FSGPSTrackingHistoryRaw.pKID>
    {
    }

    public abstract class username : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      LocationTrackingInq.FSGPSTrackingHistoryRaw.username>
    {
    }

    public abstract class fullName : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      LocationTrackingInq.FSGPSTrackingHistoryRaw.fullName>
    {
    }
  }

  [PXVirtual]
  public class GPSTrackingHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBDate(IsKey = true, PreserveTime = true, UseTimeZone = true, UseSmallDateTime = false, InputMask = "g")]
    [PXDefault]
    [PXUIField(DisplayName = "Execution Date", Enabled = false)]
    public virtual DateTime? ExecutionDate { get; set; }

    [PXDBGuid(false, IsKey = true)]
    [PXDefault]
    [PXUIField(DisplayName = "Tracking ID", Enabled = false)]
    public virtual Guid? TrackingID { get; set; }

    [PXDBDecimal(6)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Latitude", Enabled = false)]
    public virtual Decimal? Latitude { get; set; }

    [PXDBDecimal(6)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Longitude", Enabled = false)]
    public virtual Decimal? Longitude { get; set; }

    [PXDBDecimal(6)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Altitude", Enabled = false)]
    public virtual Decimal? Altitude { get; set; }

    [PXDBCreatedDateTime]
    public virtual DateTime? CreatedDateTime { get; set; }

    [PXDBGuidMaintainDeleted]
    public virtual Guid? PKID { get; set; }

    [PXDBString]
    [PXUIField]
    public virtual string Username { get; set; }

    [PXDBString]
    [PXUIField(DisplayName = "Full Name", Enabled = false)]
    public virtual string FullName { get; set; }

    [PXDecimal]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Distance", Enabled = false)]
    public virtual Decimal? Distance { get; set; }

    public abstract class executionDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      LocationTrackingInq.GPSTrackingHistory.executionDate>
    {
    }

    public abstract class trackingID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      LocationTrackingInq.GPSTrackingHistory.trackingID>
    {
    }

    public abstract class latitude : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      LocationTrackingInq.GPSTrackingHistory.latitude>
    {
    }

    public abstract class longitude : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      LocationTrackingInq.GPSTrackingHistory.longitude>
    {
    }

    public abstract class altitude : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      LocationTrackingInq.GPSTrackingHistory.altitude>
    {
    }

    public abstract class createdDateTime : IBqlField, IBqlOperand
    {
    }

    public abstract class pKID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      LocationTrackingInq.GPSTrackingHistory.pKID>
    {
    }

    public abstract class username : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      LocationTrackingInq.GPSTrackingHistory.username>
    {
    }

    public abstract class fullName : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      LocationTrackingInq.GPSTrackingHistory.fullName>
    {
    }

    public abstract class distance : IBqlField, IBqlOperand
    {
    }
  }
}
