// Decompiled with JetBrains decompiler
// Type: PX.AspNetCore.HttpContextTranslation
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using Microsoft.AspNetCore.Http;
using Microsoft.Owin;
using PX.AspNetCore.Owin;
using System;
using System.Web;

#nullable disable
namespace PX.AspNetCore;

internal static class HttpContextTranslation
{
  internal static HttpContext GetHttpContext(this IOwinContext _param0)
  {
    return (_param0 != null ? _param0.Environment.GetHttpContext() : throw new ArgumentNullException("owinContext")) ?? throw new InvalidOperationException("Core HttpContext not found");
  }

  internal static HttpContext GetCoreHttpContext(this HttpContextBase _param0)
  {
    return HttpContextBaseExtensions.GetOwinContext(_param0).GetHttpContext();
  }

  internal static HttpContext GetCoreHttpContext(this HttpContext _param0)
  {
    return HttpContextExtensions.GetOwinContext(_param0).GetHttpContext();
  }

  internal static HttpContextBase GetSystemWebHttpContextBase(this HttpContext _param0)
  {
    return _param0.Features.Get<HttpContextBase>() ?? throw new InvalidOperationException("HttpContextBase not present");
  }
}
