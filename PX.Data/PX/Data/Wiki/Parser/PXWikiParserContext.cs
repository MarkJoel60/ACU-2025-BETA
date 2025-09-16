// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXWikiParserContext
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.Parser.Html;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>
/// Represents settings that are used during output rendering.
/// </summary>
public class PXWikiParserContext
{
  protected string pageTitle;
  protected System.DateTime? lastModified;
  protected string createdByLogin;
  protected string lastModifiedLogin;
  protected string editLink;
  protected string editSectionLink;
  protected bool renderSectionLink;
  protected bool hideBrokenLink;
  protected bool redirectAvailable;
  protected bool redirectEnable;
  protected int sectionCount = 1;
  protected bool expandableTOC = true;
  protected bool allowSectionsExpand = true;
  protected bool enableScript = true;
  protected bool isDesignMode;
  protected string idSuffix = "";
  private string stylesPath;
  protected int contentWidth;
  protected int contentHeight;
  protected bool isSimpleRender;
  protected string wikiTitle;
  protected string themePath;
  protected bool renderFileList;
  protected bool noneedparse;
  protected string text;
  protected PXRenderer renderer;
  protected Dictionary<string, object> Exchangedata;
  protected int listlevel;
  public readonly ISettings Settings;

  public Dictionary<string, object> GetExchangedata() => this.Exchangedata;

  /// <summary>Gets or sets wiki title.</summary>
  public string WikiTitle
  {
    get => this.wikiTitle;
    set => this.wikiTitle = value;
  }

  /// <summary>
  /// Gets or sets path to a theme used for wiki formatting.
  /// </summary>
  public string ThemePath
  {
    get => this.themePath;
    set => this.themePath = value;
  }

  /// <summary>Gets or sets a title for current wiki page.</summary>
  public string PageTitle
  {
    get => this.pageTitle;
    set => this.pageTitle = value;
  }

  public string Text
  {
    get => this.text;
    set => this.text = value;
  }

  /// <summary>
  /// Gets or sets time of last modification for current wiki page.
  /// </summary>
  public System.DateTime? LastModified
  {
    get => this.lastModified;
    set => this.lastModified = value;
  }

  /// <summary>
  /// Gets or sets name of user who created current wiki page.
  /// </summary>
  public string CreatedByLogin
  {
    get => this.createdByLogin;
    set => this.createdByLogin = value;
  }

  /// <summary>
  /// Gets or sets name of user who was the last to modify current wiki page.
  /// </summary>
  public string LastModifiedByLogin
  {
    get => this.lastModifiedLogin;
    set => this.lastModifiedLogin = value;
  }

  /// <summary>
  /// Gets or sets link to a web-page which allows to edit current wiki page.
  /// </summary>
  public string EditLink
  {
    get => this.editLink;
    set => this.editLink = value;
  }

  /// <summary>
  /// Gets or sets value indicating whether section editing is allowed (EnableScript should be true).
  /// </summary>
  public bool RenderSectionLink
  {
    get => this.renderSectionLink;
    set => this.renderSectionLink = value;
  }

  /// <summary>
  /// Gets or sets value indicating whether broken link is hidden.
  /// </summary>
  public bool HideBrokenLink
  {
    get => this.hideBrokenLink;
    set => this.hideBrokenLink = value;
  }

  /// <summary>
  /// Gets or sets value indicating whether redirect is available.
  /// </summary>
  public bool RedirectAvailable
  {
    get => this.redirectAvailable;
    set => this.redirectAvailable = value;
  }

  /// <summary>
  /// Gets or sets value indicating whether redirect is enabled on render page.
  /// </summary>
  internal bool RedirectEnable
  {
    get => this.redirectEnable && this.redirectAvailable;
    set => this.redirectEnable = value;
  }

  /// <summary>
  /// Gets or sets value indicating whether table of contents should be expandable.
  /// </summary>
  public bool ExpandableTOC
  {
    get => this.expandableTOC;
    set => this.expandableTOC = value;
  }

