// Decompiled with JetBrains decompiler
// Type: PX.Data.PXAccess
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PX.Common;
using PX.Data.BQL;
using PX.Data.Localization;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.Update;
using PX.DbServices.QueryObjectModel;
using PX.Hosting;
using PX.SM;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Web;
using System.Web.Compilation;
using System.Web.Hosting;

#nullable enable
namespace PX.Data;

[Serializable]
public static class PXAccess
{
  private static 
  #nullable disable
  IConfiguration _configuration;
  private const string _MASTER_COLLECTION_KEY = "_MASTER_COLLECTION_KEY";
  private const string _USER_CONTACTS_COLLECTION_KEY = "_USER_CONTACTS_COLLECTION_KEY";
  private static bool s_Initialized;
  private static bool s_Skipped;
  private static Exception s_InitializeException;
  private static object s_lock = new object();
  private static PXAccessProvider s_Provider;
  public static byte[] InstallationID;

  private static ICurrentUserInformationProvider CurrentUserInformationProvider
  {
    get => PX.Data.CurrentUserInformationProvider.Instance;
  }

  public static IEnumerable<PXAccess.FeatureInfo> GetAllFeaturesInfo()
  {
    if (PXAccess.Provider.AllFeatures != null)
    {
      PXGraph graph = new PXGraph();
      System.Type featureType = (System.Type) null;
      foreach (string allFeature in PXAccess.Provider.AllFeatures)
      {
        string[] strArray = allFeature.Split('+');
        if (featureType == (System.Type) null || featureType.FullName != strArray[0])
          featureType = PXBuildManager.GetType(strArray[0], false);
        if (!(featureType == (System.Type) null) && graph.Caches[featureType].GetStateExt((object) null, strArray[1]) is PXFieldState stateExt)
          yield return new PXAccess.FeatureInfo()
          {
            ID = allFeature,
            FeatureSet = featureType,
            Name = strArray[1],
            Title = stateExt.DisplayName,
            Visible = stateExt.Visible
          };
      }
    }
  }

  public static bool FeatureInstalled<FeatureType>() where FeatureType : IBqlField
  {
    return PXAccess.FeatureInstalled(typeof (FeatureType).FullName);
  }

  public static bool FeatureInstalled(string feature)
  {
    return PXAccess.Provider != null && PXAccess.Provider.FeatureInstalled(feature);
  }

  public static bool LocalizationEnabled<TFeature>(string localizationCode) where TFeature : IBqlField
  {
    string localizationCodeForFeature = ServiceLocator.Current.GetInstance<ILocalizationFeaturesService>().GetLocalizationCodeForFeature(typeof (TFeature).FullName);
    return string.Equals(localizationCode, localizationCodeForFeature) && PXAccess.FeatureInstalled<TFeature>();
  }

  public static int Version
  {
    get => PXDatabase.Provider.Version;
    set => PXDatabase.Provider.Version = value;
  }

  public static bool FeatureSetInstalled<FeatureSetType>() where FeatureSetType : IBqlTable
  {
    return PXAccess.FeatureSetInstalled(typeof (FeatureSetType).FullName);
  }

  public static bool FeatureSetInstalled(string featureSet)
  {
    return PXAccess.Provider != null && PXAccess.Provider.FeatureSetInstalled(featureSet);
  }

  public static bool FeatureReadOnly(string feature)
  {
    return PXAccess.Provider != null && PXAccess.Provider.FeatureReadOnly(feature);
  }

  public static bool IsRoleEnabled(string role) => PXAccess.Provider.IsRoleEnabled(role);

  internal static string GetConnectionString(NameValueCollection config)
  {
    return PXAccess._configuration == null ? string.Empty : PXAccess._configuration.GetConnectionString(config);
  }

  internal static PXCache.FieldDefaultingDelegate GetDefaultingDelegate(System.Type cacheType)
  {
    return PXAccess.Provider != null ? PXAccess.Provider.GetDefaultingDelegate(cacheType) : (PXCache.FieldDefaultingDelegate) null;
  }

  public static bool IsSchedulesEnabled() => PXAccess.Provider.IsSchedulesEnabled();

  public static bool IsScreenApiEnabled(string screenId)
  {
    return PXAccess.Provider.IsScreenApiEnabled(screenId);
  }

  public static bool IsScreenMobileEnabled(string screenId)
  {
    return PXAccess.Provider.IsScreenMobileEnabled(screenId);
  }

  internal static bool IsScreenDisabled(string screenId)
  {
    return PXAccess.Provider.IsScreenDisabled(screenId);
  }

  internal static PXAccess.MasterCollection MasterBranches
  {
    get
    {
      PXAccess.MasterCollection slot1 = PXContext.GetSlot<PXAccess.MasterCollection>();
      if (slot1 != null)
        return slot1;
      PXAccess.MasterCollection slot2 = PXDatabase.GetSlot<PXAccess.MasterCollection>("_MASTER_COLLECTION_KEY", PXAccess.MasterCollection.DBTables);
      PXContext.SetSlot<PXAccess.MasterCollection>(slot2);
      return slot2;
    }
  }

  public static void ResetOrganizationBranchSlot()
  {
    PXDatabase.ResetSlot<PXAccess.MasterCollection>("_MASTER_COLLECTION_KEY", PXAccess.MasterCollection.DBTables);
  }

  internal static PXAccess.UserContactsCollection UserContacts
  {
    get
    {
      return PXDatabase.GetSlot<PXAccess.UserContactsCollection>("_USER_CONTACTS_COLLECTION_KEY", PXAccess.UserContactsCollection.DBTables);
    }
  }

  public static PXAccessProvider Provider
  {
    get
    {
      PXAccess.Initialize();
      return PXAccess.s_Provider;
    }
  }

  public static bool Initialized => PXAccess.s_Initialized;

  private static void Initialize()
  {
    if (PXAccess.s_Initialized)
    {
      if (PXAccess.s_InitializeException != null)
        throw PXAccess.s_InitializeException;
    }
    else
    {
      if (PXAccess.s_InitializeException != null)
        throw PXAccess.s_InitializeException;
      lock (PXAccess.s_lock)
      {
        if (PXAccess.s_Initialized)
        {
          if (PXAccess.s_InitializeException != null)
            throw PXAccess.s_InitializeException;
        }
        else
        {
          try
          {
            if (HostingEnvironment.IsHosted)
              throw new PXProviderException("Should have been explicitly initialized");
            try
            {
              PXAccess.s_Provider = ServiceLocator.Current.GetInstance<PXAccessProvider>();
            }
            catch
            {
            }
            if (PXAccess.s_Provider == null)
            {
              PXAccess.s_Skipped = true;
              if (!HostingEnvironment.IsHosted)
                PXAccess.s_Provider = (PXAccessProvider) new PXAccessDummyProvider();
            }
          }
          catch (Exception ex)
          {
            PXAccess.s_InitializeException = ex;
            throw;
          }
          PXAccess.s_Initialized = true;
        }
      }
    }
  }

  private static ConfigurationSectionWithProviders GetConfigurationSection(
    IConfiguration configuration)
  {
    return new ConfigurationSectionWithProviders(configuration.GetSection("pxaccess"));
  }

  internal static PXAccessProvider Initialize(IConfiguration configuration)
  {
    ConfigurationSectionWithProviders section = configuration != null ? PXAccess.GetConfigurationSection(configuration) : throw new PXProviderException("Configuration not specified");
    if (!ConfigurationExtensions.Exists((IConfigurationSection) section))
      throw new PXProviderException("Configuration not specified");
    PXAccess._configuration = configuration;
    PXAccessProvider provider;
    try
    {
      provider = PXAccess.CreateProvider(section);
    }
    catch (Exception ex)
    {
      PXAccess.s_InitializeException = ex;
      throw;
    }
    return PXAccess.Initialize(provider);
  }

  internal static PXAccessProvider Initialize(PXAccessProvider provider)
  {
    if (provider == null)
      throw new ArgumentNullException(nameof (provider));
    if (PXAccess.s_Initialized)
      throw new PXProviderException("Double initialization is not allowed");
    if (PXAccess.s_InitializeException != null)
      throw new PXProviderException("Double initialization is not allowed", PXAccess.s_InitializeException);
    PXAccess.s_Provider = provider;
    PXAccess.s_Initialized = true;
    return PXAccess.s_Provider;
  }

