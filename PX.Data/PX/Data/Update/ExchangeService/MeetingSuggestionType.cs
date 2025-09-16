// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.MeetingSuggestionType
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
public class MeetingSuggestionType : EntityType
{
  private EmailUserType[] attendeesField;
  private string locationField;
  private string subjectField;
  private string meetingStringField;
  private System.DateTime startTimeField;
  private bool startTimeFieldSpecified;
  private System.DateTime endTimeField;
  private bool endTimeFieldSpecified;

  /// <remarks />
  [XmlArrayItem("EmailUser", IsNullable = false)]
  public EmailUserType[] Attendees
  {
    get => this.attendeesField;
    set => this.attendeesField = value;
  }

  /// <remarks />
  public string Location
  {
    get => this.locationField;
    set => this.locationField = value;
  }

  /// <remarks />
  public string Subject
  {
    get => this.subjectField;
    set => this.subjectField = value;
  }

  /// <remarks />
  public string MeetingString
  {
    get => this.meetingStringField;
    set => this.meetingStringField = value;
  }

  /// <remarks />
  public System.DateTime StartTime
  {
    get => this.startTimeField;
    set => this.startTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool StartTimeSpecified
  {
    get => this.startTimeFieldSpecified;
    set => this.startTimeFieldSpecified = value;
  }

  /// <remarks />
  public System.DateTime EndTime
  {
    get => this.endTimeField;
    set => this.endTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool EndTimeSpecified
  {
    get => this.endTimeFieldSpecified;
    set => this.endTimeFieldSpecified = value;
  }
}
