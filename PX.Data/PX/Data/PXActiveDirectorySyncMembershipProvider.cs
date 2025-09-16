// Decompiled with JetBrains decompiler
// Type: PX.Data.PXActiveDirectorySyncMembershipProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Access.ActiveDirectory;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Security;

#nullable disable
namespace PX.Data;

/// <exclude />
[PXInternalUseOnly]
public sealed class PXActiveDirectorySyncMembershipProvider : PXBaseMembershipProvider
{
  private static string _applicationName;
  private MembershipProvider _dbProvider;
  private IActiveDirectoryProvider _adProvider;

  public override void Initialize(string name, NameValueCollection config)
  {
    base.Initialize(name, config);
    string typeName = config["mainProviderType"];
    if (typeName != null && !string.IsNullOrEmpty(typeName))
    {
      System.Type type = PXBuildManager.GetType(typeName, false);
      if ((object) type != null && type.GetConstructor(new System.Type[0]) != (ConstructorInfo) null)
      {
        this._dbProvider = (MembershipProvider) Activator.CreateInstance(type);
        goto label_4;
      }
    }
    this._dbProvider = (MembershipProvider) new PXDatabaseMembershipProvider();
label_4:
    this._dbProvider.Initialize(name, config);
    PXActiveDirectorySyncMembershipProvider._applicationName = this._dbProvider.ApplicationName;
    this._adProvider = ProviderBaseDependencyHelper.Resolve<IActiveDirectoryProvider>();
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
    return this._dbProvider.CreateUser(username, password, email, passwordAnswer, passwordAnswer, isApproved, providerUserKey, out status);
  }

  public override bool ChangePasswordQuestionAndAnswer(
    string username,
    string password,
    string newPasswordQuestion,
    string newPasswordAnswer)
  {
    return this._dbProvider.ChangePasswordQuestionAndAnswer(username, password, newPasswordQuestion, newPasswordAnswer);
  }

  public override string GetPassword(string username, string answer)
  {
    return this._dbProvider.GetPassword(username, answer);
  }

  public override bool ChangePassword(string username, string oldPassword, string newPassword)
  {
    return this._dbProvider.ChangePassword(username, oldPassword, newPassword);
  }

  public override string ResetPassword(string username, string answer)
  {
    return this._dbProvider.ResetPassword(username, answer);
  }

  public override void UpdateUser(MembershipUser user) => this._dbProvider.UpdateUser(user);

  public override bool ValidateUser(string username, string password)
  {
    HttpContext current = HttpContext.Current;
    return this.ValidateUser(current != null ? current.GetRequestBase() : (HttpRequestBase) null, username, password, out string _);
  }

