// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RMReportPeriods`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CS;

public class RMReportPeriods<T>
{
  private List<MasterFinPeriod> _finPeriods;
  private string _perWildcard;

  public RMReportPeriods(PXGraph graph)
  {
    this._perWildcard = new string('_', 6);
    ((IEnumerable<PXResult<MasterFinPeriod>>) PXSelectBase<MasterFinPeriod, PXSelectOrderBy<MasterFinPeriod, OrderBy<Asc<MasterFinPeriod.startDate>>>.Config>.Select(graph, Array.Empty<object>())).ToList<PXResult<MasterFinPeriod>>();
    this._finPeriods = new List<MasterFinPeriod>(graph.Caches[typeof (MasterFinPeriod)].Cached.Cast<MasterFinPeriod>());
  }

  public string PerWildcard => this._perWildcard;

  public List<MasterFinPeriod> FinPeriods => this._finPeriods;

  [Obsolete("This method is obsolete and will be removed in future versions of Acumatica. Use GetPeriodsForBeginningBalanceAmountOptimized instead")]
  public List<T> GetPeriodsForBeginningBalanceAmount(
    RMDataSourceGL dsGL,
    Dictionary<string, T> periodsForKey,
    bool limitToStartYear,
    out bool takeLast)
  {
    IReadOnlyCollection<T> balanceAmountOptimized = this.GetPeriodsForBeginningBalanceAmountOptimized(dsGL, periodsForKey, limitToStartYear, out takeLast);
    if (balanceAmountOptimized is List<T> beginningBalanceAmount)
      return beginningBalanceAmount;
    return balanceAmountOptimized == null ? (List<T>) null : EnumerableExtensions.ToList<T>((IEnumerable<T>) balanceAmountOptimized, balanceAmountOptimized.Count);
  }

  public IReadOnlyCollection<T> GetPeriodsForBeginningBalanceAmountOptimized(
    RMDataSourceGL dsGL,
    Dictionary<string, T> periodsForKey,
    bool limitToStartYear,
    out bool takeLast)
  {
    takeLast = false;
    if (dsGL.StartPeriod == null)
      return (IReadOnlyCollection<T>) periodsForKey.Values;
    string period = RMReportWildcard.EnsureWildcard(dsGL.StartPeriod, this._perWildcard);
    if (period.Contains(this._perWildcard))
      return (IReadOnlyCollection<T>) periodsForKey.Values;
    string finPeriod = this.GetFinPeriod(period, dsGL.StartPeriodYearOffset, dsGL.StartPeriodOffset);
    string key = !limitToStartYear ? RMReportPeriods<T>.GetMostRecentPeriodInList(periodsForKey, finPeriod) : RMReportPeriods<T>.GetMostRecentPeriodInList(periodsForKey, finPeriod.Substring(0, 4), finPeriod);
    if (string.IsNullOrEmpty(key))
      return (IReadOnlyCollection<T>) null;
    takeLast = finPeriod != key;
    return (IReadOnlyCollection<T>) new List<T>(1)
    {
      periodsForKey[key]
    };
  }

  [Obsolete("This method is obsolete and will be removed in future versions of Acumatica. Use GetPeriodsForEndingBalanceAmountOptimized instead")]
  public List<T> GetPeriodsForEndingBalanceAmount(
    RMDataSourceGL dsGL,
    Dictionary<string, T> periodsForKey,
    bool limitToEndYear)
  {
    IReadOnlyCollection<T> balanceAmountOptimized = this.GetPeriodsForEndingBalanceAmountOptimized(dsGL, periodsForKey, limitToEndYear);
    if (balanceAmountOptimized is List<T> endingBalanceAmount)
      return endingBalanceAmount;
    return balanceAmountOptimized == null ? (List<T>) null : EnumerableExtensions.ToList<T>((IEnumerable<T>) balanceAmountOptimized, balanceAmountOptimized.Count);
  }

