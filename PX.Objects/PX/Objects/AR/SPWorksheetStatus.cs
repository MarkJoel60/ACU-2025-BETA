// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.SPWorksheetStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AR;

public class SPWorksheetStatus
{
  public const 
  #nullable disable
  string Hold = "H";
  public const string Open = "N";
  public const string PendingApproval = "P";
  public const string Released = "R";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "H", "N", "P", "R" }, new string[4]
      {
        "On Hold",
        "Open",
        "Pending Approval",
        "Released"
      })
    {
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SPWorksheetStatus.hold>
  {
    public hold()
      : base("H")
    {
    }
  }

  public class open : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SPWorksheetStatus.open>
  {
    public open()
      : base("N")
    {
    }
  }

  public class pendingApproval : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SPWorksheetStatus.pendingApproval>
  {
    public pendingApproval()
      : base("P")
    {
    }
  }

  public class released : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SPWorksheetStatus.released>
  {
    public released()
      : base("R")
    {
    }
  }
}
