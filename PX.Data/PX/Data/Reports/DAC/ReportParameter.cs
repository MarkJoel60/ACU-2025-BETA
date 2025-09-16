// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.DAC.ReportParameter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Reports.DAC;

[PXHidden]
public class ReportParameter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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
      EnumerableExtensions.AddRange<string, object>((IDictionary<string, object>) this._values, (IEnumerable<KeyValuePair<string, object>>) value);
    }
  }
}
