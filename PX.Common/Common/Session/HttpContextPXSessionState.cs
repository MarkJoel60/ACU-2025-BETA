// Decompiled with JetBrains decompiler
// Type: PX.Common.Session.HttpContextPXSessionState
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Collections;
using System.Web;

#nullable enable
namespace PX.Common.Session;

internal static class HttpContextPXSessionState
{
  private static readonly Type \u0002 = typeof (IPXSessionState);

  internal static IPXSessionState? TryGet(HttpContext? _param0)
  {
    return _param0 == null ? (IPXSessionState) null : HttpContextPXSessionState.Get(_param0);
  }

  internal static IPXSessionState? Get(HttpContext _param0)
  {
    IPXSessionState pxSessionState = HttpContextPXSessionState.Get(_param0.Items);
    if (pxSessionState != null)
      return pxSessionState;
    if (_param0.Session != null)
      throw new InvalidOperationException("Session is present, but IPXSessionState is not set in the HttpContext");
    return (IPXSessionState) null;
  }

  internal static IPXSessionState? Get(HttpContextBase _param0)
  {
    IPXSessionState pxSessionState = HttpContextPXSessionState.Get(_param0.Items);
    if (pxSessionState != null)
      return pxSessionState;
    if (_param0.Session != null)
      throw new InvalidOperationException("Session is present, but IPXSessionState is not set in the HttpContext");
    return (IPXSessionState) null;
  }

  internal static IPXSessionState? Get(IDictionary _param0)
  {
    object obj = _param0[(object) HttpContextPXSessionState.\u0002];
    if (obj == null)
      return (IPXSessionState) null;
    return obj is IPXSessionState pxSessionState ? pxSessionState : throw new InvalidOperationException($"Unexpected type {obj.GetType()} for key {HttpContextPXSessionState.\u0002}");
  }

  internal static void Set(HttpContext _param0, IPXSessionState _param1)
  {
    _param0.Items.Add((object) HttpContextPXSessionState.\u0002, (object) _param1);
  }

  internal static void Remove(HttpContext _param0)
  {
    _param0.Items.Remove((object) HttpContextPXSessionState.\u0002);
  }
}
