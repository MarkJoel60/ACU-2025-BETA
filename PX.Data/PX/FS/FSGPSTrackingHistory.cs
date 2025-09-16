// Decompiled with JetBrains decompiler
// Type: PX.FS.FSGPSTrackingHistory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.FS;

/// <summary>The DAC that works with the tracking history. The tracking history contains tracking points. The system saves tracking points only for active
/// (<see cref="P:PX.FS.FSGPSTrackingRequest.IsActive" />=<tt>true</tt>) tracking requests.</summary>
[Serializable]
public class FSGPSTrackingHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <exclude />
  protected Guid? _TrackingID;
  /// <exclude />
  protected System.DateTime? _ExecutionDate;
  /// <exclude />
  protected Decimal? _Latitude;
  /// <exclude />
  protected Decimal? _Longitude;
  /// <exclude />
  protected Decimal? _Altitude;
  /// <exclude />
  protected System.DateTime? _CreatedDateTime;

  /// <summary>The identifier of the tracking request, which allows to match the tracking point with the details of the tracking request, such as the user name and the
  /// device name.</summary>
  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Tracking ID")]
  public virtual Guid? TrackingID
  {
    get => this._TrackingID;
    set => this._TrackingID = value;
  }

  /// <summary>The date and time when the tracking point was recorded.</summary>
  [PXDBDate(IsKey = true, PreserveTime = true, UseTimeZone = true, UseSmallDateTime = false, InputMask = "g")]
  [PXDefault]
  [PXUIField(DisplayName = "Execution Date")]
  public virtual System.DateTime? ExecutionDate
  {
    get => this._ExecutionDate;
    set => this._ExecutionDate = value;
  }

  /// <summary>The latitude of the tracking point.</summary>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Latitude")]
  public virtual Decimal? Latitude
  {
    get => this._Latitude;
    set => this._Latitude = value;
  }

  /// <summary>The longitude of the tracking point.</summary>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Longitude")]
  public virtual Decimal? Longitude
  {
    get => this._Longitude;
    set => this._Longitude = value;
  }

  /// <summary>The altitude of the tracking point.</summary>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Altitude")]
  public virtual Decimal? Altitude
  {
    get => this._Altitude;
    set => this._Altitude = value;
  }

  /// <exclude />
  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  /// <exclude />
  public abstract class trackingID : BqlType<IBqlGuid, Guid>.Field<
  #nullable disable
  FSGPSTrackingHistory.trackingID>
  {
  }

  /// <exclude />
  public abstract class executionDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    FSGPSTrackingHistory.executionDate>
  {
  }

  /// <exclude />
  public abstract class latitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSGPSTrackingHistory.latitude>
  {
  }

  /// <exclude />
  public abstract class longitude : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSGPSTrackingHistory.longitude>
  {
  }

  /// <exclude />
  public abstract class altitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSGPSTrackingHistory.altitude>
  {
  }

  /// <exclude />
  public abstract class createdDateTime : IBqlField, IBqlOperand
  {
  }
}
