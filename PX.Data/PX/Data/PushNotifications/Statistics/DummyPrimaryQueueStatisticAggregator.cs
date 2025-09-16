// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.Statistics.DummyPrimaryQueueStatisticAggregator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.DbServices.Commands;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.PushNotifications.Statistics;

/// <exclude />
[PXInternalUseOnly]
public class DummyPrimaryQueueStatisticAggregator : 
  IPrimaryQueueStatisticAggregator,
  IQueueStatisticAggregator
{
  public void SendInMessageMetadata(
    IEnumerable<PrimaryQueueInMessageMetadata> data,
    Guid transactionId)
  {
  }

  public bool LogStatisticDetails { get; set; }

  public void SendOutMessageMetadata(QueueOutMessageMetadata data)
  {
  }

  public IEnumerable<CommandBase> GetDeleteStatisticsCommand(int keepStatisticsPeriod)
  {
    yield break;
  }

  public void ReportSlowLog(QueueOutMessageMetadata data)
  {
  }

  public void ReportStatistics()
  {
  }
}
