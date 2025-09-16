// Decompiled with JetBrains decompiler
// Type: PX.Data.PXActiveDirectorySyncRoleProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Access.ActiveDirectory;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Security;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public sealed class PXActiveDirectorySyncRoleProvider : RoleProvider
{
  private RoleProvider _dbProvider;
  private IActiveDirectoryProvider _adProvider;
  private ILegacyCompanyService _legacyCompanyService;

  private PXActiveDirectorySyncRoleProvider.UsersBySource Definition
  {
    get
    {
      return PXDatabase.GetSlot<PXActiveDirectorySyncRoleProvider.UsersBySource, PXActiveDirectorySyncRoleProvider>("USERS_BY_SOURCE", this, typeof (Users));
    }
  }

  public override void Initialize(string name, NameValueCollection config)
  {
    base.Initialize(name, config);
    string typeName = config["mainProviderType"];
    if (typeName != null && !string.IsNullOrEmpty(typeName))
    {
      System.Type type = PXBuildManager.GetType(typeName, false);
      if ((object) type != null && type.GetConstructor(new System.Type[0]) != (ConstructorInfo) null)
      {
        this._dbProvider = (RoleProvider) Activator.CreateInstance(type);
        goto label_4;
      }
    }
    this._dbProvider = (RoleProvider) new PXDatabaseRoleProvider();
label_4:
    this._dbProvider.Initialize(name, config);
    this._adProvider = ProviderBaseDependencyHelper.Resolve<IActiveDirectoryProvider>();
    this._legacyCompanyService = ProviderBaseDependencyHelper.Resolve<ILegacyCompanyService>();
  }

  public override bool IsUserInRole(string username, string roleName)
  {
    if (!PXAccess.IsRoleEnabled(roleName))
      return false;
    bool flag = this._dbProvider.IsUserInRole(username, roleName);
    if (!flag)
      flag = Array.FindIndex<string>(this.GetRolesForADUser(username), (Predicate<string>) (s => string.Compare(s, roleName, true) == 0)) > -1;
    return flag;
  }

  public override string[] GetRolesForUser(string username)
  {
    string[] rolesForUser = this._dbProvider.GetRolesForUser(username);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return (rolesForUser.Length == 0 ? (IEnumerable<string>) this.GetRolesForADUser(username) : (IEnumerable<string>) rolesForUser).Where<string>(PXActiveDirectorySyncRoleProvider.\u003C\u003EO.\u003C0\u003E__IsRoleEnabled ?? (PXActiveDirectorySyncRoleProvider.\u003C\u003EO.\u003C0\u003E__IsRoleEnabled = new Func<string, bool>(PXAccess.IsRoleEnabled))).ToArray<string>();
  }

  public override void CreateRole(string roleName)
  {
    if (!PXAccess.IsRoleEnabled(roleName))
      return;
    this._dbProvider.CreateRole(roleName);
  }

  public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
  {
    return PXAccess.IsRoleEnabled(roleName) && this._dbProvider.DeleteRole(roleName, throwOnPopulatedRole);
  }

  public override bool RoleExists(string roleName)
  {
    return PXAccess.IsRoleEnabled(roleName) && this._dbProvider.RoleExists(roleName);
  }

  public override void AddUsersToRoles(string[] usernames, string[] roleNames)
  {
    this._dbProvider.AddUsersToRoles(usernames, roleNames);
  }

  public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
  {
    this._dbProvider.RemoveUsersFromRoles(usernames, roleNames);
  }

  public override string[] GetUsersInRole(string roleName)
  {
    return ((IEnumerable<string>) this._dbProvider.GetUsersInRole(roleName)).Concat<string>(((IEnumerable<string>) this.GetADUsersForRole(roleName)).Where<string>((Func<string, bool>) (r => this.Definition.Users[1].Contains(r)))).ToArray<string>();
  }

  public override string[] GetAllRoles() => this._dbProvider.GetAllRoles();

  public override string[] FindUsersInRole(string roleName, string usernameToMatch)
  {
    return this._dbProvider.FindUsersInRole(roleName, usernameToMatch);
  }

  public override string ApplicationName
  {
    get => this._dbProvider.ApplicationName;
    set => this._dbProvider.ApplicationName = value;
  }

  private string[] GetRolesForADUser(string username)
  {
    string username1 = this._legacyCompanyService.ExtractUsername(username);
    List<string> source = new List<string>((IEnumerable<string>) PX.SM.Access.GetRolesForADGroups(this._adProvider.GetUserGroupIDs(username1)));
    PX.Data.Access.ActiveDirectory.User user = this._adProvider.GetUser((object) username1);
    if (user != null && !string.IsNullOrEmpty(user.Name.DomainLogin))
    {
      string[] rolesForUser = this._dbProvider.GetRolesForUser(user.Name.DomainLogin);
      source.AddRange((IEnumerable<string>) rolesForUser);
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return source.Where<string>(PXActiveDirectorySyncRoleProvider.\u003C\u003EO.\u003C0\u003E__IsRoleEnabled ?? (PXActiveDirectorySyncRoleProvider.\u003C\u003EO.\u003C0\u003E__IsRoleEnabled = new Func<string, bool>(PXAccess.IsRoleEnabled))).ToArray<string>();
  }

  private string[] GetADUsersForRole(string roleName)
  {
    return !PXAccess.IsRoleEnabled(roleName) ? new string[0] : this._adProvider.GetUsersByGroupIDs(PX.SM.Access.GetADGroupsForRole(roleName)).Select<PX.Data.Access.ActiveDirectory.User, string>((Func<PX.Data.Access.ActiveDirectory.User, string>) (user => user.Name.DomainLogin)).ToArray<string>();
  }

  private class UsersBySource : IPrefetchable<PXActiveDirectorySyncRoleProvider>, IPXCompanyDependent
  {
    public Dictionary<int, HashSet<string>> Users;

    public void Prefetch(PXActiveDirectorySyncRoleProvider provider)
    {
      this.Users = new Dictionary<int, HashSet<string>>();
      this.Users[1] = new HashSet<string>();
      this.Users[2] = new HashSet<string>();
      this.Users[0] = new HashSet<string>();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<Users>(new PXDataField("Username"), new PXDataField("Source"), (PXDataField) new PXDataFieldValue("ApplicationName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) provider.ApplicationName)))
      {
        int valueOrDefault = pxDataRecord.GetInt32(1).GetValueOrDefault();
        HashSet<string> stringSet;
        if (!this.Users.TryGetValue(valueOrDefault, out stringSet))
          this.Users[valueOrDefault] = stringSet = new HashSet<string>();
        stringSet.Add(pxDataRecord.GetString(0));
      }
    }
  }
}
