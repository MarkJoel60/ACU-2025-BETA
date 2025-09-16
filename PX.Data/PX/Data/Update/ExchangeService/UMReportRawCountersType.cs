// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.UMReportRawCountersType
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
public class UMReportRawCountersType
{
  private long autoAttendantCallsField;
  private long failedCallsField;
  private long faxCallsField;
  private long missedCallsField;
  private long otherCallsField;
  private long outboundCallsField;
  private long subscriberAccessCallsField;
  private long voiceMailCallsField;
  private long totalCallsField;
  private System.DateTime dateField;
  private UMReportAudioMetricsAverageCountersType audioMetricsAveragesField;

  /// <remarks />
  public long AutoAttendantCalls
  {
    get => this.autoAttendantCallsField;
    set => this.autoAttendantCallsField = value;
  }

  /// <remarks />
  public long FailedCalls
  {
    get => this.failedCallsField;
    set => this.failedCallsField = value;
  }

  /// <remarks />
  public long FaxCalls
  {
    get => this.faxCallsField;
    set => this.faxCallsField = value;
  }

  /// <remarks />
  public long MissedCalls
  {
    get => this.missedCallsField;
    set => this.missedCallsField = value;
  }

  /// <remarks />
  public long OtherCalls
  {
    get => this.otherCallsField;
    set => this.otherCallsField = value;
  }

  /// <remarks />
  public long OutboundCalls
  {
    get => this.outboundCallsField;
    set => this.outboundCallsField = value;
  }

  /// <remarks />
  public long SubscriberAccessCalls
  {
    get => this.subscriberAccessCallsField;
    set => this.subscriberAccessCallsField = value;
  }

  /// <remarks />
  public long VoiceMailCalls
  {
    get => this.voiceMailCallsField;
    set => this.voiceMailCallsField = value;
  }

  /// <remarks />
  public long TotalCalls
  {
    get => this.totalCallsField;
    set => this.totalCallsField = value;
  }

  /// <remarks />
  public System.DateTime Date
  {
    get => this.dateField;
    set => this.dateField = value;
  }

  /// <remarks />
  public UMReportAudioMetricsAverageCountersType AudioMetricsAverages
  {
    get => this.audioMetricsAveragesField;
    set => this.audioMetricsAveragesField = value;
  }
}
