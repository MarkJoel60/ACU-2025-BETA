// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMMarkupLineType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.PM;

public static class PMMarkupLineType
{
  public const string Percentage = "P";
  public const string FlatFee = "F";
  public const string Cumulative = "C";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "P", "F", "C" }, new string[3]
      {
        "%",
        "Flat Fee",
        "Cumulative (%)"
      })
    {
    }
  }
}
