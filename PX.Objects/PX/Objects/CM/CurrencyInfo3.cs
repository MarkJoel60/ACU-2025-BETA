// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CurrencyInfo3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CM;

[PXHidden]
[PXBreakInheritance]
[Serializable]
public class CurrencyInfo3 : CurrencyInfo
{
  public new abstract class curyInfoID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  CurrencyInfo3.curyInfoID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyInfo3.curyID>
  {
  }

  public new abstract class curyRateTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyInfo3.curyRateTypeID>
  {
  }

  public new abstract class curyEffDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CurrencyInfo3.curyEffDate>
  {
  }

  public new abstract class curyMultDiv : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyInfo3.curyMultDiv>
  {
  }

  public new abstract class curyRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CurrencyInfo3.curyRate>
  {
  }

  public new abstract class baseCuryID : IBqlField, IBqlOperand
  {
  }

  public new abstract class recipRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CurrencyInfo3.recipRate>
  {
  }

  public new abstract class baseCalc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CurrencyInfo3.baseCalc>
  {
  }

  public new abstract class curyPrecision : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CurrencyInfo3.curyPrecision>
  {
  }

  public abstract class tstamp : IBqlField, IBqlOperand
  {
  }
}
