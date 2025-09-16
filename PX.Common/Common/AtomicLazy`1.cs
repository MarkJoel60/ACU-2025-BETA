// Decompiled with JetBrains decompiler
// Type: PX.Common.AtomicLazy`1
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Threading;

#nullable enable
namespace PX.Common;

/// <summary>ThreadSafeValueOnly Lazy, doesn't cache exceptions</summary>
[PXInternalUseOnly]
public class AtomicLazy<T>
{
  private readonly Func<T> \u0002;
  private T? \u000E;
  private bool \u0006;
  private object? \u0008;

  public AtomicLazy(Func<T> factory) => this.\u0002 = factory;

  public T Value
  {
    get
    {
      return LazyInitializer.EnsureInitialized<T>(ref this.\u000E, ref this.\u0006, ref this.\u0008, this.\u0002);
    }
  }
}
