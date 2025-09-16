// Decompiled with JetBrains decompiler
// Type: PX.SM.UploadFileInq
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Collection;
using PX.Data;
using PX.Data.Wiki.Parser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

/// <exclude />
public class UploadFileInq : PXGraph<UploadFileInq>
{
  private static Guid unassignedID = Guid.NewGuid();
  public PXFilter<PX.SM.FileDialog> FileDialog;
  public PXCancel<FilesFilter> Cancel;
  public PXFilter<FilesFilter> Filter;
  public PXSelectOrderBy<SiteMapFile, OrderBy<Asc<SiteMapFile.position>>> Tree;
  [PXFilterable(new System.Type[] {})]
  public PXSelect<UploadFile> Files;
  public PXFilter<GetLinkFilterType> GetFileLinkFilter;
  private Set<Guid> linkFiles;
  public PXAction<FilesFilter> ViewRevisions;
  public PXAction<FilesFilter> GetFile;
  public PXAction<FilesFilter> GetFileLink;
  public PXAction<FilesFilter> DeleteFile;
  public PXAction<FilesFilter> AddLink;
  public PXAction<FilesFilter> AddLinkClose;
  public PXAction<FilesFilter> ClearFiles;
  public PXFilter<ClearDateFilter> ClearingFilter;

  public UploadFileInq()
  {
    PXUIFieldAttribute.SetDisplayName(this.Caches[typeof (PXDBCreatedByIDAttribute.Creator)], "DisplayName", "Added by");
  }

  [PXUIField(DisplayName = "View Versions")]
  [PXButton(DisplayOnMainToolbar = false)]
  protected IEnumerable viewRevisions(PXAdapter a)
  {
    UploadFile current = this.Files.Current;
    if (current == null)
      return a.Get();
    WikiFileMaintenance instance = PXGraph.CreateInstance<WikiFileMaintenance>();
    instance.Files.Current = (UploadFileWithIDSelector) instance.Files.Search<UploadFile.name>((object) current.Name);
    if (this.FileDialog?.Current != null)
      throw new PXPopupRedirectException((PXGraph) instance, "View Revisions");
    throw new PXRedirectRequiredException((PXGraph) instance, "View Revisions");
  }

  [PXUIField(DisplayName = "Get File")]
  [PXButton]
  protected IEnumerable getFile(PXAdapter a)
  {
    UploadFile current = this.Files.Current;
    if (current == null)
      return a.Get();
    throw new PXRedirectToFileException(current.FileID, true);
  }

