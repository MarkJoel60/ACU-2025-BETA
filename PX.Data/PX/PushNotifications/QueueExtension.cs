// Decompiled with JetBrains decompiler
// Type: PX.PushNotifications.QueueExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Messaging;

#nullable disable
namespace PX.PushNotifications;

internal static class QueueExtension
{
  public static IEnumerable<T> SelectQueueEvents<T>(
    this IEnumerable<Message> enumerable,
    Func<Message, Stream> getter,
    JsonSerializer serializer)
    where T : class
  {
    return QueueExtension.SelectQueueEventsImpl<T>(enumerable, serializer).Where<T>((Func<T, bool>) (q => (object) q != null));
  }

  private static IEnumerable<T> SelectQueueEventsImpl<T>(
    IEnumerable<Message> enumerable,
    JsonSerializer serializer)
    where T : class
  {
    List<Message> currentMessagesList = new List<Message>();
    foreach (Message message in enumerable)
    {
      message.Formatter = (IMessageFormatter) new BinaryMessageFormatter();
      switch (message.Label)
      {
        case "SPLIT":
          currentMessagesList.Add(message);
          break;
        case "FINALSPLIT":
          if (currentMessagesList.Count > 0)
          {
            currentMessagesList.Add(message);
            yield return QueueExtension.Deserialize<T>(serializer, currentMessagesList);
          }
          currentMessagesList = new List<Message>();
          break;
        default:
          if (currentMessagesList.Count > 0)
          {
            yield return QueueExtension.Deserialize<T>(serializer, currentMessagesList);
            currentMessagesList = new List<Message>();
          }
          currentMessagesList.Add(message);
          break;
      }
    }
    if (currentMessagesList.Count > 0)
      yield return QueueExtension.Deserialize<T>(serializer, currentMessagesList);
  }

  private static T Deserialize<T>(JsonSerializer serializer, List<Message> currentMessagesList) where T : class
  {
    Stream stream;
    switch (currentMessagesList.Count)
    {
      case 0:
        return default (T);
      case 1:
        stream = currentMessagesList[0].BodyStream;
        break;
      default:
        stream = (Stream) new CompositeMessageStream(currentMessagesList);
        break;
    }
    using (JsonTextReader jsonTextReader = new JsonTextReader((TextReader) new StreamReader(stream)))
      return serializer.Deserialize((JsonReader) jsonTextReader) as T;
  }
}
