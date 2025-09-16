// Decompiled with JetBrains decompiler
// Type: PX.Common.PXUrl
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using CommonServiceLocator;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

#nullable enable
namespace PX.Common;

public static class PXUrl
{
  public const string PopupParameter = "PopupPanel";
  public const string InLayerParameter = "InLayer";
  public const string HideScriptParameter = "HideScript";
  public const string TimeStamp = "timeStamp";
  public const string UNum = "unum";
  public const string HidePageTitle = "HidePageTitle";
  public const string CompanyID = "CompanyID";
  public const string MainPagePath = "~/Main";
  public const string HelpPagePath = "~/Help";
  public const string ForceUIParameter = "ui";
  public const string IsRedirect = "isRedirect";
  [PXInternalUseOnly]
  public static readonly string[] SystemParams = UrlConstants.SystemParams;
  [Obsolete("This entity is obsolete and will be removed. Rewrite your code without this class or contact your partner for assistance.")]
  public const string InstanceId = "instanceid";
  [Obsolete("This entity is obsolete and will be removed. Rewrite your code without this class or contact your partner for assistance.")]
  public const string Id = "id";
  [Obsolete("This entity is obsolete and will be removed. Rewrite your code without this class or contact your partner for assistance.")]
  public const string ScrId = "scrid";
  [Obsolete("This entity is obsolete and will be removed. Rewrite your code without this class or contact your partner for assistance.")]
  public const string PageId = "pageid";
  [Obsolete("This entity is obsolete and will be removed. Rewrite your code without this class or contact your partner for assistance.")]
  public const string FileId = "fileid";
  /// <summary>
  /// The list of query string parameters that are preserved when logging a request URL for performance analysis.
  /// </summary>
  /// <remarks>Used in <see cref="M:PX.Common.PXUrl.GetScreenUrl(System.String)" /> that is called by Request Profiler to get a request URL. Not used anywhere else, including the actual user-facing page URL in the browser.</remarks>
  [PXInternalUseOnly]
  [Obsolete("This entity is obsolete and will be removed. Rewrite your code without this class or contact your partner for assistance.")]
  public static readonly string[] ScreenParams = new string[5]
  {
    "instanceid",
    "id",
    "scrid",
    "pageid",
    "fileid"
  };

  public static string GetDefaultMainPage()
  {
    return (WebConfig.GetBool("HelpPortal", false) ? 1 : (WebConfig.GetString("DefaultRoute", string.Empty).ToLower() == "help" ? 1 : 0)) == 0 ? "~/Main" : "~/Help";
  }

  public static string IgnoreQueryParameters(string url, params string[] parameters)
  {
    if (url.IndexOf('?') < 0)
      return url;
    if (url.EndsWith("?"))
      return url.TrimEnd('?');
    string url1 = url;
    foreach (string parameter in parameters)
    {
      if (url1.IndexOf(parameter, StringComparison.OrdinalIgnoreCase) >= 0)
        url1 = PXUrl.IgnoreQueryParameter(url1, parameter);
    }
    return url1;
  }

  /// <summary>Removes all the query parameters from the url.</summary>
  [return: NotNullIfNotNull("url")]
  public static string? IngoreAllQueryParameters(string? url)
  {
    if (!Str.IsNullOrEmpty(url))
    {
      int startIndex = url.IndexOf("?", StringComparison.OrdinalIgnoreCase);
      if (startIndex >= 0)
        return url.Remove(startIndex, url.Length - startIndex);
    }
    return url;
  }

  public static string IgnoreSystemParameters(string url)
  {
    return PXUrl.IgnoreQueryParameters(url, PXUrl.SystemParams);
  }

