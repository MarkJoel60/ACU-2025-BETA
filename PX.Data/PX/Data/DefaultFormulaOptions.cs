// Decompiled with JetBrains decompiler
// Type: PX.Data.DefaultFormulaOptions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

public static class DefaultFormulaOptions
{
  private static FormulaOption Make(string value, string category, params string[] subCategories)
  {
    return new FormulaOption()
    {
      Value = value,
      Category = EnumerableExtensions.IsNullOrEmpty<string>((IEnumerable<string>) subCategories) ? category : $"{category}/{string.Join("/", subCategories)}"
    };
  }

  public static class Functions
  {
    private static FormulaOption Make(string value, params string[] subCategories)
    {
      return DefaultFormulaOptions.Make(value, nameof (Functions), subCategories);
    }

    public static IEnumerable<FormulaOption> GetAll()
    {
      return ((IEnumerable<FormulaOption>) Array.Empty<FormulaOption>()).Concat<FormulaOption>(DefaultFormulaOptions.Functions.Conversion.GetAll()).Concat<FormulaOption>(DefaultFormulaOptions.Functions.Aggregate.GetAll()).Concat<FormulaOption>(DefaultFormulaOptions.Functions.Text.GetAll()).Concat<FormulaOption>(DefaultFormulaOptions.Functions.Math.GetAll()).Concat<FormulaOption>(DefaultFormulaOptions.Functions.DateTime.GetAll()).Concat<FormulaOption>(DefaultFormulaOptions.Functions.Other.GetAll());
    }

    public static class Aggregate
    {
      private static FormulaOption Make(string value)
      {
        return DefaultFormulaOptions.Functions.Make(value, nameof (Aggregate));
      }

      public static IEnumerable<FormulaOption> GetAll()
      {
        return (IEnumerable<FormulaOption>) new \u003C\u003Ez__ReadOnlyArray<FormulaOption>(new FormulaOption[6]
        {
          DefaultFormulaOptions.Functions.Aggregate.Avg,
          DefaultFormulaOptions.Functions.Aggregate.Count,
          DefaultFormulaOptions.Functions.Aggregate.Max,
          DefaultFormulaOptions.Functions.Aggregate.Min,
          DefaultFormulaOptions.Functions.Aggregate.Sum,
          DefaultFormulaOptions.Functions.Aggregate.StringAgg
        });
      }

      public static FormulaOption Avg
      {
        get => DefaultFormulaOptions.Functions.Aggregate.Make("Avg( expr )");
      }

      public static FormulaOption Count
      {
        get => DefaultFormulaOptions.Functions.Aggregate.Make("Count( expr )");
      }

      public static FormulaOption Max
      {
        get => DefaultFormulaOptions.Functions.Aggregate.Make("Max( expr )");
      }

      public static FormulaOption Min
      {
        get => DefaultFormulaOptions.Functions.Aggregate.Make("Min( expr )");
      }

      public static FormulaOption Sum
      {
        get => DefaultFormulaOptions.Functions.Aggregate.Make("Sum( expr )");
      }

      public static FormulaOption StringAgg
      {
        get => DefaultFormulaOptions.Functions.Aggregate.Make("StringAgg( expr, delimiter )");
      }
    }

    public static class Conversion
    {
      private static FormulaOption Make(string value)
      {
        return DefaultFormulaOptions.Functions.Make(value, nameof (Conversion));
      }

      public static IEnumerable<FormulaOption> GetAll()
      {
        return (IEnumerable<FormulaOption>) new FormulaOption[9]
        {
          DefaultFormulaOptions.Functions.Conversion.ToBoolean,
          DefaultFormulaOptions.Functions.Conversion.ToDate,
          DefaultFormulaOptions.Functions.Conversion.ToDouble,
          DefaultFormulaOptions.Functions.Conversion.ToDecimal,
          DefaultFormulaOptions.Functions.Conversion.ToInteger,
          DefaultFormulaOptions.Functions.Conversion.ToLongInteger,
          DefaultFormulaOptions.Functions.Conversion.ToSign,
          DefaultFormulaOptions.Functions.Conversion.ToString,
          DefaultFormulaOptions.Functions.Conversion.ToShort
        };
      }

