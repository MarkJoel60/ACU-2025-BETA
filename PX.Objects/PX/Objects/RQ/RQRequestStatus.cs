// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequestStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.RQ;

public class RQRequestStatus
{
  public const 
  #nullable disable
  string Hold = "H";
  public const string PendingApproval = "P";
  public const string Rejected = "R";
  public const string Open = "N";
  public const string Closed = "C";
  public const string Issued = "I";
  public const string Canceled = "L";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[7]
      {
        PXStringListAttribute.Pair("H", "On Hold"),
        PXStringListAttribute.Pair("N", "Open"),
        PXStringListAttribute.Pair("P", "Pending Approval"),
        PXStringListAttribute.Pair("L", "Canceled"),
        PXStringListAttribute.Pair("C", "Closed"),
        PXStringListAttribute.Pair("I", "Issued"),
        PXStringListAttribute.Pair("R", "Rejected")
      })
    {
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RQRequestStatus.hold>
  {
    public hold()
      : base("H")
    {
    }
  }

  public class pendingApproval : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    RQRequestStatus.pendingApproval>
  {
    public pendingApproval()
      : base("P")
    {
    }
  }

  public class rejected : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RQRequestStatus.rejected>
  {
    public rejected()
      : base("R")
    {
    }
  }

  public class open : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RQRequestStatus.open>
  {
    public open()
      : base("N")
    {
    }
  }

  public class closed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RQRequestStatus.closed>
  {
    public closed()
      : base("C")
    {
    }
  }

  public class canceled : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RQRequestStatus.canceled>
  {
    public canceled()
      : base("L")
    {
    }
  }

  public class issued : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RQRequestStatus.issued>
  {
    public issued()
      : base("I")
    {
    }
  }
}
