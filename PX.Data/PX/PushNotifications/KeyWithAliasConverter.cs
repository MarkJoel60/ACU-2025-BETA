// Decompiled with JetBrains decompiler
// Type: PX.PushNotifications.KeyWithAliasConverter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using System;

#nullable disable
namespace PX.PushNotifications;

/// <exclude />
public class KeyWithAliasConverter : JsonConverter
{
  public virtual void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
  {
    serializer.Serialize(writer, (object) ((KeyWithAlias) value)?.Alias);
  }

  public virtual object ReadJson(
    JsonReader reader,
    Type objectType,
    object existingValue,
    JsonSerializer serializer)
  {
    return (object) new KeyWithAlias(reader.ReadAsString());
  }

  public virtual bool CanRead => true;

  public virtual bool CanConvert(Type objectType) => objectType == typeof (KeyWithAlias);
}
