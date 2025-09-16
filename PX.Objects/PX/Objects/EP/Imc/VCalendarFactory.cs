// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.Imc.VCalendarFactory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Export.Imc;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.EP.Imc;

public class VCalendarFactory : IVCalendarFactory
{
  private const string OneOfRecordsIsNull = "One of records is null";
  private readonly IEnumerable<IVCalendarProcessor> _calendarProcessors;

  public VCalendarFactory(
    IEnumerable<IVCalendarProcessor> calendarProcessors)
  {
    this._calendarProcessors = calendarProcessors;
  }

  public vCalendar CreateVCalendar(IEnumerable events)
  {
    ExceptionExtensions.ThrowOnNull<IEnumerable>(events, nameof (events), (string) null);
    vCalendar vcalendar = new vCalendar();
    foreach (object obj in events)
    {
      ExceptionExtensions.ThrowOnNull<object>(obj, nameof (events), "One of records is null");
      vEvent vevent = this.CreateVEvent(obj);
      if (vEvent.op_Inequality(vevent, vEvent.Empty))
        ((vCalendarBase<vEvent>) vcalendar).AddEvent(vevent);
    }
    return vcalendar;
  }

  public vEvent CreateVEvent(object item)
  {
    vEvent card = new vEvent();
    foreach (IVCalendarProcessor calendarProcessor in this._calendarProcessors)
      calendarProcessor.Process(card, item);
    return card;
  }
}
