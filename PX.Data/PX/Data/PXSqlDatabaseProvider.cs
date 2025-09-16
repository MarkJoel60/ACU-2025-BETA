// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSqlDatabaseProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Data.SqlClient;
using PX.BulkInsert;
using PX.BulkInsert.Installer;
using PX.Common.Extensions;
using PX.Data.Database;
using PX.Data.Database.MsSql;
using PX.DbServices.Model.Entities;
using PX.DbServices.Points;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.Points.MsSql;
using PX.SM;
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
namespace PX.Data;

public class PXSqlDatabaseProvider : PXDatabaseProviderBase
{
  private const int VARCHAR_MAX_SIZE = 8000;
  private const int NVARCHAR_MAX_SIZE = 4000;
  private const int MAX_SQL_STRING_LENGTH = 128 /*0x80*/;
  private const int MAX_SQL_BYTE_ARR_SIZE = 128 /*0x80*/;
  private MsSqlMaintenance maintenance;

  /// <inheritdoc />
  protected override string OverrideConnectionString(string cs)
  {
    DbConnectionStringBuilder connectionStringBuilder = new DbConnectionStringBuilder();
    connectionStringBuilder.ConnectionString = cs;
    if (!connectionStringBuilder.ContainsKey("Encrypt"))
    {
      connectionStringBuilder["Encrypt"] = (object) "false";
      cs = connectionStringBuilder.ConnectionString;
    }
    return cs;
  }

  public override ISqlErrorMessageParser SqlErrorMessageParser
  {
    get => (ISqlErrorMessageParser) new MsSqlErrorMessageParser();
  }

  public override PointDbmsBase CreateDbServicesPoint(IDbTransaction openTransaction = null)
  {
    PointMsSqlServer dbServicesPoint = openTransaction == null ? new PointMsSqlServer(this.ConnectionString) : new PointMsSqlServer(openTransaction.Connection, openTransaction);
    ((PointDbmsBase) dbServicesPoint).DefaultCommandTimeout = this.DefaultQueryTimeout;
    if (this.schemaCache.IsSchemaLoaded())
      ((PointDbmsBase) dbServicesPoint).UseAsSchema((IEnumerable<TableHeader>) this.schemaCache.getAllTableHeaders());
    return (PointDbmsBase) dbServicesPoint;
  }

  protected internal override ConnectionDefinition GetConnectionDefinition()
  {
    return ((BaseInstallProvider) new MSSqlInstallProvider((ConnectionDefinition) null, (IExecutionObserver) null)).ParseConnectionString(this.ConnectionString);
  }

  public override DbmsMaintenance GetMaintenance(PointDbmsBase point = null, IExecutionObserver observer = null)
  {
    return point != null || observer != null ? (DbmsMaintenance) new MsSqlMaintenance((PointMsSqlServer) point, PXDatabaseProvider.GetObserver(observer)) : (DbmsMaintenance) this.maintenance ?? (DbmsMaintenance) (this.maintenance = new MsSqlMaintenance(this.CreateDbServicesPoint((IDbTransaction) null) as PointMsSqlServer, PXDatabaseProvider.GetObserver()));
  }

  protected override bool isInvalidObjectException(DbException e)
  {
    return e is SqlException && ((SqlException) e).Number == 208 /*0xD0*/;
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
    return this._AddParameter(command, "P" + parameterIndex.ToString(), type, size, direction, parameterValue);
  }

