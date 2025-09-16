// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDatabase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Configuration;
using PX.Common;
using PX.Data.Database.Common;
using PX.Data.SQLTree;
using PX.Data.Update;
using PX.DbServices.Model;
using PX.DbServices.Model.Entities;
using PX.DbServices.QueryObjectModel;
using PX.Hosting;
using PX.Licensing;
using PX.SM;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Hosting;

#nullable disable
namespace PX.Data;

public static class PXDatabase
{
  private static object s_lock;
  private static readonly Dictionary<string, PXDatabase.ProviderBucket> _providers = new Dictionary<string, PXDatabase.ProviderBucket>();
  private static ConfigurationSectionWithProviders _configurationSection;
  private static System.Func<System.Type, ILogger> _loggerFactory;
  private static IConfiguration _configuration;
  private const string DelayedFieldScopeKey = "PXDatabase.DelayedFieldScope";
  private const string ReadArchivedKey = "PXDatabase.ReadArchived";
  private const string ReadOnlyArchivedKey = "PXDatabase.ReadOnlyArchived";
  private static string[] skippedTables = new string[4]
  {
    "[dbo].[note]",
    "dbo.note",
    "[dbo].[notedoc]",
    "dbo.notedoc"
  };

  [Obsolete]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static IFormatter CreateDeserializationFormatter()
  {
    return (IFormatter) new BinaryFormatter()
    {
      Binder = (SerializationBinder) new PXAppCodeTypeBinder()
    };
  }

  [Obsolete]
  internal static object[] Deserialize(byte[] input, IFormatter formatter)
  {
    if (input == null || input.Length == 0)
      return (object[]) null;
    using (Stream serializationStream = (Stream) new MemoryStream())
    {
      serializationStream.Write(input, 0, input.Length);
      serializationStream.Position = 0L;
      return formatter.Deserialize(serializationStream) as object[];
    }
  }

  public static object[] Deserialize(byte[] input)
  {
    return PXDatabase.Deserialize(input, PXDatabase.CreateDeserializationFormatter());
  }

  public static byte[] Serialize(object[] input)
  {
    if (input == null || input.Length == 0)
      return (byte[]) null;
    using (Stream serializationStream = (Stream) new MemoryStream())
    {
      new BinaryFormatter().Serialize(serializationStream, (object) input);
      byte[] buffer = new byte[serializationStream.Length];
      serializationStream.Position = 0L;
      serializationStream.Read(buffer, 0, (int) serializationStream.Length);
      return buffer;
    }
  }

  internal static void MoveProvidersToThread()
  {
    lock (((ICollection) PXDatabase._providers).SyncRoot)
    {
      PXContext.SetSlot<PXDatabase.ProvidersSlot>((PXDatabase.ProvidersSlot) null);
      PXContext.SetSlot<PXDatabase.ProvidersSlot>(new PXDatabase.ProvidersSlot(PXDatabase._providers));
    }
  }

  internal static void RemoveProvidersFromThread()
  {
    lock (((ICollection) PXDatabase._providers).SyncRoot)
      PXContext.SetSlot<PXDatabase.ProvidersSlot>((PXDatabase.ProvidersSlot) null);
  }

  private static Dictionary<string, PXDatabase.ProviderBucket> _Providers
  {
    get
    {
      PXDatabase.ProvidersSlot slot = PXContext.GetSlot<PXDatabase.ProvidersSlot>();
      return slot != null ? slot._providers : PXDatabase._providers;
    }
  }

  internal static Dictionary<string, PXDatabase.ProviderBucket> Providers
  {
    get
    {
      Dictionary<string, PXDatabase.ProviderBucket> providers = PXDatabase._Providers;
      lock (((ICollection) providers).SyncRoot)
        return new Dictionary<string, PXDatabase.ProviderBucket>((IDictionary<string, PXDatabase.ProviderBucket>) providers);
    }
  }

  public static bool Initialized
  {
    get
    {
      Dictionary<string, PXDatabase.ProviderBucket> providers = PXDatabase._Providers;
      lock (((ICollection) providers).SyncRoot)
      {
        if (providers.Count <= 0 || !PXAccess.Initialized)
          return false;
        string key = PXAccess.GetConnectionString() ?? "default";
        PXDatabase.ProviderBucket providerBucket = (PXDatabase.ProviderBucket) null;
        providers.TryGetValue(key, out providerBucket);
        return providerBucket != null && !providerBucket.Initialised;
      }
    }
  }

  public static PXDatabaseProvider Provider
  {
    get
    {
      Dictionary<string, PXDatabase.ProviderBucket> providers = PXDatabase._Providers;
      string connString = (string) null;
      Guid? nullable = new Guid?();
      string slot = PXContext.GetSlot<string>("ProviderLicenseCheck");
      string key;
      if (!string.IsNullOrEmpty(slot))
      {
        key = slot;
      }
      else
      {
        connString = PXAccess.GetConnectionString();
        key = string.IsNullOrWhiteSpace(connString) ? "default" : connString;
        nullable = ProviderKeySuffixSlot.Get();
        if (nullable.HasValue)
          key += nullable.Value.ToString();
      }
      PXDatabase.ProviderBucket providerBucket1 = (PXDatabase.ProviderBucket) null;
      lock (((ICollection) providers).SyncRoot)
      {
        providers.TryGetValue(key, out providerBucket1);
        if (providerBucket1 != null && providerBucket1.Initialised)
          return providerBucket1.Provider;
        if (providerBucket1 != null)
        {
          if (providerBucket1.InitializationException != null)
            throw providerBucket1.InitializationException;
        }
      }
      lock (PXDatabase.s_lock)
      {
        lock (((ICollection) providers).SyncRoot)
        {
          providers.TryGetValue(key, out providerBucket1);
          if (providerBucket1 != null && providerBucket1.Initialised)
            return providerBucket1.Provider;
          if (providerBucket1 != null)
          {
            if (providerBucket1.InitializationException != null)
              throw providerBucket1.InitializationException;
          }
        }
        PXDatabase.ProviderBucket providerBucket2;
        try
        {
          PXDatabaseProvider provider;
          if (!string.IsNullOrEmpty(connString))
          {
            provider = PXDatabase.InitializeProvider(connString, PXAccess.GetCompanyID());
          }
          else
          {
            try
            {
              provider = PXDatabase.InitializeProvider(connString, PXAccess.GetCompanyID());
            }
            catch (Exception ex)
            {
              if (ex is PXProviderEmptyConnectionStringException || ex.InnerException is PXProviderEmptyConnectionStringException || ex is PXProviderException && !HostingEnvironment.IsHosted)
                provider = (PXDatabaseProvider) new PXDatabaseDummyProvider();
              else
                throw;
            }
          }
          if ((nullable.HasValue || provider is PXDatabaseDummyProvider) && WebConfig.IsClusterEnabled)
            provider.AgeGlobal = 0L;
          providerBucket2 = new PXDatabase.ProviderBucket(provider);
        }
        catch (Exception ex)
        {
          providerBucket2 = new PXDatabase.ProviderBucket(ex);
        }
        lock (((ICollection) providers).SyncRoot)
          providers[key] = providerBucket2;
        return providerBucket2.InitializationException == null ? providerBucket2.Provider : throw providerBucket2.InitializationException;
      }
    }
  }

