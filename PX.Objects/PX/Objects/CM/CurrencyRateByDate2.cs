// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.CurrencyRateByDate2
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

[PXHidden]
[Serializable]
public class CurrencyRateByDate2 : CurrencyRate
{
  protected DateTime? _NextEffDate;

  [PXDate]
  [PXDBScalar(typeof (Search<CurrencyRate2.curyEffDate, Where<CurrencyRate2.fromCuryID, Equal<CurrencyRateByDate2.fromCuryID>, And<CurrencyRate2.toCuryID, Equal<CurrencyRateByDate2.toCuryID>, And<CurrencyRate2.curyRateType, Equal<CurrencyRateByDate2.curyRateType>, And<CurrencyRate2.curyEffDate, Greater<CurrencyRateByDate2.curyEffDate>>>>>, OrderBy<Asc<CurrencyRate2.curyEffDate>>>))]
  public virtual DateTime? NextEffDate
  {
    get => this._NextEffDate;
    set => this._NextEffDate = value;
  }

  public new class PK : PrimaryKeyOf<
  #nullable disable
  CurrencyRateByDate2>.By<CurrencyRateByDate2.curyRateID>
  {
    public static CurrencyRateByDate2 Find(PXGraph graph, long? curyRateID, PKFindOptions options = 0)
    {
      return (CurrencyRateByDate2) PrimaryKeyOf<CurrencyRateByDate2>.By<CurrencyRateByDate2.curyRateID>.FindBy(graph, (object) curyRateID, options);
    }
  }

  public new static class FK
  {
    public class FromCurrency : 
      PrimaryKeyOf<Currency>.By<Currency.curyID>.ForeignKeyOf<CurrencyRateByDate2>.By<CurrencyRateByDate2.fromCuryID>
    {
    }

    public class ToCurrency : 
      PrimaryKeyOf<Currency>.By<Currency.curyID>.ForeignKeyOf<CurrencyRateByDate2>.By<CurrencyRateByDate2.toCuryID>
    {
    }

    public class CurrencyRateType : 
      PrimaryKeyOf<CurrencyRateType>.By<CurrencyRateType.curyRateTypeID>.ForeignKeyOf<CurrencyRateByDate2>.By<CurrencyRateByDate2.curyRateType>
    {
    }
  }

  public new abstract class curyRateID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CurrencyRateByDate2.curyRateID>
  {
  }

  public new abstract class fromCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyRateByDate2.fromCuryID>
  {
  }

  public new abstract class toCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyRateByDate2.toCuryID>
  {
  }

  public new abstract class curyRateType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyRateByDate2.curyRateType>
  {
  }

  public new abstract class curyEffDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CurrencyRateByDate2.curyEffDate>
  {
  }

  public abstract class nextEffDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CurrencyRateByDate2.nextEffDate>
  {
  }

  public new abstract class curyMultDiv : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CurrencyRateByDate2.curyMultDiv>
  {
  }

  public new abstract class curyRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CurrencyRateByDate2.curyRate>
  {
  }
}
