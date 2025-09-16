// Decompiled with JetBrains decompiler
// Type: PX.SM.Access
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Options;
using PX.Api;
using PX.Common;
using PX.CS;
using PX.Data;
using PX.Data.Access.ActiveDirectory;
using PX.Data.Auth;
using PX.Data.BQL;
using PX.Data.DependencyInjection;
using PX.Data.EP;
using PX.Data.Maintenance;
using PX.Data.Wiki.Parser;
using PX.Export.Authentication;
using PX.Security;
using PX.SP;
using PX.Web.UI;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

#nullable enable
namespace PX.SM;

public class Access : PXGraph<
#nullable disable
PX.SM.Access>, IGraphWithInitialization, ICanAlterSiteMap
{
  public PXSelect<PX.SM.Roles> Roles;
  public PXSave<PX.SM.Roles> Save;
  public PXCancel<PX.SM.Roles> Cancel;
  public PXSave<Role> EntSave;
  public PXCancel<Role> EntCancel;
  public PXSelectOrderBy<Graph, OrderBy<Asc<Graph.text>>> Graph_All;
  public PXSelectOrderBy<PXEntity, OrderBy<Asc<PXEntity.orderBy>>> Entities;
  public PXSelectOrderBy<PXEntity, OrderBy<Asc<PXEntity.orderBy>>> EntitiesWithLeafs;
  [PXFilterable(new System.Type[] {})]
  public PXSelect<Role> EntityRoles;
  public PXSelect<PX.SM.Roles, Where<PX.SM.Roles.applicationName, Equal<Required<PX.SM.Roles.applicationName>>>> defRoles;
  public PXSelect<RolesInGraph, Where<RolesInGraph.screenID, Equal<Required<RolesInGraph.screenID>>, And<RolesInGraph.applicationName, Equal<Required<RolesInGraph.applicationName>>>>> defGraph;
  public PXSelect<RolesInCache, Where<RolesInCache.screenID, Equal<Required<RolesInCache.screenID>>, And<RolesInCache.cachetype, Equal<Required<RolesInCache.cachetype>>, And<RolesInCache.applicationName, Equal<Required<RolesInCache.applicationName>>>>>> defCache;
  public PXSelect<RolesInMember, Where<RolesInMember.screenID, Equal<Required<RolesInMember.screenID>>, And<RolesInMember.cachetype, Equal<Required<RolesInMember.cachetype>>, And<RolesInMember.membername, Equal<Required<RolesInMember.membername>>, And<RolesInMember.applicationName, Equal<Required<RolesInMember.applicationName>>>>>>> defMember;
  public PXSelect<RolesInMember, Where<RolesInMember.screenID, Equal<Required<RolesInMember.screenID>>, And<RolesInMember.cachetype, Equal<Required<RolesInMember.cachetype>>, And<RolesInMember.applicationName, Equal<Required<RolesInMember.applicationName>>, And<RolesInMember.rolename, Equal<Required<RolesInMember.rolename>>>>>>> defMemberCache;
  public PXSelect<RolesInCache, Where<RolesInCache.screenID, Equal<Required<RolesInCache.screenID>>, And<RolesInCache.rolename, Equal<Required<PX.SM.Roles.rolename>>>>> allCache;
  public PXSelect<RolesInMember, Where<RolesInMember.screenID, Equal<Required<RolesInMember.screenID>>, And<RolesInMember.rolename, Equal<Required<PX.SM.Roles.rolename>>>>> allMember;
  public PXSelect<RolesInGraph, Where<RolesInGraph.screenID, Equal<Required<RolesInGraph.screenID>>, And<RolesInGraph.applicationName, Equal<Required<RolesInGraph.applicationName>>, And<RolesInGraph.rolename, Equal<PX.Data.Access.Constants.universalRole>>>>> UniversalRoleAccessRights;
  public PXSelect<RolesInGraph, Where<RolesInGraph.applicationName, Equal<Required<RolesInGraph.applicationName>>>> allRights;
  public PXSelect<RolesInGraph, Where<RolesInGraph.screenID, Equal<Required<RolesInGraph.screenID>>, And<RolesInGraph.applicationName, Equal<Required<RolesInGraph.applicationName>>, And<RolesInGraph.rolename, Equal<Required<PX.SM.Roles.rolename>>>>>> roleGraph;
  [PXInternalUseOnly]
  public PXSelect<RolesInCache, Where<RolesInCache.screenID, Equal<Required<RolesInCache.screenID>>, And<RolesInCache.cachetype, Equal<Required<RolesInCache.cachetype>>, And<RolesInCache.applicationName, Equal<Required<RolesInCache.applicationName>>, And<RolesInCache.rolename, Equal<Required<RolesInCache.rolename>>>>>>> roleCache;
  [PXInternalUseOnly]
  public PXSelect<RolesInMember, Where<RolesInMember.screenID, Equal<Required<RolesInMember.screenID>>, And<RolesInMember.cachetype, Equal<Required<RolesInMember.cachetype>>, And<RolesInMember.membername, Equal<Required<RolesInMember.membername>>, And<RolesInMember.applicationName, Equal<Required<RolesInMember.applicationName>>, And<RolesInMember.rolename, Equal<Required<RolesInMember.rolename>>>>>>>> roleMember;
  [PXInternalUseOnly]
  public PXSelect<RolesInCache, Where<RolesInCache.screenID, Equal<Required<RolesInCache.screenID>>, And<RolesInCache.applicationName, Equal<Required<RolesInCache.applicationName>>>>> cacheRolesByScreen;
  [PXInternalUseOnly]
  public PXSelect<RolesInMember, Where<RolesInMember.screenID, Equal<Required<RolesInMember.screenID>>, And<RolesInMember.applicationName, Equal<Required<RolesInMember.applicationName>>>>> memberRolesByScreen;
  public PXFilter<PX.SM.Access.NewRoleFilter> NewRole;
  public PXSelect<WikiAccessRights> WikiAccessRightsRecs;
  [PXHidden]
  public PXSelectJoin<SFSyncRecord, InnerJoin<SYProvider, On<PX.Data.True, Equal<False>>, InnerJoin<SFEntitySetup, On<PX.Data.True, Equal<False>>>>> SyncRecs;
  public PXAction<PX.SM.Roles> CopyRole;
  [PXFilterable(new System.Type[] {})]
  public PXSelectOrderBy<Role, OrderBy<Asc<Role.orderBy>>> RoleEntities;
  public PXSelect<RoleActiveDirectory, Where<RoleActiveDirectory.role, Equal<Current<PX.SM.Roles.rolename>>>> ActiveDirectoryMap;
  public PXSelect<RoleClaims, Where<RoleClaims.role, Equal<Current<PX.SM.Roles.rolename>>>> ClaimsMap;
  public PXSave<Users> SaveUsers;
  public PXCancel<Users> CancelUsers;
  public PXInsert<Users> InsertUsers;
  public PXDelete<Users> DeleteUsers;
  public PXFirst<Users> FirstUsers;
  public PXPrevious<Users> PrevUsers;
  public PXNext<Users> NextUsers;
  public PXLast<Users> LastUsers;
  public PXAction<Users> LoginAsUser;
  public PXAction<Users> RestrictionGroupsByUser;
  public PXAction<Users> SendUserWelcome;
  public PXAction<Users> SendChangedPassword;
  public PXAction<Users> SendUserLogin;
  public PXAction<Users> SendUserPassword;
  [PXViewName("UserList")]
  public PXSelectUsers<Users> UserList;
  [PXViewName("UserListCurrent")]
  public PXSelect<Users, Where<Users.pKID, Equal<Current<Users.pKID>>>> UserListCurrent;
  [PXViewName("UserPrefs")]
  public PXSelect<UserPreferences, Where<UserPreferences.userID, Equal<Current<Users.pKID>>>> UserPrefs;
  [PXHidden]
  public PXSelect<Branch> BranchBase;
  [PXViewName("RoleList")]
  public PXSelectUsersInRoles RoleList;
  public PXSelect<UsersInRoles, Where<UsersInRoles.applicationName, Equal<Current<PX.SM.Roles.applicationName>>, And<UsersInRoles.rolename, Equal<Current<PX.SM.Roles.rolename>>>>> UsersByRole;
  public PXSelect<UsersInRoles, Where<UsersInRoles.applicationName, Equal<Current<Users.applicationName>>, And<UsersInRoles.username, Equal<Current<Users.username>>>>> RolesByUser;
  [PXViewName("UserFilters")]
  public PXSelect<UserFilter, Where<UserFilter.username, Equal<Current<Users.username>>>> UserFilters;
  [PXViewName("GeneralInfo")]
  public PXSelect<PX.SM.Access.AccessInfoNotification> GeneralInfo;
  [PXHidden]
  public PXSelect<PX.SM.EMailAccount> EMailAccount;
  [PXHidden]
  public PXSelect<PX.SM.EMailSyncAccount> EMailSyncAccount;
  [PXViewName("Identities")]
  public PXSelect<UserIdentity> Identities;
  public PXAction<UserIdentity> RemoveIdentity;

  [InjectDependency]
  private IExternalAuthenticationService _externalAuthenticationService { get; set; }

  [InjectDependency]
  private IOptions<ExternalAuthenticationOptions> _externalAuthenticationOptions { get; set; }

  [InjectDependency]
  private IPXLogin _pxLogin { get; set; }

  [InjectDependency]
  private ILoginAsUser _loginAsUser { get; set; }

  [InjectDependency]
  internal IUserManagementService UserManagementService { get; set; }

  [InjectDependency]
  private IRoleManagementService _roleManagementService { get; set; }

  [InjectDependency]
  private ILegacyCompanyService _legacyCompanyService { get; set; }

  [InjectDependency]
  protected ILogger _logger { get; set; }

  public bool IsSiteMapAltered { get; protected set; }

  [PXInternalUseOnly]
  public virtual void Initialize()
  {
    this.Views.Caches.Remove(typeof (UsersInRoles));
    this.Views.Caches.Add(typeof (UsersInRoles));
    PXUIFieldAttribute.SetDisplayName<RoleActiveDirectory.groupID>(this.ActiveDirectoryMap.Cache, "Group");
    PXUIFieldAttribute.SetDisplayName<RoleClaims.groupID>(this.ClaimsMap.Cache, "Group");
    PXUIFieldAttribute.SetVisible(this.ActiveDirectoryMap.Cache, (string) null, this.ActiveDirectoryProvider.IsEnabled());
    PXUIFieldAttribute.SetVisible(this.ClaimsMap.Cache, (string) null, this._externalAuthenticationService.ClaimsAuthEnabled());
    System.Type table = typeof (UserFilter);
    this.FieldVerifying.AddHandler(table, typeof (UserFilter.startIPAddress).Name, new PXFieldVerifying(this.UserFilterOnStartIPAddressFieldVerifying));
    this.FieldVerifying.AddHandler(table, typeof (UserFilter.endIPAddress).Name, new PXFieldVerifying(this.UserFilterOnEndIPAddressFieldVerifying));
    PXDatabase.Subscribe(typeof (UsersInRoles), (PXDatabaseTableChanged) (() => { }), nameof (Access));
    PXUIFieldAttribute.SetVisible<Users.username>(this.UserList.Cache, (object) null);
    PXUIFieldAttribute.SetVisible<UsersInRoles.applicationName>(this.RoleList.Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<UsersInRoles.username>(this.RoleList.Cache, (object) null, false);
    if (!this.ActiveDirectoryProvider.IsEnabled())
    {
      PXUIFieldAttribute.SetVisible<Users.domain>(this.UserList.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisibility<Users.domain>(this.UserList.Cache, (object) null, PXUIVisibility.Invisible);
      PXUIFieldAttribute.SetVisible<UsersInRoles.inherited>(this.RoleList.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisibility<UsersInRoles.inherited>(this.RoleList.Cache, (object) null, PXUIVisibility.Invisible);
    }
    this.RowSelected.RemoveHandler<Users>(new PXRowSelected(this.UserList.RowSelected));
    this.RowSelected.AddHandler<Users>(new PXRowSelected(this.UserList.RowSelected));
  }

  public Access()
  {
  }

  public Access(bool showCopyRole)
    : this()
  {
    this.CopyRole.SetVisible(showCopyRole);
  }

  public override object GetStateExt(string viewName, object data, string fieldName)
  {
    object stateExt = base.GetStateExt(viewName, data, fieldName);
    if ((viewName == "RoleList" && fieldName == "Username" || viewName == "RoleEntities" && fieldName == "RoleName" || viewName == "UsersByRole" && fieldName == "Rolename") && stateExt is PXFieldState pxFieldState)
    {
      pxFieldState.Visible = false;
      pxFieldState.Enabled = false;
    }
    return stateExt;
  }

  protected virtual void UserFilterOnStartIPAddressFieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null)
      return;
    string v = e.NewValue.ToString().Replace(" ", "");
    e.NewValue = UserFilter.DecodeIpAddress(v) != null ? (object) v : throw new Exception("Invalid IP address");
  }

  protected virtual void UserFilterOnEndIPAddressFieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null)
      return;
    string v = e.NewValue.ToString().Replace(" ", "");
    e.NewValue = UserFilter.DecodeIpAddress(v) != null ? (object) v : throw new Exception("Invalid IP address");
  }

  protected virtual IEnumerable syncRecs()
  {
    yield break;
  }

  public virtual bool IsWorkspace(Guid? workspaceID) => false;

  protected virtual void applyToChildren()
  {
    Role current = this.EntityRoles.Current;
    if ((current != null ? (!current.RoleRight.HasValue ? 1 : 0) : 1) != 0 || string.IsNullOrEmpty(current.ScreenID) && string.IsNullOrEmpty(current.RoleName))
      return;
    Guid? nodeId1 = current.NodeID;
    Guid nodeId2 = PXSiteMap.RootNode.NodeID;
    bool flag = nodeId1.HasValue && (!nodeId1.HasValue || nodeId1.GetValueOrDefault() == nodeId2);
    PXSiteMapNode rootNode = flag ? PXSiteMap.RootNode : PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(current.ScreenID);
    if (rootNode == null)
      return;
    this.ResetToInheritedForCacheAndMembers(rootNode, current);
    if (!flag)
      return;
    foreach (PXSiteMapNode node in PXSiteMap.Provider.Definitions.Nodes.ToList<PXSiteMapNode>())
      this.SetAccessRight(node, current);
    this.defGraph.View.Clear();
  }

  [PXUIField(DisplayName = "Copy Role", MapEnableRights = PXCacheRights.Insert, MapViewRights = PXCacheRights.Insert)]
  [PXButton(ConfirmationMessage = "Any unsaved changes will be discarded.", ConfirmationType = PXConfirmationType.IfDirty)]
  protected virtual IEnumerable copyRole(PXAdapter adapter)
  {
    if (this.Roles.Current?.Rolename != null && this.NewRole.AskExt() == WebDialogResult.OK && !string.IsNullOrEmpty(this.NewRole.Current?.Rolename))
    {
      string rolename1 = this.Roles.Current.Rolename;
      string rolename2 = this.NewRole.Current.Rolename;
      PX.SM.Roles copy = (PX.SM.Roles) this.Roles.Cache.CreateCopy((object) this.Roles.Current);
      copy.Rolename = rolename2;
      this.Roles.Cache.Insert((object) copy);
      this.CopyAccessRights(rolename1, rolename2);
      yield return (object) copy;
    }
  }

  private void CopyAccessRights(string currentRoleName, string newRoleName)
  {
    this.CopyRolesInCache(currentRoleName, newRoleName);
    this.CopyRolesInGraph(currentRoleName, newRoleName);
    this.CopyRolesInMember(currentRoleName, newRoleName);
  }

  private void CopyRolesInCache(string currentRoleName, string newRoleName)
  {
    foreach (PXResult<RolesInCache> pxResult in PXSelectBase<RolesInCache, PXSelect<RolesInCache, Where<RolesInCache.rolename, Equal<Required<PX.SM.Roles.rolename>>>>.Config>.Select((PXGraph) this, (object) currentRoleName))
    {
      short? accessrights = ((RolesInCache) pxResult).Accessrights;
      int? nullable = accessrights.HasValue ? new int?((int) accessrights.GetValueOrDefault()) : new int?();
      int num = -1;
      if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
        this.defCache.Cache.Insert((object) new RolesInCache()
        {
          ApplicationName = "/",
          Accessrights = ((RolesInCache) pxResult).Accessrights,
          Cachetype = ((RolesInCache) pxResult).Cachetype,
          ScreenID = ((RolesInCache) pxResult).ScreenID,
          Rolename = newRoleName
        });
    }
  }

  private void CopyRolesInGraph(string currentRoleName, string newRoleName)
  {
    foreach (PXResult<RolesInGraph> pxResult in PXSelectBase<RolesInGraph, PXSelect<RolesInGraph, Where<RolesInGraph.rolename, Equal<Required<PX.SM.Roles.rolename>>>>.Config>.Select((PXGraph) this, (object) currentRoleName))
      this.defGraph.Cache.Insert((object) new RolesInGraph()
      {
        ApplicationName = "/",
        Accessrights = ((RolesInGraph) pxResult).Accessrights,
        ScreenID = ((RolesInGraph) pxResult).ScreenID,
        Rolename = newRoleName
      });
  }

  private void CopyRolesInMember(string currentRoleName, string newRoleName)
  {
    foreach (PXResult<RolesInMember> pxResult in PXSelectBase<RolesInMember, PXSelect<RolesInMember, Where<RolesInMember.rolename, Equal<Required<PX.SM.Roles.rolename>>>>.Config>.Select((PXGraph) this, (object) currentRoleName))
    {
      short? accessrights = ((RolesInMember) pxResult).Accessrights;
      int? nullable = accessrights.HasValue ? new int?((int) accessrights.GetValueOrDefault()) : new int?();
      int num = -1;
      if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
        this.defMember.Cache.Insert((object) new RolesInMember()
        {
          Accessrights = ((RolesInMember) pxResult).Accessrights,
          Cachetype = ((RolesInMember) pxResult).Cachetype,
          ApplicationName = ((RolesInMember) pxResult).ApplicationName,
          Membername = ((RolesInMember) pxResult).Membername,
          ScreenID = ((RolesInMember) pxResult).ScreenID,
          Rolename = newRoleName
        });
    }
  }

  protected virtual void SetAccessRight(PXSiteMapNode node, Role role)
  {
    RolesInGraph rolesInGraph1 = (RolesInGraph) this.roleGraph.Select((object) node.ScreenID, (object) "/", (object) role.RoleName);
    if (rolesInGraph1 == null)
    {
      RolesInGraph instance = (RolesInGraph) this.roleGraph.Cache.CreateInstance();
      instance.ApplicationName = "/";
      instance.ScreenID = node.ScreenID;
      instance.Rolename = role.RoleName;
      rolesInGraph1 = (RolesInGraph) this.roleGraph.Cache.Locate((object) instance) ?? instance;
    }
    short? nullable1 = rolesInGraph1.Accessrights;
    int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    int? roleRight = role.RoleRight;
    int? nullable3 = roleRight.HasValue ? new int?((int) (short) roleRight.GetValueOrDefault()) : new int?();
    if (!(nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue))
    {
      RolesInGraph rolesInGraph2 = rolesInGraph1;
      nullable3 = role.RoleRight;
      short? nullable4;
      if (!nullable3.HasValue)
      {
        nullable1 = new short?();
        nullable4 = nullable1;
      }
      else
        nullable4 = new short?((short) nullable3.GetValueOrDefault());
      rolesInGraph2.Accessrights = nullable4;
      this.roleGraph.Cache.Update((object) rolesInGraph1);
    }
    this.ResetToInheritedForCacheAndMembers(node, role);
  }

  protected internal virtual void Role_RoleRight_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
  }

  protected internal virtual void Role_RoleRight_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    Role row = e.Row as Role;
    int? newValue1 = (int?) e.NewValue;
    int? nullable = newValue1;
    int num1 = -1;
    if (!(nullable.GetValueOrDefault() == num1 & nullable.HasValue) && !string.IsNullOrEmpty(row.MemberName))
      this.ValidateLowerAccessRights(row, cache, newValue1);
    if (cache.GetStatus((object) row) == PXEntryStatus.Notchanged)
      return;
    int? newValue2 = (int?) e.NewValue;
    int num2 = 2;
    if (!(newValue2.GetValueOrDefault() <= num2 & newValue2.HasValue) || row.CacheName != null || row.ScreenID != cache.Graph.Accessinfo.ScreenID.Replace(".", "") || !PXContext.PXIdentity.AuthUser.IsInRole(row.RoleName) || !PX.SM.Access.WillSelfLock(cache, row.RoleName) || this.Roles.Ask("Warning", "These changes will prevent you from accessing this form. Are you sure you want to continue?", MessageButtons.YesNo, MessageIcon.Warning) == WebDialogResult.Yes)
      return;
    e.Cancel = true;
    e.NewValue = (object) row.RoleRight;
  }

  private void ValidateLowerAccessRights(Role role, PXCache cache, int? newAccessRights)
  {
    short? cacheAccessRights = this.GetCacheAccessRights(role.ScreenID, role.CacheName, role.RoleName);
    int? nullable = cacheAccessRights.HasValue ? new int?((int) cacheAccessRights.GetValueOrDefault()) : new int?();
    int num = -1;
    if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
      return;
    if (cache.GetStatus((object) role) != PXEntryStatus.Notchanged)
      throw new PXException("Explicitly define upper-level rights first.");
    cache.RaiseExceptionHandling<Role.roleRight>((object) role, (object) newAccessRights, (Exception) new PXSetPropertyException<Role.roleRight>("Explicitly define upper-level rights first."));
  }

  protected internal virtual void Role_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    Role row = e.Row as Role;
    if (string.IsNullOrEmpty(row.MemberName))
      this.UpdateRolesInCache(row);
    else
      this.UpdateRolesInMember(row);
    this.UpdateParent(row);
  }

  private void UpdateRolesInCache(Role role)
  {
    RolesInCache rolesInCache = this.CreateRolesInCache(role);
    int? roleRight = role.RoleRight;
    int num = -1;
    if (roleRight.GetValueOrDefault() > num & roleRight.HasValue)
    {
      this.defCache.Cache.Update((object) rolesInCache);
    }
    else
    {
      if (this.defCache.Select((object) role.ScreenID, (object) role.CacheName, (object) "/") != null)
        this.defCache.Cache.Delete((object) rolesInCache);
      foreach (PXResult<RolesInMember> pxResult in this.defMemberCache.Select((object) role.ScreenID, (object) role.CacheName, (object) "/", (object) role.RoleName))
        this.defMember.Cache.Delete((object) (RolesInMember) pxResult);
    }
  }

  protected RolesInCache CreateRolesInCache(Role role)
  {
    RolesInCache rolesInCache = new RolesInCache();
    rolesInCache.ApplicationName = "/";
    rolesInCache.ScreenID = role.ScreenID;
    rolesInCache.Cachetype = role.CacheName;
    rolesInCache.Rolename = role.RoleName;
    int? roleRight = role.RoleRight;
    rolesInCache.Accessrights = roleRight.HasValue ? new short?((short) roleRight.GetValueOrDefault()) : new short?();
    rolesInCache.CreatedByID = role.CreatedByID;
    rolesInCache.CreatedByScreenID = role.CreatedByScreenID;
    rolesInCache.CreatedDateTime = role.CreatedDateTime;
    rolesInCache.LastModifiedByID = role.LastModifiedByID;
    rolesInCache.LastModifiedByScreenID = role.LastModifiedByScreenID;
    rolesInCache.LastModifiedDateTime = role.LastModifiedDateTime;
    return rolesInCache;
  }

  private void UpdateRolesInMember(Role role)
  {
    RolesInMember rolesInMember = this.CreateRolesInMember(role);
    int? roleRight1 = role.RoleRight;
    int num1 = -1;
    if (roleRight1.GetValueOrDefault() > num1 & roleRight1.HasValue)
    {
      int? roleRight2 = role.RoleRight;
      int num2 = 2;
      if (roleRight2.GetValueOrDefault() > num2 & roleRight2.HasValue)
        rolesInMember.Accessrights = new short?((short) 2);
      this.defMember.Cache.Update((object) rolesInMember);
    }
    else
    {
      if (this.defMember.Select((object) role.ScreenID, (object) role.CacheName, (object) role.MemberName, (object) "/") == null)
        return;
      this.defMember.Cache.Delete((object) rolesInMember);
    }
  }

  protected RolesInMember CreateRolesInMember(Role role)
  {
    RolesInMember rolesInMember = new RolesInMember();
    rolesInMember.ApplicationName = "/";
    rolesInMember.ScreenID = role.ScreenID;
    rolesInMember.Cachetype = role.CacheName;
    rolesInMember.Membername = role.MemberName;
    rolesInMember.Rolename = role.RoleName;
    int? roleRight = role.RoleRight;
    rolesInMember.Accessrights = roleRight.HasValue ? new short?((short) roleRight.GetValueOrDefault()) : new short?();
    rolesInMember.CreatedByID = role.CreatedByID;
    rolesInMember.CreatedByScreenID = role.CreatedByScreenID;
    rolesInMember.CreatedDateTime = role.CreatedDateTime;
    rolesInMember.LastModifiedByID = role.LastModifiedByID;
    rolesInMember.LastModifiedByScreenID = role.LastModifiedByScreenID;
    rolesInMember.LastModifiedDateTime = role.LastModifiedDateTime;
    return rolesInMember;
  }

  /// <summary>
  /// Sets Roles current to update the main view (Roles) because Audit requires this to save the batch correctly.
  /// </summary>
  protected void UpdateParent(Role role)
  {
    if (this.Roles.Current != null && this.Roles.Cache.GetStatus((object) this.Roles.Current) != PXEntryStatus.Notchanged)
      return;
    this.Roles.Current = (PX.SM.Roles) this.Roles.Search<PX.SM.Roles.applicationName, PX.SM.Roles.rolename>((object) "/", (object) role.RoleName);
  }

  internal void Role_RowPersisting(PXCache cache, PXRowPersistingEventArgs e) => e.Cancel = true;

  internal IEnumerable entities([PXGuid] Guid? NodeID, [PXString] string CacheName, [PXString] string MemberName)
  {
    return this.GetEntities(NodeID, CacheName, MemberName, true);
  }

  internal IEnumerable entitiesWithLeafs([PXGuid] Guid? NodeID, [PXString] string CacheName, [PXString] string MemberName)
  {
    return this.GetEntities(NodeID, CacheName, MemberName);
  }

  protected virtual IEnumerable GetEntities(
    Guid? NodeID,
    string CacheName,
    string MemberName,
    bool hideLeafs = false)
  {
    if (!string.IsNullOrEmpty(MemberName))
      return (IEnumerable) Enumerable.Empty<PXEntity>();
    if (!string.IsNullOrEmpty(CacheName))
      return this.GetEntitiesForDacAndElements(NodeID, CacheName, hideLeafs);
    if (NodeID.HasValue)
      return this.GetEntitiesForSiteMap(NodeID.Value, hideLeafs);
    return PXSiteMap.RootNode != null ? (IEnumerable) EnumerableExtensions.AsSingleEnumerable<PXEntity>(this.GetRootNodeEntity()) : (IEnumerable) Enumerable.Empty<PXEntity>();
  }

  private IEnumerable GetEntitiesForDacAndElements(Guid? NodeID, string CacheName, bool hideLeafs)
  {
    if (!(TypeInfoProvider.GetType(CacheName) == (System.Type) null))
    {
      PXSiteMapNode sitemap = PXSiteMap.Provider.FindSiteMapNodeFromKeyUnsecure(NodeID.Value);
      int order = 0;
      foreach (PXEntity entity in this.GetEntitiesForDac(sitemap, CacheName, hideLeafs))
      {
        yield return (object) entity;
        order = int.Parse(entity.OrderBy) + 1;
      }
      if (!hideLeafs)
      {
        foreach (object entitiesForDacElement in this.GetEntitiesForDacElements(sitemap, CacheName, order))
          yield return entitiesForDacElement;
      }
    }
  }

  private IEnumerable<PXEntity> GetEntitiesForDac(
    PXSiteMapNode sitemap,
    string cacheName,
    bool hideLeafs)
  {
    if (!string.IsNullOrEmpty(sitemap?.GraphType))
    {
      System.Type type1 = TypeInfoProvider.GetType(sitemap.GraphType);
      string customizedTypeFullName = CustomizedTypeManager.GetCustomizedTypeFullName(type1);
      System.Type type2;
      if (customizedTypeFullName != type1.FullName && (type2 = TypeInfoProvider.GetType(customizedTypeFullName)) != (System.Type) null)
        type1 = type2;
      if (!hideLeafs)
      {
        System.Type type3 = TypeInfoProvider.GetType(cacheName);
        int order = 0;
        foreach (PXActionInfo pxActionInfo in GraphHelper.GetActionsFiltered(type1, type3).Where<PXActionInfo>((Func<PXActionInfo, bool>) (action => !string.IsNullOrEmpty(action.Name))))
          yield return new PXEntity()
          {
            NodeID = sitemap.NodeID,
            CacheName = pxActionInfo.ViewType?.FullName ?? cacheName,
            MemberName = pxActionInfo.Name,
            Text = Str.NullIfWhitespace(pxActionInfo.DisplayName) ?? pxActionInfo.Name,
            OrderBy = order++.ToString("000000"),
            Icon = Sprite.Tree.GetFullUrl("Expand")
          };
      }
    }
  }

  private IEnumerable GetEntitiesForDacElements(PXSiteMapNode sitemap, string CacheName, int order)
  {
    PXCache cache = this.Caches[TypeInfoProvider.GetType(CacheName)];
    foreach (PXFieldInfo cacheField in GraphHelper.GetCacheFields(cache))
      yield return (object) new PXEntity()
      {
        NodeID = sitemap.NodeID,
        CacheName = CacheName,
        MemberName = cacheField.Name,
        Text = cacheField.DisplayName,
        OrderBy = order++.ToString("000000"),
        Icon = Sprite.Tree.GetFullUrl("Field")
      };
    if (!string.IsNullOrEmpty(sitemap.GraphType))
    {
      System.Type itemType = cache.GetItemType();
      System.Type cacheType = GraphHelper.GetPrimaryCache(sitemap.GraphType)?.CacheType;
      System.Type type = PXSubstManager.GetSubstitutedTypes(itemType, TypeInfoProvider.GetType(sitemap.GraphType)).FirstOrDefault<System.Type>((Func<System.Type, bool>) (t => t == cacheType));
      if ((object) type == null)
        type = itemType;
      System.Type table = type;
      if (!(table != cacheType))
      {
        KeyValueHelper.TableAttribute[] tableAttributeArray = KeyValueHelper.Def.GetAttributes(table);
        for (int index = 0; index < tableAttributeArray.Length; ++index)
        {
          KeyValueHelper.TableAttribute tableAttribute = tableAttributeArray[index];
          yield return (object) new PXEntity()
          {
            NodeID = sitemap.NodeID,
            CacheName = CacheName,
            MemberName = ("Attribute" + tableAttribute.AttributeID),
            Text = tableAttribute.Attribute.Description,
            OrderBy = order++.ToString("000000"),
            Icon = Sprite.Tree.GetFullUrl("Field")
          };
        }
        tableAttributeArray = (KeyValueHelper.TableAttribute[]) null;
      }
    }
  }

  private IEnumerable GetEntitiesForSiteMap(Guid nodeID, bool hideLeafs)
  {
    int i = 0;
    PXSiteMapNode nodeFromKeyUnsecure = PXSiteMap.Provider.FindSiteMapNodeFromKeyUnsecure(nodeID);
    if (nodeFromKeyUnsecure != null && !PXList.Provider.IsList(nodeFromKeyUnsecure.ScreenID) && !string.IsNullOrEmpty(nodeFromKeyUnsecure.GraphType))
    {
      foreach (PXCacheInfo pxCacheInfo in GraphHelper.GetGraphCaches(nodeFromKeyUnsecure.GraphType).Where<PXCacheInfo>((Func<PXCacheInfo, bool>) (cache => !hideLeafs || ((IEnumerable<object>) this.GetEntities(new Guid?(nodeID), cache.Name, (string) null)).Any<object>())))
        yield return (object) new PXEntity()
        {
          NodeID = nodeID,
          CacheName = pxCacheInfo.Name,
          Text = pxCacheInfo.DisplayName,
          OrderBy = i++.ToString("000000"),
          Icon = Sprite.Tree.GetFullUrl("Dac")
        };
    }
  }

  private PXEntity GetRootNodeEntity()
  {
    PXSiteMapNode rootNode = PXSiteMap.RootNode;
    PXEntity rootNodeEntity = new PXEntity()
    {
      NodeID = rootNode.NodeID,
      Text = rootNode.Title,
      OrderBy = "000000",
      Icon = Sprite.Tree.GetFullUrl("Folder")
    };
    if (string.IsNullOrEmpty(rootNodeEntity.Icon))
      return rootNodeEntity;
    string[] strArray = rootNodeEntity.Icon.Split('|');
    if (strArray.Length > 1)
      rootNodeEntity.Icon = strArray[1].Contains(".") ? strArray[1] : strArray[0];
    return rootNodeEntity;
  }

  protected void ResetToInheritedForCacheAndMembers(PXSiteMapNode rootNode, Role role)
  {
    foreach (PXResult<RolesInCache> pxResult in this.allCache.Select((object) rootNode.ScreenID, (object) role.RoleName))
      this.allCache.Cache.Delete((object) (RolesInCache) pxResult);
    foreach (PXResult<RolesInMember> pxResult in this.allMember.Select((object) rootNode.ScreenID, (object) role.RoleName))
      this.allMember.Cache.Delete((object) (RolesInMember) pxResult);
    role.InheritedByChildren = new bool?(true);
  }

  protected bool InheritedForCache(PXSiteMapNode rootNode, Role role)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    PX.SM.Access.\u003C\u003Ec__DisplayClass99_0 cDisplayClass990 = new PX.SM.Access.\u003C\u003Ec__DisplayClass99_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass990.role = role;
    // ISSUE: reference to a compiler-generated field
    int? roleRight = cDisplayClass990.role.RoleRight;
    int num = -1;
    if (roleRight.GetValueOrDefault() == num & roleRight.HasValue)
      return true;
    ParameterExpression parameterExpression;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: field reference
    // ISSUE: method reference
    return this.defMemberCache.Select((object) rootNode.ScreenID, (object) cDisplayClass990.role.CacheName, (object) "/", (object) cDisplayClass990.role.RoleName).All<PXResult<RolesInMember>>(Expression.Lambda<Func<PXResult<RolesInMember>, bool>>((Expression) Expression.Equal((Expression) Expression.Convert((Expression) Expression.Property((Expression) Expression.Call(m, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (RolesInMember.get_Accessrights))), typeof (int?)), (Expression) Expression.Property((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass990, typeof (PX.SM.Access.\u003C\u003Ec__DisplayClass99_0)), System.Reflection.FieldInfo.GetFieldFromHandle(__fieldref (PX.SM.Access.\u003C\u003Ec__DisplayClass99_0.role))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Role.get_RoleRight)))), parameterExpression));
  }

  protected bool InheritedForGraph(
    PXSiteMapNode node,
    Role role,
    Dictionary<string, bool> cachesDict)
  {
    if (role.CacheName != null)
      return this.InheritedForCache(node, role);
    return !cachesDict.ContainsKey(node.ScreenID + role.RoleName) || cachesDict[node.ScreenID + role.RoleName];
  }

  internal IEnumerable Roleentities([PXString] string path)
  {
    PX.SM.Access access = this;
    access.RoleEntities.Cache.Clear();
    if (!string.IsNullOrEmpty(access.Caches[typeof (PX.SM.Roles)].Current is PX.SM.Roles current ? current.Rolename : (string) null))
    {
      foreach (Role roleEntity in access.GetRoleEntities(current.Rolename, path))
      {
        Role role = access.RoleEntities.Insert(roleEntity);
        if (role != null && PXAccess.IsRoleEnabled(role.RoleName))
          yield return (object) role;
      }
      access.RoleEntities.Cache.IsDirty = false;
    }
  }

  protected internal virtual IEnumerable GetRoleEntities(string roleName, string path)
  {
    string[] strArray = Array.Empty<string>();
    int level = 0;
    if (path == null && this.Entities.Current != null && !PXGraph.ProxyIsActive)
      path = this.Entities.Current.Path;
    if (!string.IsNullOrEmpty(path))
    {
      strArray = path.Split('|');
      level = strArray.Length;
    }
    switch (level)
    {
      case 1:
        Guid nodeID1 = Guid.Parse(strArray[0]);
        return this.GetSiteMapRoleEntities(roleName, nodeID1);
      case 2:
      case 3:
        Guid nodeID2 = Guid.Parse(strArray[0]);
        string cacheName = strArray[1];
        string memberName = level == 3 ? strArray[2] : (string) null;
        return this.GetHighLevelRoleEntities(roleName, level, nodeID2, cacheName, memberName);
      default:
        return (IEnumerable) Enumerable.Empty<Role>();
    }
  }

  private IEnumerable GetSiteMapRoleEntities(string roleName, Guid nodeID)
  {
    PX.SM.Access graph = this;
    int order = 0;
    Dictionary<string, bool> cachesDict = graph.GetCachesDictionary();
    foreach (PXEntity entity in graph.GetEntities(new Guid?(nodeID), (string) null, (string) null))
    {
      PXSiteMapNode nodeFromKeyUnsecure = PXSiteMap.Provider.FindSiteMapNodeFromKeyUnsecure(entity.NodeID);
      if (!string.IsNullOrEmpty(nodeFromKeyUnsecure?.ScreenID))
      {
        Role siteMapRoleEntity = new Role()
        {
          NodeID = new Guid?(entity.NodeID),
          Level = new int?(2),
          ScreenID = nodeFromKeyUnsecure.ScreenID,
          CacheName = entity.CacheName,
          RoleName = roleName,
          RoleDescr = entity.Text,
          RoleRight = new int?((int) graph.GetGraphAccessRights(nodeFromKeyUnsecure.ScreenID, roleName)),
          DescriptionIcon = entity.Icon,
          OrderBy = order++.ToString("000000")
        };
        if (!string.IsNullOrEmpty(entity.CacheName))
        {
          Role role = siteMapRoleEntity;
          short? cacheAccessRights = graph.GetCacheAccessRights(nodeFromKeyUnsecure.ScreenID, entity.CacheName, roleName);
          int? nullable = cacheAccessRights.HasValue ? new int?((int) cacheAccessRights.GetValueOrDefault()) : new int?();
          role.RoleRight = nullable;
        }
        siteMapRoleEntity.RoleRight = new int?(ListRoleRight.SelectRightFromAccessed(siteMapRoleEntity, (PXGraph) graph));
        siteMapRoleEntity.InheritedByChildren = new bool?(graph.InheritedForGraph(nodeFromKeyUnsecure, siteMapRoleEntity, cachesDict));
        yield return (object) siteMapRoleEntity;
      }
    }
  }

  protected Dictionary<string, bool> GetCachesDictionary()
  {
    Dictionary<string, bool> cachesDictionary = new Dictionary<string, bool>();
    foreach (PXResult<RolesInCache> pxResult in PXSelectBase<RolesInCache, PXSelect<RolesInCache>.Config>.Select((PXGraph) this))
    {
      RolesInCache rolesInCache = (RolesInCache) pxResult;
      string key1 = rolesInCache.ScreenID + rolesInCache.Rolename;
      Dictionary<string, bool> dictionary = cachesDictionary;
      string key2 = key1;
      bool flag;
      int? nullable;
      int num1;
      if (!cachesDictionary.TryGetValue(key1, out flag))
      {
        short? accessrights = rolesInCache.Accessrights;
        nullable = accessrights.HasValue ? new int?((int) accessrights.GetValueOrDefault()) : new int?();
        int num2 = -1;
        num1 = nullable.GetValueOrDefault() == num2 & nullable.HasValue ? 1 : 0;
      }
      else if (flag)
      {
        short? accessrights = rolesInCache.Accessrights;
        nullable = accessrights.HasValue ? new int?((int) accessrights.GetValueOrDefault()) : new int?();
        int num3 = -1;
        num1 = nullable.GetValueOrDefault() == num3 & nullable.HasValue ? 1 : 0;
      }
      else
        num1 = 0;
      dictionary[key2] = num1 != 0;
    }
    return cachesDictionary;
  }

  private IEnumerable GetHighLevelRoleEntities(
    string roleName,
    int level,
    Guid nodeID,
    string cacheName,
    string memberName)
  {
    PX.SM.Access graph = this;
    PXSiteMapNode sitemap = PXSiteMap.Provider.FindSiteMapNodeFromKeyUnsecure(nodeID);
    if (!string.IsNullOrEmpty(sitemap?.ScreenID))
    {
      short? cacheAccessRights = graph.GetCacheAccessRights(sitemap.ScreenID, cacheName, roleName);
      int? nullable1 = cacheAccessRights.HasValue ? new int?((int) cacheAccessRights.GetValueOrDefault()) : new int?();
      int num1 = -1;
      bool found = !(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue);
      int order = 0;
      foreach (PXEntity entity in graph.GetEntities(new Guid?(nodeID), cacheName, (string) null))
      {
        if (level != 3 || !(entity.MemberName != memberName))
        {
          Role role1 = new Role();
          role1.NodeID = new Guid?(sitemap.NodeID);
          role1.Level = new int?(3);
          role1.ScreenID = sitemap.ScreenID;
          role1.CacheName = entity.CacheName;
          role1.MemberName = entity.MemberName;
          role1.RoleName = roleName;
          role1.RoleDescr = entity.Text;
          role1.RoleRight = new int?(-1);
          role1.DescriptionIcon = string.IsNullOrEmpty(entity.Icon) ? Sprite.Tree.GetFullUrl("Leaf") : entity.Icon;
          int num2 = order++;
          role1.OrderBy = num2.ToString("000000");
          Role r = role1;
          if (found)
          {
            short? memberAccessRights = graph.GetMemberAccessRights(sitemap.ScreenID, entity.CacheName, entity.MemberName, roleName);
            short? nullable2 = memberAccessRights;
            nullable1 = nullable2.HasValue ? new int?((int) nullable2.GetValueOrDefault()) : new int?();
            num2 = -1;
            if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
            {
              Role role2 = r;
              short? nullable3 = memberAccessRights;
              int? nullable4;
              if (!nullable3.HasValue)
              {
                nullable1 = new int?();
                nullable4 = nullable1;
              }
              else
                nullable4 = new int?((int) nullable3.GetValueOrDefault());
              role2.RoleRight = nullable4;
            }
          }
          r.RoleRight = new int?(ListRoleRight.SelectRightFromAccessed(r, (PXGraph) graph));
          r.InheritedByChildren = new bool?(true);
          yield return (object) r;
        }
      }
    }
  }

  internal void Roles_Guest_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    if ((bool) e.NewValue)
      return;
    foreach (PXResult<UsersInRoles> data in this.UsersByRole.Select())
    {
      Users users = (Users) PXSelectorAttribute.Select<UsersInRoles.username>(this.UsersByRole.Cache, (object) (UsersInRoles) data);
      int num;
      if (users == null)
      {
        num = 0;
      }
      else
      {
        bool? guest = users.Guest;
        bool flag = true;
        num = guest.GetValueOrDefault() == flag & guest.HasValue ? 1 : 0;
      }
      if (num != 0)
        throw new PXSetPropertyException("Only guest roles are allowed for a guest user.");
    }
  }

  internal void Roles_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    this.CopyRole.SetEnabled(this.Roles.Current != null && this.Roles.Cache.GetStatus((object) this.Roles.Current) != PXEntryStatus.Inserted && !string.IsNullOrEmpty(this.Roles.Current.Rolename));
  }

  [InjectDependency]
  [PXInternalUseOnly]
  public IActiveDirectoryProvider ActiveDirectoryProvider { get; set; }

  [InjectDependency]
  [PXInternalUseOnly]
  protected IOptions<PX.Data.Access.ActiveDirectory.Options> Options { get; set; }

  public static string[] GetRolesForADGroups(params string[] groups)
  {
    List<string> stringList = new List<string>();
    if (groups != null && groups.Length != 0)
    {
      foreach (string str in PX.SM.Access.RolesForADGroupsDefinition.Get(groups))
        stringList.Add(str);
    }
    return stringList.ToArray();
  }

  public static string[] GetADGroupsForRole(string roleName)
  {
    return PX.SM.Access.ADGroupsForRoleDefinition.Get(roleName).ToArray<string>();
  }

  protected virtual void RoleActiveDirectory_Role_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.Roles.Current == null)
      return;
    e.NewValue = (object) this.Roles.Current.Rolename;
  }

  protected virtual void RoleActiveDirectory_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    if (this.Roles.Current == null || !(e.Row is RoleActiveDirectory row) || !string.IsNullOrEmpty(row.Role))
      return;
    row.Role = this.Roles.Current.Rolename;
  }

  protected virtual void RoleActiveDirectory_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is RoleActiveDirectory row))
      return;
    Group group = this.ActiveDirectoryProvider.GetGroup(row.GroupID);
    if (group != null)
    {
      row.GroupName = group.DisplayName;
      row.GroupDomain = group.DC;
      row.GroupDescription = group.Description;
    }
    else
      row.GroupName = row.GroupID;
  }

  protected virtual void RoleClaims_Role_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.Roles.Current == null)
      return;
    e.NewValue = (object) this.Roles.Current.Rolename;
  }

  protected virtual void RoleClaims_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (this.Roles.Current == null || !(e.Row is RoleActiveDirectory row) || !string.IsNullOrEmpty(row.Role))
      return;
    row.Role = this.Roles.Current.Rolename;
  }

  protected virtual IEnumerable usersByRole()
  {
    PX.SM.Access graph = this;
    foreach (PXResult<UsersInRoles, Users> pxResult in PXSelectBase<UsersInRoles, PXSelectJoin<UsersInRoles, LeftJoin<Users, On<Users.username, Equal<UsersInRoles.username>>>, Where<UsersInRoles.applicationName, Equal<Current<PX.SM.Roles.applicationName>>, And<UsersInRoles.rolename, Equal<Current<PX.SM.Roles.rolename>>, And<UsersInRoles.username, PX.Data.IsNotNull, And<Users.isHidden, Equal<False>>>>>>.Config>.Select((PXGraph) graph))
    {
      Users users = (Users) pxResult;
      UsersInRoles usersInRoles = (UsersInRoles) pxResult;
      if (users.Username != null)
      {
        int? source = users.Source;
        int num = 1;
        if (!(source.GetValueOrDefault() == num & source.HasValue))
        {
          usersInRoles.DisplayName = users.DisplayName;
          usersInRoles.Comment = users.Comment;
          usersInRoles.State = users.State;
          yield return (object) usersInRoles;
          continue;
        }
      }
      switch (graph.UsersByRole.Cache.GetStatus((object) usersInRoles))
      {
        case PXEntryStatus.Updated:
        case PXEntryStatus.Inserted:
          yield return (object) usersInRoles;
          continue;
        default:
          PX.Data.Access.ActiveDirectory.User user = graph.ActiveDirectoryProvider.GetUser((object) usersInRoles.Username);
          if (user != null)
          {
            usersInRoles.DisplayName = user.Name.Name;
            usersInRoles.Domain = Users.GetDomains((IEnumerable<Login>) user.Name.Logins);
            usersInRoles.Comment = user.Comment;
            yield return (object) usersInRoles;
            continue;
          }
          continue;
      }
    }
    HashSet<string> overridenNames = new HashSet<string>((IEnumerable<string>) PXSelectBase<Users, PXSelect<Users, Where<Users.overrideADRoles, Equal<PX.Data.True>>>.Config>.Select((PXGraph) graph).Select<PXResult<Users>, string>((Expression<Func<PXResult<Users>, string>>) (x => ((Users) x).Username)));
    if (graph.Roles.Current != null)
    {
      List<string> stringList = new List<string>((IEnumerable<string>) PX.SM.Access.GetADGroupsForRole(graph.Roles.Current.Rolename));
      foreach (RoleActiveDirectory roleActiveDirectory in graph.ActiveDirectoryMap.Cache.Deleted)
        stringList.Remove(roleActiveDirectory.GroupID);
      foreach (RoleActiveDirectory roleActiveDirectory in graph.ActiveDirectoryMap.Cache.Inserted)
      {
        if (!stringList.Contains(roleActiveDirectory.GroupID))
          stringList.Add(roleActiveDirectory.GroupID);
      }
      HashSet<string> existingADUsers = PXSelectBase<Users, PXSelect<Users, Where<Users.source, Equal<PXUsersSourceListAttribute.activeDirectory>>>.Config>.Select((PXGraph) graph).RowCast<Users>().Select<Users, string>((Func<Users, string>) (u => u.ExtRef)).Distinct<string>().ToHashSet<string>();
      foreach (PX.Data.Access.ActiveDirectory.User usersByGroupId in graph.ActiveDirectoryProvider.GetUsersByGroupIDs(stringList.ToArray()))
      {
        if (!overridenNames.Contains(usersByGroupId.Name.DomainLogin) && existingADUsers.Contains(usersByGroupId.SID))
        {
          graph.UserManagementService.GetUser(usersByGroupId.Name.DomainLogin);
          yield return (object) new UsersInRoles()
          {
            Username = usersByGroupId.Name.DomainLogin,
            ApplicationName = graph.Roles.Current.ApplicationName,
            Rolename = graph.Roles.Current.Rolename,
            DisplayName = usersByGroupId.Name.Name,
            Domain = Users.GetDomains((IEnumerable<Login>) usersByGroupId.Name.Logins),
            Comment = usersByGroupId.Comment,
            State = "A",
            Inherited = new bool?(true)
          };
        }
      }
      existingADUsers = (HashSet<string>) null;
    }
  }

  internal virtual IEnumerable generalInfo()
  {
    PX.SM.Access.AccessInfoNotification infoNotification = new PX.SM.Access.AccessInfoNotification();
    infoNotification.ScreenID = PXContext.GetScreenID();
    infoNotification.UserName = PXAccess.GetUserName();
    infoNotification.DisplayName = PXAccess.GetUserDisplayName();
    infoNotification.UserID = PXAccess.GetUserID();
    infoNotification.BranchID = PXAccess.GetBranchID();
    infoNotification.CompanyName = PXDatabase.Provider.GetCompanyDisplayName();
    System.DateTime now = PXTimeZoneInfo.Now;
    infoNotification.BusinessDate = PXContext.GetBusinessDate();
    if (!infoNotification.BusinessDate.HasValue)
      infoNotification.BusinessDate = new System.DateTime?(new System.DateTime(now.Year, now.Month, now.Day));
    yield return (object) infoNotification;
  }

  protected virtual void Users_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    Users row1 = (Users) e.Row;
    if (e.Row == null)
      return;
    row1.OldPassword = row1.OldPassword ?? string.Empty;
    row1.NewPassword = row1.NewPassword ?? string.Empty;
    row1.ConfirmPassword = row1.ConfirmPassword ?? string.Empty;
    int? source = row1.Source;
    int num1 = 2;
    bool flag1 = source.GetValueOrDefault() == num1 & source.HasValue;
    PXCache cache = sender;
    object row2 = e.Row;
    bool? passwordChangeable = row1.PasswordChangeable;
    bool flag2 = true;
    int num2 = passwordChangeable.GetValueOrDefault() == flag2 & passwordChangeable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<Users.passwordChangeOnNextLogin>(cache, row2, num2 != 0);
    PXUIFieldAttribute.SetEnabled<Users.isOnLine>(sender, e.Row, false);
    PXUIFieldAttribute.SetEnabled<Users.lockedOutDate>(sender, e.Row, false);
    PXUIFieldAttribute.SetEnabled<Users.lastActivityDate>(sender, e.Row, false);
    PXUIFieldAttribute.SetEnabled<Users.lastLoginDate>(sender, e.Row, false);
    PXUIFieldAttribute.SetEnabled<Users.lastLockedOutDate>(sender, e.Row, false);
    PXUIFieldAttribute.SetVisible<Users.lastLockedOutDate>(sender, e.Row, !flag1);
    PXUIFieldAttribute.SetEnabled<Users.lastPasswordChangedDate>(sender, e.Row, false);
    PXUIFieldAttribute.SetVisible<Users.lastPasswordChangedDate>(sender, e.Row, !flag1);
    PXUIFieldAttribute.SetEnabled<Users.failedPasswordAnswerAttemptCount>(sender, e.Row, false);
    PXUIFieldAttribute.SetVisible<Users.failedPasswordAnswerAttemptCount>(sender, e.Row, !flag1);
    PXUIFieldAttribute.SetEnabled<Users.failedPasswordAttemptCount>(sender, e.Row, false);
    PXUIFieldAttribute.SetVisible<Users.failedPasswordAttemptCount>(sender, e.Row, !flag1);
    PXUIFieldAttribute.SetEnabled<Users.creationDate>(sender, e.Row, false);
    PXUIFieldAttribute.SetVisible<Users.creationDate>(sender, e.Row, !flag1);
    bool flag3 = sender.GetStatus((object) row1) == PXEntryStatus.Inserted;
    bool flag4 = row1 != null && !string.IsNullOrEmpty(row1.Username) && !flag3 && row1.Username == this.Accessinfo.UserName;
    if (this._loginAsUser.TryGetLoggedAsUserNameFromCurrentContext() != null)
      this.LoginAsUser.SetEnabled(false);
    else if (!flag3 && !flag4)
      this.LoginAsUser.SetEnabled(this.UserHasRoles(row1.Username));
    else
      this.LoginAsUser.SetEnabled(false);
    this.DeleteUsers.SetEnabled(!flag3 && !flag4);
    this.RestrictionGroupsByUser.SetVisible(!flag3);
    this.RemoveIdentity.SetEnabled(this.Identities.SelectSingle() != null);
  }

  protected virtual void Users_MultiFactorType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) SitePolicy.MultiFactorAuthLevel;
    e.Cancel = true;
  }

  protected virtual void Users_MultiFactorOverride_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    Users row = (Users) e.Row;
    if ((e.NewValue == null ? 0 : (Convert.ToBoolean(e.NewValue) ? 1 : 0)) != 0)
      return;
    row.MultiFactorType = new int?(SitePolicy.MultiFactorAuthLevel);
  }

  [PXDBInt]
  [PXSelector(typeof (Search<Branch.branchID, Where<MatchWithBranch<Branch.branchID>>>), new System.Type[] {typeof (Branch.branchCD), typeof (Branch.roleName)}, DescriptionField = typeof (Branch.branchCD))]
  [PXUIField(DisplayName = "Default Branch")]
  protected virtual void UserPreferences_DefBranchID_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(DisplayName = "Sign In as User", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  public IEnumerable loginAsUser(PXAdapter adapter)
  {
    Users current = this.UserList.Current;
    if (string.IsNullOrEmpty(current?.Username))
      return adapter.Get();
    MembershipUser user = this.UserManagementService.GetUser(current.Username);
    if (user == null)
      return adapter.Get();
    string str1 = "";
    PXSessionContext pxIdentity = PXContext.PXIdentity;
    if (pxIdentity.AuthUser != null)
    {
      str1 = this._legacyCompanyService.ExtractCompanyWithBranch(pxIdentity.AuthUser.Identity.Name) ?? string.Empty;
      if (str1.Length > 0)
        str1 = "@" + str1;
    }
    string userName = user.UserName;
    string str2 = userName + str1;
    this._loginAsUser.LoginAsUser(str2, this.Accessinfo.UserName, HttpContext.Current.Session.ToLoginAsUserSession());
    PXContext.Session.SetString("ChangingPassword", (string) null);
    PXAuditJournal.Register(PXAuditJournal.Operation.Login, userName);
    string str3 = LocaleInfo.GetCulture().ToString();
    this._pxLogin.InitUserEnvironment(str2, str3 ?? "en-US");
    PXContext.PXIdentity.SetUser((IPrincipal) new GenericPrincipal((IIdentity) new GenericIdentity(str2), this._roleManagementService.GetRolesForUser(str2)));
    foreach (PXCache pxCache in this.Caches.Values.Where<PXCache>((Func<PXCache, bool>) (c => c.IsDirty)))
      pxCache.IsDirty = false;
    throw new PXRefreshException();
  }

  [PXButton]
  [PXUIField(DisplayName = "Membership", MapViewRights = PXCacheRights.Select, MapEnableRights = PXCacheRights.Select)]
  protected virtual void restrictionGroupsByUser()
  {
    UserAccess instance = PXGraph.CreateInstance<UserAccess>();
    instance.User.Current = (Users) instance.User.Search<Users.username>((object) this.UserList.Current.Username);
    throw new PXRedirectRequiredException((PXGraph) instance, "Membership");
  }

  protected virtual void sendUserNotification<NotificationTemplate>() where NotificationTemplate : IBqlField
  {
    if (this.UserList.Current == null)
      return;
    bool? nullable = this.UserList.Current.IsApproved;
    bool flag1 = true;
    if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue) || string.IsNullOrEmpty(this.UserList.Current.Email))
      return;
    PreferencesEmail emailPreferences = MailAccountManager.GetEmailPreferences();
    PXCache cach = this.Caches[typeof (PreferencesEmail)];
    if (cach.GetValue<NotificationTemplate>((object) emailPreferences) == null)
      return;
    Notification notification;
    try
    {
      notification = (Notification) PXSelectBase<Notification, PXSelect<Notification, Where<Notification.notificationID, Equal<Required<Notification.notificationID>>>>.Config>.Select((PXGraph) this, cach.GetValue<NotificationTemplate>((object) emailPreferences));
    }
    catch (Exception ex)
    {
      return;
    }
    if (notification == null)
      throw new PXException("The notification has not been found.");
    int? accountId = notification.NFrom ?? MailAccountManager.DefaultSystemMailAccountID;
    try
    {
      if (this.UserList.Current.RecoveryLink == null)
      {
        nullable = this.UserList.Current.Guest;
        bool flag2 = true;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
        {
          PreferencesGeneral preferencesGeneral = (PreferencesGeneral) PXSelectBase<PreferencesGeneral, PXSelect<PreferencesGeneral>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object[]) null);
          if (PortalHelper.IsPortalContext(PortalContexts.Modern))
            this.UserList.Current.RecoveryLink = PortalHelper.GetPortal().Url;
          else if (!string.IsNullOrEmpty(preferencesGeneral?.PortalExternalAccessLink))
            this.UserList.Current.RecoveryLink = preferencesGeneral.PortalExternalAccessLink;
          else
            this.UserList.Current.RecoveryLink = emailPreferences.NotificationSiteUrl ?? PXUrl.SiteUrlWithPath();
        }
        else
          this.UserList.Current.RecoveryLink = emailPreferences.NotificationSiteUrl ?? PXUrl.SiteUrlWithPath();
      }
      if (!string.IsNullOrEmpty(this.UserList.Current.RecoveryLink))
      {
        if (PXDatabase.Companies.Length != 0)
        {
          string companyName = PXAccess.GetCompanyName();
          if (!string.IsNullOrEmpty(companyName))
            this.UserList.Current.RecoveryLink = PXUrl.AppendUrlParameter(this.UserList.Current.RecoveryLink.TrimEnd('/'), "CompanyID", companyName);
        }
      }
    }
    catch
    {
    }
    try
    {
      this.SendUserNotification(accountId, notification);
    }
    catch (StackOverflowException ex)
    {
      throw;
    }
    catch (OutOfMemoryException ex)
    {
      throw;
    }
    catch
    {
    }
  }

  protected virtual void sendUserWelcome()
  {
    if (this.UserList.Current == null)
      return;
    bool? guest = this.UserList.Current.Guest;
    bool flag = true;
    if (guest.GetValueOrDefault() == flag & guest.HasValue && PortalHelper.IsPortalContext(PortalContexts.Modern))
      this.sendUserNotification<PreferencesEmail.portalUserWelcomeNotificationId>();
    else
      this.sendUserNotification<PreferencesEmail.userWelcomeNotificationId>();
  }

  protected virtual void sendChangedPassword()
  {
    this.sendUserNotification<PreferencesEmail.passwordChangedNotificationId>();
  }

  protected virtual void sendUserLogin()
  {
    if (this.UserList.Current == null)
      return;
    bool? isApproved = this.UserList.Current.IsApproved;
    bool flag = true;
    if (!(isApproved.GetValueOrDefault() == flag & isApproved.HasValue))
      return;
    if (string.IsNullOrEmpty(this.UserList.Current.Email))
      throw new PXException("No email address is specified in your account.");
    if (MailAccountManager.Sender == null)
      throw new PXException("The email account is empty. Please define the Default Email Account in Email Preferences.");
    PXResultset<Notification> pxResultset = PXSelectBase<Notification, PXSelectJoin<Notification, InnerJoin<PreferencesEmail, On<Notification.notificationID, Equal<PreferencesEmail.loginRecoveryNotificationId>>>>.Config>.Select((PXGraph) this);
    int? systemMailAccountId = MailAccountManager.DefaultSystemMailAccountID;
    if (!MailAccountManager.GetEmailPreferences().LoginRecoveryNotificationId.HasValue)
      throw new PXException("A notification for login recovery has not been configured.");
    if (pxResultset == null)
      throw new PXException("A notification for login recovery has not been found.");
    this.SendUserNotification(systemMailAccountId, (Notification) pxResultset);
  }

  protected virtual void sendUserPassword()
  {
    Users current = this.UserList.Current;
    if (current == null)
      return;
    bool? nullable = current.AllowPasswordRecovery;
    bool flag1 = true;
    if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue))
      throw new PXPasswordRecoveryException(PXPasswordRecoveryException.ErrorCode.PasswordRecoveryDisabled);
    nullable = current.IsApproved;
    bool flag2 = true;
    if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
      throw new PXPasswordRecoveryException(PXPasswordRecoveryException.ErrorCode.InactiveUserAccount);
    if (string.IsNullOrEmpty(current.Email))
      throw new PXPasswordRecoveryException(PXPasswordRecoveryException.ErrorCode.NoEmailInUserAccount);
    if (MailAccountManager.Sender == null)
      throw new PXPasswordRecoveryException(PXPasswordRecoveryException.ErrorCode.EmailSenderNotConfigured);
    nullable = current.Guest;
    bool flag3 = true;
    Notification notification = !(nullable.GetValueOrDefault() == flag3 & nullable.HasValue) || !PortalHelper.IsPortalContext(PortalContexts.Modern) ? (Notification) PXSelectBase<Notification, PXSelectJoin<Notification, InnerJoin<PreferencesEmail, On<Notification.notificationID, Equal<PreferencesEmail.passwordRecoveryNotificationId>>>>.Config>.Select((PXGraph) this) : (Notification) PXSelectBase<Notification, PXSelectJoin<Notification, InnerJoin<PreferencesEmail, On<Notification.notificationID, Equal<PreferencesEmail.portalPasswordRecoveryNotificationId>>>>.Config>.Select((PXGraph) this);
    int? systemMailAccountId = MailAccountManager.DefaultSystemMailAccountID;
    if (!MailAccountManager.GetEmailPreferences().PasswordRecoveryNotificationId.HasValue)
      throw new PXPasswordRecoveryException(PXPasswordRecoveryException.ErrorCode.PasswordRecoveryNotificationIsNotConfigured);
    if (notification == null)
      throw new PXPasswordRecoveryException(PXPasswordRecoveryException.ErrorCode.PasswordRecoveryNotificationNotFound);
    this.SendUserNotification(systemMailAccountId, notification);
  }

  protected virtual void SendUserNotification(int? accountId, Notification notification)
  {
    string email = this.UserList.Current.Email;
    string subject;
    string body;
    using (!string.IsNullOrEmpty(notification.LocaleName) ? new PXCultureScope(new CultureInfo(notification.LocaleName)) : (PXCultureScope) null)
    {
      subject = PXTemplateContentParser.Instance.Process(notification.Subject, (PXGraph) this, typeof (Users), (object[]) null);
      body = PXTemplateContentParser.ScriptInstance.Process(notification.Body, (PXGraph) this, typeof (Users), (object[]) null);
    }
    NotificationSenderProvider.Notify(new EmailNotificationParameters()
    {
      EmailAccountID = accountId,
      To = email,
      Subject = subject,
      Body = PX.SM.Access.CreateHtml(subject, body)
    });
  }

  private static string CreateHtml(string subject, string body)
  {
    return $"<html><head><title>{subject}</title></head><body>{body}</body></html>";
  }

  protected virtual IEnumerable identities()
  {
    if (this.UserListCurrent != null && this.UserListCurrent.Current != null && this.UserListCurrent.Current.PKID.HasValue && this.UserListCurrent.Current.Username != null)
    {
      this.UserListCurrent.Current.Source.GetValueOrDefault();
      yield break;
    }
  }

  [PXButton]
  [PXUIField(DisplayName = "")]
  public IEnumerable removeIdentity(PXAdapter adapter) => adapter.Get();

  protected virtual void UserIdentity_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is UserIdentity row))
      return;
    PXUIFieldAttribute.SetEnabled<UserIdentity.active>(sender, (object) row, row.Enabled.GetValueOrDefault());
    if (this.UserListCurrent.Current == null)
      return;
    int? source = this.UserListCurrent.Current.Source;
    int num = 0;
    if (source.GetValueOrDefault() == num & source.HasValue)
      return;
    PXUIFieldAttribute.SetEnabled<UserIdentity.active>(sender, (object) row, false);
    PXUIFieldAttribute.SetEnabled<UserIdentity.userKey>(sender, (object) row, false);
  }

  protected virtual void UserIdentity_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    UserIdentity row = e.Row as UserIdentity;
    bool? databased = row.Databased;
    if (!(databased.HasValue ? new bool?(!databased.GetValueOrDefault()) : new bool?()).GetValueOrDefault())
      return;
    sender.SetStatus((object) row, PXEntryStatus.Inserted);
  }

  protected virtual void UserIdentity_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (!(e.Row is UserIdentity row))
      return;
    sender.SetStatus((object) row, PXEntryStatus.Held);
  }

  public override void Persist()
  {
    this.IsSiteMapAltered = true;
    base.Persist();
    this.ClearDependencies();
  }

  public virtual void ClearDependencies()
  {
    PXDatabase.SelectTimeStamp();
    PXAccess.Clear();
    PXSiteMap.Provider.Clear();
    this.ActiveDirectoryProvider.Reset();
    PXPageCacheUtils.InvalidateCachedPages();
  }

  public static Guid? GetGuidFromDeletedUser(string userName)
  {
    using (new PXReadDeletedScope())
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Users>(new PXDataField(typeof (Users.pKID).Name), (PXDataField) new PXDataFieldValue(typeof (Users.username).Name, PXDbType.VarChar, new int?((int) byte.MaxValue), (object) userName)))
        return (Guid?) pxDataRecord?.GetGuid(0);
    }
  }

  internal static void CheckPasswords(
    IUserValidationService userValidationService,
    bool checkOld,
    Users current)
  {
    if (checkOld && !UserValidationServiceExtensions.CheckUserPassword(userValidationService, current.Username, current.OldPassword))
      throw new PXSetPropertyException("Incorrect old password");
    if (current.NewPassword != current.ConfirmPassword)
      throw new PXSetPropertyException("The entered password doesn't match the confirmation.");
  }

  [PXInternalUseOnly]
  public static void SetPassword(
    IUserManagementService userManagementService,
    bool checkOldPwd,
    Users current)
  {
    PX.SM.Access.SetPassword(userManagementService, checkOldPwd, true, current);
  }

  internal static void SetPassword(
    IUserManagementService userManagementService,
    bool checkOldPwd,
    bool isCheck,
    Users current)
  {
    if (isCheck)
      PX.SM.Access.CheckPasswords((IUserValidationService) userManagementService, checkOldPwd, current);
    MembershipUser user = userManagementService.GetUser(current.PKID.Value);
    if (user == null)
      throw new PXSetPropertyException("The user has not been found or is locked out. The password has not been reset.");
    System.DateTime today;
    string str1;
    if (!checkOldPwd)
    {
      string newPassword = current.NewPassword;
      today = System.DateTime.Today;
      string str2 = today.DayOfWeek.ToString();
      str1 = $"{newPassword}#{str2}";
    }
    else
      str1 = current.OldPassword;
    string oldPassword = str1;
    bool? nullable1 = current.GeneratePassword;
    bool flag = true;
    if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
    {
      string str3 = oldPassword;
      today = System.DateTime.Today;
      string str4 = today.DayOfYear.ToString();
      oldPassword = $"{str3}:{str4}";
    }
    user.ChangePassword(oldPassword, current.NewPassword);
    current.OldPassword = (string) null;
    current.NewPassword = (string) null;
    current.ConfirmPassword = (string) null;
    Users users = current;
    nullable1 = new bool?();
    bool? nullable2 = nullable1;
    users.GeneratePassword = nullable2;
  }

  public static bool WillSelfLock(PXCache sender, string currentRolename)
  {
    PXResultset<UsersInRoles> pxResultset1 = PXSelectBase<UsersInRoles, PXSelect<UsersInRoles, Where<UsersInRoles.username, Equal<Required<UsersInRoles.username>>>>.Config>.Select(sender.Graph, (object) PXAccess.GetUserName());
    PXResultset<RolesInGraph> pxResultset2 = PXSelectBase<RolesInGraph, PXSelect<RolesInGraph, Where<RolesInGraph.screenID, Equal<Required<RolesInGraph.screenID>>, And<RolesInGraph.accessrights, Greater<Required<RolesInGraph.accessrights>>>>>.Config>.Select(sender.Graph, (object) sender.Graph.Accessinfo.ScreenID.Replace(".", ""), (object) 2);
    foreach (PXResult<UsersInRoles> pxResult1 in pxResultset1)
    {
      UsersInRoles usersInRoles = (UsersInRoles) pxResult1;
      foreach (PXResult<RolesInGraph> pxResult2 in pxResultset2)
      {
        RolesInGraph rolesInGraph = (RolesInGraph) pxResult2;
        if (rolesInGraph.Rolename != currentRolename && rolesInGraph.Rolename == usersInRoles.Rolename)
          return false;
      }
    }
    return true;
  }

  protected short GetGraphAccessRights(string screenId, string roleName)
  {
    RolesInGraph rolesInGraph = NonGenericIEnumerableExtensions.Concat_(this.roleGraph.Cache.Updated, this.roleGraph.Cache.Inserted).Cast<RolesInGraph>().FirstOrDefault<RolesInGraph>((Func<RolesInGraph, bool>) (rg => screenId.Equals(rg.ScreenID, StringComparison.OrdinalIgnoreCase) && roleName.Equals(rg.Rolename, StringComparison.OrdinalIgnoreCase)));
    if (rolesInGraph != null && rolesInGraph.Accessrights.HasValue)
      return rolesInGraph.Accessrights.Value;
    Dictionary<string, short> dictionary;
    short num;
    return !PXDatabase.GetSlot<AccessRightsDefinition>("AccessRightsDefinition", typeof (RolesInGraph)).AccessRights.TryGetValue(screenId, out dictionary) || !dictionary.TryGetValue(roleName, out num) && !dictionary.TryGetValue("*", out num) ? (short) 0 : num;
  }

  protected short? GetCacheAccessRights(string screenId, string cacheTypeName, string roleName)
  {
    return new short?((short) ((int) this.roleCache.SelectSingle((object) screenId, (object) cacheTypeName, (object) "/", (object) roleName)?.Accessrights ?? -1));
  }

  protected short? GetMemberAccessRights(
    string screenId,
    string cacheTypeName,
    string memberName,
    string roleName)
  {
    return new short?((short) ((int) this.roleMember.SelectSingle((object) screenId, (object) cacheTypeName, (object) memberName, (object) "/", (object) roleName)?.Accessrights ?? -1));
  }

  private bool UserHasRoles(string username)
  {
    try
    {
      string[] rolesForUser = this._roleManagementService.GetRolesForUser(username);
      return rolesForUser != null && rolesForUser.Length != 0;
    }
    catch (Exception ex)
    {
      this._logger.Warning<string>(ex, "Unable to get roles for user {Username}", username);
      return false;
    }
  }

  [Serializable]
  public class AccessInfoNotification : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected bool _CuryViewState;

    [PXGuid]
    [PXUIField(DisplayName = "User ID")]
    public virtual Guid UserID { get; set; }

    [PXString(256 /*0x0100*/, IsUnicode = true)]
    [PXUIField(DisplayName = "User Name")]
    public virtual string UserName { get; set; }

    [PXString(IsUnicode = true)]
    [PXUIField(DisplayName = "Display Name")]
    public virtual string DisplayName { get; set; }

    [PXString(IsUnicode = true)]
    [PXUIField(DisplayName = "Company Name")]
    public virtual string CompanyName { get; set; }

    [PXDate]
    [PXUIField(DisplayName = "Business Date")]
    public virtual System.DateTime? BusinessDate { get; set; }

    [PXString(5, IsUnicode = true)]
    [PXUIField(DisplayName = "Base Currency ID")]
    public virtual string BaseCuryID { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Screen ID")]
    public virtual string ScreenID { get; set; }

    [PXBool]
    [PXUIField(DisplayName = "Cury View State")]
    public virtual bool CuryViewState { get; set; }

    [PXInt]
    [PXUIField(DisplayName = "Cury Rate ID")]
    public virtual int? CuryRateID { get; set; }

    [PXInt]
    [PXUIField(DisplayName = "Branch")]
    public virtual int? BranchID { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Site Url")]
    public virtual string NotificationSiteUrl { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Link Entity")]
    public virtual string LinkEntity { get; set; }

    [PXString(IsUnicode = true)]
    [PXUIField(DisplayName = "Email Signature")]
    public virtual string MailSignature { get; set; }

    public abstract class userID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      PX.SM.Access.AccessInfoNotification.userID>
    {
    }

    public abstract class userName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PX.SM.Access.AccessInfoNotification.userName>
    {
    }

    public abstract class displayName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PX.SM.Access.AccessInfoNotification.displayName>
    {
    }

    public abstract class companyName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PX.SM.Access.AccessInfoNotification.companyName>
    {
    }

    public abstract class businessDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      PX.SM.Access.AccessInfoNotification.businessDate>
    {
    }

    public abstract class baseCuryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PX.SM.Access.AccessInfoNotification.baseCuryID>
    {
    }

    public abstract class screenID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PX.SM.Access.AccessInfoNotification.screenID>
    {
    }

    public abstract class curyViewState : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PX.SM.Access.AccessInfoNotification.curyViewState>
    {
    }

    public abstract class curyRateID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PX.SM.Access.AccessInfoNotification.curyRateID>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PX.SM.Access.AccessInfoNotification.branchID>
    {
    }

    public abstract class notificationSiteUrl : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PX.SM.Access.AccessInfoNotification.notificationSiteUrl>
    {
    }

    public abstract class linkEntity : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PX.SM.Access.AccessInfoNotification.linkEntity>
    {
    }

    public abstract class mailSignature : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PX.SM.Access.AccessInfoNotification.mailSignature>
    {
    }
  }

  [Serializable]
  public class NewRoleFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(64 /*0x40*/, IsUnicode = true, InputMask = "")]
    [PXDefault]
    [PXUIField(DisplayName = "New Role Name", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string Rolename { get; set; }
  }

  public class RolesForADGroupsDefinition : IPrefetchable<string[]>, IPXCompanyDependent
  {
    private const string _SLOT_KEY = "_ROLES_FOR_AD_GROUPS_DEFINITION_";
    private IEnumerable<string> _roles;

    public static IEnumerable<string> Get(params string[] groups)
    {
      IEnumerable<string> strings = (IEnumerable<string>) null;
      if (groups != null && groups.Length != 0)
        strings = PXDatabase.GetSlot<PX.SM.Access.RolesForADGroupsDefinition, string[]>("_ROLES_FOR_AD_GROUPS_DEFINITION_" + string.Join("$", groups), groups, typeof (RoleActiveDirectory), typeof (PX.SM.Roles)).With<PX.SM.Access.RolesForADGroupsDefinition, IEnumerable<string>>((Func<PX.SM.Access.RolesForADGroupsDefinition, IEnumerable<string>>) (_ => _._roles));
      return strings ?? (IEnumerable<string>) new string[0];
    }

    public void Prefetch(string[] groups)
    {
      if (groups == null || groups.Length == 0)
        return;
      using (new PXConnectionScope())
        this._roles = (IEnumerable<string>) ((IEnumerable<string>) groups).SelectMany((Func<string, IEnumerable<string>>) (groupId => PXDatabase.SelectMulti<RoleActiveDirectory>((PXDataField) new PXDataField<RoleActiveDirectory.role>(), (PXDataField) new PXDataFieldValue<RoleActiveDirectory.groupID>(PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) groupId)).Select<PXDataRecord, string>((Func<PXDataRecord, string>) (roleRecord => roleRecord.With<PXDataRecord, string>((Func<PXDataRecord, string>) (_ => _.GetString(0))))).Where<string>((Func<string, bool>) (roleName => !string.IsNullOrEmpty(roleName)))), (groupId, roleName) => new
        {
          groupId = groupId,
          roleName = roleName
        }).ToList().SelectMany(t => PXDatabase.SelectMulti<PX.SM.Roles>((PXDataField) new PXDataField<PX.SM.Roles.rolename>(), (PXDataField) new PXDataFieldValue<PX.SM.Roles.rolename>(PXDbType.NVarChar, new int?(64 /*0x40*/), (object) t.roleName)).Select<PXDataRecord, string>((Func<PXDataRecord, string>) (roleRecord => roleRecord.With<PXDataRecord, string>((Func<PXDataRecord, string>) (_ => _.GetString(0))))).Where<string>((Func<string, bool>) (role => !string.IsNullOrEmpty(role)))).ToList<string>();
    }
  }

  protected class ADGroupsForRoleDefinition : IPrefetchable<string>, IPXCompanyDependent
  {
    private const string _SLOT_KEY = "_AD_GROUPS_FOR_ROLE_DEFINITION_";
    private IEnumerable<string> _groups;

    public static IEnumerable<string> Get(string roleName)
    {
      IEnumerable<string> strings = (IEnumerable<string>) null;
      if (!string.IsNullOrEmpty(roleName))
        strings = PXDatabase.GetSlot<PX.SM.Access.ADGroupsForRoleDefinition, string>($"_AD_GROUPS_FOR_ROLE_DEFINITION_${roleName}", roleName, typeof (RoleActiveDirectory)).With<PX.SM.Access.ADGroupsForRoleDefinition, IEnumerable<string>>((Func<PX.SM.Access.ADGroupsForRoleDefinition, IEnumerable<string>>) (_ => _._groups));
      return strings ?? (IEnumerable<string>) new string[0];
    }

    public void Prefetch(string roleName)
    {
      if (string.IsNullOrEmpty(roleName))
        return;
      List<string> stringList = new List<string>();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<RoleActiveDirectory>(new PXDataField(typeof (RoleActiveDirectory.groupID).Name), (PXDataField) new PXDataFieldOrder(typeof (RoleActiveDirectory.groupID).Name), (PXDataField) new PXDataFieldValue(typeof (RoleActiveDirectory.role).Name, PXDbType.NVarChar, new int?(64 /*0x40*/), (object) roleName)))
      {
        string str = pxDataRecord.GetString(0);
        stringList.Add(str);
      }
      this._groups = (IEnumerable<string>) stringList;
    }
  }
}
