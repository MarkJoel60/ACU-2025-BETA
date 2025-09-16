// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.PriceTypeList
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AR;

public static class PriceTypeList
{
  public const 
  #nullable disable
  string CustomerPriceClass = "P";
  public const string Customer = "C";
  public const string Vendor = "V";
  public const string BasePrice = "B";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "B", "P", "C" }, new string[3]
      {
        "Base",
        "Customer Price Class",
        "Customer"
      })
    {
    }
  }

  public class customerPriceClass : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PriceTypeList.customerPriceClass>
  {
    public customerPriceClass()
      : base("P")
    {
    }
  }

  public class customer : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PriceTypeList.customer>
  {
    public customer()
      : base("C")
    {
    }
  }

  public class vendor : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PriceTypeList.vendor>
  {
    public vendor()
      : base("V")
    {
    }
  }

  public class basePrice : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PriceTypeList.basePrice>
  {
    public basePrice()
      : base("B")
    {
    }
  }
}
