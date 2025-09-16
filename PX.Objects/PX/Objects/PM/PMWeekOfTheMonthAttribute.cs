// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMWeekOfTheMonthAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.PM;

/// <summary>Attribute for week of the month.</summary>
public sealed class PMWeekOfTheMonthAttribute(Type dateField) : PMDatePartAttribute(dateField)
{
  protected override object GetDatePartValue(DateTime dt)
  {
    return (object) PMWeekOfTheMonthAttribute.GetWeekOfMonth(dt);
  }

  public static int GetWeekOfMonth(DateTime date)
  {
    return PMWeekOfTheYearAttribute.GetWeekOfYear(date) - PMWeekOfTheYearAttribute.GetWeekOfYear(new DateTime(date.Year, date.Month, 1)) + 1;
  }
}
