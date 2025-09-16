// Decompiled with JetBrains decompiler
// Type: PX.Data.CurrentUserInformationProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Builder;
using Autofac.Core;
using PX.Common;
using PX.Data.Access.ActiveDirectory;
using PX.Security;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Security;

#nullable enable
namespace PX.Data;

internal sealed class CurrentUserInformationProvider : ICurrentUserInformationProvider
{
  private const 
  #nullable disable
  string _MYPROFILEMAIN_GRAPH_TYPE = "PX.SM.MyProfileMaint";
  private readonly IUserManagementService _userManagementService;
  private readonly PX.Data.Internal.IUserOrganizationService _userOrganizationService;
  private readonly IActiveDirectoryProvider _activeDirectoryProvider;
  private readonly ILoginAsUser _loginAsUser;
  private readonly ILegacyCompanyService _legacyCompanyService;

  public CurrentUserInformationProvider(
    IUserManagementService userManagementService,
    PX.Data.Internal.IUserOrganizationService userOrganizationService,
    IActiveDirectoryProvider activeDirectoryProvider,
    ILoginAsUser loginAsUser,
    ILegacyCompanyService legacyCompanyService)
  {
    this._userManagementService = userManagementService;
    this._userOrganizationService = userOrganizationService;
    this._activeDirectoryProvider = activeDirectoryProvider;
    this._loginAsUser = loginAsUser;
    this._legacyCompanyService = legacyCompanyService;
  }

  private string GetCurrentUserName() => PXContext.PXIdentity.IdentityName;

  public Guid? GetUserId()
  {
    MembershipUser user = this._userManagementService.GetUser(this.GetCurrentUserName());
    return user == null ? new Guid?() : user.GetID();
  }

  public Guid GetUserIdOrDefault()
  {
    MembershipUser user = this._userManagementService.GetUser(this.GetCurrentUserName());
    return CurrentUserInformationProvider.UserIDOrDefault(user != null ? user.GetID() : new Guid?());
  }

  public Guid GetUserIdAccountingForImpersonationOrDefault()
  {
    string username = this.GetCurrentUserName();
    if (string.IsNullOrEmpty(username))
      return CurrentUserInformationProvider.UserIDOrDefault(new Guid?());
    string fromCurrentContext = this._loginAsUser.TryGetLoggedAsUserNameFromCurrentContext();
    if (fromCurrentContext != null)
      username = fromCurrentContext;
    return this._userManagementService.GetUser(username).GetIDOrDefault();
  }

  public string GetUserName() => this.GetUserInfo()?.Name;

  public string GetUserDisplayName() => this.GetUserInfo()?.DisplayName;

  public string GetEmail() => this._userManagementService.GetUser(this.GetCurrentUserName())?.Email;

  public IEnumerable<BranchInfo> GetActiveBranches()
  {
    return this._userOrganizationService.GetBranches(this.GetUserName(), this.IsGuest(), true);
  }

  public IEnumerable<BranchInfo> GetAllBranches()
  {
    return this._userOrganizationService.GetBranches(this.GetUserName(), this.IsGuest(), false);
  }

  public IEnumerable<int> GetActiveBranchesWithParents()
  {
    return this._userOrganizationService.GetBranchesWithParents(this.GetUserName(), this.IsGuest(), true);
  }

  public IEnumerable<PXAccess.MasterCollection.Organization> GetOrganizations(
    bool onlyActive,
    bool skipGroups)
  {
    return this._userOrganizationService.GetOrganizations(this.GetUserName(), this.IsGuest(), onlyActive, skipGroups);
  }

  public string GetBranchCD()
  {
    int? id = PXContext.GetBranchID();
    if (!id.HasValue)
      return (string) null;
    return this._userOrganizationService.GetBranches(this.GetUserName(), this.IsGuest(), false).FirstOrDefault<BranchInfo>((Func<BranchInfo, bool>) (info => id.Equals((object) info.Id)))?.Cd;
  }

