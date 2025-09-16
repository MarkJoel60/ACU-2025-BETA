// Decompiled with JetBrains decompiler
// Type: PX.PushNotifications.DateTimeWithTimeZoneConverter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using PX.Common;
using System;

#nullable disable
namespace PX.PushNotifications;

public class DateTimeWithTimeZoneConverter : JsonConverter
{
  private PXTimeZoneInfo _timeZoneInfo;

  public DateTimeWithTimeZoneConverter(PXTimeZoneInfo timeZoneInfo)
  {
    this._timeZoneInfo = timeZoneInfo;
  }

  public virtual void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
  {
    DateTime dateTime = (DateTime) value;
    serializer.Serialize(writer, (object) new DateTimeOffset(dateTime, this._timeZoneInfo.BaseUtcOffset));
  }

  public virtual object ReadJson(
    JsonReader reader,
    Type objectType,
    object existingValue,
    JsonSerializer serializer)
  {
    throw new NotSupportedException();
  }

  public virtual bool CanRead => false;

  public virtual bool CanConvert(Type objectType) => objectType == typeof (DateTime);
}
