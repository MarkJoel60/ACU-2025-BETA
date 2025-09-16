// Decompiled with JetBrains decompiler
// Type: PX.Translation.ResourceByScreenCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Translation;

/// <exclude />
public class ResourceByScreenCollection
{
  private const string KEY_SEPATATOR = "_";
  private readonly Dictionary<string, HashSet<string>> resourcesByScreens;

  public ResourceByScreenCollection()
  {
    this.resourcesByScreens = new Dictionary<string, HashSet<string>>();
  }

  public static string GetKey(string neutralValue, string resKey)
  {
    return $"{neutralValue.ToUpper()}{"_"}{(resKey == null ? (object) string.Empty : (object) resKey.ToUpper())}";
  }

  public void Add(LocalizationResourceLite resourceLite)
  {
    if (resourceLite == null || string.IsNullOrEmpty(resourceLite.ScreenId))
      return;
    string resourceLiteKey = this.GetResourceLiteKey(resourceLite);
    if (this.resourcesByScreens.ContainsKey(resourceLiteKey))
      this.resourcesByScreens[resourceLiteKey].Add(resourceLite.ScreenId);
    else
      this.resourcesByScreens.Add(resourceLiteKey, new HashSet<string>((IEnumerable<string>) new string[1]
      {
        resourceLite.ScreenId
      }));
  }

  public bool ContainsResource(LocalizationResourceLite resourceLite)
  {
    bool flag = false;
    if (resourceLite != null)
      flag = this.resourcesByScreens.ContainsKey(this.GetResourceLiteKey(resourceLite));
    return flag;
  }

  public IEnumerable<LocalizationResourceByScreen> GetResourceByScreenUsage(
    string key,
    string idRes,
    string idValue)
  {
    IEnumerable<LocalizationResourceByScreen> resourceByScreenUsage = (IEnumerable<LocalizationResourceByScreen>) null;
    if (this.resourcesByScreens.ContainsKey(key))
      resourceByScreenUsage = this.resourcesByScreens[key].Select<string, LocalizationResourceByScreen>((Func<string, LocalizationResourceByScreen>) (screenId => new LocalizationResourceByScreen()
      {
        ScreenID = screenId,
        IdValue = idValue,
        IdRes = idRes
      }));
    return resourceByScreenUsage;
  }

  private string GetResourceLiteKey(LocalizationResourceLite resourceLite)
  {
    return ResourceByScreenCollection.GetKey(resourceLite.NeutralValue, resourceLite.ResKey);
  }
}
