// Decompiled with JetBrains decompiler
// Type: PX.Data.Select3`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Selects data records from a single table with ordering.
/// </summary>
/// <typeparam name="Table">The DAC that represents the table from which
/// data records are retrieved.</typeparam>
/// <typeparam name="OrderBy">The ORDER BY clause.</typeparam>
public sealed class Select3<Table, OrderBy> : SelectBase<Table, BqlNone, BqlNone, BqlNone, OrderBy>
  where Table : IBqlTable
  where OrderBy : IBqlOrderBy, new()
{
  /// <exclude />
  public override BqlCommand WhereNew<newWhere>()
  {
    return (BqlCommand) new Select<Table, newWhere, OrderBy>();
  }

  /// <exclude />
  public override BqlCommand WhereNew(System.Type newWhere)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select<,,>).MakeGenericType(typeof (Table), newWhere, typeof (OrderBy)));
  }

  /// <exclude />
  public override BqlCommand WhereAnd<where>() => (BqlCommand) new Select<Table, where, OrderBy>();

  /// <exclude />
  public override BqlCommand WhereAnd(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select<,,>).MakeGenericType(typeof (Table), where, typeof (OrderBy)));
  }

  /// <exclude />
  public override BqlCommand WhereOr<where>() => (BqlCommand) new Select<Table, where, OrderBy>();

  /// <exclude />
  public override BqlCommand WhereOr(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select<,,>).MakeGenericType(typeof (Table), where, typeof (OrderBy)));
  }

  /// <exclude />
  public override BqlCommand WhereNot() => (BqlCommand) new Select3<Table, OrderBy>();

  /// <exclude />
  public override BqlCommand OrderByNew<newOrderBy>()
  {
    return (BqlCommand) new Select3<Table, newOrderBy>();
  }

  /// <exclude />
  public override BqlCommand OrderByNew(System.Type newOrderBy)
  {
    if (newOrderBy == (System.Type) null)
      return (BqlCommand) new Select<Table>();
    return (BqlCommand) Activator.CreateInstance(typeof (Select3<,>).MakeGenericType(typeof (Table), newOrderBy));
  }

  /// <exclude />
  public override BqlCommand AddNewJoin(System.Type newJoin)
  {
    if (newJoin == (System.Type) null || typeof (BqlNone) == newJoin)
      return (BqlCommand) new Select3<Table, OrderBy>();
    return (BqlCommand) Activator.CreateInstance(typeof (Select3<,,>).MakeGenericType(typeof (Table), newJoin, typeof (OrderBy)));
  }

  /// <exclude />
  public override BqlCommand AggregateNew<newAggregate>()
  {
    return (BqlCommand) new Select6<Table, newAggregate, OrderBy>();
  }

  /// <exclude />
  public override BqlCommand AggregateNew(System.Type newAggregate)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select6<,,>).MakeGenericType(typeof (Table), newAggregate, typeof (OrderBy)));
  }
}
