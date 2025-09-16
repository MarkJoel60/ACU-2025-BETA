// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.FromMappedTableBase`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;

#nullable disable
namespace PX.Data.BQL;

public abstract class FromMappedTableBase<TBqlTableMap, NextUnion> : 
  UnionBase<TBqlTableMap, NextUnion>,
  IMappedTable
  where TBqlTableMap : IBqlTableMapper, new()
  where NextUnion : IBqlUnion, new()
{
  private NextUnion _union;

  protected IBqlUnion ensureUnion()
  {
    if ((object) this._union != null)
      return (IBqlUnion) this._union;
    return !(typeof (NextUnion) == typeof (BqlNone)) ? (IBqlUnion) (this._union = new NextUnion()) : (IBqlUnion) null;
  }

  public Query GetQuery(
    Query query,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    Query query1 = this.PrepareQuery(graph, query.ShowArchivedRecords, info, selection);
    if (this.ensureUnion() != null)
      this._union.AppendQuery(query1, graph, info, selection);
    return query1;
  }
}
