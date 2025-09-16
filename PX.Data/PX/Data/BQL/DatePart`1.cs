// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.DatePart`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.BQL;

/// <summary>
/// Provides access to the date components of a <typeparamref name="TOperand" />.
/// It's members are strongly typed versions of <see cref="T:PX.Data.DatePart`2" />.
/// </summary>
public static class DatePart<TOperand> where TOperand : IBqlOperand, IImplement<IBqlDateTime>
{
  /// <summary>
  /// Returns the year component of the date.
  /// Equivalent to SQL function DATEPART('yyyy', DATE).
  /// </summary>
  public sealed class Year : BqlFunction<DatePart<DatePart.year, TOperand>, IBqlShort>
  {
  }

  /// <summary>
  /// Returns the quarter component of the date.
  /// Equivalent to SQL function DATEPART('qq', DATE).
  /// </summary>
  public sealed class Quarter : BqlFunction<DatePart<DatePart.quarter, TOperand>, IBqlShort>
  {
  }

  /// <summary>
  /// Returns the month component of the date.
  /// Equivalent to SQL function DATEPART('mm', DATE).
  /// </summary>
  public sealed class Month : BqlFunction<DatePart<DatePart.month, TOperand>, IBqlShort>
  {
  }

  /// <summary>
  /// Returns the week component of the date.
  /// Equivalent to SQL function DATEPART('ww', DATE).
  /// </summary>
  public sealed class Week : BqlFunction<DatePart<DatePart.week, TOperand>, IBqlShort>
  {
  }

  /// <summary>
  /// Returns the week day component of the date.
  /// Equivalent to SQL function DATEPART('dw', DATE).
  /// </summary>
  public sealed class WeekDay : BqlFunction<DatePart<DatePart.weekDay, TOperand>, IBqlShort>
  {
  }

  /// <summary>
  /// Returns the day component of the date.
  /// Equivalent to SQL function DATEPART('dd', DATE).
  /// </summary>
  public sealed class Day : BqlFunction<DatePart<DatePart.day, TOperand>, IBqlShort>
  {
  }

  /// <summary>
  /// Returns the day of year component of the date.
  /// Equivalent to SQL function DATEPART('dy', DATE).
  /// </summary>
  public sealed class DayOfYear : BqlFunction<DatePart<DatePart.dayOfYear, TOperand>, IBqlShort>
  {
  }

  /// <summary>
  /// Returns the hour component of the date.
  /// Equivalent to SQL function DATEPART('hh', DATE).
  /// </summary>
  public sealed class Hour : BqlFunction<DatePart<DatePart.hour, TOperand>, IBqlShort>
  {
  }

  /// <summary>
  /// Returns the minute component of the date.
  /// Equivalent to SQL function DATEPART('mi', DATE).
  /// </summary>
  public sealed class Minute : BqlFunction<DatePart<DatePart.minute, TOperand>, IBqlShort>
  {
  }

  /// <summary>
  /// Returns the second component of the date.
  /// Equivalent to SQL function DATEPART('ss', DATE).
  /// </summary>
  public sealed class Second : BqlFunction<DatePart<DatePart.second, TOperand>, IBqlShort>
  {
  }
}