  public IReadOnlyCollection<T> GetPeriodsForEndingBalanceAmountOptimized(
    RMDataSourceGL dsGL,
    Dictionary<string, T> periodsForKey,
    bool limitToEndYear)
  {
    if (dsGL.EndPeriod == null)
      return (IReadOnlyCollection<T>) periodsForKey.Values;
    string str = RMReportWildcard.EnsureWildcard(dsGL.EndPeriod, this._perWildcard);
    if (str.Contains<char>('_'))
      return (IReadOnlyCollection<T>) periodsForKey.Values;
    string finPeriod = this.GetFinPeriod(str, dsGL.EndPeriodYearOffset, dsGL.EndPeriodOffset);
    string key = !limitToEndYear ? RMReportPeriods<T>.GetMostRecentPeriodInList(periodsForKey, finPeriod) : RMReportPeriods<T>.GetMostRecentPeriodInList(periodsForKey, finPeriod.Substring(0, 4), finPeriod);
    if (string.IsNullOrEmpty(key))
      return (IReadOnlyCollection<T>) null;
    return (IReadOnlyCollection<T>) new List<T>(1)
    {
      periodsForKey[key]
    };
  }

  [Obsolete("This method is obsolete and will be removed in future versions of Acumatica. Use GetPeriodsForRegularAmountOptimized instead")]
  public List<T> GetPeriodsForRegularAmount(
    RMDataSourceGL dsGL,
    Dictionary<string, T> periodsForKey)
  {
    IReadOnlyCollection<T> regularAmountOptimized = this.GetPeriodsForRegularAmountOptimized(dsGL, periodsForKey);
    return !(regularAmountOptimized is List<T> objList) ? EnumerableExtensions.ToList<T>((IEnumerable<T>) regularAmountOptimized, regularAmountOptimized.Count) : objList;
  }

  public IReadOnlyCollection<T> GetPeriodsForRegularAmountOptimized(
    RMDataSourceGL dsGL,
    Dictionary<string, T> periodsForKey)
  {
    if (dsGL.StartPeriod == null)
      return (IReadOnlyCollection<T>) periodsForKey.Values;
    List<T> objList = (List<T>) null;
    string str1 = RMReportWildcard.EnsureWildcard(dsGL.StartPeriod, this._perWildcard);
    if (!str1.Contains(this._perWildcard) && dsGL.StartPeriodOffset.HasValue)
    {
      short? startPeriodOffset = dsGL.StartPeriodOffset;
      int? nullable = startPeriodOffset.HasValue ? new int?((int) startPeriodOffset.GetValueOrDefault()) : new int?();
      int num = 0;
      if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
        goto label_4;
    }
    short? periodYearOffset1 = dsGL.StartPeriodYearOffset;
    int? nullable1 = periodYearOffset1.HasValue ? new int?((int) periodYearOffset1.GetValueOrDefault()) : new int?();
    int num1 = 0;
    if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
      goto label_5;
label_4:
    str1 = this.GetFinPeriod(str1, dsGL.StartPeriodYearOffset, dsGL.StartPeriodOffset);
label_5:
    if (dsGL.EndPeriod == null)
    {
      if (!str1.Contains(this._perWildcard))
      {
        T obj;
        if (periodsForKey.TryGetValue(str1, out obj))
        {
          objList = new List<T>(1);
          objList.Add(obj);
        }
      }
      else
      {
        foreach (KeyValuePair<string, T> keyValuePair in periodsForKey)
        {
          if (RMReportWildcard.IsLike(str1, keyValuePair.Key))
          {
            objList = objList ?? new List<T>(4);
            objList.Add(keyValuePair.Value);
          }
        }
      }
    }
    else
    {
      string str2 = RMReportWildcard.EnsureWildcard(dsGL.EndPeriod, this._perWildcard);
      if (!str2.Contains<char>('_') && dsGL.EndPeriodOffset.HasValue)
      {
        short? endPeriodOffset = dsGL.EndPeriodOffset;
        int? nullable2 = endPeriodOffset.HasValue ? new int?((int) endPeriodOffset.GetValueOrDefault()) : new int?();
        int num2 = 0;
        if (!(nullable2.GetValueOrDefault() == num2 & nullable2.HasValue))
          goto label_18;
      }
      short? periodYearOffset2 = dsGL.EndPeriodYearOffset;
      int? nullable3 = periodYearOffset2.HasValue ? new int?((int) periodYearOffset2.GetValueOrDefault()) : new int?();
      int num3 = 0;
      if (nullable3.GetValueOrDefault() == num3 & nullable3.HasValue)
        goto label_19;
label_18:
      str2 = this.GetFinPeriod(str2, dsGL.EndPeriodYearOffset, dsGL.EndPeriodOffset);
label_19:
      string to = str2.Replace('_', '9');
      foreach (KeyValuePair<string, T> keyValuePair in periodsForKey)
      {
        if (RMReportWildcard.IsBetween(str1, to, keyValuePair.Key))
        {
          objList = objList ?? new List<T>(4);
          objList.Add(keyValuePair.Value);
        }
      }
    }
    return (IReadOnlyCollection<T>) objList ?? (IReadOnlyCollection<T>) Array.Empty<T>();
  }

