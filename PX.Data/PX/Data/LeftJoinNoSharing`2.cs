// Decompiled with JetBrains decompiler
// Type: PX.Data.LeftJoinNoSharing`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.DbServices.QueryObjectModel;

#nullable disable
namespace PX.Data;

/// <summary>Joins tables by using the LEFT JOIN clause. Prevents sharing of table records between tenants.</summary>
/// <typeparam name="Table">A DAC class that represents a database table.</typeparam>
/// <typeparam name="On">The joining condition.</typeparam>
/// <example><para>Below is an example of a data view with LeftJoinNoSharing and the corresponding SQL query.</para>
/// <code title="Example" lang="CS">
/// PXSelectJoin&lt;Table1,
///     LeftJoinNoSharing&lt;Table2, On&lt;Table2.field2, Equal&lt;Table1.field1&gt;&gt;&gt;&gt; records;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM Table1
/// LEFT JOIN Table2
///     ON Table2.Field2 = Table1.Field1</code>
/// </example>
public sealed class LeftJoinNoSharing<Table, On> : JoinBase<Table, On, BqlNone>
  where Table : IBqlTable
  where On : class, IBqlOn, new()
{
  public override YaqlJoinType getJoinType() => (YaqlJoinType) 1;

  protected override bool PreventRowSharing { get; } = true;
}
