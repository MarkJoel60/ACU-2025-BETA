// Decompiled with JetBrains decompiler
// Type: PX.Data.UserOrganizationService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Access.ActiveDirectory;
using PX.DbServices.QueryObjectModel;
using PX.Security;
using PX.SM;
using PX.SP;
using PX.SP.Alias;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

#nullable disable
namespace PX.Data;

internal class UserOrganizationService : 
  IUserOrganizationService,
  IUserBranchSlotControl,
  PX.Data.Internal.IUserOrganizationService
{
  private readonly IActiveDirectoryProvider _activeDirectoryProvider;
  private readonly IUserManagementService _userManagementService;
  private readonly ILogger _logger;
  private readonly IPortalDescriptor _portalDescriptor;
  private const string BRANCH_COLLECTION_KEY = "_BRANCH_COLLECTION_KEY";

  public UserOrganizationService(
    IActiveDirectoryProvider activeDirectoryProvider,
    IUserManagementService userManagementService,
    ILogger logger,
    IPortalDescriptor portalDescriptor)
  {
    this._activeDirectoryProvider = activeDirectoryProvider;
    this._userManagementService = userManagementService;
    this._logger = logger;
    this._portalDescriptor = portalDescriptor;
  }

  private UserOrganizationService.BranchCollectionBase Fetch()
  {
    HashSet<int?> nullableSet1 = new HashSet<int?>();
    foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PXAccess.Branch>((PXDataField) new PXDataField<PXAccess.Branch.branchID>(), (PXDataField) new PXDataField<PXAccess.Branch.branchCD>(), (PXDataField) new PXDataField<PXAccess.Branch.roleName>(), (PXDataField) new PXDataFieldValue<PXAccess.Branch.active>(PXDbType.Bit, (object) 1)))
      nullableSet1.Add(new int?(pxDataRecord.GetInt32(0).Value));
    Dictionary<string, List<BranchInfo>> branchesByRole = new Dictionary<string, List<BranchInfo>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    List<BranchInfo> branchInfoList1 = new List<BranchInfo>();
    bool flag1 = true;
    using (new PXReadBranchRestrictedScope())
    {
      using (new PXReadDeletedScope())
      {
        foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<PXAccess.Branch>(Yaql.join<PXAccess.BAccount>(Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<PXAccess.BAccount.bAccountID>((string) null), Yaql.column<PXAccess.Branch.bAccountID>((string) null)), (YaqlJoinType) 0), (PXDataField) new PXDataField<PXAccess.Branch.branchID>(), (PXDataField) new PXDataField<PXAccess.Branch.branchCD>(), (PXDataField) new PXDataField<PXAccess.Branch.roleName>(), (PXDataField) new PXDataField<PXAccess.BAccount.acctName>()))
        {
          string key = pxDataRecord.GetString(2);
          if (string.IsNullOrWhiteSpace(key))
            key = "";
          else
            flag1 = false;
          List<BranchInfo> branchInfoList2;
          if (!branchesByRole.TryGetValue(key, out branchInfoList2))
            branchesByRole[key] = branchInfoList2 = new List<BranchInfo>();
          int? int32 = pxDataRecord.GetInt32(0);
          int id = int32.Value;
          string cd = pxDataRecord.GetString(1);
          string name = pxDataRecord.GetString(3);
          HashSet<int?> nullableSet2 = nullableSet1;
          int32 = pxDataRecord.GetInt32(0);
          int? nullable = new int?(int32.Value);
          int num = !nullableSet2.Contains(nullable) ? 1 : 0;
          BranchInfo branchInfo = new BranchInfo(id, cd, name, num != 0);
          branchInfoList2.Add(branchInfo);
          branchInfoList1.Add(branchInfo);
        }
      }
    }
    if (flag1 && !PortalHelper.IsPortalContext(PortalContexts.Modern))
      return branchesByRole.Count == 0 ? this.EmptyObjectObtained("branchesByRole") : (UserOrganizationService.BranchCollectionBase) new UserOrganizationService.NoRolesBranchCollection((IEnumerable<BranchInfo>) branchesByRole[""]);
    Dictionary<string, HashSet<string>> userRoles = new Dictionary<string, HashSet<string>>((IEqualityComparer<string>) PXLocalesProvider.CollationComparer);
    Dictionary<string, HashSet<string>> userSids = new Dictionary<string, HashSet<string>>((IEqualityComparer<string>) PXLocalesProvider.CollationComparer);
    foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<UsersInRoles>((PXDataField) new PXDataField<UsersInRoles.rolename>(), (PXDataField) new PXDataField<UsersInRoles.username>()))
    {
      string str = pxDataRecord.GetString(0);
      string userName = pxDataRecord.GetString(1);
      if (!string.IsNullOrWhiteSpace(str) && !string.IsNullOrWhiteSpace(userName))
        this.AddValueForUser(userRoles, userName, str);
    }
    if (this._activeDirectoryProvider.IsEnabled())
    {
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<Users>((PXDataField) new PXDataField<Users.username>(), (PXDataField) new PXDataField<Users.extRef>(), (PXDataField) new PXDataFieldValue<Users.source>(PXDbType.Int, (object) 1), (PXDataField) new PXDataFieldValue<Users.overrideADRoles>(PXDbType.Bit, new int?(1), (object) 1, PXComp.NE)))
      {
        string userName = pxDataRecord.GetString(0);
        string str = pxDataRecord.GetString(1);
        if (!string.IsNullOrWhiteSpace(userName))
          this.AddValueForUser(userSids, userName, str);
      }
    }
    Dictionary<string, Func<BranchInfo[]>> branchesByUser = new Dictionary<string, Func<BranchInfo[]>>((IEqualityComparer<string>) PXLocalesProvider.CollationComparer);
    foreach (string str1 in userRoles.Keys.Concat<string>((IEnumerable<string>) userSids.Keys).Distinct<string>((IEqualityComparer<string>) PXLocalesProvider.CollationComparer))
    {
      string userName = str1;
      branchesByUser[userName] = (Func<BranchInfo[]>) (() =>
      {
        HashSet<string> stringSet1;
        if (!userRoles.TryGetValue(userName, out stringSet1))
          stringSet1 = new HashSet<string>((IEqualityComparer<string>) PXLocalesProvider.CollationComparer);
        HashSet<string> stringSet2;
        if (userSids.TryGetValue(userName, out stringSet2))
        {
          foreach (string sid in stringSet2)
          {
            foreach (string str2 in this._activeDirectoryProvider.GetADMappedRolesBySID(sid))
              stringSet1.Add(str2);
          }
        }
        HashSet<int> knownBranches = new HashSet<int>();
        List<BranchInfo> branchInfoList3 = new List<BranchInfo>();
        foreach (string key in stringSet1)
        {
          List<BranchInfo> source;
          if (branchesByRole.TryGetValue(key, out source))
          {
            foreach (BranchInfo branchInfo in source.Where<BranchInfo>((Func<BranchInfo, bool>) (x => !knownBranches.Contains(x.Id))))
            {
              knownBranches.Add(branchInfo.Id);
              branchInfoList3.Add(branchInfo);
            }
          }
        }
        return branchInfoList3.ToArray();
      });
    }
    if (branchesByUser.Count == 0)
      return this.EmptyObjectObtained("branchesByUser");
    Dictionary<int, BranchInfo[]> branchesByPortal = this.GetBranchesByPortal(branchInfoList1);
    bool flag2 = false;
    try
    {
      flag2 = PXSiteMap.IsPortal;
    }
    catch
    {
    }
    if (!flag2 || WebConfig.PortalSiteID == null)
      return (UserOrganizationService.BranchCollectionBase) new UserOrganizationService.BranchCollection((IReadOnlyDictionary<string, Func<BranchInfo[]>>) branchesByUser, (IReadOnlyDictionary<int, BranchInfo[]>) branchesByPortal);
    List<BranchInfo> branchInfoList4 = new List<BranchInfo>();
    PXDataRecord pxDataRecord1 = PXDatabase.SelectSingle<PXAccess.PortalSetup>((PXDataField) new PXDataField<PXAccess.PortalSetup.displayFinancialDocuments>(), (PXDataField) new PXDataField<PXAccess.PortalSetup.restrictByOrganizationID>(), (PXDataField) new PXDataField<PXAccess.PortalSetup.restrictByBranchID>(), (PXDataField) new PXDataFieldValue<PXAccess.PortalSetup.portalSetupID>(PXDbType.NVarChar, (object) WebConfig.PortalSiteID));
    if (pxDataRecord1 != null)
    {
      try
      {
        string str = pxDataRecord1.GetString(0);
        int? int32 = pxDataRecord1.GetInt32(1);
        int? restrictByBranchID = pxDataRecord1.GetInt32(2);
        switch (str)
        {
          case "A":
            branchInfoList4.AddRange((IEnumerable<BranchInfo>) branchInfoList1);
            break;
          case "B":
            branchInfoList4.AddRange(branchInfoList1.Where<BranchInfo>((Func<BranchInfo, bool>) (_ =>
            {
              int id = _.Id;
              int? nullable = restrictByBranchID;
              int valueOrDefault = nullable.GetValueOrDefault();
              return id == valueOrDefault & nullable.HasValue;
            })));
            break;
          case "C":
            HashSet<int> branchIDs = new HashSet<int>((IEnumerable<int>) (PXAccess.GetChildBranchIDs(int32, false) ?? Array.Empty<int>()));
            branchInfoList4.AddRange(branchInfoList1.Where<BranchInfo>((Func<BranchInfo, bool>) (_ => branchIDs.Contains(_.Id))));
            break;
        }
      }
      finally
      {
        ((IDisposable) pxDataRecord1).Dispose();
      }
    }
    return (UserOrganizationService.BranchCollectionBase) new UserOrganizationService.BranchCollectionWithGuest(this._userManagementService, (IReadOnlyDictionary<string, Func<BranchInfo[]>>) branchesByUser, (IReadOnlyDictionary<int, BranchInfo[]>) new Dictionary<int, BranchInfo[]>()
    {
      {
        0,
        branchInfoList4.ToArray()
      }
    });
  }

  private Dictionary<int, BranchInfo[]> GetBranchesByPortal(List<BranchInfo> allBranches)
  {
    Dictionary<int, BranchInfo[]> branchesByPortal = new Dictionary<int, BranchInfo[]>();
    foreach (PortalInfo portal in this._portalDescriptor.GetPortals())
      branchesByPortal[portal.ID.GetValueOrDefault()] = portal.BranchRestrictions.Select<int, BranchInfo>((Func<int, BranchInfo>) (r => allBranches.FirstOrDefault<BranchInfo>((Func<BranchInfo, bool>) (b => b.Id == r)))).Where<BranchInfo>((Func<BranchInfo, bool>) (_ => _ != null)).ToArray<BranchInfo>();
    return branchesByPortal;
  }

  private UserOrganizationService.BranchCollectionBase EmptyObjectObtained(string objectName)
  {
    this._logger.Error<string>("Empty object {ObjectName} obtained while trying to prepare branches list", objectName);
    return (UserOrganizationService.BranchCollectionBase) null;
  }

  private void AddValueForUser(
    Dictionary<string, HashSet<string>> userDictionary,
    string userName,
    string value)
  {
    HashSet<string> stringSet;
    if (userDictionary.TryGetValue(userName, out stringSet))
      stringSet.Add(value);
    else
      userDictionary[userName] = new HashSet<string>((IEnumerable<string>) new string[1]
      {
        value
      }, (IEqualityComparer<string>) PXLocalesProvider.CollationComparer);
  }

  private System.Type[] DBTables
  {
    get
    {
      bool flag = false;
      try
      {
        flag = PXSiteMap.IsPortal;
      }
      catch
      {
      }
      return flag ? new System.Type[4]
      {
        typeof (PXAccess.Branch),
        typeof (UsersInRoles),
        typeof (Users),
        typeof (PXAccess.PortalSetup)
      } : (!this._activeDirectoryProvider.IsEnabled() ? new System.Type[3]
      {
        typeof (PXAccess.Branch),
        typeof (SPPortal),
        typeof (UsersInRoles)
      } : new System.Type[4]
      {
        typeof (PXAccess.Branch),
        typeof (SPPortal),
        typeof (UsersInRoles),
        typeof (Users)
      });
    }
  }

  private IEnumerable<BranchInfo> GetBranches(
    string userName,
    bool? isGuest,
    bool onlyActive,
    bool skipRoleValidation = false)
  {
    if (!string.IsNullOrEmpty(userName))
    {
      UserOrganizationService.BranchCollectionBase slot = PXDatabase.Provider.GetSlot<UserOrganizationService.BranchCollectionBase>("_BRANCH_COLLECTION_KEY", new PrefetchDelegate<UserOrganizationService.BranchCollectionBase>(this.Fetch), this.DBTables);
      if (slot != null)
      {
        BranchInfo[] forUser = slot.GetForUser(userName, new bool?(((int) isGuest ?? (this._userManagementService.GetUser(userName).IsGuest() ? 1 : 0)) != 0), PortalHelper.GetPortalID());
        return !onlyActive ? (IEnumerable<BranchInfo>) forUser : ((IEnumerable<BranchInfo>) forUser).Where<BranchInfo>((Func<BranchInfo, bool>) (x => !x.Deleted));
      }
    }
    return (IEnumerable<BranchInfo>) new BranchInfo[0];
  }

  IEnumerable<BranchInfo> IUserOrganizationService.GetBranches(string userName, bool onlyActive)
  {
    return this.GetBranches(userName, new bool?(), onlyActive);
  }

  IEnumerable<BranchInfo> PX.Data.Internal.IUserOrganizationService.GetBranches(
    string userName,
    bool isGuest,
    bool onlyActive)
  {
    return this.GetBranches(userName, new bool?(isGuest), onlyActive);
  }

  private IEnumerable<int> GetBranchesWithParents(string userName, bool? isGuest, bool onlyActive)
  {
    HashSet<int> branchesWithParents = new HashSet<int>()
    {
      0
    };
    foreach (BranchInfo branch1 in this.GetBranches(userName, isGuest, onlyActive))
    {
      PXAccess.MasterCollection.Branch branch2 = PXAccess.GetBranch(new int?(branch1.Id));
      HashSet<int> intSet1 = branchesWithParents;
      int? baccountId = branch2.Organization.BAccountID;
      int num1 = baccountId.Value;
      if (!intSet1.Contains(num1))
      {
        HashSet<int> intSet2 = branchesWithParents;
        baccountId = branch2.Organization.BAccountID;
        int num2 = baccountId.Value;
        intSet2.Add(num2);
        if (branch2.Organization.Parents.Count > 0)
        {
          foreach (PXAccess.MasterCollection.Organization parent in branch2.Organization.Parents)
          {
            HashSet<int> intSet3 = branchesWithParents;
            baccountId = parent.BAccountID;
            int num3 = baccountId.Value;
            if (!intSet3.Contains(num3))
            {
              HashSet<int> intSet4 = branchesWithParents;
              baccountId = parent.BAccountID;
              int num4 = baccountId.Value;
              intSet4.Add(num4);
            }
          }
        }
      }
      if (!branchesWithParents.Contains(branch2.BAccountID))
        branchesWithParents.Add(branch2.BAccountID);
    }
    return (IEnumerable<int>) branchesWithParents;
  }

  IEnumerable<int> IUserOrganizationService.GetBranchesWithParents(string userName, bool onlyActive)
  {
    return this.GetBranchesWithParents(userName, new bool?(), onlyActive);
  }

  IEnumerable<int> PX.Data.Internal.IUserOrganizationService.GetBranchesWithParents(
    string userName,
    bool isGuest,
    bool onlyActive)
  {
    return this.GetBranchesWithParents(userName, new bool?(isGuest), onlyActive);
  }

  private IEnumerable<PXAccess.MasterCollection.Organization> GetOrganizations(
    string userName,
    bool? isGuest,
    bool onlyActive,
    bool skipGroups)
  {
    HashSet<int> branchIds = this.GetBranches(userName, isGuest, onlyActive).Select<BranchInfo, int>((Func<BranchInfo, int>) (b => b.Id)).ToHashSet<int>();
    Dictionary<string, PXAccess.MasterCollection.Organization> clones = new Dictionary<string, PXAccess.MasterCollection.Organization>();
    foreach (PXAccess.MasterCollection.Organization organization1 in (IEnumerable<PXAccess.MasterCollection.Organization>) PXAccess.MasterBranches.OrganizationsByCD.Values.OrderByDescending<PXAccess.MasterCollection.Organization, bool>((Func<PXAccess.MasterCollection.Organization, bool>) (_ => _.IsGroup)))
    {
      PXAccess.MasterCollection.Organization organization2 = new PXAccess.MasterCollection.Organization();
      organization2.OrganizationID = organization1.OrganizationID;
      organization2.OrganizationCD = organization1.OrganizationCD;
      organization2.OrganizationName = organization1.OrganizationName;
      organization2.BAccountID = organization1.BAccountID;
      organization2.DeletedDatabaseRecord = organization1.DeletedDatabaseRecord;
      organization2.IsGroup = organization1.IsGroup;
      organization2.IsChildOrganization = organization1.IsChildOrganization;
      PXAccess.MasterCollection.Organization organization3 = organization2;
      clones[organization3.OrganizationCD] = organization3;
    }
    List<PXAccess.MasterCollection.Organization> organizations = new List<PXAccess.MasterCollection.Organization>();
    foreach (PXAccess.MasterCollection.Organization organization4 in PXAccess.MasterBranches.OrganizationsByCD.Values.OrderByDescending<PXAccess.MasterCollection.Organization, bool>((Func<PXAccess.MasterCollection.Organization, bool>) (_ => _.IsGroup)).Where<PXAccess.MasterCollection.Organization>((Func<PXAccess.MasterCollection.Organization, bool>) (o =>
    {
      if (!onlyActive)
        return true;
      bool? deletedDatabaseRecord = o.DeletedDatabaseRecord;
      bool flag = true;
      return !(deletedDatabaseRecord.GetValueOrDefault() == flag & deletedDatabaseRecord.HasValue);
    })))
    {
      PXAccess.MasterCollection.Organization copy = PXCache<PXAccess.MasterCollection.Organization>.CreateCopy(organization4);
      List<PXAccess.MasterCollection.Branch> list = copy.ChildBranches.Where<PXAccess.MasterCollection.Branch>((Func<PXAccess.MasterCollection.Branch, bool>) (b => branchIds.Contains(b.BranchID))).ToList<PXAccess.MasterCollection.Branch>();
      if (list.Count > 0 && (!skipGroups && !copy.IsChildOrganization || skipGroups && !copy.IsGroup))
      {
        PXAccess.MasterCollection.Organization organization5 = clones[copy.OrganizationCD];
        organization5.ChildBranches = list;
        organization5.PrimaryParent = copy.PrimaryParent != null ? clones[copy.PrimaryParent.OrganizationCD] : (PXAccess.MasterCollection.Organization) null;
        organization5.Parents = copy.Parents.Select<PXAccess.MasterCollection.Organization, PXAccess.MasterCollection.Organization>((Func<PXAccess.MasterCollection.Organization, PXAccess.MasterCollection.Organization>) (s => clones[s.OrganizationCD])).ToList<PXAccess.MasterCollection.Organization>();
        if (copy.IsGroup)
        {
          foreach (PXAccess.MasterCollection.Organization childOrganization in copy.ChildOrganizations)
          {
            PXAccess.MasterCollection.Organization organization6 = clones[childOrganization.OrganizationCD];
            organization6.PrimaryParent = childOrganization.PrimaryParent != null ? clones[childOrganization.PrimaryParent.OrganizationCD] : (PXAccess.MasterCollection.Organization) null;
            organization6.ChildBranches = childOrganization.ChildBranches.Where<PXAccess.MasterCollection.Branch>((Func<PXAccess.MasterCollection.Branch, bool>) (b => branchIds.Contains(b.BranchID))).ToList<PXAccess.MasterCollection.Branch>();
            if (organization6.ChildBranches.Count > 0)
              organization5.ChildOrganizations.Add(organization6);
          }
        }
        organizations.Add(organization5);
      }
    }
    return (IEnumerable<PXAccess.MasterCollection.Organization>) organizations;
  }

  IEnumerable<PXAccess.MasterCollection.Organization> IUserOrganizationService.GetOrganizations(
    string userName,
    bool onlyActive,
    bool skipGroups)
  {
    return this.GetOrganizations(userName, new bool?(), onlyActive, skipGroups);
  }

  IEnumerable<PXAccess.MasterCollection.Organization> PX.Data.Internal.IUserOrganizationService.GetOrganizations(
    string userName,
    bool isGuest,
    bool onlyActive,
    bool skipGroups)
  {
    return this.GetOrganizations(userName, new bool?(isGuest), onlyActive, skipGroups);
  }

  void IUserBranchSlotControl.Reset()
  {
    PXDatabase.ResetSlot<UserOrganizationService.BranchCollectionBase>("_BRANCH_COLLECTION_KEY", this.DBTables);
  }

  private class BranchCollectionBase
  {
    public virtual BranchInfo[] GetForUser(string userName, bool? isGuest, int? portalID)
    {
      return Array.Empty<BranchInfo>();
    }
  }

  private class BranchCollection : UserOrganizationService.BranchCollectionBase
  {
    private readonly IReadOnlyDictionary<string, Func<BranchInfo[]>> _branchesByUser;
    private readonly IReadOnlyDictionary<int, BranchInfo[]> _branchesByPortal;
    private readonly IReadOnlyList<BranchInfo> _allBranches;
    private readonly ConcurrentDictionary<string, BranchInfo[]> _cache = new ConcurrentDictionary<string, BranchInfo[]>((IEqualityComparer<string>) PXLocalesProvider.CollationComparer);

    internal BranchCollection(
      IReadOnlyDictionary<string, Func<BranchInfo[]>> branchesByUser,
      IReadOnlyDictionary<int, BranchInfo[]> branchesByPortal)
    {
      this._branchesByUser = branchesByUser;
      this._branchesByPortal = branchesByPortal;
    }

    public override BranchInfo[] GetForUser(string userName, bool? _, int? portalID)
    {
      BranchInfo[] branchInfoArray;
      Func<BranchInfo[]> func;
      return portalID.HasValue && this._branchesByPortal.TryGetValue(portalID.Value, out branchInfoArray) ? branchInfoArray : this._cache.GetOrAdd(userName, (Func<string, BranchInfo[]>) (key => !this._branchesByUser.TryGetValue(key, out func) ? Array.Empty<BranchInfo>() : func()));
    }
  }

  private sealed class BranchCollectionWithGuest : UserOrganizationService.BranchCollection
  {
    private readonly IReadOnlyDictionary<int, BranchInfo[]> _guestBranches;
    private readonly IReadOnlyDictionary<string, Func<BranchInfo[]>> _branchesByUser;
    private readonly IUserManagementService _userManagementService;
    private readonly ConcurrentDictionary<string, BranchInfo[]> _cache = new ConcurrentDictionary<string, BranchInfo[]>((IEqualityComparer<string>) PXLocalesProvider.CollationComparer);

    internal BranchCollectionWithGuest(
      IUserManagementService userManagementService,
      IReadOnlyDictionary<string, Func<BranchInfo[]>> branchesByUser,
      IReadOnlyDictionary<int, BranchInfo[]> guestBranches)
      : base(branchesByUser, guestBranches)
    {
      this._userManagementService = userManagementService;
      this._guestBranches = guestBranches;
      this._branchesByUser = branchesByUser;
    }

    public override BranchInfo[] GetForUser(string userName, bool? isGuest, int? portalID)
    {
      if (!isGuest.GetValueOrDefault())
      {
        Func<BranchInfo[]> func;
        return this._cache.GetOrAdd(userName, (Func<string, BranchInfo[]>) (key => !this._branchesByUser.TryGetValue(key, out func) ? Array<BranchInfo>.Empty : func()));
      }
      return !this._guestBranches.Any<KeyValuePair<int, BranchInfo[]>>() ? Array.Empty<BranchInfo>() : this._guestBranches.First<KeyValuePair<int, BranchInfo[]>>().Value;
    }
  }

  private sealed class NoRolesBranchCollection : UserOrganizationService.BranchCollectionBase
  {
    private readonly BranchInfo[] _branches;

    internal NoRolesBranchCollection(IEnumerable<BranchInfo> branches)
    {
      this._branches = branches.ToArray<BranchInfo>();
    }

    public override BranchInfo[] GetForUser(string _, bool? __, int? ___) => this._branches;
  }
}
