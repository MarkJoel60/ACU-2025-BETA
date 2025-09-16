// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiMaintenance
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

#nullable disable
namespace PX.SM;

/// <exclude />
public class WikiMaintenance : 
  WikiPageGraph<WikiDescriptor, WikiDescriptor, Where<WikiDescriptor.articleType, Equal<WikiArticleType.wiki>>>,
  ICanAlterSiteMap
{
  public PXSave<WikiDescriptor> Save;
  public PXCancel<WikiDescriptor> Cancel;
  public PXInsert<WikiDescriptor> Insert;
  public PXDelete<WikiDescriptor> Delete;
  public PXFirst<WikiDescriptor> First;
  public PXPrevious<WikiDescriptor> Previous;
  public PXNext<WikiDescriptor> Next;
  public PXLast<WikiDescriptor> Last;
  public PXAction<WikiDescriptor> MoveCategoryUp;
  public PXAction<WikiDescriptor> MoveCategoryDown;
  public PXSelect<WikiDescriptor> Wikis;
  public PXSelect<WikiDescriptor, Where<WikiDescriptor.pageID, Equal<Current<WikiDescriptor.pageID>>>> CurrentWiki;
  public PXSelect<WikiPage, Where<WikiPage.pageID, Equal<Current<WikiDescriptor.pageID>>>> CurrentPage;
  public PXSelect<WikiTag, Where<WikiTag.wikiID, Equal<Current<WikiDescriptor.pageID>>>> Tags;
  public PXSelect<WikiReadLanguage> ReadLangs;
  public PXSelect<WikiSitePath> SitePaths;
  public PXSelect<WikiPageSimple, Where<WikiPageSimple.wikiID, Equal<Current<WikiDescriptor.pageID>>>> AllPages;
  public PXSelect<Role> EntityRoles;
  public PXSelect<PX.SM.Roles, Where<PX.SM.Roles.applicationName, Equal<Required<PX.SM.Roles.applicationName>>>> defRoles;
  public PXSelect<RolesInGraph, Where<RolesInGraph.screenID, Equal<Required<RolesInGraph.screenID>>, And<RolesInGraph.applicationName, Equal<Required<RolesInGraph.applicationName>>>>> defGraph;
  public PXSelect<RolesInCache, Where<RolesInCache.screenID, Equal<Required<RolesInCache.screenID>>, And<RolesInCache.cachetype, Equal<Required<RolesInCache.cachetype>>, And<RolesInCache.applicationName, Equal<Required<RolesInCache.applicationName>>>>>> defCache;
  public PXSelect<RolesInMember, Where<RolesInMember.screenID, Equal<Required<RolesInMember.screenID>>, And<RolesInMember.cachetype, Equal<Required<RolesInMember.cachetype>>, And<RolesInMember.membername, Equal<Required<RolesInMember.membername>>, And<RolesInMember.applicationName, Equal<Required<RolesInMember.applicationName>>>>>>> defMember;
  public PXSelect<RolesInGraph, Where<RolesInGraph.screenID, Equal<Required<RolesInGraph.screenID>>, And<RolesInGraph.rolename, Equal<Required<PX.SM.Roles.rolename>>>>> allGraph;
  public PXSelect<RolesInCache, Where<RolesInCache.screenID, Equal<Required<RolesInCache.screenID>>, And<RolesInCache.rolename, Equal<Required<PX.SM.Roles.rolename>>>>> allCache;
  public PXSelect<RolesInMember, Where<RolesInMember.screenID, Equal<Required<RolesInMember.screenID>>, And<RolesInMember.rolename, Equal<Required<PX.SM.Roles.rolename>>>>> allMember;
  public PXSelect<WikiAccessRights> WikiAccessRightsRecs;
  public PXSelectSiteMapTree<False, False, PX.Data.True, False, False> SiteMapTree;
  private PXBaseGenScreenToSiteMapAddHelper<WikiDescriptor> screenToSiteMapAddHelper;
  public PXAction<WikiDescriptor> ClearWiki;
  public PXFilter<ClearDateFilter> ClearingFilter;

  public WikiMaintenance()
  {
    Wiki.BlockIfOnlineHelpIsOn();
    this.Views.Caches.Add(typeof (WikiPageSimple));
    PXIntListAttribute.SetList<WikiDescriptor.spwikiArticleType>(this.Wikis.Cache, (object) null, new int[2]
    {
      10,
      14
    }, new string[2]{ "Article", "KB Article" });
    this.screenToSiteMapAddHelper = !PXSiteMap.IsPortal ? (PXBaseGenScreenToSiteMapAddHelper<WikiDescriptor>) new PXWikiScreenToSiteMapAddHelper("WI", "~/Search/Wiki.aspx", typeof (WikiDescriptor.sitemapParent), typeof (WikiDescriptor.sitemapTitle), typeof (WikiPage.title), typeof (WikiDescriptor.pageID), typeof (WikiDescriptor.pageID), "WikiID", false, this.Caches[typeof (WikiDescriptor)], this.SiteMapTree.Cache) : (PXBaseGenScreenToSiteMapAddHelper<WikiDescriptor>) new PXWikiScreenToPortalMapAddHelper("WI", "~/Search/Wiki.aspx", typeof (WikiDescriptor.sitemapParent), typeof (WikiDescriptor.sitemapTitle), typeof (WikiPage.title), typeof (WikiDescriptor.pageID), typeof (WikiDescriptor.pageID), "WikiID", false, this.Caches[typeof (WikiDescriptor)], this.SiteMapTree.Cache);
    this.Views.Caches.Remove(typeof (UsersInRoles));
    this.Views.Caches.Add(typeof (UsersInRoles));
  }

  protected IEnumerable readLangs()
  {
    WikiMaintenance graph = this;
    bool hasUpdated = false;
    foreach (PXResult<Locale, WikiReadLanguage> pxResult in PXSelectBase<Locale, PXSelectJoin<Locale, LeftJoin<WikiReadLanguage, On<Locale.localeName, Equal<WikiReadLanguage.localeID>, And<WikiReadLanguage.wikiID, Equal<Current<WikiDescriptor.pageID>>>>>>.Config>.Select((PXGraph) graph))
    {
      Locale locale = (Locale) pxResult;
      WikiReadLanguage wikiReadLanguage1 = (WikiReadLanguage) pxResult;
      wikiReadLanguage1.Selected = new bool?(wikiReadLanguage1.LocaleID != null);
      wikiReadLanguage1.LocaleID = locale.LocaleName;
      wikiReadLanguage1.WikiID = graph.Wikis.Current.PageID;
      wikiReadLanguage1.Language = CultureInfo.GetCultureInfo(locale.LocaleName).EnglishName;
      if (!(graph.ReadLangs.Cache.Locate((object) wikiReadLanguage1) is WikiReadLanguage data))
      {
        WikiReadLanguage wikiReadLanguage2 = graph.ReadLangs.Cache.Insert((object) wikiReadLanguage1) as WikiReadLanguage;
        graph.ReadLangs.Cache.SetStatus((object) wikiReadLanguage2, PXEntryStatus.Held);
        data = wikiReadLanguage2;
      }
      hasUpdated = graph.ReadLangs.Cache.GetStatus((object) data) == PXEntryStatus.Updated | hasUpdated;
      if (string.Compare(data.LocaleID, "en-us", true) == 0)
      {
        data.Selected = new bool?(true);
        PXUIFieldAttribute.SetEnabled<WikiReadLanguage.selected>(graph.ReadLangs.Cache, (object) data, false);
      }
      yield return (object) data;
    }
    if (!hasUpdated)
      graph.ReadLangs.Cache.IsDirty = false;
  }

  protected IEnumerable allPages(string PageID)
  {
    WikiMaintenance graph = this;
    if (graph.Pages.Current != null)
    {
      WikiPage current = (WikiPage) graph.Pages.Current;
      Guid? nullable = GUID.CreateGuid(PageID);
      Guid guid = nullable ?? current.PageID.Value;
      PXResultset<WikiPageSimple> pxResultset;
      if (!string.IsNullOrEmpty(PageID))
        pxResultset = PXSelectBase<WikiPageSimple, PXSelectJoin<WikiPageSimple, LeftJoin<WikiPageLanguage, On<WikiPageSimple.pageID, Equal<WikiPageLanguage.pageID>, And<WikiPageLanguage.language, Equal<Required<WikiPageLanguage.language>>>>>, Where<WikiPageSimple.parentUID, Equal<Required<WikiPageSimple.parentUID>>>>.Config>.Select((PXGraph) graph, (object) Thread.CurrentThread.CurrentCulture.Name, (object) guid);
      else
        pxResultset = PXSelectBase<WikiPageSimple, PXSelectJoin<WikiPageSimple, LeftJoin<WikiPageLanguage, On<WikiPageSimple.pageID, Equal<WikiPageLanguage.pageID>, And<WikiPageLanguage.language, Equal<Required<WikiPageLanguage.language>>>>>, Where<WikiPageSimple.pageID, Equal<Required<WikiPageSimple.pageID>>>>.Config>.Select((PXGraph) graph, (object) Thread.CurrentThread.CurrentCulture.Name, (object) current.PageID);
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
          yield return (object) wikiPageSimple;
      }
    }
  }

  internal IEnumerable Entityroles()
  {
    WikiMaintenance wikiMaintenance = this;
    wikiMaintenance.EntityRoles.Cache.Clear();
    List<Role> roleList = new List<Role>();
    PXSiteMapProvider provider = PXSiteMap.Provider;
    Guid? pageId = wikiMaintenance.Pages.Current.PageID;
    Guid key = pageId.Value;
    PXSiteMapNode sitemap = provider.FindSiteMapNodeFromKeyUnsecure(key);
    if (sitemap != null && !string.IsNullOrEmpty(sitemap.ScreenID))
    {
      foreach (PXResult<PX.SM.Roles> pxResult in wikiMaintenance.defRoles["/"])
      {
        PX.SM.Roles roles = (PX.SM.Roles) pxResult;
        Role instance1 = wikiMaintenance.EntityRoles.Cache.CreateInstance() as Role;
        instance1.ScreenID = sitemap.ScreenID;
        instance1.RoleName = roles.Rolename;
        instance1.RoleDescr = roles.Descr;
        instance1.Guest = roles.Guest;
        instance1.RoleRight = new int?(-1);
        if (wikiMaintenance.Caches[typeof (WikiAccessRights)].CreateInstance() is WikiAccessRights instance2)
        {
          instance2.ApplicationName = "/";
          instance2.PageID = new Guid?(sitemap.NodeID);
          instance2.RoleName = roles.Rolename;
        }
        if (wikiMaintenance.Caches[typeof (WikiAccessRights)].Locate((object) instance2) is WikiAccessRights wikiAccessRights)
        {
          Role role = instance1;
          short? accessRights = wikiAccessRights.AccessRights;
          int? nullable = accessRights.HasValue ? new int?((int) accessRights.GetValueOrDefault()) : new int?();
          role.RoleRight = nullable;
        }
        else
        {
          Role role = instance1;
          PXWikiProvider wikiProvider = PXSiteMap.WikiProvider;
          pageId = wikiMaintenance.Pages.Current.PageID;
          Guid pageID = pageId.Value;
          string rolename = roles.Rolename;
          int? nullable = new int?((int) wikiProvider.GetAccessRightsUnsecure(pageID, rolename));
          role.RoleRight = nullable;
        }
        Role role1 = wikiMaintenance.EntityRoles.Insert(instance1);
        if (role1 != null && PXAccess.IsRoleEnabled(role1.RoleName))
          yield return (object) role1;
      }
    }
    wikiMaintenance.EntityRoles.Cache.IsDirty = false;
  }

  [PXDBGuid(false)]
  [PXDefault]
  [PXUIField]
  protected virtual void WikiDescriptor_PageID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(InputMask = "", IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "ID", Visibility = PXUIVisibility.SelectorVisible)]
  [PXWikiSelector(typeof (WikiDescriptor.name), CheckRights = false)]
  protected virtual void WikiDescriptor_Name_CacheAttached(PXCache sender)
  {
  }

  public bool IsSiteMapAltered { get; protected set; }

  protected void WikiDescriptor_DeletedID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) Guid.NewGuid();
  }

  protected void WikiDescriptor_WikiID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) Guid.Empty;
  }

  protected void WikiDescriptor_PageID_FieldDefaulting(PXCache cache, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) Guid.NewGuid();
  }

  protected void WikiDescriptor_Name_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  protected void WikiDescriptor_SitemapParent_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    this.screenToSiteMapAddHelper.UpdateSiteMapParent(e.Row as WikiDescriptor);
  }

  protected virtual void WikiDescriptor_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    foreach (PXResult<WikiReadLanguage> pxResult in this.ReadLangs.Select())
    {
      WikiReadLanguage wikiReadLanguage = (WikiReadLanguage) pxResult;
      wikiReadLanguage.Selected = new bool?(true);
      this.ReadLangs.Cache.Update((object) wikiReadLanguage);
    }
    this.ReadLangs.Cache.IsDirty = false;
  }

  protected void WikiDescriptor_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    int? rowCount = PXSelectBase<WikiPageSimple, PXSelectGroupBy<WikiPageSimple, Where<WikiPageSimple.wikiID, Equal<Required<WikiPageSimple.wikiID>>, And<WikiPageSimple.name, NotEqual<Required<WikiPageSimple.name>>>>, Aggregate<Count>>.Config>.Select((PXGraph) this, (object) ((WikiPage) e.Row).PageID, (object) "Deleted").RowCount;
    int num = 0;
    if (rowCount.GetValueOrDefault() > num & rowCount.HasValue)
    {
      e.Cancel = true;
      throw new PXException("The wiki cannot be deleted. It contains undeleted articles or the recycle bin is not empty.");
    }
    foreach (PXResult<WikiTag> pxResult in this.Tags.Select())
      this.Tags.Delete((WikiTag) pxResult);
  }

  protected void WikiSitePath_PageName_FieldSelecting(PXCache cache, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    WikiSitePath row = (WikiSitePath) e.Row;
    Guid? pageId = row.PageID;
    if (!pageId.HasValue)
      return;
    PXWikiProvider wikiProvider = PXSiteMap.WikiProvider;
    pageId = row.PageID;
    Guid key = pageId.Value;
    if (!(wikiProvider.FindSiteMapNodeFromKey(key) is PXWikiMapNode siteMapNodeFromKey))
      return;
    e.ReturnValue = (object) siteMapNodeFromKey.Name;
  }

  protected void WikiSitePath_PageName_FieldUpdating(PXCache cache, PXFieldUpdatingEventArgs e)
  {
    WikiSitePath row = (WikiSitePath) e.Row;
    string newValue = e.NewValue as string;
    if (string.IsNullOrEmpty(newValue))
      return;
    PXSiteMapNode siteMapNode = (PXSiteMapNode) PXSiteMap.WikiProvider.FindSiteMapNode(this.Pages.Current.Name, newValue);
    if (siteMapNode == null)
      return;
    row.PageID = new Guid?(siteMapNode.NodeID);
  }

  protected void WikiDescriptor_ApprovalUserID_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null)
      return;
    if ((Users) PXSelectBase<Users, PXSelect<Users, Where<Users.pKID, Equal<Required<Users.pKID>>>>.Config>.Select((PXGraph) this, (object) GUID.CreateGuid(e.NewValue as string)) != null)
      return;
    e.NewValue = (object) null;
  }

  protected override void OnRowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    WikiDescriptor newRow = (WikiDescriptor) e.NewRow;
    if (!((WikiPage) e.Row).ApprovalGroupID.HasValue && newRow.ApprovalUserID.HasValue)
      this.isAppvoalUpdConf = true;
    base.OnRowUpdating(sender, e);
  }

  protected override void OnRowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    WikiDescriptor row = (WikiDescriptor) e.Row;
    base.OnRowUpdated(sender, e);
    if (!this.isAppvoalUpdConf)
      return;
    this.UpdateApproval(row.PageID, (WikiPage) row);
  }

  private void UpdateApproval(Guid? parentID, WikiPage row)
  {
    PXCache cach = this.Caches[typeof (WikiPageSimple)];
    foreach (PXResult<WikiPageSimple> pxResult in PXSelectBase<WikiPageSimple, PXSelect<WikiPageSimple, Where<WikiPageSimple.parentUID, Equal<Required<WikiPageSimple.parentUID>>>>.Config>.Select((PXGraph) this, (object) parentID))
    {
      WikiPageSimple wikiPageSimple = (WikiPageSimple) pxResult;
      WikiPageSimple copy = (WikiPageSimple) cach.CreateCopy((object) wikiPageSimple);
      PXSiteMapNode siteMapNodeFromKey = PXSiteMap.WikiProvider.FindSiteMapNodeFromKey(copy.PageID.GetValueOrDefault());
      copy.Title = siteMapNodeFromKey == null ? copy.Name : siteMapNodeFromKey.Title;
      if (this.isAppvoalUpdConf)
      {
        copy.ApprovalGroupID = row.ApprovalGroupID;
        copy.ApprovalUserID = row.ApprovalUserID;
      }
      cach.Update((object) copy);
      this.UpdateApproval(wikiPageSimple.PageID, row);
    }
  }

  protected void WikiTag_RowPersisting(PXCache cahce, PXRowPersistingEventArgs e)
  {
    WikiTag row = (WikiTag) e.Row;
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Delete)
      return;
    PXDatabase.Delete<WikiRevisionTag>(new PXDataFieldRestrict(typeof (WikiRevisionTag.wikiID).Name, PXDbType.UniqueIdentifier, (object) row.WikiID), new PXDataFieldRestrict(typeof (WikiRevisionTag.tagID).Name, PXDbType.Int, (object) row.TagID));
  }

  protected void WikiReadLanguage_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    WikiReadLanguage row = (WikiReadLanguage) e.Row;
    WikiReadLanguage wikiReadLanguage = (WikiReadLanguage) PXSelectBase<WikiReadLanguage, PXSelect<WikiReadLanguage, Where<WikiReadLanguage.localeID, Equal<Required<WikiReadLanguage.localeID>>, And<WikiReadLanguage.wikiID, Equal<Required<WikiReadLanguage.wikiID>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) row.LocaleID, (object) row.WikiID);
    bool? selected1 = row.Selected;
    bool flag1 = true;
    if (selected1.GetValueOrDefault() == flag1 & selected1.HasValue && wikiReadLanguage == null)
    {
      PXDatabase.Insert(typeof (WikiReadLanguage), new PXDataFieldAssign(typeof (WikiReadLanguage.localeID).Name, (object) row.LocaleID), new PXDataFieldAssign(typeof (WikiReadLanguage.wikiID).Name, (object) row.WikiID));
    }
    else
    {
      bool? selected2 = row.Selected;
      bool flag2 = false;
      if (selected2.GetValueOrDefault() == flag2 & selected2.HasValue)
        PXDatabase.Delete(typeof (WikiReadLanguage), new PXDataFieldRestrict(typeof (WikiReadLanguage.localeID).Name, (object) row.LocaleID), new PXDataFieldRestrict(typeof (WikiReadLanguage.wikiID).Name, (object) row.WikiID));
    }
    e.Cancel = true;
  }

  protected virtual void WikiDescriptor_RowPersisting(PXCache cahce, PXRowPersistingEventArgs e)
  {
    WikiDescriptor row1 = (WikiDescriptor) e.Row;
    PXDBOperation pxdbOperation = e.Operation & PXDBOperation.Delete;
    System.Type graphType = this.GetGraphType(row1.WikiArticleType);
    IWikiMaintGraph graph1 = !(graphType == (System.Type) null) ? PXGraph.CreateInstance(graphType) as IWikiMaintGraph : throw new PXException("A graph type cannot be determined from the specified article type.");
    int? nullable1;
    if (pxdbOperation == PXDBOperation.Insert || pxdbOperation == PXDBOperation.Update)
    {
      nullable1 = row1.WikiArticleType;
      if (nullable1.HasValue)
      {
        nullable1 = row1.WikiArticleType;
        string articleUrlEdit = this.GetArticleUrlEdit(nullable1.Value);
        if (articleUrlEdit != null)
          row1.UrlEdit = articleUrlEdit;
      }
      PXCache pxCache;
      if (this.Caches.TryGetValue(typeof (WikiPageSimple), out pxCache))
        pxCache.Persist(PXDBOperation.Update);
    }
    row1.ParentUID = new Guid?(Guid.Empty);
    this.Filter.Current.PrefetchProvider = new bool?(true);
    this.Filter.Current.PrefetchSiteMap = new bool?(true);
    switch (pxdbOperation)
    {
      case PXDBOperation.Update:
        graph1?.OnWikiUpdated(row1);
        PXGraph graph2 = new PXGraph();
        graph2.SelectTimeStamp();
        if (Thread.CurrentThread.CurrentCulture.Name != "en-US")
        {
          WikiPageLanguage wpl = (WikiPageLanguage) PXSelectBase<WikiPageLanguage, PXSelect<WikiPageLanguage, Where<WikiPageLanguage.pageID, Equal<Required<WikiPageLanguage.pageID>>, And<WikiPageLanguage.language, Equal<Required<WikiPageLanguage.language>>>>>.Config>.Select(graph2, (object) row1.PageID, (object) Thread.CurrentThread.CurrentCulture.Name);
          if (wpl == null)
          {
            WikiPageLanguage wikiPageLanguage = this.CreateWikiPageLanguage(row1, Thread.CurrentThread.CurrentCulture.Name);
            if (graph2.Caches[typeof (WikiPageLanguage)].Insert((object) wikiPageLanguage) is WikiPageLanguage row2)
            {
              graph2.Caches[typeof (WikiPageLanguage)].PersistInserted((object) row2);
              break;
            }
            break;
          }
          WikiPageLanguage wikiPageLanguage1 = this.UpdateWikiPageLanguage(wpl, row1);
          if (graph2.Caches[typeof (WikiPageLanguage)].Update((object) wikiPageLanguage1) is WikiPageLanguage row3)
          {
            graph2.Caches[typeof (WikiPageLanguage)].PersistUpdated((object) row3);
            break;
          }
          break;
        }
        WikiPageLanguage wpl1 = (WikiPageLanguage) PXSelectBase<WikiPageLanguage, PXSelect<WikiPageLanguage, Where<WikiPageLanguage.pageID, Equal<Required<WikiPageLanguage.pageID>>, And<WikiPageLanguage.language, Equal<Required<WikiPageLanguage.language>>>>>.Config>.Select(graph2, (object) row1.PageID, (object) "en-US");
        if (wpl1 == null)
        {
          WikiPageLanguage wikiPageLanguage = this.CreateWikiPageLanguage(row1, "en-US");
          if (graph2.Caches[typeof (WikiPageLanguage)].Insert((object) wikiPageLanguage) is WikiPageLanguage row4)
          {
            graph2.Caches[typeof (WikiPageLanguage)].PersistInserted((object) row4);
            break;
          }
          break;
        }
        WikiPageLanguage wikiPageLanguage2 = this.UpdateWikiPageLanguage(wpl1, row1);
        if (graph2.Caches[typeof (WikiPageLanguage)].Update((object) wikiPageLanguage2) is WikiPageLanguage row5)
        {
          graph2.Caches[typeof (WikiPageLanguage)].PersistUpdated((object) row5);
          break;
        }
        break;
      case PXDBOperation.Insert:
        WikiPage wikiPage1 = new WikiPage();
        wikiPage1.PageID = row1.DeletedID;
        wikiPage1.WikiID = row1.PageID;
        wikiPage1.ParentUID = row1.PageID;
        wikiPage1.Name = "Deleted";
        wikiPage1.Title = "Deleted";
        wikiPage1.Folder = new bool?(true);
        wikiPage1.StatusID = new int?(4);
        wikiPage1.Number = new double?(0.0);
        wikiPage1.ArticleType = new int?(1);
        wikiPage1.Versioned = new bool?(false);
        PXGraph pxGraph1 = new PXGraph();
        pxGraph1.SelectTimeStamp();
        if (pxGraph1.Caches[typeof (WikiPage)].Insert((object) wikiPage1) is WikiPage row6)
          pxGraph1.Caches[typeof (WikiPage)].PersistInserted((object) row6);
        WikiPageLanguage wikiPageLanguage3 = new WikiPageLanguage();
        wikiPageLanguage3.PageID = row1.DeletedID;
        wikiPageLanguage3.Language = "en-US";
        wikiPageLanguage3.Title = "Deleted Items";
        wikiPageLanguage3.LastPublishedDateTime = new System.DateTime?(System.DateTime.Now);
        WikiPageLanguage wikiPageLanguage4 = wikiPageLanguage3;
        WikiPageLanguage wikiPageLanguage5 = wikiPageLanguage3;
        nullable1 = new int?(1);
        int? nullable2 = nullable1;
        wikiPageLanguage5.LastRevisionID = nullable2;
        int? nullable3 = nullable1;
        wikiPageLanguage4.LastPublishedID = nullable3;
        if (pxGraph1.Caches[typeof (WikiPageLanguage)].Insert((object) wikiPageLanguage3) is WikiPageLanguage row7)
          pxGraph1.Caches[typeof (WikiPageLanguage)].PersistInserted((object) row7);
        if (Thread.CurrentThread.CurrentCulture.Name != "en-US")
        {
          WikiPageLanguage wikiPageLanguage6 = new WikiPageLanguage();
          wikiPageLanguage6.PageID = row1.DeletedID;
          wikiPageLanguage6.Language = Thread.CurrentThread.CurrentCulture.Name;
          wikiPageLanguage6.Title = PXMessages.LocalizeNoPrefix("Deleted Items");
          wikiPageLanguage6.LastPublishedDateTime = new System.DateTime?(System.DateTime.Now);
          WikiPageLanguage wikiPageLanguage7 = wikiPageLanguage6;
          WikiPageLanguage wikiPageLanguage8 = wikiPageLanguage6;
          nullable1 = new int?(1);
          int? nullable4 = nullable1;
          wikiPageLanguage8.LastRevisionID = nullable4;
          int? nullable5 = nullable1;
          wikiPageLanguage7.LastPublishedID = nullable5;
          if (pxGraph1.Caches[typeof (WikiPageLanguage)].Insert((object) wikiPageLanguage6) is WikiPageLanguage row8)
            pxGraph1.Caches[typeof (WikiPageLanguage)].PersistInserted((object) row8);
        }
        if (Thread.CurrentThread.CurrentCulture.Name != "en-US")
        {
          WikiPageLanguage wikiPageLanguage9 = this.CreateWikiPageLanguage(row1, Thread.CurrentThread.CurrentCulture.Name);
          if (pxGraph1.Caches[typeof (WikiPageLanguage)].Insert((object) wikiPageLanguage9) is WikiPageLanguage row9)
            pxGraph1.Caches[typeof (WikiPageLanguage)].PersistInserted((object) row9);
        }
        else
        {
          WikiPageLanguage wikiPageLanguage10 = this.CreateWikiPageLanguage(row1, "en-US");
          if (pxGraph1.Caches[typeof (WikiPageLanguage)].Insert((object) wikiPageLanguage10) is WikiPageLanguage row10)
            pxGraph1.Caches[typeof (WikiPageLanguage)].PersistInserted((object) row10);
        }
        foreach (PXResult<PX.SM.Roles> pxResult in this.Roles["/"])
        {
          PX.SM.Roles roles = (PX.SM.Roles) pxResult;
          PXCacheRights rights = PXCacheRights.Denied;
          string screenID = PXContext.GetScreenID().Replace(".", "");
          if (!string.IsNullOrEmpty(screenID))
            rights = PXAccess.Provider.GetRights(roles.Rolename, screenID);
          this.PageAccessRights.Insert(new WikiAccessRights()
          {
            PageID = row1.PageID,
            ApplicationName = roles.ApplicationName,
            RoleName = roles.Rolename,
            AccessRights = new short?((short) Wiki.Convert(rights))
          });
        }
        if (graph1 != null)
        {
          graph1.OnWikiCreated(row1);
          break;
        }
        break;
      case PXDBOperation.Delete:
        PXGraph pxGraph2 = (PXGraph) graph1;
        System.Type type1 = graph1.PageType.GetNestedType(typeof (WikiPage.parentUID).Name);
        if ((object) type1 == null)
          type1 = typeof (WikiPage.parentUID);
        System.Type type2 = type1;
        BqlCommand instance = BqlCommand.CreateInstance(BqlCommand.Compose(typeof (PX.Data.Select<,>), graph1.PageType, typeof (Where<,>), type2, typeof (Equal<>), typeof (Required<>), type2));
        PXView pxView1 = new PXView((PXGraph) graph1, true, instance);
        PXView pxView2 = pxView1;
        object[] objArray1 = new object[1]
        {
          (object) row1.PageID
        };
        foreach (object obj in pxView2.SelectMultiBound((object[]) null, objArray1))
          pxGraph2.Caches[graph1.PageType].Delete(obj);
        PXView pxView3 = pxView1;
        object[] objArray2 = new object[1]
        {
          (object) row1.DeletedID
        };
        foreach (object obj in pxView3.SelectMultiBound((object[]) null, objArray2))
          pxGraph2.Caches[graph1.PageType].Delete(obj);
        pxGraph2.Actions.PressSave();
        PXGraph graph3 = new PXGraph();
        WikiPage wikiPage2 = (WikiPage) PXSelectBase<WikiPage, PXSelect<WikiPage, Where<WikiPage.pageID, Equal<Required<WikiPage.pageID>>>>.Config>.Select(graph3, (object) row1.DeletedID);
        if (wikiPage2 != null)
        {
          graph3.Caches[typeof (WikiPage)].Delete((object) wikiPage2);
          graph3.Caches[typeof (WikiPage)].Persist(PXDBOperation.Delete);
        }
        foreach (PXResult<WikiPageLanguage> pxResult in PXSelectBase<WikiPageLanguage, PXSelect<WikiPageLanguage, Where<WikiPageLanguage.pageID, Equal<Required<WikiPage.pageID>>>>.Config>.Select(graph3, (object) row1.DeletedID))
        {
          WikiPageLanguage wikiPageLanguage11 = (WikiPageLanguage) pxResult;
          graph3.Caches[typeof (WikiPageLanguage)].Delete((object) wikiPageLanguage11);
          graph3.Caches[typeof (WikiPageLanguage)].Persist(PXDBOperation.Delete);
        }
        PXDatabase.Delete<WikiReadLanguage>(new PXDataFieldRestrict(typeof (WikiReadLanguage.wikiID).Name, (object) row1.PageID));
        if (graph1 != null)
        {
          graph1.OnWikiDeleted(row1);
          break;
        }
        break;
    }
    bool flag = this.screenToSiteMapAddHelper.DeleteObsoleteSiteMapNode(e.Row as WikiDescriptor, e.Operation);
    this.IsSiteMapAltered = flag ? flag : this.IsSiteMapAltered;
  }

  protected virtual void WikiDescriptor_RowSelecting(PXCache cache, PXRowSelectingEventArgs e)
  {
    this.screenToSiteMapAddHelper.InitializeSiteMapFields(e.Row as WikiDescriptor);
  }

  protected virtual string GetSearchUrl(int? wikiArticleType, Guid? wikiId)
  {
    return Wiki.SearchUrl(wikiId);
  }

  protected virtual string GetSiteMapIcon(int wikiArticleType)
  {
    return Wiki.GetSiteMapIcon(wikiArticleType);
  }

  protected virtual string GetArticleUrlEdit(int wikiArticleType)
  {
    switch (wikiArticleType)
    {
      case 10:
        return "~/Wiki/ArticleEdit.aspx";
      case 11:
        return "~/Wiki/AnnouncementEdit.aspx";
      case 12:
        return "~/Wiki/NotificationEdit.aspx";
      case 13:
        return "~/Wiki/SitePageEdit.aspx";
      default:
        return (string) null;
    }
  }

  protected virtual WikiPageLanguage CreateWikiPageLanguage(WikiDescriptor rec, string locale)
  {
    WikiPageLanguage wikiPageLanguage = new WikiPageLanguage();
    wikiPageLanguage.PageID = rec.PageID;
    wikiPageLanguage.Language = locale;
    wikiPageLanguage.Title = rec.WikiTitle;
    wikiPageLanguage.Summary = rec.WikiDescription;
    wikiPageLanguage.LastPublishedDateTime = new System.DateTime?(System.DateTime.Now);
    int? nullable = new int?(1);
    wikiPageLanguage.LastRevisionID = nullable;
    wikiPageLanguage.LastPublishedID = nullable;
    return wikiPageLanguage;
  }

  protected virtual WikiPageLanguage UpdateWikiPageLanguage(
    WikiPageLanguage wpl,
    WikiDescriptor rec)
  {
    wpl.Title = rec.WikiTitle;
    wpl.Summary = rec.WikiDescription;
    return wpl;
  }

  protected virtual System.Type GetGraphType(int? wikiArticleType)
  {
    return Wiki.GraphType(wikiArticleType);
  }

  protected override void OnPageSelected(PXCache sender, WikiPage row, PXWikiRights accessRights)
  {
    bool allowInsert = sender.AllowInsert;
    bool allowUpdate = sender.AllowUpdate;
    bool allowDelete = sender.AllowDelete;
    bool allowSelect = sender.AllowSelect;
    if (this.Pages.Cache.AllowDelete)
      accessRights = PXWikiRights.Delete;
    else if (this.Pages.Cache.AllowUpdate)
      accessRights = PXWikiRights.Update;
    else if (this.Pages.Cache.AllowInsert)
      accessRights = PXWikiRights.Insert;
    else if (this.Pages.Cache.AllowSelect)
      accessRights = PXWikiRights.Select;
    base.OnPageSelected(sender, row, accessRights);
    sender.AllowSelect = allowSelect;
    sender.AllowUpdate = allowUpdate;
    sender.AllowInsert = allowInsert;
    sender.AllowDelete = allowDelete;
    if (sender.GetStatus((object) row) != PXEntryStatus.Inserted)
      PXUIFieldAttribute.SetEnabled<WikiDescriptor.wikiArticleType>(sender, (object) row, false);
    if (row.Title != null)
      return;
    SiteMap siteMapRecord = Wiki.GetSiteMapRecord((PXGraph) this, row.PageID);
    if (siteMapRecord == null || string.IsNullOrEmpty(siteMapRecord.Title))
      return;
    row.Title = siteMapRecord.Title;
  }

  [PXButton(Tooltip = "Delete old revisions of wiki articles.")]
  [PXUIField(DisplayName = "Clear Wiki", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  internal IEnumerable clearWiki(PXAdapter adapter)
  {
    ClearDateFilter current = this.ClearingFilter.Current;
    if (this.ClearingFilter.AskExt() == WebDialogResult.OK)
      PXLongOperation.StartOperation(this.UID, (PXToggleAsyncDelegate) (() => WikiMaintenance.DoClearWiki(this.ClearingFilter.Current.Till)));
    return adapter.Get();
  }

  public static void DoClearWiki(System.DateTime? till)
  {
    if (!till.HasValue)
      return;
    PXGraph graph = new PXGraph();
    Guid empty = Guid.Empty;
    object[] objArray = new object[1]{ (object) till };
    foreach (PXResult<WikiPage, WikiPageLanguage, WikiRevision, WikiRevisionTagGrouped> pxResult in PXSelectBase<WikiPage, PXSelectJoin<WikiPage, InnerJoin<WikiPageLanguage, On<WikiPage.pageID, Equal<WikiPageLanguage.pageID>>, InnerJoin<WikiRevision, On<WikiPage.pageID, Equal<WikiRevision.pageID>, And<WikiPageLanguage.lastRevisionID, NotEqual<WikiRevision.pageRevisionID>, And<WikiPageLanguage.lastPublishedID, NotEqual<WikiRevision.pageRevisionID>>>>, LeftJoin<WikiRevisionTagGrouped, On<WikiRevision.pageID, Equal<WikiRevisionTagGrouped.pageID>, And<WikiRevision.pageRevisionID, Equal<WikiRevisionTagGrouped.pageRevisionID>>>>>>, Where<WikiRevisionTagGrouped.pageRevisionID, IsNull, And<WikiRevision.createdDateTime, Less<Required<WikiRevision.createdDateTime>>>>>.Config>.Select(graph, objArray))
    {
      WikiRevision wikiRevision = (WikiRevision) pxResult;
      PXDatabase.Delete<WikiRevision>(new PXDataFieldRestrict(typeof (WikiRevision.pageID).Name, (object) wikiRevision.PageID), new PXDataFieldRestrict(typeof (WikiRevision.language).Name, (object) wikiRevision.Language), new PXDataFieldRestrict(typeof (WikiRevision.pageRevisionID).Name, (object) wikiRevision.PageRevisionID));
    }
  }

  [PXButton(ImageKey = "Process", Tooltip = "Process tag for all subarticles.")]
  [PXUIField(DisplayName = "Process Tag", MapEnableRights = PXCacheRights.Update)]
  protected override IEnumerable processTag(PXAdapter adapter)
  {
    if (this.Tags.Current != null)
    {
      int? tagId = this.Tags.Current.TagID;
      if (tagId.HasValue && this.Pages.Current != null)
      {
        Guid? pageId = this.Pages.Current.PageID;
        if (pageId.HasValue)
        {
          pageId = this.Pages.Current.PageID;
          Guid pageID = pageId.Value;
          string language = this.Pages.Current.Language;
          tagId = this.Tags.Current.TagID;
          int tagID = tagId.Value;
          this.DoProcessTag(pageID, language, tagID);
        }
      }
    }
    return adapter.Get();
  }

  protected virtual void Role_RoleRight_FieldSelecting(PXCache cache, PXFieldSelectingEventArgs e)
  {
    Role row = e.Row as Role;
    List<int> intList = new List<int>();
    List<string> stringList = new List<string>();
    if (row == null)
      return;
    intList.Add(-1);
    intList.Add(0);
    intList.Add(1);
    intList.Add(2);
    intList.Add(3);
    intList.Add(4);
    intList.Add(5);
    stringList.Add(PXLocalizer.Localize("Revoked", typeof (RightMessages).FullName));
    stringList.Add(PXLocalizer.Localize("View Only", typeof (RightMessages).FullName));
    stringList.Add(PXLocalizer.Localize("Edit", typeof (RightMessages).FullName));
    stringList.Add(PXLocalizer.Localize("Insert", typeof (RightMessages).FullName));
    stringList.Add(PXLocalizer.Localize("Publish", typeof (RightMessages).FullName));
    stringList.Add(PXLocalizer.Localize("Delete", typeof (RightMessages).FullName));
    e.ReturnState = (object) PXIntState.CreateInstance(e.ReturnState, typeof (Role.roleRight).Name, new bool?(false), new int?(1), new int?(), new int?(), intList.ToArray(), stringList.ToArray(), (System.Type) null, new int?());
  }

  protected virtual void Role_RoleRight_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    int? newValue = (int?) e.NewValue;
    int num1 = 0;
    if (!(newValue.GetValueOrDefault() >= num1 & newValue.HasValue))
      return;
    Role row = e.Row as Role;
    bool flag = true;
    if (!string.IsNullOrEmpty(row.MemberName))
    {
      flag = false;
      foreach (PXResult<RolesInCache> pxResult in this.defCache.Select((object) row.ScreenID, (object) row.CacheName, (object) "/"))
      {
        RolesInCache rolesInCache = (RolesInCache) pxResult;
        if (rolesInCache.Rolename == row.RoleName)
        {
          short? accessrights = rolesInCache.Accessrights;
          int? nullable = accessrights.HasValue ? new int?((int) accessrights.GetValueOrDefault()) : new int?();
          int num2 = 0;
          if (nullable.GetValueOrDefault() >= num2 & nullable.HasValue)
          {
            flag = true;
            break;
          }
        }
      }
    }
    else if (!string.IsNullOrEmpty(row.CacheName))
    {
      flag = false;
      foreach (PXResult<RolesInGraph> pxResult in this.defGraph.Select((object) row.ScreenID, (object) "/"))
      {
        RolesInGraph rolesInGraph = (RolesInGraph) pxResult;
        if (rolesInGraph.Rolename == row.RoleName)
        {
          short? accessrights = rolesInGraph.Accessrights;
          int? nullable = accessrights.HasValue ? new int?((int) accessrights.GetValueOrDefault()) : new int?();
          int num3 = 0;
          if (nullable.GetValueOrDefault() >= num3 & nullable.HasValue)
          {
            flag = true;
            break;
          }
        }
      }
    }
    if (!flag)
      throw new PXException("Explicitly define upper-level rights first.");
  }

  internal void Role_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is Role row))
      return;
    int? nullable1;
    int? nullable2;
    if (string.IsNullOrEmpty(row.CacheName) && string.IsNullOrEmpty(row.MemberName))
    {
      RolesInGraph rolesInGraph1 = new RolesInGraph();
      rolesInGraph1.ApplicationName = "/";
      rolesInGraph1.ScreenID = row.ScreenID;
      rolesInGraph1.Rolename = row.RoleName;
      RolesInGraph rolesInGraph2 = rolesInGraph1;
      int? roleRight1 = row.RoleRight;
      short? nullable3 = roleRight1.HasValue ? new short?((short) roleRight1.GetValueOrDefault()) : new short?();
      rolesInGraph2.Accessrights = nullable3;
      rolesInGraph1.CreatedByID = row.CreatedByID;
      rolesInGraph1.CreatedByScreenID = row.CreatedByScreenID;
      rolesInGraph1.CreatedDateTime = row.CreatedDateTime;
      rolesInGraph1.LastModifiedByID = row.LastModifiedByID;
      rolesInGraph1.LastModifiedByScreenID = row.LastModifiedByScreenID;
      rolesInGraph1.LastModifiedDateTime = row.LastModifiedDateTime;
      nullable1 = row.RoleRight;
      int num1 = 0;
      if (nullable1.GetValueOrDefault() >= num1 & nullable1.HasValue)
      {
        this.defGraph.Cache.Update((object) rolesInGraph1);
      }
      else
      {
        if (this.defGraph.Select((object) row.ScreenID, (object) "/") != null)
          this.defGraph.Cache.Delete((object) rolesInGraph1);
        this.TryToResetAccessRights(row);
      }
      nullable1 = row.RoleRight;
      int num2 = 0;
      if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
      {
        nullable1 = row.RoleRight;
        int num3 = 4;
        if (!(nullable1.GetValueOrDefault() == num3 & nullable1.HasValue))
          goto label_45;
      }
      if (e.OldRow != null)
      {
        nullable1 = row.RoleRight;
        nullable2 = ((Role) e.OldRow).RoleRight;
        if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
        {
          PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(row.ScreenID);
          if (mapNodeByScreenId != null)
          {
            foreach (PXSiteMapNode descendant in PXSiteMap.Provider.GetDescendants(mapNodeByScreenId))
            {
              if (!string.IsNullOrEmpty(descendant.ScreenID))
              {
                bool flag = true;
                foreach (PXResult<RolesInGraph> pxResult in this.defGraph.Select((object) descendant.ScreenID, (object) "/"))
                {
                  RolesInGraph rolesInGraph3 = (RolesInGraph) pxResult;
                  if (rolesInGraph3.Rolename == row.RoleName)
                  {
                    short? accessrights = rolesInGraph3.Accessrights;
                    int? nullable4;
                    if (!accessrights.HasValue)
                    {
                      nullable1 = new int?();
                      nullable4 = nullable1;
                    }
                    else
                      nullable4 = new int?((int) accessrights.GetValueOrDefault());
                    nullable2 = nullable4;
                    int num4 = -1;
                    flag = nullable2.GetValueOrDefault() == num4 & nullable2.HasValue;
                    break;
                  }
                }
                if (flag)
                {
                  RolesInGraph rolesInGraph4 = new RolesInGraph();
                  rolesInGraph4.ApplicationName = "/";
                  rolesInGraph4.ScreenID = descendant.ScreenID;
                  rolesInGraph4.Rolename = row.RoleName;
                  RolesInGraph rolesInGraph5 = rolesInGraph4;
                  int? roleRight2 = row.RoleRight;
                  short? nullable5 = roleRight2.HasValue ? new short?((short) roleRight2.GetValueOrDefault()) : new short?();
                  rolesInGraph5.Accessrights = nullable5;
                  rolesInGraph4.CreatedByID = row.CreatedByID;
                  rolesInGraph4.CreatedByScreenID = row.CreatedByScreenID;
                  rolesInGraph4.CreatedDateTime = row.CreatedDateTime;
                  rolesInGraph4.LastModifiedByID = row.LastModifiedByID;
                  rolesInGraph4.LastModifiedByScreenID = row.LastModifiedByScreenID;
                  rolesInGraph4.LastModifiedDateTime = row.LastModifiedDateTime;
                  this.defGraph.Cache.Update((object) rolesInGraph4);
                }
              }
            }
          }
        }
      }
    }
    else if (string.IsNullOrEmpty(row.MemberName))
    {
      RolesInCache rolesInCache1 = new RolesInCache();
      rolesInCache1.ApplicationName = "/";
      rolesInCache1.ScreenID = row.ScreenID;
      rolesInCache1.Cachetype = row.CacheName;
      rolesInCache1.Rolename = row.RoleName;
      RolesInCache rolesInCache2 = rolesInCache1;
      int? roleRight = row.RoleRight;
      short? nullable6 = roleRight.HasValue ? new short?((short) roleRight.GetValueOrDefault()) : new short?();
      rolesInCache2.Accessrights = nullable6;
      rolesInCache1.CreatedByID = row.CreatedByID;
      rolesInCache1.CreatedByScreenID = row.CreatedByScreenID;
      rolesInCache1.CreatedDateTime = row.CreatedDateTime;
      rolesInCache1.LastModifiedByID = row.LastModifiedByID;
      rolesInCache1.LastModifiedByScreenID = row.LastModifiedByScreenID;
      rolesInCache1.LastModifiedDateTime = row.LastModifiedDateTime;
      nullable2 = row.RoleRight;
      int num = 0;
      if (nullable2.GetValueOrDefault() >= num & nullable2.HasValue)
      {
        this.defCache.Cache.Update((object) rolesInCache1);
      }
      else
      {
        if (this.defCache.Select((object) row.ScreenID, (object) row.ScreenID, (object) "/") != null)
          this.defCache.Cache.Delete((object) rolesInCache1);
        this.TryToResetAccessRights(row);
      }
    }
    else
    {
      RolesInMember rolesInMember1 = new RolesInMember();
      rolesInMember1.ApplicationName = "/";
      rolesInMember1.ScreenID = row.ScreenID;
      rolesInMember1.Cachetype = row.CacheName;
      rolesInMember1.Membername = row.MemberName;
      rolesInMember1.Rolename = row.RoleName;
      RolesInMember rolesInMember2 = rolesInMember1;
      int? roleRight = row.RoleRight;
      short? nullable7 = roleRight.HasValue ? new short?((short) roleRight.GetValueOrDefault()) : new short?();
      rolesInMember2.Accessrights = nullable7;
      rolesInMember1.CreatedByID = row.CreatedByID;
      rolesInMember1.CreatedByScreenID = row.CreatedByScreenID;
      rolesInMember1.CreatedDateTime = row.CreatedDateTime;
      rolesInMember1.LastModifiedByID = row.LastModifiedByID;
      rolesInMember1.LastModifiedByScreenID = row.LastModifiedByScreenID;
      rolesInMember1.LastModifiedDateTime = row.LastModifiedDateTime;
      nullable2 = row.RoleRight;
      int num5 = 0;
      if (nullable2.GetValueOrDefault() >= num5 & nullable2.HasValue)
      {
        nullable2 = row.RoleRight;
        int num6 = 2;
        if (nullable2.GetValueOrDefault() > num6 & nullable2.HasValue)
          rolesInMember1.Accessrights = new short?((short) 2);
        this.defMember.Cache.Update((object) rolesInMember1);
      }
      else
      {
        if (this.defMember.Select((object) row.ScreenID, (object) row.CacheName, (object) row.MemberName, (object) "/") != null)
          this.defMember.Cache.Delete((object) rolesInMember1);
        this.TryToResetAccessRights(row);
      }
    }
