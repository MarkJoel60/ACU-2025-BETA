// Decompiled with JetBrains decompiler
// Type: PX.Data.ProjectDefinition.Workflow.ActionSequencesLog
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Data.ProjectDefinition.Workflow;

internal static class ActionSequencesLog
{
  private const string ActionSequencesLogKey = "ActionSequencesLogKey";

  internal static void Log(
    string message,
    string actionName,
    bool massProcess,
    Exception exception = null,
    string errorMessage = null,
    bool isWarning = false)
  {
    ActionSequencesLog.InternalLog(new ActionSequencesLog.LogEntry()
    {
      Message = message,
      Exception = exception,
      ErrorMessage = errorMessage,
      IsWarning = isWarning,
      ActionName = actionName
    });
  }

  internal static void LogRedirect(
    string message,
    PXBaseRedirectException exception,
    bool massProcess)
  {
    exception.Mode = PXBaseRedirectException.WindowMode.New;
    ActionSequencesLog.InternalLog(new ActionSequencesLog.LogEntry()
    {
      Message = message,
      Exception = (Exception) exception
    });
  }

  private static void InternalLog(ActionSequencesLog.LogEntry logEntry)
  {
    List<ActionSequencesLog.LogEntry> info = (List<ActionSequencesLog.LogEntry>) PXLongOperation.GetCustomInfoForCurrentThread("ActionSequencesLogKey") ?? new List<ActionSequencesLog.LogEntry>();
    lock (((ICollection) info).SyncRoot)
      info.Add(logEntry);
    PXLongOperation.SetCustomInfoInternal(PXLongOperation.GetOperationKey(), "ActionSequencesLogKey", (object) info);
  }

  internal static void ClearLog(object key = null)
  {
    key = key ?? PXLongOperation.GetOperationKey();
    List<ActionSequencesLog.LogEntry> customInfo = (List<ActionSequencesLog.LogEntry>) PXLongOperation.GetCustomInfo(key, "ActionSequencesLogKey");
    if (customInfo == null)
      return;
    lock (((ICollection) customInfo).SyncRoot)
      customInfo.Clear();
  }

  internal static List<ActionSequencesLog.LogEntry> GetLog(object key = null)
  {
    key = key ?? PXLongOperation.GetOperationKey();
    List<ActionSequencesLog.LogEntry> customInfo = (List<ActionSequencesLog.LogEntry>) PXLongOperation.GetCustomInfo(key, "ActionSequencesLogKey");
    if (customInfo == null)
      return (List<ActionSequencesLog.LogEntry>) null;
    lock (((ICollection) customInfo).SyncRoot)
      return customInfo.ToList<ActionSequencesLog.LogEntry>();
  }

  internal static IEnumerable<PXReportRequiredException> GetReportRedirects()
  {
    List<ActionSequencesLog.LogEntry> log = ActionSequencesLog.GetLog();
    return log == null ? (IEnumerable<PXReportRequiredException>) null : log.Select<ActionSequencesLog.LogEntry, Exception>((Func<ActionSequencesLog.LogEntry, Exception>) (l => l.Exception)).OfType<PXReportRequiredException>();
  }

  internal static string GetMassProcessingMessage()
  {
    List<ActionSequencesLog.LogEntry> log = ActionSequencesLog.GetLog();
    if (log == null)
      return (string) null;
    StringBuilder stringBuilder = new StringBuilder();
    foreach (ActionSequencesLog.LogEntry logEntry in log)
    {
      string message = logEntry.Message;
      string str = logEntry.ErrorMessage ?? logEntry.Exception?.Message ?? logEntry.Exception?.ToString();
      stringBuilder.AppendLine(message);
      if (str != null)
        stringBuilder.AppendLine(str);
    }
    return stringBuilder.Length <= 0 ? (string) null : stringBuilder.ToString();
  }

  internal static bool HasErrors()
  {
    List<ActionSequencesLog.LogEntry> log = ActionSequencesLog.GetLog();
    return log != null && log.Any<ActionSequencesLog.LogEntry>((Func<ActionSequencesLog.LogEntry, bool>) (l => l.Exception != null && !(l.Exception is PXBaseRedirectException)));
  }

  internal class LogEntry
  {
    public string Message { get; set; }

    public Exception Exception { get; set; }

    public string ErrorMessage { get; set; }

    public bool IsWarning { get; set; }

    public string ActionName { get; set; }
  }
}
