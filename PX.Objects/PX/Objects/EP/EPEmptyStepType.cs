// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPEmptyStepType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.EP;

public static class EPEmptyStepType
{
  public const 
  #nullable disable
  string Reject = "R";
  public const string Approve = "A";
  public const string Next = "N";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "R", "A", "N" }, new string[3]
      {
        "Reject Document",
        "Approve Document",
        "Go to Next Step"
      })
    {
    }
  }

  public class reject : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPEmptyStepType.reject>
  {
    public reject()
      : base("R")
    {
    }
  }

  public class approve : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPEmptyStepType.approve>
  {
    public approve()
      : base("A")
    {
    }
  }

  public class next : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  EPEmptyStepType.next>
  {
    public next()
      : base("N")
    {
    }
  }
}