label_45:
    if (!(this.Caches[typeof (WikiAccessRights)].CreateInstance() is WikiAccessRights instance1))
      return;
    WikiAccessRights wikiAccessRights = instance1;
    nullable2 = row.RoleRight;
    short? nullable8 = new short?((short) nullable2.Value);
    wikiAccessRights.AccessRights = nullable8;
    instance1.ApplicationName = "/";
    instance1.PageID = this.CurrentPage.Current.PageID;
    instance1.RoleName = row.RoleName;
    this.Caches[typeof (WikiAccessRights)].Update((object) instance1);
    short? accessRights = instance1.AccessRights;
    int? nullable9;
    if (!accessRights.HasValue)
    {
      nullable1 = new int?();
      nullable9 = nullable1;
    }
    else
      nullable9 = new int?((int) accessRights.GetValueOrDefault());
    nullable2 = nullable9;
    int num7 = -1;
    if (nullable2.GetValueOrDefault() == num7 & nullable2.HasValue || this.Wikis.Ask("Wiki Rights", "Do you want to apply rights to nested nodes?", MessageButtons.YesNo) != WebDialogResult.Yes)
      return;
    WikiAccessRightsMaintenance instance2 = PXGraph.CreateInstance<WikiAccessRightsMaintenance>();
    accessRights = instance1.AccessRights;
    int? nullable10;
    if (!accessRights.HasValue)
    {
      nullable1 = new int?();
      nullable10 = nullable1;
    }
    else
      nullable10 = new int?((int) accessRights.GetValueOrDefault());
    nullable2 = nullable10;
    int num8 = -1;
    if (!(nullable2.GetValueOrDefault() == num8 & nullable2.HasValue))
      instance2.CheckChildrenNodes(this.CurrentPage.Current.PageID, true, instance1.ApplicationName, instance1.RoleName);
    instance2.Save.Press();
  }

  internal void Role_RowPersisting(PXCache cache, PXRowPersistingEventArgs e) => e.Cancel = true;

  internal void Role_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    Role row = (Role) e.Row;
    if (row == null || string.IsNullOrEmpty(row.ScreenID) || !(row.ScreenID == "HD000000"))
      return;
    PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
  }

  public override void Persist()
  {
    bool flag = this.screenToSiteMapAddHelper.Persist(this.Wikis.Current);
    this.IsSiteMapAltered = flag ? flag : this.IsSiteMapAltered;
    base.Persist();
    PXAccess.Clear();
    PXSiteMap.Provider.Clear();
    PXSiteMap.WikiProvider.Clear();
  }

  private void TryToResetAccessRights(Role rootRole)
  {
    if (this.Roles.Ask(PXLocalizer.Localize("Warning", typeof (ActionsMessages).FullName), PXLocalizer.Localize("Would you like to reset access rights for lower levels?", typeof (Messages).FullName), MessageButtons.YesNo, MessageIcon.Question) != WebDialogResult.Yes || string.IsNullOrEmpty(rootRole.ScreenID) || string.IsNullOrEmpty(rootRole.RoleName))
      return;
    PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(rootRole.ScreenID);
    if (screenIdUnsecure.ScreenID == null)
      return;
    List<PXSiteMapNode> list = ((IEnumerable<PXSiteMapNode>) PXSiteMap.Provider.GetDescendants(screenIdUnsecure)).Where<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (node => !string.IsNullOrEmpty(node.ScreenID))).ToList<PXSiteMapNode>();
    list.Remove(screenIdUnsecure);
    foreach (PXSiteMapNode pxSiteMapNode in list)
    {
      foreach (PXResult<RolesInGraph> pxResult in this.allGraph.Select((object) pxSiteMapNode.ScreenID, (object) rootRole.RoleName))
        this.allGraph.Cache.Delete((object) (RolesInGraph) pxResult);
      foreach (PXResult<RolesInCache> pxResult in this.allCache.Select((object) pxSiteMapNode.ScreenID, (object) rootRole.RoleName))
        this.allCache.Cache.Delete((object) (RolesInCache) pxResult);
      foreach (PXResult<RolesInMember> pxResult in this.allMember.Select((object) pxSiteMapNode.ScreenID, (object) rootRole.RoleName))
        this.allMember.Cache.Delete((object) (RolesInMember) pxResult);
    }
  }
}
