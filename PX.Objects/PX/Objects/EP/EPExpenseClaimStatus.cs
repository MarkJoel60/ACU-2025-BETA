// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPExpenseClaimStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using PX.Objects.Common;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.EP;

/// <summary>
/// The current status of the expense claim, which is set by the system.
/// The fields that determine the status of a document are:
/// <see cref="P:PX.Objects.EP.EPExpenseClaim.Hold" />, <see cref="P:PX.Objects.EP.EPExpenseClaim.Released" />,
/// <see cref="P:PX.Objects.EP.EPExpenseClaim.Approved" />, <see cref="P:PX.Objects.EP.EPExpenseClaim.Rejected" />.
/// </summary>
/// <value>
/// The field can have one of the following values:
/// <c>"H"</c>: The claim is a draft and cannot be released.
/// <c>"O"</c>: The claim is in the approval process. If the claim is approved, the status changes to Approved.
/// <c>"A"</c>: The claim has been approved.
/// <c>"C"</c>: The claim has been rejected by the approver.
/// <c>"R"</c>: The claim has been released and then an Accounts Payable bill in the employee's name has been generated.
/// </value>
public class EPExpenseClaimStatus : ILabelProvider
{
  public const 
  #nullable disable
  string ApprovedStatus = "A";
  public const string HoldStatus = "H";
  public const string ReleasedStatus = "R";
  public const string OpenStatus = "O";
  public const string RejectedStatus = "C";
  private static readonly IEnumerable<ValueLabelPair> _valueLabelPairs = (IEnumerable<ValueLabelPair>) new ValueLabelList()
  {
    {
      "H",
      "On Hold"
    },
    {
      "O",
      "Pending Approval"
    },
    {
      "A",
      "Approved"
    },
    {
      "C",
      "Rejected"
    },
    {
      "R",
      "Released"
    }
  };

  public IEnumerable<ValueLabelPair> ValueLabelPairs => EPExpenseClaimStatus._valueLabelPairs;

  public class ListAttribute : LabelListAttribute
  {
    public ListAttribute()
      : base(EPExpenseClaimStatus._valueLabelPairs)
    {
    }
  }

  public sealed class holdStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPExpenseClaimStatus.holdStatus>
  {
    public holdStatus()
      : base("H")
    {
    }
  }

  public sealed class openStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPExpenseClaimStatus.openStatus>
  {
    public openStatus()
      : base("O")
    {
    }
  }

  public sealed class approvedStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPExpenseClaimStatus.approvedStatus>
  {
    public approvedStatus()
      : base("A")
    {
    }
  }

  public sealed class rejectedStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPExpenseClaimStatus.rejectedStatus>
  {
    public rejectedStatus()
      : base("C")
    {
    }
  }

  public sealed class releasedStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPExpenseClaimStatus.releasedStatus>
  {
    public releasedStatus()
      : base("R")
    {
    }
  }
}