  private static PXAccessProvider CreateProvider(ConfigurationSectionWithProviders section)
  {
    return ConfigurationSectionExtensions.InstantiateProvider<PXAccessProvider>((IConfigurationSection) (section.GetDefaultProvider() ?? throw new PXProviderException("A default access provider is not specified.")));
  }

  static PXAccess()
  {
    PXAccess.s_Initialized = false;
    PXAccess.s_InitializeException = (Exception) null;
  }

  [Obsolete("Use ICurrentUserInformationProvider.GetUserIdOrDefault or ICurrentUserInformationProvider.GetUserId")]
  public static Guid GetUserID() => PXAccess.CurrentUserInformationProvider.GetUserIdOrDefault();

  [Obsolete("Use ICurrentUserInformationProvider.GetUserIdAccountingForImpersonationOrDefault")]
  public static Guid GetTrueUserID()
  {
    return PXAccess.CurrentUserInformationProvider.GetUserIdAccountingForImpersonationOrDefault();
  }

  public static string GetCompanyName()
  {
    IPrincipal user = PXAccess.GetUser();
    if (user == null)
      return (string) null;
    string company;
    LegacyCompanyService.ParseLogin(user.Identity.Name, out string _, out company, out string _);
    return company;
  }

  public static int? GetBranchID() => PXContext.GetBranchID();

  public static PXAccess.MasterCollection.Branch GetBranch(int? branchID)
  {
    return PXAccess.MasterBranches.GetBranch(branchID);
  }

  public static string GetBranchCD(int? branchID) => PXAccess.MasterBranches.GetBranchCD(branchID);

  public static HashSet<int> GetBranchIDsByBAccountID(int? orgBAccountID, bool excludeDeleted = true)
  {
    HashSet<int> branchIdsByBaccountId = new HashSet<int>();
    if (!orgBAccountID.HasValue)
      return branchIdsByBaccountId;
    PXAccess.MasterCollection.Branch branchByBaccountId = PXAccess.GetBranchByBAccountID(orgBAccountID);
    if (branchByBaccountId != null && (excludeDeleted && !branchByBaccountId.DeletedDatabaseRecord || !excludeDeleted))
    {
      branchIdsByBaccountId.Add(branchByBaccountId.BranchID);
    }
    else
    {
      PXAccess.MasterCollection.Organization organizationByBaccountId = PXAccess.GetOrganizationByBAccountID(orgBAccountID);
      if (organizationByBaccountId != null)
      {
        bool? deletedDatabaseRecord = organizationByBaccountId.DeletedDatabaseRecord;
        bool flag = true;
        if (!(deletedDatabaseRecord.GetValueOrDefault() == flag & deletedDatabaseRecord.HasValue))
        {
          foreach (PXAccess.MasterCollection.Branch branch in organizationByBaccountId.ChildBranches.Where<PXAccess.MasterCollection.Branch>((Func<PXAccess.MasterCollection.Branch, bool>) (x => excludeDeleted && !x.DeletedDatabaseRecord || !excludeDeleted)))
            branchIdsByBaccountId.Add(branch.BranchID);
        }
      }
    }
    return branchIdsByBaccountId;
  }

  public static PXAccess.MasterCollection.Organization GetOrganizationByBAccountID(int? bAccountID)
  {
    return PXAccess.MasterBranches.OrganizationsByCD.Values.Where<PXAccess.MasterCollection.Organization>((Func<PXAccess.MasterCollection.Organization, bool>) (organization =>
    {
      int? baccountId = organization.BAccountID;
      int? nullable = bAccountID;
      return baccountId.GetValueOrDefault() == nullable.GetValueOrDefault() & baccountId.HasValue == nullable.HasValue;
    })).FirstOrDefault<PXAccess.MasterCollection.Organization>();
  }

  public static PXAccess.MasterCollection.Organization GetOrganizationByID(int? organizationID)
  {
    return PXAccess.MasterBranches.OrganizationsByCD.Values.Where<PXAccess.MasterCollection.Organization>((Func<PXAccess.MasterCollection.Organization, bool>) (organization =>
    {
      int? organizationId = organization.OrganizationID;
      int? nullable = organizationID;
      return organizationId.GetValueOrDefault() == nullable.GetValueOrDefault() & organizationId.HasValue == nullable.HasValue;
    })).FirstOrDefault<PXAccess.MasterCollection.Organization>();
  }

  public static PXAccess.MasterCollection.Branch GetBranchByBAccountID(int? bAccountID)
  {
    return PXAccess.MasterBranches.AllBranchesByCD.Values.Where<PXAccess.MasterCollection.Branch>((Func<PXAccess.MasterCollection.Branch, bool>) (branch =>
    {
      int baccountId = branch.BAccountID;
      int? nullable = bAccountID;
      int valueOrDefault = nullable.GetValueOrDefault();
      return baccountId == valueOrDefault & nullable.HasValue;
    })).FirstOrDefault<PXAccess.MasterCollection.Branch>();
  }

  public static int? GetContactID()
  {
    return PXAccess.GetContactID(new Guid?(PXAccess.CurrentUserInformationProvider.GetUserIdOrDefault()));
  }

  public static int? GetBAccountID()
  {
    return PXAccess.UserContacts.GetBAccountID(new Guid?(PXAccess.CurrentUserInformationProvider.GetUserIdOrDefault()));
  }

  public static HashSet<int> GetBAccountIDTree()
  {
    return PXAccess.UserContacts.GetBAccountIDTree(new Guid?(PXAccess.CurrentUserInformationProvider.GetUserIdOrDefault()));
  }

  public static int? GetContactID(Guid? userID) => PXAccess.UserContacts.GetContactID(userID);

  public static Guid? GetUserID(int? contactID) => PXAccess.UserContacts.GetUserID(contactID);

  [Obsolete("Use ICurrentUserInformationProvider.GetUserName")]
  public static string GetUserName() => PXAccess.CurrentUserInformationProvider.GetUserName();

  public static string GetFullUserName()
  {
    string fullUserName = (string) null;
    IPrincipal user = PXAccess.GetUser();
    if (user != null)
    {
      fullUserName = user.Identity.Name;
      string branchCd = PXAccess.CurrentUserInformationProvider.GetBranchCD();
      if (!string.IsNullOrEmpty(branchCd))
        fullUserName = $"{fullUserName}:{branchCd}";
    }
    return fullUserName;
  }

  [Obsolete("Use ICurrentUserInformationProvider.GetUserDisplayName")]
  public static string GetUserDisplayName()
  {
    return PXAccess.CurrentUserInformationProvider.GetUserDisplayName();
  }

  public static bool BypassLicense => PXAccess.Provider.BypassLicense;

  public static bool IsUnlimitedUser() => PXAccess.Provider.IsUnlimitedUser();

  public static bool IsMultiDbMode => PXAccess.Provider.IsMultiDbMode;

  public static byte[] GetInstallationID() => PXAccess.Provider.InstallationID;

  public static PXLoginScope GetAdminLoginScope(string company = null)
  {
    return PXDatabase.Companies.Length == 0 ? new PXLoginScope("admin", Array.Empty<string>()) : new PXLoginScope("admin@" + (string.IsNullOrEmpty(company) ? PXDatabase.Companies[0] : company), Array.Empty<string>());
  }

  [Obsolete("Use ICurrentUserInformationProvider.GetLicensedAccessibleCompanies")]
  internal static string[] GetCompanies()
  {
    return PXAccess.CurrentUserInformationProvider.GetLicensedAccessibleCompanies();
  }

  [Obsolete("Use ICurrentUserInformationProvider.GetAllAccessibleCompanies")]
  internal static string[] GetCompaniesUnrestricted()
  {
    return PXAccess.CurrentUserInformationProvider.GetAllAccessibleCompanies();
  }

  [Obsolete("Use ICurrentUserInformationProvider.GetAllBranches")]
  public static int[] GetBranchIDs()
  {
    return PXAccess.CurrentUserInformationProvider.GetAllBranches().Select<BranchInfo, int>((Func<BranchInfo, int>) (b => b.Id)).Distinct<int>().ToArray<int>();
  }

  [Obsolete("Use ICurrentUserInformationProvider.GetActiveBranchesWithParents")]
  internal static IEnumerable<int> GetActiveBranchesWithParents()
  {
    return PXAccess.CurrentUserInformationProvider.GetActiveBranchesWithParents();
  }

  public static int[] GetChildBranchIDs(string organizationCD, bool restricted = true)
  {
    List<PXAccess.MasterCollection.Branch> branches;
    return !string.IsNullOrEmpty(organizationCD) && PXAccess.MasterBranches.TryGetOrganizationChildBranchIDs(organizationCD.TrimEnd(), out branches) ? PXAccess.GetAvailableBranches(branches, restricted) : new int[0];
  }

