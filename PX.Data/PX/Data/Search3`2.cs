// Decompiled with JetBrains decompiler
// Type: PX.Data.Search3`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Retrieves a field value, applying ordering.</summary>
/// <typeparam name="Field">The DAC field whose value is selected.</typeparam>
/// <typeparam name="OrderBy">The ORDER BY clause.</typeparam>
public sealed class Search3<Field, OrderBy> : SearchBase<Field, BqlNone, BqlNone, BqlNone, OrderBy>
  where Field : IBqlField
  where OrderBy : IBqlOrderBy, new()
{
  private System.Type _SelectType;

  public override System.Type GetSelectType()
  {
    if (this._SelectType == (System.Type) null)
    {
      if (typeof (Field).IsNested && typeof (IBqlTable).IsAssignableFrom(BqlCommand.GetItemType(typeof (Field))))
        this._SelectType = typeof (Select3<,>).MakeGenericType(BqlCommand.GetItemType(typeof (Field)), typeof (OrderBy));
      else
        this._SelectType = this.GetType();
    }
    return this._SelectType;
  }

  public override BqlCommand WhereNew<newWhere>()
  {
    return (BqlCommand) new Search<Field, newWhere, OrderBy>();
  }

  public override BqlCommand WhereNew(System.Type newWhere)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search<,,>).MakeGenericType(typeof (Field), newWhere, typeof (OrderBy)));
  }

  public override BqlCommand WhereAnd<where>() => (BqlCommand) new Search<Field, where>();

  public override BqlCommand WhereAnd(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search<,,>).MakeGenericType(typeof (Field), where, typeof (OrderBy)));
  }

  public override BqlCommand WhereOr<where>() => (BqlCommand) new Search<Field, where, OrderBy>();

  public override BqlCommand WhereOr(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search<,,>).MakeGenericType(typeof (Field), where, typeof (OrderBy)));
  }

  public override BqlCommand WhereNot() => (BqlCommand) new Search3<Field, OrderBy>();

  public override BqlCommand OrderByNew<newOrderBy>()
  {
    return (BqlCommand) new Search3<Field, newOrderBy>();
  }

  public override BqlCommand OrderByNew(System.Type newOrderBy)
  {
    if (newOrderBy == (System.Type) null)
      return (BqlCommand) new Search<Field>();
    return (BqlCommand) Activator.CreateInstance(typeof (Search3<,>).MakeGenericType(typeof (Field), newOrderBy));
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
