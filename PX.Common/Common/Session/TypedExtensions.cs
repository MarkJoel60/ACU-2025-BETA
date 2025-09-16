// Decompiled with JetBrains decompiler
// Type: PX.Common.Session.TypedExtensions
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Common.Session;

internal static class TypedExtensions
{
  internal static TValue? Get<TValue>(this IPXSessionState _param0, SessionKey<TValue> _param1) where TValue : class
  {
    return (TValue) _param0.Get(_param1.Key);
  }

  internal static bool TryGet<TValue>(
    this IPXSessionState _param0,
    SessionKey<TValue> _param1,
    [NotNullWhen(true)] out TValue? _param2)
  {
    object obj;
    if (_param0.TryGetValue(_param1.Key, out obj))
    {
      _param2 = (TValue) obj;
      return true;
    }
    _param2 = default (TValue);
    return false;
  }

  internal static void Set<TValue>(
    this IPXSessionState _param0,
    SessionKey<TValue> _param1,
    TValue _param2)
  {
    _param0.Set(_param1.Key, (object) _param2);
  }

  internal static void Remove<TValue>(this IPXSessionState _param0, SessionKey<TValue> _param1)
  {
    _param0.Remove(_param1.Key);
  }

  internal static bool Contains<TValue>(this IPXSessionState _param0, SessionKey<TValue> _param1)
  {
    return _param0.Contains(_param1.Key);
  }
}
