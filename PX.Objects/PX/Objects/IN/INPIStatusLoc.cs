// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPIStatusLoc
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN.PhysicalInventory;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class INPIStatusLoc : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IPILock
{
  protected int? _RecordID;
  protected int? _SiteID;
  protected int? _LocationID;
  protected bool? _Active;
  protected 
  #nullable disable
  string _PIID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  [Site]
  [PXDefault]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [Location(typeof (INPIStatusLoc.siteID))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Frozen")]
  public virtual bool? Active
  {
    get => this._Active;
    set => this._Active = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXDBDefault(typeof (INPIHeader.pIID))]
  [PXUIField]
  [PXParent(typeof (INPIStatusLoc.FK.PIHeader))]
  [PXSelector(typeof (Search<INPIHeader.pIID, Where<INPIHeader.status, NotEqual<INPIHdrStatus.completed>>>), new Type[] {typeof (INPIHeader.pIID), typeof (INPIHeader.descr), typeof (INPIHeader.status), typeof (INPIHeader.countDate), typeof (INPIHeader.pIAdjRefNbr)})]
  public virtual string PIID
  {
    get => this._PIID;
    set => this._PIID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Excluded")]
  public virtual bool? Excluded { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : PrimaryKeyOf<INPIStatusLoc>.By<INPIStatusLoc.recordID>
  {
    public static INPIStatusLoc Find(PXGraph graph, int? recordID, PKFindOptions options = 0)
    {
      return (INPIStatusLoc) PrimaryKeyOf<INPIStatusLoc>.By<INPIStatusLoc.recordID>.FindBy(graph, (object) recordID, options);
    }
  }

  public static class FK
  {
    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INPIStatusLoc>.By<INPIStatusLoc.siteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INPIStatusLoc>.By<INPIStatusLoc.locationID>
    {
    }

    public class PIHeader : 
      PrimaryKeyOf<INPIHeader>.By<INPIHeader.pIID>.ForeignKeyOf<INPIStatusLoc>.By<INPIStatusLoc.pIID>
    {
    }
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIStatusLoc.recordID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIStatusLoc.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIStatusLoc.locationID>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INPIStatusLoc.active>
  {
  }

  public abstract class pIID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIStatusLoc.pIID>
  {
  }

  public abstract class excluded : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INPIStatusLoc.excluded>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INPIStatusLoc.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPIStatusLoc.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INPIStatusLoc.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INPIStatusLoc.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPIStatusLoc.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INPIStatusLoc.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INPIStatusLoc.Tstamp>
  {
  }
}
