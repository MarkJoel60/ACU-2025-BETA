// Decompiled with JetBrains decompiler
// Type: PX.Logging.Enrichers.SourceLocationEnricher
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace PX.Logging.Enrichers;

internal class SourceLocationEnricher : ILogEventEnricher
{
  private const string PropertyName = "SourceLocation";
  private readonly string _caller;
  private readonly string _path;
  private readonly string _file;
  private readonly int _line;

  public SourceLocationEnricher(string caller, string path, int line)
  {
    if (caller != null)
      this._caller = caller + "()";
    this._file = Path.GetFileName(path);
    if (!string.IsNullOrEmpty(this._file))
    {
      string directoryName = Path.GetDirectoryName(path);
      this._path = string.IsNullOrWhiteSpace(directoryName) ? this._file : Path.Combine(Path.GetFileName(directoryName), this._file);
    }
    this._line = line;
  }

  public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
  {
    if (logEvent == null)
      throw new ArgumentNullException(nameof (logEvent));
    logEvent.AddPropertyIfAbsent(new LogEventProperty("SourceLocation", (LogEventPropertyValue) new SourceLocationEnricher.SourceLocationValue(this._path, this._file, this._line, this._caller)));
  }

  internal static string GetLocation(LogEvent logEvent)
  {
    LogEventPropertyValue eventPropertyValue;
    if (!logEvent.Properties.TryGetValue("SourceLocation", out eventPropertyValue))
      return (string) null;
    if (eventPropertyValue is SourceLocationEnricher.SourceLocationValue sourceLocationValue)
      return ((LogEventPropertyValue) sourceLocationValue).ToString(":", (IFormatProvider) null);
    return eventPropertyValue?.ToString();
  }

  private class SourceLocationValue : StructureValue
  {
    private readonly string _file;
    private readonly int _line;
    private readonly string _caller;

    public SourceLocationValue(string path, string file, int line, string caller)
      : base((IEnumerable<LogEventProperty>) new LogEventProperty[3]
      {
        new LogEventProperty(nameof (path), (LogEventPropertyValue) new ScalarValue((object) path)),
        new LogEventProperty(nameof (line), (LogEventPropertyValue) new ScalarValue((object) line)),
        new LogEventProperty(nameof (caller), (LogEventPropertyValue) new ScalarValue((object) caller))
      }, (string) null)
    {
      this._file = file;
      this._line = line;
      this._caller = caller;
    }

    public virtual void Render(TextWriter output, string format = null, IFormatProvider formatProvider = null)
    {
      if (string.IsNullOrEmpty(format) || format == "j")
        base.Render(output, format, formatProvider);
      else if (format.Length > 2)
      {
        SelfLog.WriteLine("Unexpected format {0}", (object) format, (object) null, (object) null);
        base.Render(output, format, formatProvider);
      }
      else
      {
        output.Write(this._file);
        output.Write(format[0]);
        output.Write(this._line.ToString(formatProvider));
        output.Write(format[0]);
        output.Write(this._caller);
        if (format.Length != 2)
          return;
        output.Write(format[1]);
      }
    }
  }
}
