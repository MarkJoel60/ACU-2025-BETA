// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.AP;

/// <summary>
/// A line-level tax detail of an accounts payable document.
/// The entities of this type cannot be edited directly. Instead, <see cref="T:PX.Objects.TX.TaxBaseAttribute" />
/// descendants aggregate them to <see cref="T:PX.Objects.AP.APTaxTran" /> records, which can be edited on the Bills
/// and Adjustments (AR301000) and Cash Purchases (AP304000) forms (corresponding to the
/// <see cref="T:PX.Objects.AP.APInvoiceEntry" /> and <see cref="T:PX.Objects.AP.APQuickCheckEntry" /> graphs, respectively).
/// </summary>
[PXCacheName("AP Tax Detail")]
[Serializable]
public class APTax : 
  TaxDetail,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ITranTax,
  ITaxDetailWithAmounts
{
  protected 
  #nullable disable
  string _TranType;
  protected string _RefNbr;
  protected int? _LineNbr;
  protected Decimal? _CuryTaxableAmt;
  protected Decimal? _TaxableAmt;
  protected Decimal? _CuryTaxAmt;
  protected Decimal? _TaxAmt;
  protected byte[] _tstamp;

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (APRegister.docType))]
  [PXUIField(DisplayName = "Tran. Type", Visibility = PXUIVisibility.Visible, Visible = false)]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (APRegister.refNbr))]
  [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.Visible, Visible = false)]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Line Nbr.", Visibility = PXUIVisibility.Visible, Visible = false)]
  [PXParent(typeof (Select<APRegister, Where<APRegister.docType, Equal<Current<APTax.tranType>>, And<APRegister.refNbr, Equal<Current<APTax.refNbr>>>>>))]
  [PXParent(typeof (Select<APTran, Where<APTran.tranType, Equal<Current<APTax.tranType>>, And<APTran.refNbr, Equal<Current<APTax.refNbr>>, And<APTran.lineNbr, Equal<Current<APTax.lineNbr>>>>>>))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Tax ID")]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr))]
  public override string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXDBLong]
  [PX.Objects.CM.Extensions.CurrencyInfo(typeof (APRegister.curyInfoID), Required = true)]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTax.curyInfoID), typeof (APTax.origTaxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryOrigTaxableAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigTaxableAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTax.curyInfoID), typeof (APTax.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Taxable Amount", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? CuryTaxableAmt
  {
    get => this._CuryTaxableAmt;
    set => this._CuryTaxableAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Taxable Amount", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? TaxableAmt
  {
    get => this._TaxableAmt;
    set => this._TaxableAmt = value;
  }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTax.curyInfoID), typeof (APTax.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tax Amount", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? CuryTaxAmt
  {
    get => this._CuryTaxAmt;
    set => this._CuryTaxAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tax Amount", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? TaxAmt
  {
    get => this._TaxAmt;
    set => this._TaxAmt = value;
  }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTax.curyInfoID), typeof (APTax.expenseAmt))]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Expense Amount", Visibility = PXUIVisibility.Visible)]
  public override Decimal? CuryExpenseAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTax.curyInfoID), typeof (APTax.taxableDiscountAmt))]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? CuryTaxableDiscountAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? TaxableDiscountAmt { get; set; }

  [PX.Objects.CM.Extensions.PXCurrency(typeof (APTax.curyInfoID), typeof (APTax.taxDiscountAmt))]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? CuryTaxDiscountAmt { get; set; }

  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? TaxDiscountAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTax.curyInfoID), typeof (APTax.retainedTaxableAmt))]
  [PXUIField(DisplayName = "Retained Taxable Amount", Visibility = PXUIVisibility.Visible, FieldClass = "Retainage")]
  public virtual Decimal? CuryRetainedTaxableAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXDBDecimal(4)]
  [PXUIField(DisplayName = "Retained Taxable Amount", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? RetainedTaxableAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTax.curyInfoID), typeof (APTax.retainedTaxAmt))]
  [PXUIField(DisplayName = "Retained Tax", Visibility = PXUIVisibility.Visible, FieldClass = "Retainage")]
  public virtual Decimal? CuryRetainedTaxAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXDBDecimal(4)]
  [PXUIField(DisplayName = "Retained Tax", Visibility = PXUIVisibility.Visible)]
  public virtual Decimal? RetainedTaxAmt { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : PrimaryKeyOf<APTax>.By<APTax.tranType, APTax.refNbr, APTax.lineNbr, APTax.taxID>
  {
    public static APTax Find(
      PXGraph graph,
      string tranType,
      string refNbr,
      int? lineNbr,
      string taxID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APTax>.By<APTax.tranType, APTax.refNbr, APTax.lineNbr, APTax.taxID>.FindBy(graph, (object) tranType, (object) refNbr, (object) lineNbr, (object) taxID, options);
    }
  }

  public static class FK
  {
    public class Document : 
      PrimaryKeyOf<APRegister>.By<APRegister.docType, APRegister.refNbr>.ForeignKeyOf<APTax>.By<APTax.tranType, APTax.refNbr>
    {
    }

    public class DocumentLine : 
      PrimaryKeyOf<APTran>.By<APTran.tranType, APTran.refNbr, APTran.lineNbr>.ForeignKeyOf<APTax>.By<APTax.tranType, APTax.refNbr, APTax.lineNbr>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<APTax>.By<APTax.curyInfoID>
    {
    }

    public class Tax : PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<APTax>.By<APTax.taxID>
    {
    }
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTax.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTax.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTax.lineNbr>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTax.taxID>
  {
  }

  public abstract class taxRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTax.taxRate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APTax.curyInfoID>
  {
  }

  public abstract class curyOrigTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTax.curyOrigTaxableAmt>
  {
  }

  public abstract class origTaxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTax.origTaxableAmt>
  {
  }

  public abstract class curyTaxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTax.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTax.taxableAmt>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTax.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTax.taxAmt>
  {
  }

  public abstract class curyExpenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTax.curyExpenseAmt>
  {
  }

  public abstract class expenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTax.expenseAmt>
  {
  }

  public abstract class curyTaxableDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTax.curyTaxableDiscountAmt>
  {
  }

  public abstract class taxableDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTax.taxableDiscountAmt>
  {
  }

  public abstract class curyTaxDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTax.curyTaxDiscountAmt>
  {
  }

  public abstract class taxDiscountAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTax.taxDiscountAmt>
  {
  }

  public abstract class curyRetainedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTax.curyRetainedTaxableAmt>
  {
  }

  public abstract class retainedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTax.retainedTaxableAmt>
  {
  }

  public abstract class curyRetainedTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTax.curyRetainedTaxAmt>
  {
  }

  public abstract class retainedTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTax.retainedTaxAmt>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APTax.Tstamp>
  {
  }
}
