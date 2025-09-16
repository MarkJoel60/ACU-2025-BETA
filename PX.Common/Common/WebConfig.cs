// Decompiled with JetBrains decompiler
// Type: PX.Common.WebConfig
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using System.Web.Hosting;

#nullable disable
namespace PX.Common;

public static class WebConfig
{
  [PXInternalUseOnly]
  public static readonly bool IsClusterEnabled = WebConfig.GetAndValidateIsClusterEnabled(WebConfig.\u000E(), WebConfig.\u0006(), WebConfig.\u0002());
  [PXInternalUseOnly]
  public static readonly bool IsMultiSiteMode = WebConfig.\u0002();
  [PXInternalUseOnly]
  public static readonly bool EnableConcurrentSessionAccess = WebConfig.\u0008();
  [PXInternalUseOnly]
  public static readonly bool UseTypeListDebugMode = WebConfig.\u0003();
  private static bool? \u0002;
  [PXInternalUseOnly]
  public static readonly bool CalculateUsedSpace = WebConfig.GetBool(nameof (CalculateUsedSpace), true);
  [PXInternalUseOnly]
  public static readonly string ProfilerConnectionString = WebConfig.\u0002();
  [PXInternalUseOnly]
  public static readonly bool UseSharedCustomization = WebConfig.GetBool(nameof (UseSharedCustomization), false);
  [PXInternalUseOnly]
  public static readonly bool _UseRuntimeCompilation = WebConfig.GetBool(nameof (UseRuntimeCompilation), true) && !WebConfig.GetBool(nameof (CustomizationLegacyMode), false);
  [PXInternalUseOnly]
  public static readonly bool CustomizationLegacyMode = WebConfig.GetBool(nameof (CustomizationLegacyMode), false);
  [PXInternalUseOnly]
  public static readonly bool SqlOptimizeForUnknown = WebConfig.GetBool(nameof (SqlOptimizeForUnknown), true);
  [PXInternalUseOnly]
  public static readonly int SqlFlatteningMode = WebConfig.GetInt(nameof (SqlFlatteningMode), 1);
  [PXInternalUseOnly]
  public static readonly bool ClientSideComponents = WebConfig.GetBool(nameof (ClientSideComponents), true);
  [PXInternalUseOnly]
  public static readonly bool UserNameInQueryText = WebConfig.GetBool(nameof (UserNameInQueryText), false);
  [PXInternalUseOnly]
  public static readonly bool InQueryInfoComment = WebConfig.GetBool(nameof (InQueryInfoComment), true);
  internal static readonly bool SerializeSessionItems = WebConfig.\u000F();
  [PXInternalUseOnly]
  public static readonly bool ProfilerMonitorThreads = WebConfig.GetBool(nameof (ProfilerMonitorThreads), true);
  [PXInternalUseOnly]
  public static readonly bool ShareDatabaseSchemaInfo = WebConfig.GetBool(nameof (ShareDatabaseSchemaInfo), false);
  [PXInternalUseOnly]
  public static readonly bool CleanupLongOperationInfo = WebConfig.GetBool(nameof (CleanupLongOperationInfo), true);
  [PXInternalUseOnly]
  public static readonly int LongOperationInfoTimeout = WebConfig.GetInt(nameof (LongOperationInfoTimeout), 20);
  [PXInternalUseOnly]
  public static readonly bool EnablePageOpenOptimizations = WebConfig.GetBool(nameof (EnablePageOpenOptimizations), true);
  [PXInternalUseOnly]
  public static readonly long DbSizeQuota = WebConfig.GetQuota("DatabaseQuota", 0L);
  [PXInternalUseOnly]
  public static readonly bool EnableSignleRowOptimizations = WebConfig.GetBool("EnableSingleRowOptimizations", true);
  [PXInternalUseOnly]
  public static readonly bool PrefetchInSeparateConnection = WebConfig.GetBool(nameof (PrefetchInSeparateConnection), true);
  [PXInternalUseOnly]
  public static readonly bool LogResetSlots = WebConfig.GetBool(nameof (LogResetSlots), false);
  [PXInternalUseOnly]
  public static readonly bool LazyCachesEnabled = WebConfig.GetBool(nameof (LazyCachesEnabled), true);
  [PXInternalUseOnly]
  public static readonly bool CacheLoggerEnabled = WebConfig.GetBool(nameof (CacheLoggerEnabled), false);
  [PXInternalUseOnly]
  public static readonly int GenericInquiryExportToExcelRowLimit = WebConfig.GetInt(nameof (GenericInquiryExportToExcelRowLimit), 1000);
  [PXInternalUseOnly]
  public static readonly bool PerformanceTestDontResetSlots = WebConfig.GetBool(nameof (PerformanceTestDontResetSlots), false);
  [PXInternalUseOnly]
  public static readonly int MaxFullTextSearchResultCount = WebConfig.GetInt(nameof (MaxFullTextSearchResultCount), 100);
  [PXInternalUseOnly]
  public static readonly bool DisableExternalFileStorage = WebConfig.GetBool(nameof (DisableExternalFileStorage), false);
  [PXInternalUseOnly]
  public static readonly bool DisableDeleteOnExternalFileStorage = WebConfig.GetBool(nameof (DisableDeleteOnExternalFileStorage), false);
  [PXInternalUseOnly]
  public static readonly int MinQueryCacheSize = WebConfig.GetInt(nameof (MinQueryCacheSize), 5);
  [PXInternalUseOnly]
  public static readonly int MaxQueryCacheSize = WebConfig.GetInt(nameof (MaxQueryCacheSize), 20);
  [PXInternalUseOnly]
  public static readonly int MaxSingleRowQueryCacheSize = WebConfig.GetInt(nameof (MaxSingleRowQueryCacheSize), 1000);
  [PXInternalUseOnly]
  public static readonly int MaxUnloadQueryCacheSize = WebConfig.GetInt(nameof (MaxUnloadQueryCacheSize), 5);
  [PXInternalUseOnly]
  public static readonly int MaxQueryRowsCacheSize = WebConfig.GetInt(nameof (MaxQueryRowsCacheSize), 200);
  [PXInternalUseOnly]
  public static readonly bool EnableAutoNumberingInSeparateConnection = WebConfig.GetBool(nameof (EnableAutoNumberingInSeparateConnection), false);
  [PXInternalUseOnly]
  public static bool SqlLimitsEnabled = WebConfig.GetBool(nameof (SqlLimitsEnabled), false);
  [PXInternalUseOnly]
  public static readonly int MaxSqlRows = WebConfig.GetInt(nameof (MaxSqlRows), 1000000);
  [PXInternalUseOnly]
  public static readonly int MaxSqlRowsUI = WebConfig.GetInt(nameof (MaxSqlRowsUI), 300000);
  [PXInternalUseOnly]
  public static readonly int SqlTimeoutSec = WebConfig.GetInt(nameof (SqlTimeoutSec), 600);
  [PXInternalUseOnly]
  public static readonly int SqlTimeoutUISec = WebConfig.GetInt(nameof (SqlTimeoutUISec), 120);
  [PXInternalUseOnly]
  public static readonly Dictionary<string, string> SqlLimitExceptions = ((IEnumerable<string>) WebConfig.GetString(nameof (SqlLimitExceptions), "").Split(new char[1]
  {
    ','
  }, StringSplitOptions.RemoveEmptyEntries)).ToDictionary<string, string, string>(new Func<string, string>(WebConfig.\u0002.\u0002.\u0002), new Func<string, string>(WebConfig.\u0002.\u0002.\u000E));
  [PXInternalUseOnly]
  public static readonly int ProfilerCleanupLogSize = WebConfig.GetInt(nameof (ProfilerCleanupLogSize), 10000);
  [PXInternalUseOnly]
  public static readonly int ProfilerCleanupLogDays = WebConfig.GetInt(nameof (ProfilerCleanupLogDays), 3);
  [PXInternalUseOnly]
  public static readonly int ProfilerCleanupRepeatCount = WebConfig.GetInt(nameof (ProfilerCleanupRepeatCount), 1000);
  private static string \u000E;
  [PXInternalUseOnly]
  public static bool DisableExtensions = WebConfig.GetBool(nameof (DisableExtensions), false);
  [PXInternalUseOnly]
  public static bool SetDefaultExtQueue = WebConfig.GetBool(nameof (SetDefaultExtQueue), true);
  [PXInternalUseOnly]
  public static readonly string CstSolutionName = WebConfig.GetString(nameof (CstSolutionName), (string) null);
  [PXInternalUseOnly]
  public static readonly string CstWebsiteName = WebConfig.GetString(nameof (CstWebsiteName), (string) null);
  private static bool? \u0006;
  [PXInternalUseOnly]
  public static readonly bool CustomizedFieldPrefixWarningInsteadOfError = WebConfig.GetBool(nameof (CustomizedFieldPrefixWarningInsteadOfError), false);
  [PXInternalUseOnly]
  public static readonly string SharedCustomizationGroup = WebConfig.GetString(nameof (SharedCustomizationGroup), (string) null);
  [PXInternalUseOnly]
  public static bool OptimizePublishCustomization = WebConfig.GetBool(nameof (OptimizePublishCustomization), true);
  [PXInternalUseOnly]
  public static readonly bool OptimizeDBScriptsOnPublish = WebConfig.GetBool(nameof (OptimizeDBScriptsOnPublish), true);
  [PXInternalUseOnly]
  public static readonly bool LongOperationLowPriority = WebConfig.GetBool(nameof (LongOperationLowPriority), true);
  [PXInternalUseOnly]
  public static readonly bool PageRequestsHiPriority = WebConfig.GetBool(nameof (PageRequestsHiPriority), false);
  [PXInternalUseOnly]
  public static readonly bool CheckCustomizationCompatibility = WebConfig.GetBool(nameof (CheckCustomizationCompatibility), true);
  [PXInternalUseOnly]
  public static readonly bool ParallelProcessingDisabled = WebConfig.GetBool(nameof (ParallelProcessingDisabled), true);
  [PXInternalUseOnly]
  public static readonly int ParallelProcessingMaxThreads = WebConfig.GetInt(nameof (ParallelProcessingMaxThreads), 0);
  /// <summary>
  /// (Immutable) The setting that limits maximum amount of threads used for ARM reports parallel calculation.
  /// If not specified then all threads will be used.
  /// </summary>
  [PXInternalUseOnly]
  public static readonly int? ParallelArmReportsCalculationMaxThreads = WebConfig.\u0002(WebConfigurationManager.AppSettings, nameof (ParallelArmReportsCalculationMaxThreads));
  /// <summary>
  /// A flag that determines if ARM reports should use the legacy calculation approach and do parallel calculation by all report dimensions. <br />
  /// The default is <see langword="false" />.
  /// </summary>
  /// <remarks>
  /// This setting should not be used in normal circumstances. It is introduced as a safety measure for the optimization changes in ARM reports calculation. <br />
  /// The old legacy calculation consisted of nested loops over all report dimensions where each loop was parallelized with <see cref="M:System.Threading.Tasks.Parallel.For(System.Int32,System.Int32,System.Action{System.Int32})" /> method. <br />
  /// 
  /// Parallelizing more than one loop in general is not effective and the calculation was changed to pick the most suitable loop and parallelize only it.
  /// However, if some customer faces issues with the new approach then the support can enable this setting to swtich to a legacy calculation.
  /// </remarks>
  [PXInternalUseOnly]
  public static readonly bool ParallelizeAllDimensionsInArmReports = WebConfig.GetBool(nameof (ParallelizeAllDimensionsInArmReports), false);
  [PXInternalUseOnly]
  public static readonly int ParallelProcessingBatchSize = WebConfig.GetInt(nameof (ParallelProcessingBatchSize), 10);
  [PXInternalUseOnly]
  public static readonly bool IsParallelProcessingSkipExceptions = WebConfig.GetBool("IsParallelProcessingSkipBatchExceptions", false);
  [PXInternalUseOnly]
  public static readonly int ApiPerHourLimit = WebConfig.GetInt(nameof (ApiPerHourLimit), 0);
  [PXInternalUseOnly]
  public static readonly int ApiProcessingCores = WebConfig.GetInt(nameof (ApiProcessingCores), 0);
  [PXInternalUseOnly]
  public static readonly int ApiLoginsLimit = WebConfig.GetInt(nameof (ApiLoginsLimit), 0);
  [PXInternalUseOnly]
  public static readonly int LicenseUpdateIntervalMin = WebConfig.GetInt(nameof (LicenseUpdateIntervalMin), 30);
  [PXInternalUseOnly]
  public static readonly int ApiLoginsTimeout = WebConfig.GetInt(nameof (ApiLoginsTimeout), 600);
  [PXInternalUseOnly]
  public static readonly int ApiRequestTimeout = WebConfig.GetInt(nameof (ApiRequestTimeout), 600);
  [PXInternalUseOnly]
  public static readonly int ApiQueueLimit = WebConfig.GetInt(nameof (ApiQueueLimit), 20);
  [PXInternalUseOnly]
  public static readonly bool ApiDisableRequests = WebConfig.GetBool(nameof (ApiDisableRequests), false);
  [PXInternalUseOnly]
  public static readonly bool WorkflowDisableAUStepsValidation = WebConfig.GetBool(nameof (WorkflowDisableAUStepsValidation), false);
  [PXInternalUseOnly]
  public static readonly bool ApiLimitsEnabled = true;
  [PXInternalUseOnly]
  public static readonly bool CheckSqlScriptSchemes = WebConfig.GetBool(nameof (CheckSqlScriptSchemes), true);
  [PXInternalUseOnly]
  public static readonly bool ProcessingProgressDialog = WebConfig.GetBool(nameof (ProcessingProgressDialog), true);
  [PXInternalUseOnly]
  public static readonly int? MaximumAllowedSessionsCount = WebConfig.\u0002(WebConfigurationManager.AppSettings, nameof (MaximumAllowedSessionsCount));
  [PXInternalUseOnly]
  public static int ApiMaxWaitingInQueueMinutes = WebConfig.GetInt(nameof (ApiMaxWaitingInQueueMinutes), 10);
  [PXInternalUseOnly]
  public static double ApiPauseThreshold = (double) WebConfig.GetInt(nameof (ApiPauseThreshold), 50) / 100.0;
  [PXInternalUseOnly]
  public static readonly bool SkipXSRFErrors = WebConfig.GetBool(nameof (SkipXSRFErrors), false);
  [PXInternalUseOnly]
  public static readonly bool EnableWorkflow = WebConfig.GetBool(nameof (EnableWorkflow), true);
  [PXInternalUseOnly]
  public static readonly bool StoreCachedValidation = WebConfig.GetBool("StoreResultValidation", false);
  [PXInternalUseOnly]
  public static readonly bool EnableWorkflowValidationOnStartup = WebConfig.GetBool(nameof (EnableWorkflowValidationOnStartup), false);
  [PXInternalUseOnly]
  public static readonly string CustomInstallationPrefix = WebConfig.GetString(nameof (CustomInstallationPrefix), string.Empty);
  /// <summary>Identificator for Portal instances. Not used in ERP.</summary>
  [PXInternalUseOnly]
  public static readonly string PortalSiteID = WebConfig.GetString(nameof (PortalSiteID), (string) null);
  [PXInternalUseOnly]
  public static readonly bool DisableStraightJoin = WebConfig.GetBool(nameof (DisableStraightJoin), false);
  [PXInternalUseOnly]
  public static readonly int MaxRecursionLevel = WebConfig.GetInt(nameof (MaxRecursionLevel), 3000);
  [PXInternalUseOnly]
  public static readonly bool EnableReportIncident = WebConfig.GetBool(nameof (EnableReportIncident), true);
  [PXInternalUseOnly]
  public static readonly bool EnableSubmitLogs = WebConfig.GetBool(nameof (EnableSubmitLogs), true);
  [PXInternalUseOnly]
  public static readonly int NumberOfNodesInCluster = WebConfig.GetInt(nameof (NumberOfNodesInCluster), 2);
  [PXInternalUseOnly]
  public static readonly bool RenderReportPdfCloseToHtml = WebConfig.GetBool(nameof (RenderReportPdfCloseToHtml), false);
  [PXInternalUseOnly]
  public static readonly ulong ProfilerDataSizeLimit = (ulong) WebConfig.GetQuota(nameof (ProfilerDataSizeLimit), 209715200L /*0x0C800000*/);
  [PXInternalUseOnly]
  public static readonly bool RestrictUpdates = WebConfig.GetBool(nameof (RestrictUpdates), false);
  [PXInternalUseOnly]
  public static readonly bool EnableOldUIGraphReusing = WebConfig.GetBool(nameof (EnableOldUIGraphReusing), false) && (!WebConfig.\u000E() || WebConfig.\u0002()) && !WebConfig.SerializeSessionItems;
  internal static readonly bool DisableClearSession = WebConfig.GetBool(nameof (DisableClearSession), false);

