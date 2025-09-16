// Decompiled with JetBrains decompiler
// Type: PX.Common.SessionContextFactoryExtensions
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Web;

#nullable disable
namespace PX.Common;

[PXInternalUseOnly]
public static class SessionContextFactoryExtensions
{
  internal static void PersistNewIdentity(
    this ISessionContextFactory _param0,
    HttpContextBase _param1)
  {
    PXSessionContextFactory.TempSessionBucketToSessionBucket(_param1);
  }

  [PXInternalUseOnly]
  public static void EndRequest(
    this ISessionContextFactory sessionContextFactory,
    HttpContextBase httpContext)
  {
    if (httpContext.Items.Contains((object) "ISessionContextFactory.SaveRequestToSessionMarker"))
      return;
    httpContext.Items.Add((object) "ISessionContextFactory.SaveRequestToSessionMarker", (object) null);
    sessionContextFactory.SaveRequestToSession(httpContext);
  }

  [Obsolete("Pass HttpContext explicitly")]
  internal static void Abandon(this ISessionContextFactory _param0)
  {
    HttpContext current = HttpContext.Current;
    if (current == null)
      return;
    _param0.Abandon(current);
  }

  internal static void Abandon(this ISessionContextFactory _param0, HttpContext _param1)
  {
    _param0.Abandon(_param1.GetContextBase());
  }
}
