// Decompiled with JetBrains decompiler
// Type: PX.Logging.Enrichers.UserEnricher
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using Serilog.Core;
using Serilog.Events;
using System;

#nullable disable
namespace PX.Logging.Enrichers;

internal class UserEnricher : ILogEventEnricher
{
  internal const string UserPropertyName = "ContextUserIdentity";

  public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
  {
    if (logEvent == null)
      throw new ArgumentNullException(nameof (logEvent));
    LogEventPropertyValue eventPropertyValue;
    if (logEvent.Properties.TryGetValue("ContextUserIdentity", out eventPropertyValue))
    {
      if (!(eventPropertyValue is ScalarValue scalarValue) || scalarValue.Value != null)
        return;
      logEvent.RemovePropertyIfPresent("ContextUserIdentity");
    }
    else
    {
      string userName = UserEnricher.GetUserName((string) null);
      if (string.IsNullOrEmpty(userName))
        return;
      logEvent.AddPropertyIfAbsent(new LogEventProperty("ContextUserIdentity", (LogEventPropertyValue) new ScalarValue((object) userName)));
    }
  }

  internal static string GetUserName(string defaultValue)
  {
    try
    {
      string username;
      LegacyCompanyService.ParseLogin(PXContext.PXIdentity?.UserName ?? PXContext.PXIdentity?.User?.Identity?.Name, out username, out string _, out string _);
      return username ?? defaultValue;
    }
    catch (Exception ex)
    {
      SelfLog.WriteLine("Exception in {2}.{1}: {0}", (object) ex.ToString(), (object) nameof (GetUserName), (object) typeof (UserEnricher).FullName);
      return (string) null;
    }
  }
}
