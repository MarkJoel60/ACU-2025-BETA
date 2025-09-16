// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSelectGroupBy`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Selects records from the one table, grouping and applying aggregations. The result set is not merged with the <tt>PXCache&lt;Table&gt;</tt> object.</summary>
/// <typeparam name="Table">The DAC that represents the table from which
/// data records are retrieved.</typeparam>
/// <typeparam name="Aggregate">
/// 	<tt>Aggregate</tt> clause wrapping <tt>Sum</tt>,
/// <tt>Avg</tt>, <tt>Min</tt>, <tt>Max</tt> or <tt>GroupBy</tt> functions.</typeparam>
public class PXSelectGroupBy<Table, Aggregate> : 
  PXSelectBase<Table, PXSelectGroupBy<Table, Aggregate>.Config>
  where Table : class, IBqlTable, new()
  where Aggregate : IBqlAggregate, new()
{
  /// <exclude />
  public PXSelectGroupBy(PXGraph graph)
    : base(graph)
  {
  }

  /// <exclude />
  public PXSelectGroupBy(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  public class Config : 
    PXSelectBase<Table, PXSelectGroupBy<Table, Aggregate>.Config>.IViewConfig,
    IViewConfigBase
  {
    public BqlCommand GetCommand() => (BqlCommand) new Select4<Table, Aggregate>();

    public bool IsReadOnly => true;
  }
}
