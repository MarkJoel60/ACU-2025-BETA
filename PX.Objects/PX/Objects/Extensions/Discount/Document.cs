// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.Discount.Document
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.Extensions.Discount;

/// <summary>A mapped cache extension that represents a document that supports discounts.</summary>
public class Document : PXMappedCacheExtension
{
  /// <exclude />
  protected int? _BranchID;
  /// <exclude />
  protected int? _CustomerID;
  /// <exclude />
  protected 
  #nullable disable
  string _CuryID;
  /// <exclude />
  protected long? _CuryInfoID;
  /// <exclude />
  protected Decimal? _CuryOrigDiscAmt;
  /// <exclude />
  protected Decimal? _OrigDiscAmt;
  /// <exclude />
  protected Decimal? _CuryDiscTaken;
  /// <exclude />
  protected Decimal? _DiscTaken;
  /// <exclude />
  protected Decimal? _CuryDiscBal;
  /// <exclude />
  protected Decimal? _DiscBal;
  /// <exclude />
  protected Decimal? _DiscTot;
  /// <exclude />
  protected Decimal? _CuryDiscTot;
  /// <exclude />
  protected Decimal? _DocDisc;
  /// <exclude />
  protected Decimal? _CuryDiscountedDocTotal;
  /// <exclude />
  protected Decimal? _DiscountedDocTotal;
  /// <exclude />
  protected Decimal? _CuryDiscountedTaxableTotal;
  /// <exclude />
  protected Decimal? _DiscountedTaxableTotal;
  /// <exclude />
  protected Decimal? _CuryDiscountedPrice;
  /// <exclude />
  protected Decimal? _DiscountedPrice;
  /// <exclude />
  protected int? _LocationID;
  /// <exclude />
  protected System.DateTime? _DocumentDate;
  /// <exclude />
  protected Decimal? _CuryLineTotal;
  /// <exclude />
  protected Decimal? _LineTotal;
  /// <exclude />
  protected Decimal? _CuryMiscTot;
  /// <exclude />
  protected int? _BAccountID;

  /// <summary>The identifier of the branch associated with the document.</summary>
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>The identifier of a customer account to whom this document belongs.</summary>
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  /// <summary>The identifier of the currency of the document.</summary>
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  /// <summary>The identifier of the <see cref="T:PX.Objects.CM.Extensions.CurrencyInfo">CurrencyInfo</see> object associated with the document.</summary>
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  /// <summary>The cash discount allowed for the document in the currency of the document (<see cref="P:PX.Objects.Extensions.Discount.Document.CuryID" />).</summary>
  public virtual Decimal? CuryOrigDiscAmt
  {
    get => this._CuryOrigDiscAmt;
    set => this._CuryOrigDiscAmt = value;
  }

  /// <summary>The cash discount allowed for the document in the currency of the company.</summary>
  public virtual Decimal? OrigDiscAmt
  {
    get => this._OrigDiscAmt;
    set => this._OrigDiscAmt = value;
  }

  /// <summary>The cash discount applied to the document, in the currency of the document (<see cref="P:PX.Objects.Extensions.Discount.Document.CuryID" />).</summary>
  public virtual Decimal? CuryDiscTaken
  {
    get => this._CuryDiscTaken;
    set => this._CuryDiscTaken = value;
  }

  /// <summary>The cash discount actually applied to the document, in the base currency of the company.</summary>
  public virtual Decimal? DiscTaken
  {
    get => this._DiscTaken;
    set => this._DiscTaken = value;
  }

  /// <summary>The cash discount balance of the document, in the currency of the document (<see cref="P:PX.Objects.Extensions.Discount.Document.CuryID" />).</summary>
  public virtual Decimal? CuryDiscBal
  {
    get => this._CuryDiscBal;
    set => this._CuryDiscBal = value;
  }

  /// <summary>The cash discount balance of the document, in the base currency of the company.</summary>
  public virtual Decimal? DiscBal
  {
    get => this._DiscBal;
    set => this._DiscBal = value;
  }

  /// <summary>The total group and document discount for the document, in the base currency of the company.</summary>
  public virtual Decimal? DiscTot
  {
    get => this._DiscTot;
    set => this._DiscTot = value;
  }

  /// <summary>The total group and document discount for the document. The discount is in the currency of the document (<see cref="P:PX.Objects.Extensions.Discount.Document.CuryID" />).</summary>
  public virtual Decimal? CuryDiscTot
  {
    get => this._CuryDiscTot;
    set => this._CuryDiscTot = value;
  }

  /// <summary>The document discount amount (without group discounts) for the document. The amount is in the base currency of the company.</summary>
  public virtual Decimal? DocDisc
  {
    get => this._DocDisc;
    set => this._DocDisc = value;
  }

  /// <summary>The discounted amount of the document, in the currency of the document (<see cref="P:PX.Objects.Extensions.Discount.Document.CuryID" />).</summary>
  public virtual Decimal? CuryDiscountedDocTotal
  {
    get => this._CuryDiscountedDocTotal;
    set => this._CuryDiscountedDocTotal = value;
  }

