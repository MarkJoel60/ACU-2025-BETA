// Decompiled with JetBrains decompiler
// Type: PX.Data.DbSchemaCache
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Data.SqlClient;
using PX.Api;
using PX.Common;
using PX.DbServices.Model;
using PX.DbServices.Model.Entities;
using PX.DbServices.Model.Schema;
using PX.DbServices.Points;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.Scripting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable disable
namespace PX.Data;

/// <exclude />
public class DbSchemaCache : IDbSchemaCache
{
  public const string CompanyIdentityColumnPrefix = "Company";
  protected ReaderWriterLock _SpecificTablesLock = new ReaderWriterLock();
  private readonly Dictionary<string, TableHeader> dbTables = new Dictionary<string, TableHeader>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  private bool schemaLoaded;
  private System.DateTime? reloadSchemaTime;
  private readonly Func<Point> pointFactory;
  private string _dbHost;
  private string _databaseName;
  private readonly ConcurrentDictionary<Tuple<string, bool>, companySetting> companySettingsCache = new ConcurrentDictionary<Tuple<string, bool>, companySetting>();
  private const int MaxAttemptsToGetLockedTableHeader = 5;

  public DbSchemaCache(Func<Point> createDbServicesPoint)
  {
    this.pointFactory = createDbServicesPoint;
  }

  public virtual string DatabaseName
  {
    get
    {
      if (this._databaseName == null)
        this.ensureDbAddress();
      return this._databaseName;
    }
  }

  public virtual string DatabaseHost
  {
    get
    {
      if (this._dbHost == null)
        this.ensureDbAddress();
      return this._dbHost;
    }
  }

  private void ensureDbAddress()
  {
    Lazy<PointDbmsBase> lazy = new Lazy<PointDbmsBase>((Func<PointDbmsBase>) (() => this.pointFactory() as PointDbmsBase));
    if (this._databaseName == null)
      this._databaseName = lazy.Value.DbName;
    if (this._dbHost != null)
      return;
    this._dbHost = lazy.Value.DbHost;
  }

  public companySetting getTableSetting(string tableName, bool selectablesHaveNoCompanyId)
  {
    Tuple<string, bool> key = Tuple.Create<string, bool>(tableName, selectablesHaveNoCompanyId);
    companySetting tableSetting = (companySetting) null;
    if (!this.companySettingsCache.TryGetValue(key, out tableSetting))
    {
      tableSetting = this.getSpecificTableSetting(this.GetTableHeader(tableName) ?? this.TryGetLockedTableHeader(tableName), selectablesHaveNoCompanyId);
      this.companySettingsCache.TryAdd(key, tableSetting);
    }
    return tableSetting;
  }

  private bool TableHeaderCanBeLocked()
  {
    return this.pointFactory() is PointDbmsBase pointDbmsBase && ((Enum) (object) pointDbmsBase.Platform).HasFlag((Enum) (object) (DbmsPlatform) 256 /*0x0100*/);
  }

  private TableHeader TryGetLockedTableHeader(string tableName)
  {
    if (!this.TableHeaderCanBeLocked())
      return (TableHeader) null;
    if (this.TableIsTemporal(tableName))
      return (TableHeader) null;
    PXTrace.WithStack().Verbose<string>("TryGetLockedTableHeader call for {TableName} table.", tableName);
    int num = 5;
    while (num > 0)
    {
      this.InvalidateTable(tableName, false);
      TableHeader tableHeader = this.GetTableHeader(tableName);
      if (tableHeader != null)
      {
        PXTrace.WithStack().Verbose<string, int>("Schema for {TableName} table was successfully found in DB after {AttemptsCount} attempts.", tableName, 5 - num + 1);
        return tableHeader;
      }
      --num;
      if (num > 0)
        Thread.Sleep(300);
    }
    PXTrace.WithStack().Verbose<string, int>("Failed to get schema for {TableName} table from DB after {AttemptsCount} attempts.", tableName, 5);
    return (TableHeader) null;
  }

  private bool TableIsTemporal(string tableName) => tableName.StartsWith("#");