  public string[] GetLicensedAccessibleCompanies()
  {
    string[] licensedCompanies = PXDatabase.AvailableCompanies;
    return ((IEnumerable<string>) this.GetAllAccessibleCompanies()).Where<string>((Func<string, bool>) (c => ((IEnumerable<string>) licensedCompanies).Contains<string>(c, (IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase))).ToArray<string>();
  }

  public string[] GetAllAccessibleCompanies()
  {
    Dictionary<string, string[]> slot = PXDatabase.GetSlot<Dictionary<string, string[]>>("AccessibleCompanies", typeof (Users), typeof (UsersInRoles), typeof (PreferencesSecurity));
    if (slot == null)
      return new string[0];
    lock (((ICollection) slot).SyncRoot)
    {
      string userName = this.GetUserName();
      string companyName = PXAccess.GetCompanyName();
      string key = $"{userName}@{companyName}";
      string[] accessibleCompanies;
      if (slot.TryGetValue(key, out accessibleCompanies))
        return accessibleCompanies;
      List<string> stringList = new List<string>();
      string str = (string) null;
      bool isADUser = false;
      bool isClaimUser = false;
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Users>(new PXDataField("Password"), new PXDataField("Source"), (PXDataField) new PXDataFieldValue("Username", PXDbType.NVarChar, new int?(64 /*0x40*/), (object) userName)))
      {
        if (pxDataRecord != null)
        {
          str = pxDataRecord.GetString(0);
          int? int32_1 = pxDataRecord.GetInt32(1);
          int num1 = 1;
          isADUser = int32_1.GetValueOrDefault() == num1 & int32_1.HasValue;
          int? int32_2 = pxDataRecord.GetInt32(1);
          int num2 = 2;
          isClaimUser = int32_2.GetValueOrDefault() == num2 & int32_2.HasValue;
        }
      }
      if (!string.IsNullOrEmpty(str) | isADUser | isClaimUser)
      {
        foreach (string company in PXDatabase.Companies)
        {
          bool flag1 = false;
          bool flag2 = false;
          PXDatabase.ResetCredentials();
          using (new PXLoginScope($"{userName}@{company}", Array.Empty<string>()))
          {
            using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Users>(new PXDataField("PasswordChangeOnNextLogin"), (PXDataField) new PXDataFieldValue("Username", PXDbType.NVarChar, new int?(64 /*0x40*/), (object) userName)))
            {
              if (pxDataRecord != null)
              {
                bool? boolean = pxDataRecord.GetBoolean(0);
                bool flag3 = true;
                flag1 = !(boolean.GetValueOrDefault() == flag3 & boolean.HasValue);
              }
              else if (flag1 = isADUser | isClaimUser)
                flag2 = string.IsNullOrEmpty(str);
            }
            if (flag1)
              flag1 = ((IUserValidationService) this._userManagementService).ValidateUserPasswordForTenantSwitching(userName, str, ref userName) | flag2;
            if (flag1)
              flag1 = this._activeDirectoryProvider.CheckUserRoles(userName, isADUser, isClaimUser);
          }
          if (flag1 || string.Equals(company, companyName, StringComparison.InvariantCultureIgnoreCase))
            stringList.Add(company);
        }
      }
      string[] array;
      slot[key] = array = stringList.ToArray();
      PXDatabase.ResetCredentials();
      return array;
    }
  }

  public bool IsGuest()
  {
    CurrentUserInformationProvider.UserInfo userInfo = this.GetUserInfo();
    return userInfo != null && userInfo.Guest;
  }

  public bool IsActiveDirectoryUser()
  {
    int? source = this.GetUserInfo()?.Source;
    int? nullable1 = source;
    int num1 = 1;
    if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
      return true;
    int? nullable2 = source;
    int num2 = 2;
    return nullable2.GetValueOrDefault() == num2 & nullable2.HasValue;
  }

  public bool IsClaimUser()
  {
    CurrentUserInformationProvider.UserInfo userInfo = this.GetUserInfo();
    return userInfo != null && userInfo.Source == 2;
  }

  internal static Guid UserIDOrDefault(Guid? userId) => userId.GetValueOrDefault();

  private CurrentUserInformationProvider.UserInfo GetUserInfo()
  {
    string name = PXContext.PXIdentity.AuthUser?.Identity?.Name;
    if (name == null)
      return (CurrentUserInformationProvider.UserInfo) null;
    string username = this._legacyCompanyService.ExtractUsername(name);
    if (string.IsNullOrEmpty(username))
      return (CurrentUserInformationProvider.UserInfo) null;
    CurrentUserInformationProvider.UserInfo slot = PXContext.GetSlot<CurrentUserInformationProvider.UserInfo>(username);
    if (slot != null)
      return slot;
    MembershipUser user = this._userManagementService.GetUser(name);
    if (user == null)
      return new CurrentUserInformationProvider.UserInfo(username, ((MembershipUser) null).IsGuest());
    CurrentUserInformationProvider.UserInfo userInfo = user is MembershipUserExt membershipUserExt ? new CurrentUserInformationProvider.UserInfo(membershipUserExt, MembershipUserExtensions.IsGuest(membershipUserExt)) : new CurrentUserInformationProvider.UserInfo(user.UserName, user.IsGuest());
    PXContext.SetSlot<CurrentUserInformationProvider.UserInfo>(username, userInfo);
    return userInfo;
  }

  public PXTimeZoneInfo GetTimeZone()
  {
    string userName = this.GetUserName();
    if (userName == null)
      return (PXTimeZoneInfo) null;
    System.Type type1 = PXBuildManager.GetType("PX.SM.MyProfileMaint", false);
    if ((object) type1 == null)
      type1 = typeof (SMAccessPersonalMaint);
    System.Type type2 = type1;
    object instance = Activator.CreateInstance(type2);
    MethodInfo method = type2.GetMethod("GetUserTimeZoneId", new System.Type[2]
    {
      typeof (string),
      typeof (string)
    });
    if (method == (MethodInfo) null)
      throw new PXException("The {0} type does not have the {1} method.", new object[2]
      {
        (object) type2.FullName,
        (object) "GetUserTimeZoneId"
      });
    string companyName = PXAccess.GetCompanyName();
    string id = method.Invoke(instance, new object[2]
    {
      (object) userName,
      (object) companyName
    }) as string;
    return string.IsNullOrEmpty(id) ? (PXTimeZoneInfo) null : PXTimeZoneInfo.FindSystemTimeZoneById(id);
  }

  /// <remarks>
  /// DO NOT use this until you know exactly what you're doing. This is intended for very,
  /// VERY special use cases, and yours probably isn't one of those.
  /// </remarks>
  internal static ICurrentUserInformationProvider Instance { get; private set; } = (ICurrentUserInformationProvider) new CurrentUserInformationProvider.ApplicationStartPlaceholder();

  internal static void Register(ContainerBuilder builder)
  {
    RegistrationExtensions.AutoActivate<CurrentUserInformationProvider, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<CurrentUserInformationProvider>(builder).As<ICurrentUserInformationProvider>().OnActivated((System.Action<IActivatedEventArgs<CurrentUserInformationProvider>>) (args => CurrentUserInformationProvider.Instance = (ICurrentUserInformationProvider) args.Instance))).SingleInstance();
  }

  private sealed class UserInfo
  {
    public UserInfo(MembershipUserExt mExt, bool isGuest)
    {
      this.Name = mExt.UserName;
      this.DisplayName = mExt.DisplayName;
      this.Source = mExt.Source;
      this.Guest = isGuest;
    }

    public UserInfo(string name, bool isGuest)
    {
      this.Name = name;
      this.DisplayName = name;
      this.Source = 0;
      this.Guest = isGuest;
    }

    public string Name { get; }

    public string DisplayName { get; }

    public int Source { get; }

    public bool Guest { get; }
  }

  private class ApplicationStartPlaceholder : ICurrentUserInformationProvider
  {
    private static readonly Guid DefaultUserId = CurrentUserInformationProvider.UserIDOrDefault(new Guid?());

    private static T AssertStateAndReturn<T>(T value)
    {
      if (PXContext.PXIdentity.User != null && PXHostingEnvironment.IsHosted)
        throw new InvalidOperationException("ICurrentUserInformationProvider should have been replaced by real implementation by now");
      return value;
    }

    Guid? ICurrentUserInformationProvider.GetUserId()
    {
      return CurrentUserInformationProvider.ApplicationStartPlaceholder.AssertStateAndReturn<Guid?>(new Guid?());
    }

    Guid ICurrentUserInformationProvider.GetUserIdOrDefault()
    {
      return CurrentUserInformationProvider.ApplicationStartPlaceholder.AssertStateAndReturn<Guid>(CurrentUserInformationProvider.ApplicationStartPlaceholder.DefaultUserId);
    }

    Guid ICurrentUserInformationProvider.GetUserIdAccountingForImpersonationOrDefault()
    {
      return CurrentUserInformationProvider.ApplicationStartPlaceholder.AssertStateAndReturn<Guid>(CurrentUserInformationProvider.ApplicationStartPlaceholder.DefaultUserId);
    }

    string ICurrentUserInformationProvider.GetUserName()
    {
      return CurrentUserInformationProvider.ApplicationStartPlaceholder.AssertStateAndReturn<string>((string) null);
    }

    string ICurrentUserInformationProvider.GetUserDisplayName()
    {
      return CurrentUserInformationProvider.ApplicationStartPlaceholder.AssertStateAndReturn<string>((string) null);
    }

    string ICurrentUserInformationProvider.GetEmail()
    {
      return CurrentUserInformationProvider.ApplicationStartPlaceholder.AssertStateAndReturn<string>((string) null);
    }

    IEnumerable<BranchInfo> ICurrentUserInformationProvider.GetActiveBranches()
    {
      return (IEnumerable<BranchInfo>) CurrentUserInformationProvider.ApplicationStartPlaceholder.AssertStateAndReturn<BranchInfo[]>(new BranchInfo[0]);
    }

    IEnumerable<BranchInfo> ICurrentUserInformationProvider.GetAllBranches()
    {
      return (IEnumerable<BranchInfo>) CurrentUserInformationProvider.ApplicationStartPlaceholder.AssertStateAndReturn<BranchInfo[]>(new BranchInfo[0]);
    }

    IEnumerable<int> ICurrentUserInformationProvider.GetActiveBranchesWithParents()
    {
      return (IEnumerable<int>) CurrentUserInformationProvider.ApplicationStartPlaceholder.AssertStateAndReturn<int[]>(new int[0]);
    }

    IEnumerable<PXAccess.MasterCollection.Organization> ICurrentUserInformationProvider.GetOrganizations(
      bool onlyActive,
      bool skipGroups)
    {
      return (IEnumerable<PXAccess.MasterCollection.Organization>) CurrentUserInformationProvider.ApplicationStartPlaceholder.AssertStateAndReturn<PXAccess.MasterCollection.Organization[]>(new PXAccess.MasterCollection.Organization[0]);
    }

    string ICurrentUserInformationProvider.GetBranchCD()
    {
      return CurrentUserInformationProvider.ApplicationStartPlaceholder.AssertStateAndReturn<string>((string) null);
    }

    string[] ICurrentUserInformationProvider.GetLicensedAccessibleCompanies()
    {
      return CurrentUserInformationProvider.ApplicationStartPlaceholder.AssertStateAndReturn<string[]>(new string[0]);
    }

    string[] ICurrentUserInformationProvider.GetAllAccessibleCompanies()
    {
      return CurrentUserInformationProvider.ApplicationStartPlaceholder.AssertStateAndReturn<string[]>(new string[0]);
    }

    bool ICurrentUserInformationProvider.IsActiveDirectoryUser()
    {
      return CurrentUserInformationProvider.ApplicationStartPlaceholder.AssertStateAndReturn<bool>(false);
    }

    bool ICurrentUserInformationProvider.IsClaimUser()
    {
      return CurrentUserInformationProvider.ApplicationStartPlaceholder.AssertStateAndReturn<bool>(false);
    }

    bool ICurrentUserInformationProvider.IsGuest()
    {
      return CurrentUserInformationProvider.ApplicationStartPlaceholder.AssertStateAndReturn<bool>(false);
    }

    PXTimeZoneInfo ICurrentUserInformationProvider.GetTimeZone()
    {
      return CurrentUserInformationProvider.ApplicationStartPlaceholder.AssertStateAndReturn<PXTimeZoneInfo>(PXTimeZoneInfo.Invariant);
    }
  }
}
