// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Async.AsyncSQLQueryProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree.Linq.Async;
using Remotion.Linq;
using Remotion.Linq.Clauses.StreamedData;
using Remotion.Linq.Parsing.Structure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Data.SQLTree.Async;

/// <inheritdoc />
internal class AsyncSQLQueryProvider(
  System.Type queryableType,
  IQueryParser queryParser,
  AsyncSQLinqExecutor executor) : SQLQueryProvider(queryableType, queryParser, (IQueryExecutor) executor), IAsyncQueryProvider
{
  public IAsyncQueryable<T> CreateAsyncQuery<T>(Expression expression)
  {
    return (IAsyncQueryable<T>) Activator.CreateInstance(this.QueryableType.MakeGenericType(typeof (T)), (object) this, (object) expression);
  }

  /// <inheritdoc />
  public IAsyncEnumerable<TResult> ExecuteCollectionAsync<TResult>(
    Expression expression,
    CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();
    if (expression == null)
      throw new ArgumentNullException(nameof (expression));
    if (this.Executor is SQLinqExecutor executor)
      executor.CurrentExpression = expression;
    return ((AsyncSQLinqExecutor) this.Executor).ExecuteCollectionAsync<TResult>(this.GenerateQueryModel(expression), cancellationToken);
  }

  /// <inheritdoc />
  public ValueTask<object> ExecuteAsync<TResult>(
    Expression expression,
    CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();
    ValueTask<object> result;
    this.ExecuteQueryModel(expression, out result, cancellationToken);
    return result;
  }

  public override IStreamedData Execute(Expression expression)
  {
    ValueTask<object> result;
    IStreamedDataInfo streamedDataInfo = this.ExecuteQueryModel(expression, out result, new CancellationToken());
    object enumerable = !result.IsCompleted ? result.GetAwaiter().GetResult() : result.Result;
    return streamedDataInfo is StreamedSequenceInfo ? (IStreamedData) new StreamedAsyncValue((object) new AsyncEnumerableQuery<object>(enumerable as IAsyncEnumerable<object>), streamedDataInfo) : (IStreamedData) new StreamedAsyncValue(enumerable, streamedDataInfo);
  }

  private IStreamedDataInfo ExecuteQueryModel(
    Expression expression,
    out ValueTask<object> result,
    CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();
    if (expression == null)
      throw new ArgumentNullException(nameof (expression));
    if (this.Executor is SQLinqExecutor executor)
      executor.CurrentExpression = expression;
    QueryModel queryModel = this.GenerateQueryModel(expression);
    IStreamedDataInfo outputDataInfo = queryModel.GetOutputDataInfo();
    result = this.ExecuteQueryModelAsync<object>(outputDataInfo, queryModel, this.Executor as IAsyncQueryExecutor, cancellationToken);
    return outputDataInfo;
  }

  private async ValueTask<object> ExecuteQueryModelAsync<TResult>(
    IStreamedDataInfo info,
    QueryModel queryModel,
    IAsyncQueryExecutor executor,
    CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();
    if (executor == null)
      throw new ArgumentNullException(nameof (executor));
    switch (info)
    {
      case StreamedScalarValueInfo _:
        return (object) await executor.ExecuteScalarAsync<TResult>(queryModel, cancellationToken);
      case StreamedSingleValueInfo streamedSingleValueInfo:
        return (object) await executor.ExecuteSingleAsync<TResult>(queryModel, streamedSingleValueInfo.ReturnDefaultWhenEmpty, cancellationToken);
      case StreamedSequenceInfo _:
        return (object) executor.ExecuteCollectionAsync<TResult>(queryModel, cancellationToken);
      default:
        throw new ArgumentException(info.GetType().FullName ?? "");
    }
  }
}
