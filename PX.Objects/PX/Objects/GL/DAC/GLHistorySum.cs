// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.DAC.GLHistorySum
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

[PXHidden]
public class GLHistorySum : GLHistory
{
  [PXBaseCury]
  [PXDBScalar(typeof (Search4<GLHistoryNet.finPtdNetIncome, Where<GLHistoryNet.branchID, Equal<GLHistory.branchID>, And<GLHistoryNet.ledgerID, Equal<GLHistory.ledgerID>, And<GLHistoryNet.accountID, Equal<CurrentValue<GLSetup.ytdNetIncAccountID>>, And<GLHistoryNet.subID, Equal<GLHistory.subID>, And<GLHistoryNet.finPeriodID, LessEqual<Substring<GLHistory.finPeriodID, int1, int4>>>>>>>, Aggregate<Sum<GLHistoryNet.finPtdNetIncome>>>))]
  public virtual Decimal? FinPtdNetIncomeSum { get; set; }

  [PXBaseCury]
  [PXDBScalar(typeof (Search4<GLHistoryNet.finPtdNetIncome, Where<GLHistoryNet.branchID, Equal<GLHistory.branchID>, And<GLHistoryNet.ledgerID, Equal<GLHistory.ledgerID>, And<GLHistoryNet.accountID, Equal<CurrentValue<GLSetup.retEarnAccountID>>, And<GLHistoryNet.subID, Equal<GLHistory.subID>, And<GLHistoryNet.finPeriodID, Less<GLHistory.finPeriodID>>>>>>, Aggregate<Sum<GLHistoryNet.finPtdNetIncome>>>))]
  public virtual Decimal? FinPtdRetEarnSum { get; set; }

  [PXBaseCury]
  [PXDBScalar(typeof (Search4<GLHistoryNet.tranPtdNetIncome, Where<GLHistoryNet.branchID, Equal<GLHistory.branchID>, And<GLHistoryNet.ledgerID, Equal<GLHistory.ledgerID>, And<GLHistoryNet.accountID, Equal<CurrentValue<GLSetup.ytdNetIncAccountID>>, And<GLHistoryNet.subID, Equal<GLHistory.subID>, And<GLHistoryNet.finPeriodID, LessEqual<Substring<GLHistory.finPeriodID, int1, int4>>>>>>>, Aggregate<Sum<GLHistoryNet.tranPtdNetIncome>>>))]
  public virtual Decimal? TranPtdNetIncomeSum { get; set; }

  [PXBaseCury]
  [PXDBScalar(typeof (Search4<GLHistoryNet.tranPtdNetIncome, Where<GLHistoryNet.branchID, Equal<GLHistory.branchID>, And<GLHistoryNet.ledgerID, Equal<GLHistory.ledgerID>, And<GLHistoryNet.accountID, Equal<CurrentValue<GLSetup.retEarnAccountID>>, And<GLHistoryNet.subID, Equal<GLHistory.subID>, And<GLHistoryNet.finPeriodID, Less<GLHistory.finPeriodID>>>>>>, Aggregate<Sum<GLHistoryNet.tranPtdNetIncome>>>))]
  public virtual Decimal? TranPtdRetEarnSum { get; set; }

  [PXDBDecimal(4)]
  [PXDBScalar(typeof (Search4<GLHistoryNet.curyFinPtdNetIncome, Where<GLHistoryNet.branchID, Equal<GLHistory.branchID>, And<GLHistoryNet.ledgerID, Equal<GLHistory.ledgerID>, And<GLHistoryNet.accountID, Equal<CurrentValue<GLSetup.ytdNetIncAccountID>>, And<GLHistoryNet.subID, Equal<GLHistory.subID>, And<GLHistoryNet.finPeriodID, LessEqual<Substring<GLHistory.finPeriodID, int1, int4>>>>>>>, Aggregate<Sum<GLHistoryNet.curyFinPtdNetIncome>>>))]
  public virtual Decimal? CuryFinPtdNetIncomeSum { get; set; }

  [PXDBDecimal(4)]
  [PXDBScalar(typeof (Search4<GLHistoryNet.curyFinPtdNetIncome, Where<GLHistoryNet.branchID, Equal<GLHistory.branchID>, And<GLHistoryNet.ledgerID, Equal<GLHistory.ledgerID>, And<GLHistoryNet.accountID, Equal<CurrentValue<GLSetup.retEarnAccountID>>, And<GLHistoryNet.subID, Equal<GLHistory.subID>, And<GLHistoryNet.finPeriodID, Less<GLHistory.finPeriodID>>>>>>, Aggregate<Sum<GLHistoryNet.curyFinPtdNetIncome>>>))]
  public virtual Decimal? CuryFinPtdRetEarnSum { get; set; }

  [PXDBDecimal(4)]
  [PXDBScalar(typeof (Search4<GLHistoryNet.curyTranPtdNetIncome, Where<GLHistoryNet.branchID, Equal<GLHistory.branchID>, And<GLHistoryNet.ledgerID, Equal<GLHistory.ledgerID>, And<GLHistoryNet.accountID, Equal<CurrentValue<GLSetup.ytdNetIncAccountID>>, And<GLHistoryNet.subID, Equal<GLHistory.subID>, And<GLHistoryNet.finPeriodID, LessEqual<Substring<GLHistory.finPeriodID, int1, int4>>>>>>>, Aggregate<Sum<GLHistoryNet.curyTranPtdNetIncome>>>))]
  public virtual Decimal? CuryTranPtdNetIncomeSum { get; set; }

  [PXDBDecimal(4)]
  [PXDBScalar(typeof (Search4<GLHistoryNet.curyTranPtdNetIncome, Where<GLHistoryNet.branchID, Equal<GLHistory.branchID>, And<GLHistoryNet.ledgerID, Equal<GLHistory.ledgerID>, And<GLHistoryNet.accountID, Equal<CurrentValue<GLSetup.retEarnAccountID>>, And<GLHistoryNet.subID, Equal<GLHistory.subID>, And<GLHistoryNet.finPeriodID, Less<GLHistory.finPeriodID>>>>>>, Aggregate<Sum<GLHistoryNet.curyTranPtdNetIncome>>>))]
  public virtual Decimal? CuryTranPtdRetEarnSum { get; set; }

  public abstract class finPtdNetIncomeSum : 
    BqlType<IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistorySum.finPtdNetIncomeSum>
  {
  }

  public abstract class finPtdRetEarnSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistorySum.finPtdRetEarnSum>
  {
  }

  public abstract class tranPtdNetIncomeSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistorySum.tranPtdNetIncomeSum>
  {
  }

  public abstract class tranPtdRetEarnSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistorySum.tranPtdRetEarnSum>
  {
  }

  public abstract class curyFinPtdNetIncomeSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistorySum.curyFinPtdNetIncomeSum>
  {
  }

  public abstract class curyFinPtdRetEarnSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistorySum.curyFinPtdRetEarnSum>
  {
  }

  public abstract class curyTranPtdNetIncomeSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistorySum.curyTranPtdNetIncomeSum>
  {
  }

  public abstract class curyTranPtdRetEarnSum : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistorySum.curyTranPtdRetEarnSum>
  {
  }
}