  private IDbDataParameter _AddParameter(
    IDbCommand command,
    string parameterName,
    PXDbType type,
    int? size,
    ParameterDirection direction,
    object parameterValue)
  {
    SqlParameter sqlParameter = (SqlParameter) null;
    if (command is SqlCommand sqlCommand)
    {
      if (type != PXDbType.Unspecified)
      {
        int? nullable;
        if (type.IsString() && size.HasValue && parameterValue is string str)
        {
          nullable = size;
          int length = str.Length;
          if (nullable.GetValueOrDefault() < length & nullable.HasValue)
            size = new int?(str.Length);
        }
        if (type == PXDbType.Char || type == PXDbType.VarChar)
        {
          nullable = size;
          int num = 8000;
          if (nullable.GetValueOrDefault() > num & nullable.HasValue)
            goto label_9;
        }
        if (type == PXDbType.NChar || type == PXDbType.NVarChar)
        {
          nullable = size;
          int num = 4000;
          if (!(nullable.GetValueOrDefault() > num & nullable.HasValue))
            goto label_10;
        }
        else
          goto label_10;
label_9:
        size = new int?(-1);
label_10:
        sqlParameter = size.HasValue ? sqlCommand.Parameters.Add(parameterName, (SqlDbType) type, size.Value) : sqlCommand.Parameters.Add(parameterName, (SqlDbType) type);
        int result;
        if (type == PXDbType.Bit && parameterValue is string s && int.TryParse(s, out result))
          parameterValue = (object) result;
        ((DbParameter) sqlParameter).Value = parameterValue ?? (object) DBNull.Value;
      }
      else
        sqlParameter = sqlCommand.Parameters.AddWithValue(parameterName, parameterValue ?? (object) DBNull.Value);
      ((DbParameter) sqlParameter).Direction = direction;
    }
    return (IDbDataParameter) sqlParameter;
  }

  public override object[] Execute(string procedureName, params PXSPParameter[] pars)
  {
    IDbCommand command = (IDbCommand) null;
    try
    {
      command = (IDbCommand) this.GetCommand();
      command.CommandText = procedureName;
      command.CommandType = CommandType.StoredProcedure;
      foreach (PXSPParameter par in pars)
      {
        this._AddParameter(command, par.Column.Name, par.ValueType, par.ValueLength, par.Direction, par.Value);
        if (par.Scale.HasValue && par.ValueLength.HasValue)
        {
          ((SqlParameter) command.Parameters[command.Parameters.Count - 1]).Precision = (byte) par.ValueLength.Value;
          int? valueLength = par.ValueLength;
          int num = 17;
          if (valueLength.GetValueOrDefault() < num & valueLength.HasValue)
            ((DbParameter) command.Parameters[command.Parameters.Count - 1]).Size = 17;
          ((SqlParameter) command.Parameters[command.Parameters.Count - 1]).Scale = (byte) par.Scale.Value;
        }
      }
      if (command.Connection.State == ConnectionState.Closed)
        this.OpenConnection(command.Connection);
      command.ExecuteNonQuery();
      List<object> objectList = new List<object>();
      foreach (IDataParameter parameter in (IEnumerable) command.Parameters)
      {
        if ((parameter.Direction & ParameterDirection.Output) == ParameterDirection.Output)
          objectList.Add(parameter.Value);
      }
      return objectList.ToArray();
    }
    finally
    {
      if (command != null)
      {
        this.LeaveConnection(command.Connection);
        command.Dispose();
      }
    }
  }

  public override string SelectCollation()
  {
    IDbCommand dbCommand = (IDbCommand) null;
    IDataReader dataReader = (IDataReader) null;
    try
    {
      dbCommand = (IDbCommand) this.GetCommand();
      dbCommand.CommandText = "SELECT CONVERT(NVARCHAR, DATABASEPROPERTYEX(db_name(),'collation'))";
      if (dbCommand.Connection.State == ConnectionState.Closed)
        this.OpenConnection(dbCommand.Connection);
      dataReader = dbCommand.ExecuteReader();
      return dataReader.Read() && !dataReader.IsDBNull(0) ? dataReader.GetString(0) : (string) null;
    }
    finally
    {
      dataReader?.Dispose();
      if (dbCommand != null)
      {
        this.LeaveConnection(dbCommand.Connection);
        dbCommand.Dispose();
      }
    }
  }

