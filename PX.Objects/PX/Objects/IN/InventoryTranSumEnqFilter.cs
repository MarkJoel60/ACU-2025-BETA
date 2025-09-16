// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryTranSumEnqFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class InventoryTranSumEnqFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _FinPeriodID;
  protected int? _InventoryID;
  protected string _SubItemCD;
  protected int? _SiteID;
  protected int? _LocationID;
  protected bool? _ByFinancialPeriod;
  protected bool? _SubItemDetails;
  protected bool? _SiteDetails;
  protected bool? _LocationDetails;
  protected bool? _ShowItemsWithoutMovement;

  [FinPeriodSelector(typeof (AccessInfo.businessDate))]
  [PXDefault]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [AnyInventory(typeof (Search<InventoryItem.inventoryID, Where2<Where<InventoryItem.stkItem, NotEqual<boolFalse>, Or<InventoryItem.isConverted, Equal<True>>>, And<Where<Match<Current<AccessInfo.userName>>>>>>), typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItemRawExt(typeof (InventoryTranSumEnqFilter.inventoryID), DisplayName = "Subitem")]
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

  [Location(typeof (InventoryTranSumEnqFilter.siteID))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "By Financial Period")]
  public virtual bool? ByFinancialPeriod
  {
    get => this._ByFinancialPeriod;
    set => this._ByFinancialPeriod = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Subitem Details", FieldClass = "INSUBITEM")]
  public virtual bool? SubItemDetails
  {
    get => this._SubItemDetails;
    set => this._SubItemDetails = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Warehouse Details")]
  public virtual bool? SiteDetails
  {
    get => this._SiteDetails;
    set => this._SiteDetails = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Location Details")]
  public virtual bool? LocationDetails
  {
    get => this._LocationDetails;
    set => this._LocationDetails = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Show Items Without Movement")]
  public virtual bool? ShowItemsWithoutMovement
  {
    get => this._ShowItemsWithoutMovement;
    set => this._ShowItemsWithoutMovement = value;
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranSumEnqFilter.finPeriodID>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryTranSumEnqFilter.inventoryID>
  {
  }

  public abstract class subItemCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranSumEnqFilter.subItemCD>
  {
  }

  public abstract class subItemCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryTranSumEnqFilter.subItemCDWildcard>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryTranSumEnqFilter.siteID>
  {
  }

  public abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryTranSumEnqFilter.locationID>
  {
  }

  public abstract class byFinancialPeriod : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryTranSumEnqFilter.byFinancialPeriod>
  {
  }

  public abstract class subItemDetails : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryTranSumEnqFilter.subItemDetails>
  {
  }

  public abstract class siteDetails : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryTranSumEnqFilter.siteDetails>
  {
  }

  public abstract class locationDetails : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryTranSumEnqFilter.locationDetails>
  {
  }

  public abstract class showItemsWithoutMovement : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryTranSumEnqFilter.showItemsWithoutMovement>
  {
  }
}
