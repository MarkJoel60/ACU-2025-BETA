// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.AcctSubDefault
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PM;

public class AcctSubDefault
{
  public const string Source = "S";
  public const string BillingRule = "B";
  public const string RecurentBilling = "B";
  public const string Inventory = "I";
  public const string Customer = "C";
  public const string Project = "J";
  public const string Task = "T";
  public const string Employee = "E";
  public const string Branch = "R";
  public const string PostingClass = "P";

  public class CustomListAttribute : PXStringListAttribute
  {
    public string[] AllowedValues => this._AllowedValues;

    public string[] AllowedLabels => this._AllowedLabels;

    public CustomListAttribute(string[] AllowedValues, string[] AllowedLabels)
      : base(AllowedValues, AllowedLabels)
    {
    }

    public CustomListAttribute(Tuple<string, string>[] valuesToLabels)
      : base(valuesToLabels)
    {
    }
  }

  public class SubListAttribute : AcctSubDefault.CustomListAttribute
  {
    public SubListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("I", "Inventory Item"),
        PXStringListAttribute.Pair("J", "Project"),
        PXStringListAttribute.Pair("T", "Task"),
        PXStringListAttribute.Pair("E", "Employee")
      })
    {
    }
  }

  public class BillingSubListAttribute : AcctSubDefault.CustomListAttribute
  {
    public BillingSubListAttribute()
      : base(new Tuple<string, string>[8]
      {
        PXStringListAttribute.Pair("B", "Billing Rule"),
        PXStringListAttribute.Pair("J", "Project"),
        PXStringListAttribute.Pair("T", "Task"),
        PXStringListAttribute.Pair("E", "Employee"),
        PXStringListAttribute.Pair("S", "Source Transaction"),
        PXStringListAttribute.Pair("I", "Inventory Item"),
        PXStringListAttribute.Pair("C", "Customer"),
        PXStringListAttribute.Pair("R", "Branch")
      })
    {
    }
  }

  public class BillingBudgetSubListAttribute : AcctSubDefault.CustomListAttribute
  {
    public BillingBudgetSubListAttribute()
      : base(new string[6]{ "B", "J", "T", "I", "C", "R" }, new string[6]
      {
        "Billing Rule",
        "Project",
        "Task",
        "Inventory Item",
        "Customer",
        "Branch"
      })
    {
    }
  }

  public class RecurentBillingSubListAttribute : AcctSubDefault.CustomListAttribute
  {
    public RecurentBillingSubListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("B", "Recurring Item"),
        PXStringListAttribute.Pair("J", "Project"),
        PXStringListAttribute.Pair("T", "Task")
      })
    {
    }
  }

  public class DropshipExpenseSubListAttribute : AcctSubDefault.CustomListAttribute
  {
    public DropshipExpenseSubListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("I", "Inventory Item"),
        PXStringListAttribute.Pair("P", "Posting Class"),
        PXStringListAttribute.Pair("J", "Project"),
        PXStringListAttribute.Pair("T", "Task")
      })
    {
    }
  }
}
