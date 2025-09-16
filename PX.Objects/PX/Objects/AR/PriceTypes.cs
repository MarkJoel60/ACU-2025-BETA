// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.PriceTypes
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR;

public static class PriceTypes
{
  public const 
  #nullable disable
  string Customer = "C";
  public const string CustomerPriceClass = "P";
  public const string BasePrice = "B";
  public const string AllPrices = "A";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("B", "Base"),
        PXStringListAttribute.Pair("C", "Customer"),
        PXStringListAttribute.Pair("P", "Customer Price Class")
      })
    {
      this.SortByValues = true;
    }
  }

  public class ListWithAllAttribute : PXStringListAttribute
  {
    public ListWithAllAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("A", "All Prices"),
        PXStringListAttribute.Pair("B", "Base"),
        PXStringListAttribute.Pair("C", "Customer"),
        PXStringListAttribute.Pair("P", "Customer Price Class")
      })
    {
      this.SortByValues = true;
    }
  }

  public class customer : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PriceTypes.customer>
  {
    public customer()
      : base("C")
    {
    }
  }

  public class customerPriceClass : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PriceTypes.customerPriceClass>
  {
    public customerPriceClass()
      : base("P")
    {
    }
  }

  public class basePrice : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PriceTypes.basePrice>
  {
    public basePrice()
      : base("B")
    {
    }
  }

  public class allPrices : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PriceTypes.allPrices>
  {
    public allPrices()
      : base("A")
    {
    }
  }
}