  private static ConfigurationSectionWithProviders GetConfigurationSection(
    IConfiguration configuration)
  {
    return new ConfigurationSectionWithProviders(configuration.GetSection("pxdatabase"));
  }

  internal static Func<PXDatabaseProvider> Initialize(
    IConfiguration configuration,
    System.Func<System.Type, ILogger> loggerFactory)
  {
    PXDatabase._configurationSection = configuration != null ? PXDatabase.GetConfigurationSection(configuration) : throw new PXProviderException("Configuration not specified");
    if (!ConfigurationExtensions.Exists((IConfigurationSection) PXDatabase._configurationSection))
      throw new PXProviderException("Configuration not specified");
    PXDatabase._configuration = configuration;
    PXDatabase._loggerFactory = loggerFactory;
    return (Func<PXDatabaseProvider>) (() => PXDatabase.Provider);
  }

  public static PXDatabaseProvider InitializeProvider(string connString, string companyID)
  {
    ConfigurationSectionWithProviders configurationSection = PXDatabase._configurationSection;
    System.Func<System.Type, ILogger> loggerFactory = PXDatabase._loggerFactory;
    if (configurationSection == null || !ConfigurationExtensions.Exists((IConfigurationSection) configurationSection))
      throw new PXProviderException("The provider cannot be instantiated.");
    if (loggerFactory == null)
      throw new InvalidOperationException("Logger factory not set up for PXDatabase");
    ProviderSettings providerSettings = ConfigurationSectionExtensions.AsProviderSettings((IConfigurationSection) (configurationSection.GetDefaultProvider() ?? throw new PXProviderException("The default database provider is not specified.")));
    if (string.IsNullOrEmpty(providerSettings.Type))
      throw new PXProviderException("The provider cannot be instantiated.");
    if (!string.IsNullOrEmpty(companyID))
      providerSettings.Parameters[nameof (companyID)] = companyID;
    if (!string.IsNullOrEmpty(connString))
      providerSettings.Parameters["connectionString"] = connString;
    System.Type type = System.Type.GetType(providerSettings.Type, false);
    PXDatabaseProvider databaseProvider = !(type == (System.Type) null) && typeof (PXDatabaseProvider).IsAssignableFrom(type) ? (PXDatabaseProvider) Activator.CreateInstance(type) : throw new PXProviderException("The provider must implement the PXDatabaseProvider type.");
    databaseProvider.Initialize(providerSettings.Parameters);
    databaseProvider.InitializeLogger(loggerFactory);
    return databaseProvider;
  }

  public static void InitializeRequest(int cpid) => PXDatabase.Provider.InitializeRequest(cpid);

  static PXDatabase() => PXDatabase.s_lock = new object();

  internal static void SetRestrictedTables(string[] list)
  {
    PXDatabase.Provider.SetRestrictedTables(list);
  }

  internal static string GetConnectionString(NameValueCollection config)
  {
    string connectionString = PXDatabase._configuration.GetConnectionString(config);
    return !string.IsNullOrWhiteSpace(connectionString) ? connectionString : throw new PXProviderEmptyConnectionStringException();
  }

  public static int GetReportQueryTimeout() => PXDatabase.Provider.GetReportQueryTimeout();

  [Obsolete("This method is obsolete and will be removed. Rewrite your code without this method or contact your partner for assistance.")]
  public static IEnumerable<PXDataRecord> Select(
    PXGraph graph,
    BqlCommand command,
    long topCount,
    params PXDataValue[] pars)
  {
    return PXDatabase.Select(graph, command, 0L, topCount, (PXView) null, pars);
  }

  [Obsolete("This method is obsolete and will be removed. Rewrite your code without this method or contact your partner for assistance.")]
  internal static IEnumerable<PXDataRecord> Select(
    PXGraph graph,
    BqlCommand command,
    long topCount,
    PXView view,
    params PXDataValue[] pars)
  {
    return PXDatabase.Select(graph, command, 0L, topCount, view, pars);
  }

  [Obsolete("This method is obsolete and will be removed. Rewrite your code without this method or contact your partner for assistance.")]
  internal static IEnumerable<PXDataRecord> Select(
    PXGraph graph,
    BqlCommand command,
    long skip,
    long topCount,
    PXView view,
    params PXDataValue[] pars)
  {
    return PXDatabase.Provider.Select(graph, command, skip, topCount, view, pars);
  }

  [Obsolete("This method is obsolete and will be removed. Rewrite your code without this method or contact your partner for assistance.")]
  internal static IEnumerable<PXDataRecord> Select(
    PXGraph graph,
    BqlCommand command,
    long skip,
    long topCount,
    params PXDataValue[] pars)
  {
    return PXDatabase.Select(graph, command, skip, topCount, (PXView) null, pars);
  }

  public static PXDataRecord SelectSingle(System.Type table, params PXDataField[] pars)
  {
    return PXDatabase.Provider.SelectSingle(table, (IEnumerable<YaqlJoin>) null, pars);
  }

  public static PXDataRecord SelectSingle<Table>(params PXDataField[] pars) where Table : IBqlTable
  {
    return PXDatabase.Provider.SelectSingle(typeof (Table), (IEnumerable<YaqlJoin>) null, pars);
  }

  public static PXDataRecord SelectSingle<Table>(YaqlJoin join1, params PXDataField[] pars) where Table : IBqlTable
  {
    return PXDatabase.Provider.SelectSingle(typeof (Table), (IEnumerable<YaqlJoin>) new YaqlJoin[1]
    {
      join1
    }, pars);
  }

