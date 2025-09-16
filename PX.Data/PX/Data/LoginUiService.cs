// Decompiled with JetBrains decompiler
// Type: PX.Data.LoginUiService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.AspNetCore.WebUtilities;
using PX.Api.Services;
using PX.Common;
using PX.Common.Services;
using PX.Export.Authentication;
using PX.Security;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState;

#nullable disable
namespace PX.Data;

internal class LoginUiService : ILoginUiService
{
  private readonly ILoginService _loginService;
  private readonly ICurrentUserInformationProvider _currentUserInformationProvider;
  private readonly IPXLogin _pxLogin;
  private readonly Func<ILicenseService> _licenseServiceFactory;
  private readonly IUserManagementService _userManagementService;
  private readonly IFormsAuthenticationService _formsAuthenticationService;
  private readonly ISessionContextFactory _sessionContextFactory;
  private const string CompanyIdCookieName = "CompanyID";

  public LoginUiService(
    ILoginService loginService,
    ICurrentUserInformationProvider currentUserInformationProvider,
    IPXLogin pxLogin,
    Func<ILicenseService> licenseServiceFactory,
    IUserManagementService userManagementService,
    IFormsAuthenticationService formsAuthenticationService,
    ISessionContextFactory sessionContextFactory)
  {
    this._loginService = loginService;
    this._currentUserInformationProvider = currentUserInformationProvider;
    this._pxLogin = pxLogin;
    this._licenseServiceFactory = licenseServiceFactory;
    this._userManagementService = userManagementService;
    this._formsAuthenticationService = formsAuthenticationService;
    this._sessionContextFactory = sessionContextFactory;
  }

  public string[] GetCompanies(string userName, string password)
  {
    return !PXDatabase.SecureCompanyID ? PXDatabase.AvailableCompanies : this._loginService.GetCompanies(userName, password);
  }

  public bool LoginUser(ref string userName, string password)
  {
    return this._pxLogin.LoginUser(ref userName, password);
  }

  public void LoginUser(ref string userName, string oldPassword, string newPassword)
  {
    HttpContext current = HttpContext.Current;
    HttpContextBase contextBase = current != null ? current.GetContextBase() : (HttpContextBase) null;
    IPrincipal user;
    using (PXLoginScope pxLoginScope = new PXLoginScope(userName, Array.Empty<string>()))
    {
      if (!this._userManagementService.ChangePassword(pxLoginScope.UserName, oldPassword, newPassword) || !((IUserValidationService) this._userManagementService).ValidateUser(contextBase?.Request, pxLoginScope.UserName, newPassword, ref userName))
        throw new PXException("Invalid credentials. Please try again.");
      this._licenseServiceFactory().SetAllPartnerSupportPasswords(pxLoginScope.UserName, oldPassword, newPassword);
      pxLoginScope.UserName = userName;
      this._pxLogin.TrackLoginWithPasswordChange(pxLoginScope.UserName);
      userName = PXContext.PXIdentity.User.Identity.Name;
      this._pxLogin.FinishLogin(contextBase, pxLoginScope.UserName, pxLoginScope.CompanyName, pxLoginScope.Branch);
      user = PXContext.PXIdentity.User;
    }
    if (user == null)
      return;
    PXContext.PXIdentity.SetUser(user);
  }

  public void InitUserEnvironment(string userName, string localeName)
  {
    this._pxLogin.InitUserEnvironment(userName, localeName);
    HttpContextBase contextBase = HttpContext.Current.GetContextBase();
    this._formsAuthenticationService.RedirectFromLoginPage(contextBase.Request, contextBase.Response, userName, false);
  }

  public void LogoutCurrentUser()
  {
    HttpSessionState session = HttpContext.Current.Session;
    this.LogoutUser(this._currentUserInformationProvider.GetUserName(), session);
    session.Abandon();
  }

  public void LogoutUser(string userName)
  {
    HttpContext current = HttpContext.Current;
    this.LogoutUser(userName, current.Session);
    this._sessionContextFactory.Abandon(current);
  }

  private void LogoutUser(string userName, HttpSessionState session)
  {
    this._pxLogin.LogoutUser(userName, new AspNetSession(session));
  }

