// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLinqExecutor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using Remotion.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.Expressions;
using Remotion.Linq.Clauses.ResultOperators;
using Remotion.Linq.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

#nullable disable
namespace PX.Data.SQLTree;

internal class SQLinqExecutor : IQueryExecutor
{
  protected Query _query;
  protected SQLinqBqlCommandInfo _info;
  protected bool _skipRestrictions;

  public Expression CurrentExpression { get; set; }

  public void SkipRestrictions(bool skip = true) => this._skipRestrictions = skip;

  internal ISQLQueryable Queryable { get; set; }

  /// <summary>
  /// Point what type of DAC we meed to merge.
  /// Makes sense with <see cref="P:PX.Data.SQLTree.SQLinqExecutor.MergeCache" />
  /// </summary>
  public System.Type DacToMerge { get; set; }

  /// <summary>
  /// Indicates do we need to merge objects retrieved from DB with data in PXCache.
  /// True - yes. False - no. Null - default behaviour.
  /// See <see cref="P:PX.Data.SQLTree.SQLinqExecutor.DacToMerge" /> also.
  /// </summary>
  public bool? MergeCache { get; set; }

  public T ExecuteScalar<T>(QueryModel queryModel)
  {
    return this.ExecuteCollection<T>(queryModel).SingleOrDefault<T>();
  }

  public T ExecuteSingle<T>(QueryModel queryModel, bool returnDefaultWhenEmpty)
  {
    IEnumerable<T> source = this.ExecuteCollection<T>(queryModel);
    ResultOperatorBase resultOperatorBase = queryModel.ResultOperators.LastOrDefault<ResultOperatorBase>();
    return !(resultOperatorBase is SingleResultOperator) ? (resultOperatorBase is LastResultOperator ? (!returnDefaultWhenEmpty ? source.Last<T>() : source.LastOrDefault<T>()) : (!returnDefaultWhenEmpty ? source.First<T>() : source.FirstOrDefault<T>())) : (!returnDefaultWhenEmpty ? source.Single<T>() : source.SingleOrDefault<T>());
  }

  public override string ToString() => this._query?.ToString() ?? "";

  internal string ToDbString(ISqlDialect dialect = null)
  {
    return this._query?.SQLQuery(dialect?.GetConnection() ?? PXDatabase.Provider.SqlDialect.GetConnection()).ToString() ?? "";
  }

  private static IEnumerable toSingleEnumerable(object item, System.Type itemType)
  {
    System.Type type = typeof (List<>).MakeGenericType(itemType);
    object instance = Activator.CreateInstance(type);
    type.GetMethod("Add").Invoke(instance, new object[1]
    {
      item
    });
    return instance as IEnumerable;
  }

  private static object[] prepareArgs(IEnumerable input, MethodCallExpression meth)
  {
    IEnumerable source = input;
    if (meth.Arguments[0].Type.GenericTypeArguments[0] != input.GetType().GenericTypeArguments[0])
      source = (IEnumerable) Expression.Lambda((Expression) Expression.Call((Expression) null, typeof (Enumerable).GetMethod("Cast").MakeGenericMethod(meth.Arguments[0].Type.GenericTypeArguments[0]), (Expression) Expression.Constant((object) source))).Compile().DynamicInvoke();
    List<object> objectList = new List<object>()
    {
      (object) source.AsQueryable()
    };
    for (int index = 1; index < meth.Arguments.Count; ++index)
    {
      object obj = (meth.Arguments[index] is UnaryExpression unaryExpression ? (object) unaryExpression.Operand : (object) null) ?? Expression.Lambda(meth.Arguments[index]).Compile().DynamicInvoke();
      objectList.Add(obj);
    }
    return objectList.ToArray();
  }

  private static IEnumerable evaluateExpTree(Expression node, IEnumerable input)
  {
    MethodCallExpression meth = node as MethodCallExpression;
    System.Type genericTypeArgument = input.GetType().GenericTypeArguments[0];
    if (meth?.Arguments[0].Type.GetGenericTypeDefinition() == typeof (SQLQueryable<>))
    {
      object obj = meth.Method.Invoke((object) null, SQLinqExecutor.prepareArgs(input, meth));
      return !typeof (IEnumerable).IsAssignableFrom(meth.Method.ReturnType) ? SQLinqExecutor.toSingleEnumerable(obj, meth.Method.ReturnType) : obj as IEnumerable;
    }
    input = SQLinqExecutor.evaluateExpTree(meth.Arguments[0], input);
    object obj1 = meth.Method.Invoke((object) null, SQLinqExecutor.prepareArgs(input, meth));
    return !typeof (IEnumerable).IsAssignableFrom(meth.Method.ReturnType) ? SQLinqExecutor.toSingleEnumerable(obj1, meth.Method.ReturnType) : obj1 as IEnumerable;
  }

