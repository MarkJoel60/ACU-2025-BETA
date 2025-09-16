// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryUnitType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.IN;

[Flags]
public enum InventoryUnitType : byte
{
  /// <summary>Default(unknown) type of unit</summary>
  None = 0,
  /// <summary>
  /// Corresponds to unit which set as <see cref="P:PX.Objects.IN.InventoryItem.BaseUnit" />
  /// </summary>
  BaseUnit = 1,
  /// <summary>
  /// Corresponds to unit which set as <see cref="P:PX.Objects.IN.InventoryItem.SalesUnit" />
  /// </summary>
  SalesUnit = 2,
  /// <summary>
  /// Corresponds to unit which set as <see cref="P:PX.Objects.IN.InventoryItem.PurchaseUnit" />
  /// </summary>
  PurchaseUnit = 4,
}
