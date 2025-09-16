// Decompiled with JetBrains decompiler
// Type: PX.Data.Select`1
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
public sealed class Select<Table> : SelectBase<Table, BqlNone, BqlNone, BqlNone, BqlNone> where Table : IBqlTable
{
  /// <exclude />
  public override BqlCommand WhereNew<newWhere>() => (BqlCommand) new Select<Table, newWhere>();

  /// <exclude />
  public override BqlCommand WhereNew(System.Type newWhere)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(typeof (Table), newWhere));
  }

  /// <exclude />
  public override BqlCommand WhereAnd<where>() => (BqlCommand) new Select<Table, where>();

  /// <exclude />
  public override BqlCommand WhereAnd(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(typeof (Table), where));
  }

  /// <exclude />
  public override BqlCommand WhereOr<where>() => (BqlCommand) new Select<Table, where>();

  /// <exclude />
  public override BqlCommand WhereOr(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(typeof (Table), where));
  }

  /// <exclude />
  public override BqlCommand WhereNot() => (BqlCommand) new Select<Table>();

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
      return (BqlCommand) new Select<Table>();
    return (BqlCommand) Activator.CreateInstance(typeof (Select2<,>).MakeGenericType(typeof (Table), newJoin));
  }

  /// <exclude />
  public override BqlCommand AggregateNew<newAggregate>()
  {
    return (BqlCommand) new Select4<Table, newAggregate>();
  }

  /// <exclude />
  public override BqlCommand AggregateNew(System.Type newAggregate)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select4<,>).MakeGenericType(typeof (Table), newAggregate));
  }
}