  [PXButton(ImageKey = "Link", Tooltip = "Get link to the attached file.")]
  [PXUIField(DisplayName = "Get File Link", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  protected IEnumerable getFileLink(PXAdapter adapter)
  {
    this.GetFileLinkFilter.Current.WikiLink = "";
    if (this.Files.Current != null && !string.IsNullOrEmpty(this.Files.Current.Name))
    {
      if (MimeTypes.GetMimeType(this.Files.Current.Name.Substring(System.Math.Max(0, this.Files.Current.Name.LastIndexOf('.')))).StartsWith("image/"))
        this.GetFileLinkFilter.Current.WikiLink = $"[Image:{this.Files.Current.Name}]";
      else
        this.GetFileLinkFilter.Current.WikiLink = $"[{{up}}{this.Files.Current.Name}]";
    }
    int num = (int) this.GetFileLinkFilter.AskExt(true);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Delete File", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  protected IEnumerable deleteFile(PXAdapter a)
  {
    UploadFile current = this.Files.Current;
    if (current == null || this.Files.Ask("Delete File", "Are you sure you want to delete this file? (You won't be able to undo these changes.)", MessageButtons.YesNo, MessageIcon.Warning) != WebDialogResult.Yes)
      return a.Get();
    UploadFileMaintenance.DeleteFile(current.FileID);
    return a.Get();
  }

  [PXUIField(DisplayName = "Add Link")]
  [PXButton]
  protected IEnumerable addLink(PXAdapter a)
  {
    try
    {
      PXFilter<PX.SM.FileDialog> fileDialog1 = this.FileDialog;
      Guid? nullable1;
      int num;
      if (fileDialog1 == null)
      {
        num = 0;
      }
      else
      {
        nullable1 = (Guid?) fileDialog1.Current?.Entity;
        num = nullable1.HasValue ? 1 : 0;
      }
      if (num != 0 && this.Files.Current != null)
      {
        PXDataFieldAssign[] pxDataFieldAssignArray = new PXDataFieldAssign[2];
        PXFilter<PX.SM.FileDialog> fileDialog2 = this.FileDialog;
        Guid? nullable2;
        if (fileDialog2 == null)
        {
          nullable1 = new Guid?();
          nullable2 = nullable1;
        }
        else
        {
          PX.SM.FileDialog current = fileDialog2.Current;
          if (current == null)
          {
            nullable1 = new Guid?();
            nullable2 = nullable1;
          }
          else
            nullable2 = current.Entity;
        }
        pxDataFieldAssignArray[0] = new PXDataFieldAssign("NoteID", PXDbType.UniqueIdentifier, (object) nullable2);
        pxDataFieldAssignArray[1] = new PXDataFieldAssign("FileID", PXDbType.UniqueIdentifier, (object) this.Files.Current.FileID);
        PXDatabase.Insert<NoteDoc>(pxDataFieldAssignArray);
      }
      return a.Get();
    }
    catch (PXDatabaseException ex)
    {
      if (ex.ErrorCode != PXDbExceptions.PrimaryKeyConstraintViolation)
        throw ex;
      throw new Exception("Can't insert link more than once", (Exception) ex);
    }
  }

  [PXUIField(DisplayName = "Add Link & Close")]
  [PXButton]
  protected IEnumerable addLinkClose(PXAdapter a)
  {
    try
    {
      PXFilter<PX.SM.FileDialog> fileDialog1 = this.FileDialog;
      Guid? nullable1;
      int num;
      if (fileDialog1 == null)
      {
        num = 0;
      }
      else
      {
        nullable1 = (Guid?) fileDialog1.Current?.Entity;
        num = nullable1.HasValue ? 1 : 0;
      }
      if (num != 0 && this.Files.Current != null)
      {
        PXDataFieldAssign[] pxDataFieldAssignArray = new PXDataFieldAssign[2];
        PXFilter<PX.SM.FileDialog> fileDialog2 = this.FileDialog;
        Guid? nullable2;
        if (fileDialog2 == null)
        {
          nullable1 = new Guid?();
          nullable2 = nullable1;
        }
        else
        {
          PX.SM.FileDialog current = fileDialog2.Current;
          if (current == null)
          {
            nullable1 = new Guid?();
            nullable2 = nullable1;
          }
          else
            nullable2 = current.Entity;
        }
        pxDataFieldAssignArray[0] = new PXDataFieldAssign("NoteID", PXDbType.UniqueIdentifier, (object) nullable2);
        pxDataFieldAssignArray[1] = new PXDataFieldAssign("FileID", PXDbType.UniqueIdentifier, (object) this.Files.Current.FileID);
        PXDatabase.Insert<NoteDoc>(pxDataFieldAssignArray);
      }
      throw new PXClosePopupException(string.Empty);
    }
    catch (PXDatabaseException ex)
    {
      if (ex.ErrorCode != PXDbExceptions.PrimaryKeyConstraintViolation)
        throw ex;
      throw new Exception("Can't insert link more than once", (Exception) ex);
    }
  }

  protected IEnumerable tree([PXGuid] Guid? parent)
  {
    List<SiteMapFile> siteMapFileList = new List<SiteMapFile>();
    this.LoadLinkFiles();
    if (!parent.HasValue)
    {
      PXSiteMapNode rootNode = PXSiteMap.RootNode;
      siteMapFileList.Add(new SiteMapFile()
      {
        NodeID = new Guid?(rootNode.NodeID),
        Title = rootNode.Title,
        Position = new int?(0)
      });
      if (PXContext.PXIdentity.User.IsInRole(((IEnumerable<string>) PXAccess.GetAdministratorRoles()).First<string>()))
        siteMapFileList.Add(new SiteMapFile()
        {
          NodeID = new Guid?(UploadFileInq.unassignedID),
          Title = PXMessages.LocalizeNoPrefix("Unassigned"),
          Position = new int?(1)
        });
    }
    else
    {
      PXSiteMapNode siteMapNodeFromKey1;
      if ((siteMapNodeFromKey1 = PXSiteMap.Provider.FindSiteMapNodeFromKey(parent.Value)) != null)
      {
        Guid? nullable = parent;
        Guid nodeId = PXSiteMap.RootNode.NodeID;
        if ((nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() == nodeId ? 1 : 0) : 1) : 0) != 0 || PXSiteMap.WikiProvider.FindSiteMapNodeFromKey(parent.Value) == null)
        {
          int num = 0;
          using (IEnumerator<PXSiteMapNode> enumerator = PXSiteMap.Provider.GetChildNodesSimple(siteMapNodeFromKey1).GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              PXSiteMapNode current = enumerator.Current;
              if (((KList<Guid, Guid>) this.linkFiles).Contains(current.NodeID))
                siteMapFileList.Add(new SiteMapFile()
                {
                  NodeID = new Guid?(current.NodeID),
                  Title = current.Title,
                  Position = new int?(num++)
                });
            }
            goto label_22;
          }
        }
      }
      PXSiteMapNode siteMapNodeFromKey2;
      if ((siteMapNodeFromKey2 = PXSiteMap.WikiProvider.FindSiteMapNodeFromKey(parent.Value)) != null)
      {
        int num = 0;
        foreach (PXSiteMapNode childNode in (IEnumerable<PXSiteMapNode>) PXSiteMap.WikiProvider.GetChildNodes(siteMapNodeFromKey2))
        {
          if (((KList<Guid, Guid>) this.linkFiles).Contains(childNode.NodeID))
            siteMapFileList.Add(new SiteMapFile()
            {
              NodeID = new Guid?(childNode.NodeID),
              Title = childNode.Title,
              Position = new int?(num++)
            });
        }
      }
    }
label_22:
    return (IEnumerable) siteMapFileList;
  }

