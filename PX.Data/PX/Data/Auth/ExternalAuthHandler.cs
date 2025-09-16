// Decompiled with JetBrains decompiler
// Type: PX.Data.Auth.ExternalAuthHandler
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using Microsoft.Extensions.Options;
using PX.Api;
using PX.Api.Services;
using PX.Common;
using PX.Common.Services;
using PX.Data.Access.ActiveDirectory;
using PX.Export.Authentication;
using PX.Export.Excel.Core;
using PX.Licensing;
using PX.Security;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

#nullable disable
namespace PX.Data.Auth;

[PXInternalUseOnly]
public sealed class ExternalAuthHandler : IHttpHandler, IRequiresSessionState
{
  private const string COMPANY_SWITCH = "_companySwitch_";
  private readonly IFormsAuthenticationService _formsAuthenticationService;
  private readonly IExternalAuthenticationService _externalAuthenticationService;
  private readonly ExternalAuthenticationOptions _externalAuthenticationOptions;
  private readonly IActiveDirectoryProvider _activeDirectoryProvider;
  private readonly ICurrentUserInformationProvider _currentUserInformationProvider;
  private readonly IUserManagementService _userManagementService;
  private readonly ILicensingManager _licensingManager;
  private readonly Func<ILicenseService> _licenseServiceFactory;
  private readonly IPXLogin _pxLogin;
  private readonly ISessionContextFactory _sessionContextFactory;
  private readonly ILegacyCompanyService _legacyCompanyService;
  private readonly ILoginService _loginService;

  public ExternalAuthHandler()
  {
    this._formsAuthenticationService = ServiceLocator.Current.GetInstance<IFormsAuthenticationService>();
    this._externalAuthenticationService = ServiceLocator.Current.GetInstance<IExternalAuthenticationService>();
    this._externalAuthenticationOptions = ServiceLocator.Current.GetInstance<IOptions<ExternalAuthenticationOptions>>().Value;
    this._activeDirectoryProvider = ServiceLocator.Current.GetInstance<IActiveDirectoryProvider>();
    this._currentUserInformationProvider = ServiceLocator.Current.GetInstance<ICurrentUserInformationProvider>();
    this._userManagementService = ServiceLocator.Current.GetInstance<IUserManagementService>();
    this._licensingManager = ServiceLocator.Current.GetInstance<ILicensingManager>();
    this._licenseServiceFactory = ServiceLocator.Current.GetInstance<Func<ILicenseService>>();
    this._pxLogin = ServiceLocator.Current.GetInstance<IPXLogin>();
    this._sessionContextFactory = ServiceLocator.Current.GetInstance<ISessionContextFactory>();
    this._legacyCompanyService = ServiceLocator.Current.GetInstance<ILegacyCompanyService>();
    this._loginService = ServiceLocator.Current.GetInstance<ILoginService>();
  }

  public bool IsReusable => true;

