// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.PXEntitySearch
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Mobile.Legacy;
using PX.Common;
using PX.Data.Descriptor.Attributes;
using PX.SM;
using Serilog.Events;
using SerilogTimings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Compilation;

#nullable disable
namespace PX.Data.Search;

/// <summary>
/// An internal search engine, which performs the search in records of DACs.
/// </summary>
public class PXEntitySearch : PXSearchCacheable<EntitySearchResult>
{
  private readonly int _querySize = WebConfig.MaxFullTextSearchResultCount;
  private const int MinWordLength = 2;
  private PXGraph graph;
  private bool _checkMobileSiteMap;
  private const string IsAboutPattern = "ISABOUT(\"{0}\" NEAR {1} WEIGHT(1), {1} WEIGHT(0.2))";

  public int? Category { get; set; }

  public string EntityType { get; set; }

  private BqlFullTextRenderingMethod FullTextCapability
  {
    get => PXDatabase.Provider.GetFullTextSearchCapability<SearchIndex.content>();
  }

  public bool IsFullText() => this.FullTextCapability != BqlFullTextRenderingMethod.NeutralLike;

  public PXEntitySearch()
    : this(false)
  {
  }

  public PXEntitySearch(bool checkMobileSiteMap)
  {
    this.graph = PXGraph.CreateInstance<PXGraph>();
    this._checkMobileSiteMap = checkMobileSiteMap;
  }

  public bool IsSearchIndexExists()
  {
    return (SearchIndex) PXSelectBase<SearchIndex, PXSelect<SearchIndex>.Config>.SelectWindowed(this.graph, 0, 1) != null;
  }

  protected override CacheResult CreateResult(string query, CancellationToken cancellationToken)
  {
    return this.CreateResult(query, this._querySize, cancellationToken);
  }