  protected IEnumerable files([PXGuid] Guid? parent)
  {
    PXSelectBase<UploadFile> select = (PXSelectBase<UploadFile>) null;
    List<object> pars = new List<object>();
    int startRow = PXView.StartRow;
    int maxRows = PXView.MaximumRows > 0 ? PXView.MaximumRows : -1;
    if (parent.HasValue)
    {
      Guid? nullable1 = parent;
      Guid empty = Guid.Empty;
      if ((nullable1.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == empty ? 1 : 0) : 1) : 0) == 0)
      {
        Guid? nullable2 = parent;
        Guid unassignedId = UploadFileInq.unassignedID;
        if ((nullable2.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() == unassignedId ? 1 : 0) : 1) : 0) != 0)
        {
          select = (PXSelectBase<UploadFile>) new PXSelect<UploadFile, Where<UploadFile.primaryPageID, IsNull, And<UploadFile.primaryScreenID, IsNull>>>((PXGraph) this);
          goto label_13;
        }
        if (PXSiteMap.WikiProvider.FindSiteMapNodeFromKey(parent.Value) != null)
        {
          select = (PXSelectBase<UploadFile>) new PXSelectJoinGroupBy<UploadFile, LeftJoin<WikiFileInPage, On<WikiFileInPage.fileID, Equal<UploadFile.fileID>>>, Where<UploadFile.primaryPageID, Equal<Required<UploadFile.primaryPageID>>, Or<WikiFileInPage.pageID, Equal<Required<UploadFile.primaryPageID>>>>, Aggregate<GroupBy<UploadFile.fileID, GroupBy<UploadFile.createdByID, GroupBy<UploadFile.primaryPageID>>>>>((PXGraph) this);
          pars.Add((object) parent);
          pars.Add((object) parent);
          goto label_13;
        }
        if (PXSiteMap.Provider.FindSiteMapNodeFromKey(parent.Value) != null)
        {
          PXSiteMapNode siteMapNodeFromKey = PXSiteMap.Provider.FindSiteMapNodeFromKey(parent.Value);
          string str1 = siteMapNodeFromKey.GraphType;
          if (string.IsNullOrEmpty(str1))
            str1 = "XXX";
          string str2 = siteMapNodeFromKey.ScreenID;
          if (string.IsNullOrEmpty(str2))
            str2 = "XXX";
          select = (PXSelectBase<UploadFile>) new PXSelectJoinGroupBy<UploadFile, LeftJoin<NoteDoc, On<NoteDoc.fileID, Equal<UploadFile.fileID>>, LeftJoin<PX.SM.Alias.Note, On<PX.SM.Alias.Note.noteID, Equal<NoteDoc.noteID>>>>, Where<PX.SM.Alias.Note.graphType, Equal<Required<PX.SM.Alias.Note.graphType>>>, Aggregate<GroupBy<UploadFile.fileID, GroupBy<UploadFile.createdByID, GroupBy<UploadFile.primaryPageID>>>>>((PXGraph) this);
          pars.Add((object) str1);
          pars.Add((object) str2);
          goto label_13;
        }
        goto label_13;
      }
    }
    select = (PXSelectBase<UploadFile>) new PXSelect<UploadFile>((PXGraph) this);
