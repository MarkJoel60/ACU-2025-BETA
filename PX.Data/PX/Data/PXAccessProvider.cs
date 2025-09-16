// Decompiled with JetBrains decompiler
// Type: PX.Data.PXAccessProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Api;
using PX.Common;
using PX.Security;
using PX.Security.Authorization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Security.Principal;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class PXAccessProvider : ProviderBase, ISystemRolesProvider, IPredefinedRolesProvider
{
  protected const string DEFAULT_ADMINISTRATOR_ROLE = "Administrator";
  protected const string DEFAULT_PORTAL_ADMINISTRATOR_ROLE = "Portal Admin";
  protected const string DEFAULT_ARCHIVIST_ROLE = "Archivist";
  protected Dictionary<string, string> GraphTypes = new Dictionary<string, string>();
  protected string pApplicationName;
  protected string pSessionLimit;

  public virtual byte[] InstallationID => (byte[]) null;

  public virtual bool UserManagedAssignmentToPrioritizedRoles => false;

  protected virtual Dictionary<string, PXAccessProvider.GraphAccess> Screens
  {
    get => (Dictionary<string, PXAccessProvider.GraphAccess>) null;
  }

  internal virtual bool IsScreenHiddenByFeature(string screenID) => false;

  protected virtual Dictionary<string, Tuple<List<string>, List<string>, List<string>>> ScreenRoles
  {
    get => (Dictionary<string, Tuple<List<string>, List<string>, List<string>>>) null;
  }

  protected virtual Dictionary<string, PXAccessProvider.GraphAccess> Graphs
  {
    get => (Dictionary<string, PXAccessProvider.GraphAccess>) null;
  }

  protected virtual Dictionary<string, List<string>> GraphRoles
  {
    get => (Dictionary<string, List<string>>) null;
  }

  public string ApplicationName
  {
    get => this.pApplicationName;
    set => this.pApplicationName = value;
  }

  public virtual PXCacheRights GetRights(PXCache cache)
  {
    PXCacheRights rights;
    this.GetRights(cache, out rights, out List<string> _, out List<string> _);
    return rights;
  }

  public virtual void GetRights(
    PXCache cache,
    out PXCacheRights rights,
    out List<string> invisible,
    out List<string> disabled)
  {
    this.GetRights(cache.Graph.Accessinfo.ScreenID?.Replace(".", ""), CustomizedTypeManager.GetTypeNotCustomized(cache.Graph).FullName, cache.GetItemType(), out rights, out invisible, out disabled);
  }

  public virtual PXCacheRights GetCacheRights(
    string graphScreenID,
    string graphName,
    System.Type cacheType)
  {
    PXCacheRights rights;
    this.GetRights(graphScreenID, graphName, cacheType, true, out rights, out List<string> _, out List<string> _, false);
    return rights;
  }

  public virtual void GetRights(
    string graphScreenID,
    string graphName,
    System.Type cacheType,
    out PXCacheRights rights,
    out List<string> invisible,
    out List<string> disabled)
  {
    this.GetRights(graphScreenID, graphName, cacheType, false, out rights, out invisible, out disabled, true);
  }

  /// <param name="skipGraphRights">If true, returns cache rights even if access to the screen is denied.</param>
  private void GetRights(
    string graphScreenID,
    string graphName,
    System.Type cacheType,
    bool getCacheRights,
    out PXCacheRights rights,
    out List<string> invisible,
    out List<string> disabled,
    bool fillLists)
  {
    invisible = (List<string>) null;
    disabled = (List<string>) null;
    if (!string.IsNullOrEmpty(graphScreenID))
    {
      PXSiteMapNode[] array = PXSiteMap.Provider.FindSiteMapNodesByScreenIDUnsecure(graphScreenID).ToArray<PXSiteMapNode>();
      if (array.Length != 0)
      {
        IListProvider prov = PXList.Provider;
        PXSiteMapNode pxSiteMapNode = ((IEnumerable<PXSiteMapNode>) array).FirstOrDefault<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (n => prov.IsList(n.ScreenID)));
        if (pxSiteMapNode != null)
        {
          PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(PXList.Provider.GetEntryScreenID(pxSiteMapNode.ScreenID));
          if (screenIdUnsecure != null)
          {
            this.GetRights(screenIdUnsecure.ScreenID, screenIdUnsecure.GraphType, cacheType, out rights, out invisible, out disabled);
            return;
          }
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (!getCacheRights && ((IEnumerable<PXSiteMapNode>) array).Any<PXSiteMapNode>(PXAccessProvider.\u003C\u003EO.\u003C0\u003E__IsDashboard ?? (PXAccessProvider.\u003C\u003EO.\u003C0\u003E__IsDashboard = new Func<PXSiteMapNode, bool>(PXSiteMap.IsDashboard))))
        {
          rights = PXCacheRights.Delete;
          return;
        }
      }
    }
    Dictionary<string, PXAccessProvider.GraphAccess> screens = this.Screens;
    if (screens == null)
    {
      rights = PXCacheRights.Delete;
    }
    else
    {
      rights = PXCacheRights.Denied;
      if (string.IsNullOrEmpty(graphScreenID))
      {
        rights = PXCacheRights.Delete;
      }
      else
      {
        PXAccessProvider.GraphAccess common = (PXAccessProvider.GraphAccess) null;
        Dictionary<string, PXAccessProvider.GraphAccess> graphs = this.Graphs;
        if (!screens.ContainsKey(graphScreenID))
        {
          string key;
          if (!this.GraphTypes.TryGetValue(graphScreenID, out key))
            key = graphName;
          if (graphs == null || string.IsNullOrEmpty(key) || !graphs.TryGetValue(key, out common) || common == null || common.Rights.Count == 0)
          {
            rights = PXCacheRights.Delete;
            if (common == null || common.Caches.Count == 0)
              return;
          }
        }
        else
        {
          common = screens[graphScreenID];
          if (!common.AnyPrioritized)
          {
            string key;
            if (!this.GraphTypes.TryGetValue(graphScreenID, out key))
              key = graphName;
            if (graphs != null && !string.IsNullOrEmpty(key) && graphs.ContainsKey(key))
              common = new PXAccessProvider.GraphAccess(common, graphs[key]);
          }
        }
        IPrincipal user = PXContext.PXIdentity.AuthUser;
        if (user == null)
          return;
        Dictionary<string, bool> knownUserRoles = new Dictionary<string, bool>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        ref PXCacheRights local = ref rights;
        PXCacheRights? nullable1 = FindRights<PXCacheRightsPrioritized, PXCacheRights>(common.Rights, (Func<PXCacheRightsPrioritized, PXCacheRights>) (x => x.Rights), (Func<PXCacheRightsPrioritized, bool>) (x => x.Prioritized), (Func<PXCacheRights, byte>) (x => (byte) x), PXCacheRights.Denied);
        int num = (int) nullable1 ?? (int) rights;
        local = (PXCacheRights) num;
        PXAccessProvider.CacheAccess cacheAccess;
        if (rights == PXCacheRights.Denied && !getCacheRights || common.Caches == null || !(cacheType != (System.Type) null) || !common.Caches.TryGetValue(cacheType, out cacheAccess))
          return;
        PXCacheRights? rights1 = FindRights<PXCacheRightsPrioritized, PXCacheRights>(cacheAccess.Rights, (Func<PXCacheRightsPrioritized, PXCacheRights>) (x => x.Rights), (Func<PXCacheRightsPrioritized, bool>) (x => x.Prioritized), (Func<PXCacheRights, byte>) (x => (byte) x), PXCacheRights.Denied);
        if (rights1.HasValue)
        {
          nullable1 = rights1;
          PXCacheRights pxCacheRights = rights;
          if (nullable1.GetValueOrDefault() < pxCacheRights & nullable1.HasValue | getCacheRights)
            rights = rights1.Value;
        }
        if (rights == PXCacheRights.Denied || !fillLists || cacheAccess.Members == null)
          return;
        PXCollationComparer collationComparer = PXLocalesProvider.CollationComparer;
        OrderedHashSet<string> source1 = new OrderedHashSet<string>((IEqualityComparer<string>) collationComparer);
        OrderedHashSet<string> source2 = new OrderedHashSet<string>((IEqualityComparer<string>) collationComparer);
        foreach (KeyValuePair<string, PXAccessProvider.MemberAccess> member in cacheAccess.Members)
        {
          PXMemberRights? rights2 = FindRights<PXMemberRightsPrioritized, PXMemberRights>(member.Value.Rights, (Func<PXMemberRightsPrioritized, PXMemberRights>) (x => x.Rights), (Func<PXMemberRightsPrioritized, bool>) (x => x.Prioritized), (Func<PXMemberRights, byte>) (x => (byte) x), PXMemberRights.Denied);
          PXMemberRights? nullable2 = rights2;
          PXMemberRights pxMemberRights1 = PXMemberRights.Denied;
          if (nullable2.GetValueOrDefault() == pxMemberRights1 & nullable2.HasValue)
          {
            source1.Add(member.Key);
            source2.Add(member.Key);
          }
          else
          {
            nullable2 = rights2;
            PXMemberRights pxMemberRights2 = PXMemberRights.Visible;
            if (nullable2.GetValueOrDefault() == pxMemberRights2 & nullable2.HasValue)
              source2.Add(member.Key);
          }
        }
        if (((IEnumerable<string>) source1).Any<string>())
          invisible = ((IEnumerable<string>) source1).ToList<string>();
        if (!((IEnumerable<string>) source2).Any<string>())
          return;
        disabled = ((IEnumerable<string>) source2).ToList<string>();
      }
    }

    TEnum? FindRights<TPrioritized, TEnum>(
      Dictionary<string, TPrioritized> rightsDictionary,
      Func<TPrioritized, TEnum> enumSelector,
      Func<TPrioritized, bool> prioritizedSelector,
      Func<TEnum, byte> byteConverter,
      TEnum deniedValue)
      where TEnum : struct
    {
      TEnum? rights = new TEnum?();
      List<KeyValuePair<string, TPrioritized>> list = rightsDictionary.OrderByDescending<KeyValuePair<string, TPrioritized>, TEnum>((Func<KeyValuePair<string, TPrioritized>, TEnum>) (x => enumSelector(x.Value))).ToList<KeyValuePair<string, TPrioritized>>();
      bool flag1 = list.Any<KeyValuePair<string, TPrioritized>>((Func<KeyValuePair<string, TPrioritized>, bool>) (x => prioritizedSelector(x.Value)));
      bool flag2 = list.Any<KeyValuePair<string, TPrioritized>>((Func<KeyValuePair<string, TPrioritized>, bool>) (x => !prioritizedSelector(x.Value)));
      TEnum? nullable1 = new TEnum?();
      TEnum? nullable2 = new TEnum?();
      (TEnum? nullable3, TEnum? nullable4) = GetUniversalRights<TPrioritized, TEnum>(rightsDictionary, enumSelector, prioritizedSelector);
      foreach (KeyValuePair<string, TPrioritized> keyValuePair in list)
      {
        string str;
        TPrioritized prioritized1;
        EnumerableExtensions.Deconstruct<string, TPrioritized>(keyValuePair, ref str, ref prioritized1);
        string roleName = str;
        TPrioritized prioritized2 = prioritized1;
        ref PXAccessProvider.\u003C\u003Ec__DisplayClass29_0 local = ref obj5;
        if (PXAccessProvider.\u003CGetRights\u003Eg__IsInRole\u007C29_2(roleName, ref local))
        {
          if (!prioritizedSelector(prioritized2))
            nullable1 = new TEnum?(enumSelector(prioritized2));
          else
            nullable2 = new TEnum?(enumSelector(prioritized2));
        }
        if (nullable1.HasValue || !flag2)
        {
          if (!nullable2.HasValue)
          {
            if (!flag1)
              break;
          }
          else
            break;
        }
      }
      if (!nullable2.HasValue & flag1)
        nullable2 = new TEnum?(deniedValue);
      string[] array = rightsDictionary.Keys.ToArray<string>();
      string[] userRoles = GetUserRoles();
      if (nullable2.HasValue)
      {
        rights = !nullable4.HasValue || (int) byteConverter(nullable4.Value) < (int) byteConverter(nullable2.Value) ? nullable2 : new TEnum?(GetActualRights<TEnum>(array, userRoles, nullable4.Value, nullable2.Value, byteConverter));
        if (nullable1.HasValue && nullable3.HasValue && (int) byteConverter(nullable3.Value) > (int) byteConverter(nullable1.Value))
          nullable1 = new TEnum?(GetActualRights<TEnum>(array, userRoles, nullable3.Value, nullable1.Value, byteConverter));
        if (nullable1.HasValue && (int) byteConverter(nullable1.Value) < (int) byteConverter(rights.Value))
          rights = nullable1;
      }
      else if (nullable1.HasValue)
      {
        rights = nullable1;
        TEnum universalAccessRights = nullable4 ?? nullable3 ?? deniedValue;
        if ((int) byteConverter(universalAccessRights) > (int) byteConverter(rights.Value))
          rights = new TEnum?(GetActualRights<TEnum>(array, userRoles, universalAccessRights, rights.Value, byteConverter));
      }
      else if (nullable4.HasValue)
        rights = nullable4;
      else if (nullable3.HasValue)
        rights = nullable3;
      return rights;
    }

    static (TEnum?, TEnum?) GetUniversalRights<TPrioritized, TEnum>(
      Dictionary<string, TPrioritized> rightsDictionary,
      Func<TPrioritized, TEnum> enumSelector,
      Func<TPrioritized, bool> prioritizedSelector)
      where TEnum : struct
    {
      TEnum? nullable1 = new TEnum?();
      TEnum? nullable2 = new TEnum?();
      TPrioritized prioritized;
      if (!rightsDictionary.TryGetValue("*", out prioritized))
        return (new TEnum?(), new TEnum?());
      if (prioritizedSelector(prioritized))
        nullable2 = new TEnum?(enumSelector(prioritized));
      else
        nullable1 = new TEnum?(enumSelector(prioritized));
      return (nullable1, nullable2);
    }

    static string[] GetUserRoles()
    {
      return ServiceLocator.Current.GetInstance<IRoleManagementService>().GetRolesForUser(PXContext.PXIdentity.IdentityName);
    }

    static TEnum GetActualRights<TEnum>(
      string[] allUsedRoles,
      string[] userRoles,
      TEnum universalAccessRights,
      TEnum currentAccessRights,
      Func<TEnum, byte> byteConverter)
      where TEnum : struct
    {
      return ((IEnumerable<string>) userRoles).Any<string>((Func<string, bool>) (role => !((IEnumerable<string>) allUsedRoles).Contains<string>(role, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))) && (int) byteConverter(universalAccessRights) > (int) byteConverter(currentAccessRights) ? universalAccessRights : currentAccessRights;
    }
  }

  public virtual string[] FieldClassRestricted => (string[]) null;

  public virtual PXCacheRights GetRights(string fieldClass) => PXCacheRights.Delete;

  public virtual bool IsRoleEnabled(string role) => true;

  public virtual bool IsSchedulesEnabled() => true;

  public virtual bool IsScreenApiEnabled(string screenId) => true;

  public virtual bool IsScreenMobileEnabled(string screenId) => true;

  internal bool IsScreenDisabled(string screenID)
  {
    PXAccessProvider.GraphAccess graphAccess;
    return !string.IsNullOrEmpty(screenID) && this.Screens.TryGetValue(screenID, out graphAccess) && graphAccess.Rights.Values.Any<PXCacheRightsPrioritized>((Func<PXCacheRightsPrioritized, bool>) (_ => _.Prioritized)) && !graphAccess.Rights.Values.Any<PXCacheRightsPrioritized>((Func<PXCacheRightsPrioritized, bool>) (_ => _.Prioritized && _.Rights > PXCacheRights.Denied));
  }

  public PXCacheRights GetRights(string roleName, string screenID)
  {
    if (string.IsNullOrEmpty(screenID))
      return PXCacheRights.Delete;
    PXAccessProvider.GraphAccess common;
    if (!this.Screens.ContainsKey(screenID))
    {
      string key;
      if (this.Graphs == null || !this.GraphTypes.TryGetValue(screenID, out key) || string.IsNullOrEmpty(key) || !this.Graphs.TryGetValue(key, out common))
        return PXCacheRights.Delete;
    }
    else
    {
      common = this.Screens[screenID];
      string key;
      if (!common.AnyPrioritized && this.Graphs != null && this.GraphTypes.TryGetValue(screenID, out key) && !string.IsNullOrEmpty(key) && this.Graphs.ContainsKey(key))
        common = new PXAccessProvider.GraphAccess(common, this.Graphs[key]);
    }
    foreach (KeyValuePair<string, PXCacheRightsPrioritized> right in common.Rights)
    {
      if (right.Key == roleName)
        return right.Value.Rights;
    }
    return PXCacheRights.Denied;
  }

  public PXRoleList GetRoles(string screenID)
  {
    Tuple<List<string>, List<string>, List<string>> tuple;
    return this.ScreenRoles != null && this.ScreenRoles.TryGetValue(screenID, out tuple) ? new PXRoleList(tuple.Item1, tuple.Item2, tuple.Item3) : (PXRoleList) null;
  }

  public PXRoleList RegisterGraphType(string graphType, string screenID)
  {
    if (!string.IsNullOrEmpty(screenID))
    {
      if (!string.IsNullOrEmpty(graphType))
      {
        lock (((ICollection) this.GraphTypes).SyncRoot)
          this.GraphTypes[screenID] = graphType;
        List<string> common = (List<string>) null;
        List<string> commonDenied = (List<string>) null;
        List<string> prioritized;
        if (this.GraphRoles != null && this.GraphRoles.TryGetValue(graphType, out prioritized))
        {
          Tuple<List<string>, List<string>, List<string>> tuple;
          if (this.ScreenRoles != null && this.ScreenRoles.TryGetValue(screenID, out tuple))
          {
            common = tuple.Item1;
            commonDenied = tuple.Item3;
          }
          return new PXRoleList(common, prioritized, commonDenied);
        }
      }
      return this.GetRoles(screenID);
    }
    List<string> prioritized1;
    return this.GraphRoles != null && this.GraphRoles.TryGetValue(graphType, out prioritized1) ? new PXRoleList((List<string>) null, prioritized1, (List<string>) null) : (PXRoleList) null;
  }

  public virtual string GetSessionLimit() => string.Empty;

  protected internal virtual string GetConnectionString() => string.Empty;

  /// <summary>
  /// This method should be overridden in a derived classes if multi-database environment is needed.
  /// </summary>
  protected internal virtual IPXMultiDatabaseUser[] GetMultiDatabaseUsers()
  {
    return (IPXMultiDatabaseUser[]) null;
  }

  protected internal virtual string GetCompanyID() => string.Empty;

  IEnumerable<string> ISystemRolesProvider.GetAdministratorRoles()
  {
    return (IEnumerable<string>) this.GetAdministratorRoles();
  }

  string ISystemRolesProvider.GetPortalAdministratorRole() => this.GetPortalAdministratorRole();

  string ISystemRolesProvider.GetAdministratorRole() => this.GetAdministratorRole();

  string ISystemRolesProvider.GetArchivistRole() => this.GetArchivistRole();

  IEnumerable<string> IPredefinedRolesProvider.Roles
  {
    get
    {
      return ((IEnumerable<string>) this.GetAdministratorRoles()).Concat<string>((IEnumerable<string>) new string[2]
      {
        this.GetPortalAdministratorRole(),
        this.GetArchivistRole()
      });
    }
  }

  protected internal virtual string GetAdministratorRole()
  {
    string[] administratorRoles = this.GetAdministratorRoles();
    return administratorRoles == null || administratorRoles.Length == 0 ? "Administrator" : administratorRoles[0];
  }

  protected internal virtual string[] GetAdministratorRoles()
  {
    return new string[1]{ "Administrator" };
  }

  protected internal virtual string GetPortalAdministratorRole() => "Portal Admin";

  protected internal virtual string GetArchivistRole() => "Archivist";

  public virtual bool BypassLicense => false;

  public virtual bool IsUnlimitedUser() => false;

  public virtual bool IsMultiDbMode => false;

  public virtual HashSet<string> AllFeatures => (HashSet<string>) null;

  public virtual void Clear()
  {
  }

  internal virtual void ResetContextDefinitions()
  {
  }

  public virtual bool FeatureInstalled(string feature) => false;

  public virtual bool FeatureSetInstalled(string featureSet) => true;

  public virtual bool FeatureReadOnly(string feature) => false;

  /// <summary>
  /// Returns the localization that corresponds to the requested feature.
  /// See <see cref="T:PX.Data.Localization.ILocalizationFeaturesService">ILocalizationFeaturesService</see> for additional information.
  /// </summary>
  /// <param name="feature">The name of the feature according to <see cref="P:PX.Data.PXAccessProvider.AllFeatures">PXAccessProvider</see></param>
  /// <returns>The unique ID of the localization that consists of capital letters</returns>
  public virtual string GetLocalizationCodeForFeature(string feature) => (string) null;

  /// <summary>
  /// Returns localizations available on the website.
  /// See <see cref="T:PX.Data.Localization.ILocalizationFeaturesService">ILocalizationFeaturesService</see> for additional information.
  /// </summary>
  /// <returns>A list of localizations represented by unique IDs that consist of capital letters.</returns>
  public virtual IEnumerable<string> GetEnabledLocalizations() => (IEnumerable<string>) null;

  public virtual bool IsStringListValueDisabled(string cacheName, string fieldName, string value)
  {
    return false;
  }

  internal virtual PXCache.FieldDefaultingDelegate GetDefaultingDelegate(System.Type type)
  {
    return (PXCache.FieldDefaultingDelegate) null;
  }

  /// <exclude />
  protected sealed class GraphAccess
  {
    public Dictionary<string, PXCacheRightsPrioritized> Rights = new Dictionary<string, PXCacheRightsPrioritized>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    public Dictionary<System.Type, PXAccessProvider.CacheAccess> Caches;
    public bool AnyPrioritized;

    public GraphAccess()
    {
    }

    public GraphAccess(
      PXAccessProvider.GraphAccess common,
      PXAccessProvider.GraphAccess prioritized)
    {
      foreach (KeyValuePair<string, PXCacheRightsPrioritized> right in common.Rights)
        this.Rights[right.Key] = right.Value;
      foreach (KeyValuePair<string, PXCacheRightsPrioritized> right in prioritized.Rights)
        this.Rights[right.Key] = right.Value;
      if (common.Caches == null || common.Caches.Count == 0)
      {
        if (prioritized.Caches == null)
          return;
        this.Caches = prioritized.Caches;
      }
      else if (prioritized.Caches == null || prioritized.Caches.Count == 0)
      {
        this.Caches = common.Caches;
      }
      else
      {
        this.Caches = new Dictionary<System.Type, PXAccessProvider.CacheAccess>();
        foreach (KeyValuePair<System.Type, PXAccessProvider.CacheAccess> cach in common.Caches)
          this.Caches[cach.Key] = prioritized.Caches.ContainsKey(cach.Key) ? new PXAccessProvider.CacheAccess(cach.Value, prioritized.Caches[cach.Key]) : cach.Value;
        foreach (KeyValuePair<System.Type, PXAccessProvider.CacheAccess> cach in prioritized.Caches)
        {
          if (!common.Caches.ContainsKey(cach.Key))
            this.Caches[cach.Key] = cach.Value;
        }
      }
    }
  }

  /// <exclude />
  protected sealed class CacheAccess
  {
    public Dictionary<string, PXCacheRightsPrioritized> Rights = new Dictionary<string, PXCacheRightsPrioritized>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, PXAccessProvider.MemberAccess> Members = new Dictionary<string, PXAccessProvider.MemberAccess>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

    public CacheAccess()
    {
    }

    public CacheAccess(
      PXAccessProvider.CacheAccess common,
      PXAccessProvider.CacheAccess prioritized)
    {
      foreach (KeyValuePair<string, PXCacheRightsPrioritized> right in common.Rights)
        this.Rights[right.Key] = right.Value;
      foreach (KeyValuePair<string, PXCacheRightsPrioritized> right in prioritized.Rights)
        this.Rights[right.Key] = right.Value;
      if (common.Members == null || common.Members.Count == 0)
      {
        if (prioritized.Members == null)
          return;
        this.Members = prioritized.Members;
      }
      else if (prioritized.Members == null || prioritized.Members.Count == 0)
      {
        this.Members = common.Members;
      }
      else
      {
        this.Members = new Dictionary<string, PXAccessProvider.MemberAccess>();
        foreach (KeyValuePair<string, PXAccessProvider.MemberAccess> member in common.Members)
          this.Members[member.Key] = prioritized.Members.ContainsKey(member.Key) ? new PXAccessProvider.MemberAccess(member.Value, prioritized.Members[member.Key]) : member.Value;
        foreach (KeyValuePair<string, PXAccessProvider.MemberAccess> member in prioritized.Members)
        {
          if (!common.Members.ContainsKey(member.Key))
            this.Members[member.Key] = member.Value;
        }
      }
    }
  }

  /// <exclude />
  protected sealed class MemberAccess
  {
    public Dictionary<string, PXMemberRightsPrioritized> Rights = new Dictionary<string, PXMemberRightsPrioritized>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

    public MemberAccess()
    {
    }

    public MemberAccess(
      PXAccessProvider.MemberAccess common,
      PXAccessProvider.MemberAccess prioritized)
    {
      foreach (KeyValuePair<string, PXMemberRightsPrioritized> right in common.Rights)
        this.Rights[right.Key] = right.Value;
      foreach (KeyValuePair<string, PXMemberRightsPrioritized> right in prioritized.Rights)
        this.Rights[right.Key] = right.Value;
    }
  }
}
