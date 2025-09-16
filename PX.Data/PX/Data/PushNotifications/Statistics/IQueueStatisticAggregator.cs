// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.Statistics.IQueueStatisticAggregator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.DbServices.Commands;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.PushNotifications.Statistics;

public interface IQueueStatisticAggregator
{
  void ReportStatistics();

  void ReportSlowLog(QueueOutMessageMetadata data);

  void SendOutMessageMetadata(QueueOutMessageMetadata data);

  IEnumerable<CommandBase> GetDeleteStatisticsCommand(int keepStatisticsPeriod);

  bool LogStatisticDetails { set; get; }
}
