// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.PushNotificationsProviderHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.PushNotifications.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.PushNotifications;

[PXInternalUseOnly]
public static class PushNotificationsProviderHelper
{
  internal static bool ShouldTrigger(
    this IPushNotificationDefinitionProvider provider,
    string tableName,
    QueueEvent[] events,
    out List<PrimaryQueueInMessageMetadata> sources)
  {
    sources = new List<PrimaryQueueInMessageMetadata>();
    string[] sourceNames1;
    if (!provider.ContainsTable(tableName, out sourceNames1))
      return false;
    foreach (QueueEvent queueEvent in events)
    {
      if (queueEvent.Operation == PXDBOperation.Insert || queueEvent.Operation == PXDBOperation.Delete)
      {
        sources.Add(new PrimaryQueueInMessageMetadata()
        {
          ScreenId = PXContext.GetScreenID(),
          TableName = tableName,
          Sources = sourceNames1,
          Operation = queueEvent.Operation
        });
        return true;
      }
      if ((queueEvent.TableName.StartsWith(tableName, StringComparison.OrdinalIgnoreCase) && queueEvent.TableName.EndsWith("KvExt", StringComparison.OrdinalIgnoreCase) || provider.ContainsAttributeTable(tableName)) && ((IEnumerable<QueueEvent.Field>) queueEvent.Fields).FirstOrDefault<QueueEvent.Field>((Func<QueueEvent.Field, bool>) (ef => PushNotificationsProviderHelper.KvExtValueColumns.Contains<string>(ef.FieldName) && ef.IsChanged)) != null)
        sources.AddRange(((IEnumerable<QueueEvent.Field>) queueEvent.Fields).Where<QueueEvent.Field>((Func<QueueEvent.Field, bool>) (eventField => eventField.FieldName == "FieldName" || eventField.FieldName == "AttributeID")).Select<QueueEvent.Field, PrimaryQueueInMessageMetadata>((Func<QueueEvent.Field, PrimaryQueueInMessageMetadata>) (eventField =>
        {
          PrimaryQueueInMessageMetadata inMessageMetadata = new PrimaryQueueInMessageMetadata()
          {
            FieldName = eventField.OldValue.ToString(),
            TableName = tableName,
            ScreenId = PXContext.GetScreenID()
          };
          string[] sourceNames2;
          if (provider.ContainsField(tableName, eventField.OldValue.ToString(), out sourceNames2))
            inMessageMetadata.Sources = sourceNames2;
          return inMessageMetadata;
        })).Where<PrimaryQueueInMessageMetadata>((Func<PrimaryQueueInMessageMetadata, bool>) (c => c.Sources != null)));
      sources.AddRange(((IEnumerable<QueueEvent.Field>) queueEvent.Fields).Where<QueueEvent.Field>((Func<QueueEvent.Field, bool>) (eventField => eventField.IsChanged)).Select<QueueEvent.Field, PrimaryQueueInMessageMetadata>((Func<QueueEvent.Field, PrimaryQueueInMessageMetadata>) (eventField =>
      {
        PrimaryQueueInMessageMetadata inMessageMetadata = new PrimaryQueueInMessageMetadata()
        {
          FieldName = eventField.FieldName,
          TableName = tableName,
          ScreenId = PXContext.GetScreenID()
        };
        string[] sourceNames3;
        if (provider.ContainsField(tableName, eventField.FieldName, out sourceNames3))
          inMessageMetadata.Sources = sourceNames3;
        return inMessageMetadata;
      })).Where<PrimaryQueueInMessageMetadata>((Func<PrimaryQueueInMessageMetadata, bool>) (c => c.Sources != null)));
    }
    return sources.Count > 0;
  }

  public static IEnumerable<string> KvExtValueColumns
  {
    get
    {
      return (IEnumerable<string>) new string[5]
      {
        "ValueNumeric",
        "ValueDate",
        "ValueString",
        "ValueText",
        "Value"
      };
    }
  }
}
