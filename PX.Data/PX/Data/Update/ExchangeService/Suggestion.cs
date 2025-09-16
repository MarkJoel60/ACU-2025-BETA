// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.Suggestion
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
public class Suggestion
{
  private System.DateTime meetingTimeField;
  private bool isWorkTimeField;
  private SuggestionQuality suggestionQualityField;
  private AttendeeConflictData[] attendeeConflictDataArrayField;

  /// <remarks />
  public System.DateTime MeetingTime
  {
    get => this.meetingTimeField;
    set => this.meetingTimeField = value;
  }

  /// <remarks />
  public bool IsWorkTime
  {
    get => this.isWorkTimeField;
    set => this.isWorkTimeField = value;
  }

  /// <remarks />
  public SuggestionQuality SuggestionQuality
  {
    get => this.suggestionQualityField;
    set => this.suggestionQualityField = value;
  }

  /// <remarks />
  [XmlArrayItem(typeof (GroupAttendeeConflictData))]
  [XmlArrayItem(typeof (IndividualAttendeeConflictData))]
  [XmlArrayItem(typeof (TooBigGroupAttendeeConflictData))]
  [XmlArrayItem(typeof (UnknownAttendeeConflictData))]
  public AttendeeConflictData[] AttendeeConflictDataArray
  {
    get => this.attendeeConflictDataArrayField;
    set => this.attendeeConflictDataArrayField = value;
  }
}
