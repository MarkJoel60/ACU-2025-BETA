// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.Statistics.PrimaryQueueInMessageMetadata
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data.PushNotifications.Statistics;

[PXInternalUseOnly]
public class PrimaryQueueInMessageMetadata
{
  public string ScreenId { get; set; }

  public string[] Sources { get; set; }

  public string TableName { get; set; }

  public string FieldName { get; set; }

  public Guid DbTransactionId { get; set; }

  public PXDBOperation Operation { get; set; }

  public string GetOperationDescription()
  {
    switch (this.Operation)
    {
      case PXDBOperation.Insert:
        return "Insert";
      case PXDBOperation.Delete:
        return "Delete";
      default:
        return this.Operation.ToString();
    }
  }
}
