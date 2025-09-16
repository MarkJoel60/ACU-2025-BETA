// Decompiled with JetBrains decompiler
// Type: PX.Data.Services.Interfaces.IFavoriteActionsService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Services.Implementations;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Services.Interfaces;

/// <summary>Interface for favorite actions service.</summary>
[PXInternalUseOnly]
public interface IFavoriteActionsService
{
  /// <summary>
  /// Adds record with Action Name <paramref name="actionName" /> to the current user favorites. Returns true if it succeeds, false if it fails.
  /// </summary>
  /// <param name="actionName">Name of the action added to favorites.</param>
  /// <param name="screenId">Screen Id.</param>
  /// <returns />
  bool AddToCurrentUserFavorites(string screenId, string actionName);

  /// <summary>
  /// Removes Action from current user favorites described by <paramref name="actionName" />.
  /// </summary>
  /// <param name="actionName">Name of the action added to favorites.</param>
  /// <param name="screenId">Screen Id.</param>
  void RemoveFromCurrentUserFavorites(string screenId, string actionName);

  /// <summary>Gets the current user favorite actions.</summary>
  /// <returns>The current user favorite actions.</returns>
  IEnumerable<FavoriteActionRecord> GetCurrentUserFavoriteActions(PXGraph graph, string screenId);
}
