// Decompiled with JetBrains decompiler
// Type: PX.Data.PXTrace
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PX.Common;
using PX.Data.DacDescriptorGeneration;
using PX.Logging;
using PX.Logging.Enrichers;
using PX.Logging.TraceProviders;
using PX.SM;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;
using Serilog.Parsing;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

#nullable disable
namespace PX.Data;

public static class PXTrace
{
  private const string sessionKey = "PXDataPXTrace";
  private static PXTraceProviderCollection _providers = new PXTraceProviderCollection((IConfiguration) null);
  private static ILogger _logger = (ILogger) new SilentLoggerWithBinders();
  private static readonly AsyncLocal<bool> RecursionGuard = new AsyncLocal<bool>()
  {
    Value = false
  };
  private static readonly (System.Type, string, string, int?)[] ExceptionTypeNotLoggedByTrace = new (System.Type, string, string, int?)[17]
  {
    (typeof (ThreadAbortException), "Thread was being aborted.", (string) null, new int?()),
    (typeof (NullReferenceException), "Object reference not set to an instance of an object.", (string) null, new int?()),
    (typeof (TargetInvocationException), "Exception has been thrown by the target of an invocation.", (string) null, new int?()),
    (typeof (HttpException), "Request is not available in this context", (string) null, new int?()),
    (typeof (OperationCanceledException), "The operation was canceled.", (string) null, new int?()),
    (typeof (TaskCanceledException), "A task was canceled.", (string) null, new int?()),
    (typeof (InvalidCastException), "Specified cast is not valid.", (string) null, new int?()),
    (typeof (ReflectionTypeLoadException), "Unable to load one or more of the requested types. Retrieve the LoaderExceptions property for more information.", (string) null, new int?()),
    (typeof (PXViewDoesNotExistException), "Error: The view ComplianceDocuments doesn't exist.", (string) null, new int?()),
    (typeof (Exception), "The session has expired. Please reload the page.", (string) null, new int?()),
    (typeof (FormatException), "Input string was not in a correct format.", (string) null, new int?()),
    (typeof (PXInvalidOperationException), "The opportunity stages slot was not initialized.", (string) null, new int?()),
    (typeof (PXSetPropertyException), "The specified inventory ID or alternate ID cannot be found in the system.", (string) null, new int?()),
    (typeof (HttpUnhandledException), "Exception of type 'System.Web.HttpUnhandledException' was thrown.", (string) null, new int?()),
    (typeof (ThreadInterruptedException), "Thread was interrupted from a waiting state.", (string) null, new int?()),
    (typeof (PXNotSupportedException), "Not supported result operator CastResultOperator.", (string) null, new int?()),
    (typeof (ObjectDisposedException), "Instances cannot be resolved and nested lifetimes cannot be created from this LifetimeScope as it (or one of its parent scopes) has already been disposed.", (string) null, new int?())
  };

  internal static LoggerBuilder AddPXTrace(
    this LoggerBuilder loggerBuilder,
    IConfiguration configuration)
  {
    return loggerBuilder.AddInnerLoggerBuiltHandler((System.Action<ILogger>) (logger => PXTrace.UseSerilog(logger.ForContext("Via", (object) nameof (PXTrace), false)))).AddBackEndProvider((Func<SubLogger>) (() => PXTrace.CreateBackEnd(configuration)));
  }

  /// <param name="logger">If you pass this, you take care of its disposal.</param>
  internal static void UseSerilog(ILogger logger)
  {
    PXTrace._logger = logger ?? throw new ArgumentNullException(nameof (logger));
  }

