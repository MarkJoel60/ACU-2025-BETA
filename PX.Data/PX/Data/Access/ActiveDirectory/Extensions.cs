// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.Extensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Access.ActiveDirectory;

[PXInternalUseOnly]
public static class Extensions
{
  [PXInternalUseOnly]
  public static bool IsEnabled(
    this IActiveDirectoryProvider activeDirectoryProvider)
  {
    return activeDirectoryProvider != ActiveDirectoryProviderEmpty.Instance;
  }

  internal static IEnumerable<Users> GetAllUsers(
    this IActiveDirectoryProvider activeDirectoryProvider,
    PXGraph graph,
    BqlCommand dbUsersSelect)
  {
    Dictionary<string, Users> existingADUsers = new Dictionary<string, Users>();
    foreach (Users allUser in new PXView(graph, true, dbUsersSelect).SelectMulti())
    {
      int? source = allUser.Source;
      int num = 1;
      if (source.GetValueOrDefault() == num & source.HasValue)
        existingADUsers.Add(allUser.Username, allUser);
      yield return allUser;
    }
    foreach (Users allUser in activeDirectoryProvider.GetADUsers(graph.Caches[dbUsersSelect.GetTables()[0]]).Where<Users>((Func<Users, bool>) (user => !existingADUsers.ContainsKey(user.Username))))
      yield return allUser;
  }

  private static IEnumerable<Users> GetADUsers(
    this IActiveDirectoryProvider activeDirectoryProvider,
    PXCache cache)
  {
    foreach (User user in activeDirectoryProvider.GetUsers(false))
    {
      Users instance = (Users) cache.CreateInstance();
      instance.Fill(user);
      yield return instance;
    }
  }

  public static string[] GetADMappedRolesBySID(
    this IActiveDirectoryProvider activeDirectoryProvider,
    string sid)
  {
    return PX.SM.Access.RolesForADGroupsDefinition.Get(activeDirectoryProvider.GetUserGroupIDsBySID(sid)).Distinct<string>().ToArray<string>();
  }

  internal static bool IsAzureActiveDirectoryProvider(
    this IActiveDirectoryProvider activeDirectoryProvider)
  {
    return activeDirectoryProvider.GetType() == typeof (ADALActiveDirectoryProvider) || activeDirectoryProvider.GetType() == typeof (GraphApiActiveDirectoryProvider);
  }

  internal static bool IsClaimActiveDirectoryProvider(
    this IActiveDirectoryProvider activeDirectoryProvider)
  {
    return activeDirectoryProvider.GetType() == typeof (ClaimActiveDirectoryProvider);
  }

  internal static bool CheckUserRoles(
    this IActiveDirectoryProvider activeDirectoryProvider,
    string userName,
    bool isADUser,
    bool isClaimUser)
  {
    if (isADUser && PXAccess.FeatureSetInstalled("PX.Objects.CS.FeaturesSet") && !PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+ActiveDirectoryAndOtherExternalSSO"))
      return false;
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<UsersInRoles>(new PXDataField("RoleName"), (PXDataField) new PXDataFieldValue("UserName", PXDbType.NVarChar, new int?(64 /*0x40*/), (object) userName)))
    {
      if (pxDataRecord != null)
        return true;
    }
    if (isADUser)
    {
      foreach (string userGroupId in activeDirectoryProvider.GetUserGroupIDs(userName))
      {
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<RoleActiveDirectory>((PXDataField) new PXDataField<RoleActiveDirectory.role>(), (PXDataField) new PXDataFieldValue<RoleActiveDirectory.groupID>(PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) userGroupId)))
        {
          if (pxDataRecord != null)
            return true;
        }
      }
    }
    if (isClaimUser)
    {
      foreach (string userGroupId in activeDirectoryProvider.GetUserGroupIDs(userName))
      {
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<RoleClaims>((PXDataField) new PXDataField<RoleClaims.role>(), (PXDataField) new PXDataFieldValue<RoleClaims.groupID>(PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) userGroupId)))
        {
          if (pxDataRecord != null)
            return true;
        }
      }
    }
    return false;
  }
}
