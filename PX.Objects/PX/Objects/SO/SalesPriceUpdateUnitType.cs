// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SalesPriceUpdateUnitType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.SO;

public static class SalesPriceUpdateUnitType
{
  public const string BaseUnit = "B";
  public const string SalesUnit = "S";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("B", "Base Unit"),
        PXStringListAttribute.Pair("S", "Sales Unit")
      })
    {
    }
  }
}
