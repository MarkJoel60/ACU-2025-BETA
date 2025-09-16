// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.LocationBranchSettings
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXCacheName("Location Settings for Current Branch")]
public class LocationBranchSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (Location.bAccountID))]
  public virtual int? BAccountID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (Location.locationID))]
  [PXParent(typeof (LocationBranchSettings.FK.Location))]
  public virtual int? LocationID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (Search<PX.Objects.GL.Branch.branchID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  public virtual int? BranchID { get; set; }

  [PXDBInt]
  [PXUIField]
  [PXDimensionSelector("INSITE", typeof (INSite.siteID), typeof (INSite.siteCD), DescriptionField = typeof (INSite.descr))]
  [PXRestrictor(typeof (Where<INSite.active, Equal<True>>), "Warehouse '{0}' is inactive", new System.Type[] {typeof (INSite.siteCD)})]
  [PXRestrictor(typeof (Where<INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>>), "The warehouse cannot be selected; it is used for transit.", new System.Type[] {})]
  public virtual int? VSiteID { get; set; }

  [PXDBTimestamp]
  public virtual 
  #nullable disable
  byte[] tstamp { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  public class PK : 
    PrimaryKeyOf<Location>.By<LocationBranchSettings.bAccountID, LocationBranchSettings.locationID, LocationBranchSettings.branchID>
  {
    public static Location Find(
      PXGraph graph,
      int? bAccountID,
      int? locationID,
      int? branchID,
      PKFindOptions options = 0)
    {
      return (Location) PrimaryKeyOf<Location>.By<LocationBranchSettings.bAccountID, LocationBranchSettings.locationID, LocationBranchSettings.branchID>.FindBy(graph, (object) bAccountID, (object) locationID, (object) branchID, options);
    }
  }

  public static class FK
  {
    public class Location : 
      PrimaryKeyOf<Location>.By<Location.bAccountID, Location.locationID>.ForeignKeyOf<LocationBranchSettings>.By<LocationBranchSettings.bAccountID, LocationBranchSettings.locationID>
    {
    }
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationBranchSettings.bAccountID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationBranchSettings.locationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationBranchSettings.branchID>
  {
  }

  public abstract class vSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationBranchSettings.vSiteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  LocationBranchSettings.Tstamp>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    LocationBranchSettings.createdDateTime>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    LocationBranchSettings.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationBranchSettings.createdByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    LocationBranchSettings.lastModifiedDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    LocationBranchSettings.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationBranchSettings.lastModifiedByScreenID>
  {
  }
}