  private static bool \u0002() => WebConfig.GetBool("IsMultiSiteMode", false);

  private static bool \u000E() => WebConfig.GetBool("IsClusterEnabled", false);

  private static bool \u0006() => WebConfig.GetBool("SharedSessionEnabled", false);

  private static bool \u0008() => WebConfig.GetBool("EnableConcurrentSessionAccess", true);

  private static bool \u0003() => WebConfig.GetBool("UseTypeListDebugMode", false);

  internal static bool GetAndValidateIsClusterEnabled(bool _param0, bool _param1, bool _param2)
  {
    if (!_param2)
      return _param0 | _param1;
    if (_param0)
      throw new Exception("Unsupported appSettings combination: both IsMultiSiteMode and IsClusterEnabled are set to true");
    if (_param1)
      throw new Exception("Unsupported appSettings combination: both IsMultiSiteMode and SharedSessionEnabled are set to true");
    return true;
  }

  [PXInternalUseOnly]
  public static bool DesignMode
  {
    get
    {
      if (!WebConfig.\u0002.HasValue)
      {
        List<string> stringList = new List<string>()
        {
          "devenv",
          "vcsexpress",
          "vbexpress",
          "vcexpress",
          "sharpdevelop",
          "vstest.executionengine.x86",
          "vstest.executionengine"
        };
        using (Process currentProcess = Process.GetCurrentProcess())
        {
          string lower = currentProcess.ProcessName.ToLower();
          WebConfig.\u0002 = new bool?(!HostingEnvironment.IsHosted || stringList.Contains(lower));
        }
      }
      return WebConfig.\u0002.GetValueOrDefault();
    }
  }

