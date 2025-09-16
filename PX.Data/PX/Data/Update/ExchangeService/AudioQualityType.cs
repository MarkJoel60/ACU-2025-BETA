// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.ExchangeService.AudioQualityType
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
public class AudioQualityType
{
  private float nMOSField;
  private float nMOSDegradationField;
  private float nMOSDegradationPacketLossField;
  private float nMOSDegradationJitterField;
  private float jitterField;
  private float packetLossField;
  private float roundTripField;
  private float burstDensityField;
  private float burstDurationField;
  private string audioCodecField;

  /// <remarks />
  public float NMOS
  {
    get => this.nMOSField;
    set => this.nMOSField = value;
  }

  /// <remarks />
  public float NMOSDegradation
  {
    get => this.nMOSDegradationField;
    set => this.nMOSDegradationField = value;
  }

  /// <remarks />
  public float NMOSDegradationPacketLoss
  {
    get => this.nMOSDegradationPacketLossField;
    set => this.nMOSDegradationPacketLossField = value;
  }

  /// <remarks />
  public float NMOSDegradationJitter
  {
    get => this.nMOSDegradationJitterField;
    set => this.nMOSDegradationJitterField = value;
  }

  /// <remarks />
  public float Jitter
  {
    get => this.jitterField;
    set => this.jitterField = value;
  }

  /// <remarks />
  public float PacketLoss
  {
    get => this.packetLossField;
    set => this.packetLossField = value;
  }

  /// <remarks />
  public float RoundTrip
  {
    get => this.roundTripField;
    set => this.roundTripField = value;
  }

  /// <remarks />
  public float BurstDensity
  {
    get => this.burstDensityField;
    set => this.burstDensityField = value;
  }

  /// <remarks />
  public float BurstDuration
  {
    get => this.burstDurationField;
    set => this.burstDurationField = value;
  }

  /// <remarks />
  public string AudioCodec
  {
    get => this.audioCodecField;
    set => this.audioCodecField = value;
  }
}
