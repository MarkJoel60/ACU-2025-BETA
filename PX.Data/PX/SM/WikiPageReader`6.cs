// Decompiled with JetBrains decompiler
// Type: PX.SM.WikiPageReader`6
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace PX.SM;

/// <exclude />
[PXHidden]
public class WikiPageReader<Base, BasePageID, BaseWikiID, BaseName, Primary, Where> : 
  PXGraph<WikiPageReader<Base, BasePageID, BaseWikiID, BaseName, Primary, Where>>
  where Base : WikiPage, new()
  where BasePageID : IBqlField
  where BaseWikiID : IBqlField
  where BaseName : IBqlField
  where Primary : class, IBqlTable, new()
  where Where : class, IBqlWhere, new()
{
  [PXHidden]
  public PXSelect<Users> BaseUsers;
  public PXFilter<WikiPageFilter> Filter;
  public WikiPageReader<Base, BasePageID, BaseWikiID, BaseName, Primary, Where>.PXSelectPage Pages;
  public PXSelect<WikiRevision, PX.Data.Where<WikiRevision.pageID, Equal<Current<BasePageID>>, And<WikiRevision.language, Equal<Required<WikiRevision.language>>>>, OrderBy<Desc<WikiRevision.pageRevisionID>>> Revisions;
  public PXSelect<SPWikiCategoryTags, PX.Data.Where<SPWikiCategoryTags.pageID, Equal<Current<BasePageID>>>> Category;
  public PXSelect<SPWikiProductTags, PX.Data.Where<SPWikiProductTags.pageID, Equal<Current<BasePageID>>>> Product;

  protected IEnumerable revisions()
  {
    WikiPageReader<Base, BasePageID, BaseWikiID, BaseName, Primary, Where> wikiPageReader = this;
    WikiPageFilter current1 = wikiPageReader.Filter.Current;
    if (current1 != null)
    {
      bool? showRevisions = current1.ShowRevisions;
      bool flag = true;
      if (showRevisions.GetValueOrDefault() == flag & showRevisions.HasValue)
      {
        WikiPage current2 = (WikiPage) wikiPageReader.Pages.Current;
        WikiPageReader<Base, BasePageID, BaseWikiID, BaseName, Primary, Where> graph = wikiPageReader;
        object[] objArray = new object[1]
        {
          (object) current2?.Language
        };
        foreach (PXResult<WikiRevision> pxResult in PXSelectBase<WikiRevision, PXSelect<WikiRevision, PX.Data.Where<WikiRevision.pageID, Equal<Current<BasePageID>>, And<WikiRevision.language, Equal<Required<WikiRevision.language>>>>, OrderBy<Desc<WikiRevision.pageRevisionID>>>.Config>.Select((PXGraph) graph, objArray))
          yield return (object) (WikiRevision) pxResult;
      }
    }
  }

  public WikiPageReader() => this.Initialize();

  public bool SelectMode { get; set; }

  private void Initialize()
  {
    this.RowSelected.AddHandler(typeof (Base), new PXRowSelected(this.OnRowSelected));
    this.Views.Caches.Insert(2, typeof (WikiPageLanguage));
  }

  public override void Persist()
  {
    if (this.IsReadOnly)
      return;
    base.Persist();
  }

  protected virtual bool IsReadOnly => true;

  protected void SPWikiCategoryTags_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is SPWikiCategoryTags row) || !(this.Pages.Cache.Current is WikiPage current))
      return;
    WikiPageLanguage wikiPageLanguage = (WikiPageLanguage) PXSelectBase<WikiPageLanguage, PXSelect<WikiPageLanguage, PX.Data.Where<WikiPageLanguage.pageID, Equal<Required<WikiPageLanguage.pageID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) current.PageID);
    row.PageID = current.PageID;
    row.PageName = current.Name;
    row.CreatedByID = current.CreatedByID;
    row.CreatedDateTime = current.CreatedDateTime;
    row.LastModifiedByID = current.LastModifiedByID;
    row.LastModifiedDateTime = current.LastModifiedDateTime;
    row.PageTitle = wikiPageLanguage != null ? wikiPageLanguage.Title : current.Title;
  }

  protected void SPWikiProductTags_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is SPWikiProductTags row) || !(this.Pages.Cache.Current is WikiPage current))
      return;
    WikiPageLanguage wikiPageLanguage = (WikiPageLanguage) PXSelectBase<WikiPageLanguage, PXSelect<WikiPageLanguage, PX.Data.Where<WikiPageLanguage.pageID, Equal<Required<WikiPageLanguage.pageID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) current.PageID);
    row.PageID = current.PageID;
    row.PageName = current.Name;
    row.CreatedByID = current.CreatedByID;
    row.CreatedDateTime = current.CreatedDateTime;
    row.LastModifiedByID = current.LastModifiedByID;
    row.LastModifiedDateTime = current.LastModifiedDateTime;
    row.PageTitle = wikiPageLanguage != null ? wikiPageLanguage.Title : current.Title;
  }

  protected void WikiPageFilter_Language_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) Thread.CurrentThread.CurrentUICulture.Name;
  }

  protected PXWikiRights CurrentAccessRights(WikiPage row)
  {
    if (this.SelectMode)
      return PXWikiRights.Select;
    Guid? wikiId = row.WikiID;
    Guid empty = Guid.Empty;
    if ((wikiId.HasValue ? (wikiId.HasValue ? (wikiId.GetValueOrDefault() == empty ? 1 : 0) : 1) : 0) != 0 && !row.ParentUID.HasValue)
      return PXWikiRights.Delete;
    PXWikiRights pxWikiRights = PXWikiRights.Denied;
    bool flag = false;
    PXWikiMapNode node = row.PageID.With<Guid?, PXWikiMapNode>((Func<Guid?, PXWikiMapNode>) (_ => (PXWikiMapNode) PXSiteMap.WikiProvider.FindSiteMapNodeFromKey(_.Value)));
    if (node != null)
    {
      pxWikiRights = ((PXWikiProvider.WikiDefinition) PXSiteMap.WikiProvider.Definitions).GetAccessRights(node);
      flag = true;
    }
    if (!flag)
      pxWikiRights = !row.WikiID.HasValue ? PXWikiRights.Delete : (row.ParentUID.HasValue ? PXSiteMap.WikiProvider.GetAccessRights(row.ParentUID.Value) : PXSiteMap.WikiProvider.GetAccessRights(row.WikiID.Value));
    return pxWikiRights;
  }

  private void OnRowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    this.OnRowSelected(sender, e.Row as WikiPage);
  }

  private void OnRowSelected(PXCache sender, WikiPage row)
  {
    if (row == null)
      return;
    this.OnPageSelected(sender, row, this.CurrentAccessRights(row));
  }

  protected virtual void OnPageSelected(PXCache sender, WikiPage row, PXWikiRights accessRights)
  {
    if (row.Language == null)
      row.Language = this.Filter.Current == null || this.Filter.Current.Language == null || !this.LanguageAvailable(this.Filter.Current.Language, row.WikiID, accessRights) ? Thread.CurrentThread.CurrentCulture.Name : this.Filter.Current.Language;
    int? nullable = row.PageRevisionID;
    if (nullable.HasValue)
      return;
    WikiPageLanguage wikiPageLanguage = this.ReadLanguage(row);
    if (wikiPageLanguage != null)
    {
      row.Title = wikiPageLanguage.Title;
      row.Summary = wikiPageLanguage.Summary;
      row.Keywords = wikiPageLanguage.Keywords;
      row.PublishedDateTime = wikiPageLanguage.LastPublishedDateTime;
      int valueOrDefault;
      if (this.Filter.Current != null)
      {
        nullable = this.Filter.Current.PageRevisionID;
        if (nullable.HasValue)
        {
          nullable = this.Filter.Current.PageRevisionID;
          valueOrDefault = nullable.GetValueOrDefault();
          goto label_8;
        }
      }
      nullable = wikiPageLanguage.LastRevisionID;
      valueOrDefault = nullable.GetValueOrDefault();
label_8:
      int revisionID = valueOrDefault;
      if (accessRights <= PXWikiRights.Select)
      {
        nullable = wikiPageLanguage.LastPublishedID;
        revisionID = nullable.GetValueOrDefault();
      }
      WikiRevision wikiRevision = this.OnReadRevision(row, wikiPageLanguage.Language, revisionID);
      if (wikiRevision != null)
      {
        row.Content = wikiRevision.Content;
        row.PageRevisionID = wikiRevision.PageRevisionID;
        row.PageRevisionDateTime = wikiRevision.CreatedDateTime;
        row.PageRevisionCreatedByID = wikiRevision.CreatedByID;
      }
      else
        row.PageRevisionID = new int?(revisionID);
    }
    else
    {
      row.PageRevisionID = new int?(0);
      row.PageRevisionDateTime = new System.DateTime?();
      row.PageRevisionCreatedByID = new Guid?();
    }
    PXWikiRights pxWikiRights = this.CurrentAccessRights(row);
    sender.AllowInsert = pxWikiRights >= PXWikiRights.Insert;
    sender.AllowUpdate = pxWikiRights >= PXWikiRights.Update;
    sender.AllowDelete = pxWikiRights >= PXWikiRights.Delete;
  }

  protected virtual WikiRevision OnReadRevision(WikiPage page, string lang, int revisionID)
  {
    PXSelect<WikiRevision, PX.Data.Where<WikiRevision.pageID, Equal<Required<WikiRevision.pageID>>, And<WikiRevision.pageRevisionID, Equal<Required<WikiRevision.pageRevisionID>>, And<WikiRevision.language, Equal<Required<WikiRevision.language>>>>>> pxSelect = new PXSelect<WikiRevision, PX.Data.Where<WikiRevision.pageID, Equal<Required<WikiRevision.pageID>>, And<WikiRevision.pageRevisionID, Equal<Required<WikiRevision.pageRevisionID>>, And<WikiRevision.language, Equal<Required<WikiRevision.language>>>>>>((PXGraph) this);
    WikiRevision wikiRevision = (WikiRevision) pxSelect.Select((object) page.PageID, (object) revisionID, (object) lang);
    if (wikiRevision != null)
      return wikiRevision;
    return (WikiRevision) pxSelect.Select((object) page.PageID, (object) revisionID, (object) "en-US");
  }

  protected WikiPageLanguage ReadLanguage(WikiPage row)
  {
    PXSelectBase<WikiPageLanguage> pxSelectBase = (PXSelectBase<WikiPageLanguage>) new PXSelect<WikiPageLanguage, PX.Data.Where<WikiPageLanguage.pageID, Equal<Required<WikiPageLanguage.pageID>>, And<WikiPageLanguage.language, Equal<Required<WikiPageLanguage.language>>>>>((PXGraph) this);
    pxSelectBase.View.Clear();
    WikiPageLanguage wikiPageLanguage = (WikiPageLanguage) pxSelectBase.Select((object) row.PageID, (object) row.Language);
    if (string.Compare(row.Language, "en-US", true) == 0 && wikiPageLanguage == null)
      return (WikiPageLanguage) PXSelectBase<WikiPageLanguage, PXSelect<WikiPageLanguage, PX.Data.Where<WikiPageLanguage.pageID, Equal<Required<WikiRevision.pageID>>>>.Config>.Select((PXGraph) this, (object) row.PageID);
    if (wikiPageLanguage != null || string.Compare(row.Language, "en-US", true) == 0)
      return wikiPageLanguage;
    return (WikiPageLanguage) pxSelectBase.Select((object) row.PageID, (object) "en-US");
  }

  protected bool LanguageAvailable(string language, Guid? wiki, PXWikiRights rights)
  {
    if (rights > PXWikiRights.Select)
      return true;
    return (WikiReadLanguage) PXSelectBase<WikiReadLanguage, PXSelect<WikiReadLanguage, PX.Data.Where<WikiReadLanguage.localeID, Equal<Required<WikiReadLanguage.localeID>>, And<WikiReadLanguage.wikiID, Equal<Required<WikiReadLanguage.wikiID>>>>>.Config>.Select((PXGraph) this, (object) language, (object) wiki) != null;
  }

  protected WikiDescriptor ReadWikiDescriptor(WikiPage row)
  {
    Guid? wikiId = row.WikiID;
    Guid empty = Guid.Empty;
    if ((wikiId.HasValue ? (wikiId.HasValue ? (wikiId.GetValueOrDefault() != empty ? 1 : 0) : 0) : 1) == 0)
      return (WikiDescriptor) row;
    return (WikiDescriptor) PXSelectBase<WikiDescriptor, PXSelect<WikiDescriptor, PX.Data.Where<WikiDescriptor.pageID, Equal<Required<WikiDescriptor.pageID>>>>.Config>.Select((PXGraph) this, (object) row.WikiID);
  }

  public string ConvertGuidToLink(string wikiText) => this.ConvertGuidToLink(wikiText, new Guid?());

  public string ConvertGuidToLink(string wikiText, Guid? wikiID)
  {
    return new GuidToLink((PXGraph) this, wikiText, wikiID).Result;
  }

  public string ConvertLinkToGuid(string wikiText) => this.ConvertLinkToGuid(wikiText, new Guid?());

  public string ConvertLinkToGuid(string wikiText, Guid? wikiID)
  {
    return new LinkToGuid((PXGraph) this, wikiText, wikiID).Result;
  }

  public WikiPage ReadPage()
  {
    WikiPage row = (WikiPage) (Base) this.Pages.SelectWindowed(0, 1);
    this.OnRowSelected(this.Pages.Cache, row);
    return row;
  }

  /// <exclude />
  public class PXSelectPage : PXSelectBase<Base>
  {
    public PXSelectPage(PXGraph graph)
    {
      this.View = (PXView) new WikiPageReader<Base, BasePageID, BaseWikiID, BaseName, Primary, Where>.PXSelectPage.ViewPage(graph);
    }

    public PXSelectPage(PXGraph graph, Delegate handler)
    {
      this.View = (PXView) new WikiPageReader<Base, BasePageID, BaseWikiID, BaseName, Primary, Where>.PXSelectPage.ViewPage(graph, handler);
    }

    /// <exclude />
    private class ViewPage : PXView
    {
      public ViewPage(PXGraph graph, Delegate handler)
        : base(graph, false, (BqlCommand) new Select2<Base, LeftJoin<WikiPageLanguage, On<WikiPageLanguage.pageID, Equal<BasePageID>, And<WikiPageLanguage.language, Equal<Current<WikiPageFilter.language>>>>>>(), handler)
      {
      }

      public ViewPage(PXGraph graph)
        : base(graph, false, (BqlCommand) new Select2<Base, LeftJoin<WikiPageLanguage, On<WikiPageLanguage.pageID, Equal<BasePageID>, And<WikiPageLanguage.language, Equal<Current<WikiPageFilter.language>>>>>>())
      {
      }

      public override List<object> Select(
        object[] currents,
        object[] parameters,
        object[] searches,
        string[] sortcolumns,
        bool[] descendings,
        PXFilterRow[] filters,
        ref int startRow,
        int maximumRows,
        ref int totalRows)
      {
        List<object> objectList = new List<object>();
        WikiPageFilter current = (WikiPageFilter) this.Graph.Caches[typeof (WikiPageFilter)].Current;
        if (current == null)
          return objectList;
        WikiPageFilter copy = PXCache<WikiPageFilter>.CreateCopy(current);
        try
        {
          object obj = (object) null;
          if (searches != null)
          {
            PXCache cach = this.Graph.Caches[typeof (WikiPageFilter)];
            for (int index = 0; index < searches.Length; ++index)
            {
              object search = searches[index];
              if (string.Compare(sortcolumns[index], typeof (BaseName).Name, true) == 0)
                obj = search;
              if (search != null)
                cach.SetValueExt((object) current, sortcolumns[index], search);
            }
          }
          sortcolumns = new string[1]
          {
            typeof (BaseName).Name
          };
          searches = new object[1]{ obj };
          Guid? nullable = current.PageID;
          if (nullable.HasValue)
          {
            this.WhereNew<Where2<Where, And<BasePageID, Equal<Current<WikiPageFilter.pageID>>>>>();
            searches = (object[]) null;
          }
          else
          {
            if (current.Art == null)
              return objectList;
            if (current.Wiki != null)
            {
              WikiPage wikiPage = (WikiPage) (WikiDescriptor) PXSelectBase<WikiDescriptor, PXSelect<WikiDescriptor, PX.Data.Where<WikiDescriptor.name, Equal<Current<WikiPageFilter.wiki>>>>.Config>.Select(this.Graph);
              if (wikiPage != null)
                current.WikiID = wikiPage.PageID;
            }
            nullable = current.WikiID;
            if (!nullable.HasValue)
              return objectList;
            this.WhereNew<Where2<Where, And<BaseWikiID, Equal<Current<WikiPageFilter.wikiID>>, And<BaseName, Equal<Current<WikiPageFilter.art>>>>>>();
          }
          bool flag = false;
          object[] currents1 = new object[1]
          {
            (object) current
          };
          foreach (PXResult<Base, WikiPageLanguage> pxResult in base.Select(currents1, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows))
          {
            flag = true;
            Base @base = (Base) pxResult;
            if ((object) @base != null)
            {
              nullable = @base.PageID;
              if (nullable.HasValue)
              {
                if (this.Graph.Caches[typeof (Base)].GetStatus((object) @base) == PXEntryStatus.Inserted)
                  objectList.Add((object) @base);
                else if (@base.Name.IndexOf("Template:") > -1)
                {
                  objectList.Add((object) @base);
                }
                else
                {
                  PXWikiProvider wikiProvider = PXSiteMap.WikiProvider;
                  nullable = @base.PageID;
                  Guid pageID = nullable.Value;
                  if (wikiProvider.GetAccessRights(pageID) >= PXWikiRights.Select)
                    objectList.Add((object) @base);
                }
              }
            }
          }
          if (!flag && current.Art == null)
          {
            current.Art = "Deleted";
            WikiPage wikiPage = (WikiPage) PXSelectBase<WikiPage, PXSelect<WikiPage, PX.Data.Where<WikiPage.statusID, Equal<WikiPageStatus.deleted>, And<WikiPage.name, Equal<Required<WikiPage.name>>, And<WikiPage.pageID, Equal<Required<WikiPage.pageID>>>>>>.Config>.Select(this._Graph, (object) current.Art, (object) current.PageID);
            if (wikiPage != null)
            {
              Guid? pageId = wikiPage.PageID;
              if (pageId.HasValue)
              {
                PXWikiProvider wikiProvider = PXSiteMap.WikiProvider;
                pageId = wikiPage.PageID;
                Guid pageID = pageId.Value;
                if (wikiProvider.GetAccessRights(pageID) >= PXWikiRights.Select)
                  objectList.Add((object) wikiPage);
              }
            }
          }
          return objectList;
        }
        catch
        {
          PXCache<WikiPageFilter>.RestoreCopy(current, copy);
          return objectList;
        }
      }
    }
  }
}
