// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.OccupationalGeoZoneHelper
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class OccupationalGeoZoneHelper : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt(IsKey = true)]
  [PXUIField]
  public virtual int? GeoZoneID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Service Area ID")]
  public virtual 
  #nullable disable
  string GeoZoneCD { get; set; }

  [PXDecimal(2)]
  [PXUIField(DisplayName = "Scheduled Hours", Enabled = false)]
  public virtual Decimal? ScheduledHours { get; set; }

  [PXDecimal(2)]
  [PXUIField(DisplayName = "Appointment Hours", Enabled = false)]
  public virtual Decimal? AppointmentHours { get; set; }

  [PXDecimal(2)]
  [PXUIField(DisplayName = "Idle Rate (%)", Enabled = false)]
  public virtual Decimal? IdleRate { get; set; }

  [PXDecimal(2)]
  [PXUIField(DisplayName = "Occupational Rate (%)", Enabled = false)]
  public virtual Decimal? OccupationalRate { get; set; }

  public abstract class geoZoneID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  OccupationalGeoZoneHelper.geoZoneID>
  {
  }

  public abstract class geoZoneCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OccupationalGeoZoneHelper.geoZoneCD>
  {
  }

  public abstract class scheduledHours : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    OccupationalGeoZoneHelper.scheduledHours>
  {
  }

  public abstract class appointmentHours : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    OccupationalGeoZoneHelper.appointmentHours>
  {
  }

  public abstract class idleRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    OccupationalGeoZoneHelper.idleRate>
  {
  }

  public abstract class occupationalRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    OccupationalGeoZoneHelper.occupationalRate>
  {
  }
}
