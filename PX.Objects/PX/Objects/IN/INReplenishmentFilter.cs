// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INReplenishmentFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.Maintenance;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.GL;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class INReplenishmentFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _MyOwner;
  protected int? _OwnerID;
  protected int? _WorkGroupID;
  protected bool? _MyWorkGroup;
  protected int? _ReplenishmentSiteID;
  protected DateTime? _PurchaseDate;
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _SubItemCD;
  protected int? _PreferredVendorID;
  protected string _Descr;
  protected bool? _OnlySuggested;
  protected string _ItemClassCD;

  [PXDBInt]
  [CRCurrentOwnerID]
  public virtual int? CurrentOwnerID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Me")]
  public virtual bool? MyOwner
  {
    get => this._MyOwner;
    set => this._MyOwner = value;
  }

  [SubordinateOwner(DisplayName = "Product Manager")]
  public virtual int? OwnerID
  {
    get => !this._MyOwner.GetValueOrDefault() ? this._OwnerID : this.CurrentOwnerID;
    set => this._OwnerID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Product  Workgroup")]
  [PXSelector(typeof (Search<EPCompanyTree.workGroupID, Where<EPCompanyTree.workGroupID, IsWorkgroupOrSubgroupOfContact<Current<AccessInfo.contactID>>>>), SubstituteKey = typeof (EPCompanyTree.description))]
  public virtual int? WorkGroupID
  {
    get => !this._MyWorkGroup.GetValueOrDefault() ? this._WorkGroupID : new int?();
    set => this._WorkGroupID = value;
  }

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField]
  public virtual bool? MyWorkGroup
  {
    get => this._MyWorkGroup;
    set => this._MyWorkGroup = value;
  }

  [PXDefault(false)]
  [PXDBBool]
  public virtual bool? FilterSet
  {
    get
    {
      return new bool?(this.OwnerID.HasValue || this.WorkGroupID.HasValue || this.MyWorkGroup.GetValueOrDefault());
    }
  }

  [Site(DisplayName = "Warehouse")]
  [PXDefault]
  public virtual int? ReplenishmentSiteID
  {
    get => this._ReplenishmentSiteID;
    set => this._ReplenishmentSiteID = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Purchase Date")]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? PurchaseDate
  {
    get => this._PurchaseDate;
    set => this._PurchaseDate = value;
  }

  [StockItem(Filterable = true)]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItemRawExt(typeof (INReplenishmentFilter.inventoryID), DisplayName = "Subitem")]
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

  [VendorNonEmployeeActive]
  public virtual int? PreferredVendorID
  {
    get => this._PreferredVendorID;
    set => this._PreferredVendorID = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  public virtual string DescrWildcard => this._Descr != null ? $"%{this._Descr}%" : (string) null;

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Only Suggested Items")]
  public virtual bool? OnlySuggested
  {
    get => this._OnlySuggested;
    set => this._OnlySuggested = value;
  }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField]
  [PXDimensionSelector("INITEMCLASS", typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr), ValidComboRequired = true)]
  public virtual string ItemClassCD
  {
    get => this._ItemClassCD;
    set => this._ItemClassCD = value;
  }

  [PXString(IsUnicode = true)]
  [PXUIField]
  [PXDimension("INITEMCLASS", ParentSelect = typeof (Select<INItemClass>), ParentValueField = typeof (INItemClass.itemClassCD), AutoNumbering = false)]
  public virtual string ItemClassCDWildcard
  {
    get => DimensionTree<INItemClass.dimension>.MakeWildcard(this.ItemClassCD);
    set
    {
    }
  }

  public abstract class currentOwnerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INReplenishmentFilter.currentOwnerID>
  {
  }

  public abstract class myOwner : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INReplenishmentFilter.myOwner>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INReplenishmentFilter.ownerID>
  {
  }

  public abstract class workGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INReplenishmentFilter.workGroupID>
  {
  }

  public abstract class myWorkGroup : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INReplenishmentFilter.myWorkGroup>
  {
  }

  public abstract class filterSet : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INReplenishmentFilter.filterSet>
  {
  }

  public abstract class replenishmentSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INReplenishmentFilter.replenishmentSiteID>
  {
  }

  public abstract class purchaseDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INReplenishmentFilter.purchaseDate>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INReplenishmentFilter.inventoryID>
  {
  }

  public abstract class subItemCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentFilter.subItemCD>
  {
  }

  public abstract class subItemCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentFilter.subItemCDWildcard>
  {
  }

  public abstract class preferredVendorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INReplenishmentFilter.preferredVendorID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INReplenishmentFilter.descr>
  {
  }

  public abstract class descrWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentFilter.descrWildcard>
  {
  }

  public abstract class onlySuggested : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INReplenishmentFilter.onlySuggested>
  {
  }

  public abstract class itemClassCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentFilter.itemClassCD>
  {
  }

  public abstract class itemClassCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentFilter.itemClassCDWildcard>
  {
  }
}
