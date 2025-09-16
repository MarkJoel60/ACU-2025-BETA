// Decompiled with JetBrains decompiler
// Type: PX.Data.Search4`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Retrieves an aggregated field value.</summary>
/// <typeparam name="Field">The DAC field whose value is selected.</typeparam>
/// <typeparam name="Aggregate"><tt>Aggregate</tt> clause wrapping <tt>Sum</tt>,
/// <tt>Avg</tt>, <tt>Min</tt>, <tt>Max</tt> or <tt>GroupBy</tt> functions.</typeparam>
public sealed class Search4<Field, Aggregate> : 
  SearchBase<Field, BqlNone, BqlNone, Aggregate, BqlNone>,
  IBqlAggregate,
  IBqlCreator,
  IBqlVerifier
  where Field : IBqlField
  where Aggregate : IBqlAggregate, new()
{
  private System.Type _SelectType;

  public IBqlFunction[] GetAggregates() => this.ensureAggregate().GetAggregates();

  /// <exclude />
  public IBqlHaving Having => this.ensureAggregate().Having;

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

  public override BqlCommand WhereNew<newWhere>()
  {
    return (BqlCommand) new Search4<Field, newWhere, Aggregate>();
  }

  public override BqlCommand WhereNew(System.Type newWhere)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search4<,,>).MakeGenericType(typeof (Field), newWhere, typeof (Aggregate)));
  }

  public override BqlCommand WhereAnd<where>()
  {
    return (BqlCommand) new Search4<Field, where, Aggregate>();
  }

  public override BqlCommand WhereAnd(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search4<,,>).MakeGenericType(typeof (Field), where, typeof (Aggregate)));
  }

  public override BqlCommand WhereOr<where>()
  {
    return (BqlCommand) new Search4<Field, where, Aggregate>();
  }

  public override BqlCommand WhereOr(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search4<,,>).MakeGenericType(typeof (Field), where, typeof (Aggregate)));
  }

  public override BqlCommand WhereNot() => (BqlCommand) new Search4<Field, Aggregate>();

  public override BqlCommand OrderByNew<newOrderBy>()
  {
    return (BqlCommand) new Search6<Field, Aggregate, newOrderBy>();
  }

  public override BqlCommand OrderByNew(System.Type newOrderBy)
  {
    if (newOrderBy == (System.Type) null)
      return (BqlCommand) new Search4<Field, Aggregate>();
    return (BqlCommand) Activator.CreateInstance(typeof (Search6<,,>).MakeGenericType(typeof (Field), typeof (Aggregate), newOrderBy));
  }

  public override BqlCommand AggregateNew<newAggregate>()
  {
    return (BqlCommand) new Search4<Field, newAggregate>();
  }

  public override BqlCommand AggregateNew(System.Type newAggregate)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search4<,>).MakeGenericType(typeof (Field), newAggregate));
  }
}
