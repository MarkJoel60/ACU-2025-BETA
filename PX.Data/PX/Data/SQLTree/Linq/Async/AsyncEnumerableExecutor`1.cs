// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Linq.Async.AsyncEnumerableExecutor`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Data.SQLTree.Linq.Async;

internal class AsyncEnumerableExecutor<T>
{
  private readonly Expression _expression;
  private Func<CancellationToken, ValueTask<T>> _func;

  /// <summary>
  /// Creates a new execution helper instance for the specified expression tree representing a computation over asynchronous enumerable sequences.
  /// </summary>
  /// <param name="expression">Expression tree representing a computation over asynchronous enumerable sequences.</param>
  public AsyncEnumerableExecutor(Expression expression) => this._expression = expression;

  /// <summary>Evaluated the expression tree.</summary>
  /// <param name="token">Token to cancel the evaluation.</param>
  /// <returns>Task representing the evaluation of the expression tree.</returns>
  internal ValueTask<T> ExecuteAsync(CancellationToken token)
  {
    if (this._func == null)
      this._func = Expression.Lambda<Func<CancellationToken, ValueTask<T>>>(new AsyncEnumerableRewriter().Visit(this._expression), Expression.Parameter(typeof (CancellationToken))).Compile();
    return this._func(token);
  }
}
