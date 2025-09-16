// Decompiled with JetBrains decompiler
// Type: PX.Data.UserRecords.FavoriteRecords.IFavoriteRecordsService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Linq;

#nullable disable
namespace PX.Data.UserRecords.FavoriteRecords;

/// <summary>Interface for favorite records service.</summary>
[PXInternalUseOnly]
public interface IFavoriteRecordsService
{
  /// <summary>
  /// Adds record with NoteID <paramref name="refNoteID" /> to the current user favorites. Returns true if it succeeds, false if it fails.
  /// </summary>
  /// <param name="graph">Graph to work on.</param>
  /// <param name="refNoteID">Entity's NoteID.</param>
  /// <param name="entityTypeName">Name of the entity's type.</param>
  /// <returns />
  bool AddToCurrentUserFavorites(PXGraph graph, Guid refNoteID, string entityTypeName);

  /// <summary>
  /// Removes record from current user favorites described by <paramref name="refNoteID" />.
  /// </summary>
  /// <param name="refNoteID">Entity's NoteID.</param>
  /// <param name="entityTypeName">Name of the entity's type.</param>
  void RemoveFromCurrentUserFavorites(Guid refNoteID, string entityTypeName);

  /// <summary>Gets the current user favorite records.</summary>
  IQueryable<FavoriteRecord> GetCurrentUserFavoriteRecords();
}
