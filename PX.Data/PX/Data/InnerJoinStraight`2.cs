// Decompiled with JetBrains decompiler
// Type: PX.Data.InnerJoinStraight`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.DbServices.QueryObjectModel;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public sealed class InnerJoinStraight<Table, On> : JoinBase<Table, On, BqlNone>, IBqlJoinHints
  where Table : IBqlTable
  where On : class, IBqlOn, new()
{
  public override YaqlJoinType getJoinType() => (YaqlJoinType) 0;

  bool IBqlJoinHints.TryKeepTablesOrder => true;

  bool IBqlJoinHints.TryJoinOnlyHeaderOfProjection => false;
}
