// Decompiled with JetBrains decompiler
// Type: PX.Data.Database.Common.PXDatabaseDummyProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.BulkInsert;
using PX.Data.SQLTree;
using PX.DbServices.Model;
using PX.DbServices.Model.Entities;
using PX.DbServices.Points;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.Points.MsSql;
using PX.DbServices.QueryObjectModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

#nullable disable
namespace PX.Data.Database.Common;

/// <exclude />
public class PXDatabaseDummyProvider : PXDatabaseProviderBase
{
  internal Dictionary<System.Type, object> PredefinedSlots = new Dictionary<System.Type, object>();
  private Decimal lastidentity;

  public PXDatabaseDummyProvider()
  {
    this.InitSqlDialect();
    this.schemaCache = this.CreateSchemaCache((string) null);
  }

  protected override IDbSchemaCache CreateSchemaCache(string key)
  {
    return (IDbSchemaCache) new PXDatabaseDummyProvider.DummyDbSchemaCache();
  }

  public override void InitializeRequest(int cpid)
  {
  }

  public override void InitSqlDialect()
  {
    this._sqlDialect = (ISqlDialect) new MsSqlDialect(new System.Version(10, 0), (SqlServerEngineEdition) 2);
  }

  public override DbmsMaintenance GetMaintenance(PointDbmsBase point = null, IExecutionObserver observer = null)
  {
    return (DbmsMaintenance) null;
  }

  protected internal override PXDataRecord CreateRecord(IDataReader reader, IDbCommand command)
  {
    return (PXDataRecord) null;
  }

  protected internal override PXDataRecord CreateRecord(
    IDataReader reader,
    IDbCommand command,
    StringTable stringTable)
  {
    return (PXDataRecord) null;
  }

  protected internal override PXDataRecord CreateXmlRecord(XElement rowElement)
  {
    return (PXDataRecord) null;
  }

  public override string SelectCollation() => (string) null;

  internal override DatabaseInfo SelectDatabaseInfo() => (DatabaseInfo) null;

  public override IEnumerable<string> GetTables()
  {
    return (IEnumerable<string>) new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  }

  protected override string ScriptParametersForProfiler(IDbCommand command) => (string) null;

  protected override string getFreeText(
    StringBuilder bld,
    string text,
    ref int start,
    int stop,
    string table,
    string alias,
    string column,
    string key)
  {
    return (string) null;
  }

  public override PXDatabaseException newDatabaseException(
    string tableName,
    object[] Keys,
    DbException dbex)
  {
    return (PXDatabaseException) null;
  }

  protected override IDataReader ExecuteReaderInternal(IDbCommand command, CommandBehavior behavior)
  {
    return (IDataReader) null;
  }

  /// <inheritdoc />
  protected override Task<DbDataReader> ExecuteReaderInternalAsync(
    DbCommand command,
    CommandBehavior behavior,
    CancellationToken token)
  {
    return (Task<DbDataReader>) null;
  }

  internal override IDbDataParameter _AddParameter(
    IDbCommand command,
    int parameterIndex,
    PXDbType type,
    int? size,
    ParameterDirection direction,
    object parameterValue,
    PXDatabaseProviderBase.ParameterBehavior behavior = PXDatabaseProviderBase.ParameterBehavior.Unknown)
  {
    return (IDbDataParameter) null;
  }

  protected override bool isInvalidObjectException(DbException dbException) => false;

  protected internal override Tuple<byte[], Decimal?> selectTimestamp()
  {
    return new Tuple<byte[], Decimal?>((byte[]) null, new Decimal?());
  }

  public override void OpenConnection(IDbConnection connection)
  {
  }

  protected override AuditSetup auditDefinition => new AuditSetup();

  public override bool RequiresLogOut => false;

  public override ObjectType GetSlot<ObjectType>(
    string key,
    PrefetchDelegate<ObjectType> prefetchDelegate,
    params System.Type[] tables)
  {
    try
    {
      object obj;
      return this.PredefinedSlots.TryGetValue(typeof (ObjectType), out obj) && obj is ObjectType objectType ? objectType : base.GetSlot<ObjectType>(key, prefetchDelegate, tables) ?? new ObjectType();
    }
    catch
    {
      return new ObjectType();
    }
  }

  public override byte[] SelectTimeStamp() => (byte[]) null;

  internal override void SaveAudit(List<PXDatabase.AuditTable> audit)
  {
  }

