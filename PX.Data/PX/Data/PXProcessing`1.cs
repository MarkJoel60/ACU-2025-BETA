// Decompiled with JetBrains decompiler
// Type: PX.Data.PXProcessing`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Common.Extensions;
using PX.Data.Process;
using PX.DbServices.Commands;
using PX.DbServices.Commands.Data;
using PX.DbServices.Points;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.QueryObjectModel;
using PX.Logging.Sinks.SystemEventsDbSink;
using PX.SM;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Web;

#nullable enable
namespace PX.Data;

/// <summary>
/// <para>Defines a special data view used on processing webpages, which are intended for mass processing of data records.</para>
/// <para>The <tt>PXProcessing&lt;Table&gt;</tt> type is used to define the data view in a graph bound to a processing webpage. A data view of this type includes
/// definitions of two actions, <tt>Process</tt> and <tt>ProcessAll</tt>, which are added to the graph and are used to invoke the processing. You should set the
/// processing method by invoking one of the <tt>SetProcessDelegate</tt> methods in the constructor of the graph.</para>
/// </summary>
/// <example>
/// The code below shows definition of the graph that contains the
/// processing data view.
/// <code title="" description="" lang="CS">
/// public class ARPaymentsProcessing : PXGraph&lt;ARPaymentsProcessing&gt;
/// {
/// // Definition of the data view to process
/// public PXProcessing&lt;ARPaymentInfo&gt; ARDocumentList;
/// // The constructor of the graph
/// public ARPaymentsAutoProcessing()
/// {
/// // Specifying the field to mark data records for processing
/// ARDocumentList.SetSelected&lt;ARPaymentInfo.selected&gt;();
/// // Setting the processing method
/// ARDocumentList.SetProcessDelegate(Process);
/// }
/// // The processing method (must be static)
/// public static void Process(List&lt;ARPaymentInfo&gt; products)
/// {
/// ...
/// }
/// ...
/// }</code></example>
public class PXProcessing<Table> : PXProcessingBase<
#nullable disable
Table> where Table : class, IBqlTable, new()
{
  protected const 
  #nullable enable
  string _ProcessActionKey = "Process";
  protected const string _ProcessAllActionKey = "ProcessAll";
  protected const string _ScheduleActionKey = "Schedule";
  protected const string _ScheduleAddActionKey = "_ScheduleAdd_";
  protected const string _ScheduleViewActionKey = "_ScheduleView_";
  protected const string _ScheduleHistoryActionKey = "_ScheduleHistory_";
  private const string ProcessingStatus = "ProcessingStatus";
  private const string ProcessingMessage = "ProcessingMessage";
  private static readonly Guid FailedFilterID = new Guid(0, (short) 0, (short) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 16 /*0x10*/);
  private static readonly Guid ProcessedFilterID = new Guid(0, (short) 0, (short) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 32 /*0x20*/);
  private static readonly Guid PendingFilterID = new Guid(0, (short) 0, (short) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 48 /*0x30*/);
  private static readonly Guid SkippedFilterID = new Guid(0, (short) 0, (short) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 64 /*0x40*/);
  protected 
  #nullable disable
  PXAction _ProcessButton;
  protected PXAction _ProcessAllButton;
  protected PXAction _ScheduleButton;
  protected PXAction _ScheduleAddButton;
  protected PXAction _ScheduleViewButton;
  protected PXAction _ScheduleHistoryButton;
  private Func<PXCache, object, Guid?> _getRefIdDelegate;
  private string _refIdBqlFieldName;
  public bool DoNotCheckPrevOperation;
  public PXSelect<PXProcessing<Table>.ProcessingFilterHeader> ViewFieldsFilters;

  [InjectDependency]
  private 
  #nullable enable
  IScheduleProvider ScheduleProvider { get; set; }

  [InjectDependency]
  private IScheduleAdjustmentRuleProvider AdjustmentRuleProvider { get; set; }

  [InjectDependency]
  private ILogger Logger { get; set; }

  /// <summary>Sets the error message on the data record with the specified
  /// index and specified type.</summary>
  /// <param name="index">The index of the data record marked with
  /// error.</param>
  /// <param name="message">The error message.</param>
  public static bool SetError(int index, string message)
  {
    return PXProcessing.SetError<Table>(index, message);
  }

  /// <summary>Sets the provided string as the error message of the
  /// processing operation for the specified type.</summary>
  /// <param name="message">The error message.</param>
  public static bool SetError(string message) => PXProcessing.SetError<Table>(message);

  /// <summary>Sets the provided exception as the error on the data record
  /// with the specified index and specified type.</summary>
  /// <param name="index">The index of the data record marked with
  /// error.</param>
  /// <param name="e">The exception containing information about the
  /// error.</param>
  public static bool SetError(int index, Exception e) => PXProcessing.SetError<Table>(index, e);

  /// <summary>Sets the provided exception as the error of the processing
  /// operation for the specified type.</summary>
  /// <param name="e">The exception containing information about the
  /// error.</param>
  public static bool SetError(Exception e) => PXProcessing.SetError<Table>(e);

  /// <summary>Sets the warning message on the data record with the
  /// specified index and specified type.</summary>
  /// <param name="index">The index of the data record to which the message
  /// is attached.</param>
  /// <param name="message">The warning message.</param>
  public static bool SetWarning(int index, string message)
  {
    return PXProcessing.SetWarning<Table>(index, message);
  }

  /// <summary>Sets the warning message for the processing
  /// operation for the specified type.</summary>
  /// <param name="message">The warning message.</param>
  public static bool SetWarning(string message) => PXProcessing.SetWarning<Table>(message);

  /// <summary>Attaches the provided exception as the warning-level error to
  /// the data record with the specified index and specified type.</summary>
  /// <param name="index">The index of the data record to which the
  /// exception is attached.</param>
  /// <param name="e">The exception containing warning information.</param>
  public static bool SetWarning(int index, Exception e) => PXProcessing.SetWarning<Table>(index, e);

  /// <summary>Sets the provided exceptiona as the warning-level error of
  /// the processing operation for the specified type.</summary>
  /// <param name="e">The exception containing warning information.</param>
  public static bool SetWarning(Exception e) => PXProcessing.SetWarning<Table>(e);

  /// <summary>Attaches the provided information message to the data record
  /// with the specified index and specified type.</summary>
  /// <param name="index">The index of the data record to which the message
  /// is attached.</param>
  /// <param name="message">The information message.</param>
  public static bool SetInfo(int index, string message)
  {
    return PXProcessing.SetInfo<Table>(index, message);
  }

  /// <summary>Sets the information message for the processing
  /// operation for the specified type.</summary>
  /// <param name="message">The information message.</param>
  public static bool SetInfo(string message) => PXProcessing.SetInfo<Table>(message);

  /// <summary>Attaches the provided exception as the information-level
  /// error to the data record with the specified index and specified type.</summary>
  /// <param name="index">The index of the data record that is marked with
  /// the exception.</param>
  /// <param name="e">The exception containing information.</param>
  public static bool SetInfo(int index, Exception e) => PXProcessing.SetInfo<Table>(index, e);

  /// <summary>Sets the provided exception as the information-level error
  /// for the processing operation for the specified type.</summary>
  /// <param name="e">The exception containing information.</param>
  public static bool SetInfo(Exception e) => PXProcessing.SetInfo<Table>(e);

  /// <summary>Sets the information message confirming that a data record for the specified type
  /// has been processed successfully</summary>
  public static bool SetProcessed() => PXProcessing.SetProcessed<Table>();

  /// <summary>Sets the current data record to process.</summary>
  /// <param name="currentItem">The data record to be set as the
  /// current.</param>
  public static void SetCurrentItem(object currentItem) => PXProcessing.SetCurrentItem(currentItem);

  public static PXProcessingMessage GetItemMessage() => PXProcessing.GetItemMessage<Table>();

  /// <summary>
  /// Processes a sequence of <paramref name="records" /> in the batch mass processing manner
  /// with an ability to report the state of the processing to the mass processing UI.
  /// </summary>
  /// <param name="records">
  /// A sequence of records to process. The method processes records one by one.
  /// Usually you can obtain the records from the <see cref="M:PX.Data.PXAdapter.Get``1" /> method.
  /// </param>
  /// <param name="hasMassProcessUI">
  /// An indicator of whether the processing interacts with the mass processing UI.
  /// When the value is <see langword="false" />, the method skips all calls to the mass processing UI.
  /// Usually you can obtain the value from the <see cref="P:PX.Data.PXAdapter.MassProcess" /> flag.
  /// </param>
  /// <param name="processRecordBy">
  /// A handler to process each record.
  /// </param>
  /// <param name="reportSuccessBy">
  /// A custom handler to report successful processing of a record to the mass processing UI.
  /// If the handler is not provided, the method calls the <see cref="M:PX.Data.PXProcessing`1.SetProcessed" /> method instead.
  /// </param>
  /// <param name="reportErrorBy">
  /// A custom handler to report failed processing of a record to the mass processing UI.
  /// The handler accepts the following parameters: the fault record, an exception,
  /// and the current value of the <paramref name="hasMassProcessUI" /> parameter.
  /// If the handler is not provided or returned <see langword="null" />,
  /// the method calls the <see cref="M:PX.Data.PXProcessing`1.SetError(System.Exception)" /> method instead.
  /// </param>
  /// <param name="actualizeRecordBy">
  /// A custom handler to actualize the state of the processed record.
  /// If the handler is not provided, the method does nothing to actualize the record.
  /// The actualization is crucial for workflow actions that do not update records directly.
  /// </param>
  /// <param name="finalizeRecordBy">
  /// A custom handler to finalize the processing of a record.
  /// If the handler is not provided, the method does nothing to finalize the record.
  /// </param>
  /// <returns>A number of records that the method failed to process.</returns>
  /// <example>
  /// <code>
  /// protected virtual IEnumerable CompleteProcessing(PXAdapter adapter)
  /// {
  /// 	bool isMassProcess = adapter.MassProcess;
  /// 	var invoices = adapter.Get&lt;ARInvoice&gt;().ToList();
  /// 
  /// 	PXLongOperation.StartOperation(this, delegate ()
  /// 	{
  /// 		var invoiceEntry = PXGraph.CreateInstance&lt;SOInvoiceEntry&gt;();
  /// 		PXProcessing&lt;ARInvoice&gt;.ProcessRecords(invoices, isMassProcess,
  /// 			invoice =&gt; invoiceEntry.CompleteProcessingImpl(invoice));
  /// 	});
  /// 
  /// 	return invoices;
  /// }
  /// </code>
  /// </example>
  public static int ProcessRecords(
    IEnumerable<Table> records,
    bool hasMassProcessUI,
    System.Action<Table> processRecordBy,
    System.Action<Table> reportSuccessBy = null,
    Func<Table, Exception, bool, bool?> reportErrorBy = null,
    System.Action<Table> actualizeRecordBy = null,
    System.Action<Table> finalizeRecordBy = null)
  {
    int num = 0;
    foreach (Table record in records)
    {
      if (hasMassProcessUI)
        PXProcessing<Table>.SetCurrentItem((object) record);
      try
      {
        processRecordBy(record);
        if (hasMassProcessUI)
        {
          if (actualizeRecordBy != null)
            actualizeRecordBy(record);
          if (reportSuccessBy != null)
            reportSuccessBy(record);
          else
            PXProcessing<Table>.SetProcessed();
        }
      }
      catch (Exception ex)
      {
        ++num;
        bool? nullable = reportErrorBy != null ? reportErrorBy(record, ex, hasMassProcessUI) : new bool?();
        if (nullable.HasValue)
        {
          bool valueOrDefault = nullable.GetValueOrDefault();
          if (!(hasMassProcessUI & valueOrDefault))
            throw;
        }
        else if (hasMassProcessUI)
          PXProcessing<Table>.SetError(ex);
        else
          throw;
      }
      finally
      {
        if (finalizeRecordBy != null)
          finalizeRecordBy(record);
      }
    }
    return num;
  }

  protected override BqlCommand GetCommand() => (BqlCommand) new PX.Data.Select<Table>();

  internal static bool TryPersistPerRow(PXGraph graph)
  {
    object[] processingList;
    PXProcessingMessagesCollection<Table> messages = PXProcessing.GetProcessingInfo(graph.UID, out processingList) is PXProcessingInfo<Table> processingInfo ? processingInfo.Messages : (PXProcessingMessagesCollection<Table>) null;
    if (messages == null || processingList == null || processingList.Length < 1 || messages.Length != processingList.Length)
      return false;
    PXCache cach = graph.Caches[typeof (Table)];
    HashSet<object> objectSet = new HashSet<object>((IEnumerable<object>) processingList);
    foreach (object obj in cach.Cached)
    {
      if (cach.GetStatus(obj) != PXEntryStatus.Updated)
      {
        if (EnumerableExtensions.IsNotIn<PXEntryStatus>(cach.GetStatus(obj), PXEntryStatus.Notchanged, PXEntryStatus.Held))
          return false;
      }
      else
        objectSet.Remove(obj);
    }
    if (objectSet.Count > 0)
      return false;
    object current = cach.Current;
    foreach (object obj in processingList)
      cach.SetStatus(obj, PXEntryStatus.Notchanged);
    byte[] timeStamp = graph.TimeStamp;
    byte[] numArray = timeStamp;
    for (int index = 0; index < processingList.Length; ++index)
    {
      graph.TimeStamp = timeStamp;
      object obj = processingList[index];
      try
      {
        cach.SetStatus(obj, PXEntryStatus.Updated);
        cach.Current = obj;
        graph.Persist();
        numArray = graph.TimeStamp;
      }
      catch (PXOuterException ex)
      {
        PXTrace.WriteError((Exception) ex);
        messages[index] = (PXProcessingMessage) new PXProcessingMessage<Table>(PXErrorLevel.RowError, ex.GetFullMessage(" "));
      }
      catch (PXLockViolationException ex)
      {
        PXTrace.WriteError((Exception) ex);
        messages[index] = (PXProcessingMessage) new PXProcessingMessage<Table>(PXErrorLevel.RowError, ex.Message);
        PXTimeStampScope.PutPersisted(cach, obj, (object) new byte[8]);
      }
      catch (Exception ex)
      {
        PXTrace.WriteError(ex);
        messages[index] = (PXProcessingMessage) new PXProcessingMessage<Table>(PXErrorLevel.RowError, ex.Message);
      }
      finally
      {
        cach.SetStatus(obj, PXEntryStatus.Notchanged);
      }
    }
    graph.TimeStamp = numArray;
    cach.Current = current;
    PXProcessing.SetProcessingInfo((PXProcessingInfo) processingInfo, processingList);
    return true;
  }

  protected void _ProcessScheduled(
    Action<List<Table>, CancellationToken> processor,
    List<Table> list,
    AUSchedule schedule,
    CancellationToken cancellationToken)
  {
    PXProcessing<Table>.ProcessScheduled<Table>((System.Action<List<Table>>) (itemsList => processor(itemsList, cancellationToken)), list, schedule, schedule.ScreenID, (Func<System.DateTime, PXCache, Exception, List<Table>, ProcessingResult>) ((startDate, histCache, exception, processingList) => this.WriteScheduleProcessingHistoryPerLine(startDate, histCache, exception, schedule, processingList)), this._Graph, this.AdjustmentRuleProvider);
  }

  internal static void ProcessScheduled<T>(
    System.Action<List<T>> processDelegate,
    List<T> list,
    AUSchedule schedule,
    string? screenId,
    Func<System.DateTime, PXCache, Exception?, List<T>, ProcessingResult> writeHistoryDelegate,
    PXGraph graph,
    IScheduleAdjustmentRuleProvider adjustmentRuleProvider)
  {
    System.DateTime dateTime = PXTimeZoneInfo.ConvertTimeFromUtc(PXTimeZoneInfo.UtcNow, PXTimeZoneInfo.FindSystemTimeZoneById(schedule.TimeZoneID));
    Exception exception = (Exception) null;
    PXContext.SetSlot<bool>("_ProcessScheduled", true);
    PXContext.SetSlot<string>("_ProcessScheduledMessage", (string) null);
    PXContext.SetSlot<bool>("ScheduleIsRunning", true);
    System.DateTime? lastRunDate = schedule.LastRunDate;
    System.DateTime? nextRunDate = schedule.NextRunDate;
    schedule.AbortCntr = new short?((short) 0);
    try
    {
      schedule.LastRunDate = new System.DateTime?(dateTime);
      try
      {
        schedule.AdjustNextDate(dateTime, adjustmentRuleProvider);
      }
      catch (PXSetPropertyException ex)
      {
        if (ex.ErrorLevel >= PXErrorLevel.Error)
        {
          schedule.LastRunDate = lastRunDate;
          schedule.NextRunDate = nextRunDate;
          schedule.IsActive = new bool?(false);
          throw ex;
        }
        exception = (Exception) ex;
      }
      AUSchedule auSchedule = schedule;
      int? runCntr = auSchedule.RunCntr;
      auSchedule.RunCntr = runCntr.HasValue ? new int?(runCntr.GetValueOrDefault() + 1) : new int?();
      processDelegate(list);
    }
    catch (Exception ex)
    {
      exception = ex;
    }
    finally
    {
      PXContext.SetSlot<bool>("_ProcessScheduled", false);
      PXContext.SetSlot<bool>("ScheduleIsRunning", false);
    }
    PXCache cach1 = graph.Caches[typeof (AUScheduleHistory)];
    ProcessingResult aggregatedProcessingResult = writeHistoryDelegate(dateTime, cach1, exception, list);
    PXErrorLevel errorLevel;
    schedule.LastRunResult = PXProcessing<Table>.GetLastRunResult(exception, aggregatedProcessingResult, out errorLevel);
    schedule.LastRunErrorLevel = new short?((short) errorLevel);
    graph.TimeStamp = schedule.TStamp;
    PXCache cach2 = graph.Caches[typeof (AUSchedule)];
    object obj = (object) schedule;
    if (schedule.GetType() != cach2.GetItemType())
      obj = cach2.ToChildEntity<AUSchedule>(schedule);
    cach2.SetStatus(obj, PXEntryStatus.Updated);
    PXCache cach3 = graph.Caches[typeof (AUScheduleExecution)];
    PXProcessing<Table>.InsertExecutionRecord(schedule, screenId, cach3, dateTime, aggregatedProcessingResult);
    if (aggregatedProcessingResult.HasError)
      PXTrace.Logger.ForSystemEvents("Scheduler", "Scheduler_AtLeastOneErrorProcessingFailedEventId").ForCurrentCompanyContext().ForContext("ContextScreenId", (object) "SM205020", false).ForContext("Schedule", (object) schedule, true).Warning<int?, int?>("At least one record has not been processed successfully ScheduleID:{ScheduleID} BranchID:{BranchID}", schedule.ScheduleID, schedule.BranchID);
    using (new PXConnectionScope())
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        cach1.Persist(PXDBOperation.Insert);
        cach3.Persist(PXDBOperation.Insert);
        cach2.Persist(PXDBOperation.Update);
        transactionScope.Complete();
      }
      cach1.Persisted(false);
      cach3.Persisted(false);
      cach2.Persisted(false);
      PXProcessing<Table>.DeleteRedundantHistoryRecords(schedule, cach1);
    }
    PXDatabase.SelectTimeStamp();
  }

  internal static void InsertExecutionRecord(
    AUSchedule schedule,
    string? screenId,
    PXCache execCache,
    System.DateTime executionDate,
    ProcessingResult aggregatedProcessingResult)
  {
    AUScheduleExecution scheduleExecution = new AUScheduleExecution()
    {
      ScheduleID = schedule.ScheduleID,
      ScreenID = screenId,
      ExecutionDate = new System.DateTime?(executionDate),
      ProcessedCount = new int?(aggregatedProcessingResult.Processed),
      WarningsCount = new int?(aggregatedProcessingResult.Warnings),
      ErrorsCount = new int?(aggregatedProcessingResult.Errors),
      TotalCount = new int?(aggregatedProcessingResult.Errors + aggregatedProcessingResult.Warnings + aggregatedProcessingResult.Processed)
    };
    execCache.Insert((object) scheduleExecution);
  }

  private ProcessingResult WriteScheduleProcessingHistoryPerLine(
    System.DateTime startDate,
    PXCache histCache,
    Exception? exception,
    AUSchedule schedule,
    List<Table> processedItems)
  {
    PXCache cache = this._OuterView.Cache;
    ProcessingResult processingResult = new ProcessingResult();
    bool flag = false;
    for (int index = 0; index < processedItems.Count; ++index)
    {
      object processedItem = (object) processedItems[index];
      if (processedItem != null)
      {
        Guid? refNoteId = this.GetRefIdDelegate(cache, processedItem);
        if (refNoteId.HasValue)
        {
          Guid? nullable = refNoteId;
          Guid empty = Guid.Empty;
          if ((nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() == empty ? 1 : 0) : 1) : 0) == 0)
          {
            this._Info = this._Info ?? PXProcessing.GetProcessingInfo(this._Graph.UID) as PXProcessingInfo<Table>;
            PXProcessingMessage message = (PXProcessingMessage) null;
            if (this._Info?.Messages != null && this._Info.Messages.Length > index)
              message = this._Info.Messages[index];
            PXErrorLevel errorLevel = PXProcessing<Table>.InsertHistoryRecord(schedule, histCache, message, exception, startDate, refNoteId);
            processingResult.AddToResult(errorLevel);
            continue;
          }
        }
        flag = true;
      }
    }
    if (flag)
      this.Logger.ForSystemEvents("Scheduler", "Scheduler_SavingToHistoryFailedEventId").Warning<System.Type>("The processed {ItemType} item does not contain NoteID, and the processing information cannot be saved to the schedule history.", cache.GetItemType());
    return processingResult;
  }

  internal static void DeleteRedundantHistoryRecords(AUSchedule schedule, PXCache historyCache)
  {
    bool? keepFullHistory = schedule.KeepFullHistory;
    bool flag = true;
    if (keepFullHistory.GetValueOrDefault() == flag & keepFullHistory.HasValue || !schedule.HistoryRetainCount.HasValue)
      return;
    System.DateTime? nullable = PXSelectBase<AUScheduleExecution, PXSelectReadonly<AUScheduleExecution, Where<AUScheduleExecution.scheduleID, Equal<Required<AUScheduleExecution.scheduleID>>>, OrderBy<Desc<AUScheduleExecution.executionDate>>>.Config>.SelectWindowed(historyCache.Graph, (int) schedule.HistoryRetainCount.Value, 1, (object) schedule.ScheduleID).FirstTableItems.Select<AUScheduleExecution, System.DateTime?>((System.Func<AUScheduleExecution, System.DateTime?>) (e => e.ExecutionDate)).FirstOrDefault<System.DateTime?>();
    if (!nullable.HasValue)
      return;
    nullable = new System.DateTime?(SyMappingUtils.ConvertDateToDatabaseFormat(nullable.Value, historyCache, "executionDate"));
    using (PXTransactionScope transactionScope = new PXTransactionScope(true))
    {
      ExecutionContext executionContext = new ExecutionContext((IExecutionObserver) null)
      {
        TimeoutMultiplier = 60
      };
      PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint((IDbTransaction) PXTransactionScope.GetTransaction());
      int companyId = PXDatabase.Provider.getCompanyID(dbServicesPoint.GetTable("AUScheduleExecution", FileMode.Open).TableName, out companySetting _);
      YaqlCondition yaqlCondition = Yaql.and(Yaql.eq<int>((YaqlScalar) Yaql.column("CompanyID", (string) null), companyId), Yaql.and(Yaql.eq<int?>((YaqlScalar) Yaql.column("scheduleID", (string) null), schedule.ScheduleID), Yaql.lte<System.DateTime>((YaqlScalar) Yaql.column("executionDate", (string) null), nullable.Value)));
      int num1 = 1000;
      CmdDelete cmdDelete = new CmdDelete(YaqlSchemaTable.op_Implicit("AUScheduleExecution"), (List<YaqlJoin>) null)
      {
        Condition = yaqlCondition,
        LimitRows = num1
      };
      int num2 = historyCache.Cached.OfType<object>().Count<object>();
      if (num2 == 0)
        num2 = num1;
      int num3 = num2 / num1 + 1;
      CmdRepeatableDelete repeatableDelete1 = new CmdRepeatableDelete(YaqlSchemaTable.op_Implicit("AUScheduleHistory"), (List<YaqlJoin>) null);
      ((CmdDelete) repeatableDelete1).Condition = yaqlCondition;
      ((CmdDelete) repeatableDelete1).LimitRows = num1;
      repeatableDelete1.RepeatCount = num3;
      CmdRepeatableDelete repeatableDelete2 = repeatableDelete1;
      dbServicesPoint.executeCommands((IEnumerable<CommandBase>) new CmdDelete[2]
      {
        cmdDelete,
        (CmdDelete) repeatableDelete2
      }, executionContext, false);
      transactionScope.Complete();
    }
  }

  internal static PXErrorLevel InsertHistoryRecord(
    AUSchedule schedule,
    PXCache histCache,
    PXProcessingMessage? message,
    Exception? exception,
    System.DateTime executionDate,
    Guid? refNoteId)
  {
    AUScheduleHistory auScheduleHistory = new AUScheduleHistory()
    {
      ScheduleID = schedule.ScheduleID,
      ScreenID = schedule.ScreenID,
      ExecutionDate = new System.DateTime?(executionDate),
      RefNoteID = refNoteId
    };
    PXErrorLevel errorLevel;
    if (message != null)
    {
      auScheduleHistory.ExecutionResult = message.Message;
      errorLevel = message.ErrorLevel;
    }
    else if (exception != null)
    {
      auScheduleHistory.ExecutionResult = exception.Message;
      errorLevel = PXProcessing<Table>.GetErrorLevelFromException(exception, PXErrorLevel.RowError);
      if (exception is PXBaseRedirectException && string.IsNullOrEmpty(auScheduleHistory.ExecutionResult))
        auScheduleHistory.ExecutionResult = PXMessages.LocalizeNoPrefix("The record has been processed successfully.");
    }
    else
    {
      auScheduleHistory.ExecutionResult = PXMessages.LocalizeNoPrefix("The record has been processed successfully.");
      errorLevel = PXErrorLevel.RowInfo;
    }
    auScheduleHistory.ErrorLevel = new short?((short) errorLevel);
    if (string.IsNullOrEmpty(auScheduleHistory.ExecutionResult))
      auScheduleHistory.ExecutionResult = PXProcessing<Table>.GetDefaultProcessingResultMessageFromErrorLevel(errorLevel, true);
    histCache.Insert((object) auScheduleHistory);
    return errorLevel;
  }

  private static string GetLastRunResult(
    Exception? exception,
    ProcessingResult aggregatedProcessingResult,
    out PXErrorLevel errorLevel)
  {
    switch (exception)
    {
      case null:
      case PXBaseRedirectException _:
        errorLevel = aggregatedProcessingResult.GetErrorLevel();
        return PXProcessing<Table>.GetDefaultProcessingResultMessageFromErrorLevel(errorLevel, false);
      default:
        string slot = PXContext.GetSlot<string>("_ProcessScheduledMessage");
        errorLevel = PXProcessing<Table>.GetErrorLevelFromException(exception, PXErrorLevel.Error);
        if (string.IsNullOrEmpty(exception.Message))
          return PXProcessing<Table>.GetDefaultProcessingResultMessageFromErrorLevel(errorLevel, false);
        if (string.IsNullOrEmpty(slot))
          return exception.Message;
        return $"{exception.Message} {PXMessages.LocalizeNoPrefix("Inner error(s):")} {slot}";
    }
  }

  internal static PXErrorLevel GetErrorLevelFromException(
    Exception exception,
    PXErrorLevel defaultErrorLevel)
  {
    switch (exception)
    {
      case PXOperationCompletedWithErrorException _:
      case PXOperationCompletedSingleErrorException _:
        return PXErrorLevel.Error;
      case PXOperationCompletedWithWarningException _:
        return PXErrorLevel.Warning;
      case PXOperationCompletedException _:
        return PXErrorLevel.Undefined;
      case PXSetPropertyException propertyException:
        return propertyException.ErrorLevel;
      case PXBaseRedirectException _:
        return PXErrorLevel.RowInfo;
      default:
        return defaultErrorLevel;
    }
  }

  internal static string GetDefaultProcessingResultMessageFromErrorLevel(
    PXErrorLevel errorLevel,
    bool getResultForRecord)
  {
    switch (errorLevel)
    {
      case PXErrorLevel.Warning:
      case PXErrorLevel.RowWarning:
        return !getResultForRecord ? PXMessages.LocalizeNoPrefix("The operation has been completed. At least one item was processed with a warning. Please review") : PXMessages.LocalizeNoPrefix("The record has been processed with warnings.");
      case PXErrorLevel.Error:
      case PXErrorLevel.RowError:
        return PXMessages.LocalizeNoPrefix("The operation has been completed with an error. Please review.");
      default:
        return !getResultForRecord ? PXMessages.LocalizeNoPrefix("The operation has been completed successfully.") : PXMessages.LocalizeNoPrefix("The record has been processed successfully.");
    }
  }

  /// <exclude />
  public bool TrySetRefIdGetter(
  #nullable disable
  Func<PXCache, object, Guid?> getter, string refIdBqlField)
  {
    if (this._getRefIdDelegate != null)
      return false;
    this._getRefIdDelegate = getter;
    this._refIdBqlFieldName = refIdBqlField;
    return true;
  }

  public Func<PXCache, object, Guid?> GetRefIdDelegate
  {
    get
    {
      this._getRefIdDelegate = this._getRefIdDelegate ?? PXProcessing<Table>.\u003C\u003EO.\u003C0\u003E__GetNoteIDNow ?? (PXProcessing<Table>.\u003C\u003EO.\u003C0\u003E__GetNoteIDNow = new Func<PXCache, object, Guid?>(PXNoteAttribute.GetNoteIDNow));
      return this._getRefIdDelegate;
    }
  }

  public string RefIdBqlField => this._refIdBqlFieldName ?? this._NoteIDField;

  protected internal ScheduleParam ScheduleParameters
  {
    get => (ScheduleParam) this._Graph.Views["_ScheduleHistoryParameters_"].Cache.Current;
  }

  /// <summary>Sets the display name of the button that processes the
  /// selected data records.</summary>
  /// <param name="caption">The string used as the display name.</param>
  public virtual void SetProcessCaption(string caption)
  {
    this._ProcessButton.SetDynamicText(true);
    this._ProcessButton.SetCaption(caption);
  }

  /// <summary>Sets the tooltip for the button that processes the selected
  /// data records.</summary>
  /// <param name="tooltip">The string used as the tooltip.</param>
  public virtual void SetProcessTooltip(string tooltip)
  {
    this._ProcessButton.SetDynamicText(true);
    this._ProcessButton.SetTooltip(tooltip);
  }

  /// <summary>Sets the display name of the button that processes all data
  /// records selected by the data view.</summary>
  /// <param name="caption">The string used as the display name.</param>
  public virtual void SetProcessAllCaption(string caption)
  {
    this._ProcessAllButton.SetDynamicText(true);
    this._ProcessAllButton.SetCaption(caption);
  }

  /// <summary>Sets the tooltip for the button that processes all data
  /// records selected by the data view.</summary>
  /// <param name="tooltip">The string used as the tooltip.</param>
  public virtual void SetProcessAllTooltip(string tooltip)
  {
    this._ProcessAllButton.SetDynamicText(true);
    this._ProcessAllButton.SetTooltip(tooltip);
  }

  /// <summary>Enables or disables the button that processes the selected
  /// data records.</summary>
  /// <param name="enabled">The value indicating whether the button is
  /// enabled.</param>
  public virtual void SetProcessEnabled(bool enabled) => this._ProcessButton.SetEnabled(enabled);

  /// <summary>Enables or disables the button that processes all data
  /// records selected by the data view.</summary>
  /// <param name="enabled">The value indicating whether the button is
  /// enalbed.</param>
  public virtual void SetProcessAllEnabled(bool enabled)
  {
    this._ProcessAllButton.SetEnabled(enabled);
  }

  /// <summary>Displays or hides the button that processes the selected data
  /// records.</summary>
  /// <param name="visible">The value indicating whether the button is
  /// visible.</param>
  public virtual void SetProcessVisible(bool visible) => this._ProcessButton.SetVisible(visible);

  /// <summary>Displays or hides the button that processes all data records
  /// selected by the data view.</summary>
  /// <param name="visible">The value indicating whether the button is
  /// visible.</param>
  public virtual void SetProcessAllVisible(bool visible)
  {
    this._ProcessAllButton.SetVisible(visible);
  }

  /// <summary>Indicates whether the action, that processes the selected data
  /// records, is  visible on the main toolbar</summary>
  public virtual void SetProcessDisplayOnMainToolbar(bool displayOnMainToolbar)
  {
    this._ProcessButton.SetDisplayOnMainToolbar(displayOnMainToolbar);
  }

  /// <summary>Indicates whether the action, that processes all data records
  /// selected by the data view, is  visible on the main toolbar</summary>
  public virtual void SetProcessAllDisplayOnMainToolbar(bool displayOnMainToolbar)
  {
    this._ProcessAllButton.SetDisplayOnMainToolbar(displayOnMainToolbar);
  }

  /// <summary>Indicates whether the action, that processes the selected data
  /// records, is  locked on the main toolbar</summary>
  public virtual void SetProcessIsLockedOnToolbar(bool isLockedOnToolbar)
  {
    this._ProcessButton.SetIsLockedOnToolbar(isLockedOnToolbar);
  }

  /// <summary>Indicates whether the action, that processes all data records
  /// selected by the data view, is  locked on the main toolbar</summary>
  public virtual void SetProcessAllIsLockedOnToolbar(bool isLockedOnToolbar)
  {
    this._ProcessAllButton.SetIsLockedOnToolbar(isLockedOnToolbar);
  }

  protected PXProcessing()
  {
  }

  /// <summary>Initializes a new instance of a data view bound to the
  /// specified graph.</summary>
  /// <param name="graph">The graph with which the data view is
  /// associated.</param>
  public PXProcessing(PXGraph graph)
    : base(graph, (Delegate) null)
  {
  }

  /// <summary>Initializes a new instance of a data view that is bound to
  /// the specified graph and uses the provided method to retrieve
  /// data.</summary>
  /// <param name="graph">The graph with which the data view is
  /// associated.</param>
  /// <param name="handler">The delegate of the method that is used to
  /// retrieve the data from the database (or other source).</param>
  public PXProcessing(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  protected override void _PrepareGraph<Primary>()
  {
    base._PrepareGraph<Primary>();
    this.AttachActions<Primary>();
  }

  protected virtual void AttachActions<Primary>() where Primary : class, IBqlTable, new()
  {
    PXCache cache = this._OuterView.Cache;
    PXGraph graph = cache.Graph;
    this._ProcessButton = (PXAction) new PXAction<Primary>(graph, (Delegate) new PXButtonDelegate(this.Process));
    this._ProcessButton.SetIsLockedOnToolbar(true);
    PXProcessingBase<Table>.AddAction(graph, "Process", this._ProcessButton);
    this._ProcessAllButton = (PXAction) new PXAction<Primary>(graph, (Delegate) new PXButtonDelegate(this.ProcessAll));
    this._ProcessAllButton.SetIsLockedOnToolbar(true);
    PXProcessingBase<Table>.AddAction(graph, "ProcessAll", this._ProcessAllButton);
    this.InitProcessingProgress<Primary>(graph, cache);
    this._ScheduleAddButton = (PXAction) new PXAction<Primary>(graph, (Delegate) new PXButtonDelegate(this._ScheduleAdd_));
    PXProcessingBase<Table>.AddAction(graph, "_ScheduleAdd_", this._ScheduleAddButton);
    this._ScheduleViewButton = (PXAction) new PXAction<Primary>(graph, (Delegate) new PXButtonDelegate(this._ScheduleView_));
    PXProcessingBase<Table>.AddAction(graph, "_ScheduleView_", this._ScheduleViewButton);
    this._ScheduleHistoryButton = (PXAction) new PXAction<Primary>(graph, (Delegate) new PXButtonDelegate(this._ScheduleHistory_));
    PXProcessingBase<Table>.AddAction(graph, "_ScheduleHistory_", this._ScheduleHistoryButton);
    this._ScheduleButton = (PXAction) new PXAction<Primary>(graph, (Delegate) new PXButtonDelegate(this.Schedule));
    this._ScheduleButton.SetMenu(new ButtonMenu[3]
    {
      new ButtonMenu("_ScheduleAdd_", (string) null),
      new ButtonMenu("_ScheduleView_", (string) null),
      new ButtonMenu("_ScheduleHistory_", (string) null)
    });
    this._ScheduleButton.MenuAutoOpen = true;
    PXProcessingBase<Table>.AddAction(graph, "Schedule", this._ScheduleButton);
    this._ScheduleButton.SetVisible(PXAccess.IsSchedulesEnabled() && PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+ScheduleModule"));
    (graph.Views["_ScheduleHistoryParameters_"] = new PXFilter<ScheduleParam>(graph).View).Cache.FieldVerifyingEvents["scheduleid"] += (PXFieldVerifying) ((sender, e) => e.Cancel = true);
    string screenId = this._Graph.Accessinfo.ScreenID?.Replace(".", "");
    IScheduleProvider scheduleProvider = this.ScheduleProvider;
    bool? nullable;
    if (scheduleProvider == null)
    {
      nullable = new bool?();
    }
    else
    {
      IEnumerable<AUSchedule> allSchedules = scheduleProvider.AllSchedules;
      nullable = allSchedules != null ? new bool?(allSchedules.Any<AUSchedule>((System.Func<AUSchedule, bool>) (schedule => string.Equals(schedule.ScreenID, screenId, StringComparison.OrdinalIgnoreCase)))) : new bool?();
    }
    bool anySchedules = (nullable.GetValueOrDefault() ? 1 : 0) != 0;
    PXFieldSelecting pxFieldSelecting1 = (PXFieldSelecting) ((sender, e) =>
    {
      if (PXLongOperation.GetStatus(graph.UID) == PXLongRunStatus.NotExists)
        return;
      this.DisableButton(e);
    });
    PXFieldSelecting pxFieldSelecting2 = (PXFieldSelecting) ((sender, e) =>
    {
      if (anySchedules)
        return;
      this.DisableButton(e);
    });
    this._ProcessButton.StateSelectingEvents += pxFieldSelecting1;
    this._ProcessAllButton.StateSelectingEvents += pxFieldSelecting1;
    this._ScheduleButton.StateSelectingEvents += pxFieldSelecting1;
    this._ScheduleAddButton.StateSelectingEvents += pxFieldSelecting1;
    this._ScheduleViewButton.StateSelectingEvents += pxFieldSelecting2;
    this._ScheduleHistoryButton.StateSelectingEvents += pxFieldSelecting2;
  }

  private void DisableButton(PXFieldSelectingEventArgs e)
  {
    PXButtonState instance = PXButtonState.CreateInstance(e.ReturnState, (string) null, (string) null, (string) null, (string) null, (string) null, new bool?(false), PXConfirmationType.Unspecified, (string) null, new char?(), new bool?(), new bool?(), (ButtonMenu[]) null, new bool?(), new bool?(), new bool?(), (string) null, (string) null, (PXShortCutAttribute) null, typeof (Table));
    instance.Enabled = false;
    e.ReturnState = (object) instance;
  }

  protected void InitProcessingProgress<Primary>(PXGraph graph, PXCache cache) where Primary : class, IBqlTable, new()
  {
    if (!WebConfig.ProcessingProgressDialog)
      return;
    this._Graph.IsProcessing = true;
    if ((this._Graph.IsProcessing ? 1 : (this._Graph.IsMobile ? 1 : 0)) == 0 || PXGraph.ProxyIsActive)
      return;
    this._Graph.Views["ProcessingView"] = this.View;
    this.View = (PXView) new PXProcessingBase<Table>.ParametrizedView(graph, this.View.BqlSelect, (PXProcessingBase<Table>) this, new PXSelectDelegate(((PXProcessingBase<Table>) this)._List));
    graph.Caches.AddCacheMapping(typeof (FilterHeader), typeof (FilterHeader));
    this.ViewFieldsFilters = new PXSelect<PXProcessing<Table>.ProcessingFilterHeader>(graph, (Delegate) new PXSelectDelegate(this.viewFieldsFilters));
    graph.Views["ViewFieldsFilters"] = this.ViewFieldsFilters.View;
    PXSelect<FilterRow> pxSelect = new PXSelect<FilterRow>(graph, (Delegate) new PXSelectDelegate(this.viewFilterRows));
    graph.Views["ProcessingView$FilterRow"] = pxSelect.View;
    cache.Fields.Add("ProcessingStatus");
    cache.Graph.FieldSelecting.AddHandler(cache.GetItemType(), "ProcessingStatus", new PXFieldSelecting(this.ProcessingStatus_FieldSelecting));
    cache.Fields.Add("ProcessingMessage");
    cache.Graph.FieldSelecting.AddHandler(cache.GetItemType(), "ProcessingMessage", new PXFieldSelecting(this.ProcessingMessage_FieldSelecting));
  }

  private void ProcessingMessage_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null)
    {
      PXFieldSelectingEventArgs selectingEventArgs = e;
      System.Type dataType = typeof (string);
      string str = PXMessages.LocalizeNoPrefix("Message");
      bool? isKey = new bool?();
      bool? nullable = new bool?();
      int? required = new int?();
      int? precision = new int?();
      int? length = new int?();
      string displayName = str;
      bool? enabled = new bool?();
      bool? visible = new bool?();
      bool? readOnly = new bool?();
      PXFieldState instance = PXFieldState.CreateInstance((object) null, dataType, isKey, nullable, required, precision, length, fieldName: "ProcessingMessage", displayName: displayName, enabled: enabled, visible: visible, readOnly: readOnly, visibility: PXUIVisibility.Visible);
      selectingEventArgs.ReturnState = (object) instance;
    }
    else
    {
      PXProcessingMessage processingMessage;
      this._SelectedInfo.TryGetValue(e.Row, out processingMessage);
      if (processingMessage != null)
      {
        e.ReturnValue = string.IsNullOrEmpty(processingMessage.Message) ? (object) processingMessage.Message : (object) PXMessages.LocalizeNoPrefix(processingMessage.Message);
      }
      else
      {
        if (e.ReturnValue != null)
          return;
        e.ReturnValue = (object) "";
      }
    }
  }

  private void ProcessingStatus_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null)
    {
      PXFieldSelectingEventArgs selectingEventArgs = e;
      System.Type dataType = typeof (string);
      string str = PXMessages.LocalizeNoPrefix("Status");
      bool? isKey = new bool?();
      bool? nullable = new bool?();
      int? required = new int?();
      int? precision = new int?();
      int? length = new int?();
      string displayName = str;
      bool? enabled = new bool?();
      bool? visible = new bool?();
      bool? readOnly = new bool?();
      PXFieldState instance = PXFieldState.CreateInstance((object) null, dataType, isKey, nullable, required, precision, length, fieldName: "ProcessingStatus", displayName: displayName, enabled: enabled, visible: visible, readOnly: readOnly, visibility: PXUIVisibility.Visible);
      selectingEventArgs.ReturnState = (object) instance;
    }
    else
    {
      PXProcessingMessage processingMessage;
      this._SelectedInfo.TryGetValue(e.Row, out processingMessage);
      if (processingMessage != null)
      {
        PXErrorLevel pxErrorLevel1 = PXErrorLevel.RowError;
        e.ReturnValue = (object) PXMessages.LocalizeNoPrefix("Failed");
        if (processingMessage.ErrorLevel == PXErrorLevel.RowInfo)
        {
          e.ReturnValue = (object) PXMessages.LocalizeNoPrefix("Processed");
          pxErrorLevel1 = PXErrorLevel.RowInfo;
        }
        if (processingMessage.ErrorLevel == PXErrorLevel.RowWarning || processingMessage.ErrorLevel == PXErrorLevel.Warning)
        {
          e.ReturnValue = (object) PXMessages.LocalizeNoPrefix("Skipped");
          pxErrorLevel1 = PXErrorLevel.RowWarning;
        }
        PXFieldSelectingEventArgs selectingEventArgs = e;
        object returnValue = e.ReturnValue;
        System.Type dataType = typeof (string);
        string str1 = PXMessages.LocalizeNoPrefix("Status");
        PXErrorLevel pxErrorLevel2 = pxErrorLevel1;
        string str2 = string.IsNullOrEmpty(processingMessage.Message) ? processingMessage.Message : PXMessages.LocalizeNoPrefix(processingMessage.Message);
        bool? isKey = new bool?();
        bool? nullable = new bool?();
        int? required = new int?();
        int? precision = new int?();
        int? length = new int?();
        string displayName = str1;
        string error = str2;
        int errorLevel = (int) pxErrorLevel2;
        bool? enabled = new bool?();
        bool? visible = new bool?();
        bool? readOnly = new bool?();
        PXFieldState instance = PXFieldState.CreateInstance(returnValue, dataType, isKey, nullable, required, precision, length, fieldName: "ProcessingStatus", displayName: displayName, error: error, errorLevel: (PXErrorLevel) errorLevel, enabled: enabled, visible: visible, readOnly: readOnly, visibility: PXUIVisibility.Visible);
        selectingEventArgs.ReturnState = (object) instance;
      }
      else
      {
        if (e.ReturnValue != null && !(e.ReturnValue.ToString() == ""))
          return;
        e.ReturnValue = (object) PXMessages.LocalizeNoPrefix("Pending");
      }
    }
  }

  [PXProcessButton]
  [PXUIField(DisplayName = "Process", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected virtual IEnumerable Process(PXAdapter adapter)
  {
    if (!this.DoNotCheckPrevOperation)
    {
      if (PXLongOperation.Exists(this._Graph.UID))
        throw new PXException("The previous operation has not been completed yet.");
    }
    else
      this.DoNotCheckPrevOperation = false;
    PXCache cache = this._OuterView.Cache;
    cache.IsDirty = false;
    PXProcessingBase<Table>.OuterView outerView = new PXProcessingBase<Table>.OuterView(this._OuterView, (BqlCommand) new PX.Data.Select<Table>(), (Delegate) (() => (IEnumerable) this.GetSelectedItems(cache, cache.Cached)));
    int num1 = 0;
    int num2 = 0;
    List<Table> list = new List<Table>();
    ref int local1 = ref num1;
    ref int local2 = ref num2;
    foreach (object obj in outerView.Select((object[]) null, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref local1, 0, ref local2))
      list.Add(obj is PXResult ? (Table) (PXResult<Table>) obj : (Table) obj);
    AUSchedule schedule = PXContext.GetSlot<AUSchedule>();
    if (list.Count > 0 && (this._ParametersDelegate == null || this._ParametersDelegate(list)))
    {
      if (this._AutoPersist)
      {
        this._Graph.Actions.PressSave(this._ProcessButton);
        foreach (Table able in list)
          cache.SetStatus((object) able, PXEntryStatus.Updated);
      }
      if (!this._IsInstance)
      {
        if (schedule == null)
        {
          this._Graph.LongOperationManager.StartOperation((System.Action<CancellationToken>) (cancellationToken => this.ProcessDelegate(list, cancellationToken)));
          if (HttpContext.Current != null && this.IsUIRequest(HttpContext.Current) && this._Graph.IsProcessing)
            HttpContext.Current.Items.Add((object) (this._Graph.UID?.ToString() + "_Processing"), (object) true);
        }
        else
        {
          PXContext.SetSlot<AUSchedule>((AUSchedule) null);
          AUSchedule scheduleparam = schedule;
          this._Graph.LongOperationManager.StartOperation((System.Action<CancellationToken>) (cancellationToken => this._ProcessScheduled(this.ProcessDelegate, list, scheduleparam, cancellationToken)));
          schedule = (AUSchedule) null;
        }
        PXUIFieldAttribute.SetEnabled(cache, this._SelectedField, false);
        adapter.StartRow = 0;
      }
      else
        this.ProcessDelegate(list, CancellationToken.None);
    }
    else if (schedule != null)
    {
      PXContext.SetSlot<AUSchedule>((AUSchedule) null);
      this._Graph.LongOperationManager.StartOperation((System.Action<CancellationToken>) (cancellationToken => this._ProcessScheduled(this.ProcessDelegate, list, schedule, cancellationToken)));
    }
    return adapter.Get();
  }

  [PXProcessButton]
  [PXUIField(DisplayName = "Process All", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected virtual IEnumerable ProcessAll(PXAdapter adapter)
  {
    if (!this.DoNotCheckPrevOperation)
    {
      if (PXLongOperation.Exists(this._Graph.UID))
        throw new PXException("The previous operation has not been completed yet.");
    }
    else
      this.DoNotCheckPrevOperation = false;
    AUSchedule slot = PXContext.GetSlot<AUSchedule>();
    this.RunProcessAll(adapter, slot);
    return adapter.Get();
  }

  private void RunProcessAll(PXAdapter adapter, AUSchedule schedule)
  {
    PXCache cache = this._OuterView.Cache;
    object current = cache.Current;
    List<Table> items = this._PendingList(adapter.Parameters, adapter.SortColumns, adapter.Descendings, adapter.Filters);
    if (this._IsInstance)
      cache.Current = current;
    cache.IsDirty = false;
    List<Table> list = this.GetSelectedItems(cache, (IEnumerable) items);
    if (list.Count > 0 && (this._ParametersDelegate == null || this._ParametersDelegate(list)))
    {
      if (this._AutoPersist)
      {
        this._Graph.Actions.PressSave(this._ProcessAllButton);
        foreach (Table able in list)
          cache.SetStatus((object) able, PXEntryStatus.Updated);
      }
      if (!this._IsInstance)
      {
        if (schedule == null)
        {
          this._Graph.LongOperationManager.StartOperation((System.Action<CancellationToken>) (cancellationToken => this.ProcessDelegate(list, cancellationToken)));
          if (HttpContext.Current != null && this.IsUIRequest(HttpContext.Current) && this._Graph.IsProcessing)
            HttpContext.Current.Items.Add((object) (this._Graph.UID?.ToString() + "_Processing"), (object) true);
        }
        else
        {
          PXContext.SetSlot<AUSchedule>((AUSchedule) null);
          AUSchedule scheduleparam = schedule;
          this._Graph.LongOperationManager.StartOperation((System.Action<CancellationToken>) (cancellationToken => this._ProcessScheduled(this.ProcessDelegate, list, scheduleparam, cancellationToken)));
          schedule = (AUSchedule) null;
        }
        PXUIFieldAttribute.SetEnabled(cache, this._SelectedField, false);
        adapter.StartRow = 0;
      }
      else
        this.ProcessDelegate(list, CancellationToken.None);
    }
    else
    {
      if (schedule == null)
        return;
      PXContext.SetSlot<AUSchedule>((AUSchedule) null);
      this._Graph.LongOperationManager.StartOperation((System.Action<CancellationToken>) (cancellationToken => this._ProcessScheduled(this.ProcessDelegate, list, schedule, cancellationToken)));
    }
  }

  protected bool IsUIRequest(HttpContext context)
  {
    try
    {
      if (context != null)
      {
        string executionFilePath = context.Request.AppRelativeCurrentExecutionFilePath;
        ushort num = StringExtensions.Segment(executionFilePath, '/', (ushort) 1).OrdinalEquals("t") ? (ushort) 3 : (ushort) 1;
        return StringExtensions.Segment(executionFilePath, '/', (ushort) 1).Equals("pages", StringComparison.OrdinalIgnoreCase) || StringExtensions.Segment(executionFilePath, '/', num).OrdinalEquals("ui") && StringExtensions.Segment(executionFilePath, '/', (ushort) ((uint) num + 1U)).OrdinalEquals("screen");
      }
    }
    catch
    {
      return false;
    }
    return false;
  }

  protected virtual IEnumerable viewFieldsFilters()
  {
    this.ViewFieldsFilters.Cache.Clear();
    List<PXProcessing<Table>.ProcessingFilterHeader> processingFilterHeaderList = new List<PXProcessing<Table>.ProcessingFilterHeader>();
    PXProcessing<Table>.ProcessingFilterHeader processingFilterHeader1 = new PXProcessing<Table>.ProcessingFilterHeader();
    processingFilterHeader1.FilterID = new Guid?(PXProcessing<Table>.FailedFilterID);
    processingFilterHeader1.FilterName = "Failed";
    processingFilterHeaderList.Add(processingFilterHeader1);
    PXProcessing<Table>.ProcessingFilterHeader processingFilterHeader2 = new PXProcessing<Table>.ProcessingFilterHeader();
    processingFilterHeader2.FilterID = new Guid?(PXProcessing<Table>.ProcessedFilterID);
    processingFilterHeader2.FilterName = "Processed";
    processingFilterHeaderList.Add(processingFilterHeader2);
    PXProcessing<Table>.ProcessingFilterHeader processingFilterHeader3 = new PXProcessing<Table>.ProcessingFilterHeader();
    processingFilterHeader3.FilterID = new Guid?(PXProcessing<Table>.PendingFilterID);
    processingFilterHeader3.FilterName = "Pending";
    processingFilterHeaderList.Add(processingFilterHeader3);
    PXProcessing<Table>.ProcessingFilterHeader processingFilterHeader4 = new PXProcessing<Table>.ProcessingFilterHeader();
    processingFilterHeader4.FilterID = new Guid?(PXProcessing<Table>.SkippedFilterID);
    processingFilterHeader4.FilterName = "Skipped";
    processingFilterHeaderList.Add(processingFilterHeader4);
    foreach (PXProcessing<Table>.ProcessingFilterHeader processingFilterHeader5 in processingFilterHeaderList)
    {
      bool? nullable1 = processingFilterHeader5.IsDefault;
      if (!nullable1.HasValue)
        processingFilterHeader5.IsDefault = new bool?(false);
      PXProcessing<Table>.ProcessingFilterHeader processingFilterHeader6 = processingFilterHeader5;
      processingFilterHeader5.IsSystem = nullable1 = new bool?(false);
      bool? nullable2 = nullable1;
      processingFilterHeader6.IsShared = nullable2;
      this.ViewFieldsFilters.Cache.SetStatus((object) processingFilterHeader5, PXEntryStatus.Held);
    }
    return this.ViewFieldsFilters.Cache.Cached;
  }

  protected virtual IEnumerable viewFilterRows()
  {
    PXProcessing<Table> pxProcessing = this;
    if (PXView.Parameters.Length != 0)
    {
      Guid filterID = (Guid) PXView.Parameters[0];
      if (!(filterID == Guid.Empty))
      {
        FilterRow[] array = pxProcessing._Graph.Views["ProcessingView$FilterRow"].Cache.Cached.OfType<FilterRow>().Where<FilterRow>((System.Func<FilterRow, bool>) (row =>
        {
          Guid? filterId = row.FilterID;
          Guid guid = filterID;
          if (!filterId.HasValue)
            return false;
          return !filterId.HasValue || filterId.GetValueOrDefault() == guid;
        })).ToArray<FilterRow>();
        if (((IEnumerable<FilterRow>) array).Any<FilterRow>())
        {
          FilterRow[] filterRowArray = array;
          for (int index = 0; index < filterRowArray.Length; ++index)
            yield return (object) filterRowArray[index];
          filterRowArray = (FilterRow[]) null;
        }
        else
        {
          FilterRow filterRow = new FilterRow()
          {
            FilterID = new Guid?(filterID),
            DataField = "ProcessingStatus",
            Condition = new byte?((byte) 0),
            ValueSt = "Proc",
            IsUsed = new bool?(true)
          };
          if (filterID == PXProcessing<Table>.FailedFilterID)
            filterRow.ValueSt = PXMessages.LocalizeNoPrefix("Failed");
          else if (filterID == PXProcessing<Table>.ProcessedFilterID)
            filterRow.ValueSt = PXMessages.LocalizeNoPrefix("Processed");
          else if (filterID == PXProcessing<Table>.PendingFilterID)
            filterRow.ValueSt = PXMessages.LocalizeNoPrefix("Pending");
          else if (filterID == PXProcessing<Table>.SkippedFilterID)
            filterRow.ValueSt = PXMessages.LocalizeNoPrefix("Skipped");
          yield return (object) filterRow;
        }
      }
    }
  }

  [PXButton(ImageKey = "Shedule", CommitChanges = true, Tooltip = "Schedules", SpecialType = PXSpecialButtonType.Schedule, IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected virtual IEnumerable Schedule(PXAdapter adapter) => adapter.Get();

  [PXButton(ImageKey = "AddNew", DisplayOnMainToolbar = false)]
  [PXUIField(DisplayName = "Add", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected virtual IEnumerable _ScheduleAdd_(PXAdapter adapter)
  {
    if (!string.IsNullOrEmpty(this._Graph.Accessinfo.ScreenID))
    {
      AUScheduleMaint instance = PXGraph.CreateInstance<AUScheduleMaint>();
      AUSchedule auSchedule1 = new AUSchedule();
      string str = this._Graph.Accessinfo.ScreenID.Replace(".", "");
      instance.ScheduleCurrentScreen.Current.ScreenID = str;
      auSchedule1.ViewName = this._Graph.ViewNames[this.View];
      auSchedule1.GraphName = CustomizedTypeManager.GetTypeNotCustomized(this._Graph.GetType()).FullName;
      AUSchedule auSchedule2 = instance.Schedule.Insert(auSchedule1);
      instance.Schedule.Cache.IsDirty = false;
      bool flag = false;
      PXCache cache = this._OuterView.Cache;
      AUScheduleFilter auScheduleFilter1 = (AUScheduleFilter) null;
      foreach (Table data in cache.Cached)
      {
        if (Convert.ToBoolean(cache.GetValue((object) data, this._SelectedField)) && EnumerableExtensions.IsIn<PXEntryStatus>(cache.GetStatus((object) data), PXEntryStatus.Inserted, PXEntryStatus.Updated))
        {
          foreach (string key in (IEnumerable<string>) cache.Keys)
          {
            object obj = PXFieldState.UnwrapValue(cache.GetValueExt((object) data, key));
            auScheduleFilter1 = new AUScheduleFilter();
            if (!flag)
            {
              auScheduleFilter1.OpenBrackets = new int?(1);
              flag = true;
            }
            auScheduleFilter1.FieldName = key;
            if (obj != null)
            {
              auScheduleFilter1.Condition = new int?(1);
              auScheduleFilter1.Value = obj.ToString();
            }
            else
              auScheduleFilter1.Condition = new int?(12);
            auScheduleFilter1 = instance.Filters.Insert(auScheduleFilter1);
          }
          if (auScheduleFilter1 != null)
            auScheduleFilter1.Operator = new int?(1);
        }
      }
      if (auScheduleFilter1 != null)
      {
        auScheduleFilter1.CloseBrackets = new int?(1);
        auScheduleFilter1.Operator = new int?(0);
      }
      if (adapter.Filters != null && adapter.Filters.Length != 0)
      {
        for (int index = 0; index < adapter.Filters.Length; ++index)
        {
          PXFilterRow filter = adapter.Filters[index];
          AUScheduleFilter auScheduleFilter2 = new AUScheduleFilter();
          auScheduleFilter2.OpenBrackets = new int?(filter.OpenBrackets);
          int? nullable;
          if (index == 0)
          {
            AUScheduleFilter auScheduleFilter3 = auScheduleFilter2;
            nullable = auScheduleFilter3.OpenBrackets;
            auScheduleFilter3.OpenBrackets = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + 1) : new int?();
          }
          auScheduleFilter2.FieldName = filter.DataField;
          auScheduleFilter2.Condition = new int?((int) (filter.Condition + 1));
          if (filter.Value != null)
            auScheduleFilter2.Value = filter.Value.ToString();
          if (filter.Value2 != null)
            auScheduleFilter2.Value2 = filter.Value2.ToString();
          auScheduleFilter2.CloseBrackets = new int?(filter.CloseBrackets);
          if (index == adapter.Filters.Length - 1)
          {
            AUScheduleFilter auScheduleFilter4 = auScheduleFilter2;
            nullable = auScheduleFilter4.CloseBrackets;
            auScheduleFilter4.CloseBrackets = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + 1) : new int?();
          }
          instance.Filters.Insert(auScheduleFilter2);
        }
      }
      auSchedule2.ActionName = "ProcessAll";
      foreach (PXCache pxCache in this._Graph.Caches.Values)
        pxCache.IsDirty = false;
      throw new PXPopupRedirectException((PXGraph) instance, "Add Schedule", true);
    }
    return adapter.Get();
  }

  [PXButton(ImageKey = "DataEntry", DisplayOnMainToolbar = false)]
  [PXUIField(DisplayName = "View", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected virtual IEnumerable _ScheduleView_(PXAdapter adapter)
  {
    if (!string.IsNullOrEmpty(this._Graph.Accessinfo.ScreenID))
    {
      AUScheduleMaint instance = PXGraph.CreateInstance<AUScheduleMaint>();
      string field0 = this._Graph.Accessinfo.ScreenID.Replace(".", "");
      instance.ScheduleCurrentScreen.Current.ScreenID = field0;
      instance.Schedule.Current = (AUSchedule) instance.Schedule.Search<AUSchedule.screenID>((object) field0);
      if (instance.Schedule.Current != null)
      {
        foreach (PXCache pxCache in this._Graph.Caches.Values)
          pxCache.IsDirty = false;
        throw new PXPopupRedirectException((PXGraph) instance, "Add Schedule", true);
      }
    }
    return adapter.Get();
  }

  [PXButton(ImageKey = "Inquiry", DisplayOnMainToolbar = false)]
  [PXUIField(DisplayName = "History", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  protected virtual IEnumerable _ScheduleHistory_(PXAdapter adapter)
  {
    AUScheduleExecutionMaint instance = PXGraph.CreateInstance<AUScheduleExecutionMaint>();
    string screenID = this._Graph.Accessinfo.ScreenID.Replace(".", "");
    AUSchedule auSchedule = PXDatabase.Select<AUSchedule>().Where<AUSchedule>((Expression<System.Func<AUSchedule, bool>>) (s => s.ScreenID == screenID)).FirstOrDefault<AUSchedule>();
    instance.Filter.Current.ScheduleID = (int?) auSchedule?.ScheduleID;
    throw new PXRedirectRequiredException((PXGraph) instance, true, "ScheduleHistory");
  }

  public class ProcessingFilterHeader : FilterHeader
  {
  }
}
