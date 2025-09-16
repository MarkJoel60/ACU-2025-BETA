// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXDBContext
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using System;
using System.IO;
using System.Web;

#nullable disable
namespace PX.Data.Wiki.Parser;

public class PXDBContext : PXWikiParserContext
{
  private WikiReader reader;
  private WikiAnnouncementReader announcementReader;
  public Guid? StyleID;

  public virtual Guid? WikiID { get; set; }

  public WikiReader Reader
  {
    get
    {
      if (this.reader == null)
        this.reader = PXGraph.CreateInstance<WikiReader>();
      return this.reader;
    }
  }

  public WikiAnnouncementReader AnnouncementReader
  {
    get
    {
      if (this.announcementReader == null)
        this.announcementReader = new WikiAnnouncementReader();
      return this.announcementReader;
    }
  }

  public PXDBContext(PXDBContext source)
    : this(source.Settings, source.reader)
  {
  }

  public PXDBContext(ISettings settings)
    : this(settings, (WikiReader) null)
  {
  }

  public PXDBContext(WikiReader reader)
    : this((ISettings) new PXSettings(), reader)
  {
  }

  public PXDBContext(ISettings settings, WikiReader reader)
    : base(settings)
  {
    this.reader = reader;
  }

  public override string ReadTemplateContent(string templateName) => this.GetArticle(templateName);

  public override bool Redirect(string link)
  {
    LinkInfo articleLink = this.CreateArticleLink(link);
    if (articleLink != null && articleLink.IsExisting)
      throw new PXRedirectToUrlException(articleLink.Url, "Wiki Redirect");
    return false;
  }

  public override LinkInfo CreateArticleLink(string link)
  {
    if (this.Reader == null)
      return base.CreateArticleLink(link);
    LinkInfo result = new LinkInfo();
    PXDBContext.LinkData linkData = this.GetLinkData(link);
    PXResult<WikiPageSimple, WikiDescriptor> page = this.ReadArticle(linkData.Name);
    if (page != null)
    {
      if (this.IsArticleAvailable((WikiPageSimple) page))
      {
        WikiPageSimple wikiPageSimple = (WikiPageSimple) page;
        WikiDescriptor wikiDescriptor = (WikiDescriptor) page;
        PXSiteMapNode siteMapNodeFromKey = PXSiteMap.WikiProvider.FindSiteMapNodeFromKey(wikiPageSimple.PageID.Value);
        result.PageID = wikiPageSimple.PageID;
        result.DefaultCaption = siteMapNodeFromKey == null ? PXMessages.LocalizeNoPrefix("Deleted Article") : siteMapNodeFromKey.Title;
        result.Url = this.Settings.ArticleShowUrl;
        string externalRootUrl = this.ExternalRootUrl;
        if (!string.IsNullOrEmpty(externalRootUrl))
          result.Url = $"{externalRootUrl}/{HttpUtility.UrlEncode(wikiPageSimple.Name)}";
        if (!string.IsNullOrEmpty(linkData.Anchor))
        {
          LinkInfo linkInfo = result;
          linkInfo.Url = $"{linkInfo.Url}#{linkData.Anchor}";
        }
        if (this.Settings.NamedLinks || this.IsDesignMode)
          result.Url += string.Format("?wiki={0}&wikiname={0}&art={1}", (object) HttpUtility.UrlEncode(wikiDescriptor.Name), (object) HttpUtility.UrlEncode(wikiPageSimple.Name));
        else
          result.Url += $"?wikiname={HttpUtility.UrlEncode(wikiDescriptor.Name)}&PageID={wikiPageSimple.PageID}";
        result.IsExisting = wikiPageSimple.PageID.HasValue;
      }
      else
        this.MakeRedLink(((WikiPageSimple) page).PageID.GetValueOrDefault().ToString(), result, linkData.Anchor);
    }
    else if (!GUID.CreateGuid(linkData.Name).HasValue)
      this.MakeRedLink(linkData.Name, result, linkData.Anchor);
    else
      result.IsInvalid = true;
    if (!string.IsNullOrEmpty(linkData.Parameters) && !string.IsNullOrEmpty(result.Url))
    {
      LinkInfo linkInfo = result;
      linkInfo.Url = linkInfo.Url + (result.Url.Contains("?") ? "&" : "?") + linkData.Parameters;
    }
    return result;
  }

  protected virtual string ExternalRootUrl => (string) null;

  protected PXDBContext.LinkData GetLinkData(string link)
  {
    if (string.IsNullOrEmpty(link))
      return new PXDBContext.LinkData(string.Empty, string.Empty, Array.Empty<string>());
    string[] sourceArray = link.Split('#');
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string[] destinationArray = new string[0];
    if (sourceArray.Length != 0)
      empty1 = sourceArray[0];
    if (sourceArray.Length > 1)
      empty2 = sourceArray[1];
    if (sourceArray.Length > 2)
    {
      destinationArray = new string[sourceArray.Length - 2];
      Array.Copy((Array) sourceArray, 2, (Array) destinationArray, 0, destinationArray.Length);
    }
    return new PXDBContext.LinkData(empty1, empty2, destinationArray);
  }

