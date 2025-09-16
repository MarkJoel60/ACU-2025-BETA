// Decompiled with JetBrains decompiler
// Type: PX.Data.IBqlSet
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>Marks BQL assignment commands in an update statement.</summary>
public interface IBqlSet
{
  /// <summary>
  /// Fills the list passed with target-source SQLExpression pairs
  /// </summary>
  /// <param name="graph">A PXGraph instance.</param>
  /// <param name="tables">The list of tables used in the select statement.</param>
  /// <param name="pars">The list of parameters used in the statement.</param>
  /// <param name="assigments">List of target-source pairs</param>
  /// <param name="assignmets">The list of target-source pairs.</param>
  bool AppendExpression(
    PXGraph graph,
    BqlCommandInfo info,
    List<KeyValuePair<SQLExpression, SQLExpression>> assignments);

  System.Type GetFieldType();
}
