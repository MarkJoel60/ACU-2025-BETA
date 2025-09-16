// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.BCQueueMessage
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.PushNotifications;

public abstract class BCQueueMessage
{
  public Guid CorrealtionId { get; set; }

  public string Type { get; set; }

  public string Company { get; set; }

  public string ConnectionString { get; set; }

  public System.DateTime TimeStamp { get; set; }

  public Dictionary<string, object> AdditionalInfo { get; set; }

  protected BCQueueMessage(
    Guid id,
    string type,
    string company,
    System.DateTime timeStamp,
    Dictionary<string, object> additionalInfo)
  {
    this.CorrealtionId = id;
    this.Type = type;
    this.Company = company;
    this.TimeStamp = timeStamp;
    this.AdditionalInfo = additionalInfo ?? new Dictionary<string, object>();
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine("Company = " + this.Company);
    stringBuilder.AppendLine("Type = " + this.Type);
    stringBuilder.AppendLine("TimeStamp = " + this.TimeStamp.ToString());
    return stringBuilder.ToString().TrimEnd(Environment.NewLine.ToCharArray());
  }
}
