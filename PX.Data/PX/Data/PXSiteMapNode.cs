// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSiteMapNode
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>Represents single site map node</summary>
public class PXSiteMapNode : IEquatable<PXSiteMapNode>
{
  private IList<PXSiteMapNode> _childNodes;
  protected string _originalUrl;
  private PXSiteMapNode _parentNode;
  private bool _parentNodeSet;
  private string _title = string.Empty;
  private string _url;
  private bool _useSavedChildNodes;

  [PXInternalUseOnly]
  public PXSiteMapNode(
    PXSiteMapProvider provider,
    Guid nodeID,
    string url,
    string title,
    PXRoleList roles,
    string graphType,
    string screeenID)
  {
    this.NodeID = nodeID;
    this.Key = nodeID.ToString();
    this.GraphType = graphType;
    this.ScreenID = screeenID ?? string.Empty;
    this.PXRoles = roles;
    if (!string.IsNullOrEmpty(url))
      this.OriginalUrl = url.Trim();
    this.Provider = provider;
    this._url = url == null || url.StartsWith("http") ? url?.Trim() : url.ToLower().Trim();
    this.Title = title;
    this.Roles = (IList<string>) (roles?.Prioritized ?? roles?.Common);
  }

  /// <summary>Node identifier</summary>
  [PXInternalUseOnly]
  public Guid NodeID { get; }

  /// <summary>Child nodes of the current node</summary>
  [PXInternalUseOnly]
  public IList<PXSiteMapNode> ChildNodes
  {
    get
    {
      return this._useSavedChildNodes ? this._childNodes : (IList<PXSiteMapNode>) this.Provider.GetChildNodes(this).ToList<PXSiteMapNode>();
    }
    set
    {
      this._childNodes = value;
      this._useSavedChildNodes = true;
    }
  }

  /// <summary>Parent node of the current node</summary>
  [PXInternalUseOnly]
  public PXSiteMapNode ParentNode
  {
    get => this._parentNode;
    set
    {
      this._parentNodeSet = true;
      this._parentNode = value;
    }
  }

  /// <summary>Identifier of the parent node</summary>
  [PXInternalUseOnly]
  public Guid ParentID => !this._parentNodeSet ? Guid.Empty : this.ParentNode.NodeID;

  /// <summary>Title of the node</summary>
  [PXInternalUseOnly]
  public string Title
  {
    get
    {
      return this.NodeID == Guid.Empty ? PXDatabase.Provider.GetCompanyDisplayName() : this._title ?? string.Empty;
    }
    set
    {
      if (!(this.NodeID != Guid.Empty))
        return;
      this._title = value;
    }
  }

  /// <summary>Graph type of the node</summary>
  public string GraphType { get; }

  /// <summary>Screen identifier of the node</summary>
  public string ScreenID { get; }

  /// <summary>
  /// Represents URL in original case (for case-sensitive usages).
  /// </summary>
  internal virtual string OriginalUrl
  {
    get => this._originalUrl;
    set
    {
      if (!string.IsNullOrEmpty(this._originalUrl))
      {
        string b = value != null ? PXUrl.IngoreAllQueryParameters(value) : throw new ArgumentNullException(nameof (value));
        if (!string.Equals(PXUrl.IngoreAllQueryParameters(this._originalUrl), b, StringComparison.OrdinalIgnoreCase))
          throw new ArgumentException();
      }
      this._originalUrl = value;
    }
  }

  /// <summary>Indicates whether the node has an external url</summary>
  internal virtual bool IsExternalUrl => this.SelectedUI == "E" && PXUrl.IsExternalUrl(this.Url);

  /// <summary>Roles of the node</summary>
  public PXRoleList PXRoles { get; }

  /// <summary>Node identifier</summary>
  /// <remarks>It's the string representation of the <see cref="P:PX.Data.PXSiteMapNode.NodeID" /> property that left for backward compatibility</remarks>
  [Obsolete("Use NodeID instead")]
  [PXInternalUseOnly]
  public string Key { get; }

  /// <summary>A URL of the node</summary>
  public virtual string Url
  {
    get => this._url;
    set => this._url = value?.Trim() ?? string.Empty;
  }

  /// <summary>List of role names</summary>
  [PXInternalUseOnly]
  public IList<string> Roles { get; set; }

  /// <summary>Site map provider the node is belongs to</summary>
  [PXInternalUseOnly]
  protected internal PXSiteMapProvider Provider { get; }

  /// <summary>Active UI type</summary>
  internal virtual string SelectedUI => SitePolicy.DefaultUI ?? "E";

  /// <summary>
  /// The URL of legacy UI of the node can be saved if it's not standard to be able to restore it later.
  /// </summary>
  internal virtual string UrlBackup { get; private set; }

  bool IEquatable<PXSiteMapNode>.Equals(PXSiteMapNode other) => this.EqualsToCurrent(other);

  public override bool Equals(object obj)
  {
    return obj is PXSiteMapNode other && this.EqualsToCurrent(other);
  }

  private bool EqualsToCurrent(PXSiteMapNode other)
  {
    return other != null && this.NodeID == other.NodeID && string.Equals(this.Url, other.Url, StringComparison.OrdinalIgnoreCase);
  }

  public override int GetHashCode() => this.NodeID.GetHashCode();

  internal delegate PXSiteMapNode Factory(
    PXSiteMapProvider provider,
    Guid nodeID,
    string url,
    string title,
    PXRoleList roles,
    string graphType,
    string screeenID);
}
