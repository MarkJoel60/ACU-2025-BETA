// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CurrencyRateByDateForVendor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using System;

#nullable enable
namespace PX.Objects.CM;

/// <summary>
/// Acts as a view into the base <see cref="T:PX.Objects.CM.CurrencyRate" /> entity and provides the additional <see cref="P:PX.Objects.CM.CurrencyRateByDateForVendor.NextEffDate" /> field.
/// The DAC can be used to select a currency rate for a particular date:
/// The desired date must be between <see cref="T:PX.Objects.CM.CurrencyRateByDateForVendor.curyEffDate" /> (inclusive) and <see cref="P:PX.Objects.CM.CurrencyRateByDateForVendor.NextEffDate" /> (exclusive).
/// CurrencyRateByDateForVendor is different from CurrencyRateByDate by the presence of additional "Where" to optimize performance.
/// </summary>
[PXProjection(typeof (Select5<CurrencyRate, LeftJoin<CurrencyRate2, On<CurrencyRate2.fromCuryID, Equal<CurrencyRate.fromCuryID>, And<CurrencyRate2.toCuryID, Equal<CurrencyRate.toCuryID>, And<CurrencyRate2.curyRateType, Equal<CurrencyRate.curyRateType>, And<CurrencyRate2.curyEffDate, Greater<CurrencyRate.curyEffDate>>>>>>, Where<CurrencyRate.curyRateType, Equal<CurrentValue<Vendor.curyRateTypeID>>, And<CurrencyRate.toCuryID, Equal<CurrentValue<Vendor.curyID>>>>, Aggregate<GroupBy<CurrencyRate.curyRateID, Min<CurrencyRate2.curyEffDate>>>>))]
[PXCacheName("Currency Rate by Date")]
[Obsolete("This class has been deprecated and will be removed in Acumatica ERP 2018R2.")]
[Serializable]
public class CurrencyRateByDateForVendor : CurrencyRate
{
  [PXDBDate(BqlField = typeof (CurrencyRate2.curyEffDate))]
  public virtual DateTime? NextEffDate { get; set; }

  public new class PK : 
    PrimaryKeyOf<
    #nullable disable
    CurrencyRateByDateForVendor>.By<CurrencyRateByDateForVendor.curyRateID>
  {
    public static CurrencyRateByDateForVendor Find(
      PXGraph graph,
      long? curyRateID,
      PKFindOptions options = 0)
    {
      return (CurrencyRateByDateForVendor) PrimaryKeyOf<CurrencyRateByDateForVendor>.By<CurrencyRateByDateForVendor.curyRateID>.FindBy(graph, (object) curyRateID, options);
    }
  }

  public new static class FK
  {
    public class FromCurrency : 
      PrimaryKeyOf<Currency>.By<Currency.curyID>.ForeignKeyOf<CurrencyRateByDateForVendor>.By<CurrencyRateByDateForVendor.fromCuryID>
    {
    }

    public class ToCurrency : 
      PrimaryKeyOf<Currency>.By<Currency.curyID>.ForeignKeyOf<CurrencyRateByDateForVendor>.By<CurrencyRateByDateForVendor.toCuryID>
    {
    }

    public class CurrencyRateType : 
      PrimaryKeyOf<CurrencyRateType>.By<CurrencyRateType.curyRateTypeID>.ForeignKeyOf<CurrencyRateByDateForVendor>.By<CurrencyRateByDateForVendor.curyRateType>
    {
    }
  }

  public new abstract class curyRateID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CurrencyRateByDateForVendor.curyRateID>
  {
  }

  public new abstract class fromCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyRateByDateForVendor.fromCuryID>
  {
  }

  public new abstract class toCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyRateByDateForVendor.toCuryID>
  {
  }

  public new abstract class curyRateType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyRateByDateForVendor.curyRateType>
  {
  }

  public new abstract class curyEffDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CurrencyRateByDateForVendor.curyEffDate>
  {
  }

  public abstract class nextEffDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CurrencyRateByDateForVendor.nextEffDate>
  {
  }

  public new abstract class curyMultDiv : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyRateByDateForVendor.curyMultDiv>
  {
  }

  public new abstract class curyRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CurrencyRateByDateForVendor.curyRate>
  {
  }
}
