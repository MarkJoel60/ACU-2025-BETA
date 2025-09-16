// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDatabaseProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.BulkInsert;
using PX.BulkInsert.Installer;
using PX.Common;
using PX.Common.Context;
using PX.Data.Database;
using PX.Data.Maintenance;
using PX.Data.SQLTree;
using PX.Data.Update;
using PX.DbServices.Model;
using PX.DbServices.Model.Entities;
using PX.DbServices.Points;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.QueryObjectModel;
using PX.Reports;
using PX.SM;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

#nullable disable
namespace PX.Data;

public abstract class PXDatabaseProvider
{
  private ILogger _logger = Serilog.Core.Logger.None;
  protected string companyID;
  protected int queryTimeout;
  protected bool secureCompanyID;
  protected ISqlDialect _sqlDialect;
  protected int reportQueryTimeout;
  protected string pApplicationName;
  protected int _version;
  private readonly ConcurrentDictionary<string, PXDatabase.PXTableAge> TableAge = new ConcurrentDictionary<string, PXDatabase.PXTableAge>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
  private long AgeLocal = 1;
  private bool hasReader;
  internal long AgeGlobal;
  protected ConcurrentDictionary<int, ConcurrentDictionary<string, PXDatabaseSlot>> _Slots = new ConcurrentDictionary<int, ConcurrentDictionary<string, PXDatabaseSlot>>();
  protected ConcurrentDictionary<string, string> _SlotSubcribers = new ConcurrentDictionary<string, string>();
  private const int _TimeOut = 300000;
  internal static readonly QueryHints DefaultQueryHints = WebConfig.SqlOptimizeForUnknown ? QueryHints.SqlServerOptimizeForUnknown : QueryHints.None;

  private protected ILogger Logger => this._logger;

  protected string ConnectionString { get; private set; }

  [PXInternalUseOnly]
  public string GetConnectionString() => this.ConnectionString;

  public int DefaultQueryTimeout => this.queryTimeout <= 0 ? 30 : this.queryTimeout;

  protected int TransactionQueryTimeout { get; private set; }

  public int GetReportQueryTimeout() => this.reportQueryTimeout;

  public string ApplicationName
  {
    get => this.pApplicationName;
    set => this.pApplicationName = value;
  }

  internal int Version
  {
    get => this._version;
    set => this._version = value;
  }

  public abstract IDbSchemaCache SchemaCache { get; }

  /// <summary>Initializes the database provider.</summary>
  /// <param name="config">The configuration section.</param>
  /// <exception cref="T:PX.Data.PXArgumentException"><see cref="!:config" /> is <see langword="null" />.</exception>
  protected internal virtual void Initialize(NameValueCollection config)
  {
    this.ConnectionString = config != null ? this.OverrideConnectionString(PXDatabase.GetConnectionString(config)) : throw new PXArgumentException(nameof (config), "The argument cannot be null.");
    this.companyID = config["companyID"];
    string s1 = config["queryTimeout"];
    if (!string.IsNullOrEmpty(s1))
      int.TryParse(s1, out this.queryTimeout);
    string s2 = config["transactionQueryTimeout"];
    int result;
    if (!string.IsNullOrEmpty(s2) && int.TryParse(s2, out result))
      this.TransactionQueryTimeout = result;
    string s3 = config["reportQueryTimeout"];
    if (!string.IsNullOrEmpty(s3))
      int.TryParse(s3, out this.reportQueryTimeout);
    string str = config["secureCompanyID"];
    if (!string.IsNullOrEmpty(str))
      bool.TryParse(str, out this.secureCompanyID);
    this.Subscribe(typeof (PX.Data.Update.Company), (PXDatabaseTableChanged) (() =>
    {
      if (!this.DatabaseDefinedCompanies)
        return;
      this.ClearCompaniesCache();
    }));
  }

  internal void InitializeLogger(System.Func<System.Type, ILogger> loggerFactory)
  {
    if (loggerFactory == null)
      throw new ArgumentNullException(nameof (loggerFactory));
    if (!(Interlocked.Exchange<ILogger>(ref this._logger, loggerFactory(this.GetType())) is IDisposable disposable))
      return;
    disposable.Dispose();
  }

