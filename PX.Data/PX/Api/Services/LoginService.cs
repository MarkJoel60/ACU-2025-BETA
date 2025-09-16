// Decompiled with JetBrains decompiler
// Type: PX.Api.Services.LoginService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.Access.ActiveDirectory;
using PX.Data.Api.Models;
using PX.Data.Auth;
using PX.EP;
using PX.Export.Authentication;
using PX.Security;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

#nullable disable
namespace PX.Api.Services;

[PXInternalUseOnly]
public class LoginService : ILoginService
{
  private readonly ICompanyService _companyService;
  private readonly IFormsAuthenticationService _formsAuthenticationService;
  private readonly IUserManagementService _userManagementService;
  private readonly ICurrentUserInformationProvider _currentUserInformationProvider;
  private readonly IActiveDirectoryProvider _activeDirectoryProvider;
  private readonly IPXLogin _pxLogin;
  private readonly ISessionContextFactory _sessionContextFactory;

  internal LoginService(
    ICompanyService companyService,
    IFormsAuthenticationService formsAuthenticationService,
    IUserManagementService userManagementService,
    ICurrentUserInformationProvider currentUserInformationProvider,
    IActiveDirectoryProvider activeDirectoryProvider,
    IPXLogin pxLogin,
    ISessionContextFactory sessionContextFactory)
  {
    this._companyService = companyService;
    this._formsAuthenticationService = formsAuthenticationService;
    this._userManagementService = userManagementService;
    this._currentUserInformationProvider = currentUserInformationProvider;
    this._activeDirectoryProvider = activeDirectoryProvider;
    this._pxLogin = pxLogin;
    this._sessionContextFactory = sessionContextFactory;
  }

  public static string DetermineLocale(string login, string locale)
  {
    if (string.IsNullOrEmpty(locale))
      locale = (string) null;
    PXLocale[] source = string.IsNullOrEmpty(login) ? PXLocalesProvider.GetLocales() : PXLocalesProvider.GetLocales(login);
    if (source != null && ((IEnumerable<PXLocale>) source).Any<PXLocale>())
    {
      if (locale != null && !((IEnumerable<PXLocale>) source).Any<PXLocale>((Func<PXLocale, bool>) (l => l.Name.OrdinalEquals(locale))))
      {
        if (locale.Contains<char>('-'))
        {
          string[] splitted = locale.Split('-');
          PXLocale pxLocale = ((IEnumerable<PXLocale>) source).FirstOrDefault<PXLocale>((Func<PXLocale, bool>) (l => l.Name.StartsWith(splitted[0])));
          locale = pxLocale == null ? (string) null : pxLocale.Name;
        }
        else
          locale = (string) null;
      }
    }
    else
      locale = (string) null;
    if (locale == null)
      locale = source == null || !((IEnumerable<PXLocale>) source).Any<PXLocale>() ? Thread.CurrentThread.CurrentCulture.Name : ((IEnumerable<PXLocale>) source).First<PXLocale>().Name;
    return locale;
  }

  void ILoginService.Login(
    string login,
    string password,
    string company,
    string branch,
    string locale,
    string prefix)
  {
    if (!this.TryLoginUserImpl(ref login, password, company, branch, ref locale))
      throw new PXNeedChangePasswordException();
    PXDatabase.SelectTimeStamp();
    this._pxLogin.InitUserEnvironment(login, locale);
    HttpContextBase contextBase = HttpContext.Current.GetContextBase();
    this._formsAuthenticationService.SetAuthCookie(contextBase.Request, contextBase.Response, login, false, prefix);
  }

  /// <remarks>Return/exception behavior same as <see cref="M:PX.Data.IPXLogin.LoginUser(System.String@,System.String)" />: will throw for most validation errors</remarks>
  private bool TryLoginUserImpl(
    ref string login,
    string password,
    string company,
    string branch,
    ref string locale)
  {
    login = this.GetLoginWithCompany(login, company, branch, password);
    locale = LoginService.DetermineLocale(login, locale);
    return this._pxLogin.LoginUser(ref login, password);
  }

