// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProgressBillingBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.PM;

public static class ProgressBillingBase
{
  public const string Quantity = "Q";
  public const string Amount = "A";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "Q", "A" }, new string[2]
      {
        "Quantity",
        "Amount"
      })
    {
    }
  }
}
