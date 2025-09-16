// Decompiled with JetBrains decompiler
// Type: PX.Security.UserManagementService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.SM;
using PX.SP;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;

#nullable disable
namespace PX.Security;

internal class UserManagementService : IUserManagementService, IUserValidationService
{
  private readonly PXBaseMembershipProvider _membershipProvider;
  private readonly IRoleManagementService _roleManagementService;
  private readonly IPortalService _portalService;

  public UserManagementService(
    PXBaseMembershipProvider membershipProvider,
    IRoleManagementService roleManagementService,
    IPortalService portalService)
  {
    this._membershipProvider = membershipProvider;
    this._roleManagementService = roleManagementService;
    this._portalService = portalService;
  }

  public MembershipUser GetUser(Guid id) => this._membershipProvider.GetUser((object) id, false);

  public MembershipUser GetUser(string username)
  {
    return this._membershipProvider.GetUser(username, false);
  }

  public MembershipUser GetUserAndMarkOnline(string username)
  {
    return this._membershipProvider.GetUser(username, true);
  }

  public void MarkAllUsersOffline()
  {
    try
    {
      PXDatabase.Update(typeof (Users), (PXDataFieldParam) new PXDataFieldAssign("IsOnLine", (object) false), (PXDataFieldParam) new PXDataFieldRestrict("LastHostName", (object) PXLogin.HostName), (PXDataFieldParam) new PXDataFieldRestrict("ApplicationName", (object) this._membershipProvider.ApplicationName));
    }
    catch
    {
    }
  }

  public int GetNumberOfUsersOnline() => this._membershipProvider.GetNumberOfUsersOnline();

  bool IUserValidationService.ValidateUser(
    HttpRequestBase request,
    string username,
    string password,
    out string providerLogin)
  {
    return this._membershipProvider.ValidateUser(request, username, password, out providerLogin);
  }

  public bool ValidateUserPassword(string username, string password, out string providerUsername)
  {
    return this._membershipProvider.ValidateUserPassword(username, password, true, out providerUsername);
  }

  public bool ValidateUserPasswordForTenantSwitching(
    string username,
    string password,
    out string providerUsername)
  {
    return this._membershipProvider.ValidateUserPassword(username, password, true, true, out providerUsername);
  }

  public bool ValidateUserPasswordWithoutUserRestrictions(
    string username,
    string password,
    out string providerUsername)
  {
    return this._membershipProvider.ValidateUserPassword(username, password, false, out providerUsername);
  }

  bool IUserValidationService.ValidateUserIP(HttpRequestBase request, string username)
  {
    return PXDatabaseMembershipProvider.ValidateUserIP(request, username);
  }

  bool IUserValidationService.ValidateExternalUser(string username)
  {
    return PXDatabaseMembershipProvider.ValidateExternalAccessRights(this.GetUser(username));
  }

  Exception IUserValidationService.ValidatePasswordPolicy(string password)
  {
    return PXDatabaseMembershipProvider.ValidateAgainstPasswordPolicy(password);
  }

  public bool ValidateUserRoles(string username)
  {
    string[] rolesForUser = this._roleManagementService.GetRolesForUser(username);
    return rolesForUser != null && rolesForUser.Length != 0;
  }

  string IUserValidationService.GetValidationMessage(string username)
  {
    MembershipUser user = this.GetUser(username);
    if (user == null)
      return "Invalid credentials. Please try again.";
    if (user.IsLockedOut)
      return "Your account is locked out. Please contact your system administrator.";
    if (!user.IsApproved)
      return "Your account is disabled. Please contact your system administrator.";
    if (user is MembershipUserExt membershipUserExt && membershipUserExt.IsPendingActivation)
      return "Your account is not activated yet. Please contact your system administrator.";
    bool flag = this._portalService.IsPortalContext();
    if (!flag && membershipUserExt != null && membershipUserExt.Guest)
      return "Your account does not have access to the Acumatica ERP. Please contact your system administrator.";
    if (flag && !this._portalService.IsPortalFeatureInstalled())
      return "Your account does not have access to the Portal. Contact your system administrator.";
    return membershipUserExt != null & flag && !this._portalService.IsPortalAccessAllowed(membershipUserExt.UserName) ? "Your account is not authorized to access this portal. Contact your system administrator." : (string) null;
  }

  public void UpdateUser(string username, bool skipWatchdog, params PXDataFieldAssign[] changes)
  {
    PXDatabaseMembershipProvider.UpdateUser(username, skipWatchdog, this._membershipProvider.ApplicationName, changes);
  }

  public void UpdateUserPassword(string username, string password)
  {
    this._membershipProvider.UpdateUserPassword(username, password);
  }

  public bool ChangePassword(string username, string oldPassword, string newPassword)
  {
    return this._membershipProvider.ChangePassword(username, oldPassword, newPassword);
  }

  public PXDataRecord SelectSMUser(string username, params PXDataField[] pars)
  {
    return PXDatabase.SelectSingle(typeof (Users), new List<PXDataField>((IEnumerable<PXDataField>) pars)
    {
      (PXDataField) new PXDataFieldValue("Username", (object) username),
      (PXDataField) new PXDataFieldValue("ApplicationName", (object) this._membershipProvider.ApplicationName)
    }.ToArray());
  }
}
