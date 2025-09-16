// Decompiled with JetBrains decompiler
// Type: PX.Data.Asc`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// A variant of the <tt>Asc</tt> clause used to add additional sort expression.
/// </summary>
/// <typeparam name="Field">The field to sort by.</typeparam>
/// <typeparam name="NextField">The next field to sort by.</typeparam>
/// <example><para>An example of a data view with Asc with two type parameters and the corresponding SQL query are given below.</para>
/// <code title="Example" lang="CS">
/// PXSelect&lt;Table1,
///     Where&lt;Table1.field1, IsNotNull&gt;,
///     OrderBy&lt;Asc&lt;Table1.field1,
///             Desc&lt;Table1.field2&gt;&gt;&gt;&gt; records;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM Table1
/// WHERE Table1.Field1 IS NOT NULL
/// ORDER BY Table1.Field1, Table1.Field2 DESC</code>
/// </example>
public sealed class Asc<Field, NextField> : AscDescBase<Field, NextField>
  where Field : IBqlOperand
  where NextField : IBqlSortColumn, new()
{
  /// <exclude />
  public override bool IsDescending => false;
}
