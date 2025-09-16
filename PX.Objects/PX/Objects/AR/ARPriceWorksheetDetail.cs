// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARPriceWorksheetDetail
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
/// A pending sales price record belonging to a
/// <see cref="T:PX.Objects.AR.ARPriceWorksheet">sales price worksheet</see>.
/// The actual <see cref="T:PX.Objects.AR.ARSalesPrice">sales prices</see> are updated
/// upon the worksheet release. The entities of this type can be edited
/// on the Sales Price Worksheets (AR202010) form, which corresponds to
/// the <see cref="T:PX.Objects.AR.ARPriceWorksheetMaint" /> graph.
/// </summary>
[PXCacheName("AR Price Worksheet Detail")]
[PXProjection(typeof (Select2<ARPriceWorksheetDetail, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<ARPriceWorksheetDetail.inventoryID>>, LeftJoin<Customer, On<ARPriceWorksheetDetail.priceType, Equal<PriceTypeList.customer>, And<Customer.bAccountID, Equal<ARPriceWorksheetDetail.customerID>>>>>>), new Type[] {typeof (ARPriceWorksheetDetail)})]
[Serializable]
public class ARPriceWorksheetDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _RefNbr;
  protected int? _LineID;
  protected string _PriceType;
  protected string _PriceCode;
  protected string _CustPriceClassID;
  protected int? _CustomerID;
  protected int? _InventoryID;
  protected string _AlternateID;
  protected int? _SubItemID;
  protected string _Description;
  protected string _UOM;
  protected Decimal? _BreakQty;
  protected Decimal? _CurrentPrice;
  protected Decimal? _PendingPrice;
  protected string _CuryID;
  protected int? _SiteID;
  protected string _TaxID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDBDefault(typeof (ARPriceWorksheet.refNbr))]
  [PXUIField]
  [PXParent(typeof (Select<ARPriceWorksheet, Where<ARPriceWorksheet.refNbr, Equal<Current<ARPriceWorksheetDetail.refNbr>>>>))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBIdentity(IsKey = true)]
  public virtual int? LineID
  {
    get => this._LineID;
    set => this._LineID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("P")]
  [PriceTypeList.List]
  [PXUIField]
  public virtual string PriceType
  {
    get => this._PriceType;
    set => this._PriceType = value;
  }

  [PXString(30, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARPriceWorksheetDetail.priceType, IBqlString>.IsEqual<PriceTypeList.customer>>, RTrim<Customer.acctCD>, Case<Where<BqlOperand<ARPriceWorksheetDetail.priceType, IBqlString>.IsEqual<PriceTypeList.customerPriceClass>>, RTrim<ARPriceWorksheetDetail.custPriceClassID>>>, Null>), typeof (string))]
  [PXUIField]
  [ARPriceCodeSelector(typeof (ARPriceWorksheetDetail.priceType))]
  public virtual string PriceCode
  {
    get => this._PriceCode;
    set => this._PriceCode = value;
  }

  [PXDBString(10, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [CustomerPriceClass]
  public virtual string CustPriceClassID
  {
    get => this._CustPriceClassID;
    set => this._CustPriceClassID = value;
  }

  [Customer]
  [PXParent(typeof (Select<Customer, Where<Customer.bAccountID, Equal<Current<ARPriceWorksheetDetail.customerID>>>>))]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXDBString(30, IsUnicode = true, BqlField = typeof (Customer.acctCD))]
  [PXFormula(typeof (Selector<ARPriceWorksheetDetail.customerID, Customer.acctCD>))]
  public virtual string CustomerCD { get; set; }

  [InventoryByAlternateID(typeof (ARPriceWorksheetDetail.customerID), typeof (ARPriceWorksheetDetail.alternateID), typeof (INAlternateType.cPN), typeof (ARPriceWorksheetDetail.restrictInventoryByAlternateID))]
  [PXParent(typeof (Select<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<ARPriceWorksheetDetail.inventoryID>>>>))]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBString(IsUnicode = true, BqlField = typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  [PXFormula(typeof (Selector<ARPriceWorksheetDetail.inventoryID, PX.Objects.IN.InventoryItem.inventoryCD>))]
  public virtual string InventoryCD { get; set; }

  [PriceWorksheetAlternateItem]
  public virtual string AlternateID
  {
    get => this._AlternateID;
    set => this._AlternateID = value;
  }

  [SubItem(typeof (ARPriceWorksheetDetail.inventoryID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDBLocalizableString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (PX.Objects.IN.InventoryItem.descr), IsProjection = true)]
  [PXFormula(typeof (Selector<ARPriceWorksheetDetail.inventoryID, PX.Objects.IN.InventoryItem.descr>))]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDefault]
  [INUnit(typeof (ARPriceWorksheetDetail.inventoryID))]
  [PXFormula(typeof (Switch<Case<Where<BqlOperand<ARPriceWorksheetDetail.restrictInventoryByAlternateID, IBqlBool>.IsNotEqual<True>>, Selector<ARPriceWorksheetDetail.inventoryID, PX.Objects.IN.InventoryItem.salesUnit>>, ARPriceWorksheetDetail.uOM>))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(MinValue = 0.0)]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BreakQty
  {
    get => this._BreakQty;
    set => this._BreakQty = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBPriceCost]
  [PXUIField]
  public virtual Decimal? CurrentPrice
  {
    get => this._CurrentPrice;
    set => this._CurrentPrice = value;
  }

  [PXDBPriceCost(true)]
  [PXUIField]
  public virtual Decimal? PendingPrice
  {
    get => this._PendingPrice;
    set => this._PendingPrice = value;
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

  /// <summary>
  /// Value to be transferred to the <see cref="P:PX.Objects.AR.ARSalesPrice.SkipLineDiscounts" /> on release of worksheet.
  /// </summary>
  [PXDBBool]
  [PXDefault(typeof (ARPriceWorksheet.skipLineDiscounts))]
  [PXUIField(DisplayName = "Ignore Automatic Line Discounts")]
  public virtual bool? SkipLineDiscounts { get; set; }

  [NullableSite]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
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

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.TX.TaxCategory" /> associated with the item.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.TX.TaxCategory.TaxCategoryID" /> field.
  /// </value>
  [PXString(15, IsUnicode = true)]
  [PXUIField]
  public virtual string TaxCategoryID { get; set; }

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

  [PXBool]
  [PXDefault(false)]
  public virtual bool? RestrictInventoryByAlternateID { get; set; }

  public class PK : 
    PrimaryKeyOf<ARPriceWorksheetDetail>.By<ARPriceWorksheetDetail.refNbr, ARPriceWorksheetDetail.lineID>
  {
    public static ARPriceWorksheetDetail Find(
      PXGraph graph,
      string refNbr,
      int? lineID,
      PKFindOptions options = 0)
    {
      return (ARPriceWorksheetDetail) PrimaryKeyOf<ARPriceWorksheetDetail>.By<ARPriceWorksheetDetail.refNbr, ARPriceWorksheetDetail.lineID>.FindBy(graph, (object) refNbr, (object) lineID, options);
    }
  }

  public static class FK
  {
    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARPriceWorksheetDetail>.By<ARPriceWorksheetDetail.customerID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<ARPriceWorksheetDetail>.By<ARPriceWorksheetDetail.subItemID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<ARPriceWorksheetDetail>.By<ARPriceWorksheetDetail.inventoryID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<ARPriceWorksheetDetail>.By<ARPriceWorksheetDetail.curyID>
    {
    }

    public class Tax : 
      PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<ARPriceWorksheetDetail>.By<ARPriceWorksheetDetail.taxID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<ARPriceWorksheetDetail>.By<ARPriceWorksheetDetail.siteID>
    {
    }
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPriceWorksheetDetail.refNbr>
  {
  }

  public abstract class lineID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPriceWorksheetDetail.lineID>
  {
  }

  public abstract class priceType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPriceWorksheetDetail.priceType>
  {
  }

  public abstract class priceCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPriceWorksheetDetail.priceCode>
  {
  }

  public abstract class custPriceClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPriceWorksheetDetail.custPriceClassID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPriceWorksheetDetail.customerID>
  {
  }

  public abstract class customerCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPriceWorksheetDetail.customerCD>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPriceWorksheetDetail.inventoryID>
  {
  }

  public abstract class inventoryCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPriceWorksheetDetail.inventoryCD>
  {
  }

  public abstract class alternateID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPriceWorksheetDetail.alternateID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPriceWorksheetDetail.subItemID>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPriceWorksheetDetail.description>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPriceWorksheetDetail.uOM>
  {
  }

  public abstract class breakQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARPriceWorksheetDetail.breakQty>
  {
  }

  public abstract class currentPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARPriceWorksheetDetail.currentPrice>
  {
  }

  public abstract class pendingPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARPriceWorksheetDetail.pendingPrice>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPriceWorksheetDetail.curyID>
  {
  }

  public abstract class skipLineDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARPriceWorksheetDetail.skipLineDiscounts>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARPriceWorksheetDetail.siteID>
  {
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2021 R2.")]
  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARPriceWorksheetDetail.taxID>
  {
  }

  public abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPriceWorksheetDetail.taxCategoryID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARPriceWorksheetDetail.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARPriceWorksheetDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPriceWorksheetDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARPriceWorksheetDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARPriceWorksheetDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARPriceWorksheetDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARPriceWorksheetDetail.lastModifiedDateTime>
  {
  }

  public abstract class restrictInventoryByAlternateID : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARPriceWorksheetDetail.restrictInventoryByAlternateID>
  {
  }
}
