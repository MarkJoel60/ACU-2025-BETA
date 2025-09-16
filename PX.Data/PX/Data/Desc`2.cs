// Decompiled with JetBrains decompiler
// Type: PX.Data.Desc`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// A variant of the <tt>Desc</tt> clause used to add additional sort expression.
/// </summary>
/// <typeparam name="Field">The field to sort by.</typeparam>
/// <typeparam name="NextField">The next field to sort by.</typeparam>
/// <example><para>An example of a data view with Desc with two type parameters and the corresponding SQL query are given below.</para>
/// <code title="Example" lang="CS">
/// PXSelect&lt;Table1,
///     Where&lt;Table1.field1, IsNotNull&gt;,
///     OrderBy&lt;Desc&lt;Table1.field1,
///             Asc&lt;Table1.field2&gt;&gt;&gt;&gt; records;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM Table1
/// WHERE Table1.Field1 IS NOT NULL
/// ORDER BY Table1.Field1 DESC, Tabl1e.Field2</code>
/// </example>
public sealed class Desc<Field, NextField> : AscDescBase<Field, NextField>
  where Field : IBqlOperand
  where NextField : IBqlSortColumn, new()
{
  /// <exclude />
  public override bool IsDescending => true;
}
