// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectCostProjectionByDateStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.PM;

public class ProjectCostProjectionByDateStatus
{
  public const 
  #nullable disable
  string OnHold = "H";
  public const string Open = "O";
  public const string Released = "C";
  public const string PendingApproval = "A";
  public const string Rejected = "R";
  public static readonly string[] Values = new string[5]
  {
    "H",
    "A",
    "O",
    "R",
    "C"
  };
  public static readonly string[] Labels = new string[5]
  {
    "On Hold",
    "Pending Approval",
    nameof (Open),
    nameof (Rejected),
    nameof (Released)
  };

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(ProjectCostProjectionByDateStatus.Values, ProjectCostProjectionByDateStatus.Labels)
    {
    }
  }

  public class onHold : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ProjectCostProjectionByDateStatus.onHold>
  {
    public onHold()
      : base("H")
    {
    }
  }

  public class pendingApproval : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ProjectCostProjectionByDateStatus.pendingApproval>
  {
    public pendingApproval()
      : base("A")
    {
    }
  }

  public class open : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProjectCostProjectionByDateStatus.open>
  {
    public open()
      : base("O")
    {
    }
  }

  public class rejected : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ProjectCostProjectionByDateStatus.rejected>
  {
    public rejected()
      : base("R")
    {
    }
  }

  public class released : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ProjectCostProjectionByDateStatus.released>
  {
    public released()
      : base("C")
    {
    }
  }
}
