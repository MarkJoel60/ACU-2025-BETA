// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiReader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.DependencyInjection;
using PX.Export.Excel.Core;
using PX.SP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

#nullable disable
namespace PX.SM;

/// <exclude />
[DashboardType(new int[] {1})]
[PXHidden(ServiceVisible = true)]
[FileAttachable(false)]
public class WikiReader : 
  WikiPageReader<WikiPage, WikiPageFilter, Where<WikiPage.pageID, Equal<WikiPage.pageID>>>,
  IGraphWithInitialization
{
  public PXSelect<WikiDescriptor, Where<WikiDescriptor.pageID, Equal<Required<WikiDescriptor.pageID>>>> wikis;
  public PXSelect<UploadFileInfo, Where<UploadFile.primaryPageID, Equal<Current<WikiPage.pageID>>>, OrderBy<Asc<UploadFileInfo.name>>> Attachments;
  public PXSelect<UploadFileInfo, Where<UploadFile.primaryPageID, Equal<Current<WikiPage.pageID>>, And<Where<UploadFile.isHidden, IsNull, Or<UploadFile.isHidden, Equal<False>>>>>, OrderBy<Asc<UploadFileInfo.name>>> AttachmentsVisible;
  public PXSelectJoin<WikiPageLanguage, InnerJoin<Locale, On<WikiPageLanguage.language, Equal<Locale.localeName>>, LeftJoin<WikiReadLanguage, On<WikiReadLanguage.localeID, Equal<Locale.localeName>, And<WikiReadLanguage.wikiID, Equal<Current<WikiPage.wikiID>>>>>>, Where<WikiPageLanguage.pageID, Equal<Current<WikiPage.pageID>>>> Locales;
  public PXFilter<FilesLinkInfo> FilesLink;
  public PXAction<WikiPage> Insert;
  public PXAction<WikiPage> Edit;
  public PXAction<WikiPage> Delete;
  public PXAction<WikiPage> GetFile;
  public PXAction<WikiPage> ViewProps;
  public PXAction<WikiPage> CheckOut;
  public PXAction<WikiPage> UndoCheckOut;
  public PXAction<WikiPage> Prev;
  public PXAction<WikiPage> Next;
  public PXAction<WikiPage> ExpandAll;
  public PXAction<WikiPage> CollapseAll;
  public PXAction<WikiPage> OtherLocales;
  public PXAction<WikiPage> GetFilesLink;
  public PXFilter<WikiReader.TypoParameters> TypoParametersFilter;
  public PXAction<WikiPage> SaveTypoInfo;
  public bool TransateLinksNamed;
  public string FileIdToFind;
  public string FileUrl;

  [PXButton]
  [PXUIField(DisplayName = "Expand All", Visible = false)]
  protected IEnumerable expandAll(PXAdapter adapter) => adapter.Get();

  [PXButton]
  [PXUIField(DisplayName = "Collapse All", Visible = false)]
  protected IEnumerable collapseAll(PXAdapter adapter) => adapter.Get();

  [PXButton(Tooltip = "Select the text to report a typo.")]
  [PXUIField(DisplayName = "Report Typo", MapViewRights = PXCacheRights.Select, MapEnableRights = PXCacheRights.Update)]
  public virtual IEnumerable saveTypoInfo(PXAdapter adapter)
  {
    if (this.TypoParametersFilter.AskExt() == WebDialogResult.OK && !string.IsNullOrEmpty(this.TypoParametersFilter.Current.ComplaintTextParameter))
    {
      List<object> objectList = (List<object>) adapter.Get();
      if (objectList != null && objectList.Count != 0)
      {
        WikiPage wikiPage = (WikiPage) objectList[0];
        if (wikiPage != null)
        {
          string str = this.TypoParametersFilter.Current.ComplaintTextParameter.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&").Replace("&quot;", "\"");
          PXTrace.Logger.ForTelemetry("ScreenInfo", "TYPO").Information<Guid?, Guid?, string>("A TYPO was found - WikiId: {WikiId}, PageId: {PageId}, typo: {TypoText}", wikiPage.WikiID, wikiPage.PageID, str);
        }
      }
    }
    return adapter.Get();
  }

  void IGraphWithInitialization.Initialize()
  {
    this.TransateLinksNamed = false;
    PXUIFieldAttribute.SetEnabled(this.Caches[typeof (WikiDescriptor)], (string) null, false);
    PXUIFieldAttribute.SetReadOnly<UploadFileInfo.name>(this.Caches[typeof (UploadFileInfo)], (object) null);
    PXUIFieldAttribute.SetReadOnly<UploadFileInfo.size>(this.Caches[typeof (UploadFileInfo)], (object) null);
    if (!PortalHelper.IsPortalContext())
      return;
    Guid userId = PXAccess.GetUserID();
    if (userId == Guid.Empty)
      return;
    Users users = (Users) PXSelectBase<Users, PXSelect<Users, Where<Users.pKID, Equal<Required<Users.pKID>>>>.Config>.Select((PXGraph) this, (object) userId);
    if (users == null)
      return;
    bool? guest = users.Guest;
    bool flag = true;
    if (!(guest.GetValueOrDefault() == flag & guest.HasValue))
      return;
    this.Actions["Insert"].SetVisible(false);
    this.Actions["Edit"].SetVisible(false);
    this.Actions["Delete"].SetVisible(false);
    this.Actions["GetFilesLink"].SetVisible(false);
    this.Actions["Prev"].SetVisible(false);
    this.Actions["Next"].SetVisible(false);
    this.OtherLocales.SetVisible(false);
  }

  internal IEnumerable attachments()
  {
    return this.GetAttachments<Where<UploadFile.primaryPageID, Equal<Current<WikiPage.pageID>>>>();
  }

  internal IEnumerable attachmentsVisible()
  {
    return this.GetAttachments<Where<UploadFile.primaryPageID, Equal<Current<WikiPage.pageID>>, And<Where<UploadFile.isHidden, IsNull, Or<UploadFile.isHidden, Equal<False>>>>>>();
  }

  private IEnumerable GetAttachments<Where>() where Where : IBqlWhere, new()
  {
    WikiReader graph = this;
    graph.Attachments.Cache.Clear();
    foreach (PXResult<UploadFile, UploadFileRevisionNoData, Users> pxResult in PXSelectBase<UploadFile, PXSelectJoin<UploadFile, LeftJoin<UploadFileRevisionNoData, On<UploadFile.lastRevisionID, Equal<UploadFileRevisionNoData.fileRevisionID>, And<UploadFile.fileID, Equal<UploadFileRevisionNoData.fileID>>>, LeftJoin<Users, On<UploadFile.checkedOutBy, Equal<Users.pKID>>>>, Where>.Config>.Select((PXGraph) graph))
    {
      UploadFileInfo uploadFileInfo = new UploadFileInfo((UploadFile) pxResult, (UploadFileRevision) (UploadFileRevisionNoData) pxResult);
      if (uploadFileInfo != null && !string.IsNullOrEmpty(uploadFileInfo.Name))
      {
        int num = uploadFileInfo.Name.IndexOf('\\');
        if (num > -1 && num + 1 < uploadFileInfo.Name.Length)
          uploadFileInfo.Name = uploadFileInfo.Name.Substring(num + 1);
      }
      uploadFileInfo.Icon = graph.DetermineIcon(uploadFileInfo.Name);
      uploadFileInfo.CheckedOutBy = ((Users) pxResult).Username;
      UploadFileInfo attachment = (UploadFileInfo) graph.Attachments.Cache.Insert((object) uploadFileInfo);
      if (attachment != null)
      {
        graph.Attachments.Cache.IsDirty = false;
        yield return (object) attachment;
      }
    }
  }

  internal void UploadFileInfo_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    UploadFileInfo row = (UploadFileInfo) e.Row;
    if (this.Attachments.Ask("Delete File", "Are you sure you want to delete this file? (You won't be able to undo these changes.)", MessageButtons.YesNo, MessageIcon.Warning) != WebDialogResult.Yes)
      e.Cancel = true;
    else if (PXSelectBase<WikiFileInPage, PXSelect<WikiFileInPage, Where<WikiFileInPage.fileID, Equal<Required<WikiFileInPage.fileID>>, And<WikiFileInPage.pageID, NotEqual<Current<WikiPage.pageID>>>>>.Config>.Select((PXGraph) this, (object) row.FileID).Count > 0 && this.Attachments.Ask("Delete File", "Are you sure you want to delete this file? (You won't be able to undo these changes.)", MessageButtons.YesNo, MessageIcon.Warning) != WebDialogResult.Yes)
      e.Cancel = true;
    else
      UploadFileMaintenance.DeleteFile(row.FileID);
  }

  [PXButton(Tooltip = "Download this file.", ImageKey = "GetFile")]
  [PXUIField(DisplayName = "Download", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected IEnumerable getFile(PXAdapter adapter)
  {
    throw new PXRedirectToUrlException($"{this.FileUrl}?fileid={GUID.CreateGuid(this.FileIdToFind).GetValueOrDefault().ToString()}&fdwnld=1", PXBaseRedirectException.WindowMode.Same, true, "");
  }

  [PXButton(Tooltip = "Navigate to the File Properties form.", ImageKey = "DataEntry")]
  [PXUIField(DisplayName = "Properties", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected IEnumerable viewProps(PXAdapter adapter)
  {
    throw new PXRedirectToUrlException($"{this.FileUrl}?fileid={GUID.CreateGuid(this.FileIdToFind).GetValueOrDefault().ToString()}", PXBaseRedirectException.WindowMode.Same, "");
  }

  [PXButton(Tooltip = "Check out the currently selected file.", ImageKey = "CheckOut")]
  [PXUIField(DisplayName = "Check Out", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected IEnumerable checkOut(PXAdapter adapter)
  {
    WikiFileMaintenance instance = PXGraph.CreateInstance<WikiFileMaintenance>();
    instance.Files.Current = (UploadFileWithIDSelector) instance.Files.Select((object) this.FileIdToFind);
    instance.CheckOutFile(false);
    return adapter.Get();
  }

  [PXButton(Tooltip = "Cancel file check-out.", ImageKey = "UndoCheckOut")]
  [PXUIField(DisplayName = "Undo Check Out", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected IEnumerable undoCheckOut(PXAdapter adapter)
  {
    WikiFileMaintenance instance = PXGraph.CreateInstance<WikiFileMaintenance>();
    instance.Files.Current = (UploadFileWithIDSelector) instance.Files.Select((object) this.FileIdToFind);
    instance.UndoCheckOutFile();
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Get File Link", Visible = false)]
  protected IEnumerable getFilesLink(PXAdapter adapter)
  {
    Guid? pageId = this.Pages.Current.PageID;
    if (pageId.HasValue)
    {
      string locVirtualPath = "/";
      if (!string.IsNullOrEmpty(this.AppVirtualPath))
        locVirtualPath = this.AppVirtualPath;
      locVirtualPath = WikiReader.EnsureHttpsPath(locVirtualPath);
      PXResultset<WikiDescriptor> pxResultset = this.wikis.Select((object) this.Pages.Current.WikiID);
      string pubVirtualPath = (string) null;
      if (pxResultset != null && pxResultset.Count != 0)
      {
        string pubVirtualPath1 = ((WikiDescriptor) pxResultset[0][0]).PubVirtualPath;
        if (!string.IsNullOrEmpty(pubVirtualPath1))
          pubVirtualPath = WikiReader.EnsureHttpsPath(pubVirtualPath1);
      }
      int num = (int) this.FilesLink.AskExt((PXView.InitializePanel) ((graph, name) =>
      {
        string wikiPagePath = WikiFileProvider.GetWikiPagePath(pageId.Value);
        this.FilesLink.Current.InternalPath = Utils.CombinePaths(locVirtualPath, wikiPagePath);
        if (pubVirtualPath == null)
        {
          PXUIFieldAttribute.SetEnabled<FilesLinkInfo.externalPath>(this.FilesLink.Cache, (object) null, false);
          this.FilesLink.Current.ExternalPath = string.Empty;
        }
        else
        {
          PXUIFieldAttribute.SetEnabled<FilesLinkInfo.externalPath>(this.FilesLink.Cache, (object) null, true);
          this.FilesLink.Current.ExternalPath = Utils.CombinePaths(pubVirtualPath, wikiPagePath);
        }
      }), true);
    }
    return adapter.Get();
  }

  private static string EnsureHttpsPath(string locVirtualPath)
  {
    int startIndex;
    if ((startIndex = locVirtualPath.IndexOf(Uri.SchemeDelimiter, StringComparison.OrdinalIgnoreCase)) > -1)
      locVirtualPath = "https" + locVirtualPath.Substring(startIndex);
    return locVirtualPath;
  }

  public string AppVirtualPath { get; set; }

  private string DetermineIcon(string name)
  {
    if (string.IsNullOrEmpty(name))
      return "PX.Web.UI.Images.Extensions.binary.gif";
    string lower = name.Substring(System.Math.Max(0, name.LastIndexOf('.'))).ToLower();
    if (lower != null)
    {
      switch (lower.Length)
      {
        case 4:
          switch (lower[2])
          {
            case 'a':
              if (lower == ".rar" || lower == ".cab")
                break;
              goto label_37;
            case 'd':
              switch (lower)
              {
                case ".mdb":
                  return "PX.Web.UI.Images.Extensions.mdb.gif";
                case ".pdf":
                  return "PX.Web.UI.Images.Extensions.pdf.gif";
                default:
                  goto label_37;
              }
            case 'e':
              if (lower == ".cer")
                return "PX.Web.UI.Images.Extensions.cer.gif";
              goto label_37;
            case 'f':
              if (lower == ".pfx")
                return "PX.Web.UI.Images.Extensions.pfx.gif";
              goto label_37;
            case 'i':
              switch (lower)
              {
                case ".gif":
                  return "PX.Web.UI.Images.Extensions.gif.gif";
                case ".zip":
                  break;
                default:
                  goto label_37;
              }
              break;
            case 'l':
              if (lower == ".xls")
                goto label_36;
              goto label_37;
            case 'n':
              if (lower == ".png")
                return "PX.Web.UI.Images.Extensions.Image.gif";
              goto label_37;
            case 'o':
              if (lower == ".doc")
                goto label_24;
              goto label_37;
            case 'p':
              switch (lower)
              {
                case ".jpg":
                  return "PX.Web.UI.Images.Extensions.jpg.gif";
                case ".ppt":
                  goto label_32;
                default:
                  goto label_37;
              }
            case 'q':
              if (lower == ".sql")
                return "PX.Web.UI.Images.Extensions.sql.gif";
              goto label_37;
            case 'r':
              if (lower == ".arj")
                break;
              goto label_37;
            case 's':
              if (lower == ".msi")
                return "PX.Web.UI.Images.Extensions.msi.gif";
              goto label_37;
            case 't':
              if (lower == ".rtf")
                goto label_24;
              goto label_37;
            case 'x':
              if (lower == ".txt")
                return "PX.Web.UI.Images.Extensions.txt.gif";
              goto label_37;
            default:
              goto label_37;
          }
          return "PX.Web.UI.Images.Extensions.rar.gif";
        case 5:
          switch (lower[1])
          {
            case 'd':
              if (lower == ".docx")
                break;
              goto label_37;
            case 'p':
              if (lower == ".pptx")
                goto label_32;
              goto label_37;
            case 'x':
              if (lower == ".xlsx")
                goto label_36;
              goto label_37;
            default:
              goto label_37;
          }
          break;
        default:
          goto label_37;
      }
label_24:
      return "PX.Web.UI.Images.Extensions.doc.gif";
label_32:
      return "PX.Web.UI.Images.Extensions.ppt.gif";
label_36:
      return "PX.Web.UI.Images.Extensions.xls.gif";
    }
label_37:
    return "PX.Web.UI.Images.Extensions.binary.gif";
  }

  public void InitReader(string url)
  {
    foreach (Match match in Wiki.RegexQueryString.Matches(url))
      this.Filter.Cache.SetValueExt((object) this.Filter.Current, match.Groups["Name"].Value, (object) HttpUtility.UrlDecode(match.Groups["Value"].Value));
  }

  public Guid? GetArticleID(string wikiName, string name)
  {
    this.Clear();
    this.Filter.Current.Art = name;
    this.Filter.Current.Wiki = wikiName;
    this.Pages.Current = (WikiPage) this.Pages.Select();
    return this.Pages.Current != null ? this.Pages.Current.PageID : new Guid?();
  }

  public static int? GetArticleType(Guid pageID)
  {
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<WikiPage>(new PXDataField("ArticleType"), (PXDataField) new PXDataFieldValue("PageID", PXDbType.UniqueIdentifier, (object) pageID)))
      return pxDataRecord.GetInt32(0);
  }

  [PXButton(Tooltip = "Show Previous Article (PgUp)", ShortcutChar = '!', ImageKey = "PagePrev", ConfirmationMessage = "Any unsaved changes will be discarded.")]
  [PXUIField(DisplayName = "Prev", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected IEnumerable prev(PXAdapter e)
  {
    List<object> objectList = (List<object>) e.Get();
    if (objectList != null && objectList.Count != 0)
    {
      WikiPage wikiPage = (WikiPage) objectList[0];
      if (wikiPage != null)
      {
        WikiPage current = (WikiPage) PXSelectBase<WikiPage, PXSelect<WikiPage, Where<WikiPage.parentUID, Equal<Required<WikiPage.parentUID>>, And<WikiPage.number, Less<Required<WikiPage.number>>>>, OrderBy<Desc<WikiPage.articleType, Desc<WikiPage.number>>>>.Config>.Select((PXGraph) this, (object) wikiPage.ParentUID, (object) wikiPage.Number);
        if (current == null)
        {
          Guid? parentUid = wikiPage.ParentUID;
          Guid? wikiId = wikiPage.WikiID;
          if ((parentUid.HasValue == wikiId.HasValue ? (parentUid.HasValue ? (parentUid.GetValueOrDefault() != wikiId.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
            WikiActions.View(wikiPage.ParentUID);
        }
        if (current != null)
          WikiActions.View(current);
      }
    }
    return e.Get();
  }

  [PXButton(Tooltip = "Show Next Article (PgDn)", ShortcutChar = '"', ImageKey = "PageNext", ConfirmationMessage = "Any unsaved changes will be discarded.")]
  [PXUIField(DisplayName = "Next", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected IEnumerable next(PXAdapter e)
  {
    List<object> objectList = (List<object>) e.Get();
    if (objectList != null && objectList.Count != 0 && objectList[0] is WikiPage wikiPage1)
    {
      PXSelectBase<WikiPage> pxSelectBase1 = (PXSelectBase<WikiPage>) new PXSelect<WikiPage, Where<WikiPage.parentUID, Equal<Required<WikiPage.parentUID>>, And<WikiPage.number, Greater<Required<WikiPage.number>>>>, OrderBy<Desc<WikiPage.articleType, Asc<WikiPage.number>>>>((PXGraph) this);
      PXSelectBase<WikiPage> pxSelectBase2 = (PXSelectBase<WikiPage>) new PXSelect<WikiPage, Where<WikiPage.pageID, Equal<Required<WikiPage.pageID>>>>((PXGraph) this);
      WikiPage wikiPage = (WikiPage) pxSelectBase1.Select((object) wikiPage1.PageID, (object) 0.0);
      if (wikiPage != null)
        throw new PXRedirectToUrlException(Wiki.Url(wikiPage.PageID), "Move next");
      WikiPage current = (WikiPage) pxSelectBase1.Select((object) wikiPage1.ParentUID, (object) wikiPage1.Number);
      if (current != null)
        throw new PXRedirectToUrlException(Wiki.Url(current.PageID), "Move next");
      while (true)
      {
        Guid? parentUid = wikiPage1.ParentUID;
        Guid? wikiId = wikiPage1.WikiID;
        if ((parentUid.HasValue == wikiId.HasValue ? (parentUid.HasValue ? (parentUid.GetValueOrDefault() != wikiId.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0 && current == null)
        {
          wikiPage1 = (WikiPage) pxSelectBase2.Select((object) wikiPage1.ParentUID);
          current = (WikiPage) pxSelectBase1.Select((object) wikiPage1.ParentUID, (object) wikiPage1.Number);
        }
        else
          break;
      }
      if (current != null)
        WikiActions.View(current);
    }
    return e.Get();
  }

  [PXButton(Tooltip = "Add New Article", ImageKey = "AddNew", ConfirmationMessage = "Any unsaved changes will be discarded.")]
  [PXUIField(DisplayName = "New")]
  protected virtual IEnumerable insert(PXAdapter e)
  {
    foreach (WikiPage page in e.Get())
      this.PerformInsert((WikiDescriptor) this.wikis[new object[1]
      {
        (object) page.WikiID
      }], page);
    return e.Get();
  }

  protected virtual void PerformInsert(WikiDescriptor wiki, WikiPage page)
  {
    WikiActions.Insert(wiki, page, (string) null);
  }

  [PXButton(Tooltip = "Edit Current Article", ImageKey = "DataEntry", ConfirmationMessage = "Any unsaved changes will be discarded.")]
  [PXUIField(DisplayName = " ", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected virtual IEnumerable edit(PXAdapter e)
  {
    bool flag = false;
    foreach (WikiPage current in e.Get())
    {
      flag = true;
      WikiActions.Edit((WikiDescriptor) this.wikis[new object[1]
      {
        (object) current.WikiID
      }], current);
    }
    if (!flag)
    {
      WikiPage current = (WikiPage) PXSelectBase<WikiPage, PXSelect<WikiPage, Where<WikiPage.pageID, Equal<Required<WikiPage.pageID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) GUID.CreateGuid(this.Filter.Current.Parent));
      WikiActions.Insert((WikiDescriptor) this.wikis[new object[1]
      {
        (object) PXSiteMap.WikiProvider.GetWikiID(this.Filter.Current.Wiki)
      }], current, this.Filter.Current.Art);
    }
    return e.Get();
  }

  [PXButton(Tooltip = "Delete Article", ImageKey = "Remove")]
  [PXUIField(DisplayName = "Delete", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  protected virtual IEnumerable delete(PXAdapter e)
  {
    if (this.Pages.Ask("Delete Article", "Are you sure you want to delete this article and all its children?", MessageButtons.YesNo, MessageIcon.Question) == WebDialogResult.Yes)
    {
      foreach (WikiPage page in e.Get())
        this.PerformDelete(page);
    }
    return e.Get();
  }

  protected virtual void PerformDelete(WikiPage page) => WikiActions.Delete(page);

  [PXButton(Tooltip = "Change the language.", ConfirmationMessage = "Any unsaved changes will be discarded.")]
  [PXUIField(DisplayName = "Locales", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected IEnumerable otherLocales(PXAdapter e)
  {
    foreach (PXResult<WikiPageLanguage, Locale> pxResult in this.Locales.Select())
    {
      if (((Locale) pxResult).TranslatedName == e.Menu)
      {
        this.Filter.Current.Language = ((WikiPageLanguage) pxResult).Language;
        break;
      }
    }
    return e.Get();
  }

  protected void WikiPage_RowSelected(PXCache cahce, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    WikiPage row = (WikiPage) e.Row;
    Guid? nullable1 = row.PageID;
    Guid? nullable2 = nullable1.HasValue || string.IsNullOrEmpty(this.Filter.Current.Wiki) ? row.PageID : new Guid?(PXSiteMap.WikiProvider.GetWikiID(this.Filter.Current.Wiki));
    PXWikiProvider wikiProvider1 = PXSiteMap.WikiProvider;
    nullable1 = nullable2;
    Guid pageID1 = nullable1 ?? Guid.Empty;
    PXWikiRights accessRights = wikiProvider1.GetAccessRights(pageID1);
    PXWikiRights pxWikiRights = accessRights;
    bool? folder = row.Folder;
    bool flag = true;
    if (!(folder.GetValueOrDefault() == flag & folder.HasValue))
    {
      PXWikiProvider wikiProvider2 = PXSiteMap.WikiProvider;
      nullable1 = row.ParentUID;
      Guid pageID2 = nullable1 ?? Guid.Empty;
      pxWikiRights = wikiProvider2.GetAccessRights(pageID2);
    }
    nullable1 = row.PageID;
    if (nullable1.HasValue)
    {
      this.Insert.SetEnabled(pxWikiRights >= PXWikiRights.Insert);
      this.Edit.SetEnabled(accessRights >= PXWikiRights.Update);
      this.Delete.SetEnabled(accessRights >= PXWikiRights.Delete);
    }
    else
    {
      this.Insert.SetEnabled(false);
      this.Edit.SetEnabled(accessRights >= PXWikiRights.Insert);
      this.Delete.SetEnabled(false);
    }
  }

  protected override void OnPageSelected(PXCache sender, WikiPage row, PXWikiRights accessRights)
  {
    base.OnPageSelected(sender, row, accessRights);
  }

  [Serializable]
  public class TypoParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString(IsUnicode = true)]
    public virtual string ComplaintTextParameter { get; set; }

    public abstract class complaintTextParameterParameter : IBqlField, IBqlOperand
    {
    }
  }
}
