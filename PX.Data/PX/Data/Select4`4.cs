// Decompiled with JetBrains decompiler
// Type: PX.Data.Select4`4
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Selects aggregated values from a single table with filtering and ordering.
/// </summary>
/// <typeparam name="Table">The DAC that represents the table from which
/// data records are retrieved.</typeparam>
/// <typeparam name="Where">The WHERE clause.</typeparam>
/// <typeparam name="Aggregate"><tt>Aggregate</tt> clause wrapping <tt>Sum</tt>,
/// <tt>Avg</tt>, <tt>Min</tt>, <tt>Max</tt> or <tt>GroupBy</tt> functions.</typeparam>
/// <typeparam name="OrderBy">The ORDER BY clause.</typeparam>
public sealed class Select4<Table, Where, Aggregate, OrderBy> : 
  SelectBase<Table, BqlNone, Where, Aggregate, OrderBy>,
  IBqlAggregate,
  IBqlCreator,
  IBqlVerifier
  where Table : IBqlTable
  where Where : IBqlWhere, new()
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
    return (BqlCommand) new Select4<Table, newWhere, Aggregate, OrderBy>();
  }

  /// <exclude />
  public override BqlCommand WhereNew(System.Type newWhere)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select4<,,,>).MakeGenericType(typeof (Table), newWhere, typeof (Aggregate), typeof (OrderBy)));
  }

  /// <exclude />
  public override BqlCommand WhereAnd<where>()
  {
    return (BqlCommand) new Select4<Table, Where2<Where, And<where>>, Aggregate, OrderBy>();
  }

  /// <exclude />
  public override BqlCommand WhereAnd(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select4<,,,>).MakeGenericType(typeof (Table), typeof (Where2<,>).MakeGenericType(typeof (Where), typeof (And<>).MakeGenericType(where)), typeof (Aggregate), typeof (OrderBy)));
  }

  /// <exclude />
  public override BqlCommand WhereOr<where>()
  {
    return (BqlCommand) new Select4<Table, Where2<Where, Or<where>>, Aggregate, OrderBy>();
  }

  /// <exclude />
  public override BqlCommand WhereOr(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select4<,,,>).MakeGenericType(typeof (Table), typeof (Where2<,>).MakeGenericType(typeof (Where), typeof (Or<>).MakeGenericType(where)), typeof (Aggregate), typeof (OrderBy)));
  }

  /// <exclude />
  public override BqlCommand WhereNot()
  {
    return (BqlCommand) new Select4<Table, PX.Data.Where<Not<Where>>, Aggregate, OrderBy>();
  }

  /// <exclude />
  public override BqlCommand OrderByNew<newOrderBy>()
  {
    return (BqlCommand) new Select4<Table, Where, Aggregate, newOrderBy>();
  }

  /// <exclude />
  public override BqlCommand OrderByNew(System.Type newOrderBy)
  {
    if (newOrderBy == (System.Type) null)
      return (BqlCommand) new Select4<Table, Where, Aggregate>();
    return (BqlCommand) Activator.CreateInstance(typeof (Select4<,,,>).MakeGenericType(typeof (Table), typeof (Where), typeof (Aggregate), newOrderBy));
  }

  /// <exclude />
  public override BqlCommand AddNewJoin(System.Type newJoin)
  {
    if (newJoin == (System.Type) null || typeof (BqlNone) == newJoin)
      return (BqlCommand) new Select4<Table, Where, Aggregate, OrderBy>();
    return (BqlCommand) Activator.CreateInstance(typeof (Select5<,,,,>).MakeGenericType(typeof (Table), SelectBase<Table, BqlNone, Where, Aggregate, OrderBy>.CreateNewJoinType(newJoin), typeof (Where), typeof (Aggregate), typeof (OrderBy)));
  }

  /// <exclude />
  public override BqlCommand AggregateNew<newAggregate>()
  {
    return (BqlCommand) new Select4<Table, Where, newAggregate, OrderBy>();
  }

  /// <exclude />
  public override BqlCommand AggregateNew(System.Type newAggregate)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select4<,,,>).MakeGenericType(typeof (Table), typeof (Where), newAggregate, typeof (OrderBy)));
  }
}