  public static PXDataRecord SelectSingle<Table>(
    YaqlJoin join1,
    YaqlJoin join2,
    params PXDataField[] pars)
    where Table : IBqlTable
  {
    return PXDatabase.Provider.SelectSingle(typeof (Table), (IEnumerable<YaqlJoin>) new YaqlJoin[2]
    {
      join1,
      join2
    }, pars);
  }

  public static PXDataRecord SelectSingle<Table>(
    IEnumerable<YaqlJoin> joins,
    params PXDataField[] pars)
    where Table : IBqlTable
  {
    return PXDatabase.Provider.SelectSingle(typeof (Table), joins, pars);
  }

  public static IEnumerable<PXDataRecord> SelectMulti(System.Type table, params PXDataField[] pars)
  {
    return PXDatabase.Provider.SelectMulti(table, (IEnumerable<YaqlJoin>) null, pars);
  }

  public static IEnumerable<PXDataRecord> SelectMulti<Table>(params PXDataField[] pars) where Table : IBqlTable
  {
    return PXDatabase.Provider.SelectMulti(typeof (Table), (IEnumerable<YaqlJoin>) null, pars);
  }

  public static IEnumerable<PXDataRecord> SelectMulti<Table>(
    YaqlJoin join1,
    params PXDataField[] pars)
    where Table : IBqlTable
  {
    return PXDatabase.Provider.SelectMulti(typeof (Table), (IEnumerable<YaqlJoin>) new YaqlJoin[1]
    {
      join1
    }, pars);
  }

  public static IEnumerable<PXDataRecord> SelectMulti<Table>(
    YaqlJoin join1,
    YaqlJoin join2,
    params PXDataField[] pars)
    where Table : IBqlTable
  {
    return PXDatabase.Provider.SelectMulti(typeof (Table), (IEnumerable<YaqlJoin>) new YaqlJoin[2]
    {
      join1,
      join2
    }, pars);
  }

  public static IEnumerable<PXDataRecord> SelectMulti<Table>(
    IEnumerable<YaqlJoin> joins,
    params PXDataField[] pars)
    where Table : IBqlTable
  {
    return PXDatabase.Provider.SelectMulti(typeof (Table), joins, pars);
  }

  /// <summary>
  /// Returns <code>IQueryable&lt;T&gt; for the specified DAC. Event handlers such as CommandPreparing are omitted.</code>
  /// </summary>
  /// <typeparam name="T">DAC type</typeparam>
  /// <returns>LINQ query</returns>
  public static IQueryable<T> Select<T>() where T : IBqlTable => SQLQueryable.Create<T>();

  internal static IEnumerable<PXDataRecord> SelectMulti(this Query query, params PXDataValue[] pars)
  {
    return PXDatabase.Provider.SelectMulti(query, pars);
  }

  public static IEnumerable<PXDataFieldMapping> GetDataFieldsForTable<T>() where T : class, IBqlTable, new()
  {
    return PXDatabase.Provider.GetDataFieldsForTable<T>();
  }

