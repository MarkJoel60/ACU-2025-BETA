// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.SuggestionsViewOptionsType
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
public class SuggestionsViewOptionsType
{
  private int goodThresholdField;
  private bool goodThresholdFieldSpecified;
  private int maximumResultsByDayField;
  private bool maximumResultsByDayFieldSpecified;
  private int maximumNonWorkHourResultsByDayField;
  private bool maximumNonWorkHourResultsByDayFieldSpecified;
  private int meetingDurationInMinutesField;
  private bool meetingDurationInMinutesFieldSpecified;
  private SuggestionQuality minimumSuggestionQualityField;
  private bool minimumSuggestionQualityFieldSpecified;
  private Duration detailedSuggestionsWindowField;
  private System.DateTime currentMeetingTimeField;
  private bool currentMeetingTimeFieldSpecified;
  private string globalObjectIdField;

  /// <remarks />
  public int GoodThreshold
  {
    get => this.goodThresholdField;
    set => this.goodThresholdField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool GoodThresholdSpecified
  {
    get => this.goodThresholdFieldSpecified;
    set => this.goodThresholdFieldSpecified = value;
  }

  /// <remarks />
  public int MaximumResultsByDay
  {
    get => this.maximumResultsByDayField;
    set => this.maximumResultsByDayField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MaximumResultsByDaySpecified
  {
    get => this.maximumResultsByDayFieldSpecified;
    set => this.maximumResultsByDayFieldSpecified = value;
  }

  /// <remarks />
  public int MaximumNonWorkHourResultsByDay
  {
    get => this.maximumNonWorkHourResultsByDayField;
    set => this.maximumNonWorkHourResultsByDayField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MaximumNonWorkHourResultsByDaySpecified
  {
    get => this.maximumNonWorkHourResultsByDayFieldSpecified;
    set => this.maximumNonWorkHourResultsByDayFieldSpecified = value;
  }

  /// <remarks />
  public int MeetingDurationInMinutes
  {
    get => this.meetingDurationInMinutesField;
    set => this.meetingDurationInMinutesField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MeetingDurationInMinutesSpecified
  {
    get => this.meetingDurationInMinutesFieldSpecified;
    set => this.meetingDurationInMinutesFieldSpecified = value;
  }

  /// <remarks />
  public SuggestionQuality MinimumSuggestionQuality
  {
    get => this.minimumSuggestionQualityField;
    set => this.minimumSuggestionQualityField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool MinimumSuggestionQualitySpecified
  {
    get => this.minimumSuggestionQualityFieldSpecified;
    set => this.minimumSuggestionQualityFieldSpecified = value;
  }

  /// <remarks />
  public Duration DetailedSuggestionsWindow
  {
    get => this.detailedSuggestionsWindowField;
    set => this.detailedSuggestionsWindowField = value;
  }

  /// <remarks />
  public System.DateTime CurrentMeetingTime
  {
    get => this.currentMeetingTimeField;
    set => this.currentMeetingTimeField = value;
  }

  /// <remarks />
  [XmlIgnore]
  public bool CurrentMeetingTimeSpecified
  {
    get => this.currentMeetingTimeFieldSpecified;
    set => this.currentMeetingTimeFieldSpecified = value;
  }

  /// <remarks />
  public string GlobalObjectId
  {
    get => this.globalObjectIdField;
    set => this.globalObjectIdField = value;
  }
}