label_13:
    this.ApplyFilter((PXSelectBase) select, pars);
    select.Cache.ClearQueryCache();
    IEnumerable enumerable = this.SelectFilteredFiles(select, pars, startRow, maxRows);
    PXView.StartRow = 0;
    return enumerable;
  }

  private IEnumerable SelectFilteredFiles(
    PXSelectBase<UploadFile> select,
    List<object> pars,
    int startRow,
    int maxRows)
  {
    if (maxRows > 0)
    {
      int returnedRows = 0;
      int numRows = maxRows;
      while (true)
      {
        int totalRows = 0;
        List<object> selectResult = select.View.Select((object[]) null, pars.ToArray(), PXView.Searches, PXView.SortColumns, PXView.Descendings, (PXFilterRow[]) PXView.Filters, ref startRow, numRows, ref totalRows);
        foreach (object obj in selectResult)
        {
          UploadFile file = obj is UploadFile ? (UploadFile) obj : (UploadFile) (PXResult<UploadFile>) obj;
          if (UploadFileMaintenance.AccessRights(file) >= PXCacheRights.Select)
          {
            yield return (object) file;
            ++returnedRows;
            if (returnedRows >= maxRows)
              yield break;
          }
        }
        if (selectResult.Count != 0)
        {
          startRow += selectResult.Count;
          numRows *= 2;
          selectResult = (List<object>) null;
        }
        else
          break;
      }
    }
    else
    {
      int totalRows = -1;
      foreach (object obj in select.View.Select((object[]) null, pars.ToArray(), PXView.Searches, PXView.SortColumns, PXView.Descendings, (PXFilterRow[]) PXView.Filters, ref startRow, 0, ref totalRows))
      {
        UploadFile file = obj is UploadFile ? (UploadFile) obj : (UploadFile) (PXResult<UploadFile>) obj;
        if (UploadFileMaintenance.AccessRights(file) >= PXCacheRights.Select)
          yield return (object) file;
      }
    }
  }

  private void ApplyFilter(PXSelectBase select, List<object> pars)
  {
    if (this.Filter.Current.ScreenID == null)
    {
      select.View.Join<LeftJoin<SiteMap, On<SiteMap.screenID, Equal<UploadFile.primaryScreenID>>>>();
      bool? showUnassignedFiles = this.Filter.Current.ShowUnassignedFiles;
      bool flag = true;
      if (showUnassignedFiles.GetValueOrDefault() == flag & showUnassignedFiles.HasValue)
        select.View.WhereAnd<Where<SiteMap.screenID, IsNull>>();
      else
        select.View.WhereAnd<Where<SiteMap.screenID, IsNotNull>>();
    }
    else
      select.View.WhereAnd<Where<UploadFile.primaryScreenID, Equal<Current<FilesFilter.screenID>>>>();
    if (this.Filter.Current.DocName != null)
    {
      select.View.WhereAnd<Where<UploadFile.name, Like<Required<FilesFilter.docName>>>>();
      string str = select.View.Graph.SqlDialect.PrepareLikeCondition(this.Filter.Current.DocName);
      pars.Add((object) $"%{str}%");
    }
    if (this.Filter.Current.DateCreatedFrom.HasValue)
      select.View.WhereAnd<Where<UploadFile.createdDateTime, GreaterEqual<Current<FilesFilter.dateCreatedFrom>>>>();
    if (this.Filter.Current.DateCreatedTo.HasValue)
    {
      select.View.WhereAnd<Where<UploadFile.createdDateTime, Less<Required<FilesFilter.dateCreatedTo>>>>();
      pars.Add((object) this.Filter.Current.DateCreatedTo.Value.AddDays(1.0));
    }
    if (this.Filter.Current.AddedBy.HasValue)
      select.View.WhereAnd<Where<UploadFile.createdByID, Equal<Current<FilesFilter.addedBy>>>>();
    if (!this.Filter.Current.CheckedOutBy.HasValue)
      return;
    select.View.WhereAnd<Where<UploadFile.checkedOutBy, Equal<Current<FilesFilter.checkedOutBy>>>>();
  }

  protected void FilesFilter_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    this.FilesClear();
  }

  protected void FilesFilter_ShowUnassignedFiles_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    PXUIFieldAttribute.SetEnabled<FilesFilter.screenID>(cache, e.Row, !(bool) e.NewValue);
  }

  protected void FilesFilter_ScreenID_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
  {
    PXUIFieldAttribute.SetEnabled<FilesFilter.showUnassignedFiles>(cache, e.Row, e.NewValue == null);
  }

  public override void Load()
  {
    base.Load();
    this.FilesLoad();
  }

  private void FilesLoad()
  {
    string key = this.GetType().FullName + "$linkFiles";
    this.linkFiles = PXContext.Session.linkFiles[key];
  }

  public override void Unload()
  {
    base.Unload();
    this.FilesUnload();
  }

  private void FilesUnload()
  {
    string key = this.GetType().FullName + "$linkFiles";
    PXContext.Session.linkFiles[key] = this.linkFiles;
  }

  public override void Clear(PXClearOption option)
  {
    base.Clear(option);
    this.FilesClear();
  }

  private void FilesClear()
  {
    string key = this.GetType().FullName + "$linkFiles";
    PXContext.Session.linkFiles[key] = (Set<Guid>) null;
  }

  private void LoadLinkFiles()
  {
    if (this.linkFiles != null)
      return;
    this.linkFiles = new Set<Guid>(true);
    this.LoadWikiPageLinks((PXSelectBase) new PXSelectJoinGroupBy<PX.SM.Reduced.WikiPage, InnerJoin<PX.SM.Reduced.UploadFile, On<PX.SM.Reduced.UploadFile.primaryPageID, Equal<PX.SM.Reduced.WikiPage.pageID>>>, Aggregate<GroupBy<PX.SM.Reduced.WikiPage.pageID>>>((PXGraph) this));
    PXSelectBase<PX.SM.Alias.Note> select = (PXSelectBase<PX.SM.Alias.Note>) new PXSelectJoinGroupBy<PX.SM.Alias.Note, InnerJoin<NoteDoc, On<PX.SM.Alias.Note.noteID, Equal<NoteDoc.noteID>>, InnerJoin<UploadFile, On<NoteDoc.fileID, Equal<UploadFile.fileID>>>>, Aggregate<GroupBy<PX.SM.Alias.Note.graphType>>>((PXGraph) this);
    select.Cache.ClearQueryCache();
    this.LoadSiteMapLinks2(select);
  }

  private void LoadWikiPageLinks(PXSelectBase select)
  {
    List<object> pars = new List<object>();
    this.ApplyFilter(select, pars);
    foreach (PXResult<PX.SM.Reduced.WikiPage> pxResult in select.View.SelectMulti(pars.ToArray()))
    {
      Guid key = ((PX.SM.Reduced.WikiPage) pxResult).PageID.Value;
      if (!((KList<Guid, Guid>) this.linkFiles).Contains(key))
      {
        ((KList<Guid, Guid>) this.linkFiles).Add(key);
        this.linkedWikiParents(key);
      }
    }
  }

  private void LoadSiteMapLinks(PXSelectBase<SiteMap> select)
  {
    List<object> pars = new List<object>();
    this.ApplyFilter((PXSelectBase) select, pars);
    foreach (PXResult<SiteMap> pxResult in select.View.SelectMulti(pars.ToArray()))
    {
      Guid key = ((SiteMap) pxResult).NodeID.Value;
      if (!((KList<Guid, Guid>) this.linkFiles).Contains(key))
      {
        ((KList<Guid, Guid>) this.linkFiles).Add(key);
        this.linkedSiteMapParents(key);
      }
    }
  }

  private void LoadSiteMapLinks2(PXSelectBase<PX.SM.Alias.Note> select)
  {
    List<object> pars = new List<object>();
    this.ApplyFilter((PXSelectBase) select, pars);
    foreach (PXResult<PX.SM.Alias.Note> pxResult in select.Select(pars.ToArray()))
    {
      PXSiteMapNode mapNodeByGraphType = PXSiteMap.Provider.FindSiteMapNodeByGraphType(((PX.SM.Alias.Note) pxResult).GraphType);
      if (mapNodeByGraphType != null)
      {
        Guid nodeId = mapNodeByGraphType.NodeID;
        if (!((KList<Guid, Guid>) this.linkFiles).Contains(nodeId))
        {
          ((KList<Guid, Guid>) this.linkFiles).Add(nodeId);
          this.linkedSiteMapParents(nodeId);
        }
      }
    }
  }

  private void linkedSiteMapParents(Guid key)
  {
    PXSiteMapNode siteMapNodeFromKey = PXSiteMap.Provider.FindSiteMapNodeFromKey(key);
    if (siteMapNodeFromKey == null || siteMapNodeFromKey.ParentNode == null)
      return;
    key = siteMapNodeFromKey.ParentID;
    if (((KList<Guid, Guid>) this.linkFiles).Contains(key))
      return;
    ((KList<Guid, Guid>) this.linkFiles).Add(key);
    this.linkedSiteMapParents(key);
  }

  private void linkedWikiParents(Guid key)
  {
    PXSiteMapNode siteMapNodeFromKey = PXSiteMap.WikiProvider.FindSiteMapNodeFromKey(key);
    if (siteMapNodeFromKey == null || siteMapNodeFromKey.ParentNode == null)
      return;
    key = siteMapNodeFromKey.ParentID;
    if (((KList<Guid, Guid>) this.linkFiles).Contains(key))
      return;
    ((KList<Guid, Guid>) this.linkFiles).Add(key);
    this.linkedWikiParents(key);
  }

  [PXButton(Tooltip = "Delete old revisions for all files.")]
  [PXUIField(DisplayName = "Clear Files", Visible = false)]
  internal IEnumerable clearFiles(PXAdapter adapter)
  {
    ClearDateFilter current = this.ClearingFilter.Current;
    if (this.ClearingFilter.AskExt() == WebDialogResult.OK)
      PXLongOperation.StartOperation(this.UID, (PXToggleAsyncDelegate) (() => UploadFileInq.DoClearFiles(this.ClearingFilter.Current.Till)));
    return adapter.Get();
  }

  public static void DoClearFiles(System.DateTime? till)
  {
    if (!till.HasValue)
      return;
    PXGraph graph = new PXGraph();
    foreach (PXResult<UploadFile, UploadFileRevisionNoData> pxResult in PXSelectBase<UploadFile, PXSelectJoin<UploadFile, InnerJoin<UploadFileRevisionNoData, On<UploadFile.fileID, Equal<UploadFileRevisionNoData.fileID>, And<UploadFile.lastRevisionID, NotEqual<UploadFileRevisionNoData.fileRevisionID>>>>, Where<UploadFileRevisionNoData.createdDateTime, Less<Required<UploadFileRevisionNoData.createdDateTime>>>>.Config>.Select(graph, (object) till))
    {
      UploadFileRevisionNoData fileRevisionNoData = (UploadFileRevisionNoData) pxResult;
      graph.Caches[typeof (UploadFileRevisionNoData)].Delete((object) fileRevisionNoData);
    }
    graph.Caches[typeof (UploadFileRevisionNoData)].Persist(PXDBOperation.Delete);
  }
}
