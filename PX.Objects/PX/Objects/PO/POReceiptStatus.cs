// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PO;

public class POReceiptStatus
{
  public const 
  #nullable disable
  string Initial = "_";
  public const string Hold = "H";
  public const string Balanced = "B";
  public const string Released = "R";
  public const string Received = "C";
  public const string UnderCorrection = "U";
  public const string Canceled = "D";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[6]
      {
        PXStringListAttribute.Pair("H", "On Hold"),
        PXStringListAttribute.Pair("B", "Balanced"),
        PXStringListAttribute.Pair("R", "Released"),
        PXStringListAttribute.Pair("C", "Received"),
        PXStringListAttribute.Pair("U", "Under Correction"),
        PXStringListAttribute.Pair("D", "Canceled")
      })
    {
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POReceiptStatus.hold>
  {
    public hold()
      : base("H")
    {
    }
  }

  public class balanced : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POReceiptStatus.balanced>
  {
    public balanced()
      : base("B")
    {
    }
  }

  public class released : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POReceiptStatus.released>
  {
    public released()
      : base("R")
    {
    }
  }

  public class received : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POReceiptStatus.received>
  {
    public received()
      : base("C")
    {
    }
  }

  public class underCorrection : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    POReceiptStatus.underCorrection>
  {
    public underCorrection()
      : base("U")
    {
    }
  }

  public class canceled : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POReceiptStatus.canceled>
  {
    public canceled()
      : base("D")
    {
    }
  }
}
