// Decompiled with JetBrains decompiler
// Type: PX.Data.PreEnumeratedAsyncEnumerable`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Data;

/// <summary>
/// Pre-enumerates the first item in the sequence to initialize the source provider.
/// Async copy of <see cref="!:PX.Common.PreEnumeratedEnumerable" />
/// </summary>
internal class PreEnumeratedAsyncEnumerable<T> : IAsyncEnumerable<T>
{
  private readonly Func<IDisposable> _resourceFactory;
  private readonly IAsyncEnumerable<T> _source;

  public PreEnumeratedAsyncEnumerable(Func<IDisposable> resourceFactory, IAsyncEnumerable<T> source)
  {
    this._resourceFactory = resourceFactory;
    this._source = source;
  }

  /// <inheritdoc />
  public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default (CancellationToken))
  {
    return (IAsyncEnumerator<T>) new PreEnumeratedAsyncEnumerable<T>.AsyncEnumerator(this._resourceFactory, this._source, cancellationToken);
  }

  private class AsyncEnumerator : IAsyncEnumerator<T>, IAsyncDisposable
  {
    private readonly Func<IDisposable> _resourceFactory;
    private readonly CancellationToken _cancellationToken;
    private readonly IAsyncEnumerator<T> _source;
    private volatile int iteration;

    public AsyncEnumerator(
      Func<IDisposable> resourceFactory,
      IAsyncEnumerable<T> source,
      CancellationToken cancellationToken)
    {
      this._resourceFactory = resourceFactory;
      this._cancellationToken = cancellationToken;
      this._source = source.GetAsyncEnumerator(cancellationToken);
    }

    /// <inheritdoc />
    public ValueTask DisposeAsync() => ((IAsyncDisposable) this._source).DisposeAsync();

    /// <inheritdoc />
    public async ValueTask<bool> MoveNextAsync()
    {
      this._cancellationToken.ThrowIfCancellationRequested();
      if (this.iteration != 0)
        return await this._source.MoveNextAsync();
      using (this._resourceFactory())
      {
        int num = await this._source.MoveNextAsync() ? 1 : 0;
        ++this.iteration;
        return num != 0;
      }
    }

    /// <inheritdoc />
    public T Current => this._source.Current;
  }
}
