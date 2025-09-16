// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.PriceOption
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CT;

public static class PriceOption
{
  public const 
  #nullable disable
  string ItemPrice = "I";
  public const string ItemPercent = "P";
  public const string Manually = "M";
  public const string BasePercent = "B";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "I", "P", "M" }, new string[3]
      {
        "Use Item Price",
        "Percent of Item Price",
        "Enter Manually"
      })
    {
    }
  }

  public class itemPrice : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PriceOption.itemPrice>
  {
    public itemPrice()
      : base("I")
    {
    }
  }

  public class itemPercent : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PriceOption.itemPercent>
  {
    public itemPercent()
      : base("P")
    {
    }
  }

  public class manually : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PriceOption.manually>
  {
    public manually()
      : base("M")
    {
    }
  }

  public class basePercent : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PriceOption.basePercent>
  {
    public basePercent()
      : base("B")
    {
    }
  }
}
