// Decompiled with JetBrains decompiler
// Type: PX.PushNotifications.RowDictionaryConverter`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.PushNotifications;

[PXInternalUseOnly]
public abstract class RowDictionaryConverter<T> : JsonConverter
{
  protected readonly bool _serializeAlias;

  protected RowDictionaryConverter(bool serializeAlias) => this._serializeAlias = serializeAlias;

  public virtual void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
  {
    if (!(value is Dictionary<KeyWithAlias, object> source))
      return;
    Dictionary<string, T> dictionary = this._serializeAlias ? source.Where<KeyValuePair<KeyWithAlias, object>>((Func<KeyValuePair<KeyWithAlias, object>, bool>) (record => !KeyWithAlias.IsNoteIdFieldKey(record.Key) && !KeyWithAlias.IsEntityTypeFieldKey(record.Key))).ToDictionary<KeyValuePair<KeyWithAlias, object>, string, T>((Func<KeyValuePair<KeyWithAlias, object>, string>) (c => c.Key.Alias), (Func<KeyValuePair<KeyWithAlias, object>, T>) (c => (T) c.Value)) : source.ToDictionary<KeyValuePair<KeyWithAlias, object>, string, T>((Func<KeyValuePair<KeyWithAlias, object>, string>) (c => c.Key.FieldName), (Func<KeyValuePair<KeyWithAlias, object>, T>) (c => (T) c.Value));
    serializer.Serialize(writer, (object) dictionary);
  }

  public virtual object ReadJson(
    JsonReader reader,
    Type objectType,
    object existingValue,
    JsonSerializer serializer)
  {
    Dictionary<string, T> source = serializer.Deserialize<Dictionary<string, T>>(reader);
    return source == null ? (object) null : (object) source.ToDictionary<KeyValuePair<string, T>, KeyWithAlias, object>((Func<KeyValuePair<string, T>, KeyWithAlias>) (c => new KeyWithAlias(c.Key)), (Func<KeyValuePair<string, T>, object>) (c => (object) c.Value));
  }

  public virtual bool CanConvert(Type objectType)
  {
    return objectType == typeof (Dictionary<KeyWithAlias, object>);
  }
}