  protected override CacheResult CreateResult(
    string query,
    int top,
    CancellationToken cancellationToken)
  {
    if (string.IsNullOrEmpty(query))
      return (CacheResult) null;
    using (Operation operation = Operation.At((LogEventLevel) 1, new LogEventLevel?()).Begin("Create search result for query: {Query}", new object[1]
    {
      (object) query
    }))
    {
      List<object> objectList1 = new List<object>();
      List<object> objectList2 = new List<object>();
      PXResultset<SearchIndex> pxResultset;
      if (this.IsFullText())
      {
        PXSelectBase<SearchIndex> pxSelectBase1 = (PXSelectBase<SearchIndex>) new PXSelectReadonly2<SearchIndex, LeftJoin<SearchIndexEntityRank, On<SearchIndex.entityType, Equal<SearchIndexEntityRank.entityType>>>, Where<Contains<SearchIndex.content, Required<SearchIndex.content>, SearchIndex.indexID>>, OrderBy<Desc<SearchIndexEntityRank.entityRank, Desc<RankOf<SearchIndex.content>>>>>(this.graph);
        PXSelectBase<SearchIndex> pxSelectBase2 = (PXSelectBase<SearchIndex>) new PXSelectReadonly2<SearchIndex, LeftJoin<SearchIndexEntityRank, On<SearchIndex.entityType, Equal<SearchIndexEntityRank.entityType>>>, Where<Contains<SearchIndex.content, Required<SearchIndex.content>, SearchIndex.indexID, Required<SearchIndex.top>>>, OrderBy<Desc<SearchIndexEntityRank.entityRank, Desc<RankOf<SearchIndex.content>>>>>(this.graph);
        string str = this.PrepareQuery(query, true);
        if (string.IsNullOrWhiteSpace(str))
          return (CacheResult) null;
        objectList1.Add((object) str);
        objectList2.Add((object) str);
        objectList2.Add((object) top);
        if (this.Category.HasValue)
        {
          pxSelectBase1.WhereAnd<Where<BitwiseAnd<SearchIndex.category, Required<SearchIndex.category>>, Equal<Required<SearchIndex.category>>>>();
          pxSelectBase2.WhereAnd<Where<BitwiseAnd<SearchIndex.category, Required<SearchIndex.category>>, Equal<Required<SearchIndex.category>>>>();
          objectList1.Add((object) this.Category);
          objectList1.Add((object) this.Category);
          objectList2.Add((object) this.Category);
          objectList2.Add((object) this.Category);
        }
        if (this.EntityType != null)
        {
          pxSelectBase1.WhereAnd<Where<SearchIndex.entityType, Equal<Required<SearchIndex.entityType>>>>();
          pxSelectBase2.WhereAnd<Where<SearchIndex.entityType, Equal<Required<SearchIndex.entityType>>>>();
          objectList1.Add((object) this.EntityType);
          objectList2.Add((object) this.EntityType);
        }
        cancellationToken.ThrowIfCancellationRequested();
        using (new PXFieldScope(pxSelectBase2.View, new System.Type[2]
        {
          typeof (SearchIndex.indexID),
          typeof (SearchIndex.entityType)
        }))
          pxResultset = pxSelectBase2.SelectWindowed(0, top, objectList2.ToArray());
        cancellationToken.ThrowIfCancellationRequested();
        int num = PXDatabase.Companies.Length;
        if (num == 0)
          num = 1;
        if (pxResultset.Count < this._querySize / num)
        {
          using (new PXFieldScope(pxSelectBase1.View, new System.Type[2]
          {
            typeof (SearchIndex.indexID),
            typeof (SearchIndex.entityType)
          }))
            pxResultset = pxSelectBase1.SelectWindowed(0, top, objectList1.ToArray());
        }
      }
      else
      {
        PXSelectBase<SearchIndex> pxSelectBase = (PXSelectBase<SearchIndex>) new PXSelectReadonly<SearchIndex, Where<SearchIndex.content, Like<Required<SearchIndex.content>>>>(this.graph);
        objectList1.Add((object) $"%{query}%");
        if (this.Category.HasValue)
        {
          pxSelectBase.WhereAnd<Where<BitwiseAnd<SearchIndex.category, Required<SearchIndex.category>>, Equal<Required<SearchIndex.category>>>>();
          objectList1.Add((object) this.Category);
        }
        if (this.EntityType != null)
        {
          pxSelectBase.WhereAnd<Where<SearchIndex.entityType, Equal<Required<SearchIndex.entityType>>>>();
          objectList1.Add((object) this.EntityType);
        }
        cancellationToken.ThrowIfCancellationRequested();
        using (new PXFieldScope(pxSelectBase.View, new System.Type[2]
        {
          typeof (SearchIndex.indexID),
          typeof (SearchIndex.entityType)
        }))
          pxResultset = pxSelectBase.SelectWindowed(0, top, objectList1.ToArray());
      }
      cancellationToken.ThrowIfCancellationRequested();
      List<Guid> results = new List<Guid>(pxResultset.Count);
      foreach (PXResult<SearchIndex> pxResult in pxResultset)
      {
        SearchIndex searchIndex = (SearchIndex) pxResult;
        results.Add(searchIndex.IndexID.Value);
      }
      operation.Complete("Records", (object) results.Count, false);
      return (CacheResult) new PXEntitySearch.EntitiesCacheResult(query, this.Category, top, results);
    }
  }

  protected override bool IsValidResult(CacheResult result, string query)
  {
    if (!(result is PXEntitySearch.EntitiesCacheResult entitiesCacheResult) || !(entitiesCacheResult.Query == query) || !(entitiesCacheResult.SearchType == this.GetType()))
      return false;
    int? category1 = entitiesCacheResult.Category;
    int? category2 = this.Category;
    return category1.GetValueOrDefault() == category2.GetValueOrDefault() & category1.HasValue == category2.HasValue;
  }

  protected virtual EntitySearchResult InstantiateEntitySearchResult(
    string screenId,
    SearchIndex index,
    string title,
    string path,
    string line1,
    string line2)
  {
    return new EntitySearchResult(screenId, index.NoteID.Value, title, path, line1, line2);
  }

