// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPApprovalStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.EP;

public class EPApprovalStatus
{
  public const 
  #nullable disable
  string Pending = "P";
  public const string Approved = "A";
  public const string Rejected = "R";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "P", "A", "R" }, new string[3]
      {
        "Pending",
        "Approved",
        "Rejected"
      })
    {
    }
  }

  public class pending : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPApprovalStatus.pending>
  {
    public pending()
      : base("P")
    {
    }
  }

  public class approved : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPApprovalStatus.approved>
  {
    public approved()
      : base("A")
    {
    }
  }

  public class rejected : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPApprovalStatus.rejected>
  {
    public rejected()
      : base("R")
    {
    }
  }
}
