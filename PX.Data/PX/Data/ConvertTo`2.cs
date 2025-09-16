// Decompiled with JetBrains decompiler
// Type: PX.Data.ConvertTo`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

public class ConvertTo<Source, TargetType> : BqlFunction, IBqlOperand, IBqlCreator, IBqlVerifier where Source : IBqlOperand
{
  private IBqlCreator _source;

  protected virtual int Length { get; }

  protected virtual object ConvertValue(object value) => (object) value.ToInvariantString();

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (!BqlFunction.getValue<Source>(ref this._source, cache, item, pars, ref result, out value) || value == null)
      return;
    value = this.ConvertValue(value);
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    SQLExpression exp1 = (SQLExpression) null;
    int num = 1 & (this.GetOperandExpression<Source>(ref exp1, ref this._source, graph, info, selection) ? 1 : 0);
    if (!info.BuildExpression)
      return num != 0;
    exp = exp1 != null ? (SQLExpression) new SQLConvert(typeof (TargetType), exp1, this.Length) : SQLExpression.None();
    return num != 0;
  }
}
