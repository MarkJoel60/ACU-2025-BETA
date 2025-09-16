// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARTaxTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR.Standalone;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// A tax detail of an accounts receivable document. This is a
/// projection DAC over <see cref="T:PX.Objects.TX.TaxTran" /> restricted by <see cref="T:PX.Objects.GL.BatchModule.moduleAR">the accounts receivable module</see>.
/// The entities of this type are edited on the Invoices and Memos
/// (AR301000) and Cash Sales (AR304000) forms, which correspond to
/// the <see cref="T:PX.Objects.AR.ARInvoiceEntry" /> and <see cref="T:PX.Objects.AR.ARCashSaleEntry" />
/// graphs, respectively.
/// </summary>
/// <remarks>
/// Tax details are aggregates combined by <see cref="T:PX.Objects.TX.TaxBaseAttribute" />
/// from line-level <see cref="T:PX.Objects.AR.ARTax" /> records.
/// </remarks>
[PXProjection(typeof (Select<TaxTran, Where<TaxTran.module, Equal<BatchModule.moduleAR>>>), Persistent = true)]
[PXCacheName("AR Tax")]
[Serializable]
public class ARTaxTran : TaxTran
{
  protected Decimal? _CuryDiscountedTaxableAmt;
  protected Decimal? _DiscountedTaxableAmt;
  protected Decimal? _CuryDiscountedPrice;
  protected Decimal? _DiscountedPrice;

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault("AR")]
  [PXUIField(DisplayName = "Module", Enabled = false, Visible = false)]
  public override 
  #nullable disable
  string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (ARRegister.docType))]
  [PXParent(typeof (Select<ARRegister, Where<ARRegister.docType, Equal<Current<TaxTran.tranType>>, And<ARRegister.refNbr, Equal<Current<TaxTran.refNbr>>>>>))]
  [PXUIField(DisplayName = "Tran. Type", Enabled = false, Visible = false)]
  public override string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  [PXDBString(15, IsKey = true, IsUnicode = true)]
  [PXDBDefault(typeof (ARRegister.refNbr))]
  [PXUIField(DisplayName = "Ref. Nbr.", Enabled = false, Visible = false)]
  public override string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [Branch(typeof (ARRegister.branchID), null, true, true, true, Enabled = false)]
  public override int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  public override string TaxPeriodID
  {
    get => this._TaxPeriodID;
    set => this._TaxPeriodID = value;
  }

  [PX.Objects.GL.FinPeriodID(null, typeof (ARTaxTran.branchID), null, null, null, null, true, false, null, null, typeof (ARRegister.tranPeriodID), true, true)]
  [PXDefault]
  public override string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PX.Objects.TX.TaxID]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr), DirtyRead = true)]
  public override string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  /// <summary>
  /// This is an auto-numbered field, which is a part of the primary key.
  /// </summary>
  [PXDBIdentity(IsKey = true)]
  public override int? RecordID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (Search<PX.Objects.TX.Tax.taxVendorID, Where<PX.Objects.TX.Tax.taxID, Equal<Current<ARTaxTran.taxID>>>>))]
  public override int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBInt]
  [PXDefault(typeof (Parent<ARRegister.customerID>))]
  public override int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  public override string TaxZoneID
  {
    get => this._TaxZoneID;
    set => this._TaxZoneID = value;
  }

  [Account]
  [PXDefault(typeof (Search<SalesTax.histTaxAcctID, Where<SalesTax.taxID, Equal<Current<ARTaxTran.taxID>>>>))]
  public override int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount]
  [PXDefault(typeof (Search<SalesTax.histTaxSubID, Where<SalesTax.taxID, Equal<Current<ARTaxTran.taxID>>>>))]
  public override int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBDate]
  [PXDBDefault(typeof (ARRegister.docDate))]
  public override DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (Search<SalesTax.tranTaxType, Where<SalesTax.taxID, Equal<Current<ARTaxTran.taxID>>>>))]
  public override string TaxType
  {
    get => this._TaxType;
    set => this._TaxType = value;
  }

  [PXDBInt]
  [PXDefault(typeof (Search<TaxRev.taxBucketID, Where<TaxRev.taxID, Equal<Current<ARTaxTran.taxID>>, And<Current<ARTaxTran.tranDate>, Between<TaxRev.startDate, TaxRev.endDate>, And2<Where<TaxRev.taxType, Equal<Current<ARTaxTran.taxType>>, Or<TaxRev.taxType, Equal<PX.Objects.TX.TaxType.sales>, And<Current<ARTaxTran.taxType>, Equal<PX.Objects.TX.TaxType.pendingSales>, Or<TaxRev.taxType, Equal<PX.Objects.TX.TaxType.purchase>, And<Current<ARTaxTran.taxType>, Equal<PX.Objects.TX.TaxType.pendingPurchase>>>>>>, And<TaxRev.outdated, Equal<False>>>>>>))]
  public override int? TaxBucketID
  {
    get => this._TaxBucketID;
    set => this._TaxBucketID = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (ARRegister.curyInfoID), Required = true)]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (ARTaxTran.curyInfoID), typeof (ARTaxTran.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXUnboundFormula(typeof (Switch<Case<WhereExempt<ARTaxTran.taxID>, ARTaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<ARInvoice.curyVatExemptTotal>))]
  [PXUnboundFormula(typeof (Switch<Case<WhereTaxable<ARTaxTran.taxID>, ARTaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<ARInvoice.curyVatTaxableTotal>))]
  [PXUnboundFormula(typeof (Switch<Case<WhereExempt<ARTaxTran.taxID>, ARTaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<ARCashSale.curyVatExemptTotal>))]
  [PXUnboundFormula(typeof (Switch<Case<WhereTaxable<ARTaxTran.taxID>, ARTaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<ARCashSale.curyVatTaxableTotal>))]
  public override Decimal? CuryTaxableAmt
  {
    get => this._CuryTaxableAmt;
    set => this._CuryTaxableAmt = value;
  }

  /// <summary>The exempted amount in the record currency.</summary>
  [PXDBCurrency(typeof (ARTaxTran.curyInfoID), typeof (ARTaxTran.exemptedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryExemptedAmt { get; set; }

  /// <summary>The exempted amount in the base currency.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? ExemptedAmt { get; set; }

  [PXDBCurrency(typeof (ARTaxTran.curyInfoID), typeof (ARTaxTran.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryTaxAmt
  {
    get => this._CuryTaxAmt;
    set => this._CuryTaxAmt = value;
  }

  [PXDBCurrency(typeof (ARTaxTran.curyInfoID), typeof (ARTaxTran.taxAmtSumm))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? CuryTaxAmtSumm { get; set; }

  [PXCurrency(typeof (ARTaxTran.curyInfoID), typeof (ARTaxTran.taxableDiscountAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxableDiscountAmt { get; set; }

  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxableDiscountAmt { get; set; }

  [PXDBCurrency(typeof (ARTaxTran.curyInfoID), typeof (ARTaxTran.taxDiscountAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxDiscountAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxDiscountAmt { get; set; }

  [PXDBCurrency(typeof (ARTaxTran.curyInfoID), typeof (ARTaxTran.expenseAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryExpenseAmt { get; set; }

  /// <summary>
  /// The taxable amount reduced on early payment, according to cash discount.
  /// Given in the <see cref="!:CuryID"> currency of the document</see>.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXCurrency(typeof (ARTaxTran.curyInfoID), typeof (ARTaxTran.discountedTaxableAmt))]
  [PXUIField(DisplayName = "Discounted Taxable Amount", Visible = false, Enabled = false)]
  public virtual Decimal? CuryDiscountedTaxableAmt
  {
    get => this._CuryDiscountedTaxableAmt;
    set => this._CuryDiscountedTaxableAmt = value;
  }

  /// <summary>
  /// The taxable amount reduced on early payment, according to cash discount.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// </summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscountedTaxableAmt
  {
    get => this._DiscountedTaxableAmt;
    set => this._DiscountedTaxableAmt = value;
  }

  /// <summary>
  /// The tax amount reduced on early payment, according to cash discount.
  /// Given in the <see cref="!:CuryID"> currency of the document</see>.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXCurrency(typeof (ARTaxTran.curyInfoID), typeof (ARTaxTran.discountedPrice))]
  [PXUIField(DisplayName = "Tax on Discounted Price", Visible = false, Enabled = false)]
  public virtual Decimal? CuryDiscountedPrice
  {
    get => this._CuryDiscountedPrice;
    set => this._CuryDiscountedPrice = value;
  }

  /// <summary>
  /// The tax amount reduced on early payment, according to cash discount.
  /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID"> base currency of the company</see>.
  /// </summary>
  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscountedPrice
  {
    get => this._DiscountedPrice;
    set => this._DiscountedPrice = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (ARTaxTran.curyInfoID), typeof (ARTaxTran.retainedTaxableAmt))]
  [PXUIField]
  public override Decimal? CuryRetainedTaxableAmt
  {
    get => this._CuryRetainedTaxableAmt;
    set => this._CuryRetainedTaxableAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (ARTaxTran.curyInfoID), typeof (ARTaxTran.retainedTaxAmt))]
  [PXUIField]
  public override Decimal? CuryRetainedTaxAmt
  {
    get => this._CuryRetainedTaxAmt;
    set => this._CuryRetainedTaxAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (ARTaxTran.curyInfoID), typeof (ARTaxTran.retainedTaxAmtSumm))]
  public override Decimal? CuryRetainedTaxAmtSumm { get; set; }

  [PXDBTimestamp(BqlField = typeof (TaxTran.Tstamp), RecordComesFirst = true)]
  public override byte[] tstamp { get; set; }

  /// <summary>The description of the transaction.</summary>
  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  [PXDBDefault(typeof (ARRegister.docDesc))]
  [PXUIField]
  public override string Description { get; set; }

  public new class PK : PrimaryKeyOf<ARTaxTran>.By<ARTaxTran.tranDate, ARTaxTran.recordID>
  {
    public static ARTaxTran Find(
      PXGraph graph,
      DateTime? tranDate,
      int? recordID,
      PKFindOptions options = 0)
    {
      return (ARTaxTran) PrimaryKeyOf<ARTaxTran>.By<ARTaxTran.tranDate, ARTaxTran.recordID>.FindBy(graph, (object) tranDate, (object) recordID, options);
    }
  }

  public new static class FK
  {
    public class Document : 
      PrimaryKeyOf<PX.Objects.AP.APRegister>.By<PX.Objects.AP.APRegister.docType, PX.Objects.AP.APRegister.refNbr>.ForeignKeyOf<ARTaxTran>.By<ARTaxTran.tranType, ARTaxTran.refNbr>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARTaxTran>.By<ARTaxTran.branchID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<ARTaxTran>.By<ARTaxTran.vendorID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<ARTaxTran>.By<ARTaxTran.taxZoneID>
    {
    }

    public class Tax : PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<ARTaxTran>.By<ARTaxTran.taxID>
    {
    }

    public class BAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<ARTaxTran>.By<ARTaxTran.bAccountID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<ARTaxTran>.By<ARTaxTran.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<ARTaxTran>.By<ARTaxTran.subID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<ARTaxTran>.By<ARTaxTran.curyInfoID>
    {
    }
  }

  public new abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTaxTran.module>
  {
  }

  public new abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTaxTran.tranType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTaxTran.refNbr>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTaxTran.branchID>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTaxTran.released>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTaxTran.voided>
  {
  }

  public new abstract class taxPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTaxTran.taxPeriodID>
  {
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTaxTran.finPeriodID>
  {
  }

  public new abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTaxTran.taxID>
  {
  }

  public new abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTaxTran.recordID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTaxTran.vendorID>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTaxTran.bAccountID>
  {
  }

  public new abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTaxTran.taxZoneID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTaxTran.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTaxTran.subID>
  {
  }

  public new abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARTaxTran.tranDate>
  {
  }

  public new abstract class taxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTaxTran.taxType>
  {
  }

  public new abstract class taxBucketID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTaxTran.taxBucketID>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARTaxTran.curyInfoID>
  {
  }

  public new abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTaxTran.curyTaxableAmt>
  {
  }

  public new abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTaxTran.taxableAmt>
  {
  }

  public new abstract class curyExemptedAmt : IBqlField, IBqlOperand
  {
  }

  public new abstract class exemptedAmt : IBqlField, IBqlOperand
  {
  }

  public new abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTaxTran.curyTaxAmt>
  {
  }

  public new abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTaxTran.taxAmt>
  {
  }

  public new abstract class curyTaxAmtSumm : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTaxTran.curyTaxAmtSumm>
  {
  }

  public new abstract class taxAmtSumm : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTaxTran.taxAmtSumm>
  {
  }

  public abstract class curyTaxableDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTaxTran.curyTaxableDiscountAmt>
  {
  }

  public abstract class taxableDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTaxTran.taxableDiscountAmt>
  {
  }

  public abstract class curyTaxDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTaxTran.curyTaxDiscountAmt>
  {
  }

  public abstract class taxDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTaxTran.taxDiscountAmt>
  {
  }

  public new abstract class curyExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTaxTran.curyExpenseAmt>
  {
  }

  public new abstract class expenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTaxTran.expenseAmt>
  {
  }

  public abstract class curyDiscountedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTaxTran.curyDiscountedTaxableAmt>
  {
  }

  public abstract class discountedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTaxTran.discountedTaxableAmt>
  {
  }

  public abstract class curyDiscountedPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTaxTran.curyDiscountedPrice>
  {
  }

  public abstract class discountedPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTaxTran.discountedPrice>
  {
  }

  public new abstract class curyRetainedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTaxTran.curyRetainedTaxableAmt>
  {
  }

  public new abstract class retainedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTaxTran.retainedTaxableAmt>
  {
  }

  public new abstract class curyRetainedTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTaxTran.curyRetainedTaxAmt>
  {
  }

  public new abstract class retainedTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTaxTran.retainedTaxAmt>
  {
  }

  public new abstract class curyRetainedTaxAmtSumm : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTaxTran.curyRetainedTaxAmtSumm>
  {
  }

  public new abstract class retainedTaxAmtSumm : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTaxTran.retainedTaxAmtSumm>
  {
  }

  public new abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARTaxTran.Tstamp>
  {
  }

  public new abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTaxTran.description>
  {
  }
}
