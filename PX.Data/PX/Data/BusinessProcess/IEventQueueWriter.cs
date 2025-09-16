// Decompiled with JetBrains decompiler
// Type: PX.Data.BusinessProcess.IEventQueueWriter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;

#nullable disable
namespace PX.Data.BusinessProcess;

public interface IEventQueueWriter
{
  void RaiseEvent(Event @event);

  JsonSerializer Serializer { get; }

  (long current, long max) GetQueueSize();
}
