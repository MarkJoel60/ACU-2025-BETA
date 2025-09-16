// Decompiled with JetBrains decompiler
// Type: PX.Data.EP.EPCalendarDashboardGraphAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.EP;

/// <exclude />
public class EPCalendarDashboardGraphAttribute : Attribute
{
  private readonly System.Type _calendarGraphType;
  private readonly string _filterViewName;
  private readonly string _eventsViewName;

  public EPCalendarDashboardGraphAttribute(
    System.Type calendarGraph,
    string filterView,
    string eventsView)
  {
    this._calendarGraphType = calendarGraph;
    this._filterViewName = filterView;
    this._eventsViewName = eventsView;
  }

  public EPCalendarDashboardGraphAttribute(System.Type calendarGraph, string eventsView)
    : this(calendarGraph, (string) null, eventsView)
  {
  }

  public System.Type CalendarGraphType => this._calendarGraphType;

  public string FilterViewName => this._filterViewName;

  public string EventsViewName => this._eventsViewName;
}
