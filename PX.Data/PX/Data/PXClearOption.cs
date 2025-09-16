// Decompiled with JetBrains decompiler
// Type: PX.Data.PXClearOption
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Defines possible options of clearing the graph data through
/// the <see cref="M:PX.Data.PXGraph.Clear(PX.Data.PXClearOption)">Clear(PXClearOption)</see>
/// method.</summary>
public enum PXClearOption
{
  /// <summary>Data records are preserved.</summary>
  PreserveData,
  /// <summary>The timestamp is preserved.</summary>
  PreserveTimeStamp,
  /// <summary>The query cache is preserved.</summary>
  PreserveQueries,
  /// <summary>Everything is removed.</summary>
  ClearAll,
  /// <summary>Only the query cache is cleared.</summary>
  ClearQueriesOnly,
}
