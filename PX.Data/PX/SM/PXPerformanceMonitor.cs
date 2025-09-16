// Decompiled with JetBrains decompiler
// Type: PX.SM.PXPerformanceMonitor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using Microsoft.Extensions.Configuration;
using PX.Common;
using PX.Common.Context;
using PX.Data;
using PX.Data.Automation;
using PX.Licensing;
using PX.Logging;
using PX.Logging.Sinks.SystemEventsDbSink;
using Serilog.Events;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Hosting;

#nullable disable
namespace PX.SM;

public class PXPerformanceMonitor
{
  public static long StartupWorkingSet = 0;
  private static readonly System.DateTime AppStartDateTime = System.DateTime.Now;
  private static double _lastTotalProcessorTimeInMilliseconds;
  private static long _totalSqlTimeCumulative;
  private const int MeasureInterval = 2000;
  private static Timer _cpuUtilizationMeasureTimer;
  private const int ExpensiveTaskDetectionTimerInterval = 60000;
  private static Timer _expensiveTasksMonitoringTimer;
  private static ConcurrentDictionary<PXPerformanceInfo, object> _expensiveTasksMonitoringLoggedFlags = new ConcurrentDictionary<PXPerformanceInfo, object>();
  private static readonly Process CurrentProcess = Process.GetCurrentProcess();
  private static readonly Stopwatch MaxRequestsCountPerIntervalStopwatch = Stopwatch.StartNew();
  private const int MaxRequestsCountInterval = 60000;
  internal static int RequestsSumLastMinute;
  private static int _isProfilerDataSizeOverLimits;
  internal static string[] _logLevels = new string[5]
  {
    "Error",
    "Warning",
    "Information",
    "Debug",
    "Verbose"
  };
  internal static string[] _filterLogLevels = PXPerformanceMonitor._logLevels;
  internal static Dictionary<string, int> LogLevels = ((IEnumerable<string>) PXPerformanceMonitor._logLevels).Select((item, index) => new
  {
    item = item,
    index = index
  }).ToDictionary(_ => _.item, _ => _.index);
  internal const string LogCategoryTrace = "Trace";
  internal const string LogCategoryLinq = "LINQ";
  internal const string LogCategorySql = "SQL";
  internal const string LogCategorySlots = "Slots";
  internal const string LogCategorySchemaCache = "SchemaCache";
  internal const string LogCategoryCache = "Cache";
  internal const string LogCategoryCommerce = "Commerce";
  private static IEnumerable<IProfilerCategory> _customProfilerCategories = (IEnumerable<IProfilerCategory>) new List<IProfilerCategory>();
  internal static string[] _filterCategoryLevels = new string[7]
  {
    "Trace",
    "LINQ",
    "SQL",
    "Slots",
    "SchemaCache",
    "Cache",
    "Commerce"
  };
  internal static string[] _filterImportantExceptions = new string[5]
  {
    typeof (NullReferenceException).FullName,
    typeof (ThreadAbortException).FullName,
    typeof (ArgumentNullException).FullName,
    typeof (ArgumentOutOfRangeException).FullName,
    typeof (IndexOutOfRangeException).FullName
  };
  public static bool _IsEnabled = WebConfig.GetBool("PerformanceProfilerEnabled", false);
  public static bool _SqlProfilerEnabled = WebConfig.GetBool(nameof (SqlProfilerEnabled), false);
  public static bool IsDebuggerEnabled = WebConfig.GetBool("DebuggerEnabled", false);
  public static bool _SqlProfilerStackTraceEnabled = WebConfig.GetBool(nameof (SqlProfilerStackTraceEnabled), false);
  public static bool _SqlProfilerShowStackTrace = true;
  public static bool _SqlProfilerIncludeQueryCache = false;
  public static bool _LogExpensiveRequests = false;
  public static bool _LogImportantExceptions = false;
  public static bool _TraceEnabled = WebConfig.GetBool(nameof (TraceEnabled), false);
  public static bool _TraceExceptionsEnabled = WebConfig.GetBool(nameof (TraceExceptionsEnabled), false);
  public static int PrefetchTreshhold = WebConfig.GetInt(nameof (PrefetchTreshhold), 10000);
  public static bool FormatSQLEnabled = WebConfig.GetBool(nameof (FormatSQLEnabled), true);
  public static bool ProfilerAutoTurnOff = WebConfig.GetBool(nameof (ProfilerAutoTurnOff), false);
  public static string _FilterScreenId;
  public static string _FilterUserName;
  public static int? _FilterTimeLimit;
  public static int? _FilterSqlTime;
  public static int? _FilterSqlRowCount;
  public static int? _FilterSqlCount;
  public static string _LogLevelFilter;
  public static string _LogCategoryFilter;
  public static string _SqlMethodFilter;
  public static bool _SaveRequestsToDb = false;
  public static bool _SaveSqlToDb = false;
  public static string _UserProfilerName;
  public static System.DateTime _UserProfilerDate;
  public static int _FlushVersion;
  public static bool IsLongOperationCollectMemory = WebConfig.GetBool("LongOperationCollectMemory", false);
  public const string FirstChanceExceptionTraceType = "FirstChanceException";
  private static readonly IForwardingLevelSwitch LoggingLevelSwitch = ((LogEventLevel) 5).ToForwardingLevelSwitch();
  public static HashSet<string> LogCategoryFilterCollection = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  internal static System.DateTime ExpirationTime = System.DateTime.Now.AddDays(1.0);
  /// <summary>
  /// Expiration time in minutes for PXPerformanceInfo object in the last requests queue.
  /// </summary>
  internal static int UserExpirationMinutes = 10;
  internal static volatile ConcurrentQueue<PXPerformanceInfo> Samples = new ConcurrentQueue<PXPerformanceInfo>();
  internal static volatile ConcurrentDictionary<string, ConcurrentQueue<PXPerformanceInfo>> UserSamples = new ConcurrentDictionary<string, ConcurrentQueue<PXPerformanceInfo>>();
  internal static ConcurrentQueue<PXProfilerTraceItem> TraceItems = new ConcurrentQueue<PXProfilerTraceItem>();
  internal const string RTypeLongRunReport = "LongRun-Report";
  internal const string RTypeLongRun = "LongRun";
  internal const string RTypeScreen = "Screen";
  internal const string RTypeMobile = "Mobile";
  internal const string RTypeODATA = "ODATA";
  internal const string RTypeUIReports = "UI-Reports";
  internal const string RTypeUIGI = "UI-GI";
  internal const string RTypeAPICB = "API-CB";
  internal const string RTypeSOAP = "SOAP";
  internal const string RTypeAPI = "API";
  internal const string RTypeAPIML = "API-ML";
  internal const string RTypeAPIWebHooks = "API-WebHooks";
  internal const string RTypeAPISignalR = "API-SignalR";
  internal const string RTypeUI = "UI";
  internal const string RTypeUnknown = "Unknown";
  internal static string[] _RequestTypes = new string[14]
  {
    "LongRun-Report",
    "LongRun",
    "Screen",
    "Mobile",
    "ODATA",
    "UI-Reports",
    "UI-GI",
    "API-CB",
    "SOAP",
    "API",
    "API-ML",
    "API-SignalR",
    "UI",
    "Unknown"
  };
  internal static readonly ConcurrentDictionary<PXPerformanceInfo, object> SamplesInProgress = new ConcurrentDictionary<PXPerformanceInfo, object>();
  internal static readonly HashSet<string> _disabledHandlers = new HashSet<string>()
  {
    "chartimg.axd"
  };

  internal static int CPUUtilization { get; private set; }

  internal static bool IsProfilerDataSizeOverLimits
  {
    get => PXPerformanceMonitor._isProfilerDataSizeOverLimits != 0;
    set
    {
      Interlocked.Exchange(ref PXPerformanceMonitor._isProfilerDataSizeOverLimits, value ? 1 : 0);
    }
  }

