// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.RetentionTypeList
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AR;

public static class RetentionTypeList
{
  public const 
  #nullable disable
  string LastPrice = "L";
  public const string FixedNumOfMonths = "F";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "L", "F" }, new string[2]
      {
        "Last Price",
        "Fixed Number of Months"
      })
    {
    }
  }

  public class lastPrice : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  RetentionTypeList.lastPrice>
  {
    public lastPrice()
      : base("L")
    {
    }
  }

  public class fixedNumOfMonths : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    RetentionTypeList.fixedNumOfMonths>
  {
    public fixedNumOfMonths()
      : base("F")
    {
    }
  }
}
