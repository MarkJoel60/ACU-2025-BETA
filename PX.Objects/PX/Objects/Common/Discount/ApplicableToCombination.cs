// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.ApplicableToCombination
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.Common.Discount;

/// <summary>
/// Used for calculation of intersections of discounts. Every new value should be power of two.
/// </summary>
[Flags]
public enum ApplicableToCombination
{
  None = 0,
  Customer = 1,
  InventoryItem = 2,
  CustomerPriceClass = 4,
  InventoryPriceClass = 8,
  Vendor = 16, // 0x00000010
  Warehouse = 32, // 0x00000020
  Branch = 64, // 0x00000040
  Location = 128, // 0x00000080
  Unconditional = 256, // 0x00000100
}
