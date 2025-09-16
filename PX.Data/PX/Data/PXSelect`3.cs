// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSelect`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Selects records from one table, filters them by an expression set in <tt>Where</tt>, and orders by fields specified in <tt>OrderBy</tt>. The result set is
/// merged with the modified data records kept in the <tt>PXCache&lt;Table&gt;</tt> object.</summary>
/// <typeparam name="Table">The DAC that represents the table from which
/// data records are retrieved.</typeparam>
/// <typeparam name="Where">The WHERE clause.</typeparam>
/// <typeparam name="OrderBy">The ORDER BY clause.</typeparam>
public class PXSelect<Table, Where, OrderBy> : 
  PXSelectBase<Table, PXSelect<Table, Where, OrderBy>.Config>
  where Table : class, IBqlTable, new()
  where Where : IBqlWhere, new()
  where OrderBy : IBqlOrderBy, new()
{
  /// <exclude />
  public PXSelect(PXGraph graph)
    : base(graph)
  {
  }

  /// <exclude />
  public PXSelect(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  public class Config : 
    PXSelectBase<Table, PXSelect<Table, Where, OrderBy>.Config>.IViewConfig,
    IViewConfigBase
  {
    public BqlCommand GetCommand() => (BqlCommand) new PX.Data.Select<Table, Where, OrderBy>();

    public bool IsReadOnly => false;
  }
}