  /// <summary>The discounted amount of the document, in the base currency of the company.</summary>
  public virtual Decimal? DiscountedDocTotal
  {
    get => this._DiscountedDocTotal;
    set => this._DiscountedDocTotal = value;
  }

  /// <summary>The total taxable amount reduced on early payment according to cash discount. The amount is in the currency of the document (<see cref="P:PX.Objects.Extensions.Discount.Document.CuryID" />).</summary>
  public virtual Decimal? CuryDiscountedTaxableTotal
  {
    get => this._CuryDiscountedTaxableTotal;
    set => this._CuryDiscountedTaxableTotal = value;
  }

  /// <summary>The total taxable amount reduced on early payment according to cash discount. The amount is in the base currency of the company.</summary>
  public virtual Decimal? DiscountedTaxableTotal
  {
    get => this._DiscountedTaxableTotal;
    set => this._DiscountedTaxableTotal = value;
  }

  /// <summary>The total tax amount reduced on early payment according to cash discount. The amount is in the currency of the document (<see cref="P:PX.Objects.Extensions.Discount.Document.CuryID" />).</summary>
  public virtual Decimal? CuryDiscountedPrice
  {
    get => this._CuryDiscountedPrice;
    set => this._CuryDiscountedPrice = value;
  }

  /// <summary>The total tax amount reduced on early payment according to cash discount. The amount is in the base currency of the company.</summary>
  public virtual Decimal? DiscountedPrice
  {
    get => this._DiscountedPrice;
    set => this._DiscountedPrice = value;
  }

  /// <summary>The identifier of the location of the customer.</summary>
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  /// <summary>The date of the document.</summary>
  public virtual System.DateTime? DocumentDate
  {
    get => this._DocumentDate;
    set => this._DocumentDate = value;
  }

  /// <summary>The total amount of the lines of the document, in the currency of the document (<see cref="P:PX.Objects.Extensions.Discount.Document.CuryID" />).</summary>
  public virtual Decimal? CuryLineTotal
  {
    get => this._CuryLineTotal;
    set => this._CuryLineTotal = value;
  }

  /// <summary>The total amount of the lines of the document, in the base currency of the company.</summary>
  public virtual Decimal? LineTotal
  {
    get => this._LineTotal;
    set => this._LineTotal = value;
  }

  /// <summary>The miscellaneous total amount, in the currency of the document (<see cref="P:PX.Objects.Extensions.Discount.Document.CuryID" />).</summary>
  public virtual Decimal? CuryMiscTot
  {
    get => this._CuryMiscTot;
    set => this._CuryMiscTot = value;
  }

  /// <summary>The identifier of a customer account to whom this document belongs.</summary>
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  /// <exclude />
  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.branchID>
  {
  }

  /// <exclude />
  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.customerID>
  {
  }

  /// <exclude />
  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Document.curyID>
  {
  }

  /// <exclude />
  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  Document.curyInfoID>
  {
  }

  /// <exclude />
  public abstract class curyOrigDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Document.curyOrigDiscAmt>
  {
  }

  /// <exclude />
  public abstract class origDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Document.origDiscAmt>
  {
  }

  /// <exclude />
  public abstract class curyDiscTaken : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Document.curyDiscTaken>
  {
  }

  /// <exclude />
  public abstract class discTaken : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Document.discTaken>
  {
  }

  /// <exclude />
  public abstract class curyDiscBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Document.curyDiscBal>
  {
  }

  /// <exclude />
  public abstract class discBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Document.discBal>
  {
  }

  /// <exclude />
  public abstract class discTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Document.discTot>
  {
  }

  /// <exclude />
  public abstract class curyDiscTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Document.curyDiscTot>
  {
  }

  /// <exclude />
  public abstract class docDisc : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Document.docDisc>
  {
  }

  /// <exclude />
  public abstract class curyDiscountedDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Document.curyDiscountedDocTotal>
  {
  }

  /// <exclude />
  public abstract class discountedDocTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Document.discountedDocTotal>
  {
  }

  /// <exclude />
  public abstract class curyDiscountedTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Document.curyDiscountedTaxableTotal>
  {
  }

  /// <exclude />
  public abstract class discountedTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Document.discountedTaxableTotal>
  {
  }

  /// <exclude />
  public abstract class curyDiscountedPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Document.curyDiscountedPrice>
  {
  }

  /// <exclude />
  public abstract class discountedPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Document.discountedPrice>
  {
  }

  /// <exclude />
  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.locationID>
  {
  }

  /// <exclude />
  public abstract class documentDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  Document.documentDate>
  {
  }

  /// <exclude />
  public abstract class curyLineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Document.curyLineTotal>
  {
  }

  /// <exclude />
  public abstract class lineTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Document.lineTotal>
  {
  }

  /// <exclude />
  public abstract class curyMiscTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Document.curyMiscTot>
  {
  }

  /// <exclude />
  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.bAccountID>
  {
  }
}
