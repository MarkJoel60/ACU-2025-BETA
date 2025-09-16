// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSGeoZone
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Service Area")]
[PXPrimaryGraph(typeof (GeoZoneMaint))]
[Serializable]
public class FSGeoZone : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  [PXUIField(DisplayName = "Service Area", Enabled = false)]
  public virtual int? GeoZoneID { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
  [PXUIField]
  [PXSelector(typeof (FSGeoZone.geoZoneCD), DescriptionField = typeof (FSGeoZone.descr))]
  [PXDefault]
  [NormalizeWhiteSpace]
  public virtual 
  #nullable disable
  string GeoZoneCD { get; set; }

  [PXDBString(2, IsUnicode = true)]
  [PXUIField(DisplayName = "Country")]
  [PXDefault]
  [Country]
  public virtual string CountryID { get; set; }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Descr { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "Created By Screen ID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "Last Modified By Screen ID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<FSGeoZone>.By<FSGeoZone.geoZoneID>
  {
    public static FSGeoZone Find(PXGraph graph, int? geoZoneID, PKFindOptions options = 0)
    {
      return (FSGeoZone) PrimaryKeyOf<FSGeoZone>.By<FSGeoZone.geoZoneID>.FindBy(graph, (object) geoZoneID, options);
    }
  }

  public class UK : PrimaryKeyOf<FSGeoZone>.By<FSGeoZone.geoZoneCD>
  {
    public static FSGeoZone Find(PXGraph graph, string geoZoneCD, PKFindOptions options = 0)
    {
      return (FSGeoZone) PrimaryKeyOf<FSGeoZone>.By<FSGeoZone.geoZoneCD>.FindBy(graph, (object) geoZoneCD, options);
    }
  }

  public static class FK
  {
    public class Country : 
      PrimaryKeyOf<PX.Objects.CS.Country>.By<PX.Objects.CS.Country.countryID>.ForeignKeyOf<FSGeoZone>.By<FSGeoZone.countryID>
    {
    }
  }

  public abstract class geoZoneID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSGeoZone.geoZoneID>
  {
  }

  public abstract class geoZoneCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSGeoZone.geoZoneCD>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSGeoZone.countryID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSGeoZone.descr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSGeoZone.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSGeoZone.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSGeoZone.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSGeoZone.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSGeoZone.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSGeoZone.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSGeoZone.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSGeoZone.Tstamp>
  {
  }
}
