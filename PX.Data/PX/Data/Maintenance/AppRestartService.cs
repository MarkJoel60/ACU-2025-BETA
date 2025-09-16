// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.AppRestartService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Logging.Sinks.SystemEventsDbSink;
using Serilog;
using System;

#nullable disable
namespace PX.Data.Maintenance;

internal class AppRestartService : IAppRestartService
{
  private readonly ILogger _logger;
  private readonly IAppRestartNotificationTransport _notificationTransport;
  private readonly IAppRestartExecutor _restartExecutor;

  public AppRestartService(
    ILogger logger,
    IAppRestartNotificationTransport notificationTransport,
    IAppRestartExecutor restartExecutor)
  {
    this._logger = logger;
    this._notificationTransport = notificationTransport;
    this._restartExecutor = restartExecutor;
  }

  public void RequestRestart()
  {
    this._logger.Debug("Sending restart request");
    try
    {
      this._notificationTransport.SendRestartRequestNotification();
      this._logger.ForSystemEvents("System", "System_RestartRequestedEventId").Information("A request to restart the application has been made");
    }
    catch (Exception ex)
    {
      this._logger.Error(ex, "Failed to send restart notification");
      throw;
    }
  }

  public void Subscribe()
  {
    this._logger.Debug("Subscribing to restart notifications");
    this._notificationTransport.RestartRequested.Register(new System.Action(this.NotificationsHandler));
  }

  private void NotificationsHandler()
  {
    try
    {
      this._logger.ForSystemEvents("System", "System_RestartRequestInProgressEventId").ForContext("ContextUserIdentity", (object) null, false).ForCurrentCompanyContext().Information("The Application Restart Observer is restarting the application");
      this._restartExecutor.RestartApplication();
      this._logger.Information("Restart command has been executed");
    }
    catch (Exception ex)
    {
      this._logger.Error(ex, "Failed to handle restart notification");
      throw;
    }
  }
}
