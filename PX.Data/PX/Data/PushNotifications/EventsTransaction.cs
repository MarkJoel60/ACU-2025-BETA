// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.EventsTransaction
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.PushNotifications;

public class EventsTransaction
{
  public Guid Id { get; }

  public int? CompanyId { get; }

  public IQueueEvent[] Messages { get; }

  public long TimeStamp { get; }

  public string ConnectionString { get; }

  public Dictionary<string, object> AdditionalInfo { get; }

  public HashSet<Guid> GiToSkip { get; }

  public HashSet<string> EntityScreenToSkip { get; }

  public EventsTransaction(
    Guid id,
    int? companyId,
    IQueueEvent[] messages,
    long timeStamp,
    string connectionString,
    Dictionary<string, object> additionalInfo)
  {
    this.Id = id;
    this.CompanyId = companyId;
    this.Messages = messages;
    this.TimeStamp = timeStamp;
    this.ConnectionString = connectionString;
    this.AdditionalInfo = additionalInfo ?? new Dictionary<string, object>();
    foreach (IQueueEvent message in messages)
    {
      if (message.GiSetToSkip != null)
      {
        if (this.GiToSkip == null)
          this.GiToSkip = new HashSet<Guid>((IEnumerable<Guid>) message.GiSetToSkip);
        else
          EnumerableExtensions.AddRange<Guid>((ISet<Guid>) this.GiToSkip, (IEnumerable<Guid>) message.GiSetToSkip);
      }
      if (message.EntityScreenSetToSkip != null)
      {
        if (this.EntityScreenToSkip == null)
          this.EntityScreenToSkip = new HashSet<string>((IEnumerable<string>) message.EntityScreenSetToSkip);
        else
          EnumerableExtensions.AddRange<string>((ISet<string>) this.EntityScreenToSkip, (IEnumerable<string>) message.EntityScreenSetToSkip);
      }
    }
  }
}
