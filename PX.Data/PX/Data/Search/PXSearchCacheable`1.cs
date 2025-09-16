// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.PXSearchCacheable`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace PX.Data.Search;

/// <summary>
/// Represents a base class for searching records with paging support.
/// This class caches records IDs to implement paging functionality.
/// </summary>
/// <typeparam name="DACType">Type of records to search in.</typeparam>
public abstract class PXSearchCacheable<TResult> where TResult : class
{
  protected int totalCount;

  /// <summary>
  /// Gets total number of records matching specified search query and options.
  /// </summary>
  public int TotalCount => this.totalCount;

  public virtual int PreviousIndex { get; protected set; }

  public virtual int NextIndex { get; protected set; }

  public virtual bool HasPrevPage { get; protected set; }

  public virtual bool HasNextPage { get; protected set; }

  /// <summary>
  /// Saves given CacheResult record to user session. If record for this type
  /// of search exists it will be replaced with "result".
  /// </summary>
  /// <param name="result">A record to be saved in cache.</param>
  protected void SaveCache(CacheResult result)
  {
    string key = "Search$" + this.GetType().Name;
    PXContext.SessionTyped<PXSessionStatePXData>().SearchCacheResult[key] = result;
  }

  /// <summary>
  /// Gets a CacheResult record for the current search type from user session.
  /// </summary>
  /// <returns>A record from cache.</returns>
  protected CacheResult GetCache()
  {
    string key = "Search$" + this.GetType().Name;
    return PXContext.SessionTyped<PXSessionStatePXData>().SearchCacheResult[key];
  }

  /// <summary>Resets search cache.</summary>
  protected internal void ResetCache()
  {
    string key = "Search$" + this.GetType().Name;
    PXContext.SessionTyped<PXSessionStatePXData>().SearchCacheResult[key] = (CacheResult) null;
  }

  private CacheResult ValidateCache(string query, CancellationToken cancellationToken)
  {
    CacheResult result = this.GetCache();
    query = query == null ? query : query.Trim();
    if (!this.IsValidResult(result, query))
    {
      result = this.CreateResult(query, cancellationToken);
      this.SaveCache(result);
    }
    return result;
  }

  /// <summary>Searches records which match specified query.</summary>
  /// <param name="query">Search query.</param>
  /// <param name="first">Index of the first record to return.</param>
  /// <param name="count">Number of records to return.</param>
  /// <returns>A list of PXSearchResult objects.</returns>
  public List<TResult> Search(
    string query,
    int first,
    int count,
    CancellationToken cancellationToken = default (CancellationToken))
  {
    return this.CreateSearchResult(this.ValidateCache(query, cancellationToken), first, count, cancellationToken);
  }

  /// <summary>
  /// Determines, whether data stored in cache is valid for the current type of search and given query.
  /// If this nethod returns true search results will be formed using IDs stored in user session,
  /// otherwise cache will be recreated.
  /// </summary>
  /// <param name="result">Cache record to validate.</param>
  /// <param name="query">Search query to check.</param>
  /// <returns>True if cache record is valid for the given query and current search type. False otherwise.</returns>
  protected virtual bool IsValidResult(CacheResult result, string query)
  {
    return result != null && result.Query == query && result.SearchType == this.GetType();
  }

  /// <summary>
  /// Creates a new CacheResult containing IDs of records which match
  /// specified search query to be saved in user session. Method retrieves matching records from database
  /// and saves their keys (IDs) in user session to allow paging support without additional database operations.
  /// </summary>
  /// <param name="query">Search query.</param>
  /// <returns>A CacheResult object matching given query and current search type.</returns>
  protected abstract CacheResult CreateResult(string query, CancellationToken cancellationToken);

  protected abstract CacheResult CreateResult(
    string query,
    int top,
    CancellationToken cancellationToken);

  /// <summary>
  /// Retrieves records which match IDs stored in cache from database and creates an
  /// PXSearchResult object for each of them.
  /// </summary>
  /// <param name="cacheRes">A cached results object containing IDs of records to retrieve.</param>
  /// <param name="first">Number of the first record to retrieve.</param>
  /// <param name="count">Total number of records to retrieve (-1 to select all records).</param>
  /// <param name="cancellationToken"></param>
  /// <returns>A list of <see cref="T:PX.Data.Search.PXSearchResult" /> objects.</returns>
  protected abstract List<TResult> CreateSearchResult(
    CacheResult cacheRes,
    int first,
    int count,
    CancellationToken cancellationToken);
}
