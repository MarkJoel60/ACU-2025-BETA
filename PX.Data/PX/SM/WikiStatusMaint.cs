// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiStatusMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

#nullable enable
namespace PX.SM;

/// <exclude />
[Serializable]
public class WikiStatusMaint : WikiReader
{
  public 
  #nullable disable
  PXCancel<WikiPageStatusFilter> Cancel;
  public PXAction<WikiPageStatusFilter> View;
  public PXFilter<WikiPageStatusFilter> fltStatusRecords;
  public PXFilter<WikiPageUpdatableProps> fltArticlesProps;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessing<WikiPagePath, WikiPageStatusFilter> ArticlesByStatusRecords;
  public PXSelect<WikiRevisionTag> ArticleTags;
  public PXSelect<WikiPageLanguage, Where<WikiPageLanguage.pageID, Equal<Current<WikiPage.pageID>>, And<WikiPageLanguage.language, Equal<Current<WikiPage.language>>>>> PageLanguage;
  public PXSelect<WikiRevision, Where<WikiRevision.pageID, Equal<Current<WikiPage.pageID>>, And<WikiRevision.language, Equal<Current<WikiPage.language>>, And<WikiRevision.pageRevisionID, Equal<Current<WikiPage.pageRevisionID>>>>>> Revision;
  public PXSelect<WikiPageSimple, Where<WikiPageSimple.pageID, Equal<WikiPageSimple.pageID>>, OrderBy<Desc<WikiPage.articleType, Asc<WikiPage.number>>>> Folders;
  public PXSelect<WikiPageSimple, Where<WikiPageSimple.folder, Equal<PX.Data.True>>> CurrentFolder;

  public WikiStatusMaint()
  {
    Wiki.BlockIfOnlineHelpIsOn();
    this.ArticlesByStatusRecords.SetProcessDelegate(new PXProcessingBase<WikiPagePath>.ProcessListDelegate(this.Process));
    this.ArticlesByStatusRecords.SetParametersDelegate((PXProcessingBase<WikiPagePath>.ParametersDelegate) (items => this.fltArticlesProps.AskExt() == WebDialogResult.OK));
    this.ArticlesByStatusRecords.SetProcessCaption(PXMessages.LocalizeNoPrefix("Process"));
    this.ArticlesByStatusRecords.SetProcessAllCaption(PXMessages.LocalizeNoPrefix("Process All"));
    PXCache cach = this.Caches[typeof (WikiStatusMaint.WikiPageOwned)];
    PXDefaultAttribute.SetPersistingCheck<WikiPage.title>(this.Caches[typeof (WikiPagePath)], (object) null, PXPersistingCheck.Nothing);
  }

  public override bool IsProcessing
  {
    get => false;
    set
    {
    }
  }

  internal IEnumerable fltstatusRecords([PXString] string WikiID)
  {
    if (!string.IsNullOrEmpty(WikiID))
      this.fltStatusRecords.Current.WikiID = GUID.CreateGuid(WikiID);
    this.fltStatusRecords.Cache.IsDirty = false;
    bool hasValue = this.fltStatusRecords.Current.WikiID.HasValue;
    PXUIFieldAttribute.SetEnabled<WikiPageStatusFilter.folderID>(this.fltStatusRecords.Cache, (object) null, hasValue);
    PXUIFieldAttribute.SetEnabled<WikiPageUpdatableProps.parentUID>(this.fltArticlesProps.Cache, (object) null, hasValue);
    PXUIFieldAttribute.SetEnabled<WikiPageUpdatableProps.tagID>(this.fltArticlesProps.Cache, (object) null, hasValue);
    yield return (object) this.fltStatusRecords.Current;
  }