  protected virtual string OverrideConnectionString(string cs) => cs;

  public virtual void InitializeRequest(int companyId)
  {
  }

  public abstract void InitSqlDialect();

  protected internal virtual void SetRestrictedTables(string[] list)
  {
  }

  protected internal virtual ConnectionDefinition GetConnectionDefinition()
  {
    return (ConnectionDefinition) null;
  }

  public ISqlDialect SqlDialect => this._sqlDialect;

  public virtual ISqlErrorMessageParser SqlErrorMessageParser => (ISqlErrorMessageParser) null;

  public abstract PointDbmsBase CreateDbServicesPoint(IDbTransaction openTransaction = null);

  /// <exclude />
  [PXInternalUseOnly]
  public PointDbmsBase CreateDbServicesPointWithCurrentTransaction()
  {
    return this.CreateDbServicesPoint((IDbTransaction) PXTransactionScope.GetTransaction());
  }

  [Obsolete("This method is obsolete and will be removed. Rewrite your code without this method or contact your partner for assistance.")]
  public IEnumerable<PXDataRecord> Select(
    PXGraph graph,
    BqlCommand command,
    long topCount,
    params PXDataValue[] pars)
  {
    return this.Select(graph, command, topCount, (PXView) null, pars);
  }

  [Obsolete("This method is obsolete and will be removed. Rewrite your code without this method or contact your partner for assistance.")]
  internal IEnumerable<PXDataRecord> Select(
    PXGraph graph,
    BqlCommand command,
    long topCount,
    PXView view,
    params PXDataValue[] pars)
  {
    return this.Select(graph, command, 0L, topCount, view, pars);
  }

  [Obsolete("This method is obsolete and will be removed. Rewrite your code without this method or contact your partner for assistance.")]
  public IEnumerable<PXDataRecord> Select(
    PXGraph graph,
    BqlCommand command,
    long skip,
    long topCount,
    params PXDataValue[] pars)
  {
    return this.Select(graph, command, skip, topCount, (PXView) null, pars);
  }

  [Obsolete("This method is obsolete and will be removed. Rewrite your code without this method or contact your partner for assistance.")]
  protected internal IEnumerable<PXDataRecord> Select(
    PXGraph graph,
    BqlCommand command,
    long skip,
    long topCount,
    PXView view,
    params PXDataValue[] pars)
  {
    return this.Select(command.GetQuery(graph, view, topCount, skip), (IEnumerable<PXDataValue>) pars);
  }

  /// <summary>
  /// Select collection of <see cref="T:PX.Data.PXDataRecord" /> from the database
  /// </summary>
  /// <remarks>
  /// Please use this method carefully because of its current default implementation in <see cref="T:PX.Data.PXDatabaseProviderBase" />.
  /// It returns result with active data reader and you have to close it properly either by using default iterator implementation like "foreach" and so on or by disposing enumerator of this collection explicitly.
  /// Also you have to use returned <see cref="T:PX.Data.PXDataRecord" /> while iterator is not disposed.
  /// </remarks>
  /// <param name="query">Query to be executed</param>
  /// <param name="queryParameters">Query parameters</param>
  /// <param name="configurator">Delegate to configure additional query execution parameters</param>
  /// <returns>Collection of <see cref="T:PX.Data.PXDataRecord" /></returns>
  internal abstract IEnumerable<PXDataRecord> Select(
    Query query,
    IEnumerable<PXDataValue> queryParameters,
    System.Action<PXDatabaseProvider.ExecutionParameters> configurator = null);

  internal IAsyncEnumerable<PXDataRecord> SelectAsyncEnumerable(
    CancellationToken token,
    PXGraph graph,
    BqlCommand command,
    long skip,
    long topCount,
    IEnumerable<PXDataValue> queryParameters,
    System.Action<PXDatabaseProvider.ExecutionParameters> configurator = null)
  {
    return this.SelectAsync(command.GetQuery(graph, (PXView) null, topCount, skip), queryParameters, configurator, token);
  }