  [return: NotNullIfNotNull("url")]
  public static string? IgnoreQueryParameter(string? url, string? parameter)
  {
    if (!Str.IsNullOrEmpty(url) && !Str.IsNullOrEmpty(parameter))
    {
      int startIndex1 = url.IndexOf("?" + parameter, StringComparison.OrdinalIgnoreCase);
      if (startIndex1 != -1)
      {
        int num = url.IndexOf('&', startIndex1 + 1);
        url = num == -1 ? url.Remove(startIndex1) : url.Remove(startIndex1 + 1, num - startIndex1);
      }
      else
      {
        int startIndex2 = url.IndexOf("&" + parameter, StringComparison.OrdinalIgnoreCase);
        if (startIndex2 != -1)
        {
          int num = url.IndexOf('&', startIndex2 + 1);
          url = num == -1 ? url.Remove(startIndex2) : url.Remove(startIndex2 + 1, num - startIndex2);
        }
      }
    }
    return url;
  }

  public static string CombineParameters(string url, params string[] parameters)
  {
    string url1 = url;
    foreach (string str in ((IEnumerable<string>) parameters).SelectMany<string, string>(PXUrl.\u0002.\u000E ?? (PXUrl.\u0002.\u000E = new Func<string, IEnumerable<string>>(PXUrl.\u0002.\u0002.\u0002))))
    {
      string[] strArray = str.Split('=');
      if (strArray.Length == 2)
      {
        if (PXUrl.GetParameter(url, strArray[0]) != null)
          url1 = PXUrl.IgnoreQueryParameter(url1, strArray[0]);
        url1 = PXUrl.AppendUrlParameter(url1, strArray[0], strArray[1]);
      }
    }
    return url1;
  }

  public static bool IsExternalUrl(string url)
  {
    return url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || url.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
  }

  public static string GetAppDomainAppVirtualPath() => HttpRuntime.AppDomainAppVirtualPath;

  public static string ToAbsoluteUrl(string url, bool ignoreSystemParams = true)
  {
    if (Str.IsNullOrEmpty(ExceptionExtensions.CheckIfNull<string>(url, nameof (url), (string) null)))
      throw new ArgumentOutOfRangeException(nameof (url));
    if (!Str.IsNullOrEmpty(HttpRuntime.AppDomainAppVirtualPath))
    {
      int num = url.IndexOf('?');
      string url1 = (string) null;
      if (num != -1)
      {
        url1 = url.Substring(num);
        if (ignoreSystemParams)
          url1 = PXUrl.IgnoreSystemParameters(url1);
        url = url.Substring(0, num);
      }
      if (PXUrl.IsExternalUrl(url))
      {
        Uri uri = new Uri(url);
        if (uri.HostNameType == UriHostNameType.Dns && !string.Equals(uri.DnsSafeHost, "localhost", StringComparison.OrdinalIgnoreCase) && !string.Equals(uri.DnsSafeHost, Dns.GetHostName(), StringComparison.InvariantCultureIgnoreCase))
          throw new ArgumentException("External URL has been specified.", nameof (url));
        IPAddress ipAddress;
        if ((uri.HostNameType == UriHostNameType.IPv4 || uri.HostNameType == UriHostNameType.IPv6) && Net_Utils.TryParseIPAddress(uri.DnsSafeHost, ref ipAddress))
        {
          bool flag = false;
          foreach (IPAddress hostAddress in Dns.GetHostAddresses(Dns.GetHostName()))
          {
            if (hostAddress == ipAddress || string.Equals(hostAddress.ToString(), ipAddress.ToString(), StringComparison.OrdinalIgnoreCase))
            {
              flag = true;
              break;
            }
          }
          if (!flag)
            throw new ArgumentException("External URL has been specified.", nameof (url));
        }
        url = uri.LocalPath;
      }
      if (VirtualPathUtility.IsAppRelative(url))
        url = VirtualPathUtility.ToAbsolute(url);
      if (num != -1)
        url += url1;
    }
    return url;
  }

  public static string RemoveSessionSplit(string url)
  {
    int length = url.IndexOf("/(W(");
    if (length < 0)
      return url;
    int num = url.IndexOf("))", length + 4, url.Length - length - 4);
    return num < 0 ? url : url.Substring(0, length) + url.Substring(num + 2);
  }

