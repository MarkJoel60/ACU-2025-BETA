// Decompiled with JetBrains decompiler
// Type: PX.Data.PXUpdateJoin`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public static class PXUpdateJoin<Set, Table, Join>
  where Set : IBqlSet
  where Table : IBqlTable
  where Join : IBqlJoin, new()
{
  public static int Update(PXGraph graph, params object[] parameters)
  {
    IBqlUpdate command = (IBqlUpdate) new PX.Data.Update<Set, Select2<Table, Join>>();
    return PXDatabase.Update(graph, command, PXUpdate.prepareParameters(graph, command, parameters));
  }
}
