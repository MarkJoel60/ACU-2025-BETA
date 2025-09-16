// Decompiled with JetBrains decompiler
// Type: PX.Data.CrossJoin`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.DbServices.QueryObjectModel;

#nullable disable
namespace PX.Data;

/// <summary>Joins tables by using the <tt>CROSS JOIN</tt> clause. No joining condition is specified.</summary>
/// <typeparam name="Table">A DAC class that represents a database table.</typeparam>
/// <example><para>Below is an example of a data view with CrossJoin and the corresponding SQL query.</para>
/// <code title="Example" lang="CS">
/// PXSelectJoin&lt;Table1, CrossJoin&lt;Table2&gt;&gt; records;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM Table1 CROSS JOIN Table2</code>
/// </example>
public sealed class CrossJoin<Table> : JoinBase<Table, BqlNone, BqlNone> where Table : IBqlTable
{
  public override YaqlJoinType getJoinType() => (YaqlJoinType) 3;
}