  internal override DatabaseInfo SelectDatabaseInfo()
  {
    IDbCommand dbCommand = (IDbCommand) null;
    IDataReader dataReader = (IDataReader) null;
    try
    {
      dbCommand = (IDbCommand) this.GetCommand();
      dbCommand.CommandText = "SELECT @@servername, SERVERPROPERTY('MachineName'), SERVERPROPERTY ('InstanceName'), db_id(), db_name()";
      if (dbCommand.Connection.State == ConnectionState.Closed)
        this.OpenConnection(dbCommand.Connection);
      dataReader = dbCommand.ExecuteReader();
      if (dataReader.Read())
      {
        if (!dataReader.IsDBNull(3))
          return new DatabaseInfo()
          {
            ServerName = dataReader.IsDBNull(0) ? (string) null : dataReader.GetString(0),
            MachineName = dataReader.IsDBNull(1) ? (string) null : dataReader.GetString(1),
            InstanceName = dataReader.IsDBNull(2) ? (string) null : dataReader.GetString(2),
            DatabaseID = dataReader.IsDBNull(3) ? (string) null : dataReader.GetInt16(3).ToString(),
            DatabaseName = dataReader.IsDBNull(4) ? (string) null : dataReader.GetString(4)
          };
      }
    }
    finally
    {
      dataReader?.Dispose();
      if (dbCommand != null)
      {
        this.LeaveConnection(dbCommand.Connection);
        dbCommand.Dispose();
      }
    }
    return (DatabaseInfo) null;
  }

  protected internal override DbConnection CreateConnection()
  {
    SqlConnection result = new SqlConnection(this.ConnectionString);
    PXConnectionList.AddConnection((DbConnection) result);
    return (DbConnection) result;
  }

  internal static string TimestampToString(byte[] tstamp)
  {
    return $"0x{BitConverter.ToUInt64(((IEnumerable<byte>) tstamp).Reverse<byte>().ToArray<byte>(), 0):X16}";
  }

  private string scriptParametersForProfiler(SqlCommand cmd)
  {
    if (((DbParameterCollection) cmd.Parameters).Count == 0)
      return string.Empty;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("declare ");
    bool flag = true;
    foreach (SqlParameter parameter in (DbParameterCollection) cmd.Parameters)
    {
      string str1 = parameter.SqlDbType.ToString().ToLowerInvariant();
      if (str1.Contains("char"))
      {
        string str2 = ((DbParameter) parameter).Size.ToString();
        if (((DbParameter) parameter).Size < 0)
          str2 = "max";
        if (((DbParameter) parameter).Size == 0)
        {
          switch (str1)
          {
            case "varchar":
              str2 = "8000";
              break;
            case "nvarchar":
              str2 = "4000";
              break;
          }
        }
        str1 = $"{str1}({str2})";
      }
      if (!flag)
        stringBuilder.AppendLine(", ");
      else
        flag = false;
      stringBuilder.AppendFormat("@{0} as {1} = ", (object) ((DbParameter) parameter).ParameterName, (object) str1);
      switch (((DbParameter) parameter).Value)
      {
        case string str3:
          if (str3.Length > 128 /*0x80*/)
          {
            ((SqlScripterBase) PointMsSqlServer.GenericScripter).writeValueToQuery(stringBuilder, SqlDbType.VarChar, (object) $"Too large string - {str3.Length} length. Start of string: {StringExtensions.Truncate(str3, 128 /*0x80*/)}", (List<byte[]>) null);
            continue;
          }
          break;
        case byte[] tstamp:
          byte[] numArray = tstamp;
          if (numArray != null)
          {
            if (parameter.SqlDbType != SqlDbType.Timestamp)
            {
              if (numArray.Length > 128 /*0x80*/)
              {
                ((SqlScripterBase) PointMsSqlServer.GenericScripter).writeValueToQuery(stringBuilder, SqlDbType.VarChar, (object) $"Too large value - {tstamp.Length} bytes", (List<byte[]>) null);
                continue;
              }
            }
            else
            {
              stringBuilder.Append(PXSqlDatabaseProvider.TimestampToString(tstamp));
              continue;
            }
          }
          ((SqlScripterBase) PointMsSqlServer.GenericScripter).writeValueToQuery(stringBuilder, parameter.SqlDbType, ((DbParameter) parameter).Value, (List<byte[]>) null);
          continue;
      }
      ((SqlScripterBase) PointMsSqlServer.GenericScripter).writeValueToQuery(stringBuilder, parameter.SqlDbType, ((DbParameter) parameter).Value, (List<byte[]>) null);
    }
    return stringBuilder.ToString();
  }

