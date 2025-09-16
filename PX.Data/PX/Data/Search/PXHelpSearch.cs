// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.PXHelpSearch
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

#nullable disable
namespace PX.Data.Search;

/// <exclude />
public class PXHelpSearch : PXSearchCacheable<HelpSearchResult>
{
  private const int QuerySize = 100;
  private const int ArticleSummaryLength = 600;
  private PXGraph graph;

  public Guid? WikiID { get; set; }

  private BqlFullTextRenderingMethod FullTextCapability
  {
    get => PXDatabase.Provider.GetFullTextSearchCapability<WikiRevision.plainText>();
  }

  public bool IsFullText() => this.FullTextCapability != BqlFullTextRenderingMethod.NeutralLike;

  public PXHelpSearch() => this.graph = PXGraph.CreateInstance<PXGraph>();

  internal IEnumerable<System.Type> IndexTablesUsedBySearch
  {
    get
    {
      return (IEnumerable<System.Type>) new System.Type[1]
      {
        typeof (WikiRevision)
      };
    }
  }

  protected override CacheResult CreateResult(string query, CancellationToken cancellationToken)
  {
    return this.CreateResult(query, 100, cancellationToken);
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
    List<object> objectList1 = new List<object>();
    List<object> objectList2 = new List<object>();
    PXResultset<WikiRevision> pxResultset;
    if (this.IsFullText())
    {
      PXSelectBase<WikiRevision> pxSelectBase1;
      PXSelectBase<WikiRevision> pxSelectBase2;
      if (PXEntitySearch.IsExactMatch(query))
      {
        pxSelectBase1 = (PXSelectBase<WikiRevision>) new PXSelectReadonly2<WikiRevision, InnerJoin<WikiPageLanguage, On<WikiRevision.pageID, Equal<WikiPageLanguage.pageID>, And<WikiPageLanguage.lastRevisionID, Equal<WikiRevision.pageRevisionID>, And<WikiPageLanguage.language, Equal<WikiRevision.language>>>>, InnerJoin<WikiPage, On<WikiRevision.pageID, Equal<WikiPage.pageID>>, InnerJoin<WikiDescriptor, On<WikiPage.wikiID, Equal<WikiDescriptor.pageID>>>>>, Where<Contains<WikiRevision.plainText, Required<WikiRevision.plainText>, WikiRevision.uID, Required<SearchIndex.top>>>, OrderBy<Desc<RankOf<WikiRevision.plainText>>>>(this.graph);
        pxSelectBase2 = (PXSelectBase<WikiRevision>) new PXSelectReadonly2<WikiRevision, InnerJoin<WikiPageLanguage, On<WikiRevision.pageID, Equal<WikiPageLanguage.pageID>, And<WikiPageLanguage.lastRevisionID, Equal<WikiRevision.pageRevisionID>, And<WikiPageLanguage.language, Equal<WikiRevision.language>>>>, InnerJoin<WikiPage, On<WikiRevision.pageID, Equal<WikiPage.pageID>>, InnerJoin<WikiDescriptor, On<WikiPage.wikiID, Equal<WikiDescriptor.pageID>>>>>, Where<Contains<WikiRevision.plainText, Required<WikiRevision.plainText>, WikiRevision.uID>>, OrderBy<Desc<RankOf<WikiRevision.plainText>>>>(this.graph);
        objectList1.Add((object) query);
        objectList2.Add((object) query);
      }
      else
      {
        pxSelectBase1 = (PXSelectBase<WikiRevision>) new PXSelectReadonly2<WikiRevision, InnerJoin<WikiPageLanguage, On<WikiRevision.pageID, Equal<WikiPageLanguage.pageID>, And<WikiPageLanguage.lastRevisionID, Equal<WikiRevision.pageRevisionID>, And<WikiPageLanguage.language, Equal<WikiRevision.language>>>>, InnerJoin<WikiPage, On<WikiRevision.pageID, Equal<WikiPage.pageID>>, InnerJoin<WikiDescriptor, On<WikiPage.wikiID, Equal<WikiDescriptor.pageID>>>>>, Where<FreeText<WikiRevision.plainText, Required<WikiRevision.plainText>, WikiRevision.uID, Required<SearchIndex.top>>>, OrderBy<Desc<RankOf<WikiRevision.plainText>>>>(this.graph);
        pxSelectBase2 = (PXSelectBase<WikiRevision>) new PXSelectReadonly2<WikiRevision, InnerJoin<WikiPageLanguage, On<WikiRevision.pageID, Equal<WikiPageLanguage.pageID>, And<WikiPageLanguage.lastRevisionID, Equal<WikiRevision.pageRevisionID>, And<WikiPageLanguage.language, Equal<WikiRevision.language>>>>, InnerJoin<WikiPage, On<WikiRevision.pageID, Equal<WikiPage.pageID>>, InnerJoin<WikiDescriptor, On<WikiPage.wikiID, Equal<WikiDescriptor.pageID>>>>>, Where<FreeText<WikiRevision.plainText, Required<WikiRevision.plainText>, WikiRevision.uID>>, OrderBy<Desc<RankOf<WikiRevision.plainText>>>>(this.graph);
        string str = query;
        if (this.FullTextCapability == BqlFullTextRenderingMethod.MySqlMatchAgainst)
          str = PXEntitySearch.EmulateFreeTextOnMySql(query);
        objectList1.Add((object) str);
        objectList2.Add((object) str);
      }
      if (PXSiteMap.IsPortal)
        pxSelectBase2.Join<InnerJoin<PX.SM.PortalMap, On<WikiDescriptor.pageID, Equal<PX.SM.PortalMap.nodeID>>>>();
      else
        pxSelectBase2.Join<InnerJoin<PX.SM.SiteMap, On<WikiDescriptor.pageID, Equal<PX.SM.SiteMap.nodeID>>>>();
      objectList2.Add((object) top);
      if (this.WikiID.HasValue)
      {
        pxSelectBase1.WhereAnd<Where<WikiPage.wikiID, Equal<Required<WikiPage.wikiID>>>>();
        pxSelectBase2.WhereAnd<Where<WikiPage.wikiID, Equal<Required<WikiPage.wikiID>>>>();
        objectList1.Add((object) this.WikiID);
        objectList2.Add((object) this.WikiID);
      }
      else
      {
        pxSelectBase1.WhereAnd<Where<WikiRevision.language, Equal<Required<WikiRevision.language>>, Or<NotExists<Select<WikiRevisionLocalized, Where<WikiRevision.pageID, Equal<WikiRevisionLocalized.pageID>, And<WikiRevisionLocalized.language, Equal<Required<WikiRevisionLocalized.language>>>>>>>>>();
        objectList2.Add((object) Thread.CurrentThread.CurrentCulture.Name);
        objectList2.Add((object) Thread.CurrentThread.CurrentCulture.Name);
        pxSelectBase2.WhereAnd<Where<WikiRevision.language, Equal<Required<WikiRevision.language>>, Or<NotExists<Select<WikiRevisionLocalized, Where<WikiRevision.pageID, Equal<WikiRevisionLocalized.pageID>, And<WikiRevisionLocalized.language, Equal<Required<WikiRevisionLocalized.language>>>>>>>>>();
        objectList1.Add((object) Thread.CurrentThread.CurrentCulture.Name);
        objectList1.Add((object) Thread.CurrentThread.CurrentCulture.Name);
      }
      cancellationToken.ThrowIfCancellationRequested();
      using (new PXFieldScope(pxSelectBase1.View, new System.Type[2]
      {
        typeof (WikiRevision.uID),
        typeof (WikiRevision.language)
      }))
        pxResultset = pxSelectBase1.SelectWindowed(0, top, objectList2.ToArray());
      cancellationToken.ThrowIfCancellationRequested();
      if (pxResultset.Count < 100)
      {
        using (new PXFieldScope(pxSelectBase2.View, new System.Type[2]
        {
          typeof (WikiRevision.uID),
          typeof (WikiRevision.language)
        }))
          pxResultset = pxSelectBase2.SelectWindowed(0, top, objectList1.ToArray());
      }
    }
    else
    {
      PXSelectBase<WikiRevision> pxSelectBase = (PXSelectBase<WikiRevision>) new PXSelectReadonly2<WikiRevision, InnerJoin<WikiPageLanguage, On<WikiRevision.pageID, Equal<WikiPageLanguage.pageID>, And<WikiPageLanguage.lastRevisionID, Equal<WikiRevision.pageRevisionID>, And<WikiPageLanguage.language, Equal<WikiRevision.language>>>>, InnerJoin<WikiPage, On<WikiRevision.pageID, Equal<WikiPage.pageID>>, InnerJoin<WikiDescriptor, On<WikiPage.wikiID, Equal<WikiDescriptor.pageID>>>>>, Where<WikiRevision.plainText, Like<Required<WikiRevision.plainText>>>>(this.graph);
      if (PXSiteMap.IsPortal)
        pxSelectBase.Join<InnerJoin<PX.SM.PortalMap, On<WikiDescriptor.pageID, Equal<PX.SM.PortalMap.nodeID>>>>();
      else
        pxSelectBase.Join<InnerJoin<PX.SM.SiteMap, On<WikiDescriptor.pageID, Equal<PX.SM.SiteMap.nodeID>>>>();
      objectList1.Add((object) $"%{query}%");
      if (this.WikiID.HasValue)
      {
        pxSelectBase.WhereAnd<Where<WikiPage.wikiID, Equal<Required<WikiPage.wikiID>>>>();
        objectList1.Add((object) this.WikiID);
      }
      else
      {
        pxSelectBase.WhereAnd<Where<WikiRevision.language, Equal<Required<WikiRevision.language>>, Or<NotExists<Select<WikiRevisionLocalized, Where<WikiRevision.pageID, Equal<WikiRevisionLocalized.pageID>, And<WikiRevisionLocalized.language, Equal<Required<WikiRevisionLocalized.language>>>>>>>>>();
        objectList1.Add((object) Thread.CurrentThread.CurrentCulture.Name);
        objectList1.Add((object) Thread.CurrentThread.CurrentCulture.Name);
      }
      cancellationToken.ThrowIfCancellationRequested();
      using (new PXFieldScope(pxSelectBase.View, new System.Type[2]
      {
        typeof (WikiRevision.uID),
        typeof (WikiRevision.language)
      }))
        pxResultset = pxSelectBase.SelectWindowed(0, top, objectList1.ToArray());
    }
    cancellationToken.ThrowIfCancellationRequested();
    List<Guid> results = new List<Guid>(pxResultset.Count);
    List<Guid> localizedResults = new List<Guid>();
    foreach (PXResult<WikiRevision> pxResult in pxResultset)
    {
      WikiRevision wikiRevision = (WikiRevision) pxResult;
      List<Guid> guidList1 = results;
      Guid? uid = wikiRevision.UID;
      Guid guid1 = uid.Value;
      guidList1.Add(guid1);
      if (wikiRevision.Language == Thread.CurrentThread.CurrentCulture.Name)
      {
        List<Guid> guidList2 = localizedResults;
        uid = wikiRevision.UID;
        Guid guid2 = uid.Value;
        guidList2.Add(guid2);
      }
    }
    stopwatch.Stop();
    return (CacheResult) new PXHelpSearch.HelpCacheResult(query, this.WikiID, top, results, localizedResults);
  }

