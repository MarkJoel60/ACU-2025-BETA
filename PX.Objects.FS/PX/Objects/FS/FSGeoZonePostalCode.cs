// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSGeoZonePostalCode
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

[PXCacheName("Service Area - Postal Code")]
[Serializable]
public class FSGeoZonePostalCode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Service Area", Enabled = false, Visible = false)]
  [PXSelector(typeof (Search<FSGeoZone.geoZoneID>), SubstituteKey = typeof (FSGeoZone.geoZoneCD), DescriptionField = typeof (FSGeoZone.descr))]
  [PXDBDefault(typeof (FSGeoZone.geoZoneID))]
  [PXParent(typeof (Select<FSGeoZone, Where<FSGeoZone.geoZoneID, Equal<Current<FSGeoZonePostalCode.geoZoneID>>>>))]
  public virtual int? GeoZoneID { get; set; }

  [PXDBString(2, IsUnicode = true)]
  [PXUIField(DisplayName = "Country")]
  [PXDefault(typeof (FSGeoZone.countryID))]
  [Country]
  public virtual 
  #nullable disable
  string CountryID { get; set; }

  [PXDBString(25, IsKey = true)]
  [PXDefault]
  [NormalizeWhiteSpace]
  [PXUIField(DisplayName = "Postal Code")]
  [PXZipValidation(typeof (PX.Objects.CS.Country.zipCodeRegexp), typeof (PX.Objects.CS.Country.zipCodeMask), typeof (FSGeoZonePostalCode.countryID))]
  public virtual string PostalCode { get; set; }

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

  public class PK : 
    PrimaryKeyOf<FSGeoZonePostalCode>.By<FSGeoZonePostalCode.geoZoneID, FSGeoZonePostalCode.postalCode>
  {
    public static FSGeoZonePostalCode Find(
      PXGraph graph,
      int? geoZoneID,
      string postalCode,
      PKFindOptions options = 0)
    {
      return (FSGeoZonePostalCode) PrimaryKeyOf<FSGeoZonePostalCode>.By<FSGeoZonePostalCode.geoZoneID, FSGeoZonePostalCode.postalCode>.FindBy(graph, (object) geoZoneID, (object) postalCode, options);
    }
  }

  public static class FK
  {
    public class GeoZone : 
      PrimaryKeyOf<FSGeoZone>.By<FSGeoZone.geoZoneID>.ForeignKeyOf<FSGeoZonePostalCode>.By<FSGeoZonePostalCode.geoZoneID>
    {
    }
  }

  public abstract class geoZoneID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSGeoZonePostalCode.geoZoneID>
  {
  }

  public abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSGeoZonePostalCode.countryID>
  {
  }

  public abstract class postalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSGeoZonePostalCode.postalCode>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSGeoZonePostalCode.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSGeoZonePostalCode.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSGeoZonePostalCode.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSGeoZonePostalCode.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSGeoZonePostalCode.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSGeoZonePostalCode.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSGeoZonePostalCode.Tstamp>
  {
  }
}
