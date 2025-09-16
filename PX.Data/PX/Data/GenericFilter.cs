// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
[PXHidden]
[Serializable]
public class GenericFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private readonly Dictionary<string, object> _values = new Dictionary<string, object>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

  public IDictionary<string, object> Values
  {
    get => (IDictionary<string, object>) this._values;
    set
    {
      this._values.Clear();
      if (value == null)
        return;
      foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>) value)
        this._values.Add(keyValuePair.Key, keyValuePair.Value);
    }
  }
}
