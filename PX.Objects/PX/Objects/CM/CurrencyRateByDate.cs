// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CurrencyRateByDate
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
/// Acts as a view into the base <see cref="T:PX.Objects.CM.CurrencyRate" /> entity and provides the additional <see cref="P:PX.Objects.CM.CurrencyRateByDate.NextEffDate" /> field.
/// The DAC can be used to select a currency rate for a particular date:
/// The desired date must be between <see cref="T:PX.Objects.CM.CurrencyRateByDate.curyEffDate" /> (inclusive) and <see cref="P:PX.Objects.CM.CurrencyRateByDate.NextEffDate" /> (exclusive).
/// </summary>
[PXProjection(typeof (Select5<CurrencyRate, LeftJoin<CurrencyRate2, On<CurrencyRate2.fromCuryID, Equal<CurrencyRate.fromCuryID>, And<CurrencyRate2.toCuryID, Equal<CurrencyRate.toCuryID>, And<CurrencyRate2.curyRateType, Equal<CurrencyRate.curyRateType>, And<CurrencyRate2.curyEffDate, Greater<CurrencyRate.curyEffDate>>>>>>, Where<CurrencyRate.fromCuryID, Equal<CurrentValue<CurrencyFilter.fromCuryID>>, And<CurrencyRate.toCuryID, Equal<CurrentValue<CurrencyFilter.toCuryID>>>>, Aggregate<GroupBy<CurrencyRate.curyRateID, Min<CurrencyRate2.curyEffDate>>>>))]
[PXCacheName("Currency Rate by Date")]
[Serializable]
public class CurrencyRateByDate : CurrencyRate
{
  [PXDBDate(BqlField = typeof (CurrencyRate2.curyEffDate))]
  public virtual DateTime? NextEffDate { get; set; }

  public new class PK : PrimaryKeyOf<
  #nullable disable
  CurrencyRateByDate>.By<CurrencyRateByDate.curyRateID>
  {
    public static CurrencyRateByDate Find(PXGraph graph, long? curyRateID, PKFindOptions options = 0)
    {
      return (CurrencyRateByDate) PrimaryKeyOf<CurrencyRateByDate>.By<CurrencyRateByDate.curyRateID>.FindBy(graph, (object) curyRateID, options);
    }
  }

  public new static class FK
  {
    public class FromCurrency : 
      PrimaryKeyOf<Currency>.By<Currency.curyID>.ForeignKeyOf<CurrencyRateByDate>.By<CurrencyRateByDate.fromCuryID>
    {
    }

    public class ToCurrency : 
      PrimaryKeyOf<Currency>.By<Currency.curyID>.ForeignKeyOf<CurrencyRateByDate>.By<CurrencyRateByDate.toCuryID>
    {
    }

    public class CurrencyRateType : 
      PrimaryKeyOf<CurrencyRateType>.By<CurrencyRateType.curyRateTypeID>.ForeignKeyOf<CurrencyRateByDate>.By<CurrencyRateByDate.curyRateType>
    {
    }
  }

  public new abstract class curyRateID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CurrencyRateByDate.curyRateID>
  {
  }

  public new abstract class fromCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyRateByDate.fromCuryID>
  {
  }

  public new abstract class toCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CurrencyRateByDate.toCuryID>
  {
  }

  public new abstract class curyRateType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyRateByDate.curyRateType>
  {
  }

  public new abstract class curyEffDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CurrencyRateByDate.curyEffDate>
  {
  }

  public abstract class nextEffDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CurrencyRateByDate.nextEffDate>
  {
  }

  public new abstract class curyMultDiv : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyRateByDate.curyMultDiv>
  {
  }

  public new abstract class curyRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CurrencyRateByDate.curyRate>
  {
  }
}
