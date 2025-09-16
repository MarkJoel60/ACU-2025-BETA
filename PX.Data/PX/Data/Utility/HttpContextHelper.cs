// Decompiled with JetBrains decompiler
// Type: PX.Data.Utility.HttpContextHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.AspNetCore;
using PX.AspNetCore.RouteConstraints;
using PX.Common;
using System;
using System.Text;
using System.Web;

#nullable disable
namespace PX.Data.Utility;

public static class HttpContextHelper
{
  public static bool IsModernUIRequest(this HttpContext context)
  {
    return context.GetCoreHttpContext().Features.Get<IScreenIdProvider>() != null;
  }

  public static string GetAbsoluteApplicationPath(this HttpContext context)
  {
    string applicationPath = context.Request.ApplicationPath;
    return !string.IsNullOrEmpty(applicationPath) && applicationPath != "/" ? context.Request.Url.AbsoluteUri.Substring(0, context.Request.Url.AbsoluteUri.IndexOf(applicationPath, StringComparison.Ordinal)) + applicationPath : context.Request.Url.AbsoluteUri.Substring(0, context.Request.Url.AbsoluteUri.IndexOf(context.Request.Path, StringComparison.Ordinal));
  }

  internal static string GetScreenRelativeUrl(this HttpContext context, bool withParams = false)
  {
    HttpRequest request = context.Request;
    Uri urlReferrer = request.UrlReferrer;
    if (!context.IsModernUIRequest() || !(urlReferrer != (Uri) null))
      return (!withParams ? request.Path : request.RawUrl).ToRelativeUrl(request.ApplicationPath);
    string relativeUrl = (!withParams ? urlReferrer.AbsolutePath : urlReferrer.PathAndQuery).ToRelativeUrl(request.ApplicationPath);
    if (relativeUrl.IndexOf("/Scripts/Screens", StringComparison.Ordinal) >= 0)
    {
      int startIndex = relativeUrl.LastIndexOf('/');
      if (!MemoryExtensions.AsSpan(relativeUrl).Slice("/Scripts/Screens".Length, startIndex - "/Scripts/Screens".Length).IsEmpty)
        return "/Scripts/Screens" + relativeUrl.Substring(startIndex);
    }
    return relativeUrl;
  }

  private static StringBuilder _javaScript
  {
    get
    {
      StringBuilder javaScript = (StringBuilder) HttpContext.Current.Items[(object) nameof (_javaScript)];
      if (javaScript == null)
        HttpContext.Current.Items[(object) nameof (_javaScript)] = (object) (javaScript = new StringBuilder());
      return javaScript;
    }
  }

  internal static StringBuilder GetJavaScript() => HttpContextHelper._javaScript;

  /// <summary>
  /// Register Java script which will be executed in browser.
  /// </summary>
  public static void RegisterJavaScript(string script)
  {
    HttpContextHelper._javaScript.Append(script);
  }
}
