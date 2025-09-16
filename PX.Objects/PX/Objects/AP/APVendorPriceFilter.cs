// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APVendorPriceFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.Maintenance;
using PX.Objects.CR;
using PX.Objects.IN;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.AP;

[Serializable]
public class APVendorPriceFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _VendorID;
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _AlternateID;
  private System.DateTime? _EffectiveAsOfDate;
  protected string _ItemClassCD;
  protected int? _SiteID;
  protected int? _OwnerID;
  protected bool? _MyOwner;
  protected int? _WorkGroupID;
  protected bool? _MyWorkGroup;

  [PXUIField(DisplayName = "Vendor")]
  [VendorNonEmployeeActive]
  [PXParent(typeof (Select<Vendor, Where<Vendor.bAccountID, Equal<Current<APVendorPriceFilter.vendorID>>>>))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [InventoryIncludingTemplates(DisplayName = "Inventory ID")]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXString(50, IsUnicode = true, InputMask = "")]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Alternate ID")]
  public virtual string AlternateID
  {
    get => this._AlternateID;
    set => this._AlternateID = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Effective As Of", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual System.DateTime? EffectiveAsOfDate
  {
    get => this._EffectiveAsOfDate;
    set => this._EffectiveAsOfDate = value;
  }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Item Class", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDimensionSelector("INITEMCLASS", typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr), ValidComboRequired = true)]
  public virtual string ItemClassCD
  {
    get => this._ItemClassCD;
    set => this._ItemClassCD = value;
  }

  [PXString(IsUnicode = true)]
  [PXUIField(Visible = false, Visibility = PXUIVisibility.Invisible)]
  [PXDimension("INITEMCLASS", ParentSelect = typeof (Select<INItemClass>), ParentValueField = typeof (INItemClass.itemClassCD), AutoNumbering = false)]
  public virtual string ItemClassCDWildcard
  {
    get => DimensionTree<INItemClass.dimension>.MakeWildcard(this.ItemClassCD);
    set
    {
    }
  }

  [NullableSite]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBInt]
  [CRCurrentOwnerID]
  public virtual int? CurrentOwnerID { get; set; }

  [SubordinateOwner(DisplayName = "Product Manager")]
  public virtual int? OwnerID
  {
    get => !this._MyOwner.GetValueOrDefault() ? this._OwnerID : this.CurrentOwnerID;
    set => this._OwnerID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Me")]
  public virtual bool? MyOwner
  {
    get => this._MyOwner;
    set => this._MyOwner = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Product Workgroup")]
  [PXSelector(typeof (Search<EPCompanyTree.workGroupID, Where<EPCompanyTree.workGroupID, IsWorkgroupOrSubgroupOfContact<Current<AccessInfo.contactID>>>>), SubstituteKey = typeof (EPCompanyTree.description))]
  public virtual int? WorkGroupID
  {
    get => !this._MyWorkGroup.GetValueOrDefault() ? this._WorkGroupID : new int?();
    set => this._WorkGroupID = value;
  }

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField(DisplayName = "My", Visibility = PXUIVisibility.Visible)]
  public virtual bool? MyWorkGroup
  {
    get => this._MyWorkGroup;
    set => this._MyWorkGroup = value;
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APVendorPriceFilter.vendorID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APVendorPriceFilter.inventoryID>
  {
  }

  public abstract class alternateID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APVendorPriceFilter.alternateID>
  {
  }

  public abstract class effectiveAsOfDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APVendorPriceFilter.effectiveAsOfDate>
  {
  }

  public abstract class itemClassCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APVendorPriceFilter.itemClassCD>
  {
  }

  public abstract class itemClassCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APVendorPriceFilter.itemClassCDWildcard>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APVendorPriceFilter.siteID>
  {
  }

  public abstract class currentOwnerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APVendorPriceFilter.currentOwnerID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APVendorPriceFilter.ownerID>
  {
  }

  public abstract class myOwner : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APVendorPriceFilter.myOwner>
  {
  }

  public abstract class workGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APVendorPriceFilter.workGroupID>
  {
  }

  public abstract class myWorkGroup : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APVendorPriceFilter.myWorkGroup>
  {
  }
}