  private static string GetMostRecentPeriodInList(
    Dictionary<string, T> list,
    string minPeriod,
    string maxPeriod)
  {
    string strB = string.Empty;
    foreach (string key in list.Keys)
    {
      if (string.Compare(key, minPeriod) >= 0 && string.Compare(key, maxPeriod) <= 0 && string.Compare(key, strB) > 0)
        strB = key;
    }
    return strB;
  }

  private static string GetMostRecentPeriodInList(Dictionary<string, T> list, string maxPeriod)
  {
    string strB = string.Empty;
    foreach (string key in list.Keys)
    {
      if (string.Compare(key, maxPeriod) <= 0 && string.Compare(key, strB) > 0)
        strB = key;
    }
    return strB;
  }

  /// <summary>
  /// Returns the financial period("{0:0000}{1:00}") by the current period("{0:0000}{1:00}") and offsets.
  /// Note: In different years can be a different number of financial periods.
  /// </summary>
  public string GetFinPeriod(string period, short? yearOffset, short? periodOffset)
  {
    string b = period;
    if (!string.IsNullOrEmpty(period) && period.Length == 6)
    {
      string s = period.Substring(0, 4);
      string strB = period.Substring(4);
      short? nullable1;
      int? nullable2;
      int? nullable3;
      if (yearOffset.HasValue)
      {
        short? nullable4 = yearOffset;
        int? nullable5 = nullable4.HasValue ? new int?((int) nullable4.GetValueOrDefault()) : new int?();
        int num1 = 0;
        if (!(nullable5.GetValueOrDefault() == num1 & nullable5.HasValue))
        {
          int num2 = int.Parse(s);
          nullable1 = yearOffset;
          nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
          nullable3 = nullable2.HasValue ? new int?(num2 + nullable2.GetValueOrDefault()) : new int?();
          string resYear = nullable3.ToString();
          List<MasterFinPeriod> list = this._finPeriods.Where<MasterFinPeriod>((Func<MasterFinPeriod, bool>) (f => string.Compare(f.FinYear, resYear.ToString()) == 0)).ToList<MasterFinPeriod>();
          if (list != null && list.Count > 0)
          {
            string periodNbr = list.Last<MasterFinPeriod>().PeriodNbr;
            b = string.Compare(periodNbr, strB) >= 0 ? $"{resYear:0000}{strB:00}" : $"{resYear:0000}{periodNbr:00}";
          }
          else
            b = $"{resYear:0000}{1:00}";
        }
      }
      if (periodOffset.HasValue)
      {
        nullable1 = periodOffset;
        int? nullable6;
        if (!nullable1.HasValue)
        {
          nullable3 = new int?();
          nullable6 = nullable3;
        }
        else
          nullable6 = new int?((int) nullable1.GetValueOrDefault());
        nullable2 = nullable6;
        int num3 = 0;
        if (!(nullable2.GetValueOrDefault() == num3 & nullable2.HasValue))
        {
          short num4 = 0;
          foreach (MasterFinPeriod finPeriod in this._finPeriods)
          {
            if (!string.Equals(finPeriod.FinPeriodID, b))
              ++num4;
            else
              break;
          }
          int num5 = (int) num4;
          short? nullable7 = periodOffset;
          int? nullable8 = nullable7.HasValue ? new int?((int) nullable7.GetValueOrDefault()) : new int?();
          short num6 = (short) (nullable8.HasValue ? new int?(num5 + nullable8.GetValueOrDefault()) : new int?()).Value;
          if (num6 < (short) 0)
          {
            b = $"{int.Parse(s) - 1:0000}{strB:00}";
          }
          else
          {
            short num7 = 0;
            foreach (MasterFinPeriod finPeriod in this._finPeriods)
            {
              if ((int) num7 == (int) num6)
              {
                b = finPeriod.FinPeriodID;
                break;
              }
              ++num7;
            }
          }
        }
      }
    }
    return b;
  }
}
