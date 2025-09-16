// Decompiled with JetBrains decompiler
// Type: PX.Data.Utility.DictionaryExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Utility;

public static class DictionaryExtensions
{
  public static IDictionary<TKey, TValue> Merge<TKey, TValue>(
    this IDictionary<TKey, TValue> left,
    IDictionary<TKey, TValue> right)
  {
    if (left == null)
      throw new ArgumentNullException(nameof (left));
    if (right == null)
      return left;
    Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(left);
    foreach (KeyValuePair<TKey, TValue> keyValuePair in (IEnumerable<KeyValuePair<TKey, TValue>>) right)
    {
      if (!dictionary.ContainsKey(keyValuePair.Key))
        dictionary.Add(keyValuePair.Key, keyValuePair.Value);
    }
    return (IDictionary<TKey, TValue>) dictionary;
  }
}
