// Decompiled with JetBrains decompiler
// Type: PX.Data.UserRecords.RecentlyVisitedRecords.IRecentlyVisitedRecordsService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Linq;

#nullable disable
namespace PX.Data.UserRecords.RecentlyVisitedRecords;

/// <summary>Interface for recently visit records service.</summary>
[PXInternalUseOnly]
public interface IRecentlyVisitedRecordsService
{
  /// <summary>Record visited document for the current user.</summary>
  /// <param name="screenGraph">The screen graph.</param>
  /// <param name="document">The document.</param>
  /// <returns>True if it succeeds, false if it fails.</returns>
  bool RecordVisitedDocumentForCurrentUser(PXGraph screenGraph, IBqlTable document);

  /// <summary>Gets the current user visited records.</summary>
  IQueryable<VisitedRecord> GetCurrentUserVisitedRecords();
}
