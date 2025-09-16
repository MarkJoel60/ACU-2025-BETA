// Decompiled with JetBrains decompiler
// Type: PX.Data.Search2`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

#nullable disable
namespace PX.Data;

/// <summary>
/// Retrieves a field from a table joined with other tables, applying filtering.
/// </summary>
/// <typeparam name="Field">The DAC field whose value is selected.</typeparam>
/// <typeparam name="Join">Joined DACs to select from.</typeparam>
/// <typeparam name="Where">The WHERE clause.</typeparam>
/// <example><para>The code below shows the definition of a DAC field. The Search2&lt;,,&gt; class is used to specify the data records that can be selected in the lookup control in the UI and the field (VendorClassID) whose value is assigned to the DfltVendorClassID field.</para>
/// <code title="Example" lang="CS">
/// [PXDBString(10, IsUnicode = true)]
/// [PXSelector(typeof(
///     Search2&lt;VendorClass.vendorClassID,
///         LeftJoin&lt;EPEmployeeClass,
///             On&lt;EPEmployeeClass.vendorClassID, Equal&lt;VendorClass.vendorClassID&gt;&gt;&gt;,
///         Where&lt;EPEmployeeClass.vendorClassID, IsNull&gt;&gt;))]
/// [PXUIField(DisplayName = "Default Vendor Class ID", Visibility=PXUIVisibility.Visible)]
/// public virtual String DfltVendorClassID { get; set; }</code>
/// </example>
public sealed class Search2<Field, Join, Where> : 
  SearchBase<Field, Join, Where, BqlNone, BqlNone>,
  IBqlJoinedSelect
  where Field : IBqlField
  where Join : IBqlJoin, new()
  where Where : IBqlWhere, new()
{
  private System.Type _SelectType;
  private static ConcurrentDictionary<System.Type, Func<BqlCommand>> dict = new ConcurrentDictionary<System.Type, Func<BqlCommand>>();
  private static readonly BqlCommand _Tail;

  public override System.Type GetSelectType()
  {
    if (this._SelectType == (System.Type) null)
    {
      if (typeof (Field).IsNested && typeof (IBqlTable).IsAssignableFrom(BqlCommand.GetItemType(typeof (Field))))
        this._SelectType = typeof (Select2<,,>).MakeGenericType(BqlCommand.GetItemType(typeof (Field)), typeof (Join), typeof (Where));
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

  public override BqlCommand WhereAnd<where>()
  {
    return (BqlCommand) new Search2<Field, Join, Where2<Where, And<where>>>();
  }

  public override BqlCommand WhereAnd(System.Type where)
  {
    if (WebConfig.EnablePageOpenOptimizations)
    {
      Func<BqlCommand> func = (Func<BqlCommand>) null;
      if (!Search2<Field, Join, Where>.dict.TryGetValue(where, out func))
      {
        func = ((Expression<Func<BqlCommand>>) (() => Expression.New(typeof (Search2<,,>).MakeGenericType(typeof (Field), typeof (Join), typeof (Where2<,>).MakeGenericType(typeof (Where), typeof (And<>).MakeGenericType(where)))))).Compile();
        Search2<Field, Join, Where>.dict.TryAdd(where, func);
      }
      return func();
    }
    return (BqlCommand) Activator.CreateInstance(typeof (Search2<,,>).MakeGenericType(typeof (Field), typeof (Join), typeof (Where2<,>).MakeGenericType(typeof (Where), typeof (And<>).MakeGenericType(where))));
  }

  public override BqlCommand WhereOr<where>()
  {
    return (BqlCommand) new Search2<Field, Join, Where2<Where, Or<where>>>();
  }

  public override BqlCommand WhereOr(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search2<,,>).MakeGenericType(typeof (Field), typeof (Join), typeof (Where2<,>).MakeGenericType(typeof (Where), typeof (Or<>).MakeGenericType(where))));
  }

  public override BqlCommand WhereNot()
  {
    return (BqlCommand) new Search2<Field, Join, PX.Data.Where<Not<Where>>>();
  }

  public override BqlCommand OrderByNew<newOrderBy>()
  {
    return (BqlCommand) new Search2<Field, Join, Where, newOrderBy>();
  }

  public override BqlCommand OrderByNew(System.Type newOrderBy)
  {
    if (newOrderBy == (System.Type) null)
      return (BqlCommand) new Search2<Field, Join, Where>();
    return (BqlCommand) Activator.CreateInstance(typeof (Search2<,,,>).MakeGenericType(typeof (Field), typeof (Join), typeof (Where), newOrderBy));
  }

  BqlCommand IBqlJoinedSelect.GetTail() => Search2<Field, Join, Where>._Tail;

  public bool IsInner => BqlCommand.IsInnerJoin<Join>();

  static Search2()
  {
    System.Type[] genericArguments = typeof (Join).GetGenericArguments();
    switch (genericArguments.Length)
    {
      case 1:
        Search2<Field, Join, Where>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeof (Where))));
        break;
      case 2:
        if (typeof (IBqlJoin).IsAssignableFrom(genericArguments[1]))
        {
          Search2<Field, Join, Where>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), genericArguments[1]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeof (Where))));
          break;
        }
        System.Type[] typeArray1 = typeof (IBqlOn).IsAssignableFrom(genericArguments[1]) ? genericArguments[1].GetGenericArguments() : throw new PXException(BqlCommand.invalid_join_criteria_detected);
        switch (typeArray1.Length)
        {
          case 1:
            Search2<Field, Join, Where>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (Where2<,>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[0]), typeof (And<>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeof (Where))))));
            return;
          case 2:
            if (typeof (IBqlBinary).IsAssignableFrom(typeArray1[1]))
            {
              Search2<Field, Join, Where>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (Where2<,>).MakeGenericType(typeof (Where2<,>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[0]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[1])), typeof (And<>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeof (Where))))));
              return;
            }
            Search2<Field, Join, Where>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (PX.Data.Where<,,>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[0]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[1]), typeof (And<>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeof (Where))))));
            return;
          case 3:
            Search2<Field, Join, Where>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select<,>).MakeGenericType(genericArguments[0], typeof (Where2<,>).MakeGenericType(typeof (PX.Data.Where<,,>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[0]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[1]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray1[2])), typeof (And<>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeof (Where))))));
            return;
          default:
            throw new PXException(BqlCommand.invalid_join_criteria_detected);
        }
      case 3:
        System.Type[] typeArray2 = typeof (IBqlOn).IsAssignableFrom(genericArguments[1]) ? genericArguments[1].GetGenericArguments() : throw new PXException(BqlCommand.invalid_join_criteria_detected);
        switch (typeArray2.Length)
        {
          case 1:
            Search2<Field, Join, Where>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), genericArguments[2]), typeof (Where2<,>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[0]), typeof (And<>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeof (Where))))));
            return;
          case 2:
            if (typeof (IBqlBinary).IsAssignableFrom(typeArray2[1]))
            {
              Search2<Field, Join, Where>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), genericArguments[2]), typeof (Where2<,>).MakeGenericType(typeof (Where2<,>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[0]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[1])), typeof (And<>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeof (Where))))));
              return;
            }
            Search2<Field, Join, Where>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), genericArguments[2]), typeof (PX.Data.Where<,,>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[0]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[1]), typeof (And<>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeof (Where))))));
            return;
          case 3:
            Search2<Field, Join, Where>._Tail = (BqlCommand) Activator.CreateInstance(typeof (Select2<,,>).MakeGenericType(genericArguments[0], BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), genericArguments[2]), typeof (Where2<,>).MakeGenericType(typeof (PX.Data.Where<,,>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[0]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[1]), BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeArray2[2])), typeof (And<>).MakeGenericType(BqlCommand.Parametrize(BqlCommand.GetItemType(typeof (Field)), typeof (Where))))));
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
    return (BqlCommand) new Search5<Field, Join, Where, newAggregate>();
  }

  public override BqlCommand AggregateNew(System.Type newAggregate)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search5<,,,>).MakeGenericType(typeof (Field), typeof (Join), typeof (Where), newAggregate));
  }
}
