// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDatabaseProviderBase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.BulkInsert.Installer.DatabaseSetup;
using PX.Common;
using PX.Common.Context;
using PX.Common.Extensions;
using PX.Data.Api.Export;
using PX.Data.Database.Common;
using PX.Data.Process;
using PX.Data.PushNotifications;
using PX.Data.SQLTree;
using PX.Data.Update;
using PX.DbServices.Model;
using PX.DbServices.Model.Entities;
using PX.DbServices.Points;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.QueryObjectModel;
using PX.Export.Authentication;
using PX.Logging.Enrichers;
using PX.SM;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Data;

/// <exclude />
/// <exclude />
/// <exclude />
public abstract class PXDatabaseProviderBase : PXDatabaseProvider
{
  private static readonly ConcurrentDictionary<string, IDbSchemaCache> __schemaCaches = new ConcurrentDictionary<string, IDbSchemaCache>();
  protected IDbSchemaCache schemaCache;
  protected Dictionary<string, PXDatabaseTableChanged> _Subscribers = new Dictionary<string, PXDatabaseTableChanged>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  protected ReaderWriterLock _SubscribersLock = new ReaderWriterLock();
  protected ReaderWriterLock __SubscribersStampLock = new ReaderWriterLock();
  protected Dictionary<int, PXDatabaseProviderBase.SubscribersStamp> _SubscribersStamp = new Dictionary<int, PXDatabaseProviderBase.SubscribersStamp>();
  private System.DateTime _SubscribersDateTimeGlobal = System.DateTime.Now;
  private int _singleCompanyID;
  protected const string COMPANY_MASK = "CompanyMask";
  private Dictionary<int, int[]> selectableCompanies;
  private Dictionary<int, string> updatableCompanies;
  private Dictionary<int, int[]> companySiblings;
  private Dictionary<int, int> mainCompanies;
  private HashSet<int> templateCompanies = new HashSet<int>();
  private int companyCnt;
  private Dictionary<string, int> companyNames;
  private Dictionary<int, string> companyDisplayNames;
  private Dictionary<int, string> companyMappings;
  private Dictionary<int, int> companyPosition;
  public const int MASK_READ = 2;
  public const int MASK_READWRITE = 3;
  protected ReaderWriterLock _CompaniesSlotsLock = new ReaderWriterLock();
  private HashSet<string> _RestrictedTables;
  protected readonly PXDataField[] emptyPars = new PXDataField[0];
  private static readonly PXDataFieldParam[][] _emptySelectedOriginalRows = Enumerable.Empty<PXDataFieldParam[]>().ToArray<PXDataFieldParam[]>();
  protected const string ORDER_BY_SPECIAL_FIELD = "order by special field";

  private ICurrentUserInformationProvider CurrentUserInformationProvider
  {
    get => PX.Data.CurrentUserInformationProvider.Instance;
  }

  public override IDbSchemaCache SchemaCache => this.schemaCache;

  protected virtual IDbSchemaCache CreateSchemaCache(string key)
  {
    return (IDbSchemaCache) new DbSchemaCache((Func<Point>) (() => (Point) this.CreateDbServicesPoint()));
  }

  /// <inheritdoc />
  protected internal override void Initialize(NameValueCollection config)
  {
    base.Initialize(config);
    string key = this.ConnectionString;
    if (WebConfig.ShareDatabaseSchemaInfo)
      key = "default";
    this.schemaCache = PXDatabaseProviderBase.__schemaCaches.GetOrAdd(key, new System.Func<string, IDbSchemaCache>(this.CreateSchemaCache));
    if (!WebConfig.ShareDatabaseSchemaInfo)
      this.Subscribe(typeof (PXDatabaseProvider.SchemaCacheInvalidateFlag), (PXDatabaseTableChanged) (() => this.schemaCache.InvalidateAll()));
    using (new PXIdentityScope())
      this.InitSqlDialect();
    if (string.IsNullOrEmpty(this.companyID))
      this.companyID = "1";
    if (this.companyID == "1")
      this.DatabaseDefinedCompanies = true;
    this.fillCompanyNames(this.companyID);
  }

