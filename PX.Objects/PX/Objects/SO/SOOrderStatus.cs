// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.SO;

public class SOOrderStatus
{
  public const 
  #nullable disable
  string Initial = "_";
  public const string Open = "N";
  public const string Hold = "H";
  public const string PendingApproval = "P";
  public const string Voided = "V";
  public const string PendingProcessing = "E";
  public const string AwaitingPayment = "A";
  public const string CreditHold = "R";
  public const string Completed = "C";
  public const string Cancelled = "L";
  public const string BackOrder = "B";
  public const string Shipping = "S";
  public const string Invoiced = "I";
  public const string Expired = "D";

  public class ListAttribute : PXStringListAttribute
  {
    public static readonly (string, string)[] ValuesToLabels = new (string, string)[13]
    {
      ("N", "Open"),
      ("H", "On Hold"),
      ("P", "Pending Approval"),
      ("V", "Rejected"),
      ("E", "Pending Processing"),
      ("A", "Awaiting Payment"),
      ("R", "Credit Hold"),
      ("C", "Completed"),
      ("L", "Canceled"),
      ("B", "Back Order"),
      ("S", "Shipping"),
      ("I", "Invoiced"),
      ("D", "Expired")
    };

    public ListAttribute()
      : base(SOOrderStatus.ListAttribute.ValuesToLabels)
    {
    }
  }

  public class ListWithoutOrdersAttribute : PXStringListAttribute
  {
    public ListWithoutOrdersAttribute()
      : base(new Tuple<string, string>[12]
      {
        PXStringListAttribute.Pair("N", "Open"),
        PXStringListAttribute.Pair("H", "On Hold"),
        PXStringListAttribute.Pair("P", "Pending Approval"),
        PXStringListAttribute.Pair("V", "Rejected"),
        PXStringListAttribute.Pair("E", "Pending Processing"),
        PXStringListAttribute.Pair("A", "Awaiting Payment"),
        PXStringListAttribute.Pair("R", "Credit Hold"),
        PXStringListAttribute.Pair("C", "Completed"),
        PXStringListAttribute.Pair("L", "Canceled"),
        PXStringListAttribute.Pair("B", "Back Order"),
        PXStringListAttribute.Pair("S", "Shipping"),
        PXStringListAttribute.Pair("I", "Invoiced")
      })
    {
    }
  }

  public class voided : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOOrderStatus.voided>
  {
    public voided()
      : base("V")
    {
    }
  }

  public class pendingApproval : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOOrderStatus.pendingApproval>
  {
    public pendingApproval()
      : base("P")
    {
    }
  }

  public class open : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOOrderStatus.open>
  {
    public open()
      : base("N")
    {
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOOrderStatus.hold>
  {
    public hold()
      : base("H")
    {
    }
  }

  public class pendingProcessing : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    SOOrderStatus.pendingProcessing>
  {
    public pendingProcessing()
      : base("E")
    {
    }
  }

  public class awaitingPayment : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOOrderStatus.awaitingPayment>
  {
    public awaitingPayment()
      : base("A")
    {
    }
  }

  public class creditHold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOOrderStatus.creditHold>
  {
    public creditHold()
      : base("R")
    {
    }
  }

  public class completed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOOrderStatus.completed>
  {
    public completed()
      : base("C")
    {
    }
  }

  public class cancelled : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOOrderStatus.cancelled>
  {
    public cancelled()
      : base("L")
    {
    }
  }

  public class backOrder : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOOrderStatus.backOrder>
  {
    public backOrder()
      : base("B")
    {
    }
  }

  public class shipping : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOOrderStatus.shipping>
  {
    public shipping()
      : base("S")
    {
    }
  }

  public class invoiced : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOOrderStatus.invoiced>
  {
    public invoiced()
      : base("I")
    {
    }
  }

  public class expired : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOOrderStatus.expired>
  {
    public expired()
      : base("D")
    {
    }
  }
}
