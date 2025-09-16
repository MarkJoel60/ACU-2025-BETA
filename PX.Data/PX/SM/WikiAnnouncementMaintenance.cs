// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiAnnouncementMaintenance
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

/// <exclude />
[PXHidden(ServiceVisible = true)]
public class WikiAnnouncementMaintenance : 
  WikiPageMaint<WikiAnnouncement, WikiAnnouncement, Where<WikiAnnouncement.articleType, Equal<WikiArticleType.announcement>>>
{
  public WikiAnnouncementMaintenance()
  {
    PXUIFieldAttribute.SetDisplayName<WikiPage.name>(this.Pages.Cache, "Announcement ID");
  }

  public void WikiAnnouncementRowUpdated(Events.RowUpdated<WikiAnnouncement> e)
  {
    if (!e.Row.PublishedDateTime.HasValue || e.Row.StartDate.HasValue)
      return;
    e.Row.StartDate = e.Row.PublishedDateTime;
  }

  protected void StartDate_Default(
    Events.FieldDefaulting<WikiAnnouncement.startDate> e)
  {
    e.NewValue = (object) System.DateTime.Now;
  }

  public override void OnWikiCreated(WikiDescriptor rec)
  {
    PXDatabase.Insert<WikiAnnouncement>(new PXDataFieldAssign("PageID", (object) rec.DeletedID), new PXDataFieldAssign("KeepOnTop", (object) false), new PXDataFieldAssign("HideOnExpire", (object) false), new PXDataFieldAssign("HiddenDashboard", (object) false));
  }
}
