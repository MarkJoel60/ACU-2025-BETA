// Decompiled with JetBrains decompiler
// Type: PX.Data.Select2`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Selects data records from multiple tables.</summary>
/// <typeparam name="Table">The DAC that represents the table from which
/// data records are retrieved.</typeparam>
/// <typeparam name="Join">Joined DACs to select from.</typeparam>
public sealed class Select2<Table, Join> : 
  SelectBase<Table, Join, BqlNone, BqlNone, BqlNone>,
  IBqlJoinedSelect
  where Table : IBqlTable
  where Join : IBqlJoin, new()
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
  public override BqlCommand WhereAnd<where>() => (BqlCommand) new Select2<Table, Join, where>();

  /// <exclude />
  public override BqlCommand WhereAnd(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(typeof (Table), typeof (Join), where));
  }

  /// <exclude />
  public override BqlCommand WhereOr<where>() => (BqlCommand) new Select2<Table, Join, where>();

  /// <exclude />
  public override BqlCommand WhereOr(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(typeof (Table), typeof (Join), where));
  }

  /// <exclude />
  public override BqlCommand WhereNot() => (BqlCommand) new Select2<Table, Join>();

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
      return (BqlCommand) new Select2<Table, Join>();
    return (BqlCommand) Activator.CreateInstance(typeof (Select2<,>).MakeGenericType(typeof (Table), SelectBase<Table, Join, BqlNone, BqlNone, BqlNone>.CreateNewJoinType(newJoin)));
  }

  /// <exclude />
  BqlCommand IBqlJoinedSelect.GetTail() => Select2<Table, Join>._Tail;

  /// <exclude />
  public bool IsInner => BqlCommand.IsInnerJoin<Join>();

  /// <exclude />
  static Select2()
  {
    System.Type[] genericArguments = typeof (Join).GetGenericArguments();
    switch (genericArguments.Length)
    {
      case 1:
        Select2<Table, Join>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<>).MakeGenericType(genericArguments));
        break;
      case 2:
        if (typeof (IBqlJoin).IsAssignableFrom(genericArguments[1]))
        {
          Select2<Table, Join>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), genericArguments[0]), BqlCommand.Parametrize(typeof (Table), genericArguments[1])));
          break;
        }
        System.Type[] typeArray1 = typeof (IBqlOn).IsAssignableFrom(genericArguments[1]) ? genericArguments[1].GetGenericArguments() : throw new PXException(BqlCommand.invalid_join_criteria_detected);
        switch (typeArray1.Length)
        {
          case 1:
            Select2<Table, Join>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (Where<>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray1[0]))));
            return;
          case 2:
            if (typeof (IBqlBinary).IsAssignableFrom(typeArray1[1]))
            {
              Select2<Table, Join>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (Where2<,>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray1[0]), BqlCommand.Parametrize(typeof (Table), typeArray1[1]))));
              return;
            }
            Select2<Table, Join>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (Where<,>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray1[0]), BqlCommand.Parametrize(typeof (Table), typeArray1[1]))));
            return;
          case 3:
            Select2<Table, Join>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (Where<,,>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray1[0]), BqlCommand.Parametrize(typeof (Table), typeArray1[1]), BqlCommand.Parametrize(typeof (Table), typeArray1[2]))));
            return;
          default:
            throw new PXException(BqlCommand.invalid_join_criteria_detected);
        }
      case 3:
        System.Type[] typeArray2 = typeof (IBqlOn).IsAssignableFrom(genericArguments[1]) ? genericArguments[1].GetGenericArguments() : throw new PXException(BqlCommand.invalid_join_criteria_detected);
        switch (typeArray2.Length)
        {
          case 1:
            Select2<Table, Join>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(typeof (Table), genericArguments[2]), typeof (Where<>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray2[0]))));
            return;
          case 2:
            if (typeof (IBqlBinary).IsAssignableFrom(typeArray2[1]))
            {
              Select2<Table, Join>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(typeof (Table), genericArguments[2]), typeof (Where2<,>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray2[0]), BqlCommand.Parametrize(typeof (Table), typeArray2[1]))));
              return;
            }
            Select2<Table, Join>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(typeof (Table), genericArguments[2]), typeof (Where<,>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray2[0]), BqlCommand.Parametrize(typeof (Table), typeArray2[1]))));
            return;
          case 3:
            Select2<Table, Join>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(typeof (Table), genericArguments[2]), typeof (Where<,,>).MakeGenericType(BqlCommand.Parametrize(typeof (Table), typeArray2[0]), BqlCommand.Parametrize(typeof (Table), typeArray2[1]), BqlCommand.Parametrize(typeof (Table), typeArray2[2]))));
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
    return (BqlCommand) new Select5<Table, Join, newAggregate>();
  }

  /// <exclude />
  public override BqlCommand AggregateNew(System.Type newAggregate)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Select5<,,>).MakeGenericType(typeof (Table), typeof (Join), newAggregate));
  }
}
