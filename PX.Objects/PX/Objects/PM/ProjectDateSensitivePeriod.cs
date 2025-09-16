// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectDateSensitivePeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.PM;

public class ProjectDateSensitivePeriod
{
  public static readonly string[] Values = new string[4]
  {
    "D",
    "M",
    "Q",
    "Y"
  };
  public static readonly string[] Labels = new string[4]
  {
    nameof (Day),
    nameof (Month),
    nameof (Quarter),
    nameof (Year)
  };
  public const string Day = "D";
  public const string Month = "M";
  public const string Quarter = "Q";
  public const string Year = "Y";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(ProjectDateSensitivePeriod.Values, ProjectDateSensitivePeriod.Labels)
    {
    }
  }
}
