// Decompiled with JetBrains decompiler
// Type: PX.Data.Search`2
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

/// <summary>Retrieves a field value, applying filtering.</summary>
/// <typeparam name="Field">The DAC field whose value is selected.</typeparam>
/// <typeparam name="Where">The WHERE clause.</typeparam>
/// <example><para>The code below shows the definition of a DAC field.</para>
/// <code title="Example" lang="CS">
/// [PXDBString(10, IsUnicode = true)]
/// [PXUIField(DisplayName = "Payment Method", Visibility = PXUIVisibility.SelectorVisible)]
/// [PXSelector(
///     typeof(Search&lt;PaymentMethod.paymentMethodID,
///         Where&lt;PaymentMethod.useForAP,Equal&lt;True&gt;&gt;&gt;),
///     DescriptionField = typeof(CA.PaymentMethod.descr))]
/// public virtual String PayTypeID { get; set; }</code>
/// </example>
public sealed class Search<Field, Where> : SearchBase<Field, BqlNone, Where, BqlNone, BqlNone>
  where Field : IBqlField
  where Where : IBqlWhere, new()
{
  private System.Type _SelectType;
  private static ConcurrentDictionary<System.Type, Func<BqlCommand>> dictNew = new ConcurrentDictionary<System.Type, Func<BqlCommand>>();
  private static ConcurrentDictionary<System.Type, Func<BqlCommand>> dict = new ConcurrentDictionary<System.Type, Func<BqlCommand>>();

  public override System.Type GetSelectType()
  {
    if (this._SelectType == (System.Type) null)
    {
      if (typeof (Field).IsNested && typeof (IBqlTable).IsAssignableFrom(BqlCommand.GetItemType(typeof (Field))))
        this._SelectType = typeof (Select<,>).MakeGenericType(BqlCommand.GetItemType(typeof (Field)), typeof (Where));
      else
        this._SelectType = this.GetType();
    }
    return this._SelectType;
  }

  public override BqlCommand WhereNew<newWhere>() => (BqlCommand) new Search<Field, newWhere>();

  public override BqlCommand WhereNew(System.Type newWhere)
  {
    if (WebConfig.EnablePageOpenOptimizations)
    {
      Func<BqlCommand> func = (Func<BqlCommand>) null;
      if (!Search<Field, Where>.dictNew.TryGetValue(newWhere, out func))
      {
        func = ((Expression<Func<BqlCommand>>) (() => Expression.New(typeof (Search<,>).MakeGenericType(typeof (Field), newWhere)))).Compile();
        Search<Field, Where>.dictNew.TryAdd(newWhere, func);
      }
      return func();
    }
    return (BqlCommand) Activator.CreateInstance(typeof (Search<,>).MakeGenericType(typeof (Field), newWhere));
  }

  public override BqlCommand WhereAnd<where>()
  {
    return (BqlCommand) new Search<Field, Where2<Where, And<where>>>();
  }

  public override BqlCommand WhereAnd(System.Type where)
  {
    if (WebConfig.EnablePageOpenOptimizations)
    {
      Func<BqlCommand> func = (Func<BqlCommand>) null;
      if (!Search<Field, Where>.dict.TryGetValue(where, out func))
      {
        func = ((Expression<Func<BqlCommand>>) (() => Expression.New(typeof (Search<,>).MakeGenericType(typeof (Field), typeof (Where2<,>).MakeGenericType(typeof (Where), typeof (And<>).MakeGenericType(where)))))).Compile();
        Search<Field, Where>.dict.TryAdd(where, func);
      }
      return func();
    }
    return (BqlCommand) Activator.CreateInstance(typeof (Search<,>).MakeGenericType(typeof (Field), typeof (Where2<,>).MakeGenericType(typeof (Where), typeof (And<>).MakeGenericType(where))));
  }

  public override BqlCommand WhereOr<where>()
  {
    return (BqlCommand) new Search<Field, Where2<Where, Or<where>>>();
  }

  public override BqlCommand WhereOr(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search<,>).MakeGenericType(typeof (Field), typeof (Where2<,>).MakeGenericType(typeof (Where), typeof (Or<>).MakeGenericType(where))));
  }

  public override BqlCommand WhereNot() => (BqlCommand) new Search<Field, PX.Data.Where<Not<Where>>>();

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
    return (BqlCommand) new Search4<Field, Where, newAggregate>();
  }

  public override BqlCommand AggregateNew(System.Type newAggregate)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search4<,,>).MakeGenericType(typeof (Field), typeof (Where), newAggregate));
  }
}
