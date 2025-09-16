// Decompiled with JetBrains decompiler
// Type: PX.Api.Helpers.LinkedSelectorViews
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Api.Helpers;

internal class LinkedSelectorViews
{
  private readonly Dictionary<string, string> _innerDictionary;

  public LinkedSelectorViews(Dictionary<string, string> innerDictionary = null)
  {
    this._innerDictionary = innerDictionary ?? new Dictionary<string, string>();
  }

  public bool ContainsView(string viewName) => this._innerDictionary.ContainsValue(viewName);

  public IEnumerable<string> GetFields(string viewName)
  {
    return this._innerDictionary.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (c => c.Value.OrdinalEquals(viewName))).Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (c => c.Key));
  }

  public IEnumerable<string> Views
  {
    get => (IEnumerable<string>) this._innerDictionary.Keys.ToArray<string>();
  }
}
