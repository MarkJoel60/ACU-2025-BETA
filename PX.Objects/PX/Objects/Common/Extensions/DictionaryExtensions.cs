// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Extensions.DictionaryExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common.Extensions;

public static class DictionaryExtensions
{
  /// <summary>
  /// Given a dictionary, gets the value by key. If the key is not
  /// present, adds it to the dictionary along with the value generated
  /// by the initializer function, and returns that value.
  /// </summary>
  public static TValue GetOrAdd<TKey, TValue>(
    this IDictionary<TKey, TValue> dictionary,
    TKey key,
    Func<TValue> initializer)
  {
    if (dictionary == null)
      throw new ArgumentNullException(nameof (dictionary));
    if ((object) key == null)
      throw new ArgumentNullException(nameof (key));
    if (initializer == null)
      throw new ArgumentNullException(nameof (initializer));
    TValue orAdd;
    if (!dictionary.TryGetValue(key, out orAdd))
    {
      orAdd = initializer();
      dictionary[key] = orAdd;
    }
    return orAdd;
  }

  public static ICollection<TValue> GetValueOrEmpty<TKey, TValue>(
    this IDictionary<TKey, ICollection<TValue>> dictionary,
    TKey key)
  {
    ICollection<TValue> objs;
    dictionary.TryGetValue(key, out objs);
    return objs ?? (ICollection<TValue>) new TValue[0];
  }
}
