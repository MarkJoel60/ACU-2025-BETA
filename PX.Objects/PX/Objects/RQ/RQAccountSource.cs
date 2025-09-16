// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQAccountSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.RQ;

public static class RQAccountSource
{
  public const 
  #nullable disable
  string None = "N";
  public const string Department = "D";
  public const string Requester = "R";
  public const string PurchaseItem = "I";
  public const string RequestClass = "Q";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[5]
      {
        PXStringListAttribute.Pair("N", "None"),
        PXStringListAttribute.Pair("R", "Requester"),
        PXStringListAttribute.Pair("D", "Department"),
        PXStringListAttribute.Pair("Q", "Request Class"),
        PXStringListAttribute.Pair("I", "Purchase Item")
      })
    {
    }
  }

  public class department : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RQAccountSource.department>
  {
    public department()
      : base("D")
    {
    }
  }

  public class purchaseItem : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RQAccountSource.purchaseItem>
  {
    public purchaseItem()
      : base("I")
    {
    }
  }
}
