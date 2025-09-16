// Decompiled with JetBrains decompiler
// Type: PX.Data.CacheSessionStateExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Primitives;
using PX.Common;
using PX.Common.Session;
using System;
using System.Collections.Generic;
using System.Web.Compilation;

#nullable enable
namespace PX.Data;

internal static class CacheSessionStateExtensions
{
  private const string CacheInfoSuffix = "$CacheSessionData";

  private static string GetCacheInfoKey(
    this GraphSessionStatePrefix sessionStatePrefix,
    System.Type dacType)
  {
    return sessionStatePrefix.GetSubKey(dacType.FullName + "$CacheSessionData");
  }

  private static string GetCacheInfoKey(PXGraph graph, System.Type dacType)
  {
    return GraphSessionStatePrefix.For(graph).GetCacheInfoKey(dacType);
  }

  private static object[]? GetCacheInfo(this IPXSessionState session, string key)
  {
    return session.Get(key) as object[];
  }

  internal static object[]? GetCacheInfo(this IPXSessionState session, PXGraph graph, System.Type dacType)
  {
    return session.GetCacheInfo(CacheSessionStateExtensions.GetCacheInfoKey(graph, dacType));
  }

  internal static object[]? GetCacheInfo(
    this IPXSessionState session,
    GraphSessionStatePrefix sessionStatePrefix,
    System.Type dacType)
  {
    return session.GetCacheInfo(sessionStatePrefix.GetCacheInfoKey(dacType));
  }

  internal static void SetCacheInfo(
    this IPXSessionState session,
    PXGraph graph,
    System.Type dacType,
    object[] cacheInfo)
  {
    string cacheInfoKey = CacheSessionStateExtensions.GetCacheInfoKey(graph, dacType);
    session.Set(cacheInfoKey, (object) cacheInfo);
    Dictionary<string, int> dictionary = session.EnsureCacheStateOrder();
    if (dictionary.ContainsKey(cacheInfoKey))
      return;
    dictionary[cacheInfoKey] = dictionary.Count;
  }

  internal static IEnumerable<System.Type> GetDacTypesFromCacheInfo(
    this IPXSessionState session,
    GraphSessionStatePrefix sessionPrefix)
  {
    IEnumerable<(string, StringSegment)> valueTuples = session.GetSubKeys(sessionPrefix);
    Dictionary<string, int> cacheOrder = session.GetCacheStateOrder();
    if (cacheOrder != null)
    {
      int num;
      valueTuples = (IEnumerable<(string, StringSegment)>) EnumerableExtensions.OrderBy<(string, StringSegment), int>(valueTuples, (Func<(string, StringSegment), int>) (key => !cacheOrder.TryGetValue(key.key, out num) ? 0 : num), Array.Empty<int>());
    }
    foreach ((string _, StringSegment stringSegment) in valueTuples)
    {
      if (((StringSegment) ref stringSegment).EndsWith("$CacheSessionData", StringComparison.OrdinalIgnoreCase))
      {
        string typeName = ((StringSegment) ref stringSegment).Substring(0, ((StringSegment) ref stringSegment).Length - "$CacheSessionData".Length);
        if (typeName.Length != 0)
        {
          System.Type type = PXBuildManager.GetType(typeName, false);
          if ((object) type != null)
            yield return type;
        }
      }
    }
  }

  internal static void RemoveCacheInfo(this IPXSessionState session, PXGraph graph, System.Type dacType)
  {
    session.Remove(CacheSessionStateExtensions.GetCacheInfoKey(graph, dacType));
  }

  private static Dictionary<string, int>? GetCacheStateOrder(this IPXSessionState session)
  {
    return session.Get<Dictionary<string, int>>(CacheSessionStateExtensions.Keys.CacheStateOrder);
  }

  private static Dictionary<string, int> EnsureCacheStateOrder(this IPXSessionState session)
  {
    Dictionary<string, int> cacheStateOrder = session.GetCacheStateOrder();
    if (cacheStateOrder != null)
      return cacheStateOrder;
    Dictionary<string, int> dictionary = new Dictionary<string, int>();
    session.Set<Dictionary<string, int>>(CacheSessionStateExtensions.Keys.CacheStateOrder, dictionary);
    return dictionary;
  }

  internal static IEnumerable<IBqlTableSystemDataStorage> GetPXBqlTablesFromCacheInfo(
    this IPXSessionState session,
    string key)
  {
    if (key.EndsWith("$CacheSessionData", StringComparison.OrdinalIgnoreCase))
    {
      object[] bucket = session.GetCacheInfo(key);
      if (bucket != null && bucket.Length > 0)
      {
        for (int i = 0; i < 4 && i < bucket.Length; ++i)
        {
          if (bucket[i] is IBqlTableSystemDataStorage[] systemDataStorageArray1)
          {
            IBqlTableSystemDataStorage[] systemDataStorageArray = systemDataStorageArray1;
            for (int index = 0; index < systemDataStorageArray.Length; ++index)
            {
              IBqlTableSystemDataStorage systemDataStorage = systemDataStorageArray[index];
              if (systemDataStorage != null)
                yield return systemDataStorage;
            }
            systemDataStorageArray = (IBqlTableSystemDataStorage[]) null;
          }
        }
        if (bucket.Length > 4 && bucket[4] is IBqlTableSystemDataStorage systemDataStorage1)
          yield return systemDataStorage1;
      }
    }
  }

  private static class Keys
  {
    internal static readonly SessionKey<Dictionary<string, int>> CacheStateOrder = new SessionKey<Dictionary<string, int>>(typeof (PXCache).FullName + ".CacheStateOrder");
  }
}