  [return: NotNullIfNotNull("url")]
  public static string? GetSessionId(string? url)
  {
    if (Str.IsNullOrEmpty(url))
      return url;
    int startIndex = url.IndexOf("/(W(");
    if (startIndex < 0)
      return url;
    int num = url.IndexOf("))", startIndex + 4, url.Length - startIndex - 4);
    return num < 0 ? url : url.Substring(startIndex, num - startIndex + 2);
  }

  public static string ToRelativeUrl(string url)
  {
    if (!Str.IsNullOrEmpty(HttpRuntime.AppDomainAppVirtualPath))
    {
      int num = url.IndexOf('?');
      string str = (string) null;
      if (num != -1)
      {
        str = url.Substring(num);
        url = url.Substring(0, num);
      }
      if (PXUrl.IsExternalUrl(url))
      {
        int startIndex = url.IndexOf('/', 8);
        if (startIndex != -1)
          url = url.Substring(startIndex);
      }
      if (!VirtualPathUtility.IsAppRelative(url))
      {
        try
        {
          url = VirtualPathUtility.ToAppRelative(url);
        }
        catch
        {
        }
      }
      if (num != -1)
        url += str;
    }
    return url;
  }

  public static string SiteUrlWithPath()
  {
    try
    {
      if (HttpContext.Current != null)
      {
        if (HttpContext.Current.Request != null)
          goto label_4;
      }
      return "";
    }
    catch
    {
      return "";
    }
label_4:
    return HttpContext.Current.Request.SiteUrlWithPath();
  }

  internal static string SiteUrlWithPath(
    string _param0,
    string _param1,
    Func<string, string> _param2)
  {
    string str = PXUrl.GetWebsiteUrl(_param0, _param2);
    if (str != null && str.Length > 1)
      str = str.Remove(str.Length - 1);
    return str + _param1;
  }

  public static Uri AppendParameter(this Uri url, string key, string value)
  {
    return new Uri(PXUrl.AppendUrlParameter(ExceptionExtensions.CheckIfNull<Uri>(url, nameof (url), (string) null).OriginalString, key, value));
  }

  public static string AppendUrlParameter(this string url, string key, string value)
  {
    if (url == null)
      throw new ArgumentNullException(nameof (url));
    if (key == null)
      throw new ArgumentNullException(nameof (key));
    int startIndex1 = url.IndexOf('?');
    string str1 = startIndex1 > -1 ? url.Substring(startIndex1) : string.Empty;
    string str2 = str1;
    string str3 = $"&{key}=";
    int length;
    string str4;
    if ((length = str2.IndexOf(str3)) > -1 || (length = str2.IndexOf(str3 = $"?{key}=")) > -1)
    {
      int startIndex2 = length + str3.Length;
      int startIndex3 = startIndex2 < str2.Length - 1 ? str2.IndexOf('&', startIndex2) : -1;
      str4 = str2.Substring(0, length) + str3 + value + (startIndex3 > -1 ? str2.Substring(startIndex3) : string.Empty);
    }
    else
    {
      string str5 = str2.Length > 0 ? (str2 != "?" ? "&" : string.Empty) : "?";
      str4 = $"{str2}{str5}{key}={value}";
    }
    return (str1.Length > 0 ? url.Substring(0, url.Length - str1.Length) : url) + str4;
  }

  public static string AppendUrlParameter(this string url, string key, object value)
  {
    if (value == null)
      throw new ArgumentNullException(nameof (value));
    return PXUrl.AppendUrlParameter(url, key, value.ToString());
  }

