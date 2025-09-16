// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryTranHistEnqFilter
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
public class InventoryTranHistEnqFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected DateTime? _StartDate;
  protected DateTime? _EndDate;
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _SubItemCD;
  protected int? _SiteID;
  protected int? _LocationID;
  protected string _LotSerialNbr;
  protected bool? _SummaryByDay;
  protected bool? _IncludeUnreleased;
  protected bool? _ShowAdjUnitCost;

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

  [PXDefault]
  [AnyInventory(typeof (Search<InventoryItem.inventoryID, Where2<Where<InventoryItem.stkItem, NotEqual<boolFalse>, Or<InventoryItem.isConverted, Equal<True>>>, And<Where<Match<Current<AccessInfo.userName>>>>>>), typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItemRawExt(typeof (InventoryTranHistEnqFilter.inventoryID), DisplayName = "Subitem")]
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

  [Location(typeof (InventoryTranHistEnqFilter.siteID))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PX.Objects.IN.LotSerialNbr]
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

  [PXDBBool]
  [PXDefault]
  [PXUIField(DisplayName = "Summary by Day")]
  public virtual bool? SummaryByDay
  {
    get => this._SummaryByDay;
    set => this._SummaryByDay = value;
  }

  [PXDBBool]
  [PXDefault]
  [PXUIField]
  public virtual bool? IncludeUnreleased
  {
    get => this._IncludeUnreleased;
    set => this._IncludeUnreleased = value;
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

  public abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryTranHistEnqFilter.startDate>
  {
  }

  public abstract class endDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventoryTranHistEnqFilter.endDate>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryTranHistEnqFilter.inventoryID>
  {
  }

  public abstract class subItemCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranHistEnqFilter.subItemCD>
  {
  }

  public abstract class subItemCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranHistEnqFilter.subItemCDWildcard>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryTranHistEnqFilter.siteID>
  {
  }

  public abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryTranHistEnqFilter.locationID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranHistEnqFilter.lotSerialNbr>
  {
  }

  public abstract class lotSerialNbrWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranHistEnqFilter.lotSerialNbrWildcard>
  {
  }

  public abstract class summaryByDay : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryTranHistEnqFilter.summaryByDay>
  {
  }

  public abstract class includeUnreleased : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryTranHistEnqFilter.includeUnreleased>
  {
  }

  public abstract class showAdjUnitCost : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryTranHistEnqFilter.showAdjUnitCost>
  {
  }

  public abstract class orgBAccountID : IBqlField, IBqlOperand
  {
  }
}
