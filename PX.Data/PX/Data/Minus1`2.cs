// Decompiled with JetBrains decompiler
// Type: PX.Data.Minus1`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public sealed class Minus1<Search1, Operand2> : BqlFunction, IBqlOperand, IBqlCreator, IBqlVerifier
  where Search1 : IBqlSearch
  where Operand2 : IBqlOperand
{
  private IBqlCreator _search1;
  private IBqlCreator _operand2;

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    value = (object) null;
    object obj1 = (object) null;
    if (this._search1 == null)
      this._search1 = this._search1.createOperand<Search1>();
    this._search1.Verify(cache, item, pars, ref result, ref obj1);
    object obj2;
    if (obj1 == null || !BqlFunction.getValue<Operand2>(ref this._operand2, cache, item, pars, ref result, out obj2) || obj2 == null)
      return;
    value = Sub<Operand2, Operand2>.calculateValue(obj1, obj2);
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag1 = true;
    if (this._search1 == null)
      this._search1 = this._search1.createOperand<Search1>();
    Query queryInternal = ((IBqlSearch) this._search1).GetQueryInternal(graph, info, selection);
    if (queryInternal == null)
    {
      exp = (SQLExpression) new SQLDateDiff((IConstant<string>) new DateDiff.minute());
      return false;
    }
    SQLExpression right = (SQLExpression) null;
    if (info.BuildExpression)
    {
      queryInternal.Limit(1);
      queryInternal.ClearSelection().Fields((SQLExpression) new Column(((IBqlSearch) this._search1).GetField()));
      right = new SubQuery(queryInternal).Embrace();
    }
    SQLExpression exp1 = (SQLExpression) null;
    bool flag2 = flag1 & this.GetOperandExpression<Operand2>(ref exp1, ref this._operand2, graph, info, selection);
    if (info.BuildExpression)
      exp = (SQLExpression) new SQLDateDiff((IConstant<string>) new DateDiff.minute(), exp1, right);
    return flag2;
  }
}