  internal abstract IAsyncEnumerable<PXDataRecord> SelectAsync(
    Query query,
    IEnumerable<PXDataValue> queryParameters,
    System.Action<PXDatabaseProvider.ExecutionParameters> configurator = null,
    CancellationToken token = default (CancellationToken));

  public abstract PXDataRecord SelectSingle(System.Type table, params PXDataField[] pars);

  public abstract PXDataRecord SelectSingle(
    System.Type table,
    IEnumerable<YaqlJoin> joins = null,
    params PXDataField[] pars);

  public abstract IEnumerable<PXDataRecord> SelectMulti(System.Type table, params PXDataField[] pars);

  public abstract IEnumerable<PXDataRecord> SelectMulti(
    System.Type table,
    IEnumerable<YaqlJoin> joins = null,
    params PXDataField[] pars);

  public abstract TableDataSizeInfo GetTableDataSize(string tableName);

  internal abstract IEnumerable<PXDataRecord> SelectMulti(Query query, params PXDataValue[] pars);

  internal virtual bool? IsColumnLatin(string tableName, string columnName) => new bool?();

  internal virtual string MakeCharsetPrefix(string collation) => (string) null;

  public abstract bool Insert(System.Type table, params PXDataFieldAssign[] pars);

  public abstract bool Ensure(System.Type table, PXDataFieldAssign[] values, PXDataField[] pars);

  public abstract bool Update(System.Type table, params PXDataFieldParam[] pars);

  public abstract bool Archive(System.Type table, params PXDataFieldRestrict[] pars);

  public abstract bool Extract(System.Type table, params PXDataFieldRestrict[] pars);

  public abstract bool Delete(System.Type table, params PXDataFieldRestrict[] pars);

  public abstract bool ForceDelete(System.Type table, params PXDataFieldRestrict[] pars);

  internal abstract void Truncate(System.Type table);

  public abstract int Update(PXGraph graph, IBqlUpdate command, params PXDataValue[] pars);

  public abstract IEnumerable<string> GetTables();

  public abstract TableHeader GetTableStructure(string tableName);

  public abstract object[] Execute(string procedureName, params PXSPParameter[] pars);

  public abstract byte[] SelectTimeStamp();

  public abstract byte[] SelectCrossCompanyTimeStamp();

  public abstract IEnumerable<DataVersion> SelectVersions();

  internal abstract DatabaseInfo SelectDatabaseInfo();

  public abstract string SelectCollation();

  public abstract void SelectDate(out System.DateTime dtLocal, out System.DateTime dtUtc);

  public abstract bool Subscribe(System.Type table, PXDatabaseTableChanged handler);

  public abstract void UnSubscribe(System.Type table, PXDatabaseTableChanged handler);

  public abstract bool Subscribe(System.Type table, PXDatabaseTableChanged handler, string uniqueKey = null);

  public abstract void UnSubscribe(System.Type table, PXDatabaseTableChanged handler, string uniqueKey = null);

  public virtual void TransactionFinished()
  {
  }

  public abstract Decimal? SelectIdentity();

  public abstract IDataReader ExecuteReader(IDbCommand command);

  internal abstract Task<DbDataReader> ExecuteReaderAsync(
    DbCommand command,
    CancellationToken token);

  public abstract void OpenConnection(IDbConnection connection);

  protected internal abstract PXDataRecord CreateRecord(IDataReader reader, IDbCommand command);

  protected internal abstract PXDataRecord CreateRecord(
    IDataReader reader,
    IDbCommand command,
    StringTable stringTable);

  protected internal abstract PXDataRecord CreateXmlRecord(XElement rowElement);

  protected internal abstract DbTransaction CreateTransaction();

  protected internal abstract void Commit(IDbTransaction tran);

  protected internal abstract void Rollback(IDbTransaction tran);

  internal virtual DbTransaction GetTransaction() => PXTransactionScope.GetTransaction();

  protected internal abstract DbConnection CreateConnection();

  protected internal virtual DbConnection GetConnection() => PXConnectionScope.GetConnection();

  protected internal virtual DbCommand GetCommand()
  {
    return PXCommandScope.GetCommand() ?? this.CreateCommand();
  }

