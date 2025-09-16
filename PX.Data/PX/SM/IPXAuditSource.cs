// Decompiled with JetBrains decompiler
// Type: PX.SM.IPXAuditSource
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

/// <summary>
/// Provides an ability to customize which tables should be audited for a specific graph.
/// </summary>
/// <example>
/// public class APVendorPriceMaint : PXGraph&lt;APVendorPriceMaint&gt;, IPXAuditSource
/// </example>
public interface IPXAuditSource
{
  /// <summary>
  /// Returns the name of the main data view in the current graph.
  /// </summary>
  /// <remarks>
  /// In a typical case, if there is a PXFilter-typed view in the graph, it will be the primary view.
  /// But for audit purposes we need the data view instead of such "primary" view
  /// (e.g. when we need to share audit between multiple screens)
  /// </remarks>
  /// <returns>The name of the view.</returns>
  string GetMainView();

  /// <summary>Returns the list of tables that should be audited.</summary>
  IEnumerable<Type> GetAuditedTables();
}
