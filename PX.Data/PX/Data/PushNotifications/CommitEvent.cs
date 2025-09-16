// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.CommitEvent
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.PushNotifications;

/// <exclude />
public class CommitEvent : IQueueEvent
{
  [JsonConstructor]
  [Obsolete("Serialization Only", true)]
  public CommitEvent()
  {
  }

  public CommitEvent(string timeElapsed, System.DateTime commited, string connectionString)
  {
    this.TimeElapsed = timeElapsed;
    this.ConnectionString = connectionString;
    this.TimeStamp = commited.Ticks;
    this.GiSetToSkip = SuppressPushNotificationsScope.GetGiSetToSkip();
    this.EntityScreenSetToSkip = SuppressPushNotificationsScope.GetEntityScreenSetToSkip();
  }

  public QueueEventType type => QueueEventType.Commit;

  public string TimeElapsed { get; set; }

  public string ConnectionString { get; set; }

  public long TimeStamp { get; set; }

  public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();

  public List<Guid> GiSetToSkip { get; set; } = new List<Guid>();

  public List<string> EntityScreenSetToSkip { get; set; } = new List<string>();
}
