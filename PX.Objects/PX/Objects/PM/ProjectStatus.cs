// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
public static class ProjectStatus
{
  public const 
  #nullable disable
  string All = "Z";
  public const string Planned = "D";
  public const string Active = "A";
  public const string Completed = "F";
  public const string Suspended = "E";
  public const string Cancelled = "C";
  public const string PendingApproval = "I";
  public const string OnHold = "H";
  public const string Rejected = "R";
  public const string Closed = "L";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[10]
      {
        PXStringListAttribute.Pair("D", "In Planning"),
        PXStringListAttribute.Pair("A", "Active"),
        PXStringListAttribute.Pair("F", "Completed"),
        PXStringListAttribute.Pair("C", "Canceled"),
        PXStringListAttribute.Pair("L", "Closed"),
        PXStringListAttribute.Pair("E", "Suspended"),
        PXStringListAttribute.Pair("I", "Pending Approval"),
        PXStringListAttribute.Pair("U", "Pending Upgrade"),
        PXStringListAttribute.Pair("H", "On Hold"),
        PXStringListAttribute.Pair("R", "Rejected")
      })
    {
    }
  }

  public class ProjectStatusListAttribute : PXStringListAttribute
  {
    public ProjectStatusListAttribute()
      : base(new Tuple<string, string>[8]
      {
        PXStringListAttribute.Pair("D", "In Planning"),
        PXStringListAttribute.Pair("A", "Active"),
        PXStringListAttribute.Pair("F", "Completed"),
        PXStringListAttribute.Pair("L", "Closed"),
        PXStringListAttribute.Pair("C", "Canceled"),
        PXStringListAttribute.Pair("E", "Suspended"),
        PXStringListAttribute.Pair("I", "Pending Approval"),
        PXStringListAttribute.Pair("R", "Rejected")
      })
    {
    }
  }

  public class BillableProjectStatusListAttribute : PXStringListAttribute
  {
    public BillableProjectStatusListAttribute()
      : base(new Tuple<string, string>[8]
      {
        PXStringListAttribute.Pair("Z", "All"),
        PXStringListAttribute.Pair("D", "In Planning"),
        PXStringListAttribute.Pair("A", "Active"),
        PXStringListAttribute.Pair("F", "Completed"),
        PXStringListAttribute.Pair("C", "Canceled"),
        PXStringListAttribute.Pair("E", "Suspended"),
        PXStringListAttribute.Pair("I", "Pending Approval"),
        PXStringListAttribute.Pair("R", "Rejected")
      })
    {
    }
  }

  public class TemplStatusListAttribute : PXStringListAttribute
  {
    public TemplStatusListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("A", "Active"),
        PXStringListAttribute.Pair("H", "On Hold")
      })
    {
    }
  }

  public class planned : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProjectStatus.planned>
  {
    public planned()
      : base("D")
    {
    }
  }

  public class active : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProjectStatus.active>
  {
    public active()
      : base("A")
    {
    }
  }

  public class completed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProjectStatus.completed>
  {
    public completed()
      : base("F")
    {
    }
  }

  public class cancelled : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProjectStatus.cancelled>
  {
    public cancelled()
      : base("C")
    {
    }
  }

  public class suspended : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProjectStatus.suspended>
  {
    public suspended()
      : base("E")
    {
    }
  }

  public class pendingApproval : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProjectStatus.pendingApproval>
  {
    public pendingApproval()
      : base("I")
    {
    }
  }

  public class onHold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProjectStatus.onHold>
  {
    public onHold()
      : base("H")
    {
    }
  }

  public class rejected : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProjectStatus.rejected>
  {
    public rejected()
      : base("R")
    {
    }
  }

  public class closed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProjectStatus.closed>
  {
    public closed()
      : base("L")
    {
    }
  }
}
