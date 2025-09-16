// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.ShippingApplicationTypes
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CS;

public static class ShippingApplicationTypes
{
  public const string UPS = "UPS";
  public const string FEDEX = "FED";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "UPS", "FED" }, new string[2]
      {
        "UPS WorldShip",
        "FedEx Ship Manager"
      })
    {
    }
  }
}
