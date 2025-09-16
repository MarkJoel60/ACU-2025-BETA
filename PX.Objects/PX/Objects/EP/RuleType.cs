// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.RuleType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.EP;

public static class RuleType
{
  public const string AllTrue = "A";
  public const string AtleastOneConditionIsTrue = "T";
  public const string AtleastOneConditionIsFalse = "F";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "A", "T", "F" }, new string[3]
      {
        "All conditions are true",
        "At least one condition is true",
        "At least one condition is false"
      })
    {
    }
  }
}
