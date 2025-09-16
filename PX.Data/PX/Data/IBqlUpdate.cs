// Decompiled with JetBrains decompiler
// Type: PX.Data.IBqlUpdate
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public interface IBqlUpdate
{
  string GetText(PXGraph graph);

  IBqlParameter[] GetParameters();

  System.Type GetBqlTable();

  System.Type GetSetTable(bool processCacheExtension, bool findAncestor);

  Query GetSelectQuery(PXGraph graph);

  PXDataFieldParam[] GetFieldAssignmentParameters(PXGraph graph, PXDataValue[] pars);

  IEnumerable<(Column setColumn, string valueColumnName)> GetDependentSetColumns(PXGraph graph);
}
