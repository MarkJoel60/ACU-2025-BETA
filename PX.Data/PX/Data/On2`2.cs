// Decompiled with JetBrains decompiler
// Type: PX.Data.On2`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Specifies the joining condition for a JOIN clause and allows
/// continuing the chain of conditions using a logical operator.
/// Corresponds to SQL keyword ON.
/// </summary>
/// <typeparam name="Operator">The conditional expression, the <tt>Not</tt>,
/// <tt>Where</tt>, or <tt>Where2</tt> class.</typeparam>
/// <typeparam name="NextOperator">The next conditional expression.</typeparam>
/// <example><para>The code below selects ARRegister data records for the current customer and joins ARAdjust data records. The second code sample shows the corresponding SQL query.</para>
/// <code title="Example" lang="CS">
/// PXResultset&lt;ARRegister&gt; docs = PXSelectJoin&lt;ARRegister,
///     InnerJoin&lt;ARAdjust, On2&lt;
///         Where&lt;ARAdjust.adjgDocType, Equal&lt;ARRegister.docType&gt;,
///             Or&lt;ARAdjust.adjgDocType, Equal&lt;ARDocType.payment&gt;, And&lt;ARRegister.docType, Equal&lt;ARDocType.voidPayment&gt;,
///             Or&lt;ARAdjust.adjgDocType, Equal&lt;ARDocType.prepayment&gt;, And&lt;ARRegister.docType, Equal&lt;ARDocType.voidPayment&gt;&gt;&gt;&gt;&gt;&gt;,
///         And&lt;ARAdjust.adjgRefNbr, Equal&lt;ARRegister.refNbr&gt;&gt;&gt;&gt;,
///     Where&lt;ARRegister.customerID, Equal&lt;Current&lt;Customer.bAccountID&gt;&gt;&gt;&gt;.Select(this);</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM ARRegister
/// INNER JOIN ARAdjust ON
///   ( ( ARAdjust.AdjgDocType = ARRegister.DocType
///         OR ARAdjust.AdjgDocType = "PMT" AND ARRegister.DocType = "RPM"
///         OR ARAdjust.AdjgDocType = "PPM" AND ARRegister.DocType = "RPM" )
///     AND ARAdjust.AdjgRefNbr = ARRegister.RefNbr )
/// WHERE ARRegister.CustomerID = [current Customer.BAccountID value]</code>
/// </example>
public sealed class On2<Operator, NextOperator> : OnBase
  where Operator : IBqlUnary, new()
  where NextOperator : IBqlBinary, new()
{
  private IBqlCreator _operator;
  private IBqlCreator _next;

  /// <exclude />
  public override void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (this._operator == null)
      this._operator = (IBqlCreator) new Operator();
    this._operator.Verify(cache, item, pars, ref result, ref value);
    if (this._next == null)
      this._next = (IBqlCreator) new NextOperator();
    this._next.Verify(cache, item, pars, ref result, ref value);
  }

  /// <exclude />
  public override IBqlUnary GetMatchingWhere() => (IBqlUnary) new Where2<Operator, NextOperator>();

  public override bool AppendJoinExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (this._operator == null)
      this._operator = (IBqlCreator) new Operator();
    int num1 = 1 & (this._operator.AppendExpression(ref exp, graph, info, selection) ? 1 : 0);
    if (this._next == null)
      this._next = (IBqlCreator) new NextOperator();
    if (exp == null && info.BuildExpression)
      exp = SQLExpression.None();
    int num2 = this._next.AppendExpression(ref exp, graph, info, selection) ? 1 : 0;
    return (num1 & num2) != 0;
  }
}
