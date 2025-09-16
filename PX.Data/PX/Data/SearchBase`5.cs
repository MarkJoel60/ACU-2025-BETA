// Decompiled with JetBrains decompiler
// Type: PX.Data.SearchBase`5
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class SearchBase<Field, Join, Where, Aggregate, OrderBy> : 
  BqlCommand,
  IBqlSearch,
  IHasBqlWhere
  where Field : IBqlField
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
    return !(typeof (Join) == typeof (BqlNone)) ? (IBqlJoin) ((object) this._join == null ? (this._join = new Join()) : this._join) : (IBqlJoin) null;
  }

  protected IBqlWhere ensureWhere()
  {
    return !(typeof (Where) == typeof (BqlNone)) ? (IBqlWhere) ((object) this._where == null ? (this._where = new Where()) : this._where) : (IBqlWhere) null;
  }

  protected IBqlOrderBy ensureOrderBy()
  {
    return !(typeof (OrderBy) == typeof (BqlNone)) ? (IBqlOrderBy) ((object) this._orderby == null ? (this._orderby = new OrderBy()) : this._orderby) : (IBqlOrderBy) null;
  }

  protected IBqlAggregate ensureAggregate()
  {
    return !(typeof (Aggregate) == typeof (BqlNone)) ? (IBqlAggregate) ((object) this._aggregate == null ? (this._aggregate = new Aggregate()) : this._aggregate) : (IBqlAggregate) null;
  }

  public System.Type GetField() => typeof (Field);

  public SQLExpression GetFieldExpression(PXGraph graph)
  {
    System.Type field1 = typeof (Field);
    System.Type itemType = BqlCommand.GetItemType(field1);
    SQLExpression field2 = BqlCommand.GetField(field1, graph.Caches[field1.DeclaringType], PXDBOperation.Select);
    bool flag = this.ensureAggregate() != null;
    if (!flag)
      return field2;
    IBqlFunction[] aggregates = flag ? this._aggregate.GetAggregates() : (IBqlFunction[]) null;
    string str = "MAX";
    foreach (IBqlFunction bqlFunction in aggregates)
    {
      System.Type field3 = bqlFunction.GetField();
      if (field3 != (System.Type) null && string.Equals(field1.Name, field3.Name, StringComparison.OrdinalIgnoreCase) && (BqlCommand.GetItemType(field3) == itemType || itemType.IsSubclassOf(BqlCommand.GetItemType(field3)) && !typeof (IBqlTable).IsAssignableFrom(BqlCommand.GetItemType(field3))))
        str = bqlFunction.GetFunction();
    }
    SQLExpression fieldExpression;
    switch (str)
    {
      case "MAX":
        fieldExpression = field2.Max();
        break;
      case "MIN":
        fieldExpression = field2.Min();
        break;
      case "AVG":
        fieldExpression = field2.Avg();
        break;
      case "SUM":
        fieldExpression = field2.Sum();
        break;
      default:
        fieldExpression = SQLExpression.None();
        break;
    }
    return fieldExpression;
  }

  /// <summary>Returns an SQL tree expression of the where inside the BQL search command.</summary>
  /// <param name="graph">A <see cref="T:PX.Data.PXGraph">PXGraph</see> instance.</param>
  /// <returns>An SQL tree expression.</returns>
  public SQLExpression GetWhereExpression(PXGraph graph)
  {
    this.ensureWhere();
    if ((object) this._where == null)
      return SQLExpression.Null();
    SQLExpression whereExpression = (SQLExpression) null;
    ref Where local1 = ref this._where;
    if ((object) default (Where) == null)
    {
      Where where = local1;
      local1 = ref where;
    }
    ref SQLExpression local2 = ref whereExpression;
    PXGraph graph1 = graph;
    BqlCommandInfo info = new BqlCommandInfo();
    BqlCommand.Selection selection = new BqlCommand.Selection();
    local1.AppendExpression(ref local2, graph1, info, selection);
    return whereExpression;
  }

  public string GetFieldName(PXGraph graph)
  {
    System.Type field1 = typeof (Field);
    System.Type itemType = BqlCommand.GetItemType(field1);
    string fieldName = BqlCommand.GetFieldName(field1, graph.Caches[field1.DeclaringType], PXDBOperation.Select);
    bool flag = this.ensureAggregate() != null;
    if (!flag)
      return fieldName;
    IBqlFunction[] aggregates = flag ? this._aggregate.GetAggregates() : (IBqlFunction[]) null;
    string str = "MAX";
    foreach (IBqlFunction bqlFunction in aggregates)
    {
      System.Type field2 = bqlFunction.GetField();
      if (field2 != (System.Type) null && string.Equals(field1.Name, field2.Name, StringComparison.OrdinalIgnoreCase) && (BqlCommand.GetItemType(field2) == itemType || itemType.IsSubclassOf(BqlCommand.GetItemType(field2)) && !typeof (IBqlTable).IsAssignableFrom(BqlCommand.GetItemType(field2))))
        str = bqlFunction.GetFunction();
    }
    return $"{str}({fieldName})";
  }

  public override Query GetQueryInternal(
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag1 = true;
    Query query1 = new Query();
    PXSelectOperationContext context = this.Context;
    query1.ShowArchivedRecords = (context != null ? (context.ReadArchived ? 1 : 0) : 0) != 0 && graph != null && graph.IsContractBasedAPI;
    Query query2 = query1;
    bool flag2 = this.ensureAggregate() != null;
    IBqlFunction[] aggregates = flag2 ? this._aggregate.GetAggregates() : (IBqlFunction[]) null;
    System.Type itemType = BqlCommand.GetItemType(typeof (Field));
    if (graph != null)
    {
      if (flag2)
        BqlCommand.AppendAggregatedFields(query2, itemType, graph, selection, aggregates);
      else
        BqlCommand.AppendFields(query2, itemType, graph, selection);
      Table sqlTable = BqlCommand.GetSQLTable(itemType, graph, (selection != null ? (selection.FromProjection ? 1 : 0) : 0) != 0, new BqlCommandInfo(false)
      {
        Parameters = info.Parameters
      });
      if (sqlTable is Query query3)
        query3.ShowArchivedRecords = query2.ShowArchivedRecords;
      query2.From(sqlTable);
    }
    info.Tables?.Add(itemType);
    foreach (IBqlJoin enumerateJoin in BqlCommand.enumerateJoins(this.ensureJoin()))
      enumerateJoin.AppendQuery(query2, graph, info, selection, aggregates);
    this.ensureWhere();
    if ((object) this._where != null)
    {
      SQLExpression exp = (SQLExpression) null;
      flag1 &= this._where.AppendExpression(ref exp, graph, info, selection);
      if (info.BuildExpression)
        query2.Where(exp);
    }
    SQLExpression exp1 = (SQLExpression) null;
    if ((object) this._aggregate != null)
    {
      flag1 &= this._aggregate.AppendExpression(ref exp1, graph, info, selection);
      if (info.BuildExpression)
      {
        List<SQLExpression> list = new List<SQLExpression>();
        SQLExpression field = BqlCommand.AppendExpressionList(ref list, exp1);
        query2.GroupBy(list);
        if (field != null)
          query2.Field(field);
      }
      IBqlHaving having = this._aggregate.Having;
      if (having != null)
      {
        SQLExpression exp2 = (SQLExpression) null;
        flag1 &= having.AppendExpression(ref exp2, graph, info, selection);
        if (info.BuildExpression)
          query2.Having(exp2);
      }
    }
    this.ensureOrderBy();
    if (selection == null || !selection.BqlMode.HasFlag((Enum) BqlCommand.Selection.BqlParsingMode.DiscardOrdersInPxSearch))
    {
      if ((object) this._orderby != null)
        this._orderby.AppendQuery(query2, graph, info, selection);
      else if ((object) this._aggregate == null && info.BuildExpression)
        query2.OrderAsc(BqlCommand.GetSingleExpression(typeof (Field), graph, info.Tables, selection, BqlCommand.FieldPlace.OrderBy));
    }
    if (!info.OnlyExplicitSort && info.SortColumns != null && (object) this._join == null && (object) this._orderby == null && (object) this._aggregate == null)
      info.SortColumns.Add((IBqlSortColumn) new Asc<Field>());
    if (!flag1)
      query2.NotOK();
    return query2;
  }

  public sealed override void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    result = new bool?(true);
    if (this.ensureAggregate() != null)
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

  public override System.Type GetFirstTable() => BqlCommand.GetItemType(typeof (Field));

  public IBqlWhere GetWhere()
  {
    this.ensureWhere();
    return (IBqlWhere) this._where;
  }
}