  protected IEnumerable folders(string PageID)
  {
    WikiStatusMaint graph = this;
    if (graph.fltStatusRecords.Current.WikiID.HasValue)
    {
      Guid? nullable = graph.fltStatusRecords.Current.WikiID;
      Guid guid1 = nullable.Value;
      nullable = GUID.CreateGuid(PageID);
      Guid guid2 = nullable ?? guid1;
      PXResultset<WikiPageSimple> pxResultset;
      if (!string.IsNullOrEmpty(PageID))
        pxResultset = PXSelectBase<WikiPageSimple, PXSelect<WikiPageSimple, Where<WikiPageSimple.parentUID, Equal<Required<WikiPageSimple.parentUID>>, And<WikiPageSimple.folder, Equal<Required<WikiPageSimple.folder>>>>>.Config>.Select((PXGraph) graph, (object) guid2, (object) 1);
      else
        pxResultset = PXSelectBase<WikiPageSimple, PXSelect<WikiPageSimple, Where<WikiPageSimple.pageID, Equal<Required<WikiPageSimple.pageID>>>>.Config>.Select((PXGraph) graph, (object) guid1);
      foreach (PXResult<WikiPageSimple> pxResult in pxResultset)
      {
        WikiPageSimple wikiPageSimple = (WikiPageSimple) pxResult;
        PXWikiProvider wikiProvider1 = PXSiteMap.WikiProvider;
        nullable = wikiPageSimple.PageID;
        Guid pageID = nullable.Value;
        if (wikiProvider1.GetAccessRights(pageID) >= PXWikiRights.Select)
        {
          PXWikiProvider wikiProvider2 = PXSiteMap.WikiProvider;
          nullable = wikiPageSimple.PageID;
          Guid valueOrDefault = nullable.GetValueOrDefault();
          PXSiteMapNode siteMapNodeFromKey = wikiProvider2.FindSiteMapNodeFromKey(valueOrDefault);
          wikiPageSimple.Title = siteMapNodeFromKey == null ? wikiPageSimple.Name : siteMapNodeFromKey.Title;
          yield return (object) wikiPageSimple;
        }
      }
    }
  }

  protected IEnumerable currentFolder()
  {
    WikiStatusMaint graph = this;
    if (graph.fltStatusRecords.Current.FolderID.HasValue)
    {
      Guid key = graph.fltStatusRecords.Current.FolderID.Value;
      PXSiteMapNode siteMapNodeFromKey = PXSiteMap.WikiProvider.FindSiteMapNodeFromKey(key);
      WikiPageSimple wikiPageSimple = (WikiPageSimple) PXSelectBase<WikiPageSimple, PXSelect<WikiPageSimple, Where<WikiPageSimple.pageID, Equal<Required<WikiPageSimple.pageID>>>>.Config>.Select((PXGraph) graph, (object) key);
      wikiPageSimple.Title = siteMapNodeFromKey == null ? wikiPageSimple.Name : siteMapNodeFromKey.Title;
      yield return (object) wikiPageSimple;
    }
  }

  public IEnumerable articlesByStatusRecords()
  {
    List<WikiPage> wikiPageList = new List<WikiPage>();
    return this.fltStatusRecords == null || this.fltStatusRecords.Current == null ? (IEnumerable) wikiPageList : this.Select(this.CreateSelect(), this.fltStatusRecords.Current.FolderID);
  }

  [PXButton(Tooltip = "View the selected record.")]
  [PXUIField(DisplayName = "View", MapEnableRights = PXCacheRights.Select)]
  protected IEnumerable view(PXAdapter adapter)
  {
    if (this.ArticlesByStatusRecords.Current == null)
      return adapter.Get();
    throw new PXRedirectToUrlException($"{"~/Wiki/Show.aspx"}?{"pageID"}={this.ArticlesByStatusRecords.Current.PageID}", PXBaseRedirectException.WindowMode.Same, (string) null);
  }

