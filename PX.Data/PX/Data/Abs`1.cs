// Decompiled with JetBrains decompiler
// Type: PX.Data.Abs`1
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

/// <summary>Returns module number.</summary>
/// <typeparam name="Operand">A field, constant, or function.</typeparam>
/// <example><para>The code below shows the usage of the Abs&lt;&gt; class in the calculation of a DAC field through the %PXDBCalced:PXDBCalcedAttribute% attribute.</para>
/// <code title="Example" lang="CS">
/// [PXDefault(TypeCode.Decimal, "0.0")]
/// [PXDBCalced(
///     typeof(Switch&lt;
///         Case&lt;Where&lt;LandedCostTran.aPDocType, Equal&lt;APDocType.debitAdj&gt;&gt;,
///             Abs&lt;LandedCostTran.curyLCAPAmount&gt;&gt;,
///         LandedCostTran.curyLCAPAmount&gt;),
///     typeof(Decimal))]
/// [PXUIField(DisplayName = "Amount")]
/// public virtual Decimal? CuryLCAPEffAmount { get; set; }</code>
/// </example>
public sealed class Abs<Operand> : 
  BqlFunction,
  IBqlOperand,
  IBqlCreator,
  IBqlVerifier,
  IBqlAggregateOperand,
  IImplement<IBqlCastableTo<IBqlDecimal>>,
  IImplement<IBqlCastableTo<IBqlDouble>>
  where Operand : IBqlOperand
{
  private IBqlCreator _operand;

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    value = (object) null;
    object obj;
    if (!BqlFunction.getValue<Operand>(ref this._operand, cache, item, pars, ref result, out obj) || obj == null)
      return;
    switch (System.Type.GetTypeCode(obj.GetType()))
    {
      case TypeCode.Int16:
        value = (object) System.Math.Abs(Convert.ToInt16(obj));
        break;
      case TypeCode.Int32:
        value = (object) System.Math.Abs(Convert.ToInt32(obj));
        break;
      case TypeCode.Int64:
        value = (object) System.Math.Abs(Convert.ToInt64(obj));
        break;
      case TypeCode.Double:
        value = (object) System.Math.Abs(Convert.ToDouble(obj));
        break;
      case TypeCode.Decimal:
        value = (object) System.Math.Abs(Convert.ToDecimal(obj));
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
    int num = 1 & (this.GetOperandExpression<Operand>(ref exp1, ref this._operand, graph, info, selection) ? 1 : 0);
    if (!info.BuildExpression)
      return num != 0;
    exp = (exp1 ?? SQLExpression.None()).Abs();
    return num != 0;
  }
}
