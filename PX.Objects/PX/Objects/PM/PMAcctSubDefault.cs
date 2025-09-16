// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMAcctSubDefault
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PM;

public class PMAcctSubDefault
{
  public const string MaskSource = "S";
  public const string AllocationStep = "A";
  public const string ProjectSales = "J";
  public const string TaskSales = "T";
  public const string ProjectCost = "C";
  public const string TaskCost = "D";
  public const string MaskOffsetSource = "G";

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

  public class SubListAttribute : PMAcctSubDefault.CustomListAttribute
  {
    public SubListAttribute()
      : base(new Tuple<string, string>[7]
      {
        PXStringListAttribute.Pair("S", "Debit Transaction"),
        PXStringListAttribute.Pair("A", "Allocation Step"),
        PXStringListAttribute.Pair("J", "Project Sales"),
        PXStringListAttribute.Pair("T", "Task Sales"),
        PXStringListAttribute.Pair("C", "Project Cost"),
        PXStringListAttribute.Pair("D", "Task Cost"),
        PXStringListAttribute.Pair("G", "Credit Transaction")
      })
    {
    }
  }

  public class OffsetSubListAttribute : PMAcctSubDefault.CustomListAttribute
  {
    public OffsetSubListAttribute()
      : base(new Tuple<string, string>[7]
      {
        PXStringListAttribute.Pair("S", "Credit Transaction"),
        PXStringListAttribute.Pair("A", "Allocation Step"),
        PXStringListAttribute.Pair("J", "Project Sales"),
        PXStringListAttribute.Pair("T", "Task Sales"),
        PXStringListAttribute.Pair("C", "Project Cost"),
        PXStringListAttribute.Pair("D", "Task Cost"),
        PXStringListAttribute.Pair("G", "Debit Transaction")
      })
    {
    }
  }
}