  protected internal virtual DbCommand CreateCommand()
  {
    DbTransaction transaction = this.GetTransaction();
    DbConnection connection = this.GetConnection() ?? this.CreateConnection();
    if (connection == null)
      return (DbCommand) null;
    if (connection.State == ConnectionState.Closed)
      this.OpenConnection((IDbConnection) connection);
    DbCommand command = connection.CreateCommand();
    command.Transaction = transaction;
    if (command.Transaction == null && this.queryTimeout > 0)
      command.CommandTimeout = this.DefaultQueryTimeout;
    if (command.Transaction != null && this.TransactionQueryTimeout > 0)
      command.CommandTimeout = this.TransactionQueryTimeout;
    return command;
  }

  protected internal virtual void LeaveConnection(IDbConnection connection)
  {
    if (connection == null || PXConnectionScope.IsScoped(connection))
      return;
    connection.Dispose();
  }

  internal virtual void LeaveCommand(IDbCommand command) => command?.Dispose();

  protected internal virtual bool HasActiveReader(IDbConnection connection) => false;

  public abstract string CreateParameterName(int ordinal);

  internal abstract void SaveAudit(List<PXDatabase.AuditTable> audit);

  internal virtual void SaveWatchDog(IEnumerable<System.Type> watchdog)
  {
  }

  public virtual bool AuditRequired(string screenID) => false;

  internal virtual bool AuditRequired(System.Type table) => false;

  public virtual string[] Companies => new string[0];

  public virtual bool SecureCompanyID => this.secureCompanyID;

  public virtual string[] DbCompanies => new string[0];

  protected internal virtual Dictionary<int, string> CompanyMappings
  {
    get => new Dictionary<int, string>(0);
  }

  public virtual void Enter()
  {
  }

  public virtual bool RequiresLogOut => false;

  protected void AllTablesChanged(long? id)
  {
    if (!WebConfig.IsClusterEnabled)
      throw new PXException("Cluster environment expected: AllTablesChanged");
    foreach (string key in (IEnumerable<string>) this.TableAge.Keys)
      this.TableChanged(key, id ?? this.AgeGlobal);
  }

  protected void TableChanged(string tableName, long timestamp)
  {
    if (!WebConfig.IsClusterEnabled)
      throw new PXException("Cluster environment expected: TableChanged");
    if (timestamp > this.AgeGlobal)
      this.AgeGlobal = timestamp;
    this.TableAge[tableName.ToLower()] = new PXDatabase.PXTableAge()
    {
      Global = this.AgeGlobal,
      Local = this.AgeLocal
    };
  }

  protected internal void TableChangedLocal(string tableName)
  {
    string lower = tableName.ToLower();
    if (this.hasReader)
    {
      ++this.AgeLocal;
      this.hasReader = false;
    }
    this.TableAge[lower] = new PXDatabase.PXTableAge()
    {
      Global = this.AgeGlobal,
      Local = this.AgeLocal
    };
  }

  internal PXDatabase.PXTableAge GetAge()
  {
    this.hasReader = true;
    return !WebConfig.IsClusterEnabled || this.AgeGlobal >= 0L ? new PXDatabase.PXTableAge()
    {
      Global = this.AgeGlobal,
      Local = this.AgeLocal
    } : throw new PXException("AgeGlobal is not initialized.");
  }

  internal bool IsTableModified(string tableName, PXDatabase.PXTableAge age)
  {
    PXDatabase.PXTableAge pxTableAge;
    if (!this.TableAge.TryGetValue(tableName, out pxTableAge))
      return false;
    if (WebConfig.IsClusterEnabled)
    {
      if (pxTableAge.Global > age.Global)
        return true;
      if (pxTableAge.Global < age.Global || age.Local == 0L)
        return false;
    }
    return pxTableAge.Local > age.Local;
  }

  public abstract DbmsMaintenance GetMaintenance(PointDbmsBase point = null, IExecutionObserver observer = null);

  protected internal static IExecutionObserver GetObserver(IExecutionObserver observer = null)
  {
    if (observer == null)
      return (IExecutionObserver) new DdlCommandLoggingExecutionObserver();
    if (observer is DdlCommandLoggingExecutionObserver)
      return observer;
    return (IExecutionObserver) new ChainExecutionObserver(new IExecutionObserver[2]
    {
      (IExecutionObserver) new DdlCommandLoggingExecutionObserver(),
      observer
    });
  }