  public override bool AuditRequired(string screenID) => false;

  internal override bool AuditRequired(System.Type table) => false;

  public override bool IsReadDeletedSupported(System.Type table, out string fieldName)
  {
    fieldName = (string) null;
    return false;
  }

  public override bool IsReadDeletedSupported(System.Type table) => false;

  public override bool IsVirtualTable(System.Type table) => false;

  public override TableHeader GetTableStructure(string tableName) => (TableHeader) null;

  public override PointDbmsBase CreateDbServicesPoint(IDbTransaction boundConnection = null)
  {
    return (PointDbmsBase) null;
  }

  public override string[] Companies => new string[0];

  protected internal override bool tryGetSelectableCompanies(int cid, out int[] selectables)
  {
    selectables = (int[]) null;
    return false;
  }

  public override SqlScripterBase getScripter() => (SqlScripterBase) null;

  public override string[] DbCompanies => (string[]) null;

  public override object[] Execute(string procedureName, params PXSPParameter[] pars)
  {
    return (object[]) null;
  }

  internal override IEnumerable<PXDataRecord> Select(
    Query query,
    IEnumerable<PXDataValue> queryParameters,
    System.Action<PXDatabaseProvider.ExecutionParameters> configurator = null)
  {
    return (IEnumerable<PXDataRecord>) new PXSelectResult((PXDatabaseProvider) this, (Func<IDbCommand>) (() => (IDbCommand) null));
  }

  protected internal override DbConnection CreateConnection() => (DbConnection) null;

  protected internal override void Commit(IDbTransaction tran)
  {
  }

  protected internal override DbTransaction CreateTransaction() => (DbTransaction) null;

  protected internal override void Rollback(IDbTransaction tran)
  {
  }

  public override IEnumerable<DataVersion> SelectVersions()
  {
    yield return new DataVersion();
  }

  public override void SetDesignTimeCompany()
  {
  }

  public override int Update(PXGraph graph, IBqlUpdate command, params PXDataValue[] pars) => 1;

  internal override void LeaveCommand(IDbCommand command)
  {
  }

  public override void SetReadDeletedCapability(System.Type table, bool enabled)
  {
  }

  protected override string prepareTrace(PXDataValue[] pars, string text) => string.Empty;

  public override bool Insert(System.Type table, params PXDataFieldAssign[] pars)
  {
    PXTransactionScope.SetInsertedTable(table);
    return true;
  }

  public override bool Delete(System.Type table, params PXDataFieldRestrict[] pars) => true;

  public override void SelectDate(out System.DateTime dtLocal, out System.DateTime dtUtc)
  {
    dtLocal = System.DateTime.Now;
    dtUtc = System.DateTime.UtcNow;
  }

  internal override int getCompanyID(string tableName, out companySetting setting)
  {
    setting = new companySetting(companySetting.companyFlag.Dedicated);
    return 0;
  }

  public override string GetCompanyDisplayName() => "Tenant";

  protected internal override DbCommand CreateCommand() => this.GetConnection().CreateCommand();

  protected internal override DbConnection GetConnection()
  {
    return (DbConnection) new PXDatabaseDummyProvider.DummyConnection();
  }

  internal override DbTransaction GetTransaction() => this.GetConnection().BeginTransaction();

  protected internal override DbCommand GetCommand() => this.CreateCommand();

  public override Decimal? SelectIdentity() => new Decimal?(++this.lastidentity);

  public override PXDataRecord SelectSingle(
    System.Type table,
    IEnumerable<YaqlJoin> joins = null,
    params PXDataField[] pars)
  {
    return (PXDataRecord) null;
  }

  public override bool Ensure(System.Type table, PXDataFieldAssign[] values, PXDataField[] pars)
  {
    return true;
  }

  public override bool Update(System.Type table, params PXDataFieldParam[] pars) => true;

  internal override void Truncate(System.Type table)
  {
  }

  public override IEnumerable<PXDataRecord> SelectMulti(
    System.Type table,
    IEnumerable<YaqlJoin> joins = null,
    params PXDataField[] pars)
  {
    yield break;
  }

  public override TableDataSizeInfo GetTableDataSize(string tableName) => new TableDataSizeInfo();

  /// <exclude />
  private class DummyDbSchemaCache : IDbSchemaCache
  {
    public string DatabaseName => "DummyDatabase";