  public static string? GetParameter(string url, string key)
  {
    if (url == null)
      throw new ArgumentNullException(nameof (url));
    if (key == null)
      throw new ArgumentNullException(nameof (key));
    int startIndex1 = url.IndexOf('?');
    string str1 = startIndex1 > -1 ? url.Substring(startIndex1) : string.Empty;
    string lower = str1.ToLower();
    key = key.ToLower();
    string str2 = $"&{key}=";
    int num1;
    if ((num1 = lower.IndexOf(str2)) <= -1 && (num1 = lower.IndexOf(str2 = $"?{key}=")) <= -1)
      return (string) null;
    int startIndex2 = num1 + str2.Length;
    int num2 = startIndex2 < lower.Length - 1 ? lower.IndexOf('&', startIndex2) : -1;
    return num2 > startIndex2 ? str1.Substring(startIndex2, num2 - startIndex2) : str1.Substring(startIndex2);
  }

  public static NameValueCollection GetParameters(string? url)
  {
    if (!Str.IsNullOrEmpty(url))
    {
      int startIndex = url.IndexOf("?", StringComparison.OrdinalIgnoreCase);
      if (startIndex >= 0)
      {
        string query = url.Substring(startIndex, url.Length - startIndex);
        if (!Str.IsNullOrEmpty(query))
          return HttpUtility.ParseQueryString(query);
      }
    }
    return new NameValueCollection((IEqualityComparer) StringComparer.OrdinalIgnoreCase);
  }

  [return: NotNullIfNotNull("url")]
  public static string? TrimUrl(string? url)
  {
    if (Str.IsNullOrEmpty(url))
      return url;
    int length = url.IndexOfAny(new char[1]{ '#' });
    return length <= -1 ? url : url.Substring(0, length);
  }

  [return: NotNullIfNotNull("rawUrl")]
  public static string? GetScreenUrl(string? rawUrl)
  {
    if (Str.IsNullOrEmpty(rawUrl))
      return rawUrl;
    string relativeUrl = PXUrl.ToRelativeUrl(rawUrl);
    return relativeUrl.StartsWith("~/pages/", StringComparison.OrdinalIgnoreCase) || relativeUrl.StartsWith("~/Main", StringComparison.OrdinalIgnoreCase) ? PXUrl.IngoreAllQueryParameters(relativeUrl) : PXUrl.PersistQueryParameters(relativeUrl, PXUrl.ScreenParams);
  }

  public static string PersistQueryParameters(string url, params string[] parameters)
  {
    if (url.IndexOf('?') < 0)
      return url;
    if (url.EndsWith("?"))
      return url.TrimEnd('?');
    string url1 = url;
    foreach (string parameter in (NameObjectCollectionBase) PXUrl.GetParameters(url1))
    {
      PXUrl.\u000E obj = new PXUrl.\u000E();
      obj.\u0002 = parameter;
      if (!((IEnumerable<string>) parameters).Any<string>(new Func<string, bool>(obj.\u0002)))
        url1 = PXUrl.IgnoreQueryParameter(url1, obj.\u0002);
    }
    return url1;
  }

  /// <summary>Encode the specified string value for JavaScript.</summary>
  public static string QuoteString(string value)
  {
    StringBuilder stringBuilder = (StringBuilder) null;
    if (Str.IsNullOrEmpty(value))
      return string.Empty;
    int startIndex = 0;
    int count = 0;
    for (int index = 0; index < value.Length; ++index)
    {
      char ch = value[index];
      switch (ch)
      {
        case '\b':
        case '\t':
        case '\n':
        case '\f':
        case '\r':
        case '"':
        case '\'':
        case '<':
        case '>':
        case '\\':
          if (stringBuilder == null)
            stringBuilder = new StringBuilder(value.Length + 5);
          if (count > 0)
            stringBuilder.Append(value, startIndex, count);
          startIndex = index + 1;
          count = 0;
          break;
        default:
          if (ch >= ' ')
            break;
          goto case '\b';
      }
      switch (ch)
      {
        case '\b':
          stringBuilder.Append("\\b");
          break;
        case '\t':
          stringBuilder.Append("\\t");
          break;
        case '\n':
          stringBuilder.Append("\\n");
          break;
        case '\f':
          stringBuilder.Append("\\f");
          break;
        case '\r':
          stringBuilder.Append("\\r");
          break;
        case '"':
          stringBuilder.Append("\\\"");
          break;
        case '\'':
        case '<':
        case '>':
          PXUrl.\u0002(stringBuilder, ch);
          break;
        case '\\':
          stringBuilder.Append("\\\\");
          break;
        default:
          if (ch < ' ')
          {
            PXUrl.\u0002(stringBuilder, ch);
            break;
          }
          ++count;
          break;
      }
    }
    if (stringBuilder == null)
      return value;
    if (count > 0)
      stringBuilder.Append(value, startIndex, count);
    return stringBuilder.ToString();
  }

