// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.UnitPriceVal
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.Common.Discount;

public struct UnitPriceVal(bool skipLineDiscount)
{
  public Decimal? CuryUnitPrice = new Decimal?(0M);
  public bool isBAccountSpecific = false;
  public bool isPriceClassSpecific = false;
  public bool isPromotional = false;
  public bool skipLineDiscount = skipLineDiscount;
}
