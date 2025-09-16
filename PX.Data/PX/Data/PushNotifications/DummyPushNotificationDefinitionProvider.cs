// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.DummyPushNotificationDefinitionProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.PushNotifications.Sources;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.PushNotifications;

/// <exclude />
public class DummyPushNotificationDefinitionProvider : IPushNotificationDefinitionProvider
{
  public bool ContainsTable(string tableName, out string[] sourceNames)
  {
    sourceNames = Array.Empty<string>();
    return false;
  }

  public bool ContainsAttributeTable(string tableName) => false;

  public bool ContainsField(string tableName, string fieldName, out string[] sourceNames)
  {
    sourceNames = Array.Empty<string>();
    return false;
  }

  public bool ContainsHook(string hookName) => false;

  public IInCodeNotificationDefinition GetIncodeDescriptionByName(string name)
  {
    return (IInCodeNotificationDefinition) null;
  }

  public PXGraph GetSelectGraphByDescriptionName(string name) => (PXGraph) null;

  public bool CheckObservedFieldChanged() => false;

  public IEnumerable<string> GetSourcesByTable(string tableName) => Enumerable.Empty<string>();
}
