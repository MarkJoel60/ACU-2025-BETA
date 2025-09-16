// Decompiled with JetBrains decompiler
// Type: PX.Data.PXUpdate`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public static class PXUpdate<Set, Table>
  where Set : IBqlSet
  where Table : IBqlTable
{
  public static int Update(PXGraph graph, params object[] parameters)
  {
    IBqlUpdate command = (IBqlUpdate) new PX.Data.Update<Set, Select<Table>>();
    return PXDatabase.Update(graph, command, PXUpdate.prepareParameters(graph, command, parameters));
  }
}
