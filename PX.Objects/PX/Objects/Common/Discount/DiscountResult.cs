// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.DiscountResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.Common.Discount;

public struct DiscountResult
{
  /// <summary>
  /// Gets Discount. Its either a Percent or an Amount. Check <see cref="P:PX.Objects.Common.Discount.DiscountResult.IsAmount" /> property.
  /// </summary>
  public Decimal? Discount { get; }

  /// <summary>
  /// Returns true if Discount is an Amount; otherwise false.
  /// </summary>
  public bool IsAmount { get; }

  /// <summary>Returns True if No Discount was found.</summary>
  public bool IsEmpty
  {
    get
    {
      if (!this.Discount.HasValue)
        return true;
      Decimal? discount = this.Discount;
      Decimal num = 0M;
      return discount.GetValueOrDefault() == num & discount.HasValue;
    }
  }

  internal DiscountResult(Decimal? discount, bool isAmount)
  {
    this.Discount = discount;
    this.IsAmount = isAmount;
  }
}
