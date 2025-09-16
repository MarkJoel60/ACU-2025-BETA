// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMAccountSource
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
public static class PMAccountSource
{
  public const string None = "N";
  public const string BillingRule = "B";
  public const string RecurringBillingItem = "B";
  public const string Project = "P";
  public const string Task = "T";
  public const string InventoryItem = "I";
  public const string Customer = "C";
  public const string Employee = "E";
  public const string AccountGroup = "A";
  public const string Branch = "R";

  [ExcludeFromCodeCoverage]
  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[6]
      {
        PXStringListAttribute.Pair("N", "Source Transaction"),
        PXStringListAttribute.Pair("B", "Billing Rule"),
        PXStringListAttribute.Pair("P", "Project"),
        PXStringListAttribute.Pair("T", "Task"),
        PXStringListAttribute.Pair("C", "Customer"),
        PXStringListAttribute.Pair("E", "Employee")
      })
    {
    }
  }

  [ExcludeFromCodeCoverage]
  public class ListAttributeBudget : PXStringListAttribute
  {
    public ListAttributeBudget()
      : base(new Tuple<string, string>[6]
      {
        PXStringListAttribute.Pair("N", "Current Branch"),
        PXStringListAttribute.Pair("B", "Billing Rule"),
        PXStringListAttribute.Pair("P", "Project"),
        PXStringListAttribute.Pair("T", "Task"),
        PXStringListAttribute.Pair("C", "Customer"),
        PXStringListAttribute.Pair("E", "Employee")
      })
    {
    }
  }

  [ExcludeFromCodeCoverage]
  public class RecurentListAttribute : PXStringListAttribute
  {
    public RecurentListAttribute()
      : base(new Tuple<string, string>[6]
      {
        PXStringListAttribute.Pair("N", "AR Default"),
        PXStringListAttribute.Pair("B", "Recurring Item"),
        PXStringListAttribute.Pair("P", "Project"),
        PXStringListAttribute.Pair("T", "Task"),
        PXStringListAttribute.Pair("I", "Inventory Item"),
        PXStringListAttribute.Pair("C", "Customer")
      })
    {
    }
  }
}
