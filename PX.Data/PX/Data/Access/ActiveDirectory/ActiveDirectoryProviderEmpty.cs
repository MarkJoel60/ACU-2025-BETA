// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.ActiveDirectoryProviderEmpty
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Access.ActiveDirectory;

internal sealed class ActiveDirectoryProviderEmpty : IActiveDirectoryProvider
{
  internal static readonly IActiveDirectoryProvider Instance = (IActiveDirectoryProvider) new ActiveDirectoryProviderEmpty();

  public bool ValidateUser(string login, string password) => false;

  public bool IsUserDisabled(string login) => false;

  public User GetUser(object providerUserKey) => (User) null;

  public Group GetGroup(string sid) => (Group) null;

  public string[] GetUserGroupIDs(string login, bool useCached = true) => new string[0];

  public string[] GetUserGroupIDsByLogin(string name, bool useCached = true) => new string[0];

  public string[] GetUserGroupIDsBySID(string sid, bool useCached = true) => new string[0];

  public IEnumerable<User> GetUsersByGroupIDs(string[] groups, bool useCached = true)
  {
    yield break;
  }

  public IEnumerable<Group> GetGroups(bool useCached = true)
  {
    yield break;
  }

  public IEnumerable<User> GetUsers(bool useCached = true)
  {
    yield break;
  }

  public User GetUserBySID(string sid, bool useCached = true) => (User) null;

  public User GetUserByLogin(string name, bool useCached = true) => (User) null;

  public void Reset()
  {
  }

  public string DefaultDomainComponent
  {
    get => string.Empty;
    set
    {
    }
  }

  public string DirectoryService
  {
    get => string.Empty;
    set
    {
    }
  }

  public string DirectoryVersion
  {
    get => string.Empty;
    set
    {
    }
  }

  public string TokenService
  {
    get => string.Empty;
    set
    {
    }
  }
}
