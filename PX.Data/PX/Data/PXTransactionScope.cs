// Decompiled with JetBrains decompiler
// Type: PX.Data.PXTransactionScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using CommonServiceLocator;
using PX.Api;
using PX.Common;
using PX.Data.Automation;
using PX.Data.DeletedRecordsTracking;
using PX.Data.DependencyInjection;
using PX.Data.PushNotifications;
using PX.Data.PushNotifications.Statistics;
using PX.Data.UserRecords;
using PX.Licensing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Messaging;
using System.Reflection;
using System.Runtime.InteropServices;

#nullable disable
namespace PX.Data;

/// <summary>
/// This class allows you to implement some logic within a transaction.
/// If a transaction cannot be completed, the performed actions will be rolled back.
/// </summary>
/// <remarks><para>We recommend that you create a <tt>PXTransactionScope</tt> object in
/// a <tt>using</tt> statement, implement in the <tt>using</tt> body the logic
/// that is necessary for a transaction, and call the <tt>Complete</tt> method
/// as the last command of the <tt>using</tt> body.</para>
///   <para>If you nest a transaction scope into another,
/// the inner and outer transaction scopes will use the same database transaction. If the
/// <tt>Complete</tt> method of the outer transaction scope is not executed, the transaction
/// will be rolled back.</para>
/// </remarks>
/// <example>
/// <code description="Following is the usage pattern for the &lt;tt&gt;PXTransactionScope&lt;/tt&gt; class." lang="CS">
/// using(var ts = new PXTransactionScope())
/// {
///     ...
///     ts.Complete();
/// }
/// </code></example>
public class PXTransactionScope : IDisposable
{
  private readonly IPrimaryNotificationQueueWriter _notificationWriter;
  private readonly IPushNotificationDefinitionProvider _pushNotificationDefinitionProvider;
  private readonly PXWorkflowService _workflowService;
  private readonly IDeletedRecordsTrackingService _deletedRecordsTrackingService;
  private IPrimaryQueueStatisticAggregator _queueStatisticAggregator;
  private readonly Queue<(IQueueEvent @event, List<PrimaryQueueInMessageMetadata> sources)> _transactionQueue = new Queue<(IQueueEvent, List<PrimaryQueueInMessageMetadata>)>(20);
  private readonly Queue<string> _commerceTransactionQueue = new Queue<string>(20);
  private readonly HashSet<PXTransactionScope.ErpTranInfo> _erpTransactionSet = new HashSet<PXTransactionScope.ErpTranInfo>();
  private readonly HashSet<DeleteTranInfo> _deleteTransactionSet = new HashSet<DeleteTranInfo>();
  private DbTransaction _Transaction;
  private bool _Complete;
  private bool _RolledBack;
  private bool _RolledBackWithException;
  private bool _Disposed;
  private PXTransactionScope _Previous;
  private PXConnectionScope _AutoConnection;
  private readonly IUserRecordsDBUpdater _userRecordsBatchUpdater;
  /// <summary>
  /// If the transaction scope is enlisted then it's not the top most transaction scope.
  /// </summary>
  private bool _Enlisted;
  private object _Identity;
  private bool _UpdateVisibility;
  private int _SharedInsert;
  private bool _SharedDelete;
  private System.Type _InsertedTable;
  private Decimal? _InsertedIdentity;
  private bool _ForceRollback;
  private bool _SuppressWorkflow;
  private PXTransactionScope.Slot _AnyActivity;
  private PXConnectionScope _OuterConnectionScope;
  private System.DateTime _ServerTime;
  private System.DateTime _ServerUtcTime;
  private List<PXDatabase.AuditTable> _Audit;
  private Dictionary<(Guid NoteID, System.Type EntityType), ModifiedDacEntryForUserRecordsUpdate> _entriesForUserRecordsUpdate;
  private HashSet<System.Type> _WatchDog;
  private bool _SkipWatchDog;
  private readonly Guid _UID = Guid.NewGuid();
  private Dictionary<System.Type, PXDBOperation> _primalOperation = new Dictionary<System.Type, PXDBOperation>();
  private bool _skipMainTableEvent;
  private readonly IDictionary<string, HashSet<string>> _changedTranslationFields = (IDictionary<string, HashSet<string>>) new Dictionary<string, HashSet<string>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

  internal IPXLicensePolicy LicensePolicy { get; }

  private PXTransactionScope TopMostParent
  {
    get
    {
      PXTransactionScope topMostParent = this;
      while (topMostParent._Previous != null)
        topMostParent = topMostParent._Previous;
      return topMostParent;
    }
  }

  internal Guid _RootUID { get; }

  public bool HasParent => this._Previous != null;

  private static PXTransactionScope TopMostTransaction
  {
    get => PXContext.GetSlot<PXTransactionScope>()?.TopMostParent;
  }

  internal static Guid? UID => PXContext.GetSlot<PXTransactionScope>()?._UID;

