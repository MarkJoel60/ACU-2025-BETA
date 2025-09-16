// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Standalone.CurrencyInfoAlias2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CM.Standalone;

/// <summary>
/// An alias DAC for <see cref="T:PX.Objects.CM.CurrencyInfo" /> that can e.g. be used
/// to join <see cref="T:PX.Objects.CM.CurrencyInfo" /> twice in BQL queries.
/// </summary>
[PXHidden]
[Serializable]
public class CurrencyInfoAlias2 : CurrencyInfo
{
  public new abstract class curyInfoID : BqlType<IBqlLong, long>.Field<
  #nullable disable
  CurrencyInfoAlias2.curyInfoID>
  {
  }

  public new abstract class baseCalc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CurrencyInfoAlias2.baseCalc>
  {
  }

  public new abstract class baseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyInfoAlias2.baseCuryID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyInfoAlias2.curyID>
  {
  }

  public new abstract class displayCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyInfoAlias2.displayCuryID>
  {
  }

  public new abstract class curyRateTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyInfoAlias2.curyRateTypeID>
  {
  }

  public new abstract class curyEffDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CurrencyInfoAlias2.curyEffDate>
  {
  }

  public new abstract class curyMultDiv : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyInfoAlias2.curyMultDiv>
  {
  }

  public new abstract class curyRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CurrencyInfoAlias2.curyRate>
  {
  }

  public new abstract class recipRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CurrencyInfoAlias2.recipRate>
  {
  }

  public new abstract class sampleCuryRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CurrencyInfoAlias2.sampleCuryRate>
  {
  }

  public new abstract class sampleRecipRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CurrencyInfoAlias2.sampleRecipRate>
  {
  }

  public new abstract class curyPrecision : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CurrencyInfoAlias2.curyPrecision>
  {
  }

  public new abstract class basePrecision : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CurrencyInfoAlias2.basePrecision>
  {
  }

  public new abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CurrencyInfoAlias2.Tstamp>
  {
  }
}
