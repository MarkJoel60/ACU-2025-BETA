// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSelectOrderBy`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

[Obsolete("Use PXSelectJoinOrderBy<Table, Join, OrderBy> instead")]
public class PXSelectOrderBy<Table, Join, OrderBy> : PXSelectJoinOrderBy<Table, Join, OrderBy>
  where Table : class, IBqlTable, new()
  where Join : IBqlJoin, new()
  where OrderBy : IBqlOrderBy, new()
{
  /// <exclude />
  public PXSelectOrderBy(PXGraph graph)
    : base(graph)
  {
  }

  /// <exclude />
  public PXSelectOrderBy(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }
}
