// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.IdentityEvent
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
[Serializable]
public class IdentityEvent : IQueueEvent
{
  public string FieldName { get; set; }

  public string TableName { get; set; }

  [Obsolete("Backward Compatibility. Use generic value instead")]
  public int Value { get; set; }

  public object GenericValue { get; set; }

  [JsonConstructor]
  [Obsolete("Serialization Only", true)]
  public IdentityEvent()
  {
  }

  public IdentityEvent(string fieldName, string tableName, object genericValue)
  {
    this.FieldName = fieldName;
    this.TableName = tableName;
    this.GenericValue = genericValue;
  }

  [JsonConverter(typeof (StringEnumConverter))]
  public QueueEventType type => QueueEventType.Identity;

  public string CompanyName { get; set; }

  public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();

  public List<Guid> GiSetToSkip { get; } = new List<Guid>();

  public List<string> EntityScreenSetToSkip { get; } = new List<string>();
}
