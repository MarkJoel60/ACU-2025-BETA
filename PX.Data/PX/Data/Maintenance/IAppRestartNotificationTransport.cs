// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.IAppRestartNotificationTransport
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Threading;

#nullable disable
namespace PX.Data.Maintenance;

/// <summary>
/// Represents transport to be used to notify all nodes about required restart
/// </summary>
internal interface IAppRestartNotificationTransport
{
  /// <summary>Sends restart request notification to subscribers</summary>
  void SendRestartRequestNotification();

  /// <summary>
  /// Gets <see cref="T:Microsoft.Extensions.Primitives.IChangeToken" /> to monitor restart request notifications
  /// </summary>
  CancellationToken RestartRequested { get; }
}
