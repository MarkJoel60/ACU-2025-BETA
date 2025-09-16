// Decompiled with JetBrains decompiler
// Type: PX.SM.SuppressErpTransactionsScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.SM;

[PXInternalUseOnly]
public class SuppressErpTransactionsScope : IDisposable
{
  private readonly SuppressErpTransactionsScope _parentScope;
  private readonly bool _value;

  public SuppressErpTransactionsScope(bool value)
  {
    this._value = value;
    this._parentScope = PXContext.GetSlot<SuppressErpTransactionsScope>();
    PXContext.SetSlot<SuppressErpTransactionsScope>(this);
  }

  internal static bool IsErpTransactionsSuppressed()
  {
    SuppressErpTransactionsScope slot = PXContext.GetSlot<SuppressErpTransactionsScope>();
    return slot != null && slot._value;
  }

  public void Dispose() => PXContext.SetSlot<SuppressErpTransactionsScope>(this._parentScope);
}