  protected override List<EntitySearchResult> CreateSearchResult(
    CacheResult cacheRes,
    int first,
    int count,
    CancellationToken cancellationToken)
  {
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    bool flag1 = false;
    if (first < 0)
    {
      first = System.Math.Abs(first) - 1;
      flag1 = true;
    }
    List<EntitySearchResult> searchResult = new List<EntitySearchResult>();
    if (!(cacheRes is PXEntitySearch.EntitiesCacheResult result1))
      return searchResult;
    List<Guid> results = result1.Results;
    int index = first;
    int num1 = 0;
    if (this._checkMobileSiteMap)
    {
      index = 0;
      num1 = first;
    }
    int num2 = 0;
    if (result1.Results.Count == result1.Top && first + count + 1 > result1.Results.Count && result1.Top < 16000)
    {
      this.ResetCache();
      result1 = (PXEntitySearch.EntitiesCacheResult) this.CreateResult(cacheRes.Query, result1.Top * 2, cancellationToken);
      this.SaveCache((CacheResult) result1);
      results = result1.Results;
    }
    int num3 = 0;
    while (index < result1.Results.Count && index >= 0 && num2 < count + num1)
    {
      cancellationToken.ThrowIfCancellationRequested();
      SearchIndex searchIndex = (SearchIndex) PXSelectBase<SearchIndex, PXSelectReadonly<SearchIndex, Where<SearchIndex.indexID, Equal<Required<SearchIndex.indexID>>>>.Config>.SelectWindowed(this.graph, 0, 1, (object) results[index]);
      if (searchIndex != null)
      {
        System.Type dac;
        object row1;
        PXSearchableAttribute attr;
        this.SelectEntityRow(searchIndex.NoteID.Value, searchIndex, out dac, out row1, out attr);
        if (row1 != null && num2 < count + num1)
        {
          string path = string.Empty;
          System.Type graphType = (System.Type) null;
          object row2 = row1;
          System.Type declaredType;
          PXCache declaredCache;
          if (PXPrimaryGraphAttribute.FindPrimaryGraph(this.graph.Caches[dac], true, ref row2, out graphType, out declaredType, out declaredCache) != null)
          {
            bool flag2 = false;
            string screenId = string.Empty;
            if (graphType != (System.Type) null)
            {
              PXSiteMapNode pxSiteMapNode = PXSiteMap.Provider.FindSiteMapNode(graphType);
              if (pxSiteMapNode != null)
              {
                screenId = pxSiteMapNode.ScreenID;
                flag2 = this._checkMobileSiteMap && Provider.GetScreenMap(screenId, false) != null;
                if (!this._checkMobileSiteMap | flag2)
                {
                  path = pxSiteMapNode.Title;
                  for (; pxSiteMapNode.ParentNode != null && pxSiteMapNode.ParentNode.Key != pxSiteMapNode.Provider.RootNode.Key; pxSiteMapNode = pxSiteMapNode.ParentNode)
                    path = $"{pxSiteMapNode.ParentNode.Title} > {path}";
                }
              }
            }
            if (declaredType != (System.Type) null && !this._checkMobileSiteMap | flag2)
            {
              PXSearchableAttribute.RecordInfo recordInfo = attr.BuildRecordInfo(this.graph.Caches[declaredType], row1);
              string note = PXNoteAttribute.GetNote(declaredCache, row1);
              string line1 = this._checkMobileSiteMap ? recordInfo.Line1 : TextUtils.Emphasize(recordInfo.Line1, cacheRes.Query);
              string line2 = this._checkMobileSiteMap ? recordInfo.Line2 : TextUtils.Emphasize(recordInfo.Line2, cacheRes.Query);
              if (string.IsNullOrWhiteSpace(line2))
              {
                if (!string.IsNullOrWhiteSpace(note))
                {
                  string noteSummary = this.GetNoteSummary(note, cacheRes.Query);
                  line2 = "Note: " + (this._checkMobileSiteMap ? noteSummary : TextUtils.Emphasize(noteSummary, cacheRes.Query));
                }
              }
              else if (!string.IsNullOrWhiteSpace(note))
              {
                string noteSummary = this.GetNoteSummary(note, cacheRes.Query);
                line2 = $"{line2} - Note: {(this._checkMobileSiteMap ? noteSummary : TextUtils.Emphasize(noteSummary, cacheRes.Query))}";
              }
              EntitySearchResult entitySearchResult = this.InstantiateEntitySearchResult(screenId, searchIndex, this._checkMobileSiteMap ? recordInfo.Title : TextUtils.Emphasize(recordInfo.Title, cacheRes.Query), path, line1, line2);
              if (!this._checkMobileSiteMap)
              {
                ++num2;
                searchResult.Add(entitySearchResult);
              }
              else if (flag2)
              {
                ++num2;
                if (num2 > num1)
                  searchResult.Add(entitySearchResult);
              }
            }
          }
          num3 = !flag1 ? index + 1 : index - 1;
        }
      }
      if (flag1)
        --index;
      else
        ++index;
      if (result1.Results.Count == result1.Top && result1.Top < 16000 && index + count + 1 - num2 > result1.Results.Count)
      {
        this.ResetCache();
        PXEntitySearch.EntitiesCacheResult result2 = (PXEntitySearch.EntitiesCacheResult) this.CreateResult(cacheRes.Query, result1.Top * 2, cancellationToken);
        this.SaveCache((CacheResult) result2);
        return this.CreateSearchResult((CacheResult) result2, first, count, cancellationToken);
      }
    }
    this.totalCount = result1.Results.Count;
    if (flag1)
    {
      this.PreviousIndex = num3 + 1;
      this.NextIndex = first + 1;
      this.HasPrevPage = num2 > count;
    }
    else
    {
      this.PreviousIndex = first;
      this.NextIndex = num3;
      this.HasPrevPage = first > 0;
      this.HasNextPage = index < result1.Results.Count;
    }
    stopwatch.Stop();
    if (flag1)
      searchResult.Reverse();
    return searchResult;
  }

