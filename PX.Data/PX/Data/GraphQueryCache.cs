// Decompiled with JetBrains decompiler
// Type: PX.Data.GraphQueryCache
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Session;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Data;

internal static class GraphQueryCache
{
  private const string QueryCacheSuffix = "GraphQueryCache";

  private static string GetKey(GraphSessionStatePrefix sessionStatePrefix)
  {
    return sessionStatePrefix.GetSubKey(nameof (GraphQueryCache));
  }

  private static bool IsKey(string key)
  {
    return key.EndsWith("$GraphQueryCache", StringComparison.OrdinalIgnoreCase);
  }

  private static PXGraphQueryCacheCollection? GetGraphQueryCache(
    this IPXSessionState session,
    string key)
  {
    return session.Get(key) as PXGraphQueryCacheCollection;
  }

  internal static PXGraphQueryCacheCollection? GetGraphQueryCache(
    this IPXSessionState session,
    GraphSessionStatePrefix sessionStatePrefix)
  {
    return session.GetGraphQueryCache(GraphQueryCache.GetKey(sessionStatePrefix));
  }

  internal static void SetGraphQueryCache(
    this IPXSessionState session,
    GraphSessionStatePrefix sessionStatePrefix,
    PXGraphQueryCacheCollection queryCache)
  {
    session.Set(GraphQueryCache.GetKey(sessionStatePrefix), (object) queryCache);
  }

  internal static void ClearGraphQueryCache(this IPXSessionState session)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    session.RemoveAll(GraphQueryCache.\u003C\u003EO.\u003C0\u003E__IsKey ?? (GraphQueryCache.\u003C\u003EO.\u003C0\u003E__IsKey = new Func<string, bool>(GraphQueryCache.IsKey)));
  }

  internal static IEnumerable<IBqlTableSystemDataStorage> GetPXBqlTablesFromGraphQueryCache(
    this IPXSessionState session,
    string key)
  {
    if (GraphQueryCache.IsKey(key))
    {
      PXGraphQueryCacheCollection graphQueryCache = session.GetGraphQueryCache(key);
      if (graphQueryCache != null)
      {
        foreach (KeyValuePair<ViewKey, PXViewQueryCollection> keyValuePair in (Dictionary<ViewKey, PXViewQueryCollection>) graphQueryCache)
        {
          foreach (PXQueryResult query in keyValuePair.Value.Values)
          {
            if (query.Items is PXView.VersionedList items)
            {
              PXView.VersionedList mergedList = items.MergedList;
              if (mergedList != null)
              {
                foreach (object obj in (List<object>) mergedList)
                {
                  if (obj is IBqlTableSystemDataStorage systemDataStorage)
                    yield return systemDataStorage;
                }
              }
            }
            foreach (object obj in query.Items)
            {
              if (obj is IBqlTableSystemDataStorage systemDataStorage)
                yield return systemDataStorage;
            }
          }
        }
      }
    }
  }
}
