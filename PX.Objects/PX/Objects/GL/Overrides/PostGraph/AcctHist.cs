// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Overrides.PostGraph.AcctHist
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL.FinPeriods;
using System;

#nullable enable
namespace PX.Objects.GL.Overrides.PostGraph;

[PXHidden]
[AHAccum]
[PXDisableCloneAttributes]
[PXBreakInheritance]
[Serializable]
public class AcctHist : GLHistory
{
  [AcctHistDBInt(IsKey = true)]
  public override int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [AcctHistDBInt(IsKey = true)]
  public override int? LedgerID
  {
    get => this._LedgerID;
    set => this._LedgerID = value;
  }

  [AcctHistDBInt(IsKey = true)]
  public override int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [AcctHistDBInt(IsKey = true)]
  public override int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [AcctHistDBString(6, IsFixed = true, IsKey = true)]
  public override 
  #nullable disable
  string FinPeriodID { get; set; }

  [AcctHistDBString(1, IsFixed = true)]
  [AcctHistDefault(typeof (Ledger.balanceType))]
  public override string BalanceType
  {
    get => this._BalanceType;
    set => this._BalanceType = value;
  }

  [AcctHistDBString(5, IsUnicode = true)]
  public override string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  public override string FinYear
  {
    [PXDependsOnFields(new Type[] {typeof (AcctHist.finPeriodID)})] get
    {
      return FinPeriodUtils.FiscalYear(this.FinPeriodID);
    }
  }

  [AcctHistDBDecimal]
  public override Decimal? FinPtdCredit
  {
    get => this._FinPtdCredit;
    set => this._FinPtdCredit = value;
  }

  [AcctHistDBDecimal]
  public override Decimal? FinPtdDebit
  {
    get => this._FinPtdDebit;
    set => this._FinPtdDebit = value;
  }

  [AcctHistDBDecimal]
  public override Decimal? FinYtdBalance
  {
    get => this._FinYtdBalance;
    set => this._FinYtdBalance = value;
  }

  [AcctHistDBDecimal]
  public override Decimal? FinBegBalance
  {
    get => this._FinBegBalance;
    set => this._FinBegBalance = value;
  }

  [AcctHistDBDecimal]
  public override Decimal? FinPtdRevalued
  {
    get => this._FinPtdRevalued;
    set => this._FinPtdRevalued = value;
  }

  [AcctHistDBDecimal]
  public override Decimal? TranPtdCredit
  {
    get => this._TranPtdCredit;
    set => this._TranPtdCredit = value;
  }

  [AcctHistDBDecimal]
  public override Decimal? TranPtdDebit
  {
    get => this._TranPtdDebit;
    set => this._TranPtdDebit = value;
  }

  [AcctHistDBDecimal]
  public override Decimal? TranYtdBalance
  {
    get => this._TranYtdBalance;
    set => this._TranYtdBalance = value;
  }

  [AcctHistDBDecimal]
  public override Decimal? TranBegBalance
  {
    get => this._TranBegBalance;
    set => this._TranBegBalance = value;
  }

  [AcctHistDBDecimal]
  public override Decimal? CuryFinPtdCredit
  {
    get => this._CuryFinPtdCredit;
    set => this._CuryFinPtdCredit = value;
  }

  [AcctHistDBDecimal]
  public override Decimal? CuryFinPtdDebit
  {
    get => this._CuryFinPtdDebit;
    set => this._CuryFinPtdDebit = value;
  }

  [AcctHistDBDecimal]
  public override Decimal? CuryFinYtdBalance
  {
    get => this._CuryFinYtdBalance;
    set => this._CuryFinYtdBalance = value;
  }

  [AcctHistDBDecimal]
  public override Decimal? CuryFinBegBalance
  {
    get => this._CuryFinBegBalance;
    set => this._CuryFinBegBalance = value;
  }

  [AcctHistDBDecimal]
  public override Decimal? CuryTranPtdCredit
  {
    get => this._CuryTranPtdCredit;
    set => this._CuryTranPtdCredit = value;
  }

