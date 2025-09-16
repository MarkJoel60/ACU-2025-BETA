// Decompiled with JetBrains decompiler
// Type: PX.Data.Replace`3
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
/// Replaces all occurrences of a string with another string in
/// the source expression. Equivalent to SQL function REPLACE.
/// </summary>
/// <typeparam name="Operand">A field, constant, or function.</typeparam>
/// <typeparam name="toReplace">A field, constant, or function.</typeparam>
/// <typeparam name="replaceWith">A field, constant, or function.</typeparam>
/// <example><para>The code below shows a part of a BQL command and the corresponding SQL code (provided str_AAA and str_BBB are classes representing string constants "AAA" and "BBB").</para>
/// <code title="Example" lang="CS">
/// Replace&lt;Table1.field1, str_AAA, str_BBB&gt;</code>
/// <code title="" description="" lang="SQL">
/// REPLACE(Table1.Field1, "AAA", "BBB")</code>
/// </example>
public sealed class Replace<Operand, toReplace, replaceWith> : 
  BqlFunction,
  IBqlOperand,
  IBqlCreator,
  IBqlVerifier
  where Operand : IBqlOperand
  where toReplace : IBqlOperand
  where replaceWith : IBqlOperand
{
  private IBqlCreator _operand;
  private IBqlCreator _toreplace;
  private IBqlCreator _replacewith;

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
    object obj3;
    if (!BqlFunction.getValue<Operand>(ref this._operand, cache, item, pars, ref result, out obj1) || !BqlFunction.getValue<toReplace>(ref this._toreplace, cache, item, pars, ref result, out obj2) || !BqlFunction.getValue<replaceWith>(ref this._replacewith, cache, item, pars, ref result, out obj3) || obj1 == null || obj2 == null || obj3 == null)
      return;
    int typeCode1 = (int) System.Type.GetTypeCode(obj1.GetType());
    TypeCode typeCode2 = System.Type.GetTypeCode(obj2.GetType());
    TypeCode typeCode3 = System.Type.GetTypeCode(obj3.GetType());
    if (typeCode1 != 18 || typeCode2 != TypeCode.String || typeCode3 != TypeCode.String)
      return;
    value = (object) Convert.ToString(obj1).Replace(Convert.ToString(obj2), Convert.ToString(obj3));
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    SQLExpression exp1 = (SQLExpression) null;
    int num1 = 1 & (this.GetOperandExpression<Operand>(ref exp1, ref this._operand, graph, info, selection) ? 1 : 0);
    SQLExpression exp2 = (SQLExpression) null;
    int num2 = this.GetOperandExpression<toReplace>(ref exp2, ref this._toreplace, graph, info, selection) ? 1 : 0;
    int num3 = num1 & num2;
    SQLExpression exp3 = (SQLExpression) null;
    int num4 = this.GetOperandExpression<replaceWith>(ref exp3, ref this._replacewith, graph, info, selection) ? 1 : 0;
    int num5 = num3 & num4;
    if (!info.BuildExpression)
      return num5 != 0;
    exp = (exp1 ?? SQLExpression.None()).Replace(exp2, exp3);
    return num5 != 0;
  }
}
