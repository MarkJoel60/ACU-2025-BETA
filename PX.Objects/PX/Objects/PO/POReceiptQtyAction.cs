// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptQtyAction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PO;

public class POReceiptQtyAction
{
  public const 
  #nullable disable
  string Accept = "A";
  public const string AcceptButWarn = "W";
  public const string Reject = "R";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("R", "Reject"),
        PXStringListAttribute.Pair("W", "Accept but Warn"),
        PXStringListAttribute.Pair("A", "Accept")
      })
    {
    }
  }

  public sealed class accept : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POReceiptQtyAction.accept>
  {
    public accept()
      : base("A")
    {
    }
  }

  public sealed class acceptButWarn : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    POReceiptQtyAction.acceptButWarn>
  {
    public acceptButWarn()
      : base("W")
    {
    }
  }

  public sealed class reject : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POReceiptQtyAction.reject>
  {
    public reject()
      : base("R")
    {
    }
  }
}