  internal static SubLogger CreateBackEnd(IConfiguration configurationSection)
  {
    using (SelfLog.MarkLoggerStartup())
    {
      try
      {
        PXTrace._providers = new PXTraceProviderCollection(configurationSection);
        IInformingLevelSwitch levelSwitch = PXPerformanceMonitor.InitLogging(configurationSection);
        LogEventLevel logEventLevel = PXTrace.GetLogEventLevel(PXTrace._providers.MinimumLevel);
        return new SubLogger(nameof (PXTrace), levelSwitch.Threshold(logEventLevel), (System.Action<LoggerSinkConfiguration>) (sinkConfiguration => sinkConfiguration.Sink((ILogEventSink) new PXTrace.Sink(), (LogEventLevel) 0, (LoggingLevelSwitch) null)), (IReadOnlyDictionary<string, object>) new Dictionary<string, object>()
        {
          {
            "PXPerformanceMonitor",
            (object) levelSwitch.MinimumLevel
          },
          {
            "PXTraceProviderCollection",
            (object) logEventLevel
          }
        });
      }
      catch (Exception ex) when (SelfLog.WriteAndRethrow(ex, "Exception while initializing PXTrace"))
      {
        throw;
      }
      catch (Exception ex) when (LogException(ex, "Exception while initializing PXTrace"))
      {
        throw;
      }
    }

    static bool LogException(Exception e, string message)
    {
      PXTrace._logger.Error(e, message);
      return false;
    }
  }

  /// <summary>
  /// If you set this, you take care of its disposal.
  /// The only place it actually IS set is
  /// <see cref="M:PX.Logging.LoggingExtensions.UseLogging(Microsoft.Extensions.Hosting.IHostBuilder)">UseLogging</see>.
  /// </summary>
  public static ILogger Logger { get; internal set; } = Serilog.Core.Logger.None;

  public static ILogger WithStack() => PXTrace.Logger.WithStack();

  public static ILogger WithSourceLocation([CallerMemberName] string caller = null, [CallerFilePath] string path = null, [CallerLineNumber] int line = 0)
  {
    return PXTrace.Logger.ForContext((ILogEventEnricher) new SourceLocationEnricher(caller, path, line));
  }

  /// <summary>Write event with EventType = TraceEventType.Error</summary>
  public static void WriteError(string message)
  {
    PXTrace.Write(TraceEventType.Error, (LogEventLevel) 4, (Exception) null, message);
  }

  /// <summary>Write event with EventType = TraceEventType.Error</summary>
  public static void WriteError(string format, params object[] args)
  {
    PXTrace.Write(TraceEventType.Error, (LogEventLevel) 4, (Exception) null, format, args);
  }

  internal static void WriteError(Exception exception, string format, params object[] args)
  {
    PXTrace.Write(TraceEventType.Error, (LogEventLevel) 4, exception, format, args);
  }

  /// <summary>Write event with EventType = TraceEventType.Error</summary>
  public static void WriteError(Exception e)
  {
    PXTrace.Write(TraceEventType.Error, (LogEventLevel) 4, e);
  }

  /// <summary>Write event with EventType = TraceEventType.Warning</summary>
  public static void WriteWarning(string message)
  {
    PXTrace.Write(TraceEventType.Warning, (LogEventLevel) 3, (Exception) null, message);
  }

  /// <summary>Write event with EventType = TraceEventType.Warning</summary>
  public static void WriteWarning(string format, params object[] args)
  {
    PXTrace.Write(TraceEventType.Warning, (LogEventLevel) 3, (Exception) null, format, args);
  }

  /// <summary>Write event with EventType = TraceEventType.Warning</summary>
  public static void WriteWarning(Exception e)
  {
    PXTrace.Write(TraceEventType.Warning, (LogEventLevel) 3, e);
  }

  /// <summary>
  /// Write event with EventType = TraceEventType.Information
  /// </summary>
  public static void WriteInformation(string message)
  {
    PXTrace.Write(TraceEventType.Information, (LogEventLevel) 2, (Exception) null, message);
  }

  /// <summary>
  /// Write event with EventType = TraceEventType.Information
  /// </summary>
  public static void WriteInformation(string format, params object[] args)
  {
    PXTrace.Write(TraceEventType.Information, (LogEventLevel) 2, (Exception) null, format, args);
  }

