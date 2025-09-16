// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.ScheduleProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Maintenance;
using PX.Data.PushNotifications;
using PX.Logging.Sinks.SystemEventsDbSink;
using PX.SM;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Threading;

#nullable enable
namespace PX.Data.Process;

internal sealed class ScheduleProcessor : IScheduleProcessor
{
  private readonly 
  #nullable disable
  IScheduledJobHandlerProvider _jobHandlerProvider;
  private readonly ICurrentUserInformationProvider _currentUserInformationProvider;
  private readonly IScheduleProvider _scheduleProvider;
  private readonly Func<PXDatabaseProvider> _databaseProviderFactory;
  private readonly ILogger _logger;
  private readonly ScheduleProcessor.ISettings _settings;
  private readonly Guid _hostID;
  private readonly AutoResetEvent _changed;
  private readonly List<int> _skippedScheduleIdLogged = new List<int>();
  private readonly IPXLicensePolicy _licensePolicy;
  /// <summary>User name with high privileges to get required data</summary>
  internal const string IMPERSONATION_CONTEXT_USER_NAME = "admin";

  public ScheduleProcessor(
    ICurrentUserInformationProvider currentUserInformationProvider,
    IScheduleProvider scheduleProvider,
    Func<PXDatabaseProvider> databaseProviderFactory,
    ILogger logger,
    ScheduleProcessor.ISettings settings,
    IPXLicensePolicy licensePolicy,
    IScheduledJobHandlerProvider jobHandlerProvider)
  {
    this._currentUserInformationProvider = currentUserInformationProvider ?? throw new ArgumentNullException(nameof (currentUserInformationProvider));
    this._scheduleProvider = scheduleProvider ?? throw new ArgumentNullException(nameof (scheduleProvider));
    this._databaseProviderFactory = databaseProviderFactory ?? throw new ArgumentNullException(nameof (databaseProviderFactory));
    this._logger = logger ?? throw new ArgumentNullException(nameof (logger));
    this._settings = settings ?? throw new ArgumentNullException(nameof (settings));
    this._licensePolicy = licensePolicy;
    this._jobHandlerProvider = jobHandlerProvider ?? throw new ArgumentException(nameof (jobHandlerProvider));
    this._hostID = Guid.NewGuid();
    this._changed = new AutoResetEvent(false);
  }

  public void Start(WindowsIdentity identity, CancellationToken token)
  {
    if (token.WaitHandle.WaitOne(this._settings.FirstRunDelay))
      return;
    WindowsImpersonationContext impersonationContext = identity.Impersonate();
    try
    {
      using (this._scheduleProvider.Subscribe((System.Action<object>) (o => this._changed.Set()), (object) null))
      {
        while (!token.IsCancellationRequested)
          this.Execute(token);
      }
    }
    finally
    {
      impersonationContext.Undo();
    }
  }

  public void ClearLoggedSkippedSchedules()
  {
    lock (this._skippedScheduleIdLogged)
      this._skippedScheduleIdLogged.Clear();
  }

  private void Execute(CancellationToken token)
  {
    try
    {
      IEnumerable<IPXMultiDatabaseUser> multiDatabaseUsers = (IEnumerable<IPXMultiDatabaseUser>) PXAccess.GetMultiDatabaseUsers();
      if (multiDatabaseUsers != null)
      {
        this.WatchMulti(multiDatabaseUsers.Select<IPXMultiDatabaseUser, string>((Func<IPXMultiDatabaseUser, string>) (u => u.GetCompanyID())).Distinct<string>().ToArray<string>(), true, token);
      }
      else
      {
        string[] companies = this._databaseProviderFactory().Companies;
        if (companies.Length == 0)
          this.WatchSingle(token);
        else
          this.WatchMulti(companies, false, token);
      }
    }
    catch (Exception ex)
    {
      switch (ex)
      {
        case ThreadAbortException _ when Environment.HasShutdownStarted || AppDomain.CurrentDomain.IsFinalizingForUnload():
          try
          {
            this._logger.ForSystemEvents("Scheduler", "Scheduler_SchedulerStoppedEventId").Warning("Automation Scheduler has been aborted");
            goto label_10;
          }
          catch
          {
            goto label_10;
          }
        case ThreadAbortException _:
label_10:
          if (token.IsCancellationRequested)
            break;
          token.WaitHandle.WaitOne(this._settings.OnErrorDelay);
          break;
        default:
          try
          {
            this._logger.ForSystemEvents("Scheduler", "Scheduler_SchedulerThrewExceptionEventId").Error(ex, "Automation Scheduler has failed with an exception");
            goto label_10;
          }
          catch
          {
            goto label_10;
          }
      }
    }
  }

