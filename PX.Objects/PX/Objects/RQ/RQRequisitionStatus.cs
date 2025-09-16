// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequisitionStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.RQ;

public class RQRequisitionStatus
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
  public const string Bidding = "B";
  public const string Released = "E";
  public const string PendingQuotation = "Q";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[9]
      {
        PXStringListAttribute.Pair("H", "On Hold"),
        PXStringListAttribute.Pair("N", "Open"),
        PXStringListAttribute.Pair("P", "Pending Approval"),
        PXStringListAttribute.Pair("L", "Canceled"),
        PXStringListAttribute.Pair("C", "Closed"),
        PXStringListAttribute.Pair("B", "Pending Bidding"),
        PXStringListAttribute.Pair("Q", "Pending Quote"),
        PXStringListAttribute.Pair("R", "Rejected"),
        PXStringListAttribute.Pair("E", "Released")
      })
    {
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RQRequisitionStatus.hold>
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
    RQRequisitionStatus.pendingApproval>
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
  RQRequisitionStatus.rejected>
  {
    public rejected()
      : base("R")
    {
    }
  }

  public class released : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RQRequisitionStatus.released>
  {
    public released()
      : base("E")
    {
    }
  }

  public class open : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RQRequisitionStatus.open>
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
  RQRequisitionStatus.closed>
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
  RQRequisitionStatus.canceled>
  {
    public canceled()
      : base("L")
    {
    }
  }

  public class bidding : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RQRequisitionStatus.bidding>
  {
    public bidding()
      : base("B")
    {
    }
  }

  public class pendingQuotation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    RQRequisitionStatus.pendingQuotation>
  {
    public pendingQuotation()
      : base("Q")
    {
    }
  }
}
