// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.PeriodDateSelOption
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.GL;

public static class PeriodDateSelOption
{
  public const 
  #nullable disable
  string PeriodStart = "S";
  public const string PeriodEnd = "E";
  public const string PeriodFixedDate = "D";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "S", "E", "D" }, new string[3]
      {
        "Start Day of Period",
        "End Day of Period",
        "Fixed Day of Period"
      })
    {
    }
  }

  public class periodStart : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PeriodDateSelOption.periodStart>
  {
    public periodStart()
      : base("S")
    {
    }
  }

  public class periodEnd : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PeriodDateSelOption.periodEnd>
  {
    public periodEnd()
      : base("E")
    {
    }
  }

  public class periodFixedDate : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PeriodDateSelOption.periodFixedDate>
  {
    public periodFixedDate()
      : base("D")
    {
    }
  }
}
