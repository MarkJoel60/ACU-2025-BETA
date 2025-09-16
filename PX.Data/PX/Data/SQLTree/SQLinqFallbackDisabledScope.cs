// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLinqFallbackDisabledScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data.SQLTree;

internal sealed class SQLinqFallbackDisabledScope : IDisposable
{
  private const string ScopeKeyPrefix = "SQLinqFallbackDisabledScope";
  private readonly bool _previousIsScoped;
  private bool _isDisposed;

  public SQLinqFallbackDisabledScope(bool isScoped = true)
  {
    this._previousIsScoped = SQLinqFallbackDisabledScope.IsScoped;
    SQLinqFallbackDisabledScope.IsScoped = isScoped;
  }

  public static bool IsScoped
  {
    get => PXContext.GetSlot<bool>("SQLinqFallbackDisabledScope.IsScoped");
    set => PXContext.SetSlot<bool>("SQLinqFallbackDisabledScope.IsScoped", value);
  }

  public void Dispose()
  {
    if (this._isDisposed)
      return;
    SQLinqFallbackDisabledScope.IsScoped = this._previousIsScoped;
    this._isDisposed = true;
  }
}