  public override SqlScripterBase getScripter()
  {
    return (SqlScripterBase) PointMsSqlServer.GenericScripter;
  }

  protected override IDataReader ExecuteReaderInternal(IDbCommand command, CommandBehavior behavior)
  {
    PXPerformanceInfo pxPerformanceInfo = (PXPerformanceInfo) null;
    PXProfilerSqlSample profilerSqlSample = (PXProfilerSqlSample) null;
    if (PXPerformanceMonitor.IsEnabled)
    {
      pxPerformanceInfo = PXPerformanceMonitor.CurrentSample;
      if (pxPerformanceInfo != null)
      {
        ++pxPerformanceInfo.SqlCounter;
        pxPerformanceInfo.SqlTimer.Start();
        if (PXPerformanceMonitor.SqlProfilerEnabled)
        {
          profilerSqlSample = pxPerformanceInfo.AddSqlSample(command.CommandText, this.scriptParametersForProfiler(command as SqlCommand));
          profilerSqlSample?.SqlTimer.Start();
        }
      }
    }
    IDataReader dataReader = (IDataReader) null;
    PXCancellationToken cancellationToken = PXCancellationToken.Ensure(false);
    if (cancellationToken != null)
      cancellationToken.AsyncCancelAction = new System.Action(command.Cancel);
    try
    {
      dataReader = command.ExecuteReader(behavior);
      if (profilerSqlSample != null)
        profilerSqlSample.Reader = dataReader;
    }
    catch (SqlException ex)
    {
      if (ex.Number == 207)
      {
        PXSqlDatabaseProvider.throwInvalidColumnName(ex, command);
      }
      else
      {
        if (ex.Number == -2)
          throw new PXDatabaseException((string) null, (object[]) null, PXDbExceptions.Timeout, ((Exception) ex).Message, (Exception) ex);
        if (ex.Number == 7601)
          throw new PXDatabaseException((string) null, (object[]) null, PXDbExceptions.FullTextSearchDisabled, ((Exception) ex).Message, (Exception) ex);
        throw;
      }
    }
    finally
    {
      if (cancellationToken != null)
        cancellationToken.AsyncCancelAction = (System.Action) null;
      if (pxPerformanceInfo != null)
      {
        pxPerformanceInfo.SqlTimer.Stop();
        profilerSqlSample?.SqlTimer.Stop();
      }
    }
    return dataReader;
  }

  /// <inheritdoc />
  protected override async Task<DbDataReader> ExecuteReaderInternalAsync(
    DbCommand command,
    CommandBehavior behavior,
    CancellationToken token)
  {
    PXPerformanceInfo sample = (PXPerformanceInfo) null;
    PXProfilerSqlSample sqlSample = (PXProfilerSqlSample) null;
    if (PXPerformanceMonitor.IsEnabled)
    {
      sample = PXPerformanceMonitor.CurrentSample;
      if (sample != null)
      {
        ++sample.SqlCounter;
        sample.SqlTimer.Start();
        if (PXPerformanceMonitor.SqlProfilerEnabled)
        {
          sqlSample = sample.AddSqlSample(command.CommandText, this.scriptParametersForProfiler(command as SqlCommand));
          sqlSample?.SqlTimer.Start();
        }
      }
    }
    DbDataReader res = (DbDataReader) null;
    PXCancellationToken pxToken = PXCancellationToken.Ensure(false);
    if (pxToken != null)
      pxToken.AsyncCancelAction = new System.Action(command.Cancel);
    try
    {
      res = await command.ExecuteReaderAsync(behavior, token);
      if (sqlSample != null)
        sqlSample.Reader = (IDataReader) res;
    }
    catch (SqlException ex)
    {
      if (ex.Number == 207)
      {
        PXSqlDatabaseProvider.throwInvalidColumnName(ex, (IDbCommand) command);
      }
      else
      {
        if (ex.Number == -2)
          throw new PXDatabaseException((string) null, (object[]) null, PXDbExceptions.Timeout, ((Exception) ex).Message, (Exception) ex);
        throw;
      }
    }
    finally
    {
      if (pxToken != null)
        pxToken.AsyncCancelAction = (System.Action) null;
      if (sample != null)
      {
        sample.SqlTimer.Stop();
        sqlSample?.SqlTimer.Stop();
      }
    }
    DbDataReader dbDataReader = res;
    sample = (PXPerformanceInfo) null;
    sqlSample = (PXProfilerSqlSample) null;
    res = (DbDataReader) null;
    pxToken = (PXCancellationToken) null;
    return dbDataReader;
  }

