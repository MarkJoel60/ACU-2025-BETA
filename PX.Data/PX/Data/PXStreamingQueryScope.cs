// Decompiled with JetBrains decompiler
// Type: PX.Data.PXStreamingQueryScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// The query scope that is used to execute RowSelecting graph event handlers
/// during iteration throw records. The query scope is used in streaming scenarios
/// of data reading, such as in push notifications, OData Version 4, and generic inquiries.
/// </summary>
public sealed class PXStreamingQueryScope : IDisposable
{
  private const string ScopeKeyPrefix = "PXStreamingQueryScope";
  private readonly bool _previousIsScoped;
  private bool _isDisposed;

  public PXStreamingQueryScope(bool isScoped = true)
  {
    this._previousIsScoped = PXStreamingQueryScope.IsScoped;
    PXStreamingQueryScope.IsScoped = isScoped;
  }

  public static bool IsScoped
  {
    get => PXContext.GetSlot<bool>("PXStreamingQueryScope.IsScoped");
    set => PXContext.SetSlot<bool>("PXStreamingQueryScope.IsScoped", value);
  }

  public void Dispose()
  {
    if (this._isDisposed)
      return;
    PXStreamingQueryScope.IsScoped = this._previousIsScoped;
    this._isDisposed = true;
  }
}
