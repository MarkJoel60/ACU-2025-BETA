// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSelectOrderBy`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Selects records from one table and sorts them by fields specified in <tt>OrderBy</tt>. The result set is merged with the modified data records kept in the
/// <tt>PXCache&lt;Table&gt;</tt> object.</summary>
/// <typeparam name="Table">The DAC that represents the table from which
/// data records are retrieved.</typeparam>
/// <typeparam name="OrderBy">The ORDER BY clause.</typeparam>
public class PXSelectOrderBy<Table, OrderBy> : 
  PXSelectBase<Table, PXSelectOrderBy<Table, OrderBy>.Config>
  where Table : class, IBqlTable, new()
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

  public class Config : 
    PXSelectBase<Table, PXSelectOrderBy<Table, OrderBy>.Config>.IViewConfig,
    IViewConfigBase
  {
    public BqlCommand GetCommand() => (BqlCommand) new Select3<Table, OrderBy>();

    public bool IsReadOnly => false;
  }
}
