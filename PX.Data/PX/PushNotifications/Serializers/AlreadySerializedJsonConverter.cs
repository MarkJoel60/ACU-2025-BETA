// Decompiled with JetBrains decompiler
// Type: PX.PushNotifications.Serializers.AlreadySerializedJsonConverter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using PX.Common;
using System;

#nullable disable
namespace PX.PushNotifications.Serializers;

[PXInternalUseOnly]
public class AlreadySerializedJsonConverter : JsonConverter
{
  public virtual bool CanRead => false;

  public virtual bool CanWrite => true;

  public virtual void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
  {
    string json = ((AlreadySerializedJson) value)?.Json;
    if (string.IsNullOrWhiteSpace(json))
      writer.WriteNull();
    else
      writer.WriteRawValue(json);
  }

  public virtual object ReadJson(
    JsonReader reader,
    Type objectType,
    object existingValue,
    JsonSerializer serializer)
  {
    throw new NotImplementedException();
  }

  public virtual bool CanConvert(Type objectType) => objectType == typeof (AlreadySerializedJson);
}
