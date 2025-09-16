// Decompiled with JetBrains decompiler
// Type: PX.Data.Between`2
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
/// Checks if the value of the preceding operand falls between the values of
/// <tt>Operand1</tt> and <tt>Operand2</tt>. Equivalent to SQL operator BETWEEN.
/// </summary>
/// <typeparam name="Operand1">The beginning of the range.</typeparam>
/// <typeparam name="Operand2">The end of the range.</typeparam>
/// <example><para>The code below shows a data view that uses Between&lt;&gt; in the conditional expression and the SQL query corresponding to the data view.</para>
/// 	<code title="Example" lang="CS">
/// public PXSelect&lt;TaxRev,
///     Where&lt;TaxRev.taxID, Equal&lt;Required&lt;TaxRev.taxID&gt;&gt;,
///         And&lt;TaxRev.taxType, Equal&lt;Required&lt;TaxRev.taxType&gt;&gt;,
///         And&lt;TaxRev.outdated, Equal&lt;boolFalse&gt;,
///         And&lt;Required&lt;TaxRev.startDate&gt;, Between&lt;TaxRev.startDate, TaxRev.endDate&gt;&gt;&gt;&gt;&gt;&gt;
///         SalesTaxRev_Select;
/// ...
/// // Executing the data view
/// TaxRev _TaxRev = SalesTaxRev_Select.Select(tran.TaxID, tran.TaxType, tran.TranDate);</code>
/// 	<code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM TaxRev
/// WHERE ( TaxRev.TaxID = [tran.TaxID value]
///     AND TaxRev.TaxType = [tran.TaxType value]
///     AND TaxRev.OutDated = CONVERT(bit, 0)
///     AND [tran.TranDate value] BETWEEN TaxRev.StartDate AND TaxRev.EndDate )</code>
/// </example>
public class Between<Operand1, Operand2> : IBqlComparison, IBqlCreator, IBqlVerifier
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
    object obj1;
    object obj2;
    if (!BqlFunction.getValue<Operand1>(ref this._operand1, cache, item, pars, ref result, out obj1) || !BqlFunction.getValue<Operand2>(ref this._operand2, cache, item, pars, ref result, out obj2))
      return;
    IComparable comparable = value as IComparable;
    result = comparable == null ? new bool?() : new bool?(comparable.CompareTo(obj1) >= 0 && comparable.CompareTo(obj2) <= 0);
  }

  /// <exclude />
  public virtual bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag = true;
    SQLExpression exp1 = (SQLExpression) null;
    if (!typeof (IBqlCreator).IsAssignableFrom(typeof (Operand1)))
    {
      if (info.BuildExpression)
        exp1 = BqlCommand.GetSingleExpression(typeof (Operand1), graph, info.Tables, selection, BqlCommand.FieldPlace.Condition);
    }
    else
    {
      if (this._operand1 == null)
        this._operand1 = this._operand1.createOperand<Operand1>();
      flag &= this._operand1.AppendExpression(ref exp1, graph, info, selection);
    }
    SQLExpression exp2 = (SQLExpression) null;
    if (!typeof (IBqlCreator).IsAssignableFrom(typeof (Operand2)))
    {
      if (info.BuildExpression)
        exp2 = BqlCommand.GetSingleExpression(typeof (Operand2), graph, info.Tables, selection, BqlCommand.FieldPlace.Condition);
    }
    else
    {
      if (this._operand2 == null)
        this._operand2 = this._operand2.createOperand<Operand2>();
      flag &= this._operand2.AppendExpression(ref exp2, graph, info, selection);
    }
    if (info.BuildExpression)
      exp = exp.Between(exp1, exp2);
    return flag;
  }
}
