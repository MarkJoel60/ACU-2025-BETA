// Decompiled with JetBrains decompiler
// Type: PX.Objects.SM.SPWikiCategoryMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.Search;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;

#nullable disable
namespace PX.Objects.SM;

[Serializable]
public class SPWikiCategoryMaint : PXGraph<SPWikiCategoryMaint>
{
  public PXSelect<SPWikiCategory> WikiCategory;
  public PXSelect<SPWikiCategoryTags, Where<SPWikiCategoryTags.categoryID, Equal<Current<SPWikiCategory.categoryID>>>> WikiCategoryDetails;
  public SPWikiCategoryMaint.PXSelectWikiFoldersTree Folders;
  public PXSave<SPWikiCategory> Save;
  public PXCancel<SPWikiCategory> Cancel;
  public PXInsert<SPWikiCategory> Insert;
  public PXDelete<SPWikiCategory> Delete;
  public PXFirst<SPWikiCategory> First;
  public PXPrevious<SPWikiCategory> Previous;
  public PXNext<SPWikiCategory> Next;
  public PXLast<SPWikiCategory> Last;

  public SPWikiCategoryMaint() => Wiki.BlockIfOnlineHelpIsOn();

  protected virtual void SPWikiCategoryTags_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is SPWikiCategoryTags row))
      return;
    row.CategoryID = ((PXSelectBase<SPWikiCategory>) this.WikiCategory).Current.CategoryID;
    if (row == null || row.PageName == null)
      return;
    PXResult<WikiPage, WikiPageLanguage> pxResult = (PXResult<WikiPage, WikiPageLanguage>) PXResultset<WikiPage>.op_Implicit(PXSelectBase<WikiPage, PXSelectJoin<WikiPage, LeftJoin<WikiPageLanguage, On<WikiPage.pageID, Equal<WikiPageLanguage.pageID>>>, Where<WikiPage.name, Equal<Required<WikiPage.name>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.PageName
    }));
    if (pxResult == null)
      return;
    WikiPage wikiPage = ((PXResult) pxResult)[typeof (WikiPage)] as WikiPage;
    WikiPageLanguage wikiPageLanguage = ((PXResult) pxResult)[typeof (WikiPageLanguage)] as WikiPageLanguage;
    if (wikiPage != null)
    {
      row.PageName = wikiPage.Name;
      row.PageID = wikiPage.PageID;
    }
    if (wikiPageLanguage != null)
      row.PageTitle = wikiPageLanguage.Title;
    else
      row.PageTitle = row.PageName;
  }

  protected virtual void SPWikiCategoryTags_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is SPWikiCategoryTags row) || row.PageName == null)
      return;
    PXResult<WikiPage, WikiPageLanguage> pxResult = (PXResult<WikiPage, WikiPageLanguage>) PXResultset<WikiPage>.op_Implicit(PXSelectBase<WikiPage, PXSelectJoin<WikiPage, LeftJoin<WikiPageLanguage, On<WikiPage.pageID, Equal<WikiPageLanguage.pageID>>>, Where<WikiPage.name, Equal<Required<WikiPage.name>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.PageName
    }));
    if (pxResult == null)
      return;
    WikiPage wikiPage = ((PXResult) pxResult)[typeof (WikiPage)] as WikiPage;
    WikiPageLanguage wikiPageLanguage = ((PXResult) pxResult)[typeof (WikiPageLanguage)] as WikiPageLanguage;
    if (wikiPage != null)
    {
      row.PageName = wikiPage.Name;
      row.PageID = wikiPage.PageID;
    }
    if (wikiPageLanguage != null)
      row.PageTitle = wikiPageLanguage.Title;
    else
      row.PageTitle = row.PageName;
  }

  public class PXSelectWikiFoldersTree : PXSelectBase<WikiPageSimple>
  {
    public PXSelectWikiFoldersTree(PXGraph graph)
    {
      // ISSUE: method pointer
      ((PXSelectBase) this).View = this.CreateView(graph, (Delegate) new PXSelectDelegate<Guid?>((object) this, __methodptr(folders)));
    }

    public PXSelectWikiFoldersTree(PXGraph graph, Delegate handler)
    {
      ((PXSelectBase) this).View = this.CreateView(graph, handler);
    }

    private PXView CreateView(PXGraph graph, Delegate handler)
    {
      return new PXView(graph, false, (BqlCommand) new Select<WikiPageSimple, Where<WikiPageSimple.parentUID, Equal<Argument<Guid?>>>, OrderBy<Asc<WikiPage.number>>>(), handler);
    }

    internal IEnumerable folders([PXGuid] Guid? PageID)
    {
      bool flag = false;
      if (!PageID.HasValue)
      {
        PageID = new Guid?(Guid.Empty);
        flag = true;
      }
      HttpContext ctx = HttpContext.Current;
      HttpContext.Current = (HttpContext) null;
      PXSiteMapNode siteMapNodeFromKey = ((PXSiteMapProvider) PXSiteMap.WikiProvider).FindSiteMapNodeFromKey(PageID.Value);
      if (flag)
      {
        yield return (object) this.CreateWikiPageSimple(siteMapNodeFromKey);
      }
      else
      {
        foreach (PXSiteMapNode childNode in (IEnumerable<PXSiteMapNode>) siteMapNodeFromKey.ChildNodes)
          yield return (object) this.CreateWikiPageSimple(childNode);
      }
      HttpContext.Current = ctx;
    }

    private WikiPageSimple CreateWikiPageSimple(PXSiteMapNode node)
    {
      WikiPageSimple wikiPageSimple = new WikiPageSimple();
      ((WikiPage) wikiPageSimple).PageID = new Guid?(node.NodeID);
      ((WikiPage) wikiPageSimple).Name = node is PXWikiMapNode pxWikiMapNode ? pxWikiMapNode.Name : (string) null;
      ((WikiPage) wikiPageSimple).ParentUID = new Guid?(node.ParentID);
      ((WikiPage) wikiPageSimple).Title = string.IsNullOrEmpty(node.Title) ? ((PXWikiMapNode) node).Name : node.Title;
      return wikiPageSimple;
    }
  }
}