  public static IEnumerable<PXDataFieldAssign[]> SelectDataFields<T>(
    params PXDataFieldValue[] restricts)
    where T : class, IBqlTable, new()
  {
    PXDataField[] fields = (PXDataField[]) PXDatabase.GetDataFieldsForTable<T>().ToArray<PXDataFieldMapping>();
    foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<T>(((IEnumerable<PXDataField>) fields).Union<PXDataField>((IEnumerable<PXDataField>) restricts).ToArray<PXDataField>()))
    {
      if (pxDataRecord != null)
      {
        List<PXDataFieldAssign> pxDataFieldAssignList = new List<PXDataFieldAssign>();
        for (int i = 0; i < fields.Length; ++i)
        {
          object obj = pxDataRecord.GetValue(i);
          pxDataFieldAssignList.Add(new PXDataFieldAssign((Column) fields[i].Expression, obj));
        }
        yield return pxDataFieldAssignList.ToArray();
      }
    }
  }

  /// <summary>
  /// Perform PXDatabase.SelectMulti for a table, selecting all the fields WITHOUT calling any event handlers.
  /// </summary>
  public static IEnumerable<T> SelectRecords<T>(params PXDataField[] restricts) where T : class, IBqlTable, new()
  {
    return PXDatabase.Provider.SelectRecords<T>(restricts);
  }

  public static bool Insert(System.Type table, params PXDataFieldAssign[] pars)
  {
    PXTransactionScope.RegisterActivity();
    return PXDatabase.Provider.Insert(table, pars);
  }

  public static bool Insert<Table>(params PXDataFieldAssign[] pars) where Table : IBqlTable
  {
    PXTransactionScope.RegisterActivity();
    return PXDatabase.Provider.Insert(typeof (Table), pars);
  }

  public static bool Ensure(System.Type table, PXDataFieldAssign[] values, PXDataField[] pars)
  {
    PXTransactionScope.RegisterActivity();
    return PXDatabase.Provider.Ensure(table, values, pars);
  }

  public static bool Ensure<Table>(PXDataFieldAssign[] values, PXDataField[] pars) where Table : IBqlTable
  {
    PXTransactionScope.RegisterActivity();
    return PXDatabase.Provider.Ensure(typeof (Table), values, pars);
  }

  public static bool Archive(System.Type table, params PXDataFieldRestrict[] pars)
  {
    PXTransactionScope.RegisterActivity();
    return PXDatabase.Provider.Archive(table, pars);
  }

  public static bool Archive<Table>(params PXDataFieldRestrict[] pars)
  {
    PXTransactionScope.RegisterActivity();
    return PXDatabase.Provider.Archive(typeof (Table), pars);
  }

  public static bool Extract(System.Type table, params PXDataFieldRestrict[] pars)
  {
    PXTransactionScope.RegisterActivity();
    return PXDatabase.Provider.Extract(table, pars);
  }

  public static bool Extract<Table>(params PXDataFieldRestrict[] pars)
  {
    PXTransactionScope.RegisterActivity();
    return PXDatabase.Provider.Extract(typeof (Table), pars);
  }

  public static bool Update(System.Type table, params PXDataFieldParam[] pars)
  {
    PXTransactionScope.RegisterActivity();
    return PXDatabase.Provider.Update(table, pars);
  }

  public static bool Update<Table>(params PXDataFieldParam[] pars) where Table : IBqlTable
  {
    PXTransactionScope.RegisterActivity();
    return PXDatabase.Provider.Update(typeof (Table), EnumerableExtensions.Append<PXDataFieldParam>(pars, (PXDataFieldParam) PXSelectOriginalsRestrict.SelectAllOriginalValues));
  }

  public static int Update(PXGraph graph, IBqlUpdate command, params PXDataValue[] pars)
  {
    PXTransactionScope.RegisterActivity();
    return PXDatabase.Provider.Update(graph, command, pars);
  }

  public static bool Delete(System.Type table, params PXDataFieldRestrict[] pars)
  {
    PXTransactionScope.RegisterActivity();
    return PXDatabase.Provider.Delete(table, pars);
  }

  public static bool Delete<Table>(params PXDataFieldRestrict[] pars) where Table : IBqlTable
  {
    PXTransactionScope.RegisterActivity();
    return PXDatabase.Provider.Delete(typeof (Table), EnumerableExtensions.Append<PXDataFieldRestrict>(pars, PXSelectOriginalsRestrict.SelectAllOriginalValues));
  }

  public static bool ForceDelete(System.Type table, params PXDataFieldRestrict[] pars)
  {
    return PXDatabase.Provider.ForceDelete(table, pars);
  }

  public static bool ForceDelete<Table>(params PXDataFieldRestrict[] pars) where Table : IBqlTable
  {
    return PXDatabase.Provider.ForceDelete(typeof (Table), pars);
  }

  public static byte[] SelectTimeStamp() => PXDatabase.Provider.SelectTimeStamp();

  [PXInternalUseOnly]
  public static byte[] SelectCrossCompanyTimeStamp()
  {
    return PXDatabase.Provider.SelectCrossCompanyTimeStamp();
  }

  public static IEnumerable<DataVersion> SelectVersions() => PXDatabase.Provider.SelectVersions();

  public static string SelectCollation() => PXDatabase.Provider.SelectCollation();

  public static void SelectDate(out System.DateTime dtLocal, out System.DateTime dtUtc)
  {
    PXDatabase.Provider.SelectDate(out dtLocal, out dtUtc);
  }

  public static CompanyInfo[] SelectCompanies() => PXDatabase.Provider.SelectCompanies(false);

  public static void ClearCompanyCache() => PXDatabase.Provider.ClearCompaniesCache();

  public static void SetDesignTimeCompany() => PXDatabase.Provider.SetDesignTimeCompany();

  public static void ResetCredentials() => PXDatabase.Provider.ResetCredentials();

  public static object[] Execute(string procedureName, params PXSPParameter[] pars)
  {
    return PXDatabase.Provider.Execute(procedureName, pars);
  }

  internal static companySetting.companyFlag SelectTableSetting(string tableName)
  {
    companySetting settings;
    PXDatabase.Provider.getCompanyID(tableName, out settings);
    return settings == null ? companySetting.companyFlag.Separate : settings.Flag;
  }

  public static Decimal? SelectIdentity() => PXDatabase.Provider.SelectIdentity();

  public static Decimal? SelectIdentity<Table>(string identityName) where Table : IBqlTable
  {
    return PXDatabase.SelectIdentity(typeof (Table), identityName);
  }

  public static Decimal? SelectIdentity(System.Type table, string identityName)
  {
    if (!(PXTransactionScope.GetInsertedTable() == table))
      return new Decimal?(0M);
    Decimal? identity = PXTransactionScope.GetInsertedIdentity();
    if (!identity.HasValue)
      identity = PXDatabase.SelectIdentity();
    if (identity.HasValue)
      PXTransactionScope.SetIdentityAudit(table.Name, identityName, identity);
    return identity;
  }

  internal static DbTransaction CreateTransaction() => PXDatabase.Provider.CreateTransaction();

  internal static void Commit(IDbTransaction tran) => PXDatabase.Provider.Commit(tran);

  internal static void Rollback(IDbTransaction tran) => PXDatabase.Provider.Rollback(tran);

  internal static IDbTransaction GetTransaction()
  {
    return (IDbTransaction) PXDatabase.Provider.GetTransaction();
  }

  internal static DbConnection CreateConnection() => PXDatabase.Provider.CreateConnection();

  internal static IDbConnection GetConnection()
  {
    return (IDbConnection) PXDatabase.Provider.GetConnection();
  }

  internal static IDbCommand GetCommand() => (IDbCommand) PXDatabase.Provider.GetCommand();

  internal static DbCommand CreateCommand() => PXDatabase.Provider.CreateCommand();

  internal static void LeaveConnection(IDbConnection connection)
  {
    PXDatabase.Provider.LeaveConnection(connection);
  }

  internal static void LeaveCommand(IDbCommand command)
  {
    PXDatabase.Provider.LeaveCommand(command);
  }

  public static string CreateParameterName(int ordinal)
  {
    return PXDatabase.Provider.CreateParameterName(ordinal);
  }

  internal static void SaveAudit(List<PXDatabase.AuditTable> audit)
  {
    PXDatabase.Provider.SaveAudit(audit);
  }

  internal static void SaveWatchDog(IEnumerable<System.Type> watchdog)
  {
    PXDatabase.Provider.SaveWatchDog(watchdog);
  }

  public static bool AuditRequired(string screenID) => PXDatabase.Provider.AuditRequired(screenID);

  internal static bool AuditRequired(System.Type table) => PXDatabase.Provider.AuditRequired(table);

  /// <summary>
  /// Database table names
  /// <remarks>
  /// The implementation uses a case-insensitive <c>HashSet&lt;string&gt;</c>, so using <c>.Contains(...)</c> is fast and safe
  /// </remarks>
  /// </summary>
  public static IEnumerable<string> Tables => PXDatabase.Provider.GetTables();

  public static string[] Companies => PXDatabase.Provider.Companies;

  [Obsolete("This property is extremely misleading. Use PXDatabase.Companies if you want to maintain old behavior, or PXDatabase.Provider.DbCompanies if you want correct behavior.")]
  public static string[] DbCompanies => PXDatabase.Provider.Companies;

  public static string[] AvailableCompanies
  {
    get
    {
      int num = 0;
      List<string> stringList = new List<string>();
      PXLicense license = LicensingManager.Instance.License;
      foreach (string company in PXDatabase.Provider.Companies)
      {
        if (license.Trials == null || !license.Trials.Contains(company))
        {
          if (num < license.CompaniesAllowed)
            stringList.Add(company);
          ++num;
        }
        else
          stringList.Add(company);
      }
      return stringList.ToArray();
    }
  }

  public static bool SecureCompanyID => PXDatabase.Provider.SecureCompanyID;

  public static bool RequiresLogOut
  {
    get => PXDatabase.Provider != null && PXDatabase.Provider.RequiresLogOut;
  }

  internal static bool DelayedFieldScope
  {
    get => PXContext.GetSlot<bool>("PXDatabase.DelayedFieldScope");
    set => PXContext.SetSlot<bool>("PXDatabase.DelayedFieldScope", value);
  }

  internal static bool ReadThroughArchived
  {
    get => PXContext.GetSlot<bool>("PXDatabase.ReadArchived");
    set => PXContext.SetSlot<bool>("PXDatabase.ReadArchived", value);
  }

  internal static bool ReadOnlyArchived
  {
    get => PXContext.GetSlot<bool>("PXDatabase.ReadOnlyArchived");
    set => PXContext.SetSlot<bool>("PXDatabase.ReadOnlyArchived", value);
  }

  internal static bool ReadDeleted
  {
    get => PXContext.GetSlot<bool>("PXDatabase.ReadDeleted");
    set => PXContext.SetSlot<bool>("PXDatabase.ReadDeleted", value);
  }

  internal static bool ReadInsertedDeleted
  {
    get => PXContext.GetSlot<bool>("PXDatabase.ReadInsertedDeleted");
    set => PXContext.SetSlot<bool>("PXDatabase.ReadInsertedDeleted", value);
  }

  internal static bool ReadOnlyDeleted
  {
    get => PXContext.GetSlot<bool>("PXDatabase.ReadOnlyDeleted");
    set => PXContext.SetSlot<bool>("PXDatabase.ReadOnlyDeleted", value);
  }

  internal static bool IgnoreChange
  {
    get => PXContext.GetSlot<bool>("PXDatabase.IgnoreChange");
    set => PXContext.SetSlot<bool>("PXDatabase.IgnoreChange", value);
  }

  internal static bool ReadBranchRestricted
  {
    get => PXContext.GetSlot<bool>("PXDatabase.ReadBranchRestricted");
    set => PXContext.SetSlot<bool>("PXDatabase.ReadBranchRestricted", value);
  }

  internal static bool PrefetchInSeparateConnection
  {
    get => WebConfig.PrefetchInSeparateConnection && ProviderKeySuffixSlot.NotSet();
  }

  internal static string SpecificBranchTable
  {
    get => PXContext.GetSlot<string>("PXDatabase.SpecificBranchTable");
    set => PXContext.SetSlot<string>("PXDatabase.SpecificBranchTable", value);
  }

  internal static List<int> BranchIDs
  {
    get => PXContext.GetSlot<List<int>>("PXDatabase.BranchIDs");
    set => PXContext.SetSlot<List<int>>("PXDatabase.BranchIDs", value);
  }

  /// <summary>
  /// If the slots contain a valid <typeparamref name="ObjectType" /> object that is saved
  /// under the key <paramref name="key" />, the method returns this object.
  /// Otherwise, the method creates a new <typeparamref name="ObjectType" /> object, invokes
  /// <tt>Prefetch(<paramref name="parameter" />)</tt> for this <typeparamref name="ObjectType" /> object,
  /// saves this <typeparamref name="ObjectType" /> object in the slots under the key <paramref name="key" />,
  /// and returns it.
  /// </summary>
  /// <typeparam name="ObjectType">The type of an object that is saved in a slot.</typeparam>
  /// <typeparam name="Parameter">The type of the parameter with which the <tt>Prefetch</tt>
  /// method is invoked.</typeparam>
  /// <param name="key">The key under which an <typeparamref name="ObjectType" /> object is saved.</param>
  /// <param name="parameter">The parameter with which the <tt>Prefetch</tt> method is invoked.</param>
  /// <param name="tables">The list of tables: if any of these tables change in the database,
  /// the cache is invalidated for <paramref name="key" /> and <typeparamref name="ObjectType" />.</param>
  /// <returns>The object cached in the slots in case of success.
  /// If an unhandled exception is thrown during the prefetch operation, returns <see langword="null" />.</returns>
  public static ObjectType GetSlot<ObjectType, Parameter>(
    string key,
    Parameter parameter,
    params System.Type[] tables)
    where ObjectType : class, IPrefetchable<Parameter>, new()
  {
    using (new PXPerformanceInfoTimerScope((System.Func<PXPerformanceInfo, Stopwatch>) (info => info.TmGetSlot)))
    {
      using (new PXResourceGovernorSafeScope())
        return PXDatabase.Provider.GetSlot<ObjectType>(key, (PrefetchDelegate<ObjectType>) (() =>
        {
          using (new PXPerformanceInfoTimerScope((System.Func<PXPerformanceInfo, Stopwatch>) (info => info.TmPrefetch)))
          {
            ObjectType slot = new ObjectType();
            PXConnectionScope pxConnectionScope = (PXConnectionScope) null;
            try
            {
              if (PXDatabase.PrefetchInSeparateConnection)
                pxConnectionScope = new PXConnectionScope();
              using (new PXCommandScope())
                slot.Prefetch(parameter);
            }
            finally
            {
              pxConnectionScope?.Dispose();
            }
            if ((object) slot is IInternable)
            {
              try
              {
                slot = (ObjectType) ((IInternable) (object) slot).Intern();
              }
              catch
              {
              }
            }
            return slot;
          }
        }), tables);
    }
  }

  public static ObjectType GetSlotWithContextCache<ObjectType>(string key, params System.Type[] tables) where ObjectType : class, new()
  {
    ObjectType slot = PXContext.GetSlot<ObjectType>(key);
    if ((object) slot == null)
    {
      using (new PXResourceGovernorSafeScope())
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        slot = PXDatabase.Provider.GetSlot<ObjectType>(key, PXDatabase.\u003CGetSlotWithContextCache\u003EO__142_0<ObjectType>.\u003C0\u003E__prefetchMethod ?? (PXDatabase.\u003CGetSlotWithContextCache\u003EO__142_0<ObjectType>.\u003C0\u003E__prefetchMethod = new PrefetchDelegate<ObjectType>(PXDatabase.prefetchMethod<ObjectType>)), tables);
      }
      if ((object) slot != null)
        PXContext.SetSlot<ObjectType>(key, slot);
    }
    return slot;
  }

  /// <summary>
  /// If the slots contain a valid <typeparamref name="ObjectType" /> object
  /// that is saved under the key <paramref name="key" />, the method returns this object.
  /// Otherwise, the method creates a new <typeparamref name="ObjectType" /> object, saves it in the slot
  /// under the key <paramref name="key" />, and returns it. If <typeparamref name="ObjectType" />
  /// inherits from <tt>IPrefetchable&lt;&gt;</tt>, this method invokes the <tt>Prefetch</tt> method
  /// for the <typeparamref name="ObjectType" /> object without a parameter
  /// before the <typeparamref name="ObjectType" /> object is saved in the slots.
  /// </summary>
  /// <typeparam name="ObjectType">The type of an object that is saved in a slot.</typeparam>
  /// <param name="key">The key under which an <typeparamref name="ObjectType" /> object is saved.</param>
  /// <param name="tables">The list of the table types: if any of these tables change in the database,
  /// the cache is invalidated for <paramref name="key" /> and <typeparamref name="ObjectType" />.</param>
  /// <returns>The object cached in the slots in case of success.
  /// If an unhandled exception is thrown during the prefetch operation, returns <see langword="null" />.</returns>
  public static ObjectType GetSlot<ObjectType>(string key, params System.Type[] tables) where ObjectType : class, new()
  {
    using (new PXResourceGovernorSafeScope())
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return PXDatabase.Provider.GetSlot<ObjectType>(key, PXDatabase.\u003CGetSlot\u003EO__143_0<ObjectType>.\u003C0\u003E__prefetchMethod ?? (PXDatabase.\u003CGetSlot\u003EO__143_0<ObjectType>.\u003C0\u003E__prefetchMethod = new PrefetchDelegate<ObjectType>(PXDatabase.prefetchMethod<ObjectType>)), tables);
    }
  }

  private static T prefetchMethod<T>() where T : class, new()
  {
    T obj = new T();
    if ((object) obj is IPrefetchable)
    {
      PXConnectionScope pxConnectionScope = (PXConnectionScope) null;
      try
      {
        if (PXDatabase.PrefetchInSeparateConnection)
          pxConnectionScope = new PXConnectionScope();
        using (new PXCommandScope())
          ((IPrefetchable) (object) obj).Prefetch();
      }
      finally
      {
        pxConnectionScope?.Dispose();
      }
    }
    if ((object) obj is IInternable)
    {
      try
      {
        obj = (T) ((IInternable) (object) obj).Intern();
      }
      catch
      {
      }
    }
    return obj;
  }

  public static void ResetSlot<ObjectType>(string key, params System.Type[] tables) where ObjectType : class, new()
  {
    PXDatabase.Provider.ResetSlot<ObjectType>(key, tables);
  }

  public static void ResetSlotForAllCompanies(string key, params System.Type[] tables)
  {
    PXDatabase.Provider.ResetSlotForAllCompanies(key, tables);
  }

  public static void ResetSlotForAllCompanies(string keyPrefix)
  {
    PXDatabase.Provider.ResetSlotForAllCompanies(keyPrefix);
  }

  public static void ResetSlots() => PXDatabase.Provider.ResetSlots();

  private static string CurrentLanguageModifier
  {
    get
    {
      return !PXDBLocalizableStringAttribute.IsEnabled ? string.Empty : "$" + Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
    }
  }

  public static ObjectType GetLocalizableSlot<ObjectType, Parameter>(
    string key,
    Parameter parameter,
    params System.Type[] tables)
    where ObjectType : class, IPrefetchable<Parameter>, new()
  {
    return PXDatabase.GetSlot<ObjectType, Parameter>(key + PXDatabase.CurrentLanguageModifier, parameter, tables);
  }

  public static ObjectType GetLocalizableSlot<ObjectType>(string key, params System.Type[] tables) where ObjectType : class, new()
  {
    return PXDatabase.GetSlot<ObjectType>(key + PXDatabase.CurrentLanguageModifier, tables);
  }

  public static void ResetLocalizableSlot<ObjectType>(string key, params System.Type[] tables) where ObjectType : class, new()
  {
    PXDatabase.ResetSlot<ObjectType>(key + PXDatabase.CurrentLanguageModifier, tables);
  }

  internal static void CleanupExpiredSlots(int slotTimeoutMinutes)
  {
    try
    {
      foreach (KeyValuePair<string, PXDatabase.ProviderBucket> keyValuePair in PXDatabase.Providers.Where<KeyValuePair<string, PXDatabase.ProviderBucket>>((System.Func<KeyValuePair<string, PXDatabase.ProviderBucket>, bool>) (p => p.Value != null && p.Value.Initialised && p.Value.Provider != null)))
        keyValuePair.Value.Provider.CleanupExpiredSlots(slotTimeoutMinutes);
    }
    catch
    {
    }
  }

  public static bool IsReadDeletedSupported<Table>() where Table : IBqlTable
  {
    return PXDatabase.IsReadDeletedSupported(typeof (Table));
  }

  public static bool IsReadDeletedSupported<Table>(out string fieldName) where Table : IBqlTable
  {
    return PXDatabase.IsReadDeletedSupported(typeof (Table), out fieldName);
  }

  public static bool IsReadDeletedSupported(System.Type table)
  {
    return PXDatabase.Provider.IsReadDeletedSupported(table);
  }

  public static bool IsReadDeletedSupported(System.Type table, out string fieldName)
  {
    return PXDatabase.Provider.IsReadDeletedSupported(table, out fieldName);
  }

  public static void SetReadDeletedCapability<Table>(bool enabled) where Table : IBqlTable
  {
    PXDatabase.SetReadDeletedCapability(typeof (Table), enabled);
  }

  public static void SetReadDeletedCapability(System.Type table, bool enabled)
  {
    PXDatabase.Provider.SetReadDeletedCapability(table, enabled);
  }

  public static bool IsBranchRestrictionSupported(System.Type table)
  {
    companySetting settings;
    PXDatabase.Provider.getCompanyID(table.Name, out settings);
    return settings.Branch != null;
  }

  internal static bool CheckBranchRestrictionSupported(System.Type table, ISqlDialect dialect)
  {
    foreach (System.Type type in table.GetInheritanceChain().Where<System.Type>((System.Func<System.Type, bool>) (_ => typeof (IBqlTable).IsAssignableFrom(_))))
    {
      companySetting settings;
      try
      {
        PXDatabase.Provider.getCompanyID(type.Name, out settings);
      }
      catch
      {
        settings = (companySetting) null;
      }
      if (settings != null && settings.Branch != null)
      {
        PXDatabase.SpecificBranchTable = type.Name;
        return true;
      }
    }
    return false;
  }

  public static bool IsVirtualTable(System.Type table) => PXDatabase.Provider.IsVirtualTable(table);

  public static TableHeader GetTableStructure(string tableName)
  {
    return PXDatabase.Provider.GetTableStructure(tableName);
  }

  /// <summary>
  /// Allows to get notification when data in database table were changed
  /// </summary>
  public static bool Subscribe(System.Type table, PXDatabaseTableChanged handler, string uniqueKey = null)
  {
    if (table == (System.Type) null)
      throw new PXArgumentException(nameof (table), "The argument cannot be null.");
    if (handler == null)
      throw new PXArgumentException(nameof (handler), "The argument cannot be null.");
    return PXDatabase.Provider.Subscribe(table, handler, uniqueKey);
  }

  /// <summary>
  /// Allows to get notification when data in one of the specified database tables were changed.
  /// </summary>
  public static void Subscribe(
    PXDatabaseTableChanged handler,
    string uniqueKey = null,
    params System.Type[] tables)
  {
    PXDatabase.Subscribe(handler, (IEnumerable<System.Type>) tables, uniqueKey);
  }

  /// <summary>
  /// Allows to get notification when data in one of the specified database tables were changed.
  /// </summary>
  public static void Subscribe(
    PXDatabaseTableChanged handler,
    IEnumerable<System.Type> tables,
    string uniqueKey = null)
  {
    foreach (System.Type table in tables)
      PXDatabase.Subscribe(table, handler, uniqueKey);
  }

  /// <summary>
  /// Allows to get notification when data in database table were changed
  /// </summary>
  public static bool Subscribe<Table>(PXDatabaseTableChanged handler, string uniqueKey = null) where Table : IBqlTable
  {
    return PXDatabase.Subscribe(typeof (Table), handler, uniqueKey);
  }

  /// <summary>Removes subscribtion created by Subscribe method</summary>
  public static void UnSubscribe(System.Type table, PXDatabaseTableChanged handler, string uniqueKey = null)
  {
    if (table == (System.Type) null)
      throw new PXArgumentException(nameof (table), "The argument cannot be null.");
    if (handler == null)
      throw new PXArgumentException(nameof (handler), "The argument cannot be null.");
    PXDatabase.Provider.UnSubscribe(table, handler, uniqueKey);
  }

  /// <summary>Removes subscribtion created by Subscribe method</summary>
  public static void UnSubscribe<Table>(PXDatabaseTableChanged handler, string uniqueKey = null) where Table : IBqlTable
  {
    PXDatabase.UnSubscribe(typeof (Table), handler, uniqueKey);
  }

  public static void TransactionFinished() => PXDatabase.Provider.TransactionFinished();

  internal static PXDatabase.PXTableAge GetAge() => PXDatabase.Provider.GetAge();

  internal static bool IsTableModified(string tableName, PXDatabase.PXTableAge age)
  {
    return PXDatabase.Provider.IsTableModified(tableName, age);
  }

  internal static void TableChangedLocal(string tableName)
  {
    PXDatabase.Provider.TableChangedLocal(tableName);
  }

  public static string[] ExtractTablesFromSql(string sql)
  {
    return ((IEnumerable<string>) PXDatabase.GetSqlTablesArray(sql)).Select<string, string>((System.Func<string, string>) (s => s.ToLowerInvariant())).Distinct<string>().ToArray<string>();
  }

  public static string[] ExtractTablesFromSqlWithCase(string sql)
  {
    return ((IEnumerable<string>) PXDatabase.GetSqlTablesArray(sql)).Distinct<string>().ToArray<string>();
  }

  private static string[] GetSqlTablesArray(string sqlText)
  {
    string[] strArray = Regex.Split(sqlText, "[\\s)(;]+");
    bool flag = false;
    string[] source1 = new string[2]{ "", "select" };
    string[] source2 = new string[2]{ "from", "join" };
    List<string> stringList = new List<string>();
    foreach (string tableName in strArray)
    {
      if (flag && !((IEnumerable<string>) source1).Contains<string>(tableName.ToLower()) && !((IEnumerable<string>) PXDatabase.skippedTables).Contains<string>(tableName.ToLower()))
        stringList.Add(PXDatabase.TrimTableName(tableName));
      flag = ((IEnumerable<string>) source2).Contains<string>(tableName.ToLower());
    }
    return stringList.ToArray();
  }

  private static string TrimTableName(string tableName)
  {
    if (tableName.ToLower().StartsWith("[dbo]."))
      tableName = tableName.Substring(6);
    if (tableName.ToLower().StartsWith("dbo."))
      tableName = tableName.Substring(4);
    if (tableName.StartsWith(PXDatabase.Provider.SqlDialect.LiteralToOpenIdentifier.ToString()))
      tableName = tableName.Substring(1);
    if (tableName.EndsWith(PXDatabase.Provider.SqlDialect.LiteralToCloseIdentifier.ToString()))
      tableName = tableName.Substring(0, tableName.Length - 1);
    return tableName;
  }

  public static string AdminUsername
  {
    get => PXDatabase.Companies.Length == 0 ? "admin" : $"admin@{PXDatabase.Companies[0]}";
  }

  public static bool IsDeletedDatabaseRecord(
    System.Type table,
    PXDataFieldParam[] parameters,
    out object[] keys)
  {
    IEnumerable<PXDataFieldParam> pxDataFieldParams = parameters != null ? ((IEnumerable<PXDataFieldParam>) parameters).Where<PXDataFieldParam>((System.Func<PXDataFieldParam, bool>) (p => !(p is PXDummyDataFieldRestrict))) : throw new PXArgumentException(nameof (parameters), "The argument cannot be null.");
    Dictionary<string, object> dictionary = new Dictionary<string, object>();
    foreach (PXDataFieldParam pxDataFieldParam in pxDataFieldParams)
    {
      if (!dictionary.ContainsKey(pxDataFieldParam.Column.Name))
        dictionary.Add(pxDataFieldParam.Column.Name, pxDataFieldParam.Value);
    }
    return PXDatabase.IsDeletedDatabaseRecord(table, dictionary.Keys.ToArray<string>(), dictionary.Values.ToArray<object>(), out keys);
  }

  public static bool IsDeletedDatabaseRecord(
    System.Type table,
    string[] fields,
    object[] values,
    out object[] keys)
  {
    if (table == (System.Type) null)
      throw new PXArgumentException(nameof (table), "The argument cannot be null.");
    if (fields == null)
      throw new PXArgumentException(nameof (fields), "The argument cannot be null.");
    if (values == null)
      throw new PXArgumentException(nameof (values), "The argument cannot be null.");
    if (fields.Length != values.Length)
      throw new PXArgumentException();
    TableHeader tableHeader = PXDatabase.GetTableStructure(table.Name);
    if (tableHeader == null)
    {
      PXDatabase.Provider.SchemaCacheInvalidate();
      tableHeader = PXDatabase.GetTableStructure(table.Name);
    }
    HashSet<string> hashSet = tableHeader.Indices.Where<TableIndex>((System.Func<TableIndex, bool>) (ind => ind.IsPrimaryKey || ind.IsUnique)).SelectMany<TableIndex, TableIndexOnColumn>((System.Func<TableIndex, IEnumerable<TableIndexOnColumn>>) (ind => (IEnumerable<TableIndexOnColumn>) ind.Columns)).Select<TableIndexOnColumn, TableColumn>((System.Func<TableIndexOnColumn, TableColumn>) (c => tableHeader.getColumnByName(((TableEntityBase) c).Name))).Where<TableColumn>((System.Func<TableColumn, bool>) (c => !c.Identity && c.Role != 1 && c.Role != 5)).Select<TableColumn, string>((System.Func<TableColumn, string>) (c => ((TableEntityBase) c).Name)).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    List<object> objectList = new List<object>();
    List<PXDataField> pxDataFieldList = new List<PXDataField>();
    for (int index = 0; index < fields.Length; ++index)
    {
      if (hashSet.Contains(fields[index]))
      {
        if (pxDataFieldList.Count == 0)
        {
          PXDataField pxDataField = new PXDataField(fields[index]);
          pxDataFieldList.Add(pxDataField);
        }
        Column fieldName = new Column(fields[index]);
        fieldName.PadSpaced = PXDatabase.Provider.SqlDialect.IsPadSpaceColumn(table, fields[index]);
        fieldName.SetDBType(PXDbTypeConverter.SqlDbTypeToPXDbType(tableHeader.getColumnByName(fields[index]).Type));
        PXDataFieldValue pxDataFieldValue = new PXDataFieldValue((SQLExpression) fieldName, fieldName.GetDBType(), values[index]);
        pxDataFieldList.Add((PXDataField) pxDataFieldValue);
        objectList.Add(values[index]);
      }
    }
    keys = objectList.ToArray();
    if (!PXDatabase.IsReadDeletedSupported(table))
      return false;
    using (new PXReadDeletedScope(true))
    {
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle(table, pxDataFieldList.ToArray()))
        return pxDataRecord != null;
    }
  }

  internal sealed class ProviderBucket
  {
    public bool Initialised;
    public Exception InitializationException;
    public PXDatabaseProvider Provider;

    public ProviderBucket()
    {
      this.Initialised = false;
      this.InitializationException = (Exception) null;
      this.Provider = (PXDatabaseProvider) null;
    }

    public ProviderBucket(PXDatabaseProvider provider)
    {
      this.Initialised = true;
      this.InitializationException = (Exception) null;
      this.Provider = provider;
    }

    public ProviderBucket(Exception exception)
    {
      this.Initialised = false;
      this.InitializationException = exception;
    }
  }

  internal sealed class AuditField
  {
    public readonly string FieldName;
    public readonly string FieldValue;
    public readonly bool IsRestriction;
    public readonly StorageBehavior Storage;

    public AuditField(
      string fieldName,
      string fieldValue,
      bool isRestriction,
      StorageBehavior storage = StorageBehavior.Table)
    {
      this.FieldName = fieldName;
      this.FieldValue = fieldValue;
      this.IsRestriction = isRestriction;
      this.Storage = storage;
    }

    public override string ToString()
    {
      return string.Format(this.IsRestriction ? "WHERE {0} = {1}" : "{0} => {1}", (object) this.FieldName, (object) this.FieldValue);
    }
  }

  internal sealed class AuditTable
  {
    public readonly string Operation;
    public readonly string TableName;
    public Decimal? Identity;
    public string IdentityName;
    public List<PXDatabase.AuditField> Fields = new List<PXDatabase.AuditField>();

    public string IdentityAsString
    {
      get
      {
        return !this.Identity.HasValue ? (string) null : this.Identity.Value.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      }
    }

    public AuditTable(string operation, string tableName)
    {
      this.Operation = operation;
      this.TableName = tableName;
    }
  }

  [PXContextCopyingRequired]
  private sealed class ProvidersSlot
  {
    public readonly Dictionary<string, PXDatabase.ProviderBucket> _providers = new Dictionary<string, PXDatabase.ProviderBucket>();

    public ProvidersSlot(
      Dictionary<string, PXDatabase.ProviderBucket> providers)
    {
      this._providers = new Dictionary<string, PXDatabase.ProviderBucket>((IDictionary<string, PXDatabase.ProviderBucket>) providers);
      providers.Clear();
      PXDatabase.ProviderBucket providerBucket;
      if (!this._providers.TryGetValue("default", out providerBucket))
        return;
      providers["default"] = providerBucket;
    }
  }

  internal struct PXTableAge
  {
    public long Global;
    public long Local;

    internal void ResetLocalAge()
    {
      if (!WebConfig.IsClusterEnabled || WebConfig.IsMultiSiteMode)
        return;
      this.Local = 0L;
    }
  }

  internal class PXTableAgeComparer : IComparer<PXDatabase.PXTableAge>
  {
    public static readonly PXDatabase.PXTableAgeComparer Default = new PXDatabase.PXTableAgeComparer();

    public int Compare(PXDatabase.PXTableAge x, PXDatabase.PXTableAge y)
    {
      if (WebConfig.IsClusterEnabled)
      {
        if (x.Global > y.Global)
          return 1;
        if (x.Global < y.Global)
          return -1;
      }
      return Comparer<long>.Default.Compare(x.Local, y.Local);
    }
  }
}
