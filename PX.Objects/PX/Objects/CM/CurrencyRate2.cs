// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CurrencyRate2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CM;

/// <summary>
/// An alias for the <see cref="T:PX.Objects.CM.CurrencyRate" /> DAC.
/// </summary>
[PXCacheName("Effective Currency Rate")]
[Serializable]
public class CurrencyRate2 : CurrencyRate
{
  public new class PK : PrimaryKeyOf<
  #nullable disable
  CurrencyRate2>.By<CurrencyRate2.curyRateID>
  {
    public static CurrencyRate2 Find(PXGraph graph, long? curyRateID, PKFindOptions options = 0)
    {
      return (CurrencyRate2) PrimaryKeyOf<CurrencyRate2>.By<CurrencyRate2.curyRateID>.FindBy(graph, (object) curyRateID, options);
    }
  }

  public new static class FK
  {
    public class FromCurrency : 
      PrimaryKeyOf<Currency>.By<Currency.curyID>.ForeignKeyOf<CurrencyRate2>.By<CurrencyRate2.fromCuryID>
    {
    }

    public class ToCurrency : 
      PrimaryKeyOf<Currency>.By<Currency.curyID>.ForeignKeyOf<CurrencyRate2>.By<CurrencyRate2.toCuryID>
    {
    }

    public class CurrencyRateType : 
      PrimaryKeyOf<CurrencyRateType>.By<CurrencyRateType.curyRateTypeID>.ForeignKeyOf<CurrencyRate2>.By<CurrencyRate2.curyRateType>
    {
    }
  }

  public new abstract class curyRateID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CurrencyRate2.curyRateID>
  {
  }

  public new abstract class curyEffDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CurrencyRate2.curyEffDate>
  {
  }

  public new abstract class fromCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyRate2.fromCuryID>
  {
  }

  public new abstract class toCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyRate2.toCuryID>
  {
  }

  public new abstract class curyRateType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyRate2.curyRateType>
  {
  }
}
