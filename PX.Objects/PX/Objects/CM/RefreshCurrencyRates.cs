// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.RefreshCurrencyRates
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Permissions;

#nullable disable
namespace PX.Objects.CM;

public class RefreshCurrencyRates : PXGraph<RefreshCurrencyRates>
{
  public PXCancel<RefreshFilter> Cancel;
  public PXFilter<RefreshFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<RefreshRate, RefreshFilter> CurrencyRateList;

  protected virtual void RefreshFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    RefreshCurrencyRates.\u003C\u003Ec__DisplayClass3_0 cDisplayClass30 = new RefreshCurrencyRates.\u003C\u003Ec__DisplayClass3_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass30.filter = (RefreshFilter) e.Row;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass30.graph = GraphHelper.Clone<RefreshCurrencyRates>(this);
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass30.filter == null)
      return;
    // ISSUE: method pointer
    ((PXProcessingBase<RefreshRate>) this.CurrencyRateList).SetProcessDelegate(new PXProcessingBase<RefreshRate>.ProcessListDelegate((object) cDisplayClass30, __methodptr(\u003CRefreshFilter_RowSelected\u003Eb__0)));
  }

  protected virtual void RefreshFilter_CuryID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((PXSelectBase) this.CurrencyRateList).Cache.Clear();
  }

  protected virtual void RefreshFilter_CuryRateTypeID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((PXSelectBase) this.CurrencyRateList).Cache.Clear();
    if (e.Row == null)
      return;
    RefreshFilter row = (RefreshFilter) e.Row;
    CurrencyRateType currencyRateType = (CurrencyRateType) PXSelectorAttribute.Select<RefreshFilter.curyRateTypeID>(sender, (object) row);
    if (currencyRateType != null && currencyRateType.RefreshOnline.GetValueOrDefault())
      return;
    sender.RaiseExceptionHandling<RefreshFilter.curyRateTypeID>(e.Row, (object) row.CuryID, (Exception) new PXSetPropertyException("Currency Rate Types have not been configured for online rate refresh. To use this feature, you must enable online refresh from the Currency Rate Types (CM.20.10.00) screen.", (PXErrorLevel) 2));
  }

  protected virtual void RefreshFilter_CuryEffDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    DateTime? newValue = (DateTime?) e.NewValue;
    if (!newValue.HasValue || RefreshCurrencyRates.GetUtcSyncDate(newValue.Value) > DateTime.Now.ToUniversalTime().Date)
      throw new PXSetPropertyException("You must enter a valid date. Rates can be refreshed up to the current date.");
  }

  protected virtual IEnumerable currencyRateList()
  {
    RefreshCurrencyRates refreshCurrencyRates = this;
    foreach (PXResult<CurrencyList, CurrencyRateType> pxResult in PXSelectBase<CurrencyList, PXSelectJoin<CurrencyList, CrossJoin<CurrencyRateType>, Where<CurrencyRateType.refreshOnline, Equal<boolTrue>, And<CurrencyList.isActive, Equal<boolTrue>, And<CurrencyList.curyID, NotEqual<Current<RefreshFilter.curyID>>, And<Where<CurrencyRateType.curyRateTypeID, Equal<Current<RefreshFilter.curyRateTypeID>>, Or<Current<RefreshFilter.curyRateTypeID>, IsNull>>>>>>>.Config>.Select((PXGraph) refreshCurrencyRates, Array.Empty<object>()))
    {
      CurrencyList currencyList = PXResult<CurrencyList, CurrencyRateType>.op_Implicit(pxResult);
      CurrencyRateType currencyRateType = PXResult<CurrencyList, CurrencyRateType>.op_Implicit(pxResult);
      CurrencyRate currencyRate = PXResultset<CurrencyRate>.op_Implicit(PXSelectBase<CurrencyRate, PXViewOf<CurrencyRate>.BasedOn<SelectFromBase<CurrencyRate, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CurrencyRate.fromCuryID, Equal<P.AsString>>>>, And<BqlOperand<CurrencyRate.toCuryID, IBqlString>.IsEqual<BqlField<RefreshFilter.curyID, IBqlString>.FromCurrent>>>, And<BqlOperand<CurrencyRate.curyEffDate, IBqlDateTime>.IsLessEqual<BqlField<RefreshFilter.curyEffDate, IBqlDateTime>.FromCurrent>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CurrencyRate.curyRateType, Equal<BqlField<RefreshFilter.curyRateTypeID, IBqlString>.FromCurrent>>>>>.Or<BqlOperand<Current<RefreshFilter.curyRateTypeID>, IBqlString>.IsNull>>>.Order<By<BqlField<CurrencyRate.curyEffDate, IBqlDateTime>.Desc>>>.Config>.SelectSingleBound((PXGraph) refreshCurrencyRates, new object[0], new object[1]
      {
        (object) currencyList.CuryID
      }));
      RefreshRate refreshRate1 = new RefreshRate()
      {
        FromCuryID = currencyList.CuryID,
        CuryRateType = currencyRateType.CuryRateTypeID
      };
      RefreshRate refreshRate2 = (RefreshRate) ((PXSelectBase) refreshCurrencyRates.CurrencyRateList).Cache.Locate((object) refreshRate1);
      if (refreshRate2 == null)
        ((PXSelectBase) refreshCurrencyRates.CurrencyRateList).Cache.SetStatus((object) refreshRate1, (PXEntryStatus) 5);
      else
        refreshRate1 = refreshRate2;
      refreshRate1.OnlineRateAdjustment = currencyRateType.OnlineRateAdjustment;
      RefreshRate refreshRate3 = refreshRate1;
      Guid? noteId = refreshRate3.NoteID;
      noteId.GetValueOrDefault();
      if (!noteId.HasValue)
      {
        Guid guid = Guid.NewGuid();
        Guid? nullable;
        refreshRate3.NoteID = nullable = new Guid?(guid);
      }
      refreshRate1.CuryRate = (Decimal?) currencyRate?.CuryRate;
      yield return (object) refreshRate1;
    }
  }

  protected virtual string GetApiKey() => "9bced4075f0e47e084517b5b6b576ad2";

  /// <summary>
  /// Helper method to decide if the user wants the relevant currency rates on
  /// a particular UTC date, or just the "freshest" currency rates -
  /// in the latter case, we need to add the UTC offset to the date given
  /// to avoid currency server error.
  /// </summary>
  /// <param name="requestedDate">The date that the user wants to obtain the currency rates on.</param>
  private static DateTime GetUtcSyncDate(DateTime requestedDate)
  {
    requestedDate = requestedDate.Date;
    TimeSpan utcOffset = LocaleInfo.GetTimeZone().UtcOffset;
    DateTime universalTime = DateTime.Now.ToUniversalTime();
    DateTime date = (universalTime + utcOffset).Date;
    return requestedDate == date ? universalTime.Date : requestedDate.Date;
  }

  [WebPermission(SecurityAction.Assert, Unrestricted = true)]
  public virtual void RefreshRates(RefreshFilter filter, List<RefreshRate> list)
  {
    DateTime utcSyncDate = RefreshCurrencyRates.GetUtcSyncDate(filter.CuryEffDate.Value);
    Dictionary<string, Decimal> ratesFromService = this.GetRatesFromService(filter, list, utcSyncDate);
    CuryRateMaint instance = PXGraph.CreateInstance<CuryRateMaint>();
    ((PXSelectBase<CuryRateFilter>) instance.Filter).Current.ToCurrency = filter.CuryID;
    ((PXSelectBase<CuryRateFilter>) instance.Filter).Current.EffDate = new DateTime?(utcSyncDate);
    bool flag = false;
    for (int index = 0; index < list.Count; ++index)
    {
      RefreshRate refreshRate = list[index];
      if (ratesFromService.ContainsKey(refreshRate.FromCuryID))
      {
        CurrencyRate currencyRate = ((PXSelectBase<CurrencyRate>) instance.CuryRateRecordsEntry).Insert();
        currencyRate.FromCuryID = refreshRate.FromCuryID;
        currencyRate.ToCuryID = filter.CuryID;
        currencyRate.CuryRateType = refreshRate.CuryRateType;
        currencyRate.CuryRate = new Decimal?(ratesFromService[refreshRate.FromCuryID] * (1M + (refreshRate.OnlineRateAdjustment ?? 0M) / 100M));
        currencyRate.CuryMultDiv = "D";
        refreshRate.CuryRate = currencyRate.CuryRate;
        currencyRate.NoteID = refreshRate.NoteID;
        ((PXSelectBase<CurrencyRate>) instance.CuryRateRecordsEntry).Update(currencyRate);
        PXProcessing<RefreshRate>.SetInfo(index, "The record has been processed successfully.");
      }
      else
      {
        PXProcessing<RefreshRate>.SetError(index, PXMessages.LocalizeFormatNoPrefixNLA("No exchange rate could be found online for currency {0}.", new object[1]
        {
          (object) refreshRate.FromCuryID
        }));
        flag = true;
      }
    }
    ((PXGraph) instance).Actions.PressSave();
    if (flag)
      throw new PXOperationCompletedWithErrorException("Rates for one or more currencies couldn't be refreshed.");
  }

  /// <summary>Receive Currency Rates from external service</summary>
  /// <param name="filter">RefreshCurrency Rates Parameters (to get ToCurrency)</param>
  /// <param name="list">Rates to update (For overrides only: to switch services for different currencies etc.)</param>
  /// <param name="date">Date to pass to external service</param>
  /// <returns>Rate value for each currency returned by service</returns>
  public virtual Dictionary<string, Decimal> GetRatesFromService(
    RefreshFilter filter,
    List<RefreshRate> list,
    DateTime date)
  {
    string uriString = string.Format("http://openexchangerates.org/api/time-series.json?app_id={0}&base={1}&start={2:yyyy-MM-dd}&end={2:yyyy-MM-dd}", (object) this.GetApiKey(), (object) filter.CuryID, (object) date);
    PXTrace.WriteInformation("Refresh rates URL: " + uriString);
    string str = new WebClient().DownloadString(new Uri(uriString));
    return ((IEnumerable) ((JToken) ((JObject) JsonConvert.DeserializeObject(str) ?? throw new PXException("Error decoding response into valid JSON: {0}.", new object[1]
    {
      (object) str
    }))).SelectToken($"rates.{date:yyyy-MM-dd}", true).Children()).Cast<JProperty>().ToDictionary<JProperty, string, Decimal>((Func<JProperty, string>) (p => p.Name), (Func<JProperty, Decimal>) (p => Extensions.Value<Decimal>((IEnumerable<JToken>) p.Value)));
  }
}
