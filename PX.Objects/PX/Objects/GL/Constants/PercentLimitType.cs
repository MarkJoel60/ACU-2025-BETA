// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Constants.PercentLimitType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.GL.Constants;

public static class PercentLimitType
{
  public const string ByAllocation = "A";
  public const string ByPeriod = "P";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "P", "A" }, new string[2]
      {
        "Period",
        "GL Allocation"
      })
    {
    }
  }
}
