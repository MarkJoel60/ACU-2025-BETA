// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.OverLimitValidationOption
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
public static class OverLimitValidationOption
{
  public const string Error = "E";
  public const string Warning = "W";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "E", "W" }, new string[2]
      {
        "Validate",
        "Ignore"
      })
    {
    }
  }
}
