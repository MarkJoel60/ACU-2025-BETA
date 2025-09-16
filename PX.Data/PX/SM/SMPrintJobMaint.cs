// Decompiled with JetBrains decompiler
// Type: PX.SM.SMPrintJobMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Owin.DeviceHub;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#nullable enable
namespace PX.SM;

public class SMPrintJobMaint : PXGraph<
#nullable disable
SMPrintJobMaint, SMPrintJobFilter>
{
  public const string DeviceHubValueIdentifier = "_DeviceHub:";
  public PXFilter<SMPrintJobFilter> Filter;
  public PXSelect<SMPrintJob, Where<Where2<Where<SMPrintJob.createdDateTime, GreaterEqual<Current<SMPrintJobFilter.startDate>>, Or<Current<SMPrintJobFilter.startDate>, IsNull>>, And2<Where<SMPrintJob.createdDateTime, Less<Current<SMPrintJobFilter.endDatePlusOne>>, Or<Current<SMPrintJobFilter.endDate>, IsNull>>, And<Where2<Where<Current<SMPrintJobFilter.hideProcessed>, Equal<PX.Data.True>, And<SMPrintJob.status, NotEqual<PrintJobStatus.processed>>>, Or<Current<SMPrintJobFilter.hideProcessed>, NotEqual<PX.Data.True>>>>>>>, OrderBy<Desc<SMPrintJob.createdDateTime>>> Job;
  public PXSelect<SMPrintJobParameter, Where<SMPrintJobParameter.jobID, Equal<Current<SMPrintJob.jobID>>>> PrintJobParameters;
  private const string DeviceHubFeatureName = "PX.Objects.CS.FeaturesSet+deviceHub";
  public PXAction<SMPrintJobFilter> showReport;
  public PXAction<SMPrintJobFilter> reprint;

  public SMPrintJobMaint()
  {
    this.Job.Cache.AllowInsert = false;
    this.PrintJobParameters.Cache.AllowInsert = false;
    this.Insert.SetVisible(false);
    this.Delete.SetVisible(false);
    this.First.SetVisible(false);
    this.Previous.SetVisible(false);
    this.Next.SetVisible(false);
    this.Last.SetVisible(false);
    this.CopyPaste.SetVisible(false);
    if (!this.IsContractBasedAPI)
      return;
    this.Views[this.Job.View.Name] = this.Job.View = new PXView((PXGraph) this, this.Job.View.IsReadOnly, (BqlCommand) new PX.Data.Select<SMPrintJob, Where2<Where<Current<SMPrintJobFilter.jobID>, IsNull, And<SMPrintJob.status, Equal<PrintJobStatus.pending>, Or<SMPrintJob.jobID, Equal<Current<SMPrintJobFilter.jobID>>>>>, And2<Where<Current<SMPrintJobFilter.startModifiedDateTime>, IsNull, Or<SMPrintJob.lastModifiedDateTime, Greater<Current<SMPrintJobFilter.startModifiedDateTime>>>>, And<Where<Current<SMPrintJobFilter.endModifiedDateTime>, IsNull, Or<SMPrintJob.lastModifiedDateTime, LessEqual<Current<SMPrintJobFilter.endModifiedDateTime>>>>>>>>());
    PXUIFieldAttribute.SetEnabled<SMPrintJob.status>(this.Job.Cache, (object) null);
    this.Job.Cache.Adjust<PXUIFieldAttribute>().For<SMPrintJobFilter.jobID>((System.Action<PXUIFieldAttribute>) (a =>
    {
      a.Visible = true;
      a.Visibility = PXUIVisibility.Visible;
    }));
  }

  [InjectDependency]
  internal IDeviceHubService DeviceHubService { get; set; }

  [PXUIField(DisplayName = "Preview", MapViewRights = PXCacheRights.Select, MapEnableRights = PXCacheRights.Update)]
  [PXButton]
  protected virtual IEnumerable ShowReport(PXAdapter adapter)
  {
    Dictionary<string, string> parameters = new Dictionary<string, string>();
    string reportID = (string) null;
    foreach (PXResult<SMPrintJob, SMPrintJobParameter> pxResult in PXSelectBase<SMPrintJob, PXSelectJoin<SMPrintJob, InnerJoin<SMPrintJobParameter, On<SMPrintJobParameter.jobID, Equal<SMPrintJob.jobID>>>, Where<SMPrintJob.jobID, Equal<Current<SMPrintJob.jobID>>>>.Config>.Select((PXGraph) this))
    {
      SMPrintJob smPrintJob = (SMPrintJob) pxResult;
      SMPrintJobParameter printJobParameter = (SMPrintJobParameter) pxResult;
      if (reportID == null)
        reportID = smPrintJob.ReportID;
      string key = printJobParameter.ParameterName.Replace("~", string.Empty);
      if (printJobParameter.ParameterValue.Contains("_DeviceHub:"))
      {
        string[] source = printJobParameter.ParameterValue.Split(new string[1]
        {
          "_DeviceHub:"
        }, StringSplitOptions.None);
        if (((IEnumerable<string>) source).Count<string>() > 0)
          parameters.Add(key, ((IEnumerable<string>) source).First<string>());
      }
      else
        parameters.Add(key, printJobParameter.ParameterValue);
    }
    throw new PXReportRequiredException(parameters, reportID, "Report " + reportID);
  }

  [PXUIField(DisplayName = "Reprint", MapViewRights = PXCacheRights.Select, MapEnableRights = PXCacheRights.Update)]
  [PXButton]
  protected virtual IEnumerable Reprint(PXAdapter adapter)
  {
    List<SMPrintJob> smPrintJobList = new List<SMPrintJob>();
    foreach (PXResult<SMPrintJob> pxResult in this.Job.Select())
    {
      SMPrintJob smPrintJob = (SMPrintJob) pxResult;
      bool? selected = smPrintJob.Selected;
      bool flag = true;
      if (selected.GetValueOrDefault() == flag & selected.HasValue)
      {
        smPrintJob.Status = "P";
        smPrintJob.Selected = new bool?(false);
        smPrintJobList.Add(smPrintJob);
      }
    }
    this.Actions.PressSave();
    List<PrintJob> printJobs = new List<PrintJob>();
    foreach (SMPrintJob job in smPrintJobList)
      printJobs.Add(this.ResolveHubJob(job));
    this.LongOperationManager.StartAsyncOperation((Func<CancellationToken, Task>) (async ct => await SMPrintJobMaint.SendPrintJobsAsync(printJobs, this.DeviceHubService, ct)));
    return adapter.Get();
  }

  private static async Task SendPrintJobsAsync(
    List<PrintJob> printJobs,
    IDeviceHubService deviceHubService,
    CancellationToken cancellation)
  {
    foreach (IGrouping<string, PrintJob> source in (IEnumerable<IGrouping<string, PrintJob>>) printJobs.ToLookup<PrintJob, string>((Func<PrintJob, string>) (x => x.DeviceHubID)))
      await deviceHubService.SendPrintJobs(source.Key, source.ToArray<PrintJob>(), cancellation);
  }

  protected virtual void SMPrintJob_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    SMPrinter smPrinter = (SMPrinter) PXSelectorAttribute.Select<SMPrintJob.printerName>(sender, e.Row);
    if (smPrinter == null)
      return;
    bool? isActive = smPrinter.IsActive;
    bool flag = true;
    if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
      return;
    PXUIFieldAttribute.SetWarning<SMPrintJob.printerName>(sender, e.Row, "Printer is inactive.");
  }

  protected virtual void SMPrintJob_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Operation.Equals((object) PXDBOperation.Delete))
      return;
    bool flag = false;
    if (!(e.Row is SMPrintJob row) || !string.IsNullOrWhiteSpace(row.ReportID))
      return;
    foreach (PXResult<SMPrintJobParameter> pxResult in PXSelectBase<SMPrintJobParameter, PXSelect<SMPrintJobParameter, Where<SMPrintJobParameter.jobID, Equal<Required<SMPrintJobParameter.jobID>>>>.Config>.Select((PXGraph) this, (object) row.JobID))
    {
      if (((SMPrintJobParameter) pxResult).ParameterName.Trim().ToUpperInvariant().Equals("FILEID"))
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      throw new PXRowPersistingException(typeof (SMPrintJob.reportID).Name, (object) null, "Print job has no 'Report ID' or 'FILEID' parameter defined.");
  }

  public static async Task<bool> CreatePrintJobGroups(
    Dictionary<PrintSettings, PXReportRequiredException> printJobs,
    CancellationToken cancellationToken)
  {
    bool status = true;
    foreach (KeyValuePair<PrintSettings, PXReportRequiredException> printJob in printJobs)
    {
      if (!await SMPrintJobMaint.CreatePrintJobGroup(printJob.Key, printJob.Value, (string) null, cancellationToken))
        status = false;
    }
    return status;
  }

  /// <summary>
  /// Creates new print job. Automatically extracts ReportIDs and report parameters from PXReportRequiredException
  /// </summary>
  /// <param name="adapter"></param>
  /// <param name="GetPrinterFromNotification">expects NotificationUtility(...).SearchPrinter function</param>
  /// <param name="source"></param>
  /// <param name="reportID"></param>
  /// <param name="branchID"></param>
  /// <param name="ex"></param>
  /// <param name="description"></param>
  /// <returns>Returns true if print job is created successfully</returns>
  public static async Task<bool> CreatePrintJobGroup(
    PXAdapter adapter,
    Func<string, string, int?, Guid?> GetPrinterFromNotification,
    string source,
    string reportID,
    int? branchID,
    PXReportRequiredException ex,
    string description,
    CancellationToken cancellationToken)
  {
    return PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+deviceHub") && await SMPrintJobMaint.CreatePrintJobGroup(SMPrintJobMaint.GetPrintSettings(adapter, GetPrinterFromNotification, source, reportID, branchID), SMPrintJobMaint.CreateReportList(ex), SMPrintJobMaint.CreateDescription(adapter, description), cancellationToken);
  }

  public static async Task<bool> CreatePrintJobGroup(
    IPrintable printParameters,
    Func<string, string, int?, Guid?> GetPrinterFromNotification,
    string source,
    string reportID,
    int? branchID,
    PXReportRequiredException ex,
    string description,
    CancellationToken cancellationToken)
  {
    return PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+deviceHub") && await SMPrintJobMaint.CreatePrintJobGroup(SMPrintJobMaint.GetPrintSettings(printParameters, GetPrinterFromNotification, source, reportID, branchID), SMPrintJobMaint.CreateReportList(ex), description, cancellationToken);
  }

  public static async Task<bool> CreatePrintJobGroup(
    PXAdapter adapter,
    PXReportRequiredException ex,
    string description,
    CancellationToken cancellationToken)
  {
    return PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+deviceHub") && adapter != null && SMPrintJobMaint.IsSupported(adapter) && await SMPrintJobMaint.CreatePrintJobGroup(SMPrintJobMaint.GetPrintSettings(adapter), SMPrintJobMaint.CreateReportList(ex), SMPrintJobMaint.CreateDescription(adapter, description), cancellationToken);
  }

  public static Task<bool> CreatePrintJobGroup(
    PrintSettings printSettings,
    PXReportRequiredException ex,
    string description,
    CancellationToken cancellationToken)
  {
    return SMPrintJobMaint.CreatePrintJobGroup(printSettings, SMPrintJobMaint.CreateReportList(ex), description, cancellationToken);
  }

  public static async Task<bool> CreatePrintJobGroup(
    PrintSettings printSettings,
    List<KeyValuePair<string, Dictionary<string, string>>> reportList,
    string description,
    CancellationToken cancellationToken)
  {
    if (printSettings == null || !printSettings.PrinterID.HasValue || reportList == null)
      return false;
    Guid groupID = Guid.NewGuid();
    SMPrintJobMaint jobMaint = PXGraph.CreateInstance<SMPrintJobMaint>();
    foreach (KeyValuePair<string, Dictionary<string, string>> report in reportList)
    {
      if (!await jobMaint.CreatePrintJob(printSettings, report.Key, report.Value, new Guid?(groupID), description, cancellationToken))
        return false;
    }
    return true;
  }

  public static Task<bool> CreatePrintJob(
    PXAdapter adapter,
    string actualReportID,
    Dictionary<string, string> parameters,
    string description,
    CancellationToken cancellationToken)
  {
    return SMPrintJobMaint.CreatePrintJob(adapter, actualReportID, parameters, new Guid?(), description, cancellationToken);
  }

  public static async Task<bool> CreatePrintJobForRawFile(
    PXAdapter adapter,
    Func<string, string, int?, Guid?> GetPrinterFromNotification,
    string source,
    string reportID,
    int? branchID,
    Dictionary<string, string> parameters,
    string description,
    CancellationToken cancellationToken)
  {
    if (PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+deviceHub") && adapter != null && SMPrintJobMaint.IsSupported(adapter))
    {
      PrintSettings printSettings = SMPrintJobMaint.GetPrintSettings(adapter, GetPrinterFromNotification, source, reportID, branchID);
      if (printSettings != null && printSettings.PrinterID.HasValue)
        return await PXGraph.CreateInstance<SMPrintJobMaint>().CreatePrintJob(printSettings, (string) null, parameters, description, cancellationToken);
    }
    return false;
  }

  public static async Task<bool> CreatePrintJob(
    PXAdapter adapter,
    string actualReportID,
    Dictionary<string, string> parameters,
    Guid? groupID,
    string description,
    CancellationToken cancellationToken)
  {
    return PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+deviceHub") && adapter != null && SMPrintJobMaint.IsSupported(adapter) && await PXGraph.CreateInstance<SMPrintJobMaint>().CreatePrintJob(SMPrintJobMaint.GetPrintSettings(adapter), actualReportID, parameters, groupID, SMPrintJobMaint.CreateDescription(adapter, description), cancellationToken);
  }

  private static bool IsSupported(PXAdapter adapter)
  {
    return adapter.MassProcess || adapter.QuickProcessFlow != 0;
  }

  public virtual async Task<bool> CreatePrintJob(
    PrintSettings printSettings,
    string actualReportID,
    Dictionary<string, string> parameters,
    string description,
    CancellationToken cancellationToken)
  {
    return await this.CreatePrintJob(printSettings, actualReportID, parameters, new Guid?(), description, cancellationToken);
  }

  public virtual async Task<bool> CreatePrintJob(
    PrintSettings printSettings,
    string actualReportID,
    Dictionary<string, string> parameters,
    Guid? groupID,
    string description,
    CancellationToken cancellationToken)
  {
    if (!printSettings.PrinterID.HasValue)
      return false;
    using (PXTransactionScope ts = new PXTransactionScope())
    {
      await this.AddPrintJob(PXMessages.LocalizeFormatNoPrefix(description ?? actualReportID), printSettings, actualReportID, parameters, groupID, cancellationToken);
      ts.Complete();
    }
    return true;
  }

  public virtual async Task AddPrintJob(
    string description,
    PrintSettings printSettings,
    string reportID,
    Dictionary<string, string> parameters,
    Guid? groupID,
    CancellationToken cancellationToken)
  {
    SMPrintJobMaint graph = this;
    if (!PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+deviceHub") || printSettings == null || !printSettings.PrinterID.HasValue)
      return;
    SMPrinter smPrinter = (SMPrinter) PXSelectBase<SMPrinter, PXSelect<SMPrinter, Where<SMPrinter.printerID, Equal<Required<SMPrinter.printerID>>>>.Config>.Select((PXGraph) graph, (object) printSettings.PrinterID);
    if (smPrinter == null)
      return;
    SMPrintJob instance1 = (SMPrintJob) graph.Job.Cache.CreateInstance();
    instance1.Description = description;
    instance1.DeviceHubID = smPrinter.DeviceHubID;
    instance1.PrinterName = smPrinter.PrinterName;
    SMPrintJob smPrintJob = instance1;
    int? numberOfCopies = printSettings.NumberOfCopies;
    int? nullable;
    if (numberOfCopies.HasValue)
    {
      numberOfCopies = printSettings.NumberOfCopies;
      int num = 1;
      if (!(numberOfCopies.GetValueOrDefault() < num & numberOfCopies.HasValue))
      {
        nullable = printSettings.NumberOfCopies;
        goto label_6;
      }
    }
    nullable = new int?(1);
label_6:
    smPrintJob.NumberOfCopies = nullable;
    instance1.ReportID = reportID;
    instance1.Status = "P";
    instance1.GroupID = new Guid?(groupID ?? Guid.NewGuid());
    SMPrintJob job = graph.Job.Insert(instance1);
    foreach (KeyValuePair<string, string> parameter in parameters)
    {
      if (parameter.Value != null)
      {
        SMPrintJobParameter instance2 = (SMPrintJobParameter) graph.PrintJobParameters.Cache.CreateInstance();
        instance2.ParameterName = parameter.Key;
        instance2.ParameterValue = parameter.Value;
        graph.PrintJobParameters.Insert(instance2);
      }
    }
    graph.Actions.PressSave();
    await graph.DeviceHubService.SendPrintJobs(job.DeviceHubID, new PrintJob[1]
    {
      graph.ResolveHubJob(job, parameters)
    }, cancellationToken);
  }

  public virtual PrintJob ResolveHubJob(SMPrintJob job)
  {
    Dictionary<string, string> dictionary = this.PrintJobParameters.View.SelectMultiBound((object[]) new SMPrintJob[1]
    {
      job
    }).RowCast<SMPrintJobParameter>().ToList<SMPrintJobParameter>().Select<SMPrintJobParameter, (int, SMPrintJobParameter)>((Func<SMPrintJobParameter, (int, SMPrintJobParameter)>) (x => (GetParameterOrder(x.ParameterName), x))).OrderBy<(int, SMPrintJobParameter), int>((Func<(int, SMPrintJobParameter), int>) (x => x.Order)).ToDictionary<(int, SMPrintJobParameter), string, string>((Func<(int, SMPrintJobParameter), string>) (x => x.Parameter.ParameterName), (Func<(int, SMPrintJobParameter), string>) (x => x.Parameter.ParameterValue));
    return this.ResolveHubJob(job, dictionary);

    static int GetParameterOrder(string parameter)
    {
      int num = parameter.LastIndexOf("~");
      int result;
      if (num > 0 && num < parameter.Length)
      {
        if (!int.TryParse(parameter.Substring(num + "~".Length), out result))
          result = -1;
      }
      else
        result = -1;
      return result;
    }
  }

  public virtual PrintJob ResolveHubJob(SMPrintJob job, Dictionary<string, string> parameters)
  {
    System.DateTime dateTime = PXTimeZoneInfo.ConvertTimeFromUtc(PXTimeZoneInfo.ConvertTimeToUtc(job.LastModifiedDateTime ?? PXTimeZoneInfo.Now, LocaleInfo.GetTimeZone()), PXTimeZoneInfo.Local);
    PrintJob printJob = new PrintJob();
    printJob.JobID = job.JobID.ToString();
    printJob.DeviceHubID = job.DeviceHubID;
    printJob.Status = job.Status;
    printJob.Version = dateTime;
    printJob.Description = job.Description;
    printJob.GroupID = job.GroupID.ToString();
    printJob.NumberOfCopies = job.NumberOfCopies ?? 1;
    printJob.PrinterName = job.PrinterName;
    printJob.ReportID = job.ReportID;
    printJob.Parameters = parameters;
    return printJob;
  }

  private static List<KeyValuePair<string, Dictionary<string, string>>> CreateReportList(
    PXReportRequiredException ex)
  {
    if (ex == null)
      return new List<KeyValuePair<string, Dictionary<string, string>>>();
    List<KeyValuePair<string, Dictionary<string, string>>> reportList = new List<KeyValuePair<string, Dictionary<string, string>>>();
    if (ex.HasSiblings)
    {
      foreach (KeyValuePair<string, object> keyValuePair1 in (List<KeyValuePair<string, object>>) ex.Value)
      {
        Dictionary<string, string> dictionary = PXReportRedirectParameters.UnwrapParameters(keyValuePair1.Value);
        if (dictionary != null)
        {
          if (keyValuePair1.Key != null)
          {
            KeyValuePair<string, Dictionary<string, string>> keyValuePair2 = new KeyValuePair<string, Dictionary<string, string>>(keyValuePair1.Key, dictionary);
            reportList.Add(keyValuePair2);
          }
          else
          {
            string key;
            if (dictionary.TryGetValue("ReportIDParamKey", out key))
              dictionary.Remove("ReportIDParamKey");
            else
              key = (string) null;
            KeyValuePair<string, Dictionary<string, string>> keyValuePair3 = new KeyValuePair<string, Dictionary<string, string>>(key, dictionary);
            reportList.Add(keyValuePair3);
          }
        }
      }
    }
    else if (ex.ReportID != null && ex.Parameters != null && ex.Resultset == null)
    {
      KeyValuePair<string, Dictionary<string, string>> keyValuePair = new KeyValuePair<string, Dictionary<string, string>>(ex.ReportID, ex.Parameters);
      reportList.Add(keyValuePair);
    }
    return reportList;
  }

  public static PrintSettings GetPrintSettings(
    IPrintable printParameters,
    Func<string, string, int?, Guid?> GetPrinterFromNotification,
    string source,
    string reportID,
    int? branchID)
  {
    return SMPrintJobMaint.GetPrintSettings(printParameters, (Func<Guid?>) (() =>
    {
      if (string.IsNullOrEmpty(reportID))
        return new Guid?();
      Func<string, string, int?, Guid?> func = GetPrinterFromNotification;
      return func == null ? new Guid?() : func(source, reportID, branchID);
    }));
  }

  public static PrintSettings GetPrintSettings(
    IPrintable printParameters,
    Func<Guid?> defaultPrinterGetter)
  {
    return SMPrintJobMaint.SetDefaultPrinter(SMPrintJobMaint.GetPrintSettings(printParameters), defaultPrinterGetter);
  }

  public static PrintSettings GetPrintSettings(IPrintable printParameters)
  {
    if (PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+deviceHub") && printParameters != null)
    {
      bool? nullable1 = printParameters.PrintWithDeviceHub;
      bool flag = true;
      if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
      {
        PrintSettings printSettings = new PrintSettings();
        bool? nullable2;
        if (printParameters == null)
        {
          nullable1 = new bool?();
          nullable2 = nullable1;
        }
        else
          nullable2 = printParameters.PrintWithDeviceHub;
        nullable1 = nullable2;
        printSettings.PrintWithDeviceHub = new bool?(nullable1.GetValueOrDefault());
        bool? nullable3;
        if (printParameters == null)
        {
          nullable1 = new bool?();
          nullable3 = nullable1;
        }
        else
          nullable3 = printParameters.DefinePrinterManually;
        nullable1 = nullable3;
        printSettings.DefinePrinterManually = new bool?(nullable1.GetValueOrDefault());
        printSettings.PrinterID = (Guid?) printParameters?.PrinterID;
        printSettings.NumberOfCopies = new int?((int?) printParameters?.NumberOfCopies ?? 1);
        return printSettings;
      }
    }
    return new PrintSettings();
  }

  public static PrintSettings GetPrintSettings(
    PXAdapter adapter,
    Func<string, string, int?, Guid?> GetPrinterFromNotification,
    string source,
    string reportID,
    int? branchID)
  {
    return SMPrintJobMaint.GetPrintSettings(adapter, (Func<Guid?>) (() =>
    {
      if (string.IsNullOrEmpty(reportID))
        return new Guid?();
      Func<string, string, int?, Guid?> func = GetPrinterFromNotification;
      return func == null ? new Guid?() : func(source, reportID, branchID);
    }));
  }

  public static PrintSettings GetPrintSettings(PXAdapter adapter, Func<Guid?> defaultPrinterGetter)
  {
    return SMPrintJobMaint.SetDefaultPrinter(SMPrintJobMaint.GetPrintSettings(adapter), defaultPrinterGetter);
  }

  public static PrintSettings GetPrintSettings(PXAdapter adapter)
  {
    if (!PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+deviceHub") || adapter == null || !SMPrintJobMaint.IsSupported(adapter))
      return new PrintSettings();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    PrintSettings printSettings = new PrintSettings()
    {
      PrintWithDeviceHub = new bool?(get<bool>("PrintWithDeviceHub", SMPrintJobMaint.\u003C\u003EO.\u003C0\u003E__TryParse ?? (SMPrintJobMaint.\u003C\u003EO.\u003C0\u003E__TryParse = new SMPrintJobMaint.TryTransformDelegate<string, bool>(bool.TryParse))).GetValueOrDefault()),
      DefinePrinterManually = new bool?(get<bool>("DefinePrinterManually", SMPrintJobMaint.\u003C\u003EO.\u003C0\u003E__TryParse ?? (SMPrintJobMaint.\u003C\u003EO.\u003C0\u003E__TryParse = new SMPrintJobMaint.TryTransformDelegate<string, bool>(bool.TryParse))).GetValueOrDefault()),
      PrinterID = get<Guid>("PrinterID", SMPrintJobMaint.\u003C\u003EO.\u003C1\u003E__TryParse ?? (SMPrintJobMaint.\u003C\u003EO.\u003C1\u003E__TryParse = new SMPrintJobMaint.TryTransformDelegate<string, Guid>(Guid.TryParse))),
      NumberOfCopies = new int?(get<int>("NumberOfCopies", SMPrintJobMaint.\u003C\u003EO.\u003C2\u003E__TryParse ?? (SMPrintJobMaint.\u003C\u003EO.\u003C2\u003E__TryParse = new SMPrintJobMaint.TryTransformDelegate<string, int>(int.TryParse))) ?? 1)
    };
    bool? printWithDeviceHub = printSettings.PrintWithDeviceHub;
    bool flag = true;
    return !(printWithDeviceHub.GetValueOrDefault() == flag & printWithDeviceHub.HasValue) ? new PrintSettings() : printSettings;

    T? get<T>(
      string name,
      SMPrintJobMaint.TryTransformDelegate<string, T> tryParse)
      where T : struct
    {
      object obj;
      T result;
      return adapter?.Arguments != null && adapter.Arguments.TryGetValue(name, out obj) && obj != null && tryParse(obj.ToString(), out result) ? new T?(result) : new T?();
    }
  }

  [Obsolete("Use the GetPrinterSettings(PXAdapter) instead")]
  public static PrintSettings GetPrintSettingsSkipNotification(PXAdapter adapter)
  {
    return SMPrintJobMaint.GetPrintSettings(adapter);
  }

  private static PrintSettings SetDefaultPrinter(
    PrintSettings printSettings,
    Func<Guid?> defaultPrinterGetter)
  {
    bool? printWithDeviceHub = printSettings.PrintWithDeviceHub;
    bool flag1 = true;
    if (printWithDeviceHub.GetValueOrDefault() == flag1 & printWithDeviceHub.HasValue)
    {
      bool? definePrinterManually = printSettings.DefinePrinterManually;
      bool flag2 = true;
      if (!(definePrinterManually.GetValueOrDefault() == flag2 & definePrinterManually.HasValue))
      {
        Guid? nullable = defaultPrinterGetter != null ? defaultPrinterGetter() : new Guid?();
        if (nullable.HasValue)
          printSettings.PrinterID = nullable;
      }
    }
    return printSettings;
  }

  public static Dictionary<PrintSettings, PXReportRequiredException> AssignPrintJobToPrinter(
    Dictionary<PrintSettings, PXReportRequiredException> printJobList,
    Dictionary<string, string> parameters,
    IPrintable printParameters,
    Func<string, string, int?, Guid?> GetPrinterFromNotification,
    string source,
    string reportID,
    string actualReportID,
    int? branchID,
    CurrentLocalization localization = null)
  {
    if (PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+deviceHub"))
    {
      PrintSettings printSettings = SMPrintJobMaint.GetPrintSettings(printParameters, GetPrinterFromNotification, source, reportID, branchID);
      printJobList = SMPrintJobMaint.CombineReportRequiredExceptions(printJobList, printSettings, parameters, actualReportID, localization);
    }
    return printJobList;
  }

  public static Dictionary<PrintSettings, PXReportRequiredException> AssignPrintJobToPrinter(
    Dictionary<PrintSettings, PXReportRequiredException> printJobList,
    Dictionary<string, string> parameters,
    PXAdapter adapter,
    Func<string, string, int?, Guid?> GetPrinterFromNotification,
    string source,
    string reportID,
    string actualReportID,
    int? branchID,
    CurrentLocalization localization = null)
  {
    if (PXAccess.FeatureInstalled("PX.Objects.CS.FeaturesSet+deviceHub"))
    {
      PrintSettings printSettings = SMPrintJobMaint.GetPrintSettings(adapter, GetPrinterFromNotification, source, reportID, branchID);
      printJobList = SMPrintJobMaint.CombineReportRequiredExceptions(printJobList, printSettings, parameters, actualReportID, localization);
    }
    return printJobList;
  }

  private static Dictionary<PrintSettings, PXReportRequiredException> CombineReportRequiredExceptions(
    Dictionary<PrintSettings, PXReportRequiredException> printJobs,
    PrintSettings printSettings,
    Dictionary<string, string> parameters,
    string actualReportID,
    CurrentLocalization localization = null)
  {
    if (printSettings != null && printSettings.PrinterID.HasValue)
    {
      if (printJobs.ContainsKey(printSettings))
        printJobs[printSettings] = PXReportRequiredException.CombineReport(printJobs[printSettings], actualReportID, parameters, true, true, localization);
      else
        printJobs.Add(printSettings, PXReportRequiredException.CombineReport((PXReportRequiredException) null, actualReportID, parameters, true, true, localization));
    }
    return printJobs;
  }

  public static string CreateDescription(PXAdapter adapter, string description)
  {
    if (adapter != null)
    {
      PXGraph.CreateInstance<SMPrintJobMaint>();
      if (description == null)
        description = adapter.Menu;
    }
    return description;
  }

  private delegate bool TryTransformDelegate<in TIn, TOut>(TIn value, out TOut result);
}