  private static void \u0002(StringBuilder _param0, char _param1)
  {
    _param0.Append("\\u");
    _param0.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, "{0:x4}", new object[1]
    {
      (object) (int) _param1
    });
  }

  public static bool IsMainPage(string rawUrl)
  {
    ExceptionExtensions.ThrowOnNull<string>(rawUrl, nameof (rawUrl), (string) null);
    string url = PXUrl.IngoreAllQueryParameters(rawUrl);
    if (!url.StartsWith("~"))
      url = PXUrl.ToRelativeUrl(url);
    return "~/Main".Equals(url, StringComparison.OrdinalIgnoreCase) || url.StartsWith("~/Main.", StringComparison.OrdinalIgnoreCase);
  }

  /// <summary>
  /// Returns url for current website in format http(s)://hostname/
  /// This url can be used in external links.
  /// </summary>
  /// <param name="requestUrl">Request URL</param>
  /// <param name="headerProvider">Header provider. Must return <c>null</c> if header isn't found</param>
  public static string GetWebsiteUrl(Uri requestUrl, Func<string, string> headerProvider)
  {
    return PXUrl.GetWebsiteUrl(requestUrl.Scheme, headerProvider);
  }

  internal static string GetWebsiteUrl(string _param0, Func<string, string> _param1)
  {
    (string str1, string str2, int? nullable) = PXUrl.\u0002(_param0, _param1);
    return new Uri(!nullable.HasValue ? $"{str1}://{str2}/" : $"{str1}://{str2}:{nullable}/").ToString();
  }

  internal static Uri ToExternalUri(Uri _param0, Func<string, string> _param1)
  {
    UriBuilder uriBuilder = new UriBuilder(_param0);
    (string str, string s, int? nullable) = PXUrl.\u0002(_param0.Scheme, _param1);
    uriBuilder.Scheme = str;
    string[] strArray = s.Split(':', 2);
    int result;
    if (strArray.Length == 2 && int.TryParse(strArray[1], NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result))
    {
      s = strArray[0];
      uriBuilder.Port = result;
    }
    else
      uriBuilder.Port = -1;
    if (nullable.HasValue)
      uriBuilder.Port = nullable.Value;
    uriBuilder.Host = s;
    return uriBuilder.Uri;
  }

  private static (string, string, int?) \u0002(string _param0, Func<string, string> _param1)
  {
    string str1 = _param1("X-Host");
    string s;
    if (!Str.IsNullOrEmpty(str1) && str1.Contains(":"))
    {
      int result;
      s = int.TryParse(str1.Split(':')[1], out result) ? result.ToString() : _param1("X-Forwarded-Port");
    }
    else
      s = _param1("X-Forwarded-Port");
    if (Str.IsNullOrEmpty(str1))
      str1 = _param1("Host");
    if (Str.IsNullOrEmpty(str1))
      throw new ApplicationException("Host HTTP header is empty.");
    string str2 = _param1("X-Scheme");
    if (Str.IsNullOrEmpty(str2))
      str2 = _param1("X-Forwarded-Proto");
    if (Str.IsNullOrEmpty(str2))
      str2 = _param0;
    int result1;
    if (Str.IsNullOrEmpty(s) || !int.TryParse(s, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result1))
      return (str2, str1, new int?());
    int length = str1.LastIndexOf(':');
    if (length >= 0)
      str1 = str1.Substring(0, length);
    return (str2, str1, new int?(result1));
  }

  public static bool IsAbsolute(string url)
  {
    return !Str.IsNullOrEmpty(url) ? Uri.TryCreate(url, UriKind.Absolute, out Uri _) : throw new ArgumentException("Url cannot be null", nameof (url));
  }

  internal static bool IsInternalUrl(string _param0, string _param1)
  {
    return PXUrl.IsInternalUrl(_param0, _param1, PXUrl.SiteUrlWithPath());
  }

  internal static bool IsInternalUrl(string _param0, string _param1, string _param2)
  {
    ILogger ilogger = (ILogger) null;
    if (ServiceLocator.IsLocationProviderSet)
      ilogger = LoggerFactoryExtensions.CreateLogger(ServiceLocator.Current.GetInstance<ILoggerFactory>(), typeof (PXUrl));
    Uri result1;
    if (!Uri.TryCreate(_param1, UriKind.Absolute, out result1))
    {
      if (ilogger != null)
        LoggerExtensions.LogWarning(ilogger, "Return Url fails : can't create current url {currentDirectoryUrl}", new object[1]
        {
          (object) _param1
        });
      return false;
    }
    Uri result2;
    if (!Uri.TryCreate(_param0, UriKind.Absolute, out result2) && !Uri.TryCreate(result1, _param0, out result2))
    {
      if (ilogger != null)
        LoggerExtensions.LogWarning(ilogger, "Return Url fails : can't create absolute from {uri} and cann't combine it with dir url {baseUrl}", new object[2]
        {
          (object) _param0,
          (object) _param1
        });
      return false;
    }
    Uri uri = new Uri(_param2);
    bool flag = result2.AbsoluteUri.StartsWith(uri.AbsoluteUri, StringComparison.OrdinalIgnoreCase);
    if (!flag && ilogger != null)
      LoggerExtensions.LogWarning(ilogger, "Return Url fails : Resulting url {uri} should be a part of application url {appUrl}", new object[2]
      {
        (object) result2.AbsoluteUri,
        (object) _param2
      });
    return flag;
  }

  internal static string GetCurrentDirectoryUrl(HttpRequestBase _param0)
  {
    PXUrl.\u0006 obj = new PXUrl.\u0006();
    obj.\u0002 = _param0;
    Uri uri = obj.\u0002.Url;
    try
    {
      uri = PXUrl.ToExternalUri(uri, new Func<string, string>(obj.\u0002));
    }
    catch
    {
    }
    string leftPart = uri.GetLeftPart(UriPartial.Path);
    return leftPart.Remove(leftPart.Length - ((IEnumerable<string>) obj.\u0002.Url.Segments).Last<string>().Length);
  }

  [Serializable]
  private sealed class \u0002
  {
    public static readonly 
    #nullable disable
    PXUrl.\u0002 \u0002 = new PXUrl.\u0002();
    public static Func<string, IEnumerable<string>> \u000E;

    internal IEnumerable<string> \u0002(string _param1)
    {
      return (IEnumerable<string>) _param1.TrimStart('?').Split(new char[1]
      {
        '&'
      }, StringSplitOptions.RemoveEmptyEntries);
    }
  }

  private sealed class \u0006
  {
    public HttpRequestBase \u0002;

    internal 
    #nullable enable
    string \u0002(string _param1) => this.\u0002.Headers[_param1];
  }

  private sealed class \u000E
  {
    public 
    #nullable disable
    string \u0002;

    internal bool \u0002(string _param1)
    {
      return _param1.Equals(this.\u0002, StringComparison.OrdinalIgnoreCase);
    }
  }
}
