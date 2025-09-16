// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.Discount.Detail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.Extensions.Discount;

/// <summary>A mapped cache extension that represents a detail line of the document.</summary>
public class Detail : PXMappedCacheExtension
{
  /// <exclude />
  protected int? _BranchID;
  /// <exclude />
  protected int? _InventoryID;
  /// <exclude />
  protected int? _SiteID;
  /// <exclude />
  protected int? _CustomerID;
  /// <exclude />
  protected int? _VendorID;
  /// <exclude />
  protected long? _CuryInfoID;
  /// <exclude />
  protected Decimal? _Quantity;
  /// <exclude />
  protected Decimal? _CuryUnitPrice;
  /// <exclude />
  protected Decimal? _CuryExtPrice;
  /// <exclude />
  protected Decimal? _CuryLineAmount;
  /// <exclude />
  protected 
  #nullable disable
  string _UOM;
  /// <exclude />
  protected ushort[] _DiscountsAppliedToLine;
  /// <exclude />
  protected Decimal? _OrigGroupDiscountRate;
  /// <exclude />
  protected Decimal? _OrigDocumentDiscountRate;
  /// <exclude />
  protected Decimal? _GroupDiscountRate;
  /// <exclude />
  protected Decimal? _DocumentDiscountRate;
  /// <exclude />
  protected Decimal? _CuryDiscAmt;
  /// <exclude />
  protected Decimal? _DiscPct;
  /// <exclude />
  protected string _DiscountID;
  /// <exclude />
  protected string _DiscountSequenceID;
  /// <exclude />
  protected bool? _IsFree;
  /// <exclude />
  protected bool? _ManualDisc;
  /// <exclude />
  protected bool? _ManualPrice;
  /// <exclude />
  protected string _LineType;
  /// <exclude />
  protected string _TaxCategoryID;
  /// <exclude />
  protected bool? _FreezeManualDisc;

  /// <summary>The identifier of the branch.</summary>
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>The identifier of the inventory item.</summary>
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  /// <summary>The warehouse ID.</summary>
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  /// <summary>The identifier of the customer account.</summary>
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  /// <summary>The identifier of the vendor account associated with the detail line.</summary>
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  /// <summary>The identifier of the <see cref="!:CurrencyInfo">CurrencyInfo</see> object associated with the document.</summary>
  /// <value>
  /// Corresponds to the <see cref="!:CurrencyInfoID" /> field.
  /// </value>
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  /// <summary>The quantity of the line item.</summary>
  public virtual Decimal? Quantity
  {
    get => this._Quantity;
    set => this._Quantity = value;
  }

  /// <summary>
  ///   <para>The price per unit of the line item, in the currency of the document (<see cref="P:PX.Objects.Extensions.Discount.Document.CuryID" />).</para>
  /// </summary>
  public virtual Decimal? CuryUnitPrice
  {
    get => this._CuryUnitPrice;
    set => this._CuryUnitPrice = value;
  }

  /// <summary>The extended price of the line item, in the currency of the document (<see cref="P:PX.Objects.Extensions.Discount.Document.CuryID" />).</summary>
  public virtual Decimal? CuryExtPrice
  {
    get => this._CuryExtPrice;
    set => this._CuryExtPrice = value;
  }

  /// <summary>The total amount for the line item, in the currency of the document (<see cref="P:PX.Objects.Extensions.Discount.Document.CuryID" />).</summary>
  public virtual Decimal? CuryLineAmount
  {
    get => this._CuryLineAmount;
    set => this._CuryLineAmount = value;
  }

  /// <summary>The unit of measure (UOM) used for the detail line.</summary>
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  /// <summary>Array of line numbers of discount lines applied to the current lint</summary>
  public virtual ushort[] DiscountsAppliedToLine
  {
    get => this._DiscountsAppliedToLine;
    set => this._DiscountsAppliedToLine = value;
  }

  /// <summary>Group-level discount rate for the set of discounts from original document.</summary>
  public virtual Decimal? OrigGroupDiscountRate
  {
    get => this._OrigGroupDiscountRate;
    set => this._OrigGroupDiscountRate = value;
  }

  /// <summary>Document-level discount rate for the set of discounts from original document.</summary>
  public virtual Decimal? OrigDocumentDiscountRate
  {
    get => this._OrigDocumentDiscountRate;
    set => this._OrigDocumentDiscountRate = value;
  }