  public static void Init()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    PXPerformanceMonitor._cpuUtilizationMeasureTimer = new Timer(PXPerformanceMonitor.\u003C\u003EO.\u003C0\u003E__CountCPUUtilization ?? (PXPerformanceMonitor.\u003C\u003EO.\u003C0\u003E__CountCPUUtilization = new TimerCallback(PXPerformanceMonitor.CountCPUUtilization)), (object) null, 2000, 2000);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    PXPerformanceMonitor._expensiveTasksMonitoringTimer = new Timer(PXPerformanceMonitor.\u003C\u003EO.\u003C1\u003E__DetectExpensiveTasks ?? (PXPerformanceMonitor.\u003C\u003EO.\u003C1\u003E__DetectExpensiveTasks = new TimerCallback(PXPerformanceMonitor.DetectExpensiveTasks)), (object) null, 60000, 60000);
    using (new PXConnectionStringScope(WebConfig.ProfilerConnectionString))
    {
      try
      {
        PXPerformanceMonitor.LoadSettings(true);
        PXDatabase.Subscribe(typeof (PerformanceMonitorMaint.SMPerformanceSettings), (PXDatabaseTableChanged) (() => PXPerformanceMonitor.LoadSettings(false)));
      }
      catch
      {
      }
    }
    PXPerformanceMonitor.IsTimersScopesEnabled = PXTelemetryInvoker.IsTimersScopesEnabled();
    HostingEnvironment.QueueBackgroundWorkItem((System.Action<CancellationToken>) (ct => PXPerformanceMonitor.EnsureProfilerDataSizeIsWithInTheLimit()));
    PXPerformanceMonitor._customProfilerCategories = ServiceLocator.Current.GetAllInstances<IProfilerCategory>();
    List<string> list = ((IEnumerable<string>) PXPerformanceMonitor._filterCategoryLevels).ToList<string>();
    list.AddRange(PXPerformanceMonitor._customProfilerCategories.Select<IProfilerCategory, string>((Func<IProfilerCategory, string>) (category => category.GetCategoryName())));
    PXPerformanceMonitor._filterCategoryLevels = list.ToArray();
  }

  internal static void EnsureProfilerDataSizeIsWithInTheLimit()
  {
    if (!(PXPerformanceMonitor.IsProfilerDataSizeOverLimits = PXPerformanceMonitor.CalculateIsProfilerDataSizeOverLimits()))
      return;
    PXPerformanceMonitor.DisableRequestProfiler();
  }

  private static bool CalculateIsProfilerDataSizeOverLimits()
  {
    return PXDatabase.Provider.GetTableDataSize("SMPerformanceInfo").DataBytes + PXDatabase.Provider.GetTableDataSize("SMPerformanceInfoSQL").DataBytes + PXDatabase.Provider.GetTableDataSize("SMPerformanceInfoSQLText").DataBytes + PXDatabase.Provider.GetTableDataSize("SMPerformanceInfoStackTrace").DataBytes + PXDatabase.Provider.GetTableDataSize("SMPerformanceInfoTraceEvents").DataBytes + PXDatabase.Provider.GetTableDataSize("SMPerformanceInfoTraceMessages").DataBytes >= WebConfig.ProfilerDataSizeLimit;
  }

  private static void DetectExpensiveTasks(object state)
  {
    foreach (PXPerformanceInfo key in ConcurrentDictionaryExtensions.KeysExt<PXPerformanceInfo, object>(PXPerformanceMonitor.SamplesInProgress))
    {
      if (key.Timer.ElapsedMilliseconds > 60000L && !PXPerformanceMonitor._expensiveTasksMonitoringLoggedFlags.TryGetValue(key, out object _))
      {
        PXTrace.Logger.ForSystemEvents("System", "System_ExpensiveTaskRunningEventId").ForContext("ContextScreenId", (object) key.ScreenId, false).ForContext("ContextUserIdentity", (object) key.UserId, false).ForContext("CurrentCompany", (object) key.CompanyName, false).Warning("Time-consuming task has started ElapsedTime:{ElapsedTime}, SqlTimer:{SqlTimer}, SqlCounter:{SqlCounter}, CommandName:{CommandName}", new object[4]
        {
          (object) key.Timer.Elapsed,
          (object) key.SqlTimer.Elapsed,
          (object) key.SqlCounter,
          (object) key.CommandName
        });
        PXPerformanceMonitor._expensiveTasksMonitoringLoggedFlags.TryAdd(key, (object) null);
      }
    }
  }

  private static void CountCPUUtilization(object state)
  {
    try
    {
      PXPerformanceMonitor.CPUUtilization = Convert.ToInt32((PXPerformanceMonitor.CurrentProcess.TotalProcessorTime.TotalMilliseconds - PXPerformanceMonitor._lastTotalProcessorTimeInMilliseconds) * 100.0 / (double) (Environment.ProcessorCount * 2000));
      PXPerformanceMonitor._lastTotalProcessorTimeInMilliseconds = PXPerformanceMonitor.CurrentProcess.TotalProcessorTime.TotalMilliseconds;
    }
    catch
    {
    }
  }

  internal static uint MaxTraceItemsInProgressSize { get; private set; }

  private static void AdjustLoggingLevel()
  {
    PXPerformanceMonitor.LoggingLevelSwitch.SetMinimumLevel(!PXPerformanceMonitor.IsEnabled || !PXPerformanceMonitor.TraceEnabled || string.IsNullOrEmpty(PXPerformanceMonitor._LogLevelFilter) ? (LogEventLevel) 5 : (LogEventLevel) 0);
  }

  internal static IInformingLevelSwitch InitLogging(IConfiguration cfg)
  {
    PXPerformanceMonitor.MaxTraceItemsInProgressSize = PXTraceConfig.MaxTraceCountFromConfigValue(cfg["maxTraceCount"]);
    PXPerformanceMonitor.AdjustLoggingLevel();
    return (IInformingLevelSwitch) PXPerformanceMonitor.LoggingLevelSwitch;
  }

  public static bool IsEnabled
  {
    get => PXPerformanceMonitor._IsEnabled || PXPerformanceMonitor._SaveRequestsToDb;
    set
    {
      if (PXPerformanceMonitor._IsEnabled == value)
        return;
      PXPerformanceMonitor._IsEnabled = value;
      PXPerformanceMonitor.AdjustLoggingLevel();
      PXPerformanceMonitor.SaveSettings();
    }
  }

  public static bool SqlProfilerEnabled
  {
    get => PXPerformanceMonitor._SqlProfilerEnabled || PXPerformanceMonitor._SaveSqlToDb;
    set
    {
      if (PXPerformanceMonitor._SqlProfilerEnabled == value)
        return;
      PXPerformanceMonitor._SqlProfilerEnabled = value;
      PXPerformanceMonitor.SaveSettings();
    }
  }

  public static bool SqlProfilerStackTraceEnabled
  {
    get => PXPerformanceMonitor._SqlProfilerStackTraceEnabled;
    set
    {
      if (PXPerformanceMonitor._SqlProfilerStackTraceEnabled == value)
        return;
      PXPerformanceMonitor._SqlProfilerStackTraceEnabled = value;
      PXPerformanceMonitor.SaveSettings();
    }
  }

  public static bool SqlProfilerShowStackTrace
  {
    get => PXPerformanceMonitor._SqlProfilerShowStackTrace;
    set
    {
      if (PXPerformanceMonitor._SqlProfilerShowStackTrace == value)
        return;
      PXPerformanceMonitor._SqlProfilerShowStackTrace = value;
      PXPerformanceMonitor.SaveSettings();
    }
  }

  public static bool SqlProfilerIncludeQueryCache
  {
    get => PXPerformanceMonitor._SqlProfilerIncludeQueryCache;
    set
    {
      if (PXPerformanceMonitor._SqlProfilerIncludeQueryCache == value)
        return;
      PXPerformanceMonitor._SqlProfilerIncludeQueryCache = value;
      PXPerformanceMonitor.SaveSettings();
    }
  }

  [Obsolete("This property is no longer used. See AC-330011 for details")]
  public static bool LogExpensiveRequests
  {
    get => false;
    set
    {
    }
  }

  [Obsolete("This property is no longer used. See AC-330011 for details")]
  public static bool LogImportantExceptions
  {
    get => false;
    set
    {
    }
  }

  public static bool TraceEnabled
  {
    get => PXPerformanceMonitor._TraceEnabled;
    set
    {
      if (PXPerformanceMonitor._TraceEnabled == value)
        return;
      PXPerformanceMonitor._TraceEnabled = value;
      PXPerformanceMonitor.AdjustLoggingLevel();
      PXPerformanceMonitor.SaveSettings();
    }
  }

  public static bool TraceExceptionsEnabled
  {
    get => PXPerformanceMonitor._TraceExceptionsEnabled;
    set
    {
      if (PXPerformanceMonitor._TraceExceptionsEnabled == value)
        return;
      PXPerformanceMonitor._TraceExceptionsEnabled = value;
      PXPerformanceMonitor.SaveSettings();
    }
  }

  public static string FilterScreenId
  {
    get => PXPerformanceMonitor._FilterScreenId;
    set
    {
      if (!(PXPerformanceMonitor._FilterScreenId != value))
        return;
      PXPerformanceMonitor._FilterScreenId = value;
      PXPerformanceMonitor.SaveSettings();
    }
  }

  public static string FilterUserName
  {
    get => PXPerformanceMonitor._FilterUserName;
    set
    {
      if (!(PXPerformanceMonitor._FilterUserName != value))
        return;
      PXPerformanceMonitor._FilterUserName = value;
      PXPerformanceMonitor.SaveSettings();
    }
  }

  public static int? FilterTimeLimit
  {
    get => PXPerformanceMonitor._FilterTimeLimit;
    set
    {
      int? filterTimeLimit = PXPerformanceMonitor._FilterTimeLimit;
      int? nullable = value;
      if (filterTimeLimit.GetValueOrDefault() == nullable.GetValueOrDefault() & filterTimeLimit.HasValue == nullable.HasValue)
        return;
      PXPerformanceMonitor._FilterTimeLimit = value;
      PXPerformanceMonitor.SaveSettings();
    }
  }

  public static int? FilterSqlTime
  {
    get => PXPerformanceMonitor._FilterSqlTime;
    set
    {
      int? filterSqlTime = PXPerformanceMonitor._FilterSqlTime;
      int? nullable = value;
      if (filterSqlTime.GetValueOrDefault() == nullable.GetValueOrDefault() & filterSqlTime.HasValue == nullable.HasValue)
        return;
      PXPerformanceMonitor._FilterSqlTime = value;
      PXPerformanceMonitor.SaveSettings();
    }
  }

  public static int? FilterSqlRowCount
  {
    get => PXPerformanceMonitor._FilterSqlRowCount;
    set
    {
      int? filterSqlRowCount = PXPerformanceMonitor._FilterSqlRowCount;
      int? nullable = value;
      if (filterSqlRowCount.GetValueOrDefault() == nullable.GetValueOrDefault() & filterSqlRowCount.HasValue == nullable.HasValue)
        return;
      PXPerformanceMonitor._FilterSqlRowCount = value;
      PXPerformanceMonitor.SaveSettings();
    }
  }

  public static string SqlMethodFilter
  {
    get => PXPerformanceMonitor._SqlMethodFilter;
    set
    {
      if (!(PXPerformanceMonitor._SqlMethodFilter != value))
        return;
      PXPerformanceMonitor._SqlMethodFilter = value;
      PXPerformanceMonitor.SaveSettings();
    }
  }

  public static int? FilterSqlCount
  {
    get => PXPerformanceMonitor._FilterSqlCount;
    set
    {
      int? filterSqlCount = PXPerformanceMonitor._FilterSqlCount;
      int? nullable = value;
      if (filterSqlCount.GetValueOrDefault() == nullable.GetValueOrDefault() & filterSqlCount.HasValue == nullable.HasValue)
        return;
      PXPerformanceMonitor._FilterSqlCount = value;
      PXPerformanceMonitor.SaveSettings();
    }
  }

  public static string LogLevelFilter
  {
    get => PXPerformanceMonitor._LogLevelFilter;
    set
    {
      if (!(PXPerformanceMonitor._LogLevelFilter != value) || PXContext.GetSlot<bool>("PerformanceMonitorSkipLogLevelFilter"))
        return;
      PXPerformanceMonitor._LogLevelFilter = value;
      PXPerformanceMonitor.AdjustLoggingLevel();
      PXPerformanceMonitor.SaveSettings();
    }
  }

  public static string LogCategoryFilter
  {
    get => PXPerformanceMonitor._LogCategoryFilter;
    set
    {
      if (!(PXPerformanceMonitor._LogCategoryFilter != value))
        return;
      PXPerformanceMonitor._LogCategoryFilter = value;
      PXPerformanceMonitor.SaveSettings();
    }
  }

  public static bool SaveRequestsToDb
  {
    get => PXPerformanceMonitor._SaveRequestsToDb;
    set
    {
      if (PXPerformanceMonitor._SaveRequestsToDb == value)
        return;
      PXPerformanceMonitor._SaveRequestsToDb = value;
      PXPerformanceMonitor.SaveSettings();
    }
  }

  public static bool SaveSqlToDb
  {
    get => PXPerformanceMonitor._SaveSqlToDb;
    set
    {
      if (PXPerformanceMonitor._SaveSqlToDb == value)
        return;
      PXPerformanceMonitor._SaveSqlToDb = value;
      PXPerformanceMonitor.SaveSettings();
    }
  }

  public static string UserProfilerName
  {
    get => PXPerformanceMonitor._UserProfilerName;
    set
    {
      if (!(PXPerformanceMonitor._UserProfilerName != value))
        return;
      PXPerformanceMonitor._UserProfilerName = value;
      PXPerformanceMonitor.SaveSettings();
    }
  }

  public static System.DateTime UserProfilerDate
  {
    get => PXPerformanceMonitor._UserProfilerDate;
    set
    {
      if (!(PXPerformanceMonitor._UserProfilerDate != value))
        return;
      PXPerformanceMonitor._UserProfilerDate = value;
      PXPerformanceMonitor.SaveSettings();
    }
  }

  public static int FlushVersion
  {
    get => PXPerformanceMonitor._FlushVersion;
    set
    {
      if (PXPerformanceMonitor._FlushVersion == value)
        return;
      PXPerformanceMonitor._FlushVersion = value;
      PXPerformanceMonitor.SaveSettings();
    }
  }

  public static bool IsTimersScopesEnabled { get; private set; }

  [DllImport("kernel32.dll", SetLastError = true)]
  private static extern bool GetThreadTimes(
    IntPtr hThread,
    out long lpCreationTime,
    out long lpExitTime,
    out long lpKernelTime,
    out long lpUserTime);

  [DllImport("kernel32.dll")]
  private static extern IntPtr GetCurrentThread();

  [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  private static extern IntPtr GetCurrentProcess();

  [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  public static extern bool GetProcessTimes(
    IntPtr handle,
    out long creation,
    out long exit,
    out long kernel,
    out long user);

  [DllImport("psapi.dll", SetLastError = true)]
  private static extern bool GetProcessMemoryInfo(
    IntPtr hProcess,
    out PXPerformanceMonitor.PROCESS_MEMORY_COUNTERS counters,
    uint size);

  public static long GetProcWorkingSet()
  {
    IntPtr currentProcess = PXPerformanceMonitor.GetCurrentProcess();
    PXPerformanceMonitor.PROCESS_MEMORY_COUNTERS processMemoryCounters;
    processMemoryCounters.cb = (uint) Marshal.SizeOf(typeof (PXPerformanceMonitor.PROCESS_MEMORY_COUNTERS));
    ref PXPerformanceMonitor.PROCESS_MEMORY_COUNTERS local = ref processMemoryCounters;
    int cb = (int) processMemoryCounters.cb;
    return PXPerformanceMonitor.GetProcessMemoryInfo(currentProcess, out local, (uint) cb) ? (long) processMemoryCounters.WorkingSetSize : 0L;
  }

  public static PXPerformanceInfo CurrentSample => PXContext.GetSlot<PXPerformanceInfo>();

  public static PXPerformanceInfo GetCurrentSample() => PXPerformanceMonitor.CurrentSample;

  public static void SaveSettings()
  {
    try
    {
      PerformanceMonitorSettingsMaint instance = PXGraph.CreateInstance<PerformanceMonitorSettingsMaint>();
      PerformanceMonitorMaint.SMPerformanceSettings performanceSettings1 = instance.ProfilerSettings.SelectSingle();
      PerformanceMonitorMaint.SMPerformanceSettings performanceSettings2 = new PerformanceMonitorMaint.SMPerformanceSettings()
      {
        ScreenId = PXPerformanceMonitor.FilterScreenId,
        UserId = PXPerformanceMonitor.FilterUserName,
        SqlProfiler = new bool?(PXPerformanceMonitor._SqlProfilerEnabled),
        SqlProfilerStackTrace = new bool?(PXPerformanceMonitor.SqlProfilerStackTraceEnabled),
        SqlProfilerShowStackTrace = new bool?(PXPerformanceMonitor.SqlProfilerShowStackTrace),
        TraceEnabled = new bool?(PXPerformanceMonitor.TraceEnabled),
        TraceExceptionsEnabled = new bool?(PXPerformanceMonitor.TraceExceptionsEnabled),
        ProfilerEnabled = new bool?(PXPerformanceMonitor._IsEnabled),
        TimeLimit = PXPerformanceMonitor.FilterTimeLimit,
        SqlCounterLimit = PXPerformanceMonitor.FilterSqlCount,
        FlushVersion = new int?(PXPerformanceMonitor.FlushVersion),
        SqlTimeLimit = PXPerformanceMonitor.FilterSqlTime,
        SqlRowCountLimit = PXPerformanceMonitor.FilterSqlRowCount,
        SqlMethod = PXPerformanceMonitor.SqlMethodFilter,
        LogLevel = PXPerformanceMonitor.LogLevelFilter,
        SqlProfilerIncludeQueryCache = new bool?(PXPerformanceMonitor.SqlProfilerIncludeQueryCache),
        LogExpensiveRequests = new bool?(PXPerformanceMonitor.LogExpensiveRequests),
        LogImportantExceptions = new bool?(PXPerformanceMonitor.LogImportantExceptions),
        LogCategory = PXPerformanceMonitor.LogCategoryFilter,
        SaveRequestsToDb = new bool?(PXPerformanceMonitor.SaveRequestsToDb),
        SaveSqlToDb = new bool?(PXPerformanceMonitor.SaveSqlToDb),
        UserProfilerName = PXPerformanceMonitor.UserProfilerName,
        UserProfilerDate = new System.DateTime?(PXPerformanceMonitor.UserProfilerDate)
      };
      if (!string.IsNullOrEmpty(PXPerformanceMonitor.LogCategoryFilter))
        PXPerformanceMonitor.LogCategoryFilterCollection = ((IEnumerable<string>) PXPerformanceMonitor.LogCategoryFilter.Split(',')).ToHashSet<string>();
      else
        PXPerformanceMonitor.LogCategoryFilterCollection = new HashSet<string>();
      if (performanceSettings1 == null)
      {
        performanceSettings2.LogExpensiveRequests = new bool?(false);
        performanceSettings2.LogImportantExceptions = new bool?(false);
        PXPerformanceMonitor._LogExpensiveRequests = false;
        PXPerformanceMonitor._LogImportantExceptions = false;
        PXPerformanceMonitor._LogLevelFilter = "Warning";
        instance.ProfilerSettings.Insert(performanceSettings2);
      }
      else
      {
        performanceSettings2.RecordId = performanceSettings1.RecordId;
        instance.ProfilerSettings.Update(performanceSettings2);
      }
      instance.Persist();
    }
    catch
    {
    }
  }

  public static void LoadSettings(bool init)
  {
    try
    {
      int num = 0;
      using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<PerformanceMonitorMaint.SMPerformanceSettings>(new PXDataField("ScreenID"), new PXDataField("UserId"), new PXDataField("SqlProfiler"), new PXDataField("SqlProfilerStackTrace"), new PXDataField("TraceEnabled"), new PXDataField("TraceExceptionsEnabled"), new PXDataField("ProfilerEnabled"), new PXDataField("TimeLimit"), new PXDataField("SqlCounterLimit"), new PXDataField("FlushVersion"), new PXDataField("SqlProfilerShowStackTrace"), new PXDataField("SqlTimeLimit"), new PXDataField("SqlRowCountLimit"), new PXDataField("SqlMethod"), new PXDataField("LogLevel"), new PXDataField("SqlProfilerIncludeQueryCache"), new PXDataField("LogExpensiveRequests"), new PXDataField("LogImportantExceptions"), new PXDataField("LogCategory"), new PXDataField("SaveRequestsToDb"), new PXDataField("SaveSqlToDb"), new PXDataField("UserProfilerName"), new PXDataField("UserProfilerDate")))
      {
        if (pxDataRecord == null)
        {
          PXPerformanceMonitor.SaveSettings();
        }
        else
        {
          PXPerformanceMonitor._FilterScreenId = pxDataRecord.GetString(0);
          PXPerformanceMonitor._FilterUserName = pxDataRecord.GetString(1);
          PXPerformanceMonitor._SqlProfilerEnabled = pxDataRecord.GetBoolean(2).GetValueOrDefault();
          PXPerformanceMonitor._SqlProfilerStackTraceEnabled = pxDataRecord.GetBoolean(3).GetValueOrDefault();
          PXPerformanceMonitor._TraceEnabled = pxDataRecord.GetBoolean(4).GetValueOrDefault();
          bool? boolean = pxDataRecord.GetBoolean(5);
          PXPerformanceMonitor._TraceExceptionsEnabled = ((int) boolean ?? 1) != 0;
          boolean = pxDataRecord.GetBoolean(6);
          PXPerformanceMonitor._IsEnabled = boolean.GetValueOrDefault();
          PXPerformanceMonitor._FilterTimeLimit = pxDataRecord.GetInt32(7);
          PXPerformanceMonitor._FilterSqlCount = pxDataRecord.GetInt32(8);
          num = pxDataRecord.GetInt32(9).GetValueOrDefault();
          boolean = pxDataRecord.GetBoolean(10);
          PXPerformanceMonitor._SqlProfilerShowStackTrace = boolean.GetValueOrDefault();
          PXPerformanceMonitor._FilterSqlTime = pxDataRecord.GetInt32(11);
          PXPerformanceMonitor._FilterSqlRowCount = pxDataRecord.GetInt32(12);
          PXPerformanceMonitor._SqlMethodFilter = pxDataRecord.GetString(13);
          PXPerformanceMonitor._LogLevelFilter = pxDataRecord.GetString(14);
          boolean = pxDataRecord.GetBoolean(15);
          PXPerformanceMonitor._SqlProfilerIncludeQueryCache = boolean.GetValueOrDefault();
          PXPerformanceMonitor._LogExpensiveRequests = false;
          PXPerformanceMonitor._LogImportantExceptions = false;
          PXPerformanceMonitor._LogCategoryFilter = pxDataRecord.GetString(18);
          boolean = pxDataRecord.GetBoolean(19);
          PXPerformanceMonitor._SaveRequestsToDb = boolean.GetValueOrDefault();
          boolean = pxDataRecord.GetBoolean(20);
          PXPerformanceMonitor._SaveSqlToDb = boolean.GetValueOrDefault();
          PXPerformanceMonitor._UserProfilerName = pxDataRecord.GetString(21);
          PXPerformanceMonitor._UserProfilerDate = pxDataRecord.GetDateTime(22) ?? System.DateTime.Now;
        }
      }
      if (!string.IsNullOrEmpty(PXPerformanceMonitor._LogCategoryFilter))
        PXPerformanceMonitor.LogCategoryFilterCollection = ((IEnumerable<string>) PXPerformanceMonitor._LogCategoryFilter.Split(',')).ToHashSet<string>();
      else
        PXPerformanceMonitor.LogCategoryFilterCollection = new HashSet<string>();
      if (init)
      {
        PXPerformanceMonitor._IsEnabled = WebConfig.GetBool("PerformanceProfilerEnabled", PXPerformanceMonitor._IsEnabled);
        PXPerformanceMonitor._SqlProfilerEnabled = WebConfig.GetBool("SqlProfilerEnabled", PXPerformanceMonitor._SqlProfilerEnabled);
        PXPerformanceMonitor.IsDebuggerEnabled = WebConfig.GetBool("DebuggerEnabled", PXPerformanceMonitor.IsDebuggerEnabled);
        PXPerformanceMonitor._SqlProfilerStackTraceEnabled = WebConfig.GetBool("SqlProfilerStackTraceEnabled", PXPerformanceMonitor._SqlProfilerStackTraceEnabled);
        PXPerformanceMonitor._TraceEnabled = WebConfig.GetBool("TraceEnabled", PXPerformanceMonitor._TraceEnabled);
        PXPerformanceMonitor._TraceExceptionsEnabled = WebConfig.GetBool("TraceExceptionsEnabled", PXPerformanceMonitor._TraceExceptionsEnabled);
        PXPerformanceMonitor.PrefetchTreshhold = WebConfig.GetInt("PrefetchTreshhold", PXPerformanceMonitor.PrefetchTreshhold);
        PXPerformanceMonitor.FormatSQLEnabled = WebConfig.GetBool("FormatSQLEnabled", PXPerformanceMonitor.FormatSQLEnabled);
        PXPerformanceMonitor.ProfilerAutoTurnOff = WebConfig.GetBool("ProfilerAutoTurnOff", PXPerformanceMonitor.ProfilerAutoTurnOff);
        PXPerformanceMonitor.IsLongOperationCollectMemory = WebConfig.GetBool("LongOperationCollectMemory", PXPerformanceMonitor.IsLongOperationCollectMemory);
        PXPerformanceMonitor._SaveRequestsToDb = false;
        PXPerformanceMonitor._SaveSqlToDb = false;
        PXPerformanceMonitor._UserProfilerName = (string) null;
        PXPerformanceMonitor._UserProfilerDate = System.DateTime.Now;
      }
      PXPerformanceMonitor.AdjustLoggingLevel();
      if (!init && num > PXPerformanceMonitor._FlushVersion && PXPerformanceMonitor.IsEnabled)
      {
        if (HttpContext.Current != null)
          HttpContext.Current.Items[(object) "PerformanceMonitorFlush"] = (object) true;
        else
          PXPerformanceMonitor.FlushSamples();
      }
      PXPerformanceMonitor._FlushVersion = num;
    }
    catch
    {
    }
  }

  public static void AddSample(PXPerformanceInfo s)
  {
    PXPerformanceMonitor.RemoveSampleInProgress(s);
    if (!PXPerformanceMonitor.IsEnabled && !PXPerformanceMonitor.LogExpensiveRequests && !PXPerformanceMonitor.LogImportantExceptions || SuppressPerformanceInfoScope.IsScoped)
      return;
    bool flag = System.DateTime.Now > PXPerformanceMonitor.ExpirationTime;
    if (PXPerformanceMonitor.ProfilerAutoTurnOff & flag)
    {
      PXPerformanceMonitor.SaveRequestsToDb = false;
    }
    else
    {
      if (!string.IsNullOrEmpty(PXPerformanceMonitor.UserProfilerName) && System.DateTime.Now > PXPerformanceMonitor.UserProfilerDate.AddMinutes((double) (PXPerformanceMonitor.UserExpirationMinutes * 3)))
      {
        PXPerformanceMonitor.SaveRequestsToDb = false;
        PXPerformanceMonitor.ClearUserProfiler();
      }
      if (s.InternalScreenId == "SM205070" || s.InternalScreenId == "SM201530")
        return;
      if (PXPerformanceMonitor.Samples.Count > 30)
        PXPerformanceMonitor.FlushSamples();
      PXPerformanceMonitor.Samples.Enqueue(s);
      PXPerformanceMonitor.AddUserSample(s);
    }
  }

  private static void AddUserSample(PXPerformanceInfo s)
  {
    string key = (string) null;
    try
    {
      key = PXAccess.GetUserName();
    }
    catch
    {
    }
    if (string.IsNullOrEmpty(key) || !string.IsNullOrEmpty(s.CommandTarget) && s.CommandTarget.Contains("pnlProfiler"))
      return;
    try
    {
      PXPerformanceInfo result = (PXPerformanceInfo) null;
      foreach (KeyValuePair<string, ConcurrentQueue<PXPerformanceInfo>> userSample in PXPerformanceMonitor.UserSamples)
      {
        ConcurrentQueue<PXPerformanceInfo> concurrentQueue = userSample.Value;
        while (!concurrentQueue.IsEmpty)
        {
          concurrentQueue.TryPeek(out result);
          if (result.StartTimeLocal.AddMinutes((double) PXPerformanceMonitor.UserExpirationMinutes) < System.DateTime.Now)
            concurrentQueue.TryDequeue(out result);
          else
            break;
        }
      }
      if (PXSiteMap.Provider.FindSiteMapNodeByScreenID("SM205070") == null)
      {
        ConcurrentQueue<PXPerformanceInfo> concurrentQueue = (ConcurrentQueue<PXPerformanceInfo>) null;
        PXPerformanceMonitor.UserSamples.TryRemove(key, out concurrentQueue);
      }
      else
      {
        if (!PXPerformanceMonitor.UserSamples.ContainsKey(key))
          PXPerformanceMonitor.UserSamples[key] = new ConcurrentQueue<PXPerformanceInfo>();
        ConcurrentQueue<PXPerformanceInfo> userSample = PXPerformanceMonitor.UserSamples[key];
        while (userSample.Count > 4)
          userSample.TryDequeue(out result);
        userSample.Enqueue(s);
      }
    }
    catch
    {
    }
  }

  internal static List<PXPerformanceInfo> GetUserSamples(string userName)
  {
    List<PXPerformanceInfo> userSamples = new List<PXPerformanceInfo>();
    if (!PXPerformanceMonitor.UserSamples.ContainsKey(userName))
      return userSamples;
    foreach (PXPerformanceInfo pxPerformanceInfo in PXPerformanceMonitor.UserSamples[userName])
      userSamples.Add(pxPerformanceInfo);
    return userSamples;
  }

  [Obsolete("This method is obsolete and remains for the backward compatibility. It will be removed in future versions of Acumatica.Please use the AddTrace overload with additional parameter for DAC descriptor info.")]
  public static void AddTrace(
    string source,
    string traceType,
    string message,
    string stackTrace,
    string exceptionType,
    string eventDetails,
    string messageTemplate)
  {
    PXPerformanceMonitor.AddTrace(source, traceType, message, stackTrace, exceptionType, eventDetails, messageTemplate, (string) null);
  }

  public static void AddTrace(
    string source,
    string traceType,
    string message,
    string stackTrace,
    string exceptionType,
    string eventDetails,
    string messageTemplate,
    string dacDescriptorInfo)
  {
    if (!PXPerformanceMonitor.IsEnabled && !PXPerformanceMonitor.TraceEnabled && !PXPerformanceMonitor.TraceExceptionsEnabled && !PXPerformanceMonitor.LogImportantExceptions || SuppressPerformanceInfoScope.IsScoped || string.IsNullOrEmpty(traceType))
      return;
    PXPerformanceInfo currentSample = PXPerformanceMonitor.CurrentSample;
    if (currentSample != null)
    {
      currentSample.AddTrace(source, traceType, message, stackTrace, exceptionType, eventDetails, messageTemplate, dacDescriptorInfo);
    }
    else
    {
      string traceCategory = PXPerformanceMonitor.GetTraceCategory(messageTemplate);
      if (!PXPerformanceMonitor.FilterTraceItem(source, traceType, traceCategory))
        return;
      PXProfilerTraceItem profilerTraceItem = new PXProfilerTraceItem()
      {
        Source = source,
        EventType = traceType,
        Message = message,
        Category = traceCategory,
        EventDateTime = System.DateTime.Now,
        StackTrace = stackTrace,
        ExceptionType = exceptionType,
        EventDetails = eventDetails
      };
      if (traceType.Equals("FirstChanceException"))
        profilerTraceItem.Category = (string) null;
      profilerTraceItem.MessageId = System.Math.Abs(PXReflectionSerializer.GetStableHash(profilerTraceItem.Message));
      if (!string.IsNullOrEmpty(profilerTraceItem.StackTrace))
        profilerTraceItem.TraceId = new int?(System.Math.Abs(PXReflectionSerializer.GetStableHash(profilerTraceItem.StackTrace)));
      while (PXPerformanceMonitor.TraceItems.Count > 10000)
        PXPerformanceMonitor.TraceItems.TryDequeue(out PXProfilerTraceItem _);
      PXPerformanceMonitor.TraceItems.Enqueue(profilerTraceItem);
    }
  }

  internal static string GetTraceCategory(string messageTemplate)
  {
    if (messageTemplate != null && messageTemplate.StartsWith("ProfileSlots"))
      return "Slots";
    if (messageTemplate != null && messageTemplate.StartsWith("{CommerceCaption}:"))
      return "Commerce";
    if (messageTemplate != null)
    {
      switch (messageTemplate.Length)
      {
        case 5:
          if (messageTemplate == "{SQL}")
            return "SQL";
          goto label_25;
        case 6:
          if (messageTemplate == "{Type}")
            break;
          goto label_25;
        case 12:
          if (messageTemplate == "{Subscriber}")
            break;
          goto label_25;
        case 25:
          switch (messageTemplate[0])
          {
            case 'R':
              if (messageTemplate == "Remotion Visit {NodeType}")
                goto label_21;
              goto label_25;
            case '[':
              if (messageTemplate == "[LINQ]: {SQL}{Parameters}")
                goto label_21;
              goto label_25;
            default:
              goto label_25;
          }
        case 32 /*0x20*/:
          switch (messageTemplate[27])
          {
            case 'N':
              if (messageTemplate == "Remotion Visit {NodeType} {Name}")
                goto label_21;
              goto label_25;
            case 'T':
              if (messageTemplate == "Remotion Visit {NodeType} {Type}")
                goto label_21;
              goto label_25;
            default:
              goto label_25;
          }
        case 33:
          if (messageTemplate == "Remotion Visit {NodeType} {Value}")
            goto label_21;
          goto label_25;
        case 47:
          if (messageTemplate == "Remotion Visit {NodeType} {Name} of type {Type}")
            goto label_21;
          goto label_25;
        case 51:
          if (messageTemplate == "TryGetLockedTableHeader call for {TableName} table.")
            goto label_23;
          goto label_25;
        case 54:
          if (messageTemplate == "Cache {CacheType} has inserted record with keys {keys}")
            return "Cache";
          goto label_25;
        case 55:
          if (messageTemplate == "Attempt to invalidate {TablesToInvalidateCount} tables.")
            goto label_23;
          goto label_25;
        default:
          goto label_25;
      }
      return "Slots";
label_21:
      return "LINQ";
label_23:
      return "SchemaCache";
    }
label_25:
    return PXPerformanceMonitor.GetCustomOrDefaultTraceCategory(messageTemplate);
  }

  private static string GetCustomOrDefaultTraceCategory(string messageTemplate)
  {
    foreach (IProfilerCategory profilerCategory in PXPerformanceMonitor._customProfilerCategories)
    {
      if (profilerCategory.IsMessageMatchedToCategory(messageTemplate))
        return profilerCategory.GetCategoryName();
    }
    return "Trace";
  }

  internal static bool FilterTraceItem(
    string source,
    string traceType,
    string category,
    PXPerformanceInfo sample = null)
  {
    return sample != null && sample.IsExpensive && traceType.Equals("FirstChanceException") || (!traceType.Equals("FirstChanceException") || PXPerformanceMonitor.TraceExceptionsEnabled || PXPerformanceMonitor.LogImportantExceptions) && (sample == null || sample.IsImportant || !traceType.Equals("FirstChanceException") || PXPerformanceMonitor.TraceExceptionsEnabled) && (sample != null && sample.IsImportant && traceType.Equals("FirstChanceException") && PXPerformanceMonitor.LogImportantExceptions || !PXPerformanceMonitor.LogLevels.ContainsKey(traceType) || !string.IsNullOrEmpty(PXPerformanceMonitor.LogLevelFilter) && PXPerformanceMonitor.TraceEnabled && (string.IsNullOrEmpty(PXPerformanceMonitor.LogLevelFilter) || PXPerformanceMonitor.LogLevels[PXPerformanceMonitor.LogLevelFilter] <= PXPerformanceMonitor.LogLevels["Warning"] || !string.IsNullOrEmpty(PXPerformanceMonitor.LogCategoryFilter)) && PXPerformanceMonitor.LogLevels[traceType] <= PXPerformanceMonitor.LogLevels[PXPerformanceMonitor.LogLevelFilter] && (category == null || PXPerformanceMonitor.LogCategoryFilterCollection.Count <= 0 || PXPerformanceMonitor.LogCategoryFilterCollection.Contains(category)));
  }

  internal static bool FilterSqlItem(PXPerformanceInfo sample, PXProfilerSqlSample sqlSample)
  {
    if (sample.IsExpensive || sample.IsImportant)
      return true;
    if (!PXPerformanceMonitor.SaveSqlToDb || sqlSample.QueryCache && !PXPerformanceMonitor.SqlProfilerIncludeQueryCache || !string.IsNullOrEmpty(PXPerformanceMonitor.SqlMethodFilter) && !sqlSample.StackTrace.Contains(PXPerformanceMonitor.SqlMethodFilter))
      return false;
    int? nullable = PXPerformanceMonitor.FilterSqlRowCount;
    if (nullable.HasValue)
    {
      nullable = PXPerformanceMonitor.FilterSqlRowCount;
      if (nullable.Value > sqlSample.RowCount.GetValueOrDefault())
        return false;
    }
    nullable = PXPerformanceMonitor.FilterSqlTime;
    if (nullable.HasValue)
    {
      nullable = PXPerformanceMonitor.FilterSqlTime;
      if ((double) nullable.Value > sqlSample.SqlTimer.Elapsed.TotalMilliseconds)
        return false;
    }
    return true;
  }

  internal static bool FilterRequestItem(PXPerformanceInfo sample)
  {
    if (PXPerformanceMonitor.LogExpensiveRequests)
    {
      bool flag = false;
      if ((sample.RequestType.Equals("UI", StringComparison.OrdinalIgnoreCase) || sample.RequestType.Equals("Screen", StringComparison.OrdinalIgnoreCase)) && sample.Timer.ElapsedMilliseconds > 20000L)
        flag = true;
      else if (sample.Timer.ElapsedMilliseconds > 60000L)
        flag = true;
      if (flag)
      {
        if (!PXPerformanceMonitor.TraceEnabled && sample.TraceItems != null)
          sample.TraceItems.RemoveAll((Predicate<PXProfilerTraceItem>) (_ => !_.EventType.Equals("FirstChanceException")));
        sample.IsExpensive = true;
        return true;
      }
    }
    if (PXPerformanceMonitor.LogImportantExceptions && sample.TraceItems != null)
    {
      bool flag = false;
      foreach (PXProfilerTraceItem traceItem in sample.TraceItems)
      {
        if (traceItem.EventType.Equals("FirstChanceException") && ((IEnumerable<string>) PXPerformanceMonitor._filterImportantExceptions).Contains<string>(traceItem.ExceptionType))
        {
          flag = true;
          break;
        }
      }
      if (flag)
      {
        if (!PXPerformanceMonitor.TraceEnabled)
          sample.TraceItems.RemoveAll((Predicate<PXProfilerTraceItem>) (_ => !_.EventType.Equals("FirstChanceException")));
        sample.IsImportant = true;
        return true;
      }
    }
    if (!PXPerformanceMonitor.SaveRequestsToDb || PXPerformanceMonitor.FilterScreenId != null && sample.ScreenId.IndexOf(PXPerformanceMonitor.FilterScreenId, StringComparison.InvariantCultureIgnoreCase) < 0 || PXPerformanceMonitor.FilterUserName != null && !PXPerformanceMonitor.FilterUserName.Equals(sample.UserId, StringComparison.InvariantCultureIgnoreCase))
      return false;
    if (PXPerformanceMonitor.FilterTimeLimit.HasValue || PXPerformanceMonitor.FilterSqlCount.HasValue)
    {
      long elapsedMilliseconds = sample.Timer.ElapsedMilliseconds;
      int? nullable1 = PXPerformanceMonitor.FilterTimeLimit;
      long? nullable2 = nullable1.HasValue ? new long?((long) nullable1.GetValueOrDefault()) : new long?();
      long valueOrDefault1 = nullable2.GetValueOrDefault();
      if (!(elapsedMilliseconds > valueOrDefault1 & nullable2.HasValue))
      {
        int sqlCounter = sample.SqlCounter;
        nullable1 = PXPerformanceMonitor.FilterSqlCount;
        int valueOrDefault2 = nullable1.GetValueOrDefault();
        if (!(sqlCounter > valueOrDefault2 & nullable1.HasValue))
          return false;
      }
    }
    return true;
  }

  public static double GetWorkingSet()
  {
    return (double) PXPerformanceMonitor.GetProcWorkingSet() / 1000000.0;
  }

  public static TimeSpan GetUpTime() => System.DateTime.Now - PXPerformanceMonitor.AppStartDateTime;

  public static TimeSpan GetTotalSqlTime()
  {
    return TimeSpan.FromTicks(PXPerformanceMonitor._totalSqlTimeCumulative + new HashSet<PXPerformanceInfo>(PXPerformanceMonitor.Samples.Concat<PXPerformanceInfo>((IEnumerable<PXPerformanceInfo>) PXPerformanceMonitor.SamplesInProgress.Keys)).Sum<PXPerformanceInfo>((Func<PXPerformanceInfo, long>) (sample => sample.SqlTimer.ElapsedTicks)));
  }

  public static TimeSpan CurrentThreadTime()
  {
    long num;
    long lpKernelTime;
    long lpUserTime;
    PXPerformanceMonitor.GetThreadTimes(PXPerformanceMonitor.GetCurrentThread(), out num, out num, out lpKernelTime, out lpUserTime);
    return new TimeSpan(lpKernelTime + lpUserTime);
  }

  public static void FlushSamples()
  {
    IEnumerable<PXPerformanceInfo> saved = PXPerformanceMonitor.RemoveSamples();
    PXPerformanceMonitor._totalSqlTimeCumulative += saved.Sum<PXPerformanceInfo>((Func<PXPerformanceInfo, long>) (sample => sample.SqlTimer.ElapsedTicks));
    PXLongOperation.ClearOperationResultsOnCompletion().StartOperationWithForceAsync(Guid.NewGuid(), (System.Action<CancellationToken>) (_param1 =>
    {
      try
      {
        PXContext.SetSlot<PXCacheExtensionCollection>(new PXCacheExtensionCollection());
        using (new PXLoginScope(PXDatabase.Companies.Length != 0 ? "admin@" + PXDatabase.Companies[0] : "admin", PXAccess.GetAdministratorRoles()))
        {
          using (new SuppressLocalizationScope())
          {
            using (new SuppressWorkflowScope())
            {
              PXContext.SetSlot<PXPerformanceInfo>((PXPerformanceInfo) null);
              PXGraph.CreateInstance<PerformanceMonitorMaint>().SaveSamples(saved);
              PXPerformanceMonitor.TraceItems = new ConcurrentQueue<PXProfilerTraceItem>();
            }
          }
        }
        PXContext.SetSlot<PXCacheExtensionCollection>(new PXCacheExtensionCollection());
      }
      catch (Exception ex)
      {
        PXTrace.WriteError(ex, "FlushSamples failed: {ErrorMessage}", (object) ex.Message);
      }
    }));
  }

  [PXInternalUseOnly]
  public static void StartUserProfiler()
  {
    PXPerformanceMonitor._SqlProfilerStackTraceEnabled = true;
    PXPerformanceMonitor._TraceEnabled = true;
    PXPerformanceMonitor._TraceExceptionsEnabled = true;
    PXPerformanceMonitor._FilterScreenId = (string) null;
    PXPerformanceMonitor._FilterUserName = (string) null;
    PXPerformanceMonitor._FilterTimeLimit = new int?();
    PXPerformanceMonitor._FilterSqlTime = new int?();
    PXPerformanceMonitor._FilterSqlRowCount = new int?();
    PXPerformanceMonitor._FilterSqlCount = new int?();
    PXPerformanceMonitor._LogLevelFilter = "Warning";
    PXPerformanceMonitor._LogCategoryFilter = "";
    PXPerformanceMonitor._SqlMethodFilter = (string) null;
    PXPerformanceMonitor._SqlProfilerIncludeQueryCache = false;
    PXPerformanceMonitor._SaveRequestsToDb = true;
    PXPerformanceMonitor._SaveSqlToDb = true;
    string str = (string) null;
    try
    {
      str = PXAccess.GetUserName();
    }
    catch
    {
    }
    PXPerformanceMonitor._UserProfilerName = str;
    PXPerformanceMonitor._UserProfilerDate = System.DateTime.Now;
    PXPerformanceMonitor.AdjustLoggingLevel();
    PXPerformanceMonitor.SaveSettings();
    PXPerformanceMonitor.LoadSettings(false);
    PXCache cach = PXGraph.CreateInstance<PerformanceMonitorMaint>().Caches[typeof (PerformanceMonitorMaint.SMPerformanceFilterRow)];
    object instance = cach.CreateInstance();
    cach.Current = instance;
    object newValue1 = (object) true;
    cach.RaiseFieldUpdating("SqlProfiler", instance, ref newValue1);
    object newValue2 = (object) true;
    cach.RaiseFieldUpdating("ProfilerEnabled", instance, ref newValue2);
  }

  [PXInternalUseOnly]
  public static void StopUserProfiler()
  {
    string str = (string) null;
    try
    {
      str = PXAccess.GetUserName();
    }
    catch
    {
    }
    if (string.IsNullOrEmpty(PXPerformanceMonitor.UserProfilerName) || !PXPerformanceMonitor.UserProfilerName.Equals(str, StringComparison.OrdinalIgnoreCase))
      return;
    PXPerformanceMonitor.FlushUserSamples();
    PXPerformanceMonitor.ClearUserProfiler();
  }

  internal static void DisableRequestProfiler()
  {
    PXPerformanceMonitor._SaveRequestsToDb = false;
    PXPerformanceMonitor._SaveSqlToDb = false;
    PXPerformanceMonitor._TraceEnabled = false;
    PXPerformanceMonitor._TraceExceptionsEnabled = false;
    PXPerformanceMonitor._LogExpensiveRequests = false;
    PXPerformanceMonitor._LogImportantExceptions = false;
    PXPerformanceMonitor.SaveSettings();
  }

  private static void ClearUserProfiler()
  {
    PXPerformanceMonitor._TraceEnabled = false;
    PXPerformanceMonitor._TraceExceptionsEnabled = false;
    PXPerformanceMonitor._SaveRequestsToDb = false;
    PXPerformanceMonitor._SaveSqlToDb = false;
    PXPerformanceMonitor._UserProfilerName = (string) null;
    PXPerformanceMonitor._UserProfilerDate = System.DateTime.Now;
    PXPerformanceMonitor.AdjustLoggingLevel();
    PXPerformanceMonitor.SaveSettings();
    PXPerformanceMonitor.LoadSettings(false);
    PXCache cach = PXGraph.CreateInstance<PerformanceMonitorMaint>().Caches[typeof (PerformanceMonitorMaint.SMPerformanceFilterRow)];
    object instance = cach.CreateInstance();
    cach.Current = instance;
    object newValue1 = (object) false;
    cach.RaiseFieldUpdating("SqlProfiler", instance, ref newValue1);
    object newValue2 = (object) false;
    cach.RaiseFieldUpdating("ProfilerEnabled", instance, ref newValue2);
  }

  internal static void FlushUserSamples()
  {
    IEnumerable<PXPerformanceInfo> saved = PXPerformanceMonitor.RemoveSamples();
    using (new PXLoginScope(PXDatabase.Companies.Length != 0 ? "admin@" + PXDatabase.Companies[0] : "admin", PXAccess.GetAdministratorRoles()))
    {
      PXContext.SetSlot<PXPerformanceInfo>((PXPerformanceInfo) null);
      PXGraph.CreateInstance<PerformanceMonitorMaint>().SaveSamples(saved);
      PXPerformanceMonitor.TraceItems = new ConcurrentQueue<PXProfilerTraceItem>();
    }
  }

  internal static void SetPeakMemory()
  {
    if (!PXPerformanceMonitor.IsEnabled)
      return;
    PXPerformanceMonitor.CurrentSample?.SetPeakMemory();
  }

  internal static IEnumerable<PXPerformanceInfo> RemoveSamples()
  {
    ConcurrentQueue<PXPerformanceInfo> samples = PXPerformanceMonitor.Samples;
    PXPerformanceMonitor.Samples = new ConcurrentQueue<PXPerformanceInfo>();
    return (IEnumerable<PXPerformanceInfo>) samples;
  }

  public static void SetScreenIdInternal(bool isLongRun = false)
  {
    PXPerformanceInfo currentSample = PXPerformanceMonitor.CurrentSample;
    if (currentSample == null)
      return;
    if (isLongRun)
    {
      if (currentSample.ScreenId != null && currentSample.ScreenId.StartsWith("~/pages/", StringComparison.OrdinalIgnoreCase) && currentSample.ScreenId.Length > 19 && currentSample.ScreenId[19] == '.' && currentSample.ScreenId[10] == '/')
        currentSample.InternalScreenId = currentSample.ScreenId.Substring(11, 8).ToUpperInvariant();
      string screenId = currentSample.ScreenId;
      if ((screenId != null ? (screenId.StartsWith("~/px.reportviewer.axd", StringComparison.OrdinalIgnoreCase) ? 1 : 0) : 0) != 0)
        currentSample.RequestType = "LongRun-Report";
      else
        currentSample.RequestType = "LongRun";
    }
    else
    {
      if (HttpContext.Current != null)
      {
        currentSample.InternalScreenId = !(HttpContext.Current.Items[(object) "PXScreenID"] is string str1) ? PXPerformanceMonitor.GetScreenIdFromReferrer(HttpContext.Current.Request.UrlReferrer?.Query) : str1.Replace(".", "");
        string str2 = PXPerformanceMonitor.RemoveTenant(HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath);
        if (str2 == null)
        {
          currentSample.RequestType = "Unknown";
          return;
        }
        if (str2.StartsWith("~/pages/", StringComparison.OrdinalIgnoreCase) || str2.StartsWith("~/ui/screen/", StringComparison.OrdinalIgnoreCase) || str2.StartsWith("~/Scripts/Screens/", StringComparison.OrdinalIgnoreCase))
        {
          currentSample.RequestType = "Screen";
          return;
        }
        if (str2.StartsWith("~/rest", StringComparison.OrdinalIgnoreCase) || str2.StartsWith("~/Mobile/", StringComparison.OrdinalIgnoreCase))
        {
          currentSample.RequestType = "Mobile";
          return;
        }
        if (str2.StartsWith("~/odata", StringComparison.OrdinalIgnoreCase) || str2.StartsWith("~/api/odata/", StringComparison.OrdinalIgnoreCase))
        {
          currentSample.RequestType = "ODATA";
          return;
        }
        if (str2.StartsWith("~/frames/reportlauncher.aspx", StringComparison.OrdinalIgnoreCase) || str2.StartsWith("~/PX.ReportViewer.axd", StringComparison.OrdinalIgnoreCase))
        {
          currentSample.RequestType = "UI-Reports";
          return;
        }
        if (str2.StartsWith("~/genericinquiry/", StringComparison.OrdinalIgnoreCase) || str2.StartsWith("~/ui/inquiry/", StringComparison.OrdinalIgnoreCase))
        {
          currentSample.RequestType = "UI-GI";
          return;
        }
        if (str2.StartsWith("~/entity/", StringComparison.OrdinalIgnoreCase))
        {
          currentSample.RequestType = "API-CB";
          return;
        }
        if (str2.StartsWith("~/soap/", StringComparison.OrdinalIgnoreCase))
        {
          currentSample.RequestType = "SOAP";
          return;
        }
        if (str2.StartsWith("~/Webhooks/", StringComparison.OrdinalIgnoreCase))
        {
          currentSample.RequestType = "API-WebHooks";
          return;
        }
        if (str2.StartsWith("~/Api/", StringComparison.OrdinalIgnoreCase) || str2.StartsWith("~/identity/", StringComparison.OrdinalIgnoreCase) || str2.StartsWith("~/powerBI/", StringComparison.OrdinalIgnoreCase) || str2.StartsWith("~/OAuthAuthenticationHandler", StringComparison.OrdinalIgnoreCase))
        {
          currentSample.RequestType = "API";
          return;
        }
        if (str2.StartsWith("~/ml/", StringComparison.OrdinalIgnoreCase))
        {
          currentSample.RequestType = "API-ML";
          return;
        }
        if (str2.StartsWith("~/signalr/", StringComparison.OrdinalIgnoreCase))
        {
          currentSample.RequestType = "API-SignalR";
          return;
        }
        if (str2.Equals("~/", StringComparison.OrdinalIgnoreCase) || str2.StartsWith("~/frames/", StringComparison.OrdinalIgnoreCase) || str2.StartsWith("~/frameset/", StringComparison.OrdinalIgnoreCase) || str2.StartsWith("~/main", StringComparison.OrdinalIgnoreCase) || str2.StartsWith("~/Controls/", StringComparison.OrdinalIgnoreCase) || str2.StartsWith("~/wiki/", StringComparison.OrdinalIgnoreCase) || str2.StartsWith("~/apiweb/wiki", StringComparison.OrdinalIgnoreCase) || str2.StartsWith("~/App_Themes/", StringComparison.OrdinalIgnoreCase) || str2.StartsWith("~/ui/", StringComparison.OrdinalIgnoreCase) || str2.StartsWith("~/Search/", StringComparison.OrdinalIgnoreCase) || str2.StartsWith("~/Pivot/", StringComparison.OrdinalIgnoreCase) || str2.StartsWith("~/externalresource/", StringComparison.OrdinalIgnoreCase) || str2.StartsWith("~/Help", StringComparison.OrdinalIgnoreCase))
        {
          currentSample.RequestType = "UI";
          return;
        }
      }
      currentSample.RequestType = "Unknown";
    }
  }

  private static string GetScreenIdFromReferrer(string q)
  {
    if (string.IsNullOrEmpty(q))
      return (string) null;
    try
    {
      string str = "ScreenId=";
      int num1 = q.IndexOf(str, StringComparison.OrdinalIgnoreCase);
      if (num1 >= 0)
      {
        int startIndex = num1 + str.Length;
        int num2 = q.IndexOf("&", startIndex, StringComparison.Ordinal);
        return num2 < 0 ? q.Substring(startIndex) : q.Substring(startIndex, num2 - startIndex);
      }
    }
    catch
    {
    }
    return (string) null;
  }

  public static void ReadCookie(HttpContext context)
  {
    if (PXContext.GetSlot<bool>("ProxyIsActive") || context.Request.HttpMethod == "GET")
      return;
    List<string> stringList = new List<string>();
    HttpCookie httpCookie = context.Request.Cookies.Get("perfMonHandled");
    if (httpCookie != null && !string.IsNullOrEmpty(httpCookie.Value))
      stringList = new List<string>((IEnumerable<string>) httpCookie.Value.Split(':'));
    try
    {
      for (int index = 0; index < context.Request.Cookies.Count; ++index)
      {
        HttpCookie cookie = context.Request.Cookies[index];
        if (cookie != null && cookie.Name == "perfMon" && cookie.Path == "/")
          PXPerformanceMonitor.ProcessValue(context, cookie.Value, stringList);
      }
    }
    finally
    {
      context.Response.SetCookie(new HttpCookie("perfMonHandled", string.Join(":", (IEnumerable<string>) stringList))
      {
        Path = "/"
      });
    }
  }

  private static void ProcessValue(HttpContext context, string v, List<string> handledIds)
  {
    if (string.IsNullOrEmpty(v))
      return;
    string str1 = v;
    char[] separator1 = new char[1]{ '|' };
    foreach (string str2 in str1.Split(separator1, StringSplitOptions.RemoveEmptyEntries))
    {
      if (!(str2 == "enabled"))
      {
        string str3 = (string) null;
        string key = (string) null;
        string screen = (string) null;
        string target = (string) null;
        int clientTime = 0;
        double serverTime = 0.0;
        System.DateTime startDateTime = System.DateTime.UtcNow;
        int? nullable = new int?(0);
        bool flag = false;
        string str4 = str2;
        char[] separator2 = new char[1]{ '+' };
        foreach (string str5 in str4.Split(separator2, StringSplitOptions.RemoveEmptyEntries))
        {
          char[] chArray = new char[1]{ ':' };
          string[] source = str5.Split(chArray);
          if (((IEnumerable<string>) source).First<string>() == "cmd")
            str3 = ((IEnumerable<string>) source).Last<string>();
          if (((IEnumerable<string>) source).First<string>() == "key")
            key = ((IEnumerable<string>) source).Last<string>();
          if (((IEnumerable<string>) source).First<string>() == "sc")
            screen = ((IEnumerable<string>) source).Last<string>();
          if (((IEnumerable<string>) source).First<string>() == "tg")
            target = ((IEnumerable<string>) source).Last<string>();
          if (((IEnumerable<string>) source).First<string>() == "dt")
            clientTime += Convert.ToInt32(((IEnumerable<string>) source).Last<string>());
          if (((IEnumerable<string>) source).First<string>() == "ct")
            nullable = new int?(Convert.ToInt32(((IEnumerable<string>) source).Last<string>()));
          try
          {
            if (((IEnumerable<string>) source).First<string>() == "st")
              serverTime = Convert.ToDouble(((IEnumerable<string>) source).Last<string>());
            if (((IEnumerable<string>) source).First<string>() == "start")
              startDateTime = new System.DateTime(long.Parse(((IEnumerable<string>) source).Last<string>()), DateTimeKind.Utc);
          }
          catch
          {
            flag = true;
          }
        }
        if (string.IsNullOrEmpty(key) || !handledIds.Contains(key))
        {
          if (!string.IsNullOrEmpty(key))
          {
            if (context != null)
            {
              string executionFilePath = context.Request.AppRelativeCurrentExecutionFilePath;
              if ((executionFilePath != null ? (executionFilePath.StartsWith("~/rest/", StringComparison.OrdinalIgnoreCase) ? 1 : 0) : 0) != 0)
              {
                PXPerformanceInfo pxPerformanceInfo = PXPerformanceMonitor.Samples.FirstOrDefault<PXPerformanceInfo>((Func<PXPerformanceInfo, bool>) (_ => _.ID == key));
                if (pxPerformanceInfo != null && (string.IsNullOrEmpty(str3) && string.IsNullOrEmpty(pxPerformanceInfo.CommandName) || string.Equals(str3, pxPerformanceInfo.CommandName, StringComparison.OrdinalIgnoreCase)))
                {
                  pxPerformanceInfo.ScriptTimeMs = new int?(clientTime);
                  continue;
                }
                continue;
              }
              if (!context.IsApiRequest())
                handledIds.Add(key);
            }
            else
              continue;
          }
          if (((string.IsNullOrEmpty(key) ? 1 : (key.Length != 32 /*0x20*/ ? 1 : 0)) | (flag ? 1 : 0)) == 0)
            PXTelemetryInvoker.LogClientTime(key, screen, target, str3, startDateTime, clientTime, serverTime, nullable ?? clientTime);
        }
      }
    }
  }

  public static void MarkRequest(HttpContext context)
  {
    PXPerformanceMonitor.MarkRequest(TypeKeyedOperationExtensions.Get<PXPerformanceInfo>(context.Slots()), context);
  }

  public static void MarkRequest(
    PXPerformanceInfo performanceInfo,
    HttpContext context,
    bool ignoreGetRequests = true)
  {
    if (!PXPerformanceMonitor.IsEnabled)
    {
      if (context.Request.Cookies["requestid"] == null)
        return;
      context.Response.SetCookie(new HttpCookie("requestid", "")
      {
        Expires = System.DateTime.Now.AddDays(-10.0)
      });
    }
    else
    {
      if (ignoreGetRequests && context.Request.HttpMethod == "GET")
        return;
      context.Response.SetCookie(new HttpCookie("requestid", performanceInfo.ID));
      context.Response.SetCookie(new HttpCookie("requeststat", $"+st:{Convert.ToInt64(performanceInfo.Timer.Elapsed.TotalMilliseconds).ToString()}+sc:{performanceInfo.ScreenId}+start:{performanceInfo.StartTime.Ticks.ToString()}+tg:{performanceInfo.CommandTarget}"));
    }
  }

  public static void MarkRequest(HttpContext context, bool ignoreGetRequests = true)
  {
    PXPerformanceInfo performanceInfo = TypeKeyedOperationExtensions.Get<PXPerformanceInfo>(context.Slots());
    if (performanceInfo == null)
      return;
    PXPerformanceMonitor.MarkRequest(performanceInfo, context, ignoreGetRequests);
  }

  public static string GuidToStr(Guid g)
  {
    byte[] byteArray = g.ToByteArray();
    return $"{byteArray[15]:X2}{byteArray[14]:X2}{byteArray[13]:X2}{byteArray[12]:X2}{byteArray[11]:X2}{byteArray[10]:X2}{byteArray[9]:X2}{byteArray[8]:X2}{byteArray[6]:X2}{byteArray[7]:X2}{byteArray[4]:X2}{byteArray[5]:X2}{byteArray[0]:X2}{byteArray[1]:X2}{byteArray[2]:X2}{byteArray[3]:X2}";
  }

  internal static void RegisterSampleInProgress(PXPerformanceInfo s)
  {
    if (s == null || !WebConfig.ProfilerMonitorThreads)
      return;
    s.WorkingThread = Thread.CurrentThread;
    PXPerformanceMonitor.SamplesInProgress.TryAdd(s, (object) null);
    PXTelemetryInvoker.AddToIncidentTelemetryHolder(s);
    PXTelemetryInvoker.AddToLicenseResourceMonitoring(s);
  }

  internal static void RemoveSampleInProgress(PXPerformanceInfo s)
  {
    if (s == null || !WebConfig.ProfilerMonitorThreads)
      return;
    PXTelemetryInvoker.CompleteInfoForLicenseResourceMonitoring(s);
    PXPerformanceMonitor.SamplesInProgress.TryRemove(s, out object _);
    if (PXPerformanceMonitor._expensiveTasksMonitoringLoggedFlags.TryRemove(s, out object _) && s.Timer.ElapsedMilliseconds > 60000L)
      PXTrace.Logger.ForSystemEvents("System", "System_ExpensiveTaskFinishedEventId").ForContext("ContextScreenId", (object) s.ScreenId, false).ForContext("ContextUserIdentity", (object) s.UserId, false).ForContext("CurrentCompany", (object) s.CompanyName, false).Information("Time-consuming task has finished ElapsedTime:{ElapsedTime}, SqlTimer:{SqlTimer}, SqlCounter:{SqlCounter}, CommandName:{CommandName}", new object[4]
      {
        (object) s.Timer.Elapsed,
        (object) s.SqlTimer.Elapsed,
        (object) s.SqlCounter,
        (object) s.CommandName
      });
    foreach (PXPerformanceInfo key in ConcurrentDictionaryExtensions.KeysExt<PXPerformanceInfo, object>(PXPerformanceMonitor._expensiveTasksMonitoringLoggedFlags))
    {
      if (!PXPerformanceMonitor.SamplesInProgress.ContainsKey(key))
        PXPerformanceMonitor._expensiveTasksMonitoringLoggedFlags.TryRemove(key, out object _);
    }
    s.WorkingThread = (Thread) null;
  }

  internal static string EnumWorkingThreads()
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (PXPerformanceInfo workingThread in PXPerformanceMonitor.GetWorkingThreads())
    {
      StackTrace stackTrace = PXStackTrace.GetStackTrace(workingThread.WorkingThread);
      stringBuilder.Append("User: ").Append(workingThread.UserId).AppendLine();
      stringBuilder.Append("Screen: ").Append(workingThread.ScreenId).AppendLine();
      stringBuilder.Append("Command: ").Append(workingThread.CommandName).AppendLine();
      stringBuilder.Append("Time, sec: ").Append((int) workingThread.Timer.Elapsed.TotalSeconds).AppendLine();
      stringBuilder.Append((object) stackTrace).AppendLine();
      stringBuilder.AppendLine().AppendLine().AppendLine();
    }
    return stringBuilder.ToString();
  }

  internal static int GetCurrentWorkingThreadsCount()
  {
    return PXPerformanceMonitor.SamplesInProgress.Count;
  }

  public static List<PXPerformanceInfo> GetWorkingThreads()
  {
    return ConcurrentDictionaryExtensions.KeysExt<PXPerformanceInfo, object>(PXPerformanceMonitor.SamplesInProgress).Where<PXPerformanceInfo>((Func<PXPerformanceInfo, bool>) (it => it.WorkingThread != Thread.CurrentThread)).OrderByDescending<PXPerformanceInfo, System.DateTime>((Func<PXPerformanceInfo, System.DateTime>) (_ => _.StartTime)).ToList<PXPerformanceInfo>();
  }

  public static bool BeginRequest(HttpRequest Request)
  {
    if (!PXPerformanceMonitor.IsEnabled && !PXPerformanceMonitor.IsDebuggerEnabled && !WebConfig.ProfilerMonitorThreads)
      return false;
    if (PXPerformanceMonitor.MaxRequestsCountPerIntervalStopwatch.Elapsed.TotalMilliseconds > 60000.0)
    {
      PXPerformanceMonitor.MaxRequestsCountPerIntervalStopwatch.Restart();
      Interlocked.Exchange(ref PXPerformanceMonitor.RequestsSumLastMinute, 0);
    }
    string lowerInvariant = (Request.AppRelativeCurrentExecutionFilePath ?? "").ToLowerInvariant();
    bool flag = lowerInvariant.EndsWith(".js") || lowerInvariant.EndsWith(".js.map") || lowerInvariant.EndsWith(".css") || lowerInvariant.EndsWith(".png") || lowerInvariant.EndsWith(".jpg") || lowerInvariant.EndsWith(".jpeg") || lowerInvariant.EndsWith(".gif") || lowerInvariant.EndsWith(".ico") || lowerInvariant.EndsWith(".svg") || lowerInvariant.EndsWith(".woff2") || lowerInvariant.EndsWith(".woff") || lowerInvariant.EndsWith(".ttf") || lowerInvariant.EndsWith(".otf") || lowerInvariant.EndsWith(".eot") || lowerInvariant.StartsWith("~/signalr/");
    if (lowerInvariant.EndsWith(".axd"))
      flag = PXPerformanceMonitor._disabledHandlers.Contains(Path.GetFileName(lowerInvariant));
    if (flag)
      return false;
    PXPerformanceInfo pxPerformanceInfo = PXContext.GetSlot<PXPerformanceInfo>();
    if (pxPerformanceInfo == null)
    {
      pxPerformanceInfo = new PXPerformanceInfo();
      PXContext.SetSlot<PXPerformanceInfo>(pxPerformanceInfo);
    }
    pxPerformanceInfo.ScreenId = PXUrl.GetScreenUrl(Request.RawUrl).ToLowerInvariant();
    if (PXPerformanceMonitor.IsApiPath(lowerInvariant))
    {
      pxPerformanceInfo.CommandName = PXPerformanceMonitor.GetApiCommandName(lowerInvariant);
      pxPerformanceInfo.ThreadTime = PXPerformanceMonitor.CurrentThreadTime();
      pxPerformanceInfo.MemBefore = GC.GetTotalMemory(false);
      pxPerformanceInfo.MemWorkingSet = PXPerformanceMonitor.GetWorkingSet();
      string header = Request.Headers["SOAPAction"];
      if (!string.IsNullOrEmpty(header))
        pxPerformanceInfo.CommandTarget = header;
    }
    PXPerformanceMonitor.SetScreenIdInternal();
    Interlocked.Increment(ref PXPerformanceMonitor.RequestsSumLastMinute);
    pxPerformanceInfo.Timer.Start();
    return true;
  }

  [PXInternalUseOnly]
  public static IDisposable CreatePerformanceMonitorScope(string commandName, string screenId)
  {
    if (!PXPerformanceMonitor.IsEnabled && !PXPerformanceMonitor.IsDebuggerEnabled && !WebConfig.ProfilerMonitorThreads)
      return (IDisposable) null;
    PXPerformanceMonitor.CreateAndSetupPerformanceInfo(commandName, screenId);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return Disposable.Create(PXPerformanceMonitor.\u003C\u003EO.\u003C2\u003E__ClosePerformanceMonitorScope ?? (PXPerformanceMonitor.\u003C\u003EO.\u003C2\u003E__ClosePerformanceMonitorScope = new System.Action(PXPerformanceMonitor.ClosePerformanceMonitorScope)));
  }

  [PXInternalUseOnly]
  public static PXPerformanceInfo CreateAndSetupPerformanceInfo(string commandName, string screenId)
  {
    PXPerformanceInfo s = new PXPerformanceInfo();
    s.CommandName = commandName;
    s.ThreadTime = PXPerformanceMonitor.CurrentThreadTime();
    s.CommandTarget = "LongRun";
    s.MemBefore = GC.GetTotalMemory(false);
    s.MemWorkingSet = PXPerformanceMonitor.GetWorkingSet();
    s.ScreenId = screenId;
    s.InternalScreenId = screenId;
    PXContext.SetSlot<PXPerformanceInfo>(s);
    PXPerformanceMonitor.SetScreenIdInternal(true);
    s.Timer.Start();
    PXPerformanceMonitor.RegisterSampleInProgress(s);
    return s;
  }

  [PXInternalUseOnly]
  public static void ClosePerformanceMonitorScope()
  {
    PXPerformanceMonitor.CompletePerformanceInfo(PXContext.GetSlot<PXPerformanceInfo>());
    PXContext.SetSlot<PXPerformanceInfo>((PXPerformanceInfo) null);
  }

  [PXInternalUseOnly]
  public static void CompletePerformanceInfo(PXPerformanceInfo performanceInfo)
  {
    if (performanceInfo == null)
      return;
    performanceInfo.Timer.Stop();
    performanceInfo.ThreadTime = PXPerformanceMonitor.CurrentThreadTime() - performanceInfo.ThreadTime;
    long totalMemory = GC.GetTotalMemory(false);
    performanceInfo.MemDelta = totalMemory - performanceInfo.MemBefore;
    PXPerformanceMonitor.AddSample(performanceInfo);
  }

  private static bool IsApiPath(string path)
  {
    return path.EndsWith(".asmx") || path.StartsWith("~/rest") || path.StartsWith("~/odata") || path.StartsWith("~/ml") || path.StartsWith("~/signalr");
  }

  private static string GetApiCommandName(string path)
  {
    if (path.StartsWith("~/rest"))
      return "RestAPI";
    if (path.StartsWith("~/odata"))
      return "OData";
    if (path.StartsWith("~/ml"))
      return "ML";
    return path.StartsWith("~/signalr") ? "SignalR" : "SoapAPI";
  }

  private static string RemoveTenant(string url)
  {
    return url != null && url.StartsWith("~/t/") ? "~" + url.Substring(url.IndexOf('/', "~/t/".Length + 1)) : url;
  }

  public static void EndRequest(HttpRequest Request)
  {
    PXPerformanceInfo slot1 = PXContext.GetSlot<PXPerformanceInfo>();
    if (slot1 != null)
    {
      try
      {
        slot1.UserId = PXAccess.GetUserName();
        slot1.TenantId = PXAccess.GetCompanyName();
      }
      catch
      {
      }
      if (PXPerformanceMonitor.IsApiPath(Request.AppRelativeCurrentExecutionFilePath ?? ""))
      {
        bool? slot2 = PXContext.GetSlot<bool?>("PerformanceMonitorThreadTime");
        bool flag = true;
        if (!(slot2.GetValueOrDefault() == flag & slot2.HasValue))
        {
          TimeSpan threadTime = slot1.ThreadTime;
          TimeSpan timeSpan = PXPerformanceMonitor.CurrentThreadTime();
          slot1.ThreadTime = timeSpan - threadTime;
        }
        slot1.SetPeakMemory();
      }
      slot1.Timer.Stop();
      PXPerformanceMonitor.AddSample(slot1);
    }
    if (HttpContext.Current == null || !HttpContext.Current.Items.Contains((object) "PerformanceMonitorFlush") || !(bool) HttpContext.Current.Items[(object) "PerformanceMonitorFlush"])
      return;
    PXPerformanceMonitor.FlushSamples();
  }

  internal static (IEnumerable requests, IEnumerable sql, IEnumerable trace) GetProfilerData(
    string userName,
    string action,
    CancellationToken cancellation)
  {
    bool flag1 = action == "exportResults";
    bool flag2 = action == "exportLastRequest";
    if (flag1 && (string.IsNullOrEmpty(PXPerformanceMonitor.UserProfilerName) || !PXPerformanceMonitor.UserProfilerName.Equals(userName, StringComparison.OrdinalIgnoreCase)))
      return ((IEnumerable) null, (IEnumerable) null, (IEnumerable) null);
    IEnumerable enumerable1 = (IEnumerable) null;
    IEnumerable enumerable2 = (IEnumerable) null;
    IEnumerable enumerable3 = (IEnumerable) null;
    using (new PXResourceGovernorSafeScope())
    {
      PerformanceMonitorMaint instance = PXGraph.CreateInstance<PerformanceMonitorMaint>();
      if (!(flag1 | flag2))
      {
        int totalRows = 1000000;
        if (!cancellation.IsCancellationRequested)
          enumerable1 = (IEnumerable) instance.Samples.SelectWindowed(0, totalRows);
        if (!cancellation.IsCancellationRequested)
          enumerable2 = (IEnumerable) instance.SqlImport.SelectWindowed(0, totalRows);
        if (!cancellation.IsCancellationRequested)
          enumerable3 = (IEnumerable) instance.TraceMessageImport.SelectWindowed(0, totalRows);
      }
      else if (flag1)
      {
        System.DateTime date = PXPerformanceMonitor.UserProfilerDate;
        PXPerformanceMonitor.StopUserProfiler();
        enumerable1 = (IEnumerable) ((IEnumerable<SMPerformanceInfo>) instance.Samples.Select<SMPerformanceInfo>()).Where<SMPerformanceInfo>((Func<SMPerformanceInfo, bool>) (_ =>
        {
          if (!(_.UserId == userName))
            return false;
          System.DateTime? requestStartTime = _.RequestStartTime;
          System.DateTime dateTime = date;
          return requestStartTime.HasValue && requestStartTime.GetValueOrDefault() >= dateTime;
        })).OrderByDescending<SMPerformanceInfo, System.DateTime?>((Func<SMPerformanceInfo, System.DateTime?>) (_ => _.RequestStartTime)).ToList<SMPerformanceInfo>();
        List<SMPerformanceInfoSQLWithTables> infoSqlWithTablesList = new List<SMPerformanceInfoSQLWithTables>();
        List<SMPerformanceInfoTraceWithMessages> traceWithMessagesList = new List<SMPerformanceInfoTraceWithMessages>();
        foreach (SMPerformanceInfo smPerformanceInfo in enumerable1)
        {
          List<SMPerformanceInfoSQLWithTables> list1 = ((IEnumerable<SMPerformanceInfoSQLWithTables>) instance.SqlImportRecord.Select<SMPerformanceInfoSQLWithTables>((object) smPerformanceInfo.RecordId)).ToList<SMPerformanceInfoSQLWithTables>();
          List<SMPerformanceInfoTraceWithMessages> list2 = ((IEnumerable<SMPerformanceInfoTraceWithMessages>) instance.TraceMessageImportRecord.Select<SMPerformanceInfoTraceWithMessages>((object) smPerformanceInfo.RecordId)).ToList<SMPerformanceInfoTraceWithMessages>();
          infoSqlWithTablesList.AddRange((IEnumerable<SMPerformanceInfoSQLWithTables>) list1);
          traceWithMessagesList.AddRange((IEnumerable<SMPerformanceInfoTraceWithMessages>) list2);
        }
        enumerable2 = (IEnumerable) infoSqlWithTablesList;
        enumerable3 = (IEnumerable) traceWithMessagesList;
      }
      else if (flag2)
      {
        List<PXPerformanceInfo> userSamples = PXPerformanceMonitor.GetUserSamples(userName);
        List<SMPerformanceInfo> smPerformanceInfoList = new List<SMPerformanceInfo>();
        List<SMPerformanceInfoSQLWithTables> infoSqlWithTablesList = new List<SMPerformanceInfoSQLWithTables>();
        List<SMPerformanceInfoTraceWithMessages> traceWithMessagesList = new List<SMPerformanceInfoTraceWithMessages>();
        int num1 = 1;
        int num2 = 1;
        int num3 = 1;
        foreach (PXPerformanceInfo sample in userSamples)
        {
          SMPerformanceInfo smPerformanceInfo = PerformanceMonitorMaint.ConvertSample(sample);
          smPerformanceInfo.RecordId = new int?(num1++);
          smPerformanceInfo.LoggedSqlCounter = smPerformanceInfo.SqlCounter;
          smPerformanceInfo.LoggedExceptionCounter = smPerformanceInfo.ExceptionCounter;
          smPerformanceInfo.LoggedEventCounter = smPerformanceInfo.EventCounter;
          smPerformanceInfoList.Add(smPerformanceInfo);
          if (sample.SqlSamples != null)
          {
            foreach (PXProfilerSqlSample sqlSample in sample.SqlSamples)
            {
              SMPerformanceInfoSQLWithTables infoSqlWithTables = PerformanceMonitorMaint.ConvertSqlSample(sqlSample);
              infoSqlWithTables.RecordId = new int?(num2++);
              infoSqlWithTables.ParentId = smPerformanceInfo.RecordId;
              infoSqlWithTablesList.Add(infoSqlWithTables);
            }
          }
          if (sample.TraceItems != null)
          {
            foreach (PXProfilerTraceItem traceItem in sample.TraceItems)
            {
              SMPerformanceInfoTraceWithMessages traceWithMessages = PerformanceMonitorMaint.ConvertTraceitem(traceItem);
              traceWithMessages.RecordId = new int?(num3++);
              traceWithMessages.ParentId = smPerformanceInfo.RecordId;
              traceWithMessagesList.Add(traceWithMessages);
            }
          }
        }
        enumerable1 = (IEnumerable) smPerformanceInfoList;
        enumerable2 = (IEnumerable) infoSqlWithTablesList;
        enumerable3 = (IEnumerable) traceWithMessagesList;
      }
    }
    return (enumerable1, enumerable2, enumerable3);
  }

  public class traceEventType : Constant<string>
  {
    public traceEventType()
      : base("FirstChanceException")
    {
    }
  }

  [StructLayout(LayoutKind.Sequential, Size = 72)]
  private struct PROCESS_MEMORY_COUNTERS
  {
    public uint cb;
    public uint PageFaultCount;
    public ulong PeakWorkingSetSize;
    public ulong WorkingSetSize;
    public ulong QuotaPeakPagedPoolUsage;
    public ulong QuotaPagedPoolUsage;
    public ulong QuotaPeakNonPagedPoolUsage;
    public ulong QuotaNonPagedPoolUsage;
    public ulong PagefileUsage;
    public ulong PeakPagefileUsage;
  }
}
