// Decompiled with JetBrains decompiler
// Type: PX.Data.Search4`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Retrieves an aggregated field value, applying filtering.
/// </summary>
/// <typeparam name="Field">The DAC field whose value is selected.</typeparam>
/// <typeparam name="Where">The WHERE clause.</typeparam>
/// <typeparam name="Aggregate"><tt>Aggregate</tt> clause wrapping <tt>Sum</tt>,
/// <tt>Avg</tt>, <tt>Min</tt>, <tt>Max</tt> or <tt>GroupBy</tt> functions.</typeparam>
public sealed class Search4<Field, Where, Aggregate> : 
  SearchBase<Field, BqlNone, Where, Aggregate, BqlNone>,
  IBqlAggregate,
  IBqlCreator,
  IBqlVerifier
  where Field : IBqlField
  where Where : IBqlWhere, new()
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
        this._SelectType = typeof (Select4<,,>).MakeGenericType(BqlCommand.GetItemType(typeof (Field)), typeof (Where), typeof (Aggregate));
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
    return (BqlCommand) new Search4<Field, Where2<Where, And<where>>, Aggregate>();
  }

  public override BqlCommand WhereAnd(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search4<,,>).MakeGenericType(typeof (Field), typeof (Where2<,>).MakeGenericType(typeof (Where), typeof (And<>).MakeGenericType(where)), typeof (Aggregate)));
  }

  public override BqlCommand WhereOr<where>()
  {
    return (BqlCommand) new Search4<Field, Where2<Where, Or<where>>, Aggregate>();
  }

  public override BqlCommand WhereOr(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search4<,,>).MakeGenericType(typeof (Field), typeof (Where2<,>).MakeGenericType(typeof (Where), typeof (Or<>).MakeGenericType(where)), typeof (Aggregate)));
  }

  public override BqlCommand WhereNot()
  {
    return (BqlCommand) new Search4<Field, PX.Data.Where<Not<Where>>, Aggregate>();
  }

  public override BqlCommand OrderByNew<newOrderBy>()
  {
    return (BqlCommand) new Search4<Field, Where, Aggregate, newOrderBy>();
  }

  public override BqlCommand OrderByNew(System.Type newOrderBy)
  {
    if (newOrderBy == (System.Type) null)
      return (BqlCommand) new Search4<Field, Where, Aggregate>();
    return (BqlCommand) Activator.CreateInstance(typeof (Search4<,,,>).MakeGenericType(typeof (Field), typeof (Where), typeof (Aggregate), newOrderBy));
  }

  public override BqlCommand AggregateNew<newAggregate>()
  {
    return (BqlCommand) new Search4<Field, Where, newAggregate>();
  }

  public override BqlCommand AggregateNew(System.Type newAggregate)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search4<,,>).MakeGenericType(typeof (Field), typeof (Where), newAggregate));
  }
}
