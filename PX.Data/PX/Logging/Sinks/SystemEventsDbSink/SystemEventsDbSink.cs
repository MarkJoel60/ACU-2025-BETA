// Decompiled with JetBrains decompiler
// Type: PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;

#nullable disable
namespace PX.Logging.Sinks.SystemEventsDbSink;

internal class SystemEventsDbSink : ILogEventSink, IDisposable
{
  /// <summary>
  /// Keys of the properties shown in "Details" column on the fly (output to the user in System Event grid)
  /// </summary>
  public static Dictionary<string, string[]> DisplayedPropertiesPerEventType = new Dictionary<string, string[]>()
  {
    {
      "All",
      new string[7]
      {
        "EventID",
        "SourceContext",
        "ContextScreenId",
        "CurrentCompany",
        "ContextUserIdentity",
        "Exception",
        "CustomMessage"
      }
    },
    {
      "ResourceGovernor",
      new string[6]
      {
        "ElapsedTime",
        "SqlTimer",
        "SqlCounter",
        "TypeOfRequest",
        "CommandName",
        "CustomData"
      }
    },
    {
      "Customization",
      new string[5]
      {
        "TenantList",
        "CustomizationProjects",
        "CustomizationLog",
        "CustomScript",
        "ApiCallParams"
      }
    },
    {
      "Scheduler",
      new string[1]{ "Schedule" }
    },
    {
      "System",
      new string[11]
      {
        "SourceTenant",
        "Snapshot",
        "FromVersion",
        "ToVersion",
        "Errors",
        "CurrentVersion",
        "DestinationVersion",
        "SiteUrl",
        "Parameters",
        "TargetTenant",
        "IncidentId"
      }
    },
    {
      "PushNotifications",
      new string[8]
      {
        "TenantID",
        "TransactionID",
        "EventTransactionID",
        "SlowEvents",
        "Error",
        "Notification",
        "NotificationID",
        "NotificationSource"
      }
    },
    {
      "BusinessEvents",
      new string[9]
      {
        "TenantID",
        "TransactionID",
        "EventTransactionID",
        "SlowEvents",
        "Error",
        "Notification",
        "BPEventName",
        "NotificationID",
        "NotificationSource"
      }
    },
    {
      "Commerce",
      new string[8]
      {
        "TenantID",
        "TransactionID",
        "EventTransactionID",
        "SlowEvents",
        "Error",
        "Notification",
        "NotificationID",
        "NotificationSource"
      }
    },
    {
      "License",
      new string[1]{ "LoginLimit" }
    },
    {
      "ActiveDirectory",
      new string[1]{ "Error" }
    },
    {
      "DataConsistency",
      new string[4]
      {
        "DumpCaches",
        "GUID",
        "ErrorType",
        "CustomData"
      }
    },
    {
      "Email",
      new string[5]
      {
        "EmailsCount",
        "SuccessesCount",
        "ErrorsCount",
        "EmailAccountID",
        "EmailAccountDescription"
      }
    }
  };
  private readonly Timer Timer;
  private static readonly ITextFormatter Formatter = (ITextFormatter) new JsonFormatter((string) null, false, (IFormatProvider) null);
  private static readonly StringWriter Writer = new StringWriter();
  private static readonly ConcurrentQueue<LogEvent> Queue = new ConcurrentQueue<LogEvent>();

  private static void SaveEventsToDb(object state)
  {
    if (!PXAccess.Initialized)
      return;
    if (!Monitor.TryEnter((object) PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.Queue, state == null ? 0 : int.MaxValue))
      return;
    try
    {
      using (PXAccess.GetAdminScope())
      {
        LogEvent result;
        while (PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.Queue.TryDequeue(out result))
          PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.SaveEventToDb(result);
      }
    }
    finally
    {
      Monitor.Exit((object) PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.Queue);
    }
  }

  private static string SerializeLogEventToJson(LogEvent logEvent)
  {
    PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.Writer.GetStringBuilder().Clear();
    PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.Formatter.Format(logEvent, (TextWriter) PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.Writer);
    PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.Writer.Flush();
    return PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.Writer.ToString();
  }

  private static string GetStringFromScalarValue(LogEventPropertyValue propValue)
  {
    return (propValue is ScalarValue scalarValue ? scalarValue.Value : (object) null) as string;
  }

