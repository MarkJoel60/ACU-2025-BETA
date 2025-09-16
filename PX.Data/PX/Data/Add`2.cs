// Decompiled with JetBrains decompiler
// Type: PX.Data.Add`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>Returns the sum of the <tt>Operand1</tt> and <tt>Operand2</tt> values.</summary>
/// <typeparam name="Operand1">A field, constant, or function.</typeparam>
/// <typeparam name="Operand2">A field, constant, or function.</typeparam>
/// <remarks>You can use fluent BQL <see cref="T:PX.Data.BQL.BqlOperand`2.Add`1" /> instead.</remarks>
/// <example><para>The code below shows a DAC field definition, which is calculated by the following formula:
///   ARPayment.CuryDocBal - (ARPayment.CuryApplAmt + ARPayment.CurySOApplAmt)</para>
/// <code title="Example" lang="CS">
/// [PXCurrency(typeof(ARPayment.curyInfoID), typeof(ARPayment.unappliedBal))]
/// [PXUIField(DisplayName = "Available Balance", Visibility = PXUIVisibility.Visible, Enabled = false)]
/// [PXFormula(typeof(Sub&lt;ARPayment.curyDocBal, Add&lt;ARPayment.curyApplAmt, ARPayment.curySOApplAmt&gt;&gt;))]
/// public virtual Decimal? CuryUnappliedBal
/// {
/// ...
/// }</code>
/// </example>
public sealed class Add<Operand1, Operand2> : 
  BqlFunction,
  IBqlOperand,
  IBqlCreator,
  IBqlVerifier,
  IBqlAggregateOperand
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
        value = (object) ((int) Convert.ToInt16(obj1) + (int) Convert.ToInt16(obj2));
        break;
      case TypeCode.Int32:
        value = (object) (Convert.ToInt32(obj1) + Convert.ToInt32(obj2));
        break;
      case TypeCode.Int64:
        value = (object) (Convert.ToInt64(obj1) + Convert.ToInt64(obj2));
        break;
      case TypeCode.Double:
        value = (object) (Convert.ToDouble(obj1) + Convert.ToDouble(obj2));
        break;
      case TypeCode.Decimal:
        value = (object) (Convert.ToDecimal(obj1) + Convert.ToDecimal(obj2));
        break;
      case TypeCode.DateTime:
        if (obj2 is TimeSpan timeSpan)
        {
          value = (object) Convert.ToDateTime(obj1).Add(timeSpan);
          break;
        }
        value = (object) Convert.ToDateTime(obj1).AddDays((double) Convert.ToInt32(obj2));
        break;
      case TypeCode.String:
        value = (object) (Convert.ToString(obj1) + Convert.ToString(obj2));
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
    if (graph != null && info.BuildExpression)
    {
      if (exp1 is Literal literal)
      {
        // ISSUE: explicit non-virtual call
        __nonvirtual (literal.SetDBType(this.getTypeCodeForOperand<Operand1>(graph).GetDBType()));
      }
      if (exp1 is SQLConst sqlConst)
      {
        // ISSUE: explicit non-virtual call
        __nonvirtual (sqlConst.SetDBType(this.getTypeCodeForOperand<Operand1>(graph).GetDBType()));
      }
    }
    SQLExpression exp2 = (SQLExpression) null;
    int num2 = this.GetOperandExpression<Operand2>(ref exp2, ref this._operand2, graph, info, selection) ? 1 : 0;
    int num3 = num1 & num2;
    if (graph != null && info.BuildExpression)
    {
      if (exp2 is Literal literal)
      {
        // ISSUE: explicit non-virtual call
        __nonvirtual (literal.SetDBType(this.getTypeCodeForOperand<Operand2>(graph).GetDBType()));
      }
      if (exp2 is SQLConst sqlConst)
      {
        // ISSUE: explicit non-virtual call
        __nonvirtual (sqlConst.SetDBType(this.getTypeCodeForOperand<Operand2>(graph).GetDBType()));
      }
    }
    if (!info.BuildExpression)
      return num3 != 0;
    exp = (SQLExpression) new SQLAdd(exp1, exp2);
    if (exp1.GetDBTypeOrDefault() == exp2.GetDBTypeOrDefault())
      exp1.GetDBTypeOrDefault();
    PXDbType type;
    if (exp1.GetDBTypeOrDefault().IsInt() && exp2.GetDBTypeOrDefault().IsString())
    {
      type = exp1.GetDBTypeOrDefault();
      if (exp2 is ISQLDBTypedExpression sqldbTypedExpression)
        sqldbTypedExpression.SetDBType(type);
      if (exp2 is Column column)
        column.SetDBType(type);
    }
    else if (exp2.GetDBTypeOrDefault().IsInt() && exp1.GetDBTypeOrDefault().IsString())
    {
      type = exp2.GetDBTypeOrDefault();
      if (exp1 is ISQLDBTypedExpression sqldbTypedExpression)
        sqldbTypedExpression.SetDBType(type);
      if (exp1 is Column column)
        column.SetDBType(type);
    }
    else
      type = SqlDbTypedExpressionHelper.SetExpressionDbType(exp1.GetDBTypeOrDefault(), exp2.GetDBTypeOrDefault(), exp1 as ISQLDBTypedExpression, exp2 as ISQLDBTypedExpression);
    (exp as SQLAdd).SetDBType(type);
    return num3 != 0;
  }
}