  protected internal override bool ValidateUser(
    HttpRequestBase request,
    string username,
    string password,
    out string providerLogin)
  {
    providerLogin = (string) null;
    bool flag = this._dbProvider is PXDatabaseMembershipProvider ? ((PXDatabaseMembershipProvider) this._dbProvider).ValidateUser(request, username, password, false) : this._dbProvider.ValidateUser(username, password);
    if (flag)
    {
      providerLogin = username;
    }
    else
    {
      MembershipUser adUser;
      flag = this._adProvider.ValidateUser(username, password) && (adUser = this.GetADUser((object) username)) != null && PXDatabaseMembershipProvider.ValidateUserIP(request, providerLogin = adUser.UserName) && PXDatabaseMembershipProvider.ValidateExternalAccessRights(adUser) && adUser.IsApproved;
      if (flag && !PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+ActiveDirectoryAndOtherExternalSSO"))
        throw new PXException(PXMessages.LocalizeFormatNoPrefix("You cannot sign in using an external provider because the {0} feature is disabled on the Enable/Disable Features (CS100000) form.", (object) "ActiveDirectoryAndOtherExternalSSO"));
    }
    return flag;
  }

  public override bool UnlockUser(string userName) => this._dbProvider.UnlockUser(userName);

  public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
  {
    return !(providerUserKey is string) || !string.IsNullOrEmpty((string) providerUserKey) ? this._dbProvider.GetUser(providerUserKey, userIsOnline) ?? this.GetADUser(providerUserKey) : (MembershipUser) null;
  }

  public override MembershipUser GetUser(string username, bool userIsOnline)
  {
    return !string.IsNullOrEmpty(username) ? this._dbProvider.GetUser(username, userIsOnline) ?? this.GetADUser((object) username) : (MembershipUser) null;
  }

  public override string GetUserNameByEmail(string email)
  {
    return this._dbProvider.GetUserNameByEmail(email);
  }

  public override bool DeleteUser(string username, bool deleteAllRelatedData)
  {
    return this._dbProvider.DeleteUser(username, deleteAllRelatedData);
  }

  public override MembershipUserCollection GetAllUsers(
    int pageIndex,
    int pageSize,
    out int totalRecords)
  {
    return this._dbProvider.GetAllUsers(pageIndex, pageSize, out totalRecords);
  }

  public override int GetNumberOfUsersOnline() => this._dbProvider.GetNumberOfUsersOnline();

  public override MembershipUserCollection FindUsersByName(
    string usernameToMatch,
    int pageIndex,
    int pageSize,
    out int totalRecords)
  {
    return this._dbProvider.FindUsersByName(usernameToMatch, pageIndex, pageSize, out totalRecords);
  }

  public override MembershipUserCollection FindUsersByEmail(
    string emailToMatch,
    int pageIndex,
    int pageSize,
    out int totalRecords)
  {
    return this._dbProvider.FindUsersByEmail(emailToMatch, pageIndex, pageSize, out totalRecords);
  }

  public override bool EnablePasswordRetrieval => this._dbProvider.EnablePasswordRetrieval;

  public override bool EnablePasswordReset => this._dbProvider.EnablePasswordReset;

  public override bool RequiresQuestionAndAnswer => this._dbProvider.RequiresQuestionAndAnswer;

  public override string ApplicationName
  {
    get => this._dbProvider.ApplicationName;
    set => this._dbProvider.ApplicationName = value;
  }

  public override int MaxInvalidPasswordAttempts => this._dbProvider.MaxInvalidPasswordAttempts;

  public override int PasswordAttemptWindow => this._dbProvider.PasswordAttemptWindow;

  public override bool RequiresUniqueEmail => this._dbProvider.RequiresUniqueEmail;

  public override MembershipPasswordFormat PasswordFormat => this._dbProvider.PasswordFormat;

  public override int MinRequiredPasswordLength => this._dbProvider.MinRequiredPasswordLength;

  public override int MinRequiredNonAlphanumericCharacters
  {
    get => this._dbProvider.MinRequiredNonAlphanumericCharacters;
  }

  public override string PasswordStrengthRegularExpression
  {
    get => this._dbProvider.PasswordStrengthRegularExpression;
  }

  public override bool ConcurrentUserMode
  {
    get
    {
      return this._dbProvider is PXDatabaseMembershipProvider && (this._dbProvider as PXDatabaseMembershipProvider).ConcurrentUserMode;
    }
  }

  private MembershipUser GetADUser(object providerUserKey)
  {
    string str = (string) null;
    PX.Data.Access.ActiveDirectory.User user;
    if (providerUserKey is string login)
    {
      string username = this.LegacyCompanyService.ExtractUsername(login);
      if (username != null)
      {
        user = this._adProvider.GetUser((object) username) ?? this._adProvider.GetUserByLogin(username);
        str = username;
        goto label_5;
      }
    }
    user = this._adProvider.GetUser(providerUserKey) ?? this._adProvider.GetUserBySID(providerUserKey.ToString());
    if (user != null)
      str = user.Name.DomainLogin;
label_5:
    if (user == null)
      return (MembershipUser) null;
    string companyName = PXAccess.GetCompanyName();
    string key = string.IsNullOrEmpty(companyName) ? str : $"{str}@{companyName}";
    Guid providerUserKey1 = PXActiveDirectorySyncMembershipProvider.TrySynchronizeInternalUser(key, user);
    MembershipUserExt adUser1 = (MembershipUserExt) null;
    PXBaseMembershipProvider.UsersCache slot = PXDatabase.GetSlot<PXBaseMembershipProvider.UsersCache>("MembershipUser", typeof (Users));
    lock (((ICollection) slot).SyncRoot)
      slot.TryGetValue(key, out adUser1);
    if (adUser1 != null)
      return (MembershipUser) adUser1;
    bool isApproved = false;
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Users>(new PXDataField("IsApproved"), (PXDataField) new PXDataFieldValue("Username", (object) user.Name.DomainLogin)))
    {
      bool? boolean = pxDataRecord.GetBoolean(0);
      bool flag = true;
      isApproved = boolean.GetValueOrDefault() == flag & boolean.HasValue;
    }
    MembershipUserExt adUser2 = new MembershipUserExt(this.Name, user.Name.DomainLogin, (object) providerUserKey1, user.Email, string.Empty, user.Comment, isApproved, false, false, user.CreationDate, user.LastLogonDate, System.DateTime.Now, user.LastPwdSetDate, System.DateTime.MinValue, 1, (string) null, (string) null, user.DisplayName, false);
    lock (((ICollection) slot).SyncRoot)
      slot[key] = adUser2;
    return (MembershipUser) adUser2;
  }