  public override int executeVisibilityUpdate(
    IDbCommand cmd,
    string querySelectVisibility,
    int nextParamIndex,
    out bool wasVisible)
  {
    IDbDataParameter dbDataParameter = this._AddParameter(cmd, nextParamIndex, PXDbType.Int, new int?(4), ParameterDirection.Output, (object) null, PXDatabaseProviderBase.ParameterBehavior.Unknown);
    int num = cmd.ExecuteNonQuery();
    wasVisible = num == 1 && Convert.ToInt32(dbDataParameter.Value) != 0;
    return num;
  }

  private static void throwInvalidColumnName(SqlException e, IDbCommand cmd)
  {
    List<string> stringList = new List<string>();
    int startIndex1;
    for (int index = ((Exception) e).Message.IndexOf('\''); index >= 0 && index < ((Exception) e).Message.Length - 1; index = ((Exception) e).Message.IndexOf('\'', startIndex1))
    {
      int startIndex2 = index + 1;
      int num = ((Exception) e).Message.IndexOf('\'', startIndex2);
      stringList.Add(((Exception) e).Message.Substring(startIndex2, num - startIndex2));
      startIndex1 = num + 1;
    }
    StringBuilder stringBuilder = new StringBuilder();
    foreach (string str in stringList)
    {
      int num1 = cmd.CommandText.IndexOf("." + str);
      int num2 = num1 + 1 + str.Length;
      if (num1 == -1 || num1 == 0)
      {
        num1 = cmd.CommandText.IndexOf(".[" + str);
        num2 = num1 + 3 + str.Length;
      }
      if (num1 != -1 && num1 != 0)
      {
        while (!char.IsWhiteSpace(cmd.CommandText[num1]) && num1 != 0)
          --num1;
        stringBuilder.AppendLine(PXMessages.LocalizeFormatNoPrefix("Invalid column name '{0}'", (object) cmd.CommandText.Substring(num1, num2 - num1).Trim()));
      }
    }
    if (stringBuilder.Length > 0)
      throw new Exception(stringBuilder.ToString());
    throw e;
  }

  public override void OpenConnection(IDbConnection connection)
  {
    for (int index = 0; index <= 3; ++index)
    {
      try
      {
        connection.Open();
        break;
      }
      catch (SqlException ex)
      {
        connection.Dispose();
        if (ex.Number == 17806 || ex.Number == 17829 || ex.Number == 17830)
          throw new PXException($"The system is unable to connect to the database. The following error code has been returned: {ex.Number}.");
        throw;
      }
      catch (InvalidOperationException ex)
      {
        if (ex.Message.IndexOf("The timeout period elapsed before the connection to the pool has been established.") != -1 || ex.Message.IndexOf("The timeout period elapsed prior to obtaining a connection from the pool.") != -1)
        {
          connection.Close();
          SqlConnection.ClearPool((SqlConnection) connection);
          if (index > 0)
            Thread.Sleep((int) System.Math.Pow((double) (index + 1), 2.0) * 100);
        }
        else
        {
          connection.Dispose();
          throw;
        }
      }
    }
  }

