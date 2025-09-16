// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.IActiveDirectoryProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Access.ActiveDirectory;

[PXInternalUseOnly]
public interface IActiveDirectoryProvider
{
  string DefaultDomainComponent { get; }

  string[] GetUserGroupIDsByLogin(string name, bool useCached = true);

  string[] GetUserGroupIDsBySID(string sid, bool useCached = true);

  string[] GetUserGroupIDs(string username, bool useCached = true);

  IEnumerable<Group> GetGroups(bool useCached = true);

  IEnumerable<User> GetUsers(bool useCached = true);

  User GetUserByLogin(string name, bool useCached = true);

  User GetUserBySID(string sid, bool useCached = true);

  User GetUser(object providerUserKey);

  Group GetGroup(string sid);

  IEnumerable<User> GetUsersByGroupIDs(string[] groups, bool useCached = true);

  void Reset();

  bool ValidateUser(string login, string password);

  /// <summary>
  /// Checks whether user is disabled in the Active Directory
  /// </summary>
  /// <param name="login">The login of the user</param>
  /// <returns>Returns true only if user is found in the AD and disabled by some implementation specific rule, otherwise false.</returns>
  bool IsUserDisabled(string login);
}
