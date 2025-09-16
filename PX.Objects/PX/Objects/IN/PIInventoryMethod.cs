// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PIInventoryMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

public static class PIInventoryMethod
{
  public const string ItemsHavingNegativeBookQty = "N";
  public const string RandomlySelectedItems = "R";
  public const string ListOfItems = "L";
  public const string LastCountDate = "I";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("N", "Items Having Negative Book Qty."),
        PXStringListAttribute.Pair("R", "Random Items"),
        PXStringListAttribute.Pair("L", "List of Items"),
        PXStringListAttribute.Pair("I", "Last Count On or Before")
      })
    {
    }
  }
}
