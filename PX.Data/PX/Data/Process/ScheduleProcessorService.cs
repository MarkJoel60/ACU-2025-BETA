// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.ScheduleProcessorService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PX.Data.Services.Interfaces;
using PX.Data.Update;
using PX.Licensing;
using PX.Logging.Sinks.SystemEventsDbSink;
using PX.SM;
using Serilog;
using Serilog.Events;
using SerilogTimings;
using SerilogTimings.Extensions;
using System;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Data.Process;

internal sealed class ScheduleProcessorService : 
  IScheduleProcessorService,
  IHostedService,
  IDisposable
{
  private readonly Lazy<IScheduleProcessor> _lazyProcessor;
  private readonly ILicensing _licensing;
  private readonly IAppInstanceInfo _appInstanceInfo;
  private readonly ILogger _logger;
  private readonly ScheduleProcessorOptions _options;
  private volatile Thread _runner;
  private readonly object _sync = new object();
  private volatile CancellationTokenSource _stopTokenSource = new CancellationTokenSource();
  private const string SERVICE_NAME_PROPERTY = "ServiceName";
  private const string SERVICE_NAME = "ScheduleProcessorService";

  public ScheduleProcessorService(
    Lazy<IScheduleProcessor> lazyProcessor,
    ILicensing licensing,
    IOptions<ScheduleProcessorOptions> options,
    IAppInstanceInfo appInstanceInfo,
    ILogger logger)
  {
    if (options == null)
      throw new ArgumentNullException(nameof (options));
    this._lazyProcessor = lazyProcessor ?? throw new ArgumentNullException(nameof (lazyProcessor));
    this._licensing = licensing ?? throw new ArgumentNullException(nameof (licensing));
    this._appInstanceInfo = appInstanceInfo ?? throw new ArgumentNullException(nameof (appInstanceInfo));
    this._logger = logger ?? throw new ArgumentNullException(nameof (logger));
    this._options = options.Value;
  }

  public bool CanStart
  {
    get
    {
      return this._runner == null && !this._options.DisableScheduleProcessor && (PXAccess.GetMultiDatabaseUsers() != null || !PXAccess.NoConnectionString()) && !this._appInstanceInfo.IsPortal;
    }
  }

  public void Start() => this.Start(true);

  public void ClearLoggedSkippedSchedules()
  {
    if (!this._lazyProcessor.IsValueCreated)
      return;
    this._lazyProcessor.Value.ClearLoggedSkippedSchedules();
  }

  public Task StartAsync(CancellationToken cancellationToken)
  {
    this.Start(false);
    return Task.CompletedTask;
  }

  public async Task StopAsync(CancellationToken cancellationToken)
  {
    using (Operation operation = LoggerOperationExtensions.OperationAt(this._logger, (LogEventLevel) 1, new LogEventLevel?((LogEventLevel) 4)).Begin("Stopping {ServiceName}", new object[1]
    {
      (object) nameof (ScheduleProcessorService)
    }))
    {
      this._logger.Verbose<string>("Stopping {ServiceName}...", nameof (ScheduleProcessorService));
      this._stopTokenSource.Cancel();
      Thread runnerCopy = Interlocked.Exchange<Thread>(ref this._runner, (Thread) null);
      if (runnerCopy == null)
      {
        this._logger.Verbose<string>("{ServiceName} already stopped or not started", nameof (ScheduleProcessorService));
        operation.Complete();
        return;
      }
      await Task.Run((System.Action) (() => runnerCopy.Join()), cancellationToken);
      if (runnerCopy.IsAlive)
        runnerCopy.Abort();
      Interlocked.Exchange<CancellationTokenSource>(ref this._stopTokenSource, new CancellationTokenSource()).Dispose();
      operation.Complete();
    }
  }

  private void Start(bool forceInit)
  {
    using (Operation operation = LoggerOperationExtensions.OperationAt(this._logger, (LogEventLevel) 1, new LogEventLevel?((LogEventLevel) 4)).Begin("Starting {ServiceName}", new object[1]
    {
      (object) nameof (ScheduleProcessorService)
    }))
    {
      this._logger.Verbose<string>("Starting {ServiceName}...", nameof (ScheduleProcessorService));
      if (!this.CanStart || !this.IsAllowedToLaunch(forceInit))
      {
        this._logger.Information<string>("Can't start {ServiceName} as it's already started or disabled", nameof (ScheduleProcessorService));
        operation.Cancel();
      }
      else
      {
        lock (this._sync)
        {
          if (this._runner != null || this._stopTokenSource.IsCancellationRequested)
          {
            this._logger.Verbose<string>("Can't start {ServiceName} as it's already started or stopping", nameof (ScheduleProcessorService));
            operation.Cancel();
            return;
          }
          using (new PXImpersonationContext("admin"))
          {
            try
            {
              this._runner = new Thread((ThreadStart) (() => this._lazyProcessor.Value.Start(WindowsIdentity.GetCurrent(), this._stopTokenSource.Token)))
              {
                IsBackground = true
              };
              this._runner.Start();
              operation.Complete();
            }
            catch (Exception ex)
            {
              this._runner = (Thread) null;
              operation.SetException(ex);
              throw;
            }
          }
        }
        this._logger.ForSystemEvents("Scheduler", "Scheduler_SchedulerInitializedEventId").Information("Automation Scheduler has been initialized");
      }
    }
  }

  private bool IsAllowedToLaunch(bool forceInit)
  {
    try
    {
      using (new PXImpersonationContext(PXInstanceHelper.ScopeUser))
      {
        string prettyInstallationId = this._licensing.PrettyInstallationId;
        DatabaseInfo databaseInfo = PXInstanceHelper.DatabaseInfo;
        System.DateTime dtUtc;
        PXDatabase.SelectDate(out System.DateTime _, out dtUtc);
        if (databaseInfo == null || prettyInstallationId == null)
          return true;
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = false;
        foreach (Instance instance in Instance.SelectMulti())
        {
          flag3 = true;
          if (databaseInfo.FullDatabaseInfo == instance.DatabaseInfo)
            flag1 = true;
          if (prettyInstallationId == instance.InstallationID)
          {
            flag1 = true;
            flag2 = true;
          }
        }
        if (!flag2 && ((flag1 ? 1 : (!flag3 ? 1 : 0)) | (forceInit ? 1 : 0)) != 0)
          new Instance(prettyInstallationId, databaseInfo.FullDatabaseInfo, new System.DateTime?(dtUtc)).Insert();
        else if (flag2 && flag1 | forceInit)
          new Instance(prettyInstallationId, databaseInfo.FullDatabaseInfo, new System.DateTime?(dtUtc)).Update();
        return flag1 | forceInit || !flag3;
      }
    }
    catch
    {
    }
    return true;
  }

  public void Dispose()
  {
    this._stopTokenSource.Cancel();
    this._stopTokenSource.Dispose();
  }

  internal bool Started
  {
    get
    {
      Thread runner = this._runner;
      if (runner == null)
        return false;
      return runner.IsAlive || runner.Join(0);
    }
  }
}
