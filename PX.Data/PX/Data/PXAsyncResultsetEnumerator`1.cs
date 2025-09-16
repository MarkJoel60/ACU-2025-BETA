// Decompiled with JetBrains decompiler
// Type: PX.Data.PXAsyncResultsetEnumerator`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Data;

internal class PXAsyncResultsetEnumerator<T0> : IAsyncEnumerator<PXResult<T0>>, IAsyncDisposable where T0 : class, IBqlTable, new()
{
  private readonly IAsyncEnumerator<PXResult<T0>> _data;
  private readonly CancellationTokenSource _cancellationTokenSource;
  private readonly CancellationToken _cancellationToken;

  public PXAsyncResultsetEnumerator(
    IAsyncEnumerable<PXResult<T0>> data,
    CancellationTokenSource cancellationTokenSource)
  {
    ExceptionExtensions.ThrowOnNull<CancellationTokenSource>(cancellationTokenSource, nameof (cancellationTokenSource), (string) null);
    this._cancellationTokenSource = cancellationTokenSource;
    this._cancellationToken = this._cancellationTokenSource.Token;
    this._cancellationToken.ThrowIfCancellationRequested();
    this._data = data.GetAsyncEnumerator(this._cancellationToken);
  }

  /// <inheritdoc />
  public ValueTask DisposeAsync()
  {
    this._cancellationTokenSource.Dispose();
    return ((IAsyncDisposable) this._data).DisposeAsync();
  }

  /// <inheritdoc />
  public ValueTask<bool> MoveNextAsync()
  {
    this._cancellationToken.ThrowIfCancellationRequested();
    return AsyncEnumerator.MoveNextAsync<PXResult<T0>>(this._data, this._cancellationToken);
  }

  /// <inheritdoc />
  public PXResult<T0> Current
  {
    get
    {
      this._cancellationToken.ThrowIfCancellationRequested();
      return this._data.Current;
    }
  }
}
