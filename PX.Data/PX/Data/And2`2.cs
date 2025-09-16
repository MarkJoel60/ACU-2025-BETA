// Decompiled with JetBrains decompiler
// Type: PX.Data.And2`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Appends a unary operator to a conditional expression via logical "and" and continues the chain of conditions.</summary>
/// <typeparam name="Operator">The unary conditional expression, <tt>Not</tt>,
/// <tt>Where</tt>, <tt>Where2</tt>, or <tt>Match</tt></typeparam>
/// <typeparam name="NextOperator">The next conditional expression, <tt>And</tt>,
/// <tt>And2</tt>, <tt>Or</tt>, or <tt>Or2</tt> class.</typeparam>
/// <example><para>The code below iterates over Tax data records selected by a read-only data view. The second code sample shows the SQL query corresponding to the data view.</para>
/// <code title="Example" lang="CS">
/// foreach (PXResult&lt;Tax, TaxRev&gt; record in PXSelectReadonly2&lt;Tax,
///     LeftJoin&lt;TaxRev, On&lt;TaxRev.taxID, Equal&lt;Tax.taxID&gt;,
///         And&lt;TaxRev.outdated, Equal&lt;False&gt;,
///         And2&lt;Where&lt;TaxRev.taxType, Equal&lt;TaxType.purchase&gt;, And&lt;Tax.reverseTax, Equal&lt;False&gt;,
///             Or&lt;TaxRev.taxType, Equal&lt;TaxType.sales&gt;&gt;&gt;&gt;&gt;,
///         And&lt;Current&lt;APRegister.docDate&gt;, Between&lt;TaxRev.startDate, TaxRev.endDate&gt;&gt;&gt;&gt;&gt;&gt;.Select(this))
/// {
///     ...
/// }</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM Tax
/// LEFT JOIN TaxRev ON ( TaxRev.TaxID = Tax.TaxID
///     AND TaxRev.Outdated = CONVERT(bit, 0)
///     AND ( TaxRev.TaxType = "P" AND Tax.ReverseTax = CONVERT(bit, 0) OR TaxRev.TaxType =  "S")
///     AND [current APRegister.DocDate value] BETWEEN TaxRev.StartDate AND TaxRev.EndDate )</code>
/// </example>
public sealed class And2<Operator, NextOperator> : BqlPredicateBinaryBase<Operator, NextOperator>
  where Operator : IBqlUnary, new()
  where NextOperator : IBqlBinary, new()
{
  protected override string SqlOperator => "AND";

  protected override bool BypassedValue => false;
}
