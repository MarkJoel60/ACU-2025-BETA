// Decompiled with JetBrains decompiler
// Type: PX.Data.Search6`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Retrieves an aggregated field value, applying ordering.
/// </summary>
/// <typeparam name="Field">The DAC field whose value is selected.</typeparam>
/// <typeparam name="Aggregate"><tt>Aggregate</tt> clause wrapping <tt>Sum</tt>,
/// <tt>Avg</tt>, <tt>Min</tt>, <tt>Max</tt> or <tt>GroupBy</tt> functions.</typeparam>
/// <typeparam name="OrderBy">The ORDER BY clause.</typeparam>
public sealed class Search6<Field, Aggregate, OrderBy> : 
  SearchBase<Field, BqlNone, BqlNone, Aggregate, OrderBy>,
  IBqlAggregate,
  IBqlCreator,
  IBqlVerifier
  where Field : IBqlField
  where Aggregate : IBqlAggregate, new()
  where OrderBy : IBqlOrderBy, new()
{
  private System.Type _SelectType;

  /// <exclude />
  public IBqlFunction[] GetAggregates() => this.ensureAggregate().GetAggregates();

  /// <exclude />
  public IBqlHaving Having => this.ensureAggregate().Having;

  /// <exclude />
  public override System.Type GetSelectType()
  {
    if (this._SelectType == (System.Type) null)
    {
      if (typeof (Field).IsNested && typeof (IBqlTable).IsAssignableFrom(BqlCommand.GetItemType(typeof (Field))))
        this._SelectType = typeof (Select4<,>).MakeGenericType(BqlCommand.GetItemType(typeof (Field)), typeof (Aggregate));
      else
        this._SelectType = this.GetType();
    }
    return this._SelectType;
  }

  /// <exclude />
  public override BqlCommand WhereNew<newWhere>()
  {
    return (BqlCommand) new Search4<Field, newWhere, Aggregate, OrderBy>();
  }

  /// <exclude />
  public override BqlCommand WhereNew(System.Type newWhere)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search4<,,,>).MakeGenericType(typeof (Field), newWhere, typeof (Aggregate), typeof (OrderBy)));
  }

  /// <exclude />
  public override BqlCommand WhereAnd<where>()
  {
    return (BqlCommand) new Search4<Field, where, Aggregate, OrderBy>();
  }

  /// <exclude />
  public override BqlCommand WhereAnd(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search4<,,,>).MakeGenericType(typeof (Field), where, typeof (Aggregate), typeof (OrderBy)));
  }

  /// <exclude />
  public override BqlCommand WhereOr<where>()
  {
    return (BqlCommand) new Search4<Field, where, Aggregate, OrderBy>();
  }

  /// <exclude />
  public override BqlCommand WhereOr(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search4<,,,>).MakeGenericType(typeof (Field), where, typeof (Aggregate), typeof (OrderBy)));
  }

  /// <exclude />
  public override BqlCommand WhereNot() => (BqlCommand) new Search6<Field, Aggregate, OrderBy>();

  /// <exclude />
  public override BqlCommand OrderByNew<newOrderBy>()
  {
    return (BqlCommand) new Search6<Field, Aggregate, newOrderBy>();
  }

  /// <exclude />
  public override BqlCommand OrderByNew(System.Type newOrderBy)
  {
    if (newOrderBy == (System.Type) null)
      return (BqlCommand) new Search4<Field, Aggregate>();
    return (BqlCommand) Activator.CreateInstance(typeof (Search6<,,>).MakeGenericType(typeof (Field), typeof (Aggregate), newOrderBy));
  }

  public override BqlCommand AggregateNew<newAggregate>()
  {
    return (BqlCommand) new Search6<Field, newAggregate, OrderBy>();
  }

  public override BqlCommand AggregateNew(System.Type newAggregate)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search6<,,>).MakeGenericType(typeof (Field), newAggregate, typeof (OrderBy)));
  }
}
