// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INLotSerFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class INLotSerFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _LotSerialNbr;
  protected int? _InventoryID;
  protected string _SubItemCD;
  protected int? _SiteID;
  protected int? _LocationID;
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;
  protected bool? _ShowAdjUnitCost;

  [PX.Objects.IN.LotSerialNbr]
  [PXDefault]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXDBString(100, IsUnicode = true)]
  public virtual string LotSerialNbrWildcard
  {
    get
    {
      return PXDatabase.Provider.SqlDialect.WildcardAnything + this._LotSerialNbr + PXDatabase.Provider.SqlDialect.WildcardAnything;
    }
  }

  [AnyInventory(typeof (Search<InventoryItem.inventoryID, Where2<Where<InventoryItem.stkItem, NotEqual<boolFalse>, Or<InventoryItem.isConverted, Equal<True>>>, And<Where<Match<Current<AccessInfo.userName>>>>>>), typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItemRawExt(typeof (INLotSerFilter.inventoryID), DisplayName = "Subitem")]
  public virtual string SubItemCD
  {
    get => this._SubItemCD;
    set => this._SubItemCD = value;
  }

  [PXDBString(30, IsUnicode = true)]
  public virtual string SubItemCDWildcard
  {
    get => SubCDUtils.CreateSubCDWildcard(this._SubItemCD, "INSUBITEM");
  }

  [Site(DescriptionField = typeof (INSite.descr), Required = false, DisplayName = "Warehouse")]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [Location(typeof (INLotSerFilter.siteID))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Start Date")]
  public virtual DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "End Date")]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [PXDBBool]
  [PXDefault]
  [PXUIField]
  public virtual bool? ShowAdjUnitCost
  {
    get => this._ShowAdjUnitCost;
    set => this._ShowAdjUnitCost = value;
  }

  [OrganizationTree(null, true, FieldClass = "MultipleBaseCurrencies", Required = true)]
  public int? OrgBAccountID { get; set; }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INLotSerFilter.lotSerialNbr>
  {
  }

  public abstract class lotSerialNbrWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerFilter.lotSerialNbrWildcard>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INLotSerFilter.inventoryID>
  {
  }

  public abstract class subItemCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INLotSerFilter.subItemCD>
  {
  }

  public abstract class subItemCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerFilter.subItemCDWildcard>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INLotSerFilter.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INLotSerFilter.locationID>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INLotSerFilter.startDate>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INLotSerFilter.endDate>
  {
  }

  public abstract class showAdjUnitCost : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INLotSerFilter.showAdjUnitCost>
  {
  }

  public abstract class orgBAccountID : IBqlField, IBqlOperand
  {
  }
}
