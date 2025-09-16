// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.PXScreenSearch
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;

#nullable disable
namespace PX.Data.Search;

/// <exclude />
public class PXScreenSearch : PXSearchCacheable<ScreenSearchResult>
{
  private PXGraph graph;

  public PXScreenSearch() => this.graph = PXGraph.CreateInstance<PXGraph>();

  protected override CacheResult CreateResult(string query, CancellationToken cancellationToken)
  {
    return this.CreateResult(query, 0, cancellationToken);
  }

  protected override CacheResult CreateResult(
    string query,
    int top,
    CancellationToken cancellationToken)
  {
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    if (string.IsNullOrEmpty(query))
      return (CacheResult) null;
    Match match = new Regex("([a-zA-Z]{2}).([0-9]{2}).([0-9]{2}).([0-9]{2})").Match(query);
    if (match.Success)
      query = match.Groups[1].Value + match.Groups[2].Value + match.Groups[3].Value + match.Groups[4].Value;
    string str = $"%{query}%";
    List<Guid> results = new List<Guid>();
    foreach (PXResult<PX.SM.SiteMap> pxResult in PXSelectBase<PX.SM.SiteMap, PXSelect<PX.SM.SiteMap, Where<PX.SM.SiteMap.title, Like<Required<PX.SM.SiteMap.title>>, Or<PX.SM.SiteMap.screenID, Like<Required<PX.SM.SiteMap.title>>>>>.Config>.Select(this.graph, (object) str, (object) str))
    {
      PX.SM.SiteMap siteMap = (PX.SM.SiteMap) pxResult;
      PXSiteMapProvider provider = PXSiteMap.Provider;
      Guid? nodeId = siteMap.NodeID;
      Guid key = nodeId.Value;
      if (provider.FindSiteMapNodeFromKey(key) != null)
      {
        List<Guid> guidList = results;
        nodeId = siteMap.NodeID;
        Guid guid = nodeId.Value;
        guidList.Add(guid);
      }
    }
    stopwatch.Stop();
    return (CacheResult) new ScreenCacheResult(query, results);
  }

  protected override List<ScreenSearchResult> CreateSearchResult(
    CacheResult cacheRes,
    int first,
    int count,
    CancellationToken cancellationToken)
  {
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    bool flag = false;
    if (first < 0)
    {
      first = System.Math.Abs(first) - 1;
      flag = true;
    }
    List<ScreenSearchResult> searchResult = new List<ScreenSearchResult>();
    if (!(cacheRes is ScreenCacheResult screenCacheResult))
      return searchResult;
    List<Guid> results = screenCacheResult.Results;
    int index = first;
    int num = 0;
    while (index < screenCacheResult.Results.Count && index >= 0 && num < count)
    {
      PXSiteMapNode pxSiteMapNode = PXSiteMap.Provider.FindSiteMapNodeFromKey(results[index]);
      if (pxSiteMapNode?.SelectedUI == "E")
      {
        string str1 = pxSiteMapNode.ScreenID;
        if (str1 != null && str1.Length > 7)
        {
          if (str1.IndexOf("000000") > -1 && pxSiteMapNode.Url == "~/Frames/Default.aspx")
            pxSiteMapNode.Url = $"{pxSiteMapNode.Url}?scrid={pxSiteMapNode.ScreenID}";
          str1 = str1.Insert(6, ".").Insert(4, ".").Insert(2, ".");
        }
        string str2 = $"{pxSiteMapNode.Title} ({str1})";
        string url = pxSiteMapNode.Url;
        string path = pxSiteMapNode.Title;
        for (; pxSiteMapNode.ParentNode != null && pxSiteMapNode.ParentNode.Key != pxSiteMapNode.Provider.RootNode.Key; pxSiteMapNode = pxSiteMapNode.ParentNode)
          path = $"{pxSiteMapNode.ParentNode.Title} > {path}";
        if (str1 != null && str1.Length > 7 && !string.IsNullOrEmpty(pxSiteMapNode.Url))
        {
          ScreenSearchResult screenSearchResult = new ScreenSearchResult(url, TextUtils.Emphasize(str2, cacheRes.Query), path);
          searchResult.Add(screenSearchResult);
          ++num;
        }
      }
      if (flag)
        --index;
      else
        ++index;
    }
    this.totalCount = screenCacheResult.Results.Count;
    if (flag)
    {
      this.PreviousIndex = index + 1;
      this.NextIndex = first + 1;
      this.HasPrevPage = index + 1 > 0;
    }
    else
    {
      this.PreviousIndex = first;
      this.NextIndex = index;
      this.HasPrevPage = first > 0;
      this.HasNextPage = index < screenCacheResult.Results.Count;
    }
    stopwatch.Stop();
    if (flag)
      searchResult.Reverse();
    return searchResult;
  }
}