  public void InvalidateAll()
  {
    PXTrace.WithStack().Verbose("Invalidate all tables");
    PXReaderWriterScope readerWriterScope;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._SpecificTablesLock);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope).AcquireWriterLock();
      this.schemaLoaded = false;
    }
    finally
    {
      readerWriterScope.Dispose();
    }
  }

  public void InvalidateTables(List<string> tablesToInvalidate)
  {
    PXTrace.WithStack().Verbose<int>("Attempt to invalidate {TablesToInvalidateCount} tables.", tablesToInvalidate.Count);
    if (tablesToInvalidate.Count >= 10)
    {
      this.InvalidateAll();
    }
    else
    {
      foreach (string tableName in tablesToInvalidate)
        this.InvalidateTable(tableName);
    }
  }

  public void InvalidateTable(string tableName) => this.InvalidateTable(tableName, true);

  private void InvalidateTable(string tableName, bool checkIfExists)
  {
    PXReaderWriterScope readerWriterScope;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._SpecificTablesLock);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
      if (checkIfExists && !this.dbTables.ContainsKey(tableName))
      {
        PXTrace.WithStack().Verbose<string>("Attempt to invalidate {TableName} table but it was not found in cache.", tableName);
      }
      else
      {
        ((PXReaderWriterScope) ref readerWriterScope).UpgradeToWriterLock();
        if (checkIfExists && !this.dbTables.ContainsKey(tableName))
          PXTrace.WithStack().Verbose<string>("Attempt to invalidate {TableName} table but it was not found in cache.", tableName);
        else if (!(this.pointFactory() is PointDbmsBase pointDbmsBase))
        {
          PXTrace.WithStack().Verbose<string>("Attempt to invalidate {TableName} table but db point was not found, so all tables were invalidated.", tableName);
          this.schemaLoaded = false;
        }
        else
        {
          EntityType entityType;
          List<TableColumn> columns = pointDbmsBase.SchemaReader.GetColumns(tableName, ref entityType, (SchemaReaderBase.CacheUsage) 0);
          if (columns == null)
          {
            PXTrace.WithStack().Verbose<string>("Schema for {TableName} table was not found in DB. Invalidation failed.", tableName);
          }
          else
          {
            TableHeader tableHeader = this.dbTables.TryGetValue(tableName, out tableHeader) ? tableHeader : (this.dbTables[tableName] = new TableHeader(tableName, false));
            tableHeader.Columns.Clear();
            tableHeader.Columns.AddRange((IEnumerable<TableColumn>) columns);
            tableHeader.Indices.Clear();
            EnumerableExtensions.AddRange<TableIndex>(tableHeader.Indices, (IEnumerable<TableIndex>) pointDbmsBase.SchemaReader.GetIndices(tableName, (SchemaReaderBase.CacheUsage) 0));
            companySetting companySetting;
            this.companySettingsCache.TryRemove(Tuple.Create<string, bool>(tableName, true), out companySetting);
            this.companySettingsCache.TryRemove(Tuple.Create<string, bool>(tableName, false), out companySetting);
            PXTrace.WithStack().Verbose<string>("{TableName} table was invalidated successfully.", tableName);
          }
        }
      }
    }
    finally
    {
      readerWriterScope.Dispose();
    }
  }

  /// <summary> Determines if table has predefined columns and returns that setting </summary>
  private companySetting getSpecificTableSetting(TableHeader table, bool selectablesHaveNoCompanyId)
  {
    if (table == null)
      return new companySetting(companySetting.companyFlag.Global, this.TableHeaderCanBeLocked());
    bool flag1 = false;
    bool flag2 = false;
    string identity = (string) null;
    string webAppType = (string) null;
    string deleted = (string) null;
    string recordStatus = (string) null;
    string branch = (string) null;
    string modified = (string) null;
    string modifiedBy = (string) null;
    string timetag = (string) null;
    bool flag3 = false;
    foreach (string b in table.Columns.Select<TableColumn, string>((Func<TableColumn, string>) (column => ((TableEntityBase) column).Name)))
    {
      if ("CompanyID".Equals(b, StringComparison.OrdinalIgnoreCase))
        flag1 = true;
      else if ("CompanyMask".Equals(b, StringComparison.OrdinalIgnoreCase))
        flag2 = true;
      else if ("DeletedDatabaseRecord".Equals(b, StringComparison.OrdinalIgnoreCase))
        deleted = "DeletedDatabaseRecord";
      else if ("UsrDeletedDatabaseRecord".Equals(b, StringComparison.OrdinalIgnoreCase))
        deleted = "UsrDeletedDatabaseRecord";
      else if ("WebAppType".Equals(b, StringComparison.OrdinalIgnoreCase))
        webAppType = "WebAppType";
      else if ("DatabaseRecordStatus".OrdinalEquals(b))
        recordStatus = "DatabaseRecordStatus";
      else if ("BranchID".Equals(b, StringComparison.OrdinalIgnoreCase) && !"Branch".Equals(((TableEntityBase) table).Name, StringComparison.OrdinalIgnoreCase) && !this.IsBranchExtension(((TableEntityBase) table).Name))
        branch = "BranchID";
      else if ("UsrBranchID".Equals(b, StringComparison.OrdinalIgnoreCase) && !"Branch".Equals(((TableEntityBase) table).Name, StringComparison.OrdinalIgnoreCase))
        branch = "UsrBranchID";
      else if ("UsrCompanyID".Equals(b, StringComparison.OrdinalIgnoreCase))
        flag3 = true;
      else if ("LastModifiedDateTime".Equals(b, StringComparison.OrdinalIgnoreCase))
        modified = "LastModifiedDateTime";
      else if ("UsrLastModifiedDateTime".Equals(b, StringComparison.OrdinalIgnoreCase))
        modified = "UsrLastModifiedDateTime";
      else if ("LastModifiedByID".Equals(b, StringComparison.OrdinalIgnoreCase))
        modifiedBy = "LastModifiedByID";
      else if ("TimeTag".Equals(b, StringComparison.OrdinalIgnoreCase))
        timetag = "TimeTag";
      else if (identity == null && b.StartsWith("Company", StringComparison.OrdinalIgnoreCase) && b.Length > 9)
        identity = b.Substring(7);
    }
    if (!flag1)
      return new companySetting(companySetting.companyFlag.Global, (string) null, deleted, branch, modified, modifiedBy, timetag, webAppType, recordStatus);
    if (flag3)
      return new companySetting(companySetting.companyFlag.UserGlobal, (string) null, deleted, branch, modified, modifiedBy, timetag, webAppType, recordStatus);
    return flag2 && (PXContext.GetSlot<bool>("userMappedCompany") || !selectablesHaveNoCompanyId) ? new companySetting(companySetting.companyFlag.Shared, identity, deleted, branch, modified, modifiedBy, timetag, webAppType, recordStatus) : new companySetting(companySetting.companyFlag.Separate, (string) null, deleted, branch, modified, modifiedBy, timetag, webAppType, recordStatus);
  }

  private bool IsBranchExtension(string tableName)
  {
    List<KeyValuePair<string, List<string>>> extensionTables = PXCache.GetExtensionTables("Branch");
    return extensionTables != null && extensionTables.Any<KeyValuePair<string, List<string>>>((Func<KeyValuePair<string, List<string>>, bool>) (kv => kv.Key.Equals(tableName, StringComparison.OrdinalIgnoreCase)));
  }

  public bool HasFullTextIndexOnTable(string table) => this.GetFullTextIndexOnTable(table) != null;

  public TableHeader GetTableHeader(string tableName)
  {
    PXReaderWriterScope readerWriterScope;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._SpecificTablesLock);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
      if (this.IsSchemaLoaded())
      {
        TableHeader tableHeader;
        return this.dbTables.TryGetValue(tableName, out tableHeader) ? tableHeader : (TableHeader) null;
      }
      this.ensureCacheIsFilled();
      TableHeader tableHeader1;
      return this.dbTables.TryGetValue(tableName, out tableHeader1) ? tableHeader1 : (TableHeader) null;
    }
    finally
    {
      readerWriterScope.Dispose();
    }
  }

  private void ensureCacheIsFilled()
  {
    if (this.IsSchemaLoaded())
      return;
    LockCookie writerLock = this._SpecificTablesLock.UpgradeToWriterLock(-1);
    try
    {
      if (this.IsSchemaLoaded())
        return;
      this.reloadSchemaTime = new System.DateTime?();
      PointDbmsBase pointDbmsBase = this.pointFactory() as PointDbmsBase;
      pointDbmsBase.SchemaReader.OmitTriggersAndFks = true;
      IDataSchema schema;
      try
      {
        schema = pointDbmsBase.Schema;
      }
      catch (SqlException ex)
      {
        if (ex.Number == -2)
        {
          this.schemaLoaded = true;
          this.reloadSchemaTime = new System.DateTime?(System.DateTime.UtcNow.AddSeconds(60.0));
          return;
        }
        throw;
      }
      this.dbTables.Clear();
      this.companySettingsCache.Clear();
      this.schemaLoaded = true;
      foreach (TableHeader tableHeader in (IEnumerable<TableHeader>) schema)
        this.dbTables.Add(((TableEntityBase) tableHeader).Name, tableHeader);
      PxObjectsIntern<TableHeader> pxObjectsIntern = new PxObjectsIntern<TableHeader>();
      foreach (string key in this.dbTables.Keys.ToArray<string>())
      {
        TableHeader returnValue;
        if (pxObjectsIntern.TryIntern(this.dbTables[key], out returnValue))
          this.dbTables[key] = returnValue;
      }
    }
    finally
    {
      this._SpecificTablesLock.DowngradeFromWriterLock(ref writerLock);
    }
  }

  public IList<TableHeader> getAllTableHeaders()
  {
    PXReaderWriterScope readerWriterScope;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._SpecificTablesLock);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
      return (IList<TableHeader>) this.dbTables.Values.ToList<TableHeader>();
    }
    finally
    {
      readerWriterScope.Dispose();
    }
  }

  public TableIndex GetFullTextIndexOnTable(string table)
  {
    TableHeader tableHeader = this.GetTableHeader(table);
    return tableHeader == null ? (TableIndex) null : tableHeader.Indices.FirstOrDefault<TableIndex>((Func<TableIndex, bool>) (ix => ix.IsFullText));
  }

  internal TableIndex GetTableIndex(string table, string indexName)
  {
    TableHeader tableHeader = this.GetTableHeader(table);
    return tableHeader == null ? (TableIndex) null : tableHeader.Indices.FirstOrDefault<TableIndex>((Func<TableIndex, bool>) (ix => ((TableEntityBase) ix).Name.OrdinalEquals(indexName)));
  }

  public bool TableExists(string table) => this.GetTableHeader(table) != null;

  public IEnumerable<string> GetTableNames()
  {
    PXReaderWriterScope readerWriterScope;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._SpecificTablesLock);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
      this.ensureCacheIsFilled();
      return (IEnumerable<string>) new HashSet<string>((IEnumerable<string>) this.dbTables.Keys, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    }
    finally
    {
      readerWriterScope.Dispose();
    }
  }

  public bool IsSchemaLoaded()
  {
    PXReaderWriterScope readerWriterScope;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._SpecificTablesLock);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
      int num;
      if (this.schemaLoaded && this.dbTables.Any<KeyValuePair<string, TableHeader>>())
      {
        if (this.reloadSchemaTime.HasValue)
        {
          System.DateTime? reloadSchemaTime = this.reloadSchemaTime;
          System.DateTime utcNow = System.DateTime.UtcNow;
          num = reloadSchemaTime.HasValue ? (reloadSchemaTime.GetValueOrDefault() > utcNow ? 1 : 0) : 0;
        }
        else
          num = 1;
      }
      else
        num = 0;
      return num != 0;
    }
    finally
    {
      readerWriterScope.Dispose();
    }
  }
}
