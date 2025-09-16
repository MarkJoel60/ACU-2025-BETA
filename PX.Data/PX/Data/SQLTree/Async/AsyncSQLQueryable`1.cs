// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Async.AsyncSQLQueryable`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree.Linq.Async;
using Remotion.Linq.Parsing.ExpressionVisitors.Transformation;
using Remotion.Linq.Parsing.ExpressionVisitors.TreeEvaluation;
using Remotion.Linq.Parsing.Structure;
using Remotion.Linq.Parsing.Structure.NodeTypeProviders;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

#nullable disable
namespace PX.Data.SQLTree.Async;

internal class AsyncSQLQueryable<T> : 
  SQLQueryable<T>,
  IAsyncQueryable<T>,
  IAsyncEnumerable<T>,
  IAsyncQueryable,
  IQueryable<T>,
  IEnumerable<T>,
  IEnumerable,
  IQueryable
{
  public AsyncSQLQueryable(PXGraph graph)
    : this(graph, (IPXResultsetBase) null)
  {
  }

  public AsyncSQLQueryable(PXGraph graph, IPXResultsetBase baseSelect)
    : base(graph, baseSelect, (IQueryProvider) AsyncSQLQueryable<T>.CreateSQLProvider((IQueryParser) AsyncSQLQueryable<T>.CreateParser(), AsyncSQLQueryable<T>.CreateExecutor()))
  {
  }

  public AsyncSQLQueryable(AsyncSQLQueryProvider provider, Expression expression)
    : base((IQueryProvider) provider, expression)
  {
  }

  private static AsyncSQLQueryProvider CreateSQLProvider(
    IQueryParser queryParser,
    AsyncSQLinqExecutor executor)
  {
    return new AsyncSQLQueryProvider(typeof (AsyncSQLQueryable<T>).GetGenericTypeDefinition(), queryParser, executor);
  }

  private static AsyncSQLinqExecutor CreateExecutor() => new AsyncSQLinqExecutor();

  private static QueryParser CreateParser()
  {
    ExpressionTransformerRegistry transformerRegistry = ExpressionTransformerRegistry.CreateDefault();
    SQLQueryable<T>.EvaluatableExpressionFilter expressionFilter = new SQLQueryable<T>.EvaluatableExpressionFilter();
    return new QueryParser(new ExpressionTreeParser((INodeTypeProvider) AsyncSQLQueryable<T>.CreateDefaultNodeTypeProvider(), (IExpressionTreeProcessor) ExpressionTreeParser.CreateDefaultProcessor((IExpressionTranformationProvider) transformerRegistry, (IEvaluatableExpressionFilter) expressionFilter)));
  }

  private static CompoundNodeTypeProvider CreateDefaultNodeTypeProvider()
  {
    CompoundNodeTypeProvider nodeTypeProvider1 = ExpressionTreeParser.CreateDefaultNodeTypeProvider();
    MethodInfoBasedNodeTypeRegistry nodeTypeProvider2 = nodeTypeProvider1.InnerProviders.OfType<MethodInfoBasedNodeTypeRegistry>().FirstOrDefault<MethodInfoBasedNodeTypeRegistry>();
    if (nodeTypeProvider2 == null)
      return nodeTypeProvider1;
    nodeTypeProvider2.RegisterAsyncQueryableMethods();
    return nodeTypeProvider1;
  }

  /// <inheritdoc />
  public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();
    return this.AsyncProvider.ExecuteCollectionAsync<T>(this.Expression, cancellationToken).GetAsyncEnumerator(cancellationToken);
  }

  /// <inheritdoc />
  IAsyncQueryProvider IAsyncQueryable.Provider => (IAsyncQueryProvider) this.Provider;

  internal IAsyncQueryProvider AsyncProvider => ((IAsyncQueryable) this).Provider;
}
