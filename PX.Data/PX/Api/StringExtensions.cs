// Decompiled with JetBrains decompiler
// Type: PX.Api.StringExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Api;

public static class StringExtensions
{
  public static bool OrdinalEquals(this string a, string b)
  {
    return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
  }

  public static bool OrdinalIgnoreCaseEquals(this string a, string b)
  {
    return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
  }

  public static bool OrdinalContains(this string str, params string[] values)
  {
    if (string.IsNullOrEmpty(str))
      return false;
    foreach (string str1 in values)
    {
      if (str.IndexOf(str1, StringComparison.OrdinalIgnoreCase) >= 0)
        return true;
    }
    return false;
  }

  public static bool OrdinalStartsWith(this string str, params string[] values)
  {
    if (string.IsNullOrEmpty(str))
      return false;
    foreach (string str1 in values)
    {
      if (str.StartsWith(str1, StringComparison.OrdinalIgnoreCase))
        return true;
    }
    return false;
  }

  public static bool OrdinalEndsWith(this string str, params string[] values)
  {
    if (string.IsNullOrEmpty(str))
      return false;
    foreach (string str1 in values)
    {
      if (str.EndsWith(str1, StringComparison.OrdinalIgnoreCase))
        return true;
    }
    return false;
  }
}
