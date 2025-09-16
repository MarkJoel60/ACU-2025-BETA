// Decompiled with JetBrains decompiler
// Type: PX.Data.RichTextEdit.RichLookupGraph
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.RichTextEdit;

[Serializable]
public class RichLookupGraph : PXGraph<RichLookupGraph>
{
  public PXSelect<SuggestionItem> AllEntities;
  public PXSelect<SuggestionItem> Images;

  public IEnumerable allentities()
  {
    string empty = string.Empty;
    foreach (PXFilterRow filter in PXView.Filters)
    {
      if (filter.DataField == "Caption")
        empty = filter.Value.ToString();
    }
    return (IEnumerable) this.GetAll(empty);
  }

  public IEnumerable images()
  {
    string empty = string.Empty;
    foreach (PXFilterRow filter in PXView.Filters)
    {
      if (filter.DataField == "Caption")
        empty = filter.Value.ToString();
    }
    return (IEnumerable) this.GetImages(empty);
  }

  private IEnumerable InitSearch<T>(
    string viewName,
    string field,
    string searchValue,
    bool skipHints,
    PXCondition cond = PXCondition.LIKE)
    where T : PXGraph, new()
  {
    T obj = new T();
    PXFilterRow[] filters = new PXFilterRow[1]
    {
      new PXFilterRow(field, cond, (object) searchValue)
    };
    int startRow = PXView.StartRow;
    int maxValue = int.MaxValue;
    return obj.ExecuteSelect(viewName, (object[]) null, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, filters, ref startRow, PXView.MaximumRows, ref maxValue, skipHints);
  }

  private List<SuggestionItem> GetFiles(string searchValue)
  {
    List<SuggestionItem> files = new List<SuggestionItem>();
    foreach (UploadFile uploadFile in this.InitSearch<UploadFileRichText>("Files", "ShortName", searchValue, true, PXCondition.RLIKE))
      files.Add(new SuggestionItem(SuggestionItem.TypeEnum.File, uploadFile.Name, EntityFile.ShortenName(uploadFile.Name)));
    return files;
  }

  private List<SuggestionItem> GetImages(string searchValue)
  {
    List<SuggestionItem> images = new List<SuggestionItem>();
    foreach (UploadFile uploadFile in this.InitSearch<UploadFileRichText>("Images", "ShortName", searchValue, true, PXCondition.RLIKE))
    {
      string lower = EntityFile.ShortenName(uploadFile.Name).ToLower();
      if (lower.Contains(searchValue.ToLower()))
        images.Add(new SuggestionItem(SuggestionItem.TypeEnum.Image, uploadFile.Name, lower));
    }
    return images;
  }

  private List<SuggestionItem> GetArticles(string searchValue)
  {
    List<SuggestionItem> articles = new List<SuggestionItem>();
    foreach (WikiPage2 wikiPage2 in this.InitSearch<WikiRichText>("PagesSimple", "Title", searchValue, false))
      articles.Add(new SuggestionItem(SuggestionItem.TypeEnum.Article, wikiPage2.PageID.ToString(), wikiPage2.Title));
    return articles;
  }

  private List<SuggestionItem> GetScreens(string searchValue)
  {
    List<SuggestionItem> screens = new List<SuggestionItem>();
    PXSiteMapProvider provider = PXSiteMap.Provider;
    foreach (PXSiteMapNode pxSiteMapNode in string.IsNullOrEmpty(searchValue) ? provider.Definitions.Nodes : provider.Definitions.Nodes.Where<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (node => node.Title != null && node.Title.ToLower().Contains(searchValue))))
      screens.Add(new SuggestionItem(SuggestionItem.TypeEnum.Screen, pxSiteMapNode.ScreenID, pxSiteMapNode.Title));
    return screens;
  }

  private List<SuggestionItem> GetAll(string query)
  {
    return this.Interleave<SuggestionItem>((IEnumerable<SuggestionItem>) this.GetImages(query), (IEnumerable<SuggestionItem>) this.GetFiles(query), (IEnumerable<SuggestionItem>) this.GetArticles(query), (IEnumerable<SuggestionItem>) this.GetScreens(query)).ToList<SuggestionItem>();
  }

  private IEnumerable<T> Interleave<T>(params IEnumerable<T>[] sequences)
  {
    List<T> objList = new List<T>();
    List<IEnumerator<T>> list1 = ((IEnumerable<IEnumerable<T>>) sequences).Select<IEnumerable<T>, IEnumerator<T>>((Func<IEnumerable<T>, IEnumerator<T>>) (list => list.GetEnumerator())).ToList<IEnumerator<T>>();
    List<IEnumerator<T>> second = new List<IEnumerator<T>>();
    while (list1.Count > 0)
    {
      foreach (IEnumerator<T> enumerator in list1)
      {
        if (enumerator.MoveNext())
          objList.Add(enumerator.Current);
        else
          second.Add(enumerator);
      }
      list1 = list1.Except<IEnumerator<T>>((IEnumerable<IEnumerator<T>>) second).ToList<IEnumerator<T>>();
      second.Clear();
    }
    return (IEnumerable<T>) objList;
  }
}
