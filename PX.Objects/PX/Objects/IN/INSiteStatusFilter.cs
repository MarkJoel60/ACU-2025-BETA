// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSiteStatusFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.Maintenance;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class INSiteStatusFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _SiteID;
  protected int? _LocationID;
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _SubItem;
  protected string _BarCode;
  protected string _Inventory;
  protected bool? _OnlyAvailable;
  protected string _ItemClass;

  [PXUIField(DisplayName = "Warehouse")]
  [Site]
  [InterBranchRestrictor(typeof (Where<SameOrganizationBranch<INSite.branchID, Current<INRegister.branchID>>>))]
  [PXDefault(typeof (INRegister.siteID))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [Location(typeof (INSiteStatusFilter.siteID), KeepEntry = false, DescriptionField = typeof (INLocation.descr), DisplayName = "Location")]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PX.Objects.IN.Inventory]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItemRawExt(typeof (INSiteStatusFilter.inventoryID), DisplayName = "Subitem")]
  public virtual string SubItem
  {
    get => this._SubItem;
    set => this._SubItem = value;
  }

  [PXDBString(30, IsUnicode = true)]
  public virtual string SubItemCDWildcard
  {
    get
    {
      return this._SubItem != null ? SubCDUtils.CreateSubCDWildcard(this._SubItem, "INSUBITEM") : (string) null;
    }
  }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Barcode")]
  public virtual string BarCode
  {
    get => this._BarCode;
    set => this._BarCode = value;
  }

  [PXDBString(30, IsUnicode = true)]
  public virtual string BarCodeWildcard
  {
    get => this._BarCode != null ? $"%{this._BarCode}%" : (string) null;
  }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Inventory")]
  public virtual string Inventory
  {
    get => this._Inventory;
    set => this._Inventory = value;
  }

  [PXDBString(30, IsUnicode = true)]
  public virtual string Inventory_Wildcard
  {
    get
    {
      string wildcardAnything = PXDatabase.Provider.SqlDialect.WildcardAnything;
      return this._Inventory != null ? wildcardAnything + this._Inventory + wildcardAnything : (string) null;
    }
  }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Show Available Items Only")]
  public virtual bool? OnlyAvailable
  {
    get => this._OnlyAvailable;
    set => this._OnlyAvailable = value;
  }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField]
  [PXDimensionSelector("INITEMCLASS", typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr), ValidComboRequired = true)]
  public virtual string ItemClass
  {
    get => this._ItemClass;
    set => this._ItemClass = value;
  }

  [PXString(IsUnicode = true)]
  [PXUIField]
  [PXDimension("INITEMCLASS", ParentSelect = typeof (Select<INItemClass>), ParentValueField = typeof (INItemClass.itemClassCD), AutoNumbering = false)]
  public virtual string ItemClassCDWildcard
  {
    get => DimensionTree<INItemClass.dimension>.MakeWildcard(this.ItemClass);
    set
    {
    }
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteStatusFilter.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteStatusFilter.locationID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteStatusFilter.inventoryID>
  {
  }

  public abstract class subItem : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSiteStatusFilter.subItem>
  {
  }

  public abstract class subItemCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSiteStatusFilter.subItemCDWildcard>
  {
  }

  public abstract class barCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSiteStatusFilter.barCode>
  {
  }

  public abstract class barCodeWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSiteStatusFilter.barCodeWildcard>
  {
  }

  public abstract class inventory : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSiteStatusFilter.inventory>
  {
  }

  public abstract class inventory_Wildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSiteStatusFilter.inventory_Wildcard>
  {
  }

  public abstract class onlyAvailable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INSiteStatusFilter.onlyAvailable>
  {
  }

  public abstract class itemClass : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INSiteStatusFilter.itemClass>
  {
  }

  public abstract class itemClassCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSiteStatusFilter.itemClassCDWildcard>
  {
  }
}
