// Decompiled with JetBrains decompiler
// Type: PX.Data.BusinessProcess.ValueWithInternalTypedConverter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PX.PushNotifications;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.BusinessProcess;

/// <exclude />
public class ValueWithInternalTypedConverter : JsonConverter
{
  public virtual void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
  {
    ValueWithInternal valueWithInternal = (ValueWithInternal) value;
    writer.WriteStartObject();
    if (valueWithInternal.ExternalValue != null || serializer.NullValueHandling == null)
    {
      writer.WritePropertyName("ExternalValue");
      serializer.Serialize(writer, valueWithInternal.ExternalValue);
    }
    if (valueWithInternal.InternalValue != null || serializer.NullValueHandling == null)
    {
      writer.WritePropertyName("InternalValue");
      if (valueWithInternal.InternalValue == null)
      {
        writer.WriteNull();
      }
      else
      {
        writer.WriteStartObject();
        writer.WritePropertyName(System.Type.GetTypeCode(valueWithInternal.InternalValue.GetType()).ToString("G"));
        serializer.Serialize(writer, valueWithInternal.InternalValue);
        writer.WriteEndObject();
      }
    }
    writer.WriteEndObject();
  }

  public virtual object ReadJson(
    JsonReader reader,
    System.Type objectType,
    object existingValue,
    JsonSerializer serializer)
  {
    object externalValue = (object) null;
    object internalValue = (object) null;
    if (!reader.Read())
      return (object) null;
    while (reader.TokenType == 4)
    {
      if (reader.Value.ToString() == "ExternalValue")
      {
        reader.Read();
        externalValue = serializer.Deserialize(reader, typeof (object));
        reader.Read();
      }
      if (reader.Value.ToString() == "InternalValue")
      {
        reader.Read();
        internalValue = this.ParseInternalValue(reader, serializer);
        reader.Read();
      }
    }
    return (object) new ValueWithInternal(externalValue, internalValue);
  }

  private object ParseInternalValue(JsonReader reader, JsonSerializer serializer)
  {
    if (reader.TokenType != 1)
      return serializer.Deserialize(reader, typeof (object));
    JProperty jproperty = serializer.Deserialize<JObject>(reader).Properties().FirstOrDefault<JProperty>();
    if (jproperty == null)
      return (object) null;
    TypeCode result1;
    if (!Enum.TryParse<TypeCode>(jproperty.Name, true, out result1))
      result1 = TypeCode.Object;
    object internalValue = jproperty.Value.ToObject<object>();
    if (System.Type.GetTypeCode(internalValue.GetType()) == result1 || !(internalValue is IConvertible))
      return internalValue;
    if (result1 != TypeCode.Object)
      return Convert.ChangeType(internalValue, result1);
    Guid result2;
    return Guid.TryParse(Extensions.Value<string>((IEnumerable<JToken>) jproperty.Value), out result2) ? (object) result2 : internalValue;
  }

  public virtual bool CanConvert(System.Type objectType)
  {
    return objectType == typeof (ValueWithInternal);
  }
}
