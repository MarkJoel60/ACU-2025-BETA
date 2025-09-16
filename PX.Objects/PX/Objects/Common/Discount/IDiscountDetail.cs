// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.IDiscountDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.Common.Discount;

public interface IDiscountDetail
{
  int? RecordID { get; set; }

  ushort? LineNbr { get; set; }

  bool? SkipDiscount { get; set; }

  string DiscountID { get; set; }

  string DiscountSequenceID { get; set; }

  string Type { get; set; }

  Decimal? CuryDiscountableAmt { get; set; }

  Decimal? DiscountableQty { get; set; }

  Decimal? CuryDiscountAmt { get; set; }

  Decimal? DiscountPct { get; set; }

  int? FreeItemID { get; set; }

  Decimal? FreeItemQty { get; set; }

  bool? IsManual { get; set; }

  bool? IsOrigDocDiscount { get; set; }

  string ExtDiscCode { get; set; }

  string Description { get; set; }
}
