// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PXNotDecimalUnitException
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.IN;

/// <summary>
/// This exception type raised when Unit of Inventory Item is indivisible(<see cref="P:PX.Objects.IN.InventoryItem.DecimalBaseUnit" />, <see cref="!:InventoryItem.DecimalSelesUnit" /> or <see cref="P:PX.Objects.IN.InventoryItem.DecimalPurchaseUnit" /> is set <c>false</c>)
/// and the entered value is non integer
/// </summary>
public class PXNotDecimalUnitException : PXSetPropertyException
{
  private static string GetMessageFormat(InventoryUnitType unitType)
  {
    switch (unitType)
    {
      case InventoryUnitType.BaseUnit:
        return "The {0} base UOM is not divisible for the {1} item. Check conversion rules.";
      case InventoryUnitType.SalesUnit:
        return "The {0} sales UOM is not divisible for the {1} item.";
      case InventoryUnitType.PurchaseUnit:
        return "The {0} purchase UOM is not divisible for the {1} item.";
      default:
        throw new ArgumentOutOfRangeException(nameof (unitType));
    }
  }

  public bool IsLazyThrow { get; set; }

  public PXNotDecimalUnitException(
    InventoryUnitType unitType,
    string inventoryCD,
    string unitID,
    PXErrorLevel errorLevel)
    : base(PXNotDecimalUnitException.GetMessageFormat(unitType), errorLevel, new object[2]
    {
      (object) unitID,
      (object) inventoryCD
    })
  {
  }

  public PXNotDecimalUnitException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