  public IEnumerable<T> ExecuteCollection<T>(QueryModel queryModel)
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
      return this.ExecuteFallback<T>(queryModel, currentExpression);
    }
    try
    {
      this._query = SQLinqExecutor.SQLinqQueryModelVisitor.GenerateSQLQuery(queryModel, this._info, this.Queryable.Graph ?? delayedQuery?.View.Graph, delayedQuery?.View.IsReadOnly);
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
      return this.ExecuteFallback<T>(queryModel, currentExpression);
    }
    if (delayedQuery != null && delayedQuery.View.IsReadOnly && !this.MergeCache.HasValue)
      this.MergeCache = new bool?(false);
    if (this._skipRestrictions)
      this._query.SkipRestrictions();
    return this.ExecuteCollection<T>(this._query, this.Queryable.Graph ?? delayedQuery?.View.Graph);
  }

  public IEnumerable<T> ExecuteFallback<T>(QueryModel queryModel, Expression original)
  {
    MethodCallExpression node = original as MethodCallExpression;
    object collection = (SQLinqExecutor.GetQueryable(queryModel)?.BaseSelect ?? throw new PXException("Base BQL select of SQLinq fallback procedure is undefined. ")).GetCollection();
    IEnumerable expTree;
    try
    {
      expTree = SQLinqExecutor.evaluateExpTree((Expression) node, (IEnumerable) collection);
    }
    catch (TargetInvocationException ex)
    {
      throw ex.InnerException;
    }
    return expTree as IEnumerable<T>;
  }

  private protected static ISQLQueryable GetQueryable(QueryModel queryModel)
  {
    return SQLinqExecutor.IsGroupingType(((FromClauseBase) queryModel.MainFromClause).ItemType) ? SQLinqExecutor.GetQueryable(((SubQueryExpression) ((FromClauseBase) queryModel.MainFromClause).FromExpression).QueryModel) : (((FromClauseBase) queryModel.MainFromClause).FromExpression is ConstantExpression fromExpression ? fromExpression.Value : (object) null) as ISQLQueryable;
  }

  public IEnumerable<T> ExecuteCollection<T>(Query query, PXGraph graph)
  {
    return SQLinqExecutor.ExecuteCollection<T>(query, this._info, graph, this);
  }

  public static IEnumerable<T> ExecuteCollection<T>(
    Query query,
    SQLinqBqlCommandInfo info,
    PXGraph graph = null,
    SQLinqExecutor linqExecutor = null)
  {
    IEnumerable<T> source;
    if (graph == null || linqExecutor == null)
    {
      source = SQLinqExecutor.ExecuteCollectionWithoutGraph<T>(query, info.Pars);
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
      SQLinqView<T> sqLinqView = new SQLinqView<T>(graph, select, linqExecutor);
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      object[] array = info.Arguments.ToArray();
      ref int local1 = ref num1;
      int maximumRows = num2;
      ref int local2 = ref num3;
      source = sqLinqView.Select((object[]) null, array, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref local1, maximumRows, ref local2).Cast<T>();
    }
    if (!source.Any<T>())
      source = query.Projection().GetEmptyResult().Cast<T>();
    return source;
  }

  private static IEnumerable<T> ExecuteCollectionWithoutGraph<T>(
    Query query,
    List<PXDataValue> pars)
  {
    query.InjectDirectExpressions(pars.ToArray());
    query.AppendRestrictions();
    query = query.FlattenSubselects();
    PXDatabaseProvider prov = PXDatabase.Provider;
    PXSelectResult pxSelectResult = new PXSelectResult(prov, new Func<IDbCommand>(CommandFactory));
    List<T> objList = new List<T>();
    foreach (PXDataRecord data in (IEnumerable<PXDataRecord>) pxSelectResult)
    {
      int position = 0;
      object obj = query.Projection().GetValue(data, ref position, (MergeCacheContext) null);
      objList.Add((T) obj);
    }
    return (IEnumerable<T>) objList;

    IDbCommand CommandFactory()
    {
      IDbCommand dbCommand = (IDbCommand) null;
      try
      {
        dbCommand = (IDbCommand) prov.GetCommand();
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
              IDbCommand command = dbCommand;
              int parameterIndex = num;
              int valueType = (int) par.ValueType;
              int? size = new int?();
              object parameterValue = par.Value;
              databaseProviderBase._AddParameter(command, parameterIndex, (PXDbType) valueType, size, ParameterDirection.Input, parameterValue, PXDatabaseProviderBase.ParameterBehavior.Compare);
              stringBuilder.Append(Environment.NewLine + $"@P{num}={par.Value}");
            }
            ++num;
          }
        }
        PXTrace.WithSourceLocation(nameof (ExecuteCollectionWithoutGraph), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinq.cs", 400).Verbose<string, string>("[LINQ]: {SQL}{Parameters}", dbCommand.CommandText, stringBuilder.ToString());
        return dbCommand;
      }
      catch
      {
        if (dbCommand != null)
        {
          prov.LeaveConnection(dbCommand.Connection);
          dbCommand.Dispose();
        }
        throw;
      }
    }
  }

  internal List<T> SortResult<T>(IEnumerable<T> result, out bool changed)
  {
    changed = false;
    List<(LambdaExpression, string)> valueTupleList = new List<(LambdaExpression, string)>();
    for (Expression currentExpression = this.CurrentExpression; currentExpression is MethodCallExpression methodCallExpression && (methodCallExpression.Method.Name == "OrderBy" || methodCallExpression.Method.Name == "ThenBy" || methodCallExpression.Method.Name == "OrderByDescending" || methodCallExpression.Method.Name == "ThenByDescending"); currentExpression = methodCallExpression.Arguments[0])
      valueTupleList.Insert(0, ((LambdaExpression) ((UnaryExpression) methodCallExpression.Arguments[1]).Operand, methodCallExpression.Method.Name));
    IOrderedEnumerable<T> source = (IOrderedEnumerable<T>) null;
    foreach ((LambdaExpression, string) valueTuple in valueTupleList)
      source = SQLinqExecutor.SortDynamic<T>((IEnumerable<T>) source ?? result, valueTuple.Item1, valueTuple.Item2);
    if (source != null)
    {
      changed = true;
      return source.ToList<T>();
    }
    return !(result is List<T> objList) ? result.ToList<T>() : objList;
  }

  private static IOrderedEnumerable<T> SortDynamic<T>(
    IEnumerable<T> source,
    LambdaExpression keySelector,
    string methodName)
  {
    return (IOrderedEnumerable<T>) Expression.Lambda((Expression) Expression.Call((Expression) null, ((IEnumerable<MethodInfo>) typeof (Enumerable).GetMethods()).First<MethodInfo>((System.Func<MethodInfo, bool>) (m => m.Name == methodName && m.GetParameters().Length == 2)).MakeGenericMethod(typeof (T), keySelector.ReturnType), (Expression) Expression.Constant((object) source), (Expression) keySelector)).Compile().DynamicInvoke();
  }

  private static bool IsGroupingType(System.Type type)
  {
    return type.IsGenericType && type.GetGenericTypeDefinition() == typeof (IGrouping<,>);
  }

  private static QueryModel GetQueryModelWithGrouping(MemberExpression expression)
  {
    return SQLinqExecutor.GetQueryModelWithGrouping((QuerySourceReferenceExpression) expression.Expression);
  }

  private static QueryModel GetQueryModelWithGrouping(QuerySourceReferenceExpression expression)
  {
    if (expression.ReferencedQuerySource is MainFromClause referencedQuerySource1)
      return ((SubQueryExpression) ((FromClauseBase) referencedQuerySource1).FromExpression).QueryModel;
    if (expression.ReferencedQuerySource is AdditionalFromClause referencedQuerySource2)
      return ((SubQueryExpression) ((FromClauseBase) referencedQuerySource2).FromExpression).QueryModel;
    throw new PXException("Incorrect grouping type. ");
  }

  internal class SQLinqQueryModelVisitor : QueryModelVisitorBase
  {
    protected readonly Query _query = new Query();
    private protected readonly SQLinqBqlCommandInfo _info;
    private protected readonly PXGraph _graph;
    private readonly bool? _queryIsReadOnly;
    private protected IDictionary<IQuerySource, string> _tableAliases;

    private string GetTableAlias(IQuerySource src)
    {
      return SQLinqExecutor.SQLinqExpressionVisitor.GetTableAlias(src, this._tableAliases);
    }

    protected SQLinqQueryModelVisitor(
      SQLinqBqlCommandInfo info,
      PXGraph g,
      bool? queryIsReadOnly,
      IDictionary<IQuerySource, string> tableAliases)
    {
      this._info = info;
      this._graph = g;
      this._queryIsReadOnly = queryIsReadOnly;
      this._tableAliases = tableAliases;
    }

    public static Query GenerateSQLQuery(
      QueryModel queryModel,
      SQLinqBqlCommandInfo info,
      PXGraph g,
      bool? queryIsReadOnly,
      IDictionary<IQuerySource, string> tableAliases = null)
    {
      SQLinqExecutor.SQLinqQueryModelVisitor queryModelVisitor = new SQLinqExecutor.SQLinqQueryModelVisitor(info, g, queryIsReadOnly, tableAliases ?? (IDictionary<IQuerySource, string>) new Dictionary<IQuerySource, string>());
      ((QueryModelVisitorBase) queryModelVisitor).VisitQueryModel(queryModel);
      return queryModelVisitor._query;
    }

    public virtual void VisitQueryModel(QueryModel queryModel)
    {
      PXTrace.WithSourceLocation(nameof (VisitQueryModel), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinq.cs", 492).Verbose<string>("Remotion Visit {NodeType}", "QueryModel");
      queryModel.SelectClause.Accept((IQueryModelVisitor) this, queryModel);
      queryModel.MainFromClause.Accept((IQueryModelVisitor) this, queryModel);
      this.VisitBodyClauses(queryModel.BodyClauses, queryModel);
      this.VisitResultOperators(queryModel.ResultOperators, queryModel);
      this.EnsureOrder(queryModel);
      if (!(queryModel.ResultTypeOverride != (System.Type) null) || !queryModel.ResultTypeOverride.IsGenericType)
        return;
      System.Type genericTypeArgument = queryModel.ResultTypeOverride.GenericTypeArguments[0];
      if ((!genericTypeArgument.IsGenericType ? 0 : (genericTypeArgument.GetGenericTypeDefinition() == typeof (IGrouping<,>) ? 1 : 0)) == 0)
        return;
      List<SQLExpression> list = this._query.GetGroupBy().SelectMany<SQLExpression, SQLExpression>((System.Func<SQLExpression, IEnumerable<SQLExpression>>) (gr => (IEnumerable<SQLExpression>) gr.DecomposeSequence())).ToList<SQLExpression>();
      Query query = (Query) this._query.Duplicate();
      query.GetGroupBy().Clear();
      this._query.GetSelection().Clear();
      foreach (SQLExpression field in list)
        this._query.Field(field);
      ProjectionItem projectionItem = this.GetSQLProjection((queryModel.ResultOperators.OfType<GroupResultOperator>().FirstOrDefault<GroupResultOperator>() ?? ((SubQueryExpression) ((FromClauseBase) queryModel.MainFromClause).FromExpression).QueryModel.ResultOperators.OfType<GroupResultOperator>().First<GroupResultOperator>()).KeySelector).Item1;
      this._query.SetProjection((ProjectionItem) Activator.CreateInstance(typeof (ProjectionGrouping<,>).MakeGenericType(genericTypeArgument.GenericTypeArguments[0], genericTypeArgument.GenericTypeArguments[1]), (object) query, (object) list, (object) projectionItem, (object) this._graph, (object) this._info));
    }

    private void EnsureOrder(QueryModel queryModel)
    {
      if (this._graph == null || this._query.GetOrder() != null)
        return;
      ObservableCollection<IBodyClause> bodyClauses = queryModel.BodyClauses;
      if ((bodyClauses != null ? (bodyClauses.OfType<OrderByClause>().Any<OrderByClause>() ? 1 : 0) : 1) != 0)
        return;
      ObservableCollection<ResultOperatorBase> resultOperators = queryModel.ResultOperators;
      if ((resultOperators != null ? (!resultOperators.Any<ResultOperatorBase>((System.Func<ResultOperatorBase, bool>) (r => r is SkipResultOperator || r is TakeResultOperator)) ? 1 : 0) : 1) != 0)
        return;
      string itemName = this.GetTableAlias((IQuerySource) queryModel.MainFromClause);
      IPXResultsetBase baseSelect = (((FromClauseBase) queryModel.MainFromClause).FromExpression is ConstantExpression fromExpression ? fromExpression.Value : (object) null) is ISQLQueryable sqlQueryable ? sqlQueryable.BaseSelect : (IPXResultsetBase) null;
      bool flag = true;
      PXView pxView;
      if (baseSelect != null)
      {
        pxView = baseSelect.GetDelayedQuery().View;
      }
      else
      {
        pxView = new PXView(this._graph, true, BqlCommand.CreateInstance(typeof (Select<>), ((FromClauseBase) queryModel.MainFromClause).ItemType));
        flag = this._graph.Caches[((FromClauseBase) queryModel.MainFromClause).ItemType.Name]?.BqlSelect != null;
      }
      BqlCommand.Selection selection1 = new BqlCommand.Selection();
      selection1._Command = pxView.BqlSelect;
      selection1.UseColumnAliases = flag;
      List<IBqlParameter> parameters = this._info.Parameters;
      // ISSUE: explicit non-virtual call
      selection1.ParamCounter = parameters != null ? __nonvirtual (parameters.Count) : 0;
      BqlCommand.Selection selection2 = selection1;
      bool resetTopCount = false;
      bool needOverrideSort;
      PXView.PXSearchColumn[] pxSearchColumnArray = pxView.prepareSorts((string[]) null, (bool[]) null, (object[]) null, 0, out needOverrideSort, out bool _, ref resetTopCount);
      this._info.BaseCommand = pxView.BqlSelect;
      this._info.SortColumns = new List<IBqlSortColumn>();
      List<OrderSegment> order1 = pxView.BqlSelect.GetQueryInternal(pxView.Graph, (BqlCommandInfo) this._info, selection2).GetOrder();
      List<OrderSegment> list = order1 != null ? order1.Where<OrderSegment>((System.Func<OrderSegment, bool>) (o => o.expr_ is Column)).ToList<OrderSegment>() : (List<OrderSegment>) null;
      if (list == null & needOverrideSort)
      {
        System.Type newOrderBy = pxView.ConstructSort(pxSearchColumnArray, new List<PXDataValue>(), resetTopCount);
        if (newOrderBy != (System.Type) null)
        {
          List<OrderSegment> order2 = pxView.BqlSelect.OrderByNew(newOrderBy).GetQueryInternal(pxView.Graph, (BqlCommandInfo) new SQLinqBqlCommandInfo(), selection2).GetOrder();
          list = order2 != null ? order2.Where<OrderSegment>((System.Func<OrderSegment, bool>) (o => o.expr_ is Column)).ToList<OrderSegment>() : (List<OrderSegment>) null;
        }
      }
      if (list != null && list.Any<OrderSegment>())
      {
        foreach (OrderSegment orderSegment in list)
          this._query.AddOrderSegment(new OrderSegment((SQLExpression) new Column(orderSegment.expr_.AliasOrName(), itemName, orderSegment.expr_.GetDBType()), orderSegment.ascending_));
      }
      else
      {
        if (!((IEnumerable<PXView.PXSearchColumn>) pxSearchColumnArray).Any<PXView.PXSearchColumn>())
          return;
        foreach (PXView.PXSearchColumn pxSearchColumn in ((IEnumerable<PXView.PXSearchColumn>) pxSearchColumnArray).Where<PXView.PXSearchColumn>((System.Func<PXView.PXSearchColumn, bool>) (s => s.Description != null)))
        {
          PXView.PXSearchColumn os = pxSearchColumn;
          if (os.Description.Expr is Column expr)
          {
            this._query.AddOrderSegment(new OrderSegment((SQLExpression) new Column($"{expr.Table().AliasOrName()}_{expr.Name}", itemName, expr.GetDBType()), !os.Descending));
          }
          else
          {
            os.Description.Expr.GetExpressionsOfType<Column>().Where<Column>((System.Func<Column, bool>) (c => this._info.Tables.Any<System.Type>((System.Func<System.Type, bool>) (t => c.Table().AliasOrName().Equals(t.Name, StringComparison.OrdinalIgnoreCase))))).ToList<Column>().ForEach((System.Action<Column>) (c => os.Description.Expr.substituteNode((SQLExpression) c, (SQLExpression) new Column($"{c.Table().AliasOrName()}_{c.Name}", itemName, c.GetDBType()))));
            this._query.AddOrderSegment(new OrderSegment(os.Description.Expr, !os.Descending));
          }
        }
      }
    }

    private void VisitQueryModelForGrouping(QueryModel queryModel)
    {
      PXTrace.WithSourceLocation(nameof (VisitQueryModelForGrouping), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinq.cs", 617).Verbose<string>("Remotion Visit {NodeType}", "QueryModelForGrouping");
      queryModel.MainFromClause.Accept((IQueryModelVisitor) this, queryModel);
      this.VisitBodyClauses(queryModel.BodyClauses, queryModel);
      this.VisitResultOperators(queryModel.ResultOperators, queryModel);
    }

    public virtual void VisitJoinClause(JoinClause joinClause, QueryModel queryModel, int index)
    {
      PXTrace.WithSourceLocation(nameof (VisitJoinClause), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinq.cs", 625).Verbose("Remotion Visit {NodeType} #{Index} {Name} of type {Type}", new object[4]
      {
        (object) "JoinClause",
        (object) index,
        (object) joinClause.ItemName,
        (object) joinClause.ItemType
      });
      System.Type itemType = joinClause.ItemType;
      string tableAlias = this.GetTableAlias((IQuerySource) joinClause);
      if (typeof (IBqlTable).IsAssignableFrom(itemType))
      {
        Joiner.JoinType jt = Joiner.JoinType.INNER_JOIN;
        Expression innerKeySelector1 = joinClause.InnerKeySelector;
        if (joinClause.InnerKeySelector is MethodCallExpression innerKeySelector2)
        {
          switch (innerKeySelector2.Method.Name)
          {
            case "__LeftJoinSubstitution__":
              innerKeySelector1 = innerKeySelector2.Arguments[0];
              jt = Joiner.JoinType.LEFT_JOIN;
              break;
            case "__RightJoinSubstitution__":
              innerKeySelector1 = innerKeySelector2.Arguments[0];
              jt = Joiner.JoinType.RIGHT_JOIN;
              break;
            case "__FullJoinSubstitution__":
              innerKeySelector1 = innerKeySelector2.Arguments[0];
              jt = Joiner.JoinType.FULL_JOIN;
              break;
            case "__CrossJoinSubstitution__":
              innerKeySelector1 = innerKeySelector2.Arguments[0];
              jt = Joiner.JoinType.CROSS_JOIN;
              break;
          }
        }
        Joiner j = new Joiner(jt, this.GetSQLTable(itemType, tableAlias), this._query);
        this._info.Tables?.Add(itemType);
        SQLExpression sqlExpression1 = this.GetSQLExpression(joinClause.OuterKeySelector);
        SQLExpression sqlExpression2 = this.GetSQLExpression(innerKeySelector1);
        if (jt != Joiner.JoinType.CROSS_JOIN)
        {
          if (joinClause.OuterKeySelector is MethodCallExpression outerKeySelector)
          {
            switch (outerKeySelector.Method.Name)
            {
              case "Greater":
                j.On(sqlExpression1.GT(sqlExpression2));
                break;
              case "GreaterOr":
                j.On(sqlExpression1.GE(sqlExpression2));
                break;
            }
          }
          else
          {
            List<SQLExpression> sqlExpressionList1 = sqlExpression1.DecomposeSequence();
            List<SQLExpression> sqlExpressionList2 = sqlExpression2.DecomposeSequence();
            if (sqlExpressionList1.Count != sqlExpressionList2.Count)
              throw new PXException("Different numbers of parameters on left and right sides of Join condition");
            SQLExpression e = SQLExpressionExt.EQ(sqlExpressionList1[0], sqlExpressionList2[0]);
            for (int index1 = 1; index1 < sqlExpressionList1.Count; ++index1)
              e = e.And(SQLExpressionExt.EQ(sqlExpressionList1[index1], sqlExpressionList2[index1]));
            j.On(e);
          }
        }
        this._query.AddJoin(j);
      }
      base.VisitJoinClause(joinClause, queryModel, index);
    }

    public virtual void VisitOrderByClause(
      OrderByClause orderByClause,
      QueryModel queryModel,
      int index)
    {
      PXTrace.WithSourceLocation(nameof (VisitOrderByClause), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinq.cs", 696).Verbose<string>("Remotion Visit {NodeType}", "OrderByClause");
      base.VisitOrderByClause(orderByClause, queryModel, index);
    }

    public virtual void VisitOrdering(
      Ordering ordering,
      QueryModel queryModel,
      OrderByClause orderByClause,
      int index)
    {
      PXTrace.WithSourceLocation(nameof (VisitOrdering), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinq.cs", 701).Verbose<string>("Remotion Visit {NodeType}", "Ordering");
      OrderSegment orderSegment = new OrderSegment(this.GetSQLExpression(ordering.Expression), ordering.OrderingDirection == 0);
      this._query.AddOrderSegment(orderSegment);
      if (this._info.SortColumns != null)
        this._info.SortColumns.Add((IBqlSortColumn) new SQLinqBqlSortColumn(orderSegment));
      base.VisitOrdering(ordering, queryModel, orderByClause, index);
    }

    public virtual void VisitJoinClause(
      JoinClause joinClause,
      QueryModel queryModel,
      GroupJoinClause groupJoinClause)
    {
      PXTrace.WithSourceLocation(nameof (VisitJoinClause), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinq.cs", 711).Verbose("Remotion Visit {NodeType} {Name} of type {Type} grouped into {GroupName} of type {GroupType}", new object[5]
      {
        (object) "JoinClause",
        (object) joinClause.ItemName,
        (object) joinClause.ItemType,
        (object) groupJoinClause.ItemName,
        (object) groupJoinClause.ItemType
      });
      throw new PXNotSupportedException("Linq Group Join is not supported in SQLinq library. ");
    }

    public virtual void VisitSelectClause(SelectClause selectClause, QueryModel queryModel)
    {
      PXTrace.WithSourceLocation(nameof (VisitSelectClause), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinq.cs", 716).Verbose<string>("Remotion Visit {NodeType}", "SelectClause");
      this.SetupSelection(selectClause.Selector);
      base.VisitSelectClause(selectClause, queryModel);
    }

    private void SetupSelection(Expression selector)
    {
      Tuple<ProjectionItem, SQLExpression> sqlProjection = this.GetSQLProjection(selector);
      this._query.SetProjection(sqlProjection.Item1);
      this._query.ClearSelection();
      this._query.Fields(sqlProjection.Item2);
    }

    public virtual void VisitResultOperator(
      ResultOperatorBase resultOperator,
      QueryModel queryModel,
      int index)
    {
      PXTrace.WithSourceLocation(nameof (VisitResultOperator), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinq.cs", 730).Verbose<string>("Remotion Visit {NodeType}", "ResultOperator");
      switch (resultOperator)
      {
        case CountResultOperator _:
          this._query.ClearSelection();
          this._query.GetSelection().Add(SQLExpression.Count());
          this._query.SetProjection((ProjectionItem) new ProjectionConst(typeof (int)));
          break;
        case LongCountResultOperator _:
          this._query.ClearSelection();
          this._query.GetSelection().Add(SQLExpression.Count());
          this._query.SetProjection((ProjectionItem) new ProjectionConst(typeof (long)));
          break;
        case MaxResultOperator _:
          this.SetupAggregate((System.Func<SQLExpression, SQLExpression>) (e => e.Max()), resultOperator.GetType());
          break;
        case MinResultOperator _:
          this.SetupAggregate((System.Func<SQLExpression, SQLExpression>) (e => e.Min()), resultOperator.GetType());
          break;
        case AverageResultOperator _:
          this.SetupAggregate((System.Func<SQLExpression, SQLExpression>) (e => e.Avg()), resultOperator.GetType());
          break;
        case SumResultOperator _:
          this.SetupAggregate((System.Func<SQLExpression, SQLExpression>) (e => e.Sum()), resultOperator.GetType());
          break;
        case FirstResultOperator _:
          this._query.Limit(1);
          break;
        case DefaultIfEmptyResultOperator _:
          this._query.MarkDefault();
          break;
        case GroupResultOperator groupResultOperator:
          this._query.GroupBy(this.GetSQLExpression(groupResultOperator.KeySelector));
          if (queryModel.SelectClause.Selector.Equals((object) groupResultOperator.ElementSelector))
            break;
          this.SetupSelection(groupResultOperator.ElementSelector);
          break;
        case AnyResultOperator _:
          bool? queryIsReadOnly = this._queryIsReadOnly;
          bool flag = true;
          if (queryIsReadOnly.GetValueOrDefault() == flag & queryIsReadOnly.HasValue || !ProjectionEnumerableAny.CouldBeProjectionOf(this._query.Projection()))
          {
            this._query.ClearSelection();
            this._query.GetSelection().Add((SQLExpression) new SQLConst((object) 1));
            this._query.Limit(1);
            this._query.SetProjection((ProjectionItem) new ProjectionAny());
            break;
          }
          this._query.SetProjection((ProjectionItem) new ProjectionEnumerableAny(this._query.Projection()));
          PXCache cach = this._graph.Caches[this._query.Projection().type_];
          if (cach != null)
          {
            this._query.Limit((int) cach.Deleted.Count() + (int) cach.Updated.Count() + 1);
            break;
          }
          this._query.Limit(1);
          break;
        case SingleResultOperator _:
          this._query.Limit(2);
          break;
        case LastResultOperator _:
          List<OrderSegment> order = this._query.GetOrder();
          if (order == null || !order.Any<OrderSegment>())
            throw new PXNotSupportedException("Last operator without sorting is not supported.");
          foreach (OrderSegment orderSegment in order)
            orderSegment.ascending_ = !orderSegment.ascending_;
          this._query.Limit(1);
          break;
        case SkipResultOperator skipResultOperator:
          try
          {
            this._query.Offset(skipResultOperator.GetConstantCount());
            break;
          }
          catch (InvalidOperationException ex)
          {
            throw new PXNotSupportedException("Skip operator supported only with constant value.");
          }
        case TakeResultOperator takeResultOperator:
          try
          {
            this._query.Limit(takeResultOperator.GetConstantCount());
            break;
          }
          catch (InvalidOperationException ex)
          {
            throw new PXNotSupportedException("Take operator supported only with constant value.");
          }
        default:
          throw new PXNotSupportedException($"Not supported result operator {resultOperator.GetType().Name}. ");
      }
    }

    private void SetupAggregate(
      System.Func<SQLExpression, SQLExpression> aggrAction,
      System.Type resultOperatorType)
    {
      List<SQLExpression> selection = this._query.GetSelection();
      SQLExpression sqlExpression = selection != null && selection.Count == 1 ? selection.First<SQLExpression>() : throw new PXNotSupportedException($"Can't use {resultOperatorType.Name} with multiple items in SELECT statement.");
      this._query.ClearSelection();
      this._query.GetSelection().Add(aggrAction(sqlExpression));
      this._query.SetProjection((ProjectionItem) new ProjectionConst(this._query.Projection().type_));
    }

    public virtual void VisitAdditionalFromClause(
      AdditionalFromClause fromClause,
      QueryModel queryModel,
      int index)
    {
      PXTrace.WithSourceLocation(nameof (VisitAdditionalFromClause), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinq.cs", 851).Verbose("Remotion Visit {NodeType} #{Index} {Name} of type {Type}", new object[4]
      {
        (object) "AdditionalFromClause",
        (object) index,
        (object) ((FromClauseBase) fromClause).ItemName,
        (object) ((FromClauseBase) fromClause).ItemType
      });
      System.Type itemType = ((FromClauseBase) fromClause).ItemType;
      string tableAlias = this.GetTableAlias((IQuerySource) fromClause);
      if (typeof (IBqlTable).IsAssignableFrom(itemType))
      {
        Joiner.JoinType jt1 = Joiner.JoinType.CROSS_JOIN;
        SQLExpression sqlExpression = this.GetSQLExpression(((FromClauseBase) fromClause).FromExpression);
        switch (sqlExpression)
        {
          case SQLConst _:
            this._query.AddJoin(new Joiner(jt1, this.GetSQLTable(itemType, tableAlias), this._query));
            List<System.Type> tables = this._info.Tables;
            if (tables != null)
            {
              // ISSUE: explicit non-virtual call
              __nonvirtual (tables.Add(itemType));
              break;
            }
            break;
          case SubQuery _:
            Query t = (sqlExpression as SubQuery).Query();
            SQLExpression where = t.GetWhere();
            t.Where((SQLExpression) null);
            t.Alias = tableAlias;
            if (where != null && t.GetFrom() != null)
            {
              Dictionary<string, string> dict = new Dictionary<string, string>();
              foreach (Joiner joiner in t.GetFrom())
                dict[joiner.Table().AliasOrName()] = t.Alias;
              where.substituteColumnAliases(dict);
            }
            Joiner.JoinType jt2 = Joiner.JoinType.INNER_JOIN;
            if (t.HaveDefault())
              jt2 = Joiner.JoinType.LEFT_JOIN;
            Joiner j = new Joiner(jt2, (Table) t, this._query);
            j.On(where);
            this._query.AddJoin(j);
            break;
        }
      }
      else if (SQLinqExecutor.IsGroupingType(itemType))
      {
        this.VisitQueryModelForGrouping(((SubQueryExpression) ((FromClauseBase) fromClause).FromExpression).QueryModel);
      }
      else
      {
        System.Type dac = itemType.GetGenericArguments().Length == 1 && typeof (IBqlTable).IsAssignableFrom(itemType.GetGenericArguments()[0]) ? itemType.GetGenericArguments()[0] : throw new PXException("Incorrect from type. ");
        this._query.From(this.GetSQLTable(dac, tableAlias));
        this._info.Tables?.Add(dac);
      }
      base.VisitAdditionalFromClause(fromClause, queryModel, index);
    }

    public virtual void VisitMainFromClause(MainFromClause fromClause, QueryModel queryModel)
    {
      PXTrace.WithSourceLocation(nameof (VisitMainFromClause), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinq.cs", 904).Verbose<string, string, System.Type>("Remotion Visit {NodeType} {Name} of type {Type}", "MainFromClause", ((FromClauseBase) fromClause).ItemName, ((FromClauseBase) fromClause).ItemType);
      System.Type itemType = ((FromClauseBase) fromClause).ItemType;
      string tableAlias = this.GetTableAlias((IQuerySource) fromClause);
      ConstantExpression fromExpression1 = ((FromClauseBase) fromClause).FromExpression as ConstantExpression;
      Joiner.JoinType jt = Joiner.JoinType.MAIN_TABLE;
      IPXResultsetBase baseSelect = fromExpression1?.Value is ISQLQueryable sqlQueryable ? sqlQueryable.BaseSelect : (IPXResultsetBase) null;
      if (baseSelect != null)
      {
        PXView view = baseSelect.GetDelayedQuery().View;
        BqlCommand.Selection selection = new BqlCommand.Selection()
        {
          _Command = view.BqlSelect,
          UseColumnAliases = true,
          ParamCounter = this._info.Parameters != null ? this._info.Parameters.Count : 0
        };
        bool resetTopCount = false;
        view.prepareSorts((string[]) null, (bool[]) null, (object[]) null, 0, out bool _, out bool _, ref resetTopCount);
        this._info.BaseCommand = view.BqlSelect;
        List<IBqlSortColumn> sortColumns = this._info.SortColumns;
        this._info.SortColumns = new List<IBqlSortColumn>();
        Query queryInternal = view.BqlSelect.GetQueryInternal(view.Graph, (BqlCommandInfo) this._info, selection);
        if (this._query.GetOrder() == null && !queryModel.BodyClauses.Any<IBodyClause>((System.Func<IBodyClause, bool>) (bc => bc is OrderByClause)) && queryInternal.GetOrder() != null && !queryModel.ResultOperators.Any<ResultOperatorBase>((System.Func<ResultOperatorBase, bool>) (ro => ro is ValueFromSequenceResultOperatorBase || ro is GroupResultOperator)))
        {
          foreach (OrderSegment orderSegment in queryInternal.GetOrder())
            this._query.AddOrderSegment(new OrderSegment((SQLExpression) new Column(orderSegment.expr_.Alias(), tableAlias), orderSegment.ascending_));
          if (sortColumns == null)
            sortColumns = this._info.SortColumns;
          else
            sortColumns.AddRange((IEnumerable<IBqlSortColumn>) this._info.SortColumns);
        }
        this._info.SortColumns = sortColumns;
        if (baseSelect.GetDelayedQuery().arguments != null)
          this._info.Arguments.AddRange((IEnumerable<object>) baseSelect.GetDelayedQuery().arguments);
        queryInternal.GetOrder()?.Clear();
        object[] source = view.PrepareParametersInternal((object[]) null, baseSelect.GetDelayedQuery().arguments, this._info.Parameters.ToArray());
        this._query.AddJoin(new Joiner(jt, queryInternal.As(tableAlias), this._query));
        int count = this._info.Pars.Count;
        if (source != null)
        {
          foreach (object obj in ((IEnumerable<object>) source).Skip<object>(count))
          {
            PXDataValue pxDataValue = (PXDataValue) null;
            if (count < this._info.Parameters.Count)
            {
              PXCommandPreparingEventArgs.FieldDescription description = (PXCommandPreparingEventArgs.FieldDescription) null;
              System.Type referencedType = this._info.Parameters[count].GetReferencedType();
              view.Graph.Caches[BqlCommand.GetItemType(referencedType)]?.RaiseCommandPreparing(referencedType.Name, (object) null, obj, PXDBOperation.ReadOnly, (System.Type) null, out description);
              if (description != null)
                pxDataValue = !(this._info.Parameters[count].MaskedType == typeof (Array)) ? new PXDataValue(description.DataType, obj) : new PXDataValue(PXDbType.DirectExpression, obj);
            }
            this._info.Pars.Add(pxDataValue ?? new PXDataValue(obj));
            ++count;
          }
        }
      }
      else if (typeof (IBqlTable).IsAssignableFrom(itemType))
      {
        Table t;
        if (((FromClauseBase) fromClause).FromExpression is SubQueryExpression fromExpression2)
        {
          t = (Table) ((SubQuery) this.GetSQLExpression((Expression) fromExpression2)).Query();
          t.Alias = tableAlias;
        }
        else
          t = this.GetSQLTable(itemType, tableAlias);
        this._query.AddJoin(new Joiner(jt, t, this._query));
        this._info.Tables?.Add(itemType);
      }
      else if (SQLinqExecutor.IsGroupingType(itemType))
      {
        this.VisitQueryModelForGrouping(((SubQueryExpression) ((FromClauseBase) fromClause).FromExpression).QueryModel);
      }
      else
      {
        System.Type dac = itemType.GetGenericArguments().Length == 1 && typeof (IBqlTable).IsAssignableFrom(itemType.GetGenericArguments()[0]) ? itemType.GetGenericArguments()[0] : throw new PXException("Incorrect from type. ");
        this._query.From(this.GetSQLTable(dac, tableAlias));
        this._info.Tables?.Add(dac);
      }
      base.VisitMainFromClause(fromClause, queryModel);
    }

    private Table GetSQLTable(System.Type dac, string alias)
    {
      Table sqlTable;
      if (this._graph == null)
        sqlTable = (Table) new SimpleTable(dac, alias);
      else
        sqlTable = BqlCommand.GetSQLTable(dac, this._graph, info: new BqlCommandInfo(false)
        {
          UseColumnAliases = true
        });
      sqlTable.Alias = alias;
      return sqlTable;
    }

    public virtual void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
    {
      PXTrace.WithSourceLocation(nameof (VisitWhereClause), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinq.cs", 1020).Verbose<string>("Remotion Visit {NodeType}", "WhereClause");
      SQLExpression expressionCondition = this.GetSQLExpressionCondition(whereClause.Predicate);
      if (SQLinqExecutor.IsGroupingType(((FromClauseBase) queryModel.MainFromClause).ItemType))
      {
        SQLExpression having = this._query.GetHaving();
        this._query.Having(having == null ? expressionCondition : having.And(expressionCondition));
      }
      else
      {
        SQLExpression where = this._query.GetWhere();
        this._query.Where(where == null ? expressionCondition : where.And(expressionCondition));
      }
      base.VisitWhereClause(whereClause, queryModel, index);
    }

    private protected virtual SQLExpression GetSQLExpression(Expression expression)
    {
      return SQLinqExecutor.SQLinqExpressionVisitor.GetSQLExpression(this._info, this._graph, expression, this._tableAliases);
    }

    private protected virtual SQLExpression GetSQLExpressionCondition(Expression expression)
    {
      return SQLinqExecutor.SQLinqExpressionVisitor.GetSQLExpressionCondition(this._info, this._graph, expression, this._tableAliases);
    }

    private protected virtual Tuple<ProjectionItem, SQLExpression> GetSQLProjection(
      Expression expression)
    {
      return SQLinqExecutor.SQLinqExpressionVisitor.GetSQLProjection(this._info, this._graph, expression, this._tableAliases);
    }
  }

  internal class SQLinqExpressionVisitor : ThrowingExpressionVisitor
  {
    private protected readonly SQLinqBqlCommandInfo _info;
    private protected readonly PXGraph _graph;
    private protected SQLExpression _currExpr;
    private System.Type _currDAC;
    private protected ProjectionItem _currProj;
    private Dictionary<System.Type, List<string>> _extensionColumns = new Dictionary<System.Type, List<string>>();
    private bool _isBoolContext;
    private protected IDictionary<IQuerySource, string> _tableAliases;

    private SimpleTable _currTable
    {
      get => this._info.CurrTable;
      set => this._info.CurrTable = value;
    }

    private Dictionary<System.Type, (string alias, bool isExternal)> _currAliases
    {
      get => this._info.CurrAliases;
      set => this._info.CurrAliases = value;
    }

    private bool _isMySQL
    {
      get
      {
        return PXDatabase.Provider.SqlDialect.GetType().Name == "MySqlDialect" || PXDatabase.Provider.SqlDialect.GetType().Name == "MariaDBSqlDialect";
      }
    }

    internal static string GetTableAlias(
      IQuerySource src,
      IDictionary<IQuerySource, string> tableAliases)
    {
      string tableAlias;
      if (!tableAliases.TryGetValue(src, out tableAlias))
      {
        HashSet<string> stringSet = new HashSet<string>((IEnumerable<string>) tableAliases.Values);
        int num = 1;
        string str = src.ItemName ?? "t";
        tableAlias = str;
        while (stringSet.Contains(tableAlias))
        {
          tableAlias = str + num.ToString((IFormatProvider) CultureInfo.InvariantCulture);
          ++num;
        }
        tableAliases[src] = tableAlias;
      }
      return tableAlias;
    }

    private string GetTableAlias(IQuerySource src)
    {
      return SQLinqExecutor.SQLinqExpressionVisitor.GetTableAlias(src, this._tableAliases);
    }

    protected virtual Exception CreateUnhandledItemException<T>(T unhandledItem, string visitMethod)
    {
      return new Exception($"{unhandledItem.ToString()} in {visitMethod}");
    }

    private protected SQLinqExpressionVisitor(
      SQLinqBqlCommandInfo info,
      PXGraph g,
      IDictionary<IQuerySource, string> tableAliases)
    {
      this._info = info;
      this._graph = g;
      this._tableAliases = tableAliases;
    }

    private protected virtual SQLExpression GetSQLExpression(Expression linqExpression)
    {
      return SQLinqExecutor.SQLinqExpressionVisitor.GetSQLExpression(this._info, this._graph, linqExpression, this._tableAliases);
    }

    public static SQLExpression GetSQLExpression(
      SQLinqBqlCommandInfo info,
      PXGraph g,
      Expression linqExpression,
      IDictionary<IQuerySource, string> tableAliases)
    {
      SQLinqExecutor.SQLinqExpressionVisitor expressionVisitor = new SQLinqExecutor.SQLinqExpressionVisitor(info, g, tableAliases);
      ((ExpressionVisitor) expressionVisitor).Visit(linqExpression);
      return expressionVisitor._currExpr;
    }

    public static SQLExpression GetSQLExpressionCondition(
      SQLinqBqlCommandInfo info,
      PXGraph g,
      Expression linqExpression,
      IDictionary<IQuerySource, string> tableAliases)
    {
      SQLinqExecutor.SQLinqExpressionVisitor expressionVisitor = new SQLinqExecutor.SQLinqExpressionVisitor(info, g, tableAliases);
      expressionVisitor.Visit(linqExpression, true);
      return expressionVisitor._currExpr;
    }

    public static Tuple<ProjectionItem, SQLExpression> GetSQLProjection(
      SQLinqBqlCommandInfo info,
      PXGraph g,
      Expression linqExpression,
      IDictionary<IQuerySource, string> tableAliases)
    {
      SQLinqExecutor.SQLinqExpressionVisitor expressionVisitor = new SQLinqExecutor.SQLinqExpressionVisitor(info, g, tableAliases);
      ((ExpressionVisitor) expressionVisitor).Visit(linqExpression);
      expressionVisitor.ValidateSubQuery(linqExpression.Type);
      return Tuple.Create<ProjectionItem, SQLExpression>(expressionVisitor._currProj, expressionVisitor._currExpr);
    }

    private protected Expression Visit(Expression expression, bool isBool)
    {
      bool isBoolContext = this._isBoolContext;
      this._isBoolContext = isBool;
      try
      {
        return ((ExpressionVisitor) this).Visit(expression);
      }
      finally
      {
        this._isBoolContext = isBoolContext;
      }
    }

    public virtual Expression Visit(Expression expression)
    {
      Expression expression1 = base.Visit(expression);
      if (this._currExpr == null || this._isBoolContext || !this._currExpr.IsBoolType())
        return expression1;
      this._currExpr = (SQLExpression) new SQLSwitch().Case(this._currExpr, (SQLExpression) new SQLConst((object) true)).Default((SQLExpression) new SQLConst((object) false));
      this._currProj = (ProjectionItem) new ProjectionConst(typeof (bool));
      return expression1;
    }

    protected virtual Expression VisitQuerySourceReference(QuerySourceReferenceExpression expression)
    {
      PXTrace.WithSourceLocation(nameof (VisitQuerySourceReference), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinq.cs", 1156).Verbose<string>("Remotion Visit {NodeType}", "QuerySourceReference");
      return typeof (IBqlTable).IsAssignableFrom(((Expression) expression).Type) && (expression.ReferencedQuerySource is MainFromClause referencedQuerySource ? ((FromClauseBase) referencedQuerySource).FromExpression : (Expression) null) is QuerySourceReferenceExpression fromExpression ? ((RelinqExpressionVisitor) this).VisitQuerySourceReference(fromExpression) : this.VisitQuerySourceReferenceInternal(expression);
    }

    private Expression VisitQuerySourceReferenceInternal(QuerySourceReferenceExpression expression)
    {
      System.Type type = ((Expression) expression).Type;
      string tableAlias = this.GetTableAlias(expression.ReferencedQuerySource);
      if (typeof (IBqlTable).IsAssignableFrom(type))
      {
        this._currDAC = type;
        this._currTable = new SimpleTable(type.Name, tableAlias);
        this._currExpr = SQLinqExecutor.SQLinqExpressionVisitor.GetColumnsForDac(this._graph, type, expression.ReferencedQuerySource, tableAlias);
        this._currProj = (ProjectionItem) new ProjectionReference(type);
      }
      else if (type.Name == "IEnumerable`1" && typeof (IBqlTable).IsAssignableFrom(type.GetGenericArguments()[0]))
      {
        this._currDAC = type;
        this._currTable = new SimpleTable(type.Name, tableAlias);
        this._currExpr = SQLinqExecutor.SQLinqExpressionVisitor.GetColumnsForDac(this._graph, type.GetGenericArguments()[0], expression.ReferencedQuerySource, tableAlias);
        this._currProj = (ProjectionItem) new ProjectionReferenceEnumerable(type);
      }
      else if (type.IsGenericType && type.GetGenericTypeDefinition().IsSubclassOf(typeof (PXResult)))
      {
        this._currDAC = type;
        this._currExpr = SQLExpression.None();
        BqlCommand bqlSelect = ((expression.ReferencedQuerySource is MainFromClause referencedQuerySource ? ((FromClauseBase) referencedQuerySource).FromExpression : (Expression) null) is ConstantExpression fromExpression ? fromExpression.Value : (object) null) is ISQLQueryable sqlQueryable ? sqlQueryable.BaseSelect?.GetDelayedQuery()?.View.BqlSelect : (BqlCommand) null;
        System.Type[] result = bqlSelect == null || type.GenericTypeArguments.Length > 1 ? type.GenericTypeArguments : bqlSelect.GetTables();
        foreach (System.Type exprType in result)
          this._currExpr = this._currExpr.Seq(this.SetExternalColumns(exprType, tableAlias));
        ProjectionPXResult projectionPxResult = new ProjectionPXResult(result);
        if (projectionPxResult.HasCount)
          this._currExpr = this._currExpr.Seq((SQLExpression) new Column("Count", tableAlias));
        this._currProj = (ProjectionItem) projectionPxResult;
        this._currTable = new SimpleTable(type.Name, tableAlias);
      }
      else if (SQLinqExecutor.IsGroupingType(type))
      {
        QueryModel modelWithGrouping = SQLinqExecutor.GetQueryModelWithGrouping(expression);
        ((ExpressionVisitor) this).Visit(modelWithGrouping.ResultOperators.OfType<GroupResultOperator>().First<GroupResultOperator>().ElementSelector ?? modelWithGrouping.SelectClause.Selector);
        if (this._currTable == null)
          this._currTable = new SimpleTable(tableAlias, tableAlias);
      }
      else
      {
        System.Type dac = type.GetGenericArguments().Length == 1 && typeof (IBqlTable).IsAssignableFrom(type.GetGenericArguments()[0]) ? type.GetGenericArguments()[0] : throw new PXNotSupportedException("Incorrect result type. ");
        this._currTable = new SimpleTable(dac.Name, tableAlias);
        this._currExpr = SQLinqExecutor.SQLinqExpressionVisitor.GetColumnsForDac(this._graph, dac, expression.ReferencedQuerySource, tableAlias);
        this._currProj = (ProjectionItem) new ProjectionReference(dac);
      }
      return (Expression) expression;
    }

    private static SQLExpression GetColumnsForDac(
      PXGraph graph,
      System.Type dac,
      IQuerySource querySource,
      string alias = null)
    {
      PXCache cach = graph?.Caches[dac];
      return cach != null && (cach.BqlSelect != null || (querySource is MainFromClause mainFromClause ? ((FromClauseBase) mainFromClause).FromExpression : (Expression) null) is SubQueryExpression) ? Column.ExternalColumns(graph, dac, alias) : Column.Columns(graph, dac, alias);
    }

    protected virtual Expression VisitNew(NewExpression expression)
    {
      PXTrace.WithSourceLocation(nameof (VisitNew), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinq.cs", 1233).Verbose<string>("Remotion Visit {NodeType}", "New");
      ProjectionNew projectionNew = new ProjectionNew(expression.Constructor, expression.Arguments.Count);
      SQLExpression sqlExpression = SQLExpression.None();
      for (int index = 0; index < expression.Arguments.Count; ++index)
      {
        ((ExpressionVisitor) this).Visit(expression.Arguments[index]);
        this._currExpr.SetAlias(expression.Members?[index].Name);
        this._currExpr.BindTo(expression.Arguments[index].Type);
        sqlExpression = sqlExpression.Seq(this._currExpr);
        projectionNew.SetElement(index, expression.Members?[index].Name, this._currProj);
      }
      this._currExpr = sqlExpression;
      this._currProj = (ProjectionItem) projectionNew;
      return (Expression) expression;
    }

    protected virtual Expression VisitMemberInit(MemberInitExpression expression)
    {
      ((ExpressionVisitor) this).Visit((Expression) expression.NewExpression);
      ProjectionInitNew projectionInitNew = new ProjectionInitNew((ProjectionNew) this._currProj, expression.Bindings.Count);
      SQLExpression sqlExpression = this._currExpr;
      for (int index = 0; index < expression.Bindings.Count; ++index)
      {
        MemberAssignment binding = (MemberAssignment) expression.Bindings[index];
        if (binding.Expression.NodeType != ExpressionType.Constant)
        {
          if (binding.Expression.NodeType == ExpressionType.Call && binding.Expression is MethodCallExpression expression1 && expression1.Method.Name == "GetExtensionProperty")
          {
            if (!this._extensionColumns.ContainsKey(expression1.Arguments[0].Type))
              this._extensionColumns[expression1.Arguments[0].Type] = new List<string>();
            this._extensionColumns[expression1.Arguments[0].Type].Add(((ConstantExpression) expression1.Arguments[1]).Value.ToString());
          }
          ((ExpressionVisitor) this).Visit(binding.Expression);
          this.ValidateSubQuery(binding.Expression.Type);
        }
        else
        {
          projectionInitNew.Constants[binding.Member.Name] = ((ConstantExpression) binding.Expression).Value;
          this._currExpr = SQLExpression.Null();
        }
        this._currExpr.SetAlias(binding.Member.Name);
        sqlExpression = sqlExpression.Seq(this._currExpr);
        projectionInitNew.SetMember(index, binding.Member, this._currProj);
      }
      this._currExpr = sqlExpression;
      this._currProj = (ProjectionItem) projectionInitNew;
      return (Expression) expression;
    }

    protected virtual Expression VisitMethodCall(MethodCallExpression expression)
    {
      PXTrace.WithSourceLocation(nameof (VisitMethodCall), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinq.cs", 1296).Verbose<string, string, string>("Remotion Visit {NodeType} {ClassName}.{MethodName}", "MethodCall", expression.Method.DeclaringType.FullName, expression.Method.Name);
      SQLExpression arguments = SQLExpression.None();
      foreach (Expression node in expression.Arguments)
      {
        ((ExpressionVisitor) this).Visit(node);
        arguments = arguments.Seq(this._currExpr);
      }
      ((ExpressionVisitor) this).Visit(expression.Object);
      ProjectionItem currProj = this._currProj;
      this._currProj = (ProjectionItem) new ProjectionConst(expression.Type);
      string name = expression.Method.Name;
      if (name != null)
      {
        switch (name.Length)
        {
          case 3:
            switch (name[0])
            {
              case 'A':
                if (name == "Abs")
                {
                  this._currExpr = arguments.Abs();
                  break;
                }
                goto label_224;
              case 'L':
                if (name == "Len")
                {
                  this._currExpr = arguments.Length();
                  break;
                }
                goto label_224;
              case 'N':
                if (name == "New" && expression.Method.DeclaringType == typeof (PXResult))
                {
                  System.Type type = expression.Type;
                  this._currDAC = type;
                  this._currExpr = arguments;
                  this._currAliases = new Dictionary<System.Type, (string, bool)>();
                  QuerySourceReferenceExpression referenceExpression1 = expression.Arguments[0] as QuerySourceReferenceExpression;
                  foreach (System.Type genericTypeArgument in expression.Arguments[0].Type.GenericTypeArguments)
                    this._currAliases[genericTypeArgument] = (this.GetTableAlias(referenceExpression1.ReferencedQuerySource), true);
                  for (int index = 1; index < expression.Arguments.Count; ++index)
                  {
                    QuerySourceReferenceExpression referenceExpression2 = expression.Arguments[index] as QuerySourceReferenceExpression;
                    this._currAliases[type.GenericTypeArguments[index]] = (this.GetTableAlias(referenceExpression2.ReferencedQuerySource), false);
                  }
                  this._currProj = (ProjectionItem) new ProjectionPXResult(type.GenericTypeArguments);
                  break;
                }
                goto label_224;
              case 'P':
                if (name == "Pow")
                {
                  this._currExpr = arguments.LExpr().Power(arguments.RExpr());
                  break;
                }
                goto label_224;
              default:
                goto label_224;
            }
            break;
          case 4:
            switch (name[0])
            {
              case 'I':
                if (name == "IsIn")
                {
                  this._currExpr = this.GetParsedInClause(arguments, expression.Arguments.Count);
                  break;
                }
                goto label_224;
              case 'L':
                if (name == "Like")
                {
                  this._currExpr = arguments.LExpr()?.Like(arguments.RExpr());
                  break;
                }
                goto label_224;
              case 'T':
                if (name == "Trim")
                {
                  if (expression.Object == null)
                  {
                    if (expression.Arguments.Count != 1)
                      throw new InvalidOperationException("Wrong number of parameters for SQL.TrimStart(pos, len) call. ");
                    this._currExpr = arguments.TrimStart().TrimEnd();
                    break;
                  }
                  this._currExpr = this._currExpr.TrimStart().TrimEnd();
                  break;
                }
                goto label_224;
              default:
                goto label_224;
            }
            break;
          case 5:
            switch (name[0])
            {
              case 'F':
                if (name == "Floor")
                {
                  this._currExpr = arguments.Floor();
                  break;
                }
                goto label_224;
              case 'R':
                if (name == "Round")
                {
                  if (expression.Arguments.Count == 1)
                  {
                    this._currExpr = arguments.Round(0);
                    break;
                  }
                  if (expression.Arguments.Count != 2)
                    throw new InvalidOperationException("Wrong number of parameters for SQL.Round(precision) call. ");
                  this._currExpr = arguments.LExpr().Round(arguments.RExpr());
                  break;
                }
                goto label_224;
              default:
                goto label_224;
            }
            break;
          case 6:
            switch (name[1])
            {
              case 'n':
                if (name == "Unwrap")
                {
                  this._currProj = currProj;
                  if (expression.Arguments.Count != 1 && expression.Arguments.Count != 2)
                    throw new PXNotSupportedException("Unknown signature of Unwrap() method call. ");
                  throw new PXNotSupportedException("PXResult.Unwrap() not yet implemented. ");
                }
                goto label_224;
              case 'o':
                if (name == "Concat")
                {
                  this._currExpr = arguments.LExpr().Concat(arguments.RExpr());
                  break;
                }
                goto label_224;
              case 'q':
                if (name == "Equals")
                {
                  if (expression.Method.DeclaringType == typeof (string))
                  {
                    if (expression.Arguments.Count == 2)
                    {
                      this._currExpr = arguments.LExpr()?.Equal(arguments.RExpr());
                      break;
                    }
                    if (expression.Arguments.Count != 3)
                      throw new PXNotSupportedException("Unknown signature of String.Equals() method call");
                    object obj = expression.Arguments[2] is ConstantExpression constantExpression ? constantExpression.Value : (object) null;
                    if (obj == null)
                    {
                      this._currExpr = arguments.LExpr()?.Equal(arguments.RExpr());
                      break;
                    }
                    StringComparison comparison = (StringComparison) obj;
                    this._currExpr = arguments.LExpr().LExpr()?.Equal(arguments.LExpr().RExpr(), comparison);
                    break;
                  }
                  if (expression.Arguments.Count != 2)
                    throw new PXNotSupportedException("Unknown signature of String.Equals() method call");
                  this._currExpr = arguments.LExpr()?.Equal(arguments.RExpr(), StringComparison.CurrentCultureIgnoreCase);
                  break;
                }
                goto label_224;
              case 'r':
                if (name == "Create" && expression.Method.DeclaringType == typeof (Tuple))
                {
                  this._currProj = currProj;
                  ProjectionFabricMethod projectionFabricMethod = new ProjectionFabricMethod(expression.Method, expression.Arguments.Count);
                  SQLExpression sqlExpression = SQLExpression.None();
                  for (int index = 0; index < expression.Arguments.Count; ++index)
                  {
                    ((ExpressionVisitor) this).Visit(expression.Arguments[index]);
                    this._currExpr.BindTo(expression.Arguments[index].Type);
                    sqlExpression = sqlExpression.Seq(this._currExpr);
                    projectionFabricMethod.SetElement(index, this._currProj);
                  }
                  this._currExpr = sqlExpression;
                  this._currProj = (ProjectionItem) projectionFabricMethod;
                  break;
                }
                goto label_224;
              case 'u':
                if (name == "NullIf")
                {
                  if (expression.Arguments.Count != 2)
                    throw new InvalidOperationException("Wrong number of parameters for SQL.NullIf(exp, other) call. ");
                  this._currExpr = arguments.LExpr().NullIf(arguments.RExpr());
                  break;
                }
                goto label_224;
              default:
                goto label_224;
            }
            break;
          case 7:
            switch (name[2])
            {
              case 'L':
                if (name == "ToLower" && expression.Method.DeclaringType == typeof (string))
                {
                  this._currExpr = this._currExpr?.LowerCase();
                  break;
                }
                goto label_224;
              case 'N':
                if (name == "IsNotIn")
                {
                  this._currExpr = this.GetParsedNotInClause(arguments, expression.Arguments.Count);
                  break;
                }
                goto label_224;
              case 'P':
                if (name == "AsParam")
                {
                  int num = this._info.Parameters.Count<IBqlParameter>((System.Func<IBqlParameter, bool>) (p => p.IsVisible || p is FieldNameParam));
                  while (this._info.Arguments.Count < num)
                    this._info.Arguments.Add((object) null);
                  this._currExpr = (SQLExpression) Literal.NewParameter(this._info.Parameters.Count);
                  object obj = expression.Arguments[0] is ConstantExpression constantExpression ? constantExpression.Value : (object) null;
                  this._info.Arguments.Add(obj);
                  this._info.Pars.Add(new PXDataValue(obj));
                  this._info.Parameters.Add(this.GetBqlParameter(expression.Type));
                  break;
                }
                goto label_224;
              case 'U':
                if (name == "ToUpper" && expression.Method.DeclaringType == typeof (string))
                {
                  this._currExpr = this._currExpr?.UpperCase();
                  break;
                }
                goto label_224;
              case 'd':
                if (name == "IndexOf" && expression.Method.DeclaringType == typeof (string))
                {
                  this._currExpr = this._currExpr?.CharIndex((SQLExpression) new SQLConst(((ConstantExpression) expression.Arguments[0]).Value));
                  break;
                }
                goto label_224;
              case 'i':
                switch (name)
                {
                  case "TrimEnd":
                    if (expression.Object == null)
                    {
                      if (expression.Arguments.Count != 1)
                        throw new InvalidOperationException("Wrong number of parameters for SQL.TrimEnd(pos, len) call. ");
                      this._currExpr = arguments.TrimEnd();
                      break;
                    }
                    this._currExpr = this._currExpr.TrimEnd();
                    break;
                  case "Ceiling":
                    this._currExpr = arguments.Ceiling();
                    break;
                  default:
                    goto label_224;
                }
                break;
              case 'm':
                if (name == "Compare")
                {
                  if (expression.Method.DeclaringType == typeof (string))
                  {
                    StringComparison[] source = new StringComparison[1]
                    {
                      StringComparison.Ordinal
                    };
                    if (expression.Arguments.Count == 2)
                    {
                      this._currExpr = arguments.LExpr()?.Compare(arguments.RExpr());
                      break;
                    }
                    if (expression.Arguments.Count == 3)
                    {
                      switch (expression.Arguments[2] is ConstantExpression constantExpression1 ? constantExpression1.Value : (object) null)
                      {
                        case null:
                          this._currExpr = arguments.LExpr().LExpr()?.Compare(arguments.LExpr().RExpr());
                          break;
                        case bool flag1:
                          this._currExpr = arguments.LExpr().LExpr()?.Compare(arguments.LExpr().RExpr(), new StringComparison?(flag1 ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture));
                          break;
                        case StringComparison stringComparison1:
                          if (this._isMySQL && ((IEnumerable<StringComparison>) source).Contains<StringComparison>(stringComparison1))
                            throw new PXNotSupportedException($"Method String.Compare(str, str, {stringComparison1}) is not supported");
                          this._currExpr = arguments.LExpr().LExpr()?.Compare(arguments.LExpr().RExpr(), new StringComparison?(stringComparison1));
                          break;
                        default:
                          throw new PXNotSupportedException("Unknown signature of String.Compare() method call");
                      }
                    }
                    else
                    {
                      if (expression.Arguments.Count == 4)
                      {
                        object obj1 = expression.Arguments[2] is ConstantExpression constantExpression2 ? constantExpression2.Value : (object) null;
                        object obj2 = expression.Arguments[3] is ConstantExpression constantExpression3 ? constantExpression3.Value : (object) null;
                        if (obj1 != null && obj1 is bool ignoreCase && obj2 != null && obj2 is CultureInfo ci)
                        {
                          if (this._isMySQL && !ignoreCase)
                            throw new PXNotSupportedException("Method String.Compare(str, str, false, cultureInfo) is not supported");
                          this._currExpr = arguments.LExpr().LExpr().LExpr()?.Compare(arguments.LExpr().LExpr().RExpr(), ignoreCase, ci);
                          break;
                        }
                        if (obj1 != null && obj1 is CultureInfo && obj2 != null && obj2 is CompareOptions _)
                          throw new PXNotSupportedException("Method String.Compare(str, str, cultureInfo, compareOptions) is not supported");
                        throw new PXNotSupportedException("Unknown signature of String.Compare() method call");
                      }
                      if (expression.Arguments.Count == 5)
                      {
                        SQLExpression sqlExpression1 = arguments.LExpr().LExpr().LExpr().LExpr();
                        object obj3 = expression.Arguments[1] is ConstantExpression constantExpression4 ? constantExpression4.Value : (object) null;
                        SQLExpression sqlExpression2 = arguments.LExpr().LExpr().RExpr();
                        object obj4 = expression.Arguments[3] is ConstantExpression constantExpression5 ? constantExpression5.Value : (object) null;
                        object obj5 = expression.Arguments[4] is ConstantExpression constantExpression6 ? constantExpression6.Value : (object) null;
                        if (obj3 == null || !(obj3 is int num1) || obj4 == null || !(obj4 is int num2) || obj5 == null || !(obj5 is int num3))
                          throw new PXNotSupportedException("Unknown signature of String.Compare() method call");
                        uint uint32 = Convert.ToUInt32(num3);
                        uint pos1 = Convert.ToUInt32(num1) + 1U;
                        uint pos2 = Convert.ToUInt32(num2) + 1U;
                        this._currExpr = sqlExpression1.Substr(pos1, uint32).Compare(sqlExpression2.Substr(pos2, uint32));
                        break;
                      }
                      if (expression.Arguments.Count == 6)
                      {
                        SQLExpression sqlExpression3 = arguments.LExpr().LExpr().LExpr().LExpr().LExpr();
                        object obj6 = expression.Arguments[1] is ConstantExpression constantExpression7 ? constantExpression7.Value : (object) null;
                        SQLExpression sqlExpression4 = arguments.LExpr().LExpr().LExpr().RExpr();
                        object obj7 = expression.Arguments[3] is ConstantExpression constantExpression8 ? constantExpression8.Value : (object) null;
                        object obj8 = expression.Arguments[4] is ConstantExpression constantExpression9 ? constantExpression9.Value : (object) null;
                        object obj9 = expression.Arguments[5] is ConstantExpression constantExpression10 ? constantExpression10.Value : (object) null;
                        if (obj6 == null || !(obj6 is int num4) || obj7 == null || !(obj7 is int num5) || obj8 == null || !(obj8 is int num6) || obj9 == null)
                          throw new PXNotSupportedException("Unknown signature of String.Compare() method call");
                        uint uint32 = Convert.ToUInt32(num6);
                        uint pos3 = Convert.ToUInt32(num4) + 1U;
                        uint pos4 = Convert.ToUInt32(num5) + 1U;
                        switch (obj9)
                        {
                          case bool flag2:
                            this._currExpr = sqlExpression3.Substr(pos3, uint32).Compare(sqlExpression4.Substr(pos4, uint32), new StringComparison?(flag2 ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture));
                            break;
                          case StringComparison stringComparison2:
                            if (this._isMySQL && ((IEnumerable<StringComparison>) source).Contains<StringComparison>(stringComparison2))
                              throw new PXNotSupportedException($"Method String.Compare(str, int, str, int, int, {stringComparison2}) is not supported");
                            this._currExpr = sqlExpression3.Substr(pos3, uint32).Compare(sqlExpression4.Substr(pos4, uint32), new StringComparison?(stringComparison2));
                            break;
                          default:
                            throw new PXNotSupportedException("Unknown signature of String.Compare() method call");
                        }
                      }
                      else
                      {
                        if (expression.Arguments.Count != 7)
                          throw new PXNotSupportedException("Unknown signature of String.Compare() method call");
                        SQLExpression sqlExpression5 = arguments.LExpr().LExpr().LExpr().LExpr().LExpr().LExpr();
                        object obj10 = expression.Arguments[1] is ConstantExpression constantExpression11 ? constantExpression11.Value : (object) null;
                        SQLExpression sqlExpression6 = arguments.LExpr().LExpr().LExpr().LExpr().RExpr();
                        object obj11 = expression.Arguments[3] is ConstantExpression constantExpression12 ? constantExpression12.Value : (object) null;
                        object obj12 = expression.Arguments[4] is ConstantExpression constantExpression13 ? constantExpression13.Value : (object) null;
                        object obj13 = expression.Arguments[5] is ConstantExpression constantExpression14 ? constantExpression14.Value : (object) null;
                        object obj14 = expression.Arguments[6] is ConstantExpression constantExpression15 ? constantExpression15.Value : (object) null;
                        if (obj10 != null && obj10 is int num7 && obj11 != null && obj11 is int num8 && obj12 != null && obj12 is int num9 && obj13 != null)
                        {
                          uint uint32 = Convert.ToUInt32(num9);
                          uint pos5 = Convert.ToUInt32(num7) + 1U;
                          uint pos6 = Convert.ToUInt32(num8) + 1U;
                          if (obj13 != null && obj13 is bool ignoreCase && obj14 != null && obj14 is CultureInfo ci)
                          {
                            if (this._isMySQL && !ignoreCase)
                              throw new PXNotSupportedException("Method String.Compare(str, int, str, int, int, false, cultureInfo) is not supported");
                            this._currExpr = sqlExpression5.Substr(pos5, uint32).Compare(sqlExpression6.Substr(pos6, uint32), ignoreCase, ci);
                            break;
                          }
                          if (obj13 != null && obj13 is CultureInfo && obj14 != null && obj14 is CompareOptions _)
                            throw new PXNotSupportedException("Method String.Compare(strA, indexA, strB, indexB, length, culture, options) is not supported");
                          throw new PXNotSupportedException("Unknown signature of String.Compare() method call");
                        }
                        break;
                      }
                    }
                  }
                  else
                  {
                    if (expression.Arguments.Count != 2)
                      throw new PXNotSupportedException("Unknown signature of PXCollationComparer.Compare() method call");
                    this._currExpr = arguments.LExpr()?.Compare(arguments.RExpr(), new StringComparison?(StringComparison.CurrentCultureIgnoreCase));
                    break;
                  }
                }
                else
                  goto label_224;
                break;
              case 'n':
                if (name == "Convert")
                {
                  System.Type[] genericTypeArguments = expression.Type.GenericTypeArguments;
                  this._currDAC = expression.Type;
                  this._currExpr = SQLExpression.None();
                  if (expression.Object is QuerySourceReferenceExpression referenceExpression)
                  {
                    string n = this.GetTableAlias(referenceExpression.ReferencedQuerySource);
                    this._currAliases = new Dictionary<System.Type, (string, bool)>();
                    BqlCommand bqlSelect = ((referenceExpression.ReferencedQuerySource is MainFromClause referencedQuerySource ? ((FromClauseBase) referencedQuerySource).FromExpression : (Expression) null) is ConstantExpression fromExpression ? fromExpression.Value : (object) null) is ISQLQueryable sqlQueryable ? sqlQueryable.BaseSelect?.GetDelayedQuery()?.View.BqlSelect : (BqlCommand) null;
                    if (bqlSelect != null)
                    {
                      System.Type[] tables = bqlSelect.GetTables();
                      HashSet<string> originalDacsHash = ((IEnumerable<System.Type>) tables).Select<System.Type, string>((System.Func<System.Type, string>) (x => x.FullName)).ToHashSet<string>();
                      EnumerableExtensions.ForEach<System.Type>((IEnumerable<System.Type>) genericTypeArguments, (System.Action<System.Type>) (x =>
                      {
                        if (!originalDacsHash.Contains(x.FullName))
                          throw new PXNotSupportedException($"The DAC {x.FullName} is not exist in query");
                      }));
                      EnumerableExtensions.ForEach<System.Type>((IEnumerable<System.Type>) tables, (System.Action<System.Type>) (x => this._currAliases[x] = (n, true)));
                    }
                    else
                      EnumerableExtensions.ForEach<System.Type>((IEnumerable<System.Type>) genericTypeArguments, (System.Action<System.Type>) (x => this._currAliases[x] = (n, true)));
                    foreach (System.Type exprType in genericTypeArguments)
                      this._currExpr = this._currExpr.Seq(this.SetExternalColumns(exprType, n));
                  }
                  else
                  {
                    foreach (System.Type type in genericTypeArguments)
                      this._currExpr = this._currExpr.Seq(this.SetExternalColumns(type, this._currAliases[type].alias));
                  }
                  this._currProj = (ProjectionItem) new ProjectionPXResult(genericTypeArguments);
                  break;
                }
                goto label_224;
              case 't':
                switch (name)
                {
                  case "GetItem":
                    System.Type typeForExpression1 = this.GetCacheExtensionTypeForExpression(expression.Type);
                    string alias1;
                    this.FillGetItemExpression(expression, typeForExpression1, out alias1);
                    this._currDAC = typeForExpression1;
                    this._currTable = new SimpleTable(typeForExpression1, alias1);
                    this._currProj = (ProjectionItem) new ProjectionReference(typeForExpression1);
                    break;
                  case "Between":
                    if (expression.Arguments.Count != 3)
                      throw new InvalidOperationException("Wrong number of parameters for SQL.Between(obj, min, max) call. ");
                    this._currExpr = arguments.LExpr().LExpr().Between(arguments.LExpr().RExpr(), arguments.RExpr());
                    break;
                  default:
                    goto label_224;
                }
                break;
              default:
                goto label_224;
            }
            break;
          case 8:
            switch (name[2])
            {
              case 'S':
                if (name == "ToString")
                {
                  this._currExpr = (SQLExpression) new SQLConvert(expression.Type, this._currExpr);
                  break;
                }
                goto label_224;
              case 'a':
                if (name == "Coalesce")
                {
                  if (expression.Arguments.Count != 2)
                    throw new InvalidOperationException("Wrong number of parameters for SQL.Coalesce(exp, replacement) call. ");
                  this._currExpr = arguments.LExpr().Coalesce(arguments.RExpr());
                  break;
                }
                goto label_224;
              case 'd':
                if (name == "EndsWith" && expression.Method.DeclaringType == typeof (string))
                {
                  this._currExpr = this._currExpr?.Like((SQLExpression) new SQLConst((object) ("%" + ((ConstantExpression) expression.Arguments[0]).Value?.ToString())));
                  break;
                }
                goto label_224;
              case 'n':
                if (name == "Contains" && expression.Method.DeclaringType == typeof (string))
                {
                  this._currExpr = this._currExpr?.Like((SQLExpression) new SQLConst((object) $"%{((ConstantExpression) expression.Arguments[0]).Value?.ToString()}%"));
                  break;
                }
                goto label_224;
              case 't':
                if (name == "get_Item")
                {
                  if (expression.Arguments.Count != 1 || !(expression.Arguments[0] is ConstantExpression constantExpression))
                    throw new PXNotSupportedException("Unknown signature of get_Item method call. ");
                  System.Type exprType = constantExpression.Value as System.Type;
                  if (exprType == (System.Type) null)
                  {
                    this._currExpr = arguments;
                    throw new PXNotSupportedException("Unknown method call: " + expression.Method.Name);
                  }
                  System.Type typeForExpression2 = this.GetCacheExtensionTypeForExpression(exprType);
                  string alias2;
                  this.FillGetItemExpression(expression, typeForExpression2, out alias2);
                  this._currDAC = typeForExpression2;
                  this._currTable = new SimpleTable(typeForExpression2, alias2);
                  this._currProj = (ProjectionItem) new ProjectionReference(typeForExpression2);
                  break;
                }
                goto label_224;
              default:
                goto label_224;
            }
            break;
          case 9:
            switch (name[0])
            {
              case 'B':
                if (name == "BinaryLen")
                {
                  this._currExpr = arguments.BinaryLength();
                  break;
                }
                goto label_224;
              case 'S':
                if (name == "Substring")
                {
                  if (expression.Object == null)
                  {
                    if (expression.Arguments.Count == 2)
                    {
                      this._currExpr = arguments.LExpr().Substr(arguments.RExpr(), (SQLExpression) null);
                      break;
                    }
                    if (expression.Arguments.Count != 3)
                      throw new InvalidOperationException("Wrong number of parameters for SQL.Substring(pos, len) call. ");
                    this._currExpr = arguments.LExpr().LExpr().Substr(arguments.LExpr().RExpr(), arguments.RExpr());
                    break;
                  }
                  SQLExpression len = (SQLExpression) null;
                  SQLExpression pos;
                  if (expression.Arguments.Count == 1)
                  {
                    pos = arguments;
                  }
                  else
                  {
                    if (expression.Arguments.Count != 2)
                      throw new InvalidOperationException("Wrong number of parameters for SQL.Substring(pos, len) call. ");
                    pos = arguments.LExpr();
                    len = arguments.RExpr();
                  }
                  this._currExpr = this._currExpr.Substr(pos, len);
                  break;
                }
                goto label_224;
              case 'T':
                if (name == "TrimStart")
                {
                  if (expression.Object == null)
                  {
                    if (expression.Arguments.Count != 1)
                      throw new InvalidOperationException("Wrong number of parameters for SQL.TrimStart(pos, len) call. ");
                    this._currExpr = arguments.TrimStart();
                    break;
                  }
                  this._currExpr = this._currExpr.TrimStart();
                  break;
                }
                goto label_224;
              default:
                goto label_224;
            }
            break;
          case 10:
            switch (name[0])
            {
              case 'N':
                if (name == "NotBetween")
                {
                  if (expression.Arguments.Count != 3)
                    throw new InvalidOperationException("Wrong number of parameters for SQL.Between(obj, min, max) call. ");
                  this._currExpr = arguments.LExpr().LExpr().NotBetween(arguments.LExpr().RExpr(), arguments.RExpr());
                  break;
                }
                goto label_224;
              case 'S':
                if (name == "StartsWith" && expression.Method.DeclaringType == typeof (string))
                {
                  this._currExpr = this._currExpr?.Like((SQLExpression) new SQLConst((object) (((ConstantExpression) expression.Arguments[0]).Value?.ToString() + "%")));
                  break;
                }
                goto label_224;
              default:
                goto label_224;
            }
            break;
          case 12:
            if (name == "GetExtension")
            {
              System.Type type = expression.Type;
              if (typeof (PXCacheExtension).IsAssignableFrom(type))
              {
                System.Type baseType = type.BaseType;
                if (((object) baseType != null ? (!baseType.IsGenericType ? 1 : 0) : 1) == 0)
                {
                  System.Type typeForExpression3 = this.GetCacheExtensionTypeForExpression(type);
                  string alias3;
                  this.FillGetItemExpression(expression, typeForExpression3, out alias3);
                  if (alias3 == null)
                    alias3 = this._currTable.Alias;
                  this._currDAC = typeForExpression3;
                  this._currTable = new SimpleTable(typeForExpression3, alias3);
                  this._currProj = (ProjectionItem) new ProjectionReference(typeForExpression3);
                  break;
                }
              }
              throw new PXNotSupportedException("Unknown signature of GetExtension method call. ");
            }
            goto label_224;
          case 14:
            if (name == "CompareOrdinal" && expression.Method.DeclaringType == typeof (string))
            {
              if (expression.Arguments.Count == 2)
              {
                if (this._isMySQL)
                  throw new PXNotSupportedException("Method String.CompareOrdinal(str,str) is not supported");
                this._currExpr = arguments.LExpr()?.Compare(arguments.RExpr(), new StringComparison?(StringComparison.Ordinal));
                break;
              }
              if (expression.Arguments.Count == 5)
              {
                if (this._isMySQL)
                  throw new PXNotSupportedException("Method CompareOrdinal(str, int, str, int, int) is not supported");
                SQLExpression sqlExpression7 = arguments.LExpr().LExpr().LExpr().LExpr();
                object obj15 = expression.Arguments[1] is ConstantExpression constantExpression16 ? constantExpression16.Value : (object) null;
                SQLExpression sqlExpression8 = arguments.LExpr().LExpr().RExpr();
                object obj16 = expression.Arguments[3] is ConstantExpression constantExpression17 ? constantExpression17.Value : (object) null;
                object obj17 = expression.Arguments[4] is ConstantExpression constantExpression18 ? constantExpression18.Value : (object) null;
                if (obj15 == null || !(obj15 is int num10) || obj16 == null || !(obj16 is int num11) || obj17 == null || !(obj17 is int num12))
                  throw new PXNotSupportedException("Unknown signature of String.CompareOrdinal() method call");
                uint uint32 = Convert.ToUInt32(num12);
                uint pos7 = Convert.ToUInt32(num10) + 1U;
                uint pos8 = Convert.ToUInt32(num11) + 1U;
                this._currExpr = sqlExpression7.Substr(pos7, uint32).Compare(sqlExpression8.Substr(pos8, uint32), new StringComparison?(StringComparison.Ordinal));
                break;
              }
              break;
            }
            goto label_224;
          case 20:
            if (name == "GetExtensionProperty" && expression.Method.DeclaringType == typeof (SQL))
            {
              this._currExpr = arguments.LExpr();
              if (this._currExpr is SubQuery currExpr && currExpr.Query().GetLimit() == 1)
              {
                System.Type sourceDacType = SQLinqExecutor.SQLinqExpressionVisitor.GetSourceDacType(expression.Arguments[0].Type, (Expression) expression);
                this.VisitSubQueryProperty(currExpr, sourceDacType, expression.Arguments[0].Type, expression.Type, (string) ((ConstantExpression) expression.Arguments[1]).Value);
                break;
              }
              this.VisitDacProperty(expression.Type, (string) ((ConstantExpression) expression.Arguments[1]).Value, SQLExpression.Null());
              break;
            }
            goto label_224;
          default:
            goto label_224;
        }
        return (Expression) expression;
      }
label_224:
      this._currExpr = arguments;
      throw new PXNotSupportedException("Unknown method call: " + expression.Method.Name);
    }

    private SQLExpression GetParsedInClause(SQLExpression arguments, int argCount)
    {
      return this.GetLExpressionByOrder(arguments, argCount).In(this.FillInNotInRExpressionEnumerable(arguments, argCount));
    }

    private SQLExpression GetParsedNotInClause(SQLExpression arguments, int argCount)
    {
      return this.GetLExpressionByOrder(arguments, argCount).NotIn(this.FillInNotInRExpressionEnumerable(arguments, argCount));
    }

    private void FillGetItemExpression(
      MethodCallExpression expression,
      System.Type exprType,
      out string alias)
    {
      if ((expression.Object is QuerySourceReferenceExpression referenceExpression ? referenceExpression.ReferencedQuerySource : (IQuerySource) null) is MainFromClause referencedQuerySource && SQLinqExecutor.IsGroupingType(((FromClauseBase) referencedQuerySource).FromExpression.Type) && ((FromClauseBase) referencedQuerySource).FromExpression is QuerySourceReferenceExpression fromExpression)
      {
        QueryModel modelWithGrouping = SQLinqExecutor.GetQueryModelWithGrouping(fromExpression);
        alias = this.GetTableAlias((IQuerySource) modelWithGrouping.MainFromClause);
      }
      else
        alias = referenceExpression != null ? this.GetTableAlias(referenceExpression.ReferencedQuerySource) : this._currAliases?[exprType].alias;
      Dictionary<System.Type, (string alias, bool isExternal)> currAliases = this._currAliases;
      // ISSUE: explicit non-virtual call
      if ((currAliases != null ? (!__nonvirtual (currAliases[exprType]).isExternal ? 1 : 0) : 0) != 0)
        this._currExpr = Column.Columns(this._graph, exprType, alias);
      else
        this._currExpr = this.SetExternalColumns(exprType, alias);
    }

    private SQLExpression SetExternalColumns(System.Type exprType, string alias)
    {
      if (!typeof (IBqlTable).IsAssignableFrom(exprType))
        return (SQLExpression) null;
      PXCache cach = this._graph.Caches[exprType];
      if (this._extensionColumns.ContainsKey(exprType))
        EnumerableExtensions.AddRange<string>((ISet<string>) cach._ForcedKeyValueAttributes, (IEnumerable<string>) this._extensionColumns[exprType]);
      return Column.ExternalColumns(this._graph, exprType, alias);
    }

    private System.Type GetCacheExtensionTypeForExpression(System.Type exprType)
    {
      if (!typeof (PXCacheExtension).IsAssignableFrom(exprType))
        return exprType;
      System.Type baseType = exprType.BaseType;
      if (((object) baseType != null ? (!baseType.IsGenericType ? 1 : 0) : 1) != 0)
        throw new PXNotSupportedException($"Unknown extension type {exprType}. ");
      System.Type genericArgument = exprType.BaseType.GetGenericArguments()[exprType.BaseType.GetGenericArguments().Length - 1];
      PXCache cach = this._graph.Caches[genericArgument];
      try
      {
        if (((IEnumerable<PXCacheExtension>) cach.GetCacheExtensions((IBqlTable) Activator.CreateInstance(genericArgument))).All<PXCacheExtension>((System.Func<PXCacheExtension, bool>) (e => e.GetType() != exprType)))
          throw new PXNotSupportedException($"Unknown extension type {exprType}. ");
      }
      catch (NullReferenceException ex)
      {
        throw new PXNotSupportedException($"Unknown extension type {exprType}. ", (Exception) ex);
      }
      return genericArgument;
    }

    private SQLExpression GetLExpressionByOrder(SQLExpression expression, int order)
    {
      return order == 2 ? expression.LExpr() : this.GetLExpressionByOrder(expression.LExpr(), --order);
    }

    private SQLExpression GetRExpressionByOrder(SQLExpression expression, int order)
    {
      return order == 2 ? expression.RExpr() : this.GetRExpressionByOrder(expression.LExpr(), --order);
    }

    private IEnumerable<SQLExpression> FillInNotInRExpressionEnumerable(
      SQLExpression expression,
      int count)
    {
      List<SQLExpression> sqlExpressionList = new List<SQLExpression>();
      for (int order = count; order > 1; --order)
      {
        SQLExpression rexpressionByOrder = this.GetRExpressionByOrder(expression, order);
        if (!(rexpressionByOrder is SQLConst sqlConst) || !(sqlConst.GetValue() is Array array) || array.Length != 0 || count <= 2)
          sqlExpressionList.Add(rexpressionByOrder);
      }
      return (IEnumerable<SQLExpression>) sqlExpressionList;
    }

    protected virtual Expression VisitMember(MemberExpression expression)
    {
      PXTrace.WithSourceLocation(nameof (VisitMember), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinq.cs", 2035).Verbose<string, string>("Remotion Visit {NodeType} {Name}", "Member", expression.Member.Name);
      if (typeof (IBqlTable).IsAssignableFrom(expression.Type))
      {
        MemberExpression memberExpression = expression;
        while (memberExpression.Expression is MemberExpression expression1)
          memberExpression = expression1;
        if (!(memberExpression.Expression is QuerySourceReferenceExpression expression2))
          throw new PXException("Incorrect type. ");
        this._currDAC = expression.Type;
        string name = expression.Member.Name;
        this._currTable = new SimpleTable(expression.Type.Name, name);
        this._currExpr = SQLinqExecutor.SQLinqExpressionVisitor.GetColumnsForDac(this._graph, expression.Type, expression2.ReferencedQuerySource, name);
        this._currProj = (ProjectionItem) new ProjectionReference(expression.Type);
        return (Expression) expression;
      }
      Expression memberSource = SQLinqExecutor.MemberSourceFinderExpressionVisitor.GetMemberSource((Expression) expression);
      ((ExpressionVisitor) this).Visit(memberSource);
      if (expression.Member.Name == "Length")
      {
        this._currExpr = this.GetSQLExpression(expression.Expression).Length();
        this._currProj = (ProjectionItem) new ProjectionConst(expression.Type);
      }
      else if (typeof (ITuple).IsAssignableFrom(expression.Member.DeclaringType))
      {
        if (!expression.Member.Name.StartsWith("Item"))
          throw new PXNotSupportedException("Unknown property of Tuple class: " + expression.Member.Name);
        int num = int.Parse(expression.Member.Name.Substring(4)) - 1;
        List<SQLExpression> sqlExpressionList = this._currExpr.DecomposeSequence();
        ProjectionFabricMethod currProj = (ProjectionFabricMethod) this._currProj;
        this._currExpr = sqlExpressionList[num];
        this._currProj = currProj.GetElement(num);
      }
      else if (SQLinqExecutor.IsGroupingType(expression.Expression.Type))
      {
        if (!(expression.Member.Name == "Key"))
          throw new PXNotSupportedException("Unknown property of IGrouping class: " + expression.Member.Name);
        ((ExpressionVisitor) this).Visit(SQLinqExecutor.GetQueryModelWithGrouping(expression).ResultOperators.OfType<GroupResultOperator>().First<GroupResultOperator>().KeySelector);
      }
      else
      {
        if (expression.Expression.Type == typeof (System.DateTime))
        {
          SQLExpression currExpr = this._currExpr;
          string name = expression.Member.Name;
          if (name != null)
          {
            switch (name.Length)
            {
              case 3:
                if (name == "Day")
                {
                  this._currExpr = (SQLExpression) new SQLDatePart((IConstant<string>) new DatePart.day(), currExpr);
                  break;
                }
                goto label_43;
              case 4:
                switch (name[0])
                {
                  case 'D':
                    if (name == "Date")
                    {
                      this._currExpr = (SQLExpression) new SQLDateAdd((IConstant<string>) new DatePart.day(), (SQLExpression) new SQLDateDiff((IConstant<string>) new DatePart.day(), (SQLExpression) new SQLConst((object) System.DateTime.MinValue), currExpr), (SQLExpression) new SQLConst((object) System.DateTime.MinValue));
                      break;
                    }
                    goto label_43;
                  case 'H':
                    if (name == "Hour")
                    {
                      this._currExpr = (SQLExpression) new SQLDatePart((IConstant<string>) new DatePart.hour(), currExpr);
                      break;
                    }
                    goto label_43;
                  case 'Y':
                    if (name == "Year")
                    {
                      this._currExpr = (SQLExpression) new SQLDatePart((IConstant<string>) new DatePart.year(), currExpr);
                      break;
                    }
                    goto label_43;
                  default:
                    goto label_43;
                }
                break;
              case 5:
                if (name == "Month")
                {
                  this._currExpr = (SQLExpression) new SQLDatePart((IConstant<string>) new DatePart.month(), currExpr);
                  break;
                }
                goto label_43;
              case 6:
                switch (name[0])
                {
                  case 'M':
                    if (name == "Minute")
                    {
                      this._currExpr = (SQLExpression) new SQLDatePart((IConstant<string>) new DatePart.minute(), currExpr);
                      break;
                    }
                    goto label_43;
                  case 'S':
                    if (name == "Second")
                    {
                      this._currExpr = (SQLExpression) new SQLDatePart((IConstant<string>) new DatePart.second(), currExpr);
                      break;
                    }
                    goto label_43;
                  default:
                    goto label_43;
                }
                break;
              case 9:
                switch (name[0])
                {
                  case 'D':
                    if (name == "DayOfYear")
                    {
                      this._currExpr = (SQLExpression) new SQLDatePart((IConstant<string>) new DatePart.dayOfYear(), currExpr);
                      break;
                    }
                    goto label_43;
                  case 'T':
                    if (name == "TimeOfDay")
                    {
                      this._currExpr = currExpr?.GetTime();
                      break;
                    }
                    goto label_43;
                  default:
                    goto label_43;
                }
                break;
              case 11:
                if (name == "Millisecond")
                {
                  this._currExpr = (SQLExpression) new SQLDatePart((IConstant<string>) new DatePart.millisecond(), currExpr);
                  break;
                }
                goto label_43;
              default:
                goto label_43;
            }
            this._currProj = (ProjectionItem) new ProjectionConst(expression.Type);
            goto label_50;
          }
label_43:
          throw new PXNotSupportedException("Unsupported property of DateTime class: " + expression.Member.Name);
        }
        if (this._currExpr is SubQuery currExpr1 && currExpr1.Query().GetLimit() == 1)
        {
          System.Type sourceDacType = SQLinqExecutor.SQLinqExpressionVisitor.GetSourceDacType(expression.Member.DeclaringType, memberSource);
          this.VisitSubQueryProperty(currExpr1, sourceDacType, expression.Member.DeclaringType, expression.Type, expression.Member.Name);
        }
        else
        {
          if ((this._currDAC == (System.Type) null || !typeof (IBqlTable).IsAssignableFrom(this._currDAC)) && typeof (IBqlTable).IsAssignableFrom(expression.Expression.Type))
            this._currDAC = expression.Expression.Type;
          this.VisitDacProperty(expression.Type, expression.Member.Name);
        }
      }
label_50:
      return (Expression) expression;
    }

    private static System.Type GetSourceDacType(System.Type memberType, Expression memberSource)
    {
      System.Type sourceDacType = memberType;
      if (memberSource is UnaryExpression unaryExpression && unaryExpression.Operand is SubQueryExpression operand)
        sourceDacType = ((Expression) operand).Type;
      return sourceDacType;
    }

    private void VisitSubQueryProperty(
      SubQuery subQuery,
      System.Type sourceDac,
      System.Type dac,
      System.Type propertyType,
      string propertyName)
    {
      Query query = subQuery.Query();
      string subAlias = BqlCommand.GetColumnAlias(propertyName, dac);
      string fieldName = propertyName;
      System.Type dac1 = sourceDac;
      if ((object) dac1 == null)
        dac1 = dac;
      string sourceSubAlias = BqlCommand.GetColumnAlias(fieldName, dac1);
      List<SQLExpression> selection = query.GetSelection();
      SQLExpression sqlExpression = selection != null ? selection.FirstOrDefault<SQLExpression>((System.Func<SQLExpression, bool>) (e => string.Equals(e.AliasOrName(), subAlias, StringComparison.OrdinalIgnoreCase) || string.Equals(e.AliasOrName(), propertyName, StringComparison.OrdinalIgnoreCase) || string.Equals(e.AliasOrName(), sourceSubAlias, StringComparison.OrdinalIgnoreCase))) : (SQLExpression) null;
      if (sqlExpression != null)
      {
        query.GetSelection().Clear();
        query.GetSelection().Add(sqlExpression);
        this._currExpr = (SQLExpression) subQuery;
        this._currProj = (ProjectionItem) new ProjectionConst(propertyType);
      }
      else
        throw new PXNotSupportedException($"Column {propertyName} is not found in DAC {dac.Name}. ");
    }

    private void VisitDacProperty(
      System.Type propertyType,
      string propertyName,
      SQLExpression defaultExpression = null)
    {
      SQLExpression sqlExpression = (SQLExpression) null;
      ProjectionItem projectionItem = (ProjectionItem) null;
      Column externalColumn1 = GetExternalColumn(propertyName);
      int num;
      if (this._graph != null)
      {
        PropertyInfo property = this._currDAC.GetProperty(propertyName);
        num = (object) property != null ? (property.IsDefined(typeof (PXDBLocalizableStringAttribute), true) ? 1 : 0) : 0;
      }
      else
        num = 0;
      bool flag = num != 0;
      if (externalColumn1 != null & flag)
      {
        projectionItem = (ProjectionItem) new ProjectionDBLocalizableFields(this._graph, this._currDAC, propertyType, propertyName);
        sqlExpression = (SQLExpression) new Column(externalColumn1.Name, (Table) this._currTable, externalColumn1.GetDBType());
      }
      else
      {
        if (externalColumn1 != null)
        {
          PXGraph graph = this._graph;
          if ((graph != null ? (graph.Caches[this._currDAC].IsKvExtAttribute(propertyName) ? 1 : 0) : 0) != 0)
          {
            projectionItem = (ProjectionItem) new ProjectionKvExtAttributes(this._currDAC, propertyName);
            sqlExpression = (SQLExpression) new Column(externalColumn1.Name, (Table) this._currTable, externalColumn1.GetDBType());
            goto label_20;
          }
        }
        if (externalColumn1 != null || defaultExpression != null)
        {
          sqlExpression = externalColumn1 != null ? (SQLExpression) new Column(externalColumn1.Name, (Table) this._currTable, externalColumn1.GetDBType()) : defaultExpression;
          projectionItem = (ProjectionItem) new ProjectionConst(propertyType);
        }
        else if (this._graph != null)
        {
          PropertyInfo property = this._currDAC.GetProperty(propertyName);
          if (property.IsDefined(typeof (PXDependsOnFieldsAttribute), true) || property.GetGetMethod(true).IsDefined(typeof (PXDependsOnFieldsAttribute), true))
          {
            HashSet<string> dependsRecursive = PXDependsOnFieldsAttribute.GetDependsRecursive(this._graph.Caches[this._currDAC], property.Name);
            foreach (string name in dependsRecursive)
            {
              Column externalColumn2 = GetExternalColumn(name);
              if (externalColumn2 != null)
                sqlExpression = sqlExpression == null ? (SQLExpression) new Column(externalColumn2.Name, (Table) this._currTable, externalColumn2.GetDBType()) : sqlExpression.Seq((SQLExpression) new Column(externalColumn2.Name, (Table) this._currTable, externalColumn2.GetDBType()));
            }
            projectionItem = sqlExpression != null ? (ProjectionItem) new ProjectionDependsOnFields(this._currDAC, propertyType, propertyName, dependsRecursive) : (ProjectionItem) null;
          }
        }
      }
label_20:
      this._currExpr = sqlExpression != null && projectionItem != null ? sqlExpression : throw new PXNotSupportedException($"Column {propertyName} is not found in DAC {this._currDAC.Name}. ");
      this._currProj = projectionItem;
      if (this._info.Fields == null || this._graph == null)
        return;
      System.Type bqlField = this._graph.Caches[this._currDAC].GetBqlField(propertyName);
      if (!(bqlField != (System.Type) null))
        return;
      this._info.Fields.Add(bqlField);

      Column GetExternalColumn(string name)
      {
        List<SQLExpression> sqlExpressionList1 = new List<SQLExpression>();
        string subAlias = BqlCommand.GetColumnAlias(name, this._currDAC);
        this._currExpr?.FillExpressionsOfType((Predicate<SQLExpression>) (e => e is Column column1 && string.Equals(column1.Name, subAlias, StringComparison.OrdinalIgnoreCase)), sqlExpressionList1);
        Column externalColumn = sqlExpressionList1.Cast<Column>().FirstOrDefault<Column>();
        if (externalColumn == null)
        {
          List<SQLExpression> sqlExpressionList2 = new List<SQLExpression>();
          this._currExpr?.FillExpressionsOfType((Predicate<SQLExpression>) (e => e is Column column2 && string.Equals(column2.Name, name, StringComparison.OrdinalIgnoreCase)), sqlExpressionList2);
          externalColumn = sqlExpressionList2.Cast<Column>().FirstOrDefault<Column>();
        }
        return externalColumn;
      }
    }

    protected virtual Expression VisitConditional(ConditionalExpression expression)
    {
      PXTrace.WithSourceLocation(nameof (VisitConditional), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinq.cs", 2251).Verbose<string>("Remotion Visit {NodeType}", "Conditional");
      this.Visit(expression.Test, true);
      SQLExpression currExpr1 = this._currExpr;
      ((ExpressionVisitor) this).Visit(expression.IfTrue);
      SQLExpression currExpr2 = this._currExpr;
      ((ExpressionVisitor) this).Visit(expression.IfFalse);
      SQLExpression currExpr3 = this._currExpr;
      SQLSwitch sqlSwitch1 = new SQLSwitch();
      sqlSwitch1.Case(currExpr1, currExpr2);
      if (currExpr3 is SQLSwitch sqlSwitch2)
      {
        sqlSwitch1.GetCases().AddRange((IEnumerable<Tuple<SQLExpression, SQLExpression>>) sqlSwitch2.GetCases());
        sqlSwitch1.Default(sqlSwitch2.GetDefault());
      }
      else
        sqlSwitch1.Default(currExpr3);
      this._currExpr = (SQLExpression) sqlSwitch1;
      this._currProj = (ProjectionItem) new ProjectionConst(expression.Type);
      return (Expression) expression;
    }

    protected virtual Expression VisitSubQuery(SubQueryExpression expression)
    {
      PXTrace.WithSourceLocation(nameof (VisitSubQuery), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinq.cs", 2277).Verbose<string>("Remotion Visit {NodeType}", "SubQuery");
      ObservableCollection<ResultOperatorBase> resultOperators = expression.QueryModel.ResultOperators;
      Expression fromExpression = ((FromClauseBase) expression.QueryModel.MainFromClause).FromExpression;
      if (SQLinqExecutor.IsGroupingType(fromExpression.Type))
      {
        ResultOperatorBase resultOperatorBase = resultOperators != null && resultOperators.Any<ResultOperatorBase>() ? resultOperators.First<ResultOperatorBase>() : throw new PXNotSupportedException("Subquery with grouping doesn't have result operator");
        switch (resultOperatorBase)
        {
          case MinResultOperator _:
            this._currExpr = this.GetSQLExpression(expression.QueryModel.SelectClause.Selector).Min();
            break;
          case MaxResultOperator _:
            this._currExpr = this.GetSQLExpression(expression.QueryModel.SelectClause.Selector).Max();
            break;
          case CountResultOperator _:
          case LongCountResultOperator _:
            this._currExpr = SQLExpression.Count();
            break;
          case SumResultOperator _:
            this._currExpr = this.GetSQLExpression(expression.QueryModel.SelectClause.Selector).Sum();
            break;
          case AverageResultOperator _:
            this._currExpr = this.GetSQLExpression(expression.QueryModel.SelectClause.Selector).Avg();
            break;
          default:
            throw new PXNotSupportedException($"Unsupported result operator: {resultOperatorBase.GetType()}");
        }
        this._currProj = (ProjectionItem) new ProjectionConst(((Expression) expression).Type);
        return (Expression) expression;
      }
      // ISSUE: explicit non-virtual call
      if (((Expression) expression).Type == typeof (int) && resultOperators != null && __nonvirtual (resultOperators.Count) == 1 && (resultOperators[0] is CountResultOperator || resultOperators[0] is LongCountResultOperator) && fromExpression.Type == typeof (byte[]))
      {
        ((ExpressionVisitor) this).Visit(fromExpression);
        this._currExpr = this._currExpr?.Length();
        this._currProj = (ProjectionItem) null;
        return (Expression) expression;
      }
      // ISSUE: explicit non-virtual call
      if (((Expression) expression).Type == typeof (bool) && resultOperators != null && __nonvirtual (resultOperators.Count) == 1 && resultOperators[0] is ContainsResultOperator containsResultOperator)
      {
        ((ExpressionVisitor) this).Visit(fromExpression);
        SQLExpression currExpr = this._currExpr;
        ((ExpressionVisitor) this).Visit(containsResultOperator.Item);
        this._currExpr = this._currExpr.In(currExpr);
        this._currProj = (ProjectionItem) null;
        return (Expression) expression;
      }
      SQLinqBqlCommandInfo linqBqlCommandInfo = new SQLinqBqlCommandInfo();
      linqBqlCommandInfo.Pars = this._info.Pars;
      linqBqlCommandInfo.Arguments = this._info.Arguments;
      linqBqlCommandInfo.Parameters = this._info.Parameters;
      linqBqlCommandInfo.ParentInfo = this._info;
      SQLinqBqlCommandInfo innerInfo = linqBqlCommandInfo;
      Query subQueryVisitor = this.CreateSubQueryVisitor(expression, innerInfo);
      this._currExpr = (SQLExpression) new SubQuery(subQueryVisitor);
      List<SQLExpression> selection = subQueryVisitor.GetSelection();
      if (selection != null && selection.Count == 1)
        this._currProj = (ProjectionItem) new ProjectionConst(((Expression) expression).Type);
      return (Expression) expression;
    }

    private protected virtual Query CreateSubQueryVisitor(
      SubQueryExpression expression,
      SQLinqBqlCommandInfo innerInfo)
    {
      return SQLinqExecutor.SQLinqQueryModelVisitor.GenerateSQLQuery(expression.QueryModel, innerInfo, SQLinqExecutor.GetQueryable(expression.QueryModel)?.Graph ?? this._graph, new bool?(), this._tableAliases);
    }

    protected virtual Expression VisitExtension(Expression expression)
    {
      throw new PXNotSupportedException("Extension");
    }

    protected virtual Expression VisitBlock(BlockExpression expression)
    {
      throw new PXNotSupportedException("Block");
    }

    protected virtual Expression VisitDefault(DefaultExpression expression)
    {
      throw new PXNotSupportedException("Default");
    }

    protected virtual ElementInit VisitElementInit(ElementInit elementInit)
    {
      throw new PXNotSupportedException("ElementInit");
    }

    protected virtual Expression VisitIndex(IndexExpression expression)
    {
      throw new PXNotSupportedException("Index");
    }

    protected virtual Expression VisitLambda<T>(Expression<T> expression)
    {
      throw new PXNotSupportedException("Lambda<T>");
    }

    protected virtual Expression VisitInvocation(InvocationExpression expression)
    {
      throw new PXNotSupportedException("Invocation");
    }

    protected virtual Expression VisitTypeBinary(TypeBinaryExpression expression)
    {
      if (!typeof (IBqlTable).IsAssignableFrom(expression.Expression.Type))
        throw new PXNotSupportedException("TypeBinary");
      this._currExpr = SQLExpression.IsTrue(expression.TypeOperand.IsAssignableFrom(expression.Expression.Type));
      this._currProj = (ProjectionItem) new ProjectionConst(expression.Type);
      return (Expression) expression;
    }

    protected virtual Expression VisitParameter(ParameterExpression expression)
    {
      throw new PXNotSupportedException("Parameter");
    }

    protected virtual Expression VisitRuntimeVariables(RuntimeVariablesExpression expression)
    {
      throw new PXNotSupportedException("RuntimeVariables");
    }

    protected virtual TResult VisitUnhandledItem<TItem, TResult>(
      TItem unhandledItem,
      string visitMethod,
      System.Func<TItem, TResult> baseBehavior)
      where TItem : TResult
    {
      PXTrace.WithSourceLocation(nameof (VisitUnhandledItem), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinq.cs", 2405).Verbose<string, string>("Remotion Visit {NodeType} Method: {MethodName}", "UnhandledItem", visitMethod);
      throw new PXNotSupportedException("New properties are not supported yet. ");
    }

    protected virtual Expression VisitConstant(ConstantExpression expression)
    {
      PXTrace.WithSourceLocation(nameof (VisitConstant), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinq.cs", 2411).Verbose<string, object>("Remotion Visit {NodeType} {Value}", "Constant", expression.Value);
      this._currExpr = !this._isBoolContext || !(expression.Value is bool flag) ? (SQLExpression) new SQLConst(expression.Value) : SQLExpression.IsTrue(flag);
      this._currProj = (ProjectionItem) new ProjectionConst(expression.Type);
      return (Expression) expression;
    }

    protected virtual Expression VisitUnary(UnaryExpression expression)
    {
      PXTrace.WithSourceLocation(nameof (VisitUnary), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinq.cs", 2426).Verbose<string, ExpressionType>("Remotion Visit {NodeType} {Type}", "Unary", expression.NodeType);
      ((ExpressionVisitor) this).Visit(expression.Operand);
      switch (expression.NodeType)
      {
        case ExpressionType.Convert:
        case ExpressionType.TypeAs:
          if (expression.Type.IsPrimitive || expression.Type.IsValueType || expression.Type == typeof (string))
          {
            System.Type type1 = Nullable.GetUnderlyingType(expression.Type);
            if ((object) type1 == null)
              type1 = expression.Type;
            System.Type type2 = Nullable.GetUnderlyingType(expression.Operand.Type);
            if ((object) type2 == null)
              type2 = expression.Operand.Type;
            if (!(type1 == type2))
            {
              if (!expression.Type.IsPrimitive)
              {
                if (!((IEnumerable<System.Type>) new System.Type[3]
                {
                  typeof (string),
                  typeof (System.DateTime),
                  typeof (Decimal)
                }).Contains<System.Type>(expression.Type))
                  goto label_12;
              }
              this._currExpr = (SQLExpression) new SQLConvert(expression.Type, this._currExpr);
            }
label_12:
            this._currProj = (ProjectionItem) new ProjectionConst(expression.Type);
            break;
          }
          string alias = !((expression.Operand is QuerySourceReferenceExpression operand ? operand.ReferencedQuerySource : (IQuerySource) null) is MainFromClause referencedQuerySource) || !SQLinqExecutor.IsGroupingType(((FromClauseBase) referencedQuerySource).FromExpression.Type) || !(((FromClauseBase) referencedQuerySource).FromExpression is QuerySourceReferenceExpression fromExpression) ? (operand != null ? this.GetTableAlias(operand.ReferencedQuerySource) : this._currAliases?[expression.Type].alias ?? this._currTable?.Alias) : this.GetTableAlias((IQuerySource) SQLinqExecutor.GetQueryModelWithGrouping(fromExpression).MainFromClause);
          if (this._currDAC == (System.Type) null || !typeof (IBqlTable).IsAssignableFrom(this._currDAC))
          {
            this._currDAC = expression.Type;
            this._currTable = new SimpleTable(expression.Type, alias);
          }
          this._currProj = !typeof (PXResult).IsAssignableFrom(expression.Type) ? (ProjectionItem) new ProjectionReference(expression.Type) : (ProjectionItem) new ProjectionPXResult(expression.Type.GenericTypeArguments);
          break;
        case ExpressionType.Negate:
          this._currExpr = SQLExpression.Negate(this._currExpr);
          break;
        case ExpressionType.Not:
          this._currExpr = !(expression.Operand.Type != typeof (bool)) ? SQLExpression.Not(this._currExpr) : this._currExpr?.BitNot();
          break;
        default:
          throw new PXNotSupportedException($"Unknown unary operation: {expression.NodeType}");
      }
      return (Expression) expression;
    }

    protected virtual Expression VisitBinary(BinaryExpression expression)
    {
      PXTrace.WithSourceLocation(nameof (VisitBinary), "C:\\build\\code_repo\\NetTools\\PX.Data\\SQLTree\\SQLinq.cs", 2487).Verbose<string>("Remotion Visit {NodeType}", "Binary");
      this._currExpr = (SQLExpression) null;
      bool isBool = expression.NodeType == ExpressionType.AndAlso || expression.NodeType == ExpressionType.OrElse;
      if ((typeof (PXResult).IsAssignableFrom(expression.Left.Type) || typeof (IBqlTable).IsAssignableFrom(expression.Left.Type)) && expression.Right is ConstantExpression right && right.Value == null && (expression.NodeType == ExpressionType.Equal || expression.NodeType == ExpressionType.NotEqual))
      {
        if (this._isBoolContext)
        {
          this._currExpr = SQLExpression.IsTrue(expression.NodeType == ExpressionType.NotEqual);
          return (Expression) expression;
        }
        ((ExpressionVisitor) this).Visit(expression.Left);
        if (this._currExpr is SubQuery currExpr)
        {
          this._currExpr = expression.NodeType == ExpressionType.NotEqual ? currExpr.Exists() : currExpr.NotExists();
          return (Expression) expression;
        }
      }
      this.Visit(expression.Left, isBool);
      SQLExpression sqlExpression1 = this._currExpr;
      if (sqlExpression1 != null && sqlExpression1.Oper() == SQLExpression.Operation.SEQ || this._currProj is ProjectionDependsOnFields)
        throw new PXNotSupportedException("Unknown binary operation argument. ");
      this._currExpr = (SQLExpression) null;
      this.Visit(expression.Right, isBool);
      SQLExpression sqlExpression2 = this._currExpr;
      if (this._currProj is ProjectionDependsOnFields)
        throw new PXNotSupportedException("Unknown binary operation argument. ");
      SQLConst sqlConst = sqlExpression2 as SQLConst;
      if (isBool && sqlConst?.GetValue() is bool)
        sqlExpression2 = SQLExpression.IsTrue((bool) sqlConst.GetValue());
      if (sqlExpression1 == null)
        sqlExpression1 = SQLExpression.None();
      switch (expression.NodeType)
      {
        case ExpressionType.Add:
          this._currExpr = !(expression.Type == typeof (string)) ? sqlExpression1 + sqlExpression2 : sqlExpression1.Concat(sqlExpression2);
          break;
        case ExpressionType.And:
          this._currExpr = sqlExpression1.BitAnd(sqlExpression2);
          break;
        case ExpressionType.AndAlso:
          this._currExpr = sqlExpression1.And(sqlExpression2);
          break;
        case ExpressionType.ArrayLength:
          this._currExpr = sqlExpression1.Length();
          break;
        case ExpressionType.Coalesce:
          this._currExpr = sqlExpression1.Coalesce(sqlExpression2);
          break;
        case ExpressionType.Divide:
          this._currExpr = sqlExpression1 / sqlExpression2;
          break;
        case ExpressionType.Equal:
          this._currExpr = sqlConst == null || sqlConst.GetValue() != null ? SQLExpressionExt.EQ(sqlExpression1, sqlExpression2) : sqlExpression1.IsNull();
          break;
        case ExpressionType.GreaterThan:
          this._currExpr = sqlExpression1.GT(sqlExpression2);
          break;
        case ExpressionType.GreaterThanOrEqual:
          this._currExpr = sqlExpression1.GE(sqlExpression2);
          break;
        case ExpressionType.LessThan:
          this._currExpr = sqlExpression1.LT(sqlExpression2);
          break;
        case ExpressionType.LessThanOrEqual:
          this._currExpr = sqlExpression1.LE(sqlExpression2);
          break;
        case ExpressionType.Modulo:
          this._currExpr = sqlExpression1 % sqlExpression2;
          break;
        case ExpressionType.Multiply:
          this._currExpr = sqlExpression1 * sqlExpression2;
          break;
        case ExpressionType.NotEqual:
          this._currExpr = sqlConst == null || sqlConst.GetValue() != null ? SQLExpressionExt.NE(sqlExpression1, sqlExpression2) : sqlExpression1.IsNotNull();
          break;
        case ExpressionType.Or:
          this._currExpr = sqlExpression1.BitOr(sqlExpression2);
          break;
        case ExpressionType.OrElse:
          this._currExpr = sqlExpression1.Or(sqlExpression2);
          break;
        case ExpressionType.Subtract:
          this._currExpr = sqlExpression1 - sqlExpression2;
          break;
        default:
          throw new PXNotSupportedException("Unknown binary operation. ");
      }
      return (Expression) expression;
    }

    private IBqlParameter GetBqlParameter(System.Type type)
    {
      System.Type type1 = Nullable.GetUnderlyingType(type);
      if ((object) type1 == null)
        type1 = type;
      type = type1;
      if (type == typeof (bool))
        return (IBqlParameter) new P.AsBool();
      if (type == typeof (byte))
        return (IBqlParameter) new P.AsByte();
      if (type == typeof (short))
        return (IBqlParameter) new P.AsShort();
      if (type == typeof (int))
        return (IBqlParameter) new P.AsInt();
      if (type == typeof (long))
        return (IBqlParameter) new P.AsLong();
      if (type == typeof (float))
        return (IBqlParameter) new P.AsFloat();
      if (type == typeof (double))
        return (IBqlParameter) new P.AsDouble();
      if (type == typeof (Decimal))
        return (IBqlParameter) new P.AsDecimal();
      if (type == typeof (System.DateTime))
        return (IBqlParameter) new P.AsDateTime();
      if (type == typeof (Guid))
        return (IBqlParameter) new P.AsGuid();
      if (type == typeof (string))
        return (IBqlParameter) new P.AsString();
      throw new PXNotSupportedException();
    }

    private protected void ValidateSubQuery(System.Type expressionType)
    {
      if (!(this._currExpr is SubQuery currExpr))
        return;
      Query query = currExpr.Query();
      List<SQLExpression> selection = query.GetSelection();
      // ISSUE: explicit non-virtual call
      if ((selection != null ? (__nonvirtual (selection.Count) == 1 ? 1 : 0) : 0) != 0)
      {
        if (query.GetLimit() == 1)
          return;
        if (query.GetSelection()[0].IsAggregate())
        {
          List<SQLExpression> groupBy = query.GetGroupBy();
          // ISSUE: explicit non-virtual call
          if ((groupBy != null ? __nonvirtual (groupBy.Count) : 0) == 0)
            return;
        }
      }
      XMLPathQuery q = new XMLPathQuery(currExpr.Query())
      {
        HasXsinil = true,
        HasBinaryBase64 = true,
        HasRoot = this._info.ParentInfo == null
      };
      q.EnsureColumnAliases();
      currExpr.SetQuery((Query) q);
      this._currProj = (ProjectionItem) new ProjectionForXmlReference(query.Projection(), expressionType);
      q.SetProjection(this._currProj);
    }
  }

  internal class MemberSourceFinderExpressionVisitor : ThrowingExpressionVisitor
  {
    private Expression _memberSource;
    private string _memberName;
    private System.Type _memberType;

    public static Expression GetMemberSource(Expression expression)
    {
      SQLinqExecutor.MemberSourceFinderExpressionVisitor expressionVisitor = new SQLinqExecutor.MemberSourceFinderExpressionVisitor();
      ((ExpressionVisitor) expressionVisitor).Visit(expression);
      return expressionVisitor._memberSource;
    }

    protected virtual Expression VisitMember(MemberExpression expression)
    {
      if (typeof (IBqlTable).IsAssignableFrom(expression.Type))
      {
        this._memberType = expression.Type;
        this._memberName = expression.Member.Name;
        this._memberSource = (Expression) expression;
        ((ExpressionVisitor) this).Visit(expression.Expression);
      }
      else if (expression.Expression is MemberExpression expression1)
      {
        if (expression1.Member.Name == "Key" && SQLinqExecutor.IsGroupingType(expression1.Member.DeclaringType))
        {
          GroupResultOperator groupResultOperator = SQLinqExecutor.GetQueryModelWithGrouping(expression1).ResultOperators.OfType<GroupResultOperator>().First<GroupResultOperator>();
          this._memberType = expression.Type;
          this._memberName = expression.Member.Name;
          ((ExpressionVisitor) this).Visit(groupResultOperator.KeySelector);
        }
        else
          ((ExpressionVisitor) this).Visit(expression.Expression);
      }
      else
        this._memberSource = expression.Expression;
      return (Expression) expression;
    }

    protected virtual Expression VisitQuerySourceReference(QuerySourceReferenceExpression expression)
    {
      if (expression.ReferencedQuerySource is MainFromClause referencedQuerySource)
      {
        if (((FromClauseBase) referencedQuerySource).FromExpression is ConstantExpression)
        {
          if (this._memberSource == null || typeof (PXResult).IsAssignableFrom(((Expression) expression).Type))
            this._memberSource = (Expression) expression;
        }
        else
          ((ExpressionVisitor) this).Visit(((FromClauseBase) referencedQuerySource).FromExpression);
      }
      else
        base.VisitQuerySourceReference(expression);
      return (Expression) expression;
    }

    protected virtual Expression VisitSubQuery(SubQueryExpression expression)
    {
      ((ExpressionVisitor) this).Visit(expression.QueryModel.SelectClause.Selector);
      return (Expression) expression;
    }

    protected virtual Expression VisitNew(NewExpression expression)
    {
      for (int index = 0; index < expression.Members.Count; ++index)
      {
        if (expression.Members[index].Name == this._memberName && expression.Arguments[index].Type == this._memberType)
        {
          this._memberSource = expression.Arguments[index];
          return (Expression) expression;
        }
      }
      return base.VisitNew(expression);
    }

    protected virtual TResult VisitUnhandledItem<TItem, TResult>(
      TItem unhandledItem,
      string visitMethod,
      System.Func<TItem, TResult> baseBehavior)
      where TItem : TResult
    {
      if (!(unhandledItem is Expression expression))
        return base.VisitUnhandledItem<TItem, TResult>(unhandledItem, visitMethod, baseBehavior);
      this._memberSource = expression;
      return (TResult) unhandledItem;
    }

    protected virtual Exception CreateUnhandledItemException<T>(T unhandledItem, string visitMethod)
    {
      return (Exception) new PXNotSupportedException();
    }
  }
}
