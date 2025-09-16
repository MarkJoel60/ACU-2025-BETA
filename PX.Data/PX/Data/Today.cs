// Decompiled with JetBrains decompiler
// Type: PX.Data.Today
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data;

/// <summary>
/// Represents the current date (on the server) in BQL queries.
/// </summary>
/// <example>
/// <code>
/// public PXSelect&lt;EPActivity,
///     Where&lt;EPActivity.startDate, LessEqual&lt;Today&gt;,
///         And&lt;EPActivity.endDate, Greater&lt;Tomorrow&gt;&gt;&gt;&gt; records;
/// </code>
/// </example>
public sealed class Today : BqlType<IBqlDateTime, System.DateTime>.Constant<
#nullable disable
Today>
{
  /// <exclude />
  public Today()
    : base(System.DateTime.Today)
  {
  }
}
