// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMWeekOfTheYearAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Globalization;

#nullable disable
namespace PX.Objects.PM;

/// <summary>Attribute for week of the year.</summary>
public sealed class PMWeekOfTheYearAttribute(Type dateField) : PMDatePartAttribute(dateField)
{
  protected override object GetDatePartValue(DateTime dt)
  {
    return (object) PMWeekOfTheYearAttribute.GetWeekOfYear(dt);
  }

  public static int GetWeekOfYear(DateTime date)
  {
    return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
  }
}
