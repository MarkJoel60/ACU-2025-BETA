// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.IBCNotificationQueueReader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace PX.Data.PushNotifications;

/// <summary>
/// Represents an interface for reading the commerce event queue and processing it to <see cref="!:PX.Commerce.Core.IBCNotificationQueueProcessor" />
/// </summary>
public interface IBCNotificationQueueReader
{
  void ReInitQueue();

  bool TryReadNextEvent(
    CancellationToken cancellationToken,
    out IEnumerable<BCQueueMessage> events,
    out string finalMessageId,
    TimeSpan? timeout = null);

  void CompleteEventProcessing(BCQueueMessage @event, string finalMessageId);
}