  internal static bool IsExactMatch(string query)
  {
    return query.StartsWith("\"") && query.EndsWith("\"") && query.Length > 1;
  }

  private static string ConvertToExactMatchPatern(string query)
  {
    return string.Format("ISABOUT(\"{0}\" NEAR {1} WEIGHT(1), {1} WEIGHT(0.2))", (object) "Acumatica profile:", (object) query);
  }

  internal static string ConvertToContainsPatern(string query)
  {
    string containsPatern = (string) null;
    string str = string.Empty;
    string[] strArray = Regex.Split(query, "\\W+");
    if (strArray.Length != 0)
    {
      List<string> stringList = new List<string>();
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (strArray[index].Length >= 2)
          stringList.Add(strArray[index]);
      }
      if (stringList.Count > 0)
      {
        for (int index = 0; index < stringList.Count - 1; ++index)
          str += $"\"{stringList[index]}*\" NEAR ";
        str = string.Format("ISABOUT(\"{0}\" NEAR {1} WEIGHT(1), {1} WEIGHT(0.2))", (object) "Acumatica profile:", (object) (str + $"\"{stringList[stringList.Count - 1]}*\""));
      }
    }
    else
      str = query;
    if (!string.IsNullOrWhiteSpace(str))
      containsPatern = str;
    return containsPatern;
  }

  [PXInternalUseOnly]
  public string PrepareQuery(string query, bool isProfile = false)
  {
    string str;
    if (PXEntitySearch.IsExactMatch(query))
    {
      switch (this.FullTextCapability)
      {
        case BqlFullTextRenderingMethod.MySqlMatchAgainst:
          str = query;
          break;
        case BqlFullTextRenderingMethod.PgSqlTsQuery:
          str = query;
          break;
        default:
          str = isProfile ? PXEntitySearch.ConvertToExactMatchPatern(query) : query;
          break;
      }
    }
    else
    {
      switch (this.FullTextCapability)
      {
        case BqlFullTextRenderingMethod.MySqlMatchAgainst:
          str = PXEntitySearch.ConvertToContainsPaternMySql(query);
          break;
        case BqlFullTextRenderingMethod.PgSqlTsQuery:
          str = PXEntitySearch.ConvertToContainsPaternPgSql(query);
          break;
        default:
          str = PXEntitySearch.ConvertToContainsPatern(query);
          break;
      }
    }
    return str;
  }

  [PXInternalUseOnly]
  public static string ConvertToContainsPaternPgSql(string query)
  {
    string[] array = ((IEnumerable<string>) Regex.Split(query, "\\W+")).Where<string>((Func<string, bool>) (w => w != string.Empty)).ToArray<string>();
    if (array.Length == 0)
      return query;
    bool flag = Regex.IsMatch(query, "\\s+");
    return string.Join(" & ", ((IEnumerable<string>) array).Select<string, string>((Func<string, string>) (w => $"\"{w}*\""))) + (flag || array.Length <= 1 ? "" : " | " + query);
  }

  internal static string ConvertToContainsPaternMySql(string query)
  {
    IEnumerable<string> source = ((IEnumerable<string>) Regex.Split(query, "\\W+")).Where<string>((Func<string, bool>) (w => w != string.Empty));
    return source.Count<string>() <= 0 ? query : source.Where<string>((Func<string, bool>) (w => w.Length >= 2)).Select<string, string>((Func<string, string>) (w => $"+{w}*")).DefaultIfEmpty<string>().Aggregate<string>((Func<string, string, string>) ((f, w) => $"{f} {w}"));
  }

  private void SelectEntityRow(
    Guid noteID,
    SearchIndex searchIndex,
    out System.Type dac,
    out object row,
    out PXSearchableAttribute attr)
  {
    using (Operation.At((LogEventLevel) 1, new LogEventLevel?()).Time("Select entity row", Array.Empty<object>()))
    {
      row = (object) null;
      attr = (PXSearchableAttribute) null;
      System.Type searchField;
      dac = this.GetDAC(noteID, searchIndex, out searchField);
      if (dac == (System.Type) null || searchField == (System.Type) null)
        return;
      attr = this.GetSearchAttr(dac);
      if (attr == null)
        return;
      BqlCommand select = EntitySearchBqlConstructor.CreateBqlQueryWithAccessChecks(dac, attr, searchField, false);
      if (attr.WhereConstraint != (System.Type) null)
        select = select.WhereAnd(attr.WhereConstraint);
      if (select == null)
        return;
      try
      {
        object row1 = new PXView(this.graph, true, select).SelectSingle((object) noteID, (object) this.graph.Accessinfo.UserName);
        row = (object) PXResult.Unwrap(row1, dac);
        if (attr.SelectDocumentUser != (System.Type) null)
        {
          PXResult pxResult = (PXResult) new PXView(this.graph, true, BqlCommand.CreateInstance(BqlCommand.Decompose(attr.SelectDocumentUser))).SelectSingleBound(new object[1]
          {
            row
          });
          if (pxResult != null)
          {
            Users users = (Users) pxResult[0];
            if (users != null)
            {
              Guid? nullable1 = (Guid?) this.graph.Caches[dac].GetValue(row, "createdbyid");
              Guid? nullable2 = users.PKID;
              Guid userId1 = this.graph.Accessinfo.UserID;
              if ((nullable2.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() == userId1 ? 1 : 0) : 1) : 0) == 0)
              {
                nullable2 = nullable1;
                Guid userId2 = this.graph.Accessinfo.UserID;
                if ((nullable2.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() == userId2 ? 1 : 0) : 1) : 0) == 0)
                  row = (object) null;
              }
            }
          }
        }
        if (row == null)
        {
          dac = (System.Type) null;
        }
        else
        {
          System.Type useDac;
          if (this.VerifyRights(dac, row, out useDac))
          {
            dac = useDac;
          }
          else
          {
            dac = (System.Type) null;
            row = (object) null;
          }
        }
      }
      catch
      {
        dac = (System.Type) null;
        row = (object) null;
      }
    }
  }

  public IBqlTable GetEntity(Guid noteID)
  {
    object row;
    this.SelectEntityRow(noteID, (SearchIndex) null, out System.Type _, out row, out PXSearchableAttribute _);
    return row as IBqlTable;
  }

  public void Redirect(Guid noteID)
  {
    System.Type dac;
    object row;
    this.SelectEntityRow(noteID, (SearchIndex) null, out dac, out row, out PXSearchableAttribute _);
    PXRedirectHelper.TryRedirect(this.graph.Caches[dac], row, "");
  }

  private System.Type GetDAC(Guid noteID, SearchIndex searchIndex, out System.Type searchField)
  {
    searchField = (System.Type) null;
    SearchIndex searchIndex1 = searchIndex;
    if (searchIndex1 == null)
      searchIndex1 = (SearchIndex) PXSelectBase<SearchIndex, PXSelect<SearchIndex, Where<SearchIndex.noteID, Equal<Required<SearchIndex.noteID>>>>.Config>.SelectWindowed(this.graph, 0, 1, (object) noteID);
    searchIndex = searchIndex1;
    if (searchIndex == null)
      return (System.Type) null;
    System.Type type = PXBuildManager.GetType(searchIndex.EntityType, false);
    if (type == (System.Type) null)
      return (System.Type) null;
    searchField = type.GetNestedType(nameof (noteID));
    for (System.Type baseType = type.BaseType; searchField == (System.Type) null && baseType != (System.Type) null; baseType = baseType.BaseType)
      searchField = baseType.GetNestedType(nameof (noteID));
    return type;
  }

  private bool VerifyRights(System.Type dac, object row, out System.Type useDac)
  {
    useDac = dac;
    using (Operation.At((LogEventLevel) 1, new LogEventLevel?()).Time("Verify rights", Array.Empty<object>()))
    {
      System.Type graphType1 = (System.Type) null;
      if (PXPrimaryGraphAttribute.FindPrimaryGraph(this.graph.Caches[dac], true, ref row, out graphType1) == null || graphType1 == (System.Type) null)
        return true;
      bool flag = PXAccess.VerifyRights(graphType1);
      if (!flag && typeof (IBqlTable).IsAssignableFrom(dac.BaseType))
      {
        System.Type graphType2 = (System.Type) null;
        if (PXPrimaryGraphAttribute.FindPrimaryGraph(this.graph.Caches[dac.BaseType], true, ref row, out graphType2) == null || graphType2 == (System.Type) null)
          return false;
        flag = PXAccess.VerifyRights(graphType2);
        if (flag)
          useDac = dac.BaseType;
      }
      return flag;
    }
  }

  protected string GetKey(System.Type entityType, object row)
  {
    string friendlyEntityName = EntityHelper.GetFriendlyEntityName(entityType, row);
    foreach (System.Type bqlKey in this.graph.Caches[entityType].BqlKeys)
      friendlyEntityName += this.graph.Caches[entityType].GetValue(row, bqlKey.Name)?.ToString();
    return friendlyEntityName;
  }

  private PXSearchableAttribute GetSearchAttr(System.Type dac)
  {
    foreach (PXEventSubscriberAttribute attribute in this.graph.Caches[dac].GetAttributes("noteID"))
    {
      if (attribute is PXSearchableAttribute)
        return attribute as PXSearchableAttribute;
    }
    return (PXSearchableAttribute) null;
  }

  private string GetNoteSummary(string plainText, string query)
  {
    if (string.IsNullOrEmpty(plainText))
      return plainText;
    int num = plainText.IndexOf(query, 0, plainText.Length, StringComparison.OrdinalIgnoreCase);
    if (num > 1)
    {
      int startIndex1 = plainText.LastIndexOf(".", num, System.Math.Min(200, num), StringComparison.OrdinalIgnoreCase);
      if (startIndex1 < 1 && num < 100)
      {
        int length = System.Math.Min(150, plainText.Length);
        return plainText.Substring(0, length) + " ...";
      }
      if (startIndex1 > 0)
      {
        int length = System.Math.Min(150, plainText.Length - startIndex1);
        return plainText.Substring(startIndex1, length) + " ...";
      }
      int startIndex2 = plainText.LastIndexOf(' ', num - 50);
      if (startIndex2 < 0)
        startIndex2 = 50;
      int length1 = System.Math.Min(150, plainText.Length - startIndex2);
      return $"... {plainText.Substring(startIndex2, length1)} ...";
    }
    int length2 = System.Math.Min(150, plainText.Length);
    return plainText.Substring(0, length2) + " ...";
  }

  internal static string EmulateFreeTextOnMySql(string query)
  {
    string[] strArray = query.Split(new char[1]{ ' ' }, StringSplitOptions.RemoveEmptyEntries);
    for (int index = 0; index < strArray.Length; ++index)
    {
      if (strArray[index].Length >= 4)
        strArray[index] = strArray[index] + "*";
    }
    return string.Join(" ", strArray);
  }

  internal IEnumerable<System.Type> IndexTablesUsedBySearch
  {
    get
    {
      return (IEnumerable<System.Type>) new System.Type[1]
      {
        typeof (SearchIndex)
      };
    }
  }

  [Serializable]
  protected class EntitiesCacheResult : CacheResult<Guid>
  {
    public int? Category { get; private set; }

    public int Top { get; set; }

    public EntitiesCacheResult(string query, int? category, int top, List<Guid> results)
    {
      this.query = query;
      this.results = results;
      this.Category = category;
      this.searchType = typeof (PXEntitySearch);
      this.Top = top;
    }
  }
}
