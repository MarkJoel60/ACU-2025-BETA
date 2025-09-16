// Decompiled with JetBrains decompiler
// Type: PX.Data.BqlNone
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using PX.DbServices.QueryObjectModel;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public class BqlNone : 
  PXBqlTable,
  IBqlWhere,
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier,
  IBqlAggregate,
  IBqlJoin,
  IBqlOrderBy,
  IBqlSortColumn,
  IBqlBinary,
  IBqlFunction,
  IBqlComparison,
  IBqlOperand,
  IBqlOn,
  IBqlHaving,
  IBqlSet,
  IBqlUnion
{
  private const string NoneIsDummyObject = "BqlNone is a dummy object; this method must not be called.";

  public void AppendQuery(
    Query query,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    throw new InvalidOperationException("BqlNone is a dummy object; this method must not be called.");
  }

  public void AppendQuery(
    Query query,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection,
    IBqlFunction[] aggregates,
    System.Type outerTable = null)
  {
    throw new InvalidOperationException("BqlNone is a dummy object; this method must not be called.");
  }

  public bool AppendJoiner(
    Joiner join,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    throw new InvalidOperationException("BqlNone is a dummy object; this method must not be called.");
  }

  public virtual SQLExpression MakeExpression(
    out bool status,
    PXGraph graph,
    BqlCommand.Selection selection)
  {
    throw new InvalidOperationException("BqlNone is a dummy object; this method must not be called.");
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    throw new InvalidOperationException("BqlNone is a dummy object; this method must not be called.");
  }

  public bool AppendExpression(
    PXGraph graph,
    BqlCommandInfo info,
    List<KeyValuePair<SQLExpression, SQLExpression>> assignments)
  {
    throw new InvalidOperationException("BqlNone is a dummy object; this method must not be called.");
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    throw new InvalidOperationException("BqlNone is a dummy object; this method must not be called.");
  }

  public IBqlFunction[] GetAggregates()
  {
    throw new InvalidOperationException("BqlNone is a dummy object; this method must not be called.");
  }

  public IBqlHaving Having
  {
    get
    {
      throw new InvalidOperationException("BqlNone is a dummy object; this method must not be called.");
    }
  }

  public System.Type GetReferencedType()
  {
    throw new InvalidOperationException("BqlNone is a dummy object; this method must not be called.");
  }

  public bool IsDescending
  {
    get
    {
      throw new InvalidOperationException("BqlNone is a dummy object; this method must not be called.");
    }
  }

  public bool IsConjunction() => false;

  public IBqlJoin getNextJoin() => (IBqlJoin) null;

  public System.Type getJoinedTable() => (System.Type) null;

  public YaqlJoinType getJoinType() => (YaqlJoinType) 3;

  public IBqlUnary getJoinCondition() => (IBqlUnary) null;

  public IBqlUnary GetUnary()
  {
    throw new InvalidOperationException("BqlNone is a dummy object; this method must not be called.");
  }

  public void GetAggregates(List<IBqlFunction> fields)
  {
    throw new InvalidOperationException("BqlNone is a dummy object; this method must not be called.");
  }

  public string GetFunction()
  {
    throw new InvalidOperationException("BqlNone is a dummy object; this method must not be called.");
  }

  public System.Type GetField()
  {
    throw new InvalidOperationException("BqlNone is a dummy object; this method must not be called.");
  }

  public IBqlUnary GetMatchingWhere() => (IBqlUnary) null;

  public bool IsGroupBy() => false;

  public System.Type GetFieldType()
  {
    throw new InvalidOperationException("BqlNone is a dummy object; this method must not be called.");
  }

  public IBqlUnary Condition
  {
    get
    {
      throw new InvalidOperationException("BqlNone is a dummy object; this method must not be called.");
    }
  }
}
