// Decompiled with JetBrains decompiler
// Type: PX.Data.CrossJoinSingleTable`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.DbServices.QueryObjectModel;

#nullable disable
namespace PX.Data;

/// <summary>Joins tables by using the <tt>CROSS JOIN</tt> clause and allows joining one or several more tables. Joins first table only.</summary>
/// <typeparam name="Table">A DAC class that represents a database table.</typeparam>
/// <typeparam name="NextJoin">The next JOIN clause.</typeparam>
public sealed class CrossJoinSingleTable<Table, NextJoin> : 
  JoinBase<Table, BqlNone, NextJoin>,
  IBqlJoinHints
  where Table : IBqlTable
  where NextJoin : class, IBqlJoin, new()
{
  public override YaqlJoinType getJoinType() => (YaqlJoinType) 3;

  bool IBqlJoinHints.TryKeepTablesOrder => false;

  bool IBqlJoinHints.TryJoinOnlyHeaderOfProjection => true;
}
