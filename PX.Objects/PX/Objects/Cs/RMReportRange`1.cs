// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RMReportRange`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CS;

public class RMReportRange<T> : RMReportRange
{
  private T _instance;
  private PXCache _cache;
  private Dictionary<string, List<T>> _ranges;
  private Dictionary<string, HashSet<T>> _rangeSegments;
  private string _wildcard;
  private RMReportConstants.WildcardMode _wildcardMode;
  private RMReportConstants.BetweenMode _betweenMode;

  public RMReportRange(
    PXGraph graph,
    string dimensionName,
    RMReportConstants.WildcardMode wildcardMode,
    RMReportConstants.BetweenMode betweenMode)
  {
    this._cache = graph.Caches[typeof (T)];
    this._instance = (T) this._cache.CreateInstance();
    this._ranges = new Dictionary<string, List<T>>();
    this._rangeSegments = new Dictionary<string, HashSet<T>>();
    Dimension dimension = PXResultset<Dimension>.op_Implicit(PXSelectBase<Dimension, PXSelect<Dimension, Where<Dimension.dimensionID, Equal<Required<Dimension.dimensionID>>>>.Config>.Select(graph, new object[1]
    {
      (object) dimensionName
    }));
    if (dimension != null)
    {
      short? length = dimension.Length;
      if (length.HasValue)
      {
        length = dimension.Length;
        this._wildcard = new string('_', (int) length.Value);
        goto label_4;
      }
    }
    this._wildcard = "";
label_4:
    this._wildcardMode = wildcardMode;
    this._betweenMode = betweenMode;
  }

  public T Instance => this._instance;

  public string Wildcard => this._wildcard;

  public PXCache Cache => this._cache;

  public List<T> GetItemsInRange(
    string range,
    Func<T, string> getValuePredicate,
    Action<T, string> prepareForLocatePredicate)
  {
    return this.GetItemsInRange(range, (Func<string, string>) (r => r), (Func<string, HashSet<T>>) (r => this.GetItems(r, getValuePredicate, prepareForLocatePredicate)));
  }

  public List<T> GetItemsInRange(
    string range,
    Func<string, string> cacheKey,
    Func<string, HashSet<T>> getItemsInRangePredicate)
  {
    List<T> itemsInRange = (List<T>) null;
    if (this._ranges.TryGetValue(cacheKey(range ?? ""), out itemsInRange))
      return itemsInRange;
    HashSet<T> collection = (HashSet<T>) null;
    string[] strArray1;
    if (!string.IsNullOrEmpty(range))
      strArray1 = range.Split('|');
    else
      strArray1 = new string[1]{ string.Empty };
    string[] strArray2 = strArray1;
    foreach (string str in strArray2)
    {
      HashSet<T> objSet;
      if (!this._rangeSegments.TryGetValue(cacheKey(str), out objSet))
      {
        objSet = getItemsInRangePredicate(str);
        this._rangeSegments.Add(cacheKey(str), objSet);
      }
      if (strArray2.Length == 1)
        collection = objSet;
      else if (collection == null)
        collection = new HashSet<T>((IEnumerable<T>) objSet);
      else
        collection.IntersectWith((IEnumerable<T>) objSet);
    }
    itemsInRange = new List<T>((IEnumerable<T>) collection);
    this._ranges.Add(cacheKey(range ?? ""), itemsInRange);
    return itemsInRange;
  }

  private HashSet<T> GetItems(
    string range,
    Func<T, string> getValuePredicate,
    Action<T, string> prepareForLocatePredicate)
  {
    HashSet<T> items1 = this.GetItems(range, (string) null, getValuePredicate, prepareForLocatePredicate);
    // ISSUE: explicit non-virtual call
    if (items1 != null && __nonvirtual (items1.Count) > 0)
      return items1;
    string[] strArray = range.Split(':');
    if (strArray != null && strArray.Length == 2)
    {
      HashSet<T> items2 = this.GetItems(strArray[0], strArray[1], getValuePredicate, prepareForLocatePredicate);
      // ISSUE: explicit non-virtual call
      if (items2 != null && __nonvirtual (items2.Count) > 0)
        return items2;
    }
    HashSet<T> items3 = new HashSet<T>();
    string str = range;
    char[] chArray = new char[1]{ ',' };
    foreach (string range1 in str.Split(chArray))
    {
      string start;
      string end;
      RMReportRange.ParseRangeStartEndPair(range1, out start, out end);
      items3.UnionWith((IEnumerable<T>) this.GetItems(start, end, getValuePredicate, prepareForLocatePredicate));
    }
    return items3;
  }

  private HashSet<T> GetItems(
    string start,
    string end,
    Func<T, string> getValuePredicate,
    Action<T, string> prepareForLocatePredicate)
  {
    HashSet<T> items = new HashSet<T>();
    if (!string.IsNullOrEmpty(start))
    {
      if (string.IsNullOrEmpty(end) || end == start)
      {
        string itemCode = string.Empty;
        if (this._wildcardMode == RMReportConstants.WildcardMode.Fixed)
        {
          itemCode = RMReportWildcard.EnsureWildcardForFixed(start, this._wildcard);
        }
        else
        {
          if (this._wildcardMode != RMReportConstants.WildcardMode.Normal)
            throw new ArgumentException("WildcardMode is invalid.");
          itemCode = RMReportWildcard.EnsureWildcard(start, this._wildcard);
        }
        if (itemCode.Contains<char>('_'))
        {
          items.UnionWith(this._cache.Cached.Cast<T>().Where<T>((Func<T, bool>) (x => RMReportWildcard.IsLike(itemCode, getValuePredicate(x)))));
        }
        else
        {
          prepareForLocatePredicate(this._instance, itemCode);
          if (this._cache.IsKeysFilled((object) this._instance))
          {
            T obj = (T) this._cache.Locate((object) this._instance);
            if ((object) obj != null)
              items.Add(obj);
          }
          else
            items.UnionWith(this._cache.Cached.Cast<T>().Where<T>((Func<T, bool>) (x => string.Equals(itemCode, getValuePredicate(x), StringComparison.Ordinal))));
        }
      }
      else if (this._betweenMode == RMReportConstants.BetweenMode.ByChar)
      {
        items.UnionWith(this._cache.Cached.Cast<T>().Where<T>((Func<T, bool>) (x => RMReportWildcard.IsBetweenByChar(start, end, getValuePredicate(x)))));
      }
      else
      {
        if (this._betweenMode != RMReportConstants.BetweenMode.Fixed)
          throw new ArgumentException("BetweenMode is invalid.");
        items.UnionWith(RMReportWildcard.GetBetweenForFixed<T>(start, end, this._wildcard, this._cache.Cached, getValuePredicate));
      }
    }
    else
      items.UnionWith(this._cache.Cached.Cast<T>());
    return items;
  }
}
