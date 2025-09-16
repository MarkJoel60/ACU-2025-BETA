// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.Statistics.IQueueDispatcherWithStatusInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.PushNotifications.Statistics;

public interface IQueueDispatcherWithStatusInfo
{
  /// <summary>
  /// Have to be 2 characters long (to fit nchar(2) column in database)
  /// </summary>
  string Type { get; }

  int Order { get; }

  string GetQueueName();

  bool GetQueueStatus();

  int QueueCount();

  int SlowTransactionThreshold { get; set; }

  int MaxLogCount { get; set; }

  bool LogStatisticDetail { set; }

  DispatcherStatus GetDispatcherStatus();

  void PurgeQueue();

  /// <summary>
  /// This method stops the dispatcher, invoke action, after completion starts dispatcher
  /// </summary>
  /// <param name="action"></param>
  void DoMaintenanceWork(System.Action action);

  (long current, long max) QueueSize();

  bool HasFailedToCommitMessage();
}
