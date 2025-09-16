// Decompiled with JetBrains decompiler
// Type: PX.Data.PXWikiMapNode
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

public class PXWikiMapNode : PXSiteMapNode
{
  private bool isRootDeletedItems;
  private int? approvalGroup;
  private string wiki;
  private string name;
  private string summary;
  private bool isPublished;
  private bool isLastRevPublished;
  private PXWikiRights?[] rights;
  public readonly bool IsFolder;
  private bool isRootWikiNode;

  internal override string SelectedUI => "E";

  public PXWikiMapNode(
    PXSiteMapProvider provider,
    Guid key,
    string url,
    string title,
    PXRoleList roles,
    bool? expanded,
    bool? mainForm,
    string graphType,
    bool? isFolder,
    string screeenID)
    : base(provider, key, url, title, roles, graphType, screeenID)
  {
    this.IsFolder = isFolder.GetValueOrDefault();
  }

  public bool IsRootDeletedItems
  {
    get => this.isRootDeletedItems;
    internal set => this.isRootDeletedItems = value;
  }

  public bool IsRootWikiNode
  {
    get => this.isRootWikiNode;
    internal set => this.isRootWikiNode = value;
  }

  public bool IsPublished
  {
    get => this.isPublished;
    internal set => this.isPublished = value;
  }

  public bool IsLastRevPublished
  {
    get => this.isLastRevPublished;
    internal set => this.isLastRevPublished = value;
  }

  internal PXWikiRights?[] Rights
  {
    get => this.rights;
    set => this.rights = value;
  }

  public int? ApprovalGroup
  {
    get => this.approvalGroup;
    set => this.approvalGroup = value;
  }

  public string Wiki
  {
    get => this.wiki;
    internal set => this.wiki = value;
  }

  public string Name
  {
    get => this.name;
    internal set => this.name = value;
  }

  public string Summary
  {
    get => this.summary;
    internal set => this.summary = value;
  }

  public string StringID => PXWikiMapNode.GetStringID(this.wiki, this.name);

  internal static string GetStringID(string wiki, string name) => $"{wiki}:{name ?? string.Empty}";
}
