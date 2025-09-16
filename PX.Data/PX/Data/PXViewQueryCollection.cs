// Decompiled with JetBrains decompiler
// Type: PX.Data.PXViewQueryCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <exclude />
[Serializable]
public sealed class PXViewQueryCollection
{
  public string ViewName;
  public bool IsViewReadonly;
  public System.Type CacheType;
  public System.Type[] CacheTypes;
  public bool CacheTypesInitialized;
  public bool IsCommandMutable;
  private ConcurrentDictionary<PXCommandKey, PXQueryResult> Items = new ConcurrentDictionary<PXCommandKey, PXQueryResult>();
  [NonSerialized]
  private ConcurrentDictionary<PXCommandKey, PXQueryResult> Prepared = new ConcurrentDictionary<PXCommandKey, PXQueryResult>();
  public ConcurrentDictionary<PXCommandKey, SelectCacheEntry> ParametersCache = new ConcurrentDictionary<PXCommandKey, SelectCacheEntry>();
  private static int _minItems = WebConfig.MinQueryCacheSize;
  private static int _maxItems = WebConfig.MaxQueryCacheSize;
  private static int _maxSingleRowItems = WebConfig.MaxSingleRowQueryCacheSize;
  private static int _maxUnloadItems = WebConfig.MaxUnloadQueryCacheSize;
  private static int _maxQueryRowsCacheSize = WebConfig.MaxQueryRowsCacheSize;

  [System.Runtime.Serialization.OnDeserializing]
  private void OnDeserializing(StreamingContext context)
  {
    this.ParametersCache = new ConcurrentDictionary<PXCommandKey, SelectCacheEntry>();
    this.Prepared = new ConcurrentDictionary<PXCommandKey, PXQueryResult>();
  }

  public void RemoveExpired()
  {
    PXDatabaseProvider provider = PXDatabase.Provider;
    foreach (KeyValuePair<PXCommandKey, PXQueryResult> keyValuePair in this.Items)
    {
      if ((keyValuePair.Value.RequestOnly ? 1 : (keyValuePair.Value.IsExpired(provider) ? 1 : 0)) != 0)
        this.Items.TryRemove(keyValuePair.Key, out PXQueryResult _);
    }
  }

  internal void Unload()
  {
    this.Prepared = new ConcurrentDictionary<PXCommandKey, PXQueryResult>();
    PXDatabaseProvider provider = PXDatabase.Provider;
    foreach (KeyValuePair<PXCommandKey, PXQueryResult> keyValuePair in this.Items)
    {
      if ((keyValuePair.Value.RequestOnly ? 1 : (keyValuePair.Value.IsExpired(provider) ? 1 : 0)) != 0 || keyValuePair.Value.Items.Count > PXViewQueryCollection._maxQueryRowsCacheSize)
        this.Items.TryRemove(keyValuePair.Key, out PXQueryResult _);
    }
    this.CheckSize(PXViewQueryCollection._minItems, PXViewQueryCollection._maxUnloadItems);
  }

  public void Clear()
  {
    this.Items.Clear();
    this.ParametersCache.Clear();
    this.Prepared.Clear();
  }

  public void ClearPreparedResults() => this.Prepared.Clear();

  public IEnumerable<PXQueryResult> Values
  {
    get
    {
      foreach (KeyValuePair<PXCommandKey, PXQueryResult> keyValuePair in this.Items)
        yield return keyValuePair.Value;
    }
  }

  public IEnumerable<PXCommandKey> Keys
  {
    get
    {
      foreach (KeyValuePair<PXCommandKey, PXQueryResult> keyValuePair in this.Items)
        yield return keyValuePair.Key;
    }
  }

  public override string ToString() => $"{this.CacheType.Name} {this.Items.Count} items";

  public void SetResult(PXView view, PXCommandKey queryKey, List<object> items)
  {
    if (!this.CacheTypesInitialized)
    {
      if (items.Count > 0 && items[0] is PXResult)
      {
        this.CacheTypes = ((PXResult) items[0]).Tables;
        this.CacheTypesInitialized = true;
      }
      else if (items.Count > 0)
        this.CacheTypesInitialized = true;
    }
    PXQueryResult pxQueryResult;
    if (this.Prepared.TryGetValue(queryKey, out pxQueryResult))
    {
      pxQueryResult.Items = items;
    }
    else
    {
      string[] dbTableNames = new string[0];
      PXQueryResult addValue = new PXQueryResult(dbTableNames, dbTableNames.Length == 0)
      {
        Items = items
      };
      this.Prepared.AddOrUpdate(queryKey, addValue, (Func<PXCommandKey, PXQueryResult, PXQueryResult>) ((k, q) =>
      {
        q.Items = items;
        return q;
      }));
    }
  }

