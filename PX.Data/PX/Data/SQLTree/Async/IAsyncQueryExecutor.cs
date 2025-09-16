// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Async.IAsyncQueryExecutor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Remotion.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Data.SQLTree.Async;

internal interface IAsyncQueryExecutor
{
  ValueTask<T> ExecuteScalarAsync<T>(QueryModel queryModel, CancellationToken cancellationToken);

  ValueTask<T> ExecuteSingleAsync<T>(
    QueryModel queryModel,
    bool returnDefaultWhenEmpty,
    CancellationToken cancellationToken);

  IAsyncEnumerable<T> ExecuteCollectionAsync<T>(
    QueryModel queryModel,
    CancellationToken cancellationToken);
}
