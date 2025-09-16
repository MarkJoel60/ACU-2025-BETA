// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DemandPeriodType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class DemandPeriodType
{
  public const 
  #nullable disable
  string Month = "MT";
  public const string Week = "WK";
  public const string Quarter = "QT";
  public const string Day = "DY";

  public class month : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DemandPeriodType.month>
  {
    public month()
      : base("MT")
    {
    }
  }

  public class quarter : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DemandPeriodType.quarter>
  {
    public quarter()
      : base("QT")
    {
    }
  }

  public class week : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DemandPeriodType.week>
  {
    public week()
      : base("WK")
    {
    }
  }

  public class day : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DemandPeriodType.day>
  {
    public day()
      : base("DY")
    {
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("QT", "Quarter"),
        PXStringListAttribute.Pair("MT", "Month"),
        PXStringListAttribute.Pair("WK", "Week"),
        PXStringListAttribute.Pair("DY", "Day")
      })
    {
    }
  }
}
