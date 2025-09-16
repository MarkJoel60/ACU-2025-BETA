// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Async.AsyncSQLinqExecutor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using Remotion.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.Expressions;
using Remotion.Linq.Clauses.ResultOperators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#nullable enable
namespace PX.Data.SQLTree.Async;

internal class AsyncSQLinqExecutor : SQLinqExecutor, IAsyncQueryExecutor
{
  /// <inheritdoc />
  public ValueTask<
  #nullable disable
  T> ExecuteScalarAsync<T>(QueryModel queryModel, CancellationToken cancellationToken)
  {
    return AsyncEnumerable.SingleOrDefaultAsync<T>(this.ExecuteCollectionAsync<T>(queryModel, cancellationToken), cancellationToken);
  }

  /// <inheritdoc />
  public ValueTask<T> ExecuteSingleAsync<T>(
    QueryModel queryModel,
    bool returnDefaultWhenEmpty,
    CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();
    IAsyncEnumerable<T> asyncEnumerable = this.ExecuteCollectionAsync<T>(queryModel, cancellationToken);
    ResultOperatorBase resultOperatorBase = queryModel.ResultOperators.LastOrDefault<ResultOperatorBase>();
    return !(resultOperatorBase is SingleResultOperator) ? (resultOperatorBase is LastResultOperator ? (!returnDefaultWhenEmpty ? AsyncEnumerable.LastAsync<T>(asyncEnumerable, cancellationToken) : AsyncEnumerable.LastOrDefaultAsync<T>(asyncEnumerable, cancellationToken)) : (!returnDefaultWhenEmpty ? AsyncEnumerable.FirstAsync<T>(asyncEnumerable, cancellationToken) : AsyncEnumerable.FirstOrDefaultAsync<T>(asyncEnumerable, cancellationToken))) : (!returnDefaultWhenEmpty ? AsyncEnumerable.SingleAsync<T>(asyncEnumerable, cancellationToken) : AsyncEnumerable.SingleOrDefaultAsync<T>(asyncEnumerable, cancellationToken));
  }

  /// <inheritdoc />
  public IAsyncEnumerable<T> ExecuteCollectionAsync<T>(
    QueryModel queryModel,
    CancellationToken cancellationToken)
  {
    // ISSUE: object of a compiler-generated type is created
    return (IAsyncEnumerable<T>) new AsyncSQLinqExecutor.\u003CExecuteCollectionAsync\u003Ed__2<T>(-2)
    {
      \u003C\u003E4__this = this,
      \u003C\u003E3__queryModel = queryModel,
      \u003C\u003E3__cancellationToken = cancellationToken
    };
  }

  private IAsyncEnumerable<T> ExecuteCollectionAsync<T>(QueryModel queryModel)
  {
    this._info = new SQLinqBqlCommandInfo();
    Expression currentExpression = this.CurrentExpression;
    IPXResultsetBase baseSelect = this.Queryable.BaseSelect;
    PXDelayedQuery delayedQuery = baseSelect?.GetDelayedQuery();
    if (baseSelect != null && delayedQuery == null)
    {
      if (SQLinqFallbackDisabledScope.IsScoped)
        throw new PXNotSupportedException("This kind of query is not supported by SQLinq. Make sure you are not using inherited View or BqlDelegate.");
      PXTrace.WriteError((Exception) null, "LINQ fallback! Consider rewriting your query.\nGraph: {Graph}\nLINQ Model: {Model}\n{StackTrace}", (object) this.Queryable.Graph, (object) queryModel, (object) PXStackTrace.GetStackTrace(2));
      return AsyncEnumerable.ToAsyncEnumerable<T>(this.ExecuteFallback<T>(queryModel, currentExpression));
    }
    try
    {
      this._query = AsyncSQLinqExecutor.AsyncSQLinqQueryModelVisitor.GenerateSQLQueryFromAsyncVisitor(queryModel, this._info, this.Queryable.Graph ?? delayedQuery?.View.Graph, delayedQuery?.View.IsReadOnly);
    }
    catch (PXNotSupportedException ex)
    {
      if (SQLinqFallbackDisabledScope.IsScoped || baseSelect == null)
        throw;
      object[] objArray = new object[4]
      {
        (object) (this.Queryable.Graph ?? delayedQuery?.View.Graph),
        (object) queryModel,
        null,
        null
      };
      BqlCommand baseCommand = this._info.BaseCommand;
      objArray[2] = (object) (baseCommand != null ? baseCommand.GetType().ToCodeString() : (string) null);
      objArray[3] = (object) PXStackTrace.GetStackTrace(2);
      PXTrace.WriteError((Exception) ex, "LINQ fallback! Consider rewriting your query.\nGraph: {Graph}\nLINQ Model: {Model}\nBase BQL: {Query}\n{StackTrace}", objArray);
      return AsyncEnumerable.ToAsyncEnumerable<T>(this.ExecuteFallback<T>(queryModel, currentExpression));
    }
    if (delayedQuery != null && delayedQuery.View.IsReadOnly && !this.MergeCache.HasValue)
      this.MergeCache = new bool?(false);
    if (this._skipRestrictions)
      this._query.SkipRestrictions();
    return this.ExecuteCollectionAsync<T>(this._query, this.Queryable.Graph ?? delayedQuery?.View.Graph);
  }