  public string FindUserByHash(string hash, string login)
  {
    if (string.IsNullOrEmpty(hash))
      return (string) null;
    using (new PXLoginScope(login, Array.Empty<string>()))
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle(typeof (Users), new PXDataField("Username"), new PXDataField("PasswordRecoveryExpirationDate"), (PXDataField) new PXDataFieldValue("GuidForPasswordRecovery", (object) hash)))
      {
        if (pxDataRecord != null && !pxDataRecord.IsDBNull(0))
        {
          System.DateTime? dateTime1 = pxDataRecord.GetDateTime(1);
          if (dateTime1.HasValue)
          {
            System.DateTime now = PXTimeZoneInfo.Now;
            dateTime1 = pxDataRecord.GetDateTime(1);
            System.DateTime dateTime2 = dateTime1.Value;
            if (!(now > dateTime2))
              return pxDataRecord.GetString(0);
          }
        }
        throw new PXPasswordRecoveryExpiredException();
      }
    }
  }

  public string FindQuestionByUsername(string username, string login)
  {
    if (string.IsNullOrEmpty(username))
      return (string) null;
    using (new PXLoginScope(login, Array.Empty<string>()))
      return this._userManagementService.GetUser(username).PasswordQuestion;
  }

  public bool ValidateAnswer(string userName, string answer)
  {
    using (PXLoginScope pxLoginScope = new PXLoginScope(userName, Array.Empty<string>()))
    {
      using (PXDataRecord pxDataRecord = this._userManagementService.SelectSMUser(pxLoginScope.UserName, new PXDataField("PasswordAnswer")))
        return pxDataRecord == null || pxDataRecord.IsDBNull(0) || string.Equals(pxDataRecord.GetString(0), answer, StringComparison.OrdinalIgnoreCase);
    }
  }

  public void SendUserLogin(string company, string userEmail, string linkToSend)
  {
    using (PXLoginScope pxLoginScope = new PXLoginScope(string.IsNullOrWhiteSpace(company) ? (string) null : "temp@" + company, Array.Empty<string>()))
    {
      Access instance = PXGraph.CreateInstance<Access>();
      instance.UserList.Current = (Users) instance.UserList.Search<Users.username>((object) pxLoginScope.UserName);
      foreach (PXResult<Users> pxResult in PXSelectBase<Users, PXSelect<Users, Where<Users.email, Equal<Required<Users.email>>>>.Config>.Select((PXGraph) instance, (object) userEmail))
      {
        Users users = (Users) pxResult;
        users.RecoveryLink = linkToSend;
        instance.UserList.Current = users;
        instance.SendUserLogin.Press();
      }
    }
  }

  public void SendUserPassword(string userLogin, string linkToSend, string paramName)
  {
    using (PXLoginScope pxLoginScope = new PXLoginScope(userLogin, Array.Empty<string>()))
    {
      Access instance = PXGraph.CreateInstance<Access>();
      instance.UserList.Current = (Users) instance.UserList.Search<Users.username>((object) pxLoginScope.UserName);
      if (instance.UserList.Current == null)
        return;
      string hash = LoginUiService.CreateHash(Guid.NewGuid().ToString("N"));
      this._userManagementService.UpdateUser(pxLoginScope.UserName, false, new PXDataFieldAssign("GuidForPasswordRecovery", PXDbType.VarChar, new int?(hash.Length), (object) hash), new PXDataFieldAssign("PasswordRecoveryExpirationDate", PXDbType.DateTime, (object) PXTimeZoneInfo.Now.AddDays(1.0)));
      string recoveryUrl = this.GenerateRecoveryUrl(instance, linkToSend, paramName, hash);
      instance.UserList.Current.RecoveryLink = recoveryUrl;
      instance.SendUserPassword.Press();
    }
  }

  protected virtual string GenerateRecoveryUrl(
    Access access,
    string linkToSend,
    string paramName,
    string hash)
  {
    string str1 = linkToSend;
    string str2;
    if (linkToSend.IndexOf('?') != -1)
      str2 = $"{str1}&{paramName}={hash}";
    else
      str2 = $"{str1}?{paramName}={hash}";
    return str2.Replace(' ', '+');
  }

  /// <summary>
  /// Creates an SHA1 hash from user PKID which can be sent through e-mail and writes it into database for further check.
  /// </summary>
  /// <param name="value">Value to compute hash for (user PKID).</param>
  /// <param name="UserLogin">User name (login).</param>
  /// <returns>A string containing SHA1 hash computed from user PKID.</returns>
  private static string CreateHash(string value)
  {
    byte[] hash1 = new SHA1Managed().ComputeHash(new UnicodeEncoding().GetBytes(value));
    string hash2 = "";
    foreach (byte num in hash1)
      hash2 += num.ToString();
    return hash2;
  }

  public bool EulaRequired(string userName)
  {
    if (!string.Equals(WebConfigurationManager.AppSettings["AskEula"], "true", StringComparison.OrdinalIgnoreCase))
      return false;
    using (new PXLoginScope(userName, Array.Empty<string>()))
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle(typeof (EulaStatus), (PXDataField) new PXDataFieldValue("PKID", this._userManagementService.GetUserAndMarkOnline(userName).ProviderUserKey), new PXDataField("Date")))
        return pxDataRecord == null || pxDataRecord.IsDBNull(0);
    }
  }

  public void AgreeToEula(string userName)
  {
    if (!string.Equals(WebConfigurationManager.AppSettings["AskEula"], "true", StringComparison.OrdinalIgnoreCase))
      return;
    PXDatabase.Insert<EulaStatus>(new PXDataFieldAssign("PKID", PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), this._userManagementService.GetUserAndMarkOnline(userName).ProviderUserKey), new PXDataFieldAssign("Date", PXDbType.DateTime, new int?(8), (object) System.DateTime.Now.ToUniversalTime()), new PXDataFieldAssign("IPAddress", PXDbType.VarChar, new int?(50), (object) "127.0.0.1"));
  }

  string ILoginUiService.GetCompanyIdFromCookie(HttpRequest request)
  {
    string str = request.Cookies["CompanyID"]?.Value;
    return string.IsNullOrEmpty(str) ? (string) null : str;
  }

  void ILoginUiService.ClearCompanyIdInCookie(HttpResponse response)
  {
    response.SetCookie(new HttpCookie("CompanyID")
    {
      Value = "",
      Expires = System.DateTime.Now.AddDays(-1.0)
    });
  }

  string ILoginUiService.GetPathWithoutCompanyId(HttpRequest request)
  {
    return string.IsNullOrEmpty(request.QueryString["CompanyID"]) ? request.Url.PathAndQuery : QueryHelpers.AddQueryString(request.Url.AbsolutePath, (IDictionary<string, string>) ((IEnumerable<string>) request.QueryString.AllKeys).Where<string>((Func<string, bool>) (key => !"CompanyID".Equals(key, StringComparison.OrdinalIgnoreCase))).ToDictionary<string, string, string>((Func<string, string>) (key => key), (Func<string, string>) (key => request.QueryString[key])));
  }
}
