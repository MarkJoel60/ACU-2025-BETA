// Decompiled with JetBrains decompiler
// Type: PX.Data.InnerJoinStraight`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.DbServices.QueryObjectModel;

#nullable disable
namespace PX.Data;

/// <summary>
/// Joins tables by using the <tt>INNER JOIN</tt> clause and allows joining one or several more tables.
/// If possible, prevents the DBMS optimizer from reordering joined tables, which can impact performance in both directions, so use it wisely.
/// </summary>
/// <typeparam name="Table">A DAC class that represents a database table.</typeparam>
/// <typeparam name="On">The joining condition.</typeparam>
/// <typeparam name="NextJoin">The next JOIN clause.</typeparam>
/// <example><para>Below is an example of a data view with InnerJoinStraight and the corresponding SQL query.</para>
/// <code title="Example" lang="CS">
/// PXSelectJoin&lt;Table1,
///     InnerJoinStraight&lt;Table2, On&lt;Table2.field2, Equal&lt;Table1.field1&gt;&gt;,
///     LeftJoin&lt;Table3, On&lt;Table3.field3, Equal&lt;Table1.field4&gt;&gt;&gt;&gt;&gt; records;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM Table1
/// INNER LOOP JOIN Table2 -- on MSSQL DBMS
/// -- STRAIGHT_JOIN Table2 -- on MySQL DBMS
///     ON Table2.Field2 = Table1.Field1
/// LEFT JOIN Table3
///     ON Table3.Field3 = Table1.Field4</code>
/// </example>
[PXInternalUseOnly]
public sealed class InnerJoinStraight<Table, On, NextJoin> : 
  JoinBase<Table, On, NextJoin>,
  IBqlJoinHints
  where Table : IBqlTable
  where On : class, IBqlOn, new()
  where NextJoin : class, IBqlJoin, new()
{
  public override YaqlJoinType getJoinType() => (YaqlJoinType) 0;

  bool IBqlJoinHints.TryKeepTablesOrder => true;

  bool IBqlJoinHints.TryJoinOnlyHeaderOfProjection => false;
}
