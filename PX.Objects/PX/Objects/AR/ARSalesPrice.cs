// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSalesPrice
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// An accounts receivable sales price. Depending on the
/// specified <see cref="P:PX.Objects.AR.ARSalesPrice.PriceType">price type</see>, a sales price record
/// can define a base price, a price for a given <see cref="T:PX.Objects.AR.Customer">
/// customer</see>, or a price for a certain <see cref="T:PX.Objects.AR.ARPriceClass">customer
/// price class</see>. The entities of this type can be edited on the Sales Prices
/// (AR202000) form, which corresponds to the <see cref="T:PX.Objects.AR.ARSalesPriceMaint" /> graph.
/// </summary>
[PXCacheName("AR Sales Price")]
[ARSalesPriceProjection]
[Serializable]
public class ARSalesPrice : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _RecordID;
  protected 
  #nullable disable
  string _PriceType;
  protected string _PriceCode;
  protected string _CustPriceClassID;
  protected int? _CustomerID;
  protected string _Description;
  protected int? _InventoryID;
  protected string _AlternateID;
  protected string _CuryID;
  protected string _UOM;
  protected bool? _IsPromotionalPrice;
  protected DateTime? _EffectiveDate;
  protected Decimal? _SalesPrice;
  protected string _TaxID;
  protected Decimal? _BreakQty;
  protected DateTime? _ExpirationDate;
  protected int? _SiteID;
  protected Guid? _NoteID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID
  {
    get => this._RecordID;
    set => this._RecordID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("P")]
  [PriceTypes.List]
  [PXUIField]
  public virtual string PriceType
  {
    get => this._PriceType;
    set => this._PriceType = value;
  }

  [PXString(30, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [ARPriceCodeSelector(typeof (ARSalesPrice.priceType), SuppressReadDeletedSupport = true)]
  public virtual string PriceCode
  {
    get => this._PriceCode;
    set => this._PriceCode = value;
  }

  [PXDBString(10, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [PXUIField]
  [CustomerPriceClass]
  public virtual string CustPriceClassID
  {
    get => this._CustPriceClassID;
    set => this._CustPriceClassID = value;
  }

  [Customer]
  [PXParent(typeof (Select<Customer, Where<Customer.bAccountID, Equal<Current<ARSalesPrice.customerID>>>>))]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXString(30, IsUnicode = true)]
  public virtual string CustomerCD { get; set; }

  [PXString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.TX.TaxCategory" /> associated with the item.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.TX.TaxCategory.TaxCategoryID" /> field.
  /// </value>
  [PXString(15, IsUnicode = true)]
  [PXUIField]
  public virtual string TaxCategoryID { get; set; }

  [ARCrossItem(BAccountField = typeof (ARSalesPrice.customerID), WarningOnNonUniqueSubstitution = true, AllowTemplateItems = true)]
  [PXParent(typeof (Select<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<ARSalesPrice.inventoryID>>>>))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXString(IsUnicode = true)]
  public virtual string InventoryCD { get; set; }

  [PXUIField]
  [PXDBString(50, IsUnicode = true, InputMask = "")]
  public virtual string AlternateID
  {
    get => this._AlternateID;
    set => this._AlternateID = value;
  }

  [PXDBString(5)]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID), CacheGlobal = true)]
  [PXUIField(DisplayName = "Currency")]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDefault]
  [INUnit(typeof (ARSalesPrice.inventoryID))]
  [PXFormula(typeof (Selector<ARSalesPrice.inventoryID, PX.Objects.IN.InventoryItem.salesUnit>))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Promotion")]
  public virtual bool? IsPromotionalPrice
  {
    get => this._IsPromotionalPrice;
    set => this._IsPromotionalPrice = value;
  }

  /// <summary>
  /// Determines whether price is excluded from line discount calculation.
  /// </summary>
  [PXDBBool]
  [PXDefault]
  [PXFormula(typeof (ARSalesPrice.isPromotionalPrice))]
  [PXUIField(DisplayName = "Ignore Automatic Line Discounts")]
  public virtual bool? SkipLineDiscounts { get; set; }

  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXDBDate]
  [PXUIField]
  public virtual DateTime? EffectiveDate
  {
    get => this._EffectiveDate;
    set => this._EffectiveDate = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBPriceCost]
  [PXUIField]
  public virtual Decimal? SalesPrice
  {
    get => this._SalesPrice;
    set => this._SalesPrice = value;
  }

  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr))]
  [PXDBString(60)]
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2021 R2.")]
  public virtual string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PriceTaxCalculationMode.List]
  [PXDefault("U")]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode { get; set; }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BreakQty
  {
    get => this._BreakQty;
    set => this._BreakQty = value;
  }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? ExpirationDate
  {
    get => this._ExpirationDate;
    set => this._ExpirationDate = value;
  }

  [NullableSite]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IsFairValue { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? IsProrated { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Discountable { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXString(2, IsFixed = true)]
  public virtual string ItemStatus { get; set; }

  [PXInt]
  public virtual int? ItemClassID { get; set; }

  [PXString(30, IsUnicode = true)]
  public virtual string PriceClassID { get; set; }

  [PXInt]
  public virtual int? PriceWorkgroupID { get; set; }

  [PXInt]
  public virtual int? PriceManagerID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  /// <exclude />
  public class PK : PrimaryKeyOf<ARSalesPrice>.By<ARSalesPrice.recordID>
  {
    public static ARSalesPrice Find(PXGraph graph, int? recordID, PKFindOptions options = 0)
    {
      return (ARSalesPrice) PrimaryKeyOf<ARSalesPrice>.By<ARSalesPrice.recordID>.FindBy(graph, (object) recordID, options);
    }
  }

  public static class FK
  {
    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARSalesPrice>.By<ARSalesPrice.customerID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<ARSalesPrice>.By<ARSalesPrice.inventoryID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<ARSalesPrice>.By<ARSalesPrice.curyID>
    {
    }

    public class Tax : 
      PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<ARSalesPrice>.By<ARSalesPrice.taxID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<ARSalesPrice>.By<ARSalesPrice.siteID>
    {
    }
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSalesPrice.recordID>
  {
  }

  public abstract class priceType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSalesPrice.priceType>
  {
  }

  public abstract class priceCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSalesPrice.priceCode>
  {
  }

  public abstract class custPriceClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSalesPrice.custPriceClassID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSalesPrice.customerID>
  {
  }

  public abstract class customerCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSalesPrice.customerCD>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSalesPrice.description>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSalesPrice.taxCategoryID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSalesPrice.inventoryID>
  {
  }

  public abstract class inventoryCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSalesPrice.inventoryCD>
  {
  }

  public abstract class alternateID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSalesPrice.alternateID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSalesPrice.curyID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSalesPrice.uOM>
  {
  }

  public abstract class isPromotionalPrice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARSalesPrice.isPromotionalPrice>
  {
  }

  public abstract class skipLineDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARSalesPrice.skipLineDiscounts>
  {
  }

  public abstract class effectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARSalesPrice.effectiveDate>
  {
  }

  public abstract class salesPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARSalesPrice.salesPrice>
  {
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2021 R2.")]
  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSalesPrice.taxID>
  {
  }

  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSalesPrice.taxCalcMode>
  {
  }

  public abstract class breakQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARSalesPrice.breakQty>
  {
  }

  public abstract class expirationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARSalesPrice.expirationDate>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSalesPrice.siteID>
  {
  }

  public abstract class isFairValue : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARSalesPrice.isFairValue>
  {
  }

  public abstract class isProrated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARSalesPrice.isProrated>
  {
  }

  public abstract class discountable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARSalesPrice.discountable>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARSalesPrice.noteID>
  {
  }

  public abstract class itemStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSalesPrice.itemStatus>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSalesPrice.itemClassID>
  {
  }

  public abstract class priceClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSalesPrice.priceClassID>
  {
  }

  public abstract class priceWorkgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSalesPrice.priceWorkgroupID>
  {
  }

  public abstract class priceManagerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSalesPrice.priceManagerID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARSalesPrice.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARSalesPrice.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSalesPrice.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARSalesPrice.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARSalesPrice.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARSalesPrice.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARSalesPrice.lastModifiedDateTime>
  {
  }
}
