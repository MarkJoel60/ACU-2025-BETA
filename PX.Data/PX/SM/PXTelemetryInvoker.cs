// Decompiled with JetBrains decompiler
// Type: PX.SM.PXTelemetryInvoker
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

[PXInternalUseOnly]
public static class PXTelemetryInvoker
{
  private static readonly Dictionary<string, object> Delegates = new Dictionary<string, object>();

  internal static void RegisterDelegate(string name, object callbackDelegate)
  {
    PXTelemetryInvoker.Delegates.Add(name, callbackDelegate);
  }

  private static T GetDelegate<T>(string name) where T : class
  {
    object obj1;
    return PXTelemetryInvoker.Delegates.TryGetValue(name, out obj1) && obj1 is T obj2 ? obj2 : default (T);
  }

  internal static bool IsTimersScopesEnabled()
  {
    Func<bool> func = PXTelemetryInvoker.GetDelegate<Func<bool>>(nameof (IsTimersScopesEnabled));
    return func != null && func();
  }

  internal static void AddToIncidentTelemetryHolder(PXPerformanceInfo info)
  {
    Action<PXPerformanceInfo> action = PXTelemetryInvoker.GetDelegate<Action<PXPerformanceInfo>>(nameof (AddToIncidentTelemetryHolder));
    if (action == null)
      return;
    action(info);
  }

  internal static void AddToLicenseResourceMonitoring(PXPerformanceInfo info)
  {
    Action<PXPerformanceInfo> action = PXTelemetryInvoker.GetDelegate<Action<PXPerformanceInfo>>(nameof (AddToLicenseResourceMonitoring));
    if (action == null)
      return;
    action(info);
  }

  internal static void CompleteInfoForLicenseResourceMonitoring(PXPerformanceInfo info)
  {
    Action<PXPerformanceInfo> action = PXTelemetryInvoker.GetDelegate<Action<PXPerformanceInfo>>(nameof (CompleteInfoForLicenseResourceMonitoring));
    if (action == null)
      return;
    action(info);
  }

  public static void ReportIncident(string userName, string incidentId)
  {
    Action<string, string> action = PXTelemetryInvoker.GetDelegate<Action<string, string>>(nameof (ReportIncident));
    if (action == null)
      return;
    action(userName, incidentId);
  }

  internal static void OnLongOperationStarting(object key)
  {
    Action<object> action = PXTelemetryInvoker.GetDelegate<Action<object>>(nameof (OnLongOperationStarting));
    if (action == null)
      return;
    action(key);
  }

  internal static void OnLongOperationStarted(object longRunId)
  {
    Action<object> action = PXTelemetryInvoker.GetDelegate<Action<object>>(nameof (OnLongOperationStarted));
    if (action == null)
      return;
    action(longRunId);
  }

  internal static void OnLongOperationEnded()
  {
    Action action = PXTelemetryInvoker.GetDelegate<Action>(nameof (OnLongOperationEnded));
    if (action == null)
      return;
    action();
  }

  internal static void OnLongOperationCompleted(object key)
  {
    Action<object> action = PXTelemetryInvoker.GetDelegate<Action<object>>(nameof (OnLongOperationCompleted));
    if (action == null)
      return;
    action(key);
  }

  internal static bool AllowDisableTelemetry()
  {
    Func<bool> func = PXTelemetryInvoker.GetDelegate<Func<bool>>(nameof (AllowDisableTelemetry));
    return func != null && func();
  }

  internal static void LogClientTime(
    string contextId,
    string screen,
    string target,
    string commandName,
    DateTime startDateTime,
    int clientTime,
    double serverTime,
    int clientRequestTime)
  {
    Action<Dictionary<string, object>> action = PXTelemetryInvoker.GetDelegate<Action<Dictionary<string, object>>>(nameof (LogClientTime));
    if (action == null)
      return;
    action(new Dictionary<string, object>()
    {
      {
        "ContextID",
        (object) contextId
      },
      {
        "Screen",
        (object) screen
      },
      {
        "CommandTarget",
        (object) target
      },
      {
        "CommandName",
        (object) commandName
      },
      {
        "StartTime",
        (object) startDateTime
      },
      {
        "ClientTime",
        (object) clientTime
      },
      {
        "ServerTime",
        (object) serverTime
      },
      {
        "ClientRequestTime",
        (object) clientRequestTime
      }
    });
  }

  internal static void RegisterErpTransaction(int count)
  {
    Action<int> action = PXTelemetryInvoker.GetDelegate<Action<int>>(nameof (RegisterErpTransaction));
    if (action == null)
      return;
    action(count);
  }

  internal static void RegisterErpTransactionAndType(
    int count,
    string primaryItemType,
    string key,
    string screen,
    object data)
  {
    Action<int, string, string, string, object> action = PXTelemetryInvoker.GetDelegate<Action<int, string, string, string, object>>(nameof (RegisterErpTransactionAndType));
    if (action != null)
      action(count, primaryItemType, key, screen, data);
    else
      PXTelemetryInvoker.RegisterErpTransaction(count);
  }
}
