// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiPageMaint`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.Wiki.Parser;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;

#nullable disable
namespace PX.SM;

/// <exclude />
[PXHidden]
public class WikiPageMaint<Base, Primary, Where> : 
  WikiPageGraph<Base, Primary, Where>,
  IWikiPageMaint
  where Base : WikiPage, new()
  where Primary : class, IBqlTable, new()
  where Where : class, IBqlWhere, new()
{
  public PXSave<Base> Save;
  public WikiPageMaint<Base, Primary, Where>.WikiPageSave CustomSave;
  public PXAction<Base> Cancel;
  public WikiPageMaint<Base, Primary, Where>.WikiPageDelete Delete;
  public PXSelect<WikiPageSimple, PX.Data.Where<WikiPageSimple.pageID, Equal<WikiPageSimple.pageID>>, OrderBy<Desc<WikiPage.articleType, Asc<WikiPage.number>>>> Folders;
  public PXSelect<WikiSubarticle, PX.Data.Where<WikiSubarticle.pageID, Equal<WikiSubarticle.pageID>>, OrderBy<Asc<WikiSubarticle.number>>> Subarticles;
  public PXAction<Base> MoveUp;
  public PXAction<Base> MoveDown;
  public PXAction<Base> Compare;
  public PXSelect<UploadFileInfo, PX.Data.Where<UploadFile.primaryPageID, Equal<Current<WikiPage.pageID>>>, OrderBy<Asc<UploadFileInfo.name>>> Attachments;
  public PXSelect<UploadFileRevisionNoData, PX.Data.Where<UploadFileRevisionNoData.fileID, Equal<Current<UploadFileInfo.fileID>>, And<UploadFileRevisionNoData.fileRevisionID, Equal<Current<UploadFileInfo.lastRevisionID>>>>> AttachmentRevisions;
  public PXSelect<UploadFileInfo> Links;
  public string GetFileAddress = "";
  public Guid CurrentAttachmentID = Guid.Empty;
  public PXAction<Base> EditFile;

  public WikiPageMaint()
  {
    this.RowDeleting.AddHandler<WikiRevision>(new PXRowDeleting(this.WikiRevisionRowDeleting));
    this.RowSelected.AddHandler<WikiRevision>(new PXRowSelected(this.WikiRevisionRowSelected));
    this.RowDeleted.AddHandler<WikiRevision>(new PXRowDeleted(this.WikiRevisionRowDeleted));
    this.RowPersisting.AddHandler<WikiSubarticle>(new PXRowPersisting(this.WikiSubarticleRowPersisting));
    this.RowUpdated.AddHandler<WikiRevision>(new PXRowUpdated(this.WikiRevisionRowUpdated));
    this.RowUpdating.AddHandler(typeof (Base), new PXRowUpdating(this.WikiPageRowUpdating));
    this.RowPersisting.AddHandler<UploadFileInfo>(new PXRowPersisting(this.UploadFileInfoRowPersisting));
    this.RowSelected.AddHandler<UploadFileInfo>(new PXRowSelected(this.UploadFileInfoRowSelected));
    this.RowUpdating.AddHandler<UploadFileInfo>(new PXRowUpdating(this.UploadFileInfoRowUpdating));
    this.FieldVerifying.AddHandler<UploadFileInfo.revisionCreatedByID>(new PXFieldVerifying(this.UploadFileInfoRevisionCreatedByIDFieldVerifying));
    this.Subarticles.Cache.AllowInsert = false;
    this.Subarticles.Cache.AllowUpdate = false;
    this.Subarticles.Cache.AllowDelete = false;
    this.Attachments.Cache.AllowDelete = false;
    this.Attachments.Cache.AllowInsert = false;
    this.Revisions.Cache.AllowInsert = false;
    PXUIFieldAttribute.SetEnabled(this.Revisions.Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<WikiRevision.selected>(this.Revisions.Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<WikiRevision.selectedDest>(this.Revisions.Cache, (object) null, true);
    this.Compare.SetEnabled(false);
  }

  [PXUIField(DisplayName = "Save", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXSaveButton]
  protected virtual IEnumerable save(PXAdapter adapter)
  {
    this.Persist();
    return adapter.Get();
  }

  protected IEnumerable folders(string PageID)
  {
    WikiPageMaint<Base, Primary, Where> graph = this;
    if ((object) graph.Pages.Current != null)
    {
      WikiPage page = (WikiPage) graph.Pages.Current;
      Guid? nullable = GUID.CreateGuid(PageID);
      Guid guid = nullable ?? page.WikiID.Value;
      PXResultset<WikiPageSimple> pxResultset;
      if (!string.IsNullOrEmpty(PageID))
        pxResultset = PXSelectBase<WikiPageSimple, PXSelectJoin<WikiPageSimple, LeftJoin<WikiPageLanguage, On<WikiPageSimple.pageID, Equal<WikiPageLanguage.pageID>, And<WikiPageLanguage.language, Equal<Required<WikiPageLanguage.language>>>>>, PX.Data.Where<WikiPageSimple.parentUID, Equal<Required<WikiPageSimple.parentUID>>, And<WikiPageSimple.folder, Equal<Required<WikiPageSimple.folder>>>>>.Config>.Select((PXGraph) graph, (object) Thread.CurrentThread.CurrentCulture.Name, (object) guid, (object) 1);
      else
        pxResultset = PXSelectBase<WikiPageSimple, PXSelectJoin<WikiPageSimple, LeftJoin<WikiPageLanguage, On<WikiPageSimple.pageID, Equal<WikiPageLanguage.pageID>, And<WikiPageLanguage.language, Equal<Required<WikiPageLanguage.language>>>>>, PX.Data.Where<WikiPageSimple.pageID, Equal<Required<WikiPageSimple.pageID>>>>.Config>.Select((PXGraph) graph, (object) Thread.CurrentThread.CurrentCulture.Name, (object) page.WikiID);
      foreach (PXResult<WikiPageSimple, WikiPageLanguage> pxResult in pxResultset)
      {
        WikiPageSimple wikiPageSimple = (WikiPageSimple) pxResult;
        WikiPageLanguage wikiPageLanguage = (WikiPageLanguage) pxResult;
        PXWikiProvider wikiProvider1 = PXSiteMap.WikiProvider;
        nullable = wikiPageSimple.PageID;
        Guid key = nullable.Value;
        PXSiteMapNode siteMapNodeFromKey = wikiProvider1.FindSiteMapNodeFromKey(key);
        wikiPageSimple.Title = siteMapNodeFromKey == null ? wikiPageSimple.Name : siteMapNodeFromKey.Title;
        PXWikiProvider wikiProvider2 = PXSiteMap.WikiProvider;
        nullable = wikiPageSimple.PageID;
        Guid pageID = nullable.Value;
        if (wikiProvider2.GetAccessRights(pageID) >= PXWikiRights.Select)
        {
          nullable = wikiPageSimple.PageID;
          Guid? pageId = page.PageID;
          if ((nullable.HasValue == pageId.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() != pageId.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
            yield return (object) wikiPageSimple;
        }
      }
    }
  }

  protected override void OnPageSelected(PXCache sender, WikiPage row, PXWikiRights accessRights)
  {
    base.OnPageSelected(sender, row, accessRights);
    PXResultset<WikiPage> pxResultset = PXSelectBase<WikiPage, PXSelectGroupBy<WikiPage, PX.Data.Where<WikiPage.parentUID, Equal<Current<WikiPage.pageID>>>, Aggregate<Count>>.Config>.Select((PXGraph) this);
    PXCache cache1 = sender;
    int? rowCount = pxResultset.RowCount;
    int num1 = 0;
    int num2 = rowCount.GetValueOrDefault() == num1 & rowCount.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<WikiPage.folder>(cache1, (object) null, num2 != 0);
    WikiDescriptor wikiDescriptor = this.ReadWikiDescriptor(row);
    bool? nullable1;
    int num3;
    if (wikiDescriptor != null)
    {
      nullable1 = wikiDescriptor.RequestApproval;
      bool flag = true;
      num3 = !(nullable1.GetValueOrDefault() == flag & nullable1.HasValue) ? 1 : 0;
    }
    else
      num3 = 1;
    bool flag1 = num3 != 0;
    nullable1 = row.AllowApprove;
    if (!nullable1.HasValue && !flag1 && accessRights >= PXWikiRights.Published)
    {
      EPCompanyTree epCompanyTree = (EPCompanyTree) PXSelectBase<EPCompanyTree, PXSelect<EPCompanyTree, PX.Data.Where<EPCompanyTree.workGroupID, Equal<Required<EPCompanyTree.workGroupID>>, And<EPCompanyTree.workGroupID, IsWorkgroupOrSubgroupOfContact<Current<AccessInfo.contactID>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) row.ApprovalGroupID);
      row.AllowApprove = new bool?(epCompanyTree != null);
    }
    int? nullable2;
    if (flag1)
    {
      nullable2 = row.StatusID;
      int num4 = 1;
      if (nullable2.GetValueOrDefault() == num4 & nullable2.HasValue)
        row.StatusID = new int?(0);
    }
    PXUIFieldAttribute.SetVisible(sender, (object) row, typeof (WikiPage.rejected).Name, !flag1);
    PXUIFieldAttribute.SetVisible(sender, (object) row, typeof (WikiPage.approved).Name, !flag1);
    PXUIFieldAttribute.SetVisible(sender, (object) row, typeof (WikiPage.approvalGroupID).Name, !flag1);
    PXUIFieldAttribute.SetVisible(sender, (object) row, typeof (WikiPage.approvalUserID).Name, !flag1);
    PXCache cache2 = sender;
    WikiPage data1 = row;
    string name1 = typeof (WikiPage.rejected).Name;
    nullable1 = row.AllowApprove;
    bool flag2 = true;
    int num5 = nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled(cache2, (object) data1, name1, num5 != 0);
    PXCache cache3 = sender;
    WikiPage data2 = row;
    string name2 = typeof (WikiPage.approved).Name;
    nullable1 = row.AllowApprove;
    bool flag3 = true;
    int num6 = nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled(cache3, (object) data2, name2, num6 != 0);
    PXCache cache4 = sender;
    WikiPage data3 = row;
    string name3 = typeof (WikiPage.approvalGroupID).Name;
    nullable1 = row.AllowApprove;
    bool flag4 = true;
    int num7 = nullable1.GetValueOrDefault() == flag4 & nullable1.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled(cache4, (object) data3, name3, num7 != 0);
    PXCache cache5 = sender;
    WikiPage data4 = row;
    string name4 = typeof (WikiPage.approvalUserID).Name;
    nullable1 = row.AllowApprove;
    bool flag5 = true;
    int num8 = nullable1.GetValueOrDefault() == flag5 & nullable1.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled(cache5, (object) data4, name4, num8 != 0);
    nullable2 = row.OldStatusID;
    if (!nullable2.HasValue)
    {
      WikiPage wikiPage = row;
      nullable2 = row.StatusID;
      int? nullable3 = new int?(nullable2.GetValueOrDefault());
      wikiPage.OldStatusID = nullable3;
      if (wikiDescriptor != null)
      {
        nullable1 = wikiDescriptor.HoldEntry;
        bool flag6 = true;
        if (nullable1.GetValueOrDefault() == flag6 & nullable1.HasValue && accessRights >= PXWikiRights.Update)
        {
          nullable2 = row.StatusID;
          int num9 = 1;
          if (!(nullable2.GetValueOrDefault() == num9 & nullable2.HasValue))
          {
            nullable2 = row.StatusID;
            int num10 = 4;
            if (!(nullable2.GetValueOrDefault() == num10 & nullable2.HasValue))
              row.Hold = new bool?(true);
          }
        }
      }
    }
    this.Revisions.Cache.AllowDelete = accessRights >= PXWikiRights.Delete;
    if (!(row.Name == "Deleted"))
      return;
    PXUIFieldAttribute.SetEnabled<WikiPage.name>(sender, (object) row, false);
    PXUIFieldAttribute.SetEnabled<WikiPage.folder>(sender, (object) row, false);
    PXUIFieldAttribute.SetEnabled<WikiPage.parentUID>(sender, (object) row, false);
  }

  public void InitNew(Guid wikiid) => this.InitNew(new Guid?(wikiid), new Guid?(), (string) null);

  public void InitNew(Guid? wikiid, Guid? parentid, string name)
  {
    Guid guid = Guid.NewGuid();
    PXView view1 = this.Pages.View;
    PXView view2 = this.Filter.View;
    WikiPageFilter instance1 = (WikiPageFilter) view2.Cache.CreateInstance();
    instance1.PageID = new Guid?(guid);
    view2.Cache.Current = (object) instance1;
    WikiPage instance2 = (WikiPage) view1.Cache.CreateInstance();
    instance2.PageID = new Guid?(guid);
    instance2.WikiID = wikiid;
    instance2.ParentUID = parentid ?? wikiid;
    instance2.Name = instance2.Title = name;
    view1.Cache.Current = view1.Cache.Insert((object) instance2);
  }

  public string GetFileUrl
  {
    get => this.GetFileAddress;
    set => this.GetFileAddress = value;
  }

  public Guid CurrentAttachmentGuid
  {
    get => this.CurrentAttachmentID;
    set => this.CurrentAttachmentID = value;
  }

  protected IEnumerable GetSubarticles()
  {
    WikiPageMaint<Base, Primary, Where> graph = this;
    List<Base> baseList = new List<Base>();
    foreach (PXResult<WikiPage> pxResult in PXSelectBase<WikiPage, PXSelect<WikiPage, PX.Data.Where<WikiPage.parentUID, Equal<Current<WikiPage.pageID>>>, OrderBy<Asc<WikiPage.number>>>.Config>.Select((PXGraph) graph))
    {
      Base subarticle = (Base) (WikiPage) pxResult;
      PXSiteMapNode siteMapNodeFromKey = PXSiteMap.WikiProvider.FindSiteMapNodeFromKey(subarticle.PageID.Value);
      subarticle.Title = siteMapNodeFromKey == null ? subarticle.Name : siteMapNodeFromKey.Title;
      if (PXSiteMap.WikiProvider.GetAccessRights(subarticle.PageID.Value) >= PXWikiRights.Select)
        yield return (object) subarticle;
    }
  }

  public IEnumerable subarticles()
  {
    WikiPageMaint<Base, Primary, Where> wikiPageMaint = this;
    wikiPageMaint.Subarticles.Cache.Clear();
    wikiPageMaint.Subarticles.Cache.AllowUpdate = false;
    wikiPageMaint.Subarticles.Cache.AllowInsert = false;
    wikiPageMaint.Subarticles.Cache.AllowDelete = false;
    if (wikiPageMaint.Caches[typeof (WikiPage)].Current != null)
    {
      PXWikiRights accessRights = PXSiteMap.WikiProvider.GetAccessRights(((WikiPage) wikiPageMaint.Caches[typeof (WikiPage)].Current).PageID.GetValueOrDefault());
      wikiPageMaint.Subarticles.Cache.AllowUpdate = accessRights >= PXWikiRights.Update;
      foreach (Base subarticle in wikiPageMaint.GetSubarticles())
      {
        object obj = wikiPageMaint.Subarticles.Cache.Insert((object) new WikiSubarticle((WikiPage) subarticle));
        wikiPageMaint.Subarticles.Cache.IsDirty = false;
        yield return obj;
      }
    }
  }

  private void WikiSubarticleRowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    e.Cancel = true;
  }

  private void WikiPageRowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    WikiPage row = (WikiPage) e.Row;
    WikiPage newRow = (WikiPage) e.NewRow;
    Guid? parentUid1 = row.ParentUID;
    Guid? parentUid2 = newRow.ParentUID;
    if ((parentUid1.HasValue == parentUid2.HasValue ? (parentUid1.HasValue ? (parentUid1.GetValueOrDefault() == parentUid2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && this.EqualStatus(row.StatusID.GetValueOrDefault(), newRow.StatusID.GetValueOrDefault()) || !(PXSelectorAttribute.Select<WikiPage.parentUID>(sender, e.NewRow) is WikiPageSimple wikiPageSimple))
      return;
    int? statusId = wikiPageSimple.StatusID;
    int num1 = 4;
    if (!(statusId.GetValueOrDefault() == num1 & statusId.HasValue))
    {
      int? articleType = wikiPageSimple.ArticleType;
      int num2 = 1;
      if (!(articleType.GetValueOrDefault() == num2 & articleType.HasValue))
        goto label_4;
    }
    newRow.StatusID = new int?(4);
label_4:
    newRow.OldStatusID = new int?();
  }

  private bool EqualStatus(int statusA, int statusB)
  {
    if (statusA != 4 && statusB == 4)
      return false;
    return statusA != 4 || statusB == 4;
  }

  [PXUIField(DisplayName = "Cancel", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXCancelButton(Tooltip = "Exit Edit Mode (Esc)")]
  protected IEnumerable cancel(PXAdapter a)
  {
    PXCache cach = a.View.Graph.Caches[a.View.GetItemType()];
    WikiPage current = (WikiPage) cach.Current;
    if (cach.GetStatus((object) current) != PXEntryStatus.Inserted)
      WikiActions.View(current);
    else
      WikiActions.View(current.ParentUID);
    return a.Get();
  }

  [PXUIField(DisplayName = "Move Up", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton(ImageKey = "ArrowUp", Tooltip = "Move Article Up")]
  protected IEnumerable moveUp(PXAdapter a)
  {
    WikiPage wikiPage1 = (WikiPage) null;
    WikiSubarticle current1 = (WikiSubarticle) this.Caches[typeof (WikiSubarticle)].Current;
    if (current1 == null)
      return a.Get();
    List<WikiPage> source = new List<WikiPage>();
    foreach (WikiPage subarticle in this.GetSubarticles())
    {
      Guid? nullable1 = current1.WikiID;
      Guid? nullable2 = subarticle.WikiID;
      if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
      {
        nullable2 = current1.PageID;
        nullable1 = subarticle.PageID;
        if ((nullable2.HasValue == nullable1.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
          wikiPage1 = subarticle;
      }
      double? number1 = subarticle.Number;
      double? number2 = current1.Number;
      if (number1.GetValueOrDefault() < number2.GetValueOrDefault() & number1.HasValue & number2.HasValue)
        source.Add(subarticle);
    }
    source.OrderByDescending<WikiPage, double?>((Func<WikiPage, double?>) (p => p.Number));
    List<WikiPage> list = source.Take<WikiPage>(2).ToList<WikiPage>();
    if (list.Count == 0)
      return a.Get();
    if (list.Count == 1)
    {
      WikiPage wikiPage2 = wikiPage1;
      double? number = list.First<WikiPage>().Number;
      double num = 1.0;
      double? nullable = number.HasValue ? new double?(number.GetValueOrDefault() - num) : new double?();
      wikiPage2.Number = nullable;
    }
    if (list.Count == 2)
    {
      WikiPage wikiPage3 = wikiPage1;
      double? number = list.First<WikiPage>().Number;
      double? nullable3 = list.Last<WikiPage>().Number;
      double? nullable4 = number.HasValue & nullable3.HasValue ? new double?(number.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new double?();
      double num = 2.0;
      double? nullable5;
      if (!nullable4.HasValue)
      {
        nullable3 = new double?();
        nullable5 = nullable3;
      }
      else
        nullable5 = new double?(nullable4.GetValueOrDefault() / num);
      wikiPage3.Number = nullable5;
    }
    this.Caches[typeof (WikiPage)].Update((object) wikiPage1);
    WikiPageFilter current2 = this.Filter.Current;
    WikiPageFilter current3 = this.Filter.Current;
    bool? nullable6 = new bool?(true);
    bool? nullable7 = nullable6;
    current3.PrefetchSiteMap = nullable7;
    bool? nullable8 = nullable6;
    current2.PrefetchProvider = nullable8;
    return a.Get();
  }

  [PXUIField(DisplayName = "Move Down", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton(ImageKey = "ArrowDown", Tooltip = "Move Article Down")]
  protected IEnumerable moveDown(PXAdapter a)
  {
    WikiPage wikiPage1 = (WikiPage) null;
    WikiSubarticle current1 = (WikiSubarticle) this.Caches[typeof (WikiSubarticle)].Current;
    if (current1 == null)
      return a.Get();
    List<WikiPage> source = new List<WikiPage>();
    foreach (WikiPage subarticle in this.GetSubarticles())
    {
      Guid? nullable1 = current1.WikiID;
      Guid? nullable2 = subarticle.WikiID;
      if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
      {
        nullable2 = current1.PageID;
        nullable1 = subarticle.PageID;
        if ((nullable2.HasValue == nullable1.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
          wikiPage1 = subarticle;
      }
      double? number1 = subarticle.Number;
      double? number2 = current1.Number;
      if (number1.GetValueOrDefault() > number2.GetValueOrDefault() & number1.HasValue & number2.HasValue)
        source.Add(subarticle);
    }
    source.OrderBy<WikiPage, double?>((Func<WikiPage, double?>) (p => p.Number));
    List<WikiPage> list = source.Take<WikiPage>(2).ToList<WikiPage>();
    if (list.Count == 0)
      return a.Get();
    if (list.Count == 1)
    {
      WikiPage wikiPage2 = wikiPage1;
      double? number = list.First<WikiPage>().Number;
      double num = 1.0;
      double? nullable = number.HasValue ? new double?(number.GetValueOrDefault() + num) : new double?();
      wikiPage2.Number = nullable;
    }
    if (list.Count == 2)
    {
      WikiPage wikiPage3 = wikiPage1;
      double? number = list.First<WikiPage>().Number;
      double? nullable3 = list.Last<WikiPage>().Number;
      double? nullable4 = number.HasValue & nullable3.HasValue ? new double?(number.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new double?();
      double num = 2.0;
      double? nullable5;
      if (!nullable4.HasValue)
      {
        nullable3 = new double?();
        nullable5 = nullable3;
      }
      else
        nullable5 = new double?(nullable4.GetValueOrDefault() / num);
      wikiPage3.Number = nullable5;
    }
    this.Caches[typeof (WikiPage)].Update((object) wikiPage1);
    WikiPageFilter current2 = this.Filter.Current;
    WikiPageFilter current3 = this.Filter.Current;
    bool? nullable6 = new bool?(true);
    bool? nullable7 = nullable6;
    current3.PrefetchSiteMap = nullable7;
    bool? nullable8 = nullable6;
    current2.PrefetchProvider = nullable8;
    return a.Get();
  }

  [PXButton(Tooltip = "Compare the selected versions.")]
  [PXUIField(DisplayName = "Compare", Enabled = true)]
  internal IEnumerable compare(PXAdapter adapter)
  {
    Guid empty = Guid.Empty;
    int? nullable1 = new int?();
    int? nullable2 = new int?();
    foreach (WikiRevision wikiRevision in this.Revisions.Cache.Updated)
    {
      bool? nullable3 = wikiRevision.Selected;
      bool flag1 = true;
      if (nullable3.GetValueOrDefault() == flag1 & nullable3.HasValue)
      {
        empty = wikiRevision.PageID.Value;
        nullable1 = wikiRevision.PageRevisionID;
      }
      else
      {
        nullable3 = wikiRevision.SelectedDest;
        bool flag2 = true;
        if (nullable3.GetValueOrDefault() == flag2 & nullable3.HasValue)
          nullable2 = wikiRevision.PageRevisionID;
      }
      if (nullable1.HasValue)
      {
        if (nullable2.HasValue)
          break;
      }
    }
    PXRedirectToUrlException redirectToUrlException = new PXRedirectToUrlException($"{((Control) HttpContext.Current.Handler).ResolveUrl("~/Wiki/Comparison.aspx")}?pageID={empty.ToString()}&id1={(nullable1 ?? 0).ToString()}&id2={(nullable2 ?? 0).ToString()}", "View Page Version");
    redirectToUrlException.Mode = PXBaseRedirectException.WindowMode.NewWindow;
    throw redirectToUrlException;
  }

  protected void WikiRevisionRowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    bool flag = false;
    foreach (PXResult<WikiRevision> pxResult in this.Revisions.Select())
    {
      if ((WikiRevision) pxResult != e.Row)
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      throw new PXException("The article contains only one revision. This revision cannot be deleted.");
  }

  protected void WikiRevisionRowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    bool flag1 = false;
    bool flag2 = false;
    foreach (PXResult pxResult in this.Revisions.Select())
    {
      WikiRevision wikiRevision = pxResult[typeof (WikiRevision)] as WikiRevision;
      bool? nullable = wikiRevision.Selected;
      bool flag3 = true;
      if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
        flag1 = true;
      nullable = wikiRevision.SelectedDest;
      bool flag4 = true;
      if (nullable.GetValueOrDefault() == flag4 & nullable.HasValue)
      {
        nullable = wikiRevision.Selected;
        bool flag5 = true;
        if (!(nullable.GetValueOrDefault() == flag5 & nullable.HasValue))
          flag2 = true;
      }
    }
    this.Compare.SetEnabled(flag1 & flag2);
  }

  protected void WikiRevisionRowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    bool flag = false;
    WikiRevision row = (WikiRevision) e.Row;
    Base current = this.Pages.Current;
    WikiPageLanguage wikiPageLanguage = this.ReadLanguage((WikiPage) current);
    if ((object) current == null || row == null || wikiPageLanguage == null)
      return;
    int? lastRevisionId = wikiPageLanguage.LastRevisionID;
    int? pageRevisionId1 = row.PageRevisionID;
    if (lastRevisionId.GetValueOrDefault() == pageRevisionId1.GetValueOrDefault() & lastRevisionId.HasValue == pageRevisionId1.HasValue)
    {
      wikiPageLanguage.LastRevisionID = new int?(0);
      flag = true;
    }
    int? lastPublishedId = wikiPageLanguage.LastPublishedID;
    int? pageRevisionId2 = row.PageRevisionID;
    if (lastPublishedId.GetValueOrDefault() == pageRevisionId2.GetValueOrDefault() & lastPublishedId.HasValue == pageRevisionId2.HasValue)
    {
      wikiPageLanguage.LastPublishedID = new int?(0);
      wikiPageLanguage.LastPublishedDateTime = new System.DateTime?();
    }
    foreach (WikiRevision revision in this.revisions())
    {
      int? nullable = wikiPageLanguage.LastRevisionID;
      int num1 = 0;
      if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
        wikiPageLanguage.LastRevisionID = revision.PageRevisionID;
      nullable = wikiPageLanguage.LastPublishedID;
      int num2 = 0;
      if (nullable.GetValueOrDefault() == num2 & nullable.HasValue && revision.Published)
      {
        wikiPageLanguage.LastPublishedID = revision.PageRevisionID;
        wikiPageLanguage.LastPublishedDateTime = revision.ApprovalDateTime;
      }
    }
    foreach (PXResult<WikiPageLink> pxResult in this.pageLinks.Select((object) row.PageID, (object) row.Language, (object) row.PageRevisionID))
      this.pageLinks.Delete((WikiPageLink) pxResult);
    foreach (PXResult<WikiFileInPage> pxResult in this.fileLinks.Select((object) row.PageID, (object) row.Language, (object) row.PageRevisionID))
      this.fileLinks.Delete((WikiFileInPage) pxResult);
    this.Caches[typeof (WikiPageLanguage)].Update((object) wikiPageLanguage);
    if (!flag)
      return;
    current.PageRevisionID = new int?();
    current.PageRevisionDateTime = new System.DateTime?();
    this.Pages.Current = default (Base);
    this.Pages.Current = current;
  }

  protected void WikiRevisionRowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    PXBoolAttribute.CheckSingleRow<WikiRevision.selected>(cache, this.Revisions.View, e.Row);
    PXBoolAttribute.CheckSingleRow<WikiRevision.selectedDest>(cache, this.Revisions.View, e.Row);
  }

  public IEnumerable attachments()
  {
    WikiPageMaint<Base, Primary, Where> wikiPageMaint = this;
    wikiPageMaint.Attachments.Cache.Clear();
    if ((object) wikiPageMaint.Pages.Current != null)
    {
      WikiPageMaint<Base, Primary, Where> graph = wikiPageMaint;
      object[] objArray = new object[1]
      {
        (object) wikiPageMaint.Pages.Current.PageRevisionID
      };
      foreach (PXResult<UploadFile, WikiFileInPage, UploadFileRevisionNoData> pxResult in PXSelectBase<UploadFile, PXSelectJoin<UploadFile, LeftJoin<WikiFileInPage, On<WikiFileInPage.fileID, Equal<UploadFile.fileID>, And<WikiFileInPage.pageID, Equal<Current<WikiPage.pageID>>, And<WikiFileInPage.pageRevisionID, Equal<Required<WikiFileInPage.pageRevisionID>>>>>, LeftJoin<UploadFileRevisionNoData, On<UploadFile.lastRevisionID, Equal<UploadFileRevisionNoData.fileRevisionID>, And<UploadFile.fileID, Equal<UploadFileRevisionNoData.fileID>>>>>, PX.Data.Where<UploadFile.primaryPageID, Equal<Current<WikiPage.pageID>>, Or<WikiFileInPage.fileID, IsNotNull>>>.Config>.Select((PXGraph) graph, objArray))
      {
        UploadFileInfo attachment = new UploadFileInfo((UploadFile) pxResult, (UploadFileRevision) (UploadFileRevisionNoData) pxResult);
        wikiPageMaint.SetAttachmentLinks(attachment);
        UploadFileInfo uploadFileInfo = (UploadFileInfo) wikiPageMaint.Attachments.Cache.Insert((object) attachment);
        if (uploadFileInfo != null)
        {
          wikiPageMaint.Attachments.Cache.IsDirty = false;
          yield return (object) uploadFileInfo;
        }
      }
    }
  }

  public void UploadFileInfoRowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    UploadFileInfo row = (UploadFileInfo) e.Row;
    UploadFileRevision uploadFileRevision = (UploadFileRevision) (UploadFileRevisionNoData) this.AttachmentRevisions.Select();
    if (uploadFileRevision == null)
      return;
    row.Comment = uploadFileRevision.Comment;
  }

  public void UploadFileInfoRevisionCreatedByIDFieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  public void UploadFileInfoRowUpdating(PXCache cache, PXRowUpdatingEventArgs e)
  {
    UploadFileInfo newRow = (UploadFileInfo) e.NewRow;
    if (newRow.Comment != ((UploadFileInfo) e.Row).Comment)
    {
      foreach (PXResult<UploadFileRevisionNoData> pxResult in this.AttachmentRevisions.Select())
      {
        UploadFileRevision uploadFileRevision = (UploadFileRevision) (UploadFileRevisionNoData) pxResult;
        uploadFileRevision.Comment = newRow.Comment;
        PXGraph pxGraph = new PXGraph();
        pxGraph.Views.Caches.Add(typeof (UploadFileRevision));
        pxGraph.Caches[typeof (UploadFileRevision)].Update((object) uploadFileRevision);
        pxGraph.Actions.PressSave();
      }
    }
    this.Revisions.Cache.Clear();
    this.Revisions.View.Clear();
    this.Revisions.View.RequestRefresh();
    ((UploadFileInfo) e.Row).Comment = newRow.Comment;
    e.Cancel = true;
  }

  public void UploadFileInfoRowPersisting(PXCache cache, PXRowPersistingEventArgs eventArgs)
  {
    eventArgs.Cancel = true;
  }

  private void SetAttachmentLinks(UploadFileInfo attachment)
  {
    if (attachment == null || !attachment.FileID.HasValue || string.IsNullOrEmpty(attachment.Name))
      return;
    attachment.ExternalLink = $"{this.GetFileAddress}?fileID={attachment.FileID.ToString()}";
    if (MimeTypes.GetMimeType(attachment.Name.Substring(System.Math.Max(0, attachment.Name.LastIndexOf('.')))).StartsWith("image/"))
      attachment.WikiLink = $"[Image:{PXBlockParser.EncodeSpecialChars(attachment.Name)}]";
    else
      attachment.WikiLink = $"[{{up}}{PXBlockParser.EncodeSpecialChars(attachment.Name)}]";
  }

  public IEnumerable links()
  {
    if (this.Attachments != null)
    {
      foreach (UploadFileInfo uploadFileInfo in this.Attachments.Cache.Inserted)
      {
        Guid? fileId = uploadFileInfo.FileID;
        Guid currentAttachmentId = this.CurrentAttachmentID;
        if ((fileId.HasValue ? (fileId.HasValue ? (fileId.GetValueOrDefault() == currentAttachmentId ? 1 : 0) : 1) : 0) != 0)
        {
          yield return (object) uploadFileInfo;
          break;
        }
      }
    }
  }

  [PXUIField(DisplayName = "Edit")]
  [PXButton]
  public IEnumerable editFile(PXAdapter a)
  {
    if (this.Attachments.Current == null)
      return a.Get();
    WikiFileMaintenance instance = PXGraph.CreateInstance<WikiFileMaintenance>();
    instance.Files.Current = (UploadFileWithIDSelector) instance.Files.Search<UploadFile.name>((object) this.Attachments.Current.Name);
    throw new PXRedirectRequiredException((PXGraph) instance, "Edit");
  }

  /// <exclude />
  public class WikiPageSave(PXGraph graph, string name) : PXSave<Base>(graph, name)
  {
    [PXUIField(DisplayName = "Save", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
    [PXSaveButton(Tooltip = "Save")]
    protected override IEnumerable Handler(PXAdapter adapter)
    {
      WikiPage current = (WikiPage) adapter.View.Graph.Caches[typeof (Base)].Current;
      if (current == null)
        return (IEnumerable) new List<object>();
      if (string.IsNullOrEmpty(current.Name))
        adapter.View.Graph.Caches[typeof (Base)].RaiseExceptionHandling<WikiPage.name>((object) current, (object) null, (Exception) new PXSetPropertyException("Fill Article ID", PXErrorLevel.Error));
      try
      {
        foreach (object obj in base.Handler(adapter))
          ;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
      if (adapter.View.Graph is WikiPageMaint<Base, Primary, Where> graph && graph.Filter.Current != null && graph.Filter.Current.RefreshTree.Value)
        throw new Exception($"Refresh:{current.PageID}|{PX.SM.Wiki.Url(current.PageID)}");
      throw new PXRedirectToUrlException(PX.SM.Wiki.Url(current.PageID), "WikiPageSaved");
    }
  }

  /// <exclude />
  public class WikiPageDelete(PXGraph graph, string name) : PXDelete<Base>(graph, name)
  {
    [PXUIField(DisplayName = "Delete", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
    [PXDeleteButton]
    protected override IEnumerable Handler(PXAdapter adapter)
    {
      foreach (object obj in base.Handler(adapter))
        ;
      WikiPage current = (WikiPage) adapter.View.Graph.Caches[typeof (Base)].Current;
      if (current != null)
        throw new PXRedirectToUrlException(PX.SM.Wiki.Url(current.ParentUID), "WikiPageDeleted");
      return (IEnumerable) new List<object>();
    }
  }
}
