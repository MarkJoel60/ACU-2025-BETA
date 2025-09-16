// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.BalanceFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL.Attributes;
using System;

#nullable enable
namespace PX.Objects.FA;

[Serializable]
public class BalanceFilter : ProcessAssetFilter
{
  [Organization(true)]
  [PXUIRequired(typeof (Where<FeatureInstalled<FeaturesSet.multipleCalendarsSupport>>))]
  public override int? OrganizationID { get; set; }

  [BranchOfOrganization(typeof (BalanceFilter.organizationID), true, null, typeof (FeaturesSet.multipleCalendarsSupport))]
  public override int? BranchID { get; set; }

  [OrganizationTree(typeof (BalanceFilter.organizationID), typeof (BalanceFilter.branchID), null, false)]
  public int? OrgBAccountID { get; set; }

  [PXUIField(DisplayName = "To Period")]
  [FABookPeriodOpenInGLSelector(null, null, typeof (ProcessAssetFilter.bookID), false, null, typeof (AccessInfo.businessDate), typeof (BalanceFilter.branchID), null, typeof (BalanceFilter.organizationID), null)]
  public virtual 
  #nullable disable
  string PeriodID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("C")]
  [BalanceFilter.action.List]
  [PXUIField(DisplayName = "Action", Required = true)]
  public virtual string Action { get; set; }

  public new abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BalanceFilter.organizationID>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BalanceFilter.branchID>
  {
  }

  public abstract class orgBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BalanceFilter.orgBAccountID>
  {
  }

  public abstract class periodID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BalanceFilter.organizationID>
  {
  }

  public abstract class action : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BalanceFilter.action>
  {
    public const string Calculate = "C";
    public const string Depreciate = "D";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "C", "D" }, new string[2]
        {
          "Calculate Only",
          "Depreciate"
        })
      {
      }
    }

    public class calculate : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    BalanceFilter.action.calculate>
    {
      public calculate()
        : base("C")
      {
      }
    }

    public class depreciate : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    BalanceFilter.action.depreciate>
    {
      public depreciate()
        : base("D")
      {
      }
    }
  }
}
