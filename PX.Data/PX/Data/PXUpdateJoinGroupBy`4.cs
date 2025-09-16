// Decompiled with JetBrains decompiler
// Type: PX.Data.PXUpdateJoinGroupBy`4
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public static class PXUpdateJoinGroupBy<Set, Table, Join, Aggregate>
  where Set : IBqlSet
  where Table : IBqlTable
  where Join : IBqlJoin, new()
  where Aggregate : IBqlAggregate, new()
{
  public static int Update(PXGraph graph, params object[] parameters)
  {
    IBqlUpdate command = (IBqlUpdate) new PX.Data.Update<Set, Select5<Table, Join, Aggregate>>();
    return PXDatabase.Update(graph, command, PXUpdate.prepareParameters(graph, command, parameters));
  }
}
