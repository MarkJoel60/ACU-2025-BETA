// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.CurrencyRatesProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.TX;

public class CurrencyRatesProvider
{
  private Dictionary<string, Dictionary<DateTime, CurrencyRate>> _rates;
  private string _toCuryID;
  private string _rateType;

  public CurrencyRatesProvider(string rateTypeID, string toCuryID)
  {
    if (string.IsNullOrEmpty(rateTypeID))
      throw new ArgumentNullException(nameof (rateTypeID));
    if (string.IsNullOrEmpty(toCuryID))
      throw new ArgumentNullException(nameof (toCuryID));
    this._rateType = rateTypeID;
    this._toCuryID = toCuryID;
    this._rates = new Dictionary<string, Dictionary<DateTime, CurrencyRate>>();
  }

  private DateTime GetLowerDateBoundForRatesSelection(
    PXGraph graph,
    IEnumerable<string> fromCuryIDs,
    DateTime minDate)
  {
    return GraphHelper.RowCast<CurrencyRate>((IEnumerable) PXSelectBase<CurrencyRate, PXSelectGroupBy<CurrencyRate, Where<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>, And<CurrencyRate.fromCuryID, In<Required<CurrencyRate.fromCuryID>>, And<CurrencyRate.curyRateType, Equal<Required<CurrencyRate.curyRateType>>, And<CurrencyRate.curyEffDate, LessEqual<Required<CurrencyRate.curyEffDate>>>>>>, Aggregate<GroupBy<CurrencyRate.fromCuryID, Max<CurrencyRate.curyEffDate>>>>.Config>.Select(graph, new object[4]
    {
      (object) this._toCuryID,
      (object) fromCuryIDs.ToArray<string>(),
      (object) this._rateType,
      (object) minDate
    })).Select<CurrencyRate, DateTime?>((Func<CurrencyRate, DateTime?>) (cr => cr.CuryEffDate)).Min<DateTime?>() ?? minDate;
  }

  public void Fill(
    PXGraph graph,
    IEnumerable<string> fromCuryIDs,
    DateTime minDate,
    DateTime maxDate)
  {
    fromCuryIDs = (IEnumerable<string>) new HashSet<string>(fromCuryIDs);
    if (!fromCuryIDs.Any<string>((Func<string, bool>) (c => c != null)))
      return;
    foreach (string fromCuryId in fromCuryIDs)
      this._rates[fromCuryId] = new Dictionary<DateTime, CurrencyRate>();
    DateTime forRatesSelection = this.GetLowerDateBoundForRatesSelection(graph, fromCuryIDs, minDate);
    using (IEnumerator<PXResult<CurrencyRate>> enumerator = PXSelectBase<CurrencyRate, PXSelect<CurrencyRate, Where<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>, And<CurrencyRate.fromCuryID, In<Required<CurrencyRate.fromCuryID>>, And<CurrencyRate.curyRateType, Equal<Required<CurrencyRate.curyRateType>>, And<Where<CurrencyRate.curyEffDate, GreaterEqual<Required<CurrencyRate.curyEffDate>>, And<CurrencyRate.curyEffDate, LessEqual<Required<CurrencyRate.curyEffDate>>, Or<CurrencyRate.curyEffDate, IsNull>>>>>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>.Config>.Select(graph, new object[5]
    {
      (object) this._toCuryID,
      (object) fromCuryIDs.ToArray<string>(),
      (object) this._rateType,
      (object) forRatesSelection,
      (object) maxDate
    }).GetEnumerator())
    {
label_18:
      while (enumerator.MoveNext())
      {
        CurrencyRate currencyRate = PXResult<CurrencyRate>.op_Implicit(enumerator.Current);
        DateTime? curyEffDate = currencyRate.CuryEffDate;
        DateTime dateTime1;
        if (curyEffDate.HasValue)
        {
          curyEffDate = currencyRate.CuryEffDate;
          DateTime dateTime2 = minDate;
          if ((curyEffDate.HasValue ? (curyEffDate.GetValueOrDefault() < dateTime2 ? 1 : 0) : 0) == 0)
          {
            curyEffDate = currencyRate.CuryEffDate;
            dateTime1 = curyEffDate.Value;
            goto label_15;
          }
        }
        dateTime1 = minDate;
label_15:
        DateTime key = dateTime1;
        while (true)
        {
          if (!this._rates[currencyRate.FromCuryID].ContainsKey(key) && key <= maxDate)
          {
            this._rates[currencyRate.FromCuryID][key] = currencyRate;
            key = key.AddDays(1.0);
          }
          else
            goto label_18;
        }
      }
    }
  }

  public CurrencyRate GetRate(string fromCuryID, DateTime date)
  {
    if (this._toCuryID == fromCuryID)
      return new CurrencyRate()
      {
        FromCuryID = fromCuryID,
        ToCuryID = this._toCuryID,
        CuryRate = new Decimal?(1.0M),
        RateReciprocal = new Decimal?(1.0M),
        CuryMultDiv = "M",
        CuryEffDate = new DateTime?(date),
        CuryRateType = this._rateType
      };
    if (!this._rates.ContainsKey(fromCuryID))
      throw new PXException("Rates for the {0} currency have not been specified on the Currency Rates (CM301000) form. To proceed, specify the rates.", new object[1]
      {
        (object) fromCuryID
      });
    if (this._rates[fromCuryID].Count == 0)
      return (CurrencyRate) null;
    try
    {
      return this._rates[fromCuryID][date];
    }
    catch (Exception ex)
    {
      throw new PXException("No exchange rate from {0} to {1} has been found for the date {2}. Specify the rate on the Currency Rates (CM301000) form.", new object[3]
      {
        (object) fromCuryID,
        (object) this._toCuryID,
        (object) date.ToShortDateString()
      });
    }
  }
}
