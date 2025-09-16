// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.DAC.OrganizationTypes
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.GL.DAC;

public class OrganizationTypes
{
  public const 
  #nullable disable
  string WithoutBranches = "WithoutBranches";
  public const string WithBranchesNotBalancing = "NotBalancing";
  public const string WithBranchesBalancing = "Balancing";
  public const string Group = "Group";

  public class withoutBranches : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    OrganizationTypes.withoutBranches>
  {
    public withoutBranches()
      : base("WithoutBranches")
    {
    }
  }

  public class withBranchesNotBalancing : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    OrganizationTypes.withBranchesNotBalancing>
  {
    public withBranchesNotBalancing()
      : base("NotBalancing")
    {
    }
  }

  public class withBranchesBalancing : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    OrganizationTypes.withBranchesBalancing>
  {
    public withBranchesBalancing()
      : base("Balancing")
    {
    }
  }

  public class group : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  OrganizationTypes.group>
  {
    public group()
      : base("Group")
    {
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    protected string[] ExcludedTypes { get; set; }

    public ListAttribute()
    {
    }

    public ListAttribute(params string[] excludedTypes) => this.ExcludedTypes = excludedTypes;

    public virtual void CacheAttached(PXCache sender)
    {
      List<string> stringList1 = new List<string>();
      List<string> stringList2 = new List<string>();
      stringList1.Add("WithoutBranches");
      stringList2.Add("Without Branches");
      if (PXAccess.FeatureInstalled<FeaturesSet.branch>())
      {
        stringList1.Add("NotBalancing");
        stringList2.Add("With Branches Not Requiring Balancing");
        if (PXAccess.FeatureInstalled<FeaturesSet.interBranch>())
        {
          stringList1.Add("Balancing");
          stringList2.Add("With Branches Requiring Balancing");
        }
      }
      if (PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>())
      {
        stringList1.Add("Group");
        stringList2.Add("Organization Group");
      }
      if (this.ExcludedTypes != null)
      {
        foreach (string excludedType in this.ExcludedTypes)
        {
          if (stringList1.Contains(excludedType))
          {
            int index = stringList1.IndexOf(excludedType);
            stringList1.RemoveAt(index);
            stringList2.RemoveAt(index);
          }
        }
      }
      this._AllowedValues = stringList1.ToArray();
      this._AllowedLabels = stringList2.ToArray();
      this._NeutralAllowedLabels = (string[]) null;
      base.CacheAttached(sender);
    }
  }
}
