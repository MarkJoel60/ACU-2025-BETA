// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiPageMapMaintenance
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#nullable disable
namespace PX.SM;

/// <exclude />
public class WikiPageMapMaintenance : PXGraph<WikiPageMapMaintenance>
{
  public PXSave<WikiPage> Save;
  public PXCancel<WikiPage> Cancel;
  public PXAction<WikiPage> DeletePages;
  public PXAction<WikiPage> RowDown;
  public PXAction<WikiPage> RowUp;
  public PXAction<WikiPage> ViewArticle;
  public PXSelectWikiFoldersTree Folders;
  public PXSelectOrderBy<WikiPage, OrderBy<Asc<WikiPage.number>>> Children;

  public WikiPageMapMaintenance() => Wiki.BlockIfOnlineHelpIsOn();

  internal IEnumerable children([PXGuid] Guid? parent)
  {
    if (!parent.HasValue && this.Folders.Current != null)
      parent = this.Folders.Current.PageID;
    HttpContext current = HttpContext.Current;
    HttpContext.Current = (HttpContext) null;
    List<WikiPage> wikiPages = this.GetWikiPages(parent);
    HttpContext.Current = current;
    return (IEnumerable) wikiPages;
  }

  [PXUIField(DisplayName = " ", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton(ImageKey = "RecordDel")]
  public IEnumerable deletePages(PXAdapter adapter)
  {
    foreach (PXResult<WikiPage> pxResult in PXSelectBase<WikiPage, PXSelect<WikiPage, Where<WikiPage.parentUID, Equal<Current<WikiPageSimple.pageID>>, And<WikiPage.selected, Equal<PX.Data.True>>>>.Config>.Select((PXGraph) this))
    {
      WikiPage wikiPage = (WikiPage) pxResult;
      WikiDescriptor wikiDescriptor = (WikiDescriptor) PXSelectBase<WikiDescriptor, PXSelect<WikiDescriptor, Where<WikiDescriptor.pageID, Equal<Required<WikiDescriptor.pageID>>>>.Config>.Select((PXGraph) this, (object) wikiPage.WikiID);
      wikiPage.ParentUID = wikiDescriptor.DeletedID;
      wikiPage.StatusID = new int?(4);
      this.RecursivelyMarkDeleted((WikiPage) this.Caches[typeof (WikiPage)].Update((object) wikiPage));
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View Article", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton(ImageKey = "Inquiry")]
  public IEnumerable viewArticle(PXAdapter adapter)
  {
    WikiPage current = this.Children.Current;
    if (current == null)
      return adapter.Get();
    throw new PXRedirectToUrlException("~/Wiki/ShowWiki.aspx?PageID=" + current.PageID.Value.ToString(), PXBaseRedirectException.WindowMode.Same, "View Article");
  }

  [PXButton(ImageKey = "ArrowDown", Tooltip = "Move Row Down")]
  [PXUIField(DisplayName = " ", MapEnableRights = PXCacheRights.Update)]
  internal IEnumerable rowDown(PXAdapter adapter)
  {
    if (this.Children.Current == null)
      return adapter.Get();
    WikiPage current = this.Children.Current;
    if (PXSiteMap.WikiProvider.GetAccessRights(current.PageID.Value) < PXWikiRights.Update)
      throw new PXException("You don't have enough rights to move this article.");
    List<WikiPage> source = new List<WikiPage>();
    foreach (WikiPage wikiPage in this.GetWikiPages(this.Folders.Current.PageID))
    {
      double? number1 = wikiPage.Number;
      double? number2 = current.Number;
      if (number1.GetValueOrDefault() > number2.GetValueOrDefault() & number1.HasValue & number2.HasValue)
        source.Add(wikiPage);
    }
    List<WikiPage> list = source.OrderBy<WikiPage, double?>((Func<WikiPage, double?>) (p => p.Number)).ToList<WikiPage>().Take<WikiPage>(2).ToList<WikiPage>();
    if (list.Count == 0)
      return adapter.Get();
    if (list.Count == 1)
    {
      WikiPage wikiPage = current;
      double? number = list.First<WikiPage>().Number;
      double num = 1.0;
      double? nullable = number.HasValue ? new double?(number.GetValueOrDefault() + num) : new double?();
      wikiPage.Number = nullable;
    }
    if (list.Count == 2)
    {
      WikiPage wikiPage = current;
      double? number = list.First<WikiPage>().Number;
      double? nullable1 = list.Last<WikiPage>().Number;
      double? nullable2 = number.HasValue & nullable1.HasValue ? new double?(number.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new double?();
      double num = 2.0;
      double? nullable3;
      if (!nullable2.HasValue)
      {
        nullable1 = new double?();
        nullable3 = nullable1;
      }
      else
        nullable3 = new double?(nullable2.GetValueOrDefault() / num);
      wikiPage.Number = nullable3;
    }
    this.Children.Cache.Update((object) current);
    if (list.Count > 0)
      this.Children.Current = list.First<WikiPage>();
    return adapter.Get();
  }

  [PXButton(ImageKey = "ArrowUp", Tooltip = "Move Row Up")]
  [PXUIField(DisplayName = " ", MapEnableRights = PXCacheRights.Update)]
  internal IEnumerable rowUp(PXAdapter adapter)
  {
    if (this.Children.Current == null)
      return adapter.Get();
    WikiPage current = this.Children.Current;
    if (PXSiteMap.WikiProvider.GetAccessRights(current.PageID.Value) < PXWikiRights.Update)
      throw new PXException("You don't have enough rights to move this article.");
    List<WikiPage> source = new List<WikiPage>();
    foreach (WikiPage wikiPage in this.GetWikiPages(this.Folders.Current.PageID))
    {
      double? number1 = wikiPage.Number;
      double? number2 = current.Number;
      if (number1.GetValueOrDefault() < number2.GetValueOrDefault() & number1.HasValue & number2.HasValue)
        source.Add(wikiPage);
    }
    List<WikiPage> list = source.OrderByDescending<WikiPage, double?>((Func<WikiPage, double?>) (p => p.Number)).ToList<WikiPage>().Take<WikiPage>(2).ToList<WikiPage>();
    if (list.Count == 0)
      return adapter.Get();
    if (list.Count == 1)
    {
      WikiPage wikiPage = current;
      double? number = list.First<WikiPage>().Number;
      double num = 1.0;
      double? nullable = number.HasValue ? new double?(number.GetValueOrDefault() - num) : new double?();
      wikiPage.Number = nullable;
    }
    if (list.Count == 2)
    {
      WikiPage wikiPage = current;
      double? number = list.First<WikiPage>().Number;
      double? nullable1 = list.Last<WikiPage>().Number;
      double? nullable2 = number.HasValue & nullable1.HasValue ? new double?(number.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new double?();
      double num = 2.0;
      double? nullable3;
      if (!nullable2.HasValue)
      {
        nullable1 = new double?();
        nullable3 = nullable1;
      }
      else
        nullable3 = new double?(nullable2.GetValueOrDefault() / num);
      wikiPage.Number = nullable3;
    }
    this.Children.Cache.Update((object) current);
    if (list.Count > 0)
      this.Children.Current = list.First<WikiPage>();
    return adapter.Get();
  }

  [PXDBGuid(false)]
  [PXUIField(Visible = false, DisplayName = "Wiki ID")]
  protected virtual void _(Events.CacheAttached<WikiPage.wikiID> e)
  {
  }

  [PXDefault]
  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "ID", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
  protected virtual void _(Events.CacheAttached<WikiPage.name> e)
  {
  }

  [PXDBDouble]
  [PXDefault(0.0)]
  [PXUIField(DisplayName = "Number")]
  protected virtual void _(Events.CacheAttached<WikiPage.number> e)
  {
  }

  protected void _(Events.RowSelecting<WikiPage> e)
  {
    WikiPage row = e.Row;
    if (row == null || row.Title != null)
      return;
    PXSiteMapNode siteMapNodeFromKey = PXSiteMap.WikiProvider.FindSiteMapNodeFromKey(row.PageID.Value);
    row.Title = siteMapNodeFromKey == null ? row.Name : siteMapNodeFromKey.Title;
  }

  internal void WikiPage_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    WikiPage row = (WikiPage) e.Row;
    if (row.PageID.HasValue)
      return;
    WikiPage wikiPage1 = (WikiPage) null;
    foreach (WikiPage wikiPage2 in this.Children.Cache.Cached)
    {
      if (this.Children.Cache.GetStatus((object) wikiPage2) != PXEntryStatus.Deleted)
      {
        if (wikiPage1 != null)
        {
          double? number1 = wikiPage2.Number;
          double? number2 = wikiPage1.Number;
          if (!(number1.GetValueOrDefault() > number2.GetValueOrDefault() & number1.HasValue & number2.HasValue))
            continue;
        }
        wikiPage1 = wikiPage2;
      }
    }
    WikiPage wikiPage3 = row;
    double? number = wikiPage1.Number;
    double num = 1.0;
    double? nullable = number.HasValue ? new double?(number.GetValueOrDefault() + num) : new double?();
    wikiPage3.Number = nullable;
    row.ParentUID = wikiPage1.ParentUID;
  }

  internal void WikiPage_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    WikiPage row = (WikiPage) e.Row;
    if (e.Operation != PXDBOperation.Update)
      return;
    int? statusId = row.StatusID;
    int num = 4;
    if (!(statusId.GetValueOrDefault() == num & statusId.HasValue))
      return;
    PXDatabase.Delete<WikiPage>((PXDataFieldRestrict) new PXDataFieldRestrict<WikiPage.parentUID>((object) row.PageID));
    WikiActions.RemoveArticleForever(row.PageID);
    this.SelectTimeStamp();
  }

  internal void WikiPage_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus != PXTranStatus.Completed || (e.Operation & PXDBOperation.Delete) == PXDBOperation.Select)
      return;
    PXSiteMap.WikiProvider.Clear();
    PXAccess.Clear();
  }

  private List<WikiPage> GetWikiPages(Guid? parent)
  {
    List<WikiPage> wikiPages = new List<WikiPage>();
    WikiPage current = this.Children.Current;
    if (parent.HasValue)
    {
      Guid? nullable = parent;
      Guid nodeId = PXSiteMap.WikiProvider.RootNode.NodeID;
      if ((nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() == nodeId ? 1 : 0) : 1) : 0) == 0)
      {
        foreach (PXResult<WikiPage> pxResult in PXSelectBase<WikiPage, PXSelect<WikiPage, Where<WikiPage.parentUID, Equal<Required<WikiPage.parentUID>>, And<WikiPage.statusID, NotEqual<WikiPageStatus.deleted>>>>.Config>.Select((PXGraph) this, (object) parent))
        {
          WikiPage wikiPage = (WikiPage) pxResult;
          PXSiteMapNode siteMapNodeFromKey = PXSiteMap.WikiProvider.FindSiteMapNodeFromKey(wikiPage.PageID.Value);
          wikiPage.Title = siteMapNodeFromKey == null ? wikiPage.Name : siteMapNodeFromKey.Title;
          wikiPages.Add(wikiPage);
        }
        if (current != null)
          this.Children.Current = current;
        this.Children.Cache.AllowDelete = true;
        this.Children.Cache.AllowInsert = true;
        return wikiPages;
      }
    }
    return wikiPages;
  }

  private void RecursivelyMarkDeleted(WikiPage map)
  {
    foreach (PXResult<WikiPage> pxResult in PXSelectBase<WikiPage, PXSelect<WikiPage, Where<WikiPage.parentUID, Equal<Required<WikiPage.parentUID>>>>.Config>.Select((PXGraph) this, (object) map.PageID))
    {
      WikiPage wikiPage = (WikiPage) pxResult;
      wikiPage.StatusID = new int?(4);
      this.RecursivelyMarkDeleted((WikiPage) this.Caches[typeof (WikiPage)].Update((object) wikiPage));
    }
  }
}
