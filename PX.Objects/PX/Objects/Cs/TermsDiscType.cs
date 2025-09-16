// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.TermsDiscType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CS;

public class TermsDiscType : TermsDueType
{
  public new class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[6]{ "N", "D", "E", "M", "T", "P" }, new string[6]
      {
        "Fixed Number of Days",
        "Day of Next Month",
        "End of Month",
        "End of Next Month",
        "Day of the Month",
        "Fixed Number of Days starting Next Month"
      })
    {
    }
  }
}
