// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.SPCommnPeriodTypes
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AR;

public class SPCommnPeriodTypes
{
  public const 
  #nullable disable
  string Monthly = "M";
  public const string Quarterly = "Q";
  public const string Yearly = "Y";
  public const string FiscalPeriod = "F";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "M", "Q", "Y", "F" }, new string[4]
      {
        "Monthly",
        "Quarterly",
        "Yearly",
        "By Financial Period"
      })
    {
    }
  }

  public class monthly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SPCommnPeriodTypes.monthly>
  {
    public monthly()
      : base("M")
    {
    }
  }

  public class quarterly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SPCommnPeriodTypes.quarterly>
  {
    public quarterly()
      : base("Q")
    {
    }
  }

  public class yearly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SPCommnPeriodTypes.yearly>
  {
    public yearly()
      : base("Y")
    {
    }
  }

  public class fiscalPeriod : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SPCommnPeriodTypes.fiscalPeriod>
  {
    public fiscalPeriod()
      : base("F")
    {
    }
  }
}