  protected void Process(List<WikiPagePath> list)
  {
    int num1 = 0;
    int num2 = 0;
    foreach (WikiPagePath wikiPagePath1 in list)
    {
      bool flag1 = false;
      bool? nullable1 = wikiPagePath1.Selected;
      bool flag2 = true;
      if (nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue && PXSiteMap.WikiProvider.GetAccessRights(wikiPagePath1.PageID.Value) >= PXWikiRights.Update)
      {
        WikiPageUpdatableProps current = this.fltArticlesProps.Current;
        WikiPagePath copy = (WikiPagePath) this.ArticlesByStatusRecords.Cache.CreateCopy((object) wikiPagePath1);
        if (current.ParentUID.HasValue)
        {
          copy.ParentUID = current.ParentUID;
          flag1 = true;
        }
        int? nullable2 = current.TagID;
        if (nullable2.HasValue)
        {
          WikiRevisionTag instance = (WikiRevisionTag) this.ArticleTags.Cache.CreateInstance();
          instance.WikiID = wikiPagePath1.WikiID;
          instance.PageID = wikiPagePath1.PageID;
          instance.Language = wikiPagePath1.Language;
          instance.PageRevisionID = wikiPagePath1.PageRevisionID;
          instance.TagID = current.TagID;
          this.ArticleTags.Cache.Update((object) instance);
          flag1 = true;
        }
        if (current.Keywords != null)
        {
          copy.Keywords = current.Keywords;
          flag1 = true;
        }
        nullable2 = current.Versioned;
        int num3 = 0;
        if (nullable2.GetValueOrDefault() > num3 & nullable2.HasValue)
        {
          WikiPagePath wikiPagePath2 = copy;
          nullable2 = current.Versioned;
          int num4 = 1;
          bool? nullable3 = new bool?(nullable2.GetValueOrDefault() == num4 & nullable2.HasValue);
          wikiPagePath2.Versioned = nullable3;
          flag1 = true;
        }
        nullable2 = current.Hold;
        int num5 = 1;
        if (nullable2.GetValueOrDefault() == num5 & nullable2.HasValue)
        {
          copy.Hold = new bool?(flag1 = true);
        }
        else
        {
          nullable2 = current.Hold;
          int num6 = 2;
          if (nullable2.GetValueOrDefault() == num6 & nullable2.HasValue)
          {
            nullable1 = wikiPagePath1.AllowApprove;
            bool flag3 = false;
            if (nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue)
            {
              bool flag4;
              copy.Hold = new bool?(!(flag4 = true));
              copy.Approved = new bool?(flag1 = true);
              goto label_25;
            }
          }
          nullable2 = current.Hold;
          int num7 = 2;
          if (nullable2.GetValueOrDefault() == num7 & nullable2.HasValue)
          {
            nullable1 = wikiPagePath1.AllowApprove;
            bool flag5 = true;
            if (nullable1.GetValueOrDefault() == flag5 & nullable1.HasValue)
            {
              copy.Hold = new bool?(!(flag1 = true));
              goto label_25;
            }
          }
          nullable2 = current.Hold;
          int num8 = 3;
          if (nullable2.GetValueOrDefault() == num8 & nullable2.HasValue)
          {
            nullable1 = wikiPagePath1.AllowApprove;
            bool flag6 = true;
            if (nullable1.GetValueOrDefault() == flag6 & nullable1.HasValue)
            {
              copy.Approved = new bool?(flag1 = true);
              goto label_25;
            }
          }
          nullable2 = current.Hold;
          int num9 = 4;
          if (nullable2.GetValueOrDefault() == num9 & nullable2.HasValue)
          {
            nullable1 = wikiPagePath1.AllowApprove;
            bool flag7 = true;
            if (nullable1.GetValueOrDefault() == flag7 & nullable1.HasValue)
              copy.Rejected = new bool?(flag1 = true);
          }
        }
label_25:
        if (flag1)
        {
          this.ArticlesByStatusRecords.Cache.Update((object) copy);
          nullable1 = copy.Approved;
          bool flag8 = true;
          if (nullable1.GetValueOrDefault() == flag8 & nullable1.HasValue)
            this.ApproveRevision(copy);
          ++num1;
        }
        else
        {
          nullable1 = wikiPagePath1.AllowApprove;
          bool flag9 = true;
          if (!(nullable1.GetValueOrDefault() == flag9 & nullable1.HasValue))
          {
            nullable2 = current.Hold;
            int num10 = 3;
            if (!(nullable2.GetValueOrDefault() == num10 & nullable2.HasValue))
            {
              nullable2 = current.Hold;
              int num11 = 4;
              if (!(nullable2.GetValueOrDefault() == num11 & nullable2.HasValue))
                continue;
            }
            ++num2;
          }
        }
      }
    }
    if (num1 > 0)
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        this.Persist(typeof (WikiPagePath), PXDBOperation.Update);
        this.Persist(typeof (WikiPageLanguage), PXDBOperation.Update);
        this.Persist(typeof (WikiRevision), PXDBOperation.Update);
        this.ArticleTags.Cache.Persist(PXDBOperation.Insert);
        transactionScope.Complete();
      }
    }
    this.ArticlesByStatusRecords.Cache.Clear();
    this.ArticleTags.Cache.Clear();
    this.Caches[typeof (WikiPage)].Clear();
    this.PageLanguage.Cache.Clear();
    this.Revision.Cache.Clear();
    this.ArticlesByStatusRecords.View.RequestRefresh();
    this.SelectTimeStamp();
    this.fltArticlesProps.Cache.Clear();
    if (num2 > 0)
    {
      int num12 = (int) this.fltStatusRecords.Ask(PXMessages.LocalizeFormat("{0} row(s) have been successfully updated. Approval is not allowed for {1} article(s).", (object) num1, (object) num2), MessageButtons.OK);
    }
    int num13 = (int) this.fltStatusRecords.Ask(PXMessages.LocalizeFormat("{0} row(s) out of {1} have been successfully updated.", (object) num1, (object) list.Count), MessageButtons.OK);
  }

  protected void ApproveRevision(WikiPagePath page)
  {
    WikiPageLanguage wikiPageLanguage = (WikiPageLanguage) this.PageLanguage.View.SelectSingleBound(new object[1]
    {
      (object) page
    });
    if (wikiPageLanguage == null)
      return;
    int? lastPublishedId = wikiPageLanguage.LastPublishedID;
    int? pageRevisionId = page.PageRevisionID;
    if (lastPublishedId.GetValueOrDefault() == pageRevisionId.GetValueOrDefault() & lastPublishedId.HasValue == pageRevisionId.HasValue)
      return;
    WikiPageLanguage copy1 = (WikiPageLanguage) this.PageLanguage.Cache.CreateCopy((object) wikiPageLanguage);
    copy1.LastPublishedID = page.PageRevisionID;
    copy1.LastPublishedDateTime = new System.DateTime?(System.DateTime.Now);
    this.PageLanguage.Update(copy1);
    WikiRevision wikiRevision = (WikiRevision) this.Revision.View.SelectSingleBound(new object[1]
    {
      (object) page
    });
    if (wikiRevision == null)
      return;
    WikiRevision copy2 = (WikiRevision) this.Revision.Cache.CreateCopy((object) wikiRevision);
    copy2.ApprovalByID = new Guid?(this.Accessinfo.UserID);
    this.Revision.Update(copy2);
  }

  protected void _(
    Events.FieldSelecting<WikiPageStatusFilter.folderID> e)
  {
    if (e.ReturnValue == null || e.ReturnState == null)
      return;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (System.Type) null, fieldName: "folderID", descriptionName: "title", viewName: this.CurrentFolder.View.Name);
    ((PXFieldState) e.ReturnState).ValueField = "pageID";
  }

  protected void WikiPageUpdatableProps_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    int num1;
    if (this.fltStatusRecords.Current != null)
    {
      int? statusId = this.fltStatusRecords.Current.StatusID;
      int num2 = 1;
      num1 = statusId.GetValueOrDefault() == num2 & statusId.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    bool isEnabled = this.fltStatusRecords.Current != null && this.fltStatusRecords.Current.WikiID.HasValue;
    PXUIFieldAttribute.SetEnabled<WikiPageUpdatableProps.parentUID>(sender, e.Row, isEnabled);
    PXUIFieldAttribute.SetEnabled<WikiPageUpdatableProps.tagID>(sender, e.Row, isEnabled);
    if (num1 != 0)
      PXIntListAttribute.SetList<WikiPageUpdatableProps.hold>(sender, e.Row, StatusList.FullValues, StatusList.FullLabels);
    else
      PXIntListAttribute.SetList<WikiPageUpdatableProps.hold>(sender, e.Row, StatusList.ReducedValues, StatusList.ReducedLabels);
  }

  protected void WikiPagePath_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is WikiPagePath row1))
      return;
    Guid? pageId = row1.PageID;
    if (!pageId.HasValue)
      return;
    PXCache cache = sender;
    object row2 = e.Row;
    PXWikiProvider wikiProvider = PXSiteMap.WikiProvider;
    pageId = row1.PageID;
    Guid pageID = pageId.Value;
    int num = wikiProvider.GetAccessRights(pageID) >= PXWikiRights.Update ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<WikiPage.selected>(cache, row2, num != 0);
  }

  private IEnumerable Select(PXSelectBase<WikiStatusMaint.WikiPageOwned> select, Guid? parentID)
  {
    WikiStatusMaint wikiStatusMaint = this;
    PXCache pathCache = wikiStatusMaint.Caches[typeof (WikiPagePath)];
    PXCache cache = wikiStatusMaint.Caches[typeof (WikiPage)];
    PXSelectBase<WikiStatusMaint.WikiPageOwned> pxSelectBase = select;
    object[] objArray1 = new object[2]
    {
      (object) Thread.CurrentThread.CurrentUICulture.Name,
      (object) parentID
    };
    foreach (PXResult<WikiStatusMaint.WikiPageOwned, WikiDescriptor, WikiPageLanguage> pxResult in pxSelectBase.Select(objArray1))
    {
      WikiStatusMaint.WikiPageOwned wp = (WikiStatusMaint.WikiPageOwned) pxResult;
      WikiDescriptor wiki = (WikiDescriptor) pxResult;
      WikiPageLanguage lang = (WikiPageLanguage) pxResult;
      PXWikiRights rights = PXSiteMap.WikiProvider.GetAccessRights(wp.PageID.Value);
      if (rights >= PXWikiRights.Select && wp.Name.IndexOf("ContainerTemplate:") <= -1)
      {
        WikiPagePath ret = new WikiPagePath();
        for (int index = 0; index < wikiStatusMaint.Pages.Cache.Fields.Count; ++index)
          pathCache.SetValue((object) ret, cache.Fields[index], cache.GetValue((object) wp, cache.Fields[index]));
        if (ret.Language == null)
          ret.Language = wikiStatusMaint.Filter.Current == null || wikiStatusMaint.Filter.Current.Language == null || !wikiStatusMaint.LanguageAvailable(wikiStatusMaint.Filter.Current.Language, ret.WikiID, wikiStatusMaint.CurrentAccessRights((WikiPage) ret)) ? Thread.CurrentThread.CurrentCulture.Name : wikiStatusMaint.Filter.Current.Language;
        if (ret.PageRevisionID.HasValue)
          yield return (object) null;
        WikiPageLanguage wikiPageLanguage = wikiStatusMaint.ReadLanguage((WikiPage) ret);
        if (wikiPageLanguage != null)
          ret.Title = wikiPageLanguage.Title;
        ret.Path = wikiStatusMaint.GetPath(wp.PageID.Value);
        ret.Language = lang.Language;
        ret.PageRevisionID = lang.LastRevisionID;
        int? nullable1;
        bool? requestApproval;
        if (wikiStatusMaint.fltStatusRecords.Current != null)
        {
          nullable1 = wikiStatusMaint.fltStatusRecords.Current.StatusID;
          int num = 1;
          if (nullable1.GetValueOrDefault() == num & nullable1.HasValue)
          {
            requestApproval = wiki.RequestApproval;
            bool flag = true;
            if (requestApproval.GetValueOrDefault() == flag & requestApproval.HasValue && rights >= PXWikiRights.Published)
            {
              nullable1 = wp.Approvable;
              if (!nullable1.HasValue)
              {
                WikiStatusMaint graph = wikiStatusMaint;
                object[] objArray2 = new object[1]
                {
                  (object) wp.ApprovalGroupID
                };
                wp.Approvable = new int?((EPCompanyTree) PXSelectBase<EPCompanyTree, PXSelect<EPCompanyTree, Where<EPCompanyTree.workGroupID, Equal<Required<EPCompanyTree.workGroupID>>, And<EPCompanyTree.workGroupID, IsWorkgroupOrSubgroupOfContact<Current<AccessInfo.contactID>>>>>.Config>.SelectWindowed((PXGraph) graph, 0, 1, objArray2) != null ? 1 : 0);
              }
            }
          }
        }
        WikiPagePath wikiPagePath = ret;
        requestApproval = wiki.RequestApproval;
        bool flag1 = true;
        int num1;
        if (requestApproval.GetValueOrDefault() == flag1 & requestApproval.HasValue && rights >= PXWikiRights.Published)
        {
          nullable1 = wp.Approvable;
          int num2 = 0;
          num1 = nullable1.GetValueOrDefault() > num2 & nullable1.HasValue ? 1 : 0;
        }
        else
          num1 = 0;
        bool? nullable2 = new bool?(num1 != 0);
        wikiPagePath.AllowApprove = nullable2;
        if (wikiStatusMaint.Caches[typeof (WikiPagePath)].GetStatus((object) ret) == PXEntryStatus.Notchanged)
          wikiStatusMaint.Caches[typeof (WikiPagePath)].SetStatus((object) ret, PXEntryStatus.Updated);
        yield return (object) ret;
        wp = (WikiStatusMaint.WikiPageOwned) null;
        wiki = (WikiDescriptor) null;
        lang = (WikiPageLanguage) null;
        ret = (WikiPagePath) null;
      }
    }
    if (parentID.HasValue)
    {
      WikiStatusMaint graph = wikiStatusMaint;
      object[] objArray3 = new object[1]
      {
        (object) parentID
      };
      foreach (PXResult<WikiPage> pxResult in PXSelectBase<WikiPage, PXSelect<WikiPage, Where<WikiPage.parentUID, Equal<Required<WikiPage.parentUID>>>>.Config>.Select((PXGraph) graph, objArray3))
      {
        WikiPage wikiPage = (WikiPage) pxResult;
        if (PXSiteMap.WikiProvider.GetAccessRights(wikiPage.PageID.Value) >= PXWikiRights.Select)
        {
          foreach (WikiPagePath wikiPagePath in wikiStatusMaint.Select(select, wikiPage.PageID))
            yield return (object) wikiPagePath;
        }
      }
    }
  }

  private PXSelectBase<WikiStatusMaint.WikiPageOwned> CreateSelect()
  {
    PXSelectBase<WikiStatusMaint.WikiPageOwned> select = (PXSelectBase<WikiStatusMaint.WikiPageOwned>) new PXSelectJoin<WikiStatusMaint.WikiPageOwned, InnerJoin<WikiDescriptor, On<WikiDescriptor.pageID, Equal<WikiPage.wikiID>>, InnerJoin<WikiPageLanguage, On<WikiPageLanguage.pageID, Equal<WikiPage.pageID>, And<WikiPageLanguage.language, Equal<Required<WikiPageLanguage.language>>>>>>>((PXGraph) this);
    if (this.fltStatusRecords.Current.StatusID.HasValue)
      select.WhereAnd<Where<WikiPage.statusID, Equal<Current<WikiPageStatusFilter.statusID>>>>();
    if (this.fltStatusRecords.Current.WikiID.HasValue)
      select.WhereAnd<Where<WikiPage.wikiID, Equal<Current<WikiPageStatusFilter.wikiID>>>>();
    if (this.fltStatusRecords.Current.FolderID.HasValue)
      select.WhereAnd<Where<WikiPage.parentUID, Equal<Required<WikiPageStatusFilter.folderID>>>>();
    if (this.fltStatusRecords.Current.UserID.HasValue)
      select.WhereAnd<Where<WikiPage.createdByID, Equal<Current<WikiPageStatusFilter.userID>>>>();
    if (this.fltStatusRecords.Current.CreatedFrom.HasValue)
      select.WhereAnd<Where<WikiPage.createdDateTime, GreaterEqual<Current<WikiPageStatusFilter.createdFrom>>>>();
    if (this.fltStatusRecords.Current.CreatedTill.HasValue)
      select.WhereAnd<Where<WikiPage.createdDateTime, LessEqual<Current<WikiPageStatusFilter.createdTill>>>>();
    return select;
  }

  private string GetPath(Guid pageID)
  {
    PXSiteMapNode pxSiteMapNode = PXSiteMap.WikiProvider.FindSiteMapNodeFromKey(pageID.ToString());
    List<string> stringList = new List<string>();
    StringBuilder stringBuilder = new StringBuilder();
    for (; pxSiteMapNode != null && pxSiteMapNode != PXSiteMap.WikiProvider.RootNode; pxSiteMapNode = pxSiteMapNode.ParentNode)
      stringList.Add(pxSiteMapNode.Title);
    for (int index = stringList.Count - 1; index >= 0; --index)
    {
      stringBuilder.Append(stringList[index]);
      stringBuilder.Append(" > ");
    }
    if (stringBuilder.Length != 0)
      stringBuilder = stringBuilder.Remove(stringBuilder.Length - 3, 3);
    return stringBuilder.ToString();
  }

  /// <exclude />
  [WikiPageStatusFilter.Projection]
  [Serializable]
  public class WikiPageOwned : WikiPage
  {
    protected int? _Approvable;

    [PXDBCalced(typeof (Switch<Case<Where<WikiStatusMaint.WikiPageOwned.approvalGroupID, Owned<CurrentValue<WikiPageStatusFilter.currentOwnerID>>>, PX.Data.True>, False>), typeof (int))]
    public virtual int? Approvable
    {
      get => this._Approvable;
      set => this._Approvable = value;
    }

    /// <exclude />
    public new abstract class approvalGroupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      WikiStatusMaint.WikiPageOwned.approvalGroupID>
    {
    }

    /// <exclude />
    public abstract class approvable : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      WikiStatusMaint.WikiPageOwned.approvable>
    {
    }
  }
}