  /// <summary>
  /// Write event with EventType = TraceEventType.Information
  /// </summary>
  public static void WriteInformation(Exception e)
  {
    PXTrace.Write(TraceEventType.Information, (LogEventLevel) 2, e);
  }

  /// <summary>Write event with EventType = TraceEventType.Verbose</summary>
  public static void WriteVerbose(string message)
  {
    PXTrace.Write(TraceEventType.Verbose, (LogEventLevel) 0, (Exception) null, message);
  }

  /// <summary>Write event with EventType = TraceEventType.Verbose</summary>
  public static void WriteVerbose(string format, params object[] args)
  {
    PXTrace.Write(TraceEventType.Verbose, (LogEventLevel) 0, (Exception) null, format, args);
  }

  /// <summary>Write event with EventType = TraceEventType.Verbose</summary>
  public static void WriteVerbose(Exception e)
  {
    PXTrace.Write(TraceEventType.Verbose, (LogEventLevel) 0, e);
  }

  private static void Write(TraceEventType type, LogEventLevel level, Exception e)
  {
    if (e == null || e is PXBaseRedirectException)
      return;
    if (type != TraceEventType.Verbose && e.Message != null && e.Message.StartsWith("Redirect"))
      type = TraceEventType.Information;
    if (e is PXException pxException)
    {
      if (pxException.HasBeenLogged)
        return;
      pxException.HasBeenLogged = true;
    }
    PXTrace.Write(new PXTrace.Event(PXTrace._logger, type, level, e, e is PXOuterException pxOuterException ? pxOuterException.InnerMessages : (string[]) null), e);
  }

  private static void Write(
    TraceEventType type,
    LogEventLevel level,
    Exception exception,
    string message,
    params object[] args)
  {
    PXTrace.Write(new PXTrace.Event(type, level, message, StackTraceEnricher.GetStackTraceIfEnabled(2), args, exception), exception);
  }

  private static void Write(PXTrace.Event item, Exception originalExceptionPassedToTrace)
  {
    try
    {
      if (HttpContext.Current != null && PXContext.Session.IsSessionEnabled)
      {
        PXTrace.Event @event = PXContext.SessionTyped<PXSessionStatePXData>().PXTraceEvent["PXDataPXTrace"];
        if (@event != null && @event.Message == item.Message && @event.StackTrace == item.StackTrace)
          return;
        PXContext.SessionTyped<PXSessionStatePXData>().PXTraceEvent["PXDataPXTrace"] = item;
      }
      PXTrace.WriteImpl(item, originalExceptionPassedToTrace);
      PXTrace._logger.Write(item.LogEvent);
    }
    catch (Exception ex)
    {
      SelfLog.SafeWriteExceptionInCaller(ex, nameof (Write), "C:\\build\\code_repo\\NetTools\\PX.Data\\PXTrace.cs", 412);
    }
  }

  private static void WriteImpl(
    PXTrace.Event item,
    Exception originalExceptionPassedToTraceManually,
    LogEvent original = null)
  {
    if (PXTrace.RecursionGuard.Value)
    {
      SelfLog.WriteLine("Recursion detected at WriteImpl");
    }
    else
    {
      PXTrace.RecursionGuard.Value = true;
      try
      {
        PXTrace._providers.TraceEvent(item);
        try
        {
          if (item.TraceException != null && item.TraceException.IsExcluded(item.TraceException.StackTrace, PXTrace.ExceptionTypeNotLoggedByTrace) || original?.Exception != null && original.Exception.IsExcluded(original.Exception.StackTrace, PXTrace.ExceptionTypeNotLoggedByTrace))
            return;
          string fullName = originalExceptionPassedToTraceManually?.GetType().FullName;
          DacDescriptor? nullable = originalExceptionPassedToTraceManually != null ? originalExceptionPassedToTraceManually.GetAttachedDacDescriptor() : new DacDescriptor?();
          PXPerformanceMonitor.AddTrace(item.Source, PXTrace.GetTraceEventType(item.EventType), item.Message, item.StackTrace, fullName, original != null ? JsonConvert.SerializeObject((object) original) : item.Message, original?.MessageTemplate?.Text, nullable?.ToString());
        }
        catch (Exception ex)
        {
          SelfLog.SafeWriteExceptionInCaller(ex, nameof (WriteImpl), "C:\\build\\code_repo\\NetTools\\PX.Data\\PXTrace.cs", 472);
        }
      }
      finally
      {
        PXTrace.RecursionGuard.Value = false;
      }
    }
  }

