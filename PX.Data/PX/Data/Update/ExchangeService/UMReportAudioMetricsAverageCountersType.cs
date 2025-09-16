// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.UMReportAudioMetricsAverageCountersType
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
public class UMReportAudioMetricsAverageCountersType
{
  private AudioMetricsAverageType nMOSField;
  private AudioMetricsAverageType nMOSDegradationField;
  private AudioMetricsAverageType jitterField;
  private AudioMetricsAverageType percentPacketLossField;
  private AudioMetricsAverageType roundTripField;
  private AudioMetricsAverageType burstLossDurationField;
  private long totalAudioQualityCallsSampledField;

  /// <remarks />
  public AudioMetricsAverageType NMOS
  {
    get => this.nMOSField;
    set => this.nMOSField = value;
  }

  /// <remarks />
  public AudioMetricsAverageType NMOSDegradation
  {
    get => this.nMOSDegradationField;
    set => this.nMOSDegradationField = value;
  }

  /// <remarks />
  public AudioMetricsAverageType Jitter
  {
    get => this.jitterField;
    set => this.jitterField = value;
  }

  /// <remarks />
  public AudioMetricsAverageType PercentPacketLoss
  {
    get => this.percentPacketLossField;
    set => this.percentPacketLossField = value;
  }

  /// <remarks />
  public AudioMetricsAverageType RoundTrip
  {
    get => this.roundTripField;
    set => this.roundTripField = value;
  }

  /// <remarks />
  public AudioMetricsAverageType BurstLossDuration
  {
    get => this.burstLossDurationField;
    set => this.burstLossDurationField = value;
  }

  /// <remarks />
  public long TotalAudioQualityCallsSampled
  {
    get => this.totalAudioQualityCallsSampledField;
    set => this.totalAudioQualityCallsSampledField = value;
  }
}
