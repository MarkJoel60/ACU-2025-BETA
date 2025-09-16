// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXUpdateEvent
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Diagnostics;
using System.Text;

#nullable disable
namespace PX.Data.Update;

public class PXUpdateEvent
{
  public EventLogEntryType Level { get; private set; }

  public string Message { get; private set; }

  public string Script { get; private set; }

  public Exception Error { get; private set; }

  public PXUpdateEvent(EventLogEntryType level, string message)
  {
    this.Level = level;
    this.Message = message;
  }

  public PXUpdateEvent(EventLogEntryType level, Exception error)
  {
    this.Level = level;
    this.Error = error;
  }

  public PXUpdateEvent(EventLogEntryType level, string message, Exception error)
  {
    this.Level = level;
    this.Message = message;
    this.Error = error;
  }

  public PXUpdateEvent(EventLogEntryType level, string message, string script)
    : this(level, message)
  {
    this.Script = script;
  }

  public PXUpdateEvent(EventLogEntryType level, Exception error, string script)
    : this(level, error)
  {
    this.Script = script;
  }

  public PXUpdateEvent(EventLogEntryType level, string message, Exception error, string script)
    : this(level, message, script)
  {
    this.Error = error;
  }

  public string GetMessage()
  {
    return (this.Message ?? string.Empty) + (this.Error != null ? this.Error.Message : string.Empty);
  }

  public string GetStack() => this.Error == null ? (string) null : this.Error.StackTrace;

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine(this.Level.ToString() + ":");
    if (this.Message != null)
      stringBuilder.AppendLine(this.Message);
    if (this.Error != null)
    {
      stringBuilder.AppendLine(this.Error.Message);
      stringBuilder.AppendLine(this.Error.StackTrace);
    }
    if (this.Message != null)
      stringBuilder.AppendLine(this.Message);
    return stringBuilder.ToString();
  }
}
