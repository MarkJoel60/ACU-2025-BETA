// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSiteStatusSelected
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common.Interfaces;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.DAC.Projections;
using System;

#nullable enable
namespace PX.Objects.IN;

[INSiteStatusLookupProjection(Persistent = false)]
public class INSiteStatusSelected : 
  PXBqlTable,
  IQtySelectable,
  IPXSelectable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IFTSSelectable
{
  protected bool? _Selected = new bool?(false);
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _InventoryCD;
  protected string _Descr;
  protected int? _ItemClassID;
  protected string _ItemClassCD;
  protected string _ItemClassDescription;
  protected string _PriceClassID;
  protected string _PriceClassDescription;
  protected string _BarCode;
  protected int? _SiteID;
  protected string _SiteCD;
  protected int? _LocationID;
  protected string _locationCD;
  protected int? _SubItemID;
  protected string _SubItemCD;
  protected string _BaseUnit;
  protected Decimal? _QtySelected;
  protected Decimal? _QtyOnHand;
  protected Decimal? _QtyAvail;
  protected Guid? _NoteID;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBInt(BqlField = typeof (InventoryItem.inventoryID), IsKey = true)]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDefault]
  [InventoryRaw(BqlField = typeof (InventoryItem.inventoryCD))]
  public virtual string InventoryCD
  {
    get => this._InventoryCD;
    set => this._InventoryCD = value;
  }

  [PXDBLocalizableString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (InventoryItem.descr), IsProjection = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBInt(BqlField = typeof (InventoryItem.itemClassID))]
  [PXUIField(DisplayName = "Item Class ID", Visible = false)]
  [PXDimensionSelector("INITEMCLASS", typeof (INItemClass.itemClassID), typeof (INItemClass.itemClassCD), ValidComboRequired = true)]
  public virtual int? ItemClassID
  {
    get => this._ItemClassID;
    set => this._ItemClassID = value;
  }

  [PXDBString(30, IsUnicode = true, BqlField = typeof (INItemClass.itemClassCD))]
  public virtual string ItemClassCD
  {
    get => this._ItemClassCD;
    set => this._ItemClassCD = value;
  }

  [PXDBLocalizableString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (INItemClass.descr), IsProjection = true)]
  [PXUIField(DisplayName = "Item Class Description", Visible = false)]
  public virtual string ItemClassDescription
  {
    get => this._ItemClassDescription;
    set => this._ItemClassDescription = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (InventoryItem.priceClassID))]
  [PXUIField(DisplayName = "Price Class ID", Visible = false)]
  public virtual string PriceClassID
  {
    get => this._PriceClassID;
    set => this._PriceClassID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (INPriceClass.description))]
  [PXUIField(DisplayName = "Price Class Description", Visible = false)]
  public virtual string PriceClassDescription
  {
    get => this._PriceClassDescription;
    set => this._PriceClassDescription = value;
  }

  [PXDBString(255 /*0xFF*/, BqlField = typeof (INItemXRef.alternateID), IsUnicode = true)]
  public virtual string BarCode
  {
    get => this._BarCode;
    set => this._BarCode = value;
  }

  [PXUIField(DisplayName = "Warehouse")]
  [Site(BqlField = typeof (INLocationStatusByCostCenter.siteID))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXString(IsUnicode = true, IsKey = true)]
  [PXDBCalced(typeof (IsNull<RTrim<INSite.siteCD>, Empty>), typeof (string))]
  public virtual string SiteCD
  {
    get => this._SiteCD;
    set => this._SiteCD = value;
  }

  [Location(typeof (INSiteStatusSelected.siteID), BqlField = typeof (INLocationStatusByCostCenter.locationID))]
  [PXDefault]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXString(IsUnicode = true, IsKey = true)]
  [PXDBCalced(typeof (IsNull<RTrim<INLocation.locationCD>, Empty>), typeof (string))]
  public virtual string LocationCD
  {
    get => this._locationCD;
    set => this._locationCD = value;
  }

  [SubItem(typeof (INSiteStatusSelected.inventoryID), BqlField = typeof (INSubItem.subItemID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXString(IsUnicode = true, IsKey = true)]
  [PXDBCalced(typeof (IsNull<RTrim<INSubItem.subItemCD>, Empty>), typeof (string))]
  public virtual string SubItemCD
  {
    get => this._SubItemCD;
    set => this._SubItemCD = value;
  }

  [PXDefault(typeof (Search<INItemClass.baseUnit, Where<INItemClass.itemClassID, Equal<Current<InventoryItem.itemClassID>>>>))]
  [INUnit]
  public virtual string BaseUnit
  {
    get => this._BaseUnit;
    set => this._BaseUnit = value;
  }

  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Selected")]
  public virtual Decimal? QtySelected
  {
    get => new Decimal?(this._QtySelected.GetValueOrDefault());
    set
    {
      if (value.HasValue)
      {
        Decimal? nullable = value;
        Decimal num = 0M;
        if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
          this._Selected = new bool?(true);
      }
      this._QtySelected = value;
    }
  }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtyOnHand))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. On Hand")]
  public virtual Decimal? QtyOnHand
  {
    get => this._QtyOnHand;
    set => this._QtyOnHand = value;
  }

  [PXDBQuantity(BqlField = typeof (INLocationStatusByCostCenter.qtyAvail))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Available")]
  public virtual Decimal? QtyAvail
  {
    get => this._QtyAvail;
    set => this._QtyAvail = value;
  }

  [PXNote(BqlField = typeof (InventoryItem.noteID))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.IN.INLocationStatusByCostCenter.CostCenterID" />
  [PXDBInt(BqlField = typeof (INLocationStatusByCostCenter.costCenterID))]
  [PXSelector(typeof (INCostCenter.costCenterID))]
  public virtual int? CostCenterID { get; set; }

  /// <summary>Cost Center CD</summary>
  [PXString(IsUnicode = true, IsKey = true)]
  [PXDBCalced(typeof (IsNull<RTrim<INCostCenter.costCenterCD>, Empty>), typeof (string))]
  public virtual string CostCenterCD { get; set; }

  /// <exclude />
  [DBRankTop(BqlField = typeof (InventorySearchIndexAlternateIDTop.rank))]
  public virtual int? Rank { get; set; }

  /// <exclude />
  [PXString]
  [DBCombinedSearchString(typeof (InventorySearchIndexAlternateIDTop.contentIDDesc), BqlTable = typeof (INSiteStatusSelected))]
  public virtual string CombinedSearchString { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INSiteStatusSelected.selected>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteStatusSelected.inventoryID>
  {
  }

  public abstract class inventoryCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSiteStatusSelected.inventoryCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSiteStatusSelected.descr>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteStatusSelected.itemClassID>
  {
  }

  public abstract class itemClassCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSiteStatusSelected.itemClassCD>
  {
  }

  public abstract class itemClassDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSiteStatusSelected.itemClassDescription>
  {
  }

  public abstract class priceClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSiteStatusSelected.priceClassID>
  {
  }

  public abstract class priceClassDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSiteStatusSelected.priceClassDescription>
  {
  }

  public abstract class barCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSiteStatusSelected.barCode>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteStatusSelected.siteID>
  {
  }

  public abstract class siteCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSiteStatusSelected.siteCD>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteStatusSelected.locationID>
  {
  }

  public abstract class locationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSiteStatusSelected.locationCD>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteStatusSelected.subItemID>
  {
  }

  public abstract class subItemCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSiteStatusSelected.subItemCD>
  {
  }

  public abstract class baseUnit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSiteStatusSelected.baseUnit>
  {
  }

  public abstract class qtySelected : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusSelected.qtySelected>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusSelected.qtyOnHand>
  {
  }

  public abstract class qtyAvail : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INSiteStatusSelected.qtyAvail>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INSiteStatusSelected.noteID>
  {
  }

  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteStatusSelected.costCenterID>
  {
  }

  public abstract class costCenterCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSiteStatusSelected.costCenterCD>
  {
  }

  public abstract class rank : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteStatusSelected.rank>
  {
  }

  public abstract class combinedSearchString : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSiteStatusSelected.combinedSearchString>
  {
  }
}