      public static FormulaOption ToBoolean
      {
        get => DefaultFormulaOptions.Functions.Conversion.Make("CBool( x )");
      }

      public static FormulaOption ToDate
      {
        get => DefaultFormulaOptions.Functions.Conversion.Make("CDate( x )");
      }

      public static FormulaOption ToDouble
      {
        get => DefaultFormulaOptions.Functions.Conversion.Make("CDbl( x )");
      }

      public static FormulaOption ToDecimal
      {
        get => DefaultFormulaOptions.Functions.Conversion.Make("CDec( x )");
      }

      public static FormulaOption ToInteger
      {
        get => DefaultFormulaOptions.Functions.Conversion.Make("CInt( x )");
      }

      public static FormulaOption ToLongInteger
      {
        get => DefaultFormulaOptions.Functions.Conversion.Make("CLong( x )");
      }

      public static FormulaOption ToSign
      {
        get => DefaultFormulaOptions.Functions.Conversion.Make("CSng( x )");
      }

      public static FormulaOption ToString
      {
        get => DefaultFormulaOptions.Functions.Conversion.Make("CStr( x )");
      }

      public static FormulaOption ToShort
      {
        get => DefaultFormulaOptions.Functions.Conversion.Make("CShort( x )");
      }
    }

    public static class DateTime
    {
      private static FormulaOption Make(string value)
      {
        return DefaultFormulaOptions.Functions.Make(value, nameof (DateTime));
      }

      public static IEnumerable<FormulaOption> GetAll()
      {
        return (IEnumerable<FormulaOption>) new FormulaOption[16 /*0x10*/]
        {
          DefaultFormulaOptions.Functions.DateTime.DateAdd,
          DefaultFormulaOptions.Functions.DateTime.DateDiff,
          DefaultFormulaOptions.Functions.DateTime.Year,
          DefaultFormulaOptions.Functions.DateTime.Month,
          DefaultFormulaOptions.Functions.DateTime.MonthName,
          DefaultFormulaOptions.Functions.DateTime.Day,
          DefaultFormulaOptions.Functions.DateTime.DayOfWeek,
          DefaultFormulaOptions.Functions.DateTime.DayOfYear,
          DefaultFormulaOptions.Functions.DateTime.DayOrdinal,
          DefaultFormulaOptions.Functions.DateTime.Hour,
          DefaultFormulaOptions.Functions.DateTime.Minute,
          DefaultFormulaOptions.Functions.DateTime.Second,
          DefaultFormulaOptions.Functions.DateTime.Now,
          DefaultFormulaOptions.Functions.DateTime.NowUTC,
          DefaultFormulaOptions.Functions.DateTime.Today,
          DefaultFormulaOptions.Functions.DateTime.TodayUTC
        };
      }

      public static FormulaOption DateAdd
      {
        get => DefaultFormulaOptions.Functions.DateTime.Make("DateAdd( date, interval, number )");
      }

      public static FormulaOption DateDiff
      {
        get => DefaultFormulaOptions.Functions.DateTime.Make("DateDiff( interval, date1, date2 )");
      }

      public static FormulaOption Year
      {
        get => DefaultFormulaOptions.Functions.DateTime.Make("Year( date )");
      }

      public static FormulaOption Month
      {
        get => DefaultFormulaOptions.Functions.DateTime.Make("Month( date )");
      }

      public static FormulaOption MonthName
      {
        get => DefaultFormulaOptions.Functions.DateTime.Make("MonthName( date )");
      }

      public static FormulaOption Day
      {
        get => DefaultFormulaOptions.Functions.DateTime.Make("Day( date )");
      }

