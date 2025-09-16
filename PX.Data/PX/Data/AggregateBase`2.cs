// Decompiled with JetBrains decompiler
// Type: PX.Data.AggregateBase`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class AggregateBase<TFunctions, THaving> : IBqlAggregate, IBqlCreator, IBqlVerifier
  where TFunctions : IBqlFunction, new()
  where THaving : IBqlHaving, new()
{
  private IBqlFunction _Function;
  private IBqlHaving _Having;

  private IBqlFunction ensureFunction()
  {
    return this._Function ?? (this._Function = (IBqlFunction) new TFunctions());
  }

  private IBqlHaving ensureHaving()
  {
    IBqlHaving having = this._Having;
    if (having != null)
      return having;
    return !(typeof (THaving) == typeof (BqlNone)) ? (this._Having = (IBqlHaving) new THaving()) : (IBqlHaving) null;
  }

  /// <exclude />
  public IBqlFunction[] GetAggregates()
  {
    List<IBqlFunction> fields = new List<IBqlFunction>();
    this.ensureFunction().GetAggregates(fields);
    return fields.ToArray();
  }

  /// <exclude />
  public IBqlHaving Having => this.ensureHaving();

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return (1 & (this.ensureFunction().AppendExpression(ref exp, graph, info, selection) ? 1 : 0)) != 0;
  }

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (this.ensureHaving() == null)
      return;
    this._Having.Verify(cache, item, pars, ref result, ref value);
  }
}
