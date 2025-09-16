// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.SalesTax.Detail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.Extensions.SalesTax;

/// <summary>A mapped cache extension that represents a detail line of the document.</summary>
public class Detail : PXMappedCacheExtension
{
  /// <exclude />
  protected long? _CuryInfoID;

  /// <summary>Identifier of the tax category associated with the line.</summary>
  public virtual 
  #nullable disable
  string TaxCategoryID { get; set; }

  /// <summary>The identifier of the tax applied to the detail line.</summary>
  public virtual string TaxID { get; set; }

  /// <summary>
  /// Identifier of the CurrencyInfo object associated with the document.
  /// </summary>
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  /// <summary>The total discount for the line item, in the currency of the document (<see cref="P:PX.Objects.Extensions.SalesTax.Document.CuryID" />).</summary>
  public Decimal? CuryTranDiscount { get; set; }

  /// <summary>The total amount without discount for the line item, in the currency of the document (<see cref="P:PX.Objects.Extensions.SalesTax.Document.CuryID" />).</summary>
  public Decimal? CuryTranExtPrice { get; set; }

  /// <summary>The total amount for the line item, in the currency of the document (<see cref="P:PX.Objects.Extensions.SalesTax.Document.CuryID" />).</summary>
  public Decimal? CuryTranAmt { get; set; }

  /// <summary>The Group-level discount rate.</summary>
  public virtual Decimal? GroupDiscountRate { get; set; }

  /// <summary>The Document-level discount rate.</summary>
  public virtual Decimal? DocumentDiscountRate { get; set; }

  /// <summary>Identifier of linked Inventory Item</summary>
  public virtual int? InventoryID { get; set; }

  /// <summary>Unit of mesure ID</summary>
  public virtual string UOM { get; set; }

  /// <summary>Quantity of items in transaction</summary>
  public virtual Decimal? Qty { get; set; }

  /// <exclude />
  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Detail.taxCategoryID>
  {
  }

  /// <exclude />
  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Detail.taxID>
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
  public abstract class curyTranDiscount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Detail.curyTranDiscount>
  {
  }

  /// <exclude />
  public abstract class curyTranExtPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Detail.curyTranExtPrice>
  {
  }

  /// <exclude />
  public abstract class curyTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Detail.curyTranAmt>
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

  /// <summary>Representation of InventoryID field for BQL</summary>
  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Detail.inventoryID>
  {
  }

  /// <summary>Representation of UOM field for BQL</summary>
  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Detail.uOM>
  {
  }

  /// <summary>Representation of Qty field for BQL</summary>
  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Detail.qty>
  {
  }
}
