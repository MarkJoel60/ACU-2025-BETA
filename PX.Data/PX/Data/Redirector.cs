// Decompiled with JetBrains decompiler
// Type: PX.Data.Redirector
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using PX.Common;
using PX.Data.Utility;
using PX.Owin;
using System;
using System.Web;

#nullable disable
namespace PX.Data;

[Obsolete]
[PXInternalUseOnly]
public static class Redirector
{
  public static void Redirect(HttpContext context, string url, bool bypassApiCheck = false)
  {
    if (!bypassApiCheck && (Redirector.IsApi(context) || context == null))
      return;
    bool flag = false;
    try
    {
      url = PXUrl.ToAbsoluteUrl(url);
    }
    catch (ArgumentException ex)
    {
      flag = true;
    }
    if (!flag)
      url = PXSessionStateStore.GetSessionUrl(context, url);
    context.Response.Redirect(url);
  }

  public static void RedirectPage(HttpContext context, string url)
  {
    Redirector.RedirectPage(context, url, "_top");
  }

  public static void RedirectPage(HttpContext context, string url, string target)
  {
    if (Redirector.IsApi(context))
      return;
    bool flag = false;
    try
    {
      url = PXUrl.ToAbsoluteUrl(url);
    }
    catch (ArgumentException ex)
    {
      flag = true;
    }
    if (!flag)
      url = PXSessionStateStore.GetSessionUrl(context, url);
    PXTrace.WriteInformation("Redirecting to main page via window.open");
    context.Response.Clear();
    context.Response.Write("<script>");
    context.Response.Write($"window.open(\"{url}\",\"{target}\");");
    context.Response.Write("</script>");
    context.Response.Cache.SetNoStore();
    context.Response.Cache.SetNoServerCaching();
  }

  public static void DumbRedirect(HttpContext context, string getUrl, string callbackUrl)
  {
    int num = context.Request.Form["__CALLBACKID"] != null ? 1 : 0;
    bool flag1 = string.Equals(context.Request.HttpMethod, "GET", StringComparison.InvariantCultureIgnoreCase);
    bool flag2 = string.Equals(context.Request.HttpMethod, "POST", StringComparison.InvariantCultureIgnoreCase);
    if (num != 0)
    {
      context.Response.Clear();
      context.Response.Write("eRedirect:" + callbackUrl);
      context.Response.End();
    }
    if (!(flag1 | flag2) || Redirector.IsApi(context))
      return;
    bool flag3 = false;
    try
    {
      getUrl = PXUrl.ToAbsoluteUrl(getUrl);
    }
    catch (ArgumentException ex)
    {
      flag3 = true;
    }
    if (!flag3)
      getUrl = PXSessionStateStore.GetSessionUrl(context, getUrl);
    PXTrace.WriteInformation("Redirecting to main page via window.open");
    context.Response.Clear();
    context.Response.Write("<script>");
    context.Response.Write($"window.open(\"{getUrl}\");");
    context.Response.Write("</script>");
    context.Response.Cache.SetNoStore();
    context.Response.Cache.SetNoServerCaching();
    Redirector.RedirectPage(HttpContext.Current, getUrl);
    context.Response.End();
  }

  public static void RefreshPage(HttpContext context, string url)
  {
    if (Redirector.IsApi(context))
      return;
    try
    {
      url = PXUrl.ToAbsoluteUrl(url);
    }
    catch (ArgumentException ex)
    {
    }
    context.Response.Clear();
    context.Response.Write($"eRefresh|{url}|");
  }

  public static void Refresh(HttpContext context)
  {
    if (Redirector.IsApi(context))
      return;
    if (context.IsModernUIRequest())
      throw new PXReloadPageException();
    context.Response.Clear();
    context.Response.Write("eRefresh");
  }

  public static void SmartRedirect(HttpContext context, string url)
  {
    int num = context.Request.Form["__CALLBACKID"] != null ? 1 : 0;
    bool flag1 = string.Equals(context.Request.HttpMethod, "GET", StringComparison.InvariantCultureIgnoreCase);
    bool flag2 = string.Equals(context.Request.HttpMethod, "POST", StringComparison.InvariantCultureIgnoreCase);
    if (num != 0)
    {
      context.Response.Clear();
      context.Response.Write("eRedirect:" + url);
      context.Response.End();
    }
    if (!(flag1 | flag2))
      return;
    Redirector.RedirectPage(HttpContext.Current, url);
    context.Response.End();
  }

  public static bool IsMobileApi(HttpContextBase context)
  {
    try
    {
      return context.Request.RawUrl.ToLowerInvariant().Contains("/rest/");
    }
    catch
    {
      return false;
    }
  }

  public static bool IsMobileApi(HttpContext context)
  {
    try
    {
      return context.Request.RawUrl.ToLowerInvariant().Contains("/rest/");
    }
    catch
    {
      return false;
    }
  }

  internal static bool IsApi(HttpContext httpContext)
  {
    return httpContext.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata?.GetMetadata<ApiEndpoint>() != null;
  }

  public static bool IsApi(HttpContext context)
  {
    if (!ServiceLocator.IsLocationProviderSet)
      return false;
    IHttpContextAccessor instance = ServiceLocator.Current.GetInstance<IHttpContextAccessor>();
    if (instance == null)
      return false;
    try
    {
      return Redirector.IsApi(instance.HttpContext);
    }
    catch
    {
      return false;
    }
  }
}
