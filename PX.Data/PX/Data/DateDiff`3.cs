// Decompiled with JetBrains decompiler
// Type: PX.Data.DateDiff`3
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
/// Returns the count of the datepart boundaries specified in UOM crossed
/// between <tt>Operand1</tt> and <tt>Operand2</tt>. Equivalent to SQL function DATEDIFF.
/// </summary>
/// <typeparam name="Operand1">A field, constant, or function.</typeparam>
/// <typeparam name="Operand2">A field, constant, or function.</typeparam>
/// <typeparam name="OUM">A string constant. Use nested classes of <tt>DateDiff</tt>.</typeparam>
/// <example><para>The code below shows a data view and the corresponding SQL query.</para>
/// <code title="Example" lang="CS">
/// PXSelect&lt;Table1,
///     Where&lt;DateDiff&lt;Table1.field1, Table1.field2, DateDiff.hour&gt; Greater&lt;Zero&gt;&gt;&gt; records;</code>
/// <code title="" description="" lang="SQL">
/// SELECT * FROM Table1
/// WHERE DATEDIFF(hh, Table1.Field1, Table1.Field2) &gt; 0</code>
/// </example>
public sealed class DateDiff<Operand1, Operand2, UOM> : 
  BqlFunction,
  IBqlOperand,
  IBqlCreator,
  IBqlVerifier,
  IImplement<IBqlCastableTo<IBqlInt>>
  where Operand1 : IBqlOperand
  where Operand2 : IBqlOperand
  where UOM : IConstant<string>, IBqlOperand, new()
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
    System.DateTime dateTime = new System.DateTime(1, 1, 1);
    TimeSpan timeSpan = Convert.ToDateTime(obj2) - Convert.ToDateTime(obj1);
    string str = new UOM().Value;
    if (str == null)
      return;
    switch (str.Length)
    {
      case 2:
        switch (str[1])
        {
          case 'd':
            if (!(str == "dd"))
              return;
            value = (object) Convert.ToInt32(timeSpan.TotalDays);
            return;
          case 'h':
            if (!(str == "hh"))
              return;
            value = (object) Convert.ToInt32(timeSpan.TotalHours);
            return;
          case 'i':
            if (!(str == "mi"))
              return;
            value = (object) Convert.ToInt32(timeSpan.TotalMinutes);
            return;
          case 'j':
          case 'k':
          case 'l':
          case 'n':
          case 'o':
          case 'p':
            return;
          case 'm':
            if (!(str == "mm"))
              return;
            value = (object) (((dateTime + timeSpan).Year - 1) * 12 + ((dateTime + timeSpan).Month - 1));
            return;
          case 'q':
            if (!(str == "qq"))
              return;
            value = (object) (((dateTime + timeSpan).Year - 1) * 4 + ((dateTime + timeSpan).Month - 1) / 3);
            return;
          case 's':
            switch (str)
            {
              case "ss":
                value = (object) Convert.ToInt32(timeSpan.TotalSeconds);
                return;
              case "ms":
                value = (object) Convert.ToInt32(timeSpan.TotalMilliseconds);
                return;
              default:
                return;
            }
          case 'w':
            if (!(str == "ww"))
              return;
            value = (object) Convert.ToInt32(timeSpan.TotalDays / 7.0);
            return;
          default:
            return;
        }
      case 4:
        if (!(str == "yyyy"))
          break;
        value = (object) ((dateTime + timeSpan).Year - 1);
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
    exp = (SQLExpression) new SQLDateDiff((IConstant<string>) new UOM(), exp1, exp2);
    return num3 != 0;
  }
}