      public static FormulaOption DayOfWeek
      {
        get => DefaultFormulaOptions.Functions.DateTime.Make("DayOfWeek( date )");
      }

      public static FormulaOption DayOfYear
      {
        get => DefaultFormulaOptions.Functions.DateTime.Make("DayOfYear( date )");
      }

      public static FormulaOption DayOrdinal
      {
        get => DefaultFormulaOptions.Functions.DateTime.Make("DayOrdinal( day )");
      }

      public static FormulaOption Hour
      {
        get => DefaultFormulaOptions.Functions.DateTime.Make("Hour( date )");
      }

      public static FormulaOption Minute
      {
        get => DefaultFormulaOptions.Functions.DateTime.Make("Minute( date )");
      }

      public static FormulaOption Second
      {
        get => DefaultFormulaOptions.Functions.DateTime.Make("Second( date )");
      }

      public static FormulaOption Now => DefaultFormulaOptions.Functions.DateTime.Make("Now()");

      public static FormulaOption NowUTC
      {
        get => DefaultFormulaOptions.Functions.DateTime.Make("NowUTC()");
      }

      public static FormulaOption Today => DefaultFormulaOptions.Functions.DateTime.Make("Today()");

      public static FormulaOption TodayUTC
      {
        get => DefaultFormulaOptions.Functions.DateTime.Make("TodayUTC()");
      }
    }

    public static class Math
    {
      private static FormulaOption Make(string value)
      {
        return DefaultFormulaOptions.Functions.Make(value, nameof (Math));
      }

      public static IEnumerable<FormulaOption> GetAll()
      {
        return (IEnumerable<FormulaOption>) new FormulaOption[7]
        {
          DefaultFormulaOptions.Functions.Math.Abs,
          DefaultFormulaOptions.Functions.Math.Ceiling,
          DefaultFormulaOptions.Functions.Math.Floor,
          DefaultFormulaOptions.Functions.Math.Max,
          DefaultFormulaOptions.Functions.Math.Min,
          DefaultFormulaOptions.Functions.Math.Power,
          DefaultFormulaOptions.Functions.Math.Round
        };
      }

      public static FormulaOption Abs => DefaultFormulaOptions.Functions.Math.Make("Abs( x )");

      public static FormulaOption Ceiling
      {
        get => DefaultFormulaOptions.Functions.Math.Make("Ceiling( x )");
      }

      public static FormulaOption Floor => DefaultFormulaOptions.Functions.Math.Make("Floor( x )");

      public static FormulaOption Max => DefaultFormulaOptions.Functions.Math.Make("Max( x, y )");

      public static FormulaOption Min => DefaultFormulaOptions.Functions.Math.Make("Min( x, y )");

      public static FormulaOption Power
      {
        get => DefaultFormulaOptions.Functions.Math.Make("Pow( x, pow )");
      }

      public static FormulaOption Round
      {
        get => DefaultFormulaOptions.Functions.Math.Make("Round( x, decimals )");
      }
    }

    public static class Other
    {
      private static FormulaOption Make(string value)
      {
        return DefaultFormulaOptions.Functions.Make(value, nameof (Other));
      }

      public static IEnumerable<FormulaOption> GetAll()
      {
        return (IEnumerable<FormulaOption>) new FormulaOption[6]
        {
          DefaultFormulaOptions.Functions.Other.IIf,
          DefaultFormulaOptions.Functions.Other.IsNull,
          DefaultFormulaOptions.Functions.Other.NullIf,
          DefaultFormulaOptions.Functions.Other.SubstituteAll,
          DefaultFormulaOptions.Functions.Other.SubstituteListed,
          DefaultFormulaOptions.Functions.Other.Switch
        };
      }

      public static FormulaOption IIf
      {
        get => DefaultFormulaOptions.Functions.Other.Make("IIf( expr, truePart, falsePart )");
      }

