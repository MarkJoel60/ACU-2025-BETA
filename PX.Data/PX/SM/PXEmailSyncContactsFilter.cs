// Decompiled with JetBrains decompiler
// Type: PX.SM.PXEmailSyncContactsFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;

#nullable disable
namespace PX.SM;

public static class PXEmailSyncContactsFilter
{
  public const string OwnerCode = "Owner";
  public const string WorkgroupCode = "Workgroup";

  public static string Parse(PXEmailSyncContactsFilter.Filter filter)
  {
    if (filter == PXEmailSyncContactsFilter.Filter.Owner)
      return "Owner";
    if (filter == PXEmailSyncContactsFilter.Filter.Workgroup)
      return "Workgroup";
    throw new PXException("Unknown contacts filter.");
  }

  public static PXEmailSyncContactsFilter.Filter Parse(string code)
  {
    switch (code)
    {
      case "Owner":
        return PXEmailSyncContactsFilter.Filter.Owner;
      case "Workgroup":
        return PXEmailSyncContactsFilter.Filter.Workgroup;
      default:
        throw new PXException("Unknown contacts filter.");
    }
  }

  [Flags]
  public enum Filter
  {
    All = 0,
    Owner = 1,
    OwnerVisible = 2,
    Workgroup = OwnerVisible | Owner, // 0x00000003
    WorkgroupVisible = 4,
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "Owner", "Workgroup" }, new string[2]
      {
        "Owner",
        "Workgroup"
      })
    {
    }
  }
}