  [AcctHistDBDecimal]
  public override Decimal? CuryTranPtdDebit
  {
    get => this._CuryTranPtdDebit;
    set => this._CuryTranPtdDebit = value;
  }

  [AcctHistDBDecimal]
  public override Decimal? CuryTranYtdBalance
  {
    get => this._CuryTranYtdBalance;
    set => this._CuryTranYtdBalance = value;
  }

  [AcctHistDBDecimal]
  public override Decimal? CuryTranBegBalance
  {
    get => this._CuryTranBegBalance;
    set => this._CuryTranBegBalance = value;
  }

  public override bool? FinFlag
  {
    get => this._FinFlag;
    set => this._FinFlag = value;
  }

  public override Decimal? PtdCredit
  {
    get => !this._FinFlag.Value ? this._TranPtdCredit : this._FinPtdCredit;
    set
    {
      if (this._FinFlag.Value)
        this._FinPtdCredit = value;
      else
        this._TranPtdCredit = value;
    }
  }

  public override Decimal? PtdDebit
  {
    get => !this._FinFlag.Value ? this._TranPtdDebit : this._FinPtdDebit;
    set
    {
      if (this._FinFlag.Value)
        this._FinPtdDebit = value;
      else
        this._TranPtdDebit = value;
    }
  }

  public override Decimal? YtdBalance
  {
    get => !this._FinFlag.Value ? this._TranYtdBalance : this._FinYtdBalance;
    set
    {
      if (this._FinFlag.Value)
        this._FinYtdBalance = value;
      else
        this._TranYtdBalance = value;
    }
  }

  public override Decimal? BegBalance
  {
    get => !this._FinFlag.Value ? this._TranBegBalance : this._FinBegBalance;
    set
    {
      if (this._FinFlag.Value)
        this._FinBegBalance = value;
      else
        this._TranBegBalance = value;
    }
  }

  public override Decimal? CuryPtdCredit
  {
    get => !this._FinFlag.Value ? this._CuryTranPtdCredit : this._CuryFinPtdCredit;
    set
    {
      if (this._FinFlag.Value)
        this._CuryFinPtdCredit = value;
      else
        this._CuryTranPtdCredit = value;
    }
  }

  public override Decimal? CuryPtdDebit
  {
    get => !this._FinFlag.Value ? this._CuryTranPtdDebit : this._CuryFinPtdDebit;
    set
    {
      if (this._FinFlag.Value)
        this._CuryFinPtdDebit = value;
      else
        this._CuryTranPtdDebit = value;
    }
  }

  public override Decimal? CuryYtdBalance
  {
    get => !this._FinFlag.Value ? this._CuryTranYtdBalance : this._CuryFinYtdBalance;
    set
    {
      if (this._FinFlag.Value)
        this._CuryFinYtdBalance = value;
      else
        this._CuryTranYtdBalance = value;
    }
  }

  public override Decimal? CuryBegBalance
  {
    get => !this._FinFlag.Value ? this._CuryTranBegBalance : this._CuryFinBegBalance;
    set
    {
      if (this._FinFlag.Value)
        this._CuryFinBegBalance = value;
      else
        this._CuryTranBegBalance = value;
    }
  }

  [AcctHistDBTimestamp]
  public override byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AcctHist.finPeriodID>
  {
  }

  public new abstract class finYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AcctHist.finYear>
  {
  }

  public new abstract class detDeleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AcctHist.detDeleted>
  {
  }

  public new abstract class finYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist.finYtdBalance>
  {
  }

  public new abstract class finBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist.finBegBalance>
  {
  }

  public new abstract class tranYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist.tranYtdBalance>
  {
  }

  public new abstract class tranBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist.tranBegBalance>
  {
  }

  public new abstract class curyFinYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist.curyFinYtdBalance>
  {
  }

  public new abstract class curyFinBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist.curyFinBegBalance>
  {
  }

  public new abstract class curyTranYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist.curyTranYtdBalance>
  {
  }

  public new abstract class curyTranBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist.curyTranBegBalance>
  {
  }
}
