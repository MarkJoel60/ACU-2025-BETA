// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PriceBasisTypes
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AP;

public static class PriceBasisTypes
{
  public const string LastCost = "L";
  public const string StdCost = "S";
  public const string CurrentPrice = "P";
  public const string PendingPrice = "N";
  public const string RecommendedPrice = "R";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[5]{ "L", "S", "P", "N", "R" }, new string[5]
      {
        "Last Cost",
        "Avg./Std. Cost",
        "Source Price",
        "Pending Price",
        "MSRP"
      })
    {
    }
  }
}