      public static FormulaOption IsNull
      {
        get => DefaultFormulaOptions.Functions.Other.Make("IsNull( value, nullValue )");
      }

      public static FormulaOption NullIf
      {
        get => DefaultFormulaOptions.Functions.Other.Make("NullIf( value1, value2 )");
      }

      public static FormulaOption SubstituteAll
      {
        get
        {
          return DefaultFormulaOptions.Functions.Other.Make("SubstituteAll( sourceField, substitutionList )");
        }
      }

      public static FormulaOption SubstituteListed
      {
        get
        {
          return DefaultFormulaOptions.Functions.Other.Make("SubstituteListed( sourceField, substitutionList )");
        }
      }

      public static FormulaOption Switch
      {
        get
        {
          return DefaultFormulaOptions.Functions.Other.Make("Switch( expr1, value1, expr2, value2, ...)");
        }
      }
    }

    public static class Text
    {
      private static FormulaOption Make(string value)
      {
        return DefaultFormulaOptions.Functions.Make(value, nameof (Text));
      }

      public static IEnumerable<FormulaOption> GetAll()
      {
        return (IEnumerable<FormulaOption>) new FormulaOption[15]
        {
          DefaultFormulaOptions.Functions.Text.Length,
          DefaultFormulaOptions.Functions.Text.Contains,
          DefaultFormulaOptions.Functions.Text.ContainsReversed,
          DefaultFormulaOptions.Functions.Text.TakeFirstSymbols,
          DefaultFormulaOptions.Functions.Text.TakeLastSymbols,
          DefaultFormulaOptions.Functions.Text.ToLowerCase,
          DefaultFormulaOptions.Functions.Text.ToUpperCase,
          DefaultFormulaOptions.Functions.Text.Trim,
          DefaultFormulaOptions.Functions.Text.TrimStart,
          DefaultFormulaOptions.Functions.Text.TrimEnd,
          DefaultFormulaOptions.Functions.Text.PadLeft,
          DefaultFormulaOptions.Functions.Text.PadRight,
          DefaultFormulaOptions.Functions.Text.Replace,
          DefaultFormulaOptions.Functions.Text.Substring,
          DefaultFormulaOptions.Functions.Text.Concat
        };
      }

      public static FormulaOption Length => DefaultFormulaOptions.Functions.Text.Make("Len( str )");

      public static FormulaOption Contains
      {
        get => DefaultFormulaOptions.Functions.Text.Make("InStr( str, findStr )");
      }

      public static FormulaOption ContainsReversed
      {
        get => DefaultFormulaOptions.Functions.Text.Make("InStrRev( str, findStr )");
      }

      public static FormulaOption TakeFirstSymbols
      {
        get => DefaultFormulaOptions.Functions.Text.Make("Left( str, length )");
      }

      public static FormulaOption TakeLastSymbols
      {
        get => DefaultFormulaOptions.Functions.Text.Make("Right( str, length )");
      }

      public static FormulaOption ToLowerCase
      {
        get => DefaultFormulaOptions.Functions.Text.Make("LCase( str )");
      }

      public static FormulaOption ToUpperCase
      {
        get => DefaultFormulaOptions.Functions.Text.Make("UCase( str )");
      }

      public static FormulaOption Trim => DefaultFormulaOptions.Functions.Text.Make("Trim( str )");

      public static FormulaOption TrimStart
      {
        get => DefaultFormulaOptions.Functions.Text.Make("LTrim( str )");
      }

      public static FormulaOption TrimEnd
      {
        get => DefaultFormulaOptions.Functions.Text.Make("RTrim( str )");
      }

      public static FormulaOption PadLeft
      {
        get => DefaultFormulaOptions.Functions.Text.Make("PadLeft( str, width, paddingChar )");
      }

      public static FormulaOption PadRight
      {
        get => DefaultFormulaOptions.Functions.Text.Make("PadRight( str, width, paddingChar )");
      }

