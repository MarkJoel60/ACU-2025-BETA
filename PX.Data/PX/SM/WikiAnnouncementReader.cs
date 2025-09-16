// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiAnnouncementReader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace PX.SM;

/// <exclude />
[DashboardType(new int[] {3})]
[FileAttachable(false)]
public class WikiAnnouncementReader : PXGraph<WikiAnnouncementReader>
{
  public PXFilter<WikiAnnouncementFilter> Filter;
  public PXSave<WikiAnnouncementFilter> Save;
  public PXCancel<WikiAnnouncementFilter> Cancel;
  public PXAction<WikiAnnouncementFilter> Insert;
  public PXAction<WikiAnnouncementFilter> View;
  public PXAction<WikiAnnouncementFilter> Edit;
  public PXAction<WikiAnnouncementFilter> Delete;
  public PXSelect<WikiDescriptor, Where<WikiDescriptor.pageID, Equal<Current<WikiAnnouncementFilter.wikiID>>>> Wiki;
  public PXSelectReadonly<WikiAnnouncement, Where<WikiAnnouncement.wikiID, Equal<Current<WikiAnnouncementFilter.wikiID>>>, OrderBy<Desc<WikiAnnouncement.keepOnTop, Desc<WikiAnnouncement.startDate>>>> Announcements;

  [PXButton(Tooltip = "New", ImageKey = "AddNew")]
  [PXUIField(DisplayName = "New")]
  protected IEnumerable insert(PXAdapter e)
  {
    WikiActions.Insert((WikiDescriptor) this.Wiki.SelectWindowed(0, 1), (WikiPage) null, (string) null);
    return e.Get();
  }

  [PXButton(Tooltip = "Edit Page", ImageKey = "DataEntry")]
  [PXUIField(DisplayName = "Edit")]
  protected IEnumerable edit(PXAdapter e)
  {
    WikiActions.Edit((WikiDescriptor) this.Wiki.SelectWindowed(0, 1), (WikiPage) this.Announcements.Current);
    return e.Get();
  }

  [PXButton(Tooltip = "Delete Page", ImageKey = "RemoveArticle")]
  [PXUIField(DisplayName = "Delete")]
  protected IEnumerable delete(PXAdapter e)
  {
    WikiActions.Delete((WikiPage) this.Announcements.Current);
    return e.Get();
  }

  [PXButton(Tooltip = "View or edit the article.", ImageKey = "Inquiry")]
  [PXUIField(DisplayName = "View/Edit", Visibility = PXUIVisibility.Invisible)]
  protected IEnumerable view(PXAdapter e)
  {
    WikiActions.View((WikiPage) this.Announcements.Current);
    return e.Get();
  }

  protected IEnumerable announcements() => this.GetRecords(new int?());

  public int CountAnnouncements(int? categoryID)
  {
    int num = 0;
    foreach (WikiAnnouncement record in this.GetRecords(categoryID))
      ++num;
    return num;
  }

  private IEnumerable GetRecords(int? categoryID)
  {
    WikiAnnouncementReader graph = this;
    PXSelectBase<WikiAnnouncement> pxSelectBase = (PXSelectBase<WikiAnnouncement>) new PXSelectJoin<WikiAnnouncement, LeftJoin<WikiPageLanguage, On<WikiPageLanguage.pageID, Equal<WikiAnnouncement.pageID>, And<WikiPageLanguage.language, Equal<Required<WikiPageLanguage.language>>>>>, Where<WikiAnnouncement.wikiID, Equal<Current<WikiAnnouncementFilter.wikiID>>, And<WikiAnnouncement.hiddenDashboard, NotEqual<WikiAnnouncement.hiddenDashboard.True>, And<WikiPageLanguage.lastPublishedID, Greater<WikiAnnouncement.hiddenDashboard.Zero>, And<WikiPage.statusID, NotEqual<WikiPageStatus.deleted>>>>>>((PXGraph) graph);
    List<object> objectList = new List<object>();
    objectList.Add((object) Thread.CurrentThread.CurrentCulture.Name);
    WikiAnnouncementFilter current = graph.Filter.Current;
    if (current.CreatedByID.HasValue)
      pxSelectBase.WhereAnd<Where<WikiAnnouncement.createdByID, Equal<Current<WikiAnnouncementFilter.createdByID>>>>();
    if (!current.DateFrom.HasValue && !current.DateTo.HasValue)
    {
      pxSelectBase.WhereAnd<Where<WikiAnnouncement.startDate, LessEqual<Required<WikiAnnouncement.startDate>>, And<Where<WikiAnnouncement.expireDate, IsNull, Or<WikiAnnouncement.expireDate, Greater<Required<WikiAnnouncement.expireDate>>>>>>>();
      objectList.Add((object) System.DateTime.Now);
      objectList.Add((object) System.DateTime.Now);
    }
    else
    {
      if (current.DateFrom.HasValue)
        pxSelectBase.WhereAnd<Where<WikiAnnouncement.expireDate, IsNull, Or<WikiAnnouncement.expireDate, GreaterEqual<Current<WikiAnnouncementFilter.dateFrom>>>>>();
      if (current.DateTo.HasValue)
        pxSelectBase.WhereAnd<Where<WikiAnnouncement.startDate, LessEqual<Current<WikiAnnouncementFilter.dateTo>>>>();
    }
    foreach (PXResult<WikiAnnouncement, WikiPageLanguage> pxResult in pxSelectBase.Select(objectList.ToArray()))
    {
      WikiAnnouncement record = (WikiAnnouncement) pxResult;
      WikiPageLanguage wikiPageLanguage = (WikiPageLanguage) pxResult;
      PXWikiRights accessRights = PXSiteMap.WikiProvider.GetAccessRights(record.PageID.Value);
      if (accessRights < PXWikiRights.Update)
      {
        if (accessRights >= PXWikiRights.Select && wikiPageLanguage != null)
        {
          int? lastPublishedId = wikiPageLanguage.LastPublishedID;
          int num = 0;
          if (!(lastPublishedId.GetValueOrDefault() > num & lastPublishedId.HasValue))
            continue;
        }
        else
          continue;
      }
      record.Title = wikiPageLanguage.Title;
      record.Keywords = wikiPageLanguage.Keywords;
      record.Summary = wikiPageLanguage.Summary;
      record.OldStatusID = record.StatusID;
      yield return (object) record;
    }
  }
}
