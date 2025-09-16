// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.Standalone.CurrencyInfoAlias
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
public class CurrencyInfoAlias : CurrencyInfo
{
  public new abstract class curyInfoID : BqlType<IBqlLong, long>.Field<
  #nullable disable
  CurrencyInfoAlias.curyInfoID>
  {
  }

  public new abstract class baseCalc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CurrencyInfoAlias.baseCalc>
  {
  }

  public new abstract class baseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyInfoAlias.baseCuryID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyInfoAlias.curyID>
  {
  }

  public new abstract class displayCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyInfoAlias.displayCuryID>
  {
  }

  public new abstract class curyRateTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyInfoAlias.curyRateTypeID>
  {
  }

  public new abstract class curyEffDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CurrencyInfoAlias.curyEffDate>
  {
  }

  public new abstract class curyMultDiv : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyInfoAlias.curyMultDiv>
  {
  }

  public new abstract class curyRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CurrencyInfoAlias.curyRate>
  {
  }

  public new abstract class recipRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CurrencyInfoAlias.recipRate>
  {
  }

  public new abstract class sampleCuryRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CurrencyInfoAlias.sampleCuryRate>
  {
  }

  public new abstract class sampleRecipRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CurrencyInfoAlias.sampleRecipRate>
  {
  }

  public new abstract class curyPrecision : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CurrencyInfoAlias.curyPrecision>
  {
  }

  public new abstract class basePrecision : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    CurrencyInfoAlias.basePrecision>
  {
  }

  public new abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CurrencyInfoAlias.Tstamp>
  {
  }
}