  /// <summary>The Group-level discount rate.</summary>
  public virtual Decimal? GroupDiscountRate
  {
    get => this._GroupDiscountRate;
    set => this._GroupDiscountRate = value;
  }

  /// <summary>The Document-level discount rate.</summary>
  public virtual Decimal? DocumentDiscountRate
  {
    get => this._DocumentDiscountRate;
    set => this._DocumentDiscountRate = value;
  }

  /// <summary>The amount of the discount applied to the detail line, in the currency of the document (<see cref="P:PX.Objects.Extensions.Discount.Document.CuryID" />).</summary>
  public virtual Decimal? CuryDiscAmt
  {
    get => this._CuryDiscAmt;
    set => this._CuryDiscAmt = value;
  }

  /// <summary>The percent of the line-level discount that has been applied manually or automatically to this line item.</summary>
  public virtual Decimal? DiscPct
  {
    get => this._DiscPct;
    set => this._DiscPct = value;
  }

  /// <summary>The identifier (code) of the line discount that has been applied to this line.</summary>
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  /// <summary>The identifier of a discount sequence that has been applied to this line.</summary>
  public virtual string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
  }

  /// <summary>Indicates (if set to <tt>true</tt>) that the line item is free.</summary>
  public virtual bool? IsFree
  {
    get => this._IsFree;
    set => this._IsFree = value;
  }

  /// <summary>Indicates (if set to <tt>true</tt>) that the discount has been applied manually for this line item.</summary>
  public virtual bool? ManualDisc
  {
    get => this._ManualDisc;
    set => this._ManualDisc = value;
  }

  /// <summary>Indicates (if set to <tt>true</tt>) that the unit price has been specified for this line item manually</summary>
  public virtual bool? ManualPrice
  {
    get => this._ManualPrice;
    set => this._ManualPrice = value;
  }

  /// <summary>The type of the line.</summary>
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  /// <summary>The identifier of the tax category.</summary>
  public virtual string TaxCategoryID
  {
    get => this._TaxCategoryID;
    set => this._TaxCategoryID = value;
  }

  public virtual bool? FreezeManualDisc
  {
    get => this._FreezeManualDisc;
    set => this._FreezeManualDisc = value;
  }

  /// <exclude />
  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Detail.branchID>
  {
  }

  /// <exclude />
  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Detail.inventoryID>
  {
  }

  /// <exclude />
  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Detail.siteID>
  {
  }

  /// <exclude />
  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Detail.customerID>
  {
  }

  /// <exclude />
  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Detail.vendorID>
  {
  }

  /// <exclude />
  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  Detail.curyInfoID>
  {
  }

  /// <exclude />
  public abstract class quantity : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Detail.quantity>
  {
  }

  /// <exclude />
  public abstract class curyUnitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Detail.curyUnitPrice>
  {
  }

  /// <exclude />
  public abstract class curyExtPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Detail.curyExtPrice>
  {
  }

  /// <exclude />
  public abstract class curyLineAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Detail.curyLineAmount>
  {
  }

  /// <exclude />
  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Detail.uOM>
  {
  }

  /// <exclude />
  public abstract class discountsAppliedToLine : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    Detail.discountsAppliedToLine>
  {
  }

  /// <exclude />
  public abstract class origGroupDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Detail.origGroupDiscountRate>
  {
  }

  /// <exclude />
  public abstract class origDocumentDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Detail.origDocumentDiscountRate>
  {
  }

  /// <exclude />
  public abstract class groupDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Detail.groupDiscountRate>
  {
  }

  /// <exclude />
  public abstract class documentDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Detail.documentDiscountRate>
  {
  }

  /// <exclude />
  public abstract class curyDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Detail.curyDiscAmt>
  {
  }

  /// <exclude />
  public abstract class discPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Detail.discPct>
  {
  }

  /// <exclude />
  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Detail.discountID>
  {
  }

  /// <exclude />
  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Detail.discountSequenceID>
  {
  }

  /// <exclude />
  public abstract class isFree : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Detail.isFree>
  {
  }

  /// <exclude />
  public abstract class manualDisc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Detail.manualDisc>
  {
  }

  /// <exclude />
  public abstract class manualPrice : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Detail.manualPrice>
  {
  }

  /// <exclude />
  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Detail.lineType>
  {
  }

  /// <exclude />
  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Detail.taxCategoryID>
  {
  }

  /// <exclude />
  public abstract class freezeManualDisc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Detail.freezeManualDisc>
  {
  }
}
