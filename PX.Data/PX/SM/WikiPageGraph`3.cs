// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiPageGraph`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Search;
using PX.Data.Wiki.Parser;
using PX.Data.Wiki.Parser.PlainTxt;
using System;
using System.Collections;
using System.Web.UI;

#nullable disable
namespace PX.SM;

/// <exclude />
[PXHidden]
public class WikiPageGraph<Base, Primary, Where> : 
  WikiPageReader<Base, Primary, Where>,
  IWikiMaintGraph
  where Base : WikiPage, new()
  where Primary : class, IBqlTable, new()
  where Where : class, IBqlWhere, new()
{
  public PXSelect<PX.SM.Roles, PX.Data.Where<PX.SM.Roles.applicationName, Equal<Required<PX.SM.Roles.applicationName>>>> Roles;
  public PXSelect<WikiAccessRights, PX.Data.Where<WikiAccessRights.pageID, Equal<Required<WikiAccessRights.pageID>>>> PageAccessRights;
  public PXSelect<WikiAccessRoles> AccessRights;
  public PXSelect<WikiRevisionTag> RevisionTags;
  public PXAction<Base> ProcessTag;
  public PXSelect<WikiPageLink, PX.Data.Where<WikiPageLink.pageID, Equal<Required<WikiPageLink.pageID>>, And<WikiPageLink.language, Equal<Required<WikiPageLink.language>>, And<WikiPageLink.pageRevisionID, Equal<Required<WikiPageLink.pageRevisionID>>>>>> pageLinks;
  public PXSelect<WikiFileInPage, PX.Data.Where<WikiFileInPage.pageID, Equal<Required<WikiFileInPage.pageID>>, And<WikiFileInPage.language, Equal<Required<WikiFileInPage.language>>, And<WikiFileInPage.pageRevisionID, Equal<Required<WikiFileInPage.pageRevisionID>>>>>> fileLinks;
  protected bool isAppvoalUpdConf;
  private WikiPage deletedRow;
  private readonly PXSelectBase<WikiPageLanguage> pageLanguages;

  public WikiPageGraph()
  {
    System.Type table = typeof (Base);
    this.RowInserted.AddHandler(table, new PXRowInserted(this.OnRowInserted));
    this.RowUpdated.AddHandler(table, new PXRowUpdated(this.OnRowUpdated));
    this.RowUpdating.AddHandler(table, new PXRowUpdating(this.OnRowUpdating));
    this.RowDeleting.AddHandler(table, new PXRowDeleting(this.OnRowDeleting));
    this.RowDeleted.AddHandler(table, new PXRowDeleted(this.OnRowDeleted));
    this.RowPersisting.AddHandler(table, new PXRowPersisting(this.OnRowPersisting));
    this.FieldVerifying.AddHandler<WikiPage.parentUID>(new PXFieldVerifying(this.OnParentUIDVerifying));
    this.FieldUpdating.AddHandler<WikiPage.content>(new PXFieldUpdating(this.OnContentUpdating));
    this.FieldDefaulting.AddHandler<WikiPage.pageID>(new PXFieldDefaulting(this.OnGuidNew));
    this.FieldDefaulting.AddHandler<WikiPage.parentUID>(new PXFieldDefaulting(this.OnGuidEmpty));
    this.FieldUpdated.AddHandler<WikiPage.name>(new PXFieldUpdated(this.OnWikiPageNameFieldUpdated));
    this.FieldUpdated.AddHandler<WikiPage.parentUID>(new PXFieldUpdated(this.BaseParentUIDFieldUpdated));
    this.pageLanguages = (PXSelectBase<WikiPageLanguage>) new PXSelect<WikiPageLanguage, PX.Data.Where<WikiPageLanguage.pageID, Equal<Required<WikiPageLanguage.pageID>>>>((PXGraph) this);
    this.Views.Caches.Add(typeof (WikiPageLanguage));
    this.Views.Caches.Add(typeof (UploadFile));
    this.Views.Caches.Add(typeof (UploadFileRevision));
    this.Views.Caches.Add(typeof (WikiFileInPage));
    this.Views.Caches.Add(typeof (WikiPageLink));
    this.AccessRights.Cache.AllowInsert = false;
    this.AccessRights.Cache.AllowDelete = false;
    PXUIFieldAttribute.SetEnabled<WikiAccessRoles.guest>(this.AccessRights.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<WikiAccessRights.roleName>(this.AccessRights.Cache, (object) null, false);
  }

  protected IEnumerable accessRights()
  {
    WikiPageGraph<Base, Primary, Where> wikiPageGraph = this;
    wikiPageGraph.AccessRights.Cache.Clear();
    if ((object) wikiPageGraph.Pages.Current != null)
    {
      Guid? nullable = wikiPageGraph.Pages.Current.PageID;
      Guid pageID = nullable ?? Guid.Empty;
      nullable = wikiPageGraph.Pages.Current.ParentUID;
      Guid parentID = nullable ?? Guid.Empty;
      bool hasParRights = PXSiteMap.WikiProvider.HasAccessRights(parentID);
      PXUIFieldAttribute.SetVisible(wikiPageGraph.AccessRights.Cache, "ParentAccessRights", hasParRights);
      PXResultset<WikiAccessRights> artRoles = wikiPageGraph.PageAccessRights.Select((object) pageID);
      foreach (PXResult<PX.SM.Roles> pxResult1 in wikiPageGraph.Roles["/"])
      {
        PX.SM.Roles roles = (PX.SM.Roles) pxResult1;
        WikiAccessRoles wikiAccessRoles = new WikiAccessRoles();
        wikiAccessRoles.PageID = new Guid?(pageID);
        wikiAccessRoles.Guest = roles.Guest;
        wikiAccessRoles.RoleName = roles.Rolename;
        wikiAccessRoles.ApplicationName = roles.ApplicationName;
        wikiAccessRoles.AccessRights = new short?((short) -1);
        wikiAccessRoles.ParentAccessRights = new short?((short) -1);
        if (hasParRights && PXSiteMap.WikiProvider.HasAccessRights(parentID, roles.Rolename))
          wikiAccessRoles.ParentAccessRights = new short?((short) PXSiteMap.WikiProvider.GetAccessRights(parentID, roles.Rolename));
        foreach (PXResult<WikiAccessRights> pxResult2 in artRoles)
        {
          WikiAccessRights wikiAccessRights = (WikiAccessRights) pxResult2;
          if (wikiAccessRights.RoleName == roles.Rolename)
            wikiAccessRoles.AccessRights = wikiAccessRights.AccessRights;
        }
        yield return wikiPageGraph.AccessRights.Cache.Insert((object) wikiAccessRoles);
      }
      wikiPageGraph.AccessRights.Cache.IsDirty = false;
    }
  }

  protected IEnumerable revisionTags()
  {
    WikiPageGraph<Base, Primary, Where> wikiPageGraph = this;
    WikiPage current = (WikiPage) wikiPageGraph.Pages.Current;
    if (current != null)
    {
      WikiPageGraph<Base, Primary, Where> graph = wikiPageGraph;
      object[] objArray = new object[4]
      {
        (object) current.WikiID,
        (object) current.PageID,
        (object) current.Language,
        (object) current.PageRevisionID
      };
      foreach (PXResult<WikiRevisionTag> pxResult in PXSelectBase<WikiRevisionTag, PXSelect<WikiRevisionTag, PX.Data.Where<WikiRevisionTag.wikiID, Equal<Required<WikiRevisionTag.wikiID>>, And<WikiRevisionTag.pageID, Equal<Required<WikiRevisionTag.pageID>>, And<WikiRevisionTag.language, Equal<Required<WikiRevisionTag.language>>, And<WikiRevisionTag.pageRevisionID, Equal<Required<WikiRevisionTag.pageRevisionID>>>>>>>.Config>.Select((PXGraph) graph, objArray))
        yield return (object) (WikiRevisionTag) pxResult;
    }
  }

  protected void WikiRevisionTag_Language_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this.Pages.Current != null ? (object) this.Pages.Current.Language : (object) (string) null;
  }

  protected void WikiRevisionTag_PageRevisionID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) ((object) this.Pages.Current != null ? this.Pages.Current.PageRevisionID : new int?());
  }

  [PXButton(ImageKey = "Process", Tooltip = "Process tag for all subarticles.")]
  [PXUIField(DisplayName = "Process Tag", MapEnableRights = PXCacheRights.Update)]
  protected virtual IEnumerable processTag(PXAdapter adapter)
  {
    if (this.RevisionTags.Current != null)
    {
      int? tagId = this.RevisionTags.Current.TagID;
      if (tagId.HasValue && (object) this.Pages.Current != null && this.Pages.Current.PageID.HasValue)
      {
        Guid pageID = this.Pages.Current.PageID.Value;
        string language = this.Pages.Current.Language;
        tagId = this.RevisionTags.Current.TagID;
        int tagID = tagId.Value;
        this.DoProcessTag(pageID, language, tagID);
      }
    }
    return adapter.Get();
  }

  protected void DoProcessTag(Guid pageID, string language, int tagID)
  {
    foreach (PXResult<WikiPageSimple, WikiPageLanguage> pxResult in PXSelectBase<WikiPageSimple, PXSelectJoin<WikiPageSimple, InnerJoin<WikiPageLanguage, On<WikiPageLanguage.pageID, Equal<WikiPage.pageID>, And<WikiPageLanguage.language, Equal<Required<WikiPageLanguage.language>>>>>, PX.Data.Where<WikiPage.parentUID, Equal<Required<WikiPage.parentUID>>>>.Config>.Select((PXGraph) this, (object) language, (object) pageID))
    {
      WikiPageSimple wikiPageSimple = (WikiPageSimple) pxResult;
      WikiPageLanguage wikiPageLanguage = (WikiPageLanguage) pxResult;
      int? lastPublishedId = wikiPageLanguage.LastPublishedID;
      int num = 0;
      if (lastPublishedId.GetValueOrDefault() > num & lastPublishedId.HasValue)
        this.RevisionTags.Cache.Insert((object) new WikiRevisionTag()
        {
          WikiID = wikiPageSimple.WikiID,
          PageID = wikiPageSimple.PageID,
          Language = wikiPageLanguage.Language,
          PageRevisionID = wikiPageLanguage.LastPublishedID,
          TagID = new int?(tagID)
        });
      this.DoProcessTag(wikiPageSimple.PageID.Value, language, tagID);
    }
  }

  protected WikiRevision GetRevision(Base rec)
  {
    return (WikiRevision) PXSelectBase<WikiRevision, PXSelectJoin<WikiRevision, InnerJoin<WikiPageLanguage, On<WikiRevision.pageID, Equal<WikiPageLanguage.pageID>>>, PX.Data.Where<WikiRevision.pageID, Equal<Required<WikiPage.pageID>>, And<WikiRevision.pageRevisionID, Equal<Required<WikiRevision.pageRevisionID>>, And<WikiPageLanguage.language, Equal<Required<WikiPageLanguage.language>>>>>>.Config>.Select((PXGraph) this, (object) rec.PageID, (object) rec.PageRevisionID, (object) rec.Language);
  }

  public override void Persist()
  {
    foreach (Base @base in this.Pages.Cache.Cached)
    {
      switch (this.Pages.Cache.GetStatus((object) @base))
      {
        case PXEntryStatus.Updated:
        case PXEntryStatus.Inserted:
          int? nullable = @base.ArticleType;
          int num1 = 0;
          if (!(nullable.GetValueOrDefault() == num1 & nullable.HasValue))
          {
            if (string.IsNullOrEmpty(@base.Title))
            {
              this.Pages.Cache.RaiseExceptionHandling<WikiPage.title>((object) @base, (object) @base.Title, (Exception) new PXSetPropertyException("Error: 'Name' may not be empty."));
              continue;
            }
            this.Pages.Cache.Current = (object) @base;
            WikiRevision revision = this.GetRevision(@base);
            if (revision == null || @base.Content != revision.Content)
              revision = this.CreateRevision((WikiPage) @base, true);
            else
              this.CreateRevision((WikiPage) @base, false);
            nullable = @base.StatusID;
            int num2 = 3;
            if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
            {
              this.Publish(revision, (WikiPage) @base);
              continue;
            }
            continue;
          }
          continue;
        default:
          continue;
      }
    }
    bool flag1 = false;
    bool flag2 = false;
    base.Persist();
    if (this.Filter.Current != null)
    {
      bool? nullable = this.Filter.Current.PrefetchProvider;
      flag1 = nullable.GetValueOrDefault();
      nullable = this.Filter.Current.PrefetchSiteMap;
      flag2 = nullable.GetValueOrDefault();
      this.Filter.Current.PrefetchProvider = new bool?(false);
      this.Filter.Current.PrefetchSiteMap = new bool?(false);
      this.Filter.Cache.IsDirty = false;
    }
    if (flag1)
      PXSiteMap.WikiProvider.Clear();
    if (flag2 && PXSiteMap.Provider != null)
      PXSiteMap.Provider.Clear();
    if (this.Filter.Current == null)
      return;
    this.Filter.Current.RefreshTree = new bool?(flag1 | flag2);
  }

  protected void Publish(WikiRevision rev, WikiPage pg)
  {
    WikiPageLanguage wikiPageLanguage = this.ReadLanguage(pg);
    rev.ApprovalByID = new Guid?(this.Accessinfo.UserID);
    rev.ApprovalDateTime = new System.DateTime?(System.DateTime.Now);
    this.Caches[typeof (WikiRevision)].Update((object) rev);
    wikiPageLanguage.LastPublishedID = rev.PageRevisionID;
    wikiPageLanguage.LastPublishedDateTime = rev.ApprovalDateTime;
    this.Caches[typeof (WikiPageLanguage)].Update((object) wikiPageLanguage);
  }

  protected void WikiAccessRoles_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    e.Cancel = true;
  }

  protected void WikiAccessRoles_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    WikiAccessRoles row = (WikiAccessRoles) e.Row;
    WikiAccessRights wikiAccessRights = new WikiAccessRights();
    wikiAccessRights.PageID = row.PageID;
    wikiAccessRights.ApplicationName = row.ApplicationName;
    wikiAccessRights.RoleName = row.RoleName;
    wikiAccessRights.AccessRights = row.AccessRights;
    short? accessRights = wikiAccessRights.AccessRights;
    int? nullable = accessRights.HasValue ? new int?((int) accessRights.GetValueOrDefault()) : new int?();
    int num = -1;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      this.PageAccessRights.Delete(wikiAccessRights);
    else
      this.PageAccessRights.Update(wikiAccessRights);
    this.Filter.Current.PrefetchProvider = new bool?(true);
  }

  private void OnGuidNew(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) Guid.NewGuid();
  }

  private void OnGuidEmpty(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) null;
  }

  protected override void OnPageSelected(PXCache sender, WikiPage row, PXWikiRights accessRights)
  {
    base.OnPageSelected(sender, row, accessRights);
    this.AccessRights.Cache.AllowUpdate = accessRights >= PXWikiRights.Delete;
    PXUIFieldAttribute.SetEnabled<WikiPage.parentUID>(sender, (object) row, accessRights >= PXWikiRights.Update);
    PXCache cache1 = sender;
    WikiPage data1 = row;
    int? statusId1 = row.StatusID;
    int num1 = 4;
    int num2 = !(statusId1.GetValueOrDefault() == num1 & statusId1.HasValue) ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<WikiPage.statusID>(cache1, (object) data1, num2 != 0);
    WikiDescriptor wikiDescriptor = this.ReadWikiDescriptor(row);
    if (wikiDescriptor == null)
      return;
    PXCache cache2 = sender;
    WikiPage data2 = row;
    string name1 = typeof (WikiPage.hold).Name;
    int num3;
    if (accessRights >= PXWikiRights.Published)
    {
      int? statusId2 = row.StatusID;
      int num4 = 4;
      num3 = !(statusId2.GetValueOrDefault() == num4 & statusId2.HasValue) ? 1 : 0;
    }
    else
      num3 = 0;
    PXUIFieldAttribute.SetEnabled(cache2, (object) data2, name1, num3 != 0);
    PXCache cache3 = sender;
    WikiPage data3 = row;
    string name2 = typeof (WikiPage.approvalGroupID).Name;
    int num5 = accessRights >= PXWikiRights.Published ? 1 : 0;
    bool? requestApproval1 = wikiDescriptor.RequestApproval;
    bool flag1 = true;
    int num6 = requestApproval1.GetValueOrDefault() == flag1 & requestApproval1.HasValue ? 1 : 0;
    int num7 = num5 & num6;
    PXUIFieldAttribute.SetEnabled(cache3, (object) data3, name2, num7 != 0);
    PXCache cache4 = sender;
    WikiPage data4 = row;
    string name3 = typeof (WikiPage.approvalUserID).Name;
    int num8 = accessRights >= PXWikiRights.Published ? 1 : 0;
    bool? requestApproval2 = wikiDescriptor.RequestApproval;
    bool flag2 = true;
    int num9 = requestApproval2.GetValueOrDefault() == flag2 & requestApproval2.HasValue ? 1 : 0;
    int num10 = num8 & num9;
    PXUIFieldAttribute.SetEnabled(cache4, (object) data4, name3, num10 != 0);
    PXCache cache5 = sender;
    string name4 = typeof (WikiPage.approvalGroupID).Name;
    WikiPage data5 = row;
    requestApproval2 = wikiDescriptor.RequestApproval;
    bool flag3 = true;
    int check = requestApproval2.GetValueOrDefault() == flag3 & requestApproval2.HasValue ? 1 : 2;
    PXDefaultAttribute.SetPersistingCheck(cache5, name4, (object) data5, (PXPersistingCheck) check);
  }

  protected override WikiRevision OnReadRevision(WikiPage page, string lang, int revisionID)
  {
    WikiRevision wikiRevision = base.OnReadRevision(page, lang, revisionID);
    if (wikiRevision != null && wikiRevision.Content != null)
      wikiRevision.Content = wikiRevision.Content != null ? new GuidToLink((PXGraph) this, wikiRevision.Content, new Guid?()).Result : (string) null;
    return wikiRevision;
  }

  private void OnParentUIDVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!(PXSelectorAttribute.Select<WikiPage.parentUID>(sender, e.Row, e.NewValue) is WikiPageSimple wikiPageSimple))
      return;
    int accessRights = (int) PXSiteMap.WikiProvider.GetAccessRights(wikiPageSimple.PageID.Value);
    if (accessRights < 3)
      throw new PXNotEnoughRightsException(PXCacheRights.Insert, "You don't have access rights to add an article into this folder.");
    if (accessRights >= 5)
      return;
    int? statusId = wikiPageSimple.StatusID;
    int num = 4;
    if (statusId.GetValueOrDefault() == num & statusId.HasValue)
      throw new PXNotEnoughRightsException(PXCacheRights.Delete);
  }

  private void OnContentUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
  }

  private void OnRowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    this.Filter.Current.PrefetchProvider = new bool?(true);
  }

  protected virtual void OnRowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    WikiPage row = (WikiPage) e.Row;
    WikiPage newRow = (WikiPage) e.NewRow;
    Guid? nullable1 = row.ParentUID;
    Guid? parentUid = newRow.ParentUID;
    int? nullable2;
    bool? nullable3;
    int? nullable4;
    if ((nullable1.HasValue == parentUid.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != parentUid.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
    {
      if (PXSelectorAttribute.Select<WikiPage.parentUID>(sender, e.NewRow) is WikiPageSimple wikiPageSimple1)
      {
        int? statusId = wikiPageSimple1.StatusID;
        int num = 4;
        if (statusId.GetValueOrDefault() == num & statusId.HasValue)
          newRow.StatusID = new int?(4);
        if (!this.isAppvoalUpdConf)
        {
          int? approvalGroupId = row.ApprovalGroupID;
          nullable2 = wikiPageSimple1.ApprovalGroupID;
          if (approvalGroupId.GetValueOrDefault() == nullable2.GetValueOrDefault() & approvalGroupId.HasValue == nullable2.HasValue)
          {
            Guid? approvalUserId = row.ApprovalUserID;
            nullable1 = wikiPageSimple1.ApprovalUserID;
            if ((approvalUserId.HasValue == nullable1.HasValue ? (approvalUserId.HasValue ? (approvalUserId.GetValueOrDefault() != nullable1.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
              goto label_17;
          }
          nullable3 = this.ReadWikiDescriptor(row).RequestApproval;
          bool flag = true;
          if (nullable3.GetValueOrDefault() == flag & nullable3.HasValue && this.Pages.View.Ask("Do you want to update approval settings by parent?", MessageButtons.YesNo) == WebDialogResult.Yes)
          {
            this.isAppvoalUpdConf = true;
            newRow.ApprovalGroupID = wikiPageSimple1.ApprovalGroupID;
            newRow.ApprovalUserID = wikiPageSimple1.ApprovalUserID;
            WikiPage wikiPage = newRow;
            nullable3 = new bool?();
            bool? nullable5 = nullable3;
            wikiPage.AllowApprove = nullable5;
          }
        }
      }
    }
    else
    {
      nullable2 = newRow.ApprovalGroupID;
      if (!nullable2.HasValue && !this.isAppvoalUpdConf)
      {
        WikiDescriptor wikiDescriptor = this.ReadWikiDescriptor(row);
        nullable3 = wikiDescriptor.RequestApproval;
        bool flag = true;
        if (nullable3.GetValueOrDefault() == flag & nullable3.HasValue)
        {
          WikiPageSimple wikiPageSimple2 = PXSelectorAttribute.Select<WikiPage.parentUID>(sender, e.NewRow) as WikiPageSimple;
          newRow.ApprovalGroupID = wikiPageSimple2 != null ? wikiPageSimple2.ApprovalGroupID : wikiDescriptor.ApprovalGroupID;
          newRow.ApprovalUserID = wikiPageSimple2 != null ? wikiPageSimple2.ApprovalUserID : wikiDescriptor.ApprovalUserID;
          WikiPage wikiPage = newRow;
          nullable3 = new bool?();
          bool? nullable6 = nullable3;
          wikiPage.AllowApprove = nullable6;
          this.isAppvoalUpdConf = true;
        }
      }
      else
      {
        nullable1 = newRow.ApprovalUserID;
        Guid? approvalUserId = row.ApprovalUserID;
        if ((nullable1.HasValue == approvalUserId.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != approvalUserId.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
        {
          nullable2 = newRow.ApprovalGroupID;
          nullable4 = row.ApprovalGroupID;
          if (nullable2.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable2.HasValue == nullable4.HasValue)
            goto label_17;
        }
        bool? folder = newRow.Folder;
        bool flag1 = true;
        if (folder.GetValueOrDefault() == flag1 & folder.HasValue && !this.isAppvoalUpdConf)
        {
          nullable3 = this.ReadWikiDescriptor(row).RequestApproval;
          bool flag2 = true;
          if (nullable3.GetValueOrDefault() == flag2 & nullable3.HasValue && this.Pages.View.Ask("Do you want to update approval settings in all subarticles?", MessageButtons.YesNo) == WebDialogResult.Yes)
            this.isAppvoalUpdConf = true;
        }
      }
    }
label_17:
    nullable4 = newRow.StatusID;
    int num1 = 1;
    if (nullable4.GetValueOrDefault() == num1 & nullable4.HasValue)
    {
      WikiDescriptor wikiDescriptor = this.ReadWikiDescriptor(row);
      if (wikiDescriptor != null)
      {
        nullable3 = wikiDescriptor.RequestApproval;
        bool flag = true;
        if (nullable3.GetValueOrDefault() == flag & nullable3.HasValue)
          goto label_21;
      }
      newRow.StatusID = new int?(3);
    }
label_21:
    if (this.deletedRow != null)
      return;
    nullable4 = row.StatusID;
    nullable2 = newRow.StatusID;
    if (nullable4.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable4.HasValue == nullable2.HasValue)
      return;
    nullable2 = row.StatusID;
    int num2 = 4;
    if (!(nullable2.GetValueOrDefault() == num2 & nullable2.HasValue))
    {
      nullable2 = newRow.StatusID;
      int num3 = 4;
      if (!(nullable2.GetValueOrDefault() == num3 & nullable2.HasValue))
        return;
    }
    this.deletedRow = (WikiPage) sender.CreateCopy((object) row);
  }

  protected virtual void OnRowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    WikiPage row = (WikiPage) e.Row;
    WikiPage oldRow = (WikiPage) e.OldRow;
    Guid? parentUid1 = row.ParentUID;
    Guid? parentUid2 = oldRow.ParentUID;
    int? statusId1;
    if ((parentUid1.HasValue == parentUid2.HasValue ? (parentUid1.HasValue ? (parentUid1.GetValueOrDefault() != parentUid2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
    {
      bool? folder = row.Folder;
      bool? nullable = oldRow.Folder;
      if (folder.GetValueOrDefault() == nullable.GetValueOrDefault() & folder.HasValue == nullable.HasValue)
      {
        int? statusId2 = row.StatusID;
        statusId1 = oldRow.StatusID;
        if (statusId2.GetValueOrDefault() == statusId1.GetValueOrDefault() & statusId2.HasValue == statusId1.HasValue && !(row.Title != oldRow.Title))
        {
          nullable = row.IsHtml;
          bool? isHtml = oldRow.IsHtml;
          if (nullable.GetValueOrDefault() == isHtml.GetValueOrDefault() & nullable.HasValue == isHtml.HasValue)
            goto label_5;
        }
      }
    }
    this.Filter.Current.PrefetchProvider = new bool?(true);
label_5:
    if (!this.isAppvoalUpdConf)
    {
      if (this.deletedRow == null)
        return;
      statusId1 = row.StatusID;
      int? statusId3 = this.deletedRow.StatusID;
      if (statusId1.GetValueOrDefault() == statusId3.GetValueOrDefault() & statusId1.HasValue == statusId3.HasValue)
        return;
    }
    foreach (PXResult<Base> pxResult in PXSelectBase<Base, PXSelect<Base, PX.Data.Where<WikiPage.parentUID, Equal<Required<WikiPage.parentUID>>>>.Config>.Select((PXGraph) this, (object) row.PageID))
    {
      Base @base = (Base) pxResult;
      Base copy = (Base) sender.CreateCopy((object) @base);
      if (this.deletedRow != null)
      {
        int? statusId4 = row.StatusID;
        statusId1 = this.deletedRow.StatusID;
        if (!(statusId4.GetValueOrDefault() == statusId1.GetValueOrDefault() & statusId4.HasValue == statusId1.HasValue))
          copy.StatusID = row.StatusID;
      }
      if (this.isAppvoalUpdConf)
      {
        copy.ApprovalGroupID = row.ApprovalGroupID;
        copy.ApprovalUserID = row.ApprovalUserID;
      }
      sender.Update((object) copy);
    }
  }

  public virtual void OnWikiCreated(WikiDescriptor rec)
  {
    PXDatabase.Insert<Base>(new PXDataFieldAssign("PageID", (object) rec.DeletedID));
  }

  public virtual void OnWikiUpdated(WikiDescriptor rec)
  {
  }

  public virtual void OnWikiDeleted(WikiDescriptor rec)
  {
  }

  public System.Type PageType => typeof (Base);

  private void OnWikiPageNameFieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    WikiPage row = (WikiPage) e.Row;
    if ((WikiPage) PXSelectBase<WikiPage, PXSelect<WikiPage, PX.Data.Where<WikiPage.name, Equal<Required<WikiPage.name>>, And<WikiPage.wikiID, Equal<Required<WikiPage.wikiID>>, And<WikiPage.pageID, NotEqual<Required<WikiPage.pageID>>>>>>.Config>.Select((PXGraph) this, (object) row.Name, (object) row.WikiID, (object) row.PageID) != null)
      throw new PXSetPropertyException("An article with the same name already exists.");
    if (row == null || row.Name == null)
      return;
    if (row.Name.IndexOfAny(new char[3]{ '/', '\\', '.' }) != -1)
      throw new PXSetPropertyException("The following symbols cannot be used in an article ID: '/', '\\', '.'");
  }

  private void BaseParentUIDFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    ((Base) e.Row).Number = new double?();
  }

  private void OnRowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    Base row = (Base) e.Row;
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Delete)
    {
      double? number = row.Number;
      if (number.HasValue)
        return;
      double num = 0.0;
      foreach (PXResult<WikiPage> pxResult in PXSelectBase<WikiPage, PXSelect<WikiPage, PX.Data.Where<WikiPage.parentUID, Equal<Required<WikiPage.parentUID>>>>.Config>.Select((PXGraph) this, (object) row.ParentUID))
      {
        WikiPage wikiPage = (WikiPage) pxResult;
        double val1 = num;
        number = wikiPage.Number;
        double valueOrDefault = number.GetValueOrDefault();
        num = System.Math.Max(val1, valueOrDefault);
      }
      row.Number = new double?(num + 1.0);
    }
    else
    {
      this.Filter.Current.PrefetchProvider = new bool?(true);
      this.pageLanguages.Cache.Clear();
      this.Revisions.Cache.Clear();
      PXDatabase.Delete<WikiAccessRights>(new PXDataFieldRestrict(typeof (WikiAccessRights.pageID).Name, PXDbType.UniqueIdentifier, (object) row.PageID));
      PXDatabase.Delete<WikiFileInPage>(new PXDataFieldRestrict(typeof (WikiFileInPage.pageID).Name, PXDbType.UniqueIdentifier, (object) row.PageID));
      PXDatabase.Delete<WikiPageLink>(new PXDataFieldRestrict(typeof (WikiPageLink.pageID).Name, PXDbType.UniqueIdentifier, (object) row.PageID));
      UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
      foreach (PXResult<UploadFile> pxResult in PXSelectBase<UploadFile, PXSelect<UploadFile, PX.Data.Where<UploadFile.primaryPageID, Equal<Required<UploadFile.primaryPageID>>>>.Config>.Select((PXGraph) this, (object) row.PageID))
      {
        UploadFile uploadFile = (UploadFile) pxResult;
        instance.Files.Delete(uploadFile);
      }
      instance.Actions.PressSave();
      WikiActions.RemoveArticleForever(row.PageID);
    }
  }

  private void OnRowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    WikiPage row = (WikiPage) e.Row;
    WikiDescriptor wikiDescriptor = (WikiDescriptor) PXSelectBase<WikiDescriptor, PXSelect<WikiDescriptor, PX.Data.Where<WikiDescriptor.pageID, Equal<Required<WikiDescriptor.pageID>>>>.Config>.Select((PXGraph) this, (object) row.WikiID);
    if (this.deletedRow == null)
      this.deletedRow = (WikiPage) sender.CreateCopy((object) row);
    int? statusId = this.deletedRow.StatusID;
    int num = 4;
    if (statusId.GetValueOrDefault() == num & statusId.HasValue || wikiDescriptor == null || !wikiDescriptor.DeletedID.HasValue)
      return;
    row.ParentUID = wikiDescriptor.DeletedID;
    row.StatusID = new int?(4);
    sender.SetStatus((object) row, PXEntryStatus.Notchanged);
    sender.Update((object) row);
    e.Cancel = true;
    this.Filter.Current.PrefetchProvider = new bool?(true);
    this.deletedRow = (WikiPage) null;
  }

  private void OnRowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    WikiPage row = (WikiPage) e.Row;
    Guid? wikiId = row.WikiID;
    Guid empty = Guid.Empty;
    if ((wikiId.HasValue ? (wikiId.HasValue ? (wikiId.GetValueOrDefault() != empty ? 1 : 0) : 0) : 1) != 0)
    {
      foreach (PXResult<Base> pxResult in PXSelectBase<Base, PXSelect<Base, PX.Data.Where<WikiPage.parentUID, Equal<Required<WikiPage.parentUID>>>>.Config>.Select((PXGraph) this, (object) row.PageID))
      {
        Base @base = (Base) pxResult;
        sender.Delete((object) @base);
      }
    }
    this.Filter.Current.PrefetchProvider = new bool?(true);
  }

  private bool HasAttachedFiles(WikiPage row)
  {
    return (WikiFileInPage) PXSelectBase<WikiFileInPage, PXSelect<WikiFileInPage, PX.Data.Where<WikiFileInPage.pageID, Equal<Required<WikiFileInPage.pageID>>>>.Config>.Select((PXGraph) this, (object) row.PageID) != null;
  }

  protected WikiRevision CreateRevision(WikiPage row, bool newRevision)
  {
    WikiPageLanguage wikiPageLanguage1 = this.ReadLanguage(row);
    if (wikiPageLanguage1 == null || wikiPageLanguage1.Language != row.Language)
    {
      wikiPageLanguage1 = new WikiPageLanguage();
      wikiPageLanguage1.PageID = row.PageID;
      wikiPageLanguage1.Language = row.Language;
      wikiPageLanguage1.LastRevisionID = new int?(0);
      wikiPageLanguage1.LastPublishedID = new int?(0);
      this.pageLanguages.Cache.Insert((object) wikiPageLanguage1);
    }
    WikiRevision revision = new WikiRevision();
    revision.PageID = row.PageID;
    revision.UID = new Guid?(Guid.NewGuid());
    revision.Language = row.Language;
    int? statusId = row.StatusID;
    int num1 = 3;
    if (statusId.GetValueOrDefault() == num1 & statusId.HasValue)
    {
      revision.ApprovalByID = new Guid?(this.Accessinfo.UserID);
      revision.ApprovalDateTime = new System.DateTime?(System.DateTime.Now);
    }
    revision.Content = row.Content ?? "";
    bool? isHtml = row.IsHtml;
    bool flag1 = true;
    if (!(isHtml.GetValueOrDefault() == flag1 & isHtml.HasValue))
    {
      PXWikiSettings pxWikiSettings = new PXWikiSettings((Page) null, PXGraph.CreateInstance<WikiReader>());
      PXTxtRenderer pxTxtRenderer = new PXTxtRenderer((PXWikiParserContext) pxWikiSettings.Absolute);
      PXDBContext context = new PXDBContext(pxWikiSettings.Absolute);
      context.Renderer = (PXRenderer) pxTxtRenderer;
      revision.PlainText = PXWikiParser.Parse(revision.Content, (PXWikiParserContext) context) ?? string.Empty;
    }
    else
      revision.PlainText = SearchService.Html2PlainText(row.Content) ?? string.Empty;
    WikiPage wikiPage1 = row;
    WikiRevision wikiRevision1 = revision;
    Guid? nullable1 = new Guid?(this.Accessinfo.UserID);
    Guid? nullable2 = nullable1;
    wikiRevision1.CreatedByID = nullable2;
    Guid? nullable3 = nullable1;
    wikiPage1.LastModifiedByID = nullable3;
    WikiPage wikiPage2 = row;
    WikiRevision wikiRevision2 = revision;
    System.DateTime? nullable4 = new System.DateTime?(System.DateTime.Now);
    System.DateTime? nullable5 = nullable4;
    wikiRevision2.CreatedDateTime = nullable5;
    System.DateTime? nullable6 = nullable4;
    wikiPage2.LastModifiedDateTime = nullable6;
    int? nullable7 = wikiPageLanguage1.LastRevisionID;
    int num2 = 0;
    int? nullable8;
    if (!(nullable7.GetValueOrDefault() == num2 & nullable7.HasValue))
    {
      nullable7 = wikiPageLanguage1.LastRevisionID;
      nullable8 = row.PageRevisionID;
      if (!(nullable7.GetValueOrDefault() == nullable8.GetValueOrDefault() & nullable7.HasValue == nullable8.HasValue))
      {
        nullable8 = row.PageRevisionID;
        int num3 = 0;
        if (!(nullable8.GetValueOrDefault() == num3 & nullable8.HasValue))
          goto label_10;
      }
      bool? versioned = row.Versioned;
      bool flag2 = true;
      if (!(versioned.GetValueOrDefault() == flag2 & versioned.HasValue))
      {
        newRevision = false;
        goto label_13;
      }
      goto label_13;
    }
label_10:
    newRevision = true;
label_13:
    if (newRevision)
    {
      WikiPageLanguage wikiPageLanguage2 = wikiPageLanguage1;
      nullable8 = wikiPageLanguage2.LastRevisionID;
      int? nullable9;
      if (!nullable8.HasValue)
      {
        nullable7 = new int?();
        nullable9 = nullable7;
      }
      else
        nullable9 = new int?(nullable8.GetValueOrDefault() + 1);
      wikiPageLanguage2.LastRevisionID = nullable9;
      revision.PageRevisionID = wikiPageLanguage1.LastRevisionID;
    }
    else
    {
      revision.PageRevisionID = wikiPageLanguage1.LastRevisionID;
      nullable8 = row.StatusID;
      int num4 = 3;
      if (!(nullable8.GetValueOrDefault() == num4 & nullable8.HasValue))
      {
        nullable8 = row.StatusID;
        int num5 = 4;
        if (!(nullable8.GetValueOrDefault() == num5 & nullable8.HasValue))
        {
          nullable8 = wikiPageLanguage1.LastRevisionID;
          nullable7 = wikiPageLanguage1.LastPublishedID;
          if (nullable8.GetValueOrDefault() == nullable7.GetValueOrDefault() & nullable8.HasValue == nullable7.HasValue)
          {
            WikiRevision wikiRevision3 = (WikiRevision) PXSelectBase<WikiRevision, PXSelect<WikiRevision, PX.Data.Where<WikiRevision.pageID, Equal<Required<WikiRevision.pageID>>, And<WikiRevision.language, Equal<Required<WikiRevision.language>>, And<WikiRevision.approvalByID, IsNotNull, And<WikiRevision.pageRevisionID, NotEqual<Required<WikiRevision.pageRevisionID>>>>>>, OrderBy<Desc<WikiRevision.pageRevisionID>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) wikiPageLanguage1.PageID, (object) wikiPageLanguage1.Language, (object) wikiPageLanguage1.LastPublishedID);
            int num6;
            if (wikiRevision3 == null)
            {
              num6 = 0;
            }
            else
            {
              nullable7 = wikiRevision3.PageRevisionID;
              num6 = nullable7.GetValueOrDefault();
            }
            int num7 = num6;
            foreach (WikiRevision wikiRevision4 in this.Revisions.Cache.Cached)
            {
              Guid? pageId = wikiRevision4.PageID;
              Guid? nullable10 = wikiPageLanguage1.PageID;
              if ((pageId.HasValue == nullable10.HasValue ? (pageId.HasValue ? (pageId.GetValueOrDefault() != nullable10.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0 && this.Revisions.Cache.GetStatus((object) wikiRevision4) != PXEntryStatus.Deleted)
              {
                nullable10 = wikiRevision4.ApprovalByID;
                if (nullable10.HasValue)
                {
                  nullable7 = wikiRevision4.PageRevisionID;
                  int num8 = num7;
                  if (nullable7.GetValueOrDefault() > num8 & nullable7.HasValue)
                  {
                    nullable7 = wikiRevision4.PageRevisionID;
                    nullable8 = wikiPageLanguage1.LastRevisionID;
                    if (!(nullable7.GetValueOrDefault() == nullable8.GetValueOrDefault() & nullable7.HasValue == nullable8.HasValue))
                    {
                      nullable8 = wikiRevision4.PageRevisionID;
                      num7 = nullable8.GetValueOrDefault();
                      wikiRevision3 = wikiRevision4;
                    }
                  }
                }
              }
            }
            wikiPageLanguage1.LastPublishedID = new int?(num7);
            wikiPageLanguage1.LastPublishedDateTime = (System.DateTime?) wikiRevision3?.CreatedDateTime;
          }
        }
      }
    }
    nullable8 = row.StatusID;
    int num9 = 3;
    if (nullable8.GetValueOrDefault() == num9 & nullable8.HasValue)
    {
      wikiPageLanguage1.LastPublishedID = revision.PageRevisionID;
      wikiPageLanguage1.LastPublishedDateTime = revision.CreatedDateTime;
    }
    wikiPageLanguage1.Title = row.Title;
    wikiPageLanguage1.Summary = row.Summary;
    wikiPageLanguage1.Keywords = row.Keywords;
    this.pageLanguages.Select((object) wikiPageLanguage1.PageID);
    this.pageLanguages.Cache.Update((object) wikiPageLanguage1);
    LinkToGuid linkToGuid = new LinkToGuid((PXGraph) this, revision, this.Pages.Current.WikiID);
    revision.Content = linkToGuid.Result;
    if (!newRevision)
    {
      foreach (PXResult<WikiPageLink> pxResult in this.pageLinks.Select((object) revision.PageID, (object) revision.Language, (object) revision.PageRevisionID))
        this.pageLinks.Delete((WikiPageLink) pxResult);
      foreach (PXResult<WikiFileInPage> pxResult in this.fileLinks.Select((object) revision.PageID, (object) revision.Language, (object) revision.PageRevisionID))
        this.fileLinks.Delete((WikiFileInPage) pxResult);
    }
    foreach (WikiPageLink pageLink in linkToGuid.PageLinks)
      this.pageLinks.Insert(pageLink);
    foreach (WikiFileInPage fileLink in linkToGuid.FileLinks)
      this.fileLinks.Insert(fileLink);
    return !newRevision ? this.Revisions.Update(revision) : this.Revisions.Insert(revision);
  }

  protected override bool IsReadOnly => false;
}
