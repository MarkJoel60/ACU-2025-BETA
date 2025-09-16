// Decompiled with JetBrains decompiler
// Type: PX.Common.Session.PXSessionStateTyped
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

#nullable enable
namespace PX.Common.Session;

internal static class PXSessionStateTyped
{
  internal static TValue? Get<TValue>(this PXSessionState _param0, SessionKey<TValue> _param1) where TValue : class
  {
    IPXSessionState inner = _param0.Inner;
    return inner == null ? default (TValue) : inner.Get<TValue>(_param1);
  }

  internal static void Set<TValue>(
    this PXSessionState _param0,
    SessionKey<TValue> _param1,
    TValue _param2)
  {
    IPXSessionState inner = _param0.Inner;
    if (inner == null)
      return;
    inner.Set<TValue>(_param1, _param2);
  }

  internal static void Remove<TValue>(this PXSessionState _param0, SessionKey<TValue> _param1)
  {
    IPXSessionState inner = _param0.Inner;
    if (inner == null)
      return;
    inner.Remove<TValue>(_param1);
  }
}