  public override void InitializeRequest(int cpid)
  {
    using (IEnumerator<KeyValuePair<string, int>> enumerator = this.companyNames.Where<KeyValuePair<string, int>>((System.Func<KeyValuePair<string, int>, bool>) (pair => pair.Value == cpid)).GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return;
      KeyValuePair<string, int> current = enumerator.Current;
      if (!((IEnumerable<string>) this.CurrentUserInformationProvider.GetLicensedAccessibleCompanies()).Contains<string>(current.Key, (IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase))
        return;
      SlotStore.Instance.SetSingleCompanyId(cpid);
    }
  }

  protected virtual AuditSetup auditDefinition
  {
    get
    {
      return PXDatabase.GetSlot<AuditSetup>(typeof (AuditSetup).FullName, typeof (AUAuditSetup), typeof (AUAuditTable), typeof (AUAuditField));
    }
  }

  public override void ResetSlots()
  {
    this.Logger.WithEventID("ProfileSlots", nameof (ResetSlots)).WithStack().Debug("ProfileSlots");
    if (WebConfig.IsClusterEnabled)
      this._InsertWatchDog(typeof (PXDatabaseProviderBase.ResetAllTables));
    base.ResetSlots();
  }

  protected void _InsertWatchDog(System.Type table)
  {
    if (PXDatabase.IgnoreChange)
      return;
    string name = table.Name;
    IDbCommand command = (IDbCommand) null;
    try
    {
      if (!WebConfig.IsClusterEnabled)
      {
        PXReaderWriterScope readerWriterScope;
        // ISSUE: explicit constructor call
        ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._SubscribersLock);
        try
        {
          ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
          if (!this._Subscribers.ContainsKey(name.ToLower()))
            return;
        }
        finally
        {
          readerWriterScope.Dispose();
        }
      }
      StringBuilder stringBuilder = new StringBuilder();
      command = (IDbCommand) this.GetCommand();
      int companyId = this.getCompanyID("WatchDog", out companySetting _);
      PXReaderWriterScope readerWriterScope1;
      // ISSUE: explicit constructor call
      ((PXReaderWriterScope) ref readerWriterScope1).\u002Ector(this.__SubscribersStampLock);
      try
      {
        ((PXReaderWriterScope) ref readerWriterScope1).AcquireReaderLock();
        try
        {
          PXDatabaseProviderBase.SubscribersStamp subscribersStamp;
          if (!this._SubscribersStamp.TryGetValue(companyId, out subscribersStamp))
          {
            ((PXReaderWriterScope) ref readerWriterScope1).UpgradeToWriterLock();
            if (!this._SubscribersStamp.TryGetValue(companyId, out subscribersStamp))
              this._SubscribersStamp[companyId] = new PXDatabaseProviderBase.SubscribersStamp()
              {
                _ForceReset = true
              };
          }
        }
        catch
        {
        }
      }
      finally
      {
        readerWriterScope1.Dispose();
      }
      if (companyId > 0)
      {
        stringBuilder.AppendFormat("{2} (CompanyID, TableName) VALUES({0}, {1})", (object) this.CreateParameterName(0), (object) this.CreateParameterName(1), (object) this.SqlDialect.getInsertWatchDog());
        this._AddParameter(command, 0, PXDbType.Int, new int?(4), ParameterDirection.Input, (object) this.getMainCompanyOf(companyId));
      }
      else
        stringBuilder.Append(this.SqlDialect.getInsertWatchDog()).Append($" (TableName) VALUES({this.CreateParameterName(1)})");
      stringBuilder.Append(this._sqlDialect.getLastInsertedIdentity("ChangeID"));
      command.CommandText = stringBuilder.ToString();
      this._AddParameter(command, 1, PXDbType.VarChar, new int?(100), ParameterDirection.Input, (object) name);
      if (command.Connection.State == ConnectionState.Closed)
        this.OpenConnection(command.Connection);
      DbAdapterBase.CorrectCommand(command);
      long int64_1 = Convert.ToInt64(command.ExecuteScalar());
      PXTrace.Logger.WithEventID("InsertWatchDog", "InsertWatchDog").WithStack().Debug<int, string>("{CID} {TableName}", companyId, name);
      if (WebConfig.IsClusterEnabled)
      {
        this.AgeGlobal = int64_1;
        this.TableChanged(name.ToLowerInvariant(), int64_1);
      }
      PXTransactionScope.SetInsertedTable((System.Type) null);
      if (companyId <= 0 || companyId == this.getMainCompanyOf(companyId))
        return;
      command.Parameters.Clear();
      this._AddParameter(command, 0, PXDbType.Int, new int?(4), ParameterDirection.Input, (object) companyId);
      this._AddParameter(command, 1, PXDbType.VarChar, new int?(100), ParameterDirection.Input, (object) name);
      DbAdapterBase.CorrectCommand(command);
      long int64_2 = Convert.ToInt64(command.ExecuteScalar());
      if (!WebConfig.IsClusterEnabled)
        return;
      this.AgeGlobal = int64_2;
      this.TableChanged(name.ToLowerInvariant(), int64_2);
    }
    catch
    {
      if (!WebConfig.IsClusterEnabled)
        return;
      throw;
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

  public sealed override bool Subscribe(System.Type table, PXDatabaseTableChanged handler)
  {
    PXReaderWriterScope readerWriterScope;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._SubscribersLock);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope).AcquireWriterLock();
      string lower = table.Name.ToLower();
      PXDatabaseTableChanged databaseTableChanged;
      if (this._Subscribers.TryGetValue(lower, out databaseTableChanged))
      {
        databaseTableChanged += handler;
        this._Subscribers[lower] = databaseTableChanged;
      }
      else
        this._Subscribers[lower] = handler;
    }
    finally
    {
      readerWriterScope.Dispose();
    }
    return true;
  }

  public sealed override void UnSubscribe(System.Type table, PXDatabaseTableChanged handler)
  {
    PXReaderWriterScope readerWriterScope;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._SubscribersLock);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope).AcquireWriterLock();
      string lower = table.Name.ToLower();
      PXDatabaseTableChanged databaseTableChanged;
      if (!this._Subscribers.TryGetValue(lower, out databaseTableChanged))
        return;
      databaseTableChanged -= handler;
      if (databaseTableChanged == null || databaseTableChanged.GetInvocationList().Length == 0)
        this._Subscribers.Remove(lower);
      else
        this._Subscribers[lower] = databaseTableChanged;
    }
    finally
    {
      readerWriterScope.Dispose();
    }
  }

  public sealed override bool Subscribe(
    System.Type table,
    PXDatabaseTableChanged handler,
    string uniqueKey)
  {
    PXReaderWriterScope readerWriterScope;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._SubscribersLock);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope).AcquireWriterLock();
      string lower = table.Name.ToLower();
      if (!string.IsNullOrEmpty(uniqueKey))
      {
        string key = $"{table.Name.ToLower()}+{uniqueKey}";
        if (this._SlotSubcribers.ContainsKey(key))
          return true;
        this._SlotSubcribers.TryAdd(key, lower);
      }
      PXDatabaseTableChanged databaseTableChanged;
      if (this._Subscribers.TryGetValue(lower, out databaseTableChanged))
      {
        databaseTableChanged += handler;
        this._Subscribers[lower] = databaseTableChanged;
      }
      else
        this._Subscribers[lower] = handler;
    }
    finally
    {
      readerWriterScope.Dispose();
    }
    return true;
  }

  public sealed override void UnSubscribe(
    System.Type table,
    PXDatabaseTableChanged handler,
    string uniqueKey)
  {
    PXReaderWriterScope readerWriterScope;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._SubscribersLock);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope).AcquireWriterLock();
      string lower = table.Name.ToLower();
      PXDatabaseTableChanged databaseTableChanged;
      if (!this._Subscribers.TryGetValue(lower, out databaseTableChanged))
        return;
      if (!string.IsNullOrEmpty(uniqueKey))
        this._SlotSubcribers.TryRemove($"{table.Name.ToLower()}+{uniqueKey}", out string _);
      databaseTableChanged -= handler;
      if (databaseTableChanged == null || databaseTableChanged.GetInvocationList().Length == 0)
        this._Subscribers.Remove(lower);
      else
        this._Subscribers[lower] = databaseTableChanged;
    }
    finally
    {
      readerWriterScope.Dispose();
    }
  }

  public override bool RequiresLogOut
  {
    get
    {
      ISlotStore instance = SlotStore.Instance;
      if (!instance.GetSingleCompanyId().HasValue)
      {
        if (instance.Get<bool>("userMappedCompany"))
        {
          try
          {
            this.getCompanyID("Company", out companySetting _);
          }
          catch (PXUndefinedCompanyException ex)
          {
          }
          return !instance.GetSingleCompanyId().HasValue && instance.Get<bool>("userMappedCompany");
        }
      }
      return false;
    }
  }

  private T prefetchMethod<T>(PrefetchDelegate<T> prefetchDelegate, PXDatabaseSlot slot) where T : class, new()
  {
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    T obj;
    using (AspNetCallbackEnricher.Disable())
    {
      try
      {
        using (new PXResourceGovernorSafeScope())
          obj = prefetchDelegate();
      }
      catch (Exception ex)
      {
        obj = default (T);
        slot.IsValueFaulted = true;
        this.Logger.Error<System.Type>(ex, "Exception in prefetch delegate for {SlotType}", typeof (T));
        if (PXDatabase.PrefetchInSeparateConnection)
          throw;
      }
      finally
      {
        stopwatch.Stop();
        if (stopwatch.ElapsedMilliseconds > (long) PXPerformanceMonitor.PrefetchTreshhold)
          this.Logger.ForTelemetry("ProfileSlots", "PrefetchTimeout").WithStack().Debug<string, long>("ProfileSlots: Prefetch of {Type} took {TimeMs}", typeof (T).FullName, stopwatch.ElapsedMilliseconds);
        this.Logger.WithEventID("ProfileSlots", "PrefetchMethod").WithStack().Debug<string, long>("ProfileSlots: {Type} took {TimeMs}", typeof (T).FullName, stopwatch.ElapsedMilliseconds);
      }
    }
    return obj;
  }

  public override ObjectType GetSlot<ObjectType>(
    string key,
    PrefetchDelegate<ObjectType> prefetchDelegate,
    params System.Type[] tables)
  {
    if (key == null)
      throw new ArgumentNullException(nameof (key));
    bool flag1 = false;
    List<PXDatabaseProviderBase.RequiredSubscriber> requiredSubscriberList = PXContext.GetSlot<List<PXDatabaseProviderBase.RequiredSubscriber>>();
    if (requiredSubscriberList == null)
    {
      requiredSubscriberList = PXContext.SetSlot<List<PXDatabaseProviderBase.RequiredSubscriber>>(new List<PXDatabaseProviderBase.RequiredSubscriber>());
      flag1 = true;
    }
    try
    {
      bool needSubscribe = false;
      int key1 = 0;
      string companyTable = (string) null;
      bool flag2 = typeof (ICrossCompanyPrefetchable).IsAssignableFrom(typeof (ObjectType));
      try
      {
        for (int index = tables.Length - 1; index >= 0; --index)
        {
          key = $"{tables[index].Name}${key}";
          int companyId = flag2 ? 0 : this.getCompanyID(tables[index].Name, out companySetting _);
          if (companyId > key1)
          {
            key1 = companyId;
            companyTable = tables[index].Name;
          }
        }
      }
      catch
      {
        return default (ObjectType);
      }
      ConcurrentDictionary<string, PXDatabaseSlot> orAdd = this._Slots.GetOrAdd(0, new ConcurrentDictionary<string, PXDatabaseSlot>());
      if (this.Companies.Length != 0 && key1 != 0)
        orAdd = this._Slots.GetOrAdd(key1, new ConcurrentDictionary<string, PXDatabaseSlot>());
      else
        companyTable = (string) null;
      PXDatabaseSlot orAddOrUpdate = ConcurrentDictionaryExtensions.GetOrAddOrUpdate<string, PXDatabaseSlot>(orAdd, key, (System.Func<string, PXDatabaseSlot>) (i =>
      {
        needSubscribe = true;
        return new PXDatabaseSlot((System.Func<PXDatabaseSlot, object>) (slot => (object) this.prefetchMethod<ObjectType>(prefetchDelegate, slot)));
      }), (Func<string, PXDatabaseSlot, PXDatabaseSlot>) ((j, existingVal) => existingVal == null || existingVal.IsValueFaulted || existingVal.Value == null ? new PXDatabaseSlot((System.Func<PXDatabaseSlot, object>) (slot => (object) this.prefetchMethod<ObjectType>(prefetchDelegate, slot))) : existingVal));
      if (needSubscribe)
        requiredSubscriberList.Add(new PXDatabaseProviderBase.RequiredSubscriber(key, companyTable, tables, typeof (ObjectType)));
      return (ObjectType) orAddOrUpdate.Value;
    }
    finally
    {
      if (flag1)
      {
        PXContext.SetSlot<List<PXDatabaseProviderBase.RequiredSubscriber>>((List<PXDatabaseProviderBase.RequiredSubscriber>) null);
        foreach (PXDatabaseProviderBase.RequiredSubscriber subscr in requiredSubscriberList)
        {
          foreach (System.Type table in subscr.Tables)
            this.Subscribe(table, new PXDatabaseTableChanged(new PXDatabaseProviderBase.SlotClosing(this, subscr, subscr.CompanyTable != null).Subscribe), subscr.Key);
        }
      }
    }
  }

  public override void ResetSlot<ObjectType>(string key, params System.Type[] tables)
  {
    int key1 = 0;
    bool flag = typeof (ICrossCompanyPrefetchable).IsAssignableFrom(typeof (ObjectType));
    for (int index = tables.Length - 1; index >= 0; --index)
    {
      key = $"{tables[index].Name}${key}";
      int companyId = flag ? 0 : this.getCompanyID(tables[index].Name, out companySetting _);
      if (companyId > key1)
        key1 = companyId;
    }
    ConcurrentDictionary<string, PXDatabaseSlot> orAdd = this._Slots.GetOrAdd(0, new ConcurrentDictionary<string, PXDatabaseSlot>());
    if (this.Companies.Length != 0 && key1 != 0)
      orAdd = this._Slots.GetOrAdd(key1, new ConcurrentDictionary<string, PXDatabaseSlot>());
    PXDatabaseSlot comparisonValue;
    if (!orAdd.TryGetValue(key, out comparisonValue) || comparisonValue == null)
      return;
    orAdd.TryUpdate(key, (PXDatabaseSlot) null, comparisonValue);
  }

  protected internal abstract Tuple<byte[], Decimal?> selectTimestamp();

  private byte[] SelectTimeStamp(bool isCrossCompany)
  {
    using (AspNetCallbackEnricher.Disable())
    {
      PXReaderWriterScope readerWriterScope1;
      // ISSUE: explicit constructor call
      ((PXReaderWriterScope) ref readerWriterScope1).\u002Ector(this.__SubscribersStampLock);
      try
      {
        ((PXReaderWriterScope) ref readerWriterScope1).AcquireReaderLock();
        try
        {
          Tuple<byte[], Decimal?> tuple1 = this.selectTimestamp();
          byte[] numArray = tuple1?.Item1;
          if ((tuple1 != null ? (!tuple1.Item2.HasValue ? 1 : 0) : 1) != 0)
            throw new PXUnderMaintenanceException();
          Decimal num = tuple1.Item2.Value;
          this.AgeGlobal = (long) num;
          if (PXTransactionScope.IsScoped || PXContext.GetSlot<List<PXDatabaseProviderBase.RequiredSubscriber>>() != null)
            return numArray;
          int cid = isCrossCompany ? 0 : this.getCompanyID("WatchDog", out companySetting _);
          PXDatabaseProviderBase.SubscribersStamp stamp;
          if (this._SubscribersStamp.TryGetValue(cid, out stamp) && stamp._SubscribersTStamp != null && stamp._SubscribersChangeID == num)
            return numArray;
          ((PXReaderWriterScope) ref readerWriterScope1).UpgradeToWriterLock();
          if (!this._SubscribersStamp.TryGetValue(cid, out stamp))
          {
            stamp = new PXDatabaseProviderBase.SubscribersStamp();
            this._SubscribersStamp[cid] = stamp;
          }
          else
          {
            if (stamp._SubscribersTStamp != null && stamp._SubscribersChangeID == num)
              return numArray;
            if (this.DatabaseDefinedCompanies && stamp._SubscribersChangeID != 0M && stamp._SubscribersChangeID > num + 100M)
            {
              this.Logger.WithEventID("ProfileSlots", "ClearCompanyNames1").WithStack().Debug<Decimal, Decimal>("ProfileSlots: ResetSlots {SubscribersChangeID} {ID}", stamp._SubscribersChangeID, num);
              this.companyNames = (Dictionary<string, int>) null;
              stamp._SubscribersChangeID = num;
              if (stamp._SubscribersChangeID == 1M)
                stamp._SubscribersChangeID = 0M;
            }
            if (stamp._SubscribersTStamp != null && stamp._SubscribersChangeID >= num)
              return numArray;
          }
          bool flag = true;
          Decimal? nullable1;
          if (stamp._SubscribersTStamp == null)
          {
            stamp._SubscribersTStamp = (byte[]) numArray.Clone();
            stamp._SubscribersDateTime = System.DateTime.Now;
            stamp._SubscribersOldTStamp = stamp._SubscribersTStamp;
            stamp._SubscribersOldDateTime = stamp._SubscribersDateTime;
            nullable1 = new Decimal?(num);
            flag = false;
          }
          if (flag && stamp._SubscribersChangeID < num || stamp._ForceReset)
          {
            System.DateTime now = System.DateTime.Now;
            byte[] second;
            if (now.Subtract(this._SubscribersDateTimeGlobal).TotalMinutes >= 20.0 && !WebConfig.PerformanceTestDontResetSlots || stamp._ForceReset)
            {
              if (this.DatabaseDefinedCompanies)
              {
                this.Logger.WithEventID("ProfileSlots", "ClearCompanyNames2").WithStack().Debug("ProfileSlots: {SubscribersDateTime} {ForceReset} {SubscribersChangeID} {ID}", new object[4]
                {
                  (object) this._SubscribersDateTimeGlobal,
                  (object) stamp._ForceReset,
                  (object) stamp._SubscribersChangeID,
                  (object) num
                });
                this.companyNames = (Dictionary<string, int>) null;
                string[] companies = this.Companies;
              }
              List<PXDatabaseTableChanged> databaseTableChangedList = new List<PXDatabaseTableChanged>();
              try
              {
                PXReaderWriterScope readerWriterScope2;
                // ISSUE: explicit constructor call
                ((PXReaderWriterScope) ref readerWriterScope2).\u002Ector(this._SubscribersLock);
                try
                {
                  ((PXReaderWriterScope) ref readerWriterScope2).AcquireReaderLock();
                  bool forceReset = stamp._ForceReset;
                  stamp._ForceReset = false;
                  foreach (KeyValuePair<string, PXDatabaseTableChanged> keyValuePair in this._Subscribers.ToArray<KeyValuePair<string, PXDatabaseTableChanged>>())
                  {
                    PXDatabaseTableChanged databaseTableChanged = keyValuePair.Value;
                    string key = keyValuePair.Key;
                    if (!forceReset || this.SchemaCache.TableExists(keyValuePair.Key))
                    {
                      if (databaseTableChanged != null)
                        databaseTableChangedList.Add(databaseTableChanged);
                      else
                        Trace.WriteLine("NULL subscriber from " + key);
                    }
                  }
                }
                finally
                {
                  readerWriterScope2.Dispose();
                }
              }
              finally
              {
                foreach (PXDatabaseTableChanged databaseTableChanged in databaseTableChangedList)
                  databaseTableChanged();
              }
              if (WebConfig.IsClusterEnabled)
                this.AllTablesChanged(new long?((long) num));
              this.schemaCache.InvalidateAll();
              second = numArray;
              nullable1 = new Decimal?(num);
            }
            else
            {
              second = stamp._SubscribersTStamp;
              nullable1 = new Decimal?(stamp._SubscribersChangeID);
              List<string> tablesToInvalidate = new List<string>();
              HashSet<string> stringSet = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
              string name = typeof (PXDatabaseProviderBase.ResetAllTables).Name;
              foreach (Tuple<string, long?, byte[]> tuple2 in new PXSelectResult((PXDatabaseProvider) this, new Func<IDbCommand>(CommandFactory)).Select<PXDataRecord, Tuple<string, long?, byte[]>>((System.Func<PXDataRecord, Tuple<string, long?, byte[]>>) (watch => new Tuple<string, long?, byte[]>(watch.GetString(0), watch.GetInt64(1), watch.GetTimeStamp(2)))))
              {
                string a = tuple2.Item1;
                if (name.Equals(a))
                  base.ResetSlots();
                if (tuple2.Item2.HasValue && tuple2.Item2.Value > Convert.ToInt64((object) nullable1))
                {
                  long? nullable2 = tuple2.Item2;
                  nullable1 = nullable2.HasValue ? new Decimal?((Decimal) nullable2.GetValueOrDefault()) : new Decimal?();
                }
                if (this.SqlDialect.CompareTimestamps(tuple2.Item3, second) > 0)
                  second = tuple2.Item3;
                if (a.StartsWith("--") && a.Length > 2)
                {
                  tablesToInvalidate.Add(a.Substring(2));
                }
                else
                {
                  if (this.DatabaseDefinedCompanies && string.Equals(a, "Company", StringComparison.OrdinalIgnoreCase))
                  {
                    this.Logger.WithEventID("ProfileSlots", "ClearCompanyNames3").WithStack().Debug("ProfileSlots");
                    this.companyNames = (Dictionary<string, int>) null;
                  }
                  stringSet.Add(a);
                }
              }
              List<PXDatabaseTableChanged> databaseTableChangedList = new List<PXDatabaseTableChanged>(stringSet.Count);
              PXReaderWriterScope readerWriterScope3;
              // ISSUE: explicit constructor call
              ((PXReaderWriterScope) ref readerWriterScope3).\u002Ector(this._SubscribersLock);
              try
              {
                ((PXReaderWriterScope) ref readerWriterScope3).AcquireReaderLock();
                foreach (string str in stringSet)
                {
                  PXDatabaseTableChanged databaseTableChanged1 = (PXDatabaseTableChanged) null;
                  try
                  {
                    PXDatabaseTableChanged databaseTableChanged2;
                    if (this._Subscribers.TryGetValue(str, out databaseTableChanged2) && databaseTableChanged2 != null)
                      databaseTableChanged1 = databaseTableChanged2;
                    if (WebConfig.IsClusterEnabled)
                      this.TableChanged(str, (long) nullable1.Value);
                  }
                  finally
                  {
                    if (databaseTableChanged1 != null)
                      databaseTableChangedList.Add(databaseTableChanged1);
                  }
                }
              }
              finally
              {
                readerWriterScope3.Dispose();
              }
              foreach (PXDatabaseTableChanged databaseTableChanged in databaseTableChangedList)
                databaseTableChanged();
              this.schemaCache.InvalidateTables(tablesToInvalidate);
            }
            if (now.Subtract(stamp._SubscribersOldDateTime).TotalMinutes >= 25.0)
            {
              IDbCommand command = (IDbCommand) null;
              try
              {
                string str = "";
                command = (IDbCommand) this.GetCommand();
                if (cid > 0)
                {
                  if (this.mainCompanies[cid] == cid)
                  {
                    str = " AND CompanyID = @P0";
                  }
                  else
                  {
                    str = " AND CompanyID IN (@P0, @P1)";
                    this._AddParameter(command, 1, PXDbType.Int, new int?(4), ParameterDirection.Input, (object) cid);
                  }
                  this._AddParameter(command, 0, PXDbType.Int, new int?(4), ParameterDirection.Input, (object) this.mainCompanies[cid]);
                }
                command.CommandText = "DELETE FROM WatchDog WHERE TStamp <= @P2" + str;
                command.CommandTimeout = 30;
                DbAdapterBase.CorrectCommand(command);
                this._AddParameter(command, 2, PXDbType.Timestamp, new int?(8), ParameterDirection.Input, (object) stamp._SubscribersOldTStamp);
                stamp._SubscribersOldDateTime = now;
                stamp._SubscribersOldTStamp = (byte[]) numArray.Clone();
                if (command.Connection.State == ConnectionState.Closed)
                  this.OpenConnection(command.Connection);
                command.ExecuteNonQuery();
              }
              catch
              {
                if (WebConfig.IsClusterEnabled)
                  throw;
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
            stamp._SubscribersChangeID = nullable1.Value;
            if (stamp._SubscribersChangeID == 1M)
              stamp._SubscribersChangeID = 0M;
            this._SubscribersDateTimeGlobal = now;
            stamp._SubscribersDateTime = now;
            stamp._SubscribersTStamp = (byte[]) second.Clone();
            PXTrace.Logger.WithEventID("ProfileSlots", "SubscribersChangeID").WithStack().Debug<System.DateTime, Decimal>("ProfileSlots: {SubscribersDateTime} {SubscribersChangeID}", stamp._SubscribersDateTime, stamp._SubscribersChangeID);
          }
          return numArray;

          IDbCommand CommandFactory()
          {
            bool flag = false;
            IDbCommand command = (IDbCommand) null;
            try
            {
              command = (IDbCommand) this.GetCommand();
              if (cid > 0)
              {
                command.CommandText = "SELECT DISTINCT TableName, ChangeID, TStamp FROM WatchDog WHERE CompanyID = @P0 AND TStamp > @P1";
                this._AddParameter(command, 0, PXDbType.Int, new int?(4), ParameterDirection.Input, (object) this.getMainCompanyOf(cid));
              }
              else
                command.CommandText = "SELECT DISTINCT TableName, ChangeID, TStamp FROM WatchDog WHERE TStamp > @P1";
              this._AddParameter(command, 1, PXDbType.Timestamp, new int?(8), ParameterDirection.Input, (object) stamp._SubscribersTStamp);
              flag = true;
              return command;
            }
            finally
            {
              if (!flag && command != null)
              {
                this.LeaveConnection(command.Connection);
                command.Dispose();
              }
            }
          }
        }
        catch
        {
          if (WebConfig.IsClusterEnabled)
            throw;
        }
      }
      finally
      {
        readerWriterScope1.Dispose();
      }
    }
    return (byte[]) null;
  }

  public override byte[] SelectTimeStamp() => this.SelectTimeStamp(false);

  public override byte[] SelectCrossCompanyTimeStamp() => this.SelectTimeStamp(true);

  internal override void SaveWatchDog(IEnumerable<System.Type> tablesToWatch)
  {
    foreach (System.Type table in tablesToWatch)
      this._InsertWatchDog(table);
  }

  protected void SaveKeyValueStored(
    string tableName,
    PXDBOperation operation,
    PXDataFieldParam[] values)
  {
    PXDataFieldParam pxDataFieldParam = ((IEnumerable<PXDataFieldParam>) values).FirstOrDefault<PXDataFieldParam>((System.Func<PXDataFieldParam, bool>) (_ => _.Storage == StorageBehavior.KeyValueKey && _.Value is Guid));
    if (pxDataFieldParam == null || !(pxDataFieldParam.Value is Guid))
      return;
    Guid key = (Guid) pxDataFieldParam.Value;
    int companyId = this.getCompanyID(tableName, out companySetting _);
    tableName = this._sqlDialect.quoteDbIdentifier(this._sqlDialect.GetKvExtTableName(tableName));
    this.ExecuteQuery((operation & PXDBOperation.Delete) != PXDBOperation.Delete ? this.buildUpdateInsertKvQuery(tableName, values, companyId, key) : string.Format(companyId > 0 ? "DELETE FROM {0} WHERE RecordID = {1} AND CompanyID = {2}" : "DELETE FROM {0} WHERE RecordID = {1}", (object) tableName, (object) this.SqlDialect.enquoteValue((object) key), (object) companyId));
  }

  protected internal void DeleteKeyValueStored(string tableName, string fieldName)
  {
    int companyId = this.getCompanyID(tableName, out companySetting _);
    tableName = this._sqlDialect.quoteDbIdentifier(this._sqlDialect.GetKvExtTableName(tableName));
    this.ExecuteQuery(string.Format(companyId > 0 ? "DELETE FROM {0} WHERE FieldName = {1} AND CompanyID = {2}" : "DELETE FROM {0} WHERE FieldName = {1}", (object) tableName, (object) this.SqlDialect.enquoteValue((object) fieldName), (object) companyId));
  }

  private void ExecuteQuery(string query)
  {
    if (string.IsNullOrEmpty(query))
      return;
    IDbCommand command = (IDbCommand) null;
    PXPerformanceInfo pxPerformanceInfo = (PXPerformanceInfo) null;
    PXProfilerSqlSample profilerSqlSample = (PXProfilerSqlSample) null;
    try
    {
      command = (IDbCommand) this.GetCommand();
      if (PXPerformanceMonitor.IsEnabled)
      {
        pxPerformanceInfo = PXPerformanceMonitor.CurrentSample;
        if (pxPerformanceInfo != null)
        {
          ++pxPerformanceInfo.SqlCounter;
          pxPerformanceInfo.SqlTimer.Start();
          if (PXPerformanceMonitor.SqlProfilerEnabled)
          {
            profilerSqlSample = pxPerformanceInfo.AddSqlSample(command.CommandText, this.ScriptParametersForProfiler(command));
            profilerSqlSample?.SqlTimer.Start();
          }
        }
      }
      if (command.Connection.State == ConnectionState.Closed)
        this.OpenConnection(command.Connection);
      command.CommandText = query;
      DbAdapterBase.CorrectCommand(command);
      command.ExecuteNonQuery();
    }
    finally
    {
      if (command != null)
      {
        this.LeaveConnection(command.Connection);
        command.Dispose();
      }
      if (pxPerformanceInfo != null)
      {
        pxPerformanceInfo.SqlTimer.Stop();
        profilerSqlSample?.SqlTimer.Stop();
      }
    }
  }

  protected virtual string buildUpdateInsertKvQuery(
    string tableName,
    PXDataFieldParam[] values,
    int cid,
    Guid key)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (PXDataFieldParam pxDataFieldParam in values)
    {
      if (pxDataFieldParam.Storage != StorageBehavior.KeyValueKey && pxDataFieldParam.Storage != StorageBehavior.Table)
      {
        string str1 = "NULL";
        string str2 = "NULL";
        string str3 = "NULL";
        string str4 = "NULL";
        object obj = PXFieldState.UnwrapValue(pxDataFieldParam.Value);
        if (obj != null)
        {
          switch (pxDataFieldParam.Storage)
          {
            case StorageBehavior.KeyValueNumeric:
              str1 = Convert.ToString(obj, (IFormatProvider) CultureInfo.InvariantCulture);
              if (!Decimal.TryParse(str1, out Decimal _))
              {
                str1 = string.Equals(str1, "True", StringComparison.InvariantCultureIgnoreCase) ? "1" : "0";
                break;
              }
              break;
            case StorageBehavior.KeyValueDate:
              str2 = this._sqlDialect.enquoteValue(obj);
              break;
            case StorageBehavior.KeyValueString:
              str3 = this._sqlDialect.enquoteValue(obj, PXDbType.NVarChar);
              break;
            case StorageBehavior.KeyValueText:
              str4 = this._sqlDialect.enquoteValue(obj, PXDbType.NText);
              break;
          }
          stringBuilder.Append($"UPDATE {tableName} SET ValueNumeric = {str1}, ValueDate = {str2}, ValueString = {str3}, ValueText = {str4}");
        }
        else
          stringBuilder.AppendFormat("DELETE FROM ").Append(tableName);
        SQLExpression sqlExpression = new Column("FieldName").EQ((object) pxDataFieldParam.Column.Name).And(new Column("RecordID").EQ((object) key));
        if (cid > 0)
          sqlExpression = sqlExpression.And(new Column("CompanyID").EQ((object) cid));
        stringBuilder.AppendFormat(" WHERE {0}", (object) sqlExpression.SQLQuery(this.SqlDialect.GetConnection()));
        if (pxDataFieldParam.Value != null)
        {
          stringBuilder.Append(" IF @@ROWCOUNT = 0 INSERT ");
          stringBuilder.Append(tableName);
          if (cid > 0)
          {
            stringBuilder.Append(" (CompanyID, RecordID, FieldName, ValueNumeric, ValueDate, ValueString, ValueText) VALUES (");
            stringBuilder.Append(cid);
            stringBuilder.Append(", ");
          }
          else
            stringBuilder.Append(" (RecordID, FieldName, ValueNumeric, ValueDate, ValueString, ValueText) VALUES (");
          stringBuilder.Append(this.SqlDialect.enquoteValue((object) key)).Append(", ");
          stringBuilder.Append(this.SqlDialect.enquoteValue((object) pxDataFieldParam.Column.Name)).Append(", ");
          stringBuilder.AppendFormat("{0}, {1}, {2}, {3})", (object) str1, (object) str2, (object) str3, (object) str4);
          stringBuilder.Append("; ");
        }
      }
    }
    return stringBuilder.ToString();
  }

  protected virtual string InsertExpression => "INSERT";

  protected virtual string GetIdentityExpression(
    long? currentBatchId,
    int cntQueriesAudited,
    string tableName)
  {
    if (currentBatchId.HasValue)
      return this.SqlDialect.enquoteValue((object) currentBatchId.Value);
    return cntQueriesAudited != 1 ? "@@IDENTITY" : this.SqlDialect.identCurrent(tableName);
  }

  internal override void SaveAudit(List<PXDatabase.AuditTable> audit)
  {
    IDbCommand cmd = (IDbCommand) null;
    string name = typeof (AuditHistory).Name;
    try
    {
      string screenId = PXAuditHelper.GetScreenID();
      AuditSetup auditDefinition = this.auditDefinition;
      if (auditDefinition == null)
        return;
      int companyId = this.getCompanyID(name, out companySetting _);
      StringBuilder bldcmd = new StringBuilder();
      StringBuilder updcmd = new StringBuilder();
      string str1 = this.SqlDialect.enquoteValue((object) this.CurrentUserInformationProvider.GetUserIdAccountingForImpersonationOrDefault());
      string str2 = this.SqlDialect.enquoteValue(!string.IsNullOrWhiteSpace(screenId) ? (object) screenId : (object) (string) null);
      int cntQueriesAudited = 0;
      long? currentBatchId = new long?();
      string str3 = $"{this.InsertExpression} {name} ({(companyId > 0 ? (object) "CompanyID, " : (object) string.Empty)} BatchID, ScreenID, UserID, ChangeDate, Operation, TableName, CombinedKey, ModifiedFields)";
      for (int index1 = 0; index1 < audit.Count; ++index1)
      {
        PXDatabase.AuditTable table = audit[index1];
        if (table.Fields.Count != 0 && auditDefinition.AuditRequired(table.TableName))
        {
          string str4 = this.SqlDialect.enquoteValue((object) table.Operation);
          string str5 = this.SqlDialect.enquoteValue((object) table.TableName);
          if (cntQueriesAudited > 1)
          {
            bldcmd.Append(" UNION ALL");
          }
          else
          {
            if (cntQueriesAudited == 1)
              bldcmd.Append("; ");
            bldcmd.Append(str3);
          }
          ++cntQueriesAudited;
          string identityExpression = this.GetIdentityExpression(currentBatchId, cntQueriesAudited, name);
          bldcmd.Append(" SELECT ");
          if (companyId > 0)
            bldcmd.Append(companyId.ToString()).Append(", ");
          bldcmd.AppendFormat("{0}, {1}, {2}, {3}, {4}, {5}", (object) identityExpression, (object) str2, (object) str1, (object) this.SqlDialect.GetUtcDate, (object) str4, (object) str5);
          Dictionary<string, int> keyNames;
          string[] bldkeys;
          string[] oldkeys;
          StringBuilder stringBuilder1 = PXDatabaseProviderBase.buildFields(audit, auditDefinition, table, screenId, index1, out keyNames, out bldkeys, out oldkeys);
          bldcmd.Append(", ");
          bool flag = false;
          for (int index2 = 0; index2 < bldkeys.Length; ++index2)
          {
            if (bldkeys[index2] == null)
            {
              string str6;
              if (table.Identity.HasValue && !flag)
              {
                str6 = table.IdentityAsString;
                flag = true;
              }
              else
                str6 = string.Empty;
              oldkeys[index2] = bldkeys[index2] = str6;
            }
          }
          bldcmd.Append(this.SqlDialect.enquoteValue((object) string.Join(this.SqlDialect.WildcardFieldSeparator, bldkeys)));
          bldcmd.Append(", ");
          bldcmd.Append(this.SqlDialect.enquoteValue((object) stringBuilder1.ToString()));
          if (audit[index1].Operation == "U")
          {
            for (int index3 = 0; index3 < bldkeys.Length; ++index3)
            {
              if (!string.Equals(bldkeys[index3], oldkeys[index3], StringComparison.InvariantCultureIgnoreCase))
              {
                updcmd.Append("; UPDATE AuditHistory SET ");
                string str7 = this.SqlDialect.enquoteValue((object) (keyNames.Keys.ElementAt<string>(index3) + this.SqlDialect.WildcardFieldSeparator + oldkeys[index3] + this.SqlDialect.WildcardFieldSeparator));
                updcmd.AppendFormat("CombinedKey = {0}, ModifiedFields = {1} WHERE ", (object) this.SqlDialect.enquoteValue((object) string.Join(this.SqlDialect.WildcardFieldSeparator, bldkeys)), (object) this.SqlDialect.concat(str7, "ModifiedFields"));
                if (companyId > 0)
                  updcmd.AppendFormat("CompanyID = {0} AND ", (object) companyId);
                updcmd.AppendFormat(" TableName = {0} AND CombinedKey = {1}", (object) str5, (object) this.SqlDialect.enquoteValue((object) string.Join(this.SqlDialect.WildcardFieldSeparator, oldkeys)));
                break;
              }
            }
          }
          if (cntQueriesAudited > 100)
          {
            cmd = cmd ?? (IDbCommand) this.GetCommand();
            this.executeSaveAudit(cmd, updcmd, bldcmd);
            if (!currentBatchId.HasValue)
            {
              StringBuilder stringBuilder2 = new StringBuilder($"SELECT BatchId FROM {name} WHERE ");
              if (companyId > 0)
                stringBuilder2.AppendFormat("CompanyID = {0} AND ", (object) companyId);
              stringBuilder2.AppendFormat(" TableName = {0} AND CombinedKey = {1} ORDER BY BatchId DESC", (object) str5, (object) this.SqlDialect.enquoteValue((object) string.Join(this.SqlDialect.WildcardFieldSeparator, bldkeys)));
              using (PXDataRecord pxDataRecord = this.readSingleRecord(this.emptyPars, stringBuilder2.ToString()))
                currentBatchId = (long?) pxDataRecord?.GetInt64(0);
            }
            cntQueriesAudited = 0;
          }
        }
      }
      if (cntQueriesAudited <= 0)
        return;
      cmd = cmd ?? (IDbCommand) this.GetCommand();
      this.executeSaveAudit(cmd, updcmd, bldcmd);
    }
    finally
    {
      if (cmd != null)
      {
        this.LeaveConnection(cmd.Connection);
        cmd.Dispose();
      }
    }
  }

  private void executeSaveAudit(IDbCommand cmd, StringBuilder updcmd, StringBuilder bldcmd)
  {
    if (cmd.Connection.State == ConnectionState.Closed)
      this.OpenConnection(cmd.Connection);
    if (updcmd.Length > 0)
      bldcmd.Append(updcmd.ToString());
    cmd.CommandText = bldcmd.ToString();
    DbAdapterBase.CorrectCommand(cmd);
    if (cmd.ExecuteNonQuery() > 0)
      PXTransactionScope.SetInsertedTable((System.Type) null);
    updcmd.Clear();
    bldcmd.Clear();
  }

  private static StringBuilder buildFields(
    List<PXDatabase.AuditTable> audit,
    AuditSetup setup,
    PXDatabase.AuditTable table,
    string screenId,
    int k,
    out Dictionary<string, int> keyNames,
    out string[] bldkeys,
    out string[] oldkeys)
  {
    keyNames = new Dictionary<string, int>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    HashSet<string> stringSet = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    int index1 = 0;
    foreach (string keyName in setup.GetKeyNames(table.TableName, screenId))
    {
      keyNames[keyName] = index1;
      ++index1;
      stringSet.Add(keyName);
    }
    bldkeys = new string[keyNames.Count];
    oldkeys = new string[keyNames.Count];
    StringBuilder stringBuilder = new StringBuilder();
    HashSet<string> fieldNames = setup.GetFieldNames(table.TableName, screenId);
    int index2 = k;
    bool flag = true;
    while (index2 <= k + 5 && index2 >= k - 5 && index2 >= 0 && index2 < audit.Count)
    {
      for (int index3 = 0; index3 < audit[index2].Fields.Count; ++index3)
      {
        PXDatabase.AuditField auditField = audit[index2].Fields[index3];
        if (keyNames.TryGetValue(auditField.FieldName, out index1))
        {
          stringSet.Remove(auditField.FieldName);
          if (bldkeys[index1] == null)
          {
            string str = table.IdentityName == auditField.FieldName ? table.IdentityAsString : auditField.FieldValue;
            bldkeys[index1] = str;
            oldkeys[index1] = str;
            continue;
          }
          if (auditField.IsRestriction && auditField.FieldValue != null)
          {
            oldkeys[index1] = auditField.FieldValue;
            auditField = new PXDatabase.AuditField(auditField.FieldName, bldkeys[index1], false, auditField.Storage);
          }
        }
        if (!auditField.IsRestriction && (auditField.Storage != StorageBehavior.Table || fieldNames.Contains(auditField.FieldName)) && index2 >= k && flag)
        {
          if (stringBuilder.Length > 0)
            stringBuilder.Append(PXDatabase.Provider.SqlDialect.WildcardFieldSeparatorChar);
          stringBuilder.Append(auditField.FieldName).Append(PXDatabase.Provider.SqlDialect.WildcardFieldSeparatorChar);
          if (auditField.FieldValue != null)
            stringBuilder.Append(auditField.FieldValue);
        }
      }
      if (!((IEnumerable<string>) bldkeys).All<string>((System.Func<string, bool>) (v => v != null)))
      {
        if (flag)
        {
          --index2;
          if (index2 < 0 || index2 < k - 5)
          {
            flag = false;
            index2 = k + 1;
          }
        }
        else
          ++index2;
      }
      else
        break;
    }
    if (stringSet.Count > 0)
      oldkeys = bldkeys;
    return stringBuilder;
  }

  public override bool AuditRequired(string screenID)
  {
    screenID = PXAuditHelper.GetAuditedScreenIDs(screenID).FirstOrDefault<string>() ?? screenID;
    return this.auditDefinition != null && this.auditDefinition.AuditAvailable(screenID);
  }

  internal override bool AuditRequired(System.Type table)
  {
    return this.auditDefinition != null && this.auditDefinition.AuditRequired(table.Name);
  }

  public override bool IsReadDeletedSupported(System.Type table, out string fieldName)
  {
    companySetting tableSetting = this.IsVirtualTable(table) ? (companySetting) null : this.schemaCache.getTableSetting(table.Name, false);
    fieldName = tableSetting?.Deleted;
    return tableSetting != null && tableSetting.Deleted != null;
  }

  public override bool IsRecordStatusSupported(System.Type table, out string fieldName)
  {
    companySetting tableSetting = this.IsVirtualTable(table) ? (companySetting) null : this.schemaCache.getTableSetting(table.Name, false);
    fieldName = tableSetting?.RecordStatus;
    return tableSetting != null && tableSetting.RecordStatus != null;
  }

  public override bool IsVirtualTable(System.Type table)
  {
    return !this.schemaCache.TableExists(table.Name);
  }

  public override IEnumerable<string> GetTables() => this.schemaCache.GetTableNames();

  public override TableHeader GetTableStructure(string tableName)
  {
    return this.schemaCache.GetTableHeader(tableName);
  }

  protected virtual bool canPassOutParamsToUpdate() => false;

  protected bool tryGetSiblings(int cid, out int[] siblings)
  {
    return this.companySiblings.TryGetValue(cid, out siblings);
  }

  protected bool tryGetUpdateableCompanies(int cid, out string updateables)
  {
    return this.updatableCompanies.TryGetValue(cid, out updateables);
  }

  protected internal virtual bool tryGetSelectableCompanies(int cid, out int[] selectables)
  {
    return this.selectableCompanies.TryGetValue(cid, out selectables);
  }

  protected int getMainCompanyOf(int cid)
  {
    int num;
    return !this.mainCompanies.TryGetValue(cid, out num) ? 0 : num;
  }

  /// <summary> Determines which format companyID variable uses and call matching method </summary>
  private void fillCompanyNames()
  {
    if (this.companyID.Contains<char>(';'))
    {
      if (this.companyID.Contains<char>(','))
      {
        this.fillCompanyNames(this.companyID);
        return;
      }
      this.companyID = StringExtensions.FirstSegment(this.companyID, ';');
    }
    int result;
    if (!int.TryParse(this.companyID, out result))
      return;
    this.fillCompanyNames(result, this.DatabaseDefinedCompanies);
  }

  /// <summary> Reads all companies from database, fills companyXXX dictionaries </summary>
  private void fillCompanyNames(int myCompany, bool databaseDefinedCompanies)
  {
    bool flag1 = ProviderKeySuffixSlot.NotSet();
    CompanyHeader[] array1;
    try
    {
      array1 = this.CreateDbServicesPoint(flag1 ? (IDbTransaction) null : (IDbTransaction) PXTransactionScope.GetTransaction()).getCompanies(false).Where<CompanyHeader>((System.Func<CompanyHeader, bool>) (c => c.Id > 0)).ToArray<CompanyHeader>();
    }
    catch
    {
      return;
    }
    Array.Sort<CompanyHeader>(array1);
    Dictionary<int, CompanyHeader> dictionary1 = ((IEnumerable<CompanyHeader>) array1).ToDictionary<CompanyHeader, int>((System.Func<CompanyHeader, int>) (c => c.Id));
    CompanyHeader companyHeader1 = ((IEnumerable<CompanyHeader>) array1).FirstOrDefault<CompanyHeader>((System.Func<CompanyHeader, bool>) (c => c.Id == myCompany));
    List<KeyValuePair<int, string>> source = new List<KeyValuePair<int, string>>();
    CompanyHeader[] companyHeaderArray = array1;
label_19:
    for (int index = 0; index < companyHeaderArray.Length; ++index)
    {
      CompanyHeader cd = companyHeaderArray[index];
      bool flag2 = ((IEnumerable<CompanyHeader>) array1).All<CompanyHeader>((System.Func<CompanyHeader, bool>) (c =>
      {
        if (!c.ParentId.HasValue)
          return true;
        int? parentId = c.ParentId;
        int id = cd.Id;
        return !(parentId.GetValueOrDefault() == id & parentId.HasValue);
      })) && !string.IsNullOrEmpty(cd.Key);
      if (!cd.IsReadonly || !databaseDefinedCompanies && cd.Id == myCompany)
      {
        HashSet<int> intSet = new HashSet<int>();
        bool flag3 = false;
        CompanyHeader companyHeader2 = cd;
        while (!intSet.Contains(companyHeader2.Id))
        {
          intSet.Add(companyHeader2.Id);
          if (companyHeader2.Id == myCompany)
          {
            source.Add(new KeyValuePair<int, string>(cd.Id, flag2 ? cd.Key : (string) null));
            flag3 = true;
          }
          else if (companyHeader2.ParentId.HasValue && dictionary1.TryGetValue(companyHeader2.ParentId.Value, out companyHeader2))
            continue;
          if (!(flag2 | flag3) && companyHeader1 != null)
          {
            intSet.Clear();
            CompanyHeader companyHeader3 = companyHeader1;
            while (companyHeader3.ParentId.HasValue && dictionary1.TryGetValue(companyHeader3.ParentId.Value, out companyHeader3))
            {
              if (intSet.Contains(companyHeader3.Id))
                throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("The infinite loop 2 of companies has been detected.~ {0}", (object) string.Join("\r\n", ((IEnumerable<CompanyHeader>) array1).Select<CompanyHeader, string>((System.Func<CompanyHeader, string>) (_ => _.ParentId.HasValue ? PXMessages.LocalizeFormatNoPrefixNLA("Tenant {0} with parent {1}", (object) _.Id.ToString(), (object) _.ParentId.ToString()) : PXMessages.LocalizeFormatNoPrefixNLA("Tenant {0} with parent null", (object) _.Id.ToString()))))));
              intSet.Add(companyHeader3.Id);
              if (cd.Id == companyHeader3.Id)
              {
                source.Add(new KeyValuePair<int, string>(cd.Id, (string) null));
                break;
              }
            }
            goto label_19;
          }
          goto label_19;
        }
        throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("The infinite loop 1 of companies has been detected.~ {0}", (object) string.Join("\r\n", ((IEnumerable<CompanyHeader>) array1).Select<CompanyHeader, string>((System.Func<CompanyHeader, string>) (_ => _.ParentId.HasValue ? PXMessages.LocalizeFormatNoPrefixNLA("Tenant {0} with parent {1}", (object) _.Id.ToString(), (object) _.ParentId.ToString()) : PXMessages.LocalizeFormatNoPrefixNLA("Tenant {0} with parent null", (object) _.Id.ToString()))))));
      }
    }
    Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
    dictionary2[0] = 0;
    Dictionary<string, int> dictionary3 = new Dictionary<string, int>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
    if (source.Count > 1)
    {
      Dictionary<int, int> dictionary4 = new Dictionary<int, int>();
      int num = 0;
      foreach (KeyValuePair<int, string> keyValuePair in source)
      {
        dictionary4[keyValuePair.Key] = num++;
        if ((!dictionary1[keyValuePair.Key].IsReadonly || !databaseDefinedCompanies && keyValuePair.Key == myCompany) && !string.IsNullOrEmpty(keyValuePair.Value))
        {
          dictionary3[keyValuePair.Value] = keyValuePair.Key;
          dictionary2[keyValuePair.Key] = keyValuePair.Key;
        }
      }
      if (!source.Any<KeyValuePair<int, string>>())
      {
        int key = dictionary4.Keys.Last<int>();
        if (key > 0)
          dictionary2[key] = key;
      }
      this.companyPosition = dictionary4;
    }
    else
    {
      if (source.Count == 1)
      {
        KeyValuePair<int, string> keyValuePair = source[0];
        this._singleCompanyID = myCompany = keyValuePair.Key;
      }
      dictionary2[myCompany] = myCompany;
      this.companyPosition = new Dictionary<int, int>();
    }
    bool flag4 = false;
    PXReaderWriterScope readerWriterScope1;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope1).\u002Ector(this._SubscribersLock);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope1).AcquireWriterLock();
      try
      {
        string[] array2 = this._Subscribers.Keys.ToArray<string>();
        for (int index = array2.Length - 1; index >= 0; --index)
        {
          PXDatabaseTableChanged subscriber;
          if (this._Subscribers.TryGetValue(array2[index], out subscriber) && subscriber != null)
          {
            foreach (Delegate invocation in subscriber.GetInvocationList())
            {
              if (invocation.Target is PXDatabaseProviderBase.SlotClosing target)
              {
                this._Subscribers[array2[index]] -= new PXDatabaseTableChanged(target.Subscribe);
                this._SlotSubcribers.TryRemove($"{array2[index]}+{target.subscriber.Key}", out string _);
              }
            }
            subscriber = this._Subscribers[array2[index]];
            if (subscriber == null || subscriber.GetInvocationList().Length == 0)
              this._Subscribers.Remove(array2[index]);
          }
        }
      }
      finally
      {
        this._Slots = new ConcurrentDictionary<int, ConcurrentDictionary<string, PXDatabaseSlot>>();
        flag4 = true;
      }
    }
    finally
    {
      readerWriterScope1.Dispose();
    }
    PXReaderWriterScope readerWriterScope2;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope2).\u002Ector(this._CompaniesSlotsLock);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope2).AcquireWriterLock();
      this.mainCompanies = dictionary2;
      this.companySiblings = new Dictionary<int, int[]>();
      this.companyMappings = source.ToDictionary<KeyValuePair<int, string>, int, string>((System.Func<KeyValuePair<int, string>, int>) (k => k.Key), (System.Func<KeyValuePair<int, string>, string>) (v => v.Value));
      this.companyNames = dictionary3;
      if (flag4)
        this.Logger.WithEventID("ProfileSlots", "FillCompanyNames").WithStack().Debug("ProfileSlots");
      Dictionary<int, string> dictionary5 = new Dictionary<int, string>();
      foreach (CompanyHeader companyHeader4 in array1)
      {
        string str = companyHeader4.Cd;
        if (string.IsNullOrWhiteSpace(str))
          str = companyHeader4.Key;
        if (string.IsNullOrWhiteSpace(str))
          str = companyHeader4.Id.ToString();
        dictionary5[companyHeader4.Id] = str;
      }
      this.companyDisplayNames = dictionary5;
      if (this.companyCnt > 1 && dictionary3.Count <= 1)
      {
        LogoutRequestTracker.RequestLogout();
        PXLogin.RequestInvalidating();
      }
      this.companyCnt = dictionary3.Count;
    }
    finally
    {
      readerWriterScope2.Dispose();
    }
  }

  /// <summary> parses company data from an entry in web.config </summary>
  public void fillCompanyNames(string companyID)
  {
    Dictionary<int, int> dictionary1 = new Dictionary<int, int>();
    dictionary1[0] = 0;
    Dictionary<int, int[]> dictionary2 = new Dictionary<int, int[]>();
    string[] source = companyID.Split(new char[1]{ ',' }, StringSplitOptions.RemoveEmptyEntries);
    Dictionary<int, string> dictionary3 = new Dictionary<int, string>();
    if (source.Length <= 1)
    {
      this.mainCompanies = dictionary1;
      this.companySiblings = dictionary2;
      this.companyMappings = dictionary3;
    }
    else
    {
      Dictionary<string, int> dictionary4 = new Dictionary<string, int>();
      Dictionary<int, int> dictionary5 = new Dictionary<int, int>();
      int num = 0;
      try
      {
        foreach (string[] strArray in ((IEnumerable<string>) source).Select<string, string[]>((System.Func<string, string[]>) (m => m.Split(';'))).Where<string[]>((System.Func<string[], bool>) (p => p.Length > 1)))
        {
          int result;
          if (int.TryParse(strArray[0], out result))
          {
            dictionary5[result] = num++;
            if (!string.IsNullOrEmpty(strArray[1]))
            {
              dictionary4[strArray[1]] = result;
              dictionary1[result] = result;
              dictionary3[result] = strArray[1];
            }
          }
        }
        if (((IEnumerable<string>) source).Any<string>())
          return;
        int key1 = 0;
        foreach (int key2 in dictionary5.Keys)
          key1 = key2;
        if (key1 <= 0)
          return;
        dictionary1[key1] = key1;
      }
      finally
      {
        PXReaderWriterScope readerWriterScope;
        // ISSUE: explicit constructor call
        ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._CompaniesSlotsLock);
        try
        {
          ((PXReaderWriterScope) ref readerWriterScope).AcquireWriterLock();
          this.companyNames = dictionary4;
          this.companyPosition = dictionary5;
          this.mainCompanies = dictionary1;
          this.companySiblings = dictionary2;
          this.companyMappings = dictionary3;
        }
        finally
        {
          readerWriterScope.Dispose();
        }
      }
    }
  }

  /// <summary> Fills dictionaries updatableCompanies, selectableCompanies, companySiblings, mainCompanies</summary>
  private void fillSelUpdCompanies(int? singleCompanyID)
  {
    this.templateCompanies.Clear();
    List<CompanyHeader> companies = this.CreateDbServicesPoint(ProviderKeySuffixSlot.NotSet() ? (IDbTransaction) null : (IDbTransaction) PXTransactionScope.GetTransaction()).getCompanies(true);
    Dictionary<int, int[]> dictionary1 = new Dictionary<int, int[]>();
    Dictionary<int, string> dictionary2 = new Dictionary<int, string>();
    Dictionary<int, int> dictionary3 = new Dictionary<int, int>();
    Dictionary<int, int> dictionary4 = new Dictionary<int, int>((IDictionary<int, int>) this.mainCompanies);
    Dictionary<int, int[]> dictionary5 = new Dictionary<int, int[]>();
    foreach (CompanyHeader companyHeader in companies.Where<CompanyHeader>((System.Func<CompanyHeader, bool>) (c => c.Id > 0)))
    {
      if (companyHeader.ParentId.HasValue)
        dictionary3[companyHeader.Id] = companyHeader.ParentId.Value;
      if (companyHeader.IsTemplate)
        this.templateCompanies.Add(companyHeader.Id);
    }
    List<int> intList1 = new List<int>();
    if (this.companyNames != null && this.companyNames.Count > 0)
    {
      intList1.AddRange((IEnumerable<int>) this.companyPosition.Keys);
      Dictionary<int, string> dictionary6 = new Dictionary<int, string>();
      foreach (CompanyHeader companyHeader in companies.Where<CompanyHeader>((System.Func<CompanyHeader, bool>) (c => c.ParentId.HasValue)))
      {
        string str = companyHeader.Cd;
        if (string.IsNullOrWhiteSpace(str))
          str = companyHeader.Key;
        dictionary6[companyHeader.Id] = str;
      }
      this.companyDisplayNames = dictionary6;
    }
    else
    {
      this.fillCompanyNames();
      dictionary4 = new Dictionary<int, int>((IDictionary<int, int>) this.mainCompanies);
      if (this.companyNames != null && this.companyNames.Count > 0)
        intList1.AddRange((IEnumerable<int>) this.companyPosition.Keys);
      else
        intList1.Add(singleCompanyID.Value);
    }
    foreach (int key1 in intList1)
    {
      List<int> intList2 = new List<int>();
      StringBuilder stringBuilder = new StringBuilder();
      int num;
      for (int key2 = key1; dictionary3.TryGetValue(key2, out num); key2 = num)
      {
        intList2.Add(num);
        if (intList1.Contains(num))
        {
          if (stringBuilder.Length > 0)
            stringBuilder.Append(", ");
          stringBuilder.Append(num);
          dictionary4[key1] = num;
        }
      }
      if (intList2.Count > 0)
        dictionary1[key1] = intList2.ToArray();
      if (stringBuilder.Length > 0)
        dictionary2[key1] = stringBuilder.ToString();
    }
    foreach (KeyValuePair<int, int> keyValuePair1 in dictionary4)
    {
      if (keyValuePair1.Key != keyValuePair1.Value)
      {
        List<int> intList3 = new List<int>();
        int num1 = 0;
        foreach (KeyValuePair<int, int> keyValuePair2 in dictionary3)
        {
          if (keyValuePair2.Value == keyValuePair1.Value)
          {
            intList3.Add(keyValuePair2.Key);
            if (keyValuePair2.Key > num1)
              num1 = keyValuePair2.Key;
          }
        }
        if (intList3.Count > 1)
        {
          int[] numArray = new int[(num1 - 1) / 4 + 1];
          foreach (int num2 in intList3)
            numArray[(num2 - 1) / 4] |= 2 << 2 * ((num2 + 3) % 4);
          dictionary5[keyValuePair1.Key] = numArray;
        }
      }
    }
    this.updatableCompanies = dictionary2;
    this.mainCompanies = dictionary4;
    this.companySiblings = dictionary5;
    this.selectableCompanies = dictionary1;
  }

  protected internal SQLExpression GetRestrictionExpression(
    string table,
    string alias,
    bool mainRestriction,
    bool isRightJoin = false,
    int? effectiveCid = null)
  {
    if (table == null)
      return (SQLExpression) null;
    SQLExpression sqlExpression1 = SQLExpression.None();
    Column column = new Column("CompanyID", alias);
    SQLExpression sqlExpression2 = isRightJoin ? column.IsNull() : SQLExpression.None();
    companySetting settings;
    int companyId = this.getCompanyID(table, out settings);
    if (effectiveCid.HasValue)
      companyId = effectiveCid.Value;
    if (companyId == 0)
      return !companySetting.NeedRestrict(settings) ? (SQLExpression) null : sqlExpression2.Or(SQLExpressionExt.EQ(column.Duplicate(), (SQLExpression) new SQLConst((object) 0)));
    SQLExpression sharingExpression = SQLExpression.GetRowSharingExpression(settings, column.Duplicate() as Column, new Column("CompanyMask"), companyId);
    SQLExpression r1 = sqlExpression2.Or(sharingExpression).Embrace();
    SQLExpression restrictions = sqlExpression1.And(r1);
    if (settings.Deleted != null)
    {
      SQLExpression r2 = (SQLExpression) null;
      if (!PXDatabase.ReadDeleted)
        r2 = (SQLExpression) new SQLConst((object) 0);
      else if (mainRestriction && PXDatabase.ReadOnlyDeleted)
        r2 = (SQLExpression) new SQLConst((object) 1);
      if (r2 != null)
      {
        Column l = new Column(settings.Deleted, alias);
        SQLExpression r3 = !isRightJoin ? SQLExpressionExt.EQ(l, r2) : l.IsNull().Or(SQLExpressionExt.EQ(l, r2));
        restrictions = restrictions.And(r3);
      }
    }
    SQLExpression restrictionExpression = restrictions.AddReadArchivedRestrictionIfNeeded(settings, alias, isRightJoin);
    if (settings.WebAppType != null)
    {
      SQLExpression r4 = (SQLExpression) new SQLConst((object) WebAppType.Current.AppTypeId);
      Column l = new Column(settings.WebAppType, alias);
      SQLExpression r5 = !isRightJoin ? SQLExpressionExt.EQ(l, r4) : l.IsNull().Or(SQLExpressionExt.EQ(l, r4));
      restrictionExpression = restrictionExpression.And(r5);
    }
    if (settings.Branch != null && !PXDatabase.ReadBranchRestricted && (PXDatabase.SpecificBranchTable == null || string.Equals(table, PXDatabase.SpecificBranchTable, StringComparison.OrdinalIgnoreCase)))
    {
      List<int> branchIDs = PXDatabase.BranchIDs ?? this.CurrentUserInformationProvider.GetAllBranches().Select<BranchInfo, int>((System.Func<BranchInfo, int>) (b => b.Id)).Distinct<int>().ToList<int>();
      int count = branchIDs.Count;
      Column l = new Column(settings.Branch, alias);
      restrictionExpression = count != 1 ? (count <= 1 ? restrictionExpression.And(l.IsNull().Or(l.EQ((object) 0))) : restrictionExpression.And(l.IsNull().Or(l.In((IEnumerable<SQLExpression>) branchIDs.Select<int, SQLConst>((System.Func<int, SQLConst>) (b => new SQLConst((object) branchIDs))))))) : restrictionExpression.And(l.IsNull().Or(l.EQ((object) branchIDs[0])));
    }
    return restrictionExpression;
  }

  /// <summary> Builds restriction line to optionally filter out lines belonging to different companies/branches and deleted records </summary>
  protected internal string getRestriction(
    string table,
    string alias,
    bool mainRestriction,
    bool isRightJoin = false,
    int? effectiveCid = null)
  {
    if (table == null)
      return (string) null;
    List<string> values1 = new List<string>();
    string str1 = isRightJoin ? alias + ".CompanyID IS NULL OR " : string.Empty;
    companySetting settings;
    int companyId = this.getCompanyID(table, out settings);
    if (effectiveCid.HasValue)
      companyId = effectiveCid.Value;
    if (companyId == 0)
      return !companySetting.NeedRestrict(settings) ? (string) null : $"{str1}{alias}.CompanyID = 0";
    int[] values2;
    if (settings.Flag != companySetting.companyFlag.Separate && this.selectableCompanies.TryGetValue(companyId, out values2))
      values1.Add(string.Format("({4}{3}.CompanyID IN ({0}, {1}) AND {2})", (object) string.Join<int>(", ", (IEnumerable<int>) values2), (object) companyId, (object) this.SqlDialect.binaryMaskTest(alias + ".CompanyMask", companyId, 2), (object) alias, (object) str1));
    else
      values1.Add(string.Format("({2}{1}.CompanyID = {0})", (object) companyId, (object) alias, (object) str1));
    if (settings.Deleted != null)
    {
      string str2 = (string) null;
      if (!PXDatabase.ReadDeleted)
        str2 = " = 0";
      else if (mainRestriction && PXDatabase.ReadOnlyDeleted)
        str2 = " = 1";
      if (!string.IsNullOrWhiteSpace(str2))
      {
        string str3 = this._sqlDialect.quoteTableAndColumn(alias, settings.Deleted);
        string str4 = !isRightJoin ? str3 + str2 : string.Format("({0} IS NULL OR {0}{1})", (object) str3, (object) str2);
        values1.Add(str4);
      }
    }
    SQLExpression expression;
    if (SQLTreeExtensions.TryGetReadArchivedRestrictionIfNeeded(settings, alias, out expression, isRightJoin))
    {
      StringBuilder stringBuilder = expression.SQLQuery(this._sqlDialect.GetConnection());
      values1.Add(stringBuilder.ToString());
    }
    if (settings.Branch != null && !PXDatabase.ReadBranchRestricted && (PXDatabase.SpecificBranchTable == null || string.Equals(table, PXDatabase.SpecificBranchTable, StringComparison.OrdinalIgnoreCase) || string.Equals(this.SqlDialect.unquoteTable(table), PXDatabase.SpecificBranchTable, StringComparison.OrdinalIgnoreCase)))
    {
      List<int> values3 = PXDatabase.BranchIDs ?? this.CurrentUserInformationProvider.GetAllBranches().Select<BranchInfo, int>((System.Func<BranchInfo, int>) (b => b.Id)).Distinct<int>().ToList<int>();
      int count = values3.Count;
      if (count == 1)
        values1.Add(string.Format("({0}.{1} IS NULL OR {0}.{1} = {2})", (object) alias, (object) settings.Branch, (object) values3[0]));
      else if (count > 1)
        values1.Add(string.Format("({0}.{1} IS NULL OR {0}.{1} IN ({2}))", (object) alias, (object) settings.Branch, (object) string.Join<int>(", ", (IEnumerable<int>) values3)));
    }
    return string.Join(" AND ", (IEnumerable<string>) values1);
  }

  /// <summary> Determines which per-company restrictions apply to given table </summary>
  /// <returns>0 if no restrictions apply</returns>
  internal override int getCompanyID(string tableName, out companySetting setting)
  {
    tableName = this.SqlDialect.unquoteTable(tableName);
    int currentCompany;
    try
    {
      currentCompany = this.GetCurrentCompany();
    }
    catch (Exception ex) when (ex is NotLoggedInException || ex is PXUndefinedCompanyException)
    {
      setting = this.schemaCache.getTableSetting(tableName, true);
      if (setting.Flag == companySetting.companyFlag.Global || setting.Flag == companySetting.companyFlag.UserGlobal)
        return 0;
      throw ex is NotLoggedInException ? (Exception) new PXNotLoggedInException() : (Exception) new PXUndefinedCompanyException();
    }
    if (this.selectableCompanies == null)
      this.fillSelUpdCompanies(new int?(currentCompany));
    setting = this.schemaCache.getTableSetting(tableName, this.selectableCompanies == null || !this.selectableCompanies.ContainsKey(currentCompany));
    if (setting.Flag == companySetting.companyFlag.Global || setting.Flag == companySetting.companyFlag.UserGlobal)
      return 0;
    if (currentCompany == 0)
      setting = setting.copyAsDedicated();
    return currentCompany;
  }

  private int GetCurrentCompany()
  {
    ISlotStore instance = SlotStore.Instance;
    return instance.GetSingleCompanyId() ?? this.InitializeCurrentCompany(instance, PXContext.PXIdentity.User);
  }

  internal override int InitializeCurrentCompany(ISlotStore slots, IPrincipal principal)
  {
    IPrincipal principal1 = !Anonymous.IsAnonymous(principal) ? principal : (IPrincipal) null;
    int? nullable = new int?();
    if (this.Companies.Length != 0)
    {
      slots.Set("userMappedCompany", (object) true);
      if (principal1 != null)
      {
        string name = principal1.Identity.Name;
        if (name != null)
        {
          string company;
          LegacyCompanyService.ParseLogin((PXDatabaseProvider) this, name, out string _, out company, out string _);
          int num;
          if (this.companyNames.TryGetValue(company ?? string.Empty, out num))
          {
            slots.SetSingleCompanyId(num);
            nullable = new int?(num);
          }
        }
      }
    }
    else if (this.companyPosition == null || this.companyPosition.Count == 0)
    {
      slots.SetSingleCompanyId(this._singleCompanyID);
      nullable = new int?(this._singleCompanyID);
    }
    else
    {
      foreach (int key in this.companyPosition.Keys)
      {
        slots.SetSingleCompanyId(key);
        nullable = new int?(key);
      }
    }
    if (nullable.HasValue)
      return nullable.Value;
    if (principal1 == null)
      throw new NotLoggedInException();
    throw new PXUndefinedCompanyException();
  }

  protected abstract bool isInvalidObjectException(DbException dbException);

  public override string[] Companies
  {
    get
    {
      if (this.companyNames == null)
      {
        using (new PXIdentityScope())
        {
          this.fillCompanyNames();
          this.fillSelUpdCompanies(new int?(SlotStore.Instance.GetSingleCompanyId() ?? this._singleCompanyID));
        }
      }
      return this.companyNames != null ? this.companyNames.Keys.ToArray<string>() : new string[0];
    }
  }

  public override string[] DbCompanies
  {
    get
    {
      return this.companyMappings != null ? this.companyMappings.Values.ToArray<string>() : (string[]) null;
    }
  }

  protected internal override Dictionary<int, string> CompanyMappings
  {
    get => this.companyMappings ?? base.CompanyMappings;
  }

  public sealed override void ClearCompaniesCache()
  {
    this.Logger.WithEventID("ProfileSlots", nameof (ClearCompaniesCache)).WithStack().Debug("ProfileSlots");
    this.selectableCompanies = (Dictionary<int, int[]>) null;
    this.companyNames = (Dictionary<string, int>) null;
  }

  public override string GetCompanyDisplayName()
  {
    int currentCompany = this.GetCurrentCompany();
    string companyDisplayName1 = (string) null;
    if (this.companyDisplayNames != null && this.companyDisplayNames.TryGetValue(currentCompany, out companyDisplayName1))
      return companyDisplayName1;
    Dictionary<int, string> dictionary = new Dictionary<int, string>();
    foreach (CompanyInfo selectCompany in this.SelectCompanies(false, false))
    {
      string str = selectCompany.CompanyCD;
      if (string.IsNullOrWhiteSpace(str))
        str = selectCompany.LoginName;
      if (string.IsNullOrWhiteSpace(str))
        str = selectCompany.CompanyID.ToString();
      dictionary[selectCompany.CompanyID] = str;
    }
    string companyDisplayName2 = dictionary[currentCompany];
    this.companyDisplayNames = dictionary;
    return companyDisplayName2;
  }

  public override void SetDesignTimeCompany()
  {
    if (this.Companies.Length == 0)
      return;
    SlotStore.Instance.SetSingleCompanyId(this.companyNames[this.Companies[0]]);
    this.fillSelUpdCompanies(new int?(this.companyNames[this.Companies[0]]));
  }

  public override void ResetCredentials() => SlotStore.Instance.ClearSingleCompanyId();

  protected void alterCommandForSharedRecords(IDbCommand cmd, int cid, string listCompanies)
  {
    string str = cmd.CommandText.Substring(0, 11 + cmd.CommandText.LastIndexOf(" CompanyID = ", StringComparison.Ordinal));
    cmd.CommandText = str + $" IN({listCompanies}) AND {this.SqlDialect.binaryMaskTest("CompanyMask", cid, 3)}";
  }

  public sealed override CompanyInfo[] SelectCompanies(
    bool includeNegative,
    bool includeMarkedForDeletion = false)
  {
    List<CompanyInfo> source = new List<CompanyInfo>();
    IDbCommand dbCommand = (IDbCommand) null;
    IDataReader dataReader = (IDataReader) null;
    try
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("SELECT c.* FROM Company c");
      if (this.SchemaCache.GetTableHeader("Company").getColumnByName("IsUnderDeletion") != null && !includeMarkedForDeletion)
      {
        stringBuilder.Append(" WHERE (c.IsUnderDeletion IS NULL OR c.IsUnderDeletion = 0)");
        if (!includeNegative)
          stringBuilder.Append(" AND c.CompanyID > 0");
      }
      else if (!includeNegative)
        stringBuilder.Append(" WHERE c.CompanyID > 0");
      dbCommand = (IDbCommand) this.GetCommand();
      dbCommand.CommandText = stringBuilder.ToString();
      if (dbCommand.Connection.State == ConnectionState.Closed)
        this.OpenConnection(dbCommand.Connection);
      DbAdapterBase.CorrectCommand(dbCommand);
      dataReader = dbCommand.ExecuteReader();
      Dictionary<int, string> loginCompanies = this.GetLoginCompanies();
      int ordinal1 = dataReader.GetOrdinal("CompanyID");
      int ordinal2 = dataReader.GetOrdinal("CompanyType");
      int ordinal3 = dataReader.GetOrdinal("IsTemplate");
      int ordinal4 = dataReader.GetOrdinal("Sequence");
      int ordinal5 = dataReader.GetOrdinal("ParentCompanyID");
      int i1 = -1;
      try
      {
        i1 = dataReader.GetOrdinal("CompanyKey");
      }
      catch (IndexOutOfRangeException ex)
      {
      }
      int i2 = -1;
      try
      {
        i2 = dataReader.GetOrdinal("IsReadOnly");
      }
      catch (IndexOutOfRangeException ex)
      {
      }
      int i3 = -1;
      try
      {
        i3 = dataReader.GetOrdinal("Size");
      }
      catch (IndexOutOfRangeException ex)
      {
      }
      while (dataReader.Read())
      {
        int int32 = dataReader.GetInt32(ordinal1);
        DataTypeInfo dataType = PXDataTypesHelper.GetTypeByCode(dataReader.IsDBNull(ordinal2) ? (string) null : dataReader.GetString(ordinal2).Trim()) ?? PXDataTypesHelper.UserCompany;
        string str = i1 >= 0 ? (!dataReader.IsDBNull(i1) ? dataReader.GetString(i1) : (string) null) : (string) null;
        bool flag = i2 >= 0 && !dataReader.IsDBNull(i2) && dataReader.GetBoolean(i2);
        CompanyInfo ci = new CompanyInfo(dataType, int32)
        {
          CompanyCD = dataReader["CompanyCD"].ToString().Trim(),
          ParentID = dataReader.IsDBNull(ordinal5) ? -1 : dataReader.GetInt32(ordinal5),
          Exist = true,
          Sequence = dataReader.IsDBNull(ordinal4) ? new int?() : new int?(dataReader.GetInt32(ordinal4)),
          Size = dataReader.IsDBNull(i3) ? new long?() : new long?(dataReader.GetInt64(i3))
        };
        ci.UpdateableTemplate = ordinal3 >= 0 && !dataReader.IsDBNull(ordinal3) && dataReader.GetBoolean(ordinal3);
        if (ci.System)
          ci.Hidden = true;
        if (ci.CompanyID > 0)
        {
          if (loginCompanies.Keys.Contains<int>(int32))
          {
            ci.Joined = true;
            ci.LoginName = loginCompanies[int32];
            if (loginCompanies[int32] == string.Empty)
              ci.Hidden = true;
          }
          else if (this.DatabaseDefinedCompanies)
          {
            ci.Joined = !flag;
            ci.LoginName = str;
          }
        }
        if (ci.LoginName == null)
          ci.LoginName = str;
        if (!ci.Hidden & flag)
          ci.Hidden = flag;
        if (source.All<CompanyInfo>((System.Func<CompanyInfo, bool>) (c => c.CompanyID != ci.CompanyID)))
          source.Add(ci);
      }
    }
    catch
    {
      source = (List<CompanyInfo>) null;
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
    return source?.ToArray();
  }

  public override bool IsReadDeletedSupported(System.Type table)
  {
    return this.IsReadDeletedSupported(table, out string _);
  }

  public override IEnumerable<DataVersion> SelectVersions()
  {
    return this.CreateDbServicesPoint().getDataVersions(true);
  }

  protected internal sealed override void SetRestrictedTables(string[] list)
  {
    this._RestrictedTables = new HashSet<string>((IEnumerable<string>) list, (IEqualityComparer<string>) PXLocalesProvider.CollationComparer);
  }

  internal abstract IDbDataParameter _AddParameter(
    IDbCommand command,
    int parameterIndex,
    PXDbType type,
    int? size,
    ParameterDirection direction,
    object parameterValue,
    PXDatabaseProviderBase.ParameterBehavior behavior = PXDatabaseProviderBase.ParameterBehavior.Unknown);

  protected internal override void Commit(IDbTransaction tran) => tran.Commit();

  protected internal override void Rollback(IDbTransaction tran)
  {
    if (tran.Connection.State == ConnectionState.Closed)
      return;
    tran.Rollback();
  }

  protected internal override DbTransaction CreateTransaction()
  {
    DbConnection connection = this.GetConnection() ?? this.CreateConnection();
    if (connection == null)
      return (DbTransaction) null;
    if (connection.State == ConnectionState.Closed)
      this.OpenConnection((IDbConnection) connection);
    return connection.BeginTransaction();
  }

  internal override IEnumerable<PXDataRecord> Select(
    Query query,
    IEnumerable<PXDataValue> queryParameters,
    System.Action<PXDatabaseProvider.ExecutionParameters> configurator = null)
  {
    return (IEnumerable<PXDataRecord>) new PXSelectResult((PXDatabaseProvider) this, (Func<(IDbCommand, int, TimeSpan)>) (() =>
    {
      (DbCommand, int, TimeSpan) enumerable = this.GetEnumerable(query, queryParameters, configurator);
      return ((IDbCommand) enumerable.Item1, enumerable.Item2, enumerable.Item3);
    }));
  }

  internal override IAsyncEnumerable<PXDataRecord> SelectAsync(
    Query query,
    IEnumerable<PXDataValue> queryParameters,
    System.Action<PXDatabaseProvider.ExecutionParameters> configurator = null,
    CancellationToken token = default (CancellationToken))
  {
    return AsyncEnumerableExtensions.OnCurrentSynchronizationContext<PXDataRecord>((IAsyncEnumerable<PXDataRecord>) new PXSelectAsyncResult((PXDatabaseProvider) this, (Func<(DbCommand, int, TimeSpan)>) (() => this.GetEnumerable(query, queryParameters, configurator)), token), token);
  }

  private (DbCommand, int, TimeSpan) GetEnumerable(
    Query query,
    IEnumerable<PXDataValue> queryParameters,
    System.Action<PXDatabaseProvider.ExecutionParameters> configurator = null)
  {
    bool flag1 = false;
    DbCommand cmd = (DbCommand) null;
    try
    {
      cmd = this.GetCommand();
      if (OptimizedExportScope.IsScoped)
        OptimizedExportScope.CleanupExpressions();
      long limit = (long) query.GetLimit();
      query = (Query) query.Duplicate();
      query.ApplyHints(query.GetHints() | PXDatabaseProvider.DefaultQueryHints);
      PXDataValue[] array = queryParameters.ToArray<PXDataValue>();
      this.addSelectParameters((IDbCommand) cmd, array);
      query.InjectDirectExpressions(array);
      query.AppendRestrictions();
      query = query.FlattenSubselects(out bool _);
      Connection connection = this.SqlDialect.GetConnection();
      string str = query.SQLQuery(connection).ToString();
      bool flag2 = PXSqlLimits.VerifyRowCountLimit(ref limit);
      PXDatabaseProvider.ExecutionParameters executionParameters = new PXDatabaseProvider.ExecutionParameters(cmd.CommandTimeout);
      if (configurator != null)
        configurator(executionParameters);
      cmd.CommandText = this.InsertUserAndScreenAsComment(str, this.getUserForComment(), this.getScreenForComment());
      cmd.CommandTimeout = executionParameters.CommandTimeout;
      if (executionParameters.TraceQuery)
        this.Logger.WithStack().Information<string>("{SQL}", this.prepareTrace(array, str));
      TimeSpan valueOrDefault = PXSqlLimits.GetSqlTimeLimit((IDbCommand) cmd).GetValueOrDefault();
      flag1 = true;
      return (cmd, flag2 ? (int) limit : 0, valueOrDefault);
    }
    finally
    {
      if (!flag1 && cmd != null)
      {
        this.LeaveConnection((IDbConnection) cmd.Connection);
        cmd.Dispose();
      }
    }
  }

  private string InsertUserAndScreenAsComment(string queryText, string user, string screen)
  {
    return !WebConfig.InQueryInfoComment || PerformanceMonitorSqlSampleScope.BqlHash == null || !queryText.StartsWith("SELECT ", StringComparison.OrdinalIgnoreCase) && !queryText.StartsWith("UPDATE ", StringComparison.OrdinalIgnoreCase) && !queryText.StartsWith("DELETE ", StringComparison.OrdinalIgnoreCase) && !queryText.StartsWith("INSERT ", StringComparison.OrdinalIgnoreCase) ? queryText + PXDatabaseProviderBase.wrapWithCommentBrackets($"{this.formatUserAtScreen(user, screen)}, {PerformanceMonitorSqlSampleScope.BqlHash}") : queryText.Insert("SELECT ".Length, PXDatabaseProviderBase.wrapWithCommentBrackets($"{screen}, {PerformanceMonitorSqlSampleScope.BqlHash}")) + (user != null ? PXDatabaseProviderBase.wrapWithCommentBrackets(user) : (string) null);
  }

  internal bool IsIdentical(string f, string s, out string res)
  {
    int current1 = 0;
    PXDatabaseProviderBase.Paran paran1 = PXDatabaseProviderBase.Paran.GetParan(f, ref current1);
    paran1.Cleanup();
    int current2 = 0;
    PXDatabaseProviderBase.Paran paran2 = PXDatabaseProviderBase.Paran.GetParan(s, ref current2);
    paran2.Cleanup();
    f = paran1.ToString();
    s = paran2.ToString();
    bool flag1 = true;
    StringBuilder stringBuilder = new StringBuilder();
    int num1 = 0;
    int num2 = 0;
    while (num1 < f.Length)
    {
      for (; num1 < f.Length && "\n\r\t []`".IndexOf(f[num1]) >= 0; ++num1)
        stringBuilder.Append(f[num1]);
      while (num2 < s.Length && "\n\r\t []`".IndexOf(s[num2]) >= 0)
        ++num2;
      if (num1 < f.Length)
      {
        bool flag2 = (int) f[num1] == (int) s[num2];
        if (!flag2 && num2 > 1 && (s.Substring(num2, 6) == "dbo].[" || s.Substring(num2, 6) == "dbo`.`"))
        {
          num2 += 6;
          flag2 = (int) f[num1] == (int) s[num2];
        }
        if (!flag2 && s.Substring(num2, 8) == "BranchID" && (f.Substring(num1, 10) == "[BranchID]" || f.Substring(num1, 10) == "`BranchID`" || f.Substring(num1, 10) == "\"branchid\""))
        {
          num2 += 7;
          num1 += 9;
        }
        else
        {
          if (!flag2 && num1 > 0 && (f[num1 - 1] == '[' || f[num1 - 1] == '`'))
            flag2 = (int) char.ToLower(f[num1]) == (int) char.ToLower(s[num2]);
          if (flag2 || char.IsWhiteSpace(f[num1]) && char.IsWhiteSpace(s[num2]))
          {
            stringBuilder.Append(f[num1]);
          }
          else
          {
            stringBuilder.Append("<<<");
            flag1 = false;
            break;
          }
        }
        ++num1;
        ++num2;
      }
      else
        break;
    }
    res = stringBuilder.ToString();
    return flag1;
  }

  public override Decimal? SelectIdentity()
  {
    using (PXDataRecord pxDataRecord = this.readSingleRecord(this.emptyPars, "SELECT " + this._sqlDialect.getLastInsertedIdentity()))
      return (Decimal?) pxDataRecord?.GetDecimal(0);
  }

  public override string CreateParameterName(int ordinal) => "@P" + ordinal.ToString();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected static string antiInjection(string str)
  {
    if (!string.IsNullOrEmpty(str))
      str = str.Replace("*/", "* /").Replace("--", "- -");
    return str;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected static string wrapWithCommentBrackets(string commentText)
  {
    string str;
    if (commentText == null)
      str = (string) null;
    else
      str = commentText.Trim(' ', ',');
    return $"/* {str} */";
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected string formatUserAtScreen(string user, string screen)
  {
    return string.IsNullOrEmpty(user) ? screen : user + (object) '@' + screen;
  }

  protected string getUserForComment()
  {
    return PXDatabaseProviderBase.antiInjection(WebConfig.UserNameInQueryText ? PXContext.PXIdentity.IdentityName : (string) null);
  }

  protected string getScreenForComment()
  {
    return PXDatabaseProviderBase.antiInjection(PXContext.GetScreenID());
  }

  public override IEnumerable<PXDataRecord> SelectMulti(System.Type table, params PXDataField[] pars)
  {
    return this.SelectMulti(table, (IEnumerable<YaqlJoin>) null, pars);
  }

  public abstract SqlScripterBase getScripter();

  public override IEnumerable<PXDataRecord> SelectMulti(
    System.Type table,
    IEnumerable<YaqlJoin> joins = null,
    params PXDataField[] pars)
  {
    return (IEnumerable<PXDataRecord>) new PXSelectResult((PXDatabaseProvider) this, new Func<IDbCommand>(CommandFactory));

    IDbCommand CommandFactory()
    {
      bool flag = false;
      IDbCommand command = (IDbCommand) null;
      try
      {
        string name = table.Name;
        if (string.IsNullOrEmpty(name))
          throw new PXArgumentException("tableName", "The argument is out of range.");
        StringBuilder stringBuilder = new StringBuilder("SELECT ");
        if (!this.addFields(pars, stringBuilder, false))
          throw new InvalidOperationException("There are no columns to select");
        stringBuilder.Append(" FROM ").Append(name).Append(" ");
        this.appendJoins(stringBuilder, joins);
        this.appendWhereClauseToSelect(name, pars, stringBuilder);
        command = (IDbCommand) this.GetCommand();
        string query = stringBuilder.ToString();
        string queryText = this.SqlDialect.ApplyQueryHints(query, PXDatabaseProvider.DefaultQueryHints | this.SqlDialect.GetHintsNeededByQuery(query));
        command.CommandText = this.InsertUserAndScreenAsComment(queryText, this.getUserForComment(), this.getScreenForComment());
        TableHeader tableHeader = (TableHeader) null;
        for (int parameterIndex = 0; parameterIndex < pars.Length; ++parameterIndex)
        {
          if (pars[parameterIndex] is PXDataFieldValue par && par.ValueType != PXDbType.DirectExpression && par.Comp != PXComp.ISNULL && par.Comp != PXComp.ISNOTNULL)
            this._AddParameter(command, parameterIndex, this.getValueType(name, ref tableHeader, par), par.ValueLength, ParameterDirection.Input, par.Value, PXDatabaseProviderBase.ParameterBehavior.Compare);
        }
        flag = true;
        return command;
      }
      finally
      {
        if (!flag && command != null)
        {
          this.LeaveConnection(command.Connection);
          command.Dispose();
        }
      }
    }
  }

  public sealed override PXDataRecord SelectSingle(System.Type table, params PXDataField[] pars)
  {
    return this.SelectSingle(table, (IEnumerable<YaqlJoin>) null, pars);
  }

  public override PXDataRecord SelectSingle(
    System.Type table,
    IEnumerable<YaqlJoin> joins = null,
    params PXDataField[] pars)
  {
    string name = table.Name;
    if (string.IsNullOrEmpty(name))
      throw new PXArgumentException(nameof (table), "The argument is out of range.");
    StringBuilder stringBuilder = new StringBuilder("SELECT ");
    if (!this.addFields(pars, stringBuilder, true))
      return (PXDataRecord) null;
    stringBuilder.Append(" FROM ").Append(name).Append(" ");
    this.appendJoins(stringBuilder, joins);
    this.appendWhereClauseToSelect(name, pars, stringBuilder);
    PXDataField[] pars1 = pars;
    string commandText = stringBuilder.ToString();
    return this.readSingleRecord(pars1, commandText, this.schemaCache.GetTableHeader(name) ?? throw new PXException("The table with the name '{0}' does not exist in the database.", new object[1]
    {
      (object) name
    }));
  }

  internal sealed override IEnumerable<PXDataRecord> SelectMulti(
    Query query,
    params PXDataValue[] pars)
  {
    return (IEnumerable<PXDataRecord>) new PXSelectResult((PXDatabaseProvider) this, new Func<IDbCommand>(CommandFactory));

    IDbCommand CommandFactory()
    {
      bool flag = false;
      IDbCommand cmd = (IDbCommand) null;
      try
      {
        cmd = (IDbCommand) this.GetCommand();
        query.ApplyHints(PXDatabaseProvider.DefaultQueryHints);
        this.addSelectParameters(cmd, pars);
        query.InjectDirectExpressions(pars);
        cmd.CommandText = query.SQLQuery(this.SqlDialect.GetConnection()).ToString();
        cmd.CommandText = this.InsertUserAndScreenAsComment(cmd.CommandText, this.getUserForComment(), this.getScreenForComment());
        flag = true;
        return cmd;
      }
      finally
      {
        if (!flag && cmd != null)
        {
          this.LeaveConnection(cmd.Connection);
          cmd.Dispose();
        }
      }
    }
  }

  private void appendJoins(StringBuilder text, IEnumerable<YaqlJoin> joins)
  {
    if (joins == null)
      return;
    SqlScripterBase scripter = this.getScripter();
    foreach (YaqlJoin join in joins)
    {
      YaqlCondition condition = join.Condition;
      if (join.Source is YaqlSchemaTable source)
      {
        string restriction = this.getRestriction(source.Name, join.Source.Alias ?? source.Name, false);
        if (!string.IsNullOrWhiteSpace(restriction))
          join.Condition = Yaql.and(Yaql.parethesis(join.Condition), Yaql.rawCondition(restriction));
      }
      text.Append(join.toSql((CommandScripter) scripter, (SqlGenerationOptions) null));
      join.Condition = condition;
    }
  }

  protected PXDataRecord readSingleRecord(
    PXDataField[] pars,
    string commandText,
    TableHeader tableHeader = null)
  {
    bool flag = false;
    IDbCommand command = (IDbCommand) null;
    IDataReader reader = (IDataReader) null;
    try
    {
      command = (IDbCommand) this.GetCommand();
      command.CommandText = this.SqlDialect.ApplyQueryHints(commandText, PXDatabaseProvider.DefaultQueryHints | this.SqlDialect.GetHintsNeededByQuery(commandText));
      command.CommandText = this.InsertUserAndScreenAsComment(command.CommandText, this.getUserForComment(), this.getScreenForComment());
      for (int parameterIndex = 0; parameterIndex < pars.Length; ++parameterIndex)
      {
        if (pars[parameterIndex] is PXDataFieldValue par && par.ValueType != PXDbType.DirectExpression && par.Comp != PXComp.ISNULL && par.Comp != PXComp.ISNOTNULL)
          this._AddParameter(command, parameterIndex, this.getValueType(((TableEntityBase) tableHeader)?.Name, ref tableHeader, par), par.ValueLength, ParameterDirection.Input, par.Value, PXDatabaseProviderBase.ParameterBehavior.Compare);
      }
      if (command.Connection.State == ConnectionState.Closed)
        this.OpenConnection(command.Connection);
      reader = this.ExecuteReader(command, CommandBehavior.SingleRow);
      if (!(flag = reader.Read()))
        return (PXDataRecord) null;
    }
    finally
    {
      if (!flag)
      {
        reader?.Dispose();
        if (command != null)
        {
          this.LeaveConnection(command.Connection);
          command.Dispose();
        }
      }
    }
    return this.CreateRecord(reader, command);
  }

  protected PXDbType getValueType(
    string tableName,
    ref TableHeader tableHeader,
    PXDataFieldValue fieldValue)
  {
    return this.getValueType(tableName, ref tableHeader, fieldValue.ValueType, fieldValue.Value, fieldValue.Expression as Column);
  }

  protected PXDbType getValueType(
    string tableName,
    ref TableHeader tableHeader,
    PXDataFieldParam fieldParam)
  {
    return this.getValueType(tableName, ref tableHeader, fieldParam.ValueType, fieldParam.Value, fieldParam.Column);
  }

  protected PXDbType getValueType(
    string tableName,
    ref TableHeader tableHeader,
    PXDbType valueType,
    object value,
    Column column)
  {
    if (tableName != null && valueType == PXDbType.Unspecified && value is string && column != null)
    {
      if (tableHeader == null)
        tableHeader = this.schemaCache.GetTableHeader(tableName);
      TableColumn columnByName = tableHeader?.getColumnByName(column.Name);
      if (columnByName != null)
      {
        switch (columnByName.Type)
        {
          case SqlDbType.Char:
          case SqlDbType.NChar:
          case SqlDbType.NText:
          case SqlDbType.NVarChar:
          case SqlDbType.Text:
          case SqlDbType.VarChar:
            valueType = PXDbTypeConverter.SqlDbTypeToPXDbType(columnByName.Type);
            break;
        }
      }
    }
    return valueType;
  }

  protected void appendWhereClauseToSelect(string tableName, PXDataField[] pars, StringBuilder bld)
  {
    bool flag1 = false;
    bool flag2 = false;
    for (int i = 0; i < pars.Length; ++i)
    {
      if (!flag2 && pars[i] is PXDataFieldOrder)
        flag2 = true;
      if (pars[i] is PXDataFieldValue par && (par.ValueType != PXDbType.DirectExpression || par.Value is string))
      {
        string str = par.OrOperator ? " OR " : " AND ";
        string compareCondition = this.getCompareCondition(par.Comp, par.ValueType, par.Value as string, this.AlterFieldExpression(par.Expression), i);
        bld.Append(flag1 ? str : " WHERE (");
        if (par.OpenBrackets > 0)
          bld.Append('(', par.OpenBrackets);
        bld.Append(compareCondition);
        if (par.CloseBrackets > 0)
          bld.Append(')', par.CloseBrackets);
        flag1 = true;
      }
    }
    if (flag1)
      bld.Append(")");
    string restriction = this.SqlDialect.isRealTable(tableName) ? this.getRestriction(tableName, tableName, true) : (string) null;
    if (restriction != null)
      bld.Append(flag1 ? " AND " : " WHERE ").Append(restriction);
    if (!flag2)
      return;
    this.addOrderBy((IEnumerable<PXDataField>) pars, bld);
  }

  public override IDataReader ExecuteReader(IDbCommand command)
  {
    return this.ExecuteReader(command, CommandBehavior.Default);
  }

  protected IDataReader ExecuteReader(IDbCommand command, CommandBehavior behavior)
  {
    PXSqlLimits.BeforeExecuteReader(command);
    return PXSqlCache.IsEnabled ? PXSqlCache.CacheReader(command, (Func<IDataReader>) (() => this.ExecuteReaderInternal(command, behavior))) : this.ExecuteReaderInternal(command, behavior);
  }

  internal override Task<DbDataReader> ExecuteReaderAsync(
    DbCommand command,
    CancellationToken token)
  {
    return this.ExecuteReaderAsync(command, CommandBehavior.Default, token);
  }

  protected Task<DbDataReader> ExecuteReaderAsync(
    DbCommand command,
    CommandBehavior behavior,
    CancellationToken token)
  {
    PXSqlLimits.BeforeExecuteReader((IDbCommand) command);
    return PXSqlCache.IsEnabled ? PXSqlCache.CacheReaderAsync((IDbCommand) command, (Func<Task<DbDataReader>>) (() => this.ExecuteReaderInternalAsync(command, behavior, token))) : this.ExecuteReaderInternalAsync(command, behavior, token);
  }

  protected abstract IDataReader ExecuteReaderInternal(IDbCommand command, CommandBehavior behavior);

  protected abstract Task<DbDataReader> ExecuteReaderInternalAsync(
    DbCommand command,
    CommandBehavior behavior,
    CancellationToken token);

  protected void addOrderBy(IEnumerable<PXDataField> pars, StringBuilder bld)
  {
    bld.Append(" ORDER BY ");
    bool flag = true;
    foreach (PXDataFieldOrder pxDataFieldOrder in pars.OfType<PXDataFieldOrder>())
    {
      if (flag)
        flag = false;
      else
        bld.Append(", ");
      bld.Append((object) this.AlterFieldExpression(pxDataFieldOrder.Expression).SQLQuery(this._sqlDialect.GetConnection()));
      if (pxDataFieldOrder.IsDesc)
        bld.Append(" DESC");
    }
  }

  protected void addSelectParameters(IDbCommand cmd, PXDataValue[] pars)
  {
    for (int parameterIndex = 0; parameterIndex < pars.Length; ++parameterIndex)
    {
      if (pars[parameterIndex].ValueType != PXDbType.DirectExpression)
        this._AddParameter(cmd, parameterIndex, pars[parameterIndex].ValueType, pars[parameterIndex].ValueLength, ParameterDirection.Input, pars[parameterIndex].Value, PXDatabaseProviderBase.ParameterBehavior.Compare);
    }
  }

  protected string addSelectParameters(IDbCommand cmd, PXDataValue[] pars, string text)
  {
    StringBuilder stringBuilder = (StringBuilder) null;
    string placeholder = PXFieldName.Placeholder;
    int num1 = text.IndexOf(placeholder, StringComparison.OrdinalIgnoreCase);
    int num2 = 0;
    List<Tuple<int, string>> tupleList = (List<Tuple<int, string>>) null;
    for (int parameterIndex = 0; parameterIndex < pars.Length; ++parameterIndex)
    {
      if (pars[parameterIndex].ValueType != PXDbType.DirectExpression)
      {
        this._AddParameter(cmd, parameterIndex, pars[parameterIndex].ValueType, pars[parameterIndex].ValueLength, ParameterDirection.Input, pars[parameterIndex].Value, PXDatabaseProviderBase.ParameterBehavior.Compare);
      }
      else
      {
        if (stringBuilder == null)
          stringBuilder = new StringBuilder(text);
        if (pars[parameterIndex] is PXFieldName par)
        {
          string newValue = (string) par.Value;
          stringBuilder.Replace(placeholder, newValue, num1 + num2, placeholder.Length);
          num2 += newValue.Length - placeholder.Length;
          num1 = text.IndexOf(placeholder, num1 + placeholder.Length, StringComparison.OrdinalIgnoreCase);
        }
        else
        {
          if (tupleList == null)
            tupleList = new List<Tuple<int, string>>();
          tupleList.Add(Tuple.Create<int, string>(parameterIndex, this.SqlDialect.enquoteValue(pars[parameterIndex].Value, pars[parameterIndex].ValueType)));
        }
      }
    }
    for (int index = (tupleList == null ? 0 : tupleList.Count) - 1; index >= 0; --index)
    {
      Tuple<int, string> tuple = tupleList[index];
      string parameterName = this.CreateParameterName(tuple.Item1);
      stringBuilder.Replace(parameterName, tuple.Item2);
    }
    return stringBuilder != null ? stringBuilder.ToString() : text;
  }

  protected abstract string prepareTrace(PXDataValue[] pars, string text);

  private string enquoteTableNameDotColumn(string tableDotCol)
  {
    int length = tableDotCol.IndexOf('.');
    return length < 0 ? this._sqlDialect.quoteDbIdentifier(tableDotCol) : this._sqlDialect.quoteTableAndColumn(tableDotCol.Substring(0, length), tableDotCol.Substring(1 + length));
  }

  protected internal virtual StringBuilder CreateInsertBulder(string tableName)
  {
    return new StringBuilder("INSERT " + tableName);
  }

  public override bool Insert(System.Type table, params PXDataFieldAssign[] pars)
  {
    string name = table.Name;
    PXTransactionScope.EnsurePrimalOperation(PXDBOperation.Insert, table);
    if (this._RestrictedTables != null && this._RestrictedTables.Contains(name))
      throw new PXException("Modification of the {0} table is not allowed in this installation.", new object[1]
      {
        (object) name
      });
    if (pars.Length == 0)
      return false;
    companySetting settings;
    int companyId = this.getCompanyID(name, out settings);
    if (settings.TableNotFound)
      throw new PXException("The table schema of the {0} table was not found in the cache. The table is locked by another process. Please try again later.", new object[1]
      {
        (object) name
      });
    int sharedInsert = PXTransactionScope.GetSharedInsert();
    if (sharedInsert > 0)
      PXTransactionScope.ClearSharedInsert();
    bool sharedDelete = PXTransactionScope.GetSharedDelete();
    if (sharedDelete)
      PXTransactionScope.ClearSharedDelete();
    StringBuilder insertBulder = this.CreateInsertBulder(name);
    bool switchAllowed = false;
    string identityColumn = (string) null;
    this.appendInsertFieldNames(insertBulder, name, pars, companyId, settings, sharedInsert, sharedDelete, ref switchAllowed, out identityColumn);
    this.appendInsertFieldValues(insertBulder, name, pars, companyId, settings, sharedInsert, sharedDelete, ref switchAllowed, identityColumn);
    PXTransactionScope.TableModified(table);
    int num = this.insert(table, pars, insertBulder.ToString(), switchAllowed, settings) ? 1 : 0;
    if (num == 0)
      return num != 0;
    this.SendToNotificationQueue(table, (PXDataFieldParam[]) pars, PXDBOperation.Insert, companyId);
    this.SendToCommerceTransactionsQueue(table.Name);
    return num != 0;
  }

  protected bool insert(
    System.Type table,
    PXDataFieldAssign[] pars,
    string commandText,
    bool switchAllowed,
    companySetting setting)
  {
    string name = table.Name;
    IDbCommand command = (IDbCommand) null;
    try
    {
      command = (IDbCommand) this.GetCommand();
      command.CommandText = this.InsertUserAndScreenAsComment(commandText, this.getUserForComment(), this.getScreenForComment());
      PXDatabase.AuditTable audit = (this.auditDefinition == null ? 0 : (this.auditDefinition.AuditRequired(name) ? 1 : 0)) != 0 ? PXTransactionScope.GetAudit("I", name) : (PXDatabase.AuditTable) null;
      bool flag1 = false;
      for (int parameterIndex = 0; parameterIndex < pars.Length; ++parameterIndex)
      {
        if (pars[parameterIndex].Storage == StorageBehavior.KeyValueKey && pars[parameterIndex].Value is Guid)
          flag1 = true;
        if (pars[parameterIndex].ValueType != PXDbType.DirectExpression && pars[parameterIndex].Storage != StorageBehavior.KeyValueKey)
        {
          if (audit != null && pars[parameterIndex].IsChanged)
            audit.Fields.Add(new PXDatabase.AuditField(pars[parameterIndex].Column.Name, pars[parameterIndex].NewValue, false, pars[parameterIndex].Storage));
          if (pars[parameterIndex].Storage == StorageBehavior.Table)
            this._AddParameter(command, parameterIndex, pars[parameterIndex].ValueType, pars[parameterIndex].ValueLength, ParameterDirection.Input, pars[parameterIndex].Value, PXDatabaseProviderBase.ParameterBehavior.Assign);
        }
      }
      if (command.Connection.State == ConnectionState.Closed)
        this.OpenConnection(command.Connection);
      bool flag2;
      if (pars.Length != 0 && !((IEnumerable<PXDataFieldAssign>) pars).Any<PXDataFieldAssign>((System.Func<PXDataFieldAssign, bool>) (_ => _.Storage == StorageBehavior.Table)))
      {
        flag2 = true;
      }
      else
      {
        try
        {
          flag2 = this.ExecuteNonQuery(command) > 0;
        }
        catch (Exception ex)
        {
          Trace.WriteLine($"{table.Name} -> {ex.Message}");
          throw;
        }
      }
      if (flag2)
      {
        if (flag1)
        {
          Decimal? identity = this.SelectIdentity();
          this.SaveKeyValueStored(table.Name, PXDBOperation.Insert, (PXDataFieldParam[]) pars);
          PXTransactionScope.SetInsertedTable(table, identity);
        }
        else
          PXTransactionScope.SetInsertedTable(table);
      }
      if (!(flag2 & switchAllowed) || setting == null || setting.Flag != companySetting.companyFlag.Shared || !PXTransactionScope.SetVisibilityUpdate())
        return flag2;
      if (setting.Identity != null && !PXTransactionScope.SetIdentity((object) this.SelectIdentity()))
        return true;
      throw new PXVisibiltyUpdateRequiredException(name);
    }
    catch (DbException ex)
    {
      PXDatabaseException databaseException = this.newDatabaseException(name, (object[]) null, ex);
      if (switchAllowed && setting != null && setting.Deleted != null && databaseException.ErrorCode == PXDbExceptions.PrimaryKeyConstraintViolation)
      {
        PXTransactionScope.SetSharedDelete();
        throw new PXUpdateDeletedFlagRequiredException(name);
      }
      if (databaseException is PXDataWouldBeTruncatedException truncatedException)
        truncatedException.CommandText = command.CommandText;
      throw databaseException;
    }
    catch (TimeoutException ex)
    {
      throw new PXDatabaseException(name, (object[]) null, PXDbExceptions.Timeout, ex.Message);
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

  public abstract PXDatabaseException newDatabaseException(
    string tableName,
    object[] Keys,
    DbException dbex);

  protected void appendInsertFieldValues(
    StringBuilder bld,
    string tableName,
    PXDataFieldAssign[] pars,
    int cid,
    companySetting setting,
    int sharedInsert,
    bool sharedDelete,
    ref bool switchAllowed,
    string identityColumn)
  {
    bld.Append(" VALUES(");
    bool flag = false;
    for (int iParameter = 0; iParameter < pars.Length; ++iParameter)
    {
      if (pars[iParameter] == PXDataFieldAssign.OperationSwitchAllowed)
      {
        switchAllowed = true;
        if (pars.Length == 1 && (cid > 0 || companySetting.NeedRestrict(setting)))
          bld.Append(cid);
      }
      else
      {
        if (flag)
        {
          bld.Append(", ");
        }
        else
        {
          if (cid > 0 || companySetting.NeedRestrict(setting))
          {
            bld.Append(sharedInsert == 2 ? this.getMainCompanyOf(cid) : cid);
            bld.Append(", ");
            if (setting != null && setting.Flag == companySetting.companyFlag.Shared)
            {
              if (setting.Identity != null)
              {
                object identity = PXTransactionScope.GetIdentity();
                if (identity == null)
                {
                  bld.Append(this.SqlDialect.identCurrent(tableName)).Append(", ");
                }
                else
                {
                  bld.Append(this.SqlDialect.enquoteValue(identity) + ", ");
                  PXTransactionScope.ClearIdentity();
                }
              }
              if (sharedInsert == 1)
                bld.Append(this.SqlDialect.getCompanyMask(cid)).Append(", ");
            }
          }
          flag = true;
        }
        if (pars[iParameter].Column.Name != null && (pars[iParameter].Storage != StorageBehavior.Table || identityColumn != null && pars[iParameter].Column.Name.Equals(identityColumn, StringComparison.OrdinalIgnoreCase)))
          bld.Remove(bld.Length - 2, 2);
        else
          bld.Append(this.getDataFieldValue(pars[iParameter].ValueType, pars[iParameter].Value, iParameter));
      }
    }
    if (sharedDelete)
      bld.Append(", 1");
    if (setting != null && setting.WebAppType != null)
      bld.Append(", " + WebAppType.Current.AppTypeId.ToString());
    bld.Append(")");
  }

  protected virtual string QuoteTableAndColumnForInsert(string tableName, string columnName)
  {
    return this._sqlDialect.quoteTableAndColumn(tableName, columnName);
  }

  protected void appendInsertFieldNames(
    StringBuilder bld,
    string tableName,
    PXDataFieldAssign[] pars,
    int cid,
    companySetting setting,
    int sharedInsert,
    bool sharedDelete,
    ref bool switchAllowed,
    out string identityColumn)
  {
    bld.Append("(");
    identityColumn = (string) null;
    bool flag = false;
    foreach (PXDataFieldAssign par in pars)
    {
      if (par == PXDataFieldAssign.OperationSwitchAllowed)
      {
        switchAllowed = true;
        if (pars.Length == 1 && (cid > 0 || companySetting.NeedRestrict(setting)))
          bld.Append(this.QuoteTableAndColumnForInsert(tableName, "CompanyID"));
      }
      else
      {
        if (flag)
        {
          bld.Append(", ");
        }
        else
        {
          if (cid > 0 || companySetting.NeedRestrict(setting))
          {
            bld.Append(this.QuoteTableAndColumnForInsert(tableName, "CompanyID"));
            bld.Append(", ");
            if (setting != null && setting.Flag == companySetting.companyFlag.Shared)
            {
              if (setting.Identity != null)
              {
                bld.Append(this.QuoteTableAndColumnForInsert(tableName, setting.Identity)).Append(", ");
                identityColumn = setting.Identity;
              }
              if (sharedInsert == 1)
                bld.Append(this.QuoteTableAndColumnForInsert(tableName, "CompanyMask")).Append(", ");
            }
          }
          flag = true;
        }
        if (par.Column.Name != null && (par.Storage != StorageBehavior.Table || identityColumn != null && par.Column.Name.Equals(identityColumn, StringComparison.OrdinalIgnoreCase)))
          bld.Remove(bld.Length - 2, 2);
        else
          bld.Append((object) par.Column.SQLQuery(this.SqlDialect.GetConnection()));
      }
    }
    if (sharedDelete)
    {
      bld.Append(", ");
      bld.Append(setting.Deleted);
    }
    if (setting != null && setting.WebAppType != null)
    {
      bld.Append(", ");
      bld.Append(setting.WebAppType);
    }
    bld.Append(")");
  }

  protected void insertFieldValue(
    StringBuilder bld,
    string fieldName,
    string directExpression,
    bool timeStamp,
    PXComp comp,
    bool orOperator,
    int openBrackets,
    int closeBrackets,
    int offset,
    bool firstTime)
  {
    if (!firstTime)
      bld.Append(orOperator ? " OR " : " AND ");
    if (openBrackets > 0)
      bld.Append('(', openBrackets);
    PXDbType type = timeStamp ? PXDbType.Timestamp : (string.IsNullOrEmpty(directExpression) ? PXDbType.Unspecified : PXDbType.DirectExpression);
    bld.Append(this.getCompareCondition(comp, type, directExpression, fieldName, offset));
    if (closeBrackets <= 0)
      return;
    bld.Append(')', closeBrackets);
  }

  protected bool addFields(PXDataField[] pars, StringBuilder bld, bool isSelectSingle)
  {
    bool flag = false;
    foreach (PXDataField pxDataField in ((IEnumerable<PXDataField>) pars).Where<PXDataField>((System.Func<PXDataField, bool>) (t => !(t is PXDataFieldValue) && !(t is PXDataFieldOrder))))
    {
      if (flag)
        bld.Append(", ");
      else
        flag = true;
      SQLExpression sqlExpression = isSelectSingle ? this.AlterFieldExpression(pxDataField.Expression) : pxDataField.Expression;
      bld.Append(sqlExpression.SQLQuery(this._sqlDialect.GetConnection()).ToString());
    }
    return flag;
  }

  protected virtual T usingDbCommand<T>(
    Func<string, IDbCommand, T> fnUsefulActions,
    string commandText,
    string tableName)
  {
    IDbCommand dbCommand = (IDbCommand) null;
    try
    {
      dbCommand = (IDbCommand) this.GetCommand();
      dbCommand.CommandText = commandText;
      dbCommand.Parameters.Clear();
      return fnUsefulActions(tableName, dbCommand);
    }
    catch (DbException ex)
    {
      throw this.newDatabaseException(tableName, (object[]) null, ex);
    }
    catch (TimeoutException ex)
    {
      throw new PXDatabaseException(tableName, (object[]) null, PXDbExceptions.Timeout, ex.Message);
    }
    finally
    {
      if (dbCommand != null)
      {
        this.LeaveConnection(dbCommand.Connection);
        dbCommand.Dispose();
      }
    }
  }

  protected bool ensureCommon(IDbCommand cmd, PXDataFieldAssign[] values, PXDataField[] pars)
  {
    for (int parameterIndex = 0; parameterIndex < values.Length; ++parameterIndex)
    {
      if (values[parameterIndex].ValueType != PXDbType.DirectExpression || !(values[parameterIndex].Value is string))
        this._AddParameter(cmd, parameterIndex, values[parameterIndex].ValueType, values[parameterIndex].ValueLength, ParameterDirection.Input, values[parameterIndex].Value, PXDatabaseProviderBase.ParameterBehavior.Compare);
    }
    for (int index = 0; index < pars.Length; ++index)
    {
      if (pars[index] is PXDataFieldValue par && par.ValueType != PXDbType.DirectExpression && par.Comp != PXComp.ISNULL && par.Comp != PXComp.ISNOTNULL)
        this._AddParameter(cmd, values.Length + index, par.ValueType, par.ValueLength, ParameterDirection.Input, par.Value, PXDatabaseProviderBase.ParameterBehavior.Compare);
    }
    if (cmd.Connection.State == ConnectionState.Closed)
      this.OpenConnection(cmd.Connection);
    try
    {
      return this.ExecuteNonQuery(cmd) > 0;
    }
    catch (DbException ex)
    {
      Trace.WriteLine(ex.Message);
      throw;
    }
  }

  public override bool Ensure(System.Type table, PXDataFieldAssign[] values, PXDataField[] pars)
  {
    string name = table.Name;
    if (this._RestrictedTables != null && this._RestrictedTables.Contains(name))
      throw new PXException("Modification of the {0} table is not allowed in this installation.", new object[1]
      {
        (object) name
      });
    companySetting settings;
    int num1 = !string.IsNullOrEmpty(name) ? this.getCompanyID(name, out settings) : throw new PXArgumentException(nameof (table), "The argument is out of range.");
    if ((num1 > 0 || companySetting.NeedRestrict(settings)) && this.SqlDialect.isRealTable(name))
    {
      Array.Resize<PXDataField>(ref pars, pars.Length + 1);
      pars[pars.Length - 1] = (PXDataField) new PXDataFieldValue((SQLExpression) new Column("CompanyID", name), PXDbType.Int, new int?(4), (object) num1);
    }
    StringBuilder stringBuilder = new StringBuilder();
    if (values.Length != 0 && (num1 > 0 || companySetting.NeedRestrict(settings)))
      stringBuilder.Append("CompanyID, ");
    for (int index = 0; index < values.Length; ++index)
    {
      if (index != 0)
        stringBuilder.Append(", ");
      stringBuilder.Append((object) values[index].Column.SQLQuery(this.SqlDialect.GetConnection()));
    }
    stringBuilder.Insert(0, $"INSERT INTO {name} (");
    stringBuilder.Append(") ");
    stringBuilder.Append(this.buildSelectForEnsure(name, values, pars, num1, settings));
    PXTransactionScope.TableModified(table);
    int num2 = this.usingDbCommand<bool>((Func<string, IDbCommand, bool>) ((tn, cmd) => this.ensureCommon(cmd, values, pars)), stringBuilder.ToString(), name) ? 1 : 0;
    if (num2 == 0)
      return num2 != 0;
    this.SendToNotificationQueue(table, (PXDataFieldParam[]) values, PXDBOperation.Insert, num1);
    return num2 != 0;
  }

  protected string buildSelectForEnsure(
    string tableName,
    PXDataFieldAssign[] values,
    PXDataField[] pars,
    int cid,
    companySetting setting)
  {
    int offset = values.Length - ((IEnumerable<PXDataField>) pars).Count<PXDataField>((System.Func<PXDataField, bool>) (t => !(t is PXDataFieldValue) && !(t is PXDataFieldOrder)));
    StringBuilder bld1 = new StringBuilder();
    if (values.Length != 0 && (cid > 0 || companySetting.NeedRestrict(setting)))
      bld1.Append(cid).Append(", ");
    for (int index = 0; index < values.Length; ++index)
    {
      if (index > 0)
        bld1.Append(", ");
      bld1.Append((object) new Column(values[index].Column.Name, "s").SQLQuery(this.SqlDialect.GetConnection()));
    }
    bld1.Append(" FROM (");
    TableHeader tableHeader = this.schemaCache.GetTableHeader(tableName);
    bool flag1 = false;
    StringBuilder bld2 = new StringBuilder("SELECT ");
    for (int iParameter = 0; iParameter < values.Length; ++iParameter)
    {
      string dataFieldValue = this.getDataFieldValue(values[iParameter].ValueType, values[iParameter].Value, iParameter, tableHeader.getColumnByName(values[iParameter].Column.Name));
      string str = new Column(values[iParameter].Column.Name).SQLQuery(this.SqlDialect.GetConnection()).ToString();
      bld2.Append(dataFieldValue).Append(" AS ").Append(str).Append(", ");
    }
    bld2.Append("1 as ").Append(this.SqlDialect.quoteDbIdentifier("order by special field"));
    foreach (PXDataFieldOrder pxDataFieldOrder in pars.OfType<PXDataFieldOrder>())
    {
      string str = pxDataFieldOrder.Expression is Column expression ? expression.Name : pxDataFieldOrder.Expression.SQLQuery(this._sqlDialect.GetConnection()).ToString();
      bld2.Append(", null as " + this.SqlDialect.quoteDbIdentifier("order by " + str));
    }
    bld2.Append(" UNION ALL SELECT ");
    for (int iParameter = 0; iParameter < offset; ++iParameter)
      bld2.Append(this.getDataFieldValue(values[iParameter].ValueType, values[iParameter].Value, iParameter)).Append(", ");
    foreach (PXDataField pxDataField in ((IEnumerable<PXDataField>) pars).Where<PXDataField>((System.Func<PXDataField, bool>) (t => !(t is PXDataFieldValue) && !(t is PXDataFieldOrder))))
    {
      PXDataFieldAssign pxDataFieldAssign = values[offset];
      string directExpression = pxDataFieldAssign.ValueType != PXDbType.DirectExpression || string.IsNullOrEmpty(pxDataFieldAssign.Value as string) ? (string) null : (string) pxDataFieldAssign.Value;
      bld2.Append(this.getFieldAssignment(pxDataField.Expression, directExpression, pxDataFieldAssign.Behavior, offset, true));
      bld2.Append(", ");
      ++offset;
    }
    bld2.Append("0");
    foreach (PXDataFieldOrder pxDataFieldOrder in pars.OfType<PXDataFieldOrder>())
    {
      bld2.Append(", ");
      bld2.Append((object) pxDataFieldOrder.Expression.SQLQuery(this._sqlDialect.GetConnection()));
    }
    bld2.Append(" FROM ").Append(tableName);
    bool flag2 = false;
    for (int index = 0; index < pars.Length; ++index)
    {
      if (!(pars[index] is PXDataFieldOrder))
      {
        if (!(pars[index] is PXDataFieldValue par) || par.ValueType == PXDbType.DirectExpression && !(par.Value is string) || par.CheckResultOnly)
        {
          if (!flag1 && par != null)
            flag1 = par.CheckResultOnly;
        }
        else
        {
          if (!flag2)
            bld2.Append(" WHERE (");
          this.insertFieldValue(bld2, par.Expression.SQLQuery(this._sqlDialect.GetConnection()).ToString(), par.ValueType == PXDbType.DirectExpression ? par.Value as string : (string) null, par.ValueType == PXDbType.Timestamp, par.Comp, par.OrOperator, par.OpenBrackets, par.CloseBrackets, values.Length + index, !flag2);
          flag2 = true;
        }
      }
    }
    if (flag2)
      bld2.Append(")");
    string str1 = bld2.ToString();
    bld1.Append(str1);
    bld1.Append(") s ");
    bool flag3 = false;
    SQLExpression sqlExpression = (SQLExpression) null;
    foreach (PXDataField pxDataField in ((IEnumerable<PXDataField>) pars).Where<PXDataField>((System.Func<PXDataField, bool>) (t =>
    {
      switch (t)
      {
        case PXDataFieldOrder _:
          return true;
        case PXDataFieldValue _:
          return !((PXDataFieldValue) t).CheckResultOnly;
        default:
          return false;
      }
    })))
    {
      if (pxDataField.Expression is Column expression)
      {
        if (!flag3)
          flag3 = true;
        SQLExpression r = SQLExpressionExt.EQ(new Column(expression.Name, tableName), "CompanyID".Equals(expression.Name, StringComparison.OrdinalIgnoreCase) ? (SQLExpression) new SQLConst((object) cid) : (SQLExpression) new Column(expression.Name, "s"));
        sqlExpression = sqlExpression == null ? r : sqlExpression.And(r);
      }
    }
    if (flag3)
    {
      bld1.Append(this.SqlDialect.joinForEnsure(tableName));
      bld1.Append((object) sqlExpression.SQLQuery(this._sqlDialect.GetConnection()));
    }
    else
      bld1.Append("CROSS JOIN " + tableName);
    bld1.AppendFormat(" WHERE {0}", (object) new Column(values[0].Column.Name, tableName).IsNull().SQLQuery(this.SqlDialect.GetConnection()));
    bool flag4 = false;
    if (flag1)
    {
      for (int index = 0; index < pars.Length; ++index)
      {
        if (!(pars[index] is PXDataFieldOrder) && pars[index] is PXDataFieldValue par && (par.ValueType != PXDbType.DirectExpression || par.Value is string) && par.CheckResultOnly)
        {
          if (!flag4)
            bld1.Append(" AND (");
          this.insertFieldValue(bld1, "s." + par.Expression.SQLQuery(this._sqlDialect.GetConnection())?.ToString(), par.ValueType == PXDbType.DirectExpression ? par.Value as string : (string) null, par.ValueType == PXDbType.Timestamp, par.Comp, par.OrOperator, par.OpenBrackets, par.CloseBrackets, values.Length + index, !flag4);
          flag4 = true;
        }
      }
      if (flag4)
        bld1.Append(") ");
    }
    bld1.Append(" ORDER BY s.").Append(this.SqlDialect.quoteDbIdentifier("order by special field"));
    foreach (PXDataFieldOrder pxDataFieldOrder in pars.OfType<PXDataFieldOrder>())
    {
      bld1.Append(", s.");
      string str2 = pxDataFieldOrder.Expression is Column expression ? expression.Name : pxDataFieldOrder.Expression.SQLQuery(this._sqlDialect.GetConnection()).ToString();
      bld1.Append(this.SqlDialect.quoteDbIdentifier("order by " + str2));
      if (pxDataFieldOrder.IsDesc)
        bld1.Append(" DESC");
    }
    return this.SqlDialect.ApplyQueryHints(this.SqlDialect.limitRowsBeingSelected(bld1.ToString(), 1L), QueryHints.MySqlLockInShareMode);
  }

  private void SendToNotificationQueue(
    System.Type table,
    PXDataFieldParam[] parameters,
    PXDBOperation operation,
    int companyId)
  {
    PXTransactionScope.SendNotification(this.PrepareEvents(table, parameters, (PXDBOperation) ((int) PXTransactionScope.GetPrimalOperation(table) ?? (int) operation), companyId).ToArray<QueueEvent>(), table);
  }

  private IEnumerable<QueueEvent> PrepareEvents(
    System.Type table,
    PXDataFieldParam[] values,
    PXDBOperation operation,
    int companyId)
  {
    PXDatabaseProviderBase databaseProviderBase = this;
    yield return new QueueEvent(table.Name, (IEnumerable<PXDataFieldParam>) values, PXContext.GetScreenID(), operation, companyId);
    PXDataFieldParam pxDataFieldParam1 = ((IEnumerable<PXDataFieldParam>) values).FirstOrDefault<PXDataFieldParam>((System.Func<PXDataFieldParam, bool>) (_ => _.Storage == StorageBehavior.KeyValueKey && _.Value is Guid));
    if (pxDataFieldParam1?.Value is Guid)
    {
      Guid key = (Guid) pxDataFieldParam1.Value;
      string tableName = table.Name;
      PXDataFieldParam[] pxDataFieldParamArray = values;
      for (int index = 0; index < pxDataFieldParamArray.Length; ++index)
      {
        PXDataFieldParam pxDataFieldParam2 = pxDataFieldParamArray[index];
        Column column1 = pxDataFieldParam2.Column;
        Column column2 = column1;
        switch (pxDataFieldParam2.Storage)
        {
          case StorageBehavior.Table:
          case StorageBehavior.KeyValueKey:
            continue;
          case StorageBehavior.KeyValueNumeric:
            column2 = new Column("ValueNumeric");
            break;
          case StorageBehavior.KeyValueDate:
            column2 = new Column("ValueDate");
            break;
          case StorageBehavior.KeyValueString:
            column2 = new Column("ValueString");
            break;
          case StorageBehavior.KeyValueText:
            column2 = new Column("ValueText");
            break;
        }
        StorageBehavior storage = pxDataFieldParam2.Storage;
        pxDataFieldParam2.Storage = StorageBehavior.Table;
        PXDataFieldParam pxDataFieldParam3;
        switch (pxDataFieldParam2)
        {
          case PXDataFieldAssign pxDataFieldAssign:
            pxDataFieldParam3 = (PXDataFieldParam) pxDataFieldAssign.copyAndRename(column2);
            break;
          case PXDataFieldRestrict dataFieldRestrict:
            pxDataFieldParam3 = (PXDataFieldParam) dataFieldRestrict.copyAndRename(column2);
            break;
          default:
            continue;
        }
        pxDataFieldParam2.Storage = storage;
        yield return new QueueEvent(databaseProviderBase._sqlDialect.GetKvExtTableName(tableName), (IEnumerable<PXDataFieldParam>) new PXDataFieldParam[3]
        {
          pxDataFieldParam3,
          (PXDataFieldParam) new PXDataFieldAssign("RecordID", (object) key)
          {
            OldValue = (object) key,
            IsChanged = false
          },
          (PXDataFieldParam) new PXDataFieldAssign("FieldName", (object) column1.Name)
          {
            OldValue = (object) column1.Name,
            IsChanged = false
          }
        }, PXContext.GetScreenID(), (PXDBOperation) ((int) PXTransactionScope.GetPrimalOperation(table) ?? (int) operation), companyId);
      }
      pxDataFieldParamArray = (PXDataFieldParam[]) null;
    }
  }

  private void SendToCommerceTransactionsQueue(string tableName)
  {
    PXTransactionScope.RegisterCommerceTransaction(tableName);
  }

  public override bool Archive(System.Type table, params PXDataFieldRestrict[] pars)
  {
    string name = table.Name;
    PXTransactionScope.EnsurePrimalOperation(PXDBOperation.Update, table);
    if (this._RestrictedTables != null && this._RestrictedTables.Contains(name))
      throw new PXException("Modification of the {0} table is not allowed in this installation.", new object[1]
      {
        (object) name
      });
    PXDatabase.AuditTable audit = this.auditDefinition == null || !this.auditDefinition.AuditRequired(name) ? (PXDatabase.AuditTable) null : PXTransactionScope.GetAudit("A", name);
    return this.update(table, audit, ((IEnumerable<PXDataFieldParam>) pars).Concat<PXDataFieldParam>((IEnumerable<PXDataFieldParam>) new PXDataFieldParam[1]
    {
      (PXDataFieldParam) new PXDataFieldAssign("DatabaseRecordStatus", PXDbType.Int, (object) 1)
    }).ToArray<PXDataFieldParam>());
  }

  public override bool Extract(System.Type table, params PXDataFieldRestrict[] pars)
  {
    string name = table.Name;
    PXTransactionScope.EnsurePrimalOperation(PXDBOperation.Update, table);
    if (this._RestrictedTables != null && this._RestrictedTables.Contains(name))
      throw new PXException("Modification of the {0} table is not allowed in this installation.", new object[1]
      {
        (object) name
      });
    PXDatabase.AuditTable audit = this.auditDefinition == null || !this.auditDefinition.AuditRequired(name) ? (PXDatabase.AuditTable) null : PXTransactionScope.GetAudit("E", name);
    return this.update(table, audit, ((IEnumerable<PXDataFieldParam>) pars).Concat<PXDataFieldParam>((IEnumerable<PXDataFieldParam>) new PXDataFieldParam[1]
    {
      (PXDataFieldParam) new PXDataFieldAssign("DatabaseRecordStatus", PXDbType.Int, (object) 0)
    }).ToArray<PXDataFieldParam>());
  }

  public override bool Update(System.Type table, params PXDataFieldParam[] pars1)
  {
    string name = table.Name;
    PXTransactionScope.EnsurePrimalOperation(PXDBOperation.Update, table);
    if (this._RestrictedTables != null && this._RestrictedTables.Contains(name))
      throw new PXException("Modification of the {0} table is not allowed in this installation.", new object[1]
      {
        (object) name
      });
    PXDataFieldParam[] array = ((IEnumerable<PXDataFieldParam>) pars1).Where<PXDataFieldParam>((System.Func<PXDataFieldParam, bool>) (c => !(c is PXDummyDataFieldRestrict))).ToArray<PXDataFieldParam>();
    if (array.Length == 0)
    {
      if (PXTransactionScope.GetVisibilityUpdate())
        PXTransactionScope.ClearVisibilityUpdate();
      if (PXTransactionScope.GetSharedDelete())
        PXTransactionScope.ClearSharedDelete();
      return false;
    }
    PXDatabaseProviderBase.ExpressionParametersEvaluateManager parametersEvaluateManager = new PXDatabaseProviderBase.ExpressionParametersEvaluateManager((IEnumerable<PXDataFieldParam>) array, this.SqlDialect, this.schemaCache.GetTableHeader(table.Name), this.Logger);
    parametersEvaluateManager.TryEvaluateOnExistingParameters((IEnumerable<PXDataFieldParam>) array);
    bool selectAllNeeded = ((IEnumerable<PXDataFieldParam>) pars1).Any<PXDataFieldParam>((System.Func<PXDataFieldParam, bool>) (c => c == PXSelectOriginalsRestrict.SelectAllOriginalValues));
    bool flag1 = selectAllNeeded || ((IEnumerable<PXDataFieldParam>) pars1).Any<PXDataFieldParam>((System.Func<PXDataFieldParam, bool>) (c => c == PXSelectOriginalsRestrict.SelectOriginalValues)) || parametersEvaluateManager.NeedEvaluate;
    PXDataFieldParam[][] selectedRows = flag1 ? this.SelectOriginalValues(table, array, selectAllNeeded).ToArray<PXDataFieldParam[]>() : PXDatabaseProviderBase._emptySelectedOriginalRows;
    parametersEvaluateManager.TryEvaluateOnSelectedValues(selectedRows);
    bool flag2 = this.update(table, (PXDatabase.AuditTable) null, array);
    if (flag2)
    {
      int companyId = this.getCompanyID(name, out companySetting _);
      if (flag1 && selectedRows.Length != 0)
      {
        if (!parametersEvaluateManager.TryEvaluateOnNewRowsSelected(selectedRows, array, (System.Func<PXDataField[], IEnumerable<PXDataRecord>>) (pars2 => this.SelectMulti(table, pars2))))
        {
          this.Logger.Warning<string, string>("The following expression parameters could not be evaluate during the update operation on {TableName} table: {Parameters}. The transaction will not be added to the notification queue.", name, Str.Join((IEnumerable<string>) parametersEvaluateManager.GetEvaluatedParameterNames(), ", "));
          return flag2;
        }
        foreach (PXDataFieldParam[] parameters in selectedRows)
          this.SendToNotificationQueue(table, parameters, PXDBOperation.Update, companyId);
      }
      else
        this.SendToNotificationQueue(table, pars1, PXDBOperation.Update, companyId);
    }
    return flag2;
  }

  private IEnumerable<PXDataFieldParam[]> SelectOriginalValues(
    System.Type table,
    string tableAlias,
    PXDataFieldParam[] realParameters,
    bool selectAllNeeded,
    Func<IEnumerable<PXDataRecord>> selectRows,
    IEnumerable<(Column setColumn, string valueColumnName)> dependentSetColumns)
  {
    PXDatabaseProviderBase databaseProviderBase = this;
    if (PXTransactionScope.IsPushNotificationSetupForTable(table.Name))
    {
      TableHeader tableHeader = databaseProviderBase.schemaCache.GetTableHeader(table.Name);
      ColumnNameComparer columnNameComparer = new ColumnNameComparer(table.Name, databaseProviderBase.SqlDialect);
      Dictionary<string, PXDataFieldParam> setParametersBase = ((IEnumerable<PXDataFieldParam>) realParameters).Select<PXDataFieldParam, Tuple<PXDataFieldParam, string>>((System.Func<PXDataFieldParam, Tuple<PXDataFieldParam, string>>) (c => new Tuple<PXDataFieldParam, string>(c, c.Column.SQLQuery(this.SqlDialect.GetConnection()).ToString()))).GroupBy<Tuple<PXDataFieldParam, string>, string>((System.Func<Tuple<PXDataFieldParam, string>, string>) (t => t.Item2), (IEqualityComparer<string>) columnNameComparer).ToDictionary<IGrouping<string, Tuple<PXDataFieldParam, string>>, string, PXDataFieldParam>((System.Func<IGrouping<string, Tuple<PXDataFieldParam, string>>, string>) (t => t.Key), (System.Func<IGrouping<string, Tuple<PXDataFieldParam, string>>, PXDataFieldParam>) (t => t.Select<Tuple<PXDataFieldParam, string>, PXDataFieldParam>((System.Func<Tuple<PXDataFieldParam, string>, PXDataFieldParam>) (c => c.Item1)).FirstOrDefault<PXDataFieldParam>((System.Func<PXDataFieldParam, bool>) (c => c is PXDataFieldAssign)) ?? t.Select<Tuple<PXDataFieldParam, string>, PXDataFieldParam>((System.Func<Tuple<PXDataFieldParam, string>, PXDataFieldParam>) (c => c.Item1)).FirstOrDefault<PXDataFieldParam>()), (IEqualityComparer<string>) columnNameComparer);
      IEnumerable<string> additionalSelect = PXTransactionScope.GetColumnsForAdditionalSelect(table.Name, tableHeader.Columns.Select<TableColumn, string>((System.Func<TableColumn, string>) (c => ((TableEntityBase) c).Name)));
      if (selectAllNeeded || additionalSelect.Any<string>((System.Func<string, bool>) (c => !this.TryGetParameterFromSetParameters(setParametersBase, tableAlias, c, out PXDataFieldParam _))))
      {
        IEnumerable<PXDataRecord> pxDataRecords = selectRows();
        PXDataFieldParam[] nonTableParameters = ((IEnumerable<PXDataFieldParam>) realParameters).Where<PXDataFieldParam>((System.Func<PXDataFieldParam, bool>) (c => c.Storage != 0)).ToArray<PXDataFieldParam>();
        foreach (PXDataRecord pxDataRecord in pxDataRecords)
        {
          PXDataRecord row = pxDataRecord;
          Dictionary<string, PXDataFieldParam> setParameters = new Dictionary<string, PXDataFieldParam>((IDictionary<string, PXDataFieldParam>) setParametersBase, (IEqualityComparer<string>) columnNameComparer);
          Dictionary<string, object> dictionary = tableHeader.Columns.Select<TableColumn, string>((System.Func<TableColumn, string>) (c => ((TableEntityBase) c).Name)).Concat<string>(dependentSetColumns.Select<(Column, string), string>((System.Func<(Column, string), string>) (d => d.valueColumnName))).ToHashSet<string>().ToDictionary<string, string, object>((System.Func<string, string>) (c => c), (System.Func<string, object>) (c => row[c]), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
          foreach ((Column column, string key1) in dependentSetColumns)
          {
            object obj = dictionary[key1];
            PXDataFieldAssign pxDataFieldAssign = new PXDataFieldAssign(column, obj);
            string key2 = column.SQLQuery(databaseProviderBase.SqlDialect.GetConnection()).ToString();
            setParameters.Add(key2, (PXDataFieldParam) pxDataFieldAssign);
          }
          List<PXDataFieldParam> pxDataFieldParamList = new List<PXDataFieldParam>((IEnumerable<PXDataFieldParam>) nonTableParameters);
          foreach (TableColumn column in tableHeader.Columns)
          {
            PXDataFieldParam pxDataFieldParam;
            PXDataFieldAssign pxDataFieldAssign1 = databaseProviderBase.TryGetParameterFromSetParameters(setParameters, tableAlias, ((TableEntityBase) column).Name, out pxDataFieldParam) ? pxDataFieldParam as PXDataFieldAssign : (PXDataFieldAssign) null;
            if (pxDataFieldAssign1 != null)
            {
              PXDataFieldAssign pxDataFieldAssign2 = new PXDataFieldAssign(pxDataFieldAssign1.Column, pxDataFieldAssign1.ValueType, pxDataFieldAssign1.ValueLength, pxDataFieldAssign1.Value)
              {
                OldValue = dictionary[((TableEntityBase) column).Name],
                IsChanged = !object.Equals(pxDataFieldAssign1.OldValue, pxDataFieldAssign1.Value),
                Behavior = pxDataFieldAssign1.Behavior
              };
              pxDataFieldParamList.Add((PXDataFieldParam) pxDataFieldAssign2);
            }
            else
            {
              PXDbType pxDbType = PXDbTypeConverter.SqlDbTypeToPXDbType(column.Type);
              PXDummyDataFieldRestrict dataFieldRestrict = new PXDummyDataFieldRestrict(((TableEntityBase) column).Name, pxDbType, new int?(column.Size), dictionary[((TableEntityBase) column).Name]);
              pxDataFieldParamList.Add((PXDataFieldParam) dataFieldRestrict);
            }
          }
          yield return pxDataFieldParamList.ToArray();
        }
      }
    }
  }

  private IEnumerable<PXDataFieldParam[]> SelectOriginalValues(
    System.Type table,
    PXDataFieldParam[] realParameters,
    Query selectQuery,
    PXDataValue[] selectQueryParameters,
    IEnumerable<(Column setColumn, string valueColumnName)> dependentSetColumns)
  {
    List<SQLExpression> selection = selectQuery.GetSelection();
    Column[] array = selection.OfType<Column>().ToArray<Column>();
    string tableAlias = array[0].Table().AliasOrName();
    HashSet<string> selectionColumnNames = ((IEnumerable<Column>) array).Select<Column, string>((System.Func<Column, string>) (c => c.Name)).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    selection.AddRange((IEnumerable<SQLExpression>) this.schemaCache.GetTableHeader(table.Name).Columns.Where<TableColumn>((System.Func<TableColumn, bool>) (c => !selectionColumnNames.Contains(((TableEntityBase) c).Name))).Select<TableColumn, Column>((System.Func<TableColumn, Column>) (c => new Column(((TableEntityBase) c).Name, tableAlias))));
    Func<IEnumerable<PXDataRecord>> selectRows = (Func<IEnumerable<PXDataRecord>>) (() => this.SelectMulti(selectQuery, selectQueryParameters));
    return this.SelectOriginalValues(table, tableAlias, realParameters, true, selectRows, dependentSetColumns);
  }

  private IEnumerable<PXDataFieldParam[]> SelectOriginalValues(
    System.Type table,
    PXDataFieldParam[] realParameters,
    bool selectAllNeeded)
  {
    PXDataField[] selectParameters = this.schemaCache.GetTableHeader(table.Name).Columns.Select<TableColumn, PXDataField>((System.Func<TableColumn, PXDataField>) (c => new PXDataField(((TableEntityBase) c).Name))).Concat<PXDataField>(realParameters.OfType<PXDataFieldRestrict>().Where<PXDataFieldRestrict>((System.Func<PXDataFieldRestrict, bool>) (c => c != PXDataFieldRestrict.OperationSwitchAllowed)).Select<PXDataFieldRestrict, PXDataFieldValue>((System.Func<PXDataFieldRestrict, PXDataFieldValue>) (c => new PXDataFieldValue((SQLExpression) c.Column, c.ValueType, c.ValueLength, c.Value, c.Comp)
    {
      CloseBrackets = c.CloseBrackets,
      OpenBrackets = c.OpenBrackets,
      OrOperator = c.OrOperator,
      CheckResultOnly = c.CheckResultOnly
    })).OfType<PXDataField>()).ToArray<PXDataField>();
    Func<IEnumerable<PXDataRecord>> selectRows = (Func<IEnumerable<PXDataRecord>>) (() => this.SelectMulti(table, selectParameters));
    return this.SelectOriginalValues(table, table.Name, realParameters, selectAllNeeded, selectRows, Enumerable.Empty<(Column, string)>());
  }

  private bool TryGetParameterFromSetParameters(
    Dictionary<string, PXDataFieldParam> setParameters,
    string table,
    string column,
    out PXDataFieldParam param)
  {
    param = (PXDataFieldParam) null;
    return !(column == "tstamp") && (setParameters.TryGetValue(column, out param) || setParameters.TryGetValue(this.SqlDialect.quoteDbIdentifier(column), out param) || setParameters.TryGetValue(this.SqlDialect.quoteTableAndColumn(table, column), out param));
  }

  public override int Update(PXGraph graph, IBqlUpdate command, params PXDataValue[] pars)
  {
    System.Type setTable = command.GetSetTable(true, true);
    PXDataFieldParam[] assignmentParameters = command.GetFieldAssignmentParameters(graph, pars);
    Query selectQuery = command.GetSelectQuery(graph);
    IEnumerable<(Column, string)> dependentSetColumns = command.GetDependentSetColumns(graph);
    PXDataFieldParam[][] array = this.SelectOriginalValues(setTable, assignmentParameters, selectQuery, pars, dependentSetColumns).ToArray<PXDataFieldParam[]>();
    PXTransactionScope.TableModified(setTable);
    string name = setTable.Name;
    int num = this.usingDbCommand<int>((Func<string, IDbCommand, int>) ((tn, cmd) => this.updateGraphCommon(cmd, tn, pars, command.GetText(graph), command)), string.Empty, name);
    if (num > 0)
    {
      int companyId = this.getCompanyID(name, out companySetting _);
      foreach (PXDataFieldParam[] parameters in array)
        this.SendToNotificationQueue(setTable, parameters, PXDBOperation.Update, companyId);
    }
    return num;
  }

  internal int updateGraphCommon(
    IDbCommand cmd,
    string tableName,
    PXDataValue[] pars,
    string text,
    IBqlUpdate command)
  {
    string str = this.addSelectParameters(cmd, pars, text);
    cmd.CommandText = str;
    if (cmd.Connection.State == ConnectionState.Closed)
      this.OpenConnection(cmd.Connection);
    if (cmd.CommandTimeout > 0 && cmd.CommandTimeout < 60)
      cmd.CommandTimeout = 300;
    return this.ExecuteNonQuery(cmd);
  }

  internal bool update(System.Type table, PXDatabase.AuditTable audit, params PXDataFieldParam[] pars)
  {
    string tableName = table.Name;
    if (audit == null)
      audit = this.auditDefinition == null || !this.auditDefinition.AuditRequired(tableName) ? (PXDatabase.AuditTable) null : PXTransactionScope.GetAudit("U", tableName);
    companySetting setting;
    int cid = this.getCompanyID(tableName, out setting);
    if (setting.TableNotFound)
      throw new PXException("The table schema of the {0} table was not found in the cache. The table is locked by another process. Please try again later.", new object[1]
      {
        (object) tableName
      });
    bool visibilityUpdate = PXTransactionScope.GetVisibilityUpdate();
    if (visibilityUpdate)
      PXTransactionScope.ClearVisibilityUpdate();
    else
      PXTransactionScope.TableModified(table);
    bool sharedDelete = PXTransactionScope.GetSharedDelete();
    if (sharedDelete)
      PXTransactionScope.ClearSharedDelete();
    if (sharedDelete & visibilityUpdate)
    {
      if (!this.usingDbCommand<bool>((Func<string, IDbCommand, bool>) ((tn, cmd) => this.updateCommon(cmd, tableName, cid, pars, audit, visibilityUpdate, false, setting)), string.Empty, tableName))
        return false;
      visibilityUpdate = false;
    }
    return this.usingDbCommand<bool>((Func<string, IDbCommand, bool>) ((tn, cmd) => this.updateCommon(cmd, tableName, cid, pars, audit, visibilityUpdate, sharedDelete, setting)), string.Empty, tableName);
  }

  internal bool updateCommon(
    IDbCommand cmd,
    string tableName,
    int cid,
    PXDataFieldParam[] pars,
    PXDatabase.AuditTable audit,
    bool isVisibilityUpdate,
    bool sharedDelete,
    companySetting setting)
  {
    StringBuilder bld1 = new StringBuilder();
    bool flag1 = false;
    if (string.IsNullOrEmpty(cmd.CommandText))
    {
      StringBuilder bld2 = new StringBuilder("UPDATE ");
      if (WebConfig.InQueryInfoComment && PerformanceMonitorSqlSampleScope.BqlHash != null)
        bld2.Append(PXDatabaseProviderBase.wrapWithCommentBrackets($"{this.getScreenForComment()}, {PerformanceMonitorSqlSampleScope.BqlHash}"));
      bld2.Append(tableName);
      bld2.Append(" SET ");
      if (isVisibilityUpdate)
      {
        if (setting.Flag == companySetting.companyFlag.Separate)
          return true;
        this.appendUpdateVisibility(bld2, cid, pars.Length);
      }
      else if (!this.appendUpdateAssignedFields(bld2, cid, pars, sharedDelete, setting))
        return true;
      object identity;
      if (setting != null && setting.Identity != null && (identity = PXTransactionScope.GetIdentity()) != null)
      {
        PXTransactionScope.ClearIdentity();
        foreach (PXDataFieldParam par in pars)
        {
          if (string.Equals(par.Column.Name, setting.Identity, StringComparison.OrdinalIgnoreCase))
          {
            par.Value = identity;
            break;
          }
        }
      }
      flag1 = this.appendUpdateRestrictions(pars, bld1, isVisibilityUpdate, sharedDelete, cid, setting);
      bld2.Append((object) bld1);
      if (WebConfig.InQueryInfoComment && PerformanceMonitorSqlSampleScope.BqlHash != null)
        bld2.Append(PXDatabaseProviderBase.wrapWithCommentBrackets(this.getUserForComment()));
      else
        bld2.Append(PXDatabaseProviderBase.wrapWithCommentBrackets($"{this.formatUserAtScreen(this.getUserForComment(), this.getScreenForComment())}, {PerformanceMonitorSqlSampleScope.BqlHash}"));
      cmd.CommandText = bld2.ToString();
    }
    else
      cmd.Parameters.Clear();
    TableHeader tableHeader = (TableHeader) null;
    for (int parameterIndex = 0; parameterIndex < pars.Length; ++parameterIndex)
    {
      PXDataFieldParam par = pars[parameterIndex];
      int num;
      switch (par)
      {
        case PXDataFieldRestrict _:
          num = 1;
          break;
        case PXDataFieldAssign _:
          num = 2;
          break;
        default:
          num = 0;
          break;
      }
      PXDatabaseProviderBase.ParameterBehavior behavior = (PXDatabaseProviderBase.ParameterBehavior) num;
      if (par.ValueType != PXDbType.DirectExpression && par != PXDataFieldRestrict.OperationSwitchAllowed && par.Storage != StorageBehavior.KeyValueKey && (cid <= 0 && !companySetting.NeedRestrict(setting) || string.Compare(par.Column.Name, "CompanyID", StringComparison.OrdinalIgnoreCase) != 0) && (!(par is PXDataFieldRestrict) || ((PXDataFieldRestrict) par).Comp != PXComp.ISNULL && ((PXDataFieldRestrict) par).Comp != PXComp.ISNOTNULL) && (!isVisibilityUpdate || !(par is PXDataFieldAssign)))
      {
        if (par.Storage == StorageBehavior.Table)
          this._AddParameter(cmd, parameterIndex, this.getValueType(tableName, ref tableHeader, par), par.ValueLength, ParameterDirection.Input, par.Value, behavior);
        if (!(audit == null | isVisibilityUpdate))
        {
          string name = par.Column.Name;
          PXDataFieldAssign pxDataFieldAssign = par as PXDataFieldAssign;
          if (par is PXDataFieldRestrict && par.Value != null)
            audit.Fields.Add(new PXDatabase.AuditField(name, par.Value.ToString(), true, par.Storage));
          else if (pxDataFieldAssign != null && pxDataFieldAssign.IsChanged)
            audit.Fields.Add(new PXDatabase.AuditField(name, pxDataFieldAssign.NewValue, false, pxDataFieldAssign.Storage));
        }
      }
    }
    bool wasVisible = false;
    string querySelectVisibility = $"SELECT {this.SqlDialect.caseWhenThenElse(this.SqlDialect.binaryMaskTest("CompanyMask", cid, 3), "1", "0")} FROM {tableName}{bld1?.ToString()}";
    if (cmd.Connection.State == ConnectionState.Closed)
      this.OpenConnection(cmd.Connection);
    int num1 = !isVisibilityUpdate || cid == this.getMainCompanyOf(cid) ? this.ExecuteNonQuery(cmd) : this.executeVisibilityUpdate(cmd, querySelectVisibility, pars.Length, out wasVisible);
    if (sharedDelete && num1 == 0)
      throw new PXDatabaseException(tableName, (object[]) null, PXDbExceptions.PrimaryKeyConstraintViolation, $"Violation of PRIMARY KEY constraint '{tableName}_PK'. Cannot insert duplicate key in object 'dbo.{tableName}'", (Exception) null);
    if (isVisibilityUpdate)
    {
      if (num1 == 1 & wasVisible)
      {
        cmd.CommandText = $"{cmd.CommandText} AND CompanyID = {cid.ToString()}";
        if (cmd.Connection.State == ConnectionState.Closed)
          this.OpenConnection(cmd.Connection);
        this.ExecuteNonQuery(cmd);
      }
      return true;
    }
    bool flag2 = num1 > 0;
    if (flag2)
      this.SaveKeyValueStored(tableName, PXDBOperation.Update, pars);
    if (flag2 || setting == null || !flag1)
      return flag2;
    int[] source = new int[0];
    int[] selectables1;
    if (this.tryGetSelectableCompanies(cid, out selectables1) && selectables1 != null)
      source = ((IEnumerable<int>) selectables1).Where<int>(new System.Func<int, bool>(this.templateCompanies.Contains)).ToArray<int>();
    if (((IEnumerable<int>) source).Any<int>() && setting.Flag != companySetting.companyFlag.Separate)
    {
      int cid1 = ((IEnumerable<int>) source).Last<int>();
      StringBuilder bld3 = new StringBuilder();
      string restriction = this.getRestriction(tableName, tableName, false, effectiveCid: new int?(cid1));
      bld3.AppendFormat("SELECT 1 FROM {0} WHERE {1}", (object) tableName, (object) restriction);
      if (!string.IsNullOrEmpty(restriction))
        bld3.Append(" AND ");
      bld3.Append(this._sqlDialect.binaryMaskTest("CompanyMask", cid, 3));
      cmd.Parameters.Clear();
      cmd.CommandText = this.BuildWhereClause(cmd, cid1, pars, setting, bld3);
      if (cmd.Connection.State == ConnectionState.Closed)
        this.OpenConnection(cmd.Connection);
      bool flag3 = false;
      using (IDataReader dataReader = this.ExecuteReader(cmd, CommandBehavior.Default))
      {
        if (dataReader.Read())
          flag3 = true;
      }
      if (flag3)
      {
        cmd.Parameters.Clear();
        cmd.CommandText = (string) null;
        return this.updateCommon(cmd, tableName, cid1, pars, audit, isVisibilityUpdate, sharedDelete, setting);
      }
    }
    if (setting.Flag != companySetting.companyFlag.Shared)
      return flag2;
    bool flag4 = false;
    if (setting.Identity != null)
    {
      flag4 = true;
      foreach (PXDataFieldParam par in pars)
      {
        if (par is PXDataFieldRestrict && string.Equals(par.Column.Name, setting.Identity, StringComparison.OrdinalIgnoreCase))
        {
          flag4 = !PXTransactionScope.SetIdentity(par.Value);
          break;
        }
      }
    }
    if (!flag4)
    {
      string updateables;
      if (this.tryGetUpdateableCompanies(cid, out updateables))
      {
        this.alterCommandForSharedRecords(cmd, cid, updateables);
        flag2 = this.ExecuteNonQuery(cmd) > 0;
      }
      int[] selectables2;
      if (!flag2 && this.tryGetSelectableCompanies(cid, out selectables2))
      {
        string listCompanies = string.Join<int>(", ", (IEnumerable<int>) selectables2);
        StringBuilder bld4 = new StringBuilder();
        bld4.AppendFormat("UPDATE {0} SET ", (object) tableName);
        bld4.AppendFormat("{0} = {1} WHERE CompanyID IN({2})", (object) "CompanyMask", (object) this.SqlDialect.binaryMaskSub("CompanyMask", cid, 2), (object) listCompanies);
        cmd.Parameters.Clear();
        cmd.CommandText = this.BuildWhereClause(cmd, cid, pars, setting, bld4);
        if (PXTransactionScope.SetSharedInsert(1) <= 0)
          return false;
        bool flag5 = this.ExecuteNonQuery(cmd) > 0;
        if (!flag5)
        {
          PXTransactionScope.ClearIdentity();
          PXTransactionScope.ClearSharedInsert();
          return flag5;
        }
        int[] siblings;
        if (this.getMainCompanyOf(cid) != cid && this.tryGetSiblings(cid, out siblings))
          this.updateSiblingCompanies(tableName, pars, listCompanies, cmd, cid, siblings);
        throw new PXInsertSharedRecordRequiredException(tableName);
      }
      PXTransactionScope.ClearIdentity();
    }
    return flag2;
  }

  private string BuildWhereClause(
    IDbCommand cmd,
    int cid,
    PXDataFieldParam[] pars,
    companySetting setting,
    StringBuilder bld)
  {
    bool flag = false;
    for (int i = 0; i < pars.Length; ++i)
    {
      if (pars[i] is PXDataFieldRestrict par && par != PXDataFieldRestrict.OperationSwitchAllowed && par.Storage == StorageBehavior.Table && (par.ValueType != PXDbType.DirectExpression || par.Value is string))
      {
        bld.Append(par.OrOperator & flag ? " OR " : " AND ");
        if (!flag)
          bld.Append("(");
        if (par.OpenBrackets > 0)
          bld.Append('(', par.OpenBrackets);
        bld.Append(this.getCompareCondition(par.Comp, par.ValueType, par.Value as string, par.Column, i));
        if (par.CloseBrackets > 0)
          bld.Append(')', par.CloseBrackets);
        flag = true;
      }
    }
    if (flag)
      bld.Append(')');
    for (int parameterIndex = 0; parameterIndex < pars.Length; ++parameterIndex)
    {
      if (pars[parameterIndex].ValueType != PXDbType.DirectExpression && pars[parameterIndex] != PXDataFieldRestrict.OperationSwitchAllowed && pars[parameterIndex].Storage == StorageBehavior.Table && (cid <= 0 && !companySetting.NeedRestrict(setting) || string.Compare(pars[parameterIndex].Column.Name, "CompanyID", StringComparison.OrdinalIgnoreCase) != 0) && pars[parameterIndex] is PXDataFieldRestrict && ((PXDataFieldRestrict) pars[parameterIndex]).Comp != PXComp.ISNULL && ((PXDataFieldRestrict) pars[parameterIndex]).Comp != PXComp.ISNOTNULL)
        this._AddParameter(cmd, parameterIndex, pars[parameterIndex].ValueType, pars[parameterIndex].ValueLength, ParameterDirection.Input, pars[parameterIndex].Value, PXDatabaseProviderBase.ParameterBehavior.Compare);
    }
    return bld.ToString();
  }

  public virtual int executeVisibilityUpdate(
    IDbCommand cmd,
    string querySelectVisibility,
    int nextParamIndex,
    out bool wasVisible)
  {
    string commandText = cmd.CommandText;
    cmd.CommandText = querySelectVisibility;
    DbAdapterBase.CorrectCommand(cmd);
    object obj = cmd.ExecuteScalar();
    wasVisible = obj.ToString() != "0";
    cmd.CommandText = commandText;
    DbAdapterBase.CorrectCommand(cmd);
    return cmd.ExecuteNonQuery();
  }

  internal override void Truncate(System.Type table)
  {
    string name = table.Name;
    if (this._RestrictedTables != null && this._RestrictedTables.Contains(name))
      throw new PXException("Modification of the {0} table is not allowed in this installation.", new object[1]
      {
        (object) name
      });
    this.usingDbCommand<bool>((Func<string, IDbCommand, bool>) ((q, cmd) =>
    {
      if (cmd.Connection.State == ConnectionState.Closed)
        this.OpenConnection(cmd.Connection);
      return cmd.ExecuteNonQuery() > 0;
    }), "TRUNCATE TABLE " + this.SqlDialect.quoteDbIdentifier(name), name);
  }

  public override bool Delete(System.Type table, params PXDataFieldRestrict[] pars)
  {
    PXTransactionScope.EnsurePrimalOperation(PXDBOperation.Delete, table);
    PXDataFieldRestrict[] array = ((IEnumerable<PXDataFieldRestrict>) pars).Where<PXDataFieldRestrict>((System.Func<PXDataFieldRestrict, bool>) (c => !(c is PXDummyDataFieldRestrict))).ToArray<PXDataFieldRestrict>();
    bool selectAllNeeded = ((IEnumerable<PXDataFieldRestrict>) pars).Any<PXDataFieldRestrict>((System.Func<PXDataFieldRestrict, bool>) (c => c == PXSelectOriginalsRestrict.SelectAllOriginalValues));
    bool flag1 = selectAllNeeded || ((IEnumerable<PXDataFieldRestrict>) pars).Any<PXDataFieldRestrict>((System.Func<PXDataFieldRestrict, bool>) (c => c == PXSelectOriginalsRestrict.SelectOriginalValues));
    PXDataFieldParam[][] pxDataFieldParamArray = flag1 ? this.SelectOriginalValues(table, (PXDataFieldParam[]) array, selectAllNeeded).ToArray<PXDataFieldParam[]>() : PXDatabaseProviderBase._emptySelectedOriginalRows;
    bool flag2 = this.DeleteImpl(table, true, array);
    if (flag2)
    {
      int companyId = this.getCompanyID(table.Name, out companySetting _);
      if (flag1 && pxDataFieldParamArray.Length != 0)
      {
        foreach (PXDataFieldParam[] parameters in pxDataFieldParamArray)
        {
          this.SendToNotificationQueue(table, parameters, PXDBOperation.Update, companyId);
          PXTransactionScope.SendDeletedRecord(table, parameters);
        }
      }
      else
      {
        this.SendToNotificationQueue(table, (PXDataFieldParam[]) pars, PXDBOperation.Delete, companyId);
        PXTransactionScope.SendDeletedRecord(table, (PXDataFieldParam[]) pars);
      }
    }
    return flag2;
  }

  public override bool ForceDelete(System.Type table, params PXDataFieldRestrict[] pars)
  {
    PXTransactionScope.EnsurePrimalOperation(PXDBOperation.Delete, table);
    int num = this.DeleteImpl(table, false, pars) ? 1 : 0;
    if (num == 0)
      return num != 0;
    int companyId = this.getCompanyID(table.Name, out companySetting _);
    this.SendToNotificationQueue(table, (PXDataFieldParam[]) pars, PXDBOperation.Delete, companyId);
    PXTransactionScope.SendDeletedRecord(table, (PXDataFieldParam[]) pars);
    return num != 0;
  }

  private bool DeleteImpl(System.Type table, bool checkDeletedColumn, PXDataFieldRestrict[] pars)
  {
    string tableName = table.Name;
    if (this._RestrictedTables != null && this._RestrictedTables.Contains(tableName))
      throw new PXException("Modification of the {0} table is not allowed in this installation.", new object[1]
      {
        (object) tableName
      });
    PXDatabase.AuditTable audit = this.auditDefinition == null || !this.auditDefinition.AuditRequired(tableName) ? (PXDatabase.AuditTable) null : PXTransactionScope.GetAudit("D", tableName);
    companySetting settings;
    int companyId = this.getCompanyID(tableName, out settings);
    if (settings.TableNotFound)
      throw new PXException("The table schema of the {0} table was not found in the cache. The table is locked by another process. Please try again later.", new object[1]
      {
        (object) tableName
      });
    if (checkDeletedColumn && settings != null && settings.Deleted != null)
    {
      short num1 = 2;
      if (settings.Modified != null)
        ++num1;
      if (settings.ModifiedBy != null)
        ++num1;
      if (settings.TimeTag != null)
        ++num1;
      PXDataFieldParam[] destinationArray = new PXDataFieldParam[pars.Length + (int) num1];
      Array.Copy((Array) pars, 0, (Array) destinationArray, 0, pars.Length);
      if (settings.TimeTag != null)
        destinationArray[destinationArray.Length - (int) num1--] = (PXDataFieldParam) new PXDataFieldAssign(settings.TimeTag, PXDbType.DateTime, new int?(8), (object) System.DateTime.UtcNow);
      if (settings.Modified != null)
        destinationArray[destinationArray.Length - (int) num1--] = (PXDataFieldParam) new PXDataFieldAssign(settings.Modified, PXDbType.DirectExpression, new int?(8), (object) this.SqlDialect.GetUtcDate);
      if (settings.ModifiedBy != null)
      {
        PXDataFieldParam[] pxDataFieldParamArray = destinationArray;
        int length = destinationArray.Length;
        int num2 = (int) num1;
        short num3 = (short) (num2 - 1);
        int index = length - num2;
        PXDataFieldAssign pxDataFieldAssign = new PXDataFieldAssign(settings.ModifiedBy, PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) this.CurrentUserInformationProvider.GetUserIdOrDefault());
        pxDataFieldParamArray[index] = (PXDataFieldParam) pxDataFieldAssign;
      }
      destinationArray[destinationArray.Length - 2] = (PXDataFieldParam) new PXDataFieldAssign(settings.Deleted, PXDbType.Bit, new int?(1), (object) true);
      destinationArray[destinationArray.Length - 1] = (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed;
      try
      {
        return this.update(table, audit, destinationArray);
      }
      catch (PXDatabaseException ex)
      {
        if (ex is PXDbOperationSwitchRequiredException)
          PXTransactionScope.SetSharedDelete();
        throw;
      }
    }
    else
    {
      StringBuilder stringBuilder = new StringBuilder("DELETE FROM ");
      stringBuilder.Append(tableName);
      bool flag = false;
      for (int i = 0; i < pars.Length; ++i)
      {
        PXDataFieldRestrict par = pars[i];
        if (par.Storage == StorageBehavior.Table)
        {
          if (flag)
          {
            stringBuilder.Append(par.OrOperator ? " OR " : " AND ");
          }
          else
          {
            stringBuilder.Append(" WHERE (");
            flag = true;
          }
          if (par.OpenBrackets > 0)
            stringBuilder.Append('(', par.OpenBrackets);
          stringBuilder.Append(this.getCompareCondition(par.Comp, par.ValueType, par.Value as string, par.Column, i));
          if (par.CloseBrackets > 0)
            stringBuilder.Append(')', par.CloseBrackets);
        }
      }
      if (flag)
        stringBuilder.Append(")");
      if (companyId > 0 || companySetting.NeedRestrict(settings))
      {
        stringBuilder.AppendFormat(" {0} CompanyID = {1}", flag ? (object) "AND" : (object) "WHERE", (object) companyId);
        flag = true;
      }
      if (settings.WebAppType != null)
        stringBuilder.AppendFormat($" {(flag ? "AND" : "WHERE")} WebAppType = {WebAppType.Current.AppTypeId.ToString()}");
      PXTransactionScope.TableModified(table);
      return this.usingDbCommand<bool>((Func<string, IDbCommand, bool>) ((tn1, cmd) => this.deleteCommon(cmd, pars, tableName, audit)), stringBuilder.ToString(), tableName);
    }
  }

  internal bool deleteCommon(
    IDbCommand cmd,
    PXDataFieldRestrict[] pars,
    string tableName,
    PXDatabase.AuditTable audit)
  {
    companySetting settings;
    int companyId = this.getCompanyID(tableName, out settings);
    if (settings.TableNotFound)
      throw new PXException("The table schema of the {0} table was not found in the cache. The table is locked by another process. Please try again later.", new object[1]
      {
        (object) tableName
      });
    TableHeader tableHeader = (TableHeader) null;
    for (int parameterIndex = 0; parameterIndex < pars.Length; ++parameterIndex)
    {
      PXDataFieldRestrict par = pars[parameterIndex];
      if (par.Storage != StorageBehavior.KeyValueKey && par.ValueType != PXDbType.DirectExpression && par.Comp != PXComp.ISNULL && par.Comp != PXComp.ISNOTNULL)
      {
        if (par.Storage == StorageBehavior.Table)
          this._AddParameter(cmd, parameterIndex, this.getValueType(tableName, ref tableHeader, (PXDataFieldParam) par), par.ValueLength, ParameterDirection.Input, par.Value, PXDatabaseProviderBase.ParameterBehavior.Compare);
        if (audit != null && par.Value != null)
          audit.Fields.Add(new PXDatabase.AuditField(par.Column.Name, par.Value.ToString(), true, par.Storage));
      }
    }
    if (cmd.Connection.State == ConnectionState.Closed)
      this.OpenConnection(cmd.Connection);
    bool flag = this.ExecuteNonQuery(cmd) > 0;
    string updateables;
    if (!flag && settings != null && settings.Flag == companySetting.companyFlag.Shared && this.tryGetUpdateableCompanies(companyId, out updateables))
    {
      this.alterCommandForSharedRecords(cmd, companyId, updateables);
      flag = this.ExecuteNonQuery(cmd) > 0;
    }
    int[] selectables;
    if (!flag && settings != null && settings.Flag == companySetting.companyFlag.Shared && this.tryGetSelectableCompanies(companyId, out selectables))
    {
      string str = string.Join<int>(", ", (IEnumerable<int>) selectables);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("UPDATE {0} SET {1} = {2}", (object) tableName, (object) "CompanyMask", (object) this.SqlDialect.binaryMaskSub("CompanyMask", companyId, 2));
      stringBuilder.AppendFormat(" WHERE CompanyID IN({0}) AND {1}", (object) str, (object) this.SqlDialect.binaryMaskTest("CompanyMask", companyId, 2));
      for (int i = 0; i < pars.Length; ++i)
      {
        if (pars[i].Storage == StorageBehavior.Table)
          stringBuilder.Append(" AND ").Append(this.getCompareCondition(pars[i].Comp, pars[i].ValueType, pars[i].Value as string, pars[i].Column, i));
      }
      cmd.CommandText = stringBuilder.ToString();
      cmd.Parameters.Clear();
      for (int parameterIndex = 0; parameterIndex < pars.Length; ++parameterIndex)
      {
        if (pars[parameterIndex].ValueType != PXDbType.DirectExpression && pars[parameterIndex].Storage == StorageBehavior.Table && pars[parameterIndex].Comp != PXComp.ISNULL && pars[parameterIndex].Comp != PXComp.ISNOTNULL)
          this._AddParameter(cmd, parameterIndex, this.getValueType(tableName, ref tableHeader, (PXDataFieldParam) pars[parameterIndex]), pars[parameterIndex].ValueLength, ParameterDirection.Input, pars[parameterIndex].Value, PXDatabaseProviderBase.ParameterBehavior.Compare);
      }
      flag = this.ExecuteNonQuery(cmd) > 0;
    }
    if (flag)
      this.SaveKeyValueStored(tableName, PXDBOperation.Delete, (PXDataFieldParam[]) pars);
    return flag;
  }

  protected virtual string getCompareCondition(
    PXComp opCompare,
    PXDbType type,
    string value,
    Column column,
    int i)
  {
    return this.getCompareCondition(opCompare, type, value, column.SQLQuery(this.SqlDialect.GetConnection()).ToString(), i);
  }

  protected string getCompareCondition(
    PXComp opCompare,
    PXDbType type,
    string value,
    SQLExpression expression,
    int i)
  {
    return expression is Column column ? this.getCompareCondition(opCompare, type, value, column, i) : this.getCompareCondition(opCompare, type, value, expression.SQLQuery(this.SqlDialect.GetConnection()).ToString(), i);
  }

  protected string getCompareCondition(
    PXComp opCompare,
    PXDbType type,
    string value,
    string fieldName,
    int i)
  {
    string compareCollation = this.SqlDialect.GetCompareCollation(type);
    switch (opCompare)
    {
      case PXComp.EQ:
        return type == PXDbType.Timestamp ? $"{fieldName} <= {this.CreateParameterName(i)}" : $"{fieldName} = {this.getDataFieldValue(type, (object) value, i)}{compareCollation}";
      case PXComp.NE:
        return $"{fieldName} <> {this.getDataFieldValue(type, (object) value, i)}{compareCollation}";
      case PXComp.GT:
        return $"{fieldName} > {this.getDataFieldValue(type, (object) value, i)}{compareCollation}";
      case PXComp.GE:
        return $"{fieldName} >= {this.getDataFieldValue(type, (object) value, i)}{compareCollation}";
      case PXComp.LT:
        return $"{fieldName} < {this.getDataFieldValue(type, (object) value, i)}{compareCollation}";
      case PXComp.LE:
        return $"{fieldName} <= {this.getDataFieldValue(type, (object) value, i)}{compareCollation}";
      case PXComp.ISNULL:
        return fieldName + " IS NULL";
      case PXComp.ISNOTNULL:
        return fieldName + " IS NOT NULL";
      case PXComp.LIKE:
        return this.getScripter().FormatLike(fieldName, this.getDataFieldValue(type, (object) value, i));
      case PXComp.EQorISNULL:
        return string.Format("({0} = {1}{2} OR {0} IS NULL)", (object) fieldName, (object) this.getDataFieldValue(type, (object) value, i), (object) compareCollation);
      case PXComp.NEorISNULL:
        return string.Format("({0} <> {1}{2} OR {0} IS NULL)", (object) fieldName, (object) this.getDataFieldValue(type, (object) value, i), (object) compareCollation);
      case PXComp.LEorISNULL:
        return string.Format("({0} <= {1}{2} OR {0} IS NULL)", (object) fieldName, (object) this.getDataFieldValue(type, (object) value, i), (object) compareCollation);
      default:
        return string.Empty;
    }
  }

  private void updateSiblingCompanies(
    string tableName,
    PXDataFieldParam[] pars,
    string listCompanies,
    IDbCommand cmd,
    int cid,
    int[] siblings)
  {
    string oldValue = $" WHERE CompanyID IN({listCompanies})";
    int num1 = cmd.CommandText.IndexOf(oldValue, StringComparison.OrdinalIgnoreCase);
    string str1 = cmd.CommandText.Substring(num1 + oldValue.Length);
    string updateables;
    if (num1 == -1 || !this.tryGetUpdateableCompanies(cid, out updateables))
      return;
    string newValue = $" WHERE CompanyID IN({updateables})";
    cmd.CommandText = cmd.CommandText.Replace(oldValue, newValue);
    DbAdapterBase.CorrectCommand(cmd);
    int[] selectables;
    if (cmd.ExecuteNonQuery() > 0 || !this.tryGetSelectableCompanies(cid, out selectables))
      return;
    string str2 = string.Join<int>(", ", (IEnumerable<int>) selectables);
    bool flag1;
    byte[] parameterValue;
    if (this.canPassOutParamsToUpdate())
    {
      cmd.CommandText = $"UPDATE {tableName} SET {this.CreateParameterName(pars.Length)} = CompanyMask, CompanyMask = DEFAULT WHERE CompanyID IN({str2}){str1}";
      IDbDataParameter dbDataParameter = this._AddParameter(cmd, pars.Length, PXDbType.VarBinary, new int?(-1), ParameterDirection.Output, (object) null, PXDatabaseProviderBase.ParameterBehavior.Assign);
      DbAdapterBase.CorrectCommand(cmd);
      flag1 = cmd.ExecuteNonQuery() > 0;
      parameterValue = dbDataParameter.Value as byte[];
    }
    else
    {
      cmd.CommandText = $"SELECT CompanyMask FROM {tableName} WHERE CompanyID IN ({str2}){str1}";
      DbAdapterBase.CorrectCommand(cmd);
      parameterValue = cmd.ExecuteScalar() as byte[];
      cmd.CommandText = $"UPDATE {tableName} SET CompanyMask = DEFAULT WHERE CompanyID IN({str2}){str1}";
      DbAdapterBase.CorrectCommand(cmd);
      flag1 = cmd.ExecuteNonQuery() > 0;
    }
    if (!flag1 || parameterValue == null)
      return;
    StringBuilder stringBuilder = new StringBuilder("UPDATE ");
    stringBuilder.Append(tableName);
    stringBuilder.Append(" SET ");
    int num2 = pars.Length + 1;
    int num3 = pars.Length + 2;
    ISqlDialect sqlDialect = this.SqlDialect;
    List<string> parts = new List<string>();
    for (int index = 0; index < siblings.Length; ++index)
      parts.Add(sqlDialect.@char(sqlDialect.byteOfMaskAnd(this.CreateParameterName(num3), index + 1, sqlDialect.bitNot(sqlDialect.byteOfMaskAnd("CompanyMask", index + 1, Convert.ToString(siblings[index]))))));
    int companyMaskWidth = this.schemaCache.GetTableHeader(tableName).CompanyMaskWidth;
    if (siblings.Length < companyMaskWidth)
      parts.Add(sqlDialect.substr("CompanyMask", Convert.ToString(siblings.Length + 1), Convert.ToString(companyMaskWidth - siblings.Length)));
    stringBuilder.Append("CompanyMask").Append(" = ");
    stringBuilder.Append(this.SqlDialect.caseWhenThenElse(this.SqlDialect.binaryMaskTest("CompanyMask", cid, 3), this.SqlDialect.concat((IEnumerable<string>) parts), this.CreateParameterName(num3)));
    this._AddParameter(cmd, num3, PXDbType.VarBinary, new int?(parameterValue.Length), ParameterDirection.Input, (object) parameterValue, PXDatabaseProviderBase.ParameterBehavior.Compare);
    string str3 = this.SqlDialect.caseWhenThenElse(this.SqlDialect.binaryMaskTest("CompanyMask", cid, 3), "1", "0");
    if (this.canPassOutParamsToUpdate())
      stringBuilder.AppendFormat(", {0} = {1}", (object) this.CreateParameterName(num2), (object) str3);
    stringBuilder.AppendFormat(" WHERE {2} IN({0}){1}", (object) str2, (object) str1, (object) "CompanyID");
    bool flag2;
    if (this.canPassOutParamsToUpdate())
    {
      cmd.CommandText = stringBuilder.ToString();
      DbAdapterBase.CorrectCommand(cmd);
      IDbDataParameter dbDataParameter = this._AddParameter(cmd, num2, PXDbType.Bit, new int?(1), ParameterDirection.Output, (object) null, PXDatabaseProviderBase.ParameterBehavior.Assign);
      cmd.ExecuteNonQuery();
      flag2 = Convert.ToBoolean(dbDataParameter.Value);
    }
    else
    {
      cmd.CommandText = $"SELECT {str3} FROM {tableName} WHERE CompanyID IN({str2}){str1}";
      DbAdapterBase.CorrectCommand(cmd);
      flag2 = cmd.ExecuteScalar().ToString().Equals("1");
      cmd.CommandText = stringBuilder.ToString();
      DbAdapterBase.CorrectCommand(cmd);
      cmd.ExecuteNonQuery();
    }
    if (!flag2)
      return;
    PXTransactionScope.SetSharedInsert(2);
  }

  private bool appendUpdateAssignedFields(
    StringBuilder bld,
    int cid,
    PXDataFieldParam[] pars,
    bool sharedDelete,
    companySetting setting)
  {
    bool flag = false;
    for (int offset = 0; offset < pars.Length; ++offset)
    {
      PXDataFieldAssign pxDataFieldAssign = pars[offset] as PXDataFieldAssign;
      if (sharedDelete && pxDataFieldAssign == null && pars[offset] is PXDataFieldRestrict && (pars[offset].ValueType == PXDbType.Char || pars[offset].ValueType == PXDbType.VarChar || pars[offset].ValueType == PXDbType.NChar || pars[offset].ValueType == PXDbType.NVarChar))
        pxDataFieldAssign = new PXDataFieldAssign(pars[offset].Column, pars[offset].ValueType, pars[offset].ValueLength, pars[offset].Value);
      if (pxDataFieldAssign != null && pxDataFieldAssign.Storage == StorageBehavior.Table && (pxDataFieldAssign.ValueType != PXDbType.DirectExpression || pxDataFieldAssign.Value is string) && (cid <= 0 && !companySetting.NeedRestrict(setting) || string.Compare(pars[offset].Column.Name, "CompanyID", StringComparison.OrdinalIgnoreCase) != 0))
      {
        if (flag)
          bld.Append(", ");
        else
          flag = true;
        string fieldName = pars[offset].Column.SQLQuery(this.SqlDialect.GetConnection()).ToString();
        string fieldAssignment = this.getFieldAssignment(fieldName, pxDataFieldAssign.ValueType == PXDbType.DirectExpression ? pxDataFieldAssign.Value as string : (string) null, pxDataFieldAssign.Behavior, offset, false);
        bld.Append(fieldName);
        bld.Append(" = ");
        bld.Append(fieldAssignment);
      }
    }
    if (!flag)
      return false;
    if (sharedDelete)
      bld.AppendFormat(", {0} = {1}", (object) setting.Deleted, this.SqlDialect.FalseValue);
    return true;
  }

  private void appendUpdateVisibility(StringBuilder bld, int cid, int parsLength)
  {
    ISqlDialect sqlDialect = this.SqlDialect;
    bld.Append("CompanyMask").Append(" = ");
    string format = sqlDialect.caseWhenThenElse("CompanyID = " + cid.ToString(), sqlDialect.quoteFn("binaryMaskAdd({0}, {1}, 2)"), sqlDialect.quoteFn("binaryMaskSub({0}, {1}, 2)"));
    bld.AppendFormat(format, (object) sqlDialect.quoteDbIdentifier("CompanyMask"), (object) cid);
    if (cid == this.getMainCompanyOf(cid))
      return;
    bld.Append(", CompanyID = ");
    string condition = $"CompanyID = {cid} AND {sqlDialect.binaryMaskTest("CompanyMask", cid, 3)}";
    bld.Append(sqlDialect.caseWhenThenElse(condition, this.getMainCompanyOf(cid).ToString(), "CompanyID"));
    if (!this.canPassOutParamsToUpdate())
      return;
    bld.AppendFormat(", {0} = {1}", (object) this.CreateParameterName(parsLength), (object) sqlDialect.caseWhenThenElse(sqlDialect.binaryMaskTest("CompanyMask", cid, 3), "1", "0"));
  }

  protected virtual string getDataFieldValue(
    PXDbType type,
    object value,
    int iParameter,
    TableColumn destColumn = null)
  {
    return type == PXDbType.DirectExpression ? value as string : this.CreateParameterName(iParameter);
  }

  protected bool appendUpdateRestrictions(
    PXDataFieldParam[] pars,
    StringBuilder bld,
    bool visibilityUpdate,
    bool sharedDelete,
    int cid,
    companySetting setting)
  {
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    for (int i = 0; i < pars.Length; ++i)
    {
      if (!(pars[i] is PXDataFieldRestrict par) || pars[i].ValueType == PXDbType.DirectExpression && !(pars[i].Value is string) || par.CheckResultOnly)
      {
        if (!flag1 && par != null)
          flag1 = par.CheckResultOnly;
      }
      else if (par == PXDataFieldRestrict.OperationSwitchAllowed)
        flag2 = true;
      else if (par == null || par.Storage == StorageBehavior.Table)
      {
        if (flag3)
        {
          bld.Append(par.OrOperator ? " OR " : " AND ");
        }
        else
        {
          bld.Append(" WHERE (");
          flag3 = true;
        }
        if (par.OpenBrackets > 0)
          bld.Append('(', par.OpenBrackets);
        string str = par.ValueType == PXDbType.DirectExpression ? par.Value as string : (string) null;
        PXDbType type = par.ValueType == PXDbType.Timestamp ? PXDbType.Timestamp : (string.IsNullOrEmpty(str) ? PXDbType.Unspecified : PXDbType.DirectExpression);
        bld.Append(this.getCompareCondition(par.Comp, type, str, par.Column, i));
        if (par.CloseBrackets > 0)
          bld.Append(')', par.CloseBrackets);
      }
    }
    if (flag1 && !visibilityUpdate)
    {
      for (int offset1 = 0; offset1 < pars.Length; ++offset1)
      {
        if (pars[offset1] is PXDataFieldRestrict && (pars[offset1].ValueType != PXDbType.DirectExpression || pars[offset1].Value is string) && ((PXDataFieldRestrict) pars[offset1]).CheckResultOnly && pars[offset1].Storage == StorageBehavior.Table)
        {
          if (pars[offset1] == PXDataFieldRestrict.OperationSwitchAllowed)
          {
            flag2 = true;
          }
          else
          {
            if (!flag3)
              bld.Append(" WHERE (");
            StringBuilder stringBuilder = new StringBuilder();
            for (int offset2 = 0; offset2 < pars.Length; ++offset2)
            {
              PXDataFieldAssign pxDataFieldAssign = pars[offset2] as PXDataFieldAssign;
              if (sharedDelete && pxDataFieldAssign == null && pars[offset2] is PXDataFieldRestrict && (pars[offset2].ValueType == PXDbType.Char || pars[offset2].ValueType == PXDbType.VarChar || pars[offset2].ValueType == PXDbType.NChar || pars[offset2].ValueType == PXDbType.NVarChar))
                pxDataFieldAssign = new PXDataFieldAssign(pars[offset2].Column, pars[offset2].ValueType, pars[offset2].ValueLength, pars[offset2].Value);
              if (pxDataFieldAssign != null && (pxDataFieldAssign.ValueType != PXDbType.DirectExpression || pxDataFieldAssign.Value is string) && (cid <= 0 && !companySetting.NeedRestrict(setting) || string.Compare(pars[offset2].Column.Name, "CompanyID", StringComparison.OrdinalIgnoreCase) != 0) && pars[offset2].Column.Equals((SQLExpression) pars[offset1].Column))
              {
                stringBuilder.Append(this.getFieldAssignment((SQLExpression) pars[offset2].Column, pxDataFieldAssign.ValueType == PXDbType.DirectExpression ? pxDataFieldAssign.Value as string : (string) null, pxDataFieldAssign.Behavior, offset2, false));
                break;
              }
            }
            if (stringBuilder.Length == 0)
              stringBuilder.Append((object) pars[offset1].Column.SQLQuery(this._sqlDialect.GetConnection()));
            this.insertFieldValue(bld, stringBuilder.ToString(), pars[offset1].ValueType == PXDbType.DirectExpression ? pars[offset1].Value as string : (string) null, pars[offset1].ValueType == PXDbType.Timestamp, ((PXDataFieldRestrict) pars[offset1]).Comp, ((PXDataFieldRestrict) pars[offset1]).OrOperator, ((PXDataFieldRestrict) pars[offset1]).OpenBrackets, ((PXDataFieldRestrict) pars[offset1]).CloseBrackets, offset1, !flag3);
            flag3 = true;
          }
        }
      }
    }
    if (flag3)
      bld.Append(")");
    if (cid > 0 || companySetting.NeedRestrict(setting))
    {
      bld.Append(flag3 ? " AND CompanyID" : " WHERE CompanyID");
      if (!visibilityUpdate)
      {
        bld.Append(" = ").Append(cid);
      }
      else
      {
        int[] selectables;
        if (this.tryGetSelectableCompanies(cid, out selectables))
          bld.AppendFormat(" IN ({0}, {1})", (object) string.Join<int>(", ", (IEnumerable<int>) selectables), (object) cid);
        else
          bld.Append(" = " + cid.ToString());
      }
      if (sharedDelete)
        bld.AppendFormat(" AND {0} = 1", (object) setting.Deleted);
    }
    return flag2;
  }

  protected string getFieldAssignment(
    SQLExpression fieldName,
    string directExpression,
    PXDataFieldAssign.AssignBehavior behavior,
    int offset,
    bool defaultField)
  {
    return this.getFieldAssignment(fieldName.SQLQuery(this.SqlDialect.GetConnection()).ToString(), directExpression, behavior, offset, defaultField);
  }

  protected string getFieldAssignment(
    string fieldName,
    string directExpression,
    PXDataFieldAssign.AssignBehavior behavior,
    int offset,
    bool defaultField)
  {
    string afterThen = string.IsNullOrEmpty(directExpression) ? this.CreateParameterName(offset) : directExpression;
    switch (behavior)
    {
      case PXDataFieldAssign.AssignBehavior.Summarize:
        return $"{fieldName} + {afterThen}";
      case PXDataFieldAssign.AssignBehavior.Maximize:
        return this.SqlDialect.caseWhenThenElse($"{fieldName} IS NULL OR {fieldName} < {afterThen}", afterThen, fieldName);
      case PXDataFieldAssign.AssignBehavior.Minimize:
        return this.SqlDialect.caseWhenThenElse($"{fieldName} IS NULL OR {fieldName} > {afterThen}", afterThen, fieldName);
      case PXDataFieldAssign.AssignBehavior.Initialize:
        return $"COALESCE({fieldName}, {afterThen})";
      default:
        return !defaultField ? afterThen : fieldName;
    }
  }

  protected SQLExpression AlterFieldExpression(SQLExpression fieldName)
  {
    if (fieldName is Column)
      return fieldName;
    SQLExpression sqlExpression = fieldName.Duplicate();
    foreach (SubQuery subQuery in sqlExpression.GetExpressionsOfType<SubQuery>())
      subQuery.Query().AppendRestrictions(false).Limit(1);
    return sqlExpression;
  }

  public override BqlFullTextRenderingMethod getFullTextRenderingMethod(
    string tableName,
    string column)
  {
    TableIndex textIndexOnTable = this.schemaCache.GetFullTextIndexOnTable(tableName);
    string colName = column;
    if (!string.IsNullOrWhiteSpace(colName) && this._sqlDialect.isColumnNameQuoted(colName))
      colName = column.Substring(1, column.Length - 2);
    return textIndexOnTable == null || !textIndexOnTable.Columns.Any<TableIndexOnColumn>((System.Func<TableIndexOnColumn, bool>) (c => ((TableEntityBase) c).Name.Equals(colName, StringComparison.OrdinalIgnoreCase))) ? BqlFullTextRenderingMethod.NeutralLike : this._sqlDialect.FullTextRenderingMode;
  }

  internal override void SchemaCacheInvalidate(string singleTableName = null)
  {
    if (WebConfig.IsClusterEnabled)
      this.SaveWatchDog((IEnumerable<System.Type>) new System.Type[1]
      {
        typeof (PXDatabaseProvider.SchemaCacheInvalidateFlag)
      });
    else if (singleTableName == null)
      this.schemaCache.InvalidateAll();
    else
      this.schemaCache.InvalidateTable(singleTableName);
  }

  protected abstract string getFreeText(
    StringBuilder bld,
    string text,
    ref int start,
    int stop,
    string table,
    string alias,
    string column,
    string key);

  public override void SelectDate(out System.DateTime dtLocal, out System.DateTime dtUtc)
  {
    IDbCommand dbCommand = (IDbCommand) null;
    IDataReader dataReader = (IDataReader) null;
    try
    {
      dbCommand = (IDbCommand) this.GetCommand();
      dbCommand.CommandText = $"SELECT {this.SqlDialect.GetDate}, {this.SqlDialect.GetUtcDate}";
      if (dbCommand.Connection.State == ConnectionState.Closed)
        this.OpenConnection(dbCommand.Connection);
      DbAdapterBase.CorrectCommand(dbCommand);
      dataReader = dbCommand.ExecuteReader();
      bool flag = dataReader.Read();
      dtLocal = !flag || dataReader.IsDBNull(0) ? System.DateTime.Now : dataReader.GetDateTime(0);
      dtUtc = !flag || dataReader.IsDBNull(1) ? System.DateTime.UtcNow : dataReader.GetDateTime(1);
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

  protected abstract string ScriptParametersForProfiler(IDbCommand command);

  protected virtual int ExecuteNonQueryCommand(IDbCommand command)
  {
    DbAdapterBase.CorrectCommand(command);
    return command.ExecuteNonQuery();
  }

  protected int ExecuteNonQuery(IDbCommand command)
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
          profilerSqlSample = pxPerformanceInfo.AddSqlSample(command.CommandText, this.ScriptParametersForProfiler(command));
          profilerSqlSample?.SqlTimer.Start();
        }
      }
    }
    try
    {
      return this.ExecuteNonQueryCommand(command);
    }
    finally
    {
      if (pxPerformanceInfo != null)
      {
        pxPerformanceInfo.SqlTimer.Stop();
        profilerSqlSample?.SqlTimer.Stop();
      }
    }
  }

  /// <exclude />
  public class ResetAllTables : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }

  /// <exclude />
  protected class SubscribersStamp
  {
    public Decimal _SubscribersChangeID;
    public byte[] _SubscribersTStamp;
    public System.DateTime _SubscribersDateTime;
    public byte[] _SubscribersOldTStamp;
    public System.DateTime _SubscribersOldDateTime;
    public bool _ForceReset;
  }

  /// <exclude />
  internal class RequiredSubscriber
  {
    public readonly string Key;
    public readonly string CompanyTable;
    public readonly System.Type[] Tables;
    public readonly System.Type ObjectType;

    public RequiredSubscriber(string key, string companyTable, System.Type[] tables, System.Type objectType)
    {
      this.Key = key ?? throw new ArgumentNullException(nameof (key));
      this.CompanyTable = companyTable;
      this.Tables = tables;
      this.ObjectType = objectType ?? throw new ArgumentNullException(nameof (objectType));
    }
  }

  /// <exclude />
  protected class SlotClosing
  {
    public readonly PXDatabaseProviderBase provider;
    internal readonly PXDatabaseProviderBase.RequiredSubscriber subscriber;
    public readonly bool multicompany;

    internal SlotClosing(
      PXDatabaseProviderBase prov,
      PXDatabaseProviderBase.RequiredSubscriber subscr,
      bool multi)
    {
      this.provider = prov;
      this.subscriber = subscr;
      this.multicompany = multi;
    }

    public void Subscribe()
    {
      this.provider.Logger.ForContext<PXDatabaseProviderBase.SlotClosing>().WithEventID("ProfileSlots", nameof (Subscribe)).WithStack().Debug<string>("ProfileSlots: {Subscriber}", this.subscriber.Key);
      int key = 0;
      if (this.multicompany)
        key = this.provider.getCompanyID(this.subscriber.CompanyTable, out companySetting _);
      ConcurrentDictionary<string, PXDatabaseSlot> concurrentDictionary;
      if (!this.provider._Slots.TryGetValue(key, out concurrentDictionary))
        return;
      PXDatabaseSlot comparisonValue;
      if (concurrentDictionary.TryGetValue(this.subscriber.Key, out comparisonValue))
        concurrentDictionary.TryUpdate(this.subscriber.Key, (PXDatabaseSlot) null, comparisonValue);
      TypeKeyedOperationExtensions.Remove(SlotStore.Instance, this.subscriber.ObjectType);
      SlotStore.Instance.Remove(this.subscriber.Key);
    }
  }

  internal enum ParameterBehavior
  {
    Unknown,
    Compare,
    Assign,
  }

  internal class Paran
  {
    public List<PXDatabaseProviderBase.Paran> elements = new List<PXDatabaseProviderBase.Paran>();
    private bool hidden;

    protected Paran()
    {
    }

    public static string getSegment(string s, ref int currentPos)
    {
      int num1 = s.IndexOf(')', currentPos);
      int num2 = s.IndexOf('(', currentPos);
      if (num1 >= 0 && (num1 < num2 || num2 < 0))
      {
        string str = s.Substring(currentPos, num1 - currentPos);
        currentPos = num1;
        char[] chArray = new char[3]{ ' ', '\n', '\r' };
        return str.Trim(chArray);
      }
      if (num2 >= 0 && (num2 < num1 || num1 < 0))
      {
        string str = s.Substring(currentPos, num2 - currentPos);
        currentPos = num2;
        char[] chArray = new char[3]{ ' ', '\n', '\r' };
        return str.Trim(chArray);
      }
      string str1 = s.Substring(currentPos);
      currentPos = -1;
      char[] chArray1 = new char[3]{ ' ', '\n', '\r' };
      return str1.Trim(chArray1);
    }

    public static PXDatabaseProviderBase.Paran GetParan(string s, ref int current)
    {
      PXDatabaseProviderBase.Paran paran1 = new PXDatabaseProviderBase.Paran();
      while (true)
      {
        string segment = PXDatabaseProviderBase.Paran.getSegment(s, ref current);
        if (segment.Length > 0)
          paran1.elements.Add((PXDatabaseProviderBase.Paran) new PXDatabaseProviderBase.ParanString(segment));
        if (current >= 0 && s[current] != ')')
        {
          ++current;
          PXDatabaseProviderBase.Paran paran2 = PXDatabaseProviderBase.Paran.GetParan(s, ref current);
          paran1.elements.Add(paran2);
          if (current >= 0)
            ++current;
          else
            break;
        }
        else
          break;
      }
      return paran1;
    }

    public PXDatabaseProviderBase.Paran Cleanup()
    {
      if (this.elements.Count == 1 && this.elements.First<PXDatabaseProviderBase.Paran>().GetType() == typeof (PXDatabaseProviderBase.Paran))
        this.hidden = true;
      bool flag = false;
      foreach (PXDatabaseProviderBase.Paran element in this.elements)
      {
        if (element.GetType() == typeof (PXDatabaseProviderBase.Paran))
        {
          if (flag)
          {
            element.hidden = true;
            flag = false;
          }
          element.Cleanup();
        }
        if (element.GetType() == typeof (PXDatabaseProviderBase.ParanString) && ((PXDatabaseProviderBase.ParanString) element).content.EndsWith("WHERE", StringComparison.OrdinalIgnoreCase))
          flag = true;
      }
      return this;
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (PXDatabaseProviderBase.Paran element in this.elements)
      {
        if (element.GetType() == typeof (PXDatabaseProviderBase.Paran) && !element.hidden)
          stringBuilder.Append('(');
        stringBuilder.Append(element.ToString());
        if (element.GetType() == typeof (PXDatabaseProviderBase.Paran) && !element.hidden)
          stringBuilder.Append(')');
      }
      return stringBuilder.ToString();
    }
  }

  internal class ParanString : PXDatabaseProviderBase.Paran
  {
    public string content = "";

    public ParanString(string s) => this.content = s;

    public override string ToString() => this.content + " ";
  }

  public class ExpressionParametersEvaluateManager
  {
    private readonly ISqlDialect _sqlDialect;
    private readonly TableHeader _header;
    private readonly ILogger _logger;

    private IEnumerable<(PXDataFieldAssign Parameter, string SourceColumn)> _needEvaluateParameters { get; set; } = (IEnumerable<(PXDataFieldAssign, string)>) new List<(PXDataFieldAssign, string)>();

    public bool NeedEvaluate => this._needEvaluateParameters.Any<(PXDataFieldAssign, string)>();

    public string[] GetEvaluatedParameterNames()
    {
      return this._needEvaluateParameters.Select<(PXDataFieldAssign, string), string>((System.Func<(PXDataFieldAssign, string), string>) (x => x.Parameter.Column.Name)).ToArray<string>();
    }

    public ExpressionParametersEvaluateManager(
      IEnumerable<PXDataFieldParam> allParameters,
      ISqlDialect sqlDialect,
      TableHeader header,
      ILogger logger)
    {
      PXDatabaseProviderBase.ExpressionParametersEvaluateManager parametersEvaluateManager = this;
      this._sqlDialect = sqlDialect;
      this._header = header;
      this._logger = logger;
      this._needEvaluateParameters = (IEnumerable<(PXDataFieldAssign, string)>) allParameters.OfType<PXDataFieldAssign>().Where<PXDataFieldAssign>((System.Func<PXDataFieldAssign, bool>) (x => x.ValueType == PXDbType.DirectExpression && x.Value is string str && !sqlDialect.IsValueGetDateOrGetUtcDate((object) str))).Select<PXDataFieldAssign, (PXDataFieldAssign, string)>((System.Func<PXDataFieldAssign, (PXDataFieldAssign, string)>) (x => (x, parametersEvaluateManager.GetSourceColumnName(x.Value)))).ToArray<(PXDataFieldAssign, string)>();
      if (!this._needEvaluateParameters.Any<(PXDataFieldAssign, string)>())
        return;
      logger.Debug<string, string>("The following parameters for the {TableName} table need to be filled in: {Parameters}.", ((TableEntityBase) this._header).Name, Str.Join(this._needEvaluateParameters.Select<(PXDataFieldAssign, string), string>((System.Func<(PXDataFieldAssign, string), string>) (x => x.Parameter.Column.Name)), ", "));
    }

    private string GetSourceColumnName(object value)
    {
      if (value is string str)
      {
        TableColumn columnByName = this._header.getColumnByName(str);
        if (columnByName != null)
          return ((TableEntityBase) columnByName).Name;
      }
      return (string) null;
    }

    private void LogEvaluateInfo(string parameterName, object oldValue, object newValue)
    {
      this._logger.Debug("Parameter {TableName}.{ParameterName} was evaluated. {OldValue} -> {NewValue}", new object[4]
      {
        (object) ((TableEntityBase) this._header).Name,
        (object) parameterName,
        oldValue ?? (object) "null",
        newValue ?? (object) "null"
      });
    }

    public void TryEvaluateOnExistingParameters(IEnumerable<PXDataFieldParam> allParameters)
    {
      if (!this.NeedEvaluate || allParameters.OfType<PXDataFieldRestrict>().Any<PXDataFieldRestrict>((System.Func<PXDataFieldRestrict, bool>) (x => x.Comp != PXComp.EQ || x.OrOperator)))
        return;
      ColumnNameComparer comparer = new ColumnNameComparer(((TableEntityBase) this._header).Name, this._sqlDialect);
      IEnumerable<IGrouping<string, PXDataFieldRestrict>> source = allParameters.OfType<PXDataFieldRestrict>().GroupBy<PXDataFieldRestrict, string>((System.Func<PXDataFieldRestrict, string>) (x => x.Column.Name), (IEqualityComparer<string>) comparer);
      if (source.Any<IGrouping<string, PXDataFieldRestrict>>((System.Func<IGrouping<string, PXDataFieldRestrict>, bool>) (x => x.Count<PXDataFieldRestrict>() > 1)))
        return;
      Dictionary<string, PXDataFieldRestrict> dictionary = source.ToDictionary<IGrouping<string, PXDataFieldRestrict>, string, PXDataFieldRestrict>((System.Func<IGrouping<string, PXDataFieldRestrict>, string>) (x => x.Key), (System.Func<IGrouping<string, PXDataFieldRestrict>, PXDataFieldRestrict>) (x => x.Single<PXDataFieldRestrict>()), (IEqualityComparer<string>) comparer);
      List<PXDataFieldAssign> evaluatedParameter = new List<PXDataFieldAssign>();
      foreach ((PXDataFieldAssign Parameter, string SourceColumn) evaluateParameter in this._needEvaluateParameters)
      {
        PXDataFieldRestrict dataFieldRestrict;
        if (evaluateParameter.SourceColumn != null && dictionary.TryGetValue(evaluateParameter.SourceColumn, out dataFieldRestrict) && (dataFieldRestrict.ValueType != PXDbType.DirectExpression || !(dataFieldRestrict.Value is string)))
        {
          this.LogEvaluateInfo(evaluateParameter.Parameter.Column.Name, evaluateParameter.Parameter.Value, dataFieldRestrict.Value);
          evaluateParameter.Parameter.Value = dataFieldRestrict.Value;
          evaluateParameter.Parameter.NewValue = dataFieldRestrict.Value?.ToString();
          evaluateParameter.Parameter.ValueType = dataFieldRestrict.ValueType == PXDbType.DirectExpression ? PXDbType.Unspecified : dataFieldRestrict.ValueType;
          evaluatedParameter.Add(evaluateParameter.Parameter);
        }
      }
      this._needEvaluateParameters = (IEnumerable<(PXDataFieldAssign, string)>) this._needEvaluateParameters.Where<(PXDataFieldAssign, string)>((System.Func<(PXDataFieldAssign, string), bool>) (p => !evaluatedParameter.Contains(p.Parameter))).ToArray<(PXDataFieldAssign, string)>();
    }

    public void TryEvaluateOnSelectedValues(PXDataFieldParam[][] selectedRows)
    {
      if (!this.NeedEvaluate || !((IEnumerable<PXDataFieldParam[]>) selectedRows).Any<PXDataFieldParam[]>())
        return;
      List<PXDataFieldAssign> evaluatedParameter = new List<PXDataFieldAssign>();
      foreach ((PXDataFieldAssign Parameter, string SourceColumn) evaluateParameter in this._needEvaluateParameters)
      {
        if (evaluateParameter.SourceColumn != null)
        {
          int index1 = this._header.Columns.IndexOf(this._header.getColumnByName(evaluateParameter.SourceColumn));
          int index2 = this._header.Columns.IndexOf(this._header.getColumnByName(evaluateParameter.Parameter.Column.Name));
          evaluatedParameter.Add(evaluateParameter.Parameter);
          foreach (PXDataFieldParam[] selectedRow in selectedRows)
          {
            PXDataFieldParam pxDataFieldParam1 = selectedRow[index2];
            PXDataFieldParam pxDataFieldParam2 = selectedRow[index1];
            this.LogEvaluateInfo(pxDataFieldParam1.Column.Name, pxDataFieldParam1.Value, pxDataFieldParam2.Value);
            pxDataFieldParam1.Value = pxDataFieldParam2.Value;
            ((PXDataFieldAssign) pxDataFieldParam1).NewValue = pxDataFieldParam2.Value?.ToString();
            pxDataFieldParam1.ValueType = pxDataFieldParam2.ValueType;
          }
        }
      }
      this._needEvaluateParameters = (IEnumerable<(PXDataFieldAssign, string)>) this._needEvaluateParameters.Where<(PXDataFieldAssign, string)>((System.Func<(PXDataFieldAssign, string), bool>) (p => !evaluatedParameter.Contains(p.Parameter))).ToArray<(PXDataFieldAssign, string)>();
    }

    public bool TryEvaluateOnNewRowsSelected(
      PXDataFieldParam[][] selectedRows,
      PXDataFieldParam[] realParameters,
      System.Func<PXDataField[], IEnumerable<PXDataRecord>> getNewRows)
    {
      if (!this.NeedEvaluate)
        return true;
      int[] columnNumbers = this._header.GetPrimaryKey()?.ToColumnNumbers(this._header.Columns, false);
      if (columnNumbers == null)
        return false;
      IEnumerable<PXDataField> second = ((IEnumerable<int>) columnNumbers).Select<int, PXDataField>((System.Func<int, PXDataField>) (x => new PXDataField(((TableEntityBase) this._header.Columns[x]).Name)));
      ColumnNameComparer columnNameComparer = new ColumnNameComparer(((TableEntityBase) this._header).Name, this._sqlDialect);
      PXDataField[] array = this._needEvaluateParameters.Select<(PXDataFieldAssign, string), PXDataField>((System.Func<(PXDataFieldAssign, string), PXDataField>) (x => new PXDataField((SQLExpression) x.Parameter.Column))).ToArray<PXDataField>();
      IEnumerable<PXDataField> first = realParameters.OfType<PXDataFieldRestrict>().Where<PXDataFieldRestrict>((System.Func<PXDataFieldRestrict, bool>) (c => c != PXDataFieldRestrict.OperationSwitchAllowed)).Select<PXDataFieldRestrict, PXDataFieldValue>((System.Func<PXDataFieldRestrict, PXDataFieldValue>) (c => new PXDataFieldValue((SQLExpression) c.Column, c.ValueType, c.ValueLength, c.Value, c.Comp)
      {
        CloseBrackets = c.CloseBrackets,
        OpenBrackets = c.OpenBrackets,
        OrOperator = c.OrOperator,
        CheckResultOnly = c.CheckResultOnly
      })).OfType<PXDataField>();
      Dictionary<PXDataFieldParam[], Dictionary<string, object>> dictionary1 = new Dictionary<PXDataFieldParam[], Dictionary<string, object>>();
      foreach (PXDataRecord pxDataRecord in getNewRows(first.Concat<PXDataField>(second).Concat<PXDataField>((IEnumerable<PXDataField>) array).ToArray<PXDataField>()))
      {
        PXDataRecord newRow = pxDataRecord;
        IEnumerable<PXDataFieldParam[]> whereClause = ((IEnumerable<PXDataFieldParam[]>) selectedRows).AsEnumerable<PXDataFieldParam[]>();
        EnumerableExtensions.ForEach<int>((IEnumerable<int>) columnNumbers, (System.Action<int>) (x => whereClause = whereClause.Where<PXDataFieldParam[]>((System.Func<PXDataFieldParam[], bool>) (s => s[x].Value.Equals(newRow[((TableEntityBase) this._header.Columns[x]).Name])))));
        PXDataFieldParam[] key = whereClause.FirstOrDefault<PXDataFieldParam[]>();
        if (key == null)
          return false;
        Dictionary<string, object> valuesDict = new Dictionary<string, object>();
        EnumerableExtensions.ForEach<(PXDataFieldAssign, string)>(this._needEvaluateParameters, (System.Action<(PXDataFieldAssign, string)>) (x => valuesDict.Add(x.Parameter.Column.Name, newRow[x.Parameter.Column.Name])));
        dictionary1.Add(key, valuesDict);
      }
      if (dictionary1.Count != ((IEnumerable<PXDataFieldParam[]>) selectedRows).Count<PXDataFieldParam[]>())
        return false;
      foreach (PXDataFieldParam[] selectedRow in selectedRows)
      {
        Dictionary<string, object> dictionary2;
        if (!dictionary1.TryGetValue(selectedRow, out dictionary2))
          return false;
        Dictionary<string, PXDataFieldAssign> dictionary3 = selectedRow.OfType<PXDataFieldAssign>().Where<PXDataFieldAssign>((System.Func<PXDataFieldAssign, bool>) (x => x.ValueType == PXDbType.DirectExpression)).ToDictionary<PXDataFieldAssign, string>((System.Func<PXDataFieldAssign, string>) (x => x.Column.Name));
        foreach ((PXDataFieldAssign Parameter, string SourceColumn) evaluateParameter in this._needEvaluateParameters)
        {
          object newValue = dictionary2[evaluateParameter.Parameter.Column.Name];
          PXDataFieldAssign pxDataFieldAssign = dictionary3[evaluateParameter.Parameter.Column.Name];
          this.LogEvaluateInfo(pxDataFieldAssign.Column.Name, pxDataFieldAssign.Value, newValue);
          pxDataFieldAssign.NewValue = newValue?.ToString();
          pxDataFieldAssign.Value = newValue;
        }
      }
      this._needEvaluateParameters = Enumerable.Empty<(PXDataFieldAssign, string)>();
      return true;
    }
  }
}
