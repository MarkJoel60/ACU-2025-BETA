// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFilteredProcessing`4
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Selects data records from one table filtered by the expression
/// set in Where and ordered by the fields specified in OrderBy and
/// applies the user filter.
/// </summary>
/// <seealso cref="T:PX.Data.PXProcessing`1">PXProcessing&lt;Table&gt;</seealso>
/// <typeparam name="Table">The DAC that represents the table from which
/// data records are retrieved.</typeparam>
/// <typeparam name="FilterTable">The DAC of the filter for the data
/// records.</typeparam>
/// <typeparam name="Where">The WHERE clause.</typeparam>
/// <typeparam name="OrderBy">The ORDER BY clause.</typeparam>
public class PXFilteredProcessing<Table, FilterTable, Where, OrderBy> : 
  PXFilteredProcessing<Table, FilterTable, Where>
  where Table : class, IBqlTable, new()
  where FilterTable : class, IBqlTable, new()
  where Where : IBqlWhere, new()
  where OrderBy : IBqlOrderBy, new()
{
  protected override BqlCommand GetCommand() => (BqlCommand) new PX.Data.Select<Table, Where, OrderBy>();

  public PXFilteredProcessing(PXGraph graph)
    : base(graph)
  {
  }

  public PXFilteredProcessing(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }
}
