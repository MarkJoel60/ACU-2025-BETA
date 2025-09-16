// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.MeetingResponseMessageType
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
public class MeetingResponseMessageType : MeetingMessageType
{
  private System.DateTime startField;
  private bool startFieldSpecified;
  private System.DateTime endField;
  private bool endFieldSpecified;
  private string locationField;
  private RecurrenceType recurrenceField;
  private string calendarItemTypeField;
  private System.DateTime proposedStartField;
  private bool proposedStartFieldSpecified;
  private System.DateTime proposedEndField;
  private bool proposedEndFieldSpecified;
  private EnhancedLocationType enhancedLocationField;

  /// <remarks />
  public System.DateTime Start
  {
    get => this.startField;
    set => this.startField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool StartSpecified
  {
    get => this.startFieldSpecified;
    set => this.startFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime End
  {
    get => this.endField;
    set => this.endField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool EndSpecified
  {
    get => this.endFieldSpecified;
    set => this.endFieldSpecified = value;
  }

  /// <remarks />
  public string Location
  {
    get => this.locationField;
    set => this.locationField = value;
  }

  /// <remarks />
  public RecurrenceType Recurrence
  {
    get => this.recurrenceField;
    set => this.recurrenceField = value;
  }

  /// <remarks />
  public string CalendarItemType
  {
    get => this.calendarItemTypeField;
    set => this.calendarItemTypeField = value;
  }

  /// <remarks />
  public System.DateTime ProposedStart
  {
    get => this.proposedStartField;
    set => this.proposedStartField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ProposedStartSpecified
  {
    get => this.proposedStartFieldSpecified;
    set => this.proposedStartFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime ProposedEnd
  {
    get => this.proposedEndField;
    set => this.proposedEndField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool ProposedEndSpecified
  {
    get => this.proposedEndFieldSpecified;
    set => this.proposedEndFieldSpecified = value;
  }

  /// <remarks />
  public EnhancedLocationType EnhancedLocation
  {
    get => this.enhancedLocationField;
    set => this.enhancedLocationField = value;
  }
}
