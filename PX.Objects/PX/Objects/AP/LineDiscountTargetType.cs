// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.LineDiscountTargetType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AP;

public static class LineDiscountTargetType
{
  public const string ExtendedPrice = "E";
  public const string SalesPrice = "S";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "E", "S" }, new string[2]
      {
        "Extended Cost",
        "Unit Cost"
      })
    {
    }
  }
}
