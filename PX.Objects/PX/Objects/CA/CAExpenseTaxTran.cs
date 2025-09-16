// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAExpenseTaxTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXCacheName("CAExpenseTaxTran")]
[Serializable]
public class CAExpenseTaxTran : TaxTran
{
  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault("CA")]
  [PXUIField(DisplayName = "Module", Enabled = false, Visible = false)]
  public override 
  #nullable disable
  string Module { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault(typeof (CATranType.cATransferExp))]
  [PXUIField(DisplayName = "Tran. Type", Enabled = false, Visible = false)]
  [PXParent(typeof (Select<CAExpense, Where<CAExpense.refNbr, Equal<Current<TaxTran.refNbr>>, And<CAExpense.lineNbr, Equal<Current<TaxTran.lineNbr>>>>>))]
  public override string TranType { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXDBDefault(typeof (CAExpense.lineNbr))]
  public override int? LineNbr { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true)]
  [PXDBDefault(typeof (CAExpense.refNbr))]
  [PXUIField(DisplayName = "Ref. Nbr.", Enabled = false, Visible = false)]
  public override string RefNbr { get; set; }

  [Branch(typeof (Search<CashAccount.branchID, Where<CashAccount.cashAccountID, Equal<Current<CAExpense.cashAccountID>>>>), null, true, true, true, Enabled = false)]
  public override int? BranchID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  public override string TaxPeriodID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, typeof (CAExpenseTaxTran.branchID), null, null, null, null, true, false, null, null, typeof (CAExpense.finPeriodID), true, true)]
  [PXDefault]
  public override string FinPeriodID { get; set; }

  [PXDBString(60, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr), DirtyRead = true)]
  public override string TaxID { get; set; }

  /// <summary>
  /// This is an auto-numbered field, which is a part of the primary key.
  /// </summary>
  [PXDBIdentity(IsKey = true)]
  public override int? RecordID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (Search<PX.Objects.TX.Tax.taxVendorID, Where<PX.Objects.TX.Tax.taxID, Equal<Current<CAExpenseTaxTran.taxID>>>>))]
  public override int? VendorID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDBDefault(typeof (CAExpense.taxZoneID))]
  public override string TaxZoneID { get; set; }

  [Account]
  [PXDefault]
  public override int? AccountID { get; set; }

  [SubAccount]
  [PXDefault]
  public override int? SubID { get; set; }

  [PXDBDate]
  [PXDBDefault(typeof (CAExpense.tranDate))]
  public override DateTime? TranDate { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  public override string TaxType { get; set; }

  [PXDBInt]
  [PXDefault(typeof (Search<TaxRev.taxBucketID, Where<TaxRev.taxID, Equal<Current<CAExpenseTaxTran.taxID>>, And<Current<CAExpenseTaxTran.tranDate>, Between<TaxRev.startDate, TaxRev.endDate>, And2<Where<TaxRev.taxType, Equal<Current<CAExpenseTaxTran.taxType>>, Or<TaxRev.taxType, Equal<PX.Objects.TX.TaxType.sales>, And<Current<CAExpenseTaxTran.taxType>, Equal<PX.Objects.TX.TaxType.pendingSales>, Or<TaxRev.taxType, Equal<PX.Objects.TX.TaxType.purchase>, And<Current<CAExpenseTaxTran.taxType>, Equal<PX.Objects.TX.TaxType.pendingPurchase>>>>>>, And<TaxRev.outdated, Equal<boolFalse>>>>>>))]
  public override int? TaxBucketID { get; set; }

  [PXDBLong]
  [CurrencyInfo(typeof (CAExpense.curyInfoID))]
  public override long? CuryInfoID { get; set; }

  [PXDBCurrency(typeof (CAExpenseTaxTran.curyInfoID), typeof (CAExpenseTaxTran.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXUnboundFormula(typeof (Switch<Case<WhereExempt<CAExpenseTaxTran.taxID>, CAExpenseTaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<CAExpense.curyVatExemptTotal>))]
  [PXUnboundFormula(typeof (Switch<Case<WhereTaxable<CAExpenseTaxTran.taxID>, CAExpenseTaxTran.curyTaxableAmt>, decimal0>), typeof (SumCalc<CAExpense.curyVatTaxableTotal>))]
  public override Decimal? CuryTaxableAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? TaxableAmt { get; set; }

  /// <summary>The exempted amount in the record currency.</summary>
  [PXDBCurrency(typeof (CAExpenseTaxTran.curyInfoID), typeof (CAExpenseTaxTran.exemptedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryExemptedAmt { get; set; }

  /// <summary>The exempted amount in the base currency.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? ExemptedAmt { get; set; }

  [PXDBCurrency(typeof (CAExpenseTaxTran.curyInfoID), typeof (CAExpenseTaxTran.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryTaxAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? TaxAmt { get; set; }

  [PXDBCurrency(typeof (CAExpenseTaxTran.curyInfoID), typeof (CAExpenseTaxTran.expenseAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public override Decimal? CuryExpenseAmt { get; set; }

  /// <summary>The description of the transaction.</summary>
  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  [PXDBDefault(typeof (CATransfer.descr))]
  [PXUIField]
  public override string Description { get; set; }

  public new abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAExpenseTaxTran.module>
  {
  }

  public new abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAExpenseTaxTran.tranType>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAExpenseTaxTran.lineNbr>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAExpenseTaxTran.refNbr>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAExpenseTaxTran.branchID>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAExpenseTaxTran.released>
  {
  }

  public new abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CAExpenseTaxTran.voided>
  {
  }

  public new abstract class taxPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CAExpenseTaxTran.taxPeriodID>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CAExpenseTaxTran.finPeriodID>
  {
  }

  public new abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAExpenseTaxTran.taxID>
  {
  }

  public new abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAExpenseTaxTran.recordID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAExpenseTaxTran.vendorID>
  {
  }

  public new abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAExpenseTaxTran.taxZoneID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAExpenseTaxTran.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAExpenseTaxTran.subID>
  {
  }

  public new abstract class tranDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CAExpenseTaxTran.tranDate>
  {
  }

  public new abstract class taxType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAExpenseTaxTran.taxType>
  {
  }

  public new abstract class taxBucketID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAExpenseTaxTran.taxBucketID>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CAExpenseTaxTran.curyInfoID>
  {
  }

  public new abstract class curyTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAExpenseTaxTran.curyTaxableAmt>
  {
  }

  public new abstract class taxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAExpenseTaxTran.taxableAmt>
  {
  }

  public new abstract class curyExemptedAmt : IBqlField, IBqlOperand
  {
  }

  public new abstract class exemptedAmt : IBqlField, IBqlOperand
  {
  }

  public new abstract class curyTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAExpenseTaxTran.curyTaxAmt>
  {
  }

  public new abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CAExpenseTaxTran.taxAmt>
  {
  }

  public new abstract class nonDeductibleTaxRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAExpenseTaxTran.nonDeductibleTaxRate>
  {
  }

  public new abstract class expenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAExpenseTaxTran.expenseAmt>
  {
  }

  public new abstract class curyExpenseAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CAExpenseTaxTran.curyExpenseAmt>
  {
  }

  public new abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CAExpenseTaxTran.description>
  {
  }
}
