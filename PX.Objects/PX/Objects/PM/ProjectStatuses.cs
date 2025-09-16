// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectStatuses
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PM;

public static class ProjectStatuses
{
  public const string Planned = "D";
  public const string Active = "A";
  public const string Completed = "F";
  public const string Cancelled = "C";
  public const string Suspended = "E";
  public const string PendingApproval = "I";
  public const string Rejected = "R";
  public const string ActiveCompleted = "AF";
  public const string All = "DAFCEIR";

  public class ProjectStatusListAttribute : PXStringListAttribute
  {
    public ProjectStatusListAttribute()
      : base(new Tuple<string, string>[9]
      {
        PXStringListAttribute.Pair("D", "In Planning"),
        PXStringListAttribute.Pair("A", "Active"),
        PXStringListAttribute.Pair("F", "Completed"),
        PXStringListAttribute.Pair("C", "Canceled"),
        PXStringListAttribute.Pair("E", "Suspended"),
        PXStringListAttribute.Pair("I", "Pending Approval"),
        PXStringListAttribute.Pair("R", "Rejected"),
        PXStringListAttribute.Pair("AF", "Active, Completed"),
        PXStringListAttribute.Pair("DAFCEIR", "All")
      })
    {
    }
  }
}
