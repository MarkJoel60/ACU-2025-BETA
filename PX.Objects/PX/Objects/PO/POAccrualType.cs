// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POAccrualType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PO;

public static class POAccrualType
{
  public const 
  #nullable disable
  string Receipt = "R";
  public const string Order = "O";

  public class receipt : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POAccrualType.receipt>
  {
    public receipt()
      : base("R")
    {
    }
  }

  public class order : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POAccrualType.order>
  {
    public order()
      : base("O")
    {
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("R", "Receipt"),
        PXStringListAttribute.Pair("O", "Order")
      })
    {
    }
  }
}