  public static int[] GetChildBranchIDs(int? organizationID, bool restricted = true)
  {
    List<PXAccess.MasterCollection.Branch> branches;
    return organizationID.HasValue && PXAccess.MasterBranches.TryGetOrganizationChildBranchIDs(organizationID.Value, out branches) ? PXAccess.GetAvailableBranches(branches, restricted) : new int[0];
  }

  public static int? GetParentOrganizationID(int? branchID)
  {
    return PXAccess.GetParentOrganization(branchID)?.OrganizationID;
  }

  public static PXAccess.Organization GetParentOrganization(int? branchID)
  {
    if (!branchID.HasValue)
      return (PXAccess.Organization) null;
    PXAccess.MasterCollection.Branch branch;
    return PXAccess.MasterBranches.AllBranchesByID.TryGetValue(branchID.Value, out branch) ? (PXAccess.Organization) branch.Organization : (PXAccess.Organization) null;
  }

  public static bool IsSameParentOrganization(int? branchA, int? branchB)
  {
    if (!branchA.HasValue || !branchB.HasValue)
      return false;
    int? parentOrganizationId1 = PXAccess.GetParentOrganizationID(branchA);
    int? parentOrganizationId2 = PXAccess.GetParentOrganizationID(branchB);
    return parentOrganizationId1.GetValueOrDefault() == parentOrganizationId2.GetValueOrDefault() & parentOrganizationId1.HasValue == parentOrganizationId2.HasValue;
  }

  public static int? GetBranchID(string branchCD) => PXAccess.MasterBranches.GetBranchID(branchCD);

  public static int[] GetBranchIDsByBAccount(int? bAccountID)
  {
    return !bAccountID.HasValue ? new int[0] : PXAccess.GetChildBranchIDs(PXAccess.CurrentUserInformationProvider.GetOrganizations().Where<PXAccess.MasterCollection.Organization>((Func<PXAccess.MasterCollection.Organization, bool>) (organization =>
    {
      int? baccountId = organization.BAccountID;
      int? nullable = bAccountID;
      return baccountId.GetValueOrDefault() == nullable.GetValueOrDefault() & baccountId.HasValue == nullable.HasValue;
    })).Select<PXAccess.MasterCollection.Organization, string>((Func<PXAccess.MasterCollection.Organization, string>) (organization => organization.OrganizationCD)).FirstOrDefault<string>());
  }

  public static string GetOrganizationCD(int? organizationID)
  {
    return PXAccess.MasterBranches.GetOrganizationCD(organizationID);
  }

  public static int? GetOrganizationID(string organizationCD)
  {
    return PXAccess.MasterBranches.GetOrganizationID(organizationCD);
  }

  public static int? GetOrganizationBAccountID(int? organizationID)
  {
    return PXAccess.MasterBranches.GetOrganizationBAccountID(organizationID);
  }

  public static int?[] GetAvailableOrganizationIDs()
  {
    return PXAccess.CurrentUserInformationProvider.GetActiveBranches().Select<BranchInfo, int?>((Func<BranchInfo, int?>) (item => PXAccess.GetParentOrganizationID(new int?(item.Id)))).Distinct<int?>().ToArray<int?>();
  }

  public static bool IsSingleBranchCompany()
  {
    PXAccess.MasterCollection masterBranches = PXAccess.MasterBranches;
    return (masterBranches != null ? masterBranches.AllBranchesExceptDeleted.Count : 0) <= 1;
  }

  internal static int[] GetBranchIDsForCurrentOrganization()
  {
    return PXAccess.GetBranchInfoForCurrentOrganization<int>((Func<PXAccess.MasterCollection.Branch, int>) (b => b.BranchID));
  }

  internal static string[] GetBranchCDsForCurrentOrganization()
  {
    return PXAccess.GetBranchInfoForCurrentOrganization<string>((Func<PXAccess.MasterCollection.Branch, string>) (b => b.BranchCD));
  }

  private static T[] GetBranchInfoForCurrentOrganization<T>(
    Func<PXAccess.MasterCollection.Branch, T> selector)
  {
    PXAccess.MasterCollection.Organization organizationById = PXAccess.GetOrganizationByID(PXAccess.GetParentOrganizationID(PXAccess.GetBranchID()));
    return organizationById == null ? Array.Empty<T>() : Enumerable.ToArray<T>(organizationById.ChildBranches.Select<PXAccess.MasterCollection.Branch, T>(selector));
  }

  private static int[] GetAvailableBranches(
    List<PXAccess.MasterCollection.Branch> branches,
    bool restricted)
  {
    IEnumerable<int> ints = branches.Where<PXAccess.MasterCollection.Branch>((Func<PXAccess.MasterCollection.Branch, bool>) (branch => !branch.DeletedDatabaseRecord)).Select<PXAccess.MasterCollection.Branch, int>((Func<PXAccess.MasterCollection.Branch, int>) (branch => branch.BranchID));
    return !restricted ? ints.ToArray<int>() : ints.Intersect<int>(PXAccess.CurrentUserInformationProvider.GetActiveBranches().Select<BranchInfo, int>((Func<BranchInfo, int>) (info => info.Id))).ToArray<int>();
  }

  private static IPrincipal GetUser() => PXContext.PXIdentity.AuthUser;

  public static string GetSessionLimit() => PXAccess.Provider.GetSessionLimit();

  internal static event Func<string> OnGetConnectionString;

  internal static string GetConnectionString()
  {
    RuntimeHelpers.EnsureSufficientExecutionStack();
    PXCancellationToken.CheckCancellation();
    Func<string> connectionString1 = PXAccess.OnGetConnectionString;
    string connectionString2 = connectionString1 != null ? connectionString1() : (string) null;
    string connectionString3 = PXConnectionStringScope.GetConnectionString();
    if (connectionString3 != null)
      return connectionString3;
    if (connectionString2 != null)
      return connectionString2;
    return PXAccess.Provider == null ? (string) null : PXAccess.Provider.GetConnectionString();
  }

  internal static bool NoConnectionString() => PXAccess.GetConnectionString() == null;

  internal static IPXMultiDatabaseUser[] GetMultiDatabaseUsers()
  {
    return PXAccess.Provider == null ? (IPXMultiDatabaseUser[]) null : PXAccess.Provider.GetMultiDatabaseUsers();
  }

  internal static string GetCompanyID()
  {
    return PXAccess.Provider == null ? (string) null : PXAccess.Provider.GetCompanyID();
  }

  [Obsolete("Use ISystemRolesProvider.GetAdministratorRole()")]
  public static string GetAdministratorRole() => PXAccess.Provider.GetAdministratorRole();

  [Obsolete("Use ISystemRolesProvider.GetPortalAdministratorRole()")]
  public static string GetPortalAdministratorRole()
  {
    return PXAccess.Provider.GetPortalAdministratorRole();
  }

  [Obsolete("Use IOptions<CustomizationOptions>.Value.CustomizerRole")]
  public static string GetCustomizerRole()
  {
    return ServiceLocator.Current.GetInstance<IOptions<CustomizationOptions>>().Value.CustomizerRole;
  }

  [Obsolete("Use IPrincipal.IsInRole(IOptions<ReportOptions>.Value.DesignerRole). Current IPrincipal may be got using IPXIdentityAccessor.Identity?.User")]
  public static bool IsUserReportDesigner()
  {
    string designerRole = ServiceLocator.Current.GetInstance<IOptions<ReportOptions>>().Value.DesignerRole;
    return PXAccess.GetUser().IsInRole(designerRole);
  }

