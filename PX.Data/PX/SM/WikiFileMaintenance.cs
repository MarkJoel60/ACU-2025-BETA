// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiFileMaintenance
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.Wiki.ExternalFiles;
using PX.Data.Wiki.Parser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Compilation;

#nullable enable
namespace PX.SM;

/// <exclude />
[Serializable]
public class WikiFileMaintenance : PXGraph<
#nullable disable
WikiFileMaintenance>, ICaptionable
{
  private readonly EntityHelper entity;
  public WikiFileMaintenance.PXSelectFile Files;
  public WikiFileMaintenance.PXSelectCurrentFile CurrentFile;
  public PXSelectSiteMapTree<False, False, False, False, False> SiteMap;
  public PXFilter<WikiFileMaintenance.CheckOutComment> CheckoutComment;
  public PXSave<UploadFileWithIDSelector> Save;
  public PXCancel<UploadFileWithIDSelector> Cancel;
  public PXDelete<UploadFileWithIDSelector> Delete;
  public PXAction<UploadFileWithIDSelector> CheckOut;
  public PXAction<UploadFileWithIDSelector> UndoCheckOut;
  public PXAction<UploadFileWithIDSelector> UploadNewVersion;
  public PXAction<UploadFileWithIDSelector> GetLatest;
  public PXSelect<UploadFileWithIDSelector> NewRevisionPanel;
  public PXSelect<UploadFileRevision> RevisionsWithData;
  public PXSelectOrderBy<UploadFileRevisionNoData, OrderBy<Desc<UploadFileRevisionNoData.fileRevisionID>>> Revisions;
  public PXAction<UploadFileWithIDSelector> ViewRevision;
  public PXSelectJoin<WikiFileInPage, InnerJoin<WikiPage, On<WikiPage.pageID, Equal<WikiFileInPage.pageID>>>, Where<WikiFileInPage.fileID, Equal<Current<PX.SM.UploadFile.fileID>>>, OrderBy<Desc<WikiFileInPage.pageRevisionID>>> Articles;
  public PXSelectJoin<WikiPage, InnerJoin<PX.SM.UploadFile, On<WikiPage.pageID, Equal<PX.SM.UploadFile.primaryPageID>>>, Where<PX.SM.UploadFile.fileID, Equal<Current<PX.SM.UploadFile.fileID>>>> PrimaryArticle;
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoin<WikiFileInPage, LeftJoin<WikiPage, On<WikiPage.pageID, Equal<WikiFileInPage.pageID>>>, Where<WikiPage.pageID, Equal<WikiPage.pageID>>, OrderBy<Desc<WikiFileInPage.pageRevisionID>>> PagesWithFile;
  public PXAction<UploadFileWithIDSelector> OpenArticle;
  public PXSelect<WikiPageSimple, Where<WikiPageSimple.pageID, Equal<WikiPageSimple.pageID>>, OrderBy<Desc<WikiPage.articleType, Asc<WikiPage.number>>>> Pages;
  public PXSelect<WikiAccessRights, Where<WikiAccessRights.pageID, Equal<Current<UploadFileWithIDSelector.primaryPageID>>>> WikiAccessRightsRecs;
  public PXAction<UploadFileWithIDSelector> ViewEntity;
  public PXSelect<NoteDoc> EntitiesRecords;
  public PXAction<UploadFileWithIDSelector> UploadFile;
  public PXAction<UploadFileWithIDSelector> DownloadFile;

  public string Caption()
  {
    return (this.Caches["UploadFileWithIDSelector"].Current is UploadFileWithIDSelector current ? current.Name : (string) null) ?? string.Empty;
  }

  public WikiFileMaintenance()
  {
    this.Files.Cache.AllowInsert = false;
    this.entity = new EntityHelper((PXGraph) this);
    PXUIFieldAttribute.SetEnabled<PX.SM.UploadFile.checkedOutBy>(this.Files.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<PX.SM.UploadFile.checkedOutComment>(this.Files.Cache, (object) null, false);
    PXDatabase.Subscribe(typeof (PX.SM.UploadFile), (PXDatabaseTableChanged) (() => { }), nameof (WikiFileMaintenance));
  }

  internal void UploadFileWithIDSelector_RowSelecting(PXCache cache, PXRowSelectingEventArgs e)
  {
    UploadFileWithIDSelector row = (UploadFileWithIDSelector) e.Row;
    if (!row.CheckedOutBy.HasValue)
      return;
    Guid? checkedOutBy = row.CheckedOutBy;
    Guid userId = this.Accessinfo.UserID;
    if ((checkedOutBy.HasValue ? (checkedOutBy.HasValue ? (checkedOutBy.GetValueOrDefault() != userId ? 1 : 0) : 0) : 1) == 0)
      return;
    this.UploadNewVersion.SetEnabled(false);
  }

  internal void UploadFileWithIDSelector_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    UploadFileWithIDSelector row = (UploadFileWithIDSelector) e.Row;
    Guid? nullable1;
    if (row.PrimaryPageID.HasValue)
    {
      nullable1 = row.SelectedWikiID;
      if (!nullable1.HasValue && row.PrimaryScreenID == null)
      {
        WikiPageSimple wikiPageSimple = (WikiPageSimple) PXSelectBase<WikiPageSimple, PXSelect<WikiPageSimple, Where<WikiPageSimple.pageID, Equal<Required<WikiPageSimple.pageID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) row.PrimaryPageID);
        if (wikiPageSimple != null)
        {
          row.SelectedWikiID = wikiPageSimple.WikiID;
          row.SelectedPageID = wikiPageSimple.PageID;
        }
      }
    }
    UploadFileRevision uploadFileRevision = (UploadFileRevision) (UploadFileRevisionNoData) this.Revisions.Search<PX.SM.UploadFile.fileRevisionID>((object) row.LastRevisionID);
    if (uploadFileRevision != null)
      row.Comment = uploadFileRevision.Comment;
    PXCacheRights pxCacheRights = UploadFileMaintenance.AccessRights((PX.SM.UploadFile) row);
    PXCacheRights rights;
    PXAccess.Provider.GetRights(cache, out rights, out List<string> _, out List<string> _);
    if (rights >= pxCacheRights)
    {
      nullable1 = row.PrimaryPageID;
      if (nullable1.HasValue || row.PrimaryScreenID != null)
        goto label_9;
    }
    pxCacheRights = rights;
