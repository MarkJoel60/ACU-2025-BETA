// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.SalesTax.Document
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.Extensions.SalesTax;

/// <summary>A mapped cache extension that represents a document that supports sales taxes.</summary>
public class Document : PXMappedCacheExtension
{
  /// <exclude />
  protected int? _BranchID;
  /// <exclude />
  protected 
  #nullable disable
  string _CuryID;
  /// <exclude />
  protected long? _CuryInfoID;
  /// <exclude />
  protected System.DateTime? _DocumentDate;

  /// <summary>The identifier of the branch associated with the document.</summary>
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>The identifier of the currency of the document.</summary>
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  /// <summary>
  /// The identifier of the CurrencyInfo object associated with the document.
  /// </summary>
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  /// <summary>The date of the document.</summary>
  public virtual System.DateTime? DocumentDate
  {
    get => this._DocumentDate;
    set => this._DocumentDate = value;
  }

  /// <summary>The identifier of the financial period of the document.</summary>
  public virtual string FinPeriodID { get; set; }

  /// <summary>The identifier of the tax zone.</summary>
  public virtual string TaxZoneID { get; set; }

  /// <summary>The identifier of the credit terms.</summary>
  public virtual string TermsID { get; set; }

  /// <summary>The total amount of the lines of the document, in the currency of the document (<see cref="P:PX.Objects.Extensions.SalesTax.Document.CuryID" />).</summary>
  public virtual Decimal? CuryLineTotal { get; set; }

  /// <summary>The total discounts of the lines of the document, in the currency of the document (<see cref="P:PX.Objects.Extensions.SalesTax.Document.CuryID" />).</summary>
  public virtual Decimal? CuryDiscountLineTotal { get; set; }

  /// <summary>The total amount without discounts of the lines of the document, in the currency of the document (<see cref="P:PX.Objects.Extensions.SalesTax.Document.CuryID" />).</summary>
  public virtual Decimal? CuryExtPriceTotal { get; set; }

  /// <summary>The balance of the document, in the currency of the document (<see cref="P:PX.Objects.Extensions.SalesTax.Document.CuryID" />).</summary>
  public Decimal? CuryDocBal { get; set; }

  /// <summary>The total amount of tax paid on the document, in the currency of the document (<see cref="P:PX.Objects.Extensions.SalesTax.Document.CuryID" />).</summary>
  public Decimal? CuryTaxTotal { get; set; }

  public Decimal? CuryWhTaxTotal { get; set; }

  /// <summary>The total group and document discount for the document. The discount is in the currency of the document (<see cref="P:PX.Objects.Extensions.SalesTax.Document.CuryID" />).</summary>
  public virtual Decimal? CuryDiscTot { get; set; }

  /// <summary>The discount amount of the document, in the currency of the document (<see cref="P:PX.Objects.Extensions.SalesTax.Document.CuryID" />).</summary>
  public virtual Decimal? CuryDiscAmt { get; set; }

  /// <summary>The amount of withholding tax calculated for the document, in the currency of the document (<see cref="P:PX.Objects.Extensions.SalesTax.Document.CuryID" />).</summary>
  public virtual Decimal? CuryOrigWhTaxAmt { get; set; }

  /// <summary>The tax amount discrepancy (that is, the difference between the tax amount calculated by the system and the tax amounts entered by a user manually for the
  /// tax-inclusive items). The amount is in the currency of the document (<see cref="P:PX.Objects.Extensions.SalesTax.Document.CuryID" />).</summary>
  public Decimal? CuryTaxRoundDiff { get; set; }

  /// <summary>The tax amount discrepancy (that is, the difference between the tax amount calculated by the system and the tax amounts entered by a user manually for the
  /// tax-inclusive items). The amount is in the base currency of the company.</summary>
  public Decimal? TaxRoundDiff { get; set; }

  /// <summary>Indicates (if set to <tt>true</tt>) that the tax information related to the document was was imported from the external tax engine.</summary>
  public virtual bool? ExternalTaxesImportInProgress { get; set; }

  /// <summary>Indicates (if set to <tt>true</tt>) that the tax information related to the document was saved to the external tax engine (Avalara).</summary>
  public virtual bool? IsTaxSaved { get; set; }

  /// <summary>Specifies whether taxes should be calculated and how they should be calculated.</summary>
  public PX.Objects.TX.TaxCalc? TaxCalc { get; set; }

  /// <summary>The tax calculation mode, which defines which amounts (tax-inclusive or tax-exclusive) should be entered in the detail lines of a document.</summary>
  public virtual string TaxCalcMode { get; set; }

  /// <exclude />
  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.branchID>
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
  public abstract class documentDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  Document.documentDate>
  {
  }

  /// <exclude />
  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Document.finPeriodID>
  {
  }

  /// <exclude />
  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Document.taxZoneID>
  {
  }

  /// <exclude />
  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Document.termsID>
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
  public abstract class curyDiscountLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Document.curyDiscountLineTotal>
  {
  }

  /// <exclude />
  public abstract class curyExtPriceTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Document.curyExtPriceTotal>
  {
  }

  /// <exclude />
  public abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Document.curyDocBal>
  {
  }

  /// <exclude />
  public abstract class curyTaxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Document.curyTaxTotal>
  {
  }

  /// <exclude />
  public abstract class curyWhTaxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Document.curyWhTaxTotal>
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
  public abstract class curyDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Document.curyDiscAmt>
  {
  }

  /// <exclude />
  public abstract class curyOrigWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Document.curyOrigWhTaxAmt>
  {
  }

  /// <exclude />
  public abstract class curyTaxRoundDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    Document.curyTaxRoundDiff>
  {
  }

  /// <exclude />
  public abstract class taxRoundDiff : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Document.taxRoundDiff>
  {
  }

  /// <exclude />
  public abstract class externalTaxesImportInProgress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Document.externalTaxesImportInProgress>
  {
  }

  /// <exclude />
  public abstract class isTaxSaved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Document.isTaxSaved>
  {
  }

  /// <exclude />
  public abstract class taxCalc : IBqlField, IBqlOperand
  {
  }

  /// <exclude />
  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Document.taxCalcMode>
  {
  }
}
