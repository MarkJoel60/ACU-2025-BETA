// Decompiled with JetBrains decompiler
// Type: PX.SM.PXSelectRedirect`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;

#nullable disable
namespace PX.SM;

public class PXSelectRedirect<Table, PView> : 
  PXSelectRedirect<Table, Where<PX.Data.True, Equal<PX.Data.True>>, PView>
  where Table : class, IBqlTable, new()
  where PView : class, IBqlTable, new()
{
  public PXSelectRedirect(PXGraph graph)
    : base(graph)
  {
  }

  public PXSelectRedirect(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }
}
