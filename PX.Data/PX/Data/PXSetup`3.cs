// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSetup`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXSetup<Table, Join, Where> : PXSetup<Table, Where>
  where Table : class, IBqlTable, new()
  where Join : IBqlJoin, new()
  where Where : IBqlWhere, new()
{
  public PXSetup(PXGraph graph)
    : base(graph)
  {
    this.View = new PXView(graph, true, (BqlCommand) new Select2<Table, Join, Where>());
  }
}
