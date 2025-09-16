// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ApprovalStatusListAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR;

public class ApprovalStatusListAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string NotRequired = "NR";
  public const string Approved = "AP";
  public const string Rejected = "RJ";
  public const string PartiallyApprove = "PR";
  public const string PendingApproval = "PA";

  public ApprovalStatusListAttribute()
    : base(new string[5]{ "NR", "AP", "RJ", "PR", "PA" }, new string[5]
    {
      "Not Required",
      nameof (Approved),
      nameof (Rejected),
      "Partially",
      "Pending Approval"
    })
  {
  }

  public class notRequired : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ApprovalStatusListAttribute.notRequired>
  {
    public notRequired()
      : base("NR")
    {
    }
  }

  public class approved : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ApprovalStatusListAttribute.approved>
  {
    public approved()
      : base("AP")
    {
    }
  }

  public class rejected : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ApprovalStatusListAttribute.rejected>
  {
    public rejected()
      : base("RJ")
    {
    }
  }

  public class partiallyApprove : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ApprovalStatusListAttribute.partiallyApprove>
  {
    public partiallyApprove()
      : base("PR")
    {
    }
  }

  public class pendingApproval : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ApprovalStatusListAttribute.pendingApproval>
  {
    public pendingApproval()
      : base("PA")
    {
    }
  }
}
