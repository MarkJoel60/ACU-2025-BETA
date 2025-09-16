// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.ClaimActiveDirectoryProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Access.ActiveDirectory;

internal sealed class ClaimActiveDirectoryProvider(
  IOptions<PX.Data.Access.ActiveDirectory.Options> options,
  ILoggerFactory loggerFactory,
  IExpirationStorage expirationStorage) : ActiveDirectoryProvider((string) null, (Credential) null, options.Value, loggerFactory, expirationStorage), IActiveDirectoryProvider
{
  public string[] GetUserGroupIDs(string username, bool useCached = true)
  {
    if (!ActiveDirectoryProvider.IsSID(username))
    {
      User userByLogin = this.GetUserByLogin(username, true);
      if (userByLogin != null)
        username = $"{ActiveDirectoryProvider.ExtractLogin(username, this.DefaultDomainComponent).Domain}\\{userByLogin.SID}";
    }
    return this.GetGroupsBySID(username, useCached);
  }

  public string[] GetUserGroupIDsByLogin(string name, bool useCached = true)
  {
    Func<string[]> del = (Func<string[]>) (() => this.GetUserGroupIDsInternal_((string) null, name));
    return this.GetCachedGroupsForUser(name, del, useCached).ToArray<string>();
  }

  public string[] GetUserGroupIDsBySID(string sid, bool useCached = true)
  {
    Func<string[]> del = (Func<string[]>) (() => this.GetUserGroupIDsInternal_(sid, (string) null));
    return this.GetCachedGroupsForUser(sid, del, useCached).ToArray<string>();
  }

  private string[] GetUserGroupIDsInternal_(string sid, string name) => new string[0];

  public IEnumerable<User> GetUsersByGroupIDs(string[] groups, bool useCached = true)
  {
    return (IEnumerable<User>) new User[0];
  }

  public User GetUser(object providerUserKey)
  {
    if (providerUserKey == null)
      return (User) null;
    string userKey = providerUserKey.ToString();
    return this.GetCachedUser(userKey, (Func<User>) (() => !ActiveDirectoryProvider.IsGUID(userKey) ? this.GetUserByLoginInternal(userKey) : this.GetUserBySIDInternal(userKey)), true);
  }

  public Group GetGroup(string sid)
  {
    foreach (Group group in this.GetGroups(true))
    {
      if (group.SID == sid)
        return group;
    }
    return (Group) null;
  }

  public User GetUserByLogin(string name, bool useCached = true)
  {
    return string.IsNullOrEmpty(name) ? (User) null : this.GetCachedUser(name, (Func<User>) (() => this.GetUserByLoginInternal(name)), useCached);
  }

  public User GetUserBySID(string sid, bool useCached = true)
  {
    return string.IsNullOrEmpty(sid) ? (User) null : this.GetCachedUser(sid, (Func<User>) (() => this.GetUserBySIDInternal(sid)), useCached);
  }

  public User GetUserBySIDInternal(string sid) => (User) null;

  private string[] GetGroupsBySID(string sid, bool useCached)
  {
    if (this.GetUser((object) sid) == null)
      return new string[0];
    Func<string[]> del = (Func<string[]>) (() => this.GetUserGroupIDsInternal_(sid, (string) null));
    return this.GetCachedGroupsForUser(sid, del, useCached).ToArray<string>();
  }

  private Group[] GetGroupsInternal()
  {
    this.GetDefaultDomain();
    return new Group[0];
  }

  public IEnumerable<Group> GetGroups(bool useCached = true)
  {
    return this.GetCachedGroups(new Func<Group[]>(this.GetGroupsInternal), useCached);
  }

  public IEnumerable<User> GetUsers(bool useCached = true)
  {
    return this.GetCachedUsers((Func<User[]>) (() => new User[0]), useCached);
  }

  public bool ValidateUser(string login, string password) => false;

  private User GetUserByLoginInternal(string providerUserKey) => (User) null;

  private string GetDefaultDomain() => (string) null;
}
