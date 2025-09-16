// Decompiled with JetBrains decompiler
// Type: PX.SM.AccessWebUsers
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.EP;
using PX.Licensing;
using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.SM;

public class AccessWebUsers : PX.SM.Access
{
  public PXCancel<Users> Cancel;
  public PXSave<Users> Save;
  public PXSelect<EPLoginType, Where<EPLoginType.loginTypeID, Equal<Current<Users.loginTypeID>>>> LoginType;
  public PXAction<Users> AddADUser;
  public PXAction<Users> AddADUserOK;
  [PXViewName("AllowedRoles")]
  public PXSelect<AccessWebUsers.AllowedRole> AllowedRoles;
  public PXFilter<ADUserFilter> ADUser;

  [InjectDependency]
  private ILicensing _licensing { get; set; }

  [PXUIField(DisplayName = "Add Active Directory User", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update, Visible = false)]
  [PXButton]
  public IEnumerable addADUser(PXAdapter adapter) => adapter.Get();

  [PXUIField(DisplayName = "OK", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update, Visible = false)]
  [PXButton]
  public IEnumerable addADUserOK(PXAdapter adapter) => adapter.Get();

  [PXDBString(256 /*0x0100*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Username", Visibility = PXUIVisibility.SelectorVisible)]
  [PXUsersSelector]
  protected virtual void Users_Username_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(256 /*0x0100*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault(typeof (Users.username))]
  [PXUIField(DisplayName = "Username")]
  [PXParent(typeof (PX.Data.Select<Users, Where<Users.username, Equal<Current<UsersInRoles.username>>>>))]
  [PXSelector(typeof (Search<Users.username, Where<Current<PX.SM.Roles.guest>, Equal<PX.Data.True>, Or<Users.guest, NotEqual<PX.Data.True>>>>), DescriptionField = typeof (Users.comment), DirtyRead = true)]
  protected virtual void UsersInRoles_Username_CacheAttached(PXCache sender)
  {
  }

  protected virtual void _(Events.RowSelected<Users> e)
  {
    if (e.Row == null)
      return;
    EPLoginType epLoginType = this.LoginType.SelectSingle();
    if (epLoginType != null)
    {
      bool? disableTwoFactorAuth = epLoginType.DisableTwoFactorAuth;
      bool flag = true;
      if (disableTwoFactorAuth.GetValueOrDefault() == flag & disableTwoFactorAuth.HasValue || epLoginType.AllowedLoginType == "A")
      {
        PXUIFieldAttribute.SetEnabled<Users.multiFactorOverride>(e.Cache, (object) e.Row, false);
        PXUIFieldAttribute.SetEnabled<Users.multiFactorType>(e.Cache, (object) e.Row, false);
        return;
      }
    }
    PXUIFieldAttribute.SetEnabled<Users.multiFactorOverride>(e.Cache, (object) e.Row, true);
  }

  protected virtual void _(
    Events.FieldSelecting<Users, Users.allowedSessions> e)
  {
    if (e.ReturnValue != null || e.Row == null || !e.Row.LoginTypeID.HasValue)
      return;
    e.ReturnValue = (object) (int?) this.LoginType.SelectSingle()?.AllowedSessions;
  }

  protected virtual void _(
    Events.FieldUpdating<Users, Users.allowedSessions> e)
  {
    if (e.NewValue == null || !e.Row.LoginTypeID.HasValue)
      return;
    int? allowedSessions = (int?) this.LoginType.SelectSingle()?.AllowedSessions;
    int newValue = (int) e.NewValue;
    if (!(allowedSessions.GetValueOrDefault() == newValue & allowedSessions.HasValue))
      return;
    e.NewValue = (object) null;
  }

  protected virtual void _(Events.FieldUpdated<Users, Users.loginTypeID> e)
  {
    if (e.NewValue == null || this.LoginType.SelectSingle() == null)
      return;
    e.Cache.SetValueExt<Users.allowedSessions>((object) e.Row, (object) null);
  }

  protected virtual void _(
    Events.FieldVerifying<Users, Users.allowedSessions> e)
  {
    if (!(e.NewValue is int newValue))
      return;
    PXLicense license = this._licensing.License;
    int val1 = WebConfig.MaximumAllowedSessionsCount ?? int.MaxValue;
    int val2;
    switch (this.LoginType.SelectSingle()?.AllowedLoginType)
    {
      case "U":
        val2 = license.UsersAllowed;
        break;
      case "A":
        val2 = license.MaxApiUsersAllowed;
        break;
      default:
        val2 = System.Math.Max(license.UsersAllowed, license.MaxApiUsersAllowed);
        break;
    }
    int num = System.Math.Max(3, System.Math.Min(val1, val2));
    if (newValue < 1 || newValue > num)
      throw new PXSetPropertyException("The value in the Max. Number of Concurrent Logins box should be between {1} and {0}.", new object[2]
      {
        (object) num,
        (object) 1
      });
  }

  public AccessWebUsers()
  {
    this.Cancel.SetVisible(false);
    this.Save.SetVisible(false);
    this.AllowedRoles.Cache.AllowSelect = false;
    PXUIFieldAttribute.SetVisible<Users.loginTypeID>(this.UserList.Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<Users.contactID>(this.UserList.Cache, (object) null, false);
  }

  protected virtual IEnumerable userList()
  {
    AccessWebUsers graph = this;
    Hashtable hashtable = new Hashtable();
    Users inserted;
    foreach (Users users in graph.UserList.Cache.Inserted)
    {
      inserted = users;
      yield return (object) inserted;
      hashtable.Add((object) inserted.PKID, (object) inserted);
      inserted = (Users) null;
    }
    foreach (Users users in graph.UserList.Cache.Updated)
    {
      inserted = users;
      yield return (object) inserted;
      hashtable.Add((object) inserted.PKID, (object) inserted);
      inserted = (Users) null;
    }
    foreach (object obj in graph.ActiveDirectoryProvider.GetAllUsers((PXGraph) graph, graph.UserList.View.BqlSelect).Where<Users>((Func<Users, bool>) (user => !user.PKID.HasValue || !hashtable.ContainsKey((object) user.PKID))))
      yield return obj;
  }

  protected virtual IEnumerable roleList()
  {
    AccessWebUsers accessWebUsers = this;
    string username = accessWebUsers.UserList.Current == null ? (string) null : accessWebUsers.UserList.Current.Username;
    Hashtable ids = new Hashtable();
    AccessWebUsers graph = accessWebUsers;
    object[] objArray = new object[1]{ (object) username };
    foreach (UsersInRoles usersInRoles in PXSelectBase<UsersInRoles, PXSelect<UsersInRoles, Where<UsersInRoles.applicationName, Equal<Current<Users.applicationName>>, And<UsersInRoles.username, Equal<Required<UsersInRoles.username>>>>>.Config>.Select((PXGraph) graph, objArray).RowCast<UsersInRoles>().Where<UsersInRoles>((Func<UsersInRoles, bool>) (role => role.Rolename != null && !ids.Contains((object) role.Rolename) && PXAccess.IsRoleEnabled(role.Rolename))))
    {
      ids.Add((object) usersInRoles.Rolename, (object) usersInRoles);
      yield return (object) usersInRoles;
    }
    string extRef = accessWebUsers.UserList.Current == null ? (string) null : accessWebUsers.UserList.Current.ExtRef;
    if (!string.IsNullOrEmpty(extRef))
    {
      string[] userGroupIdsBySid = accessWebUsers.ActiveDirectoryProvider.GetUserGroupIDsBySID(extRef, false);
      string applicationName = accessWebUsers.UserList.Current.ApplicationName;
      foreach (string str in PX.SM.Access.RolesForADGroupsDefinition.Get(userGroupIdsBySid).Where<string>((Func<string, bool>) (role => !ids.ContainsKey((object) role) && PXAccess.IsRoleEnabled(role))))
        yield return (object) new UsersInRoles()
        {
          Username = username,
          ApplicationName = applicationName,
          Rolename = str,
          Inherited = new bool?(true)
        };
      applicationName = (string) null;
    }
  }

  public override void ClearDependencies()
  {
    PXDatabase.SelectTimeStamp();
    PXPageCacheUtils.InvalidateCachedPages();
  }

  [Serializable]
  public class AllowedRole : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXUIField(DisplayName = "Selected")]
    public bool? Selected { get; set; }

    [PXUIField(DisplayName = "Role Name", Visibility = PXUIVisibility.SelectorVisible)]
    [PXSelector(typeof (Search<PX.SM.Roles.rolename, Where<PX.SM.Roles.guest, Equal<PX.Data.True>>>), DescriptionField = typeof (PX.SM.Roles.descr))]
    public virtual string Rolename { get; set; }
  }
}
