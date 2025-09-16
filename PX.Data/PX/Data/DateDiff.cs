// Decompiled with JetBrains decompiler
// Type: PX.Data.DateDiff
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data;

/// <summary>
/// Wraps string constants that can be used as the third argument in
/// the <see cref="T:PX.Data.DateDiff`3" /> BQL function.
/// </summary>
/// <seealso cref="T:PX.Data.DateDiff.day" />
/// <seealso cref="T:PX.Data.DateDiff.hour" />
/// <seealso cref="T:PX.Data.DateDiff.minute" />
/// <seealso cref="T:PX.Data.DateDiff.second" />
/// <seealso cref="T:PX.Data.DateDiff.millisecond" />
public sealed class DateDiff
{
  /// <exclude />
  public const 
  #nullable disable
  string Year = "yyyy";
  /// <exclude />
  public const string Quarter = "qq";
  /// <exclude />
  public const string Month = "mm";
  /// <exclude />
  public const string Week = "ww";
  /// <exclude />
  public const string Day = "dd";
  /// <exclude />
  public const string Hour = "hh";
  /// <exclude />
  public const string Minute = "mi";
  /// <exclude />
  public const string Second = "ss";
  /// <exclude />
  public const string Millisecond = "ms";

  /// <summary>
  /// Constant <tt>yyyy</tt>.
  /// </summary>
  public class year : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DateDiff.year>
  {
    public year()
      : base("yyyy")
    {
    }
  }

  /// <summary>
  /// Constant <tt>qq</tt>.
  /// </summary>
  public class quarter : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DateDiff.quarter>
  {
    public quarter()
      : base("qq")
    {
    }
  }

  /// <summary>
  /// Constant <tt>mm</tt>.
  /// </summary>
  public class month : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DateDiff.month>
  {
    public month()
      : base("mm")
    {
    }
  }

  /// <summary>
  /// Constant <tt>ww</tt>.
  /// </summary>
  public class week : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DateDiff.week>
  {
    public week()
      : base("ww")
    {
    }
  }

  /// <summary>
  /// Constant <tt>dd</tt>.
  /// </summary>
  public class day : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DateDiff.day>
  {
    public day()
      : base("dd")
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
  DateDiff.hour>
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
  DateDiff.minute>
  {
    public minute()
      : base("mi")
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
  DateDiff.second>
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
  DateDiff.millisecond>
  {
    public millisecond()
      : base("ms")
    {
    }
  }
}
