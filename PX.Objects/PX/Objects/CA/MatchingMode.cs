// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.MatchingMode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CA;

public class MatchingMode
{
  public const string None = "N";
  public const string Equal = "E";
  public const string Between = "B";

  public class AmountAttribute : PXStringListAttribute
  {
    public AmountAttribute()
      : base(new string[3]{ "N", "E", "B" }, new string[3]
      {
        "None",
        "Equal",
        "Between"
      })
    {
    }
  }
}
