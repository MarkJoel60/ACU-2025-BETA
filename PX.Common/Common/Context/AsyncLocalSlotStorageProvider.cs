// Decompiled with JetBrains decompiler
// Type: PX.Common.Context.AsyncLocalSlotStorageProvider
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace PX.Common.Context;

internal class AsyncLocalSlotStorageProvider : 
  SlotStorageProviderBase,
  IDisposableSlotStorageProvider,
  ISlotStorageProvider,
  ISlotStore,
  IDisposable
{
  private static readonly AsyncLocal<AsyncLocalSlotStorageProvider> \u0002 = new AsyncLocal<AsyncLocalSlotStorageProvider>();
  private readonly AsyncLocalSlotStorageProvider \u000E;
  private readonly ConcurrentDictionary<string, object> \u0006;

  private AsyncLocalSlotStorageProvider(AsyncLocalSlotStorageProvider _param1)
  {
    this.\u0006 = new ConcurrentDictionary<string, object>();
    this.\u000E = _param1;
  }

  internal static SlotStorageProviderBase TryGet()
  {
    return (SlotStorageProviderBase) AsyncLocalSlotStorageProvider.\u0002.Value;
  }

  internal static IDisposableSlotStorageProvider Activate()
  {
    AsyncLocalSlotStorageProvider slotStorageProvider = new AsyncLocalSlotStorageProvider(AsyncLocalSlotStorageProvider.\u0002.Value);
    AsyncLocalSlotStorageProvider.\u0002.Value = slotStorageProvider;
    return (IDisposableSlotStorageProvider) slotStorageProvider;
  }

  protected sealed override object Get(string _param1)
  {
    object obj;
    return !this.\u0006.TryGetValue(_param1, out obj) ? (object) null : obj;
  }

  protected sealed override void Set(string _param1, object _param2)
  {
    this.\u0006[_param1] = _param2;
  }

  protected sealed override void Remove(string _param1)
  {
    this.\u0006.TryRemove(_param1, out object _);
  }

  protected sealed override IEnumerable<KeyValuePair<string, object>> Items()
  {
    return (IEnumerable<KeyValuePair<string, object>>) this.\u0006.ToArray();
  }

  protected sealed override void Clear() => this.\u0006.Clear();

  void IDisposable.zrql29uzws7uqlz22275xdn3q7flhvrd\u2009\u2009\u2009\u0002()
  {
    AsyncLocalSlotStorageProvider.\u0002.Value = this.\u000E;
    this.\u0006.Clear();
  }
}
