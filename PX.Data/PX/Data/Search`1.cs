// Decompiled with JetBrains decompiler
// Type: PX.Data.Search`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Retrieves a field value.</summary>
/// <typeparam name="Field">The DAC field whose value is selected.</typeparam>
/// <example><para>The code below shows the definition of a DAC field. The Search&lt;&gt; class is used in the PXDefault attribute to specify the field whose value should be used as the default value for the CashCuryID field.</para>
/// <code title="Example" lang="CS">
/// [PXDBString(5, IsUnicode = true, BqlField = typeof(CashAccount.curyID))]
/// [PXDefault(typeof(Search&lt;Company.baseCuryID&gt;))]
/// [PXUIField(DisplayName = "Cash Account Currency")]
/// public virtual string CashCuryID { get; set; }</code>
/// </example>
public sealed class Search<Field> : SearchBase<Field, BqlNone, BqlNone, BqlNone, BqlNone> where Field : IBqlField
{
  private System.Type _SelectType;

  public override System.Type GetSelectType()
  {
    if (this._SelectType == (System.Type) null)
    {
      if (typeof (Field).IsNested && typeof (IBqlTable).IsAssignableFrom(BqlCommand.GetItemType(typeof (Field))))
        this._SelectType = typeof (Select<>).MakeGenericType(BqlCommand.GetItemType(typeof (Field)));
      else
        this._SelectType = this.GetType();
    }
    return this._SelectType;
  }

  public override BqlCommand WhereNew<newWhere>() => (BqlCommand) new Search<Field, newWhere>();

  public override BqlCommand WhereNew(System.Type newWhere)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search<,>).MakeGenericType(typeof (Field), newWhere));
  }

  public override BqlCommand WhereAnd<where>() => (BqlCommand) new Search<Field, where>();

  public override BqlCommand WhereAnd(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search<,>).MakeGenericType(typeof (Field), where));
  }

  public override BqlCommand WhereOr<where>() => (BqlCommand) new Search<Field, where>();

  public override BqlCommand WhereOr(System.Type where)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search<,>).MakeGenericType(typeof (Field), where));
  }

  public override BqlCommand WhereNot() => (BqlCommand) new Search<Field>();

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
    return (BqlCommand) new Search4<Field, newAggregate>();
  }

  public override BqlCommand AggregateNew(System.Type newAggregate)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Search4<,>).MakeGenericType(typeof (Field), newAggregate));
  }
}
