// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.IPrimaryNotificationQueueReader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Threading;

#nullable disable
namespace PX.Data.PushNotifications;

public interface IPrimaryNotificationQueueReader
{
  void ReInitQueue();

  bool ReadNextMessageTransaction(
    CancellationToken cancellationToken,
    out EventsTransaction messages);

  void CompleteReading(Guid correlationId);
}