  protected override bool IsValidResult(CacheResult result, string query)
  {
    if (!(result is PXHelpSearch.HelpCacheResult helpCacheResult) || !(helpCacheResult.Query == query) || !(helpCacheResult.SearchType == this.GetType()))
      return false;
    Guid? wikiId1 = helpCacheResult.WikiID;
    Guid? wikiId2 = this.WikiID;
    return (wikiId1.HasValue == wikiId2.HasValue ? (wikiId1.HasValue ? (wikiId1.GetValueOrDefault() == wikiId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0;
  }

  protected override List<HelpSearchResult> CreateSearchResult(
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
    List<HelpSearchResult> searchResult = new List<HelpSearchResult>();
    if (!(cacheRes is PXHelpSearch.HelpCacheResult result1))
      return searchResult;
    List<Guid> results = result1.Results;
    List<Guid> localizedResults = result1.LocalizedResults;
    int index = first;
    int num = 0;
    if (result1.Results.Count == result1.Top && first + count + 1 > result1.Results.Count)
    {
      this.ResetCache();
      result1 = (PXHelpSearch.HelpCacheResult) this.CreateResult(cacheRes.Query, result1.Top * 2, cancellationToken);
      this.SaveCache((CacheResult) result1);
      results = result1.Results;
    }
    while (index < result1.Results.Count && index >= 0 && num < count)
    {
      cancellationToken.ThrowIfCancellationRequested();
      PXResultset<WikiRevision> pxResultset;
      if (localizedResults != null && localizedResults.Contains(results[index]))
        pxResultset = PXSelectBase<WikiRevision, PXSelectJoin<WikiRevision, InnerJoin<WikiPageLanguage, On<WikiRevision.pageID, Equal<WikiPageLanguage.pageID>, And<WikiPageLanguage.lastRevisionID, Equal<WikiRevision.pageRevisionID>, And<WikiRevision.language, Equal<Required<WikiRevision.language>>, And<WikiPageLanguage.language, Equal<WikiRevision.language>>>>>>, Where<WikiRevision.uID, Equal<Required<WikiRevision.uID>>>>.Config>.SelectWindowed(this.graph, 0, 1, (object) Thread.CurrentThread.CurrentCulture.Name, (object) results[index]);
      else
        pxResultset = PXSelectBase<WikiRevision, PXSelectJoin<WikiRevision, InnerJoin<WikiPageLanguage, On<WikiRevision.pageID, Equal<WikiPageLanguage.pageID>, And<WikiPageLanguage.lastRevisionID, Equal<WikiRevision.pageRevisionID>>>>, Where<WikiRevision.uID, Equal<Required<WikiRevision.uID>>>>.Config>.SelectWindowed(this.graph, 0, 1, (object) results[index]);
      if (pxResultset.Count > 0)
      {
        WikiRevision wikiRevision = (WikiRevision) pxResultset[0][typeof (WikiRevision)];
        WikiPageLanguage wikiPageLanguage = (WikiPageLanguage) pxResultset[0][typeof (WikiPageLanguage)];
        if (wikiRevision != null)
        {
          PXWikiProvider wikiProvider1 = PXSiteMap.WikiProvider;
          Guid? nullable = wikiRevision.PageID;
          Guid pageID1 = nullable.Value;
          if (wikiProvider1.GetAccessRights(pageID1) >= PXWikiRights.Select)
          {
            string str = string.Empty;
            PXWikiProvider wikiProvider2 = PXSiteMap.WikiProvider;
            nullable = wikiRevision.PageID;
            Guid key = nullable.Value;
            PXSiteMapNode pxSiteMapNode = wikiProvider2.FindSiteMapNodeFromKey(key);
            if (pxSiteMapNode != null)
            {
              str = pxSiteMapNode.Title;
              for (; pxSiteMapNode.ParentNode != null && pxSiteMapNode.ParentNode.Key != pxSiteMapNode.Provider.RootNode.Key; pxSiteMapNode = pxSiteMapNode.ParentNode)
                str = $"{pxSiteMapNode.ParentNode.Title} > {str}";
            }
            wikiRevision.PlainText = wikiRevision.PlainText.Replace(">", "&gt;");
            wikiRevision.PlainText = wikiRevision.PlainText.Replace("<", "&lt;");
            nullable = wikiRevision.UID;
            Guid id = nullable.Value;
            nullable = wikiRevision.PageID;
            Guid pageID2 = nullable.Value;
            string title = TextUtils.Emphasize(wikiPageLanguage.Title, cacheRes.Query);
            string path = str;
            string articleSummary = this.GetArticleSummary(wikiRevision.PlainText, cacheRes.Query);
            HelpSearchResult helpSearchResult = new HelpSearchResult(id, pageID2, title, path, articleSummary);
            searchResult.Add(helpSearchResult);
            ++num;
          }
        }
      }
      if (flag)
        --index;
      else
        ++index;
      if (result1.Results.Count == result1.Top && index + count + 1 - num > result1.Results.Count)
      {
        this.ResetCache();
        PXHelpSearch.HelpCacheResult result2 = (PXHelpSearch.HelpCacheResult) this.CreateResult(cacheRes.Query, result1.Top * 2, cancellationToken);
        this.SaveCache((CacheResult) result2);
        return this.CreateSearchResult((CacheResult) result2, first, count, cancellationToken);
      }
    }
    this.totalCount = result1.Results.Count;
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
      this.HasNextPage = index < result1.Results.Count;
    }
    stopwatch.Stop();
    if (flag)
      searchResult.Reverse();
    return searchResult;
  }

  private string GetArticleSummary(string plainText, string query)
  {
    MatchCollection matches = Regex.Matches(plainText, TextUtils.ConvertQueryToRegexPatern(query), RegexOptions.IgnoreCase);
    List<string> stringList = new List<string>();
    Dictionary<string, List<int>> dictionary = new Dictionary<string, List<int>>();
    if (matches.Count > 0)
    {
      for (int i = 0; i < matches.Count; ++i)
      {
        string lowerInvariant = matches[i].Value.ToLowerInvariant();
        if (!dictionary.ContainsKey(lowerInvariant))
        {
          dictionary.Add(lowerInvariant, new List<int>());
          stringList.Add(lowerInvariant);
        }
        dictionary[lowerInvariant].Add(i);
      }
    }
    StringBuilder stringBuilder1 = new StringBuilder();
    int startOfSentance1;
    if (dictionary.Count == 0)
      stringBuilder1.Append(this.GetArticleSubSummary(plainText, matches, 0, 600, out startOfSentance1));
    else if (dictionary.Count == 1)
      stringBuilder1.Append(this.GetArticleSubSummary(plainText, matches, dictionary[stringList[0]][0], 600, out startOfSentance1));
    else if (dictionary.Count > 1)
    {
      int startOfSentance2;
      string articleSubSummary = this.GetArticleSubSummary(plainText, matches, dictionary[stringList[0]][0], 300, out startOfSentance2);
      int origin = 0;
      foreach (int i in dictionary[stringList[1]])
      {
        if (matches[i].Index >= startOfSentance2 + articleSubSummary.Length)
        {
          origin = i;
          break;
        }
      }
      if (origin == 0)
      {
        stringBuilder1.Append(this.GetArticleSubSummary(plainText, matches, dictionary[stringList[0]][0], 600, out startOfSentance2));
      }
      else
      {
        stringBuilder1.Append(articleSubSummary);
        stringBuilder1.Append(" ... ");
        stringBuilder1.Append(this.GetArticleSubSummary(plainText, matches, origin, 300, out startOfSentance2));
      }
    }
    int num1 = 0;
    int num2 = 0;
    int index1 = 0;
    string str;
    if (stringBuilder1.Length > 600)
    {
      for (int index2 = 0; index2 < 600; ++index2)
      {
        char ch = stringBuilder1[index2];
        if (ch == '<' && num2 == 0)
          num2 = 1;
        else if (ch == 'b' && num2 == 1)
          num2 = 2;
        else if (ch == '>' && num2 == 2)
        {
          ++num1;
          num2 = 0;
        }
        else
          num2 = 0;
        if (ch == '<' && index1 == 0)
          index1 = 1;
        else if (ch == '/' && index1 == 1)
          index1 = 2;
        else if (ch == 'b' && index1 == 2)
          index1 = 3;
        else if (ch == '>' && index1 == 3)
        {
          --num1;
          index1 = 0;
        }
        else
          index1 = 0;
      }
      if (num2 > 0)
      {
        str = stringBuilder1.ToString(0, 600 - num2);
      }
      else
      {
        StringBuilder stringBuilder2 = new StringBuilder(stringBuilder1.ToString(0, 600));
        char[] chArray = new char[4]{ '<', '/', 'b', '>' };
        while (num1 > 0)
        {
          --num1;
          for (; index1 < chArray.Length; ++index1)
            stringBuilder2.Append(chArray[index1]);
          index1 = 0;
        }
        str = stringBuilder2.ToString();
      }
    }
    else
      str = stringBuilder1.ToString();
    return str + "...";
  }

  private string GetArticleSubSummary(
    string plainText,
    MatchCollection matches,
    int origin,
    int length,
    out int startOfSentance)
  {
    startOfSentance = 0;
    if (matches.Count == 0)
      return plainText.Substring(startOfSentance, System.Math.Min(length, plainText.Length)) + " ...";
    startOfSentance = plainText.LastIndexOf(".", matches[origin].Index, System.Math.Min(length, matches[origin].Index), StringComparison.OrdinalIgnoreCase) + 1;
    List<Match> matchList = new List<Match>();
    if (matches.Count > 0)
    {
      for (int i = 0; i < matches.Count; ++i)
      {
        if (matches[i].Index >= startOfSentance && matches[i].Index <= startOfSentance + length)
          matchList.Add(matches[i]);
      }
    }
    StringBuilder stringBuilder = new StringBuilder();
    int startIndex = System.Math.Max(startOfSentance, 0);
    foreach (Match match in matchList)
    {
      stringBuilder.Append(plainText.Substring(startIndex, match.Index - startIndex));
      stringBuilder.AppendFormat("<b>{0}</b>", (object) plainText.Substring(match.Index, match.Length));
      startIndex = match.Index + match.Length;
    }
    int length1 = stringBuilder.Length;
    stringBuilder.Append(plainText.Substring(startIndex));
    return stringBuilder.ToString(0, System.Math.Max(System.Math.Min(length, stringBuilder.Length), length1)) + "...";
  }

  [Serializable]
  protected class HelpCacheResult : CacheResult<Guid>
  {
    protected List<Guid> localizedResults;

    public Guid? WikiID { get; private set; }

    public int Top { get; set; }

    public HelpCacheResult(string query, Guid? wikiID, int top, List<Guid> results)
    {
      this.query = query;
      this.results = results;
      this.searchType = typeof (PXHelpSearch);
      this.WikiID = wikiID;
      this.Top = top;
    }

    public HelpCacheResult(
      string query,
      Guid? wikiID,
      int top,
      List<Guid> results,
      List<Guid> localizedResults)
      : this(query, wikiID, top, results)
    {
      this.localizedResults = localizedResults;
    }

    public List<Guid> LocalizedResults => this.localizedResults;
  }
}
