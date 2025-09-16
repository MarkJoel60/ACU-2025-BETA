// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.SearchManagementService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.BulkInsert;
using PX.Common;
using PX.Logging.Sinks.SystemEventsDbSink;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Search;

internal class SearchManagementService : ISearchManagementService
{
  private readonly Func<PXDatabaseProvider> _providerFactory;
  private readonly IEnumerable<IFullTextSearchService> _fullTextSearchServices;
  private readonly ILogger _logger;

  public SearchManagementService(
    Func<PXDatabaseProvider> providerFactory,
    IEnumerable<IFullTextSearchService> fullTextSearchServices,
    ILogger logger)
  {
    this._providerFactory = providerFactory;
    this._fullTextSearchServices = fullTextSearchServices;
    this._logger = logger;
  }

  void ISearchManagementService.RestartFullTextFeature()
  {
    try
    {
      this._logger.ForSystemEvents("System", "System_EnableFtsRequestedEventId").Information("A request to enable FTS has been made");
      this.EnableFtsOnDB();
      this.ClearFtsRelatedCaches();
    }
    catch (Exception ex)
    {
      this._logger.Error(ex, "Failed to enable FTS");
      throw;
    }
  }

  private void EnableFtsOnDB()
  {
    DbmsMaintenance maintenance = this._providerFactory().GetMaintenance();
    this._logger.Debug("Disabling FTS on DB");
    maintenance.DisableFullText();
    this._logger.Debug("Enabling FTS on DB");
    maintenance.EnableFullText();
  }

  private void ClearFtsRelatedCaches()
  {
    PXDatabaseProvider databaseProvider = this._providerFactory();
    HashSet<string> stringSet = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (IFullTextSearchService textSearchService in this._fullTextSearchServices)
    {
      this._logger.Debug<string>("Clearing search cache for {Service} service", textSearchService.GetType().Name);
      textSearchService.ClearCache();
      EnumerableExtensions.AddRange<string>((ISet<string>) stringSet, textSearchService.IndexTablesUsedBySearch().Select<System.Type, string>((Func<System.Type, string>) (x => x.Name)));
    }
    foreach (string singleTableName in stringSet)
    {
      this._logger.Debug<string>("Sending request to invalidate {Table} table", singleTableName);
      databaseProvider.SchemaCacheInvalidate(singleTableName);
    }
  }
}
