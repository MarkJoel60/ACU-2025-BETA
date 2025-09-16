// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiCategory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

public class WikiCategory
{
  public const string EndUser = "EU";
  public const string Admin = "ADMIN";
  public const string Consultant = "CON";
  public const string Reference = "REF";
  public const string QuickGuide = "QG";
  public const string Developer = "DEV";
  public const string UiDeveloper = "UIDEV";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[7]
      {
        "EU",
        "ADMIN",
        "CON",
        "REF",
        "QG",
        "DEV",
        "UIDEV"
      }, new string[7]
      {
        "End User",
        "Administrator",
        "Implementation Consultant",
        "Reference",
        "Quick Guides",
        "Developer",
        "UI Developer"
      })
    {
    }
  }
}
