// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Scopes.BaseCounterScope
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Objects.Common.Scopes;

public abstract class BaseCounterScope : IDisposable
{
  private readonly string _scopeKey;

  protected BaseCounterScope(string scopeKey)
  {
    this._scopeKey = scopeKey;
    this.Inc();
  }

  void IDisposable.Dispose() => this.Dec();

  protected virtual int Inc() => PXContext.SetSlot<int>(this._scopeKey, this.GetCounter() + 1);

  protected virtual int Dec()
  {
    int counter = this.GetCounter();
    if (counter != 1)
      return PXContext.SetSlot<int>(this._scopeKey, counter - 1);
    PXContext.ClearSlot(this._scopeKey);
    return 0;
  }

  public int GetCounter() => BaseCounterScope.GetCounter(this._scopeKey);

  protected static int GetCounter(string scopeKey) => PXContext.GetSlot<int>(scopeKey);

  protected static bool IsEmpty(string scopeKey) => BaseCounterScope.GetCounter(scopeKey) == 0;
}