  private static void SaveEventToDb(LogEvent logEvent)
  {
    object obj = (object) null;
    string str1 = (string) null;
    string username = (string) null;
    string company = (string) null;
    string str2 = (string) null;
    try
    {
      LogEventPropertyValue eventPropertyValue;
      if (logEvent.Properties.TryGetValue("SourceContext", out eventPropertyValue))
        obj = ((ScalarValue) eventPropertyValue).Value;
      LogEventPropertyValue propValue1;
      if (logEvent.Properties.TryGetValue("ContextUserIdentity", out propValue1))
        username = PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.GetStringFromScalarValue(propValue1);
      if (!string.IsNullOrEmpty(username))
        LegacyCompanyService.ParseLogin(username, out username, out company, out string _);
      LogEventPropertyValue propValue2;
      if (logEvent.Properties.TryGetValue("CurrentCompany", out propValue2))
        company = PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.GetStringFromScalarValue(propValue2);
      LogEventPropertyValue propValue3;
      if (logEvent.Properties.TryGetValue("EventID", out propValue3))
        str2 = PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.GetStringFromScalarValue(propValue3);
      LogEventPropertyValue propValue4;
      if (logEvent.Properties.TryGetValue("ContextScreenId", out propValue4))
        str1 = PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.GetStringFromScalarValue(propValue4);
      string json = PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.SerializeLogEventToJson(logEvent);
      PXDatabase.Insert<SystemEvent>((PXDataFieldAssign) new PXDataFieldAssign<SystemEvent.level>((object) (int) logEvent.Level), (PXDataFieldAssign) new PXDataFieldAssign<SystemEvent.source>(obj), (PXDataFieldAssign) new PXDataFieldAssign<SystemEvent.eventId>((object) str2), (PXDataFieldAssign) new PXDataFieldAssign<SystemEvent.screenId>((object) str1), (PXDataFieldAssign) new PXDataFieldAssign<SystemEvent.date>((object) logEvent.Timestamp.UtcDateTime), (PXDataFieldAssign) new PXDataFieldAssign<SystemEvent.user>((object) username), (PXDataFieldAssign) new PXDataFieldAssign<SystemEvent.details>((object) logEvent.RenderMessage((IFormatProvider) null)), (PXDataFieldAssign) new PXDataFieldAssign<SystemEvent.tenantName>((object) company), (PXDataFieldAssign) new PXDataFieldAssign<SystemEvent.properties>((object) json));
    }
    catch (Exception ex)
    {
      SelfLog.WriteLine("Exception in {0}.{1}: {2}", (object) typeof (PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink).FullName, (object) nameof (SaveEventToDb), (object) ex.ToString());
    }
  }

  public SystemEventsDbSink()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this.Timer = new Timer(PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.\u003C\u003EO.\u003C0\u003E__SaveEventsToDb ?? (PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.\u003C\u003EO.\u003C0\u003E__SaveEventsToDb = new TimerCallback(PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.SaveEventsToDb)), (object) null, 5000, 5000);
  }

  internal static bool IsValidSystemEvent(LogEvent logEvent)
  {
    LogEventPropertyValue eventCategory;
    LogEventPropertyValue eventId;
    return logEvent.Properties.ContainsKey("SystemEvent") && logEvent.Properties.TryGetValue("SourceContext", out eventCategory) && logEvent.Properties.TryGetValue("EventID", out eventId) && ThrottledInvoker<string>.Invoke(eventId.ToString(), (System.Action) null, (System.Action) (() =>
    {
      LogEvent logEvent1 = new LogEvent(logEvent.Timestamp, logEvent.Level, (Exception) null, MessageTemplate.Empty, (IEnumerable<LogEventProperty>) new LogEventProperty[4]
      {
        new LogEventProperty("CustomMessage", (LogEventPropertyValue) new ScalarValue((object) "Too many events of this kind (EventID).")),
        new LogEventProperty("SourceContext", eventCategory),
        new LogEventProperty("EventID", eventId),
        new LogEventProperty("SystemEvent", (LogEventPropertyValue) new ScalarValue((object) true))
      });
      PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.Queue.Enqueue(logEvent1);
    }), TimeSpan.FromMinutes(1.0));
  }

  void ILogEventSink.Emit(LogEvent logEvent) => PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.Queue.Enqueue(logEvent);

  public void Dispose()
  {
    this.Timer?.Dispose();
    PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.SaveEventsToDb((object) 1);
    PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.Writer.Dispose();
  }

  /// <summary>
  /// Used by components outside of ERP worker process (via Reflection) to signal that the ERP worker process is getting terminated
  /// </summary>
  public static void RaiseProcessTerminating() => PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.SaveEventsToDb((object) 1);
}
