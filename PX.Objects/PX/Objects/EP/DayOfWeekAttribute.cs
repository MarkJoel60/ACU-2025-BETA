// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.DayOfWeekAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Globalization;

#nullable disable
namespace PX.Objects.EP;

/// <summary>List days of week.</summary>
/// <example>[DayOfWeek]</example>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class DayOfWeekAttribute : PXIntListAttribute
{
  public virtual bool IsLocalizable => false;

  public virtual void CacheAttached(PXCache sender)
  {
    this._AllowedValues = new int[7]{ 0, 1, 2, 3, 4, 5, 6 };
    this._AllowedLabels = new string[7]
    {
      DayOfWeekAttribute.GetDayName(DayOfWeek.Sunday),
      DayOfWeekAttribute.GetDayName(DayOfWeek.Monday),
      DayOfWeekAttribute.GetDayName(DayOfWeek.Tuesday),
      DayOfWeekAttribute.GetDayName(DayOfWeek.Wednesday),
      DayOfWeekAttribute.GetDayName(DayOfWeek.Thursday),
      DayOfWeekAttribute.GetDayName(DayOfWeek.Friday),
      DayOfWeekAttribute.GetDayName(DayOfWeek.Saturday)
    };
    this._NeutralAllowedLabels = this._AllowedLabels;
    base.CacheAttached(sender);
  }

  private static string GetDayName(DayOfWeek day)
  {
    return CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(day);
  }
}
