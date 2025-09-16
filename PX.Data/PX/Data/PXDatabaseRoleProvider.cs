// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDatabaseRoleProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Linq;
using System.Web.Hosting;
using System.Web.Security;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public class PXDatabaseRoleProvider : RoleProvider
{
  private const string ROLES_SLOT_KEY = "DAATABASE_ROLES_DEFINITION";
  private string pApplicationName;
  private ILegacyCompanyService _legacyCompanyService;

  private PXDatabaseRoleProvider.RolesDefinition Definition
  {
    get
    {
      return PXDatabase.GetSlot<PXDatabaseRoleProvider.RolesDefinition, PXDatabaseRoleProvider>("DAATABASE_ROLES_DEFINITION", this, typeof (PX.SM.Roles), typeof (UsersInRoles));
    }
  }

  public override string ApplicationName
  {
    get => this.pApplicationName;
    set => this.pApplicationName = value;
  }

  public override void Initialize(string name, NameValueCollection config)
  {
    if (config == null)
      throw new PXArgumentException(nameof (config), "The argument cannot be null.");
    if (name == null || name.Length == 0)
      name = nameof (PXDatabaseRoleProvider);
    if (string.IsNullOrEmpty(config["description"]))
    {
      config.Remove("description");
      config.Add("description", "PX Role provider");
    }
    base.Initialize(name, config);
    this._legacyCompanyService = ProviderBaseDependencyHelper.Resolve<ILegacyCompanyService>();
    if (config["applicationName"] == null || config["applicationName"].Trim() == "")
      this.pApplicationName = HostingEnvironment.ApplicationVirtualPath;
    else
      this.pApplicationName = config["applicationName"];
  }

  public override void AddUsersToRoles(string[] usernames, string[] rolenames)
  {
    foreach (string rolename in rolenames)
    {
      if (!this.RoleExists(rolename))
        throw new PXProviderException("The role name is not found.");
    }
    foreach (string username in usernames)
    {
      if (username.Contains(","))
        throw new PXArgumentException("username", "Usernames cannot contain commas.");
      foreach (string rolename in rolenames)
      {
        if (this.IsUserInRole(username, rolename))
          throw new PXProviderException("The user already belongs to the role.");
      }
    }
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (string username in usernames)
      {
        foreach (string rolename in rolenames)
          PXDatabase.Insert<UsersInRoles>(new PXDataFieldAssign("Username", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) username), new PXDataFieldAssign("Rolename", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) rolename), new PXDataFieldAssign("ApplicationName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) this.ApplicationName), new PXDataFieldAssign("CreatedByID", PXDbType.UniqueIdentifier, new int?(30), (object) PXAccess.GetTrueUserID()), new PXDataFieldAssign("CreatedByScreenID", PXDbType.Char, new int?(8), (object) "00000000"), new PXDataFieldAssign("CreatedDateTime", PXDbType.DateTime, new int?(8), (object) System.DateTime.Now), new PXDataFieldAssign("LastModifiedByID", PXDbType.UniqueIdentifier, new int?(30), (object) PXAccess.GetTrueUserID()), new PXDataFieldAssign("LastModifiedByScreenID", PXDbType.Char, new int?(8), (object) "00000000"), new PXDataFieldAssign("LastModifiedDateTime", PXDbType.SmallDateTime, new int?(8), (object) System.DateTime.Now));
      }
      transactionScope.Complete();
    }
  }

  public override void CreateRole(string rolename)
  {
    if (rolename.Contains(","))
      throw new PXArgumentException(nameof (rolename), "Role names cannot contain commas.");
    if (this.RoleExists(rolename))
      throw new PXProviderException("The role name already exists.");
    PXDatabase.Insert<PX.SM.Roles>(new PXDataFieldAssign("Rolename", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) rolename), new PXDataFieldAssign("ApplicationName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) this.ApplicationName));
  }

  public override bool DeleteRole(string rolename, bool throwOnPopulatedRole)
  {
    if (!this.RoleExists(rolename))
      throw new PXProviderException("The role name is not found.");
    if (throwOnPopulatedRole && this.GetUsersInRole(rolename).Length != 0)
      throw new PXProviderException("A populated role cannot be deleted.");
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      PXDatabase.Delete<PX.SM.Roles>(new PXDataFieldRestrict("Rolename", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) rolename), new PXDataFieldRestrict("ApplicationName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) this.ApplicationName));
      PXDatabase.Delete<UsersInRoles>(new PXDataFieldRestrict("Rolename", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) rolename), new PXDataFieldRestrict("ApplicationName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) this.ApplicationName));
      transactionScope.Complete();
    }
    return true;
  }

  public override string[] GetAllRoles()
  {
    PXDatabaseRoleProvider.RolesDefinition definition = this.Definition;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return definition == null ? Array.Empty<string>() : definition.Roles.Where<string>(PXDatabaseRoleProvider.\u003C\u003EO.\u003C0\u003E__IsRoleEnabled ?? (PXDatabaseRoleProvider.\u003C\u003EO.\u003C0\u003E__IsRoleEnabled = new Func<string, bool>(PXAccess.IsRoleEnabled))).ToArray<string>();
  }

  public override string[] GetRolesForUser(string username)
  {
    username = this._legacyCompanyService.ExtractUsername(username);
    PXDatabaseRoleProvider.RolesDefinition definition = this.Definition;
    OrderedHashSet<string> source;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return definition == null || !definition.RolesInUser.TryGetValue(username, out source) ? Array.Empty<string>() : ((IEnumerable<string>) source).Where<string>(PXDatabaseRoleProvider.\u003C\u003EO.\u003C0\u003E__IsRoleEnabled ?? (PXDatabaseRoleProvider.\u003C\u003EO.\u003C0\u003E__IsRoleEnabled = new Func<string, bool>(PXAccess.IsRoleEnabled))).ToArray<string>();
  }

  public override string[] GetUsersInRole(string rolename)
  {
    PXDatabaseRoleProvider.RolesDefinition definition = this.Definition;
    OrderedHashSet<string> source;
    return definition == null || !PXAccess.IsRoleEnabled(rolename) || !definition.UsersInRole.TryGetValue(rolename, out source) ? Array.Empty<string>() : ((IEnumerable<string>) source).ToArray<string>();
  }

  public override bool IsUserInRole(string username, string rolename)
  {
    username = this._legacyCompanyService.ExtractUsername(username);
    PXCollationComparer comparer = PXLocalesProvider.CollationComparer;
    PXDatabaseRoleProvider.RolesDefinition definition = this.Definition;
    OrderedHashSet<string> source;
    if (definition == null || !definition.RolesInUser.TryGetValue(username, out source))
      return false;
    string role = ((IEnumerable<string>) source).FirstOrDefault<string>((Func<string, bool>) (r => comparer.Equals(r, rolename)));
    return role != null && PXAccess.IsRoleEnabled(role);
  }

  public override void RemoveUsersFromRoles(string[] usernames, string[] rolenames)
  {
    foreach (string rolename in rolenames)
    {
      if (!this.RoleExists(rolename))
        throw new PXProviderException("The role name is not found.");
    }
    foreach (string username in usernames)
    {
      foreach (string rolename in rolenames)
      {
        if (!this.IsUserInRole(username, rolename))
          throw new PXProviderException("The user does not belong to a role.");
      }
    }
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (string username in usernames)
      {
        foreach (string rolename in rolenames)
          PXDatabase.Delete<UsersInRoles>(new PXDataFieldRestrict("Username", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) username), new PXDataFieldRestrict("Rolename", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) rolename), new PXDataFieldRestrict("ApplicationName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) this.ApplicationName));
      }
      transactionScope.Complete();
    }
  }

  public override bool RoleExists(string rolename)
  {
    PXCollationComparer comparer = PXLocalesProvider.CollationComparer;
    PXDatabaseRoleProvider.RolesDefinition definition = this.Definition;
    if (definition == null)
      return false;
    string role = definition.Roles.FirstOrDefault<string>((Func<string, bool>) (r => comparer.Equals(r, rolename)));
    return role != null && PXAccess.IsRoleEnabled(role);
  }

  public override string[] FindUsersInRole(string rolename, string usernameToMatch)
  {
    PXCollationComparer comparer = PXLocalesProvider.CollationComparer;
    PXDatabaseRoleProvider.RolesDefinition definition = this.Definition;
    OrderedHashSet<string> source;
    return definition == null || !PXAccess.IsRoleEnabled(rolename) || !definition.UsersInRole.TryGetValue(rolename, out source) ? Array.Empty<string>() : ((IEnumerable<string>) source).Where<string>((Func<string, bool>) (user => comparer.StartsWith(user, usernameToMatch))).ToArray<string>();
  }

  private class RolesDefinition : IPrefetchable<PXDatabaseRoleProvider>, IPXCompanyDependent
  {
    public readonly List<string> Roles = new List<string>();
    public readonly Dictionary<string, OrderedHashSet<string>> UsersInRole = new Dictionary<string, OrderedHashSet<string>>((IEqualityComparer<string>) PXLocalesProvider.CollationComparer);
    public readonly Dictionary<string, OrderedHashSet<string>> RolesInUser = new Dictionary<string, OrderedHashSet<string>>((IEqualityComparer<string>) PXLocalesProvider.CollationComparer);

    public void Prefetch(PXDatabaseRoleProvider provider)
    {
      this.Roles.Clear();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PX.SM.Roles>(new PXDataField("Rolename"), (PXDataField) new PXDataFieldValue("ApplicationName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) provider.ApplicationName)))
        this.Roles.Add(pxDataRecord.GetString(0));
      this.UsersInRole.Clear();
      this.RolesInUser.Clear();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<UsersInRoles>(new PXDataField("Username"), new PXDataField("Rolename"), (PXDataField) new PXDataFieldValue("ApplicationName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) provider.ApplicationName)))
      {
        string key1 = pxDataRecord.GetString(0);
        string key2 = pxDataRecord.GetString(1);
        PXCollationComparer collationComparer = PXLocalesProvider.CollationComparer;
        OrderedHashSet<string> orderedHashSet1;
        if (!this.RolesInUser.TryGetValue(key1, out orderedHashSet1))
          this.RolesInUser[key1] = orderedHashSet1 = new OrderedHashSet<string>((IEqualityComparer<string>) collationComparer);
        orderedHashSet1.Add(key2);
        OrderedHashSet<string> orderedHashSet2;
        if (!this.UsersInRole.TryGetValue(key2, out orderedHashSet2))
          this.UsersInRole[key2] = orderedHashSet2 = new OrderedHashSet<string>((IEqualityComparer<string>) collationComparer);
        orderedHashSet2.Add(key1);
      }
    }
  }
}
