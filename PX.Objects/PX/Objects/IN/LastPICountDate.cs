// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.LastPICountDate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXProjection(typeof (Select5<INPIHeader, InnerJoin<INPIDetail, On<INPIDetail.FK.PIHeader>>, Where<INPIHeader.status, Equal<INPIHdrStatus.completed>, And<INPIDetail.status, Equal<INPIDetStatus.entered>>>, Aggregate<GroupBy<INPIHeader.siteID, GroupBy<INPIDetail.inventoryID, GroupBy<INPIDetail.subItemID, GroupBy<INPIDetail.locationID, Max<INPIHeader.countDate>>>>>>>))]
[PXHidden]
[Serializable]
public class LastPICountDate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _SiteID;
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected int? _LocationID;
  protected DateTime? _LastCountDate;

  [Site(BqlField = typeof (INPIHeader.siteID))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [Inventory(BqlField = typeof (INPIDetail.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDefault(typeof (Search<InventoryItem.defaultSubItemID, Where<InventoryItem.inventoryID, Equal<Current<INPIDetail.inventoryID>>, And<InventoryItem.defaultSubItemOnEntry, Equal<boolTrue>>>>))]
  [SubItem(typeof (INPIDetail.inventoryID), BqlField = typeof (INPIDetail.subItemID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [Location(typeof (INPIHeader.siteID))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBDate(BqlField = typeof (INPIHeader.countDate))]
  public virtual DateTime? LastCountDate
  {
    get => this._LastCountDate;
    set => this._LastCountDate = value;
  }

  public abstract class siteID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  LastPICountDate.siteID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LastPICountDate.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LastPICountDate.subItemID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LastPICountDate.locationID>
  {
  }

  public abstract class lastCountDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    LastPICountDate.lastCountDate>
  {
  }
}
