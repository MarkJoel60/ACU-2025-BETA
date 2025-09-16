// Decompiled with JetBrains decompiler
// Type: PX.Data.True
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// The true value for comparing with boolean fields. In translation
/// to SQL corresponds to CONVERT(BIT, 1).
/// </summary>
/// <example><para>The code below iterates over the APInvoice data records retrieved by using the static Select(...) method. The SQL query corresponding to the BQL query is given below.</para>
/// <code title="Example" lang="CS">
/// foreach (APInvoice rec in PXSelect&lt;APInvoice,
///     Where&lt;APInvoice.openDoc, Equal&lt;True&gt;,
///         And2&lt;Where&lt;APInvoice.released, Equal&lt;True&gt;,
///             Or&lt;APInvoice.prebooked,Equal&lt;True&gt;&gt;&gt;&gt;&gt;.Select(this))
/// {
/// ...
/// }</code>
/// <code title="" description="" lang="SQL">
/// SELECT * FROM APInvoice
/// WHERE APInvoice.OpenDoc = CONVERT(BIT, 1) AND
///     ( APInvoice.Released = CONVERT(BIT, 1) OR APInvoice.Prebooked = CONVERT(BIT, 1) )</code>
/// </example>
public sealed class True : BoolConstant<True>
{
  /// <exclude />
  public True()
    : base(true)
  {
  }

  /// <exclude />
  protected override Lazy<IBqlUnary> UnaryLazyImpl { get; } = new Lazy<IBqlUnary>((Func<IBqlUnary>) (() => (IBqlUnary) new WhereNp<True, Equal<True>>()));
}
