// Decompiled with JetBrains decompiler
// Type: PX.Data.SelectBase`5
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.PushNotifications;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class SelectBase<Table, Join, Where, Aggregate, OrderBy> : 
  BqlCommand,
  IBqlSelect<Table>,
  IBqlSelect,
  IHasBqlWhere
  where Table : IBqlTable
  where Join : IBqlJoin, new()
  where Where : IBqlWhere, new()
  where Aggregate : IBqlAggregate, new()
  where OrderBy : IBqlOrderBy, new()
{
  private Join _join;
  private Where _where;
  private OrderBy _orderby;
  private Aggregate _aggregate;

  protected IBqlJoin ensureJoin()
  {
    if ((object) this._join != null)
      return (IBqlJoin) this._join;
    return !(typeof (Join) == typeof (BqlNone)) ? (IBqlJoin) (this._join = new Join()) : (IBqlJoin) null;
  }

  protected IBqlWhere ensureWhere()
  {
    if ((object) this._where != null)
      return (IBqlWhere) this._where;
    return !(typeof (Where) == typeof (BqlNone)) ? (IBqlWhere) (this._where = new Where()) : (IBqlWhere) null;
  }

  protected IBqlOrderBy ensureOrderBy()
  {
    if ((object) this._orderby != null)
      return (IBqlOrderBy) this._orderby;
    return !(typeof (OrderBy) == typeof (BqlNone)) ? (IBqlOrderBy) (this._orderby = new OrderBy()) : (IBqlOrderBy) null;
  }

  protected IBqlAggregate ensureAggregate()
  {
    if ((object) this._aggregate != null)
      return (IBqlAggregate) this._aggregate;
    return !(typeof (Aggregate) == typeof (BqlNone)) ? (IBqlAggregate) (this._aggregate = new Aggregate()) : (IBqlAggregate) null;
  }

  public override void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (this.ensureAggregate() != null && !(cache.Interceptor is PXUIEmulatorAttribute))
      return;
    if (this.ensureJoin() != null)
    {
      this._join.Verify(cache, item, pars, ref result, ref value);
      bool? nullable = result;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        return;
    }
    if (this.ensureWhere() == null)
      return;
    this._where.Verify(cache, item, pars, ref result, ref value);
  }

  protected virtual void BuildQueryFrom(
    PXGraph graph,
    Query query,
    System.Type mainTableType,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (graph == null)
      return;
    query.From(TableChangingScope.GetSQLTable((Func<PX.Data.SQLTree.Table>) (() =>
    {
      System.Type table = mainTableType;
      PXGraph graph1 = graph;
      int num = selection != null ? (selection.FromProjection ? 1 : 0) : 0;
      BqlCommandInfo info1 = new BqlCommandInfo(false);
      info1.Parameters = info.Parameters;
      Query mainQuery = query;
      return BqlCommand.GetSQLTable(table, graph1, num != 0, info1, mainQuery);
    }), mainTableType.Name).As(mainTableType.Name));
  }

  public override Query GetQueryInternal(
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag1 = true;
    Query query = this.CreateQuery(graph);
    bool flag2 = this.ensureAggregate() != null;
    IBqlFunction[] aggregates = flag2 ? this._aggregate.GetAggregates() : (IBqlFunction[]) null;
    System.Type type = typeof (Table);
    if (graph != null)
    {
      if (flag2)
      {
        BqlCommand.AppendAggregatedFields(query, type, graph, selection, aggregates);
        BqlCommand.AppendAggregatedCalculated(query, graph, selection, aggregates, new BqlCommandInfo(false)
        {
          Parameters = info.Parameters,
          Tables = new List<System.Type>((IEnumerable<System.Type>) new System.Type[1]
          {
            type
          })
        });
      }
      else
        BqlCommand.AppendFields(query, type, graph, selection);
    }
    this.BuildQueryFrom(graph, query, type, info, selection);
    info.Tables?.Add(type);
    foreach (IBqlJoin enumerateJoin in BqlCommand.enumerateJoins(this.ensureJoin()))
      enumerateJoin.AppendQuery(query, graph, info, selection, aggregates);
    this.ensureWhere();
    if ((object) this._where != null)
    {
      SQLExpression exp = (SQLExpression) null;
      flag1 &= this._where.AppendExpression(ref exp, graph, info, selection);
      if (info.BuildExpression)
        query.Where(exp);
    }
    SQLExpression exp1 = (SQLExpression) null;
    if ((object) this._aggregate != null)
    {
      flag1 &= this._aggregate.AppendExpression(ref exp1, graph, info, selection);
      if (info.BuildExpression)
      {
        List<SQLExpression> list = new List<SQLExpression>();
        SQLExpression field = BqlCommand.AppendExpressionList(ref list, exp1);
        query.GroupBy(list);
        if (field != null)
        {
          if (selection.UseColumnAliases)
            field.SetAlias("Count");
          query.Field(field);
        }
      }
      IBqlHaving having = this._aggregate.Having;
      if (having != null)
      {
        SQLExpression exp2 = (SQLExpression) null;
        flag1 &= having.AppendExpression(ref exp2, graph, info, selection);
        if (info.BuildExpression)
          query.Having(exp2);
      }
    }
    this.ensureOrderBy();
    if ((object) this._orderby != null)
      this._orderby.AppendQuery(query, graph, info, selection);
    if (!flag1)
      query.NotOK();
    return query;
  }

  public override System.Type GetFirstTable() => typeof (Table);

  public IBqlWhere GetWhere()
  {
    this.ensureWhere();
    return (IBqlWhere) this._where;
  }

  public IBqlOrderBy GetOrderBy() => this.ensureOrderBy();

  public abstract BqlCommand AddNewJoin(System.Type newJoin);

  protected static System.Type CreateNewJoinType(System.Type newJoin)
  {
    List<System.Type> list = ((IEnumerable<System.Type>) BqlCommand.Decompose(typeof (Join))).ToList<System.Type>();
    var data = list.Select((type, i) => new
    {
      type = type,
      i = i
    }).Last(c => typeof (IBqlJoin).IsAssignableFrom(c.type));
    if (data.type == typeof (BqlNone))
      list[0] = newJoin;
    else if (data.type == typeof (CrossJoin<>))
    {
      list[data.i] = typeof (CrossJoin<,>);
      list.Add(newJoin);
    }
    else if (data.type == typeof (FullJoin<,>))
    {
      list[data.i] = typeof (FullJoin<,,>);
      list.Add(newJoin);
    }
    else if (data.type == typeof (FullJoin<,>))
    {
      list[data.i] = typeof (FullJoin<,,>);
      list.Add(newJoin);
    }
    else if (data.type == typeof (InnerJoin<,>))
    {
      list[data.i] = typeof (InnerJoin<,,>);
      list.Add(newJoin);
    }
    else if (data.type == typeof (LeftJoin<,>))
    {
      list[data.i] = typeof (LeftJoin<,,>);
      list.Add(newJoin);
    }
    else if (data.type == typeof (RightJoin<,>))
    {
      list[data.i] = typeof (RightJoin<,,>);
      list.Add(newJoin);
    }
    else if (data.type == typeof (LeftJoinSingleTable<,>))
    {
      list[data.i] = typeof (LeftJoinSingleTable<,,>);
      list.Add(newJoin);
    }
    else if (data.type == typeof (InnerJoinSingleTable<,>))
    {
      list[data.i] = typeof (InnerJoinSingleTable<,,>);
      list.Add(newJoin);
    }
    else if (data.type == typeof (RightJoinSingleTable<,>))
    {
      list[data.i] = typeof (RightJoinSingleTable<,,>);
      list.Add(newJoin);
    }
    else if (data.type == typeof (CrossJoinSingleTable<>))
    {
      list[data.i] = typeof (CrossJoinSingleTable<,>);
      list.Add(newJoin);
    }
    else if (data.type == typeof (FullJoinSingleTable<,>))
    {
      list[data.i] = typeof (FullJoinSingleTable<,,>);
      list.Add(newJoin);
    }
    return BqlCommand.Compose(list.ToArray());
  }
}
