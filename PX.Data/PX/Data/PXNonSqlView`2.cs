// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNonSqlView`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

public class PXNonSqlView<TTable, OrderBy> : PXNonSqlView<TTable>
  where TTable : class, IBqlTable, new()
  where OrderBy : IBqlOrderBy, new()
{
  public PXNonSqlView(PXGraph graph)
    : base(graph)
  {
    this.View = new PXView(graph, false, PXSelectBase<TTable, PXSelect<TTable>.Config>.GetCommand().OrderByNew(typeof (OrderBy)), (Delegate) new PXSelectDelegate(((PXNonSqlView<TTable>) this).Getter))
    {
      CacheType = typeof (TTable)
    };
  }
}
