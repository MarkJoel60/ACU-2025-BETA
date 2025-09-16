// Decompiled with JetBrains decompiler
// Type: PX.Data.AggregatedFnBase`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class AggregatedFnBase<Field, NextAggregate> : 
  IBqlFunction,
  IBqlCreator,
  IBqlVerifier
  where Field : IBqlField
  where NextAggregate : IBqlFunction, new()
{
  private IBqlFunction _next;

  private IBqlFunction ensureNext()
  {
    return !(typeof (NextAggregate) == typeof (BqlNone)) ? this._next ?? (this._next = (IBqlFunction) new NextAggregate()) : (IBqlFunction) null;
  }

  public void GetAggregates(List<IBqlFunction> fields)
  {
    fields.Add((IBqlFunction) this);
    if (this.ensureNext() == null)
      return;
    this._next.GetAggregates(fields);
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag = true;
    if (this.ensureNext() != null)
      flag &= this._next.AppendExpression(ref exp, graph, info, selection);
    return flag;
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (this.ensureNext() == null)
      return;
    this._next.Verify(cache, item, pars, ref result, ref value);
  }

  public System.Type GetField() => typeof (Field);

  public bool IsGroupBy() => false;

  public abstract string GetFunction();
}
