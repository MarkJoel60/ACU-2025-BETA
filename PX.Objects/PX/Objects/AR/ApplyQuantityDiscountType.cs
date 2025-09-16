// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ApplyQuantityDiscountType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AR;

public static class ApplyQuantityDiscountType
{
  public const string DocumentLineUOM = "L";
  public const string BaseUOM = "B";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "L", "B" }, new string[2]
      {
        "Document Line UOM",
        "Base UOM"
      })
    {
    }
  }
}
