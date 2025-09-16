// Decompiled with JetBrains decompiler
// Type: PX.Data.IBqlCreator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;

#nullable disable
namespace PX.Data;

/// <summary>A BQL element creator.</summary>
public interface IBqlCreator : IBqlVerifier
{
  /// <summary>Appends the SQL tree expression that corresponds to the BQL command to an SQL tree query.</summary>
  /// <param name="exp">The SQL tree expression to be appended.</param>
  /// <param name="graph">A graph instance.</param>
  /// <param name="info">The information about the BQL command.</param>
  /// <param name="selection">The fragment of the BQL command that is translated to an SQL tree expression.</param>
  bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection);
}
