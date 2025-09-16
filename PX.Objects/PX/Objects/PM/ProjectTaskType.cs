// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectTaskType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.PM;

public class ProjectTaskType
{
  public const 
  #nullable disable
  string Cost = "Cost";
  public const string Revenue = "Rev";
  public const string CostRevenue = "CostRev";

  public class ListAttribute : PXStringListAttribute
  {
    private static readonly string[] TaskTypesValues = new string[3]
    {
      "Cost",
      "Rev",
      "CostRev"
    };
    private static readonly string[] TaskTypesLabels = new string[3]
    {
      "Cost Task",
      "Revenue Task",
      "Cost and Revenue Task"
    };

    public ListAttribute()
      : base(ProjectTaskType.ListAttribute.TaskTypesValues, ProjectTaskType.ListAttribute.TaskTypesLabels)
    {
    }
  }

  public class cost : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProjectTaskType.cost>
  {
    public cost()
      : base("Cost")
    {
    }
  }

  public class revenue : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProjectTaskType.revenue>
  {
    public revenue()
      : base("Rev")
    {
    }
  }

  public class costRevenue : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProjectTaskType.costRevenue>
  {
    public costRevenue()
      : base("CostRev")
    {
    }
  }
}