  internal static Guid? RootUID => PXContext.GetSlot<PXTransactionScope>()?._RootUID;

  internal static void CheckZombieTransaction(PXTransactionScope scope)
  {
    if (scope != null && scope._Transaction != null && scope._Transaction.Connection == null)
      throw PXDatabaseException.GenericCriticalException("Your last operation cannot be completed because the database server closed the connection unexpectedly. The results of the operation are lost. Please repeat this operation.");
  }

  internal static void CheckActiveReader(IDbTransaction transaction)
  {
    if (PXDatabase.Provider.HasActiveReader(transaction?.Connection))
      throw PXDatabaseException.GenericCriticalException("The connection is busy. The current DB reader is already in use.");
  }

  internal static void EnsurePrimalOperation(PXDBOperation operation, System.Type table)
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    if (slot == null)
      return;
    int orAdd = (int) slot._primalOperation.GetOrAdd<System.Type, PXDBOperation>(table, operation);
  }

  internal static PXDBOperation? GetPrimalOperation(System.Type table)
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    PXDBOperation pxdbOperation;
    return slot != null && slot._primalOperation.TryGetValue(table, out pxdbOperation) ? new PXDBOperation?(pxdbOperation) : new PXDBOperation?();
  }

  internal static void SendDeletedRecord(System.Type table, PXDataFieldParam[] parameters)
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    if (slot == null || !slot._deletedRecordsTrackingService.ContainsTable(table))
      return;
    DeleteTranInfo track = slot._deletedRecordsTrackingService.PrepareToTrack(table, parameters);
    slot?._deleteTransactionSet.Add(track);
  }

  internal static void SendNotification(QueueEvent[] notificationEvents, System.Type table)
  {
    string name = table.Name;
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    List<PrimaryQueueInMessageMetadata> sources = new List<PrimaryQueueInMessageMetadata>();
    if (slot?._pushNotificationDefinitionProvider != null && (slot != null ? (slot.ShouldTriggerPushNotification(slot._pushNotificationDefinitionProvider, name, notificationEvents, out sources) ? 1 : 0) : 1) != 0)
    {
      foreach (QueueEvent notificationEvent in notificationEvents)
      {
        if (slot != null && slot._skipMainTableEvent)
          slot._skipMainTableEvent = false;
        else
          slot._transactionQueue.Enqueue(((IQueueEvent) notificationEvent, sources));
      }
    }
    else if (slot != null && slot._skipMainTableEvent)
      slot._skipMainTableEvent = false;
    slot?._primalOperation.Remove(table);
  }

  internal static IEnumerable<string> GetColumnsForAdditionalSelect(
    string tableName,
    IEnumerable<string> tableColumns)
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    if (slot?._pushNotificationDefinitionProvider != null && slot._pushNotificationDefinitionProvider.ContainsTable(tableName, out string[] _))
    {
      foreach (string tableColumn in tableColumns)
        yield return tableColumn;
    }
  }

  private bool ShouldTriggerPushNotification(
    IPushNotificationDefinitionProvider provider,
    string tableName,
    QueueEvent[] events,
    out List<PrimaryQueueInMessageMetadata> sources)
  {
    HashSet<string> translationFields = this._changedTranslationFields.GetOrAdd<string, HashSet<string>>(tableName, new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase));
    foreach (QueueEvent queueEvent in events)
    {
      foreach (QueueEvent.Field field in ((IEnumerable<QueueEvent.Field>) queueEvent.Fields).Where<QueueEvent.Field>((System.Func<QueueEvent.Field, bool>) (f => translationFields.Contains(f.FieldName))))
        field.IsChanged = true;
    }
    return provider.ShouldTrigger(tableName, events, out sources);
  }

  internal static bool IsPushNotificationSetupForTable(string tableName)
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    return slot != null && slot._pushNotificationDefinitionProvider.ContainsTable(tableName, out string[] _);
  }

  internal static void SendIdentity(string fieldName, string tableName, object value)
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    if (slot == null || !slot._pushNotificationDefinitionProvider.ContainsTable(tableName, out string[] _) || slot == null)
      return;
    slot._transactionQueue.Enqueue(((IQueueEvent) new IdentityEvent(fieldName, tableName, value), (List<PrimaryQueueInMessageMetadata>) null));
  }

  internal static void RegisterCommerceTransaction(string tableName)
  {
    System.Type slot = PXContext.GetSlot<System.Type>("PXProjectionCommerceTranType");
    if (!(slot == (System.Type) null) && !(slot.Name == tableName))
      return;
    PXContext.GetSlot<PXTransactionScope>()?._commerceTransactionQueue.Enqueue(tableName);
  }

  internal static bool EnlistErpTransaction(PXTransactionScope.ErpTranInfo erpInfo)
  {
    PXTrace.Logger.Debug<string>("EnlistErpTransaction(): {ErpTranStringified}", erpInfo.ToString());
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    bool flag1 = false;
    if (slot == null)
      return flag1;
    PXTransactionScope topMostParent = slot.TopMostParent;
    erpInfo.TopmostTransactionScopeUID = topMostParent._UID;
    PXTransactionScope.ErpTranInfo actualValue;
    bool flag2;
    if (topMostParent._erpTransactionSet.TryGetValue(erpInfo, out actualValue))
    {
      PXTrace.Logger.Debug<string, string>("EnlistErpTransaction(): merge ErpTran2 => ErpTran1: [ErpTran2: {ErpTran2}]=>[ErpTran1: {ErpTran1}]", erpInfo.ToString(), actualValue.ToString());
      flag2 = actualValue.MergeErpTranInfo(erpInfo);
      if (flag2)
      {
        topMostParent._erpTransactionSet.Remove(erpInfo);
        topMostParent._erpTransactionSet.Add(actualValue);
      }
    }
    else
    {
      PXTrace.Logger.Debug<string>("EnlistErpTransaction(): add ErpTran to TranScope: {ErpTranStringified}", erpInfo.ToString());
      topMostParent._erpTransactionSet.Add(erpInfo);
      flag2 = true;
    }
    return flag2;
  }

  internal void UpdateCommerceTransactions()
  {
    try
    {
      foreach (string commerceTransaction in this._commerceTransactionQueue)
        this.LicensePolicy.OnDataRowInserted(commerceTransaction);
    }
    catch
    {
    }
  }

  /// <exclude />
  public PXTransactionScope()
  {
    ILifetimeScope lifetimeScope = LifetimeScopeHelper.GetLifetimeScope();
    if (lifetimeScope != null)
    {
      this._notificationWriter = ResolutionExtensions.Resolve<IPrimaryNotificationQueueWriter>((IComponentContext) lifetimeScope);
      this._pushNotificationDefinitionProvider = ResolutionExtensions.ResolveKeyed<IPushNotificationDefinitionProvider>((IComponentContext) lifetimeScope, (object) "Cached");
      this.LicensePolicy = ResolutionExtensions.Resolve<IPXLicensePolicy>((IComponentContext) lifetimeScope);
      this._workflowService = ResolutionExtensions.Resolve<PXWorkflowService>((IComponentContext) lifetimeScope);
      this._userRecordsBatchUpdater = ResolutionExtensions.Resolve<IUserRecordsDBUpdater>((IComponentContext) lifetimeScope);
      this._queueStatisticAggregator = ResolutionExtensions.Resolve<IPrimaryQueueStatisticAggregator>((IComponentContext) lifetimeScope);
      this._deletedRecordsTrackingService = ResolutionExtensions.Resolve<IDeletedRecordsTrackingService>((IComponentContext) lifetimeScope);
    }
    else
    {
      this._notificationWriter = (IPrimaryNotificationQueueWriter) new DummyPrimaryNotificationQueue();
      this._pushNotificationDefinitionProvider = (IPushNotificationDefinitionProvider) new DummyPushNotificationDefinitionProvider();
      this._userRecordsBatchUpdater = (IUserRecordsDBUpdater) new DummyUserRecordsDBUpdater();
      this._queueStatisticAggregator = (IPrimaryQueueStatisticAggregator) new DummyPrimaryQueueStatisticAggregator();
      this._deletedRecordsTrackingService = (IDeletedRecordsTrackingService) new DummyDeletedRecordsTrackingService();
      if (ServiceLocator.IsLocationProviderSet)
      {
        this.LicensePolicy = ServiceLocator.Current.GetInstance<IPXLicensePolicy>() ?? (IPXLicensePolicy) LicensingManager.DummyLicensePolicy;
        this._workflowService = ServiceLocator.Current.GetInstance<PXWorkflowService>();
      }
      else
        this.LicensePolicy = (IPXLicensePolicy) LicensingManager.DummyLicensePolicy;
    }
    this._Previous = PXContext.GetSlot<PXTransactionScope>();
    if (this._Previous != null)
    {
      this._AnyActivity = this._Previous._AnyActivity;
      this._RootUID = this._Previous._RootUID;
      this._RolledBack = this._Previous._RolledBack;
      this._RolledBackWithException = this._Previous._RolledBackWithException;
      this._RootUID = this._Previous._RootUID;
    }
    else
    {
      this._AnyActivity = new PXTransactionScope.Slot();
      this._RootUID = this._UID;
    }
    this._OuterConnectionScope = PXContext.GetSlot<PXConnectionScope>();
    PXContext.SetSlot<PXTransactionScope>(this);
    PXDatabase.SelectDate(out this._ServerTime, out this._ServerUtcTime);
  }

  internal PXTransactionScope(bool skipWatchDog)
    : this()
  {
    this._SkipWatchDog = skipWatchDog;
  }

  /// <summary>Completes the transaction.</summary>
  /// <remarks>Run this method after all other operations within a transaction,
  /// but before the transaction scope object is destroyed.</remarks>
  public void Complete() => this.Complete((PXGraph) null);

  /// <summary>Completes the transaction.</summary>
  /// <remarks>Run this method after all other operations within a transaction,
  /// but before the transaction scope object is destroyed.</remarks>
  /// <param name="graph">If not <tt>null</tt>, the timestamp value is stored for
  /// this graph.</param>
  public virtual void Complete(PXGraph graph)
  {
    if (this._Transaction != null)
    {
      Dictionary<(Guid, System.Type), ModifiedDacEntryForUserRecordsUpdate> userRecordsUpdate = this._entriesForUserRecordsUpdate;
      // ISSUE: explicit non-virtual call
      if ((userRecordsUpdate != null ? (__nonvirtual (userRecordsUpdate.Count) > 0 ? 1 : 0) : 0) != 0 && !this._Enlisted)
        this._userRecordsBatchUpdater.UpdateUserRecords((IReadOnlyCollection<ModifiedDacEntryForUserRecordsUpdate>) this._entriesForUserRecordsUpdate.Values);
      if (this._WatchDog != null && !this._Enlisted)
      {
        PXDatabase.SaveWatchDog((IEnumerable<System.Type>) this._WatchDog);
        foreach (MemberInfo memberInfo in this._WatchDog)
          PXDatabase.TableChangedLocal(memberInfo.Name);
      }
      this._Complete = true;
      graph?.SelectTimeStamp();
    }
    if (this._Previous == null)
      return;
    foreach ((IQueueEvent @event, List<PrimaryQueueInMessageMetadata> sources) transaction in this._transactionQueue)
      this._Previous._transactionQueue.Enqueue(transaction);
    foreach (string commerceTransaction in this._commerceTransactionQueue)
      this._Previous._commerceTransactionQueue.Enqueue(commerceTransaction);
    if (!this._Enlisted)
      return;
    foreach (DeleteTranInfo deleteTransaction in this._deleteTransactionSet)
      this._Previous._deleteTransactionSet.Add(deleteTransaction);
  }

  /// <summary>Clears the active scope and all its parent scopes.</summary>
  public static void Restart()
  {
    PXTransactionScope transactionScope = PXContext.GetSlot<PXTransactionScope>();
    DbTransaction transaction = transactionScope?._Transaction;
    if (transactionScope != null && transactionScope._workflowService != null)
    {
      transactionScope._workflowService.RestoreActionData();
      transactionScope._workflowService.RestoreLongRunActionData();
      transactionScope._workflowService.RestorePrimaryWorkflowItemPersisted();
    }
    for (; transactionScope != null; transactionScope = transactionScope._Previous)
    {
      if (transactionScope._Transaction != null && transactionScope._Transaction == transaction)
      {
        transactionScope._Transaction.Dispose();
        transactionScope._Transaction = (DbTransaction) null;
      }
      transactionScope._Enlisted = false;
      transactionScope._RolledBack = false;
      transactionScope._RolledBackWithException = false;
      if (transactionScope._AutoConnection != null)
      {
        transactionScope._AutoConnection.Dispose();
        transactionScope._AutoConnection = (PXConnectionScope) null;
      }
      if (transactionScope._OuterConnectionScope != null && transactionScope._OuterConnectionScope._Connection != null)
      {
        transactionScope._OuterConnectionScope._Connection.Dispose();
        transactionScope._OuterConnectionScope._Connection = (DbConnection) null;
      }
    }
  }

  /// <summary>
  /// This method performs the <tt>COMMIT</tt> operation if the <tt>Complete</tt>
  /// method was called; otherwise, the <tt>ROLLBACK</tt> operation is performed.
  /// </summary>
  public void Dispose()
  {
    if (this._Disposed)
      return;
    try
    {
      PXContext.SetSlot<PXTransactionScope>(this._Previous);
      if (this._Transaction == null)
        return;
      if (this._Transaction.Connection != null)
      {
        if (this._Complete)
        {
          if (this._Enlisted)
            return;
          if (!this._ForceRollback)
          {
            try
            {
              PXBaseRedirectException redirectException = (PXBaseRedirectException) null;
              if (!this._SuppressWorkflow)
              {
                if (PXConnectionScope.IsTopMost())
                {
                  try
                  {
                    PXContext.SetSlot<PXTransactionScope>(this);
                    this._workflowService?.CompleteOperation(this._RootUID);
                  }
                  catch (PXBaseRedirectException ex)
                  {
                    redirectException = ex;
                  }
                  catch (Exception ex)
                  {
                    PXContext.SetSlot<PXTransactionScope>(this._Previous);
                    if (this._Transaction?.Connection != null)
                      this.PerformRollback();
                    throw;
                  }
                  finally
                  {
                    PXContext.SetSlot<PXTransactionScope>(this._Previous);
                  }
                }
              }
              bool flag = true;
              if (this._Previous == null && this.OnBeforeCommitImpl != null)
              {
                CancelEventArgs cancelEventArgs = new CancelEventArgs();
                try
                {
                  PXContext.SetSlot<PXTransactionScope>(this);
                  this.OnBeforeCommitImpl(cancelEventArgs);
                  this.OnBeforeCommitImpl = (System.Action<CancelEventArgs>) null;
                }
                finally
                {
                  PXContext.SetSlot<PXTransactionScope>(this._Previous);
                }
                if (cancelEventArgs.Cancel)
                {
                  flag = false;
                  this.PerformRollback();
                }
              }
              if (flag)
              {
                if (this._Audit != null)
                {
                  if (!this._Enlisted)
                  {
                    try
                    {
                      PXContext.SetSlot<PXTransactionScope>(this);
                      PXDatabase.Provider.SaveAudit(this._Audit);
                    }
                    catch (Exception ex)
                    {
                      PXContext.SetSlot<PXTransactionScope>(this._Previous);
                      if (this._Transaction?.Connection != null)
                        this.PerformRollback();
                      throw;
                    }
                    finally
                    {
                      PXContext.SetSlot<PXTransactionScope>(this._Previous);
                    }
                  }
                }
                int num = this.PrepareToCommitToQueue() ? 1 : 0;
                PXDatabase.Commit((IDbTransaction) this._Transaction);
                if (this._deleteTransactionSet.Any<DeleteTranInfo>())
                  this._deletedRecordsTrackingService.SaveHistory((IEnumerable<DeleteTranInfo>) this._deleteTransactionSet);
                if (num != 0)
                  this._notificationWriter.CommitTransaction(this._RootUID, PXAccess.GetConnectionString());
              }
              this.UpdateCommerceTransactions();
              try
              {
                foreach (PXTransactionScope.ErpTranInfo erpTransaction in this._erpTransactionSet)
                  this.LicensePolicy.RegisterErpTransactionToPersistenceQueue(erpTransaction, out PXTransactionScope.ErpTranInfo _);
              }
              catch
              {
              }
              if (redirectException != null)
                throw redirectException;
            }
            catch (Exception ex)
            {
              if (!(ex is MessageQueueException))
                this._notificationWriter.RollbackTransaction(this._RootUID);
              throw;
            }
          }
          else
            this.PerformRollback();
        }
        else
        {
          this.PerformRollback();
          if (!this._Enlisted)
            return;
          bool flag = Marshal.GetExceptionPointers() != IntPtr.Zero || Marshal.GetExceptionCode() != 0;
          for (PXTransactionScope previous = this._Previous; previous != null; previous = previous._Previous)
          {
            previous._RolledBack = true;
            previous._RolledBackWithException = flag;
            if (this._Transaction == previous._Transaction)
            {
              previous._Transaction.Dispose();
              previous._Transaction = (DbTransaction) null;
            }
            if (!previous._Enlisted)
              break;
          }
        }
      }
      else
      {
        for (PXTransactionScope previous = this._Previous; previous != null; previous = previous._Previous)
        {
          previous._RolledBack = true;
          previous._RolledBackWithException = true;
          if (!previous._Enlisted)
            break;
        }
      }
    }
    finally
    {
      if (this._AutoConnection != null)
        this._AutoConnection.Dispose();
      this._Disposed = true;
      if (!this._Enlisted)
      {
        try
        {
          this._Transaction?.Dispose();
        }
        catch
        {
        }
        PXDatabase.TransactionFinished();
      }
    }
  }

  private void PerformRollback()
  {
    this._notificationWriter.RollbackTransaction(this._RootUID);
    PXDatabase.Rollback((IDbTransaction) this._Transaction);
    if (this._workflowService == null)
      return;
    this._workflowService.ClearActionData();
    this._workflowService.ClearLongRunActionData();
    this._workflowService.ClearPrimaryWorkflowItemPersisted();
    this._workflowService.ClearMassProcessingWorkflowObjectKeys();
  }

  private bool PrepareToCommitToQueue()
  {
    if (this._Previous != null || this._transactionQueue.Count == 0)
      return false;
    this._notificationWriter.BeginTransaction(this._RootUID);
    foreach ((IQueueEvent @event, List<PrimaryQueueInMessageMetadata> sources) transaction in this._transactionQueue)
    {
      IQueueEvent queueEvent = transaction.@event;
      this._queueStatisticAggregator.SendInMessageMetadata((IEnumerable<PrimaryQueueInMessageMetadata>) transaction.sources, this._RootUID);
      if (queueEvent is QueueEvent messageData)
        this._notificationWriter.Send(messageData, this._RootUID);
      else if (queueEvent is IdentityEvent identityEvent)
        this._notificationWriter.SendIdentity(identityEvent.FieldName, identityEvent.TableName, identityEvent.GenericValue, this._RootUID);
    }
    this._notificationWriter.ReadyToCommitTransaction(this._RootUID);
    return true;
  }

  /// <summary>
  /// Returns <tt>true</tt>, if a transaction scope is set. Otherwise, it returns <tt>false</tt>.
  /// </summary>
  public static bool IsScoped => PXContext.GetSlot<PXTransactionScope>() != null;

  /// <summary>
  /// Returns true if a transaction scope is set but the current connection was created in a separate PXConnectionScope.
  /// </summary>
  [PXInternalUseOnly]
  public static bool IsConnectionOutOfScope
  {
    get
    {
      PXConnectionScope slot1 = PXContext.GetSlot<PXConnectionScope>();
      PXTransactionScope slot2 = PXContext.GetSlot<PXTransactionScope>();
      return slot1 != null && slot2 != null && slot1._Connection != slot2._Transaction.Connection;
    }
  }

  internal static bool RetryAllowed
  {
    get
    {
      PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
      return slot == null || !slot._AnyActivity.Value;
    }
  }

  /// <exclude />
  public static void AddTranslationField(string bqlTableName, string fieldName)
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    if (slot == null)
      return;
    slot._changedTranslationFields.GetOrAdd<string, HashSet<string>>(bqlTableName, new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)).Add(fieldName);
  }

  public static void RemoveTranslationField(string bqlTableName, string fieldName)
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    if (slot == null)
      return;
    slot._changedTranslationFields.GetOrAdd<string, HashSet<string>>(bqlTableName, new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)).Remove(fieldName);
  }

  /// <exclude />
  public static void SetSkipMainTableEvent()
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    if (slot == null)
      return;
    slot._skipMainTableEvent = true;
  }

  internal static void RegisterActivity()
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    if (slot == null)
      return;
    if (slot._RolledBack)
    {
      if (!slot._RolledBackWithException || slot._Transaction != null)
        throw new PXException("The transaction has been silently rolled back before a database update operation.");
      if ((Marshal.GetExceptionPointers() != IntPtr.Zero ? 1 : (Marshal.GetExceptionCode() != 0 ? 1 : 0)) == 0)
        throw new PXException("The transaction has been silently rolled back before a database update operation.");
    }
    PXTransactionScope.CheckZombieTransaction(slot);
    PXTransactionScope.CheckActiveReader((IDbTransaction) slot._Transaction);
    slot._AnyActivity.Value = true;
  }

  internal static DbTransaction GetTransaction()
  {
    PXTransactionScope scope = PXContext.GetSlot<PXTransactionScope>();
    if (scope == null)
      return (DbTransaction) null;
    PXTransactionScope.CheckZombieTransaction(scope);
    IDbConnection connection = (IDbConnection) PXConnectionScope.GetConnection();
    if (scope._Transaction != null && connection != null && scope._Transaction.Connection != connection)
      return (DbTransaction) null;
    PXTransactionScope.CheckActiveReader((IDbTransaction) scope._Transaction);
    if (scope._Transaction == null)
    {
      PXConnectionScope slot = PXContext.GetSlot<PXConnectionScope>();
      if (slot == null)
      {
        scope._AutoConnection = new PXConnectionScope();
      }
      else
      {
        PXTransactionScope previous = scope._Previous;
        while (previous != null && previous._Transaction == null)
          previous = previous._Previous;
        if (previous != null && previous._Transaction != null && previous._Transaction.Connection == slot.Connection)
        {
          scope._Transaction = previous._Transaction;
          scope._Enlisted = true;
        }
      }
      if (!scope._Enlisted)
      {
        PXTransactionScope transactionScope = scope;
        while (scope != null && scope._OuterConnectionScope != slot)
          scope = scope._Previous;
        if (scope == null)
          return (DbTransaction) null;
        for (scope._Transaction = PXDatabase.CreateTransaction(); scope._Transaction != null && transactionScope._Transaction == null && scope != transactionScope; transactionScope = transactionScope._Previous)
        {
          transactionScope._Transaction = scope._Transaction;
          transactionScope._Enlisted = true;
        }
        for (; scope._Previous != null && scope._Previous._Transaction == null; scope = scope._Previous)
        {
          scope._Previous._Transaction = scope._Transaction;
          scope._Previous._AutoConnection = scope._AutoConnection;
          scope._AutoConnection = (PXConnectionScope) null;
          scope._Enlisted = true;
        }
      }
      PXTransactionScope.CheckZombieTransaction(scope);
      PXTransactionScope.CheckActiveReader((IDbTransaction) scope._Transaction);
    }
    return scope._Transaction;
  }

  internal static object GetIdentity() => PXContext.GetSlot<PXTransactionScope>()?._Identity;

  internal static bool SetIdentity(object identity)
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    if (slot == null || identity == null)
      return false;
    slot._Identity = identity;
    return true;
  }

  internal static void ClearIdentity()
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    if (slot == null)
      return;
    slot._Identity = (object) null;
  }

  internal static bool GetVisibilityUpdate()
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    return slot != null && slot._UpdateVisibility;
  }

  internal static bool SetVisibilityUpdate()
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    if (slot == null)
      return false;
    slot._UpdateVisibility = true;
    return true;
  }

  internal static void ClearVisibilityUpdate()
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    if (slot == null)
      return;
    slot._UpdateVisibility = false;
  }

  internal static int GetSharedInsert()
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    return slot == null ? 0 : slot._SharedInsert;
  }

  internal static int SetSharedInsert(int level)
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    return slot == null ? 0 : (slot._SharedInsert = level);
  }

  internal static void ClearSharedInsert()
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    if (slot == null)
      return;
    slot._SharedInsert = 0;
  }

  internal static bool GetSharedDelete()
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    return slot != null && slot._SharedDelete;
  }

  internal static bool SetSharedDelete()
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    if (slot == null)
      return false;
    slot._SharedDelete = true;
    return true;
  }

  internal static void ClearSharedDelete()
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    if (slot == null)
      return;
    slot._SharedDelete = false;
  }

  internal static System.Type GetInsertedTable()
  {
    return PXContext.GetSlot<PXTransactionScope>()?._InsertedTable;
  }

  internal static Decimal? GetInsertedIdentity()
  {
    return PXContext.GetSlot<PXTransactionScope>()?._InsertedIdentity;
  }

  internal static bool SetInsertedTable(System.Type table, Decimal? identity = null)
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    if (slot == null)
      return false;
    slot._InsertedTable = table;
    slot._InsertedIdentity = identity;
    return true;
  }

  internal static bool GetSkipWatchdog()
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    return slot != null && slot._SkipWatchDog;
  }

  internal static bool? GetSuppressWorkflow()
  {
    return PXContext.GetSlot<PXTransactionScope>()?._SuppressWorkflow;
  }

  [PXInternalUseOnly]
  public static bool SetSuppressWorkflow(bool suppressWorkflow)
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    if (slot == null)
      return false;
    slot._SuppressWorkflow = suppressWorkflow;
    return true;
  }

  internal static void ForceRollbackParentTransaction()
  {
    for (PXTransactionScope transactionScope = PXContext.GetSlot<PXTransactionScope>(); transactionScope != null; transactionScope = transactionScope._Previous)
      transactionScope._ForceRollback = true;
  }

  internal static System.DateTime? GetServerDateTime(bool utc)
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    return slot == null ? new System.DateTime?() : new System.DateTime?(utc ? slot._ServerUtcTime : slot._ServerTime);
  }

  internal static bool AddChangedDacEntryForUserRecordsModification(
    Guid noteID,
    System.Type entityType,
    string cachedContent,
    DacModificationType modificationType)
  {
    Dictionary<(Guid, System.Type), ModifiedDacEntryForUserRecordsUpdate> dictionary = PXTransactionScope.EnsureMember<Dictionary<(Guid, System.Type), ModifiedDacEntryForUserRecordsUpdate>>((System.Func<PXTransactionScope, Dictionary<(Guid, System.Type), ModifiedDacEntryForUserRecordsUpdate>>) (tranScope => tranScope._entriesForUserRecordsUpdate), (Action<PXTransactionScope, Dictionary<(Guid, System.Type), ModifiedDacEntryForUserRecordsUpdate>>) ((tranScope, value) => tranScope._entriesForUserRecordsUpdate = value));
    if (dictionary == null)
      return false;
    (Guid, System.Type) key = (noteID, entityType);
    ModifiedDacEntryForUserRecordsUpdate userRecordsUpdate;
    if (dictionary.TryGetValue(key, out userRecordsUpdate))
    {
      userRecordsUpdate.Update(modificationType, cachedContent);
    }
    else
    {
      userRecordsUpdate = new ModifiedDacEntryForUserRecordsUpdate(modificationType, noteID, entityType, cachedContent);
      dictionary.Add(key, userRecordsUpdate);
    }
    return true;
  }

  internal static void SetIdentityAudit(string tableName, string identityName, Decimal? identity)
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    if (slot == null || slot._Audit == null || slot._Audit.Count <= 0 || !string.Equals(slot._Audit[slot._Audit.Count - 1].TableName, tableName, StringComparison.OrdinalIgnoreCase))
      return;
    PXDatabase.AuditTable auditTable = slot._Audit[slot._Audit.Count - 1];
    auditTable.IdentityName = identityName;
    auditTable.Identity = identity;
  }

  private static T EnsureMember<T>(
    System.Func<PXTransactionScope, T> getMember,
    Action<PXTransactionScope, T> setMember)
    where T : class, new()
  {
    PXTransactionScope slot = PXContext.GetSlot<PXTransactionScope>();
    if (slot == null)
      return default (T);
    if ((object) getMember(slot) == null)
    {
      PXTransactionScope transactionScope = slot;
      while (transactionScope != null && transactionScope._Enlisted && (object) getMember(transactionScope) == null)
        transactionScope = transactionScope._Previous;
      if (transactionScope != null && (object) getMember(transactionScope) != null)
        setMember(slot, getMember(transactionScope));
      else
        setMember(slot, new T());
      PXTransactionScope previous = slot._Previous;
      if (slot._Enlisted)
      {
        for (; previous != null && (object) getMember(previous) == null; previous = previous._Previous)
        {
          setMember(previous, getMember(slot));
          if (!previous._Enlisted)
            break;
        }
      }
    }
    return getMember(slot);
  }

  internal static PXDatabase.AuditTable GetAudit(string operation, string tableName)
  {
    List<PXDatabase.AuditTable> auditTableList = PXTransactionScope.EnsureMember<List<PXDatabase.AuditTable>>((System.Func<PXTransactionScope, List<PXDatabase.AuditTable>>) (s => s._Audit), (Action<PXTransactionScope, List<PXDatabase.AuditTable>>) ((s, v) => s._Audit = v));
    if (auditTableList == null)
      return (PXDatabase.AuditTable) null;
    PXDatabase.AuditTable audit;
    auditTableList.Add(audit = new PXDatabase.AuditTable(operation, tableName));
    return audit;
  }

  /// <exclude />
  [PXInternalUseOnly]
  public static void TableModified(System.Type table)
  {
    if (PXTransactionScope.GetSkipWatchdog())
      return;
    PXDatabase.TableChangedLocal(table.Name);
    HashSet<System.Type> typeSet = PXTransactionScope.EnsureMember<HashSet<System.Type>>((System.Func<PXTransactionScope, HashSet<System.Type>>) (s => s._WatchDog), (Action<PXTransactionScope, HashSet<System.Type>>) ((s, v) => s._WatchDog = v));
    if (typeSet == null)
      PXDatabase.SaveWatchDog((IEnumerable<System.Type>) new HashSet<System.Type>()
      {
        table
      });
    else
      typeSet.Add(table);
  }

  private event System.Action<CancelEventArgs> OnBeforeCommitImpl;

  /// <summary>
  /// The event is raised for the top most transaction before the <tt>COMMIT</tt>
  /// operation. Using this event, you can perform the <tt>ROLLBACK</tt> operation
  /// instead of the <tt>COMMIT</tt> operation.
  /// </summary>
  public static event System.Action<CancelEventArgs> OnBeforeCommit
  {
    add
    {
      PXTransactionScope topMostTransaction = PXTransactionScope.TopMostTransaction;
      if (topMostTransaction == null)
        throw new PXInvalidOperationException("No transaction is available.");
      topMostTransaction.OnBeforeCommitImpl += value;
    }
    remove
    {
      PXTransactionScope topMostTransaction = PXTransactionScope.TopMostTransaction;
      if (topMostTransaction == null)
        throw new PXInvalidOperationException("No transaction is available.");
      topMostTransaction.OnBeforeCommitImpl -= value;
    }
  }

  private class Slot
  {
    public bool Value;
  }

  [PXInternalUseOnly]
  public struct ErpTranInfo : IEquatable<PXTransactionScope.ErpTranInfo>
  {
    internal Guid TopmostTransactionScopeUID;
    internal string Screen;
    internal string Type;
    internal string ItemType;
    internal HashSet<string> DbKey;
    internal string CommandName;
    internal string CommandType;
    internal System.DateTime Timesamp;
    private const int MaxDbKeyCountToLog_Debug = 10;

    public bool Equals(PXTransactionScope.ErpTranInfo other)
    {
      return this.TopmostTransactionScopeUID == other.TopmostTransactionScopeUID && this.Screen == other.Screen && this.Type == other.Type && this.ItemType == other.ItemType;
    }

    public override bool Equals(object obj)
    {
      return obj is PXTransactionScope.ErpTranInfo other && this.Equals(other);
    }

    public override int GetHashCode()
    {
      return this.TopmostTransactionScopeUID.GetHashCode() ^ this.Screen.GetHashCode() ^ this.Type.GetHashCode() ^ this.ItemType.GetHashCode();
    }

    public bool MergeErpTranInfo(PXTransactionScope.ErpTranInfo erpTranInfo)
    {
      int count = this.DbKey.Count;
      this.DbKey.UnionWith((IEnumerable<string>) erpTranInfo.DbKey);
      return this.DbKey.Count != count;
    }

    internal string StringifyDbKey(int maxDbKeyCountToLog)
    {
      return string.Join(";", this.AdditionalDbKeysFor(maxDbKeyCountToLog, 0));
    }

    internal IEnumerable<string> AdditionalDbKeysFor(int maxDbKeyCountToLog, int existingCount)
    {
      return this.DbKey.Take<string>(maxDbKeyCountToLog - existingCount);
    }

    public override string ToString()
    {
      return $"Screen={this.Screen};TranType={this.Type};PrimaryItemType={this.ItemType};DbKey={this.StringifyDbKey(10)}";
    }
  }
}
