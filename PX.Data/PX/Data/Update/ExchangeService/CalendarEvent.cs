// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.CalendarEvent
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

#nullable disable
namespace PX.Data.Update.ExchangeService;

/// <remarks />
[GeneratedCode("System.Xml", "4.0.30319.18408")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
[Serializable]
public class CalendarEvent
{
  private System.DateTime startTimeField;
  private System.DateTime endTimeField;
  private LegacyFreeBusyType busyTypeField;
  private CalendarEventDetails calendarEventDetailsField;

  /// <remarks />
  public System.DateTime StartTime
  {
    get => this.startTimeField;
    set => this.startTimeField = value;
  }

  /// <remarks />
  public System.DateTime EndTime
  {
    get => this.endTimeField;
    set => this.endTimeField = value;
  }

  /// <remarks />
  public LegacyFreeBusyType BusyType
  {
    get => this.busyTypeField;
    set => this.busyTypeField = value;
  }

  /// <remarks />
  public CalendarEventDetails CalendarEventDetails
  {
    get => this.calendarEventDetailsField;
    set => this.calendarEventDetailsField = value;
  }
}
