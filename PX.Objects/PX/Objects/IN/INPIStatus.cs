// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPIStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("Physical Inventory Status")]
[PXProjection(typeof (Select2<INPIStatusItem, InnerJoin<INPIStatusLoc, On<INPIStatusLoc.pIID, Equal<INPIStatusItem.pIID>>>>), Persistent = false)]
[Serializable]
public class INPIStatus : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _RecordID;
  protected int? _LocRecordID;
  protected int? _SiteID;
  protected int? _LocationID;
  protected int? _InventoryID;
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

  [PXDBInt(IsKey = true, BqlField = typeof (INPIStatusItem.recordID))]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (INPIStatusLoc.recordID))]
  public virtual int? LocRecordID
  {
    get => this._LocRecordID;
    set => this._LocRecordID = value;
  }

  [Site(BqlField = typeof (INPIStatusLoc.siteID))]
  [PXDefault]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [Location(typeof (INPIStatus.siteID), BqlField = typeof (INPIStatusLoc.locationID))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [StockItem(BqlField = typeof (INPIStatusItem.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBBool(BqlField = typeof (INPIStatusItem.active))]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active
  {
    get => this._Active;
    set => this._Active = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (INPIStatusItem.pIID))]
  [PXUIField]
  public virtual string PIID
  {
    get => this._PIID;
    set => this._PIID = value;
  }

  [PXDBCreatedByID(BqlField = typeof (INPIStatusItem.createdByID))]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID(BqlField = typeof (INPIStatusItem.createdByScreenID))]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime(BqlField = typeof (INPIStatusItem.createdDateTime))]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID(BqlField = typeof (INPIStatusItem.lastModifiedByID))]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID(BqlField = typeof (INPIStatusItem.lastModifiedByScreenID))]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime(BqlField = typeof (INPIStatusItem.lastModifiedDateTime))]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : PrimaryKeyOf<INPIStatus>.By<INPIStatus.recordID>
  {
    public static INPIStatus Find(PXGraph graph, int? recordID, PKFindOptions options = 0)
    {
      return (INPIStatus) PrimaryKeyOf<INPIStatus>.By<INPIStatus.recordID>.FindBy(graph, (object) recordID, options);
    }
  }

  public static class FK
  {
    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INPIStatus>.By<INPIStatus.siteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INPIStatus>.By<INPIStatus.locationID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INPIStatus>.By<INPIStatus.inventoryID>
    {
    }

    public class PIHeader : 
      PrimaryKeyOf<INPIHeader>.By<INPIHeader.pIID>.ForeignKeyOf<INPIStatus>.By<INPIStatus.pIID>
    {
    }
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIStatus.recordID>
  {
  }

  public abstract class locRecordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIStatus.locRecordID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIStatus.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIStatus.locationID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIStatus.inventoryID>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INPIStatus.active>
  {
  }

  public abstract class pIID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIStatus.pIID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INPIStatus.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPIStatus.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INPIStatus.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INPIStatus.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPIStatus.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INPIStatus.lastModifiedDateTime>
  {
  }
}
