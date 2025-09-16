// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.BqlCommandDecorator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.BQL;

public class BqlCommandDecorator : BqlCommand
{
  protected readonly BqlCommand InnerBqlCommand;

  protected BqlCommandDecorator(BqlCommand bqlCommand) => this.InnerBqlCommand = bqlCommand;

  public override void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    this.InnerBqlCommand.Verify(cache, item, pars, ref result, ref value);
  }

  public override bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this.InnerBqlCommand.AppendExpression(ref exp, graph, info, selection);
  }

  public override Query GetQueryInternal(
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this.InnerBqlCommand.GetQueryInternal(graph, info, selection);
  }

  public override System.Type GetFirstTable() => this.InnerBqlCommand.GetFirstTable();

  public override System.Type GetSelectType() => this.InnerBqlCommand.GetSelectType();

  public override BqlCommand WhereNew<TNewWhere>() => this.InnerBqlCommand.WhereNew<TNewWhere>();

  public override BqlCommand WhereNew(System.Type newWhere)
  {
    return this.InnerBqlCommand.WhereNew(newWhere);
  }

  public override BqlCommand WhereAnd<TWhere>() => this.InnerBqlCommand.WhereAnd<TWhere>();

  public override BqlCommand WhereAnd(System.Type where) => this.InnerBqlCommand.WhereAnd(where);

  public override BqlCommand WhereOr<TWhere>() => this.InnerBqlCommand.WhereOr<TWhere>();

  public override BqlCommand WhereOr(System.Type where) => this.InnerBqlCommand.WhereOr(where);

  public override BqlCommand WhereNot() => this.InnerBqlCommand.WhereNot();

  public override BqlCommand OrderByNew<TNewOrderBy>()
  {
    return this.InnerBqlCommand.OrderByNew<TNewOrderBy>();
  }

  public override BqlCommand OrderByNew(System.Type newOrderBy)
  {
    return this.InnerBqlCommand.OrderByNew(newOrderBy);
  }

  public override BqlCommand AggregateNew<newAggregate>()
  {
    return this.InnerBqlCommand.AggregateNew<newAggregate>();
  }

  public override BqlCommand AggregateNew(System.Type newAggregate)
  {
    return this.InnerBqlCommand.AggregateNew(newAggregate);
  }

  public System.Type GetOriginalType()
  {
    System.Type originalType = this.InnerBqlCommand is BqlCommandDecorator innerBqlCommand ? innerBqlCommand.GetOriginalType() : (System.Type) null;
    return (object) originalType != null ? originalType : this.InnerBqlCommand.GetType();
  }

  public BqlCommand Unwrap() => BqlCommand.CreateInstance(this.GetOriginalType());

  public static System.Type Unwrap(System.Type commandType)
  {
    if (commandType == (System.Type) null)
      return (System.Type) null;
    if (!typeof (BqlCommandDecorator).IsAssignableFrom(commandType))
      return commandType;
    return ((BqlCommandDecorator) BqlCommand.CreateInstance(commandType)).GetOriginalType();
  }
}
