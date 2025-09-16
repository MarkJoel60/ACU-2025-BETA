// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPExpenseClaimDetailsStatus
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
/// The current status of the expense receipt, which is set by the system.
/// The fields that determine the status of a document are:
/// <see cref="P:PX.Objects.EP.EPExpenseClaimDetails.Hold" />, <see cref="P:PX.Objects.EP.EPExpenseClaimDetails.Released" />,
/// <see cref="P:PX.Objects.EP.EPExpenseClaimDetails.Approved" />, <see cref="P:PX.Objects.EP.EPExpenseClaimDetails.Rejected" />.
/// </summary>
/// <value>
/// The field can have one of the following values:
/// <c>"H"</c>: The receipt is new and has not been submitted for approval yet, or the receipt has been rejected and then put on hold while a user is adjusting it.
/// <c>"A"</c>: The receipt is ready to be added to a claim after it has been approved (if approval is required for the receipt)
/// or after it has been submitted for further processing (if approval is not required).
/// <c>"O"</c>: The receipt is pending approval.
/// <c>"C"</c>: The receipt has been rejected.
/// <c>"R"</c>: The expense claim associated with the receipt has been released.
/// </value>
public class EPExpenseClaimDetailsStatus : ILabelProvider
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
      "A",
      "Open"
    },
    {
      "O",
      "Pending Approval"
    },
    {
      "R",
      "Released"
    },
    {
      "C",
      "Rejected"
    }
  };

  public IEnumerable<ValueLabelPair> ValueLabelPairs
  {
    get => EPExpenseClaimDetailsStatus._valueLabelPairs;
  }

  public class ListAttribute : LabelListAttribute
  {
    public ListAttribute()
      : base(EPExpenseClaimDetailsStatus._valueLabelPairs)
    {
    }
  }

  public class holdStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPExpenseClaimDetailsStatus.holdStatus>
  {
    public holdStatus()
      : base("H")
    {
    }
  }

  public class approvedStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPExpenseClaimDetailsStatus.approvedStatus>
  {
    public approvedStatus()
      : base("A")
    {
    }
  }

  public class openStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPExpenseClaimDetailsStatus.openStatus>
  {
    public openStatus()
      : base("O")
    {
    }
  }

  public class releasedStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPExpenseClaimDetailsStatus.releasedStatus>
  {
    public releasedStatus()
      : base("R")
    {
    }
  }

  public class rejectedStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPExpenseClaimDetailsStatus.rejectedStatus>
  {
    public rejectedStatus()
      : base("C")
    {
    }
  }
}