  internal static string TryGetAspNetCallbackInformation()
  {
    return PXTrace.FormatAspNetCallbackInformation(PXTrace.TryGetAspNetCallbackInformationImpl());
  }

  private static (string source, string command) TryGetAspNetCallbackInformationImpl()
  {
    try
    {
      HttpRequestBase request = AspNetCallbackEnricher.RequestAccessor();
      return request == null ? ((string) null, (string) null) : AspNetCallbackEnricher.TryGetAspNetCallbackInformation(request);
    }
    catch (Exception ex)
    {
      SelfLog.WriteLine("Exception in PXTrace.{1}: {0}", (object) ex.ToString(), (object) "TryGetAspNetCallbackInformation");
      return ("", (string) null);
    }
  }

  private static string FormatAspNetCallbackInformation(
    (string source, string command) callbackInformation)
  {
    (string source, string command) tuple1 = callbackInformation;
    if (tuple1.command == null)
      return tuple1.source;
    (string source, string command) tuple2 = tuple1;
    return $"{tuple2.source}|{tuple2.command}";
  }

  private static LogEventLevel GetLogEventLevel(SourceLevels level)
  {
    if (level == SourceLevels.All)
      return (LogEventLevel) 0;
    if (level == SourceLevels.Off)
      return (LogEventLevel) 5;
    if (level.HasFlag((Enum) SourceLevels.Verbose))
      return (LogEventLevel) 0;
    if (level.HasFlag((Enum) SourceLevels.Information))
      return (LogEventLevel) 2;
    if (level.HasFlag((Enum) SourceLevels.Warning))
      return (LogEventLevel) 3;
    if (level.HasFlag((Enum) SourceLevels.Error))
      return (LogEventLevel) 4;
    level.HasFlag((Enum) SourceLevels.Critical);
    return (LogEventLevel) 5;
  }

  private static string GetTraceEventType(TraceEventType eventType)
  {
    switch (eventType)
    {
      case TraceEventType.Critical:
        return "Critical";
      case TraceEventType.Error:
        return "Error";
      case TraceEventType.Warning:
        return "Warning";
      case TraceEventType.Information:
        return "Information";
      case TraceEventType.Verbose:
        return "Verbose";
      default:
        return (string) null;
    }
  }

  internal static void Reset()
  {
    if (HttpContext.Current == null || !PXContext.Session.IsSessionEnabled)
      return;
    PXContext.SessionTyped<PXSessionStatePXData>().PXTraceEvent["PXDataPXTrace"] = (PXTrace.Event) null;
  }

  [PXInternalUseOnly]
  [Serializable]
  public sealed class Event
  {
    private static readonly ConcurrentDictionary<CultureInfo, ITextFormatter> Formatters = new ConcurrentDictionary<CultureInfo, ITextFormatter>();
    public readonly TraceEventType EventType;
    public readonly System.DateTime RaiseDateTime;
    public readonly string Message;
    public readonly string StackTrace;
    public readonly string ScreenID = ScreenIdEnricher.GetScreenId(string.Empty);
    [NonSerialized]
    private readonly (string source, string command) _callbackInformation;
    public readonly string Source;
    public readonly string AdditionalInfo;
    private readonly string _location;
    [NonSerialized]
    internal readonly LogEvent LogEvent;

    public Exception TraceException { get; }

