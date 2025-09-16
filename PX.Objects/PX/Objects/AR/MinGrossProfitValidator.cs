// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.MinGrossProfitValidator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.AR;

public static class MinGrossProfitValidator
{
  public static Decimal? Validate<TField>(
    PXCache sender,
    object line,
    PX.Objects.IN.InventoryItem inItem,
    string validationMode,
    Decimal? currentValue,
    Decimal? minValue,
    Decimal? newValue,
    Decimal? setToMinValue,
    MinGrossProfitValidator.Target target)
    where TField : IBqlField
  {
    Decimal? nullable1 = currentValue;
    Decimal? nullable2 = minValue;
    if (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
    {
      switch (validationMode)
      {
        case "W":
          sender.RaiseExceptionHandling(typeof (TField).Name, line, (object) newValue, (Exception) new PXSetPropertyException("Minimum Gross Profit requirement is not satisfied.", (PXErrorLevel) 2));
          break;
        case "S":
          newValue = setToMinValue;
          sender.RaiseExceptionHandling(typeof (TField).Name, line, (object) newValue, (Exception) new PXSetPropertyException(target.ToFixWarning(), (PXErrorLevel) 2));
          break;
      }
    }
    else if (validationMode != "N")
    {
      Decimal? nullable3 = minValue;
      Decimal num = 0M;
      if (nullable3.GetValueOrDefault() == num & nullable3.HasValue && inItem.ValMethod != "T")
        sender.RaiseExceptionHandling(typeof (TField).Name, line, (object) newValue, (Exception) new PXSetPropertyException("No Average or Last cost found to determine minimum valid Unit price.", (PXErrorLevel) 2));
    }
    return newValue;
  }

  public enum Target
  {
    Discount,
    SalesPrice,
    UnitPrice,
  }
}
