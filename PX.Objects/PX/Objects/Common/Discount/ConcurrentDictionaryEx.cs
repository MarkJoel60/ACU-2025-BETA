// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.ConcurrentDictionaryEx
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Concurrent;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common.Discount;

public static class ConcurrentDictionaryEx
{
  public static bool Remove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> self, TKey key)
  {
    return ((IDictionary<TKey, TValue>) self).Remove(key);
  }
}
