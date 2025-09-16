// Decompiled with JetBrains decompiler
// Type: PX.Data.Search5`4
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
/// Retrieves an aggregated field value from one table joined with other
/// tables, applying filtering.
/// </summary>
/// <typeparam name="Field">The DAC field whose value is selected.</typeparam>
/// <typeparam name="Join">Joined DACs to select from.</typeparam>
/// <typeparam name="Where">The WHERE clause.</typeparam>
/// <typeparam name="Aggregate"><tt>Aggregate</tt> clause wrapping <tt>Sum</tt>,
/// <tt>Avg</tt>, <tt>Min</tt>, <tt>Max</tt> or <tt>GroupBy</tt> functions.</typeparam>
public sealed class Search5<Field, Join, Where, Aggregate> : 
  SearchBase<Field, Join, Where, Aggregate, BqlNone>,
  IBqlAggregate,
  IBqlCreator,
  IBqlVerifier
  where Field : IBqlField
  where Join : IBqlJoin, new()
  where Where : IBqlWhere, new()
  where Aggregate : IBqlAggregate, new()
{
  private System.Type _SelectType;
  private static ConcurrentDictionary<System.Type, Func<BqlCommand>> dictNew = new ConcurrentDictionary<System.Type, Func<BqlCommand>>();
  private static ConcurrentDictionary<System.Type, Func<BqlCommand>> dict = new ConcurrentDictionary<System.Type, Func<BqlCommand>>();

  public IBqlFunction[] GetAggregates() => this.ensureAggregate().GetAggregates();

  /// <exclude />
  public IBqlHaving Having => this.ensureAggregate().Having;

  public override System.Type GetSelectType()
  {
    if (this._SelectType == (System.Type) null)
    {
      if (typeof (Field).IsNested && typeof (IBqlTable).IsAssignableFrom(BqlCommand.GetItemType(typeof (Field))))
        this._SelectType = typeof (Select5<,,,>).MakeGenericType(BqlCommand.GetItemType(typeof (Field)), typeof (Join), typeof (Where), typeof (Aggregate));
      else
        this._SelectType = this.GetType();
    }
    return this._SelectType;
  }

  public override BqlCommand WhereNew<newWhere>()
  {
    return (BqlCommand) new Search5<Field, Join, newWhere, Aggregate>();
  }

  public override BqlCommand WhereNew(System.Type newWhere)
  {
    if (WebConfig.EnablePageOpenOptimizations)
    {
      Func<BqlCommand> func = (Func<BqlCommand>) null;
      if (!Search5<Field, Join, Where, Aggregate>.dictNew.TryGetValue(newWhere, out func))
      {
        func = ((Expression<Func<BqlCommand>>) (() => Expression.New(typeof (Search5<,,,>).MakeGenericType(typeof (Field), typeof (Join), newWhere, typeof (Aggregate))))).Compile();
        Search5<Field, Join, Where, Aggregate>.dictNew.TryAdd(newWhere, func);
      }
      return func();
    }
    return (BqlCommand) Activator.CreateInstance(typeof (Search5<,,,>).MakeGenericType(typeof (Field), typeof (Join), newWhere, typeof (Aggregate)));
  }

  public override BqlCommand WhereAnd<where>()
  {
    return (BqlCommand) new Search5<Field, Join, Where2<Where, And<where>>, Aggregate>();
  }

  public override BqlCommand WhereAnd(System.Type where)
  {
    if (WebConfig.EnablePageOpenOptimizations)
    {
      Func<BqlCommand> func = (Func<BqlCommand>) null;
      if (!Search5<Field, Join, Where, Aggregate>.dict.TryGetValue(where, out func))
      {
        func = ((Expression<Func<BqlCommand>>) (() => Expression.New(typeof (Search5<,,,>).MakeGenericType(typeof (Field), typeof (Join), typeof (Where2<,>).MakeGenericType(typeof (Where), typeof (And<>).MakeGenericType(where)), typeof (Aggregate))))).Compile();
        Search5<Field, Join, Where, Aggregate>.dict.TryAdd(where, func);
      }
      return func();
    }
    return (BqlCommand) Activator.CreateInstance(typeof (Search5<,,,>).MakeGenericType(typeof (Field), typeof (Join), typeof (Where2<,>).MakeGenericType(typeof (Where), typeof (And<>).MakeGenericType(where)), typeof (Aggregate)));
  }

  public override BqlCommand WhereOr<where>()
  {
    return (BqlCommand) new Search5<Field, Join, Where2<Where, Or<where>>, Aggregate>();
  }

  public override BqlCommand WhereOr(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search5<,,,>).MakeGenericType(typeof (Field), typeof (Join), typeof (Where2<,>).MakeGenericType(typeof (Where), typeof (Or<>).MakeGenericType(where)), typeof (Aggregate)));
  }

  public override BqlCommand WhereNot()
  {
    return (BqlCommand) new Search5<Field, Join, PX.Data.Where<Not<Where>>, Aggregate>();
  }

  public override BqlCommand OrderByNew<newOrderBy>()
  {
    return (BqlCommand) new Search5<Field, Join, Where, Aggregate, newOrderBy>();
  }

  public override BqlCommand OrderByNew(System.Type newOrderBy)
  {
    if (newOrderBy == (System.Type) null)
      return (BqlCommand) new Search5<Field, Join, Where, Aggregate>();
    return (BqlCommand) Activator.CreateInstance(typeof (Search5<,,,,>).MakeGenericType(typeof (Field), typeof (Join), typeof (Where), typeof (Aggregate), newOrderBy));
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
