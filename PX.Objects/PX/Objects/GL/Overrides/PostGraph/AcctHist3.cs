// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Overrides.PostGraph.AcctHist3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL.Overrides.PostGraph;

[PXHidden]
[PXBreakInheritance]
[Serializable]
public class AcctHist3 : GLHistory
{
  public new abstract class ledgerID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  AcctHist3.ledgerID>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AcctHist3.branchID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AcctHist3.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AcctHist3.subID>
  {
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AcctHist3.finPeriodID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AcctHist3.curyID>
  {
  }

  public new abstract class detDeleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AcctHist3.detDeleted>
  {
  }

  public abstract class yearClosed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AcctHist3.yearClosed>
  {
  }

  public new abstract class finPtdCredit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist3.finPtdCredit>
  {
  }

  public new abstract class finPtdDebit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  AcctHist3.finPtdDebit>
  {
  }

  public new abstract class finYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist3.finYtdBalance>
  {
  }

  public new abstract class finBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist3.finBegBalance>
  {
  }

  public new abstract class finPtdRevalued : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist3.finPtdRevalued>
  {
  }

  public new abstract class tranPtdCredit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist3.tranPtdCredit>
  {
  }

  public new abstract class tranPtdDebit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist3.tranPtdDebit>
  {
  }

  public new abstract class tranYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist3.tranYtdBalance>
  {
  }

  public new abstract class tranBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist3.tranBegBalance>
  {
  }

  public new abstract class curyFinPtdCredit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist3.curyFinPtdCredit>
  {
  }

  public new abstract class curyFinPtdDebit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist3.curyFinPtdDebit>
  {
  }

  public new abstract class curyFinYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist3.curyFinYtdBalance>
  {
  }

  public new abstract class curyFinBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist3.curyFinBegBalance>
  {
  }

  public new abstract class curyTranPtdCredit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist3.curyTranPtdCredit>
  {
  }

  public new abstract class curyTranPtdDebit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist3.curyTranPtdDebit>
  {
  }

  public new abstract class curyTranYtdBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist3.curyTranYtdBalance>
  {
  }

  public new abstract class curyTranBegBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AcctHist3.curyTranBegBalance>
  {
  }
}
