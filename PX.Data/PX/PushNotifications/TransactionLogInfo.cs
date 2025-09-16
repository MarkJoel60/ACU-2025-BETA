// Decompiled with JetBrains decompiler
// Type: PX.PushNotifications.TransactionLogInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace PX.PushNotifications;

public class TransactionLogInfo
{
  internal const string TooManyRowsProperty = "TooManyRows";

  public IEnumerable<LogEvent> Log { get; private set; }

  public DateTime TimeStamp { get; private set; }

  public TimeSpan ProcessTime { get; private set; }

  public bool HasError { get; private set; }

  public List<(string sourceName, TimeSpan elapsed)> ProcessedSources { get; private set; }

  public TransactionLogInfo(
    LinkedList<LogEvent> log,
    TimeSpan processTime,
    DateTime timeStamp,
    List<(string sourceName, TimeSpan elapsed)> processedSources = null)
  {
    this.Log = (IEnumerable<LogEvent>) log;
    this.ProcessTime = processTime;
    this.TimeStamp = timeStamp;
    this.ProcessedSources = processedSources ?? new List<(string, TimeSpan)>(0);
    this.HasError = TransactionLogInfo.LogHasError(this.Log);
  }

  internal static bool LogHasError(IEnumerable<LogEvent> log)
  {
    return log.Any<LogEvent>((Func<LogEvent, bool>) (e => e.Properties.TryGetValue("TooManyRows", out LogEventPropertyValue _)));
  }

  internal string GetFormattedLog()
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (StreamWriter streamWriter = new StreamWriter((Stream) memoryStream))
      {
        foreach (LogEvent logEvent in this.Log ?? Enumerable.Empty<LogEvent>())
        {
          streamWriter.Write(logEvent.Level >= 4 ? $"-->  {logEvent.Level.ToString()}: " : "     ");
          logEvent.RenderMessage((TextWriter) streamWriter, (IFormatProvider) null);
          streamWriter.WriteLine();
        }
        streamWriter.Flush();
        memoryStream.Position = 0L;
        using (StreamReader streamReader = new StreamReader((Stream) memoryStream))
          return streamReader.ReadToEnd();
      }
    }
  }
}
