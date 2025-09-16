// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Extensions.DecimalExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.Common.Extensions;

public static class DecimalExtensions
{
  public static bool IsNonZero(this Decimal? number) => number.GetValueOrDefault() != 0M;

  public static bool IsNullOrZero(this Decimal? number)
  {
    if (!number.HasValue)
      return true;
    Decimal? nullable = number;
    Decimal num = 0M;
    return nullable.GetValueOrDefault() == num & nullable.HasValue;
  }

  public static string ToFormattedString(this Decimal? number)
  {
    if (!number.HasValue)
      throw new ArgumentNullException(nameof (number));
    return number.Value.ToString($"F{CommonSetupDecPl.Qty}");
  }
}
