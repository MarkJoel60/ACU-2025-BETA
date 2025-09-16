// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.TX;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents a line-level tax detail of an Proforma document.
/// The entities of this type cannot be edited directly. Instead,
/// <see cref="T:PX.Objects.TX.TaxBaseAttribute" /> descendants aggregate them to
/// <see cref="!:ARTaxTran" /> records, which can be edited on the Invoices
/// and Memos (AR301000) and Cash Sales (AR304000) forms (corresponding
/// to the <see cref="!:ARInvoiceEntry" /> and <see cref="!:ARCashSaleEntry" />
/// graphs, recpectively).
/// </summary>
[PXCacheName("PM Tax Detail")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMTax : TaxDetail, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _RefNbr;
  protected int? _LineNbr;
  protected Decimal? _CuryTaxableAmt;
  protected Decimal? _TaxableAmt;
  protected Decimal? _CuryTaxAmt;
  protected Decimal? _TaxAmt;
  protected byte[] _tstamp;

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (PMProforma.refNbr))]
  [PXUIField]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXUIField(DisplayName = "Revision", Visible = false)]
  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (PMProforma.revisionID))]
  public virtual int? RevisionID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXParent(typeof (Select<PMProforma, Where<PMProforma.refNbr, Equal<Current<PMTax.refNbr>>, And<PMProforma.revisionID, Equal<Current<PMTax.revisionID>>>>>))]
  [PXParent(typeof (Select<PMProformaTransactLine, Where<PMProformaTransactLine.refNbr, Equal<Current<PMTax.refNbr>>, And<PMProformaTransactLine.lineNbr, Equal<Current<PMTax.lineNbr>>, And<PMProformaTransactLine.revisionID, Equal<Current<PMTax.revisionID>>, And<PMProformaTransactLine.type, Equal<PMProformaLineType.transaction>>>>>>), LeaveChildren = true)]
  [PXParent(typeof (Select<PMProformaProgressLine, Where<PMProformaProgressLine.refNbr, Equal<Current<PMTax.refNbr>>, And<PMProformaProgressLine.lineNbr, Equal<Current<PMTax.lineNbr>>, And<PMProformaProgressLine.revisionID, Equal<Current<PMTax.revisionID>>, And<PMProformaProgressLine.type, Equal<PMProformaLineType.progressive>>>>>>), LeaveChildren = true)]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Tax ID")]
  [PXSelector(typeof (Tax.taxID), DescriptionField = typeof (Tax.descr))]
  public override string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (PMProforma.curyInfoID))]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (PMTax.curyInfoID), typeof (PMTax.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxableAmt
  {
    get => this._CuryTaxableAmt;
    set => this._CuryTaxableAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxableAmt
  {
    get => this._TaxableAmt;
    set => this._TaxableAmt = value;
  }

  /// <summary>The exempted amount in the record currency.</summary>
  [PXDBCurrency(typeof (PMTax.curyInfoID), typeof (PMTax.exemptedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryExemptedAmt { get; set; }

  /// <summary>The exempted amount in the base currency.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? ExemptedAmt { get; set; }

  [PXDBCurrency(typeof (PMTax.curyInfoID), typeof (PMTax.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxAmt
  {
    get => this._CuryTaxAmt;
    set => this._CuryTaxAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TaxAmt
  {
    get => this._TaxAmt;
    set => this._TaxAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (PMTax.curyInfoID), typeof (PMTax.retainedTaxableAmt))]
  [PXUIField]
  public virtual Decimal? CuryRetainedTaxableAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(4)]
  [PXUIField]
  public virtual Decimal? RetainedTaxableAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (PMTax.curyInfoID), typeof (PMTax.retainedTaxAmt))]
  [PXUIField]
  public virtual Decimal? CuryRetainedTaxAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(4)]
  [PXUIField]
  public virtual Decimal? RetainedTaxAmt { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTax.refNbr>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTax.revisionID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTax.lineNbr>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTax.taxID>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTax.taxRate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PMTax.curyInfoID>
  {
  }

  public abstract class curyTaxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTax.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTax.taxableAmt>
  {
  }

  public abstract class curyExemptedAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class exemptedAmt : IBqlField, IBqlOperand
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTax.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTax.taxAmt>
  {
  }

  public abstract class curyRetainedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMTax.curyRetainedTaxableAmt>
  {
  }

  public abstract class retainedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMTax.retainedTaxableAmt>
  {
  }

  public abstract class curyRetainedTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMTax.curyRetainedTaxAmt>
  {
  }

  public abstract class retainedTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTax.retainedTaxAmt>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMTax.Tstamp>
  {
  }
}
