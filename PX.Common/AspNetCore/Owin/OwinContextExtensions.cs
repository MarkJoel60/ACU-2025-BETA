// Decompiled with JetBrains decompiler
// Type: PX.AspNetCore.Owin.OwinContextExtensions
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.AspNetCore.Owin;

internal static class OwinContextExtensions
{
  private static readonly string \u0002 = typeof (OwinContextExtensions).FullName + ".HttpContext";

  internal static void SetHttpContext(this IDictionary<string, object> _param0, HttpContext _param1)
  {
    _param0.Add(OwinContextExtensions.\u0002, (object) _param1);
  }

  internal static HttpContext GetHttpContext(this IDictionary<string, object> _param0)
  {
    object obj;
    if (!_param0.TryGetValue(OwinContextExtensions.\u0002, out obj))
      return (HttpContext) null;
    if (obj is HttpContext httpContext)
      return httpContext;
    if (obj == null)
      return (HttpContext) null;
    throw new InvalidOperationException($"Unexpected value type {obj.GetType().FullName} for key {OwinContextExtensions.\u0002}");
  }
}
