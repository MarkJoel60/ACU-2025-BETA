// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXSettings
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

[Serializable]
public sealed class PXSettings : ISettings
{
  private string _searchLink = "";
  private string _searchUnknownLink = "";
  private string _editLinkText = "Edit";
  private string _closeLinkText = "Close";
  private string _rootUrl = PXUrl.SiteUrlWithPath();
  /// <summary>
  /// Gets or sets paths to images displayed as icons for files with specified extensions.
  /// </summary>
  public readonly Dictionary<string, string> _extensionsImages = new Dictionary<string, string>();

  public PXSettings()
  {
  }

  public PXSettings(ISettings source)
  {
    this.NamedLinks = source.NamedLinks;
    this.EditLinkText = source.EditLinkText;
    this.CloseLinkText = source.CloseLinkText;
    this.HintImageUrl = source.HintImageUrl;
    this.WarnImageUrl = source.WarnImageUrl;
    this.IsHtml = source.IsHtml;
    this.DefaultStylesPath = source.DefaultStylesPath;
    this.GetCSSUrl = source.GetCSSUrl;
    this.ArticleShowUrl = source.ArticleShowUrl;
    this.GetFileUrl = source.GetFileUrl;
    this.FileEditUrl = source.FileEditUrl;
    this.MagnifyImageUrl = source.MagnifyImageUrl;
    this.FilesDirectAccess = source.FilesDirectAccess;
    this.PlayFlashIconUrl = source.PlayFlashIconUrl;
    this.GetRSSUrl = source.GetRSSUrl;
    this.RSSImageUrl = source.RSSImageUrl;
    this.RootUrl = source.RootUrl;
    this.SearchLink = source.SearchLink;
    this.SearchUnknownLink = source.SearchLink;
  }

  public bool NamedLinks { get; set; }

  public string SearchLink
  {
    get => this._searchLink;
    set => this._searchLink = value;
  }

  public string SearchUnknownLink
  {
    get => this._searchUnknownLink;
    set => this._searchUnknownLink = value;
  }

  /// <summary>
  /// Gets or sets text whic is used as caption for EditLink in 'edit' mode.
  /// </summary>
  public string EditLinkText
  {
    get => this._editLinkText;
    set => this._editLinkText = value;
  }

  /// <summary>
  /// Gets or sets text which is used as caption for EditLink in 'close' mode.
  /// </summary>
  public string CloseLinkText
  {
    get => this._closeLinkText;
    set => this._closeLinkText = value;
  }

  /// <summary>
  /// Gets or sets URL of image used as Info icon inside of HintBox.
  /// </summary>
  public string HintImageUrl { get; set; }

  /// <summary>
  /// Gets or sets URL of image used as Warning icon inside of WarnBox.
  /// </summary>
  public string WarnImageUrl { get; set; }

  public bool IsHtml { get; set; }

  /// <summary>Gets or sets path to CSS styles for wiki.</summary>
  public string DefaultStylesPath { get; set; }

  public string GetCSSUrl { get; set; }

  public string ArticleShowUrl { get; set; }

  public string GetFileUrl { get; set; }

  public string FileEditUrl { get; set; }

  public string MagnifyImageUrl { get; set; }

  public bool FilesDirectAccess { get; set; }

  public string PlayFlashIconUrl { get; set; }

  public string GetRSSUrl { get; set; }

  public string RSSImageUrl { get; set; }

  public string RootUrl
  {
    get => this._rootUrl;
    set => this._rootUrl = value;
  }

  public string ExternalRootUrl { get; set; }

  /// <summary>
  /// Gets or sets path to a default image displayed as icon for files with unknown extensions.
  /// </summary>
  public string DefaultExtensionImage { get; set; }

  public IDictionary<string, string> ExtensionsImages
  {
    get => (IDictionary<string, string>) this._extensionsImages;
  }
}
