// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.ParentTableFilterAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Maintenance.GI;

public class ParentTableFilterAttribute : PXCustomSelectorAttribute
{
  private Func<IEnumerable> _getRecords = (Func<IEnumerable>) (() => (IEnumerable) Array<GITable>.Empty);

  public ParentTableFilterAttribute()
    : base(typeof (Search<GITable.name>), typeof (GITable.alias), typeof (GITable.description), typeof (GITable.name))
  {
  }

  public static void SetFunction(
    PXCache cache,
    object data,
    string name,
    Func<IEnumerable> getRecords)
  {
    if (data == null)
      cache.SetAltered(name, true);
    foreach (ParentTableFilterAttribute tableFilterAttribute in cache.GetAttributesOfType<ParentTableFilterAttribute>(data, name))
    {
      if (tableFilterAttribute != null)
        tableFilterAttribute._getRecords = getRecords;
    }
  }

  public static void SetFunction<Field>(PXCache cache, object data, Func<IEnumerable> getRecords) where Field : IBqlField
  {
    ParentTableFilterAttribute.SetFunction(cache, data, typeof (Field).Name, getRecords);
  }

  protected virtual IEnumerable GetRecords()
  {
    Dictionary<string, GITable> dictionary = new Dictionary<string, GITable>();
    foreach (object obj in this._getRecords())
    {
      GITable giTable = obj as GITable;
      dictionary[giTable.Name] = new GITable()
      {
        Name = giTable.Name,
        Alias = giTable.Alias
      };
    }
    return (IEnumerable) dictionary.Values;
  }
}
