// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSelectReadonly3`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Selects records from one table without merging the result set with the <tt>PXCache&lt;Table&gt;</tt> object.</summary>
/// <typeparam name="Table">The DAC that represents the table from which
/// data records are retrieved.</typeparam>
/// <typeparam name="OrderBy">The ORDER BY clause.</typeparam>
public class PXSelectReadonly3<Table, OrderBy> : 
  PXSelectBase<Table, PXSelectReadonly3<Table, OrderBy>.Config>
  where Table : class, IBqlTable, new()
  where OrderBy : IBqlOrderBy, new()
{
  /// <exclude />
  public PXSelectReadonly3(PXGraph graph)
    : base(graph)
  {
  }

  /// <exclude />
  public PXSelectReadonly3(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  public class Config : 
    PXSelectBase<Table, PXSelectReadonly3<Table, OrderBy>.Config>.IViewConfig,
    IViewConfigBase
  {
    public BqlCommand GetCommand() => (BqlCommand) new Select3<Table, OrderBy>();

    public bool IsReadOnly => true;
  }
}
