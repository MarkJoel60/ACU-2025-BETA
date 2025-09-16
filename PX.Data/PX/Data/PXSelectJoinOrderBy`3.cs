// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSelectJoinOrderBy`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Defines a data view for retrieving a particular data set from
/// the database and provides the interface to the cache for inserting,
/// updating, and deleting the data records.</summary>
/// <typeparam name="Table">The DAC that represents the table from which
/// data records are retrieved.</typeparam>
/// <typeparam name="Join">Joined DACs to select from.</typeparam>
/// <typeparam name="OrderBy">The ORDER BY clause.</typeparam>
public class PXSelectJoinOrderBy<Table, Join, OrderBy> : 
  PXSelectBase<Table, PXSelectJoinOrderBy<Table, Join, OrderBy>.Config>
  where Table : class, IBqlTable, new()
  where Join : IBqlJoin, new()
  where OrderBy : IBqlOrderBy, new()
{
  /// <exclude />
  public PXSelectJoinOrderBy(PXGraph graph)
    : base(graph)
  {
  }

  /// <exclude />
  public PXSelectJoinOrderBy(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  public class Config : 
    PXSelectBase<Table, PXSelectJoinOrderBy<Table, Join, OrderBy>.Config>.IViewConfig,
    IViewConfigBase
  {
    public BqlCommand GetCommand() => (BqlCommand) new Select3<Table, Join, OrderBy>();

    public bool IsReadOnly => false;
  }
}