    public string DatabaseHost => "DummyHost";

    public IEnumerable<string> GetTableNames()
    {
      return (IEnumerable<string>) new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    }

    public TableHeader GetTableHeader(string tableName) => (TableHeader) null;

    public TableIndex GetFullTextIndexOnTable(string table) => (TableIndex) null;

    public bool TableExists(string table) => false;

    public void InvalidateAll()
    {
    }

    public void InvalidateTables(List<string> tablesToInvalidate)
    {
    }

    public companySetting getTableSetting(string tableName, bool selectablesHaveNoCompanyId)
    {
      return (companySetting) null;
    }

    public IList<TableHeader> getAllTableHeaders() => (IList<TableHeader>) new List<TableHeader>();

    public bool IsSchemaLoaded() => false;

    public void InvalidateTable(string singleTableName)
    {
    }
  }

  /// <exclude />
  private class DummyCommand : DbCommand
  {
    public DummyCommand()
    {
    }

    public DummyCommand(DbConnection connection)
    {
      this.DbConnection = connection;
      this.DbParameterCollection = (DbParameterCollection) new PXDatabaseDummyProvider.DummyDataParameterCollection();
    }

    public override void Prepare()
    {
    }

    public override string CommandText { get; set; }

    public override int CommandTimeout { get; set; }

    public override CommandType CommandType { get; set; }

    public override UpdateRowSource UpdatedRowSource { get; set; }

    public override void Cancel()
    {
    }

    protected override DbParameter CreateDbParameter()
    {
      return (DbParameter) new PXDatabaseDummyProvider.DummyParameter();
    }

    protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
    {
      return (DbDataReader) new PXDatabaseDummyProvider.DummyDataReader();
    }

    public override int ExecuteNonQuery() => 0;

    public override object ExecuteScalar() => (object) null;

    /// <inheritdoc />
    protected override DbConnection DbConnection { get; set; }

    protected override DbParameterCollection DbParameterCollection { get; }

    protected override DbTransaction DbTransaction { get; set; }

    public override bool DesignTimeVisible { get; set; }
  }

  /// <exclude />
  internal class DummyDataReader : DbDataReader
  {
    public override object this[string name] => (object) null;

    public override object this[int i] => (object) null;

    public override int Depth => 0;

    public override int FieldCount => 0;

    public override bool HasRows { get; }

    public override bool IsClosed => true;

    public override int RecordsAffected => 0;

    private T GetDefault<T>() => default (T);

    public override bool GetBoolean(int i) => this.GetDefault<bool>();

    public override byte GetByte(int i) => this.GetDefault<byte>();

    public override long GetBytes(
      int i,
      long fieldOffset,
      byte[] buffer,
      int bufferoffset,
      int length)
    {
      return this.GetDefault<long>();
    }

    public override char GetChar(int i) => this.GetDefault<char>();

    public override long GetChars(
      int i,
      long fieldoffset,
      char[] buffer,
      int bufferoffset,
      int length)
    {
      return this.GetDefault<long>();
    }

    public override string GetDataTypeName(int i) => typeof (string).Namespace;

    public override IEnumerator GetEnumerator() => throw new NotImplementedException();

    public override System.DateTime GetDateTime(int i) => this.GetDefault<System.DateTime>();

    public override Decimal GetDecimal(int i) => this.GetDefault<Decimal>();

    public override double GetDouble(int i) => this.GetDefault<double>();

    public override System.Type GetFieldType(int i) => typeof (string);

    public override float GetFloat(int i) => this.GetDefault<float>();

    public override Guid GetGuid(int i) => this.GetDefault<Guid>();

    public override short GetInt16(int i) => this.GetDefault<short>();

    public override int GetInt32(int i) => this.GetDefault<int>();

    public override long GetInt64(int i) => this.GetDefault<long>();

    public override string GetName(int i) => string.Empty;

    public override int GetOrdinal(string name) => this.GetDefault<int>();

    public override DataTable GetSchemaTable() => new DataTable();

    public override string GetString(int i) => string.Empty;

    public override object GetValue(int i) => (object) null;

    public override int GetValues(object[] values) => 0;

    public override bool IsDBNull(int i) => true;

    public override bool NextResult() => false;

    public override bool Read() => false;
  }

  /// <exclude />
  private class DummyParameter : DbParameter
  {
    public override void ResetDbType()
    {
    }

