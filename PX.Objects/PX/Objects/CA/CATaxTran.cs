// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CATaxTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXProjection(typeof (Select<TaxTran, Where<TaxTran.module, Equal<BatchModule.moduleCA>>>), Persistent = true)]
[PXCacheName("CA Tax Transaction")]
[Serializable]
public class CATaxTran : TaxTran
{
  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault("CA")]
  [PXUIField(DisplayName = "Module", Enabled = false, Visible = false)]
  public override 
  #nullable disable
  string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (CAAdj.adjTranType))]
  [PXParent(typeof (Select<CAAdj, Where<CAAdj.adjTranType, Equal<Current<TaxTran.tranType>>, And<CAAdj.adjRefNbr, Equal<Current<TaxTran.refNbr>>>>>))]
  [PXUIField(DisplayName = "Tran. Type", Enabled = false, Visible = false)]
  public override string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  [PXDBString(15, IsKey = true, IsUnicode = true)]
  [PXDBDefault(typeof (CAAdj.adjRefNbr))]
  [PXUIField(DisplayName = "Ref. Nbr.", Enabled = false, Visible = false)]
  public override string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [Branch(typeof (Search<CashAccount.branchID, Where<CashAccount.cashAccountID, Equal<Current<CAAdj.cashAccountID>>>>), null, true, true, true, Enabled = false)]
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

  [PX.Objects.GL.FinPeriodID(null, typeof (CATaxTran.branchID), null, null, null, null, true, false, null, null, typeof (CAAdj.tranPeriodID), true, true)]
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
  [PXDefault(typeof (Search<PX.Objects.TX.Tax.taxVendorID, Where<PX.Objects.TX.Tax.taxID, Equal<Current<CATaxTran.taxID>>>>))]
  public override int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDBDefault(typeof (CAAdj.taxZoneID))]
  public override string TaxZoneID
  {
    get => this._TaxZoneID;
    set => this._TaxZoneID = value;
  }

  [Account]
  [PXDefault]
  public override int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount]
  [PXDefault]
  public override int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBDate]
  [PXDBDefault(typeof (CAAdj.tranDate))]
  public override DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  public override string TaxType
  {
    get => this._TaxType;
    set => this._TaxType = value;
  }

  [PXDBInt]
  [PXDefault(typeof (Search<TaxRev.taxBucketID, Where<TaxRev.taxID, Equal<Current<CATaxTran.taxID>>, And<Current<CATaxTran.tranDate>, Between<TaxRev.startDate, TaxRev.endDate>, And2<Where<TaxRev.taxType, Equal<Current<CATaxTran.taxType>>, Or<TaxRev.taxType, Equal<PX.Objects.TX.TaxType.sales>, And<Current<CATaxTran.taxType>, Equal<PX.Objects.TX.TaxType.pendingSales>, Or<TaxRev.taxType, Equal<PX.Objects.TX.TaxType.purchase>, And<Current<CATaxTran.taxType>, Equal<PX.Objects.TX.TaxType.pendingPurchase>>>>>>, And<TaxRev.outdated, Equal<boolFalse>>>>>>))]
  public override int? TaxBucketID
  {
    get => this._TaxBucketID;
    set => this._TaxBucketID = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (CAAdj.curyInfoID))]
  public override long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (CATaxTran.curyInfoID), typeof (CATaxTran.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXUnboundFormula(typeof (Switch<Case<WhereExempt<CATaxTran.taxID>, CATaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<CAAdj.curyVatExemptTotal>))]
  [PXUnboundFormula(typeof (Switch<Case<WhereTaxable<CATaxTran.taxID>, CATaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<CAAdj.curyVatTaxableTotal>))]
  public override Decimal? CuryTaxableAmt
  {
    get => this._CuryTaxableAmt;
    set => this._CuryTaxableAmt = value;
  }

  /// <summary>The exempted amount in the record currency.</summary>
  [PXDBCurrency(typeof (CATaxTran.curyInfoID), typeof (CATaxTran.exemptedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryExemptedAmt { get; set; }

  /// <summary>The exempted amount in the base currency.</summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? ExemptedAmt { get; set; }

  [PXDBCurrency(typeof (CATaxTran.curyInfoID), typeof (CATaxTran.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryTaxAmt
  {
    get => this._CuryTaxAmt;
    set => this._CuryTaxAmt = value;
  }

  [PXDBCurrency(typeof (CATaxTran.curyInfoID), typeof (CATaxTran.taxAmtSumm))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? CuryTaxAmtSumm { get; set; }

  [PXDBCurrency(typeof (CATaxTran.curyInfoID), typeof (CATaxTran.expenseAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryExpenseAmt { get; set; }

  /// <summary>The description of the transaction.</summary>
  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  [PXDBDefault(typeof (CAAdj.tranDesc))]
  [PXUIField]
  public override string Description { get; set; }

  public new class PK : PrimaryKeyOf<CATaxTran>.By<CATaxTran.tranDate, CATaxTran.recordID>
  {
    public static CATaxTran Find(
      PXGraph graph,
      DateTime? tranDate,
      int? recordID,
      PKFindOptions options = 0)
    {
      return (CATaxTran) PrimaryKeyOf<CATaxTran>.By<CATaxTran.tranDate, CATaxTran.recordID>.FindBy(graph, (object) tranDate, (object) recordID, options);
    }
  }

  public new static class FK
  {
    public class Tax : PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<CATaxTran>.By<CATaxTran.taxID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<CATaxTran>.By<CATaxTran.branchID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<CATaxTran>.By<CATaxTran.taxZoneID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<CATaxTran>.By<TaxTran.bAccountID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CATaxTran>.By<CATaxTran.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<CATaxTran>.By<CATaxTran.subID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<CATaxTran>.By<CATaxTran.curyInfoID>
    {
    }
  }

  public new abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATaxTran.module>
  {
  }

  public new abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATaxTran.tranType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATaxTran.refNbr>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATaxTran.branchID>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CATaxTran.released>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CATaxTran.voided>
  {
  }

  public new abstract class taxPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATaxTran.taxPeriodID>
  {
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATaxTran.finPeriodID>
  {
  }

  public new abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATaxTran.taxID>
  {
  }

  public new abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATaxTran.recordID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATaxTran.vendorID>
  {
  }

  public new abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATaxTran.taxZoneID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATaxTran.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATaxTran.subID>
  {
  }

  public new abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CATaxTran.tranDate>
  {
  }

  public new abstract class taxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATaxTran.taxType>
  {
  }

  public new abstract class taxBucketID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CATaxTran.taxBucketID>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CATaxTran.curyInfoID>
  {
  }

  public new abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CATaxTran.curyTaxableAmt>
  {
  }

  public new abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATaxTran.taxableAmt>
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
  CATaxTran.curyTaxAmt>
  {
  }

  public new abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATaxTran.taxAmt>
  {
  }

  public new abstract class curyTaxAmtSumm : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CATaxTran.curyTaxAmtSumm>
  {
  }

  public new abstract class taxAmtSumm : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATaxTran.taxAmtSumm>
  {
  }

  public new abstract class nonDeductibleTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CATaxTran.nonDeductibleTaxRate>
  {
  }

  public new abstract class expenseAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CATaxTran.expenseAmt>
  {
  }

  public new abstract class curyExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CATaxTran.curyExpenseAmt>
  {
  }

  public new abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CATaxTran.description>
  {
  }
}
