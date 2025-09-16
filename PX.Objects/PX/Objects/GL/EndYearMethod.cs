// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.EndYearMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.GL;

public class EndYearMethod
{
  public const string Calendar = "CA";
  public const string LastDay = "LD";
  public const string NearestDay = "ND";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "CA", "LD", "ND" }, new string[3]
      {
        "Last Day of the Financial Year",
        "Include Last <Day of Week> of the Financial Year",
        "Include <Day of Week> Nearest to the End of the Financial Year"
      })
    {
    }
  }
}