  protected internal override PXDataRecord CreateRecord(IDataReader reader, IDbCommand command)
  {
    return new PXDataRecord(reader, command, (PXDatabaseProvider) this);
  }

  protected internal override PXDataRecord CreateRecord(
    IDataReader reader,
    IDbCommand command,
    StringTable stringTable)
  {
    return new PXDataRecord(reader, command, (PXDatabaseProvider) this, stringTable);
  }

  protected internal override PXDataRecord CreateXmlRecord(XElement rowElement)
  {
    return (PXDataRecord) new PXXmlDataRecord(rowElement);
  }

  public override PXDatabaseException newDatabaseException(
    string tableName,
    object[] Keys,
    DbException dbex)
  {
    SqlException sqlException = (SqlException) dbex;
    switch (sqlException.Number)
    {
      case -2:
        return new PXDatabaseException(tableName, Keys, PXDbExceptions.Timeout, ((Exception) sqlException).Message, (Exception) sqlException);
      case 547:
        return this.CreateConstraintStatementError(tableName, Keys, sqlException);
      case 1205:
        return new PXDatabaseException(tableName, Keys, PXDbExceptions.Deadlock, ((Exception) sqlException).Message, (Exception) sqlException);
      case 2601:
      case 2627:
        return new PXDatabaseException(tableName, Keys, PXDbExceptions.PrimaryKeyConstraintViolation, ((Exception) sqlException).Message, (Exception) sqlException);
      case 8152:
        return (PXDatabaseException) new PXDataWouldBeTruncatedException(tableName, (Exception) sqlException);
      case 14005:
      case 22549:
      case 33103:
      case 33141:
        string message = ((Exception) sqlException).Message.Replace("' exists for this '", PXMessages.LocalizeNoPrefix("' exists for this '"));
        return new PXDatabaseException(tableName, Keys, PXDbExceptions.DeleteForeignKeyConstraintViolation, message, (Exception) sqlException)
        {
          IsFriendlyMessage = true
        };
      default:
        return this.CreateUnknownDatabaseException(tableName, Keys, sqlException);
    }
  }

  private PXDatabaseException CreateUnknownDatabaseException(
    string tableName,
    object[] keys,
    SqlException e)
  {
    return new PXDatabaseException(tableName, keys, PXDbExceptions.Unknown, ((Exception) e).Message, (Exception) e);
  }

  private PXDatabaseException CreateConstraintStatementError(
    string tableName,
    object[] Keys,
    SqlException e)
  {
    ConstraintStatementSqlError statementSqlError = this.SqlErrorMessageParser.AsConstraintStatementSqlError(e, 1033);
    if (statementSqlError == null)
      return this.CreateUnknownDatabaseException(tableName, Keys, e);
    if (statementSqlError.IsDeleteStatement && statementSqlError.HasFkConstraint)
      return new PXDatabaseException(statementSqlError.Table, Keys, PXDbExceptions.DeleteForeignKeyConstraintViolation, ((Exception) e).Message, (Exception) e);
    if (statementSqlError.IsDeleteStatement && statementSqlError.HasRefConstraint)
      return new PXDatabaseException(statementSqlError.Table, Keys, PXDbExceptions.DeleteReferenceConstraintViolation, PXMessages.LocalizeFormatNoPrefix("This record is referenced by at least one '{0}' record. The operation cannot be completed.", (object) statementSqlError.Table), (Exception) e)
      {
        IsFriendlyMessage = true
      };
    if (statementSqlError.IsInsertStatement && statementSqlError.HasFkConstraint)
      return new PXDatabaseException(statementSqlError.Table, Keys, PXDbExceptions.InsertForeignKeyConstraintViolation, ((Exception) e).Message, (Exception) e);
    return statementSqlError.IsUpdateStatement && statementSqlError.HasFkConstraint ? new PXDatabaseException(statementSqlError.Table, Keys, PXDbExceptions.UpdateForeignKeyConstraintViolation, ((Exception) e).Message, (Exception) e) : this.CreateUnknownDatabaseException(tableName, Keys, e);
  }

