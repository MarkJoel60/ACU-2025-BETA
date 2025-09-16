// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLQueryable`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Remotion.Linq;
using Remotion.Linq.Parsing.ExpressionVisitors.Transformation;
using Remotion.Linq.Parsing.ExpressionVisitors.TreeEvaluation;
using Remotion.Linq.Parsing.Structure;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Data.SQLTree;

internal class SQLQueryable<T> : QueryableBase<T>, ISQLQueryable
{
  public PXGraph Graph { get; }

  public IPXResultsetBase BaseSelect { get; }

  public virtual bool IsOuter() => false;

  private static IQueryExecutor CreateExecutor() => (IQueryExecutor) new SQLinqExecutor();

  internal SQLinqExecutor SQLinqExecutor
  {
    get
    {
      return (this.Provider is QueryProviderBase provider ? provider.Executor : (IQueryExecutor) null) as SQLinqExecutor;
    }
  }

  internal static SQLQueryable<T> ToSQLQueryable(IPXResultset rs)
  {
    return new SQLQueryable<T>(rs.GetDelayedQuery()?.View.Graph, (IPXResultsetBase) rs);
  }

  private static QueryParser CreateParser()
  {
    SQLQueryable<T>.EvaluatableExpressionFilter expressionFilter = new SQLQueryable<T>.EvaluatableExpressionFilter();
    return new QueryParser(new ExpressionTreeParser((INodeTypeProvider) SQLQueryable.DefaultNodeTypeProvider.Value, (IExpressionTreeProcessor) ExpressionTreeParser.CreateDefaultProcessor((IExpressionTranformationProvider) SQLQueryable.DefaultTransformerRegistry.Value, (IEvaluatableExpressionFilter) expressionFilter)));
  }

  private static SQLQueryProvider CreateSQLProvider(
    IQueryParser queryParser,
    IQueryExecutor executor)
  {
    return new SQLQueryProvider(typeof (SQLQueryable<T>).GetGenericTypeDefinition(), queryParser, executor);
  }

  public SQLQueryable()
    : this((PXGraph) null)
  {
  }

  public SQLQueryable(PXGraph graph)
    : this(graph, (IPXResultsetBase) null)
  {
  }

  protected SQLQueryable(PXGraph graph, IPXResultsetBase baseSelect, IQueryProvider provider)
    : base(provider)
  {
    this.Graph = graph;
    this.BaseSelect = baseSelect;
    SQLinqExecutor sqLinqExecutor = this.SQLinqExecutor;
    if (sqLinqExecutor == null)
      return;
    sqLinqExecutor.Queryable = (ISQLQueryable) this;
  }

  public SQLQueryable(PXGraph graph, IPXResultsetBase baseSelect)
    : this(graph, baseSelect, (IQueryProvider) SQLQueryable<T>.CreateSQLProvider((IQueryParser) SQLQueryable<T>.CreateParser(), SQLQueryable<T>.CreateExecutor()))
  {
  }

  public SQLQueryable(IQueryProvider provider, Expression expression)
    : base(provider, expression)
  {
  }

  internal sealed class EvaluatableExpressionFilter : EvaluatableExpressionFilterBase
  {
    public virtual bool IsEvaluatableConstant(ConstantExpression node) => true;

    public virtual bool IsEvaluatableMethodCall(MethodCallExpression node)
    {
      return node.Method.Name != "AsParam" && !JoinSubstitutions.IsSubstitutionMethod(node.Method.Name);
    }
  }
}