  private static T prefetchMethod<T>(PrefetchDelegate<T> prefetchDelegate) where T : class, new()
  {
    T obj;
    try
    {
      using (new PXResourceGovernorSafeScope())
        obj = prefetchDelegate();
    }
    catch
    {
      obj = default (T);
    }
    return obj;
  }

  /// <remarks>
  /// <para>Will return <see langword="null" /> if an unhandled exception occurred during prefetch.</para>
  /// <para>If called from a background thread, an explicit <see cref="M:PX.Data.PXDatabaseProvider.SelectTimeStamp" />
  /// or <see cref="M:PX.Data.PXDatabaseProvider.SelectCrossCompanyTimeStamp" /> call is required beforehand.</para>
  /// <para>If <typeparamref name="ObjectType" /> is <see cref="T:PX.Data.ICrossCompanyPrefetchable" />
  /// or all <paramref name="tables" /> don't have the CompanyID column,
  /// <see cref="M:PX.Data.PXDatabaseProvider.SelectCrossCompanyTimeStamp" /> should be called before that.</para>
  /// </remarks>
  public virtual ObjectType GetSlot<ObjectType>(
    string key,
    PrefetchDelegate<ObjectType> prefetchDelegate,
    params System.Type[] tables)
    where ObjectType : class, new()
  {
    for (int index = tables.Length - 1; index >= 0; --index)
      key = $"{tables[index].Name}${key}";
    return (ObjectType) ConcurrentDictionaryExtensions.GetOrAddOrUpdate<string, PXDatabaseSlot>(this._Slots.GetOrAdd(0, new ConcurrentDictionary<string, PXDatabaseSlot>()), key, (System.Func<string, PXDatabaseSlot>) (i => new PXDatabaseSlot((Func<object>) (() => (object) PXDatabaseProvider.prefetchMethod<ObjectType>(prefetchDelegate)))), (Func<string, PXDatabaseSlot, PXDatabaseSlot>) ((j, existingVal) => existingVal == null || existingVal.IsValueFaulted || existingVal.Value == null ? new PXDatabaseSlot((Func<object>) (() => (object) PXDatabaseProvider.prefetchMethod<ObjectType>(prefetchDelegate))) : existingVal)).Value;
  }

  public virtual void ResetSlot<ObjectType>(string key, params System.Type[] tables) where ObjectType : class, new()
  {
    if (WebConfig.IsClusterEnabled)
      return;
    for (int index = tables.Length - 1; index >= 0; --index)
      key = $"{tables[index].Name}${key}";
    EnumerableExtensions.ForEach<KeyValuePair<int, ConcurrentDictionary<string, PXDatabaseSlot>>>((IEnumerable<KeyValuePair<int, ConcurrentDictionary<string, PXDatabaseSlot>>>) this._Slots, (System.Action<KeyValuePair<int, ConcurrentDictionary<string, PXDatabaseSlot>>>) (s => s.Value[key] = (PXDatabaseSlot) null));
  }

  public virtual void ResetSlots()
  {
    EnumerableExtensions.ForEach<KeyValuePair<int, ConcurrentDictionary<string, PXDatabaseSlot>>>((IEnumerable<KeyValuePair<int, ConcurrentDictionary<string, PXDatabaseSlot>>>) this._Slots, (System.Action<KeyValuePair<int, ConcurrentDictionary<string, PXDatabaseSlot>>>) (s => s.Value.Clear()));
  }

  public virtual void ResetSlotForAllCompanies(string key, params System.Type[] tables)
  {
    for (int index = tables.Length - 1; index >= 0; --index)
      key = $"{tables[index].Name}${key}";
    EnumerableExtensions.ForEach<KeyValuePair<int, ConcurrentDictionary<string, PXDatabaseSlot>>>((IEnumerable<KeyValuePair<int, ConcurrentDictionary<string, PXDatabaseSlot>>>) this._Slots, (System.Action<KeyValuePair<int, ConcurrentDictionary<string, PXDatabaseSlot>>>) (s =>
    {
      PXDatabaseSlot comparisonValue;
      if (!s.Value.TryGetValue(key, out comparisonValue) || comparisonValue == null)
        return;
      s.Value.TryUpdate(key, (PXDatabaseSlot) null, comparisonValue);
    }));
  }

