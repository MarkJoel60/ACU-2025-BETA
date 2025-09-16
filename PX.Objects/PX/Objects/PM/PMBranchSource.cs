// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMBranchSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
public static class PMBranchSource
{
  public const string None = "N";
  public const string BillingRule = "B";
  public const string Project = "P";
  public const string Task = "T";

  [ExcludeFromCodeCoverage]
  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "N", "B", "P", "T" }, new string[4]
      {
        "None",
        "Billing Rule",
        "Project",
        "Task"
      })
    {
    }
  }
}
