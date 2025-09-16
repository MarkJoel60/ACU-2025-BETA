// Decompiled with JetBrains decompiler
// Type: PX.Common.UrlExtensions
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using PX.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;

#nullable enable
namespace PX.Common;

[PXInternalUseOnly]
public static class UrlExtensions
{
  public const string ExternalUrlSpecifiedError = "External URL has been specified.";
  public const string PopupParameter = "PopupPanel";
  public const string InLayerParameter = "InLayer";
  public const string HideScriptParameter = "HideScript";
  public const string TimeStamp = "timeStamp";
  public const string UNum = "unum";
  public const string HidePageTitle = "HidePageTitle";
  public const string ActiveContext = "ActiveContext";
  public const string CompanyID = "CompanyID";
  public const string ForceUIParameter = "ui";
  public const string IsRedirectFlagQueryParams = "isRedirect";
  public static readonly string[] SystemParams = new string[10]
  {
    "PopupPanel",
    "HideScript",
    "timeStamp",
    "unum",
    nameof (HidePageTitle),
    nameof (ActiveContext),
    nameof (CompanyID),
    "InLayer",
    "ui",
    "isRedirect"
  };

  public static string ToRelativeUrl(
    this string url,
    string? basePath = null,
    IDictionary<string, string?>? queryParamsToAdd = null,
    bool ignoreSystemParams = true)
  {
    ExceptionExtensions.ThrowOnNullOrWhiteSpace(url, nameof (url), (string) null);
    url = url.Replace('\\', '/');
    Uri result;
    string str1;
    if (Uri.TryCreate(url, UriKind.Absolute, out result) && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps))
    {
      if (!UrlExtensions.TryResolveExternalUrl(url, out string _))
        throw new ArgumentException("External URL has been specified.", nameof (url));
      str1 = result.PathAndQuery;
    }
    else
    {
      str1 = Uri.TryCreate(url, UriKind.Relative, out result) ? url : throw new ArgumentException("Path for converting to relative url is in incorrect format.");
      int startIndex = url.IndexOf('/');
      if (!char.IsLetter(url[0]) && startIndex > 0)
        str1 = url.Substring(startIndex);
    }
    int num = str1.IndexOf('?');
    List<KeyValuePair<string, string>> keyValuePairList = new List<KeyValuePair<string, string>>();
    if (num > -1)
    {
      keyValuePairList = QueryHelpers.ParseQuery(str1.Substring(num)).SelectMany<KeyValuePair<string, StringValues>, string, KeyValuePair<string, string>>(UrlExtensions.\u0002.\u000E ?? (UrlExtensions.\u0002.\u000E = new Func<KeyValuePair<string, StringValues>, IEnumerable<string>>(UrlExtensions.\u0002.\u0002.\u0002)), UrlExtensions.\u0002.\u0006 ?? (UrlExtensions.\u0002.\u0006 = new Func<KeyValuePair<string, StringValues>, string, KeyValuePair<string, string>>(UrlExtensions.\u0002.\u0002.\u0002))).ToList<KeyValuePair<string, string>>();
      if (ignoreSystemParams)
        keyValuePairList = keyValuePairList.\u0002(UrlExtensions.SystemParams).ToList<KeyValuePair<string, string>>();
      str1 = str1.Substring(0, num);
    }
    if (queryParamsToAdd != null && queryParamsToAdd.Any<KeyValuePair<string, string>>())
      keyValuePairList = keyValuePairList.\u0002(queryParamsToAdd.Select<KeyValuePair<string, string>, KeyValuePair<string, string>>(UrlExtensions.\u0002.\u0008 ?? (UrlExtensions.\u0002.\u0008 = new Func<KeyValuePair<string, string>, KeyValuePair<string, string>>(UrlExtensions.\u0002.\u0002.\u0002)))).ToList<KeyValuePair<string, string>>();
    QueryString queryString1 = new QueryBuilder((IEnumerable<KeyValuePair<string, string>>) keyValuePairList).ToQueryString();
    string str2 = basePath == null ? str1 : StringExtensions.TrimStart(str1, basePath);
    int startIndex1 = str2.IndexOf("(W", StringComparison.Ordinal);
    if (startIndex1 >= 0)
      str2 = str2.Substring(str2.IndexOf('/', startIndex1));
    PathString pathString1 = UrlExtensions.\u0002(str2);
    QueryString queryString2 = queryString1;
    PathString pathString2 = new PathString();
    QueryString queryString3 = queryString2;
    FragmentString fragmentString = new FragmentString();
    return Uri.UnescapeDataString(UriHelper.BuildRelative(pathString1, pathString2, queryString3, fragmentString));
  }

  public static string? TrimQueryParams(this string? url)
  {
    if (url == null)
      return (string) null;
    int length = url.IndexOf("?");
    return length <= 0 ? url : url.Substring(0, length);
  }

  public static bool TryResolveExternalUrl(string uriString, out string resultPath)
  {
    ExceptionExtensions.ThrowOnNull<string>(uriString, nameof (uriString), (string) null);
    resultPath = uriString;
    if (uriString.IsAbsoluteUrl())
    {
      Uri uri = new Uri(uriString);
      if (uri.HostNameType == UriHostNameType.Dns && !string.Equals(uri.DnsSafeHost, "localhost", StringComparison.OrdinalIgnoreCase) && !string.Equals(uri.DnsSafeHost, Dns.GetHostName(), StringComparison.InvariantCultureIgnoreCase))
        return false;
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
          return false;
      }
      resultPath = uri.LocalPath;
    }
    resultPath = UriHelper.BuildRelative(new PathString(), UrlExtensions.\u0002(resultPath), new QueryString(), new FragmentString());
    return true;
  }

  private static PathString \u0002(string _param0)
  {
    return PathString.op_Implicit("/" + _param0.TrimStart('/'));
  }

  private static IEnumerable<KeyValuePair<string, string?>> \u0002(
    this IEnumerable<KeyValuePair<string, string?>> _param0,
    string[] _param1)
  {
    return _param0.Where<KeyValuePair<string, string>>(new Func<KeyValuePair<string, string>, bool>(new UrlExtensions.\u000E()
    {
      \u0002 = _param1
    }.\u0002));
  }

  private static IEnumerable<KeyValuePair<string, string?>> \u0002(
    this IEnumerable<KeyValuePair<string, string?>> _param0,
    IEnumerable<KeyValuePair<string, string?>> _param1)
  {
    return _param0.Union<KeyValuePair<string, string>>(_param1);
  }

  internal static bool IsAbsoluteUrl(this string _param0)
  {
    return _param0.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || _param0.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
  }

  internal static bool IsExternalUrl(this string _param0)
  {
    return !UrlExtensions.TryResolveExternalUrl(_param0, out string _);
  }

  [return: NotNullIfNotNull("queryStringPart")]
  internal static string? EncodeQueryStringPart(this string? _param0)
  {
    return Str.IsNullOrEmpty(_param0) ? _param0 : Uri.EscapeDataString(_param0);
  }

  [Serializable]
  private sealed class \u0002
  {
    public static readonly 
    #nullable disable
    UrlExtensions.\u0002 \u0002 = new UrlExtensions.\u0002();
    public static Func<KeyValuePair<string, StringValues>, IEnumerable<string>> \u000E;
    public static Func<KeyValuePair<string, StringValues>, string, KeyValuePair<string, string>> \u0006;
    public static Func<KeyValuePair<string, string>, KeyValuePair<string, string>> \u0008;

    internal IEnumerable<string> \u0002(KeyValuePair<string, StringValues> _param1)
    {
      return (IEnumerable<string>) (object) _param1.Value;
    }

    internal KeyValuePair<string, string> \u0002(
      KeyValuePair<string, StringValues> _param1,
      string _param2)
    {
      return new KeyValuePair<string, string>(_param1.Key, _param2);
    }

    internal KeyValuePair<string, string> \u0002(KeyValuePair<string, string> _param1)
    {
      return new KeyValuePair<string, string>(_param1.Key, _param1.Value);
    }
  }

  private sealed class \u000E
  {
    public 
    #nullable enable
    string[] \u0002;

    internal bool \u0002(KeyValuePair<
    #nullable disable
    string, string> _param1) => !((IEnumerable<string>) this.\u0002).Contains<string>(_param1.Key);
  }
}
