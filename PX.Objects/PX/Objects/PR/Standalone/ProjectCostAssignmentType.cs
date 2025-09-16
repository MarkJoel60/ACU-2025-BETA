// Decompiled with JetBrains decompiler
// Type: PX.Objects.PR.Standalone.ProjectCostAssignmentType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.PR.Standalone;

public class ProjectCostAssignmentType
{
  public const 
  #nullable disable
  string NoCostAssigned = "NCA";
  public const string WageCostAssigned = "WCA";
  public const string WageLaborBurdenAssigned = "WLB";

  public class noCostAssigned : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ProjectCostAssignmentType.noCostAssigned>
  {
    public noCostAssigned()
      : base("NCA")
    {
    }
  }

  public class wageCostAssigned : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ProjectCostAssignmentType.wageCostAssigned>
  {
    public wageCostAssigned()
      : base("WCA")
    {
    }
  }

  public class wageLaborBurdenAssigned : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ProjectCostAssignmentType.wageLaborBurdenAssigned>
  {
    public wageLaborBurdenAssigned()
      : base("WLB")
    {
    }
  }
}
