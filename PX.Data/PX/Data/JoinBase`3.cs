// Decompiled with JetBrains decompiler
// Type: PX.Data.JoinBase`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.PushNotifications;
using PX.Data.SQLTree;
using PX.DbServices.QueryObjectModel;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>The base class for JOIN clauses.</summary>
/// <typeparam name="Table">A DAC class that represents a database table.</typeparam>
/// <typeparam name="On">The joining condition.</typeparam>
/// <typeparam name="NextJoin">The next JOIN clause.</typeparam>
public abstract class JoinBase<Table, On, NextJoin> : IBqlJoin, IBqlVerifier
  where Table : IBqlTable
  where On : class, IBqlOn, new()
  where NextJoin : class, IBqlJoin, new()
{
  protected On _on;
  protected NextJoin _next;

  protected IBqlOn ensureOn()
  {
    return (IBqlOn) (this._on ?? (typeof (On) == typeof (BqlNone) ? default (On) : (this._on = new On())));
  }

  /// <exclude />
  public IBqlJoin getNextJoin()
  {
    return (IBqlJoin) (this._next ?? (typeof (NextJoin) == typeof (BqlNone) ? default (NextJoin) : (this._next = new NextJoin())));
  }

  public System.Type getJoinedTable() => typeof (Table);

  public IBqlUnary getJoinCondition() => this.ensureOn()?.GetMatchingWhere();

  public abstract YaqlJoinType getJoinType();

  protected virtual bool isLeftJoin() => this.getJoinType() == 1;

  protected virtual bool PreventRowSharing { get; }

  public virtual void AppendQuery(
    Query query,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection,
    IBqlFunction[] aggregates,
    System.Type outerTable = null)
  {
    this.AppendQuery(query, graph, info, selection, aggregates, outerTable, this is IBqlJoinHints bqlJoinHints && bqlJoinHints.TryJoinOnlyHeaderOfProjection);
  }

  public virtual void AppendQuery(
    Query query,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection,
    IBqlFunction[] aggregates,
    System.Type outerTable = null,
    bool mainTableOnly = false)
  {
    this.ensureOn();
    System.Type typeofTable = typeof (Table);
    info.Tables?.Add(typeofTable);
    PX.Data.SQLTree.Table table = (PX.Data.SQLTree.Table) null;
    if (graph != null)
    {
      graph.Caches[typeofTable]._SingleTableSelecting = mainTableOnly;
      TableChangingScope.InsertIsNewIfNeeded(typeofTable, graph.Caches[typeofTable], selection);
      PXDatabaseRecordStatusHelper.InsertDatabaseRecordStatusIfNeeded(typeofTable, graph.Caches[typeofTable], selection);
      PXDeletedDatabaseRecordHelper.InsertDeletedDatabaseRecordIfNeeded(typeofTable, graph.Caches[typeofTable]);
      PX.Data.SQLTree.Table sqlTable;
      if (mainTableOnly)
      {
        TableChangingScope.AddUnchangedRealName(typeofTable.Name);
        sqlTable = TableChangingScope.GetSQLTable((Func<PX.Data.SQLTree.Table>) (() => (PX.Data.SQLTree.Table) new SimpleTable(typeofTable.Name, typeofTable.Name)), typeofTable.Name);
      }
      else
        sqlTable = TableChangingScope.GetSQLTable((Func<PX.Data.SQLTree.Table>) (() => BqlCommand.GetSQLTable(typeofTable, graph, (selection != null ? (selection.FromProjection ? 1 : 0) : 0) != 0, new BqlCommandInfo(false)
        {
          Parameters = info.Parameters
        })), typeofTable.Name);
      if (sqlTable is Query query1)
        query1.ShowArchivedRecords = query.ShowArchivedRecords;
      Joiner on = this.AppendJoinerToOn(query, graph, sqlTable, info, selection);
      query.AddJoin(on);
      if (aggregates != null)
        BqlCommand.AppendAggregatedFields(query, typeofTable, graph, selection, aggregates, mainTableOnly);
      else if (outerTable == (System.Type) null)
        BqlCommand.AppendFields(query, typeofTable, graph, selection, outerTable, mainTableOnly);
    }
    else if (!info.IsEmpty)
      this.AppendJoinerToOn(query, graph, table, info, selection);
    if (info.Parameters == null || !this.isLeftJoin())
      return;
    foreach (IBqlParameter parameter in info.Parameters)
      parameter.NullAllowed = true;
  }

  private Joiner AppendJoinerToOn(
    Query query,
    PXGraph graph,
    PX.Data.SQLTree.Table table,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    Joiner join = new Joiner(Joiner.JoinType.UNDEFINED, table, query);
    switch ((int) this.getJoinType())
    {
      case 0:
        join.SetJoinType(Joiner.JoinType.INNER_JOIN);
        if (this is IBqlJoinHints bqlJoinHints && bqlJoinHints.TryKeepTablesOrder)
        {
          join.SetHint(Joiner.JoinHint.STRAIGHT_FORCED);
          break;
        }
        break;
      case 1:
        join.SetJoinType(Joiner.JoinType.LEFT_JOIN);
        break;
      case 2:
        join.SetJoinType(Joiner.JoinType.RIGHT_JOIN);
        break;
      case 3:
        join.SetJoinType(Joiner.JoinType.CROSS_JOIN);
        break;
      case 4:
        join.SetJoinType(Joiner.JoinType.FULL_JOIN);
        break;
    }
    IBqlOn bqlOn = this.ensureOn();
    if ((bqlOn != null ? (!bqlOn.AppendJoiner(join, graph, info, selection) ? 1 : 0) : 0) != 0)
      query.NotOK();
    if (this.PreventRowSharing)
      join.PreventRowSharing();
    return join;
  }

  /// <exclude />
  public virtual void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if ((this.isLeftJoin() ? 1 : (this.getJoinType() != null ? 0 : (cache.Interceptor is PXUIEmulatorAttribute ? 1 : 0))) != 0)
    {
      bool? result1 = new bool?();
      object obj = (object) null;
      this.ensureOn()?.Verify(cache, item, pars, ref result1, ref obj);
      this.getNextJoin()?.Verify(cache, item, pars, ref result1, ref obj);
    }
    else
    {
      bool flag1 = false;
      if (this.ensureOn() != null)
      {
        this._on.Verify(cache, item, pars, ref result, ref value);
        bool? nullable = result;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
          flag1 = true;
      }
      if (this.getNextJoin() != null)
      {
        this._next.Verify(cache, item, pars, ref result, ref value);
        bool? nullable = result;
        bool flag3 = false;
        if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
          flag1 = true;
      }
      if (!flag1)
        return;
      result = new bool?(false);
    }
  }
}
