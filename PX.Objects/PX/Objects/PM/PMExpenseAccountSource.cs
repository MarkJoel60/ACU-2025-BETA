// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMExpenseAccountSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
public static class PMExpenseAccountSource
{
  public const string Project = "P";
  public const string Task = "T";
  public const string InventoryItem = "I";
  public const string Employee = "E";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("I", "Labor Item"),
        PXStringListAttribute.Pair("P", "Project"),
        PXStringListAttribute.Pair("T", "Task"),
        PXStringListAttribute.Pair("E", "Employee")
      })
    {
    }
  }

  public class AccrualListAttribute : PXStringListAttribute
  {
    public AccrualListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("I", "Labor Item Accrual"),
        PXStringListAttribute.Pair("P", "Project Accrual"),
        PXStringListAttribute.Pair("T", "Task Accrual")
      })
    {
    }
  }
}