  public static string GetString(string key, string def)
  {
    return WebConfig.GetString(WebConfigurationManager.AppSettings, key, def);
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  internal static string GetString(NameValueCollection _param0, string _param1, string _param2)
  {
    string str = _param0[_param1];
    return string.IsNullOrEmpty(str) ? _param2 : str;
  }

  public static bool GetBool(string key, bool def)
  {
    return WebConfig.GetBool(WebConfigurationManager.AppSettings, key, def);
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  internal static bool GetBool(NameValueCollection _param0, string _param1, bool _param2)
  {
    string str = _param0[_param1];
    if (string.IsNullOrEmpty(str))
      return _param2;
    bool result;
    if (!bool.TryParse(str, out result))
      throw new Exception("Invalid value in web.config for " + _param1);
    return result;
  }

  public static int GetInt(string key, int def)
  {
    return WebConfig.GetInt(WebConfigurationManager.AppSettings, key, def);
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  internal static int GetInt(NameValueCollection _param0, string _param1, int _param2)
  {
    string str = _param0[_param1];
    if (string.IsNullOrEmpty(str))
      return _param2;
    int result;
    if (!int.TryParse(str.ToLowerInvariant(), out result))
      throw new Exception("Invalid value in web.config for " + _param1);
    return result;
  }

  private static int? \u0002(NameValueCollection _param0, string _param1)
  {
    string str = _param0[_param1];
    if (string.IsNullOrEmpty(str))
      return new int?();
    int result;
    if (!int.TryParse(str.ToLowerInvariant(), out result))
      throw new Exception("Invalid value in web.config for " + _param1);
    return new int?(result);
  }

  [PXInternalUseOnly]
  public static long GetQuota(string key, long def)
  {
    if (!WebConfig.CalculateUsedSpace)
      return def;
    string appSetting = WebConfigurationManager.AppSettings[key];
    if (string.IsNullOrWhiteSpace(appSetting))
      return def;
    string s = appSetting.ToUpperInvariant().Trim();
    int num1 = 0;
    int num2 = 0;
    if (s.EndsWith("KB", StringComparison.Ordinal))
    {
      num2 = 2;
      num1 = 1;
    }
    else if (s.EndsWith("K", StringComparison.Ordinal))
    {
      num2 = 1;
      num1 = 1;
    }
    else if (s.EndsWith("MB", StringComparison.Ordinal))
    {
      num2 = 2;
      num1 = 2;
    }
    else if (s.EndsWith("M", StringComparison.Ordinal))
    {
      num2 = 1;
      num1 = 2;
    }
    else if (s.EndsWith("GB", StringComparison.Ordinal))
    {
      num2 = 2;
      num1 = 3;
    }
    else if (s.EndsWith("G", StringComparison.Ordinal))
    {
      num2 = 1;
      num1 = 3;
    }
    if (num2 > 0)
      s = s.Substring(0, s.Length - num2);
    long result;
    return !long.TryParse(s, out result) ? def : result << num1 * 10;
  }

  [PXInternalUseOnly]
  private static string \u0002()
  {
    return WebConfigurationManager.ConnectionStrings["PerformanceProfiler"]?.ConnectionString;
  }

  [PXInternalUseOnly]
  public static bool UseRuntimeCompilation
  {
    get => !WebConfig.DesignMode && WebConfig._UseRuntimeCompilation;
  }

  private static bool \u000F()
  {
    string appSetting = WebConfigurationManager.AppSettings["SerializeSessionItems"];
    bool? nullable1;
    if (!string.IsNullOrEmpty(appSetting))
    {
      bool result;
      if (!bool.TryParse(appSetting, out result))
        throw new Exception($"appSettings error: invalid value \"{appSetting}\" for SerializeSessionItems");
      nullable1 = new bool?(result);
    }
    else
      nullable1 = new bool?();
    bool? nullable2 = nullable1;
    if (WebConfig.\u000E())
    {
      if (!nullable2.HasValue || nullable2.GetValueOrDefault())
        return true;
      throw new Exception("Unsupported appSettings combination: IsClusterEnabled is set to true, but SerializeSessionItems is set to false");
    }
    if (WebConfig.\u0006())
    {
      if (!nullable2.HasValue)
        throw new Exception("Unsupported appSettings combination: SharedSessionEnabled is set to true, but SerializeSessionItems is missing. Please remove SharedSessionEnabled and use IsClusterEnabled instead.");
      if (nullable2.GetValueOrDefault())
        return true;
      throw new Exception("Unsupported appSettings combination: SharedSessionEnabled is set to true, but SerializeSessionItems is set to false");
    }
    if (!nullable2.HasValue || !nullable2.GetValueOrDefault())
      return false;
    throw new Exception("Unsupported appSettings combination: SerializeSessionItems is set to true, but no other cluster-related setting is present. Please remove SerializeSessionItems and use IsClusterEnabled instead.");
  }

  private static string \u000E()
  {
    string path = WebConfigurationManager.AppSettings["CustomizationTempFilesPath"];
    if (WebConfig.DesignMode)
      path = Path.GetTempPath();
    if (string.IsNullOrEmpty(path))
      path = WebConfig.\u0002("/CustomizationTempFilesPath", Environment.GetCommandLineArgs());
    if (string.IsNullOrEmpty(path))
      throw new Exception("CustomizationTempFilesPath is not defined in web.config.");
    if (!Directory.Exists(path))
      throw new Exception("The directory does not exist: " + path);
    if (!WebConfig.DesignMode && !string.IsNullOrEmpty(HostingEnvironment.ApplicationPhysicalPath) && path.StartsWith(HostingEnvironment.ApplicationPhysicalPath, StringComparison.OrdinalIgnoreCase))
      throw new Exception("CustomizationTempFilesPath should refer to a location outside the website folder.");
    return path;
  }

  private static string \u0002(string _param0, string[] _param1)
  {
    for (int index = 0; index < _param1.Length; ++index)
    {
      if (_param1[index] == _param0)
        return _param1[index + 1];
    }
    return (string) null;
  }

  [PXInternalUseOnly]
  public static string CustomizationTempFilesPath
  {
    get
    {
      if (WebConfig.\u000E == null)
        WebConfig.\u000E = WebConfig.\u000E();
      return WebConfig.\u000E;
    }
  }

  [PXInternalUseOnly]
  public static bool CompilationInDebugMode
  {
    get
    {
      if (!WebConfig.\u0006.HasValue)
      {
        CompilationSection section = (CompilationSection) ConfigurationManager.GetSection("system.web/compilation");
        WebConfig.\u0006 = new bool?(section != null && section.Debug);
      }
      return WebConfig.\u0006.GetValueOrDefault();
    }
  }

  [Serializable]
  private sealed class \u0002
  {
    public static readonly WebConfig.\u0002 \u0002 = new WebConfig.\u0002();

    internal string \u0002(string _param1) => _param1;

    internal string \u000E(string _param1) => _param1;
  }

  internal static class Keys
  {
    internal const string IsMultiSiteMode = "IsMultiSiteMode";
    internal const string IsClusterEnabled = "IsClusterEnabled";
    internal const string SharedSessionEnabled = "SharedSessionEnabled";
    internal const string EnableConcurrentSessionAccess = "EnableConcurrentSessionAccess";
    internal const string UseTypeListDebugMode = "UseTypeListDebugMode";
  }
}
