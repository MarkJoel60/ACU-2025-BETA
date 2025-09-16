// Decompiled with JetBrains decompiler
// Type: PX.Data.Select6`4
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Selects aggregated values from multiple tables with ordering.
/// </summary>
/// <typeparam name="Table">The DAC that represents the table from which
/// data records are retrieved.</typeparam>
/// <typeparam name="Aggregate"><tt>Aggregate</tt> clause wrapping <tt>Sum</tt>,
/// <tt>Avg</tt>, <tt>Min</tt>, <tt>Max</tt> or <tt>GroupBy</tt> functions.</typeparam>
/// <typeparam name="OrderBy">The ORDER BY clause.</typeparam>
public sealed class Select6<Table, Join, Aggregate, OrderBy> : 
  SelectBase<Table, Join, BqlNone, Aggregate, OrderBy>,
  IBqlAggregate,
  IBqlCreator,
  IBqlVerifier,
  IBqlJoinedSelect
  where Table : IBqlTable
  where Join : IBqlJoin, new()
  where Aggregate : IBqlAggregate, new()
  where OrderBy : IBqlOrderBy, new()
{
  /// <exclude />
  public IBqlFunction[] GetAggregates() => this.ensureAggregate().GetAggregates();

  /// <exclude />
  public IBqlHaving Having => this.ensureAggregate().Having;

  /// <exclude />
  public override BqlCommand WhereNew<newWhere>()
  {
    return (BqlCommand) new Select5<Table, Join, newWhere, Aggregate, OrderBy>();
  }

  /// <exclude />
  public override BqlCommand WhereNew(System.Type newWhere)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select5<,,,,>).MakeGenericType(typeof (Table), typeof (Join), newWhere, typeof (Aggregate), typeof (OrderBy)));
  }

  /// <exclude />
  public override BqlCommand WhereAnd<where>()
  {
    return (BqlCommand) new Select5<Table, Join, where, Aggregate, OrderBy>();
  }

  /// <exclude />
  public override BqlCommand WhereAnd(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select5<,,,,>).MakeGenericType(typeof (Table), typeof (Join), where, typeof (Aggregate), typeof (OrderBy)));
  }

  /// <exclude />
  public override BqlCommand WhereOr<where>()
  {
    return (BqlCommand) new Select5<Table, Join, where, Aggregate, OrderBy>();
  }

  /// <exclude />
  public override BqlCommand WhereOr(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select5<,,,,>).MakeGenericType(typeof (Table), typeof (Join), where, typeof (Aggregate), typeof (OrderBy)));
  }

  /// <exclude />
  public override BqlCommand WhereNot()
  {
    return (BqlCommand) new Select6<Table, Join, Aggregate, OrderBy>();
  }

  /// <exclude />
  public override BqlCommand OrderByNew<newOrderBy>()
  {
    return (BqlCommand) new Select6<Table, Join, Aggregate, newOrderBy>();
  }

  /// <exclude />
  public override BqlCommand OrderByNew(System.Type newOrderBy)
  {
    if (newOrderBy == (System.Type) null)
      return (BqlCommand) new Select5<Table, Join, Aggregate>();
    return (BqlCommand) Activator.CreateInstance(typeof (Select6<,,,>).MakeGenericType(typeof (Table), typeof (Join), typeof (Aggregate), newOrderBy));
  }

  /// <exclude />
  public override BqlCommand AddNewJoin(System.Type newJoin)
  {
    if (newJoin == (System.Type) null || typeof (BqlNone) == newJoin)
      return (BqlCommand) new Select6<Table, Join, Aggregate, OrderBy>();
    return (BqlCommand) Activator.CreateInstance(typeof (Select6<,,,>).MakeGenericType(typeof (Table), SelectBase<Table, Join, BqlNone, Aggregate, OrderBy>.CreateNewJoinType(newJoin), typeof (Aggregate), typeof (OrderBy)));
  }

  /// <exclude />
  public override BqlCommand AggregateNew<newAggregate>()
  {
    return (BqlCommand) new Select6<Table, Join, newAggregate, OrderBy>();
  }

  /// <exclude />
  public override BqlCommand AggregateNew(System.Type newAggregate)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select6<,,,>).MakeGenericType(typeof (Table), typeof (Join), newAggregate, typeof (OrderBy)));
  }

  /// <exclude />
  BqlCommand IBqlJoinedSelect.GetTail() => (BqlCommand) null;

  /// <exclude />
  public bool IsInner => BqlCommand.IsInnerJoin<Join>();
}
