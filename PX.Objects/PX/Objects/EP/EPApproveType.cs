// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPApproveType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.EP;

public static class EPApproveType
{
  public const 
  #nullable disable
  string Wait = "W";
  public const string Complete = "C";
  public const string Approve = "A";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "W", "C", "A" }, new string[3]
      {
        "Collect All Approvals from This Step",
        "Complete Step",
        "Approve Document"
      })
    {
    }
  }

  public class wait : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPApproveType.wait>
  {
    public wait()
      : base("W")
    {
    }
  }

  public class complete : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPApproveType.complete>
  {
    public complete()
      : base("C")
    {
    }
  }

  public class approve : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPApproveType.approve>
  {
    public approve()
      : base("A")
    {
    }
  }
}
