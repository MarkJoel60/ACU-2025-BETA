// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POOrderStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PO;

public class POOrderStatus
{
  public const 
  #nullable disable
  string Initial = "_";
  public const string Hold = "H";
  public const string PendingApproval = "B";
  public const string Rejected = "V";
  public const string Open = "N";
  public const string AwaitingLink = "A";
  public const string PendingPrint = "D";
  public const string PendingEmail = "E";
  public const string Completed = "M";
  public const string Closed = "C";
  public const string Printed = "P";
  public const string Cancelled = "L";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[11]
      {
        PXStringListAttribute.Pair("H", "On Hold"),
        PXStringListAttribute.Pair("B", "Pending Approval"),
        PXStringListAttribute.Pair("V", "Rejected"),
        PXStringListAttribute.Pair("N", "Open"),
        PXStringListAttribute.Pair("A", "Awaiting Link"),
        PXStringListAttribute.Pair("D", "Pending Printing"),
        PXStringListAttribute.Pair("E", "Pending Email"),
        PXStringListAttribute.Pair("L", "Canceled"),
        PXStringListAttribute.Pair("M", "Completed"),
        PXStringListAttribute.Pair("C", "Closed"),
        PXStringListAttribute.Pair("P", "Printed")
      })
    {
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POOrderStatus.hold>
  {
    public hold()
      : base("H")
    {
    }
  }

  public class pendingApproval : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POOrderStatus.pendingApproval>
  {
    public pendingApproval()
      : base("B")
    {
    }
  }

  public class rejected : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POOrderStatus.rejected>
  {
    public rejected()
      : base("V")
    {
    }
  }

  public class open : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POOrderStatus.open>
  {
    public open()
      : base("N")
    {
    }
  }

  public class awaitingLink : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POOrderStatus.awaitingLink>
  {
    public awaitingLink()
      : base("A")
    {
    }
  }

  public class completed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POOrderStatus.completed>
  {
    public completed()
      : base("M")
    {
    }
  }

  public class closed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POOrderStatus.closed>
  {
    public closed()
      : base("C")
    {
    }
  }

  public class printed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POOrderStatus.printed>
  {
    public printed()
      : base("P")
    {
    }
  }

  public class pendingPrint : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POOrderStatus.pendingPrint>
  {
    public pendingPrint()
      : base("D")
    {
    }
  }

  public class pendingEmail : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POOrderStatus.pendingEmail>
  {
    public pendingEmail()
      : base("E")
    {
    }
  }

  public class cancelled : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POOrderStatus.cancelled>
  {
    public cancelled()
      : base("L")
    {
    }
  }
}
