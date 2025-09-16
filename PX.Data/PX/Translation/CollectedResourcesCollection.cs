// Decompiled with JetBrains decompiler
// Type: PX.Translation.CollectedResourcesCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Translation;

internal class CollectedResourcesCollection : 
  Dictionary<string, KeyValuePair<string, List<LocalizationResourceLite>>>
{
  public void Add(LocalizationResourceLite newResource)
  {
    string key = this.NormalizeKey(newResource.NeutralValue);
    KeyValuePair<string, List<LocalizationResourceLite>> keyValuePair;
    if (this.TryGetValue(key, out keyValuePair))
    {
      keyValuePair.Value.Add(newResource);
      if (string.Compare(keyValuePair.Key, newResource.NeutralValue, StringComparison.Ordinal) <= 0)
        return;
      this[key] = new KeyValuePair<string, List<LocalizationResourceLite>>(newResource.NeutralValue, keyValuePair.Value);
    }
    else
      this.Add(key, new KeyValuePair<string, List<LocalizationResourceLite>>(newResource.NeutralValue, new List<LocalizationResourceLite>()
      {
        newResource
      }));
  }

  public bool TryGetResources(LocalizationValue value, out List<LocalizationResourceLite> resources)
  {
    resources = (List<LocalizationResourceLite>) null;
    KeyValuePair<string, List<LocalizationResourceLite>> keyValuePair;
    int num = this.TryGetValue(this.NormalizeKey(value.NeutralValue), out keyValuePair) ? 1 : 0;
    if (num == 0)
      return num != 0;
    resources = keyValuePair.Value;
    return num != 0;
  }

  public bool Contains(LocalizationValue value)
  {
    return this.ContainsKey(this.NormalizeKey(value.NeutralValue));
  }

  public string GetNeutralValue(LocalizationValue value)
  {
    return this[this.NormalizeKey(value.NeutralValue)].Key;
  }

  private string NormalizeKey(string neutralValue) => neutralValue.ToLower();
}