  [Obsolete("Use IPrincipal.IsInRole(IOptions<BusinessDateOptions>.Value.OverriderRole). Current IPrincipal may be got using IPXIdentityAccessor.Identity?.User")]
  public static bool IsUserBusinessDateOverride()
  {
    if (!PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+secureBusinessDate"))
      return true;
    string overriderRole = ServiceLocator.Current.GetInstance<IOptions<BusinessDateOptions>>().Value.OverriderRole;
    return PXAccess.GetUser().IsInRole(overriderRole);
  }

  [Obsolete("Use IPrincipal.IsInRole(IOptions<CustomizationOptions>.Value.CustomizerRole). Current IPrincipal may be got using IPXIdentityAccessor.Identity?.User")]
  internal static bool IsUserCustomizator()
  {
    string role = ServiceLocator.Current.GetInstance<IOptions<CustomizationOptions>>().Value.CustomizerRole;
    return PXContext.PXIdentity.AuthUser.With<IPrincipal, bool>((Func<IPrincipal, bool>) (_ => _.IsInRole(role)));
  }

  [Obsolete("Use ISystemRolesProvider.GetAdministratorRoles()")]
  public static string[] GetAdministratorRoles() => PXAccess.Provider.GetAdministratorRoles();

  internal static PXLoginScope GetAdminScope()
  {
    string userName = "admin";
    if (PXDatabase.Companies.Length != 0)
      userName = $"{userName}@{PXDatabase.Companies[0]}";
    return new PXLoginScope(userName, PXAccess.GetAdministratorRoles());
  }

  internal static PXLoginScope GetAdminScopeForCompany(int companyId)
  {
    int num = PXDatabase.Provider.Companies.Length != 0 ? 1 : 0;
    CompanyInfo companyInfo = ((IEnumerable<CompanyInfo>) PXDatabase.Provider.SelectCompanies(false)).FirstOrDefault<CompanyInfo>((Func<CompanyInfo, bool>) (c => c.CompanyID == companyId));
    string userName = "admin";
    if (num != 0)
      userName = $"{userName}@{companyInfo?.LoginName}";
    return new PXLoginScope(userName, PXAccess.GetAdministratorRoles());
  }

  internal static PXImpersonationContext GetAdminImpersonator()
  {
    string userName = "admin";
    if (PXDatabase.Companies.Length != 0)
      userName = $"{userName}@{PXDatabase.Companies[0]}";
    return new PXImpersonationContext(userName, PXAccess.GetAdministratorRoles());
  }

  private static void GetRights(
    PXCache cache,
    out PXCacheRights rights,
    out List<string> invisible,
    out List<string> disabled)
  {
    PXAccessProvider provider = PXAccess.Provider;
    if (PXAccess.s_Skipped)
    {
      rights = PXCacheRights.Delete;
      invisible = (List<string>) null;
      disabled = (List<string>) null;
    }
    else
      provider.GetRights(cache, out rights, out invisible, out disabled);
  }

  public static void GetRights(
    string graphScreenID,
    string graphName,
    System.Type cacheType,
    out PXCacheRights rights,
    out List<string> invisible,
    out List<string> disabled)
  {
    PXAccessProvider provider = PXAccess.Provider;
    if (PXAccess.s_Skipped)
    {
      rights = PXCacheRights.Delete;
      invisible = (List<string>) null;
      disabled = (List<string>) null;
    }
    else
      provider.GetRights(graphScreenID, graphName, cacheType, out rights, out invisible, out disabled);
  }

  internal static bool IsScreenHiddenByFeature(string screenID)
  {
    return PXAccess.Provider.IsScreenHiddenByFeature(screenID);
  }

  private static PXCacheRights GetRights(string fieldClass)
  {
    PXAccessProvider provider = PXAccess.Provider;
    return !PXAccess.s_Skipped ? provider.GetRights(fieldClass) : PXCacheRights.Select;
  }

  public static PXRoleList GetRoles(string screenID)
  {
    PXAccessProvider provider = PXAccess.Provider;
    return PXAccess.s_Skipped ? (PXRoleList) null : provider.GetRoles(screenID);
  }

  public static PXRoleList RegisterGraphType(string graphType, string screenID)
  {
    PXAccessProvider provider = PXAccess.Provider;
    return PXAccess.s_Skipped ? (PXRoleList) null : provider.RegisterGraphType(graphType, screenID);
  }

  public static bool VerifyRights(System.Type graphType)
  {
    PXSiteMapNode siteMapNode = PXSiteMap.Provider.FindSiteMapNode(graphType);
    return !string.IsNullOrEmpty(siteMapNode?.ScreenID) && siteMapNode.IsAccessibleToUser();
  }

  public static bool VerifyRights(string screenID)
  {
    PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenID);
    return mapNodeByScreenId != null && mapNodeByScreenId.IsAccessibleToUser();
  }

  internal static void SetRights(
    PXCache cache,
    PXCacheRights rights,
    List<string> invisible,
    List<string> disabled)
  {
    if (cache._CacheSecurity == null)
      cache._CacheSecurity = (object) new PXAccess.cacheInitializer(cache);
    ((PXAccess.cacheInitializer) cache._CacheSecurity).SetRights(cache, rights, invisible, disabled);
  }

  [PXInternalUseOnly]
  public static (bool IsInvisible, bool isDisabled) CheckFieldsRights(
    PXCache cache,
    string fieldName)
  {
    return (((PXAccess.cacheInitializer) cache._CacheSecurity).isInvisible(fieldName), ((PXAccess.cacheInitializer) cache._CacheSecurity).isDisabled(fieldName));
  }

  public static void Secure(PXCache cache, PXEventSubscriberAttribute attribute)
  {
    if (cache._CacheSecurity == null)
      cache._CacheSecurity = (object) new PXAccess.cacheInitializer(cache);
    ((PXAccess.cacheInitializer) cache._CacheSecurity).Secure(cache, attribute);
  }

  public static void Clear() => PXAccess.Provider.Clear();

  public static bool IsStringListValueDisabled(string cacheName, string fieldName, string value)
  {
    return PXAccess.Provider.IsStringListValueDisabled(cacheName, fieldName, value);
  }

  /// <summary>
  /// <para>Executes an <paramref name="action" /> for each tenant in the system.</para>
  /// <para>The action will be executed for each tenant even if it threw an exception in some of the tenants.</para>
  /// </summary>
  /// <param name="action">An action to be executed for each tenant.</param>
  /// <exception cref="T:System.AggregateException">Thrown if the <paramref name="action" /> threw an exception in at least one tenant.</exception>
  internal static void ForEachTenantAsAdmin(System.Action action)
  {
    if (PXDatabase.Companies == null || PXDatabase.Companies.Length == 0)
    {
      using (new PXLoginScope("admin", PXAccess.GetAdministratorRoles()))
      {
        try
        {
          action();
        }
        catch (Exception ex)
        {
          throw new AggregateException(new Exception[1]
          {
            ex
          });
        }
      }
    }
    else
    {
      List<Exception> innerExceptions = new List<Exception>();
      foreach (string company in PXDatabase.Companies)
      {
        using (new PXLoginScope("admin@" + company, PXAccess.GetAdministratorRoles()))
        {
          try
          {
            action();
          }
          catch (Exception ex)
          {
            innerExceptions.Add(ex);
          }
        }
      }
      if (innerExceptions.Count > 0)
        throw new AggregateException((IEnumerable<Exception>) innerExceptions);
    }
  }

  public class FeatureInfo
  {
    public string ID { get; set; }

    public string Name { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public System.Type FeatureSet { get; set; }

    public bool Visible { get; set; }
  }

  [Serializable]
  public class Branch : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBInt]
    public virtual int? OrganizationID { get; set; }

    [PXDBInt]
    public virtual int? BranchID { get; set; }

    [PXDBString(30, IsUnicode = true, IsKey = true)]
    public virtual string BranchCD { get; set; }

    [PXDBString(64 /*0x40*/, IsKey = true, IsUnicode = true)]
    public string RoleName { get; set; }

    [PXDBString(IsUnicode = true, InputMask = "")]
    public string LogoName { get; set; }

    [PXDBInt]
    public virtual int? LedgerID { get; set; }

    [PXDBBool]
    public bool? Active { get; set; }

    [PXDBInt]
    [PXUIField(Visible = false, Enabled = false)]
    public virtual int? BAccountID { get; set; }

    [PXDBString(5, IsUnicode = true)]
    public virtual string BaseCuryID { get; set; }

    [PXDBString(2)]
    public virtual string OrganizationLocalizationCode { get; set; }

    [PXDBBool]
    public virtual bool? DeletedDatabaseRecord { get; set; }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXAccess.Branch.organizationID>
    {
    }

    public abstract class branchID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PXAccess.Branch.branchID>
    {
    }

    public abstract class branchCD : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PXAccess.Branch.branchCD>
    {
    }

    public abstract class roleName : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PXAccess.Branch.roleName>
    {
    }

    public abstract class logoName : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PXAccess.Branch.logoName>
    {
    }

    public abstract class ledgerID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PXAccess.Branch.ledgerID>
    {
    }

    public abstract class active : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PXAccess.Branch.active>
    {
    }

