// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.DummyPrimaryNotificationQueue
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Threading;

#nullable disable
namespace PX.Data.PushNotifications;

/// <exclude />
public class DummyPrimaryNotificationQueue : 
  IPrimaryNotificationQueueWriter,
  IPrimaryNotificationQueueReader
{
  public void BeginTransaction(Guid correlationId)
  {
  }

  public void CommitTransaction(Guid correlationId, string connectionString)
  {
  }

  public void RollbackTransaction(Guid correlationId)
  {
  }

  public void Send(QueueEvent messageData, Guid correlationId)
  {
  }

  public void SendIdentity(string fieldName, string tableName, object value, Guid correlationId)
  {
  }

  public void ReadyToCommitTransaction(Guid correlationId)
  {
  }

  public bool IsEnabled => false;

  public (long current, long max) GetQueueSize() => (-1L, -1L);

  public void ReInitQueue()
  {
  }

  public bool ReadNextMessageTransaction(
    CancellationToken cancellationToken,
    out EventsTransaction messages)
  {
    messages = (EventsTransaction) null;
    return false;
  }

  public void CompleteReading(Guid correlationId)
  {
  }
}
