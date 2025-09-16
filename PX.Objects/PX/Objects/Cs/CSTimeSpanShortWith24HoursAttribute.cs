// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CSTimeSpanShortWith24HoursAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS;

[PXInternalUseOnly]
public class CSTimeSpanShortWith24HoursAttribute : PXTimeSpanLongAttribute
{
  public static readonly int MinutesInDay = (int) TimeSpan.FromDays(1.0).TotalMinutes;
  public const string StringValueFor24Hours = "2400";

  public CSTimeSpanShortWith24HoursAttribute() => this.Format = (TimeSpanFormatType) 4;

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    int num = !(e.ReturnValue is int returnValue) ? 0 : (returnValue == CSTimeSpanShortWith24HoursAttribute.MinutesInDay ? 1 : 0);
    base.FieldSelecting(sender, e);
    if (num == 0 || !(e.ReturnValue is string))
      return;
    e.ReturnValue = (object) "2400";
  }
}
