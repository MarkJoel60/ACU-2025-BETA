// Decompiled with JetBrains decompiler
// Type: PX.Licensing.PXLicenseObserver
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Options;
using PX.AspNetCore;
using PX.AspNetCore.RouteConstraints;
using PX.Common;
using PX.Common.Services;
using PX.Data;
using PX.Data.Update;
using PX.EP;
using PX.Logging.Sinks.SystemEventsDbSink;
using PX.Security;
using PX.SM;
using PX.SP;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

#nullable disable
namespace PX.Licensing;

internal class PXLicenseObserver
{
  private readonly ILogger _logger;
  private readonly Func<ILicenseService> _licenseServiceFactory;
  private readonly ILicensingManager _licensingManager;
  private readonly ILegacySignOutManager _legacySignOutManager;
  private readonly IUserManagementService _userManagementService;
  private readonly ILegacyCompanyService _legacyCompanyService;
  private readonly IPortalService _portalService;
  private readonly LicensingOptions _options;
  private const string API_SESSION_PREFIX = "API_";
  private static readonly string _wardenKey = "LicenseWardenKey";
  private static readonly System.Type[] _wardenTypes = new System.Type[3]
  {
    typeof (Warden),
    typeof (PX.SM.Licensing),
    typeof (PX.Data.Update.Company)
  };
  private static readonly ConcurrentDictionary<string, PXLicenseObserver.CacheRequestInfo> _wardenSingleton = new ConcurrentDictionary<string, PXLicenseObserver.CacheRequestInfo>();

  public PXLicenseObserver(
    ILogger licensingLogger,
    Func<ILicenseService> licenseServiceFactory,
    ILicensingManager licensingManager,
    ILegacySignOutManager legacySignOutManager,
    IUserManagementService userManagementService,
    ILegacyCompanyService legacyCompanyService,
    IPortalService portalService,
    IOptions<LicensingOptions> licensingOptions)
  {
    this._logger = licensingLogger;
    this._licenseServiceFactory = licenseServiceFactory;
    this._licensingManager = licensingManager;
    this._legacySignOutManager = legacySignOutManager;
    this._userManagementService = userManagementService;
    this._legacyCompanyService = legacyCompanyService;
    this._portalService = portalService;
    this._options = licensingOptions.Value;
  }

  private static PXLicenseObserver.CacheRequestInfo CacheInfo
  {
    get
    {
      if (WebConfig.IsClusterEnabled)
        return (PXLicenseObserver.CacheRequestInfo) PXDatabase.GetSlot<PXLicenseObserver.DatabaseCacheRequestInfo>(PXLicenseObserver._wardenKey, PXLicenseObserver._wardenTypes);
      string key = string.Empty;
      if (PXAccess.IsMultiDbMode)
        key = PXInstanceHelper.DatabaseName ?? string.Empty;
      return PXLicenseObserver._wardenSingleton.GetOrAdd(key, new PXLicenseObserver.CacheRequestInfo());
    }
  }

  private string GetSessionKey(string identityName)
  {
    ILicenseService ilicenseService;
    try
    {
      ilicenseService = this._licenseServiceFactory();
    }
    catch (Exception ex)
    {
      this._logger.ForMethodContext(nameof (GetSessionKey)).Error<System.Type>(ex, "Error while getting {Service}", typeof (ILicenseService));
      throw;
    }
    try
    {
      if (!ilicenseService.HandleApiCallInSeparateSession())
        return identityName;
    }
    catch (Exception ex)
    {
      this._logger.ForMethodContext(nameof (GetSessionKey)).Error<string, string>(ex, "Error while calling {Method} from {Service}", "HandleApiCallInSeparateSession", ilicenseService.GetType().FullName);
      throw;
    }
    this._logger.ForMethodContext(nameof (GetSessionKey)).Verbose<string, string>("Prepending session prefix {SessionPrefix} to {IdentityName}", "API_", identityName);
    return "API_" + identityName;
  }

  internal void OnAuthenticate()
  {
    bool concurrentUserMode;
    string sessionId;
    string identityName;
    string[] array;
    bool flag;
    PXLicenseObserver.CacheRequestInfo cacheInfo;
    try
    {
      if (HttpContext.Current == null || HttpContext.Current.ApplicationInstance == null)
        return;
      concurrentUserMode = this._options.ConcurrentUserMode;
      HttpSessionState session = HttpContext.Current.Session;
      ILicensingSession licensingSession = session != null ? session.ToLicensingSession() : (ILicensingSession) null;
      if (licensingSession == null || HttpContext.Current.IsApiRequest())
        return;
      sessionId = licensingSession.SessionId;
      PXSessionContext pxIdentity = PXContext.PXIdentity;
      if (pxIdentity == null || !pxIdentity.Authenticated)
        return;
      identityName = pxIdentity.IdentityName;
      PXLicense license = this._licensingManager.License;
      if (license == null)
      {
        this._logger.ForMethodContext(nameof (OnAuthenticate)).Warning<string>("{ShortCircuitReason}, short-circuiting", "ILicensingManager.License is null");
        return;
      }
      array = license.Trials == null ? (string[]) null : license.Trials.ToArray<string>();
      flag = license.IsUnlimitedUser();
      cacheInfo = PXLicenseObserver.CacheInfo;
      if (cacheInfo == null)
      {
        this._logger.ForMethodContext(nameof (OnAuthenticate)).Warning<string>("{ShortCircuitReason}, short-circuiting", "PXLicenseObserver.CacheInfo is null");
        return;
      }
    }
    catch (Exception ex)
    {
      this._logger.ForMethodContext(nameof (OnAuthenticate)).Warning<string>(ex, "{ShortCircuitReason}, short-circuiting", "Exception in initialization");
      return;
    }
    ILogger ilogger = this._logger.ForMethodContext(nameof (OnAuthenticate));
    string company = this._legacyCompanyService.ExtractCompany(identityName);
    if (!string.IsNullOrEmpty(company))
    {
      PXLicenseObserver.CompanyRequestInfo info;
      if (cacheInfo.companies.TryGetValue(company, out info))
      {
        PXLicenseObserver.UnExpireInfo(cacheInfo, info);
        ilogger.Verbose<string, string>("Unexpired {Key} request in {Collection}", company, "companies");
      }
      else
        ilogger.Verbose<string, string>("Request for {Key} not found in {Collection}", company, "companies");
    }
    if ((company == null || array == null ? 0 : (((IEnumerable<string>) array).Contains<string>(company) ? 1 : 0)) == 0)
    {
      if (!flag)
      {
        using (ilogger.IsEnabled((LogEventLevel) 0) ? LogContext.PushProperty("ConcurrentUserMode", (object) concurrentUserMode, false) : (IDisposable) null)
        {
          if (concurrentUserMode)
          {
            string sessionKey = this.GetSessionKey(identityName);
            PXLicenseObserver.SessionRequestInfo info1;
            if (cacheInfo.sessions.TryGetValue(sessionKey, out info1))
            {
              PXLicenseObserver.UnExpireInfo(cacheInfo, (PXLicenseObserver.CompanyRequestInfo) info1);
              ilogger.Verbose<string, string>("Unexpired {Key} request in {Collection}", sessionKey, "sessions");
              PXLicenseObserver.UserRequestInfo info2;
              if (info1.users.TryGetValue(sessionId, out info2))
              {
                PXLicenseObserver.UnExpireInfo(cacheInfo, (PXLicenseObserver.CompanyRequestInfo) info2);
                ilogger.Verbose<string, string>("Unexpired {Key} request in {Collection}", sessionId, "users");
              }
              else
                ilogger.Verbose<string, string>("Request for {Key} not found in {Collection}", sessionId, "users");
            }
            else
              ilogger.Verbose<string, string>("Request for {Key} not found in {Collection}", identityName, "sessions");
          }
          else
          {
            PXLicenseObserver.UserRequestInfo info;
            if (cacheInfo.users.TryGetValue(sessionId, out info))
            {
              PXLicenseObserver.UnExpireInfo(cacheInfo, (PXLicenseObserver.CompanyRequestInfo) info);
              ilogger.Verbose<string, string>("Unexpired {Key} request in {Collection}", sessionId, "users");
            }
            else
              ilogger.Verbose<string, string>("Request for {Key} not found in {Collection}", sessionId, "users");
          }
        }
      }
      else
        ilogger.Verbose("Unlimited user mode, no user/session unexpiration");
    }
    else
    {
      using (ilogger.IsEnabled((LogEventLevel) 0) ? LogContext.PushProperty("UnlicensedCompany", (object) true, false) : (IDisposable) null)
      {
        PXLicenseObserver.UserRequestInfo info;
        if (cacheInfo.trials.TryGetValue(sessionId, out info))
        {
          PXLicenseObserver.UnExpireInfo(cacheInfo, (PXLicenseObserver.CompanyRequestInfo) info);
          ilogger.Verbose<string, string>("Unexpired {Company} request in {Collection}", sessionId, "trials");
        }
        else
          ilogger.Verbose<string, string>("Request for {Company} not found in {Collection}", sessionId, "trials");
      }
    }
    cacheInfo.Flush();
  }