  IPrincipal ILoginService.TryLoginUser(
    string login,
    string password,
    string company,
    string branch,
    string locale)
  {
    this.TryLoginUserImpl(ref login, password, company, branch, ref locale);
    return PXContext.PXIdentity.AuthUser;
  }

  public void Logout()
  {
    HttpContextBase contextBase = HttpContext.Current.GetContextBase();
    AspNetSession from = AspNetSession.TryCreateFrom(contextBase);
    if (from != null)
      this._pxLogin.LogoutUser(this._currentUserInformationProvider.GetUserName(), from);
    this._formsAuthenticationService.SignOut(contextBase.Request, contextBase.Response);
    try
    {
      this._sessionContextFactory.Abandon(contextBase);
      contextBase.Session.Abandon();
      string name = "ASP.NET_SessionId";
      SessionStateSection section = (SessionStateSection) WebConfigurationManager.GetSection("system.web/sessionState");
      if (section != null)
        name = section.CookieName;
      contextBase.Response.Cookies.Set(new HttpCookie(name, "")
      {
        Expires = System.DateTime.Now.AddYears(-1)
      });
    }
    catch
    {
    }
  }

  public bool IsUserAuthenticated(bool throwException, string prefix = "")
  {
    if ((HttpContext.Current != null ? (Anonymous.IsAnonymous(HttpContext.Current.User) ? 1 : 0) : (Anonymous.IsAnonymous(Thread.CurrentPrincipal) ? 1 : 0)) != 0)
    {
      if (!throwException)
        return false;
      throw new PXNotLoggedInException();
    }
    bool? nullable = LoginService.CheckPrefix(prefix);
    bool flag = false;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return true;
    if (!throwException)
      return false;
    throw new PXNotLoggedInException();
  }

  private static bool? CheckPrefix(string prefix)
  {
    return !string.IsNullOrEmpty(prefix) && HttpContext.Current != null && HttpContext.Current.User != null ? LoginService.CheckPrefix(HttpContext.Current.User, prefix) : new bool?();
  }

  internal static bool? CheckPrefix(IPrincipal user, string prefix)
  {
    return user.Identity is FormsIdentity identity ? new bool?(identity.CheckPrefix(prefix)) : new bool?();
  }

  public static bool HasMobileIdentity() => LoginService.CheckPrefix("mobile").GetValueOrDefault();

  public PXLoginScope GetAdminLoginScope(string company = null)
  {
    return !this.IsMultiCompany ? new PXLoginScope("admin", PXAccess.GetAdministratorRoles()) : new PXLoginScope(this.GetLoginWithCompany("admin", company, (string) null, ""), Array.Empty<string>());
  }

  public string[] GetCompanies(string login, string password)
  {
    return PXDatabase.AvailableCompanies.Length == 0 ? PXDatabase.AvailableCompanies : this.GetCompaniesSecured(login, password);
  }

  private string[] GetCompaniesSecured(string userName, string password)
  {
    List<string> companies = new List<string>();
    this.ProcessForValidCompanies(userName, password, new Action<string, string>(ProcessDelegate));
    return companies.ToArray();

    void ProcessDelegate(string companyId, string originalUserName) => companies.Add(companyId);
  }

  private void ProcessForValidCompanies(
    string userName,
    string password,
    Action<string, string> processDelegate)
  {
    foreach (string availableCompany in PXDatabase.AvailableCompanies)
    {
      PXDatabase.ResetCredentials();
      userName = this.ProcessForCompany(userName, password, processDelegate, availableCompany);
    }
    PXDatabase.ResetCredentials();
  }

