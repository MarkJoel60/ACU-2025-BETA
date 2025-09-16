// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.Statistics.QueueOutMessageMetadata
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Telemetry;
using PX.PushNotifications;
using System;

#nullable disable
namespace PX.Data.PushNotifications.Statistics;

[PXInternalUseOnly]
public class QueueOutMessageMetadata
{
  public TimeSpan elapsed { get; set; }

  public bool IsThresholdExceeded { get; set; }

  public Guid DbTransactionId { get; set; }

  public TransactionLogInfo LogInfo { get; set; }

  public string QueueType { get; set; }

  internal ITelemetryInfo TelemetryInfo { get; set; }
}
