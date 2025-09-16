// Decompiled with JetBrains decompiler
// Type: PX.Logging.Sinks.SystemEventsDbSink.SystemEventHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Logging.Sinks.SystemEventsDbSink;

[PXInternalUseOnly]
public static class SystemEventHelper
{
  /// <summary>
  /// Adds properties to the logger in order to create a System Event logging event.
  /// </summary>
  /// <param name="logger">Logger that is going to issue a System Event.</param>
  /// <param name="eventGroupName">Event group name.</param>
  /// <param name="eventID">Event ID.</param>
  /// <returns>Original Logger object to chain as fluent syntax.</returns>
  public static ILogger ForSystemEvents(this ILogger logger, string eventGroupName, string eventID)
  {
    return logger.ForTelemetry(eventGroupName, eventID).ForContext("SystemEvent", (object) true, false);
  }

  /// <summary>
  /// Supress username property adding in <see cref="T:PX.Logging.Enrichers.UserEnricher" />
  /// </summary>
  /// <param name="logger"></param>
  /// <returns></returns>
  public static ILogger WithoutUserName(this ILogger logger)
  {
    return logger.ForContext("ContextUserIdentity", (object) null, false);
  }

  /// <summary>Returns the current user's company name.</summary>
  public static string GetContextCompanyName()
  {
    string companyName = PXAccess.GetCompanyName();
    if (!string.IsNullOrEmpty(companyName))
      return companyName;
    string name = PXContext.PXIdentity?.User?.Identity?.Name;
    string company = PXLogin.ExtractCompany(name);
    if (!string.IsNullOrEmpty(company))
      return company;
    if (!string.IsNullOrEmpty(name))
    {
      PXDatabaseProvider provider = PXDatabase.Provider;
      int num1;
      if (provider == null)
      {
        num1 = 1;
      }
      else
      {
        int? length = provider.DbCompanies?.Length;
        int num2 = 1;
        num1 = !(length.GetValueOrDefault() == num2 & length.HasValue) ? 1 : 0;
      }
      if (num1 == 0)
        return PXDatabase.Provider.DbCompanies[0];
    }
    return (string) null;
  }

  /// <summary>
  /// Adds the current user's company name to the logging event context properties, if the company name can be retrieved at the moment.
  /// </summary>
  /// <param name="logger">Logger to add the property to.</param>
  /// <returns>Original Logger object to chain as fluent syntax.</returns>
  public static ILogger ForCurrentCompanyContext(this ILogger logger)
  {
    string contextCompanyName = SystemEventHelper.GetContextCompanyName();
    return contextCompanyName != null ? logger.ForContext("CurrentCompany", (object) contextCompanyName, false) : logger;
  }

  /// <summary>
  /// Logs the Resource Governor event to SystemEvent table. Intended to be used by RG via reflection.
  /// </summary>
  /// <param name="logLevel">Logging level (Debug, Information, etc.).</param>
  /// <param name="logEventId">EventId within "ResourceGovernor" event category.</param>
  /// <param name="screenId">Screen identifier ('ScreenID' colum in SystemEvent table).</param>
  /// <param name="userId">User identifier  ('User' colum in SystemEvent table).</param>
  /// <param name="customData">Additional key/value dictionary to pass as the log event properties.</param>
  public static void LogResourceGovernorSystemEvent(
    string logLevel,
    string logEventId,
    string screenId,
    string userId,
    IDictionary<string, string> customData = null)
  {
    string str = (string) null;
    customData?.TryGetValue("CurrentCompany", out str);
    object[] objArray = new object[1]{ (object) customData };
    ILogger ilogger = PXTrace.Logger.ForSystemEvents("ResourceGovernor", logEventId).ForContext("CustomData", (object) objArray, true).ForContext("ContextScreenId", (object) screenId, false).ForContext("ContextUserIdentity", (object) userId, false).ForContext("CurrentCompany", (object) str, false);
    LogEventLevel result;
    if (!string.IsNullOrWhiteSpace(logLevel) && Enum.TryParse<LogEventLevel>(logLevel, out result))
      ilogger.Write(result, "A request has been terminated");
    else
      ilogger.Error("A request has been terminated");
  }
}
