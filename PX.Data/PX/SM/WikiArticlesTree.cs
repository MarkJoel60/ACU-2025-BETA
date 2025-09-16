// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiArticlesTree
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.SM;

/// <exclude />
[PXHidden]
public class WikiArticlesTree : PXGraph<WikiArticlesTree>
{
  public PXSelect<WikiPageSimple, Where<WikiPageSimple.pageID, Equal<WikiPageSimple.pageID>>, OrderBy<Desc<WikiPage.articleType, Asc<WikiPage.number>>>> Articles;

  protected IEnumerable articles(string PageID)
  {
    WikiArticlesTree wikiArticlesTree = this;
    Guid? nullable = GUID.CreateGuid(PageID);
    Guid guid = nullable ?? Guid.Empty;
    WikiArticlesTree graph = wikiArticlesTree;
    object[] objArray = new object[1]{ (object) guid };
    foreach (PXResult<WikiPageSimple> pxResult in PXSelectBase<WikiPageSimple, PXSelect<WikiPageSimple, Where<WikiPageSimple.parentUID, Equal<Required<WikiPageSimple.parentUID>>>>.Config>.Select((PXGraph) graph, objArray))
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