    private Event(
      TraceEventType eventType,
      System.DateTime raiseDateTime,
      (string source, string command) callbackInformation)
    {
      this.EventType = eventType;
      this.RaiseDateTime = raiseDateTime;
      this._callbackInformation = callbackInformation;
      this.Source = PXTrace.FormatAspNetCallbackInformation(this._callbackInformation);
    }

    private Event(TraceEventType eventType)
      : this(eventType, System.DateTime.Now, PXTrace.TryGetAspNetCallbackInformationImpl())
    {
    }

    internal Event(
      TraceEventType eventType,
      LogEventLevel level,
      string message,
      string stackTrace,
      object[] args,
      Exception exception)
      : this(eventType)
    {
      if (message != null && args != null && args.Length != 0)
      {
        MessageTemplate messageTemplate;
        IEnumerable<LogEventProperty> properties;
        if (PXTrace._logger.BindMessageTemplate(message, args, ref messageTemplate, ref properties))
        {
          this.LogEvent = this.CreateLogEvent(level, exception, messageTemplate, properties, stackTrace);
          this.Message = PXTrace.Event.RenderMessage(this.LogEvent);
        }
        else
        {
          SelfLog.WriteLine("Could not bind template: {0}", (object) message);
          this.Message = string.Format((IFormatProvider) PXTrace.Event.GetMessageFormattingCulture(), message, args);
          this.LogEvent = this.CreateLogEvent(level, exception, this.Message, stackTrace);
        }
      }
      else
      {
        this.Message = message;
        this.LogEvent = this.CreateLogEvent(level, exception, message, stackTrace);
      }
      this.StackTrace = stackTrace;
    }

    internal Event(
      ILogger logger,
      TraceEventType eventType,
      LogEventLevel level,
      Exception error,
      string[] additionalInfo)
      : this(eventType)
    {
      this.Message = error.Message;
      (this.TraceException, this.StackTrace) = PXTrace.Event.AggregateException(error, (string) null);
      if (additionalInfo != null)
        this.AdditionalInfo = string.Join(Environment.NewLine, additionalInfo);
      if (!logger.IsEnabled(level))
        return;
      this.LogEvent = this.CreateLogEvent(level, error, error.Message, this.StackTrace);
      if (additionalInfo == null || additionalInfo.Length == 0)
        return;
      this.LogEvent.AddOrUpdateProperty(new LogEventProperty("PXTrace_AdditionalInfo", (LogEventPropertyValue) new SequenceValue((IEnumerable<LogEventPropertyValue>) ((IEnumerable<string>) additionalInfo).Select<string, ScalarValue>((Func<string, ScalarValue>) (s => new ScalarValue((object) s))))));
    }

    internal Event(LogEvent logEvent)
      : this(PXTrace.Event.GetTraceEventType(logEvent.Level), logEvent.Timestamp.LocalDateTime, AspNetCallbackEnricher.TryGetAspNetCallbackInformation(logEvent))
    {
      this.Message = PXTrace.Event.RenderMessage(logEvent);
      this.LogEvent = (LogEvent) null;
      this.StackTrace = StackTraceEnricher.GetStackTrace(logEvent);
      this._location = SourceLocationEnricher.GetLocation(logEvent);
      if (logEvent.Exception == null)
        return;
      this.Message = $"{this.Message}{Environment.NewLine}{logEvent.Exception.GetType().FullName}: {logEvent.Exception.Message}";
      (this.TraceException, this.StackTrace) = PXTrace.Event.AggregateException(logEvent.Exception, this.StackTrace != null ? $"Stack trace from event:{Environment.NewLine}{this.StackTrace}" : (string) null);
    }

    private LogEvent CreateLogEvent(
      LogEventLevel level,
      Exception exception,
      string message,
      string stackTrace)
    {
      message = message ?? "<no message>";
      return this.CreateLogEvent(level, exception, new MessageTemplate(message, (IEnumerable<MessageTemplateToken>) new TextToken[1]
      {
        new TextToken(message)
      }), (IEnumerable<LogEventProperty>) new LogEventProperty[0], stackTrace);
    }

