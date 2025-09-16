// Decompiled with JetBrains decompiler
// Type: PX.Data.Asc`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Indication of sorting in ascending order: from the least value to the largest value. The field to order by is specified in the Field type parameter. The clause itself is used as a type parameter in OrderBy.
/// </summary>
/// <typeparam name="Field">The field to sort by.</typeparam>
/// <example><para>An example of a data view with Asc with one type parameter and the corresponding SQL query are given below.</para>
/// <code title="Example" lang="CS">
/// PXSelect&lt;Table1,
///     Where&lt;Table1.field1, IsNotNull&gt;,
///     OrderBy&lt;Asc&lt;Table1.field1&gt;&gt;&gt; records;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM Table1
/// WHERE Table1.Field1 IS NOT NULL
/// ORDER BY Table1.Field1</code>
/// </example>
public sealed class Asc<Field> : AscDescBase<Field, BqlNone> where Field : IBqlOperand
{
  /// <exclude />
  public override bool IsDescending => false;
}
