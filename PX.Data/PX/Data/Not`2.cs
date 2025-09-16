// Decompiled with JetBrains decompiler
// Type: PX.Data.Not`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Adds logical "not" to a single condition in a BQL statement.
/// </summary>
/// <typeparam name="Operand">The compared operand.</typeparam>
/// <typeparam name="Comparison">The comparison operator.</typeparam>
/// <remarks>
/// This <tt>Not</tt> class can be used to negate a single comparison.
/// In many cases, you can avoid using this class as most of the comparison
/// operators have their negative counterparts
/// (for example, <tt>Equal</tt> and <tt>NotEqual</tt>).</remarks>
/// <example>
/// The code below shows the defition of a data view and the
/// corresponding SQL query.
/// <code>
/// public PXSelect&lt;Table1,
///     Where&lt;Table1.field1, IsNotNull,
///         And&lt;Not&lt;Table1.field2, Equal&lt;Zero&gt;&gt;&gt;&gt;&gt; records;
/// </code>
/// <code lang="SQL">
/// SELECT * FROM Table1
/// WHERE ( Table1.Field1 IS NOT NULL
///     AND NOT (Table1.Field2 = 0) )
/// </code>
/// </example>
public sealed class Not<Operand, Comparison> : IBqlUnary, IBqlCreator, IBqlVerifier
  where Operand : IBqlOperand
  where Comparison : IBqlComparison, new()
{
  private IBqlCreator _operand;
  private IBqlComparison _comparison;

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    int num = !BqlFunction.getValue<Operand>(ref this._operand, cache, item, pars, ref result, out value) ? 1 : 0;
    if (this._comparison == null)
      this._comparison = (IBqlComparison) new Comparison();
    this._comparison.Verify(cache, item, pars, ref result, ref value);
    if (num != 0)
    {
      result = new bool?();
      value = (object) null;
    }
    else
    {
      if (!result.HasValue)
        return;
      ref bool? local = ref result;
      bool? nullable1 = result;
      bool? nullable2 = nullable1.HasValue ? new bool?(!nullable1.GetValueOrDefault()) : new bool?();
      local = nullable2;
    }
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag1 = true;
    if (info.Fields is BqlCommand.EqualityList fields)
      fields.NonStrict = true;
    SQLExpression exp1 = (SQLExpression) null;
    if (!typeof (IBqlCreator).IsAssignableFrom(typeof (Operand)))
    {
      if (info.BuildExpression)
        exp1 = BqlCommand.GetSingleExpression(typeof (Operand), graph, info.Tables, selection, BqlCommand.FieldPlace.Condition);
      info.Fields?.Add(typeof (Operand));
    }
    else
    {
      if (this._operand == null)
        this._operand = this._operand.createOperand<Operand>();
      flag1 &= this._operand.AppendExpression(ref exp1, graph, info, selection);
    }
    if (info.BuildExpression)
      exp = exp1 ?? SQLExpression.None();
    if (this._comparison == null)
      this._comparison = (IBqlComparison) new Comparison();
    bool flag2 = flag1 & this._comparison.AppendExpression(ref exp, graph, info, selection);
    if (info.BuildExpression)
      exp = SQLExpression.Not(exp);
    return flag2;
  }
}
