// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFilteredProcessingJoinOrderBy`4
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Selects data records from multiple tables linked by the Join
/// clause and ordered by the fields specified in OrderBy and applies the user filter.
/// </summary>
/// <seealso cref="T:PX.Data.PXProcessing`1">PXProcessing&lt;Table&gt;</seealso>
/// <typeparam name="Table">The DAC that represents the table from which
/// data records are retrieved.</typeparam>
/// <typeparam name="FilterTable">The DAC of the filter for the data
/// records.</typeparam>
/// <typeparam name="Join">The JOIN clause.</typeparam>
/// <typeparam name="OrderBy">The ORDER BY clause.</typeparam>
public class PXFilteredProcessingJoinOrderBy<Table, FilterTable, Join, OrderBy> : 
  PXFilteredProcessingJoin<Table, FilterTable, Join>
  where Table : class, IBqlTable, new()
  where FilterTable : class, IBqlTable, new()
  where Join : IBqlJoin, new()
  where OrderBy : IBqlOrderBy, new()
{
  protected override BqlCommand GetCommand() => (BqlCommand) new Select3<Table, Join, OrderBy>();

  public PXFilteredProcessingJoinOrderBy(PXGraph graph)
    : base(graph)
  {
  }

  public PXFilteredProcessingJoinOrderBy(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }
}
