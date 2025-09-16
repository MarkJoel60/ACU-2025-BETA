// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PIGenerationMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

public static class PIGenerationMethod
{
  public const string FullPhysicalInventory = "FPI";
  public const string ByMovementClassCountFrequency = "MCF";
  public const string ByABCClassCountFrequency = "ACF";
  public const string ByCycleID = "BCI";
  public const string ByCycleCountFrequency = "CCF";
  public const string LastCountDate = "LCD";
  public const string ByPreviousPIID = "PPI";
  public const string ByItemClassID = "BIC";
  public const string ListOfItems = "LOI";
  public const string RandomlySelectedItems = "RSI";
  public const string ItemsHavingNegativeBookQty = "HNQ";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[11]
      {
        PXStringListAttribute.Pair("FPI", "Full Physical Inventory"),
        PXStringListAttribute.Pair("CCF", "By Cycle Count Frequency"),
        PXStringListAttribute.Pair("MCF", "By Movement Class Count Frequency"),
        PXStringListAttribute.Pair("ACF", "By ABC Code Count Frequency"),
        PXStringListAttribute.Pair("BCI", "By Cycle"),
        PXStringListAttribute.Pair("LCD", "Last Count On or Before"),
        PXStringListAttribute.Pair("PPI", "By Previous Physical Count"),
        PXStringListAttribute.Pair("BIC", "By Item Class"),
        PXStringListAttribute.Pair("LOI", "List of Items"),
        PXStringListAttribute.Pair("RSI", "Random Items"),
        PXStringListAttribute.Pair("HNQ", "Items Having Negative Book Qty.")
      })
    {
    }
  }
}
