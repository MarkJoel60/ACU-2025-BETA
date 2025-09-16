// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDatabaseAccessProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using PX.DbServices.QueryObjectModel;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Web.Compilation;
using System.Web.Hosting;

#nullable enable
namespace PX.Data;

/// <exclude />
public class PXDatabaseAccessProvider : PXAccessProvider
{
  protected 
  #nullable disable
  string connectionString;
  protected string companyID;
  protected string administratorRole;
  protected string[] administratorRoles;

  public override void Initialize(string provname, NameValueCollection config)
  {
    if (config == null)
      throw new PXArgumentException(nameof (config), "The argument cannot be null.");
    if (provname == null || provname.Length == 0)
      provname = nameof (PXDatabaseAccessProvider);
    if (string.IsNullOrEmpty(config["description"]))
    {
      config.Remove("description");
      config.Add("description", "PX Access provider");
    }
    base.Initialize(provname, config);
    if (config["applicationName"] == null || config["applicationName"].Trim() == "")
      this.pApplicationName = HostingEnvironment.ApplicationVirtualPath;
    else
      this.pApplicationName = config["applicationName"];
    this.pSessionLimit = config["sessionLimit"];
    this.connectionString = PXAccess.GetConnectionString(config);
    this.companyID = config["companyID"];
    if (config["administratorRole"] == null || !(config["administratorRole"].Trim() != ""))
      return;
    this.administratorRoles = config["administratorRole"].Split(new char[1]
    {
      ','
    }, StringSplitOptions.RemoveEmptyEntries);
  }

  public override string GetSessionLimit() => this.pSessionLimit;

  protected internal override string GetConnectionString() => this.connectionString ?? string.Empty;

  protected internal override string GetCompanyID() => this.companyID;

  protected internal override string[] GetAdministratorRoles()
  {
    if (this.administratorRoles != null && this.administratorRoles.Length != 0)
      return this.administratorRoles;
    return new string[1]{ "Administrator" };
  }

  protected virtual PXDatabaseAccessProvider.Definition Definitions
  {
    get
    {
      return PXContext.GetSlot<PXDatabaseAccessProvider.Definition>() ?? PXContext.SetSlot<PXDatabaseAccessProvider.Definition>(PXDatabase.GetSlot<PXDatabaseAccessProvider.Definition, string>("Definition", this.pApplicationName, PXDatabaseAccessProvider.Definition.Tables));
    }
  }

  public override void Clear()
  {
    PXDatabase.ResetSlot<PXDatabaseAccessProvider.Definition>("Definition", PXDatabaseAccessProvider.Definition.Tables);
    PXContext.SetSlot<PXDatabaseAccessProvider.Definition>((PXDatabaseAccessProvider.Definition) null);
  }

  internal override void ResetContextDefinitions()
  {
    PXContext.SetSlot<PXDatabaseAccessProvider.Definition>((PXDatabaseAccessProvider.Definition) null);
  }

  public override bool UserManagedAssignmentToPrioritizedRoles
  {
    get => this.Definitions != null && this.Definitions.UserManagedAssignmentToPrioritizedRoles;
  }

