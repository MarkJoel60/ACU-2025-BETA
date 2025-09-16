// Decompiled with JetBrains decompiler
// Type: PX.Common.Extensions.HttpResponseExtensions
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Web;

#nullable disable
namespace PX.Common.Extensions;

public static class HttpResponseExtensions
{
  public static ISubscriptionToken AddOnSendingHeadersWhenContextPresent(
    this HttpResponse response,
    Action<HttpContext> callback)
  {
    return response.AddOnSendingHeaders(new Action<HttpContext>(new HttpResponseExtensions.\u0002()
    {
      \u0002 = callback
    }.\u0002));
  }

  public static ISubscriptionToken AddOnSendingHeadersWhenContextPresent(
    this HttpResponse response,
    Action<HttpResponse> callback)
  {
    return response.AddOnSendingHeadersWhenContextPresent(new Action<HttpContext>(new HttpResponseExtensions.\u000E()
    {
      \u0002 = callback
    }.\u0002));
  }

  public static void AddOnSendingHeadersIfHeadersNotWritten(
    this HttpResponse response,
    Action<HttpContext> callback)
  {
    if (response.HeadersWritten)
      return;
    response.AddOnSendingHeadersWhenContextPresent(callback);
  }

  private sealed class \u0002
  {
    public Action<HttpContext> \u0002;

    internal void \u0002(HttpContext _param1)
    {
      if (_param1 == null)
        return;
      this.\u0002(_param1);
    }
  }

  private sealed class \u000E
  {
    public Action<HttpResponse> \u0002;

    internal void \u0002(HttpContext _param1) => this.\u0002(_param1.Response);
  }
}
