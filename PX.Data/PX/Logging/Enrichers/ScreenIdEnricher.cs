// Decompiled with JetBrains decompiler
// Type: PX.Logging.Enrichers.ScreenIdEnricher
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Logging.Enrichers;

internal class ScreenIdEnricher : ILogEventEnricher
{
  internal const string ScreenIdPropertyName = "ContextScreenId";
  internal static readonly Regex ScreenRegex = new Regex("^[A-Z][A-Z]\\.[0-9A-Z][0-9A-Z]\\.[0-9A-Z][0-9A-Z]\\.[0-9A-Z][0-9A-Z]$", RegexOptions.Compiled);

  public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
  {
    if (logEvent == null)
      throw new ArgumentNullException(nameof (logEvent));
    string str1 = (string) null;
    LogEventPropertyValue eventPropertyValue1;
    if (logEvent.Properties.TryGetValue("ContextScreenId", out eventPropertyValue1))
    {
      LogEventPropertyValue eventPropertyValue2 = eventPropertyValue1;
      if (eventPropertyValue2 != null)
      {
        if (eventPropertyValue2 is ScalarValue scalarValue)
        {
          if (scalarValue.Value != null)
          {
            if (scalarValue.Value is string str2)
            {
              str1 = string.IsNullOrWhiteSpace(str2) ? (string) null : str2;
            }
            else
            {
              SelfLog.WriteLine("ContextScreenId property has unexpected scalar value type {0}", (object) scalarValue.Value.GetType().FullName);
              return;
            }
          }
        }
        else
        {
          SelfLog.WriteLine("ContextScreenId property has unexpected type {0}", (object) eventPropertyValue1.GetType().FullName);
          return;
        }
      }
    }
    string str3 = ScreenIdEnricher.FormatScreenId(str1 ?? ScreenIdEnricher.GetScreenId((string) null));
    if (str3 == null)
      return;
    logEvent.AddOrUpdateProperty(new LogEventProperty("ContextScreenId", (LogEventPropertyValue) new ScalarValue((object) str3)));
  }

  internal static string FormatScreenId(string screenId)
  {
    if (screenId == null)
      return (string) null;
    screenId = screenId.Trim();
    if (screenId.Length == 0)
      return (string) null;
    screenId = screenId.ToUpperInvariant();
    return screenId.StartsWith("~") || !ScreenIdEnricher.ScreenRegex.IsMatch(screenId) ? screenId : screenId.Replace(".", "");
  }

  internal static string GetScreenId(string defaultValue)
  {
    try
    {
      return PXContext.GetScreenID() ?? defaultValue;
    }
    catch (Exception ex)
    {
      SelfLog.WriteLine("Exception in {2}.{1}: {0}", (object) ex.ToString(), (object) nameof (GetScreenId), (object) typeof (ScreenIdEnricher).FullName);
      return (string) null;
    }
  }
}
