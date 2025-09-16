// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.FullBalanceDelta
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.Common;

public struct FullBalanceDelta
{
  /// <summary>
  /// The unsigned amount (in adjusted document currency)
  /// on which the adjusted document balance is affected
  /// and which is not induced by a symmetrical change
  /// in the adjusting document balance. For example,
  /// it may include RGOL, write-offs, cash discounts etc.
  /// </summary>
  public Decimal CurrencyAdjustedExtraAmount;
  /// <summary>
  /// The unsigned amount (in adjusting document currency)
  /// on which the adjusted document balance is affected
  /// and which is not induced by a symmetrical change
  /// in the adjusting document balance. For example,
  /// it may include RGOL, write-offs, cash discounts etc.
  /// </summary>
  public Decimal CurrencyAdjustingExtraAmount;
  /// <summary>
  /// The unsigned amount (in base currency)
  /// on which the adjusted document balance is affected
  /// and which is not induced by a symmetrical change
  /// in the adjusting document balance. For example,
  /// it may include RGOL, write-offs, cash discounts etc.
  /// </summary>
  public Decimal BaseAdjustedExtraAmount;
  /// <summary>
  /// The full unsigned amount (in document currency)
  /// on which the adjusted document balance is affected
  /// by the application.
  /// </summary>
  public Decimal CurrencyAdjustedBalanceDelta;
  /// <summary>
  /// The full unsigned amount (in document currency)
  /// on which the adjusting document balance is affected
  /// by the application.
  /// </summary>
  public Decimal CurrencyAdjustingBalanceDelta;
  /// <summary>
  /// The full unsigned amount (in base currency)
  /// on which the adjusted document balance is affected
  /// by the application.
  /// </summary>
  public Decimal BaseAdjustedBalanceDelta;
  /// <summary>
  /// The full unsigned amount (in base currency)
  /// on which the adjusting document balance is affected
  /// by the application.
  /// </summary>
  public Decimal BaseAdjustingBalanceDelta;
}