      public static FormulaOption Replace
      {
        get => DefaultFormulaOptions.Functions.Text.Make("Replace( str, oldValue, newValue )");
      }

      public static FormulaOption Substring
      {
        get => DefaultFormulaOptions.Functions.Text.Make("Substring( str, start, length )");
      }

      public static FormulaOption Concat
      {
        get => DefaultFormulaOptions.Functions.Text.Make("Concat( str1, str2, ...)");
      }
    }
  }

  public static class Operators
  {
    private static FormulaOption Make(string value, params string[] subCategories)
    {
      return DefaultFormulaOptions.Make(value, nameof (Operators), subCategories);
    }

    public static IEnumerable<FormulaOption> GetAll()
    {
      return ((IEnumerable<FormulaOption>) Array.Empty<FormulaOption>()).Concat<FormulaOption>(DefaultFormulaOptions.Operators.Arithmetic.GetAll()).Concat<FormulaOption>(DefaultFormulaOptions.Operators.Logical.GetAll()).Concat<FormulaOption>(DefaultFormulaOptions.Operators.Comparison.GetAll()).Concat<FormulaOption>(DefaultFormulaOptions.Operators.Other.GetAll());
    }

    public static class Arithmetic
    {
      private static FormulaOption Make(string value)
      {
        return DefaultFormulaOptions.Operators.Make(value, nameof (Arithmetic));
      }

      public static IEnumerable<FormulaOption> GetAll()
      {
        return (IEnumerable<FormulaOption>) new FormulaOption[5]
        {
          DefaultFormulaOptions.Operators.Arithmetic.Plus,
          DefaultFormulaOptions.Operators.Arithmetic.Minus,
          DefaultFormulaOptions.Operators.Arithmetic.Multiply,
          DefaultFormulaOptions.Operators.Arithmetic.Divide,
          DefaultFormulaOptions.Operators.Arithmetic.Mod
        };
      }

      public static FormulaOption Plus => DefaultFormulaOptions.Operators.Arithmetic.Make("+");

      public static FormulaOption Minus => DefaultFormulaOptions.Operators.Arithmetic.Make("-");

      public static FormulaOption Multiply => DefaultFormulaOptions.Operators.Arithmetic.Make("*");

      public static FormulaOption Divide => DefaultFormulaOptions.Operators.Arithmetic.Make("/");

      public static FormulaOption Mod => DefaultFormulaOptions.Operators.Arithmetic.Make("%");
    }

    public static class Comparison
    {
      private static FormulaOption Make(string value)
      {
        return DefaultFormulaOptions.Operators.Make(value, nameof (Comparison));
      }

      public static IEnumerable<FormulaOption> GetAll()
      {
        return (IEnumerable<FormulaOption>) new FormulaOption[6]
        {
          DefaultFormulaOptions.Operators.Comparison.Equal,
          DefaultFormulaOptions.Operators.Comparison.NotEqual,
          DefaultFormulaOptions.Operators.Comparison.Less,
          DefaultFormulaOptions.Operators.Comparison.Greater,
          DefaultFormulaOptions.Operators.Comparison.LessOrEqual,
          DefaultFormulaOptions.Operators.Comparison.GreaterOrEqual
        };
      }

      public static FormulaOption Equal => DefaultFormulaOptions.Operators.Comparison.Make("=");

      public static FormulaOption NotEqual => DefaultFormulaOptions.Operators.Comparison.Make("<>");

      public static FormulaOption Less => DefaultFormulaOptions.Operators.Comparison.Make("<");

      public static FormulaOption Greater => DefaultFormulaOptions.Operators.Comparison.Make(">");

      public static FormulaOption LessOrEqual
      {
        get => DefaultFormulaOptions.Operators.Comparison.Make("<=");
      }

      public static FormulaOption GreaterOrEqual
      {
        get => DefaultFormulaOptions.Operators.Comparison.Make(">=");
      }
    }