  internal void OnHandler()
  {
    string identityname = (string) null;
    bool concurrentUserMode;
    string sessionId;
    int usersAllowed;
    int companiesAllowed;
    string[] array;
    bool flag1;
    PXLicenseObserver.CacheRequestInfo cacheInfo;
    try
    {
      if (HttpContext.Current == null || HttpContext.Current.Session == null)
        return;
      HttpApplication applicationInstance = HttpContext.Current.ApplicationInstance;
      if (applicationInstance == null || applicationInstance.Context == null || applicationInstance.Request == null || !applicationInstance.Request.IsAuthenticated)
        return;
      concurrentUserMode = this._options.ConcurrentUserMode;
      sessionId = applicationInstance.Session.ToLicensingSession().SessionId;
      if (HttpContext.Current.IsApiRequest())
        return;
      PXSessionContext pxIdentity = PXContext.PXIdentity;
      if (pxIdentity == null || !pxIdentity.Authenticated)
        return;
      identityname = pxIdentity.IdentityName;
      PXLicense license = this._licensingManager.License;
      if (license == null)
      {
        this._logger.ForMethodContext(nameof (OnHandler)).Warning<string>("{ShortCircuitReason}, short-circuiting", "ILicensingManager.License is null");
        return;
      }
      usersAllowed = license.UsersAllowed;
      companiesAllowed = license.CompaniesAllowed;
      array = license.Trials == null ? (string[]) null : license.Trials.ToArray<string>();
      flag1 = license.IsUnlimitedUser();
      cacheInfo = PXLicenseObserver.CacheInfo;
      if (cacheInfo == null)
      {
        this._logger.ForMethodContext(nameof (OnHandler)).Warning<string>("{ShortCircuitReason}, short-circuiting", "PXLicenseObserver.CacheInfo is null");
        return;
      }
    }
    catch (Exception ex)
    {
      this._logger.ForMethodContext(nameof (OnHandler)).Warning<string>(ex, "{ShortCircuitReason}, short-circuiting", "Exception in initialization");
      return;
    }
    MembershipUser user = this._userManagementService.GetUser(identityname);
    if (user != null)
    {
      if (!user.IsApproved)
      {
        this._logger.ForMethodContext(nameof (OnHandler)).Warning<string>("{ShortCircuitReason}, short-circuiting", "user disabled");
        this.LogOut(PXLogOutReason.UserDisabled);
        return;
      }
      PortalInfo portal = this._portalService.GetPortal();
      if (portal != null)
      {
        if (!portal.IsActive || !this._portalService.IsPortalFeatureInstalled() || portal.AccessRole != null && !PXContext.PXIdentity.User.IsInRole(portal.AccessRole))
        {
          this.LogOut(PXLogOutReason.AccessDisabled);
          return;
        }
      }
      else if (user.IsGuest() && !PXSiteMap.IsPortal)
      {
        this.LogOut(PXLogOutReason.AccessDisabled);
        return;
      }
    }
    string company = this._legacyCompanyService.ExtractCompany(identityname);
    bool flag2 = company != null && array != null && ((IEnumerable<string>) array).Contains<string>(company);
    ILogger ilogger = this._logger.ForMethodContext(nameof (OnHandler));
    bool flag3 = ilogger.IsEnabled((LogEventLevel) 3);
    if (!string.IsNullOrEmpty(company) && !flag2)
    {
      PXLicenseObserver.CompanyRequestInfo info;
      if (!cacheInfo.companies.TryGetValue(company, out info))
      {
        cacheInfo.companies[company] = info = new PXLicenseObserver.CompanyRequestInfo();
        PXLicenseObserver.InsertInfo(cacheInfo, info);
        ilogger.Verbose<string, string>("Inserted request for {Key} into {Collection}", company, "companies");
      }
      else
        ilogger.Verbose<string, string>("Request for {Key} found in {Collection}", company, "companies");
      int num = cacheInfo.companies.Count<KeyValuePair<string, PXLicenseObserver.CompanyRequestInfo>>((Func<KeyValuePair<string, PXLicenseObserver.CompanyRequestInfo>, bool>) (u => !u.Value.Expired));
      if (companiesAllowed > 0 && num > companiesAllowed)
      {
        using (flag3 ? LogContext.PushProperty("AllowedCompanies", (object) companiesAllowed, false) : (IDisposable) null)
        {
          using (flag3 ? LogContext.PushProperty("CompanyCount", (object) num, false) : (IDisposable) null)
          {
            HashSet<string> availableCompanies = ((IEnumerable<string>) PXDatabase.AvailableCompanies).ToHashSet<string>();
            HashSet<string> trialCompanies = ((IEnumerable<string>) array).ToHashSet<string>();
            foreach (KeyValuePair<string, PXLicenseObserver.CompanyRequestInfo> keyValuePair in cacheInfo.companies.OrderByDescending<KeyValuePair<string, PXLicenseObserver.CompanyRequestInfo>, int>((Func<KeyValuePair<string, PXLicenseObserver.CompanyRequestInfo>, int>) (c =>
            {
              if (trialCompanies.Contains(c.Key))
                return 0;
              return !availableCompanies.Contains(c.Key) ? 1 : 2;
            })).ThenByDescending<KeyValuePair<string, PXLicenseObserver.CompanyRequestInfo>, System.DateTime>((Func<KeyValuePair<string, PXLicenseObserver.CompanyRequestInfo>, System.DateTime>) (c => c.Value.LastRequest)).Skip<KeyValuePair<string, PXLicenseObserver.CompanyRequestInfo>>(companiesAllowed))
            {
              PXLicenseObserver.ExpireInfo(cacheInfo, keyValuePair.Value, new PXLogOutReason?(PXLogOutReason.CompaniesExceeded));
              ilogger.Warning<string, string>("Expired {Key} request in {Collection}", keyValuePair.Key, "companies");
            }
          }
        }
      }
      else
        ilogger.Verbose<int, int>("Used {CompanyCount} of {AllowedCompanies}, skipping company expiration", num, companiesAllowed);
      if (info.Expired)
      {
        ilogger.Warning<string, string>("{ExpirationUnit} {ExpirationUnitId} is expired, logging out", "Company", company);
        this.LogOut(PXLogOutReason.CompaniesExceeded);
        return;
      }
      PXLicenseObserver.UpdateInfo(cacheInfo, info);
      ilogger.Verbose<string, string>("Updated request for {Key} in {Collection}", company, "companies");
    }
    System.DateTime? lastRequest1 = new System.DateTime?();
    if (!flag2)
    {
      if (!flag1)
      {
        using (ilogger.IsEnabled((LogEventLevel) 3) ? LogContext.PushProperty("ConcurrentUserMode", (object) concurrentUserMode, false) : (IDisposable) null)
        {
          if (concurrentUserMode)
          {
            string sessionKey = this.GetSessionKey(identityname);
            PXLicenseObserver.SessionRequestInfo info1;
            if (!cacheInfo.sessions.TryGetValue(sessionKey, out info1))
            {
              cacheInfo.sessions[sessionKey] = info1 = new PXLicenseObserver.SessionRequestInfo();
              PXLicenseObserver.InsertInfo(cacheInfo, (PXLicenseObserver.CompanyRequestInfo) info1);
              ilogger.Verbose<string, string>("Inserted request for {Key} into {Collection}", sessionKey, "sessions");
            }
            else
              ilogger.Verbose<string, string>("Request for {Key} found in {Collection}", sessionKey, "sessions");
            PXLicenseObserver.UserRequestInfo info2;
            if (!info1.users.TryGetValue(sessionId, out info2))
            {
              ConcurrentDictionary<string, PXLicenseObserver.UserRequestInfo> users = info1.users;
              string key = sessionId;
              PXLicenseObserver.UserRequestInfo userRequestInfo = new PXLicenseObserver.UserRequestInfo();
              userRequestInfo.Username = identityname;
              info2 = userRequestInfo;
              users[key] = userRequestInfo;
              PXLicenseObserver.InsertInfo(cacheInfo, (PXLicenseObserver.CompanyRequestInfo) info2);
              ilogger.Verbose<string, string>("Inserted request for {Key} into {Collection}", sessionId, "users");
            }
            else
            {
              lastRequest1 = PXLicenseObserver.UpdateUserInfo(cacheInfo, info2, identityname, lastRequest1);
              ilogger.Verbose<string, string>("Request for {Key} found in {Collection}", sessionId, "users");
            }
            int num = cacheInfo.sessions.Count<KeyValuePair<string, PXLicenseObserver.SessionRequestInfo>>((Func<KeyValuePair<string, PXLicenseObserver.SessionRequestInfo>, bool>) (u => !u.Value.Expired));
            if (usersAllowed > 0 && num > usersAllowed)
            {
              using (flag3 ? LogContext.PushProperty("AllowedUsers", (object) usersAllowed, false) : (IDisposable) null)
              {
                using (flag3 ? LogContext.PushProperty("UserCount", (object) num, false) : (IDisposable) null)
                {
                  foreach (KeyValuePair<string, PXLicenseObserver.SessionRequestInfo> keyValuePair in cacheInfo.sessions.OrderByDescending<KeyValuePair<string, PXLicenseObserver.SessionRequestInfo>, System.DateTime>((Func<KeyValuePair<string, PXLicenseObserver.SessionRequestInfo>, System.DateTime>) (u => u.Value.LastRequest)).Skip<KeyValuePair<string, PXLicenseObserver.SessionRequestInfo>>(usersAllowed))
                  {
                    PXLicenseObserver.ExpireInfo(cacheInfo, (PXLicenseObserver.CompanyRequestInfo) keyValuePair.Value, new PXLogOutReason?(PXLogOutReason.UsersExceeded));
                    ilogger.Warning<string, string>("Expired {Key} request in {Collection}", keyValuePair.Key, "sessions");
                  }
                }
              }
            }
            else
              ilogger.Verbose<int, int>("Used {UserCount} of {AllowedUsers}, skipping session expiration", num, usersAllowed);
            foreach (KeyValuePair<string, PXLicenseObserver.UserRequestInfo> keyValuePair in info1.users.Where<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>>((Func<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, bool>) (u => !u.Value.Expired)).OrderByDescending<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, System.DateTime>((Func<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, System.DateTime>) (u => u.Value.LastRequest)).Skip<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>>(1))
            {
              PXLicenseObserver.ExpireInfo(cacheInfo, (PXLicenseObserver.CompanyRequestInfo) keyValuePair.Value);
              ilogger.Warning<string, string>("Expired {Key} request in {Collection}", keyValuePair.Key, "users");
            }
            if (info1.Expired || info2.Expired)
            {
              if (info1.Expired)
                ilogger.Warning<string, string>("{ExpirationUnit} {ExpirationUnitId} is expired, logging out", "Session", sessionId);
              if (info2.Expired)
                ilogger.Warning<string, string>("{ExpirationUnit} {ExpirationUnitId} is expired, logging out", "User", identityname);
              this.LogOut(info1.Expired ? PXLogOutReason.ConcurrentLoginsExceeded : PXLogOutReason.UsersExceeded);
              return;
            }
            PXLicenseObserver.UpdateInfo(cacheInfo, (PXLicenseObserver.CompanyRequestInfo) info2);
            ilogger.Verbose<string, string>("Updated request for {Key} in {Collection}", identityname, "users");
          }
          else
          {
            int? nullable1 = new int?();
            int? nullable2 = new int?();
            using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Users>((PXDataField) new PXDataField<Users.allowedSessions>(), (PXDataField) new PXDataField<Users.loginTypeID>(), (PXDataField) new PXDataFieldValue<Users.pKID>((object) user.GetIDOrDefault())))
            {
              if (pxDataRecord != null)
              {
                nullable1 = pxDataRecord.GetInt32(0);
                nullable2 = pxDataRecord.GetInt32(1);
              }
            }
            if (!nullable1.HasValue && nullable2.HasValue)
            {
              using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<EPLoginType>((PXDataField) new PXDataField<EPLoginType.allowedSessions>(), (PXDataField) new PXDataField<EPLoginType.allowedLoginType>(), (PXDataField) new PXDataFieldValue<EPLoginType.loginTypeID>((object) nullable2)))
              {
                if (pxDataRecord != null)
                {
                  nullable1 = pxDataRecord.GetInt32(0);
                  string str = pxDataRecord.GetString(1);
                  if (!string.IsNullOrEmpty(str))
                  {
                    if (str == "A")
                    {
                      ilogger.Warning<string>("User {Key} is API user.", identityname);
                      this.LogOut(PXLogOutReason.UserDisabled);
                      return;
                    }
                  }
                }
              }
            }
            PXLicenseObserver.UserRequestInfo info;
            if (!cacheInfo.users.TryGetValue(sessionId, out info))
            {
              ConcurrentDictionary<string, PXLicenseObserver.UserRequestInfo> users = cacheInfo.users;
              string key = sessionId;
              PXLicenseObserver.UserRequestInfo userRequestInfo = new PXLicenseObserver.UserRequestInfo();
              userRequestInfo.Username = identityname;
              info = userRequestInfo;
              users[key] = userRequestInfo;
              PXLicenseObserver.InsertInfo(cacheInfo, (PXLicenseObserver.CompanyRequestInfo) info);
              ilogger.Verbose<string, string>("Inserted request for {Key} into {Collection}", identityname, "users");
            }
            else
            {
              lastRequest1 = PXLicenseObserver.UpdateUserInfo(cacheInfo, info, identityname, lastRequest1);
              ilogger.Verbose<string, string>("Request for {Key} found in {Collection}", identityname, "users");
            }
            int num1 = cacheInfo.users.Count<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>>((Func<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, bool>) (u => !u.Value.Expired));
            if (usersAllowed > 0 && num1 > usersAllowed)
            {
              using (flag3 ? LogContext.PushProperty("AllowedUsers", (object) usersAllowed, false) : (IDisposable) null)
              {
                using (flag3 ? LogContext.PushProperty("UserCount", (object) num1, false) : (IDisposable) null)
                {
                  foreach (KeyValuePair<string, PXLicenseObserver.UserRequestInfo> keyValuePair in cacheInfo.users.Where<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>>((Func<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, bool>) (u => !u.Value.Expired)).OrderByDescending<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, System.DateTime>((Func<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, System.DateTime>) (u => u.Value.LastRequest)).Skip<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>>(usersAllowed))
                  {
                    PXLicenseObserver.ExpireInfo(cacheInfo, (PXLicenseObserver.CompanyRequestInfo) keyValuePair.Value, new PXLogOutReason?(PXLogOutReason.UsersExceeded));
                    ilogger.Warning<string, string>("Expired {Key} request in {Collection}", keyValuePair.Key, "users");
                  }
                }
              }
            }
            else
              ilogger.Verbose<int, int>("Used {UserCount} of {AllowedUsers}, skipping user expiration", num1, usersAllowed);
            if (nullable1.HasValue)
            {
              int num2 = cacheInfo.users.Count<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>>((Func<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, bool>) (it => !it.Value.Expired && it.Value.Username == identityname));
              if (num2 > nullable1.Value)
              {
                using (flag3 ? LogContext.PushProperty("AllowedSessions", (object) nullable1.Value, false) : (IDisposable) null)
                {
                  using (flag3 ? LogContext.PushProperty("SessionsCount", (object) num2, false) : (IDisposable) null)
                  {
                    foreach (KeyValuePair<string, PXLicenseObserver.UserRequestInfo> keyValuePair in cacheInfo.users.Where<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>>((Func<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, bool>) (u => !u.Value.Expired && u.Value.Username == identityname)).OrderByDescending<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, System.DateTime>((Func<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, System.DateTime>) (u => u.Value.LastRequest)).Skip<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>>(nullable1.Value))
                    {
                      PXLicenseObserver.ExpireInfo(cacheInfo, (PXLicenseObserver.CompanyRequestInfo) keyValuePair.Value, new PXLogOutReason?(PXLogOutReason.ConcurrentLoginsExceeded));
                      ilogger.Warning<string, string>("Expired {Key} request in {Collection}", keyValuePair.Key, "users");
                    }
                  }
                }
              }
            }
            if (info.Expired)
            {
              PXLogOutReason? expireReason = info.ExpireReason;
              if (expireReason.HasValue && expireReason.GetValueOrDefault() == PXLogOutReason.ConcurrentLoginsExceeded)
              {
                ilogger.Warning<string, string>("{ExpirationUnit} {ExpirationUnitId} is expired, logging out", "Session", sessionId);
                this.LogOut(PXLogOutReason.ConcurrentLoginsExceeded);
                return;
              }
              ilogger.Warning<string, string>("{ExpirationUnit} {ExpirationUnitId} is expired, logging out", "User", identityname);
              this.LogOut(PXLogOutReason.UsersExceeded);
              return;
            }
            PXLicenseObserver.UpdateInfo(cacheInfo, (PXLicenseObserver.CompanyRequestInfo) info);
            ilogger.Verbose<string, string>("Updated request for {Key} in {Collection}", identityname, "users");
          }
        }
      }
      else
        ilogger.Verbose("Unlimited user mode, no user/session expiration");
    }
    else
    {
      PXLicenseObserver.UserRequestInfo info;
      if (!cacheInfo.trials.TryGetValue(sessionId, out info))
      {
        ConcurrentDictionary<string, PXLicenseObserver.UserRequestInfo> trials = cacheInfo.trials;
        string key = sessionId;
        PXLicenseObserver.UserRequestInfo userRequestInfo = new PXLicenseObserver.UserRequestInfo();
        userRequestInfo.Username = identityname;
        info = userRequestInfo;
        trials[key] = userRequestInfo;
        PXLicenseObserver.InsertInfo(cacheInfo, (PXLicenseObserver.CompanyRequestInfo) info);
        ilogger.Verbose<string, string>("Inserted request for {Key} into {Collection}", sessionId, "trials");
      }
      else
      {
        lastRequest1 = PXLicenseObserver.UpdateUserInfo(cacheInfo, info, identityname, lastRequest1);
        ilogger.Verbose<string, string>("Request for {Key} found in {Collection}", sessionId, "trials");
      }
      int num = cacheInfo.trials.Count<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>>((Func<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, bool>) (u => !u.Value.Expired));
      if (num > usersAllowed)
      {
        using (flag3 ? LogContext.PushProperty("AllowedUsers", (object) usersAllowed, false) : (IDisposable) null)
        {
          using (flag3 ? LogContext.PushProperty("UserCount", (object) num, false) : (IDisposable) null)
          {
            foreach (KeyValuePair<string, PXLicenseObserver.UserRequestInfo> keyValuePair in cacheInfo.trials.OrderByDescending<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, System.DateTime>((Func<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, System.DateTime>) (u => u.Value.LastRequest)).Skip<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>>(usersAllowed))
            {
              PXLicenseObserver.ExpireInfo(cacheInfo, (PXLicenseObserver.CompanyRequestInfo) keyValuePair.Value, new PXLogOutReason?(PXLogOutReason.UsersExceeded));
              ilogger.Warning<string, string>("Expired {Key} request in {Collection}", keyValuePair.Key, "trials");
            }
          }
        }
      }
      else
        ilogger.Verbose<int, int>("Used {UserCount} of {AllowedUsers}, skipping session expiration", num, usersAllowed);
      if (info.Expired)
      {
        ilogger.Warning<string, string>("{ExpirationUnit} {ExpirationUnitId} is expired, logging out", "Session", sessionId);
        this.LogOut(PXLogOutReason.UsersExceeded);
      }
      else
      {
        PXLicenseObserver.UpdateInfo(cacheInfo, (PXLicenseObserver.CompanyRequestInfo) info);
        ilogger.Verbose<string, string>("Updated request for {Key} in {Collection}", sessionId, "trials");
      }
    }
    PXLicenseObserver.CompanyRequestInfo companyRequestInfo;
    if (lastRequest1.HasValue && cacheInfo.logOuts.TryGetValue(company ?? string.Empty, out companyRequestInfo))
    {
      System.DateTime lastRequest2 = companyRequestInfo.LastRequest;
      System.DateTime? nullable = lastRequest1;
      if ((nullable.HasValue ? (lastRequest2 > nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        if (flag3)
          ilogger.ForContext("LastRequest", (object) lastRequest1, false).ForContext("Info from logouts", (object) companyRequestInfo.LastRequest, false).Warning("Snapshot was restored, logging out");
        this.LogOut(PXLogOutReason.SnapshotRestored);
      }
    }
    cacheInfo.Flush();
  }

  internal void OnExpiration(string identifier)
  {
    this._logger.ForMethodContext(nameof (OnExpiration)).Debug<string>("Removing {Key} from CacheInfo", identifier);
    PXLicenseObserver.CacheRequestInfo cacheInfo = PXLicenseObserver.CacheInfo;
    if (cacheInfo != null)
      cacheInfo.RemoveInfo(identifier);
    else
      this._logger.ForMethodContext(nameof (OnExpiration)).Warning<string>("CacheInfo is null, cannot remove {Key}", identifier);
  }

  internal void RequestLogOut(string company)
  {
    PXLicenseObserver.CacheRequestInfo cacheInfo = PXLicenseObserver.CacheInfo;
    if (cacheInfo == null)
      return;
    PXLicenseObserver.CompanyRequestInfo companyRequestInfo1;
    if (!cacheInfo.logOuts.TryGetValue(company ?? string.Empty, out companyRequestInfo1))
    {
      ConcurrentDictionary<string, PXLicenseObserver.CompanyRequestInfo> logOuts = cacheInfo.logOuts;
      string key = company ?? string.Empty;
      PXLicenseObserver.CompanyRequestInfo companyRequestInfo2 = new PXLicenseObserver.CompanyRequestInfo();
      companyRequestInfo2.State = PXRequestInfoState.Inserted;
      companyRequestInfo1 = companyRequestInfo2;
      logOuts[key] = companyRequestInfo2;
    }
    else
      companyRequestInfo1.State = PXRequestInfoState.Updated;
    companyRequestInfo1.Expired = true;
    companyRequestInfo1.LastRequest = System.DateTime.UtcNow;
    cacheInfo.Flush(true);
  }

  private static void ExpireInfo(
    PXLicenseObserver.CacheRequestInfo cache,
    PXLicenseObserver.CompanyRequestInfo info,
    PXLogOutReason? expireReason = null)
  {
    cache.RequestFlush();
    info.Expired = true;
    info.ExpireReason = expireReason;
    if (info.State == PXRequestInfoState.Inserted)
      return;
    info.State = PXRequestInfoState.Updated;
  }

  private static void UnExpireInfo(
    PXLicenseObserver.CacheRequestInfo cache,
    PXLicenseObserver.CompanyRequestInfo info)
  {
    cache.RequestFlush();
    info.Expired = false;
    info.ExpireReason = new PXLogOutReason?();
    info.LastRequest = System.DateTime.UtcNow;
    if (info.State == PXRequestInfoState.Inserted)
      return;
    info.State = PXRequestInfoState.Updated;
  }

  private static void UpdateInfo(
    PXLicenseObserver.CacheRequestInfo cache,
    PXLicenseObserver.CompanyRequestInfo info)
  {
    info.LastRequest = System.DateTime.UtcNow;
    if (info.State != PXRequestInfoState.Unchanged)
      return;
    info.State = PXRequestInfoState.Defered;
  }

  private static void InsertInfo(
    PXLicenseObserver.CacheRequestInfo cache,
    PXLicenseObserver.CompanyRequestInfo info)
  {
    cache.RequestFlush();
    info.Expired = false;
    info.LastRequest = System.DateTime.UtcNow;
    info.State = PXRequestInfoState.Inserted;
  }

  private static System.DateTime? UpdateUserInfo(
    PXLicenseObserver.CacheRequestInfo cache,
    PXLicenseObserver.UserRequestInfo info,
    string identityName,
    System.DateTime? lastRequest)
  {
    if (info.Username != identityName)
    {
      info.Username = identityName;
      cache.RequestFlush();
      if (info.State != PXRequestInfoState.Inserted)
        info.State = PXRequestInfoState.Updated;
    }
    if (!lastRequest.HasValue || lastRequest.Value < info.LastRequest)
      lastRequest = new System.DateTime?(info.LastRequest);
    return lastRequest;
  }

  internal IEnumerable<RowActiveUserInfo> GetCurrentUsers()
  {
    PXLicenseObserver.CacheRequestInfo cacheInfo = PXLicenseObserver.CacheInfo;
    if (cacheInfo == null)
      return (IEnumerable<RowActiveUserInfo>) new RowActiveUserInfo[0];
    bool flag = this._licensingManager.License.IsUnlimitedUser();
    if (this.CheckTrialCompaniesOnly())
      return cacheInfo.trials.Where<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>>((Func<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, bool>) (it => !it.Value.Expired)).Select<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, RowActiveUserInfo>((Func<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, RowActiveUserInfo>) (u => new RowActiveUserInfo()
      {
        LastActivity = new int?(Convert.ToInt32((System.DateTime.UtcNow - u.Value.LastRequest).TotalMinutes)),
        LoginType = new int?(1),
        User = this._legacyCompanyService.ExtractUsername(u.Value.Username),
        Company = this._legacyCompanyService.ExtractCompany(u.Value.Username) ?? ((IEnumerable<string>) PXDatabase.AvailableCompanies).FirstOrDefault<string>() ?? PXAccess.GetCompanyName(),
        SessionId = u.Key
      }));
    if (flag)
      return (IEnumerable<RowActiveUserInfo>) new RowActiveUserInfo[0];
    return this._options.ConcurrentUserMode ? cacheInfo.sessions.Where<KeyValuePair<string, PXLicenseObserver.SessionRequestInfo>>((Func<KeyValuePair<string, PXLicenseObserver.SessionRequestInfo>, bool>) (it => !it.Value.Expired)).SelectMany<KeyValuePair<string, PXLicenseObserver.SessionRequestInfo>, RowActiveUserInfo>((Func<KeyValuePair<string, PXLicenseObserver.SessionRequestInfo>, IEnumerable<RowActiveUserInfo>>) (it => it.Value.users.Where<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>>((Func<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, bool>) (u => !u.Value.Expired)).Select<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, RowActiveUserInfo>((Func<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, RowActiveUserInfo>) (u => new RowActiveUserInfo()
    {
      LastActivity = new int?(Convert.ToInt32((System.DateTime.UtcNow - u.Value.LastRequest).TotalMinutes)),
      LoginType = new int?(1),
      User = this._legacyCompanyService.ExtractUsername(u.Value.Username),
      Company = this._legacyCompanyService.ExtractCompany(u.Value.Username) ?? ((IEnumerable<string>) PXDatabase.AvailableCompanies).FirstOrDefault<string>() ?? PXAccess.GetCompanyName(),
      SessionId = u.Key
    })))) : cacheInfo.users.Where<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>>((Func<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, bool>) (it => !it.Value.Expired)).Select<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, RowActiveUserInfo>((Func<KeyValuePair<string, PXLicenseObserver.UserRequestInfo>, RowActiveUserInfo>) (u => new RowActiveUserInfo()
    {
      LastActivity = new int?(Convert.ToInt32((System.DateTime.UtcNow - u.Value.LastRequest).TotalMinutes)),
      LoginType = new int?(1),
      User = this._legacyCompanyService.ExtractUsername(u.Value.Username),
      Company = this._legacyCompanyService.ExtractCompany(u.Value.Username) ?? ((IEnumerable<string>) PXDatabase.AvailableCompanies).FirstOrDefault<string>() ?? PXAccess.GetCompanyName(),
      SessionId = u.Key
    }));
  }

  private void LogOut(PXLogOutReason reason)
  {
    HttpContext current = HttpContext.Current;
    PXForceLogOutException exception = new PXForceLogOutException(reason);
    switch (reason)
    {
      case PXLogOutReason.UsersExceeded:
      case PXLogOutReason.CompaniesExceeded:
        PXAuditJournal.Register(PXAuditJournal.Operation.LicenseExceeded, (string) null, (string) null, exception.Title);
        break;
      case PXLogOutReason.SnapshotRestored:
        PXAuditJournal.Register(PXAuditJournal.Operation.SnapshotRestored, (string) null, (string) null, exception.Title);
        break;
    }
    string.IsNullOrEmpty(current.Request.Form["__CALLBACKID"]);
    bool flag1 = string.Equals(current.Request.HttpMethod, "GET", StringComparison.InvariantCultureIgnoreCase);
    bool flag2 = string.Equals(current.Request.HttpMethod, "POST", StringComparison.InvariantCultureIgnoreCase);
    this._legacySignOutManager.SignOut();
    PXSessionContext pxIdentity = PXContext.PXIdentity;
    if (reason == PXLogOutReason.UsersExceeded)
      PXTrace.Logger.ForSystemEvents("License", "License_LoginLimitExceededEventId").ForContext("ContextScreenId", (object) "SM604000", false).Warning<string, string>("Sign-In limit exceeded User:{ContextUserIdentity}, TenantName:{CurrentCompany}", pxIdentity.IdentityName, PXAccess.GetCompanyName());
    if (pxIdentity != null)
    {
      try
      {
        this._userManagementService.MarkOffline(pxIdentity.IdentityName);
      }
      catch
      {
      }
    }
    if (flag1 | flag2 && !NoRedirectOnLogout.Enabled(current))
    {
      if (PXLicenseObserver.IsModernUIEndpoint(current))
        throw new PXLogoutRedirectException(exception, current.Request.ApplicationPath, (string) null, (string) null);
      Redirector.RedirectPage(current, exception.Url);
    }
    throw exception;
  }

  private static bool IsModernUIEndpoint(HttpContext context)
  {
    return context.GetCoreHttpContext().Features.Get<IScreenIdProvider>() != null;
  }

  private bool CheckTrialCompaniesOnly()
  {
    try
    {
      PXLicense license = this._licensingManager.License;
      if (license == null)
        return true;
      PXSessionContext pxIdentity = PXContext.PXIdentity;
      if (pxIdentity == null || !pxIdentity.Authenticated)
        return false;
      string company = this._legacyCompanyService.ExtractCompany(pxIdentity.IdentityName);
      return !string.IsNullOrEmpty(company) && license.Trials.Contains(company);
    }
    catch
    {
      return false;
    }
  }

  private class CompanyRequestInfo
  {
    public string InstallationID;
    public PXRequestInfoState State = PXRequestInfoState.Inserted;
    public System.DateTime LastRequest;

    public bool Expired { get; set; }

    public PXLogOutReason? ExpireReason { get; set; }
  }

  private class UserRequestInfo : PXLicenseObserver.CompanyRequestInfo
  {
    public string Username;
  }

  private class SessionRequestInfo : PXLicenseObserver.UserRequestInfo
  {
    public ConcurrentDictionary<string, PXLicenseObserver.UserRequestInfo> users = new ConcurrentDictionary<string, PXLicenseObserver.UserRequestInfo>();
  }

  private class CacheRequestInfo
  {
    protected Random randomize = new Random();
    protected bool flushRequested;
    protected object PrefetchLock = new object();
    public readonly ConcurrentDictionary<string, PXLicenseObserver.UserRequestInfo> users = new ConcurrentDictionary<string, PXLicenseObserver.UserRequestInfo>();
    public readonly ConcurrentDictionary<string, PXLicenseObserver.UserRequestInfo> trials = new ConcurrentDictionary<string, PXLicenseObserver.UserRequestInfo>();
    public readonly ConcurrentDictionary<string, PXLicenseObserver.CompanyRequestInfo> companies = new ConcurrentDictionary<string, PXLicenseObserver.CompanyRequestInfo>();
    public readonly ConcurrentDictionary<string, PXLicenseObserver.SessionRequestInfo> sessions = new ConcurrentDictionary<string, PXLicenseObserver.SessionRequestInfo>();
    public readonly ConcurrentDictionary<string, PXLicenseObserver.CompanyRequestInfo> logOuts = new ConcurrentDictionary<string, PXLicenseObserver.CompanyRequestInfo>();

    public virtual void Prefetch()
    {
    }

    public virtual void Flush(bool force = false)
    {
    }

    public virtual void RequestFlush()
    {
      lock (this.PrefetchLock)
        this.flushRequested = true;
    }

    public virtual void RemoveInfo(string key)
    {
      this.users.TryRemove(key, out PXLicenseObserver.UserRequestInfo _);
    }
  }

  private class DatabaseCacheRequestInfo : 
    PXLicenseObserver.CacheRequestInfo,
    IPrefetchable,
    IPXCompanyDependent
  {
    protected string InstallationID;
    protected System.DateTime PrefetchStamp = System.DateTime.UtcNow;
    protected System.DateTime PrefetchNext = System.DateTime.UtcNow;

    public DatabaseCacheRequestInfo() => this.InstallationID = "*";

    public override void Prefetch()
    {
      this.users.Clear();
      this.trials.Clear();
      this.companies.Clear();
      this.sessions.Clear();
      this.logOuts.Clear();
      this.PrefetchStamp = System.DateTime.UtcNow;
      this.PrefetchNext = this.PrefetchStamp.AddSeconds((double) this.randomize.Next(45, 75));
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<Warden>((PXDataField) new PXDataField<Warden.installationID>(), (PXDataField) new PXDataField<Warden.type>(), (PXDataField) new PXDataField<Warden.key>(), (PXDataField) new PXDataField<Warden.sub>(), (PXDataField) new PXDataField<Warden.expired>(), (PXDataField) new PXDataField<Warden.lastRequest>(), (PXDataField) new PXDataField<Warden.details>(), (PXDataField) new PXDataFieldOrder<Warden.type>(), (PXDataField) new PXDataFieldOrder<Warden.key>(), (PXDataField) new PXDataFieldOrder<Warden.sub>()))
      {
        string str1 = pxDataRecord.GetString(0);
        PXRequestInfoType pxRequestInfoType = (PXRequestInfoType) pxDataRecord.GetInt16(1).Value;
        string key1 = pxDataRecord.GetString(2);
        string key2 = pxDataRecord.GetString(3);
        bool valueOrDefault = pxDataRecord.GetBoolean(4).GetValueOrDefault();
        System.DateTime dateTime = pxDataRecord.GetDateTime(5) ?? System.DateTime.MinValue;
        string str2 = pxDataRecord.GetString(6);
        PXLicenseObserver.CompanyRequestInfo companyRequestInfo1 = (PXLicenseObserver.CompanyRequestInfo) null;
        switch (pxRequestInfoType)
        {
          case PXRequestInfoType.Company:
            PXLicenseObserver.CompanyRequestInfo companyRequestInfo2;
            if (!this.companies.TryGetValue(key1, out companyRequestInfo2) || companyRequestInfo2.LastRequest < dateTime)
            {
              ConcurrentDictionary<string, PXLicenseObserver.CompanyRequestInfo> companies = this.companies;
              string key3 = key1;
              PXLicenseObserver.CompanyRequestInfo companyRequestInfo3 = new PXLicenseObserver.CompanyRequestInfo();
              companyRequestInfo3.Expired = valueOrDefault;
              companyRequestInfo3.LastRequest = dateTime;
              companyRequestInfo2 = companyRequestInfo3;
              companies[key3] = companyRequestInfo3;
            }
            companyRequestInfo1 = companyRequestInfo2;
            break;
          case PXRequestInfoType.Trial:
            PXLicenseObserver.UserRequestInfo userRequestInfo1;
            if (!this.trials.TryGetValue(key1, out userRequestInfo1) || userRequestInfo1.LastRequest < dateTime)
            {
              ConcurrentDictionary<string, PXLicenseObserver.UserRequestInfo> trials = this.trials;
              string key4 = key1;
              PXLicenseObserver.UserRequestInfo userRequestInfo2 = new PXLicenseObserver.UserRequestInfo();
              userRequestInfo2.Username = str2;
              userRequestInfo2.Expired = valueOrDefault;
              userRequestInfo2.LastRequest = dateTime;
              userRequestInfo1 = userRequestInfo2;
              trials[key4] = userRequestInfo2;
            }
            companyRequestInfo1 = (PXLicenseObserver.CompanyRequestInfo) userRequestInfo1;
            break;
          case PXRequestInfoType.User:
            PXLicenseObserver.UserRequestInfo userRequestInfo3;
            if (!this.users.TryGetValue(key1, out userRequestInfo3) || userRequestInfo3.LastRequest < dateTime)
            {
              ConcurrentDictionary<string, PXLicenseObserver.UserRequestInfo> users = this.users;
              string key5 = key1;
              PXLicenseObserver.UserRequestInfo userRequestInfo4 = new PXLicenseObserver.UserRequestInfo();
              userRequestInfo4.Username = str2;
              userRequestInfo4.Expired = valueOrDefault;
              userRequestInfo4.LastRequest = dateTime;
              userRequestInfo3 = userRequestInfo4;
              users[key5] = userRequestInfo4;
            }
            companyRequestInfo1 = (PXLicenseObserver.CompanyRequestInfo) userRequestInfo3;
            break;
          case PXRequestInfoType.Session:
            PXLicenseObserver.SessionRequestInfo sessionRequestInfo;
            if (!this.sessions.TryGetValue(key1, out sessionRequestInfo))
              this.sessions[key1] = sessionRequestInfo = new PXLicenseObserver.SessionRequestInfo();
            if (string.IsNullOrWhiteSpace(key2))
            {
              if (sessionRequestInfo.LastRequest < dateTime)
              {
                sessionRequestInfo.Expired = valueOrDefault;
                sessionRequestInfo.LastRequest = dateTime;
              }
              companyRequestInfo1 = (PXLicenseObserver.CompanyRequestInfo) sessionRequestInfo;
              break;
            }
            PXLicenseObserver.UserRequestInfo userRequestInfo5;
            if (!sessionRequestInfo.users.TryGetValue(key2, out userRequestInfo5) || userRequestInfo5.LastRequest < dateTime)
            {
              ConcurrentDictionary<string, PXLicenseObserver.UserRequestInfo> users = sessionRequestInfo.users;
              string key6 = key2;
              PXLicenseObserver.UserRequestInfo userRequestInfo6 = new PXLicenseObserver.UserRequestInfo();
              userRequestInfo6.Username = str2;
              userRequestInfo6.Expired = valueOrDefault;
              userRequestInfo6.LastRequest = dateTime;
              userRequestInfo5 = userRequestInfo6;
              users[key6] = userRequestInfo6;
            }
            companyRequestInfo1 = (PXLicenseObserver.CompanyRequestInfo) userRequestInfo5;
            break;
          case PXRequestInfoType.LogOut:
            PXLicenseObserver.CompanyRequestInfo companyRequestInfo4;
            if (!this.logOuts.TryGetValue(key1, out companyRequestInfo4) || companyRequestInfo4.LastRequest < dateTime)
            {
              ConcurrentDictionary<string, PXLicenseObserver.CompanyRequestInfo> logOuts = this.logOuts;
              string key7 = key1;
              PXLicenseObserver.CompanyRequestInfo companyRequestInfo5 = new PXLicenseObserver.CompanyRequestInfo();
              companyRequestInfo5.Expired = valueOrDefault;
              companyRequestInfo5.LastRequest = dateTime;
              companyRequestInfo4 = companyRequestInfo5;
              logOuts[key7] = companyRequestInfo5;
            }
            companyRequestInfo1 = companyRequestInfo4;
            break;
        }
        if (companyRequestInfo1 != null)
        {
          companyRequestInfo1.InstallationID = str1;
          companyRequestInfo1.State = PXRequestInfoState.Unchanged;
        }
      }
    }

    public override void Flush(bool force = false)
    {
      bool flag1 = force;
      System.DateTime? nullable1 = new System.DateTime?();
      System.DateTime? nullable2 = new System.DateTime?();
      bool flag2 = false;
      lock (this.PrefetchLock)
      {
        flag2 = this.flushRequested;
        flag1 |= flag2;
        if (!flag1)
        {
          flag1 = this.PrefetchNext < System.DateTime.UtcNow;
          if (flag1)
          {
            nullable1 = new System.DateTime?(this.PrefetchStamp);
            this.PrefetchStamp = System.DateTime.UtcNow;
            nullable2 = new System.DateTime?(this.PrefetchNext);
            this.PrefetchNext = this.PrefetchStamp.AddSeconds((double) this.randomize.Next(45, 75));
          }
        }
        else
          this.flushRequested = false;
      }
      if (!flag1)
        return;
      try
      {
        foreach (KeyValuePair<string, PXLicenseObserver.UserRequestInfo> user in this.users)
          this.Upsert((PXLicenseObserver.CompanyRequestInfo) user.Value, PXRequestInfoType.User, user.Key, string.Empty, user.Value.Username);
        foreach (KeyValuePair<string, PXLicenseObserver.UserRequestInfo> trial in this.trials)
          this.Upsert((PXLicenseObserver.CompanyRequestInfo) trial.Value, PXRequestInfoType.Trial, trial.Key, string.Empty, trial.Value.Username);
        foreach (KeyValuePair<string, PXLicenseObserver.CompanyRequestInfo> company in this.companies)
          this.Upsert(company.Value, PXRequestInfoType.Company, company.Key, string.Empty);
        foreach (KeyValuePair<string, PXLicenseObserver.SessionRequestInfo> session in this.sessions)
        {
          this.Upsert((PXLicenseObserver.CompanyRequestInfo) session.Value, PXRequestInfoType.Session, session.Key, string.Empty, session.Value.Username);
          foreach (KeyValuePair<string, PXLicenseObserver.UserRequestInfo> user in session.Value.users)
            this.Upsert((PXLicenseObserver.CompanyRequestInfo) user.Value, PXRequestInfoType.Session, session.Key, user.Key, user.Value.Username);
        }
        foreach (KeyValuePair<string, PXLicenseObserver.CompanyRequestInfo> logOut in this.logOuts)
          this.Upsert(logOut.Value, PXRequestInfoType.LogOut, logOut.Key, string.Empty);
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          PXDatabase.Delete<Warden>((PXDataFieldRestrict) new PXDataFieldRestrict<Warden.lastRequest>(PXDbType.DateTime, new int?(), (object) System.DateTime.UtcNow.AddDays(-1.0), PXComp.LE));
          transactionScope.Complete();
        }
      }
      catch (Exception ex)
      {
        lock (this.PrefetchLock)
        {
          if (!this.flushRequested & flag2)
            this.flushRequested = true;
          if (nullable1.HasValue)
          {
            if (nullable2.HasValue)
            {
              this.PrefetchStamp = nullable1.Value;
              this.PrefetchNext = nullable2.Value;
            }
          }
        }
        if (ex is PXDatabaseException && ((PXDatabaseException) ex).ErrorCode == PXDbExceptions.Deadlock)
          return;
        throw;
      }
    }

    public override void RemoveInfo(string key)
    {
      base.RemoveInfo(key);
      try
      {
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          PXDatabase.Delete<Warden>((PXDataFieldRestrict) new PXDataFieldRestrict<Warden.key>(PXDbType.NVarChar, new int?(128 /*0x80*/), (object) key, PXComp.EQ));
          transactionScope.Complete();
        }
      }
      catch (PXDatabaseException ex)
      {
        if (ex.ErrorCode == PXDbExceptions.Deadlock)
          return;
        throw;
      }
    }

    protected void Upsert(
      PXLicenseObserver.CompanyRequestInfo info,
      PXRequestInfoType type,
      string key,
      string sub,
      string details = null)
    {
      if (info.State == PXRequestInfoState.Inserted)
      {
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          this.Insert(type, key, sub, info.Expired, info.LastRequest, details);
          info.State = PXRequestInfoState.Unchanged;
          transactionScope.Complete();
        }
      }
      if (info.State != PXRequestInfoState.Updated && info.State != PXRequestInfoState.Defered)
        return;
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        this.Update(type, key, sub, info.Expired, info.LastRequest, details);
        info.State = PXRequestInfoState.Unchanged;
        transactionScope.Complete();
      }
    }

    protected void Insert(
      PXRequestInfoType type,
      string key,
      string sub,
      bool expired,
      System.DateTime lastRequest,
      string details = null)
    {
      ISqlDialect sqlDialect = PXDatabase.Provider.SqlDialect;
      List<PXDataFieldAssign> pxDataFieldAssignList = new List<PXDataFieldAssign>();
      pxDataFieldAssignList.Add((PXDataFieldAssign) new PXDataFieldAssign<Warden.installationID>(PXDbType.VarChar, (object) this.InstallationID));
      pxDataFieldAssignList.Add((PXDataFieldAssign) new PXDataFieldAssign<Warden.type>(PXDbType.SmallInt, (object) (int) type));
      pxDataFieldAssignList.Add((PXDataFieldAssign) new PXDataFieldAssign<Warden.key>(PXDbType.NVarChar, (object) key));
      pxDataFieldAssignList.Add((PXDataFieldAssign) new PXDataFieldAssign<Warden.sub>(PXDbType.NVarChar, (object) sub));
      pxDataFieldAssignList.Add((PXDataFieldAssign) new PXDataFieldAssign<Warden.expired>(PXDbType.Bit, (object) expired));
      pxDataFieldAssignList.Add((PXDataFieldAssign) new PXDataFieldAssign<Warden.lastRequest>(PXDbType.DateTime, (object) lastRequest));
      if (details != null)
        pxDataFieldAssignList.Add((PXDataFieldAssign) new PXDataFieldAssign<Warden.details>(PXDbType.NVarChar, (object) details));
      List<PXDataField> pxDataFieldList = new List<PXDataField>();
      pxDataFieldList.Add((PXDataField) new PXDataFieldValue<Warden.installationID>(PXDbType.VarChar, (object) this.InstallationID));
      pxDataFieldList.Add((PXDataField) new PXDataFieldValue<Warden.type>(PXDbType.SmallInt, (object) (int) type));
      pxDataFieldList.Add((PXDataField) new PXDataFieldValue<Warden.key>(PXDbType.NVarChar, (object) key));
      pxDataFieldList.Add((PXDataField) new PXDataFieldValue<Warden.sub>(PXDbType.NVarChar, (object) sub));
      try
      {
        if (PXDatabase.Ensure<Warden>(pxDataFieldAssignList.ToArray(), pxDataFieldList.ToArray()))
          return;
        this.Update(type, key, sub, expired, lastRequest, details);
      }
      catch (PXLockViolationException ex)
      {
        this.Update(type, key, sub, expired, lastRequest, details);
      }
    }

    protected void Update(
      PXRequestInfoType type,
      string key,
      string sub,
      bool expired,
      System.DateTime lastRequest,
      string details = null)
    {
      List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>();
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict<Warden.installationID>(PXDbType.VarChar, (object) this.InstallationID));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict<Warden.type>(PXDbType.SmallInt, (object) (int) type));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict<Warden.key>(PXDbType.NVarChar, (object) key));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict<Warden.sub>(PXDbType.NVarChar, (object) sub));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<Warden.expired>(PXDbType.Bit, (object) expired));
      pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<Warden.lastRequest>(PXDbType.DateTime, (object) lastRequest));
      if (details != null)
        pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<Warden.details>(PXDbType.NVarChar, (object) details));
      if (PXDatabase.Update<Warden>(pxDataFieldParamList.ToArray()))
        return;
      this.Insert(type, key, sub, expired, lastRequest, details);
    }
  }
}