  /// <summary>
  /// Gets or sets value indicating whether sections are expandable.
  /// </summary>
  public bool AllowSectionsExpand
  {
    get => this.allowSectionsExpand;
    set => this.allowSectionsExpand = value;
  }

  /// <summary>
  /// Gets or sets number of sections in current wiki article.
  /// </summary>
  internal int SectionCount
  {
    get => this.sectionCount;
    set => this.sectionCount = value;
  }

  /// <summary>
  /// Gets value indicating whether FileList control will be rendered somewhere in the article.
  /// </summary>
  public bool RenderFileList
  {
    get => this.renderFileList;
    internal set => this.renderFileList = value;
  }

  /// <summary>
  /// Gets or sets value indicating whether client-side scripts should be enabled for HTML rendering.
  /// </summary>
  public bool EnableScript
  {
    get => this.enableScript;
    set => this.enableScript = value;
  }

  /// <summary>Gets or sets path to CSS styles for wiki.</summary>
  public string StylesPath
  {
    get => this.stylesPath ?? this.DefaultStylesPath;
    set => this.stylesPath = value;
  }

  /// <summary>
  /// Gets or sets value indicating whether wiki text will be parsed for design mode (when WYSIWYG editor is available).
  /// </summary>
  public bool IsDesignMode
  {
    get => this.isDesignMode;
    set => this.isDesignMode = value;
  }

  /// <summary>
  /// Gets or sets string which will be added to sections ID and images in order that several PXWikiShow controls could be shown on a page.
  /// </summary>
  public string IDSuffix
  {
    get => this.idSuffix;
    set => this.idSuffix = value;
  }

  /// <summary>Gets or sets width of rendered content.</summary>
  public int ContentWidth
  {
    get => this.contentWidth;
    set => this.contentWidth = value;
  }

  /// <summary>Gets or sets height of rendered content.</summary>
  public int ContentHeight
  {
    get => this.contentHeight;
    set => this.contentHeight = value;
  }

  /// <summary>
  /// Gets or sets renderer which will be used to display parsed article (PXHtmlRenderer is used by default).
  /// </summary>
  public PXRenderer Renderer
  {
    get => this.renderer;
    set => this.renderer = value;
  }

  /// <summary>
  /// Gets or sets value inicating whether article should be rendered as simple, not intended for use with PXWikiShow control.
  /// </summary>
  public bool IsSimpleRender
  {
    get => this.isSimpleRender;
    set => this.isSimpleRender = value;
  }

  /// <summary>Gets or sets value level of list during parsing.</summary>
  public int Listlevel
  {
    get => this.listlevel;
    set => this.listlevel = value;
  }

  public PXWikiParserContext(ISettings settings)
  {
    this.Settings = settings ?? (ISettings) new PXSettings();
    this.renderer = (PXRenderer) new PXHtmlRenderer(this);
    this.Exchangedata = new Dictionary<string, object>();
  }

  public PXWikiParserContext()
    : this((ISettings) new PXSettings())
  {
    this.Exchangedata = new Dictionary<string, object>();
  }

  public virtual string ReadTemplateContent(string templateName) => (string) null;

  public virtual bool Redirect(string link) => false;

  public virtual LinkInfo CreateArticleLink(string link) => this.GetDefaultLinkInfo(link);

  public virtual LinkInfo CreateImageLink(PXImageElement e, bool getBinaryData)
  {
    return this.GetDefaultLinkInfo(e.Name);
  }

  public virtual LinkInfo CreateFileLink(string link, PXLinkElement e)
  {
    return this.GetDefaultLinkInfo(link);
  }

  public virtual bool ResizeImage(LinkInfo info, int width, int height) => true;

  protected virtual string DefaultStylesPath => this.Settings.DefaultStylesPath;

  private LinkInfo GetDefaultLinkInfo(string link)
  {
    LinkInfo defaultLinkInfo = new LinkInfo();
    string str1;
    string str2 = str1 = "#";
    defaultLinkInfo.FileInfoPath = str1;
    defaultLinkInfo.Url = str2;
    defaultLinkInfo.DefaultCaption = link;
    defaultLinkInfo.IsExisting = true;
    return defaultLinkInfo;
  }
}
