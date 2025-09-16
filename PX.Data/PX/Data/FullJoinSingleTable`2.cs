// Decompiled with JetBrains decompiler
// Type: PX.Data.FullJoinSingleTable`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.DbServices.QueryObjectModel;

#nullable disable
namespace PX.Data;

/// <summary>Joins tables by using the <tt>FULL JOIN</tt> clause. Joins first table only.</summary>
/// <typeparam name="Table">A DAC class that represents a database table.</typeparam>
/// <typeparam name="On">The joining condition.</typeparam>
public sealed class FullJoinSingleTable<Table, On> : JoinBase<Table, On, BqlNone>, IBqlJoinHints
  where Table : IBqlTable
  where On : class, IBqlOn, new()
{
  public override YaqlJoinType getJoinType() => (YaqlJoinType) 4;

  bool IBqlJoinHints.TryKeepTablesOrder => false;

  bool IBqlJoinHints.TryJoinOnlyHeaderOfProjection => true;
}