  public override LinkInfo CreateImageLink(PXImageElement e, bool getBinaryData)
  {
    if (this.Reader == null)
      return base.CreateImageLink(e, getBinaryData);
    LinkInfo linkInfo1 = this.CreateLinkInfo(e.Name, getBinaryData);
    if (linkInfo1.IsExisting)
    {
      int? fileRevision = e.FileRevision;
      if (fileRevision.HasValue)
      {
        LinkInfo linkInfo2 = linkInfo1;
        string url = linkInfo2.Url;
        fileRevision = e.FileRevision;
        string str = fileRevision.Value.ToString();
        linkInfo2.Url = $"{url}&revision={str}";
      }
    }
    return linkInfo1;
  }

  public override LinkInfo CreateFileLink(string link, PXLinkElement e)
  {
    if (this.Reader == null)
      return base.CreateFileLink(link, e);
    LinkInfo linkInfo1 = this.CreateLinkInfo(link, false);
    if (linkInfo1.IsExisting)
    {
      int? fileRevision = e.FileRevision;
      if (fileRevision.HasValue)
      {
        LinkInfo linkInfo2 = linkInfo1;
        string url = linkInfo2.Url;
        fileRevision = e.FileRevision;
        string str = fileRevision.Value.ToString();
        linkInfo2.Url = $"{url}&revision={str}";
      }
    }
    return linkInfo1;
  }

  protected override string DefaultStylesPath
  {
    get
    {
      if (this.StyleID.HasValue)
        return $"{this.Settings.GetCSSUrl}?style={this.StyleID.ToString()}";
      Guid? wikiId = this.WikiID;
      if (wikiId.HasValue)
      {
        PXWikiProvider wikiProvider = PXSiteMap.WikiProvider;
        wikiId = this.WikiID;
        Guid key = wikiId.Value;
        PXWikiMapNode siteMapNodeFromKey = (PXWikiMapNode) wikiProvider.FindSiteMapNodeFromKey(key);
        if (siteMapNodeFromKey != null)
          return $"{this.Settings.GetCSSUrl}?wiki={siteMapNodeFromKey.Name}";
      }
      return base.DefaultStylesPath;
    }
  }

  protected string GetArticle(string artName)
  {
    if (this.Reader == null)
      return (string) null;
    this.Reader.Filter.Current = new WikiPageFilter();
    this.Reader.Filter.Current.WikiID = this.WikiID;
    this.Reader.Filter.Current.Art = artName;
    return this.Reader.ReadPage()?.Content;
  }

  protected UploadFile ReadFile(string link, bool getBinaryData)
  {
    Guid? guid = GUID.CreateGuid(link);
    int length = link.IndexOf(':');
    if (length > 0 && length + 1 < link.Length && int.TryParse(link.Substring(0, length), out int _))
      link = link.Substring(length + 1);
    link = HttpUtility.UrlDecode(link);
    if (getBinaryData)
    {
      PXResult<UploadFile, UploadFileRevision> pxResult = (PXResult<UploadFile, UploadFileRevision>) (PXResult<UploadFile>) PXSelectBase<UploadFile, PXSelectJoin<UploadFile, InnerJoin<UploadFileRevision, On<UploadFile.fileID, Equal<UploadFileRevision.fileID>, And<UploadFile.lastRevisionID, Equal<UploadFileRevision.fileRevisionID>>>>, Where<UploadFile.fileID, Equal<Required<UploadFile.fileID>>, Or<UploadFile.name, Equal<Required<UploadFile.name>>>>>.Config>.Select((PXGraph) this.Reader, (object) guid, (object) link);
      if (pxResult == null)
        return (UploadFile) null;
      UploadFile uploadFile = (UploadFile) pxResult[typeof (UploadFile)];
      UploadFileRevision uploadFileRevision = (UploadFileRevision) pxResult[typeof (UploadFileRevision)];
      if (uploadFile != null)
        uploadFile.Data = uploadFileRevision.Data;
      return uploadFile;
    }
    return (UploadFile) PXSelectBase<UploadFile, PXSelect<UploadFile, Where<UploadFile.fileID, Equal<Required<UploadFile.fileID>>, Or<UploadFile.name, Equal<Required<UploadFile.name>>>>>.Config>.Select((PXGraph) this.Reader, (object) guid, (object) link);
  }

