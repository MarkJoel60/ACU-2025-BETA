// Decompiled with JetBrains decompiler
// Type: PX.Data.PXVirtualDACAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Prevents the data view from selecting data records from the
/// database.</summary>
/// <remarks>The attribute can be placed on data views defined in a graph.
/// The data view will not try to select data records from the database.
/// You should define the optional method for this data view to form the
/// resultset which the data view will return.</remarks>
/// <example>
/// <code>
/// [PXVirtualDAC]
/// public PXSelect&lt;PMProjectBalanceRecord,
///     Where&lt;PMProjectBalanceRecord.recordID, IsNotNull&gt;,
///     OrderBy&lt;Asc&lt;PMProjectBalanceRecord.sortOrder&gt;&gt;&gt; BalanceRecords;
/// </code>
/// </example>
public sealed class PXVirtualDACAttribute : PXCacheExtensionAttribute
{
  protected override void AddHandlers(PXCache cache) => cache.DisableReadItem = true;
}
