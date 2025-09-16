// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Async.IAsyncQueryProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree.Linq.Async;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Data.SQLTree.Async;

internal interface IAsyncQueryProvider
{
  ValueTask<object> ExecuteAsync<TResult>(
    Expression expression,
    CancellationToken cancellationToken);

  IAsyncEnumerable<TResult> ExecuteCollectionAsync<TResult>(
    Expression expression,
    CancellationToken cancellationToken);

  IAsyncQueryable<TResult> CreateAsyncQuery<TResult>(Expression expression);
}