    private LogEvent CreateLogEvent(
      LogEventLevel level,
      Exception exception,
      MessageTemplate messageTemplate,
      IEnumerable<LogEventProperty> properties,
      string stackTrace)
    {
      Activity current = Activity.Current;
      LogEvent logEvent = new LogEvent(new DateTimeOffset(this.RaiseDateTime), level, exception, messageTemplate, properties, current != null ? current.TraceId : new ActivityTraceId(), current != null ? current.SpanId : new ActivitySpanId());
      AspNetCallbackEnricher.Enrich(logEvent, this._callbackInformation.source, this._callbackInformation.command);
      if (!string.IsNullOrWhiteSpace(this.ScreenID))
        logEvent.AddOrUpdateProperty(new LogEventProperty("ContextScreenId", (LogEventPropertyValue) new ScalarValue((object) ScreenIdEnricher.FormatScreenId(this.ScreenID))));
      if (!string.IsNullOrWhiteSpace(stackTrace))
        logEvent.AddOrUpdateProperty(new LogEventProperty("StackTrace", (LogEventPropertyValue) new ScalarValue((object) stackTrace)));
      return logEvent;
    }

    private static TraceEventType GetTraceEventType(LogEventLevel level)
    {
      switch ((int) level)
      {
        case 0:
          return TraceEventType.Verbose;
        case 1:
          return TraceEventType.Verbose;
        case 2:
          return TraceEventType.Information;
        case 3:
          return TraceEventType.Warning;
        case 4:
          return TraceEventType.Error;
        case 5:
          return TraceEventType.Critical;
        default:
          return TraceEventType.Information;
      }
    }

    private static string RenderMessage(LogEvent logEvent)
    {
      using (StringWriter stringWriter = new StringWriter())
      {
        PXTrace.Event.Formatters.GetOrAdd(PXTrace.Event.GetMessageFormattingCulture(), (Func<CultureInfo, ITextFormatter>) (c => (ITextFormatter) new MessageTemplateTextFormatter("{Message:l}", (IFormatProvider) c))).Format(logEvent, (TextWriter) stringWriter);
        return stringWriter.ToString();
      }
    }

    private static CultureInfo GetMessageFormattingCulture() => CultureInfo.CurrentCulture;

    private static (Exception traceException, string stackTrace) AggregateException(
      Exception error,
      string stackTrace)
    {
      stackTrace = stackTrace == null ? error.StackTrace : error.StackTrace + Environment.NewLine + Environment.NewLine + stackTrace;
      for (; error.InnerException != null; error = error.InnerException)
        stackTrace = error.InnerException?.ToString() + Environment.NewLine + stackTrace;
      if (error.GetType().Name == "MySqlException")
        error = new Exception(error.Message);
      return (error, stackTrace);
    }

    public override string ToString() => this.ToString(false);

    internal string ToString(bool withStackTrace)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.EventType == TraceEventType.Verbose)
        stringBuilder.AppendFormat("{0}", (object) this.RaiseDateTime);
      else
        stringBuilder.AppendFormat("{0} {1}", (object) this.RaiseDateTime, (object) this.EventType);
      if (this._location == null)
        stringBuilder.Append(":");
      else
        stringBuilder.AppendFormat(" at {0}:", (object) this._location);
      stringBuilder.AppendLine();
      stringBuilder.AppendLine(this.Message);
      if (!string.IsNullOrEmpty(this.AdditionalInfo))
      {
        stringBuilder.AppendLine();
        stringBuilder.AppendLine(this.AdditionalInfo);
      }
      if (withStackTrace)
      {
        stringBuilder.AppendLine();
        stringBuilder.AppendLine(this.StackTrace);
      }
      return stringBuilder.ToString();
    }
  }

  private class Sink : ILogEventSink
  {
    public void Emit(LogEvent logEvent)
    {
      PXTrace.WriteImpl(new PXTrace.Event(logEvent ?? throw new ArgumentNullException(nameof (logEvent))), (Exception) null, logEvent);
    }
  }
}
