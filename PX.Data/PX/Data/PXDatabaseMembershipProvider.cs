// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDatabaseMembershipProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PX.Common;
using PX.Common.Context;
using PX.Data.DependencyInjection;
using PX.Data.PushNotifications;
using PX.Data.SQLTree;
using PX.Hosting.MachineKey;
using PX.Security;
using PX.SM;
using PX.SP;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Security;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public class PXDatabaseMembershipProvider : PXBaseMembershipProvider
{
  private const int newPasswordLength = 8;
  private const string eventSource = "PXDatabaseMembershipProvider";
  private const string exceptionMessage = "An exception occurred. Please check the event log.";
  private static readonly Regex[] PasswordComplexityRegex = new Regex[4]
  {
    new Regex("\\d", RegexOptions.Compiled),
    new Regex("\\p{Ll}", RegexOptions.Compiled),
    new Regex("\\p{Lu}", RegexOptions.Compiled),
    new Regex("\\W", RegexOptions.Compiled)
  };
  private MachineKeyOptions _machineKeyOptions;
  private string pApplicationName;
  private bool pEnablePasswordReset;
  private bool pEnablePasswordRetrieval;
  private bool pRequiresQuestionAndAnswer;
  private bool pRequiresUniqueEmail;
  private int pMinRequiredNonAlphanumericCharacters;
  private string pPasswordStrengthRegularExpression;
  private bool pConcurrentUserMode;
  private bool pEnableGuestAccess;
  private readonly object syncRoot = new object();
  private static readonly ConcurrentDictionary<PXBaseMembershipProvider.UserWithPasswordCacheKey, string> _hashedPasswords = new ConcurrentDictionary<PXBaseMembershipProvider.UserWithPasswordCacheKey, string>();

  public override bool EnablePasswordReset => this.pEnablePasswordReset;

  public override bool EnablePasswordRetrieval => this.pEnablePasswordRetrieval;

  public override bool RequiresQuestionAndAnswer => this.pRequiresQuestionAndAnswer;

  public override bool RequiresUniqueEmail => this.pRequiresUniqueEmail;

  public override int MaxInvalidPasswordAttempts => SitePolicy.AccountLockoutThreshold;

  public override int PasswordAttemptWindow => SitePolicy.AccountLockoutReset;

  public override MembershipPasswordFormat PasswordFormat => MembershipPasswordFormat.Hashed;

  public override int MinRequiredNonAlphanumericCharacters
  {
    get => this.pMinRequiredNonAlphanumericCharacters;
  }

  public override int MinRequiredPasswordLength => SitePolicy.PasswordMinLength;

  public override string PasswordStrengthRegularExpression
  {
    get => this.pPasswordStrengthRegularExpression;
  }

  public override string ApplicationName
  {
    get => this.pApplicationName;
    set => this.pApplicationName = value;
  }

  public override bool ConcurrentUserMode => this.pConcurrentUserMode;

  public bool EnableGuestAccess => this.pEnableGuestAccess;

  public override void Initialize(string name, NameValueCollection config)
  {
    if (config == null)
      throw new PXArgumentException(nameof (config), "The argument cannot be null.");
    if (name.Length == 0)
      name = nameof (PXDatabaseMembershipProvider);
    if (string.IsNullOrEmpty(config["description"]))
    {
      config.Remove("description");
      config.Add("description", "PX Database Membership provider");
    }
    base.Initialize(name, config);
    this.pApplicationName = PXDatabaseMembershipProvider.GetConfigValue(config["applicationName"], HostingEnvironment.ApplicationVirtualPath);
    this.pMinRequiredNonAlphanumericCharacters = Convert.ToInt32(PXDatabaseMembershipProvider.GetConfigValue(config["minRequiredNonAlphanumericCharacters"], "1"));
    this.pPasswordStrengthRegularExpression = Convert.ToString(PXDatabaseMembershipProvider.GetConfigValue(config["passwordStrengthRegularExpression"], ""));
    this.pEnableGuestAccess = Convert.ToBoolean(PXDatabaseMembershipProvider.GetConfigValue(config["enableGuestAccess"], "false"));
    this.pEnablePasswordReset = Convert.ToBoolean(PXDatabaseMembershipProvider.GetConfigValue(config["enablePasswordReset"], "true"));
    this.pEnablePasswordRetrieval = Convert.ToBoolean(PXDatabaseMembershipProvider.GetConfigValue(config["enablePasswordRetrieval"], "true"));
    this.pRequiresQuestionAndAnswer = Convert.ToBoolean(PXDatabaseMembershipProvider.GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
    this.pRequiresUniqueEmail = Convert.ToBoolean(PXDatabaseMembershipProvider.GetConfigValue(config["requiresUniqueEmail"], "true"));
    this.pConcurrentUserMode = Convert.ToBoolean(PXDatabaseMembershipProvider.GetConfigValue(config["concurrentUserMode"], "false"));
    this._machineKeyOptions = ProviderBaseDependencyHelper.Resolve<IOptions<MachineKeyOptions>>().Value;
  }

  private static string GetConfigValue(string configValue, string defaultValue)
  {
    return !string.IsNullOrEmpty(configValue) ? configValue : defaultValue;
  }

  internal static bool UpdateUser(
    string username,
    bool skipWatchdog,
    string application,
    params PXDataFieldAssign[] changes)
  {
    List<PXDataFieldAssign> pxDataFieldAssignList = new List<PXDataFieldAssign>();
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Users>(new PXDataField("PKID"), new PXDataField("FirstName"), new PXDataField("LastName"), new PXDataField("Email"), new PXDataField("Comment"), new PXDataField("Password"), new PXDataField("PasswordChangeable"), new PXDataField("PasswordChangeOnNextLogin"), new PXDataField("PasswordNeverExpires"), new PXDataField("AllowPasswordRecovery"), new PXDataField("PasswordQuestion"), new PXDataField("PasswordAnswer"), new PXDataField("IsApproved"), new PXDataField("Guest"), new PXDataField("LastActivityDate"), new PXDataField("LastLoginDate"), new PXDataField("LastPasswordChangedDate"), new PXDataField("CreationDate"), new PXDataField("IsOnLine"), new PXDataField("LastHostName"), new PXDataField("LockedOutDate"), new PXDataField("LastLockedOutDate"), new PXDataField("FailedPasswordAttemptCount"), new PXDataField("FailedPasswordAttemptWindowStart"), new PXDataField("FailedPasswordAnswerAttemptCount"), new PXDataField("FailedPasswordAnswerAttemptWindowStart"), new PXDataField("GroupMask"), new PXDataField("GuidForPasswordRecovery"), new PXDataField("PasswordRecoveryExpirationDate"), new PXDataField("Source"), new PXDataField("ExtRef"), new PXDataField("FullName"), new PXDataField("IsAssigned"), new PXDataField("OverrideADRoles"), new PXDataField("IsHidden"), new PXDataField("NoteID"), new PXDataField("Phone"), new PXDataField("LoginTypeID"), new PXDataField("IsPendingActivation"), new PXDataField("DeletedDatabaseRecord"), new PXDataField("PseudonymizationStatus"), new PXDataField("MultiFactorType"), new PXDataField("MultiFactorOverride"), new PXDataField("AllowedSessions"), new PXDataField("ForbidLoginWithPassword"), new PXDataField("OverrideLocalRolesWithOidcProviderRoles"), (PXDataField) new PXDataFieldValue("Username", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) username), (PXDataField) new PXDataFieldValue("ApplicationName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) application)))
    {
      if (pxDataRecord != null)
      {
        pxDataFieldAssignList.Add(new PXDataFieldAssign("Username", PXDbType.NVarChar, new int?(64 /*0x40*/), (object) username));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("ApplicationName", PXDbType.VarChar, new int?(32 /*0x20*/), (object) application));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("PKID", PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) pxDataRecord.GetGuid(0)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("FirstName", PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) pxDataRecord.GetString(1)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("LastName", PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) pxDataRecord.GetString(2)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("Email", PXDbType.VarChar, new int?(128 /*0x80*/), (object) pxDataRecord.GetString(3)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("Comment", PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) pxDataRecord.GetString(4)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("Password", PXDbType.NVarChar, new int?(512 /*0x0200*/), (object) pxDataRecord.GetString(5), PXDBUserPasswordAttribute.DefaultVeil));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("PasswordChangeable", PXDbType.Bit, new int?(1), (object) pxDataRecord.GetBoolean(6)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("PasswordChangeOnNextLogin", PXDbType.Bit, new int?(1), (object) pxDataRecord.GetBoolean(7)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("PasswordNeverExpires", PXDbType.Bit, new int?(1), (object) pxDataRecord.GetBoolean(8)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("AllowPasswordRecovery", PXDbType.Bit, new int?(1), (object) pxDataRecord.GetBoolean(9)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("PasswordQuestion", PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) pxDataRecord.GetString(10)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("PasswordAnswer", PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) pxDataRecord.GetString(11), PXDBUserPasswordAttribute.DefaultVeil));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("IsApproved", PXDbType.Bit, new int?(1), (object) pxDataRecord.GetBoolean(12)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("Guest", PXDbType.Bit, new int?(1), (object) pxDataRecord.GetBoolean(13)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("LastActivityDate", PXDbType.DateTime, new int?(8), (object) pxDataRecord.GetDateTime(14)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("LastLoginDate", PXDbType.DateTime, new int?(8), (object) pxDataRecord.GetDateTime(15)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("LastPasswordChangedDate", PXDbType.DateTime, new int?(8), (object) pxDataRecord.GetDateTime(16 /*0x10*/)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("CreationDate", PXDbType.DateTime, new int?(8), (object) pxDataRecord.GetDateTime(17)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("IsOnLine", PXDbType.Bit, new int?(1), (object) pxDataRecord.GetBoolean(18)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("LastHostName", PXDbType.VarChar, new int?(50), (object) pxDataRecord.GetString(19)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("LockedOutDate", PXDbType.DateTime, new int?(8), (object) pxDataRecord.GetDateTime(20)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("LastLockedOutDate", PXDbType.DateTime, new int?(8), (object) pxDataRecord.GetDateTime(21)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("FailedPasswordAttemptCount", PXDbType.Int, new int?(4), (object) pxDataRecord.GetInt32(22)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("FailedPasswordAttemptWindowStart", PXDbType.DateTime, new int?(8), (object) pxDataRecord.GetDateTime(23)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("FailedPasswordAnswerAttemptCount", PXDbType.Int, new int?(4), (object) pxDataRecord.GetInt32(24)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("FailedPasswordAnswerAttemptWindowStart", PXDbType.DateTime, new int?(8), (object) pxDataRecord.GetDateTime(25)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("GroupMask", PXDbType.VarBinary, new int?(32 /*0x20*/), (object) pxDataRecord.GetBytes(26)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("GuidForPasswordRecovery", PXDbType.VarChar, new int?(60), (object) pxDataRecord.GetString(27)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("PasswordRecoveryExpirationDate", PXDbType.DateTime, (object) pxDataRecord.GetDateTime(28)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("Source", PXDbType.Int, (object) pxDataRecord.GetInt32(29)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("ExtRef", PXDbType.NVarChar, new int?(512 /*0x0200*/), (object) pxDataRecord.GetString(30)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("FullName", PXDbType.NVarChar, new int?(512 /*0x0200*/), (object) pxDataRecord.GetString(31 /*0x1F*/)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("IsAssigned", PXDbType.Bit, new int?(1), (object) pxDataRecord.GetBoolean(32 /*0x20*/)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("OverrideADRoles", PXDbType.Bit, new int?(1), (object) pxDataRecord.GetBoolean(33)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("IsHidden", PXDbType.Bit, new int?(1), (object) pxDataRecord.GetBoolean(34)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("NoteID", PXDbType.UniqueIdentifier, new int?(1), (object) pxDataRecord.GetGuid(35)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("Phone", PXDbType.NVarChar, new int?(50), (object) pxDataRecord.GetString(36)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("LoginTypeID", PXDbType.Int, (object) pxDataRecord.GetInt32(37)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("IsPendingActivation", PXDbType.Bit, new int?(1), (object) pxDataRecord.GetBoolean(38)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("DeletedDatabaseRecord", PXDbType.Bit, new int?(1), (object) pxDataRecord.GetBoolean(39)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("PseudonymizationStatus", PXDbType.Int, (object) pxDataRecord.GetInt32(40)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("MultiFactorType", PXDbType.Int, (object) pxDataRecord.GetInt32(41)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("MultiFactorOverride", PXDbType.Bit, new int?(1), (object) pxDataRecord.GetBoolean(42)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("AllowedSessions", PXDbType.Int, (object) pxDataRecord.GetInt32(43)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("ForbidLoginWithPassword", PXDbType.Bit, new int?(1), (object) pxDataRecord.GetBoolean(44)));
        pxDataFieldAssignList.Add(new PXDataFieldAssign("OverrideLocalRolesWithOidcProviderRoles", PXDbType.Bit, new int?(1), (object) pxDataRecord.GetBoolean(45)));
      }
    }
    List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>((IEnumerable<PXDataFieldParam>) changes);
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict("Username", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) username));
    pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict("ApplicationName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) application));
    pxDataFieldParamList.Add((PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed);
    bool flag = false;
    using (PXTransactionScope transactionScope = new PXTransactionScope(skipWatchdog))
    {
      using (new SuppressPushNotificationsScope())
      {
        try
        {
          using (changes.Length <= 1 ? new PXIgnoreChangeScope() : (PXIgnoreChangeScope) null)
          {
            flag = PXDatabase.Update<Users>(pxDataFieldParamList.ToArray());
            transactionScope.Complete();
          }
        }
        catch (PXDbOperationSwitchRequiredException ex)
        {
          if (pxDataFieldAssignList.Count == 0)
            throw;
          for (int index1 = 0; index1 < pxDataFieldAssignList.Count; ++index1)
          {
            for (int index2 = 0; index2 < changes.Length; ++index2)
            {
              if (pxDataFieldAssignList[index1].Column.Equals((SQLExpression) changes[index2].Column))
              {
                pxDataFieldAssignList[index1].Value = changes[index2].Value;
                break;
              }
            }
          }
          flag = PXDatabase.Insert<Users>(pxDataFieldAssignList.ToArray());
          transactionScope.Complete();
        }
      }
      if (skipWatchdog)
      {
        PXBaseMembershipProvider.UsersCache slot = PXDatabase.GetSlot<PXBaseMembershipProvider.UsersCache>("MembershipUser", typeof (Users));
        if (slot != null)
        {
          string companyName = PXAccess.GetCompanyName();
          string key = string.IsNullOrEmpty(companyName) ? username : $"{username}@{companyName}";
          lock (((ICollection) slot).SyncRoot)
            slot.Remove(key);
        }
      }
    }
    PXDatabase.SelectTimeStamp();
    return flag;
  }

  public override bool ChangePassword(string username, string oldPwd, string newPwd)
  {
    bool flag1 = oldPwd == $"{newPwd}#{System.DateTime.Today.DayOfWeek.ToString()}:{System.DateTime.Today.DayOfYear.ToString()}";
    bool flag2 = oldPwd == $"{newPwd}#{System.DateTime.Today.DayOfWeek.ToString()}";
    if (!flag1 && !flag2)
    {
      HttpContext current = HttpContext.Current;
      if (!PXDatabaseMembershipProvider.ValidateUserIP(current != null ? current.GetRequestBase() : (HttpRequestBase) null, username) || !PXDatabaseMembershipProvider.ValidateExternalAccessRights(this.GetUser(username, true)) || !this.ValidateUserPassword(username, oldPwd, false, false) && !this.ValidateUserHash(username, oldPwd))
        return false;
    }
    ValidatePasswordEventArgs e = new ValidatePasswordEventArgs(username, newPwd, true);
    if (!flag1)
      this.OnValidatingPassword(e);
    if (e.Cancel)
    {
      if (e.FailureInformation != null)
        throw e.FailureInformation;
      throw new PXMembershipPasswordException("The password change has been canceled because of a new password validation failure.");
    }
    return PXDatabaseMembershipProvider.UpdateUser(username, false, this.pApplicationName, new PXDataFieldAssign("Password", PXDbType.NVarChar, new int?(512 /*0x0200*/), (object) PXDatabaseMembershipProvider.HashPassword(username, newPwd), PXDBUserPasswordAttribute.DefaultVeil), new PXDataFieldAssign("LastPasswordChangedDate", PXDbType.DateTime, new int?(8), (object) System.DateTime.UtcNow), new PXDataFieldAssign("GuidForPasswordRecovery", PXDbType.VarChar, (object) ""), new PXDataFieldAssign("PasswordRecoveryExpirationDate", PXDbType.DateTime, (object) null), new PXDataFieldAssign("LockedOutDate", PXDbType.DateTime, (object) null));
  }

  public override bool ChangePasswordQuestionAndAnswer(
    string username,
    string password,
    string newPwdQuestion,
    string newPwdAnswer)
  {
    if (!this.ValidateUserPassword(username, password, true, false))
      return false;
    return PXDatabaseMembershipProvider.UpdateUser(username, false, this.pApplicationName, new PXDataFieldAssign("PasswordQuestion", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) newPwdQuestion), new PXDataFieldAssign("PasswordAnswer", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) newPwdAnswer, PXDBUserPasswordAttribute.DefaultVeil));
  }

  public override MembershipUser CreateUser(
    string username,
    string password,
    string email,
    string passwordQuestion,
    string passwordAnswer,
    bool isApproved,
    object providerUserKey,
    out MembershipCreateStatus status)
  {
    ValidatePasswordEventArgs e = new ValidatePasswordEventArgs(username, password, true);
    this.OnValidatingPassword(e);
    if (e.Cancel)
    {
      status = MembershipCreateStatus.InvalidPassword;
      return (MembershipUser) null;
    }
    if (this.RequiresUniqueEmail && this.GetUserNameByEmail(email) != "")
    {
      status = MembershipCreateStatus.DuplicateEmail;
      return (MembershipUser) null;
    }
    if (this.GetUser(username, false) == null)
    {
      System.DateTime universalTime = System.DateTime.Now.ToUniversalTime();
      if (providerUserKey == null)
        providerUserKey = (object) Guid.NewGuid();
      else if (!(providerUserKey is Guid))
      {
        status = MembershipCreateStatus.InvalidProviderUserKey;
        return (MembershipUser) null;
      }
      try
      {
        status = (MembershipCreateStatus) (PXDatabase.Insert<Users>(new PXDataFieldAssign("PKID", PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), providerUserKey), new PXDataFieldAssign("Username", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) username), new PXDataFieldAssign("Password", PXDbType.NVarChar, new int?(512 /*0x0200*/), (object) PXDatabaseMembershipProvider.HashPassword(username, password)), new PXDataFieldAssign("Email", PXDbType.VarChar, new int?(128 /*0x80*/), (object) email), new PXDataFieldAssign("PasswordQuestion", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) passwordQuestion), new PXDataFieldAssign("PasswordAnswer", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) PXDatabaseMembershipProvider.HashPassword(username, passwordAnswer)), new PXDataFieldAssign("IsApproved", PXDbType.Bit, new int?(1), (object) isApproved), new PXDataFieldAssign("Comment", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) ""), new PXDataFieldAssign("CreationDate", PXDbType.DateTime, new int?(8), (object) universalTime), new PXDataFieldAssign("LastPasswordChangedDate", PXDbType.DateTime, new int?(8), (object) universalTime), new PXDataFieldAssign("LastActivityDate", PXDbType.DateTime, new int?(8), (object) universalTime), new PXDataFieldAssign("ApplicationName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) this.pApplicationName), new PXDataFieldAssign("LockedOutDate", PXDbType.DateTime, new int?(8), (object) null), new PXDataFieldAssign("LastLockedOutDate", PXDbType.DateTime, new int?(8), (object) null), new PXDataFieldAssign("FailedPasswordAttemptCount", PXDbType.Int, new int?(4), (object) 0), new PXDataFieldAssign("FailedPasswordAttemptWindowStart", PXDbType.DateTime, new int?(8), (object) universalTime), new PXDataFieldAssign("FailedPasswordAnswerAttemptCount", PXDbType.Int, new int?(4), (object) 0), new PXDataFieldAssign("FailedPasswordAnswerAttemptWindowStart", PXDbType.DateTime, new int?(8), (object) universalTime), new PXDataFieldAssign("Source", PXDbType.Int, (object) 0), new PXDataFieldAssign("FullName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) ""), new PXDataFieldAssign("PasswordChangeable", PXDbType.Bit, (object) 1), new PXDataFieldAssign("PasswordChangeOnNextLogin", PXDbType.Bit, (object) 0), new PXDataFieldAssign("PasswordNeverExpires", PXDbType.Bit, (object) 1), new PXDataFieldAssign("AllowPasswordRecovery", PXDbType.Bit, (object) 1), new PXDataFieldAssign("Guest", PXDbType.Bit, (object) 1), new PXDataFieldAssign("IsOnLine", PXDbType.Bit, (object) 0), (PXDataFieldAssign) new PXDataFieldAssign<Users.noteID>(PXDbType.UniqueIdentifier, (object) Guid.NewGuid())) ? 0 : 8);
      }
      catch (PXDatabaseException ex)
      {
        status = MembershipCreateStatus.ProviderError;
      }
      return this.GetUser(username, false);
    }
    status = MembershipCreateStatus.DuplicateUserName;
    return (MembershipUser) null;
  }

  public override bool DeleteUser(string username, bool deleteAllRelatedData)
  {
    return PXDatabase.Delete<Users>(new PXDataFieldRestrict("Username", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) username), new PXDataFieldRestrict("ApplicationName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) this.pApplicationName));
  }

  public override MembershipUserCollection GetAllUsers(
    int pageIndex,
    int pageSize,
    out int totalRecords)
  {
    totalRecords = 0;
    MembershipUserCollection allUsers = new MembershipUserCollection();
    int num1 = pageSize * pageIndex;
    int num2 = num1 + pageSize - 1;
    System.DateTime lockoutTime = PXDatabaseMembershipProvider.LockoutTime;
    foreach (PXDataRecord reader in PXDatabase.SelectMulti<Users>(new PXDataField("PKID"), new PXDataField("Username"), new PXDataField("Email"), new PXDataField("PasswordQuestion"), new PXDataField("Comment"), new PXDataField("IsApproved"), new PXDataField("LockedOutDate"), new PXDataField("CreationDate"), new PXDataField("LastLoginDate"), new PXDataField("LastActivityDate"), new PXDataField("LastPasswordChangedDate"), new PXDataField("LastLockedOutDate"), new PXDataField("ExtRef"), new PXDataField("Source"), new PXDataField("FirstName"), new PXDataField("LastName"), new PXDataField("Guest"), new PXDataField("IsPendingActivation"), new PXDataField("FullName"), (PXDataField) new PXDataFieldValue("ApplicationName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) this.pApplicationName)))
    {
      if (totalRecords >= num1 && totalRecords <= num2)
      {
        MembershipUser userFromReader = (MembershipUser) this.GetUserFromReader(reader, lockoutTime);
        allUsers.Add(userFromReader);
      }
      ++totalRecords;
    }
    return allUsers;
  }

  public override int GetNumberOfUsersOnline()
  {
    System.DateTime dateTime1 = System.DateTime.UtcNow.Subtract(new TimeSpan(0, Membership.UserIsOnlineTimeWindow, 0));
    int numberOfUsersOnline = 0;
    foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<Users>(new PXDataField("LastActivityDate"), (PXDataField) new PXDataFieldValue("ApplicationName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) this.pApplicationName), (PXDataField) new PXDataFieldValue("LastActivityDate", PXDbType.DateTime, new int?(), (object) dateTime1, PXComp.GT)))
    {
      System.DateTime? dateTime2 = pxDataRecord.GetDateTime(0);
      System.DateTime dateTime3 = dateTime1;
      if ((dateTime2.HasValue ? (dateTime2.GetValueOrDefault() > dateTime3 ? 1 : 0) : 0) != 0)
        ++numberOfUsersOnline;
    }
    return numberOfUsersOnline;
  }

  public override string GetPassword(string username, string answer)
  {
    throw new NotSupportedException("Can't restore password for MembershipPasswordFormat.Hashed password format.");
  }

  public override MembershipUser GetUser(string username, bool userIsOnline)
  {
    return this.GetUser(username, (userIsOnline ? 1 : 0) != 0, new int[2]
    {
      0,
      2
    });
  }

  public MembershipUser GetUser(string username, bool userIsOnline, int[] sources)
  {
    username = this.LegacyCompanyService.ExtractUsername(username);
    MembershipUserExt user = (MembershipUserExt) null;
    PXBaseMembershipProvider.UsersCache slot = PXDatabase.GetSlot<PXBaseMembershipProvider.UsersCache>("MembershipUser", typeof (Users));
    if (slot != null)
    {
      lock (((ICollection) slot).SyncRoot)
      {
        string companyName = PXAccess.GetCompanyName();
        string key = string.IsNullOrEmpty(companyName) ? username : $"{username}@{companyName}";
        slot.TryGetValue(key, out user);
      }
      if (user != null && sources != null && !((IEnumerable<int>) sources).Contains<int>(user.Source))
        return (MembershipUser) null;
    }
    if (user == null)
    {
      using (new PXReadBranchRestrictedScope())
      {
        System.DateTime lockoutTime = PXDatabaseMembershipProvider.LockoutTime;
        using (PXDataRecord reader = PXDatabase.SelectSingle<Users>(new PXDataField("PKID"), new PXDataField("Username"), new PXDataField("Email"), new PXDataField("PasswordQuestion"), new PXDataField("Comment"), new PXDataField("IsApproved"), new PXDataField("LockedOutDate"), new PXDataField("CreationDate"), new PXDataField("LastLoginDate"), new PXDataField("LastActivityDate"), new PXDataField("LastPasswordChangedDate"), new PXDataField("LastLockedOutDate"), new PXDataField("ExtRef"), new PXDataField("Source"), new PXDataField("FirstName"), new PXDataField("LastName"), new PXDataField("Guest"), new PXDataField("IsPendingActivation"), new PXDataField("FullName"), (PXDataField) new PXDataFieldValue("Username", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) username), (PXDataField) new PXDataFieldValue("ApplicationName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) this.pApplicationName)))
          user = this.GetUserFromReader(reader, lockoutTime);
      }
      if (user != null && sources != null && !((IEnumerable<int>) sources).Contains<int>(user.Source))
        return (MembershipUser) null;
      if (userIsOnline && user != null && !WebConfig.IsClusterEnabled)
      {
        TimeSpan timeSpan = System.DateTime.Now.ToUniversalTime().Subtract(user.LastActivityDate);
        if (timeSpan.TotalMinutes >= 15.0)
        {
          timeSpan = System.DateTime.Now.ToUniversalTime().Subtract(user.LastActivityDate);
          if (timeSpan.TotalMinutes < 15.0)
            return (MembershipUser) user;
          PXDatabaseMembershipProvider.UpdateUser(username, false, this.pApplicationName, new PXDataFieldAssign("LastActivityDate", PXDbType.DateTime, new int?(8), (object) System.DateTime.Now.ToUniversalTime()));
        }
      }
    }
    return (MembershipUser) user;
  }

  public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
  {
    return this.GetUser(providerUserKey, (userIsOnline ? 1 : 0) != 0, new int[2]
    {
      0,
      2
    });
  }

  public MembershipUser GetUser(object providerUserKey, bool userIsOnline, int[] sources)
  {
    System.DateTime lockoutTime = PXDatabaseMembershipProvider.LockoutTime;
    MembershipUserExt userFromReader;
    using (PXDataRecord reader = PXDatabase.SelectSingle<Users>(new PXDataField("PKID"), new PXDataField("Username"), new PXDataField("Email"), new PXDataField("PasswordQuestion"), new PXDataField("Comment"), new PXDataField("IsApproved"), new PXDataField("LockedOutDate"), new PXDataField("CreationDate"), new PXDataField("LastLoginDate"), new PXDataField("LastActivityDate"), new PXDataField("LastPasswordChangedDate"), new PXDataField("LastLockedOutDate"), new PXDataField("ExtRef"), new PXDataField("Source"), new PXDataField("FirstName"), new PXDataField("LastName"), new PXDataField("Guest"), new PXDataField("IsPendingActivation"), new PXDataField("FullName"), (PXDataField) new PXDataFieldValue("PKID", PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), providerUserKey), (PXDataField) new PXDataFieldValue("ApplicationName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) this.pApplicationName)))
    {
      userFromReader = this.GetUserFromReader(reader, lockoutTime);
      if (userFromReader != null)
      {
        if (sources != null)
        {
          if (!((IEnumerable<int>) sources).Contains<int>(userFromReader.Source))
            return (MembershipUser) null;
        }
      }
    }
    if (userIsOnline && userFromReader != null && !WebConfig.IsClusterEnabled)
    {
      System.DateTime dateTime = System.DateTime.Now;
      dateTime = dateTime.ToUniversalTime();
      if (dateTime.Subtract(userFromReader.LastActivityDate).TotalMinutes >= 15.0)
      {
        string userName = userFromReader.UserName;
        string pApplicationName = this.pApplicationName;
        PXDataFieldAssign[] pxDataFieldAssignArray = new PXDataFieldAssign[1];
        int? valueLength = new int?(8);
        dateTime = System.DateTime.Now;
        // ISSUE: variable of a boxed type
        __Boxed<System.DateTime> universalTime = (ValueType) dateTime.ToUniversalTime();
        pxDataFieldAssignArray[0] = new PXDataFieldAssign("LastActivityDate", PXDbType.DateTime, valueLength, (object) universalTime);
        PXDatabaseMembershipProvider.UpdateUser(userName, false, pApplicationName, pxDataFieldAssignArray);
      }
    }
    return (MembershipUser) userFromReader;
  }

  private MembershipUserExt GetUserFromReader(PXDataRecord reader, System.DateTime lockoutTime)
  {
    if (reader == null)
      return (MembershipUserExt) null;
    object guid = (object) reader.GetGuid(0);
    string str = reader.GetString(1);
    string email = reader.GetString(2);
    string passwordQuestion = "";
    if (reader.GetValue(3) != DBNull.Value)
      passwordQuestion = reader.GetString(3);
    string comment = "";
    if (reader.GetValue(4) != DBNull.Value)
      comment = reader.GetString(4);
    bool isApproved = reader.GetBoolean(5).Value;
    int num;
    if (!reader.IsDBNull(6))
    {
      System.DateTime? dateTime1 = reader.GetDateTime(6);
      System.DateTime dateTime2 = lockoutTime;
      num = dateTime1.HasValue ? (dateTime1.GetValueOrDefault() > dateTime2 ? 1 : 0) : 0;
    }
    else
      num = 0;
    bool isLockedOut = num != 0;
    System.DateTime valueOrDefault1 = reader.GetDateTime(7).GetValueOrDefault();
    System.DateTime? dateTime = reader.GetDateTime(8);
    System.DateTime valueOrDefault2 = dateTime.GetValueOrDefault();
    dateTime = reader.GetDateTime(9);
    System.DateTime valueOrDefault3 = dateTime.GetValueOrDefault();
    dateTime = reader.GetDateTime(10);
    System.DateTime valueOrDefault4 = dateTime.GetValueOrDefault();
    dateTime = reader.GetDateTime(11);
    System.DateTime valueOrDefault5 = dateTime.GetValueOrDefault();
    int valueOrDefault6 = reader.GetInt32(13).GetValueOrDefault();
    string firstName = reader.GetString(14);
    string lastName = reader.GetString(15);
    bool? boolean = reader.GetBoolean(16 /*0x10*/);
    bool valueOrDefault7 = boolean.GetValueOrDefault();
    boolean = reader.GetBoolean(17);
    bool valueOrDefault8 = boolean.GetValueOrDefault();
    string empty = reader.GetString(18);
    if (string.IsNullOrEmpty(empty))
      empty = string.Empty;
    MembershipUserExt userFromReader = new MembershipUserExt(this.Name, str, guid, email, passwordQuestion, comment, isApproved, isLockedOut, valueOrDefault8, valueOrDefault1, valueOrDefault2, valueOrDefault3, valueOrDefault4, valueOrDefault5, valueOrDefault6, firstName, lastName, empty, valueOrDefault7);
    string companyName = PXAccess.GetCompanyName();
    if (!string.IsNullOrEmpty(companyName))
      str = $"{str}@{companyName}";
    PXBaseMembershipProvider.UsersCache slot = PXDatabase.GetSlot<PXBaseMembershipProvider.UsersCache>("MembershipUser", typeof (Users));
    lock (((ICollection) slot).SyncRoot)
      slot[str] = userFromReader;
    return userFromReader;
  }

  public override bool UnlockUser(string username)
  {
    return PXDatabaseMembershipProvider.UpdateUser(username, false, this.pApplicationName, new PXDataFieldAssign("LockedOutDate", PXDbType.DateTime, new int?(8), (object) null));
  }

  public override string GetUserNameByEmail(string email)
  {
    string userNameByEmail = "";
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Users>(new PXDataField("Username"), (PXDataField) new PXDataFieldValue("Email", PXDbType.VarChar, new int?(128 /*0x80*/), (object) email), (PXDataField) new PXDataFieldValue("ApplicationName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) this.pApplicationName)))
    {
      if (pxDataRecord != null)
        userNameByEmail = pxDataRecord.GetString(0);
    }
    if (userNameByEmail == null)
      userNameByEmail = "";
    return userNameByEmail;
  }

  public override string ResetPassword(string username, string answer)
  {
    if (!this.EnablePasswordReset)
      throw new PXNotSupportedException("Password reset is not enabled.");
    if (answer == null && this.RequiresQuestionAndAnswer)
    {
      this.UpdateFailureCount(username, "PasswordAnswer");
      throw new PXProviderException("A password answer is required for password reset.");
    }
    string password = Membership.GeneratePassword(System.Math.Max(SitePolicy.PasswordMinLength, 8), this.MinRequiredNonAlphanumericCharacters);
    ValidatePasswordEventArgs e = new ValidatePasswordEventArgs(username, password, true);
    this.OnValidatingPassword(e);
    if (e.Cancel)
    {
      if (e.FailureInformation != null)
        throw e.FailureInformation;
      throw new PXMembershipPasswordException("The password reset has been canceled because of a password validation failure.");
    }
    System.DateTime lockoutTime = PXDatabaseMembershipProvider.LockoutTime;
    string dbpassword;
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Users>(new PXDataField("PasswordAnswer"), new PXDataField("LockedOutDate"), (PXDataField) new PXDataFieldValue("Username", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) username), (PXDataField) new PXDataFieldValue("ApplicationName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) this.pApplicationName)))
    {
      System.DateTime? nullable = pxDataRecord != null ? pxDataRecord.GetDateTime(1) : throw new PXMembershipPasswordException("The supplied username has not been found.");
      System.DateTime dateTime = lockoutTime;
      if ((nullable.HasValue ? (nullable.GetValueOrDefault() > dateTime ? 1 : 0) : 0) != 0)
        throw new PXMembershipPasswordException("The supplied user is locked out.");
      dbpassword = pxDataRecord.GetString(0);
    }
    if (this.RequiresQuestionAndAnswer && !this.CheckPassword(username, answer, dbpassword))
    {
      this.UpdateFailureCount(username, "PasswordAnswer");
      throw new PXMembershipPasswordException("Incorrect password answer");
    }
    if (PXDatabaseMembershipProvider.UpdateUser(username, false, this.pApplicationName, new PXDataFieldAssign("Password", PXDbType.NVarChar, new int?(512 /*0x0200*/), (object) PXDatabaseMembershipProvider.HashPassword(username, password), PXDBUserPasswordAttribute.DefaultVeil), new PXDataFieldAssign("LastPasswordChangedDate", PXDbType.DateTime, new int?(8), (object) System.DateTime.Now.ToUniversalTime()), new PXDataFieldAssign("GuidForPasswordRecovery", PXDbType.VarChar, (object) "")
    {
      IsChanged = false
    }, new PXDataFieldAssign("PasswordRecoveryExpirationDate", PXDbType.DateTime, (object) null)
    {
      IsChanged = false
    }))
      return password;
    throw new PXMembershipPasswordException("The user has not been found or is locked out. The password has not been reset.");
  }

  public override void UpdateUser(MembershipUser user)
  {
    PXDatabaseMembershipProvider.UpdateUser(user.UserName, false, this.pApplicationName, new PXDataFieldAssign("Email", PXDbType.VarChar, new int?(128 /*0x80*/), (object) user.Email), new PXDataFieldAssign("Comment", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) user.Comment), new PXDataFieldAssign("IsApproved", PXDbType.Bit, new int?(1), (object) user.IsApproved));
  }

  internal static bool ValidateUserIP(HttpRequestBase request, string username)
  {
    PXBaseMembershipProvider.AssertNotNull(request);
    foreach (string str in request.GetUserHostsChain())
    {
      IPAddress ipAddress1;
      byte[] source = Net_Utils.TryParseIPAddress(str, ref ipAddress1) ? ipAddress1.GetAddressBytes() : throw new Exception($"The system cannot parse the user IP address from '{str}'.");
      if (source.Length != 4)
        return true;
      bool flag1 = false;
      bool flag2 = false;
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<UserFilter>((PXDataField) new PXDataField<UserFilter.startIPAddress>(), (PXDataField) new PXDataField<UserFilter.endIPAddress>(), (PXDataField) new PXDataFieldValue<UserFilter.username>((object) username)))
      {
        using (pxDataRecord)
        {
          flag2 = true;
          IPAddress ipAddress2 = UserFilter.DecodeIpAddress(pxDataRecord.GetString(0));
          IPAddress ipAddress3 = UserFilter.DecodeIpAddress(pxDataRecord.GetString(1));
          if (ipAddress2 == null && ipAddress3 == null)
          {
            flag1 = true;
            break;
          }
          IPAddress ipAddress4 = ipAddress2 ?? Net_Utils.ParseIPAddress("0.0.0.0");
          IPAddress ipAddress5 = ipAddress3 ?? Net_Utils.ParseIPAddress("255.255.255.255");
          byte[] startBytes = ipAddress4.GetAddressBytes();
          byte[] endBytes = ipAddress5.GetAddressBytes();
          if (startBytes.Length == 4)
          {
            if (endBytes.Length == 4)
            {
              flag1 = !((IEnumerable<byte>) source).Where<byte>((Func<byte, int, bool>) ((t, i) => (int) startBytes[i] > (int) t || (int) t > (int) endBytes[i])).Any<byte>();
              if (flag1)
                break;
            }
          }
        }
      }
      bool flag3 = !flag2 | flag1;
      if (!flag3)
      {
        PXAuditJournal.Register(PXAuditJournal.Operation.LoginFailed, username, ipAddress1.ToString(), "The IP address is out of range.");
        return flag3;
      }
    }
    return true;
  }

  internal static bool ValidateExternalAccessRights(MembershipUser user)
  {
    bool flag = PXSiteMap.IsPortal || PortalHelper.ValidateAccessRights(user);
    if (flag)
      return flag;
    PXAuditJournal.Register(PXAuditJournal.Operation.LoginFailed, user.UserName, (string) null, "The user type does not match the application.");
    return flag;
  }

  protected internal override bool ValidateUserPassword(
    string username,
    string password,
    bool onlyAllowed,
    bool skipForbidWithPasswordCheck = false)
  {
    System.DateTime lockoutTime = PXDatabaseMembershipProvider.LockoutTime;
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    string str1 = (string) null;
    bool flag4 = false;
    string companyName = PXAccess.GetCompanyName();
    string[] strArray = PXDatabase.Companies.Length == 0 ? new string[1] : PXDatabase.Companies;
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Users>(new PXDataField("Password"), new PXDataField("IsApproved"), new PXDataField("LockedOutDate"), new PXDataField("Guest"), new PXDataField("IsPendingActivation"), (PXDataField) new PXDataField<Users.forbidLoginWithPassword>(), (PXDataField) new PXDataFieldValue("Username", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) username), (PXDataField) new PXDataFieldValue("ApplicationName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) this.pApplicationName)))
    {
      if (pxDataRecord == null)
        return this.MakeEmptyHashIterationsToAvoidTimeBasedAttack(onlyAllowed);
      str1 = pxDataRecord.GetString(0);
      bool? boolean1 = pxDataRecord.GetBoolean(1);
      bool flag5 = true;
      flag1 = boolean1.GetValueOrDefault() == flag5 & boolean1.HasValue;
      int num;
      if (!pxDataRecord.IsDBNull(2))
      {
        System.DateTime? dateTime1 = pxDataRecord.GetDateTime(2);
        System.DateTime dateTime2 = lockoutTime;
        num = dateTime1.HasValue ? (dateTime1.GetValueOrDefault() > dateTime2 ? 1 : 0) : 0;
      }
      else
        num = 0;
      flag2 = num != 0;
      bool? boolean2 = pxDataRecord.GetBoolean(4);
      bool flag6 = true;
      flag3 = boolean2.GetValueOrDefault() == flag6 & boolean2.HasValue;
      if (flag1 && !flag2)
      {
        boolean2 = pxDataRecord.GetBoolean(3);
        if (boolean2.GetValueOrDefault())
          flag1 = this.EnableGuestAccess;
      }
      boolean2 = pxDataRecord.GetBoolean(5);
      bool flag7 = true;
      flag4 = boolean2.GetValueOrDefault() == flag7 & boolean2.HasValue;
    }
    if (!skipForbidWithPasswordCheck & flag4)
      return this.MakeEmptyHashIterationsToAvoidTimeBasedAttack(onlyAllowed);
    bool flag8 = this.CheckPassword(username, password, str1);
    if (!PasswordHashing.IsPasswordMigrated(str1) || !flag8 && this.CheckPassword(username, password, str1, false))
    {
      flag8 = false;
      lock (this.syncRoot)
      {
        foreach (string str2 in strArray)
        {
          using (new PXLoginScope(str2 == null ? username : $"{username}@{str2}", Array.Empty<string>()))
          {
            PXDatabase.ResetCredentials();
            string str3;
            string dbpassword;
            using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Users>(new PXDataField("Password"), new PXDataField("Source"), (PXDataField) new PXDataFieldValue("Username", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) username)))
            {
              if (pxDataRecord != null)
              {
                int? int32 = pxDataRecord.GetInt32(1);
                int num = 0;
                if (int32.GetValueOrDefault() == num & int32.HasValue)
                {
                  str3 = pxDataRecord.GetString(0);
                  dbpassword = str3;
                }
                else
                  continue;
              }
              else
                continue;
            }
            if (!PasswordHashing.IsPasswordMigrated(str3))
            {
              bool flag9 = false;
              switch (SitePolicy.PasswordSecurityType)
              {
                case MembershipPasswordFormat.Clear:
                  dbpassword = PXDatabaseMembershipProvider.HashPassword(username, str3);
                  break;
                case MembershipPasswordFormat.Hashed:
                  if (ConstantTimeStringComparer.AreEqual(this.CompatibilityHashPassword(password), str3))
                  {
                    dbpassword = PXDatabaseMembershipProvider.HashPassword(username, password);
                    break;
                  }
                  break;
                case MembershipPasswordFormat.Encrypted:
                  dbpassword = PXDatabaseMembershipProvider.HashPassword(username, this.UnEncodePassword(str3, MembershipPasswordFormat.Encrypted));
                  break;
                default:
                  throw new PXProviderException("The password format is not supported.");
              }
              if (!flag9)
              {
                if (this.CheckPassword(username, password, dbpassword))
                {
                  if (str3 != dbpassword)
                  {
                    PXDatabaseMembershipProvider.UpdateUser(username, false, this.pApplicationName, new PXDataFieldAssign("Password", (object) dbpassword));
                    if (str2 == companyName)
                      str1 = dbpassword;
                  }
                }
              }
            }
            else if (!this.CheckPassword(username, password, str3))
            {
              if (this.CheckPassword(username, password, str3, false))
              {
                this.UpdateUserPassword(username, password);
                flag8 = true;
              }
            }
          }
        }
      }
    }
    if (onlyAllowed && (!flag1 || flag2 || flag3))
      return false;
    return flag8 || this.CheckPassword(username, password, str1);
  }

  /// <summary>
  /// Makes empty hash iterations to avoid time based attack. Always return <see langword="false" />.
  /// </summary>
  /// <returns>
  /// Always return <see langword="false" />.
  /// </returns>
  private bool MakeEmptyHashIterationsToAvoidTimeBasedAttack(bool onlyAllowed)
  {
    bool avoidTimeBasedAttack = this.CheckPassword(PXBaseMembershipProvider._mockUserName, PXBaseMembershipProvider._mockedPassword, PXBaseMembershipProvider._mockedSavedPassword);
    if (!onlyAllowed && !avoidTimeBasedAttack)
      avoidTimeBasedAttack = this.CheckPassword(PXBaseMembershipProvider._mockUserName, PXBaseMembershipProvider._mockedPassword, PXBaseMembershipProvider._mockedSavedPassword);
    return avoidTimeBasedAttack;
  }

  private bool ValidateUserHash(string username, string hash)
  {
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle(typeof (Users), new PXDataField("GuidForPasswordRecovery"), new PXDataField("PasswordRecoveryExpirationDate"), (PXDataField) new PXDataFieldValue("UserName", (object) username)))
    {
      int num;
      if (pxDataRecord != null && pxDataRecord.GetString(0) == hash)
      {
        System.DateTime? dateTime = pxDataRecord.GetDateTime(1);
        if (dateTime.HasValue)
        {
          dateTime = pxDataRecord.GetDateTime(1);
          System.DateTime now = PXTimeZoneInfo.Now;
          num = dateTime.HasValue ? (dateTime.GetValueOrDefault() >= now ? 1 : 0) : 0;
          goto label_5;
        }
      }
      num = 0;
label_5:
      return num != 0;
    }
  }

  public override bool ValidateUser(string username, string password)
  {
    HttpContext current = HttpContext.Current;
    return this.ValidateUser(current != null ? current.GetRequestBase() : (HttpRequestBase) null, username, password, true);
  }

  public bool ValidateUser(
    HttpRequestBase request,
    string username,
    string password,
    bool throwException)
  {
    PXBaseMembershipProvider.AssertNotNull(request);
    MembershipUser user = this.GetUser(username, true);
    if (this.ValidateUserPassword(username, password, true, false) && PXDatabaseMembershipProvider.ValidateUserIP(request, username) && PXDatabaseMembershipProvider.ValidateExternalAccessRights(user))
      return true;
    if (user != null)
      username = user.UserName;
    if (throwException || user != null)
    {
      int? companyId = SlotStore.Instance.GetSingleCompanyId();
      Task.Run((System.Action) (() =>
      {
        using (IDisposableSlotStorageProvider slots = SlotStore.AsyncLocal())
        {
          using (ILifetimeScope ilifetimeScope = ((ISlotStore) slots).BeginLifetimeScope())
          {
            if (companyId.HasValue)
              ((ISlotStore) slots).SetSingleCompanyId(companyId.GetValueOrDefault());
            using (PXTransactionScope transactionScope = new PXTransactionScope(true))
            {
              try
              {
                this.UpdateFailureCount(username, "Password");
                transactionScope.Complete();
              }
              catch (Exception ex)
              {
                LoggerExtensions.LogError((ILogger) ResolutionExtensions.Resolve<ILogger<PXDatabaseMembershipProvider>>((IComponentContext) ilifetimeScope), ex, "An error occurred while updating the password failure counter for the user {UserName}.", new object[1]
                {
                  (object) username
                });
              }
            }
          }
        }
      }));
    }
    return false;
  }

  /// <summary>
  /// Finds user with 'GuidForPasswordRecovery' field equal to 'hash'.
  /// </summary>
  /// <param name="hash">Hash to find.</param>
  /// <returns>Login of the user with matching 'GuidForPasswordRecovery' field in case of success. String.Empty in case of failure.</returns>
  public static string FindUserByHash(string hash)
  {
    if (string.IsNullOrEmpty(hash))
      return string.Empty;
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle(typeof (Users), new PXDataField("PKID"), new PXDataField("Username"), (PXDataField) new PXDataFieldValue("GuidForPasswordRecovery", (object) hash)))
      return pxDataRecord == null || pxDataRecord.IsDBNull(0) || pxDataRecord.IsDBNull(1) ? string.Empty : pxDataRecord.GetString(1);
  }

  internal void UpdateFailureCount(string username, string failureType)
  {
    System.DateTime dateTime1 = new System.DateTime();
    int num1 = 0;
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Users>(new PXDataField($"Failed{failureType}AttemptCount"), new PXDataField($"Failed{failureType}AttemptWindowStart"), (PXDataField) new PXDataFieldValue("Username", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) username), (PXDataField) new PXDataFieldValue("ApplicationName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) this.pApplicationName)))
    {
      if (pxDataRecord != null)
      {
        num1 = !pxDataRecord.IsDBNull(0) ? pxDataRecord.GetInt32(0).Value : 0;
        dateTime1 = !pxDataRecord.IsDBNull(1) ? pxDataRecord.GetDateTime(1).Value : System.DateTime.Now.ToUniversalTime();
      }
    }
    System.DateTime dateTime2 = dateTime1.AddMinutes((double) this.PasswordAttemptWindow);
    if (num1 == 0 || System.DateTime.Now.ToUniversalTime() > dateTime2)
    {
      if (!PXDatabaseMembershipProvider.UpdateUser(username, false, this.pApplicationName, new PXDataFieldAssign($"Failed{failureType}AttemptCount", PXDbType.Int, new int?(4), (object) 1), new PXDataFieldAssign($"Failed{failureType}AttemptWindowStart", PXDbType.DateTime, new int?(8), (object) System.DateTime.Now.ToUniversalTime())))
        throw new PXProviderException("Unable to update the failure count and to start a window.");
    }
    else
    {
      int num2;
      if ((num2 = num1 + 1) >= this.MaxInvalidPasswordAttempts)
      {
        List<PXDataFieldAssign> pxDataFieldAssignList = new List<PXDataFieldAssign>()
        {
          new PXDataFieldAssign("LockedOutDate", PXDbType.DateTime, new int?(8), (object) System.DateTime.Now.ToUniversalTime()),
          new PXDataFieldAssign("LastLockedOutDate", PXDbType.DateTime, new int?(8), (object) System.DateTime.Now.ToUniversalTime())
        };
        if (num2 == this.MaxInvalidPasswordAttempts)
          pxDataFieldAssignList.Add(new PXDataFieldAssign($"Failed{failureType}AttemptCount", PXDbType.Int, new int?(4), (object) num2));
        if (!PXDatabaseMembershipProvider.UpdateUser(username, false, this.pApplicationName, pxDataFieldAssignList.ToArray()))
          throw new PXProviderException("The user cannot be locked out.");
      }
      else if (!PXDatabaseMembershipProvider.UpdateUser(username, false, this.pApplicationName, new PXDataFieldAssign($"Failed{failureType}AttemptCount", PXDbType.Int, new int?(4), (object) num2)))
        throw new PXProviderException("The failure count cannot be updated.");
    }
  }

  private bool CheckPassword(
    string username,
    string password,
    string dbpassword,
    bool ignoreUsernameCase = true)
  {
    PXBaseMembershipProvider.UserWithPasswordCacheKey key = new PXBaseMembershipProvider.UserWithPasswordCacheKey(username, dbpassword);
    string str;
    if (PXDatabaseMembershipProvider._hashedPasswords.TryGetValue(key, out str) && !Str.IsNullOrEmpty(str) && ConstantTimeStringComparer.AreEqual(PXDatabaseMembershipProvider.HashPassword(username, password, ignoreUsernameCase, false), str))
      return true;
    if (!ConstantTimeStringComparer.AreEqual(PXDatabaseMembershipProvider.HashPassword(username, password, ignoreUsernameCase), dbpassword))
      return false;
    PXDatabaseMembershipProvider._hashedPasswords[key] = PXDatabaseMembershipProvider.HashPassword(username, password, ignoreUsernameCase, false);
    return true;
  }

  /// <summary>Old hash function for migration purposes.</summary>
  private string CompatibilityHashPassword(string password)
  {
    return Convert.ToBase64String(new HMACSHA1(PXDatabaseMembershipProvider.HexToByte(this._machineKeyOptions.ValidationKey)).ComputeHash(Encoding.Unicode.GetBytes(password)));
  }

  /// <summary>
  /// Hashes password with salt, converts it to Base64 and adds migrates password prefix to it.
  /// </summary>
  private static string HashPassword(
    string username,
    string password,
    bool ignoreUsernameCase = true,
    bool dbpassword = true)
  {
    if (password == null)
      return (string) null;
    return PasswordHashing.IsPasswordMigrated(password) ? password : PasswordHashing.GetOrSetCachedHash(username, password, dbpassword, (Func<string>) (() => PasswordHashing.HashPasswordWithSalt(password, ignoreUsernameCase ? username.ToLower() : username, dbpassword)));
  }

  private string UnEncodePassword(string encodedPassword, MembershipPasswordFormat passwordFormat)
  {
    string s = encodedPassword;
    try
    {
      switch (passwordFormat)
      {
        case MembershipPasswordFormat.Clear:
          break;
        case MembershipPasswordFormat.Hashed:
          throw new PXProviderException("Hashed passwords can't be decoded.");
        case MembershipPasswordFormat.Encrypted:
          s = Encoding.Unicode.GetString(this.DecryptPassword(Convert.FromBase64String(s)));
          break;
        default:
          throw new PXProviderException("The password format is not supported.");
      }
    }
    catch
    {
    }
    return s;
  }

  private static byte[] HexToByte(string hexString)
  {
    byte[] numArray = new byte[hexString.Length / 2];
    for (int index = 0; index < numArray.Length; ++index)
      numArray[index] = Convert.ToByte(hexString.Substring(index * 2, 2), 16 /*0x10*/);
    return numArray;
  }

  public override MembershipUserCollection FindUsersByName(
    string usernameToMatch,
    int pageIndex,
    int pageSize,
    out int totalRecords)
  {
    totalRecords = 0;
    MembershipUserCollection usersByName = new MembershipUserCollection();
    int num1 = pageSize * pageIndex;
    int num2 = num1 + pageSize - 1;
    System.DateTime lockoutTime = PXDatabaseMembershipProvider.LockoutTime;
    foreach (PXDataRecord reader in PXDatabase.SelectMulti<Users>(new PXDataField("PKID"), new PXDataField("Username"), new PXDataField("Email"), new PXDataField("PasswordQuestion"), new PXDataField("Comment"), new PXDataField("IsApproved"), new PXDataField("LockedOutDate"), new PXDataField("CreationDate"), new PXDataField("LastLoginDate"), new PXDataField("LastActivityDate"), new PXDataField("LastPasswordChangedDate"), new PXDataField("LastLockedOutDate"), new PXDataField("ExtRef"), new PXDataField("Source"), new PXDataField("FirstName"), new PXDataField("LastName"), new PXDataField("Guest"), new PXDataField("IsPendingActivation"), new PXDataField("FullName"), (PXDataField) new PXDataFieldValue("ApplicationName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) this.pApplicationName)))
    {
      MembershipUser userFromReader = (MembershipUser) this.GetUserFromReader(reader, lockoutTime);
      if (userFromReader.UserName.StartsWith(usernameToMatch, StringComparison.CurrentCultureIgnoreCase))
      {
        if (totalRecords >= num1 && totalRecords <= num2)
          usersByName.Add(userFromReader);
        ++totalRecords;
      }
    }
    return usersByName;
  }

  public override MembershipUserCollection FindUsersByEmail(
    string emailToMatch,
    int pageIndex,
    int pageSize,
    out int totalRecords)
  {
    totalRecords = 0;
    MembershipUserCollection usersByEmail = new MembershipUserCollection();
    int num1 = pageSize * pageIndex;
    int num2 = num1 + pageSize - 1;
    System.DateTime lockoutTime = PXDatabaseMembershipProvider.LockoutTime;
    foreach (PXDataRecord reader in PXDatabase.SelectMulti<Users>(new PXDataField("PKID"), new PXDataField("Username"), new PXDataField("Email"), new PXDataField("PasswordQuestion"), new PXDataField("Comment"), new PXDataField("IsApproved"), new PXDataField("LockedOutDate"), new PXDataField("CreationDate"), new PXDataField("LastLoginDate"), new PXDataField("LastActivityDate"), new PXDataField("LastPasswordChangedDate"), new PXDataField("LastLockedOutDate"), new PXDataField("ExtRef"), new PXDataField("Source"), new PXDataField("FirstName"), new PXDataField("LastName"), new PXDataField("Guest"), new PXDataField("IsPendingActivation"), new PXDataField("FullName"), (PXDataField) new PXDataFieldValue("ApplicationName", PXDbType.VarChar, new int?((int) byte.MaxValue), (object) this.pApplicationName)))
    {
      MembershipUser userFromReader = (MembershipUser) this.GetUserFromReader(reader, lockoutTime);
      if (userFromReader.Email.StartsWith(emailToMatch, StringComparison.CurrentCultureIgnoreCase))
      {
        if (totalRecords >= num1 && totalRecords <= num2)
          usersByEmail.Add(userFromReader);
        ++totalRecords;
      }
    }
    return usersByEmail;
  }

  protected override void OnValidatingPassword(ValidatePasswordEventArgs e)
  {
    e.FailureInformation = PXDatabaseMembershipProvider.ValidateAgainstPasswordPolicy(e.Password);
    if (e.FailureInformation != null)
      e.Cancel = true;
    base.OnValidatingPassword(e);
  }

  internal static Exception ValidateAgainstPasswordPolicy(string password)
  {
    Exception exception = (Exception) null;
    if (password.Length < SitePolicy.PasswordMinLength)
      exception = (Exception) new PXException("The password length must be at least {0} characters.", new object[1]
      {
        (object) SitePolicy.PasswordMinLength
      });
    else if (SitePolicy.PasswordComplexity)
    {
      int num = 0;
      for (int index = 0; index < PXDatabaseMembershipProvider.PasswordComplexityRegex.Length; ++index)
      {
        if (PXDatabaseMembershipProvider.PasswordComplexityRegex[index].IsMatch(password))
          ++num;
      }
      if (num < 3)
        exception = (Exception) new PXException("The password is not complex.~~Passwords must contain characters from three of the following four categories:~English uppercase characters (A through Z)~English lowercase characters (a through z)~Base 10 digits (0 through 9)~Non-alphabetic characters (such as !, $, #, %)");
    }
    if (!string.IsNullOrEmpty(SitePolicy.PasswordRegex) && new Regex(SitePolicy.PasswordRegex).Match(password).ToString() != password)
      exception = (Exception) new PXException(SitePolicy.PasswordRegexMessage);
    return exception;
  }

  private static System.DateTime LockoutTime
  {
    get
    {
      System.DateTime dateTime = System.DateTime.Now;
      dateTime = dateTime.ToUniversalTime();
      return dateTime.AddMinutes((double) -SitePolicy.AccountLockoutDuration);
    }
  }

  protected internal override void UpdateUserPassword(string username, string password)
  {
    PXDatabaseMembershipProvider.UpdateUser(username, false, this.pApplicationName, new PXDataFieldAssign("Password", PXDbType.NVarChar, new int?(512 /*0x0200*/), (object) PXDatabaseMembershipProvider.HashPassword(username, password), PXDBUserPasswordAttribute.DefaultVeil), new PXDataFieldAssign("LastPasswordChangedDate", PXDbType.DateTime, new int?(8), (object) System.DateTime.Now.ToUniversalTime()), new PXDataFieldAssign("GuidForPasswordRecovery", PXDbType.VarChar, (object) ""), new PXDataFieldAssign("PasswordRecoveryExpirationDate", PXDbType.DateTime, (object) null));
  }
}
