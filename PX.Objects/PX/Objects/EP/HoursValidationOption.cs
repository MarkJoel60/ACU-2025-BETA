// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.HoursValidationOption
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.EP;

public static class HoursValidationOption
{
  public const string Validate = "V";
  public const string WarningOnly = "W";
  public const string None = "N";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "V", "W", "N" }, new string[3]
      {
        "Validate",
        "Warning Only",
        "None"
      })
    {
    }
  }
}
