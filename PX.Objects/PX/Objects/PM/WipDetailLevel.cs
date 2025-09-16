// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.WipDetailLevel
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.PM;

public static class WipDetailLevel
{
  public const 
  #nullable disable
  string ProjectTaskOnly = "A";
  public const string TaskAndCostCode = "C";
  public static readonly string[] NoCostCodeValues = new string[1]
  {
    "A"
  };
  public static readonly string[] NoCostCodeLabels = new string[1]
  {
    "Project Task"
  };
  public static readonly string[] CostCodeValues = new string[2]
  {
    "A",
    "C"
  };
  public static readonly string[] CostCodeLabels = new string[2]
  {
    "Project Task",
    "Project Task and Cost Code"
  };

  public class projectTaskOnly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  WipDetailLevel.projectTaskOnly>
  {
    public projectTaskOnly()
      : base("A")
    {
    }
  }

  public class taskAndCostCode : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  WipDetailLevel.taskAndCostCode>
  {
    public taskAndCostCode()
      : base("C")
    {
    }
  }

  public class StringListAttribute : PXStringListAttribute
  {
    public StringListAttribute()
      : base(Array.Empty<string>(), Array.Empty<string>())
    {
    }

    public virtual void CacheAttached(PXCache sender)
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.costCodes>())
      {
        this._AllowedValues = WipDetailLevel.CostCodeValues;
        this._AllowedLabels = WipDetailLevel.CostCodeLabels;
      }
      else
      {
        this._AllowedValues = WipDetailLevel.NoCostCodeValues;
        this._AllowedLabels = WipDetailLevel.NoCostCodeLabels;
      }
      base.CacheAttached(sender);
    }
  }
}
