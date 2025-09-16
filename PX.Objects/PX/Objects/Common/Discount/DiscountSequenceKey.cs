// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.DiscountSequenceKey
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common.Discount;

/// <summary>Stores discount sequence keys</summary>
public class DiscountSequenceKey
{
  public string DiscountID { get; }

  public string DiscountSequenceID { get; }

  public Decimal? CuryDiscountableAmount { get; set; }

  public Decimal? DiscountableQuantity { get; set; }

  internal DiscountSequenceKey(string discountID, string discountSequenceID)
  {
    this.DiscountID = discountID;
    this.DiscountSequenceID = discountSequenceID;
    this.CuryDiscountableAmount = new Decimal?(0M);
    this.DiscountableQuantity = new Decimal?(0M);
  }

  public override bool Equals(object obj)
  {
    if (obj == null || this.GetType() != obj.GetType())
      return false;
    DiscountSequenceKey discountSequenceKey = obj as DiscountSequenceKey;
    return discountSequenceKey.DiscountID == this.DiscountID && discountSequenceKey.DiscountSequenceID == this.DiscountSequenceID;
  }

  public override int GetHashCode()
  {
    int num1 = 17 * 11;
    int? hashCode1 = this.DiscountID?.GetHashCode();
    int num2 = (hashCode1.HasValue ? new int?(num1 + hashCode1.GetValueOrDefault()) : new int?()).GetValueOrDefault() * 11;
    int? hashCode2 = this.DiscountSequenceID?.GetHashCode();
    return (hashCode2.HasValue ? new int?(num2 + hashCode2.GetValueOrDefault()) : new int?()).GetValueOrDefault();
  }

  public class DiscountSequenceKeyComparer : IEqualityComparer<DiscountSequenceKey>
  {
    public bool Equals(
      DiscountSequenceKey discountSequenceKey1,
      DiscountSequenceKey discountSequenceKey2)
    {
      return discountSequenceKey1.DiscountID == discountSequenceKey2.DiscountID && discountSequenceKey1.DiscountSequenceID == discountSequenceKey2.DiscountSequenceID;
    }

    public int GetHashCode(DiscountSequenceKey discountSequenceKey)
    {
      int num1 = 17 * 11;
      int? hashCode1 = discountSequenceKey.DiscountID?.GetHashCode();
      int num2 = (hashCode1.HasValue ? new int?(num1 + hashCode1.GetValueOrDefault()) : new int?()).GetValueOrDefault() * 11;
      int? hashCode2 = discountSequenceKey.DiscountSequenceID?.GetHashCode();
      return (hashCode2.HasValue ? new int?(num2 + hashCode2.GetValueOrDefault()) : new int?()).GetValueOrDefault();
    }
  }
}