label_9:
    this.GetLatest.SetEnabled(pxCacheRights >= PXCacheRights.Select);
    PXAction<UploadFileWithIDSelector> checkOut = this.CheckOut;
    int num1;
    if (pxCacheRights >= PXCacheRights.Update)
    {
      nullable1 = row.CheckedOutBy;
      num1 = !nullable1.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    checkOut.SetEnabled(num1 != 0);
    PXAction<UploadFileWithIDSelector> undoCheckOut = this.UndoCheckOut;
    int num2;
    if (pxCacheRights >= PXCacheRights.Update)
    {
      nullable1 = row.CheckedOutBy;
      Guid userId = this.Accessinfo.UserID;
      num2 = nullable1.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == userId ? 1 : 0) : 1) : 0;
    }
    else
      num2 = 0;
    undoCheckOut.SetEnabled(num2 != 0);
    PXDelete<UploadFileWithIDSelector> delete = this.Delete;
    int num3;
    switch (pxCacheRights)
    {
      case PXCacheRights.Denied:
        num3 = UploadFileMaintenance.OverrideAccessRights((PX.SM.UploadFile) row) ? 1 : 0;
        break;
      case PXCacheRights.Delete:
        num3 = 1;
        break;
      default:
        num3 = 0;
        break;
    }
    delete.SetEnabled(num3 != 0);
    PXUIFieldAttribute.SetEnabled<UploadFileWithIDSelector.selectedWikiID>(cache, (object) row, pxCacheRights == PXCacheRights.Delete && cache.AllowDelete);
    PXUIFieldAttribute.SetEnabled<UploadFileWithIDSelector.selectedPageID>(cache, (object) row, pxCacheRights == PXCacheRights.Delete && cache.AllowDelete);
    PXUIFieldAttribute.SetEnabled<UploadFileWithIDSelector.primaryScreenID>(cache, (object) row, pxCacheRights == PXCacheRights.Delete && cache.AllowDelete);
    row.AccessRights = new short?((short) pxCacheRights);
    this.WikiAccessRightsRecs.View.RequestRefresh();
    PXUIFieldAttribute.SetEnabled<PX.SM.UploadFile.sourceType>(cache, (object) row, row.Synchronizable.GetValueOrDefault());
    PXCache cache1 = cache;
    UploadFileWithIDSelector data1 = row;
    bool? synchronizable = row.Synchronizable;
    int num4 = synchronizable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PX.SM.UploadFile.sourceUri>(cache1, (object) data1, num4 != 0);
    PXCache cache2 = cache;
    UploadFileWithIDSelector data2 = row;
    synchronizable = row.Synchronizable;
    int num5 = synchronizable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PX.SM.UploadFile.sourceLogin>(cache2, (object) data2, num5 != 0);
    PXCache cache3 = cache;
    UploadFileWithIDSelector data3 = row;
    bool? nullable2 = row.Synchronizable;
    int num6 = nullable2.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PX.SM.UploadFile.sourcePassword>(cache3, (object) data3, num6 != 0);
    PXCache cache4 = cache;
    UploadFileWithIDSelector data4 = row;
    nullable2 = row.Synchronizable;
    int num7 = nullable2.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PX.SM.UploadFile.sourceNamingFormat>(cache4, (object) data4, num7 != 0);
    PXCache cache5 = cache;
    UploadFileWithIDSelector data5 = row;
    nullable2 = row.Synchronizable;
    int num8 = !nullable2.GetValueOrDefault() ? 0 : (row.SourceType == "C" ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<PX.SM.UploadFile.sshCertificateName>(cache5, (object) data5, num8 != 0);
    PXCache cache6 = cache;
    UploadFileWithIDSelector data6 = row;
    nullable2 = row.Synchronizable;
    int num9 = !nullable2.GetValueOrDefault() ? 0 : (SynchronizationProcess.ListingAllowed((PX.SM.UploadFile) row) ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<PX.SM.UploadFile.sourceIsFolder>(cache6, (object) data6, num9 != 0);
    PXCache cache7 = cache;
    UploadFileWithIDSelector data7 = row;
    nullable2 = row.Synchronizable;
    int num10;
    if (nullable2.GetValueOrDefault())
    {
      nullable2 = row.SourceIsFolder;
      if (nullable2.GetValueOrDefault())
      {
        num10 = SynchronizationProcess.ListingAllowed((PX.SM.UploadFile) row) ? 1 : 0;
        goto label_23;
      }
    }
    num10 = 0;
label_23:
    PXUIFieldAttribute.SetEnabled<PX.SM.UploadFile.sourceMask>(cache7, (object) data7, num10 != 0);
    this.UploadFile.SetEnabled(SynchronizationProcess.UploadAllowed((PX.SM.UploadFile) row));
    this.DownloadFile.SetEnabled(SynchronizationProcess.DownloadAllowed((PX.SM.UploadFile) row));
    nullable2 = row.IsPublic;
    bool isEnabled = !nullable2.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<UploadFileWithIDSelector.selectedWikiID>(cache, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<UploadFileWithIDSelector.selectedPageID>(cache, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<UploadFileWithIDSelector.primaryScreenID>(cache, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<PX.SM.UploadFile.isAccessRightsFromEntities>(cache, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<WikiAccessRights.roleName>(this.WikiAccessRightsRecs.Cache, (object) null, isEnabled);
    PXUIFieldAttribute.SetEnabled<WikiAccessRights.accessRights>(this.WikiAccessRightsRecs.Cache, (object) null, isEnabled);
    this.InitializeLinkFields(row);
  }

  internal void UploadFileWithIDSelector_PrimaryScreenID_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null)
      return;
    e.NewValue = (object) ((string) e.NewValue).Replace(".", "");
    PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(e.NewValue as string);
    if (mapNodeByScreenId == null || string.IsNullOrEmpty(mapNodeByScreenId.GraphType) || PXBuildManager.GetType(mapNodeByScreenId.GraphType, false) == (System.Type) null)
      throw new PXSetPropertyException("This item cannot be selected as a primary screen.");
  }

  internal void UploadFileWithIDSelector_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    UploadFileWithIDSelector row = (UploadFileWithIDSelector) e.Row;
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Delete)
    {
      row.PrimaryPageID = row.SelectedPageID;
    }
    else
    {
      UploadFileMaintenance.DeleteFile(row.FileID);
      e.Cancel = true;
    }
  }

  internal void UploadFileWithIDSelector_RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
  {
    if (e.Operation != PXDBOperation.Delete)
      return;
    ((WikiFileMaintenance.ViewFile) this.Files.View).ShowFiles = false;
    this.Cancel.SetEnabled(false);
    this.Save.SetEnabled(false);
    this.Delete.SetEnabled(false);
    this.CheckOut.SetEnabled(false);
    this.UndoCheckOut.SetEnabled(false);
    this.UploadNewVersion.SetEnabled(false);
    this.GetLatest.SetEnabled(false);
  }

  internal void UploadFileWithIDSelector_RowUpdating(PXCache cache, PXRowUpdatingEventArgs e)
  {
    UploadFileWithIDSelector row = (UploadFileWithIDSelector) e.Row;
    UploadFileWithIDSelector newRow = (UploadFileWithIDSelector) e.NewRow;
    bool? isPublic1 = row.IsPublic;
    bool? isPublic2 = newRow.IsPublic;
    newRow.IsPublicChanged = new bool?(!(isPublic1.GetValueOrDefault() == isPublic2.GetValueOrDefault() & isPublic1.HasValue == isPublic2.HasValue));
  }

  [PXButton(Tooltip = "Check out the currently selected file.", Category = "Actions", DisplayOnMainToolbar = true)]
  [PXUIField(DisplayName = "Check Out", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected IEnumerable checkOut(PXAdapter a)
  {
    this.CheckOutFile(true);
    return a.Get();
  }

  internal void CheckOutFile(bool showCommentDlg)
  {
    if (this.Files.Current == null)
      return;
    if (this.Files.Current.CheckedOutBy.HasValue)
    {
      Users users = (Users) PXSelectBase<Users, PXSelect<Users, Where<Users.pKID, Equal<Current<PX.SM.UploadFile.checkedOutBy>>>>.Config>.Select((PXGraph) this);
      if (users != null)
        throw new PXException("This file is already checked out by '{0}'.", new object[1]
        {
          (object) users.Username
        });
    }
    else
    {
      if (UploadFileMaintenance.AccessRights((PX.SM.UploadFile) this.Files.Current) < PXCacheRights.Update)
        throw new PXException("You don't have enough rights to check out this file.");
      if (showCommentDlg && this.CheckoutComment.AskExt() != WebDialogResult.OK)
        return;
      this.Files.Current.CheckedOutBy = new Guid?(this.Accessinfo.UserID);
      this.Files.Current.CheckedOutComment = this.CheckoutComment.Current != null ? this.CheckoutComment.Current.Comment : (string) null;
      this.Files.Update(this.Files.Current);
      this.Actions.PressSave();
    }
  }

  [PXButton(Tooltip = "Cancel file check-out.", Category = "Actions", DisplayOnMainToolbar = true)]
  [PXUIField(DisplayName = "Undo Check Out", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected IEnumerable undoCheckOut(PXAdapter a)
  {
    this.UndoCheckOutFile();
    return a.Get();
  }

  internal void UndoCheckOutFile()
  {
    if (this.Files.Current != null)
    {
      Guid? nullable1 = this.Files.Current.CheckedOutBy;
      Guid userId = this.Accessinfo.UserID;
      if ((nullable1.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != userId ? 1 : 0) : 0) : 1) == 0 || PXContext.PXIdentity.User.IsInRole(((IEnumerable<string>) PXAccess.GetAdministratorRoles()).First<string>()))
      {
        UploadFileWithIDSelector current = this.Files.Current;
        nullable1 = new Guid?();
        Guid? nullable2 = nullable1;
        current.CheckedOutBy = nullable2;
        this.Files.Current.CheckedOutComment = (string) null;
        this.Files.Update(this.Files.Current);
        this.Actions.PressSave();
        return;
      }
    }
    Users users = (Users) PXSelectBase<Users, PXSelect<Users, Where<Users.pKID, Equal<Current<PX.SM.UploadFile.checkedOutBy>>>>.Config>.Select((PXGraph) this);
    if (users != null)
      throw new PXException("This file is checked out by '{0}'. You cannot undo this operation.", new object[1]
      {
        (object) users.Username
      });
  }

  [PXButton(Tooltip = "Get Latest Version", Category = "Actions", DisplayOnMainToolbar = true)]
  [PXUIField(DisplayName = "Get Latest Version", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected IEnumerable getLatest(PXAdapter a)
  {
    if (this.Files.Current == null)
      return a.Get();
    if (UploadFileMaintenance.AccessRights((PX.SM.UploadFile) this.Files.Current) < PXCacheRights.Select)
      throw new PXException("You don't have enough rights to download this file.");
    this.RedirectToRevision(this.Files.Current.FileID.Value, this.Files.Current.LastRevisionID.Value, true);
    return a.Get();
  }

  [PXButton(Tooltip = "Display a dialog box to upload a new version for the currently selected file.", Category = "Actions", DisplayOnMainToolbar = true)]
  [PXUIField(DisplayName = "Upload New Version", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  public IEnumerable uploadNewVersion(PXAdapter a)
  {
    if (this.NewRevisionPanel.AskExt() == WebDialogResult.OK)
    {
      FileInfo finfo = PXContext.SessionTyped<PXSessionStatePXData>().FileInfo["sessionKey_fileMaintenance"];
      if (finfo != null)
        this.NewRevision(finfo, finfo.CheckIn);
    }
    return a.Get();
  }

  public void NewRevision(FileInfo finfo, bool checkIn)
  {
    UploadFileWithIDSelector current = this.Files.Current;
    if (current == null || finfo.BinData == null)
      return;
    Guid? nullable1 = current.CheckedOutBy;
    if (nullable1.HasValue)
    {
      nullable1 = current.CheckedOutBy;
      Guid userId = this.Accessinfo.UserID;
      if ((nullable1.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != userId ? 1 : 0) : 0) : 1) != 0)
        return;
    }
    if (UploadFileMaintenance.AccessRights((PX.SM.UploadFile) current) < PXCacheRights.Update)
      throw new PXException("You don't have enough rights to upload new revisions of this file.");
    if (!PXPath.GetExtension(current.Name).OrdinalEquals(PXPath.GetExtension(finfo.FullName)))
      throw new PXException("The file you are trying to upload has an extension that differs from the extension that is stored in the database.");
    bool? versioned = current.Versioned;
    bool flag = true;
    if (versioned.GetValueOrDefault() == flag & versioned.HasValue)
    {
      UploadFileWithIDSelector fileWithIdSelector = current;
      int? lastRevisionId = fileWithIdSelector.LastRevisionID;
      fileWithIdSelector.LastRevisionID = lastRevisionId.HasValue ? new int?(lastRevisionId.GetValueOrDefault() + 1) : new int?();
    }
    this.RevisionsWithData.Insert(new UploadFileRevision()
    {
      FileID = current.FileID,
      FileRevisionID = current.LastRevisionID,
      Comment = finfo.Comment,
      Size = new int?(UploadFileHelper.BytesToKilobytes(finfo.BinData.Length)),
      Data = finfo.BinData,
      OriginalName = UploadFileHelper.GetOriginalName(current.Name, finfo.OriginalName)
    });
    if (checkIn)
    {
      UploadFileWithIDSelector fileWithIdSelector = current;
      nullable1 = new Guid?();
      Guid? nullable2 = nullable1;
      fileWithIdSelector.CheckedOutBy = nullable2;
      current.CheckedOutComment = (string) null;
    }
    this.Files.Update(current);
    this.Actions.PressSave();
  }

  private void InitializeLinkFields(UploadFileWithIDSelector file)
  {
    if (file == null)
      return;
    file.ExternalLink = $"{PXUrl.SiteUrlWithPath().TrimEnd('/')}/Frames/GetFile.ashx?fileID={file.FileID}";
    string str = MimeTypes.GetMimeType(file.Name.Substring(System.Math.Max(0, file.Name.LastIndexOf('.')))).StartsWith("image/") ? "Image:" : "{up}";
    file.WikiLink = $"[{str}{PXBlockParser.EncodeSpecialChars(file.Name)}]";
  }

  internal IEnumerable revisions()
  {
    WikiFileMaintenance graph = this;
    PXUIFieldAttribute.SetEnabled<UploadFileRevisionNoData.fileRevisionID>(graph.Revisions.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<UploadFileRevisionNoData.readableSize>(graph.Revisions.Cache, (object) null, false);
    foreach (PXResult<UploadFileRevisionNoData> pxResult in PXSelectBase<UploadFileRevisionNoData, PXSelect<UploadFileRevisionNoData, Where<UploadFileRevisionNoData.fileID, Equal<Current<UploadFileWithIDSelector.fileID>>>, OrderBy<Desc<UploadFileRevisionNoData.fileRevisionID>>>.Config>.Select((PXGraph) graph))
    {
      UploadFileRevisionNoData fileRevisionNoData1 = (UploadFileRevisionNoData) pxResult;
      UploadFileRevisionNoData fileRevisionNoData2 = fileRevisionNoData1;
      int? size = fileRevisionNoData1.Size;
      string dataSize = WikiFileMaintenance.GetDataSize(size.HasValue ? new long?((long) (size.GetValueOrDefault() * 1024 /*0x0400*/)) : new long?());
      fileRevisionNoData2.ReadableSize = dataSize;
      yield return (object) fileRevisionNoData1;
    }
  }

  internal void UploadFileRevisionNoData_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    UploadFileRevisionNoData row = (UploadFileRevisionNoData) e.Row;
    bool flag = true;
    foreach (PXResult<UploadFileRevisionNoData> pxResult in this.Revisions.Select())
    {
      if ((UploadFileRevisionNoData) pxResult != row)
      {
        flag = false;
        break;
      }
    }
    if (flag)
      throw new PXException("The file contains only one revision. This revision cannot be deleted.");
  }

  internal void UploadFileRevisionNoData_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    UploadFileRevisionNoData fileRevisionNoData1 = (UploadFileRevisionNoData) null;
    UploadFileRevisionNoData fileRevisionNoData2 = (UploadFileRevisionNoData) null;
    UploadFileRevisionNoData row = (UploadFileRevisionNoData) e.Row;
    bool flag = false;
    int? nullable = new int?();
    foreach (PXResult<UploadFileRevisionNoData> pxResult in this.Revisions.Select())
    {
      UploadFileRevisionNoData fileRevisionNoData3 = (UploadFileRevisionNoData) pxResult;
      if (fileRevisionNoData1 == null && fileRevisionNoData3 != row && !flag)
        fileRevisionNoData1 = fileRevisionNoData3;
      else if (fileRevisionNoData3 == row && !flag)
        flag = true;
      else if (flag)
      {
        fileRevisionNoData2 = fileRevisionNoData3;
        break;
      }
    }
    this.Files.Current.LastRevisionID = fileRevisionNoData1 == null ? (fileRevisionNoData2 != null ? fileRevisionNoData2.FileRevisionID : new int?(0)) : fileRevisionNoData1.FileRevisionID;
    this.Files.Cache.Update((object) this.Files.Current);
  }

  protected void RedirectToRevision(Guid fileID, int revID, bool forceDownload)
  {
    throw new PXRedirectToFileException(new Guid?(fileID), revID, forceDownload);
  }

  [PXUIField(DisplayName = "View Selected Version")]
  [PXButton(Tooltip = "View the selected file.")]
  protected IEnumerable viewRevision(PXAdapter a)
  {
    UploadFileRevision current = (UploadFileRevision) this.Revisions.Current;
    if (this.Files.Current == null || current == null)
      return a.Get();
    if (UploadFileMaintenance.AccessRights((PX.SM.UploadFile) this.Files.Current) < PXCacheRights.Select && !UploadFileMaintenance.OverrideAccessRights((PX.SM.UploadFile) this.Files.Current))
      throw new PXException("You don't have enough rights to download this file.");
    this.RedirectToRevision(this.Files.Current.FileID.Value, current.FileRevisionID.Value, true);
    return a.Get();
  }

  public static string GetDataSize(long? dataSize)
  {
    int num1 = 0;
    if (!dataSize.HasValue)
      return "0";
    long? nullable1 = dataSize;
    long num2 = 1024 /*0x0400*/;
    if (nullable1.GetValueOrDefault() > num2 & nullable1.HasValue)
      ++num1;
    long? nullable2 = dataSize;
    long num3 = 1048576 /*0x100000*/;
    if (nullable2.GetValueOrDefault() > num3 & nullable2.HasValue)
      ++num1;
    if (num1 == 1)
      return $"{Convert.ToString(System.Math.Round((double) dataSize.Value / 1024.0, 2))} {PXMessages.Localize("KB")}";
    return num1 == 2 ? $"{Convert.ToString(System.Math.Round((double) dataSize.Value / 1048576.0, 2))} {PXMessages.Localize("MB")}" : $"{dataSize.ToString()} {PXMessages.Localize("bytes")}";
  }

  protected IEnumerable pagesWithFile(PXAdapter a)
  {
    Dictionary<WikiFileMaintenance.FileInPageKey, bool> articlesMap = new Dictionary<WikiFileMaintenance.FileInPageKey, bool>();
    foreach (PXResult<WikiFileInPage, WikiPage> file in this.Articles.Select())
    {
      WikiFileMaintenance.FileInPageKey key = new WikiFileMaintenance.FileInPageKey((WikiFileInPage) file);
      if (!articlesMap.ContainsKey(key))
      {
        articlesMap.Add(key, true);
        ((WikiFileInPage) file).IsLatest = true;
      }
      WikiPage wikiPage = (WikiPage) file;
      PXSiteMapNode siteMapNodeFromKey = PXSiteMap.WikiProvider.FindSiteMapNodeFromKey(wikiPage.PageID.Value);
      wikiPage.Title = siteMapNodeFromKey == null ? wikiPage.Name : siteMapNodeFromKey.Title;
      yield return (object) file;
    }
  }

  [PXButton(Tooltip = "Open the selected article.")]
  [PXUIField(DisplayName = "Open")]
  protected IEnumerable openArticle(PXAdapter a)
  {
    if (this.Articles.Current == null || !this.Articles.Current.PageID.HasValue)
      return a.Get();
    throw new PXRedirectToUrlException(PX.SM.Wiki.Url(this.Articles.Current.PageID, this.Articles.Current.PageRevisionID.Value), PXBaseRedirectException.WindowMode.New, true, "View Page Version");
  }

  protected void WikiPage_RowSelecting(PXCache cache, PXRowSelectingEventArgs e)
  {
    WikiPage row = (WikiPage) e.Row;
    if (!row.PageID.HasValue)
      return;
    PXSiteMapNode siteMapNodeFromKey = PXSiteMap.WikiProvider.FindSiteMapNodeFromKey(row.PageID.Value);
    row.Title = siteMapNodeFromKey == null ? row.Name : siteMapNodeFromKey.Title;
    PXUIFieldAttribute.SetDisplayName<WikiPage.title>(cache, "Primary Page");
  }

  public IEnumerable pages([PXString] string PageID)
  {
    WikiFileMaintenance wikiFileMaintenance = this;
    if (wikiFileMaintenance.Files.Current.SelectedWikiID.HasValue)
    {
      Guid? nullable1;
      ref Guid? local = ref nullable1;
      Guid? nullable2 = GUID.CreateGuid(PageID);
      Guid guid = nullable2 ?? wikiFileMaintenance.Files.Current.SelectedWikiID.Value;
      local = new Guid?(guid);
      WikiFileMaintenance graph = wikiFileMaintenance;
      object[] objArray = new object[1]
      {
        (object) nullable1
      };
      foreach (PXResult<WikiPageSimple> pxResult in PXSelectBase<WikiPageSimple, PXSelect<WikiPageSimple, Where<WikiPageSimple.parentUID, Equal<Required<WikiPageSimple.parentUID>>>>.Config>.Select((PXGraph) graph, objArray))
      {
        WikiPageSimple wikiPageSimple = (WikiPageSimple) pxResult;
        PXWikiProvider wikiProvider1 = PXSiteMap.WikiProvider;
        nullable2 = wikiPageSimple.PageID;
        Guid pageID = nullable2.Value;
        if (wikiProvider1.GetAccessRights(pageID) >= PXWikiRights.Select)
        {
          PXWikiProvider wikiProvider2 = PXSiteMap.WikiProvider;
          nullable2 = wikiPageSimple.PageID;
          Guid key = nullable2.Value;
          PXSiteMapNode siteMapNodeFromKey = wikiProvider2.FindSiteMapNodeFromKey(key);
          wikiPageSimple.Title = siteMapNodeFromKey == null ? wikiPageSimple.Name : siteMapNodeFromKey.Title;
          yield return (object) wikiPageSimple;
        }
      }
    }
  }

  public IEnumerable wikiAccessRightsRecs()
  {
    WikiFileMaintenance wikiFileMaintenance = this;
    List<string> rolesAdded = new List<string>();
    if (wikiFileMaintenance.Files.Current != null)
    {
      WikiFileMaintenance graph = wikiFileMaintenance;
      object[] objArray = new object[1]
      {
        (object) PXAccess.Provider.ApplicationName
      };
      foreach (PXResult<Roles> pxResult in PXSelectBase<Roles, PXSelect<Roles, Where<Roles.applicationName, Equal<Required<Roles.applicationName>>>>.Config>.Select((PXGraph) graph, objArray))
      {
        Roles roles = (Roles) pxResult;
        if (!rolesAdded.Contains(roles.Rolename))
        {
          PXDatabaseSiteMapProvider provider = PXSiteMap.Provider as PXDatabaseSiteMapProvider;
          WikiAccessRights wikiAccessRights1 = new WikiAccessRights();
          wikiAccessRights1.PageID = wikiFileMaintenance.Files.Current.SelectedPageID;
          wikiAccessRights1.RoleName = roles.Rolename;
          wikiAccessRights1.ApplicationName = PXAccess.Provider.ApplicationName;
          WikiAccessRights wikiAccessRights2 = wikiAccessRights1;
          Guid? selectedPageId = wikiFileMaintenance.Files.Current.SelectedPageID;
          int num;
          if (!selectedPageId.HasValue)
          {
            num = provider == null || wikiFileMaintenance.Files.Current.PrimaryScreenID == null ? 1 : (int) (short) PX.SM.Wiki.Convert(provider.AccessRights(wikiFileMaintenance.Files.Current.PrimaryScreenID, roles.Rolename));
          }
          else
          {
            PXWikiProvider wikiProvider = PXSiteMap.WikiProvider;
            selectedPageId = wikiFileMaintenance.Files.Current.SelectedPageID;
            Guid pageID = selectedPageId.Value;
            string rolename = roles.Rolename;
            num = (int) (short) wikiProvider.GetAccessRights(pageID, rolename);
          }
          short? nullable = new short?((short) num);
          wikiAccessRights2.AccessRights = nullable;
          yield return (object) wikiAccessRights1;
        }
      }
    }
  }

  private void WikiFieldUpdated(
    Events.FieldUpdated<UploadFileWithIDSelector.selectedWikiID> e)
  {
    UploadFileWithIDSelector row = (UploadFileWithIDSelector) e.Row;
    row.SelectedPageID = new Guid?();
    row.PrimaryScreenID = (string) null;
    this.Pages.View.RequestRefresh();
  }

  private void SelectedPageUpdated(
    Events.FieldUpdated<UploadFileWithIDSelector.selectedPageID> e)
  {
    if ((UploadFileWithIDSelector) e.Row == null)
      return;
    this.WikiAccessRightsRecs.View.RequestRefresh();
  }

  private void PrimaryScreenIDUpdated(
    Events.FieldUpdated<UploadFileWithIDSelector.primaryScreenID> e)
  {
    UploadFileWithIDSelector row = (UploadFileWithIDSelector) e.Row;
    if (row == null)
      return;
    row.SelectedWikiID = new Guid?();
    row.SelectedPageID = new Guid?();
    this.WikiAccessRightsRecs.View.RequestRefresh();
  }

  [PXInternalUseOnly]
  public IEnumerable entitiesRecords()
  {
    List<NoteDoc> noteDocList = new List<NoteDoc>();
    foreach (PXResult<NoteDoc, Note> pxResult in PXSelectBase<NoteDoc, PXSelectJoin<NoteDoc, InnerJoin<Note, On<NoteDoc.noteID, Equal<Note.noteID>>>, Where<NoteDoc.fileID, Equal<Current<UploadFileWithIDSelector.fileID>>>, OrderBy<Asc<NoteDoc.entityType>>>.Config>.Select((PXGraph) this))
    {
      NoteDoc noteDoc = (NoteDoc) pxResult;
      Note note = (Note) pxResult;
      System.Type type = PXBuildManager.GetType(note.EntityType, false);
      if (type != (System.Type) null)
      {
        noteDoc.EntityType = note.EntityType;
        noteDoc.EntityName = EntityHelper.GetFriendlyEntityName(type);
        noteDoc.EntityRowValues = this.entity.GetEntityRowValues(type, note.NoteID.Value);
        noteDocList.Add(noteDoc);
      }
    }
    return (IEnumerable) noteDocList;
  }

  [PXInternalUseOnly]
  [PXButton(Tooltip = "Navigate to the selected entity.")]
  [PXUIField(DisplayName = "View Entity", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  public virtual IEnumerable viewEntity(PXAdapter adapter)
  {
    if (this.EntitiesRecords.Current == null || !this.EntitiesRecords.Current.NoteID.HasValue)
      return adapter.Get();
    System.Type type = PXBuildManager.GetType(this.EntitiesRecords.Current.EntityType, false);
    object entityRow = this.entity.GetEntityRow(type, this.EntitiesRecords.Current.NoteID);
    if (entityRow != null)
      PXRedirectHelper.TryRedirect(this.Caches[type], entityRow, "");
    return adapter.Get();
  }

  protected void UploadFileWithIDSelector_SourceUri_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    UploadFileWithIDSelector row = (UploadFileWithIDSelector) e.Row;
    if (!SynchronizationProcess.ListingAllowed((PX.SM.UploadFile) row) || string.IsNullOrEmpty(row.SourceUri))
      return;
    row.SourceIsFolder = new bool?(string.IsNullOrEmpty(Path.GetExtension(row.SourceUri)));
  }

  protected void UploadFileWithIDSelector_SourceUri_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    UploadFileWithIDSelector row = (UploadFileWithIDSelector) e.Row;
    bool flag = true;
    if (!string.IsNullOrEmpty(e.NewValue as string))
    {
      switch (row.SourceType)
      {
        case "F":
        case "H":
          flag = RegExpHelper.ValidateUrl(e.NewValue.ToString());
          break;
        case "S":
          flag = RegExpHelper.ValidatePath(e.NewValue.ToString());
          break;
      }
    }
    if (flag)
      return;
    sender.RaiseExceptionHandling<PX.SM.UploadFile.sourceUri>((object) row, (object) e.NewValue.ToString(), (Exception) new PXSetPropertyException("The specified URI does not match the validation rule.", PXErrorLevel.Warning));
  }

  protected void UploadFileWithIDSelector_SourceType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    UploadFileWithIDSelector row = (UploadFileWithIDSelector) e.Row;
    if (!row.SourceIsFolder.GetValueOrDefault())
      return;
    row.SourceIsFolder = new bool?(SynchronizationProcess.ListingAllowed((PX.SM.UploadFile) row));
  }

  [PXUIField(DisplayName = "Import File")]
  [PXProcessButton(Tooltip = "Get file(s) from the external source.", Category = "Synchronization")]
  public IEnumerable downloadFile(PXAdapter adapter)
  {
    foreach (UploadFileWithIDSelector fileWithIdSelector in adapter.Get())
    {
      UploadFileWithIDSelector file = fileWithIdSelector;
      if (this.Files.Current != null)
      {
        this.Actions.PressSave();
        PXLongOperation.StartOperation(this.UID, (PXToggleAsyncDelegate) (() => SynchronizationProcess.DownloadFile((PX.SM.UploadFile) file)));
      }
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Export File")]
  [PXProcessButton(Tooltip = "Export the file(s) to the external source.", Category = "Synchronization")]
  public IEnumerable uploadFile(PXAdapter adapter)
  {
    foreach (UploadFileWithIDSelector fileWithIdSelector in adapter.Get())
    {
      UploadFileWithIDSelector file = fileWithIdSelector;
      if (this.Files.Current != null)
      {
        this.Actions.PressSave();
        PXLongOperation.StartOperation(this.UID, (PXToggleAsyncDelegate) (() => SynchronizationProcess.UploadFile((PX.SM.UploadFile) file)));
      }
    }
    return adapter.Get();
  }

  /// <exclude />
  public class PXSelectFile : PXSelectBase<UploadFileWithIDSelector>
  {
    public PXSelectFile(PXGraph graph)
    {
      this.View = (PXView) new WikiFileMaintenance.ViewFile(graph);
    }

    public PXSelectFile(PXGraph graph, Delegate handler) => throw new NotSupportedException();
  }

  /// <exclude />
  public class PXSelectCurrentFile : PXSelectBase<UploadFileWithIDSelector>
  {
    public PXSelectCurrentFile(PXGraph graph)
    {
      this.View = (PXView) new WikiFileMaintenance.ViewFile(graph, true);
    }

    public PXSelectCurrentFile(PXGraph graph, Delegate handler)
    {
      throw new NotSupportedException();
    }
  }

  /// <exclude />
  private class ViewFile : PXView
  {
    public bool ShowFiles = true;

    public ViewFile(PXGraph graph)
      : base(graph, false, WikiFileMaintenance.ViewFile.CreateSelect(false))
    {
    }

    public ViewFile(PXGraph graph, bool mapOnCurrent)
      : base(graph, false, WikiFileMaintenance.ViewFile.CreateSelect(mapOnCurrent))
    {
    }

    public override string[] GetParameterNames()
    {
      return new string[1]{ "name" };
    }

    private static BqlCommand CreateSelect(bool mapOnCurrent)
    {
      return !mapOnCurrent ? (BqlCommand) new Select3<UploadFileWithIDSelector, OrderBy<Asc<PX.SM.UploadFile.name>>>() : (BqlCommand) new PX.Data.Select<UploadFileWithIDSelector, Where<UploadFileWithIDSelector.fileID, Equal<Current<UploadFileWithIDSelector.fileID>>>, OrderBy<Asc<PX.SM.UploadFile.name>>>();
    }

    public override List<object> Select(
      object[] currents,
      object[] parameters,
      object[] searches,
      string[] sortcolumns,
      bool[] descendings,
      PXFilterRow[] filters,
      ref int startRow,
      int maximumRows,
      ref int totalRows)
    {
      List<object> objectList = new List<object>();
      if (!this.ShowFiles || this.Graph.IsPageGeneratorRequest)
        return objectList;
      Guid? nullable = new Guid?();
      bool flag1 = false;
      if (parameters != null && parameters.Length == 1 && parameters[0] is string)
      {
        nullable = GUID.CreateGuid(parameters[0].ToString());
        flag1 = true;
      }
      else if (searches != null && searches.Length == 1 && searches[0] is string)
      {
        nullable = GUID.CreateGuid(searches[0].ToString());
        if (nullable.HasValue)
          flag1 = true;
      }
      if (flag1)
      {
        if (!nullable.HasValue)
        {
          this.WhereNew<Where<PX.SM.UploadFile.name, Equal<Required<PX.SM.UploadFile.name>>>>();
        }
        else
        {
          this.WhereNew<Where<UploadFileWithIDSelector.fileID, Equal<Required<UploadFileWithIDSelector.fileID>>>>();
          if (parameters == null)
            parameters = new object[1];
          parameters[0] = (object) nullable;
        }
        searches = (object[]) null;
      }
      foreach (UploadFileWithIDSelector file in base.Select(currents, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows))
      {
        PXCacheRights pxCacheRights = UploadFileMaintenance.AccessRights((PX.SM.UploadFile) file);
        bool flag2 = UploadFileMaintenance.IsRestrictedFile((PX.SM.UploadFile) file);
        if (pxCacheRights < PXCacheRights.Select)
        {
          bool? isPublicChanged = file.IsPublicChanged;
          bool flag3 = true;
          if (!(isPublicChanged.GetValueOrDefault() == flag3 & isPublicChanged.HasValue) && !UploadFileMaintenance.OverrideAccessRights((PX.SM.UploadFile) file))
            throw new PXException("You don't have enough rights to download this file.");
        }
        if (pxCacheRights < PXCacheRights.Select)
        {
          bool? isPublicChanged = file.IsPublicChanged;
          bool flag4 = true;
          if (isPublicChanged.GetValueOrDefault() == flag4 & isPublicChanged.HasValue)
            PXUIFieldAttribute.SetEnabled<PX.SM.UploadFile.isPublic>(this.Cache, (object) null, false);
        }
        if (flag2)
        {
          bool? isPublicChanged = file.IsPublicChanged;
          bool flag5 = true;
          if (!(isPublicChanged.GetValueOrDefault() == flag5 & isPublicChanged.HasValue))
            throw new PXException("You are not in a group that has access to this file.");
        }
        if (flag2)
        {
          bool? isPublicChanged = file.IsPublicChanged;
          bool flag6 = true;
          if (isPublicChanged.GetValueOrDefault() == flag6 & isPublicChanged.HasValue)
            PXUIFieldAttribute.SetEnabled<PX.SM.UploadFile.isPublic>(this.Cache, (object) null, false);
        }
        objectList.Add((object) file);
      }
      return objectList;
    }
  }

  /// <exclude />
  [Serializable]
  public class CheckOutComment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _Comment;

    [PXDBString(InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Check Out Comments")]
    public virtual string Comment
    {
      get => this._Comment;
      set => this._Comment = value;
    }

    /// <exclude />
    public abstract class comment : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      WikiFileMaintenance.CheckOutComment.comment>
    {
    }
  }

  /// <exclude />
  public delegate string GetFilePath(Guid? fileID);

  private struct FileInPageKey
  {
    public Guid? PageID;
    public string Language;
    public Guid? FileID;

    public FileInPageKey(WikiFileInPage file)
    {
      this.PageID = file.PageID;
      this.Language = file.Language;
      this.FileID = file.FileID;
    }
  }
}