  public virtual void ResetSlotForAllCompanies(string keyPrefix)
  {
    EnumerableExtensions.ForEach<KeyValuePair<int, ConcurrentDictionary<string, PXDatabaseSlot>>>((IEnumerable<KeyValuePair<int, ConcurrentDictionary<string, PXDatabaseSlot>>>) this._Slots, (System.Action<KeyValuePair<int, ConcurrentDictionary<string, PXDatabaseSlot>>>) (s => EnumerableExtensions.ForEach<KeyValuePair<string, PXDatabaseSlot>>((IEnumerable<KeyValuePair<string, PXDatabaseSlot>>) s.Value, (System.Action<KeyValuePair<string, PXDatabaseSlot>>) (slot =>
    {
      if (!slot.Key.EndsWith(keyPrefix) || slot.Value == null)
        return;
      s.Value.TryUpdate(slot.Key, (PXDatabaseSlot) null, slot.Value);
    }))));
  }

  internal void CleanupExpiredSlots(int slotTimeoutMinutes)
  {
    foreach (KeyValuePair<int, ConcurrentDictionary<string, PXDatabaseSlot>> slot in this._Slots)
    {
      ConcurrentDictionary<string, PXDatabaseSlot> concurrentDictionary = slot.Value;
      foreach (KeyValuePair<string, PXDatabaseSlot> keyValuePair in concurrentDictionary)
      {
        PXDatabaseSlot pxDatabaseSlot = keyValuePair.Value;
        if (pxDatabaseSlot != null && System.DateTime.Now.Subtract(pxDatabaseSlot.LastAccessTime).TotalMinutes > (double) slotTimeoutMinutes)
          concurrentDictionary[keyValuePair.Key] = (PXDatabaseSlot) null;
      }
    }
  }

  public virtual bool IsReadDeletedSupported(System.Type table) => false;

  public virtual bool IsReadDeletedSupported(System.Type table, out string fieldName)
  {
    fieldName = (string) null;
    return false;
  }

  public virtual bool IsRecordStatusSupported(System.Type table, out string fieldName)
  {
    fieldName = (string) null;
    return false;
  }

  public BqlFullTextRenderingMethod GetFullTextSearchCapability<Field>() where Field : IBqlField
  {
    return this.getFullTextRenderingMethod(typeof (Field).DeclaringType.Name, typeof (Field).Name);
  }

  public virtual BqlFullTextRenderingMethod getFullTextRenderingMethod(
    string tableName,
    string column)
  {
    return BqlFullTextRenderingMethod.NeutralLike;
  }

  public virtual void SetReadDeletedCapability(System.Type table, bool enabled)
  {
  }

  public virtual bool IsVirtualTable(System.Type table) => false;

  public bool DatabaseDefinedCompanies { get; protected set; }

  protected internal virtual bool TablesOrderedByClusteredIndexByDefault { get; } = true;

  protected internal virtual bool OrderByColumnsMustBeUnique { get; } = true;

  public abstract CompanyInfo[] SelectCompanies(bool includeNegative, bool includeMarkedForDeletion = false);

  internal virtual int getCompanyID(string tableName, out companySetting settings)
  {
    settings = new companySetting(companySetting.companyFlag.Separate);
    return 0;
  }

  public Dictionary<int, string> GetLoginCompanies()
  {
    Dictionary<int, string> loginCompanies = new Dictionary<int, string>();
    if (string.IsNullOrEmpty(this.companyID) || this.companyID.Trim() == "1")
      return loginCompanies;
    string companyId = this.companyID;
    string[] separator1 = new string[1]{ "," };
    foreach (string str in companyId.Split(separator1, StringSplitOptions.RemoveEmptyEntries))
    {
      char[] separator2 = new char[1]{ ';' };
      string[] strArray = str.Split(separator2, 2, StringSplitOptions.RemoveEmptyEntries);
      loginCompanies.Add(int.Parse(strArray[0]), strArray.Length > 1 ? strArray[1] : (string) null);
    }
    return loginCompanies;
  }

