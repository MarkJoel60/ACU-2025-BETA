// Decompiled with JetBrains decompiler
// Type: PX.Data.Search2`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Retrieves a field from a table joined with other tables.
/// </summary>
/// <typeparam name="Field">The DAC field whose value is selected.</typeparam>
/// <typeparam name="Join">Joined Bql tables to select from</typeparam>
/// <example><para>The code below shows a part of the DAC field definition. The Search2&lt;,&gt; class is used to specify the source of the default value for the DiscountAcctID field.</para>
/// <code title="Example" lang="CS">
/// [PXDefault(
///     typeof(Search2&lt;VendorClass.discountAcctID,
///         InnerJoin&lt;APSetup, On&lt;VendorClass.vendorClassID, Equal&lt;APSetup.dfltVendorClassID&gt;&gt;&gt;&gt;),
///     PersistingCheck = PXPersistingCheck.Nothing)]
/// public virtual Int32? DiscountAcctID { get; set; }</code>
/// </example>
public sealed class Search2<Field, Join> : 
  SearchBase<Field, Join, BqlNone, BqlNone, BqlNone>,
  IBqlJoinedSelect
  where Field : IBqlField
  where Join : IBqlJoin, new()
{
  private System.Type _SelectType;
  private static readonly BqlCommand _Tail;

  public override System.Type GetSelectType()
  {
    if (this._SelectType == (System.Type) null)
    {
      if (typeof (Field).IsNested && typeof (IBqlTable).IsAssignableFrom(BqlCommand.GetItemType(typeof (Field))))
        this._SelectType = typeof (Select2<,>).MakeGenericType(BqlCommand.GetItemType(typeof (Field)), typeof (Join));
      else
        this._SelectType = this.GetType();
    }
    return this._SelectType;
  }

  public override BqlCommand WhereNew<newWhere>()
  {
    return (BqlCommand) new Search2<Field, Join, newWhere>();
  }

  public override BqlCommand WhereNew(System.Type newWhere)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search2<,,>).MakeGenericType(typeof (Field), typeof (Join), newWhere));
  }

  public override BqlCommand WhereAnd<where>() => (BqlCommand) new Search2<Field, Join, where>();

  public override BqlCommand WhereAnd(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search2<,,>).MakeGenericType(typeof (Field), typeof (Join), where));
  }

  public override BqlCommand WhereOr<where>() => (BqlCommand) new Search2<Field, Join, where>();

  public override BqlCommand WhereOr(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search2<,,>).MakeGenericType(typeof (Field), typeof (Join), where));
  }

  public override BqlCommand WhereNot() => (BqlCommand) new Search2<Field, Join>();

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

  BqlCommand IBqlJoinedSelect.GetTail() => Search2<Field, Join>._Tail;

  public bool IsInner => BqlCommand.IsInnerJoin<Join>();

  static Search2()
  {
    System.Type[] genericArguments = typeof (Join).GetGenericArguments();
    switch (genericArguments.Length)
    {
      case 1:
        Search2<Field, Join>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<>).MakeGenericType(genericArguments));
        break;
      case 2:
        if (typeof (IBqlJoin).IsAssignableFrom(genericArguments[1]))
        {
          Search2<Field, Join>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), genericArguments[0]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), genericArguments[1])));
          break;
        }
        System.Type[] typeArray1 = typeof (IBqlOn).IsAssignableFrom(genericArguments[1]) ? genericArguments[1].GetGenericArguments() : throw new PXException(BqlCommand.invalid_join_criteria_detected);
        switch (typeArray1.Length)
        {
          case 1:
            Search2<Field, Join>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (Where<>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[0]))));
            return;
          case 2:
            if (typeof (IBqlBinary).IsAssignableFrom(typeArray1[1]))
            {
              Search2<Field, Join>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (Where2<,>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[0]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[1]))));
              return;
            }
            Search2<Field, Join>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (Where<,>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[0]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[1]))));
            return;
          case 3:
            Search2<Field, Join>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (Where<,,>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[0]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[1]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[2]))));
            return;
          default:
            throw new PXException(BqlCommand.invalid_join_criteria_detected);
        }
      case 3:
        System.Type[] typeArray2 = typeof (IBqlOn).IsAssignableFrom(genericArguments[1]) ? genericArguments[1].GetGenericArguments() : throw new PXException(BqlCommand.invalid_join_criteria_detected);
        switch (typeArray2.Length)
        {
          case 1:
            Search2<Field, Join>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), genericArguments[2]), typeof (Where<>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[0]))));
            return;
          case 2:
            if (typeof (IBqlBinary).IsAssignableFrom(typeArray2[1]))
            {
              Search2<Field, Join>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), genericArguments[2]), typeof (Where2<,>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[0]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[1]))));
              return;
            }
            Search2<Field, Join>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), genericArguments[2]), typeof (Where<,>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[0]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[1]))));
            return;
          case 3:
            Search2<Field, Join>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), genericArguments[2]), typeof (Where<,,>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[0]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[1]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[2]))));
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
    return (BqlCommand) new Search5<Field, Join, newAggregate>();
  }

  public override BqlCommand AggregateNew(System.Type newAggregate)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search5<,,>).MakeGenericType(typeof (Field), typeof (Join), newAggregate));
  }
}
