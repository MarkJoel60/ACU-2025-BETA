// Decompiled with JetBrains decompiler
// Type: PX.Data.CompareIgnoreCase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
[PXInternalUseOnly]
public class CompareIgnoreCase
{
  public static bool IsInList(List<string> list, string item)
  {
    if (list != null && item != null)
    {
      for (int index = 0; index < list.Count; ++index)
      {
        if (string.Equals(list[index], item, StringComparison.OrdinalIgnoreCase))
          return true;
      }
    }
    return false;
  }

  public static bool IsListEndingWithSublist(List<string> list, List<string> sublist)
  {
    if (list.Count < sublist.Count)
      return false;
    int num = list.Count - sublist.Count;
    for (int index = sublist.Count - 1; index >= 0; --index)
    {
      if (!string.Equals(sublist[index], list[index + num], StringComparison.OrdinalIgnoreCase))
        return false;
    }
    return true;
  }

  public static string GetCollectionKey(ICollection collection, string key)
  {
    if (collection != null && key != null)
    {
      foreach (object obj in (IEnumerable) collection)
      {
        if (obj is string a && string.Equals(a, key, StringComparison.OrdinalIgnoreCase))
          return a;
      }
    }
    return (string) null;
  }
}
