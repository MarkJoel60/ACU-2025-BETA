// Decompiled with JetBrains decompiler
// Type: PX.Data.Desc`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Indication of sorting in descending order: from the largest value down to the least value. The field to order by is specified in the Field type parameter. The clause itself is used as a type parameter in OrderBy.
/// </summary>
/// <typeparam name="Field">The field to sort by.</typeparam>
/// <example><para>An example of a data view with Desc with one type parameter and the corresponding SQL query are given below.</para>
/// <code title="Example" lang="CS">
/// PXSelect&lt;Table1,
///     Where&lt;Table1.field2, Equal&lt;Table1.field1&gt;&gt;,
///     OrderBy&lt;Desc&lt;Table1.field1&gt;&gt;&gt; records;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM Table1
/// WHERE Table1.Field2 = Table1.Field1
/// ORDER BY Table1.Field1 DESC</code>
/// </example>
public sealed class Desc<Field> : AscDescBase<Field, BqlNone> where Field : IBqlOperand
{
  /// <exclude />
  public override bool IsDescending => true;
}
