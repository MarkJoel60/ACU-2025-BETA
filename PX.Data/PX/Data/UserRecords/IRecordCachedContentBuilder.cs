// Decompiled with JetBrains decompiler
// Type: PX.Data.UserRecords.IRecordCachedContentBuilder
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.UserRecords;

/// <summary>
/// Interface for the builder of user record's cached content.
/// </summary>
[PXInternalUseOnly]
public interface IRecordCachedContentBuilder
{
  /// <summary>Builds cached content.</summary>
  /// <param name="screenGraph">The screen graph.</param>
  /// <param name="entity">The entity.</param>
  /// <returns>A string with the cached entity content.</returns>
  string BuildCachedContent(PXGraph screenGraph, IBqlTable entity);
}
