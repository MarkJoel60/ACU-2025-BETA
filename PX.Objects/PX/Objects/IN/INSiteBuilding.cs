// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSiteBuilding
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName]
public class INSiteBuilding : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  [PXReferentialIntegrityCheck]
  public virtual int? BuildingID { get; set; }

  [PXDBString(30, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search<INSiteBuilding.buildingCD>), SubstituteKey = typeof (INSiteBuilding.buildingCD), CacheGlobal = true)]
  [PXDefault]
  public virtual 
  #nullable disable
  string BuildingCD { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Descr { get; set; }

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID { get; set; }

  [PXDBInt]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Address.addressID))]
  public virtual int? AddressID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : PrimaryKeyOf<INSiteBuilding>.By<INSiteBuilding.buildingID>
  {
    public static INSiteBuilding Find(PXGraph graph, int? buildingID, PKFindOptions options = 0)
    {
      return (INSiteBuilding) PrimaryKeyOf<INSiteBuilding>.By<INSiteBuilding.buildingID>.FindBy(graph, (object) buildingID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<INSiteBuilding>.By<INSiteBuilding.branchID>
    {
    }

    public class Address : 
      PrimaryKeyOf<PX.Objects.CR.Address>.By<PX.Objects.CR.Address.addressID>.ForeignKeyOf<INSiteBuilding>.By<INSiteBuilding.addressID>
    {
    }
  }

  public abstract class buildingID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteBuilding.buildingID>
  {
  }

  public abstract class buildingCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSiteBuilding.buildingCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSiteBuilding.descr>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteBuilding.branchID>
  {
  }

  public abstract class addressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteBuilding.addressID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INSiteBuilding.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INSiteBuilding.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSiteBuilding.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INSiteBuilding.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INSiteBuilding.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSiteBuilding.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INSiteBuilding.lastModifiedDateTime>
  {
  }
}
