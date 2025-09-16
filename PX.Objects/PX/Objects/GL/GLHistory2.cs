// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLHistory2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXHidden]
public class GLHistory2 : GLHistory
{
  public new abstract class ledgerID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  GLHistory2.ledgerID>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistory2.branchID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistory2.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLHistory2.subID>
  {
  }

  public new abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLHistory2.finPeriodID>
  {
  }

  public new abstract class finPtdCredit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistory2.finPtdCredit>
  {
  }

  public new abstract class finPtdDebit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLHistory2.finPtdDebit>
  {
  }

  public new abstract class curyFinPtdCredit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistory2.curyFinPtdCredit>
  {
  }

  public new abstract class curyFinPtdDebit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistory2.curyFinPtdDebit>
  {
  }

  public new abstract class tranPtdCredit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistory2.tranPtdCredit>
  {
  }

  public new abstract class tranPtdDebit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistory2.tranPtdDebit>
  {
  }

  public new abstract class curyTranPtdCredit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistory2.curyTranPtdCredit>
  {
  }

  public new abstract class curyTranPtdDebit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLHistory2.curyTranPtdDebit>
  {
  }
}
