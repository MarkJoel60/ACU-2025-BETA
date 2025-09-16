// Decompiled with JetBrains decompiler
// Type: PX.Data.Div`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Returns the division of <tt>Operand1</tt> and <tt>Operand2</tt></summary>
/// <typeparam name="Operand1">A field, constant, or function.</typeparam>
/// <typeparam name="Operand2">A field, constant, or function.</typeparam>
/// <example><para>The code below shows the definition of a DAC field and the SQL expression that is inserted into the query to get the value for the DAC field.</para>
/// <code title="Example" lang="CS">
/// [PXDBCalced(
///     typeof(Switch&lt;Case&lt;Where&lt;ARTran.qty, NotEqual&lt;decimal0&gt;&gt;, Div&lt;ARTran.tranCost, ARTran.qty&gt;&gt;, decimal0&gt;),
///     typeof(Decimal))]
/// [PXDefault(TypeCode.Decimal, "0.0")]
/// public virtual Decimal? UnitCost { get; set; }</code>
/// <code title="" description="" lang="SQL">
/// CASE
///     WHEN ARTran.Qty != 10m THEN ARTran.TranCost / ARTran.Qty
///     ELSE 10m
/// END</code>
/// </example>
public sealed class Div<Operand1, Operand2> : 
  BqlFunction,
  IBqlOperand,
  IBqlCreator,
  IBqlVerifier,
  IBqlAggregateOperand,
  IImplement<IBqlCastableTo<IBqlDecimal>>,
  IImplement<IBqlCastableTo<IBqlDouble>>
  where Operand1 : IBqlOperand
  where Operand2 : IBqlOperand
{
  private IBqlCreator _operand1;
  private IBqlCreator _operand2;

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    value = (object) null;
    object obj1;
    object obj2;
    if (!BqlFunction.getValue<Operand1>(ref this._operand1, cache, item, pars, ref result, out obj1) || obj1 == null || !BqlFunction.getValue<Operand2>(ref this._operand2, cache, item, pars, ref result, out obj2) || obj2 == null)
      return;
    switch (System.Type.GetTypeCode(obj1.GetType()))
    {
      case TypeCode.Int16:
        value = (object) ((int) Convert.ToInt16(obj1) / (int) Convert.ToInt16(obj2));
        break;
      case TypeCode.Int32:
        value = (object) (Convert.ToInt32(obj1) / Convert.ToInt32(obj2));
        break;
      case TypeCode.Int64:
        value = (object) (Convert.ToInt64(obj1) / Convert.ToInt64(obj2));
        break;
      case TypeCode.Double:
        value = (object) (Convert.ToDouble(obj1) / Convert.ToDouble(obj2));
        break;
      case TypeCode.Decimal:
        value = (object) (Convert.ToDecimal(obj1) / Convert.ToDecimal(obj2));
        break;
    }
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    SQLExpression exp1 = (SQLExpression) null;
    int num1 = 1 & (this.GetOperandExpression<Operand1>(ref exp1, ref this._operand1, graph, info, selection) ? 1 : 0);
    SQLExpression exp2 = (SQLExpression) null;
    int num2 = this.GetOperandExpression<Operand2>(ref exp2, ref this._operand2, graph, info, selection) ? 1 : 0;
    int num3 = num1 & num2;
    if (!info.BuildExpression)
      return num3 != 0;
    exp = (exp1 / exp2).Embrace();
    return num3 != 0;
  }
}
