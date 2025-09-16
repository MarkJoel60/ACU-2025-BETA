// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSalesPriceFilter
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
namespace PX.Objects.AR;

[Serializable]
public class ARSalesPriceFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _PriceType;
  protected string _PriceCode;
  protected int? _InventoryID;
  protected string _AlternateID;
  private DateTime? _EffectiveAsOfDate;
  protected string _ItemClassCD;
  protected string _InventoryPriceClassID;
  protected int? _SiteID;
  protected int? _OwnerID;
  protected bool? _MyOwner;
  protected int? _WorkGroupID;
  protected bool? _MyWorkGroup;

  [PXDBString(1, IsFixed = true)]
  [PXDefault("A")]
  [PriceTypes.ListWithAll]
  [PXUIField]
  public virtual string PriceType
  {
    get => this._PriceType;
    set => this._PriceType = value;
  }

  [PXDBString(30, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [ARPriceCodeSelector(typeof (ARSalesPriceFilter.priceType), SuppressReadDeletedSupport = true)]
  public virtual string PriceCode
  {
    get => this._PriceCode;
    set => this._PriceCode = value;
  }

  [InventoryIncludingTemplates(DisplayName = "Inventory ID")]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXString(50, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Alternate ID")]
  public virtual string AlternateID
  {
    get => this._AlternateID;
    set => this._AlternateID = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? EffectiveAsOfDate
  {
    get => this._EffectiveAsOfDate;
    set => this._EffectiveAsOfDate = value;
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

  [PXDBString(10)]
  [PXSelector(typeof (INPriceClass.priceClassID))]
  [PXUIField]
  public virtual string InventoryPriceClassID
  {
    get => this._InventoryPriceClassID;
    set => this._InventoryPriceClassID = value;
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

  [SubordinateOwner(DisplayName = "Price Manager")]
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
  [PXUIField(DisplayName = "Price Workgroup")]
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

  [PXString(1, IsFixed = true)]
  [PriceTaxCalculationMode.ListWithAll]
  [PXUnboundDefault("A")]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode { get; set; }

  public abstract class priceType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSalesPriceFilter.priceType>
  {
  }

  public abstract class priceCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSalesPriceFilter.priceCode>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSalesPriceFilter.inventoryID>
  {
  }

  public abstract class alternateID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSalesPriceFilter.alternateID>
  {
  }

  public abstract class effectiveAsOfDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARSalesPriceFilter.effectiveAsOfDate>
  {
  }

  public abstract class itemClassCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSalesPriceFilter.itemClassCD>
  {
  }

  public abstract class itemClassCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSalesPriceFilter.itemClassCDWildcard>
  {
  }

  public abstract class inventoryPriceClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSalesPriceFilter.inventoryPriceClassID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSalesPriceFilter.siteID>
  {
  }

  public abstract class currentOwnerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARSalesPriceFilter.currentOwnerID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSalesPriceFilter.ownerID>
  {
  }

  public abstract class myOwner : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARSalesPriceFilter.myOwner>
  {
  }

  public abstract class workGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSalesPriceFilter.workGroupID>
  {
  }

  public abstract class myWorkGroup : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARSalesPriceFilter.myWorkGroup>
  {
  }

  public abstract class filterSet : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARSalesPriceFilter.filterSet>
  {
  }

  public abstract class taxCalcMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSalesPriceFilter.taxCalcMode>
  {
  }
}