  private IAsyncEnumerable<T> ExecuteCollectionAsync<T>(Query query, PXGraph graph)
  {
    return this.ExecuteCollectionAsync<T>(query, this._info, graph, (SQLinqExecutor) this);
  }

  private IAsyncEnumerable<T> ExecuteCollectionAsync<T>(
    Query query,
    SQLinqBqlCommandInfo info,
    PXGraph graph = null,
    SQLinqExecutor linqExecutor = null,
    CancellationToken cancellationToken = default (CancellationToken))
  {
    cancellationToken.ThrowIfCancellationRequested();
    IAsyncEnumerable<T> asyncEnumerable;
    if (graph == null || linqExecutor == null)
    {
      asyncEnumerable = this.ExecuteCollectionWithoutGraphAsync<T>(query, info.Pars);
    }
    else
    {
      ProjectionItem projectionItem = query.Projection();
      if (linqExecutor.DacToMerge == (System.Type) null)
      {
        bool? mergeCache = linqExecutor.MergeCache;
        bool flag = false;
        if (!(mergeCache.GetValueOrDefault() == flag & mergeCache.HasValue))
        {
          if (projectionItem is ProjectionPXResult projectionPxResult)
            linqExecutor.DacToMerge = ((IEnumerable<System.Type>) projectionPxResult.GetResultTypes()).FirstOrDefault<System.Type>();
          if ((projectionItem is ProjectionReference || projectionItem is ProjectionReferenceEnumerable) && typeof (IBqlTable).IsAssignableFrom(projectionItem.GetResultType()))
            linqExecutor.DacToMerge = projectionItem.GetResultType();
          if (projectionItem is ProjectionEnumerableAny projectionEnumerableAny)
            linqExecutor.DacToMerge = projectionEnumerableAny.GetResultType();
        }
      }
      SQLinqBqlCommand select = typeof (IBqlTable).IsAssignableFrom(projectionItem.GetResultType()) ? new SQLinqBqlCommand(query, info) : (SQLinqBqlCommand) new SQLinqBqlCommandWithJoin(query, info);
      SQLinqAsyncViewOfT<T> linqAsyncViewOfT = new SQLinqAsyncViewOfT<T>(graph, select, linqExecutor);
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      object[] array = info.Arguments.ToArray();
      ref int local1 = ref num1;
      int maximumRows = num2;
      ref int local2 = ref num3;
      asyncEnumerable = AsyncEnumerable.Cast<T>(linqAsyncViewOfT.SelectAsync((object[]) null, array, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref local1, maximumRows, ref local2));
    }
    return asyncEnumerable;
  }

