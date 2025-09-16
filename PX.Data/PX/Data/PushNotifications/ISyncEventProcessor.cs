// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.ISyncEventProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace PX.Data.PushNotifications;

public interface ISyncEventProcessor
{
  void ProcessEvent(PX.Data.BusinessProcess.Event @event, CancellationToken cancellationToken);

  IEnumerable<string> GetActionsWithSynchronousEvents(string screenId);
}
