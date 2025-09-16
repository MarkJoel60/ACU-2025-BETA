// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.ChildTableFilterAttribute
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

public class ChildTableFilterAttribute : PXCustomSelectorAttribute
{
  private Func<IEnumerable> _getRecords = (Func<IEnumerable>) (() => (IEnumerable) Array<GITable>.Empty);

  public ChildTableFilterAttribute()
    : base(typeof (Search<ChildTable.fullName>), typeof (PXTablesSelectorAttribute.SingleTable.name), typeof (LinkedTable.description), typeof (ChildTable.fullName))
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
    foreach (ChildTableFilterAttribute tableFilterAttribute in cache.GetAttributesOfType<ChildTableFilterAttribute>(data, name))
    {
      if (tableFilterAttribute != null)
        tableFilterAttribute._getRecords = getRecords;
    }
  }

  public static void SetFunction<Field>(PXCache cache, object data, Func<IEnumerable> getRecords) where Field : IBqlField
  {
    ChildTableFilterAttribute.SetFunction(cache, data, typeof (Field).Name, getRecords);
  }

  protected virtual IEnumerable GetRecords()
  {
    Dictionary<string, ChildTable> dictionary1 = new Dictionary<string, ChildTable>();
    foreach (object obj in this._getRecords())
    {
      LinkedTable linkedTable = obj as LinkedTable;
      Dictionary<string, ChildTable> dictionary2 = dictionary1;
      string fullName = linkedTable.FullName;
      ChildTable childTable = new ChildTable();
      childTable.FullName = linkedTable.FullName;
      childTable.Name = linkedTable.Name;
      dictionary2[fullName] = childTable;
    }
    return (IEnumerable) dictionary1.Values;
  }
}
