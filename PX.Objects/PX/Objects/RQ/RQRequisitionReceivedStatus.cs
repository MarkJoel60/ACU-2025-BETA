// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequisitionReceivedStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.RQ;

public static class RQRequisitionReceivedStatus
{
  public const 
  #nullable disable
  string Open = "O";
  public const string Partially = "P";
  public const string Closed = "C";
  public const string Ordered = "B";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("O", "Open"),
        PXStringListAttribute.Pair("P", "Partially Received"),
        PXStringListAttribute.Pair("C", "Received"),
        PXStringListAttribute.Pair("B", "Canceled")
      })
    {
    }
  }

  public class open : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RQRequisitionReceivedStatus.open>
  {
    public open()
      : base("O")
    {
    }
  }

  public class partially : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    RQRequisitionReceivedStatus.partially>
  {
    public partially()
      : base("P")
    {
    }
  }

  public class closed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RQRequisitionReceivedStatus.closed>
  {
    public closed()
      : base("C")
    {
    }
  }

  public class ordered : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RQRequisitionReceivedStatus.ordered>
  {
    public ordered()
      : base("B")
    {
    }
  }
}
