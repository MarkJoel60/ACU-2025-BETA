// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.RenewalOption
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CT;

public static class RenewalOption
{
  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "I", "P", "B", "M" }, new string[4]
      {
        "Use Item Price",
        "Percent of Item Price",
        "Percent of Setup Price",
        "Enter Manually"
      })
    {
    }
  }
}
