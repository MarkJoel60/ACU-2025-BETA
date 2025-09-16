// Decompiled with JetBrains decompiler
// Type: PX.Data.Select3`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Selects data records from multiple tables with ordering.
/// </summary>
/// <typeparam name="Table">The DAC that represents the table from which
/// data records are retrieved.</typeparam>
/// <typeparam name="Join">Joined DACs to select from.</typeparam>
/// <typeparam name="OrderBy">The ORDER BY clause.</typeparam>
public sealed class Select3<Table, Join, OrderBy> : 
  SelectBase<Table, Join, BqlNone, BqlNone, OrderBy>,
  IBqlJoinedSelect
  where Table : IBqlTable
  where Join : IBqlJoin, new()
  where OrderBy : IBqlOrderBy, new()
{
  private static readonly BqlCommand _Tail;

  /// <exclude />
  public override BqlCommand WhereNew<newWhere>()
  {
    return (BqlCommand) new Select2<Table, Join, newWhere, OrderBy>();
  }

  /// <exclude />
  public override BqlCommand WhereNew(System.Type newWhere)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select2<,,,>).MakeGenericType(typeof (Table), typeof (Join), newWhere, typeof (OrderBy)));
  }

  /// <exclude />
  public override BqlCommand WhereAnd<where>()
  {
    return (BqlCommand) new Select2<Table, Join, where, OrderBy>();
  }

  /// <exclude />
  public override BqlCommand WhereAnd(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select2<,,,>).MakeGenericType(typeof (Table), typeof (Join), where, typeof (OrderBy)));
  }

  /// <exclude />
  public override BqlCommand WhereOr<where>()
  {
    return (BqlCommand) new Select2<Table, Join, where, OrderBy>();
  }

  /// <exclude />
  public override BqlCommand WhereOr(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select2<,,,>).MakeGenericType(typeof (Table), typeof (Join), where, typeof (OrderBy)));
  }

  /// <exclude />
  public override BqlCommand WhereNot() => (BqlCommand) new Select3<Table, Join, OrderBy>();

  /// <exclude />
  public override BqlCommand OrderByNew<newOrderBy>()
  {
    return (BqlCommand) new Select3<Table, Join, newOrderBy>();
  }

  /// <exclude />
  public override BqlCommand OrderByNew(System.Type newOrderBy)
  {
    if (newOrderBy == (System.Type) null)
      return (BqlCommand) new Select2<Table, Join>();
    return (BqlCommand) Activator.CreateInstance(typeof (Select3<,,>).MakeGenericType(typeof (Table), typeof (Join), newOrderBy));
  }

  /// <exclude />
  public override BqlCommand AddNewJoin(System.Type newJoin)
  {
    if (newJoin == (System.Type) null || typeof (BqlNone) == newJoin)
      return (BqlCommand) new Select3<Table, Join, OrderBy>();
    return (BqlCommand) Activator.CreateInstance(typeof (Select3<,,>).MakeGenericType(typeof (Table), SelectBase<Table, Join, BqlNone, BqlNone, OrderBy>.CreateNewJoinType(newJoin), typeof (OrderBy)));
  }

  /// <exclude />
  BqlCommand IBqlJoinedSelect.GetTail() => Select3<Table, Join, OrderBy>._Tail;

  /// <exclude />
  public bool IsInner => BqlCommand.IsInnerJoin<Join>();

  /// <exclude />
  static Select3()
  {
    System.Type[] genericArguments = typeof (Join).GetGenericArguments();
    switch (genericArguments.Length)
    {
      case 1:
        Select3<Table, Join, OrderBy>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<>).MakeGenericType(genericArguments));
        break;
      case 2:
        if (typeof (IBqlJoin).IsAssignableFrom(genericArguments[1]))
        {
          Select3<Table, Join, OrderBy>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), genericArguments[0]), BqlCommand.Parametrize(typeof (Table), genericArguments[1])));
          break;
        }
        System.Type[] typeArray1 = typeof (IBqlOn).IsAssignableFrom(genericArguments[1]) ? genericArguments[1].GetGenericArguments() : throw new PXException(BqlCommand.invalid_join_criteria_detected);
        switch (typeArray1.Length)
        {
          case 1:
            Select3<Table, Join, OrderBy>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (Where<>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray1[0]))));
            return;
          case 2:
            if (typeof (IBqlBinary).IsAssignableFrom(typeArray1[1]))
            {
              Select3<Table, Join, OrderBy>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (Where2<,>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray1[0]), BqlCommand.Parametrize(typeof (Table), typeArray1[1]))));
              return;
            }
            Select3<Table, Join, OrderBy>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (Where<,>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray1[0]), BqlCommand.Parametrize(typeof (Table), typeArray1[1]))));
            return;
          case 3:
            Select3<Table, Join, OrderBy>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (Where<,,>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray1[0]), BqlCommand.Parametrize(typeof (Table), typeArray1[1]), BqlCommand.Parametrize(typeof (Table), typeArray1[2]))));
            return;
          default:
            throw new PXException(BqlCommand.invalid_join_criteria_detected);
        }
      case 3:
        System.Type[] typeArray2 = typeof (IBqlOn).IsAssignableFrom(genericArguments[1]) ? genericArguments[1].GetGenericArguments() : throw new PXException(BqlCommand.invalid_join_criteria_detected);
        switch (typeArray2.Length)
        {
          case 1:
            Select3<Table, Join, OrderBy>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(typeof (Table), genericArguments[2]), typeof (Where<>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray2[0]))));
            return;
          case 2:
            if (typeof (IBqlBinary).IsAssignableFrom(typeArray2[1]))
            {
              Select3<Table, Join, OrderBy>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(typeof (Table), genericArguments[2]), typeof (Where2<,>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray2[0]), BqlCommand.Parametrize(typeof (Table), typeArray2[1]))));
              return;
            }
            Select3<Table, Join, OrderBy>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(typeof (Table), genericArguments[2]), typeof (Where<,>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray2[0]), BqlCommand.Parametrize(typeof (Table), typeArray2[1]))));
            return;
          case 3:
            Select3<Table, Join, OrderBy>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(typeof (Table), genericArguments[2]), typeof (Where<,,>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray2[0]), BqlCommand.Parametrize(typeof (Table), typeArray2[1]), BqlCommand.Parametrize(typeof (Table), typeArray2[2]))));
            return;
          default:
            throw new PXException(BqlCommand.invalid_join_criteria_detected);
        }
      default:
        throw new PXException(BqlCommand.invalid_join_criteria_detected);
    }
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
}