    public static class Logical
    {
      private static FormulaOption Make(string value)
      {
        return DefaultFormulaOptions.Operators.Make(value, nameof (Logical));
      }

      public static IEnumerable<FormulaOption> GetAll()
      {
        return (IEnumerable<FormulaOption>) new FormulaOption[3]
        {
          DefaultFormulaOptions.Operators.Logical.And,
          DefaultFormulaOptions.Operators.Logical.Or,
          DefaultFormulaOptions.Operators.Logical.Not
        };
      }

      public static FormulaOption And => DefaultFormulaOptions.Operators.Logical.Make(nameof (And));

      public static FormulaOption Or => DefaultFormulaOptions.Operators.Logical.Make(nameof (Or));

      public static FormulaOption Not => DefaultFormulaOptions.Operators.Logical.Make(nameof (Not));
    }

    public static class Other
    {
      private static FormulaOption Make(string value)
      {
        return DefaultFormulaOptions.Operators.Make(value, nameof (Other));
      }

      public static IEnumerable<FormulaOption> GetAll()
      {
        return (IEnumerable<FormulaOption>) new FormulaOption[4]
        {
          DefaultFormulaOptions.Operators.Other.True,
          DefaultFormulaOptions.Operators.Other.False,
          DefaultFormulaOptions.Operators.Other.Null,
          DefaultFormulaOptions.Operators.Other.In
        };
      }

      public static FormulaOption True => DefaultFormulaOptions.Operators.Other.Make(nameof (True));

      public static FormulaOption False
      {
        get => DefaultFormulaOptions.Operators.Other.Make(nameof (False));
      }

      public static FormulaOption Null => DefaultFormulaOptions.Operators.Other.Make(nameof (Null));

      public static FormulaOption In => DefaultFormulaOptions.Operators.Other.Make(nameof (In));
    }
  }

  public static class Styles
  {
    private static FormulaOption Make(string value, params string[] subCategories)
    {
      return DefaultFormulaOptions.Make(value, nameof (Styles), subCategories);
    }

    public static IEnumerable<FormulaOption> GetAll()
    {
      return (IEnumerable<FormulaOption>) new FormulaOption[36]
      {
        DefaultFormulaOptions.Styles.Default,
        DefaultFormulaOptions.Styles.Bad,
        DefaultFormulaOptions.Styles.Good,
        DefaultFormulaOptions.Styles.Neutral,
        DefaultFormulaOptions.Styles.Bold,
        DefaultFormulaOptions.Styles.Italic,
        DefaultFormulaOptions.Styles.Red,
        DefaultFormulaOptions.Styles.Red60,
        DefaultFormulaOptions.Styles.Red40,
        DefaultFormulaOptions.Styles.Red20,
        DefaultFormulaOptions.Styles.Red00,
        DefaultFormulaOptions.Styles.Orange,
        DefaultFormulaOptions.Styles.Orange60,
        DefaultFormulaOptions.Styles.Orange40,
        DefaultFormulaOptions.Styles.Orange20,
        DefaultFormulaOptions.Styles.Orange00,
        DefaultFormulaOptions.Styles.Green,
        DefaultFormulaOptions.Styles.Green60,
        DefaultFormulaOptions.Styles.Green40,
        DefaultFormulaOptions.Styles.Green20,
        DefaultFormulaOptions.Styles.Green00,
        DefaultFormulaOptions.Styles.Blue,
        DefaultFormulaOptions.Styles.Blue60,
        DefaultFormulaOptions.Styles.Blue40,
        DefaultFormulaOptions.Styles.Blue20,
        DefaultFormulaOptions.Styles.Blue00,
        DefaultFormulaOptions.Styles.Yellow,
        DefaultFormulaOptions.Styles.Yellow60,
        DefaultFormulaOptions.Styles.Yellow40,
        DefaultFormulaOptions.Styles.Yellow20,
        DefaultFormulaOptions.Styles.Yellow00,
        DefaultFormulaOptions.Styles.Purple,
        DefaultFormulaOptions.Styles.Purple60,
        DefaultFormulaOptions.Styles.Purple40,
        DefaultFormulaOptions.Styles.Purple20,
        DefaultFormulaOptions.Styles.Purple00
      };
    }