  private bool IsElectionWon()
  {
    PXDatabaseProvider databaseProvider = this._databaseProviderFactory();
    ISqlDialect sqlDialect = databaseProvider.SqlDialect;
    string getDate = sqlDialect.GetDate;
    if (databaseProvider.Ensure(typeof (ScheduleProcessor.AUScheduleHost), new PXDataFieldAssign[2]
    {
      new PXDataFieldAssign("HostID", PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) this._hostID),
      new PXDataFieldAssign("LastProcessed", PXDbType.DirectExpression, new int?(8), (object) getDate)
    }, new PXDataField[0]))
      return true;
    if (databaseProvider.Update(typeof (ScheduleProcessor.AUScheduleHost), (PXDataFieldParam) new PXDataFieldAssign("LastProcessed", PXDbType.DirectExpression, new int?(8), (object) getDate), (PXDataFieldParam) new PXDataFieldRestrict("HostID", PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) this._hostID)))
      return true;
    return databaseProvider.Update(typeof (ScheduleProcessor.AUScheduleHost), (PXDataFieldParam) new PXDataFieldAssign("HostID", PXDbType.UniqueIdentifier, new int?(16 /*0x10*/), (object) this._hostID), (PXDataFieldParam) new PXDataFieldAssign("LastProcessed", PXDbType.DirectExpression, new int?(8), (object) getDate), (PXDataFieldParam) new PXDataFieldRestrict("LastProcessed", PXDbType.DirectExpression, new int?(8), (object) sqlDialect.scriptDateAdd<DateDiff.minute>(getDate, -6), PXComp.LT));
  }

  private int? EnsureBranch(PXGraph graph, int? scheduleBranchId)
  {
    int? branchID = graph.Accessinfo.BranchID;
    if (!branchID.HasValue || scheduleBranchId.HasValue)
    {
      if (scheduleBranchId.HasValue)
      {
        int? nullable1 = scheduleBranchId;
        int? nullable2 = branchID;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          goto label_3;
      }
      string userName = this._currentUserInformationProvider.GetUserName();
      int[] array = this._currentUserInformationProvider.GetActiveBranches().Select<BranchInfo, int>((Func<BranchInfo, int>) (i => i.Id)).ToArray<int>();
      if (array.Length == 0)
        return branchID;
      if (scheduleBranchId.HasValue && ((IEnumerable<int>) array).Contains<int>(scheduleBranchId.Value))
      {
        PXContext.SetBranchID(scheduleBranchId);
        return scheduleBranchId;
      }
      PXSelectBase<UserPreferences, PXSelectJoin<UserPreferences, InnerJoin<Users, On<Users.pKID, Equal<UserPreferences.userID>>>, Where<Users.username, Equal<Required<Users.username>>>>.Config>.Clear(graph);
      PXResultset<UserPreferences> pxResultset = PXSelectBase<UserPreferences, PXSelectJoin<UserPreferences, InnerJoin<Users, On<Users.pKID, Equal<UserPreferences.userID>>>, Where<Users.username, Equal<Required<Users.username>>>>.Config>.Select(graph, (object) userName);
      if (pxResultset != null && pxResultset.Count > 0)
        branchID = ((UserPreferences) pxResultset[0][typeof (UserPreferences)]).DefBranchID;
      if (!branchID.HasValue || !((IEnumerable<int>) array).Contains<int>(branchID.Value))
        branchID = new int?(array[0]);
      PXContext.SetBranchID(branchID);
      return branchID;
    }
label_3:
    return branchID;
  }

  private void WatchSingle(CancellationToken token)
  {
    Dictionary<int, object> dictionary = new Dictionary<int, object>();
    while (!token.IsCancellationRequested)
    {
      PXDatabaseProvider databaseProvider = this._databaseProviderFactory();
      try
      {
        if (databaseProvider.Companies.Length != 0)
          break;
      }
      catch
      {
        break;
      }
      System.DateTime nextrun;
      try
      {
        nextrun = PXTimeZoneInfo.Now.AddMilliseconds((double) this._settings.DefaultIntervalBetweenExecutions);
      }
      catch
      {
        break;
      }
      try
      {
        using (new PXLoginScope("admin", PXAccess.GetAdministratorRoles()))
        {
          databaseProvider.ResetCredentials();
          if (this.IsElectionWon())
          {
            ScheduleProcessor.SetUserLocale("admin");
            AUScheduleMaint instance = PXGraph.CreateInstance<AUScheduleMaint>();
            PXDatabase.SelectTimeStamp();
            IEnumerable<AUSchedule> activeSchedules = this._scheduleProvider.ActiveSchedules;
            if (activeSchedules != null)
            {
              foreach (AUSchedule schedule in activeSchedules)
              {
                object key;
                if (!dictionary.TryGetValue(schedule.ScheduleID.Value, out key))
                  dictionary[schedule.ScheduleID.Value] = (object) (__Boxed<Guid>) (key = (object) Guid.NewGuid());
                instance.Accessinfo.BranchID = this.EnsureBranch((PXGraph) instance, schedule.BranchID);
                nextrun = this.TryProcessSchedule(schedule, instance, key, nextrun);
              }
            }
          }
        }
      }
      catch
      {
      }
      int totalMilliseconds = (int) (nextrun - PXTimeZoneInfo.Now).TotalMilliseconds;
      if (totalMilliseconds > 0)
      {
        int watcherInterLoopDelay = this._settings.WatcherInterLoopDelay;
        if (totalMilliseconds > watcherInterLoopDelay)
        {
          WaitHandle.WaitAny(new WaitHandle[2]
          {
            (WaitHandle) this._changed,
            token.WaitHandle
          }, totalMilliseconds - watcherInterLoopDelay);
          token.WaitHandle.WaitOne(watcherInterLoopDelay);
        }
        else
          WaitHandle.WaitAny(new WaitHandle[2]
          {
            (WaitHandle) this._changed,
            token.WaitHandle
          }, totalMilliseconds);
      }
    }
  }

  private void WatchMulti(string[] companies, bool includesUsers, CancellationToken token)
  {
    IPXMultiDatabaseUser[] source = (IPXMultiDatabaseUser[]) null;
    Dictionary<int, object>[] dictionaryArray = (Dictionary<int, object>[]) null;
    int?[] nullableArray = new int?[companies.Length];
    while (!token.IsCancellationRequested)
    {
      PXDatabaseProvider databaseProvider = this._databaseProviderFactory();
      try
      {
        string[] second;
        if (includesUsers)
        {
          source = PXAccess.GetMultiDatabaseUsers();
          if (source == null)
            break;
          second = ((IEnumerable<IPXMultiDatabaseUser>) source).Select<IPXMultiDatabaseUser, string>((Func<IPXMultiDatabaseUser, string>) (u => u.GetCompanyID())).Distinct<string>().ToArray<string>();
        }
        else
          second = databaseProvider.Companies;
        if (second == null)
          break;
        if (!((IEnumerable<string>) companies).SequenceEqual<string>((IEnumerable<string>) second, (IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase))
          break;
      }
      catch
      {
        break;
      }
      System.DateTime nextrun;
      try
      {
        nextrun = System.DateTime.UtcNow.AddMilliseconds((double) this._settings.DefaultIntervalBetweenExecutions);
      }
      catch
      {
        break;
      }
      for (int index1 = 0; index1 < companies.Length; ++index1)
      {
        try
        {
          string userName;
          if (includesUsers)
          {
            string companyId = source[index1].GetCompanyID();
            userName = string.IsNullOrEmpty(companyId) ? source[index1].GetUserName() : $"{source[index1].GetUserName()}@{companyId}";
          }
          else
            userName = $"admin@{companies[index1]}";
          using (new PXLoginScope(userName, PXAccess.GetAdministratorRoles()))
          {
            if (includesUsers)
              source[index1].Initialize();
            databaseProvider.ResetCredentials();
            if (!includesUsers)
            {
              if (!this.IsElectionWon())
                continue;
            }
            ScheduleProcessor.SetUserLocale(userName);
            PXContext.SetBranchID(nullableArray[index1]);
            AUScheduleMaint instance = PXGraph.CreateInstance<AUScheduleMaint>();
            PXDatabase.SelectTimeStamp();
            IEnumerable<AUSchedule> activeSchedules = this._scheduleProvider.ActiveSchedules;
            if (activeSchedules != null)
            {
              if (dictionaryArray == null)
              {
                dictionaryArray = new Dictionary<int, object>[companies.Length];
                for (int index2 = 0; index2 < companies.Length; ++index2)
                {
                  if (dictionaryArray[index2] == null)
                    dictionaryArray[index2] = new Dictionary<int, object>();
                }
              }
              foreach (AUSchedule schedule in activeSchedules)
              {
                Dictionary<int, object> dictionary1 = dictionaryArray[index1];
                int? scheduleId = schedule.ScheduleID;
                int key1 = scheduleId.Value;
                object key2;
                ref object local1 = ref key2;
                if (!dictionary1.TryGetValue(key1, out local1))
                {
                  Dictionary<int, object> dictionary2 = dictionaryArray[index1];
                  scheduleId = schedule.ScheduleID;
                  int key3 = scheduleId.Value;
                  // ISSUE: variable of a boxed type
                  __Boxed<Guid> local2;
                  key2 = (object) (local2 = (ValueType) Guid.NewGuid());
                  dictionary2[key3] = (object) local2;
                }
                instance.Accessinfo.BranchID = this.EnsureBranch((PXGraph) instance, schedule.BranchID);
                nullableArray[index1] = instance.Accessinfo.BranchID;
                nextrun = this.TryProcessSchedule(schedule, instance, key2, nextrun);
              }
            }
          }
        }
        catch
        {
        }
      }
      int totalMilliseconds = (int) (nextrun - System.DateTime.UtcNow).TotalMilliseconds;
      if (totalMilliseconds > 0)
        WaitHandle.WaitAny(new WaitHandle[2]
        {
          (WaitHandle) this._changed,
          token.WaitHandle
        }, totalMilliseconds);
    }
  }

  private static void SetUserLocale(string userName)
  {
    PXLocale[] locales = PXLocalesProvider.GetLocales(userName);
    PXLocale pxLocale = locales != null ? ((IEnumerable<PXLocale>) locales).FirstOrDefault<PXLocale>() : (PXLocale) null;
    if (pxLocale == null)
      return;
    CultureInfo culture = new CultureInfo(pxLocale.Name);
    PXContext.PXIdentity.Culture = culture;
    PXContext.PXIdentity.UICulture = culture;
    LocaleInfo.SetAllCulture(culture);
  }

  private System.DateTime TryProcessSchedule(
    AUSchedule schedule,
    AUScheduleMaint graph,
    object key,
    System.DateTime nextrun)
  {
    PXAccess.Provider.ResetContextDefinitions();
    if (!PXAccess.IsScreenDisabled(schedule.ScreenID) && schedule.ShouldBeRan)
    {
      if (PXSiteLockout.GetStatus() != PXSiteLockout.Status.Locked)
      {
        System.DateTime? nextRunDateTimeUtc = schedule.NextRunDateTimeUTC;
        System.DateTime? nullable1 = nextRunDateTimeUtc;
        System.DateTime utcNow = PXTimeZoneInfo.UtcNow;
        if ((nullable1.HasValue ? (nullable1.GetValueOrDefault() <= utcNow ? 1 : 0) : 0) != 0)
        {
          try
          {
            PXContext.SetScreenID(Mask.Format(">CC.CC.CC.CC", schedule.ScreenID));
            this.ProcessSchedule(graph, schedule, key);
          }
          catch (Exception ex) when (!(ex is ThreadAbortException))
          {
            this._logger.ForSystemEvents("Scheduler", "Scheduler_SchedulerThrewExceptionEventId").ForContext("ContextScreenId", (object) schedule.ScreenID, false).ForContext("Schedule", (object) schedule, true).Error<int?, int?>(ex, "Automation Scheduler has failed with an exception ScheduleID:{ScheduleID} BranchID:{BranchID}", schedule.ScheduleID, schedule.BranchID);
          }
          finally
          {
            PXContext.SetScreenID((string) null);
          }
        }
        else
        {
          System.DateTime? nullable2 = nextRunDateTimeUtc;
          System.DateTime dateTime = nextrun;
          if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() < dateTime ? 1 : 0) : 0) != 0)
            nextrun = nextRunDateTimeUtc.Value;
        }
      }
      else
        this.AddSkippedSchedule(schedule);
    }
    return nextrun;
  }

  private void AddSkippedSchedule(AUSchedule schedule)
  {
    lock (this._skippedScheduleIdLogged)
    {
      System.DateTime? nextRunDateTimeUtc = schedule.NextRunDateTimeUTC;
      System.DateTime utcNow = PXTimeZoneInfo.UtcNow;
      if ((nextRunDateTimeUtc.HasValue ? (nextRunDateTimeUtc.GetValueOrDefault() <= utcNow ? 1 : 0) : 0) == 0 || !schedule.ScheduleID.HasValue || this._skippedScheduleIdLogged.Contains(schedule.ScheduleID.Value))
        return;
      this._skippedScheduleIdLogged.Add(schedule.ScheduleID.Value);
      this._logger.ForSystemEvents("Scheduler", "Scheduler_SchedulerSkippedEventId").ForContext("ContextScreenId", (object) "SM205020", false).ForContext("Schedule", (object) schedule, true).Warning<int?, int?>("Automation Schedule scenario has not been performed due to the database lockout ScheduleID:{ScheduleID} BranchID:{BranchID}", schedule.ScheduleID, schedule.BranchID);
    }
  }

  private void ProcessSchedule(
  #nullable enable
  AUScheduleMaint graph, AUSchedule schedule, object key)
  {
    if (!PXAccess.IsSchedulesEnabled())
      return;
    this.ResetContextSlot();
    if (PXLongOperation.GetStatus(key, out TimeSpan _, out Exception _) == PXLongRunStatus.InProcess || !this._scheduleProvider.ActiveSchedules.Any<AUSchedule>((Func<AUSchedule, bool>) (item =>
    {
      int? scheduleId1 = item.ScheduleID;
      int? scheduleId2 = schedule.ScheduleID;
      if (!(scheduleId1.GetValueOrDefault() == scheduleId2.GetValueOrDefault() & scheduleId1.HasValue == scheduleId2.HasValue))
        return false;
      int? runCntr1 = item.RunCntr;
      int? runCntr2 = schedule.RunCntr;
      return runCntr1.GetValueOrDefault() == runCntr2.GetValueOrDefault() & runCntr1.HasValue == runCntr2.HasValue;
    })))
      return;
    PXCache cache1 = graph.Schedule.Cache;
    bool? doNotDeactivate = schedule.DoNotDeactivate;
    bool flag = false;
    int? nullable1;
    int? nullable2;
    if (doNotDeactivate.GetValueOrDefault() == flag & doNotDeactivate.HasValue)
    {
      short? nullable3 = schedule.MaxAbortCount;
      nullable1 = nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?();
      int num = 0;
      if (nullable1.GetValueOrDefault() > num & nullable1.HasValue)
      {
        nullable3 = schedule.AbortCntr;
        nullable1 = nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?();
        nullable3 = schedule.MaxAbortCount;
        nullable2 = nullable3.HasValue ? new int?((int) nullable3.GetValueOrDefault()) : new int?();
        if (nullable1.GetValueOrDefault() >= nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
        {
          schedule.IsActive = new bool?(false);
          schedule.LastRunResult = "The system could not complete the execution of the scheduled process, and has aborted it multiple times. The schedule has been deactivated.";
          schedule.LastRunErrorLevel = new short?((short) 4);
          cache1.Update((object) schedule);
          graph.Persist();
          return;
        }
      }
    }
    PXLongOperation.ClearStatus(key);
    AUSchedule copy1 = (AUSchedule) cache1.CreateCopy((object) schedule);
    PXContext.SetSlot<AUSchedule>(copy1);
    graph.Clear();
    this.SetCurrentScheduleAndCollectScrenInfo(graph, schedule);
    PXGraph graph1 = graph.CreateGraph(schedule.GraphName, false);
    graph1.UID = key;
    graph1.Accessinfo.BusinessDate = schedule.NextRunDate;
    int[] branchIds = PXAccess.GetBranchIDs();
    nullable2 = schedule.BranchID;
    int? branchID = !nullable2.HasValue || branchIds == null || !((IEnumerable<int>) branchIds).Any<int>((Func<int, bool>) (b =>
    {
      int num = b;
      int? branchId = schedule.BranchID;
      int valueOrDefault = branchId.GetValueOrDefault();
      return num == valueOrDefault & branchId.HasValue;
    })) ? graph.Accessinfo.BranchID : schedule.BranchID;
    graph1.Accessinfo.BranchID = branchID;
    PXContext.SetBranchID(branchID);
    BackgroundWorkerHelper.SetupExecutionContext(PXTimeZoneInfo.FindSystemTimeZoneById(schedule.TimeZoneID));
    List<PXFilterRow> filters = new List<PXFilterRow>();
    foreach (PXResult<AUScheduleFilter> pxResult in graph.Filters.Select())
    {
      AUScheduleFilter auScheduleFilter = (AUScheduleFilter) pxResult;
      object obj1 = RelativeDatesManager.IsRelativeDatesString(auScheduleFilter.Value) ? (object) RelativeDatesManager.EvaluateAsString(auScheduleFilter.Value) : (object) auScheduleFilter.Value;
      object obj2 = RelativeDatesManager.IsRelativeDatesString(auScheduleFilter.Value2) ? (object) RelativeDatesManager.EvaluateAsString(auScheduleFilter.Value2) : (object) auScheduleFilter.Value2;
      string fieldName = auScheduleFilter.FieldName;
      nullable2 = auScheduleFilter.Condition;
      int? nullable4;
      if (!nullable2.HasValue)
      {
        nullable1 = new int?();
        nullable4 = nullable1;
      }
      else
        nullable4 = new int?(nullable2.GetValueOrDefault() - 1);
      nullable1 = nullable4;
      int condition = nullable1.Value;
      object obj3 = obj1;
      object obj4 = obj2;
      PXFilterRow pxFilterRow1 = new PXFilterRow(fieldName, (PXCondition) condition, obj3, obj4);
      PXFilterRow pxFilterRow2 = pxFilterRow1;
      nullable2 = auScheduleFilter.OpenBrackets;
      int num1 = nullable2.Value;
      pxFilterRow2.OpenBrackets = num1;
      PXFilterRow pxFilterRow3 = pxFilterRow1;
      nullable2 = auScheduleFilter.CloseBrackets;
      int num2 = nullable2.Value;
      pxFilterRow3.CloseBrackets = num2;
      PXFilterRow pxFilterRow4 = pxFilterRow1;
      nullable2 = auScheduleFilter.Operator;
      int num3 = 1;
      int num4 = nullable2.GetValueOrDefault() == num3 & nullable2.HasValue ? 1 : 0;
      pxFilterRow4.OrOperator = num4 != 0;
      filters.Add(pxFilterRow1);
    }
    string str1 = (string) null;
    try
    {
      if (!string.IsNullOrEmpty(schedule.FilterName))
      {
        PXCache cache2 = graph1.Views[schedule.FilterName].Cache;
        object copy2 = cache2.CreateCopy(cache2.Current);
        object current = cache2.Current;
        foreach (PXResult<AUScheduleFill> pxResult in graph.Fills.Select())
        {
          AUScheduleFill data = (AUScheduleFill) pxResult;
          object obj;
          if (RelativeDatesManager.IsRelativeDatesString(data.Value))
          {
            obj = (object) RelativeDatesManager.EvaluateAsDateTime(data.Value);
          }
          else
          {
            obj = (graph.Fills.Cache.GetStateExt<AUScheduleFill.value>((object) data) is PXFieldState stateExt ? stateExt.Value : (object) null) ?? (object) data.Value;
            if (data.FieldName == "Action" && stateExt is PXStringState pxStringState && pxStringState.AllowedValues != null && !((IEnumerable<object>) pxStringState.AllowedValues).Contains<object>(obj))
            {
              PXTrace.Logger.ForSystemEvents("Scheduler", "Scheduler_SchedulerThrewExceptionEventId").ForContext("ContextScreenId", (object) schedule.ScreenID, false).ForContext("Schedule", (object) schedule, true).Error<object, string, string>("An action with the {ActionName} name does not exist on the {ScreenID} form. Specify a correct action for the {ScheduleName} schedule.", obj, schedule.ScreenID, schedule.Description);
              schedule.IsActive = new bool?(false);
              cache1.Update((object) schedule);
              graph.Persist();
              return;
            }
            if (data.FieldName == "Action")
              str1 = obj?.ToString();
          }
          PXUIFieldAttribute.SetError(cache2, current, data.FieldName, (string) null);
          if ((cache2.GetStateExt(current, data.FieldName) is PXFieldState stateExt1 ? (stateExt1.Enabled ? 1 : 0) : 0) != 0)
          {
            cache2.SetValueExt(current, data.FieldName, obj);
            cache2.Update(current);
          }
        }
        cache2.RaiseRowUpdated(current, copy2);
        cache2.RaiseRowSelected(current);
      }
      string str2 = str1 ?? copy1.ActionName ?? copy1.Action;
      this._licensePolicy.SetCurrentActionData(new ActionData()
      {
        Name = str2,
        Origin = ActionOrigin.Schedule
      });
      AUSchedule auSchedule = copy1;
      short? abortCntr = auSchedule.AbortCntr;
      auSchedule.AbortCntr = abortCntr.HasValue ? new short?((short) ((int) abortCntr.GetValueOrDefault() + 1)) : new short?();
      cache1.Update((object) copy1);
      graph.Persist();
      PXContext.SetSlot<AUSchedule>(copy1);
      IScheduledJobHandler jobHandler;
      if (this._jobHandlerProvider.TryGetHandler(schedule.Action, out jobHandler))
        this.ProcessScheduledJob(key, jobHandler, graph1, copy1, filters);
      else
        ScheduleProcessor.ProcessProcessingScreen(key, graph1, copy1, filters);
    }
    finally
    {
      this._licensePolicy.SetCurrentActionData((ActionData) null);
    }
  }

  private void SetCurrentScheduleAndCollectScrenInfo(AUScheduleMaint graph, AUSchedule schedule)
  {
    graph.Schedule.Current = schedule;
    if (!string.IsNullOrEmpty(schedule.ScreenID) && !graph.IsScreenInfoCorrectlyCollected)
      throw new PXInvalidOperationException("The initialization of the {0} form failed due to an unexpected error. The execution of the schedule was canceled.", new object[1]
      {
        (object) schedule.ScreenID
      });
  }

  private void ResetContextSlot()
  {
    PXContext.ClearSlot<PXGraph.GraphStaticInfoDictionary>();
    PXContext.ClearSlot<PXCache.CacheStaticInfoDictionary>();
    PXContext.ClearSlot<PXCacheExtensionCollection>();
    PXGenericInqGrph.ResetContextDefinitions();
    PXSiteMap.Provider.ClearThreadSlot();
    this._databaseProviderFactory().SelectTimeStamp();
  }

  private void ProcessScheduledJob(
    object key,
    IScheduledJobHandler handler,
    PXGraph screen,
    AUSchedule schedule,
    List<PXFilterRow> filters)
  {
    int? savedBranchId = PXContext.GetBranchID();
    PXLongOperation.StartOperation(key, (PXToggleAsyncDelegate) (() =>
    {
      PXContext.SetSlot<AUSchedule>(schedule);
      PXContext.SetBranchID(savedBranchId);
      if (!handler.IsActive)
        return;
      handler.ProcessSchedule(screen, filters, schedule.ViewName, schedule, schedule.ScreenID);
    }));
  }

  private static void ProcessProcessingScreen(
    object key,
    PXGraph screen,
    AUSchedule schedule,
    List<PXFilterRow> filters)
  {
    int? savedBranchId = PXContext.GetBranchID();
    PXAction action = screen.Actions[schedule.ActionName];
    if (!string.IsNullOrEmpty(schedule.FilterName))
    {
      foreach (object obj in action.Press(new PXAdapter(screen.Views[schedule.FilterName])
      {
        StartRow = 0,
        MaximumRows = 1
      }))
        ;
      PXLongOperation.StartOperation(key, (PXToggleAsyncDelegate) (() =>
      {
        PXContext.SetSlot<AUSchedule>(schedule);
        PXContext.SetBranchID(savedBranchId);
        int startRow = 0;
        int totalRows = 0;
        foreach (object obj in screen.Views[schedule.ViewName].Select((object[]) null, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, filters.ToArray(), ref startRow, 0, ref totalRows))
          ;
      }));
    }
    else
    {
      foreach (object obj in action.Press(new PXAdapter(screen.Views[schedule.ViewName])
      {
        StartRow = 0,
        MaximumRows = 1,
        Filters = filters.ToArray()
      }))
        ;
    }
  }

  [Serializable]
  private class AUScheduleHost : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }

  internal interface ISettings
  {
    /// <summary>
    /// Delay before first processor execution in milliseconds
    /// </summary>
    int FirstRunDelay { get; }

    /// <summary>Delay executions after exception in milliseconds</summary>
    int OnErrorDelay { get; }

    /// <summary>Delay between watcher iterations in milliseconds</summary>
    int WatcherInterLoopDelay { get; }

    /// <summary>
    /// If there is no other scheduled tasks, watcher sleeps for that amount of time in milliseconds
    /// </summary>
    int DefaultIntervalBetweenExecutions { get; }
  }

  internal class Settings : ScheduleProcessor.ISettings
  {
    public int FirstRunDelay { get; } = 120000;

    public int OnErrorDelay { get; } = 120000;

    public int WatcherInterLoopDelay { get; } = 5000;

    public int DefaultIntervalBetweenExecutions { get; } = 300000;
  }
}
