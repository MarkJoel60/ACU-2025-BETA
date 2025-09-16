// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.CompletePOLineTypes
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class CompletePOLineTypes
{
  public const 
  #nullable disable
  string Amount = "A";
  public const string Quantity = "Q";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("A", "By Amount"),
        PXStringListAttribute.Pair("Q", "By Quantity")
      })
    {
    }
  }

  public class amount : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CompletePOLineTypes.amount>
  {
    public amount()
      : base("A")
    {
    }
  }

  public class quantity : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CompletePOLineTypes.quantity>
  {
    public quantity()
      : base("Q")
    {
    }
  }
}