  public virtual void ClearCompaniesCache()
  {
  }

  public virtual string GetCompanyDisplayName() => (string) null;

  internal virtual int InitializeCurrentCompany(ISlotStore slots, IPrincipal principal)
  {
    throw new NotSupportedException();
  }

  public virtual void SetDesignTimeCompany()
  {
  }

  public virtual void ResetCredentials()
  {
  }

  public object GetDirectExpressionForReportParameter(ParameterType rpType, object effectiveValue)
  {
    return this.CorrectConstValue(((System.Func<ParameterType, PXDbType>) (rpt =>
    {
      switch ((int) rpt)
      {
        case 0:
          return PXDbType.Bit;
        case 1:
          return PXDbType.DateTime;
        case 2:
          return PXDbType.Float;
        case 3:
          return PXDbType.Int;
        default:
          return PXDbType.NVarChar;
      }
    }))(rpType), effectiveValue);
  }

  internal virtual object CorrectConstValue(PXDbType type, object effectiveValue)
  {
    if (type == PXDbType.Bit)
    {
      string str = effectiveValue as string;
      bool result;
      if (bool.TryParse(str, out result))
        return (object) result;
      switch (str)
      {
        case "1":
          return (object) true;
        case "0":
          return (object) false;
      }
    }
    return (uint) (type - 5) <= 1U && effectiveValue is string str1 && string.IsNullOrEmpty(str1) ? (object) null : effectiveValue;
  }

  internal void WithConnection(System.Action DoAction)
  {
    PXConnectionScope pxConnectionScope = (PXConnectionScope) null;
    try
    {
      IDbConnection connection = (IDbConnection) this.GetConnection();
      if (connection == null)
      {
        pxConnectionScope = new PXConnectionScope();
        connection = (IDbConnection) this.GetConnection();
      }
      if (connection.State == ConnectionState.Closed)
        connection.Open();
      DoAction();
    }
    finally
    {
      pxConnectionScope?.Dispose();
    }
  }

  [Obsolete]
  internal void ExecuteSqlBatch(string Script)
  {
    using (StringReader stringReader = new StringReader(Script))
    {
      while (true)
      {
        StringBuilder b = new StringBuilder();
        while (true)
        {
          string s = stringReader.ReadLine();
          if (!this.SqlDialect.isEndOfSQLStatement(s))
            b.AppendLine(s);
          else
            break;
        }
        if (b.Length != 0)
          this.WithConnection((System.Action) (() =>
          {
            using (IDbCommand command1 = (IDbCommand) this.GetCommand())
            {
              command1.CommandText = b.ToString();
              command1.CommandTimeout = 300000;
              DbAdapterBase.CorrectCommand(command1);
              if (command1.Connection.State != ConnectionState.Open)
                command1.Connection.Open();
              try
              {
                command1.ExecuteNonQuery();
              }
              catch (DbException ex)
              {
                if (command1.Connection.State == ConnectionState.Open)
                  throw;
                Thread.Sleep(5000);
                using (new PXConnectionScope())
                {
                  using (IDbCommand command2 = (IDbCommand) this.GetCommand())
                  {
                    command2.CommandText = b.ToString();
                    command2.CommandTimeout = 300000;
                    DbAdapterBase.CorrectCommand(command2);
                    if (command1.Connection.State != ConnectionState.Open)
                      command1.Connection.Open();
                    command2.ExecuteNonQuery();
                  }
                }
              }
            }
          }));
        else
          break;
      }
    }
  }

  internal abstract void SchemaCacheInvalidate(string singleTableName = null);

  protected internal sealed class ExecutionParameters
  {
    public int CommandTimeout { get; set; }

    public bool TraceQuery { get; set; }

    protected internal ExecutionParameters(int commandTimeout)
    {
      this.CommandTimeout = commandTimeout;
    }
  }

  internal class SchemaCacheInvalidateFlag : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }
}
