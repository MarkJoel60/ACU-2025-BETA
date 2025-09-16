// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiSitePageMaintenance
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

/// <exclude />
[PXHidden(ServiceVisible = true)]
public class WikiSitePageMaintenance : 
  WikiPageMaint<WikiSitePage, WikiSitePage, Where<WikiSitePage.articleType, Equal<WikiArticleType.sitePage>>>
{
  public PXSelect<WikiPageMeta, Where<WikiPageMeta.pageID, Equal<Current<WikiPage.pageID>>>> CustomMeta;

  protected void WikiSitePage_RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Delete)
      return;
    PXDatabase.Delete<WikiPageMeta>(new PXDataFieldRestrict(typeof (WikiPageMeta.pageID).Name, PXDbType.UniqueIdentifier, (object) ((WikiPage) e.Row).PageID));
  }

  public override void OnWikiCreated(WikiDescriptor rec)
  {
    PXDatabase.Insert<WikiSitePage>(new PXDataFieldAssign("PageID", (object) rec.DeletedID), new PXDataFieldAssign("Secure", (object) false));
  }
}
