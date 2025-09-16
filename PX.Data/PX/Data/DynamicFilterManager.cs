// Decompiled with JetBrains decompiler
// Type: PX.Data.DynamicFilterManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public class DynamicFilterManager
{
  private const string FILTER_COLLECTION_SLOT_KEY = "DynamicFilterCollection";

  public static void AddFilter(
    string graphType,
    string viewName,
    string filterName,
    PXFilterRow[] filterRows)
  {
    DynamicFilterCollection collection;
    if (!DynamicFilterManager.TryGetFilterCollection(out collection))
      return;
    collection.Add(graphType, viewName, filterName, filterRows);
  }

  public static void AppendFilterNames(
    string graphType,
    string viewName,
    Dictionary<Guid, string> existingNames)
  {
    DynamicFilterCollection collection;
    if (!DynamicFilterManager.TryGetFilterCollection(out collection))
      return;
    collection.AppendFilterNames(graphType, viewName, existingNames);
  }

  public static IEnumerable<string> GetFilterFields(
    string graphType,
    string viewName,
    Guid filterId)
  {
    IEnumerable<string> filterFields = (IEnumerable<string>) new string[0];
    DynamicFilterCollection collection;
    if (DynamicFilterManager.TryGetFilterCollection(out collection))
      filterFields = collection.GetFilterFields(graphType, viewName, filterId);
    return filterFields;
  }

  public static IEnumerable<PXFilterRow> GetFilterRows(
    string graphType,
    string viewName,
    Guid filterId)
  {
    IEnumerable<PXFilterRow> filterRows = (IEnumerable<PXFilterRow>) new PXFilterRow[0];
    DynamicFilterCollection collection;
    if (DynamicFilterManager.TryGetFilterCollection(out collection))
      filterRows = collection.GetFilterRows(graphType, viewName, filterId);
    return filterRows;
  }

  public static IEnumerable<KeyValuePair<Guid, string>> GetFilterInfo(
    string graphType,
    string viewName)
  {
    IEnumerable<KeyValuePair<Guid, string>> filterInfo = (IEnumerable<KeyValuePair<Guid, string>>) new KeyValuePair<Guid, string>[0];
    DynamicFilterCollection collection;
    if (DynamicFilterManager.TryGetFilterCollection(out collection))
      filterInfo = collection.GetFilterInfo(graphType, viewName);
    return filterInfo;
  }

  public static string GetFilterName(Guid filterId)
  {
    string filterName = (string) null;
    DynamicFilterCollection collection;
    if (DynamicFilterManager.TryGetFilterCollection(out collection))
      filterName = collection.GetFilterName(filterId);
    return filterName;
  }

  private static bool TryGetFilterCollection(out DynamicFilterCollection collection)
  {
    collection = PXDatabase.GetSlot<DynamicFilterCollection>("DynamicFilterCollection");
    return collection != null;
  }
}
