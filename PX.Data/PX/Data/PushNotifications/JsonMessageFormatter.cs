// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.JsonMessageFormatter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using System;
using System.IO;
using System.Messaging;
using System.Text;

#nullable disable
namespace PX.Data.PushNotifications;

/// <exclude />
public class JsonMessageFormatter : IMessageFormatter, ICloneable
{
  private readonly JsonSerializer _serializer;

  public JsonMessageFormatter(JsonSerializer serializer) => this._serializer = serializer;

  public object Clone() => (object) new JsonMessageFormatter(this._serializer);

  public bool CanRead(Message message)
  {
    if (message == null)
      throw new ArgumentNullException(nameof (message));
    return true;
  }

  public object Read(Message message)
  {
    if (message == null)
      throw new ArgumentNullException(nameof (message));
    using (JsonTextReader jsonTextReader = new JsonTextReader((TextReader) new StreamReader(message.BodyStream)))
      return this._serializer.Deserialize((JsonReader) jsonTextReader);
  }

  public void Write(Message message, object obj)
  {
    if (message == null)
      throw new ArgumentNullException(nameof (message));
    if (obj == null)
      throw new ArgumentNullException(nameof (obj));
    message.BodyStream = (Stream) new MemoryStream();
    using (JsonTextWriter jsonTextWriter = new JsonTextWriter((TextWriter) new StreamWriter(message.BodyStream, (Encoding) new UTF8Encoding(false, true), 1024 /*0x0400*/, true)))
      this._serializer.Serialize((JsonWriter) jsonTextWriter, obj);
    message.BodyType = 0;
  }
}
