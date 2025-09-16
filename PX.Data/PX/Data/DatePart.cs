// Decompiled with JetBrains decompiler
// Type: PX.Data.DatePart
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data;

/// <exclude />
public sealed class DatePart
{
  /// <exclude />
  public const 
  #nullable disable
  string Day = "dd";
  /// <exclude />
  public const string WeekDay = "dw";
  /// <exclude />
  public const string DayOfYear = "dy";
  /// <exclude />
  public const string Hour = "hh";
  /// <exclude />
  public const string Minute = "mi";
  /// <exclude />
  public const string Week = "ww";
  public const string Month = "mm";
  public const string Year = "yyyy";
  /// <exclude />
  public const string Quarter = "qq";
  /// <exclude />
  public const string Second = "ss";
  /// <exclude />
  public const string Millisecond = "ms";

  /// <summary>
  /// Constant <tt>dd</tt>.
  /// </summary>
  public class day : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DatePart.day>
  {
    public day()
      : base("dd")
    {
    }
  }

  /// <summary>
  /// Constant <tt>dw</tt>.
  /// </summary>
  public class weekDay : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DatePart.weekDay>
  {
    public weekDay()
      : base("dw")
    {
    }
  }

  /// <summary>
  /// Constant <tt>dy</tt>.
  /// </summary>
  public class dayOfYear : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DatePart.dayOfYear>
  {
    public dayOfYear()
      : base("dy")
    {
    }
  }

  /// <summary>
  /// Constant <tt>hh</tt>.
  /// </summary>
  public class hour : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DatePart.hour>
  {
    public hour()
      : base("hh")
    {
    }
  }

  /// <summary>
  /// Constant <tt>mi</tt>.
  /// </summary>
  public class minute : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DatePart.minute>
  {
    public minute()
      : base("mi")
    {
    }
  }

  /// <exclude />
  public class week : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DatePart.week>
  {
    public week()
      : base("ww")
    {
    }
  }

  /// <exclude />
  public class month : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DatePart.month>
  {
    public month()
      : base("mm")
    {
    }
  }

  /// <exclude />
  public class quarter : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DatePart.quarter>
  {
    public quarter()
      : base("qq")
    {
    }
  }

  public class year : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DatePart.year>
  {
    public year()
      : base("yyyy")
    {
    }
  }

  /// <summary>
  /// Constant <tt>ss</tt>.
  /// </summary>
  public class second : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DatePart.second>
  {
    public second()
      : base("ss")
    {
    }
  }

  /// <summary>
  /// Constant <tt>ms</tt>.
  /// </summary>
  public class millisecond : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DatePart.millisecond>
  {
    public millisecond()
      : base("ms")
    {
    }
  }
}
