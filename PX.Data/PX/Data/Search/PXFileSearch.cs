// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.PXFileSearch
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Web;

#nullable disable
namespace PX.Data.Search;

public class PXFileSearch : PXSearchCacheable<FileSearchResult>
{
  private PXGraph graph;

  public PXFileSearch() => this.graph = PXGraph.CreateInstance<PXGraph>();

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
    string str = $"%{query}%";
    ParameterExpression parameterExpression;
    // ISSUE: method reference
    // ISSUE: type reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: type reference
    IEnumerable<\u003C\u003Ef__AnonymousType101<Guid?>> source = PXSelectBase<UploadFile, PXSelectJoinGroupBy<UploadFile, LeftJoin<UploadFileRevisionNoData, On<UploadFile.fileID, Equal<UploadFileRevisionNoData.fileID>, And<UploadFileRevisionNoData.comment, IsNotNull, And<UploadFileRevisionNoData.comment, NotEqual<PX.Data.Empty>>>>>, Where<UploadFile.name, Like<Required<UploadFile.name>>, Or<UploadFileRevisionNoData.comment, Like<Required<UploadFileRevisionNoData.comment>>>>, Aggregate<GroupBy<UploadFile.fileID>>>.Config>.Select(this.graph, (object) str, (object) str).Select(Expression.Lambda<Func<PXResult<UploadFile>, \u003C\u003Ef__AnonymousType101<Guid?>>>((Expression) Expression.New((ConstructorInfo) MethodBase.GetMethodFromHandle(__methodref (\u003C\u003Ef__AnonymousType101<Guid?>.\u002Ector), __typeref (\u003C\u003Ef__AnonymousType101<Guid?>)), (IEnumerable<Expression>) new Expression[1]
    {
      (Expression) Expression.Property((Expression) Expression.Call(f, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (UploadFile.get_FileID)))
    }, (MemberInfo) MethodBase.GetMethodFromHandle(__methodref (\u003C\u003Ef__AnonymousType101<Guid?>.get_FileID), __typeref (\u003C\u003Ef__AnonymousType101<Guid?>))), parameterExpression)).AsEnumerable();
    List<Guid> results = new List<Guid>(source.Count());
    foreach (var data in source)
      results.Add(data.FileID.Value);
    stopwatch.Stop();
    return (CacheResult) new FileCacheResult(query, results);
  }

  protected override List<FileSearchResult> CreateSearchResult(
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
    List<FileSearchResult> searchResult = new List<FileSearchResult>();
    if (!(cacheRes is FileCacheResult fileCacheResult))
      return searchResult;
    List<Guid> results = fileCacheResult.Results;
    int index = first;
    int num = 0;
    while (index < fileCacheResult.Results.Count && index >= 0 && num < count)
    {
      PXResult<UploadFile, UploadFileRevisionNoData> pxResult = (PXResult<UploadFile, UploadFileRevisionNoData>) (PXResult<UploadFile>) PXSelectBase<UploadFile, PXSelectJoinGroupBy<UploadFile, LeftJoin<UploadFileRevisionNoData, On<UploadFile.fileID, Equal<UploadFileRevisionNoData.fileID>>>, Where<UploadFile.fileID, Equal<Required<UploadFile.fileID>>>, Aggregate<GroupBy<UploadFile.fileID>>>.Config>.SelectWindowed(this.graph, 0, 1, (object) results[index]);
      if (pxResult != null)
      {
        UploadFile file = (UploadFile) pxResult;
        UploadFileRevisionNoData fileRevisionNoData = (UploadFileRevisionNoData) pxResult;
        if (UploadFileMaintenance.AccessRights(file) >= PXCacheRights.Select && !UploadFileMaintenance.IsRestrictedFile(file))
        {
          string url = HttpUtility.UrlEncode(file.Name);
          string dataSize = WikiFileMaintenance.GetDataSize(fileRevisionNoData.DataSize);
          string str = $"Created: {fileRevisionNoData.CreatedDateTime} - Size: {dataSize}";
          if (!string.IsNullOrEmpty(fileRevisionNoData.Comment))
            str += $" - Comment: {fileRevisionNoData.Comment}";
          string title = TextUtils.Emphasize(file.Name, cacheRes.Query);
          string line1 = TextUtils.Emphasize(str, cacheRes.Query);
          FileSearchResult fileSearchResult = new FileSearchResult(url, title, line1);
          searchResult.Add(fileSearchResult);
          ++num;
        }
      }
      if (flag)
        --index;
      else
        ++index;
    }
    this.totalCount = fileCacheResult.Results.Count;
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
      this.HasNextPage = index < fileCacheResult.Results.Count;
    }
    stopwatch.Stop();
    if (flag)
      searchResult.Reverse();
    return searchResult;
  }
}
