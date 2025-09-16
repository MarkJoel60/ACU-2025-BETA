// Decompiled with JetBrains decompiler
// Type: PX.SM.RoleAccess
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.Access;
using PX.Data.Access.ActiveDirectory;
using PX.Security.Authorization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

public class RoleAccess : PX.SM.Access
{
  public PXSave<PX.SM.Roles> SaveRoles;
  public PXCancel<PX.SM.Roles> CancelRoles;
  public PXInsert<PX.SM.Roles> InsertRoles;
  public PXDelete<PX.SM.Roles> DeleteRoles;
  public PXFirst<PX.SM.Roles> FirstRoles;
  public PXPrevious<PX.SM.Roles> PrevRoles;
  public PXNext<PX.SM.Roles> NextRoles;
  public PXLast<PX.SM.Roles> LastRoles;
  public PXSelect<RolesInGraph, Where<RolesInGraph.rolename, Equal<Constants.universalRole>, And<RolesInGraph.accessrights, NotEqual<Constants.revokedAccessRights>>>> NotRevokedUniversalAccessRights;
  public PXAction<PX.SM.Roles> ReloadADGroups;

  [InjectDependency]
  protected ICurrentUserInformationProvider CurrentUserInformationProvider { get; set; }

  [InjectDependency]
  private IEnumerable<IPredefinedRolesProvider> PredefinedRolesProviders { get; set; }

  [PXUIField(DisplayName = "Reload AD Groups", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  public IEnumerable reloadADGroups(PXAdapter adapter)
  {
    this.ActiveDirectoryProvider.Reset();
    this.ActiveDirectoryProvider.GetGroups();
    return adapter.Get();
  }

  public RoleAccess()
    : base(false)
  {
  }

  [PXInternalUseOnly]
  public override void Initialize()
  {
    base.Initialize();
    if (!this.ActiveDirectoryProvider.IsEnabled())
    {
      PXUIFieldAttribute.SetVisible<Users.domain>(this.UserList.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisibility<Users.domain>(this.UserList.Cache, (object) null, PXUIVisibility.Invisible);
      PXUIFieldAttribute.SetVisible<UsersInRoles.domain>(this.RoleList.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisibility<UsersInRoles.domain>(this.RoleList.Cache, (object) null, PXUIVisibility.Invisible);
      PXUIFieldAttribute.SetVisible<UsersInRoles.inherited>(this.RoleList.Cache, (object) null, false);
      PXUIFieldAttribute.SetVisibility<UsersInRoles.inherited>(this.RoleList.Cache, (object) null, PXUIVisibility.Invisible);
    }
    PXUIFieldAttribute.SetVisible<UsersInRoles.username>(this.RoleList.Cache, (object) null, true);
    this.Save.SetVisible(false);
    this.Cancel.SetVisible(false);
    bool isVisible = this.ShouldCacheADGroups();
    this.ReloadADGroups.SetVisible(isVisible);
    foreach (PXEventSubscriberAttribute attribute in this.GetAttributes("ActiveDirectoryMap", "GroupID"))
    {
      if (attribute is ADGroupSelectorAttribute)
        ((ADGroupSelectorAttribute) attribute).UseCached = isVisible;
    }
  }

  private IEnumerable<string> PredefinedRoles
  {
    get
    {
      return this.PredefinedRolesProviders.SelectMany<IPredefinedRolesProvider, string>((Func<IPredefinedRolesProvider, IEnumerable<string>>) (x => x.Roles));
    }
  }

  protected virtual void Roles_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    PX.SM.Roles role = e.Row as PX.SM.Roles;
    if (this.PredefinedRoles.Any<string>((Func<string, bool>) (r => r == role.Rolename || !PXAccess.IsRoleEnabled(role.Rolename))))
      throw new PXException("A predefined role cannot be deleted.");
    if (!PXSiteMapProvider.IsUserInRole(role.Rolename) || !PX.SM.Access.WillSelfLock(sender, role.Rolename) || this.Roles.Ask("Warning", "These changes will prevent you from accessing this form. Are you sure you want to continue?", MessageButtons.YesNo, MessageIcon.Warning) == WebDialogResult.Yes)
      return;
    e.Cancel = true;
  }

  protected virtual void _(Events.RowInserting<PX.SM.Roles> e)
  {
    string str = e.Row?.Rolename?.Trim();
    if (string.Equals(str, "*", StringComparison.OrdinalIgnoreCase))
    {
      e.Cancel = true;
    }
    else
    {
      if (string.IsNullOrWhiteSpace(str))
        return;
      this.SetRevokedAccessRightToScreensForRole(str);
    }
  }

  private void SetRevokedAccessRightToScreensForRole(string roleName)
  {
    foreach (RolesInGraph rolesInGraph1 in this.NotRevokedUniversalAccessRights.Select<RolesInGraph>())
    {
      RolesInGraph rolesInGraph2 = this.NotRevokedUniversalAccessRights.Insert();
      rolesInGraph2.Accessrights = new short?((short) 0);
      rolesInGraph2.Rolename = roleName;
      rolesInGraph2.ApplicationName = rolesInGraph1.ApplicationName;
      rolesInGraph2.ScreenID = rolesInGraph1.ScreenID;
      this.NotRevokedUniversalAccessRights.Update(rolesInGraph1);
    }
  }

  protected virtual void UsersInRoles_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is UsersInRoles row))
      return;
    if (!PXAccess.IsRoleEnabled(row.Rolename))
    {
      e.Cancel = true;
    }
    else
    {
      if (string.IsNullOrEmpty(row.Rolename) && this.RoleList.Current != null)
        row.Rolename = this.RoleList.Current.Rolename;
      if (string.IsNullOrEmpty(row.Username))
        return;
      PX.Data.Access.ActiveDirectory.User user = this.ActiveDirectoryProvider.GetUser((object) row.Username);
      if (user != null)
      {
        row.DisplayName = user.Name.Name;
        row.Domain = Users.GetDomains((IEnumerable<Login>) user.Name.Logins);
        row.Comment = user.Comment;
      }
      else
      {
        PXResultset<Users> pxResultset = PXSelectBase<Users, PXSelect<Users>.Config>.Search<Users.username>((PXGraph) this, (object) row.Username);
        if (pxResultset == null || pxResultset.Count == 0)
          return;
        Users users = (Users) pxResultset[0];
        row.DisplayName = users.DisplayName;
        row.Comment = users.Comment;
      }
    }
  }

  protected virtual void UsersInRoles_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    UsersInRoles row = e.Row as UsersInRoles;
    if (row.Username != this.Accessinfo.UserName || !PX.SM.Access.WillSelfLock(sender, row.Rolename) || this.Roles.Ask("Warning", "These changes will prevent you from accessing this form. Are you sure you want to continue?", MessageButtons.YesNo, MessageIcon.Warning) == WebDialogResult.Yes)
      return;
    e.Cancel = true;
  }

  private bool ShouldCacheADGroups()
  {
    try
    {
      return this.ActiveDirectoryProvider.GetGroups().Count<Group>() > this.Options.Value.ADGroupCacheLimit;
    }
    catch (Exception ex)
    {
      this._logger.Warning(ex, "Unable to get Active Directory groups count");
      return false;
    }
  }
}
