// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APTaxTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP.Standalone;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.AP;

/// <summary>
/// A tax detail of an accounts payable document. This is a
/// projection DAC over <see cref="T:PX.Objects.TX.TaxTran" /> restricted by <see cref="T:PX.Objects.GL.BatchModule.moduleAP">the accounts payable module</see>.
/// The entities of this type are edited on the Bills and Adjustments
/// (AP301000) and Cash Purchases (AP304000) forms, which correspond to
/// the <see cref="T:PX.Objects.AP.APInvoiceEntry" /> and <see cref="T:PX.Objects.AP.APQuickCheckEntry" />
/// graphs, respectively.
/// </summary>
/// <remarks>
/// Tax details are aggregates combined by <see cref="T:PX.Objects.TX.TaxBaseAttribute" />
/// descendants from the line-level <see cref="T:PX.Objects.AP.APTax" /> records.
/// </remarks>
[PXProjection(typeof (Select<TaxTran, Where<TaxTran.module, Equal<BatchModule.moduleAP>>>), Persistent = true)]
[PXCacheName("AP Tax Details")]
[Serializable]
public class APTaxTran : TaxTran
{
  protected Decimal? _DiscountedTaxableAmt;
  protected Decimal? _CuryDiscountedPrice;
  protected Decimal? _DiscountedPrice;

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault("AP")]
  [PXUIField(DisplayName = "Module", Enabled = false, Visible = false)]
  public override 
  #nullable disable
  string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (APRegister.docType))]
  [PXParent(typeof (Select<APRegister, Where<APRegister.docType, Equal<Current<TaxTran.tranType>>, And<APRegister.refNbr, Equal<Current<TaxTran.refNbr>>>>>))]
  [PXUIField(DisplayName = "Tran. Type", Enabled = false, Visible = false)]
  public override string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (APRegister.refNbr))]
  [PXUIField(DisplayName = "Ref. Nbr.", Enabled = false, Visible = false)]
  public override string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [Branch(typeof (APRegister.branchID), null, true, true, true, Enabled = false)]
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

  [PX.Objects.GL.FinPeriodID(null, typeof (APTaxTran.branchID), null, null, null, null, true, false, null, null, typeof (APRegister.tranPeriodID), true, true)]
  [PXDefault]
  public override string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PX.Objects.TX.TaxID]
  [PXDefault]
  [PXUIField(DisplayName = "Tax ID", Visibility = PXUIVisibility.Visible)]
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
  [PXDefault(typeof (Search<PX.Objects.TX.Tax.taxVendorID, Where<PX.Objects.TX.Tax.taxID, Equal<Current<APTaxTran.taxID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  public override int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBInt]
  [PXDefault(typeof (Parent<APRegister.vendorID>))]
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

  [Account(ControlAccountForModule = "TX")]
  [PXDefault(typeof (Search<PurchaseTax.histTaxAcctID, Where<PurchaseTax.taxID, Equal<Current<APTaxTran.taxID>>>>))]
  public override int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount]
  [PXDefault(typeof (Search<PurchaseTax.histTaxSubID, Where<PurchaseTax.taxID, Equal<Current<APTaxTran.taxID>>>>))]
  public override int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBDate]
  [PXDBDefault(typeof (APRegister.docDate))]
  public override System.DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (Search<PurchaseTax.tranTaxType, Where<PurchaseTax.taxID, Equal<Current<APTaxTran.taxID>>>>))]
  public override string TaxType
  {
    get => this._TaxType;
    set => this._TaxType = value;
  }

  [PXDBInt]
  [PXDefault(typeof (Search<TaxRev.taxBucketID, Where<TaxRev.taxID, Equal<Current<APTaxTran.taxID>>, And<Current<APTaxTran.tranDate>, Between<TaxRev.startDate, TaxRev.endDate>, And2<Where<TaxRev.taxType, Equal<Current<APTaxTran.taxType>>, Or<TaxRev.taxType, Equal<PX.Objects.TX.TaxType.sales>, And<Current<APTaxTran.taxType>, Equal<PX.Objects.TX.TaxType.pendingSales>, Or<TaxRev.taxType, Equal<PX.Objects.TX.TaxType.purchase>, And<Current<APTaxTran.taxType>, Equal<PX.Objects.TX.TaxType.pendingPurchase>>>>>>, And<TaxRev.outdated, Equal<False>>>>>>))]
  public override int? TaxBucketID
  {
    get => this._TaxBucketID;
    set => this._TaxBucketID = value;
  }

  [PXDBLong]
  [PX.Objects.CM.Extensions.CurrencyInfo(typeof (APRegister.curyInfoID), Required = true)]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTaxTran.curyInfoID), typeof (APTaxTran.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Taxable Amount", Visibility = PXUIVisibility.Visible)]
  [PXUnboundFormula(typeof (Switch<Case<WhereExempt<APTaxTran.taxID>, APTaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<APInvoice.curyVatExemptTotal>))]
  [PXUnboundFormula(typeof (Switch<Case<WhereTaxable<APTaxTran.taxID>, APTaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<APInvoice.curyVatTaxableTotal>))]
  [PXUnboundFormula(typeof (Switch<Case<WhereExempt<APTaxTran.taxID>, APTaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<APQuickCheck.curyVatExemptTotal>))]
  [PXUnboundFormula(typeof (Switch<Case<WhereTaxable<APTaxTran.taxID>, APTaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<APQuickCheck.curyVatTaxableTotal>))]
  public override Decimal? CuryTaxableAmt
  {
    get => this._CuryTaxableAmt;
    set => this._CuryTaxableAmt = value;
  }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTaxTran.curyInfoID), typeof (APTaxTran.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tax Amount", Visibility = PXUIVisibility.Visible)]
  public override Decimal? CuryTaxAmt
  {
    get => this._CuryTaxAmt;
    set => this._CuryTaxAmt = value;
  }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTaxTran.curyInfoID), typeof (APTaxTran.taxAmtSumm))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? CuryTaxAmtSumm { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTaxTran.curyInfoID), typeof (APTaxTran.expenseAmt))]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Expense Amount", Visibility = PXUIVisibility.Visible)]
  public override Decimal? CuryExpenseAmt { get; set; }

  [PX.Objects.CM.Extensions.PXCurrency(typeof (APTaxTran.curyInfoID), typeof (APTaxTran.taxableDiscountAmt))]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? CuryTaxableDiscountAmt { get; set; }

  [PXDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? TaxableDiscountAmt { get; set; }

  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTaxTran.curyInfoID), typeof (APTaxTran.taxDiscountAmt))]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? CuryTaxDiscountAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? TaxDiscountAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTaxTran.curyInfoID), typeof (APTaxTran.retainedTaxableAmt))]
  [PXUIField(DisplayName = "Retained Taxable Amount", Visibility = PXUIVisibility.Visible, FieldClass = "Retainage")]
  public override Decimal? CuryRetainedTaxableAmt
  {
    get => this._CuryRetainedTaxableAmt;
    set => this._CuryRetainedTaxableAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTaxTran.curyInfoID), typeof (APTaxTran.retainedTaxAmt))]
  [PXUIField(DisplayName = "Retained Tax", Visibility = PXUIVisibility.Visible, FieldClass = "Retainage")]
  public override Decimal? CuryRetainedTaxAmt
  {
    get => this._CuryRetainedTaxAmt;
    set => this._CuryRetainedTaxAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PX.Objects.CM.Extensions.PXDBCurrency(typeof (APTaxTran.curyInfoID), typeof (APTaxTran.retainedTaxAmtSumm))]
  public override Decimal? CuryRetainedTaxAmtSumm { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PX.Objects.CM.Extensions.PXCurrency(typeof (APTaxTran.curyInfoID), typeof (APTaxTran.discountedTaxableAmt))]
  [PXUIField(DisplayName = "Discounted Taxable Amount", Visible = false, Enabled = false)]
  public virtual Decimal? CuryDiscountedTaxableAmt { get; set; }

  [PX.Objects.CM.Extensions.PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? DiscountedTaxableAmt
  {
    get => this._DiscountedTaxableAmt;
    set => this._DiscountedTaxableAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  [PX.Objects.CM.Extensions.PXCurrency(typeof (APTaxTran.curyInfoID), typeof (APTaxTran.discountedPrice))]
  [PXUIField(DisplayName = "Tax on Discounted Price", Visible = false, Enabled = false)]
  public virtual Decimal? CuryDiscountedPrice
  {
    get => this._CuryDiscountedPrice;
    set => this._CuryDiscountedPrice = value;
  }

  [PX.Objects.CM.Extensions.PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
  public virtual Decimal? DiscountedPrice
  {
    get => this._DiscountedPrice;
    set => this._DiscountedPrice = value;
  }

  /// <summary>The description of the transaction.</summary>
  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  [PXDBDefault(typeof (APRegister.docDesc), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.Visible)]
  public override string Description { get; set; }

  [PXDBTimestamp(BqlField = typeof (TaxTran.Tstamp), RecordComesFirst = true)]
  public override byte[] tstamp { get; set; }

  public new class PK : PrimaryKeyOf<APTaxTran>.By<APTaxTran.tranDate, APTaxTran.recordID>
  {
    public static APTaxTran Find(
      PXGraph graph,
      System.DateTime? tranDate,
      int? recordID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APTaxTran>.By<APTaxTran.tranDate, APTaxTran.recordID>.FindBy(graph, (object) tranDate, (object) recordID, options);
    }
  }

  public new static class FK
  {
    public class Document : 
      PrimaryKeyOf<APRegister>.By<APRegister.docType, APRegister.refNbr>.ForeignKeyOf<APTaxTran>.By<APTaxTran.tranType, APTaxTran.refNbr>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<APTaxTran>.By<APTaxTran.branchID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<APTaxTran>.By<APTaxTran.vendorID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<APTaxTran>.By<APTaxTran.taxZoneID>
    {
    }

    public class Tax : PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<APTaxTran>.By<APTaxTran.taxID>
    {
    }

    public class BAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<APTaxTran>.By<APTaxTran.bAccountID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<APTaxTran>.By<APTaxTran.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<APTaxTran>.By<APTaxTran.subID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<APTaxTran>.By<APTaxTran.curyInfoID>
    {
    }
  }

  public new abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTaxTran.module>
  {
  }

  public new abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTaxTran.tranType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTaxTran.refNbr>
  {
  }

  public new abstract class origTranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTaxTran.origTranType>
  {
  }

  public new abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTaxTran.origRefNbr>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTaxTran.branchID>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APTaxTran.released>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APTaxTran.voided>
  {
  }

  public new abstract class taxPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTaxTran.taxPeriodID>
  {
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTaxTran.finPeriodID>
  {
  }

  public new abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTaxTran.taxID>
  {
  }

  public new abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTaxTran.recordID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTaxTran.vendorID>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTaxTran.bAccountID>
  {
  }

  public new abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTaxTran.taxZoneID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTaxTran.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTaxTran.subID>
  {
  }

  public new abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APTaxTran.tranDate>
  {
  }

  public new abstract class taxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTaxTran.taxType>
  {
  }

  public new abstract class taxBucketID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APTaxTran.taxBucketID>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APTaxTran.curyInfoID>
  {
  }

  public new abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTaxTran.curyTaxableAmt>
  {
  }

  public new abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTaxTran.taxableAmt>
  {
  }

  public new abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTaxTran.curyTaxAmt>
  {
  }

  public new abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTaxTran.taxAmt>
  {
  }

  public new abstract class curyTaxAmtSumm : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTaxTran.curyTaxAmtSumm>
  {
  }

  public new abstract class taxAmtSumm : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTaxTran.taxAmtSumm>
  {
  }

  public new abstract class nonDeductibleTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTaxTran.nonDeductibleTaxRate>
  {
  }

  public new abstract class expenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  APTaxTran.expenseAmt>
  {
  }

  public new abstract class curyExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTaxTran.curyExpenseAmt>
  {
  }

  public abstract class curyTaxableDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTaxTran.curyTaxableDiscountAmt>
  {
  }

  public abstract class taxableDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTaxTran.taxableDiscountAmt>
  {
  }

  public abstract class curyTaxDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTaxTran.curyTaxDiscountAmt>
  {
  }

  public abstract class taxDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTaxTran.taxDiscountAmt>
  {
  }

  public new abstract class curyRetainedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTaxTran.curyRetainedTaxableAmt>
  {
  }

  public new abstract class retainedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTaxTran.retainedTaxableAmt>
  {
  }

  public new abstract class curyRetainedTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTaxTran.curyRetainedTaxAmt>
  {
  }

  public new abstract class retainedTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTaxTran.retainedTaxAmt>
  {
  }

  public new abstract class curyRetainedTaxAmtSumm : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTaxTran.curyRetainedTaxAmtSumm>
  {
  }

  public new abstract class retainedTaxAmtSumm : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTaxTran.retainedTaxAmtSumm>
  {
  }

  public abstract class curyDiscountedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTaxTran.curyDiscountedTaxableAmt>
  {
  }

  public abstract class discountedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTaxTran.discountedTaxableAmt>
  {
  }

  public abstract class curyDiscountedPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTaxTran.curyDiscountedPrice>
  {
  }

  public abstract class discountedPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APTaxTran.discountedPrice>
  {
  }

  public new abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APTaxTran.description>
  {
  }

  public new abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APTaxTran.Tstamp>
  {
  }
}
