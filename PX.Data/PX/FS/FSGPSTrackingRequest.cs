// Decompiled with JetBrains decompiler
// Type: PX.FS.FSGPSTrackingRequest
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.SM;
using System;

#nullable enable
namespace PX.FS;

/// <summary>A DAC that is used to set up GPS tracking of a mobile client.</summary>
[Serializable]
public class FSGPSTrackingRequest : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <exclude />
  protected Guid? _RequestID;
  /// <exclude />
  protected 
  #nullable disable
  string _Description;
  /// <exclude />
  protected bool? _IsActive;
  /// <exclude />
  protected Guid? _TrackingID;
  /// <exclude />
  protected string _UserName;
  /// <exclude />
  protected string _DeviceID;
  /// <exclude />
  protected string _DeviceName;
  /// <exclude />
  protected string _TimeZoneID;
  /// <exclude />
  protected bool? _WeeklyOnDay1;
  /// <exclude />
  protected bool? _WeeklyOnDay2;
  /// <exclude />
  protected bool? _WeeklyOnDay3;
  /// <exclude />
  protected bool? _WeeklyOnDay4;
  /// <exclude />
  protected bool? _WeeklyOnDay5;
  /// <exclude />
  protected bool? _WeeklyOnDay6;
  /// <exclude />
  protected bool? _WeeklyOnDay7;
  /// <exclude />
  protected System.DateTime? _StartDate;
  /// <exclude />
  protected System.DateTime? _EndDate;
  /// <exclude />
  protected System.DateTime? _StartTime;
  /// <exclude />
  protected System.DateTime? _EndTime;
  /// <exclude />
  protected short? _Interval;
  /// <exclude />
  protected short? _Distance;
  /// <exclude />
  protected Guid? _NoteID;
  /// <exclude />
  protected Guid? _CreatedByID;
  /// <exclude />
  protected string _CreatedByScreenID;
  /// <exclude />
  protected System.DateTime? _CreatedDateTime;
  /// <exclude />
  protected Guid? _LastModifiedByID;
  /// <exclude />
  protected string _LastModifiedByScreenID;
  /// <exclude />
  protected System.DateTime? _LastModifiedDateTime;
  /// <exclude />
  protected byte[] _TStamp;

  /// <summary>An auto-generated unique key of the GPS tracking request.</summary>
  [PXDBGuid(true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Request ID")]
  public virtual Guid? RequestID
  {
    get => this._RequestID;
    set => this._RequestID = value;
  }

  /// <summary>A description of the GPS tracking request. The property is used to display a description of the GPS tracking request in the UI.</summary>
  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>A Boolean flag that indicates (if set to <tt>true</tt>) that the GPS tracking is active for this tracking request and the system writes the tracking history
  /// for this request.</summary>
  /// <value>The default value is <tt>true</tt>.</value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Is Active")]
  public virtual bool? IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  /// <summary>An auto-generated unique identifier, which is saved in the <see cref="T:PX.FS.FSGPSTrackingHistory" /> DAC.</summary>
  /// <value>You can replace the auto-generated tracking ID with the tracking ID of another tracking request. In this case, these tracking request will have the same
  /// tracking ID in the tracking history. You may need to use this approach, for example, to create two tracking requests for one user to handle the lunch break of
  /// the user.</value>
  [PXDBGuid(true)]
  [PXDefault]
  [PXUIField(DisplayName = "Tracking ID")]
  public virtual Guid? TrackingID
  {
    get => this._TrackingID;
    set => this._TrackingID = value;
  }

  /// <summary>The login of a user in Acumatica ERP.</summary>
  /// <value>You specify the login without the tenant name. If the value is not specified, the system accepts tracking requests from the mobile devices on which any user is
  /// signed in.</value>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "User Name")]
  public virtual string UserName
  {
    get => this._UserName;
    set => this._UserName = value;
  }

  /// <summary>The device ID of the mobile device.</summary>
  /// <value>If the value is an empty string, the system accepts tracking requests from devices with any device ID.</value>
  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Device ID", Visible = false)]
  public virtual string DeviceID
  {
    get => this._DeviceID;
    set => this._DeviceID = value;
  }

  /// <summary>The device name of the mobile device.</summary>
  /// <value>If the value is an empty string, the system accepts tracking requests from devices with any name.</value>
  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Device Name", Visible = false)]
  public virtual string DeviceName
  {
    get => this._DeviceName;
    set => this._DeviceName = value;
  }

  /// <summary>The ID of the time zone in which the <see cref="P:PX.FS.FSGPSTrackingRequest.StartDate" />, <see cref="P:PX.FS.FSGPSTrackingRequest.StartTime" />, <see cref="P:PX.FS.FSGPSTrackingRequest.EndDate" />, <see cref="P:PX.FS.FSGPSTrackingRequest.EndTime" /> are specified.</summary>
  [PXDBString(32 /*0x20*/)]
  [PXDefault("GMTE0000U")]
  [PXUIField(DisplayName = "Time Zone")]
  public virtual string TimeZoneID
  {
    get => this._TimeZoneID;
    set => this._TimeZoneID = value;
  }

  /// <summary>A Boolean value that specifies (if set to <tt>false</tt>) that the mobile device does not report tracking points on Sunday.</summary>
  /// <value>The default value is <tt>true</tt>.</value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Sunday")]
  public virtual bool? WeeklyOnDay1
  {
    get => this._WeeklyOnDay1;
    set => this._WeeklyOnDay1 = value;
  }

  /// <summary>A Boolean value that specifies (if set to <tt>false</tt>) that the mobile device does not report tracking points on Monday.</summary>
  /// <value>The default value is <tt>true</tt>.</value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Monday")]
  public virtual bool? WeeklyOnDay2
  {
    get => this._WeeklyOnDay2;
    set => this._WeeklyOnDay2 = value;
  }

  /// <summary>A Boolean value that specifies (if set to <tt>false</tt>) that the mobile device does not report tracking points on Tuesday.</summary>
  /// <value>The default value is <tt>true</tt>.</value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Tuesday")]
  public virtual bool? WeeklyOnDay3
  {
    get => this._WeeklyOnDay3;
    set => this._WeeklyOnDay3 = value;
  }

  /// <summary>A Boolean value that specifies (if set to <tt>false</tt>) that the mobile device does not report tracking points on Wednesday.</summary>
  /// <value>The default value is <tt>true</tt>.</value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Wednesday")]
  public virtual bool? WeeklyOnDay4
  {
    get => this._WeeklyOnDay4;
    set => this._WeeklyOnDay4 = value;
  }

  /// <summary>A Boolean value that specifies (if set to <tt>false</tt>) that the mobile device does not report tracking points on Thursday.</summary>
  /// <value>The default value is <tt>true</tt>.</value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Thursday")]
  public virtual bool? WeeklyOnDay5
  {
    get => this._WeeklyOnDay5;
    set => this._WeeklyOnDay5 = value;
  }

  /// <summary>A Boolean value that specifies (if set to <tt>false</tt>) that the mobile device does not report tracking points on Friday.</summary>
  /// <value>The default value is <tt>true</tt>.</value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Friday")]
  public virtual bool? WeeklyOnDay6
  {
    get => this._WeeklyOnDay6;
    set => this._WeeklyOnDay6 = value;
  }

  /// <summary>A Boolean value that specifies (if set to <tt>false</tt>) that the mobile device does not report tracking points on Saturday.</summary>
  /// <value>The default value is <tt>true</tt>.</value>
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Saturday")]
  public virtual bool? WeeklyOnDay7
  {
    get => this._WeeklyOnDay7;
    set => this._WeeklyOnDay7 = value;
  }

  /// <summary>The date when the tracking of the mobile device starts.</summary>
  [AUSchedule.AUSeparateDate(typeof (FSGPSTrackingRequest.startTime), UseTimeZone = false)]
  [PXUIField(DisplayName = "Starts On (Date)")]
  public virtual System.DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  /// <summary>The date when the tracking of the mobile device ends.</summary>
  [AUSchedule.AUSeparateDate(typeof (FSGPSTrackingRequest.endTime), UseTimeZone = false)]
  [PXUIField(DisplayName = "Expires On (Date)")]
  public virtual System.DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  /// <summary>The time when the tracking of the mobile device starts.</summary>
  [AUSchedule.AUSeparateTime(typeof (FSGPSTrackingRequest.startDate), UseTimeZone = false, DisplayMask = "t")]
  [PXUIField(DisplayName = "Starts On (Time)")]
  public virtual System.DateTime? StartTime
  {
    get => this._StartTime;
    set => this._StartTime = value;
  }

  /// <summary>The time when the tracking of the mobile device ends.</summary>
  [AUSchedule.AUSeparateTime(typeof (FSGPSTrackingRequest.endDate), UseTimeZone = false, DisplayMask = "t")]
  [PXUIField(DisplayName = "Expires On (Time)")]
  public virtual System.DateTime? EndTime
  {
    get => this._EndTime;
    set => this._EndTime = value;
  }

  /// <summary>The interval in minutes that should be passed from the latest report of the tracking point for the mobile device to report a new tracking point.</summary>
  [PXDBShort]
  [PXUIField(DisplayName = "Interval")]
  [PXDefault(180)]
  public virtual short? Interval
  {
    get => this._Interval;
    set => this._Interval = value;
  }

  /// <summary>The distance in meters for which the mobile device should be moved to report a new tracking point.</summary>
  [PXDBShort]
  [PXUIField(DisplayName = "Distance")]
  [PXDefault(250)]
  public virtual short? Distance
  {
    get => this._Distance;
    set => this._Distance = value;
  }

  /// <exclude />
  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <exclude />
  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  /// <exclude />
  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  /// <exclude />
  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  /// <exclude />
  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  /// <exclude />
  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  /// <exclude />
  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  /// <exclude />
  [PXDBTimestamp]
  public virtual byte[] TStamp
  {
    get => this._TStamp;
    set => this._TStamp = value;
  }

  /// <exclude />
  public abstract class requestID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSGPSTrackingRequest.requestID>
  {
  }

  /// <exclude />
  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSGPSTrackingRequest.description>
  {
  }

  /// <exclude />
  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSGPSTrackingRequest.isActive>
  {
  }

  /// <exclude />
  public abstract class trackingID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSGPSTrackingRequest.trackingID>
  {
  }

  /// <exclude />
  public abstract class userName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSGPSTrackingRequest.userName>
  {
  }

  /// <exclude />
  public abstract class deviceID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSGPSTrackingRequest.deviceID>
  {
  }

  /// <exclude />
  public abstract class deviceName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSGPSTrackingRequest.deviceName>
  {
  }

  /// <exclude />
  public abstract class timeZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSGPSTrackingRequest.timeZoneID>
  {
  }

  /// <exclude />
  public abstract class weeklyOnDay1 : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSGPSTrackingRequest.weeklyOnDay1>
  {
  }

  /// <exclude />
  public abstract class weeklyOnDay2 : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSGPSTrackingRequest.weeklyOnDay2>
  {
  }

  /// <exclude />
  public abstract class weeklyOnDay3 : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSGPSTrackingRequest.weeklyOnDay3>
  {
  }

  /// <exclude />
  public abstract class weeklyOnDay4 : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSGPSTrackingRequest.weeklyOnDay4>
  {
  }

  /// <exclude />
  public abstract class weeklyOnDay5 : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSGPSTrackingRequest.weeklyOnDay5>
  {
  }

  /// <exclude />
  public abstract class weeklyOnDay6 : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSGPSTrackingRequest.weeklyOnDay6>
  {
  }

  /// <exclude />
  public abstract class weeklyOnDay7 : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSGPSTrackingRequest.weeklyOnDay7>
  {
  }

  /// <exclude />
  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    FSGPSTrackingRequest.startDate>
  {
  }

  /// <exclude />
  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  FSGPSTrackingRequest.endDate>
  {
  }

  /// <exclude />
  public abstract class startTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    FSGPSTrackingRequest.startTime>
  {
  }

  /// <exclude />
  public abstract class endTime : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  FSGPSTrackingRequest.endTime>
  {
  }

  /// <exclude />
  public abstract class interval : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSGPSTrackingRequest.interval>
  {
  }

  /// <exclude />
  public abstract class distance : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSGPSTrackingRequest.distance>
  {
  }

  /// <exclude />
  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSGPSTrackingRequest.noteID>
  {
  }

  /// <exclude />
  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSGPSTrackingRequest.createdByID>
  {
  }

  /// <exclude />
  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSGPSTrackingRequest.createdByScreenID>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    FSGPSTrackingRequest.createdDateTime>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSGPSTrackingRequest.lastModifiedByID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSGPSTrackingRequest.lastModifiedByScreenID>
  {
  }

  /// <exclude />
  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    FSGPSTrackingRequest.lastModifiedDateTime>
  {
  }

  /// <exclude />
  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSGPSTrackingRequest.tStamp>
  {
  }
}
