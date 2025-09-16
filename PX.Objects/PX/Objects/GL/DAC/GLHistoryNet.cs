// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.DAC.GLHistoryNet
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.GL.DAC;

[PXProjection(typeof (Select<GLHistory>))]
[PXHidden]
public class GLHistoryNet : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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

  [PXBaseCury]
  [PXDBCalced(typeof (BqlOperand<GLHistory.finPtdCredit, IBqlDecimal>.Subtract<GLHistory.finPtdDebit>), typeof (Decimal))]
  public virtual Decimal? FinPtdNetIncome { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (BqlOperand<GLHistory.tranPtdCredit, IBqlDecimal>.Subtract<GLHistory.tranPtdDebit>), typeof (Decimal))]
  public virtual Decimal? TranPtdNetIncome { get; set; }

  [PXDBBaseCury(typeof (GLHistory.ledgerID), BqlField = typeof (GLHistory.curyFinPtdCredit))]
  public virtual Decimal? CuryFinPtdCredit { get; set; }

  [PXDBBaseCury(typeof (GLHistory.ledgerID), BqlField = typeof (GLHistory.curyFinPtdDebit))]
  public virtual Decimal? CuryFinPtdDebit { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (BqlOperand<GLHistory.curyFinPtdCredit, IBqlDecimal>.Subtract<GLHistory.curyFinPtdDebit>), typeof (Decimal))]
  public virtual Decimal? CuryFinPtdNetIncome { get; set; }

  [PXBaseCury]
  [PXDBCalced(typeof (BqlOperand<GLHistory.curyTranPtdCredit, IBqlDecimal>.Subtract<GLHistory.curyTranPtdDebit>), typeof (Decimal))]
  public virtual Decimal? CuryTranPtdNetIncome { get; set; }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryNet.branchID>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryNet.ledgerID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryNet.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistoryNet.subID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLHistoryNet.finPeriodID>
  {
  }

  public abstract class finPtdCredit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLHistoryNet.finPtdCredit>
  {
  }

  public abstract class finPtdDebit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLHistoryNet.finPtdDebit>
  {
  }

  public abstract class finPtdNetIncome : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryNet.finPtdNetIncome>
  {
  }

  public abstract class tranPtdNetIncome : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryNet.tranPtdNetIncome>
  {
  }

  public abstract class curyFinPtdCredit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryNet.curyFinPtdCredit>
  {
  }

  public abstract class curyFinPtdDebit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryNet.curyFinPtdDebit>
  {
  }

  public abstract class curyFinPtdNetIncome : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryNet.curyFinPtdNetIncome>
  {
  }

  public abstract class curyTranPtdNetIncome : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistoryNet.curyTranPtdNetIncome>
  {
  }
}
