// Decompiled with JetBrains decompiler
// Type: PX.Api.PXDummySYTableBuilder
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Api;

internal class PXDummySYTableBuilder : ISYTableAddGet
{
  private Dictionary<string, RowField> values = new Dictionary<string, RowField>();

  public void AddValue(
    string hdr,
    object v,
    PXFieldState state,
    PXDBLocalizableStringAttribute.Translations translations = null)
  {
    if (string.IsNullOrEmpty(hdr))
      return;
    this.values[hdr] = new RowField(v, state, translations);
  }

  public object GetValue(string hdr)
  {
    RowField rowField;
    return !string.IsNullOrEmpty(hdr) && this.values.TryGetValue(hdr, out rowField) ? rowField.Value : (object) null;
  }
}