  private string ProcessForCompany(
    string userName,
    string password,
    Action<string, string> processDelegate,
    string companyID)
  {
    string userName1 = userName;
    if (!string.IsNullOrEmpty(companyID))
      userName1 = $"{userName1}@{companyID}";
    using (new PXLoginScope(userName1, Array.Empty<string>()))
    {
      bool flag = ((IUserValidationService) this._userManagementService).ValidateUserPasswordWithoutUserRestrictions(userName, password, ref userName);
      if (flag)
      {
        bool isADUser = false;
        bool isClaimUser = false;
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Users>(new PXDataField("Source"), (PXDataField) new PXDataFieldValue("Username", PXDbType.NVarChar, new int?(64 /*0x40*/), (object) userName)))
        {
          if (pxDataRecord != null)
          {
            int? int32_1 = pxDataRecord.GetInt32(0);
            int num1 = 1;
            isADUser = int32_1.GetValueOrDefault() == num1 & int32_1.HasValue;
            int? int32_2 = pxDataRecord.GetInt32(0);
            int num2 = 2;
            isClaimUser = int32_2.GetValueOrDefault() == num2 & int32_2.HasValue;
          }
        }
        flag = this._activeDirectoryProvider.CheckUserRoles(userName, isADUser, isClaimUser);
      }
      if (flag)
        processDelegate(companyID, userName);
    }
    return userName;
  }

  public string[] GetCompanies()
  {
    return this._currentUserInformationProvider.GetLicensedAccessibleCompanies();
  }

  private string GetPossibleCompany(string login)
  {
    int num = login.LastIndexOf('@');
    return login.Substring(num + 1);
  }

  private string GetPossibleLogin(string login)
  {
    int length = login.LastIndexOf('@');
    return login.Substring(0, length);
  }

  public string GetLoginWithCompany(string login, string company, string branch, string password = "")
  {
    if (login.Contains(":"))
    {
      int length = login.LastIndexOf(':');
      string str = login.Substring(length + 1);
      if (string.IsNullOrWhiteSpace(branch) || branch == str)
      {
        login = login.Substring(0, length);
        branch = str;
      }
    }
    string possibleCompany = company;
    string userName = login;
    if (login.Contains("@"))
    {
      possibleCompany = this.GetPossibleCompany(login);
      userName = this.GetPossibleLogin(login);
    }
    string[] source = (string[]) null;
    if (this.IsMultiCompany)
    {
      if (!string.IsNullOrEmpty(password))
      {
        source = this.GetCompaniesSecured(userName, password);
        if (source == null || !((IEnumerable<string>) source).Any<string>())
        {
          userName = login;
          source = this.GetCompaniesSecured(userName, password);
          if (source == null || !((IEnumerable<string>) source).Any<string>())
          {
            source = PXDatabase.Companies;
            if (login.Contains("@"))
            {
              possibleCompany = this.GetPossibleCompany(login);
              userName = this.GetPossibleLogin(login);
            }
          }
        }
      }
      else
        source = PXDatabase.Companies;
    }
    if (login.Contains("@") && string.IsNullOrWhiteSpace(company))
    {
      if (!this.IsMultiCompany)
      {
        if (this._companyService.GetSingleCompanyLoginName().OrdinalEquals(possibleCompany))
        {
          login = userName;
          company = possibleCompany;
        }
      }
      else if (source != null && ((IEnumerable<string>) source).Any<string>() && ((IEnumerable<string>) source).Any<string>((Func<string, bool>) (c => c.OrdinalEquals(possibleCompany))))
      {
        login = userName;
        company = possibleCompany;
      }
    }
    if (this.IsMultiCompany)
    {
      if (string.IsNullOrEmpty(company))
        company = source[0];
      login = $"{login}@{company}";
    }
    if (!string.IsNullOrEmpty(branch))
      login = $"{login}:{branch}";
    return login;
  }

  public bool IsMultiCompany => this._companyService.IsMultiCompany;

  public IEnumerable<BranchMeta> GetBranches()
  {
    return this._currentUserInformationProvider.GetActiveBranches().Select<BranchInfo, BranchMeta>((Func<BranchInfo, BranchMeta>) (b => new BranchMeta()
    {
      Id = b.Cd.Trim(),
      DisplayName = b.Name,
      ThemeVariables = this.GetBranchThemeVariables(b.Cd)
    }));
  }

  private CompanyMeta GetCompanyWithOrganizations(string company)
  {
    IEnumerable<PXAccess.MasterCollection.Organization> organizations = this._currentUserInformationProvider.GetOrganizations();
    OrganizationMeta[] organizationMetaArray;
    if (organizations == null)
    {
      organizationMetaArray = (OrganizationMeta[]) null;
    }
    else
    {
      IEnumerable<PXAccess.MasterCollection.Organization> source = organizations.Where<PXAccess.MasterCollection.Organization>((Func<PXAccess.MasterCollection.Organization, bool>) (_ =>
      {
        bool? deletedDatabaseRecord = _.DeletedDatabaseRecord;
        bool flag = true;
        return !(deletedDatabaseRecord.GetValueOrDefault() == flag & deletedDatabaseRecord.HasValue);
      }));
      organizationMetaArray = source != null ? source.Select<PXAccess.MasterCollection.Organization, OrganizationMeta>(new Func<PXAccess.MasterCollection.Organization, OrganizationMeta>(this.GetOrganizationMeta)).ToArray<OrganizationMeta>() : (OrganizationMeta[]) null;
    }
    OrganizationMeta[] source1 = organizationMetaArray;
    CompanyMeta withOrganizations = new CompanyMeta()
    {
      DisplayName = company,
      Organizations = (IEnumerable<OrganizationMeta>) source1
    };
    if (!((IEnumerable<OrganizationMeta>) source1).Any<OrganizationMeta>())
      withOrganizations.ThemeVariables = this.GetBranchThemeVariables((string) null);
    return withOrganizations;
  }

  public IEnumerable<object> GetBranchTree()
  {
    if (this._companyService.IsMultiCompany)
    {
      string[] companies = this.GetCompanies();
      if (companies != null && companies.Length != 0)
      {
        for (int i = 0; i < companies.Length; ++i)
        {
          using (new PXLoginScope($"{this._currentUserInformationProvider.GetUserName()}@{companies[i]}", Array.Empty<string>()))
          {
            PXDatabase.SelectTimeStamp();
            yield return (object) this.GetCompanyWithOrganizations(companies[i]);
          }
        }
      }
      companies = (string[]) null;
    }
    else
      yield return (object) this.GetCompanyWithOrganizations(this.GetCurrentCompany());
  }

  private OrganizationMeta GetOrganizationMeta(
    PXAccess.MasterCollection.Organization organization)
  {
    if (organization == null || organization.ChildBranches == null)
      return (OrganizationMeta) null;
    PXAccess.MasterCollection.Branch[] array = organization.ChildBranches.Where<PXAccess.MasterCollection.Branch>((Func<PXAccess.MasterCollection.Branch, bool>) (_ => !_.DeletedDatabaseRecord)).ToArray<PXAccess.MasterCollection.Branch>();
    if (((IEnumerable<PXAccess.MasterCollection.Branch>) array).Count<PXAccess.MasterCollection.Branch>() == 1 && ((IEnumerable<PXAccess.MasterCollection.Branch>) array).Any<PXAccess.MasterCollection.Branch>((Func<PXAccess.MasterCollection.Branch, bool>) (_ => organization.OrganizationName.OrdinalEquals(_.BranchName))))
    {
      string branchCD = ((IEnumerable<PXAccess.MasterCollection.Branch>) array).First<PXAccess.MasterCollection.Branch>().BranchCD.Trim();
      return new OrganizationMeta()
      {
        DisplayName = organization.OrganizationName,
        Id = branchCD,
        ThemeVariables = this.GetBranchThemeVariables(branchCD)
      };
    }
    return new OrganizationMeta()
    {
      DisplayName = organization.OrganizationName,
      Branches = ((IEnumerable<PXAccess.MasterCollection.Branch>) array).Select<PXAccess.MasterCollection.Branch, BranchMeta>((Func<PXAccess.MasterCollection.Branch, BranchMeta>) (branch =>
      {
        string branchCD = branch.BranchCD.Trim();
        Dictionary<string, string> branchThemeVariables = this.GetBranchThemeVariables(branchCD);
        return new BranchMeta()
        {
          DisplayName = branch.BranchName,
          Id = branchCD,
          ThemeVariables = branchThemeVariables
        };
      }))
    };
  }

  public Dictionary<string, string> GetBranchThemeVariables(string branchCD)
  {
    return PXThemeLoader.GetThemeVariablesDictionary(branchCD);
  }

  public bool ValidateUser(string login)
  {
    return this._userManagementService.IsUserValid(login, out string _);
  }

  public void InitUserEnvironment(string login) => this.InitUserEnvironment(login, (string) null);

  public void InitUserEnvironment(string login, string locale)
  {
    using (new PXLoginScope(login, Array.Empty<string>()))
    {
      locale = LoginService.DetermineLocale(login, locale);
      PXDatabase.SelectTimeStamp();
      this._pxLogin.InitUserEnvironment(login, locale);
    }
  }

  public string GetCurrentCompany()
  {
    string currentCompany = this.TryGetCurrentCompany();
    return !string.IsNullOrWhiteSpace(currentCompany) ? currentCompany : throw new InvalidOperationException("Can't get current company login name");
  }

  public string TryGetCurrentCompany()
  {
    return !this._companyService.IsMultiCompany ? this._companyService.GetSingleCompanyLoginName() : PXAccess.GetCompanyName();
  }

  public IReadOnlyList<(int companyId, Guid userId, int twoFactorLevel, bool passwordChange)> GetUserIdsWithTwoFactorType(
    string userName,
    string password)
  {
    List<(int, Guid, int, bool)> ids = new List<(int, Guid, int, bool)>();
    if (PXDatabase.AvailableCompanies.Length != 0)
      this.ProcessForValidCompanies(userName, password, new Action<string, string>(ProcessDelegate));
    else
      this.ProcessForCompany(userName, password, new Action<string, string>(ProcessDelegate), (string) null);
    return (IReadOnlyList<(int, Guid, int, bool)>) ids;

    void ProcessDelegate(string companyLogin, string originalUserName)
    {
      (int, Guid, int, bool) valueTuple = (0, Guid.Empty, 0, false);
      int? nullable = new int?();
      int num1 = 0;
      bool flag1 = false;
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Users>(new PXDataField("PKID"), new PXDataField("CompanyID"), new PXDataField("multiFactorType"), new PXDataField("PasswordChangeOnNextLogin"), new PXDataField("PasswordNeverExpires"), new PXDataField("LastPasswordChangedDate"), new PXDataField("Password"), new PXDataField("Source"), new PXDataField("LoginTypeID"), (PXDataField) new PXDataFieldValue("Username", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) originalUserName)))
      {
        if (pxDataRecord != null)
        {
          IPXLogin pxLogin = this._pxLogin;
          int? int32 = pxDataRecord.GetInt32(7);
          string password = pxDataRecord.GetString(6);
          bool? boolean = pxDataRecord.GetBoolean(3);
          int num2 = boolean.Value ? 1 : 0;
          boolean = pxDataRecord.GetBoolean(4);
          int num3 = boolean.Value ? 1 : 0;
          System.DateTime? dateTime = pxDataRecord.GetDateTime(5);
          bool flag2 = pxLogin.IsPasswordChangeRequired(int32, password, num2 != 0, num3 != 0, dateTime);
          valueTuple = (pxDataRecord.GetInt32(1).Value, pxDataRecord.GetGuid(0).Value, pxDataRecord.GetInt32(2).Value, flag2);
          flag1 = true;
          nullable = pxDataRecord.GetInt32(8);
          num1 = pxDataRecord.GetInt32(1).Value;
        }
      }
      if (nullable.HasValue)
      {
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<EPLoginType>(new PXDataField("DisableTwoFactorAuth"), new PXDataField("AllowedLoginType"), (PXDataField) new PXDataFieldValue("CompanyID", (object) num1), (PXDataField) new PXDataFieldValue("LoginTypeID", (object) nullable)))
        {
          if (pxDataRecord != null)
          {
            bool? boolean = pxDataRecord.GetBoolean(0);
            bool flag3 = true;
            if (!(boolean.GetValueOrDefault() == flag3 & boolean.HasValue))
            {
              if (!(pxDataRecord.GetString(1) == "A"))
                goto label_16;
            }
            valueTuple.Item3 = 0;
          }
        }
      }
label_16:
      if (!flag1)
        return;
      ids.Add(valueTuple);
    }
  }
}
