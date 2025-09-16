// Decompiled with JetBrains decompiler
// Type: PX.Logging.Enrichers.AspNetCallbackEnricher
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.MIME;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading;
using System.Web;

#nullable disable
namespace PX.Logging.Enrichers;

internal class AspNetCallbackEnricher : ILogEventEnricher
{
  internal const string AspNetCallbackPropertyName = "AspNetCallbackInformation";
  internal const string TargetControlIdPropertyName = "TargetControlId";
  internal const string CommandPropertyName = "Command";
  private static readonly AsyncLocal<bool> IsDisabled = new AsyncLocal<bool>();
  private readonly Func<HttpRequestBase> _requestAccessor;

  public AspNetCallbackEnricher(Func<HttpRequestBase> requestAccessor)
  {
    this._requestAccessor = requestAccessor;
  }

  public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
  {
    if (logEvent == null)
      throw new ArgumentNullException(nameof (logEvent));
    HttpRequestBase request = this._requestAccessor();
    if (request == null)
      return;
    (string source, string command) = AspNetCallbackEnricher.TryGetAspNetCallbackInformation(request);
    AspNetCallbackEnricher.Enrich(logEvent, source, command);
  }

  internal static void Enrich(LogEvent logEvent, string source, string command)
  {
    if (source == null && command == null || logEvent.Properties.ContainsKey("AspNetCallbackInformation"))
      return;
    List<LogEventProperty> logEventPropertyList = new List<LogEventProperty>(2);
    if (source != null)
      logEventPropertyList.Add(new LogEventProperty("TargetControlId", (LogEventPropertyValue) new ScalarValue((object) source)));
    if (command != null)
      logEventPropertyList.Add(new LogEventProperty("Command", (LogEventPropertyValue) new ScalarValue((object) command)));
    logEvent.AddOrUpdateProperty(new LogEventProperty("AspNetCallbackInformation", (LogEventPropertyValue) new StructureValue((IEnumerable<LogEventProperty>) logEventPropertyList, (string) null)));
  }

  /// <summary>Gets current HTTP request</summary>
  /// <returns><c>null</c> if <see cref="P:System.Web.HttpContext.Current">HttpContext.Current</see> is <c>null</c></returns>
  /// <exception cref="T:System.Web.HttpException">Some obscure case when <see cref="P:System.Web.HttpContext.Request">HttpContext.Request</see> is not available</exception>
  internal static HttpRequestBase RequestAccessor()
  {
    HttpContext current = HttpContext.Current;
    return current != null ? (HttpRequestBase) new HttpRequestWrapper(current.Request) : (HttpRequestBase) null;
  }

  internal static (string source, string command) TryGetAspNetCallbackInformation(
    HttpRequestBase request)
  {
    if (AspNetCallbackEnricher.IsDisabled.Value)
      return ((string) null, (string) null);
    string str1;
    string str2;
    if (request.QueryString["fileupload"] == "1" || !string.IsNullOrWhiteSpace(request.ContentType) && request.ContentType.StartsWith(MediaTypes.Multipart.form_data, StringComparison.OrdinalIgnoreCase))
    {
      str1 = (string) null;
      str2 = "UploadFile";
    }
    else
    {
      str1 = request["__CALLBACKID"];
      str2 = request["__CALLBACKPARAM"];
    }
    if (str2 == null)
      return (str1, (string) null);
    int length = str2.IndexOf('|');
    return length <= 0 ? (str1, (string) null) : (str1, str2.Substring(0, length));
  }

  internal static (string source, string command) TryGetAspNetCallbackInformation(LogEvent logEvent)
  {
    string str1 = (string) null;
    string str2 = (string) null;
    LogEventPropertyValue eventPropertyValue;
    if (logEvent.Properties.TryGetValue("AspNetCallbackInformation", out eventPropertyValue) && eventPropertyValue is StructureValue structureValue)
    {
      foreach ((string str3, string str4) in structureValue.Properties.Select<LogEventProperty, (string, string)>((Func<LogEventProperty, (string, string)>) (p => (p.Name, (p.Value is ScalarValue scalarValue ? scalarValue.Value : (object) null) as string))))
      {
        switch (str3)
        {
          case "TargetControlId":
            str1 = str4;
            continue;
          case "Command":
            str2 = str4;
            continue;
          default:
            continue;
        }
      }
    }
    return (str1, str2);
  }

  internal static IDisposable Disable()
  {
    bool prevValue = AspNetCallbackEnricher.IsDisabled.Value;
    AspNetCallbackEnricher.IsDisabled.Value = true;
    return Disposable.Create((Action) (() => AspNetCallbackEnricher.IsDisabled.Value = prevValue));
  }
}