  public override void SetReadDeletedCapability(System.Type table, bool wantEnabled)
  {
    companySetting tableSetting = this.schemaCache.getTableSetting(table.Name, false);
    if (string.IsNullOrEmpty(tableSetting.Deleted) != wantEnabled || !wantEnabled && tableSetting.Deleted.StartsWith("Usr", StringComparison.OrdinalIgnoreCase))
      return;
    IDbCommand command = (IDbCommand) null;
    try
    {
      command = (IDbCommand) this.GetCommand();
      if (!wantEnabled)
      {
        command.CommandText = $"BEGIN TRAN; DELETE dbo.[{table.Name}] WITH (TABLOCKX) WHERE {tableSetting.Deleted} = 1; EXEC pp_DropConstraint @P0, @P1; ALTER TABLE dbo.[{table.Name}] DROP COLUMN {tableSetting.Deleted}; INSERT WatchDog (CompanyID, TableName) SELECT CompanyID, @P2 FROM Company; COMMIT";
        this._AddParameter(command, 0, PXDbType.VarChar, new int?(100), ParameterDirection.Input, (object) table.Name, PXDatabaseProviderBase.ParameterBehavior.Unknown);
        this._AddParameter(command, 1, PXDbType.VarChar, new int?(100), ParameterDirection.Input, (object) tableSetting.Deleted, PXDatabaseProviderBase.ParameterBehavior.Unknown);
        this._AddParameter(command, 2, PXDbType.VarChar, new int?(100), ParameterDirection.Input, (object) ("--" + table.Name), PXDatabaseProviderBase.ParameterBehavior.Unknown);
      }
      else
      {
        command.CommandText = $"ALTER TABLE dbo.[{table.Name}] ADD UsrDeletedDatabaseRecord BIT NOT NULL DEFAULT (0); INSERT WatchDog (CompanyID, TableName) SELECT CompanyID, @P0 FROM Company";
        this._AddParameter(command, 0, PXDbType.VarChar, new int?(100), ParameterDirection.Input, (object) ("--" + table.Name), PXDatabaseProviderBase.ParameterBehavior.Unknown);
      }
      if (command.Connection.State == ConnectionState.Closed)
        this.OpenConnection(command.Connection);
      command.ExecuteNonQuery();
    }
    catch
    {
    }
    finally
    {
      if (command != null)
      {
        this.LeaveConnection(command.Connection);
        command.Dispose();
      }
    }
    this.SchemaCacheInvalidate(table.Name);
  }

  protected internal override Tuple<byte[], Decimal?> selectTimestamp()
  {
    using (PXDataRecord pxDataRecord = this.readSingleRecord(this.emptyPars, new StringBuilder($"SELECT {this._sqlDialect.getCurrentTimestamp()}, IDENT_CURRENT('WatchDog')").ToString()))
    {
      if (pxDataRecord != null)
      {
        byte[] buffer = new byte[8];
        pxDataRecord.GetBytes(0, 0L, buffer, 0, 8);
        return Tuple.Create<byte[], Decimal?>(buffer, pxDataRecord.GetDecimal(1));
      }
    }
    return (Tuple<byte[], Decimal?>) null;
  }