  protected PXResult<WikiPageSimple, WikiDescriptor> ReadArticle(string link)
  {
    Guid? guid = GUID.CreateGuid(link);
    int length = link.IndexOf('\\');
    string str1 = string.Empty;
    string str2 = link;
    if (length > 0)
    {
      str1 = link.Substring(0, length);
      str2 = link.Substring(length + 1);
    }
    return (PXResult<WikiPageSimple, WikiDescriptor>) (PXResult<WikiPageSimple>) PXSelectBase<WikiPageSimple, PXSelectJoin<WikiPageSimple, InnerJoin<WikiDescriptor, On<WikiDescriptor.pageID, Equal<WikiPageSimple.wikiID>>>, Where<WikiPageSimple.pageID, Equal<Required<WikiPageSimple.pageID>>, Or<Where2<Where<WikiDescriptor.pageID, Equal<Required<WikiDescriptor.pageID>>, Or<WikiDescriptor.name, Equal<Required<WikiDescriptor.name>>>>, And<WikiPageSimple.name, Equal<Required<WikiPageSimple.name>>>>>>>.Config>.Select((PXGraph) this.Reader, (object) guid, (object) this.WikiID, (object) str1, (object) str2);
  }

  protected bool IsArticleAvailable(WikiPageSimple page)
  {
    return page != null && page.PageID.HasValue && PXSiteMap.WikiProvider.GetAccessRights(page.PageID.Value) >= PXWikiRights.Select;
  }

  private LinkInfo CreateLinkInfo(string link, bool getBinaryData)
  {
    bool flag = false;
    if (this.Reader == null)
      return (LinkInfo) null;
    if (!string.IsNullOrEmpty(link) && link[0] == '~')
    {
      flag = true;
      link = link.Substring(1);
    }
    LinkInfo linkInfo1 = new LinkInfo();
    UploadFile file = this.ReadFile(link, getBinaryData);
    linkInfo1.DefaultCaption = file != null ? file.Name : link;
    if (file != null)
    {
      linkInfo1.Extension = Path.GetExtension(file.Name);
      linkInfo1.DefaultCaption = file.Name;
      linkInfo1.Url = !flag || string.IsNullOrEmpty(HttpRuntime.AppDomainAppVirtualPath) ? this.Settings.GetFileUrl + (this.Settings.NamedLinks ? $"?file={HttpUtility.UrlEncode(file.Name)}" : $"?fileID={file.FileID.ToString() + linkInfo1.Extension}") : $"{this.Settings.GetFileUrl}?file={HttpUtility.UrlEncode(file.Name)}";
      if (this.Settings.FilesDirectAccess)
      {
        LinkInfo linkInfo2 = linkInfo1;
        linkInfo2.Url = $"{linkInfo2.Url}&chk={HttpUtility.UrlEncode(UploadFileMaintenance.ComputeHash(file))}";
      }
      linkInfo1.FileInfoPath = UploadFileMaintenance.AccessRights(file) >= PXCacheRights.Update ? this.Settings.FileEditUrl + $"?fileID={HttpUtility.UrlEncode(file.Name)}" : (string) null;
      linkInfo1.BinData = file.Data;
    }
    else
      linkInfo1.Url = link;
    linkInfo1.IsExisting = file != null;
    return linkInfo1;
  }

  private void MakeRedLink(string link, LinkInfo result, string anchor)
  {
    PXWikiMapNode pxWikiMapNode = !(PXSiteMap.CurrentNode is PXWikiMapNode currentNode) ? PXSiteMap.WikiProvider.FindWiki(this.WikiID.GetValueOrDefault()) : currentNode;
    int length = link.IndexOf("\\");
    string str1 = length > 0 ? link.Substring(length + 1) : link;
    string str2 = length > 0 ? link.Substring(0, length) : (pxWikiMapNode == null ? "none" : pxWikiMapNode.Wiki);
    string key = pxWikiMapNode == null ? "" : pxWikiMapNode.Key;
    Guid? guid = GUID.CreateGuid(link);
    result.Url = this.Settings.ArticleShowUrl;
    string externalRootUrl = this.ExternalRootUrl;
    if (!string.IsNullOrEmpty(externalRootUrl))
      result.Url = $"{externalRootUrl}/{HttpUtility.UrlEncode(str1)}";
    if (!string.IsNullOrEmpty(anchor))
    {
      LinkInfo linkInfo = result;
      linkInfo.Url = $"{linkInfo.Url}#{anchor}";
    }
    if (!guid.HasValue)
      result.Url += $"?wiki={str2}&art={HttpUtility.UrlEncode(str1)}&from={key}";
    else
      result.Url += $"?PageID={guid}";
  }

  protected struct LinkData(string name, string anchor, params string[] parameters)
  {
    public readonly string Name = name;
    public readonly string Anchor = anchor;
    public readonly string Parameters = string.Join("&", parameters);
  }
}
