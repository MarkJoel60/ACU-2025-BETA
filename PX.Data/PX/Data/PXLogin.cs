// Decompiled with JetBrains decompiler
// Type: PX.Data.PXLogin
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Services;
using PX.Common;
using PX.Common.Services;
using PX.Data.Auth;
using PX.EP;
using PX.Licensing;
using PX.Security;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public sealed class PXLogin : IPXLogin
{
  private readonly IUserManagementService _userManagementService;
  private readonly IExternalAuthenticationService _externalAuthenticationService;
  private readonly ICurrentUserInformationProvider _currentUserInformationProvider;
  private readonly IFederationAuthentication _federationAuthentication;
  private readonly ILoginAsUser _loginAsUser;
  private readonly ISessionContextFactory _sessionContextFactory;
  private readonly ILegacyCompanyService _legacyCompanyService;
  private readonly ICompanyService _companyService;
  private ILicensingManager _licensingManager;
  private Func<ILicenseService> _licenseServiceFactory;
  private IPXLicensePolicy _licensePolicy;

  internal PXLogin(
    IUserManagementService userManagementService,
    IExternalAuthenticationService externalAuthenticationService,
    ICurrentUserInformationProvider currentUserInformationProvider,
    IFederationAuthentication federationAuthentication,
    ILoginAsUser loginAsUser,
    ISessionContextFactory sessionContextFactory,
    ILegacyCompanyService legacyCompanyService,
    ICompanyService companyService)
  {
    this._userManagementService = userManagementService;
    this._externalAuthenticationService = externalAuthenticationService;
    this._currentUserInformationProvider = currentUserInformationProvider;
    this._federationAuthentication = federationAuthentication;
    this._loginAsUser = loginAsUser;
    this._sessionContextFactory = sessionContextFactory;
    this._legacyCompanyService = legacyCompanyService;
    this._companyService = companyService;
  }

  internal void InitializeLicensing(
    ILicensingManager licensingManager,
    Func<ILicenseService> licenseServiceFactory,
    IPXLicensePolicy licensePolicy)
  {
    this._licensingManager = licensingManager;
    this._licenseServiceFactory = licenseServiceFactory;
    this._licensePolicy = licensePolicy;
  }

  internal static string HostName => Dns.GetHostName();

  private static void CheckInvalidating(
    ISessionContextFactory sessionContextFactory,
    HttpContext httpContext)
  {
    if (!((IEnumerable<string>) httpContext.Request.Cookies.AllKeys).Contains<string>("InvalidateContext"))
      return;
    HttpCookie cookie = httpContext.Request.Cookies["InvalidateContext"];
    if (cookie == null || !(cookie.Value == PXSessionStateStore.GetSuffix(httpContext)))
      return;
    sessionContextFactory.Invalidate(httpContext.Request.RequestContext.HttpContext);
    httpContext.Request.Cookies.Remove("InvalidateContext");
  }

  [Obsolete("Pass HttpContext explicitly")]
  public static void RequestInvalidating() => PXLogin.RequestInvalidating(HttpContext.Current);

  public static void RequestInvalidating(HttpContext httpContext)
  {
    if (httpContext == null)
      return;
    PXSessionStateStore.GetSuffix(httpContext);
    HttpCookie cookie = httpContext.Response.Cookies["InvalidateContext"];
    if (cookie == null)
      httpContext.Response.Cookies.Add(new HttpCookie("InvalidateContext", PXSessionStateStore.ResolvePopup(PXSessionStateStore.GetSuffix(httpContext))));
    else
      cookie.Value = PXSessionStateStore.ResolvePopup(PXSessionStateStore.GetSuffix(httpContext));
  }

  /// <returns><c>true</c> when login is successful, <c>false</c> when password change is required.</returns>
  /// <exception cref="T:PX.Data.PXException">User validation failed (including password validation)</exception>
  bool IPXLogin.LoginUser(ref string userName, string password)
  {
    HttpContext current = HttpContext.Current;
    (IPrincipal principal, MembershipUser membershipUser) = this.LoginUserImpl(current, userName, password);
    if (principal == null)
      return false;
    userName = principal.Identity.Name;
    using (PXLoginScope pxLoginScope = new PXLoginScope(principal.Identity.Name, Array.Empty<string>()))
    {
      try
      {
        this._externalAuthenticationService.TryAssociate(current, membershipUser.ProviderUserKey);
      }
      catch (Exception ex)
      {
        PXAuditJournal.Register(PXAuditJournal.Operation.LoginFailed, pxLoginScope.UserName, (string) null, ex.Message);
        throw;
      }
      this.TrackAndFinishLogin(current.GetContextBase(), pxLoginScope.UserName, pxLoginScope.CompanyName, pxLoginScope.Branch);
    }
    PXContext.PXIdentity.SetUser(principal);
    return true;
  }

  ClaimsPrincipal IPXLogin.Authenticate(string username, string password)
  {
    IPrincipal principal = this.LoginUserImpl(HttpContext.Current, username, password).Item1;
    if (principal is ClaimsPrincipal claimsPrincipal)
      return claimsPrincipal;
    return principal != null ? new ClaimsPrincipal(principal) : throw new PXException("Invalid credentials. Please try again.");
  }

  private (IPrincipal, MembershipUser) LoginUserImpl(
    HttpContext httpContext,
    string userName,
    string password)
  {
    HttpContextBase httpContext1 = httpContext != null ? httpContext.GetContextBase() : throw new InvalidOperationException("Cannot perform login outside of HTTP context");
    PXLogin.CheckInvalidating(this._sessionContextFactory, httpContext);
    using (PXLoginScope pxLoginScope = new PXLoginScope(userName, Array.Empty<string>()))
    {
      try
      {
        PXDatabase.SelectTimeStamp();
        if (((IUserValidationService) this._userManagementService).ValidateUser(httpContext1.Request, pxLoginScope.UserName, password, ref userName))
        {
          MembershipUser user = this._userManagementService.GetUser(userName);
          if (user != null)
          {
            pxLoginScope.UserName = user.UserName;
            if (!((IUserValidationService) this._userManagementService).IsAllowedToLogin(pxLoginScope.UserName))
            {
              PXException pxException;
              if (pxLoginScope.CompanyName == null)
                pxException = new PXException("You are not allowed to log in. Please contact your system administrator.");
              else
                pxException = new PXException("You are not allowed to log in to the tenant {0}. Please contact your system administrator.", new object[1]
                {
                  (object) pxLoginScope.CompanyName
                });
              throw pxException;
            }
            if (pxLoginScope.CompanyName != null && !((IEnumerable<string>) PXDatabase.AvailableCompanies).Contains<string>(pxLoginScope.CompanyName, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
              throw new PXException("You are not allowed to log in to the tenant {0}. Please contact your system administrator.", new object[1]
              {
                (object) pxLoginScope.CompanyName
              });
            this._licenseServiceFactory().ValidateUser();
            PXLogin.CheckAllowedUserType(httpContext, user.GetIDOrDefault());
            if (this.IsPasswordChangeRequired(pxLoginScope.UserName))
              return ((IPrincipal) null, (MembershipUser) null);
            this._licensePolicy.CheckApiUsersLimits(httpContext1);
            return (PXContext.PXIdentity.User, user);
          }
        }
        else
        {
          string validationMessage = ((IUserValidationService) this._userManagementService).GetValidationMessage(pxLoginScope.UserName);
          if (validationMessage != null)
            throw new PXException(validationMessage);
        }
        throw new PXException("Invalid credentials. Please try again.");
      }
      catch (Exception ex)
      {
        PXAuditJournal.Register(PXAuditJournal.Operation.LoginFailed, pxLoginScope.UserName, (string) null, ex.Message);
        if (ex is ArgumentException)
          throw new PXException("Invalid user name. Please try again.", ex);
        throw;
      }
    }
  }

  public void TrackAndFinishLogin(
    HttpContextBase httpContext,
    string username,
    string company,
    string branch)
  {
    try
    {
      this.TrackLogin(username);
      this.FinishLogin(httpContext, username, company, branch);
    }
    catch (Exception ex)
    {
      PXAuditJournal.Register(PXAuditJournal.Operation.LoginFailed, username, (string) null, ex.Message);
      throw;
    }
  }

  public void FinishLogin(
    HttpContextBase httpContext,
    string userName,
    string companyName,
    string branch)
  {
    this.SetBranchId(userName, companyName, branch);
    this._sessionContextFactory.AuthenticateRequest(httpContext);
    this._licensingManager.TrackAuthentication();
  }

  private static void CheckAllowedUserType(HttpContext httpContext, Guid userId)
  {
    int? int32;
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Users>((PXDataField) new PXDataField<Users.loginTypeID>(), (PXDataField) new PXDataFieldValue<Users.pKID>((object) userId)))
    {
      int32 = (int?) pxDataRecord?.GetInt32(0);
      if (!int32.HasValue)
        return;
    }
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<EPLoginType>((PXDataField) new PXDataField<EPLoginType.allowedLoginType>(), (PXDataField) new PXDataFieldValue<EPLoginType.loginTypeID>((object) int32)))
    {
      if (pxDataRecord == null)
        return;
      string str = pxDataRecord.GetString(0);
      if (string.IsNullOrEmpty(str))
        return;
      if (str == "A" && !httpContext.IsApiRequest() && !httpContext.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/identity/", StringComparison.OrdinalIgnoreCase))
        throw new PXException("Only API requests are allowed for this user.");
      if (str == "U" && httpContext.IsApiRequest())
        throw new PXException("Current user cannot send API requests.");
    }
  }

  public void TrackLogin(string userName) => this.TrackLogin(userName, false);

  void IPXLogin.TrackLoginWithPasswordChange(string userName) => this.TrackLogin(userName, true);

  private void TrackLogin(string userName, bool passwordReset)
  {
    PXAuditJournal.Register(PXAuditJournal.Operation.Login, userName);
    System.DateTime universalTime = System.DateTime.Now.ToUniversalTime();
    if (passwordReset)
      this._userManagementService.UpdateUser(userName, true, new PXDataFieldAssign("IsOnLine", (object) true), new PXDataFieldAssign("PasswordChangeOnNextLogin", (object) false), new PXDataFieldAssign("LastActivityDate", PXDbType.DateTime, new int?(8), (object) universalTime), new PXDataFieldAssign("LastHostName", (object) PXLogin.HostName), new PXDataFieldAssign("LastLoginDate", PXDbType.DateTime, new int?(8), (object) universalTime), new PXDataFieldAssign("LockedOutDate", PXDbType.DateTime, new int?(8), (object) null), new PXDataFieldAssign("FailedPasswordAttemptCount", PXDbType.Int, (object) 0), new PXDataFieldAssign("FailedPasswordAttemptWindowStart", PXDbType.DateTime, (object) null));
    else
      this._userManagementService.UpdateUser(userName, true, new PXDataFieldAssign("IsOnLine", (object) true), new PXDataFieldAssign("LastActivityDate", PXDbType.DateTime, new int?(8), (object) universalTime), new PXDataFieldAssign("LastHostName", (object) PXLogin.HostName), new PXDataFieldAssign("LastLoginDate", PXDbType.DateTime, new int?(8), (object) universalTime), new PXDataFieldAssign("LockedOutDate", PXDbType.DateTime, new int?(8), (object) null), new PXDataFieldAssign("FailedPasswordAttemptCount", PXDbType.Int, (object) 0), new PXDataFieldAssign("FailedPasswordAttemptWindowStart", PXDbType.DateTime, (object) null));
  }

  void IPXLogin.LogoutUser(string userName, AspNetSession session)
  {
    PXAuditJournal.Register(PXAuditJournal.Operation.Logout, userName);
    if (!string.IsNullOrEmpty(userName))
      this._userManagementService.MarkOffline(userName);
    this._licensePolicy.RemoveSession((ILicensingSession) session);
    this._loginAsUser.RemoveLoggedAsUser((ILoginAsUserSession) session);
    this._licensingManager.OnLogout((ILicensingSession) session);
  }

  public void SessionExpired(HttpApplication application, string userName)
  {
    AspNetSession session = new AspNetSession(application.Session);
    this._licensingManager.OnSession((ILicensingSession) session);
    PXAuditJournal.RegisterSessionExpired((ILoginAsUserSession) session, application.Server, userName);
    if (!string.IsNullOrEmpty(userName))
      this._userManagementService.MarkOffline(userName);
    this._licensePolicy.RemoveSession((ILicensingSession) session);
    this._loginAsUser.RemoveLoggedAsUser((ILoginAsUserSession) session);
    PXPerformanceMonitor.StopUserProfiler();
  }

  /// <summary>
  /// Checks whether admin forced this user to change password on login.
  /// </summary>
  /// <returns>True if password has to be changed, otherwise false.</returns>
  private bool IsPasswordChangeRequired(string userName)
  {
    using (PXDataRecord pxDataRecord = this._userManagementService.SelectSMUser(userName, new PXDataField("PasswordChangeOnNextLogin"), new PXDataField("PasswordNeverExpires"), new PXDataField("LastPasswordChangedDate"), new PXDataField("Password"), new PXDataField("Source")))
      return pxDataRecord != null && !pxDataRecord.IsDBNull(0) && this.IsPasswordChangeRequired(pxDataRecord.GetInt32(4), pxDataRecord.GetString(3), pxDataRecord.GetBoolean(0).Value, pxDataRecord.GetBoolean(1).Value, pxDataRecord.GetDateTime(2));
  }

  public bool IsPasswordChangeRequired(
    int? source,
    string password,
    bool shouldChangeOnNextLogin,
    bool isPasswordNeverExpired,
    System.DateTime? lastPassworChangeDate)
  {
    int? nullable1 = source;
    int num = 1;
    if (nullable1.GetValueOrDefault() == num & nullable1.HasValue)
      return false;
    if (string.IsNullOrEmpty(password) || shouldChangeOnNextLogin)
      return true;
    if (isPasswordNeverExpired || SitePolicy.PasswordDayAge <= 0)
      return false;
    System.DateTime? nullable2 = lastPassworChangeDate;
    return !nullable2.HasValue || nullable2.Value.AddDays((double) SitePolicy.PasswordDayAge) < System.DateTime.Today;
  }

  public static bool AllowPasswordRecovery(string userName)
  {
    using (PXLoginScope pxLoginScope = new PXLoginScope(userName, Array.Empty<string>()))
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle(typeof (Users), new PXDataField(nameof (AllowPasswordRecovery)), (PXDataField) new PXDataFieldValue("Username", (object) pxLoginScope.UserName)))
        return pxDataRecord == null || pxDataRecord.GetBoolean(0).Value;
    }
  }

  public static List<string> GetReadWikiLocales(Guid wiki)
  {
    List<string> readWikiLocales = new List<string>();
    bool flag = false;
    foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<WikiReadLanguage>(new PXDataField(typeof (WikiReadLanguage.localeID).Name), (PXDataField) new PXDataFieldValue(typeof (WikiReadLanguage.wikiID).Name, (object) wiki)))
    {
      string strA = pxDataRecord.GetString(0);
      readWikiLocales.Add(strA);
      if (string.Compare(strA, "en-US", true) == 0)
        flag = true;
    }
    if (!flag)
      readWikiLocales.Insert(0, "en-US");
    return readWikiLocales;
  }

  public bool SwitchCompany(string companyID, out string redirectUrl)
  {
    redirectUrl = (string) null;
    HttpContext httpContext = HttpContext.Current ?? throw new InvalidOperationException("Cannot perform company switch outside of HTTP context");
    if (!this.TrySwitchCompany(httpContext, ref companyID))
      return false;
    if (this._currentUserInformationProvider.IsClaimUser())
    {
      Uri uri = new Uri(PXSessionStateStore.GetSessionUrl(httpContext, httpContext.Request.UrlReferrer.AbsoluteUri));
      NameValueCollection queryString = HttpUtility.ParseQueryString(uri.Query);
      queryString["CompanyID"] = companyID;
      Dictionary<string, string> signInParameters = ExternalAuthenticationParameters.CreateSignInParameters(httpContext.Request, (string) null, companyID, PXContext.PXIdentity.Culture?.Name, PXUrl.ToRelativeUrl(new UriBuilder(uri)
      {
        Query = queryString.ToString()
      }.Uri.AbsoluteUri));
      signInParameters.SetSessionId(httpContext.Request);
      redirectUrl = this._federationAuthentication.GetRedirectUrl(httpContext.Request, signInParameters);
    }
    return true;
  }

  private static string CorrectCompanyName(string companyID)
  {
    int index = Array.FindIndex<string>(PXDatabase.Companies, (Predicate<string>) (c => string.Equals(c, companyID, StringComparison.InvariantCultureIgnoreCase)));
    return index < 0 ? (string) null : PXDatabase.Companies[index];
  }

  private bool TrySwitchCompany(HttpContext httpContext, ref string companyID)
  {
    string username = this._legacyCompanyService.ExtractUsername(httpContext.User?.Identity?.Name);
    if (string.IsNullOrEmpty(username))
      return false;
    companyID = PXLogin.CorrectCompanyName(companyID);
    if (string.IsNullOrEmpty(companyID))
      return false;
    string str = (string) null;
    string userName = this._currentUserInformationProvider.GetUserName();
    bool flag1 = this._currentUserInformationProvider.IsActiveDirectoryUser();
    if (!flag1)
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Users>(new PXDataField("Password"), (PXDataField) new PXDataFieldValue("Username", PXDbType.NVarChar, new int?(64 /*0x40*/), (object) userName)))
      {
        if (pxDataRecord != null)
          str = pxDataRecord.GetString(0);
      }
    }
    PXContext.Session.SetString("ChangingPassword", (string) null);
    bool flag2 = false;
    string[] roles = (string[]) null;
    PXDatabase.ResetCredentials();
    using (new PXLoginScope($"{username}@{companyID}", Array.Empty<string>()))
    {
      if (!flag1)
      {
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Users>(new PXDataField("Password"), (PXDataField) new PXDataFieldValue("Username", PXDbType.NVarChar, new int?(64 /*0x40*/), (object) userName)))
        {
          if (pxDataRecord != null)
            flag2 = UserValidationServiceExtensions.ValidateUserPassword((IUserValidationService) this._userManagementService, userName, str);
        }
      }
      if (flag2 | flag1)
        flag2 = ((IUserValidationService) this._userManagementService).IsAllowedToLogin(userName);
      if (flag2)
        flag2 = ((IUserValidationService) this._userManagementService).ValidateUserIP(httpContext.GetRequestBase(), userName);
    }
    if (flag2)
    {
      PXContext.PXIdentity.SetUser((IPrincipal) new GenericPrincipal((IIdentity) new GenericIdentity($"{userName}@{companyID}"), roles));
      PXDatabase.ResetCredentials();
      PXLogin.InitWithDefaultBranch(httpContext, this._currentUserInformationProvider.GetUserName(), companyID);
      PXLogin.SwitchCulture(httpContext, username, companyID);
      this.ResetCompanySpecificUserInfo();
      this.InitTimeZone(companyID);
      httpContext.Response.SetCookie(new HttpCookie("CompanyID", companyID)
      {
        Expires = System.DateTime.Now.AddDays(3.0)
      });
    }
    return flag2;
  }

  public void ResetCompanySpecificUserInfo()
  {
    this._sessionContextFactory.Abandon();
    this._licenseServiceFactory().ValidateUser();
    this._licensingManager.TrackAuthentication();
    PXAccess.Clear();
    PXSiteMap.Provider.Clear();
    PXSiteMap.WikiProvider.Clear();
    PXLocalizer.ResetSlot();
  }

  void IPXLogin.SwitchCulture(string loginName, string companyId)
  {
    PXLogin.SwitchCulture(HttpContext.Current, loginName, companyId);
  }

  private static void SwitchCulture(HttpContext httpContext, string loginName, string companyId)
  {
    string current = CultureInfo.CurrentCulture.Name;
    PXLocale[] locales = PXLocalesProvider.GetLocales($"{loginName}@{companyId}");
    string name = locales == null || !((IEnumerable<PXLocale>) locales).Any<PXLocale>((Func<PXLocale, bool>) (l => l.Name == current)) ? (locales == null || locales.Length == 0 ? "en-US" : locales[0].Name) : current;
    CultureInfo culture = new CultureInfo(name);
    PXContext.PXIdentity.Culture = culture;
    PXContext.PXIdentity.UICulture = culture;
    LocaleInfo.SetAllCulture(culture);
    if (httpContext == null)
      return;
    httpContext.Response.Cookies["Locale"]["Culture"] = name;
  }

  public void SetBranchId(string userName, string company, string branch)
  {
    PXLogin.SetBranchID(this.GetBranchId(userName, company, branch));
  }

  private int? GetBranchId(string userName, string company, string branch)
  {
    MembershipUser user = this._userManagementService.GetUser(userName);
    int? branchId = new int?();
    if (user != null)
    {
      SMAccessPersonalMaint instance = PXGraph.CreateInstance<SMAccessPersonalMaint>();
      if (string.IsNullOrEmpty(branch))
      {
        branchId = instance.GetDefaultBranchId(user.UserName, company);
      }
      else
      {
        branch = branch.TrimEnd();
        foreach (KeyValuePair<int, string> avalableBranch in instance.GetAvalableBranches(user.UserName, company))
        {
          if (avalableBranch.Value != null && branch.Equals(avalableBranch.Value.TrimEnd(), StringComparison.OrdinalIgnoreCase))
            branchId = new int?(avalableBranch.Key);
        }
      }
    }
    return branchId;
  }

  public static void SetBranchID(int? branchId)
  {
    PXContext.SetBranchID(branchId);
    HttpContext current = HttpContext.Current;
    RefreshCheck.StoreCurrentUser(current);
    try
    {
      PXLogin.WriteUserBranchCookie(current, branchId);
    }
    catch (StackOverflowException ex)
    {
      throw;
    }
    catch (OutOfMemoryException ex)
    {
      throw;
    }
    catch (Exception ex)
    {
    }
  }

  private static void WriteUserBranchCookie(HttpContext httpContext, int? branchId)
  {
    if (httpContext == null)
      return;
    HttpCookie cookie = httpContext.Response.Cookies["UserBranch"];
    string str = branchId?.ToString();
    if (cookie == null)
      httpContext.Response.Cookies.Add(new HttpCookie("UserBranch", str));
    else
      cookie.Value = str;
  }

  void IPXLogin.InitUserEnvironment(string login, string localeName, bool initBranch)
  {
    using (new PXLoginScope(login, Array.Empty<string>()))
    {
      string username;
      string company;
      string branch;
      this._legacyCompanyService.ParseLogin(login, out username, out company, out branch);
      this.InitTimeZone(company);
      HttpContext current = HttpContext.Current;
      this.InitSession(current.Response.Cookies, company, localeName);
      if (!initBranch)
        return;
      if (string.IsNullOrEmpty(branch))
      {
        PXLogin.InitWithDefaultBranch(current, this._currentUserInformationProvider.GetUserName(), company);
      }
      else
      {
        int? branchId = this.GetBranchId(username, company, branch);
        PXContext.SetBranchID(branchId);
        PXLogin.WriteUserBranchCookie(current, branchId);
      }
    }
  }

  void IPXLogin.InitBranch(string username, string company)
  {
    PXLogin.InitWithDefaultBranch(HttpContext.Current, username, company);
  }

  private static void InitWithDefaultBranch(
    HttpContext httpContext,
    string username,
    string company)
  {
    int? defaultBranchId = PXGraph.CreateInstance<SMAccessPersonalMaint>().GetDefaultBranchId(username, company);
    PXContext.SetBranchID(defaultBranchId);
    PXLogin.WriteUserBranchCookie(httpContext, defaultBranchId);
  }

  private void InitTimeZone(string company)
  {
    LocaleInfo.SetTimeZone(this._currentUserInformationProvider.GetTimeZone() ?? PXTimeZoneInfo.FindSystemTimeZoneById(PreferencesGeneralMaint.GetDefaultTimeZoneId(company)) ?? PXTimeZoneInfo.Invariant);
  }

  private void InitSession(HttpCookieCollection cookies, string company, string localeName)
  {
    PXContext.Session.SetString("ChangingPassword", (string) null);
    string name = localeName;
    cookies["Locale"]["Culture"] = name;
    cookies["Locale"]["TimeZone"] = LocaleInfo.GetTimeZone().Id;
    cookies["Locale"].Expires = System.DateTime.Now.AddDays(3.0);
    CultureInfo cultureInfo = new CultureInfo(name);
    PXContext.PXIdentity.Culture = cultureInfo;
    PXContext.PXIdentity.UICulture = cultureInfo;
    if (!string.IsNullOrEmpty(company))
    {
      cookies["CompanyID"].Value = company;
      cookies["CompanyID"].Expires = System.DateTime.Now.AddDays(3.0);
    }
    else
    {
      cookies["CompanyID"].Value = "";
      cookies["CompanyID"].Expires = System.DateTime.Now;
    }
  }

  [Obsolete("Use ILegacyCompanyService.ExtractCompany")]
  public static string ExtractCompany(string login)
  {
    string company;
    LegacyCompanyService.ParseLogin(login, out string _, out company, out string _);
    return company;
  }
}