  public override void InitSqlDialect()
  {
    PointMsSqlServer dbServicesPoint = (PointMsSqlServer) this.CreateDbServicesPoint((IDbTransaction) null);
    this._sqlDialect = (ISqlDialect) new MsSqlDialect(((PointDbmsBase) dbServicesPoint).DbmsVersion, dbServicesPoint.getEngineEdition());
  }

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
    int start1 = start;
    if (!StringRoutines.byPassStringConstant(text, ref start1, stop) && !StringRoutines.byPassParameter(text, ref start1, stop))
      return (string) null;
    if (!"DECLARE @".Equals(bld.ToString(0, 9), StringComparison.OrdinalIgnoreCase))
      bld.Insert(0, "SELECT ");
    bld.Insert(0, string.Format("DECLARE @{0} TABLE ([KEY] UNIQUEIDENTIFIER NOT NULL, [RANK] INT NOT NULL, PRIMARY KEY CLUSTERED ([KEY])) INSERT @{0} SELECT CONVERT(UNIQUEIDENTIFIER, CONVERT(CHAR(36), [KEY])), MAX(RANK) FROM FREETEXTTABLE({1}, {2}, {3}) GROUP BY CONVERT(CHAR(36), [KEY]) ", (object) alias, (object) table, (object) column, (object) text.Substring(start, start1 - start)));
    string freeText = string.Format("(SELECT RANK FROM @{0} r WHERE r.[KEY] = {0}.{1})", (object) alias, (object) key);
    start = start1;
    return freeText;
  }

  protected override string ScriptParametersForProfiler(IDbCommand command)
  {
    return this.scriptParametersForProfiler((SqlCommand) command);
  }

  protected override string prepareTrace(PXDataValue[] pars, string text)
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < pars.Length; ++index)
    {
      if (pars[index].ValueType != PXDbType.DirectExpression)
      {
        stringBuilder.AppendFormat("DECLARE @P{0} ", (object) index);
        if (pars[index].ValueType != PXDbType.Unspecified)
        {
          stringBuilder.Append((object) pars[index].ValueType);
          if (pars[index].ValueLength.HasValue)
            stringBuilder.Append($"({pars[index].ValueLength.ToString()})");
        }
        else
          stringBuilder.Append("nvarchar(MAX)");
        stringBuilder.Append($" SET @P{index}=");
        object obj = pars[index].Value ?? (object) "NULL";
        if ((pars[index].Value == null || pars[index].ValueType == PXDbType.BigInt || pars[index].ValueType == PXDbType.Bit || pars[index].ValueType == PXDbType.Float || pars[index].ValueType == PXDbType.Int || pars[index].ValueType == PXDbType.Real || pars[index].ValueType == PXDbType.SmallInt ? 0 : (pars[index].ValueType != PXDbType.TinyInt ? 1 : 0)) != 0)
        {
          string str = obj.ToString().Replace("\0", "' + char(0) + '");
          stringBuilder.AppendFormat("'{0}'", (object) str);
        }
        else
          stringBuilder.Append(obj);
        stringBuilder.Append(" ");
      }
    }
    stringBuilder.Append(text);
    return stringBuilder.ToString();
  }

  protected override bool canPassOutParamsToUpdate() => true;

  public override TableDataSizeInfo GetTableDataSize(string tableName)
  {
    StringBuilder stringBuilder = new StringBuilder("\r\n\t\t\t\tSELECT\r\n\t\t\t\t\tSUM(case when i.index_id in (0, 1) then p.rows else 0 end) as rowsCount,\r\n\t\t\t\t\tSUM(au.used_pages) * 8 * 1024 as dataSizeBytes\r\n\t\t\t\tFROM sys.tables t\r\n\t\t\t\tINNER JOIN sys.indexes i ON t.OBJECT_ID = i.object_id\r\n\t\t\t\tINNER JOIN sys.partitions p ON i.object_id = p.OBJECT_ID AND i.index_id = p.index_id\r\n\t\t\t\tINNER JOIN (select container_id, sum(used_pages) as used_pages from sys.allocation_units group by container_id) au on au.container_id = p.partition_id\r\n\t\t\t\tWHERE t.is_ms_shipped = 0 ");
    stringBuilder.AppendLine($" AND t.NAME = '{tableName}'");
    using (PXDataRecord pxDataRecord = this.readSingleRecord(this.emptyPars, stringBuilder.ToString()))
    {
      if (pxDataRecord != null)
        return new TableDataSizeInfo((ulong) pxDataRecord.GetInt64(0).GetValueOrDefault(), (ulong) pxDataRecord.GetInt64(1).GetValueOrDefault());
    }
    return new TableDataSizeInfo();
  }
}
