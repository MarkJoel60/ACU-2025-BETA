// Decompiled with JetBrains decompiler
// Type: PX.Data.ICurrentUserInformationProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

public interface ICurrentUserInformationProvider
{
  /// <returns>User ID, or <see langword="null" /> if there is no current user, or the user is not found.</returns>
  Guid? GetUserId();

  /// <returns>User ID, or <c>new Guid()</c> if there is no current user, or the user is not found, or the application is in design mode.</returns>
  /// <remarks>The default behavior is implemented in the legacy code copied from <see cref="M:PX.Data.PXAccess.GetUserID">PXAccess.GetUserID()</see>.</remarks>
  Guid GetUserIdOrDefault();

  /// <returns>The ID of the current user, or the ID of the user being impersonated, or <c>new Guid()</c> if there is no current user, or the user is not found, or the application is in design mode.</returns>
  /// <remarks>The default behavior is implemented in the legacy code copied from <see cref="M:PX.Data.PXAccess.GetUserID">PXAccess.GetUserID()</see>.</remarks>
  [PXInternalUseOnly]
  Guid GetUserIdAccountingForImpersonationOrDefault();

  string GetUserName();

  string GetUserDisplayName();

  string GetEmail();

  string GetBranchCD();

  IEnumerable<BranchInfo> GetActiveBranches();

  [PXInternalUseOnly]
  IEnumerable<BranchInfo> GetAllBranches();

  [PXInternalUseOnly]
  IEnumerable<int> GetActiveBranchesWithParents();

  [PXInternalUseOnly]
  IEnumerable<PXAccess.MasterCollection.Organization> GetOrganizations(
    bool onlyActive = true,
    bool skipGroups = true);

  [PXInternalUseOnly]
  string[] GetLicensedAccessibleCompanies();

  [PXInternalUseOnly]
  string[] GetAllAccessibleCompanies();

  [PXInternalUseOnly]
  bool IsActiveDirectoryUser();

  [PXInternalUseOnly]
  bool IsClaimUser();

  [PXInternalUseOnly]
  bool IsGuest();

  [PXInternalUseOnly]
  PXTimeZoneInfo GetTimeZone();
}
