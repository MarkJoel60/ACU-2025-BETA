// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.CommitTransactionMessage
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.PushNotifications;

public class CommitTransactionMessage : IQueueEvent
{
  public CommitTransactionMessage()
  {
  }

  public CommitTransactionMessage(Guid transactionId, System.DateTime date, string connectionString)
  {
    this.TransactionId = transactionId;
    this.TimeStamp = date.Ticks;
    this.ConnectionString = connectionString;
    this.AdditionalInfo = new Dictionary<string, object>();
    this.GiSetToSkip = SuppressPushNotificationsScope.GetGiSetToSkip();
    this.EntityScreenSetToSkip = SuppressPushNotificationsScope.GetEntityScreenSetToSkip();
  }

  public Guid TransactionId { get; set; }

  public long TimeStamp { get; set; }

  public string ConnectionString { get; set; }

  public QueueEventType type => QueueEventType.Commit;

  public Dictionary<string, object> AdditionalInfo { get; set; }

  public List<Guid> GiSetToSkip { get; set; } = new List<Guid>();

  public List<string> EntityScreenSetToSkip { get; set; } = new List<string>();
}
