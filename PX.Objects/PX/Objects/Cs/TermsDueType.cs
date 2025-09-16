// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.TermsDueType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CS;

public class TermsDueType
{
  public const 
  #nullable disable
  string FixedNumberOfDays = "N";
  public const string DayOfNextMonth = "D";
  public const string EndOfMonth = "E";
  public const string EndOfNextMonth = "M";
  public const string DayOfTheMonth = "T";
  public const string Prox = "P";
  public const string Custom = "C";
  public const string FixedNumberOfDaysPlusDayOfNextMonth = "F";
  public const string ByDecadesInNextMonth = "B";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[9]
      {
        "N",
        "D",
        "E",
        "M",
        "T",
        "P",
        "F",
        "B",
        "C"
      }, new string[9]
      {
        "Fixed Number of Days",
        "Day of Next Month",
        "End of Month",
        "End of Next Month",
        "Day of the Month",
        "Fixed Number of Days starting Next Month",
        "Fixed Number of Days Plus Day of Next Month",
        "10th, 20th, or Last Day of Next Month",
        "Custom"
      })
    {
    }
  }

  public class fixedNumberOfDays : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    TermsDueType.fixedNumberOfDays>
  {
    public fixedNumberOfDays()
      : base("N")
    {
    }
  }

  public class dayOfNextMonth : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TermsDueType.dayOfNextMonth>
  {
    public dayOfNextMonth()
      : base("D")
    {
    }
  }

  public class endOfMonth : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TermsDueType.endOfMonth>
  {
    public endOfMonth()
      : base("E")
    {
    }
  }

  public class endOfNextMonth : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TermsDueType.endOfNextMonth>
  {
    public endOfNextMonth()
      : base("M")
    {
    }
  }

  public class dayOfTheMonth : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TermsDueType.dayOfTheMonth>
  {
    public dayOfTheMonth()
      : base("T")
    {
    }
  }

  public class prox : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TermsDueType.prox>
  {
    public prox()
      : base("P")
    {
    }
  }

  public class custom : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TermsDueType.custom>
  {
    public custom()
      : base("C")
    {
    }
  }
}
