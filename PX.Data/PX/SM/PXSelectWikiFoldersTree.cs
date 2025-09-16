// Decompiled with JetBrains decompiler
// Type: PX.SM.PXSelectWikiFoldersTree
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Context;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;

#nullable disable
namespace PX.SM;

/// <exclude />
public class PXSelectWikiFoldersTree : PXSelectBase<WikiPageSimple>
{
  public PXSelectWikiFoldersTree(PXGraph graph)
  {
    this.View = this.CreateView(graph, (Delegate) new PXSelectDelegate<Guid?>(this.folders));
  }

  public PXSelectWikiFoldersTree(PXGraph graph, Delegate handler)
  {
    this.View = this.CreateView(graph, handler);
  }

  private PXView CreateView(PXGraph graph, Delegate handler)
  {
    return new PXView(graph, false, (BqlCommand) new PX.Data.Select<WikiPageSimple, Where<WikiPageSimple.parentUID, Equal<Argument<Guid?>>>, OrderBy<Asc<WikiPage.number>>>(), handler);
  }

  internal IEnumerable folders([PXGuid] Guid? PageID)
  {
    List<WikiPageSimple> wikiPageSimpleList = new List<WikiPageSimple>();
    bool flag = false;
    if (!PageID.HasValue)
    {
      PageID = new Guid?(Guid.Empty);
      flag = true;
    }
    int? singleCompanyId = SlotStore.Instance.GetSingleCompanyId();
    HttpContext current = HttpContext.Current;
    HttpContext.Current = (HttpContext) null;
    if (singleCompanyId.HasValue)
      SlotStore.Instance.SetSingleCompanyId(singleCompanyId.GetValueOrDefault());
    PXSiteMapNode siteMapNodeFromKey = PXSiteMap.WikiProvider.FindSiteMapNodeFromKey(PageID.Value);
    if (flag)
    {
      wikiPageSimpleList.Add(this.CreateWikiPageSimple(siteMapNodeFromKey));
    }
    else
    {
      foreach (PXSiteMapNode childNode in (IEnumerable<PXSiteMapNode>) siteMapNodeFromKey.ChildNodes)
      {
        if (((PXWikiMapNode) childNode).IsFolder)
          wikiPageSimpleList.Add(this.CreateWikiPageSimple(childNode));
      }
    }
    HttpContext.Current = current;
    return (IEnumerable) wikiPageSimpleList;
  }

  private WikiPageSimple CreateWikiPageSimple(PXSiteMapNode node)
  {
    WikiPageSimple wikiPageSimple = new WikiPageSimple();
    wikiPageSimple.PageID = new Guid?(node.NodeID);
    wikiPageSimple.ParentUID = new Guid?(node.ParentID);
    wikiPageSimple.Title = string.IsNullOrEmpty(node.Title) ? ((PXWikiMapNode) node).Name : node.Title;
    return wikiPageSimple;
  }
}
