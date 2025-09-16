// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.AnnouncementMajorStatusesAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
[Obsolete]
public class AnnouncementMajorStatusesAttribute : PXIntListAttribute
{
  public const int _DRAFT = 0;
  public const int _PUBLISHED = 1;
  public const int _ARCHIVED = 2;

  [Obsolete]
  public AnnouncementMajorStatusesAttribute()
    : base(new int[3]{ 0, 1, 2 }, new string[3]
    {
      "Draft",
      "Published",
      "Archived"
    })
  {
  }
}