  public static void CheckAndRenameDeletedADUser(string username, string sid)
  {
    string str1 = $"Deleted_from_AD_{username}";
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      HashSet<string> hashSet = PXDatabase.SelectRecords<Users>((PXDataField) new PXDataField<Users.username>(), (PXDataField) new PXDataFieldValue<Users.applicationName>(PXDbType.VarChar, new int?(32 /*0x20*/), (object) PXActiveDirectorySyncMembershipProvider._applicationName), (PXDataField) new PXDataFieldValue<Users.username>(PXDbType.NVarChar, new int?(256 /*0x0100*/), (object) (str1 + "%"), PXComp.LIKE), (PXDataField) new PXDataFieldValue<Users.extRef>(PXDbType.NVarChar, new int?(512 /*0x0200*/), (object) sid, PXComp.NE)).Select<Users, string>((Func<Users, string>) (x => x.Username)).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      if (hashSet.Contains(str1))
      {
        int num = 1;
        string str2 = str1;
        while (num == 1 || hashSet.Contains(str2))
          str2 = $"{str1}&{++num}";
        str1 = str2;
      }
      if (PXDatabase.Update<Users>((PXDataFieldParam) new PXDataFieldAssign<Users.username>(PXDbType.NVarChar, new int?(256 /*0x0100*/), (object) str1), (PXDataFieldParam) new PXDataFieldAssign<Users.isApproved>(PXDbType.Bit, new int?(1), (object) 0), (PXDataFieldParam) new PXDataFieldRestrict<Users.applicationName>(PXDbType.VarChar, new int?(32 /*0x20*/), (object) PXActiveDirectorySyncMembershipProvider._applicationName), (PXDataFieldParam) new PXDataFieldRestrict<Users.username>(PXDbType.NVarChar, new int?(256 /*0x0100*/), (object) username), (PXDataFieldParam) new PXDataFieldRestrict<Users.extRef>(PXDbType.NVarChar, new int?(512 /*0x0200*/), (object) sid, PXComp.NE)))
        PXDatabase.Delete<UsersInRoles>((PXDataFieldRestrict) new PXDataFieldRestrict<UsersInRoles.username>(PXDbType.NVarChar, new int?(64 /*0x40*/), (object) username));
      transactionScope.Complete();
    }
  }

  public static Guid TrySynchronizeInternalUser(string key, PX.Data.Access.ActiveDirectory.User user)
  {
    return PXActiveDirectorySyncMembershipProvider.TrySynchronizeInternalUser(key, user, out bool _);
  }

  internal static Guid TrySynchronizeInternalUser(string key, PX.Data.Access.ActiveDirectory.User user, out bool isNewUser)
  {
    isNewUser = false;
    string a1;
    string a2;
    if (string.IsNullOrEmpty(user.FirstName) && string.IsNullOrEmpty(user.LastName))
    {
      a1 = user.DisplayName;
      a2 = (string) null;
    }
    else
    {
      a1 = user.FirstName;
      a2 = user.LastName;
    }
    if (!string.IsNullOrEmpty(key))
    {
      PXBaseMembershipProvider.UsersCache slot = PXDatabase.GetSlot<PXBaseMembershipProvider.UsersCache>("MembershipUser", typeof (Users));
      if (slot != null)
      {
        MembershipUserExt membershipUserExt;
        lock (((ICollection) slot).SyncRoot)
          slot.TryGetValue(key, out membershipUserExt);
        if (membershipUserExt != null && membershipUserExt.Source == 1 && string.Equals(user.Email, membershipUserExt.Email))
          return (Guid) membershipUserExt.ProviderUserKey;
      }
    }
    PXDataRecord pxDataRecord1 = PXDatabase.SelectSingle<Users>((PXDataField) new PXDataField<Users.pKID>(), (PXDataField) new PXDataField<Users.firstName>(), (PXDataField) new PXDataField<Users.lastName>(), (PXDataField) new PXDataField<Users.email>(), (PXDataField) new PXDataField<Users.isAssigned>(), (PXDataField) new PXDataFieldValue<Users.username>(PXDbType.NVarChar, new int?(64 /*0x40*/), (object) user.Name.DomainLogin), (PXDataField) new PXDataFieldValue<Users.extRef>(PXDbType.NVarChar, new int?(512 /*0x0200*/), (object) user.SID), (PXDataField) new PXDataFieldValue<Users.applicationName>(PXDbType.VarChar, new int?((int) byte.MaxValue), (object) PXActiveDirectorySyncMembershipProvider._applicationName));
    Guid guid;
    if (pxDataRecord1 == null)
    {
      PXDataRecord pxDataRecord2 = PXDatabase.SelectSingle<Users>((PXDataField) new PXDataField<Users.pKID>(), (PXDataField) new PXDataField<Users.username>(), (PXDataField) new PXDataFieldValue<Users.extRef>(PXDbType.NVarChar, new int?(512 /*0x0200*/), (object) user.SID), (PXDataField) new PXDataFieldValue<Users.applicationName>(PXDbType.VarChar, new int?((int) byte.MaxValue), (object) PXActiveDirectorySyncMembershipProvider._applicationName));
      string str1 = (string) null;
      if (pxDataRecord2 == null)
      {
        isNewUser = true;
        PXActiveDirectorySyncMembershipProvider.CheckAndRenameDeletedADUser(user.Name.DomainLogin, user.SID);
        guid = Guid.NewGuid();
      }
      else
      {
        try
        {
          guid = pxDataRecord2.GetGuid(0).Value;
          str1 = pxDataRecord2.GetString(1);
        }
        finally
        {
          ((IDisposable) pxDataRecord2).Dispose();
        }
      }
      bool isScoped = PXTransactionScope.IsScoped;
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        bool flag1 = false;
        try
        {
          PXDatabase.Insert<Users>((PXDataFieldAssign) new PXDataFieldAssign<Users.pKID>(PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) guid), (PXDataFieldAssign) new PXDataFieldAssign<Users.username>(PXDbType.NVarChar, new int?(64 /*0x40*/), (object) user.Name.DomainLogin), (PXDataFieldAssign) new PXDataFieldAssign<Users.firstName>(PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) a1), (PXDataFieldAssign) new PXDataFieldAssign<Users.lastName>(PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) a2), (PXDataFieldAssign) new PXDataFieldAssign<Users.email>(PXDbType.VarChar, new int?(128 /*0x80*/), (object) user.Email), (PXDataFieldAssign) new PXDataFieldAssign<Users.comment>(PXDbType.VarChar, new int?((int) byte.MaxValue), (object) user.Comment), (PXDataFieldAssign) new PXDataFieldAssign<Users.applicationName>(PXDbType.VarChar, new int?((int) byte.MaxValue), (object) PXActiveDirectorySyncMembershipProvider._applicationName), (PXDataFieldAssign) new PXDataFieldAssign<Users.passwordChangeable>(PXDbType.Bit, new int?(1), (object) false), (PXDataFieldAssign) new PXDataFieldAssign<Users.passwordChangeOnNextLogin>(PXDbType.Bit, new int?(1), (object) false), (PXDataFieldAssign) new PXDataFieldAssign<Users.passwordNeverExpires>(PXDbType.Bit, new int?(1), (object) true), (PXDataFieldAssign) new PXDataFieldAssign<Users.allowPasswordRecovery>(PXDbType.Bit, new int?(1), (object) false), (PXDataFieldAssign) new PXDataFieldAssign<Users.guest>(PXDbType.Bit, new int?(1), (object) false), (PXDataFieldAssign) new PXDataFieldAssign<Users.isApproved>(PXDbType.Bit, new int?(1), (object) true), (PXDataFieldAssign) new PXDataFieldAssign<Users.isOnLine>(PXDbType.Bit, new int?(1), (object) false), (PXDataFieldAssign) new PXDataFieldAssign<Users.isAssigned>(PXDbType.Bit, new int?(1), (object) false), (PXDataFieldAssign) new PXDataFieldAssign<Users.overrideADRoles>(PXDbType.Bit, new int?(1), (object) false), (PXDataFieldAssign) new PXDataFieldAssign<Users.isHidden>(PXDbType.Bit, new int?(1), (object) false), new PXDataFieldAssign("GuidForPasswordRecovery", PXDbType.VarChar, new int?(60), (object) Guid.Empty.ToString()), (PXDataFieldAssign) new PXDataFieldAssign<Users.source>(PXDbType.Int, (object) user.Source), (PXDataFieldAssign) new PXDataFieldAssign<Users.extRef>(PXDbType.NVarChar, new int?(512 /*0x0200*/), (object) user.SID), (PXDataFieldAssign) new PXDataFieldAssign<Users.fullName>(PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) (user.DisplayName ?? "")), (PXDataFieldAssign) new PXDataFieldAssign<Users.noteID>(PXDbType.UniqueIdentifier, (object) Guid.NewGuid()), PXDataFieldAssign.OperationSwitchAllowed);
        }
        catch (PXDbOperationSwitchRequiredException ex1)
        {
          List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>()
          {
            (PXDataFieldParam) new PXDataFieldAssign<Users.firstName>(PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) a1),
            (PXDataFieldParam) new PXDataFieldAssign<Users.lastName>(PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) a2),
            (PXDataFieldParam) new PXDataFieldAssign<Users.email>(PXDbType.VarChar, new int?(128 /*0x80*/), (object) user.Email),
            (PXDataFieldParam) new PXDataFieldAssign<Users.comment>(PXDbType.VarChar, new int?((int) byte.MaxValue), (object) user.Comment),
            (PXDataFieldParam) new PXDataFieldAssign<Users.passwordChangeable>(PXDbType.Bit, new int?(1), (object) false),
            (PXDataFieldParam) new PXDataFieldAssign<Users.passwordChangeOnNextLogin>(PXDbType.Bit, new int?(1), (object) false),
            (PXDataFieldParam) new PXDataFieldAssign<Users.passwordNeverExpires>(PXDbType.Bit, new int?(1), (object) true),
            (PXDataFieldParam) new PXDataFieldAssign<Users.allowPasswordRecovery>(PXDbType.Bit, new int?(1), (object) false),
            (PXDataFieldParam) new PXDataFieldAssign<Users.guest>(PXDbType.Bit, new int?(1), (object) false),
            (PXDataFieldParam) new PXDataFieldAssign<Users.isApproved>(PXDbType.Bit, new int?(1), (object) true),
            (PXDataFieldParam) new PXDataFieldAssign<Users.isOnLine>(PXDbType.Bit, new int?(1), (object) false),
            (PXDataFieldParam) new PXDataFieldAssign("GuidForPasswordRecovery", PXDbType.VarChar, new int?(60), (object) Guid.Empty.ToString()),
            (PXDataFieldParam) new PXDataFieldAssign<Users.source>(PXDbType.Int, (object) user.Source),
            (PXDataFieldParam) new PXDataFieldAssign<Users.fullName>(PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) (user.DisplayName ?? "")),
            (PXDataFieldParam) new PXDataFieldRestrict<Users.applicationName>(PXDbType.VarChar, new int?((int) byte.MaxValue), (object) PXActiveDirectorySyncMembershipProvider._applicationName)
          };
          string str2 = (string) null;
          bool flag2 = false;
          Guid? nullable = new Guid?();
          bool flag3 = false;
          using (new PXReadDeletedScope())
          {
            using (PXDataRecord pxDataRecord3 = PXDatabase.SelectSingle<Users>((PXDataField) new PXDataField<Users.extRef>(), new PXDataField("DeletedDatabaseRecord"), (PXDataField) new PXDataField<Users.pKID>(), (PXDataField) new PXDataFieldValue<Users.username>(PXDbType.NVarChar, new int?(64 /*0x40*/), (object) user.Name.DomainLogin), (PXDataField) new PXDataFieldValue<Users.applicationName>(PXDbType.VarChar, new int?((int) byte.MaxValue), (object) PXActiveDirectorySyncMembershipProvider._applicationName)))
            {
              if (pxDataRecord3 != null)
              {
                flag3 = true;
                str2 = pxDataRecord3.GetString(0);
                flag2 = pxDataRecord3.GetBoolean(1).GetValueOrDefault();
                nullable = new Guid?(pxDataRecord3.GetGuid(2).Value);
              }
            }
          }
          if (!flag3)
          {
            pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<Users.username>(PXDbType.NVarChar, new int?(64 /*0x40*/), (object) user.Name.DomainLogin));
            pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict<Users.extRef>(PXDbType.NVarChar, new int?(512 /*0x0200*/), (object) user.SID));
            PXTransactionScope.ClearSharedDelete();
          }
          else if (!Str.IsNullOrEmpty(str2))
          {
            pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<Users.username>(PXDbType.NVarChar, new int?(64 /*0x40*/), (object) user.Name.DomainLogin));
            pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict<Users.extRef>(PXDbType.NVarChar, new int?(512 /*0x0200*/), (object) user.SID));
          }
          else
          {
            if (!flag2)
              throw new PXException("A non-ADFS user with the same name already exists.");
            pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldAssign<Users.extRef>(PXDbType.NVarChar, new int?(512 /*0x0200*/), (object) user.SID));
            pxDataFieldParamList.Add((PXDataFieldParam) new PXDataFieldRestrict<Users.username>(PXDbType.NVarChar, new int?(64 /*0x40*/), (object) user.Name.DomainLogin));
            guid = nullable.Value;
          }
          try
          {
            PXDatabase.Update<Users>(pxDataFieldParamList.ToArray());
          }
          catch (PXDatabaseException ex2)
          {
            if (ex2.ErrorCode == PXDbExceptions.PrimaryKeyConstraintViolation && pxDataRecord2 == null)
            {
              PXDataField[] pxDataFieldArray = new PXDataField[4]
              {
                (PXDataField) new PXDataField<Users.pKID>(),
                (PXDataField) new PXDataField<Users.username>(),
                (PXDataField) new PXDataFieldValue<Users.extRef>(PXDbType.NVarChar, new int?(512 /*0x0200*/), (object) user.SID),
                (PXDataField) new PXDataFieldValue<Users.applicationName>(PXDbType.VarChar, new int?((int) byte.MaxValue), (object) PXActiveDirectorySyncMembershipProvider._applicationName)
              };
              using (pxDataRecord2 = PXDatabase.SelectSingle<Users>(pxDataFieldArray))
              {
                if (pxDataRecord2 != null)
                  flag1 = true;
                else
                  throw;
              }
            }
            else
              throw;
          }
        }
        if (!flag1)
        {
          if (pxDataRecord2 != null)
            PXDatabase.Update<UsersInRoles>((PXDataFieldParam) new PXDataFieldAssign<UsersInRoles.username>(PXDbType.NVarChar, new int?(64 /*0x40*/), (object) user.Name.DomainLogin), (PXDataFieldParam) new PXDataFieldRestrict<UsersInRoles.username>(PXDbType.NVarChar, new int?(64 /*0x40*/), (object) str1));
          transactionScope.Complete();
        }
        else if (isScoped)
          transactionScope.Complete();
      }
    }
    else
    {
      string b1;
      string b2;
      string b3;
      bool? boolean;
      try
      {
        guid = pxDataRecord1.GetGuid(0).Value;
        b1 = pxDataRecord1.GetString(1);
        b2 = pxDataRecord1.GetString(2);
        b3 = pxDataRecord1.GetString(3);
        boolean = pxDataRecord1.GetBoolean(4);
      }
      finally
      {
        ((IDisposable) pxDataRecord1).Dispose();
      }
      bool flag4 = !string.IsNullOrEmpty(user.Email) && !string.Equals(user.Email, b3);
      string str = flag4 ? user.Email : b3;
      bool? nullable = boolean;
      bool flag5 = true;
      if (!(nullable.GetValueOrDefault() == flag5 & nullable.HasValue) && ((!string.Equals(a1, b1) ? 1 : (!string.Equals(a2, b2) ? 1 : 0)) | (flag4 ? 1 : 0)) != 0)
      {
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          try
          {
            PXDatabase.Update<Users>((PXDataFieldParam) new PXDataFieldAssign<Users.firstName>(PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) a1), (PXDataFieldParam) new PXDataFieldAssign<Users.lastName>(PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) a2), (PXDataFieldParam) new PXDataFieldAssign<Users.email>(PXDbType.VarChar, new int?(128 /*0x80*/), (object) str), (PXDataFieldParam) new PXDataFieldAssign<Users.comment>(PXDbType.VarChar, new int?((int) byte.MaxValue), (object) user.Comment), (PXDataFieldParam) new PXDataFieldAssign<Users.passwordChangeable>(PXDbType.Bit, new int?(1), (object) false), (PXDataFieldParam) new PXDataFieldAssign<Users.passwordChangeOnNextLogin>(PXDbType.Bit, new int?(1), (object) false), (PXDataFieldParam) new PXDataFieldAssign<Users.passwordNeverExpires>(PXDbType.Bit, new int?(1), (object) true), (PXDataFieldParam) new PXDataFieldAssign<Users.allowPasswordRecovery>(PXDbType.Bit, new int?(1), (object) false), (PXDataFieldParam) new PXDataFieldAssign<Users.guest>(PXDbType.Bit, new int?(1), (object) false), (PXDataFieldParam) new PXDataFieldAssign<Users.isApproved>(PXDbType.Bit, new int?(1), (object) true), (PXDataFieldParam) new PXDataFieldAssign<Users.isOnLine>(PXDbType.Bit, new int?(1), (object) false), (PXDataFieldParam) new PXDataFieldAssign("GuidForPasswordRecovery", PXDbType.VarChar, new int?(60), (object) Guid.Empty.ToString()), (PXDataFieldParam) new PXDataFieldAssign<Users.source>(PXDbType.Int, (object) user.Source), (PXDataFieldParam) new PXDataFieldAssign<Users.fullName>(PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) (user.DisplayName ?? "")), (PXDataFieldParam) new PXDataFieldRestrict<Users.applicationName>(PXDbType.VarChar, new int?((int) byte.MaxValue), (object) PXActiveDirectorySyncMembershipProvider._applicationName), (PXDataFieldParam) new PXDataFieldRestrict<Users.username>(PXDbType.NVarChar, new int?(64 /*0x40*/), (object) user.Name.DomainLogin), (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed);
          }
          catch (PXDbOperationSwitchRequiredException ex)
          {
            PXDatabase.Insert<Users>((PXDataFieldAssign) new PXDataFieldAssign<Users.pKID>(PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) guid), (PXDataFieldAssign) new PXDataFieldAssign<Users.username>(PXDbType.NVarChar, new int?(64 /*0x40*/), (object) user.Name.DomainLogin), (PXDataFieldAssign) new PXDataFieldAssign<Users.firstName>(PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) a1), (PXDataFieldAssign) new PXDataFieldAssign<Users.lastName>(PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) a2), (PXDataFieldAssign) new PXDataFieldAssign<Users.email>(PXDbType.VarChar, new int?(128 /*0x80*/), (object) str), (PXDataFieldAssign) new PXDataFieldAssign<Users.comment>(PXDbType.VarChar, new int?((int) byte.MaxValue), (object) user.Comment), (PXDataFieldAssign) new PXDataFieldAssign<Users.applicationName>(PXDbType.VarChar, new int?((int) byte.MaxValue), (object) PXActiveDirectorySyncMembershipProvider._applicationName), (PXDataFieldAssign) new PXDataFieldAssign<Users.passwordChangeable>(PXDbType.Bit, new int?(1), (object) false), (PXDataFieldAssign) new PXDataFieldAssign<Users.passwordChangeOnNextLogin>(PXDbType.Bit, new int?(1), (object) false), (PXDataFieldAssign) new PXDataFieldAssign<Users.passwordNeverExpires>(PXDbType.Bit, new int?(1), (object) true), (PXDataFieldAssign) new PXDataFieldAssign<Users.allowPasswordRecovery>(PXDbType.Bit, new int?(1), (object) false), (PXDataFieldAssign) new PXDataFieldAssign<Users.guest>(PXDbType.Bit, new int?(1), (object) false), (PXDataFieldAssign) new PXDataFieldAssign<Users.isApproved>(PXDbType.Bit, new int?(1), (object) true), (PXDataFieldAssign) new PXDataFieldAssign<Users.isOnLine>(PXDbType.Bit, new int?(1), (object) false), (PXDataFieldAssign) new PXDataFieldAssign<Users.isAssigned>(PXDbType.Bit, new int?(1), (object) false), (PXDataFieldAssign) new PXDataFieldAssign<Users.overrideADRoles>(PXDbType.Bit, new int?(1), (object) false), (PXDataFieldAssign) new PXDataFieldAssign<Users.isHidden>(PXDbType.Bit, new int?(1), (object) false), new PXDataFieldAssign("GuidForPasswordRecovery", PXDbType.VarChar, new int?(60), (object) Guid.Empty.ToString()), (PXDataFieldAssign) new PXDataFieldAssign<Users.source>(PXDbType.Int, (object) user.Source), (PXDataFieldAssign) new PXDataFieldAssign<Users.extRef>(PXDbType.NVarChar, new int?(512 /*0x0200*/), (object) user.SID), (PXDataFieldAssign) new PXDataFieldAssign<Users.fullName>(PXDbType.NVarChar, new int?((int) byte.MaxValue), (object) (user.DisplayName ?? "")), (PXDataFieldAssign) new PXDataFieldAssign<Users.noteID>(PXDbType.UniqueIdentifier, (object) Guid.NewGuid()));
          }
          transactionScope.Complete();
        }
      }
    }
    return guid;
  }

  public bool ValidateUserPassword(string userName, string password)
  {
    return this.ValidateUserPassword(userName, password, false, false);
  }

  protected internal override bool ValidateUserPassword(
    string userName,
    string password,
    bool onlyAllowed,
    bool skipForbidWithPasswordCheck = false)
  {
    return this.ValidateUserPassword(userName, password, onlyAllowed, out string _);
  }

  protected internal override bool ValidateUserPassword(
    string userName,
    string password,
    bool onlyAllowed,
    bool skipForbidWithPasswordCheck,
    out string providerUsername)
  {
    bool flag = true;
    providerUsername = userName;
    if (this._dbProvider is PXBaseMembershipProvider)
      flag = ((PXBaseMembershipProvider) this._dbProvider).ValidateUserPassword(userName, password, onlyAllowed, skipForbidWithPasswordCheck);
    if (!flag && this._adProvider.ValidateUser(userName, password))
    {
      flag = true;
      PX.Data.Access.ActiveDirectory.User userByLogin = this._adProvider.GetUserByLogin(userName);
      if (userByLogin == null)
        return false;
      providerUsername = userByLogin.Name.DomainLogin;
    }
    return flag;
  }

  protected internal override void UpdateUserPassword(string username, string password)
  {
    if (!(this._dbProvider is PXBaseMembershipProvider))
      return;
    ((PXBaseMembershipProvider) this._dbProvider).UpdateUserPassword(username, password);
  }
}
