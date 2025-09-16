// Decompiled with JetBrains decompiler
// Type: PX.Data.PXProcessingJoin`4
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Selects data records from multiple tables linked by the Join
/// clause, filtered according to the expression set in Where,
/// and ordered by the fields specified in OrderBy.
/// </summary>
/// <seealso cref="T:PX.Data.PXProcessing`1">PXProcessing&lt;Table&gt;</seealso>
/// <typeparam name="Table">The DAC that represents the table from which
/// data records are retrieved.</typeparam>
/// <typeparam name="Join">The JOIN clause.</typeparam>
/// <typeparam name="Where">The WHERE clause.</typeparam>
/// <typeparam name="OrderBy">The ORDER BY clause.</typeparam>
public class PXProcessingJoin<Table, Join, Where, OrderBy> : PXProcessingJoin<Table, Join, Where>
  where Table : class, IBqlTable, new()
  where Join : IBqlJoin, new()
  where Where : IBqlWhere, new()
  where OrderBy : IBqlOrderBy, new()
{
  protected override BqlCommand GetCommand()
  {
    return (BqlCommand) new Select2<Table, Join, Where, OrderBy>();
  }

  public PXProcessingJoin(PXGraph graph)
    : base(graph)
  {
  }

  public PXProcessingJoin(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }
}
