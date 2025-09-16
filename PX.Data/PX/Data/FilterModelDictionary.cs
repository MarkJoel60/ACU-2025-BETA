// Decompiled with JetBrains decompiler
// Type: PX.Data.FilterModelDictionary
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Concurrent;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
internal class FilterModelDictionary : ConcurrentDictionary<Guid, FilterModel>
{
  private static readonly Guid DEFAULT_FILTER_ID = new Guid("24b26425-1530-4af8-b8dc-ecba6554296d");
  private static Guid nextFilterId = FilterModelDictionary.DEFAULT_FILTER_ID;

  public bool ContainsName(string name)
  {
    return ConcurrentDictionaryExtensions.ValuesExt<Guid, FilterModel>((ConcurrentDictionary<Guid, FilterModel>) this).Any<FilterModel>((Func<FilterModel, bool>) (filterModel => string.Compare(filterModel.Name, name, StringComparison.OrdinalIgnoreCase) == 0));
  }

  public void Add(string filterName, PXFilterRow[] filterRows)
  {
    FilterModel filterModel = new FilterModel()
    {
      Name = filterName,
      Rows = filterRows
    };
    if (!this.TryAdd(FilterModelDictionary.nextFilterId, filterModel))
      return;
    FilterModelDictionary.nextFilterId = Guid.NewGuid();
  }
}
