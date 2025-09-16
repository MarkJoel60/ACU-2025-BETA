// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.IPrimaryNotificationQueueWriter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.PushNotifications;

public interface IPrimaryNotificationQueueWriter
{
  void BeginTransaction(Guid correlationId);

  void CommitTransaction(Guid correlationId, string connectionString);

  void RollbackTransaction(Guid correlationId);

  void Send(QueueEvent messageData, Guid correlationId);

  void SendIdentity(string fieldName, string tableName, object value, Guid correlationId);

  void ReadyToCommitTransaction(Guid correlationId);

  bool IsEnabled { get; }

  (long current, long max) GetQueueSize();
}
