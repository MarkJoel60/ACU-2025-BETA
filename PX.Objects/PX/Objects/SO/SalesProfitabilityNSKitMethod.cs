// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SalesProfitabilityNSKitMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.SO;

public static class SalesProfitabilityNSKitMethod
{
  public const string StockComponentsCostOnly = "S";
  public const string NSKitStandardCostOnly = "K";
  public const string NSKitStandardAndStockComponentsCost = "C";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "S", "K", "C" }, new string[3]
      {
        "Stock Component Cost",
        "Non-Stock Kit Standard Cost",
        "Non-Stock Kit Standard Cost Plus Stock Component Cost"
      })
    {
    }
  }
}