    public static FormulaOption Default => DefaultFormulaOptions.Styles.Make("'default'");

    public static FormulaOption Bad => DefaultFormulaOptions.Styles.Make("'bad'");

    public static FormulaOption Good => DefaultFormulaOptions.Styles.Make("'good'");

    public static FormulaOption Neutral => DefaultFormulaOptions.Styles.Make("'neutral'");

    public static FormulaOption Bold => DefaultFormulaOptions.Styles.Make("'bold'");

    public static FormulaOption Italic => DefaultFormulaOptions.Styles.Make("'italic'");

    public static FormulaOption Red => DefaultFormulaOptions.Styles.Make("'red'");

    public static FormulaOption Red60 => DefaultFormulaOptions.Styles.Make("'red60'");

    public static FormulaOption Red40 => DefaultFormulaOptions.Styles.Make("'red40'");

    public static FormulaOption Red20 => DefaultFormulaOptions.Styles.Make("'red20'");

    public static FormulaOption Red00 => DefaultFormulaOptions.Styles.Make("'red0'");

    public static FormulaOption Orange => DefaultFormulaOptions.Styles.Make("'orange'");

    public static FormulaOption Orange60 => DefaultFormulaOptions.Styles.Make("'orange60'");

    public static FormulaOption Orange40 => DefaultFormulaOptions.Styles.Make("'orange40'");

    public static FormulaOption Orange20 => DefaultFormulaOptions.Styles.Make("'orange20'");

    public static FormulaOption Orange00 => DefaultFormulaOptions.Styles.Make("'orange0'");

    public static FormulaOption Green => DefaultFormulaOptions.Styles.Make("'green'");

    public static FormulaOption Green60 => DefaultFormulaOptions.Styles.Make("'green60'");

    public static FormulaOption Green40 => DefaultFormulaOptions.Styles.Make("'green40'");

    public static FormulaOption Green20 => DefaultFormulaOptions.Styles.Make("'green20'");

    public static FormulaOption Green00 => DefaultFormulaOptions.Styles.Make("'green0'");

    public static FormulaOption Blue => DefaultFormulaOptions.Styles.Make("'blue'");

    public static FormulaOption Blue60 => DefaultFormulaOptions.Styles.Make("'blue60'");

    public static FormulaOption Blue40 => DefaultFormulaOptions.Styles.Make("'blue40'");

    public static FormulaOption Blue20 => DefaultFormulaOptions.Styles.Make("'blue20'");

    public static FormulaOption Blue00 => DefaultFormulaOptions.Styles.Make("'blue0'");

    public static FormulaOption Yellow => DefaultFormulaOptions.Styles.Make("'yellow'");

    public static FormulaOption Yellow60 => DefaultFormulaOptions.Styles.Make("'yellow60'");

    public static FormulaOption Yellow40 => DefaultFormulaOptions.Styles.Make("'yellow40'");

    public static FormulaOption Yellow20 => DefaultFormulaOptions.Styles.Make("'yellow20'");

    public static FormulaOption Yellow00 => DefaultFormulaOptions.Styles.Make("'yellow0'");

    public static FormulaOption Purple => DefaultFormulaOptions.Styles.Make("'purple'");

    public static FormulaOption Purple60 => DefaultFormulaOptions.Styles.Make("'purple60'");

    public static FormulaOption Purple40 => DefaultFormulaOptions.Styles.Make("'purple40'");

    public static FormulaOption Purple20 => DefaultFormulaOptions.Styles.Make("'purple20'");

    public static FormulaOption Purple00 => DefaultFormulaOptions.Styles.Make("'purple0'");
  }
}
