// Decompiled with JetBrains decompiler
// Type: PX.Data.Minus`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Returns <tt>-Operand</tt> (multiplies by -1).
/// </summary>
/// <typeparam name="Operand">A field, constant, or function.</typeparam>
/// <example><para>The code below shows the usage of the Minus&lt;&gt; class in the calculation of a DAC field through the %PXDBCalced:PXDBCalcedAttribute% attribute.</para>
/// <code title="Example" lang="CS">
/// [PXDefault(TypeCode.Decimal, "0.0")]
/// [PXDBCalced(
///     typeof(Switch&lt;
///         Case&lt;Where&lt;LandedCostTran.aPDocType, Equal&lt;APDocType.debitAdj&gt;&gt;,
///             Minus&lt;LandedCostTran.curyLCAPAmount&gt;&gt;,
///         LandedCostTran.curyLCAPAmount&gt;),
///     typeof(Decimal))]
/// [PXUIField(DisplayName = "Amount")]
/// public virtual Decimal? CuryLCAPEffAmount { get; set; }</code>
/// </example>
public sealed class Minus<Operand> : BqlFunction, IBqlOperand, IBqlCreator, IBqlVerifier where Operand : IBqlOperand
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
        value = (object) (int) -Convert.ToInt16(obj);
        break;
      case TypeCode.Int32:
        value = (object) -Convert.ToInt32(obj);
        break;
      case TypeCode.Int64:
        value = (object) -Convert.ToInt64(obj);
        break;
      case TypeCode.Double:
        value = (object) -Convert.ToDouble(obj);
        break;
      case TypeCode.Decimal:
        value = (object) -Convert.ToDecimal(obj);
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
    bool flag = true;
    SQLExpression exp1 = (SQLExpression) null;
    if (typeof (IBqlCreator).IsAssignableFrom(typeof (Operand)))
    {
      if (this._operand == null)
        this._operand = this._operand.createOperand<Operand>();
      flag &= this._operand.AppendExpression(ref exp1, graph, info, selection);
    }
    else if (info.BuildExpression)
      exp1 = BqlCommand.GetSingleExpression(typeof (Operand), graph, info.Tables, selection, BqlCommand.FieldPlace.Condition);
    if (info.BuildExpression)
      exp = -exp1;
    return flag;
  }
}
