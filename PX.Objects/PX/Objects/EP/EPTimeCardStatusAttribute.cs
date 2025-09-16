// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPTimeCardStatusAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.EP;

public class EPTimeCardStatusAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string ApprovedStatus = "A";
  public const string HoldStatus = "H";
  public const string ReleasedStatus = "R";
  public const string OpenStatus = "O";
  public const string RejectedStatus = "C";

  public EPTimeCardStatusAttribute()
    : base(new string[5]{ "H", "O", "A", "C", "R" }, new string[5]
    {
      "On Hold",
      "Pending Approval",
      "Approved",
      "Rejected",
      "Released"
    })
  {
  }

  public class approvedStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPTimeCardStatusAttribute.approvedStatus>
  {
    public approvedStatus()
      : base("A")
    {
    }
  }

  public class holdStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPTimeCardStatusAttribute.holdStatus>
  {
    public holdStatus()
      : base("H")
    {
    }
  }

  public class releasedStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPTimeCardStatusAttribute.releasedStatus>
  {
    public releasedStatus()
      : base("R")
    {
    }
  }

  public class openStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPTimeCardStatusAttribute.openStatus>
  {
    public openStatus()
      : base("O")
    {
    }
  }

  public class rejectedStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    EPTimeCardStatusAttribute.rejectedStatus>
  {
    public rejectedStatus()
      : base("C")
    {
    }
  }
}
