// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PriceOfSuggestedItemsList
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.IN;

public class PriceOfSuggestedItemsList
{
  public const string AnyPrice = "A";
  public const string LowerPrice = "L";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "A", "L" }, new string[2]
      {
        "Any Price",
        "Lower Price"
      })
    {
    }
  }
}