  internal PXCommandKey GetAltCommandKey(object[] queryKey)
  {
    return new PXCommandKey(queryKey, false, new bool?(false));
  }

  internal void StoreCached(
    PXView view,
    PXCommandKey queryKey,
    List<object> items,
    PXSelectOperationContext context)
  {
    PXCommandKey altCommandKey = this.GetAltCommandKey(queryKey.GetParameters());
    if (this.Prepared.ContainsKey(altCommandKey))
    {
      this.SetResult(view, altCommandKey, items);
    }
    else
    {
      if (this.IsCommandMutable && queryKey.CommandText == null && !queryKey.BadParamsQueryNotExecuted)
        queryKey.CommandText = view.ToString();
      if (!this.CacheTypesInitialized)
      {
        if (items.Count > 0 && items[0] is PXResult)
        {
          this.CacheTypes = ((PXResult) items[0]).Tables;
          this.CacheTypesInitialized = true;
        }
        else if (items.Count > 0)
          this.CacheTypesInitialized = true;
      }
      PXQueryResult pxQueryResult1;
      PXQueryResult pxQueryResult2;
      if (this.Items.TryGetValue(queryKey, out pxQueryResult1))
      {
        if (queryKey.BadParamsQueryNotExecuted)
        {
          int count = items.Count;
        }
        pxQueryResult1.Items = items;
      }
      else if (context != null && context.BadParametersQueryNotExecuted)
      {
        int count = items.Count;
        queryKey.BadParamsQueryNotExecuted = true;
        string[] strArray = new string[0];
        string[] dbTableNames = context.LastSqlTables == null ? PXViewQueryCollection.GetSqlTables((IEnumerable<System.Type>) view.BqlSelect.GetTables(), view.Graph) : context.LastSqlTables.ToArray();
        PXQueryResult addValue = new PXQueryResult(dbTableNames, dbTableNames.Length == 0)
        {
          Items = items
        };
        addValue.BadParamsSkipMergeCache = context.BadParametersSkipMergeCache;
        this.Items.AddOrUpdate(queryKey, addValue, (Func<PXCommandKey, PXQueryResult, PXQueryResult>) ((k, q) =>
        {
          q.Items = items;
          return q;
        }));
        int? maximumRows = queryKey._MaximumRows;
        int num = 2;
        if (maximumRows.GetValueOrDefault() > num & maximumRows.HasValue)
        {
          PXCommandKey key = queryKey.ClonePrevKey();
          if (this.Items.ContainsKey(key))
            this.Items.TryRemove(key, out pxQueryResult2);
        }
        this.CheckSize(PXViewQueryCollection._minItems, PXViewQueryCollection._maxItems);
      }
      else
      {
        if (queryKey.CommandText == null)
        {
          this.IsCommandMutable = context != null && context.LastCommandMutable;
          if (this.IsCommandMutable)
            queryKey.CommandText = view.ToString();
          if (this.Items.TryGetValue(queryKey, out pxQueryResult1))
          {
            pxQueryResult1.Items = items;
            return;
          }
        }
        string[] dbTableNames = new string[0];
        if (context?.LastSqlTables != null)
          dbTableNames = context.LastSqlTables.ToArray();
        PXQueryResult addValue = new PXQueryResult(dbTableNames, dbTableNames.Length == 0)
        {
          Items = items
        };
        this.Items.AddOrUpdate(queryKey, addValue, (Func<PXCommandKey, PXQueryResult, PXQueryResult>) ((k, q) =>
        {
          q.Items = items;
          return q;
        }));
        int? maximumRows = queryKey._MaximumRows;
        int num = 2;
        if (maximumRows.GetValueOrDefault() > num & maximumRows.HasValue)
        {
          PXCommandKey key = queryKey.ClonePrevKey();
          if (this.Items.ContainsKey(key))
            this.Items.TryRemove(key, out pxQueryResult2);
        }
        this.CheckSize(PXViewQueryCollection._minItems, PXViewQueryCollection._maxItems);
      }
    }
  }