    public abstract class bAccountID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PXAccess.Branch.bAccountID>
    {
    }

    public abstract class baseCuryID : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PXAccess.Branch.baseCuryID>
    {
    }

    public abstract class organizationLocalizationCode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXAccess.Branch.organizationLocalizationCode>
    {
    }

    public abstract class deletedDatabaseRecord : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PXAccess.Branch.deletedDatabaseRecord>
    {
    }
  }

  public class Organization : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBInt]
    public virtual int? OrganizationID { get; set; }

    [PXDBString(30, IsUnicode = true, IsKey = true)]
    public virtual string OrganizationCD { get; set; }

    [PXDBString(60, IsUnicode = true)]
    public virtual string OrganizationName { get; set; }

    [PXDBInt]
    [PXUIField(Visible = false, Enabled = false)]
    public virtual int? BAccountID { get; set; }

    [PXDBString(5, IsUnicode = true)]
    public virtual string BaseCuryID { get; set; }

    [PXDBString(2)]
    public virtual string OrganizationLocalizationCode { get; set; }

    [PXDBBool]
    public virtual bool? DeletedDatabaseRecord { get; set; }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXAccess.Organization.organizationID>
    {
    }

    public abstract class organizationCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXAccess.Organization.organizationCD>
    {
    }

    public abstract class organizationName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXAccess.Organization.organizationName>
    {
    }

    public abstract class bAccountID : IBqlField, IBqlOperand
    {
    }

    public abstract class baseCuryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXAccess.Organization.baseCuryID>
    {
    }

    public abstract class organizationLocalizationCode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXAccess.Organization.organizationLocalizationCode>
    {
    }

    public abstract class deletedDatabaseRecord : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PXAccess.Organization.deletedDatabaseRecord>
    {
    }
  }

  [PXHidden]
  public class GroupOrganizationLink : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBInt]
    public virtual int? GroupID { get; set; }

    [PXDBInt]
    public virtual int? OrganizationID { get; set; }

    [PXDBBool]
    public virtual bool? PrimaryGroup { get; set; }

    public abstract class groupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXAccess.GroupOrganizationLink.groupID>
    {
    }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXAccess.GroupOrganizationLink.organizationID>
    {
    }

    public abstract class primaryGroup : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PXAccess.GroupOrganizationLink.primaryGroup>
    {
    }
  }

  public class PortalSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(32 /*0x20*/, IsKey = true)]
    public virtual string PortalSetupID { get; set; }

    [PXDBString(1, IsFixed = true)]
    public virtual string DisplayFinancialDocuments { get; set; }

    [PXDBInt]
    public virtual int? RestrictByOrganizationID { get; set; }

    [PXDBInt]
    public virtual int? RestrictByBranchID { get; set; }

    public abstract class portalSetupID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXAccess.PortalSetup.portalSetupID>
    {
    }

    public abstract class displayFinancialDocuments : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXAccess.PortalSetup.displayFinancialDocuments>
    {
    }

    public abstract class restrictByOrganizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXAccess.PortalSetup.restrictByOrganizationID>
    {
    }

    public abstract class restrictByBranchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXAccess.PortalSetup.restrictByBranchID>
    {
    }
  }

  [PXHidden]
  public class BAccount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _ParentBAccountID;

    [PXDBIdentity]
    public virtual int? BAccountID { get; set; }

    [PXForeignReference(typeof (PX.Data.ReferentialIntegrity.Attributes.Field<PXAccess.BAccount.parentBAccountID>.IsRelatedTo<PXAccess.BAccount.bAccountID>))]
    public virtual int? ParentBAccountID
    {
      get => this._ParentBAccountID;
      set => this._ParentBAccountID = value;
    }

    [PXDBInt]
    public virtual int? DefContactID { get; set; }

    [PXDBString(60, IsUnicode = true)]
    public virtual string AcctName { get; set; }

    public abstract class bAccountID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PXAccess.BAccount.bAccountID>
    {
    }

    public abstract class parentBAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXAccess.BAccount.parentBAccountID>
    {
    }

    public abstract class defContactID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PXAccess.BAccount.defContactID>
    {
    }

    public abstract class acctName : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PXAccess.BAccount.acctName>
    {
    }
  }

  [PXHidden]
  public class EPEmployee : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBIdentity]
    public virtual int? BAccountID { get; set; }

    [PXDBGuid(false)]
    public virtual Guid? UserID { get; set; }

    public abstract class bAccountID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PXAccess.EPEmployee.bAccountID>
    {
    }

    public abstract class userID : BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PXAccess.EPEmployee.userID>
    {
    }
  }

  [PXHidden]
  public class Contact : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBIdentity]
    public virtual int? ContactID { get; set; }

    [PXDBInt]
    public virtual int? BAccountID { get; set; }

    [PXDBGuid(false)]
    public virtual Guid? UserID { get; set; }

    public abstract class contactID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PXAccess.Contact.contactID>
    {
    }

    public abstract class bAccountID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PXAccess.Contact.bAccountID>
    {
    }

    public abstract class userID : BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PXAccess.Contact.userID>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class Ledger : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }

  public class MasterCollection : IPrefetchable, IPXCompanyDependent
  {
    private static readonly System.Type[] _dbTables = new System.Type[2]
    {
      typeof (PXAccess.MasterCollection.Branch),
      typeof (PXAccess.MasterCollection.Organization)
    };
    private Dictionary<int, PXAccess.MasterCollection.Organization> organizationsByID = new Dictionary<int, PXAccess.MasterCollection.Organization>();
    private Dictionary<string, PXAccess.MasterCollection.Organization> organizationsByCD = new Dictionary<string, PXAccess.MasterCollection.Organization>();
    private Dictionary<int, string> _allBranchesExceptDeleted = new Dictionary<int, string>();
    private Dictionary<int, PXAccess.MasterCollection.Branch> allBranchesByID = new Dictionary<int, PXAccess.MasterCollection.Branch>();
    private Dictionary<string, PXAccess.MasterCollection.Branch> allBranchesByCD = new Dictionary<string, PXAccess.MasterCollection.Branch>();

    public static System.Type[] DBTables => PXAccess.MasterCollection._dbTables;

    internal Dictionary<int, PXAccess.MasterCollection.Organization> OrganizationsByID
    {
      get => this.organizationsByID;
    }

    internal Dictionary<string, PXAccess.MasterCollection.Organization> OrganizationsByCD
    {
      get => this.organizationsByCD;
    }

    internal Dictionary<int, string> AllBranchesExceptDeleted => this._allBranchesExceptDeleted;

    protected internal Dictionary<int, PXAccess.MasterCollection.Branch> AllBranchesByID
    {
      get => this.allBranchesByID;
    }

    internal Dictionary<string, PXAccess.MasterCollection.Branch> AllBranchesByCD
    {
      get => this.allBranchesByCD;
    }

    public void Prefetch()
    {
      this.organizationsByCD.Clear();
      this.organizationsByID.Clear();
      this._allBranchesExceptDeleted.Clear();
      this.allBranchesByID.Clear();
      this.allBranchesByCD.Clear();
      if (!PXDatabase.Provider.SchemaCache.TableExists(typeof (PXAccess.Organization).Name) || !PXDatabase.Provider.SchemaCache.TableExists(typeof (PXAccess.Branch).Name))
        return;
      using (new PXReadDeletedScope())
      {
        List<PXAccess.MasterCollection.Organization> list1 = PXDatabase.SelectMulti<PXAccess.Organization>((PXDataField) new PXDataField<PXAccess.Organization.organizationID>(), (PXDataField) new PXDataField<PXAccess.Organization.organizationCD>(), (PXDataField) new PXDataField<PXAccess.Organization.organizationName>(), (PXDataField) new PXDataField<PXAccess.Organization.bAccountID>(), (PXDataField) new PXDataField<PXAccess.Organization.deletedDatabaseRecord>(), (PXDataField) new PXDataField<PXAccess.Organization.baseCuryID>(), (PXDataField) new PXDataField<PXAccess.Organization.organizationLocalizationCode>(), (PXDataField) new PXDataFieldOrder<PXAccess.Organization.deletedDatabaseRecord>(true), (PXDataField) new PXDataFieldOrder<PXAccess.Organization.organizationCD>()).Select<PXDataRecord, PXAccess.MasterCollection.Organization>((Func<PXDataRecord, PXAccess.MasterCollection.Organization>) (row =>
        {
          return new PXAccess.MasterCollection.Organization()
          {
            OrganizationID = row.GetInt32(0),
            OrganizationCD = row.GetString(1).Trim(),
            OrganizationName = row.GetString(2),
            BAccountID = row.GetInt32(3),
            DeletedDatabaseRecord = row.GetBoolean(4),
            BaseCuryID = row.GetString(5),
            OrganizationLocalizationCode = row.GetString(6)
          };
        })).ToList<PXAccess.MasterCollection.Organization>();
        foreach (PXAccess.MasterCollection.Organization organization in list1)
        {
          this.organizationsByCD[organization.OrganizationCD] = organization;
          this.organizationsByID[organization.OrganizationID.Value] = organization;
        }
        List<PXAccess.GroupOrganizationLink> list2 = PXDatabase.SelectMulti<PXAccess.GroupOrganizationLink>((PXDataField) new PXDataField<PXAccess.GroupOrganizationLink.groupID>(), (PXDataField) new PXDataField<PXAccess.GroupOrganizationLink.organizationID>(), (PXDataField) new PXDataField<PXAccess.GroupOrganizationLink.primaryGroup>()).Select<PXDataRecord, PXAccess.GroupOrganizationLink>((Func<PXDataRecord, PXAccess.GroupOrganizationLink>) (row => new PXAccess.GroupOrganizationLink()
        {
          GroupID = row.GetInt32(0),
          OrganizationID = row.GetInt32(1),
          PrimaryGroup = row.GetBoolean(2)
        })).ToList<PXAccess.GroupOrganizationLink>();
        foreach (PXAccess.MasterCollection.Organization organization1 in list1)
        {
          PXAccess.MasterCollection.Organization organization = organization1;
          foreach (PXAccess.GroupOrganizationLink organizationLink in list2.Where<PXAccess.GroupOrganizationLink>((Func<PXAccess.GroupOrganizationLink, bool>) (_ =>
          {
            int? groupId = _.GroupID;
            int? organizationId = organization.OrganizationID;
            return groupId.GetValueOrDefault() == organizationId.GetValueOrDefault() & groupId.HasValue == organizationId.HasValue;
          })))
          {
            organization.IsGroup = true;
            PXAccess.MasterCollection.Organization organization2 = this.organizationsByID[organizationLink.OrganizationID.Value];
            organization2.IsChildOrganization = true;
            organization2.Parents.Add(organization);
            organization.ChildOrganizations.Add(organization2);
            bool? primaryGroup = organizationLink.PrimaryGroup;
            bool flag = true;
            if (primaryGroup.GetValueOrDefault() == flag & primaryGroup.HasValue)
              organization2.PrimaryParent = this.organizationsByID[organization.OrganizationID.Value];
          }
        }
        if (this.organizationsByID.Any<KeyValuePair<int, PXAccess.MasterCollection.Organization>>())
        {
          foreach (PXAccess.MasterCollection.Branch branch1 in PXDatabase.SelectMulti<PXAccess.Branch>(Yaql.join<PXAccess.BAccount>(Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<PXAccess.BAccount.bAccountID>((string) null), Yaql.column<PXAccess.Branch.bAccountID>((string) null)), (YaqlJoinType) 0), (PXDataField) new PXAliasedDataField<PXAccess.Branch.organizationID>(), (PXDataField) new PXAliasedDataField<PXAccess.Branch.branchID>(), (PXDataField) new PXAliasedDataField<PXAccess.Branch.branchCD>(), (PXDataField) new PXAliasedDataField<PXAccess.BAccount.acctName>(), (PXDataField) new PXAliasedDataField<PXAccess.BAccount.bAccountID>(), (PXDataField) new PXAliasedDataField<PXAccess.Branch.deletedDatabaseRecord>(), (PXDataField) new PXAliasedDataField<PXAccess.Branch.baseCuryID>(), (PXDataField) new PXAliasedDataFieldOrder<PXAccess.Branch.deletedDatabaseRecord>(true), (PXDataField) new PXAliasedDataFieldOrder<PXAccess.Branch.branchCD>()).Select<PXDataRecord, PXAccess.MasterCollection.Branch>((Func<PXDataRecord, PXAccess.MasterCollection.Branch>) (record =>
          {
            int? int32 = record.GetInt32(1);
            int branchID = int32.Value;
            string branchCD = record.GetString(2).TrimEnd();
            string branchName = record.GetString(3);
            int32 = record.GetInt32(4);
            int bAccountID = int32.Value;
            Dictionary<int, PXAccess.MasterCollection.Organization> organizationsById = this.organizationsByID;
            int32 = record.GetInt32(0);
            int key = int32.Value;
            PXAccess.MasterCollection.Organization organization = organizationsById[key];
            int num = record.GetBoolean(5).Value ? 1 : 0;
            string baseCuryID = record.GetString(6);
            return new PXAccess.MasterCollection.Branch(branchID, branchCD, branchName, bAccountID, organization, num != 0, baseCuryID);
          })))
          {
            PXAccess.MasterCollection.Branch branch = branch1;
            branch.Organization.ChildBranches.Add(branch);
            branch.Organization.Parents.ForEach((System.Action<PXAccess.MasterCollection.Organization>) (o => o.ChildBranches.Add(branch)));
            this.allBranchesByID[branch.BranchID] = branch;
            this.allBranchesByCD[branch.BranchCD] = branch;
          }
        }
      }
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PXAccess.Branch>((PXDataField) new PXDataField<PXAccess.Branch.branchID>(), (PXDataField) new PXDataField<PXAccess.Branch.branchCD>(), (PXDataField) new PXDataFieldValue<PXAccess.Branch.active>(PXDbType.Bit, (object) 1)))
      {
        int? int32 = pxDataRecord.GetInt32(0);
        string str = pxDataRecord.GetString(1).Trim();
        if (int32.HasValue)
          this._allBranchesExceptDeleted[int32.Value] = str;
      }
    }

    public PXAccess.MasterCollection.Branch GetBranch(int? branchID)
    {
      if (!branchID.HasValue)
        return (PXAccess.MasterCollection.Branch) null;
      PXAccess.MasterCollection.Branch branch;
      return PXAccess.MasterBranches.AllBranchesByID.TryGetValue(branchID.Value, out branch) ? branch : (PXAccess.MasterCollection.Branch) null;
    }

    public string GetBranchCD(int? branchID)
    {
      if (!branchID.HasValue)
        return (string) null;
      PXAccess.MasterCollection.Branch branch;
      return PXAccess.MasterBranches.AllBranchesByID.TryGetValue(branchID.Value, out branch) ? branch.BranchCD : (string) null;
    }

    public int? GetBranchID(string branchCD)
    {
      if (string.IsNullOrEmpty(branchCD))
        return new int?();
      PXAccess.MasterCollection.Branch branch;
      return PXAccess.MasterBranches.AllBranchesByCD.TryGetValue(branchCD, out branch) ? new int?(branch.BranchID) : new int?();
    }

    public bool TryGetOrganizationChildBranchIDs(
      string organizationCD,
      out List<PXAccess.MasterCollection.Branch> branches)
    {
      PXAccess.MasterCollection.Organization organization;
      if (this.organizationsByCD.TryGetValue(organizationCD, out organization))
      {
        branches = new List<PXAccess.MasterCollection.Branch>((IEnumerable<PXAccess.MasterCollection.Branch>) organization.ChildBranches);
        return true;
      }
      branches = new List<PXAccess.MasterCollection.Branch>();
      return false;
    }

    public bool TryGetOrganizationChildBranchIDs(
      int organizationID,
      out List<PXAccess.MasterCollection.Branch> branches)
    {
      PXAccess.MasterCollection.Organization organization;
      if (this.organizationsByID.TryGetValue(organizationID, out organization))
      {
        branches = new List<PXAccess.MasterCollection.Branch>((IEnumerable<PXAccess.MasterCollection.Branch>) organization.ChildBranches);
        return true;
      }
      branches = new List<PXAccess.MasterCollection.Branch>();
      return false;
    }

    public PXAccess.MasterCollection.Organization GetOrganization(int? organizationID)
    {
      if (!organizationID.HasValue)
        return (PXAccess.MasterCollection.Organization) null;
      PXAccess.MasterCollection.Organization organization;
      return PXAccess.MasterBranches.OrganizationsByID.TryGetValue(organizationID.Value, out organization) ? organization : (PXAccess.MasterCollection.Organization) null;
    }

    public string GetOrganizationCD(int? organizationID)
    {
      if (!organizationID.HasValue)
        return (string) null;
      PXAccess.MasterCollection.Organization organization;
      return PXAccess.MasterBranches.OrganizationsByID.TryGetValue(organizationID.Value, out organization) ? organization.OrganizationCD : (string) null;
    }

    public int? GetOrganizationID(string organizationCD)
    {
      if (string.IsNullOrEmpty(organizationCD))
        return new int?();
      PXAccess.MasterCollection.Organization organization;
      return PXAccess.MasterBranches.OrganizationsByCD.TryGetValue(organizationCD, out organization) ? organization.OrganizationID : new int?();
    }

    public int? GetOrganizationBAccountID(int? organizationID)
    {
      if (!organizationID.HasValue)
        return new int?();
      PXAccess.MasterCollection.Organization organization;
      return PXAccess.MasterBranches.OrganizationsByID.TryGetValue(organizationID.Value, out organization) ? organization.BAccountID : new int?();
    }

    public class Organization : PXAccess.Organization
    {
      public List<PXAccess.MasterCollection.Organization> Parents = new List<PXAccess.MasterCollection.Organization>();
      public List<PXAccess.MasterCollection.Branch> ChildBranches = new List<PXAccess.MasterCollection.Branch>();
      public List<PXAccess.MasterCollection.Organization> ChildOrganizations = new List<PXAccess.MasterCollection.Organization>();

      public PXAccess.MasterCollection.Organization PrimaryParent { get; set; }

      public bool IsGroup { get; set; }

      public bool IsChildOrganization { get; set; }

      public bool IsSingle
      {
        get
        {
          if (this.ChildBranches.Count != 1)
            return false;
          int baccountId1 = this.ChildBranches[0].BAccountID;
          int? baccountId2 = this.BAccountID;
          int valueOrDefault = baccountId2.GetValueOrDefault();
          return baccountId1 == valueOrDefault & baccountId2.HasValue;
        }
      }
    }

    public class Branch
    {
      public int BranchID { get; private set; }

      public string BranchCD { get; private set; }

      public string BranchName { get; private set; }

      public PXAccess.MasterCollection.Organization Organization { get; private set; }

      public int BAccountID { get; private set; }

      public string BaseCuryID { get; private set; }

      public bool DeletedDatabaseRecord { get; private set; }

      public Branch(
        int branchID,
        string branchCD,
        string branchName,
        int bAccountID,
        PXAccess.MasterCollection.Organization organization,
        bool deletedDatabaseRecord)
      {
        this.BranchID = branchID;
        this.BranchCD = branchCD;
        this.BranchName = branchName;
        this.BAccountID = bAccountID;
        this.Organization = organization;
        this.DeletedDatabaseRecord = deletedDatabaseRecord;
      }

      public Branch(
        int branchID,
        string branchCD,
        string branchName,
        int bAccountID,
        PXAccess.MasterCollection.Organization organization,
        bool deletedDatabaseRecord,
        string baseCuryID)
      {
        this.BranchID = branchID;
        this.BranchCD = branchCD;
        this.BranchName = branchName;
        this.BAccountID = bAccountID;
        this.Organization = organization;
        this.DeletedDatabaseRecord = deletedDatabaseRecord;
        this.BaseCuryID = baseCuryID;
      }
    }
  }

  public sealed class UserContactsCollection
  {
    private bool? IsAvailable;
    private static readonly System.Type[] _dbTables = new System.Type[4]
    {
      typeof (Users),
      typeof (PXAccess.EPEmployee),
      typeof (PXAccess.Contact),
      typeof (PXAccess.BAccount)
    };
    private ConcurrentDictionary<Guid, int?> userToContact = new ConcurrentDictionary<Guid, int?>();
    private readonly ConcurrentDictionary<Guid, int?> userToBAccount = new ConcurrentDictionary<Guid, int?>();
    private readonly ConcurrentDictionary<Guid, HashSet<int>> userToBAccountTree = new ConcurrentDictionary<Guid, HashSet<int>>();
    private ConcurrentDictionary<int, Guid?> contactToUser = new ConcurrentDictionary<int, Guid?>();

    public static System.Type[] DBTables => PXAccess.UserContactsCollection._dbTables;

    public int? GetContactID(Guid? userID)
    {
      if (!userID.HasValue)
        return new int?();
      if (((int) this.IsAvailable ?? (this.CheckAvailability() ? 1 : 0)) == 0)
        return new int?();
      int? contactId;
      if (this.userToContact.TryGetValue(userID.Value, out contactId))
        return contactId;
      int? contactID;
      int? baccountID;
      this.GetAccountByUser(userID, out contactID, out baccountID);
      this.userToContact[userID.Value] = contactID;
      this.userToBAccount[userID.Value] = baccountID;
      if (contactID.HasValue)
        this.contactToUser[contactID.Value] = userID;
      return contactID;
    }

    public Guid? GetUserID(int? contactID)
    {
      if (!contactID.HasValue)
        return new Guid?();
      if (((int) this.IsAvailable ?? (this.CheckAvailability() ? 1 : 0)) == 0)
        return new Guid?();
      Guid? userId;
      if (this.contactToUser.TryGetValue(contactID.Value, out userId))
        return userId;
      Guid? userID;
      int? baccountID;
      this.GetAccountByContact(contactID, out userID, out baccountID);
      this.contactToUser[contactID.Value] = userID;
      if (userID.HasValue)
      {
        this.userToContact[userID.Value] = contactID;
        this.userToBAccount[userID.Value] = baccountID;
      }
      return userID;
    }

    public int? GetBAccountID(Guid? userID)
    {
      if (!userID.HasValue)
        return new int?();
      if (((int) this.IsAvailable ?? (this.CheckAvailability() ? 1 : 0)) == 0)
        return new int?();
      int? baccountId;
      if (this.userToBAccount.TryGetValue(userID.Value, out baccountId))
        return baccountId;
      int? contactID;
      int? baccountID;
      this.GetAccountByUser(userID, out contactID, out baccountID);
      this.userToContact[userID.Value] = contactID;
      this.userToBAccount[userID.Value] = baccountID;
      if (contactID.HasValue)
        this.contactToUser[contactID.Value] = userID;
      return baccountID;
    }

    public HashSet<int> GetBAccountIDTree(Guid? userID)
    {
      if (!userID.HasValue)
        return (HashSet<int>) null;
      if (((int) this.IsAvailable ?? (this.CheckAvailability() ? 1 : 0)) == 0)
        return (HashSet<int>) null;
      HashSet<int> intSet;
      if (this.userToBAccountTree.TryGetValue(userID.Value, out intSet))
        return intSet ?? new HashSet<int>();
      int? contactID;
      int? baccountID;
      this.GetAccountByUser(userID, out contactID, out baccountID);
      HashSet<int> baccountIDTree;
      this.GetAccountTreeByAccount(baccountID, out baccountIDTree);
      if (baccountIDTree == null)
        baccountIDTree = new HashSet<int>();
      this.userToContact[userID.Value] = contactID;
      this.userToBAccount[userID.Value] = baccountID;
      this.userToBAccountTree[userID.Value] = baccountIDTree;
      if (contactID.HasValue)
        this.contactToUser[contactID.Value] = userID;
      return baccountIDTree;
    }

    private void GetAccountByContact(int? contactID, out Guid? userID, out int? baccountID)
    {
      using (new PXConnectionScope())
      {
        userID = new Guid?();
        baccountID = new int?();
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Users>(Yaql.join<PXAccess.EPEmployee>(Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<PXAccess.EPEmployee.userID>((string) null), Yaql.column<Users.pKID>((string) null)), (YaqlJoinType) 0), Yaql.join<PXAccess.BAccount>(Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<PXAccess.BAccount.bAccountID>((string) null), Yaql.column<PXAccess.EPEmployee.bAccountID>((string) null)), (YaqlJoinType) 0), (PXDataField) new PXDataField<Users.pKID>(), (PXDataField) new PXDataField<PXAccess.BAccount.defContactID>(), (PXDataField) new PXDataField<PXAccess.BAccount.bAccountID>("BAccount"), (PXDataField) new PXDataFieldValue<PXAccess.BAccount.defContactID>((object) contactID.Value)))
        {
          userID = (Guid?) pxDataRecord?.GetGuid(0);
          baccountID = (int?) pxDataRecord?.GetInt32(2);
        }
        if (PXSiteMap.IsPortal || contactID.HasValue || baccountID.HasValue)
          return;
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Users>(Yaql.join<PXAccess.Contact>(Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<PXAccess.Contact.userID>((string) null), Yaql.column<Users.pKID>((string) null)), (YaqlJoinType) 0), (PXDataField) new PXDataField<Users.pKID>(), (PXDataField) new PXDataField<PXAccess.Contact.contactID>(), (PXDataField) new PXDataField<PXAccess.Contact.bAccountID>(), (PXDataField) new PXDataFieldValue<PXAccess.BAccount.defContactID>((object) contactID.Value)))
        {
          userID = (Guid?) pxDataRecord?.GetGuid(0);
          baccountID = (int?) pxDataRecord?.GetInt32(2);
        }
      }
    }

    private void GetAccountByUser(Guid? userID, out int? contactID, out int? baccountID)
    {
      using (new PXConnectionScope())
      {
        contactID = new int?();
        baccountID = new int?();
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Users>(Yaql.join<PXAccess.EPEmployee>(Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<PXAccess.EPEmployee.userID>((string) null), Yaql.column<Users.pKID>((string) null)), (YaqlJoinType) 0), Yaql.join<PXAccess.BAccount>(Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<PXAccess.BAccount.bAccountID>((string) null), Yaql.column<PXAccess.EPEmployee.bAccountID>((string) null)), (YaqlJoinType) 0), (PXDataField) new PXDataField<Users.pKID>(), (PXDataField) new PXDataField<PXAccess.BAccount.defContactID>(), (PXDataField) new PXDataField<PXAccess.BAccount.bAccountID>("BAccount"), (PXDataField) new PXDataFieldValue<Users.pKID>((object) userID.Value)))
        {
          contactID = (int?) pxDataRecord?.GetInt32(1);
          baccountID = (int?) pxDataRecord?.GetInt32(2);
        }
        if (PXSiteMap.IsPortal || contactID.HasValue || baccountID.HasValue)
          return;
        using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<Users>(Yaql.join<PXAccess.Contact>(Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<PXAccess.Contact.userID>((string) null), Yaql.column<Users.pKID>((string) null)), (YaqlJoinType) 0), (PXDataField) new PXDataField<Users.pKID>(), (PXDataField) new PXDataField<PXAccess.Contact.contactID>(), (PXDataField) new PXDataField<PXAccess.Contact.bAccountID>(), (PXDataField) new PXDataFieldValue<Users.pKID>((object) userID.Value)))
        {
          contactID = (int?) pxDataRecord?.GetInt32(1);
          baccountID = (int?) pxDataRecord?.GetInt32(2);
        }
      }
    }

    private void GetAccountTreeByAccount(int? baccountID, out HashSet<int> baccountIDTree)
    {
      if (!baccountID.HasValue)
      {
        baccountIDTree = (HashSet<int>) null;
      }
      else
      {
        baccountIDTree = new HashSet<int>()
        {
          baccountID.Value
        };
        Queue<int> intQueue = new Queue<int>();
        intQueue.Enqueue(baccountID.Value);
        using (new PXConnectionScope())
        {
          while (intQueue.Count != 0)
          {
            foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PXAccess.BAccount>((PXDataField) new PXDataField<PXAccess.BAccount.bAccountID>(), (PXDataField) new PXDataFieldValue<PXAccess.BAccount.parentBAccountID>((object) intQueue.Dequeue())))
            {
              int num = pxDataRecord.GetInt32(0).Value;
              baccountIDTree.Add(num);
              intQueue.Enqueue(num);
            }
          }
        }
      }
    }

    private bool CheckAvailability()
    {
      this.IsAvailable = new bool?(PXDatabase.Provider.SchemaCache.TableExists(typeof (PXAccess.EPEmployee).Name) && PXDatabase.Provider.SchemaCache.TableExists(typeof (PXAccess.BAccount).Name) && PXDatabase.Provider.SchemaCache.TableExists(typeof (PXAccess.Contact).Name));
      return this.IsAvailable.Value;
    }
  }

  private sealed class cacheInitializer
  {
    private List<string> invisible;
    private List<string> disabled;
    private PXCacheRights rights;

    internal cacheInitializer(PXCache cache)
    {
      if (cache.Graph.FullTrust)
        return;
      PXAccess.GetRights(cache, out this.rights, out this.invisible, out this.disabled);
      this.SetRights(cache);
    }

    internal void SetRights(
      PXCache cache,
      PXCacheRights rights,
      List<string> invisible,
      List<string> disabled)
    {
      this.rights = rights;
      this.invisible = invisible;
      this.disabled = disabled;
      this.SetRights(cache);
    }

    private void SetRights(PXCache cache)
    {
      switch (this.rights)
      {
        case PXCacheRights.Select:
          cache.SelectRights = true;
          cache.UpdateRights = false;
          cache.InsertRights = false;
          cache.DeleteRights = false;
          break;
        case PXCacheRights.Update:
          cache.SelectRights = true;
          cache.UpdateRights = true;
          cache.InsertRights = false;
          cache.DeleteRights = false;
          break;
        case PXCacheRights.Insert:
          cache.SelectRights = true;
          cache.UpdateRights = true;
          cache.InsertRights = true;
          cache.DeleteRights = false;
          break;
        case PXCacheRights.Delete:
          cache.SelectRights = true;
          cache.UpdateRights = true;
          cache.InsertRights = true;
          cache.DeleteRights = true;
          break;
        default:
          cache.SelectRights = false;
          cache.UpdateRights = false;
          cache.InsertRights = false;
          cache.DeleteRights = false;
          break;
      }
    }

    internal void Secure(PXCache cache, PXEventSubscriberAttribute attribute)
    {
      if ((cache.Graph == null || cache.Graph.UnattendedMode && !cache.Graph.IsPageGeneratorRequest ? (cache.Graph != null ? 0 : (HttpContext.Current != null ? 1 : 0)) : 1) == 0 || !(attribute is PXUIFieldAttribute pxuiFieldAttribute))
        return;
      if (pxuiFieldAttribute.FieldClass != null && PXAccess.GetRights(pxuiFieldAttribute.FieldClass) == PXCacheRights.Denied)
      {
        pxuiFieldAttribute.ViewRights = false;
        pxuiFieldAttribute.EnableRights = false;
      }
      else
      {
        if (this.invisible == null || !CompareIgnoreCase.IsInList(this.invisible, attribute.FieldName))
        {
          switch (pxuiFieldAttribute.MapViewRights)
          {
            case PXCacheRights.Update:
              pxuiFieldAttribute.ViewRights = cache.UpdateRights;
              break;
            case PXCacheRights.Insert:
              pxuiFieldAttribute.ViewRights = cache.InsertRights;
              break;
            case PXCacheRights.Delete:
              pxuiFieldAttribute.ViewRights = cache.DeleteRights;
              break;
            default:
              pxuiFieldAttribute.ViewRights = cache.SelectRights;
              break;
          }
        }
        else
          pxuiFieldAttribute.ViewRights = false;
        if (this.disabled == null || !CompareIgnoreCase.IsInList(this.disabled, attribute.FieldName))
        {
          switch (pxuiFieldAttribute.MapEnableRights)
          {
            case PXCacheRights.Select:
              pxuiFieldAttribute.EnableRights = cache.SelectRights;
              break;
            case PXCacheRights.Insert:
              pxuiFieldAttribute.EnableRights = cache.InsertRights;
              break;
            case PXCacheRights.Delete:
              pxuiFieldAttribute.EnableRights = cache.DeleteRights;
              break;
            default:
              pxuiFieldAttribute.EnableRights = cache.UpdateRights;
              break;
          }
        }
        else
          pxuiFieldAttribute.EnableRights = false;
      }
      if (pxuiFieldAttribute.ViewRights && pxuiFieldAttribute.EnableRights)
        return;
      cache.SetAltered(pxuiFieldAttribute.FieldName, true);
      if (string.IsNullOrWhiteSpace(pxuiFieldAttribute.FieldName))
        return;
      cache.SecuredFields.Add(pxuiFieldAttribute.FieldName.ToLower());
    }

    internal bool isInvisible(string fieldName)
    {
      List<string> invisible = this.invisible;
      return invisible != null && invisible.Any<string>((Func<string, bool>) (_ => string.Equals(_, fieldName, StringComparison.OrdinalIgnoreCase)));
    }

    internal bool isDisabled(string fieldName)
    {
      List<string> disabled = this.disabled;
      return disabled != null && disabled.Any<string>((Func<string, bool>) (_ => string.Equals(_, fieldName, StringComparison.OrdinalIgnoreCase)));
    }
  }
}