    public override DbType DbType { get; set; }

    public override ParameterDirection Direction { get; set; }

    public override bool IsNullable { get; set; }

    public override string ParameterName { get; set; }

    public override string SourceColumn { get; set; }

    public override bool SourceColumnNullMapping { get; set; }

    public override object Value { get; set; }

    public override byte Precision { get; set; }

    public override byte Scale { get; set; }

    public override int Size { get; set; }
  }

  /// <exclude />
  private class DummyDataParameterCollection : DbParameterCollection
  {
    private List<object> _list = new List<object>();

    /// <inheritdoc />
    public override int Add(object value) => ((IList) this._list).Add(value);

    /// <inheritdoc />
    public override bool Contains(object value) => this._list.Contains(value);

    /// <inheritdoc />
    public override void Clear() => this._list.Clear();

    /// <inheritdoc />
    public override int IndexOf(object value) => this._list.IndexOf(value);

    /// <inheritdoc />
    public override void Insert(int index, object value) => this._list.Insert(index, value);

    /// <inheritdoc />
    public override void Remove(object value) => this._list.Remove(value);

    /// <inheritdoc />
    public override void RemoveAt(int index) => this._list.RemoveAt(index);

    /// <inheritdoc />
    public override IEnumerator GetEnumerator() => (IEnumerator) this._list.GetEnumerator();

    /// <inheritdoc />
    protected override DbParameter GetParameter(int index) => (DbParameter) this._list[index];

    /// <inheritdoc />
    protected override DbParameter GetParameter(string parameterName)
    {
      return (DbParameter) this._list.FirstOrDefault<object>((System.Func<object, bool>) (p => ((DbParameter) p).ParameterName == parameterName));
    }

    /// <inheritdoc />
    public override void AddRange(Array values) => this._list.AddRange(values.OfType<object>());

    public override bool Contains(string parameterName)
    {
      return this._list.Any<object>((System.Func<object, bool>) (p => ((DbParameter) p).ParameterName == parameterName));
    }

    /// <inheritdoc />
    public override void CopyTo(Array array, int index)
    {
      ((ICollection) this._list).CopyTo(array, index);
    }

    /// <inheritdoc />
    public override int Count => this._list.Count;

    /// <inheritdoc />
    public override object SyncRoot => throw new NotSupportedException();

    public override int IndexOf(string parameterName)
    {
      object obj = this._list.FirstOrDefault<object>((System.Func<object, bool>) (p => ((IDataParameter) p).ParameterName == parameterName));
      return obj == null ? -1 : this._list.IndexOf(obj);
    }

    public override void RemoveAt(string parameterName)
    {
      object obj = this._list.FirstOrDefault<object>((System.Func<object, bool>) (p => ((IDataParameter) p).ParameterName == parameterName));
      if (obj == null)
        return;
      this._list.Remove(obj);
    }

    /// <inheritdoc />
    protected override void SetParameter(int index, DbParameter value)
    {
      this._list[index] = (object) value;
    }

    /// <inheritdoc />
    protected override void SetParameter(string parameterName, DbParameter value)
    {
      this._list[this.IndexOf(parameterName)] = (object) value;
    }
  }

  /// <exclude />
  private class DummyConnection : DbConnection
  {
    public override string ConnectionString { get; set; }

    public override int ConnectionTimeout => 60;

    public override string Database { get; }

    /// <inheritdoc />
    public override string DataSource { get; }

    /// <inheritdoc />
    public override string ServerVersion { get; }

    public override ConnectionState State => ConnectionState.Open;

    /// <inheritdoc />
    protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
    {
      return (DbTransaction) new PXDatabaseDummyProvider.DummyTransaction((DbConnection) this);
    }

    public override void ChangeDatabase(string databaseName)
    {
    }

    public override void Close()
    {
    }

    protected override DbCommand CreateDbCommand()
    {
      return (DbCommand) new PXDatabaseDummyProvider.DummyCommand((DbConnection) this);
    }

    public override void Open()
    {
    }
  }

  /// <exclude />
  private class DummyTransaction : DbTransaction
  {
    public DummyTransaction(DbConnection connection) => this.DbConnection = connection;

    protected override DbConnection DbConnection { get; }

    public override IsolationLevel IsolationLevel => IsolationLevel.Unspecified;

    public override void Commit()
    {
    }

    public override void Rollback()
    {
    }
  }
}
