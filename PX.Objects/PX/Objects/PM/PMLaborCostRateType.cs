// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMLaborCostRateType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
public static class PMLaborCostRateType
{
  public const 
  #nullable disable
  string All = "A";
  public const string Employee = "E";
  public const string Union = "U";
  public const string Certified = "C";
  public const string Project = "P";
  public const string Item = "I";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(PMLaborCostRateType.ListAttribute.GetListBasedOnFeatures().ToArray())
    {
    }

    public static List<Tuple<string, string>> GetListBasedOnFeatures()
    {
      List<Tuple<string, string>> listBasedOnFeatures = new List<Tuple<string, string>>();
      listBasedOnFeatures.Add(PXStringListAttribute.Pair("E", "Employee"));
      if (PXAccess.FeatureInstalled<FeaturesSet.construction>() || PXAccess.FeatureInstalled<FeaturesSet.payrollModule>())
        listBasedOnFeatures.Add(PXStringListAttribute.Pair("U", "Union Wage"));
      if (PXAccess.FeatureInstalled<FeaturesSet.construction>() || PXAccess.FeatureInstalled<FeaturesSet.payrollUS>())
        listBasedOnFeatures.Add(PXStringListAttribute.Pair("C", "Prevailing Wage"));
      listBasedOnFeatures.Add(PXStringListAttribute.Pair("P", "Project"));
      listBasedOnFeatures.Add(PXStringListAttribute.Pair("I", "Labor Item"));
      return listBasedOnFeatures;
    }
  }

  public class FilterListAttribute : PXStringListAttribute
  {
    public FilterListAttribute()
      : base(PMLaborCostRateType.FilterListAttribute.GetListBasedOnFeatures().ToArray())
    {
    }

    public static List<Tuple<string, string>> GetListBasedOnFeatures()
    {
      List<Tuple<string, string>> listBasedOnFeatures = new List<Tuple<string, string>>();
      listBasedOnFeatures.Add(PXStringListAttribute.Pair("A", "All"));
      listBasedOnFeatures.Add(PXStringListAttribute.Pair("E", "Employee"));
      if (PXAccess.FeatureInstalled<FeaturesSet.construction>() || PXAccess.FeatureInstalled<FeaturesSet.payrollModule>())
      {
        listBasedOnFeatures.Add(PXStringListAttribute.Pair("U", "Union Wage"));
        listBasedOnFeatures.Add(PXStringListAttribute.Pair("C", "Prevailing Wage"));
      }
      if (PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>())
        listBasedOnFeatures.Add(PXStringListAttribute.Pair("P", "Project"));
      listBasedOnFeatures.Add(PXStringListAttribute.Pair("I", "Labor Item"));
      return listBasedOnFeatures;
    }
  }

  public class union : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMLaborCostRateType.union>
  {
    public union()
      : base("U")
    {
    }
  }

  public class certified : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMLaborCostRateType.certified>
  {
    public certified()
      : base("C")
    {
    }
  }

  public class project : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMLaborCostRateType.project>
  {
    public project()
      : base("P")
    {
    }
  }

  public class item : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMLaborCostRateType.item>
  {
    public item()
      : base("I")
    {
    }
  }

  public class employee : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMLaborCostRateType.employee>
  {
    public employee()
      : base("E")
    {
    }
  }

  public class all : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMLaborCostRateType.all>
  {
    public all()
      : base("A")
    {
    }
  }
}
