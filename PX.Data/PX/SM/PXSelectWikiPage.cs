// Decompiled with JetBrains decompiler
// Type: PX.SM.PXSelectWikiPage
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
public class PXSelectWikiPage : PXSelectBase<WikiPageSimple>
{
  public System.Type WikiID;

  public PXSelectWikiPage(PXGraph graph)
  {
    this.View = PXSelectWikiPage.CreateView(graph, (Delegate) new PXSelectDelegate<string>(this.articles));
  }

  public PXSelectWikiPage(PXGraph graph, Delegate handler)
  {
    this.View = PXSelectWikiPage.CreateView(graph, handler);
  }

  private IEnumerable articles([PXString] string parent)
  {
    PXSelectWikiPage pxSelectWikiPage = this;
    Guid? currentWikiId = pxSelectWikiPage.GetCurrentWikiID();
    if (currentWikiId.HasValue)
    {
      Guid? nullable = GUID.CreateGuid(parent) ?? currentWikiId;
      PXGraph graph = pxSelectWikiPage.View.Graph;
      object[] objArray = new object[1]{ (object) nullable };
      foreach (PXResult<WikiPageSimple> pxResult in PXSelectBase<WikiPageSimple, PXSelect<WikiPageSimple, Where<WikiPageSimple.parentUID, Equal<Required<WikiPageSimple.parentUID>>>>.Config>.Select(graph, objArray))
      {
        WikiPageSimple wikiPageSimple = (WikiPageSimple) pxResult;
        if (PXSiteMap.WikiProvider.GetAccessRights(wikiPageSimple.PageID.Value) >= PXWikiRights.Select)
        {
          PXSiteMapNode siteMapNodeFromKey = PXSiteMap.WikiProvider.FindSiteMapNodeFromKey(wikiPageSimple.PageID.Value);
          if (siteMapNodeFromKey != null)
            wikiPageSimple.Title = siteMapNodeFromKey.Title;
          yield return (object) wikiPageSimple;
        }
      }
    }
  }

  private static PXView CreateView(PXGraph graph, Delegate handler)
  {
    return new PXView(graph, false, (BqlCommand) new PX.Data.Select<WikiPageSimple, Where<WikiPageSimple.pageID, Equal<WikiPageSimple.pageID>>, OrderBy<Desc<WikiPage.articleType, Asc<WikiPage.number>>>>(), handler);
  }

  private Guid? GetCurrentWikiID()
  {
    System.Type wikiId = this.WikiID;
    PXCache cach = this.View.Graph.Caches[BqlCommand.GetItemType(wikiId)];
    return cach.Current != null ? (Guid?) cach.GetValue(cach.Current, wikiId.Name) : new Guid?();
  }
}
