// Decompiled with JetBrains decompiler
// Type: PX.Data.BusinessProcess.IEventQueueReader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Threading;

#nullable disable
namespace PX.Data.BusinessProcess;

public interface IEventQueueReader
{
  bool TryReadNextEvent(
    CancellationToken cancellationToken,
    out Event @event,
    out string finalMessageId);

  void CompleteEventProcessing(Event @event, string finalMessageId);

  bool ReRaiseEvent(Event @event, string finalMessageId);

  void ReInitQueue();
}
