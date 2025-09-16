// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSelectJoin`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Selects records from multiple tables linked via the <tt>Join</tt> clause and filters the result set according to expression set in Where. The resulting data
/// records from the main table are merged with the modified data records from the <tt>PXCache&lt;Table&gt;</tt> object.</summary>
/// <typeparam name="Table">The DAC that represents the table from which
/// data records are retrieved.</typeparam>
/// <typeparam name="Join">Joined DACs to select from.</typeparam>
/// <typeparam name="Where">The WHERE clause.</typeparam>
public class PXSelectJoin<Table, Join, Where> : 
  PXSelectBase<Table, PXSelectJoin<Table, Join, Where>.Config>
  where Table : class, IBqlTable, new()
  where Join : IBqlJoin, new()
  where Where : IBqlWhere, new()
{
  /// <exclude />
  public PXSelectJoin(PXGraph graph)
    : base(graph)
  {
  }

  /// <exclude />
  public PXSelectJoin(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  public class Config : 
    PXSelectBase<Table, PXSelectJoin<Table, Join, Where>.Config>.IViewConfig,
    IViewConfigBase
  {
    public BqlCommand GetCommand() => (BqlCommand) new Select2<Table, Join, Where>();

    public bool IsReadOnly => false;
  }
}