  private IAsyncEnumerable<T> ExecuteCollectionWithoutGraphAsync<T>(
    Query query,
    List<PXDataValue> pars,
    CancellationToken cancellationToken = default (CancellationToken))
  {
    cancellationToken.ThrowIfCancellationRequested();
    query.InjectDirectExpressions(pars.ToArray());
    query.AppendRestrictions();
    query = query.FlattenSubselects();
    PXDatabaseProvider prov = PXDatabase.Provider;
    PXSelectAsyncResult selectAsyncResult = new PXSelectAsyncResult(prov, new Func<DbCommand>(CommandFactory), cancellationToken);
    int position = 0;
    System.Func<PXDataRecord, T> func = (System.Func<PXDataRecord, T>) (r => (T) query.Projection().GetValue(r, ref position, (MergeCacheContext) null));
    return AsyncEnumerable.Select<PXDataRecord, T>((IAsyncEnumerable<PXDataRecord>) selectAsyncResult, func);

    DbCommand CommandFactory()
    {
      DbCommand dbCommand = (DbCommand) null;
      try
      {
        dbCommand = prov.GetCommand();
        Connection connection = prov.SqlDialect.GetConnection();
        dbCommand.CommandText = query.SQLQuery(connection).ToString();
        StringBuilder stringBuilder = new StringBuilder();
        if (prov is PXDatabaseProviderBase databaseProviderBase)
        {
          int num = 0;
          foreach (PXDataValue par in pars)
          {
            if (par.ValueType != PXDbType.DirectExpression)
            {
              DbCommand command = dbCommand;
              int parameterIndex = num;
              int valueType = (int) par.ValueType;
              int? size = new int?();
              object parameterValue = par.Value;
              databaseProviderBase._AddParameter((IDbCommand) command, parameterIndex, (PXDbType) valueType, size, ParameterDirection.Input, parameterValue, PXDatabaseProviderBase.ParameterBehavior.Compare);
              stringBuilder.Append(Environment.NewLine + $"@P{num}={par.Value}");
            }
            ++num;
          }
        }
        PXTrace.WithSourceLocation(nameof (ExecuteCollectionWithoutGraphAsync), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinqAsync.cs", 266).Verbose<string, string>("[LINQ]: {SQL}{Parameters}", dbCommand.CommandText, stringBuilder.ToString());
        return dbCommand;
      }
      catch
      {
        if (dbCommand != null)
        {
          prov.LeaveConnection((IDbConnection) dbCommand.Connection);
          dbCommand.Dispose();
        }
        throw;
      }
    }
  }

  internal class AsyncSQLinqQueryModelVisitor : SQLinqExecutor.SQLinqQueryModelVisitor
  {
    /// <inheritdoc />
    protected AsyncSQLinqQueryModelVisitor(
      SQLinqBqlCommandInfo info,
      PXGraph g,
      bool? queryIsReadOnly,
      IDictionary<IQuerySource, string> tableAliases)
      : base(info, g, queryIsReadOnly, tableAliases)
    {
    }

    internal static Query GenerateSQLQueryFromAsyncVisitor(
      QueryModel queryModel,
      SQLinqBqlCommandInfo info,
      PXGraph g,
      bool? queryIsReadOnly,
      IDictionary<IQuerySource, string> tableAliases = null)
    {
      AsyncSQLinqExecutor.AsyncSQLinqQueryModelVisitor queryModelVisitor = new AsyncSQLinqExecutor.AsyncSQLinqQueryModelVisitor(info, g, queryIsReadOnly, tableAliases ?? (IDictionary<IQuerySource, string>) new Dictionary<IQuerySource, string>());
      ((QueryModelVisitorBase) queryModelVisitor).VisitQueryModel(queryModel);
      return queryModelVisitor._query;
    }

    private protected override SQLExpression GetSQLExpression(Expression expression)
    {
      return AsyncSQLinqExecutor.AsyncSQLinqExpressionVisitor.GetSQLExpressionFromAsyncVisitor(this._info, this._graph, expression, this._tableAliases);
    }

    private protected override SQLExpression GetSQLExpressionCondition(Expression expression)
    {
      return AsyncSQLinqExecutor.AsyncSQLinqExpressionVisitor.GetSQLExpressionConditionFromAsyncVisitor(this._info, this._graph, expression, this._tableAliases);
    }

    private protected override Tuple<ProjectionItem, SQLExpression> GetSQLProjection(
      Expression expression)
    {
      return AsyncSQLinqExecutor.AsyncSQLinqExpressionVisitor.GetSQLProjectionFromAsyncVisitor(this._info, this._graph, expression, this._tableAliases);
    }

    public override void VisitResultOperator(
      ResultOperatorBase resultOperator,
      QueryModel queryModel,
      int index)
    {
      if (resultOperator is FirstAsyncResultOperator)
        this._query.Limit(1);
      else
        base.VisitResultOperator(resultOperator, queryModel, index);
    }
  }

  internal class AsyncSQLinqExpressionVisitor : SQLinqExecutor.SQLinqExpressionVisitor
  {
    /// <inheritdoc />
    private protected AsyncSQLinqExpressionVisitor(
      SQLinqBqlCommandInfo info,
      PXGraph g,
      IDictionary<IQuerySource, string> tableAliases)
      : base(info, g, tableAliases)
    {
    }

    private protected override SQLExpression GetSQLExpression(Expression linqExpression)
    {
      return AsyncSQLinqExecutor.AsyncSQLinqExpressionVisitor.GetSQLExpressionFromAsyncVisitor(this._info, this._graph, linqExpression, this._tableAliases);
    }

    internal static SQLExpression GetSQLExpressionFromAsyncVisitor(
      SQLinqBqlCommandInfo info,
      PXGraph g,
      Expression linqExpression,
      IDictionary<IQuerySource, string> tableAliases)
    {
      AsyncSQLinqExecutor.AsyncSQLinqExpressionVisitor expressionVisitor = new AsyncSQLinqExecutor.AsyncSQLinqExpressionVisitor(info, g, tableAliases);
      ((ExpressionVisitor) expressionVisitor).Visit(linqExpression);
      return expressionVisitor._currExpr;
    }

    internal static SQLExpression GetSQLExpressionConditionFromAsyncVisitor(
      SQLinqBqlCommandInfo info,
      PXGraph g,
      Expression linqExpression,
      IDictionary<IQuerySource, string> tableAliases)
    {
      AsyncSQLinqExecutor.AsyncSQLinqExpressionVisitor expressionVisitor = new AsyncSQLinqExecutor.AsyncSQLinqExpressionVisitor(info, g, tableAliases);
      expressionVisitor.Visit(linqExpression, true);
      return expressionVisitor._currExpr;
    }

    internal static Tuple<ProjectionItem, SQLExpression> GetSQLProjectionFromAsyncVisitor(
      SQLinqBqlCommandInfo info,
      PXGraph g,
      Expression linqExpression,
      IDictionary<IQuerySource, string> tableAliases)
    {
      AsyncSQLinqExecutor.AsyncSQLinqExpressionVisitor expressionVisitor = new AsyncSQLinqExecutor.AsyncSQLinqExpressionVisitor(info, g, tableAliases);
      ((ExpressionVisitor) expressionVisitor).Visit(linqExpression);
      expressionVisitor.ValidateSubQuery(linqExpression.Type);
      return Tuple.Create<ProjectionItem, SQLExpression>(expressionVisitor._currProj, expressionVisitor._currExpr);
    }

    private protected override Query CreateSubQueryVisitor(
      SubQueryExpression expression,
      SQLinqBqlCommandInfo innerInfo)
    {
      return AsyncSQLinqExecutor.AsyncSQLinqQueryModelVisitor.GenerateSQLQueryFromAsyncVisitor(expression.QueryModel, innerInfo, SQLinqExecutor.GetQueryable(expression.QueryModel)?.Graph ?? this._graph, new bool?(), this._tableAliases);
    }
  }
}
