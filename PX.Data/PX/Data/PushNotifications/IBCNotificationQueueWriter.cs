// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.IBCNotificationQueueWriter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.PushNotifications;

/// <summary>
/// Represents an interface for raising events in the commerce queue
/// </summary>
public interface IBCNotificationQueueWriter
{
  /// <summary>Raises a commerce notification event</summary>
  void RaiseEvent(BCQueueMessage @event);

  (long current, long max) GetQueueSize();
}
