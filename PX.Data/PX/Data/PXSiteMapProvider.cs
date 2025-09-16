// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSiteMapProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using PX.Common;
using PX.Common.Context;
using PX.Security;
using PX.SP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;

#nullable disable
namespace PX.Data;

/// <summary>Base class for all site map provders</summary>
public abstract class PXSiteMapProvider
{
  private readonly bool _constrainAdminAccess;
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly IRoleManagementService _roleManagementService;
  private readonly string _slotName;
  private readonly string _siteMapResolveCurrentNodeRecursionCheckKey = "GetSiteMapResolveCurrentNode_RecursionCheck_" + Guid.NewGuid().ToString();

  protected PXSiteMapProvider(
    IOptions<PXSiteMapOptions> options,
    IHttpContextAccessor httpContextAccessor,
    IRoleManagementService roleManagementService)
  {
    this._httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof (httpContextAccessor));
    this._roleManagementService = roleManagementService ?? throw new ArgumentNullException(nameof (roleManagementService));
    PXSiteMapOptions pxSiteMapOptions = options.Value;
    this._constrainAdminAccess = pxSiteMapOptions.ConstrainAdminAccess;
    this.SecurityTrimmingEnabled = pxSiteMapOptions.SecurityTrimmingEnabled;
    this._slotName = this.GetType().Name + Guid.NewGuid().ToString();
  }

  /// <summary>
  /// The root node of the provider.
  /// Can be <see langword="null" /> if the current user doesn't have permissions to view the node.
  /// </summary>
  /// <exception cref="T:PX.Data.PXException">Occurs when the root node is not set</exception>
  [PXInternalUseOnly]
  public PXSiteMapNode RootNode
  {
    get
    {
      if (this.Definitions == null)
        return (PXSiteMapNode) null;
      PXSiteMapNode siteMapNode = this.Definitions.RootNode != null ? this.Definitions.RootNode : throw new PXException("The related site map root node is missing.");
      return !siteMapNode.IsAccessibleToUser() ? (PXSiteMapNode) null : siteMapNode;
    }
  }

  /// <summary>
  /// Indicates whether the current user permissions are checked in some methods
  /// </summary>
  internal bool SecurityTrimmingEnabled { get; }

  private PXSiteMapNode LatestCurrentNode
  {
    get => PXContext.GetSlot<PXSiteMapNode>("PXSiteMapCurrentNode");
    set => PXContext.SetSlot<PXSiteMapNode>("PXSiteMapCurrentNode", value);
  }

  /// <summary>The current node of the provider</summary>
  public PXSiteMapNode CurrentNode
  {
    get
    {
      return this.GetSiteMapResolveCurrentNode() ?? this.GetCurrentContextNode() ?? this.LatestCurrentNode;
    }
  }

  protected internal virtual PXSiteMapProvider.Definition Definitions
  {
    get
    {
      string slotName = this._slotName;
      return PXContext.GetSlot<PXSiteMapProvider.Definition>(slotName + Thread.CurrentThread.CurrentUICulture.Name) ?? PXContext.SetSlot<PXSiteMapProvider.Definition>(slotName + Thread.CurrentThread.CurrentUICulture.Name, this.GetSlot(slotName));
    }
  }

  [PXInternalUseOnly]
  [Obsolete]
  public event PXSiteMapProvider.PXSiteMapResolveEventHandler SiteMapResolve;

  private PXSiteMapNode GetSiteMapResolveCurrentNode()
  {
    PXSiteMapProvider.PXSiteMapResolveEventHandler siteMapResolve = this.SiteMapResolve;
    if (siteMapResolve == null)
      return (PXSiteMapNode) null;
    ISlotStore instance = SlotStore.Instance;
    if (instance.Get<object>(this._siteMapResolveCurrentNodeRecursionCheckKey) != null)
      return (PXSiteMapNode) null;
    instance.Set(this._siteMapResolveCurrentNodeRecursionCheckKey, new object());
    try
    {
      foreach (PXSiteMapProvider.PXSiteMapResolveEventHandler invocation in siteMapResolve.GetInvocationList())
      {
        PXSiteMapNode resolveCurrentNode = invocation((object) this, EventArgs.Empty);
        if (resolveCurrentNode != null)
          return resolveCurrentNode;
      }
    }
    finally
    {
      instance.Remove(this._siteMapResolveCurrentNodeRecursionCheckKey);
    }
    return (PXSiteMapNode) null;
  }

  private PXSiteMapNode GetCurrentContextNode()
  {
    HttpContext httpContext = this._httpContextAccessor.HttpContext;
    if (httpContext == null)
      return (PXSiteMapNode) null;
    HttpRequest request = httpContext.Request;
    return this.FindSiteMapNode(UriHelper.BuildRelative(request.PathBase, request.Path, request.QueryString, new FragmentString())) ?? this.FindSiteMapNode(UriHelper.BuildRelative(request.PathBase, request.Path, new QueryString(), new FragmentString()));
  }

  /// <summary>Sets the current node.</summary>
  /// <param name="node">The node to be set as current</param>
  /// <remarks>
  /// <see cref="P:PX.Data.PXSiteMapProvider.CurrentNode" /> property won't return this value until
  /// the current http context has it's own current node
  /// </remarks>
  [PXInternalUseOnly]
  public void SetCurrentNode(PXSiteMapNode node) => this.LatestCurrentNode = node;

  /// <summary>Clear used slots</summary>
  public void Clear()
  {
    this.ResetSlot(this._slotName);
    this.ClearThreadSlot();
  }

  /// <summary>Clear context slots for all locales</summary>
  [PXInternalUseOnly]
  public void ClearThreadSlot()
  {
    SlotStore.Remove((Func<string, object, bool>) ((key, _) => key != null && key.StartsWith(this._slotName, StringComparison.Ordinal)));
  }

  protected abstract PXSiteMapProvider.Definition GetSlot(string slotName);

  protected abstract void ResetSlot(string slotName);

  /// <summary>Finds site map node by its URL</summary>
  /// <param name="rawUrl">The URL of the node</param>
  /// <returns>The site map node or <see langword="null" /> if it's not found or the user doesn't have access to the node</returns>
  [PXInternalUseOnly]
  public virtual PXSiteMapNode FindSiteMapNode(string rawUrl)
  {
    PXSiteMapProvider.Definition definitions = this.Definitions;
    return definitions == null ? (PXSiteMapNode) null : definitions.FindSiteMapNode(rawUrl?.ToLower(), true);
  }

  /// <summary>Finds site map node by its identifier</summary>
  /// <param name="key">The node identifier</param>
  /// <returns>The site map node or <see langword="null" /> if it's not found or the user doesn't have access to the node</returns>
  [PXInternalUseOnly]
  public PXSiteMapNode FindSiteMapNodeFromKey(Guid key) => this.FindSiteMapNodeByGuidKey(key, true);

  /// <summary>Finds site map node by screen identifier</summary>
  /// <param name="key">The screen identifier</param>
  /// <returns>The site map node or <see langword="null" /> if it's not found or the user doesn't have access to the node</returns>
  public PXSiteMapNode FindSiteMapNodeByScreenID(string screenID)
  {
    return this.FindSiteMapNodeByScreenID(screenID, true);
  }

  /// <summary>
  /// Checks whether the node is accessible to the current user
  /// </summary>
  /// <param name="node">The node to be checked</param>
  /// <returns><see langword="true" /> if the user can view the node, otherwise <see langword="false" /></returns>
  public virtual bool IsAccessibleToUser(PXSiteMapNode node)
  {
    PXSiteMapNode pxSiteMapNode = node;
    if (pxSiteMapNode != null && PXList.Provider.IsList(pxSiteMapNode.ScreenID))
    {
      string entryScreenId = PXList.Provider.GetEntryScreenID(pxSiteMapNode.ScreenID);
      if (entryScreenId != null)
      {
        PXSiteMapNode screenIdUnsecure = this.FindSiteMapNodeByScreenIDUnsecure(entryScreenId);
        return screenIdUnsecure != null && this.IsAccessibleToUser(screenIdUnsecure);
      }
    }
    string screenId = node.ScreenID;
    if (screenId == "HD000000" || screenId == "00000000")
      return true;
    if (node.Roles == null)
      return this.IsWiki(node);
    PXRoleList roles = node.PXRoles;
    if (pxSiteMapNode == null || roles == null)
      return node.Roles.Any<string>((Func<string, bool>) (role => PXSiteMapProvider.IsUserInRole(role) || PXSiteMapProvider.IsUniversalRoleInERP(role)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (roles.Common != null && ((IEnumerable<string>) roles.Common).Any<string>(PXSiteMapProvider.\u003C\u003EO.\u003C0\u003E__IsUserInRole ?? (PXSiteMapProvider.\u003C\u003EO.\u003C0\u003E__IsUserInRole = new Func<string, bool>(PXSiteMapProvider.IsUserInRole))) && (roles.Prioritized == null || ((IEnumerable<string>) roles.Prioritized).Any<string>(PXSiteMapProvider.\u003C\u003EO.\u003C0\u003E__IsUserInRole ?? (PXSiteMapProvider.\u003C\u003EO.\u003C0\u003E__IsUserInRole = new Func<string, bool>(PXSiteMapProvider.IsUserInRole)))))
      return true;
    if (roles.Prioritized == null)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return (roles.CommonDenied == null || !((IEnumerable<string>) roles.CommonDenied).Any<string>(PXSiteMapProvider.\u003C\u003EO.\u003C0\u003E__IsUserInRole ?? (PXSiteMapProvider.\u003C\u003EO.\u003C0\u003E__IsUserInRole = new Func<string, bool>(PXSiteMapProvider.IsUserInRole))) || !((IEnumerable<string>) this._roleManagementService.GetRolesForUser(PXContext.PXIdentity.IdentityName)).All<string>((Func<string, bool>) (role => ((IEnumerable<string>) roles.CommonDenied).Contains<string>(role, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)))) && PXSiteMapProvider.AnyUniversalRole(roles.Common);
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if ((roles.Common == null || PXAccess.Provider.UserManagedAssignmentToPrioritizedRoles) && roles.CommonDenied != null && ((IEnumerable<string>) roles.CommonDenied).Any<string>(PXSiteMapProvider.\u003C\u003EO.\u003C0\u003E__IsUserInRole ?? (PXSiteMapProvider.\u003C\u003EO.\u003C0\u003E__IsUserInRole = new Func<string, bool>(PXSiteMapProvider.IsUserInRole))))
      return false;
    foreach (string str in roles.Prioritized)
    {
      string prioritized = str;
      if (this._constrainAdminAccess)
      {
        if (PXSiteMapProvider.IsUserInRole(prioritized) && (roles.Common == null || PXAccess.Provider.UserManagedAssignmentToPrioritizedRoles) && ((IEnumerable<string>) PXAccess.GetAdministratorRoles()).Any<string>((Func<string, bool>) (t => string.Equals(t, prioritized, StringComparison.InvariantCultureIgnoreCase))))
          return true;
      }
      else if (PXSiteMapProvider.IsUserInRole(prioritized) && (roles.Common == null || PXAccess.Provider.UserManagedAssignmentToPrioritizedRoles || ((IEnumerable<string>) PXAccess.GetAdministratorRoles()).Any<string>((Func<string, bool>) (t => string.Equals(t, prioritized, StringComparison.InvariantCultureIgnoreCase)))))
        return true;
    }
    return PXSiteMapProvider.AnyUniversalRole(roles.Prioritized) || PXSiteMapProvider.AnyUniversalRole(roles.Common);
  }

  private bool IsWiki(PXSiteMapNode sitemap)
  {
    string url = sitemap.Url;
    return !string.IsNullOrEmpty(url) && url.ToLowerInvariant().Contains("wiki.aspx");
  }

  /// <summary>
  /// Checks whether the current user has the specific role assigned
  /// </summary>
  /// <param name="role">The role to be checked</param>
  [Obsolete("Use IPrincipal.IsInRole() instead")]
  public static bool IsUserInRole(string role)
  {
    IPrincipal authUser = PXContext.PXIdentity.AuthUser;
    return authUser != null && authUser.IsInRole(role);
  }

  private static bool AnyUniversalRole(string[] roles)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return roles != null && ((IEnumerable<string>) roles).Any<string>(PXSiteMapProvider.\u003C\u003EO.\u003C1\u003E__IsUniversalRoleInERP ?? (PXSiteMapProvider.\u003C\u003EO.\u003C1\u003E__IsUniversalRoleInERP = new Func<string, bool>(PXSiteMapProvider.IsUniversalRoleInERP)));
  }

  private static bool IsUniversalRoleInERP(string role)
  {
    return role == "*" && !PortalHelper.IsPortalContext(PortalContexts.Modern);
  }

  [PXInternalUseOnly]
  [Obsolete]
  public delegate PXSiteMapNode PXSiteMapResolveEventHandler(object sender, EventArgs e);

  [PXInternalUseOnly]
  public abstract class Definition : IInternable
  {
    private readonly object _internObjectLock = new object();
    private bool _isInterned;
    protected readonly Dictionary<Guid, IList<PXSiteMapNode>> ChildNodeCollectionTable = new Dictionary<Guid, IList<PXSiteMapNode>>();
    protected readonly MultiValueDictionary<string, PXSiteMapNode> GraphTypeTable = new MultiValueDictionary<string, PXSiteMapNode>();
    protected readonly Dictionary<Guid, PXSiteMapNode> KeyTable = new Dictionary<Guid, PXSiteMapNode>();
    protected readonly MultiValueDictionary<string, PXSiteMapNode> ScreenIDTable = new MultiValueDictionary<string, PXSiteMapNode>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    protected readonly Dictionary<string, PXSiteMapNode> UrlTable = new Dictionary<string, PXSiteMapNode>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

    /// <summary>The root node</summary>
    public PXSiteMapNode RootNode { get; protected set; }

    /// <summary>All nodes</summary>
    public IEnumerable<PXSiteMapNode> Nodes => (IEnumerable<PXSiteMapNode>) this.KeyTable.Values;

    object IInternable.Intern()
    {
      PXSiteMapProvider.Definition definition = this;
      PXSiteMapProvider.Definition returnValue1;
      if (new PxObjectsIntern<PXSiteMapProvider.Definition>().TryIntern(definition, out returnValue1))
        definition = returnValue1;
      if (definition._isInterned)
        return (object) definition;
      lock (definition._internObjectLock)
      {
        if (!definition._isInterned)
        {
          Dictionary<PXSiteMapNode, PXSiteMapNode> cache = new Dictionary<PXSiteMapNode, PXSiteMapNode>((IEqualityComparer<PXSiteMapNode>) new ReflectionSerializer.ObjectComparer<object>());
          PxObjectsIntern<PXSiteMapNode> intern = new PxObjectsIntern<PXSiteMapNode>();
          PXSiteMapNode returnValue2;
          if (intern.TryIntern(this.RootNode, out returnValue2, cache))
            this.RootNode = returnValue2;
          PXSiteMapProvider.Definition.InternMultiDictionary(this.GraphTypeTable, intern, cache);
          PXSiteMapProvider.Definition.InternMultiDictionary(this.ScreenIDTable, intern, cache);
          PXSiteMapProvider.Definition.InternDictionary<string, PXSiteMapNode>(this.UrlTable, intern, cache);
          PXSiteMapProvider.Definition.InternDictionary<Guid, PXSiteMapNode>(this.KeyTable, intern, cache);
          foreach (IList<PXSiteMapNode> source in this.ChildNodeCollectionTable.Values)
          {
            foreach (PXSiteMapNode pxSiteMapNode in source.ToList<PXSiteMapNode>())
            {
              if (intern.TryIntern(pxSiteMapNode, out returnValue2, cache))
              {
                source.Remove(pxSiteMapNode);
                source.Add(returnValue2);
              }
            }
          }
          definition._isInterned = true;
        }
      }
      return (object) definition;
    }

    protected static void InternDictionary<TKey, TValue>(
      Dictionary<TKey, TValue> dict,
      PxObjectsIntern<TValue> intern,
      Dictionary<TValue, TValue> cache)
      where TValue : class
    {
      foreach (TKey key in Enumerable.ToArray<TKey>(dict.Keys))
      {
        TValue returnValue;
        if (intern.TryIntern(dict[key], out returnValue, cache))
          dict[key] = returnValue;
      }
    }

    protected static void InternMultiDictionary(
      MultiValueDictionary<string, PXSiteMapNode> dict,
      PxObjectsIntern<PXSiteMapNode> intern,
      Dictionary<PXSiteMapNode, PXSiteMapNode> cache)
    {
      foreach (string str in dict.Keys.ToArray<string>())
      {
        foreach (PXSiteMapNode pxSiteMapNode in new List<PXSiteMapNode>((IEnumerable<PXSiteMapNode>) dict[str]))
        {
          PXSiteMapNode returnValue;
          if (intern.TryIntern(pxSiteMapNode, out returnValue, cache))
          {
            dict.Remove(str, pxSiteMapNode);
            dict.Add(str, returnValue);
          }
        }
      }
    }

    protected virtual void AddNode(PXSiteMapNode node, Guid parentID)
    {
      string str1 = node != null ? node.Url : throw new PXArgumentException(nameof (node), "The argument cannot be null.");
      if (!string.IsNullOrEmpty(str1))
      {
        try
        {
          string url = str1.Replace(' ', '+');
          bool isExternalUrl = node.IsExternalUrl;
          try
          {
            url = PXUrl.ToAbsoluteUrl(url);
          }
          catch (ArgumentException ex)
          {
          }
          int num = 0;
          string str2;
          object[] objArray;
          for (str2 = url; this.UrlTable.ContainsKey(str2); str2 = string.Concat(objArray))
          {
            string str3 = url.Contains("?") ? "&" : "?";
            objArray = new object[5]
            {
              (object) url,
              (object) str3,
              (object) "unum",
              (object) "=",
              (object) ++num
            };
          }
          if (!isExternalUrl && node.SelectedUI == "E")
          {
            node.Url = PXUrl.ToRelativeUrl(str2);
            if (node.OriginalUrl != null)
              node.OriginalUrl = $"{node.OriginalUrl}{(node.OriginalUrl.Contains("?") ? (object) "&" : (object) "?")}unum={(object) num}";
          }
          this.UrlTable[str2] = node;
          if (!string.IsNullOrEmpty(node.ScreenID))
            this.ScreenIDTable.Add(node.ScreenID, node);
          if (!string.IsNullOrEmpty(node.GraphType))
            this.GraphTypeTable.Add(node.GraphType, node);
        }
        catch (Exception ex)
        {
          PXTrace.Logger.Error<Guid, string, string>(ex, "Error when adding node {NodeGuid} with title {NodeTitle} and {NodeUrl} to SiteMap", node.NodeID, node.Title, node.Url);
        }
      }
      if (this.KeyTable.ContainsKey(node.NodeID))
        throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("Multiple nodes with identical key {0} have been detected.", (object) node.Key));
      this.KeyTable[node.NodeID] = node;
      IList<PXSiteMapNode> pxSiteMapNodeList;
      if (this.ChildNodeCollectionTable.TryGetValue(node.NodeID, out pxSiteMapNodeList))
      {
        foreach (PXSiteMapNode pxSiteMapNode in (IEnumerable<PXSiteMapNode>) pxSiteMapNodeList)
          pxSiteMapNode.ParentNode = node;
      }
      if (parentID != node.NodeID)
      {
        if (!this.ChildNodeCollectionTable.TryGetValue(parentID, out pxSiteMapNodeList))
          this.ChildNodeCollectionTable[parentID] = pxSiteMapNodeList = (IList<PXSiteMapNode>) new List<PXSiteMapNode>();
        pxSiteMapNodeList.Add(node);
        PXSiteMapNode pxSiteMapNode1;
        if (!this.KeyTable.TryGetValue(parentID, out pxSiteMapNode1))
          return;
        foreach (PXSiteMapNode pxSiteMapNode2 in (IEnumerable<PXSiteMapNode>) pxSiteMapNodeList)
          pxSiteMapNode2.ParentNode = pxSiteMapNode1;
      }
      else
      {
        if (this.RootNode != null)
          return;
        this.RootNode = node;
      }
    }

    protected virtual void RemoveNode(PXSiteMapNode node)
    {
      if (node == null)
        throw new PXArgumentException(nameof (node), "The argument cannot be null.");
      IList<PXSiteMapNode> pxSiteMapNodeList;
      if ((node.ParentID != Guid.Empty || node.NodeID != Guid.Empty) && this.ChildNodeCollectionTable.TryGetValue(node.ParentID, out pxSiteMapNodeList) && pxSiteMapNodeList.Contains(node))
        pxSiteMapNodeList.Remove(node);
      string url = node.Url;
      if (!string.IsNullOrEmpty(url) && this.UrlTable.ContainsKey(url))
        this.UrlTable.Remove(url);
      if (this.KeyTable.ContainsKey(node.NodeID))
        this.KeyTable.Remove(node.NodeID);
      if (!string.IsNullOrEmpty(node.ScreenID) && this.ScreenIDTable.ContainsKey(node.ScreenID))
        this.ScreenIDTable.Remove(node.ScreenID, node);
      if (string.IsNullOrEmpty(node.GraphType) || !this.GraphTypeTable.ContainsKey(node.GraphType))
        return;
      this.GraphTypeTable.Remove(node.GraphType, node);
    }

    /// <summary>Tries to find the node using the specific condition</summary>
    /// <param name="predicate">The predicate to be used to find the node</param>
    internal PXSiteMapNode TryGetNode(Func<PXSiteMapNode, bool> predicate)
    {
      return this.Nodes.FirstOrDefault<PXSiteMapNode>(predicate);
    }

    /// <summary>Tries to find the node by URL</summary>
    /// <param name="url">The URL to be used</param>
    /// <param name="node">The found node or <see langword="null" /></param>
    /// <returns><see langword="true" /> if the node is found, otherwise <see langword="false" /></returns>
    internal bool TryGetNodeByUrl(string url, out PXSiteMapNode node)
    {
      return this.UrlTable.TryGetValue(url, out node);
    }

    /// <summary>
    /// Tries to find the node by the URL using the specific condition
    /// </summary>
    /// <param name="urlPredicate">The predicate to be used to find the node</param>
    /// <returns>The found node or <see langword="null" /></returns>
    internal PXSiteMapNode TryGetNodeByUrl(Func<string, PXSiteMapNode, bool> urlPredicate)
    {
      return this.UrlTable.FirstOrDefault<KeyValuePair<string, PXSiteMapNode>>((Func<KeyValuePair<string, PXSiteMapNode>, bool>) (x => urlPredicate(x.Key, x.Value))).Value;
    }

    /// <summary>Tries to find the node by its identifier</summary>
    /// <param name="key">The node identifier</param>
    /// <param name="node">The found node or <see langword="null" /></param>
    /// <returns><see langword="true" /> if the node is found, otherwise <see langword="false" /></returns>
    internal bool TryGetNodeByKey(Guid key, out PXSiteMapNode node)
    {
      return this.KeyTable.TryGetValue(key, out node);
    }

    internal virtual bool TryGetNodesByScreenId(
      string screenId,
      out IReadOnlyCollection<PXSiteMapNode> nodes)
    {
      return this.ScreenIDTable.TryGetValue(screenId, ref nodes);
    }

    internal virtual bool TryGetNodesByGraphType(
      string graphType,
      out IReadOnlyCollection<PXSiteMapNode> nodes)
    {
      return this.GraphTypeTable.TryGetValue(graphType, ref nodes);
    }

    /// <summary>Tries to find child node of the specific node</summary>
    /// <param name="nodeId">The node identifier</param>
    /// <param name="nodes">Found nodes or an empty collection</param>
    /// <returns><see langword="true" /> if the node is found, otherwise <see langword="false" /></returns>
    internal bool TryGetChildNodesByNodeId(Guid nodeId, out IList<PXSiteMapNode> nodes)
    {
      return this.ChildNodeCollectionTable.TryGetValue(nodeId, out nodes);
    }
  }
}
