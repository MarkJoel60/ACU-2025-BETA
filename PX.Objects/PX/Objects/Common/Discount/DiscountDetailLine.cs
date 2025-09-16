// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.DiscountDetailLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.Common.Discount;

/// <summary>
/// Stores Discount Details lines combined with Discount Code and Discount Sequence fields
/// </summary>
public struct DiscountDetailLine
{
  public string DiscountID;
  public string DiscountSequenceID;
  public ApplicableToCombination ApplicableToCombined;
  public string Type;
  public string DiscountedFor;
  public string BreakBy;
  public Decimal? AmountFrom;
  public Decimal? AmountTo;
  public Decimal? Discount;
  public int? freeItemID;
  public Decimal? freeItemQty;
  public bool? Prorate;
  public string ExtDiscCode;
  public string Description;
}
