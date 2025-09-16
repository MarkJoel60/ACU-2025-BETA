// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.SalesTax.TaxDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.Extensions.SalesTax;

/// <summary>A mapped cache extension that represents a tax detail line.</summary>
public class TaxDetail : PXMappedCacheExtension
{
  /// <summary>The ID of the tax calculated on the document.</summary>
  public virtual 
  #nullable disable
  string RefTaxID { get; set; }

  /// <summary>The tax rate used for tax calculation.</summary>
  public virtual Decimal? TaxRate { get; set; }

  /// <summary>Identifier of the <see cref="!:CurrencyInfo">CurrencyInfo</see> object associated with the document.</summary>
  public virtual long? CuryInfoID { get; set; }

  /// <summary>The percent of deduction that applies to the tax amount paid to the vendor for specific purchases.</summary>
  public virtual Decimal? NonDeductibleTaxRate { get; set; }

  public virtual Decimal? CuryOrigTaxableAmt { get; set; }

  /// <summary>The taxable amount (tax base), in the currency of the document (<see cref="!:CuryID" />).</summary>
  public virtual Decimal? CuryTaxableAmt { get; set; }

  /// <summary>The exempte amount, in the currency of the document (<see cref="!:CuryID" />).</summary>
  public virtual Decimal? CuryExemptedAmt { get; set; }

  /// <summary>The tax amount, in the currency of the document (<see cref="!:CuryID" />).</summary>
  public virtual Decimal? CuryTaxAmt { get; set; }

  /// <summary>The percentage that is deducted from the tax amount paid to the vendor for specific purchases, in the currency of the document (<see cref="!:CuryID" />).</summary>
  public virtual Decimal? CuryExpenseAmt { get; set; }

  /// <summary>The percentage that is deducted from the tax amount paid to the vendor for specific purchases, in the base currency of the company.</summary>
  public virtual Decimal? ExpenseAmt { get; set; }

  /// <summary>Converts TaxDetail to <see cref="T:PX.Objects.Extensions.SalesTax.TaxItem" />.</summary>
  public static explicit operator TaxItem(TaxDetail i)
  {
    return new TaxItem() { TaxID = i.RefTaxID };
  }

  /// <summary>
  /// The unit of measure used by tax. Specific/Per Unit taxes are calculated on quantities in this UOM
  /// </summary>
  public virtual string TaxUOM { get; set; }

  /// <summary>The taxable quantity for per unit taxes.</summary>
  public virtual Decimal? TaxableQty { get; set; }

  /// <exclude />
  public abstract class refTaxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxDetail.refTaxID>
  {
  }

  /// <exclude />
  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxDetail.taxRate>
  {
  }

  /// <exclude />
  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  TaxDetail.curyInfoID>
  {
  }

  /// <exclude />
  public abstract class nonDeductibleTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxDetail.nonDeductibleTaxRate>
  {
  }

  /// <exclude />
  public abstract class curyOrigTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxDetail.curyOrigTaxableAmt>
  {
  }

  /// <exclude />
  public abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxDetail.curyTaxableAmt>
  {
  }

  /// <exclude />
  public abstract class curyExemptedAmt : IBqlField, IBqlOperand
  {
  }

  /// <exclude />
  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxDetail.curyTaxAmt>
  {
  }

  /// <exclude />
  public abstract class curyExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxDetail.curyExpenseAmt>
  {
  }

  /// <exclude />
  public abstract class expenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxDetail.expenseAmt>
  {
  }

  /// <summary>
  /// The unit of measure used by tax. Specific/Per Unit taxes are calculated on quantities in this UOM.
  ///  </summary>
  public abstract class taxUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxDetail.taxUOM>
  {
  }

  /// <summary>The taxable quantity for per unit taxes.</summary>
  public abstract class taxableQty : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxDetail.taxableQty>
  {
  }
}