  private void CheckSize(int minItems, int maxItems)
  {
    if (this.Items.Count <= maxItems)
      return;
    KeyValuePair<PXCommandKey, PXQueryResult>[] array1 = this.Items.ToArray();
    KeyValuePair<PXCommandKey, PXQueryResult>[] array2 = ((IEnumerable<KeyValuePair<PXCommandKey, PXQueryResult>>) array1).Where<KeyValuePair<PXCommandKey, PXQueryResult>>((Func<KeyValuePair<PXCommandKey, PXQueryResult>, bool>) (pair => pair.Value.Items.Count <= 1)).ToArray<KeyValuePair<PXCommandKey, PXQueryResult>>();
    KeyValuePair<PXCommandKey, PXQueryResult>[] array3 = ((IEnumerable<KeyValuePair<PXCommandKey, PXQueryResult>>) array1).Where<KeyValuePair<PXCommandKey, PXQueryResult>>((Func<KeyValuePair<PXCommandKey, PXQueryResult>, bool>) (pair => pair.Value.Items.Count > 1)).ToArray<KeyValuePair<PXCommandKey, PXQueryResult>>();
    PXQueryResult pxQueryResult;
    if (array2.Length > PXViewQueryCollection._maxSingleRowItems)
    {
      foreach (KeyValuePair<PXCommandKey, PXQueryResult> keyValuePair in ((IEnumerable<KeyValuePair<PXCommandKey, PXQueryResult>>) array2).OrderByDescending<KeyValuePair<PXCommandKey, PXQueryResult>, PXDatabase.PXTableAge>((Func<KeyValuePair<PXCommandKey, PXQueryResult>, PXDatabase.PXTableAge>) (pair => pair.Value.Age), (IComparer<PXDatabase.PXTableAge>) PXDatabase.PXTableAgeComparer.Default).Take<KeyValuePair<PXCommandKey, PXQueryResult>>(array2.Length - PXViewQueryCollection._maxSingleRowItems))
        this.Items.TryRemove(keyValuePair.Key, out pxQueryResult);
    }
    if (array3.Length <= maxItems)
      return;
    foreach (KeyValuePair<PXCommandKey, PXQueryResult> keyValuePair in ((IEnumerable<KeyValuePair<PXCommandKey, PXQueryResult>>) array3).OrderByDescending<KeyValuePair<PXCommandKey, PXQueryResult>, PXDatabase.PXTableAge>((Func<KeyValuePair<PXCommandKey, PXQueryResult>, PXDatabase.PXTableAge>) (pair => pair.Value.Age), (IComparer<PXDatabase.PXTableAge>) PXDatabase.PXTableAgeComparer.Default).Take<KeyValuePair<PXCommandKey, PXQueryResult>>(array3.Length - minItems))
      this.Items.TryRemove(keyValuePair.Key, out pxQueryResult);
  }

  private static System.Type[] GetSqlTablesTypes(IEnumerable<System.Type> bqlTables, PXGraph g)
  {
    HashSet<System.Type> source = new HashSet<System.Type>(bqlTables);
    foreach (System.Type bqlTable in bqlTables)
    {
      if (Attribute.IsDefined((MemberInfo) bqlTable, typeof (PXProjectionAttribute)))
      {
        PXCache cach = g.Caches[bqlTable];
        if (cach.BqlSelect != null)
        {
          foreach (System.Type table in cach.BqlSelect.GetTables())
            source.Add(table);
          continue;
        }
      }
      source.Add(bqlTable);
    }
    System.Type graphType = CustomizedTypeManager.GetTypeNotCustomized(g);
    return source.Select<System.Type, System.Type>((Func<System.Type, System.Type>) (_ => PXCache.GetBqlTable(PXSubstManager.Substitute(_, graphType)))).Distinct<System.Type>().ToArray<System.Type>();
  }

  private static string[] GetSqlTables(IEnumerable<System.Type> bqlTables, PXGraph g)
  {
    return ((IEnumerable<System.Type>) PXViewQueryCollection.GetSqlTablesTypes(bqlTables, g)).Select<System.Type, string>((Func<System.Type, string>) (_ => _.Name.ToLowerInvariant())).ToArray<string>();
  }

  public bool TryRemove(PXCommandKey key, out PXQueryResult v) => this.Items.TryRemove(key, out v);

  public void RemoveAll(Func<PXQueryResult, bool> predicate)
  {
    foreach (KeyValuePair<PXCommandKey, PXQueryResult> keyValuePair in this.Items)
    {
      if (predicate(keyValuePair.Value))
        this.Items.TryRemove(keyValuePair.Key, out PXQueryResult _);
    }
  }

  public bool TryGetValue(PXCommandKey key, out PXQueryResult v)
  {
    return this.Prepared.TryGetValue(this.GetAltCommandKey(key.GetParameters()), out v) || this.Items.TryGetValue(key, out v);
  }

  public bool HasStoredResults() => !this.Prepared.IsEmpty;
}
