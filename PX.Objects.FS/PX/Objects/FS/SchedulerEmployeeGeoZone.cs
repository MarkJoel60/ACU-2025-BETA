// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SchedulerEmployeeGeoZone
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable enable
namespace PX.Objects.FS;

/// <exclude />
[PXProjection(typeof (SelectFrom<FSGeoZone>))]
[PXCacheName("Appointment Geo Zone")]
[Serializable]
public class SchedulerEmployeeGeoZone : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(BqlField = typeof (FSGeoZone.geoZoneID))]
  [PXUIField(DisplayName = "Employee Service Area ID", Enabled = false)]
  public virtual int? GeoZoneID { get; set; }

  [PXDBString(BqlField = typeof (FSGeoZone.geoZoneCD))]
  [PXUIField]
  [PXSelector(typeof (FSGeoZone.geoZoneCD), DescriptionField = typeof (FSGeoZone.descr))]
  [NormalizeWhiteSpace]
  public virtual 
  #nullable disable
  string GeoZoneCD { get; set; }

  [PXDBString(BqlField = typeof (FSGeoZone.descr))]
  [PXUIField(DisplayName = "Employee Service Area Desc.", Enabled = false)]
  public virtual string Descr { get; set; }

  public abstract class geoZoneID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SchedulerEmployeeGeoZone.geoZoneID>
  {
  }

  public abstract class geoZoneCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerEmployeeGeoZone.geoZoneCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerEmployeeGeoZone.descr>
  {
  }
}
