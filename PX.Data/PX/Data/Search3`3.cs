// Decompiled with JetBrains decompiler
// Type: PX.Data.Search3`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Retrieves a field value from a table joined with other tables, applying ordering.
/// </summary>
/// <typeparam name="Field">The DAC field whose value is selected.</typeparam>
/// <typeparam name="Join">Joined DACs to select from.</typeparam>
/// <typeparam name="OrderBy">The ORDER BY clause.</typeparam>
public sealed class Search3<Field, Join, OrderBy> : 
  SearchBase<Field, Join, BqlNone, BqlNone, OrderBy>,
  IBqlJoinedSelect
  where Field : IBqlField
  where Join : IBqlJoin, new()
  where OrderBy : IBqlOrderBy, new()
{
  private System.Type _SelectType;
  private static readonly BqlCommand _Tail;

  public override System.Type GetSelectType()
  {
    if (this._SelectType == (System.Type) null)
    {
      if (typeof (Field).IsNested && typeof (IBqlTable).IsAssignableFrom(BqlCommand.GetItemType(typeof (Field))))
        this._SelectType = typeof (Select3<,,>).MakeGenericType(BqlCommand.GetItemType(typeof (Field)), typeof (Join), typeof (OrderBy));
      else
        this._SelectType = this.GetType();
    }
    return this._SelectType;
  }

  public override BqlCommand WhereNew<newWhere>()
  {
    return (BqlCommand) new Search2<Field, Join, newWhere, OrderBy>();
  }

  public override BqlCommand WhereNew(System.Type newWhere)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search2<,,,>).MakeGenericType(typeof (Field), typeof (Join), newWhere, typeof (OrderBy)));
  }

  public override BqlCommand WhereAnd<where>()
  {
    return (BqlCommand) new Search2<Field, Join, where, OrderBy>();
  }

  public override BqlCommand WhereAnd(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search2<,,,>).MakeGenericType(typeof (Field), typeof (Join), where, typeof (OrderBy)));
  }

  public override BqlCommand WhereOr<where>()
  {
    return (BqlCommand) new Search2<Field, Join, where, OrderBy>();
  }

  public override BqlCommand WhereOr(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search2<,,,>).MakeGenericType(typeof (Field), typeof (Join), where, typeof (OrderBy)));
  }

  public override BqlCommand WhereNot() => (BqlCommand) new Search3<Field, Join, OrderBy>();

  public override BqlCommand OrderByNew<newOrderBy>()
  {
    return (BqlCommand) new Search3<Field, Join, newOrderBy>();
  }

  public override BqlCommand OrderByNew(System.Type newOrderBy)
  {
    if (newOrderBy == (System.Type) null)
      return (BqlCommand) new Search2<Field, Join>();
    return (BqlCommand) Activator.CreateInstance(typeof (Search3<,,>).MakeGenericType(typeof (Field), typeof (Join), newOrderBy));
  }

  BqlCommand IBqlJoinedSelect.GetTail() => Search3<Field, Join, OrderBy>._Tail;

  public bool IsInner => BqlCommand.IsInnerJoin<Join>();

  static Search3()
  {
    System.Type[] genericArguments = typeof (Join).GetGenericArguments();
    switch (genericArguments.Length)
    {
      case 1:
        Search3<Field, Join, OrderBy>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<>).MakeGenericType(genericArguments));
        break;
      case 2:
        if (typeof (IBqlJoin).IsAssignableFrom(genericArguments[1]))
        {
          Search3<Field, Join, OrderBy>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), genericArguments[0]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), genericArguments[1])));
          break;
        }
        System.Type[] typeArray1 = typeof (IBqlOn).IsAssignableFrom(genericArguments[1]) ? genericArguments[1].GetGenericArguments() : throw new PXException(BqlCommand.invalid_join_criteria_detected);
        switch (typeArray1.Length)
        {
          case 1:
            Search3<Field, Join, OrderBy>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (Where<>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[0]))));
            return;
          case 2:
            if (typeof (IBqlBinary).IsAssignableFrom(typeArray1[1]))
            {
              Search3<Field, Join, OrderBy>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (Where2<,>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[0]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[1]))));
              return;
            }
            Search3<Field, Join, OrderBy>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (Where<,>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[0]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[1]))));
            return;
          case 3:
            Search3<Field, Join, OrderBy>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (Where<,,>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[0]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[1]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[2]))));
            return;
          default:
            throw new PXException(BqlCommand.invalid_join_criteria_detected);
        }
      case 3:
        System.Type[] typeArray2 = typeof (IBqlOn).IsAssignableFrom(genericArguments[1]) ? genericArguments[1].GetGenericArguments() : throw new PXException(BqlCommand.invalid_join_criteria_detected);
        switch (typeArray2.Length)
        {
          case 1:
            Search3<Field, Join, OrderBy>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), genericArguments[2]), typeof (Where<>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[0]))));
            return;
          case 2:
            if (typeof (IBqlBinary).IsAssignableFrom(typeArray2[1]))
            {
              Search3<Field, Join, OrderBy>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), genericArguments[2]), typeof (Where2<,>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[0]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[1]))));
              return;
            }
            Search3<Field, Join, OrderBy>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), genericArguments[2]), typeof (Where<,>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[0]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[1]))));
            return;
          case 3:
            Search3<Field, Join, OrderBy>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), genericArguments[2]), typeof (Where<,,>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[0]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[1]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[2]))));
            return;
          default:
            throw new PXException(BqlCommand.invalid_join_criteria_detected);
        }
      default:
        throw new PXException(BqlCommand.invalid_join_criteria_detected);
    }
  }

  public override BqlCommand AggregateNew<newAggregate>()
  {
    return (BqlCommand) new Search6<Field, Join, newAggregate, OrderBy>();
  }

  public override BqlCommand AggregateNew(System.Type newAggregate)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search6<,,,>).MakeGenericType(typeof (Field), typeof (Join), newAggregate, typeof (OrderBy)));
  }
}
