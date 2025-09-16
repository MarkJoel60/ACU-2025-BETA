// Decompiled with JetBrains decompiler
// Type: PX.Data.Search`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Retrieves a field from a table, applying filtering and ordering.
/// </summary>
/// <typeparam name="Field">The DAC field whose value is selected.</typeparam>
/// <typeparam name="Where">The WHERE clause.</typeparam>
/// <typeparam name="OrderBy">The ORDER BY clause.</typeparam>
/// <example><para>The code below shows a part of the DAC field definition. The Search&lt;,,&gt; class is used to specify the source of the default value for the FinPeriodID field.</para>
/// <code title="Example" lang="CS">
/// [PXDefault(typeof(
///     Search&lt;FinPeriod.finPeriodID,
///         Where&lt;FinPeriod.aPClosed, Equal&lt;False&gt;&gt;,
///         OrderBy&lt;Desc&lt;FinPeriod.finPeriodID&gt;&gt;&gt;))]
/// [PXUIField(DisplayName = "Fin. Period")]
/// public virtual String FinPeriodID { get; set; }</code>
/// </example>
public sealed class Search<Field, Where, OrderBy> : 
  SearchBase<Field, BqlNone, Where, BqlNone, OrderBy>
  where Field : IBqlField
  where Where : IBqlWhere, new()
  where OrderBy : IBqlOrderBy, new()
{
  private System.Type _SelectType;

  public override System.Type GetSelectType()
  {
    if (this._SelectType == (System.Type) null)
    {
      if (typeof (Field).IsNested && typeof (IBqlTable).IsAssignableFrom(BqlCommand.GetItemType(typeof (Field))))
        this._SelectType = typeof (Select<,,>).MakeGenericType(BqlCommand.GetItemType(typeof (Field)), typeof (Where), typeof (OrderBy));
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

  public override BqlCommand WhereAnd<where>()
  {
    return (BqlCommand) new Search<Field, Where2<Where, And<where>>, OrderBy>();
  }

  public override BqlCommand WhereAnd(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search<,,>).MakeGenericType(typeof (Field), typeof (Where2<,>).MakeGenericType(typeof (Where), typeof (And<>).MakeGenericType(where)), typeof (OrderBy)));
  }

  public override BqlCommand WhereOr<where>()
  {
    return (BqlCommand) new Search<Field, Where2<Where, Or<where>>, OrderBy>();
  }

  public override BqlCommand WhereOr(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search<,,>).MakeGenericType(typeof (Field), typeof (Where2<,>).MakeGenericType(typeof (Where), typeof (Or<>).MakeGenericType(where)), typeof (OrderBy)));
  }

  public override BqlCommand WhereNot()
  {
    return (BqlCommand) new Search<Field, PX.Data.Where<Not<Where>>, OrderBy>();
  }

  public override BqlCommand OrderByNew<newOrderBy>()
  {
    return (BqlCommand) new Search<Field, Where, newOrderBy>();
  }

  public override BqlCommand OrderByNew(System.Type newOrderBy)
  {
    if (newOrderBy == (System.Type) null)
      return (BqlCommand) new Search<Field, Where>();
    return (BqlCommand) Activator.CreateInstance(typeof (Search<,,>).MakeGenericType(typeof (Field), typeof (Where), newOrderBy));
  }

  public override BqlCommand AggregateNew<newAggregate>()
  {
    return (BqlCommand) new Search4<Field, Where, newAggregate, OrderBy>();
  }

  public override BqlCommand AggregateNew(System.Type newAggregate)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search4<,>).MakeGenericType(typeof (Field), typeof (Where), newAggregate, typeof (OrderBy)));
  }
}
