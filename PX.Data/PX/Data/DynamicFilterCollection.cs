// Decompiled with JetBrains decompiler
// Type: PX.Data.DynamicFilterCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
internal class DynamicFilterCollection : 
  ConcurrentDictionary<string, FilterModelDictionary>,
  IPrefetchable,
  IPXCompanyDependent
{
  public void Add(string graphType, string viewName, string filterName, PXFilterRow[] filterRows)
  {
    if (string.IsNullOrEmpty(graphType))
      throw new PXArgumentException("", "The graph type can't be empty.");
    if (string.IsNullOrEmpty(viewName))
      throw new PXArgumentException("", "The view name can't be empty.");
    if (string.IsNullOrEmpty(filterName))
      throw new PXArgumentException("", "The filter name can't be empty.");
    if (filterRows == null)
      throw new PXArgumentException("", "The filter rows can't be null.");
    string filterKey = this.GetFilterKey(graphType, viewName);
    if (this.ContainsKey(filterKey))
    {
      FilterModelDictionary filterModelDictionary = this[filterKey];
      if (filterModelDictionary.ContainsName(filterName))
        return;
      filterModelDictionary.Add(filterName, filterRows);
    }
    else
      this.TryAdd(filterKey, new FilterModelDictionary()
      {
        {
          filterName,
          filterRows
        }
      });
  }

  public void AppendFilterNames(
    string graphType,
    string viewName,
    Dictionary<Guid, string> existingNames)
  {
    if (existingNames == null)
      throw new PXArgumentException("", "You can't append filter names to an undefined collection.");
    FilterModelDictionary filterModelDictionary;
    if (!this.TryGetValue(this.GetFilterKey(graphType, viewName), out filterModelDictionary))
      return;
    foreach (KeyValuePair<Guid, FilterModel> keyValuePair in (ConcurrentDictionary<Guid, FilterModel>) filterModelDictionary)
      existingNames.Add(keyValuePair.Key, PXMessages.LocalizeNoPrefix(keyValuePair.Value.Name));
  }

  public IEnumerable<KeyValuePair<Guid, string>> GetFilterInfo(string graphType, string viewName)
  {
    string filterKey = this.GetFilterKey(graphType, viewName);
    IEnumerable<KeyValuePair<Guid, string>> filterInfo1 = (IEnumerable<KeyValuePair<Guid, string>>) new KeyValuePair<Guid, string>[0];
    FilterModelDictionary source;
    if (this.TryGetValue(filterKey, out source))
      filterInfo1 = source.Select<KeyValuePair<Guid, FilterModel>, KeyValuePair<Guid, string>>((Func<KeyValuePair<Guid, FilterModel>, KeyValuePair<Guid, string>>) (filterInfo => new KeyValuePair<Guid, string>(filterInfo.Key, PXMessages.LocalizeNoPrefix(filterInfo.Value.Name))));
    return filterInfo1;
  }

  public IEnumerable<string> GetFilterFields(string graphType, string viewName, Guid filterId)
  {
    return this.GetFilterRows(graphType, viewName, filterId).Select<PXFilterRow, string>((Func<PXFilterRow, string>) (row => row.DataField));
  }

  public IEnumerable<PXFilterRow> GetFilterRows(string graphType, string viewName, Guid filterId)
  {
    IEnumerable<PXFilterRow> filterRows = (IEnumerable<PXFilterRow>) new PXFilterRow[0];
    FilterModelDictionary filterModelDictionary;
    FilterModel filterModel;
    if (this.TryGetValue(this.GetFilterKey(graphType, viewName), out filterModelDictionary) && filterModelDictionary.TryGetValue(filterId, out filterModel))
      filterRows = (IEnumerable<PXFilterRow>) filterModel.Rows;
    return filterRows;
  }

  public string GetFilterName(Guid filterId)
  {
    FilterModel filterModel = (FilterModel) null;
    bool flag = false;
    IEnumerator<KeyValuePair<string, FilterModelDictionary>> enumerator = this.GetEnumerator();
    while (!flag && enumerator.MoveNext())
    {
      if (enumerator.Current.Value.TryGetValue(filterId, out filterModel))
        flag = true;
    }
    return !flag ? (string) null : filterModel.Name;
  }

  public void Prefetch()
  {
  }

  private string GetFilterKey(string graphType, string viewName)
  {
    return $"{graphType}+{viewName}+{CultureInfo.CurrentCulture.Name}";
  }
}
