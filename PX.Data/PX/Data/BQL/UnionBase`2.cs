// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.UnionBase`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.PushNotifications;
using PX.Data.SQLTree;
using PX.DbServices.Model.Entities;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.BQL;

public abstract class UnionBase<TBqlTableMap, NextUnion> : IBqlUnion, IBqlCreator, IBqlVerifier
  where TBqlTableMap : IBqlTableMapper, new()
  where NextUnion : IBqlUnion, new()
{
  private NextUnion _next;
  private TBqlTableMap _tableMap;

  protected abstract Unioner.UnionType UnionType { get; }

  protected IBqlUnion ensureNext()
  {
    if ((object) this._next != null)
      return (IBqlUnion) this._next;
    return !(typeof (NextUnion) == typeof (BqlNone)) ? (IBqlUnion) (this._next = new NextUnion()) : (IBqlUnion) null;
  }

  protected TBqlTableMap ensureTableMap()
  {
    return (object) this._tableMap == null ? (this._tableMap = new TBqlTableMap()) : this._tableMap;
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    throw new NotImplementedException();
  }

  /// <summary>Appends the SQL tree expression that corresponds to the BQL command to an SQL tree query.</summary>
  /// <param name="exp">The SQL tree expression to be appended.</param>
  /// <param name="graph">A graph instance.</param>
  /// <param name="info">The information about the BQL command.</param>
  /// <param name="selection">The fragment of the BQL command that is translated to an SQL tree expression.</param>
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    throw new NotImplementedException();
  }

  public void AppendQuery(
    Query query,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    Query t = this.PrepareQuery(graph, query.ShowArchivedRecords, info, selection);
    if (graph != null)
      query.AddUnion(new Unioner(this.UnionType, (Table) t, query));
    if (this.ensureNext() == null)
      return;
    this._next.AppendQuery(query, graph, info, selection);
  }

  protected Query PrepareQuery(
    PXGraph graph,
    bool showArchivedRecords,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    Query innerQuery = new Query()
    {
      ShowArchivedRecords = showArchivedRecords
    };
    System.Type mainTableType = this.ensureTableMap().TableType;
    System.Type mappedTo = this.ensureTableMap().GetMapper().MappedTo;
    if (graph != null)
    {
      BqlCommand.Selection innerSelection = new BqlCommand.Selection();
      Dictionary<string, SQLExpression> mapping = this.ensureTableMap().GetMapper().GetMapping(graph);
      PXCache cach1 = graph.Caches[mappedTo];
      innerSelection.UseColumnAliases = true;
      PXCache cach2 = graph.Caches[mainTableType];
      TableChangingScope.InsertIsNewIfNeeded(mainTableType, cach2, innerSelection);
      PXDatabaseRecordStatusHelper.InsertDatabaseRecordStatusIfNeeded(mainTableType, cach2, innerSelection);
      PXDeletedDatabaseRecordHelper.InsertDeletedDatabaseRecordIfNeeded(mainTableType, cach2);
      System.Type bqlTable = mainTableType;
      foreach (string field in (List<string>) cach1.Fields)
      {
        string fieldTo = field;
        PXCommandPreparingEventArgs.FieldDescription description;
        cach1.RaiseCommandPreparing(fieldTo, (object) null, (object) null, PXDBOperation.Select, mappedTo, out description);
        if (description?.Expr != null && !BqlCommand.IsDbCalced(description))
        {
          SQLExpression sqlExpression;
          if (mapping.TryGetValue(fieldTo, out sqlExpression) && sqlExpression != null)
          {
            if (sqlExpression is Column column)
              BqlCommand.AppendField(innerQuery, column.Name, cach2, mainTableType, bqlTable, (TableHeader) null, innerSelection, false, (Func<string>) (() => fieldTo));
            else
              AddExprIntoSelection(sqlExpression.As(fieldTo), fieldTo);
          }
          else
            AddExprIntoSelection(SQLExpression.Null().As(fieldTo), fieldTo);
        }
      }
      innerQuery.From(TableChangingScope.GetSQLTable((Func<Table>) (() => BqlCommand.GetSQLTable(mainTableType, graph, (selection != null ? (selection.FromProjection ? 1 : 0) : 0) != 0, new BqlCommandInfo(false)
      {
        Parameters = info.Parameters
      })), mainTableType.Name).As(mainTableType.Name));

      void AddExprIntoSelection(SQLExpression fieldExpr, string fieldTo)
      {
        innerQuery.GetSelection().Add(fieldExpr);
        innerSelection.AddExpr(fieldTo, fieldExpr);
        ++innerSelection._PositionInQuery;
        ++innerSelection._PositionInResult;
      }
    }
    return innerQuery;
  }
}
