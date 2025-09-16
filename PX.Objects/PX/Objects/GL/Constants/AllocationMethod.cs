// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Constants.AllocationMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.GL.Constants;

public static class AllocationMethod
{
  public const string ByPercent = "C";
  public const string ByWeight = "W";
  public const string ByAcctPTD = "P";
  public const string ByAcctYTD = "Y";
  public const string ByExternalRule = "E";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "C", "W", "P", "Y" }, new string[4]
      {
        "By Percent",
        "By Weight",
        "By Dest. Account PTD",
        "By Dest. Account YTD"
      })
    {
    }
  }
}
