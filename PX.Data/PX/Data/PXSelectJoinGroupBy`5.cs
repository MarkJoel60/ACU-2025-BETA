// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSelectJoinGroupBy`5
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Selects records from multiple tables, grouping and applying aggregations. The result set is not merged with the <tt>PXCache&lt;Table&gt;</tt> object.</summary>
/// <typeparam name="Table">The DAC that represents the table from which
/// data records are retrieved.</typeparam>
/// <typeparam name="Join">Joined DACs to select from.</typeparam>
/// <typeparam name="Where">The WHERE clause.</typeparam>
/// <typeparam name="Aggregate"><tt>Aggregate</tt> clause wrapping <tt>Sum</tt>,
/// <tt>Avg</tt>, <tt>Min</tt>, <tt>Max</tt> or <tt>GroupBy</tt> functions.</typeparam>
/// <typeparam name="OrderBy">The ORDER BY clause.</typeparam>
public class PXSelectJoinGroupBy<Table, Join, Where, Aggregate, OrderBy> : 
  PXSelectBase<Table, PXSelectJoinGroupBy<Table, Join, Where, Aggregate, OrderBy>.Config>
  where Table : class, IBqlTable, new()
  where Join : IBqlJoin, new()
  where Where : IBqlWhere, new()
  where Aggregate : IBqlAggregate, new()
  where OrderBy : IBqlOrderBy, new()
{
  /// <exclude />
  public PXSelectJoinGroupBy(PXGraph graph)
    : base(graph)
  {
  }

  /// <exclude />
  public PXSelectJoinGroupBy(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  public class Config : 
    PXSelectBase<Table, PXSelectJoinGroupBy<Table, Join, Where, Aggregate, OrderBy>.Config>.IViewConfig,
    IViewConfigBase
  {
    public BqlCommand GetCommand()
    {
      return (BqlCommand) new Select5<Table, Join, Where, Aggregate, OrderBy>();
    }

    public bool IsReadOnly => true;
  }
}
