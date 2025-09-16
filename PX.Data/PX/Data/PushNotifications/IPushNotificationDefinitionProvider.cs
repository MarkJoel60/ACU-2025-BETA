// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.IPushNotificationDefinitionProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.PushNotifications.Sources;

#nullable disable
namespace PX.Data.PushNotifications;

[PXInternalUseOnly]
public interface IPushNotificationDefinitionProvider
{
  bool ContainsTable(string tableName, out string[] sourceNames);

  bool ContainsAttributeTable(string tableName);

  bool ContainsField(string tableName, string fieldName, out string[] sourceNames);

  bool ContainsHook(string hookName);

  IInCodeNotificationDefinition GetIncodeDescriptionByName(string name);

  PXGraph GetSelectGraphByDescriptionName(string name);
}