  protected override Dictionary<string, PXAccessProvider.GraphAccess> Screens
  {
    get
    {
      return this.Definitions == null ? new Dictionary<string, PXAccessProvider.GraphAccess>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase) : this.Definitions._Screens;
    }
  }

  protected override Dictionary<string, Tuple<List<string>, List<string>, List<string>>> ScreenRoles
  {
    get => this.Definitions._ScreenRoles;
  }

  protected override Dictionary<string, PXAccessProvider.GraphAccess> Graphs
  {
    get => this.Definitions._Graphs;
  }

  protected override Dictionary<string, List<string>> GraphRoles => this.Definitions._GraphRoles;

  /// <exclude />
  [PXHidden]
  protected class Roles : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    public readonly string Rolename;

    public Roles(PXDataRecord record) => this.Rolename = record.GetString(0);

    public abstract class applicationName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXDatabaseAccessProvider.Roles.applicationName>
    {
    }

    public abstract class rolename : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXDatabaseAccessProvider.Roles.rolename>
    {
    }
  }

  /// <exclude />
  [PXHidden]
  [DebuggerDisplay("{Rolename,nq} {ScreenID,nq} {AccessRightsEnum,nq}")]
  protected class RolesInGraph : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    public readonly string Rolename;
    public readonly short Accessrights;
    public readonly string ScreenID;

    public PXCacheRights AccessRightsEnum => (PXCacheRights) this.Accessrights;

    public RolesInGraph(PXDataRecord record)
    {
      this.Rolename = record.GetString(0);
      this.Accessrights = record.GetInt16(1).Value;
      this.ScreenID = record.GetString(2);
    }

    public RolesInGraph(string role, short rights, string screen)
    {
      this.Rolename = role;
      this.Accessrights = rights;
      this.ScreenID = screen;
    }

    public abstract class applicationName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXDatabaseAccessProvider.RolesInGraph.applicationName>
    {
    }

    public abstract class rolename : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXDatabaseAccessProvider.RolesInGraph.rolename>
    {
    }
  }

  /// <exclude />
  [PXHidden]
  [DebuggerDisplay("{Rolename,nq} {ScreenID,nq} { Cachetype,nq} {AccessRightsEnum,nq}")]
  protected class RolesInCache : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    public readonly string Rolename;
    public readonly short Accessrights;
    public readonly string ScreenID;
    public readonly string Cachetype;

    public PXCacheRights AccessRightsEnum => (PXCacheRights) this.Accessrights;

    public RolesInCache(PXDataRecord record)
    {
      this.Rolename = record.GetString(0);
      this.Accessrights = record.GetInt16(1).Value;
      this.ScreenID = record.GetString(2);
      this.Cachetype = record.GetString(3);
    }

    public abstract class applicationName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXDatabaseAccessProvider.RolesInCache.applicationName>
    {
    }

    public abstract class rolename : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXDatabaseAccessProvider.RolesInCache.rolename>
    {
    }
  }

  /// <exclude />
  [PXHidden]
  [DebuggerDisplay("{Rolename,nq} {ScreenID,nq} { Cachetype,nq} {Membername,nq} {AccessRightsEnum,nq}")]
  protected sealed class RolesInMember : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    public readonly string Rolename;
    public readonly short Accessrights;
    public readonly string ScreenID;
    public readonly string Cachetype;
    public readonly string Membername;

    public PXMemberRights AccessRightsEnum => (PXMemberRights) this.Accessrights;

    public RolesInMember(PXDataRecord record)
    {
      this.Rolename = record.GetString(0);
      this.Accessrights = record.GetInt16(1).Value;
      this.ScreenID = record.GetString(2);
      this.Cachetype = record.GetString(3);
      this.Membername = record.GetString(4);
    }

    public abstract class applicationName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXDatabaseAccessProvider.RolesInMember.applicationName>
    {
    }

    public abstract class rolename : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXDatabaseAccessProvider.RolesInMember.rolename>
    {
    }
  }

  /// <exclude />
  protected class Definition : IPrefetchable<string>, IPXCompanyDependent, IInternable
  {
    protected static readonly System.Type[] _tables = new System.Type[6]
    {
      typeof (PXDatabaseAccessProvider.RolesInGraph),
      typeof (PXDatabaseAccessProvider.RolesInCache),
      typeof (PXDatabaseAccessProvider.RolesInMember),
      typeof (RoleClaims),
      typeof (PX.SM.SiteMap),
      typeof (PX.SM.PortalMap)
    };
    protected bool PXDatabaseAccessProviderBypass;
    public bool UserManagedAssignmentToPrioritizedRoles;
    protected Dictionary<string, System.Type> CacheTypes = new Dictionary<string, System.Type>();
    private const PXCacheRights NotSetCacheRights = ~PXCacheRights.Denied;
    private bool isInterned;
    private object internObjectLock = new object();
    public Dictionary<string, PXAccessProvider.GraphAccess> _Screens;
    public Dictionary<string, Tuple<List<string>, List<string>, List<string>>> _ScreenRoles;
    public Dictionary<string, PXAccessProvider.GraphAccess> _Graphs;
    public Dictionary<string, List<string>> _GraphRoles;
    protected HashSet<string> _PrioritizedRoles = new HashSet<string>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
    /// <summary>
    /// Estimated number of rows in the <see cref="T:PX.Data.PXDatabaseAccessProvider.RolesInGraph" /> DB table.
    /// </summary>
    /// <remarks>
    /// The median amount of rows in the RolesInGraph DB table calced over 200 DBs from support cases is about <c>30 400</c> rows, the average amount is about <c>64 096</c> rows
    /// The median amount of rows  in the RolesInGraph DB table calced over several versions of SalesDemo is about <c>12 400</c> rows.
    /// The estimated capacity is <c>2^14</c>, a power of two (since the size of dynamic lists starts with <c>4</c> and is doubled on resize, so it will reach this number after some resizes anyway)
    /// It is not too big but can store data for a median Sales Demo and after one resize for the median amount calced from the actual user data.
    /// </remarks>
    private const int _EstimatedRolesInGraphRowsCount = 16384 /*0x4000*/;

    protected internal static System.Type[] Tables => PXDatabaseAccessProvider.Definition._tables;

    protected virtual System.Type GetType(string typeName)
    {
      System.Type type;
      if (!this.CacheTypes.TryGetValue(typeName, out type))
      {
        type = PXBuildManager.GetType(typeName, false);
        this.CacheTypes.Add(typeName, type);
      }
      return type;
    }

    [Obsolete("Use SetGraphRights with multiple roles instead")]
    protected virtual void SetGraphRights(
      string screenID,
      string roleName,
      PXCacheRights rights,
      bool isPrioritized)
    {
      this.SetGraphRights(screenID, EnumerableExtensions.AsSingleEnumerable<string>(roleName), rights, isPrioritized);
    }

    [Obsolete("Use SetCacheRights with multiple roles instead")]
    protected virtual void SetCacheRights(
      string screenID,
      string cacheName,
      string roleName,
      PXCacheRights rights,
      bool isPrioritized)
    {
      this.SetCacheRights(screenID, cacheName, EnumerableExtensions.AsSingleEnumerable<string>(roleName), rights, isPrioritized);
    }

    [Obsolete("Use SetMemberRights with multiple roles instead")]
    protected virtual void SetMemberRights(
      string screenID,
      string cacheName,
      string memberName,
      string roleName,
      PXMemberRights rights,
      bool isPrioritized)
    {
      this.SetMemberRights(screenID, cacheName, memberName, EnumerableExtensions.AsSingleEnumerable<string>(roleName), rights, isPrioritized);
    }

    protected virtual void SetGraphRights(
      string screenID,
      IEnumerable<string> roles,
      PXCacheRights rights,
      bool isPrioritized)
    {
      if (!(roles is IList<string> stringList))
        stringList = (IList<string>) roles.ToList<string>();
      roles = (IEnumerable<string>) stringList;
      EnumerableExtensions.AddRange<string>((ISet<string>) this._PrioritizedRoles, roles);
      List<string> source1 = new List<string>();
      List<string> source2 = new List<string>();
      List<string> source3 = new List<string>();
      PXAccessProvider.GraphAccess screenGraphAccess;
      if (!this._Screens.TryGetValue(screenID, out screenGraphAccess))
        this._Screens[screenID] = screenGraphAccess = new PXAccessProvider.GraphAccess();
      if (isPrioritized)
        screenGraphAccess.AnyPrioritized = true;
      HashSet<string> hashSet = screenGraphAccess.Rights.Where<KeyValuePair<string, PXCacheRightsPrioritized>>((Func<KeyValuePair<string, PXCacheRightsPrioritized>, bool>) (x => !x.Value.Prioritized && x.Value.Rights == ~PXCacheRights.Denied)).Select<KeyValuePair<string, PXCacheRightsPrioritized>, string>((Func<KeyValuePair<string, PXCacheRightsPrioritized>, string>) (x => x.Key)).ToHashSet<string>((IEqualityComparer<string>) PXLocalesProvider.CollationComparer);
      bool flag = false;
      PXCacheRightsPrioritized cacheRights = new PXCacheRightsPrioritized(isPrioritized, rights);
      EnumerableExtensions.ForEach<string>(roles, (System.Action<string>) (x => screenGraphAccess.Rights[x] = cacheRights));
      foreach (KeyValuePair<string, PXCacheRightsPrioritized> right in screenGraphAccess.Rights)
      {
        string str1;
        PXCacheRightsPrioritized rightsPrioritized1;
        EnumerableExtensions.Deconstruct<string, PXCacheRightsPrioritized>(right, ref str1, ref rightsPrioritized1);
        string str2 = str1;
        PXCacheRightsPrioritized rightsPrioritized2 = rightsPrioritized1;
        if (rightsPrioritized2.Prioritized)
        {
          flag = true;
          if (rightsPrioritized2.Rights != PXCacheRights.Denied)
            source2.Add(str2);
        }
        else if (isPrioritized && hashSet.Contains(str2) && rightsPrioritized2.Rights == ~PXCacheRights.Denied)
          source2.Add(str2);
        else if (rightsPrioritized2.Rights != PXCacheRights.Denied)
          source1.Add(str2);
        else
          source3.Add(str2);
      }
      this._ScreenRoles[screenID] = new Tuple<List<string>, List<string>, List<string>>(source1.Any<string>() ? source1 : (List<string>) null, source2.Any<string>() | flag ? source2 : (List<string>) null, source3.Any<string>() ? source3 : (List<string>) null);
      if (!isPrioritized)
        return;
      string graphTypeByScreenId = PXPageIndexingService.GetGraphTypeByScreenID(screenID);
      if (string.IsNullOrEmpty(graphTypeByScreenId))
        return;
      PXAccessProvider.GraphAccess graphAccess;
      if (!this._Graphs.TryGetValue(graphTypeByScreenId, out graphAccess))
        this._Graphs[graphTypeByScreenId] = graphAccess = new PXAccessProvider.GraphAccess();
      EnumerableExtensions.ForEach<string>(roles, (System.Action<string>) (x => graphAccess.Rights[x] = cacheRights));
      this._GraphRoles[graphTypeByScreenId] = source2;
    }

    protected virtual void SetCacheRights(
      string screenID,
      string cacheName,
      IEnumerable<string> roles,
      PXCacheRights rights,
      bool isPrioritized)
    {
      if (!(roles is IList<string> stringList))
        stringList = (IList<string>) roles.ToList<string>();
      roles = (IEnumerable<string>) stringList;
      EnumerableExtensions.AddRange<string>((ISet<string>) this._PrioritizedRoles, roles);
      System.Type type = this.GetType(cacheName);
      if (type == (System.Type) null)
        return;
      PXAccessProvider.GraphAccess graphAccess1;
      if (!this._Screens.TryGetValue(screenID, out graphAccess1))
        this._Screens[screenID] = graphAccess1 = new PXAccessProvider.GraphAccess();
      if (isPrioritized)
        graphAccess1.AnyPrioritized = true;
      PXAccessProvider.CacheAccess screenCacheAccess;
      if (graphAccess1.Caches == null)
        graphAccess1.Caches = new Dictionary<System.Type, PXAccessProvider.CacheAccess>()
        {
          [type] = screenCacheAccess = new PXAccessProvider.CacheAccess()
        };
      else if (!graphAccess1.Caches.TryGetValue(type, out screenCacheAccess))
        graphAccess1.Caches[type] = screenCacheAccess = new PXAccessProvider.CacheAccess();
      PXCacheRightsPrioritized cacheRights = new PXCacheRightsPrioritized(isPrioritized, rights);
      EnumerableExtensions.ForEach<string>(roles, (System.Action<string>) (x => screenCacheAccess.Rights[x] = cacheRights));
      if (!isPrioritized)
        return;
      string graphTypeByScreenId = PXPageIndexingService.GetGraphTypeByScreenID(screenID);
      if (string.IsNullOrEmpty(graphTypeByScreenId))
        return;
      PXAccessProvider.GraphAccess graphAccess2;
      if (!this._Graphs.TryGetValue(graphTypeByScreenId, out graphAccess2))
        this._Graphs[graphTypeByScreenId] = graphAccess2 = new PXAccessProvider.GraphAccess();
      PXAccessProvider.CacheAccess cacheAccess;
      if (graphAccess2.Caches == null)
        graphAccess2.Caches = new Dictionary<System.Type, PXAccessProvider.CacheAccess>()
        {
          [type] = cacheAccess = new PXAccessProvider.CacheAccess()
        };
      else if (!graphAccess2.Caches.TryGetValue(type, out cacheAccess))
        graphAccess2.Caches[type] = cacheAccess = new PXAccessProvider.CacheAccess();
      EnumerableExtensions.ForEach<string>(roles, (System.Action<string>) (x => cacheAccess.Rights[x] = cacheRights));
    }

    protected virtual void SetMemberRights(
      string screenID,
      string cacheName,
      string memberName,
      IEnumerable<string> roles,
      PXMemberRights rights,
      bool isPrioritized)
    {
      if (!(roles is IList<string> stringList))
        stringList = (IList<string>) roles.ToList<string>();
      roles = (IEnumerable<string>) stringList;
      EnumerableExtensions.AddRange<string>((ISet<string>) this._PrioritizedRoles, roles);
      System.Type type = this.GetType(cacheName);
      if (type == (System.Type) null)
        return;
      PXAccessProvider.GraphAccess graphAccess1;
      if (!this._Screens.TryGetValue(screenID, out graphAccess1))
        this._Screens[screenID] = graphAccess1 = new PXAccessProvider.GraphAccess();
      if (isPrioritized)
        graphAccess1.AnyPrioritized = true;
      PXAccessProvider.CacheAccess cacheAccess1;
      if (graphAccess1.Caches == null)
        graphAccess1.Caches = new Dictionary<System.Type, PXAccessProvider.CacheAccess>()
        {
          [type] = cacheAccess1 = new PXAccessProvider.CacheAccess()
        };
      else if (!graphAccess1.Caches.TryGetValue(type, out cacheAccess1))
        graphAccess1.Caches[type] = cacheAccess1 = new PXAccessProvider.CacheAccess();
      PXAccessProvider.MemberAccess screenMemberAccess;
      if (cacheAccess1.Members == null)
        cacheAccess1.Members = new Dictionary<string, PXAccessProvider.MemberAccess>()
        {
          [memberName] = screenMemberAccess = new PXAccessProvider.MemberAccess()
        };
      else if (!cacheAccess1.Members.TryGetValue(memberName, out screenMemberAccess))
        cacheAccess1.Members[memberName] = screenMemberAccess = new PXAccessProvider.MemberAccess();
      PXMemberRightsPrioritized memberRights = new PXMemberRightsPrioritized(isPrioritized, rights);
      EnumerableExtensions.ForEach<string>(roles, (System.Action<string>) (x => screenMemberAccess.Rights[x] = memberRights));
      if (!isPrioritized)
        return;
      string graphTypeByScreenId = PXPageIndexingService.GetGraphTypeByScreenID(screenID);
      if (string.IsNullOrEmpty(graphTypeByScreenId))
        return;
      PXAccessProvider.GraphAccess graphAccess2;
      if (!this._Graphs.TryGetValue(graphTypeByScreenId, out graphAccess2))
        this._Graphs[graphTypeByScreenId] = graphAccess2 = new PXAccessProvider.GraphAccess();
      PXAccessProvider.CacheAccess cacheAccess2;
      if (graphAccess2.Caches == null)
        graphAccess2.Caches = new Dictionary<System.Type, PXAccessProvider.CacheAccess>()
        {
          [type] = cacheAccess2 = new PXAccessProvider.CacheAccess()
        };
      else if (!graphAccess2.Caches.TryGetValue(type, out cacheAccess2))
        graphAccess2.Caches[type] = cacheAccess2 = new PXAccessProvider.CacheAccess();
      PXAccessProvider.MemberAccess memberAccess;
      if (cacheAccess2.Members == null)
        cacheAccess2.Members = new Dictionary<string, PXAccessProvider.MemberAccess>()
        {
          [memberName] = memberAccess = new PXAccessProvider.MemberAccess()
        };
      else if (!cacheAccess2.Members.TryGetValue(memberName, out memberAccess))
        cacheAccess2.Members[memberName] = memberAccess = new PXAccessProvider.MemberAccess();
      EnumerableExtensions.ForEach<string>(roles, (System.Action<string>) (x => memberAccess.Rights[x] = memberRights));
    }

    protected virtual void SetRestrictedTables(string[] list)
    {
      PXDatabase.SetRestrictedTables(list);
    }

    public object Intern()
    {
      PXDatabaseAccessProvider.Definition definition = this;
      PXDatabaseAccessProvider.Definition returnValue1;
      if (new PxObjectsIntern<PXDatabaseAccessProvider.Definition>().TryIntern(definition, out returnValue1))
        definition = returnValue1;
      if (definition.isInterned)
        return (object) definition;
      lock (definition.internObjectLock)
      {
        if (!definition.isInterned)
        {
          PxObjectsIntern<Dictionary<string, PXCacheRightsPrioritized>> pxObjectsIntern1 = new PxObjectsIntern<Dictionary<string, PXCacheRightsPrioritized>>(true);
          foreach (PXAccessProvider.GraphAccess graphAccess in this.GetGraphAccess())
          {
            Dictionary<string, PXCacheRightsPrioritized> returnValue2;
            if (pxObjectsIntern1.TryIntern(graphAccess.Rights, out returnValue2))
              graphAccess.Rights = returnValue2;
          }
          PxObjectsIntern<Dictionary<string, PXMemberRightsPrioritized>> pxObjectsIntern2 = new PxObjectsIntern<Dictionary<string, PXMemberRightsPrioritized>>(true);
          foreach (PXAccessProvider.MemberAccess memberAccess in this.GetMemberAccess())
          {
            Dictionary<string, PXMemberRightsPrioritized> returnValue3;
            if (pxObjectsIntern2.TryIntern(memberAccess.Rights, out returnValue3))
              memberAccess.Rights = returnValue3;
          }
          PxObjectsIntern<Dictionary<string, PXCacheRightsPrioritized>> pxObjectsIntern3 = new PxObjectsIntern<Dictionary<string, PXCacheRightsPrioritized>>(true);
          foreach (PXAccessProvider.CacheAccess cacheAccess in this.GetCacheAccess())
          {
            Dictionary<string, PXCacheRightsPrioritized> returnValue4;
            if (pxObjectsIntern3.TryIntern(cacheAccess.Rights, out returnValue4))
              cacheAccess.Rights = returnValue4;
          }
          PxObjectsIntern<List<string>> pxObjectsIntern4 = new PxObjectsIntern<List<string>>(true);
          foreach (string key in this._ScreenRoles.Keys.ToArray<string>())
          {
            List<string> returnValue5;
            if (pxObjectsIntern4.TryIntern(this._ScreenRoles[key].Item1, out returnValue5))
              this._ScreenRoles[key] = new Tuple<List<string>, List<string>, List<string>>(returnValue5, this._ScreenRoles[key].Item2, this._ScreenRoles[key].Item3);
          }
          definition.isInterned = true;
        }
      }
      return (object) definition;
    }

    private IEnumerable<PXAccessProvider.GraphAccess> GetGraphAccess()
    {
      foreach (KeyValuePair<string, PXAccessProvider.GraphAccess> screen in this._Screens)
        yield return screen.Value;
      foreach (KeyValuePair<string, PXAccessProvider.GraphAccess> graph in this._Graphs)
        yield return graph.Value;
    }

    private IEnumerable<PXAccessProvider.MemberAccess> GetMemberAccess()
    {
      foreach (KeyValuePair<string, PXAccessProvider.GraphAccess> screen in this._Screens)
      {
        if (screen.Value.Caches != null)
        {
          foreach (KeyValuePair<System.Type, PXAccessProvider.CacheAccess> cach in screen.Value.Caches)
          {
            if (cach.Value.Members != null)
            {
              foreach (KeyValuePair<string, PXAccessProvider.MemberAccess> member in cach.Value.Members)
                yield return member.Value;
            }
          }
        }
      }
      foreach (KeyValuePair<string, PXAccessProvider.GraphAccess> graph in this._Graphs)
      {
        if (graph.Value.Caches != null)
        {
          foreach (KeyValuePair<System.Type, PXAccessProvider.CacheAccess> cach in graph.Value.Caches)
          {
            if (cach.Value.Members != null)
            {
              foreach (KeyValuePair<string, PXAccessProvider.MemberAccess> member in cach.Value.Members)
                yield return member.Value;
            }
          }
        }
      }
    }

    private IEnumerable<PXAccessProvider.CacheAccess> GetCacheAccess()
    {
      foreach (KeyValuePair<string, PXAccessProvider.GraphAccess> graph in this._Graphs)
      {
        if (graph.Value.Caches != null)
        {
          foreach (KeyValuePair<System.Type, PXAccessProvider.CacheAccess> cach in graph.Value.Caches)
            yield return cach.Value;
        }
      }
    }

    public virtual void Prefetch(string pApplicationName)
    {
      if (this.PXDatabaseAccessProviderBypass)
        return;
      PXContext.SetSlot<bool>("PrefetchSiteMap", true);
      List<PXDatabaseAccessProvider.RolesInGraph> list = EnumerableExtensions.ToList<PXDatabaseAccessProvider.RolesInGraph>(this.GetUserRolesAccessForGraphs(pApplicationName).Union<PXDatabaseAccessProvider.RolesInGraph>(this.GetDefaultAccessForGraphs(pApplicationName, "*")), 16384 /*0x4000*/);
      List<PXDatabaseAccessProvider.RolesInCache> rolesInCacheList = new List<PXDatabaseAccessProvider.RolesInCache>();
      List<PXDatabaseAccessProvider.RolesInMember> rolesInMemberList = new List<PXDatabaseAccessProvider.RolesInMember>();
      list.Sort((Comparison<PXDatabaseAccessProvider.RolesInGraph>) ((a, b) => !string.Equals(a.ScreenID, b.ScreenID, StringComparison.OrdinalIgnoreCase) ? string.Compare(a.ScreenID, b.ScreenID, StringComparison.OrdinalIgnoreCase) : string.Compare(a.Rolename, b.Rolename)));
      foreach (PXDataRecord record in PXDatabase.SelectMulti<PXDatabaseAccessProvider.RolesInCache>(Yaql.join<PXDatabaseAccessProvider.Roles>(Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<PXDatabaseAccessProvider.RolesInCache.rolename>((string) null), Yaql.column<PXDatabaseAccessProvider.Roles.rolename>((string) null)), (YaqlJoinType) 0), (PXDataField) new PXAliasedDataField<PXDatabaseAccessProvider.RolesInCache.rolename>(), new PXDataField("Accessrights"), new PXDataField("ScreenID"), new PXDataField("Cachetype"), (PXDataField) new PXAliasedDataFieldValue<PXDatabaseAccessProvider.RolesInCache.applicationName>((object) pApplicationName)))
        rolesInCacheList.Add(new PXDatabaseAccessProvider.RolesInCache(record));
      rolesInCacheList.Sort((Comparison<PXDatabaseAccessProvider.RolesInCache>) ((a, b) =>
      {
        if (!string.Equals(a.ScreenID, b.ScreenID, StringComparison.OrdinalIgnoreCase))
          return string.Compare(a.ScreenID, b.ScreenID, StringComparison.OrdinalIgnoreCase);
        return a.Cachetype != b.Cachetype ? string.Compare(a.Cachetype, b.Cachetype) : string.Compare(a.Rolename, b.Rolename);
      }));
      foreach (PXDataRecord record in PXDatabase.SelectMulti<PXDatabaseAccessProvider.RolesInMember>(Yaql.join<PXDatabaseAccessProvider.Roles>(Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<PXDatabaseAccessProvider.RolesInMember.rolename>((string) null), Yaql.column<PXDatabaseAccessProvider.Roles.rolename>((string) null)), (YaqlJoinType) 0), (PXDataField) new PXAliasedDataField<PXDatabaseAccessProvider.RolesInMember.rolename>(), new PXDataField("Accessrights"), new PXDataField("ScreenID"), new PXDataField("Cachetype"), new PXDataField("Membername"), (PXDataField) new PXAliasedDataFieldValue<PXDatabaseAccessProvider.RolesInMember.applicationName>((object) pApplicationName)))
        rolesInMemberList.Add(new PXDatabaseAccessProvider.RolesInMember(record));
      rolesInMemberList.Sort((Comparison<PXDatabaseAccessProvider.RolesInMember>) ((a, b) =>
      {
        if (!string.Equals(a.ScreenID, b.ScreenID, StringComparison.OrdinalIgnoreCase))
          return string.Compare(a.ScreenID, b.ScreenID, StringComparison.OrdinalIgnoreCase);
        if (a.Cachetype != b.Cachetype)
          return string.Compare(a.Cachetype, b.Cachetype);
        return a.Membername != b.Membername ? string.Compare(a.Membername, b.Membername) : string.Compare(a.Rolename, b.Rolename);
      }));
      int index1 = 0;
      PXDatabaseAccessProvider.RolesInGraph rolesInGraph = (PXDatabaseAccessProvider.RolesInGraph) null;
      if (index1 < list.Count && list[index1].ScreenID != null)
        rolesInGraph = list[index1];
      int index2 = 0;
      PXDatabaseAccessProvider.RolesInCache rolesInCache = (PXDatabaseAccessProvider.RolesInCache) null;
      if (index2 < rolesInCacheList.Count)
        rolesInCache = rolesInCacheList[index2];
      int index3 = 0;
      PXDatabaseAccessProvider.RolesInMember rolesInMember = (PXDatabaseAccessProvider.RolesInMember) null;
      if (index3 < rolesInMemberList.Count)
        rolesInMember = rolesInMemberList[index3];
      Dictionary<string, PXAccessProvider.GraphAccess> dictionary1 = new Dictionary<string, PXAccessProvider.GraphAccess>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      Dictionary<string, Tuple<List<string>, List<string>, List<string>>> dictionary2 = new Dictionary<string, Tuple<List<string>, List<string>, List<string>>>((IEqualityComparer<string>) PXLocalesProvider.CollationComparer);
      string str1 = (string) null;
      PXAccessProvider.GraphAccess graphAccess = (PXAccessProvider.GraphAccess) null;
      List<string> stringList1 = (List<string>) null;
      List<string> stringList2 = (List<string>) null;
      for (; rolesInGraph != null; rolesInGraph = index1 >= list.Count ? (PXDatabaseAccessProvider.RolesInGraph) null : list[index1])
      {
        string screenId = rolesInGraph.ScreenID;
        if (screenId != null && string.Equals(screenId, str1, StringComparison.OrdinalIgnoreCase))
        {
          string rolename;
          if (graphAccess != null)
            graphAccess.Rights[rolename = rolesInGraph.Rolename] = new PXCacheRightsPrioritized(false, rolesInGraph.AccessRightsEnum);
          if (rolesInGraph.AccessRightsEnum != PXCacheRights.Denied)
            stringList1.Add(rolename = rolesInGraph.Rolename);
          else
            stringList2.Add(rolename = rolesInGraph.Rolename);
        }
        else
        {
          if (graphAccess != null)
            graphAccess.Rights = new Dictionary<string, PXCacheRightsPrioritized>((IDictionary<string, PXCacheRightsPrioritized>) graphAccess.Rights);
          str1 = screenId;
          graphAccess = (PXAccessProvider.GraphAccess) null;
          stringList1 = new List<string>();
          stringList2 = new List<string>();
          dictionary2[screenId] = new Tuple<List<string>, List<string>, List<string>>(stringList1, (List<string>) null, stringList2);
          string str2 = rolesInGraph.Rolename;
          if (rolesInGraph.AccessRightsEnum != PXCacheRights.Denied)
            stringList1.Add(str2);
          else
            stringList2.Add(str2);
          if (!string.IsNullOrEmpty(str1))
          {
            graphAccess = new PXAccessProvider.GraphAccess();
            dictionary1[str1] = graphAccess;
            graphAccess.Rights[str2] = new PXCacheRightsPrioritized(false, rolesInGraph.AccessRightsEnum);
            for (; rolesInCache != null && string.Compare(str2 = rolesInCache.ScreenID, str1, StringComparison.OrdinalIgnoreCase) < 0; rolesInCache = index2 >= rolesInCacheList.Count ? (PXDatabaseAccessProvider.RolesInCache) null : rolesInCacheList[index2])
              ++index2;
            if (rolesInCache != null && string.Equals(str2, str1, StringComparison.OrdinalIgnoreCase))
            {
              string str3 = (string) null;
              PXAccessProvider.CacheAccess cacheAccess = (PXAccessProvider.CacheAccess) null;
              for (; rolesInCache != null && rolesInCache.ScreenID == str1; rolesInCache = index2 >= rolesInCacheList.Count ? (PXDatabaseAccessProvider.RolesInCache) null : rolesInCacheList[index2])
              {
                string str4 = rolesInCache.Cachetype;
                if (str4 == str3)
                {
                  cacheAccess.Rights[rolesInCache.Rolename] = new PXCacheRightsPrioritized(false, rolesInCache.AccessRightsEnum);
                }
                else
                {
                  System.Type type = PXBuildManager.GetType(str4, false);
                  if (type != (System.Type) null)
                  {
                    str3 = str4;
                    if (graphAccess.Caches == null)
                      graphAccess.Caches = new Dictionary<System.Type, PXAccessProvider.CacheAccess>();
                    cacheAccess = new PXAccessProvider.CacheAccess();
                    graphAccess.Caches[type] = cacheAccess;
                    cacheAccess.Rights[rolesInCache.Rolename] = new PXCacheRightsPrioritized(false, rolesInCache.AccessRightsEnum);
                    for (; rolesInMember != null && (string.Compare(str4 = rolesInMember.ScreenID, str1, StringComparison.OrdinalIgnoreCase) < 0 || string.Equals(str4, str1, StringComparison.OrdinalIgnoreCase) && string.Compare(rolesInMember.Cachetype, str3, StringComparison.OrdinalIgnoreCase) < 0); rolesInMember = index3 >= rolesInMemberList.Count ? (PXDatabaseAccessProvider.RolesInMember) null : rolesInMemberList[index3])
                      ++index3;
                    if (rolesInMember != null && string.Equals(str4, str1, StringComparison.OrdinalIgnoreCase) && string.Equals(rolesInMember.Cachetype, str3, StringComparison.OrdinalIgnoreCase))
                    {
                      string str5 = (string) null;
                      PXAccessProvider.MemberAccess memberAccess = (PXAccessProvider.MemberAccess) null;
                      for (; rolesInMember != null && string.Equals(rolesInMember.ScreenID, str1, StringComparison.OrdinalIgnoreCase) && rolesInMember.Cachetype == str3; rolesInMember = index3 >= rolesInMemberList.Count ? (PXDatabaseAccessProvider.RolesInMember) null : rolesInMemberList[index3])
                      {
                        string membername = rolesInMember.Membername;
                        if (string.Equals(membername, str5, StringComparison.OrdinalIgnoreCase))
                        {
                          memberAccess.Rights[rolesInMember.Rolename] = new PXMemberRightsPrioritized(false, rolesInMember.AccessRightsEnum);
                        }
                        else
                        {
                          str5 = membername;
                          if (cacheAccess.Members == null)
                            cacheAccess.Members = new Dictionary<string, PXAccessProvider.MemberAccess>();
                          memberAccess = new PXAccessProvider.MemberAccess();
                          cacheAccess.Members[str5] = memberAccess;
                          memberAccess.Rights[rolesInMember.Rolename] = new PXMemberRightsPrioritized(false, rolesInMember.AccessRightsEnum);
                        }
                        ++index3;
                      }
                    }
                  }
                }
                ++index2;
              }
            }
          }
        }
        ++index1;
      }
      this._Screens = dictionary1;
      this._ScreenRoles = dictionary2;
      this._Graphs = new Dictionary<string, PXAccessProvider.GraphAccess>();
      this._GraphRoles = new Dictionary<string, List<string>>();
      PXContext.SetSlot<bool>("PrefetchSiteMap", false);
    }

    private IEnumerable<PXDatabaseAccessProvider.RolesInGraph> GetUserRolesAccessForGraphs(
      string pApplicationName)
    {
      return PXDatabase.SelectMulti<PXDatabaseAccessProvider.RolesInGraph>(Yaql.join<PXDatabaseAccessProvider.Roles>(Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column<PXDatabaseAccessProvider.RolesInGraph.rolename>((string) null), Yaql.column<PXDatabaseAccessProvider.Roles.rolename>((string) null)), (YaqlJoinType) 0), (PXDataField) new PXAliasedDataField<PXDatabaseAccessProvider.RolesInGraph.rolename>(), new PXDataField("Accessrights"), new PXDataField("ScreenID"), (PXDataField) new PXAliasedDataFieldValue<PXDatabaseAccessProvider.RolesInGraph.applicationName>((object) pApplicationName)).Select<PXDataRecord, PXDatabaseAccessProvider.RolesInGraph>((Func<PXDataRecord, PXDatabaseAccessProvider.RolesInGraph>) (record => new PXDatabaseAccessProvider.RolesInGraph(record)));
    }

    private IEnumerable<PXDatabaseAccessProvider.RolesInGraph> GetDefaultAccessForGraphs(
      string pApplicationName,
      string defaultRole)
    {
      return PXDatabase.SelectMulti<PXDatabaseAccessProvider.RolesInGraph>((PXDataField) new PXDataField<PXDatabaseAccessProvider.RolesInGraph.rolename>(), new PXDataField("Accessrights"), new PXDataField("ScreenID"), (PXDataField) new PXDataFieldValue<PXDatabaseAccessProvider.RolesInGraph.applicationName>((object) pApplicationName), (PXDataField) new PXDataFieldValue<PXDatabaseAccessProvider.RolesInGraph.rolename>((object) defaultRole)).Select<PXDataRecord, PXDatabaseAccessProvider.RolesInGraph>((Func<PXDataRecord, PXDatabaseAccessProvider.RolesInGraph>) (record => new PXDatabaseAccessProvider.RolesInGraph(record)));
    }
  }
}
