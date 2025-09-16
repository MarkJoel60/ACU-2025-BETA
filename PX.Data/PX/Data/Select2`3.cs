// Decompiled with JetBrains decompiler
// Type: PX.Data.Select2`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Selects data records from multiple tables with filtering.
/// </summary>
/// <typeparam name="Table">The DAC that represents the table from which
/// data records are retrieved.</typeparam>
/// <typeparam name="Join">Joined DACs to select from.</typeparam>
/// <typeparam name="Where">The WHERE clause.</typeparam>
public sealed class Select2<Table, Join, Where> : 
  SelectBase<Table, Join, Where, BqlNone, BqlNone>,
  IBqlJoinedSelect
  where Table : IBqlTable
  where Join : IBqlJoin, new()
  where Where : IBqlWhere, new()
{
  private static readonly BqlCommand _Tail;

  /// <exclude />
  public override BqlCommand WhereNew<newWhere>()
  {
    return (BqlCommand) new Select2<Table, Join, newWhere>();
  }

  /// <exclude />
  public override BqlCommand WhereNew(System.Type newWhere)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(typeof (Table), typeof (Join), newWhere));
  }

  /// <exclude />
  public override BqlCommand WhereAnd<where>()
  {
    return (BqlCommand) new Select2<Table, Join, Where2<Where, And<where>>>();
  }

  /// <exclude />
  public override BqlCommand WhereAnd(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(typeof (Table), typeof (Join), typeof (Where2<,>).MakeGenericType(typeof (Where), typeof (And<>).MakeGenericType(where))));
  }

  /// <exclude />
  public override BqlCommand WhereOr<where>()
  {
    return (BqlCommand) new Select2<Table, Join, Where2<Where, Or<where>>>();
  }

  /// <exclude />
  public override BqlCommand WhereOr(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(typeof (Table), typeof (Join), typeof (Where2<,>).MakeGenericType(typeof (Where), typeof (Or<>).MakeGenericType(where))));
  }

  /// <exclude />
  public override BqlCommand WhereNot()
  {
    return (BqlCommand) new Select2<Table, Join, PX.Data.Where<Not<Where>>>();
  }

  /// <exclude />
  public override BqlCommand OrderByNew<newOrderBy>()
  {
    return (BqlCommand) new Select2<Table, Join, Where, newOrderBy>();
  }

  /// <exclude />
  public override BqlCommand OrderByNew(System.Type newOrderBy)
  {
    if (newOrderBy == (System.Type) null)
      return (BqlCommand) new Select2<Table, Join, Where>();
    return (BqlCommand) Activator.CreateInstance(typeof (Select2<,,,>).MakeGenericType(typeof (Table), typeof (Join), typeof (Where), newOrderBy));
  }

  /// <exclude />
  public override BqlCommand AddNewJoin(System.Type newJoin)
  {
    if (newJoin == (System.Type) null || typeof (BqlNone) == newJoin)
      return (BqlCommand) new Select2<Table, Join, Where>();
    return (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(typeof (Table), SelectBase<Table, Join, Where, BqlNone, BqlNone>.CreateNewJoinType(newJoin), typeof (Where)));
  }

  /// <exclude />
  BqlCommand IBqlJoinedSelect.GetTail() => Select2<Table, Join, Where>._Tail;

  /// <exclude />
  public bool IsInner => BqlCommand.IsInnerJoin<Join>();

  /// <exclude />
  static Select2()
  {
    System.Type[] genericArguments = typeof (Join).GetGenericArguments();
    switch (genericArguments.Length)
    {
      case 1:
        Select2<Table, Join, Where>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(typeof (Table), typeof (Where))));
        break;
      case 2:
        if (typeof (IBqlJoin).IsAssignableFrom(genericArguments[1]))
        {
          Select2<Table, Join, Where>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(typeof (Table), genericArguments[1]), BqlCommand.Parametrize(typeof (Table), typeof (Where))));
          break;
        }
        System.Type[] typeArray1 = typeof (IBqlOn).IsAssignableFrom(genericArguments[1]) ? genericArguments[1].GetGenericArguments() : throw new PXException(BqlCommand.invalid_join_criteria_detected);
        switch (typeArray1.Length)
        {
          case 1:
            Select2<Table, Join, Where>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (Where2<,>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray1[0]), typeof (And<>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeof (Where))))));
            return;
          case 2:
            if (typeof (IBqlBinary).IsAssignableFrom(typeArray1[1]))
            {
              Select2<Table, Join, Where>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (Where2<,>).MakeGenericType(typeof (Where2<,>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray1[0]), BqlCommand.Parametrize(typeof (Table), typeArray1[1])), typeof (And<>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeof (Where))))));
              return;
            }
            Select2<Table, Join, Where>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (PX.Data.Where<,,>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray1[0]), BqlCommand.Parametrize(typeof (Table), typeArray1[1]), typeof (And<>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeof (Where))))));
            return;
          case 3:
            Select2<Table, Join, Where>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (Where2<,>).MakeGenericType(typeof (PX.Data.Where<,,>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray1[0]), BqlCommand.Parametrize(typeof (Table), typeArray1[1]), BqlCommand.Parametrize(typeof (Table), typeArray1[2])), typeof (And<>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeof (Where))))));
            return;
          default:
            throw new PXException(BqlCommand.invalid_join_criteria_detected);
        }
      case 3:
        System.Type[] typeArray2 = typeof (IBqlOn).IsAssignableFrom(genericArguments[1]) ? genericArguments[1].GetGenericArguments() : throw new PXException(BqlCommand.invalid_join_criteria_detected);
        switch (typeArray2.Length)
        {
          case 1:
            Select2<Table, Join, Where>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(typeof (Table), genericArguments[2]), typeof (Where2<,>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray2[0]), typeof (And<>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeof (Where))))));
            return;
          case 2:
            if (typeof (IBqlBinary).IsAssignableFrom(typeArray2[1]))
            {
              Select2<Table, Join, Where>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(typeof (Table), genericArguments[2]), typeof (Where2<,>).MakeGenericType(typeof (Where2<,>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray2[0]), BqlCommand.Parametrize(typeof (Table), typeArray2[1])), typeof (And<>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeof (Where))))));
              return;
            }
            Select2<Table, Join, Where>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(typeof (Table), genericArguments[2]), typeof (PX.Data.Where<,,>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray2[0]), BqlCommand.Parametrize(typeof (Table), typeArray2[1]), typeof (And<>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeof (Where))))));
            return;
          case 3:
            Select2<Table, Join, Where>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(typeof (Table), genericArguments[2]), typeof (Where2<,>).MakeGenericType(typeof (PX.Data.Where<,,>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray2[0]), BqlCommand.Parametrize(typeof (Table), typeArray2[1]), BqlCommand.Parametrize(typeof (Table), typeArray2[2])), typeof (And<>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeof (Where))))));
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
    return (BqlCommand) new Select5<Table, Join, Where, newAggregate>();
  }

  /// <exclude />
  public override BqlCommand AggregateNew(System.Type newAggregate)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select5<,,,>).MakeGenericType(typeof (Table), typeof (Join), typeof (Where), newAggregate));
  }
}
