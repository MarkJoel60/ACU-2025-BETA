// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.DAC.GLHistoryCalcRetainedEarnings
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.GL.DAC;

[PXProjection(typeof (Select<GLHistorySum>))]
[PXHidden]
public class GLHistoryCalcRetainedEarnings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlField = typeof (GLHistory.branchID))]
  public virtual int? BranchID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (GLHistory.ledgerID))]
  public virtual int? LedgerID { get; set; }

  [Account(IsKey = true, BqlField = typeof (GLHistory.accountID))]
  public virtual int? AccountID { get; set; }

  [SubAccount(IsKey = true, BqlField = typeof (GLHistory.subID))]
  public virtual int? SubID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlField = typeof (GLHistory.finPeriodID))]
  public virtual 
  #nullable disable
  string FinPeriodID { get; set; }

  [PXDBBaseCury(typeof (GLHistory.ledgerID), BqlField = typeof (GLHistory.finPtdCredit))]
  public virtual Decimal? FinPtdCredit { get; set; }

  [PXDBBaseCury(typeof (GLHistory.ledgerID), BqlField = typeof (GLHistory.finPtdDebit))]
  public virtual Decimal? FinPtdDebit { get; set; }

  [PXDBBaseCury(typeof (GLHistory.ledgerID), BqlField = typeof (GLHistory.tranPtdCredit))]
  public virtual Decimal? TranPtdCredit { get; set; }

  [PXDBBaseCury(typeof (GLHistory.ledgerID), BqlField = typeof (GLHistory.tranPtdDebit))]
  public virtual Decimal? TranPtdDebit { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (Add<IsNull<GLHistorySum.finPtdNetIncomeSum, decimal0>, IsNull<GLHistorySum.finPtdRetEarnSum, decimal0>>), typeof (Decimal))]
  public virtual Decimal? FinBegBalanceNew { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (Add<IsNull<GLHistorySum.finPtdNetIncomeSum, decimal0>, Add<IsNull<GLHistorySum.finPtdRetEarnSum, decimal0>, Sub<IsNull<GLHistoryCalcRetainedEarnings.finPtdCredit, decimal0>, IsNull<GLHistoryCalcRetainedEarnings.finPtdDebit, decimal0>>>>), typeof (Decimal))]
  public virtual Decimal? FinYtdBalanceNew { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (Add<IsNull<GLHistorySum.tranPtdNetIncomeSum, decimal0>, IsNull<GLHistorySum.tranPtdRetEarnSum, decimal0>>), typeof (Decimal))]
  public virtual Decimal? TranBegBalanceNew { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (Add<IsNull<GLHistorySum.tranPtdNetIncomeSum, decimal0>, Add<IsNull<GLHistorySum.tranPtdRetEarnSum, decimal0>, Sub<IsNull<GLHistoryCalcRetainedEarnings.tranPtdCredit, decimal0>, IsNull<GLHistoryCalcRetainedEarnings.tranPtdDebit, decimal0>>>>), typeof (Decimal))]
  public virtual Decimal? TranYtdBalanceNew { get; set; }

  [PXDBBaseCury(typeof (GLHistory.ledgerID), BqlField = typeof (GLHistory.curyFinPtdCredit))]
  public virtual Decimal? CuryFinPtdCredit { get; set; }

  [PXDBBaseCury(typeof (GLHistory.ledgerID), BqlField = typeof (GLHistory.curyFinPtdDebit))]
  public virtual Decimal? CuryFinPtdDebit { get; set; }

  [PXDBBaseCury(typeof (GLHistory.ledgerID), BqlField = typeof (GLHistory.curyTranPtdCredit))]
  public virtual Decimal? CuryTranPtdCredit { get; set; }

  [PXDBBaseCury(typeof (GLHistory.ledgerID), BqlField = typeof (GLHistory.curyTranPtdDebit))]
  public virtual Decimal? CuryTranPtdDebit { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (Add<IsNull<GLHistorySum.curyFinPtdNetIncomeSum, decimal0>, IsNull<GLHistorySum.curyFinPtdRetEarnSum, decimal0>>), typeof (Decimal))]
  public virtual Decimal? CuryFinBegBalanceNew { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (Add<IsNull<GLHistorySum.curyFinPtdNetIncomeSum, decimal0>, Add<IsNull<GLHistorySum.curyFinPtdRetEarnSum, decimal0>, Sub<IsNull<GLHistoryCalcRetainedEarnings.curyFinPtdCredit, decimal0>, IsNull<GLHistoryCalcRetainedEarnings.curyFinPtdDebit, decimal0>>>>), typeof (Decimal))]
  public virtual Decimal? CuryFinYtdBalanceNew { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (Add<IsNull<GLHistorySum.curyTranPtdNetIncomeSum, decimal0>, IsNull<GLHistorySum.curyTranPtdRetEarnSum, decimal0>>), typeof (Decimal))]
  public virtual Decimal? CuryTranBegBalanceNew { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (Add<IsNull<GLHistorySum.curyTranPtdNetIncomeSum, decimal0>, Add<IsNull<GLHistorySum.curyTranPtdRetEarnSum, decimal0>, Sub<IsNull<GLHistoryCalcRetainedEarnings.curyTranPtdCredit, decimal0>, IsNull<GLHistoryCalcRetainedEarnings.curyTranPtdDebit, decimal0>>>>), typeof (Decimal))]
  public virtual Decimal? CuryTranYtdBalanceNew { get; set; }

  public abstract class branchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLHistoryCalcRetainedEarnings.branchID>
  {
  }

  public abstract class ledgerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLHistoryCalcRetainedEarnings.ledgerID>
  {
  }

  public abstract class accountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLHistoryCalcRetainedEarnings.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryCalcRetainedEarnings.subID>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLHistoryCalcRetainedEarnings.finPeriodID>
  {
  }

  public abstract class finPtdCredit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryCalcRetainedEarnings.finPtdCredit>
  {
  }

  public abstract class finPtdDebit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryCalcRetainedEarnings.finPtdDebit>
  {
  }

  public abstract class tranPtdCredit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryCalcRetainedEarnings.tranPtdCredit>
  {
  }

  public abstract class tranPtdDebit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryCalcRetainedEarnings.tranPtdDebit>
  {
  }

  public abstract class finBegBalanceNew : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryCalcRetainedEarnings.finBegBalanceNew>
  {
  }

  public abstract class finYtdBalanceNew : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryCalcRetainedEarnings.finYtdBalanceNew>
  {
  }

  public abstract class tranBegBalanceNew : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryCalcRetainedEarnings.tranBegBalanceNew>
  {
  }

  public abstract class tranYtdBalanceNew : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryCalcRetainedEarnings.tranYtdBalanceNew>
  {
  }

  public abstract class curyFinPtdCredit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryCalcRetainedEarnings.curyFinPtdCredit>
  {
  }

  public abstract class curyFinPtdDebit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryCalcRetainedEarnings.curyFinPtdDebit>
  {
  }

  public abstract class curyTranPtdCredit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryCalcRetainedEarnings.curyTranPtdCredit>
  {
  }

  public abstract class curyTranPtdDebit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryCalcRetainedEarnings.curyTranPtdDebit>
  {
  }

  public abstract class curyFinBegBalanceNew : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryCalcRetainedEarnings.curyFinBegBalanceNew>
  {
  }

  public abstract class curyFinYtdBalanceNew : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryCalcRetainedEarnings.curyFinYtdBalanceNew>
  {
  }

  public abstract class curyTranBegBalanceNew : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryCalcRetainedEarnings.curyTranBegBalanceNew>
  {
  }

  public abstract class curyTranYtdBalanceNew : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryCalcRetainedEarnings.curyTranYtdBalanceNew>
  {
  }
}
