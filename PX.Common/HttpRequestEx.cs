// Decompiled with JetBrains decompiler
// Type: HttpRequestEx
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using PX.Common;
using PX.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

#nullable enable
public static class HttpRequestEx
{
  /// <summary>
  /// Returns url for current website in format http(s)://hostname/
  /// This url can be used in external links.
  /// </summary>
  /// <param name="request">
  /// </param>
  /// <returns>
  /// </returns>
  public static string GetWebsiteUrl(this HttpRequest request)
  {
    HttpRequestEx.\u000E obj = new HttpRequestEx.\u000E();
    obj.\u0002 = request;
    return PXUrl.GetWebsiteUrl(obj.\u0002.Url, new Func<string, string>(obj.\u0002));
  }

  /// <summary>
  /// Returns scheme and hostname for current website.
  /// This url can be used in external links.
  /// </summary>
  /// <param name="request">
  /// </param>
  /// <returns>
  /// </returns>
  public static Uri GetWebsiteAuthority(this HttpRequest request)
  {
    return new Uri(request.GetWebsiteUrl());
  }

  public static Uri GetExternalUrl(this HttpRequest request) => request.Url.FixAuthority(request);

  public static Uri FixAuthority(this Uri uri, HttpRequest request)
  {
    string pathAndQuery = uri.PathAndQuery;
    return new Uri(request.GetWebsiteAuthority(), pathAndQuery);
  }

  public static string GetUserHostAddress(this HttpRequestBase request)
  {
    string userHostAddress = request.Headers["X-UserHostAddress"];
    if (Str.IsNullOrEmpty(userHostAddress))
      userHostAddress = HttpRequestEx.\u0002(request).First<string>();
    return userHostAddress;
  }

  public static string GetUserHostAddress(this HttpRequest request)
  {
    return new HttpRequestWrapper(request).GetUserHostAddress();
  }

  public static IEnumerable<string> GetUserHostsChain(this HttpRequest request)
  {
    return new HttpRequestWrapper(request).GetUserHostsChain();
  }

  public static IEnumerable<string> GetUserHostsChain(this HttpRequestBase request)
  {
    string header = request.Headers["X-UserHostAddress"];
    if (Str.IsNullOrEmpty(header))
      return HttpRequestEx.\u0002(request);
    return (IEnumerable<string>) new string[1]{ header };
  }

  [return: NotNullIfNotNull("host")]
  internal static string? RemovePort(string? _param0)
  {
    if (Str.IsNullOrWhiteSpace(_param0))
      return _param0;
    if (_param0.Count<char>(HttpRequestEx.\u0002.\u000E ?? (HttpRequestEx.\u0002.\u000E = new Func<char, bool>(HttpRequestEx.\u0002.\u0002.\u0002))) <= 1)
      return StringExtensions.FirstSegment(_param0, ':');
    if (_param0[0] == '[')
    {
      int num = _param0.LastIndexOf(']');
      if (num > 0 && num + 1 < _param0.Length - 1 && _param0[num + 1] == ':')
        return _param0.Substring(0, num + 1);
    }
    return _param0;
  }

  private static IEnumerable<string> \u0002(HttpRequestBase _param0)
  {
    string[] strArray = new string[5]
    {
      "HTTP_X_FORWARDED",
      "HTTP_X_FORWARDED_FOR",
      "HTTP_FORWARDED_FOR",
      "HTTP_FORWARDED",
      "X-Forwarded-For"
    };
    foreach (string name in strArray)
    {
      string header = _param0.Headers[name];
      if (!Str.IsNullOrEmpty(header))
        return ((IEnumerable<string>) header.Split(',')).Select<string, string>(HttpRequestEx.\u0002.\u0006 ?? (HttpRequestEx.\u0002.\u0006 = new Func<string, string>(HttpRequestEx.\u0002.\u0002.\u0002))).Select<string, string>(HttpRequestEx.\u0002.\u0008 ?? (HttpRequestEx.\u0002.\u0008 = new Func<string, string>(HttpRequestEx.\u0002.\u0002.\u000E)));
    }
    return (IEnumerable<string>) new string[1]
    {
      _param0.UserHostAddress
    };
  }

  internal static string SiteUrlWithPath(this HttpRequest _param0)
  {
    HttpRequestEx.\u0006 obj = new HttpRequestEx.\u0006();
    obj.\u0002 = _param0;
    ExceptionExtensions.ThrowOnNull<HttpRequest>(obj.\u0002, "request", (string) null);
    return PXUrl.SiteUrlWithPath(obj.\u0002.Url.Scheme, obj.\u0002.ApplicationPath, new Func<string, string>(obj.\u0002));
  }

  internal static HttpContextBase GetContextBase(this HttpContext _param0)
  {
    return _param0.Request.RequestContext.HttpContext;
  }

  internal static HttpRequestBase GetRequestBase(this HttpContext _param0)
  {
    return _param0.GetContextBase().Request;
  }

  [Serializable]
  private sealed class \u0002
  {
    public static readonly 
    #nullable disable
    HttpRequestEx.\u0002 \u0002 = new HttpRequestEx.\u0002();
    public static Func<char, bool> \u000E;
    public static Func<string, string> \u0006;
    public static Func<string, string> \u0008;

    internal bool \u0002(char _param1) => _param1 == ':';

    internal string \u0002(string _param1) => _param1.Trim();

    internal string \u000E(string _param1) => HttpRequestEx.RemovePort(_param1);
  }

  private sealed class \u0006
  {
    public HttpRequest \u0002;

    internal 
    #nullable enable
    string \u0002(string _param1) => this.\u0002.Headers[_param1];
  }

  private sealed class \u000E
  {
    public 
    #nullable disable
    HttpRequest \u0002;

    internal 
    #nullable enable
    string \u0002(string _param1) => this.\u0002.Headers[_param1];
  }
}
