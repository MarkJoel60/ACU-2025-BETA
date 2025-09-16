// Decompiled with JetBrains decompiler
// Type: PX.TokenLogin.TokenLoginHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Security;
using PX.SM;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

#nullable disable
namespace PX.TokenLogin;

internal class TokenLoginHelper : ITokenLoginHelper
{
  private readonly IUserManagementService _userManagementService;
  private readonly IRoleManagementService _roleManagementService;
  private readonly ILoginAsUser _loginAsUser;
  private readonly ILegacyCompanyService _legacyCompanyService;

  public TokenLoginHelper(
    IUserManagementService userManagementService,
    IRoleManagementService roleManagementService,
    ILoginAsUser loginAsUser,
    ILegacyCompanyService legacyCompanyService)
  {
    this._userManagementService = userManagementService;
    this._roleManagementService = roleManagementService;
    this._loginAsUser = loginAsUser;
    this._legacyCompanyService = legacyCompanyService;
  }

  bool ITokenLoginHelper.LoginAsUser(string username)
  {
    MembershipUser user = this._userManagementService.GetUser(username);
    if (user != null)
    {
      string str1 = "";
      PXSessionContext pxIdentity1 = PXContext.PXIdentity;
      if (pxIdentity1.AuthUser != null)
      {
        str1 = this._legacyCompanyService.ExtractCompanyWithBranch(pxIdentity1.AuthUser.Identity.Name) ?? string.Empty;
        if (str1.Length > 0)
          str1 = "@" + str1;
      }
      string userName = user.UserName;
      this._loginAsUser.LoginAsUser(userName);
      PXContext.Session.SetString("ChangingPassword", (string) null);
      PXContext.Session.SetString("IPAddress", HttpContext.Current.Request.GetUserHostAddress());
      PXAuditJournal.Register(PXAuditJournal.Operation.Login, userName);
      int? defaultBranchId = PXGraph.CreateInstance<SMAccessPersonalMaint>().GetDefaultBranchId(user.UserName, PXAccess.GetCompanyName());
      PXContext.SetBranchID(defaultBranchId);
      HttpCookie cookie = HttpContext.Current.Response.Cookies["UserBranch"];
      string str2 = !defaultBranchId.HasValue ? (string) null : defaultBranchId.ToString();
      if (cookie == null)
        HttpContext.Current.Response.Cookies.Add(new HttpCookie("UserBranch", str2));
      else
        cookie.Value = str2;
      PXSessionContext pxIdentity2 = PXContext.PXIdentity;
      pxIdentity2.BranchID = defaultBranchId;
      pxIdentity2.SetUser((IPrincipal) new GenericPrincipal((IIdentity) new GenericIdentity(userName + str1), this._roleManagementService.GetRolesForUser(userName + str1)));
    }
    return true;
  }
}
