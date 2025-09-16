// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GIDataHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

public static class GIDataHelper
{
  public static T GetValue<T>(this IDictionary<string, object> dataDictionary) where T : new()
  {
    return (T) dataDictionary.Values.FirstOrDefault<object>((Func<object, bool>) (x => x is T)) ?? new T();
  }

  public static IDictionary<string, object> GetGenericResult(
    this IDictionary<string, object> dataDictionary)
  {
    return (IDictionary<string, object>) dataDictionary.GetValue<Dictionary<string, object>>();
  }

  public static T GetGenericResultValue<T>(
    this IDictionary<string, object> dataDictionary,
    string keyPrefix)
  {
    Dictionary<string, object> source = dataDictionary.GetValue<Dictionary<string, object>>();
    return (T) (source != null ? source.FirstOrDefault<KeyValuePair<string, object>>((Func<KeyValuePair<string, object>, bool>) (x => x.Value is T && x.Key.StartsWith(keyPrefix, StringComparison.OrdinalIgnoreCase))).Value : (object) null) ?? default (T);
  }

  public static T GetValue<T>(this IDictionary<string, object> dataDictionary, string itemName) where T : new()
  {
    object obj1;
    return dataDictionary.TryGetValue(itemName, out obj1) && obj1 is T obj2 ? obj2 : new T();
  }
}
