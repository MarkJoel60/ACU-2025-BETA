// Decompiled with JetBrains decompiler
// Type: PX.Data.Select`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Selects data records from a single table with filtering.
/// </summary>
/// <typeparam name="Table">The DAC that represents the table from which
/// data records are retrieved.</typeparam>
/// <typeparam name="Where">The WHERE clause.</typeparam>
public sealed class Select<Table, Where> : SelectBase<Table, BqlNone, Where, BqlNone, BqlNone>
  where Table : IBqlTable
  where Where : IBqlWhere, new()
{
  /// <exclude />
  public override BqlCommand WhereNew<newWhere>() => (BqlCommand) new Select<Table, newWhere>();

  /// <exclude />
  public override BqlCommand WhereNew(System.Type newWhere)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(typeof (Table), newWhere));
  }

  /// <exclude />
  public override BqlCommand WhereAnd<where>()
  {
    return (BqlCommand) new Select<Table, Where2<Where, And<where>>>();
  }

  /// <exclude />
  public override BqlCommand WhereAnd(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(typeof (Table), typeof (Where2<,>).MakeGenericType(typeof (Where), typeof (And<>).MakeGenericType(where))));
  }

  /// <exclude />
  public override BqlCommand WhereOr<where>()
  {
    return (BqlCommand) new Select<Table, Where2<Where, Or<where>>>();
  }

  /// <exclude />
  public override BqlCommand WhereOr(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(typeof (Table), typeof (Where2<,>).MakeGenericType(typeof (Where), typeof (Or<>).MakeGenericType(where))));
  }

  /// <exclude />
  public override BqlCommand WhereNot() => (BqlCommand) new Select<Table, PX.Data.Where<Not<Where>>>();

  /// <exclude />
  public override BqlCommand OrderByNew<newOrderBy>()
  {
    return (BqlCommand) new Select<Table, Where, newOrderBy>();
  }

  /// <exclude />
  public override BqlCommand OrderByNew(System.Type newOrderBy)
  {
    if (newOrderBy == (System.Type) null)
      return (BqlCommand) new Select<Table, Where>();
    return (BqlCommand) Activator.CreateInstance(typeof (Select<,,>).MakeGenericType(typeof (Table), typeof (Where), newOrderBy));
  }

  /// <exclude />
  public override BqlCommand AddNewJoin(System.Type newJoin)
  {
    if (newJoin == (System.Type) null || typeof (BqlNone) == newJoin)
      return (BqlCommand) new Select<Table, Where>();
    return (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(typeof (Table), SelectBase<Table, BqlNone, Where, BqlNone, BqlNone>.CreateNewJoinType(newJoin), typeof (Where)));
  }

  /// <exclude />
  public override BqlCommand AggregateNew<newAggregate>()
  {
    return (BqlCommand) new Select4<Table, Where, newAggregate>();
  }

  /// <exclude />
  public override BqlCommand AggregateNew(System.Type newAggregate)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select4<,,>).MakeGenericType(typeof (Table), typeof (Where), newAggregate));
  }
}
