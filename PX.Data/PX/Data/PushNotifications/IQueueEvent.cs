// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.IQueueEvent
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.PushNotifications;

/// <exclude />
public interface IQueueEvent
{
  [JsonConverter(typeof (StringEnumConverter))]
  QueueEventType type { get; }

  Dictionary<string, object> AdditionalInfo { get; }

  List<Guid> GiSetToSkip { get; }

  List<string> EntityScreenSetToSkip { get; }
}