  private string GetReturnUrl(HttpContextBase context)
  {
    string returnUrl1 = context.Request.GetReturnUrl();
    string currentDirectoryUrl = PXUrl.GetCurrentDirectoryUrl(context.Request);
    if (!string.IsNullOrEmpty(returnUrl1) && PXUrl.IsInternalUrl(returnUrl1, currentDirectoryUrl))
      return returnUrl1;
    string returnUrl2 = this._externalAuthenticationOptions.ReturnUrl;
    if (string.IsNullOrEmpty(returnUrl2))
      returnUrl2 = "~/Main";
    if (!returnUrl2.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !returnUrl2.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
      returnUrl2 = Utils.CombinePaths(HttpRuntime.AppDomainAppVirtualPath, returnUrl2);
    return returnUrl2;
  }

  public void ProcessRequest(HttpContext context)
  {
    HttpContextBase contextBase = context.GetContextBase();
    string str1 = (string) null;
    string sessionId = context.Request.GetSessionId();
    bool flag1 = context.Request.HasMobileSourceInQueryString();
    if (!string.IsNullOrEmpty(sessionId))
    {
      string href = PXUrl.AppendUrlParameter(context.Request.Url.IgnoreSessionId(), "_companySwitch_", true.ToString());
      HttpContext.Current.Server.TransferRequest(PXUrl.ToRelativeUrl(PXSessionStateStore.GetSessionUrl(context, href, sessionId)), true, (string) null, new System.Collections.Specialized.NameValueCollection(context.Request.Headers)
      {
        ["AspFilterSessionId"] = sessionId
      });
    }
    else
    {
      bool flag2 = false;
      string locale = (string) null;
      try
      {
        string company1 = context.Request.GetCompany();
        string branch = context.Request.GetBranch();
        string provider = context.Request.GetProvider();
        locale = context.Request.GetLocale();
        if (locale != null & flag1)
        {
          using (this._loginService.GetAdminLoginScope(company1))
            locale = LoginService.DetermineLocale((string) null, locale);
        }
        string association = context.Request.GetAssociation();
        string returnUrl = this.GetReturnUrl(contextBase);
        string queryParameter = context.Request.GetQueryParameter("_companySwitch_");
        try
        {
          str1 = HttpUtility.UrlDecode(returnUrl);
          string[] strArray = str1.Split('?', 2);
          strArray[0] = VirtualPathUtility.ToAbsolute(strArray[0]);
          str1 = string.Join("?", strArray);
        }
        catch (ArgumentException ex)
        {
          str1 = returnUrl;
        }
        string company2 = company1;
        if (flag1 && !string.IsNullOrEmpty(company1))
          company2 = (string) null;
        string disabledFeature;
        if (!string.IsNullOrEmpty(provider) && !this._externalAuthenticationService.IsFeatureEnabledForProvider(provider, out disabledFeature, company2))
          throw new PXException(PXMessages.LocalizeFormatNoPrefix("You cannot sign in using an external provider because the {0} feature is disabled on the Enable/Disable Features (CS100000) form.", (object) disabledFeature));
        string company3;
        string name = this._externalAuthenticationService.Authenticate(context, out company3)?.Identity?.Name;
        if (name != null)
        {
          string username = this._legacyCompanyService.ExtractUsername(name);
          if (!string.IsNullOrEmpty(company3) && company1 == null | flag1)
            company1 = company3;
          if (company1 == null)
          {
            this._legacyCompanyService.ParseLogin(name, out username, out company3, out string _);
            if (!string.IsNullOrEmpty(company3))
              company1 = company3;
          }
          string[] userGroupIds = this._activeDirectoryProvider.GetUserGroupIDs(name, false);
          if (company1 == null && PXDatabase.Companies != null && ((IEnumerable<string>) PXDatabase.Companies).Any<string>())
          {
            HashSet<string> stringSet = new HashSet<string>((IEnumerable<string>) userGroupIds, (IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
            foreach (string company4 in PXDatabase.Companies)
            {
              PXDatabase.ResetCredentials();
              using (new PXLoginScope(this._legacyCompanyService.ConcatLogin(username, company4), Array.Empty<string>()))
              {
                foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<RoleActiveDirectory>((PXDataField) new PXDataField<RoleActiveDirectory.groupID>()))
                {
                  if (stringSet.Contains(pxDataRecord.GetString(0)))
                  {
                    company1 = company4;
                    break;
                  }
                }
                if (company1 == null)
                {
                  foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<RoleClaims>((PXDataField) new PXDataField<RoleClaims.groupID>()))
                  {
                    if (stringSet.Contains(pxDataRecord.GetString(0)))
                    {
                      company1 = company4;
                      break;
                    }
                  }
                  if (company1 == null)
                  {
                    try
                    {
                      MembershipUser user = this._userManagementService.GetUser(username);
                      if (user != null)
                      {
                        if (user.UserName != null)
                        {
                          using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<UsersInRoles>((PXDataField) new PXDataField<UsersInRoles.rolename>(), (PXDataField) new PXDataFieldValue<UsersInRoles.username>(PXDbType.NVarChar, new int?(64 /*0x40*/), (object) user?.UserName)))
                          {
                            if (pxDataRecord != null)
                              company1 = company4;
                          }
                        }
                      }
                    }
                    catch
                    {
                    }
                    if (company1 != null)
                      break;
                  }
                  else
                    break;
                }
                else
                  break;
              }
            }
            PXDatabase.ResetCredentials();
          }
          string str2 = this.NormalizeUserName(username, company1);
          string str3 = this._legacyCompanyService.ConcatLogin(str2, company1);
          if (association == null)
          {
            this._pxLogin.InitUserEnvironment(str3, locale ?? "en-US");
            using (new PXLoginScope(str3, Array.Empty<string>()))
            {
              MembershipUser userAndMarkOnline = this._userManagementService.GetUserAndMarkOnline(str3);
              if (!((IUserValidationService) this._userManagementService).IsValidForLogin(contextBase.Request, userAndMarkOnline))
                throw new PXException("Login failed.");
              this._externalAuthenticationService.TryAssociate(context, userAndMarkOnline.ProviderUserKey);
              this._sessionContextFactory.AuthenticateRequest(contextBase);
              this._sessionContextFactory.SaveRequestToSession(contextBase);
              this._sessionContextFactory.PersistNewIdentity(contextBase);
              this._formsAuthenticationService.SetAuthCookie(contextBase.Request, contextBase.Response, str3, false, context.Request.GetAuthenticationPrefix() ?? string.Empty);
              this._licensingManager.TrackAuthentication();
              this._licenseServiceFactory().ValidateUser();
              if (!((IUserValidationService) this._userManagementService).IsAllowedToLogin(str3) || this._activeDirectoryProvider.IsUserDisabled(str3))
              {
                PXException pxException;
                if (company1 == null)
                  pxException = new PXException("You are not allowed to log in. Please contact your system administrator.");
                else
                  pxException = new PXException("You are not allowed to log in to the tenant {0}. Please contact your system administrator.", new object[1]
                  {
                    (object) company1
                  });
                throw pxException;
              }
              if (!string.IsNullOrEmpty(branch))
              {
                if (provider == this._externalAuthenticationService.FederatedProviderName)
                {
                  using (new PXLoginScope(str3, Array.Empty<string>()))
                  {
                    BranchInfo branchInfo = this._currentUserInformationProvider.GetActiveBranches().FirstOrDefault<BranchInfo>((Func<BranchInfo, bool>) (b => b.Cd.Trim().OrdinalEquals(branch)));
                    if (branchInfo != null)
                      PXLogin.SetBranchID(new int?(branchInfo.Id));
                  }
                }
                else
                  this._pxLogin.SetBranchId(str2, company1, branch);
              }
              bool result;
              if (((queryParameter == null ? 0 : (bool.TryParse(queryParameter, out result) ? 1 : 0)) & (result ? 1 : 0)) != 0)
                this._pxLogin.ResetCompanySpecificUserInfo();
              else
                this._pxLogin.TrackLogin(str2);
            }
          }
          else
            flag2 = true;
          if (flag1)
          {
            if (!string.IsNullOrEmpty(branch))
            {
              using (new PXLoginScope(str3, Array.Empty<string>()))
              {
                string branchCD = this._currentUserInformationProvider.GetBranchCD();
                IEnumerable<BranchInfo> activeBranches = this._currentUserInformationProvider.GetActiveBranches();
                if (!string.IsNullOrEmpty(branchCD))
                {
                  if (activeBranches.Any<BranchInfo>((Func<BranchInfo, bool>) (b => b.Cd.Trim().OrdinalEquals(branchCD.Trim()))))
                    goto label_105;
                }
                this._formsAuthenticationService.SignOut(contextBase.Request, contextBase.Response);
                this._pxLogin.LogoutUser(this._currentUserInformationProvider.GetUserName(), new AspNetSession(context.Session));
                context.RedirectToFailedMobileAuth(locale);
              }
            }
          }
        }
        else
        {
          if (!flag1)
            throw new PXException("The system cannot process an external authentication. Please make sure that authentication is set up correctly and the specified parameters are valid.");
          context.RedirectToFailedMobileAuth(locale);
        }
      }
      catch (PXExternalLoginAssociateException ex)
      {
        string key = Guid.NewGuid().ToString();
        PXTrace.WriteError((Exception) ex);
        if (flag1)
        {
          context.RedirectToFailedMobileAuth(locale);
        }
        else
        {
          PXContext.Session.Exception[key] = (Exception) ex;
          Redirector.Redirect(context, $"~/Frames/Login.aspx?ReturnUrl={context.Server.UrlEncode(str1)}&{"exceptionID"}={key}");
        }
      }
      catch (PXException ex)
      {
        string str4 = HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(ex.Message ?? string.Empty));
        PXTrace.WriteError((Exception) ex);
        if (flag1)
          context.RedirectToFailedMobileAuth(locale);
        else
          Redirector.Redirect(context, $"~/Frames/Login.aspx?ReturnUrl={context.Server.UrlEncode(str1)}&message={str4}");
      }
      catch (Exception ex)
      {
        PXTrace.WriteError(ex);
        string str5 = HttpUtility.UrlEncode(Encoding.UTF8.GetBytes(ex.Message));
        if (flag1)
          context.RedirectToFailedMobileAuth(locale);
        else
          Redirector.Redirect(context, $"~/Frames/Error.aspx?message={str5}");
      }
label_105:
      if (flag1)
        context.RedirectToSuccessMobileAuth(locale);
      else if (!flag2)
        context.Response.Redirect(str1);
      else
        Redirector.RedirectPage(context, str1);
    }
  }

  /// <summary>
  /// Return real username as it is stored in the DB.
  /// In some scenarios there might be user in the user@domain format obtained from the AD provider,
  /// but we store it internally in the domain\user format
  /// </summary>
  private string NormalizeUserName(string username, string company)
  {
    using (new PXLoginScope(this._legacyCompanyService.ConcatLogin(username, company), Array.Empty<string>()))
      return this._currentUserInformationProvider.GetUserName() ?? username;
  }
}
